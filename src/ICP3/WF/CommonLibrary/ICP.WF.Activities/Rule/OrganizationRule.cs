using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ICP.WF.ServiceInterface;
using System.Linq;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 组织结构规则条件
    /// </summary>
    [Serializable]
	public class OrganizationRule:CommonRule
    {

        public OrganizationRule()
        {
        }

        /// <summary>
        /// 组织结构属性(id,name,code)
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
        /// 操作符号 "Descendants", "Self"
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
        /// 替换规则里面的值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public override void ReplaceRuleValue(string source, string target)
        {
            if (string.IsNullOrEmpty(this.LeftExpress)) return;

            if (this.LeftExpress.Equals(source))
            {
                this.LeftExpress = target;
            }
        }

        bool IsGuid(string value)
        {
            try
            {
               Guid k= new Guid(value);
               return true;
            }
            catch
            {
                return false;
            }
        }

        Guid ConvertGuid(string value)
        {
            try
            {
                Guid k = new Guid(value);
                return k;
            }
            catch
            {
                return Guid.Empty;
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
            if (this.RightExpress == null || this.RightExpress.Count == 0) return new List<Guid>();
            return new List<Guid>();
        }


        public override bool Evaluate(IWorkFlowExtendService service, Guid callerid)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (this.RightExpress == null || this.RightExpress.Count == 0) return false;
            
            Guid[] expressids = null;
  

             expressids = (from g in this.RightExpress
                              select ConvertGuid(g)).ToArray();


            //List<Guid> orgIDList = new List<Guid>();
            //foreach (string item in this.RightExpress)
            //{
            //    orgIDList.Add(new Guid(item));
            //}

             bool result = service.CheckOrganizationIDIsChildList(new Guid(LeftExpress), expressids.ToArray());
            if (this.Operator.Equals(WWFConstants.ELike))
                return result;
            else
                return !result;
        }
	}
}
