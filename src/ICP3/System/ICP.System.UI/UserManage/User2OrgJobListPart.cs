using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.UserManage
{
    [ToolboxItem(false)]
    public partial class User2OrgJobListPart : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
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

        public User2OrgJobListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this._InitSelected = null;
                this._parentList = null;
                this._Source = null;
                this.Saved = null;
                jobOrgSelectPart1.CurrentChanged -= this.OnjobOrgSelectPart1CurrentChanged;
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
            barSetDefault.Caption = "设为默认(&F)";
        }
        private void OnjobOrgSelectPart1CurrentChanged(object sender, object data)
        {
            Organization2JobList organization2JobList = data as Organization2JobList;
            if (organization2JobList == null || organization2JobList.Type == OrganizationJobType.Organization)
                barSetDefault.Enabled = false;
            else
                barSetDefault.Enabled = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            jobOrgSelectPart1.CurrentChanged += this.OnjobOrgSelectPart1CurrentChanged;

            Dictionary<string, object> dic = new Dictionary<string, object>();
            List<Organization2JobList> orgJobDatas = JobService.GetOrganization2JobList(null, true);
            Guid? defaultID = null;
            List<Guid> selectedIDs = new List<Guid>();
            if (_Source != null && _Source.Count > 0)
            {
                foreach (var item in _Source)
                {
                    if (item.IsDefault) defaultID = item.OrganizationJobID;

                    selectedIDs.Add(item.OrganizationJobID);
                }
            }
            dic.Add("OriginalList", orgJobDatas);
            dic.Add("DefaultID", defaultID);
            jobOrgSelectPart1.Init(dic);
            jobOrgSelectPart1.DataSource = selectedIDs;
        }

        #endregion

        #region BarItem

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            if (_parentList == null) return false;

            List<Guid> selectedIDs = jobOrgSelectPart1.DataSource as List<Guid>;
            List<User2OrganizationJobList> needSaveData = new List<User2OrganizationJobList>();
            foreach (var item in _Source)
            {
                if (selectedIDs.Contains(item.OrganizationJobID))
                    needSaveData.Add(item);
            }

            List<Guid> existIDs = new List<Guid>();
            foreach (var item in needSaveData)
            {
                existIDs.Add(item.OrganizationJobID);
            }

            foreach (var item in selectedIDs)
            {
                if (existIDs.Contains(item)) continue;

                User2OrganizationJobList newData = new User2OrganizationJobList();
                newData.CreateById = LocalData.UserInfo.LoginID;
                newData.CreateBy = LocalData.UserInfo.LoginName;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.UserID = _parentList.ID;
                newData.OrganizationJobID = item;
                needSaveData.Add(newData);
            }

            if (needSaveData == null) return false;

            bool hasDefault = false;
            foreach (var item in needSaveData)
            {
                if (item.Validate() == false) return false;

                if (item.IsDefault) hasDefault = true;
            }
            if (hasDefault == false)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Plase set a Default Job." : "请设置一个默认职位.");
                return false;
            }



            try
            {
                List<Guid> ids = new List<Guid>();
                List<Guid> orgJobIds = new List<Guid>();
                List<bool> isDefaults = new List<bool>();

                //List<DateTime?> updateDatas = new List<DateTime?>();
                foreach (var item in needSaveData)
                {
                    ids.Add(item.ID);
                    orgJobIds.Add(item.OrganizationJobID);
                    isDefaults.Add(item.IsDefault);
                    //updateDatas.Add(item.UpdateDate);
                }

                ManyResultData result = UserService.SetUserOrganizationJob(_parentList.ID
                                                                               , ids.ToArray()
                                                                               , orgJobIds.ToArray()
                                                                               , isDefaults.ToArray()
                                                                               , LocalData.UserInfo.LoginID);

                for (int i = 0; i < needSaveData.Count; i++)
                {
                    needSaveData[i].ID = result.ChildResults[i].ID;
                    needSaveData[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    needSaveData[i].BeginEdit();
                }
                _Source = needSaveData;
                _InitSelected = new List<Guid>();
                foreach (var item in _Source)
                {
                    _InitSelected.Add(item.OrganizationJobID);
                    item.IsDirty = false;
                }

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

        private void barSetDefault_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Organization2JobList organization2JobList = jobOrgSelectPart1.Current as Organization2JobList;
                if (organization2JobList == null || organization2JobList.Type == OrganizationJobType.Organization) return;


                List<Guid> selectedIDs = jobOrgSelectPart1.DataSource as List<Guid>;

                _Source = new List<User2OrganizationJobList>();
                foreach (var item in selectedIDs)
                {
                    User2OrganizationJobList newData = new User2OrganizationJobList();
                    newData.CreateById = LocalData.UserInfo.LoginID;
                    newData.CreateBy = LocalData.UserInfo.LoginName;
                    newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    newData.UserID = _parentList.ID;
                    newData.OrganizationJobID = item;
                    newData.IsDefault = false;
                    _Source.Add(newData);
                }

                foreach (var item in _Source)
                {
                    if (item.OrganizationJobID == organization2JobList.ID) item.IsDefault = true;
                    else item.IsDefault = false;
                }

                List<Guid> selectIDs = new List<Guid>();
                foreach (var item in _Source)
                {
                    selectIDs.Add(item.OrganizationJobID);
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DefaultID", organization2JobList.ID);
                jobOrgSelectPart1.Init(dic);
                jobOrgSelectPart1.DataSource = selectIDs;
                #region
                //User2OrganizationJobList needSave = _Source.Find(delegate(User2OrganizationJobList item) { return item.OrganizationJobID == organization2JobList.ID; });
                //needSave.IsDefault = true;

                //User2OrganizationJobList needSave = _Source.Find(delegate(User2OrganizationJobList item) { return item.OrganizationJobID == organization2JobList.ID; });
                //needSave.IsDefault = true;


                //if (needSave == null || needSave.IsNew)
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "请先保存数据" : "请先保存数据.");
                //    return;
                //}

                ////ManyResultData result = userService.SetUserDefaultOrganizationJob(needSave.ID, LocalData.UserInfo.LoginID, needSave.UpdateDate);

                //////needSave.IsDefault =

                //foreach (var r in result.ChildResults)
                //{
                //    User2OrganizationJobList data = _Source.Find(delegate(User2OrganizationJobList item) { return item.ID == r.ID; });
                //    if (data != null)
                //    {
                //        data.UpdateDate = r.UpdateDate;

                //        if (data.ID == needSave.ID)
                //        {
                //            data.IsDefault = true;
                //            data.IsDirty = false;
                //        }
                //        else
                //        {
                //            data.IsDefault = false;
                //            data.IsDirty = false;
                //        }

                //    }
                //}

                //List<Guid> selectIDs = new List<Guid>();
                //foreach (var item in _Source)
                //{
                //    selectIDs.Add(item.OrganizationJobID);
                //}

                //if (_Source != null && _Source.Count > 0)
                //{
                //    foreach (var item in _Source) { item.BeginEdit(); }
                //}

                //Dictionary<string, object> dic = new Dictionary<string, object>();
                //dic.Add("DefaultID", organization2JobList.ID);
                //jobOrgSelectPart1.Init(dic);
                //jobOrgSelectPart1.DataSource = selectIDs;


                //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                #endregion
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region IListPart 成员

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

        BaseList<User2OrganizationJobList> GetSelectedSource()
        {
            if (jobOrgSelectPart1.Created == false) return new BaseList<User2OrganizationJobList>(_Source);
            List<Guid> selectedIDs = jobOrgSelectPart1.DataSource as List<Guid>;
            List<User2OrganizationJobList> selected = new List<User2OrganizationJobList>();
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

                User2OrganizationJobList newData = new User2OrganizationJobList();
                newData.CreateById = LocalData.UserInfo.LoginID;
                newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                newData.UserID = _parentList.ID;
                newData.OrganizationJobID = item;
                selected.Add(newData);
            }

            bool isDirty = false;
            foreach (var item in selected)
            {
                if (item.IsNew) { isDirty = true; break; }
                if (item.IsDirty) { isDirty = true; break; }
            }
            ICP.Framework.CommonLibrary.Common.BaseList<User2OrganizationJobList> list = new BaseList<User2OrganizationJobList>(selected);
            list.IsDirty = isDirty;
            if (list.Count != _InitSelected.Count) list.IsDirty = true;
            else
            {
                foreach (var item in selected)
                {
                    if (_InitSelected.Contains(item.OrganizationJobID) == false)
                    { list.IsDirty = true; break; }
                }
            }
            return list;
        }

        List<User2OrganizationJobList> _Source = null;
        List<Guid> _InitSelected = new List<Guid>();


        void BindingData(object data)
        {
            if (data == null) { jobOrgSelectPart1.DataSource = null; }
            else
            {
                _Source = data as List<User2OrganizationJobList>;
                if (_Source != null && _Source.Count > 0)
                {
                    foreach (var item in _Source) { item.BeginEdit(); }
                }
                List<Guid> selectedIDs = new List<Guid>();
                Guid? defaultID = null;
                _InitSelected = new List<Guid>();

                foreach (var item in _Source)
                {
                    if (item.IsDefault) defaultID = item.OrganizationJobID;

                    selectedIDs.Add(item.OrganizationJobID);
                    _InitSelected.Add(item.OrganizationJobID);
                }
                jobOrgSelectPart1.DataSource = selectedIDs;

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("DefaultID", defaultID);
                jobOrgSelectPart1.Init(dic);
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

        UserList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as UserList;
                    if (_parentList == null || _parentList.IsNew || _parentList.IsValid == false)
                        this.Enabled = false;
                    else
                        this.Enabled = true;
                }
            }
        }

        #endregion


    }
}
