namespace ICP.FAM.UI.BatchBill
{
    partial class OperationListFinderPart
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.dtpEndDate = new DevExpress.XtraEditors.DateEdit();
            this.dteStartDate = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.scmbOperationType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.lblOperationType = new DevExpress.XtraEditors.LabelControl();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.txtOperationNo = new DevExpress.XtraEditors.TextEdit();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsOperationCommInfo = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gcManifest = new DevExpress.XtraGrid.GridControl();
            this.bsManifest = new System.Windows.Forms.BindingSource(this.components);
            this.formsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvManifest = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.cmbOperationType = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scmbOperationType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).BeginInit();
            this.panelCenter.SuspendLayout();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOperationCommInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcManifest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsManifest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvManifest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.panelBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.dtpEndDate);
            this.panelTop.Controls.Add(this.dteStartDate);
            this.panelTop.Controls.Add(this.labTo);
            this.panelTop.Controls.Add(this.labFrom);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.scmbOperationType);
            this.panelTop.Controls.Add(this.lblOperationType);
            this.panelTop.Controls.Add(this.labOperationNo);
            this.panelTop.Controls.Add(this.txtOperationNo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(964, 38);
            this.panelTop.TabIndex = 0;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.EditValue = new System.DateTime(2016, 2, 29, 13, 51, 15, 149);
            this.dtpEndDate.Location = new System.Drawing.Point(721, 11);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpEndDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dtpEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpEndDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dtpEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpEndDate.Properties.Mask.EditMask = "";
            this.dtpEndDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtpEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpEndDate.Size = new System.Drawing.Size(142, 21);
            this.dtpEndDate.TabIndex = 62;
            // 
            // dteStartDate
            // 
            this.dteStartDate.EditValue = new System.DateTime(2016, 2, 29, 13, 51, 27, 3);
            this.dteStartDate.Location = new System.Drawing.Point(531, 11);
            this.dteStartDate.Name = "dteStartDate";
            this.dteStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteStartDate.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";
            this.dteStartDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteStartDate.Properties.EditFormat.FormatString = "yyyy/MM/dd";
            this.dteStartDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteStartDate.Properties.Mask.EditMask = "";
            this.dteStartDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteStartDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteStartDate.Size = new System.Drawing.Size(142, 21);
            this.dteStartDate.TabIndex = 61;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(689, 14);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(15, 14);
            this.labTo.TabIndex = 64;
            this.labTo.Text = "To";
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(496, 14);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(27, 14);
            this.labFrom.TabIndex = 63;
            this.labFrom.Text = "From";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(869, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 60;
            this.btnSearch.Text = "&Search";
            // 
            // scmbOperationType
            // 
            this.scmbOperationType.Location = new System.Drawing.Point(347, 11);
            this.scmbOperationType.Name = "scmbOperationType";
            this.scmbOperationType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.scmbOperationType.Properties.Appearance.Options.UseBackColor = true;
            this.scmbOperationType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.scmbOperationType.Size = new System.Drawing.Size(142, 21);
            this.scmbOperationType.SpecifiedBackColor = System.Drawing.Color.White;
            this.scmbOperationType.TabIndex = 59;
            // 
            // lblOperationType
            // 
            this.lblOperationType.Location = new System.Drawing.Point(251, 14);
            this.lblOperationType.Name = "lblOperationType";
            this.lblOperationType.Size = new System.Drawing.Size(86, 14);
            this.lblOperationType.TabIndex = 58;
            this.lblOperationType.Text = "Operation Type";
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(15, 14);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(73, 14);
            this.labOperationNo.TabIndex = 58;
            this.labOperationNo.Text = "Operation No";
            // 
            // txtOperationNo
            // 
            this.txtOperationNo.Location = new System.Drawing.Point(96, 11);
            this.txtOperationNo.Name = "txtOperationNo";
            this.txtOperationNo.Size = new System.Drawing.Size(142, 21);
            this.txtOperationNo.TabIndex = 57;
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.scMain);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 38);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(964, 505);
            this.panelCenter.TabIndex = 1;
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.gcMain);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.gcManifest);
            this.scMain.Size = new System.Drawing.Size(964, 505);
            this.scMain.SplitterDistance = 448;
            this.scMain.TabIndex = 0;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsOperationCommInfo;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 0);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.rcmbState,
            this.cmbOperationType});
            this.gcMain.Size = new System.Drawing.Size(448, 505);
            this.gcMain.TabIndex = 3;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsOperationCommInfo
            // 
            this.bsOperationCommInfo.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.OperationCommonInfo);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOperationNo,
            this.colOperationType,
            this.colOperationDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colOperationNo
            // 
            this.colOperationNo.Caption = "No";
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 0;
            this.colOperationNo.Width = 140;
            // 
            // colOperationType
            // 
            this.colOperationType.Caption = "Type";
            this.colOperationType.ColumnEdit = this.cmbOperationType;
            this.colOperationType.FieldName = "OperationType";
            this.colOperationType.Name = "colOperationType";
            this.colOperationType.Visible = true;
            this.colOperationType.VisibleIndex = 1;
            this.colOperationType.Width = 120;
            // 
            // colOperationDate
            // 
            this.colOperationDate.Caption = "Operation Date";
            this.colOperationDate.FieldName = "OperationDate";
            this.colOperationDate.Name = "colOperationDate";
            this.colOperationDate.Visible = true;
            this.colOperationDate.VisibleIndex = 2;
            this.colOperationDate.Width = 120;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // gcManifest
            // 
            this.gcManifest.DataSource = this.bsManifest;
            this.gcManifest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcManifest.Location = new System.Drawing.Point(0, 0);
            this.gcManifest.MainView = this.gvManifest;
            this.gcManifest.Name = "gcManifest";
            this.gcManifest.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2,
            this.repositoryItemImageComboBox1});
            this.gcManifest.Size = new System.Drawing.Size(512, 505);
            this.gcManifest.TabIndex = 4;
            this.gcManifest.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvManifest});
            // 
            // bsManifest
            // 
            this.bsManifest.DataSource = this.formsBindingSource;
            // 
            // formsBindingSource
            // 
            this.formsBindingSource.DataMember = "Forms";
            this.formsBindingSource.DataSource = this.bsOperationCommInfo;
            // 
            // gvManifest
            // 
            this.gvManifest.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNo,
            this.colType});
            this.gvManifest.GridControl = this.gcManifest;
            this.gvManifest.Name = "gvManifest";
            this.gvManifest.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvManifest.OptionsBehavior.Editable = false;
            this.gvManifest.OptionsSelection.MultiSelect = true;
            this.gvManifest.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvManifest.OptionsView.EnableAppearanceEvenRow = true;
            this.gvManifest.OptionsView.ShowDetailButtons = false;
            this.gvManifest.OptionsView.ShowGroupPanel = false;
            // 
            // colNo
            // 
            this.colNo.FieldName = "No";
            this.colNo.Name = "colNo";
            this.colNo.Visible = true;
            this.colNo.VisibleIndex = 0;
            this.colNo.Width = 140;
            // 
            // colType
            // 
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.Visible = true;
            this.colType.VisibleIndex = 1;
            this.colType.Width = 120;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.btnCancel);
            this.panelBottom.Controls.Add(this.btnOK);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 543);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(964, 36);
            this.panelBottom.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(452, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 60;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(358, 7);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 60;
            this.btnOK.Text = "&OK";
            // 
            // cmbOperationType
            // 
            this.cmbOperationType.AutoHeight = false;
            this.cmbOperationType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOperationType.Name = "cmbOperationType";
            // 
            // OperationListFinderPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "OperationListFinderPart";
            this.Size = new System.Drawing.Size(964, 579);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scmbOperationType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOperationNo.Properties)).EndInit();
            this.panelCenter.ResumeLayout(false);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsOperationCommInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcManifest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsManifest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvManifest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.panelBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperationType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelCenter;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        protected DevExpress.XtraEditors.LabelControl labOperationNo;
        protected DevExpress.XtraEditors.TextEdit txtOperationNo;
        public Framework.ClientComponents.Controls.LWImageComboBoxEdit scmbOperationType;
        protected DevExpress.XtraEditors.LabelControl lblOperationType;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Panel panelBottom;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.DateEdit dtpEndDate;
        private DevExpress.XtraEditors.DateEdit dteStartDate;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraGrid.GridControl gcManifest;
        private DevExpress.XtraGrid.Views.Grid.GridView gvManifest;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.Windows.Forms.BindingSource bsOperationCommInfo;
        private System.Windows.Forms.BindingSource bsManifest;
        private System.Windows.Forms.BindingSource formsBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationType;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationDate;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colNo;
        private System.Windows.Forms.SplitContainer scMain;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbOperationType;
    }
}
