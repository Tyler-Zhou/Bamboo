using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.Configure.Currency
{
    public class CurrencyLayoutUIProxyLogic : CurrencyLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(CurrencyUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(CurrencyEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public partial class CurrencyUIProxyLogic : CurrencyUIProxy
    {
        #region service

        public IConfigureService ConfigureService
        {
            get {
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
                    else return ((CurrencyList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));

                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));


                value.AddToolAction("Disuse", delegate(object obj)
                {
                    //if (obj == null) return true;
                    //else return ((CurrencyList)obj).ID == Guid.Empty;
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((CurrencyList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((CurrencyList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;
            }
            get
            {
                return _dataHoster;
            }
        }

        #endregion

        #region Method

        protected override bool AddData(object obj)
        {
            CurrencyInfo newData = new CurrencyInfo();

            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified); newData.IsDirty = false;
            newData.IsValid = true;
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }
        protected override bool DisuseData(object obj)
        {
            CurrencyList currentRow = obj as CurrencyList;
            if (currentRow == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = ConfigureService.ChangeCurrencyState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
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

        #endregion
    }

    public class CurrencyEditUIProxyLogic : CurrencyEditUIProxy
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
                    else return ((CurrencyInfo)obj).IsValid == false;

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


            CurrencyInfo currentData = _dataHoster.ContentPart.DataSource as CurrencyInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {

                SingleResultData result = ConfigureService.SaveCurrencyInfo(currentData.ID,
                                                                            currentData.Code,
                                                                            currentData.CName,
                                                                            currentData.EName,
                                                                            currentData.CountryID,
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
            CurrencyList list = obj as CurrencyList;

            CurrencyInfo info = null;
            if (list != null && list.ID != Guid.Empty)
            {
                info = ConfigureService.GetCurrencyInfo(list.ID);
               // _dataHoster.ContentPart.DataSource = info;
            }
            else
            {
                info = list as CurrencyInfo;
            }

            _dataHoster.ContentPart.DataSource = info;
            _dataHoster.ExecuteToolAction("Save", info);
            return true;
        }
    }
}
