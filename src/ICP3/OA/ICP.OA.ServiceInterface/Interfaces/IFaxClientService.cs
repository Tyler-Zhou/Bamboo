using System;
using System.Collections.Generic;
using ICP.Message.ServiceInterface;
namespace ICP.OA.ServiceInterface
{
    public interface IFaxClientService : IFaxService
    {
     

        event EventHandler<MessageFolderSaveFinishEventArgs> FolderSaved;
        event EventHandler<MessageFlagChangeEventArgs> FlagChanged;
        event EventHandler<ChangeMessageFolderEventArgs> MessageFolderChanged;

        event EventHandler<MessageStateChangeEventArgs> MessageStateChanged;
       /// <summary>
       /// 显示发送传真界面
       /// </summary>
       /// <param name="message"></param>
       /// <returns>点击发送,返回True，取消发送返回False</returns>
       bool ShowSendForm(ICP.Message.ServiceInterface.Message message);
       /// <summary>
       /// 显示传真预览界面
       /// </summary>
       /// <param name="entry"></param>
       void ShowReadForm(ICP.Message.ServiceInterface.Message message);
        /// <summary>
        /// 传真状态改变，回调刷新界面
        /// </summary>
      ///<param name="messages"></param>
       void ChangeState(ICP.Message.ServiceInterface.Message[] messages);
    }
  
   public class MessageFolderSaveFinishEventArgs : EventArgs
   {
       Guid? oldFolderId;
       Guid newFolderId;
       Guid parentFolderId;
       DateTime? updateDate;
       public MessageFolderSaveFinishEventArgs(Guid? oldId, Guid newId,Guid parentId, DateTime? updateDate)
       {
           this.oldFolderId = oldId;
           this.newFolderId = newId;
           this.parentFolderId = parentId;
           this.updateDate = updateDate;
       }
       public Guid NewFolderId
       {
           get { return this.newFolderId; }
       }
       public Guid? OldFolderId
       {
           get { return this.oldFolderId; }
       }
       public DateTime? UpdateDate
       {
           get { return this.updateDate; }
       }
       public Guid ParentId
       {
           get { return this.parentFolderId; }
       }
   }
   public class MessageFlagChangeEventArgs : EventArgs
   {
       Guid messageId;
       MessageFlag flag;
       DateTime? updateDate;
       public MessageFlagChangeEventArgs(Guid messageId, MessageFlag flag, DateTime? updateDate)
       {
           this.messageId = messageId;
           this.flag = flag;
           this.updateDate = updateDate;
       }
       public Guid MessageId
       {
           get { return this.messageId; }
       }
       public MessageFlag Flag
       {
           get { return this.flag; }
       }
       public DateTime? UpdateDate
       {
           get { return this.updateDate; }
       }
   }
   public class ChangeMessageFolderEventArgs : EventArgs
   {
       List<Guid> messageIds;
       List<DateTime?> updateDates;
       public ChangeMessageFolderEventArgs(List<Guid> ids,List<DateTime?> updateDates)
       {
           this.messageIds = ids;
           this.updateDates = updateDates;
       }
       public List<Guid> MessageIds
       {
           get { return this.messageIds; }
       }
       public List<DateTime?> UpdateDates
       {
           get { return this.updateDates; }
       }
     
   }
    /// <summary>
    /// 
    /// </summary>
   public class MessageStateChangeEventArgs : EventArgs
   {
       List<ICP.Message.ServiceInterface.Message> datas;
       public MessageStateChangeEventArgs(List<ICP.Message.ServiceInterface.Message> states)
       {
           this.datas = states;
       }
       public List<ICP.Message.ServiceInterface.Message> Data
       {
           get {
               return this.datas;
           }
       }
     
   }
    [Serializable]
   public class StateChange
   {
        public Guid Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public MessageState State { get; set; }
   }
   
}
