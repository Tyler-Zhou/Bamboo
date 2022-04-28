using Bamboo.Server.Context;
using Bamboo.Server.Core;
using Bamboo.Server.Entities;

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
