namespace ICP.WF.FormDesigner
{
    partial class DeployForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeployForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtCName = new DevExpress.XtraEditors.TextEdit();
            this.txtEName = new DevExpress.XtraEditors.TextEdit();
            this.txtVersion = new DevExpress.XtraEditors.SpinEdit();
            this.errorTip = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.cmbKeys = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKeys.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // labelControl4
            // 
            resources.ApplyResources(this.labelControl4, "labelControl4");
            this.labelControl4.Name = "labelControl4";
            // 
            // labelControl5
            // 
            resources.ApplyResources(this.labelControl5, "labelControl5");
            this.labelControl5.Name = "labelControl5";
            // 
            // txtCName
            // 
            this.txtCName.AllowDrop = true;
            this.errorTip.SetIconAlignment(this.txtCName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtCName, "txtCName");
            this.txtCName.Name = "txtCName";
            // 
            // txtEName
            // 
            this.txtEName.AllowDrop = true;
            this.errorTip.SetIconAlignment(this.txtEName, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtEName, "txtEName");
            this.txtEName.Name = "txtEName";
            // 
            // txtVersion
            // 
            resources.ApplyResources(this.txtVersion, "txtVersion");
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtVersion.Properties.IsFloatValue = false;
            this.txtVersion.Properties.Mask.EditMask = resources.GetString("txtVersion.Properties.Mask.EditMask");
            // 
            // errorTip
            // 
            this.errorTip.ContainerControl = this;
            // 
            // cmbKeys
            // 
            resources.ApplyResources(this.cmbKeys, "cmbKeys");
            this.cmbKeys.Name = "cmbKeys";
            this.cmbKeys.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.cmbKeys.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbKeys.Properties.Buttons"))))});
            this.cmbKeys.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("cmbKeys.Properties.Columns"), resources.GetString("cmbKeys.Properties.Columns1"), ((int)(resources.GetObject("cmbKeys.Properties.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("cmbKeys.Properties.Columns3"))), resources.GetString("cmbKeys.Properties.Columns4"), ((bool)(resources.GetObject("cmbKeys.Properties.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("cmbKeys.Properties.Columns6"))), ((DevExpress.Data.ColumnSortOrder)(resources.GetObject("cmbKeys.Properties.Columns7")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("cmbKeys.Properties.Columns8"), ((int)(resources.GetObject("cmbKeys.Properties.Columns9"))), resources.GetString("cmbKeys.Properties.Columns10")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("cmbKeys.Properties.Columns11"), ((int)(resources.GetObject("cmbKeys.Properties.Columns12"))), resources.GetString("cmbKeys.Properties.Columns13")),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("cmbKeys.Properties.Columns14"), resources.GetString("cmbKeys.Properties.Columns15"), ((int)(resources.GetObject("cmbKeys.Properties.Columns16"))), ((DevExpress.Utils.FormatType)(resources.GetObject("cmbKeys.Properties.Columns17"))), resources.GetString("cmbKeys.Properties.Columns18"), ((bool)(resources.GetObject("cmbKeys.Properties.Columns19"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("cmbKeys.Properties.Columns20"))), ((DevExpress.Data.ColumnSortOrder)(resources.GetObject("cmbKeys.Properties.Columns21"))))});
            this.cmbKeys.Properties.NullText = resources.GetString("cmbKeys.Properties.NullText");
            this.cmbKeys.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbKeys.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(this.cmbKeys_ProcessNewValue);
            this.cmbKeys.Leave += new System.EventHandler(this.cmbKeys_Leave);
            // 
            // DeployForm
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.cmbKeys);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtEName);
            this.Controls.Add(this.txtCName);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeployForm";
            ((System.ComponentModel.ISupportInitialize)(this.txtCName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVersion.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbKeys.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtEName;
        private DevExpress.XtraEditors.TextEdit txtCName;
        private DevExpress.XtraEditors.SpinEdit txtVersion;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorTip;
        private DevExpress.XtraEditors.LookUpEdit cmbKeys;
    }
}