namespace ICP.Common.UI.TransportFoundation.VesselVoyage
{
    partial class VoyageEditPart
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
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labVessel = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.cboVessel = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVessel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.VoyageInfo);
            // 
            // labVessel
            // 
            this.labVessel.Location = new System.Drawing.Point(7, 13);
            this.labVessel.Name = "labVessel";
            this.labVessel.Size = new System.Drawing.Size(34, 14);
            this.labVessel.TabIndex = 0;
            this.labVessel.Text = "Vessel";
            // 
            // labNo
            // 
            this.labNo.Location = new System.Drawing.Point(7, 40);
            this.labNo.Name = "labNo";
            this.labNo.Size = new System.Drawing.Size(15, 14);
            this.labNo.TabIndex = 45;
            this.labNo.Text = "No";
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "No", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.txtNo.Location = new System.Drawing.Point(75, 37);
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.MaxLength = 20;
            this.txtNo.Size = new System.Drawing.Size(232, 21);
            this.txtNo.TabIndex = 1;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // cboVessel
            // 
          
            this.dxErrorProvider1.SetIconAlignment(this.cboVessel, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.cboVessel.Location = new System.Drawing.Point(75, 11);
            this.cboVessel.Properties.NullText = string.Empty;
             
            this.cboVessel.Name = "cboVessel";
            this.cboVessel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
           });
            this.cboVessel.Size = new System.Drawing.Size(232, 21);
            this.cboVessel.TabIndex = 0;
            this.cboVessel.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboVessel_ButtonClick);
            
            
            // 
            // VoyageEditPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.cboVessel);
            this.Controls.Add(this.labVessel);
            this.Controls.Add(this.labNo);
            this.Controls.Add(this.txtNo);
            this.Name = "VoyageEditPart";
            this.Size = new System.Drawing.Size(314, 221);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboVessel.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }   

        #endregion

        private DevExpress.XtraEditors.LabelControl labVessel;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.TextEdit txtNo;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.LookUpEdit cboVessel;
    }
}
