using System;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Reflection;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 消息与EDI信息关联类
    /// </summary>
    [Serializable]
    public class MessageEDILogRelation : IXmlSerializable
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 消息Id
        /// </summary>
        public Guid IMessageId { get; set; }
        /// <summary>
        /// EDIConfig关联Id
        /// </summary>
        public Guid EDIConfigId { get; set; }
        /// <summary>
        /// EDI标记
        /// </summary>
        public EDIFlagType Flag { get; set; }
        /// <summary>
        /// EDI类型
        /// </summary>
        public EDIMode Type { get; set; }
        public string TypeDescription { get; set; }

        #region IXmlSerializable 成员

        public XmlSchema GetSchema()
        {
            return null;
        }


        public PropertyInfo[] GetPropertyInfos()
        {
            return GetType().GetProperties();
        }


        public void ReadXml(XmlReader reader)
        {
            string id = reader.GetAttribute("Id");
            if (!string.IsNullOrEmpty(id))
            {
                Id = new Guid(id);
            }

            TypeDescription = reader.GetAttribute("TypeDescription");
            if (!string.IsNullOrEmpty(reader.GetAttribute("IMessageId")))
            {
                IMessageId = new Guid(reader.GetAttribute("IMessageId"));
            }
            if (!string.IsNullOrEmpty(reader.GetAttribute("EDIConfigId")))
            {
                EDIConfigId = new Guid(reader.GetAttribute("EDIConfigId"));
            }

            Flag = (EDIFlagType)Enum.Parse(typeof(EDIFlagType), reader.GetAttribute("Flag"));
            Type = (EDIMode)Enum.Parse(typeof(EDIMode), reader.GetAttribute("Type"));
            reader.Skip();
        }

        public void WriteXml(XmlWriter writer)
        {
            PropertyInfo[] propertyInfos = GetPropertyInfos();
            foreach (PropertyInfo property in propertyInfos)
            {
                object value = property.GetValue(this, null);
                writer.WriteAttributeString(property.Name, value == null ? string.Empty : value.ToString());
            }
        }

        public XElement GetXmlDataNode()
        {
            XElement element = new XElement("edilogrelation");
            PropertyInfo[] propertyInfos = GetPropertyInfos();
            Array.ForEach(propertyInfos, p =>
                {
                    object value = p.GetValue(this, null);
                    if (value != null)
                    {
                        if (p.PropertyType == typeof(EDIFlagType))
                            value = Flag.GetHashCode();
                        if (p.PropertyType == typeof(EDITypeByCarrier))
                            value = Type.GetHashCode();

                        XAttribute nameAttribute = new XAttribute(p.Name, value);
                        element.Add(nameAttribute);
                    }
                });
            return element;
        }

        #endregion
    }
}
