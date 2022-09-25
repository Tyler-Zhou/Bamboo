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
        /// 获取所有章节
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <returns></returns>
        ObservableCollection<ChapterModel> GetAll(string bookKey);

        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Remove(ChapterModel model);
    }
}
