using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.DataCache.ServiceInterface1
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
    /// 文档类型
    /// </summary>
    public enum DocumentType
    {
        [MemberDescription("订舱", "SO")]
        SO = 1,
        [MemberDescription("拖车", "Truck")]
        TRK,
        [MemberDescription("报关", "Customs")]
        CF,
        [MemberDescription("补料", "SI")]
        SI,
        [MemberDescription("账单", "Bill")]
        BILL,
        [MemberDescription("主提单", "MBL")]
        MBL,
        [MemberDescription("分提单", "HBL")]
        HBL,
        [MemberDescription("本地服务", "Local Service")]
        LS,
        [MemberDescription("进口安全申报", "Importer Security Filing")]
        ISF,
        [MemberDescription("应收费用", "AP")]
        AP,
        [MemberDescription("其它", "Other")]
        Other,
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
    /// 文件上传状态
    /// </summary>
    public enum UploadState
    {
        [MemberDescription("本地处理中", "Local Processing")]
        LocalProcessing,
        [MemberDescription("本地保存中", "Local Saving")]
        LocalSaving,
        [MemberDescription("本地保存完成", "Local Saved")]
        LocalSaved,
        [MemberDescription("开始上传到服务器", "Uploading")]
        Uploading,
        [MemberDescription("上传成功", "Successed")]
        Successed,
        [MemberDescription("上传失败", "Failed")]
        Failed
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
    /// 文档状态
    /// </summary>
    public enum DocumentState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [MemberDescription("待定", "Pending")]
        Pending = 0,
        /// <summary>
        /// 已分发
        /// </summary>
        [MemberDescription("已分发", "Dispatched")]
        Dispatched = 1,
        /// <summary>
        /// 接收
        /// </summary>
        [MemberDescription("已签收", "Accepted")]
        Accepted = 2
    }

    public enum FileSource
    {
        [MemberDescription("正常上载", "From DocumentList")]
        FDocument = 1,
        [MemberDescription("来自分文件", "From Dispatch")]
        FDispatch = 2
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

}
