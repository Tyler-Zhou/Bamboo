using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using ICP.FileSystem.ServiceInterface;

namespace ICP.DataCache.BusinessOperation
{
    public class BusinessOperationHelper
    {
        public static IClientFileService ClientFileService
        {
            get
            {
                return ServiceClient.GetClientService<IClientFileService>();
            }
        }

        public static IFileSystemService FileServiceWCF
        {
            get
            {
                return ServiceClient.GetService<IFileSystemService>();
            }
        }

        /// <summary>
        /// 服务端判断是否存在重名，有则提示是否覆盖
        /// </summary>
        /// <param name="docList"></param>
        /// <returns>返回原文档与重名文档合并后的文档集合</returns>
        public static List<DocumentInfo> IsExistFileNames(List<DocumentInfo> docList)
        {
            List<string> fileNames = new List<string>();
            docList.ForEach(item => fileNames.Add(item.Name));
            if (fileNames.Count <= 0)
            {
                return new List<DocumentInfo>();
            }
            //List<DocumentInfo> documentlist = ClientFileService.IsExistFileNames(fileNames, docList[0].OperationID);
            List<DocumentInfo> documentlist;
            try
            {
                documentlist = FileServiceWCF.IsExistFileNames(fileNames, docList[0].OperationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (documentlist != null && documentlist.Count > 0)
            {
                if (TipConfirmation() == DialogResult.OK)
                {
                    return documentlist;
                }
                else
                {
                    return null;
                }
            }
            return documentlist;
        }
        /// <summary>
        /// 服务端判断是否存在重名，有则提示是否覆盖
        /// </summary>
        /// <param name="docList"></param>
        /// <returns>返回原文档与重名文档合并后的文档集合</returns>
        public static List<DocumentInfo> IsExistFileNames(List<DocumentInfo> docList, List<string> deleteName)
        {
            List<string> fileNames = new List<string>();
            docList.ForEach(item =>
            {
                if (!deleteName.Contains(item.Name))
                {
                    fileNames.Add(item.Name);
                }

            });
            if (fileNames.Count <= 0)
            {
                return new List<DocumentInfo>();
            }
            List<DocumentInfo> documentlist = ClientFileService.IsExistFileNames(fileNames, docList[0].OperationID);
            if (documentlist != null && documentlist.Count > 0)
            {
                if (TipConfirmation() == DialogResult.OK)
                {
                    return documentlist;
                }
                else
                {
                    return null;
                }
            }
            return documentlist;
        }



        public static List<DocumentInfo> IsExistFileNames(List<DocumentInfo> docList, List<Guid> deleteGuid)
        {
            List<string> fileNames = new List<string>();

            docList.ForEach(item =>
            {
                if (!deleteGuid.Contains(item.Id))
                {
                    fileNames.Add(item.Name);
                }
            });
            if (fileNames.Count <= 0)
            {
                return new List<DocumentInfo>();
            }
            List<DocumentInfo> documentlist;
            try
            {
                documentlist = FileServiceWCF.IsExistFileNames(fileNames, docList[0].OperationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (documentlist != null && documentlist.Count > 0)
            {
                if (TipConfirmation() == DialogResult.OK)
                {
                    return documentlist;
                }
                else
                {
                    return null;
                }
            }
            return documentlist;
        }

        /// <summary>
        /// 源文档与重名的文档合并
        /// </summary>
        /// <param name="docList"></param>
        /// <param name="contrastdoclist">重名的文档集合</param>
        private static void MergerDocument(List<DocumentInfo> docList, List<DocumentInfo> contrastdoclist)
        {
            if (contrastdoclist.Count != 0)
            {
                foreach (var item in contrastdoclist)
                {
                    DialogResult dlg = TipConfirmation(item.Name);
                    if (dlg != DialogResult.OK)
                    {
                        docList.Remove(docList.First(e => e.Name == item.Name));
                        continue;
                    }
                    foreach (DocumentInfo itemcompare in docList)
                    {
                        if (itemcompare.Name == item.Name)
                        {
                            itemcompare.Id = item.Id;
                            itemcompare.UpdateDate = item.UpdateDate;
                        }
                    }
                }
            }
        }

        public static DialogResult TipConfirmation()
        {
            DialogResult dlg = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The target file is already exists. Are you sure you want to replace it?" : " 已存在相同的文件名，确认覆盖?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel);
            return dlg;
        }


        /// <summary>
        /// 确实提示
        /// </summary>
        /// <returns></returns>
        public static DialogResult TipConfirmation(string fileName)
        {
            DialogResult dlg = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? fileName + "The target file is already exists. Are you sure you want to replace it?" : fileName + " 已存在相同的文件名，确认覆盖?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel);
            return dlg;
        }
    }
}
