using System.Collections.Generic;
using System.Linq;
using ICP.MailCenter.ServiceInterface;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 工具栏模板段落类
    /// </summary>
   public class ToolbarTemplateSectionData:TemplateSectionData
    {

        public override List<XElement> GetChildElements()
        {
            return Document.Descendants(XName.Get(Name)).Descendants(XName.Get("Item")).ToList();
        }

        public override string GetKeyName(XElement element)
        {
            return element.Attribute(XName.Get("Id")).Value;
        }

        public override ITemplateItemData GetTemplateItemData()
        {
            return new OperationToolbarCommand();
        }
    }
}
