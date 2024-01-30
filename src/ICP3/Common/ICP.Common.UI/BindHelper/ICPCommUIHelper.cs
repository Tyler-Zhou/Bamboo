using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Helper;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Controls;

namespace ICP.Common.UI
{
    /// <summary>
    /// ICP通用UI帮助类
    /// </summary>
    public class ICPCommUIHelper : Controller, IDisposable
    {

        #region Services


        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }
        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }
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
                    _dataDictionarys = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, null, true, 0);
                    return _dataDictionarys;
                }
            }
        }

        List<LocationList> _locationlist = null;
        private List<LocationList> _LocationList
        {
            get
            {
                if (_locationlist != null) return _locationlist;
                else
                {
                    _locationlist = GeographyService.GetLocationList(string.Empty, null, null, true, null, null, true, 0);
                    return _locationlist;
                }
            }
        }

        #region 缓存数据
        /// <summary>
        /// 最近使用港口
        /// </summary>
        List<LocationList> _recentlocationlist = null;
        /// <summary>
        /// 最近使用港口
        /// </summary>
        private List<LocationList> _RecentLocationList
        {
            get
            {
                if (_recentlocationlist == null)
                    _recentlocationlist = new List<LocationList>();
                return _recentlocationlist;
            }
            set
            {
                _recentlocationlist = value;
            }
        }

        /// <summary>
        /// 最近使用船名
        /// </summary>
        List<VesselList> _recentvessellist = null;
        /// <summary>
        /// 最近使用船名
        /// </summary>
        private List<VesselList> _RecentVesselList
        {
            get
            {
                if (_recentvessellist == null)
                    _recentvessellist = new List<VesselList>();
                return _recentvessellist;
            }
            set
            {
                _recentvessellist = value;
            }
        }
        /// <summary>
        /// 费用代码列表
        /// </summary>
        List<ChargingCodeList> _chargingcodelist = null;
        /// <summary>
        /// 费用代码列表
        /// </summary>
        private List<ChargingCodeList> _ChargingCodeList
        {
            get
            {
                if (_chargingcodelist == null)
                    _chargingcodelist = new List<ChargingCodeList>();
                return _chargingcodelist;
            }
            set
            {
                _chargingcodelist = value;
            }
        }

        
        #endregion
        #endregion

        #region 绑定用户

        #region 下拉列表控件
        /// <summary>
        /// 填充用户列表(角色+职位)
        /// </summary>
        /// <param name="mcmbUsers">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="roleName">角色</param>
        /// <param name="jobName">职位名称</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<UserList> SetMcmbUsersByAll(MultiSearchCommonBox mcmbUsers, bool isNewNullData)
        {
            List<UserList> users = UserService.GetUserListByList(string.Empty, string.Empty, null, null, null, null, null, true, 0);
            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser != null)
            {
                users.Remove(currentUser);

                users.Insert(0, currentUser);
            }

            if (isNewNullData)
            {
                UserList NewData = new UserList();

                users.Insert(0, NewData);
            }

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbUsers.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");

            return users;
        }


        /// <summary>
        /// 填充用户列表(角色+职位)
        /// </summary>
        /// <param name="mcmbUsers">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="roleName">角色</param>
        /// <param name="jobName">职位名称</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<UserList> SetMcmbUsers(MultiSearchCommonBox mcmbUsers,
            Guid companyID,
            string roleName,
            string jobName,
            bool isNewNullData)
        {
            List<UserList> users = UserService.GetUserListBySearch(companyID, roleName, jobName, true, true, 0);

            users = LocalData.IsEnglish ? users.OrderBy(n => n.EName).ToList() : users.OrderBy(n => n.CName).ToList();
            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser != null)
            {
                users.Remove(currentUser);

                users.Insert(0, currentUser);
            }

            if (isNewNullData)
            {
                UserList NewData = new UserList();

                users.Insert(0, NewData);
            }

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbUsers.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            return users;
        }

        /// <summary>
        /// 填充用户列表(角色+职位) 可选是否查询有效客户
        /// </summary>
        /// <param name="mcmbUsers"></param>
        /// <param name="companyID"></param>
        /// <param name="roleName"></param>
        /// <param name="jobName"></param>
        /// <param name="isNewNullData"></param>
        /// <param name="isValid">是否查询有效客户</param>
        /// <returns></returns>
        public List<UserList> SetMcmbUsers(MultiSearchCommonBox mcmbUsers,
         Guid companyID,
         string roleName,
         string jobName,
         bool isNewNullData, bool? isValid)
        {
            List<UserList> users = UserService.GetUserListBySearch(companyID, roleName, jobName, true, isValid, 0);

            users = LocalData.IsEnglish ? users.OrderBy(n => n.EName).ToList() : users.OrderBy(n => n.CName).ToList();
            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser != null)
            {
                users.Remove(currentUser);

                users.Insert(0, currentUser);
            }

            if (isNewNullData)
            {
                UserList NewData = new UserList();

                users.Insert(0, NewData);
            }

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbUsers.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            return users;
        }

        /// <summary>
        /// 绑定当前用户所有部门的公司员工列表
        /// <param name="mcmbUsers"></param>
        /// <returns></returns>
        public List<UserList> SetMcmbUsersByCompanys(MultiSearchCommonBox mcmbUsers)
        {
            List<Guid> depIds = (from d in LocalData.UserInfo.UserOrganizationList where d.Type == LocalOrganizationType.Company select d.ID).ToList();

            List<UserList> userList = UserService.GetUnderlingUserList(depIds.ToArray(), null, null, true);

            UserList insertItem = new UserList();
            insertItem.ID = Guid.Empty;
            insertItem.EName = insertItem.CName = string.Empty;
            userList.Insert(0, insertItem);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");


            mcmbUsers.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");

            return userList;
        }

        /// <summary>
        /// 国家列表
        /// <param name="mcmbUsers">MultiSearchCommonBox</param>
        /// <returns></returns>
        public List<CountryList> SetMcmbCountry(MultiSearchCommonBox mcmbUsers)
        {
            List<Guid> depIds = (from d in LocalData.UserInfo.UserOrganizationList where d.Type == LocalOrganizationType.Company select d.ID).ToList();

            List<CountryList> countrys = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

            CountryList insertItem = new CountryList();
            insertItem.ID = Guid.Empty;
            insertItem.EName = insertItem.CName = string.Empty;
            countrys.Insert(0, insertItem);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");


            mcmbUsers.InitSource<CountryList>(countrys, col, LocalData.IsEnglish ? "EName" : "CName", "ID");

            return countrys;
        }


        /// <summary>
        /// 填充用户列表(角色)
        /// </summary>
        /// <param name="mcmbUsers">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="roleName">角色</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<UserList> SetMcmbUsersByRole(
            MultiSearchCommonBox mcmbUsers,
            Guid companyID,
            string roleName,
            bool isNewNullData)
        {
            return SetMcmbUsers(mcmbUsers, companyID, roleName, string.Empty, isNewNullData);
        }

        /// <summary>
        /// 填充用户列表(职位)
        /// </summary>
        /// <param name="mcmbUsers">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="jobName">职位</param>
        /// <returns></returns>
        public List<UserList> SetMcmbUsersByJob(
            MultiSearchCommonBox mcmbUsers,
            Guid companyID,
            string jobName,
            bool isNewNullData)
        {
            return SetMcmbUsers(mcmbUsers, companyID, string.Empty, jobName, isNewNullData);
        }

        /// <summary>
        /// 填充用户列表,根据命令行
        /// </summary>
        /// <param name="mcmbUsers"></param>
        /// <returns></returns>
        public List<ModuleUserList> SetMcmbUsersByCommand(
            MultiSearchCommonBox mcmbUsers,
            string commandConstantName,
            bool ensureFirstEmpty,
            bool IsInsertCurrentUser)
        {
            List<ModuleUserList> users = PermissionService.GetModuleUserList(commandConstantName, null, 0);

            if (IsInsertCurrentUser)
            {
                ModuleUserList currentUser = users.Find(delegate(ModuleUserList item) { return item.ID == LocalData.UserInfo.LoginID; });
                if (currentUser == null)
                {
                    currentUser = new ModuleUserList();

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

        #region 下拉框控件
        /// <summary>
        /// 填充用户列表(角色+职位)
        /// </summary>
        /// <param name="cmbUser">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="roleName">角色</param>
        /// <param name="jobName">职位名称</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<UserList> SetComboxUsers(LWImageComboBoxEdit cmbUser,
            Guid companyID,
            string roleName,
            string jobName,
            bool isNewNullData)
        {
            List<UserList> users = UserService.GetUserListBySearch(companyID, roleName, jobName, true, true, 0);
            users = LocalData.IsEnglish ? users.OrderBy(n => n.EName).ToList() : users.OrderBy(n => n.CName).ToList();
            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser != null)
            {
                users.Remove(currentUser);

                users.Insert(0, currentUser);
            }

            cmbUser.Properties.BeginUpdate();
            cmbUser.Properties.Items.Clear();

            foreach (UserList item in users)
            {
                cmbUser.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }

            if (isNewNullData)
            {
                cmbUser.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbUser.Properties.EndUpdate();

            return users;
        }

        /// <summary>
        /// 填充用户列表(角色+职位) 公司为列表
        /// </summary>
        /// <param name="cmbUser"></param>
        /// <param name="companyID"></param>
        /// <param name="roleName"></param>
        /// <param name="jobName"></param>
        /// <param name="isNewNullData"></param>
        /// <returns></returns>
        public List<UserList> SetComboxUsers(LWImageComboBoxEdit cmbUser,
        List<Guid> companyID,
        string roleName,
        string jobName,
        bool isNewNullData)
        {
            List<UserList> users = new List<UserList>();

            foreach (Guid item in companyID)
            {
                users.AddRange(UserService.GetUserListBySearch(item, roleName, jobName, true, true, 0));
            }

            users = LocalData.IsEnglish ? users.OrderBy(n => n.EName).ToList() : users.OrderBy(n => n.CName).ToList();
            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser != null)
            {
                users.Remove(currentUser);

                users.Insert(0, currentUser);
            }

            cmbUser.Properties.BeginUpdate();
            cmbUser.Properties.Items.Clear();

            foreach (UserList item in users)
            {
                cmbUser.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }

            if (isNewNullData)
            {
                cmbUser.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbUser.Properties.EndUpdate();

            return users;
        }


        /// <summary>
        /// 填充用户列表(角色)
        /// </summary>
        /// <param name="cmbUser">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="roleName">角色</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<UserList> SetComboxUsersByRole(
            LWImageComboBoxEdit cmbUser,
            Guid companyID,
            string roleName,
            bool isNewNullData)
        {
            return SetComboxUsers(cmbUser, companyID, roleName, string.Empty, isNewNullData);
        }


        /// <summary>
        ///  填充用户列表(角色- 公司为列表)
        /// </summary>
        /// <param name="cmbUser"></param>
        /// <param name="companyIDs"></param>
        /// <param name="roleName"></param>
        /// <param name="isNewNullData"></param>
        /// <returns></returns>
        public List<UserList> SetComboxUsersByRole(
           LWImageComboBoxEdit cmbUser,
           List<Guid> companyIDs,
           string roleName,
           bool isNewNullData)
        {
            return SetComboxUsers(cmbUser, companyIDs, roleName, string.Empty, isNewNullData);
        }

        /// <summary>
        /// 填充用户列表(By角色列表)
        /// </summary>
        /// <param name="cmbUser">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="roleList">角色列表</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<UserList> SetComboxUsersByRoles(
            LWImageComboBoxEdit cmbUser,
            Guid companyID,
            string[] roleList,
            bool isNewNullData)
        {
            Guid[] companyIDs = new Guid[1];
            companyIDs[0] = companyID;
            List<UserList> users = UserService.GetOrganizationJobNameUserList(companyIDs, null, roleList, true);

            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser != null)
            {
                users.Remove(currentUser);

                users.Insert(0, currentUser);
            }

            cmbUser.Properties.BeginUpdate();
            cmbUser.Properties.Items.Clear();
            foreach (UserList item in users)
            {
                cmbUser.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }

            if (isNewNullData)
            {
                cmbUser.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbUser.Properties.EndUpdate();
            return users;
        }

        /// <summary>
        /// 填充用户列表(职位)
        /// </summary>
        /// <param name="cmbUser">控件</param>
        /// <param name="companyID">公司ID</param>/param>
        /// <param name="jobName">职位</param>
        /// <param name="isNewNullData">是否添加空行</param>
        /// <returns></returns>
        public List<UserList> SetComboxUsersByJob(
            LWImageComboBoxEdit cmbUser,
            Guid companyID,
            string jobName,
            bool isNewNullData)
        {
            return SetComboxUsers(cmbUser, companyID, string.Empty, jobName, isNewNullData);
        }

        /// <summary>
        /// 填充用户列表,根据命令行
        /// </summary>
        /// <param name="cmbUser">控件名</param>
        /// <param name="commandConstantName"></param>
        /// <param name="isNewNullData">是否添加空行数据</param>
        /// <returns></returns>
        public List<ModuleUserList> SetComboxUsers(
            LWImageComboBoxEdit cmbUser,
            string commandConstantName,
            bool isNewNullData)
        {
            List<ModuleUserList> users = PermissionService.GetModuleUserList(commandConstantName, null, 0);

            cmbUser.Properties.BeginUpdate();

            foreach (ModuleUserList item in users)
            {
                cmbUser.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }

            if (isNewNullData)
            {
                cmbUser.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbUser.Properties.EndUpdate();

            return users;
        }

        #endregion

        #endregion

        #region 绑定职位


        #endregion

        #region 绑定组织结构

        #region 绑定公司

        /// <summary>
        /// 绑定当前用户所属的公司列表(下拉框)
        /// </summary>
        /// <param name="cmbCompany">公司下拉框</param>
        /// <param name="isNewNullData">是否添加一个空行</param>
        public List<LocalOrganizationInfo> BindCompanyByUser(LWImageComboBoxEdit cmbCompany,
                    bool isNewNullData)
        {
            cmbCompany.Properties.BeginUpdate();

            cmbCompany.Properties.Items.Clear();

            List<LocalOrganizationInfo> list = (from d in LocalData.UserInfo.UserOrganizationList where d.Type == LocalOrganizationType.Company select d).ToList();

            foreach (LocalOrganizationInfo item in list)
            {
                cmbCompany.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }
            if (isNewNullData)
            {
                cmbCompany.Properties.Items.Insert(0, new ImageComboBoxItem(LocalData.IsEnglish ? "Total" : "全部", null));
            }

            cmbCompany.Properties.EndUpdate();
            cmbCompany.EditValue = LocalData.UserInfo.DefaultCompanyID;
            return list;
        }

        /// <summary>
        /// 绑定所有的公司列表(下拉框)
        /// </summary>
        /// <param name="cmbCompany">控件</param>
        /// <param name="isNewNullData">是否添加空行数据</param>
        public void BindCompanyByAll(LWImageComboBoxEdit cmbCompany,
                    bool isNewNullData)
        {
            cmbCompany.Properties.BeginUpdate();

            cmbCompany.Properties.Items.Clear();

            List<OrganizationList> list = OrganizationService.GetOfficeList();

            foreach (OrganizationList item in list)
            {
                cmbCompany.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }
            if (isNewNullData)
            {
                cmbCompany.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbCompany.Properties.EndUpdate();
        }

        /// <summary>
        /// 绑定当前用户所属的公司列表(下拉勾选框)
        /// </summary>
        /// <param name="checkCombox"></param>
        public void BindCompanyByUser(CheckedComboBoxEdit checkComboxCompany, CheckState heckedstate)
        {
            List<LocalOrganizationInfo> list = (from d in LocalData.UserInfo.UserOrganizationList where d.Type == LocalOrganizationType.Company select d).ToList();

            checkComboxCompany.Properties.BeginUpdate();

            checkComboxCompany.Properties.Items.Clear();
            foreach (var item in list)
            {
                checkComboxCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   heckedstate, true);
            }

            checkComboxCompany.Properties.EndUpdate();
        }

        /// <summary>
        /// 绑定当前用户所属的部门列表(下拉村勾选框)，选上默认部门
        /// </summary>
        /// <param name="checkCombox"></param>
        public void BindCompanyByUser(TreeCheckBox treeBoxSalesDep, CheckState heckedstate)
        {
            List<UserOrganizationTreeList> userOrganizationTrees = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);
            treeBoxSalesDep.SetSource<UserOrganizationTreeList>(userOrganizationTrees, LocalData.IsEnglish ? "EShortName" : "CShortName", "HasPermission");
            UserOrganizationTreeList orginazation = userOrganizationTrees.Find(o => o.IsDefault);
            if (orginazation != null)
            {
                List<Guid> ids = new List<Guid>();
                ids.Add(orginazation.ID);
                treeBoxSalesDep.EditValue = ids;
            }
        }

        #endregion

        #region 绑定部门
        /// <summary>
        /// 绑定当前用户所属的部门列表(下拉框)
        /// </summary>
        /// <param name="cmbCompany">公司下拉框</param>
        /// <param name="isNewNullData">是否添加一个空行</param>
        public void BindDepartmentByUser(LWImageComboBoxEdit cmbDepartment,
                    bool isNewNullData)
        {
            cmbDepartment.Properties.BeginUpdate();

            cmbDepartment.Properties.Items.Clear();

            List<LocalOrganizationInfo> list = (from d in LocalData.UserInfo.UserOrganizationList where d.Type == LocalOrganizationType.Department select d).ToList();

            foreach (LocalOrganizationInfo item in list)
            {
                cmbDepartment.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }
            if (isNewNullData)
            {
                cmbDepartment.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbDepartment.Properties.EndUpdate();
        }


        /// <summary>
        /// 绑定当前用户所属的部门列表(勾选树)
        /// </summary>
        /// <param name="checkTree"></param>
        public void BindDepartmentByUser(TreeCheckBox checkTreeDepartment)
        {
            List<UserOrganizationTreeList> userOrganizationTrees = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);
            checkTreeDepartment.SetSource<UserOrganizationTreeList>(userOrganizationTrees, LocalData.IsEnglish ? "EShortName" : "CShortName", "HasPermission");
            checkTreeDepartment.SelectAll();
        }
        /// <summary>
        /// 绑定当前用户所属的部门列表(勾选树)
        /// </summary>
        /// <param name="checkTree"></param>
        public void BindDepartmentByAll(TreeCheckBox checkTreeDepartment)
        {
            List<UserOrganizationTreeList> userOrganizationTrees = new List<UserOrganizationTreeList>();
            List<OrganizationList> orgList = OrganizationService.GetOrganizationList(string.Empty, string.Empty, true, 0);

            foreach (OrganizationList item in orgList)
            {
                UserOrganizationTreeList userOrg = new UserOrganizationTreeList();

                userOrg.Code = item.Code;
                userOrg.CShortName = item.CShortName;
                userOrg.EShortName = item.EShortName;
                userOrg.FullName = item.FullName;
                userOrg.HasPermission = true;
                userOrg.HierarchyCode = item.HierarchyCode;
                userOrg.ID = item.ID;
                userOrg.IsDefault = item.IsDefault;
                userOrg.ParentID = item.ParentID;

                userOrganizationTrees.Add(userOrg);

            }

            checkTreeDepartment.SetSource<UserOrganizationTreeList>(userOrganizationTrees, LocalData.IsEnglish ? "EShortName" : "CShortName", "HasPermission");
            checkTreeDepartment.SelectAll();
        }

        /// <summary>
        /// 绑定当前用户所属的公司列表(下拉勾选框)
        /// </summary>
        /// <param name="checkCombox"></param>
        public void BindDepartmentByUser(CheckedComboBoxEdit checkComboxCompany, CheckState isChecked)
        {
            List<LocalOrganizationInfo> list = (from d in LocalData.UserInfo.UserOrganizationList where d.Type == LocalOrganizationType.Department select d).ToList();

            checkComboxCompany.Properties.BeginUpdate();

            checkComboxCompany.Properties.Items.Clear();
            foreach (var item in list)
            {
                checkComboxCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   isChecked, true);
            }

            checkComboxCompany.Properties.EndUpdate();
        }

        #endregion

        #endregion

        #region 绑定运输条款
        /// <summary>
        /// 运输条款
        /// </summary>
        public List<TransportClauseList> SetCmbTransportClause(LWImageComboBoxEdit cmbEdit)
        {
            cmbEdit.Properties.BeginUpdate();
            cmbEdit.Properties.Items.Clear();
            List<TransportClauseList> transportClauses = TransportFoundationService.GetTransportClauseList(string.Empty, string.Empty, true, 0);
            foreach (var item in transportClauses)
            {
                cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            cmbEdit.Properties.EndUpdate();

            return transportClauses;
        }

        #endregion

        #region 枚举
        /// <summary>
        /// 将枚举类型的值和对应语言的描述绑定到下拉列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="combox">控件</param>
        /// <param name="isNewNullData">第一项为空</param>
        /// <param name="keepFirstOriginal">当第一个枚举值的int为0时，true表示文字描述用值的描述，false表示文字描述用string.Empty代替</param>
        /// <param name="skipUnknown">未知类型跳过</param>
        public void SetComboxByEnum<T>(LWImageComboBoxEdit combox, bool isNewNullData, bool keepFirstOriginal, bool skipUnknown=false)
        {
            combox.Properties.BeginUpdate();
            combox.Properties.Items.Clear();
            List<EnumHelper.ListItem<T>> list = EnumHelper.GetEnumValues<T>(LocalData.IsEnglish);
            foreach (var item in list)
            {
                if (Convert.ToInt32(item.Value) == 0)
                {
                    if (skipUnknown)
                        continue;
                    if (keepFirstOriginal)
                    {
                        combox.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                    }
                    else if (isNewNullData)
                    {
                        combox.Properties.Items.Insert(0, new ImageComboBoxItem(string.Empty, item.Value));
                    }
                    continue;
                }

                combox.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            if (isNewNullData)
            {
                if (combox.Properties.Items.Count > 0)
                {
                    if (!string.IsNullOrEmpty(combox.Properties.Items[0].Description))
                    {
                        combox.Properties.Items.Insert(0, new ImageComboBoxItem(string.Empty, null));
                    }
                }
                else
                {
                    combox.Properties.Items.Insert(0, new ImageComboBoxItem(string.Empty, null));
                }
            }
            combox.Properties.EndUpdate();
        }

        /// <summary>
        /// 将枚举类型的值和对应语言的描述绑定到下拉列表   
        /// </summary>
        /// <typeparam name="T">必须是枚举类型</typeparam>
        /// <param name="combox">下拉列表控件</param>
        /// <param name="ensureFirstEmpty">是否保留第一项空白</param>
        public void SetComboxByEnum<T>(LWImageComboBoxEdit combox, bool ensureFirstEmpty)
        {
            SetComboxByEnum<T>(combox, ensureFirstEmpty, false);
        }

        #endregion

        #region 绑定字典
        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(ImageComboBoxEdit cmbEdit, DataDictionaryType type)
        {
            return SetCmbDataDictionary(cmbEdit, type, DataBindType.Unknown);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(ImageComboBoxEdit cmbEdit, DataDictionaryType type, DataBindType bindType)
        {
            return SetCmbDataDictionary(new List<ImageComboBoxEdit> { cmbEdit }, type, bindType, false);
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
        public List<DataDictionaryList> SetCmbDataDictionary(List<ImageComboBoxEdit> cmbEdits, DataDictionaryType type, DataBindType bindType,
            bool ensureFirstEmpty)
        {
            List<DataDictionaryList> list = DataDictionarys.FindAll(delegate(DataDictionaryList item) { return item.Type == type; });
            if (bindType == DataBindType.Code)
            {
                list = list.OrderBy(i => i.Code).ToList();
            }
            else if (bindType == DataBindType.EName)
            {
                list = list.OrderBy(i => i.EName).ToList();
            }
            else if (bindType == DataBindType.CName)
            {
                list = list.OrderBy(i => i.CName).ToList();
            }
            else if (bindType == DataBindType.Unknown)
            {
                list = list.OrderBy(i => LocalData.IsEnglish ? i.EName : i.CName).ToList();
            }

            foreach (var cmbEdit in cmbEdits)
            {
                ImageComboBoxItem selectedItem = cmbEdit.SelectedItem as ImageComboBoxItem;

                cmbEdit.Properties.BeginUpdate();
                cmbEdit.Properties.Items.Clear();

                foreach (var item in list)
                {
                    if (cmbEdit.Properties.Items.GetItem(item.ID) == null)
                    {
                        if (bindType == DataBindType.Code)
                        {
                            cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
                        }
                        else if (bindType == DataBindType.EName)
                        {
                            cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
                        }
                        else if (bindType == DataBindType.CName)
                        {
                            cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.CName, item.ID));
                        }
                        else if (bindType == DataBindType.Unknown)
                        {
                            cmbEdit.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                        }
                    }
                }
                cmbEdit.Properties.EndUpdate();

                if (selectedItem != null)
                {
                    foreach (ImageComboBoxItem item in cmbEdit.Properties.Items)
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
                if (list.FindAll(item => CommonUtility.GuidIsNullOrEmpty(item.ID)).Count == 0)
                {
                    foreach (var cmbEdit in cmbEdits)
                    {
                        cmbEdit.Properties.Items.Insert(0, new ImageComboBoxItem(string.Empty, Guid.Empty));
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
            return SetCmbDataDictionary(cmbEdit, type, DataBindType.Unknown);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(RepositoryItemImageComboBox cmbEdit, DataDictionaryType type, DataBindType bindType)
        {
            return SetCmbDataDictionary(new List<RepositoryItemImageComboBox> { cmbEdit }, type, bindType);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(List<RepositoryItemImageComboBox> cmbEdits, DataDictionaryType type)
        {
            return SetCmbDataDictionary(cmbEdits, type, DataBindType.Unknown);
        }

        /// <summary>
        /// 字典
        /// </summary>
        public List<DataDictionaryList> SetCmbDataDictionary(List<RepositoryItemImageComboBox> cmbEdits, DataDictionaryType type, DataBindType bindType)
        {
            List<DataDictionaryList> list = DataDictionarys.FindAll(delegate(DataDictionaryList item) { return item.Type == type; });
            foreach (var item in list)
            {
                foreach (var cmbEdit in cmbEdits)
                {
                    if (bindType == DataBindType.Code)
                    {
                        cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
                    }
                    else if (bindType == DataBindType.EName)
                    {
                        cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
                    }
                    else if (bindType == DataBindType.CName)
                    {
                        cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.CName, item.ID));
                    }
                    else if (bindType == DataBindType.Unknown)
                    {
                        cmbEdit.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmbEdit"></param>
        /// <param name="type"></param>
        /// <param name="bingdingCode"></param>
        /// <param name="ensureFirstEmpty">确保第一项为空</param>
        /// <returns></returns>
        public List<DataDictionaryList> SetCmbDataDictionary(ImageComboBoxEdit cmbEdit, DataDictionaryType type, DataBindType bindType,
            bool ensureFirstEmpty)
        {
            return SetCmbDataDictionary(new List<ImageComboBoxEdit> { cmbEdit }, type, bindType, ensureFirstEmpty);
        }
        #endregion

        #region 获得默认的字典类型
        List<DataDictionaryList> normalDictionaryList = null;
        /// <summary>
        /// 获得字典列表
        /// </summary>
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
        /// <summary>
        /// 获得指定字典类型的默认数据的Guid
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Guid GetNormalDictionaryID(DataDictionaryType type)
        {
            if (NormalDictionaryList == null) return Guid.Empty;

            DataDictionaryList tager = NormalDictionaryList.Find(delegate(DataDictionaryList item) { return item.Type == type; });
            if (tager == null) return Guid.Empty;
            else return tager.ID;
        }
        /// <summary>
        /// 获得指定类型的默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataDictionaryList GetNormalDictionary(DataDictionaryType type)
        {
            if (NormalDictionaryList == null) return null;

            DataDictionaryList tager = NormalDictionaryList.Find(delegate(DataDictionaryList item) { return item.Type == type; });
            if (tager == null) return tager;
            else return tager;
        }

        #endregion

        #region 绑定客户列表

        /// <summary>
        /// 设置客户的描述类
        /// </summary>
        /// <param name="customerID">客户的ID</param>
        /// <param name="customerDescription">客户的描述,不可为空</param>
        public CustomerInfo SetCustomerDesByID(Guid? customerID, CustomerDescription customerDescription)
        {
            if (customerID.HasValue == false || customerID.Value == Guid.Empty)
            {
                customerDescription.Address 
                    = customerDescription.City 
                    = customerDescription.Contact 
                    = customerDescription.Country
                    = customerDescription.Fax 
                    = customerDescription.Name 
                    = customerDescription.Tel = string.Empty;
                return null;
            }
            else
            {
                if (customerDescription == null)
                {
                    customerDescription = new CustomerDescription();
                }
                CustomerInfo info = CustomerService.GetCustomerInfo(customerID.Value);
                customerDescription.Address = info.EAddress ?? string.Empty;
                customerDescription.City = info.CityName ?? string.Empty;
                customerDescription.Contact = string.Empty ?? string.Empty;
                customerDescription.Country = info.CountryEName ?? string.Empty;
                customerDescription.Fax = info.Fax ?? string.Empty;
                customerDescription.Name = info.EName ?? string.Empty;
                customerDescription.Tel = info.Tel1 ?? string.Empty;
                customerDescription.Remark = info.Remark ?? string.Empty;

                return info;
            }
        }

        /// <summary>
        /// 设置客户的描述类
        /// </summary>
        /// <param name="customerID">客户的ID</param>
        /// <param name="customerDescription">客户的描述,不可为空</param>
        public CustomerInfo SetCustomerDesByID(Guid? customerID, CustomerDescriptionForNew customerDescription)
        {
            if (customerID.HasValue == false || customerID.Value == Guid.Empty)
            {
                customerDescription.Address
                    = customerDescription.City
                    = customerDescription.EnterpriseCodeType
                    = customerDescription.EnterpriseCode
                    = customerDescription.Contact
                    = customerDescription.Country
                    = customerDescription.Fax
                    = customerDescription.Name
                    = customerDescription.Tel = string.Empty;
                return null;
            }
            else
            {
                if (customerDescription == null)
                {
                    customerDescription = new CustomerDescriptionForNew();
                }
                CustomerInfo info = CustomerService.GetCustomerInfo(customerID.Value);
                customerDescription.Address = info.EAddress ?? string.Empty;
                customerDescription.City = info.CityName ?? string.Empty;
                customerDescription.EnterpriseCodeType = info.EnterpriseCodeType ?? string.Empty;
                customerDescription.EnterpriseCode = info.EnterpriseCode ?? string.Empty;
                customerDescription.Contact = string.Empty ?? string.Empty;
                customerDescription.Country = info.CountryEName ?? string.Empty;
                customerDescription.Fax = info.Fax ?? string.Empty;
                customerDescription.Name = info.EName ?? string.Empty;
                customerDescription.Tel = info.Tel1 ?? string.Empty;
                customerDescription.Remark = info.Remark ?? string.Empty;

                return info;
            }
        }
        /// <summary>
        /// 设置客户的描述类(AMS)
        /// </summary>
        /// <param name="customerID">客户的ID</param>
        /// <param name="customerDescriptionForAMS">客户的描述,不可为空</param>
        public CustomerInfo SetCustomerDesByIDForAMS(Guid? customerID, CustomerDescriptionForAMS customerDescriptionForAMS)
        {
            if (customerID.HasValue == false || customerID.Value == Guid.Empty)
            {
                customerDescriptionForAMS.Address = customerDescriptionForAMS.City
                    = customerDescriptionForAMS.Country = customerDescriptionForAMS.Zip
                    = customerDescriptionForAMS.Name = string.Empty;
                return null;
            }
            else
            {
                if (customerDescriptionForAMS == null)
                {
                    customerDescriptionForAMS = new CustomerDescriptionForAMS();
                }
                CustomerInfo info = CustomerService.GetCustomerInfo(customerID.Value);
                customerDescriptionForAMS.Address = info.EAddress ?? string.Empty;
                customerDescriptionForAMS.City = info.CityName ?? string.Empty;
                customerDescriptionForAMS.Country = info.CountryEName ?? string.Empty;
                customerDescriptionForAMS.Name = info.EName ?? string.Empty;
                customerDescriptionForAMS.Zip = info.Tel1 ?? string.Empty;

                return info;
            }
        }


        /// <summary>
        /// 设置客户的下拉列表
        /// </summary>
        /// <param name="mcmbCarrier">控件</param>
        /// <param name="customerType">客户类型</param>
        /// <returns></returns>
        public List<CustomerList> BindCustomerList(MultiSearchCommonBox mcmbCarrier, CustomerType customerType)
        {


            List<CustomerList> carriers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                             string.Empty, string.Empty, null, null, CustomerStateType.Valid,
                                                             customerType, null, null, null, null, null, 0);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
            col.Add("Code", "代码");
            mcmbCarrier.InitSource<CustomerList>(carriers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            return carriers;
        }

        public List<CustomerList> SetCheckBoxComboBoxCarriers(COCheckBoxComboBox cmbControls)
        {
            List<CustomerList> dataList = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                             string.Empty, string.Empty, null, null, CustomerStateType.Valid,
                                                             CustomerType.Carrier, null, null, null, null, null, 0);

            cmbControls.BeginUpdate();
            cmbControls.ClearItems();

            foreach (CustomerList item in dataList)
            {
                cmbControls.AddItem(item.ID, item.Code);
            }
            cmbControls.EndUpdate();
            return dataList;
        }

        public List<CustomerList> SetComboxCarriers(LWCheckBoxComboBox cmbControls)
        {
            List<CustomerList> dataList = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                             string.Empty, string.Empty, null, null, CustomerStateType.Valid,
                                                             CustomerType.Carrier, null, null, null, null, null, 0);

            cmbControls.BeginUpdate();
            cmbControls.Items.Clear();

            foreach (CustomerList item in dataList)
            {
                cmbControls.AddItem(item.ID, item.Code);
            }
            cmbControls.EndUpdate();
            return dataList;
        }
        #endregion

        #region 公司配置
        /// <summary>
        /// 公司 配置里的公司列表
        /// </summary>
        public List<ConfigureList> SetCmbConfigureCompany(ImageComboBoxEdit cmbEdit)
        {
            List<ConfigureList> configureList = ConfigureService.GetConfigureListByVaid(true);
            cmbEdit.Properties.BeginUpdate();
            cmbEdit.Properties.Items.Clear();
            foreach (var item in configureList)
            {
                cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.CompanyName, item.CompanyID));
            }
            cmbEdit.Properties.EndUpdate();
            return configureList;
        }

        #endregion

        #region 币种
        #region 下拉框控件
        /// <summary>
        /// 填充币种(通过口岸)
        /// </summary>
        /// <param name="cmbCurrency">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="IsSetStandard">是否设置本位币</param>
        /// <param name="isSetDefault">是否设置默认币种</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<SolutionCurrencyList> SetComboxCurrencys(LWImageComboBoxEdit cmbCurrency, Guid companyID
            ,bool IsSetStandard = false,bool isSetDefault = false, bool isNewNullData= false)
        {
            List<SolutionCurrencyList> currencys = ConfigureService.GetCompanyCurrencyList(companyID, true);
            cmbCurrency.Properties.BeginUpdate();
            cmbCurrency.Properties.Items.Clear();

            foreach (SolutionCurrencyList item in currencys)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }

            if (isNewNullData)
            {
                cmbCurrency.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbCurrency.Properties.EndUpdate();
            if (currencys != null && currencys.Count > 0)
            {
                if (IsSetStandard)
                {
                    SolutionCurrencyList currency=
                        currencys.SingleOrDefault(fitem => fitem.CurrencyID == fitem.StandardCurrencyID);
                    if (currency != null)
                    {
                        cmbCurrency.EditValue = currency.CurrencyID;
                        cmbCurrency.Text = currency.CurrencyName;
                    }
                }
                if (isSetDefault)
                {
                    SolutionCurrencyList currency =
                        currencys.SingleOrDefault(fitem => fitem.CurrencyID == fitem.DefaultCurrencyID);
                    if (currency != null)
                    {
                        cmbCurrency.EditValue = currency.CurrencyID;
                        cmbCurrency.Text = currency.CurrencyName;
                    }
                }
            }
            return currencys;
        }

        /// <summary>
        /// 填充币种(通过解决方案)
        /// </summary>
        /// <param name="cmbCurrency">下拉框</param>
        /// <param name="solutionID">解决方案ID</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<SolutionCurrencyList> SetComboxCurrencysBySolution(LWImageComboBoxEdit cmbCurrency, Guid solutionID,bool isNewNullData = false)
        {
            List<SolutionCurrencyList> currencys = ConfigureService.GetSolutionCurrencyList(solutionID, true);
            cmbCurrency.Properties.BeginUpdate();
            cmbCurrency.Properties.Items.Clear();

            foreach (SolutionCurrencyList item in currencys)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.ID));
            }

            if (isNewNullData)
            {
                cmbCurrency.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbCurrency.Properties.EndUpdate();
            return currencys;
        }
        #endregion
        #endregion

        #region 箱型
        /// <summary>
        /// 箱型
        /// </summary>
        public List<ContainerList> SetCmbContainerType(RepositoryItemImageComboBox cmbEdit)
        {
            List<ContainerList> ctntypes = TransportFoundationService.GetContainerList(string.Empty, true, 0);
            cmbEdit.Properties.BeginUpdate();
            foreach (var item in ctntypes)
            {
                cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            cmbEdit.Properties.EndUpdate();
            return ctntypes;
        }
        /// <summary>
        /// 绑箱形
        /// </summary>
        /// <param name="combox"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<ContainerList> SetCmbContainerType(ImageComboBoxEdit combox, List<ContainerList> list)
        {
            combox.Properties.BeginUpdate();
            combox.Properties.Items.Clear();
            foreach (var item in list)
            {
                combox.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            combox.Properties.EndUpdate();
            return list;
        }
        #endregion


        #region 费用代码Charging Code
        /// <summary>
        /// 填充费用代码(通过组)
        /// </summary>
        /// <param name="cmbLocation">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="IsSetStandard">是否设置本位币</param>
        /// <param name="isSetDefault">是否设置默认币种</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<ChargingCodeList> SetCheckBoxComboBoxChargeCode(COCheckBoxComboBox cmbControls)
        {
            if (_ChargingCodeList.Count <= 0)
                _ChargingCodeList = ConfigureService.GetChargingCodeListByGroupID(new Guid("FD7FDFD7-AC1C-E011-B1CD-001321CC6D9F"));
            cmbControls.BeginUpdate();
            cmbControls.ClearItems();

            foreach (ChargingCodeList item in _ChargingCodeList)
            {
                cmbControls.AddItem(item.ID, LocalData.IsEnglish?item.EName:item.CName);
            }
            cmbControls.EndUpdate();
            return _ChargingCodeList;
        }
        #endregion

        #region 地点信息
        /// <summary>
        /// 填充币种(通过口岸)
        /// </summary>
        /// <param name="cmbControls">下拉框</param>
        /// <returns></returns>
        public List<LocationList> SetCheckBoxComboBoxLocations(COCheckBoxComboBox cmbControls)
        {
            if(_RecentLocationList.Count<= 0)
                _RecentLocationList = GeographyService.GetRecentLocationList(DateTime.Now.AddMonths(-1),DateTime.Now.AddMonths(1)); ;
            cmbControls.BeginUpdate();
            cmbControls.ClearItems();

            foreach (LocationList item in _RecentLocationList)
            {
                cmbControls.AddItem(item.ID, item.EName);
            }
            cmbControls.EndUpdate();
            return _RecentLocationList;
        }
        /// <summary>
        /// 填充地点信息(通过口岸)
        /// </summary>
        /// <param name="cmbLocation">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="IsSetStandard">是否设置本位币</param>
        /// <param name="isSetDefault">是否设置默认币种</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<LocationList> SetComboxLocations(LWImageComboBoxEdit cmbLocation, bool isNewNullData = false)
        {
            List<LocationList> locations = GeographyService.GetLocationList(string.Empty, null, null, true, null, null, true, 0);
            cmbLocation.Properties.BeginUpdate();
            cmbLocation.Properties.Items.Clear();

            foreach (LocationList item in locations)
            {
                cmbLocation.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
            }

            if (isNewNullData)
            {
                cmbLocation.Properties.Items.Insert(0, new ImageComboBoxItem(null, null));
            }

            cmbLocation.Properties.EndUpdate();
            return locations;
        }

        /// <summary>
        /// 填充币种(通过口岸)
        /// </summary>
        /// <param name="cmbLocation">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="IsSetStandard">是否设置本位币</param>
        /// <param name="isSetDefault">是否设置默认币种</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<LocationList> SetComboxLocations(LWCheckBoxComboBox cmbLocation)
        {
            List<LocationList> locations = GeographyService.GetLocationList(string.Empty, null, null, true, null, null, true, 0);
            cmbLocation.BeginUpdate();
            cmbLocation.Items.Clear();

            foreach (LocationList item in locations)
            {
                cmbLocation.AddItem(item.ID, item.EName);
            }
            cmbLocation.EndUpdate();
            return locations;
        }
        #endregion

        #region 船名
        /// <summary>
        /// 填充币种(通过口岸)
        /// </summary>
        /// <param name="cmbLocation">下拉框</param>
        /// <param name="companyID">公司ID</param>
        /// <param name="IsSetStandard">是否设置本位币</param>
        /// <param name="isSetDefault">是否设置默认币种</param>
        /// <param name="isNewNullData">是否加入空行</param>
        /// <returns></returns>
        public List<VesselList> SetCheckBoxComboBoxVessels(COCheckBoxComboBox cmbControls)
        {
            if(_RecentVesselList.Count<=0)
            {
                DateTime beginDate = DateTime.Now.AddMonths(-1);
                beginDate = new DateTime(beginDate.Year, beginDate.Month, 1);
                DateTime endDate = DateTime.Now.AddMonths(1);
                _RecentVesselList = TransportFoundationService.GetRecentVesselList(beginDate, endDate);
            }
            cmbControls.BeginUpdate();
            cmbControls.ClearItems();

            foreach (VesselList item in _RecentVesselList)
            {
                cmbControls.AddItem(item.ID, item.Name);
            }
            cmbControls.EndUpdate();
            return _RecentVesselList;
        }
        #endregion

        #region 航线
        /// <summary>
        /// 航线
        /// </summary>
        public List<ShippingLineList> SetCheckBoxComboBoxShippingLine(COCheckBoxComboBox cmbControls)
        {
            List<ShippingLineList> shippingLines = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 100);
            cmbControls.BeginUpdate();
            cmbControls.ClearItems();

            foreach (ShippingLineList item in shippingLines)
            {
                cmbControls.AddItem(item.ID,LocalData.IsEnglish ? item.EName : item.CName);
            }
            cmbControls.EndUpdate();
            return shippingLines;
        }
        /// <summary>
        /// 航线
        /// </summary>
        public List<ShippingLineList> SetCmbShippingLine(ImageComboBoxEdit cmbEdit)
        {
            List<ShippingLineList> shippingLines = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 100);
            cmbEdit.Properties.BeginUpdate();
            cmbEdit.Properties.Items.Clear();

            foreach (ShippingLineList item in shippingLines)
            {
                cmbEdit.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }
            cmbEdit.Properties.EndUpdate();

            return shippingLines;
        }
        /// <summary>
        /// 航线
        /// </summary>
        /// <param name="cmbControls"></param>
        public List<ShippingLineList> SetCmbShippingLine(LWCheckBoxComboBox cmbControls)
        {
            List<ShippingLineList> dataList = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 100);
            cmbControls.BeginUpdate();
            cmbControls.Items.Clear();

            foreach (ShippingLineList item in dataList)
            {
                cmbControls.AddItem(item.ID, LocalData.IsEnglish ? item.EName : item.CName);
            }
            cmbControls.EndUpdate();
            return dataList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmbControls"></param>
        /// <returns></returns>
        public List<ShippingLineList> SetTreeCheckShippingLine(TreeCheckControl tccControl)
        {
            List<ShippingLineList> dataList = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 100);
            List<TreeCheckControlSource> tss = new List<TreeCheckControlSource>();
            foreach (var item in dataList)
            {
                tss.Add(new TreeCheckControlSource { ID = item.ID, ParentID = item.ParentID, Name = LocalData.IsEnglish ? item.EName : item.CName });
            }
            tccControl.SetSource(tss);
            return dataList;
        }
        #endregion

        #region 运输条款
        /// <summary>
        /// 运输条款
        /// </summary>
        public List<TransportClauseList> SetCmbTransportClause(ImageComboBoxEdit cmbEdit)
        {
            cmbEdit.Properties.BeginUpdate();
            cmbEdit.Properties.Items.Clear();
            List<TransportClauseList> transportClauses = TransportFoundationService.GetTransportClauseList(string.Empty, string.Empty, true, 0);
            foreach (var item in transportClauses)
            {
                cmbEdit.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            cmbEdit.Properties.EndUpdate();
            return transportClauses;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (_dataDictionarys != null)
            {
                _dataDictionarys = null;
            }
        }

        #endregion
    }
}
