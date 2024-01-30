namespace ICP.FCM.Common.ServiceInterface
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Common;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using System.ServiceModel;
    /// <summary>
    /// 申请代理接口
    /// </summary>
    [ServiceInfomation("申请代理接口")]
    [ServiceContract]
    public interface IAgentRequestService
    {
        #region AgentRequest

        /// <summary>
        /// 获取代理申请
        /// </summary>
        /// <param name="agentRequestID">agentRequestID</param>
        /// <returns>返回代理申请数据</returns>
        [FunctionInfomation]
        [OperationContract]
        AgentRequestInfo GetAgentRequestInfo(Guid agentRequestID);

        /// <summary>
        /// 获取代理申请列表
        /// </summary>
        /// <param name="opeartionNO">业务号</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="agentType">申请代理类型</param>
        /// <param name="agentState">申请代理状态</param>
        /// <param name="solverID">指定人</param>
        /// <param name="sendFrom">申请开始日期</param>
        /// <param name="sendTo">申请结束日期</param>
        /// <param name="maxRecords">最大行数</param>
        /// <returns>返回代理申请列表</returns>        
        [FunctionInfomation]
        [OperationContract]
        List<AgentRequestInfo> GetAgentRequestList(
                                                     string opeartionNO
                                                    , ICP.Framework.CommonLibrary.Client.OperationType? operationType
                                                    , AgentType? agentType
                                                    , AgentRequestStateEnum? agentState
                                                    , Guid? solverID
                                                    , DateTime? sendFrom
                                                    , DateTime? sendTo
                                                    , int maxRecords);
        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="senderID">申请人</param>
        /// <param name="senderDate">申请时间</param>
        /// <param name="agentType">代理类型(0:普通,1:第三方代理,2:需要对收款有特殊要求的代理)</param>
        /// <param name="senderRemark">申请人备注</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult RequestOceanAgent(
            Guid operationID,
            ICP.Framework.CommonLibrary.Client.OperationType operationType,
            Guid senderID,
            DateTime senderDate,
            AgentType agentType,
            string senderRemark,
            DateTime? updateDate);

        /// <summary>
        /// 打回代理申请
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="solverID">打回人</param>
        /// <param name="solveDate">打回时间</param>
        /// <param name="solverRemark">打回备注</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult RejectAgentRequest(
            Guid id,
            Guid solverID,
            DateTime solveDate,
            string solverRemark,
            DateTime? updateDate);

        /// <summary>
        /// 回复代理申请
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="solverID">回复人</param>
        /// <param name="solveDate">回复时间</param>
        /// <param name="solverRemark">回复备注</param>
        /// <param name="agentID">代理ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult AnswerAgentRequest(
            Guid id,
            Guid solverID,
            DateTime solveDate,
            string solverRemark,
            Guid? agentID,
            DateTime? updateDate);

        /// <summary>
        /// 删除代理申请
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        [FunctionInfomation]
        [OperationContract]
        void RemoveAgentRequest(
            Guid id,
            Guid removeByID,
            DateTime? updateDate);

        #endregion
    }
}
