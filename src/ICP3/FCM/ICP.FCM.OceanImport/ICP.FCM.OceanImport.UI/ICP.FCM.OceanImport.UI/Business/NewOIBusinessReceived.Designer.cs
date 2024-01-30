namespace ICP.FCM.OceanImport.UI
{
    partial class NewOIBusinessReceived
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
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.dtpReleaseDate = new DevExpress.XtraEditors.DateEdit();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.labOrderDate = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReleaseDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReleaseDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(169, 46);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 27);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "取消(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtpReleaseDate
            // 
            this.dtpReleaseDate.EditValue = null;
            this.dtpReleaseDate.Location = new System.Drawing.Point(105, 15);
            this.dtpReleaseDate.Name = "dtpReleaseDate";
            this.dtpReleaseDate.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.dtpReleaseDate.Properties.Appearance.Options.UseBackColor = true;
            this.dtpReleaseDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpReleaseDate.Properties.Mask.EditMask = "";
            this.dtpReleaseDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtpReleaseDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtpReleaseDate.Size = new System.Drawing.Size(139, 21);
            this.dtpReleaseDate.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(29, 46);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 27);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // labOrderDate
            // 
            this.labOrderDate.Location = new System.Drawing.Point(29, 18);
            this.labOrderDate.Name = "labOrderDate";
            this.labOrderDate.Size = new System.Drawing.Size(48, 14);
            this.labOrderDate.TabIndex = 143;
            this.labOrderDate.Text = "收到正本";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.dtpReleaseDate);
            this.panelControl1.Controls.Add(this.btnOK);
            this.panelControl1.Controls.Add(this.labOrderDate);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(297, 106);
            this.panelControl1.TabIndex = 4;
            // 
            // NewOIBusinessReceived
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "NewOIBusinessReceived";
            this.Size = new System.Drawing.Size(297, 106);
            ((System.ComponentModel.ISupportInitialize)(this.dtpReleaseDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReleaseDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.DateEdit dtpReleaseDate;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl labOrderDate;
        private DevExpress.XtraEditors.PanelControl panelControl1;




    }
}
