namespace ICP.FCM.OceanExport.UI.BL.MBL
{
    partial class AddHblNum
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
            this.labNum = new DevExpress.XtraEditors.LabelControl();
            this.labMonth = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labNum
            // 
            this.labNum.Location = new System.Drawing.Point(36, 30);
            this.labNum.Name = "labNum";
            this.labNum.Size = new System.Drawing.Size(24, 14);
            this.labNum.TabIndex = 14;
            this.labNum.Text = "数量";
            // 
            // labMonth
            // 
            this.labMonth.Location = new System.Drawing.Point(36, 30);
            this.labMonth.Name = "labMonth";
            this.labMonth.Size = new System.Drawing.Size(24, 14);
            this.labMonth.TabIndex = 11;
            this.labMonth.Text = "月份";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(31, 61);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(153, 61);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // numQuantity
            // 
            this.numQuantity.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Location = new System.Drawing.Point(75, 27);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numQuantity.Properties.IsFloatValue = false;
            this.numQuantity.Properties.Mask.EditMask = "N00";
            this.numQuantity.Properties.MaxValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numQuantity.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Size = new System.Drawing.Size(133, 21);
            this.numQuantity.TabIndex = 25;
            // 
            // AddHblNum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.labNum);
            this.Controls.Add(this.labMonth);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Name = "AddHblNum";
            this.Size = new System.Drawing.Size(262, 112);
            this.Controls.SetChildIndex(this.btnClose, 0);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.labMonth, 0);
            this.Controls.SetChildIndex(this.labNum, 0);
            this.Controls.SetChildIndex(this.numQuantity, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labNum;
        private DevExpress.XtraEditors.LabelControl labMonth;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
    }
}
