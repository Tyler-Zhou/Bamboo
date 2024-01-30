#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/25 10:43:30
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 数据库自定义表字段(SQL Custom Table Field)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class TableFieldAttribute : Attribute
    {
    }
}
