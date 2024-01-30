using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.TransportFoundation.Container
{
    public class ContainerLayoutUIProxyLogic : ContainerLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(ContainerUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(ContainerEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class ContainerUIProxyLogic : ContainerUIProxy
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
                    else return ((ContainerList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    //if (obj == null) return true;
                    //else return ((ContainerList)obj).ID == Guid.Empty;
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((ContainerList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((ContainerList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;
            }
        }

        #region Method

        protected override bool AddData(object currentitem)
        {
            ContainerInfo newData = new ContainerInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
            newData.IsValid = true;
            _dataHoster.RefreshData(newData);

            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            ContainerList currentRow = currentitem as ContainerList;
            if (currentitem == null) return false;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = TransportFoundationService.ChangeContainerState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
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
    public class ContainerEditUIProxyLogic : ContainerEditUIProxy
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
            

            ContainerInfo currentData = _dataHoster.ContentPart.DataSource as ContainerInfo;
            if (currentData == null) return false;
            if (currentData.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if(currentData.TEU <0|| currentData.TEU >4)
                        {
                            e.SetErrorInfo("TEU",LocalData.IsEnglish? "TEU Can't be 0 or biger 4.":"箱量不能小于0或大于4");
                        }
                    }
                ) == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {


                SingleResultData result = TransportFoundationService.SaveContainerInfo(currentData.ID,
                                                                   currentData.Code,
                                                                   currentData.ISOCode,
                                                                   currentData.Description,
                                                                   currentData.TEU,
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
            ContainerList list = obj as ContainerList;
            if (list != null && list.ID != Guid.Empty)
            {
                ContainerInfo info = TransportFoundationService.GetContainerInfo(list.ID);
                _dataHoster.ContentPart.DataSource = info;
            }
            else
            {
                _dataHoster.ContentPart.DataSource = list as ContainerInfo;
            }
            return true;
        }
    }
}
