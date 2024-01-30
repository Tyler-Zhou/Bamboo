namespace ICP.FRM.UI.SearchRate
{
    partial class SearchTruckSearchPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchTruckSearchPart));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtZipCode = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScope.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.navBarGroupControlContainer1.SuspendLayout();
            this.navBarGroupControlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZipCode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labShipline
            // 
            this.labShipline.Location = new System.Drawing.Point(9, 3);
            // 
            // cmbShipline
            // 
            this.cmbShipline.TabIndex = 0;
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(9, 126);
            // 
            // labCarrier
            // 
            this.labCarrier.Location = new System.Drawing.Point(9, 43);
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(9, 86);
            // 
            // cmbScope
            // 
            this.cmbScope.Location = new System.Drawing.Point(9, 220);
            this.cmbScope.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbScope.Properties.Appearance.Options.UseBackColor = true;
            // 
            // labScope
            // 
            this.labScope.Location = new System.Drawing.Point(9, 204);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "Input_16.png");
            this.imageList1.Images.SetKeyName(1, "EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(2, "WILL BE EFFECTIVE.png");
            this.imageList1.Images.SetKeyName(3, "EXPIRED.png");
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.labelControl1);
            this.navBarGroupControlContainer1.Controls.Add(this.txtZipCode);
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(210, 253);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.txtZipCode, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.labelControl1, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.labScope, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.cmbScope, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.labPOD, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.cmbCarrier, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.labCarrier, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.labPOL, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.cmbShipline, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.labShipline, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.txtPOL, 0);
            this.navBarGroupControlContainer1.Controls.SetChildIndex(this.txtPOD, 0);
            // 
            // txtPOD
            // 
            this.txtPOD.TabIndex = 3;
            // 
            // txtPOL
            // 
            this.txtPOL.TabIndex = 2;
            // 
            // dateMonthControl1
            // 
            this.dateMonthControl1.Size = new System.Drawing.Size(143, 141);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(10, 165);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 14);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "ZipCode";
            // 
            // txtZipCode
            // 
            this.txtZipCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZipCode.Location = new System.Drawing.Point(9, 182);
            this.txtZipCode.Name = "txtZipCode";
            this.txtZipCode.Size = new System.Drawing.Size(194, 21);
            this.txtZipCode.TabIndex = 5;
            // 
            // SearchTruckSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "SearchTruckSearchPart";
            ((System.ComponentModel.ISupportInitialize)(this.cmbScope.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            this.navBarGroupControlContainer2.ResumeLayout(false);
            this.navBarGroupControlContainer2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZipCode.Properties)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtZipCode;



  

    }
}
