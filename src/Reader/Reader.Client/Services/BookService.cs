using Reader.Client.Interfaces;
using Reader.Client.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace Reader.Client.Services
{
    /// <summary>
    /// 书籍服务
    /// </summary>
    public class BookService: IBookService
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
        /// 书籍服务
        /// </summary>
        /// <param name="cacheService"></param>
        public BookService(ICacheService cacheService)
        {
            _CacheService = cacheService;
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 保存书籍
        /// </summary>
        /// <param name="model">书籍实体</param>
        /// <returns></returns>
        public bool Save(BookModel model)
        {
            SubDirectory = $"\\Book\\";
            return Task.Run(() => _CacheService.SaveAsync(SubDirectory, $"{model.ID}", model).Result).Result;
        }
        /// <summary>
        /// 获取所有书籍
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<BookModel> GetAll()
        {
            SubDirectory = $"\\Book\\";
            ObservableCollection<BookModel> result = new ObservableCollection<BookModel>();
            string basePath = Task.Run(() => _CacheService.GetSavePathAsync().Result).Result;
            DirectoryInfo dir = new DirectoryInfo($"{basePath}{SubDirectory}");
            FileInfo[] fis = dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                string name = fi.Name.Replace(fi.Extension, "");
                BookModel model = Task.Run(() => _CacheService.GetAsync<BookModel>(SubDirectory, name).Result).Result;
                if (model != null)
                {
                    result.Add(model);
                }
            }
            return result;
        }

        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Remove(BookModel model)
        {
            return true;
        }
        #endregion
    }
}
