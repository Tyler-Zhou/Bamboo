#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/16 17:07:47
 *
 * Description:
 *         ->http://seleniumhq.github.io/selenium/docs/api/dotnet/
 *           By.XPath("//标签名称[匹配方法(@属性,'表达式')]") 
 *              匹配方法：starts-with[以表达式开始]
 *                      end-with[已表达式结尾]
 *                      contains[包含表达式]
 *                      Invalid[表达式是否有效]
 *           
 *           By.Id()
 *
 * History:
 *         ->
 */
#endregion

using System.IO;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Threading;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Safari;

namespace Cityocean.Crawl.CommonTool
{
    /// <summary>
    /// 爬虫
    /// </summary>
    public sealed class WCrawler : IWCrawler
    {
        private string _WebsiteID { get; set; }
        /// <summary>
        /// 浏览器
        /// </summary>
        private Browsers _Browsers { get; set; }
        /// <summary>
        /// 是否严重异常
        /// </summary>
        private bool IsCriticalException { get; set; }
        /// <summary>
        /// 进程ID
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// 原始HTML
        /// </summary>
        public string OriginalPageSource { get; set; }
        /// <summary>
        /// 爬虫启动动作
        /// </summary>
        public Action<ParamStart> StartAction { get; set; }
        /// <summary>
        /// 爬虫完成动作
        /// </summary>
        public Action<ParamCompleted> CompletedAction { get; set; }
        /// <summary>
        /// 爬虫出错动作
        /// </summary>
        public Action<ParamError> ErrorAction { get; set; }
        /// <summary>
        /// 爬虫停止动作
        /// </summary>
        public Action<ParamStop> StopAction { get; set; }

        /// <summary>
        /// 爬虫
        /// </summary>
        /// <param name="paramBrowsers">浏览器</param>
        /// <param name="paramWebsiteID">网站ID</param>
        public WCrawler(Browsers paramBrowsers,string paramWebsiteID = "")
        {
            if (paramWebsiteID.IsNullOrEmpty())
                paramWebsiteID = Guid.NewGuid().ToUpperString();
            _WebsiteID = paramWebsiteID;
            _Browsers = paramBrowsers;
            IsCriticalException = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramCommandTimeOut"></param>
        /// <returns></returns>
        private IWebDriver InitWebDriver(int paramCommandTimeOut)
        {
            IWebDriver theDriver = null;
            try
            {
                switch (_Browsers)
                {
                    case Browsers.PhantomJS:
                        {
                            PhantomJSDriverService _DriverService = PhantomJSDriverService.CreateDefaultService(GlobalVariable.ProgramDirectory);//初始化Selenium配置，传入存放phantomjs.exe文件的目录
                            _DriverService.IgnoreSslErrors = true;//忽略证书错误
                            _DriverService.WebSecurity = false;//禁用网页安全
                            _DriverService.LoadImages = true;//禁止加载图片
                            _DriverService.LocalToRemoteUrlAccess = true;//允许使用本地资源响应远程 URL
                            _DriverService.HideCommandPromptWindow = true; //隐藏弹出窗体(CMD窗口)
                            PhantomJSOptions _PhantomJSOptions = new PhantomJSOptions();//定义PhantomJS的参数配置对象
                            _PhantomJSOptions.AddAdditionalCapability(@"phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36");
                            theDriver = new PhantomJSDriver(_DriverService, _PhantomJSOptions, TimeSpan.FromSeconds(paramCommandTimeOut));
                            ProcessId = _DriverService.ProcessId;
                        }
                        break;
                    case Browsers.Chrome:
                        {
                            ChromeDriverService _ChromeDriverService = ChromeDriverService.CreateDefaultService(GlobalVariable.ProgramDirectory);
                            _ChromeDriverService.HideCommandPromptWindow = true;
                            ChromeOptions _ChromeOptions = new ChromeOptions();
                            string tempDir = string.Format("{0}chromeprofiles\\profile\\{1}", GlobalVariable.ProgramDirectory, _WebsiteID);
                            if (!tempDir.IsExistsDirectory())
                            {
                                Directory.CreateDirectory(tempDir);
                            }
                            _ChromeOptions.AddArgument(string.Format("--user-data-dir={0}", tempDir));
                            _ChromeOptions.AddArgument("--disable-images");
                            theDriver = new ChromeDriver(_ChromeDriverService, _ChromeOptions);
                            ProcessId = _ChromeDriverService.ProcessId;
                        }
                        break;
                    case Browsers.IE:
                        {
                            InternetExplorerDriverService _InternetExplorerDriverService =
                                InternetExplorerDriverService.CreateDefaultService(GlobalVariable.ProgramDirectory);
                            InternetExplorerOptions _ieOptions = new InternetExplorerOptions
                            {
                                IntroduceInstabilityByIgnoringProtectedModeSettings = true ,
                                
                            };
                            theDriver = new InternetExplorerDriver(_InternetExplorerDriverService, _ieOptions);
                            ProcessId = _InternetExplorerDriverService.ProcessId;
                        }
                        break;
                    case Browsers.Firefox:
                        {
                            theDriver = new FirefoxDriver();
                        }
                        break;
                    case Browsers.Safari:
                        {
                            theDriver = new SafariDriver();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, string.Format("启动抓取网站[{0}]数据程序出现异常:{1}", _WebsiteID, ex.Message));
                theDriver = null;
            }
            return theDriver;
        }

        /// <summary>
        /// 爬虫
        /// </summary>
        /// <param name="paramObject">任务对象</param>
        /// <param name="paramConfig">配置</param>
        /// <param name="paramCommandTimeOut">命令超时秒数</param>
        /// <param name="paramTaskTimeOut">任务超时时间</param>
        /// <returns></returns>
        public void StartCrawl(object paramObject, dynamic paramConfig, int paramCommandTimeOut, TimeSpan paramTaskTimeOut)
        {
            try
            {
                CrawlData(paramObject, paramConfig, paramCommandTimeOut);
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "StartCrawl EX", ex);
            }
            finally
            {
                if (IsCriticalException)
                {
                    throw new Exception(string.Format("爬虫进程[{0}]停止抓取数据", ProcessId));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramObject"></param>
        /// <param name="paramConfig"></param>
        /// <param name="paramCommandTimeOut"></param>
        
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public void CrawlData(object paramObject, dynamic paramConfig, int paramCommandTimeOut)
        {
            try
            {
                #region Task Crawl

                IWebDriver _Driver = null;
                try
                {
                    _Driver = InitWebDriver(paramCommandTimeOut);
                    if (_Driver.IsNull()) return;
                    var watch = DateTime.Now;
                    if (StartAction != null)
                        StartAction(new ParamStart(_Driver, paramObject, paramConfig));
                    var milliseconds = DateTime.Now.Subtract(watch).TotalMilliseconds; //获取请求执行时间;
                    if (CompletedAction != null)
                        CompletedAction(new ParamCompleted(_Driver, paramObject, paramConfig,
                            (int)milliseconds));
                }
                catch (Exception ex)
                {
                    if (isCriticalException(ex))
                    {
                        LogService.Fatal(CommonConstants.MODULENAME_CRAWLTOOL, "爬虫意料之外异常", ex);
                        IsCriticalException = true;
                    }
                    if (ErrorAction != null)
                        ErrorAction(new ParamError(paramObject, ProcessId, ex));
                    try
                    {
                        _Driver.Close();
                        ProcessHelper.KillProcess4ID(ProcessId);
                    }
                    catch (Exception ex2)
                    {
                        LogService.Fatal(CommonConstants.MODULENAME_CRAWLTOOL, "爬虫严重异常", ex2);
                    }
                }
                finally
                {
                    AbortDriver(_Driver);
                    if (StopAction != null)
                        StopAction(new ParamStop(paramObject));
                }
                #endregion
            }
            catch (Exception ex)
            {
                IsCriticalException = true;
                KillProcess();
                LogService.Fatal(CommonConstants.MODULENAME_CRAWLTOOL, "爬虫严重异常", ex);
            }
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void KillProcess()
        {
            try
            {
                ProcessHelper.KillProcess(ProcessId);
            }
            catch (Exception ex)
            {
                LogService.Fatal(CommonConstants.MODULENAME_CRAWLTOOL, "强制结束爬虫程序异常", ex);
            }
        }
        private bool isCriticalException(Exception pEx)
        {
            try
            {
                string crawlException = INIHelper.Instance.IniReadValue(CommonConstants.MODULENAME_SERVICECONFIG, CommonConstants.CONFIG_CRAWLEXCEPTION);
                return !pEx.Message.RegexIsMatch(crawlException);
            }
            catch (Exception ex)
            {
                LogService.Fatal(CommonConstants.MODULENAME_CRAWLTOOL, "验证是否严重异常", ex);
                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        private void StopDriver(IWebDriver driver)
        {
            try
            {
                if (driver.IsNull()) return;
                driver.NavigateStop();
            }
            catch (Exception ex)
            {
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, "停止驱动器", ex);
            }
        }
        /// <summary>
        /// 终止驱动器
        /// </summary>
        /// <param name="driver"></param>
        private void AbortDriver(IWebDriver driver)
        {
            try
            {
                if (driver.IsNull()) return;
                StopDriver(driver);

                IList<string> handlers = driver.WindowHandles;
                foreach (var winHandler in handlers)
                {
                    try
                    {
                        driver.SwitchTo().Window(winHandler);
                        driver.Close();
                    }
                    catch (Exception fex)
                    {
                        LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL,string.Format("进程[{0}]关闭窗体", ProcessId),fex);
                    }
                }
                try
                {
                    driver.Quit();
                }
                catch (Exception qex)
                {
                    LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL, string.Format("进程[{0}]退出驱动器", ProcessId), qex);
                }
            }
            catch (Exception ex)
            {
                IsCriticalException = true;
                LogService.Error(CommonConstants.MODULENAME_CRAWLTOOL,string.Format("进程[{0}]退出", ProcessId),ex);
            }
        }
    }
}
