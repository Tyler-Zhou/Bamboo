using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenter.UI
{
    /// <summary>  
    /// TreeView串行化类  
    /// </summary>  
    public class TreeViewDataAccess
    {
        public TreeViewDataAccess() { }
        /// <summary>  
        /// TreeViewData  
        /// </summary>  
        [Serializable]
        public struct TreeViewData
        {
            public TreeNodeData[] Nodes;

            /// <summary>  
            /// 递归初始化TreeView数据  
            /// </summary>  
            /// <param name="treeview"></param>  
            public TreeViewData(TreeView treeview, string entryID)
            {
                TreeNodeCollection tnCollection = treeview.Nodes;
                int count = tnCollection.Count;
                Nodes = new TreeNodeData[count];
                if (count == 0)
                {
                    return;
                }
                for (int i = 0; i <= count - 1; i++)
                {
                    Nodes[i] = new TreeNodeData(tnCollection[i],entryID);
                }
            }

            /// <summary>  
            /// 通过TreeViewData弹出TreeView  
            /// </summary>  
            /// <param name="treeview"></param>  
            public void PopulateTree(TreeView treeview)
            {
                int length = this.Nodes.Length;
                if (this.Nodes == null || length == 0)
                {
                    return;
                }
                try
                {
                    treeview.BeginUpdate();
                    for (int i = 0; i <= length - 1; i++)
                    {
                        treeview.Nodes.Add(this.Nodes[i].ToTreeNode());
                    }
                    treeview.EndUpdate();
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>  
        /// TreeNodeData  
        /// </summary>  
        [Serializable]
        public struct TreeNodeData
        {
            public string Text;
            public bool Expanded;
            public object Tag;
            public TreeNodeData[] Nodes;


            /// <summary>  
            /// TreeNode构造函数  
            /// </summary>  
            /// <param name="node"></param>  
            public TreeNodeData(TreeNode node, string entryID)
            {
                this.Text = node.Text;
                this.Tag = node.Tag;
                this.Expanded = node.IsExpanded;

                int count = node.Nodes.Count;
                //保证默认使用的文件夹展开
                if (node.Level == 0 && String.Compare(entryID, Tag.ToString()) == 0)
                {
                    this.Expanded = true;
                }

                if (!this.Expanded && count > 0)
                {
                    this.Nodes = new TreeNodeData[1];
                    Nodes[0].Text = LocalData.IsEnglish ? "Loading..." : "正在载入...";
                }
                else
                {
                    this.Nodes = new TreeNodeData[count];
                    if (node.Tag != null && node.Tag.GetType().IsSerializable)
                    {
                        this.Tag = node.Tag;
                    }
                    else
                    {
                        this.Tag = string.Empty;
                    }
                    if (count == 0)
                    {
                        return;
                    }

                    for (int i = 0; i <= count - 1; i++)
                    {
                        TreeNode treeNode = node.Nodes[i];
                        //如果节点已经被加载后，被折叠了，则需要删除节点下的子文件夹
                        TreeNodeCollection tnCollection = treeNode.Nodes;
                        if (tnCollection.Count > 0 && treeNode.IsExpanded == false)
                        {
                            if (!treeNode.FirstNode.Text.Equals(LocalData.IsEnglish ? "Loading..." : "正在载入..."))
                            {
                                tnCollection.Clear();
                                tnCollection.Add(LocalData.IsEnglish ? "Loading..." : "正在载入...");
                                Nodes[i] = new TreeNodeData(treeNode, entryID);
                                continue;
                            }
                        }
                        Nodes[i] = new TreeNodeData(treeNode, entryID);
                    }
                }
            }

            /// <summary>  
            /// TreeNodeData返回TreeNode  
            /// </summary>  
            /// <returns></returns>  
            public TreeNode ToTreeNode()
            {
                TreeNode newNode = new TreeNode(this.Text) { Tag = this.Tag };

                int length = 0;
                if (Nodes != null) { length = this.Nodes.Length; }
                if (this.Expanded)
                {
                    newNode.Expand();
                }
                if (this.Nodes == null && length == 0)
                {
                    return newNode;
                }
                if (newNode != null && length == 0)
                {
                    return newNode;
                }
                for (int i = 0; i <= length - 1; i++)
                {
                    newNode.Nodes.Add(this.Nodes[i].ToTreeNode());
                }

                return newNode;
            }
        }


        /// <summary>  
        /// 加载TreeView  
        /// </summary>  
        /// <param name="treeView"></param>  
        /// <param name="path"></param>  
        public static void LoadTreeViewDataFromXml(TreeView treeView, string path)
        {
            if (!File.Exists(path)) File.Create(path);
            BinaryFormatter bf = new BinaryFormatter();
            using (Stream file = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                TreeViewData TreeData = ((TreeViewData)(bf.Deserialize(file)));
                TreeData.PopulateTree(treeView);// 通过TreeViewData弹出TreeView 
                file.Close();
                TreeData.Nodes = null;
            }
            bf = null;
        }

        /// <summary>  
        /// 保存TreeView到文件  
        /// </summary>  
        /// <param name="treeView"></param>  
        /// <param name="path"></param>  
        public static void SaveTreeViewDataToXml(TreeView treeView, string path, string entryID)
        {
            if (!File.Exists(path)) File.Create(path);

            BinaryFormatter bf = new BinaryFormatter();
            using (Stream file = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                bf.Serialize(file, new TreeViewData(treeView, entryID));
                file.Close();
                treeView = null;
            }
            bf = null;
        }
    }

}
