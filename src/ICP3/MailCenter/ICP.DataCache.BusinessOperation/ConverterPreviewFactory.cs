using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Common.ServiceInterface;
using ICP.DataCache.ServiceInterface.File;
using Microsoft.Practices.CompositeUI;

namespace ICP.DataCache.BusinessOperation
{
    public class ConverterPreviewFactory : IDisposable
    {
        [ServiceDependency]
        public WorkItem RootWorkItem { get; set; }

        private static Dictionary<List<String>, Type> converters = new Dictionary<List<String>, Type>(new ListEqualityComparer());
        private Dictionary<List<string>, IFileConverter> instances = new Dictionary<List<string>, IFileConverter>();
        private object synObj = new object();
        public IFileConverter GetFileConverter(String filePath)
        {
            lock (synObj)
            {
                IFileConverter fileConverter = null;
                String extension = System.IO.Path.GetExtension(filePath).ToLower();
                var targets = from item in instances
                              where item.Key.Contains(extension)
                              select item.Value;
                if (targets != null && targets.Count() > 0)
                {
                    fileConverter = targets.First();
                    return fileConverter;
                }
                var converter = GetItem(converters, typeof(BaseFileConverter), filePath);
                fileConverter = (IFileConverter)RootWorkItem.Items.AddNew(converter.Value);
                instances.Add(converter.Key, fileConverter);
                return fileConverter;
            }
        }
        private KeyValuePair<List<string>, Type> GetItem(Dictionary<List<String>, Type> collection, Type baseType, String filePath)
        {
            if (collection.Count <= 0)
            {
                InitCollection(collection, baseType);
            }

            String extension = System.IO.Path.GetExtension(filePath).ToLower();
            var targets = from item in collection
                          where item.Key.Contains(extension)
                          select item;

            return targets.First();

        }

        public Byte[] GetConvertedContent(String filePath)
        {
            if (filePath.ToLower().EndsWith(".pdf"))
                return IOHelper.ReadFileContentFromDisk(filePath);
            IFileConverter converter = GetFileConverter(filePath);
            converter.Convert(filePath);
            return IOHelper.ReadFileContentFromDisk(converter.FileNewPath);
        }

        private static void InitCollection(Dictionary<List<String>, Type> collection, Type baseType)
        {
            Type[] types = baseType.Assembly.GetExportedTypes().Where(type => baseType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract).ToArray();
            Array.ForEach(types, type =>
            {

                object instance = Activator.CreateInstance(type);
                collection.Add(((List<String>)type.GetProperty("FileExtensions").GetValue(instance, null)), type);
                instance = null;
            });
            Type mailConvertType = Type.GetType("ICP.MailCenter.UI.MailConverter,ICP.MailCenter.UI");
            collection.Add(new List<String> { ".msg" }, mailConvertType);
        }
        /// <summary>
        /// List<String>比较实现类
        /// </summary>
        public sealed class ListEqualityComparer : IEqualityComparer<List<String>>
        {

            #region IEqualityComparer<List<String>> 成员

            public bool Equals(List<String> list1, List<String> list2)
            {
                if (list1 == null && list2 == null)
                    return true;
                if (list1 == null || list2 == null)
                    return false;
                if (list1.Count != list2.Count)
                    return false;
                int count = list1.Count;
                for (int i = 0; i < count; i++)
                {
                    if (!list1[i].Equals(list2[i]))
                        return false;
                }
                return true;
            }

            public int GetHashCode(List<String> obj)
            {
                return obj.GetHashCode();
            }

            #endregion
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (converters != null)
            {
                converters.Clear();
                converters = null;
            }
            this.RootWorkItem = null;

        }

        #endregion
    }
}
