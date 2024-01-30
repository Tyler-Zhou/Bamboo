using System.Text;

namespace ICP.EDI.ServiceComponent.Rule
{
    /// <summary>
    /// 必填规则
    /// </summary>
    public class MustInputRule : IRule
    {
        /// <summary>
        /// 必须输入
        /// </summary>
        public bool MustInput { get; set; }

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

            if (this.ParentRule == null) errorInfo.Append("必填规则，必须指定父规则.");

            if (this.MustInput && (value==null || value.ToString().Length==0))
            {
                if (this.ParentRule is IRuleContainer)
                {
                    IRuleContainer colRule = this.ParentRule as IRuleContainer;
                    
                    string error = string.Format("{0}必须填写!!", colRule.CDesc);
                    if (errorInfo.Length > 0) errorInfo.Append(System.Environment.NewLine);

                    errorInfo.Append(error);
                }
            }
        }

        public void InitRule(System.Xml.XmlNode node)
        {
            this.MustInput =bool.Parse(node.InnerText.Trim());
        }
    }
}
