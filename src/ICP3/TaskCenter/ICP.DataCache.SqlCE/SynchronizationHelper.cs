using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.IO;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data.SqlServerCe;
using Microsoft.Synchronization.Data;
using ICP.DataSynchronization.ServiceInterface;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.DataOperation.SqlCE1
{
    public class SynchronizationHelper
    {
        private static string userScopeName = DataSynchronizationUtility.ScopeName + LocalData.UserInfo.LoginID.ToString();
        private static SynchronizationHelper helper = null;
        private static object synObj = new object();

        public static SynchronizationHelper Current
        {
            get
            {
                if (helper == null)
                {
                    lock (synObj.GetType())
                    {
                        helper = new SynchronizationHelper();

                    }
                }
                return helper;
            }
        }
        public bool IsSynchronizeFinished { get; set; }
        private SynchronizationHelper() { }
        /// <summary>
        /// 数据同步
        /// </summary>
        /// <param name="localProvider"></param>
        /// <param name="remoteProvider"></param>
        /// <returns></returns>
        public SyncOperationStatistics Synchronize(KnowledgeSyncProvider localProvider, KnowledgeSyncProvider remoteProvider)
        {
            SyncOrchestrator orchestrator = new SyncOrchestrator();
            orchestrator.LocalProvider = localProvider;
            orchestrator.RemoteProvider = remoteProvider;
            orchestrator.Direction = SyncDirectionOrder.Download;
            
            CheckIfProviderNeedsSchema(localProvider as SqlCeSyncProvider);
            SyncOperationStatistics stats = orchestrator.Synchronize();
            
            return stats;
        }
        /// <summary>
        /// 检查客户端同步数据结构是否存在 若不存在则新建
        /// </summary>
        /// <param name="localProvider"></param>
        private void CheckIfProviderNeedsSchema(SqlCeSyncProvider localProvider)
        {

            if (localProvider != null)
            {
                SqlCeConnection ceConn = (SqlCeConnection)localProvider.Connection;
                SqlCeSyncScopeProvisioning ceConfig = new SqlCeSyncScopeProvisioning(ceConn);
                
                string scopeName = localProvider.ScopeName;
                if (!ceConfig.ScopeExists(scopeName))
                {
                    SqlDataSynchronizationServiceProxy serverProxy = new SqlDataSynchronizationServiceProxy(scopeName);
                    DbSyncScopeDescription scopeDesc = serverProxy.GetScopeDescription();
                    serverProxy.Dispose();
                    ceConfig.PopulateFromScopeDescription(scopeDesc);
                    ceConfig.Apply();
                    AlterOperationFileTable(ceConn);
                }
            }
        }

        private void AlterOperationFileTable(SqlCeConnection ceConn)
        {
            //string alterString = "alter table OperationFiles add UploadState tinyint not null default 4";
           // ExecuteSql(ceConn, alterString);

        }
        private void ExecuteSql(SqlCeConnection connection, string commandText)
        {
            EnsureConnectionOpen(connection);
            SqlCeCommand command = new SqlCeCommand(commandText, connection);
            command.ExecuteNonQuery();
        }
        private object ExecuteScalar(SqlCeConnection connection, string commandText)
        {
            EnsureConnectionOpen(connection);
            SqlCeCommand command = new SqlCeCommand(commandText, connection);
            return command.ExecuteScalar();
        }
        private void EnsureConnectionOpen(SqlCeConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        private void CreateFileStorageTable(SqlCeConnection connection)
        {
            string insertString = @"CREATE TABLE [fcm.FileStorage](
	                               [ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	                               [Content] [image] NULL,
	                               [HtmlContent] [image] NULL,
                                   [Name] nvarchar(255) NOT NULL,
                                   [CreateDate] [datetime] NOT NULL,
                                   [UploadState] tinyint not null default 4,
                                   CONSTRAINT [PK_fcm.FileStorage] PRIMARY KEY 
                                   (
	                                [ID] 
                                    ))";
            ExecuteSql(connection, insertString);


        }
        private void EnsureDatabaseDirectoryExists(string databaseDirectory)
        {
           
            if (!Directory.Exists(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);
            }
        }

        /// <summary>
        /// 检查并创建客户端数据库文件
        /// Edit by:sunnydeng
        /// Date:2013-3-4
        /// Description:针对多用户登录系统
        /// </summary>
        /// <param name="client"></param>
        public CEDatabase CheckAndCreateCEDatabase()
        {
            CEDatabase client = new CEDatabase();
            client.CreationMode = CEDatabaseCreationMode.FullInitialization;
            string databasePath=Path.Combine(Application.StartupPath,LocalData.UserInfo.LoginName);
            EnsureDatabaseDirectoryExists(databasePath);
            client.Location = Path.Combine(databasePath, DataSynchronizationUtility.ClientDatabaseName);
            client.Name = "ICP35Client";
            if (!File.Exists(client.Location))
            {
                SqlCeEngine engine = new SqlCeEngine(client.Connection.ConnectionString);
                
                engine.CreateDatabase();
                CreateFileStorageTable(client.Connection);
                EnsureContactInfoTableExists(client.Connection);
                EnsureUserCustomColumnExists(client.Connection);
                engine.Dispose();

            }
            else
            {
                DeleteHistoryRecord(client.Connection);
                
            }
            return client;
        }

        private void EnsureUserCustomColumnExists(SqlCeConnection sqlCeConnection)
        {
            string queryString = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'UserCustomColumns'";
            object result = ExecuteScalar(sqlCeConnection, queryString);
            if (result == null)
            {
                queryString = @"create table UserCustomColumns(
                                ID uniqueidentifier not null,
                                UserID uniqueidentifier null,
                                ColumnData ntext not null,
                                UpdateDate  datetime null,
                                TemplateCode nvarchar(255) not null,
                                constraint [PK_SM.USERCUSTOMCOLUMNS] primary key nonclustered (ID))";
                ExecuteSql(sqlCeConnection, queryString);
            }
        }

        private void EnsureContactInfoTableExists(SqlCeConnection sqlCeConnection)
        {
            string queryString = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'contactPersonInfo'";
            object result = ExecuteScalar(sqlCeConnection, queryString);
            if (result == null)
            {
                queryString = @"CREATE TABLE [contactPersonInfo](
	                               [EmailAddress] nvarchar(255) NOT NULL,
	                               [Type] [int] NOT NULL,
                                   CONSTRAINT [PK_fcm.contactPersonInfo] PRIMARY KEY 
                                   (
	                                [EmailAddress] 
                                    ))";
                ExecuteSql(sqlCeConnection, queryString);
            }

        }
        /// <summary>
        /// 删除fcm.FileStorage两个月前的记录
        /// </summary>
        private void DeleteHistoryRecord(SqlCeConnection conn)
        {
            string deleteString = "delete from [fcm.FileStorage]  where CreateDate < dateadd(month,-2,getdate())";
            ExecuteSql(conn, deleteString);
        }
        public SqlCeSyncProvider ConfigureCESyncProvider(SqlCeConnection sqlCeConnection)
        {
            SqlCeSyncProvider provider = new SqlCeSyncProvider(userScopeName, sqlCeConnection);
           
            

            provider.ScopeName =userScopeName;
            provider.Connection = sqlCeConnection;
            
            return provider;

        }
        public void Synchronize()
        {

            SynchronizationHelper helper = SynchronizationHelper.Current;
            CEDatabase client = helper.CheckAndCreateCEDatabase();

            RelationalSyncProvider srcProvider = helper.ConfigureCESyncProvider(new SqlCeConnection(string.Format(DataSynchronizationUtility.ConnectionStringTemplate, client.Location)));
            RelationalProviderProxy destinationProxy = new SqlDataSynchronizationServiceProxy(userScopeName);

            SyncOperationStatistics stats = SynchronizationHelper.Current.Synchronize(srcProvider, destinationProxy);
            IsSynchronizeFinished = true;
            destinationProxy.Dispose();
           // return stats;
        }


    }
}
