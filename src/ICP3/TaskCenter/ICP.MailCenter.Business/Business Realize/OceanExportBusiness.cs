using ICP.TaskCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 海出业务的不同处理实现该类必须实现IBusiness接口，
 
    /// </summary>
    public class OceanExportBusiness : IBusiness

    {
        #region IBusiness 成员

        /// <summary>
        /// 操作视图服务对象
        /// </summary>
        private  IOperationViewService OperationViewService
        {
            get {
                return ServiceClient.GetService<IOperationViewService>();
            }
        }

        /// <summary>
        /// 保存修改业务信息
        /// </summary>
        /// <param name="updateBuisinessParam">修改业务信息对象</param>
        /// <returns></returns>
        public bool SaveEditData(ICP.TaskCenter.ServiceInterface.UpdateBuisinessParam updateBuisinessParam)
        {
           return  OperationViewService.SaveOceanExportData(updateBuisinessParam);
        }

        #endregion
    }
}
