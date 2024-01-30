
//-----------------------------------------------------------------------
// <copyright file="IDesign.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ICP.WF.WorkFlowDesigner
{
    using System.ComponentModel.Design;

    /// <summary>
    /// 属性面版接口
    /// </summary>
    public interface IWorkProperty 
    {
        object SelectedObject { get; set; }

        IDesignerHost CurrentDesignerHost { get; set; }
    }
}
