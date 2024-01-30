using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using CommonLogHelper = ICP.Framework.CommonLibrary.LogHelper;

namespace ICP.EDI.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    partial class EDIService
    {
        /// <summary>
        /// 记录发送日志
        /// </summary>
        /// <param name="id"></param>
        /// <param name="compnayID"></param>
        /// <param name="mblIds"></param>
        /// <param name="mblNos"></param>
        /// <param name="documentNo"></param>
        /// <param name="flag"></param>
        /// <param name="subject"></param>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="emailContent"></param>
        /// <param name="eDIContent"></param>
        /// <param name="sendDate"></param>
        /// <param name="senderId"></param>
        /// <param name="ediMode"></param>
        /// <returns></returns>
        Guid Log(Guid? id, EDIMode ediMode, Guid compnayID, Guid[] mblIds, string[] mblNos, string documentNo, EDIFlagType flag, string subject, string fromEmail, string toEmail, string emailContent, string eDIContent, DateTime sendDate, Guid senderId)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspSaveEDILog");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, compnayID);
                db.AddInParameter(dbCommand, "@MBLIDs", DbType.String, mblIds.Join());
                db.AddInParameter(dbCommand, "@MBLNos", DbType.String, mblNos.Join());
                db.AddInParameter(dbCommand, "@Flag", DbType.Int16, flag);
                db.AddInParameter(dbCommand, "@EDILogType", DbType.Int16, ediMode);
                db.AddInParameter(dbCommand, "@Subject", DbType.String, subject);
                db.AddInParameter(dbCommand, "@FromEmail ", DbType.String, fromEmail);
                db.AddInParameter(dbCommand, "@ToEmail", DbType.String, toEmail);
                db.AddInParameter(dbCommand, "@EmailContent", DbType.String, emailContent);
                db.AddInParameter(dbCommand, "@EDIContent", DbType.String, eDIContent);
                db.AddInParameter(dbCommand, "@SendDate", DbType.DateTime, sendDate);
                db.AddInParameter(dbCommand, "@SenderId", DbType.Guid, senderId);
                db.AddInParameter(dbCommand, "@DocumentNo", DbType.String, documentNo);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);


                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID" });

                return result.GetValue<Guid>("ID");
            }
            catch (Exception ex)
            {
                CommonLogHelper.SaveLog("EDIService", string.Format("{0}{1}", "Log", ex.Message));
                return Guid.Empty;
            }

        }


        /// <summary>
        /// 记录发送日志
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <returns></returns>
        Guid SaveLog(EDISendOption sendOption)
        {

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspSaveEDILog");

                db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, sendOption.CompanyID);
                db.AddInParameter(dbCommand, "@MBLIDs", DbType.String, sendOption.IDs.Join());
                db.AddInParameter(dbCommand, "@MBLNos", DbType.String, sendOption.NOs.Join());
                db.AddInParameter(dbCommand, "@Flag", DbType.Int16, sendOption.Flag);
                db.AddInParameter(dbCommand, "@EDILogType", DbType.Int16, sendOption.EdiMode);
                db.AddInParameter(dbCommand, "@Subject", DbType.String, sendOption.Subject);
                db.AddInParameter(dbCommand, "@FromEmail ", DbType.String, sendOption.CurrentSender);
                db.AddInParameter(dbCommand, "@ToEmail", DbType.String, sendOption.CurrentReceiver);
                db.AddInParameter(dbCommand, "@EmailContent", DbType.String, sendOption.Content);
                db.AddInParameter(dbCommand, "@EDIContent", DbType.String, sendOption.CurrentContent);
                db.AddInParameter(dbCommand, "@SendDate", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "@SenderId", DbType.Guid, sendOption.SendByID);
                db.AddInParameter(dbCommand, "@DocumentNo", DbType.String, sendOption.DocumentNO);
                db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);


                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID" });

                return result.GetValue<Guid>("ID");
            }
            catch (Exception ex)
            {
                CommonLogHelper.SaveLog("EDIService", string.Format("{0}{1}", "Log", ex.Message));
                return Guid.Empty;
            }

        }

        /// <summary>
        /// 获取EDI发送日志列表
        /// </summary>
        /// <param name="mblIds"></param>
        /// <returns></returns>
        public List<LogData> GetLogList(Guid[] mblIds)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspGetEDILogList");

            db.AddInParameter(dbCommand, "@MBLIDs", DbType.String, mblIds.Join());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }

            List<LogData> items = (from l in ds.Tables[0].AsEnumerable()
                                   select new LogData
                                   {
                                       EDIContent = l.Field<string>("EDIContent"),
                                       EDIMode = (EDIMode)l.Field<Byte>("EDILogType"),
                                       EDILogTypeName = l.Field<string>("EDILogTypeName"),
                                       EMailContent = l.Field<string>("EMailContent"),
                                       FromEMail = l.Field<string>("FromEMailAddress"),
                                       Id = l.Field<Guid>("Id"),
                                       SenderId = l.Field<Guid>("SenderId"),
                                       SenderName = l.Field<string>("SenderName"),
                                       Subject = l.Field<string>("Subject"),
                                       ToEMail = l.Field<string>("ToEMailAddress"),
                                       DocumentNo = l.Field<string>("DocumentNo"),
                                       SendTime = l.Field<DateTime>("CreateDate"),
                                       State = (MessageState)l.Field<Byte>("State"),
                                       EDIFlag = (EDIFlagType)l.Field<Int16>("EDIFlag")
                                   }).ToList();

            return items;
        }

        /// <summary>
        /// 获取EDI发送状态列表
        /// </summary>
        /// <param name="SeadbyID"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GetLogStateList(Guid? SeadbyID, string query)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("fcm.uspGetEDILogStateList");

            db.AddInParameter(dbCommand, "@Sendby", DbType.Guid, SeadbyID);
            db.AddInParameter(dbCommand, "@query", DbType.String, query);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);

            DataSet ds = null;
            ds = db.ExecuteDataSet(dbCommand);
            if (ds == null || ds.Tables.Count < 1)
            {
                return null;
            }

            List<LogData> items = (from l in ds.Tables[0].AsEnumerable()
                                   select new LogData
                                   {
                                       EDIContent = l.Field<string>("EDIContent"),
                                       EDIMode = (EDIMode)l.Field<Byte>("EDILogType"),
                                       EDILogTypeName = l.Field<string>("EDILogTypeName"),
                                       EMailContent = l.Field<string>("EMailContent"),
                                       FromEMail = l.Field<string>("FromEMailAddress"),
                                       Id = l.Field<Guid>("Id"),
                                       SenderId = l.Field<Guid>("SenderId"),
                                       SenderName = l.Field<string>("SenderName"),
                                       Subject = l.Field<string>("Subject"),
                                       ToEMail = l.Field<string>("ToEMailAddress"),
                                       DocumentNo = l.Field<string>("DocumentNo"),
                                       SendTime = l.Field<DateTime>("CreateDate"),
                                       EDIFlag = (EDIFlagType)l.Field<Int16>("EDIFlag"),
                                       State = (MessageState)l.Field<Byte>("State"),
                                       Remark = l.Field<string>("remark"),
                                       FileName = l.Field<string>("filename"),
                                       Filebyte = l.Field<byte[]>("filebyte"),
                                   }).ToList();

            string returnstring = JsonConvert.SerializeObject(items);
            return returnstring;
        }

        /// <summary>
        /// 回写EDI状态
        /// </summary>
        /// <param name="OperationNo">业务号</param>
        /// <param name="BLNO">提单号</param>
        /// <param name="type">EDI类型</param>
        /// <param name="state">EDI状态</param>
        /// <param name="remark">备注</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件内容(byte类型)</param>
        /// <param name="number">邮件索引(收件箱位置索引)</param>
        /// <param name="saveBy">保存人</param>
        public void SaveEDIStateLog(string OperationNo, string BLNO, int type, int state, string remark, string filename, byte[] file, int number,Guid saveBy)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("pub.uspEdiStateLog");
            db.AddInParameter(dbCommand, "@OperationID", DbType.Guid, null);
            db.AddInParameter(dbCommand, "@OperationNO", DbType.String, OperationNo);
            db.AddInParameter(dbCommand, "@BLID", DbType.Guid, null);
            db.AddInParameter(dbCommand, "@BLNO", DbType.String, BLNO);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, type);
            db.AddInParameter(dbCommand, "@State", DbType.Int32, state);
            db.AddInParameter(dbCommand, "@remark", DbType.String, remark);
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, new Guid("4047CFAD-ECC8-E111-9D0D-0026551CA87B"));
            db.AddInParameter(dbCommand, "@filename", DbType.String, filename);
            db.AddInParameter(dbCommand, "@files", DbType.Binary, file);
            db.AddInParameter(dbCommand, "@number", DbType.Int32, number);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);
            db.ExecuteNonQuery(dbCommand);
        }
    }
}
