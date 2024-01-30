namespace ICP.FAM.UI.TelexApply
{
    partial class TelexApplyEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelexApplyEditPart));
            this.labApplicant = new DevExpress.XtraEditors.LabelControl();
            this.bsTelexApply = new System.Windows.Forms.BindingSource(this.components);
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.dteApplyDate = new DevExpress.XtraEditors.DateEdit();
            this.labApplyDate = new DevExpress.XtraEditors.LabelControl();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.groupConsignee = new System.Windows.Forms.GroupBox();
            this.lwGridControl1 = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.consigneesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gvCustomer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colConsignees = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.chkForAllConsignees = new DevExpress.XtraEditors.CheckEdit();
            this.cmbCompany = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.barConsignees = new DevExpress.XtraBars.Bar();
            this.bbiAddConsignee = new DevExpress.XtraBars.BarButtonItem();
            this.bbiRemoveConsignee = new DevExpress.XtraBars.BarButtonItem();
            this.txtApplicant = new DevExpress.XtraEditors.TextEdit();
            this.labValidDate = new DevExpress.XtraEditors.LabelControl();
            this.dteValidDate = new DevExpress.XtraEditors.DateEdit();
            this.cbxIsValid = new DevExpress.XtraEditors.CheckEdit();
            this.txtCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtCustomerDescription = new DevExpress.XtraEditors.MemoEdit();
            this.labCustomerDescription = new DevExpress.XtraEditors.LabelControl();
            this.cbxOpenEnd = new DevExpress.XtraEditors.CheckEdit();
            this.rdoTelexType = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.bsTelexApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteApplyDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteApplyDate.Properties)).BeginInit();
            this.groupConsignee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lwGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.consigneesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkForAllConsignees.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApplicant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteValidDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteValidDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxIsValid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxOpenEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoTelexType.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labApplicant
            // 
            this.labApplicant.Location = new System.Drawing.Point(12, 193);
            this.labApplicant.Name = "labApplicant";
            this.labApplicant.Size = new System.Drawing.Size(36, 14);
            this.labApplicant.TabIndex = 4;
            this.labApplicant.Text = "申请人";
            // 
            // bsTelexApply
            // 
            this.bsTelexApply.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.TelexApplyInfo);
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTelexApply, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(75, 244);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(302, 85);
            this.txtRemark.TabIndex = 6;
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(12, 35);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(24, 14);
            this.labCustomer.TabIndex = 33;
            this.labCustomer.Text = "客户";
            // 
            // dteApplyDate
            // 
            this.dteApplyDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTelexApply, "ApplyTime", true));
            this.dteApplyDate.EditValue = null;
            this.dteApplyDate.Enabled = false;
            this.dteApplyDate.Location = new System.Drawing.Point(259, 190);
            this.dteApplyDate.Name = "dteApplyDate";
            this.dteApplyDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteApplyDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteApplyDate.Size = new System.Drawing.Size(118, 21);
            this.dteApplyDate.TabIndex = 3;
            // 
            // labApplyDate
            // 
            this.labApplyDate.Location = new System.Drawing.Point(197, 193);
            this.labApplyDate.Name = "labApplyDate";
            this.labApplyDate.Size = new System.Drawing.Size(48, 14);
            this.labApplyDate.TabIndex = 40;
            this.labApplyDate.Text = "申请日期";
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(12, 246);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(24, 14);
            this.labRemark.TabIndex = 41;
            this.labRemark.Text = "备注";
            // 
            // groupConsignee
            // 
            this.groupConsignee.Controls.Add(this.lwGridControl1);
            this.groupConsignee.Controls.Add(this.barDockControl3);
            this.groupConsignee.Controls.Add(this.barDockControl4);
            this.groupConsignee.Controls.Add(this.barDockControl2);
            this.groupConsignee.Controls.Add(this.barDockControl1);
            this.groupConsignee.Location = new System.Drawing.Point(394, 53);
            this.groupConsignee.Name = "groupConsignee";
            this.groupConsignee.Size = new System.Drawing.Size(346, 276);
            this.groupConsignee.TabIndex = 43;
            this.groupConsignee.TabStop = false;
            this.groupConsignee.Text = "收货人";
            // 
            // lwGridControl1
            // 
            this.lwGridControl1.DataSource = this.consigneesBindingSource;
            this.lwGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lwGridControl1.Location = new System.Drawing.Point(3, 44);
            this.lwGridControl1.MainView = this.gvCustomer;
            this.lwGridControl1.MenuManager = this.barManager1;
            this.lwGridControl1.Name = "lwGridControl1";
            this.lwGridControl1.Size = new System.Drawing.Size(340, 229);
            this.lwGridControl1.TabIndex = 47;
            this.lwGridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCustomer});
            // 
            // gvCustomer
            // 
            this.gvCustomer.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colConsignees});
            this.gvCustomer.GridControl = this.lwGridControl1;
            this.gvCustomer.Name = "gvCustomer";
            this.gvCustomer.OptionsView.ShowGroupPanel = false;
            this.gvCustomer.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvCustomer_CellValueChanged);
            // 
            // colConsignees
            // 
            this.colConsignees.Caption = "收货人";
            this.colConsignees.FieldName = "CustomerName";
            this.colConsignees.Name = "colConsignees";
            this.colConsignees.Visible = true;
            this.colConsignees.VisibleIndex = 0;
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
            this.barSave});
            this.barManager1.MaxItemId = 2;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.FAM.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(748, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 340);
            this.barDockControlBottom.Size = new System.Drawing.Size(748, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 314);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(748, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 314);
            // 
            // barDockControl3
            // 
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(3, 44);
            this.barDockControl3.Size = new System.Drawing.Size(0, 229);
            // 
            // barDockControl4
            // 
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(343, 44);
            this.barDockControl4.Size = new System.Drawing.Size(0, 229);
            // 
            // barDockControl2
            // 
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(3, 273);
            this.barDockControl2.Size = new System.Drawing.Size(340, 0);
            // 
            // barDockControl1
            // 
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(3, 18);
            this.barDockControl1.Size = new System.Drawing.Size(340, 26);
            // 
            // chkForAllConsignees
            // 
            this.chkForAllConsignees.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsTelexApply, "HasConsignees", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkForAllConsignees.EditValue = true;
            this.chkForAllConsignees.Location = new System.Drawing.Point(394, 32);
            this.chkForAllConsignees.MenuManager = this.barManager1;
            this.chkForAllConsignees.Name = "chkForAllConsignees";
            this.chkForAllConsignees.Properties.Caption = "全部收货人";
            this.chkForAllConsignees.Size = new System.Drawing.Size(102, 19);
            this.chkForAllConsignees.TabIndex = 48;
            this.chkForAllConsignees.CheckedChanged += new System.EventHandler(this.cbxForAllConsignees_CheckedChanged);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTelexApply, "CompanyId", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbCompany, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbCompany.Location = new System.Drawing.Point(75, 163);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(302, 21);
            this.cmbCompany.TabIndex = 1;
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(12, 166);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 54;
            this.labCompany.Text = "公司";
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barConsignees});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this.groupConsignee;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiAddConsignee,
            this.bbiRemoveConsignee});
            this.barManager2.MainMenu = this.barConsignees;
            this.barManager2.MaxItemId = 2;
            // 
            // barConsignees
            // 
            this.barConsignees.BarName = "Main menu";
            this.barConsignees.DockCol = 0;
            this.barConsignees.DockRow = 0;
            this.barConsignees.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barConsignees.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAddConsignee),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiRemoveConsignee)});
            this.barConsignees.OptionsBar.MultiLine = true;
            this.barConsignees.OptionsBar.UseWholeRow = true;
            this.barConsignees.Text = "Main menu";
            // 
            // bbiAddConsignee
            // 
            this.bbiAddConsignee.Caption = "新增(&N)";
            this.bbiAddConsignee.Glyph = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.bbiAddConsignee.Id = 0;
            this.bbiAddConsignee.Name = "bbiAddConsignee";
            this.bbiAddConsignee.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiAddConsignee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAddConsignee_ItemClick);
            // 
            // bbiRemoveConsignee
            // 
            this.bbiRemoveConsignee.Caption = "删除(&R)";
            this.bbiRemoveConsignee.Glyph = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.bbiRemoveConsignee.Id = 1;
            this.bbiRemoveConsignee.Name = "bbiRemoveConsignee";
            this.bbiRemoveConsignee.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.bbiRemoveConsignee.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRemoveConsignee_ItemClick);
            // 
            // txtApplicant
            // 
            this.txtApplicant.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTelexApply, "CreateByID", true));
            this.txtApplicant.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTelexApply, "CreateByName", true));
            this.txtApplicant.Location = new System.Drawing.Point(75, 190);
            this.txtApplicant.MenuManager = this.barManager1;
            this.txtApplicant.Name = "txtApplicant";
            this.txtApplicant.Properties.ReadOnly = true;
            this.txtApplicant.Size = new System.Drawing.Size(118, 21);
            this.txtApplicant.TabIndex = 2;
            // 
            // labValidDate
            // 
            this.labValidDate.Location = new System.Drawing.Point(12, 220);
            this.labValidDate.Name = "labValidDate";
            this.labValidDate.Size = new System.Drawing.Size(48, 14);
            this.labValidDate.TabIndex = 351;
            this.labValidDate.Text = "有效日期";
            // 
            // dteValidDate
            // 
            this.dteValidDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTelexApply, "ValidDate", true));
            this.dteValidDate.EditValue = null;
            this.dteValidDate.Location = new System.Drawing.Point(75, 217);
            this.dteValidDate.Name = "dteValidDate";
            this.dteValidDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteValidDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteValidDate.Size = new System.Drawing.Size(118, 21);
            this.dteValidDate.TabIndex = 4;
            // 
            // cbxIsValid
            // 
            this.cbxIsValid.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsTelexApply, "IsValid", true));
            this.cbxIsValid.Enabled = false;
            this.cbxIsValid.Location = new System.Drawing.Point(313, 219);
            this.cbxIsValid.MenuManager = this.barManager1;
            this.cbxIsValid.Name = "cbxIsValid";
            this.cbxIsValid.Properties.Caption = "有效";
            this.cbxIsValid.Size = new System.Drawing.Size(75, 19);
            this.cbxIsValid.TabIndex = 5;
            // 
            // txtCustomer
            // 
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsTelexApply, "CustomerId", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCustomer.Location = new System.Drawing.Point(75, 32);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.txtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(302, 21);
            this.txtCustomer.TabIndex = 0;
            this.txtCustomer.TabStop = false;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // txtCustomerDescription
            // 
            this.txtCustomerDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsTelexApply, "CustomerDescription", true));
            this.txtCustomerDescription.Location = new System.Drawing.Point(75, 59);
            this.txtCustomerDescription.Name = "txtCustomerDescription";
            this.txtCustomerDescription.Size = new System.Drawing.Size(302, 66);
            this.txtCustomerDescription.TabIndex = 356;
            // 
            // labCustomerDescription
            // 
            this.labCustomerDescription.Location = new System.Drawing.Point(12, 61);
            this.labCustomerDescription.Name = "labCustomerDescription";
            this.labCustomerDescription.Size = new System.Drawing.Size(48, 14);
            this.labCustomerDescription.TabIndex = 357;
            this.labCustomerDescription.Text = "客户描述";
            // 
            // cbxOpenEnd
            // 
            this.cbxOpenEnd.Location = new System.Drawing.Point(213, 219);
            this.cbxOpenEnd.MenuManager = this.barManager1;
            this.cbxOpenEnd.Name = "cbxOpenEnd";
            this.cbxOpenEnd.Properties.Caption = "无期限";
            this.cbxOpenEnd.Size = new System.Drawing.Size(75, 19);
            this.cbxOpenEnd.TabIndex = 362;
            this.cbxOpenEnd.CheckedChanged += new System.EventHandler(this.cbxOpenEnd_CheckedChanged);
            // 
            // rdoTelexType
            // 
            this.rdoTelexType.Location = new System.Drawing.Point(75, 133);
            this.rdoTelexType.Name = "rdoTelexType";
            this.rdoTelexType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0D, "All"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1D, "Telex"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2D, "SWB")});
            this.rdoTelexType.Size = new System.Drawing.Size(302, 24);
            this.rdoTelexType.TabIndex = 367;
            // 
            // TelexApplyEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CodeValuePairs = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("$this.CodeValuePairs")));
            this.Controls.Add(this.rdoTelexType);
            this.Controls.Add(this.cbxOpenEnd);
            this.Controls.Add(this.txtCustomerDescription);
            this.Controls.Add(this.labCustomerDescription);
            this.Controls.Add(this.cbxIsValid);
            this.Controls.Add(this.labValidDate);
            this.Controls.Add(this.dteValidDate);
            this.Controls.Add(this.txtApplicant);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.labCompany);
            this.Controls.Add(this.chkForAllConsignees);
            this.Controls.Add(this.groupConsignee);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labApplyDate);
            this.Controls.Add(this.dteApplyDate);
            this.Controls.Add(this.labCustomer);
            this.Controls.Add(this.labApplicant);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "TelexApplyEditPart";
            this.Size = new System.Drawing.Size(748, 340);
            ((System.ComponentModel.ISupportInitialize)(this.bsTelexApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteApplyDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteApplyDate.Properties)).EndInit();
            this.groupConsignee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lwGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.consigneesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkForAllConsignees.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApplicant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteValidDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteValidDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxIsValid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxOpenEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoTelexType.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labApplicant;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.DateEdit dteApplyDate;
        private DevExpress.XtraEditors.LabelControl labApplyDate;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private System.Windows.Forms.GroupBox groupConsignee;
        private System.Windows.Forms.BindingSource bsTelexApply;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.CheckEdit chkForAllConsignees;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar barConsignees;
        private DevExpress.XtraBars.BarButtonItem bbiAddConsignee;
        private DevExpress.XtraBars.BarButtonItem bbiRemoveConsignee;
        private ICP.Framework.ClientComponents.Controls.LWGridControl lwGridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCustomer;
        private DevExpress.XtraGrid.Columns.GridColumn colConsignees;
        private DevExpress.XtraEditors.TextEdit txtApplicant;
        private DevExpress.XtraEditors.LabelControl labValidDate;
        private DevExpress.XtraEditors.DateEdit dteValidDate;
        private DevExpress.XtraEditors.CheckEdit cbxIsValid;
        private DevExpress.XtraEditors.ButtonEdit txtCustomer;
        private System.Windows.Forms.BindingSource consigneesBindingSource;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.MemoEdit txtCustomerDescription;
        private DevExpress.XtraEditors.LabelControl labCustomerDescription;
        private DevExpress.XtraEditors.CheckEdit cbxOpenEnd;
        private DevExpress.XtraEditors.RadioGroup rdoTelexType;
    }
}
