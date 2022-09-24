using HtmlAgilityPack;
using System;

namespace Reader.Client.Spider.Extensions
{
    /// <summary>
    /// HtmlDocument 扩展方法
    /// </summary>
    public static class HtmlDocumentExtensions
    {
        /// <summary>
        /// 根据XPath路径获取文本
        /// </summary>
        /// <param name="input">文档对象</param>
        /// <param name="XPath">XPath路径</param>
        /// <param name="isDebug">是否调试</param>
        /// <returns></returns>
        public static string XPathInnerText(this HtmlDocument input, string XPath, bool isDebug = false)
        {
            string innerText = "";
            try
            {
                if (string.IsNullOrWhiteSpace(XPath))
                {
                    throw new Exception("未配置");
                }
                HtmlNode htmlNode = input.DocumentNode.SelectSingleNode(XPath);
                if (htmlNode != null)
                {
                    innerText = htmlNode.InnerText;
                }
            }
            catch(Exception ex)
            {
                if (isDebug)
                    innerText = $"{XPath}:{ex.Message}";
                else
                    innerText = "";
            }
            return innerText;
        }

        /// <summary>
        /// 根据XPath路径获取时间
        /// </summary>
        /// <param name="input">文档对象</param>
        /// <param name="XPath">XPath路径</param>
        /// <returns></returns>
        public static DateTime XPathDateTime(this HtmlDocument input, string XPath)
        {
            DateTime innerDateTime = DateTime.MinValue;
            try
            {
                if(!string.IsNullOrWhiteSpace(XPath))
                {
                    HtmlNode htmlNode = input.DocumentNode.SelectSingleNode(XPath);
                    if (htmlNode != null)
                    {
                        innerDateTime = DateTime.Parse(htmlNode.InnerText);
                    }
                }
            }
            catch
            {
                innerDateTime = DateTime.MinValue;
            }
            return innerDateTime;
        }

        /// <summary>
        /// 根据XPath路径获取属性值
        /// </summary>
        /// <param name="input">文档对象</param>
        /// <param name="XPath">XPath路径</param>
        /// <param name="attribute">标签值</param>
        /// <param name="isDebug">是否调试</param>
        /// <returns></returns>
        public static string XPathAttributeValue(this HtmlDocument input, string XPath,string attribute, bool isDebug = false)
        {
            string attributeValue = "";
            try
            {
                if (!string.IsNullOrWhiteSpace(XPath))
                {
                    HtmlNode htmlNode = input.DocumentNode.SelectSingleNode(XPath);
                    if (htmlNode != null)
                    {
                        attributeValue = htmlNode.Attributes[attribute].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                if (isDebug)
                    attributeValue = $"{XPath}:{ex.Message}";
                else
                    attributeValue = "";
            }
            return attributeValue;
        }
    }
}
