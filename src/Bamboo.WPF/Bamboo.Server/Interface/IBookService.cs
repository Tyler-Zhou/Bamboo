using Bamboo.Library.Common.Dto;
using Bamboo.Library.Common.Parameter;
using Bamboo.Server.Models;
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
