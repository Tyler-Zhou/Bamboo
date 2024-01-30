#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/1/11 星期四 12:48:07
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Data;
using System.Data.Common;
using System.Runtime.ExceptionServices;
using System.Security;
using Cityocean.Crawl.LogComponents;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// 数据库扩展方法
    /// </summary>
    public static class DatabaseExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="dbCommand"></param>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public static void TryExecuteNonQuery(this Database database, DbCommand dbCommand)
        {
            try
            {
                database.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                LogService.Fatal("BaseService", "ExecuteNonQuery", ex);
                throw new Exception("执行SQL语句/存储过程发生严重异常");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="dbCommand"></param>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public static DataSet TryExecuteDataSet(this Database database, DbCommand dbCommand)
        {
            try
            {
                return database.ExecuteDataSet(dbCommand);
            }
            catch (Exception ex)
            {
                LogService.Fatal("BaseService", "ExecuteNonQuery", ex);
                throw new Exception("执行SQL语句/存储过程发生严重异常");
            }
        }
    }
}
