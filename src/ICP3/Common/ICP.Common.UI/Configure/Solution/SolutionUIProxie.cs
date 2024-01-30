using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System.Collections;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI
{
    public class SolutionLayoutUIProxyLogic : SolutionLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(SolutionUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(SolutionEditUIProxyLogic);
                (elements[3] as UIProxyElement).ProxyType = typeof(SolutionCodeRuleUIProxyLogic);
                (elements[4] as UIProxyElement).ProxyType = typeof(SolutionCurrencyUIProxyLogic);
                (elements[5] as UIProxyElement).ProxyType = typeof(SolutionExchangeRateUIProxyLogic);
                (elements[6] as UIProxyElement).ProxyType = typeof(SolutionAgentUIProxyLogic);

                (elements[10] as UIProxyElement).ProxyType = typeof(SolutionChargingGropUIProxyLogic);
                (elements[9] as UIProxyElement).ProxyType = typeof(SolutionChargingCodeUIProxyLogic);
                (elements[12] as UIProxyElement).ProxyType = typeof(SolutionGLGroupUIProxyLogic);
                (elements[11] as UIProxyElement).ProxyType = typeof(SolutionGLCodeUIProxyLogic);
                (elements[13] as UIProxyElement).ProxyType = typeof(SolutionGLConfigUIProxyLogic);
                return base.Elements;
            }
        }
    }

    //主面板

    public partial class SolutionUIProxyLogic : SolutionUIProxy
    {
        #region 服务

        public IConfigureService ConfigureService
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
                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return false;
                    else return ((SolutionList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));

                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((SolutionList)obj).ID == GlobalConstants.NewRowID;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));


                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return true;
                    else return false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));


                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((SolutionList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((SolutionList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;
            }
        }

        #endregion

        #region Method

        protected override bool AddData(object obj)
        {
            SolutionInfo newData = new SolutionInfo();

            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified); newData.IsDirty = false;
            newData.IsValid = true;
            workitem.Services.Get<IDataHoster>().RefreshData(newData);
            return true;
        }
        protected override bool DisuseData(object obj)
        {
            SolutionList currentRow = obj as SolutionList;
            if (currentRow == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                SingleResultData result = ConfigureService.ChangeSolutionState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                currentRow.IsValid = !currentRow.IsValid;
                currentRow.UpdateDate = result.UpdateDate;
                _dataHoster.RaiseCurrentChanged(currentRow);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }
        protected override bool Refresh(object obj)
        {
            List<SolutionList> list = ConfigureService.GetSolutionList(string.Empty, null, 0);
            _dataHoster.ContentPart.DataSource = list;
            return true;
        }

        #endregion

    }

    //编辑
    public class SolutionEditUIProxyLogic : SolutionEditUIProxy
    {
        #region 服务注入

        public static SolutionInfo current;
        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set
            {
                value.AddToolAction("Save", delegate(object obj)
                {
                    if (obj == null) return false;
                    else return ((SolutionInfo)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                _dataHoster = value;
            }
            get { return _dataHoster; }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        protected override bool SaveData(object data)
        {


            SolutionInfo currentData = _dataHoster.ContentPart.DataSource as SolutionInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);

            try
            {

                SingleResultData result = ConfigureService.SaveSolutionInfo(currentData.ID,
                                                                            currentData.CName,
                                                                            currentData.EName,
                                                                            currentData.InvoiceDateType,
                                                                            currentData.IsAccountShare,
                                                                            currentData.Remark,
                                                                            LocalData.UserInfo.LoginID,
                                                                            currentData.UpdateDate);
                currentData.CancelEdit();
                currentData.ID = result.ID;
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
            SolutionList list = obj as SolutionList;
            SolutionInfo info = null;

            if (list != null && list.ID != Guid.Empty)
            {
                info = ConfigureService.GetSolutionInfo(list.ID);

            }
            else
            {
                info = list as SolutionInfo;
            }
            current = info;
            _dataHoster.ContentPart.DataSource = info;
            _dataHoster.ExecuteToolAction("Save", info);
            return true;
        }
    }

    //代码规则
    public class SolutionCodeRuleUIProxyLogic : SolutionCodeRuleUIProxy
    {
        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set { _dataHoster = value; }
            get { return _dataHoster; }
        }

        #region Method

        protected override bool BeforeParentChanged(object obj)
        {
            return true;
        }

        protected override bool ParentChanged(object obj)
        {
            _dataHoster.ContentPart.DataSource = obj;
            return true;
        }

        #endregion

    }

    //币种
    public class SolutionCurrencyUIProxyLogic : SolutionCurrencyUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set { _dataHoster = value; }
            get { return _dataHoster; }
        }
        #endregion

        #region Method

        protected override bool BeforeParentChanged(object obj)
        {
            return true;
        }

        protected override bool ParentChanged(object obj)
        {
            _dataHoster.ContentPart.DataSource = obj;
            return true;
        }

        #endregion
    }

    //汇率
    public class SolutionExchangeRateUIProxyLogic : SolutionExchangeRateUIProxy
    {
        #region 服务注入
        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set { _dataHoster = value; }
            get { return _dataHoster; }
        }
        #endregion

        #region Method

        protected override bool BeforeParentChanged(object obj)
        {
            return true;
        }

        protected override bool ParentChanged(object obj)
        {
            _dataHoster.ContentPart.DataSource = obj;
            return true;
        }

        #endregion
    }

    //代理
    public partial class SolutionAgentUIProxy
    {
        public virtual BuildNewObjectHandler InitNewItem { get { return null; } }
    }
    public class SolutionAgentUIProxyLogic : SolutionAgentUIProxy
    {
        #region 服务注入

        public IConfigureService ConfigureService
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
                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return false;
                    else return ((SolutionAgentList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));

                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((SolutionList)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));



                value.AddToolAction("Disuse", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                value.AddToolAction("Save", delegate(object obj)
                {
                    return false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((SolutionAgentList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);
                _dataHoster = value;
            }
            get
            {
                return _dataHoster;
            }
        }
        #endregion
        #region Method

        SolutionList parentList = null;
        protected override bool ParentChanged(object obj)
        {
            parentList = obj as SolutionList;

            List<SolutionAgentList> list = new List<SolutionAgentList>();
            if (parentList != null && parentList.ID != Guid.Empty)
            {
                list = ConfigureService.GetSolutionAgentList(parentList.ID, null);
            }

            _dataHoster.ContentPart.DataSource = list;
            if (parentList == null || parentList.ID == Guid.Empty || !parentList.IsValid)
            {
                ((Control)(_dataHoster.ContentPart)).Enabled = false;
            }
            else
            {
                ((Control)(_dataHoster.ContentPart)).Enabled = true;
            }

            return true;
        }

        protected override bool DisuseData(object obj)
        {
            SolutionAgentList currentRow = obj as SolutionAgentList;
            if (currentRow == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = ConfigureService.ChangeSolutionAgentState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    currentRow.IsValid = !currentRow.IsValid;
                    currentRow.UpdateDate = result.UpdateDate;
                    _dataHoster.RefreshData(currentRow);
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
        protected override bool SaveData(object obj)
        {


            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (parentList == null || _dataHoster.ContentPart.DataSource is ICollection == false || ((IList)_dataHoster.ContentPart.DataSource).Count == 0) return false;

                List<SolutionAgentList> currentList = ((ICollection)_dataHoster.ContentPart.DataSource).Cast<SolutionAgentList>().ToList();

                List<Guid> ids = new List<Guid>();
                List<Guid> agentIds = new List<Guid>();
                List<string> remarks = new List<string>();
                List<DateTime?> versions = new List<DateTime?>();

                for (int i = 0; i < currentList.Count; i++)
                {
                    if (currentList[i].Validate() == false)
                    {
                        return false;
                    }

                    ids.Add(currentList[i].ID);
                    agentIds.Add(currentList[i].AgentID);
                    remarks.Add(currentList[i].Remark);
                    versions.Add(currentList[i].UpdateDate);
                }

                ManyResultData result = ConfigureService.SaveSolutionAgentInfo(parentList.ID,
                                                                                    ids.ToArray(),
                                                                                    agentIds.ToArray(),
                                                                                    remarks.ToArray(),
                                                                                    LocalData.UserInfo.LoginID,
                                                                                    versions.ToArray());


                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                }

                _dataHoster.RefreshData(currentList);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        public override BuildNewObjectHandler InitNewItem
        {
            get
            {
                return delegate(BaseDataObject newobj)
                {
                    if (parentList == null) throw new ApplicationException("InitNewItem error at SolutionAgentUIProxyLogic");

                    SolutionAgentList newData;
                    if (newobj == null)
                        newData = new SolutionAgentList();
                    else
                        newData = newobj as SolutionAgentList;

                    newData.SolutionID = parentList.ID;
                    newData.CreateByID = LocalData.UserInfo.LoginID;
                    newData.CreateByName = LocalData.UserInfo.LoginName;
                    newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    newData.IsValid = true;
                    return newData;
                };
            }
        }

        #endregion
    }

    #region 费用代码
    //分组
    public class SolutionChargingGropUIProxyLogic : SolutionChargingGropUIProxy
    {
        #region 服务注入

        public IConfigureService ConfigureService
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
                value.AddToolAction("Edit", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                value.AddToolAction("Delete", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                _dataHoster = value;
            }
            get
            {
                return _dataHoster;
            }
        }
        #endregion

        #region
        protected override bool RefreshData(object currentitem)
        {
            List<ChargingGroupList> list = ConfigureService.GetChargingGroupList(string.Empty, string.Empty, null, 0);
            _dataHoster.ContentPart.DataSource = list;
            return true;
        }
        #endregion
    }

    //费用代码
    public partial class SolutionChargingCodeUIProxy
    {
        public virtual BuildNewObjectHandler InitNewItem { get { return null; } }
    }
    public class SolutionChargingCodeUIProxyLogic : SolutionChargingCodeUIProxy
    {
        #region 服务注入
        public IConfigureService ConfigureService
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
                value.AddToolAction("Edit", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                _dataHoster = value;
            }
            get
            {
                return _dataHoster;
            }
        }
        #endregion
        SolutionList solutionList = null;
        ChargingGroupList groupList = null;

        #region Method

        protected override bool SolutionChanged(object obj)
        {
            solutionList = obj as SolutionList;
            if (solutionList == null || solutionList.ID == Guid.Empty || !solutionList.IsValid)
            {
                _dataHoster.ContentPart.DataSource = null;
                ((Control)(_dataHoster.ContentPart)).Enabled = false;
                return false;
            }
            else
            {
                ((Control)(_dataHoster.ContentPart)).Enabled = true;
            }

            if (groupList == null) return false;
            List<SolutionChargingCodeList> list = new List<SolutionChargingCodeList>();
            List<SolutionChargingCodeList> sourceList = ConfigureService.GetSolutionChargingCodeListBySolutionID(solutionList.ID, true);
            list = sourceList.FindAll(delegate(SolutionChargingCodeList item) { return item.ParentID == groupList.ID; });
            _dataHoster.ContentPart.DataSource = list;
            return true;
        }

        protected override bool GroupChanged(object obj)
        {
            groupList = obj as ChargingGroupList;
            if (groupList == null || solutionList == null) return false;
            if (solutionList.ID == Guid.Empty) return false;
            //if (groupList.NodeType == ChargeGroupNodeType.Root)
            //{
            //    ((Control)(_dataHoster.ContentPart)).Enabled = false;
            //    return false;
            //}
            //else
            //{

            ((Control)(_dataHoster.ContentPart)).Enabled = true;
            List<SolutionChargingCodeList> sourceList = ConfigureService.GetSolutionChargingCodeListBySolutionID(solutionList.ID, true);
            if (sourceList == null) return false;
            List<SolutionChargingCodeList> list = sourceList.FindAll(delegate(SolutionChargingCodeList item) { return item.ParentID == groupList.ID; });
            _dataHoster.ContentPart.DataSource = list;
            
            //}

            return true;
        }

        protected override bool SaveData(object obj)
        {


            //if (_dataHoster.ContentPart.DataSource is ICollection == false || ((IList)_dataHoster.ContentPart.DataSource).Count == 0 || solutionList == null) return false;

            List<SolutionChargingCodeList> currentList = ((ICollection)_dataHoster.ContentPart.DataSource).Cast<SolutionChargingCodeList>().ToList();
            if (currentList == null || currentList.Count == 0)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                List<Guid?> ids = new List<Guid?>();
                List<Guid> chargeCodeIDs = new List<Guid>();
                List<bool> isAgents = new List<bool>();
                List<DateTime?> updateDates = new List<DateTime?>();

                foreach (SolutionChargingCodeList item in currentList)
                {
                    if (item.Validate() == false)
                    {
                        return false;
                    }

                    ids.Add(item.ID);
                    chargeCodeIDs.Add(item.ChargingCodeID);
                    isAgents.Add(item.IsAgent);
                    updateDates.Add(item.UpdateDate);
                }

                ManyResultData result = ConfigureService.SaveSolutionChargingCodeInfo(solutionList.ID
                                                              , ids.ToArray()
                                                              , chargeCodeIDs.ToArray()
                                                              , isAgents.ToArray()
                                                              , LocalData.UserInfo.LoginID
                                                              , updateDates.ToArray());

                for (int i = 0; i < result.ChildResults.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        protected override bool DeleteData(object obj)
        {
            //  if (obj is ICollection == false || ((IList)obj).Count == 0) return false;

            //DialogResult dlg = XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //if (dlg == DialogResult.Cancel)
            //{
            //    return false;
            //}

            if (CommonUtility.EnquireIsDeleteCurrentData() == false) return false;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                SolutionChargingCodeList currentRow = obj as SolutionChargingCodeList;
                if (currentRow == null) return false;
                if (!currentRow.IsNew)
                {
                    //List<SolutionChargingCodeList> currentList = ((ICollection)obj).Cast<SolutionChargingCodeList>().ToList();

                    List<Guid> needRemoveId = new List<Guid> { currentRow.ID };
                    List<DateTime?> versions = new List<DateTime?> { currentRow.UpdateDate };

                    //foreach (var item in currentList)
                    //{
                    //    needRemoveId.Add(item.ID);
                    //    versions.Add(item.UpdateDate);
                    //}

                    ConfigureService.RemoveSolutionChargingCodeInfo(needRemoveId.ToArray()
                                                                    , LocalData.UserInfo.LoginID
                                                                    , versions.ToArray().ToArray());
                }

                _dataHoster.RemoveData(currentRow);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        public override BuildNewObjectHandler InitNewItem
        {
            get
            {
                return delegate(BaseDataObject newobj)
                {
                    if (solutionList == null) return null;

                    SolutionChargingCodeList newData;
                    if (newobj == null)
                        newData = new SolutionChargingCodeList();
                    else
                        newData = newobj as SolutionChargingCodeList;

                    newData.SolutionID = solutionList.ID;
                    newData.IsAgent = false;
                    newData.CreateByID = LocalData.UserInfo.LoginID;
                    newData.CreateByName = LocalData.UserInfo.LoginName;
                    newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    return newData;
                };
            }
        }

        #endregion
    }

    #endregion

    #region 会计科目

    //目录
    public partial class SolutionGLGroupUIProxyLogic : SolutionGLGroupUIProxy
    {
        #region 服务注入

        public IConfigureService ConfigureService
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
                value.AddToolAction("Add", delegate(object obj)
                {
                    SolutionGLGroupList group = (SolutionGLGroupList)obj;
                    return group == null || group.HierarchyCode.Length < 2;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                value.AddToolAction("Edit", delegate(object obj)
                {
                    SolutionGLGroupList group = (SolutionGLGroupList)obj;
                    return group == null || group.HierarchyCode.Length < 4;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                value.AddToolAction("Delete", delegate(object obj)
                {
                    SolutionGLGroupList group = (SolutionGLGroupList)obj;
                    return group == null || group.HierarchyCode.Split('/').Length < 4;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                _dataHoster = value;

            }
            get
            {
                return _dataHoster;
            }
        }
        #endregion
        #region Method

        protected override bool DeleteData(object obj)
        {
            SolutionGLGroupList currentRow = obj as SolutionGLGroupList;
            if (currentRow == null)
            {
                return false;
            }

            DialogResult dlg = XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg == DialogResult.Cancel)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                ConfigureService.RemoveSolutionGLGroupInfo(currentRow.ID, LocalData.UserInfo.LoginID, currentRow.UpdateDate);

                _dataHoster.RemoveData(currentRow);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        protected override bool Refresh(object obj)
        {
            List<SolutionGLGroupList> list = ConfigureService.GetSolutionGLGroupList(string.Empty, string.Empty, 0);
            _dataHoster.ContentPart.DataSource = list;
            return true;
        }

        protected override bool AddData(object obj)
        {
            SolutionGLGroupInfo newData = new SolutionGLGroupInfo();

            SolutionGLGroupList solutionGLGroupList = _dataHoster.GetCurrentItem<SolutionGLGroupList>();

            if (solutionGLGroupList != null)
            {
                newData.ParentID = solutionGLGroupList.ID;
                newData.ParentName = LocalData.IsEnglish ? solutionGLGroupList.EName : solutionGLGroupList.CName;
            }
            else
            {
                newData.ParentID = null;
                newData.ParentName = string.Empty;
            }

            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newData.IsDirty = false;
            workitem.Services.Get<IDataHoster>().OpenUI<SolutionGLGroupEditUIProxyLogic>(newData, null, true);

            return true;
        }

        protected override bool EditData(object obj)
        {
            SolutionGLGroupList currentData = obj as SolutionGLGroupList;
            if (currentData == null) return false;
            SolutionGLGroupInfo editData = ConfigureService.GetSolutionGLGroupInfo(currentData.ID);
            workitem.Services.Get<IDataHoster>().OpenUI<SolutionGLGroupEditUIProxyLogic>(editData, null, true);
            return true;
        }

        protected override bool Drag(object obj)
        {
            try
            {
                SolutionGLGroupList currentItem = obj as SolutionGLGroupList;

                ManyResultData result = ConfigureService.SetSolutionGLGroupParent(currentItem.ID,
                                                                            currentItem.ParentID,
                                                                            LocalData.UserInfo.LoginID,
                                                                            currentItem.UpdateDate);

                currentItem.UpdateDate = result.ChildResults[0].UpdateDate;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        #endregion
    }
    public class SolutionGLGroupEditUIProxyLogic : SolutionGLGroupEditUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set { _dataHoster = value; }
            get { return _dataHoster; }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        protected override bool SaveData(object data)
        {


            SolutionGLGroupInfo currentData = _dataHoster.ContentPart.DataSource as SolutionGLGroupInfo;
            if (currentData == null)
            {
                return false;
            }
            if (currentData.Validate() == false)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {

                SingleHierarchyResultData result = ConfigureService.SaveSolutionGLGroupInfo(currentData.ID,
                                                                                    currentData.ParentID,
                                                                                    currentData.Code,
                                                                                    currentData.CName,
                                                                                    currentData.EName,
                                                                                    LocalData.UserInfo.LoginID,
                                                                                    currentData.UpdateDate);

                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                currentData.HierarchyCode = result.HierarchyCode;

                _dataHoster.RefreshData(currentData);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                var findForm = form.FindForm();
                if (findForm != null) findForm.Close();

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }
    }

    //编辑会计科目
    public class SolutionGLCodeUIProxyLogic : SolutionGLCodeUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return false;
                    else return ((SolutionGLCodeList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));


                value.AddToolAction("Disuse", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                value.AddToolAction("Edit", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((SolutionGLCodeList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);
                _dataHoster = value;
            }
            get
            {
                return _dataHoster;
            }
        }
        #endregion
        #region Method

        SolutionGLGroupList glList = null;
        protected override bool BeforeGLGroupChanged(object obj)
        {
            return base.BeforeGLGroupChanged(obj);
        }
        protected override bool GLGroupChanged(object obj)
        {
            glList = obj as SolutionGLGroupList;
            if (solutionList == null) solutionList = SolutionEditUIProxyLogic.current;
            if (glList == null || solutionList == null) return false;

            GLCodeParent glCodeParent = new GLCodeParent();
            glCodeParent.SolutionID = solutionList.ID;
            glCodeParent.GLGroupID = glList.ID;
            _dataHoster.ContentPart.DataSource = glCodeParent;
            return true;
        }

        SolutionList solutionList = null;
        protected override bool BeforeSolutionChanged(object obj)
        {
            return base.BeforeSolutionChanged(obj);
        }
        protected override bool SolutionChanged(object obj)
        {
            solutionList = obj as SolutionList;
            if (glList == null || solutionList == null) return false;

            GLCodeParent glCodeParent = new GLCodeParent();
            glCodeParent.SolutionID = solutionList.ID;
            glCodeParent.GLGroupID = glList.ID;

            _dataHoster.ContentPart.DataSource = glCodeParent;

            return true;
        }

        #endregion
    }
    public class GLCodeParent
    {
        public Guid SolutionID { get; set; }
        public Guid GLGroupID { get; set; }
    }

    #endregion

    #region 会计科目配置

    public class SolutionGLConfigUIProxyLogic : SolutionGLConfigUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set { _dataHoster = value; }
            get { return _dataHoster; }
        }
        #endregion

        #region Method

        protected override bool BeforeParentChanged(object obj)
        {
            return true;
        }

        protected override bool ParentChanged(object obj)
        {
            _dataHoster.ContentPart.DataSource = obj;
            return true;
        }

        #endregion
    }

    #endregion

}
