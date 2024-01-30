#region Comment

/*
 * 
 * FileName:    FormFilePreview.cs
 * CreatedOn:   2014/5/20 星期二 9:57:15
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->PDF文件代理类
 *      ->1.PDF文件在控件UCPDFControl上预览
 *      ->2.文件打印
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.Document
{
    public partial class FormFilePreview : FormFadeBase
    {
        #region 成员变量
        /// <summary>
        /// 预览控件
        /// </summary>
        private UCPDFControl _previewControl;
        /// <summary>
        /// 是否自动隐藏
        /// </summary>
        private bool _isAutoHide = false;
        /// <summary>
        /// 关闭预览计时器
        /// </summary>
        private System.Windows.Forms.Timer closeTimer = new System.Windows.Forms.Timer();
        //预览文件路径
        private string _prevFilePath;
        /// <summary>
        /// 预览窗体对象
        /// </summary>
        private static FormFilePreview _frmFilePreview;
        /// <summary>
        /// 当前预览窗体:单例模式
        /// </summary>
        public static FormFilePreview Current
        {
            get
            {
                if (_frmFilePreview == null)
                {
                    _frmFilePreview = new FormFilePreview();

                }
                return _frmFilePreview;
            }
        } 
        #endregion

        #region 构造方法
        public FormFilePreview()
        {
            InitializeComponent();
            //初始化预览控件
            InitPreviewControl();
            //预览窗体大小改变
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
            //关闭计时预览禁用
            this.closeTimer.Enabled = false;
            //计时时间设置
            this.closeTimer.Interval = 1500;
            //计时显示
            this.closeTimer.Tick += new EventHandler(closeTimer_Tick);
            //鼠标
            this.MouseBoundChanged += OnMouseBoundChanged;
        } 
        #endregion

        #region 事件
        void closeTimer_Tick(object sender, EventArgs e)
        {
            if (this.closeTimer.Enabled)
            {
                this.closeTimer.Stop();
            }
            this.Hide();
        } 
        #endregion

        #region 自定义方法
        /// <summary>
        /// 预览文档
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="location">窗口位置</param>
        /// <param name="size">窗体大小</param>
        /// <param name="isAutoHide">是否自动隐藏</param>
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

            _previewControl.LoadFile(filePath);
            SetLocationAndSize(location, size);
            this.Show();
            _prevFilePath = filePath;
            SetTimer(isAutoHide);
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            this._previewControl.Print();
        }
        /// <summary>
        /// 初始化预览控件
        /// </summary>
        private void InitPreviewControl()
        {
            _previewControl = new UCPDFControl();
            _previewControl.Dock = DockStyle.Fill;
            this.Controls.Add(_previewControl);
        }
        /// <summary>
        /// 设置位置和大小
        /// </summary>
        /// <param name="location">位置</param>
        /// <param name="size">大小</param>
        private void SetLocationAndSize(Point location, Size size)
        {
            this.Location = location;
            this.Size = size;
        }
        /// <summary>
        /// 鼠标
        /// </summary>
        /// <param name="inBound">是否</param>
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
        /// <summary>
        /// 设置计时器：自动隐藏时进入倒计时
        /// </summary>
        /// <param name="isAutoHide">是否自动隐藏</param>
        private void SetTimer(bool isAutoHide)
        {
            //自动隐藏
            _isAutoHide = isAutoHide;
            //计时器关闭
            this.closeTimer.Stop();
            //设置计时器可用
            this.closeTimer.Enabled = isAutoHide;
            if (isAutoHide) //自动隐藏开启
            {
                this.closeTimer.Start();
            }
        } 
        #endregion

        #region API
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
        #endregion
    }
}
