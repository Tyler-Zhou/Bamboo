using AutoMapper;
using Bamboo.Common;
using Bamboo.Server.Models;
using Bamboo.Server.Core;
using Bamboo.Server.Extensions;
using Bamboo.Server.Interface;
using System;
using System.Threading.Tasks;

namespace Bamboo.Server.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticationService : IAuthenticationService
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
        public AuthenticationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ServerResponse> LoginAsync(string account, string password)
        {
            try
            {
                password = password.GetMD5();

                var model = await _UnitOfWork.GetRepository<UserEntity>().GetFirstOrDefaultAsync(predicate:
                    x => (x.Account.Equals(account)) &&
                    (x.Password.Equals(password)));

                if (model == null)
                    return new ServerResponse("账号或密码错误,请重试！");

                return new ServerResponse(true, new UserDto()
                {
                    Account = model.Account,
                    UserName = model.UserName,
                    Id = model.Id
                });
            }
            catch (Exception ex)
            {
                return new ServerResponse($"登录失败！异常:{ex.Message}");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<ServerResponse> Resgiter(UserDto userDto)
        {
            try
            {
                var model = _Mapper.Map<UserEntity>(userDto);
                var repository = _UnitOfWork.GetRepository<UserEntity>();
                var userModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Account.Equals(model.Account));

                if (userModel != null)
                    return new ServerResponse($"当前账号:{model.Account}已存在,请重新注册！");

                model.CreateDate = DateTime.Now;
                model.Password = model.Password.GetMD5();
                await repository.InsertAsync(model);

                if (await _UnitOfWork.SaveChangesAsync() > 0)
                    return new ServerResponse(true, model);

                return new ServerResponse("注册失败,请稍后重试！");
            }
            catch (Exception ex)
            {
                return new ServerResponse($"注册账号失败！{ex.Message}");
            }
        }
    }
}
