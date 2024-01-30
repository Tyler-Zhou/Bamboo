using System.Collections.Generic;
using System.Linq;
using ICP.MailCenter.ServiceInterface;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{
   public class ColumnTemplateSectionData:TemplateSectionData
    {
        public override ITemplateItemData GetTemplateItemData()
        {
            return new BusinessColumnInfo();
        }

        public override string GetKeyName(XElement element)
        {
            return element.Attribute(XName.Get("Name")).Value;
        }

        public override List<XElement> GetChildElements()
        {
            return Document.Descendants(XName.Get(Name)).Descendants(XName.Get("Column")).ToList();
        }
    }
}
