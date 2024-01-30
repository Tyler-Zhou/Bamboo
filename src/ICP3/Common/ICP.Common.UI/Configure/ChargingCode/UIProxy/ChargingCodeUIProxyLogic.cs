using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.Configure.Charging
{

    public class ChargingLayoutUIProxyLogic : ChargingLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {                
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[2] as UIProxyElement).ProxyType = typeof(ChargingGroupUIProxyLogic);
                (elements[3] as UIProxyElement).ProxyType = typeof(ChargingCodeUIProxyLogic);
                (elements[4] as UIProxyElement).ProxyType = typeof(ChargingCodeEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class ChargingGroupUIProxyLogic : ChargingGroupUIProxy
    {
        #region Service


        public IConfigureService configureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                value.BeforeDragItem += new EventHandler<DragCancelableArgs>(value_BeforeDragItem);

                value.AddToolAction("Edit", delegate(object obj)
                {
                    ChargingGroupList group = (ChargingGroupList)obj;

                    return group == null || group.NodeType == ChargeGroupNodeType.Root;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                value.AddToolAction("Delete", delegate(object obj)
                {
                    ChargingGroupList group = (ChargingGroupList)obj;

                    return group == null || group.NodeType == ChargeGroupNodeType.Root;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                _dataHoster = value;
            }
        }

        [ServiceDependency]
        public ICP.Framework.CommonLibrary.Client.IStatusbar statusbar { get; set; }

        #endregion

        void value_BeforeDragItem(object sender, DragCancelableArgs e)
        {
            ChargingGroupList dragItem = e.DragItem as ChargingGroupList;
            ChargingGroupList dropto = e.Dropto as ChargingGroupList;

            if (dragItem == null || dropto == null)
            {
                e.Cancel = true;
            }
            else if (dragItem.NodeType == ChargeGroupNodeType.Root)
            {
                e.Cancel = true;
            }
        }

        public ChargingGroupList CurrentRow { get { return _dataHoster.GetCurrentItem<ChargingGroupList>(); } }

        #region Method

        protected override bool AddData(object currentitem)
        {
            ChargingGroupList parentList = CurrentRow;

            ChargingGroupInfo newData = new ChargingGroupInfo();

            if (parentList != null)
            {
                newData.ParentID = parentList.ID;
                newData.ParentName = LocalData.IsEnglish ? parentList.EName : parentList.CName;
            }

            newData.ID = Guid.Empty;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsDirty = false;
            newData.NodeType = ChargeGroupNodeType.Group;

            workitem.Services.Get<IDataHoster>().OpenUI<ChargingGroupEditUIProxyLogic>(newData, null, true);

            return true;
        }

        protected override bool EditData(object currentitem)
        {
            ChargingGroupList currentData = currentitem as ChargingGroupList;
            if (currentData == null) return false;

            ChargingGroupInfo editData = configureService.GetChargingGroupInfo(currentData.ID);
            workitem.Services.Get<IDataHoster>().OpenUI<ChargingGroupEditUIProxyLogic>(editData, null, true);

            return true;
        }

        protected override bool DeleteData(object currentitem)
        {
            ChargingGroupList currentItem = currentitem as ChargingGroupList;
            if (currentitem == null) return false;
            DialogResult dlg = XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg == DialogResult.Cancel) return false;

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                List<ChargingCodeList> codeList = configureService.GetChargingCodeListByGroupID(currentItem.ID);
                if (codeList != null && codeList.Count > 0)
                {
                    XtraMessageBox.Show("无法删除费用代码组，请先删除其下的费用代码!");
                    return true;
                }

                configureService.RemoveChargingGroupInfo(
                    currentItem.ID,
                    LocalData.UserInfo.LoginID,
                    currentItem.UpdateDate);

                List<ChargingGroupList> currentlist = _dataHoster.ContentPart.DataSource as List<ChargingGroupList>;
                List<ChargingGroupList> removes = currentlist.FindAll(delegate(ChargingGroupList item) { return item.HierarchyCode.StartsWith(currentItem.HierarchyCode); });
                foreach (var item in removes)
                {
                    _dataHoster.RemoveData(item);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(control, LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(control, ex); }
            return true;
        }
      
        protected override bool RefreshData(object currentitem)
        {
            List<ChargingGroupList> list = configureService.GetChargingGroupList(string.Empty, string.Empty, null, 0);
            _dataHoster.ContentPart.DataSource = list;
            return true;
        }

        protected override bool DragData(object currentitem)
        {
            ChargingGroupList currentItem = currentitem as ChargingGroupList;
            if (currentItem == null)
            {
                return false;
            }


            Control owner = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                ManyHierarchyResultData result = configureService.SetChargingGroupParent(currentItem.ID,
                                                  currentItem.ParentID,
                                                  LocalData.UserInfo.LoginID,
                                                  currentItem.UpdateDate);

                List<ChargingGroupList> datas = UpdateChargingGroupList(_dataHoster.ContentPart.DataSource, result);
                _dataHoster.RefreshData(datas);
               // _dataHoster.RefreshData(datas[0]);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(owner, LocalData.IsEnglish ? "Set Successfully" : "设置父级成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(owner, ex);
                return false;
            }
        }

        List<ChargingGroupList> UpdateChargingGroupList(object datasource, ManyHierarchyResultData result)
        {
            if (_dataHoster.ContentPart.DataSource == null) return new List<ChargingGroupList>();

            List<ChargingGroupList> returnlist = new List<ChargingGroupList>();
            List<ChargingGroupList> sourcelist = _dataHoster.ContentPart.DataSource as List<ChargingGroupList>;
            foreach (SingleHierarchyResultData sr in result.ChildResults)
            {
                ChargingGroupList value = sourcelist.Find(delegate(ChargingGroupList v) { return v.ID == sr.ID; });
                if (value != null)
                {
                    value.UpdateDate = sr.UpdateDate;
                    value.HierarchyCode = sr.HierarchyCode;

                    returnlist.Add(value);
                }
            }

            return returnlist;
        }

        #endregion

    }
    public class ChargingGroupEditUIProxyLogic : ChargingGroupEditUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set
            {
                _dataHoster = value;
                value.AddToolAction("Save", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
            }
            get { return _dataHoster; }
        }

        [ServiceDependency]
        public ICP.Framework.CommonLibrary.Client.IStatusbar statusbar { get; set; }

        [ServiceDependency]
        public IConfigureService configureService { get; set; }

        #endregion

        protected override bool SaveData(object data)
        {
            

            ChargingGroupInfo currentData = _dataHoster.ContentPart.Current as ChargingGroupInfo;
            if (currentData == null) return false;

            if (currentData.Validate() == false)
            {
                return false;
            }

            Form control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);

            try
            {

                ManyHierarchyResultData result = configureService.SaveChargingGroupInfo(currentData.ID,
                                                                     currentData.ParentID,
                                                                     currentData.Code,
                                                                     currentData.CName,
                                                                     currentData.EName,
                                                                     LocalData.UserInfo.LoginID,
                                                                     currentData.UpdateDate);

                List<ChargingGroupList> changedResults = this.UpdateChargingGroupList(_dataHoster.GetParentDataSource < ChargingGroupList>(), currentData, result);
                _dataHoster.RefreshData(changedResults);

                control.Close();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(control, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(control, ex);
                return false;
            }
        }

        List<ChargingGroupList> UpdateChargingGroupList(object datasource, ChargingGroupInfo currentData, ManyHierarchyResultData result)
        {
            if (_dataHoster.ContentPart.DataSource == null) return new List<ChargingGroupList>();

            List<ChargingGroupList> returnlist = new List<ChargingGroupList>();

            if (currentData.IsNew)
            {
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                currentData.HierarchyCode = result.HierarchyCode;
                returnlist.Add(currentData);
            }
            else
            {
                List<ChargingGroupList> sourcelist = datasource as List<ChargingGroupList>;

                foreach (SingleHierarchyResultData sr in result.ChildResults)
                {
                    if (currentData.ID == sr.ID)
                    {
                        currentData.UpdateDate = result.UpdateDate;
                        currentData.HierarchyCode = result.HierarchyCode;
                        returnlist.Add(currentData);
                    }
                    ChargingGroupList value = sourcelist.Find(delegate(ChargingGroupList v) { return v.ID == sr.ID; });
                    if (value != null)
                    {
                        value.UpdateDate = sr.UpdateDate;
                        value.HierarchyCode = sr.HierarchyCode;

                        returnlist.Add(value);
                    }
                }
            }

            return returnlist;
        }

    }

    public class ChargingCodeUIProxyLogic : ChargingCodeUIProxy
    {
        #region 服务

        [ServiceDependency]
        public IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ICP.Framework.CommonLibrary.Client.IStatusbar statusbar { get; set; }

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return false;
                    else return ((ChargingCodeList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));


                value.AddToolAction("Edit", delegate(object obj)
                {
                    if (obj == null || parentList == null) return false;
                    else return ((ChargingCodeList)obj).IsValid == false;
                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null || parentList == null) return true;
                    else return false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));


                value.AddToolAction("Add", delegate(object obj)
                {
                    return parentList == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((ChargingCodeList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                _dataHoster = value;
            }
        }

        #endregion

        ChargingGroupList parentList = null;

        #region Method

        protected override bool AddData(object currentitem)
        {

            if (parentList == null) return false;

            ChargingCodeInfo newData = new ChargingCodeInfo();
            newData.GroupID = parentList.ID;
            newData.GroupName = LocalData.IsEnglish ? parentList.EName : parentList.CName;

            newData.ID = Guid.Empty;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
            newData.IsValid = true;
            _dataHoster.RefreshData(newData);

            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            ChargingCodeList currentRow = currentitem as ChargingCodeList;
            if (currentRow == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = configureService.ChangeChargingCodeState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    currentRow.IsValid = !currentRow.IsValid;
                    currentRow.UpdateDate = result.UpdateDate;
                    _dataHoster.RefreshData(currentRow);
                    _dataHoster.RaiseCurrentChanged(currentRow);
                }
                else
                {
                    _dataHoster.RemoveData(currentRow);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        protected override bool Close(object currentitem)
        {
            Form form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            if (form != null)
            {
                form.Close();
            }

            return true;
        }

        protected override bool GroupChanged(object obj)
        {
            parentList = obj as ChargingGroupList;
            List<ChargingCodeList> list = new List<ChargingCodeList>();

            if (parentList != null)
            {
                list = configureService.GetChargingCodeListByGroupID(parentList.ID);
            }

            _dataHoster.ContentPart.DataSource = list;
            //workitem.State["ChargingCodeList"] = list;
            return true;
        }

        #endregion
    }
    public class ChargingCodeEditUIProxyLogic : ChargingCodeEditUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set 
            {
                value.AddToolAction("Save", delegate(object obj)
                {
                    if (obj == null) return true;
                    else return ((ChargingCodeInfo)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));


                _dataHoster = value; 
            }
            get { return _dataHoster; }
        }

        [ServiceDependency]
        public ICP.Framework.CommonLibrary.Client.IStatusbar statusbar { get; set; }

        [ServiceDependency]
        public IConfigureService configureService { get; set; }

        #endregion

        protected override bool SaveData(object data)
        {
            

            ChargingCodeInfo currentData = _dataHoster.ContentPart.DataSource as ChargingCodeInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {

                SingleHierarchyResultData result = configureService.SaveChargingCodeInfo(currentData.ID,
                                                                                 currentData.GroupID,
                                                                                 currentData.Code,
                                                                                 currentData.CName,
                                                                                 currentData.EName,
                                                                                 currentData.IsCommission,
                                                                                 LocalData.UserInfo.LoginID,
                                                                                 currentData.UpdateDate);
                currentData.CancelEdit();
                currentData.ID = result.ID;
                currentData.GroupHierarchyCode = result.HierarchyCode;
                currentData.UpdateDate = result.UpdateDate;
                currentData.BeginEdit();
                _dataHoster.RefreshData(currentData);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(control, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(control, ex);
                return false;
            }
        }

        protected override bool BeforeParentChanged(object obj)
        {
            return CommonUtility.BeforeParentChanged(obj, _dataHoster, SaveData);           
        }

        protected override bool ParentChanged(object obj)
        {
            ChargingCodeList list = obj as ChargingCodeList;
            ChargingCodeInfo info = null;

            if (list != null && list.ID != Guid.Empty)
            {
                info = configureService.GetChargingCodeInfo(list.ID);
            }
            else
            {
                info = list as ChargingCodeInfo;
            }

            _dataHoster.ContentPart.DataSource = info;
            _dataHoster.ExecuteToolAction("Save", info);

            return true;
        }
    }

    #region 搜索器

    //public class ChargingCodeFinderUIProxyLogic : ChargingCodeDataFinderUIProxy
    //{
    //    #region 服务

    //    [ServiceDependency]
    //    public IConfigureService configureService { get; set; }

    //    [ServiceDependency]
    //    public ICP.Framework.CommonLibrary.Client.IStatusbar statusbar { get; set; }

    //    IDataHoster _hoster;
    //    [ServiceDependency]
    //    public IDataHoster hoster
    //    {
    //        set
    //        {
    //            value.AddToolAction("Edit", delegate(object obj)
    //            {
    //                bool flag = LocalCommonServices.PermissionService.HaveActionPermission("SOLUTION_ADDCHARGINGCODE");                
    //                return (obj == null || !flag);

    //            }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

    //            value.AddToolAction("Add", delegate(object obj)
    //            {
    //                return !LocalCommonServices.PermissionService.HaveActionPermission("SOLUTION_ADDCHARGINGCODE");

    //            }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

    //            _hoster = value;
    //            _hoster.CurrentChanged += new EventHandler<CommonEventArgs<BaseDataObject>>(_hoster_CurrentChanged);
    //        }
    //    }

    //    void _hoster_CurrentChanged(object sender, CommonEventArgs<BaseDataObject> e)
    //    {
    //        ChargingCodeList list = e.Data as ChargingCodeList;
    //        if (list == null) return;

    //        parentID = list.GroupID;
    //        parentName = list.GroupName;
    //    }

    //    #endregion

    //    public override Type SearchPartType
    //    {
    //        get { return typeof(ICP.Common.UI.Configure.ChargingCode.ChargingCodeSearchPart); }
    //    }

    //    Guid parentID = Guid.Empty;
    //    string parentName = string.Empty;

    //    #region Method

    //    protected override bool AddData(object currentitem)
    //    {

    //        if (parentID == Guid.Empty) return false;

    //        ChargingCodeInfo newData = new ChargingCodeInfo();
    //        newData.GroupID = parentID;
    //        newData.GroupName = parentName;

    //        newData.ID = Guid.Empty;
    //        newData.CreateByID = LocalData.UserInfo.LoginID;
    //        newData.CreateByName = LocalData.UserInfo.LoginName;
    //        newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
    //        newData.IsValid = true;

    //        workitem.Services.Get<IDataHoster>().OpenUI<ChargingCodeEditUIProxyLogic>(newData, null, true);

    //        return true;
    //    }

    //    protected override bool EditData(object currentitem)
    //    {
    //        ChargingCodeList currentData = currentitem as ChargingCodeList;
    //        if (currentData == null) return false;

    //        ChargingCodeInfo editData = configureService.GetChargingCodeInfo(currentData.ID);
    //        workitem.Services.Get<IDataHoster>().OpenUI<ChargingCodeEditUIProxyLogic>(editData, null, true);

    //        return true;
    //    }

    //    #endregion
    //}


    #endregion
}
