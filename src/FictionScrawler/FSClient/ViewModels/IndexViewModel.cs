using Prism.Ioc;
using Prism.Regions;

namespace FSClient.ViewModels
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class IndexViewModel: BaseViewModel
    {
        #region 成员(Member)

        #endregion

        #region 服务(Services)
        #endregion

        #region 命令(Commands)

        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 首页视图模型
        /// </summary>
        /// <param name="containerProvider"></param>
        public IndexViewModel(IContainerProvider containerProvider) : base(containerProvider)
        {
            InitData();
        }
        #endregion

        #region 重写方法(Override)
        /// <summary>
        /// 是否可以处理请求的导航行为,当前视图/模型是否可以重用
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>true:</remarks>
        /// <returns></returns>
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        /// <summary>
        /// 从本页面转到其它页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含目标页面的URI</remarks>
        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        /// <summary>
        /// 从其它页面导航至本页面时
        /// </summary>
        /// <param name="navigationContext">导航内容</param>
        /// <remarks>NavigationContext包含传递过来的参数</remarks>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        #endregion

        #region 方法(Methods)
        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void InitData()
        {
        }
        #endregion
    }
}
