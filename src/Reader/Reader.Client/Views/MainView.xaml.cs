using Microsoft.Extensions.Logging;
using Prism.Events;
using Reader.Client.Events;
using Reader.Client.Extensions;
using Reader.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Reader.Client.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        #region 成员(Member)
        /// <summary>
        /// 提示信息队列
        /// </summary>
        Queue<TipsInfo> tipsInfos = new Queue<TipsInfo>();
        /// <summary>
        /// 时间计时器
        /// </summary>
        DispatcherTimer _DateTimeTimer = new DispatcherTimer();
        /// <summary>
        /// 提示信息计时器
        /// </summary>
        DispatcherTimer _TipsTimer = new DispatcherTimer();
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// MainView.xaml 的交互逻辑
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="eventAggregator">事件聚合器</param>
        public MainView(ILogger logger,IEventAggregator eventAggregator)
        {
            InitializeComponent();
            _Logger = logger;
            //注册提示消息
            eventAggregator.ResgiterMessage(arg =>
            {
                tipsInfos.Enqueue(arg.Tips);
                _TipsTimer.Start();
            });

            //注册正在加载窗口
            eventAggregator.ResgiterLoading(arg =>
            {
                if (arg.IsOpen)
                    ProgressBarLoading.Visibility = Visibility.Visible;
                else
                    ProgressBarLoading.Visibility = Visibility.Collapsed;
            });
            _TipsTimer.Interval = TimeSpan.FromSeconds(0.1);
            _TipsTimer.Tick += _TipsTimer_Tick;
            Closing += MainView_Closing;
            _DateTimeTimer.Interval = TimeSpan.FromSeconds(1);
            _DateTimeTimer.Tick += DateTimeTimer_Tick;
            _DateTimeTimer.Start();
        }
        #endregion

        #region 事件(Event)
        /// <summary>
        /// 当前事件
        /// </summary>
        private void DateTimeTimer_Tick(object sender, EventArgs e)
        {
            TextBlockCurrentDateTime.Text = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
        }
        private void _TipsTimer_Tick(object sender, EventArgs e)
        {
            _TipsTimer.Interval = TimeSpan.FromSeconds(3);
            if (tipsInfos == null || tipsInfos.Count <= 0)
            {
                TextBlockTipsContent.Text = "";
                _TipsTimer.Stop();
                return;
            }
            TipsInfo tipsInfo = tipsInfos.Dequeue();
            if(tipsInfo.Type==EnumTipsType.Error)
            {
                _Logger.LogError(tipsInfo.Content);
            }
            TextBlockTipsContent.Text = tipsInfo.Content;
        }
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
