using AutoMapper;
using Bamboo.Library.Common.Dto;
using Bamboo.Library.Common.Parameter;
using Bamboo.Common.Parameter;
using Bamboo.Server.Core;
using Bamboo.Server.Interface;
using Bamboo.Server.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bamboo.Server.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class BookService : IBookService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _UnitOfWork;
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _Mapper;
        private readonly ILogService _LogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <param name="logService"></param>
        public BookService(IUnitOfWork unitOfWork, IMapper mapper, ILogService logService )
        {
            _LogService = logService;
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServerResponse> AddAsync(BookDto dto)
        {
            try
            {
                var updateModel = _Mapper.Map<BookEntity>(dto);
                updateModel.CreateDate = DateTime.Now;
                await _UnitOfWork.GetRepository<BookEntity>().InsertAsync(updateModel);
                if (await _UnitOfWork.SaveChangesAsync() > 0)
                    return new ServerResponse(true, updateModel);
                return new ServerResponse("添加数据失败");
            }
            catch (Exception ex)
            {
                _LogService.LogError(ex);
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServerResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = _UnitOfWork.GetRepository<BookEntity>();
                var updateModel = await repository
                    .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(updateModel);
                if (await _UnitOfWork.SaveChangesAsync() > 0)
                    return new ServerResponse(true, "");
                return new ServerResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<ServerResponse> GetAllAsync(QueryParameter parameter)
        {
            try
            {
                var repository = _UnitOfWork.GetRepository<BookEntity>();
                var models = await repository.GetPagedListAsync(predicate:
                   x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ServerResponse(true, models);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<ServerResponse> GetAllAsync(BookParameter parameter)
        {
            try
            {
                var repository = _UnitOfWork.GetRepository<BookEntity>();
                var models = await repository.GetPagedListAsync(predicate:
                   x => (string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search))
                   && (parameter.Status == null ? true : x.Status.Equals(parameter.Status)),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderByDescending(t => t.CreateDate));
                return new ServerResponse(true, models);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServerResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = _UnitOfWork.GetRepository<BookEntity>();
                var updateModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ServerResponse(true, updateModel);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServerResponse> UpdateAsync(BookDto dto)
        {
            try
            {
                var model = _Mapper.Map<BookEntity>(dto);
                var repository = _UnitOfWork.GetRepository<BookEntity>();
                var updateModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(model.Id));

                updateModel.Key = model.Key;
                updateModel.Name = model.Name;
                updateModel.Author = model.Author;
                updateModel.Link = model.Link;
                updateModel.Tag = model.Tag;
                updateModel.Introduction = model.Introduction;
                updateModel.Status = model.Status;
                updateModel.UpdateDate = DateTime.Now;

                repository.Update(updateModel);

                if (await _UnitOfWork.SaveChangesAsync() > 0)
                    return new ServerResponse(true, updateModel);
                return new ServerResponse("更新数据异常！");
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
    }
}
