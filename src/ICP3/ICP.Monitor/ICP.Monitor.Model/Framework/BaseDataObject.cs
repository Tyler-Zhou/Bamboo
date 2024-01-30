#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/6 11:27:40
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICP.Monitor.Model.Framework
{
    /// <summary>
    /// 所有业务对象的基类
    /// </summary>
    [Serializable]
    public class BaseDataObject
    {
        #region Fields & Property
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
        #endregion

        #region Init
        /// <summary>
        /// 初始化业务对象
        /// 会准备所有的验证信息
        /// </summary>
        public BaseDataObject()
        {
        }
        #endregion
    }
}
