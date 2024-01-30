using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Business.Common.UI
{
    /// <summary>
    /// 业务联系人客户搜索器桥接处理类
    /// </summary>
    public class CustomerContactFinderBridge : IDisposable
    {

        #region 本地变量

        private CustomerDescription _customerDescription;
        private List<CustomerCarrierObjects> contactList;
        private bool _isEnglish;
        private BusinessContactPopupContainerEdit _customerFinderControl;
        private IDataFindClientService _dfService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        private ICustomerService _customerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        private IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        private IFCMCommonService FCMCommonService
        {
            get
            { 
            return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        private List<CountryList> _countries;
        private string _normalDescription = string.Empty;
        private ICPCommUIHelper _ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public void SetSearchConfig(string finderName,string codeName,string[] resultValue )
        {
            this.finderName = finderName;
            this.codeName = codeName;
            this.resultValue = resultValue;
        }
        private string finderName=CommonFinderConstants.CustoemrFinder;
        private string codeName=SearchFieldConstants.CodeName;
        private string[] resultValue=SearchFieldConstants.ResultValue;
        Control _descriptionControl;
        private ContactStage contactStage;
        private Guid customerID;
    
        #endregion

        #region 构造函数
        public CustomerContactFinderBridge(
          BusinessContactPopupContainerEdit customerFinderControl,
          CustomerDescription customerDescription,
          List<CustomerCarrierObjects> contactList,
         ContactStage contactStage,

         Guid customerID,
          bool isEnglish,
            ContactType contactType)
        {
            _customerFinderControl = customerFinderControl;
            _countries = ServiceClient.GetService<IGeographyService>().GetCountryList(string.Empty, string.Empty, true, 0);
            _customerDescription = customerDescription;
            _isEnglish = isEnglish;
            this.customerID = customerID;
            this.contactStage = contactStage;
            this.contactType = contactType;
            this.contactList = contactList;
        }
        private ContactType contactType;


        private List<CountryList> GetCountryList(List<CountryList> countries)
        {
            if (countries != null && countries.Count > 0)
            {
                return countries;
            }
            return GeographyService.GetCountryList(null, null, true, 0);
        }
        #endregion
        IDisposable finder;
        /// <summary>
        /// 修改时间: 2011-06-21 09:39
        /// 将Text属性赋值放到最后，方便在UI中进一步使用TextChanged事件
        /// </summary>
        public void Init()
        {
            //初始化国家数据

            _customerFinderControl.SetCountryList(_countries);

            //初始化描述信息

            _customerFinderControl.CustomerDescription = _customerDescription;

            //设置中英文标题


            _customerFinderControl.SetLanguage(_isEnglish);

            //注册客户搜索器
            if (_customerDescription == null)
            {
                _customerDescription = new CustomerDescription();
            }
            _customerFinderControl.SetContactStage(contactStage);
            _customerFinderControl.ContactType = contactType;
            _customerFinderControl.SetCustomerID(customerID);
            _customerFinderControl.ContactList = contactList;
         

           _customerFinderControl.OnOk += OnOk;
            

            _customerFinderControl.OnClear += new EventHandler(_customerFinderControl_OnClear);
        }
        private void OnOk(object sender, EventArgs e)
        {
            if (_descriptionControl == null) return;

            string description = _customerFinderControl.CustomerDescription == null ? string.Empty
                                    : _customerFinderControl.CustomerDescription.ToString(_isEnglish);

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
            foreach (Binding binding in _customerFinderControl.DataBindings)
            {
                if (binding.PropertyName == "Tag")
                {
                    binding.WriteValue();
                }
            }
        }
        public void SetCustomerDescription(CustomerDescription customerDescription)
        {
            _customerFinderControl.CustomerDescription = _customerDescription = customerDescription;
        }


        #region IDisposable 成员

        public void Dispose()
        {
            _customerFinderControl.OnClear -= _customerFinderControl_OnClear;
            _customerFinderControl.OnOk -= OnOk;
            _customerFinderControl = null;
            if (finder != null)
            {
                finder.Dispose();
                finder = null;
            }
        }

        #endregion
    }
}
