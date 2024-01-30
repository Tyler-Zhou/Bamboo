using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;
using ICP.MailCenter.ServiceInterface;
using ICP.DataCache.FileSystem;
using ICP.Framework.CommonLibrary.Common;
using System.Windows.Forms;
using System.IO;
using Microsoft.Practices.CompositeUI;
using System.Threading;
using ICP.Message.ServiceInterface;
namespace ICP.DataCache.BusinessOperation1
{
    /// <summary>
    /// 沟通历史记录客户端服务类（当前仅缓存附件）
    /// </summary>
    public class ClientCommunicationHistoryService : IClientCommunicationHistoryService
    {
        [ServiceDependency]
        public ICommunicationHistoryService HistoryService { get; set; }
        [ServiceDependency]
        public IFileStoreOperation FileOperation { get; set; }
        #region ICommunicationHistoryService 成员

        public List<CommunicationHistory> GetCommunicationHistoryList(BusinessOperationContext context)
        {
            return HistoryService.GetCommunicationHistoryList(context);
        }

        public CommunicationHistory GetCommunicationHistoryDetailInfo(Guid id)
        {
            CommunicationHistory entry = HistoryService.GetCommunicationHistoryDetailInfo(id);
            if (entry == null)
            {
                throw new ICPException(string.Format("Failed to find the record:{0}",id));
            }
            //if (entry.HasAttachment)
            //{   
            //    List<Guid> attachmentIds=(from item in entry.Attachments
            //                             select item.Id).ToList();
            //    entry.Attachments = GetAttachment(id, attachmentIds);
            //}

            return entry;
        }

        public SingleResult SaveCommunicationHistoryEntry(CommunicationHistory entry)
        {
            ManyResult[] results = HistoryService.SaveCommunicationHistoryEntry(entry);
            FileSaveContext context = GetContext(results[0].Items[0].GetValue<Guid>("ID"));
            if (entry.HasAttachment)
            {
                int count = entry.Attachments.Count;
                for (int i = 0; i < count; i++)
                {
                    entry.Attachments[i].Id = results[1].Items[i].GetValue<Guid>("ID");

                }
            }
            WaitCallback callback = data =>
                {

                    FileOperation.Save(context, entry.Attachments);
                };
            ThreadPool.QueueUserWorkItem(callback);
            return results[0].Items[0];
        }
        private FileSaveContext GetContext(Guid id)
        {
            return new FileSaveContext { FileType = SaveFileType.CommunicationHistory, Id = id };
        }

        public List<AttachmentContent> GetAttachment(Guid mailId, List<Guid> attachmentIds)
        {   
            FileSaveContext context=GetContext(mailId);
            List<AttachmentContent> attachments = new List<AttachmentContent>();
            List<AttachmentContent> localAttachments = FileOperation.Get(context, attachmentIds);
            attachments.AddRange(localAttachments);
            var localAttachmentIds=(from item in localAttachments
                                   select item.Id).ToList();
            List<Guid> localMissingAttachmentIds = attachmentIds.Except(localAttachmentIds).ToList();
            if (localMissingAttachmentIds.Count > 0)
            {
                List<AttachmentContent> remoteAttachments = HistoryService.GetAttachment(mailId, localMissingAttachmentIds);
                FileOperation.Save(context, remoteAttachments);
                attachments.AddRange(remoteAttachments);

            }
            return attachments;
        }

        public AttachmentContent GetAttachment(Guid mailId, String attachmentName)
        {
            AttachmentContent attachment = HistoryService.GetAttachment(mailId, attachmentName);
            FileOperation.Save(GetContext(mailId), attachment);
            return attachment;
        }
        public void Open(Guid mailId, String attachmentName)
        {
            FileSaveContext context = GetContext(mailId);
            String fullPath = FileOperation.GetFullPath(context, attachmentName);

            if (!FileOperation.Exists(context, attachmentName))
            {
                GetAttachment(mailId, attachmentName);
            }
            OpenFile(fullPath);

        }
        private void OpenFile(string fileFullPath)
        {
            System.Diagnostics.Process.Start(fileFullPath);
        }
        public string Open(Guid mailId, Guid attachmentId)
        {
            FileSaveContext context = GetContext(mailId);
            AttachmentContent attachment = GetAttachment(mailId, new List<Guid> { attachmentId }).First();
            OpenFile(attachment.ClientPath);
            return attachment.ClientPath;
        }
        public void SaveAs(Guid mailId, Guid attachmentId,string displayName)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.AddExtension = false;
            dialog.CheckPathExists = true;
            dialog.FileName = displayName;
            dialog.OverwritePrompt = true;
            dialog.RestoreDirectory = true;
            FileSaveContext context = GetContext(mailId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                String fileSavePath = dialog.FileName;
                if (!FileOperation.Exists(context, attachmentId))
                {
                    AttachmentContent attachment = GetAttachment(mailId, new List<Guid> { attachmentId }).First();
                    FileOperation.Save(attachment, fileSavePath);
                }
                else
                {
                    String fullPath = FileOperation.GetFullPath(context,attachmentId);
                    File.Copy(fullPath, fileSavePath);
                }
            }




        }

        #endregion
    }
}
