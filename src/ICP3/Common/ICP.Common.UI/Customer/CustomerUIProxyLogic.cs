using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System.Collections;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIManagement;

namespace ICP.Common.UI
{
    public class CustomerLayoutUIProxyLogic : CustomerLayoutUIProxy
    {
        public override List<LayoutElement> Elements
        {
            get
            {
                List<LayoutElement> elements = base.Elements;
                (elements[1] as UIProxyElement).ProxyType = typeof(CustomerUIProxyLogic);
                (elements[2] as UIProxyElement).ProxyType = typeof(CustomerEditUIProxyLogic);
                (elements[3] as UIProxyElement).ProxyType = typeof(CustomerContactUIProxyLogic);
                (elements[4] as UIProxyElement).ProxyType = typeof(CustomerPartnerUIProxyLogic);
                (elements[5] as UIProxyElement).ProxyType = typeof(CustomerMemoUIProxyLogic);
                return base.Elements;
            }
        }
    }

    public class CustomerUIProxyLogic : CustomerUIProxy
    {
        #region 服务注入
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                _dataHoster = value;

                value.AddToolAction("Edit", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return true;
                    else return ((CustomerList)obj).ID == Guid.Empty;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return false;
                    else return ((CustomerList)obj).State == CustomerStateType.Invalid;
                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));

                //value.AddToolAction("Disuse", delegate(object obj)
                //{
                //    if (obj == null) return false;
                //    else return ((ICP.Framework.CommonLibrary.Common.BaseDataObject)obj).IsNew;

                //}, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "&Delete" : "删除(&D)"));

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((CustomerList)data).State == CustomerStateType.Invalid; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);

            }
        }
        #endregion
        #region Method

        protected override bool AddData(object currentitem)
        {
            CustomerInfo newData = new CustomerInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified); newData.IsDirty = false;
            _dataHoster.RefreshData(newData);
            return true;
        }

        protected override bool DisuseData(object currentitem)
        {
            CustomerList currentData = currentitem as CustomerList;
            if (currentData == null) return false;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                CustomerStateType state = CustomerStateType.Invalid;
                if (currentData.State == CustomerStateType.Valid)
                {
                    state = CustomerStateType.Invalid;
                }
                else
                {
                    state = CustomerStateType.Valid;
                }

                SingleResultData result = CustomerService.ChangeCustomerState(
                    currentData.ID,
                    state,
                    LocalData.UserInfo.LoginID,
                    "更改状态.",
                    currentData.UpdateDate);

                currentData.State = state;
                currentData.UpdateDate = result.UpdateDate;
                _dataHoster.RefreshData(currentData);
                _dataHoster.RaiseCurrentChanged(currentData);
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
    public class CustomerEditUIProxyLogic : CustomerEditUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set { _dataHoster = value; }
            get { return _dataHoster; }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #endregion

        protected override bool ParentChanged(object obj)
        {
            CustomerList list = obj as CustomerList;
            if (list != null && list.ID != Guid.Empty)
            {
                CustomerInfo info = CustomerService.GetCustomerInfo(list.ID);
                _dataHoster.ContentPart.DataSource = info;
            }
            else
            {
                _dataHoster.ContentPart.DataSource = list as CustomerInfo;
            }
            return true;
        }

        protected override bool BeforeParentChanged(object obj)
        {
            return CommonUtility.BeforeParentChanged(obj, _dataHoster, SaveData);
        }

        protected override bool SaveData(object obj)
        {

            if (this.ValidataData(obj) == false) return false;
            CustomerInfo customerInfo = obj as CustomerInfo;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                SingleResultData result = CustomerService.SaveCustomerInfo(customerInfo.ID,
                                                                            customerInfo.Code,
                                                                            customerInfo.KeyWord,
                                                                            customerInfo.CShortName,
                                                                            customerInfo.EShortName,
                                                                            customerInfo.CName,
                                                                            customerInfo.EName,
                                                                            customerInfo.CBillName,
                                                                            customerInfo.EBillName,
                                                                            customerInfo.CAddress,
                                                                            customerInfo.EAddress,
                                                                            customerInfo.CountryID,
                                                                            customerInfo.ProvinceID,
                                                                            customerInfo.CityID,
                                                                            customerInfo.EnterpriseCodeType,
                                                                            customerInfo.EnterpriseCode,
                                                                            customerInfo.PostCode,
                                                                            customerInfo.Tel1, customerInfo.Tel2,
                                                                            customerInfo.Fax,
                                                                            customerInfo.EMail,
                                                                            customerInfo.Homepage,
                                                                            customerInfo.TaxIdType,
                                                                            customerInfo.TaxIdNo,
                                                                            customerInfo.BankAccountNo,
                                                                            customerInfo.CreditLimit,
                                                                            customerInfo.Term,
                                                                            customerInfo.TradeTermID,
                                                                            customerInfo.PaymentTypeID,
                                                                            customerInfo.Type,
                                                                            customerInfo.IsAgentOfCarrier,
                                                                            customerInfo.FIRMCODE,
                                                                            customerInfo.Remark,
                                                                            LocalData.UserInfo.LoginID,
                                                                            customerInfo.IsCompany,
                                                                            customerInfo.UpdateDate);
                customerInfo.CancelEdit();
                customerInfo.ID = result.ID;
                customerInfo.UpdateDate = result.UpdateDate;
                customerInfo.BeginEdit();
                _dataHoster.RefreshData(customerInfo);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        bool ValidataData(object obj)
        {
            CustomerInfo customerInfo = obj as CustomerInfo;
            if (customerInfo == null) return false;

            if (customerInfo.Validate(delegate(ValidateEventArgs e)
            {
                //if (string.IsNullOrEmpty(customerInfo.Tel1) == false && Utility.IsTEL(customerInfo.Tel1) == false)
                //{
                //    e.SetErrorInfo("Tel1", "电话格式不正确.例:086-0755-33959323-22");
                //}

                //if (string.IsNullOrEmpty(customerInfo.Homepage) == false && Utility.IsURL(customerInfo.Homepage) == false)
                //{
                //    e.SetErrorInfo("Homepage", "网址格式不正确.例:http://www.baidu.com");
                //}

                //if (string.IsNullOrEmpty(customerInfo.EMail) == false && Utility.IsEmail(customerInfo.EMail) == false)
                //{
                //    e.SetErrorInfo("EMail", "邮箱格式不正确.例:aaa@hotmail.com");
                //}

                //if (string.IsNullOrEmpty(customerInfo.PostCode) == false && Utility.IsNumeric(customerInfo.PostCode) == false)
                //{
                //    e.SetErrorInfo("EMail", "邮编格式不正确.例:565656");
                //}

            }) == false)
            {
                return false;
            }

            return true;
        }
    }

    #region contact
    public partial class CustomerContactUIProxy
    {
        public virtual BuildNewObjectHandler InitNewItem { get { return null; } }
    }
    public partial class CustomerContactUIProxyLogic : CustomerContactUIProxy
    {
        #region 服务注入
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }


        IDataHoster _dataHoster = null;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                value.AddToolAction("Disuse", delegate(object obj)
                {
                    if (obj == null) return false;
                    else return ((CustomerContactList)obj).IsValid == false;

                }, new ToolAction(ToolAction.ToolActionType.ChangeText, LocalData.IsEnglish ? "Available(&D)" : "激活(&D)"));
                value.AddToolAction("Save", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));

                value.AddToolAction("Disuse", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((CustomerContactList)data).IsValid == false; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);
                _dataHoster = value;
            }
        }
        #endregion
        #region Method

        CustomerList parentList = null;
        protected override bool ParentChanged(object obj)
        {
            parentList = obj as CustomerList;
            if (parentList == null)
            {
                _dataHoster.ContentPart.DataSource = new List<CustomerContactList>();
                return false;
            }

            List<CustomerContactList> list = new List<CustomerContactList>();
            list = CustomerService.GetCustomerContactList(parentList.ID);
            _dataHoster.ContentPart.DataSource = list;
            _dataHoster.RefreshData(list);

            return true;
        }
        protected override bool BeforeParentChanged(object obj)
        {
            return base.BeforeParentChanged(obj);
        }

        protected override bool DisuseData(object currentitem)
        {
            CustomerContactList currentList = currentitem as CustomerContactList;
            if (currentList == null) return false;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentList.ID == Guid.Empty)
                {
                    _dataHoster.RemoveData(currentitem);

                }
                else
                {
                    bool isValid = !currentList.IsValid;

                    ManyResultData result = CustomerService.ChangeCustomerContactState(currentList.ID, isValid,
                                                                                       LocalData.UserInfo.LoginID,
                                                                                       currentList.UpdateDate);
                    currentList.IsValid = isValid;
                    currentList.UpdateDate = result.ChildResults[0].UpdateDate;

                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Successfully" : "成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        protected override bool SaveData(object currentitem)
        {


            if ((currentitem is ICollection) == false || ((ICollection)currentitem).Count == 0) return false;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                List<CustomerContactList> currentList = ((ICollection)currentitem).Cast<CustomerContactList>().ToList();
                List<Guid?> ids = new List<Guid?>();
                List<string> cNames = new List<string>();
                List<string> eNames = new List<string>();
                List<string> departments = new List<string>();
                List<string> positions = new List<string>();
                List<string> tels = new List<string>();
                List<string> faxs = new List<string>();
                List<string> mobiles = new List<string>();
                List<string> eMails = new List<string>();
                List<string> remarks = new List<string>();
                List<CustomerContactType> types = new List<CustomerContactType>();
                List<DateTime?> versions = new List<DateTime?>();

                foreach (var item in currentList)
                {
                    if (item.Validate() == false)
                    {
                        return false;
                    }
                    ids.Add(item.ID);
                    cNames.Add(item.CName);
                    eNames.Add(item.EName);
                    departments.Add(item.Department);
                    positions.Add(item.Position);
                    tels.Add(item.Tel);
                    faxs.Add(item.Fax);
                    mobiles.Add(item.Mobile);
                    eMails.Add(item.EMail);
                    remarks.Add(item.Remark);
                    types.Add(item.Type);
                    versions.Add(item.UpdateDate);
                }



                ManyResultData result = CustomerService.SaveCustomerContactInfo(currentList[0].CustomerID,
                                                                                ids.ToArray(),
                                                                                 cNames.ToArray(),
                                                                                eNames.ToArray(),
                                                                                departments.ToArray(),
                                                                                positions.ToArray(),
                                                                                tels.ToArray(),
                                                                                faxs.ToArray(),
                                                                                mobiles.ToArray(),
                                                                                eMails.ToArray(),
                                                                                remarks.ToArray(),
                                                                                types.ToArray(),
                                                                                LocalData.UserInfo.LoginID,
                                                                                versions.ToArray());

                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                }
                ((IList)currentitem).Clear();


                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        public override BuildNewObjectHandler InitNewItem
        {
            get
            {
                return delegate(BaseDataObject newobj)
                {
                    if (parentList == null) throw new ApplicationException("传入的类型错误,在" + this.ToString() + ",SetDefaultValues,parent 参数");

                    CustomerContactList newData = new CustomerContactList();
                    if (newobj == null)
                        newData = new CustomerContactList();
                    else
                        newData = newobj as CustomerContactList;
                    newData.IsValid = true;
                    newData.CustomerID = parentList.ID;
                    newData.CreateByID = LocalData.UserInfo.LoginID;
                    newData.CreateByName = LocalData.UserInfo.LoginName;
                    newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                    return newData;
                };
            }
        }

        #endregion
    }
    #endregion

    #region Partner
    public partial class CustomerPartnerUIProxy
    {
        public virtual BuildNewObjectHandler InitNewItem { get { return null; } }
    }
    public partial class CustomerPartnerUIProxyLogic : CustomerPartnerUIProxy
    {
        #region 服务注入
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }


        IDataHoster _dataHoster = null;
        [ServiceDependency]
        public IDataHoster hoster
        {
            set
            {
                _dataHoster = value;
                value.AddToolAction("Delete", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
                value.AddToolAction("Edit", delegate(object obj)
                {
                    return obj == null;

                }, new ToolAction(ToolAction.ToolActionType.ChangeEnable, "false"));
            }
        }
        #endregion
        #region Method

        CustomerList parentList = null;
        protected override bool ParentChanged(object obj)
        {
            parentList = obj as CustomerList;
            if (parentList == null)
            {
                _dataHoster.ContentPart.DataSource = new List<CustomerPartnerList>();
                return false;
            }

            List<CustomerPartnerList> list = new List<CustomerPartnerList>();
            list = CustomerService.GetCustomerPartnerList(parentList.ID);
            _dataHoster.ContentPart.DataSource = list;
            _dataHoster.RefreshData(list);

            return true;
        }
        protected override bool BeforeParentChanged(object obj)
        {
            return base.BeforeParentChanged(obj);
        }

        public override BuildNewObjectHandler InitNewItem
        {
            get
            {
                return delegate(BaseDataObject newobj)
                {
                    if (parentList == null) return null;

                    CustomerPartnerList newData = null;
                    if (newobj != null)
                    {
                        newData = newobj as CustomerPartnerList;
                    }

                    if (newData == null)
                    {
                        newData = new CustomerPartnerList();
                    }

                    newData.CustomerID = parentList.ID;
                    newData.CreateByID = LocalData.UserInfo.LoginID;
                    newData.CreateByName = LocalData.UserInfo.LoginName;
                    newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                    return newData;
                };
            }
        }

        protected override bool DeleteData(object currentitem)
        {
            CustomerPartnerList currentData = currentitem as CustomerPartnerList;
            if (currentData == null) return false;

            DialogResult dlg = XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg == DialogResult.Cancel)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentData.ID == Guid.Empty)
                {
                    _dataHoster.RemoveData(currentitem);
                }
                else
                {
                    List<Guid> ids = new List<Guid>() { currentData.ID };
                    List<DateTime?> versions = new List<DateTime?>() { currentData.UpdateDate };
                    CustomerService.RemoveCustomerPartnerInfo(ids.ToArray(),
                                                              LocalData.UserInfo.LoginID,
                                                              versions.ToArray());
                    _dataHoster.RemoveData(currentitem);
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }

        protected override bool SaveData(object currentitem)
        {


            if ((currentitem is ICollection) == false || ((ICollection)currentitem).Count == 0) return false;

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                List<CustomerPartnerList> currentList = ((ICollection)currentitem).Cast<CustomerPartnerList>().ToList();
                List<Guid?> ids = new List<Guid?>();
                List<Guid> partners = new List<Guid>();
                List<DateTime?> versions = new List<DateTime?>();

                foreach (var item in currentList)
                {
                    if (item.Validate() == false)
                    {
                        return false;
                    }
                    ids.Add(item.ID);
                    partners.Add(item.PartnerID);
                    versions.Add(item.UpdateDate);
                }

                ManyResultData result = CustomerService.SaveCustomerPartnerInfo(currentList[0].CustomerID,
                                                                                ids.ToArray(),
                                                                                partners.ToArray(),
                                                                                LocalData.UserInfo.LoginID,
                                                                                versions.ToArray());

                for (int i = 0; i < currentList.Count; i++)
                {
                    currentList[i].ID = result.ChildResults[i].ID;
                    currentList[i].UpdateDate = result.ChildResults[i].UpdateDate;
                }
                ((IList)currentitem).Clear();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
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
    #endregion

    #region Memo
    public partial class CustomerMemoUIProxyLogic : CustomerMemoUIProxy
    {
        #region 服务注入
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
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
                _dataHoster = value;
            }
            get
            {
                return _dataHoster;
            }
        }
        #endregion
        #region Method

        CustomerList parentList = null;
        protected override bool ParentChanged(object obj)
        {
            parentList = obj as CustomerList;
            if (parentList == null)
            {
                _dataHoster.ContentPart.DataSource = new List<CustomerMemoList>();
                return false;
            }

            List<CustomerMemoList> list = new List<CustomerMemoList>();
            list = CustomerService.GetCustomerMemoList(parentList.ID);
            _dataHoster.ContentPart.DataSource = list;
            _dataHoster.RefreshData(list);

            return true;
        }
        protected override bool BeforeParentChanged(object obj)
        {
            return base.BeforeParentChanged(obj);
        }

        protected override bool AddData(object currentitem)
        {
            CustomerMemoInfo newData = new CustomerMemoInfo();
            if (parentList != null)
            {
                newData.CustomerID = parentList.ID;
            }
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified); newData.IsDirty = false;
            workitem.Services.Get<IDataHoster>().OpenUI<CustomerMemoEditUIProxyLogic>(newData, null, true);

            return true;
        }

        protected override bool EditData(object currentitem)
        {
            CustomerMemoList currentData = currentitem as CustomerMemoList;
            if (currentData == null) return false;
            CustomerMemoInfo editData = CustomerService.GetCustomerMemoInfo(currentData.ID);
            workitem.Services.Get<IDataHoster>().OpenUI<CustomerMemoEditUIProxyLogic>(editData, null, true);

            return true;
        }

        protected override bool DeleteData(object currentitem)
        {
            CustomerMemoList currentData = currentitem as CustomerMemoList;
            if (currentData == null) return false;

            DialogResult dlg = XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前数据?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg == DialogResult.Cancel)
            {
                return false;
            }

            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                if (currentData.ID == Guid.Empty)
                {
                    _dataHoster.RemoveData(currentitem);
                }
                else
                {
                    if (currentData.Type == MemoType.System)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Can't delete system data" : "不能删除系统级别的数据.");
                        return false;
                    }

                    CustomerService.RemoveCustomerMemoInfo(currentData.ID,
                                                              LocalData.UserInfo.LoginID,
                                                              currentData.UpdateDate);


                    _dataHoster.RemoveData(currentitem);
                }
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
    public class CustomerMemoEditUIProxyLogic : CustomerMemoEditUIProxy
    {
        #region 服务注入

        IDataHoster _dataHoster;
        [ServiceDependency]
        public IDataHoster dataHoster
        {
            set { _dataHoster = value; }
            get { return _dataHoster; }
        }

        #endregion

        protected override bool SaveData(object data)
        {


            CustomerMemoInfo currentData = _dataHoster.ContentPart.DataSource as CustomerMemoInfo;
            if (currentData == null) return false;
            if (currentData.Validate() == false)
            {
                return false;
            }
            Control form = CommonUtility.GetFormByDataContent(_dataHoster.ContentPart);
            try
            {
                //SingleResultData result = customerService.SaveCustomerMemoInfo(currentData.ID,
                //                                                                currentData.CustomerID,
                //                                                                currentData.Subject,
                //                                                                currentData.Content,
                //                                                                currentData.Type,
                //                                                                currentData.AttachmentName,
                //                                                                currentData.Attachment,
                //                                                                LocalData.UserInfo.LoginID,
                //                                                                currentData.UpdateDate);
                //currentData.ID = result.ID;
                //currentData.MemoID = result.ID;
                //currentData.UpdateDate = result.UpdateDate;
                //_dataHoster.RefreshData(currentData);
                //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(form, LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(form, ex);
                return false;
            }
        }
    }

    #endregion

    #region DataFinders

    public class CustomerDataFinderUIProxyLogic : CustomerDataFinderUIProxy
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerFinderSearchPart); }
        }

        #region 服务注入

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
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

                DataPresenceStyle s = new DataPresenceStyle();
                s.Condition = delegate(object data) { return ((CustomerList)data).State == CustomerStateType.Invalid; };
                s.Style = DataPresenceType.Disabled;
                value.AddDataPresenceStyle(s);
            }
        }
        #endregion
        #region Method

        protected override bool AddData(object currentitem)
        {
            CustomerInfo newData = new CustomerInfo();
            newData.CreateByID = LocalData.UserInfo.LoginID;
            newData.CreateByName = LocalData.UserInfo.LoginName;
            newData.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified); newData.IsDirty = false;
            workitem.Services.Get<IDataHoster>().OpenUI<CustomerEditUIProxyLogic>(newData, null, false);

            return true;
        }

        protected override bool EditData(object currentitem)
        {
            CustomerList currentData = currentitem as CustomerList;
            if (currentData == null) return false;
            CustomerInfo editData = CustomerService.GetCustomerInfo(currentData.ID);
            workitem.Services.Get<IDataHoster>().OpenUI<CustomerEditUIProxyLogic>(editData, null, false);

            return true;
        }

        #endregion
    }

    public class CustomerAirlineFinderUIProxy : CustomerDataFinderUIProxyLogic
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerAirlineFinderSearchPart); }
        }
    }

    public class CustomerCarrierFinderUIProxy : CustomerDataFinderUIProxyLogic
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerCarrierFinderSearchPart); }
        }
    }

    public class CustomerForwardingFinderUIProxy : CustomerDataFinderUIProxyLogic
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerForwardingFinderSearchPart); }
        }
    }

    public class CustomerAgentOfCarrierFinderUIProxy : CustomerDataFinderUIProxyLogic
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerAgentOfCarrierFinderSearchPart); }
        }
    }

    public class CustomerAgentFinderUIProxy : CustomerDataFinderUIProxyLogic
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerAgentFinderSearchPart); }
        }
    }

    public class CustomerCustomsBrokerFinderUIProxy : CustomerDataFinderUIProxyLogic
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerCustomsBrokerFinderSearchPart); }
        }
    }

    public class CustomerTruckerFinderUIProxy : CustomerDataFinderUIProxyLogic
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerTruckerFinderSearchPart); }
        }
    }

    public class CustomerWarehouseFinderUIProxy : CustomerDataFinderUIProxyLogic
    {
        public override Type SearchPartType
        {
            get { return typeof(Customer.CustomerWarehouseFinderSearchPart); }
        }
    }
    #endregion
}
