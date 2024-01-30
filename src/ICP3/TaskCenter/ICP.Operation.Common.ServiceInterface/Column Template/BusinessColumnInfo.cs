using ICP.DataCache.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.MailCenter.ServiceInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务面板列信息类
    /// </summary>
    [Serializable]
    public sealed class BusinessColumnInfo : ITemplateItemData
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessColumnInfo()
        {
            EditType = ColumnEditType.Text;
            ItemInfos = new List<ImageComboBoxItemInfo>();
            Editable = true;
            Dropable = false;
            ReadOnly = false;
            IsSearch = false;
            DocumentType = DocumentType.Other;
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
        /// <summary>
        /// 是否支持拖放操作中的拖入动作
        /// </summary>
        public bool Dropable { get; set; }
        /// <summary>
        /// 如果列支持拖入动作，拖入的系统文档类型，比如MBL,SO
        /// </summary>
        public DocumentType DocumentType { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// 中英环境
        /// </summary>
        public bool IsEnglish
        {
            get
            {
                return ApplicationContext.Current.IsEnglish;
            }
        }
        /// <summary>
        /// 表头的宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// 字段是否需要转换
        /// </summary>
        public bool Convert { get; set; }

        /// <summary>
        /// 分组条件
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 排序条件
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 是否查询
        /// </summary>
        public bool IsSearch { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        static List<XElement> sectionElements = null;

        #region ITemplateItemData 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public ITemplateItemData Init(XElement element)
        {
            if (sectionElements == null)
            {
                sectionElements = GetColumnsList();
            }
            XElement xElement =
                (from elemen in sectionElements
                 where elemen.Attribute("Name").Value == element.Attribute("Name").Value
                 select elemen).FirstOrDefault();
            if(xElement==null)
            {
                throw new Exception(string.Format("There is no definition of column [{0}]", element.Attribute("Name").Value));
            }
            ReadOrdinaryPropertyValues(element, xElement);
            ReadCaption(element, xElement);
            ReadItemInfos(xElement);
            ReadToolTip(xElement);
            ReadSqlWhere(element);
            return this;
        }

        /// <summary>
        /// 读取表头配置文件
        /// </summary>
        /// <returns></returns>
        public List<XElement> GetColumnsList()
        {
            string fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
            string tempalteFileName = "ColumnTemplateGather.xml";
            string templateFilePath = Path.Combine(fileRootDirectory, tempalteFileName);
            XDocument document = XDocument.Load(templateFilePath);
            return document.Element(XName.Get("Columns")).Elements().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnElement"></param>
        private void ReadItemInfos(XElement columnElement)
        {
            try
            {
                if (EditType != ColumnEditType.ImageComboBox)
                    return;
                XElement xelement = columnElement.Element(XName.Get("ItemsInfos"));
                if (xelement == null)
                    return;
                if (xelement.Attribute("ItemType") == null)
                    return;
                string itemTypeName = xelement.Attribute("ItemType").Value;
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
                    ItemInfos.Add(item);
                }
                items.Clear();
                items = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="english"></param>
        /// <param name="returnEmptyIfNull"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 表头显示文本值
        /// </summary>
        /// <param name="element"></param>
        private void ReadCaption(XElement element, XElement Xelement)
        {
            XElement captionElement = null;
            string languageAttributeName = string.Empty;
            string Caption = string.Empty;
            //判断当前的表头文本值是否被重命名
            captionElement = element.Element(XName.Get("CaptionRname"));
            if (captionElement != null)
            {
                languageAttributeName = MailBusinessHelper.GetLanguageAttributeName();
                Caption = captionElement.Attribute(XName.Get(languageAttributeName)).Value;
            }
            else
            {
                captionElement = Xelement.Element(XName.Get("Caption"));
                languageAttributeName = MailBusinessHelper.GetLanguageAttributeName();
                Caption = captionElement.Attribute(XName.Get(languageAttributeName)).Value;
            }
            this.Caption = Caption;
        }
        /// <summary>
        /// ToolTip值
        /// </summary>
        /// <param name="element"></param>
        private void ReadToolTip(XElement element)
        {
            XElement toolTipElement = element.Element(XName.Get("ToolTip"));
            if (toolTipElement != null)
            {
                string languageAttributeName = MailBusinessHelper.GetLanguageAttributeName();
                ToolTip = toolTipElement.Attribute(XName.Get(languageAttributeName)).Value;
            }
            else
            {
                ToolTip = string.Empty;
            }
        }
        /// <summary>
        /// 分组条件和排序条件
        /// </summary>
        /// <param name="element"></param>
        private void ReadSqlWhere(XElement element)
        {
            XElement SqlWhereElement = element.Element(XName.Get("SqlWhere"));
            if (SqlWhereElement != null)
            {
                Group = string.IsNullOrEmpty(SqlWhereElement.Attribute("Group").Value) ? string.Empty : SqlWhereElement.Attribute("Group").Value;
                Order = string.IsNullOrEmpty(SqlWhereElement.Attribute("Order").Value) ? string.Empty : SqlWhereElement.Attribute("Order").Value;
            }
        }


        /// <summary>
        /// 构造类表的属性值
        /// </summary>
        /// <param name="element"></param>
        /// <param name="xElement"></param>
        private void ReadOrdinaryPropertyValues(XElement element, XElement xElement)
        {
            Name = xElement.Attribute("Name").Value;
            FieldName = xElement.Attribute("FieldName").Value;
            Editable = Boolean.Parse(xElement.Attribute("Editable").Value);
            EditType = (ColumnEditType)Enum.Parse(typeof(ColumnEditType), xElement.Attribute("EditType").Value);
            //先读取集合XML文件中的信息
            if (xElement.Attribute("Dropable") != null)
            {
                Dropable = Boolean.Parse(xElement.Attribute("Dropable").Value);
            }
            if (xElement.Attribute("ReadOnly") != null)
            {
                ReadOnly = Boolean.Parse(xElement.Attribute("ReadOnly").Value);
            }
            if (xElement.Attribute("DocumentType") != null)
            {
                DocumentType = (DocumentType)Enum.Parse(typeof(DocumentType), xElement.Attribute("DocumentType").Value);
            }
            if (xElement.Attribute("Width") != null)
            {
                Width = int.Parse(xElement.Attribute("Width").Value);
            }
            if (xElement.Attribute("Convert") != null)
            {
                Convert = Boolean.Parse(xElement.Attribute("Convert").Value);
            }
            if (xElement.Attribute("IsSearch") != null)
            {
                IsSearch = Boolean.Parse(xElement.Attribute("IsSearch").Value);
            }
            //读取当前配置文件的XML进行特殊的处理
            if (element.Attribute("Convert") != null)
            {
                Convert = Boolean.Parse(element.Attribute("Convert").Value);
            }
            if (element.Attribute("ReadOnly") != null)
            {
                ReadOnly = Boolean.Parse(element.Attribute("ReadOnly").Value);
            }
            if (element.Attribute("Width") != null)
            {
                Width = int.Parse(element.Attribute("Width").Value);
            }
            if (element.Attribute("Dropable") != null)
            {
                Dropable = Boolean.Parse(element.Attribute("Dropable").Value);
            }
            if (element.Attribute("DocumentType") != null)
            {
                DocumentType = (DocumentType)Enum.Parse(typeof(DocumentType), element.Attribute("DocumentType").Value);
            }
            if (element.Attribute("EditType") != null)
            {
                EditType = (ColumnEditType)Enum.Parse(typeof(ColumnEditType), element.Attribute("EditType").Value);
            }
            if (element.Attribute("Editable") != null)
            {
                Editable = Boolean.Parse(element.Attribute("Editable").Value);
            }
            if (element.Attribute("IsSearch") != null)
            {
                IsSearch = Boolean.Parse(element.Attribute("IsSearch").Value);
            }
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
    /// <summary>
    /// 
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IntegalValue { get; set; }
    }
    /// <summary>
    /// 列比较器 
    /// </summary>   
    public class CustomColumnInfoComparer : IComparer<CustomColumnInfo>
    {
        #region IComparer<CustomColumnInfo> 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(CustomColumnInfo x, CustomColumnInfo y)
        {
            if (x == y) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            return x.VisibleIndex.CompareTo(y.VisibleIndex);
        }

        #endregion
    }
}
