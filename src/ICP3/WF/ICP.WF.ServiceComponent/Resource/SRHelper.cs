using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.WF.ServiceComponent.Resource;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
namespace ICP.WF.ServiceComponent
{
    /// <summary>
    /// 资源文件里面的键常量
    /// </summary>
    internal sealed class SRConstants
    {
        public const string FirstSignWorkItem = "FirstSignWorkItem";
        public const string CannotExcuteOperation = "CannotExcuteOperation";
        public const string WorkItemAlreadySignedIn = "WorkItemAlreadySignedIn";
        public const string WorkItemCannotSignIn = "WorkItemCannotSignIn";
    }

    internal sealed class SRHelper
    {
        /// <summary>
        /// 获取指定资源
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaulteValue"></param>
        /// <returns></returns>
        public static string GetString(string key, string defaulteValue)
        {
            string rtnVal = string.Empty;
            if (ApplicationContext.Current.IsEnglish)
            {
                 rtnVal = WFResourceEN.ResourceManager.GetString(key);
            }
            else
            {
                rtnVal = WFResourceCN.ResourceManager.GetString(key);
            }
          
            if (!string.IsNullOrEmpty(rtnVal))
            {
                return rtnVal;
            }
            else
            {
                return defaulteValue;
            }


        }
    }
}
