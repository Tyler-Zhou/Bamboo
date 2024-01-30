using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Service;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// OceanPrice WorkItem
    /// </summary>
    public class OceanPriceWorkitem : WorkItem
    {

        #region Show
        /// <summary>
        /// On Run Started
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            OPMainWorkspace opMainWorkspace = SmartParts.Get<OPMainWorkspace>("OPMainWorkspace");
            if (opMainWorkspace == null)
            {
                opMainWorkspace = SmartParts.AddNew<OPMainWorkspace>("OPMainWorkspace");

                #region AddPart

                OPToolBar opToolBar = SmartParts.AddNew<OPToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(opToolBar);

                OPContractListPart opMainListPart = SmartParts.AddNew<OPContractListPart>();
                IWorkspace listWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(opMainListPart);

                OPContractEditPart opEditPart = SmartParts.AddNew<OPContractEditPart>();
                IWorkspace editWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.ContractWorkspace];
                editWorkspace.Show(opEditPart);

                OPSearchPart opSearchPart = SmartParts.AddNew<OPSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(opSearchPart);

                OPBasePortRatesPart opBasePortRatesPart = SmartParts.AddNew<OPBasePortRatesPart>();
                IWorkspace bprWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.BPRWorkspace];
                bprWorkspace.Show(opBasePortRatesPart);

                OPArbitraryRatesPart opArbitraryRatesPart = SmartParts.AddNew<OPArbitraryRatesPart>();
                IWorkspace arWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.ARWorkspace];
                arWorkspace.Show(opArbitraryRatesPart);

                OPAdditionalFeePart opAdditionalFeePart = SmartParts.AddNew<OPAdditionalFeePart>();
                IWorkspace afWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.AFWorkspace];
                afWorkspace.Show(opAdditionalFeePart);

                OPPermissionsPart opPermissionsPart = SmartParts.AddNew<OPPermissionsPart>();
                IWorkspace permissionsWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.PermissionsWorkspace];
                permissionsWorkspace.Show(opPermissionsPart);

                OPAttachmentPart opAttachmentPart = SmartParts.AddNew<OPAttachmentPart>();
                IWorkspace attachmentWorkspace = (IWorkspace)Workspaces[OPWorkSpaceConstants.AttachmentWorkspace];
                attachmentWorkspace.Show(opAttachmentPart);

                #endregion

                OceanPriceUIAdapter uiAdapter = Items.AddNew<OceanPriceUIAdapter>();
                uiAdapter.InitPart(opMainWorkspace,
                                    opToolBar,
                                    opSearchPart,
                                    opMainListPart,
                                    opEditPart,
                                    opBasePortRatesPart,
                                    opArbitraryRatesPart,
                                    opAdditionalFeePart,
                                    opPermissionsPart,
                                    opAttachmentPart);

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = NativeLanguageService.GetText(opMainWorkspace, "Titel");
                mainWorkspace.Show(opMainWorkspace, smartPartInfo);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(opMainWorkspace);
            }
        }

        #endregion
    }

}
