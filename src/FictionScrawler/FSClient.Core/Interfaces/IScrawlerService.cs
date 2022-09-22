using FSClient.Models;
using System.Collections.ObjectModel;

namespace FSClient.Core
{
    /// <summary>
    /// 爬虫服务接口
    /// </summary>
    public interface IScrawlerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord">键入搜索词</param>
        /// <param name="bookSource">书源</param>
        /// <returns></returns>
        ObservableCollection<BookModel> SearchBookPost(string keyWord, BookSourceModel bookSource);
    }
}
