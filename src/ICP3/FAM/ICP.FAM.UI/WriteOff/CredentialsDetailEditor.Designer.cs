namespace ICP.FAM.UI.WriteOff
{
    partial class CredentialsDetailEditor
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
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrgDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrgCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCustomer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cmbGL = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.labTip = new System.Windows.Forms.Label();
            this.labTotal = new DevExpress.XtraEditors.LabelControl();
            this.txtCredit = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.bAutoGen = new DevExpress.XtraEditors.SimpleButton();
            this.txtDebit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.ckbLock = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDebit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbLock.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsList;
            this.gcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMain.Location = new System.Drawing.Point(2, 2);
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbGL,
            this.repositoryItemButtonEdit1});
            this.gcMain.Size = new System.Drawing.Size(840, 259);
            this.gcMain.TabIndex = 0;
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain});
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CredentialsDetailList);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRemark,
            this.colGL,
            this.colOrgDebit,
            this.colOrgCredit,
            this.colRate,
            this.colDebit,
            this.colCredit,
            this.colCustomer});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.IndicatorWidth = 30;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // colRemark
            // 
            this.colRemark.Caption = "Remark";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.OptionsColumn.AllowEdit = false;
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 0;
            this.colRemark.Width = 121;
            // 
            // colGL
            // 
            this.colGL.Caption = "GL";
            this.colGL.FieldName = "GL";
            this.colGL.Name = "colGL";
            this.colGL.OptionsColumn.AllowEdit = false;
            this.colGL.Visible = true;
            this.colGL.VisibleIndex = 1;
            this.colGL.Width = 152;
            // 
            // colOrgDebit
            // 
            this.colOrgDebit.Caption = "Org.Debit";
            this.colOrgDebit.DisplayFormat.FormatString = "n";
            this.colOrgDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOrgDebit.FieldName = "OrgDebit";
            this.colOrgDebit.Name = "colOrgDebit";
            this.colOrgDebit.OptionsColumn.AllowEdit = false;
            this.colOrgDebit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colOrgDebit.Visible = true;
            this.colOrgDebit.VisibleIndex = 2;
            this.colOrgDebit.Width = 80;
            // 
            // colOrgCredit
            // 
            this.colOrgCredit.Caption = "Org.Credit";
            this.colOrgCredit.DisplayFormat.FormatString = "n";
            this.colOrgCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOrgCredit.FieldName = "OrgCredit";
            this.colOrgCredit.Name = "colOrgCredit";
            this.colOrgCredit.OptionsColumn.AllowEdit = false;
            this.colOrgCredit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colOrgCredit.Visible = true;
            this.colOrgCredit.VisibleIndex = 3;
            this.colOrgCredit.Width = 80;
            // 
            // colRate
            // 
            this.colRate.Caption = "Rate";
            this.colRate.DisplayFormat.FormatString = "n";
            this.colRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.OptionsColumn.AllowEdit = false;
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 4;
            this.colRate.Width = 59;
            // 
            // colDebit
            // 
            this.colDebit.Caption = "Debit";
            this.colDebit.DisplayFormat.FormatString = "n";
            this.colDebit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDebit.FieldName = "Debit";
            this.colDebit.Name = "colDebit";
            this.colDebit.OptionsColumn.AllowEdit = false;
            this.colDebit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colDebit.Visible = true;
            this.colDebit.VisibleIndex = 5;
            this.colDebit.Width = 85;
            // 
            // colCredit
            // 
            this.colCredit.Caption = "Credit";
            this.colCredit.DisplayFormat.FormatString = "n";
            this.colCredit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCredit.FieldName = "Credit";
            this.colCredit.Name = "colCredit";
            this.colCredit.OptionsColumn.AllowEdit = false;
            this.colCredit.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            this.colCredit.Visible = true;
            this.colCredit.VisibleIndex = 6;
            this.colCredit.Width = 85;
            // 
            // colCustomer
            // 
            this.colCustomer.Caption = "Customer";
            this.colCustomer.FieldName = "CustomerName";
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.OptionsColumn.AllowEdit = false;
            this.colCustomer.Visible = true;
            this.colCustomer.VisibleIndex = 7;
            this.colCustomer.Width = 165;
            // 
            // cmbGL
            // 
            this.cmbGL.AutoHeight = false;
            this.cmbGL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGL.Name = "cmbGL";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.gcMain);
            this.pnlMain.Controls.Add(this.pnlBottom);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(844, 347);
            this.pnlMain.TabIndex = 67;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.labTip);
            this.pnlBottom.Controls.Add(this.labTotal);
            this.pnlBottom.Controls.Add(this.txtCredit);
            this.pnlBottom.Controls.Add(this.labelControl2);
            this.pnlBottom.Controls.Add(this.bAutoGen);
            this.pnlBottom.Controls.Add(this.txtDebit);
            this.pnlBottom.Controls.Add(this.labelControl1);
            this.pnlBottom.Controls.Add(this.ckbLock);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(2, 261);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(840, 84);
            this.pnlBottom.TabIndex = 1;
            // 
            // labTip
            // 
            this.labTip.AutoSize = true;
            this.labTip.ForeColor = System.Drawing.Color.Red;
            this.labTip.Location = new System.Drawing.Point(157, 56);
            this.labTip.Name = "labTip";
            this.labTip.Size = new System.Drawing.Size(0, 14);
            this.labTip.TabIndex = 70;
            // 
            // labTotal
            // 
            this.labTotal.Location = new System.Drawing.Point(422, 20);
            this.labTotal.Name = "labTotal";
            this.labTotal.Size = new System.Drawing.Size(70, 14);
            this.labTotal.TabIndex = 69;
            this.labTotal.Text = "labelControl3";
            // 
            // txtCredit
            // 
            this.txtCredit.Location = new System.Drawing.Point(684, 17);
            this.txtCredit.MenuManager = this.barManager1;
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.Properties.ReadOnly = true;
            this.txtCredit.Size = new System.Drawing.Size(115, 21);
            this.txtCredit.TabIndex = 3;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barAdd,
            this.barDelete,
            this.barClose,
            this.barSave});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 4;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClose, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭(&C)";
            this.barClose.Glyph = global::ICP.FAM.UI.Properties.Resources.Close_16;
            this.barClose.Id = 2;
            this.barClose.Name = "barClose";
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(844, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 373);
            this.barDockControlBottom.Size = new System.Drawing.Size(844, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 347);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(844, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 347);
            // 
            // barAdd
            // 
            this.barAdd.Caption = "新增";
            this.barAdd.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_16;
            this.barAdd.Id = 0;
            this.barAdd.Name = "barAdd";
            // 
            // barDelete
            // 
            this.barDelete.Caption = "删除(&D)";
            this.barDelete.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barDelete.Id = 1;
            this.barDelete.Name = "barDelete";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_16;
            this.barSave.Id = 3;
            this.barSave.Name = "barSave";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(646, 20);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(32, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Credit";
            // 
            // bAutoGen
            // 
            this.bAutoGen.Location = new System.Drawing.Point(5, 15);
            this.bAutoGen.Name = "bAutoGen";
            this.bAutoGen.Size = new System.Drawing.Size(97, 23);
            this.bAutoGen.TabIndex = 68;
            this.bAutoGen.Text = "自动生成凭证";
            this.bAutoGen.Click += new System.EventHandler(this.bAutoGen_Click);
            // 
            // txtDebit
            // 
            this.txtDebit.Location = new System.Drawing.Point(514, 17);
            this.txtDebit.MenuManager = this.barManager1;
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.Properties.ReadOnly = true;
            this.txtDebit.Size = new System.Drawing.Size(115, 21);
            this.txtDebit.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(479, 20);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Debit";
            // 
            // ckbLock
            // 
            this.ckbLock.Location = new System.Drawing.Point(5, 53);
            this.ckbLock.MenuManager = this.barManager1;
            this.ckbLock.Name = "ckbLock";
            this.ckbLock.Properties.Caption = "Lock Accounts";
            this.ckbLock.Size = new System.Drawing.Size(109, 19);
            this.ckbLock.TabIndex = 67;
            // 
            // CredentialsDetailEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 373);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "CredentialsDetailEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDebit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckbLock.Properties)).EndInit();
            this.ResumeLayout(false);

        }
    
        #endregion

        private ICP.Framework.ClientComponents.Controls.LWGridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn colGL;
        private DevExpress.XtraGrid.Columns.GridColumn colOrgDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraGrid.Columns.GridColumn colDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colCredit;
        private DevExpress.XtraGrid.Columns.GridColumn colCustomer;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barDelete;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraGrid.Columns.GridColumn colOrgCredit;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.CheckEdit ckbLock;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbGL;
        private DevExpress.XtraEditors.SimpleButton bAutoGen;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.TextEdit txtCredit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDebit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labTotal;
        private System.Windows.Forms.Label labTip;
    }
}
