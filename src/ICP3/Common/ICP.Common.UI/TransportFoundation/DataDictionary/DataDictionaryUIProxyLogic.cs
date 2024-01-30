using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;
namespace ICP.Common.UI.TransportFoundation.DataDictionary
{
    public class DataDictionaryLayoutUIProxyLogic : DataDictionaryLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(DataDictionaryUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(DataDictionaryEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class DataDictionaryUIProxyLogic : DataDictionaryUIProxy
    {
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
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
                    else return ((DataDictionaryList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    //if (obj == null) return true;
                    //else return ((DataDictionaryList)obj).ID == Guid.Empty;
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((DataDictionaryList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((DataDictionaryList)data).ID == Guid.Empty; };
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
            DataDictionaryInfo newData = new DataDictionaryInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
            newData.IsValid = true;
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            DataDictionaryList currentRow = currentitem as DataDictionaryList;
            if (currentitem == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(hoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = TransportFoundationService.ChangeDataDictionaryState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    currentRow.IsValid = !currentRow.IsValid;
                    currentRow.UpdateDate = result.UpdateDate;
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
    public class DataDictionaryEditUIProxyLogic : DataDictionaryEditUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set { _dataHoster = value; }
            get { return _dataHoster; }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        #endregion

        protected override bool SaveData(object data)
        {
            

            DataDictionaryInfo currentData = _dataHoster.ContentPart.DataSource as DataDictionaryInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }


            try
            {
                SingleResultData result = TransportFoundationService.SaveDataDictionaryInfo(currentData.ID,
                                                                   currentData.Code,
                                                                   currentData.CName,
                                                                   currentData.EName,
                                                                   currentData.Description,
                                                                   currentData.Type,
                                                                   LocalData.UserInfo.LoginID,
                                                                   currentData.UpdateDate);
               // currentData.CancelEdit();
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                //currentData.BeginEdit();
                currentData.EndEdit();
                _dataHoster.RefreshData(currentData);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(CommonUtility.GetFormByDataContent(_dataHoster.ContentPart), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(CommonUtility.GetFormByDataContent(_dataHoster.ContentPart), ex);
                return false;
            }
        }
        protected override bool BeforeParentChanged(object obj)
        {
            return CommonUtility.BeforeParentChanged(obj, _dataHoster, SaveData);
        }
        protected override bool ParentChanged(object obj)
        {
            DataDictionaryList list = obj as DataDictionaryList;
            if (list != null && list.ID != Guid.Empty)
            {
                DataDictionaryInfo info = TransportFoundationService.GetDataDictionaryInfo(list.ID);
                _dataHoster.ContentPart.DataSource = info;
            }
            else
            {
                _dataHoster.ContentPart.DataSource = list as DataDictionaryInfo;
            }
            return true;
        }
    }
}
