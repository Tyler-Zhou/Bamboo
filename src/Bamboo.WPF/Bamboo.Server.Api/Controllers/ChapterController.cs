using Bamboo.Library.Entities;
using Bamboo.Server.Entities;
using Bamboo.Server.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bamboo.Server.Api.Controllers
{
    /// <summary>
    /// 书籍章节控制器
    /// </summary>
    [ApiController]
    [Route("Bamboo/[controller]/[action]")]
    public class ChapterController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IChapterService _ChapterService;
        /// <summary>
        /// 书籍章节控制器
        /// </summary>
        /// <param name="chapterService">章节服务</param>
        public ChapterController(IChapterService chapterService)
        {
            _ChapterService = chapterService;
        }
        /// <summary>
        /// 获取单个章节
        /// </summary>
        /// <param name="id">章节Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServerResponse> Get(int id) => await _ChapterService.GetSingleAsync(id);
        /// <summary>
        /// 获取所有章节(分页)
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServerResponse> GetAll([FromQuery] ChapterParameter param) => await _ChapterService.GetAllAsync(param);
        /// <summary>
        /// 添加章节
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Add([FromBody] ChapterDto model) => await _ChapterService.AddAsync(model);
        /// <summary>
        /// 更新章节
        /// </summary>
        /// <param name="model">章节实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ServerResponse> Update([FromBody] ChapterDto model) => await _ChapterService.UpdateAsync(model);
        /// <summary>
        /// 删除章节
        /// </summary>
        /// <param name="id">章节Id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ServerResponse> Delete(int id) => await _ChapterService.DeleteAsync(id);
    }
}
