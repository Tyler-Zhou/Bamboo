using System.Windows.Forms;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// ��װ���ؼ���
    /// </summary>
    public class TreeViewEx : TreeView
    {
        public TreeViewEx()
        {
            // �򿪿ؼ���˫����
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
    }
}
