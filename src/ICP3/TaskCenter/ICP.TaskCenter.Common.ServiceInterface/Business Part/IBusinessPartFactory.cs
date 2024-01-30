using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.MailCenter.Business.ServiceInterface
{  
    /// <summary>
    /// 业务面板工厂接口
    /// </summary>
   public interface IBusinessPartFactory
    {
       BaseBusinessPart Get(string templateCode);
    }
}
