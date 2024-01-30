
namespace ICP.MailCenter.ServiceInterface
{
    //在此定义枚举类型
    /// <summary>
    /// 文件夹操作类型
    /// </summary>
    public enum OperationFileType
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 新建
        /// </summary>
        Create,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 重命名
        /// </summary>
        Rename,
        /// <summary>
        /// 移动
        /// </summary>
        Move,
        /// <summary>
        /// 打开
        /// </summary>
        Open,
        /// <summary>
        /// 选择打开
        /// </summary>
        OpenWith,
        /// <summary>
        /// 另存为
        /// </summary>
        SaveAs,
        /// <summary>
        /// 粘贴
        /// </summary>
        Paste,
        /// <summary>
        /// 清空
        /// </summary>
        Clear,
    }
    /// <summary>
    /// 邮件接收者类型
    /// </summary>
    public enum RecipientType
    {
        /// <summary>
        /// 发件人
        /// </summary>
        Sender = 0,
        /// <summary>
        /// 接收人
        /// </summary>
        Recipient = 1,
        /// <summary>
        /// 抄送人
        /// </summary>
        CC = 2,
    }

    /// <summary>
    /// 设置时间种类
    /// </summary>
    public enum TimeType
    {
        /// <summary>
        /// 设置邮件已读时间
        /// </summary>
        MailRead,
        /// <summary>
        /// 设置接收邮件时间
        /// </summary>
        ReceiveMail,
    }


    /// <summary>
    /// 动作代码
    /// </summary>
    public enum ActionCode
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unknown,
        /// <summary>
        /// 收到客户邮件
        /// </summary>
        Customer,
        /// <summary>
        /// 收到承运人邮件 
        /// </summary>
        Carrier,
        /// <summary>
        /// 收到代理邮件
        /// </summary>
        Agent,

        /// <summary>
        /// 一般邮件
        /// </summary>
        General,
        /// <summary>
        /// 通知客户有新订单
        /// </summary>
        COD,
        /// <summary>
        /// 下达订单（业务联单）
        /// </summary>
        ODA,
        /// <summary>
        /// 通知订舱员booking已更改
        /// </summary>
        ODM,
        /// <summary>
        /// 创建订舱单
        /// </summary>
        CSO,
        /// <summary>
        /// 申请订舱
        /// </summary>
        SOA,
        /// <summary>
        /// 变更订舱
        /// </summary>
        SOM,
        /// <summary>
        /// 已向承运人发送订舱请求
        /// </summary>
        SOB,
        /// <summary>
        /// 发订舱确认给客户
        /// </summary>
        SOCfmC,
        /// <summary>
        /// 订舱成功
        /// </summary>
        SOD,
        /// <summary>
        /// 通知客户订舱成功
        /// </summary>
        SOS,
        /// <summary>
        /// 订舱失败
        /// </summary>
        SOF,
        /// <summary>
        /// 通知客户订舱失败
        /// </summary>
        SOFC,
        /// <summary>
        /// 通知业务员订舱失败
        /// </summary>
        SOFS,
        /// <summary>
        /// 已通知订舱取消
        /// </summary>
        SOAC,
        /// <summary>
        /// 订舱已取消
        /// </summary>
        SOC,
        /// <summary>
        /// 已向商务员询问应付费用
        /// </summary>
        SOCC,
        /// <summary>
        /// 商务员已回复应付费用
        /// </summary>
        SOCCD,
        /// <summary>
        /// 已向业务员要求提供应收费用
        /// </summary>
        SOCD,
        /// <summary>
        /// 业务已回复应收费用
        /// </summary>
        SOCDD,
        /// <summary>
        /// 已审批SO毛利
        /// </summary>
        SOPV,
        /// <summary>
        /// 客服要求业务员承诺此单可盈利
        /// </summary>
        SOPA,
        /// <summary>
        /// 业务员承诺此单可盈利
        /// </summary>
        SOPP,
        /// <summary>
        /// 创建拖车委托单
        /// </summary>
        CTrk,
        /// <summary>
        /// 已向拖车公司下达委托单
        /// </summary>
        TrkA,
        /// <summary>
        /// 拖车公司已收到委托单
        /// </summary>
        TrkR,
        /// <summary>
        /// 拖车公司已通知提柜信息
        /// </summary>
        TrkD,
        /// <summary>
        /// 拖车公司已更新费用
        /// </summary>
        TrkF,
        /// <summary>
        /// 已向客户通知提柜信息
        /// </summary>
        TrkS,
        /// <summary>
        /// 创建报关委托单
        /// </summary>
        CCF,
        /// <summary>
        /// 已向报关公司下达委托单
        /// </summary>
        CFA,
        /// <summary>
        /// 报关公司已收到委托单
        /// </summary>
        CFR,
        /// <summary>
        /// 报关公司已提供报关单
        /// </summary>
        CFD,
        /// <summary>
        /// 报关公司已更新费用
        /// </summary>
        CFF,
        /// <summary>
        /// 提醒文件员补料
        /// </summary>
        SIN,
        /// <summary>
        /// 已通知客户提供补料
        /// </summary>
        SINC,
        /// <summary>
        /// 已收到客户提供的补料
        /// </summary>
        SIR,
        /// <summary>
        /// HBL已创建
        /// </summary>
        CHBL,
        /// <summary>
        /// 已发HBL COPY 让客户确认
        /// </summary>
        HBLS,
        /// <summary>
        /// 已发HBL COPY 让客户确认
        /// </summary>
        HBLCfm,
        /// <summary>
        /// MBL已创建
        /// </summary>
        CMBL,
        /// <summary>
        /// 已发MBL COPY 让客户确认
        /// </summary>
        MBLS,
        /// <summary>
        /// SI发送给承运人
        /// </summary>
        MBLD,
        /// <summary>
        /// 已发MBL COPY 让客户确认
        /// </summary>
        MBLCfm,
        /// <summary>
        /// 承运人已接收MBL Copy
        /// </summary>
        MBLR,
        /// <summary>
        /// AMS is done.
        /// </summary>
        AMS,
        /// <summary>
        /// ISF is done.
        /// </summary>
        ISF,
        /// <summary>
        /// Reminder Customer Service to confirm Debit Note with customers
        /// </summary>
        DNN,
        /// <summary>
        /// Credit Note is received from the carrier
        /// </summary>
        CNR,
        /// <summary>
        /// Debit Note is confirmed with the customer
        /// </summary>
        DNCfm,
        /// <summary>
        /// Notified Debit Note to the customer[中威] for pay.
        /// </summary>
        DNA,
        /// <summary>
        /// Debit Note[#OESZGS12020374A] is paied from the customer[中威].
        /// </summary>
        DNP,
        /// <summary>
        /// 运费付讫
        /// </summary>
        CNA,
        /// <summary>
        /// Credit Note[#OESZGS12020374J] is paied to MSC.
        /// </summary>
        CNP,
        /// <summary>
        /// Ask the carrier for invoices when the Credit Note is confirmed.
        /// </summary>
        CNInv,
        /// <summary>
        /// 已申请放单
        /// </summary>
        RBLA,
        /// <summary>
        /// Release BL is approving.
        /// </summary>
        RBLH,
        /// <summary>
        ///已放单 
        /// </summary>
        RBLD,
        /// <summary>
        /// CS/Agent has received the notice of the releasing BL.
        /// </summary>
        RBLRcv,
        /// <summary>
        /// BL已放货
        /// </summary>
        BLRC,
        /// <summary>
        /// 
        /// </summary>
        OBLR,
        /// <summary>
        /// 
        /// </summary>
        OBLD,
        /// <summary>
        /// 
        /// </summary>
        TBLA,
        /// <summary>
        /// 
        /// </summary>
        TBLR,
        /// <summary>
        /// 
        /// </summary>
        TBLD,
        /// <summary>
        /// The documents are sent to the agent.
        /// </summary>
        DocS
    }



    /// <summary>
    /// 邮件操作
    /// </summary>
    public enum MailAction
    {
        Unknown,

        AddNew,

        Forword,

        Reply,

        ReplyAll,

        ReplyAllContainsAttachment
    }

    /// <summary>
    /// 操作联系人
    /// </summary>
    public enum ContactType
    {
        /// <summary>
        /// 新增一封邮件
        /// </summary>
        NewMail,
        /// <summary>
        /// 复制联系人
        /// </summary>
        CopyContact,
        /// <summary>
        /// 将联系人添加在联系人列表
        /// </summary>
        AddToContact,
        /// <summary>
        /// 以联系人作为发件人的历史记录
        /// </summary>
        SendingByContact,
        /// <summary>
        /// 以联系人作为接收人的历史记录
        /// </summary>
        ReceivingByContact,
        /// <summary>
        /// 以联系人作为接收人和发件人的历史记录
        /// </summary>
        SendingReceivingByContact,
    }

    /// <summary>
    /// 滚动条方向
    /// </summary>
    public enum ScrollBarDirection
    {
        SB_HORZ = 0,
        SB_VERT = 1,
        SB_CTL = 2,
        SB_BOTH = 3
    }

    public enum ShowWindowState
    {
        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10
    }


}
