using System;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.Common.ServiceInterface.DataObjects;
using System.Data;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 帐单接口
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ICustomerBillService
    {
        #region 获取帐单列表
        /// <summary>
        /// 获取帐单列表
        /// </summary>
        /// <param name="companyIDs">口岸ID列表</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="billState">账单状态</param>
        /// <param name="billType">账单类型</param>
        /// <param name="isValid">是否可用</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="maxRecords">返回最大行行数</param>
        /// <returns>返回帐单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<BillList> GetBillList(Guid[] companyIDs, string invoiceNo, string customerName
            , BillState? billState, BillType? billType, bool? isValid, DateTime? beginTime,
            DateTime? endTime, int maxRecords); 
        #endregion

        #region 获取帐单列表
        /// <summary>
        /// 获取帐单列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>返回帐单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<BillList> GetBillListByOperactioID(Guid operationID); 
        #endregion

        #region 获取帐单列表
        /// <summary>
        /// 获取帐单列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <returns>返回帐单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<BillInfo> GetBillInfos(Guid operationID); 
        #endregion

        #region 设置帐单已寄单
        /// <summary>
        /// 设置帐单已寄单
        /// </summary>
        /// <param name="ids">帐单ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SetBillIsSend(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates); 
        #endregion

        #region 保存帐单信息
        /// <summary>
        /// 保存帐单信息
        /// </summary>
        /// <param name="billinfos">billinfos</param>
        /// <param name="operationID">业务ID</param>
        /// <param name="saveByID">saveByID</param>
        /// <param name="SaveSource">数据来源</param>
        /// <param name="operationDate">业务时间</param>
        /// <returns>HierarchyManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        List<HierarchyManyResult> SaveBillInfos(
            Guid operationID,
            List<BillInfo> billinfos,
            Guid saveByID,
            byte SaveSource,
            DateTime operationDate
            ); 
        #endregion

        #region 批量保存帐单信息
        /// <summary>
        /// 批量保存帐单信息
        /// </summary>
        /// <param name="billinfos">账单列表信息</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="operationDate">操作时间</param>
        /// <returns>HierarchyManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        List<HierarchyManyResult> BatchSaveBillInfos(
             List<BillInfo> billinfos,
             Guid saveByID,
             DateTime operationDate); 
        #endregion

        #region 获取帐单列表
        /// <summary>
        /// 获取帐单列表
        /// </summary>
        /// <param name="ids">帐单IDs</param>
        /// <returns>返回帐单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<BillList> GetBillListByIDs(Guid[] ids); 
        #endregion

        #region 开始执行Revise时间
        /// <summary>
        /// 开始执行Revise时间
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DateTime? GetStartDateForReviseAgentBill(); 
        #endregion

        #region 获取帐单信息
        /// <summary>
        /// 获取帐单信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回帐单信息</returns>
        [FunctionInfomation]
        [OperationContract]
        BillInfo GetBillInfo(Guid id); 
        #endregion

        #region 获取帐单下费用列表
        /// <summary>
        /// 获取帐单下费用列表
        /// </summary>
        /// <param name="billIDs">帐单ID</param>
        /// <returns>返回帐单下费用列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetChargeListByCustomerBill")]
        List<ChargeList> GetChargeList(Guid[] billIDs); 
        #endregion

        #region 获取帐单下费用列表
        /// <summary>
        /// 获取帐单下费用列表
        /// </summary>
        /// <param name="billIDs">帐单ID</param>
        /// <returns>返回帐单下费用列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ChargeList> GetChargeListForPrintBill(Guid[] billIDs, bool isEN); 
        #endregion

        #region 删除帐单
        /// <summary>
        /// 删除帐单
        /// </summary>
        /// <param name="ids">帐单ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        int RemoveBillInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates); 
        #endregion

        #region 删除费用信息
        /// <summary>
        /// 删除费用信息
        /// </summary>
        /// <param name="ids">费用ID列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        [FunctionInfomation]
        [OperationContract(Name = "RemoveChargeInfoByCustomerBill")]
        void RemoveChargeInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates); 
        #endregion

        #region 保存帐单信息
        /// <summary>
        /// 保存帐单信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="formID">表单ID</param>
        /// <param name="formType">表单类型</param>
        /// <param name="id">帐单ID</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="shipToID">Ship To ID</param>
        /// <param name="customerDescription">客户的描述信息</param>
        /// <param name="custoemrRefNo">客户参考号</param>
        /// <param name="type">账单类型（0:应收,1:应付,2:代理）</param>
        /// <param name="accountDate">账单日</param>
        /// <param name="dueDate">到期日</param>
        /// <param name="payCurrencyId">按一种币种支付.为空时说明不是按一种币种支付的</param>
        /// <param name="auditorId">审核人ID</param>
        /// <param name="auditorEmail">审核人邮件地址</param>
        /// <param name="state">账单状态</param>
        /// <param name="billNo">账单号</param>
        /// <param name="operationDate">业务时间</param>
        /// <param name="billFromType">账单来源类型海出1海进2</param>
        /// <param name="rateCurrencyID">汇率的币种(如果这个列表里没有该币种就按公司配置下默认的)</param>
        /// <param name="rateValue">汇率值(如果这个列表里没有该币种就按公司配置下默认的)</param>
        /// <param name="remark">备注</param>
        /// <param name="isVATInvoiced">是否开增值税发票</param>
        /// <param name="taxrate">税率</param>
        /// <param name="updateDate">帐单版本</param>
        /// <param name="feeIDs">费用-ID列表</param>
        /// <param name="feeWays">费用-方向列表</param>
        /// <param name="feeTypes">费用-类型列表</param>
        /// <param name="feeIsAgents">费用-是否代理费列表</param>
        /// <param name="feeIsSecondSales">费用-是否二次销售列表</param>
        /// <param name="feeIsVATInvoiceds">费用-是否开增值税发票列表</param>
        /// <param name="feeIsGSTs">费用-GSTs列表</param>
        /// <param name="feeChargingCodeIDs">费用-费用代码ID列表</param>
        /// <param name="feeDescriptions">费用-费用描述列表</param>
        /// <param name="feeCurrencyIDs">费用-币种ID列表</param>
        /// <param name="feeRates">费用-汇率列表</param>
        /// <param name="feeContainerIDs">费用-关联柜号ID列表</param>
        /// <param name="feeUnitIDs">费用-费用单位列</param>
        /// <param name="feeUnitPrices">费用-费用单价列表</param>
        /// <param name="feeQuantities">费用-费用数量列表</param>
        /// <param name="feeAmounts">费用-费用金额列表</param>
        /// <param name="feeRemarks">费用-费用备注列表</param>
        /// <param name="feeUpdateDates">费用-数据版本</param>
        /// <param name="feeFromType">费用-费用来源类型海出1海进2</param>
        /// <param name="feeIsRevises">费用-是否修改</param>
        /// <param name="IsClosingEdit">是否是关账后的修改</param>
        /// <param name="IsAppcfm">Appcfm </param>
        /// <param name="saveByID">修改人ID</param>
        /// <returns>ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveBillInfo(
            Guid operationID,
            Guid formID,
            FormType formType,
            Guid? id,
            Guid companyID,
            Guid customerID,
            Guid? shipToID,
            FAMCustomerDescription customerDescription,
            string custoemrRefNo,
            BillType type,
            DateTime accountDate,
            DateTime dueDate,
            Guid? payCurrencyId,
           Guid? auditorId,
            string auditorEmail,
            BillState state,
            string billNo,
           DateTime operationDate,
            int? billFromType,
            Guid[] rateCurrencyID,
            decimal[] rateValue,
            string remark,
            bool isVATInvoiced,
            Decimal? taxrate,
            DateTime? updateDate,
            Guid?[] feeIDs,
            FeeWay[] feeWays,
            FeeType[] feeTypes,
            bool[] feeIsAgents,
            bool[] feeIsSecondSales,
            bool[] feeIsVATInvoiceds,
            bool[] feeIsGSTs,
            Guid[] feeChargingCodeIDs,
            string[] feeDescriptions,
            Guid[] feeCurrencyIDs,
            decimal[] feeRates,
            Guid?[] feeContainerIDs,
            Guid[] feeUnitIDs,
            decimal[] feeUnitPrices,
            decimal[] feeQuantities,
            decimal[] feeAmounts,
            string[] feeRemarks,
            DateTime?[] feeUpdateDates,
            int?[] feeFromType,
            bool[] feeIsRevises,
            bool IsClosingEdit,
            bool IsAppcfm,
            Guid saveByID
           ); 
        #endregion

        #region 保存帐单及其费用列表
        /// <summary>
        /// 保存帐单及其费用列表
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns>ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        HierarchyManyResult SaveBillAndFeeList(SaveRequestBill saveRequest); 
        #endregion

        #region 获取所属提单的业务的所有应收账单列表
        /// <summary>
        /// 获取所属提单的业务的所有应收账单列表
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <returns>CurrencyBillList</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CurrencyBillList> GetDRCurrencyBillList(Guid operationId); 
        #endregion

        #region 获取公司的打印汇率列表
        /// <summary>
        /// 获取公司的打印汇率列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <returns>返回公司的汇率列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<SolutionExchangeRateList> GetCompanyPrintBillExchangeRateList(Guid companyID); 
        #endregion

        #region 获取帐单信息
        /// <summary>
        /// 获取帐单信息
        /// 2013-7-31 joe initial
        /// </summary>
        /// <param name="operationID">业务ID</param>
        ///<param name="billType">账单类型（1:应收,2:应付,3:代理）</param>
        /// <returns>返回帐单列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<BillInfo> GetBillListByOperactioIDAndBillType(Guid operationID, BillType billType); 
        #endregion

        #region 得到你账单可以绑定的参考号ID，NO
        /// <summary>
        /// 得到你账单可以绑定的参考号ID，NO
        /// </summary>
        /// <param name="operationID">操作ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        DataTable GetValidReNos(Guid operationID); 
        #endregion

        #region 账款统计
        /// <summary>
        /// 账款统计
        /// </summary>
        /// <param name="arcsearchParameter">账款统计查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerAgingList> GetCustomerAgingList(AccReceControlSearchParameter arcsearchParameter); 
        #endregion

        #region 获取客户日志
        /// <summary>
        /// 获取客户日志
        /// </summary>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        string GetCustomerAgingLogs(Guid? customerId); 
        #endregion

        #region 保存客户日志
        /// <summary>
        /// 保存客户日志
        /// </summary>
        /// <param name="logInfo"></param>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveCustomerAgingLog(CustomerLogSaveSequest logInfo); 
        #endregion

        #region 保存客户日志文件
        /// <summary>
        /// 保存客户日志文件
        /// </summary>
        /// <param name="AgingID"></param>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveCustomerAgingLogAtts(Guid AgingID, string filename, byte[] file, Guid CreateBy); 
        #endregion

        #region 删除客户日志文件
        /// <summary>
        /// 删除客户日志文件
        /// </summary>
        /// <param name="ID"></param>
        [FunctionInfomation]
        [OperationContract]
        void DeleteCustomerAgingLogAtts(Guid ID); 
        #endregion

        #region 删除客户日志
        /// <summary>
        /// 删除客户日志
        /// </summary>
        /// <param name="ID"></param>
        [FunctionInfomation]
        [OperationContract]
        void DeleteCustomerAgingLogs(Guid ID); 
        #endregion

        #region 保存费用设置
        /// <summary>
        /// 保存费用设置
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveLocalFeeConfigure(LocalFeeConfigureSaveRequest logInfo); 
        #endregion

        #region 获取费用设置
        /// <summary>
        /// 获取费用设置
        /// </summary>
        /// <param name="CarrierIDs"></param>
        /// <param name="LocationIDs"></param>
        /// <param name="ShippingLineIDs"></param>
        /// <param name="CompanyIDs"></param>
        /// <param name="ChargeID"></param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <param name="IsValid"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<LocalFeeConfigure> GetLoaclFeeConfigureList(Guid[] CarrierIDs, Guid[] LocationIDs, Guid[] ShippingLineIDs, Guid[] CompanyIDs, Guid ChargeID, DateTime StartDate, DateTime EndDate, bool IsValid, bool IsEnglish); 
        #endregion

        #region 删除费用配置
        /// <summary>
        /// 删除费用配置
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IsEnglish"></param>
        [FunctionInfomation]
        [OperationContract]
        void DeleteLoaclFeeConfigure(Guid ID, bool IsEnglish); 
        #endregion

        #region 获取业务本地费用列表
        /// <summary>
        /// 获取业务本地费用列表
        /// </summary>
        /// <param name="OperationID"></param>
        /// <param name="IsEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<AddLocalFeeList> GetLocalFeeListForOperationID(Guid OperationID, bool IsEnglish); 
        #endregion

        
    }
}
