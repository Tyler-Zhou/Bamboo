using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1.File;
using System.Reflection;
using ICP.Framework.CommonLibrary.Common;
using ICP.DataCache.ServiceInterface1;
using ICP.DataCache.BusinessOperation1.Preview;
using Microsoft.Practices.CompositeUI;

namespace ICP.DataCache.BusinessOperation1
{
   public class ConverterPreviewFactory:IDisposable
    { 
       [ServiceDependency]
       public WorkItem RootWorkItem { get; set; }
        
       private static Dictionary<List<String>, Type> converters = new Dictionary<List<String>, Type>(new ListEqualityComparer());
       private static Dictionary<List<String>, Type> previews = new Dictionary<List<String>, Type>(new ListEqualityComparer());
        public  IFileConverter GetFileConverter(String filePath)
        {
            Type converter = GetItem(converters,typeof(BaseFileConverter),filePath);
            if (converter != null)
            {
                return (IFileConverter)RootWorkItem.Items.AddNew(converter);
            }

            else
            {
                String message = String.Format(ApplicationContext.Current.IsEnglish ? "Failed to find file converter for:{0}" : "为:{0}查找文件转换器失败.", filePath);
                throw new NotImplementedException(message);
            }
        }
        private Type GetItem(Dictionary<List<String>, Type> collection, Type baseType, String filePath)
        {
            if (collection.Count <= 0)
            {
                InitCollection(collection,baseType);
            }

            String extension = System.IO.Path.GetExtension(filePath).ToLower();
            var targets = from item in collection
                            where item.Key.Contains(extension)
                            select item.Value;
            if (targets != null && targets.Count() > 0)
                return targets.First();
            else
            {
                return null;
            }
        }
        public  IFilePreview GetFilePreviewer(String filePath)
        {
            Type previewer = GetItem(previews, typeof(IFilePreview), filePath);
            if (previewer != null)
            {
                return (IFilePreview)RootWorkItem.Items.AddNew(previewer);
            }
            else
            {
                String message = String.Format(ApplicationContext.Current.IsEnglish ? "Failed to find file previewer for:{0}" : "为:{0}查找文件预览器失败.", filePath);
                throw new NotImplementedException(message);
            }
        }
        public Byte[] GetConvertedContent(String filePath)
        {
            IFileConverter converter = GetFileConverter(filePath);
            converter.Convert(filePath);
            return DataCacheUtility.ReadFileContentFromDisk(converter.FileNewPath);
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
            if (previews != null)
            {
                previews.Clear();
                previews = null;
            }
        }

        #endregion
    }
}
