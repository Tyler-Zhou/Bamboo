using System.Xml.Linq;

namespace ICP.MailCenter.ServiceInterface
{  
    /// <summary>
    /// 模板子项接口
    /// </summary>
   public interface ITemplateItemData
    {  
       /// <summary>
       /// 初始化
       /// </summary>
       /// <param name="element">对应于XML文件中的元素配置信息</param>
       /// <returns></returns>
       ITemplateItemData Init(XElement element);
    }
}
