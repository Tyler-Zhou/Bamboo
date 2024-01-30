#region Comment

/*
 * 
 * FileName:    BusEventArgs.cs
 * CreatedOn:   2014/5/15 星期四 16:17:08
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->业务事件自定义传入参数
 *      ->1.SearchParam1:查询参数1，No/Description
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
    /// 业务事件自定义传入参数
    /// </summary>
    public class BusEventArgs : EventArgs
    {
        /// <summary>
        /// Search Param 1
        /// </summary>
        public string SearchParam1 { get; set; }

    }
}
