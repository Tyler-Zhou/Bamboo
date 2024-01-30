using System;

namespace ICP.Operation.Common.ServiceInterface
{
   public class CommonConstants
    {
        public const string TemplateCodeKey = "TemplateCode";
        public const string OperationTypeKey = "OperationType";
        public const string OperationIdKey = "OperationID";
        public const string FormIdKey = "FormID";
        public const string ActionKey = "Action";
        public const string AdvanceQueryStringKey = "AdvanceQueryString";
        /// <summary>
        /// 添加附件命令命令名称
        /// </summary>
        public const string Command_Add_Attachment_Name = "Command_Add_Attachment";
        /// <summary>
        /// 删除附件命令名称
        /// </summary>
        public const string Command_Delete_Attachment_Name = "Command_Delete_Attachment";
       /// <summary>
       /// 字符串分隔符，用于文档名称、SONO分隔
       /// </summary>
       public static string  NormalSeparator = Environment.NewLine;
    }
}
