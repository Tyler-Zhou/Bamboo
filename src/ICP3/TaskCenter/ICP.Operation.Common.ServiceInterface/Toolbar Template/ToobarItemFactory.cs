using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;
using System.IO;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Helper;

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

        private static BarItemPaintStyle BarItemPaintStyle
        {
            get { return BarItemPaintStyle.Caption; }
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
                    barItem = CreateComboBoxNew(itemInfo, manager);
                    break;
                default:
                    throw new ICPException(string.Format("ToobarItemFactory不支持工具栏子项类型:{0}", itemInfo.Type.ToString()));


            }
            return barItem;
        }

        private static RepositoryItemCheckedComboBoxEdit edit;
        private static BarItem CreateComboBox(OperationToolbarCommand itemInfo, BarManager manager)
        {
            edit = new RepositoryItemCheckedComboBoxEdit();
            manager.RepositoryItems.Add(edit);

            BarEditItem barItem = new BarEditItem(manager) { PaintStyle = BarItemPaintStyle };
            barItem.Edit = edit;
            barItem.Width = 110;
            barItem.CanOpenEdit = true;
            barItem.Caption = itemInfo.Text;
            edit.NullText = string.Empty;
            edit.TextEditStyle = TextEditStyles.DisableTextEditor;
            edit.AllowNullInput = DefaultBoolean.True;
            edit.SelectAllItemVisible = true;
            edit.SelectAllItemCaption = LocalData.IsEnglish ? "Select All" : "全选";
            if (itemInfo.Tag == "FillCompany")
            {
                StringBuilder strBuf = new StringBuilder();
                List<LocalOrganizationInfo> organizationList = LocalData.UserInfo.UserOrganizationList;
                organizationList.Sort(new CompanyComparer());
                organizationList.ForEach(delegate(LocalOrganizationInfo item)
                  {
                      if (item.Type.Equals(LocalOrganizationType.Company))
                      {
                          edit.Items.Add(item.ID, (LocalData.IsEnglish ? item.EShortName : item.CShortName), CheckState.Checked, true);
                          strBuf.Append(string.Format("{0},", (LocalData.IsEnglish ? item.EShortName : item.CShortName)));
                      }
                  });
                string name = LocalData.IsEnglish ? "EShortName" : "CShortName";
                edit.DisplayMember = name;
                edit.ValueMember = "ID";
                edit.Popup += OnShowCheckedPopup;
                //默认显示选中所有操作口岸出来                
                if (!string.IsNullOrEmpty(strBuf.ToString()) && strBuf.ToString().Length > 1)
                    barItem.EditValue = strBuf.ToString(0, strBuf.ToString().Length - 1);
                organizationList = null;
            }

            edit.SynchronizeEditValueWithCheckedItems = false;

            return barItem;
        }

        private static RepositoryItemComboBox comboBox;
        private static BarItem CreateComboBoxNew(OperationToolbarCommand itemInfo, BarManager manager)
        {
            comboBox = new RepositoryItemComboBox();
            manager.RepositoryItems.Add(comboBox);

            BarEditItem barItem = new BarEditItem(manager) { PaintStyle = BarItemPaintStyle };
            barItem.Edit = comboBox;
            if (itemInfo.Width > 0)
                barItem.Width = itemInfo.Width;
            else
                barItem.Width = 100;

            barItem.CanOpenEdit = true;
            barItem.Caption = itemInfo.Text;

            if (itemInfo.Tag == "FillScope")
            {
                List<EnumHelper.ListItem<ScopeItem>> scopeItems = EnumHelper.GetEnumValues<ScopeItem>(LocalData.IsEnglish);
                comboBox.TextEditStyle = TextEditStyles.DisableTextEditor;
                comboBox.Items.Clear();
                foreach (var item in scopeItems)
                {
                    comboBox.Items.Add(item.Name);
                }
                barItem.EditValue = scopeItems[0].Name;
            }

            return barItem;
        }

        static void OnShowCheckedPopup(object sender, EventArgs e)
        {
            edit.SynchronizeEditValueWithCheckedItems = true;
        }

        private static BarItem CreateCheckbox(OperationToolbarCommand itemInfo)
        {
            BarCheckItem barItem = new BarCheckItem() { PaintStyle = BarItemPaintStyle };

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
            return new BarStaticItem() { PaintStyle = BarItemPaintStyle };
        }

        private static BarEditItem CreateTextBox(OperationToolbarCommand itemInfo)
        {
            BarEditItem barItem = new BarEditItem();
            RepositoryItemTextEdit edit = new RepositoryItemTextEdit();
            barItem.Name = itemInfo.Name;
            edit.NullValuePromptShowForEmptyValue = true;
            edit.NullValuePrompt = itemInfo.Text;
            edit.AutoHeight = false;
            if (itemInfo.Tag == "Query")
            {
                edit.KeyDown += OnEditItemKeyDown;
            }
            barItem.Edit = edit;
            edit.AllowFocused = true;
            barItem.Width = 142;
            //barItem.PaintStyle = BarItemPaintStyle.CaptionInMenu;
            barItem.CanOpenEdit = itemInfo.HasPermission;
            return barItem;
        }

        private static void OnEditItemKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
            e.SuppressKeyPress = true;
            string templateCode = UIHelper.TemplateCode;
            if (string.IsNullOrEmpty(templateCode))
            {
                return;
            }
            SetCacheApplication(sender as TextEdit);

            if (templateCode.Equals(ListFormType.MailLink4in1.ToString()))
            {
                RootWorkItem.State["MailCenterEntrySearchStopwatch"] = StopwatchHelper.StartStopwatch();
                RootWorkItem.Commands["Command_MailLink4in1_EmailQuery"].Execute();
            }

            #region Invalidate
            //if (
            //    templateCode.Equals(
            //        ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Customer.ToString()))
            //{
            //    RootWorkItem.Commands["Command_Customer_EmailQuery_1"].Execute();
            //}
            //else if (
            //    templateCode.Equals(
            //        ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4Unknown.ToString()))
            //{
            //    RootWorkItem.Commands["Command_Unknown_EmailQuery_1"].Execute();
            //}
            //else if (templateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierSO.ToString()))
            //{
            //    RootWorkItem.Commands["Command_CarrierSO_EmailQuery_1"].Execute();
            //}
            //else if (templateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierMBL.ToString()))
            //{
            //    RootWorkItem.Commands["Command_CarrierMBL_EmailQuery_1"].Execute();
            //}
            //else if (
            //    templateCode.Equals(
            //        ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierAP.ToString()))
            //{
            //    RootWorkItem.Commands["Command_CarrierAP_EmailQuery_1"].Execute();
            //}
            //else if (templateCode.Equals(ICP.Common.ServiceInterface.DataObjects.ListFormType.MailLink4CarrierAN.ToString()))
            //{
            //    RootWorkItem.Commands["Command_CarrierAN_EmailQuery_1"].Execute();
            //}
            #endregion

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

        private static void SetCacheApplication(TextEdit textEdit)
        {
            if (textEdit != null)
            {
                RootWorkItem.State["QueryString"] = textEdit.Text;
            }
            RootWorkItem.State["CurrentBaseBusinessPart"] = listBaseBusinessPart;
        }

        private static BarButtonItem CreateButton(OperationToolbarCommand itemInfo)
        {
            return new BarButtonItem() { PaintStyle = BarItemPaintStyle };
        }

        private static BarSubItem CreateSubButton(OperationToolbarCommand itemInfo)
        {
            return new BarSubItem() { PaintStyle = BarItemPaintStyle };
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
