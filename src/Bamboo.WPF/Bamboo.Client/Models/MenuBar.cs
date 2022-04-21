using Prism.Mvvm;
using Prism.Regions;

namespace Bamboo.Client.Models
{
    /// <summary>
    /// 系统导航菜单
    /// </summary>
    public class MenuBar : BindableBase
    {
        #region 图标
        /// <summary>
        /// Icon
        /// </summary>
        private string _Icon = "";
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }
        #endregion

        #region 标题
        /// <summary>
        /// Title
        /// </summary>
        private string _Title = "";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        #endregion

        #region 命名空间
        /// <summary>
        /// NameSpace
        /// </summary>
        private string _NameSpace = "";
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace
        {
            get { return _NameSpace; }
            set { _NameSpace = value; }
        }
        #endregion

        #region 导航参数
        /// <summary>
        /// NavigationParams
        /// </summary>
        private NavigationParameters _NavigationParams = new NavigationParameters();
        /// <summary>
        /// 导航参数
        /// </summary>
        public NavigationParameters NavigationParams
        {
            get { return _NavigationParams; }
            set { _NavigationParams = value; }
        }

        #endregion
    }
}
