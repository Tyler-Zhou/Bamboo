using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.UI.SystemHelp.Comm;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;

namespace ICP.Sys.UI.SystemHelp.SystemErrorMemo
{
    public class SystemErrorMemoWorkItem : WorkItem
    {
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            FeedbackMainWorkspce newFeedbackMainSpce = this.SmartParts.Get<FeedbackMainWorkspce>("SystemErrorMemoMainWorkSpace");
            if (newFeedbackMainSpce == null)
            {
                newFeedbackMainSpce = this.SmartParts.AddNew<FeedbackMainWorkspce>("SystemErrorMemoMainWorkSpace");                
                SystemErrorMemoPart systemErrorMemo = this.SmartParts.AddNew<SystemErrorMemoPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[HelpWorkSpaceConstants.FeedbackMainWorkspace];
                if (listWorkspace == null) listWorkspace = this.Workspaces.AddNew<DeckWorkspace>(HelpWorkSpaceConstants.FeedbackMainWorkspace);
                listWorkspace.Show(systemErrorMemo);

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo(LocalData.IsEnglish ? "System Error Log" : "系统错误日志", "System Error Log");                
                mainWorkspace.Show(newFeedbackMainSpce, smartPartInfo);
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(newFeedbackMainSpce);
            }
        }
    }
}
