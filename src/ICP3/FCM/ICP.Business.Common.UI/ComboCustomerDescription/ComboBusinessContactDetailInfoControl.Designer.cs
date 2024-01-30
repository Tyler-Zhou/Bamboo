namespace ICP.Business.Common.UI
{
    /// <summary>
    /// ComboCustomerDescriptionControl
    /// </summary>
    partial class ComboBusinessContactDetailInfoControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComboBusinessContactDetailInfoControl));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.customerPopupContainerEdit1 = new ComboBusinessContactPopupContainer();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerPopupContainerEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lookUpEdit1.Location = new System.Drawing.Point(0, 0);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.lookUpEdit1.Properties.Appearance.Options.UseBackColor = true;
            
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            });
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name2", "Name2", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.PopupWidth = 400;
            this.lookUpEdit1.Properties.ShowFooter = false;
            this.lookUpEdit1.Properties.ShowHeader = false;
            this.lookUpEdit1.Properties.ShowLines = false;
            this.lookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEdit1.Size = new System.Drawing.Size(214, 21);
            this.lookUpEdit1.TabIndex = 2;
            // 
            // customerPopupContainerEdit1
            // 
            
            this.customerPopupContainerEdit1.Dock = System.Windows.Forms.DockStyle.Right;
            this.customerPopupContainerEdit1.Location = new System.Drawing.Point(214, 0);
            this.customerPopupContainerEdit1.Margin = new System.Windows.Forms.Padding(0);
            this.customerPopupContainerEdit1.Name = "customerPopupContainerEdit1";
            this.customerPopupContainerEdit1.PopupFormPosition = ICP.Framework.ClientComponents.Controls.PopupFormPosition.Right;
            this.customerPopupContainerEdit1.Properties.ActionButtonIndex = 1;
            this.customerPopupContainerEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.customerPopupContainerEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.customerPopupContainerEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.SpinRight, "", 12, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
            this.customerPopupContainerEdit1.Properties.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Simple;
            this.customerPopupContainerEdit1.Properties.PopupSizeable = false;
            this.customerPopupContainerEdit1.Properties.ShowPopupCloseButton = false;
            this.customerPopupContainerEdit1.Properties.CloseOnOuterMouseClick = false;
            this.customerPopupContainerEdit1.Properties.CloseOnLostFocus = false;
            this.customerPopupContainerEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.customerPopupContainerEdit1.Size = new System.Drawing.Size(19, 21);
            this.customerPopupContainerEdit1.TabIndex = 0;
            // 
            // ComboBusinessContactDetailInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lookUpEdit1);
            this.Controls.Add(this.customerPopupContainerEdit1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ComboBusinessContactDetailInfoControl";
            this.Size = new System.Drawing.Size(233, 24);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customerPopupContainerEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBusinessContactPopupContainer customerPopupContainerEdit1;
        public DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
    }
}
