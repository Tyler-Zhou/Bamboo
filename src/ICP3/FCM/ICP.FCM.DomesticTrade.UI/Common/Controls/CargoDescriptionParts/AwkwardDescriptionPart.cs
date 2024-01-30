using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.DomesticTrade.UI.Common.Controls
{
    //特种货

    public class AwkwardDescriptionPart : CargoDescriptionPart
    {
        private ComboBoxEdit cmbNetWeightUnit;
        private BindingSource bindingSource1;
        private IContainer components;
        private SpinEdit numHeight;
        private MemoEdit txtDetails;
        private ComboBoxEdit cmbGrossWeightUnit;
        private MemoEdit txtCommodity;
        private LabelControl labGrossWeight;
        private LabelControl labHeight;
        private SpinEdit numLength;
        private SpinEdit numQuantity;
        private SpinEdit numNetWeight;
        private LabelControl labWidth;
        private SpinEdit numWidth;
        private LabelControl labCommodity;
        private LabelControl labNetWeight;
        private LabelControl labQuantity;
        private SpinEdit numGrossWeight;
        private LabelControl labDetails;
        private LabelControl labLength;
    
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            cmbNetWeightUnit = new ComboBoxEdit();
            bindingSource1 = new BindingSource(components);
            cmbGrossWeightUnit = new ComboBoxEdit();
            labHeight = new LabelControl();
            labWidth = new LabelControl();
            labQuantity = new LabelControl();
            labLength = new LabelControl();
            labNetWeight = new LabelControl();
            numNetWeight = new SpinEdit();
            labGrossWeight = new LabelControl();
            txtDetails = new MemoEdit();
            txtCommodity = new MemoEdit();
            numLength = new SpinEdit();
            numWidth = new SpinEdit();
            numGrossWeight = new SpinEdit();
            labDetails = new LabelControl();
            labCommodity = new LabelControl();
            numHeight = new SpinEdit();
            numQuantity = new SpinEdit();
            ((ISupportInitialize)(popupContainerControl1)).BeginInit();
            popupContainerControl1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)(cmbNetWeightUnit.Properties)).BeginInit();
            ((ISupportInitialize)(bindingSource1)).BeginInit();
            ((ISupportInitialize)(cmbGrossWeightUnit.Properties)).BeginInit();
            ((ISupportInitialize)(numNetWeight.Properties)).BeginInit();
            ((ISupportInitialize)(txtDetails.Properties)).BeginInit();
            ((ISupportInitialize)(txtCommodity.Properties)).BeginInit();
            ((ISupportInitialize)(numLength.Properties)).BeginInit();
            ((ISupportInitialize)(numWidth.Properties)).BeginInit();
            ((ISupportInitialize)(numGrossWeight.Properties)).BeginInit();
            ((ISupportInitialize)(numHeight.Properties)).BeginInit();
            ((ISupportInitialize)(numQuantity.Properties)).BeginInit();
            SuspendLayout();
            // 
            // popupContainerControl1
            // 
            popupContainerControl1.Appearance.BackColor = SystemColors.Control;
            popupContainerControl1.Appearance.Options.UseBackColor = true;
            popupContainerControl1.Size = new Size(426, 253);
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            panel1.Location = new Point(0, 217);
            panel1.Size = new Size(426, 36);
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            panel2.Controls.Add(cmbNetWeightUnit);
            panel2.Controls.Add(numHeight);
            panel2.Controls.Add(txtDetails);
            panel2.Controls.Add(cmbGrossWeightUnit);
            panel2.Controls.Add(txtCommodity);
            panel2.Controls.Add(labGrossWeight);
            panel2.Controls.Add(labHeight);
            panel2.Controls.Add(numLength);
            panel2.Controls.Add(numQuantity);
            panel2.Controls.Add(numNetWeight);
            panel2.Controls.Add(labWidth);
            panel2.Controls.Add(numWidth);
            panel2.Controls.Add(labCommodity);
            panel2.Controls.Add(labNetWeight);
            panel2.Controls.Add(labQuantity);
            panel2.Controls.Add(numGrossWeight);
            panel2.Controls.Add(labDetails);
            panel2.Controls.Add(labLength);
            panel2.Dock = DockStyle.Fill;
            panel2.Size = new Size(426, 217);
            // 
            // cmbNetWeightUnit
            // 
            cmbNetWeightUnit.DataBindings.Add(new Binding("Text", bindingSource1, "NetWeightUnit", true));
            cmbNetWeightUnit.Location = new Point(351, 57);
            cmbNetWeightUnit.Name = "cmbNetWeightUnit";
            cmbNetWeightUnit.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton(ButtonPredefines.Combo)});
            cmbNetWeightUnit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            cmbNetWeightUnit.Size = new Size(68, 21);
            cmbNetWeightUnit.TabIndex = 40;
            // 
            // bindingSource1
            // 
            bindingSource1.DataSource = typeof(AwkwardCargo);
            // 
            // cmbGrossWeightUnit
            // 
            cmbGrossWeightUnit.DataBindings.Add(new Binding("Text", bindingSource1, "GrossWeightUnit", true));
            cmbGrossWeightUnit.Location = new Point(351, 30);
            cmbGrossWeightUnit.Name = "cmbGrossWeightUnit";
            cmbGrossWeightUnit.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton(ButtonPredefines.Combo)});
            cmbGrossWeightUnit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            cmbGrossWeightUnit.Size = new Size(68, 21);
            cmbGrossWeightUnit.TabIndex = 41;
            // 
            // labHeight
            // 
            labHeight.Location = new Point(3, 60);
            labHeight.Name = "labHeight";
            labHeight.Size = new Size(36, 14);
            labHeight.TabIndex = 36;
            labHeight.Text = "Height";
            // 
            // labWidth
            // 
            labWidth.Location = new Point(3, 33);
            labWidth.Name = "labWidth";
            labWidth.Size = new Size(33, 14);
            labWidth.TabIndex = 35;
            labWidth.Text = "Width";
            // 
            // labQuantity
            // 
            labQuantity.Location = new Point(213, 6);
            labQuantity.Name = "labQuantity";
            labQuantity.Size = new Size(47, 14);
            labQuantity.TabIndex = 34;
            labQuantity.Text = "Quantity";
            // 
            // labLength
            // 
            labLength.Location = new Point(2, 6);
            labLength.Name = "labLength";
            labLength.Size = new Size(39, 14);
            labLength.TabIndex = 39;
            labLength.Text = "Length";
            // 
            // labNetWeight
            // 
            labNetWeight.Location = new Point(213, 60);
            labNetWeight.Name = "labNetWeight";
            labNetWeight.Size = new Size(60, 14);
            labNetWeight.TabIndex = 38;
            labNetWeight.Text = "NetWeight";
            // 
            // numNetWeight
            // 
            numNetWeight.DataBindings.Add(new Binding("EditValue", bindingSource1, "NetWeight", true));
            numNetWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numNetWeight.Location = new Point(275, 57);
            numNetWeight.Name = "numNetWeight";
            numNetWeight.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numNetWeight.Size = new Size(75, 21);
            numNetWeight.TabIndex = 33;
            // 
            // labGrossWeight
            // 
            labGrossWeight.Location = new Point(213, 33);
            labGrossWeight.Name = "labGrossWeight";
            labGrossWeight.Size = new Size(40, 14);
            labGrossWeight.TabIndex = 37;
            labGrossWeight.Text = "Weight";
            // 
            // txtDetails
            // 
            txtDetails.DataBindings.Add(new Binding("Text", bindingSource1, "Details", true));
            txtDetails.Location = new Point(65, 152);
            txtDetails.Name = "txtDetails";
            txtDetails.Size = new Size(354, 61);
            txtDetails.TabIndex = 28;
            // 
            // txtCommodity
            // 
            txtCommodity.DataBindings.Add(new Binding("Text", bindingSource1, "Commodity", true));
            txtCommodity.Location = new Point(65, 85);
            txtCommodity.Name = "txtCommodity";
            txtCommodity.Size = new Size(354, 61);
            txtCommodity.TabIndex = 29;
            // 
            // numLength
            // 
            numLength.DataBindings.Add(new Binding("EditValue", bindingSource1, "Length", true));
            numLength.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numLength.Location = new Point(65, 3);
            numLength.Name = "numLength";
            numLength.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numLength.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
            numLength.Properties.Mask.MaskType = MaskType.None;
            numLength.Size = new Size(144, 21);
            numLength.TabIndex = 27;
            numLength.TabStop = false;
            // 
            // numWidth
            // 
            numWidth.DataBindings.Add(new Binding("EditValue", bindingSource1, "Width", true));
            numWidth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numWidth.Location = new Point(65, 30);
            numWidth.Name = "numWidth";
            numWidth.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numWidth.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
            numWidth.Properties.Mask.MaskType = MaskType.None;
            numWidth.Size = new Size(144, 21);
            numWidth.TabIndex = 24;
            numWidth.TabStop = false;
            // 
            // numGrossWeight
            // 
            numGrossWeight.DataBindings.Add(new Binding("EditValue", bindingSource1, "GrossWeight", true));
            numGrossWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numGrossWeight.Location = new Point(275, 30);
            numGrossWeight.Name = "numGrossWeight";
            numGrossWeight.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numGrossWeight.Size = new Size(75, 21);
            numGrossWeight.TabIndex = 32;
            // 
            // labDetails
            // 
            labDetails.Location = new Point(2, 155);
            labDetails.Name = "labDetails";
            labDetails.Size = new Size(35, 14);
            labDetails.TabIndex = 30;
            labDetails.Text = "Details";
            // 
            // labCommodity
            // 
            labCommodity.Location = new Point(2, 87);
            labCommodity.Name = "labCommodity";
            labCommodity.Size = new Size(61, 14);
            labCommodity.TabIndex = 31;
            labCommodity.Text = "Commodity";
            // 
            // numHeight
            // 
            numHeight.DataBindings.Add(new Binding("EditValue", bindingSource1, "Height", true));
            numHeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numHeight.Location = new Point(65, 57);
            numHeight.Name = "numHeight";
            numHeight.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numHeight.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
            numHeight.Properties.Mask.MaskType = MaskType.None;
            numHeight.Size = new Size(144, 21);
            numHeight.TabIndex = 25;
            numHeight.TabStop = false;
            // 
            // numQuantity
            // 
            numQuantity.DataBindings.Add(new Binding("EditValue", bindingSource1, "Quantity", true));
            numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            numQuantity.Location = new Point(275, 3);
            numQuantity.Name = "numQuantity";
            numQuantity.Properties.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            numQuantity.Properties.EditValueChangedFiringMode = EditValueChangedFiringMode.Default;
            numQuantity.Properties.Mask.MaskType = MaskType.None;
            numQuantity.Size = new Size(144, 21);
            numQuantity.TabIndex = 26;
            numQuantity.TabStop = false;
            // 
            // AwkwardDescriptionPart
            // 
            AutoScaleDimensions = new SizeF(7F, 14F);
            Name = "AwkwardDescriptionPart";
            Size = new Size(21, 22);
            ((ISupportInitialize)(popupContainerControl1)).EndInit();
            popupContainerControl1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)(cmbNetWeightUnit.Properties)).EndInit();
            ((ISupportInitialize)(bindingSource1)).EndInit();
            ((ISupportInitialize)(cmbGrossWeightUnit.Properties)).EndInit();
            ((ISupportInitialize)(numNetWeight.Properties)).EndInit();
            ((ISupportInitialize)(txtDetails.Properties)).EndInit();
            ((ISupportInitialize)(txtCommodity.Properties)).EndInit();
            ((ISupportInitialize)(numLength.Properties)).EndInit();
            ((ISupportInitialize)(numWidth.Properties)).EndInit();
            ((ISupportInitialize)(numGrossWeight.Properties)).EndInit();
            ((ISupportInitialize)(numHeight.Properties)).EndInit();
            ((ISupportInitialize)(numQuantity.Properties)).EndInit();
            ResumeLayout(false);

        }

        public AwkwardDescriptionPart()
            : base()
        {
            InitializeComponent();
            Disposed += delegate
            {  
               
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
            
            };
            if (LocalData.IsEnglish == false) SetCnText();

            DevHelper.FormatSpinEdit(numGrossWeight, 1);
            DevHelper.FormatSpinEdit(numHeight, 1);
            DevHelper.FormatSpinEdit(numLength, 1);
            DevHelper.FormatSpinEdit(numNetWeight, 1);
            DevHelper.FormatSpinEditForInteger(numQuantity);
            DevHelper.FormatSpinEdit(numWidth, 1);
        }

        private void SetCnText()
        {
            labCommodity.Text = "品名";
            labDetails.Text = "描述";
            labGrossWeight.Text = "净重";
            labHeight .Text = "高";
            labLength .Text = "长度";
            labNetWeight .Text = "毛重";
            labQuantity .Text = "数量";
            labWidth .Text = "宽";
        }

        public override void SetParentControl(Control parentControl, CargoDescription cargoDescription)
        {
            base.SetParentControl(parentControl, cargoDescription);

            if (cmbGrossWeightUnit.Properties.Items.Count > 0)
                cmbGrossWeightUnit.SelectedIndex = cmbNetWeightUnit.SelectedIndex = 0;
            else if(_weightUnitsList !=null)
            {
                foreach (var item in _weightUnitsList)
                {
                    cmbGrossWeightUnit.Properties.Items.Add(item.EName);
                    cmbNetWeightUnit.Properties.Items.Add(item.EName);
                }
                cmbGrossWeightUnit.SelectedIndex = cmbNetWeightUnit.SelectedIndex = 0;
            }
        }

        protected override void SetSource(CommonCargo commonCargo)
        {
            bindingSource1.DataSource = commonCargo;
            //if(cmbGrossWeightUnit.Properties.Items.Count >0)
            //    cmbGrossWeightUnit.SelectedIndex = cmbNetWeightUnit.SelectedIndex = 0;

            AwkwardCargo cargo = commonCargo  as AwkwardCargo;

            if (cargo != null)
            {
                cmbGrossWeightUnit.Text = cargo.GrossWeightUnit;
                cmbNetWeightUnit.Text = cargo.NetWeightUnit;
            }
        }

        protected override void EndEdit()
        {
            Validate();
            bindingSource1.EndEdit();
        }

        void ShowUnitNames(ComboBoxEdit cmbEdit,
            List<DataDictionaryList> list)
        {
            cmbEdit.Properties.Items.Clear();
            foreach (var item in list)
            {
                cmbEdit.Properties.Items.Add(
                        LocalData.IsEnglish ? item.EName : item.CName);

            }
        }

        public override void ShowWeightUnit(List<DataDictionaryList> units)
        {
            ShowUnitNames(cmbGrossWeightUnit, units);
            ShowUnitNames(cmbNetWeightUnit, units);
        }
    }
}

