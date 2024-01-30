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
    public partial class UCConfigJobList : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
    {
        public UCConfigJobList()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                if (organizationFinder != null)
                {
                    organizationFinder.Dispose();
                    organizationFinder = null;
                }
                if (jobFinder != null)
                {
                    jobFinder.Dispose();
                    jobFinder = null;
                }
                this.gcJobList.DataSource = null;
                
                gvJobList.RowCellClick -= gvJobList_RowCellClick;
                this.JobBindingSource.AddingNew -= this.JobBindingSource_AddingNew;
                this.JobBindingSource.DataSource = null;
                this.JobBindingSource.Dispose();
                this.JobList = null;
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
        /// 职位列表--数据源
        /// </summary>
        public List<WorkFlowConfigJobPermissionInfo> JobList
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

        #region 绑定数据
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                InitControls();
            }
        }
        private IDisposable organizationFinder, jobFinder;
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            JobList = new List<WorkFlowConfigJobPermissionInfo>();

            JobBindingSource.DataSource = JobList;

            // btnDelete.ButtonClick += new EventHandler(btnDelete_Click);
            // btnDelete.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(btnDelete_ButtonClick);
            gvJobList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(gvJobList_RowCellClick);
            organizationFinder = DataFindClientService.RegisterGridColumnFinder(colOrganizationName
                                              , SystemFinderConstants.OrganizationFinder
                                              , "OrganizationID"
                                              , "OrganizationName"
                                              , "ID"
                                              , LocalData.IsEnglish ? "EShortName" : "CShortName");

            jobFinder = DataFindClientService.RegisterGridColumnFinder(colJobName
                                             , SystemFinderConstants.JobFinder
                                             , "JobID"
                                             , "JobName"
                                             , "ID"
                                             , LocalData.IsEnglish ? "EName" : "CName");

        }

        void gvJobList_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == DCDelete)
            {
                RemoveData();
                isDirty = true;
            }
        }

        //void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    RemoveData();
        //    isDirty = true;
        //}



        /// <summary>
        /// 刷新列表数据
        /// </summary>
        public void RefreshList()
        {
            JobBindingSource.DataSource = JobList;

            foreach (WorkFlowConfigJobPermissionInfo job in this.JobList)
            {
                job.IsDirty = false;
                job.BeginEdit();
            }
            isDirty = false;

        }
        #endregion

        #region 删除

        public void btnDelete_Click(object sender, EventArgs e)
        {
            RemoveData();
            isDirty = true;
        }
        private void RemoveData()
        {
            if (JobBindingSource.Current == null)
            {
                return;
            }
            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            JobBindingSource.RemoveCurrent();

        }
        #endregion

        private void JobBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            isDirty = true;
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public void CheckData()
        {
            foreach (WorkFlowConfigJobPermissionInfo job in this.JobList)
            {
                if (job.IsDirty)
                {
                    isDirty = true;
                    break;
                }
            }
        }


    }
}
