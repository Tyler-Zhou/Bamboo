using System;
using System.Collections.Generic;
using DevExpress.XtraBars;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;
using System.IO;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 工具栏子项工厂
    /// </summary>
    public class ToobarItemFactory
    {
        private static ImageList imageList;
        public static ImageList ImageList
        {
            get
            {

                if (imageList == null)
                {
                    imageList = CreateImageList();
                }
                return imageList;
            }
        }
        private static ImageList CreateImageList()
        {
            return UIHelper.GetImageList(GetImageFilePath());

        }
        private static string GetImageFilePath()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine("Images", "Toolbar");
            return Path.Combine(root, relativePath);
        }

        /// <summary>
        /// 获取工具栏子项
        /// </summary>
        /// <param name="itemInfo">子项信息</param>
        /// <returns></returns>
        public static BarItem GetBarItem(OperationToolbarCommand itemInfo, BarManager manager)
        {
            BarItem barItem = null;
            if (itemInfo == null)
            {
                return barItem;
            }
            switch (itemInfo.Type)
            {
                case MenuItemType.SubButton:
                    barItem = CreateSubButton(itemInfo);
                    break;

                case MenuItemType.Button:
                    barItem = CreateButton(itemInfo);
                    break;

                case MenuItemType.TextBox:
                    barItem = CreateTextBox(itemInfo);
                    break;
                case MenuItemType.Label:
                    barItem = CreateLabel(itemInfo);
                    break;
                case MenuItemType.CheckBox:
                    barItem = CreateCheckbox(itemInfo);
                    break;
                case MenuItemType.ComboBox:
                    barItem = CreateComboBox(itemInfo, manager);
                    break;
                default:
                    throw new ICPException(string.Format("ToobarItemFactory不支持工具栏子项类型:{0}", itemInfo.Type.ToString()));


            }
            return barItem;
        }

        private static BarItem CreateComboBox(OperationToolbarCommand itemInfo, BarManager manager)
        {
            DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit edit = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            manager.RepositoryItems.Add(edit);
            BarEditItem barItem = new BarEditItem(manager);
            barItem.Edit = edit;
            barItem.Width = 150;
            barItem.CanOpenEdit = true;
            barItem.Caption = itemInfo.Text;
            edit.NullText = string.Empty;
            edit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            edit.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            edit.SelectAllItemVisible = true;
            edit.SelectAllItemCaption = LocalData.IsEnglish ? "Select All" : "全选";
            edit.SynchronizeEditValueWithCheckedItems = true;
            if (itemInfo.Tag == "FillCompany")
            {     
                List<LocalOrganizationInfo> companyList = LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
                companyList.Sort(new CompanyComparer());
                edit.DataSource = companyList;
                string name = LocalData.IsEnglish ? "EShortName" : "CShortName";
                edit.DisplayMember = name;
                edit.ValueMember = "ID";
                //设置用户默认显示和选中值
                barItem.EditValue = LocalData.UserInfo.DefaultCompanyName;
                DevExpress.XtraEditors.Controls.CheckedListBoxItemCollection items = edit.GetItems();
                //foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in items)
                //{
                //    if (item.Description.Equals(LocalData.UserInfo.DefaultCompanyName))
                //    {
                //        item.CheckState = CheckState.Checked;
                //    }
                //}
                
            }


            return barItem;
        }

        private static BarItem CreateCheckbox(OperationToolbarCommand itemInfo)
        {

            BarCheckItem barItem = new BarCheckItem();

            SetBarCheckItemCheckState(itemInfo, barItem);
            return barItem;
        }

        private static void SetBarCheckItemCheckState(OperationToolbarCommand itemInfo, BarCheckItem item)
        {
            List<string> keys = new List<string> { Constants.AutoFillSOKey, Constants.NotifyCSKey };
            if (itemInfo.Name == Constants.AutoFillSOKey)
            {
                item.Checked = SOSetting.Current.AutoFillSO;
            }
            else if (itemInfo.Name == Constants.NotifyCSKey)
            {
                item.Checked = SOSetting.Current.NotfiyCS;
            }
            else
            {
                item.Checked = true;
            }
        }



        private static BarStaticItem CreateLabel(OperationToolbarCommand itemInfo)
        {
            return new BarStaticItem();
        }

        private static BarEditItem CreateTextBox(OperationToolbarCommand itemInfo)
        {
            BarEditItem barItem = new BarEditItem();
            DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            barItem.Edit = edit;
            barItem.Name = itemInfo.Name;
            if (itemInfo.Tag == "Query")
            {
                edit.KeyDown += new KeyEventHandler(edit_KeyDown);
            }
            edit.NullText = itemInfo.Text;
            barItem.Width = 150;
            barItem.CanOpenEdit = itemInfo.HasPermission;
            return barItem;
        }

        static void edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(UIHelper.TemplateCode))
                {
                    if (
                        UIHelper.TemplateCode.Equals(
                            ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Customer.ToString()))
                    {
                        SetCacheApplication(sender);
                        RootWorkItem.Commands["Command_Customer_EmailQuery"].Execute();
                    }
                    else if (
                        UIHelper.TemplateCode.Equals(
                            ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Unknown.ToString()))
                    {
                        SetCacheApplication(sender);
                        RootWorkItem.Commands["Command_Unknown_EmailQuery"].Execute();
                    }
                }
            }
        }
        private static WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        private static IListBaseBusinessPart listBaseBusinessPart
        {
            get { return RootWorkItem.Items.Get(UIHelper.TemplateCode) as IListBaseBusinessPart; }

        }

        private static void SetCacheApplication(object editoer)
        {
            TextEdit textEdit = editoer as TextEdit;
            if (textEdit != null)
            {
                RootWorkItem.State["QueryString"] = textEdit.Text;
            }
            RootWorkItem.State["CurrentBaseBusinessPart"] = listBaseBusinessPart;
        }






        private static BarButtonItem CreateButton(OperationToolbarCommand itemInfo)
        {
            return new BarButtonItem();
        }

        private static BarSubItem CreateSubButton(OperationToolbarCommand itemInfo)
        {
            return new BarSubItem();
        }
    }
    public class CompanyComparer : IComparer<LocalOrganizationInfo>
    {

        #region IComparer<LocalOrganizationInfo> 成员

        public int Compare(LocalOrganizationInfo x, LocalOrganizationInfo y)
        {
            if (x == y)
                return 0;
            return x.IsDefault ? -1 : y.IsDefault ? 1 : 0;

        }

        #endregion
    }
}
