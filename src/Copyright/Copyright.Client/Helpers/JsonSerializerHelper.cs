/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 9:35:24
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Copyright.Client.Helpers
{
    /// <summary>
    /// Json序列化帮助类
    /// </summary>
    public class JsonSerializerHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SerializeObject(object value)
        {
            string josnText = "";
            DataContractJsonSerializer js = new DataContractJsonSerializer(value.GetType());
            using (MemoryStream msObj = new MemoryStream())
            {
                //将序列化之后的Json格式数据写入流中
                js.WriteObject(msObj, value);
                msObj.Position = 0;
                //从0这个位置开始读取流中的数据
                using (StreamReader sr = new StreamReader(msObj, Encoding.UTF8))
                {
                    josnText = sr.ReadToEnd();
                }
            }
            return josnText;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value)
        {
            T tJSON;
            try
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(value)))
                {
                    DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(T));
                    //反序列化ReadObject
                    tJSON = (T)deseralizer.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"反序列化失败{ex.Message}");
            }
            return tJSON;
        }
    }
}
