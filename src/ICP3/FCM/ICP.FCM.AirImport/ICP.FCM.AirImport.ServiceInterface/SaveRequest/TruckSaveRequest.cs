using System;

namespace ICP.FCM.AirImport.ServiceInterface
{
    /// <summary>
    /// 用于保存拖车
    /// </summary>
    [Serializable]
    public class TruckSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest 
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID;

        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OIBookingID;

        /// <summary>
        /// 派车单号 
        /// </summary>
        public string NO;

        /// <summary>
        /// 拖车公司ID
        /// </summary>
        public Guid TruckerID;

        /// <summary>
        /// 提货地点ID（关联客户）
        /// </summary>
        public Guid? PickUpAtID;

        /// <summary>
        /// 提货日期
        /// </summary>
        public DateTime? PickUpDate;

        /// <summary>
        /// 交货地点ID（关联客户）
        /// </summary>
        public Guid? DeliveryAtID;

        /// <summary>
        /// 交货时间
        /// </summary>
        public DateTime? DeliveryDate;

        /// <summary>
        /// 是否需要司机本
        /// </summary>
        public bool IsDrivingLicence;

        /// <summary>
        /// 账单寄送ID
        /// </summary>
        public Guid BillToID;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;

        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity;

        /// <summary>
        /// 发送提货单日
        /// </summary>
        public DateTime? PickUpSendDate;

        /// <summary>
        /// 提货参考号
        /// </summary>
        public string PickUpRefNo;

        /// <summary>
        /// 提货联系人
        /// </summary>
        public string PickUpContact;

        /// <summary>
        /// 送货参考号
        /// </summary>
        public string DeliveryNo;

        /// <summary>
        /// 送货联系人
        /// </summary>
        public string DeliveryContact;

        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate;

        /// <summary>
        /// 是否英文环境 
        /// </summary>
        public bool IsEnglish;      
    }
}
