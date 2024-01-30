using System;
using System.Reflection;

namespace ICP.Common.Business.ServiceInterface
{
   /// <summary>
   /// 不同意业务处理对象的抽象工厂类
   /// </summary>
   public class BusinessFactory
    {
       /// <summary>
       /// 通过业务类型得到不同业务对象
       /// </summary>
       /// <param name="templateCode">操作视图Code</param>
       /// <returns></returns>
       public static IBusiness CreateBusiness(string templateCode)
       {
           string strOperationType = templateCode.Split(new char[] { '_' })[1] + "Business";

           Type businessPartType = Type.GetType(string.Format("{0}.{1},{0}", "ICP.Common.Business.ServiceInterface", strOperationType));
           if (businessPartType != null)
           {
               Assembly ss = Assembly.GetAssembly(businessPartType);
               object obj = ss.CreateInstance("ICP.Common.Business.ServiceInterface." + strOperationType);
               return obj as IBusiness;
           }
           return null;
       }
    }
}
