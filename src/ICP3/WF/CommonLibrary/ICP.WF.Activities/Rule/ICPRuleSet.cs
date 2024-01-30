using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

namespace ICP.WF.Activities
{
    [Serializable]
	public class ICPRuleSet
	{
        public ICPRuleSet()
        {
            conditions = new List<ICPCondition>();
        }

        List<ICPCondition> conditions ;
        [XmlElement("Conditions",typeof(ICPCondition))]
        public List<ICPCondition> Conditions {
            get
            {
                return conditions;
            }
            set
            {
                conditions = value;
            }
        }
	}

}
