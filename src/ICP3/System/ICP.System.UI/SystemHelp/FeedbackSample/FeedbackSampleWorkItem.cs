using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.UI.SystemHelp.Comm;

namespace ICP.Sys.UI.SystemHelp.FeedbackSample
{
    public class FeedbackSampleWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        private void Show()
        {
            FeedbackMainWorkspce feedbackSampleMainSpce = this.SmartParts.Get<FeedbackMainWorkspce>("FeedbackSampleWorkspce");
            if (feedbackSampleMainSpce == null)
            {
                feedbackSampleMainSpce = this.SmartParts.AddNew<FeedbackMainWorkspce>("FeedbackSampleWorkspce");

                FeedbackSamplePart myFeedbackPart = this.SmartParts.AddNew<FeedbackSamplePart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[HelpWorkSpaceConstants.FeedbackMainWorkspace];
                if (listWorkspace == null) listWorkspace = this.Workspaces.AddNew<DeckWorkspace>(HelpWorkSpaceConstants.FeedbackMainWorkspace);
                listWorkspace.Show(myFeedbackPart);

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Feedback Sample" : "反馈示例";
                mainWorkspace.Show(feedbackSampleMainSpce, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(feedbackSampleMainSpce);
            }
        }

    }
}
