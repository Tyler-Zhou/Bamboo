/**
 *  创建时间:2014-07-24
 *  创建人:Joabwang    
 *  描  述:参数类
 **/
using System;
//using Microsoft.Office.Interop.Outlook;
using System.Collections.Generic;
using ICP.Message.ServiceInterface;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 参数类
    /// </summary>
    public static class Parameter
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public static Guid OperationId
        {
            get;
            set;
        }

        /// <summary>
        /// 业务号
        /// </summary>
        public static string OperationNo
        {
            get;
            set;
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        public static DateTime? UpdateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public static ICP.Framework.CommonLibrary.Common.OperationType OperationType
        {
            get;
            set;
        }


        /// <summary>
        /// 数据库视图CODE的名称
        /// </summary>
        public static string TemplateCode
        {
            get;
            set;
        }
        /// <summary>
        /// Outlook实体
        /// </summary>
        public static object OutlookItem
        {
            get;
            set;
        }
        /// <summary>
        /// 当前邮件实体
        /// </summary>
        public static Message.ServiceInterface.Message Message
        {
            get;
            set;
        }
        /// <summary>
        /// MBL,HBL号
        /// </summary>
        public static string BLNO
        {
            get;
            set;
        }
        /// <summary>
        /// 是否完成了一项工作的标识
        /// </summary>
        private static int flagFinish = 0;
        /// <summary>
        /// 是否完成了一项工作的标识
        /// 0：初始化
        /// 1:完成
        /// 2：重新查询数据
        /// </summary>
        public static int FlagFinish
        {
            get { return flagFinish; }
            set { flagFinish = value; }
        }

        /// <summary>
        /// ICP连接是否可用
        /// </summary>
        private static bool _IsEnableICPConnection = true;
        /// <summary>
        /// ICP连接是否可用
        /// </summary>
        public static bool IsEnableICPConnection
        {
            get { return _IsEnableICPConnection; }
            set { _IsEnableICPConnection = value; }
        }

        /// <summary>
        /// 方法是否执行
        /// </summary>
        public static bool Performflg
        {
            get; 
            set;
        }
        /// <summary>
        /// 记录方法开始调用时间
        /// </summary>
        public static DateTime Calledtime
        {
            get;
            set;
        }

        private static List<AttachmentContent> _MailAttachmentContents;
        /// <summary>
        /// 邮件附件信息
        /// </summary>
        public static List<AttachmentContent> MailAttachmentContents
        {
            get { return _MailAttachmentContents ?? new List<AttachmentContent>(); }
            set { _MailAttachmentContents = value; }
        }

    }
}
