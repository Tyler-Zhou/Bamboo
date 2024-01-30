namespace ICP.FRM.UI.InquireRates
{
    partial class InquireOceanRatesShipmentPart
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
            shipmentPart = new InquireRatesShipmentPart();
            this.SuspendLayout();
            //
            // shipmentPart
            //
            this.shipmentPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shipmentPart.Location = new System.Drawing.Point(0, 0);
            this.shipmentPart.Name = "shipmentPart";
            this.shipmentPart.Size = new System.Drawing.Size(603, 441);
            this.shipmentPart.TabIndex = 0;
            // 
            // InquireOceanRatesShipmentPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.shipmentPart);
            this.Name = "InquireOceanRatesShipmentPart";
            this.Size = new System.Drawing.Size(593, 393);
            this.ResumeLayout(false);

        }

        #endregion

        private InquireRatesShipmentPart shipmentPart;
    }
}
