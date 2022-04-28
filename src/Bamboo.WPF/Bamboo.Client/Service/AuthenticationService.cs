using Bamboo.Client.Core.Common;
using Bamboo.Client.Core.Models;
using Bamboo.Client.Interface;
using Bamboo.Entities;
using System.Threading.Tasks;

namespace Bamboo.Client.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ClientService _ClientService;
        /// <summary>
        /// 
        /// </summary>
        private readonly string _ServiceName = "Authentication";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientService"></param>
        public AuthenticationService(ClientService clientService)
        {
            _ClientService = clientService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ReceiveResponse> Login(UserDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"{ApplicationConstant.BASE_SERVICE_NAME}/{_ServiceName}/Login";
            request.Parameter = user;
            return await _ClientService.ExecuteAsync(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<ReceiveResponse> Resgiter(UserDto user)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"{ApplicationConstant.BASE_SERVICE_NAME}/{_ServiceName}/Resgiter";
            request.Parameter = user;
            return await _ClientService.ExecuteAsync(request);
        }
    }
}
