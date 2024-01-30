using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using System.Xml.Linq;
using System.Reflection;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.MailCenter.Business.ServiceInterface
{   
    /// <summary>
    /// 业务面板列信息类
    /// </summary>
    [Serializable]
   public sealed class BusinessColumnInfo:ITemplateItemData
   {
       #region 构造函数
       /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessColumnInfo()
        {
            this.EditType = ColumnEditType.Text;
            ItemInfos = new List<ImageComboBoxItemInfo>();
            this.Editable = true;
        }
       #endregion

        #region 属性
        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }
     
        /// <summary>
        /// 所绑定的数据源字段/属性名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool Editable { get; set; }
    
        /// <summary>
        /// 列编辑器类型
        /// </summary>
        public ColumnEditType EditType { get; set; }
        /// <summary>
        /// 图像下拉选框子项信息
        /// </summary>
        public List<ImageComboBoxItemInfo> ItemInfos { get; set; }

        public bool IsEnglish
        {
            get {
                return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.IsEnglish;
            }
        }
        #endregion
        #region ITemplateItemData 成员

        public ITemplateItemData Init(XElement element)
        {  

            ReadOrdinaryPropertyValues(element);
            ReadCaption(element);
            ReadItemInfos(element);
            return this;
        }

        private void ReadItemInfos(XElement columnElement)
        {
            try
            {
                if (this.EditType != ColumnEditType.ImageComboBox)
                    return;
                string itemTypeName = columnElement.Element(XName.Get("ItemsInfos")).Attribute("ItemType").Value;
                Type type = Type.GetType(itemTypeName);

                Array values = Enum.GetValues(type);
                List<EnumItem> items = new List<EnumItem>();
                bool isEnglish = IsEnglish;
                foreach (object item in values)
                {
                    EnumItem enumItem = new EnumItem();
                    enumItem.Value = item;
                    enumItem.Description = GetDescription(item, isEnglish, true);
                    enumItem.IntegalValue = (int)item;
                    items.Add(enumItem);
                }

                var itemElements = columnElement.Element(XName.Get("ItemsInfos")).Elements();
                if (itemElements.Count() <= 0)
                    return;
                foreach (var element in itemElements)
                {
                    ImageComboBoxItemInfo item = new ImageComboBoxItemInfo();
                    item.Value = Int32.Parse(element.Attribute("Value").Value);
                    item.ImageName = element.Attribute("ImageName").Value;
                    item.Description = items.Find(enumItem => enumItem.IntegalValue == item.Value).Description;
                    this.ItemInfos.Add(item);
                }
                items.Clear();
                items = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static string GetDescription(object value, bool english, bool returnEmptyIfNull)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = string.Empty;
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo != null)
            {
                object[] attrs = fieldInfo.GetCustomAttributes(typeof(MemberDescriptionAttribute), false);

                if (attrs != null)
                {
                    MemberDescriptionAttribute[] attributes = (MemberDescriptionAttribute[])attrs;
                    if (attributes != null && attributes.Length > 0)
                    {
                        description = english ? attributes[0].EDescription : attributes[0].CDescription;
                    }
                }
            }

            if (string.IsNullOrEmpty(description))
            {
                if (returnEmptyIfNull)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.ToString();
                }
            }

            return description;
        }

        private void ReadCaption(XElement element)
        {
          XElement captionElement = element.Element(XName.Get("Caption"));
          string languageAttributeName = MailBusinessHelper.GetLanguageAttributeName();
          this.Caption= captionElement.Attribute(XName.Get(languageAttributeName)).Value;
        }

        private void ReadOrdinaryPropertyValues(XElement element)
        {
            this.Name = element.Attribute("Name").Value;
        

            this.FieldName = element.Attribute("FieldName").Value;
            this.Editable = Boolean.Parse(element.Attribute("Editable").Value);
            this.EditType =(ColumnEditType)Enum.Parse(typeof(ColumnEditType),element.Attribute("EditType").Value);
        }


        #endregion
    }
    /// <summary>
    /// 图像下拉选框项信息类
    /// </summary>
    [Serializable]
    public sealed class ImageComboBoxItemInfo
    {   
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 枚举对应的整数值
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 项所对应的图片名称
        /// </summary>
        public string ImageName { get; set; }
    }
    public class EnumItem
    {
        public object Value { get; set; }
        public string Description { get; set; }
        public int IntegalValue { get; set; }
    }
}
