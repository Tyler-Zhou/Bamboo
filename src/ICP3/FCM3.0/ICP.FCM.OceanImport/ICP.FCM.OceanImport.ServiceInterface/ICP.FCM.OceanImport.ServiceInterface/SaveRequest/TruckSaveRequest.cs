using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.ServiceInterface
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
        /// 
        /// </summary>
        public CustomerDescription TruckerDescription;

        /// <summary>
        /// 提货地点ID（关联客户）
        /// </summary>
        public Guid? PickUpAtID;

        /// <summary>
        /// 
        /// </summary>
        public CustomerDescription PickUpAtDescription;

        /// <summary>
        /// 提货日期
        /// </summary>
        public DateTime? PickUpDate;

        /// <summary>
        /// 交货地点ID（关联客户）
        /// </summary>
        public Guid? DeliveryAtID;

        /// <summary>
        /// 
        /// </summary>
        public CustomerDescription DeliveryAtDescription;

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
        /// 
        /// </summary>
        public CustomerDescription BillToDescription;

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

        /// <summary>
        /// 关联的集装箱ID
        /// </summary>
        public List<Guid?> ContainerIDList;
    }
}
