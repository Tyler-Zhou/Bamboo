//-----------------------------------------------------------------------
// <copyright file="ISystemService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ServiceModel;
using System.Data;

namespace ICP.Sys.ServiceInterface
{


    /// <summary>
    /// 系统管理服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ISystemService
    {
        /// <summary>
        /// 获取网卡认证信息列表
        /// </summary>
        /// <param name="code">网卡地址</param>
        /// <param name="senderName">请求认证人</param>
        /// <param name="approverName">审批人</param>
        /// <param name="sendDateFrom">请求认证时间(开始）</param>
        /// <param name="sendDateTo">请求认证时间(结束)</param>
        /// <param name="approveDateFrom">审批时间(开始)</param>
        /// <param name="approveDateTo">审批时间 （结束）</param>
        /// <param name="state">状态</param>
        /// <param name="maxRecords">最大记录数(0:不限制,否则按指定记录返回)</param>
        /// <returns>返回网卡认证信息列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AuthcodeList> GetAuthCodeList(
            string code,
            string senderName,
            string approverName,
            DateTime? sendDateFrom,
            DateTime? sendDateTo,
            DateTime? approveDateFrom,
            DateTime? approveDateTo,
            bool? state,
            int maxRecords);

        /// <summary>
        /// 获取认证详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回认证详细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        AuthcodeInfo GetAuthCodeInfo(Guid id);

        /// <summary>
        /// 申请网卡认证信息
        /// </summary>
        /// <param name="authCode">网卡地址</param>
        /// <param name="senderId">发送人</param>
        /// <param name="sendDate">发送时间</param>
        /// <param name="senderRemark">申请人备注</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData RegisterAuthCodeInfo(
            string authCode,
            Guid senderId,
            DateTime sendDate,
            string senderRemark,
            DateTime? updateDate);

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="remark">备注</param>
        /// <param name="changeByID">修改人</param>
        /// <param name="updateDate">版本控制</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeAuthCodeState(
            Guid id,
            bool isValid,
            string remark,
            Guid changeByID,
            DateTime? updateDate);

        /// <summary>
        /// 获取错误日志列表
        /// </summary>
        /// <param name="projectName">项目名</param>
        /// <param name="senderName">发送人</param>
        /// <param name="approveName">审批人</param>
        /// <param name="sendDateFrom">发送时间-开始</param>
        /// <param name="sendDateTo">发送时间-结束</param>
        /// <param name="approveDateFrom">审批时间-开始</param>
        /// <param name="approveDateTo">审批时间-结束</param>
        /// <param name="maxRecords">最大记录数(0:不限制,否则按指定记录返回)</param>
        /// <returns>返回错误日志列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<ErrorLogList> GetErrorLogList(
            string projectName,
            string senderName,
            string approveName,
            DateTime? sendDateFrom,
            DateTime? sendDateTo,
            DateTime? approveDateFrom,
            DateTime? approveDateTo,
            int maxRecords);

        /// <summary>
        /// 获取错误日志详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回错误日志详细信息</returns>
        [FunctionInfomation]
        [OperationContract]
        ErrorLogInfo GetErrorLogInfo(Guid id);

        /// <summary>
        /// 向TFS添加反馈。
        /// </summary>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="area"></param>
        /// <param name="level"></param>
        /// <param name="mail"></param>
        /// <param name="attachmentID"></param>
        /// <param name="attachment"></param>
        [FunctionInfomation]
        [OperationContract]
        void AddFeedBack(
            string title,
            string desc,
            string area,
            string level,
            string mail,
            Guid? attachmentID,
            string attachmentName,
            byte[] attachment
            );

        /// <summary>
        /// 保存错误日志详细信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="projectName">项目模块名</param>
        /// <param name="errorContenxt">错误日志内容</param>
        /// <param name="senderId">发送人ID</param>
        /// <param name="sendDate">发送日期</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData SaveErrorLogInfo(
            Guid? id,
            string projectName,
            string errorContenxt,
            Guid senderId,
            DateTime sendDate,
            DateTime? updateDate);

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="projectName">项目模块名</param>
        /// <param name="errorContenxt">错误日志内容</param>
        /// <param name="senderId">发送人ID</param>
        /// <param name="sendDate">发送日期</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData WriteErrorLog(
            string projectName,
            string errorContenxt,
            Guid senderId,
            DateTime sendDate,
            DateTime? updateDate);

        /// <summary>
        /// 改变状态
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="remark">备注</param>
        /// <param name="changeById">修改人ID</param>
        /// <param name="updateDate">版本(控制并发冲突)</param>
        /// <returns>返回SingleResultData</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResultData ChangeErrorLogState(
            Guid id,
            bool isValid,
            string remark,
            Guid changeById,
            DateTime? updateDate);

        /// <summary>
        /// 插入解锁数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="refID"></param>
        /// <param name="isValid"></param>
        /// <param name="saveByID"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        void SaveUntieLockInfo(
                            UntieLockType type,
                            Guid[] refIDs,                          
                            Guid saveByID);
        [FunctionInfomation]
        [OperationContract]
        DataSet GetDataSet(string sql);

        void ExecSql(string sql);

    }
}