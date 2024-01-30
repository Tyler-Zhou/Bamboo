using System;
using System.Collections.Generic;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.UI
{
    /// <summary>
    /// 客户搜索器桥接处理类
    /// </summary>
    public class CustomerFinderBridge:IDisposable
    {

        #region 本地变量

        private ICP.Framework.CommonLibrary.Common.CustomerDescription _customerDescription;
        private bool _isEnglish;
        private CustomerPopupContainerEdit _customerFinderControl;
        private IDataFindClientService _dfService;
        private ICustomerService _customerService;
        private List<CountryList> _countries;
        private string _normalDescription = string.Empty;
        private ConditionsGetHandler _ConditionsGetHandler = null;
        private ICPCommUIHelper _ICPCommUIHelper;


        System.Windows.Forms.Control _descriptionControl;

        #endregion

        #region 构造函数
        public CustomerFinderBridge(
          CustomerPopupContainerEdit customerFinderControl,
          ICP.Framework.CommonLibrary.Common.CustomerDescription customerDescription,
          bool isEnglish)
        {
            _customerFinderControl = customerFinderControl;
            _countries =ServiceClient.GetService<IGeographyService>().GetCountryList(string.Empty, string.Empty, true, 0);
            _dfService = ServiceClient.GetClientService<IDataFindClientService>();
            _customerService = ServiceClient.GetService<ICustomerService>();
            _customerDescription = customerDescription;
            _isEnglish = isEnglish;
            _ICPCommUIHelper = ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
        }


        public CustomerFinderBridge(
            CustomerPopupContainerEdit customerFinderControl,
            List<CountryList> countries,
            IDataFindClientService dfService,
            ICustomerService customerService,
            ICP.Framework.CommonLibrary.Common.CustomerDescription customerDescription,
            ICPCommUIHelper oceanImportUIHelper,
            bool isEnglish)
        {
            _customerFinderControl = customerFinderControl;
            _countries = countries;
            _dfService = dfService;
            _customerService = customerService;
            _customerDescription = customerDescription;
            _isEnglish = isEnglish;
            _ICPCommUIHelper = oceanImportUIHelper;
        }

        public CustomerFinderBridge(
            CustomerPopupContainerEdit customerFinderControl,
            List<CountryList> countries,
            IDataFindClientService dfService,
            ICustomerService customerService,
            ConditionsGetHandler conditionsGetHandler,
            ICP.Framework.CommonLibrary.Common.CustomerDescription customerDescription,
            ICPCommUIHelper oceanImportUIHelper,
            bool isEnglish)
        {
            _customerFinderControl = customerFinderControl;
            _countries = countries;
            _dfService = dfService;
            _customerService = customerService;
            _ConditionsGetHandler = conditionsGetHandler;
            _customerDescription = customerDescription;
            _isEnglish = isEnglish;
            _ICPCommUIHelper = oceanImportUIHelper;
        }

        public CustomerFinderBridge(
            CustomerPopupContainerEdit customerFinderControl,
             List<CountryList> countries,
            IDataFindClientService dfService,
            ICustomerService customerService,
            ICP.Framework.CommonLibrary.Common.CustomerDescription customerDescription,
            System.Windows.Forms.Control descriptionControl,
            ICPCommUIHelper oceanImportUIHelper,
            bool isEnglish)
        {
            _customerFinderControl = customerFinderControl;
            _countries = countries;
            _dfService = dfService;
            _customerService = customerService;
            _customerDescription = customerDescription;
            _descriptionControl = descriptionControl;
            _isEnglish = isEnglish;
            _ICPCommUIHelper = oceanImportUIHelper;
        }

        public CustomerFinderBridge(
             CustomerPopupContainerEdit customerFinderControl,
             List<CountryList> countries,
             IDataFindClientService dfService,
             ICustomerService customerService,
             ICP.Framework.CommonLibrary.Common.CustomerDescription customerDescription,
             System.Windows.Forms.Control descriptionControl,
            ICPCommUIHelper oceanImportUIHelper,
             bool isEnglish,
            string normalDescription)
        {
            _customerFinderControl = customerFinderControl;
            _countries = countries;
            _dfService = dfService;
            _customerService = customerService;
            _customerDescription = customerDescription;
            _descriptionControl = descriptionControl;
            _isEnglish = isEnglish;
            _normalDescription = normalDescription;
            _ICPCommUIHelper = oceanImportUIHelper;
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
                _customerFinderControl.CountryItems.Add(new ImageComboBoxItem(c.EName));
            }
            

            //初始化描述信息

            _customerFinderControl.CustomerDescription = _customerDescription;

            //设置中英文标题


            _customerFinderControl.SetLanguage(_isEnglish);

            //注册客户搜索器
            if (_customerDescription == null)
            {
                _customerDescription = new ICP.Framework.CommonLibrary.Common.CustomerDescription();
            }
            _dfService.Register(_customerFinderControl, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                _ConditionsGetHandler,
                delegate (object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    _customerFinderControl.Tag = id;
                    _customerFinderControl.Update();
                    _ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
                    _customerFinderControl.CustomerDescription = _customerDescription;

                    if (_descriptionControl != null)
                    {
                        _descriptionControl.Text = _customerDescription.ToString(_isEnglish);
                    }
                    ResetDataBinding();
                    _customerFinderControl.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate()
                {
                    _customerFinderControl.Tag = Guid.Empty;
                    _customerFinderControl.Update();
                    _customerDescription = new ICP.Framework.CommonLibrary.Common.CustomerDescription();
                    _customerFinderControl.CustomerDescription = _customerDescription;
                    if (_descriptionControl != null)
                    {
                        _descriptionControl.Text = string.Empty;
                    }
                    ResetDataBinding();
                    _customerFinderControl.Text = string.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            _customerFinderControl.OnOk += delegate
            {
                if (_descriptionControl == null) return;

                string description = _customerFinderControl.CustomerDescription == null ? string.Empty
                                        : _customerFinderControl.CustomerDescription.ToString(_isEnglish);

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
           //_customerFinderControl.Text = string.Empty;
           // _customerFinderControl.Tag = null;
        }

        void ResetDataBinding()
        {
            foreach (System.Windows.Forms.Binding binding in _customerFinderControl.DataBindings)
            {
                if (binding.PropertyName == "Tag")
                {
                    binding.WriteValue();
                }
            }
        }
        public void SetCustomerDescription(ICP.Framework.CommonLibrary.Common.CustomerDescription customerDescription)
        {
            _customerFinderControl.CustomerDescription = _customerDescription = customerDescription;
        }


        #region IDisposable 成员

        public void Dispose()
        {
            _customerFinderControl.OnClear -= _customerFinderControl_OnClear;
            _customerFinderControl = null;
        }

        #endregion
    }
}
