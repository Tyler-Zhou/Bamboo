namespace ICP.FCM.Common.UI.AgentRequest
{
    partial class AssignAgentRequestPart
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labAgent = new DevExpress.XtraEditors.LabelControl();
            this.stxtAgent = new DevExpress.XtraEditors.ButtonEdit();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.AgentRequestInfo);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(160, 133);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 10;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(253, 133);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labAgent
            // 
            this.labAgent.Location = new System.Drawing.Point(9, 13);
            this.labAgent.Name = "labAgent";
            this.labAgent.Size = new System.Drawing.Size(34, 14);
            this.labAgent.TabIndex = 23;
            this.labAgent.Text = "Agent";
            // 
            // stxtAgent
            // 
            this.stxtAgent.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "AgentID", true));
            this.stxtAgent.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AgentName", true));
            this.stxtAgent.Location = new System.Drawing.Point(68, 10);
            this.stxtAgent.Name = "stxtAgent";
            this.stxtAgent.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtAgent.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAgent.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtAgent.Size = new System.Drawing.Size(260, 21);
            this.stxtAgent.TabIndex = 0;
            // 
            // txtRemark
            // 
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "SolverRemark", true));
            this.txtRemark.Location = new System.Drawing.Point(68, 37);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(260, 90);
            this.txtRemark.TabIndex = 5;
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(9, 40);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 23;
            this.labRemark.Text = "Remark";
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // AnswerAgentRequestPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.labRemark);
            this.Controls.Add(this.labAgent);
            this.Controls.Add(this.stxtAgent);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "AnswerAgentRequestPart";
            this.Size = new System.Drawing.Size(336, 167);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAgent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labAgent;
        private DevExpress.XtraEditors.ButtonEdit stxtAgent;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}