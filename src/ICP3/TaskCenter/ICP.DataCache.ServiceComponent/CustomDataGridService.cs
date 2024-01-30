using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using ICP.DataCache.ServiceInterface1;

namespace ICP.DataCache.ServiceComponent1
{
    /// <summary>
    /// 用户自定义列表显示服务
    /// </summary>
    public class CustomDataGridService : ICustomDataGridService
    {
        public Guid UserId
        {
            get
            {
                return ApplicationContext.Current.UserId;
            }
        }
        public bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }
        #region ICustomDataGridService 成员

        public SingleResult Save(UserCustomGridInfo customInfo)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommandWithTimeout("[sm].[uspSaveCustomGridInfoByTemplateCode]");

            db.AddInParameter(dbCommand, "@Id", DbType.Guid, customInfo.Id);
            db.AddInParameter(dbCommand, "@UserId", DbType.Guid, customInfo.UserId);
           // db.AddInParameter(dbCommand, "@ListFormType", DbType.Int32, customInfo.ListType.GetHashCode());
            db.AddInParameter(dbCommand, "@TemplateCode", DbType.String, customInfo.TemplateCode);
            db.AddInParameter(dbCommand, "@UpdateDate", DbType.DateTime, customInfo.UpdateDate);
            string xml = SerializerHelper.SerializeToString<UserCustomGridInfo>(customInfo);
            db.AddInParameter(dbCommand, "@ColumnData", DbType.Xml, xml);
            db.AddInParameter(dbCommand, "@SaveById", DbType.Guid, this.UserId);
            db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, this.IsEnglish);
           
            try
            {
                SingleResult result = db.SingleResult(dbCommand, new string[] { "ID", "UpdateDate" });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetUserColumns(Guid? userId, string templateCode)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand("SELECT [ID],[UserID],[ColumnData],[UpdateBy],[UpdateDate],[TemplateCode],[ListFormType] FROM [sm].[UserCustomColumns] where TemplateCode=@TemplateCode");//UserID=@UserID and 

            //db.AddInParameter(dbCommand, "@UserID", DbType.Guid, userId);
            // db.AddInParameter(dbCommand, "@ListFormType", DbType.Int32, customInfo.ListType.GetHashCode());
            db.AddInParameter(dbCommand, "@TemplateCode", DbType.String, templateCode);
            try
            {
               DataSet ds=  db.ExecuteDataSet(dbCommand);
               if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }



        #endregion
    }
}
