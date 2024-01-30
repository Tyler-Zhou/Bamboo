
namespace ICP.MailCenter.Business.ServiceInterface.Internal_Mail
{
    /// <summary>
    /// 内部邮件链接业务面板基实体类
    /// </summary>
    public class BaseOperationInfo
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNO { get; set; }
        /// <summary>
        /// SO NO
        /// </summary>
        public string SONO { get; set; }
        /// <summary>
        /// 预付
        /// </summary>
        public string POL { get; set; }
        /// <summary>
        /// 到付
        /// </summary>
        public string POD { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 承运人
        /// </summary>
        public string Carrier { get; set; }
    }
}
