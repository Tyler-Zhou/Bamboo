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
    public partial class LocationSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region serivce
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

        #endregion

        #region init

        public LocationSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.DataReturned = null;
                this.cmbCountryProvince.KeyDown -= this.cmbCountryProvince_KeyDown;
                this.cmbCountryProvince.Enter -= this.OnCmbCountryProvinceEnter;
              
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
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
            this.btnClear.Text = "清空(&L)";
            nbarBase.Caption = "基本信息";
        }

        protected override void OnLoad(EventArgs e)
        {
           
            base.OnLoad(e);
            InitTreeListSource();
            InitControls();
            //btnSearch.PerformClick();
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

        public object GetData()
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
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;
        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            this.ClearControl();
            this.InitControls();
            if (triggerType == FinderTriggerType.KeyEnter)
            {
                txtCodeOrName.Text = searchValue == null ? string.Empty : searchValue.ToString();
            }
        }

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DataReturned != null)
            {
                using (new CursorHelper())
                {
                    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
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
