using System;
using System.Windows.Forms;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 树节点比较器
    /// </summary>
    public class TreeNodeComparer : System.Collections.IComparer
    {
        public int Compare(object x, object y)
        {
            TreeNode tx = (TreeNode)x;
            TreeNode ty = (TreeNode)y;

            return string.Compare(tx.Text, ty.Text, StringComparison.CurrentCultureIgnoreCase);
        }

    }
}
