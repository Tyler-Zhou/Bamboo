using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Service;

namespace ICP.Common.UI.CC
{
    public class CCWorkitem : WorkItem
    {

        #region Show

        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            CCMainWorkspace crmMainWorkspace = this.SmartParts.Get<CCMainWorkspace>("CCMainWorkspace");
            if (crmMainWorkspace == null)
            {
                crmMainWorkspace = this.SmartParts.AddNew<CCMainWorkspace>("CCMainWorkspace");

                #region AddPart

                CCToolBar crmToolBar = this.SmartParts.AddNew<CCToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[CCWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(crmToolBar);

                CCListPart crmMainListPart = this.SmartParts.AddNew<CCListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[CCWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(crmMainListPart);


                CCSearchPart crmSearchPart = this.SmartParts.AddNew<CCSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[CCWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(crmSearchPart);

                ///

                CCArchivesPart crmCustomerInfoPart = this.SmartParts.AddNew<CCArchivesPart>();
                IWorkspace custoemrInfoWorkspace = (IWorkspace)this.Workspaces[CCWorkSpaceConstants.CustoemrArchivesWorkspace];
                custoemrInfoWorkspace.Show(crmCustomerInfoPart);

                CCPartnerPart crmPartnerPart = this.SmartParts.AddNew<CCPartnerPart>();
                IWorkspace partnerWorkspace = (IWorkspace)this.Workspaces[CCWorkSpaceConstants.PartnerWorkspace];
                partnerWorkspace.Show(crmPartnerPart);

                CCBusinessPart crmBusinessPart = this.SmartParts.AddNew<CCBusinessPart>();
                IWorkspace businessWorkspace = (IWorkspace)this.Workspaces[CCWorkSpaceConstants.BusinessWorkspace];
                businessWorkspace.Show(crmBusinessPart);

                CCReportPart crmReportPart = this.SmartParts.AddNew<CCReportPart>();
                IWorkspace reportWorkspace = (IWorkspace)this.Workspaces[CCWorkSpaceConstants.ReportWorkspace];
                reportWorkspace.Show(crmReportPart);
           

                #endregion


                CCUIAdapter uiAdapter = this.Items.AddNew<CCUIAdapter>();
                uiAdapter.InitPart(crmMainWorkspace,
                                    crmToolBar,
                                    crmSearchPart,
                                    crmMainListPart,
                                    crmCustomerInfoPart,
                                    crmPartnerPart,
                                    crmBusinessPart,
                                    crmReportPart);

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = NativeLanguageService.GetText(crmMainWorkspace, "Titel");
                mainWorkspace.Show(crmMainWorkspace, smartPartInfo);

                crmSearchPart.RaiseSearched();
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(crmMainWorkspace);
            }
        }

        #endregion
    }
}
