using System.Collections.Generic;
using System.Text;

namespace ICP.EDI.ServiceComponent.Rule
{
    /// <summary>
    /// 规则接口
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// 父规则
        /// </summary>
        IRule ParentRule { get; set; }


        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="errorInfo"></param>
        void Validate(object value,ref StringBuilder errorInfo);


        /// <summary>
        /// 初始化规则数据
        /// </summary>
        /// <param name="node"></param>
        void InitRule(System.Xml.XmlNode node);
    }

    /// <summary>
    /// 能包含规则的父容器
    /// </summary>
    public interface IRuleContainer : IRule
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 中文描述
        /// </summary>
        string CDesc { get; set; }

        /// <summary>
        /// 英文描述
        /// </summary>
        string EDesc { get; set; }


        /// <summary>
        /// 子规则集
        /// </summary>
        List<IRule> ChildRules { get; set; }


        /// <summary>
        /// 添加规则
        /// </summary>
        /// <param name="rule"></param>
        void AddRule(IRule rule);

    }
}
