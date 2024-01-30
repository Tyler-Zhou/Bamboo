using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Test
{
    /// <summary>
    /// ICP自动测试接口
    /// </summary>
    public interface IAutoTestServiceInterface
    {

        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        void AutoTestSetMessage(string message);
        /// <summary>
        /// 设置错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        void AutoTestSetError(string message);
        /// <summary>
        /// 设置预警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
         void AutoTestSetWarning(string message);
        /// <summary>
        /// 执行下一个案例
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
       void AutoTestNextCase();
    }
}
