﻿using Client.Helpers;
using Client.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Client.Services
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public class CacheService : ICacheService
    {
        #region 成员(Member)
        /// <summary>
        /// 目录名称
        /// </summary>
        string _DirectoryName { get; set; } = "Cache";
        /// <summary>
        /// 扩展名
        /// </summary>
        string _ExtensionName { get; set; } = ".json";
        /// <summary>
        /// 基本路径
        /// </summary>
        string _BasePath
        {
            get
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _DirectoryName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        #endregion

        #region 服务(Service)
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志服务</param>
        public CacheService(ILogger logger)
        {
            _Logger = logger;
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 获取保存路径
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<string> GetSavePathAsync()
        {
            return await Task.Run(() => SaveFileBasePath());
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <param name="obj">配置文件对象</param>
        public async Task<bool> SaveAsync(string configName, object obj)
        {
            return await Task.Run(() => SaveConfig(configName, obj));
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <typeparam name="TSetting">设置实体对象</typeparam>
        /// <returns></returns>
        public async Task<TSetting> GetAsync<TSetting>(string configName)
        {
            return await Task.Run(() => GetConfig<TSetting>(configName));
        }

        #endregion

        #region 私有方法(Private Method)
        /// <summary>
        /// 保存文件的基本路径
        /// </summary>
        /// <returns></returns>
        string SaveFileBasePath()
        {
            return _BasePath;
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool SaveConfig(string configName, object obj)
        {
            try
            {
                string fullPath = Path.Combine(_BasePath, configName + _ExtensionName);
                string josnText = JsonSerializerHelper.SerializeObject(obj);
                File.WriteAllText(fullPath, josnText);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <typeparam name="TSetting">设置实体对象</typeparam>
        /// <returns></returns>
        TSetting GetConfig<TSetting>(string configName)
        {
            try
            {
                string fullPath = Path.Combine(_BasePath, configName + _ExtensionName);
                string josnText = File.ReadAllText(fullPath);
                return JsonSerializerHelper.DeserializeObject<TSetting>(josnText);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return default(TSetting);
            }
        }
        #endregion
    }
}
