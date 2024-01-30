
//-----------------------------------------------------------------------
// <copyright file="ITitle.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    /// <summary>
    /// 标题接口
    /// </summary>
    public interface ITitle
    {
        /// <summary>
        /// 中文标题
        /// </summary>
        string CText { get; set; }

        /// <summary>
        /// 英文标题
        /// </summary>
        string EText { get; set; }
    }
}
