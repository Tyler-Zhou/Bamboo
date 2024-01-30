using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SaveRequestPurchaseOrderItem : SaveRequest
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

        #region 唯一键
        /// <summary>
        /// 唯一键
        /// </summary>
        public int[] IDs { get; set; }
        #endregion

        #region 提单ID
        /// <summary>
        /// 提单ID
        /// </summary>
        public Guid[] BillOfLadingIDs { get; set; }
        #endregion

        #region 提单号
        /// <summary>
        /// 提单号
        /// </summary>
        public string[] BillOfLadingNOs { get; set; }
        #endregion

        #region 箱ID
        /// <summary>
        /// 箱ID
        /// </summary>
        public Guid[] ContainerIDs { get; set; }
        #endregion

        #region 箱号
        /// <summary>
        /// 箱号
        /// </summary>
        public string[] ContainerNOs { get; set; }
        #endregion

        #region 采购单ID
        /// <summary>
        /// 采购单ID
        /// </summary>
        public int[] PurchaseOrderIDs { get; set; }
        #endregion

        #region 采购单号
        /// <summary>
        /// 采购单号
        /// </summary>
        public string[] PurchaseOrderNOs { get; set; }
        #endregion

        #region 产品名称
        /// <summary>
        /// 产品名称
        /// </summary>
        public string[] ProductNames { get; set; }
        #endregion

        #region 库存量单位(SKU)
        /// <summary>
        /// 库存量单位(SKU)
        /// </summary>
        public string[] StockKeepingUnits { get; set; }
        #endregion

        #region 制造商零件编号(MPN)
        /// <summary>
        /// 制造商零件编号(MPN)
        /// </summary>
        public string[] ManufacturerPartNumbers { get; set; }
        #endregion

        #region 纸箱
        /// <summary>
        /// 纸箱
        /// </summary>
        public int[] CartonCounts { get; set; }
        #endregion

        #region 件数
        /// <summary>
        /// 件数
        /// </summary>
        public decimal[] Quantitys { get; set; }
        #endregion

        #region 单元成本
        /// <summary>
        /// 单元成本
        /// </summary>
        public decimal[] UnitCostPrices { get; set; }
        #endregion

        #region 重量
        /// <summary>
        /// 重量
        /// </summary>
        public decimal[] Weights { get; set; }
        #endregion

        #region 体积
        /// <summary>
        /// 体积
        /// </summary>
        public decimal[] Volumes { get; set; }
        #endregion

        #region 保存人ID
        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid SaveByID { get; set; }
        #endregion

        #region 更新时间
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        #endregion
    }
}
