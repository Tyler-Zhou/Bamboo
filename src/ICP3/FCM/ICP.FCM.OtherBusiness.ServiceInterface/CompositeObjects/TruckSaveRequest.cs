using System;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 用于保存拖车
    /// </summary>
    [Serializable]
    public class TruckSaveRequest : SaveRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? id;
        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid oceanBookingID;
        /// <summary>
        /// 拖车公司
        /// </summary>
        public Guid truckerID;
        /// <summary>
        /// 拖车号
        /// </summary>
        public string no;
        /// <summary>
        /// 订舱号
        /// </summary>
        public string sono;
        /// <summary>
        /// 拖车公司描述
        /// </summary>
        public CustomerDescription truckerDescription;
        /// <summary>
        /// 提柜地点
        /// </summary>
        public Guid? pickUpAtID;
        /// <summary>
        /// 提柜地点描述
        /// </summary>
        public CustomerDescription pickUpAtDescription;
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid shipperID;
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription shipperDescription;

        /// <summary>
        /// 帐单寄送
        /// </summary>
        public Guid? BillToID;
        /// <summary>
        /// 帐单寄送描述
        /// </summary>
        public CustomerDescription BillToDescription;

        public string Commodity;

        public DateTime? DeliveryDate;

        /// <summary>
        /// 装货时间
        /// </summary>
        public DateTime? loadTime;
        /// <summary>
        /// 还柜地点
        /// </summary>
        public Guid? deliveryAtID;
        /// <summary>
        /// 还柜地点描述
        /// </summary>
        public CustomerDescription deliveryAtDescription;
        /// <summary>
        /// 是否需要司机本
        /// </summary>
        public bool isDrivingLicence;
        /// <summary>
        /// 报关公司
        /// </summary>
        public Guid? customsBrokerID;
        /// <summary>
        /// 报关公司描述
        /// </summary>
        public CustomerDescription customsBrokerDescription;
        /// <summary>
        /// 箱需求描述
        /// </summary>
        public ContainerDescription containerDescription;
        /// <summary>
        /// 费用描述
        /// </summary>
        public string feeDescription;
        /// <summary>
        /// 备注
        /// </summary>
        public string remark;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? updateDate;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
    }
}
