using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceComponent.JSONObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Transactions;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 备注服务
    /// </summary>
    public partial class FCMCommonService
    {
        #region 成员
        /// <summary>
        /// 缓存事件对象集
        /// </summary>
        private static List<EventObjects> _CacheEventObjects = null;
        #endregion

        #region 获取已完成的事件
        /// <summary>
        /// 获取已完成的事件
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="ownerID"></param>
        /// <returns></returns>
        public List<EventObjects> GetMemoList(Guid operationID, Guid? ownerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationMemoList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@GetMemoType", DbType.Int16, DataSearchType.Local);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<EventObjects> results = ReturnEventObjects(ds);
                List<EventObjects> Thereare = results.Where(n => n.EventIndex != 0).ToList();
                if (Thereare.Any())
                {
                    results = results.OrderBy(n => n.EventIndex).ToList();
                }
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
        #endregion

        #region 获取已完成的事件
        /// <summary>
        /// 获取已完成的事件
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="eventType">事件类型(本地/代理/所有)</param>
        /// <param name="ownerID"></param>
        /// <returns></returns>
        public List<EventObjects> GetMemoList(Guid operationID, DataSearchType eventType, Guid? ownerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");

            try
            {
                DataSet dsResult = null;
                DataSet dds = null;
                DataSet sds = null;
                dds = GetMemoDataSet(operationID, eventType == DataSearchType.ALL ? DataSearchType.Local : eventType, eventType == DataSearchType.Agent ? null : (bool?)true);
                sds = GetMemoDataSet(operationID, eventType == DataSearchType.ALL ? DataSearchType.Agent : eventType, eventType == DataSearchType.Local ? null : (bool?)false);
                dsResult = DataSetHelper.MergeSet(dds, sds);
                if (dsResult == null || dsResult.Tables.Count < 1)
                {
                    return null;
                }
                List<EventObjects> results = ReturnEventObjects(dsResult);
                List<EventObjects> Thereare = results.Where(n => n.EventIndex != 0).ToList();
                if (Thereare.Any())
                {
                    results = results.OrderBy(n => n.EventIndex).ToList();
                }
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
        #endregion

        #region 获取已完成的事件
        /// <summary>
        /// 获取已完成的事件
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="eventType">事件类型</param>
        /// <param name="isDefaultDB">是否默认数据库</param>
        /// <returns></returns>
        DataSet GetMemoDataSet(Guid operationID, DataSearchType eventType, bool? isDefaultDB)
        {
            if (isDefaultDB == null)
                return null;
            try
            {
                Database db = DatabaseFactoryHelper.CreateDatabase(isDefaultDB.Value);
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationMemoList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@GetMemoType", DbType.Int16, eventType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                return ds;
            }
            catch (SqlException sqlException)
            {
                if (eventType == DataSearchType.ALL && !isDefaultDB.Value)
                    throw new ApplicationException(string.Format(IsEnglish ? "Proxy server exception occurred:{0}" : "代理服务器发生异常：{0}", sqlException.Message));
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 返回当前事件列表数据
        /// <summary>
        /// 返回当前事件列表数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public List<EventObjects> ReturnEventObjects(DataSet ds)
        {
            List<EventObjects> results = (from b in ds.Tables[0].AsEnumerable()
                                          select new EventObjects
                                          {
                                              Id = b.Field<Guid>("ID"),
                                              OperationID = b.Field<Guid>("OperationID"),
                                              EventID = b.IsNull("EventID") ? Guid.Empty : b.Field<Guid?>("EventID"),
                                              Type = b.IsNull("Type") ? (MemoType)1 : (MemoType)b.Field<Byte>("Type"),
                                              Code = b.IsNull("Code") ? string.Empty : b.Field<String>("Code").Trim(),
                                              Subject = b.IsNull("Subject") ? string.Empty : b.Field<String>("Subject").Trim(),
                                              Description = b.IsNull("Content") ? string.Empty : b.Field<String>("Content"),
                                              Owner = string.IsNullOrEmpty(b.Field<String>("Owner")) ? string.Empty : b.Field<String>("Owner"),
                                              CategoryName = string.IsNullOrEmpty(b.Field<String>("Category")) ? string.Empty : b.Field<String>("Category").Trim(),
                                              UpdateDate = b.Field<DateTime?>("Updatedate"),
                                              CreateDate = b.Field<DateTime?>("CreateDate"),
                                              IsShowAgent = b.Field<bool>("IsShowAgent"),
                                              IsShowCustomer = b.Field<bool>("IsShowCustomer"),
                                              FormID = b.IsNull("FormID") ? Guid.Empty : b.Field<Guid>("FormID"),
                                              FormType = b.IsNull("FormType") ? (FormType)0 : (FormType)b.Field<Byte>("FormType"),
                                              MessageID = b.IsNull("MessageID") ? Guid.Empty : b.Field<Guid>("MessageID"),
                                              ShowImage = b.IsNull("Type") ? 0 : b.Field<Byte>("Type"),
                                              OccurrenceTime = b.IsNull("OccurrenceTime") ? b.Field<DateTime?>("CreateDate") : b.Field<DateTime?>("OccurrenceTime"),
                                              Logged = b.IsNull("Logged") ? true : b.Field<bool>("Logged"),
                                              Important = b.IsNull("Important") ? false : b.Field<bool>("Important"),
                                              EventIndex = b.IsNull("EventIndex") ? 0 : b.Field<int>("EventIndex"),
                                              ManualImportant = b.IsNull("ManualImportant") ? false : b.Field<bool>("ManualImportant"),
                                              Required = b.IsNull("IsRequired") ? false : b.Field<bool>("IsRequired")
                                          }).ToList();
            if (_CacheEventObjects == null)
            {
                _CacheEventObjects = GetOperationEvents();
            }
            foreach (var result in results)
            {
                var re = _CacheEventObjects.FirstOrDefault(n => n.Code == result.Code);
                if (re != null)
                {
                    result.Subject = re.Description;
                }
                if (result.MessageID == Guid.Empty)
                {
                    result.ShowImage = -1;
                }
                if (result.MessageID != Guid.Empty && result.Type == MemoType.Manually)
                {
                    //显示类型为邮件图标
                    result.ShowImage = 4;
                }
            }
            return results;
        }
        #endregion

        #region 返回事件列表的信息
        /// <summary>
        /// 返回事件列表的信息
        /// </summary>
        /// <returns></returns>
        public List<EventObjects> GetOperationEvents()
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationEvents");
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                List<EventObjects> results = (from b in ds.Tables[0].AsEnumerable()
                                              select new EventObjects
                                              {
                                                  Code = b.IsNull("Code") ? string.Empty : b.Field<string>("Code").Trim(),
                                                  Description = b.IsNull("Subject") ? string.Empty : b.Field<string>("Subject").Trim()
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
        #endregion

        #region 根据业务的ID和代码ID返回最新的备注记录
        /// <summary>
        /// 根据业务的ID和代码ID返回最新的备注记录
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="operationEventId">代码ID</param>
        /// <returns></returns>
        public EventObjects GetMemoFirst(Guid operationId, Guid operationEventId)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationMemoContent");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@OperationEventID", DbType.Guid, operationEventId);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }
                EventObjects results = (from b in ds.Tables[0].AsEnumerable()
                                        select new EventObjects
                                        {
                                            Id = b.Field<Guid>("ID"),
                                            OperationID = b.Field<Guid>("OperationID"),
                                            EventID = b.Field<Guid?>("EventID"),
                                            Type = (MemoType)b.Field<Byte>("Type"),
                                            Code = b.IsNull("Code") ? string.Empty : b.Field<String>("Code").Trim(),
                                            Subject = b.IsNull("Subject") ? string.Empty : b.Field<String>("Subject").Trim(),
                                            Description = b.IsNull("Content") ? string.Empty : b.Field<String>("Content").Trim(),
                                            Owner = b.Field<String>("Owner"),
                                            CategoryName = b.Field<String>("Category"),
                                            UpdateDate = b.Field<DateTime?>("Updatedate"),
                                            CreateDate = b.Field<DateTime>("CreateDate"),
                                            IsShowAgent = b.Field<bool>("IsShowAgent"),
                                            IsShowCustomer = b.Field<bool>("IsShowCustomer"),
                                            FormID = b.Field<Guid>("FormID"),
                                            FormType =
                                                           (FormType)
                                                           b.Field<Byte>("FormType")
                                            //MemoID = b.Field<Guid>("MemoID")
                                        }).FirstOrDefault();
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
        #endregion

        #region 保存事件
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="eventObjects"></param>
        public SingleResult SaveMemoInfo(EventObjects eventObjects)
        {
            ArgumentHelper.AssertGuidNotEmpty(eventObjects.OperationID, "operationID");
            ArgumentHelper.AssertGuidNotEmpty(eventObjects.UpdateBy, "saveByID");
            try
            {
                SingleResult result = null;
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;

                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveOperationMemoInfo]");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, eventObjects.OperationID);   //业务ID
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, eventObjects.OperationType.GetHashCode());
                db.AddInParameter(dbCommand, "@OperationEventCodes", DbType.String, "" + eventObjects.Code);//CODE
                db.AddInParameter(dbCommand, "@Ids", DbType.String, eventObjects.Id.ToString());
                db.AddInParameter(dbCommand, "@FormIDs", DbType.String, eventObjects.FormID.ToString());
                db.AddInParameter(dbCommand, "@FormTypes", DbType.String, eventObjects.FormType.GetHashCode().ToString());
                db.AddInParameter(dbCommand, "@IsShowAgents", DbType.String, eventObjects.IsShowAgent.ToString());
                db.AddInParameter(dbCommand, "@IsShowCustomers", DbType.String, eventObjects.IsShowCustomer.ToString());
                db.AddInParameter(dbCommand, "@Subjects", DbType.String, eventObjects.Subject);
                db.AddInParameter(dbCommand, "@Contents", DbType.String, eventObjects.Description);
                db.AddInParameter(dbCommand, "@Prioritys", DbType.String, eventObjects.Priority.GetHashCode().ToString());
                db.AddInParameter(dbCommand, "@Types", DbType.String, eventObjects.Type.GetHashCode().ToString(CultureInfo.InvariantCulture));
                db.AddInParameter(dbCommand, "@MessageIDs", DbType.String, eventObjects.MessageID == Guid.Empty ? null : eventObjects.MessageID.ToString());

                db.AddInParameter(dbCommand, "@MailMessageIDs", DbType.String, eventObjects.MailMsgID);

                db.AddInParameter(dbCommand, "@ReturnResult", DbType.Boolean, true);
                db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, eventObjects.UpdateBy);
                //带星期日期格式在数据库会转换错误
                db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, eventObjects.UpdateDate == null ? "" : eventObjects.UpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
#if DEBUG
                if (eventObjects.IsShowCustomer)
                {
                    eventObjects.Id = result.GetValue<Guid>("ID");
                    SaveShipmentEventForCSP(eventObjects);
                }
#endif

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
        #endregion

        #region 保存事件列表
        /// <summary>
        /// 保存事件列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType"></param>
        /// <param name="OperationEventCodes"></param>
        /// <param name="ids">ID</param>
        /// <param name="FormIDs"></param>
        /// <param name="FormTypes"></param>
        /// <param name="IsShowAgents"></param>
        /// <param name="IsShowCustomers"></param>
        /// <param name="Subjects"></param>
        /// <param name="Contents"></param>
        /// <param name="Types">类型集合</param>
        /// <param name="prioritys"></param>
        /// <param name="Categorys"></param>
        /// <param name="Owner"></param>
        /// <param name="saveByID">保存人ID集合</param>
        /// <param name="ReturnResult"></param>
        /// <param name="updateDates">更新时间集合</param>
        /// <param name="occurrenceTime">发生时间</param>
        /// <param name="ManualImportant"></param>
        /// <param name="MessageIDs"></param>
        /// <returns></returns>
        public ManyResultData SaveMemoList(
            Guid operationID,
            OperationType operationType,
            EventObjects eventObjects,
            string[] OperationEventCodes,
            string[] ids,
            string[] FormIDs,
            string[] FormTypes,
            string[] IsShowAgents,
            string[] IsShowCustomers,
            string[] Subjects,
            string[] Contents,
            MemoType[] Types,
            MemoPriority[] prioritys,
            string[] Categorys,
            string Owner,
            DateTime?[] updateDates,
            Guid saveByID,
            bool ReturnResult,
            string occurrenceTime,
            bool ManualImportant,
            string[] MessageIDs)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");
            ArgumentHelper.AssertGuidNotEmpty(saveByID, "saveByID");

            try
            {
                ManyResultData result = null;
                TransactionOptions option = new TransactionOptions();
                option.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    Database db = DatabaseFactory.CreateDatabase();
                    DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveOperationMemoInfo]");

                    db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);   //业务ID
                    db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType);
                    db.AddInParameter(dbCommand, "@OperationEventCodes", DbType.String, OperationEventCodes.Join());//CODE
                    db.AddInParameter(dbCommand, "@Ids", DbType.String, ids.Join());
                    db.AddInParameter(dbCommand, "@FormIDs", DbType.String, FormIDs.Join());
                    db.AddInParameter(dbCommand, "@FormTypes", DbType.String, FormTypes.Join());
                    db.AddInParameter(dbCommand, "@IsShowAgents", DbType.String, IsShowAgents.Join());
                    db.AddInParameter(dbCommand, "@IsShowCustomers", DbType.String, IsShowCustomers.Join());
                    db.AddInParameter(dbCommand, "@Subjects", DbType.String, Subjects.Join());
                    db.AddInParameter(dbCommand, "@Contents", DbType.String, Contents.Join());
                    db.AddInParameter(dbCommand, "@Prioritys", DbType.String, prioritys.Join<MemoPriority>());
                    db.AddInParameter(dbCommand, "@Types", DbType.String, Types.Join<MemoType>());
                    db.AddInParameter(dbCommand, "@MessageIDs", DbType.String, MessageIDs.Join());

                    db.AddInParameter(dbCommand, "@OccurrenceTimes", DbType.String, string.IsNullOrEmpty(occurrenceTime) ? null : occurrenceTime);
                    db.AddInParameter(dbCommand, "@Importants", DbType.String, ManualImportant == true ? "1" : "0");

                    db.AddInParameter(dbCommand, "@ReturnResult", DbType.Boolean, ReturnResult);
                    db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, saveByID);
                    db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
                    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
                    result = db.ManyResult(dbCommand);
#if DEBUG
                    if (eventObjects.IsShowCustomer)
                    {
                        eventObjects.Id = result.ChildResults[0].ID;
                        SaveShipmentEventForCSP(eventObjects);
                    }
#endif
                    scope.Complete();
                }
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
        #endregion

        #region 获取备注附件内容
        /// <summary>
        /// 获取备注附件内容
        /// </summary>
        /// <param name="memoID">备注ID</param>
        /// <param name="attachName">附件名</param>
        /// <returns>返回附件内容</returns>
        public byte[] GetMemoAttachmentContent(
            Guid memoID,
            string attachName)
        {
            return null;
        } 
        #endregion

        #region 删除备注信息
        /// <summary>
        /// 删除备注信息
        /// </summary>
        /// <param name="memoId"></param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate"></param>
        /// <returns>返回SingleResult</returns>
        public void RemoveMemoInfo(
            Guid memoId,  //是memo.id
            Guid removeByID,
            DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(memoId, "memoId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspRemoveOperationMemoInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, memoId);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
                db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, removeByID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                db.ExecuteNonQuery(dbCommand);
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

        #region 获取OperationMemos表的ID
        /// <summary>
        /// 获取OperationMemos表的ID
        /// </summary>
        /// <param name="operationId">FROMID</param>
        /// <param name="code">事件的CODE</param>
        /// <returns></returns>
        public DataTable GetOperationMemosID(Guid operationId, string code)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationId, "operationId");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationMemosID");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
                db.AddInParameter(dbCommand, "@Code", DbType.String, code);
                DataTable dt = null;
                dt = db.ExecuteDataSet(dbCommand).Tables[0];
                if (dt != null)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
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

        #region 获取事件CODE集合
        /// <summary>
        /// 获取事件CODE集合
        /// </summary>
        /// <returns></returns>
        public List<EventCode> GetEventCodeList(OperationType operationType)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationEventCodeList]");
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                List<EventCode> results = (from b in ds.Tables[0].AsEnumerable()
                                           select new EventCode
                                           {
                                               Id = b.Field<Guid>("ID"),
                                               Code = b.IsNull("Code") ? string.Empty : b.Field<string>("Code").Trim(),
                                               Subject = b.IsNull("Subject") ? string.Empty : b.Field<string>("Subject").Trim(),
                                               Category = b.IsNull("Category") ? string.Empty : b.Field<string>("Category").Trim(),
                                               ShowCode = b.IsNull("ShowCode") ? string.Empty : b.Field<string>("ShowCode").Trim(),
                                               Show = b.Field<string>("ShowCode").Trim() + "-" + b.Field<string>("Subject").Trim(),
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
        #endregion
    }
}
