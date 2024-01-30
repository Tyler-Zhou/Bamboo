#region Comment

/*
 * 
 * FileName:    InquireRatesMainWorkspace.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->询价WorkSpace
 *      ->SelectedPageChanging:未实现
 *      ->SelectedPageChanged:在切换至空运与拖车询价时实例化WorkItem类，以加载各自面板
 *      ->Command_ShowSearch:显示或关隐藏询面板
 *      ->SetXtraTabPageVisible:设置TabPage的有效性
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 询价WorkSpace
    /// </summary>
    [ToolboxItem(false)]
    public partial class InquireRatesMainWorkspace : XtraUserControl
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 委托事件
        /// <summary>
        /// TabPage改变
        /// </summary>
        public event CancelEventHandler PageChanging;
        #endregion

        #region 成员变量
        /// <summary>
        /// 是否加载空运界面容器
        /// </summary>
        bool _isLoadAirWorkitem = false;
        /// <summary>
        /// 是否加载拖车界面容器
        /// </summary>
        bool _isLoadTruckWorkitem = false; 
        #endregion

        #region 构造函数
        public InquireRatesMainWorkspace()
        {
            InitializeComponent();
            Disposed += delegate
            {
                PageChanging = null;
                if (Workitem != null)
                {

                    Workitem.Workspaces.Remove(ToolbarWorkspace);

                    Workitem.Workspaces.Remove(SearchWorkspace);
                    Workitem.Workspaces.Remove(OceanRatesWorkspace);
                    Workitem.Workspaces.Remove(AirRatesWorkspace);
                    Workitem.Workspaces.Remove(TruckingRatesWorkspace);

                    Workitem.Items.Remove(this);

                    SearchWorkspace.PerformLayout();
                    ToolbarWorkspace.PerformLayout();
                    OceanRatesWorkspace.PerformLayout();
                    AirRatesWorkspace.PerformLayout();
                    TruckingRatesWorkspace.PerformLayout();

                    PerformLayout();
                    Workitem.Dispose();
                    Workitem = null;

                }
            };
        } 
        #endregion

        #region 窗体事件
        /// <summary>
        /// 选择面板改变时
        /// </summary>
        private void xtabMain_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            if (xtabMain.SelectedTabPageIndex == 0 && PageChanging != null)
            {
                CancelEventArgs er = new CancelEventArgs();
                PageChanging(this, er);
                e.Cancel = er.Cancel;
            }
        }
        /// <summary>
        /// 选择面板改变后
        ///     在切换至空运与拖车询价时实例化WorkItem类，以加载各自面板
        /// </summary>
        private void xtabMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtabMain.SelectedTabPageIndex == 0)//Ocean
            {
            }
            else if (xtabMain.SelectedTabPageIndex == 1 && _isLoadAirWorkitem == false)//Air
            {
                InquireAirRatesWorkitem airWorkitem = Workitem.WorkItems.AddNew<InquireAirRatesWorkitem>();
                airWorkitem.Run();
                _isLoadAirWorkitem = true;
            }
            else if (xtabMain.SelectedTabPageIndex == 2 && _isLoadTruckWorkitem == false)//Truck
            {
                InquireTruckingRatesWorkitem truckWorkitem = Workitem.WorkItems.AddNew<InquireTruckingRatesWorkitem>();
                truckWorkitem.Run();
                _isLoadTruckWorkitem = true;
            }
        }

        #region 显示/关闭查询面板
        [CommandHandler(InquireRatesCommandConstants.Command_ShowSearch)]
        public void Command_ShowSearch(object o, EventArgs e)
        {
            dpSearch.Visibility = dpSearch.Visibility == DockVisibility.Hidden ? DockVisibility.Visible : DockVisibility.Hidden;
            ToolbarWorkspace.SendToBack();
            Refresh();
        }

        #endregion

        #region EventSubscription
        /// <summary>
        /// 新增界面容器(WorkItem)
        ///     新增空运、拖车界面容器
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_AddNewWorkItem)]
        public void AddNewWorkItem(object sender, DataEventArgs<InquierType> e)
        {
            InquierType? currentType = e.Data as InquierType?;
            if (currentType != null)
            {
                if (currentType == InquierType.AirRates && _isLoadAirWorkitem == false)
                {
                    //加载空运
                    InquireAirRatesWorkitem airWorkitem = Workitem.WorkItems.AddNew<InquireAirRatesWorkitem>();
                    airWorkitem.Run();
                    _isLoadAirWorkitem = true;
                    tabAirRates.PageVisible = true;
                }
                else if (currentType == InquierType.TruckingRates && _isLoadTruckWorkitem == false)
                {
                    //加载拖车
                    InquireTruckingRatesWorkitem truckWorkitem = Workitem.WorkItems.AddNew<InquireTruckingRatesWorkitem>();
                    truckWorkitem.Run();
                    _isLoadTruckWorkitem = true;
                    tabTruckingRates.PageVisible = true;
                }

                Refresh();
            }
        }

        /// <summary>
        /// This is the subscription for the CustomerAdded event
        /// We're using the default scope, which is Global
        /// </summary>
        [EventSubscription(InquireRatesCommandConstants.Command_SetTabVisible)]
        public void SetXtraTabPageVisible(object sender, DataEventArgs<InquierType> e)
        {
            if (e == null)
                return;
            InquierType? currentType = e.Data as InquierType?;
            if (currentType == InquierType.OceanRates)
            {
                tabOceanRates.PageVisible = true;
                tabAirRates.PageVisible = false;
                tabTruckingRates.PageVisible = false;
            }
            else if (currentType == InquierType.AirRates)
            {
                tabOceanRates.PageVisible = false;
                tabAirRates.PageVisible = true;
                tabTruckingRates.PageVisible = false;
            }
            else if (currentType == InquierType.TruckingRates)
            {
                tabOceanRates.PageVisible = false;
                tabAirRates.PageVisible = false;
                tabTruckingRates.PageVisible = true;
            }
            else
            {
                tabOceanRates.PageVisible = true;
                tabAirRates.PageVisible = true;
                tabTruckingRates.PageVisible = true;
            }

            Refresh();
        }

        #endregion 
        #endregion

        #region Comment Code

        [EventSubscription(InquireRatesCommandConstants.Command_UnReadCount)]
        public void SetUnReadCount(object sender, DataEventArgs<List<UnReadDiscussingCount>> e)
        {
            //List<UnReadDiscussingCount> countList = e.Data as List<UnReadDiscussingCount>;
            //if (countList != null && countList.Count > 0)
            //{
            //    foreach (var item in countList)
            //    {
            //        if (item.Type == InquierType.OceanRates)
            //        {
            //            tabOceanRates.Text = "Ocean Rates" + "(" + item.CountOfUnreply.ToString() + ")";
            //            tabOceanRates.Tooltip = string.Format("You have {0} un-read messages(discussing).", item.CountOfUnreply.ToString());
            //        }
            //        else if (item.Type == InquierType.AirRates)
            //        {
            //            tabAirRates.Text = "Air Rates" + "(" + item.CountOfUnreply.ToString() + ")";
            //            tabAirRates.Tooltip = string.Format("You have {0} un-read messages(discussing).", item.CountOfUnreply.ToString());
            //        }
            //        else if (item.Type == InquierType.TruckingRates)
            //        {
            //            tabTruckingRates.Tooltip = string.Format("You have {0} un-read messages(discussing).", item.CountOfUnreply.ToString());
            //            tabTruckingRates.Text = "Trucking Rates" + "(" + item.CountOfUnreply.ToString() + ")";
            //        }
            //    }

            //    this.Refresh();
            //}
            //else
            //{
            //    tabOceanRates.Text = "Ocean Rates";
            //    tabAirRates.Text = "Air Rates";
            //    tabTruckingRates.Text = "Trucking Rates";
            //}
        }

        //[CommandHandler(OPCommonConstants.Command_InsterNewData)]
        //public void Command_InsterNewData(object sender, EventArgs e)
        //{
        //    if (this.splitContainerControl1.Collapsed) this.splitContainerControl1.Collapsed = false;

        //    if (this.xtabMain.SelectedTabPageIndex != 0)
        //    {
        //        this.xtabMain.SelectedPageChanging -= new DevExpress.XtraTab.TabPageChangingEventHandler(xtabMain_SelectedPageChanging);
        //        this.xtabMain.SelectedPageChanged -= new DevExpress.XtraTab.TabPageChangedEventHandler(xtabMain_SelectedPageChanged);

        //        this.xtabMain.SelectedTabPageIndex = 0;

        //        this.xtabMain.SelectedPageChanging += new DevExpress.XtraTab.TabPageChangingEventHandler(xtabMain_SelectedPageChanging);
        //        this.xtabMain.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtabMain_SelectedPageChanged);
        //    }
        //}

        //private void xtabMain_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        //{
        //    this.Workitem.Commands[InquireRatesCommandConstants.Command_TabChanged].Execute();
        //} 
        #endregion
    }
}

