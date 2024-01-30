namespace ICP.FAM.UI.Invoice
{
    partial class InvoiceContrast
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbCompany = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.labStartDate = new DevExpress.XtraEditors.LabelControl();
            this.labCompanyID = new DevExpress.XtraEditors.LabelControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.DateStart = new DevExpress.XtraEditors.DateEdit();
            this.DateEnd = new DevExpress.XtraEditors.DateEdit();
            this.labEndDate = new DevExpress.XtraEditors.LabelControl();
            this.gcInvoice = new ICP.Framework.ClientComponents.Controls.LWGridControl();
            this.gvInvoice = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colICPNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colICPAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.curCureency_ = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colSKNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtRate = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colSKAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.reQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemTextEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rtxtRemark = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.cmbWay = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.cmbCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.seUnitPrice = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.cmbUnit = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.rseAmount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this._curRate = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.ICP = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.curCureency_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rseAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._curRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCompany
            // 
            this.cmbCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCompany.Location = new System.Drawing.Point(76, 13);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbCompany.Properties.Appearance.Options.UseBackColor = true;
            this.cmbCompany.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCompany.Size = new System.Drawing.Size(116, 21);
            this.cmbCompany.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCompany.TabIndex = 10;
            // 
            // labStartDate
            // 
            this.labStartDate.Location = new System.Drawing.Point(209, 16);
            this.labStartDate.Name = "labStartDate";
            this.labStartDate.Size = new System.Drawing.Size(48, 14);
            this.labStartDate.TabIndex = 9;
            this.labStartDate.Text = "开始日期";
            // 
            // labCompanyID
            // 
            this.labCompanyID.Location = new System.Drawing.Point(16, 16);
            this.labCompanyID.Name = "labCompanyID";
            this.labCompanyID.Size = new System.Drawing.Size(48, 14);
            this.labCompanyID.TabIndex = 8;
            this.labCompanyID.Text = "所属公司";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(595, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "查询";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // DateStart
            // 
            this.DateStart.CausesValidation = false;
            this.DateStart.EditValue = null;
            this.DateStart.Location = new System.Drawing.Point(269, 13);
            this.DateStart.Name = "DateStart";
            this.DateStart.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.DateStart.Properties.Appearance.Options.UseBackColor = true;
            this.DateStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateStart.Properties.Mask.EditMask = "";
            this.DateStart.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.DateStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateStart.Size = new System.Drawing.Size(116, 21);
            this.DateStart.TabIndex = 11;
            // 
            // DateEnd
            // 
            this.DateEnd.CausesValidation = false;
            this.DateEnd.EditValue = null;
            this.DateEnd.Location = new System.Drawing.Point(459, 13);
            this.DateEnd.Name = "DateEnd";
            this.DateEnd.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.DateEnd.Properties.Appearance.Options.UseBackColor = true;
            this.DateEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DateEnd.Properties.Mask.EditMask = "";
            this.DateEnd.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.DateEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DateEnd.Size = new System.Drawing.Size(116, 21);
            this.DateEnd.TabIndex = 13;
            // 
            // labEndDate
            // 
            this.labEndDate.Location = new System.Drawing.Point(399, 16);
            this.labEndDate.Name = "labEndDate";
            this.labEndDate.Size = new System.Drawing.Size(48, 14);
            this.labEndDate.TabIndex = 12;
            this.labEndDate.Text = "结束日期";
            // 
            // gcInvoice
            // 
            this.gcInvoice.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gcInvoice.Location = new System.Drawing.Point(0, 41);
            this.gcInvoice.MainView = this.bandedGridView1;
            this.gcInvoice.Name = "gcInvoice";
            this.gcInvoice.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.cmbWay,
            this.cmbCurrency,
            this.reQuantity,
            this.seUnitPrice,
            this.rtxtRemark,
            this.cmbUnit,
            this.rseAmount,
            this.repositoryItemCheckEdit2,
            this.btnEdit1,
            this.curCureency_,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2,
            this.repositoryItemTextEdit3,
            this._curRate,
            this.txtRate});
            this.gcInvoice.Size = new System.Drawing.Size(706, 298);
            this.gcInvoice.TabIndex = 14;
            this.gcInvoice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInvoice,
            this.bandedGridView1});
            // 
            // gvInvoice
            // 
            this.gvInvoice.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colICPNo,
            this.colICPAmount,
            this.colSKNo,
            this.colSKAmount});
            this.gvInvoice.GridControl = this.gcInvoice;
            this.gvInvoice.Name = "gvInvoice";
            this.gvInvoice.OptionsSelection.MultiSelect = true;
            this.gvInvoice.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvInvoice.OptionsView.ShowGroupPanel = false;
            // 
            // colICPNo
            // 
            this.colICPNo.Caption = "发票号";
            this.colICPNo.FieldName = "ICPNo";
            this.colICPNo.Name = "colICPNo";
            this.colICPNo.Visible = true;
            this.colICPNo.VisibleIndex = 0;
            this.colICPNo.Width = 81;
            // 
            // btnEdit1
            // 
            this.btnEdit1.AutoHeight = false;
            this.btnEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnEdit1.Name = "btnEdit1";
            // 
            // colICPAmount
            // 
            this.colICPAmount.Caption = "金额";
            this.colICPAmount.DisplayFormat.FormatString = "\"N4\"";
            this.colICPAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colICPAmount.FieldName = "ICPAmount";
            this.colICPAmount.Name = "colICPAmount";
            this.colICPAmount.Visible = true;
            this.colICPAmount.VisibleIndex = 1;
            // 
            // curCureency_
            // 
            this.curCureency_.AutoHeight = false;
            this.curCureency_.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.curCureency_.Name = "curCureency_";
            // 
            // colSKNo
            // 
            this.colSKNo.Caption = "发票号";
            this.colSKNo.FieldName = "SKNo";
            this.colSKNo.Name = "colSKNo";
            this.colSKNo.OptionsFilter.AllowAutoFilter = false;
            this.colSKNo.Visible = true;
            this.colSKNo.VisibleIndex = 2;
            this.colSKNo.Width = 79;
            // 
            // txtRate
            // 
            this.txtRate.AutoHeight = false;
            this.txtRate.Name = "txtRate";
            // 
            // colSKAmount
            // 
            this.colSKAmount.Caption = "金额";
            this.colSKAmount.DisplayFormat.FormatString = "N4";
            this.colSKAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSKAmount.FieldName = "SKAmount";
            this.colSKAmount.Name = "colSKAmount";
            this.colSKAmount.Visible = true;
            this.colSKAmount.VisibleIndex = 3;
            this.colSKAmount.Width = 81;
            // 
            // reQuantity
            // 
            this.reQuantity.AutoHeight = false;
            this.reQuantity.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.reQuantity.IsFloatValue = false;
            this.reQuantity.Mask.EditMask = "N00";
            this.reQuantity.Name = "reQuantity";
            // 
            // repositoryItemTextEdit3
            // 
            this.repositoryItemTextEdit3.AutoHeight = false;
            this.repositoryItemTextEdit3.Name = "repositoryItemTextEdit3";
            // 
            // rtxtRemark
            // 
            this.rtxtRemark.AutoHeight = false;
            this.rtxtRemark.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rtxtRemark.Name = "rtxtRemark";
            this.rtxtRemark.ShowIcon = false;
            // 
            // cmbWay
            // 
            this.cmbWay.AutoHeight = false;
            this.cmbWay.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbWay.Name = "cmbWay";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoHeight = false;
            this.cmbCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCurrency.Name = "cmbCurrency";
            // 
            // seUnitPrice
            // 
            this.seUnitPrice.AutoHeight = false;
            this.seUnitPrice.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seUnitPrice.Mask.EditMask = "F3";
            this.seUnitPrice.Name = "seUnitPrice";
            // 
            // cmbUnit
            // 
            this.cmbUnit.AutoHeight = false;
            this.cmbUnit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbUnit.Name = "cmbUnit";
            // 
            // rseAmount
            // 
            this.rseAmount.AutoHeight = false;
            this.rseAmount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rseAmount.Mask.EditMask = "F2";
            this.rseAmount.Name = "rseAmount";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // _curRate
            // 
            this._curRate.AutoHeight = false;
            this._curRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this._curRate.Name = "_curRate";
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.ICP,
            this.gridBand2});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn1,
            this.bandedGridColumn2,
            this.bandedGridColumn3,
            this.bandedGridColumn4});
            this.bandedGridView1.GridControl = this.gcInvoice;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsSelection.MultiSelect = true;
            this.bandedGridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "发票号";
            this.bandedGridColumn1.FieldName = "ICPNo";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 65;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "金额";
            this.bandedGridColumn2.DisplayFormat.FormatString = "\"N4\"";
            this.bandedGridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn2.FieldName = "ICPAmount";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 60;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "发票号";
            this.bandedGridColumn3.FieldName = "SKNo";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.OptionsFilter.AllowAutoFilter = false;
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.Width = 63;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.Caption = "金额";
            this.bandedGridColumn4.DisplayFormat.FormatString = "N4";
            this.bandedGridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn4.FieldName = "SKAmount";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 68;
            // 
            // ICP
            // 
            this.ICP.Caption = "ICP";
            this.ICP.Columns.Add(this.bandedGridColumn1);
            this.ICP.Columns.Add(this.bandedGridColumn2);
            this.ICP.Name = "ICP";
            this.ICP.Width = 125;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "税控";
            this.gridBand2.Columns.Add(this.bandedGridColumn3);
            this.gridBand2.Columns.Add(this.bandedGridColumn4);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 131;
            // 
            // InvoiceContrast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcInvoice);
            this.Controls.Add(this.DateEnd);
            this.Controls.Add(this.labEndDate);
            this.Controls.Add(this.DateStart);
            this.Controls.Add(this.cmbCompany);
            this.Controls.Add(this.labStartDate);
            this.Controls.Add(this.labCompanyID);
            this.Controls.Add(this.btnOK);
            this.Name = "InvoiceContrast";
            this.Size = new System.Drawing.Size(706, 339);
            this.Load += new System.EventHandler(this.InvoiceContrast_Load);
            this.Controls.SetChildIndex(this.btnOK, 0);
            this.Controls.SetChildIndex(this.labCompanyID, 0);
            this.Controls.SetChildIndex(this.labStartDate, 0);
            this.Controls.SetChildIndex(this.cmbCompany, 0);
            this.Controls.SetChildIndex(this.DateStart, 0);
            this.Controls.SetChildIndex(this.labEndDate, 0);
            this.Controls.SetChildIndex(this.DateEnd, 0);
            this.Controls.SetChildIndex(this.gcInvoice, 0);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCompany.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DateEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.curCureency_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rseAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._curRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCompany;
        private DevExpress.XtraEditors.LabelControl labStartDate;
        private DevExpress.XtraEditors.LabelControl labCompanyID;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.DateEdit DateStart;
        private DevExpress.XtraEditors.DateEdit DateEnd;
        private DevExpress.XtraEditors.LabelControl labEndDate;
        private Framework.ClientComponents.Controls.LWGridControl gcInvoice;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInvoice;
        private DevExpress.XtraGrid.Columns.GridColumn colICPNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btnEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colICPAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox curCureency_;
        private DevExpress.XtraGrid.Columns.GridColumn colSKNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtRate;
        private DevExpress.XtraGrid.Columns.GridColumn colSKAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit reQuantity;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit rtxtRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbWay;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit seUnitPrice;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox cmbUnit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit rseAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox _curRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand ICP;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
    }
}
