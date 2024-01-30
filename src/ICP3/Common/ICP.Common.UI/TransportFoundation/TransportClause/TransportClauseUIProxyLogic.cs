using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.TransportFoundation.TransportClause
{
    public class TransportClauseLayoutUIProxyLogic : TransportClauseLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(TransportClauseUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(TransportClauseEditUIProxyLogic);
                return base.Elements;
            }
        }
    }
    public class TransportClauseUIProxyLogic : TransportClauseUIProxy
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
                    else return ((TransportClauseList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    //if (obj == null) return true;
                    //else return ((TransportClauseList)obj).ID == Guid.Empty;
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((TransportClauseList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((TransportClauseList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;
            }
        }

        #region Method
        protected override bool AddData(object currentitem)
        {
            TransportClauseInfo newData = new TransportClauseInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsValid = true;
            newData.EndEdit();
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            TransportClauseList currentRow = currentitem as TransportClauseList;
            if (currentRow == null) return false;

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = TransportFoundationService.ChangeTransportClauseState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    currentRow.IsValid = !currentRow.IsValid;
                    currentRow.UpdateDate = result.UpdateDate;
                    _dataHoster.RaiseCurrentChanged(currentRow);
                }
                else
                {
                    _dataHoster.RemoveData(currentRow);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(control, LocalData.IsEnglish ? "Change State Successfully" : "更改状态成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(control, ex);
                return false;
            }
        }

        #endregion

    }
    public class TransportClauseEditUIProxyLogic : TransportClauseEditUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set {
                value.AddToolAction("Save", delegate(object obj)
                {
                    if (obj == null) return true;
                    else return ((TransportClauseInfo)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));


                _dataHoster = value; 
            }
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
            

            TransportClauseInfo currentData = _dataHoster.ContentPart.DataSource as TransportClauseInfo;
            if (currentData == null||currentData.Validate() == false) return false;

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                SingleResultData result = TransportFoundationService.SaveTransportClauseInfo(currentData.ID,
                                                                   currentData.OriginalCodeID,
                                                                   currentData.DestinationCodeID,
                                                                   currentData.Description,
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
            TransportClauseList list = obj as TransportClauseList;

            TransportClauseInfo info = null;
            if (list != null && list.ID != Guid.Empty)
            {
                info = TransportFoundationService.GetTransportClauseInfo(list.ID);
            }
            else
            {
               // if (list != null)list.EndEdit();

               info = list as TransportClauseInfo;
               
            }

            _dataHoster.ContentPart.DataSource = info;
            _dataHoster.ExecuteToolAction("Save", info);
            return true;
        }
    }
}
