
//-----------------------------------------------------------------------
// <copyright file="IDesign.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System.ComponentModel.Design;
    using System.Collections.Generic;

    /// <summary>
    /// 设计器
    /// </summary>
    public interface IDesignPart:IBasePart
    {
        void NewFormDesigner(
            string title, 
            string savePath,
            string templateFileName);

        void ShowXMLCode();

        event ActiveDesignerChangedEventHandler ActiveDesignerChanged;

        event SelectedChangedEventHandler SelectionChanged;

        void ExecuteAction(CommandID commandname);

        string FormFilePath { get; }

        string Title { get; }

        void Save();
        /// <summary>
        /// 获得错误列表
        /// </summary>
        /// <returns></returns>
        List<string> GetErrorList();
    }
}
