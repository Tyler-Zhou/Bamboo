using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ICP.Test
{
    /// <summary>
    /// 方法树列表
    /// </summary>
    [Serializable]
    public class TestMethodList
    {
        public Guid ID { get; set; }
        public int Type { get; set; }
        public string MethodName { get; set; }
        public string MethodDescription { get; set; }
        public Guid ParentID { get; set; }
    }
    public class TestDataList
    {
        /// <summary>
        /// 方法名
        /// </summary>
        public Guid MethodID { get; set; }
        /// <summary>
        /// 数据集
        /// </summary>
        public DataSet ds { get; set; }
    }
}
