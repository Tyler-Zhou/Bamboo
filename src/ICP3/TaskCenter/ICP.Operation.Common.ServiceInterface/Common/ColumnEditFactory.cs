using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 列编辑器工厂类
    /// </summary>
    public class ColumnEditFactory
    {
        public static int i = 1;
        /// <summary>
        /// 
        /// </summary>
        private static ImageList imageList;
        /// <summary>
        /// 
        /// </summary>
        public static ImageList ImageList
        {
            get
            {

                if (imageList == null)
                {
                    imageList = CreateImageList();
                    imageList.ImageSize = new Size(12, 12);
                }
                return imageList;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static ImageList CreateImageList()
        {
            return UIHelper.GetImageList(GetImageFilePath());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetImageFilePath()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine("Images", "Email");
            return Path.Combine(root, relativePath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
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
                case ColumnEditType.DateTime:
                    columnEdit = CreateDateTimeEdit(columnInfo);
                    break;
                case ColumnEditType.ItemCheckEdit:
                    columnEdit = CreateItemCheckEdit(columnInfo);
                    break;
                case ColumnEditType.ItemCheckEdits:
                    columnEdit = CreateItemCheckEdits(columnInfo);
                    break;
                default:
                    throw new ICPException(string.Format("ColumnEditFactory不支持列编辑器类型:{0}", columnInfo.EditType.ToString()));
            }
            columnEdit.Name = columnInfo.Name;
            return columnEdit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private static RepositoryItem CreateItemCheckEdit(BusinessColumnInfo columnInfo)
        {
            RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
            ((ISupportInitialize)(repositoryItemCheckEdit)).BeginInit();
            repositoryItemCheckEdit.AutoHeight = false;
            repositoryItemCheckEdit.ValueChecked = (byte)3;
            repositoryItemCheckEdit.ValueGrayed = (byte)2;
            repositoryItemCheckEdit.ValueUnchecked = (byte)1;
            ((ISupportInitialize)(repositoryItemCheckEdit)).EndInit();
            return repositoryItemCheckEdit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private static RepositoryItem CreateItemCheckEdits(BusinessColumnInfo columnInfo)
        {
            RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
            ((ISupportInitialize)(repositoryItemCheckEdit)).BeginInit();
            repositoryItemCheckEdit.AutoHeight = false;
            repositoryItemCheckEdit.ValueChecked = 3;
            repositoryItemCheckEdit.ValueGrayed = 2;
            repositoryItemCheckEdit.ValueUnchecked = 1;
            ((ISupportInitialize)(repositoryItemCheckEdit)).EndInit();
            return repositoryItemCheckEdit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private static RepositoryItem CreateDateTimeEdit(BusinessColumnInfo columnInfo)
        {
            RepositoryItemDateEdit dateEdit = new RepositoryItemDateEdit();
            dateEdit.AllowNullInput = DefaultBoolean.True;
            dateEdit.NullDate = null;
            dateEdit.NullText = string.Empty;
            dateEdit.NullValuePromptShowForEmptyValue = false;
            dateEdit.ShowClear = true;
            dateEdit.ShowToday = true;
            dateEdit.DisplayFormat.FormatString = "yyyy-MM-dd";
            dateEdit.EditMask = "yyyy-MM-dd";
            dateEdit.TextEditStyle = TextEditStyles.Standard;
            dateEdit.HighlightHolidays = true;
            dateEdit.ShowDropDown = ShowDropDown.SingleClick;

            return dateEdit;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private static RepositoryItem CreateTextEdit(BusinessColumnInfo columnInfo)
        {
            RepositoryItemTextEdit textEdit = new RepositoryItemTextEdit();
            textEdit.AllowNullInput = DefaultBoolean.True;
            textEdit.NullText = string.Empty;
            return textEdit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private static RepositoryItem CreateCheckboxEdit(BusinessColumnInfo columnInfo)
        {
            RepositoryItemCheckEdit checkboxEdit = new RepositoryItemCheckEdit();
            checkboxEdit.CheckStyle = CheckStyles.Standard;
            checkboxEdit.NullStyle = StyleIndeterminate.Unchecked;
            checkboxEdit.ReadOnly = true;
            return checkboxEdit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private static RepositoryItem CreateImageComboBoxEdit(BusinessColumnInfo columnInfo)
        {
            RepositoryItemImageComboBox comboboxEdit = new RepositoryItemImageComboBox();
            comboboxEdit.AutoHeight = false;
            ((ISupportInitialize)(comboboxEdit)).BeginInit();
            comboboxEdit.AllowNullInput = DefaultBoolean.True;
            comboboxEdit.SmallImages = ImageList;
            if (columnInfo != null && columnInfo.ItemInfos != null && columnInfo.ItemInfos.Count > 0)
            {
                comboboxEdit.Items.AddRange(CreateImageComboBoxItems(columnInfo.ItemInfos));
            }
            comboboxEdit.LargeImages = imageList;
            ((ISupportInitialize)(comboboxEdit)).EndInit();
            return comboboxEdit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemInfoList"></param>
        /// <returns></returns>
        private static ImageComboBoxItem[] CreateImageComboBoxItems(List<ImageComboBoxItemInfo> itemInfoList)
        {
            if (itemInfoList == null || itemInfoList.Count <= 0)
                return null;
            List<ImageComboBoxItem> itemList = new List<ImageComboBoxItem>();
            foreach (ImageComboBoxItemInfo itemInfo in itemInfoList)
            {

                int imageIndex = -1;
                if (!string.IsNullOrEmpty(itemInfo.ImageName))
                {
                    imageIndex = ImageList.Images.IndexOfKey(itemInfo.ImageName);
                }

                //判断是否使用图片
                if (imageIndex == -1)
                {
                    itemList.Add(new ImageComboBoxItem(itemInfo.Description,
                                                                                       (Byte)itemInfo.Value, imageIndex));
                }
                else
                {
                    itemList.Add(new ImageComboBoxItem(string.Empty,
                                                                                       (Byte)itemInfo.Value, imageIndex));
                }
            }
            return itemList.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        private static RepositoryItem CreateMemoEdit(BusinessColumnInfo columnInfo)
        {

            RepositoryItemMemoExEditBIS memoEdit = new RepositoryItemMemoExEditBIS();
            memoEdit.Appearance.Options.UseTextOptions = true;
            memoEdit.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            memoEdit.AutoSpellCheck = false;
            memoEdit.Buttons.AddRange(new EditorButton[] {
            new EditorButton()});
            memoEdit.ConvertEmptyStringToNull = true;
            //memoEdit.Name = "memoEdit"+(i++).ToString();
            memoEdit.ShowIcon = false;
            return memoEdit;

        }
    }
}
