using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using System;
using System.Collections.Generic;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 邮件信息抽取类
    /// </summary>
    public class MailInfoGetter
    {
        /// <summary>
        /// 
        /// </summary>

        public IClientBusinessContactService ClientBusinessContactService
        {
            get
            {
                return ServiceClient.GetService<IClientBusinessContactService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public List<string> Nos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EmailSourceType SourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="businessPartTypeName"></param>
        public void ExtractMailInfo(Message.ServiceInterface.Message mail, string businessPartTypeName)
        {
            int index = businessPartTypeName.IndexOf("BusinessPart", StringComparison.InvariantCultureIgnoreCase);
            SourceType = (EmailSourceType)Enum.Parse(typeof(EmailSourceType), businessPartTypeName.Substring(0, index));
            string regexExpression = "[a-zA-Z0-9]{10,30}";
            Nos = mail.Subject.MatchUseRegex(regexExpression);
            EmailAddress = mail.SendFrom;
        }
    }
}
