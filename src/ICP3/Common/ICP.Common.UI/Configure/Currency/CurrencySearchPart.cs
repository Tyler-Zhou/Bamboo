using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.Configure.Currency
{

    [System.ComponentModel.ToolboxItem(false)]
    public partial class CurrencySearchPart : DevExpress.XtraEditors.XtraUserControl, ISearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
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

        public CurrencySearchPart()
        {
            InitializeComponent();

            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
            this.Disposed += delegate
            {
                this.DataReturned = null;
                CommonUtility.RemoveSearchPartKeyEnterToSearch(new List<Control> { this.txtName, txtCode }, KeyEventHandle);
                this.cmbCountry.QueryPopUp -= this.cmbCountry_QueryPopUp;
                cmbCountry.SelectedIndexChanged -= this.OncmbCountrySelectedIndexChanged;
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

            this.cmbCountry.QueryPopUp += cmbCountry_QueryPopUp;

            CountryList emptyCountry = new CountryList();
            emptyCountry.CName = emptyCountry.EName = LocalData.IsEnglish ? "" : "";
            emptyCountry.ID = Guid.Empty;
            cmbCountry.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                            (emptyCountry.CName, emptyCountry.ID));
            cmbCountry.SelectedIndex = 0;

            //if (DataReturned != null)
            //    DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));

            CommonUtility.SearchPartKeyEnterToSearch(new List<Control> { this.txtName,txtCode }, this.btnSearch,KeyEventHandle);
        }
        private void KeyEventHandle(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        Guid? _countryId = null;
        void cmbCountry_QueryPopUp(object sender, CancelEventArgs e)
        {
            this.cmbCountry.QueryPopUp -= new CancelEventHandler(cmbCountry_QueryPopUp);

             List<CountryList> list = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
  
            foreach (var item in list)
            {
                cmbCountry.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                                                (LocalData.IsEnglish?item.EName:item.CName,item.ID));
            }
            cmbCountry.SelectedIndexChanged += this.OncmbCountrySelectedIndexChanged;

        }
        private void OncmbCountrySelectedIndexChanged(object sender, EventArgs e)
        {
            _countryId = new Guid(cmbCountry.EditValue.ToString());
        }

        private void SetCnText()
        {
            labCode.Text = "代码";
            labIsValid.Text = "有效";
            labMax.Text = "最大行数";
            labName.Text = "名称";
            labCountry.Text = "国家";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&R)";
            nbarBase.Caption = "基本信息";
        }

        #endregion

        #region ISearchPart 成员

        public object GetData()
        {
            List<CurrencyList> data = ConfigureService.GetCurrencyList(txtCode.Text.Trim(),
                                                                           txtName.Text.Trim(),
                                                                            _countryId,
                                                                            lwchkIsValid.Checked,
                                                                           int.Parse(numMax.Value.ToString()));
            return data;
        }
        public event EventHandler<ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>> DataReturned;

        #endregion

        #region btn
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (DataReturned != null)
                DataReturned(this, new ICP.Framework.CommonLibrary.Common.CommonEventArgs<object>(GetData()));
        }

        private void btnClear_Click(object sender, EventArgs e)
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

            _countryId = null;
            lwchkIsValid.Checked = true;
            txtCode.Focus();
        }

        #endregion

        #region ISearchPart 成员

        public virtual void InitialValues(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType)
        {
            btnClear.PerformClick();
        }

        #endregion
    }
}

