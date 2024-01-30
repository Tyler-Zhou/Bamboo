using System;
using System.Collections.Generic;
using System.Reflection;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.UI.Common.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ReportObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;

namespace ICP.FCM.OtherBusiness.UI
{

    public class ExportUIHelper : Controller
    {
        #region Services

        [ServiceDependency]
        public ITransportFoundationService TFService { get; set; }

        [ServiceDependency]
        public IConfigureService ConfigureService { get; set; }


        [ServiceDependency]
        public IUserService UserService { get; set; }

        [ServiceDependency]
        public ICustomerService CustomerService { get; set; }

        [ServiceDependency]
        public IPermissionService PermissionService { get; set; }


        #endregion

        #region 属性

        List<DataDictionaryList> _dataDictionarys = null;
        public List<DataDictionaryList> DataDictionarys
        {
            get
            {
                if (_dataDictionarys != null) return _dataDictionarys;
                else
                {
                    _dataDictionarys = TFService.GetDataDictionaryList(string.Empty, string.Empty, null, true, 0);
                    return _dataDictionarys;
                }
            }
        }

        #endregion

        #region cmbInitItem

        /// <summary>
        /// 公司 配置里的公司列表
        /// </summary>
        public List<ConfigureList> SetCmbConfigureCompany(ImageComboBoxEdit cmbEdit)
        {
            List<ConfigureList> configureList = ConfigureService.GetConfigureListByVaid(true);
            foreach (var item in configureList)
            {
                cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.CompanyName, item.CompanyID));
            }
            return configureList;
        }

        public List<OrganizationList> SetCmbUserCompanies(ImageComboBoxEdit cmbEdit)
        {
            cmbEdit.Properties.Items.Clear();
            List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            foreach (var item in userCompanyList)
            {
                cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }
            return userCompanyList;
        }

        /// <summary>
        /// 运输条款
        /// </summary>
        public List<TransportClauseList> SetCmbTransportClause(ImageComboBoxEdit cmbEdit)
        {
            cmbEdit.Properties.Items.Clear();
            List<TransportClauseList> transportClauses = TFService.GetTransportClauseList(string.Empty, string.Empty, true, 0);
            foreach (var item in transportClauses)
            {
                cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }
            return transportClauses;
        }

        #region 字典

        /// <summary>
        /// 作者: Royal
        /// 创建时间: 2011-06-11 17：23
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combox"></param>
        /// <param name="ensureFirstEmpty">是否确保下拉列表第一项为空值</param>
        /// <param name="keepFirstOriginal">当第一个枚举值的int为0时，true表示文字描述用值的描述，false表示文字描述用string.Empty代替</param>
        public void SetComboxByEnum<T>(ImageComboBoxEdit combox, bool ensureFirstEmpty, bool keepFirstOriginal)
        {
            combox.Properties.Items.Clear();
            List<EnumHelper.ListItem<T>> list = EnumHelper.GetEnumValues<T>(LocalData.IsEnglish);
            foreach (var item in list)
            {
                if (Convert.ToInt32(item.Value) == 0)
                {
                    if (keepFirstOriginal)
                    {
                        combox.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                    }
                    else if (ensureFirstEmpty)
                    {
                        combox.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, item.Value));
                    }
                    else
                    {
                    }

                    continue;
                }

                combox.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            if (ensureFirstEmpty)
            {
                if (combox.Properties.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(combox.Properties.Items[0].Description))
                    {
                        combox.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));
                    }
                }
                else
                {
                    combox.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));
                }
            }
        }

        /// <summary>
        /// 将枚举类型的值和对应语言的描述绑定到下拉列表
        /// 作者：Royal
        /// 创建时间：2011-05-25 11:17
        /// </summary>
        /// <typeparam name="T">必须是枚举类型</typeparam>
        /// <param name="combox">下拉列表控件</param>
        /// <param name="ensureFirstEmpty">是否保留第一项空白</param>
        public void SetComboxByEnum<T>(ImageComboBoxEdit combox, bool ensureFirstEmpty)
        {
            this.SetComboxByEnum<T>(combox, ensureFirstEmpty, false);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(ImageComboBoxEdit cmbEdit, DataDictionaryType type)
        {
            return SetCmbDataDictionary(cmbEdit, type, false);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(ImageComboBoxEdit cmbEdit, DataDictionaryType type, bool bingdingCode)
        {
            return SetCmbDataDictionary(new List<ImageComboBoxEdit> { cmbEdit }, type, bingdingCode, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmbEdit"></param>
        /// <param name="type"></param>
        /// <param name="bingdingCode"></param>
        /// <param name="ensureFirstEmpty">确保第一项为空</param>
        /// <returns></returns>
        public List<DataDictionaryList> SetCmbDataDictionary(ImageComboBoxEdit cmbEdit, DataDictionaryType type, bool bingdingCode,
            bool ensureFirstEmpty)
        {
            return SetCmbDataDictionary(new List<ImageComboBoxEdit> { cmbEdit }, type, bingdingCode, ensureFirstEmpty);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(List<ImageComboBoxEdit> cmbEdits, DataDictionaryType type)
        {
            return SetCmbDataDictionary(cmbEdits, type, false, false);
        }

        /// <summary>
        /// 字典
        /// 会自动避免下拉列表里面有重复的值
        /// </summary>
        /// <param name="cmbEdits"></param>
        /// <param name="type"></param>
        /// <param name="bingdingCode"></param>
        /// <param name="ensureFirstEmpty">是否强制第一项为空</param>
        /// <returns></returns>
        public List<DataDictionaryList> SetCmbDataDictionary(List<ImageComboBoxEdit> cmbEdits, DataDictionaryType type, bool bingdingCode,
            bool ensureFirstEmpty)
        {
            List<DataDictionaryList> list = DataDictionarys.FindAll(delegate(DataDictionaryList item) { return item.Type == type; });
            foreach (var cmbEdit in cmbEdits)
            {

                DevExpress.XtraEditors.Controls.ImageComboBoxItem selectedItem = cmbEdit.SelectedItem as DevExpress.XtraEditors.Controls.ImageComboBoxItem;
                cmbEdit.Properties.Items.Clear();

                foreach (var item in list)
                {
                    if (cmbEdit.Properties.Items.GetItem(item.ID) == null)
                    {
                        if (bingdingCode)
                        {
                            cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
                        }
                        else
                        {
                            cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                        }
                    }
                }

                if (selectedItem != null)
                {
                    foreach (DevExpress.XtraEditors.Controls.ImageComboBoxItem item in cmbEdit.Properties.Items)
                    {
                        if (item.Value != null && item.Value == selectedItem.Value)
                        {
                            cmbEdit.SelectedItem = item; break;
                        }

                        if (item.Description == selectedItem.Description)
                        {
                            cmbEdit.SelectedItem = item; break;
                        }
                    }
                }
            }

            if (ensureFirstEmpty)
            {
                if (list.FindAll(item => Utility.GuidIsNullOrEmpty(item.ID)).Count == 0)
                {
                    foreach (var cmbEdit in cmbEdits)
                    {
                        cmbEdit.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, Guid.Empty));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(RepositoryItemImageComboBox cmbEdit, DataDictionaryType type)
        {
            return SetCmbDataDictionary(cmbEdit, type, false);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(RepositoryItemImageComboBox cmbEdit, DataDictionaryType type, bool bingdingCode)
        {
            return SetCmbDataDictionary(new List<RepositoryItemImageComboBox> { cmbEdit }, type, bingdingCode);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(List<RepositoryItemImageComboBox> cmbEdits, DataDictionaryType type)
        {
            return SetCmbDataDictionary(cmbEdits, type, false);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(List<RepositoryItemImageComboBox> cmbEdits, DataDictionaryType type, bool bingdingCode)
        {
            List<DataDictionaryList> list = DataDictionarys.FindAll(delegate(DataDictionaryList item) { return item.Type == type; });
            foreach (var item in list)
            {
                foreach (var cmbEdit in cmbEdits)
                {
                    if (bingdingCode)
                        cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
                    else
                        cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                }
            }

            return list;
        }

        #endregion

        /// <summary>
        /// 航线
        /// </summary>
        public List<ShippingLineList> SetCmbShippingLine(ImageComboBoxEdit cmbEdit)
        {
            List<ShippingLineList> shippingLines = TFService.GetShippingLineList(string.Empty, string.Empty, true, 100);
            foreach (ShippingLineList item in shippingLines)
            {
                cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            return shippingLines;
        }

        /// <summary>
        /// 箱型
        /// </summary>
        //public List<ICP.FCM.OtherBusiness.ServiceInterface.DataObjects.ICP.Common.ServiceInterface.DataObjects.ContainerList> SetCmbContainerType( RepositoryItemImageComboBox cmbEdit)
        //{
        //    List<ICP.Common.ServiceInterface.DataObjects.ContainerList> ctntypes = TFService.GetICP.ICP.Common.ServiceInterface.DataObjects.ContainerList(string.Empty, true, 0);
        //    foreach (var item in ctntypes)
        //    {
        //        cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
        //    }
        //    return ctntypes;
        //}

        public List<ICP.Common.ServiceInterface.DataObjects.ContainerList> SetCmbContainerType(ImageComboBoxEdit combox, List<ICP.Common.ServiceInterface.DataObjects.ContainerList> list)
        {
            foreach (var item in list)
            {
                combox.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Code, item.ID));
            }
            return list;
        }

        /// <summary>
        /// 客户
        /// </summary>
        public List<CustomerList> SetCmbCustomer(ImageComboBoxEdit cmbEdit, CustomerType type)
        {
            return SetCmbCustomer(cmbEdit, type, false);
        }

        /// <summary>
        /// 客户
        /// </summary>
        public List<CustomerList> SetCmbCustomer(ImageComboBoxEdit cmbEdit, CustomerType type, bool insterEmptyCustomer)
        {
            int maxCount = 100;
            if (type == CustomerType.Carrier) maxCount = 0;

            List<CustomerList> customers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                                          string.Empty, string.Empty, null, null, null,
                                                                          type, null, null, null, null, null, maxCount);

            if (insterEmptyCustomer)
            {
                CustomerList emptyCustomer = new CustomerList();
                emptyCustomer.CName = emptyCustomer.EName = string.Empty;
                emptyCustomer.ID = Guid.Empty;
                customers.Insert(0, emptyCustomer);
            }

            foreach (CustomerList item in customers)
            {
                cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            return customers;
        }

        /// <summary>
        /// 用户
        /// </summary>
        public List<UserList> SetCmbUser(ImageComboBoxEdit cmbEdit, Guid departmentID, bool IsInsertCurrentUser)
        {
            List<UserList> users = UserService.GetUserListBySearch(departmentID, "操作", string.Empty, true, true, 0);

            if (IsInsertCurrentUser)
            {
                UserList us = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
                if (us != null) users.Remove(us);

                UserList currentUser = new UserList();
                currentUser.CName = currentUser.EName = LocalData.UserInfo.LoginName;
                currentUser.ID = LocalData.UserInfo.LoginID;
                users.Insert(0, currentUser);
            }

            foreach (var item in users)
            {
                cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem
                    (LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            return users;
        }

        /// <summary>
        /// 设置航空公司的下拉列表
        /// </summary>
        /// <param name="mcmbCarrier"></param>
        /// <returns></returns>
        public List<CustomerList> SetMCmbCarrier(ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbCarrier)
        {
            List<CustomerList> carriers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                             string.Empty, string.Empty, null, null, null,
                                                             CustomerType.Carrier, null, null, null, null, null, 0);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
            col.Add("Code", "代码");
            mcmbCarrier.InitSource<CustomerList>(carriers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            return carriers;
        }

        /// <summary>
        /// 给人员多选下拉列表按角色和部门(公司)查找并设置数据源
        /// </summary>
        /// <param name="mcmbCarrier">要设置的LWImageComboBoxEdit控件</param>
        /// <param name="IsInsertCurrentUser">是否把当前登录用户插在最前</param>
        /// <param name="organizationId">公司或部门的Guid</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="ensureFirstEmpty">确保第一行为空</param>
        /// <returns></returns>
        public List<UserList> SetUsersList(ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit mcmbCarrier, bool IsInsertCurrentUser, Guid organizationId, string roleName,
            bool ensureFirstEmpty)
        {
            List<UserList> users = UserService.GetUserListBySearch(organizationId, roleName, string.Empty, true, true, 0);
            if (IsInsertCurrentUser)
            {
                UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
                if (currentUser != null)
                {
                    users.Remove(currentUser);
                    users.Insert(0, currentUser);
                }
                else
                {
                    currentUser = new UserList();
                    currentUser = UserService.GetUserInfo(LocalData.UserInfo.LoginID);
                    users.Insert(0, currentUser);
                }
            }

            mcmbCarrier.Properties.Items.Clear();

            foreach (UserList item in users)
            {
                mcmbCarrier.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }

            //Dictionary<string, string> col = new Dictionary<string, string>();
            //col.Add("CName", "名称");
            //col.Add("Code", "代码");
            //mcmbCarrier.InitSource<UserList>(users, col, "CName", "ID");



            if (ensureFirstEmpty)
            {
                if (users.FindAll(item => Utility.GuidIsNullOrEmpty(item.ID)).Count == 0)
                {
                    mcmbCarrier.Properties.Items.Insert(0, new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, Guid.Empty));
                }
            }

            return users;
        }

        /// <summary>
        /// 填充“揽货人”下拉列表
        /// </summary>
        /// <param name="mcmbUsers"></param>
        /// <returns></returns>
        public List<ModuleUserList> SetMcmbUsers(ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox mcmbUsers, bool ensureFirstEmpty, bool IsInsertCurrentUser)
        {
            List<ModuleUserList> users = PermissionService.GetModuleUserList(CommandConstants.FCM_AE_ORDERLIST, null, 0);

            if (IsInsertCurrentUser)
            {
                ModuleUserList currentUser = users.Find(delegate(ModuleUserList item) { return item.ID == LocalData.UserInfo.LoginID; });
                if (currentUser == null)
                {
                    currentUser = new ModuleUserList();
                    //TODO:添加自己的逻辑
                    //currentUser = UserService.getus(LocalData.UserInfo.LoginID);
                    users.Insert(0, currentUser);
                }
            }

            if (ensureFirstEmpty)
            {
                users.Insert(0, new ModuleUserList());
            }

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbUsers.InitSource<ModuleUserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");

            return users;
        }

        #endregion

        #region NormalValue

        List<DataDictionaryList> normalDictionaryList = null;
        List<DataDictionaryList> NormalDictionaryList
        {
            get
            {
                if (normalDictionaryList != null) return normalDictionaryList;
                else
                {
                    normalDictionaryList = ConfigureService.GetCompanyDefaultUnitList(LocalData.UserInfo.DefaultCompanyID);
                    return normalDictionaryList;
                }
            }
        }

        public Guid GetNormalDictionaryID(DataDictionaryType type)
        {
            if (NormalDictionaryList == null) return Guid.Empty;

            DataDictionaryList tager = NormalDictionaryList.Find(delegate(DataDictionaryList item) { return item.Type == type; });
            if (tager == null) return Guid.Empty;
            else return tager.ID;
        }

        public DataDictionaryList GetNormalDictionary(DataDictionaryType type)
        {
            if (NormalDictionaryList == null) return null;

            DataDictionaryList tager = NormalDictionaryList.Find(delegate(DataDictionaryList item) { return item.Type == type; });
            if (tager == null) return tager;
            else return tager;
        }

        #endregion

        #region  Valid
        /// <summary>
        /// 判断是否需要输入相关的订舱信息
        /// </summary>
        //public  bool ValidateNeedInputBookingInfo(AirBookingInfo bookingInfo)
        //{
        //    //if (string.IsNullOrEmpty(bookingInfo.AirShippingOrderNo) == false)
        //    //    return true;
        //    if (Utility.GuidIsNullOrEmpty(bookingInfo.AgentOfCarrierID) == false)
        //        return true;

        //    if (Utility.GuidIsNullOrEmpty(bookingInfo.CarrierID) == false)
        //        return true;
        //    //if (Utility.GuidIsNullOrEmpty(bookingInfo.PreVoyageID ) == false)
        //    //    return true;
        //    //if (Utility.GuidIsNullOrEmpty(bookingInfo.VoyageID ) == false)
        //    //    return true;
        //    if (Utility.GuidIsNullOrEmpty(bookingInfo.PlaceOfDeliveryID) == false)
        //        return true;
        //    if (Utility.GuidIsNullOrEmpty(bookingInfo.ShippingLineID) == false)
        //        return true;

        //    return false;
        //}
        #endregion

        /// <summary>
        /// 设置客户的描述类
        /// </summary>
        /// <param name="customerID">客户的ID</param>
        /// <param name="customerDescription">客户的描述,不可为空</param>
        public CustomerInfo SetCustomerDesByID(Guid? customerID, CustomerDescription customerDescription)
        {
            //if (customerID.HasValue == false || customerID.Value == Guid.Empty || customerDescription == null)
            //{
            //    customerDescription.Address = customerDescription.City = customerDescription.Contact = customerDescription.Country
            //        = customerDescription.Fax = customerDescription.Name = customerDescription.Tel = string.Empty;
            //    return null;
            //}
            //else
            //{
            //    CustomerInfo info = CustomerService.GetCustomerInfo(customerID.Value);
            //    customerDescription.Address = info.EAddress ?? string.Empty;
            //    customerDescription.City = info.CityName ?? string.Empty;
            //    customerDescription.Contact = string.Empty ?? string.Empty;
            //    customerDescription.Country = info.CountryName ?? string.Empty;
            //    customerDescription.Fax = info.Fax ?? string.Empty;
            //    customerDescription.Name = info.EName ?? string.Empty;
            //    customerDescription.Tel = info.Tel1 ?? string.Empty;

            //    return info;
            //}
            return null;
        }

        /// <summary>
        /// 设置客户的描述类
        /// </summary>
        /// <param name="customerID">客户的ID</param>
        /// <param name="customerDescription">客户的描述,不可为空</param>
        public void SetCustomerDesByID(Guid? customerID, List<CustomerDescription> customerDescriptions)
        {
            if (customerID.HasValue == false || customerID.Value == Guid.Empty)
            {
                foreach (var item in customerDescriptions)
                {
                    item.Address = item.City = item.Contact = item.Country
                        = item.Fax = item.Name = item.Tel = string.Empty;
                }
            }
            else
            {
                CustomerInfo info = CustomerService.GetCustomerInfo(customerID.Value);

                foreach (var item in customerDescriptions)
                {
                    item.Address = info.EAddress ?? string.Empty;
                    item.City = info.CityName ?? string.Empty;
                    item.Contact = string.Empty ?? string.Empty;
                    item.Country = info.CountryName ?? string.Empty;
                    item.Fax = info.Fax ?? string.Empty;
                    item.Name = info.EName ?? string.Empty;
                    item.Tel = info.Tel1 ?? string.Empty;
                }
            }
        }
        public void ShowTruckEdit(Guid id, OtherBusinessList CurrentRow, WorkItem workItem, IOtherBusinessService obService, string no)
        {
            List<TruckInfo> truckList = obService.GetOBTruckServiceList(id, LocalData.IsEnglish);
            SingleResult recentData = obService.GetTruckRecentData(id, LocalData.IsEnglish);

            Dictionary<string, object> stateValues = new Dictionary<string, object>();
            stateValues.Add("Booking", CurrentRow);

            if (recentData != null)
            { 
                stateValues.Add("RecentTruckerID", recentData.GetValue<Guid?>("TruckerID"));
                stateValues.Add("RecentShipperID", recentData.GetValue<Guid?>("ShipperID"));
                stateValues.Add("ReturnLocationID", recentData.GetValue<Guid?>("ReturnLocationID"));
                stateValues.Add("ContainerDescription", SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), recentData.GetValue<string>("ContainerDescription")));
                stateValues.Add("CustomsBrokerID", recentData.GetValue<Guid?>("CustomsBrokerID"));
                stateValues.Add("IsDrivingLicence", recentData.GetValue<bool?>("IsDrivingLicence"));
                stateValues.Add("Remark", recentData.GetValue<string>("Remark"));
            }

            string title = LocalData.IsEnglish ? "Truck Service" : "拖车服务";
            PartLoader.ShowEditPart<Business.TruckEditPart>(workItem, truckList, stateValues, title + no, null,
                Business.OBCommandConstants.Command_Truck + id.ToString());
        }

        ///// <summary>
        ///// 根据装船类型设置chk控件的Checked
        ///// </summary>
        ///// <param name="voyageShowType">装船类型</param>
        ///// <param name="chkVoyage">显示大船</param>
        ///// <param name="chkPreVoyage">显示驳船</param>
        //public  void SetVoyageCheckByVoyageShowType(VoyageShowType voyageShowType, CheckEdit chkVoyage, CheckEdit chkPreVoyage)
        //{
        //    if (voyageShowType == VoyageShowType.Unknown)
        //    {
        //        chkVoyage.Checked = chkPreVoyage.Checked = false;
        //    }
        //    else if (voyageShowType == VoyageShowType.PreConfirm)
        //    {
        //        chkPreVoyage.Checked = true;
        //        chkVoyage.Checked = false;
        //    }
        //    else if (voyageShowType == VoyageShowType.Confirm)
        //    {
        //        chkPreVoyage.Checked = false;
        //        chkVoyage.Checked = true;
        //    }
        //    else if (voyageShowType == VoyageShowType.All)
        //    {
        //        chkVoyage.Checked = chkPreVoyage.Checked = true;
        //    }
        //}

        ///// <summary>
        ///// 根椐chk获得VoyageShowType
        ///// </summary>
        ///// <param name="chkVoyage"></param>
        ///// <param name="chkPreVoyage"></param>
        ///// <returns>VoyageShowType</returns>
        //public  VoyageShowType GetVoyageShowTypeByVoyageCheck(CheckEdit chkVoyage,CheckEdit chkPreVoyage)
        //{
        //    if (chkVoyage.Checked && chkPreVoyage.Checked)
        //    {
        //        return VoyageShowType.All;
        //    }
        //    else if (chkVoyage.Checked==false && chkPreVoyage.Checked==false)
        //    {
        //        return VoyageShowType.Unknown;
        //    }
        //    else if (chkVoyage.Checked == false && chkPreVoyage.Checked == true)
        //    {
        //        return VoyageShowType.PreConfirm;
        //    }
        //    else if (chkVoyage.Checked == true && chkPreVoyage.Checked == false)
        //    {
        //        return VoyageShowType.Confirm;
        //    }

        //    return VoyageShowType.All;
        //}

        //public void ShowTruckEdit(Guid id, AirBookingList CurrentRow, WorkItem workItem, IAirExportTruckService oeService,string no)
        //{
        //    List<TruckInfo> truckList = oeService.GetAirTruckServiceList(id);
        //    SingleResult recentData = oeService.GetTruckRecentData(id);

        //    Dictionary<string, object> stateValues = new Dictionary<string, object>();
        //    stateValues.Add("Booking", CurrentRow);

        //    if (recentData != null)
        //    {
        //        stateValues.Add("RecentTruckerID", recentData.GetValue<Guid?>("TruckerID"));
        //        stateValues.Add("RecentShipperID", recentData.GetValue<Guid?>("ShipperID"));
        //        stateValues.Add("ReturnLocationID", recentData.GetValue<Guid?>("ReturnLocationID"));
        //        stateValues.Add("ContainerDescription", SerializerHelper.DeserializeFromString<ContainerDescription>(typeof(ContainerDescription), recentData.GetValue<string>("ContainerDescription")));
        //        stateValues.Add("CustomsBrokerID", recentData.GetValue<Guid?>("CustomsBrokerID"));
        //        stateValues.Add("IsDrivingLicence", recentData.GetValue<bool?>("IsDrivingLicence"));
        //        stateValues.Add("Remark", recentData.GetValue<string>("Remark"));
        //    }

        //    string title = LocalData.IsEnglish ? "Truck Service" : "拖车服务";
        //    PartLoader.ShowEditPart<Business.TruckEditPart>(workItem, truckList, stateValues, title + no, null,
        //        null);
        //}
    }

    public class OBExportPrintHelper
    {
        #region Services

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IOBReportDataService OBReportSrvice { get; set; }

        [ServiceDependency]
        public IReportViewService ReportViewService { get; set; }
        /// <summary>
        /// 其他业务服务
        /// </summary>
        [ServiceDependency]
        public IOtherBusinessService OBService { get; set; }
        #endregion

        /// <summary>
        /// 获取空运出口报表路径 System.Windows.Forms.Application.StartupPath + "\\Reports\\AirExport\\
        /// </summary>
        public string GetOEReportPath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\Reports\\OtherBusiness\\";
        }

        /// <summary>
        /// 打印操作联系单
        /// </summary>
        /// <param name="orderID">orderID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOBOrder(Guid orderID)
        {
            OBOrderReportData data = OBReportSrvice.GetOBOrderReportData(orderID, LocalData.IsEnglish);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Order" : "打印操作联系单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath();
            if (LocalData.IsEnglish) fileName += "OB_OrderInfo_EN.frx";
            else fileName += "OB_OrderInfo_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);
            reportSource.Add("OrderFee", data.Fees);

            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="bookingID">bookingID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOEBookingProfit(OtherBusinessList otherBusinessList)
        {
            OtherBusinessList _otherBusinessList = otherBusinessList;
            string mblString = _otherBusinessList.Mblno;
            string hblString = _otherBusinessList.Hblno;
            string vesselVoyageString = _otherBusinessList.VesselVoyage;

            ProfitContainerObjects data = OBService.GetOtherProfitReportData(_otherBusinessList.ID);

            //账单币种
            string DefaultCurrencyName = string.Empty;
            List<ConfigureInfo> configureInfo = data.ConfigureInfo;
            if (configureInfo != null && configureInfo.Count > 0)
            {
                DefaultCurrencyName = configureInfo[0].DefaultCurrency;
            }

            ICP.FCM.OceanImport.ServiceInterface.ProfitReportData reportData = new ICP.FCM.OceanImport.ServiceInterface.ProfitReportData();
            reportData.BaseReportData = new ICP.FCM.OceanImport.ServiceInterface.ProfitBaseReportData();
            reportData.BaseReportData.PrintDate = DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportData.BaseReportData.DefaultCurrency = DefaultCurrencyName;
            reportData.BaseReportData.ReferenceNo = _otherBusinessList.NO;
            reportData.BaseReportData.MasterBLNo = mblString;
            reportData.BaseReportData.HouseBLNo = hblString;
            reportData.BaseReportData.AgentName = string.Empty;
            reportData.BaseReportData.VesselVoyageNo = vesselVoyageString;
            reportData.BaseReportData.LoadPortName = _otherBusinessList.PodName;
            reportData.BaseReportData.ETD = _otherBusinessList.Etd == null ? string.Empty : _otherBusinessList.Etd.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            reportData.BaseReportData.DiscPortName = _otherBusinessList.PodName;
            reportData.BaseReportData.ETA = _otherBusinessList.Eta == null ? string.Empty : _otherBusinessList.Eta.Value.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);

            //账单信息
            List<ICP.FCM.OceanExport.ServiceInterface.DataObjects.BillTotalInfo> billList = data.BillInfoList;
            if (billList != null && billList.Count > 0)
            {
                reportData.Fees = new List<ProfitReportFeeData>();
                decimal totalRevenue = 0m;
                decimal totalCost = 0m;
                decimal totalAgent = 0m;

                foreach (var bill in billList)
                {
                    ProfitReportFeeData feeItem = new ProfitReportFeeData();
                    feeItem.InvNo = bill.No;
                    feeItem.PostDate = bill.DueDate.ToString("MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
                    feeItem.company = bill.CustomerName;
                    feeItem.ChargeItemDescription = bill.PayAmountDescription;
                    decimal money = 0m; //保存换算后的金额

                    if (bill.Type == BillType.AR)
                    {
                        money = bill.Amount;
                        feeItem.Revenue = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                        feeItem.Cost = 0.00m.ToString("n");
                        feeItem.agent = 0.00m.ToString("n");
                        totalRevenue += bill.Way == FeeWay.AR ? money : -money;
                    }
                    else if (bill.Type == BillType.AP)
                    {
                        money = bill.Amount;
                        feeItem.Revenue = 0.00m.ToString("n");
                        feeItem.Cost = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                        feeItem.agent = 0.00m.ToString("n");
                        totalCost += bill.Way == FeeWay.AR ? money : -money;
                    }
                    else if (bill.Type == BillType.DC)
                    {
                        money = bill.Amount;
                        feeItem.Revenue = 0.00m.ToString("n");
                        feeItem.Cost = 0.00m.ToString("n");
                        feeItem.agent = bill.Way == FeeWay.AR ? money.ToString("n") : (-money).ToString("n");
                        totalAgent += bill.Way == FeeWay.AR ? money : -money;
                    }

                    reportData.Fees.Add(feeItem);
                }

                reportData.BaseReportData.TotalRevenue = totalRevenue.ToString("n");
                reportData.BaseReportData.TotalCost = totalCost.ToString("n");
                reportData.BaseReportData.TotalAgent = totalAgent.ToString("n");
                reportData.BaseReportData.Profit = (totalRevenue + totalCost + totalAgent).ToString("n");
            }

            string fileName = GetOEReportPath();
            fileName += "OB_Profit.frx";
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("ReportSource", reportData.BaseReportData);
            if (reportData.Fees != null && reportData.Fees.Count > 0)
            {
                reportSource.Add("FeeListReportSource", reportData.Fees);
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Profit Print" : "利润打印", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            viewer.BindData(fileName, reportSource, null);
            //清空
            data = null; configureInfo.Clear(); billList.Clear();
            return viewer;
        }

        /// <summary>
        /// 打印订舱确认书
        /// </summary>
        /// <param name="blID">bookingID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOEBookingConfirmation(Guid bookingID)
        {
            //BookingConfirmationReportData data = OEReportSrvice.GetBookingConfirmationReportData(bookingID);
            //if (data == null) return null;

            //IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Booking Confirmation" : "打印订舱确认书", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            //string fileName = GetOEReportPath() + "OE_BookingConfirmation.frx";
            //Dictionary<string, object> reportSource = new Dictionary<string, object>();

            //reportSource.Add("ReportSource", data);
            //viewer.BindData(fileName, reportSource, null);
            //return viewer;
            return null;
        }

        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="containerID">containerID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOELoadContainer(Guid containerID)
        {
            //ContainerPackingReportData data = OEReportSrvice.GetContainerPackingReportData(containerID);
            //if (data == null) return null;

            //IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Load Container" : "打印装箱单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            //string fileName = GetOEReportPath() + "OE_BL_LoadCtn.frx";

            //Dictionary<string, object> reportSource = new Dictionary<string, object>();

            //reportSource.Add("ReportSource", data);

            //viewer.BindData(fileName, reportSource, null);
            //return viewer;
            return null;
        }

        /// <summary>
        /// 打印装货单
        /// </summary>
        /// <param name="blID">提单ID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintOELoadGoods(Guid blID)
        {
            //ShippingOrderReportData data = OEReportSrvice.GetShippingOrderReportData(blID);
            //if (data == null) return null;

            //IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Load Goods" : "打印装货单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            //string fileName = GetOEReportPath() + "OE_LoadGoods.frx";

            //Dictionary<string, object> reportSource = new Dictionary<string, object>();

            //reportSource.Add("ReportSource", data);

            //viewer.BindData(fileName, reportSource, null);
            //return viewer;
            return null;
        }

        /// <summary>
        /// 打印派车国内报表
        /// </summary>
        /// <param name="truckID">truckID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintPickupCN(Guid truckID)
        {
            PickupCNReportData data = OBReportSrvice.GetPickupCNReportData(truckID);
            if (data == null) return null;

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Pickup" : "打印派车单", (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            string fileName = GetOEReportPath() + "OB_PickupDelivery_CN.frx";

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            reportSource.Add("ReportSource", data);

            viewer.BindData(fileName, reportSource, null);
            return viewer;
        }

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="operationID">operationID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintBusinessInfo(Guid operationID)
        {
            //OEBusinessReportData data = OEReportSrvice.GetOEBusinessReportData(operationID);
            //if (data == null) return null;

            //IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BusinessInfo" : "业务信息"
            //                                                        , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);
            //string fileName = GetOEReportPath() + (LocalData.IsEnglish ? "OE_BusinessInfo_EN.frx" : "OE_BusinessInfo_CN.frx");
            //Dictionary<string, object> reportSource = new Dictionary<string, object>();
            //reportSource.Add("OEBusinessReportData", data);
            //reportSource.Add("BLListReportData", data.blListReportDatas);
            //viewer.BindData(fileName, reportSource, null);
            //return viewer;
            return null;
        }

        /// <summary>
        /// 打印业务信息报表
        /// </summary>
        /// <param name="operationID">operationID</param>
        /// <returns>返回IReportViewer</returns>
        public IReportViewer PrintBusinessInfoCopy(Guid operationID, BLType blType)
        {
            //#region 数据源

            //BLReportData data = OEReportSrvice.GetBLReportData(operationID, blType);
            //ICP.FCM.AirExport.UI.BL.BLReportClientData blReportData = new ICP.FCM.AirExport.UI.BL.BLReportClientData();
            //Utility.CopyToValue(data, blReportData, typeof(ICP.FCM.AirExport.UI.BL.BLReportClientData));
            //if (blReportData.BLType == BLType.MAWB)
            //{
            //    blReportData.MBLNo = string.Empty;
            //}
            //else if (string.IsNullOrEmpty(blReportData.MBLNo) == false)
            //{
            //    blReportData.MBLNo = "MBLNO:" + blReportData.MBLNo;
            //}

            //if (blReportData.ETD != null)
            //{
            //    blReportData.ETDString = blReportData.ETD.Value.ToString("MMM,dd.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            //}
            //#endregion

            //IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? " BL Info" : "提单信息"
            //                                                        , (IWorkspace)Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace]);

            //string fileName = GetOEReportPath() + "OE_BL_TR_Report.frx";
            //Dictionary<string, object> reportSource = new Dictionary<string, object>();
            //reportSource.Add("ReportSource", blReportData);
            //viewer.BindData(fileName, reportSource, null);
            //return viewer;
            return null;
        }
    }
}
