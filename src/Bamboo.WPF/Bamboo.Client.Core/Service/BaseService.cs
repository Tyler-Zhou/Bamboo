using Bamboo.Client.Core.Common;
using Bamboo.Client.Core.Interface;
using Bamboo.Client.Core.Models;
using Bamboo.Entities;
using System.Threading.Tasks;

namespace Bamboo.Client.Service
{
    /// <summary>
    /// 服务基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// 客户端服务
        /// </summary>
        private readonly ClientService _ClientService;
        /// <summary>
        /// 服务名称
        /// </summary>
        private readonly string _ServiceName;
        /// <summary>
        /// 服务基类
        /// </summary>
        /// <param name="clientService">客户端服务</param>
        /// <param name="serviceName">服务名称</param>
        public BaseService(ClientService clientService, string serviceName)
        {
            _ClientService = clientService;
            _ServiceName = serviceName;
        }
        /// <summary>
        /// 新增[TEntity]
        /// </summary>
        /// <param name="entity">实体[TEntity]对象</param>
        /// <returns></returns>
        public async Task<ReceiveResponse<TEntity>> AddAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"{ApplicationConstant.BASE_SERVICE_NAME}/{_ServiceName}/Add";
            request.Parameter = entity;
            return await _ClientService.ExecuteAsync<TEntity>(request);
        }
        /// <summary>
        /// 删除[TEntity]
        /// </summary>
        /// <param name="id">[TEntity] 唯一键</param>
        /// <returns></returns>
        public async Task<ReceiveResponse> DeleteAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.DELETE;
            request.Route = $"{ApplicationConstant.BASE_SERVICE_NAME}/{_ServiceName}/Delete?id={id}";
            return await _ClientService.ExecuteAsync(request);
        }
        /// <summary>
        /// 获取分页[TEntity]
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns></returns>
        public async Task<ReceiveResponse<PagedList<TEntity>>> GetAllAsync(QueryParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"{ApplicationConstant.BASE_SERVICE_NAME}/{_ServiceName}/GetAll?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}";
            return await _ClientService.ExecuteAsync<PagedList<TEntity>>(request);
        }
        /// <summary>
        /// 获取单个[TEntity]
        /// </summary>
        /// <param name="id">[TEntity] 唯一键</param>
        /// <returns></returns>
        public async Task<ReceiveResponse<TEntity>> GetFirstOfDefaultAsync(int id)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"{ApplicationConstant.BASE_SERVICE_NAME}/{_ServiceName}/Get?id={id}";
            return await _ClientService.ExecuteAsync<TEntity>(request);
        }
        /// <summary>
        /// 更新单个[TEntity]
        /// </summary>
        /// <param name="entity">实体[TEntity]对象</param>
        /// <returns></returns>
        public async Task<ReceiveResponse<TEntity>> UpdateAsync(TEntity entity)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"{ApplicationConstant.BASE_SERVICE_NAME}/{_ServiceName}/Update";
            request.Parameter = entity;
            return await _ClientService.ExecuteAsync<TEntity>(request);
        }
    }
}
