using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ICP3.MailStoring
{
    /// <summary>
    /// 获取配置信息
    /// </summary>
    public static class AppConfiguration
    {
        static string _NewMailFolder = null;
        /// <summary>
        /// 获取新邮件文件夹
        /// </summary>
        public static string GetNewMailFolder
        {
            get
            {
                if (_NewMailFolder == null)
                {
                    _NewMailFolder = ConfigurationSettings.AppSettings.Get("NewMailFolder");
                }
                return _NewMailFolder;
            }
        }

        static string _RecordedMailFolder = null;
        /// <summary>
        /// 获取已记录邮件文件夹
        /// </summary>
        public static string GetRecordedMailFolder
        {
            get
            {
                if (_RecordedMailFolder == null)
                {
                    _RecordedMailFolder = ConfigurationSettings.AppSettings.Get("RecordedMailFolder");
                }
                return _RecordedMailFolder;
            }
        }

        static int? _RecordSaveMailSchedule = null;
        /// <summary>
        /// 获取记录保存邮件的执行计划 (设置n分钟执行一次)
        /// </summary>
        public static int? GetRecordSaveMailSchedule
        {
            get
            {
                if (_RecordSaveMailSchedule == null)
                {
                    _RecordSaveMailSchedule = Convert.ToInt16(ConfigurationSettings.AppSettings.Get("RecordSaveMailSchedule"));
                }
                return _RecordSaveMailSchedule;
            }
        }

        static string _RemoveOlderMailSchedule = null;
        /// <summary>
        /// 获取删除过期邮件的执行计划 (设置每天几点执行一次，例19:30)
        /// </summary>
        public static string GetRemoveOlderMailSchedule
        {
            get
            {
                if (_RemoveOlderMailSchedule == null)
                {
                    _RemoveOlderMailSchedule = ConfigurationSettings.AppSettings.Get("RemoveOlderMailSchedule");
                }
                return _RemoveOlderMailSchedule;
            }
        }

        static int? _ExpiredMailDays = null;
        /// <summary>
        /// 获取记录保存邮件的执行计划 (设置n分钟执行一次)
        /// </summary>
        public static int? GetExpiredMailDays
        {
            get
            {
                if (_ExpiredMailDays == null)
                {
                    _ExpiredMailDays = Convert.ToInt16(ConfigurationSettings.AppSettings.Get("ExpiredMail"));
                }
                return _ExpiredMailDays;
            }
        }

        static int? _BatchUpload = null;
        /// <summary>
        /// 获取批量上传邮件文件的数目。
        /// </summary>
        public static int? GetBatchUpload
        {
            get
            {
                if (_BatchUpload == null)
                {
                    _BatchUpload = Convert.ToInt16(ConfigurationSettings.AppSettings.Get("BatchUpload"));
                }
                return _BatchUpload;
            }
        }

        static string _BackupFolder = null;
        /// <summary>
        /// 获取备份文件夹
        /// </summary>
        public static string GetBackupFolder
        {
            get
            {
                if (_BackupFolder == null)
                {
                    _BackupFolder = ConfigurationSettings.AppSettings.Get("BackupFolder");
                }
                return _BackupFolder;
            }
        }
    }
}
