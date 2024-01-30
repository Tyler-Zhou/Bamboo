using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.TransportFoundation.VesselVoyage
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class VesselEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region init

        VesselInfo CurrentData
        {
            get { return bindingSource1.DataSource as VesselInfo; }
            set { bindingSource1.DataSource = value; }
        }

        public VesselEditPart()
        {
            InitializeComponent();
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.mscmbCountry.Enter -= this.OnMscmbCountryEnter;
                this.cmbCarrier.SelectedIndexChanged -= this.OnCmbCarrierSelectedIndexChanged;
                this.cmbCarrier.OnFirstEnter -= this.OncmbCarrierFirstEnter;
                this.DataChanged = null;
                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }
        private void SetCnText()
        {
            this.labCarrier.Text = "船东";
            labCode.Text = "代码";
            labName.Text = "名称";
            lblVesselFlag.Text = "船籍";
        }

        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            base.OnLoad(e);
        }
        private bool iscmbCarrierEntered = false;
        private void OncmbCarrierFirstEnter(object sender, EventArgs e)
        {
            List<CustomerList> customers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                           string.Empty, string.Empty, null, null, null,
                                                                           CustomerType.Carrier, null, null, null, null, null, 0);

            CustomerList emptyCustomer = new CustomerList();
            emptyCustomer.CName = emptyCustomer.EName = string.Empty;
            emptyCustomer.ID = Guid.Empty;
            this.cmbCarrier.Properties.BeginUpdate();
            this.cmbCarrier.Properties.Items.Clear();
            customers.Insert(0, emptyCustomer);

            this.mscmbCountry.Enter += OnMscmbCountryEnter;
            foreach (CustomerList item in customers)
            {
                cmbCarrier.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            this.cmbCarrier.Properties.EndUpdate();
            iscmbCarrierEntered = true;
            cmbCarrier.SelectedIndexChanged += OnCmbCarrierSelectedIndexChanged;
           
        }
        void InitControls()
        {
            this.cmbCarrier.OnFirstEnter += this.OncmbCarrierFirstEnter;
           

        }
        private void OnMscmbCountryEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetMcmbCountry(mscmbCountry);
        }
        private void OnCmbCarrierSelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentData != null)
                CurrentData.CarrierName = cmbCarrier.Text.Trim();
        }
        #endregion

        public void BindingData(object data)
        {
            if (data == null) 
            {
                this.bindingSource1.DataSource = typeof(VesselInfo); this.Enabled = false;
            }
            else
            {
                lstIsDirty.Clear();
                this.bindingSource1.DataSource = data;
                this.bindingSource1.ResetBindings(false);

                VesselInfo info = data as VesselInfo;
                if (info != null)
                {
                    Dictionary<string, string> col = new Dictionary<string, string>();
                    col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                    col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
                    List<CountryList> countrys = new List<CountryList>();
                    CountryList cList = new CountryList() { CName = info.RegistrationName == null ? "" : info.RegistrationName, EName = info.RegistrationName == null ? "" : info.RegistrationName, ID = info.Registration == null ? Guid.Empty : info.Registration == Guid.Empty ? Guid.Empty : (Guid)info.Registration };
                    countrys.Add(cList);

                    mscmbCountry.InitSource<CountryList>(countrys, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
                    mscmbCountry.ShowSelectedValue(cList.ID, cList.CName);
                    countryID = cList.ID;
                    if (!iscmbCarrierEntered)
                    {
                        this.cmbCarrier.ShowSelectedValue(info.CarrierID, info.CarrierName);
                    }
                    
                }

                if ((data as VesselInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.Enabled = true;
                    ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                    ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).IsDirty = false;
                }
            }
        }

        void ValidateData(VesselInfo info)
        {
            lstIsDirty.Add(IsDirty);
            if (lstIsDirty.Contains(true))
            {
                info.IsDirty = true;
            }
            else
            {
                info.IsDirty = false;
            }
        }

        #region IDataContentPart 成员
        List<bool> lstIsDirty = new List<bool>(2);
        Guid? countryID = null;
        public bool IsDirty
        {
            get
            {
                if (countryID != EditValue)
                {
                    return true;
                }
                return false;
            }
        }

        public string EditText
        {
            get
            {
                return mscmbCountry.EditText;
            }
        }

        public Guid? EditValue
        {
            get
            {
                Guid? guid;
                if (mscmbCountry.EditValue == null)
                {
                    guid = null;
                }
                else if (mscmbCountry.EditValue == "")
                {
                    guid = Guid.Empty;
                }
                else
                {
                    guid = new Guid(mscmbCountry.EditValue.ToString());
                }

                return guid;
            }
        }

        public object Current
        {
            get
            {
                VesselInfo info = this.bindingSource1.Current as VesselInfo;
                if (info != null)
                {
                    bool _isDirty = info.IsDirty;
                    lstIsDirty.Add(_isDirty);
                    info.Registration = EditValue;
                    info.RegistrationName = EditText;
                    ValidateData(info);
                }
                return info;
            }
        }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        public void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion
    }
}
