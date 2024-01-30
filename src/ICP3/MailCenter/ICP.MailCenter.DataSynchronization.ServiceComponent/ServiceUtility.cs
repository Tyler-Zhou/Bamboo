using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ICP.DataSynchronization.ServiceInterface;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;

namespace ICP.DataSynchronization.ServiceComponent
{
    /// <summary>
    /// 数据同步服务端辅助类
    /// </summary>
    public class ServiceUtility
    {
        /// <summary>
        /// 同步模板名称
        /// 不同于用户同步模板名称，用户同步模板名称为:同步模板名称+UserId
        /// </summary>
        static string templateBaseName = DataSynchronizationUtility.ScopeName;
        /// <summary>
        /// 同步所用表和过程、触发器等所在模式名称
        /// </summary>
        static string schemaName = "dbo";
        /// <summary>
        /// 海出业务数据冗余表过滤字段
        /// </summary>
        static string[] filterColumns = new string[] { "CustomerServiceID" };
        /// <summary>
        /// 自定义列表名
        /// </summary>
        static string operationContactDomainCacheTableName = DataSynchronizationUtility.OperationContactDomainCacheTableName;

        /// <summary>
        /// 配置同步提供程序
        /// </summary>
        /// <param name="context"></param>
        /// <param name="scopeName"></param>
        /// <param name="sqlConnectionString"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        public static SqlSyncProvider ConfigureSqlSyncProvider(Guid userId, String scopeName, String sqlConnectionString)
        {
            SqlSyncProvider provider = null;
            try
            {
                provider = new SqlSyncProvider();
                provider.ScopeName = scopeName;
                //provider.MemoryDataCacheSize = 2048;
                SqlConnection conn = new SqlConnection(sqlConnectionString);

                provider.CommandTimeout = 4000;
                provider.Connection = conn;

                SqlSyncScopeProvisioning serverConfig = new SqlSyncScopeProvisioning(conn,
                                                                                     SqlSyncScopeProvisioningType
                                                                                         .Template);

                serverConfig.ObjectSchema = schemaName;
                //检测同步模板是否存在，不存在则新建
                if (!serverConfig.TemplateExists(templateBaseName))
                {
                    DbSyncScopeDescription scopeDesc = new DbSyncScopeDescription(templateBaseName);
                    scopeDesc.UserComment =
                        "Template for cache tables: fcm.OperationContactDomainCaches,fcm.OperationContactCache-Operation Contact information,fcm.OperationViewOECache-Ocean Export Operation Data,fcm.OperationMessages-shipment and message relation information";

                    foreach (String tableName in DataSynchronizationUtility.SyncAdapterTables)
                    {
                        DbSyncTableDescription tableDescription = SqlSyncDescriptionBuilder.GetDescriptionForTable(
                            tableName, conn);

                        scopeDesc.Tables.Add(tableDescription);
                    }
                    serverConfig.PopulateFromScopeDescription(scopeDesc);

                    AddOETableFilterClause(serverConfig);
                    AddOperationMessageTableFilter(serverConfig);

                    serverConfig.SetCreateProceduresForAdditionalScopeDefault(DbSyncCreationOption.Create);
                    serverConfig.SetCreateTableDefault(DbSyncCreationOption.Skip);
                    //serverConfig.SetCreateTableDefault(DbSyncCreationOption.Create);
                    serverConfig.Apply();
                }



                FilterData(scopeName, conn, userId);

            }
            catch (System.Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(ex.Message);
            }
            return provider;
        }



        private static void AddOperationContactTableFilter(SqlSyncScopeProvisioning serverConfig)
        {
            serverConfig.Tables[operationContactTableName].AddFilterColumn("CustomerID");

            serverConfig.Tables[operationContactTableName].FilterClause = "[side].[CustomerID] = @CustomerID";
            SqlParameter param = new SqlParameter("@CustomerID", SqlDbType.UniqueIdentifier);
            serverConfig.Tables[operationContactTableName].FilterParameters.Add(param);
        }
        private static string operationContactTableName = DataSynchronizationUtility.OperationContactTableName;
        private static string operationMessageTableName = DataSynchronizationUtility.OperationMessageTableName;
        private static void AddOperationMessageTableFilter(SqlSyncScopeProvisioning serverConfig)
        {
            serverConfig.Tables[operationMessageTableName].AddFilterColumn("CreateBy");

            serverConfig.Tables[operationMessageTableName].FilterClause = "[side].[CreateBy] = @CreateBy";
            SqlParameter param = new SqlParameter("@CreateBy", SqlDbType.UniqueIdentifier);
            serverConfig.Tables[operationMessageTableName].FilterParameters.Add(param);
        }
        /// <summary>
        /// 海出业务数据过滤
        /// </summary>
        /// <param name="serverConfig"></param>
        private static void AddOETableFilterClause(SqlSyncScopeProvisioning serverConfig)
        {


            string tableName = DataSynchronizationUtility.OEOperationViewTableName;
            foreach (string fileterColumnName in filterColumns)
            {
                serverConfig.Tables[tableName].AddFilterColumn(fileterColumnName);
                SqlParameter param = new SqlParameter("@" + fileterColumnName, SqlDbType.UniqueIdentifier);

                serverConfig.Tables[tableName].FilterParameters.Add(param);
            }
            serverConfig.Tables[tableName].FilterClause = GetOEFilterClause(filterColumns);

        }
        /// <summary>
        /// 海出业务过滤条件语句
        /// </summary>
        /// <param name="filterColumns"></param>
        /// <returns></returns>
        private static string GetOEFilterClause(string[] filterColumns)
        {
            string columnNamePrefix = "[side].";
            Dictionary<string, string> columnNames = new Dictionary<string, string>();
            filterColumns.ToList().ForEach(item =>
            {
                columnNames.Add(item, columnNamePrefix + item);
            });
            List<string> clauses = new List<string>();
            foreach (KeyValuePair<string, string> item in columnNames)
            {
                clauses.Add(item.Value + " =@" + item.Key);
            }
            string result = clauses.Aggregate((a, b) => a + " OR " + b);
            return result;
        }
        private static List<string> GetColumnNames(string prefix)
        {

            List<string> columnNames = new List<string>();
            filterColumns.ToList().ForEach(item =>
            {
                columnNames.Add(prefix + item);
            });
            return columnNames;
        }

        /// <summary>
        /// 过滤数据
        /// </summary>
        /// <param name="scopeName"></param>
        /// <param name="conn"></param>
        /// <param name="userId"></param>
        /// <param name="userService"></param>
        private static void FilterData(string scopeName, SqlConnection conn, Guid userId)
        {

            SqlSyncScopeProvisioning serverProvWholesale = new SqlSyncScopeProvisioning(conn, SqlSyncScopeProvisioningType.Scope);

            serverProvWholesale.ObjectSchema = schemaName;
            serverProvWholesale.CommandTimeout = 300;

            //检测用户同步模板是否存在，不存在，则新建
            if (!serverProvWholesale.ScopeExists(scopeName))
            {
                serverProvWholesale.PopulateFromTemplate(scopeName, templateBaseName);
                //过滤自定义列数据

                //过滤邮件与消息关联表数据 CreateBy实际存放的是当前登陆用户Id
                serverProvWholesale.Tables[operationMessageTableName].FilterParameters["@CreateBy"].Value = userId;

                //过滤业务联系人表数据
                // serverProvWholesale.Tables[operationContactTableName].FilterParameters["@CustomerID"].Value = userId;

                //过滤海出业务数据
                // List<ICP.Sys.ServiceInterface.DataObjects.UserList> subordinateUserList = userService.GetSubordinateUserList(userId, false);
                // List<string> userIds = subordinateUserList.Select<ICP.Sys.ServiceInterface.DataObjects.UserList, string>(item => item.ID.ToString()).ToList();
                List<string> temp = GetColumnNames("@");

                foreach (string columnName in temp)
                {
                    serverProvWholesale.Tables[DataSynchronizationUtility.OEOperationViewTableName].FilterParameters[columnName].Value = userId;
                }

                serverProvWholesale.Apply();
            }

        }



    }
}
