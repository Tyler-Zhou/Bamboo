using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.WinForms;
using System.Reflection;
using System.Windows.Forms;
using ICP.DataCache.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary;
namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 业务面板工厂类
    /// </summary>
    public class BusinessPartFactory : IBusinessPartFactory
    {
        private static Dictionary<string, BaseBusinessPart> parts = new Dictionary<string, BaseBusinessPart>();


        public IClientBusinessContactService ClientBusinessContactService
        {
            get
            {
                return ServiceClient.GetService<IClientBusinessContactService>();
            }
        }

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }


        public BaseBusinessPart Get(string templateCode)
        {

            if (parts.ContainsKey(templateCode))
            {
                BaseBusinessPart uc = parts[templateCode];
                if (uc.IsDisposed)
                {
                    parts.Remove(templateCode);
                }
                else
                {
                    return parts[templateCode];
                }
            }
            string businessPartTypeName = GetBusinessPartName(templateCode);
            try
            {
                Type businessPartType = Type.GetType(string.Format("{0}.{1},{0}", "ICP.MailCenter.Business.UI",businessPartTypeName));//Assembly.GetExecutingAssembly().GetExportedTypes().Where(type => type.Name == businessPartTypeName).First();


                BaseBusinessPart part = WorkItem.SmartParts.AddNew(businessPartType) as BaseBusinessPart;
                parts[templateCode] = part;
               // part.Init(mail);
                return part;
            }
            catch (Exception ex)
            {  
               // Logger.Log.Error(DateTime.Now.ToString() + Environment.NewLine + CommonHelper.BuildExceptionString(ex));
               
                throw new ICPException(string.Format("实例化类型:{0}失败!", businessPartTypeName + ex.Message));
            }
        }

        private string GetBusinessPartName(string templateCode)
        {
            return string.Format("{0}{1}", templateCode, "BusinessPart");
        }

    }
}
