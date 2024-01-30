using ICP.Framework.ClientComponents.Controls;
namespace ICP.FCM.OceanImport.UI.Business.DownLoad
{
    partial class OIBusinessAssignToCS
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
            this.lblAssignToCS = new DevExpress.XtraEditors.LabelControl();
            this.btnSure = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.mcmbCustomerContact = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.SuspendLayout();
            // 
            // lblAssignToCS
            // 
            this.lblAssignToCS.Location = new System.Drawing.Point(13, 15);
            this.lblAssignToCS.Name = "lblAssignToCS";
            this.lblAssignToCS.Size = new System.Drawing.Size(88, 14);
            this.lblAssignToCS.TabIndex = 0;
            this.lblAssignToCS.Text = "Custom Service:";
            // 
            // btnSure
            // 
            this.btnSure.Location = new System.Drawing.Point(116, 56);
            this.btnSure.Name = "btnSure";
            this.btnSure.Size = new System.Drawing.Size(87, 27);
            this.btnSure.TabIndex = 12;
            this.btnSure.Text = "Sure";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(219, 56);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            // 
            // mcmbCustomerContact
            // 
            this.mcmbCustomerContact.EditText = "";
            this.mcmbCustomerContact.EditValue = null;
            this.mcmbCustomerContact.Location = new System.Drawing.Point(107, 12);
            this.mcmbCustomerContact.Name = "mcmbCustomerContact";
            this.mcmbCustomerContact.ReadOnly = false;
            this.mcmbCustomerContact.RefreshButtonToolTip = "";
            this.mcmbCustomerContact.ShowRefreshButton = false;
            this.mcmbCustomerContact.Size = new System.Drawing.Size(213, 21);
            this.mcmbCustomerContact.SpecifiedBackColor = System.Drawing.Color.White;
            this.mcmbCustomerContact.TabIndex = 14;
            this.mcmbCustomerContact.ToolTip = "";
            // 
            // OIBusinessAssignToCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mcmbCustomerContact);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSure);
            this.Controls.Add(this.lblAssignToCS);
            this.Name = "OIBusinessAssignToCS";
            this.Size = new System.Drawing.Size(340, 105);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblAssignToCS;
        private DevExpress.XtraEditors.SimpleButton btnSure;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private MultiSearchCommonBox mcmbCustomerContact;
    }
}
