namespace ICP.FCM.OceanExport.UI.Order
{
    partial class AgentFilerListPart
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
            this.bsAgentFilerList = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnsearch = new DevExpress.XtraEditors.SimpleButton();
            this.txtConsignee = new DevExpress.XtraEditors.TextEdit();
            this.labConsignee = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcMain = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCustomerEname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerCname = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomerAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAgentFilerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEmail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTel = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcmbState = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsAgentFilerList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsignee.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).BeginInit();
            this.SuspendLayout();
            // 
            // bsAgentFilerList
            // 
            this.bsAgentFilerList.DataSource = typeof(ICP.FCM.OceanExport.UI.Order.AgentFilerListPart);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnsearch);
            this.panelControl1.Controls.Add(this.txtConsignee);
            this.panelControl1.Controls.Add(this.labConsignee);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(829, 35);
            this.panelControl1.TabIndex = 2;
            // 
            // btnsearch
            // 
            this.btnsearch.Location = new System.Drawing.Point(417, 6);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(75, 23);
            this.btnsearch.TabIndex = 2;
            this.btnsearch.Text = "查询";
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // txtConsignee
            // 
            this.txtConsignee.Location = new System.Drawing.Point(92, 8);
            this.txtConsignee.Name = "txtConsignee";
            this.txtConsignee.Size = new System.Drawing.Size(293, 21);
            this.txtConsignee.TabIndex = 1;
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(34, 11);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(36, 14);
            this.labConsignee.TabIndex = 0;
            this.labConsignee.Text = "收货人";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gcMain);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 35);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(829, 461);
            this.panelControl2.TabIndex = 3;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsAgentFilerList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 2);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rcmbState});
            this.gcMain.Size = new System.Drawing.Size(825, 457);
            this.gcMain.TabIndex = 2;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // gvMain
            // 
            this.gvMain.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCustomerEname,
            this.colCustomerCname,
            this.colCustomerAddress,
            this.colAgentFilerName,
            this.colEmail,
            this.colTel});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 35;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ColumnAutoWidth = false;
            this.gvMain.OptionsView.EnableAppearanceEvenRow = true;
            this.gvMain.OptionsView.ShowDetailButtons = false;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colCustomerEname
            // 
            this.colCustomerEname.Caption = "Customer Ename";
            this.colCustomerEname.FieldName = "CustomerEname";
            this.colCustomerEname.Name = "colCustomerEname";
            this.colCustomerEname.Visible = true;
            this.colCustomerEname.VisibleIndex = 0;
            this.colCustomerEname.Width = 150;
            // 
            // colCustomerCname
            // 
            this.colCustomerCname.Caption = "Customer Cname";
            this.colCustomerCname.FieldName = "CustomerCname";
            this.colCustomerCname.Name = "colCustomerCname";
            this.colCustomerCname.Visible = true;
            this.colCustomerCname.VisibleIndex = 1;
            this.colCustomerCname.Width = 150;
            // 
            // colCustomerAddress
            // 
            this.colCustomerAddress.Caption = "Address";
            this.colCustomerAddress.FieldName = "CustomerAddress";
            this.colCustomerAddress.Name = "colCustomerAddress";
            this.colCustomerAddress.Visible = true;
            this.colCustomerAddress.VisibleIndex = 2;
            this.colCustomerAddress.Width = 150;
            // 
            // colAgentFilerName
            // 
            this.colAgentFilerName.Caption = "AgentFilerName";
            this.colAgentFilerName.FieldName = "AgentFilerName";
            this.colAgentFilerName.Name = "colAgentFilerName";
            this.colAgentFilerName.Visible = true;
            this.colAgentFilerName.VisibleIndex = 3;
            this.colAgentFilerName.Width = 114;
            // 
            // colEmail
            // 
            this.colEmail.Caption = "Email";
            this.colEmail.FieldName = "Email";
            this.colEmail.Name = "colEmail";
            this.colEmail.Visible = true;
            this.colEmail.VisibleIndex = 4;
            this.colEmail.Width = 138;
            // 
            // colTel
            // 
            this.colTel.Caption = "Tel";
            this.colTel.FieldName = "Tel";
            this.colTel.Name = "colTel";
            this.colTel.Visible = true;
            this.colTel.VisibleIndex = 5;
            this.colTel.Width = 148;
            // 
            // rcmbState
            // 
            this.rcmbState.AutoHeight = false;
            this.rcmbState.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rcmbState.Name = "rcmbState";
            // 
            // AgentFilerListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "AgentFilerListPart";
            this.Size = new System.Drawing.Size(829, 496);
            ((System.ComponentModel.ISupportInitialize)(this.bsAgentFilerList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConsignee.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcmbState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsAgentFilerList;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        protected ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        protected DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerEname;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerCname;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomerAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colAgentFilerName;
        private DevExpress.XtraGrid.Columns.GridColumn colEmail;
        private DevExpress.XtraGrid.Columns.GridColumn colTel;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox rcmbState;
        private DevExpress.XtraEditors.TextEdit txtConsignee;
        private DevExpress.XtraEditors.LabelControl labConsignee;
        private DevExpress.XtraEditors.SimpleButton btnsearch;
    }
}
