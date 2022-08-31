using Client.Models;
using System.Collections.ObjectModel;

namespace Client.Interfaces
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// 获取所有种族
        /// </summary>
        /// <returns></returns>
        ObservableCollection<RaceModel> GetAllRace();
        /// <summary>
        /// 获取所有职业
        /// </summary>
        /// <returns></returns>
        ObservableCollection<ClassModel> GetAllClass();
    }
}
