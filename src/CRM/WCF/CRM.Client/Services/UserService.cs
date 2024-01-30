using AutoMapper;
using CRM.Client.DataAccess;
using CRM.Client.Entities;
using CRM.Client.Interfaces;
using CRM.Client.Models;
using Dm.parser;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;

namespace CRM.Client.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _Mapper;

        public UserService(IMapper mapper)
        {
            _Mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Add(UserInfo model)
        {
            var modelEntity = _Mapper.Map<UserEntity>(model);
            modelEntity.CreateTime = DateTime.Now;
            UserRepository userRepository = new UserRepository();
            userRepository.AddAsync(modelEntity);
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(UserInfo model)
        {
            var modelEntity = _Mapper.Map<UserEntity>(model);
            modelEntity.CreateTime = DateTime.Now;
            UserRepository userRepository = new UserRepository();
            userRepository.UpdateAsync(modelEntity);
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(UserInfo model)
        {
            UserRepository userRepository = new UserRepository();
            userRepository.DeleteAsync(new List<UserEntity>());
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserInfo GetById(long id)
        {
            UserRepository userRepository = new UserRepository();
            var modelFind = userRepository.SingleAsync(new UserEntity { ID = id });
            return _Mapper.Map<UserInfo>(modelFind);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<UserInfo> GetAll()
        {
            // 查列表
            UserRepository userRepository = new UserRepository();
            var listModel = userRepository.ListAsync(new UserEntity { });

            return _Mapper.Map<ObservableCollection<UserInfo>>(listModel);
        }
    }
}
