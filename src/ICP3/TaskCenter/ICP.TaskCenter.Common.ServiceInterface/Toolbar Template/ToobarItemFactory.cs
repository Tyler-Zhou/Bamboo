using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using ICP.MailCenter.Business.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using DevExpress.XtraEditors.Controls;

namespace ICP.MailCenter.Business.ServiceInterface
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
       public static BarItem GetBarItem(OperationToolbarCommand itemInfo,BarManager manager)
       {
           BarItem barItem = null;
           if (itemInfo == null)
           {
               return barItem;
           }
           switch (itemInfo.Type)
           {
               case  MenuItemType.SubButton:
                   barItem = CreateSubButton(itemInfo);
                   break;

               case  MenuItemType.Button:
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
                   barItem = CreateComboBox(itemInfo,manager);
                   break;
               default:
                   throw new ICPException(string.Format("ToobarItemFactory不支持工具栏子项类型:{0}", itemInfo.Type.ToString()));


           }
           return barItem;
       }

       private static BarItem CreateComboBox(OperationToolbarCommand itemInfo,BarManager manager)
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
            List<LocalOrganizationInfo>  companyList = LocalData.UserInfo.UserOrganizationList.FindAll(o => o.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
            companyList.Sort(new CompanyComparer());
            edit.DataSource = companyList;
               string name=LocalData.IsEnglish?"EShortName" : "CShortName";
            edit.DisplayMember = name;
            edit.ValueMember = "ID";

            
           

            
           }
            
     
           return barItem;
        

       }
      
       private static BarItem CreateCheckbox(OperationToolbarCommand itemInfo)
       {  
           
           BarCheckItem barItem = new BarCheckItem();

           SetBarCheckItemCheckState(itemInfo,barItem);
           return barItem;
       }

       private static void SetBarCheckItemCheckState(OperationToolbarCommand itemInfo,BarCheckItem item)
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
           edit.NullText = itemInfo.Text;
           barItem.Width = 150;
          
           return barItem;
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
