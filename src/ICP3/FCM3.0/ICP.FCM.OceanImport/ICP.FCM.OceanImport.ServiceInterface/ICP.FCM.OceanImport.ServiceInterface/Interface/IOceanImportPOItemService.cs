using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 海运进口PO服务类
    /// </summary>
    [ServiceInfomation("海运PO服务")]
    [ServiceContract]
    public interface IOceanImportPOItemService
    {
        /// <summary>
        /// 获取PO列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回PO列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OceanImportPOList> GetOIOrderPOList(Guid orderID);

        /// <summary>
        /// 保存PO信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="relationID">RelationID</param>
        /// <param name="orderID">订单ID</param>
        /// <param name="no">PO号</param>
        /// <param name="podc">订单描述</param>
        /// <param name="vendorID">卖主</param>
        /// <param name="vendor">卖主描述</param>
        /// <param name="buyerID">买主</param>
        /// <param name="buyer">买主描述</param>
        /// <param name="finalDestination">最终目的地</param>
        /// <param name="inWarehouseDate">入仓时间</param>
        /// <param name="orderDate">处理时间</param>
        /// <param name="updateDate">数据版本</param>
        /// <param name="itemIDs">ItemID</param>
        /// <param name="itemNos">Item号列表</param>
        /// <param name="itemDescriptions">Item描述列表</param>
        /// <param name="itemColors">Item颜色列表</param>
        /// <param name="itemSizes">Item尺寸列表</param>
        /// <param name="itemVolumes">Item体积列表</param>
        /// <param name="itemWeights">Item重量列表</param>
        /// <param name="itemCartons">Item装箱数量列表</param>
        /// <param name="itemUnits">tem件数列表</param>
        /// <param name="itemHTSCodes">Item海关编码列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="itemUpdateDates">更新时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveOIOrderPOInfo(POSaveRequest poSaveRequest);

        /// <summary>
        /// 删除PO
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveOIOrderPOInfo(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);
    }
}
