using ICP.Framework.CommonLibrary.Client;
namespace ICP.Common.UI.CustomerManager
{
    partial class CustomerAuditing
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
            this.labelIsPass = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.labelCode = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.bsDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.labelUser = new DevExpress.XtraEditors.LabelControl();
            this.txtApplyUser = new DevExpress.XtraEditors.TextEdit();
            this.labelDate = new DevExpress.XtraEditors.LabelControl();
            this.txtDate = new DevExpress.XtraEditors.TextEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.linkTip = new DevExpress.XtraEditors.LabelControl();
            this.labelCodeError = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApplyUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelIsPass
            // 
            this.labelIsPass.Location = new System.Drawing.Point(27, 14);
            this.labelIsPass.Name = "labelIsPass";
            this.labelIsPass.Size = new System.Drawing.Size(36, 14);
            this.labelIsPass.TabIndex = 0;
            this.labelIsPass.Text = "Is Pass";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // labelCode
            // 
            this.labelCode.Location = new System.Drawing.Point(27, 51);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(28, 14);
            this.labelCode.TabIndex = 1;
            this.labelCode.Text = "Code";
            // 
            // radioGroup1
            // 
            this.radioGroup1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bsDataSource, "IsAgree", true));
            this.radioGroup1.Location = new System.Drawing.Point(99, 4);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, LocalData.IsEnglish?"Agree":"同意"), //
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false,LocalData.IsEnglish?"Refuse":"拒绝")});
            this.radioGroup1.Size = new System.Drawing.Size(264, 34);
            this.radioGroup1.TabIndex = 2;
            // 
            // bsDataSource
            // 
            this.bsDataSource.DataSource = typeof(ICP.Common.UI.CustomerManager.CustomerConfirmInfoForAuditing);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(99, 48);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(264, 21);
            this.txtCode.TabIndex = 3;
            // 
            // labelUser
            // 
            this.labelUser.Location = new System.Drawing.Point(27, 82);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(58, 14);
            this.labelUser.TabIndex = 4;
            this.labelUser.Text = "Apply User";
            // 
            // txtApplyUser
            // 
            this.txtApplyUser.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "ApplicantName", true));
            this.txtApplyUser.Location = new System.Drawing.Point(99, 79);
            this.txtApplyUser.Name = "txtApplyUser";
            this.txtApplyUser.Properties.ReadOnly = true;
            this.txtApplyUser.Size = new System.Drawing.Size(169, 21);
            this.txtApplyUser.TabIndex = 5;
            // 
            // labelDate
            // 
            this.labelDate.Location = new System.Drawing.Point(312, 82);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(60, 14);
            this.labelDate.TabIndex = 6;
            this.labelDate.Text = "Apply Date";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(392, 79);
            this.txtDate.Name = "txtDate";
            this.txtDate.Properties.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(169, 21);
            this.txtDate.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Location = new System.Drawing.Point(14, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(546, 127);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsDataSource, "ApplicantRemark", true));
            this.txtRemark.Location = new System.Drawing.Point(7, 21);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(532, 98);
            this.txtRemark.TabIndex = 0;
            // 
            // linkTip
            // 
            this.linkTip.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Underline);
            this.linkTip.Appearance.ForeColor = System.Drawing.Color.Red;
            this.linkTip.Appearance.Options.UseFont = true;
            this.linkTip.Appearance.Options.UseForeColor = true;
            this.linkTip.Location = new System.Drawing.Point(392, 14);
            this.linkTip.Name = "linkTip";
            this.linkTip.Size = new System.Drawing.Size(70, 14);
            this.linkTip.TabIndex = 10;
            this.linkTip.Text = "labelControl5";
            this.linkTip.Click += new System.EventHandler(this.linkTip_Click);
            // 
            // labelCodeError
            // 
            this.labelCodeError.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelCodeError.Appearance.Options.UseForeColor = true;
            this.labelCodeError.Location = new System.Drawing.Point(392, 51);
            this.labelCodeError.Name = "labelCodeError";
            this.labelCodeError.Size = new System.Drawing.Size(78, 14);
            this.labelCodeError.TabIndex = 14;
            this.labelCodeError.Text = "labelCodeError";
            this.labelCodeError.Visible = false;
            this.labelCodeError.Click += new System.EventHandler(this.labelCodeError_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(392, 250);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 12;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(486, 250);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CustomerAuditing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 284);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.linkTip);
            this.Controls.Add(this.labelCodeError);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.labelDate);
            this.Controls.Add(this.txtApplyUser);
            this.Controls.Add(this.labelUser);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.labelCode);
            this.Controls.Add(this.labelIsPass);
            this.Name = "CustomerAuditing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerAuditing";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtApplyUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelIsPass;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider1;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.LabelControl labelCode;
        private DevExpress.XtraEditors.TextEdit txtDate;
        private DevExpress.XtraEditors.LabelControl labelDate;
        private DevExpress.XtraEditors.TextEdit txtApplyUser;
        private DevExpress.XtraEditors.LabelControl labelUser;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl linkTip;
        private DevExpress.XtraEditors.LabelControl labelCodeError;
        private System.Windows.Forms.BindingSource bsDataSource;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
    }
}