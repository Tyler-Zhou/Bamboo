using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.FCM.AirExport.UI.Common.Controls
{
    //危险品

    public class DangerousDescriptionPart : CargoDescriptionPart
    {
        private LabelControl labImdgcode;
        private TextEdit txtProperty;
        private BindingSource bindingSource1;
        private IContainer components;
        private TextEdit txtClass;
        private LabelControl labUnno;
        private LabelControl labProperty;
        private LabelControl labClass;
        private TextEdit txtIMDGCode;
        private SpinEdit numUNNo;
    
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            labImdgcode = new LabelControl();
            txtProperty = new TextEdit();
            bindingSource1 = new BindingSource(components);
            txtClass = new TextEdit();
            labUnno = new LabelControl();
            labProperty = new LabelControl();
            labClass = new LabelControl();
            txtIMDGCode = new TextEdit();
            numUNNo = new SpinEdit();
            ((ISupportInitialize)(popupContainerControl1)).BeginInit();
            popupContainerControl1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)(txtProperty.Properties)).BeginInit();
            ((ISupportInitialize)(bindingSource1)).BeginInit();
            ((ISupportInitialize)(txtClass.Properties)).BeginInit();
            ((ISupportInitialize)(txtIMDGCode.Properties)).BeginInit();
            ((ISupportInitialize)(numUNNo.Properties)).BeginInit();
            SuspendLayout();
            // 
            // popupContainerControl1
            // 
            popupContainerControl1.Appearance.BackColor = SystemColors.Control;
            popupContainerControl1.Appearance.Options.UseBackColor = true;
            popupContainerControl1.Size = new Size(249, 147);
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            panel1.Size = new Size(249, 36);
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            panel2.Controls.Add(labImdgcode);
            panel2.Controls.Add(txtProperty);
            panel2.Controls.Add(txtClass);
            panel2.Controls.Add(labUnno);
            panel2.Controls.Add(labProperty);
            panel2.Controls.Add(labClass);
            panel2.Controls.Add(txtIMDGCode);
            panel2.Controls.Add(numUNNo);
            // 
            // labImdgcode
            // 
            labImdgcode.Location = new Point(5, 87);
            labImdgcode.Name = "labImdgcode";
            labImdgcode.Size = new Size(55, 14);
            labImdgcode.TabIndex = 14;
            labImdgcode.Text = "Imdgcode";
            // 
            // txtProperty
            // 
            txtProperty.DataBindings.Add(new Binding("Text", bindingSource1, "Property", true));
            txtProperty.Location = new Point(61, 57);
            txtProperty.Name = "txtProperty";
            txtProperty.Size = new Size(183, 21);
            txtProperty.TabIndex = 10;
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(DangerousCargo);
            // 
            // txtClass
            // 
            txtClass.DataBindings.Add(new Binding("Text", bindingSource1, "Class", true));
            txtClass.Location = new Point(61, 30);
            txtClass.Name = "txtClass";
            txtClass.Size = new Size(183, 21);
            txtClass.TabIndex = 9;
            // 
            // labUnno
            // 
            labUnno.Location = new Point(5, 6);
            labUnno.Name = "labUnno";
            labUnno.Size = new Size(31, 14);
            labUnno.TabIndex = 11;
            labUnno.Text = "UNNo";
            // 
            // labProperty
            // 
            labProperty.Location = new Point(4, 60);
            labProperty.Name = "labProperty";
            labProperty.Size = new Size(47, 14);
            labProperty.TabIndex = 13;
            labProperty.Text = "Property";
            // 
            // labClass
            // 
            labClass.Location = new Point(4, 33);
            labClass.Name = "labClass";
            labClass.Size = new Size(25, 14);
            labClass.TabIndex = 16;
            labClass.Text = "Class";
            // 
            // txtIMDGCode
            // 
            txtIMDGCode.DataBindings.Add(new Binding("Text", bindingSource1, "IMDGCode", true));
            txtIMDGCode.Location = new Point(61, 84);
            txtIMDGCode.Name = "txtIMDGCode";
            txtIMDGCode.Size = new Size(183, 21);
            txtIMDGCode.TabIndex = 15;
            // 
            // numUNNo
            // 
            numUNNo.DataBindings.Add(new Binding("EditValue", bindingSource1, "UNNo", true));
            numUNNo.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numUNNo.Location = new Point(61, 3);
            numUNNo.Name = "numUNNo";
            numUNNo.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numUNNo.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
            numUNNo.Properties.IsFloatValue = false;
            numUNNo.Properties.Mask.EditMask = "N00";
            numUNNo.Properties.Mask.MaskType = MaskType.None;
            numUNNo.Size = new Size(183, 21);
            numUNNo.TabIndex = 12;
            numUNNo.TabStop = false;
            // 
            // DangerousDescriptionPart
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            Name = "DangerousDescriptionPart";
            Size = new Size(19, 21);
            ((ISupportInitialize)(popupContainerControl1)).EndInit();
            popupContainerControl1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)(txtProperty.Properties)).EndInit();
            ((ISupportInitialize)(bindingSource1)).EndInit();
            ((ISupportInitialize)(txtClass.Properties)).EndInit();
            ((ISupportInitialize)(txtIMDGCode.Properties)).EndInit();
            ((ISupportInitialize)(numUNNo.Properties)).EndInit();
            ResumeLayout(false);

        }

        public DangerousDescriptionPart()
            : base()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            Disposed += delegate
            {
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            };


        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        private void SetCnText()
        {
            labClass .Text = "类型";
            labImdgcode.Text = "Imdgcode";
            labProperty .Text = "原型";
            labUnno.Text = "UN号";
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
