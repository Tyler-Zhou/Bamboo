using AutoMapper;
using Bamboo.Common.Parameter;
using Bamboo.Library.Common.Dto;
using Bamboo.Library.Common.Parameter;
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
    public class ChapterService : IChapterService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IUnitOfWork _UnitOfWork;
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _Mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public ChapterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ServerResponse> AddAsync(ChapterDto dto)
        {
            try
            {
                var updateModel = _Mapper.Map<ChapterEntity>(dto);
                updateModel.CreateDate = DateTime.Now;
                await _UnitOfWork.GetRepository<ChapterEntity>().InsertAsync(updateModel);
                if (await _UnitOfWork.SaveChangesAsync() > 0)
                    return new ServerResponse(true, updateModel);
                return new ServerResponse("添加数据失败");
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
        public async Task<ServerResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = _UnitOfWork.GetRepository<ChapterEntity>();
                var todo = await repository
                    .GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                repository.Delete(todo);
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
                var repository = _UnitOfWork.GetRepository<ChapterEntity>();
                var todos = await repository.GetPagedListAsync(
                    selector:source=>new ChapterEntity() {
                        Id=source.Id,
                        BookKey=source.BookKey,
                        Name=source.Name,
                        Link = source.Link,
                        Content = "",
                        CreateDate = source.CreateDate,
                        UpdateDate = source.UpdateDate,
                    },
                    predicate:
                   x => string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderBy(t => t.OrderIndex)
                   );
                return new ServerResponse(true, todos);
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
        public async Task<ServerResponse> GetAllAsync(ChapterParameter parameter)
        {
            try
            {
                var repository = _UnitOfWork.GetRepository<ChapterEntity>();
                var todos = await repository.GetPagedListAsync(
                    selector: source => new ChapterEntity()
                    {
                        Id = source.Id,
                        BookKey = source.BookKey,
                        Name = source.Name,
                        Link = source.Link,
                        Content = "",
                        CreateDate = source.CreateDate,
                        UpdateDate = source.UpdateDate,
                    },
                    predicate:
                   x => (string.IsNullOrWhiteSpace(parameter.Search) ? true : x.Name.Contains(parameter.Search))
                   && (x.BookKey.Equals(parameter.BookKey)),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderBy(t => t.OrderIndex)
                   );
                return new ServerResponse(true, todos);
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
                var repository = _UnitOfWork.GetRepository<ChapterEntity>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(id));
                return new ServerResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ServerResponse> UpdateAsync(ChapterDto model)
        {
            try
            {
                var dbToDo = _Mapper.Map<ChapterEntity>(model);
                var repository = _UnitOfWork.GetRepository<ChapterEntity>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id.Equals(dbToDo.Id));

                todo.Key = dbToDo.Key;
                todo.Name = dbToDo.Name;
                todo.Content = dbToDo.Content;
                todo.Link = dbToDo.Link;
                todo.UpdateDate = DateTime.Now;

                repository.Update(todo);

                if (await _UnitOfWork.SaveChangesAsync() > 0)
                    return new ServerResponse(true, todo);
                return new ServerResponse("更新数据异常！");
            }
            catch (Exception ex)
            {
                return new ServerResponse(ex.Message);
            }
        }
    }
}
