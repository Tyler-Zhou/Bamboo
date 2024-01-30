using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;

namespace ICP.EDI.ServiceComponent.Rule
{
    /// <summary>
    /// 数据集规则
    /// </summary>
    public class DataSetRule : IRuleContainer
    {
        public DataSetRule()
        {
            this.ChildRules = new List<IRule>();
        }

        /// <summary>
        /// 数据集名称
        /// </summary>
        public string Name { get; set; }

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



        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">验证对象</param>
        /// <returns></returns>
        public void Validate(object value, ref StringBuilder errorInfo)
        {
            if (value == null)
            {
                throw new Exception("数据集不能为空!");
            }

            DataSet ds = value as DataSet;

            if (this.ChildRules == null || this.ChildRules.Count == 0) return;

            foreach (DataTable dt in ds.Tables)
            {
                foreach (IRuleContainer rule in this.ChildRules)
                {
                    if (rule.Name.ToLower().Equals(dt.TableName.ToLower()))
                    {
                        rule.Validate(dt, ref errorInfo);
                    }
                }
            }
        }


        public void InitRule(System.Xml.XmlNode node)
        {
            this.CDesc = node.Attributes["cdesc"].Value;
            this.EDesc = node.Attributes["edesc"].Value;
            this.Name = node.Attributes["name"].Value;

            this.ChildRules.Clear();

            XmlNodeList tableruleNodeList = node.SelectNodes("tablerule");
            foreach (XmlNode tableRulenode in tableruleNodeList)
            {
                TableRule tableRule = new TableRule();
                tableRule.ParentRule = this;
                tableRule.InitRule(tableRulenode);
                this.ChildRules.Add(tableRule);
            }
        }
    }
}
