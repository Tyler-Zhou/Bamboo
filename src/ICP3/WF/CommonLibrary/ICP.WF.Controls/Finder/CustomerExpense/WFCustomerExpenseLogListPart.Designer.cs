namespace ICP.WF.Controls.Form.CustomerExpense
{
    partial class WFCustomerExpenseLogListPart
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
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new ICP.Framework.ClientComponents.Controls.LWGridView();
            this.colWorkFlowNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWorkName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLog = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLog)).BeginInit();
            this.gcLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcMain.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcMain.Location = new System.Drawing.Point(2, 23);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.Size = new System.Drawing.Size(710, 308);
            this.gcMain.TabIndex = 1;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.WF.ServiceInterface.WFCustomerExpenseLogList);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWorkFlowNo,
            this.colWorkName,
            this.colCreateName,
            this.colCreateDate});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colWorkFlowNo
            // 
            this.colWorkFlowNo.Caption = "流程单号";
            this.colWorkFlowNo.FieldName = "WorkNo";
            this.colWorkFlowNo.Name = "colWorkFlowNo";
            this.colWorkFlowNo.OptionsColumn.AllowEdit = false;
            this.colWorkFlowNo.Visible = true;
            this.colWorkFlowNo.VisibleIndex = 0;
            this.colWorkFlowNo.Width = 110;
            // 
            // colWorkName
            // 
            this.colWorkName.Caption = "工作名称";
            this.colWorkName.FieldName = "WorkName";
            this.colWorkName.Name = "colWorkName";
            this.colWorkName.Visible = true;
            this.colWorkName.VisibleIndex = 1;
            this.colWorkName.Width = 120;
            // 
            // colCreateName
            // 
            this.colCreateName.Caption = "申请人";
            this.colCreateName.FieldName = "CreateBy";
            this.colCreateName.Name = "colCreateName";
            this.colCreateName.OptionsColumn.AllowEdit = false;
            this.colCreateName.Visible = true;
            this.colCreateName.VisibleIndex = 2;
            this.colCreateName.Width = 100;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "申请时间";
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 3;
            this.colCreateDate.Width = 150;
            // 
            // gcLog
            // 
            this.gcLog.Controls.Add(this.gcMain);
            this.gcLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLog.Location = new System.Drawing.Point(0, 0);
            this.gcLog.Name = "gcLog";
            this.gcLog.Size = new System.Drawing.Size(714, 333);
            this.gcLog.TabIndex = 2;
            this.gcLog.Text = "流程日志";
            // 
            // WFCommissionLogListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcLog);
            this.Name = "WFCommissionLogListPart";
            this.Size = new System.Drawing.Size(714, 333);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLog)).EndInit();
            this.gcLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private ICP.Framework.ClientComponents.Controls.LWGridView gvMain;
        protected System.Windows.Forms.BindingSource bsList;

        private DevExpress.XtraGrid.Columns.GridColumn colWorkFlowNo;
        private DevExpress.XtraGrid.Columns.GridColumn colWorkName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraEditors.GroupControl gcLog;


    }
}
