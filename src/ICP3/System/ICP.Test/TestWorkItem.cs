using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Test
{
    class TestWorkItem : WorkItem
    {

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            MainWorkSpace mainSpce = this.SmartParts.Get<MainWorkSpace>("MainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = this.SmartParts.AddNew<MainWorkSpace>("MainWorkSpace");

                TestList listPart = this.SmartParts.AddNew<TestList>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces["ListWorkspace"];
                listWorkspace.Show(listPart);


                IWorkspace mainWorkspace = Workspaces["MainWorkspace"];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title =  "测试";
                mainWorkspace.Show(mainSpce, smartPartInfo);

            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }
}
