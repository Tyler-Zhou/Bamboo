using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.Job
{
    [ToolboxItem(false)]
    public partial class JobEditPart : BaseEditPart
    {
        #region 服务注入
        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        #endregion

        #region 初始化

        public JobEditPart()
        {
            InitializeComponent(); this.Enabled = false;
            this.Disposed += delegate 
            {
                this.popParent.QueryPopUp -= popParent_QueryPopUp;
                this._originalList = null;
                this.dxErrorProvider1.DataSource = null;
                this.treeParent.DataSource = null;
                this.bsParent.DataSource = null;
                this.bsParent.Dispose();
                this.bindingSource1.DataSource = null;
                this.bindingSource1.Dispose();
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
            labCName.Text = "中文名";
            labCode.Text = "代码";
            labParent.Text = "上级";
            labDescription.Text = "描述";
            labEName.Text = "英文名";
            btnClearPop.Text = "清空";
            
            barSave.Caption = "保存(&S)";
        }

        protected override void OnLoad(EventArgs e)
        {
            panelScroll.Click += delegate { panelScroll.Focus(); };
            this.popParent.QueryPopUp += new CancelEventHandler(popParent_QueryPopUp);
            if (LocalData.IsEnglish)
                colEName.Visible = true;
            else
                colCName.Visible = true;
            base.OnLoad(e);
        }

        #endregion

        #region Property
        List<JobList> _originalList = null;
        JobInfo CurrentData
        {
            get { return bindingSource1.DataSource as JobInfo; }
        }

        #endregion

        #region Pop
        Guid? _initID = null;
        void popParent_QueryPopUp(object sender, CancelEventArgs e)
        {
            List<JobList> data = new List<JobList>();
            JobInfo currentData = bindingSource1.DataSource as JobInfo;
            if (currentData.ID != Guid.Empty)
            {
                if (_initID != null && _initID.Value == currentData.ID) return;

                List<Guid> needRemoveIds = GetChildIdsById(_originalList, currentData.ID);
                List<JobList> needAddChilds = _originalList.FindAll(delegate(JobList item) { return needRemoveIds.Contains(item.ID) == false; });
                foreach (var item in needAddChilds)
                {
                    data.Add(item);
                }
                _initID = currentData.ID;
            }
            else
            {
                _initID = null;
                foreach (var item in _originalList)
                {
                    data.Add(item);
                }
            }

            bsParent.DataSource = data;
            treeParent.ExpandAll();
        }

        /// <summary>
        /// 获取所有子项(包括自身)ID
        /// </summary>
        List<Guid> GetChildIdsById(List<JobList> data, Guid currentId)
        {
            List<Guid> childIds = new List<Guid>();
            childIds.Add(currentId);

            while (true)
            {
                List<JobList> childs = data.FindAll(delegate(JobList item)
                { return childIds.Contains(item.ParentID?? Guid.Empty) && childIds.Contains(item.ID) == false; });

                if (childs == null || childs.Count == 0)
                    break;
                else
                {
                    foreach (JobList item in childs)
                    {
                        childIds.Add(item.ID);
                    }
                }
            }
            return childIds;
        }


        JobList CurrentJobList
        {
            get { return bsParent.Current as JobList; }
        }

        private void treeParent_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentJobList != null)
            {
                JobInfo currentData = bindingSource1.DataSource as JobInfo;
                currentData.ParentID = CurrentJobList.ID;
                currentData.ParentName = LocalData.IsEnglish ? CurrentJobList.EName : CurrentJobList.CName;
            }
            popParent.ClosePopup();
        }

        private void btnClearPop_Click(object sender, EventArgs e)
        {
            JobInfo currentData = bindingSource1.DataSource as JobInfo;
            currentData.ParentID = Guid.Empty;
            currentData.ParentName = string.Empty;
            popParent.ClosePopup();
        }

        #endregion

        #region Save

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private bool Save()
        {
            this.Validate();
            bindingSource1.EndEdit();
            JobInfo currentData = bindingSource1.DataSource as JobInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false) return false;
            try
            {
                ManyHierarchyResultData result = JobService.SaveJobInfo(currentData.IsNew ? Guid.Empty : currentData.ID,
                                                                        currentData.ParentID,
                                                                        currentData.Code,
                                                                        currentData.CName,
                                                                        currentData.EName,
                                                                        currentData.Description,
                                                                        LocalData.UserInfo.LoginID,
                                                                        currentData.UpdateDate);

                currentData.ID = result.ID;
                currentData.HierarchyCode = result.HierarchyCode;
                currentData.UpdateDate = result.UpdateDate;
                currentData.CancelEdit();
                currentData.BeginEdit();
                if (Saved != null) Saved(new object[]{currentData,result});


                JobList exist= _originalList.Find(delegate(JobList item) { return item.ID == currentData.ID; });
                if (exist == null)
                    _originalList.Add(currentData);


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

        #region IEditPart 成员

        void BindingData(object data)
        {
            _initID = null;
            JobInfo info = data as JobInfo;

            #region 更新了IsValid 刷新父节点列表
            JobInfo currentInfo = this.bindingSource1.DataSource as JobInfo;
            if (currentInfo != null && info != null && currentInfo.ID == info.ID)
            {
                if (currentInfo.IsValid != info.IsValid)
                {
                    _originalList = JobService.GetJobList(string.Empty, string.Empty, true, 0);
                }
            }
            #endregion

            if (info == null) { this.bindingSource1.DataSource = typeof(JobInfo); this.Enabled = false; }
            else if (info.IsValid == false) 
            {
                this.Enabled = false;
                this.bindingSource1.DataSource = info;
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).EndEdit();
            }
            else
            {
                if (_originalList == null) _originalList = JobService.GetJobList(string.Empty, string.Empty, true, 0);

                if (Utility.GuidIsNullOrEmpty(info.ParentID) == false)
                {
                    JobList parent = _originalList.Find(delegate(JobList item) { return item.ID == info.ParentID; });
                    if (parent != null)
                        info.ParentName = LocalData.IsEnglish ? parent.EName : parent.CName;
                    
                }
                if (info.IsNew) { txtCode.Focus(); }

                this.bindingSource1.DataSource = info;
                this.Enabled = true;
                ((ICP.Framework.CommonLibrary.Common.BaseDataObject)data).BeginEdit();
            }
        }

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override void EndEdit()
        {
            bindingSource1.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion
    }
}
