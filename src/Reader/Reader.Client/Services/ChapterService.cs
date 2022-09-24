using Reader.Client.Interfaces;
using Reader.Client.Models;
using System;
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
            SubDirectory = $"\\Book\\{model.BookID}\\";
            return Task.Run(() => _CacheService.SaveAsync(SubDirectory, $"{model.ID}", model).Result).Result;
        }
        /// <summary>
        /// 获取所有章节
        /// </summary>
        /// <param name="bookID">书籍ID</param>
        /// <returns></returns>
        public ObservableCollection<ChapterModel> GetAll(Guid bookID)
        {
            SubDirectory = $"\\Book\\{bookID}\\";
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
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Remove(ChapterModel model)
        {
            return true;
        }
        #endregion
    }
}
