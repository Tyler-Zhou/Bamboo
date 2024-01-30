#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/4 10:04:17
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.Monitor.Model.Framework
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    [Serializable]
    public class ELoginInfo : BaseDataObject
    {
        /// <summary>
        /// 用户GUID
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 用户代码(用于登录)
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 用户名称(中文)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户名称(英文)
        /// </summary>
        public string UserEName { get; set; }
        /// <summary>
        /// 用户默认邮箱
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 默认公司GUID
        /// </summary>
        public Guid DefaultCompanyID { get; set; }

        /// <summary>
        /// 默认公司名称
        /// </summary>
        public string DefaultCompanyName { get; set; }

        /// <summary>
        /// 默认部门的GUID
        /// </summary>
        public Guid DefaultDepartmentID { get; set; }

        /// <summary>
        /// 默认部门的名称
        /// </summary>
        public string DefaultDepartmentName { get; set; }

        /// <summary>
        /// 用户登录Mac地址
        /// </summary>
        public string MacAddress { get; set; }

        /// <summary>
        /// 用户登录IP地址
        /// </summary>
        public string LocalIpAddress { get; set; }

        /// <summary>
        /// 用户登录公网IP地址
        /// </summary>
        public string PublicIpAddress { get; set; }

        

        
    }
}
