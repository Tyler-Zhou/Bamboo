using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using System.ComponentModel;
using System;
using ICP.Business.Common.UI.Document;

namespace ICP.FAM.UI.MonthlyClosingEntry
{
    class EntryWorkItem :WorkItem 
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            ShowMainUI();
        }

        private void ShowMainUI()
        {
            MainWorkSpace mainSpce = SmartParts.Get<MainWorkSpace>("MonthlyMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<MainWorkSpace>("MonthlyMainWorkSpace");

                #region AddPart

                ToolBar toolBar = SmartParts.AddNew<ToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[EntryWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                EntryList listPart = SmartParts.AddNew<EntryList>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[EntryWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                SearchPanel searchPart = SmartParts.AddNew<SearchPanel>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[EntryWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                EntryEdit detailPart = SmartParts.AddNew<EntryEdit>();
                IWorkspace detailWorkspace = (IWorkspace)Workspaces[EntryWorkSpaceConstants.EditWorkspace];
                detailPart.Enabled = false;
                detailWorkspace.Show(detailPart);

                UCDocumentList documentListPart = SmartParts.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = SmartParts.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentListPart;
                documentListPart.Presenter = documentPresenter;
                IWorkspace documentListWorkspace = (IWorkspace)Workspaces[EntryWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);


                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Monthly Closing List" : "月结协议管理";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                EntryUIAdapter bookingAdapter = new EntryUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(detailPart.GetType().Name, detailPart);
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
    /// WorkSpace常量
    /// </summary>
    public class EntryWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

        public const string EditWorkspace = "EditWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
    }


    public class EntryCommondConstants
    {
        /// <summary>
        /// Commond_Add
        /// </summary>
        public const string Commond_Add = "Commond_Add";

        /// <summary>
        /// Commond_Delete
        /// </summary>
        public const string Commond_Delete = "Commond_Delete";

        /// <summary>
        /// Edit
        /// </summary>
        public const string Commond_Edit = "Commond_Edit";

        /// <summary>
        /// 查看总电放
        /// </summary>
        public const string Commond_View = "Commond_View";

        public const string Command_ShowSearch = "Command_ShowSearch";

    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class EntryUIAdapter : IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        EntryList _mainListPart;
        EntryEdit _detailListPart;
        UCDocumentList _ucDocumentList;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(ToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(SearchPanel).Name];
            _mainListPart = (EntryList)controls[typeof(EntryList).Name];

            _detailListPart = (EntryEdit)controls[typeof(EntryEdit).Name];
            _ucDocumentList = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                MonthlyClosingEntryList dataList = data as MonthlyClosingEntryList;
                _detailListPart.DataSource = dataList;
                RefreshBarEnabled(_toolBar, dataList);

                BusinessOperationContext context = new BusinessOperationContext();
                //show visibleHBL OR VisibleMBL 时触发CurrentChanged事件,当列表中无数据时，Alert"未将对象引用到实例"
                if (dataList != null)
                {
                    context.OperationID = dataList.ID;
                    context.FormId = dataList.ID;
                    context.FormType = FormType.Unknown;
                    context.OperationType = OperationType.Other;

                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_ucDocumentList, context);
                }
            };

            _mainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForEditPart(_mainListPart
                                                           , _detailListPart.SaveData
                                                           , (_detailListPart.DataSource as MonthlyClosingAgreement)
                                                           , e
                                                           , LocalData.IsEnglish ? "Monthly Closing Entry" : "月结协议");


            };

            _mainListPart.Selected += delegate(object sender, object data)
            {

            };

            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            _detailListPart.Saved += delegate(object[] data)
            {
                _mainListPart.Refresh(data);
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
        private void RefreshBarEnabled(IToolBar toolBar, MonthlyClosingEntryList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("bbiRecycle", false);
            }
            else
            {
                toolBar.SetEnable("bbiRecycle", true);

                if (listData.IsValid)
                {
                    toolBar.SetText("bbiRecycle", LocalData.IsEnglish ? "Cancel(&D)" : "作废(&D)");
                }
                else
                {
                    toolBar.SetText("bbiRecycle", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                }
            }
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _detailListPart = null;
            if (_mainListPart != null)
            {
                _mainListPart.KeyDown -= _mainListPart_KeyDown;
                _mainListPart = null;
            }
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }
}
