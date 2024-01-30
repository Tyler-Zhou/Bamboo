using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI
{
    /// <summary>
    /// 期初余额
    /// </summary>
    class BeginBalanceWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            ImportUFBeginBalanceMainWorkSpace mainSpce = SmartParts.Get<ImportUFBeginBalanceMainWorkSpace>("ImportUFBeginBalanceMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<ImportUFBeginBalanceMainWorkSpace>("ImportUFBeginBalanceMainWorkSpace");

                #region AddPart

                ImportUFBeginBalanceListPart listPart = SmartParts.AddNew<ImportUFBeginBalanceListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[ImportUFBeginBalanceWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "BeginBalance" : "期初余额";
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
    public class ImportUFBeginBalanceWorkSpaceConstants
    {
        public const string ListWorkspace = "ListWorkspace";
    }

}
