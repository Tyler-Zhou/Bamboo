using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI
{
    #region Vessel

    public class VesselLayoutUIProxyLogic : VesselLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(VesselUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(VesselEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class VesselUIProxyLogic : VesselUIProxy
    {
        #region service

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
                    else return ((VesselList)obj).IsValid == false;

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
                s.Condition = delegate(object data) { return ((VesselList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((VesselList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;
            }
        }

        #endregion

        #region Method

        protected override bool AddData(object currentitem)
        {
            VesselInfo newData = new VesselInfo();

            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified); newData.IsDirty = false;
            newData.IsValid = true;
            newData.EndEdit();
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            VesselList currentRow = currentitem as VesselList;
            if (currentRow == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = TransportFoundationService.ChangeVesselState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
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
    public class VesselEditUIProxyLogic : VesselEditUIProxy
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
            VesselInfo currentData = _dataHoster.ContentPart.DataSource as VesselInfo;
            if (currentData == null) return false;

            if (currentData.Validate() == false)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(currentData.UNCode) && currentData.UNCode.Length != 9)
            {
                ICP.Framework.ClientComponents.Controls.Utility.ShowMessage("UNCode输入错误，UNCode长度应为9");
                return false;
            }


            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (!string.IsNullOrEmpty(currentData.UNCode))
                {
                    currentData.IMO = currentData.UNCode.Substring(2, currentData.UNCode.Length - 2);
                }
             
                SingleResultData result = TransportFoundationService.SaveVesselInfo(currentData.ID,
                                                                    currentData.Code,
                                                                    currentData.Name,
                                                                    currentData.CarrierID,
                                                                    LocalData.UserInfo.LoginID,
                                                                    currentData.UpdateDate,
                                                                    currentData.IMO,
                                                                    currentData.UNCode,
                                                                    currentData.Registration);

                currentData.CancelEdit();
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                currentData.IsDirty = false;
                //currentData.BeginEdit();
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
            VesselList list = obj as VesselList;
            if (list != null && list.ID != Guid.Empty)
            {
                VesselInfo info = TransportFoundationService.GetVesselInfo(list.ID);
                _dataHoster.ContentPart.DataSource = info;
            }
            else
            {
                _dataHoster.ContentPart.DataSource = list as VesselInfo;
            }
            return true;
        }
    }

    #endregion

    #region Voyage

    public class VoyageLayoutUIProxyLogic : VoyageLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(VoyageUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(VoyageEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class VoyageUIProxyLogic : VoyageUIProxy
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
                    else return ((VoyageList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));

                value.AddToolAction("Edit", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                value.AddToolAction("Copy", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                value.AddToolAction("Disuse", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((VoyageList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                _dataHoster = value;
            }
        }

        #region Method

        protected override bool AddData(object currentitem)
        {
            VoyageInfo newData = new VoyageInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified); newData.IsDirty = false;
            newData.IsValid = true;

            newData.EndEdit();
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        protected override bool CopyData(object currentitem)
        {
            VoyageList currentRow = currentitem as VoyageList;
            if (currentRow == null)
            {
                return false;
            }

            VoyageInfo info = TransportFoundationService.GetVoyageInfo(currentRow.ID);

            VoyageInfo newData = new VoyageInfo();
            newData.VesselID = info.VesselID;
            newData.VesselName = info.VesselName;

            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified); newData.IsDirty = false;
            newData.IsValid = true;
            // newData.ETA = newData.ETD = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.EndEdit();
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            VoyageList currentRow = currentitem as VoyageList;
            if (currentRow == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = TransportFoundationService.ChangeVoyageState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
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
    public class VoyageEditUIProxyLogic : VoyageEditUIProxy
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
                    else return ((VoyageInfo)obj).IsValid == false;

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


            VoyageInfo currentData = _dataHoster.ContentPart.DataSource as VoyageInfo;
            if (currentData == null) return false;
            if (currentData.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        this.ValidateData(currentData, e);
                    }
                ) == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                SingleResultData result = TransportFoundationService.SaveVoyageInfo(
                    currentData.ID,
                    currentData.VesselID,
                    currentData.No,
                    LocalData.UserInfo.LoginID,
                    currentData.UpdateDate);

                currentData.CancelEdit();
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                _dataHoster.RefreshData(currentData);
                currentData.BeginEdit();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(control, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(control, ex);
                return false;
            }
        }

        #region ValidateData

        void ValidateData(VoyageInfo currentData, ValidateEventArgs e)
        {
            List<ValidateDataHelper> hs = new List<ValidateDataHelper>();
            //hs.Add(new ValidateDataHelper("ClosingDate",currentData.ClosingDate));
            //hs.Add(new ValidateDataHelper("ETD", currentData.ETD));
            //hs.Add(new ValidateDataHelper("ETA",currentData.ETA));

            //Dictionary<string,string> errorInfo = new Dictionary<string,string>();

            //for (int i = 0; i < hs.Count-1; i++)
            //{
            //    for (int j = i+1; j < hs.Count; j++)
            //    {
            //        if (hs[i] == null || hs[j] == null) continue;
            //        if(errorInfo.Keys.Contains(hs[i].Name))continue;
            //        if (hs[i].Value >= hs[j].Value)
            //        {
            //            errorInfo.Add(hs[i].Name,hs[j].Name);
            //        }
            //    } 
            //}

            //Dictionary<string, string> cnInfo = new Dictionary<string, string>();
            //cnInfo.Add("ClosingDate", "截关日");
            //cnInfo.Add("ETD", "估计离港日");
            //cnInfo.Add("ETA", "估计到港日");


            //foreach (var item in errorInfo)
            //{
            //    if(LocalData.IsEnglish)
            //        e.SetErrorInfo(item.Key, item.Key + " Can not bigger then " + item.Value);
            //    else
            //        e.SetErrorInfo(item.Key, cnInfo[item.Key] + " 不能大于 " + cnInfo[item.Value]);
            //}
        }

        #endregion

        protected override bool BeforeParentChanged(object obj)
        {
            return CommonUtility.BeforeParentChanged(obj, _dataHoster, SaveData);
        }
        protected override bool ParentChanged(object obj)
        {
            VoyageList list = obj as VoyageList;
            VoyageInfo info = null;
            if (list != null && list.ID != Guid.Empty)
            {
                info = TransportFoundationService.GetVoyageInfo(list.ID);
            }
            else
            {
                info = list as VoyageInfo;
            }
            _dataHoster.ContentPart.DataSource = info;
            _dataHoster.ExecuteToolAction("Save", info);
            return true;
        }
    }

    #endregion

    public class VesselVoyageDataFinderUIProxyLogic : VesselVoyageDataFinderUIProxy
    {
        public override Type SearchPartType
        {
            get
            {
                return typeof(TransportFoundation.VesselVoyage.VoyageFinderSearchPart);
            }
        }

        #region service

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                value.AddToolAction("Edit", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
            }
        }

        #endregion

        #region Method

        protected override bool AddData(object currentitem)
        {
            VoyageInfo newData = new VoyageInfo();

            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified); newData.IsDirty = false;
            newData.IsValid = true;
            workitem.Services.Get<IDataHoster>().OpenUI<VoyageEditUIProxyLogic>(newData, null, true);

            return true;
        }

        protected override bool EditData(object currentitem)
        {
            VoyageList currentData = currentitem as VoyageList;
            if (currentitem == null) return false;

            VoyageInfo editData = TransportFoundationService.GetVoyageInfo(currentData.ID);
            workitem.Services.Get<IDataHoster>().OpenUI<VoyageEditUIProxyLogic>(editData, null, true);

            return true;
        }

        #endregion
    }
}
