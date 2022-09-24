using Reader.Client.Models;
using System.Collections.ObjectModel;

namespace Reader.Client.Interfaces
{
    /// <summary>
    /// 书籍服务接口
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// 保存书籍
        /// </summary>
        /// <param name="model">书籍实体</param>
        /// <returns></returns>
        bool Save(BookModel model);

        /// <summary>
        /// 获取所有书籍
        /// </summary>
        /// <returns></returns>
        ObservableCollection<BookModel> GetAll();

        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Remove(BookModel model);
    }
}
