using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.DomesticTrade.UI.Common.Controls
{
    //冷藏货

    public class ReeferDescriptionPart : CargoDescriptionPart
    {
        private LabelControl labelControl2;
        private SpinEdit numFahrenheir;
        private BindingSource bindingSource1;
        private IContainer components;
        private LabelControl labelControl1;
        private SpinEdit numCelsius;
    
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            labelControl2 = new LabelControl();
            numFahrenheir = new SpinEdit();
            bindingSource1 = new BindingSource(components);
            labelControl1 = new LabelControl();
            numCelsius = new SpinEdit();
            ((ISupportInitialize)(popupContainerControl1)).BeginInit();
            popupContainerControl1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)(numFahrenheir.Properties)).BeginInit();
            ((ISupportInitialize)(bindingSource1)).BeginInit();
            ((ISupportInitialize)(numCelsius.Properties)).BeginInit();
            SuspendLayout();
            // 
            // popupContainerControl1
            // 
            popupContainerControl1.Appearance.BackColor = SystemColors.Control;
            popupContainerControl1.Appearance.Options.UseBackColor = true;
            popupContainerControl1.Size = new Size(176, 91);
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            panel1.Location = new Point(0, 55);
            panel1.Size = new Size(176, 36);
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            panel2.Controls.Add(labelControl2);
            panel2.Controls.Add(numFahrenheir);
            panel2.Controls.Add(labelControl1);
            panel2.Controls.Add(numCelsius);
            panel2.Dock = DockStyle.Fill;
            panel2.Size = new Size(176, 55);
            // 
            // labelControl2
            // 
            labelControl2.Location = new Point(153, 32);
            labelControl2.Name = "labelControl2";
            labelControl2.Size = new Size(13, 14);
            labelControl2.TabIndex = 8;
            labelControl2.Text = "°C";
            // 
            // numFahrenheir
            // 
            numFahrenheir.DataBindings.Add(new Binding("Text", bindingSource1, "F", true));
            numFahrenheir.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numFahrenheir.Location = new Point(3, 3);
            numFahrenheir.Name = "numFahrenheir";
            numFahrenheir.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numFahrenheir.Properties.DisplayFormat.FormatString = "F1";
            numFahrenheir.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            numFahrenheir.Properties.EditFormat.FormatString = "F1";
            numFahrenheir.Properties.EditFormat.FormatType = FormatType.Numeric;
            numFahrenheir.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
            numFahrenheir.Properties.Mask.EditMask = "F1";
            numFahrenheir.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            numFahrenheir.Size = new Size(144, 21);
            numFahrenheir.TabIndex = 7;
            numFahrenheir.TabStop = false;
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(ReeferCargo);
            // 
            // labelControl1
            // 
            labelControl1.Location = new Point(153, 6);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(12, 14);
            labelControl1.TabIndex = 9;
            labelControl1.Text = "°F";
            // 
            // numCelsius
            // 
            numCelsius.DataBindings.Add(new Binding("Text", bindingSource1, "C", true));
            numCelsius.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numCelsius.Location = new Point(3, 29);
            numCelsius.Name = "numCelsius";
            numCelsius.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numCelsius.Properties.DisplayFormat.FormatString = "F1";
            numCelsius.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            numCelsius.Properties.EditFormat.FormatString = "F1";
            numCelsius.Properties.EditFormat.FormatType = FormatType.Numeric;
            numCelsius.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
            numCelsius.Properties.Mask.EditMask = "F1";
            numCelsius.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            numCelsius.Size = new Size(144, 21);
            numCelsius.TabIndex = 6;
            numCelsius.TabStop = false;
            // 
            // ReeferDescriptionPart
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            Name = "ReeferDescriptionPart";
            Size = new Size(20, 23);
            ((ISupportInitialize)(popupContainerControl1)).EndInit();
            popupContainerControl1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)(numFahrenheir.Properties)).EndInit();
            ((ISupportInitialize)(bindingSource1)).EndInit();
            ((ISupportInitialize)(numCelsius.Properties)).EndInit();
            ResumeLayout(false);

        }

        public ReeferDescriptionPart()
            : base()
        {
            InitializeComponent();
            Disposed += delegate
            {
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
            
            };

            DevHelper.FormatSpinEdit(numCelsius, 1);
            DevHelper.FormatSpinEdit(numFahrenheir, 1);
        }



        protected override void SetSource(CommonCargo commonCargo)
        {
            bindingSource1.DataSource = commonCargo;
        }

        protected override void EndEdit()
        {
            Validate();
            bindingSource1.EndEdit();
        }
    }
}
