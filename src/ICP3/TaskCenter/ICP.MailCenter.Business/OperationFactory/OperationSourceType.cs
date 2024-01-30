namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 业务操作面板来自哪里类型
    /// TaskCenterOperation为来自任务中心
    /// EmailCenterOperation为来自邮件中心
    /// </summary>
    public  enum OperationSourceType
    {
        /// <summary>
        /// 任务中心
        /// </summary>
        TaskCenterOperation=0,

        /// <summary>
        /// 邮件中心
        /// </summary>
        EmailCenterOperation=1
    }
}
