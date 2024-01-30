using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.UI.SystemHelp.Comm;

namespace ICP.Sys.UI.SystemHelp.HelpDocument
{
    public class HelpDocumentWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        private void Show()
        {
            FeedbackMainWorkspce helpDocumentMainSpce = this.SmartParts.Get<FeedbackMainWorkspce>("HelpDocumentMainWorkspce");
            if (helpDocumentMainSpce == null)
            {
                helpDocumentMainSpce = this.SmartParts.AddNew<FeedbackMainWorkspce>("HelpDocumentMainWorkspce");

                HelpDocumentPart myFeedbackPart = this.SmartParts.AddNew<HelpDocumentPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[HelpWorkSpaceConstants.FeedbackMainWorkspace];
                if (listWorkspace == null) listWorkspace = this.Workspaces.AddNew<DeckWorkspace>(HelpWorkSpaceConstants.FeedbackMainWorkspace);
                listWorkspace.Show(myFeedbackPart);

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Help Document" : "帮助文档";
                mainWorkspace.Show(helpDocumentMainSpce, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(helpDocumentMainSpce);
            }
        }

    }
}
