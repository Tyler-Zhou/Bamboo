namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Client;
    using System.ServiceModel;

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
        [OperationContract]
        List<VoyageDateInfo> GetVoyageDateInfo(List<Guid?> voyagelIds);
        /// <summary>
        /// 获取订舱列表
        /// </summary>
        /// <param name="companyIDs">业务所在口岸公司(操作的公司)</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="customerName">客户名</param>
        /// <param name="polName">装货港名</param>
        /// <param name="podName">卸货港名</param>
        /// <param name="salesID">业务员</param>
        /// <param name="bookingerID">订舱</param>
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
            //Guid? bookingerID,
            //Guid? overseasFilerID,
            bool? isValid,
            OEOrderState? state,
            DateSearchTypeForDataFinder dateSearchType,
            DateTime? beginTime,
            DateTime? endTime,
            int maxRecords);
    }
}