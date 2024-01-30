#region Comment

/*
 * 
 * FileName:    IViewBase.cs
 * CreatedOn:   2014/5/14 星期三 16:24:34
 * CreatedBy:   taylor
 * 
 * Description：
 *      ->呈现数据接口
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.ComponentModel;

namespace ICP.Document
{
    /// <summary>
    /// 呈现数据接口
    /// </summary>
    public interface IViewBase
    {
        /// <summary>
        /// 加载
        /// </summary>
        event EventHandler Load;
    }
}
