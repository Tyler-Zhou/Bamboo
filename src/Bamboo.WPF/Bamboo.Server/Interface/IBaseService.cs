using Bamboo.Common.Parameter;
using Bamboo.Server.Models;
using System.Threading.Tasks;

namespace Bamboo.Server.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ServerResponse> GetAllAsync(QueryParameter query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServerResponse> GetSingleAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServerResponse> AddAsync(T dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ServerResponse> UpdateAsync(T dto);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServerResponse> DeleteAsync(int id);
    }
}
