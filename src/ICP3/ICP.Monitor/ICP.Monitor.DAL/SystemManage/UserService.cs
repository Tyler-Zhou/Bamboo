#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/11 15:00:25
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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICP.Monitor.Model.Framework;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.Monitor.DAL.SystemManage
{
    public class UserService : DALBaseService
    {
        public ELoginInfo AuthUser(string userCode, string password, string macAddress, DateTime loginTime)
        {
            ELoginInfo loginInfo = new ELoginInfo { UserID = Guid.NewGuid(), UserCode = userCode, UserName = "Taylor Zhou" };
            return loginInfo;
            //if (string.IsNullOrEmpty(userCode))
            //{
            //    throw new ApplicationException("帐号不能为空.");
            //}
            //try
            //{
            //    Database db = GetDatabase();
            //    DbCommand dbCommand = db.GetStoredProcCommand("[sm].[uspAuthUser]");
            //    db.AddInParameter(dbCommand, "@Code", DbType.String, userCode);
            //    db.AddInParameter(dbCommand, "@Password", DbType.String, password);
            //    db.AddInParameter(dbCommand, "@IsEnglish", DbType.Boolean, false);

            //    DataSet ds = null;
            //    ds = db.ExecuteDataSet(dbCommand);
            //    if (ds == null || ds.Tables.Count < 1)
            //    {
            //        throw new ApplicationException("不存在该帐号.");
            //    }

            //    var result = (from b in ds.Tables[0].AsEnumerable()
            //                  select new
            //                  {
            //                      Id = b.Field<Guid>("ID"),
            //                      Password = b.Field<string>("Password")
            //                  }).SingleOrDefault();

            //    if ((string.IsNullOrEmpty(result.Password) && string.IsNullOrEmpty(password)))
            //    {

            //        LoginInfo authInfo = new LoginInfo();
            //        authInfo.LoginID = result.Id;
            //        WebAppContext.UserID = result.Id;
            //        WebAppContext.SessionID = "";

            //        return authInfo;
            //    }
            //    else
            //    {
            //        throw new ApplicationException("密码不正确！");
            //    }
            //}
            //catch (SqlException sqlException)
            //{
            //    throw new ApplicationException(sqlException.Message);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}
