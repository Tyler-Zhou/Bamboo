using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FilePreviewServiceLibrary;
using System.ServiceModel;
using System.Threading;
using ICP.Framework.CommonLibrary.Client;
namespace ICP.FilePreviewService
{
    public partial class frmFilePreview : frmFadeBase
    {
        public static frmFilePreview _frmFilePreview;
        public static frmFilePreview Current
        {
            get
            {
                if (_frmFilePreview == null)
                {
                    _frmFilePreview = new frmFilePreview();
                    
                }
                
                return _frmFilePreview;
            }


        }
        private UCPDFControl _previewControl;
        private System.Windows.Forms.Timer closeTimer = new System.Windows.Forms.Timer();
        public frmFilePreview()
        {

            InitializeComponent();
            InitPreviewControl();
            RegisterFilePreviewService();

            this.SizeChanged += (sender, e) =>
            {
                MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
                if (this._previewControl != null)
                {
                    this.SuspendLayout();
                    this._previewControl.Dock = DockStyle.Fill;
                    this.ResumeLayout();
                }

            };
            this.closeTimer.Enabled = false;
            this.closeTimer.Interval = 1500;
            this.closeTimer.Tick += new EventHandler(closeTimer_Tick);
            this.MouseBoundChanged += OnMouseBoundChanged;
            ClientHelper.ReleaseWaitHandle(ClientHelper.GetAppSettingValue(ClientConstants.FilePreviewServiceAppNameKey));

        }

        private void RegisterFilePreviewService()
        {
            Type type = typeof(IFilePreviewService);

            ServiceHost host = new ServiceHost(typeof(FilePreviewService));
            host.OpenTimeout = host.CloseTimeout = TimeSpan.FromMinutes(4);
            host.AddServiceEndpoint(typeof(IFilePreviewService), FilePreviewHelper.GetBinding(), string.Format("{0}/{1}", FilePreviewHelper.GetServiceBaseAddress(), type.Name.Substring(1)));
            host.Open();
        }


        private void OnMouseBoundChanged(bool inBound)
        {
            if (inBound)
            {
                if (this.closeTimer.Enabled)
                {
                    this.closeTimer.Stop();
                }

            }
            else
            {
                if (_isAutoHide)
                {
                    if (!this.closeTimer.Enabled)
                    {
                        this.closeTimer.Enabled = true;
                    }
                    this.closeTimer.Start();
                }

            }
        }
        void closeTimer_Tick(object sender, EventArgs e)
        {
            if (this.closeTimer.Enabled)
            {
                this.closeTimer.Stop();
            }
            this.Hide();
        }

        private string _prevFilePath;
        #region IFilePreviewWindow 成员
        private void SetLocationAndSize(Point location, Size size)
        {

            this.Location = location;
            this.Size = size;

        }
        /// <summary>
        /// 预览文档
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="isAutoHide"></param>
        public void Preview(String filePath, Point location, Size size, bool isAutoHide)
        {
           
            //如果上一次预览的文档路径和本次相同，则直接显示预览窗口
            if (filePath.Equals(_prevFilePath))
            {
                SetLocationAndSize(location, size);
                this.Show();

                SetTimer(isAutoHide);
                return;
            }

            _previewControl.Load(filePath);
            SetLocationAndSize(location, size);
            this.Show();
            _prevFilePath = filePath;
            SetTimer(isAutoHide);
        }
        public void Print()
        {
            this._previewControl.Print();
        }
        private bool _isAutoHide = false;
        private void InitPreviewControl()
        {
            _previewControl = new UCPDFControl();
            _previewControl.Dock = DockStyle.Fill;
            this.Controls.Add(_previewControl);
        }
        private void SetTimer(bool isAutoHide)
        {
            _isAutoHide = isAutoHide;
            this.closeTimer.Stop();

            this.closeTimer.Enabled = isAutoHide;
            if (isAutoHide)
            {
                this.closeTimer.Start();
            }
        }

        #endregion
        private const int WIDTH = 5;
        private const int WM_NCLBUTTONDBLCLK = 0xA3;
        private const int WM_NCHITTEST = 0x84;
        private const int HTLEFT = 10;// <0xA> 左边框
        private const int HTTOP = 12;//上边框
        private const int HTTOPLEFT = 13;//左上角
        private const int HTTOPRIGHT = 14;//右上角
        private const int HTRIGHT = 11;//右边框
        private const int HTBOTTOM = 15;//底边框
        private const int HTBOTTOMLEFT = 16;//左下角
        private const int HTBOTTOMRIGHT = 17;//右下角
        private const int WM_LBUTTONDBLCLK = 0x0203;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {

            Point vPoint = new Point();

            long tempX = (long)m.LParam & 0xFFFF;
            long tempY = (long)m.LParam >> 16 & 0xFFFF;
            vPoint = new Point((int)(tempX), (int)(tempY));

            vPoint = PointToClient(vPoint);


            int x = vPoint.X;
            int y = vPoint.Y;
            switch (m.Msg)
            {
                case WM_LBUTTONDBLCLK:
                    if (y < 32)
                    {
                        if (WindowState == FormWindowState.Maximized)
                        {
                            WindowState = FormWindowState.Normal;

                        }
                    }

                    break;
                case WM_NCHITTEST://WM_NCHITTEST=132 <0x84> 
                    base.WndProc(ref m);//如果去掉这一行代码,窗体将失去MouseMove..等事件
                    //判断：仅当当前窗体状态不是最大化时，相关鼠标事件生效
                    if (this.WindowState != FormWindowState.Maximized)
                    {
                        if (vPoint.X < WIDTH)
                        {
                            if (vPoint.Y < 10)
                            {
                                m.Result = (IntPtr)HTTOPLEFT;
                            }
                            else if (vPoint.Y > this.Height - 10)
                            {
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            }
                            else
                            {
                                m.Result = (IntPtr)HTLEFT;
                            }
                        }
                        else if (vPoint.X > this.Width - WIDTH)
                        {
                            if (vPoint.Y < WIDTH)
                            {
                                m.Result = (IntPtr)HTTOPRIGHT;
                            }
                            else if (vPoint.Y > this.Height - WIDTH)
                            {
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                            }
                            else
                            {
                                m.Result = (IntPtr)HTRIGHT;
                            }
                        }
                        else if (WIDTH < vPoint.X && vPoint.X < this.Width - WIDTH)
                        {
                            if (vPoint.Y < WIDTH)
                            {
                                m.Result = (IntPtr)HTTOP;
                            }
                            else if (vPoint.Y > this.Height - WIDTH)
                            {
                                m.Result = (IntPtr)HTBOTTOM;
                            }
                        }
                        if (vPoint.X > WIDTH && vPoint.Y > WIDTH && vPoint.X < this.Width - WIDTH && vPoint.Y < this.Height - WIDTH)
                        {
                            m.Result = (IntPtr)0x2;
                        }

                        return;
                    }
                    break;

                default:
                    base.WndProc(ref m);
                    return;
            }


        }

    }

}