using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 下拉控件子项
    /// </summary>
    public class ListItem
    {

        public ListItem(string id, string name)
        { Id = id; Name = name; }

        public ListItem(string id, string name, object tag)
        {
            Id = id; Name = name;
            Tag = tag;
        }

        public override string ToString() { return this.Name; }

        public string Id { get; set; }

        public string Name { get; set; }

        public object Tag { get; set; }
    }
    /// <summary>
    /// 设置时间实体类
    /// </summary>
    public partial class TimeInfo
    {
        /// <summary>
        /// 客户端配置文件Key值
        /// </summary>
        public string KeyName { get; set; }
        /// <summary>
        /// 设置时间值
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 设置时间种类
        /// </summary>
        public TimeType TimeType { get; set; }
    }

    /// <summary>
    /// 搜索文件夹参数类
    /// </summary>
    
    public partial class SearchFolderInfo
    {
        public TreeNode TreeNode { get; set; }
        /// <summary>
        ///记录没有展开的文件夹到查找到的文件夹之间的层次级别
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 文件夹层次路径
        /// </summary>
        public List<string> FolderPaths { get; set; }

        /// <summary>
        /// 查找到的节点的Tag
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// 有没有查找到文件夹
        /// </summary>
        public bool HasFoundTreeNode { get; set; }
        /// <summary>
        /// 如果TreeView没有展开节点，就会到Outlook文件夹中去查找
        /// </summary>
        public bool FoundInOutlookFolder { get; set; }
    }
}
