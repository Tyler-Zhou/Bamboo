/**
 *  创建时间:2014-07-17
 *  创建人:Joabwang    
 *  描  述:提示对话框
 **/

using System;
using System.Threading;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenterFramework.UI
{
    public partial class MessageBoxFrom : Form
    {
        /// <summary>
        /// 需要终止的当前线程
        /// </summary>
        public Thread Thread { get; set; }
        /// <summary>
        /// 对话框显示内容
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public MessageBoxFrom()
        {
            InitializeComponent();
            this.Load += MessageBoxFrom_Load;
            this.Disposed += MessageBoxFrom_Disposed;

        }

        void MessageBoxFrom_Disposed(object sender, EventArgs e)
        {
            this.Load -= MessageBoxFrom_Load;
            this.refreshTime.Tick -= this.refreshTime_Tick;
            Thread = null;
            Text = null;

        }

        void MessageBoxFrom_Load(object sender, EventArgs e)
        {
            labText.Text = Text;
            if (LocalData.IsEnglish)
            {
                butok.Text = "OK";
            }
        }

        private void butok_Click(object sender, EventArgs e)
        {
            Thread.Abort();
            this.Close();
        }


        /// <summary>
        /// 计时器     调用方法超过10秒后弹出对话框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshTime_Tick(object sender, EventArgs e)
        {
            DateTime newTime = DateTime.Now;
            TimeSpan span = (TimeSpan)(newTime - Parameter.Calledtime);
            if (span.Seconds >= 10)
            {
                if (Parameter.Performflg == false)
                {
                    this.Show();
                }
                else if (Parameter.Performflg)
                {
                    this.Close();
                }
            }
        }
    }
}
