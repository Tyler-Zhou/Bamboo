using System;
using System.Collections.Generic;
using System.Linq;

namespace ICP.DataCache.FileSystem
{  
    /// <summary>
    /// 文件缓存上下文类
    /// </summary>
    public class FileStorageMetaDataContext : IDictionary<string, FileStorageMetaData>,IDisposable
    {
        private static Dictionary<string, FileStorageMetaData> metaStorage = new Dictionary<string, FileStorageMetaData>();
        private FileStorageMetaDataContext() { }
        private static FileStorageMetaDataContext context;
        private static object synObj = new object();
        /// <summary>
        /// 返回唯一实例
        /// </summary>
        public static FileStorageMetaDataContext Current
        {
            get
            {
                if (context == null)
                {
                    lock (synObj)
                    {
                        if (context == null)
                            context = new FileStorageMetaDataContext();
                    }
                }
                return context;
            }
        }
        #region IDictionary<string,FileStorageMetaData> 成员

        public void Add(string key, FileStorageMetaData value)
        {
            metaStorage.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return metaStorage.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return metaStorage.Keys; }
        }

        public bool Remove(string key)
        {
            return metaStorage.Remove(key);
        }

        public bool TryGetValue(string key, out FileStorageMetaData value)
        {
            return metaStorage.TryGetValue(key, out value);
        }

        public ICollection<FileStorageMetaData> Values
        {
            get { return metaStorage.Values; }
        }
        /// <summary>
        /// 返回对应缓存路径的文件缓存信息维护类，如果不存在，则实例化后返回
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FileStorageMetaData this[string key]
        {
            get
            {
                if (!metaStorage.ContainsKey(key))
                {
                    FileStorageMetaData metaData = new FileStorageMetaData();
                    metaData.FilePath = key;
                    metaData.Init();
                    metaStorage[key] = metaData;
                }
                return metaStorage[key];
            }
            set
            {
                metaStorage[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<string,FileStorageMetaData>> 成员

        public void Add(KeyValuePair<string, FileStorageMetaData> item)
        {
            metaStorage.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            metaStorage.Clear();
        }

        public bool Contains(KeyValuePair<string, FileStorageMetaData> item)
        {
            return metaStorage.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, FileStorageMetaData>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return metaStorage.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, FileStorageMetaData> item)
        {
          return  metaStorage.Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,FileStorageMetaData>> 成员

        public IEnumerator<KeyValuePair<string, FileStorageMetaData>> GetEnumerator()
        {
            return metaStorage.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return metaStorage.GetEnumerator();
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (metaStorage != null)
            {
                metaStorage.Clear();
                metaStorage = null;
            }
        }

        #endregion
    }
}
