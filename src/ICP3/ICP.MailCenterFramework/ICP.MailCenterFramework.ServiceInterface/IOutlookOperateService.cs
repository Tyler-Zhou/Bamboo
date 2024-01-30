#region Comment

/*
 * 
 * FileName:    IOutlookPluginService.cs
 * CreatedOn:   2015/7/30 18:17:32
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using ICP.Framework.CommonLibrary.Attributes;
using System.Data;
using System.ServiceModel;

namespace ICP.MailCenterFramework.ServiceInterface
{
    /// <summary>
    /// Outlook 插件公开服务方法
    /// </summary>
    [EmailCenterServiceHost]
    [ServiceContract]
    public interface IOutlookOperateService
    {
        /// <summary>
        /// 从ICP推送关联信息到内存变量
        /// </summary>
        /// <param name="dt">关联信息Table</param>
        [OperationContract]
        void SearchOperationView(DataTable dt);

        /// <summary>
        /// 获取邮件另存目录
        /// </summary>
        /// <param name="strEntryID">邮件EntryID</param>
        /// <param name="strMessageID">邮件MessageID</param>
        /// <param name="strIMessageID">消息Guid</param>
        /// <returns>另存目录</returns>
        [OperationContract]
        string GetMailItemSaveAsPath(string strEntryID, string strMessageID, string strIMessageID);

        /// <summary>
        /// 是否当前登录用户
        /// </summary>
        /// <param name="loginName">当前登录用户名</param>
        /// <returns></returns>
        [OperationContract]
        bool IsCurrentLoginUser(string loginName);
    }
}
