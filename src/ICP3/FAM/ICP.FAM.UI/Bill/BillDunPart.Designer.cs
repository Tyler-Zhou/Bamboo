namespace ICP.FAM.UI.Bill
{
    partial class BillDunPart
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
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.chkAmount = new DevExpress.XtraEditors.CheckEdit();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.cmbCurrency = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.listBox = new DevExpress.XtraEditors.ListBoxControl();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.cmbBank = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labBank = new DevExpress.XtraEditors.LabelControl();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labBillList = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barRemove = new DevExpress.XtraBars.BarButtonItem();
            this.barClear = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.chkAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(8, 8);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(24, 14);
            this.labType.TabIndex = 0;
            this.labType.Text = "类型";
            // 
            // chkAmount
            // 
            this.chkAmount.Location = new System.Drawing.Point(245, 6);
            this.chkAmount.Name = "chkAmount";
            this.chkAmount.Properties.Caption = "金额";
            this.chkAmount.Size = new System.Drawing.Size(61, 19);
            this.chkAmount.TabIndex = 1;
            this.chkAmount.CheckedChanged += new System.EventHandler(this.chkAmount_CheckedChanged);
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(319, 8);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(24, 14);
            this.labCurrency.TabIndex = 0;
            this.labCurrency.Text = "币种";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Enabled = false;
            this.cmbCurrency.Location = new System.Drawing.Point(361, 5);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCurrency.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(110, 21);
            this.cmbCurrency.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCurrency.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.listBox);
            this.pnlMain.Controls.Add(this.chkAmount);
            this.pnlMain.Controls.Add(this.cmbBank);
            this.pnlMain.Controls.Add(this.cmbCompany);
            this.pnlMain.Controls.Add(this.labBank);
            this.pnlMain.Controls.Add(this.cmbType);
            this.pnlMain.Controls.Add(this.labBillList);
            this.pnlMain.Controls.Add(this.labCompany);
            this.pnlMain.Controls.Add(this.cmbCurrency);
            this.pnlMain.Controls.Add(this.labType);
            this.pnlMain.Controls.Add(this.labCurrency);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(542, 152);
            this.pnlMain.TabIndex = 3;
            // 
            // listBox
            // 
            this.listBox.DataSource = this.bsList;
            this.listBox.Location = new System.Drawing.Point(66, 54);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(405, 93);
            this.listBox.TabIndex = 5;
            // 
            // cmbBank
            // 
            this.cmbBank.Location = new System.Drawing.Point(319, 29);
            this.cmbBank.Name = "cmbBank";
            this.cmbBank.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbBank.Properties.Appearance.Options.UseBackColor = true;
            this.cmbBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBank.Size = new System.Drawing.Size(152, 21);
            this.cmbBank.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbBank.TabIndex = 4;
            // 
            // cmbCompany
            // 
            this.cmbCompany.Location = new System.Drawing.Point(66, 29);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(127, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 3;
            this.cmbCompany.SelectedIndexChanged += new System.EventHandler(this.cmbCompany_SelectedIndexChanged);
            // 
            // labBank
            // 
            this.labBank.Location = new System.Drawing.Point(247, 32);
            this.labBank.Name = "labBank";
            this.labBank.Size = new System.Drawing.Size(48, 14);
            this.labBank.TabIndex = 3;
            this.labBank.Text = "银行信息";
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(66, 5);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(127, 21);
            this.cmbType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbType.TabIndex = 0;
            // 
            // labBillList
            // 
            this.labBillList.Location = new System.Drawing.Point(8, 58);
            this.labBillList.Name = "labBillList";
            this.labBillList.Size = new System.Drawing.Size(48, 14);
            this.labBillList.TabIndex = 0;
            this.labBillList.Text = "账单列表";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(8, 32);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 0;
            this.labCompany.Text = "公司";
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
            this.barPrint,
            this.barClear,
            this.barRemove});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barRemove, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barClear, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印预览(&P)";
            this.barPrint.Glyph = global::ICP.FAM.UI.Properties.Resources.Preview;
            this.barPrint.Id = 0;
            this.barPrint.Name = "barPrint";
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barRemove
            // 
            this.barRemove.Caption = "移除";
            this.barRemove.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.barRemove.Id = 2;
            this.barRemove.Name = "barRemove";
            this.barRemove.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barRemove_ItemClick);
            // 
            // barClear
            // 
            this.barClear.Caption = "清除";
            this.barClear.Glyph = global::ICP.FAM.UI.Properties.Resources.Empty;
            this.barClear.Id = 1;
            this.barClear.Name = "barClear";
            this.barClear.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClear_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(542, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 178);
            this.barDockControlBottom.Size = new System.Drawing.Size(542, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 152);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(542, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 152);
            // 
            // BillDunPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "BillDunPart";
            this.Size = new System.Drawing.Size(542, 178);
            ((System.ComponentModel.ISupportInitialize)(this.chkAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.CheckEdit chkAmount;
        private DevExpress.XtraEditors.LabelControl labCurrency;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barClear;
        private DevExpress.XtraBars.BarButtonItem barRemove;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbBank;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labBank;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraEditors.ListBoxControl listBox;
        private DevExpress.XtraEditors.LabelControl labBillList;
        private System.Windows.Forms.BindingSource bsList;
    }
}
