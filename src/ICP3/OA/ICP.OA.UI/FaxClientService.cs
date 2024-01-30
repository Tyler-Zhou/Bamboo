using System;
using System.Collections.Generic;
using System.Linq;
using ICP.OA.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.Message.ServiceInterface;

namespace ICP.OA.UI
{
    public class FaxClientService : IFaxClientService
    {

        public IFaxService FaxService
        {
            get
            {
                return ServiceClient.GetService<IFaxService>();
            }
        }
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


       

        public bool ShowSendForm(ICP.Message.ServiceInterface.Message message)
        {
            ICP.OA.UI.FaxManage.frmSendFax sf = Workitem.Items.AddNew<ICP.OA.UI.FaxManage.frmSendFax>();
            sf.SetSource(message);
            System.Windows.Forms.DialogResult result = sf.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string tip = LocalData.IsEnglish ? "The fax has been sent to fax server waiting for sending." : "传真已发送到传真服务器,等待发送。";
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(sf.FindForm(), tip);
                return true;
            }
            return false;
        }
        public void ShowReadForm(ICP.Message.ServiceInterface.Message message)
        {
            ICP.OA.UI.FaxManage.frmFaxPreview frmPreview = Workitem.Items.AddNew<ICP.OA.UI.FaxManage.frmFaxPreview>();
            frmPreview.BindData(message);
            frmPreview.ShowDialog();
        }

       

      

        public event EventHandler<MessageSendFinishEventArgs> MessageSent;

        public event EventHandler<MessageFolderSaveFinishEventArgs> FolderSaved;
        public event EventHandler<MessageFlagChangeEventArgs> FlagChanged;
        public event EventHandler<ChangeMessageFolderEventArgs> MessageFolderChanged;
        public event EventHandler<MessageStateChangeEventArgs> MessageStateChanged;
       

        

        public List<MessageFolderList> GetMessageFolderList(Guid ownerID)
        {
            return FaxService.GetMessageFolderList(ownerID);
        }

        public ManyResultData SaveMessageFolder(Guid? folderID, Guid parentID, string name, MessageFolderType folderType, DateTime? updateDate)
        {
            ManyResultData result = FaxService.SaveMessageFolder(folderID, parentID, name, folderType, updateDate);
            // result.Add("oldFolderId", folderID);
            // TriggerFolderSaveEvent(result);
            return result;
        }

        public ManyResultData RemoveFolder(Guid folderID, DateTime? updateDate)
        {
            return FaxService.RemoveFolder(folderID, updateDate);
        }

        public List<ICP.Message.ServiceInterface.Message> GetMessageListByFolderId(Guid folderId)
        {
            return FaxService.GetMessageListByFolderId(folderId);
        }

        public ManyResult ChangeMessageFolder(Guid[] ids, Guid folderId, DateTime?[] updateDates)
        {
            return FaxService.ChangeMessageFolder(ids, folderId, updateDates);
        }


        public void ChangeState(ICP.Message.ServiceInterface.Message[] messages)
        {
            if (MessageStateChanged != null)
            {
                MessageStateChanged(this, new MessageStateChangeEventArgs(messages.ToList()));
            }
        }



        private void TriggerFlagChangeEvent(MessageFlag flag, SingleResultData result)
        {
            if (FlagChanged != null)
            {
                FlagChanged(this, new MessageFlagChangeEventArgs(result.ID, flag, result.UpdateDate));
            }
        }
        private void TriggerFolderSaveEvent(SingleResult result)
        {
            if (FolderSaved != null)
            {
                Guid? oldFolderId = result.GetValue<Guid?>("oldFolderId");
                Guid newFolderId = result.GetValue<Guid>("ID");
                Guid parentId = result.GetValue<Guid>("ParentID");
                DateTime? updateDate = result.GetValue<DateTime?>("UpdateDate");
                FolderSaved(this, new MessageFolderSaveFinishEventArgs(oldFolderId, newFolderId, parentId, updateDate));
            }
        }
        private void TriggerMessageFolderChangeEvent(ManyResultData result)
        {
            if (MessageFolderChanged != null)
            {
                List<Guid> ids = new List<Guid>();
                List<DateTime?> updateDates = new List<DateTime?>();
                for (int i = 0; i < result.ChildResults.Count; i++)
                {
                    ids.Add(result.ChildResults[i].ID);
                    updateDates.Add(result.ChildResults[i].UpdateDate);
                }
                MessageFolderChanged(this, new ChangeMessageFolderEventArgs(ids, updateDates));
            }
        }

 


        public List<ICP.Message.ServiceInterface.Message> GetMessageList(Guid ownerID, string ownerAccount, Guid? folderId, MessageFolderType? folderType, string folderName, string from, string to, string subject, bool? includeAttachment, MessagePriority? priority, MessageFlag? flag,
            DateTime? fromTime, DateTime? toTime, Guid? companyID)
        {
            return FaxService.GetMessageList(ownerID, ownerAccount, folderId, folderType, folderName, from, to, subject, includeAttachment, priority, flag, fromTime, toTime, companyID);
        }


        public List<Message.ServiceInterface.Message> GetFaxMessageListByFastSearch(
            Guid ownerID,
            Guid? folderId,
            Guid? companyId,
            string keyWord)
        {
            return FaxService.GetFaxMessageListByFastSearch(ownerID, folderId, companyId, keyWord);
        }
     

    


        public SingleResult Send(ICP.Message.ServiceInterface.Message message)
        {
            return FaxService.Send(message);
        }

        public void Resend(ICP.Message.ServiceInterface.Message message)
        {
            FaxService.Resend(message);
        }


    



   


        public ConfigureObjects GetConfigureInfoByCompanyID(Guid companyID)
        {
            return FaxService.GetConfigureInfoByCompanyID(companyID);
        }

        public List<ConfigureObjects> GetConfigureInfoByEmail(string email, bool isTaxNo)
        {
            return FaxService.GetConfigureInfoByEmail(email, isTaxNo);
        }

     

     


        public ManyResult UpdateConfigureInfoByCompanyID(ConfigureObjects info)
        {
            return FaxService.UpdateConfigureInfoByCompanyID(info);
        }

       
    }
}
