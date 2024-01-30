using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.Sys.ServiceInterface.DataObjects;
using System.ComponentModel;
using ICP.Framework;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.UI.SystemHelp.Comm;


namespace ICP.Sys.UI.SystemHelp.NewFeedback
{

    public class NewFeedbackWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }
        private void Show()
        {
            FeedbackMainWorkspce newFeedbackMainSpce = this.SmartParts.Get<FeedbackMainWorkspce>("NewFeedbackMainWorkspce");
            if (newFeedbackMainSpce == null)
            {
                newFeedbackMainSpce = this.SmartParts.AddNew<FeedbackMainWorkspce>("NewFeedbackMainWorkspce");

                NewFeedbackPart newFeedbackPart = this.SmartParts.AddNew<NewFeedbackPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[HelpWorkSpaceConstants.FeedbackMainWorkspace];
                if (listWorkspace == null) listWorkspace = this.Workspaces.AddNew<DeckWorkspace>(HelpWorkSpaceConstants.FeedbackMainWorkspace);
                listWorkspace.Show(newFeedbackPart);

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "New Feedback" : "新增反馈";
                mainWorkspace.Show(newFeedbackMainSpce, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(newFeedbackMainSpce);
            }
        }
    }



}
