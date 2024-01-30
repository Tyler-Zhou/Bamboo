
//-----------------------------------------------------------------------
// <copyright file="IDesign.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.WF.FormDesigner
{
    /// <summary>
    /// 属性面版接口
    /// </summary>
    public interface IPropertyPart : IBasePart
    {
        object SelectedObject { get; set; }
    }
}
