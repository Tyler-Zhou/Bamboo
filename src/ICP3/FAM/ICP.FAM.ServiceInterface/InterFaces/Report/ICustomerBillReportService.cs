using System;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Common.ServiceInterface.DataObjects;
using System.ServiceModel;
using ICP.FAM.ServiceInterface.DataObjects.Report;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 帐单的报表服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ICustomerBillReportService
    {

        /// <summary>
        /// 获取费用列表报表数据
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>费用清单的报表对象</returns>
        [FunctionInfomation]
        [OperationContract]
        FeeListReportData GetFeeListReportData(Guid operationID);

        /// <summary>
        /// 获取报表帐单列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>返回报表帐单</returns>
        [FunctionInfomation]
        [OperationContract]
        CommonBillReportData GetCommonBillReportData(Guid operationID);

        /// <summary>
        /// 获取报表帐单列表
        /// </summary>
        /// <param name="billId">billId</param>
        /// <returns>返回报表帐单</returns>
        [FunctionInfomation]
        [OperationContract]
        LocalBillReportData GetLocalBillReportData(Guid billId);

        /// <summary>
        /// 获取后缀号And数据库里最大后缀号
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <returns>SuffixNo</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult GetSuffixNo(Guid billID);

        /// <summary>
        /// 保存后缀号
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <param name="suffixNo">suffixNo</param>
        /// <param name="saveByID">saveByID</param>
        [FunctionInfomation]
        [OperationContract]
        void SaveSuffixNo(Guid billID, string suffixNo, Guid saveByID);

        /// <summary>
        /// 获取帐单打印用的汇率列表
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <returns>SolutionExchangeRateListe</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionExchangeRateList> GetPrintBillRateList(Guid billID);


        /// <summary>
        /// 保存帐单打印用的汇率列表
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <param name="sourceCurrencys">sourceCurrencyIDs</param>
        /// <param name="tagerCurrencys">tagerCurrencyIDs</param>
        /// <param name="rates">rates</param>
        /// <param name="saveByID">saveByID</param>
        [FunctionInfomation]
        [OperationContract]
        void SavePrintBillRateList(Guid billID, string[] sourceCurrencys, string[] tagerCurrencys
            , decimal[] rates, Guid saveByID);


        /// <summary>
        /// 获得催款单报表数据
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="userID">用户ID</param>
        /// <param name="bankID">银行ID</param>
        /// <param name="billIDs">帐单ID集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BillDunReportData GetBillDunReportData(Guid customerID, Guid companyID, Guid userID,
            Guid? bankID, Guid[] billIDs);


        /// <summary>
        /// 获取报表账龄列表
        /// </summary>
        /// <param name="StructType"></param>
        /// <param name="StructNodeId"></param>
        /// <param name="ETD_Ending_Date"></param>
        /// <param name="ViewType"></param>
        /// <param name="IsEnglish"></param>
        /// <param name="GroupBy"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<DebitnoteAgeSumData> GetDebitnoteAgeReportData(byte StructType, string StructNodeId
            , DateTime ETD_Ending_Date, byte ViewType, string IsEnglish, string GroupBy);


        /// <summary>
        /// 批量账单
        /// </summary>
        ///// <param name="customerID">客户ID</param>
        ///// <param name="companyID">公司ID</param>
        /// <param name="billIDs">帐单ID集合</param>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BatchBillReportData GetBatchBillReportData(Guid customerID, Guid companyID,Guid[] billIDs, Guid userID);
    }
}
