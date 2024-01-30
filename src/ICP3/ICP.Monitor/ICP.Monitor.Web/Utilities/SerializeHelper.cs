#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/19 9:28:14
 *
 * Description:
 *         ->对象序列化辅助类
 *
 * History:
 *         ->
 */
#endregion

using System.IO;
using System.Runtime.Serialization.Json;

namespace ICP.Monitor.Web.Utilities
{
    /// <summary>
    /// 对象序列化辅助类
    /// </summary>
    public class SerializeHelper
    {
        /// <summary>
        /// 对象序列化辅助实例
        /// </summary>
        public readonly static SerializeHelper Instance = new SerializeHelper();

        /// <summary>
        /// 将实体对象转化为JSON数据
        /// </summary>
        /// <param name="obj">要转化的数据实体</param>
        /// <returns>JSON格式字符串</returns>
        public string JsonSerialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            string resultStr = sr.ReadToEnd();
            sr.Close();
            stream.Close();

            return resultStr;
        }

        /// <summary>
        /// 将JSON数据转化为实体对象
        /// </summary>
        /// <param name="json">符合JSON格式的字符串</param>
        /// <returns>T类型的对象</returns>
        public T JsonDeserialize<T>(string json)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(json.ToCharArray()));
            T obj = (T)serializer.ReadObject(ms);
            ms.Close();

            return obj;
        }

    }
}