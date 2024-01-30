namespace ICP.TMS.UI
{
    partial class DriverEdit
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
            this.labDriverName = new DevExpress.XtraEditors.LabelControl();
            this.labAdress = new DevExpress.XtraEditors.LabelControl();
            this.labMobile = new DevExpress.XtraEditors.LabelControl();
            this.txtDriverName = new DevExpress.XtraEditors.TextEdit();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.txtMobile = new DevExpress.XtraEditors.TextEdit();
            this.txtAdress = new DevExpress.XtraEditors.TextEdit();
            this.labCityID = new DevExpress.XtraEditors.LabelControl();
            this.labProvinceID = new DevExpress.XtraEditors.LabelControl();
            this.labCardID = new DevExpress.XtraEditors.LabelControl();
            this.txtCardID = new DevExpress.XtraEditors.TextEdit();
            this.cmbCityID = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbTruck = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labTruck = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.cmbProvinceID = new ICP.TMS.UI.LWComboBoxTree();
            this.bsGeography = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtDriverName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCardID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCityID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTruck.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProvinceID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGeography)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // labDriverName
            // 
            this.labDriverName.Location = new System.Drawing.Point(14, 37);
            this.labDriverName.Name = "labDriverName";
            this.labDriverName.Size = new System.Drawing.Size(48, 14);
            this.labDriverName.TabIndex = 0;
            this.labDriverName.Text = "司机姓名";
            // 
            // labAdress
            // 
            this.labAdress.Location = new System.Drawing.Point(14, 65);
            this.labAdress.Name = "labAdress";
            this.labAdress.Size = new System.Drawing.Size(24, 14);
            this.labAdress.TabIndex = 0;
            this.labAdress.Text = "地址";
            // 
            // labMobile
            // 
            this.labMobile.Location = new System.Drawing.Point(275, 37);
            this.labMobile.Name = "labMobile";
            this.labMobile.Size = new System.Drawing.Size(24, 14);
            this.labMobile.TabIndex = 1;
            this.labMobile.Text = "手机";
            // 
            // txtDriverName
            // 
            this.txtDriverName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Name", true));
            this.txtDriverName.Location = new System.Drawing.Point(76, 34);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtDriverName.Properties.Appearance.Options.UseBackColor = true;
            this.txtDriverName.Size = new System.Drawing.Size(125, 21);
            this.txtDriverName.TabIndex = 0;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.TMS.ServiceInterface.DriversDataList);
            // 
            // txtMobile
            // 
            this.txtMobile.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Mobile", true));
            this.txtMobile.Location = new System.Drawing.Point(356, 34);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtMobile.Properties.Appearance.Options.UseBackColor = true;
            this.txtMobile.Size = new System.Drawing.Size(125, 21);
            this.txtMobile.TabIndex = 1;
            // 
            // txtAdress
            // 
            this.txtAdress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Adress", true));
            this.txtAdress.Location = new System.Drawing.Point(76, 62);
            this.txtAdress.Name = "txtAdress";
            this.txtAdress.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtAdress.Properties.Appearance.Options.UseBackColor = true;
            this.txtAdress.Size = new System.Drawing.Size(405, 21);
            this.txtAdress.TabIndex = 2;
            // 
            // labCityID
            // 
            this.labCityID.Location = new System.Drawing.Point(275, 93);
            this.labCityID.Name = "labCityID";
            this.labCityID.Size = new System.Drawing.Size(24, 14);
            this.labCityID.TabIndex = 0;
            this.labCityID.Text = "城市";
            // 
            // labProvinceID
            // 
            this.labProvinceID.Location = new System.Drawing.Point(14, 93);
            this.labProvinceID.Name = "labProvinceID";
            this.labProvinceID.Size = new System.Drawing.Size(29, 14);
            this.labProvinceID.TabIndex = 0;
            this.labProvinceID.Text = "省/州";
            // 
            // labCardID
            // 
            this.labCardID.Location = new System.Drawing.Point(14, 122);
            this.labCardID.Name = "labCardID";
            this.labCardID.Size = new System.Drawing.Size(36, 14);
            this.labCardID.TabIndex = 1;
            this.labCardID.Text = "身份ID";
            // 
            // txtCardID
            // 
            this.txtCardID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "CardNo", true));
            this.txtCardID.Location = new System.Drawing.Point(76, 119);
            this.txtCardID.Name = "txtCardID";
            this.txtCardID.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtCardID.Properties.Appearance.Options.UseBackColor = true;
            this.txtCardID.Size = new System.Drawing.Size(125, 21);
            this.txtCardID.TabIndex = 5;
            // 
            // cmbCityID
            // 
            this.cmbCityID.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "CityID", true));
            this.cmbCityID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "CityName", true));
            this.cmbCityID.Location = new System.Drawing.Point(356, 90);
            this.cmbCityID.Name = "cmbCityID";
            this.cmbCityID.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCityID.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCityID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCityID.Size = new System.Drawing.Size(125, 21);
            this.cmbCityID.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCityID.TabIndex = 4;
            // 
            // cmbTruck
            // 
            this.cmbTruck.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "TruckID", true));
            this.cmbTruck.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "TruckNo", true));
            this.cmbTruck.Location = new System.Drawing.Point(356, 119);
            this.cmbTruck.Name = "cmbTruck";
            this.cmbTruck.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbTruck.Properties.Appearance.Options.UseBackColor = true;
            this.cmbTruck.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTruck.Size = new System.Drawing.Size(125, 21);
            this.cmbTruck.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbTruck.TabIndex = 6;
            // 
            // labTruck
            // 
            this.labTruck.Location = new System.Drawing.Point(275, 122);
            this.labTruck.Name = "labTruck";
            this.labTruck.Size = new System.Drawing.Size(48, 14);
            this.labTruck.TabIndex = 0;
            this.labTruck.Text = "默认拖车";
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(76, 146);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(405, 63);
            this.txtRemark.TabIndex = 7;
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(14, 148);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(24, 14);
            this.labRemark.TabIndex = 1;
            this.labRemark.Text = "备注";
            // 
            // cmbProvinceID
            // 
            this.cmbProvinceID.AllowMultSelect = false;
            this.cmbProvinceID.DataSource = null;
            this.cmbProvinceID.DisplayMember = "CName";
            this.cmbProvinceID.Location = new System.Drawing.Point(76, 90);
            this.cmbProvinceID.Name = "cmbProvinceID";
            this.cmbProvinceID.ParentMember = "ParentID";
            this.cmbProvinceID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProvinceID.Properties.PopupSizeable = false;
            this.cmbProvinceID.Properties.ShowPopupCloseButton = false;
            this.cmbProvinceID.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbProvinceID.RootValue = 0;
            this.cmbProvinceID.SelectedValue = null;
            this.cmbProvinceID.Separator = ",";
            this.cmbProvinceID.Size = new System.Drawing.Size(125, 21);
            this.cmbProvinceID.TabIndex = 3;
            this.cmbProvinceID.ValueMember = "ID";
            // 
            // bsGeography
            // 
            this.bsGeography.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CountryProvinceList);
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
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存(&S)";
            this.barSave.Glyph = global::ICP.TMS.UI.Properties.Resources.Save_16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(536, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 235);
            this.barDockControlBottom.Size = new System.Drawing.Size(536, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 209);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(536, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 209);
            // 
            // DriverEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbProvinceID);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.cmbTruck);
            this.Controls.Add(this.cmbCityID);
            this.Controls.Add(this.txtAdress);
            this.Controls.Add(this.txtCardID);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.txtDriverName);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labCardID);
            this.Controls.Add(this.labTruck);
            this.Controls.Add(this.labProvinceID);
            this.Controls.Add(this.labMobile);
            this.Controls.Add(this.labCityID);
            this.Controls.Add(this.labAdress);
            this.Controls.Add(this.labDriverName);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "DriverEdit";
            this.Size = new System.Drawing.Size(536, 235);
            ((System.ComponentModel.ISupportInitialize)(this.txtDriverName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobile.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAdress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCardID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCityID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTruck.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProvinceID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsGeography)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labDriverName;
        private DevExpress.XtraEditors.LabelControl labAdress;
        private DevExpress.XtraEditors.LabelControl labMobile;
        private DevExpress.XtraEditors.TextEdit txtDriverName;
        private DevExpress.XtraEditors.TextEdit txtMobile;
        private DevExpress.XtraEditors.TextEdit txtAdress;
        private DevExpress.XtraEditors.LabelControl labCityID;
        private DevExpress.XtraEditors.LabelControl labProvinceID;
        private DevExpress.XtraEditors.LabelControl labCardID;
        private DevExpress.XtraEditors.TextEdit txtCardID;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCityID;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbTruck;
        private DevExpress.XtraEditors.LabelControl labTruck;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private System.Windows.Forms.BindingSource bsList;
        private ICP.TMS.UI.LWComboBoxTree cmbProvinceID;
        private System.Windows.Forms.BindingSource bsGeography;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
