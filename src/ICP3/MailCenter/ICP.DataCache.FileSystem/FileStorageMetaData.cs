using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace ICP.DataCache.FileSystem
{
    /// <summary>
    /// 沟通历史记录缓存文件元数据维护类
    /// </summary>
    [Serializable]
    public class FileStorageMetaData:IDisposable
    {
        private  string fileName = "FileStorageConfig.cfg";
        private string fileDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        private Dictionary<string, FileItem> configList = new Dictionary<string, FileItem>();
        

        public string FilePath {
            get {
                return this.fileDirectory;
            }
            set {

                this.fileDirectory = value;
            }
            
        }
        public string FileName
        {
            get {
               return this.fileName;
            }
            set {
                this.fileName = value;
            }
        
        }

        public void Init()
        {
            configList.Clear();
            FileConfig config = GetConfig();
            if (config != null)
            {
                foreach (FileItem item in config.Items)
                {
                    configList.Add(item.Id,item);
                }
            }
            else
            {
                SaveConfig();
            }
        }

        /// <summary>
        /// 取配置项的值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>对应的值,不存在则返回null</returns>
        public string GetFileName(Guid id)
        {
            FileItem item = GetValue(id);
            string fileName = string.Empty;
            if (item != null)
                fileName = item.FileName;
            return fileName;
        }
        public string GetDisplayName(Guid id)
        {
            FileItem item = GetValue(id);
            string displayName = string.Empty;
            if (item != null)
                displayName = item.DisplayName;
            return displayName;
        }

        /// <summary>
        /// 设置项的值,如果名称不存在,不做任何操作.
        /// </summary>
        /// <param name="name">项名称</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="displayName">文件显示名称</param>
        public void SetValue(Guid id, string fileName,string displayName)
        {
            string fileId = id.ToString();
            if (configList.ContainsKey(fileId))
            {
                configList[fileId] = new FileItem(id.ToString(), fileName, displayName);
                SaveConfig();
            }
        }
        public FileItem GetValue(Guid id)
        {
            string fileId = id.ToString();
            if (configList.ContainsKey(fileId))
            {
                return configList[fileId];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加项,如果名称已经存在,则修改原值
        /// </summary>
        /// <param name="name">项名称</param>
        /// <param name="value">项值</param>
        public void AddValue(Guid id, string fileName, string displayName)
        {
            string fileId = id.ToString();
            if (configList.ContainsKey(fileId))
            {
                SetValue(id, fileName, displayName);
            }
            else
            {  
                FileItem item=new FileItem(fileId,fileName,displayName);
                configList.Add(fileId, item);
            }
            SaveConfig();
        }
        public void Remove(Guid id)
        {
            string fileId = id.ToString();
            if (configList.ContainsKey(fileId))
            {
                configList.Remove(fileId);
                SaveConfig();
            }
            
        }

        /// <summary>
        /// 是否包含特定名称的项
        /// </summary>
        /// <param name="name">项名称</param>
        /// <returns>包含返回true,否则返回false</returns>
        public bool Contains(Guid id)
        {
            if (configList.ContainsKey(id.ToString()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Contains(string fileName)
        {
            var result = (from item in configList
                          where item.Value.FileName == fileName
                          select item).Count();
            return result > 0;
            
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        private FileConfig GetConfig()
        {
            try
            {
                string filePath = Path.Combine(fileDirectory, fileName);
                if (File.Exists(filePath))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(FileConfig));
                        FileConfig fig = xml.Deserialize(fs) as FileConfig;
                        if (fig != null)
                        {
                            return fig;
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void SaveConfig()
        {
            FileConfig fig = new FileConfig();
            FileItem item;
            foreach (string key in configList.Keys)
            {
                item = configList[key];
                fig.Items.Add(item);   
            }
            string filePath = Path.Combine(fileDirectory, fileName);
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xml = new XmlSerializer(typeof(FileConfig));
                XmlSerializerNamespaces xn = new XmlSerializerNamespaces();
                xn.Add(string.Empty, string.Empty);
                xml.Serialize(fs, fig, xn);
            }
        }

        /// <summary>
        /// 配置对象
        /// </summary>
        [Serializable]
        public class FileConfig
        {
            private List<FileItem> items = new List<FileItem>();
            /// <summary>
            /// 配置项
            /// </summary>
            public List<FileItem> Items
            {
                get
                {
                    return items;
                }
                set
                {
                    items = value;
                }
            }
        }

        /// <summary>
        /// 配置项
        /// </summary>
        [Serializable]
        public class FileItem
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public FileItem()
            {

            }

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="id">文档Id</param>
            /// <param name="fileName">文档名称</param>
            /// <param name="displayName">文档显示名称</param>
            public FileItem(string id, string fileName,string displayName)
            {
                this.id = id;
                this.itemValue = fileName;
                this.itemDisplayName = displayName;

            }

            private string id;
            /// <summary>
            /// id
            /// </summary>
            [XmlAttribute]
            public string Id
            {
                get
                {
                    return id;
                }
                set
                {
                    id = value;
                }
            }

            private string itemValue;
            /// <summary>
            /// 文档名称
            /// </summary>
            [XmlAttribute]
            public string FileName
            {
                get
                {
                    return itemValue;
                }
                set
                {
                    itemValue = value;
                }
            }
            private string itemDisplayName;
            /// <summary>
            /// 文档显示名称
            /// </summary>
            [XmlAttribute]
            public string DisplayName
            {
                get
                {
                    return itemDisplayName;
                }
                set
                {
                    itemDisplayName = value;
                }
            }

        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (configList != null)
            {
                configList.Clear();
                configList = null;
            }
        }

        #endregion
    }
}
