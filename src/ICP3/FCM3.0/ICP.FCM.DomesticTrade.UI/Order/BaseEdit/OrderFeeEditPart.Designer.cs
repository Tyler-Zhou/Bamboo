namespace ICP.FCM.DomesticTrade.UI.Order
{
    partial class OrderFeeEditPart
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
            this.txtARAmount = new DevExpress.XtraEditors.TextEdit();
            this.labARAmount = new DevExpress.XtraEditors.LabelControl();
            this.labAPAmount = new DevExpress.XtraEditors.LabelControl();
            this.txtAPAmount = new DevExpress.XtraEditors.TextEdit();
            this.labProfit = new DevExpress.XtraEditors.LabelControl();
            this.txtProfit = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.feePartAR = new ICP.FCM.DomesticTrade.UI.Common.FeePart();
            this.cmbCurrency = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtARAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAPAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProfit.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtARAmount
            // 
            this.txtARAmount.EditValue = "";
            this.txtARAmount.Location = new System.Drawing.Point(71, 3);
            this.txtARAmount.Name = "txtARAmount";
            this.txtARAmount.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtARAmount.Properties.Appearance.Options.UseForeColor = true;
            this.txtARAmount.Properties.DisplayFormat.FormatString = "N";
            this.txtARAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtARAmount.Properties.EditFormat.FormatString = "N";
            this.txtARAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtARAmount.Properties.ReadOnly = true;
            this.txtARAmount.Size = new System.Drawing.Size(150, 21);
            this.txtARAmount.TabIndex = 3;
            // 
            // labARAmount
            // 
            this.labARAmount.Location = new System.Drawing.Point(6, 6);
            this.labARAmount.Name = "labARAmount";
            this.labARAmount.Size = new System.Drawing.Size(59, 14);
            this.labARAmount.TabIndex = 2;
            this.labARAmount.Text = "ARAmount";
            // 
            // labAPAmount
            // 
            this.labAPAmount.Location = new System.Drawing.Point(242, 6);
            this.labAPAmount.Name = "labAPAmount";
            this.labAPAmount.Size = new System.Drawing.Size(59, 14);
            this.labAPAmount.TabIndex = 2;
            this.labAPAmount.Text = "APAmount";
            // 
            // txtAPAmount
            // 
            this.txtAPAmount.EditValue = "";
            this.txtAPAmount.Location = new System.Drawing.Point(307, 3);
            this.txtAPAmount.Name = "txtAPAmount";
            this.txtAPAmount.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAPAmount.Properties.Appearance.Options.UseForeColor = true;
            this.txtAPAmount.Properties.DisplayFormat.FormatString = "N";
            this.txtAPAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAPAmount.Properties.EditFormat.FormatString = "N";
            this.txtAPAmount.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtAPAmount.Properties.ReadOnly = true;
            this.txtAPAmount.Size = new System.Drawing.Size(150, 21);
            this.txtAPAmount.TabIndex = 3;
            // 
            // labProfit
            // 
            this.labProfit.Location = new System.Drawing.Point(463, 6);
            this.labProfit.Name = "labProfit";
            this.labProfit.Size = new System.Drawing.Size(29, 14);
            this.labProfit.TabIndex = 2;
            this.labProfit.Text = "Profit";
            // 
            // txtProfit
            // 
            this.txtProfit.EditValue = "";
            this.txtProfit.Location = new System.Drawing.Point(498, 3);
            this.txtProfit.Name = "txtProfit";
            this.txtProfit.Properties.Appearance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtProfit.Properties.Appearance.Options.UseForeColor = true;
            this.txtProfit.Properties.DisplayFormat.FormatString = "N";
            this.txtProfit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtProfit.Properties.EditFormat.FormatString = "N";
            this.txtProfit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtProfit.Properties.ReadOnly = true;
            this.txtProfit.Size = new System.Drawing.Size(85, 21);
            this.txtProfit.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtARAmount);
            this.panel1.Controls.Add(this.txtProfit);
            this.panel1.Controls.Add(this.cmbCurrency);
            this.panel1.Controls.Add(this.txtAPAmount);
            this.panel1.Controls.Add(this.labARAmount);
            this.panel1.Controls.Add(this.labProfit);
            this.panel1.Controls.Add(this.labAPAmount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(658, 29);
            this.panel1.TabIndex = 4;
            // 
            // feePartAR
            // 
            this.feePartAR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.feePartAR.Location = new System.Drawing.Point(0, 0);
            this.feePartAR.Name = "feePartAR";
            this.feePartAR.Size = new System.Drawing.Size(658, 170);
            this.feePartAR.TabIndex = 0;
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.Location = new System.Drawing.Point(586, 3);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCurrency.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Size = new System.Drawing.Size(65, 21);
            this.cmbCurrency.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCurrency.TabIndex = 1;
            // 
            // OrderFeeEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.feePartAR);
            this.Controls.Add(this.panel1);
            this.Name = "OrderFeeEditPart";
            this.Size = new System.Drawing.Size(658, 199);
            ((System.ComponentModel.ISupportInitialize)(this.txtARAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAPAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProfit.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCurrency;
        private DevExpress.XtraEditors.TextEdit txtARAmount;
        private DevExpress.XtraEditors.LabelControl labARAmount;
        private DevExpress.XtraEditors.LabelControl labAPAmount;
        private DevExpress.XtraEditors.TextEdit txtAPAmount;
        private DevExpress.XtraEditors.LabelControl labProfit;
        private DevExpress.XtraEditors.TextEdit txtProfit;
        private ICP.FCM.DomesticTrade.UI.Common.FeePart feePartAR;
        private System.Windows.Forms.Panel panel1;
    }
}
