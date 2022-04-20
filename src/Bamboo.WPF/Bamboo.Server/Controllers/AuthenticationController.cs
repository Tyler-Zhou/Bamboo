using Bamboo.Common;
using Bamboo.Server.Interface;
using Bamboo.Server.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bamboo.Server.Controllers
{
    /// <summary>
    /// 验证控制器
    /// </summary>
    [ApiController]
    [Route("Bamboo/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// 登录服务
        /// </summary>
        private readonly IAuthenticationService _AuthenticationService;
        /// <summary>
        /// 验证控制器
        /// </summary>
        /// <param name="authenticationService">登录服务</param>
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _AuthenticationService = authenticationService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Login([FromBody] UserDto param) =>
           await _AuthenticationService.LoginAsync(param.Account, param.Password);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Resgiter([FromBody] UserDto param) =>
            await _AuthenticationService.Resgiter(param);
    }
}
