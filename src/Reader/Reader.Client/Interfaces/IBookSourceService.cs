using Reader.Client.Models;
using System;
using System.Collections.ObjectModel;

namespace Reader.Client.Interfaces
{
    /// <summary>
    /// 书源服务接口
    /// </summary>
    public interface IBookSourceService
    {
        /// <summary>
        /// 保存书源
        /// </summary>
        /// <param name="model">书源实体</param>
        /// <returns></returns>
        bool Save(BookSourceModel model);
        /// <summary>
        /// 获取单个书源
        /// </summary>
        /// <param name="id">标识键</param>
        /// <returns></returns>
        BookSourceModel SingleOrDefault(Guid id);
        /// <summary>
        /// 生成源
        /// </summary>
        /// <param name="model">书源模型</param>
        /// <returns></returns>
        string Generate(BookSourceModel model);

        /// <summary>
        /// 获取所有书源
        /// </summary>
        /// <returns></returns>
        ObservableCollection<BookSourceModel> GetAll();
        /// <summary>
        /// 删除书源
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        bool Remove(Guid id);
    }
}
