using Bamboo.Client.Core.Models;
using Bamboo.Entities;
using System.Threading.Tasks;

namespace Bamboo.Client.Interface
{
    /// <summary>
    /// 验证服务
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user">用户传输对象</param>
        /// <returns></returns>
        Task<ReceiveResponse> Login(UserDto user);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user">用户注册传输对象</param>
        /// <returns></returns>
        Task<ReceiveResponse> Resgiter(UserDto user);
    }
}
