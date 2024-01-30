namespace ICP.FCM.Common.ServiceComponent
{
    using System;
    using System.Collections.Generic;
    using ServiceInterface.DataObjects;
    using Framework.CommonLibrary.Common;
    using Framework.CommonLibrary.Helper;
    using System.Data;
    using System.Linq;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data.Common;
    using System.Data.SqlClient;

    public partial class FCMCommonService
    {
        #region MailFax

        /// <summary>
        /// 获取邮件传真日志列表
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">业务类型(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="ownerID">所有者ID</param>
        /// <returns>返回邮件传真日志列表</returns>
        public List<CommonMailFaxLogList> GetMailFaxLogList(
            Guid operationID,
            OperationType operationType,
            Guid? ownerID)
        {
            ArgumentHelper.AssertGuidNotEmpty(operationID, "operationID");
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("fcm.uspGetOperationMailMessagesList");

                db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, operationID);
                db.AddInParameter(dbCommand, "@OperationType", DbType.Int16, operationType);
                db.AddInParameter(dbCommand, "@FormID", DbType.Guid, ownerID);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return null;
                }

                List<CommonMailFaxLogList> result = (from b in ds.Tables[0].AsEnumerable()
                                                     select new CommonMailFaxLogList
                                                     {
                                                         ID = b.Field<Guid>("ID"),
                                                         Sender = b.Field<string>("From"),
                                                         To = b.Field<string>("To"),
                                                         Subject = b.Field<string>("Subject"),
                                                         CreateDate = b.Field<DateTime>("SendDate"),
                                                         AttachmentList = (from h in ds.Tables[1].AsEnumerable()
                                                                           where b.Field<Guid>("MailMessageID") == h.Field<Guid>("MailMessageID")
                                                                           select new AttachmentList
                                                                           {
                                                                               AttachmentName = h.Field<string>("AttachmentName"),
                                                                               AttachmentSize = h.Field<float>("MessageSize") 
                                                                           }).ToList(),
                                                     }).ToList();

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
        /// 保存邮件传真日志
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="ownerID">所有者ID</param>
        /// <param name="ownerSource">业务类型(0:海运出口,1:海运进口,2:空运出口,3:空运进口,4:其他出口,5:其它进口)</param>
        /// <param name="keyID">关键字ID</param>
        /// <param name="userMailAccountID">用户邮件账号ID</param>
        /// <param name="mailFrom">发件人</param>
        /// <param name="mailTo">收件人</param>
        /// <param name="mailCC">抄送人</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="mailContent">邮件内容</param>
        /// <param name="priority">重要性（0普通、1高、2低）</param>
        /// <param name="isFax">是否发送传真</param>
        /// <param name="attachmentNames">附件名列表</param>
        /// <param name="attachmentContents">附件内容列表</param>
        /// <param name="saveByID">保存人ID</param>
        /// <param name="updateDate">更新时间-做数据版本用</param>
        /// <returns>返回SingleResult</returns>
        public ManyResult SaveMailFaxLogInfo(
            Guid ownerID,
            OperationType ownerSource,
            Guid?[] id,
            Guid?[] keyID,
            Guid[] userMailAccountID,
            string[] mailFrom,
            string[] mailTo,
            string[] mailCC,
            string[] subject,
            string[] mailContent,
            EMailPriority[] priority,
            bool[] isFax,
            string[] attachmentNames,
            byte[][] attachmentContents,
            Guid saveByID,
            DateTime? updateDate)
        {
            return new ManyResult();
        }

        /// <summary>
        /// 获取邮件的附件列表
        /// </summary>
        /// <param name="mailLogID">邮件日志ID</param>
        /// <returns>返回邮件的附件列表</returns>
        public List<AttachmentList> GetMailLogAttachmentList(Guid mailLogID)
        {
            return new List<AttachmentList>();
        }

        /// <summary>
        /// 删除备注信息
        /// </summary>
        /// <param name="mailLogID"></param>
        /// <param name="removeByID">删除人</param>
        /// <param name="updateDate"></param>
        /// <returns>返回SingleResult</returns>
        public void RemoveMailMessageInfo(Guid mailLogID, Guid removeByID, DateTime? updateDate)
        {
            ArgumentHelper.AssertGuidNotEmpty(mailLogID, "mailLogID");

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspRemoveMailMessageInfo");

                db.AddInParameter(dbCommand, "@Id", DbType.Guid, mailLogID);
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

        #endregion
        }
    }
}
