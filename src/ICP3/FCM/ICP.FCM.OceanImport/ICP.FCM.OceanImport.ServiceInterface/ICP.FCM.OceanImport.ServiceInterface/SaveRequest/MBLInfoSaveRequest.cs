using System;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    [Serializable]
    public class MBLInfoSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest 
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID;
        /// <summary>
        /// 业务类型ID
        /// </summary>
        public FCMOperationType OperationTypeID;
        /// <summary>
        /// 主提单号
        /// </summary>
        public string MBLNo;
        /// <summary>
        /// 子提单号
        /// </summary>
        public string SubNo;
        /// <summary>
        /// 船公司
        /// </summary>
        public Guid? CarrierID;
        /// <summary>
        /// 承运人
        /// </summary>
        public Guid? AgentOfCarrierID;
        /// <summary>
        /// 大船ID
        /// </summary>
        public Guid VesselID;
        /// <summary>
        /// 驳船ID
        /// </summary>
        public Guid? PreVoyageID;
        /// <summary>
        /// 提货地ID
        /// </summary>
        public Guid? FinalWareHouseID;
        /// <summary>
        /// 还柜地
        /// </summary>
        public Guid? ReturnLocationID;
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

        public DateTime? ETD;

        public DateTime? ETA;
        public DateTime? GateInDate;

    }
}
