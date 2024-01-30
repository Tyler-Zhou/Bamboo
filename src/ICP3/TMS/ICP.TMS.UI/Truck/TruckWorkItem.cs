using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using System.ComponentModel;
using ICP.TMS.ServiceInterface;

namespace ICP.TMS.UI
{
    class TruckWorkItem : WorkItem 
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            this.ShowMainUI();
        }

        private void ShowMainUI()
        {
            TruckMainWorkSpace mainSpce = this.SmartParts.Get<TruckMainWorkSpace>("TruckMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<TruckMainWorkSpace>("TruckMainWorkSpace");

                #region AddPart

                TruckToolBar toolBar = this.SmartParts.AddNew<TruckToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[TruckWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                TruckList listPart = this.SmartParts.AddNew<TruckList>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[TruckWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                TruckSearchPanel searchPart = this.SmartParts.AddNew<TruckSearchPanel>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[TruckWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                TruckEdit editPart = this.SmartParts.AddNew<TruckEdit>();
                IWorkspace detailWorkspace = (IWorkspace)this.Workspaces[TruckWorkSpaceConstants.EditWorkspace];
                editPart.Enabled = false;
                detailWorkspace.Show(editPart);


                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Truck List" : "拖车管理";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                TruckUIAdapter bookingAdapter = new TruckUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(editPart.GetType().Name, editPart);

                bookingAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class TruckWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

        public const string EditWorkspace = "EditWorkspace";
    }


    public class TruckCommondConstants
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
    public class TruckUIAdapter:IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        TruckList _mainListPart;
        TruckEdit _editListPart;

        #endregion

        #region interface
        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(TruckToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(TruckSearchPanel).Name];
            _mainListPart = (TruckList)controls[typeof(TruckList).Name];
            _editListPart = (TruckEdit)controls[typeof(TruckEdit).Name];

            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                TruckDataList listData = data as TruckDataList;
                _editListPart.DataSource = listData;

                RefreshBarEnabled(_toolBar, listData);
            };

            #endregion


            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };

            #endregion

            #region CurrentChanging
            _mainListPart.CurrentChanging += delegate(object sender, CancelEventArgs e)
            {
                UIConnectionHelper.ParentChangingForEditPart(_mainListPart
                                                           , _editListPart.SaveData
                                                           , (_editListPart.DataSource as TruckDataList)
                                                           , e
                                                           , LocalData.IsEnglish ? "Truck Info" : "拖车资料");


            };
            #endregion

            #region Save
            _editListPart.Saved += delegate(object[] data)
            {
                _mainListPart.RefreshData(data);
            };
            #endregion

            #endregion
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
        private void RefreshBarEnabled(IToolBar toolBar, TruckDataList listData)
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
                    toolBar.SetText("bbiRecycle", LocalData.IsEnglish ? "Invalid" : "作废");
                }
                else
                {
                    toolBar.SetText("bbiRecycle", LocalData.IsEnglish ? "Activation" : "激活");
                }
            }

        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this._editListPart = null;
            this._mainListPart = null;
            this._searchPart = null;
            this._toolBar = null;
         
        }

        #endregion
    }
}
