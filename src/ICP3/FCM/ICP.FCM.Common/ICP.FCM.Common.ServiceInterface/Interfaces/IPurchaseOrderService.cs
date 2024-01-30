using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 采购单服务
    /// </summary>
    [ServiceInfomation("采购单服务")]
    [ServiceContract]
    public interface IPurchaseOrderService
    {
        /// <summary>
        ///  保存导入的采购单
        /// </summary>
        /// <param name="saveRequests">保存集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveImportPurchaseOrderItem(List<SaveRequestPurchaseOrderItem> saveRequests);

        /// <summary>
        /// 通过业务信息查询采购单明细
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<PurchaseOrderItem> SearchPurchaseOrderItemByShipmentInfo(SearchParameterOrderItemByShipment searchParameter);
    }
}
