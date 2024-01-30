namespace ICP.FAM.UI.VerificationSheet
{
    partial class VerificationSheetEditPart
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
            this.bsVerifiSheet = new System.Windows.Forms.BindingSource(this.components);
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labSheetNo = new DevExpress.XtraEditors.LabelControl();
            this.dteReceiptDate = new DevExpress.XtraEditors.DateEdit();
            this.labReceiptDate = new DevExpress.XtraEditors.LabelControl();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labReturnDate = new DevExpress.XtraEditors.LabelControl();
            this.dteReturnDate = new DevExpress.XtraEditors.DateEdit();
            this.chkIsFreightArrive = new DevExpress.XtraEditors.CheckEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.txtCustomer = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtOperationNo = new DevExpress.XtraEditors.ButtonEdit();
            this.labOperationNo = new DevExpress.XtraEditors.LabelControl();
            this.txtSheetNo = new DevExpress.XtraEditors.TextEdit();
            this.txtExpressNO = new DevExpress.XtraEditors.TextEdit();
            this.labExpressNO = new DevExpress.XtraEditors.LabelControl();
            this.labCustomer = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsVerifiSheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceiptDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceiptDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFreightArrive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOperationNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNO.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bsVerifiSheet
            // 
            this.bsVerifiSheet.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.VerificationSheet);
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVerifiSheet, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(95, 221);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(302, 63);
            this.txtRemark.TabIndex = 6;
            // 
            // labSheetNo
            // 
            this.labSheetNo.Location = new System.Drawing.Point(12, 35);
            this.labSheetNo.Name = "labSheetNo";
            this.labSheetNo.Size = new System.Drawing.Size(48, 14);
            this.labSheetNo.TabIndex = 33;
            this.labSheetNo.Text = "SheetNo";
            // 
            // dteReceiptDate
            // 
            this.dteReceiptDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsVerifiSheet, "ReceiptDate", true));
            this.dteReceiptDate.EditValue = null;
            this.dteReceiptDate.Location = new System.Drawing.Point(95, 136);
            this.dteReceiptDate.Name = "dteReceiptDate";
            this.dteReceiptDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteReceiptDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReceiptDate.Size = new System.Drawing.Size(302, 21);
            this.dteReceiptDate.TabIndex = 3;
            // 
            // labReceiptDate
            // 
            this.labReceiptDate.Location = new System.Drawing.Point(12, 139);
            this.labReceiptDate.Name = "labReceiptDate";
            this.labReceiptDate.Size = new System.Drawing.Size(67, 14);
            this.labReceiptDate.TabIndex = 40;
            this.labReceiptDate.Text = "ReceiptDate";
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(12, 222);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 41;
            this.labRemark.Text = "Remark";
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
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(745, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 290);
            this.barDockControlBottom.Size = new System.Drawing.Size(745, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 264);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(745, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 264);
            // 
            // labReturnDate
            // 
            this.labReturnDate.Location = new System.Drawing.Point(12, 165);
            this.labReturnDate.Name = "labReturnDate";
            this.labReturnDate.Size = new System.Drawing.Size(63, 14);
            this.labReturnDate.TabIndex = 351;
            this.labReturnDate.Text = "ReturnDate";
            // 
            // dteReturnDate
            // 
            this.dteReturnDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsVerifiSheet, "ReturnDate", true));
            this.dteReturnDate.EditValue = null;
            this.dteReturnDate.Location = new System.Drawing.Point(95, 162);
            this.dteReturnDate.Name = "dteReturnDate";
            this.dteReturnDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteReturnDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteReturnDate.Size = new System.Drawing.Size(302, 21);
            this.dteReturnDate.TabIndex = 4;
            // 
            // chkIsFreightArrive
            // 
            this.chkIsFreightArrive.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsVerifiSheet, "IsFreightArrive", true));
            this.chkIsFreightArrive.Location = new System.Drawing.Point(12, 191);
            this.chkIsFreightArrive.MenuManager = this.barManager1;
            this.chkIsFreightArrive.Name = "chkIsFreightArrive";
            this.chkIsFreightArrive.Properties.Caption = "IsFreightArrive";
            this.chkIsFreightArrive.Size = new System.Drawing.Size(136, 19);
            this.chkIsFreightArrive.TabIndex = 5;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // txtCustomer
            // 
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsVerifiSheet, "CustomerId", true));
            this.txtCustomer.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVerifiSheet, "CustomerName", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtCustomer, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCustomer.Location = new System.Drawing.Point(95, 110);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtCustomer.Size = new System.Drawing.Size(302, 21);
            this.txtCustomer.TabIndex = 366;
            this.txtCustomer.TabStop = false;
            // 
            // stxtOperationNo
            // 
            this.stxtOperationNo.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bsVerifiSheet, "OperationId", true));
            this.stxtOperationNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVerifiSheet, "OperationNo", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtOperationNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtOperationNo.Location = new System.Drawing.Point(95, 58);
            this.stxtOperationNo.Name = "stxtOperationNo";
            this.stxtOperationNo.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.stxtOperationNo.Properties.Appearance.Options.UseBackColor = true;
            this.stxtOperationNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtOperationNo.Size = new System.Drawing.Size(302, 21);
            this.stxtOperationNo.TabIndex = 372;
            // 
            // labOperationNo
            // 
            this.labOperationNo.Location = new System.Drawing.Point(12, 61);
            this.labOperationNo.Name = "labOperationNo";
            this.labOperationNo.Size = new System.Drawing.Size(69, 14);
            this.labOperationNo.TabIndex = 357;
            this.labOperationNo.Text = "OperationNo";
            // 
            // txtSheetNo
            // 
            this.txtSheetNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVerifiSheet, "SheetNo", true));
            this.txtSheetNo.Location = new System.Drawing.Point(95, 32);
            this.txtSheetNo.MenuManager = this.barManager1;
            this.txtSheetNo.Name = "txtSheetNo";
            this.txtSheetNo.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.txtSheetNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtSheetNo.Size = new System.Drawing.Size(302, 21);
            this.txtSheetNo.TabIndex = 362;
            // 
            // txtExpressNO
            // 
            this.txtExpressNO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVerifiSheet, "ExpressNO", true));
            this.txtExpressNO.Location = new System.Drawing.Point(95, 84);
            this.txtExpressNO.MenuManager = this.barManager1;
            this.txtExpressNO.Name = "txtExpressNO";
            this.txtExpressNO.Size = new System.Drawing.Size(302, 21);
            this.txtExpressNO.TabIndex = 365;
            // 
            // labExpressNO
            // 
            this.labExpressNO.Location = new System.Drawing.Point(12, 87);
            this.labExpressNO.Name = "labExpressNO";
            this.labExpressNO.Size = new System.Drawing.Size(58, 14);
            this.labExpressNO.TabIndex = 364;
            this.labExpressNO.Text = "ExpressNO";
            // 
            // labCustomer
            // 
            this.labCustomer.Location = new System.Drawing.Point(12, 113);
            this.labCustomer.Name = "labCustomer";
            this.labCustomer.Size = new System.Drawing.Size(52, 14);
            this.labCustomer.TabIndex = 367;
            this.labCustomer.Text = "Customer";
            // 
            // VerificationSheetEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stxtOperationNo);
            this.Controls.Add(this.labCustomer);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.txtExpressNO);
            this.Controls.Add(this.labExpressNO);
            this.Controls.Add(this.txtSheetNo);
            this.Controls.Add(this.labOperationNo);
            this.Controls.Add(this.chkIsFreightArrive);
            this.Controls.Add(this.labReturnDate);
            this.Controls.Add(this.dteReturnDate);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labReceiptDate);
            this.Controls.Add(this.dteReceiptDate);
            this.Controls.Add(this.labSheetNo);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "VerificationSheetEditPart";
            this.Size = new System.Drawing.Size(745, 290);
            ((System.ComponentModel.ISupportInitialize)(this.bsVerifiSheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceiptDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReceiptDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteReturnDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsFreightArrive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtOperationNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSheetNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNO.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labSheetNo;
        private DevExpress.XtraEditors.DateEdit dteReceiptDate;
        private DevExpress.XtraEditors.LabelControl labReceiptDate;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private System.Windows.Forms.BindingSource bsVerifiSheet;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.LabelControl labReturnDate;
        private DevExpress.XtraEditors.DateEdit dteReturnDate;
        private DevExpress.XtraEditors.CheckEdit chkIsFreightArrive;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.LabelControl labOperationNo;
        private DevExpress.XtraEditors.TextEdit txtSheetNo;
        private DevExpress.XtraEditors.TextEdit txtExpressNO;
        private DevExpress.XtraEditors.LabelControl labExpressNO;
        private DevExpress.XtraEditors.LabelControl labCustomer;
        private DevExpress.XtraEditors.ButtonEdit txtCustomer;
        private DevExpress.XtraEditors.ButtonEdit stxtOperationNo;
    }
}

