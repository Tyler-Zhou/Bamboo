using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.Drawing;
using System;
using ICP.Business.Common.UI.EventList;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI.Communication;

namespace ICP.FAM.UI.ReleaseBL
{
    public class ReleaseBLWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();

            //Guid id = new Guid("d3cb47ac-39a9-e011-9d3b-001321cc6d9f");
            //ICP.FCM.Common.ServiceInterface.DataObjects.OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(id, OperationType.OceanExport);

            //finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
        }

        private void Show()
        {
            ReleaseBLMainWorkspace mainSpace = SmartParts.Get<ReleaseBLMainWorkspace>("ReleaseBLMainWorkspace");

            if (mainSpace == null)
            {
                mainSpace = Items.AddNew<ReleaseBLMainWorkspace>("ReleaseBLMainWorkspace");

                #region AddPart

                ReleaseBLToolBar toolBar = Items.AddNew<ReleaseBLToolBar>();
                IWorkspace toolBarSpace = Workspaces[ReleaseBLWorkSpaceConstants.ToolBarWorkspace];
                toolBarSpace.Show(toolBar);

                ReleaseBLSearchPart searchPart = Items.AddNew<ReleaseBLSearchPart>();
                IWorkspace searchSpace = Workspaces[ReleaseBLWorkSpaceConstants.SearchWorkspace];
                searchSpace.Show(searchPart);

                ReleaseBLListPart listPart = Items.AddNew<ReleaseBLListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[ReleaseBLWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                ReleaseBLBillListPart billlistPart = Items.AddNew<ReleaseBLBillListPart>();
                IWorkspace billlistWorkspace = (IWorkspace)Workspaces[ReleaseBLWorkSpaceConstants.BillListWorkspace];
                billlistWorkspace.Show(billlistPart);

                ReleaseBLDebtListPart debtlistPart = Items.AddNew<ReleaseBLDebtListPart>();
                IWorkspace debtlistWorkspace = (IWorkspace)Workspaces[ReleaseBLWorkSpaceConstants.DebtWorkspace];
                debtlistWorkspace.Show(debtlistPart);

                FollowingBusinessListPart followingBusinessListPart = Items.AddNew<FollowingBusinessListPart>();
                IWorkspace nextJobslistWorkspace = (IWorkspace)Workspaces[ReleaseBLWorkSpaceConstants.NextJobsWorkspace];
                nextJobslistWorkspace.Show(followingBusinessListPart);

                EventListPart eventListPart = Items.AddNew<EventListPart>();
                IWorkspace memoListPartspace = (IWorkspace)Workspaces[ReleaseBLWorkSpaceConstants.EventWorkspace];
                memoListPartspace.Show(eventListPart);

                ContactListPart contactlistPart = Items.AddNew<ContactListPart>();
                IWorkspace contactWorkspace = (IWorkspace)Workspaces[ReleaseBLWorkSpaceConstants.ContactWorkspace];
                contactWorkspace.Show(contactlistPart);

                UCDocumentList documentListPart = Items.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentListPart;
                documentListPart.Presenter = documentPresenter;
                IWorkspace documentListWorkspace = (IWorkspace)Workspaces[ReleaseBLWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);

                UCCommunicationHistory faxMailEDIListPart = Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = (IWorkspace)Workspaces[ReleaseBLWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);
                #endregion

                ReleaseBLUIAdapter adapter = new ReleaseBLUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(listPart.GetType().Name, listPart);

                dic.Add(eventListPart.GetType().Name, eventListPart);
                dic.Add(billlistPart.GetType().Name, billlistPart);
                dic.Add(debtlistPart.GetType().Name, debtlistPart);
                dic.Add(followingBusinessListPart.GetType().Name, followingBusinessListPart);
                dic.Add(contactlistPart.GetType().Name, contactlistPart);
                dic.Add(documentListPart.GetType().Name,documentListPart);
                dic.Add(faxMailEDIListPart.GetType().Name, faxMailEDIListPart);

                adapter.Init(dic);

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "ReleaseBL" : "放单列表";
                mainWorkspace.Show(mainSpace, smartPartInfo);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpace);
            }
        }
    }

    public class ReleaseBLCommondConstants
    {
        /// <summary>
        /// 编辑
        /// </summary>
        public const string Commond_Edit = "Commond_Edit";
        /// <summary>
        /// 已签收
        /// </summary>
        public const string Commond_Received = "Commond_Received";
        /// <summary>
        /// 已申请
        /// </summary>
        public const string Commond_Apply = "Commond_Apply";

        /// <summary>
        /// 已申请
        /// </summary>
        public const string Commond_CancelApply = "Commond_CancelApply";
        
        /// <summary>
        /// 放单
        /// </summary>
        public const string Commond_ReleaseBL = "Commond_ReleaseBL";
        /// <summary>
        /// 取消放单
        /// </summary>
        public const string Commond_CancelReleaseBL = "Commond_CancelReleaseBL";

        /// <summary>
        /// 改变类型 到正本
        /// </summary>
        public const string Commond_Change2Original = "Commond_Change2Original";

        /// <summary>
        /// 改变类型 到电放
        /// </summary>
        public const string Commond_Change2Telex = "Commond_Change2Telex";

        /// <summary>
        /// 例外客户
        /// </summary>
        public const string Commond_ExceptionCustomer = "Commond_ExceptionCustomer";

        /// <summary>
        /// 例外放货
        /// </summary>
        public const string Commond_ExRelease = "Commond_ExRelease";

        /// <summary>
        /// 提醒放货
        /// </summary>
        public const string Commond_SetArEmail = "Commond_SetArEmail";

        /// <summary>
        /// 催款
        /// </summary>
        public const string Commond_PressMoney = "Commond_PressMoney";

        /// <summary>
        /// 刷新
        /// </summary>
        public const string Commond_Refresh = "Commond_Refresh";

        /// <summary>
        /// 显示/隐藏 查询面板
        /// </summary>
        public const string Commond_ShowSearch = "Commond_ShowSearch";
        /// <summary>
        /// 查看业务信息
        /// </summary>
        public const string Command_ViewBusinessInfo = "Command_ViewBusinessInfo";
        /// <summary>
        /// 帐单
        /// </summary>
        public const string Command_Bill = "Command_Bill";

        /// <summary>
        /// 主页面改变页面时
        /// </summary>
        public const string Command_ChangedTab = "Command_ChangedTab";

        /// <summary>
        /// 显示所有
        /// </summary>
        public const string Command_VisibleALL = "Command_VisibleALL";

        /// <summary>
        /// 只显示HBL        
        /// </summary>
        public const string Command_VisibleHBL = "Command_VisibleHBL";

        /// <summary>
        /// 只显示MBL
        /// </summary>
        public const string Command_VisibleMBL = "Command_VisibleMBL";

        /// <summary>
        /// 打印BL
        /// </summary>
        public const string Command_PrintBL = "Command_PrintBL";

        /// <summary>
        /// List导出成Excel
        /// </summary>
        public const string Command_ExportToExcel = "Command_ExportToExcel";
        
        /// <summary>
        /// 收到电放正本
        /// </summary>
        public const string Command_Recevied = "Command_Recevied";

        /// <summary>
        /// 取消收到电放正本
        /// </summary>
        public const string Command_CancelRecevied = "Command_CancelRecevied";
    }

    public class ReleaseBLWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
       
        //public const string MemoWorkspace = "MemoWorkspace";
        public const string EventWorkspace = "EventWorkspace";
        public const string NextJobsWorkspace = "NextJobsWorkspace";
        public const string BillListWorkspace = "BillListWorkspace";
        public const string DebtWorkspace = "DebtWorkspace";
        public const string ContactWorkspace = "ContactListspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
    }

    public class ReleaseBLColorConstant
    {
        /// <summary>
        /// 电放
        /// </summary>
        public static Color TelexColor = Color.FromArgb(67, 156, 50); //Color.Lime;
        /// <summary>
        /// 正本
        /// </summary>
        public static Color OriginalColor = Color.FromArgb(237, 75, 11); //Color.Orange;

        /// <summary>
        /// 已签收 Color.Red
        /// </summary>
        public static Color Issue = Color.Red;
    }

    public class ReleaseBLUIAdapter:IDisposable
    {
        IEditPart _toolBar;
        ISearchPart _searchPart;
        BaseListPart _mainListPart;

        IListPart _memoListPart;
        IListPart _billlistPart;
        IListPart _debtlistPart;
        IListPart _followingBusinessListPart;
        ContactListPart _contactlistPart;
        UCDocumentList _ucDocumentList;
        UCCommunicationHistory _faxMailEDIListPart;

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IEditPart)controls[typeof(ReleaseBLToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(ReleaseBLSearchPart).Name];
            _mainListPart = (BaseListPart)controls[typeof(ReleaseBLListPart).Name];

            _billlistPart = (IListPart)controls[typeof(ReleaseBLBillListPart).Name];
            _debtlistPart = (IListPart)controls[typeof(ReleaseBLDebtListPart).Name];
            _followingBusinessListPart = (IListPart)controls[typeof(FollowingBusinessListPart).Name];
            _memoListPart = (IListPart)controls[typeof(EventListPart).Name];
            _contactlistPart = (ContactListPart)controls[typeof(ContactListPart).Name];
            _ucDocumentList = (UCDocumentList)controls[typeof(UCDocumentList).Name];
            _faxMailEDIListPart = (UCCommunicationHistory)controls[typeof(UCCommunicationHistory).Name];

            _toolBar.DataSource = null;

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                ReleaseBLList listData = data as ReleaseBLList;
                _toolBar.DataSource = listData;
                _billlistPart.DataSource = listData;
                _debtlistPart.DataSource = listData;
                _followingBusinessListPart.DataSource = listData;
                _contactlistPart.DataSource = listData;

                BusinessOperationContext context = new BusinessOperationContext();
                //show visibleHBL OR VisibleMBL 时触发CurrentChanged事件,当列表中无数据时，Alert"未将对象引用到实例"
                if (listData != null)
                {
                    context.OperationID = listData.OperationID;
                    context.FormId = listData.OperationID;
                    context.FormType = FormType.ReleaseBL;
                    context.OperationType = OperationType.OceanExport;
                    _memoListPart.DataSource = context;

                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_ucDocumentList, context);
                    _faxMailEDIListPart.BindData(context);
  
                }
            };
            #endregion
            #region _mainListPart.InvokeGetData
            _mainListPart.InvokeGetData += delegate(object sender, object data)
            {
                _searchPart.RaiseSearched(data);//快捷键
            };
            #endregion
            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };

            #endregion

            #endregion
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _billlistPart = null;
            _contactlistPart = null;
            _debtlistPart = null;
            _followingBusinessListPart = null;
            _mainListPart = null;
            _memoListPart = null;
            _searchPart = null;
            _toolBar = null;
            _faxMailEDIListPart = null;
        }
        #endregion
    }
}
