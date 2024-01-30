using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirExport.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 保存PO的时候传递的实体
    /// </summary>
    [Serializable]
    public class POSaveRequest : SaveRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? id;
        /// <summary>
        /// RelationID
        /// </summary>
        public Guid? relationID;
        /// <summary>
        /// 订单
        /// </summary>
        public Guid orderID;
        /// <summary>
        /// PO号
        /// </summary>
        public string no;
        /// <summary>
        /// 订单描述
        /// </summary>
        public string podc;
        /// <summary>
        /// 卖主
        /// </summary>
        public Guid? vendorID;
        /// <summary>
        /// 卖主描述
        /// </summary>
        public string vendor;
        /// <summary>
        /// 买主
        /// </summary>
        public Guid? buyerID;
        /// <summary>
        /// 买主描述
        /// </summary>
        public string buyer;
        /// <summary>
        /// 最终目的地
        /// </summary>
        public string finalDestination;
        /// <summary>
        /// 入仓时间
        /// </summary>
        public DateTime? inWarehouseDate;
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? orderDate;
        /// <summary>
        /// 数据版本
        /// </summary>
        public DateTime? updateDate;
        /// <summary>
        /// ItemID
        /// </summary>
        public Guid?[] itemIDs;
        /// <summary>
        /// Item号列表
        /// </summary>
        public string[] itemNos;
        /// <summary>
        /// Item描述列表
        /// </summary>
        public string[] itemDescriptions;
        /// <summary>
        /// Item颜色列表
        /// </summary>
        public string[] itemColors;
        /// <summary>
        /// Item尺寸列表
        /// </summary>
        public string[] itemSizes;
        /// <summary>
        /// Item体积列表
        /// </summary>
        public decimal[] itemVolumes;
        /// <summary>
        /// Item重量列表
        /// </summary>
        public decimal[] itemWeights;
        /// <summary>
        /// Item装箱数量列表
        /// </summary>
        public int[] itemCartons;
        /// <summary>
        /// Item件数列表
        /// </summary>
        public int[] itemUnits;
        /// <summary>
        /// Item海关编码列表
        /// </summary>
        public string[] itemHTSCodes;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] itemUpdateDates;
    }
}
