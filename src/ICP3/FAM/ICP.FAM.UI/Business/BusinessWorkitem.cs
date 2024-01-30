using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;

namespace ICP.FAM.UI.Business
{
    public class BusinessWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        
        private void Show()
        {
            BusinessMainWorkspace mainSpce = SmartParts.Get<BusinessMainWorkspace>("BusinessMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<BusinessMainWorkspace>("BusinessMainWorkspace");

                #region AddPart

                BusinessToolBar toolBar = SmartParts.AddNew<BusinessToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[BusinessWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BusinessListPart listPart = SmartParts.AddNew<BusinessListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[BusinessWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BusinessSearchPart searchPart = SmartParts.AddNew<BusinessSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[BusinessWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Business List" : "业务列表";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                BusinessUIAdapter bookingAdapter = new BusinessUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                bookingAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class BusinessCommandConstants
    {
        /// <summary>
        /// 查看业务信息
        /// </summary>
        public const string Command_ViewBusinessInfo = "Command_ViewBusinessInfo";
        /// <summary>
        /// 帐单
        /// </summary>
        public const string Command_Bill = "Command_Bill";
        /// <summary>
        /// 显示/隐藏 查询面板
        /// </summary>
        public const string Command_ShowSearch = "Command_ShowSearch";
        /// <summary>
        /// 全选
        /// </summary>
        public const string Command_SelectAll = "Command_SelectAll";
        /// <summary>
        /// 刷新
        /// </summary>
        public const string Command_Refresh = "Command_Refresh";
        /// <summary>
        /// 批量帐单
        /// </summary>
        public const string Command_BatchAddBill = "Command_BatchAddBill";
    }

    public class BusinessWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
    }

    public class BusinessUIAdapter : IDisposable
    {

        #region parts

        IEditPart _toolBar;
        ISearchPart _searchPart;
        BaseListPart _mainListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IEditPart)controls[typeof(BusinessToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(BusinessSearchPart).Name];
            _mainListPart = (BaseListPart)controls[typeof(BusinessListPart).Name];

            _toolBar.DataSource = null;

            #region Connection

            #region _mainListPart.Selecting
            _mainListPart.Selecting += delegate(object sender, object data, CancelEventArgs e)
            {
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("Selected", data);
                _toolBar.Init(keyValue);
            };
            #endregion

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                _toolBar.DataSource = data;
            };
            #endregion

            #region _mainListPart.InvokeGetData
            _mainListPart.InvokeGetData  += delegate(object sender, object data)
            {
                _searchPart.RaiseSearched(data);
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

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }
}
