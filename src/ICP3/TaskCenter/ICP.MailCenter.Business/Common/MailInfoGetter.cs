using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Helper;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
namespace ICP.Operation.Common.ServiceInterface
{ 
    /// <summary>
    /// 邮件信息抽取类
    /// </summary>
  public  class MailInfoGetter
    {

      public IClientBusinessContactService ClientBusinessContactService
      {
          get
          {
              return ServiceClient.GetService<IClientBusinessContactService>();
          }
      }
      public List<string> Nos { get; set; }
      public EmailSourceType SourceType { get; set; }
      public string EmailAddress { get; set; }
      public void ExtractMailInfo(Message.ServiceInterface.Message mail,string businessPartTypeName)
      {  
          int index =businessPartTypeName.IndexOf("BusinessPart",StringComparison.InvariantCultureIgnoreCase);

          this.SourceType = (EmailSourceType)Enum.Parse(typeof(EmailSourceType), businessPartTypeName.Substring(0, index));
          string regexExpression = "[a-zA-Z0-9]{10,30}";
          this.Nos =mail.Subject.MatchUseRegex(regexExpression);
          this.EmailAddress = mail.SendFrom;
      }
    }
}
