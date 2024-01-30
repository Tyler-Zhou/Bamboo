using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.ServiceInterface.DataObjects;
using System;
using ICP.Business.Common.UI.Document;

namespace ICP.FAM.UI.TelexApply
{
    public class TelexApplyWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            Show();
        }

        private void Show()
        {
            TelexApplyMainWorkspace mainSpace = SmartParts.Get<TelexApplyMainWorkspace>("TelexApplyMainWorkspace");

            if (mainSpace == null)
            {
                mainSpace = Items.AddNew<TelexApplyMainWorkspace>("TelexApplyMainWorkspace");

                #region AddPart

                TelexApplyToolBar toolBar = Items.AddNew<TelexApplyToolBar>();
                IWorkspace toolBarSpace = Workspaces[TelexApplyWorkSpaceConstants.ToolBarWorkspace];
                toolBarSpace.Show(toolBar);

                TelexApplySearchPart searchPart = Items.AddNew<TelexApplySearchPart>();
                IWorkspace searchSpace = Workspaces[TelexApplyWorkSpaceConstants.SearchWorkspace];
                searchSpace.Show(searchPart);

                TelexApplyListPart listPart = SmartParts.AddNew<TelexApplyListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[TelexApplyWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                TelexApplyEditPart editor = SmartParts.AddNew<TelexApplyEditPart>();
                IWorkspace editworkspace = (IWorkspace)Workspaces[TelexApplyWorkSpaceConstants.EditWorkspace];
                editor.Enabled = false;
                editworkspace.Show(editor);

                UCDocumentList documentListPart = SmartParts.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = SmartParts.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentListPart;
                documentListPart.Presenter = documentPresenter;
                IWorkspace documentListWorkspace = (IWorkspace)Workspaces[TelexApplyWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkspace.Show(documentListPart);

                #endregion

                TelexApplyUIAdapter adapter = new TelexApplyUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(editor.GetType().Name, editor);
                dic.Add(documentListPart.GetType().Name, documentListPart);
                adapter.Init(dic);


                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Telex Apply" : "总电放列表";
                mainWorkspace.Show(mainSpace, smartPartInfo);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpace);
            }
        }
    }

    #region 常量
    public class TelexApplyCommondConstants
    {
        /// <summary>
        /// Commond_Add
        /// </summary>
        public const string Commond_Add = "Commond_Add";

         /// <summary>
        /// Commond_Delete
        /// </summary>
        public const string Commond_Cancel = "Commond_Cancel";

        /// <summary>
        /// 查看总电放
        /// </summary>
        public const string Commond_View = "Commond_View";


        public const string Command_ShowSearch = "Command_ShowSearch";
    }

    public class TelexApplyWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string EditWorkspace = "EditWorkspace";
        public const string DocumentListWorkspace = "DocumentListWorkspace";
    }

    #endregion

    public class TelexApplyUIAdapter:IDisposable
    {

        IToolBar _toolBar;
        ISearchPart _searchPart;
        TelexApplyListPart _mainListPart;
        TelexApplyEditPart _editPart;
        UCDocumentList _ucDocumentList;

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(TelexApplyToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(TelexApplySearchPart).Name];
            _mainListPart = (TelexApplyListPart)controls[typeof(TelexApplyListPart).Name];
            _editPart = (TelexApplyEditPart)controls[typeof(TelexApplyEditPart).Name];
            _ucDocumentList = (UCDocumentList)controls[typeof(UCDocumentList).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                TelexApplyList listData = data as TelexApplyList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion

                _editPart.DataSource = listData;

                BusinessOperationContext context = new BusinessOperationContext();

                if (listData != null)
                {
                    context.OperationID = listData.ID;
                    context.FormId = listData.ID;
                    context.FormType = FormType.Unknown;
                    context.OperationType = OperationType.Other;

                    FCM.Common.UI.FCMUIUtility.SetDocumentListDataSource(_ucDocumentList, context);
                }
            };

            _mainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForEditPart(_mainListPart
                                                            , _editPart.SaveData
                                                            , (_editPart.DataSource as TelexApplyList)
                                                            , e
                                                            , LocalData.IsEnglish ? "Telex Apply" : "总电放");
            };
            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            _editPart.Saved += delegate(object[] data)
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
        private void RefreshBarEnabled(IToolBar toolBar, TelexApplyList listData)
        {
            if (listData == null || listData.IsNew)
            {
                toolBar.SetEnable("barCancel", false);
            }
            else
            {
                toolBar.SetEnable("barCancel", true);

                if (listData.IsValid)
                {
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Cancel(&D)" : "作废(&D)");
                }
                else
                {
                    toolBar.SetText("barCancel", LocalData.IsEnglish ? "Available(&D)" : "激活(&D)");
                }
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _editPart = null;
            _mainListPart.KeyDown -= _mainListPart_KeyDown;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
            
        }

        #endregion
    }
}
