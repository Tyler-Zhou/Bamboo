using System;
using System.Windows.Forms;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.MailCenter.UI.MailGallery
{
    /// <summary>
    /// 设置接收和发送时间窗体
    /// </summary>
    public partial class frmSetTime : Form
    {
        #region 常量
        public TimeType Type { get; set; }
        public EventHandler<CommonEventArgs<TimeInfo>> QueryReminder;
        #endregion

        #region 构造函数
        public frmSetTime(TimeType timeType, string txtTime)
            : this()
        {
            this.txtSetTime.Text = txtTime;
            this.Type = timeType;
            Init();
        }

        public frmSetTime()
        {
            InitializeComponent();
        }
        #endregion

        #region 方法
        void SaveOrUpdateConfig()
        {
            if (Validated())
            {
                string keyName = GetConfigNodeKeyName();
                TimerManager.SetConfigNodeValue(keyName, this.txtSetTime.Text.Trim());
                CacheReadTimeInterval(keyName);
                if (QueryReminder != null)
                {
                    QueryReminder(null, new CommonEventArgs<TimeInfo>(new TimeInfo() { KeyName = keyName, Time = this.txtSetTime.Text.Trim(), TimeType = this.Type }));
                }
            }
        }

        private void CacheReadTimeInterval(string keyName)
        {
            if (keyName == TimerManager.MailReadTime)
            {
                int time = 0;
                int.TryParse(this.txtSetTime.Text.Trim(), out time);
                ClientProperties.MailReadTimeInterval = time * 1000;
            }
        }

        private bool Validated()
        {
            if (!string.IsNullOrEmpty(this.txtSetTime.Text))
            {
                if (this.txtSetTime.Text.StartsWith("0"))
                    return false;
            }
            else
                return false;

            return true;
        }

        private string GetConfigNodeKeyName()
        {
            string key = string.Empty;
            if (Type == TimeType.MailRead)
                key = TimerManager.MailReadTime;
            else
                key = TimerManager.ReveiveTimer;


            return key;
        }

        private void Init()
        {
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
            {
                if (this.Type == TimeType.MailRead)
                {
                    this.Text = "Set Mail Read Time";
                    this.lblTime.Text = "use second as unit:";
                }
                else
                {
                    this.Text = "Set Send/Receive Time";
                    this.lblTime.Text = "use minute as unit:";
                }
                this.btnOK.Text = "OK(&S)";
                this.btnCancel.Text = "Cancel(&C)";
            }
            else
            {
                if (this.Type == TimeType.MailRead)
                {
                    this.Text = "设置邮件已读时间段";
                    this.lblTime.Text = "以秒作单位:";
                }
                else
                {
                    this.Text = "设置邮件发送/接收时间段";
                    this.lblTime.Text = "以分钟作单位:";
                }

                this.btnOK.Text = "确定(&S)";
                this.btnCancel.Text = "取消(&C)";
            }
        }

        #endregion

        #region 事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveOrUpdateConfig();
            this.Close();
        }

        private void txtSetTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 57 || (e.KeyChar > 8 && e.KeyChar < 47) || e.KeyChar < 8)
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetTime_Load(object sender, EventArgs e)
        {
        }

        #endregion

        private void frmSetTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}
