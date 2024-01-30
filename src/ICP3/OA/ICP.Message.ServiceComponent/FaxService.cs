#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/27 星期五 19:29:16
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.Message.ServiceComponent
{
    public class FaxService : IFaxService
    {
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }
        public Guid UserId
        {
            get { return ApplicationContext.Current.UserId; }
        }
        private IMailBeeService _mailBeeService;
        private IMessageService _messageService;
        public FaxService(IMailBeeService mailBeeService, IMessageService messageService)
        {
            this._messageService = messageService;
            this._mailBeeService = mailBeeService;
            (mailBeeService as MailBeeService)._FaxService = this;
        }
        private string GetUserAddressString(string[] addresses)
        {
            return addresses == null ? string.Empty : addresses.Join();
        }
        private bool HasAttachment(AttachmentContent[] attachments)
        {
            return attachments != null && attachments.Length > 0;
        }


        #region IFaxService 成员

        public List<MessageFolderList> GetMessageFolderList(Guid ownerID)
        {

            Database db = DatabaseFactory.CreateDatabase();

            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspGetUserMessageFolderList]");

            db.AddInParameter(dbCommand, "@UserID", DbType.Guid, ownerID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return new List<MessageFolderList>();
            }

            List<MessageFolderList> results = (from folder in ds.Tables[0].AsEnumerable()
                                               select new MessageFolderList
                                               {
                                                   ID = folder.Field<Guid>("ID"),
                                                   UserID = folder.Field<Guid>("UserID"),
                                                   UserName = folder.Field<String>("UserName"),
                                                   Name = folder.Field<String>("FolderName"),
                                                   Type = (MessageFolderType)folder.Field<Byte>("Type"),
                                                   ParentID = folder.Field<Guid?>("ParentID"),
                                                   ParentName = folder.Field<String>("ParentFolderName"),
                                                   MessageLogCount = folder.Field<Int32>("MessageLogCount"),
                                                   UpdateDate = folder.Field<DateTime?>("UpdateDate")
                                               }).ToList();

            return results;

        }


        public ManyResultData SaveMessageFolder(Guid? folderID, Guid parentID, string name, MessageFolderType folderType, DateTime? updateDate)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspSaveUserMessageFolderInfo]");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, folderID);
            db.AddInParameter(dbCommand, "@ParentID", DbType.Guid, parentID);
            db.AddInParameter(dbCommand, "@Name", DbType.String, name);
            db.AddInParameter(dbCommand, "@Type", DbType.Int16, folderType.GetHashCode());
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, updateDate);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            //ManyResultData result = db.ManyResult(dbCommand, new string[] { "ID", "ParentID", "UpdateDate" });
            ManyResultData result = db.ManyResult(dbCommand);
            return result;
        }

        public ManyResultData RemoveFolder(Guid folderID, DateTime? updateDate)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspRemoveUserMessageFolderInfo]");

            db.AddInParameter(dbCommand, "@IDs", DbType.Guid, folderID);
            db.AddInParameter(dbCommand, "@RemoveByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.DateTime, updateDate);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            ManyResultData result = db.ManyResult(dbCommand);
            return result;
        }

        public List<ICP.Message.ServiceInterface.Message> GetMessageList(Guid ownerID, string ownerAccount, Guid? folderId, MessageFolderType? folderType, string folderName, string from, string to, string subject, bool? includeAttachment, MessagePriority? priority, MessageFlag? flag, DateTime? fromTime, DateTime? toTime, Guid? companyID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspGetFaxMessageList]");

            db.AddInParameter(dbCommand, "@UserID", DbType.Guid, ownerID);
            db.AddInParameter(dbCommand, "@UserAccount", DbType.String, ownerAccount);
            db.AddInParameter(dbCommand, "@FolderId", DbType.Guid, folderId);
            db.AddInParameter(dbCommand, "@FolderType", DbType.Int16, folderType);
            db.AddInParameter(dbCommand, "@FolderName", DbType.String, folderName);
            db.AddInParameter(dbCommand, "@MessageFrom", DbType.String, from);
            db.AddInParameter(dbCommand, "@MessageTo", DbType.String, to);
            db.AddInParameter(dbCommand, "@Subject", DbType.String, subject);
            db.AddInParameter(dbCommand, "@IsHasAttachment", DbType.Boolean, includeAttachment);
            db.AddInParameter(dbCommand, "@Priority", DbType.Int16, priority);
            db.AddInParameter(dbCommand, "@Flag", DbType.Int16, flag);
            db.AddInParameter(dbCommand, "@FromTime", DbType.DateTime, fromTime);
            db.AddInParameter(dbCommand, "@ToTime", DbType.DateTime, toTime);
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            DataSet ds = null;
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<ICP.Message.ServiceInterface.Message>();
                }

                return MessageUtility.ConvertTableToMessageList(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Message.ServiceInterface.Message> GetFaxMessageListByFastSearch(
            Guid ownerID,
            Guid? folderId,
            Guid? companyId,
            string keyWord)
        {
            List<Message.ServiceInterface.Message> results = null;

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspGetFaxMessageListByFastSearch]");

            db.AddInParameter(dbCommand, "@OwnerID", DbType.Guid, ownerID);
            db.AddInParameter(dbCommand, "@FolderId", DbType.Guid, folderId);
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyId);
            db.AddInParameter(dbCommand, "@KeyWord", DbType.String, keyWord);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            DataSet ds = null;
            try
            {
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    return new List<ICP.Message.ServiceInterface.Message>();
                }

                results = MessageUtility.ConvertTableToMessageList(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return results;
        }

        /// <summary>
        /// 用户点击文件夹，默认导出最近一个月的传真日志
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public List<ICP.Message.ServiceInterface.Message> GetMessageListByFolderId(Guid folderId)
        {
            DateTime dtLastMonth = DateTime.SpecifyKind(DateTime.Now.AddMonths(-1), DateTimeKind.Unspecified);
            DateTime dtNow = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            return GetMessageList(this.UserId, string.Empty, folderId, null, string.Empty, string.Empty, string.Empty, string.Empty, null, null, null, dtLastMonth, dtNow, null);

        }

        public ManyResult ChangeMessageFolder(Guid[] ids, Guid folderId, DateTime?[] updateDates)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspChangeMessageFolder]");
            db.AddInParameter(dbCommand, "@IDs", DbType.String, ids.Join());
            db.AddInParameter(dbCommand, "@FolderID", DbType.Guid, folderId);
            db.AddInParameter(dbCommand, "@UpdateDates", DbType.String, updateDates.Join());
            db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

            return db.ManyResult(dbCommand, new string[] { "ID", "FolderID", "UpdateDate" });
        }



        #endregion


        #region IBusinessMessageService 成员


        public SingleResult Send(ICP.Message.ServiceInterface.Message message)
        {
            return this._mailBeeService.Send(message);

        }

        #endregion
        public void Resend(ICP.Message.ServiceInterface.Message message)
        {
            this._mailBeeService.Send(message);
        }

        public ConfigureObjects GetConfigureInfoByCompanyID(Guid companyID)
        {
            ConfigureObjects result = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureInfoByCompanyID");
                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, companyID);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                    return new ConfigureObjects();

                result = (from entry in ds.Tables[0].AsEnumerable()
                          select new ConfigureObjects
                          {
                              BusinessNo = entry.Field<string>("BusinessNo"),
                              CompanyName = entry.Field<string>("CompanyName"),
                              ID = entry.Field<Guid>("ID"),
                              CompanyID = entry.Field<Guid>("CompanyID"),
                              CustomerID = entry.Field<Guid>("CustomerID"),
                              Email = entry.Field<string>("Email"),
                              EmailAddress = entry.Field<string>("EmailAddress"),
                              EmailHost = entry.Field<string>("EmailHost"),
                              EmailPassWord = entry.Field<string>("EmailPassWord"),
                              TaxNo = entry.Field<string>("TaxNo"),
                              UpdateDate = entry.Field<DateTime?>("UpdateDate"),
                              UpdateBy = entry.Field<Guid?>("UpdateBy"),

                              Type = LocalOrganizationType.Company
                          }).SingleOrDefault();

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        public ConfigureObjects GetConfigureInfoByMessageID(Guid messageID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspGetConfigureInfoByCompanyID");
            db.AddInParameter(dbCommand, "@MessageID", DbType.Guid, messageID);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
                return new ConfigureObjects();
            ConfigureObjects result = (from entry in ds.Tables[0].AsEnumerable()
                                       select new ConfigureObjects
                                       {
                                           UpdateDate = entry.Field<DateTime?>("UpdateDate"),
                                           TaxNo = entry.Field<string>("TaxNo"),
                                           ID = entry.Field<Guid>("ID"),
                                           Email = entry.Field<string>("Email"),
                                           EmailAddress = entry.Field<string>("EmailAddress"),
                                           EmailHost = entry.Field<string>("EmailHost"),
                                           EmailPassWord = entry.Field<string>("EmailPassWord"),
                                           CompanyID = entry.Field<Guid>("CompanyID")
                                       }).SingleOrDefault();
            return result;
        }

        public List<ConfigureObjects> ConvertDataSetToConfigureInfo(DataSet ds)
        {
            return (from entry in ds.Tables[0].AsEnumerable()
                    select new ConfigureObjects
                    {
                        UpdateDate = entry.Field<DateTime?>("UpdateDate"),
                        TaxNo = entry.Field<string>("TaxNo"),
                        ID = entry.Field<Guid>("ID"),
                        Email = entry.Field<string>("Email"),
                        EmailAddress = entry.Field<string>("EmailAddress"),
                        EmailHost = entry.Field<string>("EmailHost"),
                        EmailPassWord = entry.Field<string>("EmailPassWord"),
                        CompanyID = entry.Field<Guid>("CompanyID")
                    }).ToList();
        }

        public List<ConfigureObjects> GetConfigureInfoByEmail(string email, bool isTaxNo)
        {
            List<ConfigureObjects> result = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[pub].[uspGetConfigureInfoByEmailAddressOrTaxNo]");
                db.AddInParameter(dbCommand, "@Email", DbType.String, email);
                db.AddInParameter(dbCommand, "@IsTaxNo", DbType.Boolean, isTaxNo);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                DataSet ds = null;
                ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                    return new List<ConfigureObjects>();

                result = ConvertDataSetToConfigureInfo(ds);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }


        #region IFaxService 成员


        public ManyResult UpdateConfigureInfoByCompanyID(ConfigureObjects info)
        {
            ArgumentHelper.AssertGuidNotEmpty(info.CompanyID, "CompanyID");

            ManyResult result = null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("pub.uspUpdateConfigureInfoByCompanyID");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, info.CompanyID);
                db.AddInParameter(dbCommand, "@TaxNo", DbType.String, info.TaxNo);
                db.AddInParameter(dbCommand, "@EmailAddress", DbType.String, info.EmailAddress);
                db.AddInParameter(dbCommand, "@EmailHost", DbType.String, info.EmailHost);
                db.AddInParameter(dbCommand, "@Email", DbType.String, info.Email);
                db.AddInParameter(dbCommand, "@PassWord", DbType.String, info.EmailPassWord);
                db.AddInParameter(dbCommand, "@ChangeByID", DbType.Guid, this.UserId);
                db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, info.UpdateDate);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);

                result = db.ManyResult(dbCommand, new string[] { "ID", "UpdateDate" });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion
    }
}
