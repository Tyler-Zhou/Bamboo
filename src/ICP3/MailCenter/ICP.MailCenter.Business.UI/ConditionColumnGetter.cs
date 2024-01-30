using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.MailCenter.Business.UI
{
   public class ConditionColumnGetter
    {
       private static ListDictionary<string, string> dicConfigs = new ListDictionary<string, string>();
       static ConditionColumnGetter()
       {
           ReadConfigItems();
       }
       private static void ReadConfigItems()
       {
           string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
           string tempalteFileName = "ConditionNameToTemplateCodeConfig.xml";
           string fileFullPath = Path.Combine(fileRootDirectory, tempalteFileName);
           XDocument document = XDocument.Load(fileFullPath);
           var elements = document.Element(XName.Get("Config")).Elements().ToList();
           var items = (from element in elements
                        select new { TemplateCode = element.Attribute("TemplateCode").Value, ConditionNames = element.Attribute("ConditionNames").Value }).ToList();
           items.ForEach(item =>
           {

             
               dicConfigs[item.TemplateCode].AddRange(item.ConditionNames.Split(','));
           });
       }
       public List<string> Get(string templateCode)
       {
           return dicConfigs[templateCode];
       }
    }
}
