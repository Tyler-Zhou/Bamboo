using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;


namespace ICP.FCM.OceanExport.ServiceInterface
{
    /// <summary>
    /// 开放的业务客户端服务接口
    /// </summary>
    public interface IWorkOnOceanExportClientService
    {
        /// <summary>
        /// 打开业务添加界面
        /// </summary>
        /// <param name="operationType">业务类型</param>
        /// <param name="sender">发件人</param>
        /// <param name="workspaceName"></param>
        void ShowOperationAddPart(OperationType operationType, string sender);
        /// <summary>
        /// 打开业务编辑界面
        /// </summary>
        /// <param name="editOperationType"></param>
        void ShowOperationEditPart(OriginType originType, object tag);
        /// <summary>
        /// 打开业务列表界面
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="sender"></param>
        void ShowOperationListPart(ICP.Framework.CommonLibrary.Common.OperationType operationType, string sender);
        /// <summary>
        /// 执行本地服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="tag"></param>
        void ExecOperationService(ServiceType serviceType, object tag);

        void OpenBillInfoPart(Guid operationID);
    }
}
