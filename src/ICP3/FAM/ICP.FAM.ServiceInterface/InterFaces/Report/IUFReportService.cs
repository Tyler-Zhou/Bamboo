using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.ServiceInterface
{



    /// <summary>
    /// 用友统计报表
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IUFReportService
    {
        /// <summary>
        /// 科目余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="fromGLCode">开始科目</param>
        /// <param name="toGLCode">结束科目</param>
        /// <param name="glCodeType">科目类型(1资产;2负债;3权益;4成本;5损益)</param>
        /// <param name="fromGLLevel">开始级别</param>
        /// <param name="toGLLevel">结束级别</param>
        /// <param name="showEndLevel">只显示未级科目</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="showCumulation">本期无发生额，累计有发生显示</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<GLBalanceData> GetGLBalanceDataList(Guid[] companyIDs,
                                                string fromGLCode,
                                                string toGLCode,
                                                GLCodeType glCodeType,
                                                int fromGLLevel,
                                                int toGLLevel,
                                                bool showEndLevel,
                                                DateTime fromDate,
                                                DateTime toDate,
                                                bool showCumulation,
                                                Guid? CurrencyID,
                                                GLCodeLedgerStyle format);

        /// <summary>
        /// 科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="glID">科目ID</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<GLDetailData> GetGLDetailDataList(Guid[] companyIDs,
                                               Guid glID,
                                               DateTime fromDate,
                                               DateTime toDate);


        /// <summary>
        /// 外币式科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="glID">科目ID</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<GLDetailData> GetFCGLDetailBalanceList(Guid[] companyIDs,
                                                      Guid glID,
                                                      DateTime fromDate,
                                                      DateTime toDate);

        /// <summary>
        /// 客户余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="glIDs">科目ID集合</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="propertys">余额方向(1借方,2贷方)</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerGLBalance> GetCustomerGLBalanceList(Guid[] companyIDs,
                                                         Guid[] customerIDs,
                                                         Guid[] glIDs,
                                                         DateTime fromDate,
                                                         DateTime toDate,
                                                         GLCodeProperty[] propertys,
                                                         GLCodeLedgerStyle format);

        /// <summary>
        /// 客户三栏余额表(不需要)
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerID">客户ID</param>
        /// <param name="glIDs">科目ID集合</param>
        /// <param name="years">年份</param>
        /// <param name="noAccounting">包含未记账凭证</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Customer3ColumnGLBalance> GetCustomer3ColumnGLBalanceList(Guid[] companyIDs,
                                                                       Guid customerID,
                                                                       Guid glIDs,
                                                                       int years,
                                                                       bool noAccounting);

        /// <summary>
        /// 客户科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="glIDs">科目ID集合</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerGLDetail> GetCustomerGLDetailList(Guid[] companyIDs,
                                                       Guid[] customerIDs,
                                                       Guid[] glIDs,
                                                       DateTime fromDate,
                                                       DateTime toDate,
                                                       GLCodeLedgerStyle format);

        /// <summary>
        /// 个人科目余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门ID集合</param>
        /// <param name="personalIDs">个人ID集合</param>
        /// <param name="glIds">科目ID集合</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="property">统计方向(0全部;1借方;2贷方)</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<PersonalGLBalance> GetPersonalGLBalanceList(Guid[] companyIDs,
                                                         Guid[] departmentIDs,
                                                         Guid[] personalIDs,
                                                         Guid[] glIds,
                                                         DateTime fromDate,
                                                         DateTime toDate,
                                                         GLCodeProperty property);



        /// <summary>
        /// 个人三栏余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门部门ID</param>
        /// <param name="personalID">个人ID</param>
        /// <param name="glId">科目ID</param>
        /// <param name="years">年份</param>
        /// <param name="property">余额方向</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<Personal3ColumnGLBalance> GetPersonal3ColumnGLBalanceList(Guid[] companyIDs,
                                                                Guid[] departmentIDs,
                                                                Guid personalID,
                                                                Guid glId,
                                                                DateTime fromDate,
                                                                DateTime toDate);

        /// <summary>
        /// 个人科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门ID集合</param>
        /// <param name="personalIDs">个人ID集合</param>
        /// <param name="glIds">科目ID集合</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="orderByDebit">借方在前,贷方在后</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<PersonalGLDetail> GetPersonalGLDetailList(Guid[] companyIDs,
                                                       Guid[] departmentIDs,
                                                       Guid[] personalIDs,
                                                       Guid[] glIds,
                                                       DateTime fromDate,
                                                       DateTime toDate,
                                                       bool orderByDebit);


    }
}
