using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using ICP.WF.Activities.Common;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.WF.Activities;

namespace ICP.WF.Activities
{
    [ActivityValidator(typeof(ICPRuleValidator)), TypeConverter(typeof(ICPRuleTypeConverter)), SRDisplayName("ConditionsOfTheApplicant")]
	public class ICPActivityCondition:ActivityCondition
    {
        #region 本地变量

        private string _condition;
        private bool _runtimeInitialized;
        private string declaringActivityId = string.Empty;
        [NonSerialized]
        private object syncLock = new object();

        #endregion


        #region ActivityCondition方法重载

        public override bool Evaluate(Activity activity, IServiceProvider provider)
        {
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            //检查是否设置条件
            if (string.IsNullOrEmpty(this.ConditionName))
            {
                throw new InvalidOperationException(activity.Name+":未设置条件.");
            }

            //检查是否规则文件存在
            ICPRuleSet ruleDefinitions = WFHelpers.GetRootActivity(activity).GetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty) as ICPRuleSet;
            if ((ruleDefinitions == null) || (ruleDefinitions.Conditions == null))
            {
                throw new InvalidOperationException("未能加载规则文件");
            }

            //检测该分支的条件是否存在
            ICPCondition condition = ruleDefinitions.Conditions.Find(delegate(ICPCondition con) 
            {
                if (string.IsNullOrEmpty(con.ConditionName)) return false;

                return con.ConditionName.Equals(this.ConditionName);
            });
            if (condition == null)
            {
                throw new InvalidOperationException("规则文件中的该规则丢失:"+this.ConditionName);
            }

            //判断当前用户是否有权限执行走该条分支
            IWorkFlowExtendService wService = (IWorkFlowExtendService)provider.GetService(typeof(IWorkFlowExtendService));
            if (wService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            string workflowId = string.Empty;
            Dictionary<string, object> dataCollect = WFHelpers.GetRootActivity(activity).GetValue(DefaultSequenceActivity.DataCollectionProperty) as Dictionary<string, object>;
            if (dataCollect.ContainsKey(WWFConstants.WorkflowId_C) && string.IsNullOrEmpty(workflowId))
            {
                workflowId = dataCollect[WWFConstants.WorkflowId_C].ToString();
            }

            if (dataCollect.ContainsKey(WWFConstants.WorkflowId_E) && string.IsNullOrEmpty(workflowId))
            {
                workflowId = dataCollect[WWFConstants.WorkflowId_E].ToString();
            }
            IWorkflowService wfService = (IWorkflowService)provider.GetService(typeof(IWorkflowService));
            if (wfService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }
            Dictionary<string, object> obs = wfService.GetDataCollect(new Guid(workflowId)).DataCollect;
            foreach (string key in obs.Keys)
            {
                condition.Rule.ReplaceRuleValue(key, obs[key] == null ? string.Empty : obs[key].ToString());
            }

            string callId = WFHelpers.GetRootActivity(activity).GetValue(DefaultSequenceActivity.ProposerProperty).ToString();

            bool isSucc = condition.Rule.Evaluate(wService, new Guid(callId));
            //if (users.Count == 1 && users[0]==Guid.Empty)
            //{//表单条件满足的情况下。。
            //    return true;
            //}

            //return users.Contains(new Guid(callId));

            return isSucc;
        }

        protected override void InitializeProperties()
        {
            lock (this.syncLock)
            {
                if (!this._runtimeInitialized)
                {
                    Activity parentDependencyObject = base.ParentDependencyObject as Activity;
                    CompositeActivity declaringActivity = GetCompositeActivity(parentDependencyObject);
                    this.declaringActivityId = declaringActivity.QualifiedName;
                    base.InitializeProperties();
                    this._runtimeInitialized = true;
                }
            }
        }

        #endregion


        #region 本地方法

        private static ICPRuleSet GetRuleDefinitions(IServiceProvider provider,Activity activity, out CompositeActivity declaringActivity)
        {
            declaringActivity = WFHelpers.GetDeclaringActivity(activity);
            if (declaringActivity == null)
            {
                declaringActivity = WFHelpers.GetRootActivity(activity) as CompositeActivity;
            }

            return RuleHelper.LoadRulesDT(provider, declaringActivity);
        }

        private static CompositeActivity GetCompositeActivity(Activity activity)
        {
            CompositeActivity  declaringActivity = WFHelpers.GetDeclaringActivity(activity);
            if (declaringActivity == null)
            {
                declaringActivity = WFHelpers.GetRootActivity(activity) as CompositeActivity;
            }
            return declaringActivity;
        }
        #endregion



        #region 公共方法
        /// <summary>
        /// 条件名称
        /// </summary>
        public string ConditionName
        {
            get
            {
                return this._condition;
            }
            set
            {
                this._condition = value;
            }
        }

        #endregion
    }
}
