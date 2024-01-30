using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using ICP.WF.ServiceInterface;
using System.Linq;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary;

namespace ICP.WF.Activities
{
    /// <summary>
    /// And 规则
    /// </summary>
	[Serializable]
    public class AndRule:CommonRule
	{
        /// <summary>
        /// and 表达式列表
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

        public AndRule()
        {
            Express = new List<CommonRule>();
        }

       
        /// <summary>
        /// 替换规则里面的值
        /// </summary>
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
        /// 根据条件计算出用户
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public override List<Guid> Evaluate(IWorkFlowExtendService service)
        {
            if (service == null) throw new ArgumentNullException("No Service");
            if (this.Express == null || this.Express.Count == 0) return new List<Guid>();

            List<Guid> ids = this.Express[0].Evaluate(service);

            List<CommonRule> roles = new List<CommonRule>();
            List<CommonRule> departments = new List<CommonRule>();
            foreach (CommonRule rule in this.Express)
            {
                if (rule is DepartmentRule)
                {
                    departments.Add(rule);
                }
                else if (rule is RoleRule)
                {
                    roles.Add(rule);
                }
                else
                {
                    ids = ids.Intersect(rule.Evaluate(service)).ToList();
                }
            }

            if (roles.Count > 0 && departments.Count > 0)
            {
                List<string> ruls = new List<string>();

                foreach (CommonRule rule in roles)
                {
                    ruls.AddRange(rule.RightExpress);
                }

                Guid[] userIds;

                string str = string.Empty;
                if (departments != null &&
                    departments.Count > 0 &&
                    departments[0].RightExpress != null &&
                    departments[0].RightExpress.ToArray().Length > 0)
                {
                    str=departments[0].RightExpress.ToArray()[0];
                }
 
                if (departments[0].Operator.Equals(WWFConstants.DepartmentEqual) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), false, OrganizationType.Department);
                }
                else if (departments[0].Operator.Equals(WWFConstants.CompanyLike) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), true, OrganizationType.Company);
                }
                else if (departments[0].Operator.Equals(WWFConstants.CompanyEqual) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), false, OrganizationType.Company);
                }
                else if (departments[0].Operator.Equals(WWFConstants.SectionEqual) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), false, OrganizationType.Section);
                }
                else if (departments[0].Operator.Equals(WWFConstants.SectionLike) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), true, OrganizationType.Section);
                }
                else
                {
                    if (DataTypeHelper.IsGuid(str))
                    {
                        userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), true, OrganizationType.Department);
                    }
                    else
                    { 
                        userIds=new Guid[0];
                    }
                }

                ids = ids.Intersect(userIds).ToList();
            }
            else
            {
                foreach (CommonRule rule in departments)
                {
                    ids = ids.Intersect(rule.Evaluate(service)).ToList();
                }

                foreach (CommonRule rule in roles)
                {
                    ids = ids.Intersect(rule.Evaluate(service)).ToList();
                }
            }

            return ids.Distinct().ToList();
        }


        /// <summary>
        /// 根据条件计算出用户
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public override bool Evaluate(IWorkFlowExtendService service,Guid callerid)
        {
            if (service == null) throw new ArgumentNullException("No Service");
            if (this.Express == null || this.Express.Count == 0) return false;

            List<Guid> ids = this.Express[0].Evaluate(service);

            List<CommonRule> roles = new List<CommonRule>();
            List<CommonRule> departments = new List<CommonRule>();
            foreach (CommonRule rule in this.Express)
            {
                if (rule is DepartmentRule)
                {
                    departments.Add(rule);
                }
                else if (rule is RoleRule)
                {
                    roles.Add(rule);
                }
                else
                {
                    ids = ids.Intersect(rule.Evaluate(service)).ToList();
                }
            }

            if (roles.Count > 0 && departments.Count > 0)
            {
                List<string> ruls = new List<string>();

                foreach (CommonRule rule in roles)
                {
                    ruls.AddRange(rule.RightExpress);
                }

                Guid[] userIds;

                string str = departments[0].RightExpress.ToArray()[0];

                if (departments[0].Operator.Equals(WWFConstants.DepartmentEqual) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), false, OrganizationType.Department);
                }
                else if (departments[0].Operator.Equals(WWFConstants.CompanyLike) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), true, OrganizationType.Company);
                }
                else if (departments[0].Operator.Equals(WWFConstants.CompanyEqual) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), false, OrganizationType.Company);
                }
                else if (departments[0].Operator.Equals(WWFConstants.SectionEqual) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), false, OrganizationType.Section);
                }
                else if (departments[0].Operator.Equals(WWFConstants.SectionLike) && DataTypeHelper.IsGuid(str))
                {
                    userIds = service.GetUsersByJobAndOrganization(str, ruls.ToArray(), true, OrganizationType.Section);
                }
                else
                {
                    if (DataTypeHelper.IsGuid(str))
                    {
                        userIds = service.GetUsersByJobAndOrganization(departments[0].RightExpress.ToArray()[0], ruls.ToArray(), true, OrganizationType.Department);
                    }
                    else
                    {
                        userIds=new Guid[0];
                    }


                }

                ids = ids.Intersect(userIds).ToList();
            }
            else
            {
                foreach (CommonRule rule in departments)
                {
                    ids = ids.Intersect(rule.Evaluate(service)).ToList();
                }

                foreach (CommonRule rule in roles)
                {
                    ids = ids.Intersect(rule.Evaluate(service)).ToList();
                }
            }

            return ids.Contains(callerid);
        }
	}
}
