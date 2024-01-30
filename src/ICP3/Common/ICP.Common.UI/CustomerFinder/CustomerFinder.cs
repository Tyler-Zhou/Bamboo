using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections;

namespace ICP.Common.UI.CustomerFinder
{
    public class CustomerFinder : IDataFinder, IDisposable
    {
        /// <summary>
        /// IDataFinder 成员
        /// </summary>
        public bool IsBusy { get; set; }

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        #endregion

        private const string CustomerFinderWorkspace = "CustomerFinderWorkspace";

        #region ChildWorkitem

        CustomerSingleFinderWorkitem singleFinderWorkitem = null;
        CustomerMultiFinderWorkitem multiFinderWorkitem = null;

        #endregion

        #region IDataFinder 成员

        public event EventHandler<DataFindEventArgs> DataChoosed;

        #region One

        public void PickOne(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(CustomerFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (this.Workitem.Workspaces.Contains(CustomerFinderWorkspace))
                {
                    this.Workitem.Workspaces.Remove(workspce);
                }

                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(CustomerFinderWorkspace);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            PickOne(searchValue, property, conditions, returnFields, triggerType, CustomerFinderWorkspace);
        }

        public void PickOne(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            #region

            string name = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter) name = searchValue;

            #region Get Condition

            _customerTypes = new List<CustomerType>();
            if (conditions != null && conditions.Contain("CustomerType"))
            {
                List<ICP.Framework.CommonLibrary.Client.SearchCondition> typeConditions
                    = conditions.GetValues("CustomerType");
                if (typeConditions != null && typeConditions.Count > 0)
                {
                    foreach (SearchCondition condition in typeConditions)
                    {
                        CustomerType type = (CustomerType)condition.Value;
                        _customerTypes.Add(type);
                    }
                }
            }
            else
            {
                if (Type == null)
                {
                    _customerTypes = null;
                }
                else
                {
                    _customerTypes.Add(Type.Value);
                }
            }

            Guid? agentCustomerSolutionID = null;
            if (conditions != null && conditions.Contain("SolutionID"))
            {
                SearchCondition solutionIDCondition = conditions.GetValue("SolutionID");
                if (solutionIDCondition != null && solutionIDCondition.Value != null)
                    agentCustomerSolutionID = new Guid(solutionIDCondition.Value.ToString());
            }


            #region bug2972: 业务写订单时，选择客户，只能选择自己有权限的CRM关联的公共客户。
            if (conditions != null && conditions.Contain("IsFromOrder"))
            {
                SearchCondition isFromOrderCondition = conditions.GetValue("IsFromOrder");
                if (isFromOrderCondition != null && isFromOrderCondition.Value != null)
                    _isFromOrder = (bool)isFromOrderCondition.Value;
            }
            else
            {
                _isFromOrder = false;
            }

            if (conditions != null && conditions.Contain("CurruntUserID"))
            {
                SearchCondition curruntUserIDCondition = conditions.GetValue("CurruntUserID");
                if (curruntUserIDCondition != null && curruntUserIDCondition.Value != null)
                    _curruntUserID = new Guid(curruntUserIDCondition.Value.ToString());
            }
            else
            {
                _curruntUserID = null;
            }

            if (conditions != null && conditions.Contain("CurruntSalesID"))
            {
                SearchCondition curruntSalesIDCondition = conditions.GetValue("CurruntSalesID");
                if (curruntSalesIDCondition != null && curruntSalesIDCondition.Value != null)
                    _curruntSalesID = new Guid(curruntSalesIDCondition.Value.ToString());
            }
            else
            {
                _curruntSalesID = null;
            }

            _codeApplyState = null;
            if (conditions != null && conditions.Contain("CodeApplyState"))
            {
                SearchCondition codeApplyState = conditions.GetValue("CodeApplyState");
                if (codeApplyState != null)
                    _codeApplyState = (CustomerCodeApplyState)codeApplyState.Value;
            }

            #endregion

            #endregion

            List<CustomerInfo> list;
            if (triggerType == FinderTriggerType.ClickButton && string.IsNullOrEmpty(name))
            {
                //单击选择按钮时，如果没有输入查询时，则不进行搜索。只弹出查询对话框。
                list = new List<CustomerInfo>();
            }
            else
            {
                list = CustomerService.GetCustomerListBySearch(name, string.Empty, string.Empty, string.Empty, string.Empty
                                                                        , Guid.Empty, null
                                                                        , CustomerStateType.Valid
                                                                        , _customerTypes == null ? null : _customerTypes.ToArray()
                                                                        , IsAgentOfCarrier
                                                                        , _codeApplyState
                                                                       , null, agentCustomerSolutionID, null, null, _isFromOrder, _curruntUserID, 100);
            }

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(CommonUtility.GetSingleSearchResult<CustomerInfo>(list[0], returnFields)));

                return;
            }

            #endregion

            singleFinderWorkitem = Workitem.WorkItems.Get<CustomerSingleFinderWorkitem>(this.GetHashCode().ToString() + "CustomerSingleFinderWorkitem");
            if (singleFinderWorkitem == null)
            {
                singleFinderWorkitem = Workitem.WorkItems.AddNew<CustomerSingleFinderWorkitem>(this.GetHashCode().ToString() + "CustomerSingleFinderWorkitem");
                singleFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
                {
                    if (this.DataChoosed != null) DataChoosed(sender, e);
                };
            }

            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("Name", name);

            if (_customerTypes != null)
                initValues.Add("CustomerType", _customerTypes);

            if (IsAgentOfCarrier != null)
                initValues.Add("IsAgentOfCarrier", IsAgentOfCarrier);

            if (_solutionID != null)
                initValues.Add("SolutionID", _solutionID);

            initValues.Add("IsFromOrder", _isFromOrder);

            initValues.Add("CodeApplyState", _codeApplyState);

            if (_curruntUserID != null)
            {
                initValues.Add("CurruntUserID", _curruntUserID);
            }

            if (_curruntSalesID != null)
            {
                initValues.Add("CurruntSalesID", _curruntSalesID);
            }

            singleFinderWorkitem.Show(workspace, list, returnFields, initValues);
        }

        #endregion

        #region Many

        public void PickMany(string searchValue, string property, SearchConditionCollection conditions
            , string[] returnFields, FinderTriggerType triggerType
            , GetExistValueHandler getExistValueHandler, System.Windows.Forms.Control container)
        {
            DeckWorkspace workspce = this.Workitem.Workspaces.Get<DeckWorkspace>(CustomerFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                workspce = this.Workitem.Workspaces.AddNew<DeckWorkspace>(CustomerFinderWorkspace);
                container.Controls.Add(workspce);
                workspce.Dock = System.Windows.Forms.DockStyle.Fill;
                workspce.BringToFront();
            }
            this.PickMany(searchValue, property, conditions, returnFields, triggerType, getExistValueHandler, CustomerFinderWorkspace);
        }

        public void PickMany(string searchValue, string property
            , SearchConditionCollection conditions, string[] returnFields
            , FinderTriggerType triggerType, GetExistValueHandler getExistValueHandler, string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
                workspaceName = ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace;

            IWorkspace workspace = this.Workitem.Workspaces[workspaceName];

            #region

            #region Get Condition
            string name = string.Empty;
            if (triggerType == FinderTriggerType.KeyEnter)
            {
                name = searchValue;
            }
            _customerTypes = new List<CustomerType>();
            if (conditions != null && conditions.Contain("CustomerType"))
            {
                List<ICP.Framework.CommonLibrary.Client.SearchCondition> typeConditions
                    = conditions.GetValues("CustomerType");
                if (typeConditions != null && typeConditions.Count > 0)
                {
                    foreach (SearchCondition condition in typeConditions)
                    {
                        CustomerType type = (CustomerType)condition.Value;
                        _customerTypes.Add(type);
                    }
                }
            }
            else
            {
                if (Type == null)
                {
                    _customerTypes = null;
                }
                else
                {
                    _customerTypes.Add(Type.Value);
                }
            }

            if (conditions != null && conditions.Contain("SolutionID"))
            {
                SearchCondition solutionIDCondition = conditions.GetValue("SolutionID");
                if (solutionIDCondition != null && solutionIDCondition.Value != null)
                {
                    _solutionID = new Guid(solutionIDCondition.Value.ToString());
                }
            }

            #region bug2972: 业务写订单时，选择客户，只能选择自己有权限的CRM关联的公共客户。
            if (conditions != null && conditions.Contain("IsFromOrder"))
            {
                SearchCondition isFromOrderCondition = conditions.GetValue("IsFromOrder");
                if (isFromOrderCondition != null && isFromOrderCondition.Value != null)
                    _isFromOrder = (bool)isFromOrderCondition.Value;
            }
            else
            {
                _isFromOrder = false;
            }

            if (conditions != null && conditions.Contain("CurruntUserID"))
            {
                SearchCondition curruntUserIDCondition = conditions.GetValue("CurruntUserID");
                if (curruntUserIDCondition != null && curruntUserIDCondition.Value != null)
                    _curruntUserID = new Guid(curruntUserIDCondition.Value.ToString());
            }
            else
            {
                _curruntUserID = null;
            }


            if (conditions != null && conditions.Contain("CurruntSalesID"))
            {
                SearchCondition curruntSalesIDCondition = conditions.GetValue("CurruntUserID");
                if (curruntSalesIDCondition != null && curruntSalesIDCondition.Value != null)
                    _curruntSalesID = new Guid(curruntSalesIDCondition.Value.ToString());
            }
            else
            {
                _curruntSalesID = null;
            }

            //if (conditions != null && conditions.Contain("IsApproved"))
            //{
            //    SearchCondition isApprovedCondition = conditions.GetValue("IsApproved");
            //    if (isApprovedCondition != null && isApprovedCondition.Value != null)
            //        _isApproved = Convert.ToBoolean(isApprovedCondition.Value.ToString());
            //}
            //else
            //{
            //    _isApproved = false;
            //}
            #endregion

            #endregion

            List<CustomerInfo> list = CustomerService.GetCustomerListBySearch(name, string.Empty, string.Empty, string.Empty, string.Empty
                                                                    , Guid.Empty, null
                                                                    , CustomerStateType.Valid
                                                                    , _customerTypes == null ? null : _customerTypes.ToArray()
                                                                    , IsAgentOfCarrier, null, null, _solutionID, null, null, _isFromOrder, _curruntUserID, 100);

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(CommonUtility.GetMultiSearchResult<CustomerInfo>(list, returnFields)));

                return;
            }

              List<CustomerInfo> existList = new List<CustomerInfo>();
              if (getExistValueHandler != null)
              {
                  List<Guid> existValues = new List<Guid>();
                  IList exists = getExistValueHandler();
                  if (exists != null && exists.Count > 0)
                  {
                      foreach (var item in exists)
                      {
                          existValues.Add((Guid)item);
                      }
                  }

                  foreach (var item in list)
                  {
                      if (existValues.Contains(item.ID))
                          existList.Add(item);
                  }
              }
            #endregion

            multiFinderWorkitem = Workitem.WorkItems.Get<CustomerMultiFinderWorkitem>(this.GetHashCode().ToString() + "CustomerMultiFinderWorkitem");
            if (multiFinderWorkitem == null)
            {
                multiFinderWorkitem = Workitem.WorkItems.AddNew<CustomerMultiFinderWorkitem>(this.GetHashCode().ToString() + "CustomerMultiFinderWorkitem");
                multiFinderWorkitem.DataChoosed += delegate(object sender, DataFindEventArgs e)
                {
                    if (this.DataChoosed != null) DataChoosed(sender, e);
                };
            }

            Dictionary<string, object> initValues = new Dictionary<string, object>();
            initValues.Add("NAME", name);
            if (_customerTypes != null)
                initValues.Add("CustomerType", _customerTypes);

            if (IsAgentOfCarrier != null)
                initValues.Add("IsAgentOfCarrier", IsAgentOfCarrier);

            if (_solutionID != null)
                initValues.Add("SolutionID", _solutionID);

            initValues.Add("IsFromOrder", _isFromOrder);
            //initValues.Add("IsApproved", _isApproved);


            if (_curruntUserID != null)
            {
                initValues.Add("CurruntUserID", _curruntUserID);
            }

            if (_curruntSalesID != null)
            {
                initValues.Add("CurruntSalesID", _curruntSalesID);
            }

            multiFinderWorkitem.Show(workspace, list, existList, returnFields, initValues);
        }

        #endregion

        #endregion

        #region 属性

        protected virtual CustomerType? Type { get { return null; } }
        protected virtual bool? IsAgentOfCarrier { get { return null; } }

        private List<CustomerType> _customerTypes = null;
        private Guid? _solutionID = null;
        private bool _isFromOrder = false;
        private Guid? _curruntUserID = null;
        /// <summary>
        /// 当前揽货人
        /// </summary>
        private Guid? _curruntSalesID = null;
        private CustomerCodeApplyState? _codeApplyState = null;

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(Boolean isDisposing)
        {
            if (isDisposing)
            {
                this.DataChoosed = null;
                this.singleFinderWorkitem = null;
                this.multiFinderWorkitem = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            }
        }

        #endregion
    }

    public class CustomerAirlineFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Airline; } }
    }

    public class CustomerCarrierFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Carrier; } }
    }

    public class CustomerForwardingFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Forwarding; } }
    }

    public class CustomerAgentOfCarrierFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Forwarding; } }
        protected override bool? IsAgentOfCarrier { get { return true; } }
    }

    public class CustomerAgentFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Forwarding; } }
    }

    public class CustomerCustomsBrokerFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Broker; } }
    }

    public class CustomerTruckerFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Trucker; } }
    }

    public class CustomerWarehouseFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Warehouse; } }
    }

    public class CustomerStorageFinder : CustomerFinder
    {
        protected override CustomerType? Type { get { return CustomerType.Storage; } }
    }

    #region

    public class CustomerWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

        public const string SelectedListWorkspace = "SelectedListWorkspace";
        public const string SelectedToolBarWorkspace = "SelectedToolBarWorkspace";
    }

    public class CustomerCommonConstants
    {
        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Common_FinderConfirm = "Common_FinderConfirm";
        public const string Common_FindeSelect = "Common_FindeSelect";
        public const string Common_FinderRemove = "Common_FinderRemove";
        public const string Common_FinderRemoveAll = "Common_FinderRemoveAll";
        public const string Common_FinderAdd = "Common_FinderAdd";
        public const string Common_FinderEdit = "Common_FinderEdit";
    }

    #endregion
}
