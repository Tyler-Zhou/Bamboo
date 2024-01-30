using System;
using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 系统右下方温馨提示消息
    /// </summary>
    public partial class frmReminder : Form
    {

        private const int AW_HOR_POSITIVE = 0X0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private int AW_HOR_NEGATIVE = 0X0002;//自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        private const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        private const int AW_HIDE = 0x10000;//隐藏窗口
        private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AW_BLEND = 0x80000;//使用淡入淡出效果



        public frmReminder(string strMsg, bool stay)
            : this()
        {
            Init(strMsg);
            if (stay)
                messageBoxTimer.Stop();
            else
            {
                messageBoxTimer.Stop();
                messageBoxTimer.Start();
            }
        }

        public frmReminder()
        {
            InitializeComponent();
            this.Disposed += delegate { DisposedComponent(); };
        }

        private void DisposedComponent()
        {
            this.messageBoxTimer.Dispose();
            this.messageBoxTimer = null;
        }

        private void Init(string msg)
        {
            if (LocalData.IsEnglish)
            {
                this.Text = "Reminder";
            }
            this.txtMsg.Text = string.Format("    {0}", msg);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int x = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - this.Height;
            this.Location = new Point(x, y);

            WindowsExtension.AnimateWindow(this.Handle, 1000, AW_SLIDE | AW_ACTIVE | AW_CENTER);
        }
        private void messageBoxTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReminder_FormClosing(object sender, FormClosingEventArgs e)
        {
            WindowsExtension.AnimateWindow(this.Handle, 1000, AW_BLEND | AW_HIDE | AW_CENTER);
        }

        private void frmReminder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Alt)
            {
                e.Handled = false;
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
