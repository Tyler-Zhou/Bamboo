//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;

//namespace ICP.EDI.ServiceComponent.FTP
//{

//    ///<summary>
//    /// FTP客户端类库
//    ///</summary>
//    public class FTPClient
//    {

//        private string remoteHost, remotePath, remoteUser, remotePass, mes;
//        private int remotePort, bytes;
//        private Socket clientSocket;

//        private int retValue;
//        private Boolean debug;
//        private Boolean logined;
//        private string reply;

//        private static int BLOCK_SIZE = 512;

//        Byte[] buffer = new Byte[BLOCK_SIZE];
//        Encoding ASCII = Encoding.ASCII;


//        ///<summary>
//        ///构造函数. 
//        ///</summary>
//        public FTPClient()
//        {

//            remoteHost = "localhost";
//            remotePath = ".";
//            remoteUser = "anonymous";
//            remotePass = "anonymous@localhost.localdomain";
//            remotePort = 21;
//            debug = false;
//            logined = false;

//        }


//        /// <summary>
//        /// 获取或设置远程主机地址
//        /// </summary>

//        public string RemoteHost
//        {
//            get { return remoteHost; }
//            set { remoteHost = value; }
//        }

//        /// <summary>
//        /// 获取或设置远程主机链接端口


//        /// </summary>
//        public int RemotePort
//        {
//            get { return remotePort; }
//            set { remotePort = value; }
//        }

//        /// <summary>
//        /// 获取或设置初始目录
//        /// </summary>
//        public string RemotePath
//        {
//            get { return remotePath; }
//            set { remotePath = value; }
//        }

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

//            if (!(retValue == 150 || retValue == 125))
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

//            if (retValue != 226)
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

//            if (retValue == 213)
//            {
//                size = Int64.Parse(reply.Substring(4));
//            }
//            else
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            return size;

//        }

//        /// <summary>
//        /// 登陆远程服务器.
//        /// </summary>
//        public void Login()
//        {

//            clientSocket = new
//             Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            IPEndPoint ep = new
//             IPEndPoint(Dns.GetHostEntry(remoteHost).AddressList[0], remotePort);

//            try
//            {
//                clientSocket.Connect(ep);
//            }
//            catch (Exception)
//            {
//                throw new IOException("Couldn't connect to remote server");
//            }

//            readReply();
//            if (retValue != 220)
//            {
//                Close();
//                throw new IOException(reply.Substring(4));
//            }
           

//            sendCommand("USER " + remoteUser);

//            if (!(retValue == 331 || retValue == 230))
//            {
//                cleanup();
//                throw new IOException(reply.Substring(4));
//            }

//            if (retValue != 230)
//            {
//                sendCommand("PASS " + remotePass);
//                if (!(retValue == 230 || retValue == 202))
//                {
//                    cleanup();
//                    throw new IOException(reply.Substring(4));
//                }
//            }

//            logined = true;

//            Chdir(remotePath);

//        }


//        /// <summary>
//        /// 设置传输类型 
//        /// </summary>
//        /// <param name="mode">True 为 binary 类型, false 为其他类型</param>
//        public void SetBinaryMode(Boolean mode)
//        {

//            if (mode)
//            {
//                sendCommand("TYPE I");
//            }
//            else
//            {
//                sendCommand("TYPE A");
//            }
//            if (retValue != 200)
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
//        public void Download(string remFileName, Boolean resume)
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

//        public void Download(string remFileName, string
//         locFileName, Boolean resume)
//        {
//            if (!logined)
//            {
//                Login();
//            }

//            SetBinaryMode(true);

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
//                    if (retValue != 350)
//                    {
//                        //throw new IOException(reply.Substring(4));
//                        //Some servers may not support resuming.
//                        offset = 0;
//                    }
//                }

//                if (offset > 0)
//                {
//                    long npos = output.Seek(offset, SeekOrigin.Begin);
//                }
//            }

//            sendCommand("RETR " + remFileName);

//            if (!(retValue == 150 || retValue == 125))
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


//            readReply();

//            if (!(retValue == 226 || retValue == 250))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//        }


//        public byte[] DownloadContent(string remFileName, Boolean resume)
//        {
//            if (!logined)
//            {
//                Login();
//            }

//            SetBinaryMode(true);

//            MemoryStream output = new MemoryStream();

//            Socket cSocket = createDataSocket();

//            long offset = 0;

//            if (resume)
//            {

//                offset = output.Length;

//                if (offset > 0)
//                {
//                    sendCommand("REST " + offset);
//                    if (retValue != 350)
//                    {
//                        //throw new IOException(reply.Substring(4));
//                        //Some servers may not support resuming.
//                        offset = 0;
//                    }
//                }

//                if (offset > 0)
//                {
//                    long npos = output.Seek(offset, SeekOrigin.Begin);
//                }
//            }

//            sendCommand("RETR " + remFileName);

//            if (!(retValue == 150 || retValue == 125))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            List<byte> outStreamBytes = new List<byte>();
//            while (true)
//            {

//                bytes = cSocket.Receive(buffer, buffer.Length, 0);
//                output.Write(buffer, 0, bytes);

//                if (bytes <= 0)
//                {
//                    break;
//                }
//            }

//            byte[] outBytes = output.ToArray();

//            output.Close();
//            if (cSocket.Connected)
//            {
//                cSocket.Close();
//            }


//            readReply();

//            if (!(retValue == 226 || retValue == 250))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            return outBytes;

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
//        public void Upload(string fileName, Boolean resume)
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
//                if (retValue != 350)
//                {
//                    //throw new IOException(reply.Substring(4));
//                    //Remote server may not support resuming.
//                    offset = 0;
//                }
//            }

//            sendCommand("STOR " + Path.GetFileName(fileName));

//            if (!(retValue == 125 || retValue == 150))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            // open input stream to read source file
//            FileStream input = new
//             FileStream(fileName, FileMode.Open);

//            if (offset != 0)
//            {


//                input.Seek(offset, SeekOrigin.Begin);
//            }

//            //Console.WriteLine("Uploading file " + fileName + " to " + remotePath);

//            while ((bytes = input.Read(buffer, 0, buffer.Length)) > 0)
//            {

//                cSocket.Send(buffer, bytes, 0);

//            }
//            input.Close();


//            if (cSocket.Connected)
//            {
//                cSocket.Close();
//            }

//            readReply();
//            if (!(retValue == 226 || retValue == 250))
//            {
//                throw new IOException(reply.Substring(4));
//            }
//        }


//        /// <summary>
//        /// 上传一个文件，并发送复位标识
//        /// </summary>
//        /// <param name="fileName">本地文件路径名</param>
//        /// <param name="resume">是否复位</param>
//        public void Upload(string fileName,Byte[] datas)
//        {

//            if (!logined)
//            {
//                Login();
//            }

//            Socket cSocket = createDataSocket();
//            long offset = 0;


//            if (offset > 0)
//            {
//                sendCommand("REST " + offset);
//                if (retValue != 350)
//                {
//                    //throw new IOException(reply.Substring(4));
//                    //Remote server may not support resuming.
//                    offset = 0;
//                }
//            }

//            sendCommand("STOR " + Path.GetFileName(fileName));

//            if (!(retValue == 125 || retValue == 150))
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            // open input stream to read source file
//            //FileStream input = new
//            // FileStream(fileName, FileMode.Open);

//            MemoryStream input = new MemoryStream(datas);  
//            if (offset != 0)
//            {


//                input.Seek(offset, SeekOrigin.Begin);
//            }

//            //Console.WriteLine("Uploading file " + fileName + " to " + remotePath);

//            while ((bytes = input.Read(buffer, 0, buffer.Length)) > 0)
//            {

//                cSocket.Send(buffer, bytes, 0);

//            }
//            input.Close();


//            if (cSocket.Connected)
//            {
//                cSocket.Close();
//            }

//            readReply();
//            if (!(retValue == 226 || retValue == 250))
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

//            if (retValue != 250)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//        }


//        /// <summary>
//        /// 重命名远程文件.
//        /// </summary>
//        /// <param name="oldFileName">原始文件名</param>
//        /// <param name="newFileName">新文件名</param>
//        public void RenameRemoteFile(string oldFileName, string
//         newFileName)
//        {

//            if (!logined)
//            {
//                Login();
//            }

//            sendCommand("RNFR " + oldFileName);

//            if (retValue != 350)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            sendCommand("RNTO " + newFileName);
//            if (retValue != 250)
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

//            if (retValue != 250)
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

//            if (retValue != 250)
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

//            if (retValue != 250)
//            {
//                throw new IOException(reply.Substring(4));
//            }

//            this.remotePath = dirName;


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
//        }


//        /// <summary>
//        /// 设置是否为调试模式
//        /// </summary>
//        public bool Debug
//        {
//            set { debug = value; }
//            get { return debug; }
//        }

//        ///<summary>
//        /// 获取服务器返回的信息.
//        /// </summary>
//        public void readReply()
//        {
//            mes = "";
//            reply = readLine();
//            retValue = Int32.Parse(reply.Substring(0, 3));
//        }

//        private void cleanup()
//        {
//            if (clientSocket != null)
//            {
//                clientSocket.Close();
//                clientSocket = null;
//            }
//            logined = false;
//        }

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

          
//            return mes;
//        }


//        ///<summary>
//        /// 发送FTP协议的命令，比如 NLST, LIST,CWD
//        /// </summary>
//        public void sendCommand(String command)
//        {

//            Byte[] cmdBytes =
//             Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
//            clientSocket.Send(cmdBytes, cmdBytes.Length, 0);
//            readReply();
//        }

//        private Socket createDataSocket()
//        {

//            sendCommand("PASV");

//            if (retValue != 227)
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

//                char ch = Char.Parse(ipData.Substring(i, 1));
//                if (Char.IsDigit(ch))
//                    buf += ch;
//                else if (ch != ',')
//                {
//                    throw new IOException("Malformed PASV reply: " +
//                     reply);
//                }

//                if (ch == ',' || i + 1 == len)
//                {

//                    try
//                    {
//                        parts[partCount++] = Int32.Parse(buf);
//                        buf = "";
//                    }
//                    catch (Exception)
//                    {
//                        throw new IOException("Malformed PASV reply: " +
//                         reply);
//                    }
//                }
//            }

//            string ipAddress = parts[0] + "." + parts[1] + "." +
//             parts[2] + "." + parts[3];

//            int port = (parts[4] << 8) + parts[5];

//            Socket s = new
//             Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//            IPEndPoint ep = new
//             IPEndPoint(Dns.Resolve(ipAddress).AddressList[0], port);

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

//        ///<summary>
//        /// 获取最后一个命令引发的服务器响应的信息字符串
//        /// </summary>
//        public string ServerResponse
//        {
//            get { return reply; }
//        }

//        ///<summary>
//        /// 获取服务器返回的代码
//        ///</summary>
//        public int ResponseCode
//        {
//            get { return retValue; }
//        }
//    }
//}
