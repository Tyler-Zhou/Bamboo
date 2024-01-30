namespace ICP.Common.UI.TransportFoundation.Flight
{
    partial class FlightEditPart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlightEditPart));
            this.labAirline = new DevExpress.XtraEditors.LabelControl();
            this.labNo = new DevExpress.XtraEditors.LabelControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.stxtAirline = new DevExpress.XtraEditors.ButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAirline.Properties)).BeginInit();
            this.SuspendLayout();          
            // 
            // labAirline
            // 
            resources.ApplyResources(this.labAirline, "labAirline");
            this.labAirline.Name = "labAirline";
            // 
            // labNo
            // 
            resources.ApplyResources(this.labNo, "labNo");
            this.labNo.Name = "labNo";
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.Common.ServiceInterface.DataObjects.FlightInfo);
            // 
            // txtNo
            // 
            this.txtNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "No", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.txtNo, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.txtNo, "txtNo");
            this.txtNo.Name = "txtNo";
            this.txtNo.Properties.MaxLength = 20;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            this.dxErrorProvider1.DataSource = this.bindingSource1;
            // 
            // stxtAirline
            // 
            this.stxtAirline.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "AirlineID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.stxtAirline.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "AirlineName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dxErrorProvider1.SetIconAlignment(this.stxtAirline, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            resources.ApplyResources(this.stxtAirline, "stxtAirline");
            this.stxtAirline.Name = "stxtAirline";
            this.stxtAirline.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtAirline.Properties.Appearance.Options.UseBackColor = true;
            this.stxtAirline.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // FlightEditPart
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stxtAirline);       
            this.Controls.Add(this.labAirline);
            this.Controls.Add(this.labNo);
            this.Controls.Add(this.txtNo);
            this.Name = "FlightEditPart";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtAirline.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
      
        private DevExpress.XtraEditors.LabelControl labAirline;
        private DevExpress.XtraEditors.LabelControl labNo;
        private DevExpress.XtraEditors.TextEdit txtNo; 
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;    
        private DevExpress.XtraEditors.ButtonEdit stxtAirline;
    }
}
