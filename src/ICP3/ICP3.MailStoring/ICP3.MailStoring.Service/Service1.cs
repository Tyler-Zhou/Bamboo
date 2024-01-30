using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace ICP3.MailStoring.Service
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        MailStoringService _MailStoringService;
        Timer _Timer = new Timer();

        protected override void OnStart(string[] args)
        {
            _MailStoringService = new MailStoringService(this);

            //启动计时器
            _Timer.Interval = 70000;
            _Timer.Elapsed += t1_Elapsed;
            _Timer.Start();
        }

        bool _IsInitailizedMailRec = false;

        void t1_Elapsed(object sender, ElapsedEventArgs e)
        {
            //启动应用程序后，遍历所有已记录邮件，记录到_cache
            if (_IsInitailizedMailRec == false)
            {
                _IsInitailizedMailRec = true;
                _MailStoringService.InitailizeMailRec();
            }

            ///如果系统正在执行其它任务，则退出（保证不同时执行两个以上的任务）。
            if (_MailStoringService.CurrentStatus != ICP3.MailStoring.MailStoringService.Status.Pending) return;

            //记录保存邮件的执行计划 (设置n分钟执行一次)
            //如果没有执行过此任务，则执行
            //如果距离上一次执行时间，超过10分钟以上，则执行
            if (_MailStoringService.LastRecordAndSaveMail == null
                || (
                        ((TimeSpan)(DateTime.Now - _MailStoringService.LastRecordAndSaveMail.Value)).TotalMinutes 
                        >=
                        AppConfiguration.GetRecordSaveMailSchedule))
            {
                _MailStoringService.RecordAndSaveMail();
            }

            //删除过期邮件的执行计划 (设置每天几点执行一次，例19:30)
            //如果刚好到计划的时间(例：19:30)，则执行
            //如果距离上一次执行时间，超过1天以上，则执行。
            if (DateTime.Now.ToString("HH:mm") == AppConfiguration.GetRemoveOlderMailSchedule 
                || (
                        ((TimeSpan)(DateTime.Now - _MailStoringService.LastRemoveOlderFolderAndMailRec.Value)).TotalDays >= 1
                   ))
            {
                _MailStoringService.RemoveOlderFolderAndMailRec();
            }
        }

        protected override void OnStop()
        {
            if (_MailStoringService.CurrentStatus != MailStoringService.Status.Pending)
            {
                _MailStoringService.Log("请稍后停止服务，处理邮件的任务还未完成。", EventLogEntryType.Error);
                this.ExitCode = -1;
                return;
            }

            this.RequestAdditionalTime(60000);
            _Timer.Stop();
            //把内存记录保存到本地，避免重启再次花大量时间加载。
            _MailStoringService.SaveCache();
            
            this.ExitCode = 0;
        }
    }
}
