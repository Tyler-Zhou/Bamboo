using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FRM.UI.BookingReport;

namespace ICP.FRM.UI
{
    public class BookingReportWoritem : WorkItem
    {
        #region Service

        public IUIBuilder UIBuilder
        {
            get
            {
                return ServiceClient.GetClientService<IUIBuilder>();
            }
        }
        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            BookingReportMainWorkspace mainSpce = SmartParts.Get<BookingReportMainWorkspace>("BookingReportMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<BookingReportMainWorkspace>("BookingReportMainWorkspace");

                #region AddPart

                BookingReportListPart listPart = SmartParts.AddNew<BookingReportListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[BookingReportWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);


                BookingReportSearchPart searchPart = SmartParts.AddNew<BookingReportSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[BookingReportWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                BookingReportToolPart toolPart = SmartParts.AddNew<BookingReportToolPart>();
                IWorkspace toolWorkspace = (IWorkspace)Workspaces[BookingReportWorkSpaceConstants.ToolbarWorkspace];
                toolWorkspace.Show(toolPart);
                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Booking Report" : "订舱统计报表";
                mainWorkspace.Show(mainSpce, smartPartInfo);

                searchPart.OnSearched += delegate(object sender, object results)
                {
                    listPart.DataSource = results;
                };
                searchPart.GroupByChange += delegate(object sender, object data)
                {
                    List<BPGroupBy> list= data as List<BPGroupBy>;
                    listPart.GroupByList = list;
                    listPart.SetGroupBy();
                };
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }


    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class BookingReportWorkSpaceConstants
    {
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ToolbarWorkspace = "ToolbarWorkspace";
        public const string ListWorkspace = "ListWorkspace";
    }

    public class BookingReportCommonConstants
    {
        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Command_ExportToExcel = "Command_ExportToExcel";

    }

    public enum BPGroupBy
    {
        VoyageName=1,
        /// <summary>
        /// 公司
        /// </summary>
        Company = 2,
        /// <summary>
        /// 航线
        /// </summary>
        ShipLine = 3,
        /// <summary>
        /// 船东
        /// </summary>
        Carrier =4


    }

}
