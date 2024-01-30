using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using System.ComponentModel;
using ICP.TMS.ServiceInterface;
using System;

namespace ICP.TMS.UI
{
    class DriverWorkItem : WorkItem 
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            this.ShowMainUI();
        }

        private void ShowMainUI()
        {
            DriverMainWorkSpace mainSpce = this.SmartParts.Get<DriverMainWorkSpace>("DriverMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<DriverMainWorkSpace>("DriverMainWorkSpace");

                #region AddPart

                DriverToolBar toolBar = this.SmartParts.AddNew<DriverToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[DriverWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                DriverList listPart = this.SmartParts.AddNew<DriverList>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[DriverWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                DriverSearchPanel searchPart = this.SmartParts.AddNew<DriverSearchPanel>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[DriverWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                DriverEdit detailPart = this.SmartParts.AddNew<DriverEdit>();
                IWorkspace detailWorkspace = (IWorkspace)this.Workspaces[DriverWorkSpaceConstants.EditWorkspace];
                detailPart.Enabled = false;
                detailWorkspace.Show(detailPart);


                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Driver List" : "司机管理";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                DriverUIAdapter driverAdapter = new DriverUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(detailPart.GetType().Name, detailPart);

                driverAdapter.Init(dic);
                
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
    public class DriverWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

        public const string EditWorkspace = "EditWorkspace";
    }


    public class DriverCommondConstants
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
    public class DriverUIAdapter:IDisposable
    {

        #region parts

        IToolBar _toolBar;
        ISearchPart _searchPart;
        DriverList _mainListPart;
        DriverEdit _editListPart;

        #endregion

        #region interface
        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(DriverToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(DriverSearchPanel).Name];
            _mainListPart = (DriverList)controls[typeof(DriverList).Name];
            _editListPart = (DriverEdit)controls[typeof(DriverEdit).Name];

            RefreshBarEnabled(_toolBar, null);
            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                DriversDataList listData = data as DriversDataList;
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
                                                           , (_editListPart.DataSource as DriversDataList)
                                                           , e
                                                           , LocalData.IsEnglish ? "Driver Info" : "司机资料");


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
        private void RefreshBarEnabled(IToolBar toolBar, DriversDataList listData)
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
