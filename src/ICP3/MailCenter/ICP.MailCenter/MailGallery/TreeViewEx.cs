using System.Windows.Forms;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 封装树控件类
    /// </summary>
    public class TreeViewEx : TreeView
    {
        public TreeViewEx()
        {
            // 打开控件的双缓冲
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
