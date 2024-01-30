using System;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 规则序列化辅助类
    /// </summary>
    public class RuleSetSerializer
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">序列到指定的文件名</param>
        /// <returns></returns>
        public static string SerializeToFile<T>(T t,string file)
        {
            using (TextWriter tw = new StreamWriter(file,false))
            {
                using (XmlWriter sw = WFHelpers.CreateXmlWriter(tw))
                {
                    XmlSerializer xz = new XmlSerializer(typeof(T));
                    xz.Serialize(sw, t);
                    return sw.ToString();
                }
            }
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="s">对象序列化后的文件</param>
        /// <returns></returns>
        public static object DeserializeFromFile(Type type, string file)
        {
            using (TextReader reader =new StreamReader(file))
            {
                using (XmlReader reader2 = WFHelpers.CreateXmlReader(reader))
                {
                    XmlSerializer xz = new XmlSerializer(type);
                    return xz.Deserialize(reader2);
                }
            }
        }


        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="s">对象序列化后的文件</param>
        /// <returns></returns>
        public static object DeserializeFromString(Type type, string content)
        {
            if (string.IsNullOrEmpty(content)) return null;

            using (TextReader reader = new StringReader(content))
            {
                using (XmlReader reader2 = WFHelpers.CreateXmlReader(reader))
                {
                    XmlSerializer xz = new XmlSerializer(type);
                    return xz.Deserialize(reader2);
                }
            }
        }
    }

}
