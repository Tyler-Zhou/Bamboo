namespace ICP.FAM.UI
{
    partial class SetExpressPart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dteExpressDate = new DevExpress.XtraEditors.DateEdit();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.labExpressDate = new DevExpress.XtraEditors.LabelControl();
            this.txtExpressNo = new DevExpress.XtraEditors.TextEdit();
            this.labExpressNo = new DevExpress.XtraEditors.LabelControl();
            this.txtInvoiceNo = new DevExpress.XtraEditors.TextEdit();
            this.labInvoiceNo = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpressDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpressDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.dteExpressDate);
            this.panelControl1.Controls.Add(this.labExpressDate);
            this.panelControl1.Controls.Add(this.txtExpressNo);
            this.panelControl1.Controls.Add(this.labExpressNo);
            this.panelControl1.Controls.Add(this.txtInvoiceNo);
            this.panelControl1.Controls.Add(this.labInvoiceNo);
            this.panelControl1.Location = new System.Drawing.Point(12, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(295, 90);
            this.panelControl1.TabIndex = 0;
            // 
            // dteExpressDate
            // 
            this.dteExpressDate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsList, "ExpressDate", true));
            this.dteExpressDate.EditValue = null;
            this.dteExpressDate.Location = new System.Drawing.Point(101, 59);
            this.dteExpressDate.Name = "dteExpressDate";
            this.dteExpressDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dteExpressDate.Properties.Appearance.Options.UseBackColor = true;
            this.dteExpressDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteExpressDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteExpressDate.Size = new System.Drawing.Size(164, 21);
            this.dteExpressDate.TabIndex = 2;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.InvoiceList);
            // 
            // labExpressDate
            // 
            this.labExpressDate.Location = new System.Drawing.Point(31, 62);
            this.labExpressDate.Name = "labExpressDate";
            this.labExpressDate.Size = new System.Drawing.Size(67, 14);
            this.labExpressDate.TabIndex = 17;
            this.labExpressDate.Text = "ExpressDate";
            // 
            // txtExpressNo
            // 
            this.txtExpressNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "ExpressNo", true));
            this.txtExpressNo.Location = new System.Drawing.Point(101, 32);
            this.txtExpressNo.Name = "txtExpressNo";
            this.txtExpressNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtExpressNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtExpressNo.Size = new System.Drawing.Size(164, 21);
            this.txtExpressNo.TabIndex = 1;
            // 
            // labExpressNo
            // 
            this.labExpressNo.Location = new System.Drawing.Point(31, 35);
            this.labExpressNo.Name = "labExpressNo";
            this.labExpressNo.Size = new System.Drawing.Size(56, 14);
            this.labExpressNo.TabIndex = 1;
            this.labExpressNo.Text = "ExpressNo";
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "InvoiceNo", true));
            this.txtInvoiceNo.Enabled = false;
            this.txtInvoiceNo.Location = new System.Drawing.Point(101, 5);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtInvoiceNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtInvoiceNo.Properties.ReadOnly = true;
            this.txtInvoiceNo.Size = new System.Drawing.Size(164, 21);
            this.txtInvoiceNo.TabIndex = 0;
            // 
            // labInvoiceNo
            // 
            this.labInvoiceNo.Location = new System.Drawing.Point(31, 8);
            this.labInvoiceNo.Name = "labInvoiceNo";
            this.labInvoiceNo.Size = new System.Drawing.Size(54, 14);
            this.labInvoiceNo.TabIndex = 1;
            this.labInvoiceNo.Text = "InvoiceNo";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(113, 99);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(211, 99);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Cancel";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // SetExpressPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panelControl1);
            this.Name = "SetExpressPart";
            this.Size = new System.Drawing.Size(322, 133);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpressDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteExpressDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExpressNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInvoiceNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.TextEdit txtInvoiceNo;
        private DevExpress.XtraEditors.LabelControl labInvoiceNo;
        private DevExpress.XtraEditors.TextEdit txtExpressNo;
        private DevExpress.XtraEditors.LabelControl labExpressNo;
        private DevExpress.XtraEditors.DateEdit dteExpressDate;
        private DevExpress.XtraEditors.LabelControl labExpressDate;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;

    }
}