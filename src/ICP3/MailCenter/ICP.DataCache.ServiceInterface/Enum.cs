using System;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.DataCache.ServiceInterface
{

    /// <summary>
    /// 邮件来源类型
    /// </summary>
    [Flags]
    public enum EmailSourceType
    {
        /// <summary>
        /// 未知
        /// </summary>
        [MemberDescription("未知", "Unknown")]
        Unknown = 0,
        /// <summary>
        /// 客户
        /// </summary>
        [MemberDescription("客户", "Customer")]
        Customer = 1,
        /// <summary>
        /// 船东
        /// </summary>
        [MemberDescription("船东", "Shipper")]
        Shipper = 2,
        /// <summary>
        /// 同事
        /// </summary>
        [MemberDescription("同事", "Colleague")]
        Colleague = 4,
        /// <summary>
        /// 代理
        /// </summary>
        [MemberDescription("代理", "Agent")]
        Agent = 8

    }

    /// <summary>
    ///文档内容类型
    /// </summary>
    public enum ContentType
    {
        Html,
        Content
    }

   

    /// <summary>
    /// 文档类型
    /// </summary>
    public enum FileType
    {
        Word,
        Excel,
        PowerPoint,
        Text,
        Pdf,
        Image,
        Html,
        Media,
        msg
    }
    

    
    /// <summary>
    /// 文档回调通知类型
    /// </summary>
    public enum NotifyType
    {
        Add,
        Update,
        Delete,
        Dispatched,
        Accepted,
        UnAccepted,
        AssignTo,
        Error,
        Download,
        Sucessed,
        Failed
    }

    /// <summary>
    /// 文档接受者类型
    /// </summary>
    public enum RecipientType
    {
        /// <summary>
        ///外部代理 
        /// </summary>
        External = 1,
        /// <summary>
        /// 内部代理
        /// </summary>
        Branch,
        /// <summary>
        /// 海外客服
        /// </summary>
        OverseasCS,

    }

    
    

    

    /// <summary>
    /// 上传附件选择工作类型
    /// </summary>
    public enum SelectionType
    {
        /// <summary>
        /// 上传附件
        /// </summary>
        [MemberDescription("指定类型:", "Specify Type:")]
        Normal = 0,
        /// <summary>
        /// 上传SI
        /// </summary>
        [MemberDescription("上传SI补料", "Upload SI Stuff")]
        SI,
        /// <summary>
        ///上传SO 
        /// </summary>
        [MemberDescription("上传SO附件", "Upload SO Copy")]
        SO,
        /// <summary>
        /// 上传AN
        /// </summary>
        [MemberDescription("上传A/N附件", "Upload A/N Copy")]
        AN,
        /// <summary>
        /// 上传MBL
        /// </summary>
        [MemberDescription("上传MBL附件", "Upload MBL Copy")]
        MBL,
        /// <summary>
        /// 应付款
        /// </summary>
        [MemberDescription("上传A/P附件", "Upload A/P Copy")]
        AP
    }

    /// <summary>
    /// 上传附件方式
    /// </summary>
    public enum UploadWay
    {
        [MemberDescription("上下文菜单打开上传")]
        DirectOpen,
        [MemberDescription("桌面拖拽或从邮件中心附件拖拽")]
        DragDrop,
    }

    /// <summary>
    /// 关联类型
    /// </summary>
    public enum AssociateType
    {
        /// <summary>
        /// 一般关联
        /// </summary>
        [MemberDescription("一般关联", "general relation")]
        Normal = 0,
        /// <summary>
        /// 关联并设置为客户邮件
        /// </summary>
        [MemberDescription("关联并设置为客户邮件", "Associate As Customer Mail")]
        AsCustomer = 1,
        /// <summary>
        /// 关联并设置为承运人邮件
        /// </summary>
        [MemberDescription("关联并设置为承运人邮件", "Associate As Carrier Mail")]
        AsCarrier = 2,
        /// <summary>
        /// 关联并设为SO阶段
        /// </summary>
        [MemberDescription("关联并设为SO阶段", "Associate and set as stage SO")]
        WithStageSO=3,
        /// <summary>
        /// 关联并设为SI阶段
        /// </summary>
        [MemberDescription("关联并设为SI阶段", "Associate and set as stage SI")]
        WithStageSI=4
    }
}
