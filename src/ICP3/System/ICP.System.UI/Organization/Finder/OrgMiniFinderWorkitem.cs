using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;

namespace ICP.Sys.UI.Organization.Finder
{
    public class OrgMiniFinderWorkitem:WorkItem
    {
        #region Service


        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._listPart = null;
                this.DataChoosed = null;
            }
            base.Dispose(disposing);
        }
        OrgMiniFinderListPart _listPart = null;

        public event EventHandler<DataFindEventArgs> DataChoosed;

        public void Show(IWorkspace mainWorkspace, string[] returnFields)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            _listPart = this.Items.Get<OrgMiniFinderListPart>("OrgMiniFinderListPart");

            if (_listPart == null)
            {
                _listPart = this.SmartParts.AddNew<OrgMiniFinderListPart>("OrgMiniFinderListPart");
                BulidConnection(_listPart, returnFields);
                List<OrganizationList> list = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);
                OrganizationList tager = list.Find(delegate(OrganizationList item) { return Utility.GuidIsNullOrEmpty(item.ParentID); });
                if (tager != null) list.Remove(tager);

                _listPart.DataSource = list;

                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Select Organization" : "组织结构选择";
                mainWorkspace.Show(_listPart, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(_listPart);
            }
            
        }

        public void ResetCondition(IDictionary<string, object> values)
        {
            _listPart.Clear();
        }

        private void BulidConnection(BaseListPart listPart
                                     , string[] returnFields)
        {

            listPart.Selected += delegate(object sender, object data)
            {
                OrganizationList list = data as OrganizationList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<OrganizationList>(list, returnFields)));
                }
            };
        }
    }
}
