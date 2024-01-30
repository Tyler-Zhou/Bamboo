using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OtherBusiness.ServiceInterface;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FCM.OtherBusiness.UI.Business;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;

namespace ICP.FCM.OtherBusiness.UI
{
    /// <summary>
    /// 其他业务
    /// </summary>
    public class OBWorkitem : WorkItem
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            OBMainWorkspace mainSpce = this.SmartParts.Get<OBMainWorkspace>("OBMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<OBMainWorkspace>("OBMainWorkspace");

                #region AddPart

                OBMainToolBar toolBar = this.SmartParts.AddNew<OBMainToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.OBMainToolBar];
                toolBarWorkspace.Show(toolBar);

                OBListPart listPart = this.SmartParts.AddNew<OBListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.OBListPart];
                listWorkspace.Show(listPart);

                OBSearchPart searchPart = this.SmartParts.AddNew<OBSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.OBSearchPart];
                searchWorkspace.Show(searchPart);


                OBFastSearchPart fastSearchPart = this.SmartParts.AddNew<OBFastSearchPart>();
                IWorkspace fastSearchPartWorkspace = (IWorkspace)this.Workspaces[OBWorkSpaceConstants.OBFastSearchPart];
                fastSearchPartWorkspace.Show(fastSearchPart);

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
                orderAdapter.Init(dic);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }

        }
    }

    public class OrderCommandConstants
    {
        public const string Command_AddOtherData = "Command_AddOtherData";
        public const string Command_CancelData = "Command_CancelData";
        public const string Command_CopyData = "Command_CopyData";
        public const string Command_EditData = "Command_EditData";
        public const string Command_Download = "Command_Download";
        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Command_Remark = "Command_Remark";
        public const string Command_FaxLog = "Command_FaxLog";
        public const string Command_Bill = "Command_Bill";
        public const string Command_Close = "Command_Close";
        public const string Command_Verification = "Command_Verification";//核销单
        public const string Command_OpContact = "Command_OpContact"; //操作联系单
        public const string Command_Profit = "Command_Profit";
        public const string Command_PrintOrder = "Command_PrintOrder";
        public const string Command_PickUp = "Command_PickUp";

        public const string Command_Document = "Command_Document";
        public const string Command_FaxEmail = "Command_FaxEmail";
        public const string Command_Memo = "Command_Memo";
        public const string Command_SendEmail = "Command_SendEmail";
        public const string Command_RefreshData = "Command_RefreshData";
        public const string Command_VerifiSheet = "Command_VerifiSheet";
        public const string Command_ShowChildWorkspace = "Command_ShowChildWorkspace";
    }

    public class OBWorkSpaceConstants
    {
        public const string OBMainToolBar = "OBMainToolBar";
        public const string OBMainWorkSpace = "OBMainWorkSpace";
        public const string OBSearchPart = "OBSearchPart";
        public const string OBListPart = "OBListPart";
        public const string OBFastSearchPart = "OBFastSearchPart";
        public const string ChildWorkspace = "ChildWorkspace";
    }

    public class OrderStateConstants
    {
        public const string CurrentRow = "CurrentRow";
    }

    public class OBUIAdapter
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


        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {

            _toolBar = (IToolBar)controls[typeof(OBMainToolBar).Name];
            _searchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OBSearchPart).Name];
            _mainListPart = (OBListPart)controls[typeof(OBListPart).Name];
            _fastSearchPart = (ICP.Framework.ClientComponents.UIFramework.ISearchPart)controls[typeof(OBFastSearchPart).Name];

            RefreshBarEnabled(_toolBar, null);

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                OtherBusinessList listData = data as OtherBusinessList;
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add("ParentList", data);

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

            _searchPart.RaiseSearched();

            _mainListPart.KeyDown += new System.Windows.Forms.KeyEventHandler(_mainListPart_KeyDown);

            _fastSearchPart.RaiseSearched();
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

            }
            else
            {

            }
        }

        #endregion
    }
}
