using Bamboo.Client.Core.Extensions;
using Bamboo.Client.Core.Interface;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System.Windows;
using System.Windows.Input;

namespace Bamboo.Client.Views
{
    /// <summary>
    /// 主窗体视图
    /// </summary>
    public partial class MainView : Window
    {
        #region 服务
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        /// <summary>
        /// 对话框主机服务
        /// </summary>
        private readonly IDialogHostService _DialogHostService;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 主窗体视图
        /// </summary>
        /// <param name="eventAggregator">事件聚合器</param>
        /// <param name="dialogHostService">对话框主机服务</param>
        /// <param name="logger">日志服务</param>
        public MainView(IEventAggregator eventAggregator, IDialogHostService dialogHostService, ILogger logger)
        {
            InitializeComponent();
            _Logger = logger;
            _DialogHostService = dialogHostService;

            //注册提示消息
            eventAggregator.ResgiterMessage(arg =>
            {
                _Logger.LogInformation(arg.Message);
                MainSnackbar?.MessageQueue?.Enqueue(arg.Message);
            });

            //注册等待消息窗口
            eventAggregator.Resgiter(arg =>
            {
                MainDialogHost.IsOpen = arg.IsOpen;

                if (MainDialogHost.IsOpen)
                    MainDialogHost.DialogContent = new ProgressView();
            });
            //最小化
            MinButton.Click += (sender, e) => { WindowState = WindowState.Minimized; };
            //最大化
            MaxButton.Click += (sender, e) =>
            {
                if (WindowState == WindowState.Maximized)
                    WindowState = WindowState.Normal;
                else
                    WindowState = WindowState.Maximized;
            };
            //关闭
            CloseButton.Click += async (sender, e) =>
             {
                 if (_DialogHostService != null)
                 {
                     var dialogResult = await _DialogHostService.Question("温馨提示", "确认退出系统?");
                     if (dialogResult.Result != Prism.Services.Dialogs.ButtonResult.OK) return;
                 }
                 Close();
             };
            //顶部区域拖动
            MainColorZone.MouseMove += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            };
            //双击顶部区域窗体变更最大最小化
            MainColorZone.MouseDoubleClick += (sender, e) =>
            {
                if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
                else
                    WindowState = WindowState.Normal;
            };
            //菜单切换
            MenubarListBox.SelectionChanged += (sender, e) =>
              {
                  //隐藏菜单面板
                  MenuBarDrawerHost.IsLeftDrawerOpen = false;
              };

        }
        #endregion
    }
}
