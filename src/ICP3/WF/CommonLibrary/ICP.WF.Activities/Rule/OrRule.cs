using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ICP.WF.ServiceInterface;
using System.Linq;

namespace ICP.WF.Activities
{
    /// <summary>
    /// Or规则
    /// </summary>
	[Serializable]
    public class OrRule:CommonRule
	{
        /// <summary>
        /// 或表达式列表
        /// </summary>
        [XmlElement("Express")]
        public override List<CommonRule> Express
        {
            get
            {
                return base.Express;
            }
            set
            {
                base.Express = value;
            }
        }

        public OrRule()
        {
            Express = new List<CommonRule>();
        }

        /// <summary>
        /// 替换规则里面的值        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public override void ReplaceRuleValue(string source, string target)
        {
            foreach (CommonRule rule in Express)
            {
                rule.ReplaceRuleValue(source, target);
            }
        }

        /// <summary>
        /// 根据条件计算出用户        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public override List<Guid> Evaluate(IWorkFlowExtendService service)
        {
            if (service == null) throw new ArgumentNullException("service");

            List<Guid> ids = new List<Guid>();
            
            foreach (CommonRule rule in this.Express)
            {
                List<Guid> ds=rule.Evaluate(service);
                if (ds.Count > 0)
                {
                    ids.AddRange(ds);
                    break;
                }
            }

            return ids.Distinct().ToList();
        }


        public override bool Evaluate(IWorkFlowExtendService service, Guid callerid)
        {
            if (service == null) throw new ArgumentNullException("service");

            List<Guid> ids = new List<Guid>();

            foreach (CommonRule rule in this.Express)
            {
                bool isSucc = rule.Evaluate(service, callerid);
                if (isSucc)
                {
                    return isSucc;
                }
            }

            return false;
        }
	}
}
