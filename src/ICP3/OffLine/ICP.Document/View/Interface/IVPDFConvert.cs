#region Comment

/*
 * 
 * FileName:    IVPDFConvert.cs
 * CreatedOn:   2014/5/19 星期一 16:43:07
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->PDF转换后呈现数据接口
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
    /// PDF转换后呈现数据接口
    /// </summary>
    public interface IVPDFConvert : IViewBase
    {
        /// <summary>
        /// 转换文件
        /// </summary>
        event EventHandler<PDFConvertEventArgs> ConvertFile;
        /// <summary>
        /// 控件加载文件
        /// </summary>
        /// <param name="paramFilePath">文件路径</param>
        void ControlLoadFile(string paramFilePath);
        /// <summary>
        /// 加载文件
        /// </summary>
        /// <param name="paramFilePath">文件路径</param>
        void LoadFile(string paramFilePath);
    }
}
