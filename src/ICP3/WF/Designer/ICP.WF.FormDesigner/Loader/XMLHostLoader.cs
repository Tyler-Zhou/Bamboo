
//-----------------------------------------------------------------------
// <copyright file="XMLHostLoader.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Windows.Forms;
    using System.Xml;
    using ICP.WF.Controls;
    using DevExpress.XtraEditors;
    using ICP.WF.ServiceInterface.Client;
    using System.Collections.Generic;

    /// <summary>
    ///把界面序列化为XML文件，或根据XML文件反序列化为界面
    /// </summary>
    public class XMLDesignerLoader : BasicDesignerLoader
    {
        #region 本地变量

        private XmlDocument xmlDocument;
        private static readonly Attribute[] propertyAttributes = new Attribute[] { DesignOnlyAttribute.No };
        private Type rootComponentType;

        #endregion

        #region 构造函数

        /// <summary>
        ///传入Form或则UserControl的Type
        /// </summary>
        /// <param name="rootComponentType"></param>
        public XMLDesignerLoader(Type rootComponentType)
        {
            this.rootComponentType = rootComponentType;
            this.Modified = true;
        }

        /// <summary>
        ///传入一个XML文件(支持的格式)路径
        /// </summary>
        public XMLDesignerLoader(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            this.fileName = fileName;
        }

        #endregion

        #region BasicDesignerLoader 重载

        /// <summary>
        /// 当加载文档的时候调用该方法()
        /// </summary>
        /// <param name="designerSerializationManager">设计时序列化管理器</param>
        protected override void PerformLoad(IDesignerSerializationManager designerSerializationManager)
        {
            if (this.LoaderHost == null)
            {
                throw new ArgumentNullException(Utility.GetString("InvalidDesignerLoaderHost", "designerLoaderHost无效。"));
            }

            ArrayList errors = new ArrayList();
            bool successful = true;
            string baseClassName;
            if (fileName == null)
            {
                if (rootComponentType == typeof(LWBaseForm))
                {
                    LWBaseForm form = (LWBaseForm)this.LoaderHost.CreateComponent(typeof(LWBaseForm));
                    form.Name = "Form1";
                    form.Height=250;
                    LWTableLayoutPanel t1 = (LWTableLayoutPanel)this.LoaderHost.CreateComponent(typeof(LWTableLayoutPanel));
                    t1.RowCount = 8;
                    t1.ColumnCount = 4;
                    t1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                    t1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                    t1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
                    t1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

                    t1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    t1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    t1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    t1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    t1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    t1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    t1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    t1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    t1.Dock = DockStyle.Fill;
                    form.Controls.Add(t1);
                    baseClassName = "Form1";
                }
                else if (rootComponentType == typeof(UserControl))
                {
                    this.LoaderHost.CreateComponent(typeof(XtraUserControl));
                    baseClassName = "XtraUserControl1";
                }
                else if (rootComponentType == typeof(UserControl))
                {
                    this.LoaderHost.CreateComponent(typeof(UserControl));
                    baseClassName = "UserControl1";
                }
                else if (rootComponentType == typeof(Component))
                {
                    this.LoaderHost.CreateComponent(typeof(Component));
                    baseClassName = "Component1";
                }
                else
                {
                    throw new Exception(Utility.GetString("UndefinedHostType", "未定义的属主类型.: ") + rootComponentType.ToString());
                }
            }
            else
            {
                baseClassName = ReadFile(
                    fileName,
                    errors,
                    out xmlDocument);
            }

            this.LoaderHost.EndLoad(
                baseClassName,
                successful,
                errors);

            IComponentChangeService cs = this.LoaderHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            if (cs != null)
            {
                cs.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
                cs.ComponentAdded += new ComponentEventHandler(OnComponentAddedRemoved);
                cs.ComponentRemoved += new ComponentEventHandler(OnComponentAddedRemoved);
            }

            this.Modified = true;
        }

        protected override void PerformFlush(IDesignerSerializationManager designerSerializationManager)
        {
            if (this.Modified)
            {
                return;
            }

            PerformFlushWorker();
        }

        public override void Dispose()
        {
            IComponentChangeService cs = this.LoaderHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            if (cs != null)
            {
                cs.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                cs.ComponentAdded -= new ComponentEventHandler(OnComponentAddedRemoved);
                cs.ComponentRemoved -= new ComponentEventHandler(OnComponentAddedRemoved);
            }
        }

        #endregion

        #region 本地方法

        private bool GetConversionSupported(TypeConverter converter, Type conversionType)
        {
            return (converter.CanConvertFrom(conversionType)
                && converter.CanConvertTo(conversionType));
        }

        private void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
        {
            this.Modified = true;
        }

        private void OnComponentAddedRemoved(object sender, ComponentEventArgs ce)
        {
            this.Modified = true;
        }

        #endregion

        #region 序列化为文件

        public void PerformFlushWorker()
        {
            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));
            Hashtable nametable = new Hashtable(idh.Container.Components.Count);
            IDesignerSerializationManager manager = this.LoaderHost.GetService(typeof(IDesignerSerializationManager)) as IDesignerSerializationManager;

            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateElement("DOCUMENT_ELEMENT"));
            document.DocumentElement.AppendChild(WriteObject(
                document,
                nametable,
                idh.RootComponent));

            foreach (IComponent comp in idh.Container.Components)
            {
                //只有bindingsouce才可以与WFForm作兄弟节点，其他的只能是WFForm子节点
                if (comp != idh.RootComponent
                    && !nametable.ContainsKey(comp)
                    && comp.GetType().BaseType.Equals(typeof(BindingSource)))
                {
                    XmlNode node = WriteObject(document, nametable, comp);
                    document.DocumentElement.AppendChild(node);
                }
            }

            xmlDocument = document;
        }

        private XmlNode WriteObject(
            XmlDocument document,
            IDictionary nametable, object value)
        {
            XmlNode node = document.CreateElement("Object");
            XmlAttribute typeAttr = document.CreateAttribute("type");
            typeAttr.Value = value.GetType().AssemblyQualifiedName;
            node.Attributes.Append(typeAttr);

            IComponent component = value as IComponent;
            if (component != null
                && component.Site != null
                && component.Site.Name != null)
            {
                XmlAttribute nameAttr = document.CreateAttribute("name");
                nameAttr.Value = component.Site.Name;
                node.Attributes.Append(nameAttr);
                nametable[value] = component.Site.Name;
            }

            bool isControl = (value is Control);
            if (value is LWTableLayoutPanel)
            {
                XmlAttribute childAttr = document.CreateAttribute("children");
                childAttr.Value = "Controls";
                node.Attributes.Append(childAttr);
            }
            else if (isControl)
            {
                XmlAttribute childAttr = document.CreateAttribute("children");
                childAttr.Value = "Controls";
                node.Attributes.Append(childAttr);
            }

            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));
            if (component != null)
            {
                if (value is LWTableLayoutPanel)
                {
                    LWTableLayoutPanel tlpControl = (LWTableLayoutPanel)value;
                    foreach (Control child in tlpControl.Controls)
                    {
                        if (child.Site != null
                            && child.Site.Container == idh.Container)
                        {
                            XmlNode cellNode = document.CreateElement("Cell");

                            XmlAttribute rowAttr = document.CreateAttribute("Row");
                            rowAttr.Value = tlpControl.GetRow(child).ToString();
                            cellNode.Attributes.Append(rowAttr);

                            XmlAttribute rowSpanAttr = document.CreateAttribute("RowSpan");
                            rowSpanAttr.Value = tlpControl.GetRowSpan(child).ToString();
                            cellNode.Attributes.Append(rowSpanAttr);

                            XmlAttribute columnAttr = document.CreateAttribute("Column");
                            columnAttr.Value = tlpControl.GetColumn(child).ToString();
                            cellNode.Attributes.Append(columnAttr);

                            XmlAttribute columnSpanAttr = document.CreateAttribute("ColumnSpan");
                            columnSpanAttr.Value = tlpControl.GetColumnSpan(child).ToString();
                            cellNode.Attributes.Append(columnSpanAttr);

                            cellNode.AppendChild(WriteObject(document, nametable, child));

                            node.AppendChild(cellNode);
                        }
                    }
                }
                else if (isControl)
                {
                    foreach (Control child in ((Control)value).Controls)
                    {
                        if (child.Site != null
                            && child.Site.Container == idh.Container)
                        {
                            XmlNode childnode = WriteObject(
                                document,
                                nametable,
                                child);

                            node.AppendChild(childnode);
                        }
                    }
                }

                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(
                    value,
                    propertyAttributes);
                if (isControl)
                {
                    PropertyDescriptor controlProp = properties["Controls"];
                    if (controlProp != null)
                    {
                        PropertyDescriptor[] propArray = new PropertyDescriptor[properties.Count - 1];
                        int idx = 0;
                        foreach (PropertyDescriptor p in properties)
                        {
                            if (p != controlProp)
                            {
                                propArray[idx++] = p;
                            }
                        }

                        properties = new PropertyDescriptorCollection(propArray);
                    }
                }

                WriteProperties(
                    document,
                    properties,
                    value,
                    node,
                    "Property");
            }
            else
            {
                WriteValue(
                    document,
                    value,
                    node);
            }

            return node;
        }

        private void WriteProperties(
            XmlDocument document,
            PropertyDescriptorCollection properties,
            object value,
            XmlNode parent,
            string elementName)
        {
            foreach (PropertyDescriptor prop in properties)
            {
                if (prop.ShouldSerializeValue(value))
                {
                    //try
                    //{
                        string compName = parent.Name;
                        XmlNode node = document.CreateElement(elementName);
                        XmlAttribute attr = document.CreateAttribute("name");
                        attr.Value = prop.Name;
                        node.Attributes.Append(attr);

                        DesignerSerializationVisibilityAttribute visibility = (DesignerSerializationVisibilityAttribute)prop.Attributes[typeof(DesignerSerializationVisibilityAttribute)];
                        switch (visibility.Visibility)
                        {
                            case DesignerSerializationVisibility.Visible:
                                if (!prop.IsReadOnly
                                    && WriteValue(document, prop.GetValue(value), node))
                                {
                                    parent.AppendChild(node);
                                }
                                break;

                            case DesignerSerializationVisibility.Content:
                                object propValue = prop.GetValue(value);
                                if (typeof(IList).IsAssignableFrom(prop.PropertyType))
                                {
                                    WriteCollection(
                                        document,
                                        (IList)propValue,
                                        node);
                                }
                                else
                                {
                                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(
                                        propValue,
                                        propertyAttributes);

                                    WriteProperties(
                                        document,
                                        props,
                                        propValue,
                                        node,
                                        elementName);
                                }

                                if (node.ChildNodes.Count > 0)
                                {
                                    parent.AppendChild(node);
                                }
                                break;

                            default:
                                break;
                        }
                    //}
                    //catch
                    //{
                    //}
                }
            }
        }

        private XmlNode WriteReference(
            XmlDocument document,
            IComponent value)
        {
            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));

            XmlNode node = document.CreateElement("Reference");
            XmlAttribute attr = document.CreateAttribute("name");
            attr.Value = value.Site.Name;
            node.Attributes.Append(attr);

            return node;
        }

        private bool WriteValue(
            XmlDocument document,
            object value,
            XmlNode parent)
        {
            if (value == null)
            {
                return true;
            }

            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));
            TypeConverter converter = TypeDescriptor.GetConverter(value);
            if (GetConversionSupported(converter, typeof(string)))
            {
                parent.InnerText = (string)converter.ConvertTo(
                    null,
                    CultureInfo.InvariantCulture,
                    value,
                    typeof(string));
            }
            else if (GetConversionSupported(converter, typeof(byte[])))
            {
                byte[] data = (byte[])converter.ConvertTo(
                    null,
                    CultureInfo.InvariantCulture,
                    value,
                    typeof(byte[]));

                parent.AppendChild(WriteBinary(document, data));
            }
            else if (GetConversionSupported(converter, typeof(InstanceDescriptor)))
            {
                InstanceDescriptor id = (InstanceDescriptor)converter.ConvertTo(
                    null,
                    CultureInfo.InvariantCulture,
                    value,
                    typeof(InstanceDescriptor));

                parent.AppendChild(WriteInstanceDescriptor(document, id, value));
            }
            else if (value is IComponent
                && ((IComponent)value).Site != null
                && ((IComponent)value).Site.Container == idh.Container)
            {
                parent.AppendChild(WriteReference(document, (IComponent)value));
            }
            else if (value.GetType().IsSerializable)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                formatter.Serialize(stream, value);
                XmlNode binaryNode = WriteBinary(document, stream.ToArray());
                parent.AppendChild(binaryNode);
            }
            //else if (value.GetType().Equals(typeof(LWRadioButton)))
            //{
            //    //添加对列表对象支持
            //    Hashtable nametable = new Hashtable(idh.Container.Components.Count);
            //    parent.AppendChild(WriteObject(document, nametable, value));
            //}
            else
            {
                return false;
            }

            return true;
        }

        private void WriteCollection(XmlDocument document, IList list, XmlNode parent)
        {
            foreach (object obj in list)
            {
                XmlNode node = document.CreateElement("Item");
                XmlAttribute typeAttr = document.CreateAttribute("type");
                typeAttr.Value = obj.GetType().AssemblyQualifiedName;
                node.Attributes.Append(typeAttr);
                WriteValue(document, obj, node);
                parent.AppendChild(node);
            }
        }

        private XmlNode WriteBinary(
            XmlDocument document,
            byte[] value)
        {
            XmlNode node = document.CreateElement("Binary");
            node.InnerText = Convert.ToBase64String(value);
            return node;
        }

        private XmlNode WriteInstanceDescriptor(
            XmlDocument document,
            InstanceDescriptor desc,
            object value)
        {
            XmlNode node = document.CreateElement("InstanceDescriptor");
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, desc.MemberInfo);

            XmlAttribute memberAttr = document.CreateAttribute("member");
            memberAttr.Value = Convert.ToBase64String(stream.ToArray());
            node.Attributes.Append(memberAttr);
            foreach (object arg in desc.Arguments)
            {
                XmlNode argNode = document.CreateElement("Argument");
                if (WriteValue(document, arg, argNode))
                {
                    node.AppendChild(argNode);
                }
            }

            if (!desc.IsComplete)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(
                    value,
                    propertyAttributes);

                WriteProperties(document, props, value, node, "Property");
            }

            return node;
        }

        #endregion

        #region 反序列化为设计时对象

        private string ReadFile(
            string fileName,
            ArrayList errors,
            out XmlDocument document)
        {
            string baseClass = null;
            StreamReader sr = new StreamReader(fileName);

            try
            {
                string cleandown = sr.ReadToEnd();
                cleandown = "<DOCUMENT_ELEMENT>" + cleandown + "</DOCUMENT_ELEMENT>";

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(cleandown);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (baseClass == null)
                    {
                        baseClass = node.Attributes["name"].Value;
                    }

                    if (node.Name.Equals("Object"))
                    {
                        ReadObject(node, errors);
                    }
                    else
                    {
                        errors.Add(Utility.GetString("llowedHere", "这里不允许存在节点类型{0}.", node.Name));
                    }
                }

                document = doc;
            }
            catch (Exception ex)
            {
                document = null;
                errors.Add(ex);
            }
            finally
            {
                sr.Close();
                sr.Dispose();
            }
            return baseClass;
        }

        private void ReadEvent(
            XmlNode childNode,
            object instance,
            ArrayList errors)
        {
            IEventBindingService bindings = this.LoaderHost.GetService(typeof(IEventBindingService)) as IEventBindingService;
            if (bindings == null)
            {
                errors.Add(Utility.GetString("UnableBindAnyEvents", "Unable to contact event binding service so we can't bind any events"));
                return;
            }

            XmlAttribute nameAttr = childNode.Attributes["name"];
            if (nameAttr == null)
            {
                errors.Add(Utility.GetString("NoEventName", "没有事件名称"));
                return;
            }

            XmlAttribute methodAttr = childNode.Attributes["method"];
            if (methodAttr == null
                || methodAttr.Value == null
                || methodAttr.Value.Length == 0)
            {
                errors.Add(Utility.GetString("EventHasNotMethodBoundToIt", "Event {0} has no method bound to it", nameAttr));
                return;
            }

            EventDescriptor evt = TypeDescriptor.GetEvents(instance)[nameAttr.Value];
            if (evt == null)
            {
                errors.Add(Utility.GetString("EventDoesNotExist", "事件件{0}不存在类型{1}中.", nameAttr.Value, instance.GetType().FullName));
                return;
            }

            PropertyDescriptor prop = bindings.GetEventProperty(evt);
            try
            {
                prop.SetValue(instance, methodAttr.Value);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
        }

        private object ReadInstanceDescriptor(
            XmlNode node,
            ArrayList errors)
        {
            XmlAttribute memberAttr = node.Attributes["member"];
            if (memberAttr == null)
            {
                errors.Add(Utility.GetString("NoMemberAttribute", "不存在[Member]特性."));
                return null;
            }

            byte[] data = Convert.FromBase64String(memberAttr.Value);
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(data);
            MemberInfo mi = (MemberInfo)formatter.Deserialize(stream);
            object[] args = null;

            if (mi is MethodBase)
            {
                ParameterInfo[] paramInfos = ((MethodBase)mi).GetParameters();
                args = new object[paramInfos.Length];
                int idx = 0;
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.Name.Equals("Argument"))
                    {
                        object value;
                        if (!ReadValue(
                            child,
                            TypeDescriptor.GetConverter(paramInfos[idx].ParameterType),
                            errors,
                            out value))
                        {
                            return null;
                        }

                        args[idx++] = value;
                    }
                }

                if (idx != paramInfos.Length)
                {
                    errors.Add(Utility.GetString("MethodRequiresArguments", "方法{0}需要{1}个参数,而不是{2}个.", mi.Name, args.Length, idx));
                    return null;
                }
            }

            InstanceDescriptor id = new InstanceDescriptor(mi, args);
            object instance = id.Invoke();
            foreach (XmlNode prop in node.ChildNodes)
            {
                if (prop.Name.Equals("Property"))
                {
                    ReadProperty(prop, instance, errors);
                }
            }

            return instance;
        }

        private object ReadObject(
            XmlNode node,
            ArrayList errors)
        {
            XmlAttribute typeAttr = node.Attributes["type"];
            if (typeAttr == null)
            {
                errors.Add(Utility.GetString("ObjectTagMissingTypeAttribute", "<OBJECT>标记是缺少必要的type属性"));
                return null;
            }

            Type type = Type.GetType(typeAttr.Value);
            if (type == null)
            {
                errors.Add(Utility.GetString("TypeCouldNotBeLoaded", "类型{0}不能被加载.", typeAttr.Value));
                return null;
            }

            XmlAttribute nameAttr = node.Attributes["name"];
            object instance;
            if (typeof(IComponent).IsAssignableFrom(type))
            {
                if (nameAttr == null)
                {
                    instance = this.LoaderHost.CreateComponent(type);
                }
                else
                {
                    instance = this.LoaderHost.CreateComponent(type, nameAttr.Value);
                }
            }
            else
            {
                instance = Activator.CreateInstance(type);
            }

            XmlAttribute childAttr = node.Attributes["children"];
            IList childList = null;
            if (childAttr != null)
            {
                PropertyDescriptor childProp = TypeDescriptor.GetProperties(instance)[childAttr.Value];
                if (childProp == null)
                {
                    errors.Add(string.Format("The children attribute lists {0} as the child collection but this is not a property on {1}", childAttr.Value, instance.GetType().FullName));
                }
                else
                {
                    childList = childProp.GetValue(instance) as IList;
                    if (childList == null)
                    {
                        errors.Add(string.Format("The property {0} was found but did not return a valid IList", childProp.Name));
                    }
                }
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                if (childNode.Name.Equals("Cell"))
                {
                    LWTableLayoutPanel tableLayoutPanel = instance as LWTableLayoutPanel;
                    if (tableLayoutPanel != null && childList != null)
                    {
                        int row = int.Parse(childNode.Attributes["Row"].Value);
                        int rowSpan = int.Parse(childNode.Attributes["RowSpan"].Value);
                        int column = int.Parse(childNode.Attributes["Column"].Value);
                        int columnSpan = int.Parse(childNode.Attributes["ColumnSpan"].Value);

                        XmlNode ctrlNode = childNode.ChildNodes[0];
                        object childInstance = ReadObject(ctrlNode, errors);
                        childList.Add(childInstance);

                        Control ctrl = childInstance as Control;
                        if (ctrl != null)
                        {
                            tableLayoutPanel.SetRow(ctrl, row);
                            tableLayoutPanel.SetRowSpan(ctrl, rowSpan);
                            tableLayoutPanel.SetColumn(ctrl, column);
                            tableLayoutPanel.SetColumnSpan(ctrl, columnSpan);
                        }
                    }
                }
                else if (childNode.Name.Equals("Object"))
                {
                    if (childAttr == null)
                    {
                        errors.Add("Child object found but there is no children attribute");
                        continue;
                    }

                    if (childList != null)
                    {
                        object childInstance = ReadObject(childNode, errors);
                        childList.Add(childInstance);
                    }
                }
                else if (childNode.Name.Equals("Property"))
                {

                    ReadProperty(childNode, instance, errors);
                }
                else if (childNode.Name.Equals("Event"))
                {
                    ReadEvent(childNode, instance, errors);
                }
            }

            return instance;
        }

        private void ReadProperty(
            XmlNode node,
            object instance,
            ArrayList errors)
        {
            XmlAttribute nameAttr = node.Attributes["name"];
            if (nameAttr == null)
            {
                errors.Add(Utility.GetString("PropertyHasNoName", "属性没名称."));
                return;
            }

            PropertyDescriptor prop = TypeDescriptor.GetProperties(instance)[nameAttr.Value];
            if (prop == null)
            {
                errors.Add(Utility.GetString("PropertyDoesNotExist", "属性{0}不存在类型{1}中。", nameAttr.Value, instance.GetType().FullName));
                return;
            }

            bool isContent = prop.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
            if (isContent)
            {
                object value = prop.GetValue(instance);
                if (value is IList)
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name.Equals("Item"))
                        {
                            object item;
                            XmlAttribute typeAttr = child.Attributes["type"];
                            if (typeAttr == null)
                            {
                                errors.Add(Utility.GetString("ItemHasNoTypeAttribute", "Item不存在[Type]特性."));
                                continue;
                            }

                            Type type = Type.GetType(typeAttr.Value);
                            if (type == null)
                            {
                                errors.Add(Utility.GetString("ItemTypeCouldNotBeFound", "Item类型{0}无法找到。", typeAttr.Value));
                                continue;
                            }

                            if (ReadValue(child, TypeDescriptor.GetConverter(type), errors, out item))
                            {
                                try
                                {
                                    ((IList)value).Add(item);
                                }
                                catch (Exception ex)
                                {
                                    errors.Add(ex.Message);
                                }
                            }
                        }
                        else
                        {
                            errors.Add(Utility.GetString("OnlyAllowItemElements", "只有Item元素是允许在集合，而不是{0}的内容.", child.Name));
                        }
                    }
                }
                else
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name.Equals("Property"))
                        {
                            ReadProperty(child, value, errors);
                        }
                        else
                        {
                            errors.Add(Utility.GetString("OnlyAllowPropertyElements", "Only Property elements are allowed in content properties, not {0} elements.", child.Name));
                        }
                    }
                }
            }
            else
            {
                object value;
                if (ReadValue(
                    node,
                    prop.Converter,
                    errors,
                    out value))
                {
                    try
                    {
                        prop.SetValue(instance, value);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex.Message);
                    }
                }
            }
        }


        private bool ReadValue(
            XmlNode node,
            TypeConverter converter,
            ArrayList errors,
            out object value)
        {
            try
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.Text)
                    {
                        value = converter.ConvertFromInvariantString(node.InnerText);
                        return true;
                    }
                    else if (child.Name.Equals("Binary"))
                    {
                        byte[] data = Convert.FromBase64String(child.InnerText);
                        if (GetConversionSupported(converter, typeof(byte[])))
                        {
                            value = converter.ConvertFrom(null, CultureInfo.InvariantCulture, data);
                            return true;
                        }
                        else
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            MemoryStream stream = new MemoryStream(data);

                            value = formatter.Deserialize(stream);
                            return true;
                        }
                    }
                    else if (child.Name.Equals("InstanceDescriptor"))
                    {
                        value = ReadInstanceDescriptor(child, errors);
                        return (value != null);
                    }
                    else if (child.Name.Equals("Object"))
                    {
                        //添加对于列表对象时候反序列化 
                        value = ReadObject(child, errors);
                        return (value != null);
                    }
                    else
                    {
                        errors.Add(Utility.GetString("UnexpectedElementType", "Unexpected element type {0}", child.Name));
                        value = null;
                        return false;
                    }
                }

                value = null;
                return true;
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
                value = null;
                return false;
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 获取表单文件生成的XML
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            PerformFlushWorker();

            StringWriter sw;
            sw = new StringWriter();
            XmlTextWriter xtw = new XmlTextWriter(sw);
            xtw.Formatting = Formatting.Indented;
            xmlDocument.WriteTo(xtw);

            string cleanup = sw.ToString().Replace("<DOCUMENT_ELEMENT>", "");
            cleanup = cleanup.Replace("</DOCUMENT_ELEMENT>", "");
            sw.Close();
            return cleanup;
        }

        public List<string> Validate()
        {
            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));
            IComponent rt = idh.RootComponent;
            if (rt == null)
            {
                return new List<string>();
            }

            LWBaseForm form = rt as LWBaseForm;
            if (form != null)
            {
                return form.ValidateForDesign();
            }
  
            return new List<string>();
      
        }

        /// <summary>
        /// 直接保存
        /// </summary>
        public void Save()
        {
            Save(false);
        }

        /// <summary>
        /// 保存表单是否给出提示
        /// </summary>
        /// <param name="forceFilePrompt"></param>
        public void Save(bool forceFilePrompt)
        {
            //try
            //{
                PerformFlushWorker();

                if ((fileName == null)
                    || forceFilePrompt)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.InitialDirectory = DefaultConfigMananger.Default.FormDesignFolder.Path;
                    dlg.DefaultExt = "xml";
                    dlg.Filter = "xml files|*.xml";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        fileName = dlg.FileName;
                    }
                }

                if (!string.IsNullOrEmpty(fileName))
                {
                    StringWriter sw = new StringWriter();
                    XmlTextWriter xtw = new XmlTextWriter(sw);
                    xtw.Formatting = Formatting.Indented;

                    xmlDocument.WriteTo(xtw);

                    string cleanup = sw.ToString().Replace("<DOCUMENT_ELEMENT>", "");
                    cleanup = cleanup.Replace("</DOCUMENT_ELEMENT>", "");
                    xtw.Close();

                    StreamWriter file = new StreamWriter(fileName);
                    file.Write(cleanup);
                    file.Close();

                    //更新对应的数据源
                    SaveDataSchema(fileName.Replace(".xml", ".xsd"));
                }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        /*保存表单对应的数据源*/
        private void SaveDataSchema(string fileName)
        {
            IDesignerHost idh = (IDesignerHost)this.LoaderHost.GetService(typeof(IDesignerHost));
            IComponent rt = idh.RootComponent;
            if (rt == null)
            {
                return;
            }

            LWBaseForm form = rt as LWBaseForm;
            if (form != null)
            {
                System.Data.DataSet ds = form.BuildDataSet(form.Name);
                if (ds != null)
                {
                    ds.WriteXmlSchema(fileName);
                }
            }
        }

        private string fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        #endregion
    }
}

