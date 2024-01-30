using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FCM.Common.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// Business映射保存对象
    /// </summary>
    [Serializable]
    public class SaveRequestBusinessMapping : SaveRequest
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 映射ID
        /// </summary>
        public int MapID { get; set; }
        /// <summary>
        /// 映射类型
        /// </summary>
        public string MapType { get; set; }
        /// <summary>
        /// 是否移除
        /// </summary>
        public bool IsRemove { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
