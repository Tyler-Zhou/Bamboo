using System.Collections.Generic;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.OtherBusiness.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 其他业务
    /// </summary>
    public class OBBWorkitem : WorkItem   //业务管理
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Workitem = null;
            }
            base.Dispose(disposing);
        }
        /// <summary>
        /// 
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Show()
        {
            OBBMainWorkSpace mainSpce = SmartParts.Get<OBBMainWorkSpace>(OBBWorkSpaceConstants.MainWorkSpace);
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<OBBMainWorkSpace>(OBBWorkSpaceConstants.MainWorkSpace);

                #region AddPart
                OBBMainToolBar toolBar = SmartParts.AddNew<OBBMainToolBar>();
                toolBar.addType = AddType.OtherBusiness;
                IWorkspace ToolbarWorkspace = Workspaces[OBWorkSpaceConstants.ToolbarWorkspace];
                ToolbarWorkspace.Show(toolBar);

                OBBListPart listPart = SmartParts.AddNew<OBBListPart>();
                listPart.AddType = AddType.OtherBusiness;
                IWorkspace ListWorkspace = Workspaces[OBWorkSpaceConstants.ListWorkspace];
                ListWorkspace.Show(listPart);



                OBBSearchPart searchPart = SmartParts.AddNew<OBBSearchPart>();
                IWorkspace SearchWorkspace = Workspaces[OBWorkSpaceConstants.SearchWorkspace];
                SearchWorkspace.Show(searchPart);


                OBBFastSearchPart fastSearchPart = SmartParts.AddNew<OBBFastSearchPart>();
                IWorkspace FastSearchWorkspace = Workspaces[OBWorkSpaceConstants.FastSearchWorkspace];
                FastSearchWorkspace.Show(fastSearchPart);

                EventListPart memoListPart = Items.AddNew<EventListPart>();
                IWorkspace EventListWorkspace = Workspaces[OBWorkSpaceConstants.EventListWorkspace];
                EventListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailListPart = Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = Workspaces[OBWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailListPart);


                UCDocumentList documentPart = SmartParts.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentPart;
                documentPart.Presenter = documentPresenter;
                IWorkspace documentPartWorkSapce = Workspaces[OBWorkSpaceConstants.DocumentListWorkspace];
                documentPartWorkSapce.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo
                {
                    Title = LocalData.IsEnglish ? "Other Business" : "其他业务"
                };
                mainWorkspace.Show(mainSpce, smartPartInfo);

                OBBUIAdapter orderAdapter = new OBBUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {toolBar.GetType().Name, toolBar},
                    {listPart.GetType().Name, listPart},
                    {searchPart.GetType().Name, searchPart},
                    {fastSearchPart.GetType().Name, fastSearchPart},
                    {memoListPart.GetType().Name, memoListPart},
                    {faxMailListPart.GetType().Name, faxMailListPart},
                    {documentPart.GetType().Name, documentPart}
                };
                orderAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    

    /// <summary>
    /// 
    /// </summary>
    public class OrderStateConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public const string CurrentRow = "CurrentRow";
    }

    
    
}
