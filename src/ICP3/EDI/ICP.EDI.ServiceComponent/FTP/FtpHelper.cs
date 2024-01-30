//using System;

//namespace ICP.EDI.ServiceComponent.FTP
//{
//    public class FtpHelper
//    {
//       /// <summary>
//        /// 发送FTP文件
//       /// </summary>
//       /// <param name="host">FTP主机</param>
//       /// <param name="path">路径</param>
//       /// <param name="username">用户名</param>
//       /// <param name="password">密码</param>
//       /// <param name="content"></param>
//       /// <param name="filename">上传到服务器储存的文件名</param>
//       /// <param name="datas">上传文件内容</param>
//        public static void SendFile(string host, string path, string username, string password, string content, string filename, byte[] datas)
//        {
//            FTP.FTPClient ftp = new FTP.FTPClient();
//            ftp.RemoteHost = host;
//            ftp.RemotePath = path;
//            ftp.UserName = username;
//            ftp.Password = password;

//            try
//            {
//                ftp.Login();
//                ftp.Upload(filename, datas);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                ftp.Close();
//            }
//        }

//        /// <summary>
//        /// 下载文件
//        /// </summary>
//        /// <param name="host"></param>
//        /// <param name="path"></param>
//        /// <param name="username"></param>
//        /// <param name="password"></param>
//        /// <param name="remoteFileName"></param>
//        /// <returns></returns>
//        public static byte[] DownFileContent(string host, string path, string username, string password, string remoteFileName)
//        {
//            FTP.FTPClient ftp = new FTP.FTPClient();
//            ftp.RemoteHost = host;
//            ftp.RemotePath = path;
//            ftp.UserName = username;
//            ftp.Password = password;

//            try
//            {
//               ftp.Login();
//               byte[] results= ftp.DownloadContent(remoteFileName, false);
//               return results;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//            finally
//            {
//                ftp.Close();
//            }
//        }

//    }
//}
