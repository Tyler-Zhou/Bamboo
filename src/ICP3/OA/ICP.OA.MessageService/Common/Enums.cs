using System;
using System.Xml.Serialization;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 消息类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum MessageType
    {
        Email = 1,//邮件消息
        Fax,      //传真消息
        EDI,      //普通EDI消息
        SOEDI,    //订舱EDI消息
        SIEDI,    //补料EDI消息
        VGMEDI,   //VGMEDI消息
        AMSEDI    //AMSEDI消息
    }
    /// <summary>
    /// 消息状态
    /// </summary>
    [Flags]
    [Serializable]
    public enum MessageState
    {
        /// <summary>
        /// 发送中
        /// </summary>
        Sending = 1,
        /// <summary>
        /// 发送成功
        /// </summary>
        Success,
        /// <summary>
        /// 发送失败
        /// </summary>
        Failure,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft,
        /// <summary>
        /// 已发送
        /// </summary>
        Transmitted,
        // <summary>
        /// EDI失败
        /// </summary>
        EdiFailure,
        /// <summary>
        /// EDI成功 
        /// </summary>
        EdiSuccess,
        /// <summary>
        /// 船东认可 
        /// </summary>
        CarrierAccepted,
    }
    /// <summary>
    /// 邮件方向
    /// </summary>
    [Flags]
    [Serializable]
    public enum MessageWay
    {
        Send = 1,
        Receive = 2
    }
    /// <summary>
    /// 标记状态（1:未读,2:已读,3:答复,4:转发）
    /// </summary>
    [Flags]
    [Serializable]
    public enum MessageFlag
    {
        /// <summary>
        /// 未读
        /// </summary>
        [XmlEnum(Name = "1")]
        UnRead = 1,

        /// <summary>
        /// 已读
        /// </summary>
        [XmlEnum(Name = "2")]
        Read = 2,

        /// <summary>
        /// 答复
        /// </summary>
        [XmlEnum(Name = "3")]
        Reply = 3,

        /// <summary>
        /// 转发
        /// </summary>
        [XmlEnum(Name = "4")]
        Transfer = 4,


    }
    /// <summary>
    /// 重要性
    /// </summary>
    [Flags]
    [Serializable]
    public enum MessagePriority
    {
        /// <summary>
        /// 普通
        /// </summary>
        [XmlEnum(Name = "1")]
        Normal = 1,

        /// <summary>
        /// 高
        /// </summary>
        [XmlEnum(Name = "2")]
        High = 2,

        /// <summary>
        /// 低
        /// </summary>
        [XmlEnum(Name = "3")]
        Low = 3
    }

    /// <summary>
    /// 消息关联类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum MessageRelationType
    {
        /// <summary>
        /// 系统自动关联
        /// </summary>
        Auto = 0,
        /// <summary>
        /// 用户手动关联
        /// </summary>
        Hand,
    }

    /// <summary>
    /// 更改数据类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum UpdateDataType
    {
        /// <summary>
        /// 新增
        /// </summary>
        [MemberDescription("直接新增")]
        AddNew = 1,
        /// <summary>
        /// 重新保存一票业务的所有关联信息（暂未用到）
        /// </summary>
        [MemberDescription("以OperationID为主，删除该业务的所有关联信息记录后后，重新添加关联信息")]
        MainForOperationID = 2,
        /// <summary>
        /// 重新保存一封邮件的所有关联信息 
        /// </summary>
        [MemberDescription("以MessageID为主，删除该邮件的所有关联信息记录后，重新添加关联信息")]
        MainForMessageID = 3,
    }

    /// <summary>
    /// 邮件附件类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum MailAttachmentType
    {
        olByValue,
        olByReference,
        olEmbeddeditem,
        olOLE
    }
    /// <summary>
    /// 邮件正本内容类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum BodyFormat
    {
        olFormatUnspecified = 1,
        olFormatPlain,
        olFormatHTML,
        olFormatRichText
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags]
    [Serializable]
    public enum ReceiveFaxState
    {
        /// <summary>
        /// 接收
        /// </summary>
        [MemberDescription("接收", "Received")]
        Received = 1,
        /// <summary>
        /// 打回
        /// </summary>
        [MemberDescription("打回", "Return")]
        Return = 2
    }

    /// <summary>
    /// 类型（1:草稿,2:收件箱,3:发件箱,4已发送邮件,5:已删除邮件,6:其它）
    /// </summary>
    [Flags]
    [Serializable]
    public enum MessageFolderType
    {

        /// <summary>
        /// 系统
        /// </summary>
        [MemberDescription("系统", "System")]
        System = 1,

        /// <summary>
        /// 用户
        /// </summary>
        [MemberDescription("用户", "User")]
        User = 2,

        /// <summary>
        /// 草稿
        /// </summary>
        [MemberDescription("草稿", "Drafts")]
        Drafts = 3,

        /// <summary>
        /// 收件箱
        /// </summary>
        [MemberDescription("收件箱", "Inbox")]
        Inbox = 4,

        /// <summary>
        /// 发件箱
        /// </summary>
        [MemberDescription("发件箱", "Outbox")]
        Outbox = 5,

        /// <summary>
        /// 已发送邮件 
        /// </summary>
        [MemberDescription("已发送邮件", "Sent Items")]
        Sended = 6,
        /// <summary>
        /// 已删除邮件 
        /// </summary>
        [MemberDescription("已删除邮件", "Deleted Items")]
        Deleted = 7,

        /// <summary>
        /// 用户自定义 
        /// </summary>
        [MemberDescription("用户自定义", "User Defined")]
        UserDefined = 8
    }

}
