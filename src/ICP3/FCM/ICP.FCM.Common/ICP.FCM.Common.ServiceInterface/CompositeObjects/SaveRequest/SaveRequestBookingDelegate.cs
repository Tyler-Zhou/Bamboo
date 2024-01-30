using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface.CompositeObjects
{
    #region 舱单委托保存
    /// <summary>
    /// 舱单委托保存
    /// </summary>
    [Serializable]
    public class SaveRequestBookingDelegate : SaveRequest
    {
        #region 业务ID
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        #endregion

        #region 业务类型
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }
        #endregion

        #region ID
        /// <summary>
        /// ID
        /// </summary>
        public Guid?[] ItemIDs { get; set; }
        #endregion

        #region 舱单映射ID
        /// <summary>
        /// 舱单映射ID
        /// </summary>
        public int[] BookingMapIDs { get; set; }
        #endregion

        #region 舱单号
        /// <summary>
        /// 舱单号
        /// </summary>
        public string[] BookingNos { get; set; }
        #endregion

        #region 舱单名称
        /// <summary>
        /// 舱单名称
        /// </summary>
        public string[] BookingNames { get; set; }
        #endregion

        #region 委托时间
        /// <summary>
        /// 委托时间
        /// </summary>
        public DateTime[] BookingDates { get; set; }
        #endregion

        #region 运输方式
        /// <summary>
        /// 运输方式
        /// </summary>
        public CSP_FREIGHTMETHODTYPE[] FreightMethodTypes { get; set; }
        #endregion

        #region 运输类型
        /// <summary>
        /// 运输类型
        /// </summary>
        public CSP_SHIPMENTTYPE[] ShipmentTypes { get; set; }
        #endregion

        #region 贸易条款ID
        /// <summary>
        /// 贸易条款ID
        /// </summary>
        public Guid[] IncoTermIDs { get; set; }
        #endregion

        #region 运输条款
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid[] TransportClauseIDs { get; set; } 
        #endregion

        #region 贸易类型
        /// <summary>
        /// 贸易类型
        /// </summary>
        public CSP_TRADETYPE[] TradeTypes { get; set; }
        #endregion

        #region 是否需要拖车
        /// <summary>
        /// 是否需要拖车
        /// </summary>
        public bool[] IsTrucks { get; set; }
        #endregion

        #region 是否报关
        /// <summary>
        /// 是否报关
        /// </summary>
        public bool[] IsDeclarations { get; set; }
        #endregion

        #region 是否保险
        /// <summary>
        /// 是否保险
        /// </summary>
        public bool[] IsInsurances { get; set; }
        #endregion

        #region 客户
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid[] CustomerIDs { get; set; }
        #endregion

        #region 发货人
        /// <summary>
        /// 发货人ID
        /// </summary>
        public Guid[] ShipperIDs { get; set; }
        #endregion

        #region 收货人
        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid?[] ConsigneeIDs { get; set; }
        #endregion

        #region 装货港ID
        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid?[] POLIDs { get; set; }
        #endregion

        #region 装货港地址映射ID
        /// <summary>
        /// 装货港地址映射ID
        /// </summary>
        public int[] POLAddressMapIDs { get; set; }

        #endregion

        #region 装货港地址
        /// <summary>
        /// 装货港地址
        /// </summary>
        public string[] POLAddresss { get; set; }

        #endregion

        #region ETD For POL
        /// <summary>
        /// ETD For POL
        /// </summary>
        public DateTime?[] ETDForPOLs { get; set; }

        #endregion

        #region 卸货港ID
        /// <summary>
        /// 卸货港ID
        /// </summary>
        public Guid?[] PODIDs { get; set; }
        #endregion

        #region 卸货港地址映射ID
        /// <summary>
        /// 卸货港地址映射ID
        /// </summary>
        public int[] PODAddressMapIDs { get; set; }

        #endregion

        #region 卸货港地址
        /// <summary>
        /// 卸货港地址
        /// </summary>
        public string[] PODAddresss { get; set; }

        #endregion

        #region ETA For POD
        /// <summary>
        /// ETA For POD
        /// </summary>
        public DateTime?[] ETAForPODs { get; set; }

        #endregion

        #region 箱信息
        /// <summary>
        /// 箱信息
        /// </summary>
        public string[] Containerss { get; set; }
        #endregion

        #region 数量、数量单位
        /// <summary>
        /// 数量
        /// </summary>
        public int[] Quantitys { get; set; }
        /// <summary>
        /// 数量单位ID
        /// </summary>
        public Guid?[] QuantityUnitIDs { get; set; }
        #endregion

        #region 重量、重量单位
        /// <summary>
        /// 重量
        /// </summary>
        public decimal[] Weights { get; set; }
        /// <summary>
        /// 重量单位ID
        /// </summary>
        public Guid?[] WeightUnitIDs { get; set; }

        #endregion

        #region 体积、体积单位
        /// <summary>
        /// 体积
        /// </summary>
        public decimal[] Measurements { get; set; }
        /// <summary>
        /// 体积单位ID
        /// </summary>
        public Guid?[] MeasurementUnitIDs { get; set; }

        #endregion

        #region 是否同步到ICP
        /// <summary>
        /// 是否同步到ICP
        /// </summary>
        public bool IsSyncCSP { get; set; }
        #endregion

        #region 业务员ID
        /// <summary>
        /// 业务员ID
        /// </summary>
        public Guid[] SalesIDs { get; set; }
        #endregion

        #region 取消备注
        /// <summary>
        /// 取消备注
        /// </summary>
        public string[] CancelRemarks { get; set; }
        #endregion

        #region 是否取消
        /// <summary>
        /// 是否取消
        /// </summary>
        public bool[] IsCancels { get; set; } 
        #endregion

        #region 更新人
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveBy { get; set; }
        #endregion

        #region 更新时间
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        #endregion
    } 
    #endregion
}
