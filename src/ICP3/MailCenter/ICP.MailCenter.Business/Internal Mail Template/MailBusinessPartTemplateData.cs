using System.Xml.Linq;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 内部邮件连接业务面板信息类
    /// </summary>
    public class MailBusinessPartTemplateData
    {
        /// <summary>
        /// 程序集名称 
        /// </summary>
        public string Assmbly { get; set; }
        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName { get; set; }
        /// <summary>
        /// 模版代码
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool Editable { get; set; }

        public void Init(XElement element)
        {
            this.Assmbly = element.Attribute("AssemblyModule").Value;            
            this.ControlName = element.Attribute("ControlName").Value;
            this.TemplateCode = element.Attribute("TemplateCode").Value;            
            this.Editable = bool.Parse(element.Attribute("Editable").Value);
        }
    }
}
