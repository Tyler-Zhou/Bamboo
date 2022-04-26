using Bamboo.Client.Core.Helper;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bamboo.Client.Core.Common
{
    #region 配置类
    /// <summary>
    /// 配置类
    /// </summary>
    /// <remarks>个性化信息保存类（以文本文件的形式存在磁盘）</remarks>
    [Serializable]
    public class Configure
    {
        #region 成员(Member)
        
        #region 文件名
        /// <summary>
        /// FileName
        /// </summary>
        string _FileName = "Config.json";
        /// <summary>
        /// 文件名
        /// </summary>
        string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _FileName = value;
                }
            }
        }
        #endregion

        #region 文件目录
        /// <summary>
        /// FileDirectory
        /// </summary>
        string _FileDirectory = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 文件目录
        /// </summary>
        string FileDirectory
        {
            get
            {
                return _FileDirectory;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _FileDirectory = value;
                }
            }
        }
        #endregion

        #region 文件完整路径
        /// <summary>
        /// 文件完整路径
        /// </summary>
        string FullFileName
        {
            get
            {
                return Path.Combine(FileDirectory, FileName);
            }
        } 
        #endregion

        #region 配置文件词典
        /// <summary>
        /// 配置文件词典
        /// </summary>
        private Dictionary<string, string> configDic = new Dictionary<string, string>();
        #endregion

        #region 同步锁
        /// <summary>
        /// 同步锁
        /// </summary>
        private static object syncLock = new object();
        #endregion

        #region 单实例
        /// <summary>
        /// Current
        /// </summary>
        private static Configure cfg;
        /// <summary>
        /// 单实例
        /// </summary>
        public static Configure Current
        {
            get
            {
                if (cfg == null)
                {
                    lock (syncLock)
                    {
                        if (cfg == null)
                        {
                            cfg = new Configure();
                        }
                    }
                }
                return cfg;
            }
        }
        #endregion
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名</param>
        public Configure(string fileName)
        {
            FileName = fileName;
            InitConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="fileDirectory">文件路径</param>
        public Configure(string fileName, string fileDirectory)
        {
            FileName = fileName;
            FileDirectory = fileDirectory;
            InitConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        private Configure()
        {
            InitConfig();
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 取配置项的值
        /// </summary>
        /// <param name="key">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>对应的值,不存在则返回null</returns>
        public string GetValue(string key,string defaultValue="")
        {
            string value = GetValue<string>(key);
            if (string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(defaultValue))
                return defaultValue;
            return GetValue<string>(key);
        }

        /// <summary>
        /// 取配置项的值
        /// </summary>
        /// <param name="key">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>对应的值,不存在则返回null</returns>
        public T GetValue<T>(string key, string defaultValue = "")
        {
            return (T)GetValue(key, typeof(T),defaultValue);
        }

        /// <summary>
        /// 添加项,如果名称已经存在,则修改原值
        /// </summary>
        /// <param name="key">项名称</param>
        /// <param name="value">项值</param>
        public void Add(string key, object value)
        {
            string strValue = "" + value;
            if (configDic.ContainsKey(key))
            {
                configDic[key] = strValue;
            }
            else
            {
                configDic.Add(key, strValue);
            }
            SaveConfig();
        }
        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (configDic != null && configDic.ContainsKey(key))
            {
                configDic.Remove(key);

            }
        }
        /// <summary>
        /// 是否包含特定名称的项
        /// </summary>
        /// <param name="key">项名称</param>
        /// <returns>包含返回true,否则返回false</returns>
        public bool Contains(string key)
        {
            if (configDic.ContainsKey(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        private void InitConfig()
        {
            configDic.Clear();
            GetConfig();
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        private void GetConfig()
        {
            try
            {
                //如果不存在就创建file文件夹
                if (File.Exists(FileName) == false)
                {
                    FileStream f = File.Create(FileName);
                    f.Close();
                }
                string s = File.ReadAllText(FileName);
                try
                {
                    configDic = JsonSerializerHelper.DeserializeObject<Dictionary<string, string>>(s);
                }
                catch
                {
                    configDic = new Dictionary<string, string>();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void SaveConfig()
        {
            try
            {
                string s = JsonSerializerHelper.SerializeObject(configDic);
                File.WriteAllText(FileName, s);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        private object GetValue(string key, Type type, string defaultValue = "")
        {
            if(configDic.ContainsKey(key))
            {
                if (TryGetValueInternal(configDic[key], type, out var value))
                    return value;
                throw new InvalidCastException($"Unable to convert the value of Type '{configDic[key].GetType().FullName}' to '{type.FullName}' for the key '{key}' ");
            }
            else if(!string.IsNullOrWhiteSpace(defaultValue))
            {
                if (TryGetValueInternal(defaultValue, type, out var value))
                    return value;
                throw new InvalidCastException($"Unable to convert the value of Type '{defaultValue.GetType().FullName}' to '{type.FullName}' for the key '{key}' ");
            }else
                return GetDefault(type);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kvp"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool TryGetValueInternal(string kvp, Type type, out object value)
        {
            value = GetDefault(type);
            var success = false;
            if (kvp == null)
            {
                success = true;
            }
            else if (kvp.GetType() == type)
            {
                success = true;
                value = kvp;
            }
            else if (type.IsAssignableFrom(kvp.GetType()))
            {
                success = true;
                value = kvp;
            }
            else if (type.IsEnum)
            {
                if (Enum.IsDefined(type, kvp))
                {
                    success = true;
                    value = Enum.Parse(type, kvp);
                }
                else if (int.TryParse(kvp, out var numericValue))
                {
                    success = true;
                    value = Enum.ToObject(type, numericValue);
                }
            }

            if (!success && type.GetInterface("System.IConvertible") != null)
            {
                success = true;
                value = Convert.ChangeType(kvp, type);
            }

            return success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
        #endregion
    }
    #endregion
}
