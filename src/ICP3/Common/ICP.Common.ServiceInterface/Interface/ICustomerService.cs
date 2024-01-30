//-----------------------------------------------------------------------
// <copyright file="ICustomerService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using ICP.Common.ServiceInterface.CompositeObjects;

namespace ICP.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using DataObjects;
    using Framework.CommonLibrary.Attributes;
    using Framework.CommonLibrary.Common;
    using System.ServiceModel;

    /// <summary>
    /// 公共客户管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ICustomerService
    {
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">全称</param>
        /// <param name="address">地址</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省份</param>
        /// <param name="customerState">客户状态</param>
        /// <param name="customerType">客户类型</param>
        /// <param name="isAgentOfCarrier">是否承运人（客户类型必须是货代才可以设置这个值）</param>
        /// <param name="codeApplyState">客户审核状态</param>
        /// <param name="areaID"></param>
        /// <param name="applyTimeFrom">申请时间-开始</param>
        /// <param name="applyTimeTo">申请时间-结束</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回客户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerList> GetCustomerListByList(
            string code,
            string name,
            string address,
            string tel,
            string fax,
            string eMail,
            Guid? countryID,
            Guid? provinceID,
            CustomerStateType? customerState,
            CustomerType? customerType,
            bool? isAgentOfCarrier,
            CustomerCodeApplyState? codeApplyState,
            Guid? areaID,
            DateTime? applyTimeFrom,
            DateTime? applyTimeTo,
            int maxRecords);

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">全称</param>
        /// <param name="address">地址</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省份</param>
        /// <param name="customerState">客户状态</param>
        /// <param name="customerType">客户类型</param>
        /// <param name="companyid">公司ID</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回客户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerList> GetExCustomerListByList(
            string code,
            string name,
            string address,
            string tel,
            string fax,
            string eMail,
            Guid? countryID,
            Guid? provinceID,
            CustomerStateType? customerState,
            CustomerType? customerType,
            Guid companyid,
            int maxRecords);

        /// <summary>
        /// 获得客户列表(绑定下拉框时使用)
        /// </summary>
        /// <param name="customerTypes">客户类型集合</param>
        /// <param name="isAgentOfCarrier">是否为货代</param>
        /// <param name="customerState">客户状态</param>
        /// <param name="codeApplyState">审核状态</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerList> GetCustomerListByCombox(
                CustomerType[] customerTypes,
                CustomerCodeApplyState? codeApplyState,
                bool? isAgentOfCarrier,
                CustomerStateType? customerState);

        /// <summary>
        /// 获得所有公司对应的客户列表
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerList> GetCustomerListCompany();


        /// <summary>
        /// 获取客户列表(搜索器用)
        /// </summary>
        /// <param name="codeOrName">代码或全称</param>
        /// <param name="address">地址</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省份</param>
        /// <param name="customerState">客户状态</param>
        /// <param name="customerTypes">客户类型</param>
        /// <param name="isAgentOfCarrier">是否承运人（客户类型必须是货代才可以设置这个值）</param>
        /// <param name="codeApplyState">客户审核状态</param>
        /// <param name="codeApplicantCompanyID">申请人所在解决方案</param>
        /// <param name="agentCustomerSolutionID">用于查找指定解决方案下的代理客户</param>
        /// <param name="applyTimeFrom">申请时间-开始</param>
        /// <param name="applyTimeTo">申请时间-结束</param>
        /// <param name="curruntUserID"></param>
        /// <param name="maxRecords">最大记录数</param>
        /// <param name="isFromOrder"></param>
        /// <returns>返回客户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerInfo> GetCustomerListBySearch(
            string codeOrName,
            string address,
            string tel,
            string fax,
            string eMail,
            Guid? countryID,
            Guid? provinceID,
            CustomerStateType? customerState,
            CustomerType[] customerTypes,
            bool? isAgentOfCarrier,
            CustomerCodeApplyState? codeApplyState,
            Guid? codeApplicantCompanyID,
            Guid? agentCustomerSolutionID,
            DateTime? applyTimeFrom,
            DateTime? applyTimeTo,
            bool isFromOrder,
            Guid? curruntUserID,
            int maxRecords);

        /// <summary>
        /// 获取客户列表(客户名称检查时用)
        /// </summary>
        /// <param name="name">全称</param>       
        /// <returns>返回客户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerList> GetCustomerListForName(string name);

        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enterprisecode"></param>
        /// <remarks>通过客户名、企业编码查询；用于宁波导入预配信息时判断是否需要新增客户</remarks>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerInfo> GetCustomerInfoBySearch(string name, string enterprisecode);

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回客户信息</returns>
        [FunctionInfomation]
        [OperationContract]
        CustomerInfo GetCustomerInfo(Guid id);

        /// <summary>
        /// 检测客户代码是否存在

        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="code">代码</param>
        /// <returns>存在返回true,否则返回fasle</returns>
        [FunctionInfomation]
        [OperationContract]
        bool CheckCustomerCodeExist(
            Guid customerID,
            string code);

        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <param name="cisaveRequest">客户保存对象</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract(Name = "SaveCustomerInfoBySaveRequest")]
        SingleResultData SaveCustomerInfo(CustomerInfoSaveRequest cisaveRequest);

        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="code">代码</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="cShortName">中文简称</param>
        /// <param name="eShortName">英文简称</param>
        /// <param name="cName">中文名</param>
        /// <param name="eName">英文名</param>
        /// <param name="cBillName">中文账单名称</param>
        /// <param name="eBillName">英文账单名称</param>
        /// <param name="cAddress">中文地址</param>
        /// <param name="eAddress">英文地址</param>
        /// <param name="countryID">国家</param>
        /// <param name="provinceID">省/州</param>
        /// <param name="cityID">城市ID</param>
        /// <param name="enterprisecodetype">企业代码类型</param>
        /// <param name="enterprisecode">企业代码</param>
        /// <param name="postCode">邮政编码</param>
        /// <param name="tel1">电话1</param>
        /// <param name="tel2">电话2</param>
        /// <param name="fax">传真</param>
        /// <param name="eMail">邮件地址</param>
        /// <param name="homepage">主页</param>
        /// <param name="taxIdType">税务登记类型</param>
        /// <param name="taxIdNo">税务登记号</param>
        /// <param name="bankAccountNo"></param>
        /// <param name="creditLimit">信用限额</param>
        /// <param name="term">信用期限</param>
        /// <param name="tradeTermID">贸易条款</param>
        /// <param name="paymentTypeID">付款方式</param>
        /// <param name="type">类型（0船东（Carrier）、1航空公司(Airline)、2货代（Forwarding）、3直客（DirectClient）、4拖车行（Trucker）、5报关行（Broker）、6仓储（Warehouse）、7铁路（Railway）、8快递（Express））</param>
        /// <param name="isAgentOfCarrier">是否承运人（客户类型必须是货代才可以设置这个值）</param>
        /// <param name="remark">备注</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract(Name = "SaveCustomerInfoByBasicParameters")]
        SingleResultData SaveCustomerInfo(
            Guid? id,
            string code,
             string keyWord,
            string cShortName,
            string eShortName,
            string cName,
            string eName,
            string cBillName,
            string eBillName,
            string cAddress,
            string eAddress,
            Guid countryID,
            Guid? provinceID,
            Guid? cityID,
            string enterprisecodetype,
            string enterprisecode,
            string postCode,
            string tel1,
            string tel2,
            string fax,
            string eMail,
            string homepage,
            TaxType? taxIdType,
            string taxIdNo,
            string bankAccountNo,
            decimal creditLimit,
            int term,
            Guid? tradeTermID,
            Guid? paymentTypeID,
            CustomerType type,
            bool isAgentOfCarrier,
            string fIRMCODE,
            string remark,
            Guid saveByID,
            bool isCompany,
            DateTime? updateDate);

        /// <summary>
        /// 保存例外客户
        /// </summary>
        /// <param name="companyid">公司ID</param>
        /// <param name="Customers">客户集合</param>
        /// <param name="savebyid">保存人</param>
        /// <param name="isEnglish">是否英文</param>
        [FunctionInfomation]
        [OperationContract]
        void SaveExCustomerInfo(Guid companyid, Guid[] Customers, Guid savebyid, bool isEnglish);

        /// <summary>
        /// 检查是否是例外客户
        /// </summary>
        /// <param name="companyid"></param>
        /// <param name="customerid"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ExCustomer> CheckExCustomer(Guid companyid, Guid customerid, bool isEnglish);

        /// <summary>
        /// 改变客户状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="state">状态</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="memoContent">备注内容</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeCustomerState(
            Guid id,
            CustomerStateType state,
            Guid changeByID,
            string memoContent,
            DateTime? updateDate);

        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回联系人列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerContactList> GetCustomerContactList(Guid customerID);

        /// <summary>
        /// 保存客户联系人信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID列表</param>
        /// <param name="cNames">中文名列表</param>
        /// <param name="eNames">英文名列表</param>
        /// <param name="departments">部门列表</param>
        /// <param name="positions">职位列表</param>
        /// <param name="tels">电话列表</param>
        /// <param name="faxs">传真列表</param>
        /// <param name="mobiles">手机列表</param>
        /// <param name="eMails">邮件列表</param>
        /// <param name="remarks">批注列表</param>
        /// <param name="types">类型列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveCustomerContactInfo(
            Guid customerID,
            Guid?[] ids,
            string[] cNames,
            string[] eNames,
            string[] departments,
            string[] positions,
            string[] tels,
            string[] faxs,
            string[] mobiles,
            string[] eMails,
            string[] remarks,
            CustomerContactType[] types,
            Guid saveByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 更改客户联系人有效状态
        /// </summary>
        /// <param name="id">ID列表</param>
        /// <param name="isValid">是否有效列表</param>
        /// <param name="changeByID">改变人</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData ChangeCustomerContactState(
            Guid id,
            bool isValid,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取合作伙伴列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回合作伙伴列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerPartnerList> GetCustomerPartnerList(Guid customerID);

        /// <summary>
        /// 保存合作伙伴信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID列表</param>
        /// <param name="partnerIDs">合作伙伴列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveCustomerPartnerInfo(
            Guid customerID,
            Guid?[] ids,
            Guid[] partnerIDs,
            Guid saveByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 删除合作伙伴
        /// </summary>
        /// <param name="ids">合作伙伴列表</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveCustomerPartnerInfo(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 获取客户审核列表
        /// </summary>
        /// <param name="customerCode">客户代码</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="applicantID">申请人</param>
        /// <param name="apllyDateFrom">申请时间-开始</param>
        /// <param name="applyDateTo">申请时间-结束</param>
        /// <param name="confirmorID">审批人</param>
        /// <param name="confirmDateFrom">审批时间-开始</param>
        /// <param name="confirmDateTo">审批时间-结束</param>
        /// <param name="state">状态</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回户审核列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerConfirmList> GetCustomerConfirmList(
            string customerCode,
            string customerName,
            Guid? applicantID,
            DateTime? apllyDateFrom,
            DateTime? applyDateTo,
            Guid? confirmorID,
            DateTime? confirmDateFrom,
            DateTime? confirmDateTo,
            CustomerCodeApplyState? state,
            int maxRecords);

        /// <summary>
        /// 获取最近的客户申请信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回客户审核信息</returns>
        [FunctionInfomation]
        [OperationContract]
        CustomerConfirmInfo GetLatelyCustomerConfirmInfo(Guid customerID);

        /// <summary>
        /// 申请客户信息
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="applicantID">申请人</param>
        /// <param name="applicantRemark">申请批注</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ApplyCustomerCode(
            Guid customerID,
            Guid applicantID,
            string applicantRemark,
            DateTime? updateDate);

        /// <summary>
        /// 审核客户信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="customerCode">客户代码</param>
        /// <param name="state">状态</param>
        /// <param name="confirmorID">审核人</param>
        /// <param name="confirmorRemark">审核批注</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ConfirmCustomerInfo(
            Guid id,
            string customerCode,
            CustomerCodeApplyState state,
            Guid confirmorID,
            string confirmorRemark,
            DateTime? updateDate);

        /// <summary>
        /// 获取客户备注列表
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <returns>返回客户备注列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerMemoList> GetCustomerMemoList(Guid customerID);

        /// <summary>
        /// 获取客户备注信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回备注信息</returns>
        [FunctionInfomation]
        [OperationContract]
        CustomerMemoInfo GetCustomerMemoInfo(Guid id);

        /// <summary>
        /// 保存客户备注
        /// </summary>
        /// <param name="customerID">客户</param>
        /// <param name="ids">ID</param>
        /// <param name="subjects">主题</param>
        /// <param name="contents">内容</param>
        /// <param name="types">类型</param>
        /// <param name="types">类型</param>
        /// <param name="prioritys">优先级</param>
        /// <param name="isShowUsers">显示用户</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveCustomerMemoInfo(
            Guid customerID,
            Guid?[] ids,
            string[] subjects,
            string[] contents,
            MemoType[] types,
            MemoPriority[] prioritys,
            bool[] isShowCustomers,
            bool[] isShowUsers,
            Guid saveByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 删除备注
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="updateDate">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveCustomerMemoInfo(
            Guid id,
             Guid removeByID,
            DateTime? updateDate);


        /// <summary>
        /// 获取客户合并列表
        /// </summary>
        /// <param name="mainCustomerID">主客户ID</param>
        /// <returns>返回</returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerCombineList> GetCustomerCombineList(Guid mainCustomerID);

        /// <summary>
        /// 获取合并客户ID列表 
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <returns>返回</returns>
        [FunctionInfomation]
        [OperationContract]
        List<Guid> GetCombineCustomerIDList(Guid customerID);

        /// <summary>
        /// 合并客户
        /// </summary>
        /// <param name="mainCustomerID">主客户</param>
        /// <param name="customerIDs">要被合并的客户</param>
        /// <param name="combineByID">合并人</param>
        /// <param name="updateDates">被何必客户的数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData CombineCustomers(
            Guid mainCustomerID,
            Guid[] customerIDs,
            Guid combineByID,
            DateTime?[] updateDates);


        /// <summary>
        /// 取消例外客户
        /// </summary>
        /// <param name="customerIDs">取消客户列表</param>
        /// <param name="companyid">所属公司</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="IsEnglish">是否英文</param>
        [FunctionInfomation]
        [OperationContract]
        void CancelExCustomer(
            Guid[] customerIDs,
            Guid companyid,
            Guid cancelByID,
            bool IsEnglish);


        /// <summary>
        /// 取消合并客户
        /// </summary>
        /// <param name="customerIDs">取消客户列表</param>
        /// <param name="cancelByID">取消人</param>
        /// <param name="updateDates">取消客户的数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData CancelCombineCustomers(
            Guid[] customerIDs,
            Guid cancelByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 设置/取消客户为危险客户
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isDangerous">是否危险客户</param>
        /// <param name="changeByID">更改人</param>
        /// <param name="memoContent">备注内容</param>
        /// <param name="updateDate">数据版本</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SetCustomerIsDangerous(
            Guid id,
            bool isDangerous,
            Guid changeByID,
            string memoContent,
            DateTime? updateDate);

        #region 获得客户发票台头信息
        /// <summary>
        /// 获得客户发票抬头列表
        /// </summary>
        /// <param name="customerID">顾客ID</param>
        /// <param name="companyID">所属公司</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetCustomerInvoiceTitleListByList")]
        List<CustomerInvoiceTitleInfo> GetCustomerInvoiceTitleList(Guid customerID, Guid companyID);

        /// <summary>
        /// 获得不重复（客户名称唯一）客户发票抬头列表
        /// </summary>
        /// <param name="companyID">公司ID</param>
        /// <param name="updateTime">更新时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetUNICustomerInvoiceTitleListByList")]
        List<CustomerInvoiceTitleInfo> GetUNICustomerInvoiceTitleList(Guid companyID, DateTime updateTime);

        /// <summary>
        /// 获得客户发票抬头列表
        /// </summary>
        /// <param name="customerID">顾客ID</param>
        /// <param name="invoiceTitle">发票抬头</param>
        /// <param name="companyID">所属公司</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetCustomerInvoiceTitleListByInfo")]
        List<CustomerInvoiceTitleInfo> GetCustomerInvoiceTitleList(Guid customerID, string invoiceTitle, Guid companyName);

        /// <summary>
        /// 获得客户发票信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "PubGetCustomerInvoiceTitleInfo")]
        CustomerInvoiceTitleInfo GetCustomerInvoiceTitleInfo(Guid id);

        /// <summary>
        /// 获得客户发票信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerInvoiceTitleInfo> GetCustomerInvoiceTitleListForFinder(string taxNo, string name, string companyID);
        #endregion

        #region 保存客户发票台头信息
        /// <summary>
        /// 保存客户发票抬头信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "PubSaveCustomerInvoiceTitleInfo")]
        SingleResultData SaveCustomerInvoiceTitleInfo(
                    Guid customerID,
                    Guid companyID,
                    Guid id,
                    string code,
                    CustomerInvoiceType invoiceType,
                    string name,
                    string taxNo,
                    string addressTel,
                    string bankAccountNo,
                    bool isValid,
                    Guid saveByID,
                    DateTime? updateDate);
        #endregion

        /// <summary>
        /// 根据客户ID和公司ID得到客户的月结信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        CustomerInfo GetMonthlyClosingEntriesForCustomer(Guid id, Guid companyID);
    }
}
