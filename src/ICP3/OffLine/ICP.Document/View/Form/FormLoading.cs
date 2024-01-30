#region Comment

/*
 * 
 * FileName:    FormLoading.cs
 * CreatedOn:   2014/5/21 星期三 11:48:20
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->加载窗体：显示加载图片及其文字
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System.Windows.Forms;

namespace ICP.Document
{
    public partial class FormLoading : FormBase
    {
        public FormLoading()
        {
            InitializeComponent();
            this.labMessage.Text = "Loading...";
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// ShowDialog
        /// </summary>
        /// <param name="message">message</param>
        public void ShowDialog(string message)
        {
            labMessage.Text = message;
            this.ShowDialog();
        }
    }
}
