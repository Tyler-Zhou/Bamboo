using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;

namespace ICP.EDI.ServiceComponent.Rule
{
    /// <summary>
    /// 数据表验证规则
    /// </summary>
    public class TableRule:IRuleContainer
    {


        /// <summary>
        /// 表名
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



        public TableRule()
        {
            this.ChildRules = new List<IRule>();
        }


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


        public void InitRule(System.Xml.XmlNode node)
        {
            this.CDesc = node.Attributes["cdesc"].Value;
            this.EDesc = node.Attributes["edesc"].Value;
            this.Name = node.Attributes["name"].Value;

            this.ChildRules.Clear();

            XmlNodeList columnRuleList = node.SelectNodes("columnrule");
            foreach (XmlNode columnRulenode in columnRuleList)
            {
                ColumnRule columnRule = new ColumnRule();
                columnRule.ParentRule = this;
                columnRule.InitRule(columnRulenode);
                this.ChildRules.Add(columnRule);
            }
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value">验证对象</param>
        /// <returns></returns>
        public void Validate(object value, ref StringBuilder errorInfo)
        {

            if (this.ParentRule == null) errorInfo.Append("必须指定父规则.");

            if (value == null)
            {
                //throw new Exception("表不存在验证数据.");
                errorInfo.AppendFormat("{0}不存在数据", this.CDesc);
                return;
            }


            DataTable dt = value as DataTable;
            if (dt==null || dt.Rows.Count == 0)
            {
                errorInfo.AppendFormat("{0}不存在数据", this.CDesc);
                return;
            }

            if (this.ChildRules == null || this.ChildRules.Count == 0) return;

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    foreach (IRuleContainer rule in this.ChildRules)
                    {
                        if (rule.Name.ToLower().Equals(col.ColumnName.ToLower()))
                        {
                            rule.Validate(dr[col], ref errorInfo);
                        }
                    }
                }
            }
        }
    }
}
