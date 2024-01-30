using System;
using System.Collections.Generic;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.FileSystem.ServiceInterface;

namespace ICP.DataCache.BusinessOperation
{
    public class DocumentMemoryCache : IDisposable
    {
        static ListDictionary<Guid, DocumentInfo> documentMemoryCache = new ListDictionary<Guid, DocumentInfo>();

        static object synObj = new object();

        public static void Remove(Guid id)
        {
            lock (synObj)
            {
                Remove(new DocumentInfo() { Id = id });
            }
        }
        public static void Remove(List<Guid> ids)
        {
            Array.ForEach(ids.ToArray(), id => Remove(id));
        }
        public static void Remove(DocumentInfo document)
        {
            lock (synObj)
            {
                List<DocumentInfo> documents = documentMemoryCache.Values;
                if (documents == null || documents.Count <= 0)
                    return;
                DocumentInfo target = documents.Find(doc => doc.Id == document.Id);
                if (target!= null)
                {
                    documentMemoryCache.Remove(target);
                }        
            }
        }
        public static void Remove(DocumentInfo[] documents)
        {
            Array.ForEach(documents, document => Remove(document));
        }
        public static void RemoveOperationDocuments(Guid operationId)
        {
            lock (synObj)
            {
                documentMemoryCache.Remove(operationId);
            }
        }
        public static bool Add(DocumentInfo document)
        {

            if (document == null)
                return false;
            lock (synObj)
            {
                documentMemoryCache.Add(document.OperationID, document);
            }
            return true;
        }
        public static bool Add(DocumentInfo[] documents)
        {
            if (documents == null)
                return false;

            lock (synObj)
            {
                foreach (DocumentInfo document in documents)
                {
                    documentMemoryCache.Add(document.OperationID, document);
                }

            }
            return true;
        }
        public static void Clear()
        {
            documentMemoryCache.Clear();
        }


        public static DocumentInfo Find(Guid id)
        {
            lock (synObj)
            {
                List<DocumentInfo> documents = documentMemoryCache.Values;
                if (documents == null || documents.Count <= 0)
                    return null;
                DocumentInfo target = documents.Find(document => document.Id == id);
                return target;
            }

        }

        public static DocumentInfo FindFileName(Guid operationId,string fileName) 
        {
            lock (synObj)
            {
                if (!documentMemoryCache.Keys.Contains(operationId))
                    return null;
                DocumentInfo target = documentMemoryCache[operationId].Find(document => document.Name == fileName);
                return target; 
               
            }     
        }


        public static List<DocumentInfo> FindOperationDocuments(Guid id)
        {
            lock (synObj)
            {
                if (documentMemoryCache.ContainsKey(id))
                {
                    return documentMemoryCache[id];
                }
                else
                {
                    return null;
                }
            }
        }
        public static bool ContainsFailedData(Predicate<DocumentInfo> info)
        {
            lock (synObj)
            {
                if (documentMemoryCache.Values.Exists(info))
                    return true;
                return false;
            }
        }

        public static bool IsExistFileNames(List<DocumentInfo> docList)
        {
            foreach (var item in docList)
            {
                if (FindFileName(item.OperationID,item.Name) != null)
                {
                    return true;
                }
            }
            return false;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (documentMemoryCache != null)
            {
                documentMemoryCache.Clear();
                documentMemoryCache = null;
            }
        }

        #endregion
    }
}
