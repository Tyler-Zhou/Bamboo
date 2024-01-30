using System;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
namespace ICP.FRM.UI.SearchRate
{
    public class SearchOceanWorkItem : WorkItem
    {
        #region Service

        public ISearchRatesService SearchRatesService 
        {
            get
            {
                return ServiceClient.GetService<ISearchRatesService>();
            }
        }

        #endregion

        #region Show

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            SearchOceanMainWorkspace srMainWorkspace = SmartParts.Get<SearchOceanMainWorkspace>("SearchOceanMainWorkspace");
            if (srMainWorkspace == null)
            {
                srMainWorkspace = SmartParts.AddNew<SearchOceanMainWorkspace>("SearchOceanMainWorkspace");

                SearchOceanSearchPart SOSerachPart = Items.AddNew<SearchOceanSearchPart>();
                IWorkspace searchOceanWorkspace = Workspaces[SearchOceanWorkSpaceConstants.SearchWorkspace];
                searchOceanWorkspace.Show(SOSerachPart);

                SearchOceanListPart SOListPart = Items.AddNew<SearchOceanListPart>();
                IWorkspace listWorkspace = Workspaces[SearchOceanWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(SOListPart);

                SearchOceanToolPart SOToolPart = Items.AddNew<SearchOceanToolPart>();
                IWorkspace toolWorkspace = Workspaces[SearchOceanWorkSpaceConstants.ToolBarWorkspace];
                toolWorkspace.Show(SOToolPart);

                SearchOceanBaseInfoPart SOBaseInfoPart = Items.AddNew<SearchOceanBaseInfoPart>();
                IWorkspace baseInfoWorkspace = Workspaces[SearchOceanWorkSpaceConstants.BaseInfoWorkspace];
                baseInfoWorkspace.Show(SOBaseInfoPart);

                SearchOceanContractInfoPart SOContractInfoPart = Items.AddNew<SearchOceanContractInfoPart>();
                IWorkspace contractInfoWorkspace = Workspaces[SearchOceanWorkSpaceConstants.ContractWorkspace];
                contractInfoWorkspace.Show(SOContractInfoPart);


                SearchOceanRemark SORemarkPart = Items.AddNew<SearchOceanRemark>();
                IWorkspace remarkWorkspace = Workspaces[SearchOceanWorkSpaceConstants.RemarkWorkspace];
                remarkWorkspace.Show(SORemarkPart);

                OceanRateFeeDetailPart SOFeesPart = Items.AddNew<OceanRateFeeDetailPart>();
                IWorkspace feeWorkspace = Workspaces[SearchOceanWorkSpaceConstants.FeesWorkspace];
                feeWorkspace.Show(SOFeesPart);

                #region 定义面板连接

                #region 查询 
                SOSerachPart.OnSearched += delegate(object sender, object results)
                {
                    SOListPart.DataSource = results;
                };
                #endregion

                #region 换行
                SOListPart.CurrentChanged += delegate(object sender, object data)
                {
                    SearchOceanRateList list = data as SearchOceanRateList;
                    SearchOceanBaseInfo baseInfo = null;
                    if (list != null && list.IsNew == false)
                    {
                        baseInfo = SearchRatesService.GetSearchOceanBaseInfo(list.ID, list.SearchrateType);
                        if (Utility.SearchOceanPermissionType == SearchPricePermission.ViewReserve)
                        {
                            foreach (var item in baseInfo.UnitList) { item.Rate = item.ReserveRate; }
                        }
                        else
                        {
                            foreach (var item in baseInfo.UnitList) { item.Rate = item.SalesRate; }
                        }


                    }
                    SOBaseInfoPart.DataSource = baseInfo;
                    SORemarkPart.DataSource = baseInfo;
                    if (LocalCommonServices.PermissionService.HaveActionPermission(PermissionCommandConstants.SEARCHOCEAN_VIEWCONTRACTNO))
                    {
                        SOContractInfoPart.DataSource = data;
                    }
                    SOFeesPart.DataSource = list.ID;
                };
                #endregion

                #region 翻页
                SOListPart.InvokeGetData += delegate(object sender, object data)
                {
                    SOSerachPart.RaiseSearched(data);
                };
                #endregion

                #region 显示备注
                SOBaseInfoPart.EventBroker_ShowRemark +=delegate(object sender, EventArgs e)
                {
                      srMainWorkspace.Command_ShowRemark(sender,e);
                      SORemarkPart.Command_ShowRemark(sender,e);

                };
                #endregion

                #region 显示文件
                SOBaseInfoPart.EventBroker_ShowAttachment += delegate(object sender, EventArgs e)
               {
                   srMainWorkspace.Command_ShowAttachment(sender, e);
                   SORemarkPart.Command_ShowAttachment(sender, e);

               };
                #endregion

                #endregion

                IWorkspace mainWorkspace = Workspaces[SearchRateWorkSpaceConstants.OceanMainWorkspace];
                mainWorkspace.Show(srMainWorkspace);
            }
            else
            {
                Workspaces[SearchRateWorkSpaceConstants.OceanMainWorkspace].Activate(srMainWorkspace);
            }
        }
        #endregion
    }

    #region Ocean
    /// <summary>
    /// SearchOcean WorkSpace 常量
    /// </summary>
    public class SearchOceanWorkSpaceConstants
    {
        /// <summary>
        /// 查询条件工作区
        /// </summary>
        public const string SearchWorkspace = "SearchWorkspace";
        /// <summary>
        /// 运价明细工作区
        /// </summary>
        public const string ListWorkspace = "ListWorkspace";
        /// <summary>
        /// 工具栏工作区
        /// </summary>
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        /// <summary>
        /// 基本信息工作区
        /// </summary>
        public const string BaseInfoWorkspace = "BaseInfoWorkspace";
        /// <summary>
        /// 合约工作区
        /// </summary>
        public const string ContractWorkspace = "ContractWorkspace";
        /// <summary>
        /// 备注工作区
        /// </summary>
        public const string RemarkWorkspace = "RemarkWorkspace";
        /// <summary>
        /// 费用工作区
        /// </summary>
        public const string FeesWorkspace = "FeesWorkspace";
        /// <summary>
        /// 附件列表工作区
        /// </summary>
        public const string AttachmentsWorkspace = "AttachmentsWorkspace";

        
    }

    /// <summary>
    /// 海运运价查询常量
    /// </summary>
    public class SearchOceanCommandConstants
    {
        /// <summary>
        /// 查询命令
        /// </summary>
        public const string Command_ShowSearch = "Command_ShowSearch";
        /// <summary>
        /// 刷新数据
        /// </summary>
        public const string Command_RefreshData = "Command_RefreshData";
        /// <summary>
        /// 导出Excel
        /// </summary>
        public const string Command_ExportToExcel = "Command_ExportToExcel";
        /// <summary>
        /// 升级到云服务
        /// </summary>
        public const string Command_UpgradeCloud = "Command_UpgradeCloud";
        /// <summary>
        /// 显示备注
        /// </summary>
        public const string Command_ShowRemark = "Command_ShowRemark";
        /// <summary>
        /// 显示附件
        /// </summary>
        public const string Command_ShowAttachment = "Command_ShowAttachment";
    }

    // /// <summary>
    ///// 海运运价查询常量
    ///// </summary>
    //public class SearchOceanEventBrokerConstants
    //{

    //    public const string EventBroker_ShowRemark = "EventBroker_ShowRemark";

    //    public const string EventBroker_ShowAttachment = "EventBroker_ShowAttachment";

    //}

    

    #endregion
}
