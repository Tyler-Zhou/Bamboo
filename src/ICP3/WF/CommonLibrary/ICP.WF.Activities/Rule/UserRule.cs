using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ICP.WF.ServiceInterface;
using System.Linq;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 用户条件规则
    /// </summary>
    [Serializable]
    public class UserRule : CommonRule
    {
        public UserRule()
        {
        }

        /// <summary>
        /// 用户属性(id或code或name)
        /// </summary>
        [XmlElement("LeftExpress")]
        public override string LeftExpress
        {
            get
            {
                return base.LeftExpress;
            }
            set
            {
                base.LeftExpress = value;
            }
        }

        /// <summary>
        /// 值列表        /// </summary>
        [XmlElement("RightExpress")]
        public override List<string> RightExpress
        {
            get
            {
                return base.RightExpress;
            }
            set
            {
                base.RightExpress = value;
            }
        }
        /// <summary>
        /// 值列表
        /// </summary>
        public List<string> ValueList
        {
            get
            {
               return (from b in RightExpress select b).ToList();  

            }
        }
        /// <summary>
        /// 操作符号(=,%)
        /// </summary>
        [XmlElement("Operator")]
        public override string Operator
        {
            get
            {
                return base.Operator;
            }
            set
            {
                base.Operator = value;
            }
        }

        /// <summary>
        /// 替换规则里面的值        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public override void ReplaceRuleValue(string source, string target)
        {
            if (this.RightExpress == null || this.RightExpress.Count == 0) return;

            if (this.RightExpress.Contains(source))
            {
                this.RightExpress.Remove(source);

                if (string.IsNullOrEmpty(target) == false)
                {
                    this.RightExpress.Add(target);
                }
            }
        }

        /// <summary>
        /// 根据条件计算出用户
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public override List<Guid> Evaluate(IWorkFlowExtendService service)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (this.RightExpress == null || this.RightExpress.Count == 0) 
            {
                return new List<Guid>();
            }


            Guid[] ids = service.GetUsersByIDs(GetValueList().ToArray(), null, null);
            return ids.Distinct().ToList();
        }

        public List<Guid> GetValueList()
        {
            List<Guid> idList = new List<Guid>();
            foreach (string str in ValueList)
            {
                if (DataTypeHelper.IsGuid(str))
                {
                    if (!idList.Contains(new Guid(str)))
                    {
                        idList.Add(new Guid(str));
                    }
                }
            }
            return idList;
        }

        public override bool Evaluate(IWorkFlowExtendService service, Guid callerid)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (this.RightExpress == null || this.RightExpress.Count == 0) return false;


            Guid[] ids = service.GetUsersByIDs(GetValueList().ToArray(), null, null);
            return ids.Contains(callerid);
        }

       
    }
}
