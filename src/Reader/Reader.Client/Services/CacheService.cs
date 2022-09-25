using Microsoft.Extensions.Logging;
using Reader.Client.Helpers;
using Reader.Client.Interfaces;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Client.Services
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
        /// <param name="subDirectory">子目录</param>
        /// <param name="configName">配置名称</param>
        /// <param name="obj">配置文件对象</param>
        public async Task<bool> SaveAsync(string subDirectory, string configName, object obj)
        {
            return await Task.Run(() => SaveConfig(subDirectory, configName, obj));
        }

        /// <summary>
        /// 生成Json文本
        /// </summary>
        /// <param name="obj">配置文件对象</param>
        public async Task<string> GenerateAsync(object obj)
        {
            return await Task.Run(() => GenerateJSON(obj));
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="subDirectory">子目录</param>
        /// <param name="configName">配置名称</param>
        /// <typeparam name="TSetting">设置实体对象</typeparam>
        /// <returns></returns>
        public async Task<TSetting> GetAsync<TSetting>(string subDirectory, string configName)
        {
            return await Task.Run(() => GetConfig<TSetting>(subDirectory,configName));
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
        /// <param name="obj"></param>
        /// <returns></returns>
        string GenerateJSON(object obj)
        {
            try
            {
                //序列化
                return JsonSerializerHelper.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return ex.Message;
            }
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="subDirectory">子目录</param>
        /// <param name="configName">配置名称</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool SaveConfig(string subDirectory, string configName, object obj)
        {
            try
            {
                string basePath= $"{_BasePath}{subDirectory}";
                EnsureDirectoryExists(basePath);
                string fullPath = $"{basePath}{configName}{_ExtensionName}";
                //序列化
                string josnText = JsonSerializerHelper.SerializeObject(obj);
                //写入文件流
                using (StreamWriter sw = new StreamWriter(fullPath))
                {
                    sw.WriteLine(josnText);
                }
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
        /// <param name="subDirectory">子目录</param>
        /// <param name="configName">配置名称</param>
        /// <typeparam name="TSetting">设置实体对象</typeparam>
        /// <returns></returns>
        TSetting GetConfig<TSetting>(string subDirectory, string configName)
        {
            try
            {
                string fullPath = $"{_BasePath}{subDirectory}{configName}{_ExtensionName}";
                if (!File.Exists(fullPath))
                {
                    throw new Exception($"文件[{fullPath}]不存在");
                }
                string josnText = "";
                using (StreamReader sr = new StreamReader(fullPath, Encoding.UTF8))
                {
                    josnText = sr.ReadToEnd();
                }
                return JsonSerializerHelper.DeserializeObject<TSetting>(josnText);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex.Message);
                return default(TSetting);
            }
        }
        /// <summary>
        /// 确保目录存在
        /// </summary>
        /// <param name="dir"></param>
        void EnsureDirectoryExists(string dir)
        {
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        #endregion
    }
}
