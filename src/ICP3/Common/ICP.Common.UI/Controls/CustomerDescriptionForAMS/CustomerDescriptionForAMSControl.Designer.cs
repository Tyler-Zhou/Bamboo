namespace ICP.Common.UI.Controls
{
    partial class CustomerDescriptionForAMSControl
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
            this.components = new System.ComponentModel.Container();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.labCityZip = new DevExpress.XtraEditors.LabelControl();
            this.labCountry = new DevExpress.XtraEditors.LabelControl();
            this.labAddress = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.cmbCountry = new DevExpress.XtraEditors.ComboBoxEdit();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtName = new DevExpress.XtraEditors.MemoEdit();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            this.cmbCityState = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblZip = new DevExpress.XtraEditors.LabelControl();
            this.cmbZip = new DevExpress.XtraEditors.ComboBoxEdit();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCityState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbZip.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(211, 218);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(54, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(150, 218);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(54, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "C&lear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // labCityZip
            // 
            this.labCityZip.Location = new System.Drawing.Point(2, 173);
            this.labCityZip.Name = "labCityZip";
            this.labCityZip.Size = new System.Drawing.Size(54, 14);
            this.labCityZip.TabIndex = 5;
            this.labCityZip.Text = "City,State";
            this.labCityZip.ToolTip = "City/State";
            // 
            // labCountry
            // 
            this.labCountry.Location = new System.Drawing.Point(2, 151);
            this.labCountry.Name = "labCountry";
            this.labCountry.Size = new System.Drawing.Size(43, 14);
            this.labCountry.TabIndex = 4;
            this.labCountry.Text = "Country";
            // 
            // labAddress
            // 
            this.labAddress.Location = new System.Drawing.Point(2, 52);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(26, 14);
            this.labAddress.TabIndex = 3;
            this.labAddress.Text = "Add.";
            this.labAddress.ToolTip = "Address";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(2, 2);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 9;
            this.labName.Text = "Name";
            // 
            // cmbCountry
            // 
            this.cmbCountry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Country", true));
            this.cmbCountry.Location = new System.Drawing.Point(59, 148);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbCountry.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbCountry.Size = new System.Drawing.Size(206, 21);
            this.cmbCountry.TabIndex = 2;
            this.cmbCountry.SelectedIndexChanged += new System.EventHandler(this.cmbCountry_SelectedIndexChanged);
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS);
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Name", true));
            this.txtName.Location = new System.Drawing.Point(59, 2);
            this.txtName.Name = "txtName";
            this.txtName.Properties.MaxLength = 40;
            this.txtName.Size = new System.Drawing.Size(206, 49);
            this.txtName.TabIndex = 0;
            // 
            // txtAddress
            // 
            this.txtAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Address", true));
            this.txtAddress.Location = new System.Drawing.Point(59, 52);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Properties.MaxLength = 70;
            this.txtAddress.Size = new System.Drawing.Size(206, 95);
            this.txtAddress.TabIndex = 1;
            // 
            // cmbCityState
            // 
            this.cmbCityState.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "City", true));
            this.cmbCityState.Location = new System.Drawing.Point(59, 170);
            this.cmbCityState.Name = "cmbCityState";
            this.cmbCityState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbCityState.Size = new System.Drawing.Size(206, 21);
            this.cmbCityState.TabIndex = 3;
            this.cmbCityState.SelectedIndexChanged += new System.EventHandler(this.cmbCityState_SelectedIndexChanged);
            // 
            // lblZip
            // 
            this.lblZip.Location = new System.Drawing.Point(3, 195);
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size(18, 14);
            this.lblZip.TabIndex = 202;
            this.lblZip.Text = "ZIP";
            this.lblZip.ToolTip = "City/State";
            // 
            // cmbZip
            // 
            this.cmbZip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Zip", true));
            this.cmbZip.Location = new System.Drawing.Point(59, 192);
            this.cmbZip.Name = "cmbZip";
            this.cmbZip.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbZip.Size = new System.Drawing.Size(206, 21);
            this.cmbZip.TabIndex = 4;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(89, 221);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(45, 20);
            this.webBrowser1.TabIndex = 203;
            this.webBrowser1.Visible = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // CustomerDescriptionForAMSControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.cmbZip);
            this.Controls.Add(this.lblZip);
            this.Controls.Add(this.cmbCityState);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.labCityZip);
            this.Controls.Add(this.labCountry);
            this.Controls.Add(this.labAddress);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.cmbCountry);
            this.Name = "CustomerDescriptionForAMSControl";
            this.Size = new System.Drawing.Size(268, 245);
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCityState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbZip.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.LabelControl labCityZip;
        private DevExpress.XtraEditors.LabelControl labCountry;
        private DevExpress.XtraEditors.LabelControl labAddress;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCountry;
        private DevExpress.XtraEditors.MemoEdit txtName;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCityState;
        private DevExpress.XtraEditors.LabelControl lblZip;
        private DevExpress.XtraEditors.ComboBoxEdit cmbZip;
        private System.Windows.Forms.WebBrowser webBrowser1;

    }
}
