namespace ICP.FCM.OceanExport.UI.MBL
{
    partial class LoadShipConfirm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupVoyage = new System.Windows.Forms.GroupBox();
            this.chkConfirmVoyage = new DevExpress.XtraEditors.CheckEdit();
            this.stxtVoyage = new DevExpress.XtraEditors.ButtonEdit();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dteETA = new DevExpress.XtraEditors.DateEdit();
            this.labETA = new DevExpress.XtraEditors.LabelControl();
            this.dteETD = new DevExpress.XtraEditors.DateEdit();
            this.labETD2 = new DevExpress.XtraEditors.LabelControl();
            this.labVoyage = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.groupPreVoyage = new System.Windows.Forms.GroupBox();
            this.chkConfirmPreVoyage = new DevExpress.XtraEditors.CheckEdit();
            this.stxtPreVoyage = new DevExpress.XtraEditors.ButtonEdit();
            this.labPreVoyage = new DevExpress.XtraEditors.LabelControl();
            this.labETD = new DevExpress.XtraEditors.LabelControl();
            this.dtePreETD = new DevExpress.XtraEditors.DateEdit();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.groupVoyage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConfirmVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).BeginInit();
            this.groupPreVoyage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConfirmPreVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPreVoyage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePreETD.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePreETD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupVoyage
            // 
            this.groupVoyage.Controls.Add(this.chkConfirmVoyage);
            this.groupVoyage.Controls.Add(this.stxtVoyage);
            this.groupVoyage.Controls.Add(this.dteETA);
            this.groupVoyage.Controls.Add(this.labETA);
            this.groupVoyage.Controls.Add(this.dteETD);
            this.groupVoyage.Controls.Add(this.labETD2);
            this.groupVoyage.Controls.Add(this.labVoyage);
            this.groupVoyage.Location = new System.Drawing.Point(5, 82);
            this.groupVoyage.Name = "groupVoyage";
            this.groupVoyage.Size = new System.Drawing.Size(326, 71);
            this.groupVoyage.TabIndex = 1;
            this.groupVoyage.TabStop = false;
            this.groupVoyage.Text = "Voyage";
            // 
            // chkConfirmVoyage
            // 
            this.chkConfirmVoyage.Location = new System.Drawing.Point(254, 42);
            this.chkConfirmVoyage.Name = "chkConfirmVoyage";
            this.chkConfirmVoyage.Properties.Caption = "Confirm";
            this.chkConfirmVoyage.Size = new System.Drawing.Size(66, 19);
            this.chkConfirmVoyage.TabIndex = 2;
            // 
            // stxtVoyage
            // 
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "VesselVoyage", true));
            this.stxtVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "VoyageID", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtVoyage, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtVoyage.Location = new System.Drawing.Point(55, 14);
            this.stxtVoyage.Name = "stxtVoyage";
            this.stxtVoyage.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtVoyage.Properties.Appearance.Options.UseBackColor = true;
            this.stxtVoyage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtVoyage.Size = new System.Drawing.Size(265, 21);
            this.stxtVoyage.TabIndex = 0;
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanMBLInfo);
            // 
            // dteETA
            // 
            this.dteETA.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETA", true));
            this.dteETA.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteETA, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteETA.Location = new System.Drawing.Point(171, 41);
            this.dteETA.Name = "dteETA";
            this.dteETA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETA.Properties.Mask.EditMask = "";
            this.dteETA.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETA.Properties.ReadOnly = true;
            this.dteETA.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteETA.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETA.Size = new System.Drawing.Size(80, 21);
            this.dteETA.TabIndex = 2;
            // 
            // labETA
            // 
            this.labETA.Location = new System.Drawing.Point(142, 44);
            this.labETA.Name = "labETA";
            this.labETA.Size = new System.Drawing.Size(23, 14);
            this.labETA.TabIndex = 0;
            this.labETA.Text = "ETA";
            // 
            // dteETD
            // 
            this.dteETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETD", true));
            this.dteETD.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dteETD, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dteETD.Location = new System.Drawing.Point(55, 41);
            this.dteETD.Name = "dteETD";
            this.dteETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteETD.Properties.Mask.EditMask = "";
            this.dteETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteETD.Properties.ReadOnly = true;
            this.dteETD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dteETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteETD.Size = new System.Drawing.Size(80, 21);
            this.dteETD.TabIndex = 1;
            // 
            // labETD2
            // 
            this.labETD2.Location = new System.Drawing.Point(6, 47);
            this.labETD2.Name = "labETD2";
            this.labETD2.Size = new System.Drawing.Size(23, 14);
            this.labETD2.TabIndex = 0;
            this.labETD2.Text = "ETD";
            // 
            // labVoyage
            // 
            this.labVoyage.Location = new System.Drawing.Point(4, 21);
            this.labVoyage.Name = "labVoyage";
            this.labVoyage.Size = new System.Drawing.Size(41, 14);
            this.labVoyage.TabIndex = 0;
            this.labVoyage.Text = "Voyage";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(169, 159);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupPreVoyage
            // 
            this.groupPreVoyage.Controls.Add(this.chkConfirmPreVoyage);
            this.groupPreVoyage.Controls.Add(this.stxtPreVoyage);
            this.groupPreVoyage.Controls.Add(this.labPreVoyage);
            this.groupPreVoyage.Controls.Add(this.labETD);
            this.groupPreVoyage.Controls.Add(this.dtePreETD);
            this.groupPreVoyage.Location = new System.Drawing.Point(3, 3);
            this.groupPreVoyage.Name = "groupPreVoyage";
            this.groupPreVoyage.Size = new System.Drawing.Size(328, 73);
            this.groupPreVoyage.TabIndex = 0;
            this.groupPreVoyage.TabStop = false;
            this.groupPreVoyage.Text = "PreVoyage";
            // 
            // chkConfirmPreVoyage
            // 
            this.chkConfirmPreVoyage.Location = new System.Drawing.Point(259, 44);
            this.chkConfirmPreVoyage.Name = "chkConfirmPreVoyage";
            this.chkConfirmPreVoyage.Properties.Caption = "Confirm";
            this.chkConfirmPreVoyage.Size = new System.Drawing.Size(62, 19);
            this.chkConfirmPreVoyage.TabIndex = 2;
            // 
            // stxtPreVoyage
            // 
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource1, "PreVesselVoyage", true));
            this.stxtPreVoyage.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.bindingSource1, "PreVoyageID", true));
            this.dxErrorProvider1.SetIconAlignment(this.stxtPreVoyage, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.stxtPreVoyage.Location = new System.Drawing.Point(58, 17);
            this.stxtPreVoyage.Name = "stxtPreVoyage";
            this.stxtPreVoyage.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.stxtPreVoyage.Properties.Appearance.Options.UseBackColor = true;
            this.stxtPreVoyage.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.stxtPreVoyage.Size = new System.Drawing.Size(264, 21);
            this.stxtPreVoyage.TabIndex = 0;
            // 
            // labPreVoyage
            // 
            this.labPreVoyage.Location = new System.Drawing.Point(7, 21);
            this.labPreVoyage.Name = "labPreVoyage";
            this.labPreVoyage.Size = new System.Drawing.Size(41, 14);
            this.labPreVoyage.TabIndex = 0;
            this.labPreVoyage.Text = "Voyage";
            // 
            // labETD
            // 
            this.labETD.Location = new System.Drawing.Point(7, 51);
            this.labETD.Name = "labETD";
            this.labETD.Size = new System.Drawing.Size(23, 14);
            this.labETD.TabIndex = 0;
            this.labETD.Text = "ETD";
            // 
            // dtePreETD
            // 
            this.dtePreETD.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.bindingSource1, "ETD", true));
            this.dtePreETD.EditValue = null;
            this.dxErrorProvider1.SetIconAlignment(this.dtePreETD, System.Windows.Forms.ErrorIconAlignment.MiddleRight);
            this.dtePreETD.Location = new System.Drawing.Point(58, 44);
            this.dtePreETD.Name = "dtePreETD";
            this.dtePreETD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtePreETD.Properties.Mask.EditMask = "";
            this.dtePreETD.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtePreETD.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dtePreETD.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtePreETD.Size = new System.Drawing.Size(79, 21);
            this.dtePreETD.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(250, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // LoadShipConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupVoyage);
            this.Controls.Add(this.groupPreVoyage);
            this.Name = "LoadShipConfirm";
            this.Size = new System.Drawing.Size(340, 191);
            this.groupVoyage.ResumeLayout(false);
            this.groupVoyage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConfirmVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteETD.Properties)).EndInit();
            this.groupPreVoyage.ResumeLayout(false);
            this.groupPreVoyage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConfirmPreVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stxtPreVoyage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePreETD.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtePreETD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupVoyage;
        private DevExpress.XtraEditors.ButtonEdit stxtVoyage;
        private DevExpress.XtraEditors.DateEdit dteETA;
        private DevExpress.XtraEditors.LabelControl labETA;
        private DevExpress.XtraEditors.DateEdit dteETD;
        private DevExpress.XtraEditors.LabelControl labETD2;
        private DevExpress.XtraEditors.LabelControl labVoyage;
        private System.Windows.Forms.GroupBox groupPreVoyage;
        private DevExpress.XtraEditors.ButtonEdit stxtPreVoyage;
        private DevExpress.XtraEditors.LabelControl labPreVoyage;
        private DevExpress.XtraEditors.LabelControl labETD;
        private DevExpress.XtraEditors.DateEdit dtePreETD;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private DevExpress.XtraEditors.CheckEdit chkConfirmVoyage;
        private DevExpress.XtraEditors.CheckEdit chkConfirmPreVoyage;
    }
}
