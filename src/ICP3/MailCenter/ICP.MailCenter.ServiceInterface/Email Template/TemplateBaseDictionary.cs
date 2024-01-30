using System;
using System.Collections.Generic;
using System.Linq;

namespace ICP.MailCenter.ServiceInterface
{  
    /// <summary>
    /// Email模板文件信息基类
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class TemplateBaseDictionary<K, V> : IDictionary<K, V>, IDisposable where V : class
    {
        private Dictionary<K, V> metaStorage = new Dictionary<K, V>();
        private object synObj = new object();
        #region IDictionary<K,V> 成员

        public void Add(K key, V value)
        {
            try
            {
                metaStorage.Add(key, value);
            }
            catch (Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                throw new Exception(string.Format("Add key [{0}] exception occurred:{1}", key,ex.Message));
            }
        }

        public bool ContainsKey(K key)
        {
           return metaStorage.ContainsKey(key);
        }

        public ICollection<K> Keys
        {
            get { return metaStorage.Keys; }
        }

        public bool Remove(K key)
        {
           return metaStorage.Remove(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            return metaStorage.TryGetValue(key,out value);
        }

        public ICollection<V> Values
        {
            get { return metaStorage.Values ; }
        }

        public V this[K key]
        {
            get
            {
             
                    EnsureExists(key);
                    if (!ContainsKey(key))
                    {
                        return default(V);
                    }
                    return metaStorage[key];
                
            }
            set
            {
                metaStorage[key]=value;
            }
        }
        /// <summary>
        /// 在访问成员时,确保成员存在
        /// </summary>
        /// <param name="key"></param>
        public virtual void EnsureExists(K key)
        {

        }
        
        #endregion

        #region ICollection<KeyValuePair<K,V>> 成员

        public void Add(KeyValuePair<K, V> item)
        {
            metaStorage.Add(item.Key,item.Value);
        }

        public void Clear()
        {
            metaStorage.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
           return metaStorage.Contains<KeyValuePair<K,V>>(item);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
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

        public bool Remove(KeyValuePair<K, V> item)
        {
            return Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<K,V>> 成员

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
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
