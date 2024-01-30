using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI.Common;
namespace ICP.Common.UI.Geography.CountryProvince
{
    public partial class CountryProvinceEditPart : BaseControl, ICP.Framework.ClientComponents.UIManagement.IDataContentPart
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

        public CountryProvinceEditPart()
        {
            InitializeComponent();this.Enabled = false;
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.lookUpCountry.Properties.DataSource = null;
                this.dxErrorProvider1.DataSource = null;
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
                this.bsCountry.DataSource = null;
                this.bsCountry.Dispose();
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }



        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }

        CountryProvinceInfo CurrentData
        {
            get { return bindingSource1.DataSource as CountryProvinceInfo; }
            set { bindingSource1.DataSource = value; }
        }

        private void SetCnText()
        {
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labCountry.Text = "国家";
            labEName.Text = "英文名";
        }

        private void InitControls()
        {
            if (CurrentData == null) return;
            if (CurrentData.Type == CountryProvinceType.Province)
            {
                List<CountryList>  countrys = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                
                CountryInfo ci = new CountryInfo
                {
                    ID = Guid.Empty,
                    CName = "",
                    EName = ""
                };
                countrys.Insert(0, ci);

                if (LocalData.IsEnglish)
                {
                    this.lookUpCountry.Properties.DisplayMember = "EName";
                    this.lookUpCountry.Properties.Columns["EName"].Visible = true;
                }
                else
                {
                    this.lookUpCountry.Properties.DisplayMember = "CName";
                    this.lookUpCountry.Properties.Columns["CName"].Visible = true;
                }

                bsCountry.DataSource = countrys;
                
                if (CurrentData.ID == Guid.Empty)
                {
                    lookUpCountry.EditValue = Guid.Empty;
                }
                
                this.txtCode.Properties.MaxLength = 20;


                //if (Utility.GuidIsNullOrEmpty(CurrentData.ParentID))
                //{
                //    CurrentData.ParentID = Guid
                //    CurrentData.ParentName = LocalData.IsEnglish ? countrys[0].EName : countrys[0].CName;
                //}

                labCountry.Visible = true;
                lookUpCountry.Visible = true;
            }
            else
            {
                this.txtCode.Properties.MaxLength = 2;
                labCountry.Visible = false;
                lookUpCountry.Visible = false;
            }
        }

        public void BindingData(object data)
        {
            if (data == null) { this.bindingSource1.DataSource = typeof(CountryProvinceInfo); this.Enabled = false; }
            else
            {
                this.bindingSource1.DataSource = data;
                if ((data as CountryProvinceInfo).IsValid == false) { this.Enabled = false; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit(); }
                else
                {
                    this.Enabled = true; ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
                }
            }
            InitControls();
        }

        #region IDataContentPart 成员
        public object Current { get { return this.bindingSource1.Current; } }
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
