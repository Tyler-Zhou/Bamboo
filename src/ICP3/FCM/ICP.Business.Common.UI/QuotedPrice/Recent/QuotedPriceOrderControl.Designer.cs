namespace ICP.Business.Common.UI.QuotedPrice.Recent
{
    partial class QuotedPriceOrderControl
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
            this.popupContainerEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.lblSpace = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerEdit
            // 
            this.popupContainerEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupContainerEdit.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEdit.Name = "popupContainerEdit";
            this.popupContainerEdit.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.popupContainerEdit.Properties.Appearance.Options.UseBackColor = true;
            this.popupContainerEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit.Size = new System.Drawing.Size(239, 21);
            this.popupContainerEdit.TabIndex = 4;
            // 
            // btnView
            // 
            this.btnView.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnView.Enabled = false;
            this.btnView.Location = new System.Drawing.Point(251, 0);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 22);
            this.btnView.TabIndex = 5;
            this.btnView.Text = "View";
            // 
            // lblSpace
            // 
            this.lblSpace.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSpace.Location = new System.Drawing.Point(239, 0);
            this.lblSpace.Name = "lblSpace";
            this.lblSpace.Size = new System.Drawing.Size(12, 14);
            this.lblSpace.TabIndex = 6;
            this.lblSpace.Text = "   ";
            // 
            // QuotedPriceOrderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerEdit);
            this.Controls.Add(this.lblSpace);
            this.Controls.Add(this.btnView);
            this.Name = "QuotedPriceOrderControl";
            this.Size = new System.Drawing.Size(326, 22);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit;
        private DevExpress.XtraEditors.SimpleButton btnView;
        private DevExpress.XtraEditors.LabelControl lblSpace;

    }
}
