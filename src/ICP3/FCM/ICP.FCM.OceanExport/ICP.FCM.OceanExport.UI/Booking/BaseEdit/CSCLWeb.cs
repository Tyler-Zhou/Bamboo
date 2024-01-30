using ICP.Framework.CommonLibrary.Common;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ICP.FCM.OceanExport.UI.Booking.BaseEdit
{
    public partial class CSCLWeb : Form
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// 登录URL
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 文件本地目录
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 订舱/装箱
        /// </summary>
        public string EDIType { get; set; }
        /// <summary>
        /// 承运人
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// 上传类型添加/修改
        /// </summary>
        public EDIFlagType UploadType { get; set; }
        public bool c;
        public CSCLWeb()
        {
            InitializeComponent();
        }

        private void CSCLWeb_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Path))
            {
                Clipboard.SetDataObject(FilePath);//设置剪贴板
                webBrowser1.Navigate(Path);
                if (string.IsNullOrEmpty(Carrier))
                    Carrier = "CSC";

                webBrowser1.ScriptErrorsSuppressed = true;
                c = true;
            }
            else
            {
                MessageBox.Show("未获取到CSCL登录地址！");
                this.Close();
            }
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        static extern byte MapVirtualKey(byte wCode, int wMap); 


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //判断加载完成
            if (this.webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                //订舱
                string goBookingAddUrl = Path + "ebooking/ediorder.jsp";
                string goSIAddUrl = Path + "ebooking/ediorder.jsp?messagetype=ediloading";
                string goBookingUpdateUrl = Path + "ebooking/ediorder.jsp?messagetype=edichange";

                if (webBrowser1.Url.ToString() == Path)
                {
                    //赋值
                    webBrowser1.Document.GetElementById("userid").SetAttribute("value", UID);
                    webBrowser1.Document.GetElementById("password").SetAttribute("value", Pwd);
                    //触发事件
                    webBrowser1.Document.GetElementById("B1").InvokeMember("click");
                }
                //中海订舱
                if (EDIType == EDITypeByCarrier.CSCL_Booking_NorthChina.ToString())
                {
                    //原单
                    if (UploadType == EDIFlagType.Original)
                    {
                        if (webBrowser1.Url.ToString() == (Path + "index.jsp"))
                        {
                            webBrowser1.Navigate(goBookingAddUrl);
                        }
                        else if (webBrowser1.Url.ToString() == goBookingAddUrl)
                        {
                            HtmlElement file = webBrowser1.Document.GetElementById("file1");
                            file.Focus();
                            
                            SendKeys.Send("{Tab}");
                            SendKeys.Send(" ");
                            BackgroundWorker b = new BackgroundWorker();
                            b.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b_RunWorkerCompleted);
                            b.DoWork += new DoWorkEventHandler(b_DoWork);
                            b.RunWorkerAsync(FilePath);

                            foreach (HtmlElement ele in webBrowser1.Document.GetElementsByTagName("input"))
                            {
                                if (ele.GetAttribute("value") == Carrier)
                                {
                                    //ele.SetAttribute("checked", "checked");
                                    ele.InvokeMember("click");
                                    break;
                                }
                            }

                        }
                    }
                    //修改
                    else
                    {
                        if (webBrowser1.Url.ToString() == (Path + "index.jsp"))
                        {
                            webBrowser1.Navigate(goBookingUpdateUrl);
                        }
                        else if (webBrowser1.Url.ToString() == goBookingUpdateUrl)
                        {
                            Clipboard.SetDataObject(FilePath);//设置剪贴板

                            HtmlElement file = webBrowser1.Document.GetElementById("file1");
                            file.Focus();
                            SendKeys.Send("{Tab}");
                            SendKeys.Send(" ");
                            BackgroundWorker b = new BackgroundWorker();
                            b.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b_RunWorkerCompleted);
                            b.DoWork += new DoWorkEventHandler(b_DoWork);
                            b.RunWorkerAsync(FilePath);                            
                        }
                    }
                }
                //补料
                else if (EDIType == EDITypeByCarrier.CSCL_SI_NorthChina.ToString())
                {
                    if (webBrowser1.Url.ToString() == (Path + "index.jsp"))
                    {
                        webBrowser1.Navigate(goSIAddUrl);
                    }
                    else if (webBrowser1.Url.ToString() == goSIAddUrl)
                    {
                        Clipboard.SetDataObject(FilePath);//设置剪贴板

                        HtmlElement file = webBrowser1.Document.GetElementById("file1");
                        file.Focus();
                        SendKeys.Send("{Tab}");
                        SendKeys.Send(" ");
                        BackgroundWorker b = new BackgroundWorker();
                        b.RunWorkerCompleted += new RunWorkerCompletedEventHandler(b_RunWorkerCompleted);
                        b.DoWork += new DoWorkEventHandler(b_DoWork);
                        b.RunWorkerAsync(FilePath);
                    }
                }
            }
        }

        void b_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SendKeys.Send(e.Result as string);
            SendKeys.Send("{Enter}");
        }

        void b_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(2000);
            e.Result = e.Argument;
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

    }
}
