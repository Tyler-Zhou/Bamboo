using System.Collections.Generic;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using ICP.Operation.Common.UI;
using Microsoft.Practices.CompositeUI;
using System;
using Microsoft.Practices.ObjectBuilder;

namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 其他业务按钮方法实现类
    /// </summary>
    public class OtherBusinessCommandHandler : IBaseComnandHandler
    {
        #region  Fields & Property & Services
        /// <summary>
        /// RootWorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        public BaseBusinessPart CurrentBaseBusinessPart
        {
            get { return RootWorkItem.State["CurrentBaseBusinessPart"] as BaseBusinessPart; }
            set { RootWorkItem.State["CurrentBaseBusinessPart"] = value; }
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
        /// 公共服务接口
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
                    CompanyID=CurrentBaseBusinessPart.CompanyID,
                    OperationType = OperationType.Other,
                    CurrentBusinessPart = CurrentBaseBusinessPart,
                    Updatetime = CurrentBaseBusinessPart.Updatetime
                };
            }

        }

        #endregion

        #region   方法
        /// <summary>
        /// 新增
        /// </summary>
        public void OtherBusinessAddData(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessAddData();
        }
        /// <summary>
        /// 复制
        /// </summary>
        public void OtherBusinessCopyData(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessCopyData(BusinessPartParam.ID, Guid.Empty);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        public void OtherBusinessEditData(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessEditData(BusinessPartParam.ID, BusinessPartParam.CompanyID);
        }
        /// <summary>
        /// 取消业务/恢复业务
        /// </summary>
        public void OtherBusinessCancelData(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessEditData(BusinessPartParam.ID, BusinessPartParam.CompanyID);
        }
        /// <summary>
        ///  打开账单
        /// </summary>
        public void OtherBusinessBill(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessBill(BusinessPartParam.ID);
        }
        /// <summary>
        /// 核销单
        /// </summary>
        public void OtherBusinessVerifiSheet(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessVerifiSheet(BusinessPartParam.ID, BusinessPartParam.CompanyID);
        }
        /// <summary>
        /// 提货通知书
        /// </summary>
        public void OtherBusinessPickUp(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessPickUp(BusinessPartParam.ID, BusinessPartParam.CompanyID);
        }
        /// <summary>
        /// 打印
        /// </summary>
        public void OtherBusinessPrintOrder(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessPrintOrder(BusinessPartParam.ID, BusinessPartParam.CompanyID);
        }

        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OtherBusinessProfit(object sender, EventArgs e)
        {
            IcpCommonOperationService.OtherBusinessProfit(BusinessPartParam.ID, BusinessPartParam.CompanyID);
        }
        #endregion

        #region 其他业务-电商物流

        /// <summary>
        /// 其他业务-电商物流-新增
        /// </summary>
        public void OtherBusinessECAddData(object sender, EventArgs e)
        {
            IcpCommonOperationService.AddBusiness(null,OperationType.Other,FormType.ECommerceOrder);
        }

        /// <summary>
        /// 其他业务-电商物流-复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OtherBusinessECCopyData(object sender, EventArgs e)
        {
            var editPartShowCriteria = new EditPartShowCriteria
            {
                OperationID = BusinessPartParam.ID,
                OperationNo = BusinessPartParam.OperationNo,
                CompanyID = BusinessPartParam.CompanyID
            };
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.CopyBusiness(editPartShowCriteria,dictionary,OperationType.Other,FormType.ECommerceOrder);
        }

        /// <summary>
        /// 其他业务-电商物流-编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OtherBusinessECEditData(object sender, EventArgs e)
        {
            var editPartShowCriteria = new EditPartShowCriteria
            {
                OperationID = BusinessPartParam.ID,
                OperationNo = BusinessPartParam.OperationNo,
                CompanyID = BusinessPartParam.CompanyID
            };
            Dictionary<string, object> dictionary = CommandHandler.CreateBusinessParameter(ActionType.Edit, false);
            IcpCommonOperationService.EditBusiness(editPartShowCriteria, dictionary, OperationType.Other, FormType.ECommerceOrder);
        }

        /// <summary>
        /// 其他业务-电商物流-取消业务/恢复业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OtherBusinessECCancelData(object sender, EventArgs e)
        {
            var editPartShowCriteria = new EditPartShowCriteria
            {
                OperationID = BusinessPartParam.ID,
                OperationNo = BusinessPartParam.OperationNo,
                CompanyID = BusinessPartParam.CompanyID
            };
            IcpCommonOperationService.CancelBusiness(editPartShowCriteria, OperationType.Other, FormType.ECommerceOrder);
        }
        /// <summary>
        /// 其他业务-电商物流-打开账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OtherBusinessECBill(object sender, EventArgs e)
        {
            IcpCommonOperationService.OpenBill(BusinessPartParam.ID,OperationType.Other);
        }

        /// <summary>
        /// 其他业务-电商物流-核销单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OtherBusinessECVerifiSheet(object sender, EventArgs e)
        {
            IcpCommonOperationService.VerifiSheet(BusinessPartParam.ID, BusinessPartParam.OperationNo, OperationType.Other, FormType.ECommerceOrder);
        }
        #endregion
    }
}
