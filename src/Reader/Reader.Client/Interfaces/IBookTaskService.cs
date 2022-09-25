using Reader.Client.Models;
using System.Collections.ObjectModel;

namespace Reader.Client.Interfaces
{
    /// <summary>
    /// 章节任务服务接口
    /// </summary>
    public interface IBookTaskService
    {
        /// <summary>
        /// 保存书籍下载任务
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        bool Save(BookTaskModel model);

        /// <summary>
        /// 获取书籍下载任务
        /// </summary>
        /// <returns></returns>
        ObservableCollection<BookTaskModel> GetAll();
    }
}
