using Reader.Client.Interfaces;
using Reader.Client.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Reader.Client.Services
{
    /// <summary>
    /// 书源服务
    /// </summary>
    public class BookSourceService: IBookSourceService
    {
        #region 成员(Member)
        /// <summary>
        /// 存储目录
        /// </summary>
        string SubDirectory = "\\Source\\";
        #endregion

        #region 服务(Services)
        /// <summary>
        /// 缓存服务
        /// </summary>
        ICacheService _CacheService;
        #endregion

        #region 构造函数(Constructor)
        /// <summary>
        /// 书源服务
        /// </summary>
        /// <param name="cacheService"></param>
        public BookSourceService(ICacheService cacheService)
        {
            _CacheService = cacheService;
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 保存书源
        /// </summary>
        /// <param name="model">书源实体</param>
        /// <returns></returns>
        public bool Save(BookSourceModel model)
        {
            return Task.Run(() => _CacheService.SaveAsync(SubDirectory, $"{model.ID}", model).Result).Result;
        }

        /// <summary>
        /// 获取单个书源
        /// </summary>
        /// <param name="id">标识键</param>
        /// <returns></returns>
        public BookSourceModel SingleOrDefault(Guid id)
        {
            if (id!=null && !id.Equals(Guid.Empty))
            {
                BookSourceModel model = Task.Run(() => _CacheService.GetAsync<BookSourceModel>(SubDirectory, $"{id}").Result).Result;
                return model;
            }
            return null;
        }

        /// <summary>
        /// 生成源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Generate(BookSourceModel model)
        {
            return Task.Run(() => _CacheService.GenerateAsync(model).Result).Result;
        }

        /// <summary>
        /// 获取所有书源
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<BookSourceModel> GetAll()
        {
            ObservableCollection<BookSourceModel> result = new ObservableCollection<BookSourceModel>();
            string basePath = Task.Run(() => _CacheService.GetSavePathAsync().Result).Result;
            DirectoryInfo dir = new DirectoryInfo($"{basePath}{SubDirectory}");
            FileInfo[] fis = dir.GetFiles();
            for (int i = 0; i < fis.Length; i++)
            {
                FileInfo fi = fis[i];
                string name = fi.Name.Replace(fi.Extension, "");
                BookSourceModel model = Task.Run(() => _CacheService.GetAsync<BookSourceModel>(SubDirectory, name).Result).Result;
                if (model != null)
                {
                    result.Add(model);
                }
            }
            return result;
        }

        /// <summary>
        /// 删除书源
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public bool Remove(Guid id)
        {
            return Task.Run(() => _CacheService.RemoveAsync(SubDirectory, $"{id}").Result).Result;
        }

        #endregion
    }
}
