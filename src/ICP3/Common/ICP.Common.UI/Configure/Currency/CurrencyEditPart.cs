using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;

namespace ICP.Common.UI.Configure.Currency
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CurrencyEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {
        #region 服务注入

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion

        #region

        public CurrencyEditPart()
        {
            InitializeComponent();
            this.Enabled = false;
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.DataChanged = null;
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this._countryList = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        private void SetCnText()
        {
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labCountry.Text = "国家";
            labEName.Text = "英文名";
        }

        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            base.OnLoad(e);
        }

        List<CountryList> _countryList = null;
        CurrencyInfo CurrentData
        {
            get { return bindingSource1.DataSource as CurrencyInfo; }
            set { bindingSource1.DataSource = value; }
        }


        private void InitControls()
        {
            _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            cmbCountry.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                               ("", Guid.Empty));
            foreach (var item in _countryList)
            {
                cmbCountry.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                (LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
        }

        #endregion

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(CurrencyInfo); this.Enabled = false; }

            else
            {
                this.bindingSource1.DataSource = data;
                if ((data as CurrencyInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }

            if (CurrentData != null && CurrentData.CountryID == Guid.Empty)
            {
                cmbCountry.Text = string.Empty;
                cmbCountry.SelectedIndex = 0;
            }

        }

        #region IDataContentPart 成员
        public bool AutoWidth { get; set; }
        public object Current { get { return this.bindingSource1.Current; } }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataChanged;
        public object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }
        public void EndEdit()
        {
            CurrencyInfo currency = (CurrencyInfo)this.Current;
            if (currency != null)
            {
                currency.CountryName = cmbCountry.Text.Trim();
            }

            this.Validate();
            bindingSource1.EndEdit();
        }
        #endregion
    }
}
