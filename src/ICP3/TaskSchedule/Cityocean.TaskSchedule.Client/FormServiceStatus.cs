#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/3 14:48:52
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Threading.Tasks;
using Cityocean.Crawl.CommonLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using Cityocean.Crawl.LogComponents;
using Cityocean.Crawl.NoticeComponents;

namespace Cityocean.TaskSchedule.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormServiceStatus : Form
    {
        #region Fields
        /// <summary>
        /// 当前消息数量：在超过限制时清空显示消息
        /// </summary>
        int _CurrentMessageCount = 1;
        /// <summary>
        /// 服务是否正在运行
        /// </summary>
        bool _ServiceIsRuning = true;
        /// <summary>
        /// 打印消息线程
        /// </summary>
        Thread threadPrintMessage;
        /// <summary>
        /// 服务管理线程:安装、卸载、启动、停止服务
        /// </summary>
        Thread threadServiceManager;

        /// <summary>
        /// InstallUtil(.NET SDK)程序存放路径
        /// </summary>
        private readonly string _InstallUtilPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(); //InstallUtil Path
        /// <summary>
        /// 服务程序名称
        /// </summary>
        private string _ServerAppName = "ICPCrawlService";
        #endregion

        #region Service
        private IMessageQueueService _MQService = null;
        /// <summary>
        /// 消息队列服务
        /// </summary>
        public IMessageQueueService MQService
        {
            get { return _MQService ?? (_MQService = new MessageQueueService()); }
        }
        #endregion

        #region Init
        /// <summary>
        /// 
        /// </summary>
        public FormServiceStatus()
        {
            InitializeComponent();
            RegisterEvent();
            Disposed += (sender, arg) =>
            {
                UnRegisterEvent();
            };
        } 
        #endregion

        #region Control Event
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void FormMain_Load(object sender, EventArgs e)
        {
            threadPrintMessage = new Thread(PrintServiceStatus);
            threadPrintMessage.Start();
            try
            {
                MQService.CreateQueuePath();
                INIHelper.Instance.IniWriteValue("ServiceConfig", "WebCrawlerClientRunning", "True");
            }
            catch (Exception ex)
            {
                LogService.Error("", "初始数据抓取消息队列发生异常",ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                INIHelper.Instance.IniWriteValue("ServiceConfig", "WebCrawlerClientRunning", "False");
                MQService.RemoveQueuePath();
            }
            catch (Exception ex)
            {
                LogService.Error("","",ex);
            }
            try
            {
                threadPrintMessage.Abort();
            }
            catch (Exception ex)
            {
                LogService.Error("","",ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstallation_Click(object sender, EventArgs e)
        {
            try
            {
                threadServiceManager.Abort();
            }
            catch { }

            threadServiceManager = new Thread(Install);
            threadServiceManager.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUninstall_Click(object sender, EventArgs e)
        {
            try
            {
                threadServiceManager.Abort();
            }
            catch { }

            threadServiceManager = new Thread(Uninstall);
            threadServiceManager.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                threadServiceManager.Abort();
            }
            catch { }

            threadServiceManager = new Thread(Start);
            threadServiceManager.Start();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                threadServiceManager.Abort();
            }
            catch { }

            threadServiceManager = new Thread(Stop);
            threadServiceManager.Start();
        }
        #endregion

        #region Custom Method
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            Load += FormMain_Load;
            FormClosing += FormMain_FormClosing;

            btnInstallation.Click += btnInstallation_Click;
            btnUninstall.Click += btnUninstall_Click;
            btnStart.Click += btnStart_Click;
            btnStop.Click += btnStop_Click;
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            Load -= FormMain_Load;
            FormClosing -= FormMain_FormClosing;
            btnInstallation.Click -= btnInstallation_Click;
            btnUninstall.Click -= btnUninstall_Click;
            btnStart.Click -= btnStart_Click;
            btnStop.Click -= btnStop_Click;
        }

        /// <summary>
        /// 安装服务
        /// </summary>
        void Install()
        {
            string c = string.Format("{0}InstallUtil {1}{2}.exe", _InstallUtilPath, GlobalVariable.ProgramDirectory, _ServerAppName);
            Cmd(c);
        }
        /// <summary>
        /// 卸载服务
        /// </summary>
        void Uninstall()
        {
            string c = string.Format("{0}InstallUtil /u {1}{2}.exe", _InstallUtilPath, GlobalVariable.ProgramDirectory, _ServerAppName);
            Cmd(c);
        }

        void Start()
        {
            string c = @"net start " + _ServerAppName;
            Cmd(c);
        }

        void Stop()
        {
            string c = @"net stop " + _ServerAppName;
            Cmd(c);
        }

        /// <summary>
        /// 执行Cmd命令
        /// </summary>
        private void Cmd(string c)
        {
            Task.Factory.StartNew(() =>
            {
                Process process = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true
                    }
                };
                process.Start();

                process.StandardInput.WriteLine(c);
                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine("exit");

                StreamReader reader = process.StandardOutput;//截取输出流

                string output = reader.ReadLine();//每次读取一行

                while (!reader.EndOfStream)
                {
                    PrintThrendInfo(GetType().Name, output, Color.Black);
                    output = reader.ReadLine();
                }

                process.WaitForExit();
            });
        }

        /// <summary>
        /// 进度显示
        /// </summary>
        /// <param name="paramJobName">任务名称</param>
        /// <param name="paramInfo">提示信息</param>
        /// <param name="paramForeColor">字体颜色</param>
        private void PrintThrendInfo(string paramJobName,string paramInfo, Color paramForeColor)
        {
            //锁定资源
            lock (lvMessage)
            {
                try
                {
                    ListViewItem Item = new ListViewItem(_CurrentMessageCount.ToString(CultureInfo.InvariantCulture));
                    Item.SubItems.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    Item.SubItems.Add(paramJobName);
                    Item.SubItems.Add(paramInfo);
                    Item.ForeColor = paramForeColor;
                    if (lvMessage != null && Item != null && lvMessage.Items.Count > 0)
                    {
                        lvMessage.Items.Add(Item);
                        Item.EnsureVisible();
                    }
                    _CurrentMessageCount++;
                    if (_CurrentMessageCount >= 100)
                    {
                        lvMessage.Items.Clear();
                        _CurrentMessageCount = 1;
                    }
                }
                catch (Exception ex)
                {
                    LogService.Error("","",ex);
                }
            }
        }

        private void PrintServiceStatus()
        {
            try
            {
                while (true)
                {
                    threadPrintMessage.Join(1000);

                    _ICPWebCrawler.Refresh();

                    if (_ICPWebCrawler.Status != ServiceControllerStatus.Running)
                    {
                        if (_ServiceIsRuning)
                        {
                            PrintThrendInfo(GetType().Name, "服务运行状态：" + _ICPWebCrawler.Status, Color.Black);
                        }
                        _ServiceIsRuning = false;
                        continue;
                    }
                    _ServiceIsRuning = true;

                    List<EMessageInfo> Msg = null;
                    try
                    {
                        Msg = MQService.GetNewMessageInfo();
                    }
                    catch
                    {
                        Msg = new List<EMessageInfo>();
                    }
                    foreach (EMessageInfo mInfo in Msg)
                    {
                        lock (mInfo)
                        {
                            //XML格式化传输量较大
                            //Msg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                            switch (mInfo.MType)
                            {
                                case 1:
                                    PrintThrendInfo(mInfo.MOwerJob, mInfo.MContent, Color.Black);
                                    break;
                                case 2:
                                    PrintThrendInfo(mInfo.MOwerJob, mInfo.MContent, Color.Fuchsia);
                                    break;
                                case 3:
                                    PrintThrendInfo(mInfo.MOwerJob, mInfo.MContent, Color.Red);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.Error("","",ex);
            }
        }
        #endregion

    }
}
