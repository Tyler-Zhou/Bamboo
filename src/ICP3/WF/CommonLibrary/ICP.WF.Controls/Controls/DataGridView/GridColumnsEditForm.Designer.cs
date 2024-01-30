namespace ICP.WF.Controls
{
    partial class GridColumnsEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridColumnsEditForm));
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.pnlLeft = new DevExpress.XtraEditors.PanelControl();
            this.pnlColumns = new DevExpress.XtraEditors.GroupControl();
            this.listColumns = new DevExpress.XtraEditors.ListBoxControl();
            this.pnlButton = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.pnlProperties = new DevExpress.XtraEditors.PanelControl();
            this.pnlLeft2 = new DevExpress.XtraEditors.PanelControl();
            this.btnDown = new DevExpress.XtraEditors.SimpleButton();
            this.btnUp = new DevExpress.XtraEditors.SimpleButton();
            this.lwXtraPropertyGrid1 = new ICP.Framework.ClientComponents.Controls.LWXtraPropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlColumns)).BeginInit();
            this.pnlColumns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).BeginInit();
            this.pnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlProperties)).BeginInit();
            this.pnlProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft2)).BeginInit();
            this.pnlLeft2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.pnlColumns);
            this.pnlLeft.Controls.Add(this.pnlButton);
            resources.ApplyResources(this.pnlLeft, "pnlLeft");
            this.pnlLeft.Name = "pnlLeft";
            // 
            // pnlColumns
            // 
            this.pnlColumns.Controls.Add(this.listColumns);
            resources.ApplyResources(this.pnlColumns, "pnlColumns");
            this.pnlColumns.Name = "pnlColumns";
            // 
            // listColumns
            // 
            resources.ApplyResources(this.listColumns, "listColumns");
            this.listColumns.Name = "listColumns";
            this.listColumns.SelectedValueChanged += new System.EventHandler(this.listColumns_SelectedValueChanged);
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.simpleButton1);
            this.pnlButton.Controls.Add(this.btnAdd);
            resources.ApplyResources(this.pnlButton, "pnlButton");
            this.pnlButton.Name = "pnlButton";
            // 
            // simpleButton1
            // 
            resources.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlProperties
            // 
            this.pnlProperties.Controls.Add(this.lwXtraPropertyGrid1);
            resources.ApplyResources(this.pnlProperties, "pnlProperties");
            this.pnlProperties.Name = "pnlProperties";
            // 
            // pnlLeft2
            // 
            this.pnlLeft2.Controls.Add(this.btnDown);
            this.pnlLeft2.Controls.Add(this.btnUp);
            resources.ApplyResources(this.pnlLeft2, "pnlLeft2");
            this.pnlLeft2.Name = "pnlLeft2";
            // 
            // btnDown
            // 
            this.btnDown.Image = global::ICP.WF.Controls.Properties.Resources.moveDownPress;
            resources.ApplyResources(this.btnDown, "btnDown");
            this.btnDown.Name = "btnDown";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::ICP.WF.Controls.Properties.Resources.moveUpHover;
            resources.ApplyResources(this.btnUp, "btnUp");
            this.btnUp.Name = "btnUp";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // lwXtraPropertyGrid1
            // 
            resources.ApplyResources(this.lwXtraPropertyGrid1, "lwXtraPropertyGrid1");
            this.lwXtraPropertyGrid1.Name = "lwXtraPropertyGrid1";
            // 
            // GridColumnsEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlProperties);
            this.Controls.Add(this.pnlLeft2);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Name = "GridColumnsEditForm";
            this.Load += new System.EventHandler(this.GridColumnsEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlColumns)).EndInit();
            this.pnlColumns.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlButton)).EndInit();
            this.pnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlProperties)).EndInit();
            this.pnlProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft2)).EndInit();
            this.pnlLeft2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.PanelControl pnlLeft;
        private DevExpress.XtraEditors.PanelControl pnlProperties;
        private DevExpress.XtraEditors.PanelControl pnlButton;
        private DevExpress.XtraEditors.GroupControl pnlColumns;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.ListBoxControl listColumns;
        private DevExpress.XtraEditors.PanelControl pnlLeft2;
        private DevExpress.XtraEditors.SimpleButton btnDown;
        private DevExpress.XtraEditors.SimpleButton btnUp;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private ICP.Framework.ClientComponents.Controls.LWXtraPropertyGrid lwXtraPropertyGrid1;

    }
}