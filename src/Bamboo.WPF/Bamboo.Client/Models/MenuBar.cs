using Prism.Mvvm;

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

    }
}
