using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI.Geography.CountryProvince
{
    public partial class CountryProvinceSearchPart : DevExpress.XtraEditors.XtraUserControl, ICP.Framework.ClientComponents.UIManagement.ISearchPart
    {
        #region service
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

        public CountryProvinceSearchPart()
        {
            InitializeComponent();
            this.numMax.Value = 100;
            if (LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate {
                this.DataReturned = null;
                this.lookUpCountry.QueryCloseUp -= this.lookUpCountry_QueryPopUp;
                this.lookUpCountry.Properties.DataSource = null;
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

            lookUpCountry.QueryPopUp += new CancelEventHandler(lookUpCountry_QueryPopUp);
            if (LocalData.IsEnglish)
            {
                lookUpCountry.Properties.DisplayMember = "EName";
                lookUpCountry.Properties.Columns["EName"].Visible = true;
            }
            else
            {
                lookUpCountry.Properties.Columns["CName"].Visible = true;
                lookUpCountry.Properties.DisplayMember = "CName";
            }

            
        }

        private void SetCnText()
        {
            labCode.Text = "代码";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labName.Text = "名称";
            labCountry.Text = "国家";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.UnCheckedText = "无效";
            lwchkIsValid.NULLText = "全部";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";
        }

        void lookUpCountry_QueryPopUp(object sender, CancelEventArgs e)
        {
            lookUpCountry.QueryPopUp -= new CancelEventHandler(lookUpCountry_QueryPopUp);
            List<CountryList> list = GeographyService.GetCountryList(string.Empty, string.Empty, null, 0);
            CountryList emptyCountry = new CountryList();
            emptyCountry.CName = emptyCountry.EName = string.Empty;
            emptyCountry.ID = Guid.Empty;
            list.Insert(0, emptyCountry);

            if (LocalData.IsEnglish)
            {
                bsCountry.DataSource = list.OrderBy(delegate(CountryList c){ return c.CName;});
            }
            else
            {
                bsCountry.DataSource = list.OrderBy(delegate(CountryList c) { return c.EName; });
            }
        }

        #endregion

        #region ISearchPart 成员

        public object GetData()
        {
            Guid? countryId = null;
            if (lookUpCountry.EditValue != null)
            {
                countryId = new Guid(lookUpCountry.EditValue.ToString());
            }

            List<CountryProvinceList> list = GeographyService.GetCountryProvinceList(txtCode.Text.Trim(),
                                                                                     txtName.Text.Trim(),
                                                                                     countryId,
                                                                                     lwchkIsValid.Checked,
                                                                                     int.Parse(numMax.Value.ToString()));
            return list;
        }

        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;
        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            btnClear.PerformClick();

            if (triggerType == FinderTriggerType.KeyEnter)
            {
                if (property.Contains(SearchFieldConstants.Code))
                    txtCode.Text = searchValue == null ? string.Empty : searchValue.ToString();
                else
                    txtName.Text = searchValue == null ? string.Empty : searchValue.ToString();
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
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.LookUpEdit)
                {
                    continue;
                }
                
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                {
                    item.Text = string.Empty;
                }
            }
            lookUpCountry.EditValue = Guid.Empty;
            txtCode.Focus();
        }

        #endregion

    }
}
