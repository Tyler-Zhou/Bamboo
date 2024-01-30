/**
 *  创建时间:2014-08-20
 *  创建人:Joabwang    
 *  描  述:记录当前邮件插件安装盘符的类
 **/
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Xml.Serialization;
//using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenterFramework.UI
{
    //public class ClientConfig
    //{
    //    private readonly string fileName = "ClientConfig.cfg";
    //    private string fileDirectory = LocalData.MainPath;
    //    private Dictionary<string, string> configList = new Dictionary<string, string>();


    //    public void ReadConfig()
    //    {
    //        configList.Clear();
    //        Config config = GetConfig();
    //        if (config != null)
    //        {
    //            foreach (ConfigItem item in config.Items)
    //            {
    //                configList.Add(item.Name, item.Value);
    //            }
    //        }
    //        else
    //        {
    //            SaveConfig();
    //        }
    //    }
    //    /// <summary>
    //    /// 添加项,如果名称已经存在,则修改原值
    //    /// </summary>
    //    /// <param name="name">项名称</param>
    //    /// <param name="value">项值</param>
    //    public void AddValue(string name, string value)
    //    {
    //        if (configList.ContainsKey(name))
    //        {
    //            configList[name] = value;
    //        }
    //        else
    //        {
    //            configList.Add(name, value);
    //        }
    //        SaveConfig();
    //    }

    //    /// <summary>
    //    /// 取配置项的值
    //    /// </summary>
    //    /// <param name="name">名称</param>
    //    /// <returns>对应的值,不存在则返回null</returns>
    //    public string GetValue(string name)
    //    {
    //        if (configList.ContainsKey(name))
    //        {
    //            return configList[name];
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }

    //    /// <summary>
    //    /// 获取配置信息
    //    /// </summary>
    //    /// <returns></returns>
    //    private Config GetConfig()
    //    {
    //        try
    //        {
    //            if (File.Exists(Path.Combine(fileDirectory, fileName)))
    //            {
    //                using (FileStream fs = new FileStream(Path.Combine(fileDirectory, fileName), FileMode.Open, FileAccess.Read))
    //                {
    //                    XmlSerializer xml = new XmlSerializer(typeof(Config));
    //                    Config fig = xml.Deserialize(fs) as Config;
    //                    if (fig != null)
    //                    {
    //                        return fig;
    //                    }
    //                }
    //            }
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// 保存配置
    //    /// </summary>
    //    private void SaveConfig()
    //    {
    //        Config fig = new Config();
    //        ConfigItem item;
    //        foreach (string key in configList.Keys)
    //        {
    //            item = new ConfigItem(key, configList[key]);
    //            fig.Items.Add(item);
    //        }
    //        using (FileStream fs = new FileStream(Path.Combine(fileDirectory, fileName), FileMode.Create, FileAccess.Write))
    //        {
    //            XmlSerializer xml = new XmlSerializer(typeof(Config));
    //            XmlSerializerNamespaces xn = new XmlSerializerNamespaces();
    //            xn.Add(string.Empty, string.Empty);
    //            xml.Serialize(fs, fig, xn);
    //        }
    //    }

    //    /// <summary>
    //    /// 配置对象
    //    /// </summary>
    //    [Serializable]
    //    public class Config
    //    {
    //        private List<ConfigItem> items = new List<ConfigItem>();
    //        /// <summary>
    //        /// 配置项
    //        /// </summary>
    //        public List<ConfigItem> Items
    //        {
    //            get
    //            {
    //                return items;
    //            }
    //            set
    //            {
    //                items = value;
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 配置项
    //    /// </summary>
    //    [Serializable]
    //    public class ConfigItem
    //    {
    //        /// <summary>
    //        /// 构造函数
    //        /// </summary>
    //        public ConfigItem()
    //        {

    //        }

    //        /// <summary>
    //        /// 构造函数
    //        /// </summary>
    //        /// <param name="name">项名称</param>
    //        /// <param name="value">项值</param>
    //        public ConfigItem(string name, string @value)
    //        {
    //            this.name = name;
    //            this.itemValue = value;
    //        }

    //        private string name;
    //        /// <summary>
    //        /// 名称
    //        /// </summary>
    //        [XmlAttribute]
    //        public string Name
    //        {
    //            get
    //            {
    //                return name;
    //            }
    //            set
    //            {
    //                name = value;
    //            }
    //        }

    //        private string itemValue;
    //        /// <summary>
    //        /// 值
    //        /// </summary>
    //        [XmlAttribute]
    //        public string Value
    //        {
    //            get
    //            {
    //                return itemValue;
    //            }
    //            set
    //            {
    //                itemValue = value;
    //            }
    //        }
    //    }
    //}
}
