using Bamboo.Client.Core.Common;
using Bamboo.Client.Core.Models;
using Bamboo.Client.Service;
using Bamboo.Common;
using Bamboo.Book.Client.Interface;
using Bamboo.Book.Common.Dto;
using Bamboo.Book.Common.Parameter;
using System.Threading.Tasks;

namespace Bamboo.Book.Client.Service
{
    /// <summary>
    /// 书籍服务
    /// </summary>
    public class BookService : BaseService<BookDto>, IBookService
    {
        /// <summary>
        /// 客户端服务
        /// </summary>
        private readonly ClientService _ClientService;
        /// <summary>
        /// 服务名称
        /// </summary>
        private const string _ServiceName = "Book";

        /// <summary>
        /// 书籍服务
        /// </summary>
        /// <param name="clientService">客户端服务</param>
        public BookService(ClientService clientService) : base(clientService, _ServiceName)
        {
            _ClientService = clientService;
        }
        /// <summary>
        /// 获取筛选后的分页书籍对象
        /// </summary>
        /// <param name="parameter">查询参数</param>
        /// <returns></returns>
        public async Task<ReceiveResponse<PagedList<BookDto>>> GetAllFilterAsync(BookParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"{MainConstants.BASE_SERVICE_NAME}/{_ServiceName}/GetAll?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}" +
                $"&status={parameter.Status}";
            return await _ClientService.ExecuteAsync<PagedList<BookDto>>(request);
        }

    }
}
