namespace ICP.Business.Common.UI.EventList
{
    partial class EventEditPart
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
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.chkShowAgent = new DevExpress.XtraEditors.CheckEdit();
            this.chkWhowCustomer = new DevExpress.XtraEditors.CheckEdit();
            this.lblDescription = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCan = new DevExpress.XtraEditors.SimpleButton();
            this.txtDescription = new DevExpress.XtraEditors.MemoEdit();
            this.errorValidate = new System.Windows.Forms.ErrorProvider();
            this.cmbCodeSubject = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labelOccurrenceTime = new DevExpress.XtraEditors.LabelControl();
            this.dteOccurrenceTime = new DevExpress.XtraEditors.DateEdit();
            this.checkImportant = new DevExpress.XtraEditors.CheckEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWhowCustomer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorValidate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOccurrenceTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOccurrenceTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkImportant.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(26, 10);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 0;
            this.labCode.Text = "主题";
            // 
            // chkShowAgent
            // 
            this.chkShowAgent.Location = new System.Drawing.Point(104, 62);
            this.chkShowAgent.Name = "chkShowAgent";
            this.chkShowAgent.Properties.Caption = "是否显示给代理";
            this.chkShowAgent.Size = new System.Drawing.Size(430, 19);
            this.chkShowAgent.TabIndex = 4;
            // 
            // chkWhowCustomer
            // 
            this.chkWhowCustomer.Location = new System.Drawing.Point(104, 87);
            this.chkWhowCustomer.Name = "chkWhowCustomer";
            this.chkWhowCustomer.Properties.Caption = "是否显示给客户";
            this.chkWhowCustomer.Size = new System.Drawing.Size(430, 19);
            this.chkWhowCustomer.TabIndex = 5;
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(26, 140);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(24, 14);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "描述";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(366, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCan
            // 
            this.btnCan.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCan.Location = new System.Drawing.Point(465, 4);
            this.btnCan.Name = "btnCan";
            this.btnCan.Size = new System.Drawing.Size(75, 23);
            this.btnCan.TabIndex = 9;
            this.btnCan.Text = "返回(&C)";
            // 
            // txtDescription
            // 
            this.errorValidate.SetIconAlignment(this.txtDescription, System.Windows.Forms.ErrorIconAlignment.TopLeft);
            this.txtDescription.Location = new System.Drawing.Point(106, 137);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(428, 172);
            this.txtDescription.TabIndex = 7;
            this.txtDescription.TabStop = false;
            // 
            // errorValidate
            // 
            this.errorValidate.ContainerControl = this;
            // 
            // cmbCodeSubject
            // 
            this.cmbCodeSubject.EditText = "";
            this.cmbCodeSubject.EditValue = null;
            this.errorValidate.SetIconAlignment(this.cmbCodeSubject, System.Windows.Forms.ErrorIconAlignment.TopLeft);
            this.cmbCodeSubject.Location = new System.Drawing.Point(106, 10);
            this.cmbCodeSubject.Name = "cmbCodeSubject";
            this.cmbCodeSubject.ReadOnly = false;
            this.cmbCodeSubject.RefreshButtonToolTip = "";
            this.cmbCodeSubject.ShowRefreshButton = false;
            this.cmbCodeSubject.Size = new System.Drawing.Size(428, 21);
            this.cmbCodeSubject.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCodeSubject.TabIndex = 16;
            this.cmbCodeSubject.ToolTip = "";
            // 
            // labelOccurrenceTime
            // 
            this.labelOccurrenceTime.Location = new System.Drawing.Point(26, 40);
            this.labelOccurrenceTime.Name = "labelOccurrenceTime";
            this.labelOccurrenceTime.Size = new System.Drawing.Size(48, 14);
            this.labelOccurrenceTime.TabIndex = 19;
            this.labelOccurrenceTime.Text = "发生时间";
            // 
            // dteOccurrenceTime
            // 
            this.dteOccurrenceTime.EditValue = null;
            this.dteOccurrenceTime.Location = new System.Drawing.Point(106, 37);
            this.dteOccurrenceTime.Name = "dteOccurrenceTime";
            this.dteOccurrenceTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteOccurrenceTime.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dteOccurrenceTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteOccurrenceTime.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dteOccurrenceTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dteOccurrenceTime.Properties.Mask.EditMask = "";
            this.dteOccurrenceTime.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteOccurrenceTime.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteOccurrenceTime.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dteOccurrenceTime.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dteOccurrenceTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteOccurrenceTime.Size = new System.Drawing.Size(241, 21);
            this.dteOccurrenceTime.TabIndex = 651;
            // 
            // checkImportant
            // 
            this.checkImportant.Location = new System.Drawing.Point(104, 112);
            this.checkImportant.Name = "checkImportant";
            this.checkImportant.Properties.Caption = "是否重要";
            this.checkImportant.Size = new System.Drawing.Size(430, 19);
            this.checkImportant.TabIndex = 652;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 358);
            this.panel1.TabIndex = 653;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnCan);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 327);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(578, 31);
            this.panel3.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.labelOccurrenceTime);
            this.panel2.Controls.Add(this.checkImportant);
            this.panel2.Controls.Add(this.txtDescription);
            this.panel2.Controls.Add(this.dteOccurrenceTime);
            this.panel2.Controls.Add(this.labCode);
            this.panel2.Controls.Add(this.chkShowAgent);
            this.panel2.Controls.Add(this.cmbCodeSubject);
            this.panel2.Controls.Add(this.chkWhowCustomer);
            this.panel2.Controls.Add(this.lblDescription);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(578, 358);
            this.panel2.TabIndex = 0;
            // 
            // EventEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "EventEditPart";
            this.Size = new System.Drawing.Size(578, 358);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWhowCustomer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorValidate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOccurrenceTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteOccurrenceTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkImportant.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.CheckEdit chkShowAgent;
        private DevExpress.XtraEditors.CheckEdit chkWhowCustomer;
        private DevExpress.XtraEditors.LabelControl lblDescription;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCan;
        private DevExpress.XtraEditors.MemoEdit txtDescription;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox cmbCodeSubject;
        private System.Windows.Forms.ErrorProvider errorValidate;
        private DevExpress.XtraEditors.LabelControl labelOccurrenceTime;
        private DevExpress.XtraEditors.DateEdit dteOccurrenceTime;
        private DevExpress.XtraEditors.CheckEdit checkImportant;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}