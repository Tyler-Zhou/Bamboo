using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;

namespace ICP.WF.Controls.Form.Commission
{
    public class WFBusinessWorkitem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            WFBusinessMainWorkspace mainSpce = this.SmartParts.Get<WFBusinessMainWorkspace>("WFBusinessMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<WFBusinessMainWorkspace>("WFBusinessMainWorkspace");

                #region AddPart

                WFBusinessToolBar toolBar = this.SmartParts.AddNew<WFBusinessToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[WFBusinessWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                WFBusinessListPart listPart = this.SmartParts.AddNew<WFBusinessListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[WFBusinessWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                WFBusinessSearchPart searchPart = this.SmartParts.AddNew<WFBusinessSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[WFBusinessWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);


                WFSelectBusinessListPart selectListPart = this.SmartParts.AddNew<WFSelectBusinessListPart>();
                IWorkspace selectListWorkspace = (IWorkspace)this.Workspaces[WFBusinessWorkSpaceConstants.SelectListWorkspace];
                selectListWorkspace.Show(selectListPart);


                WFCommissionLogListPart logListPart = this.SmartParts.AddNew<WFCommissionLogListPart>();
                IWorkspace logListWorkspace = (IWorkspace)this.Workspaces[WFBusinessWorkSpaceConstants.CommissionLogWorkspace];
                logListWorkspace.Show(logListPart);

                #endregion

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Search Business" : "查询业务";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                WFBusinessUIAdapter bookingAdapter = new WFBusinessUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                bookingAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    public class WFBusinessCommandConstants
    {
        /// <summary>
        /// 选择
        /// </summary>
        public const string Command_Select = "Command_Select";


        /// <summary>
        /// 确定
        /// </summary>
        public const string Command_Commission_OK = "Command_Commission_OK";


    }

    public class WFBusinessWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
        public const string SelectListWorkspace = "SelectListWorkspace";
        public const string CommissionLogWorkspace = "CommissionLogWorkspace";
    }

    public class WFBusinessUIAdapter : IDisposable
    {

        #region parts

        IEditPart _toolBar;
        ISearchPart _searchPart;
        BaseListPart _mainListPart;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IEditPart)controls[typeof(WFBusinessToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(WFBusinessSearchPart).Name];
            _mainListPart = (BaseListPart)controls[typeof(WFBusinessListPart).Name];

            _toolBar.DataSource = null;

            #region Connection

            #region _mainListPart.Selecting
            _mainListPart.Selecting += OnMailListPartSelecting;
            #endregion

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += this.OnMainListPartCurrentChanged;

            #endregion

            #region _mainListPart.InvokeGetData
            _mainListPart.InvokeGetData += this.OnInvokeGetData;

            #endregion
            #region _searchPart.OnSearched
            _searchPart.OnSearched += this.OnSearchPartSearched;

            #endregion

            #endregion
        }
        private void OnMailListPartSelecting(object sender, object data, System.ComponentModel.CancelEventArgs e)
        {
            Dictionary<string, object> keyValue = new Dictionary<string, object>();
            keyValue.Add("Selected", data);
            _toolBar.Init(keyValue);
        }
        private void OnMainListPartCurrentChanged(object sender, object data)
        {
            _toolBar.DataSource = data;
        }
        private void OnInvokeGetData(object sender, object data)
        {
            _searchPart.RaiseSearched(data);
        }
        private void OnSearchPartSearched(object sender, object results)
        {
            _mainListPart.DataSource = results;
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (this._mainListPart != null)
            {
                this._mainListPart.CurrentChanged -= this.OnMainListPartCurrentChanged;
                this._mainListPart.Selecting -= this.OnMailListPartSelecting;
                this._mainListPart = null;
            }
            if (this._searchPart != null)
            {
                this._searchPart.OnSearched -= this.OnSearchPartSearched;
                this._searchPart = null;
            }
            this._toolBar = null;
        }

        #endregion
    }
}
