using System;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.AirImport.ServiceInterface
{
    [Serializable]
    public class MBLInfoSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID;

        /// <summary>
        /// 主提单号
        /// </summary>
        public string MBLNo;
        /// <summary>
        /// 子提单号
        /// </summary>
        public string SubNo;
        /// <summary>
        /// 航空公司
        /// </summary>
        public Guid? AirCompanyID;
        /// <summary>
        /// 承运人
        /// </summary>
        public Guid? AgentOfCarrierID;

        /// <summary>
        /// FlightFlag
        /// </summary>
        public string FlightFlag;
        /// <summary>
        /// FlightCountry
        /// </summary>
        public string FlightCountry;

        /// <summary>
        /// Manifest NO
        /// </summary>
        public string ManifestNO;
        /// <summary>
        /// G.O. Date
        /// </summary>
        public DateTime? GODate;

        public DateTime? ETD;
        public DateTime? ETA;

        /// <summary>
        /// 航班ID
        /// </summary>
        public Guid? FlightID;

        /// <summary>
        /// 提货地ID
        /// </summary>
        public Guid? FinalWareHouseID;

        /// <summary>
        /// 转关单号
        /// </summary>
        public string ITNO;
        /// <summary>
        /// 转关日期
        /// </summary>
        public DateTime? ITDate;
        /// <summary>
        /// 转关地点
        /// </summary>
        public string ITPalce;
        /// <summary>
        /// 放单类型
        /// </summary>
        public FCMReleaseType ReleaseType;
        /// <summary>
        /// MBL运输条款
        /// </summary>
        public Guid? MBLTransportClauseID;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDates;
        /// <summary>
        /// 是否英文环境 
        /// </summary>
        public bool IsEnglish;
    }
}
