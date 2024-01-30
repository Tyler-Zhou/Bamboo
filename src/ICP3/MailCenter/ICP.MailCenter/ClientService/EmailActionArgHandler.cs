using System;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;
using System.IO;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using System.Reflection;
namespace ICP.MailCenter.UI
{
    /// <summary>
    ///邮件发送命令参数处理类
    ///<remarks>ICP调用发送邮件功能，将发送邮件信息所保存的文件路径作为命令参数传递</remarks>
    /// </summary>
    public class EmailActionArgHandler : IAPPArgHandler
    {

        public IOutLookService OutLookService
        {
            get { return ServiceClient.GetService<IOutLookService>(); }
        }
        public string[] Parameters
        {
            get;
            set;
        }

        public void Handle()
        {
            PreHandle();
            if (this.Parameters == null || this.Parameters.Length <= 0)
                return;
            string saveFilePath = this.Parameters[0];
            if (string.IsNullOrEmpty(saveFilePath))
                return;

            if (!File.Exists(saveFilePath))
            {
                return;
            }

            MessageParameter parameter = SerializerHelper.DeserializeFromXMLDocument<MessageParameter>(saveFilePath);
            InvokeOutLookService(parameter);
            PostHandle();

        }

        private void InvokeOutLookService(MessageParameter parameter)
        {
            ActionType actionType = parameter.ActionType;
            ICP.Message.ServiceInterface.Message mail = parameter.Message;
            string actionName = actionType.ToString();
            Type type = OutLookService.GetType();
            MethodInfo methodInfo = type.GetMethod(actionName);
            methodInfo.Invoke(this.OutLookService, new object[] { mail });
        }

        public void PreHandle()
        {
            this.Parameters = MailUtility.AppStartArgs;
        }

        public void PostHandle()
        {

            try
            {
                string saveFilePath = this.Parameters[0];
                System.IO.File.Delete(saveFilePath);
            }
            catch
            {

            }
        }
    }
}
