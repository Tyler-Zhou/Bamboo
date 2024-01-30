using System.Xml.Linq;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 业务面板SQL语句实体
    /// </summary>
    public class QueryStringTemplateData
    {
        public string TemplateCode { get; set; }
        public string QueryString { get; set; }


        public void Init(XElement element)
        {            
            this.QueryString = element.Attribute("QueryString").Value;
        }
    }
}
