using System;
using ICP.MailCenter.ServiceInterface;
using System.Xml.Linq;
namespace ICP.Operation.Common.ServiceInterface
{   
    /// <summary>
    /// 上下文菜单项信息类
    /// </summary>
    [Serializable]
   public class ContextMenuItemInfo:ITemplateItemData
    {   
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

        #region ITemplateItemData 成员

        public ITemplateItemData Init(XElement element)
        {
            ReadOrdinaryPropertyValues(element);
            if (this.Type == ContextMenuItemType.MenuItem)
            {
                ReadText(element);
            }
            return this;
        }

        private void ReadText(XElement element)
        {
            XElement captionElement = element.Element(XName.Get("Text"));
            string languageAttributeName = MailBusinessHelper.GetLanguageAttributeName();
            this.Text = captionElement.Attribute(XName.Get(languageAttributeName)).Value;
        }

        private void ReadOrdinaryPropertyValues(XElement element)
        {
            this.Name = element.Attribute("Name").Value;
            this.Id = element.Attribute("Id").Value;
            this.ImageName = element.Attribute("ImageName").Value;

            this.Site = element.Attribute("Site").Value;

            this.RegisterSite = element.Attribute("RegisterSite").Value;

            this.Type = (ContextMenuItemType)Enum.Parse(typeof(ContextMenuItemType), element.Attribute("Type").Value);
            if (element.Attribute("BeginGroup") != null)
            {
                this.BeginGroup = Boolean.Parse(element.Attribute("BeginGroup").Value);
            }
        }

        #endregion
    }
}
