using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI
{
    class AdjustRateWorkItem: WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            AdjustRateMainWorkSpace mainSpce = SmartParts.Get<AdjustRateMainWorkSpace>("AdjustRateMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<AdjustRateMainWorkSpace>("AdjustRateMainWorkSpace");

                #region AddPart

                AdjustRateListPart listPart = SmartParts.AddNew<AdjustRateListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[AdjustRateWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Set Rate" : "设置汇率";
                mainWorkspace.Show(mainSpce, smartPartInfo);

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
    public class AdjustRateWorkSpaceConstants
    {
        public const string ListWorkspace = "ListWorkspace";
    }

}
