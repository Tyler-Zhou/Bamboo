using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Repository;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.ReportCenter.UI.Comm.Controls;
using ICP.Framework.ClientComponents.Controls;
using System.Text.RegularExpressions;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using Microsoft.Reporting.WinForms;
using ICP.FAM.ServiceInterface;

namespace ICP.ReportCenter.UI
{
    public static class Utility
    {

        #region 处理值类型

        public static DateTime GetEndDate(DateTime date)
        {
            string dateStr = date.ToShortDateString();
            dateStr += " 23:59:59";
            return DateTime.Parse(dateStr);
        }

        #endregion

        #region UIHelper

        /// <summary>
        /// 绑定controls的keyDown的Enter事件到执行btnSearch的PerformClick方法
        /// </summary>
        public static void SearchPartKeyEnterToSearch(List<Control> controls, DevExpress.XtraEditors.SimpleButton btnSearch)
        {
            if (controls == null || controls.Count == 0) return;
            foreach (var item in controls)
            {
                item.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
                };
            }
        }

        /// <summary>
        /// 绑定controls的keyDown的F2事件到执行btnSearch的PerformClick方法
        /// </summary>
        public static void SearchPartKeyF2ToSearch(List<Control> controls, DevExpress.XtraEditors.SimpleButton btn)
        {
            if (controls == null || controls.Count == 0) return;
            foreach (var item in controls)
            {
                item.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.F2) btn.PerformClick();
                };
            }
        }

        /// <summary>
        /// 生成ComboboxItem
        /// </summary>
        public static void BulidComboboxItem<T>(DevExpress.XtraEditors.ImageComboBoxEdit cmbEdit, int index)
        {
            List<EnumHelper.ListItem<T>> types = EnumHelper.GetEnumValues<T>(LocalData.IsEnglish);
            cmbEdit.Properties.BeginUpdate();
            foreach (var item in types)
            {
                cmbEdit.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbEdit.Properties.EndUpdate();
            cmbEdit.SelectedIndex = index;
        }

        /// <summary>
        /// 绑定年份
        /// </summary>
        /// <param name="cmbEdit"></param>
        public static void BulidYearCombBox(DevExpress.XtraEditors.ImageComboBoxEdit cmbEdit)
        {
            for (int i = 2000; i <= DateTime.Now.Year; i++)
            {
                cmbEdit.Properties.Items.Insert(0,new DevExpress.XtraEditors.Controls.ImageComboBoxItem(i.ToString(), i.ToString()));
            }
            cmbEdit.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定余额方向
        /// </summary>
        public static void BulidBalanceDirectionComBox(DevExpress.XtraEditors.CheckedComboBoxEdit cmbEdit)
        {
            List<EnumHelper.ListItem<BalanceDirection>> types = EnumHelper.GetEnumValues<BalanceDirection>(LocalData.IsEnglish);

            cmbEdit.Properties.Items.Clear();
            cmbEdit.Properties.BeginUpdate();

            foreach (var itemCode in types)
            {
                if (itemCode.Value != BalanceDirection.All)
                {
                    CheckedListBoxItem item = new CheckedListBoxItem();
                    item.Value = itemCode.Value;
                    item.Description = itemCode.Name;
                    item.CheckState = CheckState.Checked;
                    cmbEdit.Properties.Items.Add(item);
                }
            }

            cmbEdit.Properties.EndUpdate();
        }
        /// <summary>
        /// 绑定帐页格式 
        /// </summary>
        /// <param name="cmbReportFormat"></param>
        public static void BulidGLCodeLedgerStyle(DevExpress.XtraEditors.ImageComboBoxEdit cmbReportFormat)
        {
            List<EnumHelper.ListItem<GLCodeLedgerStyle>> formats = EnumHelper.GetEnumValues<GLCodeLedgerStyle>(LocalData.IsEnglish);
            cmbReportFormat.Properties.BeginUpdate();
            cmbReportFormat.Properties.Items.Clear();
            foreach (var item in formats)
            {
                if (item.Value == GLCodeLedgerStyle.AMOUNT || item.Value == GLCodeLedgerStyle.OUTGOAMOUNT)
                {
                    cmbReportFormat.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
            }
            cmbReportFormat.Properties.EndUpdate();
            cmbReportFormat.SelectedIndex = 0;
        }

        /// <summary>
        /// 分组类型-出口箱信息报表
        /// </summary>
        public static void BuildGroupBy_ExportContainerList(Comm.Controls.CheckBoxComboBox chkcmbGroupBy)
        {
            List<EnumHelper.ListItem<ECLGroupBy>> groups = EnumHelper.GetEnumValues<ECLGroupBy>(LocalData.IsEnglish);

            chkcmbGroupBy.ClearItems();
            foreach (var item in groups)
            {
                chkcmbGroupBy.AddItem(item.Value, item.Name, false);
            }
            chkcmbGroupBy.Items[0].CheckState = CheckState.Checked;
            chkcmbGroupBy.RefreshText();
        }
        #endregion

        #region ExecuteOnec

        public delegate void SetDelegateSource();

        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(Control control, SetDelegateSource method)
        {
            bool isInit = false;
            control.Enter += delegate
            {
                if (isInit) return;
                isInit = true;
                method();
            };
        }

        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(RepositoryItemImageComboBox control, SetDelegateSource method)
        {
            bool isInit = false;
            control.Enter += delegate
            {
                if (isInit) return;
                isInit = true;
                method();
            };
        }

        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(List<Control> controls, SetDelegateSource method)
        {
            bool isInit = false;

            foreach (var item in controls)
            {
                item.Enter += delegate
                {
                    if (isInit) return;
                    isInit = true;
                    method();
                };
            }
        }

        #endregion

        #region ExtensionMethods
        public static bool IsGuid(string str)
        {
            Match m = Regex.Match(str, @"^[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}$", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                //可以转换 
                return true;
            }
            else
            {
                //不可转换 
                return false;
            }
        }
        /// <summary>
        /// ToSplitString
        /// </summary>
        public static string ToSplitString(this List<Guid> input, string splitString)
        {
            if (input == null || input.Count ==0) return string.Empty;

            StringBuilder strBuilder = new StringBuilder();
            foreach (var item in input)
            {
                if (strBuilder.Length > 0) strBuilder.Append(splitString);
                strBuilder.Append(item.ToString());
            }

            return strBuilder.ToString();

        }


        /// <summary>
        /// ToSplitString
        /// </summary>
        public static string ObjectsToSplitString(this List<object> input, string splitString)
        {
            if (input == null || input.Count == 0) return string.Empty;

            StringBuilder strBuilder = new StringBuilder();
            foreach (var item in input)
            {
                if (item == null) continue;

                if (strBuilder.Length > 0) strBuilder.Append(splitString);
                strBuilder.Append(item.ToString());
            }

            return strBuilder.ToString();

        }

        /// <summary>
        /// TagToSplitString
        /// </summary>
        public static string TagToSplitString(this object obj, string splitString)
        {
            if (obj == null) return string.Empty;

            List<Guid> input =obj as List<Guid>;
            if (input == null || input.Count == 0) return string.Empty;

            StringBuilder strBuilder = new StringBuilder();
            foreach (var item in input)
            {
                if (strBuilder.Length > 0) strBuilder.Append(splitString );
                strBuilder.Append(item.ToString());
            }

            return strBuilder.ToString();

        }

        #endregion

        #region 用户的默认公司与默认部门
        /// <summary>
        ///默认公司名称
        /// </summary>
        public static string UserDefaultCompanyName
        {
            get
            { 
                 string companyName=string.Empty;
                 if (string.IsNullOrEmpty(companyName))
                 {
                     //找到默认公司的名称
                     LocalOrganizationInfo CompanyInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Company && item.IsDefault == true; });
                     if (CompanyInfo != null)
                     {
                         companyName = LocalData.IsEnglish ? CompanyInfo.EShortName : CompanyInfo.CShortName;
                     }
                 }
                 if(string.IsNullOrEmpty(companyName))
                 {
                     //如果没有默认公司，就返回默认区域的名称
                     LocalOrganizationInfo SectionInfo=LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type==LocalOrganizationType.Section && item.IsDefault==true;});
                     if(SectionInfo!=null)
                     {
                        companyName=LocalData.IsEnglish?SectionInfo.EShortName:SectionInfo.CShortName;
                     }
                 }
                 if(string.IsNullOrEmpty(companyName))
                 {
                     //如果没有默认公司，就返回默认总部的名称
                     LocalOrganizationInfo RootInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Root && item.IsDefault == true; });
                     if(RootInfo!=null)
                     {
                        companyName=LocalData.IsEnglish?RootInfo.EShortName:RootInfo.CShortName;
                     }
                 }
                return companyName;
            }
        }

        /// <summary>
        ///默认公司ID
        /// </summary>
        public static Guid UserDefaultCompanyID
        {
            get
            { 
                 Guid companyID=Guid.Empty;
                 if (companyID == Guid.Empty)
                 {
                     //找到默认公司的ID
                     LocalOrganizationInfo CompanyInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Company && item.IsDefault == true; });

                     if (CompanyInfo != null)
                     {
                         companyID = CompanyInfo.ID;
                     }
                 }
                 if(companyID==Guid.Empty)
                 {
                     //如果没有默认公司，就返回默认区域的名称
                     LocalOrganizationInfo SectionInfo=LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type==LocalOrganizationType.Section && item.IsDefault==true;});
                     if(SectionInfo!=null)
                     {
                        companyID=SectionInfo.ID;
                     }
                 }
                  if(companyID==Guid.Empty)
                 {
                     //如果没有默认公司，就返回默认总部的名称
                     LocalOrganizationInfo RootInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Root && item.IsDefault == true; });
                     if(RootInfo!=null)
                     {
                        companyID=RootInfo.ID;
                     }
                 }


                return companyID;
            }
        }

        /// <summary>
        ///默认部门名称
        /// </summary>
        public static string UserDefaultDepartmentName
        {
            get
            {
                string departmentName = string.Empty;

                if (string.IsNullOrEmpty(departmentName))
                {
                    //没有默认部门的名称，就返回默认公司的名称
                    LocalOrganizationInfo DepartmentInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Department && item.IsDefault == true; });
                    if (DepartmentInfo != null)
                    {
                        departmentName = LocalData.IsEnglish ? DepartmentInfo.EShortName : DepartmentInfo.CShortName;
                    }
                }
                if (string.IsNullOrEmpty(departmentName))
                {
                    //没有默认部门，就返回默认公司的名称
                    LocalOrganizationInfo CompanyInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Company && item.IsDefault == true; });
                    if (CompanyInfo != null)
                    {
                        departmentName = LocalData.IsEnglish ? CompanyInfo.EShortName : CompanyInfo.CShortName;
                    }
                }
                if (string.IsNullOrEmpty(departmentName))
                {
                    //如果没有默认公司，就返回默认区域的名称
                    LocalOrganizationInfo SectionInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Section && item.IsDefault == true; });
                    if (SectionInfo != null)
                    {
                        departmentName = LocalData.IsEnglish ? SectionInfo.EShortName : SectionInfo.CShortName;
                    }
                }
                if (string.IsNullOrEmpty(departmentName))
                {
                    //如果没有默认区域，就返回默认总部的名称
                    LocalOrganizationInfo RootInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Root && item.IsDefault == true; });
                    if (RootInfo != null)
                    {
                        departmentName = LocalData.IsEnglish ? RootInfo.EShortName : RootInfo.CShortName;
                    }
                }

                return departmentName;
            }
        }

        /// <summary>
        ///默认部门ID
        /// </summary>
        public static Guid UserDefaultDepartmentID
        {
            get
            {
                Guid departmentID = Guid.Empty;

                if (departmentID==Guid.Empty)
                {
                    // 找到默认部门的ID
                    LocalOrganizationInfo DepartmentInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Department && item.IsDefault == true; });
                    if (DepartmentInfo != null)
                    {
                        departmentID = DepartmentInfo.ID;
                    }
                }
                if (departmentID == Guid.Empty)
                {
                    //没有默认部门，就找默认公司的
                    LocalOrganizationInfo CompanyInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Company && item.IsDefault == true; });
                    if (CompanyInfo != null)
                    {
                        departmentID = CompanyInfo.ID;
                    }
                }
                if (departmentID == Guid.Empty)
                {
                    //如果没有默认公司，就返回默认区域的名称
                    LocalOrganizationInfo SectionInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Section && item.IsDefault == true; });
                    if (SectionInfo != null)
                    {
                        departmentID = SectionInfo.ID;
                    }
                }
                if (departmentID == Guid.Empty)
                {
                    //如果没有默认区域，就返回默认总部的名称
                    LocalOrganizationInfo RootInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Root && item.IsDefault == true; });
                    if (RootInfo != null)
                    {
                        departmentID = RootInfo.ID;
                    }
                }
                return departmentID;
            }
        }
        #endregion

        #region 用户的下属列表
        /// <summary>
        /// 绑定用户下属列表
        /// </summary>
        /// <param name="Workitem"></param>
        /// <param name="txtSales"></param>
        /// <param name="userServices"></param>
        public static void BindUserList(Microsoft.Practices.CompositeUI.WorkItem Workitem,DevExpress.XtraEditors.ButtonEdit txtSales)
        {

            List<UserList> UserDataList = GetSubordinateUserList();
            if (UserDataList == null || UserDataList.Count == 0 || UserDataList.Count==1)
            {
                txtSales.Enabled = false;
                List<Guid> idList = new List<Guid>();
                idList.Add(LocalData.UserInfo.LoginID);
                txtSales.Tag = idList;
                txtSales.Text = LocalData.UserInfo.UserName;
            }
            else
            {
                txtSales.ButtonClick += delegate(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
                {
                    ShowSelectUser(Workitem, txtSales, UserDataList);
                };

                txtSales.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        ShowSelectUser(Workitem, txtSales, UserDataList);
                    }
                };

                txtSales.KeyPress += delegate(object sender, KeyPressEventArgs e)
                {
                    e.Handled = true;
                };
            }
        }

  

        /// <summary>
        /// 查询用户
        /// </summary>
        private static void ShowSelectUser(Microsoft.Practices.CompositeUI.WorkItem Workitem, DevExpress.XtraEditors.ButtonEdit txtSales, List<UserList> UserDataList)
        {
            SelectUserListPart selectUser = Workitem.Items.AddNew<SelectUserListPart>();
            string title = LocalData.IsEnglish ? "Select User" : "选择用户";

            selectUser.DataList = UserDataList;
            selectUser.SelectIDList = txtSales.Tag;

            if (PartLoader.ShowDialog(selectUser, title) == DialogResult.OK)
            {
                txtSales.Tag = selectUser.UserIDList;
                txtSales.Text = selectUser.UserNames;
            }
        }

        private static List<UserList> subordinateUserList;
        public static List<UserList> GetSubordinateUserList()
        {
            if (subordinateUserList == null || subordinateUserList.Count == 0)
            {
                subordinateUserList = UserService.GetSubordinateUserList(LocalData.UserInfo.LoginID, true);
            }
            return subordinateUserList;
        }
        #endregion

        #region Service
        public static IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }
        public static IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        public static IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }
        #endregion

        #region 默认公司的解决方案ID
        private static Guid solutionID = Guid.Empty;
        public static Guid SolutionID
        {
            get
            {
                if (solutionID == Guid.Empty)
                {
                    ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    if (configureInfo != null)
                    {
                        solutionID = configureInfo.SolutionID;
                    }
                }

                return solutionID;
            }
        }
        #endregion

        #region 显示凭证信息
        public static void ShoLedgerInfo(string reportEmbeddedResource, IList<ReportParameter> parameters)
        {
            if (!reportEmbeddedResource.Contains("UFShowLedgerReport"))
            {
                return;
            }
            if (parameters == null)
            {
                return ;
            }
            if (parameters[0].Values[0] == null)
            {
                return ;
            }
            Guid id = new Guid(parameters[0].Values[0].ToString());

            FinanceClientService.ShowLedgerInfo(id);
        }
        #endregion
    }
 
}
