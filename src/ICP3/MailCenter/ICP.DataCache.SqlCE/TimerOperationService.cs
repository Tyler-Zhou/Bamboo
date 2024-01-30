using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using System;
using System.Threading;


namespace ICP.DataOperation.SqlCE
{
    /// <summary>
    /// 计时器操作服务
    /// </summary>
    public class TimerOperationService
    {
        #region Member & Variables
        /// <summary>
        /// 读取关联数据计时器
        /// </summary>
        private Timer tReadOM;
        /// <summary>
        /// 读取业务联系人数据计时器
        /// </summary>
        private Timer tReadOC;
        /// <summary>
        /// 上传计时器
        /// </summary>
        private Timer _UploadRelationTimer;
        /// <summary>
        /// 备份邮件
        /// </summary>
        private Timer _BackUpMailTimer;
        /// <summary>
        /// 上传操作日志
        /// </summary>
        private Timer _UploadOLogsTimer;
        /// <summary>
        /// Lock
        /// </summary>
        private static object lockObj = new object();
        /// <summary>
        /// 单例模式
        /// </summary>
        private static TimerOperationService helper = null;

        /// <summary>
        /// 可用
        /// </summary>
        public bool Enabled { get; set; }

        #region 计时器(单例模式)
        /// <summary>
        /// 单一实例
        /// </summary>
        public static TimerOperationService Current
        {
            get
            {
                if (helper == null)
                {
                    lock (lockObj)
                    {
                        if (helper == null)
                            helper = new TimerOperationService();
                    }
                }
                return helper;
            }
        }
        #endregion
        #endregion

        #region Services

        /// <summary>
        /// 缓存文件操作服务
        /// </summary>
        public ILocalBusinessCacheDataOperation LocalBusinessCacheDataOperation
        {
            get { return ServiceClient.GetClientService<ILocalBusinessCacheDataOperation>(); }
        }
        #endregion

        #region Public Method
        /// <summary>
        /// 开始同步
        /// </summary>
        public void Start()
        {
            try
            {
                Enabled = true;
                tReadOM = new Timer(ReadLocalFileOperationMessage, null
                    , (long)TimeSpan.FromMinutes(LocalData.DelayedStartTimer).TotalMilliseconds
                    , (long)TimeSpan.FromMinutes(TimerManager.GetAppSettingValue(ClientConstants.ReadLocalFileOperationMessageKey, 1)).TotalMilliseconds);

                tReadOC = new Timer(ReadLocalFileOperationContact, null
                    , (long)TimeSpan.FromMinutes(LocalData.DelayedStartTimer).TotalMilliseconds
                    , (long)TimeSpan.FromMinutes(TimerManager.GetAppSettingValue(ClientConstants.ReadLocalFileOperationContactKey, 1)).TotalMilliseconds);
                //上传是否启用
                if (LocalData.UploadIntervalRelation > 0)
                {
                    _UploadRelationTimer = new Timer(UploadMessageRelation4Batch, null
                    , (long)TimeSpan.FromMinutes(LocalData.DelayedStartTimer).TotalMilliseconds
                    , (long)TimeSpan.FromMinutes(LocalData.UploadIntervalRelation).TotalMilliseconds);

                }
                //上传操作日志
                if (LocalData.UploadIntervalOperationLog > 0)
                {
                    _UploadOLogsTimer = new Timer(UploadOperationLog4Batch, null
                        , (long)TimeSpan.FromMinutes(LocalData.DelayedStartTimer).TotalMilliseconds
                        , (long)TimeSpan.FromMinutes(LocalData.UploadIntervalOperationLog).TotalMilliseconds);
                }

                //备份邮件是否启用
                if (LocalData.UploadIntervalBackUpMail > 0)
                {
                    _BackUpMailTimer = new Timer(RegularBackUpMail, null
                        , (long)TimeSpan.FromMinutes(LocalData.DelayedStartTimer).TotalMilliseconds
                        , (long)TimeSpan.FromMinutes(LocalData.UploadIntervalBackUpMail).TotalMilliseconds);
                }
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
            if (tReadOM != null)
            {
                tReadOM.Dispose();
                tReadOM = null;
            }
            if (tReadOC != null)
            {
                tReadOC.Dispose();
                tReadOC = null;
            }
            if (_UploadRelationTimer != null)
            {
                _UploadRelationTimer.Dispose();
                _UploadRelationTimer = null;
            }
            if (_UploadOLogsTimer != null)
            {
                _UploadOLogsTimer.Dispose();
                _UploadOLogsTimer = null;
            }
            if (_BackUpMailTimer != null)
            {
                _BackUpMailTimer.Dispose();
                _BackUpMailTimer = null;
            }
        } 
        #endregion

        #region Private Method
        /// <summary>
        /// 批量上传关联信息
        /// </summary>
        /// <param name="state"></param>
        private void UploadMessageRelation4Batch(object state)
        {
            if (!Enabled)
                StopTimer(_UploadRelationTimer);
            if (LocalBusinessCacheDataOperation != null)
                LocalBusinessCacheDataOperation.UploadMessageRelation4Batch();
        }

        /// <summary>
        /// 批量上传操作日志
        /// </summary>
        /// <param name="state"></param>
        private void UploadOperationLog4Batch(object state)
        {
            if (!Enabled)
                StopTimer(_UploadOLogsTimer);
            if (LocalBusinessCacheDataOperation != null)
                LocalBusinessCacheDataOperation.UploadUserOperationLog();
        }

        /// <summary>
        /// 备份邮件
        /// </summary>
        /// <param name="state"></param>
        private void RegularBackUpMail(object state)
        {
            if (!Enabled)
                StopTimer(_BackUpMailTimer);
            if (!LocalData.NeedBackUpMail)
            {
                _BackUpMailTimer.Dispose();
                _BackUpMailTimer = null;
                return;
            }
            if (LocalBusinessCacheDataOperation != null)
                LocalBusinessCacheDataOperation.UploadMailEntity4Batch();
        }

        /// <summary>
        /// 读取本地文件：关联信息
        /// </summary>
        private void ReadLocalFileOperationMessage(object state)
        {
            if (!Enabled)
                StopTimer(tReadOM);
            if (LocalBusinessCacheDataOperation != null)
                LocalBusinessCacheDataOperation.ReadLocalFileOperationMessage();
        }

        /// <summary>
        /// 读取本地文件：操作联系人
        /// </summary>
        private void ReadLocalFileOperationContact(object state)
        {
            if (!Enabled)
                StopTimer(tReadOC);
            if (LocalBusinessCacheDataOperation != null)
                LocalBusinessCacheDataOperation.ReadLocalFileOperationContact();
        }

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
        #endregion
    }
}
