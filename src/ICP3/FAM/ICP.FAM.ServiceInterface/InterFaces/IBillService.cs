using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects;
using System.ServiceModel;
using ICP.FAM.ServiceInterface.CompositeObjects;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 账单服务类
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IBillService
    {
        #region 获得账单列表
        /// <summary>
        /// 获得账单列表
        /// </summary>
        /// <param name="auditorState">审核状态(0:全部、1:已审核、2:未审核)</param>
        /// <param name="writeOffStatue">核销状态(0:全部、1:已核销、2:未核销)</param>
        /// <param name="feeWay">收付类型(0:全部、1:、2:)</param>
        /// <param name="invoceStatue">发票状态(0:全部、1:已开发票、2:未开发票)</param>
        /// <param name="isCommission">是否为业务管理成本</param>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="billNo">账单号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="refNo">参考号</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="salesID">业务员</param>
        /// <param name="operateID">操作员</param>
        /// <param name="billingStartDate">计费开始时间</param>
        /// <param name="billingEndDate">计费结束时间</param>
        /// <param name="amountMin">核销金额最小值</param>
        /// <param name="amountMax">核销金额最大值</param>
        /// <param name="operationParameter">其他条件XML</param>
        /// <param name="isEnglish">是否为英文版本</param>
        /// <param name="operType">operType</param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BillListAllData GetBillListByList(
                               BillSearchAuditorStatue auditorState,
                               BillSearchWriteOffStatue writeOffStatue,
                               BillSearchFeeWay feeWay,
                               BillSearchInvoiceStatue invoceStatue,
                               bool? isCommission,
                               String companyIDs,
                               string billNo,
                               string customerName,
                               string refNo,
                               string invoiceNo,
                               Guid? salesID,
                               Guid? operateID,
                               DateTime? billingStartDate,
                               DateTime? billingEndDate,
                               Decimal? amountMin,
                               Decimal? amountMax,
                               DataPageInfo dataPageInfo,
                               OperationType operType,
                               OperationParameter operationParameter,
                               bool isEnglish);
        /// <summary>
        /// 根据账单ID集合获得账单的信息
        /// </summary>
        /// <param name="ids">账单ID集合</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CurrencyBillList> GetBillListByIds(Guid[] ids, bool isEnglish);

        /// <summary>
        /// 获得账单列表
        /// </summary>
        /// <param name="id">业务ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CurrencyBillList> GetBillListById(Guid id);

        #endregion

        #region 获得账单列表(重载)
        /// <summary>
        /// 获得账单列表(重载)
        /// </summary>
        /// <param name="auditorState">审核状态(0:全部、1:已审核、2:未审核)</param>
        /// <param name="writeOffStatue">核销状态(0:全部、1:已核销、2:未核销)</param>
        /// <param name="feeWay">收付类型(0:全部、1:、2:)</param>
        /// <param name="invoceStatue">发票状态(0:全部、1:已开发票、2:未开发票)</param>
        /// <param name="isCommission">是否为业务管理成本</param>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="billNo">账单号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="refNo">参考号</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="salesID">业务员</param>
        /// <param name="operateID">操作员</param>
        /// <param name="billingStartDate">计费开始时间</param>
        /// <param name="billingEndDate">计费结束时间</param>
        /// <param name="amountMin">核销金额最小值</param>
        /// <param name="amountMax">核销金额最大值</param>
        /// <param name="operationParameter">其他条件XML</param>
        /// <param name="isEnglish">是否为英文版本</param>
        /// <param name="operType">operType</param>
        /// <param name="currencyID">币种</param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BillListAllData GetBillListByCurrency(
                               BillSearchAuditorStatue auditorState,
                               BillSearchWriteOffStatue writeOffStatue,
                               BillSearchFeeWay feeWay,
                               BillSearchInvoiceStatue invoceStatue,
                               bool? isCommission,
                               String companyIDs,
                               String operationIDs,
                               string billNo,
                               string customerName,
                               string refNo,
                               string invoiceNo,
                               Guid? salesID,
                               Guid? operateID,
                               DateTime? billingStartDate,
                               DateTime? billingEndDate,
                               Decimal? amountMin,
                               Decimal? amountMax,
                               DataPageInfo dataPageInfo,
                               OperationType operType,
                               Guid? currencyID,
                               OperationParameter operationParameter,
                               bool isEnglish);
        #endregion

        #region 审核账单
        /// <summary>
        /// 审核账单
        /// </summary>
        /// <param name="billIds">业务ID集合</param>
        /// <param name="isAuditor">是否审核(True为审核,False为取消审核)</param>
        /// <param name="changeByID">操作人</param>
        /// <param name="updateDates">最后更新时间集合</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult AuditorBill(Guid[] billIds,
                                 bool isAuditor,
                                 Guid changeByID,
                                 DateTime?[] updateDates,
                                 bool isEnglish);
        #endregion

        #region  获得销账历史

        /// <summary>
        /// 根据帐单号获得核销列表
        /// </summary>
        /// <param name="billID">账单ID</param>
        /// <param name="currencyID">币种ID</param>
        /// <param name="way">收付方向</param>
        /// <param name="isCommission">是否佣金</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<WriteOffItemList> GetWriteOffListByBill(
            Guid billID,
            Guid currencyID,
            FeeWay way,
            bool isCommission,
            bool isEnglish);
        #endregion

        #region 根据业务ID获得账单数据
        /// <summary>
        /// 根据业务ID获得账单数据
        /// </summary>
        /// <param name="operationIDs"></param>
        ///<param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CurrencyBillList> GetBillListByOperID(Guid[] billIds, Guid[] operIds, bool isEnglish);

        #endregion

        #region 保存退佣数据
        /// <summary>
        /// 获得退佣数据
        /// </summary>
        /// <param name="billIDList">账单ID集合</param>
        /// <param name="customersID">客户ID</param>
        /// <param name="operateRate">手续费比例</param>
        /// <param name="isEnglish">是否英文</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ComissionData GetComissionDataByCodition(List<Guid> operationIDList, Guid customersID, decimal operateRate, bool isEnglish);

        /// <summary>
        /// 获得退佣数据
        /// </summary>
        /// <param name="operationIDList"></param>
        /// <param name="customersID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ComissionData GetComissionDataByWF(List<Guid> operationIDList, List<Guid> currencyIDList, Guid customersID, bool isEnglish);

        #endregion

        #region 获得币种账单费用明细
        /// <summary>
        /// 获取帐单下费用列表
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <param name="currenyID">币种</param>
        /// <param name="feeWay">方向</param>
        /// <param name="isCommission">是否佣金</param>
        /// <returns>返回帐单下费用列表</returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetChargeListByBill")]
        List<ChargeList> GetChargeList(
                        Guid billID,
                        Guid currenyID,
                        FeeWay feeWay,
                        bool isCommission);
        #endregion

        #region 获得业务信息
        /// <summary>
        /// 获得业务信息(开发票时使用)
        /// </summary>
        /// <param name="id">业务ID</param>
        /// <param name="billID">账单ID</param>
        /// <param name="isEnglish">是否为英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        BusinessByInvoice GetBusinessByInvoice(Guid[] operationIDs, Guid[] billIDs, bool isEnglish);
        #endregion

        #region 获得账单的发票列表
        /// <summary>
        /// 获取帐单下费用的发票列表
        /// </summary>
        /// <param name="billID">帐单ID</param>
        /// <param name="currenyID">币种</param>
        /// <param name="feeWay">方向</param>
        /// <param name="isCommission">是否佣金</param>
        /// <returns>返回帐单下发票列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<InvoiceList> GetInvoiceList(Guid billID,
                        Guid currenyID,
                        FeeWay feeWay,
                        bool isCommission);

        #endregion

        #region 更新账单的客户参考号
        /// <summary>
        /// 保存客户参考号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerRefNo"></param>
        /// <param name="updateDate"></param>
        /// <param name="saveById"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveBillCustomerRefNo(
                             Guid id,
                             string customerRefNo,
                             DateTime? updateDate,
                             Guid saveById);
        #endregion

        #region  得到代理账单是否修改  joe 2013-08-20

        /// <summary>
        /// 得到代理账单是否修改
        /// </summary>
        /// <param name="operationID">海出业务号</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool GetAgentBillState(Guid operationID);
        #endregion

        #region 获取口岸下对应客户最近一次应收账单需要要开增值税发票的税率
        /// <summary>
        /// 获取口岸下对应客户最近一次应收账单需要要开增值税发票的税率
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="customerID">客户ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Decimal GetAPBillTariff(Guid companyID, Guid customerID);
        #endregion

        #region 关帐之前判断是否有业务没有签收
        /// <summary>
        /// 关帐之前判断是否有业务没有签收
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="AccountingClosingDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsAcceptForAccountingClosing(Guid CompanyId, DateTime AccountingClosingDate);
        #endregion

        #region 生成交货地手续费
        /// <summary>
        /// 生成交货地手续费
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="saveById">保存人</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveRegenerateDeliveryHandlingFee(Guid operationID, Guid saveById);
        #endregion

        #region 应收账单转代理
        /// <summary>
        /// 应收账单转代理
        /// </summary>
        /// <param name="saveRequest">保存对象</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ConvertBillFromARToDN(SaveRequestBillConvert saveRequest);
        #endregion
    }
}
