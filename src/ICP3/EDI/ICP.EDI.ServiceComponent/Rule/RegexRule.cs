using System;
using System.Text;
using System.Text.RegularExpressions;

namespace ICP.EDI.ServiceComponent.Rule
{
    /// <summary>
    /// 正则表达式规则
    /// </summary>
    public class RegexRule:IRule
    {
        /// <summary>
        /// 
        /// </summary>
        public string RegexExpression { get; set; }


        public RegexRule()
        {
        }

        public RegexRule(string regex)
        {
            this.RegexExpression = regex;
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

            if (string.IsNullOrEmpty(this.RegexExpression))
            {
                return;
//                throw new Exception("正则表达式规则必须指定正则表达式!");
            }

            if (this.ParentRule == null)
            {
                throw new Exception("正则表达式规则规则，必须指定父规则.");
            } 

            string strValue = value.ToString();

            Regex valuePattern = new Regex(this.RegexExpression);
            bool isSucc = valuePattern.IsMatch(strValue);
            if (!isSucc)
            {
                if (this.ParentRule is IRuleContainer)
                {
                    IRuleContainer colRule = this.ParentRule as IRuleContainer;
                    string error = string.Format("{0}录入格式错误!", colRule.CDesc);
                    if (errorInfo.Length > 0) errorInfo.Append(System.Environment.NewLine);

                    errorInfo.Append(error);
                }
            }
        }

        public void InitRule(System.Xml.XmlNode node)
        {
            this.RegexExpression =node.InnerText;
        }
    }
}
