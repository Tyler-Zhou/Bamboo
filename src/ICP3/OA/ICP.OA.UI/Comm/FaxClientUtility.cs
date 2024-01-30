using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;

namespace ICP.OA.UI
{
    /// <summary>
    /// 传真通用类
    /// </summary>
    public class FaxClientUtility
    {

        static IClientMessageService _clientMessageService;
        public static IClientMessageService clientMessageService
        {
            get
            {
                if (_clientMessageService == null)
                {
                    //_clientMessageService = ServiceClient.GetService<IClientMessageService>();
                    _clientMessageService = ServiceClient.GetClientService<IClientMessageService>();
                }

                return _clientMessageService;
            }
        }

          /// <summary>
        /// 打开发送传真窗口，并且为已发送文件夹插入一行数据
        /// </summary>
        /// <param name="currentFolder"></param>
        /// <param name="folderList"></param>
        /// <param name="bsMailList"></param>
        public static void AddNewMail(MessageFolderList currentFolder, List<MessageFolderList> folderList, BindingSource bsMailList)
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.Type = MessageType.Fax;
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            message.Flag = MessageFlag.Read;
            message.Way = MessageWay.Send;            

            if (clientMessageService.ShowSendForm(message))
            {                
                ManyResult[] results = clientMessageService.Save(message);
                if (currentFolder.Type == MessageFolderType.Sended)
                {
                    MessageFolderList sendedFodler = folderList.Find(folder => folder.Type == MessageFolderType.Sended);
                    ManyResult result = results[0];
                    message.Id = result.Items[0].GetValue<Guid>("ID");
                    message.FolderId = sendedFodler.ID;
                    bsMailList.Insert(0, message);
                    bsMailList.ResetBindings(false);
                }
            }
        }

    }
}
