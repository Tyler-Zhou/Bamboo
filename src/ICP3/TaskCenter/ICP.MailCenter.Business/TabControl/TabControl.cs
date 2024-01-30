using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// TabControl实体对象
    /// </summary>
    public class TabControl
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
        public string Cname { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string Ename { get; set; }
        /// <summary>
        /// 控件名称
        /// </summary>
        public UserControl Control { get; set; }
        /// <summary>
        /// 控件是否加载
        /// </summary>
        public Dictionary<int, bool> Dictionary { get; set; }
    }
}
