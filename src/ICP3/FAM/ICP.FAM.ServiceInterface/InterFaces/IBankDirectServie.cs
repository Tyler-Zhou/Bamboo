using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.FAM.ServiceInterface.CompositeObjects;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 银行信息的服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IBankDirectServie
    {
        #region 获得指定公司下所有有效的银行账号
        /// <summary>
        /// 获取某个公司下的所有直连银行号信息
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BankAccountList> DirectBankAccountList(DirectBankSearchParameter searchParameter);

        #endregion

        #region 根据销账ID获取支付信息
        /// <summary>
        /// 根据销账ID获取支付信息
        /// </summary>
        /// <param name="searchParameter">查询对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        APIPaymentInfo GetSinglePaymentInfo(PaymentSearchParameter searchParameter); 
        #endregion

        #region 根据销账ID获取支付信息
        /// <summary>
        /// 根据销账ID获取支付信息
        /// </summary>
        /// <param name="searchParameter">查询对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<APIPaymentInfo> GetBankDirectPaymentInfoList(BatchPaymentSearchParameter searchParameter); 
        #endregion

        #region 获取支付验证码
        /// <summary>
        /// 获取支付验证码
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        void GetPaymentValidCode(SinglePaymentSaveRequest saveRequest);
        #endregion

        #region 保存单个支付
        /// <summary>
        /// 保存单个支付
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool SaveSinglePaymentInfo(SinglePaymentSaveRequest saveRequest); 
        #endregion

        #region 保存批量支付
        /// <summary>
        /// 保存批量支付
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool SaveBatchPaymentInfo(BatchPaymentSaveRequest saveRequest); 
        #endregion

        #region 银行流水关联前检查销账数据
        /// <summary>
        /// 银行流水关联前检查销账数据
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BankTransaction2Checks AssociationTransactionCheck(AssociationSaveRequest saveRequest);
        #endregion

        #region 关联银行流水到销账数据
        /// <summary>
        /// 关联银行流水到销账数据
        /// </summary>
        /// <param name="saveRequest">保存请求对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool AssociationTransactionToWriteOff(AssociationSaveRequest saveRequest);
        #endregion

        #region 关联银行流水到销账数据
        /// <summary>
        /// 关联银行流水到销账数据
        /// </summary>
        /// <param name="searchParameter">查询对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        AssociationInfo GetAssociationInfo(AssociationSearchParameter searchParameter);
        #endregion

        #region 获取银行流水报表信息
        /// <summary>
        /// 获取银行流水报表信息
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BankTransactionReportData> ReportDataForBankTransaction(BankTransactionReportSearchParameter searchParameter);

        #endregion
    }
}
