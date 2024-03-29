using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;

namespace ICP.Sys.UI.Role
{
    [ToolboxItem(false)]
    public partial class Role2OrgJobListPart : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IRoleService RoleService
        {
            get
            {
                return ServiceClient.GetService<IRoleService>();
            }
        }

        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        #endregion

        #region init

        public Role2OrgJobListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._parentList = null;
                this._Source = null;
                this.barSave.ItemClick -= this.barSave_ItemClick;
                this.Saved = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish == false) SetCnText();
        }

        private void SetCnText()
        {
            barSave.Caption = "保存(&S)";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<Organization2JobList> orgJobDatas = JobService.GetOrganization2JobList(null, true);
            List<Guid> selectedIDs = new List<Guid>();
            if (_Source != null && _Source.Count > 0)
            {
                foreach (var item in _Source)
                {
                    selectedIDs.Add(item.OrganizationJobID);
                }
            }
            dic.Add("OriginalList", orgJobDatas);
            jobOrgSelectPart1.Init(dic);
            jobOrgSelectPart1.DataSource = selectedIDs;
        }

        #endregion

        #region BarItem

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        BaseList<Role2OrganizationJobList> GetSelectedSource()
        {
            if (jobOrgSelectPart1.Created == false) return new BaseList<Role2OrganizationJobList>(_Source);
            List<Guid> selectedIDs = jobOrgSelectPart1.DataSource as List<Guid>;
            List<Role2OrganizationJobList> selected = new List<Role2OrganizationJobList>();
            foreach (var item in _Source)
            {
                if (selectedIDs.Contains(item.OrganizationJobID))
                    selected.Add(item);
            }

            List<Guid> existIDs = new List<Guid>();
            foreach (var item in selected)
            {
                existIDs.Add(item.OrganizationJobID);
            }

            foreach (var item in selectedIDs)
            {
                if (existIDs.Contains(item)) continue;

                Role2OrganizationJobList newData = new Role2OrganizationJobList();
                newData.CreateById = LocalData.UserInfo.LoginID;
                newData.CreateByName = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                newData.RoleID = _parentList.ID;
                newData.OrganizationJobID = item;
                selected.Add(newData);
            }
           
            bool isDirty = false;
            foreach (var item in selected)
            {
                if (item.IsNew) { isDirty = true; break; }
                if (item.IsDirty) { isDirty = true; break; }
            }
            ICP.Framework.CommonLibrary.Common.BaseList<Role2OrganizationJobList> list = new BaseList<Role2OrganizationJobList>(selected);
            list.IsDirty = isDirty;
            if (list.Count != _Source.Count) list.IsDirty = true;

            return list;
        }

        private bool Save()
        {
            if (_parentList == null) return false;

            List<Guid> selectedIDs = jobOrgSelectPart1.DataSource as List<Guid>;
            BaseList<Role2OrganizationJobList> temp = GetSelectedSource();

            List<Role2OrganizationJobList> needSaveData = new List<Role2OrganizationJobList>();
            foreach (var item in temp)
            {
                needSaveData.Add(item);
            }


            if (needSaveData == null) return false;
            
            foreach (var item in needSaveData)
	        {
        		 if(item.Validate()==false) return false;
	        }

            try
            {
                List<Guid?> ids = new List<Guid?>();
                List<Guid> orgJobIds = new List<Guid>();
                foreach (var item in needSaveData)
                {
            		ids.Add(item.ID);
                    orgJobIds.Add(item.OrganizationJobID);
                }

                ManyResultData result = RoleService.SetRole2OrganizationJobInfo(_parentList.ID
                                                                                , ids.ToArray()
                                                                                , orgJobIds.ToArray()
                                                                                ,LocalData.UserInfo.LoginID);

               for (int i = 0; i < needSaveData.Count; i++)
               {
                   needSaveData[i].ID = result.ChildResults[i].ID;
                   //needSaveData[i].UpdateDate = result.ChildResults[i].UpdateDate;
                   needSaveData[i].BeginEdit();
               }
               _Source = needSaveData;
               if (Saved != null) Saved(needSaveData);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        #endregion

        #region IListPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        public override object DataSource
        {
            get
            {
                return GetSelectedSource();
            }
            set
            {
                BindingData(value);
            }
        }

        List<Role2OrganizationJobList> _Source = null;

        void BindingData(object data)
        {
            if (data == null) { jobOrgSelectPart1.DataSource = new List<Guid>(); }
            else
            {
                _Source = data as List<Role2OrganizationJobList>;
                if (_Source != null && _Source.Count > 0)
                {
                    foreach (var item in _Source) { item.BeginEdit(); }
                }
                List<Guid> selectedIDs = new List<Guid>();
                foreach (var item in _Source)
                {
                    selectedIDs.Add(item.OrganizationJobID);
                }
                jobOrgSelectPart1.DataSource = selectedIDs;
            }
        }

        #endregion

        #region IEditPart成员

        public override bool SaveData()
        {
            return Save();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region IPart 成员

        RoleList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as RoleList;
                    if (_parentList == null || _parentList.IsNew|| _parentList.IsValid == false)
                        this.Enabled = false;
                    else
                        this.Enabled = true;
                }
            }
        }

        #endregion
    }
}
