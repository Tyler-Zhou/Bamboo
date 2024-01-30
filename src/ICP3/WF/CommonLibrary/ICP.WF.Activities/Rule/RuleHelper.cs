using System;
using System.IO;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 处理规则辅助方法类

    /// </summary>
    public class RuleHelper
	{
        /// <summary>
        /// 刷新规则到指定文件
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="activity"></param>
        internal static void FlushRules(IServiceProvider serviceProvider, Activity activity)
        {
            ICPRuleSet definitions = (ICPRuleSet)activity.GetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty);
            if (definitions != null)
            {
                WorkflowDesignerLoader service = (WorkflowDesignerLoader)serviceProvider.GetService(typeof(WorkflowDesignerLoader));
                if (service != null)
                {
                    string filePath = string.Empty;
                    if (!string.IsNullOrEmpty(service.FileName))
                    {
                        filePath = Path.Combine(Path.GetDirectoryName(service.FileName), Path.GetFileNameWithoutExtension(service.FileName));
                    }
                    filePath = filePath + ".rules";

                    RuleSetSerializer.SerializeToFile<ICPRuleSet>(definitions, filePath);

                    //service.Flush();
                }
            }
        }


        /// <summary>
        /// 直接从文件中加载 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        internal static ICPRuleSet LoadRulesDT(IServiceProvider serviceProvider, DependencyObject activity)
        {

            ICPRuleSet definitions = (ICPRuleSet)activity.GetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty);
            if (definitions == null || definitions.Conditions == null || definitions.Conditions.Count == 0)
            {
                WorkflowDesignerLoader service = (WorkflowDesignerLoader)serviceProvider.GetService(typeof(WorkflowDesignerLoader));
                if (service != null)
                {
                    string filePath = string.Empty;
                    if (!string.IsNullOrEmpty(service.FileName))
                    {
                        filePath = Path.Combine(Path.GetDirectoryName(service.FileName), Path.GetFileNameWithoutExtension(service.FileName));
                    }
                    filePath = filePath + ".rules";
                    try
                    {
                       definitions= RuleSetSerializer.DeserializeFromFile(typeof(ICPRuleSet), filePath) as ICPRuleSet;
                    }
                    catch (Exception)
                    {
                        definitions = new ICPRuleSet();
                    }
                }

                if (definitions == null) definitions = new ICPRuleSet();
                activity.SetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty, definitions);
            }
            return definitions;
        }

        /// <summary>
        /// 先找缓存，如果不存在在去从文件中取 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="declaringActivity"></param>
        /// <returns></returns>
        internal static ICPRuleSet LoadRules(IServiceProvider serviceProvider, Activity declaringActivity)
        {
            ICPRuleSet ruleDefinitions = declaringActivity.GetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty) as ICPRuleSet;
            if (ruleDefinitions == null)
            {
                ruleDefinitions = LoadRulesDT(serviceProvider, declaringActivity);
                if (ruleDefinitions != null)
                {
                    declaringActivity.SetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty, ruleDefinitions);
                }
            }
            return ruleDefinitions;
        }
	}
}
