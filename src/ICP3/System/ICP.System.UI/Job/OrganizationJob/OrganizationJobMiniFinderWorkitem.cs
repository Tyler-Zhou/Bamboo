using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.Job.Finder
{
    public class OrganizationJobMiniFinderWorkitem : WorkItem
    {

        public event EventHandler<DataFindEventArgs> DataChoosed;
        ICP.Framework.ClientComponents.UIFramework.BaseListPart _listPart = null;

        List<Organization2JobList> originallist = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                if (this._listPart != null)
                {
                    this._listPart.DataSource = null;
                    this._listPart = null;
                }
                

            }
            base.Dispose(disposing);
        }
        public void Show(IWorkspace mainWorkspace, List<Organization2JobList> list, string[] returnFields)
        {
            originallist = list;

            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            _listPart = this.SmartParts.Get<OrganizationJobMiniFinderListPart>("OrganizationJobMiniFinderListPart");
            if (_listPart == null)
            {
                _listPart = this.SmartParts.AddNew<OrganizationJobMiniFinderListPart>("OrganizationJobMiniFinderListPart");

                BulidConnection(_listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Select Job" : "职位选择";
                mainWorkspace.Show(_listPart, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(_listPart);
            }
            _listPart.DataSource = originallist;
        }

        public void ResetCondition(IDictionary<string, object> values)
        {
            if (_listPart != null)
            {
                _listPart.Clear();

                if (values == null || values.Count == 0) return;
                if (values.ContainsKey("Source"))
                    _listPart.DataSource = values["Source"];
                else
                    _listPart.DataSource = originallist;
            }
        }

        private void BulidConnection( BaseListPart listPart
                                     , string[] returnFields)
        {

            listPart.Selected += delegate(object sender, object data)
            {
                Organization2JobList list = data as Organization2JobList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<Organization2JobList>(list, returnFields)));
                }
            };
        }
    
       

    }

    public class OrganizationAndJobMiniFinderWorkitem : WorkItem
    {

        public event EventHandler<DataFindEventArgs> DataChoosed;
        ICP.Framework.ClientComponents.UIFramework.BaseListPart _listPart = null;

        List<Organization2JobList> originallist = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DataChoosed = null;
                this.originallist = null;
                if (this._listPart != null)
                {
                    this._listPart.DataSource = null;
                    this._listPart = null;
                }
            }
            base.Dispose(disposing);
        }
        public void Show(IWorkspace mainWorkspace, List<Organization2JobList> list, string[] returnFields)
        {
            originallist = list;

            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            _listPart = this.SmartParts.Get<OrganizationAndJobMiniFinderListPart>("OrganizationAndJobMiniFinderListPart");
            if (_listPart == null)
            {
                _listPart = this.SmartParts.AddNew<OrganizationAndJobMiniFinderListPart>("OrganizationAndJobMiniFinderListPart");

                BulidConnection(_listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Select Job" : "职位选择";
                mainWorkspace.Show(_listPart, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(_listPart);
            }
            _listPart.DataSource = originallist;
        }

        public void ResetCondition(IDictionary<string, object> values)
        {
            if (_listPart != null)
            {
                _listPart.Clear();

                if (values == null || values.Count == 0) return;
                if (values.ContainsKey("Source"))
                    _listPart.DataSource = values["Source"];
                else
                    _listPart.DataSource = originallist;
            }
        }

        private void BulidConnection(BaseListPart listPart
                                     , string[] returnFields)
        {

            listPart.Selected += delegate(object sender, object data)
            {
                Organization2JobList list = data as Organization2JobList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<Organization2JobList>(list, returnFields)));
                }
            };
        }
    }
}
