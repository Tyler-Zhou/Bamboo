using System;
using System.Collections.Generic;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 空运出口方法
    /// </summary>
    public class AirExportCommandHandler : IBaseComnandHandler
    {
        /// <summary>
        /// RootWorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        /// <summary>
        /// 公共命令处理类
        /// </summary>
        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public CommonCommandHandler CommandHandler
        {
            get;
            set;
        }

        public BaseBusinessPart CurrentBaseBusinessPart
        {
            get { return RootWorkItem.State["CurrentBaseBusinessPart"] as BaseBusinessPart; }
            set { RootWorkItem.State["CurrentBaseBusinessPart"] = value; }
        }

        /// <summary>
        /// 公共服务接口类
        /// </summary>
        public IICPCommonOperationService IcpCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }

        /// <summary>
        /// 实体对象
        /// </summary>
        public BusinessPartParam BusinessPartParam
        {
            get
            {
                return new BusinessPartParam
                {
                    TemplateCode = CurrentBaseBusinessPart.TemplateCode,
                    OperationNo = CurrentBaseBusinessPart.OperationNo,
                    ID = CurrentBaseBusinessPart.OperationID,
                    OperationType = OperationType.AirExport,
                    CurrentBusinessPart = CurrentBaseBusinessPart,
                    Updatetime = CurrentBaseBusinessPart.Updatetime
                };
            }

        }


        #region  方法
        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirExportAddData(object sender, EventArgs e)
        {
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Create, true);
            object value;
            dictionary.TryGetValue("businessOperationParameter", out value);
            if (value.GetType() == typeof(BusinessOperationParameter))
            {
                BusinessOperationParameter businessOperation = value as BusinessOperationParameter;
                businessOperation.ContactStage = ContactStage.SO;
            }
            IcpCommonOperationService.AirExportAddData(dictionary);
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirExportCopyData(object sender, EventArgs e)
        {
            var id = BusinessPartParam.ID;
            var editPartShowCriteria = new EditPartShowCriteria
            {
                BillNo = BusinessPartParam.ID,
                OperationNo = BusinessPartParam.OperationNo
            };
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.AirExportCopyData(editPartShowCriteria, dictionary);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirExportEditData(object sender, EventArgs e)
        {
            if (BusinessPartParam != null && BusinessPartParam.ID != Guid.Empty &&
                BusinessPartParam.OperationNo != string.Empty)
            {
                var editPartShowCriteria = new EditPartShowCriteria
                {
                    BillNo = BusinessPartParam.ID,
                    OperationNo = BusinessPartParam.OperationNo
                };
                Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
                IcpCommonOperationService.AirExportEditData(editPartShowCriteria, dictionary);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirExportCancelData(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirExportCancelData(BusinessPartParam.ID);
        }

        /// <summary>
        /// 申请代理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirExportReplyAgent(object sender, EventArgs e)
        {
            var id = BusinessPartParam.ID;
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.AirExportReplyAgent(id, dictionary);
        }

        /// <summary>
        /// 提单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirExportOpenBl(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirExportOpenBl(BusinessPartParam.ID);
        }

        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirExportOpenBill(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirExportOpenBill(BusinessPartParam.ID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirExportPrintOrder(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirExportPrintOrder(BusinessPartParam.ID);
        }

        #endregion

    }
}
