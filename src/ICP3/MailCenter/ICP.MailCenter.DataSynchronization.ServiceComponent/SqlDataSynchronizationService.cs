using ICP.DataSynchronization.ServiceInterface;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using System;
using System.Data.SqlClient;
using System.ServiceModel;

namespace ICP.DataSynchronization.ServiceComponent
{  
    /// <summary>
    /// sqlserver数据同步服务
    /// </summary>
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession,AutomaticSessionShutdown=true)]
    public class SqlDataSynchronizationService : RelationalDataSynchronizationService, ISqlDataSynchronizationService
    {
        SqlSyncProvider dbProvider;
        String sqlConnectionString;
        Guid userId;
        public SqlDataSynchronizationService(String sqlConnectionString)
        {   
            this.sqlConnectionString = sqlConnectionString;
        }
        protected override RelationalSyncProvider ConfigureProvider(String scopeName,Guid userId)
        {
            this.dbProvider = ServiceUtility.ConfigureSqlSyncProvider(userId, scopeName, this.sqlConnectionString);
            this.userId = userId;
            return this.dbProvider;
        }
        public DbSyncScopeDescription GetScopeDescription()
        {
            DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope(DataSynchronizationUtility.ScopeName+this.userId.ToString(), (SqlConnection)this.dbProvider.Connection);
            
            return scopeDesc;
        }

     
    }
}
