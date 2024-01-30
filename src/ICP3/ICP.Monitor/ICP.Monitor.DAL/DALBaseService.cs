#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/3 16:11:18
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using ICP.Monitor.Model;
using ICP.Monitor.Model.Framework;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.Monitor.DAL
{
    public class DALBaseService
    {
        /// <summary>
        /// 用户Session
        /// </summary>
        public virtual WebAppContext WebAppContext { get; set; }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public virtual Database GetDatabase()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            return factory.Create("ICPMonitorDB");
        }

        /// <summary>
        /// 通用处理异常方法
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="isthrowEx">是否抛出异常</param>
        /// <returns></returns>
        public virtual CustomException HandlingException(Exception ex)
        {
            string errorID = "50000";
            CustomException fpmException = new CustomException(ex.Message, "", ex)
            {
                IsOnlyTips = false,
                ErrorLogID = errorID
            };
            return fpmException;
        }
    }
}
