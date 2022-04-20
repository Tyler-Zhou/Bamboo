using Bamboo.Server.Models;
using Bamboo.Server.Context;
using Bamboo.Server.Core;

namespace Bamboo.Server.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class BookRepository : Repository<BookEntity>, IRepository<BookEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public BookRepository(DefaultContext dbContext) : base(dbContext)
        {
        }
    }
}
