using Bamboo.Client.Core.Extensions;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System.Windows.Controls;

namespace Bamboo.Client.Views
{
    /// <summary>
    /// 登录视图
    /// </summary>
    public partial class LoginView : UserControl
    {
        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 登录视图
        /// </summary>
        /// <param name="eventAggregator">事件聚合器</param>
        /// <param name="logger">日志服务</param>
        public LoginView(IEventAggregator eventAggregator, ILogger logger)
        {
            InitializeComponent();
            _Logger = logger;
            //注册提示消息
            eventAggregator.ResgiterMessage(arg =>
            {
                _Logger.LogInformation(arg.Message);
                LoginSnakeBar?.MessageQueue?.Enqueue(arg.Message);
            }, "Login");
        }
        #endregion
    }
}
