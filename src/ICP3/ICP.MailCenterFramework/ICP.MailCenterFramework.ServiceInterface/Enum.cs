#region Comment

/*
 * 
 * FileName:    Enum.cs
 * CreatedOn:   2015/9/16 13:54:37
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

namespace ICP.MailCenterFramework.ServiceInterface
{
    /// <summary>
    /// 归档类型
    /// </summary>
    public enum ArchivingType
    {
        /// <summary>
        /// 筛选：过滤项
        /// </summary>
        [MemberDescription("筛选", "Filter")]
        Filter = 0,
        /// <summary>
        /// 已分发
        /// </summary>
        [MemberDescription("关联", "Relation")]
        Relation = 1,
        /// <summary>
        /// 接收
        /// </summary>
        [MemberDescription("无业务", "NoBusiness")]
        NoBusiness = 2,
    }
}
