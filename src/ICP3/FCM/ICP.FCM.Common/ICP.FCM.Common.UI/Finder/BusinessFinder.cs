using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.UI.Finder
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessFinder : IDataFinder
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IUserService userService { get; set; }

        [ServiceDependency]
        public IFCMCommonService fcmCommonService { get; set; }

        #endregion
             
        #region 属性

        protected virtual List<OperationType> OperationTypes { get { return null; } }

        private List<OperationType> _operationTypes = null;

        #endregion

        #region 缓存的控件

        IToolBar _toolBar;
        ISearchPart _searchPart;
        BaseListPart _mainListPart;
        List<Guid> _selectedData;
        bool _isMulti = false;
        private const string BusinessFinderWorkspace = "BusinessFinderWorkspace";
       
        /// <summary>
        /// 缓存一个公司列表
        /// </summary>
        List<OrganizationList> _userCompanyList = null;
        string[] _returnFields;

        #endregion

        #region

        private void Show(string searchValue, string property
                            , SearchConditionCollection conditions
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , Control container)
        {
            DeckWorkspace workspce = Workitem.Workspaces.Get<DeckWorkspace>(BusinessFinderWorkspace);
            if (workspce == null || workspce.IsDisposed)
            {
                if (Workitem.Workspaces.Contains(BusinessFinderWorkspace))
                {
                    Workitem.Workspaces.Remove(workspce);
                }

                workspce = Workitem.Workspaces.AddNew<DeckWorkspace>(BusinessFinderWorkspace);
                workspce.Dock = DockStyle.Fill;
                workspce.BringToFront();
            }
            container.Controls.Clear();
            container.Controls.Add(workspce);
            Show(searchValue, property, conditions, triggerType, getExistValueHandler,BusinessFinderWorkspace);
        }

        private void Show(string searchValue, string property
                            , SearchConditionCollection conditions
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , string workspaceName)
        {
            if (string.IsNullOrEmpty(workspaceName))
            {
                XtraForm form = new XtraForm();
                form.Height = 600;
                form.Width = 800;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Text = LocalData.IsEnglish ? "Business Finder" : "选择业务";
                Show(searchValue, property, conditions, triggerType, getExistValueHandler, form);
            }
            else
            {
                BulidFinderByWorkspaceName(searchValue, property, conditions, triggerType, workspaceName);
            }

           
        }

        private void BulidFinderByWorkspaceName(string searchValue, string property, SearchConditionCollection conditions, FinderTriggerType triggerType, string workspaceName)
        {
            string businessNo = searchValue;
            WorkItem tempWorkitem = Workitem.WorkItems.Get<WorkItem>(BusinessFinderConstants.BUSINESSFINDERTEMPWORKITEM);
            if (tempWorkitem == null)
            {
                tempWorkitem = Workitem.WorkItems.AddNew<WorkItem>(BusinessFinderConstants.BUSINESSFINDERTEMPWORKITEM);
                #region Show
                BusinessFinderWorkspace mainSpce = tempWorkitem.SmartParts.Get<BusinessFinderWorkspace>("BusinessFinderWorkspace");
                if (mainSpce == null)
                {
                    mainSpce = tempWorkitem.SmartParts.AddNew<BusinessFinderWorkspace>("BusinessFinderWorkspace");

                    #region AddPart

                    BusinessFinderToolBar toolBar = tempWorkitem.SmartParts.AddNew<BusinessFinderToolBar>();
                    IWorkspace toolBarWorkspace = (IWorkspace)tempWorkitem.Workspaces[BusinessFinderConstants.ToolBarWorkspace];
                    toolBarWorkspace.Show(toolBar);

                    BusinessListPart listPart = tempWorkitem.SmartParts.AddNew<BusinessListPart>();

                    IWorkspace listWorkspace = (IWorkspace)tempWorkitem.Workspaces[BusinessFinderConstants.ListWorkspace];
                    listWorkspace.Show(listPart);

                    BusinessSearchPart searchPart = tempWorkitem.SmartParts.AddNew<BusinessSearchPart>();

                    IWorkspace searchWorkspace = (IWorkspace)tempWorkitem.Workspaces[BusinessFinderConstants.SearchWorkspace];
                    searchWorkspace.Show(searchPart);

                    #endregion

                    IWorkspace mainWorkspace = tempWorkitem.Workspaces[workspaceName];
                    SmartPartInfo smartPartInfo = new SmartPartInfo();
                    smartPartInfo.Title = LocalData.IsEnglish ? "Business Finder" : "选择业务";
                    mainWorkspace.Show(mainSpce, smartPartInfo);

                    #region
                    mainSpce.Disposed += delegate
                    {
                        tempWorkitem.Dispose();
                    };

                    _toolBar = toolBar;
                    _searchPart = searchPart;
                    _mainListPart = listPart;

                    _searchPart.OnSearched += delegate(object sender, object results)
                    {
                        _mainListPart.DataSource = results;
                    };
                    #endregion
                }
                else
                {
                    tempWorkitem.Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
                }
                #endregion
            }
            else
            {
                tempWorkitem.Activate();
            }

            #region  BulidList

            Dictionary<string, object> initValues = new Dictionary<string, object>
            {
                {"BusinessNo", businessNo},
                {"OperationTypes", OperationTypes},
                {"IsMulti", _isMulti}
            };


            _mainListPart.Init(initValues);
            #endregion

            _searchPart.InitialValues(searchValue, property, conditions, triggerType);
            _searchPart.RaiseSearched();
        }

        #endregion

        #region Workitem Common
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BusinessFinderConstants.Command_FinderConfirm)]
        public void Command_FinderConfirm(object sender, EventArgs e)
        {
            if (DataChoosed != null)
            {
                if (_isMulti)
                {
                    List<BusinessData> list = _mainListPart.DataSource as List<BusinessData>;
                    if (list != null)
                    {
                        DataChoosed(this, new DataFindEventArgs(list.ToArray()));
                        _mainListPart.FindForm().Close();
                    }
                }
                else
                {
                    BusinessData data = _mainListPart.Current as BusinessData;

                    if (data != null)
                    {
                        DataChoosed(this, new DataFindEventArgs(FCMUIUtility.GetSingleSearchResult<BusinessData>(data, _returnFields)));
                        _mainListPart.FindForm().Close();
                    }
                }         
            }
        }

        #endregion

        #region IDataFinder 成员
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DataFindEventArgs> DataChoosed;

        #region One

        public void PickOne(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields
                            , FinderTriggerType triggerType
                            , Control container)
        {
            _returnFields = returnFields;
            Show(searchValue, property, conditions, triggerType, null, container);
        }

        public void PickOne(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields
                            , FinderTriggerType triggerType
                            , string workspaceName)
        {
            #region

            string businessNo = searchValue;
            _returnFields = returnFields;
            if (_userCompanyList == null) _userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);

            List<BusinessData> list = fcmCommonService.GetBusinessListForDataFinder(OperationTypes == null ? null : OperationTypes.ToArray(),
                                                                                              _userCompanyList.Select(item => item.ID).ToArray(),
                                                                                               businessNo,
                                                                                               string.Empty,
                                                                                               string.Empty,
                                                                                               string.Empty,
                                                                                               string.Empty,
                                                                                               null,
                                                                                               true,
                                                                                               null,
                                                                                               DateSearchTypeForDataFinder.All,
                                                                                               null,
                                                                                               null,
                                                                                               100);

            if (list != null && list.Count == 1)
            {
                if (DataChoosed != null)
                    DataChoosed(this, new DataFindEventArgs(FCMUIUtility.GetSingleSearchResult<BusinessData>(list[0], returnFields)));

                return;
            }

            #endregion

            if (string.IsNullOrEmpty(workspaceName))
            {
                XtraForm form = new XtraForm
                {
                    Height = 600,
                    Width = 800,
                    StartPosition = FormStartPosition.CenterScreen,
                    Text = LocalData.IsEnglish ? "Business Finder" : "选择业务"
                };
                Show(searchValue, property, conditions, triggerType, null, form);
            }
            else
            {
                //string businessNo = searchValue;
                WorkItem tempWorkitem = Workitem.WorkItems.Get<WorkItem>(BusinessFinderConstants.BUSINESSFINDERTEMPWORKITEM);
                if (tempWorkitem == null)
                {
                    tempWorkitem = Workitem.WorkItems.AddNew<WorkItem>(BusinessFinderConstants.BUSINESSFINDERTEMPWORKITEM);
                    #region Show
                    BusinessFinderWorkspace mainSpce = tempWorkitem.SmartParts.Get<BusinessFinderWorkspace>("BusinessFinderWorkspace");
                    if (mainSpce == null)
                    {
                        mainSpce = tempWorkitem.SmartParts.AddNew<BusinessFinderWorkspace>("BusinessFinderWorkspace");

                        #region AddPart

                        BusinessFinderToolBar toolBar = tempWorkitem.SmartParts.AddNew<BusinessFinderToolBar>();
                        IWorkspace toolBarWorkspace = (IWorkspace)tempWorkitem.Workspaces[BusinessFinderConstants.ToolBarWorkspace];
                        toolBarWorkspace.Show(toolBar);

                        BusinessListPart listPart = tempWorkitem.SmartParts.AddNew<BusinessListPart>();

                        IWorkspace listWorkspace = (IWorkspace)tempWorkitem.Workspaces[BusinessFinderConstants.ListWorkspace];
                        listWorkspace.Show(listPart);

                        BusinessSearchPart searchPart = tempWorkitem.SmartParts.AddNew<BusinessSearchPart>();

                        IWorkspace searchWorkspace = (IWorkspace)tempWorkitem.Workspaces[BusinessFinderConstants.SearchWorkspace];
                        searchWorkspace.Show(searchPart);

                        #endregion

                        IWorkspace mainWorkspace = tempWorkitem.Workspaces[workspaceName];
                        SmartPartInfo smartPartInfo = new SmartPartInfo();
                        smartPartInfo.Title = LocalData.IsEnglish ? "Business Finder" : "选择业务";
                        mainWorkspace.Show(mainSpce, smartPartInfo);

                        #region
                        mainSpce.Disposed += delegate
                        {
                            tempWorkitem.Dispose();
                        };

                        _toolBar = toolBar;
                        _searchPart = searchPart;
                        _mainListPart = listPart;

                        _searchPart.OnSearched += delegate(object sender, object results)
                        {
                            _mainListPart.DataSource = results;
                        };
                        #endregion
                    }
                    else
                    {
                        tempWorkitem.Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
                    }
                    #endregion
                }
                else
                {
                    tempWorkitem.Activate();
                }

                #region  BulidList

                Dictionary<string, object> initValues = new Dictionary<string, object>
                {
                    {"BusinessNo", businessNo},
                    {"OperationTypes", OperationTypes},
                    {"IsMulti", _isMulti}
                };
                _mainListPart.Init(initValues);
                _searchPart.Init(initValues);
                #endregion

                if (list != null) _mainListPart.DataSource = list;   
            }
        }

        #endregion

        #region Many

        public void PickMany(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields
                            , FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , Control container)
        {
            _isMulti = true;
            _returnFields = returnFields;
            Show(searchValue, property, conditions, triggerType, null, container);
        }

        public void PickMany(string searchValue, string property
                            , SearchConditionCollection conditions
                            , string[] returnFields, FinderTriggerType triggerType
                            , GetExistValueHandler getExistValueHandler
                            , string workspaceName)
        {
            _isMulti = true;
            _returnFields = returnFields;
            Show(searchValue, property, conditions, triggerType, null, workspaceName);
        }

        #endregion

        #endregion
        
    }

    /// <summary>
    /// 搜索业务类型为海出，空出
    /// </summary>
    public class BusinessFinderForOEAE : BusinessFinder
    {
        /// <summary>
        /// 
        /// </summary>
        protected override List<OperationType> OperationTypes
        {
            get
            {
                List<OperationType> types = new List<OperationType> {OperationType.OceanExport, OperationType.AirExport};
                return types;
            }
        }
    }

    /// <summary>
    /// 搜索业务类型为海出，空出，其它业务的业务
    /// </summary>
    public class BusinessFinderForOEAEOB : BusinessFinder
    {
        /// <summary>
        /// 
        /// </summary>
        protected override List<OperationType> OperationTypes 
        { 
            get 
            {
                List<OperationType> types = new List<OperationType>
                {
                    OperationType.OceanExport,
                    OperationType.AirExport,
                    OperationType.Other
                };
                return types; 
            } 
        }
    }

    /// <summary>
    /// 搜索业务类型为海进的业务
    /// </summary>
    public class BusinessFinderForOI : BusinessFinder
    {
        /// <summary>
        /// 
        /// </summary>
        protected override List<OperationType> OperationTypes
        {
            get
            {
                List<OperationType> types = new List<OperationType> {OperationType.OceanImport};
                return types;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class BusinessFinderConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string SearchWorkspace = "SearchWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string ListWorkspace = "ListWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string BUSINESSFINDERTEMPWORKITEM = "BUSINESSFINDERTEMPWORKITEM";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_FinderConfirm = "Command_FinderConfirm";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_ShowSearch = "Command_ShowSearch";
    }
}
