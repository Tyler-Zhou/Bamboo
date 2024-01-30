using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.UI.SystemHelp.Comm;

namespace ICP.Sys.UI.SystemHelp.MyFeedback
{
    public class MyFeedbackWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        private void Show()
        {
            FeedbackMainWorkspce myFeedbackMainSpce = this.SmartParts.Get<FeedbackMainWorkspce>("MyFeedbackMainWorkspce");
            if (myFeedbackMainSpce == null)
            {
                myFeedbackMainSpce = this.SmartParts.AddNew<FeedbackMainWorkspce>("MyFeedbackMainWorkspce");

                MyFeedbackPart myFeedbackPart = this.SmartParts.AddNew<MyFeedbackPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[HelpWorkSpaceConstants.FeedbackMainWorkspace];
                if (listWorkspace == null) listWorkspace = this.Workspaces.AddNew<DeckWorkspace>(HelpWorkSpaceConstants.FeedbackMainWorkspace);
                listWorkspace.Show(myFeedbackPart);

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "My Feedback" : "我的反馈";
                mainWorkspace.Show(myFeedbackMainSpce, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(myFeedbackMainSpce);
            }
        }

    }
}
