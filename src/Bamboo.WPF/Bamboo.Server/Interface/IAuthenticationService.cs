using Bamboo.Common;
using Bamboo.Server.Models;
using System.Threading.Tasks;

namespace Bamboo.Server.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<ServerResponse> LoginAsync(string account, string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        Task<ServerResponse> Resgiter(UserDto userDto);
    }
}
