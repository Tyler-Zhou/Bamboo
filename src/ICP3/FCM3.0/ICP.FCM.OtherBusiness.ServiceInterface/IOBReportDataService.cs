using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    [ServiceInfomation("其他业务报表服务")]
    [ServiceContract]
    public interface IOBReportDataService
    {

         /// <summary>
        /// 打印业务联单
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns></returns>
        [OperationContract]
        OBOrderReportData GetOBOrderReportData(Guid orderID, bool IsEnglish);
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
