using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.Commands;

namespace ICP.FCM.AirImport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public class OrderFastSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {      
        #region service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IAirImportService oiService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        #endregion 

        #region 属性


        protected OIBusinessNoSearchType PartNoSearchType
        { get { return (OIBusinessNoSearchType)Enum.Parse(typeof(OIBusinessNoSearchType), cmbNoSearchType.EditValue.ToString()); } }
        protected OIBusinessCustomerSearchType PartCustomerSearchType
        { get { return (OIBusinessCustomerSearchType)Enum.Parse(typeof(OIBusinessCustomerSearchType), cmbCustomerSearchType.EditValue.ToString()); } }
        protected OIBusinessPortSearchType PartPortSearchType
        { get { return (OIBusinessPortSearchType)Enum.Parse(typeof(OIBusinessPortSearchType), cmbPortSearchType.EditValue.ToString()); } }
        protected OIBusinessDateSearchType PartDateSearchType
        { get { return (OIBusinessDateSearchType)Enum.Parse(typeof(OIBusinessDateSearchType), cmbDateSearchType.EditValue.ToString()); } }


        #endregion

        #region init

        public OrderFastSearchPart()
        {
            InitializeComponent();
            if (DesignMode == false)
            {
                this.Load += new EventHandler(BookingFastSearchPart_Load);
                if (LocalData.IsEnglish == false) SetCnText();
            }
        }
        void BookingFastSearchPart_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            if (LocalData.IsEnglish) SetCnText();

            InitControls();
            SetControlsEnterToSearch();
        }

        private void SetCnText()
        {
            this.btnSearch.Text = "查询(&S)";
            this.llabMore.Text = "更多";
        }

        private void SetControlsEnterToSearch()
        {
            foreach (Control cl in panel1.Controls)
            {
                if (cl is TextEdit)
                {
                    cl.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
                        {
                            this.btnSearch.PerformClick();
                        }
                        if (e.KeyCode == Keys.F3)
                        {
                            ClareText();
                        }
                    };

                }
            }

            llabMore.Click += delegate { OnClickMore(); };
        }

        protected void OnClickMore()
        {
            Workitem.Commands[OIOrderCommandConstants.Command_ShowSearch].Execute();
        }

        private void ClareText()
        {
            this.txtCustomer.Text = string.Empty;
            this.txtNo.Text = string.Empty;
            this.txtPort.Text = string.Empty;
            this.cmbDateType.SelectedIndex = 0;

        }


        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessNoSearchType>> noSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessNoSearchType>(LocalData.IsEnglish);
            foreach (var item in noSearchTypes)
            {
                cmbNoSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessCustomerSearchType>> customerSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessCustomerSearchType>(LocalData.IsEnglish);
            foreach (var item in customerSearchTypes)
            {
                cmbCustomerSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessPortSearchType>> portSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessPortSearchType>(LocalData.IsEnglish);
            foreach (var item in portSearchTypes)
            {
                cmbPortSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            //时间内容
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateValueSearchType>> dateValueSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateValueSearchType>(LocalData.IsEnglish);
            foreach (var item in dateValueSearchTypes)
            {
                cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }


            cmbNoSearchType.SelectedIndex = cmbCustomerSearchType.SelectedIndex = cmbPortSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = cmbDateType .SelectedIndex= 0;
        }

        public DateTime? FromDate
        {
            get
            {
                switch (cmbDateType.SelectedIndex)
                {
                    case 0:
                        return null;
                    case 1:
                        return DateTime.Now.Date.AddDays(-7);
                    case 2:
                        return DateTime.Now.Date.AddDays(-30);
                    case 3:
                        return DateTime.Now.Date.AddDays(-365);
                };
                return null;
            }
        }

        public DateTime? ToDate
        {
            get
            {
                if (cmbDateType.SelectedIndex <= 0)
                    return null;
                else
                    return Utility.GetEndDate(DateTime.Now);
            }
        }

        void SetSearchTypes(ref OIBusinessNoSearchType noSearchType, ref OIBusinessCustomerSearchType customerSearchType,
                           ref OIBusinessPortSearchType portSearchType, ref OIBusinessDateSearchType dateSearchType)
        {
            noSearchType = (OIBusinessNoSearchType)Enum.Parse(typeof(OIBusinessNoSearchType), cmbNoSearchType.EditValue.ToString());
            customerSearchType = (OIBusinessCustomerSearchType)Enum.Parse(typeof(OIBusinessCustomerSearchType), cmbCustomerSearchType.EditValue.ToString());
            portSearchType = (OIBusinessPortSearchType)Enum.Parse(typeof(OIBusinessPortSearchType), cmbPortSearchType.EditValue.ToString());
            dateSearchType = (OIBusinessDateSearchType)Enum.Parse(typeof(OIBusinessDateSearchType), cmbDateSearchType.EditValue.ToString());
        }

        #endregion

        #region ISearchPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn
        [CommandHandler(OIOrderCommandConstants.Command_FastSecharData)]
        public void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

      

        public override object GetData()
        {
            //Guid[] companyIDs = Utility.GetDepartmentIDs(LocalData.UserInfo.LoginID,userService);
            List<UserOrganizationTreeList> userOrganizationTrees = userService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);
            List<Guid> companyIDs = new List<Guid>();
            foreach (var item in userOrganizationTrees)
            {
                companyIDs.Add(item.ID);
            }

            List<AirOrderList> list = oiService.GetAIOrderListByFastSearch(
                                 companyIDs.ToArray(),
                                 LocalData.UserInfo.LoginID,
                                 (OIBusinessNoSearchType)this.cmbNoSearchType.EditValue,
                                 this.txtNo.Text.Trim(),
                                 (OIBusinessCustomerSearchType)this.cmbCustomerSearchType.EditValue,
                                 this.txtCustomer.Text.Trim(),
                                 (OIBusinessPortSearchType)this.cmbPortSearchType.EditValue,
                                 this.txtPort.Text.Trim(),
                                 (OIBusinessDateSearchType)this.cmbDateSearchType.EditValue,
                                 FromDate,
                                 ToDate,
                                 0);

            return list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtNo.Text = txtCustomer.Text = txtPort.Text = string.Empty;
            cmbCustomerSearchType.SelectedIndex = cmbDateSearchType.SelectedIndex = cmbDateType.SelectedIndex = cmbNoSearchType.SelectedIndex = 0;
        }

        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbNoSearchType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.txtNo = new DevExpress.XtraEditors.TextEdit();
            this.cmbCustomerSearchType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbPortSearchType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.cmbDateSearchType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.llabMore = new System.Windows.Forms.LinkLabel();
            this.cmbDateType = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtPort = new DevExpress.XtraEditors.TextEdit();
            this.txtCustomer = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNoSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPortSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateSearchType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbNoSearchType
            // 
            this.cmbNoSearchType.Location = new System.Drawing.Point(52, 5);
            this.cmbNoSearchType.Name = "cmbNoSearchType";
            this.cmbNoSearchType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNoSearchType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbNoSearchType.Size = new System.Drawing.Size(86, 21);
            this.cmbNoSearchType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbNoSearchType.TabIndex = 14;
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(140, 5);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(89, 21);
            this.txtNo.TabIndex = 12;
            // 
            // cmbCustomerSearchType
            // 
            this.cmbCustomerSearchType.Location = new System.Drawing.Point(235, 5);
            this.cmbCustomerSearchType.Name = "cmbCustomerSearchType";
            this.cmbCustomerSearchType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomerSearchType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbCustomerSearchType.Size = new System.Drawing.Size(86, 21);
            this.cmbCustomerSearchType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbCustomerSearchType.TabIndex = 14;
            // 
            // cmbPortSearchType
            // 
            this.cmbPortSearchType.Location = new System.Drawing.Point(418, 5);
            this.cmbPortSearchType.Name = "cmbPortSearchType";
            this.cmbPortSearchType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPortSearchType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbPortSearchType.Size = new System.Drawing.Size(86, 21);
            this.cmbPortSearchType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbPortSearchType.TabIndex = 14;
            // 
            // cmbDateSearchType
            // 
            this.cmbDateSearchType.Location = new System.Drawing.Point(601, 5);
            this.cmbDateSearchType.Name = "cmbDateSearchType";
            this.cmbDateSearchType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDateSearchType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbDateSearchType.Size = new System.Drawing.Size(86, 21);
            this.cmbDateSearchType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbDateSearchType.TabIndex = 14;
            // 
            // llabMore
            // 
            this.llabMore.AutoSize = true;
            this.llabMore.Location = new System.Drawing.Point(3, 9);
            this.llabMore.Name = "llabMore";
            this.llabMore.Size = new System.Drawing.Size(34, 14);
            this.llabMore.TabIndex = 17;
            this.llabMore.TabStop = true;
            this.llabMore.Text = "&More";
            // 
            // cmbDateType
            // 
            this.cmbDateType.Location = new System.Drawing.Point(693, 5);
            this.cmbDateType.Name = "cmbDateType";
            this.cmbDateType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDateType.Properties.ExportMode = DevExpress.XtraEditors.Repository.ExportMode.Value;
            this.cmbDateType.Size = new System.Drawing.Size(86, 21);
            this.cmbDateType.SpecifiedBackColor = System.Drawing.Color.White;
            this.cmbDateType.TabIndex = 14;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(788, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 19;
            this.btnSearch.Text = "&Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(875, 36);
            this.panelControl2.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.llabMore);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cmbDateSearchType);
            this.panel1.Controls.Add(this.txtNo);
            this.panel1.Controls.Add(this.cmbDateType);
            this.panel1.Controls.Add(this.cmbPortSearchType);
            this.panel1.Controls.Add(this.cmbNoSearchType);
            this.panel1.Controls.Add(this.cmbCustomerSearchType);
            this.panel1.Controls.Add(this.txtPort);
            this.panel1.Controls.Add(this.txtCustomer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 32);
            this.panel1.TabIndex = 0;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(510, 5);
            this.txtPort.Name = "txtPort";
            this.txtPort.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPort.Properties.Appearance.Options.UseBackColor = true;
            this.txtPort.Size = new System.Drawing.Size(86, 21);
            this.txtPort.TabIndex = 18;
            this.txtPort.TabStop = false;
            // 
            // txtCustomer
            // 
            this.txtCustomer.Location = new System.Drawing.Point(326, 5);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtCustomer.Properties.Appearance.Options.UseBackColor = true;
            this.txtCustomer.Size = new System.Drawing.Size(86, 21);
            this.txtCustomer.TabIndex = 18;
            this.txtCustomer.TabStop = false;
            // 
            // OrderFastSearchPart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Name = "OrderFastSearchPart";
            this.Size = new System.Drawing.Size(875, 36);
            ((System.ComponentModel.ISupportInitialize)(this.cmbNoSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPortSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateSearchType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDateType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbNoSearchType;
        protected DevExpress.XtraEditors.TextEdit txtNo;
        protected ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbCustomerSearchType;
        protected ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbPortSearchType;
        protected ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbDateSearchType;
        protected System.Windows.Forms.LinkLabel llabMore;
        protected ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbDateType;
        protected DevExpress.XtraEditors.SimpleButton btnSearch;
        protected DevExpress.XtraEditors.PanelControl panelControl2;
        protected System.Windows.Forms.Panel panel1;
        protected DevExpress.XtraEditors.TextEdit txtPort;
        protected DevExpress.XtraEditors.TextEdit txtCustomer;
    }
      
}
