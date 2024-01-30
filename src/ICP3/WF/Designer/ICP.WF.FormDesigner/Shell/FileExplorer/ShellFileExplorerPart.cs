
//-----------------------------------------------------------------------
// <copyright file="ShellFileExplorer.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Windows.Forms;
    using ICP.Framework.ClientComponents.UIFramework;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.Client;
    using System.Linq;
using Microsoft.Practices.CompositeUI;

	/// <summary>
	/// 表单文件浏览器.
	/// </summary>
    public class ShellFileExplorerPart : BaseListPart, IFileExplorerPart
    {

        #region 本地属性

        private ImageList imageListSolution;
        public DevExpress.XtraTreeList.TreeList treeView1;

        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumnName;
        private IList<DocumentNode> nodeList = new List<DocumentNode>();
        #endregion

        #region 对象初始化与释放

        public ShellFileExplorerPart()
		{
			InitializeComponent();

            if (DesignMode == false)
            {
                InitControl();
            }
            this.Disposed += delegate {

                this.treeView1.CellValueChanged -= this.treeView1_CellValueChanged;
                this.treeView1.DoubleClick -= this.treeView1_DoubleClick;
                this.treeView1.GetStateImage -= this.treeView1_GetStateImage;
                this.OnException = null;
                this.OpentDesignFormEvent = null;
                this.CurrentDesignerHost = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            InitTreeView();

           // treeView1.DoubleClick += new EventHandler(treeView1_DoubleClick);

        
        }
        /// <summary>
        /// 初始化控件树
        /// </summary>
        private void InitTreeView()
        {
            nodeList.Clear();
            treeView1.Nodes.Clear();

            DocumentNode folderNode = new DocumentNode(DefaultConfigMananger.Default.FormDesignFolder.Path, ToolboxGroupType.Form);
           
            nodeList=folderNode.GetList();
            treeView1.DataSource = nodeList;
            treeView1.ExpandAll();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellFileExplorerPart));
            this.imageListSolution = new System.Windows.Forms.ImageList(this.components);
            this.treeView1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumnName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            ((System.ComponentModel.ISupportInitialize)(this.treeView1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageListSolution
            // 
            this.imageListSolution.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSolution.ImageStream")));
            this.imageListSolution.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSolution.Images.SetKeyName(0, "文件夹.ico");
            this.imageListSolution.Images.SetKeyName(1, "个人文档.ico");
            this.imageListSolution.Images.SetKeyName(2, "主表.ico");
            this.imageListSolution.Images.SetKeyName(3, "跳向页.ico");
            this.imageListSolution.Images.SetKeyName(4, "文件夹打开状态.ico");
            // 
            // treeView1
            // 
            this.treeView1.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeView1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeView1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeView1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeView1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumnName});
            this.treeView1.ColumnsImageList = this.imageListSolution;
            resources.ApplyResources(this.treeView1, "treeView1");
            this.treeView1.ImageIndexFieldName = "Name";
            this.treeView1.Name = "treeView1";
            this.treeView1.OptionsBehavior.Editable = false;
            this.treeView1.OptionsView.ShowColumns = false;
            this.treeView1.OptionsView.ShowIndicator = false;
            this.treeView1.StateImageList = this.imageListSolution;
            this.treeView1.Tag = "";
            this.treeView1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeView1_GetStateImage);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            this.treeView1.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeView1_CellValueChanged);
            // 
            // treeListColumnName
            // 
            resources.ApplyResources(this.treeListColumnName, "treeListColumnName");
            this.treeListColumnName.FieldName = "Name";
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.UnboundType = DevExpress.XtraTreeList.Data.UnboundColumnType.String;
            // 
            // ShellFileExplorerPart
            // 
            this.Controls.Add(this.treeView1);
            this.Name = "ShellFileExplorerPart";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.treeView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        #region 事件处理

        void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.FocusedNode == null) return;

            if (OpentDesignFormEvent != null)
            {
                OpentDesignFormEvent(treeView1.FocusedNode, e);
            }
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNodeType data = e.Node.Tag as TreeNodeType;
            if (data != null && data.Type == TreeNodeTypeEnum.Folder)
            {
                e.Node.ImageIndex = 4;
                e.Node.SelectedImageIndex = 4;
            }
        }

        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNodeType data = e.Node.Tag as TreeNodeType;
            if (data != null && data.Type == TreeNodeTypeEnum.Folder)
            {
                e.Node.ImageIndex = 0;
                e.Node.SelectedImageIndex = 0;
            }
        }

        #endregion

        #region 外部接口

        public event ExceptionEventHandler OnException;

        public IDesignerHost CurrentDesignerHost { get; set; }

        /// <summary>
        /// 接点双击触发
        /// </summary>
        public event EventHandler OpentDesignFormEvent;
        
        /// <summary>
        /// 添加一个文件夹节点
        /// </summary>
        /// <param name="nodeText">节点名称</param>
        public void AddFolderFileNode(string nodeText)
        {
            AddFolderFileNode(nodeText,string.Empty);
        }


        /// <summary>
        /// 添加一个文件夹节点
        /// </summary>
        /// <param name="nodeText">节点名称</param>
        /// <param name="fileName">文件夹路径</param>
        public void AddFolderFileNode(string nodeText,string fileName)
        {
            DocumentNode documentNode = new DocumentNode();
            documentNode.Name = nodeText;
            documentNode.ID = nodeList.Count + 2;
            documentNode.ParentID = 1;
            documentNode.DocumentNodeType = DocumentNodeType.Folder;

            nodeList.Add(documentNode);

            this.treeView1.RefreshDataSource();

        }

        
        /// <summary>
        /// 添加一个表单节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
		public void AddFormFileNode(string nodeText)
		{
            AddFormFileNode(nodeText,string.Empty);
        }

        /// <summary>
        /// 添加一个表单节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        /// <param name="filePath">表单文件路径</param>
        public void AddFormFileNode(string nodeText, string filePath)
        {
            var q = from d in nodeList where d.Name == nodeText select d;

            if (q.ToList<DocumentNode>().Count==0)
            {
                DocumentNode documentNode = new DocumentNode();
                documentNode.Name = nodeText;
                documentNode.ID = nodeList.Count + 2;
                documentNode.ParentID = 1;
                documentNode.DocumentNodeType = DocumentNodeType.File;
                documentNode.Path = filePath;

                nodeList.Add(documentNode);

                this.treeView1.RefreshDataSource();
            }
        }

        
        /// <summary>
        /// 添加一个数据源文件
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        public void AddDataDesignFileNode(string nodeText)
        {
            AddDataDesignFileNode(nodeText,string.Empty);
        }

        
        /// <summary>
        /// 添加一个数据源文件节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        /// <param name="fileName">文件名称</param>
        public void AddDataDesignFileNode(string nodeText,string fileName)
        {
            DocumentNode documentNode = new DocumentNode();
            documentNode.Name = nodeText;
            documentNode.ID = nodeList.Count + 2;
            documentNode.ParentID = 1;
            documentNode.DocumentNodeType = DocumentNodeType.File;

            nodeList.Add(documentNode);

            this.treeView1.RefreshDataSource();

        }

        /// <summary>
        /// 删除指定文本节点
        /// </summary>
        /// <param name="nodeText">节点文本</param>
        public void RemoveFileNode(string nodeText)
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
               RemoveNode(node, nodeText);
            }
        }

        /// <summary>
        /// 删除所有节点
        /// </summary>
        public void RemoveAllNode()
        {
            this.treeView1.Nodes.Clear();
        }
     
        #endregion


        #region 本地方法

        private void RemoveNode(TreeNode parent, string fileName)
        {
            foreach (TreeNode n in parent.Nodes)
            {
                if (n.Text.Equals(fileName))
                {
                    parent.Nodes.Remove(n);
                }

                if (n.Nodes.Count > 0)
                {
                    RemoveNode(n, fileName);
                }
            }
        }

        private TreeNode FindFirstNode(TreeNode parent, string fileName)
        {
            foreach (TreeNode n in parent.Nodes)
            {
                if (n.Text.Equals(fileName))
                {
                    return n;
                }

                TreeNode node = null;
                if (n.Nodes.Count > 0)
                {
                    node = FindFirstNode(n, fileName);
                }

                if (node != null) return node;
            }

            return null;
        }

        #endregion

        private void treeView1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            if((treeView1.GetDataRecordByNode(e.Node) as ICP.WF.ServiceInterface.Client.DocumentNode).DocumentNodeType==DocumentNodeType.File)
            {
               e.Node.SelectImageIndex=2;
               e.Node.StateImageIndex = 2;
            }
            else
            {
                e.Node.SelectImageIndex = 0;
                e.Node.StateImageIndex = 0;
            }
        }

        private void treeView1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            this.treeView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeView1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;

        }

       

    }



}
