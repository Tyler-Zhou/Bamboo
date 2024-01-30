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
using ICP.Common.ServiceInterface.CompositeObjects;

namespace ICP.Common.UI.Configure.CommpanyConfigure
{
    public class CommpanyConfigureLayoutUIProxyLogic : CommpanyConfigureLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(CommpanyConfigureUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(CommpanyConfigureEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class CommpanyConfigureUIProxyLogic : CommpanyConfigureUIProxy
    {
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
                    else return ((ConfigureList)obj).IsValid == false;

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
                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((ConfigureList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);
                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((ConfigureList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;
            }
            get
            {
                return _dataHoster;
            }
        }


        #region Method

        protected override bool AddData(object currentitem)
        {
            ConfigureInfo newData = new ConfigureInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
            newData.IsValid = true;
            newData.ChargingClosingdate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date.AddMonths(1);
            newData.AccountingClosingdate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date.AddMonths(1);
            workitem.Services.Get<IDataHoster>().RefreshData(newData);
            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            ConfigureList currentRow = currentitem as ConfigureList;
            if (currentitem == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = ConfigureService.ChangeConfigureState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
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
        protected override bool Refresh(object currentitem)
        {
            List<ConfigureList> list = ConfigureService.GetConfigureListByList(null, null, null, 0);
            _dataHoster.ContentPart.DataSource = list;
            return true;
        }

        bool DisuseDataMethod(object data)
        {
            ConfigureList currentRow = data as ConfigureList;
            if (currentRow == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                SingleResultData result = ConfigureService.ChangeConfigureState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                currentRow.IsValid = !currentRow.IsValid;
                currentRow.UpdateDate = result.UpdateDate;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Set Successfully" : "设置成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        #endregion
    }

    public class CommpanyConfigureEditUIProxyLogic : CommpanyConfigureEditUIProxy
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
            

            ConfigureInfo currentData = _dataHoster.ContentPart.DataSource as ConfigureInfo;
            if (currentData == null) return false;

            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                ConfigureSaveRequest saveRequest = new ConfigureSaveRequest()
                {
                    ID = currentData.ID,
                    CompanyID = currentData.CompanyID,
                    CustomerID = currentData.CustomerID,
                    StandardCurrencyID = currentData.StandardCurrencyID,
                    DefaultCurrencyID = currentData.DefaultCurrencyID,
                    SolutionID = currentData.SolutionID,
                    IssuePlaceID = currentData.IssuePlaceID,
                    BusinessClosingDate = currentData.BusinessClosingDate,
                    ChargingClosingDate = currentData.ChargingClosingdate,
                    AccountingClosingDate = currentData.AccountingClosingdate,
                    ShortCode = currentData.ShortCode,
                    DefaultAgentDescription = currentData.DefaultAgentDescription,
                    BLTitleID = currentData.BLTitleID,
                    IsVATinvoice = currentData.IsVATinvoice,
                    VATFEEID = currentData.VATFeeID,
                    VATrateAP = currentData.VATrate,
                    CMBNetComUserID = currentData.CMBNetComUserID,
                    SaveByID = LocalData.UserInfo.LoginID,
                    UpdateDate = currentData.UpdateDate,
                };
                saveRequest.ID = currentData.ID;
                SingleResultData result = ConfigureService.SaveConfigureInfo(saveRequest);
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

        protected override bool ParentChanged(object obj)
        {
            ConfigureList list = obj as ConfigureList;
            if (list != null && list.ID != Guid.Empty)
            {
                ConfigureInfo info = ConfigureService.GetConfigureInfo(list.ID);
                _dataHoster.ContentPart.DataSource = info;
            }
            else
            {
                _dataHoster.ContentPart.DataSource = list as ConfigureInfo;
            }
            return true;
        }

        protected override bool BeforeParentChanged(object obj)
        {
            return CommonUtility.BeforeParentChanged(obj, _dataHoster, SaveData);
        }
    }

    public class ConfigureKeyValueUIProxyLogic : ConfigureKeyValueUIProxy
    {
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


        #region Method

        protected override bool AddData(object currentitem)
        {
            ConfigureKeyValueInfo newData = new ConfigureKeyValueInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
            workitem.Services.Get<IDataHoster>().OpenUI<ConfigureKeyValueEditUIProxyLogic>(newData, null, true);
            return true;
        }

        protected override bool EditData(object currentitem)
        {
            ConfigureKeyValueList currentData = currentitem as ConfigureKeyValueList;
            if (currentData == null) return false;

            ConfigureKeyValueInfo editData = ConfigureService.GetConfigureKeyValueInfo(currentData.ID);
            workitem.Services.Get<IDataHoster>().OpenUI<ConfigureKeyValueEditUIProxyLogic>(editData, null, true);
            return true;
        }

        protected override bool DeleteData(object currentitem)
        {
            ConfigureKeyValueList currentRow = currentitem as ConfigureKeyValueList;
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
                ConfigureService.RemoveConfigureKeyValueInfo(currentRow.ID, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
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

        #endregion
    }

    public class ConfigureKeyValueEditUIProxyLogic : ConfigureKeyValueEditUIProxy
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
            

            ConfigureKeyValueInfo currentData = _dataHoster.ContentPart.DataSource as ConfigureKeyValueInfo;
            if (currentData == null) return false;

            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {

                SingleResultData result = ConfigureService.SaveConfigureKeyValueInfo(currentData.ConfigureID,
                                                                                    currentData.ID,
                                                                                    currentData.ConfigureKeyID,
                                                                                    currentData.Value,
                                                                                    LocalData.UserInfo.LoginID,
                                                                                    currentData.UpdateDate);

                //currentData.ConfigureKeyName = cmbConfigureKey.Text.Trim();
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
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
    }
}
