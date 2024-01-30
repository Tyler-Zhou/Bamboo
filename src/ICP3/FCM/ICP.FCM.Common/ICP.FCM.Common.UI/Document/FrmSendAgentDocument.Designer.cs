namespace ICP.FCM.Common.UI.Document
{
    partial class FrmSendAgentDocument
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
            this.bdsOverseas = new System.Windows.Forms.BindingSource();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.btnColse = new DevExpress.XtraEditors.SimpleButton();
            this.lblErr = new DevExpress.XtraEditors.LabelControl();
            this.depValidate = new System.Windows.Forms.ErrorProvider();
            this.DocumentDispatchContainer = new DevExpress.XtraEditors.PanelControl();
            this.grbAgentType = new System.Windows.Forms.GroupBox();
            this.txtAgent = new DevExpress.XtraEditors.TextEdit();
            this.lblAgent = new DevExpress.XtraEditors.LabelControl();
            this.ckeBelong = new DevExpress.XtraEditors.CheckEdit();
            this.grbDispatch = new System.Windows.Forms.GroupBox();
            this.lstboxOverseas = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.rdoOverseas = new System.Windows.Forms.RadioButton();
            this.rdoAgent = new System.Windows.Forms.RadioButton();
            this.txtSendAgent = new DevExpress.XtraEditors.TextEdit();
            this.checkApplyRelease = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsOverseas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.depValidate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentDispatchContainer)).BeginInit();
            this.grbAgentType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckeBelong.Properties)).BeginInit();
            this.grbDispatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkApplyRelease.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bdsOverseas
            // 
            this.bdsOverseas.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.UserList);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(829, 510);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 37;
            this.btnSend.Text = "Dispatch(&F)";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnColse
            // 
            this.btnColse.Location = new System.Drawing.Point(934, 510);
            this.btnColse.Name = "btnColse";
            this.btnColse.Size = new System.Drawing.Size(75, 23);
            this.btnColse.TabIndex = 38;
            this.btnColse.Text = "Cancel(&C)";
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // lblErr
            // 
            this.lblErr.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblErr.Appearance.Options.UseForeColor = true;
            this.lblErr.Location = new System.Drawing.Point(521, 515);
            this.lblErr.Name = "lblErr";
            this.lblErr.Size = new System.Drawing.Size(72, 14);
            this.lblErr.TabIndex = 40;
            this.lblErr.Text = "ErrorMessage";
            // 
            // depValidate
            // 
            this.depValidate.ContainerControl = this;
            // 
            // DocumentDispatchContainer
            // 
            this.DocumentDispatchContainer.Location = new System.Drawing.Point(5, 82);
            this.DocumentDispatchContainer.Name = "DocumentDispatchContainer";
            this.DocumentDispatchContainer.Size = new System.Drawing.Size(1016, 411);
            this.DocumentDispatchContainer.TabIndex = 41;
            // 
            // grbAgentType
            // 
            this.grbAgentType.Controls.Add(this.txtAgent);
            this.grbAgentType.Controls.Add(this.lblAgent);
            this.grbAgentType.Controls.Add(this.ckeBelong);
            this.grbAgentType.Location = new System.Drawing.Point(8, 3);
            this.grbAgentType.Name = "grbAgentType";
            this.grbAgentType.Size = new System.Drawing.Size(589, 73);
            this.grbAgentType.TabIndex = 0;
            this.grbAgentType.TabStop = false;
            this.grbAgentType.Text = "Agent Type";
            // 
            // txtAgent
            // 
            this.txtAgent.Location = new System.Drawing.Point(55, 33);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Properties.ReadOnly = true;
            this.txtAgent.Size = new System.Drawing.Size(374, 21);
            this.txtAgent.TabIndex = 34;
            // 
            // lblAgent
            // 
            this.lblAgent.Location = new System.Drawing.Point(16, 36);
            this.lblAgent.Name = "lblAgent";
            this.lblAgent.Size = new System.Drawing.Size(34, 14);
            this.lblAgent.TabIndex = 32;
            this.lblAgent.Text = "Agent";
            // 
            // ckeBelong
            // 
            this.ckeBelong.Enabled = false;
            this.ckeBelong.Location = new System.Drawing.Point(435, 35);
            this.ckeBelong.Name = "ckeBelong";
            this.ckeBelong.Properties.Caption = "Belong  to City Ocean";
            this.ckeBelong.Properties.ReadOnly = true;
            this.ckeBelong.Size = new System.Drawing.Size(145, 19);
            this.ckeBelong.TabIndex = 33;
            // 
            // grbDispatch
            // 
            this.grbDispatch.Controls.Add(this.lstboxOverseas);
            this.grbDispatch.Controls.Add(this.rdoOverseas);
            this.grbDispatch.Controls.Add(this.rdoAgent);
            this.grbDispatch.Controls.Add(this.txtSendAgent);
            this.grbDispatch.Location = new System.Drawing.Point(603, 2);
            this.grbDispatch.Name = "grbDispatch";
            this.grbDispatch.Size = new System.Drawing.Size(413, 73);
            this.grbDispatch.TabIndex = 10;
            this.grbDispatch.TabStop = false;
            this.grbDispatch.Text = "Dispatch Docs to Agent/CS";
            // 
            // lstboxOverseas
            // 
            this.lstboxOverseas.EditText = "";
            this.lstboxOverseas.EditValue = null;
            this.lstboxOverseas.Enabled = false;
            this.lstboxOverseas.Location = new System.Drawing.Point(174, 43);
            this.lstboxOverseas.Name = "lstboxOverseas";
            this.lstboxOverseas.ReadOnly = false;
            this.lstboxOverseas.RefreshButtonToolTip = "";
            this.lstboxOverseas.ShowRefreshButton = false;
            this.lstboxOverseas.Size = new System.Drawing.Size(220, 21);
            this.lstboxOverseas.SpecifiedBackColor = System.Drawing.Color.White;
            this.lstboxOverseas.TabIndex = 12;
            this.lstboxOverseas.ToolTip = "";
            // 
            // rdoOverseas
            // 
            this.rdoOverseas.AutoSize = true;
            this.rdoOverseas.Location = new System.Drawing.Point(17, 43);
            this.rdoOverseas.Name = "rdoOverseas";
            this.rdoOverseas.Size = new System.Drawing.Size(158, 18);
            this.rdoOverseas.TabIndex = 11;
            this.rdoOverseas.TabStop = true;
            this.rdoOverseas.Text = "Dispatch to Overseas CS";
            this.rdoOverseas.UseVisualStyleBackColor = true;
            this.rdoOverseas.CheckedChanged += new System.EventHandler(this.rdoOverseas_CheckedChanged);
            // 
            // rdoAgent
            // 
            this.rdoAgent.AutoSize = true;
            this.rdoAgent.Location = new System.Drawing.Point(16, 17);
            this.rdoAgent.Name = "rdoAgent";
            this.rdoAgent.Size = new System.Drawing.Size(125, 18);
            this.rdoAgent.TabIndex = 10;
            this.rdoAgent.TabStop = true;
            this.rdoAgent.Text = "Dispatch to Agent";
            this.rdoAgent.UseVisualStyleBackColor = true;
            this.rdoAgent.CheckedChanged += new System.EventHandler(this.rdoAgent_CheckedChanged);
            // 
            // txtSendAgent
            // 
            this.txtSendAgent.Enabled = false;
            this.txtSendAgent.Location = new System.Drawing.Point(174, 15);
            this.txtSendAgent.Name = "txtSendAgent";
            this.txtSendAgent.Size = new System.Drawing.Size(220, 21);
            this.txtSendAgent.TabIndex = 9;
            // 
            // checkApplyRelease
            // 
            this.checkApplyRelease.EditValue = true;
            this.checkApplyRelease.Enabled = false;
            this.checkApplyRelease.Location = new System.Drawing.Point(6, 514);
            this.checkApplyRelease.Name = "checkApplyRelease";
            this.checkApplyRelease.Properties.Caption = "IsApplyRelease";
            this.checkApplyRelease.Size = new System.Drawing.Size(244, 19);
            this.checkApplyRelease.TabIndex = 43;
            // 
            // FrmSendAgentDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkApplyRelease);
            this.Controls.Add(this.grbDispatch);
            this.Controls.Add(this.grbAgentType);
            this.Controls.Add(this.DocumentDispatchContainer);
            this.Controls.Add(this.lblErr);
            this.Controls.Add(this.btnColse);
            this.Controls.Add(this.btnSend);
            this.Name = "FrmSendAgentDocument";
            this.Size = new System.Drawing.Size(1024, 552);
            ((System.ComponentModel.ISupportInitialize)(this.bdsOverseas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.depValidate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentDispatchContainer)).EndInit();
            this.grbAgentType.ResumeLayout(false);
            this.grbAgentType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckeBelong.Properties)).EndInit();
            this.grbDispatch.ResumeLayout(false);
            this.grbDispatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkApplyRelease.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ICP.FCM.Common.UI.Common.Parts.UCDocumentDispatchPartNew ucDocumentListDispatch;
        private System.Windows.Forms.BindingSource bdsOverseas;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.SimpleButton btnColse;
        private DevExpress.XtraEditors.LabelControl lblErr;
        private System.Windows.Forms.ErrorProvider depValidate;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.PanelControl DocumentDispatchContainer;
        private System.Windows.Forms.GroupBox grbAgentType;
        private DevExpress.XtraEditors.TextEdit txtAgent;
        private DevExpress.XtraEditors.LabelControl lblAgent;
        private DevExpress.XtraEditors.CheckEdit ckeBelong;
        private System.Windows.Forms.GroupBox grbDispatch;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox lstboxOverseas;
        private System.Windows.Forms.RadioButton rdoOverseas;
        private System.Windows.Forms.RadioButton rdoAgent;
        private DevExpress.XtraEditors.TextEdit txtSendAgent;
        private DevExpress.XtraEditors.CheckEdit checkApplyRelease;

    }
}