using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.OceanExport.UI.Common.Controls
{
    /// <summary>
    /// 冷藏货
    /// </summary>
    public class ReeferDescriptionPart : CargoDescriptionPart
    {
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit numFahrenheir;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SpinEdit numCelsius;
    
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.numFahrenheir = new DevExpress.XtraEditors.SpinEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.numCelsius = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFahrenheir.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCelsius.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.popupContainerControl1.Appearance.Options.UseBackColor = true;
            this.popupContainerControl1.Size = new System.Drawing.Size(176, 91);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Size = new System.Drawing.Size(176, 36);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panel2.Controls.Add(this.labelControl2);
            this.panel2.Controls.Add(this.numFahrenheir);
            this.panel2.Controls.Add(this.labelControl1);
            this.panel2.Controls.Add(this.numCelsius);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Size = new System.Drawing.Size(176, 55);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(153, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(13, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "°C";
            // 
            // numFahrenheir
            // 
            this.numFahrenheir.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "F", true));
            this.numFahrenheir.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numFahrenheir.Location = new System.Drawing.Point(3, 3);
            this.numFahrenheir.Name = "numFahrenheir";
            this.numFahrenheir.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numFahrenheir.Properties.DisplayFormat.FormatString = "F1";
            this.numFahrenheir.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numFahrenheir.Properties.EditFormat.FormatString = "F1";
            this.numFahrenheir.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numFahrenheir.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numFahrenheir.Properties.Mask.EditMask = "F1";
            this.numFahrenheir.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFahrenheir.Size = new System.Drawing.Size(144, 21);
            this.numFahrenheir.TabIndex = 7;
            this.numFahrenheir.TabStop = false;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.ReeferCargo);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(153, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "°F";
            // 
            // numCelsius
            // 
            this.numCelsius.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "C", true));
            this.numCelsius.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numCelsius.Location = new System.Drawing.Point(3, 29);
            this.numCelsius.Name = "numCelsius";
            this.numCelsius.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numCelsius.Properties.DisplayFormat.FormatString = "F1";
            this.numCelsius.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numCelsius.Properties.EditFormat.FormatString = "F1";
            this.numCelsius.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numCelsius.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numCelsius.Properties.Mask.EditMask = "F1";
            this.numCelsius.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCelsius.Size = new System.Drawing.Size(144, 21);
            this.numCelsius.TabIndex = 6;
            this.numCelsius.TabStop = false;
            // 
            // ReeferDescriptionPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "ReeferDescriptionPart";
            this.Size = new System.Drawing.Size(20, 23);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFahrenheir.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCelsius.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        public ReeferDescriptionPart()
            : base()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();

            DevHelper.FormatSpinEdit(this.numCelsius, 1);
            DevHelper.FormatSpinEdit(this.numFahrenheir, 1);
        }

        private void SetCnText()
        {
            
        }

        protected override void SetSource(ICP.FCM.Common.ServiceInterface.DataObjects.CommonCargo commonCargo)
        {
            this.bindingSource1.DataSource = commonCargo;
        }

        protected override void EndEdit()
        {
            this.Validate();
            this.bindingSource1.EndEdit();
        }
    }
}
