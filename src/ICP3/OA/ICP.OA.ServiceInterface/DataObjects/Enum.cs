using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.OA.ServiceInterface.DataObjects
{




    /// <summary>
    /// 重要性 (0=高,1=普通,2=低
    /// </summary>
    public enum BulletinPriority
    {
        /// <summary>
        /// 高
        /// </summary>
        [MemberDescription("高", "High")]
        High = 0,

        /// <summary>
        /// 普通
        /// </summary>
        [MemberDescription("普通", "Normal")]
        Normal = 1,

        /// <summary>
        /// 低
        /// </summary>
        [MemberDescription("低", "Low")]
        Low = 2
    }

    /// <summary>
    /// 文档类型
    /// </summary>
    public enum OADocumentType
    {
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder = 1,

        /// <summary>
        /// 文件
        /// </summary>
        File = 2
    }

    /// <summary>
    /// 文件夹类型
    /// </summary>
    public enum FolderType
    {
        /// <summary>
        /// 根文件夹
        /// </summary>
        Root = 0,
        /// <summary>
        /// 个人
        /// </summary>
        Private = 1,

        /// <summary>
        /// 公共
        /// </summary>
        Public = 2,

        /// <summary>
        /// 邮件
        /// </summary>
        EMail = 3,

        /// <summary>
        /// 运价
        /// </summary>
        FreightRate = 4,

        /// <summary>
        /// 商务信息
        /// </summary>
        Business = 5,
        /// <summary>
        /// 订舱统计
        /// </summary>
        Booking = 6
    }

   

    /// <summary>
    /// 使用对象（对应岗位或用户）
    /// </summary>
    public enum UserObjectType
    {
        /// <summary>
        /// 岗位
        /// </summary>
        Job = 1,

        /// <summary>
        /// 用户
        /// </summary>
        User = 2
    }


    /// <summary>
    /// 文件权限
    /// </summary>
    public enum DocuentPermission
    {
        /// <summary>
        /// 查看
        /// </summary>
        View = 1,

        /// <summary>
        /// 修改
        /// </summary>
        Edit = 3,

        /// <summary>
        /// 完全控制
        /// </summary>
        Manager = 7
    }


}
