using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.Role.Finder
{
    public class RoleMiniFinderWorkitem:WorkItem
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                this._listPart = null;
            }
            base.Dispose(disposing);
        }

        public event EventHandler<DataFindEventArgs> DataChoosed;
        RoleMiniFinderListPart _listPart = null;

        public void Show(IWorkspace mainWorkspace, List<RoleList> list, string[] returnFields)
        {

            _listPart = this.SmartParts.Get<RoleMiniFinderListPart>("RoleMiniFinderListPart");
            if (_listPart == null)
            {
                _listPart = this.SmartParts.AddNew<RoleMiniFinderListPart>("RoleMiniFinderListPart");

                if (mainWorkspace == null)
                    mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

                BulidConnection(_listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Select Role" : "角色选择";
                mainWorkspace.Show(_listPart, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(_listPart);
            }


            _listPart.DataSource = list;
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
                RoleList list = data as RoleList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<RoleList>(list, returnFields)));
                }
            };
        }
    }
}
