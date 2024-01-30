using System;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 上传附件参数类
    /// </summary>
    [Serializable]
    public class UploadAttachmentParameter
    {
        public UploadAttachmentParameter()
        {
            UploadWay = UploadWay.DirectOpen;
        }

        /// <summary>
        /// 打开方式
        /// </summary>
        public UploadWay UploadWay { get; set; }
        /// <summary>
        /// 打开后默认选择的动作类型
        /// </summary>
        public SelectionType SelectionType { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 模版代码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 业务实体
        /// </summary>
        public OceanBookingInfo OceanBookingInfo { get; set; }

        /// <summary>
        /// 原生态的邮件对象
        /// </summary>
        public object MailItem { get; set; }

        /// <summary>
        /// 转换成Message消息对象
        /// </summary>
        public Message.ServiceInterface.Message MessageInfo { get; set; }
        /// <summary>
        /// 业务上下文
        /// </summary>
        public BusinessOperationContext OperationContext { get; set; }

        public void GetOperationContext()
        {
            if (OperationType == OperationType.OceanExport)
            {
                OperationContext = new BusinessOperationContext()
                    {
                        OperationType = OperationType,
                        OperationID = OceanBookingInfo == null ? Guid.Empty : OceanBookingInfo.ID,
                        OperationNO = OceanBookingInfo == null ? string.Empty : OceanBookingInfo.No,
                        CompanyID = OceanBookingInfo == null ? Guid.Empty : OceanBookingInfo.CompanyID,
                        FormId = OceanBookingInfo == null ? Guid.Empty : OceanBookingInfo.ID,
                        FormType = OceanBookingInfo == null ? FormType.Unknown : FormType.Booking,
                        UpdateDate = OceanBookingInfo == null ? null : OceanBookingInfo.UpdateDate
                    };
            }            
        }
    }
}
