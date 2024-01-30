using System.Windows.Forms;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 树控件的包装类
    /// </summary>
    public partial class TreeViewPacked
    {
        public TreeViewPacked(TreeView treeView, TreeNode clickNode)
        {
            this.TreeView = treeView;
            this.ClickNode = clickNode;
        }

        /// <summary>
        /// outlook 文件夹树
        /// </summary>            
        public TreeView TreeView { get; set; }

        /// <summary>
        /// 选中的节点
        /// </summary>
        public TreeNode ClickNode { get; set; }
    }
}
