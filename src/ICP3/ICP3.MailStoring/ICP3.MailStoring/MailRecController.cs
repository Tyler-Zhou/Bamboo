using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using ICP3.MailStoring.DataSet1TableAdapters;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ICP3.MailStoring
{
    /// <summary>
    /// 邮件记录控制器
    /// 本地DB的邮件记录表的插入、更新、删除操作。
    /// </summary>
    [Serializable]
    public class MailRecController
    {
        public List<MailRecModule> _Cache = new List<MailRecModule>();

        string CACHE_FILE_PATH = AppDomain.CurrentDomain.BaseDirectory + "CacheFile.dat";

        /// <summary>
        /// 将缓存保存到本地
        /// </summary>
        public void SaveCache()
        {
            //如果文件存在，则删除
            if (IsExistedCacheFile == true)
            {
                File.Delete(CACHE_FILE_PATH);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(CACHE_FILE_PATH, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this); 
            stream.Close();
        }

        public void RemoveCache()
        {
            //如果文件存在，则删除
            if (IsExistedCacheFile == true)
            {
                File.Delete(CACHE_FILE_PATH);
            }
        }
        /// <summary>
        /// 从本地加载缓存
        /// </summary>
        public int RestoreCache()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream destream = new FileStream(CACHE_FILE_PATH, FileMode.Open, FileAccess.Read, FileShare.Read);
            _Cache = ((MailRecController)formatter.Deserialize(destream))._Cache; 
            destream.Close();
            return _Cache.Count;
        }

        /// <summary>
        /// 检查缓存文件是否存在
        /// </summary>
        bool IsExistedCacheFile
        {
            get
            {
                return File.Exists(CACHE_FILE_PATH);
            }
        }

        /// <summary>
        /// 遍历所有已记录邮件，记录到_cache
        /// </summary>
        int LoadCacheFromMailfile()
        {
            string[] files = Directory.GetFiles(AppConfiguration.GetRecordedMailFolder, "*.imap", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                _Cache.Add(new MailRecModule() { FilePath = file, CreateTime = File.GetCreationTime(file) });
            }

            MailFileController mfc = new MailFileController();
            for (int i = 0; i < _Cache.Count; i++)
            {
                _Cache[i].MsgID = mfc.GetMsgidFromMailFile(_Cache[i].FilePath);
            }

            return files.Length;
        }

        /// <summary>
        /// 初始化缓存
        /// </summary>
        /// <returns></returns>
        public int InitailizeCache()
        {
            if (IsExistedCacheFile == true)
            {
                try
                {
                    //从本地加载缓存
                    int rtv = RestoreCache();
                    Log("从本地缓存文件恢复缓存: " + rtv.ToString() + "条", EventLogEntryType.Information);
                    return rtv;
                }
                catch (Exception ex)
                {
                    //从本地缓存文件恢复缓存出错？
                    Log("失败，从本地缓存文件恢复缓存" + "/n/r" + ex.Message + "/n/r" + ex.StackTrace, EventLogEntryType.Error);

                    //遍历所有已记录邮件，记录到_cache
                    return LoadCacheFromMailfile();
                }
            }
            else
            {
                //遍历所有已记录邮件，记录到_cache
                return LoadCacheFromMailfile();
            }
        }

        /// <summary>
        /// 记录windows日志，可在事件探查器查看。
        /// </summary>
        /// <param name="sEvent"></param>
        /// <param name="type"></param>
        void Log(string sEvent, EventLogEntryType type)
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

        /// <summary>
        /// 记录新邮件的MsgID和邮件副本的路径
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="mailFilePath"></param>
        public void SetMsgidAndFilepath(string msgID, string mailFilePath)
        {
            _Cache.Add(new MailRecModule() { MsgID = msgID, FilePath = mailFilePath, CreateTime = File.GetCreationTime(mailFilePath)});
        }
        
        /// <summary>
        /// 获取未归档的邮件记录(条件是有文件路径 and 已关联=true and 已归档=false)
        /// </summary>
        public List<MailRecModule> GetUnsavedMailRecs(List<MailRecModule> handledMailRecs)
        {
            DataSet1TableAdapters.uspGetUnDocumentedOperationMailListTableAdapter d = new DataSet1TableAdapters.uspGetUnDocumentedOperationMailListTableAdapter();
            var result = d.GetData();

            //将集合转换成dictionary，以保证检索速度
            Dictionary<string, MailRecModule> dict = new Dictionary<string,MailRecModule>();
            foreach (var sub in _Cache)
            {
                if (dict.ContainsKey(sub.MsgID)) continue;
                dict.Add(sub.MsgID, sub);
            }

            //从数据库获取没有邮件实体的数据。IMessage表
            List<MailRecModule> unsavedMailRecs = new List<MailRecModule>();
            MailRecModule matched = null;
            foreach (DataRow sub in result.Rows)
            {
                //unsavedList.Add(new MailRecModule() { MsgID = sub["MessageID"].ToString(), IMessageID = sub["ID"].ToString() }); 
                if (sub["MessageID"].ToString().Trim() == "")
                    continue;
                
                //注释此代码是因为找出所有匹配的id，性能代价太大，所以改为找到一个元素就够了。
                //剩下的冗余元素，可每天的清理旧邮件执行。
                //matched = _Cache.FindAll(a => a.MsgID == sub["MessageID"].ToString().ToLower());

                if (dict.ContainsKey(sub["MessageID"].ToString().ToLower()))
                    matched = dict[sub["MessageID"].ToString().ToLower()];

                if (matched != null)
                {
                    unsavedMailRecs.Add(new MailRecModule() { MsgID = matched.MsgID, FilePath = matched.FilePath, IMessageID = sub["ID"].ToString() });

                    handledMailRecs.Add(matched);
                }

                matched = null;
            }

            //int i = 0;
            //foreach (var sub in matchedSet)
            //{
            //    if (sub.IMessageID == "4910726b-dea5-e311-af69-00226497230c")
            //        Console.Write("sdf");
                
            //    i++;
            //}

            //var unsavedList1 = matchedSet.GroupBy(pet => pet.IMessageID, pet => pet);



            //if (matchedSet.Count != unsavedList1.Count())
            //{
            //    throw new Exception("asdfa:");
            //}

            return unsavedMailRecs;
        }

        /// <summary>
        /// 删除指定的邮件记录
        /// </summary>
        public void RemoveOlderMailRec(MailRecModule mailRec)
        {
            _Cache.Remove(mailRec);
        }

        /// <summary>
        /// 删除过期的邮件记录
        /// </summary>
        public void RemoveOlderMailRec()
        {
            MailFolderController mailFolder = new MailFolderController();
            MailRecModule mailRec;
            for (int i=_Cache.Count-1;i>=0;i--)
            {
                mailRec = _Cache[i];                ;

                if (((TimeSpan)(DateTime.Now - mailRec.CreateTime)).Days >= AppConfiguration.GetExpiredMailDays)
                {
                    //删除邮件副本文件
                    try
                    {
                        mailFolder.RemoveMailFile(mailRec.FilePath);
                    }
                    catch(IOException IOEx)
                    {
                        //The specified file is in use.
                    }

                    //删除记录
                    _Cache.Remove(mailRec);
                }
            }
        }

        /// <summary>
        /// 判断记录是否已过期。
        /// </summary>
        /// <param name="mailRec"></param>
        /// <returns></returns>
        private static bool IsOlder(MailRecModule mailRec)
        {
            if (((TimeSpan)(DateTime.Now - mailRec.CreateTime)).Days >= AppConfiguration.GetExpiredMailDays)
            {
                return true;
            }
            else
                return false;
        }
    }
}