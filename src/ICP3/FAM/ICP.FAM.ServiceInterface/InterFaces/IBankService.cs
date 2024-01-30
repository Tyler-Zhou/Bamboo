using System;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 银行信息的服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IBankService
    {
        #region 获得指定公司下所有有效的银行账号
        /// <summary>
        /// 获取某个公司下的所有银行号信息
        /// </summary>
        /// <param name="companyId">公司ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BankAccountList> GetCompanyBankAccounts(Guid companyId, bool isEnglish);

        #endregion

        

        #region 通过银行账号获取账号明细及其公司信息
        /// <summary>
        /// 通过银行账号获取账号明细及其公司信息
        /// </summary>
        /// <param name="accountNO">公司ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BankAccountList GetBankAccountByNO(string accountNO);

        #endregion

        #region 获取银行账号列表，用户常用的排最前面
        /// <summary>
        /// 获取银行账号列表，用户常用的排最前面
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BankAccountList> GetBankAccountsOrderByRecentUsedFirst(Guid companyId, Guid userId, bool isEnglish);

        #endregion

        #region 获得银行列表
        /// <summary>
        /// 根据查询面板的输入条件，获取银行列表
        /// 银行的账号信息（从表）也要返回
        /// </summary> 
        /// <param name="companyIds">公司ID的数组</param>
        /// <param name="simpleName">银行账号的简称</param>
        /// <param name="cnName">银行账号的中文名称</param>
        /// <param name="enName">银行账号的英文名称</param>
        /// <param name="isValid">账号有效性</param>
        /// <param name="dataPageInfo">dataPageInfo</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        PageList GetBankList(
             Guid[] companyIds,
             string simpleName,
             string cnName,
             string enName,
             bool? isValid,
             DataPageInfo dataPageInfo,
             bool isEnglish
             );

        #endregion

        #region 获得银行账号列表
        /// <summary>
        /// 获得指定银行账号列表
        /// </summary>
        /// <param name="bankID">银行ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BankAccountList> GetBankAccountList(object bankID, bool isEnglish);
        #endregion

        #region 保存关联的银行信息
        /// <summary>
        /// 保存银行与公司的关联
        /// </summary>
        /// <param name="bankID">银行ID</param>
        /// <param name="companyIds">公司ID集合</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <param name="saveByID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveBankAndCompany(Guid bankID, Guid[] companyIds, bool isEnglish, Guid saveByID);
        #endregion

        #region 获得银行关联的所有公司
        /// <summary>
        /// 获得银行关联的公司集合
        /// </summary>
        /// <param name="bankID">银行ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Guid> GetBankAndCompany(Guid bankID);
        #endregion

        #region 获得银行详细信息
        /// <summary>
        /// 获取银行的详细信息
        /// </summary>
        /// <param name="bankId">主键ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BankInfo GetBankInfo(Guid bankId, bool isEnglish);
        #endregion

        #region 保存银行信息

        /// <summary>
        /// 保存银行信息
        /// </summary>
        /// <param name="saveRequest">银行信息</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <param name="accounts">accounts</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveBankInfo(BankSaveRequest saveRequest, BankAccountSaveRequest[] accounts, bool isEnglish);
        #endregion

        #region 修改银行的有效性
        /// <summary>
        /// 修改银行有效性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <param name="saveById"></param>
        /// <param name="updateDate"></param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeBankValidity(Guid id, bool isCancel, Guid saveById, DateTime? updateDate, bool isEnglish);

        #endregion

        #region  修改银行账号的有性效

        /// <summary>
        /// 修改银行账号有效性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <param name="saveById"></param>
        /// <param name="updateDate"></param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeBankAccountValidity(Guid id, bool isCancel, Guid saveById, DateTime? updateDate, bool isEnglish);

        #endregion

        #region 获得帐号详细信息
        /// <summary>
        /// 获得帐号的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BankAccountInfo GetBankAccountInfo(Guid id, bool isEnglish);
        #endregion

        #region 银行流水
        /// <summary>
        /// 通过销账单号支付(API)
        /// </summary>
        /// <param name="writeOffID">销账ID</param>
        /// <param name="saveBy">保存人</param>
        [FunctionInfomation]
        [OperationContract]
        void PluginPaymentByWriteOffID(Guid writeOffID, Guid saveBy);

        /// <summary>
        /// 获取交易流水
        /// </summary>
        /// <param name="requestParameter"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BankTransactionInfo> GetTransList(BankTransactionSearchParameter requestParameter);
        /// <summary>
        /// 获取银行/销账关联数据
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BankTransaction2Checks> GetBankTransaction2Checks(BankTransaction2ChecksSearchParameter searchParameter);
        /// <summary>
        /// 关联银行流水到销账数据
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool BankTransactionAssociationChecks(SaveRequestBankTransaction2Checks saveRequest);
        #endregion
    }
}
