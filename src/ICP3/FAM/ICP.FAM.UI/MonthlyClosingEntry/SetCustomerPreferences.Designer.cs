namespace ICP.FAM.UI.MonthlyClosingEntry
{
    partial class SetCustomerPreferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetCustomerPreferences));
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btSave = new System.Windows.Forms.ToolStripButton();
            this.btRefresh = new System.Windows.Forms.ToolStripButton();
            this.panne = new System.Windows.Forms.Panel();
            this.numTue = new DevExpress.XtraEditors.SpinEdit();
            this.labTue = new DevExpress.XtraEditors.LabelControl();
            this.txtContact = new DevExpress.XtraEditors.MemoEdit();
            this.labContact = new DevExpress.XtraEditors.LabelControl();
            this.txtCustomerID = new ICP.ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit();
            this.cmbCompany = new ICP.ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl();
            this.chkIsOnlyMBL = new DevExpress.XtraEditors.CheckEdit();
            this.groupLocalService = new System.Windows.Forms.GroupBox();
            this.chkIsHblCopy = new DevExpress.XtraEditors.CheckEdit();
            this.chkIsTruck = new DevExpress.XtraEditors.CheckEdit();
            this.cmbCargoType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labPdfAssembled = new DevExpress.XtraEditors.LabelControl();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            this.labPaymentDay = new DevExpress.XtraEditors.LabelControl();
            this.txtShipTo = new DevExpress.XtraEditors.MemoEdit();
            this.labShipto = new DevExpress.XtraEditors.LabelControl();
            this.txtCommodity = new DevExpress.XtraEditors.MemoEdit();
            this.labTitle = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.btAddTo = new System.Windows.Forms.ToolStripButton();
            this.btAddCC = new System.Windows.Forms.ToolStripButton();
            this.btDelete = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridPre = new DevExpress.XtraGrid.GridControl();
            this.ViewPre = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.panne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContact.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).BeginInit();
            this.groupLocalService.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsHblCopy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPre)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.CustomerPreferencesInfo);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btSave,
            this.btRefresh});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(647, 25);
            this.toolStrip2.TabIndex = 805;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btSave
            // 
            this.btSave.Image = global::ICP.FAM.UI.Properties.Resources.Save_16;
            this.btSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(70, 22);
            this.btSave.Text = "Save(&S)";
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Image = global::ICP.FAM.UI.Properties.Resources.Refresh_16;
            this.btRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(74, 22);
            this.btRefresh.Text = "Clear(&C)";
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // panne
            // 
            this.panne.Controls.Add(this.numTue);
            this.panne.Controls.Add(this.labTue);
            this.panne.Controls.Add(this.txtContact);
            this.panne.Controls.Add(this.labContact);
            this.panne.Controls.Add(this.toolStrip2);
            this.panne.Controls.Add(this.txtCustomerID);
            this.panne.Controls.Add(this.cmbCompany);
            this.panne.Controls.Add(this.chkIsOnlyMBL);
            this.panne.Controls.Add(this.groupLocalService);
            this.panne.Controls.Add(this.cmbCargoType);
            this.panne.Controls.Add(this.labPdfAssembled);
            this.panne.Controls.Add(this.numQuantity);
            this.panne.Controls.Add(this.labPaymentDay);
            this.panne.Controls.Add(this.txtShipTo);
            this.panne.Controls.Add(this.labShipto);
            this.panne.Controls.Add(this.txtCommodity);
            this.panne.Controls.Add(this.labTitle);
            this.panne.Controls.Add(this.labCustomer);
            this.panne.Controls.Add(this.labCompany);
            this.panne.Dock = System.Windows.Forms.DockStyle.Top;
            this.panne.Location = new System.Drawing.Point(0, 0);
            this.panne.Name = "panne";
            this.panne.Size = new System.Drawing.Size(647, 281);
            this.panne.TabIndex = 806;
            // 
            // numTue
            // 
            this.numTue.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Tue", true));
            this.numTue.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numTue.Location = new System.Drawing.Point(67, 199);
            this.numTue.Name = "numTue";
            this.numTue.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.numTue.Properties.Appearance.Options.UseBackColor = true;
            this.numTue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numTue.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numTue.Properties.IsFloatValue = false;
            this.numTue.Properties.Mask.EditMask = "N00";
            this.numTue.Properties.MaxValue = new decimal(new int[] {
            1410065408,
            2,
            0,
            0});
            this.numTue.Size = new System.Drawing.Size(226, 21);
            this.numTue.TabIndex = 7;
            // 
            // labTue
            // 
            this.labTue.Location = new System.Drawing.Point(39, 201);
            this.labTue.Name = "labTue";
            this.labTue.Size = new System.Drawing.Size(22, 14);
            this.labTue.TabIndex = 821;
            this.labTue.Text = "Tue";
            // 
            // txtContact
            // 
            this.txtContact.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NotifyContact", true));
            this.txtContact.Location = new System.Drawing.Point(392, 202);
            this.txtContact.Name = "txtContact";
            this.txtContact.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.txtContact.Properties.Appearance.Options.UseBackColor = true;
            this.txtContact.Size = new System.Drawing.Size(226, 70);
            this.txtContact.TabIndex = 10;
            // 
            // labContact
            // 
            this.labContact.Location = new System.Drawing.Point(341, 202);
            this.labContact.Name = "labContact";
            this.labContact.Size = new System.Drawing.Size(43, 14);
            this.labContact.TabIndex = 819;
            this.labContact.Text = "Contact";
            // 
            // txtCustomerID
            // 
            this.txtCustomerID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CustomerName", true));
            this.txtCustomerID.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "CustomerID", true));
            this.txtCustomerID.Enabled = false;
            this.txtCustomerID.FinderName = "CustomerFinder";
            this.txtCustomerID.Location = new System.Drawing.Point(392, 36);
            this.txtCustomerID.Name = "txtCustomerID";
            this.txtCustomerID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomerID.Size = new System.Drawing.Size(226, 21);
            this.txtCustomerID.TabIndex = 2;
            // 
            // cmbCompany
            // 
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "CompanyID", true));
            this.cmbCompany.DataBindings.Add(new System.Windows.Forms.Binding("EditText", this.bindingSource1, "CompanyName", true));
            this.cmbCompany.EditText = "";
            this.cmbCompany.EditValue = ((System.Collections.Generic.List<System.Guid>)(resources.GetObject("cmbCompany.EditValue")));
            this.cmbCompany.Enabled = false;
            this.cmbCompany.Location = new System.Drawing.Point(66, 34);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.ReadOnly = false;
            this.cmbCompany.ShowDepartment = false;
            this.cmbCompany.Size = new System.Drawing.Size(226, 21);
            this.cmbCompany.SplitString = ",";
            this.cmbCompany.TabIndex = 1;
            // 
            // chkIsOnlyMBL
            // 
            this.chkIsOnlyMBL.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsNeedPO", true));
            this.chkIsOnlyMBL.Location = new System.Drawing.Point(250, 247);
            this.chkIsOnlyMBL.Name = "chkIsOnlyMBL";
            this.chkIsOnlyMBL.Properties.Caption = "提供PO#";
            this.chkIsOnlyMBL.Size = new System.Drawing.Size(78, 19);
            this.chkIsOnlyMBL.TabIndex = 9;
            // 
            // groupLocalService
            // 
            this.groupLocalService.Controls.Add(this.chkIsHblCopy);
            this.groupLocalService.Controls.Add(this.chkIsTruck);
            this.groupLocalService.Font = new System.Drawing.Font("Tahoma", 8F);
            this.groupLocalService.Location = new System.Drawing.Point(23, 230);
            this.groupLocalService.Name = "groupLocalService";
            this.groupLocalService.Size = new System.Drawing.Size(221, 42);
            this.groupLocalService.TabIndex = 8;
            this.groupLocalService.TabStop = false;
            this.groupLocalService.Text = "其它附件";
            // 
            // chkIsHblCopy
            // 
            this.chkIsHblCopy.Location = new System.Drawing.Point(6, 17);
            this.chkIsHblCopy.Name = "chkIsHblCopy";
            this.chkIsHblCopy.Properties.Caption = "HBL Copy";
            this.chkIsHblCopy.Size = new System.Drawing.Size(85, 19);
            this.chkIsHblCopy.TabIndex = 31;
            // 
            // chkIsTruck
            // 
            this.chkIsTruck.Location = new System.Drawing.Point(110, 17);
            this.chkIsTruck.Name = "chkIsTruck";
            this.chkIsTruck.Properties.Caption = "Arrival Notice";
            this.chkIsTruck.Size = new System.Drawing.Size(100, 19);
            this.chkIsTruck.TabIndex = 32;
            // 
            // cmbCargoType
            // 
            this.cmbCargoType.Location = new System.Drawing.Point(392, 166);
            this.cmbCargoType.Name = "cmbCargoType";
            this.cmbCargoType.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbCargoType.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCargoType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCargoType.Size = new System.Drawing.Size(226, 21);
            this.cmbCargoType.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbCargoType.TabIndex = 6;
            // 
            // labPdfAssembled
            // 
            this.labPdfAssembled.Location = new System.Drawing.Point(308, 169);
            this.labPdfAssembled.Name = "labPdfAssembled";
            this.labPdfAssembled.Size = new System.Drawing.Size(76, 14);
            this.labPdfAssembled.TabIndex = 813;
            this.labPdfAssembled.Text = "PdfAssembled";
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "NotifyPaymentDay", true));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numQuantity.Location = new System.Drawing.Point(67, 167);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.numQuantity.Properties.Appearance.Options.UseBackColor = true;
            this.numQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numQuantity.Properties.IsFloatValue = false;
            this.numQuantity.Properties.Mask.EditMask = "N00";
            this.numQuantity.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numQuantity.Size = new System.Drawing.Size(226, 21);
            this.numQuantity.TabIndex = 5;
            // 
            // labPaymentDay
            // 
            this.labPaymentDay.Location = new System.Drawing.Point(17, 169);
            this.labPaymentDay.Name = "labPaymentDay";
            this.labPaymentDay.Size = new System.Drawing.Size(44, 14);
            this.labPaymentDay.TabIndex = 811;
            this.labPaymentDay.Text = "PayDays";
            // 
            // txtShipTo
            // 
            this.txtShipTo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ShipTo", true));
            this.txtShipTo.Location = new System.Drawing.Point(392, 68);
            this.txtShipTo.Name = "txtShipTo";
            this.txtShipTo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtShipTo.Properties.Appearance.Options.UseBackColor = true;
            this.txtShipTo.Size = new System.Drawing.Size(226, 83);
            this.txtShipTo.TabIndex = 4;
            // 
            // labShipto
            // 
            this.labShipto.Location = new System.Drawing.Point(345, 69);
            this.labShipto.Name = "labShipto";
            this.labShipto.Size = new System.Drawing.Size(38, 14);
            this.labShipto.TabIndex = 809;
            this.labShipto.Text = "ShipTo";
            // 
            // txtCommodity
            // 
            this.txtCommodity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "InvoiceTitle", true));
            this.txtCommodity.Location = new System.Drawing.Point(67, 68);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCommodity.Properties.Appearance.Options.UseBackColor = true;
            this.txtCommodity.Size = new System.Drawing.Size(226, 83);
            this.txtCommodity.TabIndex = 3;
            // 
            // labTitle
            // 
            this.labTitle.Location = new System.Drawing.Point(37, 69);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(24, 14);
            this.labTitle.TabIndex = 807;
            this.labTitle.Text = "Title";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(332, 38);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 806;
            this.labCustomer.Text = "Customer";
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(11, 38);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(50, 14);
            this.labCompany.TabIndex = 805;
            this.labCompany.Text = "Company";
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.Location = new System.Drawing.Point(272, 3);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(628, 25);
            this.miniToolStrip.TabIndex = 0;
            // 
            // btAddTo
            // 
            this.btAddTo.Image = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.btAddTo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAddTo.Name = "btAddTo";
            this.btAddTo.Size = new System.Drawing.Size(99, 22);
            this.btAddTo.Text = "AddToEMail";
            this.btAddTo.Click += new System.EventHandler(this.btAddTo_Click);
            // 
            // btAddCC
            // 
            this.btAddCC.Image = global::ICP.FAM.UI.Properties.Resources.Add_File_16;
            this.btAddCC.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btAddCC.Name = "btAddCC";
            this.btAddCC.Size = new System.Drawing.Size(99, 22);
            this.btAddCC.Text = "AddCCEmail";
            this.btAddCC.Click += new System.EventHandler(this.btAddCC_Click);
            // 
            // btDelete
            // 
            this.btDelete.Image = global::ICP.FAM.UI.Properties.Resources.Delete_16;
            this.btDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(65, 22);
            this.btDelete.Text = "Delete";
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridPre);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 281);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(647, 175);
            this.panel1.TabIndex = 802;
            // 
            // gridPre
            // 
            this.gridPre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPre.Location = new System.Drawing.Point(0, 25);
            this.gridPre.MainView = this.ViewPre;
            this.gridPre.Name = "gridPre";
            this.gridPre.Size = new System.Drawing.Size(647, 150);
            this.gridPre.TabIndex = 1;
            this.gridPre.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ViewPre});
            // 
            // ViewPre
            // 
            this.ViewPre.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColType,
            this.ColName,
            this.ColMail});
            this.ViewPre.GridControl = this.gridPre;
            this.ViewPre.Name = "ViewPre";
            this.ViewPre.OptionsView.ShowGroupPanel = false;
            // 
            // ColType
            // 
            this.ColType.Caption = "Type";
            this.ColType.FieldName = "Type";
            this.ColType.Name = "ColType";
            this.ColType.OptionsColumn.AllowEdit = false;
            this.ColType.OptionsColumn.AllowFocus = false;
            this.ColType.Visible = true;
            this.ColType.VisibleIndex = 0;
            this.ColType.Width = 80;
            // 
            // ColName
            // 
            this.ColName.Caption = "Name";
            this.ColName.FieldName = "Name";
            this.ColName.Name = "ColName";
            this.ColName.Visible = true;
            this.ColName.VisibleIndex = 1;
            this.ColName.Width = 126;
            // 
            // ColMail
            // 
            this.ColMail.Caption = "Mail";
            this.ColMail.FieldName = "Mail";
            this.ColMail.Name = "ColMail";
            this.ColMail.Visible = true;
            this.ColMail.VisibleIndex = 2;
            this.ColMail.Width = 355;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btAddTo,
            this.btAddCC,
            this.btDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(647, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SetCustomerPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panne);
            this.Name = "SetCustomerPreferences";
            this.Size = new System.Drawing.Size(647, 456);
            this.Load += new System.EventHandler(this.SetCustomerPreferences_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panne.ResumeLayout(false);
            this.panne.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContact.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsOnlyMBL.Properties)).EndInit();
            this.groupLocalService.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkIsHblCopy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsTruck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCargoType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShipTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPre)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btSave;
        private System.Windows.Forms.ToolStripButton btRefresh;
        private System.Windows.Forms.Panel panne;
        private ReportCenter.UI.Comm.Controls.MutipleCustomerFinderButtonEdit txtCustomerID;
        private ReportCenter.UI.Comm.Controls.CompanyTreeCheckControl cmbCompany;
        private DevExpress.XtraEditors.CheckEdit chkIsOnlyMBL;
        private System.Windows.Forms.GroupBox groupLocalService;
        private DevExpress.XtraEditors.CheckEdit chkIsHblCopy;
        private DevExpress.XtraEditors.CheckEdit chkIsTruck;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCargoType;
        private DevExpress.XtraEditors.LabelControl labPdfAssembled;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.LabelControl labPaymentDay;
        private DevExpress.XtraEditors.MemoEdit txtShipTo;
        private DevExpress.XtraEditors.LabelControl labShipto;
        private DevExpress.XtraEditors.MemoEdit txtCommodity;
        private DevExpress.XtraEditors.LabelControl labTitle;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.ToolStripButton btAddTo;
        private System.Windows.Forms.ToolStripButton btAddCC;
        private System.Windows.Forms.ToolStripButton btDelete;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridPre;
        private DevExpress.XtraGrid.Views.Grid.GridView ViewPre;
        private DevExpress.XtraGrid.Columns.GridColumn ColType;
        private DevExpress.XtraGrid.Columns.GridColumn ColName;
        private DevExpress.XtraGrid.Columns.GridColumn ColMail;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevExpress.XtraEditors.MemoEdit txtContact;
        private DevExpress.XtraEditors.LabelControl labContact;
        private DevExpress.XtraEditors.SpinEdit numTue;
        private DevExpress.XtraEditors.LabelControl labTue;
    }
}
