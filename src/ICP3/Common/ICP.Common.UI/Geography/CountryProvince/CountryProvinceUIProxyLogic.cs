using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.Geography.CountryProvince
{
    public class CountryProvinceLayoutUIProxyLogic : CountryProvinceLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(CountryProvinceUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(CountryProvinceEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class CountryProvinceUIProxyLogic : CountryProvinceUIProxy
    {
        #region Service

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                if (value != null)
                {
                    value.AddToolAction("AddProvince", delegate(object obj)
                    {
                        return (obj == null);
                    }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Add &ProvinceData" : "新增省/州(&P)"));

                  
                    value.AddToolAction("Disuse", delegate(object obj)
                    {
                        if (obj == null) return true;
                        else return false;

                    }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                    value.AddToolAction("Disuse", delegate(object obj)
                    {
                        if (obj == null) return false;
                        else return ((CountryProvinceList)obj).IsValid == false;

                    }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));

                    //value.AddToolAction("Disuse", delegate(object obj)
                    //{
                    //    if (obj == null) return false;
                    //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                    //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));


                }
                _dataHoster = value;
                _dataHoster.BeforeDragItem +=new EventHandler<DragCancelableArgs>(_dataHoster_BeforeDragItem);

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((CountryProvinceList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((CountryProvinceList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);
            }
        }

        #endregion

        #region Method

        CountryProvinceList CurrentRow
        {
            get
            {
                if (_dataHoster != null)
                {
                    return _dataHoster.GetCurrentItem<CountryProvinceList>();
                }
                else
                {
                    return null;
                }
            }
        }

        protected override bool AddCountryData(object currentitem)
        {
            CountryProvinceInfo newData = new CountryProvinceInfo();

            newData.ID = Constants.NewRowID;
            newData.Type = CountryProvinceType.Country;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
            newData.IsValid = true;
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        protected override bool AddProvinceData(object currentitem)
        {
            if (CurrentRow == null) return false;

            CountryProvinceInfo newData = new CountryProvinceInfo();
            if (CommonUtility.GuidIsNullOrEmpty(CurrentRow.ParentID))
            {
                newData.ParentName = LocalData.IsEnglish ? CurrentRow.EName : CurrentRow.CName;
                newData.ParentID = CurrentRow.ID;
            }
            else
            {
                newData.ParentName = CurrentRow.ParentName;
                newData.ParentID = CurrentRow.ParentID;
            }

            newData.ID = Constants.NewRowID;
            newData.Type = CountryProvinceType.Province;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);newData.IsDirty = false;
            newData.IsValid = true;

            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            CountryProvinceList currentRow = currentitem as CountryProvinceList;
            if (currentRow == null) return false;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    ManyResultData result = null;

                    if (currentRow.Type == CountryProvinceType.Country)
                    {
                        result = GeographyService.ChangeCountryState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    }
                    else
                    {
                        result = GeographyService.ChangeProvinceState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    }



                    List<CountryProvinceList> datas = UpdateCountryProvinceList(_dataHoster.ContentPart.DataSource, currentRow, result, !currentRow.IsValid);
                    _dataHoster.RefreshData(datas);
                    _dataHoster.RaiseCurrentChanged(datas[0]);
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

        List<CountryProvinceList> UpdateCountryProvinceList(object datasource, CountryProvinceList currentdata, ManyResultData result, bool isValid)
        {
            if (_dataHoster.ContentPart.DataSource == null) return new List<CountryProvinceList>();


            List<CountryProvinceList> returnlist = new List<CountryProvinceList>();
            //if (currentdata.ID == result)
            //{
            //    currentdata.UpdateDate = result.UpdateDate;
            //    currentdata.IsValid = isValid;
            //    returnlist.Add(currentdata);
            //}

            List<CountryProvinceList> sourcelist = _dataHoster.ContentPart.DataSource as List<CountryProvinceList>;
            foreach (SingleResultData sr in result.ChildResults)
            {
                if (currentdata.ID == sr.ID)
                {
                    currentdata.UpdateDate = sr.UpdateDate;
                    currentdata.IsValid = isValid;
                    returnlist.Add(currentdata);
                    if (isValid)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                CountryProvinceList value = sourcelist.Find(delegate(CountryProvinceList v) { return v.ID == sr.ID; });
                if (value != null)
                {
                    value.UpdateDate = sr.UpdateDate;

                    if (isValid)
                    {
                        value.IsValid = isValid;
                    }
                    returnlist.Add(value);
                }
            }

            return returnlist;
        }

        void _dataHoster_BeforeDragItem(object sender, DragCancelableArgs e)
        {
            CountryProvinceList dragItem = e.DragItem as CountryProvinceList;
            if (dragItem.IsValid == false
                || dragItem.Type== CountryProvinceType.Country)
            {
                e.Cancel = true;
                return;
            }

            CountryProvinceList destItem = e.Dropto as CountryProvinceList;
            if (destItem != null)
            {
                if (destItem.IsValid==false 
                    || destItem.Type == CountryProvinceType.Province)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (CommonUtility.GuidIsNullOrEmpty(dragItem.ParentID) || CommonUtility.GuidIsNullOrEmpty(dragItem.ID)) e.Cancel = true;
        }

        protected override bool DragData(object currentitem)
        {

            CountryProvinceList currentItem = currentitem as CountryProvinceList;
            if (currentItem == null) return false;

            try
            {


                SingleResultData result = GeographyService.SaveProvinceInfo(currentItem.ID == Constants.NewRowID ? Guid.Empty : currentItem.ID,
                                                                  currentItem.Code,
                                                                  currentItem.CName,
                                                                  currentItem.EName,
                                                                  currentItem.ParentID.Value,
                                                                  LocalData.UserInfo.LoginID,
                                                                  currentItem.UpdateDate);
                currentItem.CancelEdit();
                currentItem.ID = result.ID;
                currentItem.UpdateDate = result.UpdateDate;
                currentItem.BeginEdit();
                _dataHoster.RefreshData(currentItem);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(_dataHoster.ContentPart.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(_dataHoster.ContentPart.FindForm(), ex);
                return false;
            }

        }

        #endregion
    }
    public class CountryProvinceEditUIProxyLogic : CountryProvinceEditUIProxy
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
                    else return ((CountryProvinceInfo)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                _dataHoster = value; 
            }
            get { return _dataHoster; }
        }

        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        #endregion

        protected override bool SaveData(object data)
        {

            

            CountryProvinceInfo currentData = _dataHoster.ContentPart.DataSource as CountryProvinceInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {

                SingleResultData result = null;

                if (currentData.Type == CountryProvinceType.Country)
                    result = GeographyService.SaveCountryInfo(currentData.ID == Constants.NewRowID ? Guid.Empty : currentData.ID,
                                                               currentData.Code,
                                                               currentData.CName,
                                                               currentData.EName,
                                                               LocalData.UserInfo.LoginID,
                                                               currentData.UpdateDate);
                else
                    result = GeographyService.SaveProvinceInfo(currentData.ID == Constants.NewRowID ? Guid.Empty : currentData.ID,
                                                               currentData.Code,
                                                               currentData.CName,
                                                               currentData.EName,
                                                               currentData.ParentID.Value,
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
            CountryProvinceList list = obj as CountryProvinceList;
            CountryProvinceInfo info=null;
            if (list != null && list.ID != Constants.NewRowID)
            {

                if (list.Type == CountryProvinceType.Country)
                {
                    info = new CountryProvinceInfo();
                    CountryInfo data = GeographyService.GetCountryInfo(list.ID);
                    info.CName = data.CName;
                    info.Code = data.Code;
                    info.CreateByID = data.CreateByID;
                    info.CreateByName = data.CreateByName;

                    info.CreateDate = data.CreateDate;
                    info.UpdateDate = data.UpdateDate;
                    info.EName = data.EName;
                    info.ID = data.ID;
                    info.IsValid = data.IsValid;
                    info.Type = CountryProvinceType.Country;

                    
                }
                else
                {
                    info = GeographyService.GetProvinceInfo(list.ID);
                }


            }
            else
            {
                info = list as CountryProvinceInfo;
            }

            _dataHoster.ContentPart.DataSource = info;
            _dataHoster.ExecuteToolAction("Save", info);
            return true;
        }
    }
}
