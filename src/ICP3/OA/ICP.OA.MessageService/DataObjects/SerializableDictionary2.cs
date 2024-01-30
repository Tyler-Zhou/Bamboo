using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection.Emit;
using System.Reflection;
using System.Collections;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// Implements a <see cref="Dictionary{TKey, TValue}"/> that can be safely 
    /// serialized to XML and deserialized back, preserving type information.
    /// </summary>
    /// <remarks>
    /// The serialization format will attempt to write the minimal information possible. 
    /// Typical format is as follows:
    /// <code>
    /// <dictionary>
    /// <entry>
    /// <key>foo</key>
    /// <value>25</value>
    /// </entry>
    /// <entry>
    /// <key>bar</key>
    /// <value>30</value>
    /// </entry>
    /// </dictionary>
    /// </code>
    /// The type of the key and the value are the same as the ones for 
    /// <typeparamref name="TKey"/> and <typeparamref name="TValue"/>. 
    /// If the type of a value for either one is a derived type, the 
    /// type information will be written in a <c>type</c> attribute, 
    /// which will be used to deserialize the XML with the appropriate 
    /// <see cref="XmlSerializer"/>. The serialized type name does not 
    /// include assembly version information, to make it more version-resilient.
    /// </remarks>
    [XmlRoot("dictionary")]
    public class SerializableDictionary2<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
#if NET20
internal delegate T Func<T>();
#endif

        private static readonly Dictionary<CacheKey, Func<XmlSerializer>> keySerializers = new Dictionary<CacheKey, Func<XmlSerializer>>();
        private static readonly Dictionary<CacheKey, Func<XmlSerializer>> valueSerializers = new Dictionary<CacheKey, Func<XmlSerializer>>();

        private static readonly XmlSerializerFactory serializerFactory = new XmlSerializerFactory();
        private static readonly XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

        /// <summary>
        /// Initializes an instance of the dictionary, setting the 
        /// <see cref="XmlRoot"/> namespace to an empty string.
        /// </summary>
        public SerializableDictionary2()
        {
            XmlRoot = new XmlRootAttribute("dictionary") { Namespace = "" };
        }

        /// <summary>
        /// Allows overriding of the xml namespace used to serialize 
        /// child elements.
        /// </summary>
        public XmlRootAttribute XmlRoot { get; set; }

        /// <summary>
        /// Tests whether an extensions dictionary can be read from the current
        /// <see cref="XmlReader"/> position using the default empty XML namespace Uri.
        /// </summary>
        /// <remarks>
        /// If the reader is in the <see cref="ReadState.Initial"/>, it's advanced 
        /// to the content for the check.
        /// </remarks>
        public static bool CanRead(XmlReader reader)
        {
            return CanRead(reader, new XmlRootAttribute("dictionary"));
        }

        /// <summary>
        /// Tests whether an extensions dictionary can be read from the current
        /// <see cref="XmlReader"/> position using the given root element information.
        /// </summary>
        /// <remarks>
        /// If the reader is in the <see cref="ReadState.Initial"/>, it's advanced 
        /// to the content for the check.
        /// </remarks>
        public static bool CanRead(XmlReader reader, XmlRootAttribute xmlRoot)
        {
            if (reader.ReadState == ReadState.Initial)
                reader.MoveToContent();

            return reader.NodeType == XmlNodeType.Element &&
            reader.LocalName == xmlRoot.ElementName &&
            reader.NamespaceURI == xmlRoot.Namespace;
        }

        /// <summary>
        /// Reads the dictionary using the default root element name and namespace.
        /// </summary>
        public static SerializableDictionary2<TKey, TValue> ReadXml(XmlReader reader)
        {
            return ReadXml(reader, new XmlRootAttribute("dictionary"));
        }

        /// <summary>
        /// Reads the dictionary using the given root element override.
        /// </summary>
        public static SerializableDictionary2<TKey, TValue> ReadXml(XmlReader reader, XmlRootAttribute xmlRoot)
        {
            if (!CanRead(reader, xmlRoot))
                XmlExceptions.ThrowXmlException(String.Format(
                "Unexpected element <{0} xmlns='{1}' ...>.",
                reader.LocalName, reader.NamespaceURI),
                reader);

            var extensions = new SerializableDictionary2<TKey, TValue>();
            ((IXmlSerializable)extensions).ReadXml(reader);

            return extensions;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(XmlRoot.ElementName, XmlRoot.Namespace);
            ((IXmlSerializable)this).WriteXml(writer);
            writer.WriteEndElement();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            var rootNamespaceUri = reader.NamespaceURI;
            if (reader.ReadToDescendant("entry", rootNamespaceUri))
            {
                var depth = reader.Depth;
                do
                {
                    using (var entry = reader.ReadSubtree())
                    {
                        if (reader.ReadToDescendant("key", rootNamespaceUri))
                        {
                            bool skip = false;
                            var tKey = ReadType(reader, typeof(TKey), out skip);
                            if (skip) continue;

                            var keySerializer = GetSerializer(keySerializers, tKey, new XmlRootAttribute("key") { Namespace = rootNamespaceUri });
                            var key = keySerializer.Deserialize(reader.ReadSubtree());

                            if (reader.ReadToNextSibling("value", rootNamespaceUri))
                            {
                                var tValue = ReadType(reader, typeof(TValue), out skip);
                                if (skip) continue;

                                var valueSerializer = GetSerializer(valueSerializers, tValue, new XmlRootAttribute("value") { Namespace = rootNamespaceUri });
                                var value = valueSerializer.Deserialize(reader.ReadSubtree());

                                Add((TKey)key, (TValue)value);
                            }
                            else
                            {
                                Add((TKey)key, default(TValue));
                            }
                        }
                    }
                } while (reader.Read() && reader.MoveToContent() == XmlNodeType.Element && reader.Depth >= depth);
            }
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            var tKey = typeof(TKey);
            var tValue = typeof(TValue);

            foreach (var item in this)
            {
                try
                {
                    // Ensure we can create the serializers first for the types
                    var keyType = item.Key.GetType();
                    var valueType = item.Value != null ? item.Value.GetType() : tValue;

                    // Optimize if we know the data is not serializable up front.
                    if (!
                    (keyType.IsPublic || keyType.IsNestedPublic) &&
                    (valueType.IsPublic || valueType.IsNestedPublic))
                        continue;

                    var keyWriter = keyType == tKey ? writer : new TypeWriter(writer, keyType);
                    keyWriter = new NonXsiXmlWriter(keyWriter);
                    var keySerializer = GetSerializer(keySerializers, keyType, new XmlRootAttribute("key") { Namespace = XmlRoot.Namespace });

                    if (item.Value != null)
                    {
                        var valueWriter = valueType == tValue ? writer : new TypeWriter(writer, valueType);
                        valueWriter = new NonXsiXmlWriter(valueWriter);
                        var valueSerializer = GetSerializer(valueSerializers, valueType, new XmlRootAttribute("value") { Namespace = XmlRoot.Namespace });

                        writer.WriteStartElement("entry");
                        // Serialize Key
                        keySerializer.Serialize(keyWriter, item.Key, serializerNamespaces);
                        keyWriter.Flush();

                        // Serialize Value
                        valueSerializer.Serialize(valueWriter, item.Value, serializerNamespaces);
                        valueWriter.Flush();

                        // Make sure we close the entry tag.
                        writer.WriteEndElement();
                    }
                    else
                    {
                        writer.WriteStartElement("entry");
                        // Serialize Key
                        keySerializer.Serialize(keyWriter, item.Key, serializerNamespaces);
                        keyWriter.Flush();

                        // Make sure we close the entry tag.
                        writer.WriteEndElement();
                    }

                }
                catch (Exception)
                {
                }
            }
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotImplementedException();
        }

        private Type ReadType(XmlReader reader, Type defaultType, out bool shouldSkip)
        {
            var result = defaultType;
            shouldSkip = false;

            var typeName = reader.GetAttribute("type");
            if (!String.IsNullOrEmpty(typeName))
            {
                var type = Type.GetType(typeName);

                if (type == null || !defaultType.IsAssignableFrom(type) ||
                !(type.IsPublic || type.IsNestedPublic))
                    shouldSkip = true;

                if (type != null)
                    result = type;
            }

            return result;
        }

        private XmlSerializer GetSerializer(Dictionary<CacheKey, Func<XmlSerializer>> cache, Type forType, XmlRootAttribute root)
        {
            Func<XmlSerializer> factory;
            var key = new CacheKey { Type = forType, Root = root };
            if (!cache.TryGetValue(key, out factory))
            {
                var serializer = serializerFactory.CreateSerializer(forType, root);
                factory = BuildSerializerFactory(serializer.GetType());
                cache[key] = factory;
            }

            return factory();
        }

        private Func<XmlSerializer> BuildSerializerFactory(Type serializerType)
        {
            // generate new serializerType() anonymous factory function.
            var method = new DynamicMethod("KeyString", serializerType, null);

            // Preparing Reflection instances
            var ctor = serializerType.GetConstructor(
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
            null, new Type[] { }, null);
            // Setting return type
            // Adding parameters
            ILGenerator gen = method.GetILGenerator();
            // Preparing locals
            LocalBuilder lb = gen.DeclareLocal(serializerType);
            // Preparing labels
            Label label9 = gen.DefineLabel();
            // Writing body
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Newobj, ctor);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Br_S, label9);
            gen.MarkLabel(label9);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Ret);
            // finished

            return (Func<XmlSerializer>)method.CreateDelegate(typeof(Func<XmlSerializer>));
        }

        private class TypeWriter : XmlWrappingWriter
        {
            bool root = true;
            Type type;

            public TypeWriter(XmlWriter baseWriter, Type type)
                : base(baseWriter)
            {
                this.type = type;
            }

            public override void WriteStartElement(string prefix, string localName, string ns)
            {
                base.WriteStartElement(prefix, localName, ns);

                if (root)
                {
                    var typeName = Type.GetType(type.FullName) != null ?
                    type.FullName :
                    type.FullName + ", " + type.Assembly.FullName.Substring(0, type.Assembly.FullName.IndexOf(",")).Trim();

                    base.WriteAttributeString("type", typeName);
                    root = false;
                }
            }
        }

        private class NonXsiXmlWriter : XmlWrappingWriter
        {
            bool skip = false;

            public NonXsiXmlWriter(XmlWriter baseWriter)
                : base(baseWriter)
            {
            }

            public override void WriteStartAttribute(string prefix, string localName, string ns)
            {
                if (prefix == "xmlns" && (localName == "xsd" || localName == "xsi"))
                {
                    skip = true;
                    return;
                }

                base.WriteStartAttribute(prefix, localName, ns);
            }

            public override void WriteString(string text)
            {
                if (skip)
                    return;

                base.WriteString(text);
            }

            public override void WriteEndAttribute()
            {
                if (skip)
                {
                    skip = false;
                    return;
                }

                base.WriteEndAttribute();
            }
        }

        private static class XmlExceptions
        {
            internal static void ThrowIfAttributeMissing(string attributeName, XmlReader reader)
            {
                if (String.IsNullOrEmpty(reader.GetAttribute(attributeName)))
                {
                    ThrowXmlException(
                    String.Format(
                    "Attribute '{0}' is required.",
                    attributeName),
                    reader);
                }
            }

            internal static void ThrowXmlException(string message, XmlReader reader)
            {
                int lineNumber = -1;
                int linePosition = -1;

                var info = reader as IXmlLineInfo;
                if (info != null && info.HasLineInfo())
                {
                    lineNumber = info.LineNumber;
                    linePosition = info.LinePosition;
                }

                var summary = new StringBuilder();
                if (reader.NodeType != XmlNodeType.Element)
                    reader.MoveToElement();

                using (XmlWriter w = XmlWriter.Create(summary, new XmlWriterSettings { OmitXmlDeclaration = true }))
                {
                    w.WriteNode(new SummaryXmlReader(
                    reader.LocalName, reader.NamespaceURI, reader.ReadSubtree()),
                    false);
                }

                throw new XmlException(
                String.Format(
                @"{0}
There is an error in the XML document or fragment:
{1}",
                message,
                summary.ToString().Substring(0, summary.ToString().Length - 2).Trim() + ">"),
                null,
                lineNumber,
                linePosition);
            }

            class SummaryXmlReader : XmlWrappingReader
            {
                string emptyLocalName;
                string namespaceUri;
                bool eof;

                public SummaryXmlReader(string emptyLocalName, string namespaceUri, XmlReader baseReader)
                    : base(baseReader)
                {
                    this.emptyLocalName = emptyLocalName;
                    this.namespaceUri = namespaceUri;
                }

                public new XmlReader MoveToContent()
                {
                    base.MoveToContent();
                    return this;
                }

                public override bool Read()
                {
                    if (LocalName == emptyLocalName &&
                    NamespaceURI == namespaceUri &&
                    NodeType == XmlNodeType.Element)
                    {
                        eof = true;
                    }

                    return !eof && base.Read();
                }

                public override bool IsEmptyElement
                {
                    get
                    {
                        if (LocalName == emptyLocalName && NamespaceURI == namespaceUri)
                            return true;
                        else
                            return base.IsEmptyElement;
                    }
                }
            }
        }

        private class CacheKey
        {
            public Type Type;
            public XmlRootAttribute Root;

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(this, obj)) return true;

                var x = this;
                var y = obj as CacheKey;

                if (Equals(null, y)) return false;

                return x.Type == y.Type &&
                x.Root.ElementName == y.Root.ElementName &&
                x.Root.Namespace == y.Root.Namespace;
            }

            public override int GetHashCode()
            {
                return Type.GetHashCode() ^ Root.ElementName.GetHashCode() ^ Root.Namespace.GetHashCode();
            }
        }
    }

    /// <summary>
    /// Extensions to <see cref="IEnumerable{T}"/>.
    /// </summary>
#if NetFx 
public static class EnumerableExtensions
#else
    internal static class EnumerableExtensions
#endif
    {
        /// <summary>
        /// Iterates the <paramref name="source"/> and applies the <paramref name="action"/> 
        /// to each item.
        /// </summary>
        public static void ForEach<TItem>(this IEnumerable<TItem> source, Action<TItem> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Allows chaining actions on a set of items for later processing, filtering 
        /// or projection (transformation), ignoring exceptions that might happen in the action.
        /// </summary>
        /// <returns>An enumeration with the same items as the source.</returns>
        public static IEnumerable<T> TryDo<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                try
                {
                    action(item);
                }
                catch { }

                yield return item;
            }
        }

        /// <summary>
        /// Allows chaining actions on a set of items for later processing, filtering 
        /// or projection (transformation).
        /// </summary>
        /// <returns>An enumeration with the same items as the source.</returns>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
                yield return item;
            }
        }

        /// <summary>
        /// Enumerates all elements in the source, so that any intermediate 
        /// action or side-effect from it is performed eagerly.
        /// </summary>
        public static void EnumerateAll<T>(this IEnumerable<T> source)
        {
            foreach (var item in source)
            {
            }
        }
    }
    /// <summary>
    /// Provides extensions to implement covariance of generic types.
    /// </summary>
#if NetFx 
public static class CovariantExtensions
#else
    internal static class CovariantExtensions
#endif
    {
        /// <summary>
        /// Allows for covariance of generic ICollections. Adapts a collection of type
        /// <typeparam name="T" /> into a collection of type <typeparam name="U" />
        /// </summary>
        public static ICollection<U> ToCovariant<T, U>(this ICollection<T> source)
        where T : U
        {
            return new CollectionInterfaceAdapter<T, U>(source);
        }

        /// <summary>
        /// Allows for covariance of generic ILists. Adapts a collection of type
        /// <typeparam name="T" /> into a collection of type <typeparam name="U" />
        /// </summary>
        public static IList<U> ToCovariant<T, U>(this IList<T> source)
        where T : U
        {
            return new ListInterfaceAdapter<T, U>(source);
        }

        /// <summary>
        /// Allows for covariance of generic IEnumerables. Adapts a collection of type
        /// <typeparam name="T" /> into a collection of type <typeparam name="U" />
        /// </summary>
        public static IEnumerable<U> ToCovariant<T, U>(this IEnumerable<T> source)
        where T : U
        {
            return new EnumerableInterfaceAdapter<T, U>(source);
        }

        /* Credits go to the Umbrella (http://codeplex.com/umbrella) project */

        /// <summary>
        /// Allows for covariance of generic ICollections. Adapts a collection of type
        /// <typeparam name="T" /> into a collection of type <typeparam name="U" />
        /// </summary>
        class CollectionInterfaceAdapter<T, U>
        : EnumerableInterfaceAdapter<T, U>, ICollection<U> where T : U
        {
            new ICollection<T> Target { get; set; }

            public void Add(U item)
            {
                Target.Add((T)item);
            }

            public void Clear()
            {
                Target.Clear();
            }

            public bool Contains(U item)
            {
                return Target.Contains((T)item);
            }

            public void CopyTo(U[] array, int arrayIndex)
            {
                for (int i = arrayIndex; i < Target.Count; i++)
                {
                    array[i] = Target.ElementAt(i);
                }
            }

            public bool Remove(U item)
            {
                return Target.Remove((T)item);
            }

            public int Count
            {
                get { return Target.Count; }
            }

            public bool IsReadOnly
            {
                get { return Target.IsReadOnly; }
            }

            public CollectionInterfaceAdapter(ICollection<T> target)
                : base(target)
            {
            }
        }

        /// <summary>
        /// Allows for covariance of generic IEnumerables. Adapts a collection of type
        /// <typeparam name="T" /> into a collection of type <typeparam name="U" />
        /// </summary>
        class EnumerableInterfaceAdapter<T, U> : IEnumerable<U> where T : U
        {
            public IEnumerable<T> Target { get; set; }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public IEnumerator<U> GetEnumerator()
            {
                foreach (T item in Target)
                    yield return item;
            }

            public EnumerableInterfaceAdapter(IEnumerable<T> target)
            {
                Target = target;
            }
        }

        /// <summary>
        /// Allows for covariance of generic ILists. Adapts a collection of type
        /// <typeparam name="T" /> into a collection of type <typeparam name="U" />
        /// </summary>
        class ListInterfaceAdapter<T, U> : CollectionInterfaceAdapter<T, U>, IList<U>
        where T : U
        {
            new IList<T> Target { get; set; }

            public int IndexOf(U item)
            {
                return Target.IndexOf((T)item);
            }

            public void Insert(int index, U item)
            {
                Target.Insert(index, (T)item);
            }

            public void RemoveAt(int index)
            {
                Target.RemoveAt(index);
            }

            public U this[int index]
            {
                get { return Target[index]; }
                set { Target[index] = (T)value; }
            }

            public ListInterfaceAdapter(IList<T> target)
                : base(target)
            {
            }
        }
    }





}
