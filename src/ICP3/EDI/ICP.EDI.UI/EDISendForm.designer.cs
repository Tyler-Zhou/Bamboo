using System.Windows.Forms;
namespace ICP.EDI.UI
{
    partial class EDISendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EDISendForm));
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.splitContainer1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.txtDescripton = new DevExpress.XtraEditors.MemoEdit();
            this.spLogList = new DevExpress.XtraEditors.SplitterControl();
            this.gcMain = new DevExpress.XtraGrid.GridControl();
            this.bsLogs = new System.Windows.Forms.BindingSource(this.components);
            this.gvMain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LogType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubject = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSenderName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSendTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEDIContent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinkEDIContent = new DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.txtContent = new DevExpress.XtraEditors.MemoEdit();
            this.txtSubject = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.rbReplace = new System.Windows.Forms.RadioButton();
            this.rbCancel = new System.Windows.Forms.RadioButton();
            this.rbOriginal = new System.Windows.Forms.RadioButton();
            this.label7 = new DevExpress.XtraEditors.LabelControl();
            this.pnlPILCommod = new DevExpress.XtraEditors.PanelControl();
            this.mcmbCommodity = new ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox();
            this.labCommodity = new DevExpress.XtraEditors.LabelControl();
            this.panelAange = new DevExpress.XtraEditors.PanelControl();
            this.labelAgent = new DevExpress.XtraEditors.LabelControl();
            this.cmbAgent = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.panelVGM = new System.Windows.Forms.Panel();
            this.txtRemark = new DevExpress.XtraEditors.MemoEdit();
            this.labRamark = new DevExpress.XtraEditors.LabelControl();
            this.labVGMDate = new DevExpress.XtraEditors.LabelControl();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.labEmail = new DevExpress.XtraEditors.LabelControl();
            this.txtTel = new DevExpress.XtraEditors.TextEdit();
            this.labTel = new DevExpress.XtraEditors.LabelControl();
            this.txtContack = new DevExpress.XtraEditors.TextEdit();
            this.labContack = new DevExpress.XtraEditors.LabelControl();
            this.txtDepartment = new DevExpress.XtraEditors.TextEdit();
            this.labVGMDep = new DevExpress.XtraEditors.LabelControl();
            this.dtDate = new DevExpress.XtraEditors.DateEdit();
            this.linkLog = new System.Windows.Forms.LinkLabel();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.ifcsuM_FYCW1 = new LongWin.EDI.FileManager.IFCSUM_FYCW();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripton.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colLinkEDIContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPILCommod)).BeginInit();
            this.pnlPILCommod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelAange)).BeginInit();
            this.panelAange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgent.Properties)).BeginInit();
            this.panelVGM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContack.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ifcsuM_FYCW1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.Name = "btnSend";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.txtDescripton);
            this.splitContainer1.Panel1.Controls.Add(this.spLogList);
            this.splitContainer1.Panel1.Controls.Add(this.gcMain);
            this.splitContainer1.Panel2.Controls.Add(this.pnlMain);
            this.splitContainer1.Panel2.Controls.Add(this.pnlPILCommod);
            this.splitContainer1.Panel2.Controls.Add(this.panelAange);
            this.splitContainer1.Panel2.Controls.Add(this.panelVGM);
            this.splitContainer1.PanelVisibility = DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            // 
            // txtDescripton
            // 
            resources.ApplyResources(this.txtDescripton, "txtDescripton");
            this.txtDescripton.Name = "txtDescripton";
            this.txtDescripton.Properties.ReadOnly = true;
            // 
            // spLogList
            // 
            resources.ApplyResources(this.spLogList, "spLogList");
            this.spLogList.Name = "spLogList";
            this.spLogList.TabStop = false;
            // 
            // gcMain
            // 
            this.gcMain.DataSource = this.bsLogs;
            resources.ApplyResources(this.gcMain, "gcMain");
            this.gcMain.MainView = this.gvMain;
            this.gcMain.Name = "gcMain";
            this.gcMain.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.colLinkEDIContent});
            this.gcMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMain,
            this.gridView2});
            // 
            // bsLogs
            // 
            this.bsLogs.DataSource = typeof(ICP.EDI.ServiceInterface.DataObjects.LogData);
            this.bsLogs.PositionChanged += new System.EventHandler(this.bsLogs_PositionChanged);
            // 
            // gvMain
            // 
            this.gvMain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.LogType,
            this.colSubject,
            this.colSenderName,
            this.colSendTime,
            this.colEDIContent});
            this.gvMain.GridControl = this.gcMain;
            this.gvMain.Name = "gvMain";
            this.gvMain.OptionsBehavior.Editable = false;
            this.gvMain.OptionsSelection.MultiSelect = true;
            this.gvMain.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvMain.OptionsView.ShowGroupPanel = false;
            // 
            // LogType
            // 
            resources.ApplyResources(this.LogType, "LogType");
            this.LogType.FieldName = "EDILogTypeName";
            this.LogType.Name = "LogType";
            // 
            // colSubject
            // 
            resources.ApplyResources(this.colSubject, "colSubject");
            this.colSubject.FieldName = "Subject";
            this.colSubject.Name = "colSubject";
            // 
            // colSenderName
            // 
            resources.ApplyResources(this.colSenderName, "colSenderName");
            this.colSenderName.FieldName = "SenderName";
            this.colSenderName.Name = "colSenderName";
            // 
            // colSendTime
            // 
            resources.ApplyResources(this.colSendTime, "colSendTime");
            this.colSendTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colSendTime.FieldName = "SendTime";
            this.colSendTime.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colSendTime.Name = "colSendTime";
            // 
            // colEDIContent
            // 
            resources.ApplyResources(this.colEDIContent, "colEDIContent");
            this.colEDIContent.ColumnEdit = this.colLinkEDIContent;
            this.colEDIContent.FieldName = "EDIContent";
            this.colEDIContent.Name = "colEDIContent";
            // 
            // colLinkEDIContent
            // 
            resources.ApplyResources(this.colLinkEDIContent, "colLinkEDIContent");
            this.colLinkEDIContent.Name = "colLinkEDIContent";
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gcMain;
            this.gridView2.Name = "gridView2";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.txtContent);
            this.pnlMain.Controls.Add(this.txtSubject);
            this.pnlMain.Controls.Add(this.label3);
            this.pnlMain.Controls.Add(this.label6);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.label7);
            resources.ApplyResources(this.pnlMain, "pnlMain");
            this.pnlMain.Name = "pnlMain";
            // 
            // txtContent
            // 
            resources.ApplyResources(this.txtContent, "txtContent");
            this.txtContent.Name = "txtContent";
            // 
            // txtSubject
            // 
            resources.ApplyResources(this.txtSubject, "txtSubject");
            this.txtSubject.Name = "txtSubject";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.rbReplace);
            this.panel1.Controls.Add(this.rbCancel);
            this.panel1.Controls.Add(this.rbOriginal);
            this.panel1.Name = "panel1";
            // 
            // rbReplace
            // 
            resources.ApplyResources(this.rbReplace, "rbReplace");
            this.rbReplace.Name = "rbReplace";
            this.rbReplace.UseVisualStyleBackColor = true;
            // 
            // rbCancel
            // 
            resources.ApplyResources(this.rbCancel, "rbCancel");
            this.rbCancel.Name = "rbCancel";
            this.rbCancel.UseVisualStyleBackColor = true;
            // 
            // rbOriginal
            // 
            resources.ApplyResources(this.rbOriginal, "rbOriginal");
            this.rbOriginal.Checked = true;
            this.rbOriginal.Name = "rbOriginal";
            this.rbOriginal.TabStop = true;
            this.rbOriginal.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // pnlPILCommod
            // 
            this.pnlPILCommod.Controls.Add(this.mcmbCommodity);
            this.pnlPILCommod.Controls.Add(this.labCommodity);
            resources.ApplyResources(this.pnlPILCommod, "pnlPILCommod");
            this.pnlPILCommod.Name = "pnlPILCommod";
            // 
            // mcmbCommodity
            // 
            this.mcmbCommodity.EditText = "";
            this.mcmbCommodity.EditValue = null;
            resources.ApplyResources(this.mcmbCommodity, "mcmbCommodity");
            this.mcmbCommodity.Name = "mcmbCommodity";
            this.mcmbCommodity.ReadOnly = false;
            this.mcmbCommodity.RefreshButtonToolTip = "";
            this.mcmbCommodity.ShowRefreshButton = false;
            this.mcmbCommodity.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.mcmbCommodity.ToolTip = "";
            // 
            // labCommodity
            // 
            resources.ApplyResources(this.labCommodity, "labCommodity");
            this.labCommodity.Name = "labCommodity";
            // 
            // panelAange
            // 
            this.panelAange.Controls.Add(this.labelAgent);
            this.panelAange.Controls.Add(this.cmbAgent);
            resources.ApplyResources(this.panelAange, "panelAange");
            this.panelAange.Name = "panelAange";
            // 
            // labelAgent
            // 
            resources.ApplyResources(this.labelAgent, "labelAgent");
            this.labelAgent.Name = "labelAgent";
            // 
            // cmbAgent
            // 
            this.cmbAgent.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.cmbAgent, "cmbAgent");
            this.cmbAgent.Name = "cmbAgent";
            this.cmbAgent.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            this.cmbAgent.Properties.Appearance.Options.UseBackColor = true;
            this.cmbAgent.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("cmbAgent.Properties.Items"), resources.GetString("cmbAgent.Properties.Items1"), ((int)(resources.GetObject("cmbAgent.Properties.Items2")))),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("cmbAgent.Properties.Items3"), resources.GetString("cmbAgent.Properties.Items4"), ((int)(resources.GetObject("cmbAgent.Properties.Items5")))),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("cmbAgent.Properties.Items6"), resources.GetString("cmbAgent.Properties.Items7"), ((int)(resources.GetObject("cmbAgent.Properties.Items8")))),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("cmbAgent.Properties.Items9"), resources.GetString("cmbAgent.Properties.Items10"), ((int)(resources.GetObject("cmbAgent.Properties.Items11")))),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem(resources.GetString("cmbAgent.Properties.Items12"), resources.GetString("cmbAgent.Properties.Items13"), ((int)(resources.GetObject("cmbAgent.Properties.Items14"))))});
            this.cmbAgent.SpecifiedBackColor = System.Drawing.SystemColors.Info;
            this.cmbAgent.SelectedIndexChanged += new System.EventHandler(this.cmbAgent_SelectedIndexChanged);
            // 
            // panelVGM
            // 
            this.panelVGM.Controls.Add(this.txtRemark);
            this.panelVGM.Controls.Add(this.labRamark);
            this.panelVGM.Controls.Add(this.labVGMDate);
            this.panelVGM.Controls.Add(this.txtEmail);
            this.panelVGM.Controls.Add(this.labEmail);
            this.panelVGM.Controls.Add(this.txtTel);
            this.panelVGM.Controls.Add(this.labTel);
            this.panelVGM.Controls.Add(this.txtContack);
            this.panelVGM.Controls.Add(this.labContack);
            this.panelVGM.Controls.Add(this.txtDepartment);
            this.panelVGM.Controls.Add(this.labVGMDep);
            this.panelVGM.Controls.Add(this.dtDate);
            resources.ApplyResources(this.panelVGM, "panelVGM");
            this.panelVGM.Name = "panelVGM";
            // 
            // txtRemark
            // 
            resources.ApplyResources(this.txtRemark, "txtRemark");
            this.txtRemark.Name = "txtRemark";
            // 
            // labRamark
            // 
            resources.ApplyResources(this.labRamark, "labRamark");
            this.labRamark.Name = "labRamark";
            // 
            // labVGMDate
            // 
            resources.ApplyResources(this.labVGMDate, "labVGMDate");
            this.labVGMDate.Name = "labVGMDate";
            // 
            // txtEmail
            // 
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.Name = "txtEmail";
            // 
            // labEmail
            // 
            resources.ApplyResources(this.labEmail, "labEmail");
            this.labEmail.Name = "labEmail";
            // 
            // txtTel
            // 
            resources.ApplyResources(this.txtTel, "txtTel");
            this.txtTel.Name = "txtTel";
            // 
            // labTel
            // 
            resources.ApplyResources(this.labTel, "labTel");
            this.labTel.Name = "labTel";
            // 
            // txtContack
            // 
            resources.ApplyResources(this.txtContack, "txtContack");
            this.txtContack.Name = "txtContack";
            // 
            // labContack
            // 
            resources.ApplyResources(this.labContack, "labContack");
            this.labContack.Name = "labContack";
            // 
            // txtDepartment
            // 
            resources.ApplyResources(this.txtDepartment, "txtDepartment");
            this.txtDepartment.Name = "txtDepartment";
            // 
            // labVGMDep
            // 
            resources.ApplyResources(this.labVGMDep, "labVGMDep");
            this.labVGMDep.Name = "labVGMDep";
            // 
            // dtDate
            // 
            resources.ApplyResources(this.dtDate, "dtDate");
            this.dtDate.Name = "dtDate";
            this.dtDate.Properties.NullDate = "";
            // 
            // linkLog
            // 
            resources.ApplyResources(this.linkLog, "linkLog");
            this.linkLog.Name = "linkLog";
            this.linkLog.TabStop = true;
            this.linkLog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLog_LinkClicked);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.linkLog);
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnTest);
            this.pnlBottom.Controls.Add(this.btnSend);
            resources.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            // 
            // btnTest
            // 
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.Name = "btnTest";
            this.btnTest.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ifcsuM_FYCW1
            // 
            this.ifcsuM_FYCW1.DataSetName = "IFCSUM_FYCW";
            this.ifcsuM_FYCW1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // EDISendForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EDISendForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDescripton.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colLinkEDIContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPILCommod)).EndInit();
            this.pnlPILCommod.ResumeLayout(false);
            this.pnlPILCommod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelAange)).EndInit();
            this.panelAange.ResumeLayout(false);
            this.panelAange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAgent.Properties)).EndInit();
            this.panelVGM.ResumeLayout(false);
            this.panelVGM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContack.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDepartment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ifcsuM_FYCW1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSend;
        private DevExpress.XtraEditors.LabelControl label3;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainer1;
        private DevExpress.XtraEditors.PanelControl panel1;
        private System.Windows.Forms.RadioButton rbReplace;
        private System.Windows.Forms.RadioButton rbCancel;
        private System.Windows.Forms.RadioButton rbOriginal;
        private DevExpress.XtraEditors.LabelControl label6;
        private DevExpress.XtraEditors.LabelControl label7;
        private System.Windows.Forms.LinkLabel linkLog;
        private System.Windows.Forms.BindingSource bsLogs;
        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.MemoEdit txtContent;
        private DevExpress.XtraEditors.TextEdit txtSubject;
        private DevExpress.XtraGrid.GridControl gcMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMain;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colSubject;
        private DevExpress.XtraGrid.Columns.GridColumn colSenderName;
        private DevExpress.XtraGrid.Columns.GridColumn colSendTime;
        private DevExpress.XtraGrid.Columns.GridColumn colEDIContent;
        private DevExpress.XtraEditors.Repository.RepositoryItemHyperLinkEdit colLinkEDIContent;
        private DevExpress.XtraEditors.SplitterControl spLogList;
        private DevExpress.XtraEditors.MemoEdit txtDescripton;
        private DevExpress.XtraGrid.Columns.GridColumn LogType;
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.PanelControl pnlPILCommod;
        private DevExpress.XtraEditors.LabelControl labCommodity;
        private ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbCommodity;
        private DevExpress.XtraEditors.PanelControl panelAange;
        private DevExpress.XtraEditors.LabelControl labelAgent;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbAgent;
        private LongWin.EDI.FileManager.IFCSUM_FYCW ifcsuM_FYCW1;
        private Panel panelVGM;
        private DevExpress.XtraEditors.LabelControl labVGMDate;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.LabelControl labEmail;
        private DevExpress.XtraEditors.TextEdit txtTel;
        private DevExpress.XtraEditors.LabelControl labTel;
        private DevExpress.XtraEditors.TextEdit txtContack;
        private DevExpress.XtraEditors.LabelControl labContack;
        private DevExpress.XtraEditors.TextEdit txtDepartment;
        private DevExpress.XtraEditors.LabelControl labVGMDep;
        private DevExpress.XtraEditors.DateEdit dtDate;
        private DevExpress.XtraEditors.MemoEdit txtRemark;
        private DevExpress.XtraEditors.LabelControl labRamark;
        private DevExpress.XtraEditors.SimpleButton btnTest;
    }
}