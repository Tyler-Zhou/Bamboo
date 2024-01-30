using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;


namespace ICP.Sys.UI.UserManage
{
    [ToolboxItem(false)]
    public partial class User2FunctionListPart : ICP.Framework.ClientComponents.UIFramework.BaseListEditPart
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

        public User2FunctionListPart()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.CurrentChanged = null;
                this._parentList = null;
                this._Source = null;
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
                    selectedIDs.Add(item.ID);
                }
            }
            dic.Add("OriginalList", functionLists);
            dic.Add("CanCheck", false);
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
            //if (_parentList == null) return false;

            //List<Guid> selectedIDs = functionSelectPart.DataSource as List<Guid>;
            //List<FunctionList> needSaveData = new List<FunctionList>();
            //foreach (var item in _Source)
            //{
            //    if (selectedIDs.Contains(item.ID))
            //        needSaveData.Add(item);
            //}

            //List<Guid> existIDs = new List<Guid>();
            //foreach (var item in needSaveData)
            //{
            //    existIDs.Add(item.ID);
            //}

            //foreach (var item in selectedIDs)
            //{
            //    if (existIDs.Contains(item)) continue;

            //    FunctionList newData = new FunctionList();
            //    newData.ID = _parentList.ID;
            //    newData.ID = item;
            //    needSaveData.Add(newData);
            //}

            //if (needSaveData == null || needSaveData.Count == 0) return false;

            //foreach (var item in needSaveData)
            //{
            //    if (item.Validate() == false) return false;
            //}

            //try
            //{
            //    List<Guid?> ids = new List<Guid?>();
            //    List<Guid> permissionIDs = new List<Guid>();
            //    List<DateTime?> updateDatas = new List<DateTime?>();
            //    foreach (var item in needSaveData)
            //    {
            //        ids.Add(item.ID);
            //        permissionIDs.Add(item.PermissionID);
            //        updateDatas.Add(item.UpdateDate);
            //    }

            //    ManyResultData result = pService.SetFunctionRolePermission(ids.ToArray()
            //                                                               , permissionIDs.ToArray()
            //                                                               , new Guid[] { _parentList.ID }
            //                                                               , LocalData.UserInfo.LoginID
            //                                                               , updateDatas.ToArray());

            //    for (int i = 0; i < needSaveData.Count; i++)
            //    {
            //        needSaveData[i].ID = result.ChildResults[i].ID;
            //        needSaveData[i].UpdateDate = result.ChildResults[i].UpdateDate;
            //    }

            //    if (Saved != null) Saved(needSaveData);

            //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            //}
            //catch (Exception ex)
            //{
            //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            //    return false;
            //}
        }

        #endregion

        #region IListPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;

        public override object DataSource
        {
            get
            {
                return _Source;
            }
            set
            {
                BindingData(value);
            }
        }

        List<FunctionList> _Source = null;
        void BindingData(object data)
        {
            if (data == null) { functionSelectPart.DataSource = new List<Guid>(); }
            else
            {
                _Source = data as List<FunctionList>;
                if (_Source != null && _Source.Count > 0)
                {
                    foreach (var item in _Source) { item.BeginEdit(); }
                }
                List<Guid> selectedIDs = new List<Guid>();
                foreach (var item in _Source)
                {
                    selectedIDs.Add(item.ID);
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

        UserList _parentList = null;
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "ParentList")
                {
                    _parentList = item.Value as UserList;
                    if (_parentList == null || _parentList.IsValid == false)
                        this.Enabled = false;
                    else
                        this.Enabled = true;
                }
            }
        }

        #endregion
    }
}
