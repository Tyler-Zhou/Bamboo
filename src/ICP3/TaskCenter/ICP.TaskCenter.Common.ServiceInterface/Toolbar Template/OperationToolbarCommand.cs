using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using System.Xml.Linq;

namespace ICP.MailCenter.Business.ServiceInterface
{
 
        /// <summary>
        /// 工具栏项类
        /// </summary>
        public class OperationToolbarCommand:ITemplateItemData
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
            /// 是否有权限
            /// </summary>
            public bool HasPermission { get; set; }

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
                this.Text = text;
                this.Name = name;
                this.Site = site.ToLower();
                this.RegisterSite = registerSite.ToLower();
                this.Type = (MenuItemType)Enum.Parse(typeof(MenuItemType), type, true);
                this.ImageName = imageName;
            }
            public OperationToolbarCommand()
            {
                HasPermission = true;
            }
            #endregion


            #region ITemplateItemData 成员

            public ITemplateItemData Init(XElement element)
            {

                ReadOrdinaryPropertyValues(element);
                ReadText(element);
                return this;
            }

            #endregion
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
                this.BeginGroup = Boolean.Parse(element.Attribute("BeginGroup").Value);
                
                this.Type = (MenuItemType)Enum.Parse(typeof(MenuItemType), element.Attribute("Type").Value);
                this.Tag = element.Attribute("Tag").Value;
            }

        }
    
}
