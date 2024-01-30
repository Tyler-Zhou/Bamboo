namespace ICP.FCM.Common.UI.Document
{
    partial class BindCorrelation
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
            this.gridControlList = new DevExpress.XtraGrid.GridControl();
            this.gridViewList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsDirty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIMessageID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentTypeValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateByName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUpdateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOriginalPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPreviewPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileSources = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHtmlContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FileSystem.ServiceInterface.DocumentInfo);
            // 
            // gridControlList
            // 
            this.gridControlList.DataSource = this.bindingSource1;
            this.gridControlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlList.Location = new System.Drawing.Point(0, 40);
            this.gridControlList.MainView = this.gridViewList;
            this.gridControlList.Name = "gridControlList";
            this.gridControlList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemLookUpEdit1});
            this.gridControlList.Size = new System.Drawing.Size(589, 328);
            this.gridControlList.TabIndex = 5;
            this.gridControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
            // 
            // gridViewList
            // 
            this.gridViewList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colIsDirty,
            this.colOperationID,
            this.colIMessageID,
            this.colType,
            this.colDocumentType,
            this.colDocumentTypeValue,
            this.colFormType,
            this.colDocumentState,
            this.colName,
            this.colCreateByName,
            this.colCreateBy,
            this.colSelected,
            this.colCreateDate,
            this.colUpdateBy,
            this.colUpdateByName,
            this.colUpdateDate,
            this.colDocumentTypeName,
            this.colRemark,
            this.colOriginalPath,
            this.colPreviewPath,
            this.colFileSources,
            this.colHtmlContent,
            this.colContent,
            this.colState,
            this.colFormId});
            this.gridViewList.GridControl = this.gridControlList;
            this.gridViewList.Name = "gridViewList";
            this.gridViewList.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewList.OptionsView.ShowGroupPanel = false;
            this.gridViewList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewList_CellValueChanged);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colIsDirty
            // 
            this.colIsDirty.FieldName = "IsDirty";
            this.colIsDirty.Name = "colIsDirty";
            // 
            // colOperationID
            // 
            this.colOperationID.FieldName = "OperationID";
            this.colOperationID.Name = "colOperationID";
            // 
            // colIMessageID
            // 
            this.colIMessageID.FieldName = "IMessageID";
            this.colIMessageID.Name = "colIMessageID";
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            // 
            // colDocumentType
            // 
            this.colDocumentType.AppearanceCell.Options.UseTextOptions = true;
            this.colDocumentType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colDocumentType.Caption = "DocumentType";
            this.colDocumentType.FieldName = "DocumentType";
            this.colDocumentType.Name = "colDocumentType";
            this.colDocumentType.OptionsColumn.AllowEdit = false;
            this.colDocumentType.Visible = true;
            this.colDocumentType.VisibleIndex = 0;
            this.colDocumentType.Width = 134;
            // 
            // colDocumentTypeValue
            // 
            this.colDocumentTypeValue.FieldName = "DocumentTypeValue";
            this.colDocumentTypeValue.Name = "colDocumentTypeValue";
            // 
            // colFormType
            // 
            this.colFormType.FieldName = "FormType";
            this.colFormType.Name = "colFormType";
            // 
            // colDocumentState
            // 
            this.colDocumentState.FieldName = "DocumentState";
            this.colDocumentState.Name = "colDocumentState";
            // 
            // colName
            // 
            this.colName.Caption = "DocumentName";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 1;
            this.colName.Width = 269;
            // 
            // colCreateByName
            // 
            this.colCreateByName.Caption = "CreateBy";
            this.colCreateByName.FieldName = "CreateByName";
            this.colCreateByName.Name = "colCreateByName";
            this.colCreateByName.OptionsColumn.AllowEdit = false;
            this.colCreateByName.Width = 78;
            // 
            // colCreateBy
            // 
            this.colCreateBy.FieldName = "CreateBy";
            this.colCreateBy.Name = "colCreateBy";
            // 
            // colSelected
            // 
            this.colSelected.FieldName = "Selected";
            this.colSelected.Name = "colSelected";
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "CreateDate";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Width = 92;
            // 
            // colUpdateBy
            // 
            this.colUpdateBy.FieldName = "UpdateBy";
            this.colUpdateBy.Name = "colUpdateBy";
            // 
            // colUpdateByName
            // 
            this.colUpdateByName.FieldName = "UpdateByName";
            this.colUpdateByName.Name = "colUpdateByName";
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.FieldName = "UpdateDate";
            this.colUpdateDate.Name = "colUpdateDate";
            // 
            // colDocumentTypeName
            // 
            this.colDocumentTypeName.Caption = "DocumentType";
            this.colDocumentTypeName.FieldName = "DocumentTypeName";
            this.colDocumentTypeName.Name = "colDocumentTypeName";
            this.colDocumentTypeName.OptionsColumn.AllowEdit = false;
            this.colDocumentTypeName.Width = 141;
            // 
            // colRemark
            // 
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            // 
            // colOriginalPath
            // 
            this.colOriginalPath.FieldName = "OriginalPath";
            this.colOriginalPath.Name = "colOriginalPath";
            // 
            // colPreviewPath
            // 
            this.colPreviewPath.FieldName = "PreviewPath";
            this.colPreviewPath.Name = "colPreviewPath";
            // 
            // colFileSources
            // 
            this.colFileSources.FieldName = "FileSources";
            this.colFileSources.Name = "colFileSources";
            // 
            // colHtmlContent
            // 
            this.colHtmlContent.FieldName = "HtmlContent";
            this.colHtmlContent.Name = "colHtmlContent";
            // 
            // colContent
            // 
            this.colContent.FieldName = "Content";
            this.colContent.Name = "colContent";
            // 
            // colState
            // 
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            // 
            // colFormId
            // 
            this.colFormId.Caption = "M/H BLNO";
            this.colFormId.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colFormId.FieldName = "FormId";
            this.colFormId.Name = "colFormId";
            this.colFormId.Visible = true;
            this.colFormId.VisibleIndex = 2;
            this.colFormId.Width = 170;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("No", 80, "MBL/HBL"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "HBLID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 40);
            this.panel1.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(505, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOk.Location = new System.Drawing.Point(418, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Confirm";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // BindCorrelation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlList);
            this.Controls.Add(this.panel1);
            this.Name = "BindCorrelation";
            this.Size = new System.Drawing.Size(589, 368);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.gridControlList, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewList;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colIsDirty;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationID;
        private DevExpress.XtraGrid.Columns.GridColumn colIMessageID;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentType;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentTypeValue;
        private DevExpress.XtraGrid.Columns.GridColumn colFormType;
        private DevExpress.XtraGrid.Columns.GridColumn colFormId;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentState;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateByName;
        private DevExpress.XtraGrid.Columns.GridColumn colUpdateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colOriginalPath;
        private DevExpress.XtraGrid.Columns.GridColumn colPreviewPath;
        private DevExpress.XtraGrid.Columns.GridColumn colFileSources;
        private DevExpress.XtraGrid.Columns.GridColumn colHtmlContent;
        private DevExpress.XtraGrid.Columns.GridColumn colContent;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private System.Windows.Forms.Panel panel1;
        protected DevExpress.XtraEditors.SimpleButton btnClose;
        protected DevExpress.XtraEditors.SimpleButton btnOk;

    }
}
