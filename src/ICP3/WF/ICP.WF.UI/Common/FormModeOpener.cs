using System;
using System.Windows.Forms;
using System.Drawing;
using DevExpress.XtraEditors;

namespace ICP.WF.UI
{
    /// <summary>
    /// 把控件加到摸态窗体显示辅助类
    /// </summary>
    public static class FormModeOpener
    {
        /// <summary>
        /// 把控件加到摸态窗体显示辅助类
        /// </summary>
        /// <param name="control">显示控件</param>
        /// <param name="title">标题</param>
        public static void ShowDialog(System.Windows.Forms.Control control, string title)
        {
            XtraForm form = new XtraForm();
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Text = title;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Size = new Size(control.Size.Width + 10, control.Size.Height + 30);
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.Controls.Add(control);
            form.ShowDialog();
        }


        /// <summary>
        /// 把控件加到摸态窗体显示辅助类
        /// </summary>
        /// <param name="control">显示控件</param>
        /// <param name="title">标题</param>
        public static void ShowFullDialog(System.Windows.Forms.Control control, string title)
        {
            XtraForm form = new XtraForm();
            form.Text = title;
            form.Size = new Size(control.Size.Width + 10, control.Size.Height + 30);
            form.MaximizedBoundsChanged += delegate(object sender, EventArgs e)
            {
                form.StartPosition = FormStartPosition.CenterScreen;
            };
            form.Controls.Add(control);
            form.WindowState = FormWindowState.Maximized;
            form.ShowDialog();
        }

    }
}
