#region Comment

/*
 * 
 * FileName:    IInquireRateClientService.cs
 * CreatedOn:   2014/10/21 21:08:12
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInquireRateClientService
    {
        #region 获取主界面
        /// <summary>
        /// 获取主界面
        /// </summary>
        /// <returns></returns>
        Control GetInquireRateWorkSpace(string viewCode, string strQuery);
        #endregion
    }
}
