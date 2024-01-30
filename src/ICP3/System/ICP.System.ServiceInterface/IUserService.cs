//-----------------------------------------------------------------------
// <copyright file="IUserService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ServiceModel;

namespace ICP.Sys.ServiceInterface
{
    /// <summary>
    /// 用户管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="identityCard">身份证</param>
        /// <returns>bool</returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsExistUser(
            string code,
            string identityCard);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="name">名称</param>
        /// <param name="gender">性别</param>
        /// <param name="jobId">职位ID</param>
        /// <param name="roleId">角色ID</param>
        /// <param name="organizationId">部门ID</param>
        /// <param name="hasJob">是否有职位</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数(0:不限制,否则按指定记录返回)</param>
        /// <returns>返回用户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserList> GetUserListByList(
            string code,
            string name,
            GenderType? gender,
            Guid? jobId,
            Guid? roleId,
            Guid? organizationId,
            bool? hasJob,
            bool? isValid,
            int maxRecords);


        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="organizationId">部门Id</param>
        /// <param name="roleName">角色名</param>
        /// <param name="jobName">职位名</param>
        /// <param name="hasJob">是否有职位</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回用户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserList> GetUserListBySearch(
            Guid organizationId,
            string roleName,
            string jobName,
            bool? hasJob,
            bool? isValid,
            int maxRecords);

        /// <summary>
        /// 获取指定部门下职位的下属用户列表(默认是有职位的用户)
        /// </summary>
        /// <param name="organizationIDs">部门ID</param>
        /// <param name="jobNames">职位名</param>
        /// <param name="roleNames">角色名</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回用户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserList> GetUnderlingUserList(
            Guid[] organizationIDs,
            string[] jobNames,
            string[] roleNames,
            bool? isValid);


        /// <summary>
        /// 获取组织架构下的用户列表(通过部门IDs、职位、角色)
        /// </summary>
        /// <param name="organizationIDs">部门ID</param>
        /// <param name="jobNames">职位名</param>
        /// <param name="roleNames">角色名</param>
        /// <param name="isValid">是否有效</param>
        /// <returns>返回用户列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserList> GetOrganizationJobNameUserList(
            Guid[] organizationIDs,
            string[] jobNames,
            string[] roleNames,
            bool? isValid);


        /// <summary>
        /// 获得指定用户的下属列表
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserList> GetSubordinateUserList(
            Guid userID,
            bool? isValid);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回用户详细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        UserInfo GetUserInfo(Guid id);

        /// <summary>
        /// 通过邮箱获取用户详细信息
        /// </summary>
        /// <param name="address">address</param>
        /// <returns>返回用户邮箱详细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        UserInfoEmail GetUserInfoByEmailAddress(string address);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="code">代码</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="gender">性别</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="mobile">移动电话</param>
        /// <param name="saveById">修改人Id</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveUserInfo(
            Guid? id,
            string code,
            string cname,
            string ename,
            GenderType gender,
            string tel,
            string fax,
            string mobile,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回用户详细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        UserDetailInfo GetUserDetailInfo(Guid id);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>返回所有用户集合</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ICP.ReportCenter.ServiceInterface.DataObjects.UserDetailInfo> GetAllUsersDetailInfo(Guid id);

        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>返回所有用户集合</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserDetailInfo> GetAllUsersDetailInfo_UserService(Guid id);

        /// <summary>
        /// 获取通讯录列表
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject GetAllContactList(Guid organizationID);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="id">id==null或则id==Guid.Empty则新增数据,否则修改对应键的信息</param>
        /// <param name="cname">中文名称</param>
        /// <param name="ename">英文名称</param>
        /// <param name="gender">性别</param>
        /// <param name="tel">电话</param>
        /// <param name="fax">传真</param>
        /// <param name="mobile">移动电话</param>
        /// <param name="saveById">修改人Id</param>
        /// <param name="address">地址</param>
        /// <param name="birthday">birthday</param>
        /// <param name="remark">remark</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveUserDetailInfo(
            Guid id,
            string cname,
            string ename,
            GenderType gender,
            string tel,
            string fax,
            string mobile,
            string address,
            string remark,
            DateTime? birthday,
            Guid saveById,
            DateTime? updateDate);

        /// <summary>
        /// 获取FTP服务器信息
        /// </summary>
        /// <returns>返回</returns>
        [FunctionInfomation]
        [OperationContract]
        FTPServerConfig GetFTPServerConfig();

        /// <summary>
        /// 改变密码
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="oldPassword">oldPassword</param>
        /// <param name="newPassword">password</param>
        /// <param name="updateDate">updateDate</param>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangedUserPassword(Guid id, string oldPassword, string newPassword, DateTime? updateDate);


        /// <summary>
        /// 设置用户默认邮件帐号
        /// </summary>
        /// <param name="userMailAccountID">邮件帐号ID</param>
        /// <param name="setById">设置人ID</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetUserDefaultMailAccount(
            Guid userMailAccountID,
            Guid setById);

        /// <summary>
        /// 改变邮箱密码
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="newPassword">password</param>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangedUserMailPassword(Guid userid, string newPassword);



        /// <summary>
        /// 保存用户邮件帐号
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="ids">邮件帐号ID</param>
        /// <param name="emails">邮件地址</param>
        /// <param name="mailIncomingProtocols">接收邮件服务器协议</param>
        /// <param name="mailIncomingHosts">接收邮件服务器</param>
        /// <param name="mailIncomingPorts">接收邮件服务器端口</param>
        /// <param name="mailIncomingLogins">接收邮件用户名</param>
        /// <param name="mailIncomingPasswords">接收邮件用户密码</param>
        /// <param name="mailOutgoingProtocols">发送邮件服务器协议</param>
        /// <param name="mailOutgoingHosts">发送邮件服务器</param>
        /// <param name="mailOutgoingPorts">发送邮件服务器端口</param>
        /// <param name="mailOutgoingLogins">发送邮件用户名</param>
        /// <param name="mailOutgoingPasswords">发送邮件用户密码</param>
        /// <param name="friendlyNames">用户昵称</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="getMailAtLogins">是否登录后就接收邮件</param>
        /// <param name="updateDates">数据版本</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SaveUserMailAccountInfo(
            Guid userID,
            Guid[] ids,
            string[] emails,
            MailProtocol[] mailIncomingProtocols,
            string[] mailIncomingHosts,
            int[] mailIncomingPorts,
            string[] mailIncomingLogins,
            string[] mailIncomingPasswords,
            MailProtocol[] mailOutgoingProtocols,
            string[] mailOutgoingHosts,
            int[] mailOutgoingPorts,
            string[] mailOutgoingLogins,
            string[] mailOutgoingPasswords,
            string[] friendlyNames,
            bool[] getMailAtLogins,
            Guid saveByID,
            DateTime?[] updateDates);

        /// <summary>
        /// 删除邮件帐号
        /// </summary>
        /// <param name="ids">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDates">数据版本</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveUserMailAccount(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid removeByID);

        /// <summary>
        /// 获取邮件列表
        /// </summary>
        /// <param name="organizationID">organizationID</param>
        /// <param name="jobID">jobID</param>
        /// <param name="roleID">roleID</param>
        /// <param name="userName">userName</param>
        /// <param name="email">email</param>
        /// <param name="isValid">isValid</param>
        /// <param name="maxRecords">maxRecords</param>
        /// <returns>MailAccount</returns>
        [FunctionInfomation]
        [OperationContract]
        List<MailAccount> GetMailAccountList(
            Guid? organizationID,
            Guid? jobID,
            Guid? roleID,
            string userName,
            string email,
            bool? isValid,
            int maxRecords);


        /// <summary>
        /// 获取用户的邮件帐号列表
        /// </summary>
        /// <param name="userIDs">用户ID</param>
        /// <returns>返回用户的邮件帐号列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserMailAccountList> GetUserMailAccountList(Guid[] userIDs);

        /// <summary>
        /// 更改用户状态
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">修改人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeUserState(
            Guid userId,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);


        /// <summary>
        /// 根据保留用户ID获取被合并用户的日志列表
        /// </summary>
        /// <param name="preserveUserId">保留用户ID</param>
        /// <returns>获取被合并用户的日志列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserList> GetMergeUserList(Guid preserveUserId);


        /// <summary>
        /// 合并用户
        /// </summary>
        /// <param name="mergeUserIds">要被合并的用户列表ID</param>
        /// <param name="preserveUserId">保留的用户Id</param>
        /// <param name="mergeById">合并人ID</param>
        /// <param name="updateDates">版本(控制并发冲突)</param>
        /// <returns>/返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData MergeUser(
            Guid[] mergeUserIds,
            Guid preserveUserId,
            Guid mergeById,
            DateTime?[] updateDates);


        /// <summary>
        /// 取消合并用户
        /// </summary>
        /// <param name="ids">用户ID列表</param>
        /// <param name="cancelById">取消人ID</param>
        /// <param name="updateDates">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData CancelMergeUser(
            Guid[] ids,
            DateTime?[] updateDates,
            Guid cancelById);


        /// <summary>
        /// 获取用户所挂公司列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="type">类型</param>
        /// <returns>返回用户所挂公司列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OrganizationList> GetUserCompanyList(Guid userId, OrganizationType? type);

        /// <summary>
        /// 获取用户所挂公司树列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户所挂公司树列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserOrganizationTreeList> GetUserOrganizationTreeList(Guid userId);


        /// <summary>
        /// 获取用户职位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户职位列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<User2OrganizationJobList> GetUser2OrganizationJobList(Guid userId);


        /// <summary>
        /// 设置用户职位
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="ids">用户职位关系列表</param>
        /// <param name="organizationJobIds">组织结构列表</param>
        /// <param name="setById">修改人ID </param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetUserOrganizationJob(
            Guid userId,
            Guid[] ids,
            Guid[] organizationJobIds,
            bool[] isDefaults,
            Guid setById);

        /// <summary>
        /// 设置默认职位
        /// </summary>
        /// <param name="userOrganizationJobId">用户职位关系ID</param>
        /// <param name="setById">设置人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回ManyResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResultData SetUserDefaultOrganizationJob(
            Guid userOrganizationJobId,
            Guid setById,
            DateTime? updateDate);

        /// <summary>
        /// 删除用户职位
        /// </summary>
        /// <param name="userOrganizationJobIds">用户职位关系列表</param>
        /// <param name="updateDates">版本(控制并发冲突)</param>
        /// <param name="removeById">删除人ID</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        void RemoveUserOrganizationJob(
            Guid[] userOrganizationJobIds,
            DateTime?[] updateDates,
            Guid removeById);




        /// <summary>
        /// 获取用户交接列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="isEnglsih">是否英文</param>
        /// <param name="maxRecords">最大记录数</param>
        /// <returns>返回用户交接列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<UserConnectionList> GetUserConnectionList(
            Guid userId,
            DateTime fromDate,
            DateTime toDate,
            bool? isValid,
            bool isEnglsih,
            int maxRecords);


        /// <summary>
        /// 获取接交日志详细信息
        /// </summary>
        /// <param name="userConnectionLogId">交接日志ID</param>
        /// <returns>返回交接日志列表</returns>
        [FunctionInfomation]
        [OperationContract]
        UserConnectionInfo GetUserConnectionInfo(Guid userConnectionLogId);


        /// <summary>
        /// 交接用户工作
        /// </summary>
        /// <param name="id">如果id==null或则id==Guid.Empty则新增，否则修改</param>
        /// <param name="fromUserId">交接人</param>
        /// <param name="toUserId">接替工作人</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="setById">设置人</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData UserConnection(
            Guid? id,
            Guid fromUserId,
            Guid toUserId,
            DateTime fromDate,
            DateTime toDate,
            Guid setById,
            DateTime? updateDate);


        /// <summary>
        /// 改变交接状态
        /// </summary>
        /// <param name="userConnectionId">交接日志ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="changeById">改变人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>/返回SingleResultData </returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeUserConnectionState(
            Guid userConnectionId,
            bool isValid,
            Guid changeById,
            DateTime? updateDate);


        /// <summary>
        /// 判断两个用户是否上下属关系
        /// </summary>
        /// <param name="underlingUserId">下属用户ID</param>
        /// <param name="seniorUserId">上司用户ID</param>
        /// <returns>如果是上下属关系返回true，否则返回fale</returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsUnderlingUser(
            Guid underlingUserId,
            Guid seniorUserId);

        /// <summary>
        ///    检测指定用户的账号是否被其它用户使用
        /// </summary>
        /// <param name="userId">   用户ID</param>
        /// <param name="code">    账号</param>
        /// <returns>   如果存在返回true,否则返回false</returns>
        [FunctionInfomation]
        [OperationContract]
        bool CheckUserCodeExsit(
            Guid userId,
            string code);


        /// <summary>
        /// 更改用户密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <param name="changeById">更改人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeUserPassword(
            Guid userId,
            string oldPassword,
            string newPassword,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 工作流新增用户
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="sex">性别</param>
        /// <param name="organizationID">部门</param>
        /// <param name="jobID">职位</param>
        /// <param name="saveByID"><保存人/param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <param name="birthday">生日</param>
        /// <param name="culture">学历</param>
        /// <param name="emolumentDate">入职计薪日期</param>
        /// <param name="familyAddress">户口所在地</param>
        /// <param name="nativePlace">籍贯</param>
        /// <param name="identityCard">身份证号</param>
        /// <param name="specialty">专业</param>
        /// <param name="email">邮箱</param>
        /// <param name="ename">英文名</param>
        /// <param name="phone">手机</param>
        /// <param name="tel">电话</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveUserInfoForWF(
                     string name,
                     string ename,
                     string tel,
                     string phone,
                     string email,
                     string sex,
                     Guid organizationID,
                     Guid jobID,
                     DateTime? birthday,
                     string nativePlace,
                     string familyAddress,
                     string culture,
                     string specialty,
                     string identityCard,
                     DateTime? emolumentDate,
                     Guid saveByID);

        /// <summary>
        /// 根据用户邮箱名获取用户邮箱密码
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        [OperationContract]
        string GetUserEmailPassword(string emailAddress);

        [OperationContract]
        ManyResult GetUserNamesByID(Guid[] userIds);

        /// <summary>
        /// 根据部门名称得到UserList
        /// </summary>
        /// <param name="OperationId"></param>
        /// <returns></returns>
        [OperationContract]
        List<UserList> GetOverseasSalesList(string GetUserListName);

        /// <summary>
        /// 返回当前人员的上级
        /// </summary>
        /// <param name="UserID">当前人员</param>
        /// <returns></returns>
        [OperationContract]
        List<UserDetailInfo> GetSuperior(Guid UserID);

         /// </summary>
        /// 保存邮件签名
        /// </summary>
        /// <param name="accountID">邮箱ID</param>
        /// <param name="type">签名类型</param>
        /// <param name="signature">签名</param>
        [FunctionInfomation]
        [OperationContract]
        void SaveEmailAccountSignature(Guid accountID, int type, string signature, Guid savebyid);

        /// </summary>
        /// 获取邮箱签名
        /// </summary>
        /// <param name="releaseid">邮箱ID</param>
        [FunctionInfomation]
        [OperationContract]
        List<EmailAccountSignature> GetEmailAccountSignature(Guid? AccountID, string EmailAddress);
    }
}