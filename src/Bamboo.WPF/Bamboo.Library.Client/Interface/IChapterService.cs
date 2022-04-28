using Bamboo.Client.Core.Interface;
using Bamboo.Client.Core.Models;
using Bamboo.Entities;
using Bamboo.Library.Entities;
using System.Threading.Tasks;

namespace Bamboo.Library.Client.Interface
{
    /// <summary>
    /// 章节服务
    /// </summary>
    public interface IChapterService : IBaseService<ChapterDto>
    {
        /// <summary>
        /// 获取筛选后的分页章节对象
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns></returns>
        Task<ReceiveResponse<PagedList<ChapterDto>>> GetAllFilterAsync(ChapterParameter parameter);
    }
}
