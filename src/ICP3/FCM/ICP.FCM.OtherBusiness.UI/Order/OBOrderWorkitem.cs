#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/13 星期二 14:13:23
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
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Collections.Generic;

namespace ICP.FCM.OtherBusiness.UI.Order
{
    /// <summary>
    /// 订单
    /// </summary>
    public class OBOrderWorkitem : WorkItem
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public DocumentListPresenter _DocumentPresenter { get; set; }


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
                _DocumentPresenter = null;
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

        private void Show()
        {
            OBOrderMainWorkSpace mainSpce = SmartParts.Get<OBOrderMainWorkSpace>("OBMainWorkSpace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<OBOrderMainWorkSpace>("OBMainWorkSpace");

                #region AddPart
                OBOrderMainToolBar toolBar = SmartParts.AddNew<OBOrderMainToolBar>();
                IWorkspace ToolbarWorkspace = Workspaces[OBWorkSpaceConstants.ToolbarWorkspace];
                ToolbarWorkspace.Show(toolBar);

                OBOrderListPart listPart = SmartParts.AddNew<OBOrderListPart>();
                IWorkspace ListWorkspace = Workspaces[OBWorkSpaceConstants.ListWorkspace];
                ListWorkspace.Show(listPart);



                OBOrderSearchPart searchPart = SmartParts.AddNew<OBOrderSearchPart>();
                IWorkspace SearchWorkspace = Workspaces[OBWorkSpaceConstants.SearchWorkspace];
                //searchPart.Workitem.SetEnable("barEdit", false)
                SearchWorkspace.Show(searchPart);


                OBOrderFastSearchPart fastSearchPart = SmartParts.AddNew<OBOrderFastSearchPart>();
                IWorkspace FastSearchWorkspace = Workspaces[OBWorkSpaceConstants.FastSearchWorkspace];
                FastSearchWorkspace.Show(fastSearchPart);



                EventListPart memoListPart = Items.AddNew<EventListPart>();
                IWorkspace EventListWorkspace = Workspaces[OBWorkSpaceConstants.EventListWorkspace];
                EventListWorkspace.Show(memoListPart);

                UCCommunicationHistory faxMailEDIListPart = Items.AddNew<UCCommunicationHistory>();
                IWorkspace faxMailListWorkspace = Workspaces[OBWorkSpaceConstants.FaxMailEDIListWorkspace];
                faxMailListWorkspace.Show(faxMailEDIListPart);


                UCDocumentList documentPart = SmartParts.AddNew<UCDocumentList>();
                DocumentListPresenter documentPresenter = Items.AddNew<DocumentListPresenter>();
                documentPresenter.ucList = documentPart;
                documentPart.Presenter = documentPresenter;
                IWorkspace documentListWorkSpace = Workspaces[OBWorkSpaceConstants.DocumentListWorkspace];
                documentListWorkSpace.Show(documentPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo
                {
                    Title = LocalData.IsEnglish ? "Other Business Order" : "其他业务订单"
                };
                mainWorkspace.Show(mainSpce, smartPartInfo);


                OBOrderUIAdapter orderAdapter = new OBOrderUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {toolBar.GetType().Name, toolBar},
                    {listPart.GetType().Name, listPart},
                    {searchPart.GetType().Name, searchPart},
                    {fastSearchPart.GetType().Name, fastSearchPart},
                    {memoListPart.GetType().Name, memoListPart},
                    {faxMailEDIListPart.GetType().Name, faxMailEDIListPart},
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
}
