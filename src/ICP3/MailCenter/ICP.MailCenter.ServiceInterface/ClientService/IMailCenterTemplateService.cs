using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 调用邮件模板发送邮件服务接口
    /// </summary>
    [EmailCenterServiceHost]
    [ServiceContract]
    [ServiceKnownType(typeof(ICP.FCM.OceanExport.ServiceInterface.DataObjects.OceanBookingInfo))]
    [ServiceKnownType(typeof(ICP.FCM.AirExport.ServiceInterface.DataObjects.AirBookingInfo))]
    [ServiceKnownType(typeof(ICP.FCM.OceanImport.ServiceInterface.OceanBusinessInfo))]
    [ServiceKnownType(typeof(ICP.FCM.AirImport.ServiceInterface.AirBusinessInfo))]
    [ServiceKnownType(typeof(ICP.Framework.CommonLibrary.Common.EventObjects))]
    public interface IMailCenterTemplateService
    {
        
        /// <summary>
        /// 调用邮件模板弹出邮件发送界面
        /// </summary>
        /// <param name="mail">Message对象</param>
        /// <param name="isEnglish">模板语言</param>
        /// <param name="sectionName">节点名称</param>
        /// <param name="itemKey">itemKey</param>
        /// <param name="values"></param> 
        [OperationContract(Name = "SendMailWithTemplate5",IsOneWay=true)]
        void SendMailWithTemplate(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string sectionName, string itemKey, object[] values);

        
        /// <summary>
        /// 调用邮件模板Common节点弹出邮件发送界面
        /// </summary>
        /// <param name="mail">Message对象</param>
        /// <param name="isEnglish">模板语言</param>
        /// <param name="itemKey">item键</param>
        /// <param name="values"></param>
        [OperationContract(IsOneWay=true)]
        void SendMailWithTemplate(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string itemKey, object[] values);

       
        /// <summary>
        /// 调用邮件英文版本弹出邮件发送界面
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <param name="values">对象集合(OceanBookingInfo,AirBookingInfo,OceanBusinessInfo,AirBusinessInfo)</param>
        [OperationContract(Name = "SendMailWithTemplateEN4", IsOneWay = true)]
        void SendMailWithTemplateEN(ICP.Message.ServiceInterface.Message mail, string sectionName, string itemKey, object[] values);

        
        /// <summary>
        ///调用邮件中文模板弹出邮件发送界面
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="sectionName"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        [OperationContract(Name = "SendMailWithTemplateCN4", IsOneWay = true)]
        void SendMailWithTemplateCN(ICP.Message.ServiceInterface.Message mail, string sectionName, string itemKey, object[] values);
      
        /// <summary>
        /// 调用邮件英文模板Common节点弹出邮件发送界面
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        [OperationContract(IsOneWay=true)]
        void SendMailWithTemplateEN(ICP.Message.ServiceInterface.Message mail, string itemKey, object[] values);

        
        /// <summary>
        /// 调用邮件中文模板Common节点弹出邮件发送界面
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="itemKey"></param>
        /// <param name="values"></param>
        [OperationContract(IsOneWay=true)]
        void SendMailWithTemplateCN(ICP.Message.ServiceInterface.Message mail, string itemKey, object[] values);
        /// <summary>
        /// 邮件自动发送
        /// </summary>
        /// <param name="mail">邮件信息实体类</param>
        /// <param name="isEnglish">是否发送英文版本</param>
        /// <param name="itemKey">发送邮件模版名称</param>
        /// <param name="values">发送模版内容</param>
        [OperationContract(IsOneWay = true)]
        void AutoSendMailWithTemplate(ICP.Message.ServiceInterface.Message mail, bool isEnglish, string itemKey,
                                      object[] values);

     
    }
}
