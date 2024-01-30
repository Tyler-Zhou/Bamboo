#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/26 11:21:31
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 短日期格式(yyyy-MM-dd)
    /// </summary>
    public sealed class ShortDateConverter : DateTimeConverterBase
    {
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };
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
