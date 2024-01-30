using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.OtherBusiness.UI.Common.Controls
{
    //危险品
    public class DangerousDescriptionPart : CargoDescriptionPart
    {
        private DevExpress.XtraEditors.LabelControl labImdgcode;
        private DevExpress.XtraEditors.TextEdit txtProperty;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraEditors.TextEdit txtClass;
        private DevExpress.XtraEditors.LabelControl labUnno;
        private DevExpress.XtraEditors.LabelControl labProperty;
        private DevExpress.XtraEditors.LabelControl labClass;
        private DevExpress.XtraEditors.TextEdit txtIMDGCode;
        private DevExpress.XtraEditors.SpinEdit numUNNo;
    
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labImdgcode = new DevExpress.XtraEditors.LabelControl();
            this.txtProperty = new DevExpress.XtraEditors.TextEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtClass = new DevExpress.XtraEditors.TextEdit();
            this.labUnno = new DevExpress.XtraEditors.LabelControl();
            this.labProperty = new DevExpress.XtraEditors.LabelControl();
            this.labClass = new DevExpress.XtraEditors.LabelControl();
            this.txtIMDGCode = new DevExpress.XtraEditors.TextEdit();
            this.numUNNo = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProperty.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIMDGCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUNNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.popupContainerControl1.Appearance.Options.UseBackColor = true;
            this.popupContainerControl1.Size = new System.Drawing.Size(249, 147);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panel1.Size = new System.Drawing.Size(249, 36);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panel2.Controls.Add(this.labImdgcode);
            this.panel2.Controls.Add(this.txtProperty);
            this.panel2.Controls.Add(this.txtClass);
            this.panel2.Controls.Add(this.labUnno);
            this.panel2.Controls.Add(this.labProperty);
            this.panel2.Controls.Add(this.labClass);
            this.panel2.Controls.Add(this.txtIMDGCode);
            this.panel2.Controls.Add(this.numUNNo);
            // 
            // labImdgcode
            // 
            this.labImdgcode.Location = new System.Drawing.Point(5, 87);
            this.labImdgcode.Name = "labImdgcode";
            this.labImdgcode.Size = new System.Drawing.Size(55, 14);
            this.labImdgcode.TabIndex = 14;
            this.labImdgcode.Text = "Imdgcode";
            // 
            // txtProperty
            // 
            this.txtProperty.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Property", true));
            this.txtProperty.Location = new System.Drawing.Point(61, 57);
            this.txtProperty.Name = "txtProperty";
            this.txtProperty.Size = new System.Drawing.Size(183, 21);
            this.txtProperty.TabIndex = 10;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.DangerousCargo);
            // 
            // txtClass
            // 
            this.txtClass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Class", true));
            this.txtClass.Location = new System.Drawing.Point(61, 30);
            this.txtClass.Name = "txtClass";
            this.txtClass.Size = new System.Drawing.Size(183, 21);
            this.txtClass.TabIndex = 9;
            // 
            // labUnno
            // 
            this.labUnno.Location = new System.Drawing.Point(5, 6);
            this.labUnno.Name = "labUnno";
            this.labUnno.Size = new System.Drawing.Size(31, 14);
            this.labUnno.TabIndex = 11;
            this.labUnno.Text = "UNNo";
            // 
            // labProperty
            // 
            this.labProperty.Location = new System.Drawing.Point(4, 60);
            this.labProperty.Name = "labProperty";
            this.labProperty.Size = new System.Drawing.Size(47, 14);
            this.labProperty.TabIndex = 13;
            this.labProperty.Text = "Property";
            // 
            // labClass
            // 
            this.labClass.Location = new System.Drawing.Point(4, 33);
            this.labClass.Name = "labClass";
            this.labClass.Size = new System.Drawing.Size(25, 14);
            this.labClass.TabIndex = 16;
            this.labClass.Text = "Class";
            // 
            // txtIMDGCode
            // 
            this.txtIMDGCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "IMDGCode", true));
            this.txtIMDGCode.Location = new System.Drawing.Point(61, 84);
            this.txtIMDGCode.Name = "txtIMDGCode";
            this.txtIMDGCode.Size = new System.Drawing.Size(183, 21);
            this.txtIMDGCode.TabIndex = 15;
            // 
            // numUNNo
            // 
            this.numUNNo.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "UNNo", true));
            this.numUNNo.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numUNNo.Location = new System.Drawing.Point(61, 3);
            this.numUNNo.Name = "numUNNo";
            this.numUNNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numUNNo.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numUNNo.Properties.IsFloatValue = false;
            this.numUNNo.Properties.Mask.EditMask = "N00";
            this.numUNNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numUNNo.Size = new System.Drawing.Size(183, 21);
            this.numUNNo.TabIndex = 12;
            this.numUNNo.TabStop = false;
            // 
            // DangerousDescriptionPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "DangerousDescriptionPart";
            this.Size = new System.Drawing.Size(19, 21);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtProperty.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClass.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIMDGCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUNNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        public DangerousDescriptionPart()
            : base()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            labClass .Text = "类型";
            labImdgcode.Text = "Imdgcode";
            labProperty .Text = "原型";
            labUnno.Text = "UN号";
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
