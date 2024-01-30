using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FRM.UI
{
    public class BusinessWeeklyReportWoritem : WorkItem
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
            WeeklyReportMainWorkspace mainSpce = SmartParts.Get<WeeklyReportMainWorkspace>("WeeklyReportMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<WeeklyReportMainWorkspace>("WeeklyReportMainWorkspace");

                #region AddPart



                WeeklyReportPart listPart = SmartParts.AddNew<WeeklyReportPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[BWRWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);



                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Business Weekly Report" : "商务周报表";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                //BusinessWeeklyReportUIAdapter bookingAdapter = new BusinessWeeklyReportUIAdapter();
                //bookingAdapter.Workitem = Workitem;

                //Dictionary<string, object> dic = new Dictionary<string, object>();
                //dic.Add(toolBar.GetType().Name, toolBar);
                //dic.Add(listPart.GetType().Name, listPart);
                //dic.Add(searchPart.GetType().Name, searchPart);
                //dic.Add(operationToolBar.GetType().Name, operationToolBar);
                //dic.Add(operationListPart.GetType().Name, operationListPart);

                //bookingAdapter.Init(dic);
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
    public class BWRWorkSpaceConstants
    {
        public const string ListWorkspace = "ListWorkspace";
    }

    public class BWRCommonConstants
    {
        public const string Command_AddData = "Command_AddData";
        public const string Command_DeleteData = "Command_DeleteData";
        public const string Command_SaveData = "Command_SaveData";

        /// <summary>
        /// 按航线分组
        /// </summary>
        public const string Command_GroupByShipperingLine = "Command_GroupByShipperingLine";
        /// <summary>
        /// 按公司分组
        /// </summary>
        public const string Command_GroupByCompany = "Command_GroupByCompany";
    }
    public class BWPermissionCommandConstants
    {
        /// <summary>
        /// 商务员
        /// </summary>
        public const string WEEKLYREPORT_BUSINESS = "WEEKLYREPORT_BUSINESS";
        /// <summary>
        /// 总经理
        /// </summary>
        public const string WEEKLYREPORT_GENERALMANAGER = "WEEKLYREPORT_GENERALMANAGER";
    }


    //public class BusinessWeeklyReportUIAdapter : IPartBridge
    //{
    //    #region Service

    //    [ServiceDependency]
    //    public WorkItem Workitem { get; set; }

    //    [ServiceDependency]
    //    public IOceanPriceService opService { get; set; }

    //    #endregion

    //    #region parts

    //    #endregion

    //    #region interface


    //    #endregion
    //}
}
