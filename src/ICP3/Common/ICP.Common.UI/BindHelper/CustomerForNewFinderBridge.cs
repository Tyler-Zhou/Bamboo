#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/15 星期五 13:56:24
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion


namespace ICP.Common.UI
{
    using System;
    using System.Collections.Generic;
    using ServiceInterface;
    using ServiceInterface.DataObjects;
    using Controls;
    using DevExpress.XtraEditors.Controls;
    using Framework.CommonLibrary.Client;
    using System.Windows.Forms;

    /// <summary>
    /// 客户搜索器桥接处理类
    /// </summary>
    public class CustomerForNewFinderBridge : IDisposable
    {

        #region 本地变量

        private bool _isEnglish;
        private string _normalDescription = string.Empty;
        private CustomerDescriptionForNew _DataSource;
        private CustomerForNewPopupContainerEdit _PopupContainerEdit;
        private IDataFindClientService _dfService;
        private List<CountryList> _countries;
        private ICPCommUIHelper _ICPCommUIHelper;
        Control _descriptionControl;



        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerFinderControl">客户选择资料控件</param>
        /// <param name="datasource">客户数据源</param>
        /// <param name="isEnglish">是否英文</param>
        public CustomerForNewFinderBridge(
          CustomerForNewPopupContainerEdit customerFinderControl,
          CustomerDescriptionForNew datasource,
          bool isEnglish)
        {
            _PopupContainerEdit = customerFinderControl;
            _countries = ServiceClient.GetService<IGeographyService>().GetCountryList(string.Empty, string.Empty, true, 0);
            _dfService = ServiceClient.GetClientService<IDataFindClientService>();
            _DataSource = datasource;
            _isEnglish = isEnglish;
            _ICPCommUIHelper = ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerFinderControl">客户选择资料控件</param>
        /// <param name="countries">国家数据源</param>
        /// <param name="dfService">搜索器客户端服务</param>
        /// <param name="datasource">客户数据源</param>
        /// <param name="oceanImportUIHelper">ICP通用UI帮助类</param>
        /// <param name="isEnglish">是否英文</param>
        public CustomerForNewFinderBridge(
            CustomerForNewPopupContainerEdit customerFinderControl,
            List<CountryList> countries,
            IDataFindClientService dfService,
            CustomerDescriptionForNew datasource,
            ICPCommUIHelper oceanImportUIHelper,
            bool isEnglish)
        {
            _PopupContainerEdit = customerFinderControl;
            _countries = countries;
            _dfService = dfService;
            _DataSource = datasource;
            _isEnglish = isEnglish;
            _ICPCommUIHelper = oceanImportUIHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerFinderControl">客户选择资料控件</param>
        /// <param name="countries">国家数据源</param>
        /// <param name="dfService">搜索器客户端服务</param>
        /// <param name="datasource">客户数据源</param>
        /// <param name="descriptionControl">显示客户描述的控件</param>
        /// <param name="oceanImportUIHelper">ICP通用UI帮助类</param>
        /// <param name="isEnglish">是否英文</param>
        public CustomerForNewFinderBridge(
            CustomerForNewPopupContainerEdit customerFinderControl,
             List<CountryList> countries,
            IDataFindClientService dfService,
            CustomerDescriptionForNew datasource,
            Control descriptionControl,
            ICPCommUIHelper oceanImportUIHelper,
            bool isEnglish)
        {
            _PopupContainerEdit = customerFinderControl;
            _countries = countries;
            _dfService = dfService;
            _DataSource = datasource;
            _descriptionControl = descriptionControl;
            _isEnglish = isEnglish;
            _ICPCommUIHelper = oceanImportUIHelper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerFinderControl">客户选择资料控件</param>
        /// <param name="countries">国家数据源</param>
        /// <param name="dfService">搜索器客户端服务</param>
        /// <param name="datasource">客户数据源</param>
        /// <param name="descriptionControl">显示客户描述的控件</param>
        /// <param name="oceanImportUIHelper">ICP通用UI帮助类</param>
        /// <param name="isEnglish">是否英文</param>
        /// <param name="normalDescription">默认客户描述</param>
        public CustomerForNewFinderBridge(
             CustomerForNewPopupContainerEdit customerFinderControl,
             List<CountryList> countries,
             IDataFindClientService dfService,
             CustomerDescriptionForNew datasource,
             Control descriptionControl,
            ICPCommUIHelper oceanImportUIHelper,
             bool isEnglish,
            string normalDescription)
        {
            _PopupContainerEdit = customerFinderControl;
            _countries = countries;
            _dfService = dfService;
            _DataSource = datasource;
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
                _PopupContainerEdit.CountryItems.Add(new ImageComboBoxItem(c.EName, c.Code));
            }
            //初始化描述信息
            if (_DataSource == null)
            {
                _DataSource = new CustomerDescriptionForNew();
            }
            _PopupContainerEdit.DataSource = _DataSource;
            //设置中英文标题
            _PopupContainerEdit.SetLanguage(_isEnglish);

            //注册客户搜索器
            _dfService.Register(_PopupContainerEdit, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    Guid id = new Guid(resultData[0].ToString());
                    _PopupContainerEdit.Tag = id;
                    _PopupContainerEdit.Update();
                    _ICPCommUIHelper.SetCustomerDesByID(id, _DataSource);
                    _PopupContainerEdit.DataSource = _DataSource;

                    if (_descriptionControl != null)
                    {
                        _descriptionControl.Text = _DataSource.ToString(_isEnglish);
                    }
                    ResetDataBinding();
                    _PopupContainerEdit.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                },
                delegate()
                {
                    _PopupContainerEdit.Tag = Guid.Empty;
                    _PopupContainerEdit.Update();
                    _DataSource = new CustomerDescriptionForNew();
                    _PopupContainerEdit.DataSource = _DataSource;
                    if (_descriptionControl != null)
                    {
                        _descriptionControl.Text = string.Empty;
                    }
                    ResetDataBinding();
                    _PopupContainerEdit.Text = string.Empty;
                },
                ClientConstants.MainWorkspace);

            _PopupContainerEdit.OnOk += delegate
            {
                if (_descriptionControl == null) return;
                string description = _PopupContainerEdit.DataSource == null ? string.Empty : _PopupContainerEdit.DataSource.ToString(_isEnglish);

                _descriptionControl.Text = string.IsNullOrEmpty(description) ? (_normalDescription ?? string.Empty) : description;
            };

            _PopupContainerEdit.OnClear += _customerFinderControl_OnClear;
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
        }

        void ResetDataBinding()
        {
            foreach (Binding binding in _PopupContainerEdit.DataBindings)
            {
                if (binding.PropertyName == "Tag")
                {
                    binding.WriteValue();
                }
            }
        }
        public void SetCustomerDescription(CustomerDescriptionForNew customerDescription)
        {
            _PopupContainerEdit.DataSource = _DataSource = customerDescription;
        }


        #region IDisposable 成员
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _PopupContainerEdit.OnClear -= _customerFinderControl_OnClear;
            _PopupContainerEdit = null;
        }

        #endregion
    }
}
