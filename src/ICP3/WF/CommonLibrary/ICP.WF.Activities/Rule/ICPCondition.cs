using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.ComponentModel;
using ICP.WF.ServiceInterface;
using System.Xml;
using System.Xml.Serialization;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 条件数据结构
    /// </summary>
    [Serializable]
	public class ICPCondition
	{
        public ICPCondition()
        {
        }

        /// <summary>
        /// 条件名
        /// </summary>
        [XmlElement("ConditionName")]
        public string ConditionName { get; set; }

        /// <summary>
        /// 规则
        /// </summary>
        [XmlElement("Rule")]
        public CommonRule Rule { get; set; }

	}
}
