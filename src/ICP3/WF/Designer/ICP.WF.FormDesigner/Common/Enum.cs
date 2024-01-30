//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    /// <summary>
    /// 加载器类型
    /// </summary>
    public enum LoaderType
    {
        /// <summary>
        /// Form->xml或则Xml->Form
        /// </summary>
        XmlDesignerLoader = 1,

        /// <summary>
        /// Form->Code或则Code->Form
        /// </summary>
        CodeDomDesignerLoader = 2,

    }
}
