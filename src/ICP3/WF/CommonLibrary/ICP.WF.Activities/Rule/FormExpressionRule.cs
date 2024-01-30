using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using ICP.WF.ServiceInterface;

namespace  ICP.WF.Activities
{
    /// <summary>
    /// 表单规则条件
    /// </summary>
    [Serializable]
 	public class FormExpressionRule:CommonRule
	{
        public FormExpressionRule()
        {
        }

        /// <summary>
        /// 表达属性
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
        /// 常量值        /// </summary>
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
        /// 操作符号
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
        /// 根据条件计算出用户
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public override List<Guid> Evaluate(IWorkFlowExtendService service)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (this.RightExpress == null || this.RightExpress.Count == 0) return new List<Guid>();

            int intLeftvalue = 0;
            int intValue = 0;

            List<Guid> ids = new List<Guid>();
            foreach (string val in this.RightExpress)
            {
                if (this.Operator.Equals("equal"))
                {
                    if (val.Equals(this.LeftExpress))
                    {
                        ids.Add(Guid.Empty);
                    }
                }
                else if (this.Operator.Equals("like"))
                {
                    if (this.LeftExpress.Contains(val))
                    {
                        ids.Add(Guid.Empty);
                    }
                }
                else if (this.Operator.Equals("is null"))
                {
                    if (string.IsNullOrEmpty(this.LeftExpress) || this.LeftExpress == Guid.Empty.ToString())
                    {
                        ids.Add(Guid.Empty);
                    }
                }
                else if (this.Operator.Equals(">="))
                {
                     try
                     {
                         intLeftvalue = Convert.ToInt32(LeftExpress);
                         intValue = Convert.ToInt32(val);
                     }catch{};
                     if (intLeftvalue >= intValue)
                     {
                         ids.Add(Guid.Empty);
                     }

                }
                else if (this.Operator.Equals("<="))
                {
                    try
                    {
                        intLeftvalue = Convert.ToInt32(LeftExpress);
                        intValue = Convert.ToInt32(val);
                    }
                    catch { };
                    if (intLeftvalue <= intValue)
                    {
                        ids.Add(Guid.Empty);
                    }

                }
                else if (this.Operator.Equals("<>"))
                {
                    try
                    {
                        intLeftvalue = Convert.ToInt32(LeftExpress);
                        intValue = Convert.ToInt32(val);
                    }
                    catch { };
                    if (intLeftvalue != intValue)
                    {
                        ids.Add(Guid.Empty);
                    }
                }

            }

            return ids;
        }

        public override bool Evaluate(IWorkFlowExtendService service, Guid callerid)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (this.RightExpress == null || this.RightExpress.Count == 0) return false;

            decimal intLeftvalue = 0.0m;
            decimal intValue = 0.0m;

            List<Guid> ids = new List<Guid>();
            foreach (string val in this.RightExpress)
            {
               
                if (this.Operator.Equals("equal"))
                {
                    if (val.Equals(this.LeftExpress))
                    {
                        ids.Add(Guid.Empty);
                    }
                }
                else if (this.Operator.Equals("like"))
                {
                    if (this.LeftExpress.Contains(val))
                    {
                        ids.Add(Guid.Empty);
                    }
                }
                else if (this.Operator.Equals("is null"))
                {
                    if (string.IsNullOrEmpty(this.LeftExpress) ||this.LeftExpress==Guid.Empty.ToString())
                    {
                        ids.Add(Guid.Empty);
                    }
                }
                else if (this.Operator.Equals(">="))
                {
                    try
                    {
                        intLeftvalue = Convert.ToDecimal(LeftExpress);
                        intValue = Convert.ToDecimal(val);
                    }
                    catch { };
                    if (intLeftvalue >= intValue)
                    {
                        ids.Add(Guid.Empty);
                    }

                }
                else if (this.Operator.Equals("<="))
                {
                    try
                    {
                        intLeftvalue = Convert.ToDecimal(LeftExpress);
                        intValue = Convert.ToDecimal(val);
                    }
                    catch { };
                    if (intLeftvalue <= intValue)
                    {
                        ids.Add(Guid.Empty);
                    }

                }
                else if (this.Operator.Equals("<>"))
                {
                    try
                    {
                        intLeftvalue = Convert.ToDecimal(LeftExpress);
                        intValue = Convert.ToDecimal(val);
                    }
                    catch { };
                    if (intLeftvalue != intValue)
                    {
                        ids.Add(Guid.Empty);
                    }
                }

            }

            return ids.Count > 0;
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
	}
}
