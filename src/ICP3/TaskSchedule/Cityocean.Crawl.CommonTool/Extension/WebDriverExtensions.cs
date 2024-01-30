#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/8/17 11:48:34
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Cityocean.Crawl.LogComponents;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Cityocean.Crawl.CommonLibrary;

namespace Cityocean.Crawl.CommonTool
{
    /// <summary>
    /// WebDriver扩展
    /// </summary>
    public static class WebDriverExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetAllCookies(this IWebDriver driver)
        {
            try
            {
                Dictionary<string, string> cookies = new Dictionary<string, string>();
                var allCookies = driver.Manage().Cookies.AllCookies;
                foreach (Cookie cookie in allCookies)
                {
                    cookies[cookie.Name] = cookie.Value;
                }
                return cookies;
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "GetAllCookies", ex);
                throw new Exception(string.Format("获取所有Cookies发生异常：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 等待查找控件(扩展)
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="paramRegexString">XPath字符串</param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <param name="paramCheckVisible">检查可见</param>
        /// <param name="timeoutSeconds">超时秒数</param>
        /// <returns></returns>
        public static IWebElement WaitFindElement(this IWebDriver driver, string paramRegexString, bool isThrowException = true
            , bool paramCheckVisible = false, int timeoutSeconds = 5)
        {
            IWebElement element;

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                By _by = By.XPath(paramRegexString);
                element = wait.Until(paramCheckVisible ? ExpectedConditions.ElementIsVisible(_by) : ExpectedConditions.ElementExists(_by));
            }
            catch (NoSuchElementException nseEX)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "WaitFindElement", nseEX);
                if (isThrowException)
                    throw new WebDriverException(string.Format("未找到[{0}]控件:{1}", paramRegexString, nseEX.Message));
                element = null;
            }
            catch (WebDriverTimeoutException wdtEx)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "WaitFindElement", wdtEx);
                if (isThrowException)
                    throw new WebDriverException(string.Format("查找[{0}]控件驱动器[{1}]后超时:{2}", paramRegexString, timeoutSeconds, wdtEx.Message));
                element = null;
            }
            catch (TimeoutException tEX)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "WaitFindElement", tEX);
                if (isThrowException)
                    throw new WebDriverException(string.Format("查找[{0}]控件[{1}]后超时:{2}", paramRegexString, timeoutSeconds, tEX.Message));
                element = null;
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "WaitFindElement", ex);
                if (isThrowException)
                    throw new WebDriverException(string.Format("查找[{0}]控件出现异常,异常明细{1}", paramRegexString, ex.Message));
                element = null;
            }
            return element;
        }

        /// <summary>
        /// 等待加载
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="paramRegexString">XPath字符串</param>
        /// <param name="timeoutSeconds">超时秒数</param>
        /// <returns></returns>
        public static bool WaitElementsLoad(this IWebDriver driver, string paramRegexString, int timeoutSeconds = 10)
        {
            bool returnResult = false;
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                wait.IgnoreExceptionTypes(typeof (NoSuchElementException), typeof (WebDriverTimeoutException),
                    typeof (TimeoutException));
                returnResult = wait.Until((x) =>
                {
                    x.IsCompelete();
                    var pageSource = x.GetPageSource();
                    return pageSource.RegexIsMatch(paramRegexString, true);
                });
            }
            catch (WebDriverTimeoutException)
            {
                returnResult = false;
            }
            catch(Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "WaitElementsLoad", ex);
                returnResult = false;
            }
            return returnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="paramUrlAddress">网址</param>
        /// <param name="timeoutSeconds">超时时间(s)</param>
        /// <returns></returns>
        public static void NavigateUrl(this IWebDriver driver, string paramUrlAddress, int timeoutSeconds = 30)
        {

            try
            {
                driver.Navigate().GoToUrl(paramUrlAddress);
                bool isComplate = driver.IsCompelete(timeoutSeconds);
                if (!isComplate)
                    isComplate = driver.WaitElementsLoad(@"<body[^>]*>", timeoutSeconds);
                if (!isComplate)
                    throw new WebDriverException(string.Format("[{0}]超时", paramUrlAddress));
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "NavigateUrl", ex);
                throw new WebDriverException(string.Format("NoResponse:导航到网站出现异常:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 浏览器停止访问
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <returns></returns>
        public static void NavigateStop(this IWebDriver driver)
        {
            try
            {
                driver.ExecuteScript("window.stop ? window.stop() : document.execCommand('Stop');");
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "NavigateStop", ex);
                throw new WebDriverException(string.Format("停止网站发生异常:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 执行脚本(扩展)
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="paramScript">脚本</param>
        /// <returns></returns>
        public static object ExecuteScript(this IWebDriver driver, string paramScript)
        {
            try
            {
                var pjs = driver as IJavaScriptExecutor;
                if (pjs == null)
                {
                    throw new WebDriverException("驱动器转IJavaScriptExecutor失败");
                }
                return pjs.ExecuteScript(paramScript);
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "ExecuteScript", ex);
                throw new WebDriverException(string.Format("执行脚本[{0}]出现异常:{1}", paramScript, ex.Message));
            }
        }

        /// <summary>
        /// 是否完成，等待5s后检测是否加载完成(扩展)
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="isThrowException">是否抛出异常</param>
        /// <param name="timeoutSeconds">等待时间</param>
        /// <returns></returns>
        public static bool IsCompelete(this IWebDriver driver, int timeoutSeconds = 5, bool isThrowException = false)
        {
            bool returnResult = false;
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                returnResult =
                    wait.Until((x) => "complete".Equals((string) driver.ExecuteScript("return document.readyState")));
            }
            catch (WebDriverTimeoutException wdtEx)
            {
                throw new Exception(wdtEx.Message);
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "IsCompelete", ex);
                if (isThrowException)
                    throw new Exception(string.Format("{0}s后超时", timeoutSeconds));
                returnResult = false;
            }
            return returnResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="timeoutSeconds">超时时间(s)</param>
        /// <returns></returns>
        public static string GetPageSource(this IWebDriver driver, int timeoutSeconds = 10)
        {
            string pageSource = string.Empty;
            try
            {
                pageSource = driver.PageSource;
                return pageSource.GetBody(timeoutSeconds);
            }
            catch(Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "GetPageSource", ex);
                return pageSource;
            }
        }

        /// <summary>
        /// 转到Window(扩展)
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="paramRegexString">匹配URL的表达式</param>
        /// <param name="timeoutSeconds">重新导航</param>
        /// <returns></returns>
        public static void GotoWindow(this IWebDriver driver, string paramRegexString, int timeoutSeconds = 5)
        {
            try
            {
                bool complete = false;
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
                wait.Until((x) =>
                {
                    IList<string> handlers = driver.WindowHandles;
                    foreach (var winHandler in handlers)
                    {
                        driver.SwitchTo().Window(winHandler);
                        if (!driver.Url.RegexIsMatch(paramRegexString) && !driver.Title.RegexIsMatch(paramRegexString))
                            continue;
                        complete = true;
                        break;
                    }
                    return complete;
                });
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "GotoWindow", ex);
                throw new WebDriverException(string.Format(":转到Window失败，未找到URL/Title包含[{0}]的窗体,{1}", paramRegexString, ex.Message));
            }
        }

        /// <summary>
        /// 转到Frame(扩展)
        /// </summary>
        /// <param name="driver">驱动器</param>
        /// <param name="paramRegexString">XPath</param>
        /// <returns></returns>
        public static void GotoFrame(this IWebDriver driver, string paramRegexString)
        {
            try
            {
                bool complete = false;
                var frame = driver.WaitFindElement(paramRegexString);
                if (frame != null)
                {
                    driver.SwitchTo().Frame(frame);
                    complete = true;
                }
                if (!complete)
                    throw new WebDriverException(string.Format("转到Frame失败，未找到[{0}]", paramRegexString));
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "GotoFrame", ex);
                throw new Exception(string.Format("跳转到Frame出现异常:{0}", ex.Message));
            }
        }
        /// <summary>
        /// 网站地址
        /// </summary>
        /// <param name="driver"></param>
        public static string GetURLAddress(this IWebDriver driver)
        {
            string urlAddress = string.Empty;

            try
            {
                if (driver.IsNull())
                    return string.Empty;
                urlAddress = driver.Url;
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "GetURLAddress", ex);
                urlAddress = string.Empty;
            }
            return urlAddress;
        }
    }
}
