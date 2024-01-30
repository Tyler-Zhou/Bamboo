using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public class OrderFastSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {      
        #region service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanImportService OceanImportService 
        {
            get 
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }

        public ICP.Sys.ServiceInterface.IUserService UserService 
        {
            get 
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

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
            if (!LocalData.IsDesignMode)
            {
                this.Disposed += delegate 
                {
                    OnSearched = null;
                    RemoveControlsEnterToSearchHandle();
                    this.cmbDateType.OnFirstEnter -= this.OncmbDateTypeFirstEnter;
                    this.cmbNoSearchType.OnFirstEnter -= this.OncmbNoSearchTypeFirstEnter;
                    this.cmbPortSearchType.OnFirstEnter -= this.OncmbPortSearchTypeFirstEnter;
                    this.cmbCustomerSearchType.OnFirstEnter -= this.OncmbCustomerSearchTypeFirstEnter;
                    this.cmbDateSearchType.OnFirstEnter -= this.OncmbDateSearchTypeFirstEnter;
                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                };
               
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                SetControlsEnterToSearch();
            }
        }

        private void RemoveControlsEnterToSearchHandle()
        {
            foreach (Control cl in panel1.Controls)
            {
                if (cl is TextEdit)
                {
                    cl.KeyDown -= this.OnControlKeyDown;

                }
            }

            llabMore.Click -= this.OnLabMoreClick;
        }


        private void SetControlsEnterToSearch()
        {
            foreach (Control cl in panel1.Controls)
            {
                if (cl is TextEdit)
                {
                    cl.KeyDown += this.OnControlKeyDown;
                  
                }
            }

            llabMore.Click += this.OnLabMoreClick;
        }
        private void OnControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                this.btnSearch.PerformClick();
            }
            if (e.KeyCode == Keys.F3)
            {
                ClareText();
            }
        }
        private void OnLabMoreClick(object sender, EventArgs e)
        {
            OnClickMore();
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
        private void OncmbNoSearchTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessNoSearchType>> noSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessNoSearchType>(LocalData.IsEnglish);
            cmbNoSearchType.Properties.BeginUpdate();
            this.cmbNoSearchType.Properties.Items.Clear();
            foreach (var item in noSearchTypes)
            {
                cmbNoSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbNoSearchType.Properties.EndUpdate();
        }
        private void OncmbCustomerSearchTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessCustomerSearchType>> customerSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessCustomerSearchType>(LocalData.IsEnglish);
            cmbCustomerSearchType.Properties.BeginUpdate();
            this.cmbCustomerSearchType.Properties.Items.Clear();
            foreach (var item in customerSearchTypes)
            {
                cmbCustomerSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbCustomerSearchType.Properties.EndUpdate();
        }
        private void OncmbPortSearchTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessPortSearchType>> portSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessPortSearchType>(LocalData.IsEnglish);
            cmbPortSearchType.Properties.BeginUpdate();
            this.cmbPortSearchType.Properties.Items.Clear();
            foreach (var item in portSearchTypes)
            {
                cmbPortSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbPortSearchType.Properties.EndUpdate();
        }
        private void OncmbDateSearchTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
            cmbDateSearchType.Properties.BeginUpdate();
            this.cmbDateSearchType.Properties.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.Properties.EndUpdate();
        }
        private void OncmbDateTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateValueSearchType>> dateValueSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateValueSearchType>(LocalData.IsEnglish);
            cmbDateType.Properties.BeginUpdate();
            this.cmbDateType.Properties.Items.Clear();
            foreach (var item in dateValueSearchTypes)
            {
                cmbDateType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateType.Properties.EndUpdate();
        }

        private void InitControls()
        {

            this.cmbNoSearchType.ShowSelectedValue(0,LocalData.IsEnglish?"All No":"全部单号");
            this.cmbCustomerSearchType.ShowSelectedValue(0, LocalData.IsEnglish ? "All Customer" : "全部客户");
            this.cmbPortSearchType.ShowSelectedValue(0, LocalData.IsEnglish ? "All Port" : "全部地点");
            this.cmbDateSearchType.ShowSelectedValue(0, LocalData.IsEnglish ? "All Date" : "全部日期");
            this.cmbDateType.ShowSelectedValue(0, LocalData.IsEnglish ? "UnKnow" : "不确定");

            //单号类型
            this.cmbNoSearchType.OnFirstEnter += this.OncmbNoSearchTypeFirstEnter;
        
            //客户类型
            this.cmbCustomerSearchType.OnFirstEnter += this.OncmbCustomerSearchTypeFirstEnter;
       

            //地点类型
            this.cmbPortSearchType.OnFirstEnter += this.OncmbPortSearchTypeFirstEnter;
       

            //时间类型
            this.cmbDateSearchType.OnFirstEnter += this.OncmbDateSearchTypeFirstEnter;
         

            //时间内容
            this.cmbDateType.OnFirstEnter += this.OncmbDateTypeFirstEnter;
       
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
                    return DateTime.Now.DateAttachEndTime();
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
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

      

        public override object GetData()
        {
            //Guid[] companyIDs = Utility.GetDepartmentIDs(LocalData.UserInfo.LoginID,userService);
            List<UserOrganizationTreeList> userOrganizationTrees = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);
            List<Guid> companyIDs = new List<Guid>();
            foreach (var item in userOrganizationTrees)
            {
                companyIDs.Add(item.ID);
            }

            List<OceanOrderList> list = OceanImportService.GetOIOrderListByFastSearch(
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

        protected LWImageComboBoxEdit cmbNoSearchType;
        protected DevExpress.XtraEditors.TextEdit txtNo;
        protected LWImageComboBoxEdit cmbCustomerSearchType;
        protected LWImageComboBoxEdit cmbPortSearchType;
        protected LWImageComboBoxEdit cmbDateSearchType;
        protected System.Windows.Forms.LinkLabel llabMore;
        protected LWImageComboBoxEdit cmbDateType;
        protected DevExpress.XtraEditors.SimpleButton btnSearch;
        protected DevExpress.XtraEditors.PanelControl panelControl2;
        protected System.Windows.Forms.Panel panel1;
        protected DevExpress.XtraEditors.TextEdit txtPort;
        protected DevExpress.XtraEditors.TextEdit txtCustomer;
    }
      
}
