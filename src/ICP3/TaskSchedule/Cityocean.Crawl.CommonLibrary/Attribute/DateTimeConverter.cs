#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/9/18 17:14:31
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DateTime1Converter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy/MM/dd HH:mm:ss" };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter.WriteJson(writer, value, serializer);
        }
    }
}
