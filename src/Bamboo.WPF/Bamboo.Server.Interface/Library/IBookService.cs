using Bamboo.Library.Entities;
using Bamboo.Server.Entities;
using System.Threading.Tasks;

namespace Bamboo.Server.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookService : IBaseService<BookDto>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ServerResponse> GetAllAsync(BookParameter query);
    }
}
