namespace ICP.Business.Common.UI.Contact
{
    partial class UCContactListWithToolbar
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
            this.ucCustomerToolbar = new ICP.Business.Common.UI.Contact.UCCustomerToolbar();            
            this.ucCustomerList = new ICP.Business.Common.UI.Contact.UCCustomerList();
            this.SuspendLayout();
            // 
            // ucCustomerToolbar
            // 
            this.ucCustomerToolbar.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Customer;            
            this.ucCustomerToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucCustomerToolbar.Location = new System.Drawing.Point(0, 0);
            this.ucCustomerToolbar.Name = "ucCustomerToolbar";
            this.ucCustomerToolbar.ReadOnly = true;
            this.ucCustomerToolbar.Size = new System.Drawing.Size(697, 28);
            this.ucCustomerToolbar.TabIndex = 0;
            // 
            // ucCustomerList
            // 
            this.ucCustomerList.AllowColumnEditable = true;
            this.ucCustomerList.CompanyID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ucCustomerList.conditions = new bool[] {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false};
            this.ucCustomerList.ContactStage = ICP.Framework.CommonLibrary.Common.ContactStage.Unknown;
            this.ucCustomerList.ContactType = ICP.Framework.CommonLibrary.Common.ContactType.Unknown;
            this.ucCustomerList.CustomerID = new System.Guid("00000000-0000-0000-0000-000000000000");
            this.ucCustomerList.CustomerName = null;
            this.ucCustomerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCustomerList.IsChanged = false;
            this.ucCustomerList.IsShowCaption = false;
            this.ucCustomerList.IsValidatePass = false;
            this.ucCustomerList.Location = new System.Drawing.Point(0, 28);
            this.ucCustomerList.Name = "ucCustomerList";
            this.ucCustomerList.OperationContext = null;
            this.ucCustomerList.Size = new System.Drawing.Size(697, 361);
            this.ucCustomerList.TabIndex = 1;
            this.ucCustomerList.ValidateReturnErrorString = false;
            // 
            // UCContactListWithToolbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucCustomerList);
            this.Controls.Add(this.ucCustomerToolbar);
            this.Name = "UCContactListWithToolbar";
            this.Size = new System.Drawing.Size(697, 389);
            this.ResumeLayout(false);

        }

        #endregion

        private UCCustomerToolbar ucCustomerToolbar;
        private UCCustomerList ucCustomerList;
    }
}
