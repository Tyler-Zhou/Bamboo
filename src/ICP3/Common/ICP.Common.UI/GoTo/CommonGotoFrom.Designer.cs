namespace ICP.Common.UI.GoTo
{
    partial class CommonGotoFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonGotoFrom));
            this.butGoTo = new DevExpress.XtraEditors.SimpleButton();
            this.txtQuery = new DevExpress.XtraEditors.TextEdit();
            this.cbRecentShipments = new DevExpress.XtraEditors.CheckEdit();
            this.guidControlList = new DevExpress.XtraGrid.GridControl();
            this.bindDataSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.OperationNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MBLNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HBLNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SONO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ContainerNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.linkSetting = new System.Windows.Forms.LinkLabel();
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.linkmore = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Mblid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Hblid = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.txtQuery.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbRecentShipments.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guidControlList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindDataSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).BeginInit();
            this.SuspendLayout();
            // 
            // butGoTo
            // 
            this.butGoTo.Location = new System.Drawing.Point(320, 16);
            this.butGoTo.Name = "butGoTo";
            this.butGoTo.Size = new System.Drawing.Size(101, 31);
            this.butGoTo.TabIndex = 18;
            this.butGoTo.Text = "GoTo";
            this.butGoTo.Click += new System.EventHandler(this.butGoTo_Click);
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(125, 26);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(189, 21);
            this.txtQuery.TabIndex = 17;
            // 
            // cbRecentShipments
            // 
            this.cbRecentShipments.EditValue = true;
            this.cbRecentShipments.Location = new System.Drawing.Point(123, 1);
            this.cbRecentShipments.Name = "cbRecentShipments";
            this.cbRecentShipments.Properties.Caption = "Recent Shipments";
            this.cbRecentShipments.Size = new System.Drawing.Size(178, 19);
            this.cbRecentShipments.TabIndex = 16;
            // 
            // guidControlList
            // 
            this.guidControlList.DataSource = this.bindDataSource;
            this.guidControlList.Location = new System.Drawing.Point(12, 170);
            this.guidControlList.MainView = this.gridViewList;
            this.guidControlList.Name = "guidControlList";
            this.guidControlList.Size = new System.Drawing.Size(409, 158);
            this.guidControlList.TabIndex = 19;
            this.guidControlList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewList});
            this.guidControlList.Visible = false;
            // 
            // gridViewList
            // 
            this.gridViewList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.OperationNo,
            this.MBLNO,
            this.HBLNO,
            this.SONO,
            this.ContainerNo,
            this.Mblid,
            this.Hblid});
            this.gridViewList.GridControl = this.guidControlList;
            this.gridViewList.Name = "gridViewList";
            this.gridViewList.OptionsBehavior.Editable = false;
            this.gridViewList.OptionsBehavior.ReadOnly = true;
            this.gridViewList.OptionsView.ShowGroupPanel = false;
            this.gridViewList.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewList_RowClick);
            // 
            // OperationNo
            // 
            this.OperationNo.Caption = "No";
            this.OperationNo.FieldName = "OperationNo";
            this.OperationNo.Name = "OperationNo";
            this.OperationNo.Visible = true;
            this.OperationNo.VisibleIndex = 0;
            // 
            // MBLNO
            // 
            this.MBLNO.Caption = "MBLNO";
            this.MBLNO.FieldName = "Mblno";
            this.MBLNO.Name = "MBLNO";
            this.MBLNO.Visible = true;
            this.MBLNO.VisibleIndex = 1;
            // 
            // HBLNO
            // 
            this.HBLNO.Caption = "HBLNO";
            this.HBLNO.FieldName = "Hblno";
            this.HBLNO.Name = "HBLNO";
            this.HBLNO.Visible = true;
            this.HBLNO.VisibleIndex = 2;
            // 
            // SONO
            // 
            this.SONO.Caption = "SONO";
            this.SONO.FieldName = "Sono";
            this.SONO.Name = "SONO";
            this.SONO.Visible = true;
            this.SONO.VisibleIndex = 3;
            // 
            // ContainerNo
            // 
            this.ContainerNo.Caption = "CntNo";
            this.ContainerNo.FieldName = "ContainerNo";
            this.ContainerNo.Name = "ContainerNo";
            this.ContainerNo.Visible = true;
            this.ContainerNo.VisibleIndex = 4;
            // 
            // linkSetting
            // 
            this.linkSetting.AutoSize = true;
            this.linkSetting.Location = new System.Drawing.Point(390, 59);
            this.linkSetting.Name = "linkSetting";
            this.linkSetting.Size = new System.Drawing.Size(31, 14);
            this.linkSetting.TabIndex = 20;
            this.linkSetting.TabStop = true;
            this.linkSetting.Text = "设置";
            this.linkSetting.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSetting_LinkClicked);
            // 
            // tablePanel
            // 
            this.tablePanel.AutoSize = true;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tablePanel.Location = new System.Drawing.Point(14, 3);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Size = new System.Drawing.Size(103, 0);
            this.tablePanel.TabIndex = 1;
            // 
            // linkmore
            // 
            this.linkmore.AutoSize = true;
            this.linkmore.Location = new System.Drawing.Point(317, 59);
            this.linkmore.Name = "linkmore";
            this.linkmore.Size = new System.Drawing.Size(31, 14);
            this.linkmore.TabIndex = 21;
            this.linkmore.TabStop = true;
            this.linkmore.Text = "更多";
            this.linkmore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkmore_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Location = new System.Drawing.Point(59, 341);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(0, 0);
            this.panel1.TabIndex = 22;
            // 
            // Mblid
            // 
            this.Mblid.Caption = "Mblid";
            this.Mblid.FieldName = "Mblid";
            this.Mblid.Name = "Mblid";
            // 
            // Hblid
            // 
            this.Hblid.Caption = "Hblid";
            this.Hblid.FieldName = "Hblid";
            this.Hblid.Name = "Hblid";
            // 
            // CommonGotoFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 340);
            this.Controls.Add(this.tablePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.guidControlList);
            this.Controls.Add(this.linkSetting);
            this.Controls.Add(this.linkmore);
            this.Controls.Add(this.butGoTo);
            this.Controls.Add(this.cbRecentShipments);
            this.Controls.Add(this.txtQuery);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CommonGotoFrom";
            this.Text = "Go To";
            this.Load += new System.EventHandler(this.CommonGotoFrom_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CommonGotoFrom_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CommonGotoFrom_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.txtQuery.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbRecentShipments.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guidControlList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindDataSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton butGoTo;
        private DevExpress.XtraEditors.TextEdit txtQuery;
        private DevExpress.XtraEditors.CheckEdit cbRecentShipments;
        private DevExpress.XtraGrid.GridControl guidControlList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewList;
        private System.Windows.Forms.BindingSource bindDataSource;
        private System.Windows.Forms.LinkLabel linkSetting;
        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.LinkLabel linkmore;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.Columns.GridColumn OperationNo;
        private DevExpress.XtraGrid.Columns.GridColumn MBLNO;
        private DevExpress.XtraGrid.Columns.GridColumn HBLNO;
        private DevExpress.XtraGrid.Columns.GridColumn SONO;
        private DevExpress.XtraGrid.Columns.GridColumn ContainerNo;
        private DevExpress.XtraGrid.Columns.GridColumn Mblid;
        private DevExpress.XtraGrid.Columns.GridColumn Hblid;
    }
}