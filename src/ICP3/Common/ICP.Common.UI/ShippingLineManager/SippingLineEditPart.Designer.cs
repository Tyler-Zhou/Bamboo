namespace ICP.Common.UI.ShippingLineManager
{
    partial class SippingLineEditPart
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.panelEdit = new DevExpress.XtraEditors.PanelControl();
            this.dteCreateDate = new DevExpress.XtraEditors.DateEdit();
            this.bsSippingLine = new System.Windows.Forms.BindingSource(this.components);
            this.txtCreateBy = new DevExpress.XtraEditors.TextEdit();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labCreateDate = new DevExpress.XtraEditors.LabelControl();
            this.labCreateBy = new DevExpress.XtraEditors.LabelControl();
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEdit)).BeginInit();
            this.panelEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSippingLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.SuspendLayout();
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
            this.barManager1.MainMenu = this.bar1;
            this.barManager1.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 2";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 2";
            // 
            // barSave
            // 
            this.barSave.Caption = "保存";
            this.barSave.Glyph = global::ICP.Common.UI.Properties.Resources.filesave16;
            this.barSave.Id = 0;
            this.barSave.Name = "barSave";
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(386, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 207);
            this.barDockControlBottom.Size = new System.Drawing.Size(386, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 181);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(386, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 181);
            // 
            // panelEdit
            // 
            this.panelEdit.Controls.Add(this.dteCreateDate);
            this.panelEdit.Controls.Add(this.txtCreateBy);
            this.panelEdit.Controls.Add(this.txtEName);
            this.panelEdit.Controls.Add(this.txtCName);
            this.panelEdit.Controls.Add(this.txtCode);
            this.panelEdit.Controls.Add(this.labCreateDate);
            this.panelEdit.Controls.Add(this.labCreateBy);
            this.panelEdit.Controls.Add(this.labEName);
            this.panelEdit.Controls.Add(this.labCName);
            this.panelEdit.Controls.Add(this.labCode);
            this.panelEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEdit.Location = new System.Drawing.Point(0, 26);
            this.panelEdit.Name = "panelEdit";
            this.panelEdit.Size = new System.Drawing.Size(386, 181);
            this.panelEdit.TabIndex = 4;
            // 
            // dteCreateDate
            // 
            this.dteCreateDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsSippingLine, "CreateDate", true));
            this.dteCreateDate.EditValue = null;
            this.dteCreateDate.Location = new System.Drawing.Point(85, 118);
            this.dteCreateDate.MenuManager = this.barManager1;
            this.dteCreateDate.Name = "dteCreateDate";
            this.dteCreateDate.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.dteCreateDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteCreateDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteCreateDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteCreateDate.Size = new System.Drawing.Size(193, 21);
            this.dteCreateDate.TabIndex = 36;
            // 
            // bsSippingLine
            // 
            this.bsSippingLine.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.ShippingLineInfo);
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSippingLine, "CreateByName", true));
            this.txtCreateBy.Location = new System.Drawing.Point(85, 84);
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.Properties.ReadOnly = true;
            this.txtCreateBy.Size = new System.Drawing.Size(193, 21);
            this.txtCreateBy.TabIndex = 35;
            this.txtCreateBy.TabStop = false;
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSippingLine, "EName", true));
            this.txtEName.Location = new System.Drawing.Point(85, 57);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 100;
            this.txtEName.Size = new System.Drawing.Size(193, 21);
            this.txtEName.TabIndex = 34;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSippingLine, "CName", true));
            this.txtCName.Location = new System.Drawing.Point(85, 30);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 100;
            this.txtCName.Size = new System.Drawing.Size(193, 21);
            this.txtCName.TabIndex = 33;
            // 
            // txtCode
            // 
            this.txtCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSippingLine, "Code", true));
            this.txtCode.Location = new System.Drawing.Point(85, 3);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.MaxLength = 20;
            this.txtCode.Size = new System.Drawing.Size(193, 21);
            this.txtCode.TabIndex = 32;
            // 
            // labCreateDate
            // 
            this.labCreateDate.Location = new System.Drawing.Point(9, 121);
            this.labCreateDate.Name = "labCreateDate";
            this.labCreateDate.Size = new System.Drawing.Size(66, 14);
            this.labCreateDate.TabIndex = 31;
            this.labCreateDate.Text = "Create Date";
            // 
            // labCreateBy
            // 
            this.labCreateBy.Location = new System.Drawing.Point(9, 87);
            this.labCreateBy.Name = "labCreateBy";
            this.labCreateBy.Size = new System.Drawing.Size(53, 14);
            this.labCreateBy.TabIndex = 30;
            this.labCreateBy.Text = "Create By";
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(9, 60);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(38, 14);
            this.labEName.TabIndex = 29;
            this.labEName.Text = "EName";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(9, 33);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(38, 14);
            this.labCName.TabIndex = 28;
            this.labCName.Text = "CName";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(9, 6);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(28, 14);
            this.labCode.TabIndex = 27;
            this.labCode.Text = "Code";
            // 
            // SippingLineEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEdit);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "SippingLineEditPart";
            this.Size = new System.Drawing.Size(386, 207);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelEdit)).EndInit();
            this.panelEdit.ResumeLayout(false);
            this.panelEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteCreateDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSippingLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateBy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraEditors.PanelControl panelEdit;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labCreateBy;
        private DevExpress.XtraEditors.LabelControl labCreateDate;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCreateBy;
        private DevExpress.XtraEditors.DateEdit dteCreateDate;
        private System.Windows.Forms.BindingSource bsSippingLine;
    }
}
