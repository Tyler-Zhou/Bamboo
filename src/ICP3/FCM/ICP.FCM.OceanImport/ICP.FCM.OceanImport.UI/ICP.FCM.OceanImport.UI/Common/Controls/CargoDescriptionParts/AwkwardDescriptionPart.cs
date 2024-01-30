using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.OceanImport.UI.Common.Controls
{
    /// <summary>
    /// 特种货
    /// </summary>
    public class AwkwardDescriptionPart : CargoDescriptionPart
    {
        private DevExpress.XtraEditors.ComboBoxEdit cmbNetWeightUnit;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.ComponentModel.IContainer components;
        private DevExpress.XtraEditors.SpinEdit numHeight;
        private DevExpress.XtraEditors.MemoEdit txtDetails;
        private DevExpress.XtraEditors.ComboBoxEdit cmbGrossWeightUnit;
        private DevExpress.XtraEditors.MemoEdit txtCommodity;
        private DevExpress.XtraEditors.LabelControl labGrossWeight;
        private DevExpress.XtraEditors.LabelControl labHeight;
        private DevExpress.XtraEditors.SpinEdit numLength;
        private DevExpress.XtraEditors.SpinEdit numQuantity;
        private DevExpress.XtraEditors.SpinEdit numNetWeight;
        private DevExpress.XtraEditors.LabelControl labWidth;
        private DevExpress.XtraEditors.SpinEdit numWidth;
        private DevExpress.XtraEditors.LabelControl labCommodity;
        private DevExpress.XtraEditors.LabelControl labNetWeight;
        private DevExpress.XtraEditors.LabelControl labQuantity;
        private DevExpress.XtraEditors.SpinEdit numGrossWeight;
        private DevExpress.XtraEditors.LabelControl labDetails;
        private DevExpress.XtraEditors.LabelControl labLength;
    
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbNetWeightUnit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cmbGrossWeightUnit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labHeight = new DevExpress.XtraEditors.LabelControl();
            this.labWidth = new DevExpress.XtraEditors.LabelControl();
            this.labQuantity = new DevExpress.XtraEditors.LabelControl();
            this.labLength = new DevExpress.XtraEditors.LabelControl();
            this.labNetWeight = new DevExpress.XtraEditors.LabelControl();
            this.numNetWeight = new DevExpress.XtraEditors.SpinEdit();
            this.labGrossWeight = new DevExpress.XtraEditors.LabelControl();
            this.txtDetails = new DevExpress.XtraEditors.MemoEdit();
            this.txtCommodity = new DevExpress.XtraEditors.MemoEdit();
            this.numLength = new DevExpress.XtraEditors.SpinEdit();
            this.numWidth = new DevExpress.XtraEditors.SpinEdit();
            this.numGrossWeight = new DevExpress.XtraEditors.SpinEdit();
            this.labDetails = new DevExpress.XtraEditors.LabelControl();
            this.labCommodity = new DevExpress.XtraEditors.LabelControl();
            this.numHeight = new DevExpress.XtraEditors.SpinEdit();
            this.numQuantity = new DevExpress.XtraEditors.SpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
            this.popupContainerControl1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNetWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGrossWeightUnit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetails.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrossWeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.popupContainerControl1.Appearance.Options.UseBackColor = true;
            this.popupContainerControl1.Size = new System.Drawing.Size(426, 253);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panel1.Location = new System.Drawing.Point(0, 217);
            this.panel1.Size = new System.Drawing.Size(426, 36);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.panel2.Controls.Add(this.cmbNetWeightUnit);
            this.panel2.Controls.Add(this.numHeight);
            this.panel2.Controls.Add(this.txtDetails);
            this.panel2.Controls.Add(this.cmbGrossWeightUnit);
            this.panel2.Controls.Add(this.txtCommodity);
            this.panel2.Controls.Add(this.labGrossWeight);
            this.panel2.Controls.Add(this.labHeight);
            this.panel2.Controls.Add(this.numLength);
            this.panel2.Controls.Add(this.numQuantity);
            this.panel2.Controls.Add(this.numNetWeight);
            this.panel2.Controls.Add(this.labWidth);
            this.panel2.Controls.Add(this.numWidth);
            this.panel2.Controls.Add(this.labCommodity);
            this.panel2.Controls.Add(this.labNetWeight);
            this.panel2.Controls.Add(this.labQuantity);
            this.panel2.Controls.Add(this.numGrossWeight);
            this.panel2.Controls.Add(this.labDetails);
            this.panel2.Controls.Add(this.labLength);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Size = new System.Drawing.Size(426, 217);
            // 
            // cmbNetWeightUnit
            // 
            this.cmbNetWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "NetWeightUnit", true));
            this.cmbNetWeightUnit.Location = new System.Drawing.Point(351, 57);
            this.cmbNetWeightUnit.Name = "cmbNetWeightUnit";
            this.cmbNetWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNetWeightUnit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbNetWeightUnit.Size = new System.Drawing.Size(68, 21);
            this.cmbNetWeightUnit.TabIndex = 40;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.Common.ServiceInterface.DataObjects.AwkwardCargo);
            // 
            // cmbGrossWeightUnit
            // 
            this.cmbGrossWeightUnit.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "GrossWeightUnit", true));
            this.cmbGrossWeightUnit.Location = new System.Drawing.Point(351, 30);
            this.cmbGrossWeightUnit.Name = "cmbGrossWeightUnit";
            this.cmbGrossWeightUnit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGrossWeightUnit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbGrossWeightUnit.Size = new System.Drawing.Size(68, 21);
            this.cmbGrossWeightUnit.TabIndex = 41;
            // 
            // labHeight
            // 
            this.labHeight.Location = new System.Drawing.Point(3, 60);
            this.labHeight.Name = "labHeight";
            this.labHeight.Size = new System.Drawing.Size(36, 14);
            this.labHeight.TabIndex = 36;
            this.labHeight.Text = "Height";
            // 
            // labWidth
            // 
            this.labWidth.Location = new System.Drawing.Point(3, 33);
            this.labWidth.Name = "labWidth";
            this.labWidth.Size = new System.Drawing.Size(33, 14);
            this.labWidth.TabIndex = 35;
            this.labWidth.Text = "Width";
            // 
            // labQuantity
            // 
            this.labQuantity.Location = new System.Drawing.Point(213, 6);
            this.labQuantity.Name = "labQuantity";
            this.labQuantity.Size = new System.Drawing.Size(47, 14);
            this.labQuantity.TabIndex = 34;
            this.labQuantity.Text = "Quantity";
            // 
            // labLength
            // 
            this.labLength.Location = new System.Drawing.Point(2, 6);
            this.labLength.Name = "labLength";
            this.labLength.Size = new System.Drawing.Size(39, 14);
            this.labLength.TabIndex = 39;
            this.labLength.Text = "Length";
            // 
            // labNetWeight
            // 
            this.labNetWeight.Location = new System.Drawing.Point(213, 60);
            this.labNetWeight.Name = "labNetWeight";
            this.labNetWeight.Size = new System.Drawing.Size(60, 14);
            this.labNetWeight.TabIndex = 38;
            this.labNetWeight.Text = "NetWeight";
            // 
            // numNetWeight
            // 
            this.numNetWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "NetWeight", true));
            this.numNetWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numNetWeight.Location = new System.Drawing.Point(275, 57);
            this.numNetWeight.Name = "numNetWeight";
            this.numNetWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numNetWeight.Size = new System.Drawing.Size(75, 21);
            this.numNetWeight.TabIndex = 33;
            // 
            // labGrossWeight
            // 
            this.labGrossWeight.Location = new System.Drawing.Point(213, 33);
            this.labGrossWeight.Name = "labGrossWeight";
            this.labGrossWeight.Size = new System.Drawing.Size(40, 14);
            this.labGrossWeight.TabIndex = 37;
            this.labGrossWeight.Text = "Weight";
            // 
            // txtDetails
            // 
            this.txtDetails.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Details", true));
            this.txtDetails.Location = new System.Drawing.Point(65, 152);
            this.txtDetails.Name = "txtDetails";
            this.txtDetails.Size = new System.Drawing.Size(354, 61);
            this.txtDetails.TabIndex = 28;
            // 
            // txtCommodity
            // 
            this.txtCommodity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "Commodity", true));
            this.txtCommodity.Location = new System.Drawing.Point(65, 85);
            this.txtCommodity.Name = "txtCommodity";
            this.txtCommodity.Size = new System.Drawing.Size(354, 61);
            this.txtCommodity.TabIndex = 29;
            // 
            // numLength
            // 
            this.numLength.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Length", true));
            this.numLength.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numLength.Location = new System.Drawing.Point(65, 3);
            this.numLength.Name = "numLength";
            this.numLength.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numLength.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numLength.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numLength.Size = new System.Drawing.Size(144, 21);
            this.numLength.TabIndex = 27;
            this.numLength.TabStop = false;
            // 
            // numWidth
            // 
            this.numWidth.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Width", true));
            this.numWidth.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numWidth.Location = new System.Drawing.Point(65, 30);
            this.numWidth.Name = "numWidth";
            this.numWidth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numWidth.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numWidth.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numWidth.Size = new System.Drawing.Size(144, 21);
            this.numWidth.TabIndex = 24;
            this.numWidth.TabStop = false;
            // 
            // numGrossWeight
            // 
            this.numGrossWeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "GrossWeight", true));
            this.numGrossWeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numGrossWeight.Location = new System.Drawing.Point(275, 30);
            this.numGrossWeight.Name = "numGrossWeight";
            this.numGrossWeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numGrossWeight.Size = new System.Drawing.Size(75, 21);
            this.numGrossWeight.TabIndex = 32;
            // 
            // labDetails
            // 
            this.labDetails.Location = new System.Drawing.Point(2, 155);
            this.labDetails.Name = "labDetails";
            this.labDetails.Size = new System.Drawing.Size(35, 14);
            this.labDetails.TabIndex = 30;
            this.labDetails.Text = "Details";
            // 
            // labCommodity
            // 
            this.labCommodity.Location = new System.Drawing.Point(2, 87);
            this.labCommodity.Name = "labCommodity";
            this.labCommodity.Size = new System.Drawing.Size(61, 14);
            this.labCommodity.TabIndex = 31;
            this.labCommodity.Text = "Commodity";
            // 
            // numHeight
            // 
            this.numHeight.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Height", true));
            this.numHeight.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numHeight.Location = new System.Drawing.Point(65, 57);
            this.numHeight.Name = "numHeight";
            this.numHeight.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numHeight.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numHeight.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numHeight.Size = new System.Drawing.Size(144, 21);
            this.numHeight.TabIndex = 25;
            this.numHeight.TabStop = false;
            // 
            // numQuantity
            // 
            this.numQuantity.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "Quantity", true));
            this.numQuantity.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numQuantity.Location = new System.Drawing.Point(275, 3);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numQuantity.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.numQuantity.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.numQuantity.Size = new System.Drawing.Size(144, 21);
            this.numQuantity.TabIndex = 26;
            this.numQuantity.TabStop = false;
            // 
            // AwkwardDescriptionPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.Name = "AwkwardDescriptionPart";
            this.Size = new System.Drawing.Size(21, 22);
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.popupContainerControl1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNetWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGrossWeightUnit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNetWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDetails.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommodity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrossWeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        public AwkwardDescriptionPart()
            : base()
        {
            InitializeComponent();
            this.Disposed += delegate {

                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
            };
            if (LocalData.IsEnglish == false)
            {
                SetCnText();
                InitControl();
            }
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

        private void InitControl()
        {

            DevHelper.FormatSpinEdit(this.numHeight,1);
            DevHelper.FormatSpinEdit(this.numLength,1);
            DevHelper.FormatSpinEdit(this.numNetWeight,1);
            DevHelper.FormatSpinEdit(this.numWidth, 1);
            DevHelper.FormatSpinEdit(this.numGrossWeight, 1);

            DevHelper.FormatSpinEditForInteger(this.numQuantity);
        }

        public override void SetParentControl(System.Windows.Forms.Control parentControl, ICP.FCM.Common.ServiceInterface.DataObjects.CargoDescription cargoDescription)
        {
            base.SetParentControl(parentControl, cargoDescription);

            if (cmbGrossWeightUnit.Properties.Items.Count > 0)
                cmbGrossWeightUnit.SelectedIndex = cmbNetWeightUnit.SelectedIndex = 0;
            else if(_weightUnitsList !=null)
            {
                foreach (var item in _weightUnitsList)
                {
                    this.cmbGrossWeightUnit.Properties.Items.Add(item.EName);
                    this.cmbNetWeightUnit.Properties.Items.Add(item.EName);
                }
                cmbGrossWeightUnit.SelectedIndex = cmbNetWeightUnit.SelectedIndex = 0;
            }
        }

        protected override void SetSource(ICP.FCM.Common.ServiceInterface.DataObjects.CommonCargo commonCargo)
        {
            this.bindingSource1.DataSource = commonCargo;
            if(cmbGrossWeightUnit.Properties.Items.Count >0)
                cmbGrossWeightUnit.SelectedIndex = cmbNetWeightUnit.SelectedIndex = 0;
        }

        protected override void EndEdit()
        {
            this.Validate();
            this.bindingSource1.EndEdit();
        }
        void ShowUnitNames(DevExpress.XtraEditors.ComboBoxEdit cmbEdit,
    List<ICP.Common.ServiceInterface.DataObjects.DataDictionaryList> list)
        {
            cmbEdit.Properties.Items.Clear();
            foreach (var item in list)
            {
                cmbEdit.Properties.Items.Add( item.EName);

            }
        }

        public override void ShowWeightUnit(List<ICP.Common.ServiceInterface.DataObjects.DataDictionaryList> units)
        {
            this.ShowUnitNames(this.cmbGrossWeightUnit, units);
            this.ShowUnitNames(this.cmbNetWeightUnit, units);
        }
    }
}

