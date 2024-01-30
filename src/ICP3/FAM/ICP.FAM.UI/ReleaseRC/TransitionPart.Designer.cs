namespace ICP.FAM.UI
{
    partial class TransitionPart
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
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labExpressDate = new DevExpress.XtraEditors.LabelControl();
            this.txtExpressNo = new DevExpress.XtraEditors.TextEdit();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.labConsignee = new DevExpress.XtraEditors.LabelControl();
            this.txtInvoiceNo = new DevExpress.XtraEditors.TextEdit();
            this.labBLNo = new DevExpress.XtraEditors.LabelControl();
            this.cmbCompanyID = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(285, 87);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Cancel";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(210, 87);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cmbCompanyID);
            this.panelControl1.Controls.Add(this.textEdit3);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labExpressDate);
            this.panelControl1.Controls.Add(this.txtExpressNo);
            this.panelControl1.Controls.Add(this.labConsignee);
            this.panelControl1.Controls.Add(this.txtInvoiceNo);
            this.panelControl1.Controls.Add(this.labBLNo);
            this.panelControl1.Location = new System.Drawing.Point(14, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(573, 78);
            this.panelControl1.TabIndex = 3;
            // 
            // textEdit3
            // 
            this.textEdit3.Enabled = false;
            this.textEdit3.Location = new System.Drawing.Point(389, 5);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.textEdit3.Properties.Appearance.Options.UseBackColor = true;
            this.textEdit3.Properties.ReadOnly = true;
            this.textEdit3.Size = new System.Drawing.Size(164, 21);
            this.textEdit3.TabIndex = 24;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(271, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(112, 14);
            this.labelControl3.TabIndex = 25;
            this.labelControl3.Text = "RcCompany(Original)";
            // 
            // labExpressDate
            // 
            this.labExpressDate.Location = new System.Drawing.Point(271, 35);
            this.labExpressDate.Name = "labExpressDate";
            this.labExpressDate.Size = new System.Drawing.Size(98, 14);
            this.labExpressDate.TabIndex = 17;
            this.labExpressDate.Text = "RcCompany(New)";
            // 
            // txtExpressNo
            // 
            this.txtExpressNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "ConsigneeName", true));
            this.txtExpressNo.Enabled = false;
            this.txtExpressNo.Location = new System.Drawing.Point(85, 32);
            this.txtExpressNo.Name = "txtExpressNo";
            this.txtExpressNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtExpressNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtExpressNo.Properties.ReadOnly = true;
            this.txtExpressNo.Size = new System.Drawing.Size(164, 21);
            this.txtExpressNo.TabIndex = 1;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.ReleaseRCList);
            // 
            // labConsignee
            // 
            this.labConsignee.Location = new System.Drawing.Point(15, 35);
            this.labConsignee.Name = "labConsignee";
            this.labConsignee.Size = new System.Drawing.Size(56, 14);
            this.labConsignee.TabIndex = 1;
            this.labConsignee.Text = "Consignee";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "BlNo", true));
            this.txtInvoiceNo.Enabled = false;
            this.txtInvoiceNo.Location = new System.Drawing.Point(85, 5);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtInvoiceNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtInvoiceNo.Properties.ReadOnly = true;
            this.txtInvoiceNo.Size = new System.Drawing.Size(164, 21);
            this.txtInvoiceNo.TabIndex = 0;
            // 
            // labBLNo
            // 
            this.labBLNo.Location = new System.Drawing.Point(15, 8);
            this.labBLNo.Name = "labBLNo";
            this.labBLNo.Size = new System.Drawing.Size(28, 14);
            this.labBLNo.TabIndex = 1;
            this.labBLNo.Text = "BLNo";
            // 
            // cmbCompanyID
            // 
            this.cmbCompanyID.Location = new System.Drawing.Point(389, 32);
            this.cmbCompanyID.Name = "cmbCompanyID";
            this.cmbCompanyID.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompanyID.Size = new System.Drawing.Size(164, 21);
            this.cmbCompanyID.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompanyID.TabIndex = 26;
            // 
            // TransitionPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelControl1);
            this.Name = "TransitionPart";
            this.Size = new System.Drawing.Size(601, 118);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompanyID.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labExpressDate;
        private DevExpress.XtraEditors.TextEdit txtExpressNo;
        private DevExpress.XtraEditors.LabelControl labConsignee;
        private DevExpress.XtraEditors.TextEdit txtInvoiceNo;
        private DevExpress.XtraEditors.LabelControl labBLNo;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.BindingSource bsList;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompanyID;
    }
}
