using System;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;

namespace ICP.MailCenter.Business.UI
{
    public class QueryControllerFactory
    {
        public static WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        public static ICommonQuery CreateInstance(string assemblyName, string control, string templateCode)
        {
            ICommonQuery query = null;
            string id = string.Format("{0}_Query", templateCode);
            try
            {
                if (RootWorkItem.Items.Contains(id))
                    return RootWorkItem.Items.Get(id) as ICommonQuery;

                query = RootWorkItem.Items.AddNew(Type.GetType(string.Format("{0}.{1},{0}", assemblyName, control)), id) as ICommonQuery;
            }
            catch (Exception ex)
            {
                ICP.Framework.CommonLibrary.Logger.Log.Error(ICP.Framework.CommonLibrary.Common.CommonHelper.BuildExceptionString(ex));
                throw ex;
            }
            if (query == null)
            {
                throw new NotImplementedException(string.Format(LocalData.IsEnglish ? "the query was not found!" : "没有找到对应的查询器!"));
            }

            return query;
        }
    }
}
