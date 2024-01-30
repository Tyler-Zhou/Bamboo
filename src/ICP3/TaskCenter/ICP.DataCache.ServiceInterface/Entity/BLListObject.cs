using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.DataCache.ServiceInterface1
{
    /// <summary>
    /// 提单列表数据源
    /// </summary>    
    [Serializable]
    public class BLList
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 表单类型
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.FormType FormType { get; set; }
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public string Shipper { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public string Noticer { get; set; }
    }
}
