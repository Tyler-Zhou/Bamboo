namespace ICP.FCM.OceanExport.UI.BL.HBL
{
    partial class AmsCustomerDes
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
            this.labAddress = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbZip = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblZip = new DevExpress.XtraEditors.LabelControl();
            this.cmbCityState = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labCountry = new DevExpress.XtraEditors.LabelControl();
            this.cmbCountry = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labCityZip = new DevExpress.XtraEditors.LabelControl();
            this.txtAddress = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbZip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCityState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labAddress
            // 
            this.labAddress.Location = new System.Drawing.Point(10, 28);
            this.labAddress.Margin = new System.Windows.Forms.Padding(0);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(43, 14);
            this.labAddress.TabIndex = 114;
            this.labAddress.Text = "Address";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(24, 4);
            this.labName.Margin = new System.Windows.Forms.Padding(0);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(31, 14);
            this.labName.TabIndex = 112;
            this.labName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Name", true));
            this.txtName.Location = new System.Drawing.Point(65, 5);
            this.txtName.Margin = new System.Windows.Forms.Padding(0);
            this.txtName.Name = "txtName";
            this.txtName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtName.Size = new System.Drawing.Size(247, 21);
            this.txtName.TabIndex = 1;
            // 
            // bindingSource
            // 
            this.bindingSource.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.CustomerDescriptionForAMS);
            // 
            // cmbZip
            // 
            this.cmbZip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Zip", true));
            this.cmbZip.Location = new System.Drawing.Point(65, 97);
            this.cmbZip.Margin = new System.Windows.Forms.Padding(0);
            this.cmbZip.Name = "cmbZip";
            this.cmbZip.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbZip.Size = new System.Drawing.Size(247, 21);
            this.cmbZip.TabIndex = 6;
            // 
            // lblZip
            // 
            this.lblZip.Location = new System.Drawing.Point(37, 97);
            this.lblZip.Margin = new System.Windows.Forms.Padding(0);
            this.lblZip.Name = "lblZip";
            this.lblZip.Size = new System.Drawing.Size(18, 14);
            this.lblZip.TabIndex = 207;
            this.lblZip.Text = "ZIP";
            this.lblZip.ToolTip = "City/State";
            // 
            // cmbCityState
            // 
            this.cmbCityState.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "City", true));
            this.cmbCityState.Location = new System.Drawing.Point(210, 73);
            this.cmbCityState.Margin = new System.Windows.Forms.Padding(0);
            this.cmbCityState.Name = "cmbCityState";
            this.cmbCityState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbCityState.Size = new System.Drawing.Size(102, 21);
            this.cmbCityState.TabIndex = 5;
            
            // 
            // labCountry
            // 
            this.labCountry.Location = new System.Drawing.Point(12, 76);
            this.labCountry.Margin = new System.Windows.Forms.Padding(0);
            this.labCountry.Name = "labCountry";
            this.labCountry.Size = new System.Drawing.Size(43, 14);
            this.labCountry.TabIndex = 206;
            this.labCountry.Text = "Country";
            // 
            // cmbCountry
            // 
            this.cmbCountry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Country", true));
            this.cmbCountry.Location = new System.Drawing.Point(65, 73);
            this.cmbCountry.Margin = new System.Windows.Forms.Padding(0);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.cmbCountry.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbCountry.Size = new System.Drawing.Size(118, 21);
            this.cmbCountry.TabIndex = 4;
            // 
            // labCityZip
            // 
            this.labCityZip.Location = new System.Drawing.Point(186, 77);
            this.labCityZip.Margin = new System.Windows.Forms.Padding(0);
            this.labCityZip.Name = "labCityZip";
            this.labCityZip.Size = new System.Drawing.Size(20, 14);
            this.labCityZip.TabIndex = 208;
            this.labCityZip.Text = "City";
            this.labCityZip.ToolTip = "City/State";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(65, 29);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(247, 41);
            this.txtAddress.TabIndex = 209;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
            // 
            // AmsCustomerDes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.labCityZip);
            this.Controls.Add(this.cmbZip);
            this.Controls.Add(this.lblZip);
            this.Controls.Add(this.cmbCityState);
            this.Controls.Add(this.labCountry);
            this.Controls.Add(this.cmbCountry);
            this.Controls.Add(this.labAddress);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.txtName);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(320, 125);
            this.MinimumSize = new System.Drawing.Size(320, 125);
            this.Name = "AmsCustomerDes";
            this.Size = new System.Drawing.Size(320, 125);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbZip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCityState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labAddress;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbZip;
        private DevExpress.XtraEditors.LabelControl lblZip;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCityState;
        private DevExpress.XtraEditors.LabelControl labCountry;
        private DevExpress.XtraEditors.ComboBoxEdit cmbCountry;
        private DevExpress.XtraEditors.LabelControl labCityZip;
        private System.Windows.Forms.BindingSource bindingSource;
        private DevExpress.XtraEditors.MemoEdit txtAddress;
    }
}
