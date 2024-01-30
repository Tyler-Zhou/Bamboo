namespace ICP.ReportCenter.UI
{
    using ICP.Framework.ClientComponents.Service;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.ReportCenter.UI.BusinessReports;
    using ICP.ReportCenter.UI.CRMReports;
    using ICP.ReportCenter.UI.FCMReports;
    using ICP.ReportCenter.UI.FinanceOEReports;
    using ICP.ReportCenter.UI.FinanceOEReports._4帐龄表;
    using ICP.ReportCenter.UI.FinanceOIReports;
    using ICP.ReportCenter.UI.FinanceReports;
    using ICP.ReportCenter.UI.UFReports;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using System;

    public class ReportCenterModuleInit : ModuleInit
    {
        #region Init
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;

        public ReportCenterModuleInit(
            [ServiceDependency]WorkItem rootWorkItem
            )
        {
            _rootWorkItem = rootWorkItem;
        }
        #endregion

        #region Business

        #region 箱量利润统计表

        /// <summary>
        /// 箱量利润统计表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_DWPortfolioAndProfitTotal)]
        public void Open_PortfolioAndProfitTotal(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            ReportWorkitem<PortfolioAndProfitTotalSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PortfolioAndProfitTotalSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Volume and Profit By Total" : "箱量利润统计表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 箱量利润明细表

        /// <summary>
        /// 箱量利润明细表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_DWPortfolioAndProfitDetail)]
        public void Open_RP_DWPortfolioAndProfitDetail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<PortfolioAndProfitDetailSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PortfolioAndProfitDetailSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Volume and Profit By Detail" : "箱量利润详细表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 商务箱量统计表

        /// <summary>
        /// 商务箱量统计表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_CommercePortfolioAndProfitForGeneral_Total)]
        public void Open_RP_CommercePortfolioAndProfitForGeneral_Total(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<PortfolioAndProfitForGeneral_TotalSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PortfolioAndProfitForGeneral_TotalSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Business volume and profit by Detail" : "商务箱量统计表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 商务箱量详细表

        /// <summary>
        /// 商务箱量详细表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_CommercePortfolioAndProfitForGeneral_Detail)]
        public void Open_RP_CommercePortfolioAndProfitForGeneral_Detail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<PortfolioAndProfitForGeneral_DetailSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PortfolioAndProfitForGeneral_DetailSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Business volume and profit by Detail" : "商务箱量详细表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 利润组成分析

        /// <summary>
        /// 利润组成分析
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_DWProfitForCompose)]
        public void Open_RP_DWProfitForCompose(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ProfitForComposeSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ProfitForComposeSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Profit Constitutes Analysis" : "利润组成分析表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 单箱利润分析

        /// <summary>
        /// 单箱利润分析
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_DWPorfitForT)]
        public void Open_RP_DWPorfitForT(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<PorfitForTSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PorfitForTSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Per Container Of Profit Graph" : "单箱利润表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 利润趋势分析

        /// <summary>
        /// 利润趋势分析
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ALLGetDirectionForProfit)]
        public void Open_RP_ALLGetDirectionForProfit(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ProfitForDirectionSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ProfitForDirectionSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Profit Trend Analysis" : "利润趋势表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 箱量趋势分析

        /// <summary>
        /// 箱量趋势分析
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ALLGetDirectionForTEU)]
        public void Open_RP_ALLGetDirectionForTEU(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<DirectionForTEUSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<DirectionForTEUSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Volume Trend Analysis" : "箱量趋势表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 利润同期对比分析

        /// <summary>
        /// 利润同期对比分析
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ALLGetSameTermCompareForProfit)]
        public void Open_RP_ALLGetSameTermCompareForProfit(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<SameTermCompareForProfitSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<SameTermCompareForProfitSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Profit Same Term Compare Analysis" : "利润同期对比图";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 箱量同期对比分析


        /// <summary>
        /// 箱量同期对比分析
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ALLGetSameTermCompareForT)]
        public void Open_RP_ALLGetSameTermCompareForT(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<SameTermCompareForTSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<SameTermCompareForTSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "TEU Same Term Compare Analysis" : "箱量同期对比图";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 成本组成分析

        /// <summary>
        /// 成本组成分析
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ALLCostFor_Total)]
        public void Open_RP_ALLCostFor_Total(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CostFor_TotalSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CostFor_TotalSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Cost Constitutes Analysis" : "成本分析图";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 成本趋势分析

        /// <summary>
        /// 成本趋势分析
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ALLCostForDirection)]
        public void Open_RP_ALLCostForDirection(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CostForDirectionSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CostForDirectionSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Cost Direction Analysis" : "成本趋势分析图";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 成本同期对较

        /// <summary>
        /// 成本同期对较
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ALLCostForSameTermCompare)]
        public void Open_RP_ALLCostForSameTermCompare(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CostForSameTermCompareSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CostForSameTermCompareSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Cost Same Term Compare Analysis" : "成本同期比较分析图";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 经营状况分析表

        /// <summary>
        /// 经营状况分析表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_AnalysisOfOperatingConditions)]
        public void Open_RP_AnalysisOfOperatingConditions(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<AnalysisOfOperatingConditionsSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<AnalysisOfOperatingConditionsSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Analysis Of Operating Statement" : "经营状况分析表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 成本分析-个人帐单

        /// <summary>
        /// 成本分析-个人帐单
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_CostDetail)]
        public void Open_RP_CostDetail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CostDetailSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CostDetailSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Cost-Individual Expense" : "成本分析-个人帐单";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 业务员利润提成表

        /// <summary>
        /// 业务员利润提成表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ALLPortfolioAndProfitForSalesCustomer)]
        public void Open_RP_ALLPortfolioAndProfitForSalesCustomer(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<PortfolioAndProfitForSalesCustomer_Total> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PortfolioAndProfitForSalesCustomer_Total>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "PortfolioAndProfitForSalesCustomer" : "业务员利润提成表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 航线箱量统计报表
        /// <summary>
        /// 航线箱量统计报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BusinessFunctionConstants.RP_ContainerVolumeForShippingLine_Total)]
        public void Open_RP_ContainerVolumeForShippingLine_Total(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ContainerVolumeTotalSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ContainerVolumeTotalSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Business volume and profit by Detail" : "航线箱量统计表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 进口箱量统计表

        /// <summary>
        /// 进口箱量统计表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.ImportContainerList)]
        public void ImportContainerList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ImportContainerList> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ImportContainerList>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Import Business Container Volume" : "进口箱量统计表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 出口箱信息列表
        /// <summary>
        /// 出口箱信息列表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_ExportContainerList)]
        public void Open_RP_ExportContainerList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ExportContainerList> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ExportContainerList>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Export Container List" : "出口箱信息列表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 目的港返利表
        /// <summary>
        /// 目的港返利表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_DESTINATIONRETURNPROFIT)]
        public void Open_RP_AgentReturnProfit(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<DestinationReturnProfit> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<DestinationReturnProfit>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Destination Return Profit" : "目的港返利表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 配比汇总
        /// <summary>
        /// 配比汇总(配比余额表)
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_PROFITRATIOS_SUMMARY)]
        public void Open_RP_ProfitRatios_Summary(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ProfitRatiosSummary> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ProfitRatiosSummary>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Profit Ratios" : "配比余额表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 本地服务详细表
        /// <summary>
        /// 本地服务详细表
        /// </summary>
        [CommandHandler(BusinessFunctionConstants.RP_LOCALSERVICE_DETAIL)]
        public void Open_RP_LocalService_Detail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<LocalServiceDetail> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<LocalServiceDetail>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Local Service Detail" : "本地服务详细表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #endregion


        #region 出口财务

        #region 业务管理成本

        /// <summary>
        /// 业务管理成本
        /// </summary>
        [CommandHandler(FIN_OEFunctionConstants.RP_Commision)]
        public void Open_RP_Commision(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CommisionSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CommisionSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Return Commission Report" : "业务管理成本统计表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 应收帐款表

        /// <summary>
        /// 应收帐款表
        /// </summary>
        [CommandHandler(FIN_OEFunctionConstants.RP_DcNoteDR)]
        public void Open_RP_DcNoteDR(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<DcNoteDRSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<DcNoteDRSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Account Receivable Report" : "应收帐款表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 应付帐款表

        /// <summary>
        /// 应付帐款表
        /// </summary>
        [CommandHandler(FIN_OEFunctionConstants.RP_DcNoteCR)]
        public void Open_RP_DcNoteCR(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<DcNoteCRSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<DcNoteCRSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Account Payable Report" : "应付帐款表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 帐龄表

        /// <summary>
        /// 帐龄表
        /// </summary>
        [CommandHandler(FIN_OEFunctionConstants.RP_DcNoteForAge)]
        public void Open_RP_DcNoteForAge(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<DcNoteForAgeSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<DcNoteForAgeSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Aging Statement" : "应收帐款帐龄表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 应收帐款帐龄统计表

        /// <summary>
        /// 应收帐款帐龄统计表
        /// </summary>
        [CommandHandler(FIN_OEFunctionConstants.RP_DcNoteForAgeSum)]
        public void Open_RP_DcNoteForAgeSum(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<DcNoteForAgeDetailsSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<DcNoteForAgeDetailsSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Aging Statement Sum" : "应收帐款帐龄统计表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 代理对帐表

        /// <summary>
        /// 代理对帐表
        /// </summary>
        [CommandHandler(FIN_OEFunctionConstants.RP_DcNoteForAgent)]
        public void Open_RP_DcNoteForAgent(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<DcNoteForAgentSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<DcNoteForAgentSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Agent Statement Report" : "代理帐款表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 应收应付盈余表

        /// <summary>
        /// 应收应付盈余表
        /// </summary>
        [CommandHandler(FIN_OEFunctionConstants.RP_DebitNoteForProfit)]
        public void Open_RP_DebitNoteForProfit(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<DebitNoteForProfitSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<DebitNoteForProfitSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Surplus Statement Report" : "应收应付盈余表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 凭证明细列表

        /// <summary>
        /// 凭证明细列表
        /// </summary>
        [CommandHandler(FIN_OEFunctionConstants.RP_ALLGetVoucherInfo)]
        public void Open_RP_ALLGetVoucherInfo(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<VoucherInfoSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<VoucherInfoSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Voucher List" : "凭证明细表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #endregion

        #region 财务

        #region 会计科目余额报表

        /// <summary>
        /// 科目余额表
        /// </summary>
        [CommandHandler(FIN_FunctionConstants.RP_GLSummary)]
        public void Open_RP_GLSummary(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<GLSummarySearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<GLSummarySearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "GL Summary Report" : "会计科目余额报表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 试算平衡表

        /// <summary>
        /// 试算平衡表
        /// </summary>
        [CommandHandler(FIN_FunctionConstants.RP_TrialBalance)]
        public void Open_RP_TrialBalance(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<TrialBalanceSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<TrialBalanceSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Trial Balance Report" : "试算平衡表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 资产负债表

        /// <summary>
        /// 资产负债表
        /// </summary>
        [CommandHandler(FIN_FunctionConstants.RP_BalanceSheetReport)]
        public void Open_RP_BalanceSheetReport(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<BalanceSheetSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<BalanceSheetSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Balance Sheet" : "资产负债表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 损益表

        /// <summary>
        /// 损益表
        /// </summary>
        [CommandHandler(FIN_FunctionConstants.RP_InComeStatementReport)]
        public void Open_RP_InComeStatementReport(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<InComeStatementSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<InComeStatementSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Income Statement Report" : "损益表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 银行对帐表

        /// <summary>
        /// 银行对帐表
        /// </summary>
        [CommandHandler(FIN_FunctionConstants.RP_BankstandingReport)]
        public void Open_RP_BankstandingReport(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<BankstandingSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<BankstandingSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Bank Outstanding Report" : "银行对帐表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }
            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 日记帐报表

        /// <summary>
        /// 日记帐报表
        /// </summary>
        [CommandHandler(FIN_FunctionConstants.RP_Journal_Report)]
        public void Open_RP_Journal_Report(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<Journal_ReportSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<Journal_ReportSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Miscellaneous Transaction Report" : "杂项交易报表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #endregion

        #region 销售中心 - 业务信息表

        /// <summary>
        /// 业务信息表
        /// </summary>
        [CommandHandler(CRMFunctionConstants.RP_DWJobInformation)]
        public void Open_RP_DWJobInformation(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<JobInformationSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<JobInformationSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Job Information Detail" : "业务信息详细";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 业务活动统计
        /// </summary>
        [CommandHandler(CRMFunctionConstants.RP_DevCRMSalesActivity)]
        public void Open_RP_DevCRMSalesActivity(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<SalesActivitySearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<SalesActivitySearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Sales Activity Total" : "业务活动统计";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 经理分析统计
        /// </summary>
        [CommandHandler(CRMFunctionConstants.RP_DevCRMCustomerAnalysis)]
        public void Open_RP_DevCRMCustomerAnalysis(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CustomerAnalysisSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CustomerAnalysisSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Manager Analysis" : "经理分析统计";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 合作状态统计
        /// </summary>
        [CommandHandler(CRMFunctionConstants.RP_DevCRMCustomerState)]
        public void Open_RP_DevCRMCustomerState(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CustomerStateSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CustomerStateSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Customer State" : "客户合作状态统计";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 业务阶段统计
        /// </summary>
        [CommandHandler(CRMFunctionConstants.RP_DevCRMCustomerPhase)]
        public void Open_RP_DevCRMCustomerPhase(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CustomerPhaseSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CustomerPhaseSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Customer Phase" : "业务阶段统计";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 操作报表

        #region 操作工作量统计
        /// <summary>
        /// 操作工作量统计
        /// </summary>
        [CommandHandler(FCMFunctionConstants.RP_WorkLoadForOperator)]
        public void Open_RP_WorkLoadForOperator(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<WorkLoadForOperatorSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<WorkLoadForOperatorSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Workload Statistics" : "操作工作量统计";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 操作指定货报表
        /// <summary>
        /// 操作指定货报表
        /// </summary>
        [CommandHandler(FCMFunctionConstants.RP_AgentForOperator)]
        public void Open_RP_AgentForOperator(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<AgentForOperatorSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<AgentForOperatorSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Operator Agent Report" : "操作指定货报表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 出口业务信息报表
        /// <summary>
        /// 出口业务信息报表
        /// </summary>
        [CommandHandler(FCMFunctionConstants.RP_OEBussinesInfo)]
        public void Open_RP_OEBussinesInfo(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<OEBussinesInfoSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<OEBussinesInfoSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "OEBussinesInfo Report" : "出口业务信息报表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 业务数据核对表
        /// <summary>
        /// 业务数据核对表
        /// </summary>
        [CommandHandler(FCMFunctionConstants.RP_JobInfoForCargoTracking)]
        public void Open_RP_JobInfoForCargoTracking(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<JobInfoForCargoTrackingSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<JobInfoForCargoTrackingSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Job For CargoTracking" : "业务数据核对表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #endregion

        #region 进口财务

        #region 帐龄表
        /// <summary>
        /// 帐龄表
        /// </summary>
        [CommandHandler(FIN_OIFunctionConstants.RP_AgingReport)]
        public void Open_RP_AgingReport(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<AgingReportSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<AgingReportSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Aging Report" : "进口账龄表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 代理对帐表  暂时搁置
        /// <summary>
        /// 代理对帐表
        /// </summary>
        [CommandHandler(FIN_OIFunctionConstants.RP_AgentStatement)]
        public void Open_RP_AgentStatement(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<AgentStatementSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<AgentStatementSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Agent Statement Report" : "Agent Statement Report";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 对帐表
        /// <summary>
        /// 对帐表
        /// </summary>
        [CommandHandler(FIN_OIFunctionConstants.RP_LocalStatement)]
        public void Open_RP_LocalStatement(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<LocalStatementSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<LocalStatementSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Local Statement Report" : "进口对帐表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 收支报表
        /// <summary>
        /// 收支报表
        /// </summary>
        [CommandHandler(FIN_OIFunctionConstants.RP_CheckDeposit)]
        public void Open_RP_CheckDeposit(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CheckDepositSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CheckDepositSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Check/Deposit List" : "Check/Deposit List";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 收支报表CA
        /// <summary>
        /// 收支报表
        /// </summary>
        [CommandHandler(FIN_OIFunctionConstants.RP_CheckDepositCA)]
        public void Open_RP_CheckDepositCA(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CheckDepositCASearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CheckDepositCASearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Check/Deposit List(CA)" : "Check/Deposit List(CA)";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 预收预付报表
        /// <summary>
        /// 预收预付报表
        /// </summary>
        [CommandHandler(FIN_OIFunctionConstants.RP_PrepaidInAdvance)]
        public void Open_RP_PrepaidInAdvance(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<PrepaidInAdvanceSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PrepaidInAdvanceSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "PrepaidInAdvance List" : "PrepaidInAdvance List";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion



        #endregion

        #region 会计统计报表

        #region 科目余额表
        /// <summary>
        /// 科目余额表
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_GLBalanceData)]
        public void Open_RP_GLBalanceData(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<GLBalanceSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<GLBalanceSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "GLBalance Report" : "科目余额表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 科目明细帐
        /// <summary>
        /// 科目明细帐
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_GLDetailData)]
        public void Open_RP_GLDetailData(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<GLDetailDataSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<GLDetailDataSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "GLDetail Report" : "科目明细帐";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 客户科目余额表
        /// <summary>
        /// 客户科目余额表
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_CustomerGLBalance)]
        public void Open_RP_CustomerGLBalance(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CustomerGLBalanceSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CustomerGLBalanceSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "CustomerGLBalance Report" : "客户科目余额表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 客户科目三栏余额表
        /// <summary>
        /// 客户科目三栏余额表
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_Customer3ColumnGLBalance)]
        public void Open_RP_Customer3ColumnGLBalance(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<Customer3ColumnGLBalanceSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<Customer3ColumnGLBalanceSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Customer3ColumnGLBalance" : "客户三栏余额表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 客户明细帐
        /// <summary>
        /// 客户明细帐
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_CustomerGLDetail)]
        public void Open_RP_CustomerGLDetail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CustomerGLDetailSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CustomerGLDetailSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Customer Detail" : "客户明细帐";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 个人科目余额表
        /// <summary>
        /// 个人科目余额表
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_PersonalGLBalance)]
        public void Open_RP_PersonalGLBalance(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<PersonalGLBalanceSarchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PersonalGLBalanceSarchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "PersonalGLBalance Report" : "个人科目余额表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 个人科目三栏余额表
        /// <summary>
        /// 个人科目三栏余额表
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_Personal3ColumnGLBalance)]
        public void Open_RP_Personal3ColumnGLBalance(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<Personal3ColumnGLBalanceSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<Personal3ColumnGLBalanceSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Personal3ColumnGLBalance" : "个人三栏余额表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 个人明细帐
        /// <summary>
        /// 个人明细帐
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_PersonalGLDetail)]
        public void Open_RP_PersonalGLDetail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<PersonalGLDetailSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<PersonalGLDetailSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Personal Detail" : "个人明细帐";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 科目明细帐
        /// <summary>
        /// 科目明细帐
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_UFTest)]
        public void Open_RP_UFTest(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<UFTestSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<UFTestSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "UF Test" : "UF Test";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 资产负债表
        /// <summary>
        /// 资产负债表
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_BalanceSheetDetail)]
        public void Open_RP_BalanceSheetDetail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CompanyBalanceSheetDetailSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CompanyBalanceSheetDetailSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Balance Sheet Detail" : "资产负债表(明细)";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 资产负债表LA
        /// <summary>
        /// 资产负债表LA
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_BalanceSheetReportForLA)]
        public void Open_RP_BalanceSheetDetailLA(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CompanyBalanceSheetDetailForLASearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CompanyBalanceSheetDetailForLASearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Balance Sheet Detail(LA)" : "资产负债表(LA)";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 资产负债表(集团汇总)
        /// <summary>
        /// 资产负债表(集团汇总)
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_BalanceSheetReportForAll)]
        public void Open_RP_BalanceSheetReportForAll(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<CompanyBalanceSheetDetailSearchAllPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<CompanyBalanceSheetDetailSearchAllPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Balance Sheet Detail(Group Summary)" : "资产负债表(集团汇总)";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 费用分析表
        /// <summary>
        /// 费用分析表
        /// </summary>
        [CommandHandler(UF_FunctionConstants.RP_ExpenseAnalysisSheetDetail)]
        public void Open_RP_ExpenseAnalysisSheetDetail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ExpenseAnalysisSheetDetailSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ExpenseAnalysisSheetDetailSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Expense Analysis Sheet Detail" : "费用分析表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 利润表
        [CommandHandler(UF_FunctionConstants.RP_ProfitDetail)]
        public void Open_RP_ProfitDetail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ProfitDetailSearchPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ProfitDetailSearchPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Profit Sheet" : "利润表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 利润表集团汇总
        [CommandHandler(UF_FunctionConstants.RP_ProfitTotalAll)]
        public void Open_RP_ProfitDetailAll(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ProfitDetailSearchPartAll> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ProfitDetailSearchPartAll>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Profit Sheet(Group Summary)" : "利润表（集团汇总）";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 利润分配表
        [CommandHandler(UF_FunctionConstants.RP_ProfitAllocationDetail)]
        public void Open_RP_ProfitAllocationDetail(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            ReportWorkitem<ProfitAllocationDetailPart> workitem = _rootWorkItem.WorkItems.AddNew<ReportWorkitem<ProfitAllocationDetailPart>>();
            try
            {
                workitem.Titel = LocalData.IsEnglish ? "Profit Allocation" : "利润分配表";
                workitem.Run();
            }
            catch { workitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #endregion


    }
}
