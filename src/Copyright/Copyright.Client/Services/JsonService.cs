/*******************************************************
 * Project:
 * Description:
 * Version:1.0.0.0
 * Time:2022-10-02 9:49:08
 * Author:zhoubiyu@hotmail.com
 * Update:
********************************************************/
using Copyright.Client.Helpers;
using System;
using System.IO;
using System.Text;

namespace Copyright.Client.Services
{
    /// <summary>
    /// Json服务
    /// </summary>
    public class JsonService
    {
        #region 成员(Member)
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
                string path = $"{AppDomain.CurrentDomain.BaseDirectory}\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        #endregion

        #region 服务(Service)
        #endregion

        #region 构造函数(Constructor)
        #endregion

        #region 方法(Method)

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <param name="obj">配置文件对象</param>
        public bool Save(string configName, object obj)
        {
            return  SaveJSON(configName, obj);
        }

        /// <summary>
        /// 获取Json
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <typeparam name="TJson">Json实体对象</typeparam>
        /// <returns></returns>
        public TJson Get<TJson>(string configName)
        {
            return GetJSON<TJson>(configName);
        }
        
        #endregion

        #region 私有方法(Private Method)

        /// <summary>
        /// 保存Json文件
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool SaveJSON(string configName, object obj)
        {
            try
            {
                string fullPath = $"{_BasePath}{configName}{_ExtensionName}";
                //序列化
                string josnText = JsonSerializerHelper.SerializeObject(obj);
                //写入文件流
                using (StreamWriter sw = new StreamWriter(fullPath))
                {
                    sw.WriteLine(josnText);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="subDirectory">子目录</param>
        /// <param name="configName">配置名称</param>
        /// <typeparam name="TJSON">设置实体对象</typeparam>
        /// <returns></returns>
        TJSON GetJSON<TJSON>(string configName)
        {
            try
            {
                string fullPath = $"{_BasePath}{configName}{_ExtensionName}";
                if (!File.Exists(fullPath))
                {
                    throw new Exception($"文件[{fullPath}]不存在");
                }
                string josnText = "";
                using (StreamReader sr = new StreamReader(fullPath, Encoding.UTF8))
                {
                    josnText = sr.ReadToEnd();
                }
                return JsonSerializerHelper.DeserializeObject<TJSON>(josnText);
            }
            catch
            {
                return default(TJSON);
            }
        }
        
        #endregion
    }
}
