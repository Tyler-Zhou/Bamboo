using System.Text;

namespace ICP.EDI.ServiceComponent.Rule
{
    /// <summary>
    /// 最大长度规则
    /// </summary>
    public class MaxLengthRule : IRule
    {
        /// <summary>
        /// 最大长度0-不限制
        /// </summary>
        public int MaxLength=0;

        public MaxLengthRule()
        {
        }

        

        /// <summary>
        /// 父规则
        /// </summary>
        public IRule ParentRule { get; set; }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorInfo"></param>
        public void Validate(object value, ref StringBuilder errorInfo)
        {
            if (value == null) return;
            if (this.MaxLength == 0) return;

            if (this.ParentRule == null) errorInfo.Append("最大长度规则，必须指定父规则.");
            
            string strValue = value.ToString();
            if (strValue.Length > this.MaxLength)
            {
                if (this.ParentRule is IRuleContainer)
                {
                    IRuleContainer colRule = this.ParentRule as IRuleContainer;
                    string error = string.Format("{0}超出了最大长度{1}", colRule.CDesc, MaxLength);
                    if (errorInfo.Length > 0) errorInfo.Append(System.Environment.NewLine);

                    errorInfo.Append(error);
                }
               
            }
        }

        public void InitRule(System.Xml.XmlNode node)
        {
            this.MaxLength = int.Parse(node.InnerText);
        }
    }
}
