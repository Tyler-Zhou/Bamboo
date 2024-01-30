using Prism.Regions;

namespace CRM.Client.Models
{
    /// <summary>
    /// 系统导航菜单
    /// </summary>
    public class MenuBar
    {

        #region 视图名称
        /// <summary>
        /// 视图名称
        /// </summary>
        public string ViewName { get; set; }
        #endregion

        #region 导航参数
        /// <summary>
        /// 导航参数
        /// </summary>
        public NavigationParameters NavigationParams { get; set; }

        #endregion
    }
}
