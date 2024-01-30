using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Sys.UI.PermissionManage.Function
{
    [ToolboxItem(false)]
    public partial class Function2RoleListPart : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }


        public IRoleService RoleService
        {
            get
            {
                return ServiceClient.GetService<IRoleService>();
            }
        }

        #endregion

        #region init

        public Function2RoleListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.Saved = null;
                this.CurrentChanged = null;
                this._parentList = null;
                this._Source = null;
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
            List<RoleList> roles = RoleService.GetRoleList(string.Empty, true, 0);
            dic.Add("OriginalList", roles);
            List<Guid> selectedIDs = new List<Guid>();
            if (_Source != null && _Source.Count > 0)
            {
                foreach (var item in _Source)
                {
                    selectedIDs.Add(item.RoleID);
                }
            }
            roleSelectPart1.Init(dic);
            roleSelectPart1.DataSource = selectedIDs;
        }

        #endregion

        #region BarItem

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        BaseList<RolePermissionList> GetSelectedSource()
        {
            if (roleSelectPart1.Created == false) return new BaseList<RolePermissionList>(_Source);
            List<Guid> selectedIDs = roleSelectPart1.DataSource as List<Guid>;
            List<RolePermissionList> selected = new List<RolePermissionList>();
            foreach (var item in _Source)
            {
                if (selectedIDs.Contains(item.RoleID))
                    selected.Add(item);
            }

            List<Guid> existIDs = new List<Guid>();
            foreach (var item in selected)
            {
                existIDs.Add(item.RoleID);
            }

            foreach (var item in selectedIDs)
            {
                if (existIDs.Contains(item)) continue;

                RolePermissionList newData = new RolePermissionList();
                newData.PermissionID = _parentList.ID;
                newData.RoleID = item;
                selected.Add(newData);
            }

            bool isDirty = false;
            foreach (var item in selected)
            {
                if (item.IsNew) { isDirty = true; break; }
                if (item.IsDirty) { isDirty = true; break; }
            }
            ICP.Framework.CommonLibrary.Common.BaseList<RolePermissionList> list = new BaseList<RolePermissionList>(selected);
            list.IsDirty = isDirty;
            if (list.Count != _Source.Count) list.IsDirty = true;

            return list;
        }

        private bool Save()
        {
            if (_parentList == null) return false;


            BaseList<RolePermissionList> temp = GetSelectedSource();

            List<RolePermissionList> needSaveData = new List<RolePermissionList>();
            foreach (var item in temp)
            {
                needSaveData.Add(item);
            }
            if (needSaveData == null) return false;

            foreach (var item in needSaveData)
            {
                if (item.Validate() == false) return false;
            }

            try
            {
                List<Guid> roleIDs = new List<Guid>();
                foreach (var item in needSaveData)
                {
                    roleIDs.Add(item.RoleID);
                }
                ManyResultData result = null;

                if (_parentList.FunctionType == FunctionType.Function)
                {
                    result = PermissionService.SetFunctionRoles(_parentList.ID
                                                        , roleIDs.ToArray()
                                                        , LocalData.UserInfo.LoginID);
                }
                else
                {
                    result = PermissionService.SetActionRoles(_parentList.ID
                                                       , roleIDs.ToArray()
                                                       , LocalData.UserInfo.LoginID);
                }

                for (int i = 0; i < needSaveData.Count; i++)
                {
                    needSaveData[i].ID = result.ChildResults[i].ID;
                    needSaveData[i].UpdateDate = result.ChildResults[i].UpdateDate;
                    needSaveData[i].BeginEdit();
                }
                this._Source = needSaveData;
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

        List<RolePermissionList> _Source = null;

        void BindingData(object data)
        {
            if (data == null) { roleSelectPart1.DataSource = new List<Guid>(); }
            else
            {
                _Source = data as List<RolePermissionList>;
                if (_Source != null && _Source.Count > 0)
                {
                    foreach (var item in _Source) { item.BeginEdit(); }
                }
                List<Guid> selectedIDs = new List<Guid>();
                foreach (var item in _Source)
                {
                    selectedIDs.Add(item.RoleID);
                }
                roleSelectPart1.DataSource = selectedIDs;
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

        FunctionList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as FunctionList;
                    if (_parentList == null|| _parentList.FunctionType == FunctionType.Module)
                        this.Enabled = false;
                    else
                        this.Enabled = true;
                }
            }
        }

        #endregion
    }
}
