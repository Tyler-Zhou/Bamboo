namespace ICP.Business.Common.UI.UpdateETA
{
    partial class UpdateETA
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
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.bindingSourceupdate = new System.Windows.Forms.BindingSource();
            this.chkETA = new DevExpress.XtraEditors.CheckEdit();
            this.chkWareHouse = new DevExpress.XtraEditors.CheckEdit();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.btnUpdate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceupdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWareHouse.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateEdit1
            // 
            this.dateEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSourceupdate, "ETA", true));
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(76, 19);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateEdit1.Size = new System.Drawing.Size(144, 21);
            this.dateEdit1.TabIndex = 1;
            // 
            // bindingSourceupdate
            // 
            this.bindingSourceupdate.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.UpdateETAInfo);
            // 
            // chkETA
            // 
            this.chkETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSourceupdate, "IsETA", true));
            this.chkETA.Location = new System.Drawing.Point(21, 19);
            this.chkETA.Name = "chkETA";
            this.chkETA.Properties.Caption = "ETA";
            this.chkETA.Size = new System.Drawing.Size(49, 19);
            this.chkETA.TabIndex = 3;
            // 
            // chkWareHouse
            // 
            this.chkWareHouse.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSourceupdate, "IsWareHouse", true));
            this.chkWareHouse.Location = new System.Drawing.Point(254, 19);
            this.chkWareHouse.Name = "chkWareHouse";
            this.chkWareHouse.Properties.Caption = "WareHouse";
            this.chkWareHouse.Size = new System.Drawing.Size(89, 19);
            this.chkWareHouse.TabIndex = 4;
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSourceupdate, "WareHouseName", true));
            this.buttonEdit1.Location = new System.Drawing.Point(349, 18);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(144, 21);
            this.buttonEdit1.TabIndex = 5;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(408, 56);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(85, 23);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // UpdateETA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.buttonEdit1);
            this.Controls.Add(this.chkWareHouse);
            this.Controls.Add(this.chkETA);
            this.Controls.Add(this.dateEdit1);
            this.Name = "UpdateETA";
            this.Size = new System.Drawing.Size(506, 93);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceupdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWareHouse.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.CheckEdit chkETA;
        private DevExpress.XtraEditors.CheckEdit chkWareHouse;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.SimpleButton btnUpdate;
        private System.Windows.Forms.BindingSource bindingSourceupdate;
    }
}
