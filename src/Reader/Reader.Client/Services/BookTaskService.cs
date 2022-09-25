using Reader.Client.Interfaces;
using Reader.Client.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace Reader.Client.Services
{
    /// <summary>
    /// 书籍下载任务服务
    /// </summary>
    public class BookTaskService: IBookTaskService
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
        /// 书籍下载任务服务
        /// </summary>
        /// <param name="cacheService"></param>
        public BookTaskService(ICacheService cacheService)
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
        public bool Save(BookTaskModel model)
        {
            return Task.Run(() => _CacheService.SaveAsync(SubDirectory, $"{model.BookKey}", model).Result).Result;
        }
        /// <summary>
        /// 获取书籍下载任务
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<BookTaskModel> GetAll()
        {
            ObservableCollection<BookTaskModel> result = new ObservableCollection<BookTaskModel>();
            string basePath = Task.Run(() => _CacheService.GetSavePathAsync().Result).Result;
            DirectoryInfo dir = new DirectoryInfo($"{basePath}{SubDirectory}");
            FileInfo[] fis = dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                string name = fi.Name.Replace(fi.Extension, "");
                BookTaskModel model = Task.Run(() => _CacheService.GetAsync<BookTaskModel>(SubDirectory, name).Result).Result;
                if (model != null && !model.IsComplete)
                {
                    result.Add(model);
                }
            }
            return result;
        }

        #endregion
    }
}
