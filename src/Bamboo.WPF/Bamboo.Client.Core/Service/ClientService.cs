using Bamboo.Client.Core.Helper;
using Bamboo.Client.Core.Models;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Bamboo.Client.Service
{
    /// <summary>
    /// 客户端服务
    /// </summary>
    public class ClientService
    {
        /// <summary>
        /// API URL地址
        /// </summary>
        private readonly string _ApiUrl;

        /// <summary>
        /// 客户端发送HTTP请求对象
        /// </summary>
        protected readonly RestClient _RestClient;
        /// <summary>
        /// 客户端服务
        /// </summary>
        /// <param name="apiUrl">API URL地址</param>
        public ClientService(string apiUrl)
        {
            _ApiUrl = apiUrl;
            _RestClient = new RestClient(apiUrl);
        }
        /// <summary>
        /// 执行请求并接受响应结果
        /// </summary>
        /// <param name="baseRequest">HTTP请求对象</param>
        /// <returns>HTTP响应结果对象</returns>
        public async Task<ReceiveResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);
            if (baseRequest.Parameter != null)
            {
                var param = JsonSerializerHelper.SerializeObject(baseRequest.Parameter);
                request.AddParameter("param", param, ParameterType.RequestBody);
            }
            _RestClient.BaseUrl = new Uri(_ApiUrl + baseRequest.Route);
            var response = await _RestClient.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializerHelper.DeserializeObject<ReceiveResponse>(response.Content);

            else
                return new ReceiveResponse()
                {
                    Status = false,
                    Result = null,
                    Message = response.ErrorMessage
                };
        }
        /// <summary>
        /// 执行请求并接收包含[T]类型响应结果
        /// </summary>
        /// <typeparam name="T">HTTP请求对象</typeparam>
        /// <param name="baseRequest"></param>
        /// <returns>包含[T]类型HTTP响应结果</returns>
        public async Task<ReceiveResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
            {
                var param = JsonSerializerHelper.SerializeObject(baseRequest.Parameter);
                request.AddParameter("param", param, ParameterType.RequestBody);
            }
            _RestClient.BaseUrl = new Uri(_ApiUrl + baseRequest.Route);
            var response = await _RestClient.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonSerializerHelper.DeserializeObject<ReceiveResponse<T>>(response.Content);

            else
                return new ReceiveResponse<T>()
                {
                    Status = false,
                    Message = response.ErrorMessage
                };
        }
    }
}
