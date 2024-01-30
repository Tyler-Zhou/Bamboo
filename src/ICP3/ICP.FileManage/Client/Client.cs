using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Configuration;
using ICPwcfInterface;
using System.IO;
using System.Threading;
using System.Data;

namespace ICPwcfClient
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
            InitiaUI();
        }

        TextBox tbServerAddress;
        TextBox tbClientSaveFilePath;
        TextBox tbClientUploadFilePath;
        TextBox tbServerFile;
        Button btnServerToClient;
        Button btnClientToServer;
        Label lbSavePath;
        Label lbUploadFath;
        Label lbServerFile;
        ProgressBar proClientsendFileToServer;
        static ListView lvLog;

        ICPwcfInterface.Interface _proxy = null;
        string FormText = string.Empty;

        private void InitiaUI()
        {
            this.Text = "Client";

            System.Drawing.Size size = new Size(600, 550);
            this.Size = size;
            this.MaximumSize = size;
            tbServerAddress = new TextBox();
            tbServerAddress.Size = new Size(350, 30);
            tbServerAddress.Location = new Point(10, 10);
            this.Controls.Add(tbServerAddress);

            btnServerToClient = new Button();
            btnServerToClient.Size = new Size(100, tbServerAddress.Size.Height + 5);
            btnServerToClient.Location = new Point(
                tbServerAddress.Location.X + tbServerAddress.Size.Width + 10,
                tbServerAddress.Location.Y - 3);
            btnServerToClient.Text = "下载文件";
            btnServerToClient.Click += btnServerToClient_Click;
            this.Controls.Add(btnServerToClient);

            btnClientToServer = new Button();
            btnClientToServer.Size = new Size(100, tbServerAddress.Size.Height + 5);
            btnClientToServer.Location = new Point(
                btnServerToClient.Location.X + btnServerToClient.Size.Width + 10,
                btnServerToClient.Location.Y);
            btnClientToServer.Text = "上传文件";
            btnClientToServer.Click += btnClientToServer_Click;
            this.Controls.Add(btnClientToServer);

            tbClientSaveFilePath = new TextBox();
            tbClientSaveFilePath.Size = new Size(450, 30);
            tbClientSaveFilePath.Location = new Point(10, tbServerAddress.Location.Y + tbServerAddress.Size.Height + 10);
            tbClientSaveFilePath.Text = AppDomain.CurrentDomain.BaseDirectory;
            this.Controls.Add(tbClientSaveFilePath);

            lbSavePath = new Label();
            lbSavePath.Size = new System.Drawing.Size(150, 20);
            lbSavePath.Location = new Point(tbClientSaveFilePath.Location.X + tbClientSaveFilePath.Size.Width + 10
                , tbClientSaveFilePath.Location.Y + 3);
            lbSavePath.Text = "下载文件存放路径";
            this.Controls.Add(lbSavePath);

            tbServerFile = new TextBox();
            tbServerFile.Size = new Size(450, 30);
            tbServerFile.Location = new Point(10, tbClientSaveFilePath.Location.Y
                + tbClientSaveFilePath.Size.Height + 10);
            //tbServerFile.Text = "Test.txt";
            tbServerFile.Text = "ServerUploadTest.pdf";
            this.Controls.Add(tbServerFile);

            lbServerFile = new Label();
            lbServerFile.Size = new System.Drawing.Size(150, 20);
            lbServerFile.Location = new Point(tbServerFile.Location.X + tbServerFile.Size.Width + 10
                , tbServerFile.Location.Y + 3);
            lbServerFile.Text = "下载服务器文件";
            this.Controls.Add(lbServerFile);

            tbClientUploadFilePath = new TextBox();
            tbClientUploadFilePath.Size = new Size(450, 30);
            tbClientUploadFilePath.Location = new Point(10, tbServerFile.Location.Y
                + tbServerFile.Size.Height + 10);
            //tbClientUploadFilePath.Text = AppDomain.CurrentDomain.BaseDirectory + "Test.txt";
            tbClientUploadFilePath.Text = AppDomain.CurrentDomain.BaseDirectory + "ClientUploadTest.pdf";
            this.Controls.Add(tbClientUploadFilePath);

            lbUploadFath = new Label();
            lbUploadFath.Size = new System.Drawing.Size(150, 20);
            lbUploadFath.Location = new Point(tbClientUploadFilePath.Location.X + tbClientUploadFilePath.Size.Width + 10
                , tbClientUploadFilePath.Location.Y + 3);
            lbUploadFath.Text = "上传文件存放定位";
            this.Controls.Add(lbUploadFath);

            lvLog = new ListView();
            lvLog.BackColor = Color.FromArgb(220, 220, 220);
            lvLog.Location = new Point(tbServerAddress.Location.X, tbClientUploadFilePath.Location.Y
                + tbClientUploadFilePath.Size.Height + 10);
            lvLog.Size = new Size(btnClientToServer.Location.X + btnClientToServer.Size.Width - tbServerAddress.Location.X,
                this.Size.Height - (lvLog.Location.Y + 80));
            this.Controls.Add(lvLog);

            proClientsendFileToServer = new ProgressBar();
            proClientsendFileToServer.Size = new Size(lvLog.Size.Width, 25);
            proClientsendFileToServer.Location = new Point(lvLog.Location.X, lvLog.Size.Height + lvLog.Location.Y + 10);
            this.Controls.Add(proClientsendFileToServer);

            this.Load += Client_Load;
        }
        private void Client_Load(object sender, EventArgs e)
        {
            FormText = this.Text + "---";

            lvLog.FullRowSelect = true;
            lvLog.GridLines = true;
            lvLog.UseCompatibleStateImageBehavior = false;
            lvLog.View = System.Windows.Forms.View.Details;
            Rectangle rc = lvLog.ClientRectangle;
            lvLog.Clear();

            int n = (rc.Width) / 8;
            lvLog.Columns.Add("序号", n, System.Windows.Forms.HorizontalAlignment.Left);
            lvLog.Columns.Add("消息", n * 5, System.Windows.Forms.HorizontalAlignment.Left);
            lvLog.Columns.Add("时间", n * 2, System.Windows.Forms.HorizontalAlignment.Left);

            ConnectChannel();

        }
        private void btnServerToClient_Click(object sender, EventArgs e)
        {
            string DownLoadFilePath = tbServerFile.Text;
            ServerToClient(DownLoadFilePath);
        }

        private void btnClientToServer_Click(object sender, EventArgs e)
        {
            ClientToServer();
        }
        string FilePath = string.Empty;
        private void ClientToServer()
        {
            FilePath = tbClientUploadFilePath.Text;
            Thread SendFileThread = new Thread(new ThreadStart(SendFile));
            SendFileThread.Start();
        }
        public void SendFile()
        {

            if (FilePath == string.Empty)
            {
                MessageBox.Show("请选择要传输的文件");
                return;
            }
            if (!File.Exists(FilePath))
            {
                MessageBox.Show("选定的文件不存在");
                return;
            }
            if (_proxy == null)
            {
                MessageBox.Show("服务已经断开");
                return;
            }
            Stream GetFileInfo = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            int FileLenght = (int)GetFileInfo.Length;//最大支持256MB

            SetprogressValue(proClientsendFileToServer, "Maximum", FileLenght);

            GetFileInfo.Close();
            int DivideNumber = 10;
            int[] DivideFileLenghtArrayStartSeek = new int[DivideNumber];
            int[] DivideFileLenghtArraySize = new int[DivideNumber];
            int avragesize = FileLenght / DivideNumber;//平均分配
            string[] DivisionFileNameArray = new string[DivideNumber];

            for (int i = 0; i < DivideNumber; i++)
            {
                if (i < DivideNumber - 1)
                {
                    DivideFileLenghtArrayStartSeek[i] = avragesize * i;
                    DivideFileLenghtArraySize[i] = avragesize - 1;
                }
                if (i == DivideNumber - 1)
                {
                    DivideFileLenghtArrayStartSeek[i] = avragesize * i;
                    DivideFileLenghtArraySize[i] = (avragesize + FileLenght % DivideNumber) - 1;
                }
            }
            //拆分文件
            GetFileInfo = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryReader SplitFileReader = new BinaryReader(GetFileInfo);
            byte[] TempBytes;
            string rfile = Path.GetFileNameWithoutExtension(FilePath);

            for (int i = 0; i < DivideNumber; i++)
            {
                string tempfile = FilePath.Replace(rfile, rfile + "_Temp" + i);
                DivisionFileNameArray[i] = tempfile;

                FileStream TempStream = new FileStream(
                    DivisionFileNameArray[i], FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                //根据文件名称和文件打开模式来初始化FileStream文件流实例
                BinaryWriter TempWriter = new BinaryWriter(TempStream);
                //以FileStream实例来创建、初始化BinaryWriter书写器实例 
                TempBytes = SplitFileReader.ReadBytes(DivideFileLenghtArraySize[i] + 1);
                //从大文件中读取指定大小数据
                TempWriter.Write(TempBytes);
                //把此数据写入小文件
                TempWriter.Close();
                //关闭书写器，形成小文件
                TempStream.Close();
                //关闭文件流
            }
            int SendFileLenght = 0;
            //初始化进度条为0
            SetprogressValue(proClientsendFileToServer, "Value", SendFileLenght);
            FileTransferMessage file = null;
            Interface proxy = null;
            for (int i = 0; i < DivideNumber; i++)
            {
                try
                {
                    //重新为流赋值
                    GetFileInfo = new FileStream(DivisionFileNameArray[i], FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    //创建文件传输参数对象
                    file = new FileTransferMessage();
                    file.DisivionFileStream = GetFileInfo;
                    file.FilePath = DivisionFileNameArray[i];
                    file.WholeFileSize = file.DisivionFileStream.Length;
                    SendFileLenght += (int)file.DisivionFileStream.Length;
                    //创建文件传输对象
                    FileSendThread sendThread = new FileSendThread();
                    sendThread._file = file;
                    proxy = GetNewproxy();
                    sendThread.Send_proxy = proxy;//_proxy;
                    //同步传输
                    sendThread.SendFile();
                    SetprogressValue(proClientsendFileToServer, "Value", SendFileLenght);

                    //异步传输
                    //Thread threadRead = new Thread(new ThreadStart(sendThread.SendFile));
                    //threadRead.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            GetFileInfo.Close();
            //合并文件
            try
            {
                for (int i = 0; i < DivideNumber; i++)
                {
                    DivisionFileNameArray[i] = Path.GetFileName(DivisionFileNameArray[i]);
                }
                Client.SetInfo("合并文件");
                proxy.MergeFile(DivisionFileNameArray);
                (proxy as ICommunicationObject).Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        Stream temp = null;
        Stream WriteFileStream = null;
        private void ServerToClient(string ClientReceiveFilePath)
        {
            if (_proxy == null)
            {
                MessageBox.Show("服务已经断开");
                return;
            }
            try
            {
                string FilePath = AppDomain.CurrentDomain.BaseDirectory;
                if (!FilePath.EndsWith("\\")) FilePath += "\\";

                IContextChannel obj = _proxy as IContextChannel;

                FileTransferMessage ReceivefileInfo = new FileTransferMessage();
                ReceivefileInfo.FilePath = ClientReceiveFilePath;
                if (temp == null)
                    temp = new FileStream("temp.config", FileMode.Create);
                ReceivefileInfo.DisivionFileStream = temp;

                FileReceiveThread FileThreadInfo = new FileReceiveThread();
                FileThreadInfo.ServerFilePath = ClientReceiveFilePath;
                if (WriteFileStream == null)
                    WriteFileStream = new FileStream(ClientReceiveFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
                else
                {
                    WriteFileStream.Dispose();
                    WriteFileStream = new FileStream(ClientReceiveFilePath, FileMode.Create, FileAccess.Write, FileShare.None);
                }
                FileThreadInfo.WriteFileStream = WriteFileStream;
                FileThreadInfo.Receive_proxy = _proxy;
                FileThreadInfo._ReceivefileInfo = ReceivefileInfo;
                Thread threadReceive = new Thread(new ThreadStart(FileThreadInfo.ReceiveFile));
                threadReceive.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        class FileSendThread
        {
            public FileTransferMessage _file;
            public Interface Send_proxy;
            public void SendFile()
            {
                try
                {
                    Client.SetInfo("文件开始上传" + Path.GetFileName(_file.FilePath) + _file.DisivionFileStream.Length.ToString());
                    Send_proxy.ClintTransferFileToService(_file);
                    Client.SetInfo("文件上传成功");
                }
                catch (Exception ex)
                {
                    string exception = ex.Message;
                    Client.SetInfo(exception);
                }
                finally
                {
                    if (_file.DisivionFileStream != null)
                    {
                        _file.DisivionFileStream.Close();
                        _file.DisivionFileStream.Dispose();
                    }
                    if (File.Exists(_file.FilePath))
                    {
                        File.Delete(_file.FilePath);
                    }
                }
            }
        }
        class FileReceiveThread
        {
            public string ServerFilePath;
            public FileTransferMessage _ReceivefileInfo;
            public FileTransferMessage _Receivefile;
            public Stream WriteFileStream = null;
            public Interface Receive_proxy;
            public void ReceiveFile()
            {
                try
                {
                    SetInfo("开始接收文件");
                    string filedir = _ReceivefileInfo.FilePath;
                    _Receivefile = Receive_proxy.ServiceTransferFileToClint(_ReceivefileInfo);

                    int bytesReadpertime = 0;
                    long totalBytesRead = 0;
                    byte[] buffer = new byte[4096 * 2];
                    do
                    {
                        bytesReadpertime = _Receivefile.DisivionFileStream.Read(buffer, 0, buffer.Length);
                        WriteFileStream.Write(buffer, 0, bytesReadpertime);
                        totalBytesRead += bytesReadpertime;
                    } while (bytesReadpertime > 0);
                    _Receivefile.DisivionFileStream.Close();
                    SetInfo("文件接收成功");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    if (_Receivefile.DisivionFileStream != null)
                    {
                        _Receivefile.DisivionFileStream.Close();
                    }
                }
            }
        }

        private void ConnectChannel()
        {
            try
            {
                ChannelFactory<ICPwcfInterface.Interface> channelFactory =
                    new ChannelFactory<ICPwcfInterface.Interface>("EndPoint");
                _proxy = channelFactory.CreateChannel();

                double server_return = _proxy.Test(20, 30);
                SetInfo("确认测试连接\t" + server_return.ToString());

                this.Text = FormText + "服务代理已开启";

            }
            catch (Exception ex)
            {

                SetInfo("创建服务代理失败" + ex.ToString());
                this.Text = FormText + "创建服务代理失败";
            }
        }

        private Interface GetNewproxy()
        {
            Interface returnproxy;
            ChannelFactory<ICPwcfInterface.Interface> channelFactory =
                               new ChannelFactory<ICPwcfInterface.Interface>("EndPoint");
            returnproxy = channelFactory.CreateChannel();
            return returnproxy;
        }

        delegate void SetInfodelegate(string info);
        private static void SetInfo(string info)
        {
            if (lvLog.InvokeRequired)
            {
                lvLog.Invoke(new SetInfodelegate(SetInfo), info);
            }
            else
            {
                string[] lvData = new string[3];
                lvData[0] = (lvLog.Items.Count + 1).ToString();
                lvData[1] = info;
                lvData[2] = DateTime.Now.ToString();
                ListViewItem lvItem = new ListViewItem(lvData);
                lvLog.Items.Add(lvItem);
            }
        }
        delegate void SetprogressValuedelegate(ProgressBar pro, string Function, int Value);
        public static void SetprogressValue(ProgressBar pro, string Function, int Value)
        {
            if (pro.InvokeRequired)
            {
                pro.Invoke(new SetprogressValuedelegate(SetprogressValue), pro, Function, Value);
            }
            else
            {
                switch (Function)
                {
                    case "Maximum":
                        pro.Maximum = Value;
                        break;
                    case "Value":
                        pro.Value = Value;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
