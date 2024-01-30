using System;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceComponent
{
    public class FaxMessageService : IFaxMessageService
    {
        public bool IsEnglish
        {
            get { return ApplicationContext.Current.IsEnglish; }
        }
        public Guid UserId
        {
            get { return ApplicationContext.Current.UserId; }
        }

        public ICP.Framework.CommonLibrary.Common.ManyResult[] SaveFaxMessage(FaxMessageObjects entry)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspSaveFaxMessageInfo");

            db.AddInParameter(dbCommand, "@ID", DbType.Guid, entry.Id);
            db.AddInParameter(dbCommand, "@Subject", DbType.String, entry.Subject);
            db.AddInParameter(dbCommand, "@MessageFrom", DbType.String, entry.SendFrom);
            db.AddInParameter(dbCommand, "@MessageTo", DbType.String, entry.SendTo);

            db.AddInParameter(dbCommand, "@MessageCC", DbType.String, entry.CC);
            db.AddInParameter(dbCommand, "@Type", DbType.Int32, entry.Type.GetHashCode());
            db.AddInParameter(dbCommand, "@State", DbType.Int32, entry.State.GetHashCode());
            db.AddInParameter(dbCommand, "@BodyFormat", DbType.Int32, entry.BodyFormat.GetHashCode());
            db.AddInParameter(dbCommand, "@CompanyID", DbType.Guid, entry.CompanyID);
            db.AddInParameter(dbCommand, "@Body", DbType.String, entry.Body);
            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, entry.UpdateDate);
            db.AddInParameter(dbCommand, "@Way", DbType.Int32, entry.Way.GetHashCode());
            db.AddInParameter(dbCommand, "@MessageID", DbType.String, entry.MessageId);
            db.AddInParameter(dbCommand, "@ReceiveFaxID", DbType.Guid, entry.ReceiveFaxID);
            db.AddInParameter(dbCommand, "@Flag", DbType.Int32, entry.Flag.GetHashCode());
            db.AddInParameter(dbCommand, "@Priority", DbType.Int32, entry.Priority.GetHashCode());
            db.AddInParameter(dbCommand, "@Size", DbType.Int64, entry.Size);

            DataTable dtAttachment = MessageUtility.CreateAttachmentTable("Attachments");
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

            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
            db.AddInParameter(dbCommand, "@SaveByID", DbType.Guid, this.UserId);
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
    }
}
