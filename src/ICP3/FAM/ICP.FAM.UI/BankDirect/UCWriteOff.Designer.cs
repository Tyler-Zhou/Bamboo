namespace ICP.FAM.UI
{
    partial class UCWriteOff
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
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.txtWriteOffNO = new DevExpress.XtraEditors.TextEdit();
            this.labWriteOffNO = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWriteOffNO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtWriteOffNO);
            this.pnlMain.Controls.Add(this.labWriteOffNO);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(426, 75);
            this.pnlMain.TabIndex = 22;
            // 
            // txtWriteOffNO
            // 
            this.txtWriteOffNO.Location = new System.Drawing.Point(155, 30);
            this.txtWriteOffNO.Name = "txtWriteOffNO";
            this.txtWriteOffNO.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtWriteOffNO.Properties.Appearance.Options.UseBackColor = true;
            this.txtWriteOffNO.Properties.MaxLength = 200;
            this.txtWriteOffNO.Size = new System.Drawing.Size(228, 21);
            this.txtWriteOffNO.TabIndex = 56;
            // 
            // labWriteOffNO
            // 
            this.labWriteOffNO.Location = new System.Drawing.Point(49, 33);
            this.labWriteOffNO.Name = "labWriteOffNO";
            this.labWriteOffNO.Size = new System.Drawing.Size(76, 14);
            this.labWriteOffNO.TabIndex = 57;
            this.labWriteOffNO.Text = "Write Off NO.";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(244, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel(&C)";
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 75);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(426, 37);
            this.pnlBottom.TabIndex = 23;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(82, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Association(&A)";
            // 
            // UCWriteOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlBottom);
            this.Name = "UCWriteOff";
            this.Size = new System.Drawing.Size(426, 112);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtWriteOffNO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtWriteOffNO;
        private DevExpress.XtraEditors.LabelControl labWriteOffNO;
    }
}
