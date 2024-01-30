using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.Common.ServiceInterface
{
   
    /// <summary>
    /// FCM.Common服务
    /// </summary>
    [ServiceInfomation("FCM.Common服务")]
    [ServiceContract]
    public interface IBusinessService
    {
        /// <summary>
        /// 获取航次相关时间信息
        /// </summary>
        /// <param name="voyagelIds">航次Id列表</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<VoyageDateInfo> GetVoyageDateInfo(List<Guid?> voyagelIds);

        /// <summary>
        /// 获取订舱列表
        /// </summary>
        /// <param name="operationTypes">业务类型</param>
        /// <param name="companyIDs">业务所在口岸公司(操作的公司)</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="salesID">业务员</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="state">状态()</param>
        /// <param name="dateSearchType">日期搜索类型()</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回订舱列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<BusinessData> GetBusinessListForDataFinder(
            OperationType[] operationTypes,
            Guid[] companyIDs,  
            string operationNo,
            string blNo,
            string customerName,
            string polName,
            string podName,
            Guid? salesID,
            bool? isValid,
            OrderState? state,
            DateSearchTypeForDataFinder dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords);

        /// <summary>
        /// 保存附件和SO NO
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult UpdateSoForOperation(FileCopyParameters parameter);

        /// <summary>
        /// 批量更新业务的ETA,提货地
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult UpdateOceanEDAandWareHouse(UpdateETAInfo updateInfo);

        /// <summary>
        /// 获取更新的业务ID集合
        /// </summary>
        /// <param name="updateInfo"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Guid> GetOperationIdsForUpdate(UpdateETAInfo updateInfo);

        /// <summary>
        ///  通过海出订单ID得到所有相关联的海进业务ID  
        ///  2013-07-22 joe 
        /// </summary>
        /// <param name="OEBookingID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<SimpleBusinnessInfo> GetSimpleBusinessByBookingID(Guid OEBookingID);

        /// <summary>
        ///  获得上次分发日志的ID  
        /// </summary>
        /// <param name="OIBookingID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        string GetDispatchLastLogID(Guid OIBookingID);

        /// <summary>
        ///  获得最新分发日志的ID  
        /// </summary>
        /// <param name="OIBookingID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        string GetDispatchNewLogID(Guid OIBookingID);

        /// <summary>
        ///  发起账单修订
        /// </summary>
        /// <param name="OEBookingID">海出业务ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="ramark"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        int ApplyRevise(Guid OEBookingID, Guid userID, string ramark);

        /// <summary>
        ///  通过海进业务ID得到所有相关联的海出业务ID  
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<SimpleBusinnessInfo> GetOEIDByOIID(Guid OIBookingID);
    }
}