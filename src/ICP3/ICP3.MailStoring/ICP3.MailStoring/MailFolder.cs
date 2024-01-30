using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace ICP3.MailStoring
{
    /// <summary>
    /// 文件夹
    /// </summary>
    public class MailFolderController
    {
        static string NewFolder = @"\New";
        static string RecFolder = @"\Rec";
        /// <summary>
        /// 获取邮件副本文件列表(路径)
        /// </summary>
        public string[] GetNewMailFiles()
        {
            return Directory.GetFiles(AppConfiguration.GetNewMailFolder, "*.imap", SearchOption.AllDirectories);
        }

        /// <summary> 
        /// 移动邮件到已记录文件夹
        /// </summary>
        public string MoveMailFileToRecFolder(string filePath)
        {
            //备份到另一个磁盘 (网管需要)
            BackupFile(filePath);

            string newDir = AppConfiguration.GetRecordedMailFolder + @"\" + File.GetCreationTime(filePath).ToString("yyyyMMdd");
            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }

            string newFilePath = newDir + @"\" + Path.GetFileName(filePath);

            File.Move(filePath, newFilePath);

            return newFilePath;
        }

        /// <summary>
        /// 备份到另一个磁盘
        /// </summary>
        /// <param name="filePath"></param>
        void BackupFile(string filePath)
        {
            if (string.IsNullOrEmpty(AppConfiguration.GetBackupFolder))
                return;

            string newFilePath = AppConfiguration.GetBackupFolder + filePath.Substring(2);
            string newDir = (new FileInfo(newFilePath)).DirectoryName;
            if (!Directory.Exists(newDir))
            {
                Directory.CreateDirectory(newDir);
            }

            File.Copy(filePath, newFilePath, true);
        }

        /// <summary>
        /// 删除邮件副本文件
        /// </summary>
        public void RemoveMailFile(string filePath)
        {
            //if (string.IsNullOrEmpty(AppConfiguration.GetBackupFolder))
            //{
                File.Delete(filePath);
            //}
            //else
            //{
            //    string newFilePath = AppConfiguration.GetBackupFolder + @"\" + Path.GetFileName(filePath);
            //    File.Move(filePath, newFilePath);
            //}
        }

        /// <summary>
        /// 删除过期的记录文件夹
        /// </summary>
        public void RemoveOlderFolders()
        {
            string[] dirs = Directory.GetDirectories(AppConfiguration.GetRecordedMailFolder);
            foreach(var dir in dirs)
            {
                if (((TimeSpan)(DateTime.Now - Directory.GetCreationTime(dir))).Days >= 120)
                {
                    try
                    {
                        Directory.Delete(dir, true);
                    }
                    catch (IOException iox)
                    {
                        //A file with the same name and location specified by path exists.
                        //The directory specified by path is read-only, or recursive is false and path is not an empty directory. 
                        //The directory is the application's current working directory. 
                        //The directory contains a read-only file.
                        //The directory is being used by another process.
                        continue;
                    }
                }
            }
        }
    }


}