using System;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;

namespace ICP.EDI.ServiceInterface.DataObjects
{
    /// <summary>
    /// 邮件日志对象
    /// </summary>
    [Serializable]
    public class LogData
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id{get;set;}

        /// <summary>
        /// 所属业务
        /// </summary>
        public Guid OwnerId { get; set; }

        /// <summary>
        /// 文档号
        /// </summary>
        public string DocumentNo { get; set; }

        /// <summary>
        /// 发送标志
        /// </summary>
        public EDIFlagType EDIFlag { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮件发送地址
        /// </summary>
        public string FromEMail { get; set; }

        /// <summary>
        /// 邮件接收地址
        /// </summary>
        public string ToEMail { get; set; }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string EMailContent { get; set; }

        /// <summary>
        /// EDI内容
        /// </summary>
        public string EDIContent { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// 发送id
        /// </summary>
        public Guid SenderId { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// EDI返回类型
        /// </summary>
        public EDIMode EDIMode
        {
            get;
            set;
        }

        /// <summary>
        /// 发送类型
        /// </summary>
        public string EDILogTypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 生成描述提示信息
        /// </summary>
        public string Description
        {
            get
            {
                return Subject + "(" + SendTime.ToString() + ")";
            }
        }

        /// <summary>
        /// EDI状态
        /// </summary>
        public MessageState State
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// 附件
        /// </summary>
        public byte[] Filebyte
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LogStatusInfo
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNO { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public EDIMode EDIType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public EDIStatus Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 邮件序号(收件箱)
        /// </summary>
        public int MailNumber { get; set; }
    }
}
