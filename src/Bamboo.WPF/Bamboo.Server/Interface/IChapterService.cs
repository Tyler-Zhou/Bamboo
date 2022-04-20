using Bamboo.Library.Common.Dto;
using Bamboo.Library.Common.Parameter;
using Bamboo.Server.Models;
using System.Threading.Tasks;

namespace Bamboo.Server.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IChapterService : IBaseService<ChapterDto>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ServerResponse> GetAllAsync(ChapterParameter query);
    }
}
