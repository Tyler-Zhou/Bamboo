using Bamboo.Library.Entities;
using Bamboo.Server.Entities;
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
