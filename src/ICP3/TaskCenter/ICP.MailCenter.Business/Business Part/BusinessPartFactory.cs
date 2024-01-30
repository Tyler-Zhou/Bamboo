using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary;
using DevExpress.XtraEditors;
namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务面板工厂类
    /// </summary>
    public class BusinessPartFactory : IBusinessPartFactory,IDisposable
    {
        private static Dictionary<string, XtraUserControl> parts = new Dictionary<string, XtraUserControl>();
        //private static Dictionary<string, TabBaseBusinessPart> tabParts = new Dictionary<string, TabBaseBusinessPart>();
        //private static Dictionary<string, ListBaseBusinessPart> listParts = new Dictionary<string, ListBaseBusinessPart>();


        public WorkItem WorkItem
        {
            get {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        public T Get<T>(string templateCode, object parameter) where T : XtraUserControl, IBaseBusinessPart_New
        {
            if (parts.ContainsKey(templateCode))
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

                parts[templateCode] = part;
                part.Init(parameter);
                return part;
            }
            catch (Exception ex)
            {
                Logger.Log.Error(DateTime.Now.ToString() + Environment.NewLine + CommonHelper.BuildExceptionString(ex));

                throw new ICPException(ex.Message);
            }
          
        }




        #region IDisposable 成员

        public void Dispose()
        {
            foreach (XtraUserControl control in parts.Values)
            {
                control.Dispose();
                
            }
            parts.Clear();
            parts = null;
        }

        #endregion
    }
}
