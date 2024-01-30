using System.Xml.Linq;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// Email模板数据项类
    /// </summary>
    public class EmailTemplateItemData : ITemplateItemData
    {
        private string name;
        private string subject;
        private string body;

        /// <summary>
        /// 项名称标识
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        ///邮件标题
        /// </summary>
        public string Subject
        {
            get { return this.subject; }
            set { this.subject = value; }
        }
        public string Body
        {
            get { return this.body; }
            set { this.body = value; }
        }
        #region ITemplateItemData 成员

        public ITemplateItemData Init(XElement element)
        {
            this.Name = element.Name.LocalName;
            this.Subject = element.Attribute(XName.Get("Subject")).Value;
            this.Body = element.Attribute(XName.Get("Body")).Value;

            return this;
        }

        #endregion
    }
}
