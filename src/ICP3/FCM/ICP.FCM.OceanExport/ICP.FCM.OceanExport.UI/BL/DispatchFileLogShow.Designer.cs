namespace ICP.FCM.OceanExport.UI.BL
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
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsTransTo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rcmbCompany = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.rlueCompany = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAll = new DevExpress.XtraEditors.SimpleButton();
            this.labTo = new System.Windows.Forms.Label();
            this.labDateStrat = new System.Windows.Forms.Label();
            this.ckbState = new System.Windows.Forms.CheckBox();
            this.ckbIsTransTo = new System.Windows.Forms.CheckBox();
            this.btnReDispatch = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.DateStart = new DevExpress.XtraEditors.DateEdit();
            this.DateEnd = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            //this.bindingSource1.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.DispathLogData);
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
            this.colID,
            this.colOperationID,
            this.colOperationNo,
            this.colCreateBy,
            this.colCreateDate,
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
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            // 
            // colOperationID
            // 
            this.colOperationID.FieldName = "OperationID";
            this.colOperationID.Name = "colOperationID";
            this.colOperationID.Width = 101;
            // 
            // colOperationNo
            // 
            this.colOperationNo.Caption = "业务号";
            this.colOperationNo.FieldName = "OperationNo";
            this.colOperationNo.Name = "colOperationNo";
            this.colOperationNo.Visible = true;
            this.colOperationNo.VisibleIndex = 0;
            this.colOperationNo.Width = 173;
            // 
            // colCreateBy
            // 
            this.colCreateBy.FieldName = "CreateBy";
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.Width = 120;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "分发时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 1;
            this.colCreateDate.Width = 188;
            // 
            // colIsTransTo
            // 
            this.colIsTransTo.Caption = "是否分发到港后";
            this.colIsTransTo.FieldName = "IsTransTo";
            this.colIsTransTo.Name = "colIsTransTo";
            this.colIsTransTo.Visible = true;
            this.colIsTransTo.VisibleIndex = 2;
            this.colIsTransTo.Width = 124;
            // 
            // colState
            // 
            this.colState.Caption = "是否签收";
            this.colState.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colState.FieldName = "State";
            this.colState.Name = "colState";
            this.colState.Visible = true;
            this.colState.VisibleIndex = 3;
            this.colState.Width = 130;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = 2D;
            this.repositoryItemCheckEdit1.ValueUnchecked = 1D;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.DateEnd);
            this.panel1.Controls.Add(this.DateStart);
            this.panel1.Controls.Add(this.btnAll);
            this.panel1.Controls.Add(this.labTo);
            this.panel1.Controls.Add(this.labDateStrat);
            this.panel1.Controls.Add(this.ckbState);
            this.panel1.Controls.Add(this.ckbIsTransTo);
            this.panel1.Controls.Add(this.btnReDispatch);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(972, 42);
            this.panel1.TabIndex = 23;
            // 
            // btnAll
            // 
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAll.Location = new System.Drawing.Point(658, 11);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(75, 23);
            this.btnAll.TabIndex = 5;
            this.btnAll.Text = "&All";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
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
            this.labDateStrat.Location = new System.Drawing.Point(224, 15);
            this.labDateStrat.Name = "labDateStrat";
            this.labDateStrat.Size = new System.Drawing.Size(55, 14);
            this.labDateStrat.TabIndex = 7;
            this.labDateStrat.Text = "分发时间";
            // 
            // ckbState
            // 
            this.ckbState.AutoSize = true;
            this.ckbState.Location = new System.Drawing.Point(132, 14);
            this.ckbState.Name = "ckbState";
            this.ckbState.Size = new System.Drawing.Size(74, 18);
            this.ckbState.TabIndex = 1;
            this.ckbState.Text = "已经签收";
            this.ckbState.UseVisualStyleBackColor = true;
            // 
            // ckbIsTransTo
            // 
            this.ckbIsTransTo.AutoSize = true;
            this.ckbIsTransTo.Location = new System.Drawing.Point(23, 14);
            this.ckbIsTransTo.Name = "ckbIsTransTo";
            this.ckbIsTransTo.Size = new System.Drawing.Size(86, 18);
            this.ckbIsTransTo.TabIndex = 0;
            this.ckbIsTransTo.Text = "分发到港后";
            this.ckbIsTransTo.UseVisualStyleBackColor = true;
            // 
            // btnReDispatch
            // 
            this.btnReDispatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReDispatch.Location = new System.Drawing.Point(740, 11);
            this.btnReDispatch.Name = "btnReDispatch";
            this.btnReDispatch.Size = new System.Drawing.Size(75, 23);
            this.btnReDispatch.TabIndex = 6;
            this.btnReDispatch.Text = "&ReDispatch";
            this.btnReDispatch.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(576, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // DateStart
            // 
            this.DateStart.EditValue = new System.DateTime(((long)(0)));
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
            // DateEnd
            // 
            this.DateEnd.EditValue = new System.DateTime(((long)(0)));
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
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlueCompany)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcmbCompany;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit rlueCompany;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationID;
        private DevExpress.XtraGrid.Columns.GridColumn colOperationNo;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colIsTransTo;
        private DevExpress.XtraGrid.Columns.GridColumn colState;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private System.Windows.Forms.Panel panel1;
        protected DevExpress.XtraEditors.SimpleButton btnReDispatch;
        protected DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.Label labTo;
        private System.Windows.Forms.Label labDateStrat;
        private System.Windows.Forms.CheckBox ckbState;
        private System.Windows.Forms.CheckBox ckbIsTransTo;
        protected DevExpress.XtraEditors.SimpleButton btnAll;
        private DevExpress.XtraEditors.DateEdit DateEnd;
        private DevExpress.XtraEditors.DateEdit DateStart;
    }
}
