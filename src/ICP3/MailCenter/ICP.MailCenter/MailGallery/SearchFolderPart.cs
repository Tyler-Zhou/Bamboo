using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.Threading;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.MailCenter.UI
{
    /// <summary>
    /// 邮件中心搜索文件夹后定位文件夹,并将其展开
    /// 备注：如果使用本地缓存，用户在新增文件夹，删除文件夹，更改文件夹，就需要更新本地缓存
    /// 如果不使用本地缓存，就每次需要循环遍历文件夹查找
    /// </summary>
    [SmartPart]
    public partial class SearchFolderPart : UserControl, IDisposable
    {
        #region 常量
        public WorkItem rootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }
        private Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> _SmartParts;
        public Microsoft.Practices.CompositeUI.Collections.ManagedObjectCollection<object> SmartParts
        {
            get
            {
                if (_SmartParts == null)
                    _SmartParts = this.rootWorkItem.SmartParts;
                return _SmartParts;
            }
        }
        public EmailFolderPart folderPart
        {
            get
            {
                return SmartParts.Get<EmailFolderPart>(MailCenterWorkSpaceConstants.EmailFolderPart);
            }
        }


        private string toolTipText = LocalData.IsEnglish ? "Search  Folder" : "搜索 文件夹";
        /// <summary>
        /// 记录上次搜索的关键字
        /// </summary>
        private string lastKeyWord = string.Empty;
        /// <summary>
        /// 记录上次定位的节点Index
        /// </summary>
        private int index = 0;
        /// <summary>
        /// 将搜索文件夹缓存在本地
        /// </summary>
        public Dictionary<string, SearchFolderInfo> dicSearchTreeNodes = new Dictionary<string, SearchFolderInfo>();
        #endregion

        #region 构造函数
        public SearchFolderPart()
        {
            InitializeComponent();
            this.txtSearchFolderKeyWord.Text = toolTipText;
            if (!LocalData.IsDesignMode)
            {
                RegisterEvents();

                this.Disposed += delegate
                {
                    UnRegisterEvents();
                };
            }
        }

        private void RegisterEvents()
        {
            this.GotFocus += delegate
            {
                this.txtSearchFolderKeyWord.Focus();
            };
            this.txtSearchFolderKeyWord.GotFocus += new EventHandler(txtSearchFolderKeyWord_GotFocus);
            this.txtSearchFolderKeyWord.LostFocus += new EventHandler(txtSearchFolderKeyWord_LostFocus);
            this.LostFocus += new EventHandler(SearchFolderPart_LostFocus);
        }

        private void UnRegisterEvents()
        {
            this.txtSearchFolderKeyWord.GotFocus -= new EventHandler(txtSearchFolderKeyWord_GotFocus);
            this.txtSearchFolderKeyWord.LostFocus -= new EventHandler(txtSearchFolderKeyWord_LostFocus);
            this.LostFocus -= new EventHandler(SearchFolderPart_LostFocus);
        }

        void txtSearchFolderKeyWord_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchFolderKeyWord.Text))
            {
                SetTextBoxToolTip();
            }
            DrawSearchTextBorder(false);
        }

        /// <summary>
        /// 设置TextBox提示
        /// </summary>
        private void SetTextBoxToolTip()
        {
            this.txtSearchFolderKeyWord.Text = toolTipText;
        }

        void txtSearchFolderKeyWord_GotFocus(object sender, EventArgs e)
        {
            if (this.txtSearchFolderKeyWord.Text == toolTipText)
                this.txtSearchFolderKeyWord.Text = string.Empty;
            DrawSearchTextBorder(true);
        }

        void SearchFolderPart_LostFocus(object sender, EventArgs e)
        {
            DrawSearchTextBorder(false);
        }



        #endregion

        #region 搜索文件夹


        public void OnSearched(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord) && !keyWord.Equals(toolTipText))
            {
                string _keyWord = keyWord.Trim().ToLower();
                //记录上次输入的值，如果是同一个值查询多次，说明是查询下一次节点
                //if (HasRecordKeyWord(_keyWord))
                //{
                //    index = index++;
                //}

                (new Thread(() => InnerSearch(_keyWord)) { IsBackground = true }).Start();
            }
        }

        private void InnerSearch(string keyWord)
        {
            try
            {
                if (!dicSearchTreeNodes.ContainsKey(keyWord))
                {
                    AddItem(keyWord, TreeViewPresenter.SearchAllTreeNodes(folderPart.trvFolder, keyWord));
                }

                SelectedTreeNode(keyWord);
            }
            catch (System.Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }

        /// <summary>
        /// 当前搜索的关键字是否与上次搜索的关键字相同
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private bool HasRecordKeyWord(string keyWord)
        {
            if (string.Compare(lastKeyWord, keyWord, StringComparison.OrdinalIgnoreCase) != 0)
            {
                lastKeyWord = keyWord;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将项添加到搜索结果集合中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void AddItem(string key, SearchFolderInfo value)
        {
            if (!dicSearchTreeNodes.ContainsKey(key))
                dicSearchTreeNodes.Add(key, value);
        }

        /// <summary>
        /// 根据输入的关键字直接从缓存集合中获取
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        private SearchFolderInfo GetTargetTreeNode(string keyWord)
        {
            SearchFolderInfo targetNode = null;
            dicSearchTreeNodes.TryGetValue(keyWord, out targetNode);
            return targetNode;
        }

        /// <summary>
        /// 设置背景颜色
        /// </summary>
        /// <param name="focused"></param>
        private void DrawSearchTextBorder(bool focused)
        {
            Color color;
            if (focused)
            {
                color = Color.FromArgb(247, 190, 87);

            }
            else
            {
                color = this.BackColor;
            }
            Pen pen = new Pen(color);
            pen.Width = 20;
            using (Graphics graphics = this.CreateGraphics())
            {
                this.CreateGraphics().DrawRectangle(pen, this.Bounds);
            }
        }

        /// <summary>
        /// 将查找到的TreeNode选中
        /// </summary>
        /// <param name="keyWord"></param>
        private void SelectedTreeNode(string keyWord)
        {
            SearchFolderInfo targetNodeInfo = GetTargetTreeNode(keyWord);
            if (targetNodeInfo != null && targetNodeInfo.HasFoundTreeNode)
            {
                //直接在TreeView中查找到了TreeNode
                if (!targetNodeInfo.FoundInOutlookFolder)
                {
                    //确保父节点们是展开的
                    TreeViewPresenter.ExpandAllSubTreeNodes(targetNodeInfo.TreeNode);
                    InnerExpandTreeNode(targetNodeInfo.TreeNode);
                }
                else
                {
                    //确保父节点们是展开的
                    TreeViewPresenter.ExpandAllSubTreeNodes(targetNodeInfo.TreeNode);
                    targetNodeInfo.TreeNode.Expand();
                    //展开完了之后才去查找子节点

                    if (targetNodeInfo.TreeNode.FirstNode.Text == MailUtility.LoadingText || targetNodeInfo.TreeNode.LastNode.Text == MailUtility.LoadingText)
                    {
                        WaitExpandTreeNode();
                    }
                    int level = targetNodeInfo.Level;
                    SelectedAndExpandTreeNode(targetNodeInfo.TreeNode.Nodes, targetNodeInfo.Tag.ToString(),
                                                  targetNodeInfo.FolderPaths, level);
                }
            }
        }

        /// <summary>
        /// 等待节点把所有子节点展开完
        /// </summary>
        private void WaitExpandTreeNode()
        {
            if (ClientProperties.ExpandResetEvent == null)
                ClientProperties.ExpandResetEvent = new AutoResetEvent(false);

            ClientProperties.ExpandResetEvent.WaitOne();
        }

        /// <summary>
        /// 选中和展开文件夹
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="entryID"></param>
        /// <param name="folderPaths"></param>
        /// <param name="level"></param>
        private void SelectedAndExpandTreeNode(TreeNodeCollection nodes, string entryID, List<string> folderPaths, int level)
        {
            foreach (TreeNode item in nodes)
            {
                if (level != (item.Level + 1))
                {
                    List<string> node_FullPath = MailUtility.SplitStringToList(item.FullPath, new char[1] { '\\' });
                    string treeNodeFullPath =
                        TreeViewPresenter.GetRealTreeNodeName(node_FullPath[node_FullPath.Count - 1]);

                    if (treeNodeFullPath.Equals(folderPaths[item.Level]))
                        item.Expand();
                    //等待子节点展开完
                    if (!folderPart.isLoadingFinished)
                        WaitExpandTreeNode();

                    SelectedAndExpandTreeNode(item.Nodes, entryID, folderPaths, level);
                }
                else
                {
                    if (item.Tag != null && string.Compare(item.Tag.ToString(), entryID) == 0)
                    {
                        InnerExpandTreeNode(item);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 展开节点和选中节点
        /// </summary>
        /// <param name="node"></param>
        private void InnerExpandTreeNode(TreeNode node)
        {
            if (folderPart.trvFolder.InvokeRequired)
            {
                folderPart.trvFolder.Invoke((System.Action)delegate
                {
                    folderPart.trvFolder.Focus();
                    folderPart.trvFolder.SelectedNode = node;
                    node.EnsureVisible();
                    folderPart.trvFolder.SelectedNode.Expand();
                });
            }
            else
            {
                folderPart.trvFolder.Focus();
                folderPart.trvFolder.SelectedNode = node;
                node.EnsureVisible();
                folderPart.trvFolder.SelectedNode.Expand();
            }
        }

        /// <summary>
        /// 搜索事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchFolder_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OnSearched(txtSearchFolderKeyWord.Text);
            }
        }
        #endregion

        private void txtSearchFolderKeyWord_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtSearchFolderKeyWord.Text == string.Empty)
                this.btnSearchFolder.Enabled = false;
            else
            {
                this.btnSearchFolder.Enabled = true;
            }
        }

        private void txtSearchFolderKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    OnSearched(this.txtSearchFolderKeyWord.Text);
                }
            }
        }


        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            this.dicSearchTreeNodes.Clear();
            this.dicSearchTreeNodes = null;
        }

        #endregion
    }
}
