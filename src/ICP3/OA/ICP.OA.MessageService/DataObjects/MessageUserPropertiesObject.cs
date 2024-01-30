using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Common;
using System.Xml.Serialization;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 消息自定义属性类
    /// </summary>
    [Serializable]
    public class MessageUserPropertiesObject : IDictionary<string, object>, IDisposable, IXmlSerializable
    {
        private Dictionary<string, object> items = new Dictionary<string, object>();
        private string operationIdTag = "OperationId";
        private string operationTypeTag = "OperationType";
        private string formTypeTag = "FormType";
        private string formIdTag = "FormId";
        private string ediLogTag = "EDILog";
        private string actionTag = "Action";
        private string customerServiceTag = "customerService";
        private string eventTag = "Event";

        private string referenceTag = "reference";

        private string templateCodeTag = "TemplateCode";

        private string operationNoTag = "OperationNO";
        private string operationRelationIDTag = "OperationRelationID";
        private string contactStageTag = "ContactStage";
        /// <summary>
        /// 消息在业务处理过程中所处的阶段
        /// </summary>
        public string ContactStage
        {
            get
            {
                return InnerGet(contactStageTag);
            }
            set
            {
                items[contactStageTag] = value;
            }
        }
        public string OperationNO
        {
            get
            {
                return InnerGet(operationNoTag);
            }
            set
            {
                items[operationNoTag] = value;
            }
        }
        /// <summary>
        /// 消息与业务关联ID
        /// </summary>
        public Guid OperationRelationID
        {
            get
            {
                string temp = InnerGet(operationRelationIDTag);
                if (string.IsNullOrEmpty(temp))
                {
                    return Guid.Empty;
                }
                return new Guid(temp);
            }
            set
            {
                items[operationRelationIDTag] = value;
            }
        }

        public string TemplateCode
        {
            get
            {
                return InnerGet(templateCodeTag);
            }
            set
            {
                items[templateCodeTag] = value;
            }
        }
        private string InnerGet(string tag)
        {
            if (items.ContainsKey(tag))
            {
                return items[tag].ToString();
            }
            else
                return string.Empty;
        }
        /// <summary>
        /// 邮件MailItem之间关联的Reference属性值
        /// </summary>
        public string Reference
        {
            get
            {
                return InnerGet(referenceTag);
            }
            set
            {
                items[referenceTag] = value;
            }
        }
        /// <summary>
        /// 客服Email地址
        /// </summary>
        public string CustomerService
        {
            get
            {
                return InnerGet(customerServiceTag);
            }
            set
            {
                items[customerServiceTag] = value;
            }
        }
        /// <summary>
        /// 业务动作
        /// </summary>
        public string Action
        {
            get
            {
                return InnerGet(actionTag);
            }
            set
            {
                items[actionTag] = value;
            }
        }
        public Guid OperationId
        {
            get
            {
                string operationId = items[operationIdTag].ToString();
                if (string.IsNullOrEmpty(operationId))
                    return Guid.Empty;
                else
                    return new Guid(operationId);
            }
            set
            {

                items[operationIdTag] = value;
            }
        }

        /// <summary>
        /// 事件对象
        /// </summary>
        public EventObjects EventInfo
        {
            get
            {
                if (items.ContainsKey(eventTag))
                {
                    return items[eventTag] as EventObjects;
                }
                else
                    return null;

            }
            set
            {
                items[eventTag] = value;
            }
        }

        public OperationType OperationType
        {
            get
            {
                string operationType = items[operationTypeTag].ToString();
                if (string.IsNullOrEmpty(operationType))
                    return OperationType.Unknown;
                else
                    return (OperationType)Enum.Parse(typeof(OperationType), operationType);
            }
            set
            {
                items[operationTypeTag] = value.ToString();
            }

        }
        public FormType FormType
        {
            get
            {
                if (items.ContainsKey(formTypeTag))
                {
                    string formType = items[formTypeTag].ToString();
                    if (string.IsNullOrEmpty(formType))
                        return FormType.Unknown;
                    else
                        return (FormType)Enum.Parse(typeof(FormType), formType);
                }
                else
                    return FormType.Unknown;
            }
            set
            {
                items[formTypeTag] = value.ToString();
            }
        }

        public Guid? FormId
        {
            get
            {
                if (items.ContainsKey(formIdTag))
                {
                    string formID = items[formIdTag].ToString();
                    if (string.IsNullOrEmpty(formID))
                        return Guid.Empty;
                    else
                        return new Guid(formID);
                }
                else
                    return null;
            }
            set
            {

                items[formIdTag] = value;
            }
        }
        public MessageEDILogRelation EDILogRelation
        {
            get
            {
                if (items.ContainsKey(ediLogTag))

                    return (items[ediLogTag]) as MessageEDILogRelation;
                else
                    return null;
            }
            set
            {

                items[ediLogTag] = value;
            }
        }
        #region IDictionary<string,object> 成员

        public void Add(string key, object value)
        {
            items.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return items.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return items.Keys; }
        }

        public bool Remove(string key)
        {
            return items.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return items.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return items.Values; }
        }

        public object this[string key]
        {
            get
            {
                if (ContainsKey(key))
                    return items[key];
                else
                    return null;
            }
            set
            {
                items[key] = value; ;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<string,object>> 成员

        public void Add(KeyValuePair<string, object> item)
        {
            items.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return items.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return items.Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,object>> 成员

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            if (items != null)
            {
                items.Clear();
            }
        }
        public override string ToString()
        {
            List<string> list = new List<string>();
            foreach (var pair in items)
            {
                list.Add(string.Format("{0}={1}", pair.Key, pair.Value==null ? string.Empty : pair.Value.ToString()));
            }
            return list.Aggregate((i, j) => i + ";" + j);

        }
        public static MessageUserPropertiesObject Convert(string value)
        {
            MessageUserPropertiesObject userObject = new MessageUserPropertiesObject();
            List<string> pairs = value.Split(';').ToList();
            foreach (string pair in pairs)
            {
                if (!string.IsNullOrEmpty(pair))
                {
                    string[] keyValuePair = pair.Split('=');
                    string key = keyValuePair[0];
                    if (key.Equals("Event"))
                    {
                        EventObjects entity = keyValuePair[1].ToDeserializeXml() as EventObjects;
                        if (entity != null)
                        {
                            userObject[key] = entity;
                        }
                    }
                    else
                    {
                        string valueString = keyValuePair[1];
                        userObject[key] = valueString;
                    }

                }
            }
            return userObject;

        }
        #endregion
        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {

            return null;

        }



        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(object));
            XmlSerializer ediValueSerializer = new XmlSerializer(typeof(MessageEDILogRelation));
            XmlSerializer eventValueSerializer = new XmlSerializer(typeof(EventObjects));
            try
            {
                bool wasEmpty = reader.IsEmptyElement;
                reader.Read();

                if (wasEmpty)
                    return;

                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    reader.ReadStartElement("item");
                    reader.ReadStartElement("key");
                    string key = (string)keySerializer.Deserialize(reader);
                    reader.ReadEndElement();
                    reader.ReadStartElement("value");
                    object value;
                    if (key.Equals(ediLogTag))
                    {
                        value = ediValueSerializer.Deserialize(reader);
                    }
                    else if (key.Equals(eventTag))
                    {
                        value = eventValueSerializer.Deserialize(reader);
                    }
                    else
                    {
                        value = valueSerializer.Deserialize(reader);
                    }
                    reader.ReadEndElement();
                    Add(key, value);
                    reader.ReadEndElement();
                    reader.MoveToContent();
                }

                reader.ReadEndElement();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(ex.Message);
                throw ex;
            }
        }



        public void WriteXml(XmlWriter writer2)
        {
            //System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            //settings.ConformanceLevel = System.Xml.ConformanceLevel.Fragment;
            //settings.CloseOutput = true;
            //settings.Encoding = System.Text.Encoding.UTF8;
            //settings.Indent = true;

            // System.Xml.XmlWriter writer2 = System.Xml.XmlWriter.Create("test.xml", settings);
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));
            XmlSerializer ediValueSerializer = new XmlSerializer(typeof(MessageEDILogRelation));

            XmlSerializer valueSerializer = new XmlSerializer(typeof(object));

            XmlSerializer eventValueSerializer = new XmlSerializer(typeof(EventObjects));
            try
            {
                foreach (string key in Keys)
                {
                    writer2.WriteStartElement("item");
                    writer2.WriteStartElement("key");
                    keySerializer.Serialize(writer2, key);
                    writer2.WriteEndElement();
                    writer2.WriteStartElement("value");
                    object value = this[key];
                    if (value == null)
                    {
                        valueSerializer.Serialize(writer2, value);
                    }
                    else if (value.GetType().Equals(typeof(MessageEDILogRelation)))
                    {
                        ediValueSerializer.Serialize(writer2, value);
                    }
                    else if (value.GetType().Equals(typeof(EventObjects)))
                    {
                        eventValueSerializer.Serialize(writer2, value);
                    }
                    else
                    {
                        valueSerializer.Serialize(writer2, value);
                    }
                    writer2.WriteEndElement();
                    writer2.WriteEndElement();
                }
                //  writer2.Close();
            }
            catch (Exception ex)
            {
                LogHelper.SaveLog(ex.Message);
                throw ex;
            }
        }

        public PropertyInfo[] GetPropertyInfos()
        {
            return GetType().GetProperties();

        }

        public XElement GetXmlDataNode()
        {
            XElement element = new XElement("userproperties");
            PropertyInfo[] propertyInfos = GetPropertyInfos();
            XElement ediLogRelationElement = null;
            XElement eventObjectsElement = null;

            foreach (PropertyInfo p in propertyInfos)
            {
                if (p.PropertyType == typeof(ICollection<object>) || p.PropertyType == typeof(ICollection<string>))
                    continue;
                if (p.Name.Equals("Count") || p.Name.Equals("IsReadOnly") || p.Name.Equals("Item"))
                    continue;

                object value = p.GetValue(this, null);
                if (value != null)
                {
                    if (p.PropertyType == typeof(OperationType))
                        value = OperationType.GetHashCode();

                    else if (p.PropertyType == typeof(FormType))
                        value = FormType.GetHashCode();

                    else if (p.PropertyType == typeof(MessageEDILogRelation))
                    {
                        ediLogRelationElement = EDILogRelation.GetXmlDataNode();
                        continue;
                    }
                    else if (p.PropertyType == typeof(EventObjects))
                    {
                        eventObjectsElement = EventInfo.GetXmlDataNode();
                        continue;
                    }

                    XAttribute nameAttribute = new XAttribute(p.Name, value);
                    element.Add(nameAttribute);
                }
            }

            if (ediLogRelationElement != null)
                element.Add(ediLogRelationElement);
            if (eventObjectsElement != null)
                element.Add(eventObjectsElement);

            return element;
        }

        #endregion


    }
}
