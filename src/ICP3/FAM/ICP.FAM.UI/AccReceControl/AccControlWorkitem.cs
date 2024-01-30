using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.AccReceControl
{
    class AccControlWorkitem : WorkItem
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            AccControlMainWorkSpace mainSpce = SmartParts.Get<AccControlMainWorkSpace>("AccControlMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<AccControlMainWorkSpace>("AccControlMainWorkSpace");

                #region AddPart

                AccControlTool toolBar = SmartParts.AddNew<AccControlTool>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[AccControlWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                AccControlList listPart = SmartParts.AddNew<AccControlList>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[AccControlWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                AccControlSearch searchPart = SmartParts.AddNew<AccControlSearch>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[AccControlWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                AccControlLogList logListPart = SmartParts.AddNew<AccControlLogList>();
                IWorkspace logListWorkspace = (IWorkspace)Workspaces[AccControlWorkSpaceConstants.LogListWorkspace];
                logListWorkspace.Show(logListPart);

                CustomerDocumentList documentListPart = SmartParts.AddNew<CustomerDocumentList>();
                IWorkspace documentListWorkspace = (IWorkspace)Workspaces[AccControlWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);
                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Account Control" : "应收账款控制";
                mainWorkspace.Show(mainSpce, smartPartInfo);

                AccControlUIAdapter bookingAdapter = new AccControlUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(logListPart.GetType().Name, logListPart);
                dic.Add(documentListPart.GetType().Name, documentListPart);

                bookingAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }


    /// <summary>
    /// 命常量
    /// </summary>
    public class AccControlCommandConstants
    {
        public const string Command_AccControlMail = "Command_AccControlMail";

        public const string Command_AccControlEdit = "Command_AccControlEdit";

        public const string Command_AccControlCancel = "Command_AccControlCancel";

        public const string Command_AccControlRefreshData = "Command_AccControlRefreshData";

        public const string Command_AccControlMarkStatus = "Command_AccControlMarkStatus";

        public const string Command_AccControlCustomerPreference = "Command_AccControlCustomerPreference";
    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class AccControlWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string LogListWorkspace = "LogListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
    }

   public class SearchParameter
    {
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime EndingDate { get; set; }
        public List<Guid> CompanyIDs { get; set; }
        public List<BillType> BillTypes { get; set; }
        public List<OperationType> OperationTypes { get; set; }
        public Guid? CustomerID { get; set; }
        public string Currency { get; set; }
        public short SearchType { get; set; }
        public bool OnlyOverPaid { get; set; }
    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class AccControlUIAdapter : IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        AccControlList _mainListPart;
        AccControlLogList _logListPart;
        CustomerDocumentList _CustomerDocumentList;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(AccControlTool).Name];
            _searchPart = (ISearchPart)controls[typeof(AccControlSearch).Name];
            _mainListPart = (AccControlList)controls[typeof(AccControlList).Name];
            _logListPart = (AccControlLogList)controls[typeof(AccControlLogList).Name];
            _CustomerDocumentList = (CustomerDocumentList)controls[typeof(CustomerDocumentList).Name];
            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                if (data != null)
                {
                    CustomerAgingList listData = data as CustomerAgingList;

                    RefreshBarEnabled(_toolBar, listData);

                    _logListPart.BindDataList(listData.CustomerID, listData.CompanyID);
                    BusinessOperationContext boc = new BusinessOperationContext() {
                            CompanyID = listData.CompanyID,
                            CustomerID = listData.CustomerID,
                    };
                    _CustomerDocumentList.DataBind(boc);
                }
            };

            _mainListPart.Selected += delegate(object sender, object data)
            {

            };

            _mainListPart.InvokeGetData += delegate(object sender, object data)
            {
                _searchPart.RaiseSearched(data);
            };

            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            #endregion


            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                AccControlSearch search = sender as AccControlSearch;
                _mainListPart.DataSource = results;
                _mainListPart._SearchParameter = search._SearchParameter;
            };
            #endregion

            #endregion
        }

        void _mainListPart_KeyDown(object sender, KeyEventArgs e)
        {

            if (sender != null)
            {
                Dictionary<string, object> keyValue = sender as Dictionary<string, object>;
                if (keyValue != null)
                {
                    _searchPart.Init(keyValue);
                    _searchPart.RaiseSearched();
                }
            }
        }
        private void RefreshBarEnabled(IToolBar toolBar, CustomerAgingList listData)
        {
            if (listData == null)
            {
                //toolBar.SetEnable("barEdit", false);
                //toolBar.SetEnable("barCancel", false);
            }
            else
            {
                //toolBar.SetEnable("barEdit", true);
                //toolBar.SetEnable("barCancel", true);

                //if (listData.IsValid)
                //{
                //    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Invalid" : "作废");
                //}
                //else
                //{
                //    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Activation" : "激活");
                //}
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _CustomerDocumentList = null;
            _logListPart = null;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }

}
