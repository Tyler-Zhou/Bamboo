using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ICP.EDI.ServiceComponent.Rule
{
    /// <summary>
    /// 存放XML的验证规则
    /// </summary>
    public class XMLRule : IRuleContainer
    {

        public string Name { get; set; }

        /// <summary>
        /// xpath表达式
        /// </summary>
        public string XPath{get;set;}

        /// <summary>
        /// 中文描述
        /// </summary>
        public string CDesc { get; set; }

        /// <summary>
        /// 英文描述
        /// </summary>
        public string EDesc { get; set; }

        /// <summary>
        /// 父规则
        /// </summary>
        public IRule ParentRule { get; set; }

        /// <summary>
        /// 子规则集
        /// </summary>
        public List<IRule> ChildRules { get; set; }

        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="rule"></param>
        public void AddRule(IRule rule)
        {
            if (this.ChildRules == null) this.ChildRules = new List<IRule>();
            this.ChildRules.Add(rule);
        }

        public XMLRule()
        {
            this.ChildRules = new List<IRule>();
        }

      

     

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">验证对象</param>
        /// <returns></returns>
        public void Validate(object value,ref StringBuilder errorInfo)
        {
            if (this.ParentRule == null) errorInfo.Append("必须指定父规则.");

            if (value == null || value.ToString().Length == 0)
            {
                return;
            }

            if (string.IsNullOrEmpty(this.XPath))
            {
                throw new Exception("必须指定XPath表达式!");
            }

            XmlDocument document = new XmlDocument();    //创建xml文档对象
            document.LoadXml(value.ToString());
            
            XmlNodeList nodeList = document.SelectNodes(this.XPath);

            foreach (XmlNode node in nodeList)
            {
                string val = node.InnerText;

                foreach (IRule rule in this.ChildRules)
                {
                    rule.Validate(val,ref errorInfo);
                }
            }
        }

        public void InitRule(System.Xml.XmlNode node)
        {
            this.Name = node.Attributes["name"].Value;
            this.CDesc = node.Attributes["cdesc"].Value;
            this.EDesc = node.Attributes["edesc"].Value;
            this.XPath = node.Attributes["xpath"].Value;

            this.ChildRules.Clear();

            //加载必填项规则
            XmlNode maxLengthRuleNode = node.SelectSingleNode("maxlengthrule");
            if (maxLengthRuleNode != null)
            {
                MaxLengthRule maxLengthRule = new MaxLengthRule();
                maxLengthRule.ParentRule = this;
                maxLengthRule.InitRule(maxLengthRuleNode);
                this.ChildRules.Add(maxLengthRule);
            }

            //加载必输项规则
            XmlNode mustInputRuleNode = node.SelectSingleNode("mustinputrule");
            if (mustInputRuleNode != null)
            {
                MustInputRule mustInputRule = new MustInputRule();
                mustInputRule.ParentRule = this;
                mustInputRule.InitRule(mustInputRuleNode);
                this.ChildRules.Add(mustInputRule);
            }

            //加载正则表达式规则
            XmlNode regexruleNode = node.SelectSingleNode("regexrule");
            if (regexruleNode != null)
            {
                RegexRule regexrule = new RegexRule();
                regexrule.ParentRule = this;
                regexrule.InitRule(regexruleNode);
                this.ChildRules.Add(regexrule);
            }

            //xml规则
            XmlNode xmlruleNode = node.SelectSingleNode("xmlrule");
            if (xmlruleNode != null)
            {
                XMLRule xmlrule = new XMLRule();
                xmlrule.ParentRule = this;
                xmlrule.InitRule(xmlruleNode);
                this.ChildRules.Add(xmlrule);
            }
        }
    }
}
