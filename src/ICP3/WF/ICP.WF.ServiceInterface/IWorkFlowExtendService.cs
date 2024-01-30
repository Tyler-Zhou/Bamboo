
namespace ICP.WF.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Sys.ServiceInterface.DataObjects;
    using ICP.TMS.ServiceInterface;

    using System.ServiceModel;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.FAM.ServiceInterface.DataObjects;

    /// <summary>
    /// 工作流程项目与其他模块交互的.通过该接口代理,
    /// 同时该接口提供一些其他服务
    /// 在ICP.WF.Activities中调用
    /// </summary>
    [ServiceInfomation("工作流扩展服务", ServiceType.Business)]
    [ServiceContract]
    public interface IWorkFlowExtendService
    {
       
        /// <summary>
        /// 获取指定用户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="sex">名称</param>
        /// <param name="jobId">职位ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="organizationId">部门ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">返回行数</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<UserList> GetUserList(string code, string name, ICP.Sys.ServiceInterface.DataObjects.GenderType? sex, Guid? jobId, Guid? roleId, Guid? organizationId, bool? isValid, int maxRecords);

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">返回行数</param>
        [FunctionInfomation()]
        [OperationContract]
        List<JobList> GetJobList(string code, string name, bool? isValid, int maxRecords);


        /// <summary>
        /// 获得全部的公司信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<OrganizationList> GetWFAllOffice();

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">返回行数</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<OrganizationList> GetOrganizationList(string code, string name, bool? isValid, int maxRecords);

        /// <summary>
        /// 获得用户的部门树列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<OrganizationList> GetOrganizationListByDepartment(Guid userID);

        /// <summary>
        /// 获得用户的公司树列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<OrganizationList> GetOrganizationListByUserCompany(Guid userID);

        /// <summary>
        /// 获得所有的公司树列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<OrganizationList> GetOrganizationListByCompany();


        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="code">代码</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        UserInfo GetUserInfo(string code);

        /// <summary>
        /// 获取部门详细信息
        /// </summary>
        /// <param name="ID">部门ID</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        OrganizationInfo GetOrganizationInfo(string ID);

        /// <summary>
        /// 获取职位详细信息
        /// </summary>
        /// <param name="code">职位代码</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        JobInfo GetJobInfo(string code);



        /// <summary>
        /// 获取可用管理费用项目列表
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<ChargingCodeList> GetChargeCodeList(Guid? groupID);

        /// <summary>
        /// 获得币种列表
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<CurrencyList> GetCurrencys();

        /// <summary>
        /// 获得客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="returnProperty"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        string GetCustomerInfo(Guid id, string returnProperty);



        /// <summary>
        /// 获得指定部门下的职位列表
        /// </summary>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<JobList> GetJobListByOrg(Guid departmentId);


        /// <summary>
        /// 获得指定的部门列表
        /// </summary>
        /// <param name="code">部门代码</param>
        /// <param name="cName">部门中文名称</param>
        /// <param name="eName">部门英文名称</param>
        /// <param name="fullName">部门全称</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<OrganizationList> GetStructures(string code, string cName, string eName, string fullName);

        /// <summary>
        /// 获取指定用户的部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<UserOrganizationTreeList> GetStructuresByUser(Guid userId);

        /// <summary>
        /// 获得用户详细信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        UserInfo GetUserInfoById(Guid id);

        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="returnProperty">返回字段类型</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        string GetUserInfoByIDProperty(Guid id, string returnProperty);


        /// <summary>
        /// 获得指定用户ID集合
        /// </summary>
        /// <param name="userIds">ID集合</param>
        /// <param name="userNames">用户名称集合</param>
        /// <param name="userCodes">用户代码集合</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        Guid[] GetUsersByIDs(Guid[] userIds, string[] userNames, string[] userCodes);
        

        /// <summary>
        /// 根据角色获得用户
        /// </summary>
        /// <param name="jobIds"></param>
        /// <param name="jobNames"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        Guid[] GetUsersByJob(Guid[] jobIds, string[] jobNames);

        /// <summary>
        /// 获得指定职位与部门下的用户
        /// </summary>
        /// <param name="orgID">部门ID</param>
        /// <param name="jobIds">职位ID集合</param>
        /// <param name="containsDescendants">包含子节点</param>
        /// <param name="organizationType">部门类型</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        Guid[] GetUsersByJobAndOrganization(string orgID, string[] jobIds, bool containsDescendants, OrganizationType organizationType);

           /// <summary>
        /// 根据获组织机构得用户ID集合
        /// </summary>
        /// <param name="ids">ID集合</param>
        /// <param name="names">名称集合</param>
        /// <param name="codes">代码集合</param>
        /// <param name="containsDescendants">是否包含下属单位</param>
        /// <param name="organizationType">机构类型</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        Guid[] GetUserByOrganization(Guid[] ids, string[] names, string[] codes, bool containsDescendants, OrganizationType organizationType);

        [FunctionInfomation()]
        [OperationContract]
        Boolean CheckOrganizationIDIsChildList(Guid checkID, Guid[] orgIds);

        /// <summary>
        /// 向指定用户发送邮件
        /// </summary>
        /// <param name="sender">发送人ID</param>
        /// <param name="receivers">接收人ID</param>
        /// <param name="subject">主题</param>
        /// <param name="conent">内容</param>
        [FunctionInfomation()]
        [OperationContract]
        void SendEMail(Guid sender, Guid[] receivers, string subject, string conent);

        /// <summary>
        /// 向指定用户发送消息
        /// </summary>
        /// <param name="receivers">收人ID集合</param>
        /// <param name="sender">发送人ID</param>
        /// <param name="message">消息内容</param>
        [FunctionInfomation()]
        [OperationContract]
        void SendMessage(Guid[] receivers, Guid? sender, string message);


        /// <summary>
        /// 获得车辆列表
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<TruckDataList> GetTruckList();

        /// <summary>
        /// 获得业务管理成本费用列表
        /// 管理成本14年调整为直接从会计科目中取出，所以此方法独立出来
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<CostItemData> GetCostItemList(Guid companyID);

        /// <summary>
        /// 获得影视公司的生产成本费用
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<CostItemData> GetMovieCostList();

        /// <summary>
        /// 获得所有的费用项目
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<CostItemData> GetAllCostItemList();

        #region 退佣
        /// <summary>
        /// 获得退佣日志
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<WFCommissionLogList> GetCommissionLogList(Guid[] operationIDs, bool isEnglish);


        /// <summary>
        /// 获得退佣业务纪录
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="blNo">提单号</param>
        /// <param name="containerNo">箱号</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="isApply">是否已申请</param>
        /// <param name="dataPage">分布集合</param>
        /// <param name="isEnglish">是否为英文版本</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        PageList GetCommissionBusinessList(
                            Guid userID,
                            Guid[] companyIDs,
                            string operationNo,
                            string blNo,
                            string containerNo,
                            string customerName,
                            bool? isApply,
                            DataPageInfo dataPage,
                            bool isEnglish);


        /// <summary>
        /// 获得业务列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        string GetCommissionBusinessNos(string[] ids);

        /// <summary>
        /// 获得退佣数据
        /// </summary>
        /// <param name="operationIDList"></param>
        /// <param name="customersID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        ComissionData GetWFComissionDataByCodition(List<Guid> operationIDList,List<Guid> currencyIDList,Guid customersID,bool isEnglish);

        #endregion

        #region 业务费用报销
        /// <summary>
        /// 获得业务费用报销日志列表
        /// </summary>
        /// <param name="customerTouchID"></param>
        /// <param name="isEnglish"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<WFCustomerExpenseLogList> GetWFCustomerExpenseLogList(Guid[] customerTouchIDs, bool isEnglish);

        /// <summary>
        /// 获得CRM客户信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="isenglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        WFCECRMCustomerList GetCRMCustomerInfo(Guid customerID, bool isenglish);

        /// <summary>
        /// 获得当前用户受益的CRM客户列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="code">代码</param>
        /// <param name="keyWord">关键字</param>
        /// <param name="cnmae">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="contact">联系人</param>
        /// <param name="email">邮箱</param>
        /// <param name="country">国家</param>
        /// <param name="maxrow">最大行数</param>
        /// <param name="isenglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<WFCECRMCustomerList> GetWFCRMCustomerList(Guid userID, string code, string keyWord, string cnmae, string ename, string contact, string email, string country, int maxrow, bool isenglish);
        /// <summary>
        /// 获得客户跟进纪录列表
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract]
        List<WFCECRMCustomerTouchLogList> GetCRMCustomerTouchLogList(Guid customerID, bool isEnglish);

        /// <summary>
        /// 获得FTP信息
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract(Name = "WFGetFTPServerConfig")]
        ICP.OA.ServiceInterface.DataObjects.FTPServerConfig GetFTPServerConfig();

        /// <summary>
        /// 获得公司列表
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract(Name="WFGetBankAccountNoList")]
        List<BankAccountList> GetBankAccountNoLis(Guid companyID);

        /// <summary>
        /// 获得会计科目列表
        /// </summary>
        /// <param name="solutionID"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract(Name = "WFGetSolutionGLCodeList")]
        List<SolutionGLCodeList> GetSolutionGLCodeList(Guid solutionID);

        /// <summary>
        /// 获得数据字典列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [FunctionInfomation()]
        [OperationContract(Name = "WFGetDataDictionaryList")]
        List<DataDictionaryList> GetDataDictionaryList(DataDictionaryType type);
        #endregion

    }
}
