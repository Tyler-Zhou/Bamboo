using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;

namespace ICP.FCM.Platform.ServiceInterface
{
    /// <summary>
    /// CSP订单信息(CSP Booking Info)
    /// </summary>
    [Serializable]
    public class CSPBookingInfo
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 运输方式
        /// </summary>
        public int freightMethodType { get; set; }
        /// <summary>
        /// 始发装载时间
        /// </summary>
        public string cargoReadyDate { get; set; }
        /// <summary>
        /// 货物详情
        /// </summary>
        public string cargoDetails { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 是否是当前登录公司
        /// </summary>
        public string isCurrentTenant { get; set; }
        /// <summary>
        /// 发货方名称
        /// </summary>
        public bool shipperName { get; set; }
        /// <summary>
        /// 发货方地址
        /// </summary>
        public bool shipperAddress { get; set; }
        /// <summary>
        /// 收货人名称
        /// </summary>
        public bool consigneeName { get; set; }
        /// <summary>
        /// 收货人地址
        /// </summary>
        public bool consigneeAddress { get; set; }
        /// <summary>
        /// 重量单位
        /// </summary>
        public bool totalWeightUnitStr { get; set; }
        /// <summary>
        /// 体积条件
        /// </summary>
        public bool totalVolumeUnitStr { get; set; }
    }

    /// <summary>
    /// CSP订单信息(CSP Booking Info)
    /// </summary>
    [Serializable]
    public class CSPBookingItem : PlatformResponseBaseList<List<CSPBookingInfo>>
    {

    }
}
