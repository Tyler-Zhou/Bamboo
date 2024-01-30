namespace ICP.Business.Common.UI.Document
{
    partial class UCDocumentListGrid
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
            this.gridControlDocumentList = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAddDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.gridViewDocumentList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDocumentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDocumentList)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDocumentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlDocumentList
            // 
            this.gridControlDocumentList.ContextMenuStrip = this.contextMenuStrip;
            this.gridControlDocumentList.DataSource = this.bindingSource;
            this.gridControlDocumentList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDocumentList.Location = new System.Drawing.Point(0, 0);
            this.gridControlDocumentList.MainView = this.gridViewDocumentList;
            this.gridControlDocumentList.Name = "gridControlDocumentList";
            this.gridControlDocumentList.Size = new System.Drawing.Size(395, 177);
            this.gridControlDocumentList.TabIndex = 0;
            this.gridControlDocumentList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDocumentList});
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAddDocument,
            this.toolStripMenuItemDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(113, 48);
            // 
            // toolStripMenuItemAddDocument
            // 
            this.toolStripMenuItemAddDocument.Image = global::ICP.Business.Common.UI.Properties.Resources.upload;
            this.toolStripMenuItemAddDocument.Name = "toolStripMenuItemAddDocument";
            this.toolStripMenuItemAddDocument.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemAddDocument.Text = "Add";
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Image = global::ICP.Business.Common.UI.Properties.Resources.Delete_16;
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemDelete.Text = "Delete ";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.OnMenuItemClick);
            // 
            // gridViewDocumentList
            // 
            this.gridViewDocumentList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnId,
            this.gridColumnDocumentType,
            this.gridColumnName,
            this.gridColumnCreateBy,
            this.gridColumnCreateDate,
            this.gridColumnState});
            this.gridViewDocumentList.GridControl = this.gridControlDocumentList;
            this.gridViewDocumentList.Name = "gridViewDocumentList";
            this.gridViewDocumentList.OptionsCustomization.AllowGroup = false;
            this.gridViewDocumentList.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewDocumentList.OptionsCustomization.AllowRowSizing = true;
            this.gridViewDocumentList.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridViewDocumentList.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumnId
            // 
            this.gridColumnId.Caption = "Id";
            this.gridColumnId.FieldName = "Id";
            this.gridColumnId.Name = "gridColumnId";
            // 
            // gridColumnDocumentType
            // 
            this.gridColumnDocumentType.Caption = "Type";
            this.gridColumnDocumentType.FieldName = "DocumentType";
            this.gridColumnDocumentType.Name = "gridColumnDocumentType";
            this.gridColumnDocumentType.Visible = true;
            this.gridColumnDocumentType.VisibleIndex = 0;
            this.gridColumnDocumentType.Width = 63;
            // 
            // gridColumnName
            // 
            this.gridColumnName.Caption = "Name";
            this.gridColumnName.FieldName = "Name";
            this.gridColumnName.Name = "gridColumnName";
            this.gridColumnName.Visible = true;
            this.gridColumnName.VisibleIndex = 1;
            this.gridColumnName.Width = 186;
            // 
            // gridColumnCreateBy
            // 
            this.gridColumnCreateBy.Caption = "Create By";
            this.gridColumnCreateBy.FieldName = "CreateByName";
            this.gridColumnCreateBy.Name = "gridColumnCreateBy";
            // 
            // gridColumnCreateDate
            // 
            this.gridColumnCreateDate.Caption = "Upload Date";
            this.gridColumnCreateDate.FieldName = "CreateDate";
            this.gridColumnCreateDate.Name = "gridColumnCreateDate";
            // 
            // gridColumnState
            // 
            this.gridColumnState.Caption = "Upload State";
            this.gridColumnState.FieldName = "State";
            this.gridColumnState.Name = "gridColumnState";
            this.gridColumnState.Visible = true;
            this.gridColumnState.VisibleIndex = 2;
            this.gridColumnState.Width = 125;
            // 
            // UCDocumentListGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlDocumentList);
            this.Name = "UCDocumentListGrid";
            this.Size = new System.Drawing.Size(395, 177);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDocumentList)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDocumentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlDocumentList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDocumentList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnId;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDocumentType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnState;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAddDocument;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}
