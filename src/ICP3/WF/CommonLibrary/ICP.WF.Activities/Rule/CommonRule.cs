using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ICP.WF.ServiceInterface;

namespace ICP.WF.Activities
{
    [Serializable]
    [XmlInclude(typeof(AndRule))]
    [XmlInclude(typeof(OrRule))]
    [XmlInclude(typeof(UserRule))]
    [XmlInclude(typeof(DepartmentRule))]
    [XmlInclude(typeof(OrganizationRule))]
    [XmlInclude(typeof(RoleRule))]
    [XmlInclude(typeof(FormExpressionRule))]
	public abstract class CommonRule
	{
        public CommonRule()
        {
            Express = new List<CommonRule>();
            RightExpress = new List<string>();
        }
        
        /// <summary>
        /// 表单式列表
        /// </summary>
        [XmlElement("Express")]
        public virtual List<CommonRule> Express { get; set; }


        /// <summary>
        /// 属性
        /// </summary>
        [XmlElement("LeftExpress")]
        public virtual string LeftExpress { get; set; }

        /// <summary>
        /// 值列表
        /// </summary>
        [XmlElement("RightExpress")]
        public virtual List<string> RightExpress { get; set; }

        /// <summary>
        /// 操作符号(=,like)
        /// </summary>
        [XmlElement("Operator")]
        public virtual string Operator { get; set; }

        /// <summary>
        /// 计算规则
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public abstract List<Guid> Evaluate(IWorkFlowExtendService service);

        /// <summary>
        /// 计算规则
        /// </summary>
        /// <param name="service"></param>
        /// <param name="callerid"></param>
        /// <returns></returns>
        public abstract bool Evaluate(IWorkFlowExtendService service, Guid callerid);

        /// <summary>
        /// 替换规则里面的值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public abstract void ReplaceRuleValue(string source, string target);


       
	}

    public enum PlaceEnum
    {
        IfElse,
        Approve,
        SendMessage
    }
}
