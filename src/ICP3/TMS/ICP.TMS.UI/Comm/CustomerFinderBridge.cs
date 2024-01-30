using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.TMS.UI
{
    /// <summary>
    /// 客户搜索器桥接处理类
    /// </summary>
    public class CustomerFinderBridge:IDisposable
    {

        #region 本地变量

        private CustomerDescription _customerDescription;
        private bool _isEnglish;
        private CustomerPopupContainerEdit _customerFinderControl;
        private IDataFindClientService _dfService;
        private ICustomerService _customerService;
        private List<CountryList> _countries;
        private string _normalDescription = string.Empty;
        private TMSUIHelper _TMSUIHelper;
        private SearchConditionCollection _SearchConditionCollection;
        private IDisposable customerFinder;

        System.Windows.Forms.Control _descriptionControl;


        private SearchConditionCollection GetSearchConditionCollection()
        {
            return _SearchConditionCollection;
        }

        #endregion

        #region 构造函数


        public CustomerFinderBridge(
            CustomerPopupContainerEdit customerFinderControl,
            List<CountryList> countries,
            IDataFindClientService dfService,
            ICustomerService customerService,
            CustomerDescription customerDescription,
            TMSUIHelper oceanImportUIHelper,
            SearchConditionCollection searchConditionCollection,
            bool isEnglish)
        {
            this._customerFinderControl = customerFinderControl;
            this._countries = countries;
            this._dfService = dfService;
            this._customerService = customerService;
            this._customerDescription = customerDescription;
            this._isEnglish = isEnglish;
            _TMSUIHelper = oceanImportUIHelper;
            _SearchConditionCollection = searchConditionCollection;
        }

        public CustomerFinderBridge(
            CustomerPopupContainerEdit customerFinderControl,
             List<CountryList> countries,
            IDataFindClientService dfService,
            ICustomerService customerService,
            CustomerDescription customerDescription,
            System.Windows.Forms.Control descriptionControl,
            TMSUIHelper oceanImportUIHelper,
            SearchConditionCollection searchConditionCollection,
            bool isEnglish)
        {
            this._customerFinderControl = customerFinderControl;
            this._countries = countries;
            this._dfService = dfService;
            this._customerService = customerService;
            this._customerDescription = customerDescription;
            _descriptionControl = descriptionControl;
            this._isEnglish = isEnglish;
            _TMSUIHelper = oceanImportUIHelper;
            _SearchConditionCollection = searchConditionCollection;
        }

        public CustomerFinderBridge(
             CustomerPopupContainerEdit customerFinderControl,
             List<CountryList> countries,
             IDataFindClientService dfService,
             ICustomerService customerService,
             CustomerDescription customerDescription,
             System.Windows.Forms.Control descriptionControl,
             TMSUIHelper oceanImportUIHelper,
             SearchConditionCollection searchConditionCollection,
             bool isEnglish,
            string normalDescription)
        {
            this._customerFinderControl = customerFinderControl;
            this._countries = countries;
            this._dfService = dfService;
            this._customerService = customerService;
            this._customerDescription = customerDescription;
            this._descriptionControl = descriptionControl;
            this._isEnglish = isEnglish;
            this._normalDescription = normalDescription;
            _TMSUIHelper = oceanImportUIHelper;
            _SearchConditionCollection = searchConditionCollection;
        }

        #endregion

        /// <summary>
        /// 修改时间: 2011-06-21 09:39
        /// 将Text属性赋值放到最后，方便在UI中进一步使用TextChanged事件
        /// </summary>
        public void Init()
        {
            //初始化国家数据

            foreach (CountryList c in _countries)
            {
                _customerFinderControl.CountryItems.Add(new ImageComboBoxItem(this._isEnglish ? c.EName : c.CName));
            }

            //初始化描述信息

            this._customerFinderControl.CustomerDescription = _customerDescription;

            //设置中英文标题


            this._customerFinderControl.SetLanguage(this._isEnglish);

            //注册客户搜索器

           customerFinder= _dfService.Register(
                this._customerFinderControl, 
                CommonFinderConstants.CustoemrFinder,
                SearchFieldConstants.CodeName, 
                SearchFieldConstants.ResultValue,
                GetSearchConditionCollection,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    this._customerFinderControl.Tag = id;
                    this._customerFinderControl.Update();
                    _TMSUIHelper.SetCustomerDesByID(id, this._customerDescription);
                    this._customerFinderControl.CustomerDescription = _customerDescription;

                    if (_descriptionControl != null)
                    {
                        _descriptionControl.Text = _customerDescription.ToString(_isEnglish);
                    }
                    this.ResetDataBinding();
                    this._customerFinderControl.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate()
                {
                    this._customerFinderControl.Tag = Guid.Empty;
                    this._customerFinderControl.Update();
                    this._customerDescription = new CustomerDescription();
                    if (_descriptionControl != null)
                    {
                        _descriptionControl.Text = string.Empty;
                    }
                    this.ResetDataBinding();
                    this._customerFinderControl.Text = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            _customerFinderControl.OnOk += delegate
            {
                if (_descriptionControl == null) return;

                string description = this._customerFinderControl.CustomerDescription == null ? string.Empty
                                        : this._customerFinderControl.CustomerDescription.ToString(this._isEnglish);

                _descriptionControl.Text = string.IsNullOrEmpty(description) ? (_normalDescription ?? string.Empty) : description;
            };

            _customerFinderControl.OnClear += new EventHandler(_customerFinderControl_OnClear);
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
            _customerFinderControl.Text = string.Empty;
            _customerFinderControl.Tag = null;
        }

        void ResetDataBinding()
        {
            foreach (System.Windows.Forms.Binding binding in this._customerFinderControl.DataBindings)
            {
                if (binding.PropertyName == "Tag")
                {
                    binding.WriteValue();
                }
            }
        }

        public void SetCustomerDescription(CustomerDescription customerDescription)
        {
            this._customerFinderControl.CustomerDescription = _customerDescription = customerDescription;
        }


        #region IDisposable 成员

        public void Dispose()
        {
            if (this.customerFinder != null)
            {
                this.customerFinder.Dispose();
                this.customerFinder = null;
            }
            this._countries = null;
            this._customerDescription = null;
            if (this._customerFinderControl != null)
            {
                this._customerFinderControl.OnClear -= this._customerFinderControl_OnClear;
                this._customerFinderControl = null;
            }
            this._customerService = null;
            this._descriptionControl = null;
            this._dfService = null;
            this._SearchConditionCollection = null;
            this._TMSUIHelper = null;
        }

        #endregion
    }
}
