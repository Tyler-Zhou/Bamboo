namespace ICP.Sys.UI.SystemHelp
{
    partial class SystemErrorMemoPart
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
            this.pnlSystemErrorMemo = new DevExpress.XtraEditors.PanelControl();
            this.gridErrorLogList = new DevExpress.XtraGrid.GridControl();
            this.bsErrorLogs = new System.Windows.Forms.BindingSource(this.components);
            this.gvErrorLogList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPreview = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemHyperLinkEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxt_100 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.colCreateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            this.btnBottom = new DevExpress.XtraEditors.SimpleButton();
            this.btnTop = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.dateFrom = new DevExpress.XtraEditors.DateEdit();
            this.lblFrom = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSystemErrorMemo)).BeginInit();
            this.pnlSystemErrorMemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridErrorLogList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsErrorLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvErrorLogList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_100)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSystemErrorMemo
            // 
            this.pnlSystemErrorMemo.Controls.Add(this.gridErrorLogList);
            this.pnlSystemErrorMemo.Controls.Add(this.panelControl1);
            this.pnlSystemErrorMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSystemErrorMemo.Location = new System.Drawing.Point(0, 0);
            this.pnlSystemErrorMemo.Name = "pnlSystemErrorMemo";
            this.pnlSystemErrorMemo.Size = new System.Drawing.Size(941, 653);
            this.pnlSystemErrorMemo.TabIndex = 0;
            // 
            // gridErrorLogList
            // 
            this.gridErrorLogList.DataSource = this.bsErrorLogs;
            this.gridErrorLogList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridErrorLogList.Location = new System.Drawing.Point(2, 36);
            this.gridErrorLogList.MainView = this.gvErrorLogList;
            this.gridErrorLogList.Name = "gridErrorLogList";
            this.gridErrorLogList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemHyperLinkEdit1,
            this.rtxt_100});
            this.gridErrorLogList.Size = new System.Drawing.Size(937, 615);
            this.gridErrorLogList.TabIndex = 1;
            this.gridErrorLogList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvErrorLogList});
            this.gridErrorLogList.DoubleClick += new System.EventHandler(this.gridErrorLogList_DoubleClick);
            // 
            // bsErrorLogs
            // 
            this.bsErrorLogs.DataSource = typeof(ICP.Sys.ServiceInterface.SystemErrorLogObject);
            // 
            // gvErrorLogList
            // 
            this.gvErrorLogList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPreview,
            this.colUserName,
            this.colDescription,
            this.colCreateTime});
            this.gvErrorLogList.GridControl = this.gridErrorLogList;
            this.gvErrorLogList.Name = "gvErrorLogList";
            this.gvErrorLogList.OptionsCustomization.AllowQuickHideColumns = false;
            this.gvErrorLogList.OptionsSelection.MultiSelect = true;
            this.gvErrorLogList.OptionsView.ShowGroupPanel = false;
            this.gvErrorLogList.OptionsView.ShowIndicator = false;
            this.gvErrorLogList.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCreateTime, DevExpress.Data.ColumnSortOrder.Descending)});
            this.gvErrorLogList.Click += new System.EventHandler(this.gvErrorLogList_Click);
            // 
            // colPreview
            // 
            this.colPreview.Caption = "Preview";
            this.colPreview.ColumnEdit = this.repositoryItemHyperLinkEdit1;
            this.colPreview.FieldName = "Preview";
            this.colPreview.Name = "colPreview";
            this.colPreview.OptionsColumn.AllowEdit = false;
            this.colPreview.Visible = true;
            this.colPreview.VisibleIndex = 0;
            this.colPreview.Width = 58;
            // 
            // repositoryItemHyperLinkEdit1
            // 
            this.repositoryItemHyperLinkEdit1.AutoHeight = false;
            this.repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            // 
            // colUserName
            // 
            this.colUserName.Caption = "UserName";
            this.colUserName.FieldName = "UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.OptionsColumn.AllowEdit = false;
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 1;
            this.colUserName.Width = 81;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Detail";
            this.colDescription.ColumnEdit = this.rtxt_100;
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 695;
            // 
            // rtxt_100
            // 
            this.rtxt_100.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rtxt_100.Name = "rtxt_100";
            this.rtxt_100.ShowIcon = false;
            // 
            // colCreateTime
            // 
            this.colCreateTime.Caption = "CreateTime";
            this.colCreateTime.FieldName = "CreateTime";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.OptionsColumn.AllowEdit = false;
            this.colCreateTime.Visible = true;
            this.colCreateTime.VisibleIndex = 3;
            this.colCreateTime.Width = 99;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtUserName);
            this.panelControl1.Controls.Add(this.btnBottom);
            this.panelControl1.Controls.Add(this.btnTop);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.dateTo);
            this.panelControl1.Controls.Add(this.lblTo);
            this.panelControl1.Controls.Add(this.dateFrom);
            this.panelControl1.Controls.Add(this.lblFrom);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(937, 34);
            this.panelControl1.TabIndex = 0;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(447, 6);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.NullText = " -- UserName -- ";
            this.txtUserName.Size = new System.Drawing.Size(116, 21);
            this.txtUserName.TabIndex = 7;
            // 
            // btnBottom
            // 
            this.btnBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBottom.Location = new System.Drawing.Point(827, 5);
            this.btnBottom.Name = "btnBottom";
            this.btnBottom.Size = new System.Drawing.Size(75, 23);
            this.btnBottom.TabIndex = 6;
            this.btnBottom.Text = "Bottom";
            this.btnBottom.Click += new System.EventHandler(this.Bottom_Click);
            // 
            // btnTop
            // 
            this.btnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTop.Location = new System.Drawing.Point(740, 5);
            this.btnTop.Name = "btnTop";
            this.btnTop.Size = new System.Drawing.Size(75, 23);
            this.btnTop.TabIndex = 5;
            this.btnTop.Text = "Top";
            this.btnTop.Click += new System.EventHandler(this.btnTop_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(579, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dateTo
            // 
            this.dateTo.EditValue = new System.DateTime(2013, 8, 19, 0, 0, 0, 0);
            this.dateTo.Location = new System.Drawing.Point(254, 6);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTo.Size = new System.Drawing.Size(176, 21);
            this.dateTo.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(228, 9);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(15, 14);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "To";
            // 
            // dateFrom
            // 
            this.dateFrom.EditValue = new System.DateTime(2013, 8, 19, 0, 0, 0, 0);
            this.dateFrom.Location = new System.Drawing.Point(47, 6);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateFrom.Size = new System.Drawing.Size(165, 21);
            this.dateFrom.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.Location = new System.Drawing.Point(8, 9);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(27, 14);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "From";
            // 
            // SystemErrorMemoPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlSystemErrorMemo);
            this.Name = "SystemErrorMemoPart";
            this.Size = new System.Drawing.Size(941, 653);
            ((System.ComponentModel.ISupportInitialize)(this.pnlSystemErrorMemo)).EndInit();
            this.pnlSystemErrorMemo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridErrorLogList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsErrorLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvErrorLogList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemHyperLinkEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxt_100)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlSystemErrorMemo;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lblFrom;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.DateEdit dateFrom;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraGrid.GridControl gridErrorLogList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvErrorLogList;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private System.Windows.Forms.BindingSource bsErrorLogs;
        private DevExpress.XtraGrid.Columns.GridColumn colPreview;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1;
        private DevExpress.XtraEditors.SimpleButton btnBottom;
        private DevExpress.XtraEditors.SimpleButton btnTop;
        private DevExpress.XtraEditors.TextEdit txtUserName;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rtxt_100;
    }
}
