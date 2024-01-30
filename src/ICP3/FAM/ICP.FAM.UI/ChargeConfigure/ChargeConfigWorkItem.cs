using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.ChargeConfigure
{
    class ChargeConfigWorkItem  : WorkItem
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            ChargeConfigMainWorkSpace mainSpce = SmartParts.Get<ChargeConfigMainWorkSpace>("ChargeConfigMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<ChargeConfigMainWorkSpace>("ChargeConfigMainWorkSpace");

                ChargeConfigListPart listPart = SmartParts.AddNew<ChargeConfigListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[ChargeConfigWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "ChargeCode Configure" : "费用模板设置";
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
    public class ChargeConfigWorkSpaceConstants
    {
        public const string ListWorkspace = "ListWorkspace";
    }
}
