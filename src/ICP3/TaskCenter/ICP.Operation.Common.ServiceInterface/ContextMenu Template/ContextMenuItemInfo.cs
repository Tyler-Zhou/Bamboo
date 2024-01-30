using ICP.MailCenter.ServiceInterface;
using System;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 上下文菜单项信息类
    /// </summary>
    [Serializable]
    public class ContextMenuItemInfo : ITemplateItemData
    {
        /// <summary>
        /// 
        /// </summary>
        public ContextMenuItemInfo()
        {
            Enabled = true;
        }

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 命令名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 注册点
        /// </summary>
        public string Site { get; set; }
        /// <summary>
        /// 关联的业务号(可以是SO,MBL NO,HBL No等)
        /// </summary>
        public string BusinessNo
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegisterSite
        {
            get;
            set;
        }
        /// <summary>
        /// 命名Caption
        /// </summary>
        public string Text
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public ContextMenuItemType Type
        {
            get;
            set;
        }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImageName
        {
            get;
            set;
        }
        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool HasPermission
        {
            get;
            set;
        }
        /// <summary>
        /// 是否开始分组，即加入分隔符
        /// </summary>
        public bool BeginGroup
        {
            get;
            set;
        }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string Tag
        {
            get;
            set;
        }
        /// <summary>
        /// 是否能够使用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否为子项菜单
        /// </summary>
        public bool Subitem { get; set; }

        #region ITemplateItemData 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public ITemplateItemData Init(XElement element)
        {
            ReadOrdinaryPropertyValues(element);

            ReadText(element);

            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        private void ReadText(XElement element)
        {
            XElement captionElement = element.Element(XName.Get("Text"));
            string languageAttributeName = MailBusinessHelper.GetLanguageAttributeName();
            Text = captionElement.Attribute(XName.Get(languageAttributeName)).Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        private void ReadOrdinaryPropertyValues(XElement element)
        {
            Name = element.Attribute("Name").Value;
            Id = element.Attribute("Id").Value;
            ImageName = element.Attribute("ImageName").Value;

            Site = element.Attribute("Site").Value;

            RegisterSite = element.Attribute("RegisterSite").Value;

            Type = (ContextMenuItemType)Enum.Parse(typeof(ContextMenuItemType), element.Attribute("Type").Value);
            if (element.Attribute("BeginGroup") != null)
            {
                BeginGroup = Boolean.Parse(element.Attribute("BeginGroup").Value);
            }

            XAttribute enabledAttribute = element.Attribute("Enabled");
            if (enabledAttribute == null)
                Enabled = true;
            else
                Enabled = Boolean.Parse(enabledAttribute.Value);

            Index = 0;
            if (element.Attribute("Subitem") != null)
            {
                Subitem = Boolean.Parse(element.Attribute("Subitem").Value);
            }

        }

        #endregion
    }
}
