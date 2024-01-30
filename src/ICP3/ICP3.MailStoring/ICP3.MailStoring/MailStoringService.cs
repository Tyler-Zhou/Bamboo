using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using ICP3.MailStoring.DataSet1TableAdapters;
using System.ServiceProcess;

namespace ICP3.MailStoring
{
    /// <summary>
    /// 邮件归档Service
    /// </summary>
    public class MailStoringService
    {

        ServiceBase _ServiceBase
        {
            get;
            set;
        }

        public MailStoringService(ServiceBase sb)
        {
            _ServiceBase = sb;
        }

        /// <summary>
        /// 定义运行的状态
        /// </summary>
        /// <remarks>
        /// 用于避免同一个时间内，同时执行两个以上的任务。
        /// </remarks>
        public enum Status
        {
            Pending,
            InitailizingCache,
            RecordingAndSavingNewMail,
            RemovingOlderMail
        }


        Status _CurrentStatus;
        /// <summary>
        /// 获取或设置当前运行的状态
        /// </summary>
        public Status CurrentStatus
        {
            get
            {
                return _CurrentStatus;
            }
            set
            {
                _CurrentStatus = value;
            }
        }

        //文件夹控制器
        MailFolderController _MailFolder = new MailFolderController();
        //邮件文件控制器
        MailFileController _MailFileController = new MailFileController();
        //邮件记录控制器
        MailRecController _MailRecController = new MailRecController();

        public MailStoringService()
        {
            CurrentStatus = Status.Pending;
        }

        /// <summary>
        /// 启动应用程序后，遍历所有已记录邮件，记录到_cache
        /// </summary>
        public void InitailizeMailRec()
        {
            try
            {
                CurrentStatus = Status.RecordingAndSavingNewMail;

                Log("开始初始化已记录的邮件。", EventLogEntryType.Information); 

                DateTime start = DateTime.Now;

                int num1 = _MailRecController.InitailizeCache();

                Log("初始化已记录的邮件:" + num1.ToString() + "条。耗时：" + ((TimeSpan)(DateTime.Now - start)).TotalSeconds.ToString() + "秒", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Log("失败：初始化已记录邮件。" + "/n/r" + ex.Message + "/n/r" + ex.StackTrace, EventLogEntryType.Error);
            }
            finally
            {
                CurrentStatus = Status.Pending;
            }

            CurrentStatus = Status.Pending;
        }

        /// <summary>
        /// 最近一次执行时间，关于记录新邮件的MsgID和邮件副本的路径
        /// </summary>
        public DateTime? LastRecordAndSaveMail
        {
            get;
            set;
        }

        /// <summary>
        /// 记录新邮件的MsgID和邮件副本的路径
        /// 邮件文件存储到邮件数据库
        /// </summary>
        public void RecordAndSaveMail()
        {
            ///如果系统正在执行其它任务，则退出（保证不同时执行两个以上的任务）。
            if (CurrentStatus != Status.Pending) return;

            try
            {
                CurrentStatus = Status.RecordingAndSavingNewMail;

                DateTime start = DateTime.Now;

                int num1 = SetMsgidAndFilepath();
                string ts1 = ((TimeSpan)(DateTime.Now - start)).TotalSeconds.ToString() + "秒";
                Log("记录新邮件的MsgID:" + num1.ToString() + "条，耗时：" + ts1, EventLogEntryType.Information);

                start = DateTime.Now;
                int num2 = SaveMailfilesToDB();
                string ts2 = ((TimeSpan)(DateTime.Now - start)).TotalSeconds.ToString() + "秒";

                Log("邮件文件存储到邮件数据库:" + num2 + "条，耗时：" + ts2 + "秒", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Log("失败：记录新邮件的MsgID和邮件副本的路径，并且邮件文件存储到邮件数据库。" + "/n/r" + ex.Message + "/n/r" + ex.StackTrace, EventLogEntryType.Error);
            }
            finally
            {
                LastRecordAndSaveMail = DateTime.Now;
                CurrentStatus = Status.Pending;
            }
        }

        /// <summary>
        /// 将缓存保存到本地
        /// </summary>
        public void SaveCache()
        {
            try
            {
                _MailRecController.SaveCache();
                Log("已保存缓存到本地。", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Log("失败，保存缓存到本地。" + "/n/r" + ex.Message + "/n/r" + ex.StackTrace, EventLogEntryType.Error);
                _MailRecController.RemoveCache();
            }
        }

        /// <summary>
        /// 记录新邮件的MsgID和邮件副本的路径
        /// </summary>
        /// <remarks>
        ///     获取new文件夹中的所有邮件副本文件列表
        ///     如果文件列表为空，则退出此场景，否则继续。
        ///     装载邮件副本文件
        ///     获取messageid
        ///     将messageid /文件新路径保存到本地db的邮件记录表。
        ///          如果已存在messageid的记录，则更新文件新路径字段
        ///          否则，创建新记录
        ///      移动邮件文件到[日期+已记录]文件夹
        /// 设计思考：
        ///      由于已记录的邮件文件一周内就达到10万条级别，所以文件夹按[日期+已记录]命名，提高获取效率。如果文件夹[日期+已记录]不存在，则创建此目录。
        ///      保存到本地db的动作，与移动邮件文件，必须在同一个事务中。所以须先执行保存本地db，成功后再移动邮件文件，如果移动邮件文件失败，必须撤消保存本地db的动作。
        /// </remarks>
        int SetMsgidAndFilepath()
        {
            QueriesTableAdapter qta = new QueriesTableAdapter();

            //获取邮件副本文件列表(路径)
            string[] files = _MailFolder.GetNewMailFiles();
            Log("检测到新邮件，" + files.Length.ToString() + "个。", EventLogEntryType.Information); 

            //获取邮件文件的MessageID
            string newFilePath = null;
            string msgID;
            int demoCount = 0;
            foreach (var file in files)
            {
                //移动邮件到已记录文件夹
                try
                {
                    newFilePath = _MailFolder.MoveMailFileToRecFolder(file);
                }
                catch (IOException iex)
                {
                    //如果文件没有找到，或目标文件已存在
                    //则放弃这个文件的记录。
                    continue;
                }
                catch
                {
                    throw;
                }

                //获取邮件文件的MessageID
                msgID = _MailFileController.GetMsgidFromMailFile(newFilePath);

                //记录新邮件的MsgID和邮件副本的路径
                _MailRecController.SetMsgidAndFilepath(msgID, newFilePath);

                ////测试用。正式使时，需要注释此代码
                ////默认此邮件关联到一票业务。
                //if (demoCount <= 5000)
                //    qta.InsertIMessage(msgID);

                demoCount++;
            }
            return files.Length;
        }

        /// <summary>
        /// 邮件文件存储到邮件数据库
        /// </summary>
        /// <remarks>
        /// 触发：系统计划任务，定期执行一次（可配置，默认15分钟一次）
        /// 场景：
        ///     如果上一次的计划任务仍然在执行，退出此场景，否则继续。
        ///     获取本地db的邮件记录表 (条件是有文件路径 and 已关联=true and 已归档=false)
        ///     如果邮件记录为空，则退出此场景，否则继续。
        ///     将邮件文件存储到邮件数据库
        ///     标识本地db的邮件记录表，设置 已归档=true
        ///     删除邮件副本文件
        /// </remarks>
        int SaveMailfilesToDB()
        {
            //获取未归档的邮件记录(条件是有文件路径 and 已关联=true and 已归档=false)
            List<MailRecModule> handledMailRecs = new List<MailRecModule>();
            List<MailRecModule> unsavedMailRecs = _MailRecController.GetUnsavedMailRecs(handledMailRecs);
            Log("计划即将上传的邮件有：" + unsavedMailRecs.Count + "个", EventLogEntryType.Information);

            //如果记录太多。则需要考虑分批存储到邮件数据库。
            ICP3.MailStoring.DataSet1TableAdapters.QueriesTableAdapter uta = new DataSet1TableAdapters.QueriesTableAdapter();

            List<MailRecModule> splitedUnsavedMailRecs = null;
            int i = 0;
            foreach(var mailRec in unsavedMailRecs)
            {
                if (splitedUnsavedMailRecs == null)
                {
                    splitedUnsavedMailRecs = new List<MailRecModule>();
                }
                splitedUnsavedMailRecs.Add(mailRec);
                
                if (splitedUnsavedMailRecs.Count==AppConfiguration.GetBatchUpload || i==(unsavedMailRecs.Count() - 1))
                {
                    //将邮件文件存储到邮件数据库
                    //一次上传10个文件
                    _MailFileController.SaveMailfileToDB(splitedUnsavedMailRecs);

                    splitedUnsavedMailRecs = null;
                }

                i++;
            }

            for (int j = handledMailRecs.Count - 1; j >= 0; j--)
            {
                //删除邮件副本文件
                try
                {
                    _MailFolder.RemoveMailFile(handledMailRecs[j].FilePath);
                }
                catch(IOException IOEx)
                {
                    //The specified file is in use.
                }

                //删除邮件记录
                _MailRecController.RemoveOlderMailRec(handledMailRecs[j]);
            }

            return unsavedMailRecs.Count;
        }

        /// <summary>
        /// 最近一次执行时间，关于记录新邮件的MsgID和邮件副本的路径
        /// </summary>
        public DateTime? LastRemoveOlderFolderAndMailRec
        {
            get;
            set;
        }

        /// <summary>
        /// 删除过期的文件夹和邮件记录
        /// </summary>
        /// <remarks>
        /// 触发：系统计划任务，定期执行一次（可配置，默认每天晚上11点一次）
        /// 场景：
        ///     将30天（可配置）之前的已记录文件夹删除
        ///     将30天（可配置）之前的已记录邮件记录删除
        /// </remarks>
        public void RemoveOlderFolderAndMailRec()
        {
            ///如果系统正在执行其它任务，则退出（保证不同时执行两个以上的任务）。
            if (CurrentStatus != Status.Pending) return;

            try
            {
                CurrentStatus = Status.RemovingOlderMail;

                DateTime start = DateTime.Now;

                //删除过期的邮件记录
                _MailRecController.RemoveOlderMailRec();

                //删除过期的记录文件夹
                _MailFolder.RemoveOlderFolders();

                Log("删除过期的邮件。耗时：" + ((TimeSpan)(DateTime.Now-start)).TotalSeconds.ToString() + "秒", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Log("失败，删除过期的邮件。" + "/n/r" + ex.Message + "/n/r" + ex.StackTrace, EventLogEntryType.Error);
            }
            finally
            {
                LastRemoveOlderFolderAndMailRec = DateTime.Now;
                CurrentStatus = Status.Pending;
            }
        }

        /// <summary>
        /// 记录windows日志，可在事件探查器查看。
        /// </summary>
        /// <param name="sEvent"></param>
        /// <param name="type"></param>
        public void Log(string sEvent, EventLogEntryType type)
        {
            string sSource;
            string sLog;
            sSource = "ICP3 MailStoring";
            sLog = "Application";
            //sEvent = "Sample Event";

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);
            
            Console.WriteLine(sEvent);
            EventLog.WriteEntry(sSource, sEvent, type, 234);

            if (type == EventLogEntryType.Error)
                NetSendMail.MailSend("ICP MailStoring Error", sEvent);
        }
    }
}