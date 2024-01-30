using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using ICP.FileSystem.ServiceInterface;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ICP.FileSystem.ServiceComponent
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class FileSystemService : IFileSystemService
    {
        /// <summary>
        /// IsEnglish
        /// </summary>
        public bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }

        /// <summary>
        /// 存储已下载文件大小信息（对客户端是已上传文件大小信息）
        /// </summary>
        public static Dictionary<Guid, int> FirstFileStreamSizeDictionary = new Dictionary<Guid, int>();

        /// <summary>
        /// 客户端发送上传文件到临时目录
        /// </summary>
        /// <param name="documentInfo">文档实体</param>
        public void UploadOperationFileByStream(DocumentStream documentInfo)
        {
            string ExceptionString = string.Empty;
            ExceptionString = documentInfo.OperationID.ToString() + "\r\n";
            Stream sourceStream = documentInfo.Content;
            FileStream targetStream = null;
            try
            {
                if (documentInfo.IncludePDF)//是否包含PDF附件
                {
                    if (!FirstFileStreamSizeDictionary.ContainsKey(documentInfo.Id))
                    {
                        FirstFileStreamSizeDictionary.Add(documentInfo.Id, documentInfo.FirstFileStreamSize);
                    }
                }
                //文件信息
                string savaPath = ICPPathUtility.TempPathCacheFile();
                string fileName = documentInfo.Id.ToString() + Path.GetExtension(documentInfo.Name);
                //判断文件是否可读
                if (!sourceStream.CanRead)
                {
                    throw new Exception("数据流不可读!");
                }
                if (!savaPath.EndsWith("\\")) savaPath += "\\";
                //创建保存文件夹
                if (!Directory.Exists(savaPath))
                {
                    Directory.CreateDirectory(savaPath);
                }
                int fileSize = 0;
                string filePath = Path.Combine(savaPath, fileName);//Combine合并路径及文件名
                targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
                try
                {
                    //定义文件缓冲区
                    const int bufferLen = 1024 * 1;//10KB
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                        fileSize += count;
                        if (documentInfo.Id != null)
                            FileAlreadyUploadSizeDictionary[documentInfo.Id] = fileSize;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionString += ex.Message;
                }
            }
            catch (Exception ex)
            {
                ExceptionString += ex.Message;
                WriteLog("UploadOperationFileByStream\r" + ex.Message);
            }
            finally
            {
                if (targetStream != null && sourceStream != null)
                {
                    targetStream.Close();
                    targetStream.Dispose();
                    sourceStream.Close();
                    sourceStream.Dispose();
                }
            }
        }

        
        
        
        /// <summary>
        /// 服务端接收来自客户端的文件
        /// </summary>
        /// <param name="request"></param>
        public void ClintTransferFileToService(DocumentStream request)
        {
            Stream sourceStream = request.Content;
            FileStream targetStream = null;
            try
            {
                //文件信息
                string savaPath = ICPPathUtility.TempPathCacheFile();
                string fileName = request.Name;
                //判断文件是否可读
                if (!sourceStream.CanRead)
                {
                    throw new Exception("数据流不可读!");
                }
                if (!savaPath.EndsWith("\\")) savaPath += "\\";
                //创建保存文件夹
                if (!Directory.Exists(savaPath))
                {
                    Directory.CreateDirectory(savaPath);
                }

                int fileSize = 0;
                string filePath = Path.Combine(savaPath, fileName);//Combine合并路径及文件名
                targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
                try
                {
                    //定义文件缓冲区
                    const int bufferLen = 1024 * 10;//10KB
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                        fileSize += count;
                        if (request.Id != null)
                            FileAlreadyUploadSizeDictionary[request.Id] = fileSize;
                    }
                }
                catch (Exception ex)
                {
                    WriteLog("ClintTransferFileToService\r\n" + ex.Message);
                    throw new Exception("上传文件流合并文件流\r\n" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("上传文件流\r\n" + ex.Message);
            }
            finally
            {
                if (targetStream != null && sourceStream != null)
                {
                    targetStream.Close();
                    targetStream.Dispose();
                    sourceStream.Close();
                    sourceStream.Dispose();
                }
            }
        }

        /// <summary>
        /// 文件上传大小缓存
        /// </summary>
        public static Dictionary<Guid, int> FileAlreadyUploadSizeDictionary = new Dictionary<Guid, int>();
        /// <summary>
        /// 获取文件已上传大小,若全部上传完毕则移除ID信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="HostRemoveID"></param>
        /// <returns></returns>
        public int GetAlreadyUploadFileSize(Guid ID, bool HostRemoveID)
        {
            int AlreadyUploadFileSize = 0;
            try
            {
                if (FileAlreadyUploadSizeDictionary.ContainsKey(ID))
                {
                    AlreadyUploadFileSize = FileAlreadyUploadSizeDictionary[ID];
                    if (HostRemoveID)
                    {
                        //移除上传文件大小信息
                        if (FirstFileStreamSizeDictionary.ContainsKey(ID))
                            FirstFileStreamSizeDictionary.Remove(ID);
                        if (FileAlreadyUploadSizeDictionary.ContainsKey(ID))
                            FileAlreadyUploadSizeDictionary.Remove(ID);
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteLog("获取上传文件大小\r\n" + ex.Message);
                throw new Exception("获取上传文件大小\r\n" + ex.Message);
            }
            return AlreadyUploadFileSize;
        }


        /// <summary>
        /// ID作为文件识别名称,转换完毕后删除流文件
        /// </summary>
        /// <param name="documentInfo"></param>
        /// <returns></returns>
        private byte[] InfoStreamConvertByte(DocumentInfo documentInfo)
        {
            byte[] Contentcopy = null;
            String FilePath = string.Empty;
            try
            {
                string savaPath = ICPPathUtility.TempPathCacheFile();
                string fileName = documentInfo.Id.ToString() + Path.GetExtension(documentInfo.Name);
                if (!savaPath.EndsWith("\\")) savaPath += "\\";
                FilePath = savaPath + fileName;
                Stream ContentcopyStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                Contentcopy = ArrayHelper.ReadAllBytesFromStream(ContentcopyStream);
                ContentcopyStream.Close();
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }
            }
            catch (Exception ex)
            {
                WriteLog("StreamConvertByte:FilePath=" + FilePath + ex.Message + "\r\n");
                if (!string.IsNullOrEmpty(FilePath))
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);
                    }
                }
            }
            return Contentcopy;
        }

        /// <summary>
        /// 日志文件路径
        /// </summary>
        string LogPath = null;
        public DateTime dt = DateTime.Now;
        public int Day = 0;
        object SyncObject = new object();
        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="Log"></param>
        public void WriteLog(string Log)
        {
            try
            {
                lock (SyncObject)
                {
                    if (String.IsNullOrEmpty(LogPath) || (Convert.ToInt32(dt.Day.ToString()) != Day))
                    {
                        LogPath = AppDomain.CurrentDomain.BaseDirectory;
                        if (!LogPath.EndsWith("\\"))
                        {
                            LogPath += "\\";
                        }
                        LogPath += "LogFiles\\";
                        if (!Directory.Exists(LogPath))
                        {
                            Directory.CreateDirectory(LogPath);
                        }
                        LogPath += "WCF" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + ".txt";
                        Day = Convert.ToInt32(dt.Day.ToString());
                        StreamWriter sw = new StreamWriter(LogPath, true, Encoding.UTF8);
                        sw.Write(Log);
                        sw.Close();
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(LogPath, true, Encoding.UTF8);
                        sw.Write(Log);
                        sw.Close();
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
