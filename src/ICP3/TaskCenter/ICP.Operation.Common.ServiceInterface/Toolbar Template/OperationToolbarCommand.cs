using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICP.MailCenter.ServiceInterface;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{

    /// <summary>
    /// 工具栏项类
    /// </summary>
    public class OperationToolbarCommand : ITemplateItemData
    {

        #region 字段及属性定义
        /// <summary>
        /// 命名Caption
        /// </summary>
        public string Text
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 驻留在界面上的位置？
        /// </summary>
        public string Site
        {
            get;
            set;
        }

        /// <summary>
        /// 注册在界面上的位置？
        /// </summary>
        public string RegisterSite
        {
            get;
            set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public MenuItemType Type
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
        public bool BeginGroup
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义信息
        /// </summary>
        public string Tag
        {
            get;
            set;
        }
        /// <summary>
        /// 提示
        /// </summary>
        public string Tooltip
        {
            get;
            set;
        }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool HasPermission { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 点击操作
        /// </summary>
        public string ClickOperation { get; set; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 实例化一个命令
        /// </summary>
        /// <param name="ctext"></param>
        /// <param name="etext"></param>
        /// <param name="name"></param>
        /// <param name="site"></param>
        /// <param name="registerSite"></param>
        /// <param name="type"></param>
        public OperationToolbarCommand(string text, string name, string site, string registerSite, string type, string imageName)
        {
            Text = text;
            Name = name;
            Site = site.ToLower();
            RegisterSite = registerSite.ToLower();
            Type = (MenuItemType)Enum.Parse(typeof(MenuItemType), type, true);
            ImageName = imageName;
        }
        public OperationToolbarCommand()
        {
            HasPermission = true;
        }
        #endregion
        static List<XElement> _toolBarList = null;
        /// <summary>
        /// 读取表头配置文件
        /// </summary>
        /// <returns></returns>
        public List<XElement> GetToolBarList()
        {
            string fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
            string tempalteFileName = "ToolBarConfigGather.xml";
            string templateFilePath = Path.Combine(fileRootDirectory, tempalteFileName);
            XDocument document = XDocument.Load(templateFilePath);
            return document.Element(XName.Get("Columns")).Elements().ToList();
        }

        #region ITemplateItemData 成员

        public ITemplateItemData Init(XElement element)
        {
            if (_toolBarList == null)
            {
                _toolBarList = GetToolBarList();
            }
            XElement xElement =
             (from e in _toolBarList
              where e.Attribute("Id").Value == element.Attribute("Id").Value
              select e).FirstOrDefault();
            ReadOrdinaryPropertyValues(element, xElement);
            ReadText(xElement, element);
            return this;
        }

        #endregion
        /// <summary>
        /// 构造按钮的Text
        /// </summary>
        /// <param name="element">当前节点的配置信息</param>
        /// <param name="xElement">基础配置信息</param>
        private void ReadText(XElement xElement, XElement element)
        {
            XElement captionElement = null;
            string languageAttributeName = string.Empty;
            if (element.Element(XName.Get("Text")) != null)
            {
                captionElement = element.Element(XName.Get("Text"));
                languageAttributeName = MailBusinessHelper.GetLanguageAttributeName();
            }
            else
            {
                captionElement = xElement.Element(XName.Get("Text"));
                languageAttributeName = MailBusinessHelper.GetLanguageAttributeName();
            }
            Text = captionElement.Attribute(XName.Get(languageAttributeName)).Value;
            Tooltip = GetTooltip(xElement, languageAttributeName);
        }

        private string GetTooltip(XElement element, string languageAttributeName)
        {
            string tooltip = string.Empty;
            XElement tooltipElement = element.Element(XName.Get("ToolTip"));
            if (tooltipElement != null)
            {
                tooltip = tooltipElement.Attribute(XName.Get(languageAttributeName)).Value;
            }
            return tooltip;

        }

        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="element">当前节点的配置信息</param>
        /// <param name="xElement">基础配置信息</param>
        private void ReadOrdinaryPropertyValues(XElement element, XElement xElement)
        {
            ImageName = xElement.Attribute("ImageName").Value;
            Site = element.Attribute("Site").Value;
            //Id
            if (xElement.Attribute("Id") != null)
            {
                Id = xElement.Attribute("Id").Value;
            }
            else
            {
                Id = element.Attribute("Id").Value;
            }
            //Name
            if (xElement.Attribute("Name") != null)
            {
                Name = xElement.Attribute("Name").Value;
            }
            //RegisterSite
            if (element.Attribute("RegisterSite") == null)
            {
                RegisterSite = xElement.Attribute("RegisterSite").Value;
            }
            else
            {
                RegisterSite = element.Attribute("RegisterSite").Value;
            }
            //ClickOperation
            if (element.Attribute("ClickOperation") == null)
            {
                ClickOperation = xElement.Attribute("ClickOperation") == null ? "" : xElement.Attribute("ClickOperation").Value;
            }
            else
            {
                ClickOperation = element.Attribute("ClickOperation").Value;
            }
            BeginGroup = Boolean.Parse(xElement.Attribute("BeginGroup").Value);
            Type = (MenuItemType)Enum.Parse(typeof(MenuItemType), xElement.Attribute("Type").Value);
            Tag = xElement.Attribute("Tag").Value;
            if (element.Attribute("Enabled") != null)
            {
                Enabled = bool.Parse(element.Attribute("Enabled").Value);
            }
            else
            {
                Enabled = true;
            }

            var visibleElement = xElement.Attribute("Visible");
            if (visibleElement == null)
                Visible = true;
            else
            {
                bool _visible;
                bool.TryParse(visibleElement.Value, out _visible);
                Visible = _visible;
            }

            var widthElement = xElement.Attribute("Width");
            if (widthElement == null)
            {
                Width = 0;
            }
            else
            {
                int _width = 0;
                int.TryParse(widthElement.Value, out _width);

                Width = _width;
            }
        }
    }

}
