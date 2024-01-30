#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/24 11:39:44
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
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 类型转换帮助类
    /// </summary>
    public static class TypeConvertHelper
    {
        #region bool To short
        /// <summary>
        /// 把bool转为short类型
        /// </summary>
        /// <param name="value">可为空bool值</param>
        /// <returns>转换后的short对象</returns>
        public static short ToShort(this bool? value)
        {
            if (!value.HasValue)
            {
                return -1;
            }
            else if (value.Value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        } 
        #endregion

        #region TimeSpan

        /// <summary>
        /// 时间间隔
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString2(this TimeSpan value)
        {
            return string.Format("{0}s", value.TotalSeconds.ToString("0.00"));
        }

        #endregion

        #region decimal[]
        /// <summary>
        /// 把decimal数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <param name="preserDigits">保留位数</param>
        /// <returns>转换后的字符串</returns>
        public static string Join(
            this decimal[] values,
            int preserDigits)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            string formart = string.Format("F{0}", preserDigits);

            StringBuilder result = new StringBuilder();

            foreach (decimal value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                result.Append(value.ToString(formart));
            }

            result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);

            return result.ToString();
        } 
        #endregion


        #region int[] Join
        /// <summary>
        /// 把Int数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <returns>转换后的字符串</returns>
        public static string Join(this int[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (int value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                result.Append(value.ToString());
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);
            }

            return result.ToString();
        } 
        #endregion

        #region short Join
        /// <summary>
        /// 把Short数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <returns>转换后的字符串</returns>
        public static string Join(this short[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (short value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                result.Append(value.ToString());
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);
            }

            return result.ToString();
        } 
        #endregion

        #region bool[] Join
        /// <summary>
        /// 把Int数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <returns>转换后的字符串</returns>
        public static string Join(this bool[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();

            foreach (bool value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                if (value)
                {
                    result.Append("1");
                }
                else
                {
                    result.Append("0");
                }
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);
            }

            return result.ToString();
        } 
        #endregion

        #region T[] Join
        /// <summary>
        /// 把Short数组转换为字符串
        /// </summary>
        /// <param name="values">Int数组</param>
        /// <returns>转换后的字符串</returns>
        public static string Join<T>(this T[] values)
        {
            if (values.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder();
            foreach (object value in values)
            {
                result.Append(CommonConstants.DIVIDED_SYMBOL);

                if (value == null)
                {
                    continue;
                }

                if (value.GetType().IsEnum)
                {
                    result.Append(((int)value));
                }
                else
                {
                    result.Append(value.ToString());
                }
            }

            if (result.Length > 0)
            {
                result = result.Remove(0, CommonConstants.DIVIDED_SYMBOL.Length);
            }

            return result.ToString();
        } 
        #endregion

        #region IList
        /// <summary>
        /// 复制实现了ICloneable对象实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        } 
        #endregion

        #region IEnumerable
        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            if (items == null)
                return null;
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => (p.GetCustomAttributes(typeof(TableFieldAttribute), false).Any())).ToArray();
            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t.IsEnum ? typeof (Int16) : t);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }
        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                return !t.IsValueType ? t : Nullable.GetUnderlyingType(t);
            }
            return t;
        }
        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        } 
        #endregion
    }
}
