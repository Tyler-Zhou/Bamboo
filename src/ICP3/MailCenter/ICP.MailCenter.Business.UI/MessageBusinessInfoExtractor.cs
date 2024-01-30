using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Operation.Common.ServiceInterface;
using EnumCommonConstants = ICP.Operation.Common.ServiceInterface.CommonConstants;

namespace ICP.MailCenter.Business.UI
{
    /// <summary>
    /// 消息业务信息抽取类
    /// </summary>
    public class MessageBusinessInfoExtractor : IBusinessInfoExtractor
    {
        private static IClientBusinessOperationService clientBusinessOperationService;
        public static IClientBusinessOperationService ClientBusinessOperationService
        {
            get
            {
                if (clientBusinessOperationService == null)
                {
                    clientBusinessOperationService = ServiceClient.GetClientService<IClientBusinessOperationService>();
                }
                return clientBusinessOperationService;
            }
        }



        static MessageBusinessInfoExtractor()
        {
            ReadConfigItems();
        }


        private const string Unknown = "Unknown";
        private static Dictionary<string, string> dicConfigs = new Dictionary<string, string>();
        #region IBusinessInfoExtractor 成员

        public BusinessOperationContext Extract(object parameter, bool getRealTemplateCode)
        {

            //首先判断是否是内部邮件
            ICP.Message.ServiceInterface.Message message = parameter as ICP.Message.ServiceInterface.Message;
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.OceanExport;
            if (message.UserProperties != null && !string.IsNullOrEmpty(message.UserProperties.Action))
            {
                string action = message.UserProperties.Action;

                context.OperationID = message.UserProperties.OperationId;
                context.FormType = message.UserProperties.FormType;
                context.FormId = message.UserProperties.FormId.Value;
                context[EnumCommonConstants.TemplateCodeKey] = GetTemplateCode(message, getRealTemplateCode);
                return context;
            }
            //外部邮件
            else
            {

                string templateCode = GetMessageTemplateCode(message, getRealTemplateCode);
                context[EnumCommonConstants.TemplateCodeKey] = templateCode;
                if (templateCode.Equals(ListFormType.MailLink4CarrierAN.ToString()))
                {
                    context.OperationType = OperationType.OceanImport;
                }
                return context;

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string GetMessageTemplateCode(ICP.Message.ServiceInterface.Message message, bool getRealTemplateCode)
        {
            if (!getRealTemplateCode)
                return ListFormType.MailLink4in1.ToString();

            string templateCode = string.Empty;
            if (message.UserProperties != null && !string.IsNullOrEmpty(message.UserProperties.TemplateCode))
            {
                templateCode = message.UserProperties.TemplateCode;
                if (!message.UserProperties.ContainsKey("OperationID"))
                {
                    message.UserProperties.OperationId = Guid.Empty;
                    message.UserProperties.OperationType = OperationType.Unknown;
                }
            }
            else
            {
                string sendFrom = message.SendFrom;
                if (string.IsNullOrEmpty(sendFrom))
                    templateCode = dicConfigs[EmailSourceType.Unknown.ToString()];
                else
                {
                    //从缓冲读取联系人类型
                    try
                    {
                        EmailSourceType sourceType = ClientBusinessOperationService.GetEmailType(sendFrom);

                        //对于当前既是客户又是承运人的发件人，按照客户来处理
                        if ((sourceType & EmailSourceType.Customer) == EmailSourceType.Customer)
                        {
                            templateCode = dicConfigs[EmailSourceType.Customer.ToString()];
                        }
                        else
                        {
                            templateCode = dicConfigs[sourceType.ToString()];
                        }

                    }
                    catch (System.Exception ex)
                    {
                        ICP.Framework.CommonLibrary.Logger.Log.Error(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                        throw new ICPException(ex.Message);
                    }
                }
            }
            return templateCode;
        }
        private static void ReadConfigItems()
        {
            string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
            string tempalteFileName = "TemplateCodeConfig.xml";
            string fileFullPath = Path.Combine(fileRootDirectory, tempalteFileName);
            XDocument document = XDocument.Load(fileFullPath);
            var elements = document.Element(XName.Get("Items")).Elements().ToList();
            var items = (from element in elements
                         select new { Action = element.Attribute("Action").Value, TemplateCode = element.Attribute("TemplateCode").Value }).ToList();
            items.ForEach(item =>
            {
                dicConfigs.Add(item.Action, item.TemplateCode);
            });
        }
        /// <summary>
        /// 根据消息获取模板代码
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetTemplateCode(ICP.Message.ServiceInterface.Message message, bool getRealTemplateCode)
        {
            string templateCode = GetMessageTemplateCode(message, getRealTemplateCode);
            return templateCode;
        }

        #endregion
    }
}
