using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.Memolist;
using ICP.FCM.Common.UI.FaxEMailLog;
using ICP.MailCenter.CommonUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 其他业务
    /// </summary>
    public class OBWorkitem : WorkItem   //业务管理
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
            OBMainWorkspace mainSpce = this.SmartParts.Get<OBMainWorkspace>("OBMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<OBMainWorkspace>("OBMainWorkSpace");

                #region AddPart
                OBMainToolBar toolBar = this.SmartParts.AddNew<OBMainToolBar>();
                toolBar.addType = AddType.OtherBusiness;
                IWorkspace ToolbarWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.ToolbarWorkspace];
                ToolbarWorkspace.Show(toolBar);

                OBListPart listPart = this.SmartParts.AddNew<OBListPart>();
                listPart.addType = AddType.OtherBusiness;
                IWorkspace ListWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.ListWorkspace];
                ListWorkspace.Show(listPart);



                OBSearchPart searchPart = this.SmartParts.AddNew<OBSearchPart>();
                searchPart.addType = AddType.OtherBusiness;
                IWorkspace SearchWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.SearchWorkspace];
                SearchWorkspace.Show(searchPart);


                OBFastSearchPart fastSearchPart = this.SmartParts.AddNew<OBFastSearchPart>();
                IWorkspace FastSearchWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.FastSearchWorkspace];
                FastSearchWorkspace.Show(fastSearchPart);

                ICP.FCM.Common.UI.Memolist.MemoListPart memoListPart = this.Items.AddNew<ICP.FCM.Common.UI.Memolist.MemoListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.MemoListWorkspace];
                memoListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailListPart = this.Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailListPart);


                UCDocumentList documentPart = this.Items.AddNew<UCDocumentList>();
                IWorkspace documentPartWorkSapce = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.DocumentListWorkspace];
                documentPartWorkSapce.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Other Business" : "其他业务";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OBUIAdapter orderAdapter = new OBUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(faxMailListPart.GetType().Name, faxMailListPart);
                dic.Add(documentPart.GetType().Name, documentPart);


                orderAdapter.Init(dic, documentPresenter);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class OBBookingWorkitem : WorkItem   //订单
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
            OBMainWorkspace mainSpce = this.SmartParts.Get<OBMainWorkspace>("OBMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<OBMainWorkspace>("OBMainWorkSpace");

                #region AddPart
                OBOrderMainToolBar toolBar = this.SmartParts.AddNew<OBOrderMainToolBar>();
                toolBar.addType = AddType.OtherBusiness;
                IWorkspace ToolbarWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.ToolbarWorkspace];
                ToolbarWorkspace.Show(toolBar);

                OBListPart listPart = this.SmartParts.AddNew<OBListPart>();
                listPart.addType = AddType.OtherBusinessOrder;
                IWorkspace ListWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.ListWorkspace];
                ListWorkspace.Show(listPart);



                OBOrderSearchPart searchPart = this.SmartParts.AddNew<OBOrderSearchPart>();
                searchPart.addType = AddType.OtherBusinessOrder;
                IWorkspace SearchWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.SearchWorkspace];
                //searchPart.Workitem.SetEnable("barEdit", false)
                SearchWorkspace.Show(searchPart);


                OBFastSearchPart fastSearchPart = this.SmartParts.AddNew<OBFastSearchPart>();
                IWorkspace FastSearchWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.FastSearchWorkspace];
                FastSearchWorkspace.Show(fastSearchPart);



                ICP.FCM.Common.UI.Memolist.MemoListPart memoListPart = this.Items.AddNew<ICP.FCM.Common.UI.Memolist.MemoListPart>();
                IWorkspace memoListWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.MemoListWorkspace];
                memoListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailEDIListPart = this.Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);


                UCDocumentList ucDocumentList = this.Items.AddNew<UCDocumentList>();
                IWorkspace documentListWorkSpace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkSpace.Show(ucDocumentList);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Other Business Order" : "其他业务订单";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OBOrderUIAdapter orderAdapter = new OBOrderUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(fastSearchPart.GetType().Name, fastSearchPart);
                dic.Add(memoListPart.GetType().Name, memoListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);
                dic.Add(ucDocumentList.GetType().Name, ucDocumentList);

                orderAdapter.Init(dic, documentPresenter);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class OBCommandConstants
    {
        public const string Command_AddData = "Command_AddData";
        public const string Command_CancelData = "Command_CancelData";
        public const string Command_EditData = "Command_EditData";
        public const string Command_Download = "Command_Download";
        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Command_Remark = "Command_Remark";
        public const string Command_FaxLog = "Command_FaxLog";
        public const string Command_Bill = "Command_Bill";
        public const string Command_Close = "Command_Close";
        public const string Command_VerifiSheet = "Command_VerifiSheet";//核销单
        public const string Command_Print = "Command_Print";
        public const string Command_Document = "Command_Document";
        public const string Command_FaxEmail = "Command_FaxEmail";
        public const string Command_Memo = "Command_Memo";
        public const string Command_SendEmail = "Command_SendEmail";
        public const string Command_RefreshData = "Command_RefreshData";
        public const string Command_Truck = "Command_Truck";

        public const string Command_ShowChildWorkspace = "Command_ShowChildWorkspace";
    }

    public class OBWorkSpaceConstants
    {
        public const string ToolbarWorkspace = "ToolbarWorkspace";
        public const string MainWorkSpace = "MainWorkSpace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        public const string BaseEditWorkSpace = "BaseEditWorkSpace";
        public const string ChildWorkspace = "ChildWorkspace";
        public const string MemoListWorkspace = "MemoListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";

    }

    public class OrderStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class OBUIAdapter //业务
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        OBListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _memolistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _DocumentListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls, DocumentListPresenter documentPresenter)
        {

            _toolBar = (IToolBar)controls[typeof(OBMainToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OBSearchPart).Name];
            _mainListPart = (OBListPart)controls[typeof(OBListPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OBFastSearchPart).Name];
            _memolistPart = (IListPart)controls[typeof(ICP.FCM.Common.UI.Memolist.MemoListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _DocumentListPart = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OtherBusinessList listData = data as OtherBusinessList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    MemoParam para = new MemoParam();
                    para.OperationId = listData.ID;
                    para.FormID = listData.ID;
                    para.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
                    para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.Other;
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
        private void RefreshBarEnabled(IToolBar toolBar, OtherBusinessList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barBill", false);
                toolBar.SetEnable("barVerifiSheet", false);
                toolBar.SetEnable("barRefresh", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barCancel", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barBill", true);
                toolBar.SetEnable("barVerifiSheet", true);
                toolBar.SetEnable("barRefresh", true);
            }
        }

        #endregion
    }

    public class OBOrderUIAdapter   //订单
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        OBListPart _mainListPart;
        ISearchPart _fastSearchPart;
        IListPart _memolistPart;
        UCCommunicationHistory _faxMailEDIListPart;
        UCDocumentList _ucDocumentList;
        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls, DocumentListPresenter documentPresenter)
        {

            _toolBar = (IToolBar)controls[typeof(OBOrderMainToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OBOrderSearchPart).Name];
            _mainListPart = (OBListPart)controls[typeof(OBListPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OBFastSearchPart).Name];
            _memolistPart = (IListPart)controls[typeof(ICP.FCM.Common.UI.Memolist.MemoListPart).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];
            _ucDocumentList = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OtherBusinessList listData = data as OtherBusinessList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                if (listData != null)
                {
                    MemoParam para = new MemoParam();
                    para.OperationId = listData.ID;
                    para.FormID = listData.ID;
                    para.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
                    para.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.Other;
                    _memolistPart.DataSource = para;

                    ICP.FCM.Common.UI.Utility.SetCommunicationDataSource(_faxMailEDIListPart, para);
                    ICP.FCM.Common.UI.Utility.SetDocumentListDataSource(_ucDocumentList, documentPresenter, para);
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
        private void RefreshBarEnabled(IToolBar toolBar, OtherBusinessList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barEdit", false);
                toolBar.SetEnable("barCopy", false);
                toolBar.SetEnable("barCancel", false);
                toolBar.SetEnable("barPrint", false);
                toolBar.SetEnable("barBill", false);
                toolBar.SetEnable("barRefresh", false);
            }
            else
            {
                toolBar.SetEnable("barEdit", true);
                toolBar.SetEnable("barCopy", true);
                toolBar.SetEnable("barCancel", true);
                toolBar.SetEnable("barPrint", true);
                toolBar.SetEnable("barBill", true);
                toolBar.SetEnable("barRefresh", true);
            }
        }

        #endregion
    }
}
