namespace ICP.FAM.UI.ChargeConfigure
{
    partial class AddContainer
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
            this.numPrice = new DevExpress.XtraEditors.SpinEdit();
            this.labPrice = new DevExpress.XtraEditors.LabelControl();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.cmbContainerUnit = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrice.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbContainerUnit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // numPrice
            // 
            this.numPrice.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numPrice.Location = new System.Drawing.Point(199, 14);
            this.numPrice.Name = "numPrice";
            this.numPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numPrice.Properties.DisplayFormat.FormatString = "N2";
            this.numPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numPrice.Properties.EditFormat.FormatString = "N2";
            this.numPrice.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numPrice.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numPrice.Properties.Mask.EditMask = "N2";
            this.numPrice.Properties.MaxValue = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numPrice.Size = new System.Drawing.Size(93, 21);
            this.numPrice.TabIndex = 2;
            // 
            // labPrice
            // 
            this.labPrice.Location = new System.Drawing.Point(164, 16);
            this.labPrice.Margin = new System.Windows.Forms.Padding(0);
            this.labPrice.Name = "labPrice";
            this.labPrice.Size = new System.Drawing.Size(26, 14);
            this.labPrice.TabIndex = 835;
            this.labPrice.Text = "Price";
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(16, 17);
            this.labCurrency.Margin = new System.Windows.Forms.Padding(0);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(28, 14);
            this.labCurrency.TabIndex = 834;
            this.labCurrency.Text = "Type";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(57, 47);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(79, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(179, 47);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbContainerUnit
            // 
            this.cmbContainerUnit.Location = new System.Drawing.Point(57, 14);
            this.cmbContainerUnit.Name = "cmbContainerUnit";
            this.cmbContainerUnit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbContainerUnit.Properties.Appearance.Options.UseBackColor = true;
            this.cmbContainerUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbContainerUnit.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbContainerUnit.Size = new System.Drawing.Size(93, 21);
            this.cmbContainerUnit.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbContainerUnit.TabIndex = 1;
            // 
            // AddContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbContainerUnit);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.labPrice);
            this.Controls.Add(this.labCurrency);
            this.Name = "AddContainer";
            this.Size = new System.Drawing.Size(313, 94);
            this.Load += new System.EventHandler(this.AddContainer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPrice.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbContainerUnit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit numPrice;
        private DevExpress.XtraEditors.LabelControl labPrice;
        private DevExpress.XtraEditors.LabelControl labCurrency;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbContainerUnit;
    }
}
