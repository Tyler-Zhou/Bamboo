using ICP.DataCache.ServiceInterface;
using ICP.DataSynchronization.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServerCe;
using System;
using System.Data;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace ICP.DataOperation.SqlCE
{
    /// <summary>
    /// 客户端同步辅助类
    /// </summary>
    public class SynchronizationHelper
    {
        #region 成员变量
        /// <summary>
        /// 当前用户的同步Scope名称
        /// </summary>
        private static string userScopeName = DataSynchronizationUtility.ScopeName + LocalData.UserInfo.LoginID.ToString();
        /// <summary>
        /// 单例模式
        /// </summary>
        private static SynchronizationHelper helper = null;
        /// <summary>
        /// Lock
        /// </summary>
        private static object synObj = new object();
        /// <summary>
        /// 同步计时器
        /// </summary>
        private Timer _SynchronizeTimer;
        /// <summary>
        /// 异步索引
        /// </summary>
        private static string arrSetLocalTableIndex = "UpdateLocalTableIndex";
        /// <summary>
        /// 上传索引
        /// </summary>
        private static string arrUpdateLocalTableStructure = "UpdateLocalTableStructure";
        /// <summary>
        /// 首次异步
        /// </summary>
        bool isFirstTimeSync = true;
        /// <summary>
        /// 异步操作
        /// </summary>
        SyncOrchestrator orchestrator;
        /// <summary>
        /// 同步版本号
        /// </summary>
        private const string _SyncVersionNoKey = "SyncVersionNo";
        /// <summary>
        /// 表结构版本号
        /// </summary>
        private const string _CacheTableVersionNoKey = "CacheTableVersionNo";

        #region 属性-配置文件
        private ClientConfig _ClientConfigOperation = null;
        /// <summary>
        /// 更新配置文件
        /// </summary>
        public ClientConfig ClientConfigOperation
        {
            get
            {
                if (_ClientConfigOperation == null)
                    _ClientConfigOperation = new ClientConfig(GetLocalDatabasePath());
                return _ClientConfigOperation;
            }
        }
        #endregion


        /// <summary>
        /// 系统错误日志服务
        /// </summary>
        public ISystemErrorLogService SystemErrorLogService
        {
            get { return ServiceClient.GetService<ISystemErrorLogService>(); }
        } 

        #region 属性-批量上传服务

        /// <summary>
        /// 缓存文件操作服务
        /// </summary>
        public ILocalBusinessCacheDataOperation LocalBusinessCacheDataOperation
        {
            get { return ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>(); }
        }
        #endregion

        #region 属性-同步是否完成
        /// <summary>
        /// 同步是否完成。暂时没用到
        /// </summary>
        public bool IsSynchronizeFinished { get; set; }
        #endregion

        #region 属性-同步帮助类(单例模式)
        /// <summary>
        /// 单一实例
        /// </summary>
        public static SynchronizationHelper Current
        {
            get
            {
                if (helper == null)
                {
                    lock (synObj)
                    {
                        if (helper == null)
                            helper = new SynchronizationHelper();
                    }
                }
                return helper;
            }
        }
        #endregion

        #region 属性-当前用户GUID主键
        /// <summary>
        /// 当前用户GUID主键
        /// </summary>
        public Guid UserId
        {
            get
            {
                return LocalData.UserInfo.LoginID;
            }
        }
        #endregion
        #endregion

        #region 构造函数
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SynchronizationHelper() { }
        #endregion

        #region 公用方法
        /// <summary>
        /// 程序登录后第一次调用同步，检测是否重建本地数据库文件，再调用同步数据
        /// </summary>
        public void Synchronize()
        {
            try
            {
                if (IsNewLocalStorageFile())
                {
                    BackUpSDFFile();
                    SaveSyncVersionNo();
                }
                CEDatabase client = CheckAndCreateCEDatabase();

                SqlCeSyncProvider srcProvider =
                    ConfigureCESyncProvider(
                        new SqlCeConnection(string.Format(DataSynchronizationUtility.ConnectionStringTemplate,
                            GetLocalStorageFilePath())));
                CheckIfProviderNeedsSchema(srcProvider);
                if (LocalData.DownLoadIntervalSynchronize > 0)
                {
                    //同步前先删除自动关联邮件信息
                    _SynchronizeTimer = new Timer(RegularSynchronize, null
                        , 1
                        ,
                        (long)
                            TimeSpan.FromMinutes(LocalData.DownLoadIntervalSynchronize).TotalMilliseconds);
                }
            }
            catch (SqlCeException ceEx)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ceEx));
                StopTimer(_SynchronizeTimer);
                TimerOperationService.Current.Enabled = false;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
        }
        

        /// <summary>
        /// 程序退出时计时器停止
        /// </summary>
        public void OnApplicationExit(object sender, EventArgs e)
        {
            StopTimer(_SynchronizeTimer);
            if (orchestrator != null && orchestrator.State == SyncOrchestratorState.Downloading)
            {
                orchestrator.Cancel();
                orchestrator = null;
            }
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 获取本地SDF文件路径：不存在则创建目录
        /// </summary>
        /// <returns>路径字符串</returns>
        private string GetLocalDatabasePath()
        {
            string databasePath = Path.Combine(Application.StartupPath, LocalData.UserInfo.LoginName.Trim());
            if (!Directory.Exists(databasePath))
            {
                Directory.CreateDirectory(databasePath);
            }
            return databasePath;
        }

        /// <summary>
        /// 获取本地数据库文件绝对路径
        /// </summary>
        /// <returns></returns>
        private string GetLocalStorageFilePath()
        {
            string path = GetLocalDatabasePath();
            return Path.Combine(path, DataSynchronizationUtility.ClientDatabaseName);
        }

        /// <summary>
        /// 检查并创建客户端数据库文件
        /// Edit by:sunnydeng
        /// Date:2013-3-4
        /// Description:针对多用户登录系统
        /// </summary>
        private CEDatabase CheckAndCreateCEDatabase()
        {
            CEDatabase client = new CEDatabase();
            SqlCeConnection connection = null;
            try
            {
                //如果本地缓存数据库文件不存在，则新建
                //新建规则:以当前登陆用户名作为文件夹名称，在当前程序根目录下新建文件夹
                client.CreationMode = CEDatabaseCreationMode.FullInitialization;
                string localFilePath = GetLocalStorageFilePath();
                client.Location = localFilePath;
                client.Name = "ICP35Client";
                connection = new SqlCeConnection(string.Format(DataSynchronizationUtility.ConnectionStringTemplate, localFilePath));

                if (!File.Exists(client.Location))
                {
                    SqlCeEngine engine = new SqlCeEngine(connection.ConnectionString);
                    engine.CreateDatabase();
                    
                    engine.Dispose();

                    EnsureFileStorageTable(connection);
                    EnsureUserOperationLogTable(connection);
                }
                else
                {
                    try
                    {
                        EnsureUserOperationLogTable(connection);
                        DeleteHistoryRecord(connection);
                        DeleteHistoryBusinessRecord(connection);
                    }
                    catch (SqlCeException ex)
                    {
                        if (ex.NativeError == DataSynchronizationUtility.DataBaseCorruptCode)
                        {
                            DataSynchronizationUtility.RepairDataBase(connection.ConnectionString);
                            DeleteHistoryRecord(connection);
                            DeleteHistoryBusinessRecord(connection);
                        }
                        else
                            throw ex;
                    }
                }

                return client;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (client != null)
                {
                    client.Connection.Close();
                }
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }

        #region 同步数据
        /// <summary>
        /// 数据同步
        /// </summary>
        /// <param name="localProvider">本地同步提供程序实例</param>
        /// <param name="remoteProvider">服务端同步提供程序实例</param>
        /// <returns></returns>
        private SyncOperationStatistics StartSynchronize(SqlCeSyncProvider localProvider, SqlDataSynchronizationServiceProxy remoteProvider)
        {
            orchestrator = new SyncOrchestrator();
            orchestrator.LocalProvider = localProvider;
            orchestrator.RemoteProvider = remoteProvider;
            //同步方向为下载
            orchestrator.Direction = SyncDirectionOrder.Download;
            localProvider.ApplyChangeFailed += localProvider_ApplyChangeFailed;
            SyncOperationStatistics stats = orchestrator.Synchronize();
            //             WriteLog("RegularSynchronize->orchestrator.Synchronize", new Exception(string.Format("Synchronize Total Download count:{0}", stats.DownloadChangesTotal)));
            localProvider.Connection.Close();
            remoteProvider.Dispose();
            orchestrator = null;
            return stats;
        }

        /// <summary>
        /// 构建SqlCE同步
        /// </summary>
        /// <param name="sqlCeConnection"></param>
        /// <returns></returns>
        private SqlCeSyncProvider ConfigureCESyncProvider(SqlCeConnection sqlCeConnection)
        {
            SqlCeSyncProvider provider = new SqlCeSyncProvider(userScopeName, sqlCeConnection);

            provider.ScopeName = userScopeName;
            provider.Connection = sqlCeConnection;
            return provider;

        }
        #endregion

        /// <summary>
        /// 停止计时器
        /// </summary>
        /// <param name="_Timer"></param>
        private void StopTimer(Timer _Timer)
        {
            if (_Timer != null)
            {
                _Timer.Dispose();
                _Timer = null;
            }
        }

        /// <summary>
        /// 保存同步版本
        /// </summary>
        private void SaveSyncVersionNo()
        {
            ClientConfigOperation.AddValue(_SyncVersionNoKey, LocalData.SyncVersionNo.ToString());
        }

        /// <summary>
        /// 判断本地客户端配置文件是否存在
        /// </summary>
        /// <returns></returns>
        private bool NeedCheckingTableStructure()
        {
            bool result = false;
            string path = GetLocalDatabasePath();
            string userConfigFile = Path.Combine(path, "ClientConfig.cfg");
            if (!File.Exists(userConfigFile))
            {
                return true;
            }

            if (!ClientConfigOperation.Contains(_CacheTableVersionNoKey))
            {
                return true;
            }
            int localCacheTableVersionNo = Int32.Parse(ClientConfigOperation.GetValue(_CacheTableVersionNoKey));
            if (LocalData.CacheTableVersionNo > localCacheTableVersionNo)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 比较服务端配置表里syncVersionNo是否高于本地当前登陆用户配置文件中syncVersionNo项的值，如果是，则代表需要删除本地数据库文件
        /// </summary>
        /// <returns></returns>
        private bool IsNewLocalStorageFile()
        {
            bool result = false;
            string sdfPath = GetLocalStorageFilePath();
            string path = GetLocalDatabasePath();
            string userConfigFile = Path.Combine(path, "ClientConfig.cfg");
            if (!File.Exists(sdfPath))
            {
                File.Delete(userConfigFile);
                return true;
            }
            if (!File.Exists(userConfigFile))
            {
                return true;
            }

            if (!ClientConfigOperation.Contains(_SyncVersionNoKey))
            {
                return true;
            }
            int localSyncVersionNo = Int32.Parse(ClientConfigOperation.GetValue(_SyncVersionNoKey));
            if (LocalData.SyncVersionNo > localSyncVersionNo)
            {
                result = true;

            }
            return result;
        }

        /// <summary>
        /// 创造用户操作日志表
        /// </summary>
        /// <param name="connection"></param>
        private void EnsureUserOperationLogTable(SqlCeConnection connection)
        {
            try
            {
                if (IsTableExists("UserOperationLog", connection))
                    return;
                string queryString = @"CREATE TABLE [UserOperationLog](
	                               [ID] [UNIQUEIDENTIFIER] ROWGUIDCOL  NOT NULL,
	                               [UserID] [UNIQUEIDENTIFIER] NOT NULL,
                                   [InternetIP] NVARCHAR(128) NULL,
                                   [IntranetIP] NVARCHAR(128) NULL,
                                   [MacAddress] NVARCHAR(128) NULL,
                                   [FunctionName] NVARCHAR(128) NULL,
                                   [OperationContent] NVARCHAR(128)  NULL,
                                   [OperationDate] DATETIME NULL,
                                   [AssemblyName] NVARCHAR(128) NULL,
                                   [F1] NTEXT NULL,
                                   [F2] NTEXT NULL,
                                   [F3] NTEXT NULL,
                                   [F4] BIT NOT NULL DEFAULT 0,
                                   [F5] BIT NULL,
                                   [F6] INT NULL,
                                   [F7] INT NULL,
                                   [F8] NTEXT NULL,
                                   [F9] NTEXT NULL,
                                   [F10] NTEXT NULL,
                                   CONSTRAINT [PK_UserOperationLog] PRIMARY KEY 
                                   (
	                                [ID] 
                                    ))";
                ExecuteSql(connection, queryString);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 创建文档缓存表
        /// </summary>
        /// <param name="connection"></param>
        private void EnsureFileStorageTable(SqlCeConnection connection)
        {
            try
            {
                if (IsTableExists("fcm.FileStorage", connection))
                    return;
                string insertString = @"CREATE TABLE [fcm.FileStorage](
	                               [ID] [UNIQUEIDENTIFIER] ROWGUIDCOL  NOT NULL,
	                               [Content] [image] NULL,
	                               [HtmlContent] [image] NULL,
                                   [Name] NVARCHAR(255) NOT NULL,
                                   [CreateDate] [datetime] NOT NULL,
                                   [UploadState] TINYINT NOT NULL DEFAULT 4,
                                   [OperationID] [UNIQUEIDENTIFIER] NULL,
                                   [TypeCode] TINYINT,
                                   CONSTRAINT [PK_fcm.FileStorage] PRIMARY KEY 
                                   (
	                                [ID] 
                                    ))";
                ExecuteSql(connection, insertString);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
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
                try
                {
                    SqlCeSyncScopeProvisioning ceConfig = new SqlCeSyncScopeProvisioning(ceConn);

                    string scopeName = localProvider.ScopeName;
                    //如果本地不存在同步Scope,则需要先从服务端获取同步Scope元数据，生成同步表结构
                    if (!ceConfig.ScopeExists(scopeName))
                    {
                        SqlDataSynchronizationServiceProxy serverProxy =
                            new SqlDataSynchronizationServiceProxy(scopeName, UserId);
                        DbSyncScopeDescription scopeDesc = serverProxy.GetScopeDescription();
                        string message = scopeDesc.Tables.Aggregate("",
                            (current, table) => current + (table.LocalName + ";"));
                        serverProxy.Dispose();
                        ceConfig.PopulateFromScopeDescription(scopeDesc);
                        ceConfig.Apply();

                        ClientConfigOperation.AddValue(arrSetLocalTableIndex, false.ToString());
                        ClientConfigOperation.AddValue(arrUpdateLocalTableStructure, false.ToString());
                    }
                    UpgradeTableStructure(localProvider.Connection.ConnectionString);
                }
                catch (SqlCeException ceEx)
                {
                    throw ceEx;
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                }
                finally
                {
                    if (ceConn != null && ceConn.State != ConnectionState.Closed)
                    {
                        ceConn.Close();
                    }
                }
            }
        }
       
        /// <summary>
        /// 定时调用数据同步的方法
        /// </summary>
        /// <param name="state"></param>
        private void RegularSynchronize(object state)
        {
            IsSynchronizeFinished = false;
            Guid OperationLogID = Guid.NewGuid();
            Stopwatch stopwatchTotaltime = StopwatchHelper.StartStopwatch();
            MethodBase methodother = MethodBase.GetCurrentMethod();
            SqlCeSyncProvider srcProvider = null;
            try
            {
                StopwatchHelper.CustomRecordStopwatch(stopwatchTotaltime, OperationLogID, DateTime.Now,
                    methodother.DeclaringType.FullName, "SYNCCACHE", "Start Synchronize");
                srcProvider =
                    ConfigureCESyncProvider(
                        new SqlCeConnection(string.Format(DataSynchronizationUtility.ConnectionStringTemplate,
                            GetLocalStorageFilePath())));
                SqlDataSynchronizationServiceProxy destinationProxy =
                    new SqlDataSynchronizationServiceProxy(userScopeName, UserId);

                SyncOperationStatistics stats = StartSynchronize(srcProvider, destinationProxy);
                StopwatchHelper.CustomUpdateStopwatchLog(stopwatchTotaltime, OperationLogID, true,
                    string.Format("同步当前用户数据完成"));
                if (LocalBusinessCacheDataOperation != null)
                {
                    //是否存在需协助同事记录
                    var assistsIds = LocalBusinessCacheDataOperation.GetUserAssistsList(UserId, DateTime.Now);
                    if (assistsIds != null && assistsIds.Count > 0)
                    {
                        foreach (
                            SqlDataSynchronizationServiceProxy destinationAssistsUserProxy in
                                assistsIds.Select(
                                    item =>
                                        new SqlDataSynchronizationServiceProxy(
                                            DataSynchronizationUtility.ScopeName + item,
                                            item)))
                        {
                            StartSynchronize(srcProvider, destinationAssistsUserProxy);
                            StopwatchHelper.CustomUpdateStopwatchLog(stopwatchTotaltime, OperationLogID, true,
                                string.Empty, string.Format("同步协助用户数据完成"));
                        }
                    }
                }
                if (isFirstTimeSync)
                {
                    //首次同步完成后加载业务数据到内存
                    DataTable dtBusinessCache = LocalBusinessCacheDataOperation.GetConciseOperationViewInfo();
                    LocalData.OperationViewInfo = dtBusinessCache;
                    isFirstTimeSync = false;
                }
                StopwatchHelper.CustomUpdateStopwatchLog(stopwatchTotaltime, OperationLogID, false, string.Empty,
                    string.Empty, "End Synchronize");
            }
            catch (SyncException syncex)
            {
                string strException = string.Format("Synchronization组件存在异常\r\n{0}\r\nOperationLogID:[{1}]\r\nLocalVersionNo:[{2}]", CommonHelper.BuildExceptionString(syncex), OperationLogID, LocalData.LocalVersionNo);
                StopwatchHelper.CustomUpdateStopwatchLog(stopwatchTotaltime, OperationLogID, false, string.Empty, string.Empty, string.Format("End Synchronize Exception SessionID:{0}", LocalData.SessionId));
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName
                                , LocalData.SessionId, new byte[0], strException, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                //"80040154：Sync组件版本与运行所需不一直"
                //检索 COM 类工厂中 CLSID 为 {EC413D66-6221-4EBB-AC55-4900FB321011} 的组件时失败，原因是出现以下错误: 80040154。检索 COM 类工厂中 CLSID 为 {EC413D66-6221-4EBB-AC55-4900FB321011} 的组件时失败，原因是出现以下错误: 80040154
                if (syncex.Message.Contains("80040154"))
                {
                    StopTimer(_SynchronizeTimer);
                }
            }
            catch (Exception ex)
            {
                string strException = string.Format("{0}\r\nOperationLogID:[{1}]\r\nLocalVersionNo:[{2}]",CommonHelper.BuildExceptionString(ex),OperationLogID ,LocalData.LocalVersionNo);
                StopwatchHelper.CustomUpdateStopwatchLog(stopwatchTotaltime, OperationLogID, false,string.Empty,string.Empty, string.Format("End Synchronize Exception SessionID:{0}", LocalData.SessionId));
                SystemErrorLogService.Save(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName
                                , LocalData.SessionId, new byte[0], strException, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                Logger.Log.Error(strException);
            }
            finally
            {
                if (srcProvider != null)
                    srcProvider.Dispose();
                IsSynchronizeFinished = true;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="message"></param>
        public static void SaveLog(string fileName, string message)
        {
            string str = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + ":" + message + System.Environment.NewLine;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName + "log.txt");//文件路径

            using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
            {
                sw.Write(str);
                sw.Close();
            }
        }

        /// <summary>
        /// 本地应用更改时，如果出现异常，则采用强制写入重试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void localProvider_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            string str = string.Empty;
            if (e.Conflict.Type == DbConflictType.ErrorsOccurred)
            {
//                 if (e.Error.Message.Contains("The column cannot contain null values") || e.Error.Message.Contains("空值"))
//                 {
//                     str += string.Format("Conflict Type:{0}\r\n", e.Conflict.Type.ToString());
//                     str += string.Format("Error.Message:{0}\r\n", e.Error.Message.ToString());
//                     WriteLog("RegularSynchronize-->Synchronize-->localProvider_ApplyChangeFailed", new Exception(str));
//                 }
                e.Action = ApplyAction.Continue;
            }
            else 
            {
//                 str += string.Format("Conflict Type:{0}\r\n", e.Conflict.Type.ToString());
//                 str += string.Format("Conflict ErrorMessage:{0}\r\n", e.Conflict.ErrorMessage);
//                 str += string.Format("Conflict.Stage:{0}\r\n", e.Conflict.Stage.ToString());
//                 str += string.Format("ApplyChange Tables Count:{0}\r\n", e.Context.DataSet.Tables.Count);
//                 WriteLog("RegularSynchronize-->Synchronize-->localProvider_ApplyChangeFailed...", new Exception(str));
                e.Action = ApplyAction.RetryWithForceWrite; 
            }
        }

        

        /// <summary>
        /// 删除缓存的过时业务数据
        /// </summary>
        /// <param name="sqlCeConnection"></param>
        private void DeleteHistoryBusinessRecord(SqlCeConnection sqlCeConnection)
        {
            //客户端主程序配置文件键TableNamesNeedUpdateData维护了需要删除过时数据的业务表名称
            string localBusinessTableNames = ClientHelper.GetAppSettingValue("TableNamesNeedUpdateData");
            if (string.IsNullOrEmpty(localBusinessTableNames))
                return;
            string[] tableNames = localBusinessTableNames.Split(',');
            try
            {
                DateTime dtSystemCurrentTime =
                    ServiceClient.GetService<IFrameworkInitializeService>().GetServerCurrentTime();
                foreach (string tableName in tableNames)
                {
                    if (string.IsNullOrEmpty(tableName))
                        continue;
                    if (IsTableExists(tableName, sqlCeConnection))
                    {
                        //关联数据删除：①三个月以前创建且未修改②三个月以前修改③业务ETD超过三个月
                        string deleteOperationMessageString =
                            string.Format(
                                "DELETE FROM OperationMessages WHERE ([CreateDate] < '{2}' AND [UpdateDate] IS NULL) OR [UpdateDate] < '{2}' OR [OperationId] IN (SELECT [OperationId] FROM {0} WHERE [{1}] < '{2}' )",
                                tableName, "ETD", dtSystemCurrentTime.AddMonths(-3).ToString("yyyy-MM-dd"));
                        string delteOperationMessageString2 = string.Format("DELETE FROM OperationMessages WHERE [CreateDate] < '{0}' AND ([F5] IS NULL OR [F5]='False') AND ([F4] IS NULL OR [F4] = 'False')", dtSystemCurrentTime.AddDays(-3).ToString("yyyy-MM-dd"));
                        //业务数据删除：①业务ETD超过三个月
                        string deleteString = string.Format("DELETE FROM {0} WHERE {1}< DATEADD(month, -3, '{2}')",tableName, "ETD", dtSystemCurrentTime.ToString("yyyy-MM-dd"));
                        ExecuteSql(sqlCeConnection, deleteOperationMessageString);
                        ExecuteSql(sqlCeConnection, delteOperationMessageString2);
                        ExecuteSql(sqlCeConnection, deleteString);
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 删除fcm.FileStorage两个月前的记录
        /// </summary>
        private void DeleteHistoryRecord(SqlCeConnection conn)
        {
            try
            {
                string deleteString = "DELETE FROM [fcm.FileStorage]  WHERE CreateDate < DATEADD(month,-1,getdate())";
                ExecuteSql(conn, deleteString);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
            }
        }

        /// <summary>
        /// 备份SDF文件
        /// </summary>
        private void BackUpSDFFile()
        {
            string databasePath = GetLocalDatabasePath();

            string fullPath = GetLocalStorageFilePath();

            if (File.Exists(fullPath))
            {
                try
                {
                    string newFileName = Path.Combine(databasePath,
                        string.Format("{0}{1:yyyyMMddHHmmss}{2}", DataSynchronizationUtility.CacheDataBaseName,
                            DateTime.Now, ".sdf"));
                    File.Copy(fullPath, newFileName);
                }
                catch
                {
                    
                }
                File.Delete(fullPath);
            }
        }

        /// <summary>
        /// 将本地缓存表添加索引
        /// </summary>
        /// <param name="connectionString"></param>
        private void UpgradeTableStructure(string connectionString)
        {
            using (SqlCeConnection conn = new SqlCeConnection(connectionString))
            {
                conn.Open();
                using (SqlCeCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        if (NeedCheckingTableStructure())
                        {
                            string strColumnNames = string.Empty;
                            int realCount = 0;
                            #region 表结构检查

                            #region OperationViewOECache
                            //字段类型更改
                            strColumnNames = "ContactMail";
                            if (!IsColumnType("OperationViewOECache", strColumnNames, "NTEXT", conn))
                            {
                                cmd.CommandText = string.Format("ALTER TABLE OperationViewOECache ALTER COLUMN {0} NTEXT;", strColumnNames);
                                cmd.ExecuteNonQuery();
                            } 
                            #endregion

                            #region fcm.FileStorage
                            //fcm.FileStorage OperationID
                            strColumnNames = "'OperationID'";
                            if (!IsColumnsExists("fcm.FileStorage", strColumnNames, strColumnNames.Split(',').Length, conn))
                            {
                                cmd.CommandText = @"ALTER TABLE [fcm.FileStorage] ADD
                                                        [OperationID] [UNIQUEIDENTIFIER] NULL;";
                                cmd.ExecuteNonQuery();
                            }

                            
                            //fcm.FileStorage TypeCode
                            strColumnNames = "'TypeCode'";
                            if (!IsColumnsExists("fcm.FileStorage", strColumnNames, strColumnNames.Split(',').Length, conn))
                            {
                                cmd.CommandText = @"ALTER TABLE [fcm.FileStorage] ADD
                                                        [TypeCode] TINYINT;";
                                cmd.ExecuteNonQuery();
                            } 
                            #endregion

                            #region UserOperationLog

                            cmd.CommandText = @"DROP TABLE [UserOperationLog];";
                            cmd.ExecuteNonQuery();
                            
                            EnsureUserOperationLogTable(conn);

                            ////UserOperationLog IntranetIP
                            //strColumnNames = "'IntranetIP'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length,conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [IntranetIP] NVARCHAR(128) NULL;";
                            //    cmd.ExecuteNonQuery();

                            //}

                            ////UserOperationLog F1
                            //strColumnNames = "'F1'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length, conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F1] NTEXT NULL;";
                            //    cmd.ExecuteNonQuery();
                            //}

                            ////UserOperationLog F2
                            //strColumnNames = "'F2'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length, conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F2] NTEXT NULL;";
                            //    cmd.ExecuteNonQuery();
                            //}
                            ////UserOperationLog F3
                            //strColumnNames = "'F3'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length, conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F3] NTEXT NULL;";
                            //    cmd.ExecuteNonQuery();
                            //}
                            ////UserOperationLog F4
                            //strColumnNames = "'F4'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length,
                            //        conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F4] BIT NOT NULL DEFAULT 0;";
                            //    cmd.ExecuteNonQuery();
                            //}
                            ////UserOperationLog F5
                            //strColumnNames = "'F5'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length, conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F5] BIT NULL;";
                            //    cmd.ExecuteNonQuery();
                            //}
                            ////UserOperationLog F6
                            //strColumnNames = "'F6'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length,conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F6] INT NULL;";
                            //    cmd.ExecuteNonQuery();
                            //}
                            ////UserOperationLog F7
                            //strColumnNames = "'F7'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length, conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F7] INT NULL;";
                            //    cmd.ExecuteNonQuery();
                            //}
                            ////UserOperationLog F8
                            //strColumnNames = "'F8'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length, conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F8] NTEXT NULL;";
                            //    cmd.ExecuteNonQuery();
                            //}
                            ////UserOperationLog F9
                            //strColumnNames = "'F9'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length, conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F9] NTEXT NULL;";
                            //    cmd.ExecuteNonQuery();
                            //}
                            ////UserOperationLog F10
                            //strColumnNames = "'F10'";
                            //if (!IsColumnsExists("UserOperationLog", strColumnNames, strColumnNames.Split(',').Length, conn))
                            //{
                            //    cmd.CommandText = @"ALTER TABLE [UserOperationLog] ADD [F10] NTEXT NULL ;";
                            //    cmd.ExecuteNonQuery();
                            //} 
                            #endregion
                            #endregion

                            #region 索引检查
                            //OperationMessages
                            strColumnNames = "'ID','OperationID','IMessageID','MessageID','StageType','Autoassociative','F4','F5','CreateDate'";
                            if (!IsIndexsExists("OperationMessages", strColumnNames, strColumnNames.Replace("'", "").Split(',').Length, conn, out realCount))
                            {
                                if (realCount != 0)
                                {
                                    try
                                    {
                                        cmd.CommandText = "DROP INDEX OperationMessages.ix_OperationMessages;";
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                }
                                cmd.CommandText = string.Format("Create Index  ix_OperationMessages on OperationMessages({0})", strColumnNames.Replace("'", ""));
                                cmd.ExecuteNonQuery();
                            }

                            //OperationViewOECache
                            strColumnNames = "'OceanBookingID','NO','SONO','CarrierID','CarrierCode','POLID','PODID','VesselID','VoyageID','IsValid','CompanyID','OPD','SOPV','DocByID','SOByID','SalesID','CustomerServiceID','ETD','AdjSOCopy','ETA','ANCopy','ContactMailDomain','F4','OverSeasFilerID','F5','CreateDate'";
                            if (!IsIndexsExists("OperationViewOECache", strColumnNames, strColumnNames.Replace("'", "").Split(',').Length, conn, out realCount))
                            {
                                if (realCount != 0)
                                {
                                    try
                                    {
                                        cmd.CommandText = "DROP INDEX OperationViewOECache.ix_OperationViewOECache;";
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                    try
                                    {
                                        cmd.CommandText = "DROP INDEX OperationViewOECache.ix_OperationViewOECache_1;";
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                }
                                strColumnNames = "'OceanBookingID','NO','SONO','CarrierID','CarrierCode','POLID','PODID','VesselID','VoyageID','IsValid','CompanyID','OPD'";
                                cmd.CommandText = string.Format("Create Index ix_OperationViewOECache on OperationViewOECache({0})", strColumnNames.Replace("'", ""));
                                cmd.ExecuteNonQuery();
                                strColumnNames = "'SOPV','DocByID','SOByID','SalesID','CustomerServiceID','ETD','AdjSOCopy','ETA','ANCopy','ContactMailDomain','F4','OverSeasFilerID','F5','CreateDate'";
                                cmd.CommandText = string.Format("Create Index ix_OperationViewOECache_1 on OperationViewOECache({0})", strColumnNames.Replace("'", ""));
                                cmd.ExecuteNonQuery();
                            }


                            //OperationContactCache
                            strColumnNames = "'ID','Mail','CustomerID','IsCustomer','IsCarrier','IsOE','IsOI','IsAE','IsAI','IsTRK','IsOther','F4','F5','UpdateDate'";
                            if (!IsIndexsExists("OperationContactCache", strColumnNames, strColumnNames.Replace("'", "").Split(',').Length, conn, out realCount))
                            {
                                if (realCount != 0)
                                {
                                    try
                                    {
                                        cmd.CommandText = "DROP INDEX OperationContactCache.ix_OperationContactCache;";
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                }

                                cmd.CommandText = string.Format("Create Index ix_OperationContactCache on OperationContactCache({0})", strColumnNames.Replace("'", ""));
                                cmd.ExecuteNonQuery();
                            }

                            //OperationContactDomainCaches
                            strColumnNames = "'ID','MailDomain','CustomerID','IsCustomer','IsCarrier','F4','F5','CreateDate'";
                            if (!IsIndexsExists("OperationContactDomainCaches", strColumnNames, strColumnNames.Replace("'", "").Split(',').Length, conn, out realCount))
                            {
                                if (realCount != 0)
                                {
                                    try
                                    {
                                        cmd.CommandText = "DROP INDEX OperationContactDomainCaches.ix_OperationContactDomainCaches;";
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                }

                                cmd.CommandText = string.Format("Create Index ix_OperationContactDomainCaches on OperationContactDomainCaches({0})", strColumnNames.Replace("'", ""));
                                cmd.ExecuteNonQuery();
                            }


                            //Languages
                            strColumnNames = "'ID','FullName','FormName','ControlName','ChineseValue','EnglishValue','CreateDate'";
                            if (!IsIndexsExists("Languages", strColumnNames, strColumnNames.Replace("'", "").Split(',').Length, conn, out realCount))
                            {
                                if (realCount != 0)
                                {
                                    try
                                    {
                                        cmd.CommandText = "DROP INDEX Languages.ix_Languages;";
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                }

                                cmd.CommandText = string.Format("Create Index ix_Languages on Languages({0})", strColumnNames.Replace("'", ""));
                                cmd.ExecuteNonQuery();
                            }

                            //UserOperationLog
                            strColumnNames = "'ID','UserID','InternetIP','IntranetIP','MacAddress','FunctionName','OperationContent','OperationDate','AssemblyName'";
                            if (!IsIndexsExists("UserOperationLog", strColumnNames, strColumnNames.Replace("'", "").Split(',').Length, conn, out realCount))
                            {
                                if (realCount != 0)
                                {
                                    try
                                    {
                                        cmd.CommandText = "DROP INDEX UserOperationLog.ix_UserOperationLog;";
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                }

                                cmd.CommandText = string.Format("Create Index ix_UserOperationLog on UserOperationLog({0})", strColumnNames.Replace("'", ""));
                                cmd.ExecuteNonQuery();
                            }


                            //fcm.FileStorage
                            strColumnNames = "'ID','Name','CreateDate','OperationID'";
                            if (!IsIndexsExists("fcm.FileStorage", strColumnNames, strColumnNames.Replace("'", "").Split(',').Length, conn, out realCount))
                            {
                                if (realCount != 0)
                                {
                                    try
                                    {
                                        cmd.CommandText = "DROP INDEX [fcm.FileStorage].ix_fcm_FileStorage;";
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch
                                    {
                                    }
                                }

                                cmd.CommandText = string.Format("Create Index ix_fcm_FileStorage on [fcm.FileStorage] ({0})", strColumnNames.Replace("'", ""));
                                cmd.ExecuteNonQuery();
                            }
                            #endregion

                            ClientConfigOperation.AddValue(_CacheTableVersionNoKey, LocalData.CacheTableVersionNo.ToString(CultureInfo.InvariantCulture));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(CommonHelper.BuildExceptionString(ex));
                        ClientConfigOperation.AddValue(_CacheTableVersionNoKey, "0");
                    }
                    finally
                    {
                        conn.Close();
                    }
                    #region Comment Code
//                    try
//                    {
//                        if (!GetLocalClientConfig(arrSetLocalTableIndex))
//                        {
//                            #region 创建各表索引
//                            //OperationMessages
//                            bool exsitLocalTableIndex_old = GetLocalClientConfig("SetLocalTableIndex");
//                            if (exsitLocalTableIndex_old)
//                            {
//                                cmd.CommandText = "DROP INDEX OperationMessages.ix_OperationMessages;";
//                                cmd.ExecuteNonQuery();
//                            }
//                            cmd.CommandText = string.Format(
//                                @"Create Index  ix_OperationMessages on OperationMessages([ID],[OperationID],[OperationType],[IMessageID],[MessageID],[StageType],[Autoassociative],[F4],[F5],[F6],[CreateDate])");
//                            cmd.ExecuteNonQuery();


//                            //OperationViewOECache
//                            if (exsitLocalTableIndex_old)
//                            {
//                                cmd.CommandText = "DROP INDEX OperationViewOECache.ix_OperationViewOECache;";
//                                cmd.ExecuteNonQuery();
//                            }
//                            cmd.CommandText =
//                                @"Create Index ix_OperationViewOECache on OperationViewOECache([OceanBookingID],[NO],[SONO],[CarrierID],[CarrierCode],[POLID],[PODID],[VesselID],[VoyageID],[IsValid],[Type],[CompanyID],[OPD],[SOPV],[APR],[MBLR])";
//                            cmd.ExecuteNonQuery();

//                            if (exsitLocalTableIndex_old)
//                            {
//                                cmd.CommandText = "DROP INDEX OperationViewOECache.ix_OperationViewOECache_1;";
//                                cmd.ExecuteNonQuery();
//                            }
//                            cmd.CommandText =
//                                @"Create Index ix_OperationViewOECache_1 on OperationViewOECache([DocByID],[SOByID],[SalesID],[CustomerServiceID],[ETD],[AdjSOCopy],[OperationType],[ETA],[ANCopy],[ContactMailDomain],[F4],[OverSeasFilerID],[F5],[F6],[CreateDate])";
//                            cmd.ExecuteNonQuery();



//                            //OperationContactCache 
//                            if (exsitLocalTableIndex_old)
//                            {
//                                cmd.CommandText = "DROP INDEX OperationContactCache.ix_OperationContactCache;";
//                                cmd.ExecuteNonQuery();
//                            }
//                            cmd.CommandText =
//                                @"Create Index ix_OperationContactCache on OperationContactCache([ID],[Mail],[CustomerID],[IsCustomer],[IsCarrier],[IsOE],[IsOI],[IsAE],[IsAI],[IsTRK],[IsOther],[F4],[F5],[F6],[UpdateDate])";

//                            cmd.ExecuteNonQuery();


//                            //OperationContactDomainCaches   
//                            if (exsitLocalTableIndex_old)
//                            {
//                                cmd.CommandText =
//                                    "DROP INDEX OperationContactDomainCaches.ix_OperationContactDomainCaches;";
//                                cmd.ExecuteNonQuery();
//                            }
//                            cmd.CommandText =
//                                @"Create Index ix_OperationContactDomainCaches on OperationContactDomainCaches([ID],[MailDomain],[CustomerID],[IsCustomer],[IsCarrier],[F4],[F5],[F6],[CreateDate])";
//                            cmd.ExecuteNonQuery();

//                            //Languages       
//                            if (exsitLocalTableIndex_old)
//                            {
//                                cmd.CommandText = "DROP INDEX Languages.ix_Languages;";
//                                cmd.ExecuteNonQuery();
//                            }
//                            cmd.CommandText =
//                                @"Create Index ix_Languages on Languages([ID],[FullName],[FormName],[ControlName],[ChineseValue],[EnglishValue],[Type],[CreateDate])";
//                            cmd.ExecuteNonQuery();


//                            //UserOperationLog 
//                            if (exsitLocalTableIndex_old)
//                            {
//                                cmd.CommandText = "DROP INDEX UserOperationLog.ix_UserOperationLog;";
//                                cmd.ExecuteNonQuery();
//                            }
//                            cmd.CommandText =
//                                @"Create Index ix_UserOperationLog on UserOperationLog([ID],[UserID],[InternetIP],[IntranetIP],[MacAddress],[FunctionName],[OperationContent],[OperationDate],[AssemblyName])";
//                            cmd.ExecuteNonQuery();

//                            ClientConfigOperation.AddValue(arrSetLocalTableIndex, true.ToString());
//                            #endregion
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        LogHelper.SaveLog(CommonHelper.BuildExceptionString(ex));
//                        ClientConfigOperation.AddValue(_CacheTableVersionNoKey, "0");
//                    }

//                    try
//                    {
//                        if (!GetLocalClientConfig(arrUpdateLocalTableStructure))
//                        {
//                            #region 更新表[OperationViewOECache]、[FileStorage]、结构
//                            cmd.CommandText = @"ALTER TABLE OperationViewOECache
//                                                                                ALTER COLUMN ContactMail NTEXT;";
//                            cmd.ExecuteNonQuery();


//                            cmd.CommandText = @"ALTER TABLE [fcm.FileStorage]  ADD  OperationID UNIQUEIDENTIFIER ,TypeCode TINYINT ;";
//                            cmd.ExecuteNonQuery();

//                            cmd.CommandText = @"Create INDEX  ix_fcm_FileStorage on [fcm.FileStorage](
//                                                                            [ID],   
//                                                                            [Name] ,
//                                                                            [CreateDate] ,
//                                                                            [UploadState],
//                                                                            [OperationID] ,
//                                                                            [TypeCode] );";
//                            cmd.ExecuteNonQuery();

//                            ClientConfigOperation.AddValue(arrUpdateLocalTableStructure, true.ToString());
//                            #endregion
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        LogHelper.SaveLog(ex.Message);
//                        ClientConfigOperation.AddValue(arrUpdateLocalTableStructure, false.ToString());
//                    }
//                    finally
//                    {
//                        conn.Close();
//                    } 
                    #endregion
                }
            }
        }

        #region SQL Server Compact

        /* 缓存数据库系统表
         * --Get all the columns of the database
         * SELECT * FROM INFORMATION_SCHEMA.COLUMNS
         * -- Get all the indexes of the database
         * SELECT * FROM INFORMATION_SCHEMA.INDEXES
         * -- Get all the indexes and columns of the database
         * SELECT * FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
         * -- Get all the datatypes of the database
         * SELECT * FROM INFORMATION_SCHEMA.PROVIDER_TYPES
         * -- Get all the tables of the database
         * SELECT * FROM INFORMATION_SCHEMA.TABLES
         * -- Get all the constraint of the database
         * SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
         * -- Get all the foreign keys of the database
         * SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS
         */


        /// <summary>
        /// 查询表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="sqlCeConnection">SqlCe连接</param>
        /// <returns></returns>
        private bool IsTableExists(string tableName, SqlCeConnection sqlCeConnection)
        {
            string queryString = string.Format("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}'", tableName);
            object result = ExecuteScalar(sqlCeConnection, queryString);
            return result != null;
        }

        /// <summary>
        /// 查询列在表中是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colNames">列名</param>
        /// <param name="colCount">列数量</param>
        /// <param name="sqlCeConnection">SqlCe连接</param>
        /// <returns></returns>
        private bool IsColumnsExists(string tableName,string colNames,int colCount, SqlCeConnection sqlCeConnection)
        {
            //列数量
            string queryString = string.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}' AND COLUMN_NAME IN({1})", tableName, colNames);
            object result = ExecuteScalar(sqlCeConnection, queryString);
            return result != null && result.ToString().Equals(colCount.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 查询列及对应列类型在表中是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colName">列名</param>
        /// <param name="colType">列类型</param>
        /// <param name="sqlCeConnection">SqlCe连接</param>
        /// <returns></returns>
        private bool IsColumnType(string tableName, string colName, string colType, SqlCeConnection sqlCeConnection)
        {
            //列数量
            string queryString = string.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}' AND COLUMN_NAME = '{1}' AND DATA_TYPE='{2}' ", tableName, colName, colType);
            object result = ExecuteScalar(sqlCeConnection, queryString);
            return result != null && result.ToString().Equals("1");
        }

        /// <summary>
        /// 查询索引在表中是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="indexNames">列名</param>
        /// <param name="indexCount">列数量</param>
        /// <param name="sqlCeConnection">SqlCe连接</param>
        /// <param name="realIndexCount">实际索引数量</param>
        /// <returns></returns>
        private bool IsIndexsExists(string tableName, string indexNames, int indexCount, SqlCeConnection sqlCeConnection,out int realIndexCount)
        {
            realIndexCount = 0;
            //列数量
            string queryString = string.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.INDEXES WHERE TABLE_NAME = '{0}' AND COLUMN_NAME IN({1}) AND INDEX_NAME LIKE 'ix_%' ", tableName, indexNames);
            object result = ExecuteScalar(sqlCeConnection, queryString);
            if (result == null)
                realIndexCount = 0;
            else
            {
                try
                {
                    realIndexCount = Int32.Parse(result.ToString());
                }
                catch (Exception ex)
                {
                    realIndexCount = 0;
                }
            }
            return realIndexCount != 0 && realIndexCount.ToString().Equals(indexCount.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 确保数据库连接已打开
        /// </summary>
        /// <param name="connection"></param>
        private void EnsureConnectionOpen(SqlCeConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="connection">数据库连接实例</param>
        /// <param name="commandText">SQL字符串</param>
        private void ExecuteSql(SqlCeConnection connection, string commandText)
        {
            EnsureConnectionOpen(connection);
            SqlCeCommand command = new SqlCeCommand(commandText, connection);
            command.ExecuteNonQuery();
        }
        /// <summary>
        /// 执行SQL，并返回单一值
        /// </summary>
        /// <param name="connection">数据库连接实例</param>
        /// <param name="commandText">SQL字符串</param>
        /// <returns></returns>
        private object ExecuteScalar(SqlCeConnection connection, string commandText)
        {
            EnsureConnectionOpen(connection);
            SqlCeCommand command = new SqlCeCommand(commandText, connection);
            return command.ExecuteScalar();
        } 
        #endregion

        
        #endregion

    }
}
