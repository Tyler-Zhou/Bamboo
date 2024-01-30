#region Comment

/*
 * 
 * FileName:    PDFConvertEventArgs.cs
 * CreatedOn:   2014/5/19 星期一 18:00:22
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->PDF转换事件自定义传入参数
 *      ->1.查询参数:SearchParam1
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;

namespace ICP.Document
{
    /// <summary>
    /// PDF转换事件自定义传入参数
    /// </summary>
    public class PDFConvertEventArgs : EventArgs
    {
        /// <summary>
        /// FilePath
        /// </summary>
        public string FilePath { get; set; }
    }
}
