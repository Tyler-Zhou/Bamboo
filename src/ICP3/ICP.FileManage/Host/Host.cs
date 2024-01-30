using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.Threading;

namespace ICPwcfServer
{
    public partial class Host : Form
    {
        public Host()
        {
            InitializeComponent();
            InitiaUI();
        }
        TextBox tbServerAddress;
        TextBox tbServerSaveFilePath;
        TextBox tbServerUplaodFilePath;
        Button btnOpenServer;
        Button btnCloseServer;
        Label lbSavePath;
        Label lbUploadpath;
        public static ListView lvLog;
        public ServiceHost _myServiceHost;
        private void InitiaUI()
        {
            this.Text = "Host";

            System.Drawing.Size size = new Size(605, 500);
            this.Size = size;
            this.MaximumSize = size;
            tbServerAddress = new TextBox();
            tbServerAddress.Size = new Size(350, 30);
            tbServerAddress.Location = new Point(10, 10);
            tbServerAddress.DoubleClick+=tbServerAddress_DoubleClick;
            this.Controls.Add(tbServerAddress);

            btnOpenServer = new Button();
            btnOpenServer.Size = new Size(100, tbServerAddress.Size.Height + 5);
            btnOpenServer.Location = new Point(
                tbServerAddress.Location.X + tbServerAddress.Size.Width + 10,
                tbServerAddress.Location.Y - 3);
            btnOpenServer.Text = "打开服务";
            btnOpenServer.Click += btnOpenServer_Click;
            this.Controls.Add(btnOpenServer);

            btnCloseServer = new Button();
            btnCloseServer.Size = new Size(100, tbServerAddress.Size.Height + 5);
            btnCloseServer.Location = new Point(
                btnOpenServer.Location.X + btnOpenServer.Size.Width + 10,
                btnOpenServer.Location.Y);
            btnCloseServer.Text = "关闭服务";
            btnCloseServer.Click += btnCloseServer_Click;
            this.Controls.Add(btnCloseServer);

            tbServerSaveFilePath = new TextBox();
            tbServerSaveFilePath.Size = new Size(450, 30);
            tbServerSaveFilePath.Location = new Point(10, tbServerAddress.Location.Y + tbServerAddress.Size.Height + 10);
            tbServerSaveFilePath.Text = AppDomain.CurrentDomain.BaseDirectory;
            this.Controls.Add(tbServerSaveFilePath);

            lbSavePath = new Label();
            lbSavePath.Size = new System.Drawing.Size(150, 20);
            lbSavePath.Location = new Point(tbServerSaveFilePath.Location.X + tbServerSaveFilePath.Size.Width + 10
                , tbServerSaveFilePath.Location.Y + 3);
            lbSavePath.Text = "文件存放路径";
            this.Controls.Add(lbSavePath);

            tbServerUplaodFilePath = new TextBox();
            tbServerUplaodFilePath.Size = new Size(450, 30);
            tbServerUplaodFilePath.Location = new Point(10, tbServerSaveFilePath.Location.Y 
                + tbServerSaveFilePath.Size.Height + 10);
            tbServerUplaodFilePath.Text = AppDomain.CurrentDomain.BaseDirectory +"ServerUploadTest.rar";
            this.Controls.Add(tbServerUplaodFilePath);

            lbUploadpath = new Label();
            lbUploadpath.Size = new System.Drawing.Size(150, 20);
            lbUploadpath.Location = new Point(tbServerUplaodFilePath.Location.X + tbServerUplaodFilePath.Size.Width + 10
                , tbServerUplaodFilePath.Location.Y + 3);
            lbUploadpath.Text = "到客户端文件";
            this.Controls.Add(lbUploadpath);

            lvLog = new ListView();
            lvLog.BackColor = Color.FromArgb(220, 220, 220);
            lvLog.Location = new Point(tbServerAddress.Location.X, tbServerUplaodFilePath.Location.Y 
                + tbServerUplaodFilePath.Size.Height+10);
            lvLog.Size = new Size(btnCloseServer.Location.X + btnCloseServer.Size.Width - tbServerAddress.Location.X,
                this.Size.Height - (lvLog.Location.Y + 60));
            this.Controls.Add(lvLog);
            this.Load += Host_Load;
        }

        private void tbServerAddress_DoubleClick(object sender,EventArgs e)
        {
            InitialvLog();
        }
        private void Host_Load(object sender, EventArgs e)
        {
            InitialvLog();
        }
        private void InitialvLog()
        {
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
        }
        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            if (_myServiceHost == null)
            {
                Thread threadRead = new Thread(new ThreadStart(ServerStart));
                threadRead.Start();
                this.btnOpenServer.Enabled = false;
                this.btnCloseServer.Enabled = true;
            }
            else
            {
                _myServiceHost.Close();
                _myServiceHost = null;
                this.btnOpenServer.Enabled = false;
                this.btnCloseServer.Enabled = true;
                SetInfo("停止服务");
            }
        }
        void ServerStart()
        {
            try
            {
                _myServiceHost = new ServiceHost(typeof(Server));//实例化WCF服务对象
                _myServiceHost.Opened += _myServiceHost_Opened;
                _myServiceHost.Open();
            }
            catch (Exception ex)
            {
                SetInfo(ex.Message);
                return;
            }
            SetInfo("启动成功");
        }
        private void _myServiceHost_Opened(object sender, EventArgs e)
        {
            try
            {
                SetInfo("服务事件已经开启");
            }
            catch (Exception ex)
            {
                SetInfo(ex.ToString());
            }
        }
        private void btnCloseServer_Click(object sender, EventArgs e)
        {
            if (_myServiceHost != null)
            {
                try
                {
                    _myServiceHost.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                _myServiceHost = null;
                this.btnOpenServer.Enabled = true;
                this.btnCloseServer.Enabled = false;
                SetInfo("服务已经停止");
            }
            else
            {
                SetInfo("服务对象未实例化");
            }
        }
        delegate void SetInfodelegate(string info);
        public static void SetInfo(string info)
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
    }
}
