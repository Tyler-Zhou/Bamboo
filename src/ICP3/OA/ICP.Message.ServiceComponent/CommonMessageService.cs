using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Helper;
using System.Data;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface;
using System.Data.SqlClient;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Server;
using ICP.SubscriptionPublish.ServiceInterface;
using ICP.FileSystem.ServiceInterface;

namespace ICP.Message.ServiceComponent
{
    public class CommonMessageService : PublishService<ISubscriptionPublishNotifyService>, ICommonMessageService
    {
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }
        public Guid UserId
        {
            get { return ApplicationContext.Current.UserId; }
        }
        //系统管理员Id
        private static Guid changeById = new Guid("4DF5B249-C7B5-4AF9-A335-01A9F662B59C");
        private IUserService _userService;
        private ISessionService _sessionService;
        private IOperationMessageRelationService _operationMessageRelationService;
        private IMailBeeService _mailBeeService;
        public CommonMessageService(IUserService userService, IOperationMessageRelationService operationMessageRelationService, ISessionService sessionService, IMailBeeService mailBeeService)
        {

            this._userService = userService;
            this._operationMessageRelationService = operationMessageRelationService;
            this._sessionService = sessionService;
            this._mailBeeService = mailBeeService;
        }
        public ManyResult ChangeFaxState(Guid[] ids, Guid?[] folderIds, ReceiveFaxState[] states, DateTime?[] updateDates, DateTime?[] faxUpdateDates)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[pub].[uspChangeFaxState]");
            db.AddInParameter(dbCommand, "@MessageIDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@FolderIDs", DbType.String, folderIds.Join());
            db.AddInParameter(dbCommand, "@States", DbType.String, states.Join());
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
            db.AddInParameter(dbCommand, "@FaxUpdateDates", DbType.String, faxUpdateDates.Join());
            db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
            return result;
        }

        public List<FaxMessageObjects> GetMessageInfoByCompanyID(Guid companyID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetIMessageInfoByCompanyID");
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);

            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return new List<FaxMessageObjects>();

            List<FaxMessageObjects> result = (from entry in set.Tables[0].AsEnumerable()
                                              select new FaxMessageObjects
                                                     {
                                                         Id = entry.Field<Guid>("ID"),
                                                         Subject = entry.Field<String>("Subject"),
                                                         SendFrom = entry.Field<string>("MessageFrom"),
                                                         SendTo = entry.Field<string>("MessageTo"),
                                                         CC = entry.Field<string>("MessageCC"),
                                                         Type = (MessageType)entry.Field<Byte>("Type"),
                                                         State = (MessageState)entry.Field<byte>("State"),
                                                         Body = entry.Field<string>("Body"),
                                                         BodyFormat = (BodyFormat)entry.Field<byte>("BodyFormat"),
                                                         HasAttachment = entry.Field<Boolean>("IsHasAttachment"),
                                                         CreateDate = entry.Field<DateTime>("CreateDate"),
                                                         UpdateDate = entry.Field<DateTime?>("UpdateDate"),
                                                         FolderId = entry.Field<Guid>("FolderID"),
                                                         FolderName = entry.Field<string>("FolderName"),
                                                         MessageId = entry.Field<string>("MessageID"),
                                                         Flag = (MessageFlag)entry.Field<Byte>("Flag"),
                                                         Size = entry.Field<long>("Size"),
                                                         // StateDescription = entry.Field<string>("StateName"),
                                                         Priority = (MessagePriority)entry.Field<Byte>("Priority"),
                                                         ReceiveFaxID = entry.Field<Guid>("ReceiveFaxID"),
                                                         FaxState = entry.IsNull("FaxState") ? ReceiveFaxState.Received : (ReceiveFaxState)entry.Field<byte>("FaxState"),
                                                         FaxUpdateBy = entry.Field<Guid>("FaxUpdateBy"),
                                                         FaxUpdateDate = entry.Field<DateTime?>("FaxUpdateDate"),
                                                         Attachments = (from attachment in set.Tables[1].AsEnumerable()
                                                                        select new AttachmentContent
                                                                        {
                                                                            Id = attachment.Field<Guid>("ID"),
                                                                            // Content = attachment.Field<Byte[]>("Body"),
                                                                            DisplayName = attachment.Field<string>("DisplayName"),
                                                                            Name = attachment.Field<string>("Name"),
                                                                            Size = attachment.Field<long>("Size")
                                                                        }).ToList()

                                                     }).ToList();

            return result;
        }

        public ICP.Message.ServiceInterface.Message Get(Guid id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetIMessageInfo");
            db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);

            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return new ICP.Message.ServiceInterface.Message();
            return ConvertDataSetToMessageInfo(set);
        }

        public ICP.Message.ServiceInterface.Message ConvertDataSetToMessageInfo(DataSet ds)
        {
            ICP.Message.ServiceInterface.Message item = (from entry in ds.Tables[0].AsEnumerable()
                                                         select new ICP.Message.ServiceInterface.Message
                                                         {
                                                             Id = entry.Field<Guid>("ID"),
                                                             CreateBy = entry.Field<Guid>("CreateByID"),
                                                             CreatorName = entry.Field<String>("CreateByName"),
                                                             Subject = entry.Field<String>("Subject"),
                                                             SendFrom = entry.Field<string>("MessageFrom"),
                                                             SendTo = entry.Field<string>("MessageTo"),
                                                             CC = entry.Field<string>("MessageCC"),
                                                             Type = (MessageType)entry.Field<Byte>("Type"),
                                                             State = (MessageState)entry.Field<byte>("State"),
                                                             Body = entry.Field<string>("Body"),
                                                             BodyFormat = (BodyFormat)entry.Field<byte>("BodyFormat"),
                                                             HasAttachment = entry.Field<Boolean>("IsHasAttachment"),
                                                             CreateDate = entry.Field<DateTime>("CreateDate"),
                                                             UpdateDate = entry.Field<DateTime?>("UpdateDate"),
                                                             FolderId = entry.Field<Guid>("FolderID"),
                                                             FolderName = entry.Field<string>("FolderName"),
                                                             MessageId = entry.Field<string>("MessageID"),
                                                             Flag = (MessageFlag)entry.Field<Byte>("Flag"),
                                                             Size = entry.Field<long>("Size"),
                                                             // StateDescription = entry.Field<string>("StateName"),
                                                             Priority = (MessagePriority)entry.Field<Byte>("Priority"),
                                                             Attachments = (from attachment in ds.Tables[1].AsEnumerable()
                                                                            select new AttachmentContent
                                                                            {
                                                                                Id = attachment.Field<Guid>("ID"),
                                                                                // Content = attachment.Field<Byte[]>("Body"),
                                                                                DisplayName = attachment.Field<string>("DisplayName"),
                                                                                Name = attachment.Field<string>("Name"),
                                                                                Size = attachment.Field<long>("Size")
                                                                            }).ToList()

                                                         }).FirstOrDefault();

            return item;
        }

        public Message.ServiceInterface.Message GetMessageByMessageId(string messageID)
        {
            if (string.IsNullOrEmpty(messageID))
                return new ServiceInterface.Message();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[FCM].[GetOperationMessageInfoByMessageID]");
            db.AddInParameter(dbCommand, "@MessageID", DbType.String, messageID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);

            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return new ICP.Message.ServiceInterface.Message();

            return GetMessageEntity(set);
        }

        private Message.ServiceInterface.Message GetMessageEntity(DataSet ds)
        {
            ICP.Message.ServiceInterface.Message item = (from entry in ds.Tables[0].AsEnumerable()
                                                         select new ICP.Message.ServiceInterface.Message
                                                         {
                                                             Id = entry.Field<Guid>("ID"),
                                                             Subject = entry.Field<String>("Subject"),
                                                             SendFrom = entry.Field<string>("MessageFrom"),
                                                             SendTo = entry.Field<string>("MessageTo"),
                                                             CC = entry.Field<string>("MessageCC"),
                                                             Type = (MessageType)entry.Field<Byte>("Type"),
                                                             State = (MessageState)entry.Field<byte>("State"),
                                                             Body = entry.Field<string>("Body"),
                                                             BodyFormat = (BodyFormat)entry.Field<byte>("BodyFormat"),
                                                             HasAttachment = entry.Field<Boolean>("IsHasAttachment"),
                                                             CreateDate = entry.Field<DateTime>("CreateDate"),
                                                             UpdateDate = entry.Field<DateTime?>("UpdateDate"),
                                                             MessageId = entry.Field<string>("MessageID"),
                                                             Flag = (MessageFlag)entry.Field<Byte>("Flag"),
                                                             Size = entry.Field<long>("Size"),
                                                             //Sendtime = entry.Field<DateTime?>("SentDate"),
                                                             // StateDescription = entry.Field<string>("StateName"),
                                                             Priority = (MessagePriority)entry.Field<Byte>("Priority"),
                                                             UserProperties = (from up in ds.Tables[1].AsEnumerable()
                                                                               select new MessageUserPropertiesObject
                                                                               {
                                                                                   FormId = up.Field<Guid?>("FormID"),
                                                                                   FormType = (FormType)up.Field<Byte>("FormType"),
                                                                                   OperationType = (OperationType)up.Field<Byte>("OperationType"),
                                                                                   OperationId = up.Field<Guid>("OperationID"),


                                                                               }).FirstOrDefault()

                                                         }).FirstOrDefault();

            return item;
        }

        #region IMessageService 成员
        /// <summary>
        /// 保存邮件(1.如果ID为空，就新增数据，2.如果ID查找不到数据，并且MessageID可以查找到数据，就直接返回ID和Update，3.如果ID查找到数据，就更改数据)
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public ManyResult[] Save(ICP.Message.ServiceInterface.Message entry)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspSaveIMessageInfo]");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, entry.Id);
            db.AddInParameter(dbCommand, "@Subject", DbType.String, entry.Subject);
            db.AddInParameter(dbCommand, "@MessageFrom", DbType.String, entry.SendFrom);
            db.AddInParameter(dbCommand, "@MessageTo", DbType.String, entry.SendTo);

            //发件人、收件人名称列表
            if (entry.Type == MessageType.Email)
            {
                //系统发送邮件因在不同地方构建邮件实体，将在此统一设置发件人名称
                //未设置发件人名称 &&  发件人包含@符号：截取邮箱地址前面名称作为发件人名称
                if (string.IsNullOrEmpty(entry.FromName) && entry.SendFrom.Contains('@'))
                    entry.FromName = entry.SendFrom.Substring(0, entry.SendFrom.IndexOf('@'));
            }
            else
                entry.FromName = entry.SendFrom; //EDI/Fax默认设置发件人名称=发件人

            db.AddInParameter(dbCommand, "@FromName", DbType.String, entry.FromName);
            db.AddInParameter(dbCommand, "@ToName", DbType.String, string.IsNullOrEmpty(entry.ToName) ? "" : entry.ToName);


            db.AddInParameter(dbCommand, "@MessageCC", DbType.String, entry.CC);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, entry.Type.GetHashCode());
            db.AddInParameter(dbCommand, "@State", DbType.Int32, entry.State.GetHashCode());
            db.AddInParameter(dbCommand, "@BodyFormat", DbType.Int32, entry.BodyFormat.GetHashCode());

            db.AddInParameter(dbCommand, "@Body", DbType.String, entry.Body);
            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, entry.UpdateDate);
            db.AddInParameter(dbCommand, "@Way", DbType.Int32, entry.Way.GetHashCode());
            db.AddInParameter(dbCommand, "@MessageID", DbType.String, entry.MessageId);
            //db.AddInParameter(dbCommand, "@ReceiveFaxID", DbType.String, string.Empty);
            db.AddInParameter(dbCommand, "@Flag", DbType.Int32, entry.Flag.GetHashCode());
            db.AddInParameter(dbCommand, "@Priority", DbType.Int32, entry.Priority.GetHashCode());
            db.AddInParameter(dbCommand, "@Size", DbType.Int64, entry.Size);
            //新加字段做邮件检索用
            db.AddInParameter(dbCommand, "@EntryID", DbType.String, entry.EntryID);
            //邮件中心解析邮件后保存信息
            db.AddInParameter(dbCommand, "@IsHasAttachment", DbType.Boolean, entry.HasAttachment);
            if (!entry.Sendtime.HasValue || entry.Sendtime == Convert.ToDateTime("0001/1/1 0:00:00"))
            {
                entry.Sendtime = DateTime.Now;
            }
            db.AddInParameter(dbCommand, "@SentDate", DbType.DateTime, entry.Sendtime);
            //系统发送邮件、传真、EDI发送时间统一为当前时间
            if (entry.SentDateTimeZone == DateTimeOffset.MinValue)
                entry.SentDateTimeZone = DateTimeOffset.Now;
            //发送时间存UTC格式
            db.AddInParameter(dbCommand, "@SentDateTimeZone", DbType.DateTimeOffset, entry.SentDateTimeZone.ToDateTimeOffsetUTC());

            if (entry.ReceivingTime == Convert.ToDateTime("0001/1/1 0:00:00"))
            {
                entry.ReceivingTime = DateTime.Now;
            }
            db.AddInParameter(dbCommand, "@ReceivedDate", DbType.DateTime, entry.ReceivingTime);

            DataTable dtAttachment = MessageUtility.CreateAttachmentTable("Attachments");
            int i = 1;
            //邮件中心保存邮件不需要上传附件
            if (entry.HasAttachment && entry.Type != MessageType.Email)
            {
                Array.ForEach(entry.Attachments.ToArray(), attachment => dtAttachment.Rows.Add(attachment.Id, entry.Id, attachment.Name, attachment.Content, null, i++));
            }
            SqlParameter parameterAttachments = new SqlParameter("@MessageAttachments", dtAttachment);
            parameterAttachments.Direction = ParameterDirection.Input;
            parameterAttachments.SqlDbType = SqlDbType.Structured;
            parameterAttachments.TypeName = "oa.uttMessageAttachments";
            dbCommand.Parameters.Add(parameterAttachments);

            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, this.UserId == Guid.Empty ? entry.CreateBy : this.UserId);
            try
            {
                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "MessageID", "UpdateDate" }, new string[] { "ID", "UpdateDate" } });
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(Guid[] ids, DateTime?[] updateDates)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspRemoveIMessageInfo]");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            db.ExecuteNonQuery(dbCommand);
        }

        #endregion

        #region IMessageService 成员

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public ManyResult[] Save(ICP.Message.ServiceInterface.Message message, OperationMessageRelation relation)
        {
            ManyResult[] results = Save(message);
            if (relation != null)
            {
                Guid messageId = results[0].Items[0].GetValue<Guid>("ID");
                relation.IMessageId = messageId;
                this._operationMessageRelationService.SaveOperationMailMessage(new OperationMessageRelation[] { relation });
            }
            return results;
        }

        #endregion


        public SingleResultData ChangeFlag(Guid id, MessageFlag flag, DateTime? updateDate)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspChangeMessageFlag]");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
            db.AddInParameter(dbCommand, "@Flag", DbType.Int16, flag);
            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
            db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            SingleResultData result = null;
            try
            {
                result = db.SingleResult(dbCommand);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return result;
        }


        #region IMessageService 成员


        public ManyResult ChangeState(Guid[] ids, MessageState[] states, DateTime?[] updateDates)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspChangeMessageState]");

            List<int> listStates = new List<int>();
            foreach (MessageState state in states)
            {
                listStates.Add(state.GetHashCode());
            }

            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@States", DbType.String, listStates.ToArray().Join());
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
            db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            ManyResult result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
            return result;
        }

        public Message.ServiceInterface.Message GetMessageInfoById(Guid id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspGetMessageInfoByID]");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);

            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return new ICP.Message.ServiceInterface.Message();
            ICP.Message.ServiceInterface.Message item = (from entry in set.Tables[0].AsEnumerable()
                                                         select new ICP.Message.ServiceInterface.Message
                                                         {
                                                             Id = entry.Field<Guid>("ID"),
                                                             Subject = entry.Field<String>("Subject"),
                                                             SendFrom = entry.Field<string>("MessageFrom"),
                                                             SendTo = entry.Field<string>("MessageTo"),
                                                             CC = entry.Field<string>("MessageCC"),
                                                             Type = (MessageType)entry.Field<Byte>("Type"),
                                                             State = (MessageState)entry.Field<byte>("State"),
                                                             Body = entry.Field<string>("Body"),
                                                             BodyFormat = (BodyFormat)entry.Field<byte>("BodyFormat"),
                                                             HasAttachment = entry.Field<Boolean>("IsHasAttachment"),
                                                             CreateDate = entry.Field<DateTime>("CreateDate"),
                                                             FolderId = entry.Field<Guid>("FolderID"),
                                                             //FolderName = entry.Field<string>("FolderName"),
                                                             MessageId = entry.Field<string>("MessageID"),
                                                             Flag = (MessageFlag)entry.Field<Byte>("Flag"),
                                                             Size = entry.Field<long>("Size"),
                                                             // StateDescription = entry.Field<string>("StateName"),
                                                             Priority = (MessagePriority)entry.Field<Byte>("Priority"),
                                                             // UpdateDate = entry.Field<DateTime?>("FaxUpdateDate"),                                                           

                                                         }).FirstOrDefault();
            return item;
        }

        #endregion



        #region IMessageService 成员

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void Reply(Guid originalId, DateTime? updateDate, ICP.Message.ServiceInterface.Message message)
        {
            //ChangeFlag(originalId, MessageFlag.Reply, updateDate);
            message.Flag = MessageFlag.Reply;
            Save(message);
        }
        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]
        public void Forward(Guid originalId, DateTime? updateDate, ICP.Message.ServiceInterface.Message message)
        {
            // ChangeFlag(originalId, MessageFlag.Transfer, updateDate);
            message.Flag = MessageFlag.Transfer;
            Save(message);
        }

        #endregion
        public void ChangeState(string[] messageIds, MessageState[] states, MessageType type)
        {

            // ArgumentHelper.AssertNotNull<Guid[]>(messageIds, "messageIds");
            Database db = DatabaseFactory.CreateDatabase();
            List<int> stateList = new List<int>();
            foreach (MessageState state in states)
            {
                stateList.Add(state.GetHashCode());
            }
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspChangeMessageStateByMessageId]");

            db.AddInParameter(dbCommand, "@MessageIDs", DbType.String, messageIds.Join());

            db.AddInParameter(dbCommand, "@States", DbType.String, stateList.ToArray().Join());
            db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, changeById);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            try
            {
                DataSet ds = null;

                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return;
                }

                List<ICP.Message.ServiceInterface.Message> messages = MessageUtility.ConvertTableToMessageList(ds.Tables[0]);
                string[] userIds = (from message in messages
                                    select message.CreateBy.ToString()).Distinct().ToArray();

                Guid[] clientIds = _sessionService.GetClientId(userIds);
                if (clientIds == null || clientIds.Length <= 0)
                    return;
                string methodName = "ChangeState";
                foreach (Guid clientId in clientIds)
                {
                    FireEvent(methodName, clientId, messages.ToArray(), type);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SingleResult Send(ICP.Message.ServiceInterface.Message message)
        {
            return this._mailBeeService.Send(message);
        }

        public void Transfer(List<ConfigureObjects> userCompanyList, Guid defaultCompanyID)
        {
            _mailBeeService.Transfer(userCompanyList, defaultCompanyID);
        }


        #region 用于邮件中心查询邮件
        /// <summary>
        /// 返回当前业务的客户的邮件地址
        /// </summary>
        /// <param name="operationId">业务ID</param>
        /// <param name="values">客户类型</param>
        public List<ICP.Message.ServiceInterface.Message> CustomerMailList(Guid operationId, string values)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetCustomerMailList");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationId);
            db.AddInParameter(dbCommand, "@Values", DbType.String, values);
            DataSet set = db.ExecuteDataSet(dbCommand);

            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return null;
            DataTable dt = set.Tables[0];
            List<ServiceInterface.Message> contents = (from c in dt.AsEnumerable()
                                                       select new ServiceInterface.Message
                                          {
                                              SendFrom = c.Field<string>("Mail")
                                          }).ToList();
            return contents;
        }


        /// <summary>
        /// 根据查询条件返回当前邮件集合
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="words">查找文字</param>
        /// <param name="wordType">查找位置</param>
        /// <param name="refNo">业务号</param>
        /// <param name="refNoType">业务搜索范围</param>
        /// <param name="customerName">客户名</param>
        /// <param name="customerType">客户类型</param>
        /// <param name="phase">邮件阶段</param>
        /// <param name="mails">邮件地址</param>
        /// <param name="mailType">邮件搜索范围</param>
        /// <param name="dateType">时间查找范围</param>
        /// <param name="fromDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public List<ICP.Message.ServiceInterface.Message> RuturnMailList(Guid userId, string words, int wordType,
                                                                         string refNo, int refNoType,
                                                                         string customerName, int customerType,
                                                                         int phase, string mails, int mailType,
                                                                         int dateType, DateTime fromDate,
                                                                         DateTime endDate)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetEmailList");
            db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userId);
            db.AddInParameter(dbCommand, "@Words", DbType.String, words);
            db.AddInParameter(dbCommand, "@WordType", DbType.Int32, wordType);
            db.AddInParameter(dbCommand, "@RefNo", DbType.String, refNo);
            db.AddInParameter(dbCommand, "@RefNoType", DbType.Int32, refNoType);
            db.AddInParameter(dbCommand, "@CustomerName", DbType.String, customerName);
            db.AddInParameter(dbCommand, "@CustomerType", DbType.Int32, customerType);
            db.AddInParameter(dbCommand, "@Phase", DbType.Int32, phase);
            db.AddInParameter(dbCommand, "@Mails", DbType.String, mails);
            db.AddInParameter(dbCommand, "@MailType", DbType.Int32, mailType);
            db.AddInParameter(dbCommand, "@DateType", DbType.Int32, dateType);
            db.AddInParameter(dbCommand, "@FromDate", DbType.DateTime, fromDate);
            db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, endDate);
            DataSet set = db.ExecuteDataSet(dbCommand);

            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return null;
            DataTable dt = set.Tables[0];
            List<ServiceInterface.Message> contents = (from c in dt.AsEnumerable()
                                                       select new ServiceInterface.Message
                                                       {
                                                           Subject = c.Field<string>("Subject"),
                                                           ReceivingTime = c.IsNull("ReceivedDate") ? DateTime.Now : c.Field<DateTime>("ReceivedDate"),
                                                           SendFrom = c.Field<string>("MessageFrom"),
                                                           FromName = c.Field<string>("FromName"),
                                                           EntryID = c.Field<string>("EntryID"),
                                                           MessageId = c.Field<Guid>("ID").ToString(),
                                                           Size = c.IsNull("Size") ? 0 : c.Field<long>("Size"),
                                                           HasAttachment = c.IsNull("IsHasAttachment") ? false : c.Field<bool>("IsHasAttachment"),
                                                           Priority = c.IsNull("Priority") ? MessagePriority.Normal : (MessagePriority)Enum.Parse(typeof(MessagePriority), c.Field<byte>("Priority").ToString(), true),
                                                           Way = c.IsNull("Way") ? MessageWay.Send : (MessageWay)Enum.Parse(typeof(MessagePriority), c.Field<byte>("Way").ToString(), true),
                                                           ContactStage = c.Field<string>("ContactStage").ToStageNames() == "Unknow" ? "" : c.Field<string>("ContactStage").ToStageNames()
                                                       }).ToList();
           
                      
            return contents;
        }

        /// <summary>
        /// 获取服务端邮件信息
        /// </summary>
        /// <param name="messageId">邮件的MessageID</param>
        public List<ContentInfo> GetMailInfo(string messageId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetMailInfo");
            db.AddInParameter(dbCommand, "@IMessageIDS", DbType.String, messageId);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return new List<ContentInfo>();
            DataTable dataTable = set.Tables[0];
            List<ContentInfo> contents = (from c in dataTable.AsEnumerable()
                                          select new ContentInfo
                                          {
                                              Content = c.Field<byte[]>("Body"),
                                              Id = c.Field<Guid>("Id"),
                                              Name = c.Field<String>("Name"),
                                              FileType = c.Field<String>("file_type")
                                          }).ToList();

            return contents;
        }

        #endregion
    }
}


