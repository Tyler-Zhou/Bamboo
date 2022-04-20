using Bamboo.Common;
using Bamboo.Common.Parameter;
using System.Threading.Tasks;

namespace Bamboo.Client.Core.Interface
{
    /// <summary>
    /// 服务基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// 添加数据(TEntity)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ReceiveResponse<TEntity>> AddAsync(TEntity entity);
        /// <summary>
        /// 更新数据(TEntity)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ReceiveResponse<TEntity>> UpdateAsync(TEntity entity);
        /// <summary>
        /// 删除数据根据唯一键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ReceiveResponse> DeleteAsync(int id);
        /// <summary>
        /// 获取单个数据根据唯一键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ReceiveResponse<TEntity>> GetFirstOfDefaultAsync(int id);
        /// <summary>
        /// 获取分页数据(TEntity)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<ReceiveResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter);
    }
}
