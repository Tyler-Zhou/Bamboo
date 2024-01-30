using System.Windows.Forms;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// TabControl子叶控件配置类
    /// </summary>
    public class TabItemConfigInfo
    {
        /// <summary>
        /// 显示的位置
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string CName { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }
        /// <summary>
        /// 子叶需显示的控件控件
        /// </summary>
        public UserControl Control { get; set; }
        /// <summary>
        /// 控件的完全程序集名称
        /// </summary>
        public string ControlFullName { get; set; }
        /// <summary>
        /// 控件是否加载
        /// </summary>
        public bool IsControlInit
        {
            get
            {
                return Control != null;
            }
        }
        /// <summary>
        /// 控件是否只读
        /// </summary>
        public bool ReadOnly { get; set; }
    }
}
