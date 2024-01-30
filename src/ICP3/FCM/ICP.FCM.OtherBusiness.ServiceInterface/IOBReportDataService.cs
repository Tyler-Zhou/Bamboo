using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System;
using System.ServiceModel;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    /// <summary>
    /// 其他业务报表服务
    /// </summary>
    [ServiceInfomation("其他业务报表服务")]
    [ServiceContract]
    public interface IOBReportDataService
    {

         /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns></returns>
        [OperationContract]
        OBOrderReportData GetOBOrderReportData(Guid orderID, Guid companyID);
        /// <summary>
        /// 派车国外报表数据对象(短格式)
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupENShortReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        PickupENShortReportData GetPickupENShortReportData(Guid truckID);

        /// <summary>
        /// 获取派车国外报表数据对象
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupENReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        PickupENReportData GetPickupENReportData(Guid truckID);

        /// <summary>
        /// 获派车国内报表数据对象
        /// </summary>
        /// <param name="truckID">拖车ID</param>
        /// <returns>PickupCNReportData</returns>
        [FunctionInfomation]
        [OperationContract]
        PickupCNReportData GetPickupCNReportData(Guid truckID);


    }
}
