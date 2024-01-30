namespace ICP.FAM.UI
{
    partial class BeginSpreadsheetPart
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
            this.labASSETS = new DevExpress.XtraEditors.LabelControl();
            this.labLIABILITIES = new DevExpress.XtraEditors.LabelControl();
            this.labCOST = new DevExpress.XtraEditors.LabelControl();
            this.labINCOME = new DevExpress.XtraEditors.LabelControl();
            this.labEQUITY = new DevExpress.XtraEditors.LabelControl();
            this.labTotalDR = new DevExpress.XtraEditors.LabelControl();
            this.labTotalCR = new DevExpress.XtraEditors.LabelControl();
            this.labResult = new DevExpress.XtraEditors.LabelControl();
            this.barClose = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // labASSETS
            // 
            this.labASSETS.Location = new System.Drawing.Point(36, 17);
            this.labASSETS.Name = "labASSETS";
            this.labASSETS.Size = new System.Drawing.Size(24, 14);
            this.labASSETS.TabIndex = 0;
            this.labASSETS.Text = "资产";
            // 
            // labLIABILITIES
            // 
            this.labLIABILITIES.Location = new System.Drawing.Point(298, 17);
            this.labLIABILITIES.Name = "labLIABILITIES";
            this.labLIABILITIES.Size = new System.Drawing.Size(24, 14);
            this.labLIABILITIES.TabIndex = 0;
            this.labLIABILITIES.Text = "负债";
            // 
            // labCOST
            // 
            this.labCOST.Location = new System.Drawing.Point(36, 50);
            this.labCOST.Name = "labCOST";
            this.labCOST.Size = new System.Drawing.Size(24, 14);
            this.labCOST.TabIndex = 0;
            this.labCOST.Text = "成本";
            // 
            // labINCOME
            // 
            this.labINCOME.Location = new System.Drawing.Point(298, 50);
            this.labINCOME.Name = "labINCOME";
            this.labINCOME.Size = new System.Drawing.Size(24, 14);
            this.labINCOME.TabIndex = 0;
            this.labINCOME.Text = "权益";
            // 
            // labEQUITY
            // 
            this.labEQUITY.Location = new System.Drawing.Point(298, 87);
            this.labEQUITY.Name = "labEQUITY";
            this.labEQUITY.Size = new System.Drawing.Size(24, 14);
            this.labEQUITY.TabIndex = 0;
            this.labEQUITY.Text = "损益";
            // 
            // labTotalDR
            // 
            this.labTotalDR.Location = new System.Drawing.Point(36, 130);
            this.labTotalDR.Name = "labTotalDR";
            this.labTotalDR.Size = new System.Drawing.Size(40, 14);
            this.labTotalDR.TabIndex = 0;
            this.labTotalDR.Text = "合计 借";
            // 
            // labTotalCR
            // 
            this.labTotalCR.Location = new System.Drawing.Point(298, 130);
            this.labTotalCR.Name = "labTotalCR";
            this.labTotalCR.Size = new System.Drawing.Size(40, 14);
            this.labTotalCR.TabIndex = 0;
            this.labTotalCR.Text = "合计 贷";
            // 
            // labResult
            // 
            this.labResult.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.labResult.Appearance.Options.UseFont = true;
            this.labResult.Location = new System.Drawing.Point(36, 162);
            this.labResult.Name = "labResult";
            this.labResult.Size = new System.Drawing.Size(52, 14);
            this.labResult.TabIndex = 0;
            this.labResult.Text = "试算结算";
            // 
            // barClose
            // 
            this.barClose.Location = new System.Drawing.Point(298, 162);
            this.barClose.Name = "barClose";
            this.barClose.Size = new System.Drawing.Size(75, 23);
            this.barClose.TabIndex = 1;
            this.barClose.Text = "关闭(&C)";
            this.barClose.Click += new System.EventHandler(this.barClose_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(3, 113);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(571, 3);
            this.panelControl1.TabIndex = 2;
            // 
            // BeginSpreadsheetPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barClose);
            this.Controls.Add(this.labResult);
            this.Controls.Add(this.labTotalCR);
            this.Controls.Add(this.labTotalDR);
            this.Controls.Add(this.labEQUITY);
            this.Controls.Add(this.labINCOME);
            this.Controls.Add(this.labCOST);
            this.Controls.Add(this.labLIABILITIES);
            this.Controls.Add(this.labASSETS);
            this.Name = "BeginSpreadsheetPart";
            this.Size = new System.Drawing.Size(578, 233);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labASSETS;
        private DevExpress.XtraEditors.LabelControl labLIABILITIES;
        private DevExpress.XtraEditors.LabelControl labCOST;
        private DevExpress.XtraEditors.LabelControl labINCOME;
        private DevExpress.XtraEditors.LabelControl labEQUITY;
        private DevExpress.XtraEditors.LabelControl labTotalDR;
        private DevExpress.XtraEditors.LabelControl labTotalCR;
        private DevExpress.XtraEditors.LabelControl labResult;
        private DevExpress.XtraEditors.SimpleButton barClose;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}
