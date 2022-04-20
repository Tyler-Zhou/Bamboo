using Bamboo.Server.Models;
using Bamboo.Server.Context;
using Bamboo.Server.Core;

namespace Bamboo.Server.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRepository : Repository<UserEntity>, IRepository<UserEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(DefaultContext dbContext) : base(dbContext)
        {
        }
    }
}
