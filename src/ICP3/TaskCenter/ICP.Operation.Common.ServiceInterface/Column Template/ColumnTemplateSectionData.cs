using ICP.MailCenter.ServiceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 列模板
    /// </summary>
    public class ColumnTemplateSectionData : TemplateSectionData
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ITemplateItemData GetTemplateItemData()
        {
            return new BusinessColumnInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public override string GetKeyName(XElement element)
        {
            return element.Attribute(XName.Get("Name")).Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override List<XElement> GetChildElements()
        {
            return Document.Descendants(XName.Get(Name)).Descendants(XName.Get("Column")).ToList();
        }
    }
}
