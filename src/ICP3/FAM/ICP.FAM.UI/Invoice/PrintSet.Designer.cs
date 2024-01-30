namespace ICP.FAM.UI.Invoice
{
    partial class PrintSet
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.radReport = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnCancle = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.chkCompany = new DevExpress.XtraEditors.CheckEdit();
            this.chkCurrencyTotal = new DevExpress.XtraEditors.CheckEdit();
            this.chkProxy = new DevExpress.XtraEditors.CheckEdit();
            this.txtRate = new DevExpress.XtraEditors.TextEdit();
            this.labRate = new DevExpress.XtraEditors.LabelControl();
            this.labCurrency = new DevExpress.XtraEditors.LabelControl();
            this.chkCurrency = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.rad = new DevExpress.XtraEditors.RadioGroup();
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radReport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCurrencyTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProxy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCurrency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.Appearance.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.radReport);
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(335, 96);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "报表选择";
            // 
            // radReport
            // 
            this.radReport.Location = new System.Drawing.Point(5, 26);
            this.radReport.Name = "radReport";
            this.radReport.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "运输发票(自有格式)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "运输发票(套打格式)"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "船代发票")});
            this.radReport.Size = new System.Drawing.Size(326, 65);
            this.radReport.TabIndex = 0;
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.btnCancle);
            this.groupControl2.Controls.Add(this.btnOK);
            this.groupControl2.Controls.Add(this.panelControl1);
            this.groupControl2.Controls.Add(this.groupControl3);
            this.groupControl2.Location = new System.Drawing.Point(1, 94);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(339, 199);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "打印设置";
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(176, 170);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 3;
            this.btnCancle.Text = "取消";
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(80, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.chkCompany);
            this.panelControl1.Controls.Add(this.chkCurrencyTotal);
            this.panelControl1.Controls.Add(this.chkProxy);
            this.panelControl1.Controls.Add(this.txtRate);
            this.panelControl1.Controls.Add(this.labRate);
            this.panelControl1.Controls.Add(this.labCurrency);
            this.panelControl1.Controls.Add(this.chkCurrency);
            this.panelControl1.Location = new System.Drawing.Point(7, 80);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(323, 83);
            this.panelControl1.TabIndex = 1;
            // 
            // chkCompany
            // 
            this.chkCompany.Location = new System.Drawing.Point(2, 54);
            this.chkCompany.Name = "chkCompany";
            this.chkCompany.Properties.Caption = "显示公司名称";
            this.chkCompany.Size = new System.Drawing.Size(146, 19);
            this.chkCompany.TabIndex = 6;
            // 
            // chkCurrencyTotal
            // 
            this.chkCurrencyTotal.Location = new System.Drawing.Point(161, 29);
            this.chkCurrencyTotal.Name = "chkCurrencyTotal";
            this.chkCurrencyTotal.Properties.Caption = "显示本位币汇总";
            this.chkCurrencyTotal.Size = new System.Drawing.Size(153, 19);
            this.chkCurrencyTotal.TabIndex = 5;
            // 
            // chkProxy
            // 
            this.chkProxy.Location = new System.Drawing.Point(2, 29);
            this.chkProxy.Name = "chkProxy";
            this.chkProxy.Properties.Caption = "添加代理文本";
            this.chkProxy.Size = new System.Drawing.Size(146, 19);
            this.chkProxy.TabIndex = 4;
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(198, 2);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(72, 21);
            this.txtRate.TabIndex = 3;
            // 
            // labRate
            // 
            this.labRate.Location = new System.Drawing.Point(162, 5);
            this.labRate.Name = "labRate";
            this.labRate.Size = new System.Drawing.Size(24, 14);
            this.labRate.TabIndex = 2;
            this.labRate.Text = "汇率";
            // 
            // labCurrency
            // 
            this.labCurrency.Location = new System.Drawing.Point(3, 5);
            this.labCurrency.Name = "labCurrency";
            this.labCurrency.Size = new System.Drawing.Size(48, 14);
            this.labCurrency.TabIndex = 0;
            this.labCurrency.Text = "折合币种";
            // 
            // chkCurrency
            // 
            this.chkCurrency.Location = new System.Drawing.Point(61, 2);
            this.chkCurrency.Name = "chkCurrency";
            this.chkCurrency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.chkCurrency.Size = new System.Drawing.Size(80, 21);
            this.chkCurrency.TabIndex = 1;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.rad);
            this.groupControl3.Location = new System.Drawing.Point(5, 26);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(330, 47);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "样式";
            // 
            // rad
            // 
            this.rad.Location = new System.Drawing.Point(0, 21);
            this.rad.Name = "rad";
            this.rad.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "旧格式"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "新格式")});
            this.rad.Size = new System.Drawing.Size(325, 26);
            this.rad.TabIndex = 0;
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FAM.ServiceInterface.DataObjects.InvoiceList);
            // 
            // PrintSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "PrintSet";
            this.Size = new System.Drawing.Size(336, 299);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radReport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCurrencyTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkProxy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCurrency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.RadioGroup rad;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labCurrency;
        private DevExpress.XtraEditors.ImageComboBoxEdit chkCurrency;
        private DevExpress.XtraEditors.LabelControl labRate;
        private DevExpress.XtraEditors.TextEdit txtRate;
        private DevExpress.XtraEditors.CheckEdit chkProxy;
        private DevExpress.XtraEditors.CheckEdit chkCurrencyTotal;
        private DevExpress.XtraEditors.CheckEdit chkCompany;
        private DevExpress.XtraEditors.SimpleButton btnCancle;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.RadioGroup radReport;
        private System.Windows.Forms.BindingSource bsList;
    }
}
