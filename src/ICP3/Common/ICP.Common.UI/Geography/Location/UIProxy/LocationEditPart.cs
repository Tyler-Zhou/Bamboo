using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class LocationEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
    {   
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        public LocationEditPart()
        {
            InitializeComponent();this.Enabled = false;
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataChanged = null;
                
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.cmbCountryProvince.Enter -= this.OnCmbCountryProvinceEnter;
                this._CountryProvinceList = null;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            base.OnLoad(e);
        }

        LocationInfo CurrentData
        {
            get { return bindingSource1.DataSource as LocationInfo; }
            set { bindingSource1.DataSource = value; }
        }
        private bool isFirstTimeEnter = true;
        private void OnCmbCountryProvinceEnter(object sender, EventArgs e)
        {
            if (isFirstTimeEnter)
            {
                _isEntercmbCountryProvince = true;
                _CountryProvinceList = GeographyService.GetCountryProvinceList(string.Empty, string.Empty, null, true, 0);
                cmbCountryProvince.AllowMultSelect = false;
                cmbCountryProvince.RootValue = Guid.Empty;
                cmbCountryProvince.ParentMember = "ParentID";
                cmbCountryProvince.ValueMember = "ID";
                cmbCountryProvince.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
                cmbCountryProvince.DataSource = _CountryProvinceList;
                isFirstTimeEnter = false;
            }
        }

        private List<CountryProvinceList> _CountryProvinceList = null;
        private bool _isEntercmbCountryProvince = false;
        private void InitControls()
        {
            this.cmbCountryProvince.Enter += this.OnCmbCountryProvinceEnter;
            var current = bindingSource1.DataSource as LocationInfo;
            if (current != null)
            {
                current.BeginEdit();
            }
        }

        private void SetCnText()
        {
            groupBox1.Text = "类型";
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labEName.Text = "英文名";
            labGeography.Text = "地区";
            labType.Text = "类型";
            chkIsAir.Text = "空运";
            chkIsOcean.Text = "海运";
            chkIsOther.Text = "其它";         
        }

        public void BindingData(object data)
        {
            if (data == null)
            {
                this.bindingSource1.DataSource = typeof(LocationInfo);
                this.Enabled = false;
            }
            else
            {
                this.bindingSource1.DataSource = data;
                if ((data as LocationInfo).IsValid == false)
                {
                    this.Enabled = false;
                }
                else
                {
                    this.Enabled = true;
                    ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();

                    //this.EndEdit();
                }
            }

            var current = data as LocationInfo;
            if (current != null)
            {
                if (_isEntercmbCountryProvince)
                {
                    if (current.ID == Guid.Empty && _CountryProvinceList != null)
                    {
                        cmbCountryProvince.EditValue = string.Empty;
                    }

                    cmbCountryProvince.SelectedValue = current.ProvinceID ?? current.CountryID;
                }
                else
                {
                    if (current.ProvinceID != null && current.ProvinceID != Guid.Empty)
                    {
                        cmbCountryProvince.Text = current.ProvinceName;
                    }
                    else if (current.CountryID != null && current.CountryID != Guid.Empty)
                    {
                        cmbCountryProvince.Text = current.CountryName;
                    }
                    else
                    {
                        cmbCountryProvince.Text = string.Empty;
                    }
                }

                current.IsDirty = false;
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
            bindingSource1.EndEdit();
            LocationInfo currentData = bindingSource1.DataSource as LocationInfo;
            if (_isEntercmbCountryProvince)
            {
                //处理界面上不能直接绑定的值
                Guid? countryId = (Guid?)cmbCountryProvince.GetSelectedValues("ParentID");
                Guid? provinceId = null;
                if (countryId != null)
                {
                    provinceId = (Guid?)cmbCountryProvince.SelectedValue;
                }
                else
                {
                    countryId = (Guid?)cmbCountryProvince.SelectedValue;
                }

                if (currentData != null)
                {
                    currentData.CountryID = countryId.HasValue ? countryId.Value : Guid.Empty;
                    currentData.ProvinceID = provinceId;
                    currentData.CountryProvinceName = cmbCountryProvince.Text.Trim();
                    currentData.CountryName = cmbCountryProvince.Text.Trim();
                }
            }

            if (currentData != null)
            {
                currentData.SetError("CountryID", "");
                if (currentData.CountryID == Guid.Empty)
                {
                    currentData.SetError("CountryID", LocalData.IsEnglish?"Country must input": "地区必须填写.");
                }

                currentData.SetError("IsOcean", "");
                if (currentData.IsOcean == false
                    && currentData.IsAir == false
                    && currentData.IsOther == false)
                {
                    currentData.SetError("IsOcean", LocalData.IsEnglish?"Type must input":"类型必须填写.");
                }
            }

            this.Validate();           
        }

        #endregion
    }
}
