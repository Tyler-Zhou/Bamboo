using Bamboo.Server.Models;
using Bamboo.Server.Context;
using Bamboo.Server.Core;

namespace Bamboo.Server.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class ChapterRepository : Repository<ChapterEntity>, IRepository<ChapterEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public ChapterRepository(DefaultContext dbContext) : base(dbContext)
        {
        }
    }
}
