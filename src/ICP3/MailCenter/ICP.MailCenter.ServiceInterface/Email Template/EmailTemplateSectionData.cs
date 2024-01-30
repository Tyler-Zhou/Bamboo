using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ICP.MailCenter.ServiceInterface
{  
    /// <summary>
    /// 邮件模板段落类
    /// </summary>
    public class EmailTemplateSectionData : TemplateSectionData
    {
        public override ITemplateItemData GetTemplateItemData()
        {
            return new EmailTemplateItemData();
        }

        public override string GetKeyName(XElement element)
        {
            return element.Name.LocalName;
        }



        public override List<XElement> GetChildElements()
        {

            return Document.Descendants(XName.Get(Name)).Elements().ToList();
        }
    }
}
