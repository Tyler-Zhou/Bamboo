using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects;
using System.ServiceModel;

namespace ICP.FCM.AirExport.ServiceInterface
{
    [ServiceInfomation("空运出口提单报表服务")]
    [ServiceContract]
    public interface IAirExportBLReportService
    {
        /// <summary>
        /// 获取空运提单MBL报表数据
        /// </summary>
        /// <param name="MBLId><主提单ID/param>
        /// <param name="isEnglish">是否英文环境</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BLReportData_HBL GetMBLReportData(Guid Id,
            bool isEnglish, BLType type);
        /// <summary>
        /// 获取空运提单HBL报表数据
        /// </summary>
        /// <param name="HBLId">分提单ID/</param>
        /// <param name="isEnglish">是否英文环境</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BLReportData_HBL GetHBLReportData(Guid HBLId,
            bool isEnglish);

        /// <summary>
        /// 获取空运出口业务联单报表数据
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        AEOrderReportData GetAEOrderReportData(Guid bookID);
    }
}
