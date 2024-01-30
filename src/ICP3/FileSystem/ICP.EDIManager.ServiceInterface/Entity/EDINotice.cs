#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/16 星期一 10:20:00
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.EDIManager.ServiceInterface.Entity;
using System;

namespace ICP.EDIManager.ServiceInterface
{
    /// <summary>
    /// EDI通知对象
    /// </summary>
    [Serializable]
    public class EDINotice
    {
        /// <summary>
        /// EDI 发送ID
        /// </summary>
        public Guid SendID { get; set; }
        /// <summary>
        /// EDI 发送主题
        /// </summary>
        public string SendSubject { get; set; }

        /// <summary>
        /// EDI发送时间
        /// </summary>
        public DateTime SendDate { get; set; }

        /// <summary>
        /// EDI 类型
        /// </summary>
        public string EDIType { get; set; }

        /// <summary>
        /// EDI 发送人
        /// </summary>
        public string Sender { get; set; }
        /// <summary>
        /// 通知邮件地址
        /// </summary>
        public string NoticeEmail { get; set; }
        /// <summary>
        /// 是否通知成功
        /// </summary>
        public bool IsSuccess { get; set; }
        
    }
}
