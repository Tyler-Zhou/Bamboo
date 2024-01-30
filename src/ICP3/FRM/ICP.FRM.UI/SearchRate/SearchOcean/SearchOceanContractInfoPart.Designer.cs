namespace ICP.FRM.UI.SearchRate
{
    partial class SearchOceanContractInfoPart
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
            this.labCarrie = new DevExpress.XtraEditors.LabelControl();
            this.labContractNo = new DevExpress.XtraEditors.LabelControl();
            this.labShipline = new DevExpress.XtraEditors.LabelControl();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.gbSCN = new DevExpress.XtraEditors.GroupControl();
            this.gcSCN = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.bsSCNList = new System.Windows.Forms.BindingSource(this.components);
            this.gvSCN = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNotifyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.txtAccountType = new DevExpress.XtraEditors.TextEdit();
            this.txtShipline = new DevExpress.XtraEditors.TextEdit();
            this.txtContractNo = new DevExpress.XtraEditors.TextEdit();
            this.txtCarrie = new DevExpress.XtraEditors.TextEdit();
            this.labAccount = new DevExpress.XtraEditors.LabelControl();
            this.labAccountType = new DevExpress.XtraEditors.LabelControl();
            this.txtAccount = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbSCN)).BeginInit();
            this.gbSCN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSCNList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSCN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipline.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrie.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labCarrie
            // 
            this.labCarrie.Location = new System.Drawing.Point(12, 12);
            this.labCarrie.Name = "labCarrie";
            this.labCarrie.Size = new System.Drawing.Size(30, 14);
            this.labCarrie.TabIndex = 0;
            this.labCarrie.Text = "Carrie";
            // 
            // labContractNo
            // 
            this.labContractNo.Location = new System.Drawing.Point(12, 39);
            this.labContractNo.Name = "labContractNo";
            this.labContractNo.Size = new System.Drawing.Size(66, 14);
            this.labContractNo.TabIndex = 0;
            this.labContractNo.Text = "Contract No";
            // 
            // labShipline
            // 
            this.labShipline.Location = new System.Drawing.Point(12, 69);
            this.labShipline.Name = "labShipline";
            this.labShipline.Size = new System.Drawing.Size(41, 14);
            this.labShipline.TabIndex = 0;
            this.labShipline.Text = "Shipline";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtAccount);
            this.pnlMain.Controls.Add(this.gbSCN);
            this.pnlMain.Controls.Add(this.txtAccountType);
            this.pnlMain.Controls.Add(this.txtShipline);
            this.pnlMain.Controls.Add(this.txtContractNo);
            this.pnlMain.Controls.Add(this.txtCarrie);
            this.pnlMain.Controls.Add(this.labContractNo);
            this.pnlMain.Controls.Add(this.labAccount);
            this.pnlMain.Controls.Add(this.labAccountType);
            this.pnlMain.Controls.Add(this.labShipline);
            this.pnlMain.Controls.Add(this.labCarrie);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(760, 229);
            this.pnlMain.TabIndex = 1;
            // 
            // gbSCN
            // 
            this.gbSCN.Controls.Add(this.gcSCN);
            this.gbSCN.Location = new System.Drawing.Point(316, 5);
            this.gbSCN.Name = "gbSCN";
            this.gbSCN.Size = new System.Drawing.Size(439, 119);
            this.gbSCN.TabIndex = 13;
            this.gbSCN.Text = "Shipper/Cosignee/Notify";
            // 
            // gcSCN
            // 
            this.gcSCN.DataSource = this.bsSCNList;
            this.gcSCN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSCN.Location = new System.Drawing.Point(2, 23);
            this.gcSCN.MainView = this.gvSCN;
            this.gcSCN.Name = "gcSCN";
            this.gcSCN.Size = new System.Drawing.Size(435, 94);
            this.gcSCN.TabIndex = 0;
            this.gcSCN.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSCN});
            // 
            // bsSCNList
            // 
            this.bsSCNList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.SCNInfo);
            // 
            // gvSCN
            // 
            this.gvSCN.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colShipperName,
            this.colConsigneeName,
            this.colNotifyName});
            this.gvSCN.GridControl = this.gcSCN;
            this.gvSCN.Name = "gvSCN";
            this.gvSCN.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gvSCN.OptionsBehavior.Editable = false;
            this.gvSCN.OptionsSelection.MultiSelect = true;
            this.gvSCN.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvSCN.OptionsView.EnableAppearanceEvenRow = true;
            this.gvSCN.OptionsView.ShowGroupPanel = false;
            // 
            // colShipperName
            // 
            this.colShipperName.Caption = "Shipper";
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 0;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.Caption = "Consignee";
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 1;
            // 
            // colNotifyName
            // 
            this.colNotifyName.Caption = "Notify";
            this.colNotifyName.FieldName = "NotifyName";
            this.colNotifyName.Name = "colNotifyName";
            this.colNotifyName.Visible = true;
            this.colNotifyName.VisibleIndex = 2;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.SearchOceanContractInfo);
            // 
            // txtAccountType
            // 
            this.txtAccountType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "AccountType", true));
            this.txtAccountType.Location = new System.Drawing.Point(110, 96);
            this.txtAccountType.Name = "txtAccountType";
            this.txtAccountType.Properties.ReadOnly = true;
            this.txtAccountType.Size = new System.Drawing.Size(188, 21);
            this.txtAccountType.TabIndex = 3;
            // 
            // txtShipline
            // 
            this.txtShipline.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "ShiplineName", true));
            this.txtShipline.Location = new System.Drawing.Point(110, 66);
            this.txtShipline.Name = "txtShipline";
            this.txtShipline.Properties.ReadOnly = true;
            this.txtShipline.Size = new System.Drawing.Size(188, 21);
            this.txtShipline.TabIndex = 2;
            // 
            // txtContractNo
            // 
            this.txtContractNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "ContractNo", true));
            this.txtContractNo.Location = new System.Drawing.Point(110, 36);
            this.txtContractNo.Name = "txtContractNo";
            this.txtContractNo.Properties.ReadOnly = true;
            this.txtContractNo.Size = new System.Drawing.Size(188, 21);
            this.txtContractNo.TabIndex = 1;
            // 
            // txtCarrie
            // 
            this.txtCarrie.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Carrier", true));
            this.txtCarrie.Location = new System.Drawing.Point(110, 9);
            this.txtCarrie.Name = "txtCarrie";
            this.txtCarrie.Properties.ReadOnly = true;
            this.txtCarrie.Size = new System.Drawing.Size(188, 21);
            this.txtCarrie.TabIndex = 0;
            // 
            // labAccount
            // 
            this.labAccount.Location = new System.Drawing.Point(12, 129);
            this.labAccount.Name = "labAccount";
            this.labAccount.Size = new System.Drawing.Size(46, 14);
            this.labAccount.TabIndex = 0;
            this.labAccount.Text = "Account";
            // 
            // labAccountType
            // 
            this.labAccountType.Location = new System.Drawing.Point(12, 99);
            this.labAccountType.Name = "labAccountType";
            this.labAccountType.Size = new System.Drawing.Size(78, 14);
            this.labAccountType.TabIndex = 0;
            this.labAccountType.Text = "Account Type";
            // 
            // txtAccount
            // 
            this.txtAccount.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Account", true));
            this.txtAccount.Location = new System.Drawing.Point(110, 126);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Properties.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(643, 96);
            this.txtAccount.TabIndex = 4;
            // 
            // SearchOceanContractInfoPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.IsMultiLanguage = false;
            this.Name = "SearchOceanContractInfoPart";
            this.Size = new System.Drawing.Size(760, 229);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbSCN)).EndInit();
            this.gbSCN.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSCNList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSCN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccountType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipline.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContractNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCarrie.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAccount.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labCarrie;
        private DevExpress.XtraEditors.LabelControl labContractNo;
        private DevExpress.XtraEditors.LabelControl labShipline;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.LabelControl labAccountType;
        private DevExpress.XtraEditors.LabelControl labAccount;
        private DevExpress.XtraEditors.TextEdit txtShipline;
        private DevExpress.XtraEditors.TextEdit txtContractNo;
        private DevExpress.XtraEditors.TextEdit txtCarrie;
        private DevExpress.XtraEditors.TextEdit txtAccountType;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcSCN;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSCN;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colNotifyName;
        private DevExpress.XtraEditors.GroupControl gbSCN;
        private System.Windows.Forms.BindingSource bsList;
        private System.Windows.Forms.BindingSource bsSCNList;
        private DevExpress.XtraEditors.MemoEdit txtAccount;
    }
}
