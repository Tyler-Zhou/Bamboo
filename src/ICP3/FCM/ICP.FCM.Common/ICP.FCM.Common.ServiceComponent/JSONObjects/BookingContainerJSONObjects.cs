using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    /// <summary>
    /// 舱单柜信息
    /// </summary>
    [Serializable]
    public class BookingContainerForCSPAPI
    {
        /// <summary>
        /// 柜型
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 柜量
        /// </summary>
        public string Value { get; set; }
    }
}
