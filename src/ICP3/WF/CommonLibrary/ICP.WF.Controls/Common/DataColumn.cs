
//-----------------------------------------------------------------------
// <copyright file="DataColumn.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 数据列对象
    /// </summary>
    [Serializable]
    public class DataColumnItem 
    {
        /// <summary>
        /// 列名
        /// </summary>
       public string ColumnName { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
       public string ColumnType { get; set; }

        /// <summary>
        /// 最大长度
        /// </summary>
       public int MaxLength { get; set; }

        /// <summary>
        /// 是否允许为null
        /// </summary>
       public bool AllowNull { get; set; }

        /// <summary>
        /// 列标题
        /// </summary>
       public string Caption{ get; set; }
     
    }
}
