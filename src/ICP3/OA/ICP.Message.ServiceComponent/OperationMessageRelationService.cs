using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using ICP.SubscriptionPublish.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ICP.Message.ServiceComponent
{
    /// <summary>
    /// 操作日志服务类
    /// </summary>
    public class OperationMessageRelationService : PublishService<ISubscriptionPublishNotifyService>, IOperationMessageRelationService
    {
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }

        }
        public Guid UserId
        {
            get { return ApplicationContext.Current.UserId; }
        }
        /// <summary>
        /// 批量上传邮件与业务的关联信息，批量上传邮件所有外部联系人与业务的关联
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public Guid[] SaveMessageRelations(byte[] bytes)
        {
            object obj = bytes.Deserialize();
            if (obj == null)
                return null;
            List<OperationMessageRelation> relations = obj as List<OperationMessageRelation>;
            if (relations == null || relations.Count <= 0)
                return null;

            int length = relations.Count;
            Guid?[] ids = new Guid?[length];
            Guid[] operationIDs = new Guid[length];
            int[] operationTypes = new int[length];
            Guid?[] formIDs = new Guid?[length];
            int[] formTypes = new int[length];
            Guid[] iMessageIds = new Guid[length];
            string[] messageIds = new string[length];
            string[] entryIds = new string[length];
            int[] contactStages = new int[length];
            DateTime?[] updateDates = new DateTime?[length];
            bool[] autoassociatives = new bool[length];
            bool[] uploadServers = new bool[length];
            bool[] backupMails = new bool[length];
            StringBuilder strContacts = new StringBuilder();
            StringBuilder strMessageInfo = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                OperationMessageRelation item = relations[i];

                ids[i] = item.ID;
                operationIDs[i] = item.OperationID;
                operationTypes[i] = item.OperationType.GetHashCode();
                formIDs[i] = item.FormID;
                formTypes[i] = item.FormType.GetValueOrDefault(FormType.Unknown).GetHashCode();
                iMessageIds[i] = item.IMessageId;
                messageIds[i] = item.MessageId;
                entryIds[i] = item.EntryID;
                contactStages[i] = item.ContactStage.GetValueOrDefault(ContactStage.Unknown).GetHashCode();
                updateDates[i] = item.UpdateDate;
                autoassociatives[i] = item.RelationType == MessageRelationType.Auto;
                uploadServers[i] = item.UploadServer;
                backupMails[i] = item.BackupMail;
                strMessageInfo.Append(relations[i].XmlMessageInfo);
                strContacts.Append(relations[i].Contacts);

            }

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveOperationMessageAndContact4Batch]");

            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@IMessageIDs", DbType.String, iMessageIds.Join());
            db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIDs.Join());
            db.AddInParameter(dbCommand, "@OperationTypes", DbType.String, operationTypes.Join());
            db.AddInParameter(dbCommand, "@FormIDs", DbType.String, formIDs.Join());
            db.AddInParameter(dbCommand, "@FormTypes", DbType.String, formTypes.Join());
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.ToArray().Join());
            db.AddInParameter(dbCommand, "@MessageIDs", DbType.String, messageIds.Join());
            db.AddInParameter(dbCommand, "@EntryIDs", DbType.String, entryIds.Join());
            db.AddInParameter(dbCommand, "@ContactStages", DbType.String, contactStages.Join());
            db.AddInParameter(dbCommand, "@Autoassociatives", DbType.String, autoassociatives.Join());
            db.AddInParameter(dbCommand, "@UploadServers", DbType.String, uploadServers.Join());
            db.AddInParameter(dbCommand, "@BackUpMails", DbType.String, backupMails.Join());
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, UserId == Guid.Empty ? new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B") : UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@MainIDType", DbType.Byte, relations[0].UpdateDataType.GetHashCode());
            string contacts = GetXmlParameter(strContacts.ToString());
            db.AddInParameter(dbCommand, "@Contacts", DbType.String, contacts);
            string messageInfo = GetXmlParameter(strMessageInfo.ToString());
            db.AddInParameter(dbCommand, "@XmlMessageInfo", DbType.String, messageInfo);
            Guid[] results = null;
            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    results = (from entity in ds.Tables[0].AsEnumerable()
                               select entity.Field<Guid>("ID")).ToArray();
                }
            }
            catch (Exception ex)
            {
                //string queryString = string.Format("exec [fcm].[uspSaveOperationMessageAndContact4Batch] @IDs='{0}',@IMessageIDs='{1}',@OperationIDs='{2}',@OperationTypes='{3}',@FormIDs='{4}',@FormTypes='{5}',@UpdateDates='{6}',@MessageIDs='{7}',@ContactStages='{8}',@Autoassociatives='{9}',@SaveByID='{10}',@IsEnglish='{11}',@MainIDType='{12}',@Contacts='{13}',@XmlMessageInfo='{14}'", ids.Join(), iMessageIds.Join(), operationIDs.Join(), operationTypes.Join(), formIDs.Join(), formTypes.Join(), updateDates.ToArray().Join(), messageIds.Join(), contactStages.Join(), autoassociatives.Join(), UserId, IsEnglish, relations[0].UpdateDataType.GetHashCode(), contacts, messageInfo);
                //throw new Exception(queryString);
                throw ex;
            }
            return results;
        }
        /// <summary>
        /// 转换邮件地址和IsCC
        /// </summary>
        /// <param name="contacts"></param>
        /// <param name="cc"></param>
        /// <returns></returns>
        private string GetXmlParameter(string contacts)
        {
            string strMails = string.Empty;
            if (string.IsNullOrEmpty(contacts))
            {
                return strMails;
            }

            return string.Format("<Data>{0}</Data>", contacts);
        }

        /// <summary>
        /// 保存操作日志
        /// </summary>
        /// <param name="relations">关联对象集合</param>
        public ManyResult SaveOperationMailMessage(OperationMessageRelation[] relations)
        {
            if (relations == null || relations.Length <= 0)
                return null;
            int length = relations.Length;
            Guid?[] ids = new Guid?[length];
            Guid[] operationIDs = new Guid[length];
            int[] operationTypes = new int[length];
            Guid?[] formIDs = new Guid?[length];
            int[] formTypes = new int[length];
            Guid[] iMessageIds = new Guid[length];
            string[] messageIds = new string[length];
            string[] entryIds = new string[length];
            int[] contactStages = new int[length];
            DateTime?[] updateDates = new DateTime?[length];
            bool[] autoassociatives = new bool[length];
            bool[] uploadServers = new bool[length];
            bool[] backupMails = new bool[length];
            for (int i = 0; i < length; i++)
            {
                OperationMessageRelation item = relations[i];

                ids[i] = item.ID;
                operationIDs[i] = item.OperationID;
                operationTypes[i] = item.OperationType.GetHashCode();
                formIDs[i] = item.FormID;
                formTypes[i] = item.FormType.GetValueOrDefault(FormType.Unknown).GetHashCode();
                iMessageIds[i] = item.IMessageId;
                messageIds[i] = item.MessageId;
                entryIds[i] = item.EntryID;
                contactStages[i] = item.ContactStage.GetValueOrDefault(ContactStage.Unknown).GetHashCode();
                updateDates[i] = item.UpdateDate;
                autoassociatives[i] = item.RelationType == MessageRelationType.Auto;
                uploadServers[i] = item.UploadServer;
                backupMails[i] = item.BackupMail;
            }


            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspSaveOperationMessageInfoForBatch]");

            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@IMessageIDs", DbType.String, iMessageIds.Join());
            db.AddInParameter(dbCommand, "@OperationIDs", DbType.String, operationIDs.Join());
            db.AddInParameter(dbCommand, "@OperationTypes", DbType.String, operationTypes.Join());
            db.AddInParameter(dbCommand, "@FormIDs", DbType.String, formIDs.Join());
            db.AddInParameter(dbCommand, "@FormTypes", DbType.String, formTypes.Join());
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.ToArray().Join());
            db.AddInParameter(dbCommand, "@MessageIDs", DbType.String, messageIds.Join());
            db.AddInParameter(dbCommand, "@EntryIDs", DbType.String, entryIds.Join());
            db.AddInParameter(dbCommand, "@ContactStages", DbType.String, contactStages.Join());
            db.AddInParameter(dbCommand, "@Autoassociatives", DbType.String, autoassociatives.Join());
            db.AddInParameter(dbCommand, "@UploadServers", DbType.String, uploadServers.Join());
            db.AddInParameter(dbCommand, "@BackUpMails", DbType.String, backupMails.Join());
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, UserId == Guid.Empty ? new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B") : UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            db.AddInParameter(dbCommand, "@MainIDType", DbType.Byte, relations[0].UpdateDataType.GetHashCode());

            ManyResult result = null;
            try
            {
                result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });

                FireEvent("SendMessage", ApplicationContext.Current.ClientId, operationIDs, operationTypes);
            }
            catch (Exception ex)
            {
                string errorString = CommonHelper.BuildExceptionString(ex) 
                    +Environment.NewLine+ "OperationID:" + operationIDs.ToArray().Join() + ",OperationType:" + operationTypes.ToArray().Join();
                //LogHelper.SaveLog("RelationException", errorString);
                throw new Exception(errorString);
            }
            return result;
        }

        public List<OperationMessageRelation> GetByMessageId(string messageId)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationMessageInfoForMessageID]");
            db.AddInParameter(dbCommand, "@MessageID", DbType.String, messageId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            DataSet ds = null;
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                return InnerGet(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OperationMessageRelation> Get(Guid messageId)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationMessageInfo");
            db.AddInParameter(dbCommand, "@IMessageId", DbType.Guid, messageId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            DataSet ds = null;
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                return InnerGet(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<OperationMessageRelation> GetByMessageIdAndReference(string messageId, string reference)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[fcm].[uspGetOperationMessageInfoForMessageIDOrReference]");
            db.AddInParameter(dbCommand, "@MessageId", DbType.String, messageId);
            db.AddInParameter(dbCommand, "@Reference", DbType.String, reference);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            DataSet ds = null;
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                return InnerGet(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private List<OperationMessageRelation> InnerGet(DataSet ds)
        {
            if (ds == null || ds.Tables.Count <= 0)
            {
                return new List<OperationMessageRelation>();
            }
            return MessageUtility.ConvertDataTableToOperationMessageRelation(ds.Tables[0]);
        }
        /// <summary>
        /// 判断是否存在关联
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <returns></returns>
        public bool ExistsRelation(string messageId)
        {
            List<OperationMessageRelation> relation = GetByMessageId(messageId);
            if (relation != null && relation.Count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 判断关联是否发送更改,如发送更改则返回关联,如果 否则返回null
        /// </summary>
        /// <param name="messageId">消息的MessageID特性</param>
        /// <param name="updateDate">消息的更改时间</param>
        /// <returns></returns>
        public OperationMessageRelation CheckRelationIsChanged(string messageId, DateTime? updateDate)
        {
            List<OperationMessageRelation> relation = GetByMessageId(messageId);
            if (relation == null || relation.Count <= 0)
            {
                return null;
            }
            //else if (relation.UpdateDate != updateDate)
            //{
            //   return relation;
            // }
            return null;

        }

        public bool RemoveOperationMessagesByIDs(Guid[] operationMessageIDs, DateTime?[] updateDates)
        {
            if (operationMessageIDs == null || operationMessageIDs.Length <= 0)
                return false;

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[FCM].[uspRemoveOperationMessages]");
            db.AddInParameter(dbCommand, "@Ids", DbType.String, operationMessageIDs.Join());
            db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            try
            {
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }



    }
}
