using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Collections.Generic;

namespace ICP.Business.Common.UI.CSPBooking
{
    /// <summary>
    /// CSPBooking WorkItem
    /// </summary>
    public class CSPBKWorkItem : WorkItem
    {
        /// <summary>
        /// CSPBooking WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItemWorkSpace { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string TitleName { get; set; }

        protected override void OnRunStarted()
        {
            base.OnRunStarted();

            Show();
        }
        /// <summary>
        /// 面板加载
        /// </summary>
        public void Show()
        {
            PartMainWorkspace mainSpce = SmartParts.Get<PartMainWorkspace>(CSPBKConstants.LAYOUT_MAIN_WORKSPACE);
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<PartMainWorkspace>(CSPBKConstants.LAYOUT_MAIN_WORKSPACE);

                #region AddPart
                PartToolBar toolBar = SmartParts.AddNew<PartToolBar>();
                IWorkspace ToolbarWorkspace = Workspaces[CSPBKConstants.LAYOUT_TOOLBAR_WORKSPACE];
                ToolbarWorkspace.Show(toolBar);

                PartList listPart = SmartParts.AddNew<PartList>();
                IWorkspace ListWorkspace = Workspaces[CSPBKConstants.LAYOUT_LIST_WORKSPACE];
                ListWorkspace.Show(listPart);

                PartSearch searchPart = SmartParts.AddNew<PartSearch>();
                IWorkspace SearchWorkspace = Workspaces[CSPBKConstants.LAYOUT_SEARCH_WORKSPACE];
                SearchWorkspace.Show(searchPart);


                PartFastSearch fastSearchPart = SmartParts.AddNew<PartFastSearch>();
                IWorkspace FastSearchWorkspace = Workspaces[CSPBKConstants.LAYOUT_FASTSEARCH_WORKSPACE];
                FastSearchWorkspace.Show(fastSearchPart);

                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = TitleName;
                mainWorkspace.Show(mainSpce, smartPartInfo);

                CSPBKUIAdapter orderAdapter = new CSPBKUIAdapter();
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    {toolBar.GetType().Name, toolBar},
                    {listPart.GetType().Name, listPart},
                    {searchPart.GetType().Name, searchPart},
                    {fastSearchPart.GetType().Name, fastSearchPart},
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
                WorkItemWorkSpace = null;
            }
            base.Dispose(disposing);
        }
    }
}
