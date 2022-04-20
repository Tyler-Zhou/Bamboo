using Bamboo.Client.Core.Interface;
using Bamboo.Common;
using Bamboo.Library.Common.Dto;
using Bamboo.Library.Common.Parameter;
using System.Threading.Tasks;

namespace Bamboo.Library.Client.Interface
{
    /// <summary>
    /// 书籍服务
    /// </summary>
    public interface IBookService : IBaseService<BookDto>
    {
        /// <summary>
        /// 获取筛选后的分页书籍对象
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns></returns>
        Task<ReceiveResponse<PagedList<BookDto>>> GetAllFilterAsync(BookParameter parameter);
    }
}
