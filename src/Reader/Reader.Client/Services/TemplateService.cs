using Reader.Client.Interfaces;
using Reader.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reader.Client.Services
{
    /// <summary>
    /// 模板服务
    /// </summary>
    public class TemplateService: ITemplateService
    {
        #region 成员(Member)
        /// <summary>
        /// 存储目录
        /// </summary>
        string SubDirectory = "\\Template\\";
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
        public TemplateService(ICacheService cacheService)
        {
            _CacheService = cacheService;
        }
        #endregion

        #region 方法(Method)
        /// <summary>
        /// 保存集合
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="collection">集合</param>
        /// <returns></returns>
        public bool SaveCollection(string name,ObservableCollection<BaseDataModel> collection)
        {
            return Task.Run(() => _CacheService.SaveAsync(SubDirectory, $"{name}", collection).Result).Result;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public ObservableCollection<BaseDataModel> GetCollection(string name)
        {
            ObservableCollection<BaseDataModel> result = Task.Run(() => _CacheService.GetAsync<ObservableCollection<BaseDataModel>>(SubDirectory, name).Result).Result;
            return result;
        }

        /// <summary>
        /// 保存单个
        /// </summary>
        /// <param name="model">首页模板</param>
        /// <returns></returns>
        public bool SaveSingle(BaseDataModel model)
        {
            return Task.Run(() => _CacheService.SaveAsync(SubDirectory, $"{model.Name}", model).Result).Result;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public BaseDataModel GetSingle(string name)
        {
            BaseDataModel result = Task.Run(() => _CacheService.GetAsync<BaseDataModel>(SubDirectory, name).Result).Result;
            return result;
        }
        #endregion
    }
}
