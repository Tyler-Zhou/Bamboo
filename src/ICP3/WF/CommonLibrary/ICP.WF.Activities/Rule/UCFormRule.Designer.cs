namespace ICP.WF.Activities
{
	partial class UCFormRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCFormRule));
            this.gbxFormRule = new DevExpress.XtraEditors.GroupControl();
            this.txtValue = new DevExpress.XtraEditors.TextEdit();
            this.cmbOperator = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbFormExpression = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gbxFormRule)).BeginInit();
            this.gbxFormRule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFormExpression.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxFormRule
            // 
            this.gbxFormRule.Controls.Add(this.txtValue);
            this.gbxFormRule.Controls.Add(this.cmbOperator);
            this.gbxFormRule.Controls.Add(this.cmbFormExpression);
            this.gbxFormRule.Controls.Add(this.labelControl3);
            this.gbxFormRule.Controls.Add(this.labelControl2);
            this.gbxFormRule.Controls.Add(this.labelControl1);
            resources.ApplyResources(this.gbxFormRule, "gbxFormRule");
            this.gbxFormRule.Name = "gbxFormRule";
            // 
            // txtValue
            // 
            resources.ApplyResources(this.txtValue, "txtValue");
            this.txtValue.Name = "txtValue";
            this.txtValue.EditValueChanged += new System.EventHandler(this.txtValue_EditValueChanged);
            // 
            // cmbOperator
            // 
            resources.ApplyResources(this.cmbOperator, "cmbOperator");
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbOperator.Properties.Buttons"))))});
            this.cmbOperator.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbOperator.SelectedValueChanged += new System.EventHandler(this.cmbOperator_SelectedValueChanged);
            // 
            // cmbFormExpression
            // 
            resources.ApplyResources(this.cmbFormExpression, "cmbFormExpression");
            this.cmbFormExpression.Name = "cmbFormExpression";
            this.cmbFormExpression.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("cmbFormExpression.Properties.Buttons"))))});
            this.cmbFormExpression.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFormExpression.SelectedIndexChanged += new System.EventHandler(this.cmbFormExpression_SelectedIndexChanged);
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
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
            // UCFormRule
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxFormRule);
            this.Name = "UCFormRule";
            ((System.ComponentModel.ISupportInitialize)(this.gbxFormRule)).EndInit();
            this.gbxFormRule.ResumeLayout(false);
            this.gbxFormRule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFormExpression.Properties)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraEditors.GroupControl gbxFormRule;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.ComboBoxEdit cmbOperator;
        public DevExpress.XtraEditors.ComboBoxEdit cmbFormExpression;
        public DevExpress.XtraEditors.TextEdit txtValue;
	}
}
