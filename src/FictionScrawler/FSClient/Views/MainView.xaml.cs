using FSClient.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace FSClient.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// MainView.xaml 的交互逻辑
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            Closing += MainView_Closing;
        }
        #endregion

        #region 事件(Event)
        /// <summary>
        /// 主窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainView_Closing(object sender, CancelEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                //检查视图模型是否应该取消
                e.Cancel = vm.ClosingWindow();
            }
        } 
        #endregion
    }
}
