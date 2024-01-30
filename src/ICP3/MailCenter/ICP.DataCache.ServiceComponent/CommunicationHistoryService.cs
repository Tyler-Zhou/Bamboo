using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ICP.Common.ServiceInterface;

namespace ICP.DataCache.ServiceComponent
{
    /// <summary>
    /// 沟通历史服务类
    /// </summary>
    public class CommunicationHistoryService : ICommunicationHistoryService
    {
        public bool isEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }
        public Guid userId
        {
            get { return ApplicationContext.Current.UserId; }
        }

        /// <summary>
        /// 得到操作沟通列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<CommunicationHistory> GetCommunicationHistoryList(BusinessOperationContext context)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationMessageList]");
            db.AddInParameter(dbCommand, "@CurrentUserID", DbType.Guid, ApplicationContext.Current.UserId);     
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, context.OperationID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<CommunicationHistory>();
            try
            {
                List<CommunicationHistory> entries = (from entry in set.Tables[0].AsEnumerable()
                                                      select new CommunicationHistory
                                                {
                                                    IsChoose = false,
                                                    Id = entry.Field<Guid>("Id"),
                                                    CreateBy = entry.Field<Guid>("CreateByID"),
                                                    CreatorName = entry.Field<String>("CreateByName"),
                                                    OperationId = entry.Field<Guid>("OperationID"),
                                                    OperationType = (OperationType)entry.Field<byte>("OperationType"),
                                                    FormType = (FormType)entry.Field<byte>("FormType"),
                                                    Subject = entry.Field<String>("Subject"),
                                                    SendFrom = entry.Field<string>("MessageFrom"),
                                                    SendTo = entry.Field<string>("MessageTo"),
                                                    CC = entry.Field<string>("MessageCC"),
                                                    FromName = entry.Field<string>("FromName"),
                                                    Type = (MessageType)entry.Field<byte>("Type"),
                                                    EntryId = entry.Field<string>("EntryID"),
                                                    State = (MessageState)entry.Field<byte>("State"),
                                                    StateDescription = entry.Field<string>("StateName"),
                                                    Remark = entry.Field<string>("remark"),
                                                    HasAttachment = entry.Field<Boolean>("IsHasAttachment"),
                                                    Priority = (MessagePriority)entry.Field<byte>("Priority"),
                                                    Way = (MessageWay)entry.Field<byte>("Way"),
                                                    CreateDate = entry.Field<DateTime>("CreateDate"),
                                                    Sendtime = entry.IsNull("SentDate") ? (DateTime?)entry.Field<DateTime>("CreateDate") : entry.Field<DateTime>("SentDate"),
                                                    SentDateTimeZone = entry.IsNull("SentDateTimeZone") ? (DateTimeOffset)(entry.IsNull("SentDate") ? entry.Field<DateTime>("CreateDate") : entry.Field<DateTime>("SentDate")) : entry.Field<DateTimeOffset>("SentDateTimeZone").ToLocalDateTime(),
                                                    UpdateDate = entry.Field<DateTime?>("UpdateDate"),
                                                    ContactStage = entry.Field<string>("ContactStage").ToStageNames() == "Unknow" ? "" : entry.Field<string>("ContactStage").ToStageNames()
                                                }).ToList();
                return entries;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ManyResult[] SaveCommunicationHistoryEntry(CommunicationHistory entry)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveOperationMessageInfo]");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, entry.Id);
            // db.AddInParameter(dbCommand, "@CreateBy", DbType.Guid, this.userId);
            db.AddInParameter(dbCommand, "@OperationId", DbType.Guid, entry.OperationId);
            db.AddInParameter(dbCommand, "@OperationType", DbType.Int32, entry.OperationType.GetHashCode());

            db.AddInParameter(dbCommand, "@FormType", DbType.Int32, entry.FormType.GetHashCode());
            db.AddInParameter(dbCommand, "@Subject", DbType.String, entry.Subject);
            db.AddInParameter(dbCommand, "@MessageFrom", DbType.String, entry.SendFrom);
            db.AddInParameter(dbCommand, "@MessageTo", DbType.String, entry.SendTo);

            db.AddInParameter(dbCommand, "@MessageCC", DbType.String, entry.CC);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, entry.Type.GetHashCode());
            db.AddInParameter(dbCommand, "@State", DbType.Int32, entry.State.GetHashCode());
            db.AddInParameter(dbCommand, "@BodyFormat", DbType.Int32, entry.BodyFormat.GetHashCode());

            db.AddInParameter(dbCommand, "@Body", DbType.String, entry.Body == null ? string.Empty : entry.Body);
            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, entry.UpdateDate);
            db.AddInParameter(dbCommand, "@Way", DbType.Int32, entry.Way.GetHashCode());
            db.AddInParameter(dbCommand, "@MessageID", DbType.String, entry.MessageId);

            db.AddInParameter(dbCommand, "@Flag", DbType.Int32, entry.Flag.GetHashCode());
            db.AddInParameter(dbCommand, "@Priority", DbType.Int32, entry.Priority.GetHashCode());
            db.AddInParameter(dbCommand, "@Size", DbType.Int64, entry.Size);

            DataTable dtAttachment = DataCacheUtility.CreateHistoryAttachmentTable("Attachments");
            int i = 1;
            if (entry.HasAttachment)
            {
                Array.ForEach(entry.Attachments.ToArray(), attachment => dtAttachment.Rows.Add(attachment.Id, entry.Id, attachment.Name, attachment.Content, null, i++));
            }
            SqlParameter parameterAttachments = new SqlParameter("@MessageAttachments", dtAttachment);
            parameterAttachments.Direction = ParameterDirection.Input;
            parameterAttachments.SqlDbType = SqlDbType.Structured;
            parameterAttachments.TypeName = "oa.uttMessageAttachments";
            dbCommand.Parameters.Add(parameterAttachments);

            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, userId);
            try
            {

                ManyResult[] results = db.ManyResults(dbCommand, new string[][] { new string[] { "ID", "UpdateDate"}, 
                    new string[] { "ID","UpdateDate" } });
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<AttachmentContent> GetAttachment(Guid messageId, List<Guid> attachmentIds)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspGetMessageAttachmentInfo]");
            db.AddInParameter(dbCommand, "@IMessageID", DbType.Guid, messageId);
            db.AddInParameter(dbCommand, "@MessageAttachmentIDs", DbType.String, attachmentIds.ToArray().Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1)
                return new List<AttachmentContent>();
            List<AttachmentContent> attachments = (from content in set.Tables[0].AsEnumerable()
                                                   select new AttachmentContent
                                                   {
                                                       Id = content.Field<Guid>("ID"),
                                                       Content = content.Field<Byte[]>("Body"),
                                                       Name = content.Field<string>("Name"),
                                                       DisplayName = content.Field<string>("DisplayName"),
                                                       Size = content.Field<long>("Size")
                                                   }).ToList();
            return attachments;
        }
        public AttachmentContent GetAttachment(Guid mailId, String attachmentName)
        {
            return new AttachmentContent { Name = attachmentName };
        }
        public CommunicationHistory GetCommunicationHistoryDetailInfo(Guid id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetIMessageInfo");
            db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.isEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);

            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return new CommunicationHistory();

            CommunicationHistory item = (from entry in set.Tables[0].AsEnumerable()
                                         select new CommunicationHistory
                                         {
                                             Id = entry.Field<Guid>("ID"),
                                             CreateBy = entry.Field<Guid>("CreateByID"),
                                             CreatorName = entry.Field<String>("CreateByName"),
                                             OperationId = entry.Field<Guid>("OperationID"),
                                             OperationType = (OperationType)entry.Field<byte>("OperationType"),
                                             FormType = (FormType)entry.Field<byte>("FormType"),
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
                                             FolderId = entry.Field<Guid>("MessageFolderID"),
                                             FolderName = entry.Field<string>("FolderName"),
                                             MessageId = entry.Field<string>("MessageID"),
                                             Flag = (MessageFlag)entry.Field<Byte>("Flag"),
                                             Size = entry.Field<long>("Size"),
                                             // StateDescription = entry.Field<string>("StateName"),
                                             Priority = (MessagePriority)entry.Field<Byte>("Priority"),
                                             ContactStage = entry.Field<string>("ContactStage").ToStageNames(),
                                             Attachments = (from attachment in set.Tables[1].AsEnumerable()
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

        public void SetCommunicationHistoryStage(Guid id, string contactstage)
        {
            ArgumentHelper.AssertGuidNotEmpty(id, "id");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspSetCommunicationHistoryStage");
                db.AddInParameter(dbCommand, "@ID", DbType.Guid, id);
                db.AddInParameter(dbCommand, "@StageType", DbType.String, contactstage);
                db.AddInParameter(dbCommand, "@ChangeBy", DbType.Guid, userId);
                db.ExecuteDataSet(dbCommand);

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

    }
}
