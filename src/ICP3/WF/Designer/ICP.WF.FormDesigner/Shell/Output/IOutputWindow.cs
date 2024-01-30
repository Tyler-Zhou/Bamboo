
//-----------------------------------------------------------------------
// <copyright file="IOutputWindow.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{

    /// <summary>
    /// 消息查看那面板公共接口
    /// </summary>
    public interface IOutputPart
    {
        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="text"></param>
        void Info(string message);

        /// <summary>
        /// 错误提示
        /// </summary>
        /// <param name="text"></param>
        void Error(string message);

        /// <summary>
        /// 清除所有消息
        /// </summary>
        void ClearAll();
    }
}
