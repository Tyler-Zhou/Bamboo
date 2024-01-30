using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OtherBusiness.UI.Common.Controls
{
    //干货
    public class DryDescriptionPart : CargoDescriptionPart
    {
        private DevExpress.XtraEditors.MemoEdit txtDry;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.ComponentModel.IContainer components;
    
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtDry = new DevExpress.XtraEditors.MemoEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.popupContainerControl1.Appearance.Options.UseBackColor = true;
            this.popupContainerControl1.Size = new System.Drawing.Size(226, 136);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panel1.Location = new System.Drawing.Point(0, 100);
            this.panel1.Size = new System.Drawing.Size(226, 36);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panel2.Controls.Add(this.txtDry);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Size = new System.Drawing.Size(226, 100);
            // 
            // txtDry
            // 
            this.txtDry.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Description", true));
            this.txtDry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDry.Location = new System.Drawing.Point(0, 0);
            this.txtDry.Name = "txtDry";
            this.txtDry.Size = new System.Drawing.Size(226, 100);
            this.txtDry.TabIndex = 4;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.DryCargo);
            // 
            // DryDescriptionPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "DryDescriptionPart";
            this.Size = new System.Drawing.Size(19, 21);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        public DryDescriptionPart()
            : base()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
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
