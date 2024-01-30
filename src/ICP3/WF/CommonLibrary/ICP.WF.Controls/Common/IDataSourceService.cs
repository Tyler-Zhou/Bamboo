//-----------------------------------------------------------------------
// <copyright file="IDataSourceService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    /// <summary>
    /// 邦定子数据源的时候必须实现该接口
    /// </summary>
    public interface IDataSourceService
    {
        /// <summary>
        /// 对应的数据源
        /// </summary>
        object DataTable { get; set; }

        /// <summary>
        /// 表名 
        /// </summary>
        string DataTableName { get;  }
    }
}
