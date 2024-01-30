
//-----------------------------------------------------------------------
// <copyright file="ShellToolBar.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.XtraBars;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.DataObject;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Helper;

    /// <summary>
    /// 工具栏
    /// </summary>
    public partial class WorkListToolBarPart : BaseToolBar
    {
        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

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



        #endregion

        #region 事件
        public event EventHandler<WorkItemClickEventArgs> NewWorkEventArgs;
        public event EventHandler EditWorkEventArgs;
        public event EventHandler CancelWork;
        public event EventHandler PrintWork;
        public event EventHandler PrintReportWork;
        public event EventHandler RefreshWork;
        public event EventHandler AuditorWork;
        public event EventHandler AuditorMergerWork;
        public event EventHandler UnAuditorWork;
        public event EventHandler MultiFinishWork;
        public event EventHandler DeleteLedger;
        #endregion

        #region 初始化

        public WorkListToolBarPart()
        {
            this.InitializeComponent();
            this.Disposed += delegate {
                this.item = null;
                this.NewWorkEventArgs = null;
                this.EditWorkEventArgs = null;
                for (int i = 0; i < this.barFormDesignToolBarManager.Items.Count; i++)
                {
                    this.barFormDesignToolBarManager.Items[i].ItemClick -= this.item_ItemClick;
                }
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                this.InitControls();
            }
        }
        BarButtonItem item = null;

        //初始化控件
        private void InitControls()
        {
            #region 创建流程工具栏按钮 

           
            List<WorkFlowConfigInfo> wfList = new List<WorkFlowConfigInfo>();
            byte[] wfitems = WorkFlowConfigService.GetWorkFlowConfigListZip(LocalData.UserInfo.LoginID, LocalData.IsEnglish);
            wfList = (List<WorkFlowConfigInfo>)DataZipStream.DecompressionArrayList(wfitems);
            if (wfList == null)
            {
                wfList = new List<WorkFlowConfigInfo>();
            }

            var infoList = from c in wfList
                           where c.IsOA
                     group c by c.CategoryName into g
                     select new { Key=g.Key,Info=g };

            foreach (var s in infoList)
            {

                BarSubItem subItem = new BarSubItem();
                subItem.Tag = s;
                subItem.Name = s.Key;
                subItem.Caption = s.Key;

                this.barFormDesignToolBarManager.Items.Add(subItem);
                this.barApplication.LinksPersistInfo.Add(new LinkPersistInfo(subItem));


                foreach (var t in s.Info)
                {

                    item = new BarButtonItem();
                    item.Tag = t.Id.ToString();
                    item.Name = t.Id.ToString();
                    item.Caption = LocalData.IsEnglish?t.EDescription:t.CDescription;

                    if (t.Days > 0)
                    {
                        ///禁用本月暂停使用的流程
                        DateTime startTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        DateTime endTime = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);
                        TimeSpan timeSpan = endTime - startTime;

                        //停用
                        DateTime stopTime = endTime.AddDays(-(t.Days));

                        if (t.Days > timeSpan.Days)
                        {
                            item.Enabled = false;

                            string stop = (stopTime.Day+1).ToString() + (LocalData.IsEnglish ? "Day Stop" : "号停用");

                            item.Caption = item.Caption + "<" + stop + ">";
                        }
                    }

                    this.barFormDesignToolBarManager.Items.Add(item);

                    subItem.LinksPersistInfo.Add(new LinkPersistInfo(item));


                    item.ItemClick += new ItemClickEventHandler(item_ItemClick);
                }
            }

            #endregion


            if (ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(CommandConstants.Command_WorkList_WF_MULTIFINISH))
            {
                this.barMultiFinish.Visibility = BarItemVisibility.Always;
            }
            else
            {
                this.barMultiFinish.Visibility = BarItemVisibility.Never;
            }
            //权限
            if (ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(CommandConstants.Command_DeleteLedger))
            {
                this.barDelete.Visibility = BarItemVisibility.Always;
            }
            else
            {
                this.barDelete.Visibility = BarItemVisibility.Never;
            }
        }
        /// <summary>
        /// 发起新流程事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_ItemClick(object sender, ItemClickEventArgs e)
        {
            //WorkItem.State["NewWork"] = e.Item.Name;

            if (NewWorkEventArgs != null)
            {
                WorkItemClickEventArgs arg = null;
                if (!string.IsNullOrEmpty(e.Item.Name))
                {
                    arg = new WorkItemClickEventArgs(e.Item.Name);
                }
                else
                {
                    arg = new WorkItemClickEventArgs(string.Empty);
                }

                NewWorkEventArgs(sender, arg);
            }
        }

        void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }
  
        #endregion

        #region 方法
        /// <summary>
        /// 设置工具栏按钮的可操作性
        /// </remarks>
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="enable">是否可操作</param>
        public override void SetEnable(string name, bool enable)
        {
            barFormDesignToolBarManager.Items[name].Enabled = enable;
        }
        #endregion

        #region 按钮事件
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDoTask_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (EditWorkEventArgs != null)
            {
                EditWorkEventArgs(sender,e);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CancelWork != null)
            {
                CancelWork(sender, e);
            }
        }
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAuditor_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (AuditorWork != null)
            {
                AuditorWork(sender, e);
            }
        }
        /// <summary>
        /// 审核多个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAuditorMerger_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (AuditorMergerWork != null)
            {
                AuditorMergerWork(sender, e);
            }
        }
        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barUnAudiror_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (UnAuditorWork != null)
            {
                UnAuditorWork(sender, e);
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PrintWork != null)
            {
                PrintWork(sender, e);
            }
        }
        /// <summary>
        /// 打印支票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPrintReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PrintReportWork != null)
            {
                PrintReportWork(sender, e);
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (RefreshWork != null)
            {
                RefreshWork(sender, e);
            }
        }
        /// <summary>
        /// 批量完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barMultiFinish_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MultiFinishWork != null)
            {
                MultiFinishWork(sender, e);
            }
        }
        #endregion
        /// <summary>
        /// 删除凭证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DeleteLedger != null)
            {
                DeleteLedger(sender,e);
            }
        }


    }


    public delegate void WorkItemClickEventHandler(object sender, WorkItemClickEventArgs e);

    public class WorkItemClickEventArgs : ItemClickEventArgs
    {
        string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }



        public WorkItemClickEventArgs(string val):base(null,null)
        {
            value = val;
        }
    }
}
