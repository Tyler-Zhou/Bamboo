

//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="ICP">
//     Copyright (c) ICP. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.WorkFlowDesigner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 命令常量
    /// </summary>
    public class CommandConstants
    {
        /// <summary>
        /// 打开设计器命令常量
        /// </summary>
        public const string Command_OpenFlowDesigner = "WF_WorkflowDesigner";

        /// <summary>
        /// 新建命令常量
        /// </summary>
        public const string Command_Flow_New = "Command_Flow_New";


        /// <summary>
        /// 打开设计器命令常量
        /// </summary>
        public const string Command_Flow_OpenLocal = "Command_Flow_OpenLocal";

        /// <summary>
        /// 打保存到本地命令常量
        /// </summary>
        public const string Command_Flow_OpenServer = "Command_Flow_OpenServer";

        /// <summary>
        /// 打开设计器命令常量
        /// </summary>
        public const string Command_Flow_SaveLocal = "Command_Flow_SaveLocal";

        /// <summary>
        /// 保存到服务器命令常量
        /// </summary>
        public const string Command_Flow_SaveServer = "Command_Flow_SaveServer";

        /// <summary>
        /// 另存为命令常量
        /// </summary>
        public const string Command_Flow_SaveAS = "Command_Flow_SaveAS";

        /// <summary>
        /// 打印预览命令常量
        /// </summary>
        public const string Command_Flow_PrintPreview = "Command_Flow_PrintPreview";

        /// <summary>
        /// 打印命令常量
        /// </summary>
        public const string Command_Flow_Print = "Command_Flow_Print";

        /// <summary>
        /// 打印设置命令常量
        /// </summary>
        public const string Command_Flow_PrintSet = "Command_Flow_PrintSet";


        /// <summary>
        /// 删除命令常量
        /// </summary>
        public const string Command_Flow_Remove = "Command_Flow_Remove";

        /// <summary>
        /// 拷贝命令常量
        /// </summary>
        public const string Command_Flow_Copy = "Command_Flow_Copy";

        /// <summary>
        /// 剪切命令常量
        /// </summary>
        public const string Command_Flow_Cut = "Command_Flow_Cut";


        /// <summary>
        /// 粘贴命令常量
        /// </summary>
        public const string Command_Flow_Paster = "Command_Flow_Paster";

        /// <summary>
        /// 放大命令常量
        /// </summary>
        public const string Command_Flow_ZoomOut = "Command_Flow_ZoomOut";

        /// <summary>
        /// 缩小命令常量
        /// </summary>
        public const string Command_Flow_ZoomIn = "Command_Flow_ZoomIn";

        /// <summary>
        /// 取消变焦命令常量
        /// </summary>
        public const string Command_Flow_CancelZaom = "Command_Flow_CancelZaom";

        /// <summary>
        /// 选择的比例发生改变命令常量
        /// </summary>
        public const string Command_Flow_ZoomLevelSelectedValueChanged = "Command_Flow_ZoomLevelSelectedValueChanged";

        /// <summary>
        /// 展开命令常量
        /// </summary>
        public const string Command_Flow_Expand = "Command_Flow_Expand";

        /// <summary>
        /// 折叠命令常量
        /// </summary>
        public const string Command_Flow_Collapse = "Command_Flow_Collapse";

        /// <summary>
        /// 关闭窗体命令常量
        /// </summary>
        public const string Command_Flow_Close = "Command_Flow_Close";

        /// <summary>
        /// 工具箱命令常量
        /// </summary>
        public const string Command_Flow_ToolBox = "Command_Flow_ToolBox";

        /// <summary>
        /// 属性栏命令常量
        /// </summary>
        public const string Command_Flow_Properties = "Command_Flow_Properties";
    }
}
