#region Comment

/*
 * 
 * FileName:    FrmPickFolders.cs
 * CreatedOn:   2014/7/22 15:38:47
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->选择文件夹
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System.Linq;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.MailCenterFramework.UI
{
    public partial class FrmPickFolders : Form
    {
        #region 成员变量
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsPick { get; set; }

        #region 属性-选择文件夹串联文本
        /// <summary>
        /// 选择文件夹串联文本
        /// </summary>
        public Dictionary<string, string> SelectFolderDictionary { get; set; }
        #endregion

        #endregion

        #region 构造函数
        public FrmPickFolders()
        {
            InitializeComponent();
            //初始化变量
            SelectFolderDictionary = new Dictionary<string, string>();
            IsPick = true;
            InitControl();
            ButtonEnable(false);
            OperationEvent(true);
            Disposed += (sender, args) =>
            {
                //0.移除事件
                OperationEvent(false);
                //1.清空数据
                SelectFolderDictionary.Clear();
                SelectFolderDictionary = null;
            };
        }
        #endregion

        #region 窗体事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        void FrmPickFolders_Load(object sender, EventArgs e)
        {
            try
            {
                //获取当前用户文件夹列表
                Folders userFolders = OutlookUtility.CurrentApplication.Session.CurrentUser.Session.Folders;
                //遍历用户文件夹填充到树形控件
                FillTreeList(null, userFolders);
                tvFolders.ExpandAll();
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Pick Folders Load",ex);
            }
        }

        /// <summary>
        /// 树形控件点击后
        /// </summary>
        void tvFolders_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
                return;
            if (e.Node.Checked)
            {
                SelectFolderDictionary.Add(e.Node.Name, e.Node.Text);
                ButtonEnable(true);
            }
            else
            {
                SelectFolderDictionary.Remove(e.Node.Name);
                var isChecked = false;
                NodeChecked(null, ref isChecked);
                ButtonEnable(isChecked);
            }

        }

        /// <summary>
        /// 确定选择文件夹
        /// </summary>
        void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                FillSelectFolderDictionary(null);
                Close();
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Pick Folders:Sure",ex);
            }
        }

        /// <summary>
        /// 取消选择
        /// </summary>
        void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                IsPick = false;
                Close();
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Pick Folders:Cancel", ex);
            }
        }

        /// <summary>
        /// 清除所有选择 
        /// </summary>
        void btnClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                SelectFolderDictionary.Clear();
                ClancelChecked(null);
                ButtonEnable(false);
            }
            catch (System.Exception ex)
            {
                ToolUtility.WriteLog("Pick Folders:Clear All", ex);
            }
        }
        #endregion

        #region 方法定义
        /// <summary>
        /// 初始化控件
        /// </summary>
        void InitControl()
        {
            if (!OutlookUtility.IsEnglish)
            {
                Text = "选定文件夹";
                lblTopTitle.Text = "文件夹:";
                btnSure.Text = "确定";
                btnCancel.Text = "取消";
                btnClearAll.Text = "全部清除(&L)";
            }
        }

        /// <summary>
        /// 操作事件
        /// </summary>
        /// <param name="isRegister">是否注册事件</param>
        private void OperationEvent(bool isRegister)
        {
            if (isRegister)
            {
                Load += FrmPickFolders_Load;                    //窗体加载
                btnSure.Click += btnSure_Click;                 //确定选择文件夹
                btnCancel.Click += btnCancel_Click;             //取消选择
                btnClearAll.Click += btnClearAll_Click;         //清除所有选择
                tvFolders.AfterCheck += tvFolders_AfterCheck;   //按钮点击后
            }
            else
            {
                Load -= FrmPickFolders_Load;                    //窗体加载
                btnSure.Click -= btnSure_Click;                 //确定选择文件夹
                btnCancel.Click -= btnCancel_Click;             //取消选择
                btnClearAll.Click -= btnClearAll_Click;         //清除所有选择
                tvFolders.AfterCheck -= tvFolders_AfterCheck;   //按钮点击后
            }
        }

        /// <summary>
        /// 填充树形控件
        /// </summary>
        /// <param name="treeListNode"></param>
        /// <param name="userFolders"></param>
        private void FillTreeList(TreeNode treeListNode, Folders userFolders)
        {
            foreach (Folder itemFolder in userFolders)
            {
                if (OutlookUtility.ArchivingFolders.Any(folderItem => folderItem.Equals(itemFolder.Name)))
                    continue;
                if (itemFolder.Name.Equals(OutlookUtility.EmailRelationSaveFolder)) continue;
                int imageIndex = OutlookUtility.GetImageIndex(itemFolder.Name.ToLower());
                if (imageIndex == -1) continue;
                TreeNode node = new TreeNode();
                node.Name = itemFolder.Store.DisplayName + "&" + itemFolder.EntryID;
                node.Text = itemFolder.Name;
                node.Checked = SelectFolderDictionary.ContainsKey(node.Name);
                node.ImageIndex = imageIndex;
                if (treeListNode != null)
                    treeListNode.Nodes.Add(node);
                else
                    tvFolders.Nodes.Add(node);
                if (itemFolder.Folders != null && itemFolder.Folders.Count > 0)
                {
                    FillTreeList(node, itemFolder.Folders);
                }
            }
        }
        /// <summary>
        /// 填充选择文件夹目录
        /// </summary>
        /// <param name="treeNode">树形节点</param>
        private void FillSelectFolderDictionary(TreeNode treeNode)
        {
            TreeNodeCollection treeNodes = treeNode == null ? tvFolders.Nodes : treeNode.Nodes;
            foreach (TreeNode itemNode in treeNodes)
            {
                if (itemNode.Checked)
                {
                    if (!SelectFolderDictionary.ContainsKey(itemNode.Name))
                        SelectFolderDictionary.Add(itemNode.Name, itemNode.Text);
                }
                else
                {
                    if (SelectFolderDictionary.ContainsKey(itemNode.Name))
                        SelectFolderDictionary.Remove(itemNode.Name);
                }

                if (itemNode.Nodes.Count > 0)
                {
                    FillSelectFolderDictionary(itemNode);
                }
            }
        }

        /// <summary>
        /// 按钮可用状态设置
        /// </summary>
        private void ButtonEnable(bool isChecked)
        {
            btnSure.Enabled = isChecked;
            btnClearAll.Enabled = isChecked;
        }

        /// <summary>
        /// 节点选中状态
        /// </summary>
        /// <param name="treeNode">节点</param>
        /// <param name="isChecked">是否选中</param>
        private void NodeChecked(TreeNode treeNode, ref bool isChecked)
        {
            TreeNodeCollection treeNodes = treeNode == null ? tvFolders.Nodes : treeNode.Nodes;
            foreach (TreeNode itemNode in treeNodes)
            {
                if (itemNode.Checked)
                {
                    isChecked = true;
                    break;
                }

                if (itemNode.Nodes.Count > 0)
                {
                    NodeChecked(itemNode, ref isChecked);
                }
            }
        }

        /// <summary>
        /// 取消按钮选中
        /// </summary>
        /// <param name="treeNode">节点</param>
        private void ClancelChecked(TreeNode treeNode)
        {
            var treeNodes = treeNode == null ? tvFolders.Nodes : treeNode.Nodes;
            foreach (TreeNode itemNode in treeNodes)
            {
                itemNode.Checked = false;
                if (itemNode.Nodes.Count > 0)
                {
                    ClancelChecked(itemNode);
                }
            }
        }
        #endregion
    }
}
