//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Windows.Forms;


//namespace ICP.EDI.UI
//{

//    public class FtpFileInfo
//    {
//        public string FileName;
//        public DateTime ModifyDateTime;
//        public bool IsDirectory;
//    }


//    public class FtpHelper
//    {
//        private string ftpServer;
//        private string userName;
//        private string password;
//        FtpWebRequest ftpRequest = null;
//        private string errMsg;
//        public string ErrMsg
//        {
//            get { return errMsg; }
//            set { errMsg = value; }
//        }
//        public bool IsAnonymous
//        {
//            get
//            {
//                return !(userName != null && userName.Trim() != string.Empty
//                    && password != null && password.Trim() != string.Empty);
//            }
//        }

//        public FtpHelper(string ftpServer, string userName, string password)
//        {
//            this.ftpServer = ftpServer;
//            this.userName = userName;
//            this.password = password;
//        }
//        /**/
//        /// <summary>        
//        /// 
//        /// 取得服务器端的文件链表         
//        /// </summary>         
//        /// <param name="serverPath"></param>         
//        /// <returns></returns>         
//        public List<FtpFileInfo> GetFilesList(string serverPath)
//        {
//            List<FtpFileInfo> fileInfoList = new List<FtpFileInfo>();
//            Uri uri = new Uri("ftp://" + ftpServer + serverPath);
//            StreamReader sr = null;
//            try
//            {
//                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(uri);
//                if (ftpRequest == null) throw new Exception("无法打开ftp服务器连接");
//                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;   //列表                    
//                if (!IsAnonymous)
//                {
//                    ftpRequest.Credentials = new NetworkCredential(userName, password);
//                }
//                sr = new StreamReader(ftpRequest.GetResponse().GetResponseStream());
//                while (!sr.EndOfStream)//读取列表                    
//                {
//                    //System.Diagnostics.Debug.WriteLine(sr.ReadLine());                     
//                    char[] splitChar = { ' ' };
//                    string[] tmpArray = sr.ReadLine().Split(splitChar, StringSplitOptions.RemoveEmptyEntries);
//                    if (tmpArray.Length != 9)
//                    {
//                        continue;
//                    }
//                    FtpFileInfo ffi = new FtpFileInfo();
//                    ffi.IsDirectory = tmpArray[0].StartsWith("d");
//                    if (!ffi.IsDirectory)
//                    {
//                        ffi.FileName = tmpArray[8];
//                        fileInfoList.Add(ffi);
//                    }
//                    else
//                    {
//                        continue;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {                 //TODO: 异常处理.                 
//                throw ex;
//            }
//            finally
//            {
//                if (sr != null) sr.Close();
//            }
//            foreach (FtpFileInfo ffi in fileInfoList)
//            {
//                ffi.ModifyDateTime = this.GetFileModifyTime(serverPath, ffi.FileName);
//            }
//            return fileInfoList;
//        }

//        /**/
//        /// <summary>         
//        /// Download        
//        /// /// </summary>        
//        /// /// <param name="serverPath"></param>         
//        /// <param name="serverFileName"></param>         
//        /// <param name="localPath"></param>        
//        /// /// <param name="localFileName"></param>         
//        /// <returns></returns>         

//        public bool Download(string serverPath, string serverFileName, string localPath, string localFileName)
//        {
//            if (!Directory.Exists(localPath))
//            {                 //TODO: 创建文件夹                 
//                errMsg = "本地路径不存在: " + localPath;
//                return false;
//            }
//            FileStream outputStream = null;
//            Stream ftpStream = null;
//            try
//            {
//                outputStream = new FileStream(localPath + "\\" + localFileName, FileMode.Create);
//                if (outputStream == null)
//                {
//                    errMsg = "无法创建本地文件";
//                    return false;
//                }
//                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServer + serverPath + "/" + serverFileName));
//                if (ftpRequest == null)
//                {
//                    errMsg = "无法连接服务器";
//                    return false;
//                }
//                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
//                ftpRequest.UseBinary = true;
//                //用户验证                 
//                if (!IsAnonymous)
//                {
//                    ftpRequest.Credentials = new NetworkCredential(userName, password);
//                }
//                ftpStream = ftpRequest.GetResponse().GetResponseStream();
//                int bufferSize = 2048;
//                int readCount;
//                byte[] buffer = new byte[bufferSize];
//                while ((readCount = ftpStream.Read(buffer, 0, bufferSize)) > 0)
//                {
//                    outputStream.Write(buffer, 0, readCount);
//                }
//            }
//            catch (Exception ex)
//            {
//                errMsg = ex.ToString();
//                return false;
//            }
//            finally
//            {
//                if (ftpStream != null) ftpStream.Close();
//                if (outputStream != null) outputStream.Close();
//            }
//            FileInfo fi = new FileInfo(localPath + "\\" + localFileName);
//            fi.LastWriteTime = GetFileModifyTime(serverPath, serverFileName);
//            return true;
//        }

//        public void DownLoadDirectory(string ServerPath, string LocalPath)
//        {
//            //取服务器端要下载的目录里的文件列表             
//            List<FtpFileInfo> serFileList = GetFilesList(ServerPath);
//            foreach (FtpFileInfo ftpFile in serFileList)
//            {
//                if (ftpFile.IsDirectory) continue;
//                DateTime modifyTime = ftpFile.ModifyDateTime;
//                string localFileName = modifyTime.ToString("yyyyMMddHHmmss") + "_" + ftpFile.FileName;
//                // 如果本地不存在该文件,则说明该文件是比较新的文件,需要下载.                 
//                if (!File.Exists(LocalPath + "\\" + localFileName))
//                {
//                    if (!Download(ServerPath, ftpFile.FileName, LocalPath, localFileName))
//                    {
//                        MessageBox.Show("下载文件出错!\r\n错误原因: " + ErrMsg);
//                    }
//                }
//            }
//        }

//        public DateTime GetFileModifyTime(string serverPath, string fileName)
//        {
//            Uri uri = new Uri("ftp://" + ftpServer + serverPath + "/" + fileName);
//            DateTime dt = DateTime.Now;
//            try
//            {
//                ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
//                if (!IsAnonymous)
//                {
//                    ftpRequest.Credentials = new NetworkCredential(userName, password);
//                }
//                ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
//                ftpRequest.UseBinary = true;
//                ftpRequest.UsePassive = false;
//                dt = ((FtpWebResponse)ftpRequest.GetResponse()).LastModified;
//            }
//            catch (Exception ex)
//            {
//                //TODO: 错误处理                 
//                throw ex;
//            }
//            return dt;
//        }
//    }



//    ///<summary>
//    /// FTP客户端类库
//    ///</summary>
//    public class FTPClient
//    {
//        #region 字段/属性
//        /// <summary>
//        /// 远程主机登陆用户名
//        /// </summary>
//        private string remoteUser;
//        /// <summary>
//        /// 远程主机登陆密码
//        /// </summary>
//        private string remotePass;
//        /// <summary>
//        /// 
//        /// </summary>
//        private string mes;
//        /// <summary>
//        /// 
//        /// </summary>
//        private int bytes;
//        /// <summary>
//        /// 服务器返回的代码
//        /// </summary>
//        private int responseCode;
//        /// <summary>
//        /// 
//        /// </summary>
//        private static int BLOCK_SIZE = 512;
//        /// <summary>
//        /// 
//        /// </summary>
//        byte[] buffer = new byte[BLOCK_SIZE];
//        /// <summary>
//        /// 
//        /// </summary>
//        Encoding ASCII = Encoding.ASCII;
//        /// <summary>
//        /// 
//        /// </summary>
//        private Socket clientSocket;
//        /// <summary>
//        /// 
//        /// </summary>
//        private bool logined { get; set; }
//        /// <summary>
//        /// 
//        /// </summary>
//        private string reply { get; set; }
//        /// <summary>
//        /// 获取或设置远程主机地址
//        /// </summary>
//        public string RemoteHost { get; set; }
//        /// <summary>
//        /// 获取或设置初始目录
//        /// </summary>
//        public string RemotePath { get; set; }
//        /// <summary>
//        /// 设置远程主机登陆用户名
//        /// </summary>
//        public string UserName
//        {
//            set { remoteUser = value; }
//        }
//        /// <summary>
//        /// 获取或设置远程主机登陆密码
//        /// </summary>
//        public string Password
//        {
//            set { remotePass = value; }
//        }
//        /// <summary>
//        /// 获取或设置远程主机链接端口
//        /// </summary>
//        public int RemotePort { get; set; }

//        ///<summary>
//        /// 获取服务器返回的代码
//        ///</summary>
//        public int ResponseCode
//        {
//            get { return ResponseCode; }
//        }
//        /// <summary>
//        /// 是否为调试模式
//        /// </summary>
//        public bool Debug { get; set; }
//        ///<summary>
//        /// 获取最后一个命令引发的服务器响应的信息字符串
//        /// </summary>
//        public string ServerResponse
//        {
//            get { return reply; }
//        }
//        #endregion

//        #region 构造函数
//        ///<summary>
//        ///构造函数. 
//        ///</summary>
//        public FTPClient()
//        {
//            RemoteHost = "localhost";
//            RemotePath = ".";
//            UserName = "anonymous";
//            Password = "anonymous@localhost.localdomain";
//            RemotePort = 21;
//            Debug = false;
//            logined = false;
//        }
//        #endregion

//        /// <summary>
//        /// 登陆远程服务器.
//        /// </summary>
//        public void Login()
//        {
//            clientSocket = new
//             Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            IPEndPoint ep = new
//             IPEndPoint(Dns.GetHostEntry(RemoteHost).AddressList[0], RemotePort);

//            try
//            {
//                clientSocket.Connect(ep);
//            }
//            catch (Exception)
//            {
//                throw new IOException("Couldn't connect to remote server");
//            }

//            readReply();
//            if (ResponseCode != 220)
//            {
//                Close();
//                throw new IOException(reply.Substring(4));
//            }
//            if (Debug)
//                Console.WriteLine("USER " + remoteUser);

//            sendCommand("USER " + remoteUser);

//            if (!(ResponseCode == 331 || ResponseCode == 230))
//            {
//                cleanup();
//                throw new IOException(reply.Substring(4));
//            }

//            if (ResponseCode != 230)
//            {
//                if (Debug)
//                    Console.WriteLine("PASS xxx");

//                sendCommand("PASS " + remotePass);
//                if (!(ResponseCode == 230 || ResponseCode == 202))
//                {
//                    cleanup();
//                    throw new IOException(reply.Substring(4));
//                }
//            }

//            logined = true;
//            Console.WriteLine("Connected to " + RemoteHost);

//            Chdir(RemotePath);

//        }

//        /// <summary>
//        /// 返回目录下的文件名称数组
//        /// </summary>
//        /// <param name="mask">搜索模式</param>
//        /// <param name="longDirListing">发送LIST 替代 NLST</param>
//        /// <returns></returns>
//        public string[] GetFileList(string mask, bool longDirListing)
//        {
//            if (!logined)
//            {
//                Login();
//            }
//            Socket cSocket = createDataSocket();

//            if (!longDirListing)
//                sendCommand("NLST " + mask);
//            else
//                sendCommand("LIST " + mask);

//            if (!(ResponseCode == 150 || ResponseCode == 125))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            mes = "";

//            while (true)
//            {

//                int bytes = cSocket.Receive(buffer, buffer.Length, 0);
//                mes += ASCII.GetString(buffer, 0, bytes);

//                if (bytes < buffer.Length)
//                {
//                    break;
//                }
//            }

//            char[] seperator = { '\n' };
//            string[] mess = mes.Split(seperator);

//            cSocket.Close();

//            readReply();

//            if (ResponseCode != 226)
//            {
//                throw new IOException(reply.Substring(4));
//            }
//            return mess;

//        }

//        /// <summary>
//        /// 获取文件大小.
//        /// </summary>
//        /// <param name="fileName">远程文件路径名称</param>
//        /// <returns>Size of file in bytes</returns>
//        public long GetFileSize(string fileName)
//        {

//            if (!logined)
//            {
//                Login();
//            }

//            sendCommand("SIZE " + fileName);
//            long size = 0;

//            if (ResponseCode == 213)
//            {
//                size = long.Parse(reply.Substring(4));
//            }
//            else
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            return size;

//        }

//        /// <summary>
//        /// 设置传输类型 
//        /// </summary>
//        /// <param name="mode">True 为 binary 类型, false 为其他类型</param>
//        public void SetBinaryMode(bool mode)
//        {
//            if (mode)
//            {
//                sendCommand("TYPE I");
//            }
//            else
//            {
//                sendCommand("TYPE A");
//            }
//            if (ResponseCode != 200)
//            {
//                throw new IOException(reply.Substring(4));
//            }
//        }

//        /// <summary>
//        /// 下载远程文件到本地目录,
//        /// 保持原始文件名.
//        /// </summary>
//        /// <param name="remFileName">远程文件路径名</param>
//        public void Download(string remFileName)
//        {
//            Download(remFileName, "", false);
//        }

//        /// <summary>
//        /// 下载远程文件到本地目录,
//        /// 保持原始文件名.并发送复位标识
//        /// </summary>
//        /// <param name="remFileName">Remote file name</param>
//        /// <param name="resume">Resume</param>
//        public void Download(string remFileName, bool resume)
//        {
//            Download(remFileName, "", resume);
//        }

//        /// <summary>
//        /// 下载远程文件到本地指定的目录路径
//        /// </summary>
//        /// <param name="remFileName">Remote file name</param>
//        /// <param name="locFileName">Local file name</param>
//        public void Download(string remFileName, string locFileName)
//        {
//            Download(remFileName, locFileName, false);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="remFileName"></param>
//        /// <param name="locFileName"></param>
//        /// <param name="resume"></param>
//        public void Download(string remFileName, string locFileName, bool resume)
//        {
//            if (!logined)
//            {
//                Login();
//            }

//            SetBinaryMode(true);

//            Console.WriteLine("Downloading file " + remFileName + " from " + RemoteHost + "/" + RemotePath);

//            if (locFileName.Equals(""))
//            {
//                locFileName = remFileName;
//            }

//            if (!File.Exists(locFileName))
//            {
//                Stream st = File.Create(locFileName);
//                st.Close();
//            }

//            FileStream output = new
//             FileStream(locFileName, FileMode.Open);

//            Socket cSocket = createDataSocket();

//            long offset = 0;

//            if (resume)
//            {

//                offset = output.Length;

//                if (offset > 0)
//                {
//                    sendCommand("REST " + offset);
//                    if (ResponseCode != 350)
//                    {
//                        //throw new IOException(reply.Substring(4));
//                        //Some servers may not support resuming.
//                        offset = 0;
//                    }
//                }

//                if (offset > 0)
//                {
//                    if (Debug)
//                    {
//                        Console.WriteLine("seeking to " + offset);
//                    }
//                    long npos = output.Seek(offset, SeekOrigin.Begin);
//                    Console.WriteLine("new pos=" + npos);
//                }
//            }

//            sendCommand("RETR " + remFileName);

//            if (!(ResponseCode == 150 || ResponseCode == 125))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            while (true)
//            {

//                bytes = cSocket.Receive(buffer, buffer.Length, 0);
//                output.Write(buffer, 0, bytes);

//                if (bytes <= 0)
//                {
//                    break;
//                }
//            }

//            output.Close();
//            if (cSocket.Connected)
//            {
//                cSocket.Close();
//            }

//            Console.WriteLine("");

//            readReply();

//            if (!(ResponseCode == 226 || ResponseCode == 250))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//        }

//        /// <summary>
//        /// 上传一个文件.
//        /// </summary>
//        /// <param name="fileName">本地文件路径名</param>
//        public void Upload(string fileName)
//        {
//            Upload(fileName, false);
//        }

//        /// <summary>
//        /// 上传一个文件，并发送复位标识
//        /// </summary>
//        /// <param name="fileName">本地文件路径名</param>
//        /// <param name="resume">是否复位</param>
//        public void Upload(string fileName, bool resume)
//        {

//            if (!logined)
//            {
//                Login();
//            }

//            Socket cSocket = createDataSocket();
//            long offset = 0;

//            if (resume)
//            {

//                try
//                {

//                    SetBinaryMode(true);
//                    offset = GetFileSize(fileName);

//                }
//                catch (Exception)
//                {
//                    offset = 0;
//                }
//            }

//            if (offset > 0)
//            {
//                sendCommand("REST " + offset);
//                if (ResponseCode != 350)
//                {
//                    //throw new IOException(reply.Substring(4));
//                    //Remote server may not support resuming.
//                    offset = 0;
//                }
//            }

//            sendCommand("STOR " + Path.GetFileName(fileName));

//            if (!(ResponseCode == 125 || ResponseCode == 150))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            // open input stream to read source file
//            FileStream input = new
//             FileStream(fileName, FileMode.Open);

//            if (offset != 0)
//            {

//                if (Debug)
//                {
//                    Console.WriteLine("seeking to " + offset);
//                }
//                input.Seek(offset, SeekOrigin.Begin);
//            }

//            Console.WriteLine("Uploading file " + fileName + " to " + RemotePath);

//            while ((bytes = input.Read(buffer, 0, buffer.Length)) > 0)
//            {

//                cSocket.Send(buffer, bytes, 0);

//            }
//            input.Close();

//            Console.WriteLine("");

//            if (cSocket.Connected)
//            {
//                cSocket.Close();
//            }

//            readReply();
//            if (!(ResponseCode == 226 || ResponseCode == 250))
//            {
//                throw new IOException(reply.Substring(4));
//            }
//        }

//        /// <summary>
//        /// 从FTP服务器上删除指定文件.
//        /// </summary>
//        /// <param name="fileName">要删除文件名</param>
//        public void DeleteRemoteFile(string fileName)
//        {

//            if (!logined)
//            {
//                Login();
//            }

//            sendCommand("DELE " + fileName);

//            if (ResponseCode != 250)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//        }

//        /// <summary>
//        /// 重命名远程文件.
//        /// </summary>
//        /// <param name="oldFileName">原始文件名</param>
//        /// <param name="newFileName">新文件名</param>
//        public void RenameRemoteFile(string oldFileName, string newFileName)
//        {

//            if (!logined)
//            {
//                Login();
//            }

//            sendCommand("RNFR " + oldFileName);

//            if (ResponseCode != 350)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            sendCommand("RNTO " + newFileName);
//            if (ResponseCode != 250)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//        }

//        /// <summary>
//        /// 在远程服务器上创建一个目录.
//        /// </summary>
//        /// <param name="dirName">目录名</param>
//        public void Mkdir(string dirName)
//        {

//            if (!logined)
//            {
//                Login();
//            }

//            sendCommand("MKD " + dirName);

//            if (ResponseCode != 250)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//        }

//        /// <summary>
//        /// 删除远程目录.
//        /// </summary>
//        /// <param name="dirName">目录名</param>
//        public void Rmdir(string dirName)
//        {

//            if (!logined)
//            {
//                Login();
//            }

//            sendCommand("RMD " + dirName);

//            if (ResponseCode != 250)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//        }

//        /// <summary>
//        /// 改变远程服务器上的工作目录.
//        /// </summary>
//        /// <param name="dirName">切换到的目录名</param>
//        public void Chdir(string dirName)
//        {

//            if (dirName.Equals("."))
//            {
//                return;
//            }

//            if (!logined)
//            {
//                Login();
//            }

//            sendCommand("CWD " + dirName);

//            if (ResponseCode != 250)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            this.RemotePath = dirName;

//            Console.WriteLine("Current directory is " + RemotePath);

//        }

//        ///<summary>
//        /// 获取服务器返回的信息.
//        /// </summary>
//        public void readReply()
//        {
//            mes = "";
//            reply = readLine();
//            responseCode = int.Parse(reply.Substring(0, 3));
//        }

//        /// <summary>
//        /// 关闭与远程服务器的连接.
//        /// </summary>
//        public void Close()
//        {

//            if (clientSocket != null)
//            {
//                sendCommand("QUIT");
//            }

//            cleanup();
//            Console.WriteLine("Closing...");
//        }

//        ///<summary>
//        /// 发送FTP协议的命令，比如 NLST, LIST,CWD
//        /// </summary>
//        public void sendCommand(string command)
//        {

//            byte[] cmdBytes =
//             Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
//            clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
//            readReply();
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        private Socket createDataSocket()
//        {
//            sendCommand("PASV");
//            if (ResponseCode != 227)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            int index1 = reply.IndexOf('(');
//            int index2 = reply.IndexOf(')');
//            string ipData =
//             reply.Substring(index1 + 1, index2 - index1 - 1);
//            int[] parts = new int[6];

//            int len = ipData.Length;
//            int partCount = 0;
//            string buf = "";

//            for (int i = 0; i < len && partCount <= 6; i++)
//            {

//                char ch = char.Parse(ipData.Substring(i, 1));
//                if (char.IsDigit(ch))
//                    buf += ch;
//                else if (ch != ',')
//                {
//                    throw new IOException("Malformed PASV reply: " + reply);
//                }

//                if (ch == ',' || i + 1 == len)
//                {

//                    try
//                    {
//                        parts[partCount++] = int.Parse(buf);
//                        buf = "";
//                    }
//                    catch (Exception)
//                    {
//                        throw new IOException("Malformed PASV reply: " + reply);
//                    }
//                }
//            }

//            string ipAddress = parts[0] + "." + parts[1] + "." +
//             parts[2] + "." + parts[3];

//            int port = (parts[4] << 8) + parts[5];

//            Socket s = new
//             Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            IPEndPoint ep = new
//             IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0], port);

//            try
//            {
//                s.Connect(ep);
//            }
//            catch (Exception)
//            {
//                throw new IOException("Can't connect to remote server");
//            }

//            return s;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        private void cleanup()
//        {
//            if (clientSocket != null)
//            {
//                clientSocket.Close();
//                clientSocket = null;
//            }
//            logined = false;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        private string readLine()
//        {

//            while (true)
//            {
//                bytes = clientSocket.Receive(buffer, buffer.Length, 0);
//                mes += ASCII.GetString(buffer, 0, bytes);
//                if (bytes < buffer.Length)
//                {
//                    break;
//                }
//            }

//            char[] seperator = { '\n' };
//            string[] mess = mes.Split(seperator);

//            if (mes.Length > 2)
//            {
//                mes = mess[mess.Length - 2];
//            }
//            else
//            {
//                mes = mess[0];
//            }

//            if (!mes.Substring(3, 1).Equals(" "))
//            {
//                return readLine();
//            }

//            if (Debug)
//            {
//                for (int k = 0; k < mess.Length - 1; k++)
//                {
//                    Console.WriteLine(mess[k]);
//                }
//            }
//            return mes;
//        }
//    }
//}
