using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 显示移动邮件进度条
    /// </summary>
    public partial class frmShowProgressBar : System.Windows.Forms.Form
    {
        #region 常量
        Image img_Animation = null;
        private EventHandler evtHandler = null;
        public int _MaxValue = 0;
        int Process_MaxValue = 100;
        float num_Percent = 0;
        private delegate void OnClosed();
        string _text = string.Empty;
        #endregion

        #region 初始化
        public frmShowProgressBar(int maxValue, string frmText)
            : this()
        {
            _MaxValue = maxValue;
            _text = frmText;
        }

        public frmShowProgressBar()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法
        public void InnerShow()
        {
            num_Percent = (float)Process_MaxValue / _MaxValue;
            this.Show();
        }

        void SetCtlMessage(int num)
        {
            this.lblMessage.Text = string.Format((LocalData.IsEnglish ? "Total: {0} mails,Moving mails to folder:'{1}',Current progress is:{2}%." : "共:{0}封邮件,正在移动邮件至文件夹:'{1}',当前进度为:{2}%."), _MaxValue, _text, num);
        }

        public void Cancel()
        {
            if (this.InvokeRequired)
            {
                OnClosed close = new OnClosed(ThisClose);
                this.Invoke(close);
            }
            else { ThisClose(); }
        }

        public void OnProgessChanged(int index)
        {
            int percentage = 0;
            int.TryParse((Math.Ceiling(index * num_Percent)).ToString(), out percentage);
            if (percentage > 100)
                percentage = 100;

            SetCtlMessage(percentage);

            this.procBar.Value = percentage;
            this.Refresh();
            if (percentage == 100)
            {
                Thread.Sleep(400);
                Cancel();
            }
        }

        void ThisClose()
        {
            this.Close();
        }

        #endregion

        #region 事件

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        private void frmProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        #endregion
    }
}
