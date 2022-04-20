using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Bamboo.Client.Views
{
    /// <summary>
    /// 关于视图
    /// </summary>
    public partial class AboutView : UserControl
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// 关于视图
        /// </summary>
        public AboutView()
        {
            InitializeComponent();
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 超链接导航事件,在浏览器打开链接
        /// </summary>
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo() { FileName = e.Uri.AbsoluteUri, UseShellExecute = true });
                e.Handled = true;
            }
            catch
            {
            }
        } 
        #endregion
    }
}
