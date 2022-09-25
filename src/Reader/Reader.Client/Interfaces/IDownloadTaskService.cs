using Reader.Client.Models;
using System.Collections.ObjectModel;

namespace Reader.Client.Interfaces
{
    /// <summary>
    /// 下载任务服务接口
    /// </summary>
    public interface IDownloadTaskService
    {
        /// <summary>
        /// 保存下载任务
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        bool Save(DownloadTaskModel model);
        /// <summary>
        /// 下载任务
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <param name="key">任务 Key</param>
        /// <returns></returns>
        DownloadTaskModel SingleOrDefault(string bookKey,string key);
        /// <summary>
        /// 获取下载任务
        /// </summary>
        /// <param name="bookKey">书籍 Key</param>
        /// <returns></returns>
        ObservableCollection<DownloadTaskModel> GetAll(string bookKey);
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);
    }
}
