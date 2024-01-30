#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/11 17:23:05
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Runtime.ExceptionServices;
using System.Security;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;
using Cityocean.Crawl.NoticeComponents;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Cityocean.Crawl.ServerComponents
{
    /// <summary>
    /// 服务基类
    /// </summary>
    public class BaseService
    {
        #region Property
        /// <summary>
        /// Module Name
        /// </summary>
        public virtual string ModuleName { get; set; }
        #endregion

        #region Service
        private IMailService _MService;
        /// <summary>
        /// 邮件服务
        /// </summary>
        public IMailService MService
        {
            get { return _MService ?? (_MService = new MailService()); }
        }
        #endregion

        #region Method
        #region DataBase
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        public virtual Database GetDefaultDatabase()
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                return factory.Create("CrawlerSQLDB");
            }
            catch (Exception ex)
            {
                LogService.Fatal("BaseService", "GetDefaultDatabase", ex);
                throw new Exception("获取数据库连接发生严重异常");
            }
        }

        /// <summary>
        /// 获取SQL Lite数据库连接
        /// </summary>
        /// <returns></returns>
        public virtual Database GetLocalDatabase()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            return factory.Create("CrawlerCache");
        } 
        #endregion

        #region Config

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="paramModuleName">模块</param>
        /// <param name="paramKey">键值</param>
        /// <param name="paramDefaultValue">默认值</param>
        /// <returns></returns>
        public string GetConfigValue(string paramModuleName, string paramKey, string paramDefaultValue)
        {
            string strTemp = INIHelper.Instance.IniReadValue(paramModuleName, paramKey);
            if (!strTemp.IsNullOrEmpty()) return strTemp;
            INIHelper.Instance.IniWriteValue(ModuleName, paramKey, paramDefaultValue);
            return paramDefaultValue;
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="paramKey">键值</param>
        /// <param name="paramDefaultValue">默认值</param>
        /// <returns></returns>
        public string GetConfigValue(string paramKey, string paramDefaultValue = "")
        {
            return GetConfigValue(ModuleName,paramKey,paramDefaultValue);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="paramKey">键值</param>
        /// <param name="paramValue">值</param>
        /// <returns></returns>
        public void SetConfigValue(string paramKey, string paramValue)
        {
            SetConfigValue(ModuleName, paramKey, paramValue);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="paramModuleName">模块</param>
        /// <param name="paramKey">键值</param>
        /// <param name="paramValue">值</param>
        /// <returns></returns>
        public void SetConfigValue(string paramModuleName, string paramKey, string paramValue)
        {
            INIHelper.Instance.IniWriteValue(paramModuleName, paramKey, paramValue);
        }
        #endregion
        #endregion
    }
}
