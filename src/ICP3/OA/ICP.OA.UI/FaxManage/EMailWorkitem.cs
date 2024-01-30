using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.OA.UI.EmailManage
{
    public class EMailWorkitem:WorkItem
    {
        #region Service

        #endregion

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {

            ICP.OA.UI.FaxManage.UCFaxList emailMainSpce = this.SmartParts.Get<ICP.OA.UI.FaxManage.UCFaxList>("UCFaxList");
            if (emailMainSpce == null)
            {
                emailMainSpce = this.SmartParts.AddNew<ICP.OA.UI.FaxManage.UCFaxList>("UCFaxList");

                IWorkspace mainWorkspace =
                    this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();                
                smartPartInfo.Title = LocalData.IsEnglish ? "Fax Center" : "传真中心";
                mainWorkspace.Show(emailMainSpce, smartPartInfo);

            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(emailMainSpce);
            }
        }
    }
}
