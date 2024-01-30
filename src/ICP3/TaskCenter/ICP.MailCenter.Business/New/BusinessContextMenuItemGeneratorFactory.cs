using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using System.IO;
using System.Xml.Linq;
using Microsoft.Practices.CompositeUI;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 业务上下文菜单项产生器工厂
    /// </summary>
   public class BusinessContextMenuItemGeneratorFactory
    {  
       [ServiceDependency]
       public WorkItem RootWorkItem
       {
           get;
           set;
       }
       static BusinessContextMenuItemGeneratorFactory()
       {
           ReadConfigItems();
       }
       private static Dictionary<OperationType, IBusinessContextMenuItemGenerator> dicGenerators = new Dictionary<OperationType, IBusinessContextMenuItemGenerator>();
       private static Dictionary<string, string> configItems;
       private static void ReadConfigItems()
       {
           string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
           string tempalteFileName = "BusinessContextMenuItemGenerator.xml";
           string fileFullPath = Path.Combine(fileRootDirectory, tempalteFileName);
           XDocument document = XDocument.Load(fileFullPath);
           var elements = document.Element(XName.Get("Generators")).Elements().ToList();
           configItems = new Dictionary<string, string>();
           elements.ForEach(element => {
               configItems.Add(element.Attribute("OperationType").Value, element.Attribute("ServiceType").Value);
           });
       }
       public IBusinessContextMenuItemGenerator Get(OperationType operationType)
       {
           if (dicGenerators.ContainsKey(operationType))
               return dicGenerators[operationType];
           string typeName = configItems[operationType.ToString()];
           Type type=Type.GetType(typeName);
           IBusinessContextMenuItemGenerator generator = this.RootWorkItem.Items.AddNew(type) as IBusinessContextMenuItemGenerator;
           dicGenerators.Add(operationType, generator);
           return generator;

       
       }
    }
}
