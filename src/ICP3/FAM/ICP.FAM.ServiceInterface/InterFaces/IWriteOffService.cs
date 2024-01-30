using System;
using System.Collections.Generic;

using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FAM.ServiceInterface.CompositeObjects;
using System.ServiceModel;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 销账用到的方法
    /// 创建时间：2011-07-11 15:44
    /// 作者：熊中方
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IWriteOffService
    {
        #region 获得销账列表
        /// <summary>
        /// 获取销账单列表
        /// </summary>
        /// <param name="searchParameter">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        PageList GetWriteOffListByList(WriteOffSearchParameter searchParameter);

        #endregion

        #region 获得销账单上的账单信列表

        /// <summary>
        /// 销账单上的账单费用明细
        /// </summary>
        /// <param name="writeOffId"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<WriteOffBill> GetWriteOffBillsByIds(Guid writeOffId);

        #endregion

        #region 获得销账单上的其他项目(原财务费用 )费用列表
        /// <summary>
        /// 销账单上的财务费用明细
        /// </summary>
        /// <param name="writeOffId"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<WriteOffCharge> GetWriteOffCharges(Guid writeOffId);

        #endregion

        #region 保存销账信息
        /// <summary>
        /// 保存销账信息
        /// </summary>
        /// <param name="saveRequest">销账信息实体</param>
        /// <param name="writeOffByID">销账人</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveWriteOffInfo(SaveRequestCheck saveRequest, Guid writeOffByID, bool isEnglish);

        //[FunctionInfomation]
        //[OperationContract]
        //string GetServerTime();
        #endregion

        #region 获得销账详细信息
        /// <summary>
        /// 获得销账详细信息
        /// </summary>
        /// <param name="id">销账ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        WriteOffItemInfo GetWriteOffItemInfo(Guid id);
        #endregion

        #region 获得币种信息
        /// <summary>
        /// 获得币种信息
        /// </summary>
        /// <param name="id">销账ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<OperationCurrencyAmountList> GetOperationCurrencyAmountList(Guid id);

        #endregion

        #region 作废销帐信息
        /// <summary>
        /// 作废销帐信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isValid"></param>
        /// <param name="changeByID"></param>
        /// <param name="updateDate"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult VoidCheckData(Guid id, bool isValid, Guid changeByID, DateTime? updateDate, bool isEnglish);

        #endregion

        #region 删除销账信息
        /// <summary>
        /// 删除销账信息
        /// </summary>
        /// <param name="id">销账单ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveWriterOff(Guid id, Guid removeByID, DateTime? updateDate);
        #endregion

        #region 删除销账币种信息
        /// <summary>
        /// 删除销账币种信息
        /// </summary>
        /// <param name="id">销账单ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">更新时间</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveWriterCurrency(Guid id, Guid removeByID, DateTime? updateDate);
        #endregion

        #region 审核/取消审核  销账信息
        /// <summary>
        /// 审核/取消审核 销账单
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="auditorById">操作人</param>
        /// <param name="isCheck">是否审核:True为审核,False为取消审核</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult AuditorWriterOff(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid auditorById,
            bool isCheck);
        #endregion

        #region 检查是否有重复的:到帐时间，实收/付金额和银行都相同的已确认到帐的销帐列表
        /// <summary>
        /// 检查是否有重复的:到帐时间，实收/付金额和银行都相同的已确认到帐的销帐列表
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="receivedAmts"></param>
        /// <param name="bankDates"></param>
        /// <param name="currencys"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        string CheckExistBankReceived(Guid[] checkIds, DateTime?[] bankDates, decimal[] receivedAmts, Guid?[] bankAccountIDs);
        #endregion

        #region 到账
        /// <summary>
        /// 到帐
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="reachedDates">到账时间集合</param>
        /// <param name="amounts">金额集合</param>
        /// <param name="accountIDs">银行账号ID集合</param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="chargeByID">更改人ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult WriteOffReachedByCheck(
                             Guid[] ids,
                             DateTime?[] reachedDates,
                             decimal[] amounts,
                             Guid?[] accountIDs,
                             DateTime?[] updateDates,
                             Guid chargeByID);

        #endregion

        #region 取消到帐
        /// <summary>
        /// 取消到帐
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="remark">备注信息</param>
        /// <param name="chargeByID">操作人</param>
        /// <param name="updateDates">最后更新时间集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult CancelReached(
                            Guid[] ids,
                            string remark,
                            Guid chargeByID,
                            DateTime?[] updateDates);
        #endregion

        #region 获得销账的凭证列表
        /// <summary>
        /// 根据核销单的ID获得凭证列表
        /// </summary>
        /// <param name="writeOffID">核销ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CredentialsDetailList> GetCredentialsDetailList(Guid writeOffID);

        #endregion

        #region 删除凭证明细
        /// <summary>
        /// 删除凭证明细
        /// </summary>
        /// <param name="id">凭证ID</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="removeByID">删除人</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveCredentialsDetail(
            Guid id,
            DateTime? updateDate,
            Guid removeByID);
        #endregion

        #region 保存凭证明细
        /// <summary>
        /// 保存凭证明细
        /// </summary>
        /// <param name="writeOffID">销账ID</param>
        /// <param name="ids">ID集合</param>
        /// <param name="glIDs">会计科目ID集合</param>
        /// <param name="remarks">Remark集合</param>
        /// <param name="orgDebigs">orgDebig集合</param>
        /// <param name="orgCredits">orgCredit集合</param>
        /// <param name="rates">汇率集合</param>
        /// <param name="debigs">debig集合</param>
        /// <param name="credits">credit集合</param>
        /// <param name="customers">customer集合</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">最后更新时间集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveCredentialsDetail(
                Guid writeOffID,
                Guid?[] ids,
                Guid[] glIDs,
                string[] remarks,
                decimal[] orgDebigs,
                decimal[] orgCredits,
                decimal[] rates,
                decimal[] debigs,
                decimal[] credits,
                Guid[] customers,
                Guid saveByID,
                DateTime?[] updateDates);
        #endregion

        #region 自动生成凭证

        /// <summary>
        /// 自动生成凭证
        /// </summary>
        /// <param name="writeOffID"></param>
        /// <param name="saveByID"></param>
        [FunctionInfomation]
        [OperationContract]
        void BuildRPLedgers(
         Guid writeOffID,
         Guid saveByID);

        #endregion

        #region 根据币种账单信息获得费用信息并转换为销账信息
        /// <summary>
        /// 根据币种账单信息获得费用信息并转换为销账信息
        /// </summary>
        /// <param name="currencyList">币种账单列表</param>
        /// <param name="billIDs">账单ID集合</param>
        /// <param name="currencyIDs">币种ID集合</param>
        /// <param name="feeWays">收付方向集合</param>
        /// <param name="isCommissions">是否佣金集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<WriteOffBill> GetWriteOffBills(List<CurrencyBillList> currencyList, bool isEnglish);
        #endregion

        #region 根据销账单ID获得销账列表信息
        /// <summary>
        ///   根据销账单ID获得销账列表信息
        /// </summary>
        /// <param name="checkID">销账单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<WriteOffItemList> GetWriteOffListByIds(Guid[] checkID);
        #endregion

        #region 根据传入的费用ID获得发票信息
        /// <summary>
        /// 根据费用ID获得发票信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<InvoiceList> GetInvoiceListByFeeID(Guid[] chargeIDs);
        #endregion

        #region 保存其他项目(到帐时金额有差异用到)

        /// <summary>
        /// 保存其他项目 
        /// </summary>
        /// <param name="CheckIDs">销帐单ID集合</param>
        /// <param name="CustomerIDs">客户ID集合</param>
        /// <param name="BillNos">账单号集合</param>
        /// <param name="GLIDs">会计科目集合</param>
        /// <param name="CurrencyIDs">币种ID集合</param>
        /// <param name="Rates">汇率集合</param>
        /// <param name="Amounts">金额集合</param>
        /// <param name="Remarks">备注集合</param>
        /// <param name="Ways">方向集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveExpenseInfo(
                     Guid[] CheckIDs,
                     Guid?[] CustomerIDs,
                     String[] BillNos,
                     Guid[] GLIDs,
                     Guid[] CurrencyIDs,
                     Decimal[] Rates,
                     Decimal[] Amounts,
                     String[] Remarks,
                     Int32[] Ways,
                     Guid saveByID);


        #endregion

        #region 事务保存到账与差异
        /// <summary>
        /// 以事务的方式保存到账与差异
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="reachedDates"></param>
        /// <param name="amounts"></param>
        /// <param name="accountIDs"></param>
        /// <param name="updateDates"></param>
        /// <param name="chargeByID"></param>
        /// <param name="saveExpense"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult WriteOffReached(
                             Guid[] ids,
                             DateTime?[] reachedDates,
                             decimal[] amounts,
                             Guid?[] accountIDs,
                             DateTime?[] updateDates,
                             Guid chargeByID,
                             SaveExpenseList saveExpense);
        #endregion

        #region 获得多收多付列表
        [FunctionInfomation]
        [OperationContract]
        List<PrepaymentList> GetPrepaymentList(Guid companyID, Guid customerID);
        #endregion

        #region 解锁销账单
        [FunctionInfomation]
        [OperationContract]
        List<UntieLockCheckResult> UntieLockChecks(Guid[] checkIds, DateTime?[] checkUpdates, Guid[] checkAmountIDs, DateTime?[] checkAmountUpdates, Guid chargeByID);
        #endregion

        #region 支付客户信息
        /// <summary>
        /// 支付客户信息
        /// </summary>
        /// <param name="searchParameter"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerBankInfo> AllCustomerBanks(CustomerBankInfoSearchParameter searchParameter);
        #endregion
        
    }
}
