using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ICP.Operation.Common.ServiceInterface
{   
    /// <summary>
    /// 业务面板数据保存参数类
    /// 数据以键值对的形式保存
    /// </summary>
    [Serializable]
    public class BusinessSaveParameter : IXmlSerializable
    {
        public Dictionary<string, object> items = new Dictionary<string, object>();
        /// <summary>
        /// 索引实现
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public object this[string fieldName]
        {
            get
            {
                return this.items[fieldName];
            }
            set
            {
                this.items[fieldName] = value;
            }
        }

        #region IXmlSerializable 成员

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));

            XmlSerializer valueSerializer = new XmlSerializer(typeof(object));


            bool wasEmpty = reader.IsEmptyElement;

            reader.Read();



            if (wasEmpty)

                return;



            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {

                reader.ReadStartElement("item");



                reader.ReadStartElement("key");

                string key = (string)keySerializer.Deserialize(reader);

                reader.ReadEndElement();



                reader.ReadStartElement("value");
                object value= valueSerializer.Deserialize(reader);
             
                     
                

                reader.ReadEndElement();



                this.items.Add(key, value);
                reader.ReadEndElement();

                reader.MoveToContent();

            }

            reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(string));

            XmlSerializer valueSerializer = new XmlSerializer(typeof(object));



            foreach (string key in this.items.Keys)
            {

                writer.WriteStartElement("item");



                writer.WriteStartElement("key");

                keySerializer.Serialize(writer, key);

                writer.WriteEndElement();



                writer.WriteStartElement("value");

                object value = this.items[key]==DBNull.Value?string.Empty:this.items[key];



                valueSerializer.Serialize(writer, value);

                writer.WriteEndElement();



                writer.WriteEndElement();
            }
        }

        #endregion
    }
}
