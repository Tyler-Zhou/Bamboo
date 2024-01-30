using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using System.Collections;
using ISearchPart = ICP.Framework.ClientComponents.UIFramework.ISearchPart;
using ICP.Business.Common.UI.Document;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.UI.BankReceiptList
{
    class BankReceiptListWorkItem : WorkItem
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            Show();
        }

        private void Show()
        {
            BankReceiptListMainWorkSpace mainSpace = SmartParts.Get<BankReceiptListMainWorkSpace>("BankReceiptListMainWorkSpace");
            if (mainSpace == null)
            {
                mainSpace = SmartParts.AddNew<BankReceiptListMainWorkSpace>("BankReceiptListMainWorkSpace");

                #region AddPart

                BankReceiptListToolPart toolBar = SmartParts.AddNew<BankReceiptListToolPart>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[BankReceiptListWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BankReceiptListPart listPart = SmartParts.AddNew<BankReceiptListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[BankReceiptListWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BankReceiptListSearchPart searchPart = SmartParts.AddNew<BankReceiptListSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[BankReceiptListWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                //UCDocumentList documentListPart = SmartParts.AddNew<UCDocumentList>();
                //DocumentListPresenter documentPresenter = SmartParts.AddNew<DocumentListPresenter>();
                //documentPresenter.ucList = documentListPart;
                //documentListPart.Presenter = documentPresenter;
                //IWorkspace documentListWorkspace = (IWorkspace)Workspaces[BankReceiptListWorkSpaceConstants.DocumentListWorkspace];
                //documentListWorkspace.Show(documentListPart);

                CustomerDocumentList documentListPart = SmartParts.AddNew<CustomerDocumentList>();
                IWorkspace documentListWorkspace = (IWorkspace)Workspaces[BankReceiptListWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "BankReceipt List" : "水单列表";
                mainWorkspace.Show(mainSpace, smartPartInfo);

                BankReceiptListUIAdapter bookingAdapter = new BankReceiptListUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(documentListPart.GetType().Name, documentListPart);
                bookingAdapter.Init(dic);
            }
            else
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpace);
        }
    }

    /// <summary>
    /// 命令常量
    /// </summary>
    public class BankReceiptListCommandConstants
    {
        public const string Command_Add = "Receipt_Add";
        public const string Command_Edit = "Receipt_Edit";
        public const string Command_Cancel = "Receipt_Cancel";
        public const string Command_Auditor = "Receipt_Auditor";
        public const string Command_UnAuditor = "Receipt_UnAuditor";
        public const string CommandShowSearch = "Receipt_ShowSearch";

        public const string CommandRefreshData = "Receipt_RefreshData";
    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class BankReceiptListWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "BankReceiptList_ToolbarWorkspace";
        public const string SearchWorkspace = "BankReceiptList_SearchWorkspace";
        public const string ListWorkspace = "BankReceiptList_ListWorkspace";
        public const string DocumentListWorkspace = "BankReceiptList_DocumentListWorkspace";
    }

    public class BankReceiptListUIAdapter : IDisposable
    {
        #region Service

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        #endregion

        #region Parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        BankReceiptListPart _mainListPart;
        BankReceiptListEditPart _editPart;
        CustomerDocumentList _CustomerDocumentList;
        #endregion

        #region Interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(BankReceiptListToolPart).Name];
            _searchPart = (ISearchPart)controls[typeof(BankReceiptListSearchPart).Name];
            _mainListPart = (BankReceiptListPart)controls[typeof(BankReceiptListPart).Name];
            //_editPart = (BankReceiptListEditPart)controls[typeof(BankReceiptListEditPart).Name];

            _CustomerDocumentList = (CustomerDocumentList)controls[typeof(CustomerDocumentList).Name];
            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                if (data != null)
                {
                    BankReceiptListInfo listData = data as BankReceiptListInfo;
                    RefreshBarEnabled(_toolBar, listData);

                    BusinessOperationContext boc = new BusinessOperationContext()
                    {
                        OperationNO = listData.No,
                        CompanyID = listData.CompanyID,
                        CustomerID = listData.ID
                    };
                    _CustomerDocumentList.DataBind(boc);
                }
            };

            _mainListPart.KeyDown += new KeyEventHandler(mainListPart_KeyDown);

            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                BankReceiptListSearchPart search = sender as BankReceiptListSearchPart;
                _mainListPart.DataSource = results;
                //_mainListPart._SearchParameter = search._SearchParameter;
            };
            #endregion

            #endregion
        }

        void mainListPart_KeyDown(object sender, KeyEventArgs e)
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

        #region 按钮是否可用
        private void RefreshBarEnabled(IToolBar toolBar, BankReceiptListInfo listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barDelete", false);
                toolBar.SetEnable("bbiAudit", false);
                toolBar.SetEnable("bbiCancelAudit", false);
            }
            else
            {
                if (listData.Status == (BankReceiptStatus)1)
                {
                    toolBar.SetEnable("barEdit", true);
                    toolBar.SetEnable("bbiAudit", true);
                    toolBar.SetEnable("barDelete", true);
                    toolBar.SetEnable("bbiCancelAudit", false);
                }
                else if (listData.Status == (BankReceiptStatus)2)
                {
                    toolBar.SetEnable("bbiCancelAudit", true);
                    toolBar.SetEnable("barEdit", false);
                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("bbiAudit", false);
                }
                else if (listData.Status == (BankReceiptStatus)3)
                {
                    toolBar.SetEnable("barEdit", false);
                    toolBar.SetEnable("barDelete", false);
                    toolBar.SetEnable("bbiAudit", false);
                    toolBar.SetEnable("bbiCancelAudit", false);
                }
                else
                {
                    MessageBox.Show(LocalData.IsEnglish ? "Not found" : "没找到" + listData.Status);
                }


                if (listData.IsValid)
                {
                    toolBar.SetText("barDelete", LocalData.IsEnglish ? "Invalid" : "作废");
                }
                else
                {
                    toolBar.SetText("barDelete", LocalData.IsEnglish ? "Activation" : "激活");
                }
            }
        }
        #endregion
        

        private void DisableToolBar()
        {
            _toolBar.SetEnable("barDelete", false);
            _toolBar.SetEnable("btnCashierChecked", false);
            _toolBar.SetEnable("btnCashierUnChecked", false);
            _toolBar.SetEnable("btnFMChecked", false);
            _toolBar.SetEnable("btnFMUnChecked", false);
            _toolBar.SetEnable("btnAduited", false);
            _toolBar.SetEnable("btnUnAduited", false);
            _toolBar.SetEnable("btnCancelAccounts", false);
        }


        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _mainListPart.KeyDown -= mainListPart_KeyDown;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;

        }

        #endregion
    }
}
