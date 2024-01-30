#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/25 11:16:49
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 限制属性是否序列化
    /// </summary>
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        private string[] props = null;
        /// <summary>
        /// 是否保留字段
        /// </summary>
        private bool retain;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramProps">传入的属性数组</param>
        /// <param name="paramRetain">true:表示paramProps是需要保留的字段  false：表示paramProps是要排除的字段</param>
        public LimitPropsContractResolver(string[] paramProps, bool paramRetain = true)
        {
            //指定要序列化属性的清单
            props = paramProps;

            retain = paramRetain;
        }
        /// <summary>
        /// 重写创建属性方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override IList<JsonProperty> CreateProperties(Type type,

            MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list =
                base.CreateProperties(type, memberSerialization);
            //只保留清单有列出的属性
            return list.Where(p =>
            {
                if (retain)
                {
                    return props.Contains(p.PropertyName);
                }
                return !props.Contains(p.PropertyName);
            }).ToList();
        }
    }
}