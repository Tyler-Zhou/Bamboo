using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
namespace ICP.Sys.UI.Role
{
    [ToolboxItem(false)]
    public partial class Role2FunctionListPart : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
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


        #endregion

        #region init

        public Role2FunctionListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._parentList = null;
                this._Source = null;
                this.CurrentChanged = null;
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
            List<FunctionList> functionLists = PermissionService.GetFunctionList(true);
            List<Guid> selectedIDs = new List<Guid>();
            if (_Source != null && _Source.Count > 0)
            {
                foreach (var item in _Source)
                {
                    selectedIDs.Add(item.PermissionID);
                }
            }
            dic.Add("OriginalList", functionLists);
            functionSelectPart.Init(dic);
            functionSelectPart.DataSource = selectedIDs;
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

            Dictionary<Guid, FunctionType> selection = functionSelectPart.DataSource as Dictionary<Guid, FunctionType>;

            if (selection == null) return false;

            List<RolePermissionList> needReflist = new List<RolePermissionList>();
            foreach (var item in selection)
            {
                RolePermissionList newData = new RolePermissionList();
                newData.PermissionID = item.Key;
                newData.RoleID = _parentList.ID;
                needReflist.Add(newData);
            }

            try
            {
                List<Guid> permissionIDs = new List<Guid>();
                List<bool> isAction = new List<bool>();
                foreach (var item in selection)
                {
                    permissionIDs.Add(item.Key);
                    if (item.Value == FunctionType.Action)
                        isAction.Add(true);
                    else
                        isAction.Add(false);
                }

                ManyResultData result = PermissionService.SetRoleFunctions(_parentList.ID
                                                               , permissionIDs.ToArray()
                                                               , isAction.ToArray()
                                                               , LocalData.UserInfo.LoginID);

                for (int i = 0; i < needReflist.Count; i++)
                {
                    needReflist[i].ID = result.ChildResults[i].ID;
                    needReflist[i].BeginEdit();
                }
                this._Source = needReflist;

                if (Saved != null) Saved(needReflist);

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

        BaseList<RolePermissionList> GetSelectedSource()
        {
            if (functionSelectPart.Created == false) return new BaseList<RolePermissionList>(_Source);
            Dictionary<Guid, FunctionType> selection = functionSelectPart.DataSource as Dictionary<Guid, FunctionType>;
            List<RolePermissionList> selected = new List<RolePermissionList>();
            foreach (var item in selection)
            {
                RolePermissionList tager = _Source.Find(delegate(RolePermissionList sitem) { return sitem.PermissionID == item.Key; });
                if (tager == null)
                {
                    tager = new RolePermissionList();
                    tager.PermissionID = item.Key;
                    tager.RoleID = _parentList.ID;
                    selected.Add(tager);
                }
                else
                    selected.Add(tager);
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

        List<RolePermissionList> _Source = null;
        void BindingData(object data)
        {
            if (data == null) { functionSelectPart.DataSource = new List<Guid>(); }
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
                    selectedIDs.Add(item.PermissionID);
                }
                functionSelectPart.DataSource = selectedIDs;
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
