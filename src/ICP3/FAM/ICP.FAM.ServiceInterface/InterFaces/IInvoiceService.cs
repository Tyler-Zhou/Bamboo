using System;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 发票用到的服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IInvoiceService
    {
        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <param name="companyIds">公司ID集合</param>
        /// <param name="customerName">客户</param>
        /// <param name="titleCName">中文抬头</param>
        /// <param name="titleEName">英文抬关</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="billNo">帐单号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="ctnNo">箱号</param>
        /// <param name="expressNo">快递号</param>
        /// <param name="remark">备注</param>
        /// <param name="amountMin">最大金额</param>
        /// <param name="amountMax">最大金额</param>
        /// <param name="invoiceBeginTime">开票开始日期</param>
        /// <param name="invoiceEndTime">开票结束日期</param>
        /// <param name="etdBeginTime">ETD开始日期</param>
        /// <param name="etdEndTime">ETD结束日期</param>
        /// <param name="dataPageInfo">包含了 当前页码数 每页显示行数 排序名</param>
        /// <returns>InvoiceData</returns>
       [FunctionInfomation]  [OperationContract]
        PageList GetInvoiceListByList(
                                        Guid[] companyIds,
                                        string customerName,
                                        string titleCName,
                                        string titleEName,
                                        bool? isValid,
                                        string invoiceNo,
                                        string operationNo,
                                        string billNo,
                                        string blNo,
                                        string ctnNo,
                                        string expressNo,
                                        string remark,
                                        decimal? amountMin,
                                        decimal? amountMax,
                                        DateTime? invoiceBeginTime,
                                        DateTime? invoiceEndTime,
                                        DateTime? etdBeginTime,
                                        DateTime? etdEndTime,
                                        DataPageInfo dataPageInfo);

        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <param name="ids">Ids</param>
        /// <returns>InvoiceList</returns>
       [FunctionInfomation]  [OperationContract]
        PageList GetInvoiceListByIds(Guid[] ids,DataPageInfo dataPageInfo);


         /// <summary>
        /// 获取某公司一定时间内发票信息
        /// </summary>
        /// <param name="Company">公司</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">Ids</param>
       /// <returns>ShortInvoiceInfo</returns>
       [FunctionInfomation]
       [OperationContract]
       List<ShortInvoiceInfo> GetCompanyInvoiceListByDate(Guid Company, DateTime start, DateTime end);

        /// <summary>
        /// 获取发票信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>InvoiceInfo</returns>
       [FunctionInfomation]  [OperationContract]
        InvoiceInfo GetInvoiceInfo(Guid id,bool isEnglish);

        /// <summary>
        /// 改变发票状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="IsValid">是否有效</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
       [FunctionInfomation]  [OperationContract]
        SingleResult CancelInvoice(
            Guid id,
            bool IsValid,
            Guid changeByID,
            DateTime? updateDate,
            bool IsEnglish);

       
        /// <summary>
        /// 保存发票
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="invoiceDate">发票日期</param>
        /// <param name="companyId">公司ID</param>
        /// <param name="bank1Id">银行1</param>
        /// <param name="bank2Id">银行2</param>
        /// <param name="tax">税金</param>
        /// <param name="customerId">客户</param>
        /// <param name="titleCName">中文抬头</param>
        /// <param name="titleEName">英文抬头</param>
        /// <param name="soNo">订舱号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="containerTypeId">箱型</param>
        /// <param name="blNo">提单号</param>
        /// <param name="etd">离港日</param>
        /// <param name="polId">装货港</param>
        /// <param name="podId">卸货港</param>
        /// <param name="placeOfDeliveryId">交货地</param>
        /// <param name="voyageId">船名</param>
        /// <param name="VoyageNo">航次</param>
        /// <param name="remark">备注</param>
        /// <param name="updateDate">更新日期</param>
        /// <param name="billFeeIds">对应的费用IDs</param>
        /// <param name="feeIds">费用IDs</param>
        /// <param name="feeCurrencyIds">费用币种</param>
        /// <param name="feeChargingCodeIds">费用-费用代码</param>
        /// <param name="feeRates">费用汇率</param>
        /// <param name="feeQuantities">费用数量</param>
        /// <param name="feeAmounts">费用金额</param>
        /// <param name="feeRemarks">费用备注</param>
        /// <param name="feeUpdateDates">费用更新日期</param>
        /// <param name="changeByID">保存人</param>
        /// <param name="isEnglish">IsEnglish</param>
        /// <returns>SingleResult 表0"ID","Upadate","No" 表1 "ID"</returns>
       [FunctionInfomation]  [OperationContract]
        HierarchyManyResult SaveInvoiceInfo(
            Guid id,
            string invoiceNo,
            DateTime  invoiceDate,
            string expressNo,
            DateTime? expressDate,
            Guid companyId,
            Guid bank1Id,
            Guid? bank2Id,
            decimal tax,
            Guid? customerId,
            string titleCName,
            string titleEName,
            Guid? taxCustomerID,
            Guid? taxCompanyID,
            string customerAddressTel,
            string customerTaxNo,
            string customerAccountNo,
            string receivablesName,
            string reviewName,
            CustomerInvoiceType invoiceType,
            string soNo,
            string containerNo,
            string  containerTypeId,
            string blNo,
            DateTime? etd,
            string polId,
            string podId,
            string  placeOfDeliveryId,
            string  voyageId,
            string  VoyageNo,
            string remark,
            DateTime? updateDate,
            Guid[] billFeeIds,
            Guid[] feeIds,
            Guid[] feeCurrencyIds,
            Guid[] feeChargingCodeIds,
            decimal[] feeRates,
            decimal[] feeQuantities,
            decimal[] feeAmounts,
            string [] feeRemarks,
            DateTime?[] feeUpdateDates,
            Guid changeByID,
            bool isEnglish);


        /// <summary>
        /// 设置发票的快递信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="expressNo">快递号</param>
        /// <param name="expressDate">快递日期</param>
        /// <param name="changeByID">更新人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <param name="IsEnglish">IsEnglish</param>
        /// <returns>返回SingleResult</returns>
       [FunctionInfomation]  [OperationContract]
        SingleResult SetInvoiceExpressInfo(
            Guid id,
            string expressNo,
            DateTime? expressDate,
            Guid changeByID,
            DateTime? updateDate,
            bool IsEnglish);

        /// <summary>
        /// 设置发票号和快递单号
        /// </summary>
        /// <param name="billIDs"></param>
        /// <param name="currencyIDs"></param>
        /// <param name="ways"></param>
        /// <param name="isCommissions"></param>
        /// <param name="invoiceIds"></param>
        /// <param name="invoiceNos"></param>
        /// <param name="expressNos"></param>
        /// <param name="changeByID"></param>
        /// <param name="invoiceUpdateDates"></param>
        /// <param name="IsEnglish"></param>
       [FunctionInfomation]
       [OperationContract]
       void SaveInvoiceNoAndExpressNoForBills(
          Guid[] billIDs,
          Guid[] currencyIDs,
          FeeWay[] ways,
          bool[] isCommissions,
          string[] invoiceIds,
          string[] invoiceNos,
          string[] expressNos,
          string[] invoiceTimes,
          Guid changeByID,
          string[] invoiceUpdateDates,
          bool IsEnglish);

        /// <summary>
        /// 删除账单
        /// </summary>
        /// <param name="ids">明细ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">日期</param>
        /// <param name="isEnglish">isEnglish</param>
       [FunctionInfomation]
       [OperationContract(Name = "RemoveBillInfoByInvoice")]
        void RemoveBillInfo(Guid[]  ids,string updateDate, Guid removeByID,
                          bool isEnglish);
        /// <summary>
        /// 删除费用明细
        /// </summary>
        /// <param name="ids">IDS</param>
        /// <param name="updateDate">updateDate</param>
        /// <param name=" removeByID">removeByID</param>
        /// <param name="isEnglish">isEnglish</param>
        [FunctionInfomation]  [OperationContract]
        void RemoveChargesInfo(Guid[] ids, DateTime[] updateDate, Guid removeByID,
                         bool isEnglish);
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="isEnglish">isEnglish </param>
        /// <returns>CustomerInvoiceInfo</returns>
       [FunctionInfomation]  [OperationContract]
       CustomerInvoiceInfo GetCustomerInfo(string CustomerName, bool isEnglish);

        /// <summary>
        /// 获得发票统计数据
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="isEnglish">中英文版本</param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       InvoieCountBaseReport GetInvoieCountReport(Guid companyID, DateTime beginDate, DateTime endDate, bool isEnglish);

        /// <summary>
        /// 保存发票号
        /// </summary>
        /// <param name="nos"></param>
        /// <param name="invoiceNos"></param>
        /// <param name="saveById"></param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       List<InvoiceInfo> SaveInvoiceNo(string[] nos, string[] invoiceNos, Guid saveById, bool isEnglish);


       /// <summary>
       /// 获取发票的汇率列表
       /// </summary>
       /// <param name="solutionID">解决方案ID</param>
       /// <param name="isValid">是否有效</param>
       /// <returns>返回发票的汇率列表</returns>
       [FunctionInfomation]
       [OperationContract]
       List<SolutionExchangeRateList> GetInvoiceExchangeRateList(bool? isValid);


           /// <summary>
        /// 更改发票的汇率有效状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeByID">改变人ID</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
       [FunctionInfomation]
       [OperationContract]
        SingleResultData ChangeInvoiceExchangeRateState(
           Guid id,
           bool isValid,
           Guid changeByID,
           DateTime? updateDate);

       /// <summary>
       /// 保存解决方案的汇率信息
       /// </summary>
       /// <param name="ids">ID</param>
       /// <param name="sourceCurrencyIDs">源币种ID</param>
       /// <param name="targetCurrencyIDs">目标币种ID</param>
       /// <param name="fromDates">开始时间</param>
       /// <param name="toDates">结束时间</param>
       /// <param name="rates">汇率</param>
       /// <param name="saveByID">保存人ID</param>
       /// <param name="updateDates">数据版本</param>
       /// <returns>返回SingleResultData</returns>
       [FunctionInfomation]
       [OperationContract]
       ManyResultData SaveInvoiceExchangeRateInfo(
           Guid?[] ids,
           Guid[] sourceCurrencyIDs,
           Guid[] targetCurrencyIDs,
           DateTime[] fromDates,
           DateTime[] toDates,
           decimal[] rates,
           Guid saveByID,
           DateTime?[] updateDates);

        /// <summary>
        /// 获得发票免税明细表
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="beginDate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       InvoiceFreeReportData GetInvoiceFreeList(Guid companyID, DateTime beginDate, DateTime endDate);

        /// <summary>
        /// 发票合同表
        /// </summary>
        /// <param name="bills"></param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       InvoiceFreeReportData GetInvoiceContractReportt(Guid[] bills,Guid currencyID);


       [FunctionInfomation]
       [OperationContract]
       List<InvoiceCountReport> GetOperationInvoiceReport(Guid companyID, DateTime fromDate, DateTime toDate);

       /// <summary>
       /// 获取公司税号
       /// </summary>
       /// <param name="companyID"></param>
       /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       string GetCompanyTaxNo(Guid companyID);
    }
}
