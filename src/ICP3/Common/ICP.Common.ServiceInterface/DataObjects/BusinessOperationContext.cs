////#region Comment
/////*
//// * Create By:Taylor Zhou
//// * Create On:2018/7/19 星期四 15:51:49
//// *
//// * Description:
//// *         ->
//// *
//// * History:
//// *         ->
//// */
////#endregion

////using ICP.Framework.CommonLibrary.Common;
////using Microsoft.Practices.CompositeUI.Utility;
////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Xml.Serialization;

////namespace ICP.Common.ServiceInterface
////{
////    /// <summary>
////    /// 业务操作上下文类
////    /// </summary>
////    [Serializable]
////    public sealed class BusinessOperationContext
////    {
////        [XmlIgnore]
////        private static BusinessOperationContext context;
////        /// <summary>
////        /// 
////        /// </summary>
////        [XmlIgnore]
////        public static BusinessOperationContext Current
////        {
////            get
////            {
////                return context;
////            }
////        }
////        static BusinessOperationContext()
////        {
////            if (context == null)
////                context = new BusinessOperationContext();
////        }

////        /// <summary>
////        /// 业务ID
////        /// </summary>
////        public Guid OperationID { get; set; }

////        /// <summary>
////        /// 客户ID
////        /// </summary>
////        public Guid CustomerID { get; set; }

////        /// <summary>
////        /// 上级操作ID，适用于树形网格
////        /// </summary>
////        public Guid? ParentOperationID { get; set; }

////        /// <summary>
////        /// 业务号
////        /// </summary>
////        public string OperationNO { get; set; }

////        /// <summary>
////        /// 代理ID
////        /// </summary>
////        public string SONO { get; set; }

////        /// <summary>
////        /// 文档名集合
////        /// </summary>
////        [XmlIgnore]
////        public ListDictionary<Guid, string> FilesName { get; set; }

////        /// <summary>
////        /// 业务类型
////        /// </summary>
////        public OperationType OperationType
////        {
////            get;
////            set;
////        }
////        /// <summary>
////        /// 业务更新时间
////        /// </summary>
////        public DateTime? UpdateDate
////        {
////            get;
////            set;
////        }

////        /// <summary>
////        /// 表单类型
////        /// </summary>
////        public FormType FormType { get; set; }
////        /// <summary>
////        /// 
////        /// </summary>
////        public Guid FormId { get; set; }

////        [XmlIgnore]
////        private static Dictionary<string, object> contextCache = new Dictionary<string, object>();
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="key"></param>
////        public void Add(string key)
////        {
////            Add(key, this);
////        }

////        #region IDictionary<object,BusinessOperationContext> 成员
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="key"></param>
////        /// <param name="value"></param>
////        public void Add(string key, object value)
////        {
////            //加判断避免重复的KEY值加入后报错
////            if (contextCache.ContainsKey(key) == false)
////            {
////                contextCache.Add(key, value);
////            }
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="key"></param>
////        /// <returns></returns>
////        public bool ContainsKey(string key)
////        {
////            return contextCache.ContainsKey(key);
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        public ICollection<string> Keys
////        {
////            get { return contextCache.Keys; }
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="key"></param>
////        /// <returns></returns>
////        public bool Remove(string key)
////        {
////            return contextCache.Remove(key);
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="key"></param>
////        /// <param name="value"></param>
////        /// <returns></returns>
////        public bool TryGetValue(string key, out object value)
////        {
////            return contextCache.TryGetValue(key, out value);
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        public ICollection<object> Values
////        {
////            get { return contextCache.Values; }
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="key"></param>
////        /// <returns></returns>
////        public object this[string key]
////        {
////            get
////            {
////                if (ContainsKey(key))
////                    return contextCache[key];
////                return null;
////            }
////            set
////            {
////                contextCache[key] = value;
////            }
////        }

////        #endregion

////        #region ICollection<KeyValuePair<object,BusinessOperationContext>> 成员
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="item"></param>
////        public void Add(KeyValuePair<string, object> item)
////        {
////            contextCache.Add(item.Key, item.Value);
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        public void Clear()
////        {
////            contextCache.Clear();
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="item"></param>
////        /// <returns></returns>
////        public bool Contains(KeyValuePair<string, object> item)
////        {
////            return contextCache.Contains(item);
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="array"></param>
////        /// <param name="arrayIndex"></param>
////        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
////        {
////            throw new NotImplementedException();
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        public int Count
////        {
////            get { return contextCache.Count; }
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        public bool IsReadOnly
////        {
////            get { return false; }
////        }
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <param name="item"></param>
////        /// <returns></returns>
////        public bool Remove(KeyValuePair<string, object> item)
////        {
////            return contextCache.Remove(item.Key);
////        }

////        #endregion

////        #region IEnumerable<KeyValuePair<object,BusinessOperationContext>> 成员
////        /// <summary>
////        /// 
////        /// </summary>
////        /// <returns></returns>
////        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
////        {
////            return contextCache.GetEnumerator();
////        }

////        #endregion

////        #region IEnumerable 成员

////        #endregion
////    }
////}
