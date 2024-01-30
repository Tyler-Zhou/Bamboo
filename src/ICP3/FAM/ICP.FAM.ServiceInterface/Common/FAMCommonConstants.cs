#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/29 星期日 16:07:36
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class FAMCommonConstants
    {
        /// <summary>
        /// 是否发送催款通知
        /// </summary>
        public const string TASKSCHEDULE_NOTICEAR = "SendNoticeAR";

        /// <summary>
        ///发送催款通知启动延迟时间
        /// </summary>
        public const string TASKSCHEDULE_NOTICEAR_DELAYED = "NoticeARDelayed";
        /// <summary>
        /// 发送催款通知间隔时间
        /// </summary>
        public const string TASKSCHEDULE_NOTICEAR_INTERVAL = "NoticeARInterval";

    }
}
