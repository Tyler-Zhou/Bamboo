using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICPwcfInterface;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;

namespace ICPwcfServer
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    class Server : ICPwcfInterface.Interface
    {
        public double Test(double a, double b)
        {
            ICPwcfServer.Host.SetInfo("Test");
            return a + b;
        }

        /// <summary>
        /// 服务端接收来自客户端的文件
        /// </summary>
        /// <param name="request"></param>
        public void ClintTransferFileToService(FileTransferMessage request)
        {
            Stream sourceStream = request.DisivionFileStream;
            FileStream targetStream = null;
            try
            {
                //文件信息
                string savaPath = AppDomain.CurrentDomain.BaseDirectory;
                string fileName = Path.GetFileName(request.FilePath);
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
                string logInfo = string.Format("开始接收文件,name={0}", Path.GetFileName(request.FilePath));
                ICPwcfServer.Host.SetInfo(logInfo);//填写日志
                targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write);
                try
                {
                    //定义文件缓冲区
                    const int bufferLen = 1024 * 2;//2KB
                    byte[] buffer = new byte[bufferLen];
                    int count = 0;
                    while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                    {
                        targetStream.Write(buffer, 0, count);
                        fileSize += count;
                    }
                }
                catch (Exception ex)
                {
                    string temp = ex.ToString();
                    ICPwcfServer.Host.SetInfo(string.Format("ReceiveFile异常:{0}", ex.Message));
                    //MessageBox.Show("ReceiveFile\r\n" + ex.ToString());
                }


                ICPwcfServer.Host.SetInfo(string.Format("接收文件完毕 name={0},filesize={1}",
                  fileName, fileSize));
            }
            catch (Exception ex)
            {
                ICPwcfServer.Host.SetInfo(string.Format("ClintTransferFileToService异常:{0}", ex.Message));
                //MessageBox.Show("ClintTransferFileToService\r\n" + ex.ToString());
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
        /// 服务端发送文件到客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FileTransferMessage ServiceTransferFileToClint(FileTransferMessage FileInfo)
        {
            string logInfo;
            FileTransferMessage FileMessage = new FileTransferMessage();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + FileInfo.FilePath;
            if (!File.Exists(filePath))
            {
                ICPwcfServer.Host.SetInfo("客户端请求下载文件不存在");
                return null;
            }
            ICPwcfServer.Host.SetInfo(logInfo = string.Format("开始发送文件请求信息,name={0}", Path.GetFileName(filePath)));//填写日志
            try
            {
                FileMessage.DisivionFileStream = File.OpenRead(filePath);
            }
            catch (Exception ex)
            {
                ICPwcfServer.Host.SetInfo(logInfo = string.Format("请求文件[name={0}]信息服务端打开失败", Path.GetFileName(filePath)));//填写日志
                FileMessage.Exception = ex.ToString();
            }
            ICPwcfServer.Host.SetInfo(string.Format("请求文件信息流发送完毕 name={0}", Path.GetFileName(filePath)));
            return FileMessage;
        }

        public string MergeFile(string[] FileName)
        {
            string returnstr = string.Empty;
            string _FileName = string.Empty;
            if (FileName.Length >= 0)
            {
                //获取文件名称
                string Path = AppDomain.CurrentDomain.BaseDirectory;
                if (!Path.EndsWith("\\")) Path += "\\";
                _FileName =Path+FileName[0].Replace("_Temp0", "");
                //创建文件
                Stream targetStreamMergeFile = new FileStream(_FileName, System.IO.FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                Stream sourceStreamMergeFile = null;
                ICPwcfServer.Host.SetInfo("开始合并文件");
                //验证文件是否存在
                string exception = string.Empty;
                for (int i = 0; i < FileName.Length; i++)
                {
                    FileName[i] = Path + FileName[i];
                    if (!File.Exists(FileName[i]))
                    {
                        returnstr += "指定文件不存在: " + FileName[i].ToString() + "\r\n";
                        ICPwcfServer.Host.SetInfo(returnstr);//填写日志
                        goto Over;
                    }
                    else
                    {
                        try
                        {
                            //移动目标文件指针位置到末尾
                            targetStreamMergeFile.Seek(0, SeekOrigin.End);
                            byte[] bytes = new byte[1024];
                            int readfile;
                            sourceStreamMergeFile = new FileStream(FileName[i],FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                            while (true)
                            {
                                readfile = sourceStreamMergeFile.Read(bytes, 0, 1024);
                                if (readfile > 0)
                                {
                                    targetStreamMergeFile.Write(bytes, 0, readfile);
                                }
                                else
                                {
                                    sourceStreamMergeFile.Close();
                                    sourceStreamMergeFile.Dispose();
                                    break;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            exception = ex.ToString();
                            ICPwcfServer.Host.SetInfo(string.Format("MergeFile异常:{0}", ex.Message));
                        }
                        finally
                        {
                            File.Delete(FileName[i]);
                        }
                    }
                }
                if (string.IsNullOrEmpty(exception)) ICPwcfServer.Host.SetInfo("合并文件成功");
                else ICPwcfServer.Host.SetInfo("合并文件失败");
                if (sourceStreamMergeFile != null && targetStreamMergeFile != null)
                {
                    sourceStreamMergeFile.Close();
                    sourceStreamMergeFile.Dispose();
                    targetStreamMergeFile.Close();
                    targetStreamMergeFile.Dispose();
                }
            }
        Over:
            return returnstr;
        }
    }
}
