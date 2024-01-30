using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务
    /// </summary>
    public class BusinessStateReadXml 
    {
        //读取模版的路径
        private readonly string _fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        //Xml的文件名称
        private const string TempalteFileName = "BusinessState.xml";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BusinessState> GetBusinessState()
        {
            List<BusinessState> businessState = new List<BusinessState>();
            //合并为完整的XML路径
            var templateFilePath = Path.Combine(_fileRootDirectory, TempalteFileName);
            var document = XDocument.Load(templateFilePath);
            //读取Item下面的所有集合信息
            var xmldocment = from ent in document.Descendants("Item") select ent;
            //读取Item段落的信息
            var xmlitem = from item in xmldocment.Descendants(XName.Get("Items"))
                          select item;

            foreach (var xElement in xmlitem)
            {
                BusinessState business = new BusinessState();
                business.Name = xElement.Attribute("Name").Value;
                business.ModifyValue = xElement.Attribute("ModifyValue").Value;
                business.OriginalValue = xElement.Attribute("OriginalValue").Value;
                business.Methods = xElement.Attribute("Methods").Value;
                business.SqlType = xElement.Attribute("SqlType").Value;
                business.OperationType = (OperationType) Enum.Parse(typeof (OperationType), xElement.Attribute("OperationType").Value);
                business.CaptionCn = xElement.Attribute("CaptionCn") == null
                                         ? string.Empty
                                         : xElement.Attribute("CaptionCn").Value;
                business.CaptionEn = xElement.Attribute("CaptionEn") == null
                                         ? string.Empty
                                         : xElement.Attribute("CaptionEn").Value;
                business.AssociatedEvent = xElement.Attribute("AssociatedEvent") == null
                                               ? string.Empty
                                               : xElement.Attribute("AssociatedEvent").Value;
                business.Permissions = xElement.Attribute("Permissions") == null
                                               ? string.Empty
                                               : xElement.Attribute("Permissions").Value;
                businessState.Add(business);
            }
            return businessState;
        }

        
    }
}
