namespace ICP.FCM.Common.UI.DipatchLogForm
{
    partial class DispatchFileLogShow
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
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.gvDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAcceptBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAcceptDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsTransTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rcmbCompany = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.rlueCompany = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRetry = new DevExpress.XtraEditors.SimpleButton();
            this.labState = new System.Windows.Forms.Label();
            this.DateEnd = new DevExpress.XtraEditors.DateEdit();
            this.DateStart = new DevExpress.XtraEditors.DateEdit();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.labTo = new System.Windows.Forms.Label();
            this.labDateStrat = new System.Windows.Forms.Label();
            this.btnOpen = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.chkDispatchState = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDispatchState.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.DispathLogData);
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bindingSource1;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(0, 42);
            this.gcMain.MainView = this.gvDetails;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1,
            this.rcmbCompany,
            this.rlueCompany,
            this.repositoryItemCheckEdit1});
            this.gcMain.Size = new System.Drawing.Size(972, 416);
            this.gcMain.TabIndex = 22;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDetails});
            // 
            // gvDetails
            // 
            this.gvDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colOperationNo,
            this.colOperationType,
            this.colCreateBy,
            this.colCreateDate,
            this.colAcceptBy,
            this.colAcceptDate,
            this.colIsTransTo,
            this.colState});
            this.gvDetails.GridControl = this.gcMain;
            this.gvDetails.Name = "gvDetails";
            this.gvDetails.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvDetails.OptionsPrint.EnableAppearanceEvenRow = true;
            this.gvDetails.OptionsPrint.EnableAppearanceOddRow = true;
            this.gvDetails.OptionsSelection.MultiSelect = true;
            this.gvDetails.OptionsView.ColumnAutoWidth = false;
            this.gvDetails.OptionsView.EnableAppearanceEvenRow = true;
            this.gvDetails.OptionsView.ShowGroupPanel = false;
            this.gvDetails.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvDetails_FocusedRowChanged);
            this.gvDetails.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gvDetails_CustomColumnDisplayText);
            this.gvDetails.DoubleClick += new System.EventHandler(this.gvDetails_DoubleClick);
            // 
            // colOperationNo
            // 
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.OptionsColumn.AllowEdit = false;
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 0;
            this.colOperationNo.Width = 155;
            // 
            // colOperationType
            // 
            this.colOperationType.FieldName = "OperationType";
            this.colOperationType.Name = "colOperationType";
            this.colOperationType.OptionsColumn.AllowEdit = false;
            this.colOperationType.Visible = true;
            this.colOperationType.VisibleIndex = 1;
            this.colOperationType.Width = 137;
            // 
            // colCreateBy
            // 
            this.colCreateBy.FieldName = "CreateBy";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.OptionsColumn.AllowEdit = false;
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 2;
            this.colCreateBy.Width = 88;
            // 
            // colCreateDate
            // 
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 3;
            this.colCreateDate.Width = 107;
            // 
            // colAcceptBy
            // 
            this.colAcceptBy.FieldName = "AcceptBy";
            this.colAcceptBy.Name = "colAcceptBy";
            this.colAcceptBy.OptionsColumn.AllowEdit = false;
            this.colAcceptBy.Visible = true;
            this.colAcceptBy.VisibleIndex = 4;
            this.colAcceptBy.Width = 93;
            // 
            // colAcceptDate
            // 
            this.colAcceptDate.FieldName = "AcceptDate";
            this.colAcceptDate.Name = "colAcceptDate";
            this.colAcceptDate.OptionsColumn.AllowEdit = false;
            this.colAcceptDate.Visible = true;
            this.colAcceptDate.VisibleIndex = 5;
            this.colAcceptDate.Width = 110;
            // 
            // colIsTransTo
            // 
            this.colIsTransTo.FieldName = "IsTransTo";
            this.colIsTransTo.Name = "colIsTransTo";
            this.colIsTransTo.OptionsColumn.AllowEdit = false;
            this.colIsTransTo.Visible = true;
            this.colIsTransTo.VisibleIndex = 6;
            this.colIsTransTo.Width = 136;
            // 
            // colState
            // 
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.OptionsColumn.AllowEdit = false;
            this.colState.Visible = true;
            this.colState.VisibleIndex = 7;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // rcmbCompany
            // 
            this.rcmbCompany.AutoHeight = false;
            this.rcmbCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbCompany.Name = "rcmbCompany";
            // 
            // rlueCompany
            // 
            this.rlueCompany.AutoHeight = false;
            this.rlueCompany.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rlueCompany.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "公司", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CShortName", 120, "公司")});
            this.rlueCompany.Name = "rlueCompany";
            this.rlueCompany.NullText = "";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkDispatchState);
            this.panel1.Controls.Add(this.btnRetry);
            this.panel1.Controls.Add(this.labState);
            this.panel1.Controls.Add(this.DateEnd);
            this.panel1.Controls.Add(this.DateStart);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.labTo);
            this.panel1.Controls.Add(this.labDateStrat);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(972, 42);
            this.panel1.TabIndex = 23;
            // 
            // btnRetry
            // 
            this.btnRetry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRetry.Location = new System.Drawing.Point(821, 10);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(75, 23);
            this.btnRetry.TabIndex = 12;
            this.btnRetry.Text = "&Retry";
            this.btnRetry.Visible = false;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // labState
            // 
            this.labState.AutoSize = true;
            this.labState.Location = new System.Drawing.Point(12, 14);
            this.labState.Name = "labState";
            this.labState.Size = new System.Drawing.Size(55, 14);
            this.labState.TabIndex = 10;
            this.labState.Text = "分发状态";
            // 
            // DateEnd
            // 
            this.DateEnd.EditValue = new System.DateTime(2015, 10, 26, 15, 56, 26, 0);
            this.DateEnd.Location = new System.Drawing.Point(428, 11);
            this.DateEnd.Name = "DateEnd";
            this.DateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEnd.Properties.Mask.EditMask = "";
            this.DateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.DateEnd.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.DateEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateEnd.Size = new System.Drawing.Size(112, 21);
            this.DateEnd.TabIndex = 3;
            this.DateEnd.TabStop = false;
            // 
            // DateStart
            // 
            this.DateStart.EditValue = new System.DateTime(2015, 10, 26, 15, 56, 12, 0);
            this.DateStart.Location = new System.Drawing.Point(285, 11);
            this.DateStart.Name = "DateStart";
            this.DateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateStart.Properties.Mask.EditMask = "";
            this.DateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.DateStart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.DateStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateStart.Size = new System.Drawing.Size(112, 21);
            this.DateStart.TabIndex = 2;
            this.DateStart.TabStop = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(658, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // labTo
            // 
            this.labTo.AutoSize = true;
            this.labTo.Location = new System.Drawing.Point(403, 14);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(19, 14);
            this.labTo.TabIndex = 8;
            this.labTo.Text = "至";
            // 
            // labDateStrat
            // 
            this.labDateStrat.AutoSize = true;
            this.labDateStrat.Location = new System.Drawing.Point(224, 14);
            this.labDateStrat.Name = "labDateStrat";
            this.labDateStrat.Size = new System.Drawing.Size(55, 14);
            this.labDateStrat.TabIndex = 7;
            this.labDateStrat.Text = "分发时间";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Location = new System.Drawing.Point(740, 10);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 6;
            this.btnOpen.Text = "&Open";
            this.btnOpen.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(576, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkDispatchState
            // 
            this.chkDispatchState.Location = new System.Drawing.Point(73, 11);
            this.chkDispatchState.Name = "chkDispatchState";
            this.chkDispatchState.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.chkDispatchState.Properties.Appearance.Options.UseBackColor = true;
            this.chkDispatchState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkDispatchState.Size = new System.Drawing.Size(145, 21);
            this.chkDispatchState.SpecifiedBackColor = System.Drawing.Color.White;
            this.chkDispatchState.TabIndex = 13;
            // 
            // DispatchFileLogShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcMain);
            this.Controls.Add(this.panel1);
            this.Name = "DispatchFileLogShow";
            this.Size = new System.Drawing.Size(972, 458);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.gcMain, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDispatchState.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcmbCompany;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlueCompany;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.Panel panel1;
        protected DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Label labTo;
        private System.Windows.Forms.Label labDateStrat;
        protected DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.DateEdit DateEnd;
        private DevExpress.XtraEditors.DateEdit DateStart;
        protected DevExpress.XtraEditors.SimpleButton btnOpen;
        private System.Windows.Forms.Label labState;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationType;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colAcceptBy;
        private DevExpress.XtraGrid.Columns.GridColumn colAcceptDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsTransTo;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        protected DevExpress.XtraEditors.SimpleButton btnRetry;
        protected Framework.ClientComponents.Controls.LWImageComboBoxEdit chkDispatchState;
    }
}
