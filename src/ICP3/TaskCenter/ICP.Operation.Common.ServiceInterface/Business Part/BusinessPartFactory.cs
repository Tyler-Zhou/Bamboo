using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务面板工厂类
    /// </summary>
    public class BusinessPartFactory : IBusinessPartFactory,IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, XtraUserControl> parts = new Dictionary<string, XtraUserControl>();
        //private static Dictionary<string, TabBaseBusinessPart> tabParts = new Dictionary<string, TabBaseBusinessPart>();
        //private static Dictionary<string, ListBaseBusinessPart> listParts = new Dictionary<string, ListBaseBusinessPart>();
        /// <summary>
        /// 
        /// </summary>
        private object synObj = new object();
        /// <summary>
        /// 
        /// </summary>
        public WorkItem WorkItem
        {
            get {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="templateCode"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T Get<T>(string templateCode, object parameter) where T : XtraUserControl, IBaseBusinessPart_New
        {
            lock (synObj)
            {
                if (parts!=null&&parts.ContainsKey(templateCode))
                {
                    T uc = parts[templateCode] as T;
                    if (uc.IsDisposed)
                    {
                        uc.UnRegisterExtensionSite();
                        WorkItem.SmartParts.Remove(uc);
                        uc.Dispose();
                        uc = null;
                        parts.Remove(templateCode);
                    }
                    else
                    {
                        return parts[templateCode] as T;
                    }
                }

                try
                {

                    Type businessPartType = typeof(T);
                    T part = WorkItem.SmartParts.AddNew(businessPartType, templateCode) as T;

                    if (parts != null) parts[templateCode] = part;
                    if (part != null)
                        part.Init(parameter);//由BaseBusinessPart 实现
                    return part;
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(DateTime.Now.ToString() + Environment.NewLine + CommonHelper.BuildExceptionString(ex));

                    throw new ICPException(ex.Message);
                }
            }
        }

        #region IDisposable 成员
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (parts != null)
            {
                foreach (XtraUserControl control in parts.Values)
                {
                    control.Dispose();

                }
                parts.Clear();
                parts = null;
            }
        }
        #endregion
    }
}
