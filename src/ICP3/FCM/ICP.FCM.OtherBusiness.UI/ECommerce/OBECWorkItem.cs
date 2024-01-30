#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/8 星期四 17:37:56
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI.EventList;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections.Generic;

namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    /// <summary>
    /// 其他业务-电商物流
    /// </summary>
    public class OBECWorkItem : WorkItem
    {
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

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
            OBECMainWorkSpace mainSpce = SmartParts.Get<OBECMainWorkSpace>("OBECMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<OBECMainWorkSpace>("OBECMainWorkSpace");

                #region AddPart
                OBECMainToolBar toolBar = SmartParts.AddNew<OBECMainToolBar>();
                toolBar.addType = AddType.OtherBusiness;
                IWorkspace ToolbarWorkspace = Workspaces[OBWorkSpaceConstants.ToolbarWorkspace];
                ToolbarWorkspace.Show(toolBar);

                OBECListPart listPart = SmartParts.AddNew<OBECListPart>();
                IWorkspace ListWorkspace = Workspaces[OBWorkSpaceConstants.ListWorkspace];
                ListWorkspace.Show(listPart);



                OBECSearchPart searchPart = SmartParts.AddNew<OBECSearchPart>();
                IWorkspace SearchWorkspace = Workspaces[OBWorkSpaceConstants.SearchWorkspace];
                SearchWorkspace.Show(searchPart);


                OBECFastSearchPart fastSearchPart = SmartParts.AddNew<OBECFastSearchPart>();
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
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? " E-Commerce Business" : "电商物流";
                mainWorkspace.Show(mainSpce, smartPartInfo);

                OBECUIAdapter orderAdapter = new OBECUIAdapter();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                WorkItem = null;
            }
            base.Dispose(disposing);
        }
    }
}
