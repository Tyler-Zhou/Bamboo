namespace ICP.WF.WorkFlowDesigner
{
    partial class NewWorkflow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewWorkflow));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.listWorkFlowTemplateView = new DevExpress.XtraEditors.ListBoxControl();
            this.viewHostPanel = new DevExpress.XtraEditors.PanelControl();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.errorTip = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.txtSaveAs = new DevExpress.XtraEditors.ButtonEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listWorkFlowTemplateView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewHostPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaveAs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            resources.ApplyResources(this.splitContainerControl1, "splitContainerControl1");
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.listWorkFlowTemplateView);
            resources.ApplyResources(this.splitContainerControl1.Panel1, "splitContainerControl1.Panel1");
            this.splitContainerControl1.Panel2.Controls.Add(this.viewHostPanel);
            resources.ApplyResources(this.splitContainerControl1.Panel2, "splitContainerControl1.Panel2");
            this.splitContainerControl1.SplitterPosition = 177;
            // 
            // listWorkFlowTemplateView
            // 
            resources.ApplyResources(this.listWorkFlowTemplateView, "listWorkFlowTemplateView");
            this.listWorkFlowTemplateView.Name = "listWorkFlowTemplateView";
            this.listWorkFlowTemplateView.SelectedIndexChanged += new System.EventHandler(this.listWorkFlowTemplateView_SelectedIndexChanged);
            // 
            // viewHostPanel
            // 
            resources.ApplyResources(this.viewHostPanel, "viewHostPanel");
            this.viewHostPanel.Name = "viewHostPanel";
       
            // 
            // dlgSaveFile
            // 
            this.dlgSaveFile.DefaultExt = "xoml";
            resources.ApplyResources(this.dlgSaveFile, "dlgSaveFile");
            // 
            // errorTip
            // 
            this.errorTip.ContainerControl = this;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.txtSaveAs);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Controls.Add(this.labelControl2);
            this.pnlBottom.Controls.Add(this.labelControl1);
            this.pnlBottom.Controls.Add(this.txtName);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // txtSaveAs
            // 
            resources.ApplyResources(this.txtSaveAs, "txtSaveAs");
            this.txtSaveAs.Name = "txtSaveAs";
            this.txtSaveAs.Properties.ReadOnly = true;
            this.txtSaveAs.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.TabStop = false;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // NewWorkflow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.pnlBottom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewWorkflow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.NewWorkflow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listWorkFlowTemplateView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewHostPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaveAs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.ErrorProvider errorTip;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl viewHostPanel;
        private DevExpress.XtraEditors.ListBoxControl listWorkFlowTemplateView;
        private DevExpress.XtraEditors.ButtonEdit txtSaveAs;
        private DevExpress.XtraEditors.ButtonEdit txtName;
    }
}