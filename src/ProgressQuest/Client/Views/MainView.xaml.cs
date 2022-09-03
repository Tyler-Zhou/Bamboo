using Client.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Client.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        #region 构造函数(Constructor)
        /// <summary>
        /// MainWindow.xaml 的交互逻辑
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            Closing += MainView_Closing;
        }

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
