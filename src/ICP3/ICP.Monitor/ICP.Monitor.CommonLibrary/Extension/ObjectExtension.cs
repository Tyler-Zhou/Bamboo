#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/23 17:38:05
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.Monitor.Common.Extension
{
    /// <summary>
    /// 对象类型扩展
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 对象是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj != null;
        }
        /// <summary>
        /// 如果对象为空则设置为默认对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T IfNull<T>(this T value, T defaultValue)
        {
            if (value.IsNull() || Convert.IsDBNull(value))
                return defaultValue;
            return value;
        }
    }
}
