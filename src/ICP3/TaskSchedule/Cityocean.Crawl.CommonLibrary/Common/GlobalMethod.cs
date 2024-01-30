#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/19 15:21:05
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Xml;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 全局方法
    /// </summary>
    public static class GlobalMethod
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceXML(string input)
        {
            string returnValue = string.Empty;
            if (!input.Contains("http://schemas.microsoft.com/2003/10/Serialization/"))
            {
                return input;
            }
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(input);
            XmlNamespaceManager nsMgr = new XmlNamespaceManager(dom.NameTable);
            nsMgr.AddNamespace("ns", "http://schemas.microsoft.com/2003/10/Serialization/");
            XmlNodeList xl = dom.SelectNodes("//ns:string", nsMgr);

            foreach (XmlNode node in xl)
            {
                returnValue=node.InnerText;
            }
            return returnValue;
        }

        #region Tip Message
        /// <summary>
        /// 获取提示信息
        /// </summary>
        /// <param name="paramSectionName">提示信息Section</param>
        /// <param name="paramMsgKey">提示信息Key</param>
        /// <returns></returns>
        public static string GetTipMessage(string paramSectionName, string paramMsgKey)
        {
            return INIHelper.Instance.IniReadValue(paramSectionName, paramMsgKey).IsNullOrEmpty()
                ? string.Format("{0}未定义", paramMsgKey) : INIHelper.Instance.IniReadValue(paramSectionName, paramMsgKey);
        }
        #endregion
    }
}
