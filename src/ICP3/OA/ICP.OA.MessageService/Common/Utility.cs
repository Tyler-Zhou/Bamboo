using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceInterface
{
    public class MessageUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public static IFaxService faxService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<OperationMessageRelation> ConvertDataTableToOperationMessageRelation(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                return new List<OperationMessageRelation>();
            }

            List<OperationMessageRelation> results = (from message in dt.AsEnumerable()
                                                      select
                                                          new OperationMessageRelation
                                                              {
                                                                  HasData = true,
                                                                  MessageId = message.Field<string>("MessageID"),
                                                                  OperationNo = message.Field<string>("OperationNo"),
                                                                  ID = message.Field<Guid>("ID"),
                                                                  OperationID = message.Field<Guid>("OperationId"),
                                                                  IMessageId = message.Field<Guid>("IMessageID"),
                                                                  FormID = message.Field<Guid?>("FormId"),
                                                                  FormType =
                                                                      message.IsNull("FormType")
                                                                          ? FormType.Unknown
                                                                          : (FormType)message.Field<byte>("FormType"),
                                                                  OperationType =
                                                                          message.IsNull("OperationType")
                                                                          ? OperationType.Unknown
                                                                        : (OperationType)message.Field<byte>("OperationType"),
                                                                  UpdateDate = message.Field<DateTime?>("UpdateDate"),
                                                                  UpdateBy = message.Field<Guid?>("UpdateBy"),
                                                                  CreateBy = message.Field<Guid>("CreateBy"),
                                                                  CreateDate = message.Field<DateTime?>("CreateDate"),
                                                                  Contacts = message.Table.Columns.Contains("Contacts") ? message.Field<string>("Contacts") : "",
                                                                  XmlMessageInfo = message.Table.Columns.Contains("XmlMessageInfo") ? message.Field<string>("XmlMessageInfo") : "",
                                                                  Body = message.Table.Columns.Contains("Body") ? message.Field<string>("Body") : "",
                                                                  ContactStage =
                                                                      (message.IsNull("ContactStage") || (string.IsNullOrEmpty(message.Field<string>("ContactStage").Trim()) || message.Field<string>("ContactStage").Contains(","))
                                                                           ? ContactStage.Unknown : (ContactStage?)Int32.Parse(message.Field<string>("ContactStage"))),
                                                                  RelationType = message.IsNull("Autoassociative") ? MessageRelationType.Auto : (message.Field<bool>("Autoassociative") == true ? MessageRelationType.Auto : MessageRelationType.Hand),
                                                                  UploadServer = !message.IsNull("UploadServer") && message.Field<bool>("UploadServer"), //是否上传服务器
                                                                  BackupMail = !message.IsNull("BackupMail") && message.Field<bool>("BackupMail"),
                                                                  EntryID = message.Field<string>("EntryID")
                                                              }).ToList();

            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Message> ConvertTableToMessageList(DataTable dt)
        {
            if (dt == null || dt.Rows.Count <= 0)
                return new List<Message>();

            List<Message> entries = (from entry in dt.AsEnumerable()
                                                                  select new Message
                                                                  {
                                                                      Id = entry.Field<Guid>("ID"),
                                                                      CreateBy = entry.Field<Guid>("CreateByID"),
                                                                      CreatorName = entry.Field<String>("CreateByName"),
                                                                      Subject = entry.Field<String>("Subject"),
                                                                      SendFrom = entry.Field<string>("MessageFrom"),
                                                                      SendTo = entry.Field<string>("MessageTo"),
                                                                      CC = entry.Field<string>("MessageCC"),
                                                                      Type = (MessageType)entry.Field<byte>("Type"),
                                                                      Flag = (MessageFlag)entry.Field<byte>("Flag"),
                                                                      FolderId = entry.Field<Guid>("FolderID"),
                                                                      FolderName = entry.Field<string>("FolderName"),
                                                                      MessageId = entry.Field<string>("MessageID"),
                                                                      Priority = (MessagePriority)entry.Field<Byte>("Priority"),
                                                                      Size = entry.Field<long>("Size"),
                                                                      Way = (MessageWay)entry.Field<byte>("Way"),
                                                                      State = (MessageState)entry.Field<byte>("State"),
                                                                      // StateDescription = entry.Field<string>("StateName"),
                                                                      HasAttachment = entry.Field<Boolean>("IsHasAttachment"),
                                                                      CreateDate = entry.Field<DateTime>("CreateDate"),
                                                                      UpdateDate = entry.Field<DateTime?>("UpdateDate")


                                                                  }).ToList();
            return entries;


        }
        /// <summary>
        /// 只从当前的消息实体中的UserProperties属性获取消息与业务的关联信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static OperationMessageRelation GetOperationMessageRelation(Message message)
        {
            OperationMessageRelation relation = new OperationMessageRelation();
            if (message.UserProperties != null && message.UserProperties.OperationId != Guid.Empty)
            {
                relation.HasData = true;
                relation.OperationID = message.UserProperties.OperationId;
                relation.OperationType = message.UserProperties.OperationType;
                relation.MessageId = message.MessageId;
                relation.FormType = message.UserProperties.FormType;
                relation.ID = message.UserProperties.OperationRelationID;

                if (message.UserProperties.ContainsKey("UpdateDate"))
                {
                    relation.UpdateDate = (DateTime?)message.UserProperties["UpdateDate"];
                }

                if (message.UserProperties.ContainsKey("ContactStage"))
                {
                    object objContactStage = message.UserProperties["ContactStage"];
                    if (objContactStage == null || ("" + objContactStage) == "")
                        relation.ContactStage = ContactStage.Unknown;
                    else
                        relation.ContactStage = (ContactStage?)Enum.Parse(typeof(ContactStage), objContactStage.ToString());
                }
                else
                    relation.ContactStage = ContactStage.Unknown;                

                return relation;
            }
            else
            {
                relation.HasData = false;
                return relation;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable CreateAttachmentTable(string tableName)
        {
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add("Id", typeof(Guid));
            dt.Columns.Add("IMessageID", typeof(Guid));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Body", typeof(Byte[]));
            DataColumn column = new DataColumn("UpdateDate", typeof(DateTime));
            column.AllowDBNull = true;
            dt.Columns.Add(column);
            column = new DataColumn("RowIndex", typeof(int));
            dt.Columns.Add(column);
            return dt;
        }
    }
}
