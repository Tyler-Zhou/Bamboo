using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;

namespace ICP.DataCache.FileSystem
{  
    /// <summary>
    /// 文件操作服务类
    /// </summary>
   public interface IFileStoreOperation
    {

       bool Exists(FileSaveContext context, String fileName);
       void Delete(FileSaveContext context, String fileName);
       void Save(FileSaveContext context, AttachmentContent content);
       void Save(FileSaveContext context, List<AttachmentContent> contents);
       AttachmentContent Get(FileSaveContext context, String fileName);
       String GetDirectoryPath(FileSaveContext context);
       String GetFullPath(FileSaveContext context, String fileName);
       void Save(AttachmentContent content, String savePath);

       bool Exists(FileSaveContext context, Guid fileId);
       void Delete(FileSaveContext context, Guid fileId);
       AttachmentContent Get(FileSaveContext context, Guid fileId);
       String GetFullPath(FileSaveContext context, Guid fileId);
       List<AttachmentContent> Get(FileSaveContext context, List<Guid> attachmentIds);
    }
}
