#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/9/7 16:41:38
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Reflection;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 对象类型转换
    /// </summary>
    public static class ObjectConvertHelper
    {
        /// <summary>
        /// 尝试转换成Guid
        /// 异常时返回Guid.Empty
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Guid GetGuid(this object o)
        {
            try
            {
                return (Guid)o;
            }
            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        /// <summary>
        /// 通过属性名称获取属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="paramPropertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(this object obj, string paramPropertyName)
        {
            if (obj.IsNull())
                return null;
            Type t = obj.GetType();
            PropertyInfo pi = t.GetProperty(paramPropertyName);
            return pi == null ? null : pi.GetValue(obj, null);
        }

        /// <summary>
        /// 通过属性名称获取属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="paramPropertyName"></param>
        /// <param name="paramPropertyValue"></param>
        /// <returns></returns>
        public static void SetPropertyValue(this object obj, string paramPropertyName, string paramPropertyValue)
        {
            if (obj.IsNull())
                return;
            Type entityType = obj.GetType();

            PropertyInfo propertyInfo = entityType.GetProperty(paramPropertyName);

            if (IsType(propertyInfo.PropertyType, "System.String"))
            {
                propertyInfo.SetValue(obj, paramPropertyValue, null);

            }

            if (IsType(propertyInfo.PropertyType, "System.Boolean"))
            {
                propertyInfo.SetValue(obj, Boolean.Parse(paramPropertyValue), null);

            }

            if (IsType(propertyInfo.PropertyType, "System.Int32"))
            {
                propertyInfo.SetValue(obj, !paramPropertyValue.IsNullOrEmpty() ? int.Parse(paramPropertyValue) : 0, null);
            }

            if (IsType(propertyInfo.PropertyType, "System.Decimal"))
            {
                propertyInfo.SetValue(obj, !paramPropertyValue.IsNullOrEmpty() ? Decimal.Parse(paramPropertyValue) : new Decimal(0), null);
            }

            if (IsType(propertyInfo.PropertyType, "System.Nullable`1[System.DateTime]"))
            {
                if (paramPropertyValue != "")
                {
                    try
                    {
                        propertyInfo.SetValue(
                            obj,
                            (DateTime?)DateTime.ParseExact(paramPropertyValue, "yyyy-MM-dd HH:mm:ss", null), null);
                    }
                    catch
                    {
                        propertyInfo.SetValue(obj, (DateTime?)DateTime.ParseExact(paramPropertyValue, "yyyy-MM-dd", null), null);
                    }
                }
                else
                    propertyInfo.SetValue(obj, null, null);

            }
        }

        /// <summary>
        /// 类型匹配
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsType(Type type, string typeName)
        {
            if (type.ToString() == typeName)
                return true;
            if (type.ToString() == "System.Object")
                return false;

            return IsType(type.BaseType, typeName);
        }
        /// <summary>
        /// Object 转 字符串型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string TryToString(this object obj)
        {
            return obj.IsNull() ? "" : obj.ToString();
        }
        /// <summary>
        /// 尝试转换成DateTime
        /// 异常时返回DateTime.MinValue
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            try
            {
                return (DateTime)obj;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
