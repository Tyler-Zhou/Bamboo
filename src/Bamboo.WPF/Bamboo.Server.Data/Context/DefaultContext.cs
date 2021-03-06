using Bamboo.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bamboo.Server.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<UserEntity> UserCollection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<BookEntity> BookCollection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<ChapterEntity> ChapterCollection { get; set; }
    }
}
