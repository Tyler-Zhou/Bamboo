using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ICP.Framework.CommonLibrary.Common;
namespace ICP.Sys.UI.Job
{
    [ToolboxItem(false)]
    public partial class JobOrganizationPart : ICP.Framework.ClientComponents.UIFramework.BaseListPart
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

        public JobOrganizationPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.treeMain.DataSource = null;
                this.treeMain.AfterDragNode -= this.treeMain_AfterDragNode;
                this.treeMain.BeforeDragNode -= this.treeMain_BeforeDragNode;
                this.treeMain.DragDrop -= this.treeMain_DragDrop;
                this.treeMain.DragEnter -= this.treeMain_DragEnter;
                this.treeMain.GetStateImage -= this.treeMain_GetStateImage;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                this.CurrentChanged = null;
                this._parentList = null;
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
            barRefresh.Caption = "刷新(&F)";
            barDelete.Caption = "删除(&L)";
        }

        private void treeMain_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            Organization2JobList data = treeMain.GetDataRecordByNode(e.Node) as Organization2JobList;
            if (data == null) return;
            e.Node.StateImageIndex = (short)data.Type;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshData();
            RefreshEnabled();
        }

        #endregion

        #region barItem

        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshData();
        }
        private void RefreshData()
        {
            try
            {
                List<Organization2JobList> data = JobService.GetOrganization2JobList(null, true);
                bsList.DataSource = data;
                treeMain.ExpandAll();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }
        private void barDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteData();
        }
        private void DeleteData()
        {
            if (CurrentRow == null || CurrentRow.Type == OrganizationJobType.Organization
                || Utility.EnquireIsDeleteCurrentData() == false) return;

            try
            {
                JobService.RemovetOrganization2JobInfo(new Guid[] { CurrentRow.ID }
                                                        , new DateTime?[] { CurrentRow.UpdateDate }
                                                        , LocalData.UserInfo.LoginID);

                bsList.RemoveCurrent();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region DragDrop

        private void treeMain_DragEnter(object sender, DragEventArgs e)
        {
            TreeListNode tn = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            if (tn == null || tn.Tag == null) return;
            List<JobList> jobList = tn.Tag as List<JobList>;
            if (jobList == null || jobList.Count ==0) return;

            e.Effect = DragDropEffects.Copy;
        }

        private void treeMain_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                TreeListNode tn = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
                if (tn == null || tn.Tag == null) return;
                List<JobList> jobList = tn.Tag as List<JobList>;
                if (jobList == null || jobList.Count == 0) return;

                Point pt = treeMain.PointToClient(new Point(e.X, e.Y));
                TreeListHitInfo tnhitInfo = treeMain.CalcHitInfo(pt);
                if (tnhitInfo == null || tnhitInfo.Node == null) return;

                Organization2JobList dragTo = treeMain.GetDataRecordByNode(tnhitInfo.Node) as Organization2JobList;
                if (dragTo.Type == OrganizationJobType.Job) return;

                List<Organization2JobList> dargEnter = new List<Organization2JobList>();
                foreach (var item in jobList)
                {
                    Organization2JobList tmp = new Organization2JobList();
                    tmp.ParentID = dragTo.ID;
                    tmp.ParentName = dragTo.Name;
                    tmp.Type = OrganizationJobType.Job;
                    tmp.RelationID = item.ID;
                    tmp.Name = item.CName;
                    dargEnter.Add(tmp);
                }

                List<Organization2JobList> currentSource = DataSource as List<Organization2JobList>;
                List<Organization2JobList> newItem = ValidateExist(currentSource, dargEnter, dragTo);
                if (newItem == null || newItem.Count == 0)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "Exist Same Item." : "已存在重复的项");
                    return;
                }

                if (Utility.EnquireIsSaveCurrentData() == false) return;

                List<Guid?> ids = new List<Guid?>();
                List<Guid> relationIDs = new List<Guid>();
                List<DateTime?> updateDates = new List<DateTime?>();
                foreach (var item in newItem)
                {
                    ids.Add(null);
                    updateDates.Add(null);
                    relationIDs.Add(item.RelationID);
                }


                ManyResultData result = JobService.SetOrganization2JobInfo(dragTo.ID
                                                                           , ids.ToArray()
                                                                           , relationIDs.ToArray()
                                                                           , updateDates.ToArray()
                                                                           , LocalData.UserInfo.LoginID);

                for (int i = 0; i < newItem.Count; i++)
                {
                    newItem[i].ID = result.ChildResults[i].ID;
                    newItem[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    newItem[i].BeginEdit();
                }

                currentSource.AddRange(newItem);
                bsList.DataSource = currentSource;
                bsList.ResetBindings(false);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        /// <summary>
        /// 验证是否有重复项,返回非重复的
        /// </summary>
        private List<Organization2JobList> ValidateExist(List<Organization2JobList> source
                                            , List<Organization2JobList> dragEnter
                                            , Organization2JobList dragTo)
        {
            List<Organization2JobList> result = null;

            List<Organization2JobList> dragToChild = source.FindAll(delegate(Organization2JobList item)
            { return item.Type == OrganizationJobType.Job && item.ParentID == dragTo.ID; });

            List<Guid> existChildIDs = new List<Guid>();
            if (dragToChild != null && dragToChild.Count > 0)
            {
                foreach (var item in dragToChild)
                {
                    existChildIDs.Add(item.RelationID);
                }
            }
            result = dragEnter.FindAll(delegate(Organization2JobList item) { return existChildIDs.Contains(item.RelationID) == false; });
            return result;
        }

        #endregion

        #region DragNode

        Guid beforeDragParentID = Guid.Empty;
        private void treeMain_BeforeDragNode(object sender, BeforeDragNodeEventArgs e)
        {
            beforeDragParentID = Guid.Empty;
            Organization2JobList dragingOrgJob = treeMain.GetDataRecordByNode(e.Node) as Organization2JobList;
            if (dragingOrgJob == null || dragingOrgJob.Type == OrganizationJobType.Organization) e.CanDrag = false;
            beforeDragParentID = dragingOrgJob.ParentID;
            
        }

        private void treeMain_AfterDragNode(object sender, NodeEventArgs e)
        {
            AfterDragNode(e);
        }
        private void AfterDragNode(NodeEventArgs e)
        {
            Organization2JobList dargEnter = treeMain.GetDataRecordByNode(e.Node) as Organization2JobList;
            if (dargEnter == null) return;

            List<Organization2JobList> list = DataSource as List<Organization2JobList>;
            Organization2JobList dragTo = list.Find(delegate(Organization2JobList item) { return item.ID == dargEnter.ParentID; });
            if (dragTo == null || dragTo.Type == OrganizationJobType.Job)
            {
                dargEnter.ParentID = beforeDragParentID;
                treeMain.RefreshDataSource();
            }
            else
            {
                //同级拖放无效
                if (beforeDragParentID != Guid.Empty && dargEnter.ParentID == beforeDragParentID) return;

                if (ValidateExistByAfterDragNode(list, new List<Organization2JobList> { dargEnter }, dragTo) == false)
                {
                    dargEnter.ParentID = beforeDragParentID;
                    treeMain.RefreshDataSource();
                    beforeDragParentID = Guid.Empty;
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Data Exist" : "数据重复");
                    return;
                }

                if (Utility.EnquireIsSaveCurrentData() == false)
                {
                    dargEnter.ParentID = beforeDragParentID;
                    beforeDragParentID = Guid.Empty;
                    return;
                }

                try
                {
                   ManyResultData result=  JobService.SetOrganization2JobInfo(dargEnter.ParentID
                                                    , new Guid?[] { dargEnter.ID }
                                                    , new Guid[] { dargEnter.RelationID }
                                                    , new DateTime?[] { dargEnter.UpdateDate }, LocalData.UserInfo.LoginID);

                   dargEnter.ID = result.ChildResults [0].ID;
                   dargEnter.UpdateDate = result.ChildResults[0].UpdateDate;
                   dargEnter.BeginEdit();
                   LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
                }
                catch (Exception ex)
                {
                    dargEnter.ParentID = beforeDragParentID;
                    treeMain.RefreshDataSource();
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }
            beforeDragParentID = Guid.Empty;
        }

        /// <summary>
        /// 验证是否有重复项
        /// </summary>
        private bool ValidateExistByAfterDragNode(List<Organization2JobList> source
                                                                        , List<Organization2JobList> dragEnter
                                                                        , Organization2JobList dragTo)
        {
            List<Organization2JobList> result = null;

            List<Organization2JobList> dragToChild = source.FindAll(delegate(Organization2JobList item)
            { return item.Type == OrganizationJobType.Job && item.ParentID == dragTo.ID; });

            List<Guid> existChildIDs = new List<Guid>();
            //在AfterDragNode如果有两个相同的子项,就是重复
            if (dragToChild != null && dragToChild.Count > 0)
            {
                foreach (var item in dragToChild)
                {
                    if(existChildIDs.Contains(item.RelationID) )return false;
                    existChildIDs.Add(item.RelationID);
                }
            }

            return true;
        }

      

        #endregion

        #region positionChanged

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
            RefreshEnabled();
        }

        private void RefreshEnabled()
        {
            if (CurrentRow == null || CurrentRow.Type == OrganizationJobType.Organization)
                barDelete.Enabled = false;
            else
                barDelete.Enabled = true;
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        private Organization2JobList CurrentRow
        {
            get { return Current as Organization2JobList; }
        }

        public override object DataSource
        {
            get { return bsList.DataSource; }
            set
            {
                List<Organization2JobList> data = value as List<Organization2JobList>;
                if (data != null && data.Count > 0)
                {
                    foreach (var item in data) { item.BeginEdit(); }
                }
                bsList.DataSource = data;
                bsList.ResetBindings(false);
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        #region IPart 成员

        JobList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as JobList;
                    if (_parentList == null || _parentList.IsValid == false)
                        this.Enabled = false;
                    else
                        this.Enabled = true;
                }
            }
        }

        #endregion

        #endregion
    }
}
