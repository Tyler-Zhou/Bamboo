using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirImport.ServiceInterface
{
    /// <summary>
    /// HBL信息保存实体
    /// </summary>
    [Serializable]
    public class HBLInfoSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest 
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid?[] IDs;
        /// <summary>
        /// 提单号
        /// </summary>
        public string[] BLNos;
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OIBookingID;
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid?[] ShipperIDs;
        /// <summary>
        /// 发货人描述
        /// </summary>
        public string[] ShipperDescriptions;
        /// <summary>
        /// 发货人描述
        /// </summary>
        public string[] StrShipperDescriptions;
       
        /// <summary>
        /// 收到正本提单时间
        /// </summary>
        public DateTime?[] ReceiveOBLDates;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] UpdateDates;
        /// <summary>
        /// 是否英文环境 
        /// </summary>
        public bool IsEnglish;
    }
}
