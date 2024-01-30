using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.Geography.Location
{
    public class LocationLayoutUIProxyLogic : LocationLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(LocationUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(LocationEditUIProxyLogic);
                (elements[2] as UIProxyElement).Height = 250;
                return base.Elements;
            }
        }
    }

    public class LocationUIProxyLogic : LocationUIProxy
    {
        #region service
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
               
                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return false;
                    else return ((LocationList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    //if (obj == null) return true ;
                    //else return false;
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((LocationList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((LocationList)data).ID == Guid.Empty; };
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

        #region

        protected override bool DisuseData(object currentitem)
        {
            LocationList currentRow = currentitem as LocationList;
            if (currentitem == null) return false;

            Control form = CommonUtility.GetFormByDataContent(hoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    SingleResultData result = null;

                    result = GeographyService.ChangeLocationState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);

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

        protected override bool AddData(object currentitem)
        {
            LocationInfo newData = new LocationInfo();
            newData.IsOcean = true;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CountryID = Guid.Empty;
            newData.CountryProvinceName = string.Empty;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsDirty = false;
            newData.IsValid = true;
            workitem.Services.Get<IDataHoster>().RefreshData(newData);

            return true;
        }

        #endregion
    }
    public class LocationEditUIProxyLogic : LocationEditUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set { _dataHoster = value; }
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
            
           
            LocationInfo currentData = _dataHoster.ContentPart.DataSource as LocationInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
               
                SingleResultData result = GeographyService.SaveLocationInfo(currentData.ID,
                                                                           currentData.Code,
                                                                           currentData.CName,
                                                                           currentData.EName,
                                                                           currentData.CountryID,
                                                                           currentData.ProvinceID,
                                                                           currentData.IsOcean,
                                                                           currentData.IsAir,
                                                                           currentData.IsOther,
                                                                           LocalData.UserInfo.LoginID,
                                                                           currentData.UpdateDate);
                currentData.CancelEdit();
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                currentData.CountryProvinceName = BuildCountryProvinceName(currentData);
                currentData.BeginEdit();
                _dataHoster.RefreshData(currentData);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(control, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex
                )
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(control, ex);
                return false;
            }
        }

        private string BuildCountryProvinceName(LocationInfo currentData)
        {
            string countryProvinceName = string.Empty;
            if (string.IsNullOrEmpty(currentData.CountryName) == false)
                countryProvinceName += currentData.CountryName;
            if (string.IsNullOrEmpty(currentData.ProvinceName) == false)
            {
                if (string.IsNullOrEmpty(countryProvinceName) == false)
                    countryProvinceName += ".";

                countryProvinceName += currentData.ProvinceName;
            }

            return countryProvinceName;
        }

        protected override bool BeforeParentChanged(object obj)
        {
            return CommonUtility.BeforeParentChanged(obj, _dataHoster, SaveData);
        }

        protected override bool ParentChanged(object obj)
        {
            LocationList list = obj as LocationList;
            if (list != null && list.ID != Guid.Empty)
            {
                LocationInfo info = GeographyService.GetLocationInfo(list.ID);
                _dataHoster.ContentPart.DataSource = info;
            }
            else
            {
                _dataHoster.ContentPart.DataSource = list;
            }
            return true;
        }
    }
}
