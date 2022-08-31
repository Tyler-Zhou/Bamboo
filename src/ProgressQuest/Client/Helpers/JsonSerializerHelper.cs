using Newtonsoft.Json;
using System;

namespace Client.Helpers
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
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value)
        {
            T result;
            try
            {
                result = JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings { Formatting = Formatting.Indented });
                if (result == null)
                    throw new Exception("结果为Null");
            }
            catch (Exception ex)
            {
                throw new Exception($"反序列化失败{ex.Message}");
            }
            return result;
        }
    }
}
