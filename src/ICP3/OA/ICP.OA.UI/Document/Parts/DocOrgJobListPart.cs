using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.OA.UI.Document
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class DocOrgJobListPart : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ICP.OA.ServiceInterface.Client.IDocumentClientService DocumentClientService
        {
            get
            {
                return ServiceClient.GetClientService<ICP.OA.ServiceInterface.Client.IDocumentClientService>();
            }
        }

        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

     

        #endregion

        #region init


        public DocOrgJobListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this.Saved = null;
                this._folderList = null;
                this._parentList = null;
                gcMain.DataSource = null;
                if (this.bsList != null)
                {
                    this.bsList.DataSource = null;
                    this.bsList = null;
                }
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
            barRemove.Caption = "删除(&R)";

            colOrganizationName.Caption = "组织结构";
            colJobName.Caption = "职位";
            colPermission.Caption = "权限";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        protected virtual void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DocuentPermission>> permissionTypes 
                = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DocuentPermission>(LocalData.IsEnglish);
            foreach (var item in permissionTypes)
            {
                rcmbPermission.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            DataFindClientService.RegisterGridColumnFinder(colOrganizationName
                                             , SystemFinderConstants.OrganizationFinder
                                             , "OrganizationID"
                                             , "OrganizationName"
                                             , "ID"
                                             , LocalData.IsEnglish ? "EShortName" : "CShortName");

            DataFindClientService.RegisterGridColumnFinder(colJobName
                                            , SystemFinderConstants.JobFinder
                                            , "JobID"
                                            , "JobName"
                                            , "ID"
                                            , LocalData.IsEnglish ? "EName" : "CName");
        }

        #endregion

        List<DocumentOrganizationJobPermissionList> SelectedItem
        {
            get
            {
                int[] rowIndexs = gvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<DocumentOrganizationJobPermissionList> tagers = new List<DocumentOrganizationJobPermissionList>();
                foreach (var item in rowIndexs)
                {
                    DocumentOrganizationJobPermissionList data = gvMain.GetRow(item) as DocumentOrganizationJobPermissionList;
                    if (data != null) tagers.Add(data);
                }

                return tagers;
            }
        }

        #region List Event

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            DocumentOrganizationJobPermissionList list = gvMain.GetRow(e.RowHandle) as DocumentOrganizationJobPermissionList;
            if (list == null) return;

            if (list.IsDirty)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            else if (list.IsParentPermission)
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Warning);
        }

        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
            RefreshEnabled();
        }
        private void RefreshEnabled()
        {
            if (CurrentRow == null) { barRemove.Enabled = false; return; }

            if (CurrentRow.IsParentPermission)
            {
                barRemove.Enabled = false;
                colPermission.OptionsColumn.AllowEdit = colJobName.OptionsColumn.AllowEdit =colOrganizationName.OptionsColumn.AllowEdit = false;
            }
            else
            {
                colPermission.OptionsColumn.AllowEdit = colJobName.OptionsColumn.AllowEdit = colOrganizationName.OptionsColumn.AllowEdit = true;
                barRemove.Enabled = true;
            }
        }

        private void gvMain_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DocumentOrganizationJobPermissionList newData = gvMain.GetRow(e.RowHandle) as DocumentOrganizationJobPermissionList;
            if (newData == null) return;

            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.Permission = DocuentPermission.View;
            if (_parentList != null)
            {
                newData.FolderName = _parentList.Name;
            }
            if (_folderList != null)
            {
                newData.FolderName = _folderList.Name;
            }

            newData.IsDirty = false;
        }

        #endregion

        #region barItem

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }
        private bool Save()
        {
            if (_parentList == null && _folderList == null)
            {
                return false;
            }

            Guid folderID = _parentList != null ? _parentList.ID : _folderList.ID;
            this.Validate();
            bsList.EndEdit();

            if (ValidateData() == false) return false;

            List<DocumentOrganizationJobPermissionList> currentList = bsList.DataSource as List<DocumentOrganizationJobPermissionList>;

            List<DocumentOrganizationJobPermissionList> needSave = currentList.FindAll(delegate (DocumentOrganizationJobPermissionList item)
                                                                    { return item.IsParentPermission == false; });

            if (needSave == null || needSave.Count == 0) return false;

            try
            {

                List<Guid?> filePermissionIds = new List<Guid?>();
                List<Guid?> organizationIds = new List<Guid?>();
                List<Guid?> jobIds = new List<Guid?>();
                List<DocuentPermission> permissions = new List<DocuentPermission>();
                List<DateTime?> updateDates = new List<DateTime?>(); ;

                for (int i = 0; i < needSave.Count; i++)
                {
                    filePermissionIds.Add(needSave[i].ID);
                    organizationIds.Add(needSave[i].OrganizationID);
                    jobIds.Add(needSave[i].JobID);
                    permissions.Add(needSave[i].Permission);
                    updateDates.Add(needSave[i].UpdateDate);
                }

                ManyResultData result = DocumentClientService.SetJobDocumentPermissionInfo(folderID
                                                        , filePermissionIds.ToArray()
                                                        , organizationIds.ToArray()
                                                        , jobIds.ToArray()
                                                        , permissions.ToArray()
                                                        , updateDates.ToArray()
                                                        , LocalData.UserInfo.LoginID);

                for (int i = 0; i < needSave.Count; i++)
                {
                    needSave[i].ID = result.ChildResults[i].ID;
                    needSave[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    needSave[i].BeginEdit();
                    needSave[i].IsDirty = false;
                }

                if (Saved != null) Saved(needSave);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                return false;
            }
        }

        bool ValidateData()
        {
            List<DocumentOrganizationJobPermissionList> currentList = bsList.DataSource as List<DocumentOrganizationJobPermissionList>;
            if (currentList == null || currentList.Count == 0) return false;

            if (currentList.Find(delegate(DocumentOrganizationJobPermissionList item) { return item.IsParentPermission == false; }) == null) return false;

            bool isSrcc = true;
            foreach (var item in currentList)
            {
                if (item.Validate(delegate(ValidateEventArgs e)
                    {
                        if (Utility.GuidIsNullOrEmpty(item.OrganizationID) && Utility.GuidIsNullOrEmpty(item.JobID))
                        {
                            e.SetErrorInfo("OrganizationID", LocalData.IsEnglish ? "Oragnization or Job Must Input." : "至少输入一个组织结构或职位.");
                        }
                    }
                    ) == false) isSrcc = false;
            }
            return isSrcc;

        }

        private void barRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RemoveData();
        }

        private void RemoveData()
        {
            List<DocumentOrganizationJobPermissionList> selectedItem = SelectedItem;
            if (selectedItem == null || selectedItem.Count == 0) return;

            List<DocumentOrganizationJobPermissionList> needRemove = selectedItem.FindAll(delegate(DocumentOrganizationJobPermissionList item)
            { return item.IsParentPermission == false; });

            if (needRemove == null || needRemove.Count == 0) return;

            if (Utility.EnquireIsDeleteCurrentData() == false) return;
          
            try
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDatas = new List<DateTime?>();
                foreach (var item in needRemove)
                {
                    if (item.IsNew) continue;
                    ids.Add(item.ID);
                    updateDatas.Add(item.UpdateDate);
                }

                if(ids.Count !=0)
                    DocumentClientService.RemoveJobDocumentPermissionInfo(ids.ToArray(),updateDatas.ToArray(),LocalData.UserInfo.LoginID);

                List<DocumentOrganizationJobPermissionList> currentData = bsList.DataSource as List<DocumentOrganizationJobPermissionList>;
                foreach (var item in needRemove)
                {
                    currentData.Remove(item);
                }
                bsList.DataSource = currentData;
                bsList.ResetBindings(false);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        }

        #endregion

        #region interface

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }

        DocumentOrganizationJobPermissionList CurrentRow
        {
            get { return bsList.Current as DocumentOrganizationJobPermissionList; }
            set
            {
                DocumentOrganizationJobPermissionList current = CurrentRow;
                if (current != null) current = value;
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        void BindingData(object data)
        {
            if (data == null) { this.bsList.DataSource = new List<DocumentOrganizationJobPermissionList>(); }
            else
            {
                this.bsList.DataSource = data;
            }
            RefreshEnabled();
        }

        #endregion

        #region IEditPart 成员

        public override void EndEdit()
        {
            bsList.EndEdit();
        }

        public override bool SaveData()
        {
            return Save();
        }

        public event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region IPart 成员

        protected DocumentFolderFileList _parentList = null;
        protected DocumentFolderList _folderList = null;

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as DocumentFolderFileList;
                    if (_parentList == null||_parentList.Permission < DocuentPermission.Manager ) this.Enabled = false;
                    else this.Enabled = true;
                }
                if (item.Key == "BusinessList")
                {
                    _folderList = item.Value as DocumentFolderList;
                    if (_folderList == null || _folderList.Permission < DocuentPermission.Manager) this.Enabled = false;
                    else this.Enabled = true;
                }
            }
        }

        #endregion
        #endregion
    }
}
