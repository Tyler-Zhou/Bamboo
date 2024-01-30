
//-----------------------------------------------------------------------
// <copyright file="IValidateService.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// 验证控件数据正确性接口    /// </summary>
    public interface IValidateService
    {
        /// <summary>
        /// 验证控件输入数据的正确性(运行时)。
        /// </summary>
        /// <param name="errorTip">错误提示控件</param>
        /// <param name="errors">错误列表</param>
        /// <returns>成功返回true,失败返回false</returns>
        bool ValidateForRuntime(
            ErrorProvider errorTip,
            List<string> errors);

        /// <summary>
        /// 验证控件正确性(设计时)。
        /// </summary>
        /// <param name="errors">错误列表</param>
        /// <returns>成功返回true,失败返回false</returns>
        bool ValidateForDesign(List<string> errors);
    }
}
