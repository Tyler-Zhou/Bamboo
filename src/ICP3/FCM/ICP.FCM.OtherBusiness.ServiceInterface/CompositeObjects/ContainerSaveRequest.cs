using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 用于保存箱的对象
    /// </summary>
    [Serializable]
    public class ContainerSaveRequest : SaveRequest
    { 
        /// <summary>
        /// 托运ID
        /// </summary>
        public Guid OtherBookingID;
        /// <summary>
        /// 订单口岸ID
        /// </summary>
        public Guid companyID;
        /// <summary>
        /// 箱号列表
        /// </summary>
        public string[] Nos;
        /// <summary>
        /// 箱ID列表箱ID列表
        /// </summary>
        public Guid[] IDs;
        /// <summary>
        /// SONOS
        /// </summary>
        public string[] SONOS;
        /// <summary>
        /// 箱型列表
        /// </summary>
        public Guid?[] TypeIDs;
        /// <summary>
        /// 封条号列表
        /// </summary>
        public string[] SealNos;
        /// <summary>
        /// 品名
        /// </summary>
        public string[] Commoditys;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal?[] Quantitys;
        /// <summary>
        /// 数量单位
        /// </summary>
        public Guid?[] QuantityUnitIDs;
        /// <summary>
        /// 重量
        /// </summary>
        public decimal?[] Weights;
        /// <summary>
        /// 重量单位
        /// </summary>
        public Guid?[] WeightUnitIDs;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal?[] Measurements;
        /// <summary>
        /// 体积单位
        /// </summary>
        public Guid?[] MeasurementUnitIDs;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] UpdateDates;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid? SaveByID;
        /// <summary>
        /// 是否英文
        /// </summary>
        public bool isEnglish;
    }
}
