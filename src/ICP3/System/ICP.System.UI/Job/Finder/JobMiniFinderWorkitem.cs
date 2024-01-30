using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.Sys.UI.Job.Finder
{
    public class JobMiniFinderWorkitem : WorkItem
    {

        #region Service
        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        #endregion
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
        JobMiniFinderListPart _listPart = null;

        public void Show(IWorkspace mainWorkspace, string[] returnFields)
        {
            if (mainWorkspace == null)
                mainWorkspace = this.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            _listPart = this.SmartParts.Get<JobMiniFinderListPart>("JobMiniFinderListPart");
            if (_listPart == null)
            {
                _listPart = this.SmartParts.AddNew<JobMiniFinderListPart>("JobMiniFinderListPart");

                List<JobList> list = JobService.GetJobList(string.Empty, string.Empty, true, 0);
                JobList tager = list.Find(delegate(JobList item) { return Utility.GuidIsNullOrEmpty(item.ParentID); });
                if (tager != null) list.Remove(tager);

                _listPart.DataSource = list;

                BulidConnection(_listPart, returnFields);
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Select Job" : "职位选择";
                mainWorkspace.Show(_listPart, smartPartInfo);
            }
            else
            {
                mainWorkspace.Activate(_listPart);
            }
        }

        public void ResetCondition(IDictionary<string, object> values)
        {
            if (_listPart != null) _listPart.Clear();
        }

        private void BulidConnection( BaseListPart listPart
                                     , string[] returnFields)
        {

            listPart.Selected += delegate(object sender, object data)
            {
                JobList list = data as JobList;
                if (list == null) return;
                if (DataChoosed != null)
                {
                    DataChoosed(sender, new DataFindEventArgs(Utility.GetSingleSearchResult<JobList>(list, returnFields)));
                }
            };
        }

    }

}
