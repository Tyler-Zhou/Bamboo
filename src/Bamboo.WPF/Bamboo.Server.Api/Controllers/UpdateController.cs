using Bamboo.Entities;
using Bamboo.Server.Entities;
using Bamboo.Server.Interface;
using GeneralUpdate.AspNetCore.Services;
using GeneralUpdate.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bamboo.Server.Api.Controllers
{
    /// <summary>
    /// 更新控制器
    /// </summary>
    [ApiController]
    [Route("Bamboo/[controller]/[action]")]
    public class UpdateController : ControllerBase
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        ILogger _Logger;

        /// <summary>
        /// 更新服务
        /// </summary>
        private readonly IUpdateService _UpdateService;
        /// <summary>
        /// 更新控制器
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="updateService">更新服务</param>
        public UpdateController(ILogger logger, IUpdateService updateService)
        {
            _Logger = logger;
            _UpdateService = updateService;
        }

        /// <summary>
        /// https://localhost:5001/api/update/getUpdateVersions/1/1.1.1
        /// </summary>
        /// <param name="clientType">1:ClientApp 2:UpdateApp</param>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        [HttpGet("Versions/{clientType}/{clientVersion}")]
        public async Task<IActionResult> Versions(int clientType, string clientVersion)
        {
            _Logger.LogInformation("Client request 'Versions'.");
            var resultJson = await _UpdateService.UpdateVersionsTaskAsync(clientType, clientVersion, UpdateVersions);
            return Ok(resultJson);
        }

        


        /// <summary>
        /// https://localhost:5001/Bamboo/update/Validate/1/1.1.1
        /// </summary>
        /// <param name="clientType">1:ClientApp 2:UpdateApp</param>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        [HttpGet("Validate/{clientType}/{clientVersion}")]
        public async Task<IActionResult> Validate(int clientType, string clientVersion)
        {
            _Logger.LogInformation("Client request 'Validate'.");
            var lastVersion = GetLastVersion();
            var resultJson = await _UpdateService.UpdateValidateTaskAsync(clientType, clientVersion, lastVersion, true, GetValidateInfos);
            return Ok(resultJson);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Task<List<UpdateVersionDTO>> UpdateVersions(int clientType, string clientVersion)
        {
            throw new System.NotImplementedException();
        }

        private string GetLastVersion()
        {
            throw new System.NotImplementedException();
        }

        private Task<List<UpdateVersionDTO>> GetValidateInfos(int clientType, string clientVersion)
        {
            throw new System.NotImplementedException();
        }
    }
}
