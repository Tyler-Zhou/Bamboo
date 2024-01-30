using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.TransportFoundation.Flight
{
    public class FlightLayoutUIProxyLogic : FlightLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(FlightUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(FlightEditUIProxyLogic);
                return base.Elements;
            }
        }
    }
    public class FlightUIProxyLogic : FlightUIProxy
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
                    else return ((FlightList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    return obj==null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((FlightList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((FlightList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;
            }
        }

        #region Method

        protected override bool AddData(object currentitem)
        {
            FlightInfo newData = new FlightInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
            newData.IsValid = true;           
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            FlightList currentRow = currentitem as FlightList;

            if (currentitem == null) return false;

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = TransportFoundationService.ChangeFlightState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    currentRow.IsValid = !currentRow.IsValid;
                    currentRow.UpdateDate = result.UpdateDate;
                    _dataHoster.RefreshData(currentRow);
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
    public class FlightEditUIProxyLogic : FlightEditUIProxy
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

        void ValidateData(FlightInfo currentData, ValidateEventArgs e)
        {
            //List<ValidateDataHelper> hs = new List<ValidateDataHelper>();
            //hs.Add(new ValidateDataHelper("DOCClosingDate", currentData.DOCClosingDate));
            //hs.Add(new ValidateDataHelper("ClosingDate", currentData.ClosingDate));
            //hs.Add(new ValidateDataHelper("ETD", currentData.ETD));
            //hs.Add(new ValidateDataHelper("ETA", currentData.ETA));

            //Dictionary<string, string> errorInfo = new Dictionary<string, string>();

            //for (int i = 0; i < hs.Count - 1; i++)
            //{
            //    for (int j = i + 1; j < hs.Count; j++)
            //    {
            //        if (hs[i] == null || hs[j] == null) continue;
            //        if (errorInfo.Keys.Contains(hs[i].Name)) continue;
            //        if (hs[i].Value >= hs[j].Value)
            //        {
            //            errorInfo.Add(hs[i].Name, hs[j].Name);
            //        }
            //    }
            //}

            //Dictionary<string, string> cnInfo = new Dictionary<string, string>();
            //cnInfo.Add("DOCClosingDate", "截文件日");
            //cnInfo.Add("ClosingDate", "截关日");
            //cnInfo.Add("ETD", "估计离港日");
            //cnInfo.Add("ETA", "估计到港日");
            
            //foreach (var item in errorInfo)
            //{
            //    if (LocalData.IsEnglish)
            //        e.SetErrorInfo(item.Key, item.Key + " Can not bigger then " + item.Value);
            //    else
            //        e.SetErrorInfo(item.Key, cnInfo[item.Key] + " 不能大于 " + cnInfo[item.Value]);
            //}
        }

        protected override bool SaveData(object data)
        {
            

            FlightInfo currentData = _dataHoster.ContentPart.DataSource as FlightInfo;
            if (currentData == null) return false;
            if (currentData.Validate(delegate(ValidateEventArgs e) 
            {
                this.ValidateData(currentData, e);

                //if (currentData.POLID != Guid.Empty && currentData.POLID == currentData.PODID)
                //{
                //    e.SetErrorInfo("POD", LocalData.IsEnglish ? "POD can't same as POL" : "到达港不能和始发港相同.");
                //}
            }) == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {

                SingleResultData result = TransportFoundationService.SaveFlightInfo(currentData.ID,
                                                                   currentData.AirlineID,
                                                                   currentData.No,
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
            FlightList list = obj as FlightList;
            if (list != null && list.ID != Guid.Empty)
            {
                FlightInfo info = TransportFoundationService.GetFilghtInfo(list.ID);
                _dataHoster.ContentPart.DataSource = info;
            }
            else
            {
                _dataHoster.ContentPart.DataSource = list as FlightInfo;
            }
            return true;
        }
    }
}
