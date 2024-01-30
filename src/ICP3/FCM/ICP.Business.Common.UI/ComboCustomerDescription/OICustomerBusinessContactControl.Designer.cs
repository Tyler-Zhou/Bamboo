namespace ICP.Business.Common.UI
{
    partial class OICustomerBusinessContactControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerBusinessContactControl));
            this.comboBusinessContactPopupContainer = new ComboBusinessContactPopupContainer();
            this.popupContainerEdit = new DevExpress.XtraEditors.PopupContainerEdit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBusinessContactPopupContainer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBusinessContactPopupContainer
            // 
            
            this.comboBusinessContactPopupContainer.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBusinessContactPopupContainer.Location = new System.Drawing.Point(214, 0);
            this.comboBusinessContactPopupContainer.Name = "comboBusinessContactPopupContainer";
            this.comboBusinessContactPopupContainer.Properties.PopupSizeable = false;

            this.comboBusinessContactPopupContainer.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.comboBusinessContactPopupContainer.Properties.ActionButtonIndex = 1;
            this.comboBusinessContactPopupContainer.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight)});
            this.comboBusinessContactPopupContainer.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            
            this.comboBusinessContactPopupContainer.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.comboBusinessContactPopupContainer.Size = new System.Drawing.Size(15, 21);
            this.comboBusinessContactPopupContainer.TabIndex = 0;
            this.comboBusinessContactPopupContainer.Properties.CloseOnLostFocus = false;
            this.comboBusinessContactPopupContainer.Properties.CloseOnOuterMouseClick = false;
            // 
            // popupContainerEdit
            // 
            this.popupContainerEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.popupContainerEdit.Location = new System.Drawing.Point(0, 0);
            this.popupContainerEdit.Name = "popupContainerEdit";
            this.popupContainerEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.popupContainerEdit.Size = new System.Drawing.Size(214, 21);
            this.popupContainerEdit.TabIndex = 1;
            this.popupContainerEdit.Properties.Appearance.BackColor = System.Drawing.Color.LightYellow;
            this.popupContainerEdit.Properties.Appearance.Options.UseBackColor = true;
            // 
            // CustomerBusinessContactControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.popupContainerEdit);
            this.Controls.Add(this.comboBusinessContactPopupContainer);
            this.Name = "CustomerBusinessContactControl";
            this.Size = new System.Drawing.Size(233, 24);
            ((System.ComponentModel.ISupportInitialize)(this.comboBusinessContactPopupContainer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBusinessContactPopupContainer comboBusinessContactPopupContainer;
        private DevExpress.XtraEditors.PopupContainerEdit popupContainerEdit;

    }
}
