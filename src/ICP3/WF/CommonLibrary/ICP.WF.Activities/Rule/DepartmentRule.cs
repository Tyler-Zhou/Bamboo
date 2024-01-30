using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ICP.WF.ServiceInterface;
using System.Linq;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface.DataObject;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 部门规则条件
    /// </summary>
    [Serializable]
	public class DepartmentRule:CommonRule
	{
        public DepartmentRule()
        {
        }

        /// <summary>
        /// 部门属性(id,name,code)
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
            if (this.RightExpress == null || this.RightExpress.Count == 0) return;

            if (this.RightExpress.Contains(source))
            {
                this.RightExpress.Remove(source);

                if (string.IsNullOrEmpty(target) == false)
                {
                    if (IsGuid(target))
                    {
                        this.LeftExpress = "Id";
                    }
                    this.RightExpress.Add(target);
                }
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
            Guid[] expressids=null;
            string[] rightexpress = null;

            if (this.LeftExpress.ToLower() == "id")
            {
                expressids = (from g in this.RightExpress
                              select ConvertGuid(g)).ToArray();
            }
            else
            {
                rightexpress = this.RightExpress.ToArray();
            }

            if (this.Operator.Equals(WWFConstants.DepartmentEqual))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, false, OrganizationType.Department);
                return ids.Distinct().ToList();
            }
            else if (this.Operator.Equals(WWFConstants.SectionEqual))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, false, OrganizationType.Section);
                return ids.Distinct().ToList();
            }
            else if (this.Operator.Equals(WWFConstants.SectionLike))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, true, OrganizationType.Section);
                return ids.Distinct().ToList();
            }
            else if (this.Operator.Equals(WWFConstants.CompanyLike))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, true, OrganizationType.Company);
                return ids.Distinct().ToList();
            }
            else if (this.Operator.Equals(WWFConstants.CompanyEqual))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, false, OrganizationType.Company);
                return ids.Distinct().ToList();
            }
            else
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, true, OrganizationType.Department);
                return ids.Distinct().ToList();
            }
        }


        public override bool Evaluate(IWorkFlowExtendService service, Guid callerid)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (this.RightExpress == null || this.RightExpress.Count == 0) return false;
            Guid[] expressids = null;
            string[] rightexpress = null;
            if (this.LeftExpress.ToUpper() == "Id".ToUpper())
            {
                expressids = (from g in this.RightExpress
                              select ConvertGuid(g)).ToArray();
            }
            else
            {
                rightexpress = this.RightExpress.ToArray();
            }
            if (this.Operator.Equals(WWFConstants.DepartmentEqual))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, false,OrganizationType.Department);
                return ids.Contains(callerid);
            }
            else if (this.Operator.Equals(WWFConstants.SectionEqual))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, false, OrganizationType.Section);
                return ids.Contains(callerid);
            }
            else if (this.Operator.Equals(WWFConstants.SectionLike))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, true, OrganizationType.Section);
                return ids.Contains(callerid);
            }
            else if (this.Operator.Equals(WWFConstants.CompanyLike))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, true, OrganizationType.Company);
                return ids.Contains(callerid);
            }
            else if (this.Operator.Equals(WWFConstants.CompanyEqual))
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, false, OrganizationType.Company);
                return ids.Contains(callerid);
            }
            else
            {
                Guid[] ids = service.GetUserByOrganization(expressids, rightexpress, null, true, OrganizationType.Department);
                return ids.Contains(callerid);
            }
        }
	}
}
