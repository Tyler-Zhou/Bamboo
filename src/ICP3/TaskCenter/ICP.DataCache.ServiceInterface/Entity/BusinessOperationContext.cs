using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System.Xml.Serialization;
using ICP.DataCache.ServiceInterface1;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.DataCache.ServiceInterface1
{
    /// <summary>
    /// 业务操作上下文类
    /// </summary>
    [Serializable]
    public sealed class BusinessOperationContext //: IDictionary<object, BusinessOperationContext>
    {   
        [XmlIgnore]
        private static BusinessOperationContext context;
        [XmlIgnore]
        public static BusinessOperationContext Current
        {
            get {
                 
             return context;
            }
        }
        static BusinessOperationContext()
        {
          if(context==null)
              context=new BusinessOperationContext();
        }
        
        
        
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public string OpeationNO { get; set; }

        /// <summary>
        /// 代理ID
        /// </summary>
        public String SONO { get; set; }

        /// <summary>
        /// 文档名集合
        /// </summary>
        public ListDictionary<Guid, string> FilesName { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.OperationType OperationType
        {
            get;
            set;
        }



        /// <summary>
        /// 代理文档状态
        /// </summary>
        public DocumentState? State
        {

            get;
            set;
        }

        /// <summary>
        /// 表单类型
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.FormType FormType { get; set; }
        public Guid FormId { get; set; }
        
        [XmlIgnore]
        private static Dictionary<object, BusinessOperationContext> contextCache = new Dictionary<object, BusinessOperationContext>();
        public void Add(object key)
        {
            Add(key, this);
        }
  
        #region IDictionary<object,BusinessOperationContext> 成员

        public void Add(object key, BusinessOperationContext value)
        {
            contextCache.Add(key, value);
        }

        public bool ContainsKey(object key)
        {
            return contextCache.ContainsKey(key);
        }

        public ICollection<object> Keys
        {
            get { return contextCache.Keys; }
        }

        public bool Remove(object key)
        {
            return contextCache.Remove(key);
        }

        public bool TryGetValue(object key, out BusinessOperationContext value)
        {
            return contextCache.TryGetValue(key, out value);
        }

        public ICollection<BusinessOperationContext> Values
        {
            get { return contextCache.Values; }
        }

        public BusinessOperationContext this[object key]
        {
            get
            {
                return contextCache[key];
            }
            set
            {
                contextCache[key] = value;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<object,BusinessOperationContext>> 成员

        public void Add(KeyValuePair<object, BusinessOperationContext> item)
        {
            contextCache.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            contextCache.Clear();
        }

        public bool Contains(KeyValuePair<object, BusinessOperationContext> item)
        {
            return contextCache.Contains(item);
        }

        public void CopyTo(KeyValuePair<object, BusinessOperationContext>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return contextCache.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<object, BusinessOperationContext> item)
        {
            return contextCache.Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<object,BusinessOperationContext>> 成员

        public IEnumerator<KeyValuePair<object, BusinessOperationContext>> GetEnumerator()
        {
            return contextCache.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        //System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        //{
        //    return contextCache.ToList().GetEnumerator();
        //}

        #endregion
    }
}
