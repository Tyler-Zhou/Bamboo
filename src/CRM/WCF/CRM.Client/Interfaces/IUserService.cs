using CRM.Client.Models;
using System.Collections.ObjectModel;

namespace CRM.Client.Interfaces
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        long Add(UserInfo user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Update(UserInfo user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool Delete(UserInfo user);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserInfo GetById(long id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ObservableCollection<UserInfo> GetAll();
    }
}
