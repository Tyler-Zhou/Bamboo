using ICP.TaskCenter.ServiceInterface;

namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 不同业务的不同处理的的接口实现该接口的类必须以类型名称+Business问类名，
    /// 如海出为OceanExportBusiness，海进为OceanImportBusiness,具体类型请参考业务类型ICP.Framework.CommonLibrary.Common.OperationType
    /// </summary>
    public interface IBusiness
    {
        /// <summary>
        /// 保存修改业务信息
        /// </summary>
        /// <param name="updateBuisinessParam">修改业务信息对象</param>
        /// <returns></returns>
        bool SaveEditData(UpdateBuisinessParam updateBuisinessParam);
    }
}
