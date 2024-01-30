using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using ICP.MailCenter.Business.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
namespace ICP.MailCenter.Business.ServiceInterface
{ 
    /// <summary>
    /// 列编辑器工厂类
    /// </summary>
  public class ColumnEditFactory
    {
      private static ImageList imageList;
      public static ImageList ImageList
      {
          get {

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
          string relativePath = Path.Combine("Images", "Email");
          return Path.Combine(root, relativePath);
      }
      public static RepositoryItem GetColumnEdit(BusinessColumnInfo columnInfo)
      {
          RepositoryItem columnEdit = null;
          if (columnInfo == null)
          {
              return columnEdit; 
          }
          switch (columnInfo.EditType)
          { 
              case ColumnEditType.Text:
                  columnEdit = CreateTextEdit(columnInfo);
                  break;

              case ColumnEditType.Checkbox:
                  columnEdit = CreateCheckboxEdit(columnInfo);
                  break;
              case ColumnEditType.ImageComboBox:
                  columnEdit = CreateImageComboBoxEdit(columnInfo);
                  break;
              case ColumnEditType.Memo:
                  columnEdit = CreateMemoEdit(columnInfo);
                  break;
              default:
                  throw new ICPException(string.Format("ColumnEditFactory不支持列编辑器类型:{0}",columnInfo.EditType.ToString()));
          
          
          }
          columnEdit.Name = columnInfo.Name;
          return columnEdit;


      }
      private static RepositoryItem CreateTextEdit(BusinessColumnInfo columnInfo)
      {
          DevExpress.XtraEditors.Repository.RepositoryItemTextEdit textEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
          textEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
          textEdit.NullText = string.Empty;
          textEdit.ReadOnly = true;
        
          return textEdit;


      }
      private static RepositoryItem CreateCheckboxEdit(BusinessColumnInfo columnInfo)
      {
          DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit checkboxEdit = new RepositoryItemCheckEdit();
          checkboxEdit.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
          checkboxEdit.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
          checkboxEdit.ReadOnly = true;
          return checkboxEdit;
      }
      private static RepositoryItem CreateImageComboBoxEdit(BusinessColumnInfo columnInfo)
      {
          DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox comboboxEdit = new RepositoryItemImageComboBox();
          comboboxEdit.AutoHeight = false;
          ((System.ComponentModel.ISupportInitialize)(comboboxEdit)).BeginInit();
          comboboxEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
          //comboboxEdit.SmallImages = ImageList;
          comboboxEdit.Items.AddRange(CreateImageComboBoxItems(columnInfo.ItemInfos));
          

          ((System.ComponentModel.ISupportInitialize)(comboboxEdit)).EndInit();
          return comboboxEdit;
      }

      private static DevExpress.XtraEditors.Controls.ImageComboBoxItem[] CreateImageComboBoxItems(List<ImageComboBoxItemInfo> itemInfoList)
      {
          if (itemInfoList == null || itemInfoList.Count <= 0)
              return null;
          List<DevExpress.XtraEditors.Controls.ImageComboBoxItem> itemList = new List<DevExpress.XtraEditors.Controls.ImageComboBoxItem>();
          foreach (ImageComboBoxItemInfo itemInfo in itemInfoList)
          {
          
              int imageIndex = -1;
              //if (!string.IsNullOrEmpty(itemInfo.ImageName))
              //{
              //   imageIndex= ImageList.Images.IndexOfKey(itemInfo.ImageName);
              //}
            
            itemList.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(itemInfo.Description,(Byte)itemInfo.Value,imageIndex));
          }
          return itemList.ToArray();
      }
      private static RepositoryItem CreateMemoEdit(BusinessColumnInfo columnInfo)
      {
         
          RepositoryItemMemoExEditBIS memoEdit=new RepositoryItemMemoExEditBIS();
          memoEdit.Appearance.Options.UseTextOptions = true;
          memoEdit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
          memoEdit.AutoSpellCheck = false;
          memoEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
          memoEdit.ConvertEmptyStringToNull = true;
          //memoEdit.Name = "memoEdit"+(i++).ToString();
          memoEdit.ShowIcon = false;
          return memoEdit;
         
      }
      public static int i = 1;

    }
}
