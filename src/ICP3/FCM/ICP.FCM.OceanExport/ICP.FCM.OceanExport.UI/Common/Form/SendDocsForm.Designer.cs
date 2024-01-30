namespace ICP.FCM.OceanExport.UI.Common.Form
{
    partial class SendDocsForm
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
            this.gpbDocs = new System.Windows.Forms.GroupBox();
            this.ucDocumentList1 = new ICP.Business.Common.UI.Document.UCDocumentList();
            this.gpbSend = new System.Windows.Forms.GroupBox();
            this.rdoAgent = new System.Windows.Forms.RadioButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.rdoOverseas = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtAgent = new DevExpress.XtraEditors.TextEdit();
            this.ckeBelong = new DevExpress.XtraEditors.CheckEdit();
            this.lblAgent = new DevExpress.XtraEditors.LabelControl();
            this.gpbDocs.SuspendLayout();
            this.gpbSend.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckeBelong.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbDocs
            // 
            this.gpbDocs.Controls.Add(this.ucDocumentList1);
            this.gpbDocs.Location = new System.Drawing.Point(18, 171);
            this.gpbDocs.Name = "gpbDocs";
            this.gpbDocs.Size = new System.Drawing.Size(624, 185);
            this.gpbDocs.TabIndex = 14;
            this.gpbDocs.TabStop = false;
            this.gpbDocs.Text = "Docs will be sent";
            // 
            // ucDocumentList1
            // 
            this.ucDocumentList1.DataSource = null;
            this.ucDocumentList1.Dock = System.Windows.Forms.DockStyle.Fill;
            
            this.ucDocumentList1.Location = new System.Drawing.Point(3, 18);
            this.ucDocumentList1.Name = "ucDocumentList1";
            
            this.ucDocumentList1.Size = new System.Drawing.Size(618, 164);
            this.ucDocumentList1.TabIndex = 9;
            this.ucDocumentList1.WorkItem = null;
            // 
            // gpbSend
            // 
            this.gpbSend.Controls.Add(this.rdoAgent);
            this.gpbSend.Controls.Add(this.textEdit1);
            this.gpbSend.Controls.Add(this.listBox1);
            this.gpbSend.Controls.Add(this.rdoOverseas);
            this.gpbSend.Location = new System.Drawing.Point(18, 58);
            this.gpbSend.Name = "gpbSend";
            this.gpbSend.Size = new System.Drawing.Size(624, 95);
            this.gpbSend.TabIndex = 13;
            this.gpbSend.TabStop = false;
            this.gpbSend.Text = "Send Docs to Agent/CS";
            // 
            // rdoAgent
            // 
            this.rdoAgent.AutoSize = true;
            this.rdoAgent.Location = new System.Drawing.Point(16, 31);
            this.rdoAgent.Name = "rdoAgent";
            this.rdoAgent.Size = new System.Drawing.Size(107, 18);
            this.rdoAgent.TabIndex = 3;
            this.rdoAgent.TabStop = true;
            this.rdoAgent.Text = "Send to Agent";
            this.rdoAgent.UseVisualStyleBackColor = true;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(31, 64);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(189, 21);
            this.textEdit1.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(338, 67);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(182, 18);
            this.listBox1.TabIndex = 5;
            // 
            // rdoOverseas
            // 
            this.rdoOverseas.AutoSize = true;
            this.rdoOverseas.Location = new System.Drawing.Point(317, 31);
            this.rdoOverseas.Name = "rdoOverseas";
            this.rdoOverseas.Size = new System.Drawing.Size(140, 18);
            this.rdoOverseas.TabIndex = 3;
            this.rdoOverseas.TabStop = true;
            this.rdoOverseas.Text = "Send to Overseas CS";
            this.rdoOverseas.UseVisualStyleBackColor = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(275, 378);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 12;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // txtAgent
            // 
            this.txtAgent.Enabled = false;
            this.txtAgent.Location = new System.Drawing.Point(75, 25);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Size = new System.Drawing.Size(189, 21);
            this.txtAgent.TabIndex = 11;
            // 
            // ckeBelong
            // 
            this.ckeBelong.Location = new System.Drawing.Point(284, 26);
            this.ckeBelong.Name = "ckeBelong";
            this.ckeBelong.Properties.Caption = "Belong  to City Ocean";
            this.ckeBelong.Size = new System.Drawing.Size(147, 19);
            this.ckeBelong.TabIndex = 10;
            // 
            // lblAgent
            // 
            this.lblAgent.Location = new System.Drawing.Point(26, 28);
            this.lblAgent.Name = "lblAgent";
            this.lblAgent.Size = new System.Drawing.Size(34, 14);
            this.lblAgent.TabIndex = 9;
            this.lblAgent.Text = "Agent";
            // 
            // SendDocsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 418);
            this.Controls.Add(this.gpbDocs);
            this.Controls.Add(this.gpbSend);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtAgent);
            this.Controls.Add(this.ckeBelong);
            this.Controls.Add(this.lblAgent);
            this.Name = "SendDocsForm";
            this.Text = "SendDocs";
            this.gpbDocs.ResumeLayout(false);
            this.gpbSend.ResumeLayout(false);
            this.gpbSend.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckeBelong.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbDocs;
        private ICP.Business.Common.UI.Document.UCDocumentList ucDocumentList1;
        private System.Windows.Forms.GroupBox gpbSend;
        private System.Windows.Forms.RadioButton rdoAgent;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.RadioButton rdoOverseas;
        private System.Windows.Forms.Button btnSubmit;
        private DevExpress.XtraEditors.TextEdit txtAgent;
        private DevExpress.XtraEditors.CheckEdit ckeBelong;
        private DevExpress.XtraEditors.LabelControl lblAgent;
    }
}