using Reader.Client.Models;
using System.Collections.ObjectModel;

namespace Reader.Client.Interfaces
{
    /// <summary>
    /// 模板服务接口
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// 保存集合
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="collection">集合</param>
        /// <returns></returns>
        bool SaveCollection(string name, ObservableCollection<BaseDataModel> collection);

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        ObservableCollection<BaseDataModel> GetCollection(string name);

        /// <summary>
        /// 保存单个
        /// </summary>
        /// <param name="model">首页模板</param>
        /// <returns></returns>
        bool SaveSingle(BaseDataModel model);

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        BaseDataModel GetSingle(string name);
    }
}
