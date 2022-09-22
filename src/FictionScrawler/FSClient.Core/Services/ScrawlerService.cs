using FSClient.Models;
using System.Collections.ObjectModel;

namespace FSClient.Core
{
    /// <summary>
    /// 爬虫服务
    /// </summary>
    public class ScrawlerService: IScrawlerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyWord">键入搜索词</param>
        /// <param name="bookSource">书源</param>
        /// <returns></returns>
        public ObservableCollection<BookModel> SearchBookPost(string keyWord, BookSourceModel bookSource)
        {
            ObservableCollection<BookModel> books = new ObservableCollection<BookModel>();
            return books;
        }
    }
}
