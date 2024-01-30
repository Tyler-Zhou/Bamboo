using CRM.Client.Entities;
using CRM.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Client.DataAccess
{
    public class UserRepository : SqlSugarDBContext
    {
        public UserRepository() { }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddAsync(UserEntity model)
        {
            return db.Insertable(model).ExecuteCommand() > 0;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="Llist"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(List<UserEntity> Llist)
        {
            var result = true;
            db.Ado.BeginTran();
            try
            {
                foreach (var item in Llist)
                {
                    if (await db.Deleteable(item).ExecuteCommandAsync() >= 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            if (result)
            {
                db.Ado.CommitTran();
            }
            else
            {
                db.Ado.RollbackTran();
            }
            return result;
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<UserEntity> ListAsync(UserEntity model)
        {
            var result = db.Queryable<UserEntity>().ToList();
            return result;
        }

        /// <summary>
        /// 查询单条信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public UserEntity SingleAsync(UserEntity model)
        {
            return db.Queryable<UserEntity>().Single(it => it.ID == model.ID);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateAsync(UserEntity model)
        {
            return db.Updateable(model).UpdateColumns(it => new { it.Name, it.Password }).Where(it => it.ID == model.ID).ExecuteCommand() > 0;
        }
    }
}
