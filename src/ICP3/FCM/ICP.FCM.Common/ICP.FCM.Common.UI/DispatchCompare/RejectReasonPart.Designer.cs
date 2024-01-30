namespace ICP.FCM.Common.UI.DispatchCompare
{
    partial class RejectReasonPart
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnReject = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl2 = new DevExpress.XtraEditors.LabelControl();
            this.lbl1 = new DevExpress.XtraEditors.LabelControl();
            this.lbl0 = new DevExpress.XtraEditors.LabelControl();
            this.rdReason = new DevExpress.XtraEditors.RadioGroup();
            this.edtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.gboxTo = new System.Windows.Forms.GroupBox();
            this.lblAttnValue = new DevExpress.XtraEditors.LabelControl();
            this.lblAttnText = new DevExpress.XtraEditors.LabelControl();
            this.lblAgentValue = new DevExpress.XtraEditors.LabelControl();
            this.lblAgentText = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdReason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtRemark.Properties)).BeginInit();
            this.gboxTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.AutoHeight = false;
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("应收", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("应付", 2, 1)});
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            this.repositoryItemImageComboBox2.ReadOnly = true;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.ValueChecked = 1;
            this.repositoryItemCheckEdit2.ValueUnchecked = 0;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.tableLayoutPanel1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 317);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(547, 40);
            this.panelControl1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.BtnCancel, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReject, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(543, 36);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // BtnCancel
            // 
            this.BtnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.BtnCancel.Location = new System.Drawing.Point(359, 8);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(8);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(84, 20);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(229, 8);
            this.btnReject.Margin = new System.Windows.Forms.Padding(8);
            this.btnReject.Name = "btnReject";
            this.btnReject.Padding = new System.Windows.Forms.Padding(15);
            this.btnReject.Size = new System.Drawing.Size(84, 20);
            this.btnReject.TabIndex = 2;
            this.btnReject.Text = "Reject";
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl2);
            this.groupBox2.Controls.Add(this.lbl1);
            this.groupBox2.Controls.Add(this.lbl0);
            this.groupBox2.Controls.Add(this.rdReason);
            this.groupBox2.Controls.Add(this.edtRemark);
            this.groupBox2.Location = new System.Drawing.Point(3, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(541, 255);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Select Reason";
            // 
            // lbl2
            // 
            this.lbl2.Location = new System.Drawing.Point(27, 129);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(133, 14);
            this.lbl2.TabIndex = 7;
            this.lbl2.Text = "Write reason by yourself";
            this.lbl2.Click += new System.EventHandler(this.lbl_Click);
            // 
            // lbl1
            // 
            this.lbl1.Location = new System.Drawing.Point(27, 84);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(484, 14);
            this.lbl1.TabIndex = 6;
            this.lbl1.Text = "Sorry for we don\'t accept the new D/C fees, because the new D/C fees are duplicat" +
                "ed.";
            this.lbl1.Click += new System.EventHandler(this.lbl_Click);
            // 
            // lbl0
            // 
            this.lbl0.AllowHtmlString = true;
            this.lbl0.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbl0.Location = new System.Drawing.Point(27, 29);
            this.lbl0.Margin = new System.Windows.Forms.Padding(1);
            this.lbl0.Name = "lbl0";
            this.lbl0.Size = new System.Drawing.Size(496, 48);
            this.lbl0.TabIndex = 5;
            this.lbl0.Text = "Sorry for we don\'t accept the new D/C fees without negotiation. you should negoti" +
                "ate       with the agent via email first before filling-in the D/C fees in ICP.";
            this.lbl0.Click += new System.EventHandler(this.lbl_Click);
            // 
            // rdReason
            // 
            this.rdReason.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.False;
            this.rdReason.EditValue = 0;
            this.rdReason.Location = new System.Drawing.Point(3, 19);
            this.rdReason.Margin = new System.Windows.Forms.Padding(1);
            this.rdReason.Name = "rdReason";
            this.rdReason.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rdReason.Properties.Appearance.Options.UseBackColor = true;
            this.rdReason.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.rdReason.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, ""),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, ""),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(3, "")});
            this.rdReason.Properties.UseParentBackground = true;
            this.rdReason.Size = new System.Drawing.Size(534, 146);
            this.rdReason.TabIndex = 0;
            this.rdReason.SelectedIndexChanged += new System.EventHandler(this.rdReason_SelectedIndexChanged);
            // 
            // edtRemark
            // 
            this.edtRemark.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.edtRemark.Enabled = false;
            this.edtRemark.Location = new System.Drawing.Point(3, 169);
            this.edtRemark.Name = "edtRemark";
            this.edtRemark.Size = new System.Drawing.Size(535, 83);
            this.edtRemark.TabIndex = 1;
            // 
            // gboxTo
            // 
            this.gboxTo.Controls.Add(this.lblAttnValue);
            this.gboxTo.Controls.Add(this.lblAttnText);
            this.gboxTo.Controls.Add(this.lblAgentValue);
            this.gboxTo.Controls.Add(this.lblAgentText);
            this.gboxTo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gboxTo.Location = new System.Drawing.Point(0, 0);
            this.gboxTo.Name = "gboxTo";
            this.gboxTo.Size = new System.Drawing.Size(547, 55);
            this.gboxTo.TabIndex = 14;
            this.gboxTo.TabStop = false;
            this.gboxTo.Text = "Agent Info";
            // 
            // lblAttnValue
            // 
            this.lblAttnValue.Location = new System.Drawing.Point(295, 21);
            this.lblAttnValue.Name = "lblAttnValue";
            this.lblAttnValue.Size = new System.Drawing.Size(66, 14);
            this.lblAttnValue.TabIndex = 11;
            this.lblAttnValue.Text = "lblAttnValue";
            // 
            // lblAttnText
            // 
            this.lblAttnText.Location = new System.Drawing.Point(260, 21);
            this.lblAttnText.Name = "lblAttnText";
            this.lblAttnText.Size = new System.Drawing.Size(29, 14);
            this.lblAttnText.TabIndex = 10;
            this.lblAttnText.Text = "Attn:";
            // 
            // lblAgentValue
            // 
            this.lblAgentValue.Location = new System.Drawing.Point(31, 21);
            this.lblAgentValue.Name = "lblAgentValue";
            this.lblAgentValue.Size = new System.Drawing.Size(75, 14);
            this.lblAgentValue.TabIndex = 9;
            this.lblAgentValue.Text = "lblAgentValue";
            // 
            // lblAgentText
            // 
            this.lblAgentText.Location = new System.Drawing.Point(6, 21);
            this.lblAgentText.Name = "lblAgentText";
            this.lblAgentText.Size = new System.Drawing.Size(19, 14);
            this.lblAgentText.TabIndex = 8;
            this.lblAgentText.Text = "To:";
            // 
            // RejectReasonPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gboxTo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelControl1);
            this.Name = "RejectReasonPart";
            this.Size = new System.Drawing.Size(547, 357);
            this.Load += new System.EventHandler(this.RejectReasonPart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdReason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtRemark.Properties)).EndInit();
            this.gboxTo.ResumeLayout(false);
            this.gboxTo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        // private ICP.Framework.ClientComponents.Controls.LWGridControl gridBill;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        public DevExpress.XtraEditors.SimpleButton btnReject;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.MemoEdit edtRemark;
        private DevExpress.XtraEditors.RadioGroup rdReason;
        private System.Windows.Forms.GroupBox gboxTo;
        private DevExpress.XtraEditors.LabelControl lbl2;
        private DevExpress.XtraEditors.LabelControl lbl1;
        private DevExpress.XtraEditors.LabelControl lbl0;
        private DevExpress.XtraEditors.LabelControl lblAttnValue;
        private DevExpress.XtraEditors.LabelControl lblAttnText;
        private DevExpress.XtraEditors.LabelControl lblAgentValue;
        private DevExpress.XtraEditors.LabelControl lblAgentText;
    }
}
