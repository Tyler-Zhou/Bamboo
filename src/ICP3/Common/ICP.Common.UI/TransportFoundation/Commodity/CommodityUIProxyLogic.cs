using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI.TransportFoundation.Commodity
{
    public class CommodityLayoutUIProxyLogic : CommodityLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(CommodityUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(CommodityEditUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class CommodityUIProxyLogic : CommodityUIProxy
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
                    else return ((CommodityList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return true;
                    else return false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((CommodityList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

                DataPresenceStyle s1 = new DataPresenceStyle();
                s1.Condition = delegate(object data) { return ((CommodityList)data).ID == Guid.Empty; };
                s1.Style = DataPresenceType.NewRow;
                value.AddDataPresenceStyle(s1);

                _dataHoster = value;

            }
        }

        #endregion

        #region Method

        protected override bool AddData(object currentitem)
        {
            CommodityInfo newData = new CommodityInfo();
            newData.ID = Constants.NewRowID;
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
            newData.IsDirty = false;
            newData.IsValid = true;
            _dataHoster.RefreshData(newData);
            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            CommodityList currentRow = currentitem as CommodityList;
            if (currentRow == null) return false;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentRow.IsNew == false)
                {
                    ManyHierarchyResultData result = TransportFoundationService.ChangeCommodityState(currentRow.ID, !currentRow.IsValid, LocalData.UserInfo.LoginID, currentRow.UpdateDate);
                    currentRow.UpdateDate = result.UpdateDate;
                    currentRow.HierarchyCode = result.HierarchyCode;
                    List<CommodityList> list = _dataHoster.ContentPart.DataSource as List<CommodityList>;

                    bool isValid = !currentRow.IsValid;
                    if (isValid == false)
                    {
                        List<CommodityList> listfinds = list.FindAll(delegate(CommodityList s) { return s.HierarchyCode.StartsWith(currentRow.HierarchyCode); });
                        foreach (CommodityList j in listfinds)
                        {
                            foreach (SingleResultData sr in result.ChildResults)
                            {
                                if (j.ID == sr.ID)
                                {
                                    j.UpdateDate = sr.UpdateDate;
                                }
                            }
                            j.IsValid = false;
                        }
                        _dataHoster.RefreshData(listfinds);
                        _dataHoster.RaiseCurrentChanged(listfinds[0]);
                    }
                    else
                    {
                        List<CommodityList> listfinds = list.FindAll(delegate(CommodityList s) { return currentRow.HierarchyCode.StartsWith(s.HierarchyCode); });
                        foreach (CommodityList j in listfinds)
                        {
                            foreach (SingleResultData sr in result.ChildResults)
                            {
                                if (j.ID == sr.ID)
                                {
                                    j.UpdateDate = sr.UpdateDate;
                                }
                            }
                            j.IsValid = true;
                        }
                        _dataHoster.RefreshData(listfinds);
                        _dataHoster.RaiseCurrentChanged(listfinds[0]);
                    }
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
        protected override bool DragData(object currentitem)
        {
            CommodityList currentData = currentitem as CommodityList;
            if (currentData == null)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                ManyHierarchyResultData result = TransportFoundationService.SetCommodityParent(currentData.ID, currentData.ParentID, LocalData.UserInfo.LoginID, currentData.UpdateDate);

                List<CommodityList> datas = UpdateCommodityList(_dataHoster.ContentPart.DataSource, result);
                _dataHoster.RefreshData(datas);
                _dataHoster.RaiseCurrentChanged(datas[0]);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Set Successfully" : "设置父级成功");

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        List<CommodityList> UpdateCommodityList(object datasource, ManyHierarchyResultData result)
        {
            if (datasource == null) return new List<CommodityList>();

            List<CommodityList> returnlist = new List<CommodityList>();
            List<CommodityList> sourcelist = datasource as List<CommodityList>;
            foreach (SingleHierarchyResultData sr in result.ChildResults)
            {
                CommodityList value = sourcelist.Find(delegate(CommodityList v) { return v.ID == sr.ID; });
                if (value != null)
                {
                    value.UpdateDate = sr.UpdateDate;
                    value.HierarchyCode = sr.HierarchyCode;

                    returnlist.Add(value);
                }
            }

            return returnlist;
        }

        #endregion
    }
    public class CommodityEditUIProxyLogic : CommodityEditUIProxy
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
                    else return ((CommodityInfo)obj).IsValid == false;

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
            CommodityInfo currentData = _dataHoster.ContentPart.DataSource as CommodityInfo;
            if (currentData == null) return false;
            if (currentData.IsValid == false) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }

            Control control = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                Guid? paretnID = null;
                if (currentData.ParentID.HasValue
                    && currentData.ParentID.Value != Guid.Empty)
                {
                    paretnID = currentData.ParentID;
                }

                ManyHierarchyResultData result = TransportFoundationService.SaveCommodityInfo(currentData.ID == Constants.NewRowID ? (Guid?)null : currentData.ID
                                                                 , paretnID,
                                                                   currentData.CName,
                                                                   currentData.EName,
                                                                   currentData.Remark,
                                                                   LocalData.UserInfo.LoginID,
                                                                   currentData.UpdateDate);
                List<CommodityList> changedResults = this.UpdateCommodityList(_dataHoster.GetParentDataSource<CommodityList>(), currentData, result);
                _dataHoster.RefreshData(changedResults);
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

        List<CommodityList> UpdateCommodityList(object datasource, CommodityInfo currentData, ManyHierarchyResultData result)
        {
            if (datasource == null) return new List<CommodityList>();

            List<CommodityList> returnlist = new List<CommodityList>();

            currentData.CancelEdit();
            if (currentData.IsNew)
            {
                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                currentData.HierarchyCode = result.HierarchyCode;
                returnlist.Add(currentData);
            }
            else
            {
                List<CommodityList> sourcelist = datasource as List<CommodityList>;

                foreach (SingleHierarchyResultData sr in result.ChildResults)
                {
                    if (currentData.ID == sr.ID)
                    {
                        currentData.UpdateDate = result.UpdateDate;
                        currentData.HierarchyCode = result.HierarchyCode;
                        returnlist.Add(currentData);
                    }
                    CommodityList value = sourcelist.Find(delegate(CommodityList v) { return v.ID == sr.ID; });
                    if (value != null)
                    {
                        value.UpdateDate = sr.UpdateDate;
                        value.HierarchyCode = sr.HierarchyCode;

                        returnlist.Add(value);
                    }
                }
            }
            currentData.BeginEdit();

            return returnlist;
        }
        protected override bool BeforeParentChanged(object obj)
        {
            return CommonUtility.BeforeParentChanged(obj, _dataHoster, SaveData);
        }
        protected override bool ParentChanged(object obj)
        {
            CommodityList list = obj as CommodityList;
            CommodityInfo info = null;

            if (list != null && list.ID != Constants.NewRowID)
            {
                info = TransportFoundationService.GetCommodityInfo(list.ID);
                
            }
            else
            {
                info = list as CommodityInfo;
            }

            _dataHoster.ContentPart.DataSource = info;
            _dataHoster.ExecuteToolAction("Save", info);
            return true;
        }
    }
}
