using System;
using System.Linq;
using ICP.Message.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace ICP.Message.Business
{  
    /// <summary>
    /// 消息与EDI信息关联服务类
    /// </summary>
   public class MessageEDILogRelationService:IMessageEDILogRelationService
    {
       public bool IsEnglish
       {
           get { return ApplicationContext.Current.IsEnglish; }
       }

        #region IMessageEDILogRelationService 成员

        public MessageEDILogRelation Get(Guid iMessageId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("oa.uspGetEDILogInfo");
            db.AddInParameter(dbCommand, "@IMessageID", DbType.Guid, iMessageId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            DataSet set = db.ExecuteDataSet(dbCommand);
            if (set == null || set.Tables.Count < 1 || set.Tables[0].Rows.Count < 0)
                return new MessageEDILogRelation();

            MessageEDILogRelation item = (from entry in set.Tables[0].AsEnumerable()
                                                                       select new MessageEDILogRelation
                                                         {
                                                             Id = entry.Field<Guid>("ID"),
                                                             EDIConfigId = entry.Field<Guid>("EDIConfigureID"),
                                                             Flag = (EDIFlagType)(entry.Field<byte>("EDIFlag")),
                                                           IMessageId=entry.Field<Guid>("IMessageID"),
                                                             Type = (EDIMode)(entry.Field<Byte>("EDIType")),
                                                          TypeDescription=entry.Field<string>("TypeDescription")
                                                         }).FirstOrDefault();
            return item;
        }

        public void Save(MessageEDILogRelation relation)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[oa].[uspSaveMessageEDILogInfo]");

            db.AddInParameter(dbCommand, "@IMessageID", DbType.Guid, relation.IMessageId);
            db.AddInParameter(dbCommand, "@EDIConfigureID", DbType.Guid, relation.EDIConfigId);
            db.AddInParameter(dbCommand, "@EDIFlag", DbType.Int32, relation.Flag.GetHashCode());
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, IsEnglish);
            try
            {
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
