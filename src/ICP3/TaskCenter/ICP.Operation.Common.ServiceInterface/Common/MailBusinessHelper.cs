using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.Utility;
using System.Collections.Generic;
using System.Linq;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 
    /// </summary>
   public class MailBusinessHelper
    {
       /// <summary>
       /// 
       /// </summary>
       public static bool IsEnglish
       {
           get
           {
               return ApplicationContext.Current.IsEnglish;
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
       /// <summary>
       /// 显示语言版本
       /// </summary>
       /// <returns></returns>
       public static string GetLanguageAttributeName()
       {
           string attributeName = IsEnglish ? "enus" : "zhcn";
           return attributeName;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="refNo"></param>
       /// <param name="pairSeparator"></param>
       /// <param name="keyValueSeparator"></param>
       /// <returns></returns>
       public static ListDictionary<string, string> ExtractNoPairs(string refNo, char[] pairSeparator, char keyValueSeparator)
       {
           refNo = refNo ?? string.Empty;
           if (string.IsNullOrEmpty(refNo))
               return new ListDictionary<string, string>();
           string[] pairs = refNo.Split(pairSeparator);
           ListDictionary<string, string> dicPair = new ListDictionary<string, string>();
           for (int i = 0; i < pairs.Length; i++)
           {
               string pair = pairs[i] ?? string.Empty;
               pair = pair.Trim();
               if (string.IsNullOrEmpty(pair))
                   continue;
               string[] keyValue = pair.Split(keyValueSeparator);
               if (keyValue == null || keyValue.Length != 2)
                   continue;
               string key = keyValue[0].Trim();
               string value = keyValue[1].Trim();
               if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                   continue;
               dicPair.Add(key, value);

           }
           return dicPair;

       }

    }
}
