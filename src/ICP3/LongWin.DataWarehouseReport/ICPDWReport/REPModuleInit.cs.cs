using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.ObjectBuilder;
using Agilelabs.Framework.Service;
using Agilelabs.Framework.Client;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using LongWin.Framework.Client;
using LongWin.DataWarehouseReport.ServiceInterface;
using LongWin.CommonData.ServiceInterface;
using LongWin.CommonData.ServiceInterface.DataObjects;
namespace LongWin.DataWarehouseReport.WinUI
{
    public class REPModuleInit:ModuleInit
    {
        //测试 服务注入问题
        static IREPBaseDataService _repService;
        public static IREPBaseDataService RepService
        {
            set
            {
                if (value != null)
                {
                    _repService = value;
                }
            }
            get
            {
                return _repService;
            }
        }

        static IInitializeService _initializeService;
        public static IInitializeService InitializeService
        {
            get
            {
                return _initializeService;
            }

            set
            {
                if (value != null)
                {
                    REPModuleInit._initializeService = value;
                }
            }
        }

        static ITransportFoundationService _foundationService;
        public static ITransportFoundationService FoundationService
        {
            get { return REPModuleInit._foundationService; }
            set
            {
                if (value != null)
                {
                    REPModuleInit._foundationService = value;
                }
            }
        }

        WorkItem _rootWorkItem;
        REPWrokItem _repWorkItem;
        IREPBaseDataService _repBaseDataService;
        ITransportFoundationService _transportFoundationService;
        IInitializeService _initeService;

        public REPModuleInit([ServiceDependency] WorkItem rootWorkItem
            , [ServiceDependency] IDataFinderFactory iDatafinderFactory
            , [ServiceDependency] IREPBaseDataService repBaseDataService
            , [ServiceDependency] ITransportFoundationService transportFoundationService
            , [ServiceDependency] IInitializeService initializeService)
        {
            //if (repBaseDataService == null)
            //{
            //    MessageBox.Show("服务为空");
            //}
            //else
            //{
            //    MessageBox.Show("服务不为空");
            //}

            this._rootWorkItem = rootWorkItem;
            this._repBaseDataService = repBaseDataService;
            this._transportFoundationService = transportFoundationService;
            this._initeService = initializeService;

            REPModuleInit.FoundationService = transportFoundationService;
            REPModuleInit.RepService = repBaseDataService;
            REPModuleInit.InitializeService = initializeService;
            if (_transportFoundationService == null || _repBaseDataService == null)
            {
                PortfolioAndProfitDetailWorkItem item = this._rootWorkItem.Items.AddNew<PortfolioAndProfitDetailWorkItem>();
                item.GetService();
                _transportFoundationService = REPModuleInit.FoundationService;
                _repBaseDataService = REPModuleInit.RepService;
            }


            
        }


        public WorkItem RootWorkItem
        {
            get { return _rootWorkItem; }
        }

      
        IWorkspace workspace;
   
        #region FCM
        /// <summary>
        /// 箱量利润统计表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("REP_DWPortfolioAndProfitTotal")]
        public void OpenPortfolioAndProfitTotal(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }

            if (_transportFoundationService == null || _repBaseDataService == null)
            {
                PortfolioAndProfitDetailWorkItem item = this._rootWorkItem.Items.AddNew<PortfolioAndProfitDetailWorkItem>();
                item.GetService();
                _transportFoundationService = REPModuleInit.FoundationService;
                _repBaseDataService = REPModuleInit.RepService;
            }

            //
            //箱量利润
            //
            Cursor.Current = Cursors.WaitCursor;
            ICollection<PortfolioAndProfitTotalWorkItem> list = this._rootWorkItem.Items.FindByType<PortfolioAndProfitTotalWorkItem>();
            if (list.Count > 0)
            {
                foreach (PortfolioAndProfitTotalWorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }

            PortfolioAndProfitTotalWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<PortfolioAndProfitTotalWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
            {
                workspace = this._rootWorkItem.Workspaces["MainWorkspace"];
            }
            costDetailItem.Show(workspace);

            Cursor.Current = Cursors.Arrow;
        }

        [CommandHandler("REP_DWPortfolioAndProfitDetail")]
        public void OpenPortfolioAndProfitDetail(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //箱量利润
            //
            Cursor.Current = Cursors.WaitCursor;
            if (_transportFoundationService == null || _repBaseDataService == null)
            {
                PortfolioAndProfitTotalWorkItem item = this._rootWorkItem.Items.AddNew<PortfolioAndProfitTotalWorkItem>();
                item.GetService();
                _transportFoundationService = REPModuleInit.FoundationService;
                _repBaseDataService = REPModuleInit.RepService;
            }

            ICollection<PortfolioAndProfitDetailWorkItem> list = this._rootWorkItem.Items.FindByType<PortfolioAndProfitDetailWorkItem>();
            if (list.Count > 0)
            {
                foreach (PortfolioAndProfitDetailWorkItem item in list)
                {
                    item.RepBaseDataService = REPModuleInit.RepService;
                    item.TransportFoundationService = REPModuleInit.FoundationService;
                    item.Show(workspace);
                    return;
                }
            }

            PortfolioAndProfitDetailWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<PortfolioAndProfitDetailWorkItem>();
            costDetailItem.RepBaseDataService = _repBaseDataService;
            costDetailItem.TransportFoundationService = _transportFoundationService;
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
            {
                workspace = this._rootWorkItem.Workspaces["MainWorkspace"];
            }

            costDetailItem.Show(workspace);

            Cursor.Current = Cursors.Arrow;
        }


        /// <summary>
        /// 商务箱量统计表 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("RPT_CommercePortfolioAndProfitForGeneral_Total")]
        public void OpenCommerceTotal(object sender, EventArgs e)
        {
             if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }

            if (_transportFoundationService == null || _repBaseDataService == null)
            {
                
                CommercePortfolioAndProfitTotallWorkItem item = this._rootWorkItem.Items.AddNew<CommercePortfolioAndProfitTotallWorkItem>();
                item.GetService();
                _transportFoundationService = REPModuleInit.FoundationService;
                _repBaseDataService = REPModuleInit.RepService;
            }

            //
            //箱量利润
            //
            Cursor.Current = Cursors.WaitCursor;
            ICollection<CommercePortfolioAndProfitTotallWorkItem> list = this._rootWorkItem.Items.FindByType<CommercePortfolioAndProfitTotallWorkItem>();
            if (list.Count > 0)
            {
                foreach (CommercePortfolioAndProfitTotallWorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }

            CommercePortfolioAndProfitTotallWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<CommercePortfolioAndProfitTotallWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
            {
                workspace = this._rootWorkItem.Workspaces["MainWorkspace"];
            }
            costDetailItem.Show(workspace);

            Cursor.Current = Cursors.Arrow;
        }
    
   

    /// <summary>
    ///  商务箱量详细表 
    /// </summary>
        [CommandHandler("RPT_CommercePortfolioAndProfitForGeneral_Detail")]
        public void OpenCommercePortfolioAndProfitForGeneral_Detail(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //商务箱量利润
            //
            Cursor.Current = Cursors.WaitCursor;
            if (_transportFoundationService == null || _repBaseDataService == null )
            {
                CommercePortfolioAndProfitDetaillWorkItem item = this._rootWorkItem.Items.AddNew<CommercePortfolioAndProfitDetaillWorkItem>();
                item.GetService();
                _transportFoundationService = REPModuleInit.FoundationService;
                _repBaseDataService = REPModuleInit.RepService;
            }

            ICollection<CommercePortfolioAndProfitDetaillWorkItem> list = this._rootWorkItem.Items.FindByType<CommercePortfolioAndProfitDetaillWorkItem>();
            if (list.Count > 0)
            {
                foreach (CommercePortfolioAndProfitDetaillWorkItem item in list)
                {
                    item.RepBaseDataService = REPModuleInit.RepService;
                    item.TransportFoundationService = REPModuleInit.FoundationService;
                    item.Show(workspace);
                    return;
                }
            }

            CommercePortfolioAndProfitDetaillWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<CommercePortfolioAndProfitDetaillWorkItem>();
            costDetailItem.RepBaseDataService = _repBaseDataService;
            costDetailItem.TransportFoundationService = _transportFoundationService;
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
            {
                workspace = this._rootWorkItem.Workspaces["MainWorkspace"];
            }
          
            costDetailItem.Show(workspace);

            Cursor.Current = Cursors.Arrow;
        }

        [CommandHandler("REP_DWPorfitForT")]
        public void OpenPorfitForT(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //单箱利润
            //
            Cursor.Current = Cursors.WaitCursor;
            
            ICollection<ProfitForTWorkItem> list = this._rootWorkItem.Items.FindByType<ProfitForTWorkItem>();
            if (list.Count > 0)
            {
                foreach (ProfitForTWorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }


            ProfitForTWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<ProfitForTWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
            costDetailItem.Show(workspace);

            Cursor.Current = Cursors.Arrow;
        }

        [CommandHandler("REP_DWProfitForCompose")]
        public void OpenProfitForCompose(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //利润组成
            //

            Cursor.Current = Cursors.WaitCursor;
            
            ICollection<ProfitForComposeWorkItem> list = this._rootWorkItem.Items.FindByType<ProfitForComposeWorkItem>();
            if (list.Count > 0)
            {
                foreach (ProfitForComposeWorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }

            ProfitForComposeWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<ProfitForComposeWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
            costDetailItem.Show(workspace);

            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>
        /// 业务信息表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("REP_DWJobInformation")]
        public void OpenJobInformation(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //业务信息
            //

            if (_transportFoundationService == null || _repBaseDataService == null)
            {
                PortfolioAndProfitDetailWorkItem item = this._rootWorkItem.Items.AddNew<PortfolioAndProfitDetailWorkItem>();
                item.GetService();
                _transportFoundationService = REPModuleInit.FoundationService;
                _repBaseDataService = REPModuleInit.RepService;
            }

            Cursor.Current = Cursors.WaitCursor;
            
            ICollection<JobInformationWorkItem> list = this._rootWorkItem.Items.FindByType<JobInformationWorkItem>();
            if (list.Count > 0)
            {
                foreach (JobInformationWorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }

            JobInformationWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<JobInformationWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
            costDetailItem.Show(workspace);

            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>
        /// 箱量趋势表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("RPT_ALLGetDirectionForTEU")]
        public void OPENALLGetDirectionForTEU(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
    
            //
            Cursor.Current = Cursors.WaitCursor;

            ICollection<TEUForDirectionWorkItem> list = this._rootWorkItem.Items.FindByType<TEUForDirectionWorkItem>();
            if (list.Count > 0)
            {
                foreach (TEUForDirectionWorkItem item in list)
                {
                    item.Show(workspace, Utility.GetValueString("箱量趋势表", "箱量趋势表"));      
                    return;
                }
            }


            TEUForDirectionWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<TEUForDirectionWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }

            costDetailItem.Show(workspace, Utility.GetValueString("箱量趋势表", "箱量趋势表"));            
            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>
        /// 利润趋势表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("RPT_ALLGetDirectionForProfit")]
        public void OPENALLGetDirectionForProfit(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //利润趋势表
            //
            Cursor.Current = Cursors.WaitCursor;

            ICollection<ProfitForDirectionWorkItem> list = this._rootWorkItem.Items.FindByType<ProfitForDirectionWorkItem>();
            if (list.Count > 0)
            {
                foreach (ProfitForDirectionWorkItem item in list)
                {
                    item.Show(workspace, Utility.GetValueString("利润趋势表", "利润趋势表") );
                    return;
                }
            }


            ProfitForDirectionWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<ProfitForDirectionWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }

            costDetailItem.Show(workspace, Utility.GetValueString("利润趋势表", "利润趋势表"));
            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>
        /// 利润同期对比图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("RPT_ALLGetSameTermCompareForProfit")]
        public void OPENALLGetSameTermCompareForProfit(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //利润趋势表
            //
            Cursor.Current = Cursors.WaitCursor;

            ICollection<SameTermCompareForPorfitWorkItem> list = this._rootWorkItem.Items.FindByType<SameTermCompareForPorfitWorkItem>();
            if (list.Count > 0)
            {
                foreach (SameTermCompareForPorfitWorkItem item in list)
                {
                    item.Show(workspace, Utility.GetValueString("利润同期对比图", "利润同期对比图"));
                    return;
                }
            }


            SameTermCompareForPorfitWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<SameTermCompareForPorfitWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }

            costDetailItem.Show(workspace, Utility.GetValueString("利润同期对比图", "利润同期对比图"));
            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>
        /// 箱量同期对比图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("RPT_ALLGetSameTermCompareForT")]
        public void OPENALLGetSameTermCompareForT(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //利润趋势表
            //
            Cursor.Current = Cursors.WaitCursor;

            ICollection<SameTermCompareForTWorkItem> list = this._rootWorkItem.Items.FindByType<SameTermCompareForTWorkItem>();
            if (list.Count > 0)
            {
                foreach (SameTermCompareForTWorkItem item in list)
                {
                    item.Show(workspace, Utility.GetValueString("箱量同期对比图", "箱量同期对比图"));
                    return;
                }
            }


            SameTermCompareForTWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<SameTermCompareForTWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }

            costDetailItem.Show(workspace, Utility.GetValueString("箱量同期对比图", "箱量同期对比图"));
            Cursor.Current = Cursors.Arrow;
        }


        /// <summary>
        /// 业务数据核对表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("GetJobInfoForCargoTracking_SearchWorkItem")]
        public void OPENGetJobInfoForCargoTracking_Search(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //业务数据核对表
            //
            Cursor.Current = Cursors.WaitCursor;

            ICollection<GetJobInfoForCargoTracking_SearchWorkItem> list = this._rootWorkItem.Items.FindByType<GetJobInfoForCargoTracking_SearchWorkItem>();
            if (list.Count > 0)
            {
                foreach (GetJobInfoForCargoTracking_SearchWorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }


            GetJobInfoForCargoTracking_SearchWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<GetJobInfoForCargoTracking_SearchWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }

            costDetailItem.Show(workspace);
            Cursor.Current = Cursors.Arrow;
        }


        /// <summary>
        /// 成本分析表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("RPT_ALLCostFor_Total")]
        public void OPENRPT_ALLCostFor_Total(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //RPT_ALLCostFor_Total
            //
            Cursor.Current = Cursors.WaitCursor;

            ICollection<TotalCost_WorkItem> list = this._rootWorkItem.Items.FindByType<TotalCost_WorkItem>();
            if (list.Count > 0)
            {
                foreach (TotalCost_WorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }


            TotalCost_WorkItem costItem = this._rootWorkItem.Items.AddNew<TotalCost_WorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }

            costItem.Show(workspace);
         
            Cursor.Current = Cursors.Arrow;
        }



        /// <summary>
        /// 成本趋势分析表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("RPT_ALLCostForDirection")]
        public void OPENRPT_TotalCosttDirection(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //RPT_ALLCostFor_Total
            //
            Cursor.Current = Cursors.WaitCursor;

            ICollection<TotalCostDirection_WorkItem> list = this._rootWorkItem.Items.FindByType<TotalCostDirection_WorkItem>();
            if (list.Count > 0)
            {
                foreach (TotalCostDirection_WorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }


            TotalCostDirection_WorkItem costItem = this._rootWorkItem.Items.AddNew<TotalCostDirection_WorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }

            costItem.Show(workspace);
            Cursor.Current = Cursors.Arrow;
        }


        /// <summary>
        /// 成本同期比较分析表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("RPT_ALLCostForSameTermCompare")]
        public void OPENRPT_CostForSameTermCompare(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //RPT_ALLCostFor_Total
            //
            Cursor.Current = Cursors.WaitCursor;

            ICollection<TotalCostSameTermCompare_WorkItem> list = this._rootWorkItem.Items.FindByType<TotalCostSameTermCompare_WorkItem>();
            if (list.Count > 0)
            {
                foreach (TotalCostSameTermCompare_WorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }


            TotalCostSameTermCompare_WorkItem costItem = this._rootWorkItem.Items.AddNew<TotalCostSameTermCompare_WorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }

            costItem.Show(workspace);
            Cursor.Current = Cursors.Arrow;
        }

        [CommandHandler("REP_WorkLoadForOperator")]
        public void OpenWorkLoadForOperator(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            //
            //操作工作量统计

            //

            Cursor.Current = Cursors.WaitCursor;
            
            ICollection<WorkLoadForOperatorWorkItem> list = this._rootWorkItem.Items.FindByType<WorkLoadForOperatorWorkItem>();
            if (list.Count > 0)
            {
                foreach (WorkLoadForOperatorWorkItem item in list)
                {
                    item.Show(workspace);
                    return;
                }
            }

            WorkLoadForOperatorWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<WorkLoadForOperatorWorkItem>();
            if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
            costDetailItem.Show(workspace);

            Cursor.Current = Cursors.Arrow;
        }

        [CommandHandler("REP_AgentForOperator")]
        public void OpenAgentForOperator(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            Cursor.Current = Cursors.WaitCursor;

            AgentForOperatorWorkitem agentForOperatorWorkitem = this._rootWorkItem.Items.Get<AgentForOperatorWorkitem>("AgentForOperatorWorkitem");
            if (agentForOperatorWorkitem != null)
            {
                //IWorkspace workspace = this._rootWorkItem.Workspaces["MainWorkspace"];
                //workspace.Activate(agentForOperatorWorkitem);
                agentForOperatorWorkitem.Run();
            }
            else 
            {
                agentForOperatorWorkitem = this._rootWorkItem.Items.AddNew<AgentForOperatorWorkitem>("AgentForOperatorWorkitem");
                agentForOperatorWorkitem.Run();
            }

            Cursor.Current = Cursors.Arrow;
        }

        [CommandHandler("REP_OEBussinesInfo")]
        public void OpenOEBussinesInfo(object sender, EventArgs e)
        {
            if (this._repWorkItem == null)
            {
                this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                this._repWorkItem.InitREPData();
            }
            Cursor.Current = Cursors.WaitCursor;

            OEBussinesInfoWorkitem oEBussinesInfoWorkitem = this._rootWorkItem.Items.Get<OEBussinesInfoWorkitem>("OEBussinesInfoWorkitem");
            if (oEBussinesInfoWorkitem != null)
            {
                //IWorkspace workspace = this._rootWorkItem.Workspaces["MainWorkspace"];
                oEBussinesInfoWorkitem.Run();
            }
            else
            {
                oEBussinesInfoWorkitem = this._rootWorkItem.Items.AddNew<OEBussinesInfoWorkitem>("OEBussinesInfoWorkitem");
                oEBussinesInfoWorkitem.Run();
            }

            Cursor.Current = Cursors.Arrow;
        }

         [CommandHandler("REP_CostDetail")]
         public void OpenCostDetail(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //个人帐单
             //
             Cursor.Current = Cursors.WaitCursor;

             ICollection<Cost_DetailWorkItem> list = this._rootWorkItem.Items.FindByType<Cost_DetailWorkItem>();
             if (list.Count > 0)
             {
                 foreach (Cost_DetailWorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }

             Cost_DetailWorkItem DetailItem = this._rootWorkItem.Items.AddNew<Cost_DetailWorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             DetailItem.Show(workspace);

             Cursor.Current = Cursors.Arrow;
         }

        /// <summary>
         /// 经营状况分析报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         [CommandHandler("RPT_AnalysisOfOperatingConditions")]
         public void OpenAnalysisOfOperatingConditions(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //经营状况分析报表
             //
             Cursor.Current = Cursors.WaitCursor;

             ICollection<AnalysisOfOperatingConditions_WorkItem> list = this._rootWorkItem.Items.FindByType<AnalysisOfOperatingConditions_WorkItem>();
             if (list.Count > 0)
             {
                 foreach (AnalysisOfOperatingConditions_WorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }

             AnalysisOfOperatingConditions_WorkItem DetailItem = this._rootWorkItem.Items.AddNew<AnalysisOfOperatingConditions_WorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             DetailItem.Show(workspace);

             Cursor.Current = Cursors.Arrow;
         }

        #endregion

         #region 出口财务

         [CommandHandler("RPT_ALLGetVoucherInfo")]
         public void OpenALLGetVoucherInfo(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //凭证查看
             //
             Cursor.Current = Cursors.WaitCursor;

             ICollection<ViewVoucherWorkItem> list = this._rootWorkItem.Items.FindByType<ViewVoucherWorkItem>();
             if (list.Count > 0)
             {
                 foreach (ViewVoucherWorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }


             LongWin.DataWarehouseReport.WinUI.ViewVoucherWorkItem DetailItem = this._rootWorkItem.Items.AddNew<LongWin.DataWarehouseReport.WinUI.ViewVoucherWorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             DetailItem.Show(workspace);

             Cursor.Current = Cursors.Arrow;
         }

         [CommandHandler("REP_Commision")]
         public void OpenCommision(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //退佣//
             Cursor.Current = Cursors.WaitCursor;
             ICollection<LongWin.DataWarehouseReport.WinUI.CommisionWorkItem> list = this._rootWorkItem.Items.FindByType<LongWin.DataWarehouseReport.WinUI.CommisionWorkItem>();
             if (list.Count > 0)
             {
                 foreach (LongWin.DataWarehouseReport.WinUI.CommisionWorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }

             LongWin.DataWarehouseReport.WinUI.CommisionWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<LongWin.DataWarehouseReport.WinUI.CommisionWorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             costDetailItem.Show(workspace);

             Cursor.Current = Cursors.Arrow;
         }

         [CommandHandler("REP_DcNoteDR")]
         public void OpenDebitNoteDR(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //应收表 //
             Cursor.Current = Cursors.WaitCursor;

             ICollection<LongWin.DataWarehouseReport.WinUI.DcNoteForDRWorkItem> list = this._rootWorkItem.Items.FindByType<LongWin.DataWarehouseReport.WinUI.DcNoteForDRWorkItem>();
             if (list.Count > 0)
             {
                 foreach (LongWin.DataWarehouseReport.WinUI.DcNoteForDRWorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }

             LongWin.DataWarehouseReport.WinUI.DcNoteForDRWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<LongWin.DataWarehouseReport.WinUI.DcNoteForDRWorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             costDetailItem.Show(workspace);
             Cursor.Current = Cursors.Arrow;
         }
         [CommandHandler("REP_DcNoteCR")]
         public void OpenDebitNoteCR(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //应收表
             //
             Cursor.Current = Cursors.WaitCursor;

             ICollection<LongWin.DataWarehouseReport.WinUI.DcNoteForCRWorkItem> list = this._rootWorkItem.Items.FindByType<LongWin.DataWarehouseReport.WinUI.DcNoteForCRWorkItem>();
             if (list.Count > 0)
             {
                 foreach (LongWin.DataWarehouseReport.WinUI.DcNoteForCRWorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }

             LongWin.DataWarehouseReport.WinUI.DcNoteForCRWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<LongWin.DataWarehouseReport.WinUI.DcNoteForCRWorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             costDetailItem.Show(workspace);

             Cursor.Current = Cursors.Arrow;
         }

         [CommandHandler("REP_DcNoteForAge")]
         public void OpenDcNoteForAge(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //帐龄表  //

             Cursor.Current = Cursors.WaitCursor;

             ICollection<LongWin.DataWarehouseReport.WinUI.DcNoteAgeForDRTotalWorkItem> list = this._rootWorkItem.Items.FindByType<LongWin.DataWarehouseReport.WinUI.DcNoteAgeForDRTotalWorkItem>();
             if (list.Count > 0)
             {
                 foreach (LongWin.DataWarehouseReport.WinUI.DcNoteAgeForDRTotalWorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }

             LongWin.DataWarehouseReport.WinUI.DcNoteAgeForDRTotalWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<LongWin.DataWarehouseReport.WinUI.DcNoteAgeForDRTotalWorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             costDetailItem.Show(workspace);

             Cursor.Current = Cursors.Arrow;
         }

         [CommandHandler("REP_DcNoteForAgent")]
         public void OpenDcNoteForAgent(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //代理对帐
             //

             Cursor.Current = Cursors.WaitCursor;

             ICollection<LongWin.DataWarehouseReport.WinUI.DebitnoteForAgentWorkItem> list = this._rootWorkItem.Items.FindByType<LongWin.DataWarehouseReport.WinUI.DebitnoteForAgentWorkItem>();
             if (list.Count > 0)
             {
                 foreach (LongWin.DataWarehouseReport.WinUI.DebitnoteForAgentWorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }

             LongWin.DataWarehouseReport.WinUI.DebitnoteForAgentWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<LongWin.DataWarehouseReport.WinUI.DebitnoteForAgentWorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             costDetailItem.Show(workspace);

             Cursor.Current = Cursors.Arrow;
         }

         [CommandHandler("REP_DebitNoteForProfit")]
         public void OpenDebitNoteForProfit(object sender, EventArgs e)
         {
             if (this._repWorkItem == null)
             {
                 this._repWorkItem = this._rootWorkItem.Items.AddNew<REPWrokItem>();
                 this._repWorkItem.InitREPData();
             }
             //
             //盈余表 //
             Cursor.Current = Cursors.WaitCursor;

             ICollection<LongWin.DataWarehouseReport.WinUI.DcNoteForProfitDetailWorkItem> list = this._rootWorkItem.Items.FindByType<LongWin.DataWarehouseReport.WinUI.DcNoteForProfitDetailWorkItem>();
             if (list.Count > 0)
             {
                 foreach (LongWin.DataWarehouseReport.WinUI.DcNoteForProfitDetailWorkItem item in list)
                 {
                     item.Show(workspace);
                     return;
                 }
             }

             LongWin.DataWarehouseReport.WinUI.DcNoteForProfitDetailWorkItem costDetailItem = this._rootWorkItem.Items.AddNew<LongWin.DataWarehouseReport.WinUI.DcNoteForProfitDetailWorkItem>();
             if (this.workspace == null) { workspace = this._rootWorkItem.Workspaces["MainWorkspace"]; }
             costDetailItem.Show(workspace);

             Cursor.Current = Cursors.Arrow;
         }

        #endregion
    }
}
