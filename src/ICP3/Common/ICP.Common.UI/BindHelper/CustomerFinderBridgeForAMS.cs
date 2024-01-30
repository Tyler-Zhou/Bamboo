using System;
using System.Collections.Generic;
using ICP.Common.UI.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.Common.UI.BindHelper
{
    public class CustomerFinderBridgeFotAMS : IDisposable
    {

        #region 本地变量

        private CustomerDescriptionForAMS _customerDescriptionForAMS;
        private bool _isEnglish;
        private CustomerPopupContainerForAMSEdit _customerFinderControlForAMS;
        private IDataFindClientService _dfService;
        private ICustomerService _customerService;
        private List<CountryList> _countries;
        private string _normalDescription = string.Empty;
        private ICPCommUIHelper _ICPCommUIHelper;

        public IGeographyService _geographyService;

        System.Windows.Forms.Control _descriptionControl;

        #endregion

        #region 构造函数


        public CustomerFinderBridgeFotAMS(
            CustomerPopupContainerForAMSEdit customerFinderControlForAMS,
            List<CountryList> countries,
            IDataFindClientService dfService,
            ICustomerService customerService,
            CustomerDescriptionForAMS customerDescriptionForAMS,
            ICPCommUIHelper oceanImportUIHelper,
            bool isEnglish,
            IGeographyService geographyService)
        {
            this._customerFinderControlForAMS = customerFinderControlForAMS;
            this._countries = countries;
            this._dfService = dfService;
            this._customerService = customerService;
            _customerDescriptionForAMS = customerDescriptionForAMS;
            this._isEnglish = isEnglish;
            _ICPCommUIHelper = oceanImportUIHelper;
            _geographyService = geographyService;
        }

        public CustomerFinderBridgeFotAMS(
            CustomerPopupContainerForAMSEdit customerFinderControlForAMS,
             List<CountryList> countries,
            IDataFindClientService dfService,
            ICustomerService customerService,
            CustomerDescriptionForAMS customerDescriptionForAMS,
            System.Windows.Forms.Control descriptionControl,
            ICPCommUIHelper oceanImportUIHelper,
            bool isEnglish,
            IGeographyService geographyService)
        {
            this._customerFinderControlForAMS = customerFinderControlForAMS;
            this._countries = countries;
            this._dfService = dfService;
            this._customerService = customerService;
            _customerDescriptionForAMS = customerDescriptionForAMS;
            _descriptionControl = descriptionControl;
            this._isEnglish = isEnglish;
            _ICPCommUIHelper = oceanImportUIHelper;
            _geographyService = geographyService;
        }

        public CustomerFinderBridgeFotAMS(
             CustomerPopupContainerForAMSEdit customerFinderControlForAMS,
             List<CountryList> countries,
             IDataFindClientService dfService,
             ICustomerService customerService,
             CustomerDescriptionForAMS customerDescriptionForAMS,
             System.Windows.Forms.Control descriptionControl,
            ICPCommUIHelper oceanImportUIHelper,
             bool isEnglish,
            string normalDescription,
            IGeographyService geographyService)
        {
            this._customerFinderControlForAMS = customerFinderControlForAMS;
            this._countries = countries;
            this._dfService = dfService;
            this._customerService = customerService;
            _customerDescriptionForAMS = customerDescriptionForAMS;
            this._descriptionControl = descriptionControl;
            this._isEnglish = isEnglish;
            this._normalDescription = normalDescription;
            _ICPCommUIHelper = oceanImportUIHelper;
            _geographyService = geographyService;
        }

        #endregion
        private IDisposable amsFinder;
        /// <summary>
        /// 修改时间: 2011-06-21 09:39
        /// 将Text属性赋值放到最后，方便在UI中进一步使用TextChanged事件
        /// </summary>
        public void Init()
        {
            //初始化国家数据
            if (_customerFinderControlForAMS.CountryItems.Count < 1)
                foreach (CountryList c in _countries)
                {

                    _customerFinderControlForAMS.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }

            //初始化描述信息

            this._customerFinderControlForAMS.CustomerDescriptionForAMS = _customerDescriptionForAMS;

            this._customerFinderControlForAMS.geographyService = _geographyService;

            //设置中英文标题


            this._customerFinderControlForAMS.SetLanguage(this._isEnglish);

            //注册客户搜索器
            if (this._customerDescriptionForAMS == null)
            {
                this._customerDescriptionForAMS = new CustomerDescriptionForAMS();
            }
            amsFinder = _dfService.Register(this._customerFinderControlForAMS, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid id = new Guid(resultData[0].ToString());
                      this._customerFinderControlForAMS.Tag = id;
                      this._customerFinderControlForAMS.Update();
                      _ICPCommUIHelper.SetCustomerDesByIDForAMS(id, this._customerDescriptionForAMS);
                      this._customerFinderControlForAMS.CustomerDescriptionForAMS = _customerDescriptionForAMS;

                      if (_descriptionControl != null)
                      {
                          _descriptionControl.Text = _customerDescriptionForAMS.ToString(_isEnglish);
                      }
                      this.ResetDataBinding();
                      this._customerFinderControlForAMS.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                  },
                  delegate()
                  {
                      this._customerFinderControlForAMS.Tag = Guid.Empty;
                      this._customerFinderControlForAMS.Update();
                      this._customerDescriptionForAMS = new CustomerDescriptionForAMS();
                      this._customerFinderControlForAMS.CustomerDescriptionForAMS = this._customerDescriptionForAMS;
                      if (_descriptionControl != null)
                      {
                          _descriptionControl.Text = string.Empty;
                      }
                      this.ResetDataBinding();
                      this._customerFinderControlForAMS.Text = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            _customerFinderControlForAMS.OnOk += this.OnOk;


            _customerFinderControlForAMS.OnClear += new EventHandler(_customerFinderControl_OnClear);
        }
        private void OnOk(object sender, EventArgs e)
        {
            if (_descriptionControl == null) return;

            string description = this._customerFinderControlForAMS.CustomerDescriptionForAMS == null ? string.Empty
                                    : this._customerFinderControlForAMS.CustomerDescriptionForAMS.ToString(this._isEnglish);

            _descriptionControl.Text = string.IsNullOrEmpty(description) ? (_normalDescription ?? string.Empty) : description;
        }

        /// <summary>
        /// 允许删除
        /// 作者: Royal
        /// 创建时间: 2011-05-29 13:06
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _customerFinderControl_OnClear(object sender, EventArgs e)
        {
            //_customerFinderControl.Text = string.Empty;
            // _customerFinderControl.Tag = null;
        }

        void ResetDataBinding()
        {
            foreach (System.Windows.Forms.Binding binding in this._customerFinderControlForAMS.DataBindings)
            {
                if (binding.PropertyName == "Tag")
                {
                    binding.WriteValue();
                }
            }
        }
        public void SetCustomerDescription(CustomerDescriptionForAMS customerDescriptionForAMS)
        {
            this._customerFinderControlForAMS.CustomerDescriptionForAMS = _customerDescriptionForAMS = customerDescriptionForAMS;
        }


        #region IDisposable 成员

        public void Dispose()
        {
            if (this.amsFinder != null)
            {
                this.amsFinder.Dispose();
                this.amsFinder = null;
            }
            if (this._customerFinderControlForAMS != null)
            {
                this._customerFinderControlForAMS.OnClear -= this._customerFinderControl_OnClear;
                this._customerFinderControlForAMS.OnOk -= this.OnOk;
                this._customerFinderControlForAMS = null;


            }
            this._countries = null;
            this._customerDescriptionForAMS = null;
            this._customerService = null;
            this._descriptionControl = null;
            this._dfService = null;
            this._geographyService = null;
            this._ICPCommUIHelper = null;

        }

        #endregion
    }
}
