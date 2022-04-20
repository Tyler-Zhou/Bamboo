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
        private readonly ILoginService _LoginService;
        /// <summary>
        /// 验证控制器
        /// </summary>
        /// <param name="loginService">登录服务</param>
        public AuthenticationController(ILoginService loginService)
        {
            _LoginService = loginService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Login([FromBody] UserDto param) =>
           await _LoginService.LoginAsync(param.Account, param.Password);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Resgiter([FromBody] UserDto param) =>
            await _LoginService.Resgiter(param);
    }
}
