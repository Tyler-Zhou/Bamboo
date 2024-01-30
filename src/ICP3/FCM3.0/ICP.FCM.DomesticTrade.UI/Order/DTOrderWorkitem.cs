using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.MailCenter.CommonUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FCM.DomesticTrade.UI.Order
{
    public class DTOrderWorkitem : WorkItem
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DocumentListPresenter documentPresenter { get; set; }

        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            DTOrderMainWorkspace mainSpce = this.SmartParts.Get<DTOrderMainWorkspace>("DTOrderMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<DTOrderMainWorkspace>("DTOrderMainWorkspace");

                #region AddPart

                DTOrderToolBar toolBar = this.SmartParts.AddNew<DTOrderToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[DTOrderWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                DTOrderListPart listPart = this.SmartParts.AddNew<DTOrderListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[DTOrderWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                OrderSearchPart searchPart = this.SmartParts.AddNew<OrderSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[DTOrderWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                OrderFastSearchPart fastSearchPart = this.SmartParts.AddNew<OrderFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[DTOrderWorkSpaceConstants.FastSearchWorkspace];
                fastSearchPartWorkspace.Show(fastSearchPart);

                ICP.FCM.Common.UI.Memolist.MemoListPart memoListPart = this.Items.AddNew<ICP.FCM.Common.UI.Memolist.MemoListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[DTOrderWorkSpaceConstants.MemoListWorkspace];
                memoListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailEDIListPart = this.Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[DTOrderWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);

                UCDocumentList documentPart = this.Items.AddNew<UCDocumentList>();
                IWorkspace documentPartWorkspace = (IWorkspace)this.Workspaces[DTOrderWorkSpaceConstants.DocumentListWorkspace];
                documentPartWorkspace.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "DT_Order" : "业务订单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                DTOrderUIAdapter orderAdapter = new DTOrderUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(documentPart.GetType().Name,documentPart);

                orderAdapter.Init(dic, documentPresenter);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class DTOrderCommandConstants
    {
        public const string Command_AddData = "Command_AddData";
        public const string Command_CancelData = "Command_CancelData";
        public const string Command_EditData = "Command_EditData";
        public const string Command_ViewReason = "Command_ViewReason";
        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Command_CopyData = "Command_CopyData";
        public const string Command_Print = "Command_Print";
        public const string Command_Document = "Command_Document";
        public const string Command_FaxEmail = "Command_FaxEmail";
        public const string Command_Memo = "Command_Memo";
        public const string Command_SendEmail = "Command_SendEmail";
        public const string Command_RefreshData = "Command_RefreshData";

        public const string Command_ShowChildWorkspace = "Command_ShowChildWorkspace";
    }

    public class DTOrderWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string MemoListWorkspace = "MemoListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";

    }

    public class DTOrderStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class DTOrderUIAdapter
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        #endregion

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        DTOrderListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _memolistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls, DocumentListPresenter documentPresenter)
        {

            _toolBar = (IToolBar)controls[typeof(DTOrderToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OrderSearchPart).Name];
            _mainListPart = (DTOrderListPart)controls[typeof(DTOrderListPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OrderFastSearchPart).Name];
            _memolistPart = (IListPart)controls[typeof(ICP.FCM.Common.UI.Memolist.MemoListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];


            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                DTOrderList listData = data as DTOrderList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);
                if (listData != null)
                {
                    MemoParam para = new MemoParam();
                    para.OperationId = listData.ID;
                    para.FormID = listData.ID;
                    para.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
                    para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.Internal;
                    _memolistPart.DataSource = para;
                    //设置沟通历史记录数据源
                    ICP.FCM.Common.UI.Utility.SetCommunicationDataSource(_faxMailEDIListPart, para);
                    //设置文档中心数据源
                    ICP.FCM.Common.UI.Utility.SetDocumentListDataSource(_DocumentListPart, documentPresenter, para);
                }

                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion
            };
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #region _fastSearchPart.OnSearched
            _fastSearchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #endregion

            //_searchPart.RaiseSearched();

            _mainListPart.KeyDown += new System.Windows.Forms.KeyEventHandler(_mainListPart_KeyDown);

            //_fastSearchPart.RaiseSearched();
        }

        void _mainListPart_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
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
        private void RefreshBarEnabled(IToolBar toolBar, DTOrderList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barShowSearch", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barDocument", false);
                toolBar.SetEnable("barFaxEmail", false);
                toolBar.SetEnable("barMemo", false);
                toolBar.SetEnable("barSendMail", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barViewReason", false);
            }
            else
            {

                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barShowSearch", true);
                toolBar.SetEnable("barCopy", listData.SalesName == LocalData.UserInfo.LoginName);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barDocument", true);
                toolBar.SetEnable("barFaxEmail", true);
                toolBar.SetEnable("barMemo", true);
                toolBar.SetEnable("barSendMail", true);

                if (listData.State != DTOrderState.NewOrder)
                    toolBar.SetEnable("barCancel", false);
                else
                    toolBar.SetEnable("barCancel", true);

                if (listData.IsValid == false)
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                else
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "取消(&D)");

                if (listData.State == DTOrderState.Rejected)
                    toolBar.SetEnable("barViewReason", true);
                else
                    toolBar.SetEnable("barViewReason", false);
            }
        }

        #endregion
    }
}
