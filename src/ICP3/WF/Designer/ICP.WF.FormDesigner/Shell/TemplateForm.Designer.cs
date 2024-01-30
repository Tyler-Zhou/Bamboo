namespace ICP.WF.FormDesigner
{
    partial class TemplateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateForm));
            this.errorTip = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            this.txtTemplateName = new DevExpress.XtraEditors.TextEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorTip
            // 
            this.errorTip.ContainerControl = this;
            resources.ApplyResources(this.errorTip, "errorTip");
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackgroundImage = null;
            this.errorTip.SetError(this.btnOK, resources.GetString("btnOK.Error"));
            this.errorTip.SetIconAlignment(this.btnOK, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnOK.IconAlignment"))));
            this.errorTip.SetIconPadding(this.btnOK, ((int)(resources.GetObject("btnOK.IconPadding"))));
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.errorTip.SetError(this.btnCancel, resources.GetString("btnCancel.Error"));
            this.errorTip.SetIconAlignment(this.btnCancel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnCancel.IconAlignment"))));
            this.errorTip.SetIconPadding(this.btnCancel, ((int)(resources.GetObject("btnCancel.IconPadding"))));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.errorTip.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorTip.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorTip.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // txtTemplateName
            // 
            resources.ApplyResources(this.txtTemplateName, "txtTemplateName");
            this.txtTemplateName.BackgroundImage = null;
            this.txtTemplateName.EditValue = null;
            this.errorTip.SetError(this.txtTemplateName, resources.GetString("txtTemplateName.Error"));
            this.errorTip.SetIconAlignment(this.txtTemplateName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtTemplateName.IconAlignment"))));
            this.errorTip.SetIconPadding(this.txtTemplateName, ((int)(resources.GetObject("txtTemplateName.IconPadding"))));
            this.txtTemplateName.Name = "txtTemplateName";
            this.txtTemplateName.Properties.AccessibleDescription = null;
            this.txtTemplateName.Properties.AccessibleName = null;
            this.txtTemplateName.Properties.AutoHeight = ((bool)(resources.GetObject("txtTemplateName.Properties.AutoHeight")));
            this.txtTemplateName.Properties.Mask.AutoComplete = ((DevExpress.XtraEditors.Mask.AutoCompleteType)(resources.GetObject("txtTemplateName.Properties.Mask.AutoComplete")));
            this.txtTemplateName.Properties.Mask.BeepOnError = ((bool)(resources.GetObject("txtTemplateName.Properties.Mask.BeepOnError")));
            this.txtTemplateName.Properties.Mask.EditMask = resources.GetString("txtTemplateName.Properties.Mask.EditMask");
            this.txtTemplateName.Properties.Mask.IgnoreMaskBlank = ((bool)(resources.GetObject("txtTemplateName.Properties.Mask.IgnoreMaskBlank")));
            this.txtTemplateName.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtTemplateName.Properties.Mask.MaskType")));
            this.txtTemplateName.Properties.Mask.PlaceHolder = ((char)(resources.GetObject("txtTemplateName.Properties.Mask.PlaceHolder")));
            this.txtTemplateName.Properties.Mask.SaveLiteral = ((bool)(resources.GetObject("txtTemplateName.Properties.Mask.SaveLiteral")));
            this.txtTemplateName.Properties.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("txtTemplateName.Properties.Mask.ShowPlaceHolders")));
            this.txtTemplateName.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtTemplateName.Properties.Mask.UseMaskAsDisplayFormat")));
            this.txtTemplateName.Properties.NullValuePrompt = resources.GetString("txtTemplateName.Properties.NullValuePrompt");
            this.txtTemplateName.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("txtTemplateName.Properties.NullValuePromptShowForEmptyValue")));
            // 
            // TemplateForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtTemplateName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemplateForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorTip;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.LabelControl label1;
        private DevExpress.XtraEditors.TextEdit txtTemplateName;
        private DevExpress.XtraEditors.StyleController styleController1;
    }
}