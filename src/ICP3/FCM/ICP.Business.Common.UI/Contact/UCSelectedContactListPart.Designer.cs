namespace ICP.Business.Common.UI.Contact
{
	partial class UCSelectedContactListPart
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pnlContactList = new DevExpress.XtraEditors.PanelControl();
            this.pnlSelected = new DevExpress.XtraEditors.PanelControl();
            this.rdoCarrier = new System.Windows.Forms.RadioButton();
            this.rdoCustomer = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContactList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSelected)).BeginInit();
            this.pnlSelected.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.pnlContactList);
            this.panelControl1.Controls.Add(this.pnlSelected);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(527, 114);
            this.panelControl1.TabIndex = 2;
            // 
            // pnlContactList
            // 
            this.pnlContactList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlContactList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContactList.Location = new System.Drawing.Point(0, 19);
            this.pnlContactList.Name = "pnlContactList";
            this.pnlContactList.Size = new System.Drawing.Size(527, 95);
            this.pnlContactList.TabIndex = 5;
            // 
            // pnlSelected
            // 
            this.pnlSelected.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlSelected.Controls.Add(this.rdoCarrier);
            this.pnlSelected.Controls.Add(this.rdoCustomer);
            this.pnlSelected.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSelected.Location = new System.Drawing.Point(0, 0);
            this.pnlSelected.Name = "pnlSelected";
            this.pnlSelected.Size = new System.Drawing.Size(527, 19);
            this.pnlSelected.TabIndex = 4;
            // 
            // rdoCarrier
            // 
            this.rdoCarrier.AutoSize = true;
            this.rdoCarrier.Location = new System.Drawing.Point(72, 1);
            this.rdoCarrier.Name = "rdoCarrier";
            this.rdoCarrier.Size = new System.Drawing.Size(65, 16);
            this.rdoCarrier.TabIndex = 5;
            this.rdoCarrier.Text = "Carrier";
            this.rdoCarrier.UseVisualStyleBackColor = true;
             
            // 
            // rdoCustomer
            // 
            this.rdoCustomer.AutoSize = true;
            this.rdoCustomer.Checked = true;
            this.rdoCustomer.Location = new System.Drawing.Point(6, 1);
            this.rdoCustomer.Name = "rdoCustomer";
            this.rdoCustomer.Size = new System.Drawing.Size(71, 16);
            this.rdoCustomer.TabIndex = 4;
            this.rdoCustomer.TabStop = true;
            this.rdoCustomer.Text = "Customer";
            this.rdoCustomer.UseVisualStyleBackColor = true;
            
            // 
            // UCSelectedContactListPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.DoubleBuffered = true;
            this.Name = "UCSelectedContactListPart";
            this.Size = new System.Drawing.Size(527, 114);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContactList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSelected)).EndInit();
            this.pnlSelected.ResumeLayout(false);
            this.pnlSelected.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl pnlSelected;
        private System.Windows.Forms.RadioButton rdoCarrier;
        private System.Windows.Forms.RadioButton rdoCustomer;
        private DevExpress.XtraEditors.PanelControl pnlContactList;

    }
}
