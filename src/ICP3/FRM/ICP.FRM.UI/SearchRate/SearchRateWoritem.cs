using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FRM.UI.SearchRate
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchRateWoritem : WorkItem
    {

        #region Show
        /// <summary>
        /// 
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Show()
        {
            SearchRateMainWorkspace srMainWorkspace = SmartParts.Get<SearchRateMainWorkspace>("SearchRateMainWorkspace");
            if (srMainWorkspace == null)
            {
                srMainWorkspace = SmartParts.AddNew<SearchRateMainWorkspace>("SearchRateMainWorkspace");


                SearchOceanWorkItem oceanWorkitem = WorkItems.AddNew<SearchOceanWorkItem>();
                oceanWorkitem.Run();

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = "Search Rates";
                mainWorkspace.Show(srMainWorkspace, smartPartInfo);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(srMainWorkspace);
            }
        }

        #endregion
    }

    /// <summary>
    /// SearchRate WorkSpace 常量
    /// </summary>
    public class SearchRateWorkSpaceConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public const string OceanMainWorkspace = "OceanMainWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string AirMainWorkspace = "AirMainWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string TruckMainWorkspace = "TruckMainWorkspace";

    }

    /// <summary>
    /// 权限命令常量
    /// </summary>
    public class PermissionCommandConstants
    {
        /// <summary>
        ///  查询海运运价--通用
        /// </summary>
        public const string SEARCHOCEAN_GENERAL = "SEARCHOCEAN_GENERAL";
        /// <summary>
        /// 查询海运运价--查看底价
        /// </summary>
        public const string SEARCHOCEAN_VIEWRESERVE = "SEARCHOCEAN_VIEWRESERVE";
        /// <summary>
        /// 查询海运运价--查看合约信息
        /// </summary>
        public const string SEARCHOCEAN_VIEWCONTRACTNO = "SEARCHOCEAN_VIEWCONTRACTNO";
        /// <summary>
        /// 查询海运运价--查看合约佣金信息
        /// </summary>
        public const string SEARCHOCEAN_VIEWCOMMISSION = "SEARCHOCEAN_VIEWCOMMISSION";
        /// <summary>
        /// 查询海运运价--导出合约
        /// </summary>
        public const string SEARCHOCEAN_EXPORTTOEXCEL = "SEARCHOCEAN_EXPORT";
    }
}
