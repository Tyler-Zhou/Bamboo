using System.Collections.Generic;

using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.Drawing;
using System;

namespace ICP.FAM.UI.ReleaseRC
{
    public class ReleaseRCWorkItem : WorkItem
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
            ReleaseRCMainWorkspace mainSpace = SmartParts.Get<ReleaseRCMainWorkspace>("ReleaseRCMainWorkspace");

            if (mainSpace == null)
            {
                mainSpace = Items.AddNew<ReleaseRCMainWorkspace>("ReleaseRCMainWorkspace");

                #region AddPart

                ReleaseRCToolBar toolBar = Items.AddNew<ReleaseRCToolBar>();
                IWorkspace toolBarSpace = Workspaces[ReleaseRCWorkSpaceConstants.ToolBarWorkspace];
                toolBarSpace.Show(toolBar);

                ReleaseRCSearchPart searchPart = Items.AddNew<ReleaseRCSearchPart>();
                IWorkspace searchSpace = Workspaces[ReleaseRCWorkSpaceConstants.SearchWorkspace];
                searchSpace.Show(searchPart);

                ReleaseRCListPart listPart = Items.AddNew<ReleaseRCListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[ReleaseRCWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                //ReleaseRCBillListPart billlistPart = this.Items.AddNew<ReleaseRCBillListPart>();
                //IWorkspace billlistWorkspace = (IWorkspace)this.Workspaces[ReleaseRCWorkSpaceConstants.BillListWorkspace];
                //billlistWorkspace.Show(billlistPart);

                //ReleaseRCDebtListPart debtlistPart = this.Items.AddNew<ReleaseRCDebtListPart>();
                //IWorkspace debtlistWorkspace = (IWorkspace)this.Workspaces[ReleaseRCWorkSpaceConstants.DebtWorkspace];
                //debtlistWorkspace.Show(debtlistPart);

                //FollowingBusinessListPart followingBusinessListPart = this.Items.AddNew<FollowingBusinessListPart>();
                //IWorkspace nextJobslistWorkspace = (IWorkspace)this.Workspaces[ReleaseRCWorkSpaceConstants.NextJobsWorkspace];
                //nextJobslistWorkspace.Show(followingBusinessListPart);

                #endregion

                ReleaseRCUIAdapter adapter = new ReleaseRCUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(listPart.GetType().Name, listPart);

                //dic.Add(billlistPart.GetType().Name, billlistPart);
                //dic.Add(debtlistPart.GetType().Name, debtlistPart);
                //dic.Add(followingBusinessListPart.GetType().Name, followingBusinessListPart);
                adapter.Init(dic);


                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "ReleaseRC" : "放货列表";
                mainWorkspace.Show(mainSpace, smartPartInfo);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpace);
            }
        }
    }

    public class ReleaseRCCommondConstants
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
        /// 放单
        /// </summary>
        public const string Commond_ReleaseBL = "Commond_ReleaseBL";
        /// <summary>
        /// 改变类型
        /// </summary>
        public const string Commond_ChangeType = "Commond_ChangeType";
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
        /// 异常放货
        /// </summary>
        public const string Command_ExceptionReleaseRC = "Command_ExceptionReleaseRC";

        /// <summary>
        /// 主页面改变页面时
        /// </summary>
        public const string Command_ChangedTab = "Command_ChangedTab";

    }

    public class ReleaseRCWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

        public const string MemoWorkspace = "MemoWorkspace";
        public const string NextJobsWorkspace = "NextJobsWorkspace";
        public const string BillListWorkspace = "BillListWorkspace";
        public const string DebtWorkspace = "DebtWorkspace";
    }

    public class ReleaseRCColorConstant
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

    public class ReleaseRCUIAdapter:IDisposable
    {
        IEditPart _toolBar;
        ISearchPart _searchPart;
        BaseListPart _mainListPart;

        IListPart _billlistPart;
        IListPart _debtlistPart;
        IListPart _followingBusinessListPart;

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IEditPart)controls[typeof(ReleaseRCToolBar).Name];
            _searchPart = (ISearchPart)controls[typeof(ReleaseRCSearchPart).Name];
            _mainListPart = (BaseListPart)controls[typeof(ReleaseRCListPart).Name];

            //_billlistPart = (IListPart)controls[typeof(ReleaseRCBillListPart).Name];
            //_debtlistPart = (IListPart)controls[typeof(ReleaseRCDebtListPart).Name];
            //_followingBusinessListPart = (IListPart)controls[typeof(FollowingBusinessListPart).Name];

            _toolBar.DataSource = null;

            #region Connection

            #region _mainListPart.CurrentChanged
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                ReleaseRCList listData = data as ReleaseRCList;
                _toolBar.DataSource = listData;
                //_billlistPart.DataSource = listData;
                //_debtlistPart.DataSource = listData;
                //_followingBusinessListPart.DataSource = listData;
            };
            #endregion

            #region _searchPart.OnSearched
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #region 分页
            _mainListPart.InvokeGetData += delegate(object sender, object data)
            {
                _searchPart.RaiseSearched(data);
            };
            #endregion

            #endregion
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _billlistPart = null;
            _debtlistPart = null;
            _followingBusinessListPart = null;
            _mainListPart = null;
            _searchPart = null;
            _toolBar = null;
        }

        #endregion
    }
}
