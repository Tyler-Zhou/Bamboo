namespace ICP.WF.FormDesigner
{
    partial class NewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewForm));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.splitContainer1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.listFormFileName = new DevExpress.XtraEditors.ListBoxControl();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.errorTip = new System.Windows.Forms.ErrorProvider(this.components);
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.txtSaveAs = new DevExpress.XtraEditors.ButtonEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listFormFileName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSaveAs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.listFormFileName);
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.splitContainer1.SplitterPosition = 161;
            // 
            // listFormFileName
            // 
            resources.ApplyResources(this.listFormFileName, "listFormFileName");
            this.listFormFileName.Name = "listFormFileName";
            this.listFormFileName.SelectedIndexChanged += new System.EventHandler(this.listFormFileName_SelectedIndexChanged);
            // 
            // dlgSaveFile
            // 
            this.dlgSaveFile.DefaultExt = "xml";
            resources.ApplyResources(this.dlgSaveFile, "dlgSaveFile");
            // 
            // errorTip
            // 
            this.errorTip.ContainerControl = this;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.pnlBottom.Controls.Add(this.txtSaveAs);
            this.pnlBottom.Controls.Add(this.txtName);
            this.pnlBottom.Controls.Add(this.label2);
            this.pnlBottom.Controls.Add(this.label1);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // txtSaveAs
            // 
            resources.ApplyResources(this.txtSaveAs, "txtSaveAs");
            this.txtSaveAs.Name = "txtSaveAs";
            this.txtSaveAs.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("txtSaveAs.Properties.Buttons"))), resources.GetString("txtSaveAs.Properties.Buttons1"), ((int)(resources.GetObject("txtSaveAs.Properties.Buttons2"))), ((bool)(resources.GetObject("txtSaveAs.Properties.Buttons3"))), ((bool)(resources.GetObject("txtSaveAs.Properties.Buttons4"))), ((bool)(resources.GetObject("txtSaveAs.Properties.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("txtSaveAs.Properties.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("txtSaveAs.Properties.Buttons7"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("txtSaveAs.Properties.Buttons8"), null, null, ((bool)(resources.GetObject("txtSaveAs.Properties.Buttons9"))))});
            this.txtSaveAs.Properties.ReadOnly = true;
            this.txtSaveAs.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit1_Properties_ButtonClick);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnCancel
            // 
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
            // NewForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlBottom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewForm";
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listFormFileName)).EndInit();
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
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl label2;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.SplitContainerControl splitContainer1;
        private DevExpress.XtraEditors.ListBoxControl listFormFileName;
        private DevExpress.XtraEditors.ButtonEdit txtSaveAs;

    }
}