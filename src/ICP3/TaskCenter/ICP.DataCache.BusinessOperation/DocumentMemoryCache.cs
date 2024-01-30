using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.DataCache.ServiceInterface1;

namespace ICP.DataCache.BusinessOperation1
{
    public class DocumentMemoryCache :IDisposable
    {
        static ListDictionary<Guid, DocumentInfo> documentMemoryCache = new ListDictionary<Guid, DocumentInfo>();

        static object synObj = new object();

        public static void Remove(Guid id)
        {
            lock (synObj)
            {
                documentMemoryCache.Remove(new DocumentInfo() { Id = id });
            }
        }
        public static void Remove(List<Guid> ids)
        {
            Array.ForEach(ids.ToArray(), id => Remove(id));
        }
        public static void Remove(DocumentInfo document)
        {
            documentMemoryCache.Remove(document);
        }
        public static void Remove(DocumentInfo[] documents)
        {
            Array.ForEach(documents, document => Remove(document));
        }
        public static void RemoveOperationDocuments(Guid operationId)
        {
            documentMemoryCache.Remove(operationId);
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
            List<DocumentInfo> documents = documentMemoryCache.Values;
            if (documents == null || documents.Count <= 0)
                return null;
            DocumentInfo target = documents.Find(document => document.Id == id);
            return target;

        }
        public static List<DocumentInfo> FindOperationDocuments(Guid id)
        {
            if (documentMemoryCache.ContainsKey(id))
                return null;
            return documentMemoryCache[id];
        }
        public static bool ContainsFailedData(Predicate<DocumentInfo> info)
        {
            if (documentMemoryCache.Values.Exists(info))
                return true;
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
