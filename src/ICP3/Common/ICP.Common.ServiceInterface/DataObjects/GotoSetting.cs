using System;

namespace ICP.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// Goto页面-设置页面实体
    /// </summary>
    public class GotoSetting
    {
        /// <summary>
        /// 设置常用范围
        /// </summary>
        public string SettingScope { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public Guid UserId { get; set; }
    }
}
