﻿using System;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FCM.AirExport.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 用于保存费用的对象
    /// </summary>
    [Serializable]
    public class FeeSaveRequest : SaveRequest
    {
        /// <summary>
        /// 订单
        /// </summary>
        public Guid orderID;
        /// <summary>
        /// 订单口岸ID
        /// </summary>
        public Guid companyID;
        /// <summary>
        /// ID列表
        /// </summary>
        public Guid?[] ids;
        /// <summary>
        /// 客户ID列表
        /// </summary>
        public Guid[] customerIDs;
        /// <summary>
        /// 费用代码
        /// </summary>
        public Guid[] chargingCodeIDs;
        /// <summary>
        /// 币种列表
        /// </summary>
        public Guid[] currencyIDs;
        /// <summary>
        /// 数量列表
        /// </summary>
        public decimal[] quantities;
        /// <summary>
        /// 单价列表
        /// </summary>
        public decimal[] unitPrices;
        /// <summary>
        /// 方向列表(DR-0-应收 ,CR-1-应付
        /// </summary>
        public FeeWay[] ways;
        /// <summary>
        /// 金额列表
        /// </summary>
        public decimal[] amounts;
        /// <summary>
        /// 备注列表
        /// </summary>
        public string[] remarks;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] updateDates;
    }
}
