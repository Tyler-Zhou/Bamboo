using System;
using System.Data;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 要生成表的控件，必须实现该接口
    /// </summary>
    public interface ITable
    {
        DataTable BuildTable();
    }

    /// <summary>
    /// 生成表列的控件，必须实现该接口
    /// </summary>
    public interface IColumn
    {
        /// <summary>
        /// 列名
        /// </summary>
        string ColumnName { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        Type ColumnType { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
        int MaxLength { get; set; }

        /// <summary>
        /// 是否允许为null
        /// </summary>
        bool AllowNull { get; set; }

        /// <summary>
        /// 列标题
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        FieldType FiledType { get; set; }
    }
}
