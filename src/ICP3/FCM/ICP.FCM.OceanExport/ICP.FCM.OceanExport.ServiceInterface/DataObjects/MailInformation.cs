namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects
{
    /// <summary>
    /// 订舱列表邮件使用的类
    /// </summary>
    public class MailInformation
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 邮件是否抄送
        /// </summary>
        public bool CC { get; set; }
    }
}
