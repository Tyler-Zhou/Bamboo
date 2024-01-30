using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.ServiceInterface.DataObject;

namespace ICP.WF.WorkFlowDesigner
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class UCConfigUserList : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
    {
        public UCConfigUserList()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.UserList = null;
                if (this.userFinder != null)
                {
                    this.userFinder.Dispose();
                    this.userFinder = null;
                }
                this.gcUserList.DataSource = null;
                this.UserBindingSource.AddingNew -= this.UserBindingSource_AddingNew;
                this.UserBindingSource.DataSource = null;
                this.UserBindingSource.Dispose();
                gvUserList.RowCellClick -= gvUserList_RowCellClick;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public Guid WorkItemID
        {
            get;
            set;
        }
        /// <summary>
        /// 员工列表--数据源
        /// </summary>
        public List<WorkFlowConfigUserPermissionInfo> UserList
        {
            get;
            set;
        }
        private bool isDirty = false;
        /// <summary>
        /// 是否有数据发生改变
        /// </summary>
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }
        private IDisposable userFinder;
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            UserList = new List<WorkFlowConfigUserPermissionInfo>();
            UserBindingSource.DataSource = UserList;

           // btnDelete.Click += new EventHandler(btnDelete_Click);
            gvUserList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gvUserList_RowCellClick);
         userFinder=DataFindClientService.RegisterGridColumnFinder(colUserName
                                          , SystemFinderConstants.UserFinder
                                          , "UserID"
                                          , "UserName"
                                          , "ID"
                                          , LocalData.IsEnglish ? "EName" : "CName");


        }

     

        /// <summary>
        /// 刷新列表数据
        /// </summary>
        public void RefreshList()
        {
            UserBindingSource.DataSource = UserList;

            foreach (WorkFlowConfigUserPermissionInfo user in this.UserList)
            {
                user.IsDirty = false;
                user.BeginEdit();
            }

            isDirty = false;
        }


        #endregion

        #region 服务

        /// <summary>
        /// 工作流配置管理服务
        /// </summary>
        public IWorkFlowConfigService WorkFlowConfigService
        {
            get 
            {
                return ServiceClient.GetService<IWorkFlowConfigService>();
            }
        }

        /// <summary>
        /// 客户端搜索器
        /// </summary>
        public IDataFindClientService DataFindClientService 
        {
            get 
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion


        #region 删除
        void gvUserList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == dcDelete)
            {
                RemoveData();
                isDirty = true;
            }
        }


        private void RemoveData()
        {
            if (UserBindingSource.Current == null)
            {
                return;
            }
            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }
            WorkFlowConfigUserPermissionInfo info = UserBindingSource.Current as WorkFlowConfigUserPermissionInfo;
            if (info == null)
            {
                return;
            }

            UserBindingSource.RemoveCurrent(); 
        }
        #endregion

        private void UserBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            isDirty = true;
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public void CheckData()
        {
            if (isDirty)
            {
                return;
            }
            foreach (WorkFlowConfigUserPermissionInfo user in this.UserList)
            {
                if (user.IsDirty)
                {
                    isDirty = true;
                    break;
                }
            }

            return;
        }

 

    }
}
