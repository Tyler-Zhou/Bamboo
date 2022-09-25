using Reader.Client.Interfaces;
using Reader.Client.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace Reader.Client.Services
{
    /// <summary>
    /// 章节服务
    /// </summary>
    public class ChapterService: IChapterService
    {
        #region 成员(Member)
        /// <summary>
        /// 存储目录
        /// </summary>
        string SubDirectory = "\\Book\\";
        #endregion

        #region 服务(Services)
        /// <summary>
        /// 缓存服务
        /// </summary>
        ICacheService _CacheService;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 章节服务
        /// </summary>
        /// <param name="cacheService"></param>
        public ChapterService(ICacheService cacheService)
        {
            _CacheService = cacheService;
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 保存章节
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        public bool Save(ChapterModel model)
        {
            if(string.IsNullOrWhiteSpace(model.BookKey))
                return false;
            if (string.IsNullOrWhiteSpace(model.Key))
                return false;
            SubDirectory = $"\\Book\\{model.BookKey}\\";
            Remove(model.Key);
            return Task.Run(() => _CacheService.SaveAsync(SubDirectory, $"{model.Key}", model).Result).Result;
        }

        /// <summary>
        /// 单个章节
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <param name="key">章节 Key</param>
        /// <returns></returns>
        public ChapterModel SingleOrDefault(string bookKey,string key)
        {
            if (string.IsNullOrWhiteSpace(bookKey) || string.IsNullOrWhiteSpace(key))
            {
                return null;
            }
            SubDirectory = $"\\Book\\{bookKey}\\";
            ChapterModel model = Task.Run(() => _CacheService.GetAsync<ChapterModel>(SubDirectory, key).Result).Result;
            return model;
        }

        /// <summary>
        /// 获取所有章节Key
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <returns></returns>
        public ObservableCollection<string> GetAllKey(string bookKey)
        {
            SubDirectory = $"\\Book\\{bookKey}\\";
            ObservableCollection<string> result = new ObservableCollection<string>();
            string basePath = Task.Run(() => _CacheService.GetSavePathAsync().Result).Result;
            DirectoryInfo dir = new DirectoryInfo($"{basePath}{SubDirectory}");
            FileInfo[] fis = dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                string name = fi.Name.Replace(fi.Extension, "");
                result.Add(name);
            }
            return result;
        }

        /// <summary>
        /// 获取所有章节
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <returns></returns>
        public ObservableCollection<ChapterModel> GetAll(string bookKey)
        {
            SubDirectory = $"\\Book\\{bookKey}\\";
            ObservableCollection<ChapterModel> result = new ObservableCollection<ChapterModel>();
            string basePath = Task.Run(() => _CacheService.GetSavePathAsync().Result).Result;
            DirectoryInfo dir = new DirectoryInfo($"{basePath}{SubDirectory}");
            FileInfo[] fis = dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                string name = fi.Name.Replace(fi.Extension, "");
                ChapterModel model = Task.Run(() => _CacheService.GetAsync<ChapterModel>(SubDirectory, name).Result).Result;
                if (model != null)
                {
                    result.Add(model);
                }
            }
            return result;
        }

        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="key">标识键</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Task.Run(() => _CacheService.RemoveAsync(SubDirectory, key).Result).Result;
        }
        #endregion
    }
}
