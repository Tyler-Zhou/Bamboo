using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Test
{
    /// <summary>
    /// ICP自动测试帮助类
    /// </summary>
    public class ICPAutoTestHelper
    {

    }

    public class ICPAutoTest
    {
        /// <summary>
        /// 自动测试模式
        /// </summary>
        public static bool IsAutoTestMode { get; set; }

        /// <summary>
        /// 超过N秒时预警
        /// </summary>
        public static int Second { get; set; }

        /// <summary>
        /// 测试方法列表
        /// </summary>
        public static List<string> TestMethodList { get; set; }

    }
}
