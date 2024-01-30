
//-----------------------------------------------------------------------
// <copyright file="ICodePart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    /// <summary>
    /// 代码预览面板
    /// </summary>
    public interface ICodePart:IBasePart
    {
        /// <summary>
        /// 代码
        /// </summary>
        string Code { get; set; }
    }
}
