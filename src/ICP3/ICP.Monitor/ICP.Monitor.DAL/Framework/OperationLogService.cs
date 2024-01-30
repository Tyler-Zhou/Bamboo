#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/6 11:52:55
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
using System.Linq;
using ICP.Monitor.Model;
using ICP.Monitor.Model.Framework;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ICP.Monitor.DAL.Framework
{
    /// <summary>
    /// 操作日志服务
    /// </summary>
    public class OperationLogService : DALBaseService
    {
        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="olSearchParam"></param>
        /// <returns></returns>
        public EResultInfo GetOperationLogs(OperationLogSearchParam olSearchParam)
        {
            Database db = GetDatabase();
            EResultInfo returnResult = new EResultInfo();
            //带输出和返回参数的存储过程调用
            DbCommand dbCommand = db.GetStoredProcCommand("[sm].[uspGetOperationLogs]");
            db.AddInParameter(dbCommand, "@UserCode", DbType.String, olSearchParam.UserCode);
            db.AddInParameter(dbCommand, "@BeginDate", DbType.DateTime, olSearchParam.BeginDate);
            db.AddInParameter(dbCommand, "@EndDate", DbType.DateTime, olSearchParam.EndDate);

            try
            {
                DataSet ds = db.ExecuteDataSet(dbCommand);
                if (ds == null || ds.Tables.Count < 1)
                {
                    returnResult.Data = new List<EOperationLogInfo>();
                    return returnResult;
                }

                List<EOperationLogInfo> results = (from b in ds.Tables[0].AsEnumerable()
                                                  select new EOperationLogInfo
                                              {
                                                  ID = b.Field<Guid>("ID"),
                                                  OperationName = b.Field<string>("OperationName"),
                                                  OperationContent = b.Field<string>("OperationContent"),
                                                  OperationTime = b.Field<DateTime>("OperationTime"),
                                                  OperationDuration = b.Field<int>("OperationDuration"),
                                                  OperationSteps1 = b.Field<string>("OperationSteps1"),
                                                  OperationSteps2 = b.Field<string>("OperationSteps2"),
                                                  OperationSteps3 = b.Field<string>("OperationSteps3"),
                                                  UserCode = b.Field<string>("UserCode"),
                                                  UserName = b.Field<string>("UserName"),
                                                  DepartmentName = b.Field<string>("DepartmentName"),
                                                  AssemblyNames = b.Field<string>("AssemblyNames")
                                              }).ToList();
                returnResult.Data = results;
                return returnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
