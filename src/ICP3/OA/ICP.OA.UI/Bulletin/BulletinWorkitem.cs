using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Service;
using ICP.OA.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.OA.UI.Bulletin
{
    public class BulletinWorkitem : WorkItem
    {

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #region Show
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Workitem = null;
            }
            base.Dispose(disposing);
        }
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            BulletinMainWorkspace bulletinMainWorkspace = this.SmartParts.Get<BulletinMainWorkspace>("BulletinMainWorkspace");
            if (bulletinMainWorkspace == null)
            {
                bulletinMainWorkspace = this.SmartParts.AddNew<BulletinMainWorkspace>("OPMainWorkspace");

                #region AddPart

                BulletinToolBar toolBar = this.SmartParts.AddNew<BulletinToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)this.Workspaces[BulletinWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BulletinListPart mainListPart = this.SmartParts.AddNew<BulletinListPart>();
                IWorkspace listWorkspace = (IWorkspace)this.Workspaces[BulletinWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(mainListPart);

                BulletinSearchPart searchPart = this.SmartParts.AddNew<BulletinSearchPart>();
                IWorkspace searchWorkspace = (IWorkspace)this.Workspaces[BulletinWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                #endregion


                searchPart.OnSearched += delegate(object sender, object results)
                {
                    mainListPart.DataSource = results;
                };

                IWorkspace mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = NativeLanguageService.GetText(bulletinMainWorkspace, "Titel");
                mainWorkspace.Show(bulletinMainWorkspace, smartPartInfo);

                searchPart.RaiseSearched();
            }
            else
            {
                this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace].Activate(bulletinMainWorkspace);
            }
        }

        #endregion
    }

    public class BulletinCommonConstants
    {
        public const string Command_ShowSearch = "Command_ShowSearch";

        public const string Command_AddData = "Command_AddData";
        public const string Command_EditData = "Command_EditData";
        public const string Command_DeleteData = "Command_DeleteData";
    }

    public class BulletinWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolBarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";
    }

    public class BulletinUIDataHelper : Controller
    {
        #region Services


        public IBulletinService BulletinService
        {
            get
            {
                return ServiceClient.GetService<IBulletinService>();
            }
        }


        #endregion

        #region 属性

        List<BulletinTypeData> _BulletinTypes = null;
        public List<BulletinTypeData> BulletinTypes
        {
            get
            {
                if (_BulletinTypes != null) return _BulletinTypes;
                else
                {
                    _BulletinTypes = BulletinService.GetBulletinTypeDatas();
                    return _BulletinTypes;
                }
            }
        }

        List<OrganizationTreeData> _OrganizationTreeDatas = null;
        public List<OrganizationTreeData> OrganizationTreeDatas
        {
            get
            {
                if (_OrganizationTreeDatas != null) return _OrganizationTreeDatas;
                else
                {
                    _OrganizationTreeDatas = BulletinService.GetOrganizationTreeData();
                    return _OrganizationTreeDatas;
                }
            }
        }

        #endregion

    }
}
