namespace ICP.Sys.UI.Organization
{
    partial class OrganizationEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrganizationEditPart));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labParent = new DevExpress.XtraEditors.LabelControl();
            this.bsParent = new System.Windows.Forms.BindingSource(this.components);
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.panelScroll = new System.Windows.Forms.Panel();
            this.popControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.treeParent = new ICP.Framework.ClientComponents.Controls.LWTreeGridControl();
            this.colCShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colEShortName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnClearPop = new DevExpress.XtraEditors.SimpleButton();
            this.cmbType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.popParent = new DevExpress.XtraEditors.PopupContainerEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.panelScroll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).BeginInit();
            this.popControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeParent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popParent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.OrganizationInfo);
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(9, 56);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(38, 14);
            this.labEName.TabIndex = 13;
            this.labEName.Text = "EName";
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(9, 79);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(28, 14);
            this.labType.TabIndex = 14;
            this.labType.Text = "Type";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(9, 10);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 0;
            this.labCode.Text = "Code";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(9, 33);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(38, 14);
            this.labCName.TabIndex = 12;
            this.labCName.Text = "CName";
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CShortName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCName.Location = new System.Drawing.Point(74, 30);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 50;
            this.txtCName.Size = new System.Drawing.Size(275, 21);
            this.txtCName.TabIndex = 1;
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EShortName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEName.Location = new System.Drawing.Point(74, 53);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 50;
            this.txtEName.Size = new System.Drawing.Size(275, 21);
            this.txtEName.TabIndex = 2;
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Code", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtCode, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCode.Location = new System.Drawing.Point(74, 7);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 20;
            this.txtCode.Size = new System.Drawing.Size(275, 21);
            this.txtCode.TabIndex = 0;
            // 
            // labParent
            // 
            this.labParent.Location = new System.Drawing.Point(9, 102);
            this.labParent.Name = "labParent";
            this.labParent.Size = new System.Drawing.Size(36, 14);
            this.labParent.TabIndex = 5;
            this.labParent.Text = "Parent";
            // 
            // bsParent
            // 
            this.bsParent.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.OrganizationList);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // panelScroll
            // 
            this.panelScroll.AutoScroll = true;
            this.panelScroll.BackColor = System.Drawing.Color.Transparent;
            this.panelScroll.Controls.Add(this.popControl1);
            this.panelScroll.Controls.Add(this.labCode);
            this.panelScroll.Controls.Add(this.txtCode);
            this.panelScroll.Controls.Add(this.txtEName);
            this.panelScroll.Controls.Add(this.labParent);
            this.panelScroll.Controls.Add(this.txtCName);
            this.panelScroll.Controls.Add(this.labCName);
            this.panelScroll.Controls.Add(this.labEName);
            this.panelScroll.Controls.Add(this.labType);
            this.panelScroll.Controls.Add(this.cmbType);
            this.panelScroll.Controls.Add(this.popParent);
            this.panelScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScroll.Location = new System.Drawing.Point(0, 26);
            this.panelScroll.Name = "panelScroll";
            this.panelScroll.Size = new System.Drawing.Size(394, 151);
            this.panelScroll.TabIndex = 0;
            // 
            // popControl1
            // 
            this.popControl1.Controls.Add(this.treeParent);
            this.popControl1.Controls.Add(this.btnClearPop);
            this.popControl1.Location = new System.Drawing.Point(74, 126);
            this.popControl1.Name = "popControl1";
            this.popControl1.Size = new System.Drawing.Size(275, 118);
            this.popControl1.TabIndex = 4;
            // 
            // treeParent
            // 
            this.treeParent.Appearance.EvenRow.BackColor = System.Drawing.Color.AliceBlue;
            this.treeParent.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeParent.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(130)))), ((int)(((byte)(230)))));
            this.treeParent.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.treeParent.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeParent.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeParent.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colCShortName,
            this.colEShortName});
            this.treeParent.DataSource = this.bsParent;
            this.treeParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeParent.Location = new System.Drawing.Point(0, 0);
            this.treeParent.Name = "treeParent";
            this.treeParent.OptionsBehavior.AllowExpandOnDblClick = false;
            this.treeParent.OptionsBehavior.Editable = false;
            this.treeParent.OptionsBehavior.PopulateServiceColumns = true;
            this.treeParent.OptionsLayout.AddNewColumns = false;
            this.treeParent.OptionsSelection.InvertSelection = true;
            this.treeParent.OptionsView.EnableAppearanceEvenRow = true;
            this.treeParent.OptionsView.ShowIndicator = false;
            this.treeParent.Size = new System.Drawing.Size(275, 95);
            this.treeParent.TabIndex = 0;
            this.treeParent.DoubleClick += new System.EventHandler(this.treeParent_DoubleClick);
            // 
            // colCShortName
            // 
            this.colCShortName.Caption = "Name";
            this.colCShortName.FieldName = "CShortName";
            this.colCShortName.Name = "colCShortName";
            this.colCShortName.OptionsColumn.AllowEdit = false;
            // 
            // colEShortName
            // 
            this.colEShortName.Caption = "Name";
            this.colEShortName.FieldName = "EShortName";
            this.colEShortName.Name = "colEShortName";
            this.colEShortName.OptionsColumn.AllowEdit = false;
            this.colEShortName.Visible = true;
            this.colEShortName.VisibleIndex = 0;
            // 
            // btnClearPop
            // 
            this.btnClearPop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnClearPop.Location = new System.Drawing.Point(0, 95);
            this.btnClearPop.Name = "btnClearPop";
            this.btnClearPop.Size = new System.Drawing.Size(275, 23);
            this.btnClearPop.TabIndex = 5;
            this.btnClearPop.Text = "Clear";
            this.btnClearPop.Click += new System.EventHandler(this.btnClearPop_Click);
            // 
            // cmbType
            // 
            this.cmbType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Type", true));
            this.cmbType.Location = new System.Drawing.Point(74, 76);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(275, 21);
            this.cmbType.TabIndex = 3;
            this.cmbType.TabStop = false;
            // 
            // popParent
            // 
            this.popParent.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ParentName", true));
            this.popParent.Location = new System.Drawing.Point(74, 99);
            this.popParent.Name = "popParent";
            this.popParent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popParent.Properties.PopupControl = this.popControl1;
            this.popParent.Size = new System.Drawing.Size(275, 21);
            this.popParent.TabIndex = 4;
            this.popParent.TabStop = false;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSave});
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // barSave
            // 
            this.barSave.Caption = "&Save";
            this.barSave.Glyph = ((System.Drawing.Image)(resources.GetObject("barSave.Glyph")));
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(394, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 177);
            this.barDockControlBottom.Size = new System.Drawing.Size(394, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 151);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(394, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 151);
            // 
            // OrganizationEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelScroll);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "OrganizationEditPart";
            this.Size = new System.Drawing.Size(394, 177);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.panelScroll.ResumeLayout(false);
            this.panelScroll.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popControl1)).EndInit();
            this.popControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeParent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popParent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.LabelControl labParent;
        private System.Windows.Forms.BindingSource bsParent;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private System.Windows.Forms.Panel panelScroll;
        private DevExpress.XtraEditors.SimpleButton btnClearPop;
        private DevExpress.XtraEditors.PopupContainerControl popControl1;
        private ICP.Framework.ClientComponents.Controls.LWTreeGridControl treeParent;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.PopupContainerEdit popParent;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colCShortName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colEShortName;

    }
}
