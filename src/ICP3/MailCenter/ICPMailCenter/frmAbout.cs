using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;

namespace ICPMailCenter
{
    /// <summary>
    /// 邮件中心关于说明窗体
    /// </summary>
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                if (LocalData.IsEnglish)
                {

                }
                else
                {
                    this.Text = "关于 邮件中心";


                }
                rtxtDescription.Text = System.Environment.NewLine + "   邮件中心是基于Microsoft Office Outlook 2007 开发的程序，当使用Microsoft Office Outlook 2003 或 2010 甚至更高版本时,会有些功能不支持！" +
                                         "在使用邮件中心前，确保 工具->信任中心->宏安全选择 \"不执行宏安全检查\" 和编程访问选择 \"从不向我发出可疑活动警告\" 已设置好。" +
                                         System.Environment.NewLine + "   在使用邮件中心过程中，不能将Microsoft Office Outlook关闭，可将其最小化至托盘,尽量不要去使用Outlook了。欢迎提出建议，邮件TomLai@cityocean.com. 谢谢！";
                rtxtDescription.Show();
            }
        }

        private void frmAbout_KeyDown(object sender, KeyEventArgs e)
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
