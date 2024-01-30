#region Comment

/*
 * 
 * FileName:    IVBusiness.cs
 * CreatedOn:   2014/5/14 星期三 17:37:21
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->业务信息呈现数据的接口
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;

namespace ICP.Document
{
    /// <summary>
    /// 业务信息呈现数据的接口
    /// </summary>
    public interface IVBusiness : IViewBase
    {
        /// <summary>
        /// 填充业务数据
        /// </summary>
        /// <param name="businessList"></param>
        void FillBusinessInfo(List<BusinessInfo> businessList);
        /// <summary>
        /// 查询业务数据
        /// </summary>
        event EventHandler<BusEventArgs> Search_ItemClick;
    }
}
