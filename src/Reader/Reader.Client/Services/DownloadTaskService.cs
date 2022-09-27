using Reader.Client.Interfaces;
using Reader.Client.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Reader.Client.Services
{
    /// <summary>
    /// 下载任务服务
    /// </summary>
    public class DownloadTaskService: IDownloadTaskService
    {
        #region 成员(Member)
        /// <summary>
        /// 存储目录
        /// </summary>
        string SubDirectory = "\\Book\\DownloadTask\\";
        #endregion

        #region 服务(Services)
        /// <summary>
        /// 缓存服务
        /// </summary>
        ICacheService _CacheService;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 下载任务服务
        /// </summary>
        /// <param name="cacheService"></param>
        public DownloadTaskService(ICacheService cacheService)
        {
            _CacheService = cacheService;
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 保存书籍下载任务
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        public bool Save(DownloadTaskModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Key)
                || string.IsNullOrWhiteSpace(model.BookKey))
                return false;
            
            SubDirectory = $"\\Book\\Task\\{model.BookKey}\\";
            Remove(model.Key);
            return Task.Run(() => _CacheService.SaveAsync(SubDirectory, $"{model.Key}", model).Result).Result;
        }
        /// <summary>
        /// 下载任务
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <param name="key">任务 Key</param>
        /// <returns></returns>
        public DownloadTaskModel SingleOrDefault(string bookKey,string key)
        {
            if (string.IsNullOrWhiteSpace(bookKey) || string.IsNullOrWhiteSpace(key))
                return null;
            SubDirectory = $"\\Book\\Task\\{bookKey}\\";
            DownloadTaskModel model = Task.Run(() => _CacheService.GetAsync<DownloadTaskModel>(SubDirectory, key).Result).Result;
            return model;
        }

        /// <summary>
        /// 获取下载任务数量
        /// </summary>
        /// <param name="bookKey"></param>
        /// <returns></returns>
        public int GetCount(string bookKey)
        {
            if (string.IsNullOrWhiteSpace(bookKey))
                return 0;
            SubDirectory = $"\\Book\\Task\\{bookKey}\\";
            string basePath = Task.Run(() => _CacheService.GetSavePathAsync().Result).Result;
            string cachePath = $"{basePath}{SubDirectory}";
            if (!Directory.Exists(cachePath))
            {
                return 0;
            }
            DirectoryInfo dir = new DirectoryInfo(cachePath);
            return dir.GetFiles().Length;
        }
        /// <summary>
        /// 获取下载任务
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <returns></returns>
        public ObservableCollection<DownloadTaskModel> GetAll(string bookKey)
        {
            if (string.IsNullOrWhiteSpace(bookKey))
                return null;
            SubDirectory = $"\\Book\\Task\\{bookKey}\\";
            ObservableCollection<DownloadTaskModel> result = new ObservableCollection<DownloadTaskModel>();
            string basePath = Task.Run(() => _CacheService.GetSavePathAsync().Result).Result;
            string cachePath = $"{basePath}{SubDirectory}";
            if (!Directory.Exists(cachePath))
            {
                Directory.CreateDirectory(cachePath);
            }
            DirectoryInfo dir = new DirectoryInfo(cachePath);
            FileInfo[] fis = dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                string name = fi.Name.Replace(fi.Extension, "");
                DownloadTaskModel model = Task.Run(() => _CacheService.GetAsync<DownloadTaskModel>(SubDirectory, name).Result).Result;
                if (model != null)
                {
                    result.Add(model);
                }
            }
            return result;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Task.Run(() => _CacheService.RemoveAsync(SubDirectory, key).Result).Result;
        }

        #endregion
    }
}
