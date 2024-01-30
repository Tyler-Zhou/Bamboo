using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.DomesticTrade.UI.Common.Controls
{
    //干货
    public class DryDescriptionPart : CargoDescriptionPart
    {
        private MemoEdit txtDry;
        private BindingSource bindingSource1;
        private IContainer components;
    
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtDry = new MemoEdit();
            bindingSource1 = new BindingSource(components);
            ((ISupportInitialize)(popupContainerControl1)).BeginInit();
            popupContainerControl1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)(txtDry.Properties)).BeginInit();
            ((ISupportInitialize)(bindingSource1)).BeginInit();
            SuspendLayout();
            // 
            // popupContainerControl1
            // 
            popupContainerControl1.Appearance.BackColor = SystemColors.Control;
            popupContainerControl1.Appearance.Options.UseBackColor = true;
            popupContainerControl1.Size = new Size(226, 136);
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            panel1.Location = new Point(0, 100);
            panel1.Size = new Size(226, 36);
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            panel2.Controls.Add(txtDry);
            panel2.Dock = DockStyle.Fill;
            panel2.Size = new Size(226, 100);
            // 
            // txtDry
            // 
            txtDry.DataBindings.Add(new Binding("Text", bindingSource1, "Description", true));
            txtDry.Dock = DockStyle.Fill;
            txtDry.Location = new Point(0, 0);
            txtDry.Name = "txtDry";
            txtDry.Size = new Size(226, 100);
            txtDry.TabIndex = 4;
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(DryCargo);
            // 
            // DryDescriptionPart
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            Name = "DryDescriptionPart";
            Size = new Size(19, 21);
            ((ISupportInitialize)(popupContainerControl1)).EndInit();
            popupContainerControl1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((ISupportInitialize)(txtDry.Properties)).EndInit();
            ((ISupportInitialize)(bindingSource1)).EndInit();
            ResumeLayout(false);

        }

        public DryDescriptionPart()
            : base()
        {
            InitializeComponent();
            Disposed += delegate
            {
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
            
            };
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
