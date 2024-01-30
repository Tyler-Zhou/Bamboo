using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using System.Windows.Forms;

namespace ICP3.MailStoring
{
    static class Program
    {
        static MailStoringService _MailStoringService = new MailStoringService();
        static System.Timers.Timer _Timer = new System.Timers.Timer();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            _MailStoringService.LastRemoveOlderFolderAndMailRec = DateTime.Now;

            //(new frmConsole()).Show();
            //启动应用程序后，遍历所有已记录邮件，记录到_cache
            _MailStoringService.InitailizeMailRec();

            //启动计时器

            _Timer.Interval = 3000;
            _Timer.Elapsed += t1_Elapsed;
            _Timer.Start();

            Console.Read();
        }

        static void t1_Elapsed(object sender, ElapsedEventArgs e)
        {

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
            //如果距离上一次执行时间，超过1天以上，则执行
            if (DateTime.Now.ToString("HH:mm") == AppConfiguration.GetRemoveOlderMailSchedule
                || (
                        _MailStoringService.LastRemoveOlderFolderAndMailRec != null &&
                        ((TimeSpan)(DateTime.Now - _MailStoringService.LastRemoveOlderFolderAndMailRec.Value)).TotalDays >= 1
                   ))
            {
                _MailStoringService.RemoveOlderFolderAndMailRec();
            }
        }
    }
}
