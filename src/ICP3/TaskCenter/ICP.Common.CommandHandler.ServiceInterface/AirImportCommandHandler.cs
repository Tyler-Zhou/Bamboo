using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Collections.Generic;

namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 空运进口方法
    /// </summary>
    public class AirImportCommandHandler : IBaseComnandHandler
    {
        #region Fields & Property & Services
        /// <summary>
        /// WorkItem
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

        /// <summary>
        /// 当前业务面板
        /// </summary>
        public BaseBusinessPart CurrentBaseBusinessPart
        {
            get { return RootWorkItem.State["CurrentBaseBusinessPart"] as BaseBusinessPart; }
            set { RootWorkItem.State["CurrentBaseBusinessPart"] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IICPCommonOperationService IcpCommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }

        /// <summary>
        /// 业务面板需要传递的信息类
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
                    OperationType = OperationType.AirImport,
                    CurrentBusinessPart = CurrentBaseBusinessPart,
                    Updatetime = CurrentBaseBusinessPart.Updatetime
                };
            }
        } 
        #endregion

        #region 方法
        /// <summary>
        /// 新增订舱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportAddBooking(object sender, EventArgs e)
        {
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Create, true);
            object value;
            dictionary.TryGetValue("businessOperationParameter", out value);
            if (value != null && value.GetType() == typeof(BusinessOperationParameter))
            {
                BusinessOperationParameter businessOperation = value as BusinessOperationParameter;
                if (businessOperation != null) businessOperation.ContactStage = ContactStage.SO;
            }
            IcpCommonOperationService.AirImportAddBooking(dictionary);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportCopyBooking(object sender, EventArgs e)
        {
            var id = BusinessPartParam.ID;
            var editPartShowCriteria = new EditPartShowCriteria
            {
                BillNo = BusinessPartParam.ID,
                OperationNo = BusinessPartParam.OperationNo
            };
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.AirImportCopyBooking(editPartShowCriteria, dictionary);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportEditBooking(object sender, EventArgs e)
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
                IcpCommonOperationService.AirImportEditBooking(editPartShowCriteria, dictionary);
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportCancelBooking(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportCancelBooking(BusinessPartParam.ID);
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportDownLoad(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportAiDownLoad();
        }
        /// <summary>
        /// 打开账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportOpenBill(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportOpenBill(BusinessPartParam.ID);
        }
        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportPrintArrivalNotice(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportPrintArrivalNotice(BusinessPartParam.ID);
        }
        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportPrintReleaseOrder(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportPrintReleaseOrder(BusinessPartParam.ID);
        }
        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportPrintProfit(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportPrintProfit(BusinessPartParam.ID);
        }
        /// <summary>
        /// 打印Authority To Make Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportPrintAuthority(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportPrintAuthority(BusinessPartParam.ID);
        }

        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportOpenCargoBook(object sender, EventArgs e)
        {
            var id = BusinessPartParam.ID;
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.AirImportOpenCargoBook(id, dictionary);
        }


        /// <summary>
        /// 业务转移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportBusinessTransfer(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportBusinessTransfer(BusinessPartParam.ID);
        }


        /// <summary>
        /// 放货和取消放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AirImportDelivery(object sender, EventArgs e)
        {
            IcpCommonOperationService.AirImportAiDelivery(BusinessPartParam.ID);
        }

        #endregion

    }
}
