using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;
namespace ICP.MailCenter.Business.ServiceInterface
{  
    /// <summary>
    /// 
    /// </summary>
   public class MailBusinessHelper
    {
       public static bool IsEnglish
       {
           get
           {
               return ICP.Framework.CommonLibrary.Common.ApplicationContext.Current.IsEnglish;
           }
       }

       /// <summary>
       /// 是否为内部邮件
       /// </summary>
       /// <param name="emailAddress"></param>
       /// <returns></returns>
       public static bool IsInternalEmail(string emailAddress)
       {
           string internalDomains = string.Empty; //ClientHelper.GetAppSettingValue(ClientConstants.InternalDomainKey);
           List<string> domains = internalDomains.Split(',').ToList();

           int index=emailAddress.LastIndexOf("@");
           string fromDomain = emailAddress.Substring(index + 1);
           if (domains.Contains(fromDomain))
               return true;
           return false;

       }
       public static string GetLanguageAttributeName()
       {
           string attributeName = IsEnglish ? "enus" : "zhcn";
           return attributeName;
       }

    }
}
