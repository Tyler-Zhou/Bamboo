namespace ICP.TMS.UI
{
    partial class TruckEdit
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
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.labTruckNo = new DevExpress.XtraEditors.LabelControl();
            this.txtTruckNo = new DevExpress.XtraEditors.TextEdit();
            this.labBuyDate = new DevExpress.XtraEditors.LabelControl();
            this.dtpBuyDate = new DevExpress.XtraEditors.DateEdit();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.txtType = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labCreateByName = new DevExpress.XtraEditors.LabelControl();
            this.txtCreateName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTruckNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBuyDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBuyDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(72, 89);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(418, 63);
            this.txtRemark.TabIndex = 4;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.TMS.ServiceInterface.TruckDataList);
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(12, 89);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(24, 14);
            this.labRemark.TabIndex = 1;
            this.labRemark.Text = "备注";
            // 
            // labTruckNo
            // 
            this.labTruckNo.Location = new System.Drawing.Point(12, 36);
            this.labTruckNo.Name = "labTruckNo";
            this.labTruckNo.Size = new System.Drawing.Size(36, 14);
            this.labTruckNo.TabIndex = 1;
            this.labTruckNo.Text = "车牌号";
            // 
            // txtTruckNo
            // 
            this.txtTruckNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "TruckNo", true));
            this.txtTruckNo.Location = new System.Drawing.Point(72, 33);
            this.txtTruckNo.Name = "txtTruckNo";
            this.txtTruckNo.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtTruckNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtTruckNo.Size = new System.Drawing.Size(143, 21);
            this.txtTruckNo.TabIndex = 0;
            // 
            // labBuyDate
            // 
            this.labBuyDate.Location = new System.Drawing.Point(268, 36);
            this.labBuyDate.Name = "labBuyDate";
            this.labBuyDate.Size = new System.Drawing.Size(48, 14);
            this.labBuyDate.TabIndex = 1;
            this.labBuyDate.Text = "购买日期";
            // 
            // dtpBuyDate
            // 
            this.dtpBuyDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "BuyDate", true));
            this.dtpBuyDate.EditValue = null;
            this.dtpBuyDate.Location = new System.Drawing.Point(347, 33);
            this.dtpBuyDate.Name = "dtpBuyDate";
            this.dtpBuyDate.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.dtpBuyDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtpBuyDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpBuyDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpBuyDate.Size = new System.Drawing.Size(144, 21);
            this.dtpBuyDate.TabIndex = 1;
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(12, 65);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(24, 14);
            this.labType.TabIndex = 1;
            this.labType.Text = "型号";
            // 
            // txtType
            // 
            this.txtType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "TypeName", true));
            this.txtType.Location = new System.Drawing.Point(72, 62);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(143, 21);
            this.txtType.TabIndex = 2;
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
            this.barManager1.MaxItemId = 2;
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
            this.barSave.Id = 1;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(585, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 167);
            this.barDockControlBottom.Size = new System.Drawing.Size(585, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 141);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(585, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 141);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(77, 175);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(10, 21);
            this.textEdit1.TabIndex = 2;
            this.textEdit1.Visible = false;
            // 
            // labCreateByName
            // 
            this.labCreateByName.Location = new System.Drawing.Point(268, 65);
            this.labCreateByName.Name = "labCreateByName";
            this.labCreateByName.Size = new System.Drawing.Size(36, 14);
            this.labCreateByName.TabIndex = 1;
            this.labCreateByName.Text = "创建人";
            // 
            // txtCreateName
            // 
            this.txtCreateName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "CreateByName", true));
            this.txtCreateName.Enabled = false;
            this.txtCreateName.Location = new System.Drawing.Point(347, 62);
            this.txtCreateName.Name = "txtCreateName";
            this.txtCreateName.Size = new System.Drawing.Size(143, 21);
            this.txtCreateName.TabIndex = 3;
            // 
            // TruckEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpBuyDate);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.txtCreateName);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtTruckNo);
            this.Controls.Add(this.labCreateByName);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labType);
            this.Controls.Add(this.labBuyDate);
            this.Controls.Add(this.labTruckNo);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "TruckEdit";
            this.Size = new System.Drawing.Size(585, 167);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTruckNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBuyDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBuyDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.LabelControl labTruckNo;
        private DevExpress.XtraEditors.TextEdit txtTruckNo;
        private DevExpress.XtraEditors.LabelControl labBuyDate;
        private DevExpress.XtraEditors.DateEdit dtpBuyDate;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.TextEdit txtType;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TextEdit txtCreateName;
        private DevExpress.XtraEditors.LabelControl labCreateByName;
    }
}
