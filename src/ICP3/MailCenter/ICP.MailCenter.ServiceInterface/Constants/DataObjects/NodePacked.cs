using System.Windows.Forms;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// TreeView节点封装类
    /// </summary>    
    public partial class NodePacked
    {
        public NodePacked() { }

        /// <summary>
        /// Outlook文件夹名称
        /// </summary>       
        public string FolderName { get; set; }
        /// <summary>
        /// 文件夹数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// TreeView文件夹
        /// </summary>
        public TreeNode TreeeNode { get; set; }
    }

    /// <summary>
    /// 树节点参数类
    /// </summary>
    public partial class TreeNodeParameters
    {
        public TreeNodeParameters(TreeNode selectNode, TreeNode visibleTreeNode)
        {
            this.SelectedNode = selectNode;
            this.VisibleTreeNode = visibleTreeNode;
        }

        /// <summary>
        /// 选中的节点
        /// </summary>
        public TreeNode SelectedNode { get; set; }
        /// <summary>
        /// 下一个节点
        /// </summary>
        public TreeNode VisibleTreeNode { get; set; }
    }
}
