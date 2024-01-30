namespace ICP.FCM.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using ICP.FCM.Common.ServiceInterface;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using CommonData = ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Common;
    using ICP.Framework.CommonLibrary.Helper;
    using System.Data;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    public partial class FCMCommonService
    {
        #region AgentRequest

        /// <summary>
        /// 获取代理申请
        /// </summary>
        /// <param name="agentRequestID">agentRequestID</param>
        /// <returns>返回代理申请数据</returns>
        public AgentRequestInfo GetAgentRequestInfo(Guid agentRequestID)
        {
            ArgumentHelper.AssertGuidNotEmpty(agentRequestID, "id");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAgentRequestInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, agentRequestID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                AgentRequestInfo result = (from b in ds.Tables[0].AsEnumerable()
                                           select new AgentRequestInfo
                                           {
                                               OperationNo = b.Field<string>("OperationNo"),
                                               POD = b.Field<string>("POD"),
                                               SenderID = b.Field<Guid>("SenderID"),
                                               SenderRemark = b.Field<string>("SenderRemark"),
                                               SolverID = b.Field<Guid?>("SolverID"),
                                               SolverRemark = b.Field<string>("SolverRemark"),
                                               AgentID = b.Field<Guid?>("AgentID"),
                                               AgentName = b.Field<string>("AgentName"),
                                               ID = b.Field<Guid>("ID"),
                                               OperationID = b.Field<Guid>("OperationID"),
                                               SenderName = b.Field<string>("SenderName"),
                                               SendDate = b.Field<DateTime>("SendDate"),
                                               Type = (AgentType)b.Field<byte>("Type"),
                                               SolverName = b.Field<string>("SolverName"),
                                               SolveDate = b.Field<DateTime?>("SolveDate"),
                                               State = (AgentRequestStateEnum)b.Field<byte>("State"),
                                               UpdateDate=b.Field<DateTime?>("UpdateDate"),
                                               IsDirty = false
                                           }).SingleOrDefault();

                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取代理申请列表
        /// </summary>
        /// <param name="opeartionNO">业务号</param>
        /// <param name="type">申请代理类型</param>
        /// <param name="state">申请代理状态</param>
        /// <param name="solverID">指定人</param>
        /// <param name="sendFrom">申请开始日期</param>
        /// <param name="sendTo">申请结束日期</param>
        /// <returns>返回代理申请列表</returns>        
        public List<AgentRequestInfo> GetAgentRequestList(
                                                             string opeartionNO
                                                            , ICP.Framework.CommonLibrary.Client.OperationType? operationType
                                                            , AgentType? agentType
                                                            , AgentRequestStateEnum? agentState
                                                            , Guid? solverID
                                                            , DateTime? sendFrom
                                                            , DateTime? sendTo
                                                            , int maxRecords)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetAgentRequestList");

                db.AddInParameter(dbCommand, "@OpeartionNO", DbType.String, opeartionNO);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType);
                db.AddInParameter(dbCommand, "@AgentType", DbType.Int16, agentType);
                db.AddInParameter(dbCommand, "@AgentRequestState", DbType.Int16, agentState);
                db.AddInParameter(dbCommand, "@SolverID", DbType.Guid, solverID);
                db.AddInParameter(dbCommand, "@SendFromDate", DbType.Date, sendFrom);
                db.AddInParameter(dbCommand, "@SendToDate", DbType.Date, sendTo);
                db.AddInParameter(dbCommand, "@MaxRecords", DbType.Int32, maxRecords);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<AgentRequestInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new AgentRequestInfo
                                                  {
                                                      OperationNo = b.Field<string>("OperationNo"),
                                                      POD = b.Field<string>("POD"),
                                                      SenderID = b.Field<Guid>("SenderID"),
                                                      SenderRemark = b.Field<string>("SenderRemark"),
                                                      SolverID = b.Field<Guid?>("SolverID"),
                                                      SolverRemark = b.Field<string>("SolverRemark"),
                                                      AgentID = b.Field<Guid?>("AgentID"),
                                                      AgentName = b.Field<string>("AgentName"),
                                                      ID = b.Field<Guid>("ID"),
                                                      OperationID = b.Field<Guid>("OceanShippingOrderID"),
                                                      SenderName = b.Field<string>("SenderName"),
                                                      SendDate = b.Field<DateTime>("SendDate"),
                                                      Type = (AgentType)b.Field<byte>("Type"),
                                                      SolverName = b.Field<string>("SolverName"),
                                                      SolveDate = b.Field<DateTime?>("SolveDate"),
                                                      UpdateDate = b.Field<DateTime?>("UpdateDate"),
                                                      State = (AgentRequestStateEnum)b.Field<byte>("State"),
                                                      IsDirty = false,
                                                  }).ToList();

                return results;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型</param>
        /// <param name="senderID">申请人</param>
        /// <param name="senderDate">申请时间</param>
        /// <param name="agentType">代理类型(0:普通,1:第三方代理,2:需要对收款有特殊要求的代理)</param>
        /// <param name="senderRemark">申请人备注</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult RequestOceanAgent(
            Guid operationID,
            ICP.Framework.CommonLibrary.Client.OperationType operationType,
            Guid senderID,
            DateTime senderDate,
            AgentType agentType,
            string senderRemark,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");
            ArgumentHelper.AssertGuidNotEmpty(senderID, "senderID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSaveRequestAgent");

                //db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType);
                db.AddInParameter(dbCommand, "@SenderID", DbType.Guid, senderID);
                db.AddInParameter(dbCommand, "@SenderDate", DbType.DateTime, senderDate);
                db.AddInParameter(dbCommand, "@AgentType", DbType.Int16, agentType);
                db.AddInParameter(dbCommand, "@SenderRemark", DbType.String, senderRemark);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
        public SingleResult AnswerAgentRequest(
            Guid id,
            Guid solverID,
            DateTime solverDate,
            string solverRemark,
            Guid? agentID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(solverID, "solverID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspAnswerAgentRequest");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@SolverID", DbType.Guid, solverID);
                db.AddInParameter(dbCommand, "@SolveDate", DbType.DateTime, solverDate);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@SolverRemark", DbType.String, solverRemark);
                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, agentID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 打回代理申请
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="solverID">打回人</param>
        /// <param name="solveDate">打回时间</param>
        /// <param name="solverRemark">打回备注</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public SingleResult RejectAgentRequest(
            Guid id,
            Guid solverID,
            DateTime solveDate,
            string solverRemark,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(solverID, "solverID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspAnswerAgentRequest");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@SolverID", DbType.Guid, solverID);
                db.AddInParameter(dbCommand, "@SolveDate", DbType.DateTime, solveDate);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@AgentID", DbType.Guid, null);
                db.AddInParameter(dbCommand, "@SolverRemark", DbType.String, solverRemark);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除代理申请
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public void RemoveAgentRequest(
           Guid id,
           Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            ArgumentHelper.AssertGuidNotEmpty(removeByID, "removeByID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveAgentRequest");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                dbCommand.ExecuteNonQuery();
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
