using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;

namespace ICP.DataCache.FileSystem
{
    public class FileStoreOperation : IFileStoreOperation
    {
        String rootPath = FileUtility.FileStoreRootPath;
        public bool Exists(FileSaveContext context, String fileName)
        {

            String fullPath = GetFullPath(context, fileName);
            return File.Exists(fullPath);
        }

        public void Delete(FileSaveContext context, String fileName)
        {
            String fullPath = GetFullPath(context, fileName);
            File.Delete(fullPath);
        }

        public void Save(FileSaveContext context, AttachmentContent content)
        {
            String fullPath = GetFullPath(context, content.Name);
            content.ClientPath = fullPath;
            WaitCallback callback = data =>
            {
                try
                {
                    // string directoryPath = GetDirectoryPath(context);
                    AttachmentContent attachment = data as AttachmentContent;
                    using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                    {
                       // stream.BeginWrite(attachment.Content, 0, attachment.Content.Length, null, null);
                        stream.Write(content.Content, 0, content.Content.Length);
                    }
                }
                catch (Exception ex)
                {

                }
            };
            ThreadPool.QueueUserWorkItem(callback, content);
        }

        public AttachmentContent Get(FileSaveContext context, String fileName)
        {
            if (!Exists(context, fileName))
                return null;
            String fullPath = GetFullPath(context, fileName);
             AttachmentContent info=null;
            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                byte[] content = new byte[(int)fs.Length];
                fs.Read(content, 0, content.Length);
                info = new AttachmentContent { Name = fileName, ClientPath = fullPath, Size = content.Length, Content = content };
                fs.Close();               
            }
            return info;
        }
        

        public void Save(FileSaveContext context, List<AttachmentContent> contents)
        {
            EnsureDirectoryExists(context);
            string directoryPath = GetDirectoryPath(context);
            FileStorageMetaData metaData = FileStorageMetaDataContext.Current[directoryPath];
            //int count = contents.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    AttachmentContent content = contents[i];
            //    string displayName=content.DisplayName;
            //    if (metaData.Contains(content.Name))
            //    {
            //        string newName = string.Format("{0}-{1}", content.Id, content.Name);
            //        content.Name = newName;
            //    }
            foreach (AttachmentContent attachment in contents)
            {
                metaData.AddValue(attachment.Id, attachment.Name, attachment.DisplayName);
                Save(context, attachment);
            }
           // }


        }
        public String GetDirectoryPath(FileSaveContext context)
        {
            //EnsureDirectoryExists(context);
            String directoryName = Path.Combine(context.FileType.ToString(), context.Id.ToString());
            return Path.Combine(rootPath, directoryName);

        }
        public String GetFullPath(FileSaveContext context, String fileName)
        {
            String fileRootPath = GetDirectoryPath(context);
            return Path.Combine(fileRootPath, fileName);
        }
        private void EnsureDirectoryExists(FileSaveContext context)
        {

            String directoryPath = GetDirectoryPath(context);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
        public void Save(AttachmentContent content, String savePath)
        {
            using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                int length = content.Content.Length;
                byte[] fileContent = new byte[length];
                fs.Write(fileContent, 0, length);

            }

        }

        #region IFileStoreOperation 成员


        public bool Exists(FileSaveContext context, Guid fileId)
        {
            string fileName = GetFileName(context, fileId);
            if (string.IsNullOrEmpty(fileName))
                return false;
            return Exists(context, fileName);

        }
        private string GetFileName(FileSaveContext context, Guid fileId)
        {
            string directoryPath = GetDirectoryPath(context);
            FileStorageMetaData metaData = FileStorageMetaDataContext.Current[directoryPath];
            bool contains = metaData.Contains(fileId);
            if (contains)
            {
                return metaData.GetFileName(fileId);



            }
            return null;

        }
        


        public void Delete(FileSaveContext context, Guid fileId)
        {
            string fileName = GetFileName(context, fileId);
            if (string.IsNullOrEmpty(fileName))
                return;
            Delete(context, fileName);
            string directoryPath = GetDirectoryPath(context);
            FileStorageMetaData metaData = FileStorageMetaDataContext.Current[directoryPath];
            metaData.Remove(fileId);
        }

        public AttachmentContent Get(FileSaveContext context, Guid fileId)
        {
            string fileName = GetFileName(context, fileId);
            if (string.IsNullOrEmpty(fileName))
                return null;
            AttachmentContent content = Get(context, fileName);
            content.Id = fileId;
            string directoryPath = GetDirectoryPath(context);
            FileStorageMetaData metaData = FileStorageMetaDataContext.Current[directoryPath];
            if (content != null)
            {
                content.DisplayName = metaData.GetDisplayName(fileId);
            }
            else
            {
                metaData.Remove(fileId);
            }
            return content;
        }

        public string GetFullPath(FileSaveContext context, Guid fileId)
        {
            string fileName = GetFileName(context, fileId);
            if (string.IsNullOrEmpty(fileName) || !Exists(context, fileName))
            {
                throw new ICPException(string.Format("File :{0} not exists.", fileId));
            }
            return GetFullPath(context, fileName);

        }
        public List<AttachmentContent> Get(FileSaveContext context, List<Guid> attachmentIds)
        {
            string directoryPath = GetDirectoryPath(context);
            if (!Directory.Exists(directoryPath))
                return new List<AttachmentContent>();
            FileStorageMetaData metaData = FileStorageMetaDataContext.Current[directoryPath];
            List<AttachmentContent> attachments = new List<AttachmentContent>();
            foreach (Guid fileId in attachmentIds)
            {
                AttachmentContent attachment = Get(context,fileId);
                if (attachment != null)
                {
                    attachments.Add(attachment);
                }


            }
            return attachments;

        }
        #endregion
    }
}
