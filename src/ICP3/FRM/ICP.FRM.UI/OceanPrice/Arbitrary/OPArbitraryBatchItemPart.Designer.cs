using ICP.Framework.ClientComponents.Controls;
namespace ICP.FRM.UI.OceanPrice
{
    partial class OPArbitraryBatchItemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.labPOL = new DevExpress.XtraEditors.LabelControl();
            this.labPOD = new DevExpress.XtraEditors.LabelControl();
            this.stxtPOD = new DevExpress.XtraEditors.ButtonEdit();
            this.stxtPOL = new DevExpress.XtraEditors.ButtonEdit();
            this.labGeographyType = new DevExpress.XtraEditors.LabelControl();
            this.cmbGeographyType = new LWImageComboBoxEdit();
            this.chkAttachRate = new DevExpress.XtraEditors.CheckEdit();
            this.bsRateUnit = new System.Windows.Forms.BindingSource(this.components);
            this.groupRate = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gcRate = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvRate = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.labItemCode = new DevExpress.XtraEditors.LabelControl();
            this.txtItemCode = new DevExpress.XtraEditors.TextEdit();
            this.cmbTransportClause = new LWImageComboBoxEdit();
            this.labTerm = new DevExpress.XtraEditors.LabelControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGeographyType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAttachRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRateUnit)).BeginInit();
            this.groupRate.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 284);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 45);
            this.panel1.TabIndex = 23;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(285, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(194, 10);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FRM.UI.OceanPrice.ArbitraryBatchItem);
            // 
            // labPOL
            // 
            this.labPOL.Location = new System.Drawing.Point(12, 174);
            this.labPOL.Name = "labPOL";
            this.labPOL.Size = new System.Drawing.Size(22, 14);
            this.labPOL.TabIndex = 1;
            this.labPOL.Text = "POL";
            // 
            // labPOD
            // 
            this.labPOD.Location = new System.Drawing.Point(12, 201);
            this.labPOD.Name = "labPOD";
            this.labPOD.Size = new System.Drawing.Size(24, 14);
            this.labPOD.TabIndex = 1;
            this.labPOD.Text = "POD";
            // 
            // stxtPOD
            // 
            this.stxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PODID", true));
            this.stxtPOD.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PODName", true));
            this.stxtPOD.Location = new System.Drawing.Point(84, 198);
            this.stxtPOD.Name = "stxtPOD";
            this.stxtPOD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOD.Size = new System.Drawing.Size(268, 21);
            this.stxtPOD.TabIndex = 3;
            this.stxtPOD.TabStop = false;
            // 
            // stxtPOL
            // 
            this.stxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "POLName", true));
            this.stxtPOL.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "POLID", true));
            this.stxtPOL.Location = new System.Drawing.Point(84, 171);
            this.stxtPOL.Name = "stxtPOL";
            this.stxtPOL.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPOL.Size = new System.Drawing.Size(268, 21);
            this.stxtPOL.TabIndex = 2;
            this.stxtPOL.TabStop = false;
            // 
            // labGeographyType
            // 
            this.labGeographyType.Location = new System.Drawing.Point(12, 147);
            this.labGeographyType.Name = "labGeographyType";
            this.labGeographyType.Size = new System.Drawing.Size(59, 14);
            this.labGeographyType.TabIndex = 24;
            this.labGeographyType.Text = "Geography";
            // 
            // cmbGeographyType
            // 
            this.cmbGeographyType.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "GeographyType", true));
            this.cmbGeographyType.Location = new System.Drawing.Point(84, 144);
            this.cmbGeographyType.Name = "cmbGeographyType";
            this.cmbGeographyType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGeographyType.Size = new System.Drawing.Size(268, 21);
            this.cmbGeographyType.TabIndex = 1;
            this.cmbGeographyType.TabStop = false;
            // 
            // chkAttachRate
            // 
            this.chkAttachRate.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "RateAppend", true));
            this.chkAttachRate.Location = new System.Drawing.Point(84, 119);
            this.chkAttachRate.Name = "chkAttachRate";
            this.chkAttachRate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.chkAttachRate.Properties.Caption = "Increase by the following rates";
            this.chkAttachRate.Properties.DisplayFormat.FormatString = "d";
            this.chkAttachRate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.chkAttachRate.Properties.EditFormat.FormatString = "d";
            this.chkAttachRate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.chkAttachRate.Size = new System.Drawing.Size(268, 19);
            this.chkAttachRate.TabIndex = 0;
            this.chkAttachRate.TabStop = false;
            // 
            // bsRateUnit
            // 
            this.bsRateUnit.DataMember = "OceanClientUnits";
            this.bsRateUnit.DataSource = this.bindingSource1;
            // 
            // groupRate
            // 
            this.groupRate.Controls.Add(this.panel2);
            this.groupRate.Location = new System.Drawing.Point(9, 4);
            this.groupRate.Name = "groupRate";
            this.groupRate.Size = new System.Drawing.Size(346, 113);
            this.groupRate.TabIndex = 28;
            this.groupRate.TabStop = false;
            this.groupRate.Text = "Rate";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gcRate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(3, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(340, 92);
            this.panel2.TabIndex = 19;
            // 
            // gcRate
            // 
            this.gcRate.DataSource = this.bsRateUnit;
            this.gcRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRate.Location = new System.Drawing.Point(0, 0);
            this.gcRate.MainView = this.gvRate;
            this.gcRate.Name = "gcRate";
            this.gcRate.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rSpinEdit1});
            this.gcRate.Size = new System.Drawing.Size(340, 92);
            this.gcRate.TabIndex = 0;
            this.gcRate.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRate});
            // 
            // gvRate
            // 
            this.gvRate.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUnitName,
            this.colRate});
            this.gvRate.GridControl = this.gcRate;
            this.gvRate.Name = "gvRate";
            this.gvRate.OptionsView.EnableAppearanceEvenRow = true;
            this.gvRate.OptionsView.ShowGroupPanel = false;
            // 
            // colUnitName
            // 
            this.colUnitName.FieldName = "UnitName";
            this.colUnitName.Name = "colUnitName";
            this.colUnitName.OptionsColumn.AllowEdit = false;
            this.colUnitName.Visible = true;
            this.colUnitName.VisibleIndex = 0;
            // 
            // colRate
            // 
            this.colRate.ColumnEdit = this.rSpinEdit1;
            this.colRate.FieldName = "Rate";
            this.colRate.Name = "colRate";
            this.colRate.Visible = true;
            this.colRate.VisibleIndex = 1;
            // 
            // rSpinEdit1
            // 
            this.rSpinEdit1.AutoHeight = false;
            this.rSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rSpinEdit1.Name = "rSpinEdit1";
            // 
            // labItemCode
            // 
            this.labItemCode.Location = new System.Drawing.Point(12, 255);
            this.labItemCode.Name = "labItemCode";
            this.labItemCode.Size = new System.Drawing.Size(54, 14);
            this.labItemCode.TabIndex = 1;
            this.labItemCode.Text = "ItemCode";
            // 
            // txtItemCode
            // 
            this.txtItemCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "ItemCode", true));
            this.txtItemCode.Location = new System.Drawing.Point(84, 252);
            this.txtItemCode.Name = "txtItemCode";
            this.txtItemCode.Size = new System.Drawing.Size(268, 21);
            this.txtItemCode.TabIndex = 5;
            // 
            // cmbTransportClause
            // 
            this.cmbTransportClause.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "TransportClauseListID", true));
            this.cmbTransportClause.Location = new System.Drawing.Point(84, 225);
            this.cmbTransportClause.Name = "cmbTransportClause";
            this.cmbTransportClause.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTransportClause.Size = new System.Drawing.Size(268, 21);
            this.cmbTransportClause.TabIndex = 4;
            // 
            // labTerm
            // 
            this.labTerm.Location = new System.Drawing.Point(12, 228);
            this.labTerm.Name = "labTerm";
            this.labTerm.Size = new System.Drawing.Size(29, 14);
            this.labTerm.TabIndex = 29;
            this.labTerm.Text = "Term";
            // 
            // OPArbitraryBatchItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbTransportClause);
            this.Controls.Add(this.labTerm);
            this.Controls.Add(this.groupRate);
            this.Controls.Add(this.cmbGeographyType);
            this.Controls.Add(this.labGeographyType);
            this.Controls.Add(this.txtItemCode);
            this.Controls.Add(this.stxtPOL);
            this.Controls.Add(this.stxtPOD);
            this.Controls.Add(this.labItemCode);
            this.Controls.Add(this.labPOD);
            this.Controls.Add(this.labPOL);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkAttachRate);
            this.IsMultiLanguage = false;
            this.Name = "OPArbitraryBatchItemForm";
            this.Size = new System.Drawing.Size(383, 329);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPOL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGeographyType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAttachRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsRateUnit)).EndInit();
            this.groupRate.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtItemCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTransportClause.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.LabelControl labPOL;
        private DevExpress.XtraEditors.LabelControl labPOD;
        private DevExpress.XtraEditors.ButtonEdit stxtPOD;
        private DevExpress.XtraEditors.ButtonEdit stxtPOL;
        private DevExpress.XtraEditors.LabelControl labGeographyType;
        private LWImageComboBoxEdit cmbGeographyType;
        private DevExpress.XtraEditors.CheckEdit chkAttachRate;
        private System.Windows.Forms.BindingSource bsRateUnit;
        private System.Windows.Forms.GroupBox groupRate;
        private System.Windows.Forms.Panel panel2;
        private ICP.Framework.ClientComponents.Controls.LWGridControl gcRate;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRate;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitName;
        private DevExpress.XtraGrid.Columns.GridColumn colRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rSpinEdit1;
        private DevExpress.XtraEditors.LabelControl labItemCode;
        private DevExpress.XtraEditors.TextEdit txtItemCode;
        private LWImageComboBoxEdit cmbTransportClause;
        private DevExpress.XtraEditors.LabelControl labTerm;
    }
}