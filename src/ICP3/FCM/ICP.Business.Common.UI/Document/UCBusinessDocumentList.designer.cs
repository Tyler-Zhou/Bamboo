using DevExpress.XtraGrid.Columns;

namespace ICP.Business.Common.UI.Document
{
    partial class UCBusinessDocumentList
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridControlDocument = new DevExpress.XtraGrid.GridControl();
            this.cmsDocumentGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenWith = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewDocument = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.popupMenuUpload = new DevExpress.XtraBars.PopupMenu(this.components);
            this.cmsSelectType = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDocument)).BeginInit();
            this.cmsDocumentGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuUpload)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlDocument
            // 
            this.gridControlDocument.AllowDrop = true;
            this.gridControlDocument.ContextMenuStrip = this.cmsDocumentGrid;
            this.gridControlDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDocument.Location = new System.Drawing.Point(0, 0);
            this.gridControlDocument.MainView = this.gridViewDocument;
            this.gridControlDocument.Name = "gridControlDocument";
            this.gridControlDocument.Size = new System.Drawing.Size(475, 220);
            this.gridControlDocument.TabIndex = 0;
            this.gridControlDocument.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDocument});
            this.gridControlDocument.DragDrop += new System.Windows.Forms.DragEventHandler(this.gridControlDocument_DragDrop);
            this.gridControlDocument.DragEnter += new System.Windows.Forms.DragEventHandler(this.gridControlDocument_DragEnter);
            // 
            // cmsDocumentGrid
            // 
            this.cmsDocumentGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiDelete,
            this.tsmiOpen,
            this.tsmiOpenWith,
            this.tsmiSaveAs});
            this.cmsDocumentGrid.Name = "cmsDocumentGrid";
            this.cmsDocumentGrid.Size = new System.Drawing.Size(125, 114);
            this.cmsDocumentGrid.Opening += new System.ComponentModel.CancelEventHandler(this.cmsDocumentGrid_Opening);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(124, 22);
            this.tsmiAdd.Text = "新增";
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(124, 22);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // tsmiOpen
            // 
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(124, 22);
            this.tsmiOpen.Text = "打开";
            this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // tsmiOpenWith
            // 
            this.tsmiOpenWith.Name = "tsmiOpenWith";
            this.tsmiOpenWith.Size = new System.Drawing.Size(124, 22);
            this.tsmiOpenWith.Text = "打开方式";
            this.tsmiOpenWith.Click += new System.EventHandler(this.tsmiOpenWith_Click);
            // 
            // tsmiSaveAs
            // 
            this.tsmiSaveAs.Name = "tsmiSaveAs";
            this.tsmiSaveAs.Size = new System.Drawing.Size(124, 22);
            this.tsmiSaveAs.Text = "另存为";
            this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
            // 
            // gridViewDocument
            // 
            this.gridViewDocument.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnId,
            this.gridColumnType,
            this.gridColumnName});
            this.gridViewDocument.GridControl = this.gridControlDocument;
            this.gridViewDocument.Name = "gridViewDocument";
            this.gridViewDocument.OptionsSelection.MultiSelect = true;
            this.gridViewDocument.OptionsView.ShowGroupPanel = false;
            this.gridViewDocument.OptionsView.ShowIndicator = false;            
            this.gridViewDocument.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewDocument_RowClick);
            this.gridViewDocument.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewDocument_RowStyle);
            // 
            // gridColumnId
            // 
            this.gridColumnId.Caption = "Id";
            this.gridColumnId.FieldName = "Id";
            this.gridColumnId.Name = "gridColumnId";
            this.gridColumnId.Visible = false;
            this.gridColumnId.VisibleIndex = -1;
            // 
            // gridColumnType
            // 
            this.gridColumnType.OptionsColumn.AllowEdit = false;
            this.gridColumnType.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnType.Caption = "Type";
            this.gridColumnType.FieldName = "DocumentType";
            this.gridColumnType.Name = "gridColumnType";
            this.gridColumnType.Visible = true;
            this.gridColumnType.VisibleIndex = 0;
            this.gridColumnType.Width = 60;
            // 
            // gridColumnName
            // 
            this.gridColumnName.OptionsColumn.AllowEdit = true;
            this.gridColumnName.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumnName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumnName.Caption = "File";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            this.gridColumnName.Width = 401;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ICP.FileSystem.ServiceInterface.DocumentInfo);
            // 
            // popupMenuUpload
            // 
            this.popupMenuUpload.Name = "popupMenuUpload";
            // 
            // cmsSelectType
            // 
            this.cmsSelectType.Name = "contextMenuStrip1";
            this.cmsSelectType.Size = new System.Drawing.Size(61, 4);
            // 
            // UCBusinessDocumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlDocument);
            this.Name = "UCBusinessDocumentList";
            this.Size = new System.Drawing.Size(475, 220);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDocument)).EndInit();
            this.cmsDocumentGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuUpload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlDocument;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDocument;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.ContextMenuStrip cmsDocumentGrid;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenWith;
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
        private DevExpress.XtraBars.PopupMenu popupMenuUpload;
        private System.Windows.Forms.ContextMenuStrip cmsSelectType;

    }
}
