using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 合约权限界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPPermissionsPart : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
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

        #region 初始化
        /// <summary>
        /// 合约权限界面
        /// </summary>
        public OPPermissionsPart()
        {
            InitializeComponent();
            Enabled = false;
            Disposed += delegate
            {
                gcOrgJob.DataSource = null;
                gcUser.DataSource = null;
                bsOrgJob.PositionChanged -= bsOrgJob_PositionChanged;
                bsUser.PositionChanged -= bsUser_PositionChanged;
                bsOrgJob.DataSource = null;
                bsUser.DataSource = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }

        private void InitMessage()
        {
            RegisterMessage("OragnizationOrJobMustInput.", "Oragnization or Job Must Input.");
            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");
        }

        protected override void OnLoad(EventArgs e)
        {
            //ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();
            base.OnLoad(e);
            InitControls();
            //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }

        private void InitControls()
        {
            splitContainerControl1.SplitterPosition = Width / 2;
            InitComboboxSource();
            SearchRegister();
        }

        void InitComboboxSource()
        {
            #region OceanPermissionMode
            if (_parentList!=null&&_parentList.PermissionMode != null)
            {
                cmbPermissionMode.ShowSelectedValue(_parentList.PermissionMode, _parentList.PermissionMode.ToString());
            }
            cmbPermissionMode.OnFirstEnter += OncmbPermissionModeFirstEnter;
            cmbPermissionMode.SelectedIndexChanged += new EventHandler(cmbPermissionMode_SelectedIndexChanged);
            #endregion

            List<EnumHelper.ListItem<OceanPermission>> permissionTypes
              = EnumHelper.GetEnumValues<OceanPermission>(LocalData.IsEnglish);
            foreach (var item in permissionTypes)
            {
                rcmbOrgJobPermission.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                rcmbUserPermission.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

        }

        private void OncmbPermissionModeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<OceanPermissionMode>> permissionModes = EnumHelper.GetEnumValues<OceanPermissionMode>(LocalData.IsEnglish);
            cmbPermissionMode.Properties.BeginUpdate();
            permissionModes.RemoveAll(item => item.Value == OceanPermissionMode.None);
            cmbPermissionMode.Properties.Items.Clear();
            foreach (var item in permissionModes)
            {
                cmbPermissionMode.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbPermissionMode.Properties.EndUpdate();
        }

        void cmbPermissionMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            OceanPermissionMode mode = (OceanPermissionMode)Enum.Parse(typeof(OceanPermissionMode), cmbPermissionMode.EditValue.ToString());
            RefreshListEnalbedByMode(mode);

            _ModeChanged = true;
        }

        void SearchRegister()
        {

            DataFindClientService.RegisterGridColumnFinder(colUserName
                                            , SystemFinderConstants.UserFinder
                                            , "UserID"
                                            , "UserName"
                                            , "ID"
                                            , LocalData.IsEnglish ? "EName" : "EName");


            DataFindClientService.RegisterGridColumnFinder(colOrganizationName
                                             , SystemFinderConstants.OrganizationFinder
                                             , "OrganizationID"
                                             , "OrganizationName"
                                             , "ID"
                                             , LocalData.IsEnglish ? "EShortName" : "EShortName");

            DataFindClientService.RegisterGridColumnFinder(colJobName
                                            , SystemFinderConstants.JobFinder
                                            , "JobID"
                                            , "JobName"
                                            , "ID"
                                            , LocalData.IsEnglish ? "EName" : "EName");
        }

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_SaveData].Execute();
        }

        #endregion

        #region  接口

        public bool ValidateData() 
        {
            EndEdit();

            bool isSrcc = true;

            List<OceanPermissionList> userPermissions = bsUser.DataSource as List<OceanPermissionList>;
            if (userPermissions != null && userPermissions.Count > 0)
            {
                foreach (var item in userPermissions)
                {
                    if (item.Validate() == false) isSrcc = false;
                }
            }

            List<OceanPermissionList> orgJobPermissions = bsOrgJob.DataSource as List<OceanPermissionList>;
            if (orgJobPermissions != null && orgJobPermissions.Count > 0)
            {

                foreach (var item in orgJobPermissions)
                {
                    if (item.Validate(delegate(ValidateEventArgs e)
                    {
                        if (Utility.GuidIsNullOrEmpty(item.OrganizationID) && Utility.GuidIsNullOrEmpty(item.JobID))
                        {
                            e.SetErrorInfo("OrganizationID", NativeLanguageService.GetText(this, "OragnizationOrJobMustInput"));
                        }
                    }
                        ) == false) isSrcc = false;
                }
            }

            return isSrcc;
        }

        bool _ModeChanged = false;
        public bool IsModeChanged
        {
            get
            {
                if (_LoadedOceanID.IsNullOrEmpty() || _parentList == null || _LoadedOceanID != _parentList.ID) return false;

                return _ModeChanged;
            }
        }

        public bool IsPermissionsChanged
        {
            get
            {
                if (_LoadedOceanID.IsNullOrEmpty() || _parentList == null || _LoadedOceanID != _parentList.ID) return false;

                List<OceanPermissionList> userPermissions = bsUser.DataSource as List<OceanPermissionList>;
                if (userPermissions != null && userPermissions.Count > 0)
                {
                    foreach (var item in userPermissions)
                    {
                        if (item.IsDirty || item.IsNew) return true;
                    }
                }

                List<OceanPermissionList> orgJobPermissions = bsOrgJob.DataSource as List<OceanPermissionList>;
                if (orgJobPermissions != null && orgJobPermissions.Count > 0)
                {
                    foreach (var item in orgJobPermissions)
                    {
                        if (item.IsDirty || item.IsNew) return true;
                    }
                }

                return false;
            }
        }

        public PermissionsModeCollect GetPermissionsModeCollect()
        {
            PermissionsModeCollect p = new PermissionsModeCollect();
            p.Mode = (OceanPermissionMode)Enum.Parse(typeof(OceanPermissionMode), cmbPermissionMode.EditValue.ToString());
            p.OceanUpdateDate = _parentList.UpdateDate;
            return p;
        }

        internal void RefreshUIData()
        {
        }

        #endregion

        #region IEditPart 成员

        public override object DataSource
        {
            get
            {
                List<OceanPermissionList> source = new List<OceanPermissionList>();
                List<OceanPermissionList> job = bsOrgJob.DataSource as List<OceanPermissionList>;
                List<OceanPermissionList> user = bsUser.DataSource as List<OceanPermissionList>;

                if(job != null) source.AddRange(job);

                if(user != null) source.AddRange(user);

                return source;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            if (_parentList == null || _parentList.OceanUnits == null || _parentList.OceanUnits.Count == 0)
            {
                Enabled = false;
                bsUser.DataSource = typeof(OceanPermissionList);
                bsOrgJob.DataSource = typeof(OceanPermissionList);
                return;
            }

            List<OceanPermissionList> datas = value as List<OceanPermissionList>;
            if (datas == null) datas = new List<OceanPermissionList>();

            List<OceanPermissionList> OrgJobPermissionList = datas.FindAll(p => p.Type == UserObjectType.Job);
            if (OrgJobPermissionList == null) OrgJobPermissionList = new List<OceanPermissionList>();


            List<OceanPermissionList> UserPermissionList = datas.FindAll(p => p.Type == UserObjectType.User);
            if (UserPermissionList == null)
            {
                UserPermissionList = new List<OceanPermissionList>();
            }
            List<OceanPermissionList>  upList = new List<OceanPermissionList>();
            //特别说明:此处控制了UI不显示运价的创建者的权限
            foreach (OceanPermissionList item in UserPermissionList)
            {
                if (item.UserID != null && item.UserID.Value != _parentList.CreateByID)
                {
                    upList.Add(item);
                }
            }
           // UserPermissionList =UserPermissionList.FindAll(u => u.UserID.Value != _parentList.CreateByID);

            bsOrgJob.DataSource = OrgJobPermissionList;
            bsUser.DataSource = UserPermissionList;
            bsOrgJob.ResetBindings(false);
            bsUser.ResetBindings(false);

           
        }

        public override void EndEdit()
        {
            Validate();
            bsUser.EndEdit();
            bsOrgJob.EndEdit();
        }

        #endregion

        #region IPart 成员


        Guid _LoadedOceanID = Guid.Empty;
        [CommandHandler(OPCommonConstants.Command_TabChanged)]
        public void Command_TabChanged(object sender, EventArgs e)
        {
            try
            {
                if (Visible == false || _LoadedOceanID.IsNullOrEmpty() == false) return;
                Enabled = _parentList != null;
                if (_parentList != null && _parentList.ID.IsNullOrEmpty() == false)
                {
                    List<OceanPermissionList> list = OceanPriceService.GetOceanPermissionList(_parentList.ID);
                    _LoadedOceanID = _parentList.ID;
                    DataSource = list;
                   
                }
            }catch(Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }

        }

        OceanList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as OceanList;
                    if (_parentList == null
                        || _parentList.IsNew
                        || _parentList.Permission < OceanPermission.Edit
                        || _parentList.OceanUnits == null
                        || _parentList.OceanUnits.Count == 0)
                        Enabled = false;
                    else
                    {
                        Enabled = true;
                        cmbPermissionMode.EditValue = _parentList.PermissionMode;

                        RefreshListEnalbedByMode(_parentList.PermissionMode);
                    }

                    if (Visible == true && Enabled == true)
                    {
                        List<OceanPermissionList> list = OceanPriceService.GetOceanPermissionList(_parentList.ID);
                        _LoadedOceanID = _parentList.ID;
                        DataSource = list;

                    }
                    else _LoadedOceanID = Guid.Empty;

                    _ModeChanged = false;

                    #region  刷新 Publish按钮状态
                    if (_parentList == null)
                    {
                        barPublish.Enabled = false;
                    }
                    else
                    {
                        if (_parentList.State == OceanState.Expired)
                            barPublish.Enabled = false;
                        else
                            barPublish.Enabled = true;

                        if (_parentList.State == OceanState.Expired || _parentList.State == OceanState.Invalidated || _parentList.State == OceanState.Draft)
                        {
                            barPublish.Caption = NativeLanguageService.GetText(this, "Publish");
                        }
                        else
                        {
                            barPublish.Caption = NativeLanguageService.GetText(this, "Pause");
                        }
                    }

                    #endregion
                }
            }


        }

        private void RefreshListEnalbedByMode(OceanPermissionMode Mode)
        {
            if (Mode == OceanPermissionMode.General) { panelOrgJob.Enabled = panelUser.Enabled = false; }
            else panelOrgJob.Enabled = panelUser.Enabled = true;
        }


        #endregion

        #region 本地成员

        OceanPermissionList CurrentUserRow
        {
            get { return bsUser.Current as OceanPermissionList; }
        }

        OceanPermissionList CurrentOrgJobRow
        {
            get { return bsOrgJob.Current as OceanPermissionList; }
        }

        List<OceanPermissionList> SelectedUsers
        {
            get
            {
                int[] rowIndexs = gvUser.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<OceanPermissionList> tagers = new List<OceanPermissionList>();
                foreach (var item in rowIndexs)
                {
                    OceanPermissionList data = gvUser.GetRow(item) as OceanPermissionList;
                    if (data != null) tagers.Add(data);
                }

                return tagers;
            }
        }

        List<OceanPermissionList> SelectedOrgJob
        {
            get
            {
                int[] rowIndexs = gvOrgJob.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<OceanPermissionList> tagers = new List<OceanPermissionList>();
                foreach (var item in rowIndexs)
                {
                    OceanPermissionList data = gvOrgJob.GetRow(item) as OceanPermissionList;
                    if (data != null) tagers.Add(data);
                }

                return tagers;
            }
        }

        #endregion

        #region gvEvent

        private void gvOrgJob_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BaseDataObject list = gvOrgJob.GetRow(e.RowHandle) as BaseDataObject;
            if (list == null) return;

            if (list.IsDirty)
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
        }

        private void gvUser_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            BaseDataObject list = gvUser.GetRow(e.RowHandle) as BaseDataObject;
            if (list == null) return;

            if (list.IsDirty)
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
        }

        private void bsOrgJob_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentOrgJobRow == null) barRemoveOrgJob.Enabled = false;
            else barRemoveOrgJob.Enabled = true;
               
        }

        private void bsUser_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentUserRow == null) barRemoveUser .Enabled = false;
            else barRemoveUser.Enabled = true;
        }

        private void gvOrgJob_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            OceanPermissionList newData = gvOrgJob.GetRow(e.RowHandle) as OceanPermissionList;
            if (newData == null) return;

            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.Permission = OceanPermission.View;
            newData.Type = UserObjectType.Job;
            if (_parentList != null)
            {
                newData.OceanID = _parentList.ID;
            }
            newData.IsDirty = false;
        }

        private void gvUser_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            OceanPermissionList newData = gvUser.GetRow(e.RowHandle) as OceanPermissionList;
            if (newData == null) return;

            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.Permission = OceanPermission.View;
            newData.Type = UserObjectType.User ;
            if (_parentList != null)
            {
                newData.OceanID = _parentList.ID;
            }
            newData.IsDirty = false;
        }

        #endregion

        #region 工作流

        private void barRemoveOrgJob_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<OceanPermissionList> selectedItem = SelectedOrgJob;
            if (selectedItem == null || selectedItem.Count == 0) return;

            if (Utility.EnquireIsDeleteCurrentData() == false) return;

            try
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDatas = new List<DateTime?>();
                foreach (var item in selectedItem)
                {
                    if (item.IsNew) continue;
                    ids.Add(item.ID);
                    updateDatas.Add(item.UpdateDate);
                }

                OceanPriceService.RemoveOceanPermissionInfo(ids.ToArray(), updateDatas.ToArray(), LocalData.UserInfo.LoginID);

                List<OceanPermissionList> currentData = bsOrgJob.DataSource as List<OceanPermissionList>;
                foreach (var item in selectedItem)
                {
                    currentData.Remove(item);
                }
                bsOrgJob.DataSource = currentData;
                bsOrgJob.ResetBindings(false);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        private void barRemoveUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<OceanPermissionList> selectedItem = SelectedUsers;
            if (selectedItem == null || selectedItem.Count == 0) return;

            if (Utility.EnquireIsDeleteCurrentData() == false) return;

            try
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDatas = new List<DateTime?>();
                foreach (var item in selectedItem)
                {
                    if (item.IsNew) continue;
                    ids.Add(item.ID);
                    updateDatas.Add(item.UpdateDate);
                }

                OceanPriceService.RemoveOceanPermissionInfo(ids.ToArray(), updateDatas.ToArray(), LocalData.UserInfo.LoginID);

                List<OceanPermissionList> currentData = bsUser.DataSource as List<OceanPermissionList>;
                foreach (var item in selectedItem)
                {
                    currentData.Remove(item);
                }
                bsUser.DataSource = currentData;
                bsUser.ResetBindings(false);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }

        private void barPublish_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[OPCommonConstants.Command_PublishPauseData].Execute();
        }

        #endregion

        #region 启用发布按钮
        /// <summary>
        /// 设置为可以发布
        /// </summary>
        public void SetPublish()
        {
            barPublish.Enabled = true;
            barPublish.Caption = NativeLanguageService.GetText(this, "Publish");
        }
        #endregion

    }
}
