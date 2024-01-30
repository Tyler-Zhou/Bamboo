namespace ICP.FRM.UI.SearchRate
{
    partial class SearchTruckRateInfoPart
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
            this.bsList = new System.Windows.Forms.BindingSource(this.components);
            this.labRespondBy = new DevExpress.XtraEditors.LabelControl();
            this.labRemark = new DevExpress.XtraEditors.LabelControl();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.txtRespondBy = new DevExpress.XtraEditors.TextEdit();
            this.panel1 = new DevExpress.XtraEditors.XtraScrollableControl();
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRespondBy.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsList
            // 
            this.bsList.DataSource = typeof(ICP.FRM.ServiceInterface.DataObjects.SearchTruckRateList);
            // 
            // labRespondBy
            // 
            this.labRespondBy.Location = new System.Drawing.Point(6, 9);
            this.labRespondBy.Name = "labRespondBy";
            this.labRespondBy.Size = new System.Drawing.Size(60, 14);
            this.labRespondBy.TabIndex = 0;
            this.labRespondBy.Text = "RespondBy";
            // 
            // labRemark
            // 
            this.labRemark.Location = new System.Drawing.Point(6, 35);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new System.Drawing.Size(40, 14);
            this.labRemark.TabIndex = 1;
            this.labRemark.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtRemark.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "Remark", true));
            this.txtRemark.Location = new System.Drawing.Point(91, 33);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Properties.ReadOnly = true;
            this.txtRemark.Size = new System.Drawing.Size(471, 122);
            this.txtRemark.TabIndex = 2;
            // 
            // txtRespondBy
            // 
            this.txtRespondBy.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsList, "RespondBy", true));
            this.txtRespondBy.Location = new System.Drawing.Point(91, 6);
            this.txtRespondBy.Name = "txtRespondBy";
            this.txtRespondBy.Properties.ReadOnly = true;
            this.txtRespondBy.Size = new System.Drawing.Size(471, 21);
            this.txtRespondBy.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtRespondBy);
            this.panel1.Controls.Add(this.labRespondBy);
            this.panel1.Controls.Add(this.txtRemark);
            this.panel1.Controls.Add(this.labRemark);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(622, 160);
            this.panel1.TabIndex = 4;
            // 
            // SearchTruckRateInfoPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panel1);
            this.IsMultiLanguage = false;
            this.Name = "SearchTruckRateInfoPart";
            this.Size = new System.Drawing.Size(622, 160);
            ((System.ComponentModel.ISupportInitialize)(this.bsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRespondBy.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bsList;
        private DevExpress.XtraEditors.LabelControl labRespondBy;
        private DevExpress.XtraEditors.LabelControl labRemark;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.TextEdit txtRespondBy;
        private DevExpress.XtraEditors.XtraScrollableControl panel1;

    }
}
