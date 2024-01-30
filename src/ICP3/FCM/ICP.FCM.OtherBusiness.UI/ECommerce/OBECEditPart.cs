using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.Common;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OBContainerSaveRequest = ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects.ContainerSaveRequest;
using OBFeeSaveRequest = ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects.FeeSaveRequest;


namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    /// <summary>
    /// 其他业务编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBECEditPart : BaseEditPart
    {
        #region 本地变量 & 属性 & 事件

        /// <summary>
        /// 是否是订单编辑页面
        /// </summary>
        protected virtual bool IsOrderEditPart
        {
            get { return false; }
        }
        /// <summary>
        /// 是否是远东区解决方案(根据当前登录人的默认公司所属解决方案)
        /// </summary>
        bool _isFarEastSolution;
        /// <summary>
        /// 业务员更改后解锁数据
        /// </summary>
        bool isChangeSales;
        OtherBusinessInfo _CurrentData;
        CustomerFinderBridge consigneeBridge;


        List<CountryList> _countryList;
        List<CountryList> _CountryList
        {
            get
            {
                return _countryList ??
                       (_countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0));
            }
            set
            {
                _countryList = value;
            }
        }
        /// <summary>
        /// 委托列表
        /// </summary>
        List<BookingDelegate> _CacheDelegate = null;
        /// <summary>
        /// 委托列表
        /// </summary>
        List<BookingDelegate> CacheDelegate
        {
            get
            {
                if (_CacheDelegate == null)
                {
                    _CacheDelegate = new List<BookingDelegate>();
                }
                return _CacheDelegate;
            }
            set
            {
                _CacheDelegate = value;
            }
        }
        private ConfigureInfo _companyconfigureInfo;

        /// <summary>
        /// 
        /// </summary>
        public override event SavedHandler Saved;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler DataChanged;
        #endregion

        #region 服务
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 组织结构管理服务
        /// </summary>
        IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        /// <summary>
        /// 用户管理服务
        /// </summary>
        IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        
        /// <summary>
        /// 公共客户管理服务
        /// </summary>
        ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 国家，省份，地点信息维护
        /// </summary>
        IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
        /// <summary>
        /// UI 辅助类
        /// </summary>
        OtherUIHelper _OtherUIHelper
        {
            get
            {
                return ClientHelper.Get<OtherUIHelper, OtherUIHelper>();
            }
        }
        /// <summary>
        /// ICP 通用UI服务
        /// </summary>
        ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 其他业务服务
        /// </summary>
        IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }
        /// <summary>
        /// 配置管理服务
        /// </summary>
        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 显示操作帐单列表
        /// </summary>
        IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }
        /// <summary>
        /// 系统服务
        /// </summary>
        ISystemService SystemService
        {
            get
            {
                return ServiceClient.GetService<ISystemService>();
            }
        }
        /// <summary>
        /// FCM公共服务
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        #endregion

        #region Init & Override
        /// <summary>
        /// 其他业务编辑界面
        /// </summary>
        public OBECEditPart()
        {
            InitializeComponent();
            barTruck.Visibility = BarItemVisibility.Never;

            Disposed += OBBaseEditPart_Disposed;
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "BOOKINGINFOFORCSP".ToUpper())
                {
                    CacheDelegate = item.Value as List<BookingDelegate>;
                    break;
                }
            }
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
                {
                    _isFarEastSolution = true;
                }

                InitMessage();

                SetTitle();
                RegisterRelativeEvents();
                RegisterRelativeEventsAndRunOnce();

                #region 设置水印
                Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit> {
                stxtCustomer,
                stxtConsignee,
                stxtAgentOfCarrier });

                Utility.SetPortTextEditNullValuePrompt(new List<TextEdit> {
                stxtDetination,
                stxtDeparture });

                SmartPartClosing += OtherBusinessEditPart_SmartPartClosing;
                ActivateSmartPartClosingEvent(Workitem);
            }
                #endregion
        }
        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get { return bsOtherInfo.DataSource; }
            set
            {

                BindingData(value);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public override void EndEdit()
        {
            _CurrentData.VoyageID = Guid.Empty;
            Validate();
            bsOtherInfo.EndEdit();
        }
        /// <summary>
        /// 根据业务信息显示下拉式控件及其它一些控件的值
        /// </summary>
        private void InitControls()
        {
            //业务类型
            List<EnumHelper.ListItem<OtOperationType>> Types = EnumHelper.GetEnumValues<OtOperationType>(LocalData.IsEnglish);
            foreach (var item in Types)
            {
                if (item.Name.Contains("FBA"))
                    cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            //业务类型
            cmbType.ShowSelectedValue(_CurrentData.OtOperationType, EnumHelper.GetDescription(_CurrentData.OtOperationType, LocalData.IsEnglish, true));
            //操作口岸(公司)
            cmbCompany.ShowSelectedValue(_CurrentData.CompanyID, _CurrentData.CompanyName);
            //重量（毛重单位）
            cmbWeightUnit.ShowSelectedValue(_CurrentData.WeightUnitID, _CurrentData.WeightUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.WeightUnitID)
                && cmbWeightUnit.EditValue != null)
            {
                _CurrentData.WeightUnitID = new Guid(cmbWeightUnit.EditValue.ToString());
            }
            //体积cmbSales
            cmbMeasurementUnit.ShowSelectedValue(_CurrentData.MeasurementUnitID, _CurrentData.MeasurementUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.MeasurementUnitID)
                && cmbMeasurementUnit.EditValue != null)
            {
                _CurrentData.MeasurementUnitID = new Guid(cmbMeasurementUnit.EditValue.ToString());
            }
            //揽货人
            Utility.SetEnterToExecuteOnec(mcmbSales, () => _OtherUIHelper.SetMcmbUsers(mcmbSales, true, true));
            Utility.SetEnterToExecuteOnec(tsbSalesDep, SetSalesDepartment);
            //揽货人
            mcmbSales.ShowSelectedValue(_CurrentData.SalesID, _CurrentData.SalesName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesID)
                && mcmbSales.EditValue != null)
            {
                _CurrentData.OperatorID = _CurrentData.SalesID = new Guid(mcmbSales.EditValue.ToString());
            }
            //揽货部门
            tsbSalesDep.ShowSelectedValue(_CurrentData.SalesDepartmentID, _CurrentData.SalesDepartmentName);

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesDepartmentID)
               && tsbSalesDep.EditValue != null)
            {
                _CurrentData.SalesDepartmentID = new Guid(tsbSalesDep.EditValue.ToString());
            }
            //包装件数
            lwPackages.ShowSelectedValue(_CurrentData.QuantityUnitID, _CurrentData.QuantityUnitName);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.QuantityUnitID)
               && lwPackages.EditValue != null)
            {
                _CurrentData.QuantityUnitID = new Guid(lwPackages.EditValue.ToString());
            }
            InitalComboxes();
            //操作
            treeOperation.ShowSelectedValue(_CurrentData.OperatorID, _CurrentData.OperatorName);
        }
        /// <summary>
        /// 初始化下拉框值
        /// </summary>
        void InitalComboxes()
        {
            //包装件数
            _OtherUIHelper.SetCmbDataDictionary(lwPackages, DataDictionaryType.QuantityUnit);
            //重量
            _OtherUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);
            //体积
            _OtherUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit);

            ICPCommUIHelper.SetComboxCurrencys(cmbCurrency, _CurrentData.CompanyID, true);

        }
        /// <summary>
        /// 
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("1110170001", LocalData.IsEnglish ? "Are you sure to deleted the selected cost" : "确认删除选中的费用?");
            RegisterMessage("1110170002", LocalData.IsEnglish ? "Are you sure clear all cost" : "确认清空所有费用?");
        }
        /// <summary>
        /// 
        /// </summary>
        void RegisterRelativeEventsAndRunOnce()
        {
            RunAtOnce();
        }
        #region 刷新工具栏按钮的可使用性
        /// <summary>
        /// 总调用处，会把其它方法都执行一遍
        /// </summary>
        void RunAtOnce()
        {
            RefreshBarEnabled();
        }
        /// <summary>
        /// 
        /// </summary>
        void RefreshBarEnabled()
        {
            if (_CurrentData!=null)
            {
                txtState.Text = EnumHelper.GetDescription(_CurrentData.State, LocalData.IsEnglish);
                cmbType.Text = EnumHelper.GetDescription(_CurrentData.OtOperationType, LocalData.IsEnglish);
            }
        }
        #endregion
        /// <summary>
        /// 搜索器注册
        /// </summary>
        void SearchRegister()
        {
            #region Customer

            //Customer
            DataFindClientService.Register(stxtCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName,
                SearchFieldConstants.ResultValue,
                GetConditionsForCustomer,
                      delegate(object inputSource, object[] resultData)
                      {
                          Guid? oldCustomerId = _CurrentData.CustomerID;
                          stxtCustomer.ClosePopup();

                          CustomerStateType state = (CustomerStateType)resultData[5];
                          if (state == CustomerStateType.Invalid)
                          {
                              if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
                              {
                                  return;
                              }
                          }

                          CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[6];
                          if (!approved.HasValue
                              || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                          {
                              if (_isFarEastSolution)
                              {
                                  if (approved.Value == CustomerCodeApplyState.Processing)
                                  {
                                      DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
                       , LocalData.IsEnglish ? "Tip" : "提示"
                       , MessageBoxButtons.OK
                       , MessageBoxIcon.Question);

                                      return;
                                  }
                                  else if (approved.Value == CustomerCodeApplyState.UnApply)
                                  {
                                      if ((resultData[8] == null ||
                                          string.IsNullOrEmpty(resultData[8].ToString())) &&
                                          (resultData[9] == null ||
                                          string.IsNullOrEmpty(resultData[9].ToString())))
                                      {
                                          XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                      , LocalData.IsEnglish ? "Tip" : "提示"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Question);
                                          return;
                                      }

                                      DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question);
                                      if (result == DialogResult.Yes)
                                      {
                                          CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                            LocalData.UserInfo.LoginID,
                                                                            LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                            (DateTime?)resultData[7]);
                                      }

                                      return;
                                  }
                                  else if (approved.Value == CustomerCodeApplyState.Unpassed)
                                  {
                                      if ((resultData[8] == null ||
                                          string.IsNullOrEmpty(resultData[8].ToString())) &&
                                          (resultData[9] == null ||
                                          string.IsNullOrEmpty(resultData[9].ToString())))
                                      {
                                          XtraMessageBox.Show(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                      , LocalData.IsEnglish ? "Tip" : "提示"
                      , MessageBoxButtons.OK
                      , MessageBoxIcon.Question);
                                          return;
                                      }

                                      DialogResult result = XtraMessageBox.Show("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question);
                                      if (result == DialogResult.Yes)
                                      {
                                          CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                            LocalData.UserInfo.LoginID,
                                                                            LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                            (DateTime?)resultData[7]);
                                      }

                                      return;
                                  }
                              }
                              else
                              {
                                  if (approved.Value == CustomerCodeApplyState.Processing || approved.Value == CustomerCodeApplyState.Unpassed)
                                  {
                                      if (PartLoader.PopCustomerUnApproved() != DialogResult.Yes)
                                      {
                                          return;
                                      }
                                  }

                                  if (approved.Value == CustomerCodeApplyState.UnApply)
                                  {
                                      DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question);
                                      if (result == DialogResult.Yes)
                                      {
                                          CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                            LocalData.UserInfo.LoginID,
                                                                            LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                            (DateTime?)resultData[7]);
                                      }
                                  }
                              }
                          }

                          stxtCustomer.EditValue = _CurrentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          stxtCustomer.Tag = _CurrentData.CustomerID = new Guid(resultData[0].ToString());

                          if (oldCustomerId != Guid.Empty && _CurrentData.CustomerID == oldCustomerId) return;

                      }, delegate
                      {
                          stxtCustomer.Text = _CurrentData.CustomerName = string.Empty;
                          stxtCustomer.Tag = _CurrentData.CustomerID = Guid.Empty;
                          stxtCustomer.ClosePopup();
                      },
                      ClientConstants.MainWorkspace);

            DataFindClientService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustomerAgentOfCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
               delegate(object inputSource, object[] resultData)
               {
                   stxtAgentOfCarrier.Text = _CurrentData.AgengofCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                   stxtAgentOfCarrier.Tag = _CurrentData.AgentofCarrierID = new Guid(resultData[0].ToString());
                   SetCompanyConfigureInfo();
               }, Guid.Empty, ClientConstants.MainWorkspace);

            #endregion

            #region Consignee
            //Consignee
            Utility.SetEnterToExecuteOnec(stxtConsignee, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                consigneeBridge = new CustomerFinderBridge(
                stxtConsignee,
                _countryList,
                DataFindClientService,
                CustomerService,
                _CurrentData.ConsigneeDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });

            stxtConsignee.OnOk += lwConsignee_OnOk;
            #endregion

            #region Port
            PortFinderBridge pfbPOL = new PortFinderBridge(stxtDeparture, DataFindClientService, LocalData.IsEnglish);

            PortFinderBridge pfbPOD = new PortFinderBridge(stxtDetination, DataFindClientService, LocalData.IsEnglish);

            #endregion

            #region RefNo

            //业务搜索器       
            DataFindClientService.Register(stxtOperationNo, FCMFinderConstants.BusinessFinderForOEAE, SearchFieldConstants.BusinessNo, SearchFieldConstants.BusinessResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid bookingID = new Guid(resultData[0].ToString());
                      string bookingNo = resultData[1].ToString();
                      if (_CurrentData.OperationID != bookingID)
                      {
                          AfterSearchRefNo(bookingID, bookingNo);
                      }
                      else
                      {
                          LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ?
                        "You has been selected this booking." : "您已选择了该票业务!");
                      }
                  },
                  delegate
                  {
                      if (_CurrentData.IsNew)
                      {
                          stxtOperationNo.Tag = _CurrentData.OperationID = Guid.Empty;
                          stxtOperationNo.Text = _CurrentData.OperationNo = string.Empty;
                      }

                  }, ClientConstants.MainWorkspace);

            #endregion
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 当前用户所在的操作口岸和揽货人所在的部门
        /// </summary>
        void tsbSalesDep_Enter(object sender, EventArgs e)
        {
            List<OrganizationList> userCompanyList = new List<OrganizationList>();

            userCompanyList = _CurrentData.SalesID != null ? UserService.GetUserCompanyList(_CurrentData.SalesID.Value, null) : OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);

            tsbSalesDep.SetSource(userCompanyList, LocalData.IsEnglish ? "EShortName" : "CShortName");
        }
        /// <summary>
        /// 
        /// </summary>
        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesID) == false)
            {
                List<UserOrganizationTreeList> orgList = UserService.GetUserOrganizationTreeList(_CurrentData.SalesID.Value);

                OrganizationList orginazation = orgList.Find(delegate(UserOrganizationTreeList item) { return item.IsDefault && item.Type == OrganizationType.Department; });
                if (orginazation != null)
                {
                    tsbSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _CurrentData.SalesDepartmentID = orginazation.ID;
                    _CurrentData.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    tsbSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
                    _CurrentData.SalesDepartmentID = Guid.Empty;
                    _CurrentData.SalesDepartmentName = string.Empty;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void lwConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null)
                _CurrentData.ConsigneeDescription = stxtConsignee.CustomerDescription;
        }

        /// <summary>
        /// 填充“操作”的用户列表供选择
        /// </summary>
        void treeOperation_Click(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = new Guid(cmbCompany.EditValue.ToString());
            }
            ICPCommUIHelper.SetComboxUsersByRoles(treeOperation, depID, new string[] { "订舱", "客服", "文件" }, true);
        }
        /// <summary>
        /// 保存业务
        /// </summary>
        void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                _CurrentData = bsOtherInfo.DataSource as OtherBusinessInfo;
                Save(_CurrentData, false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                RefreshData(_CurrentData.ID);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refersh successfully." : "刷新成功.");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refersh failed." + ex.Message : "刷新失败." + ex.Message);
            }
        }
        /// <summary>
        /// 账单
        /// </summary>
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentData == null)
                {
                    return;
                }

                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(_CurrentData.ID, OperationType.Other);
                if (operationCommonInfo != null)
                {
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }
        /// <summary>
        /// 派车
        /// </summary>
        private void barTruck_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                _CurrentData = bsOtherInfo.DataSource as OtherBusinessInfo;
            }
        }
        /// <summary>
        /// 另存为(此处细节逻辑点待进一步确认）
        /// </summary>
        private void barSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (SaveAs())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save as a new Business successfully. Ref. NO. is " + _CurrentData.NO + "." : "已成功另存为一票新的业务，业务号为" + _CurrentData.NO + "。");
                    if (Saved != null)
                    {
                        Saved(new object[] { _CurrentData });
                    }
                }
            }
        }
        /// <summary>
        /// 操作部经理更改业务员
        /// </summary>
        private void cmbSales_EditValueChanged(object sender, EventArgs e)
        {
            ConfigureInfo configure = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID, LocalData.IsEnglish);
            if (_CurrentData.OperationDate != null && _CurrentData.OperationDate < configure.AccountingClosingdate)
            {
                List<User2OrganizationJobList> jobList = UserService.GetUser2OrganizationJobList(LocalData.UserInfo.LoginID);
                foreach (User2OrganizationJobList job in jobList)
                {
                    if (job.OrganizationJobName.Contains("操作部经理") || job.OrganizationJobName.Contains("操作经理") || job.OrganizationJobName.Contains("操作部->经理"))
                    {
                        isChangeSales = true;
                    }
                }
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            var findForm = FindForm();
            if (findForm != null) findForm.Close();
        }
        /// <summary>
        /// 关闭前提示
        /// </summary>
        void OtherBusinessEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentData.IsDirty)
                {
                    DialogResult dr = PartLoader.EnquireIsSaveCurrentDataByUpdated();

                    if (dr == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                    else if (dr == DialogResult.Yes)
                    {
                        if (!Save(_CurrentData, false))
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 清理资源和避免多余操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OBBaseEditPart_Disposed(object sender, EventArgs e)
        {
            _CountryList = null;
            _CurrentData = null;
            dxErrorProvider1.DataSource = null;
            bsOtherInfo.DataSource = null;
            bsOtherInfo.Dispose();
            Saved = null;
            if (Workitem == null) return;
            Workitem.Items.Remove(this);
            Workitem = null;
        }
        /// <summary>
        /// 防闪烁
        /// </summary>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0014) return;// 禁掉清除背景消息
            base.WndProc(ref m);
        }
        #endregion

        /// <summary>
        /// bug2972: 业务写订单时，选择客户，只能选择自己有权限的CRM关联的公共客户。
        /// </summary>
        /// <returns></returns>
        SearchConditionCollection GetConditionsForCustomer()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            if (IsOrderEditPart)
            {
                conditions.AddWithValue("IsFromOrder", true, false);
                conditions.AddWithValue("CurruntUserID", LocalData.UserInfo.LoginID, false);
            }

            return conditions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetSource(object value)
        {
            if (value == null) return;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workitem"></param>
        public void SetService(WorkItem workitem)
        {
            Workitem = workitem;
            InitControls();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void BindingData1(object data)
        {
            _CurrentData = bsOtherInfo.DataSource as OtherBusinessInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void BindingData(object data)
        {
            SuspendLayout();

            OtherBusinessList listInfo = data as OtherBusinessList;

            if (listInfo == null)
            {
                //新建
                _CurrentData = new OtherBusinessInfo();
                mcmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName);
                ReadyForNew();
            }
            else
            {
                GetData(listInfo.ID, listInfo.CompanyID);
                RefreshBarEnabled();

                if (listInfo.EditMode == EditMode.Copy)
                {
                    PrepareForCopyExistOrder();
                }
            }

            _CurrentData.CancelEdit();
            SetCompanyConfigureInfo();

            ShowOrder();

            SearchRegister();
            SetLazyLoaders();

            ResumeLayout(true);
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public override bool SaveData()
        {
            return Save(_CurrentData, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="companyID"></param>
        void GetData(Guid orderId, Guid companyID)
        {
            _CurrentData = OtherBusinessService.GetOtherBusinessInfo(orderId, companyID);
        }
        /// <summary>
        /// 显示订单
        /// </summary>
        void ShowOrder()
        {
            if (_CurrentData.ConsigneeDescription == null)
            {
                _CurrentData.ConsigneeDescription = new CustomerDescription();
            }
            if (_CurrentData.ShipperDescription == null)
            {
                _CurrentData.ShipperDescription = new CustomerDescription();
            }
            if (_CurrentData.NotifyDescription == null)
            {
                _CurrentData.NotifyDescription = new CustomerDescription();
            }

            bsOtherInfo.DataSource = _CurrentData;
            bsOtherInfo.ResetBindings(false);
            bsOtherInfo.CancelEdit();

            InitControls();

            List<OBFeeList> feelist = null;
            if (_CurrentData.ID == Guid.Empty)
            {
                feelist = new List<OBFeeList>();
                if (CacheDelegate.Count > 0)
                {
                    FillCSPBookingToCurrent();
                }
            }
            else
            {
                feelist = OtherBusinessService.GetOBOrderFeeList(_CurrentData.ID, _CurrentData.CompanyID);
                CacheDelegate = FCMCommonService.GetBookingDelegateList(new SearchParameterBookingDelegate() { OperationID = _CurrentData.ID });
            }

            partDelegate.DataChanged += (sender, e) =>
            {
                CacheDelegate = sender as List<BookingDelegate>;
                FillCSPBookingToCurrent();
            };
            partDelegate.SetSource(CacheDelegate);
        }

        void FillCSPBookingToCurrent()
        {
            BookingDelegate cspPBooking = CacheDelegate.SingleOrDefault(fitem => fitem.IsDefault && fitem.CancelRemark.IsNullOrEmpty());
            if (cspPBooking == null)
                cspPBooking = CacheDelegate.First();
            if (!cspPBooking.CancelRemark.IsNullOrEmpty())
            {
                return;
            }
            if (cspPBooking.FBAFreightMethodType!=CSP_FBAFREIGHTMETHODTYPE.Unknown)
            {
                switch (cspPBooking.FBAFreightMethodType)
                {
                    case CSP_FBAFREIGHTMETHODTYPE.OceanTruck:
                        _CurrentData.OtOperationType = OtOperationType.FBAOceanTruck;
                        break;
                    case CSP_FBAFREIGHTMETHODTYPE.OceanExpress:
                         _CurrentData.OtOperationType = OtOperationType.FBAOcean;
                        break;
                    case CSP_FBAFREIGHTMETHODTYPE.Express:
                    case CSP_FBAFREIGHTMETHODTYPE.AirExpress:
                        _CurrentData.OtOperationType = OtOperationType.FBAAir;
                        break;
                }
                cmbType.ShowSelectedValue(_CurrentData.OtOperationType, EnumHelper.GetDescription(_CurrentData.OtOperationType, LocalData.IsEnglish, true));
            }
            
            //初始化
            dteBookingDate.EditValue = _CurrentData.OperationDate = cspPBooking.BookingDate;
            //Customer
            stxtCustomer.Tag = _CurrentData.CustomerID = cspPBooking.CustomerID;
            stxtCustomer.Text = _CurrentData.CustomerName = cspPBooking.CustomerName;
            //Consignee
            stxtConsignee.Tag = _CurrentData.ConsigneeID = cspPBooking.ConsigneeID;
            stxtConsignee.Text = _CurrentData.ConsigneeName = cspPBooking.ConsigneeName;
            stxtConsignee.CustomerDescription = _CurrentData.ConsigneeDescription = cspPBooking.ConsigneeDescription;

            mcmbSales.EditValue = _CurrentData.SalesID = cspPBooking.SalesID;
            _CurrentData.SalesName = cspPBooking.SalesName;
            mcmbSales.ShowSelectedValue(_CurrentData.SalesID, _CurrentData.SalesName);
            SetSalesDepartment();
            _CurrentData.IsTruck = cspPBooking.IsTruck;
            _CurrentData.IsCustoms = cspPBooking.IsDeclaration;
            _CurrentData.PlaceOfReceiptAddress = cspPBooking.POLAddress;
            _CurrentData.PlaceOfDeliveryAddress = cspPBooking.PODAddress;
            cmbWeightUnit.EditValue = cspPBooking.WeightUnitID;
            cmbMeasurementUnit.EditValue = cspPBooking.MeasurementUnitID;
            //毛件体汇总
            _CurrentData.Quantity = CacheDelegate.Sum(fitem => fitem.Quantity);
            _CurrentData.Weight = CacheDelegate.Sum(fitem => fitem.Weight);
            _CurrentData.Measurement = CacheDelegate.Sum(fitem => fitem.Measurement);
        }

        #region 注册各种联动的事件

        /// <summary>
        /// 注册各种联动的事件
        /// </summary>
        void RegisterRelativeEvents()
        {
            panelTabBaseInfo.Click += delegate
            {
                panelTabBaseInfo.Focus();
            };

            mcmbSales.SelectedRow += mcmbSales_SelectedRow;
            tsbSalesDep.Enter += tsbSalesDep_Enter;
        }
        #endregion

        #region 延迟加载的数据源
        /// <summary>
        /// 为需要延迟加载数据源的控件注册事件
        /// 这些数据源不会和其它控件发生联动，加载一次即可
        /// </summary>
        void SetLazyLoaders()
        {
            //操作口岸列表   
            Utility.SetEnterToExecuteOnec(cmbCompany, delegate
            {
                ICPCommUIHelper.BindCompanyByAll(cmbCompany, false);

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CompanyID))
                {
                    _CurrentData.CompanyID = LocalData.UserInfo.DefaultCompanyID;
                }

                cmbCompany.SelectedIndexChanged += delegate
                {
                    CompanyChanged();
                };
            });

            treeOperation.Enter += treeOperation_Click;
        }
        /// <summary>
        /// 公司改变
        /// </summary>
        private void CompanyChanged()
        {
            SetOperatorByCompany();
        }
        /// <summary>
        /// 根据操作口岸ID设置操作和文件栏的数据源
        /// </summary>
        private void SetOperatorByCompany()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.CompanyID))
            {
            }
            else
            {
                SetCompanyConfigureInfo();
                ICPCommUIHelper.SetComboxCurrencys(cmbCurrency, _CurrentData.CompanyID, true);
                Dictionary<string, string> col = new Dictionary<string, string>
                {
                    {LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称"},
                    {"Code", LocalData.IsEnglish ? "Code" : "代码"}
                };

                List<UserList> operators = UserService.GetUnderlingUserList(new[] { _CurrentData.CompanyID }, new[] { "操作" }, null, true);
            }
        }
        #endregion

        #region SetSalesDepartment
        /// <summary>
        /// 改变失去焦点后刷新揽货部门，如果有多个就清空，否则填充
        /// </summary>
        private void SetSalesDepartment()
        {
            List<OrganizationList> userOrganizationTreeLists1 = new List<OrganizationList>();
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.SalesID) == false)
            {
                userOrganizationTreeLists1 = UserService.GetUserCompanyList(_CurrentData.SalesID.Value, null);

                OrganizationList orginazation = userOrganizationTreeLists1.Find(delegate(OrganizationList item) { return item.IsDefault && item.Type == OrganizationType.Department; });
                if (orginazation != null)
                {
                    tsbSalesDep.ShowSelectedValue(orginazation.ID, LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName);
                    _CurrentData.SalesDepartmentID = orginazation.ID;
                    _CurrentData.SalesDepartmentName = LocalData.IsEnglish ? orginazation.EShortName : orginazation.CShortName;
                }
                else
                {
                    tsbSalesDep.ShowSelectedValue(Guid.Empty, string.Empty);
                    _CurrentData.SalesDepartmentID = Guid.Empty;
                    _CurrentData.SalesDepartmentName = string.Empty;
                }
            }
        }
        #endregion

        /// <summary>
        /// 新增订单逻辑
        /// </summary>
        void ReadyForNew()
        {
            OtherBusinessInfo newData = new OtherBusinessInfo { CompanyID = LocalData.UserInfo.DefaultCompanyID };
            string userName = LocalData.UserInfo.LoginName;
            if (!LocalData.IsEnglish)
            {
                userName = LocalData.UserInfo.UserName;
            }
            newData.CompanyName = LocalData.UserInfo.DefaultCompanyName;
            newData.OperatorID = LocalData.UserInfo.LoginID;
            newData.OperatorName = userName;
            newData.SalesID = LocalData.UserInfo.LoginID;
            newData.SalesName = userName;
            newData.SalesDepartmentID = LocalData.UserInfo.DefaultDepartmentID;
            newData.SalesDepartmentName = LocalData.UserInfo.DefaultDepartmentName;

            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.State = OBOrderState.NewOrder;
            newData.IsValid = true;

            #region 设置默认值
            DataDictionaryList normalDictionary = null;

            normalDictionary = _OtherUIHelper.GetNormalDictionary(DataDictionaryType.QuantityUnit);
            newData.QuantityUnitID = normalDictionary.ID;
            newData.QuantityUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;

            normalDictionary = _OtherUIHelper.GetNormalDictionary(DataDictionaryType.WeightUnit);
            newData.WeightUnitID = normalDictionary.ID;
            newData.WeightUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            newData.OperationDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.Feta = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            normalDictionary = _OtherUIHelper.GetNormalDictionary(DataDictionaryType.MeasurementUnit);
            newData.MeasurementUnitID = normalDictionary.ID;
            newData.MeasurementUnitName = LocalData.IsEnglish ? normalDictionary.EName : normalDictionary.CName;
            #endregion

            _CurrentData = newData;

            Utility.EnsureDefaultCompanyExists(UserService);
        }
        /// <summary>
        /// 复制订单逻辑
        /// </summary>
        void PrepareForCopyExistOrder()
        {
            _CurrentData.ID = Guid.Empty;
            _CurrentData.State = OBOrderState.NewOrder;
            _CurrentData.NO = string.Empty;
            _CurrentData.CreateByID = LocalData.UserInfo.LoginID;
            _CurrentData.CreateByName = LocalData.UserInfo.LoginName;
            _CurrentData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

            _CurrentData.OperatorID = LocalData.UserInfo.LoginID;
            _CurrentData.OperatorName = LocalData.UserInfo.LoginName;

            _CurrentData.OperationID = LocalData.UserInfo.DefaultCompanyID;
        }
        /// <summary>
        /// 验证界面输入
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            txtNo.Focus();
            EndEdit();
            dxErrorProvider1.ClearErrors();
            _CurrentData = bsOtherInfo.DataSource as OtherBusinessInfo;
            List<bool> isScrrs = new List<bool> { true, true };

            isScrrs[0] = _CurrentData.Validate
               (
                   delegate(ValidateEventArgs e)
                   {
                       if (_CurrentData.PolID == _CurrentData.PodID)
                       {

                           if (_CurrentData.PolID != Guid.Empty || _CurrentData.PodID != Guid.Empty)
                           {
                               if (_CurrentData.PolID != null && _CurrentData.PodID != null)
                               {
                                   e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                               }
                           }

                       }
                       if (_CurrentData.Eta != null && _CurrentData.Etd != null
                           && _CurrentData.Etd >= _CurrentData.Eta)
                           e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");

                       if (_CurrentData.OtOperationType == null || _CurrentData.OtOperationType.GetHashCode() == 0)
                       {
                           e.SetErrorInfo("OperationType", LocalData.IsEnglish ? "Business type must be selected." : "业务类型必须选择.");
                       }

                       if (_CurrentData.Weight <= Convert.ToDecimal(0) &&
                               _CurrentData.Measurement <= Convert.ToDecimal(0))
                       {
                           e.SetErrorInfo("OperationType", LocalData.IsEnglish ? "Business type is FBA Air or FBA Ocean Measurement and weight is required!" : "业务类型为FBA空派或FBA海派时毛重或体积必填!");
                       }
                   }
               );

            bool isScrr = true;
            foreach (var item in isScrrs)
                if (item == false) isScrr = false;

            if (isScrrs[0] == false)
                xtraTabControl1.SelectedTabPageIndex = 0;
            return isScrr;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="currentData"></param>
        /// <param name="isSavingAs"></param>
        /// <returns></returns>
        private bool Save(OtherBusinessInfo currentData, bool isSavingAs)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (ValidateData() == false)
                {
                    return false;
                }
                if (!currentData.IsDirty && !currentData.IsNew)
                {
                    return true;
                }
                try
                {
                    OtherBusinessSaveRequest originalBooking = null;
                    if (ArgumentHelper.GuidIsNullOrEmpty(currentData.ID))
                    {
                        originalBooking = BuildOtherBusiness(currentData);
                       
                    }
                    else if (_CurrentData.IsDirty)
                    {
                        originalBooking = BuildOtherBusiness(_CurrentData);
                    }

                    if (isChangeSales)
                    {
                        Guid[] checkIds = { _CurrentData.ID };
                        SystemService.SaveUntieLockInfo(UntieLockType.Sales, checkIds, LocalData.UserInfo.LoginID);
                    }

                    List<OBFeeSaveRequest> originalFees = new List<OBFeeSaveRequest>();
                    List<OBContainerSaveRequest> _container = null;
                    List<SaveRequestBookingDelegate> originalDelegates = partDelegate.BuildSaveRequest(currentData.ID, OperationType.Other);
                    Dictionary<Guid, SaveResponse> saved = OtherBusinessService.SaveOtherBusinessWithTrans(originalBooking, originalFees, _container, originalDelegates);
                    //基本信息
                    if (originalBooking != null)
                    {
                        SaveResponse.Analyze(new List<SaveRequest> { originalBooking }, saved, true);
                        RefreshUI(originalBooking);
                    }
                    if (EditMode == EditMode.New || EditMode == EditMode.Copy)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "The company is read only after the shipment is created." : "订单创建后，操作口岸不能更改。");
                    }
                    if (isSavingAs)
                    {
                    }
                    else
                    {
                        AfterSave();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                    return false;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void AfterSave()
        {
            _CurrentData.CancelEdit();
            _CurrentData.BeginEdit();

            if (_CurrentData.ConsigneeDescription != null)
            {
                _CurrentData.ConsigneeDescription.IsDirty = false;
            }
            if (_CurrentData.ShipperDescription != null)
            {
                _CurrentData.ShipperDescription.IsDirty = false;
            }
            if (_CurrentData.NotifyDescription != null)
            {
                _CurrentData.NotifyDescription.IsDirty = false;
            }


            TriggerSavedEvent();
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

            SetTitle();
        }
        /// <summary>
        /// 保存联动事件:设置业务、操作名称
        /// </summary>
        void TriggerSavedEvent()
        {
            if (Saved != null)
            {
                _CurrentData.SalesName = _CurrentData.SalesID.ToGuid() == Guid.Empty ?
                    string.Empty : mcmbSales.EditText;
                _CurrentData.OperatorName = _CurrentData.OperatorID.ToGuid() == Guid.Empty ?
                    string.Empty : tsbSalesDep.Text;
                Saved(new object[] { _CurrentData, OperationType.Other });

                _CurrentData.IsDirty = false;
            }
        }
        /// <summary>
        /// 保存后刷新界面数据
        /// </summary>
        /// <param name="saveRequest"></param>
        void RefreshUI(OtherBusinessSaveRequest saveRequest)
        {
            SingleResult result = saveRequest.SingleResult;

            OtherBusinessInfo currentData = saveRequest.UnBoxInvolvedObject<OtherBusinessInfo>()[0];

            currentData.ID = result.GetValue<Guid>("ID");
            currentData.NO = result.GetValue<string>("No");
            currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentData.State = (OBOrderState)result.GetValue<byte>("State");
            currentData.IsDirty = false;

            if (currentData.ConsigneeDescription != null)
            {
                currentData.ConsigneeDescription.IsDirty = false;
            }
            if (currentData.ShipperDescription != null)
            {
                currentData.ShipperDescription.IsDirty = false;
            }
            if (currentData.NotifyDescription != null)
            {
                currentData.NotifyDescription.IsDirty = false;
            }

        }
        /// <summary>
        /// 设置标题
        /// </summary>
        void SetTitle()
        {
            cmbCompany.Enabled = false;
            if (_CurrentData == null || _CurrentData.ID == Guid.Empty)
            {
                Title = LocalData.IsEnglish ? "Add E-Commerce Business" : "新增电商物流业务信息";
                cmbCompany.Enabled = true;
            }
            else
            {
                string titleNo = string.Empty;

                if (_CurrentData.NO.Length > 4)
                {
                    titleNo = _CurrentData.NO.Substring(_CurrentData.NO.Length - 4, 4);
                }
                else
                {
                    titleNo = _CurrentData.NO;
                }

                Title = LocalData.IsEnglish ? "Edit E-Commerce Business " + titleNo : "编辑电商物流业务信息：" + titleNo;
            }
        }
        /// <summary>
        /// 构建保存其他业务实体
        /// </summary>
        /// <param name="currentData"></param>
        /// <returns></returns>
        private OtherBusinessSaveRequest BuildOtherBusiness(OtherBusinessInfo currentData)
        {
            EndEdit();
            if (currentData.IsDirty == true || currentData.IsNew)
            {
                OtherBusinessSaveRequest saveRequest = new OtherBusinessSaveRequest();

                saveRequest.id = currentData.ID;
                saveRequest.AgentOfCarrierID = currentData.AgentofCarrierID;
                saveRequest.CarrierID = currentData.CarrierID;
                saveRequest.Commodity = currentData.Commodity;
                saveRequest.CompanyID = currentData.CompanyID;
                saveRequest.ConsigneeID = currentData.ConsigneeID;
                saveRequest.CustomerID = currentData.CustomerID;
                saveRequest.CustomsBrokerID = currentData.CustomsBrokerID;
                saveRequest.ETA = currentData.Eta;
                saveRequest.ETD = currentData.Etd;
                saveRequest.FETA = currentData.Feta;

                saveRequest.HBLNO = currentData.Hblno;
                saveRequest.IsCommodityInspection = currentData.IsCommodityInspection;
                saveRequest.IsCustoms = currentData.IsCustoms;
                saveRequest.IsQuarantineInspection = currentData.IsQuarantineInspection;
                saveRequest.IsTruck = currentData.IsTruck;
                saveRequest.IsWarehouse = currentData.IsWareHouse;
                saveRequest.MBLNO = currentData.Mblno;
                saveRequest.SONO = currentData.ExpressNo;
                saveRequest.ExpressNo = currentData.ExpressNo;
                saveRequest.Measurement = currentData.Measurement;
                saveRequest.MeasurementUnitID = currentData.MeasurementUnitID;
                saveRequest.NotifyPartyID = currentData.NotifyPartyID;
                saveRequest.OperationDate = currentData.OperationDate;
                saveRequest.OperationID = currentData.OperationID;
                saveRequest.OperationNo = currentData.OperationNo;
                saveRequest.CurrencyID = currentData.CurrencyID;
                saveRequest.CostAmount = currentData.CostAmount;
                saveRequest.RevenueTon = currentData.RevenueTon;
                saveRequest.OperatorID = currentData.OperatorID;
                saveRequest.OTOperationType = currentData.OtOperationType;
                saveRequest.PaymentTypeID = currentData.PaymentTypeID;
                saveRequest.PODID = currentData.PodID;
                saveRequest.PODName = currentData.PodName;
                saveRequest.FinalDestinationID = currentData.PodID;
                saveRequest.FinalDestinationName = currentData.PodName;

                saveRequest.POLID = currentData.PolID;
                saveRequest.POLName = currentData.PolName;
                saveRequest.Quantity = currentData.Quantity;
                saveRequest.QuantityUnitID = currentData.QuantityUnitID;
                saveRequest.Remark = currentData.Remark;
                saveRequest.SalesDepartmentID = currentData.SalesDepartmentID;
                saveRequest.SalesID = currentData.SalesID;
                saveRequest.OverseasFilerID = currentData.OverseasFilerID;
                saveRequest.ShipperID = currentData.ShipperID;
                saveRequest.SONO = currentData.SoNo;
                saveRequest.UpdateDate = currentData.UpdateDate;
                saveRequest.VoyageID = currentData.VoyageID;
                saveRequest.WarehouseID = currentData.WarehouseID;
                saveRequest.Weight = currentData.Weight;
                saveRequest.WeightUnitID = currentData.WeightUnitID;
                saveRequest.IsSyncCSP = CacheDelegate.Count > 0;
                saveRequest.SaveByID = LocalData.UserInfo.LoginID;
                saveRequest.AddInvolvedObject(currentData);


                return saveRequest;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="orderId"></param>
        void RefreshData(Guid orderId)
        {
            GetData(_CurrentData.ID, _CurrentData.CompanyID);
            ShowOrder();
            RunAtOnce();
            SetTitle();
        }
        /// <summary>
        /// 另存为
        /// </summary>
        /// <returns></returns>
        bool SaveAs()
        {
            if (ValidateData() == false)
            {
                return false;
            }

            if (XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的业务?",
                            LocalData.IsEnglish ? "Tip" : "提示",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            OtherBusinessInfo orderInfo = Utility.Clone(_CurrentData);
            orderInfo.ID = Guid.Empty;
            orderInfo.NO = string.Empty;

            orderInfo.State = OBOrderState.NewOrder;

            orderInfo.CreateByID = LocalData.UserInfo.LoginID;
            orderInfo.CreateByName = LocalData.UserInfo.LoginName;
            orderInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            orderInfo.OperationDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            orderInfo.UpdateDate = null;

            if (orderInfo.OtOperationType == OtOperationType.CustomsDeclaration)
            {
            }

            orderInfo.IsDirty = true;

            _CurrentData = orderInfo;

            if (Save(orderInfo, true))
            {
                RefreshData(orderInfo.ID);

                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 搜索业务号后把Booking的数据填充到当前页面
        /// </summary>
        private void AfterSearchRefNo(Guid bookingID, string bookingNo)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentData.OperationID) && _CurrentData.OperationID == bookingID) return;

            OperationType operationtype = OperationType.Unknown;
            if (_CurrentData.OtOperationType == OtOperationType.FBAOcean)
            {
                operationtype = OperationType.OceanExport;
            }
            if (_CurrentData.OtOperationType == OtOperationType.FBAAir)
            {
                operationtype = OperationType.AirExport;
            }
            if (_CurrentData.OtOperationType == OtOperationType.FBAOceanTruck)
            {
                operationtype = OperationType.OceanExport;
            }
            if (operationtype == OperationType.Unknown)
            {
                MessageBoxService.ShowWarning("查找关联业务的类型未知,请确认当前业务是否支持关联！");
                return;
            }

            OperationCommonInfo bookingInfo = FCMCommonService.GetOperationCommonInfo(bookingID, operationtype);
            if (bookingInfo == null)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),"选择的业务类型有误，请确认当前类型是否与选择的业务一致!");
                return;
            }
            stxtOperationNo.Text = _CurrentData.OperationNo = bookingInfo.OperationNo;
            stxtOperationNo.Tag = _CurrentData.OperationID = bookingInfo.OperationID;


            bsOtherInfo.EndEdit();
        }
        /// <summary>
        /// 设置口岸配置
        /// </summary>
        private void SetCompanyConfigureInfo()
        {
            if (_CurrentData == null) return;

            if (_companyconfigureInfo == null || _companyconfigureInfo.CompanyID != _CurrentData.CompanyID)
            {
                _companyconfigureInfo = ConfigureService.GetCompanyConfigureInfo(_CurrentData.CompanyID);
            }
            if (_CurrentData.AgentofCarrierID != _companyconfigureInfo.CustomerID)
            {
                stxtOperationNo.Tag = _CurrentData.OperationID = Guid.Empty;
                stxtOperationNo.Text = _CurrentData.OperationNo = string.Empty;
                stxtOperationNo.Enabled = false;
                spinCost.EditValue = 0.00;
                spinCost.Enabled = false;
                spinRevenueTon.EditValue = 0.00;
                spinRevenueTon.Enabled = false;
                cmbCurrency.EditValue = null;
                cmbCurrency.Enabled = false;
            }
            else
            {
                stxtOperationNo.Enabled = true;
                spinCost.Enabled = true;
                spinRevenueTon.Enabled = true;
                cmbCurrency.Enabled = true;
            }
        }
    }
}