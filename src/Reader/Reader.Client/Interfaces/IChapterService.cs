using Reader.Client.Models;
using System.Collections.ObjectModel;

namespace Reader.Client.Interfaces
{
    /// <summary>
    /// 章节服务接口
    /// </summary>
    public interface IChapterService
    {
        /// <summary>
        /// 保存章节
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        bool Save(ChapterModel model);
        /// <summary>
        /// 单个章节
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <param name="key">章节 Key</param>
        /// <returns></returns>
        ChapterModel SingleOrDefault(string bookKey, string key);

        /// <summary>
        /// 获取所有章节Key
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <returns></returns>
        ObservableCollection<string> GetAllKey(string bookKey);

        /// <summary>
        /// 获取所有章节
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <returns></returns>
        ObservableCollection<ChapterModel> GetAll(string bookKey);

        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="key">标识键</param>
        /// <returns></returns>
        bool Remove(string key);
    }
}
