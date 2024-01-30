using ICP.FCM.Common.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// CSP运单联系人
    /// </summary>
    [Serializable]
    public class ShipmentContactForCSPAPI
    {
        /// <summary>
        /// 服务商用户Id
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// 联系人用户ID
        /// </summary>
        public Guid ContactUserID { get; set; }
        /// <summary>
        /// 服务性质类型
        /// </summary>
        public CSP_CONTACTTYPE serviceUserType { get; set; }
    }
}
