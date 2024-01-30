namespace ICP.Common.UI.Configure.Solution
{
    partial class SolutionEditPart
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labEName = new DevExpress.XtraEditors.LabelControl();
            this.labCName = new DevExpress.XtraEditors.LabelControl();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.labInvoiceDate = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.cmbInvoiceDateType = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkAccountShare = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoiceDateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccountShare.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.SolutionInfo);
            // 
            // labEName
            // 
            this.labEName.Location = new System.Drawing.Point(4, 26);
            this.labEName.Name = "labEName";
            this.labEName.Size = new System.Drawing.Size(38, 14);
            this.labEName.TabIndex = 19;
            this.labEName.Text = "EName";
            // 
            // labCName
            // 
            this.labCName.Location = new System.Drawing.Point(4, 5);
            this.labCName.Name = "labCName";
            this.labCName.Size = new System.Drawing.Size(38, 14);
            this.labCName.TabIndex = 18;
            this.labCName.Text = "CName";
            // 
            // txtEName
            // 
            this.txtEName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "EName", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtEName.Location = new System.Drawing.Point(89, 23);
            this.txtEName.Name = "txtEName";
            this.txtEName.Properties.MaxLength = 50;
            this.txtEName.Size = new System.Drawing.Size(213, 21);
            this.txtEName.TabIndex = 1;
            // 
            // txtCName
            // 
            this.txtCName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "CName", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtCName.Location = new System.Drawing.Point(89, 3);
            this.txtCName.Name = "txtCName";
            this.txtCName.Properties.MaxLength = 50;
            this.txtCName.Size = new System.Drawing.Size(213, 21);
            this.txtCName.TabIndex = 0;
            // 
            // labInvoiceDate
            // 
            this.labInvoiceDate.Location = new System.Drawing.Point(4, 46);
            this.labInvoiceDate.Name = "labInvoiceDate";
            this.labInvoiceDate.Size = new System.Drawing.Size(69, 14);
            this.labInvoiceDate.TabIndex = 23;
            this.labInvoiceDate.Text = "Invoice Date";
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Remark", true));
            this.dxErrorProvider1.SetIconAlignment(this.txtRemark, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtRemark.Location = new System.Drawing.Point(89, 85);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.MaxLength = 2000;
            this.txtRemark.Size = new System.Drawing.Size(213, 75);
            this.txtRemark.TabIndex = 4;
            // 
            // cmbInvoiceDateType
            // 
            this.cmbInvoiceDateType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "InvoiceDateType", true));
            this.dxErrorProvider1.SetIconAlignment(this.cmbInvoiceDateType, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cmbInvoiceDateType.Location = new System.Drawing.Point(89, 44);
            this.cmbInvoiceDateType.MenuManager = this.barManager2;
            this.cmbInvoiceDateType.Name = "cmbInvoiceDateType";
            this.cmbInvoiceDateType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbInvoiceDateType.Size = new System.Drawing.Size(213, 21);
            this.cmbInvoiceDateType.TabIndex = 2;
            // 
            // barManager2
            // 
            this.barManager2.DockControls.Add(this.barDockControlTop);
            this.barManager2.DockControls.Add(this.barDockControlBottom);
            this.barManager2.DockControls.Add(this.barDockControlLeft);
            this.barManager2.DockControls.Add(this.barDockControlRight);
            this.barManager2.Form = this;
            this.barManager2.MaxItemId = 0;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(309, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 176);
            this.barDockControlBottom.Size = new System.Drawing.Size(309, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 176);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(309, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 176);
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(4, 85);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 63;
            this.labRemark.Text = "Remark";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(4, 68);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(81, 14);
            this.labelControl1.TabIndex = 64;
            this.labelControl1.Text = "Account Share";
            // 
            // chkAccountShare
            // 
            this.chkAccountShare.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "IsAccountShare", true));
            this.chkAccountShare.Location = new System.Drawing.Point(87, 66);
            this.chkAccountShare.Name = "chkAccountShare";
            this.chkAccountShare.Properties.AllowFocused = false;
            this.chkAccountShare.Properties.Caption = "";
            this.chkAccountShare.Size = new System.Drawing.Size(24, 19);
            this.chkAccountShare.TabIndex = 3;
            // 
            // SolutionEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.chkAccountShare);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbInvoiceDateType);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labInvoiceDate);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labEName);
            this.Controls.Add(this.labCName);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.txtCName);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "SolutionEditPart";
            this.Size = new System.Drawing.Size(309, 176);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbInvoiceDateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAccountShare.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraEditors.LabelControl labEName;
        private DevExpress.XtraEditors.LabelControl labCName;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.LabelControl labInvoiceDate;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbInvoiceDateType;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkAccountShare;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
