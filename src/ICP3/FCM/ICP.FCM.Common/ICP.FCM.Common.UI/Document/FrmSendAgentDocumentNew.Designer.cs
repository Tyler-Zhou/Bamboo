namespace ICP.FCM.Common.UI.Document
{
    partial class FrmSendAgentDocumentNew
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
            this.checkApplyRelease = new DevExpress.XtraEditors.CheckEdit();
            this.lstboxOverseas = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.rdoOverseas = new System.Windows.Forms.RadioButton();
            this.rdoAgent = new System.Windows.Forms.RadioButton();
            this.grbDispatch = new System.Windows.Forms.GroupBox();
            this.txtSendAgent = new DevExpress.XtraEditors.TextEdit();
            this.grbAgentType = new System.Windows.Forms.GroupBox();
            this.txtAgent = new DevExpress.XtraEditors.TextEdit();
            this.lblAgent = new DevExpress.XtraEditors.LabelControl();
            this.ckeBelong = new DevExpress.XtraEditors.CheckEdit();
            this.depValidate = new System.Windows.Forms.ErrorProvider();
            this.lblErr = new DevExpress.XtraEditors.LabelControl();
            this.btnColse = new DevExpress.XtraEditors.SimpleButton();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController();
            this.DocumentDispatchContainer = new DevExpress.XtraEditors.PanelControl();
            this.bdsOverseas = new System.Windows.Forms.BindingSource();
            ((System.ComponentModel.ISupportInitialize)(this.checkApplyRelease.Properties)).BeginInit();
            this.grbDispatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAgent.Properties)).BeginInit();
            this.grbAgentType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckeBelong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.depValidate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentDispatchContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsOverseas)).BeginInit();
            this.SuspendLayout();
            // 
            // checkApplyRelease
            // 
            this.checkApplyRelease.EditValue = true;
            this.checkApplyRelease.Enabled = false;
            this.checkApplyRelease.Location = new System.Drawing.Point(5, 523);
            this.checkApplyRelease.Name = "checkApplyRelease";
            this.checkApplyRelease.Properties.Caption = "IsApplyRelease";
            this.checkApplyRelease.Size = new System.Drawing.Size(244, 19);
            this.checkApplyRelease.TabIndex = 50;
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
            this.lstboxOverseas.EditValueChanged += new System.EventHandler(this.lstboxOverseas_EditValueChanged);
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
            // grbDispatch
            // 
            this.grbDispatch.Controls.Add(this.lstboxOverseas);
            this.grbDispatch.Controls.Add(this.rdoOverseas);
            this.grbDispatch.Controls.Add(this.rdoAgent);
            this.grbDispatch.Controls.Add(this.txtSendAgent);
            this.grbDispatch.Location = new System.Drawing.Point(602, 11);
            this.grbDispatch.Name = "grbDispatch";
            this.grbDispatch.Size = new System.Drawing.Size(413, 73);
            this.grbDispatch.TabIndex = 45;
            this.grbDispatch.TabStop = false;
            this.grbDispatch.Text = "Dispatch Docs to Agent/CS";
            // 
            // txtSendAgent
            // 
            this.txtSendAgent.Enabled = false;
            this.txtSendAgent.Location = new System.Drawing.Point(174, 15);
            this.txtSendAgent.Name = "txtSendAgent";
            this.txtSendAgent.Size = new System.Drawing.Size(220, 21);
            this.txtSendAgent.TabIndex = 9;
            this.txtSendAgent.EditValueChanged += new System.EventHandler(this.txtSendAgent_EditValueChanged);
            // 
            // grbAgentType
            // 
            this.grbAgentType.Controls.Add(this.txtAgent);
            this.grbAgentType.Controls.Add(this.lblAgent);
            this.grbAgentType.Controls.Add(this.ckeBelong);
            this.grbAgentType.Location = new System.Drawing.Point(7, 12);
            this.grbAgentType.Name = "grbAgentType";
            this.grbAgentType.Size = new System.Drawing.Size(589, 73);
            this.grbAgentType.TabIndex = 44;
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
            // depValidate
            // 
            this.depValidate.ContainerControl = this;
            // 
            // lblErr
            // 
            this.lblErr.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblErr.Appearance.Options.UseForeColor = true;
            this.lblErr.Location = new System.Drawing.Point(462, 525);
            this.lblErr.Name = "lblErr";
            this.lblErr.Size = new System.Drawing.Size(72, 14);
            this.lblErr.TabIndex = 48;
            this.lblErr.Text = "ErrorMessage";
            // 
            // btnColse
            // 
            this.btnColse.Location = new System.Drawing.Point(933, 519);
            this.btnColse.Name = "btnColse";
            this.btnColse.Size = new System.Drawing.Size(75, 23);
            this.btnColse.TabIndex = 47;
            this.btnColse.Text = "Cancel(&C)";
            this.btnColse.Click += new System.EventHandler(this.btnColse_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(828, 519);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 46;
            this.btnSend.Text = "Dispatch(&F)";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // DocumentDispatchContainer
            // 
            this.DocumentDispatchContainer.Location = new System.Drawing.Point(4, 91);
            this.DocumentDispatchContainer.Name = "DocumentDispatchContainer";
            this.DocumentDispatchContainer.Size = new System.Drawing.Size(1016, 411);
            this.DocumentDispatchContainer.TabIndex = 49;
            // 
            // bdsOverseas
            // 
            this.bdsOverseas.DataSource = typeof(ICP.Sys.ServiceInterface.DataObjects.UserList);
            // 
            // FrmSendAgentDocumentNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkApplyRelease);
            this.Controls.Add(this.grbDispatch);
            this.Controls.Add(this.grbAgentType);
            this.Controls.Add(this.lblErr);
            this.Controls.Add(this.btnColse);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.DocumentDispatchContainer);
            this.Name = "FrmSendAgentDocumentNew";
            this.Size = new System.Drawing.Size(1025, 562);
            ((System.ComponentModel.ISupportInitialize)(this.checkApplyRelease.Properties)).EndInit();
            this.grbDispatch.ResumeLayout(false);
            this.grbDispatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSendAgent.Properties)).EndInit();
            this.grbAgentType.ResumeLayout(false);
            this.grbAgentType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ckeBelong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.depValidate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentDispatchContainer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdsOverseas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ICP.FCM.Common.UI.Common.Parts.UCDocumentDispatchPartNew ucDocumentListDispatch;
        private DevExpress.XtraEditors.CheckEdit checkApplyRelease;
        private Framework.ClientComponents.Controls.MultiSearchCommonBox lstboxOverseas;
        private System.Windows.Forms.RadioButton rdoOverseas;
        private System.Windows.Forms.RadioButton rdoAgent;
        private System.Windows.Forms.GroupBox grbDispatch;
        private DevExpress.XtraEditors.TextEdit txtSendAgent;
        private System.Windows.Forms.GroupBox grbAgentType;
        private DevExpress.XtraEditors.TextEdit txtAgent;
        private DevExpress.XtraEditors.LabelControl lblAgent;
        private DevExpress.XtraEditors.CheckEdit ckeBelong;
        private System.Windows.Forms.ErrorProvider depValidate;
        private DevExpress.XtraEditors.LabelControl lblErr;
        private DevExpress.XtraEditors.SimpleButton btnColse;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.PanelControl DocumentDispatchContainer;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private System.Windows.Forms.BindingSource bdsOverseas;

    }
}
