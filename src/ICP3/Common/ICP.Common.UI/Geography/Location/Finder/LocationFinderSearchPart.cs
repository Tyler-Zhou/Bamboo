using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Geography.Location
{
    [ToolboxItem(false)]
    public partial class LocationFinderSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region serivce
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        #endregion

        #region init

        public LocationFinderSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                this.cmbCountryProvince.KeyDown -= this.cmbCountryProvince_KeyDown;
                this.cmbCountryProvince.DataSource = null;
                this.cmbCountryProvince.Enter -= this.OnCmbCountryProvinceEnter;
                this.bsGeography.DataSource = null;
                this.bsGeography.Dispose();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            //colCName.Caption = "名称";
            //colEName.Caption = "名称";
            labCodeOrName.Text = "代码/名称";
            labGeography.Text = "地区";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            //labName.Text = "名称";
            labIsAir.Text = "空运";
            labIsOcean.Text = "海运";
            labIsOther.Text = "其它";
            lwchkIsAir.CheckedText = "是";
            lwchkIsAir.UnCheckedText = "否";
            lwchkIsAir.NULLText = "不确定";

            lwchkIsOcean.CheckedText = "是";
            lwchkIsOcean.UnCheckedText = "否";
            lwchkIsOcean.NULLText = "不确定";

            lwchkIsOther.CheckedText = "是";
            lwchkIsOther.UnCheckedText = "否";
            lwchkIsOther.NULLText = "不确定";

            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";
            this.btnSearch.Text = "查询(&R)";
            this.btnClean.Text = "清空(&L)";
            nbarBase.Caption = "基本信息";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitTreeListSource();
                InitControls();
             
            }
        }
        private bool isFirstTimeEnter = true;
        private void OnCmbCountryProvinceEnter(object sender, EventArgs e)
        {
            if (isFirstTimeEnter)
            {
                //国家，省份控件初始化
                List<CountryProvinceList> datas = GeographyService.GetCountryProvinceList(
                  string.Empty,
                  string.Empty,
                  null,
                  true,
                  0);

                cmbCountryProvince.AllowMultSelect = false;
                cmbCountryProvince.RootValue = Guid.Empty;
                cmbCountryProvince.ParentMember = "ParentID";
                cmbCountryProvince.ValueMember = "ID";
                cmbCountryProvince.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
                cmbCountryProvince.DataSource = datas;
                isFirstTimeEnter = false;
            }
        }

        private void InitTreeListSource()
        {
            this.cmbCountryProvince.Enter += this.OnCmbCountryProvinceEnter;
        }

        protected virtual void InitControls() { }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            Guid? countryId = (Guid?)cmbCountryProvince.GetSelectedValues("ParentID");
            Guid? provinceId = null;
            if (cmbCountryProvince.SelectedValue != null)
            {
                if (countryId != null)
                {
                    provinceId = (Guid)cmbCountryProvince.SelectedValue;
                }
                else
                {
                    countryId = (Guid)cmbCountryProvince.SelectedValue;
                }
            }

            List<LocationList> list = GeographyService.GetLocationList(txtCodeOrName.Text.Trim(),
                                                                       countryId,
                                                                       provinceId,
                                                                       lwchkIsOcean.Checked,
                                                                       lwchkIsAir.Checked,
                                                                       lwchkIsOther.Checked,
                                                                       lwchkIsValid.Checked,
                                                                       int.Parse(numMax.Value.ToString()));
            return list;
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
            this.btnClean.PerformClick();

            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "NAME")
                {
                    txtCodeOrName.Text = item.Value.ToString();
                }
                else if (item.Key.ToUpper() == "ISOCEAN")
                {
                    lwchkIsOcean.Enabled = false;
                    lwchkIsOcean.Checked = true;
                }
                else if (item.Key.ToUpper() == "ISAIR")
                {
                    lwchkIsAir.Enabled = false;
                    lwchkIsAir.Checked = true;
                }
                else if (item.Key.ToUpper() == "ISOTHER")
                {
                    lwchkIsOther.Enabled = false;
                    lwchkIsOther.Checked = true;
                }
            }
            this.btnSearch.PerformClick();
        }

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
            {
                using (new CursorHelper())
                {
                    OnSearched(this, GetData());
                }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            this.ClearControl();
        }

        private void ClearControl()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            cmbCountryProvince.Text = string.Empty;
            cmbCountryProvince.SelectedValue = null;
            txtCodeOrName.Text = string.Empty;
            //txtName.Text = string.Empty;
            lwchkIsValid.Checked = true;

            if (this.panelLwChk.Visible)
            {
                lwchkIsOcean.Checked = null;
                lwchkIsAir.Checked = null;
                lwchkIsOther.Checked = null;
            }
            else
            {
                lwchkIsValid.Enabled = false;
            }
            txtCodeOrName.Focus();
        }
        #endregion

        private void cmbCountryProvince_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                cmbCountryProvince.Text =string.Empty;
                cmbCountryProvince.EditValue = null;
                cmbCountryProvince.SelectedValue=null;

            }
            else if (e.KeyCode == Keys.Back && this.cmbCountryProvince.Text.Trim().Length == 1)
            {
                cmbCountryProvince.SelectedValue = null;
            }
        }
    }
}
