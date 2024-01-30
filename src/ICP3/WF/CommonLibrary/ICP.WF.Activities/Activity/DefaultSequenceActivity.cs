using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using ICP.WF.Activities.Common;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 默认主活动
    /// </summary>
    [Designer(typeof(DefaultSequentialWorkflowRootDesigner), typeof(IRootDesigner))]
    public partial class DefaultSequenceActivity : SequentialWorkflowActivity,IValidateService
    {
        #region 构造函数 

        public DefaultSequenceActivity()
        {
            InitializeComponent();
        }

        #endregion

        #region 公共属性

        public static DependencyProperty WorkFlowTitleProperty =
            DependencyProperty.Register("WorkFlowTitle", typeof(string), typeof(DefaultSequenceActivity), new PropertyMetadata());

        public static DependencyProperty ProposerProperty =
         DependencyProperty.Register("Proposer", typeof(string), typeof(DefaultSequenceActivity), new PropertyMetadata());

        public static DependencyProperty ProposerDepartmentProperty =
       DependencyProperty.Register("ProposerDepartment", typeof(string), typeof(DefaultSequenceActivity), new PropertyMetadata());


        public static DependencyProperty WorkFlowKeyProperty =
            DependencyProperty.Register("WorkFlowKey", typeof(string), typeof(DefaultSequenceActivity), new PropertyMetadata());


        public static DependencyProperty DataCollectionProperty =
         DependencyProperty.Register("DataCollection", typeof(Dictionary<string, object>), typeof(DefaultSequenceActivity), new PropertyMetadata());


        public static DependencyProperty ICPRuleDefinitionsProperty =
        DependencyProperty.Register("ICPRuleDefinitions", typeof(ICPRuleSet), typeof(DefaultSequenceActivity), new PropertyMetadata());


        /// <summary>
        /// 工作流标题（打印时候使用），默认是流程名称
        /// </summary>
        [ICPBrowsable(true), SRDisplayName("Title"), SRCategory("Custom"), SRDescription("Title")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string WorkFlowTitle
        {
            get { return (string)base.GetValue(WorkFlowTitleProperty); }
            set { base.SetValue(WorkFlowTitleProperty, value); }
        }

        /// <summary>
        /// 工作流Key-根据该Key查找加载的流程
        /// </summary>
        [Browsable(false)]
        public string WorkFlowKey
        {
            get { return (string)base.GetValue(WorkFlowKeyProperty); }
            set { base.SetValue(WorkFlowKeyProperty, value); }
        }



        /// <summary>
        /// 当前用户
        /// </summary>
        [Browsable(false),]
        public string Proposer
        {
            get { return (string)base.GetValue(ProposerProperty); }
            set { base.SetValue(ProposerProperty, value); }
        }



        /// <summary>
        /// 申请部门
        /// </summary>
        [Browsable(false),]
        public string ProposerDepartment
        {
            get { return (string)base.GetValue(ProposerDepartmentProperty); }
            set { base.SetValue(ProposerDepartmentProperty, value); }
        }

     
        /// <summary>
        /// 储存表单数据列表
        /// </summary>
        [Browsable(false)]
        public Dictionary<string, object> DataCollection
        {
            get { return (Dictionary<string, object>)GetValue(DataCollectionProperty); }
            set { SetValue(DataCollectionProperty, value); }
        }

        #endregion


        #region 方法重载 
        
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            return base.Execute(executionContext);
        }


        protected override ActivityExecutionStatus HandleFault(ActivityExecutionContext executionContext, Exception exception)
        {
            return ActivityExecutionStatus.Closed;
        }



        protected override void Initialize(System.IServiceProvider provider)
        {
            base.Initialize(provider);

            if (DataCollection.ContainsKey("RuleFile") && DataCollection["RuleFile"] != null)
            {
                string ruleFile = DataCollection["RuleFile"].ToString();
                ICPRuleSet set = RuleSetSerializer.DeserializeFromString(typeof(ICPRuleSet), ruleFile) as ICPRuleSet;
                this.SetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty, set);
            }

            Dictionary<string, object> dataCollect = WFHelpers.GetRootActivity(this).GetValue(DefaultSequenceActivity.DataCollectionProperty) as Dictionary<string, object>;

            if (dataCollect != null)
            {
                if (dataCollect.ContainsKey(WWFConstants.WorkflowId_C) == false)
                {
                    dataCollect.Add(WWFConstants.WorkflowId_C, this.WorkflowInstanceId.ToString());
                }

                if (dataCollect.ContainsKey(WWFConstants.WorkflowId_E) == false)
                {
                    dataCollect.Add(WWFConstants.WorkflowId_E, this.WorkflowInstanceId.ToString());
                }

                this.SetValue(DefaultSequenceActivity.DataCollectionProperty, dataCollect);
            }
        }
        protected override void InitializeProperties()
        {
            base.InitializeProperties();
        }
        #endregion

        public bool Validate(List<string> errors)
        {

            IList<Activity> activities= WFHelpers.GetAllNestedActivities(this);
            List<string> cnames = new List<string>();
            List<string> enames = new List<string>();

            List<string> keyList = new List<string>(); 

            foreach (Activity a in activities)
            {
                IValidateService vSvc = a as IValidateService;
                if (vSvc != null)
                {
                    vSvc.Validate(errors);
                }

                #region 程序活动
                ApplicationActivity aplc = a as ApplicationActivity;
                if (aplc != null)
                {
                    if (cnames.Contains(aplc.CName))
                    {
                        errors.Add(SR.GetString("CNameRepeat", "[{0}]中文标题[{1}]存在重复", aplc.Name, aplc.CName));
                    }
                    else
                    {
                        cnames.Add(aplc.CName);
                    }

                    if (enames.Contains(aplc.EName))
                    {
                        errors.Add(SR.GetString("ENameRepeat", "[{0}]英文标题[{1}]存在重复", aplc.Name, aplc.EName));
                    }
                    else
                    {
                        enames.Add(aplc.EName);
                    }

                    //if (!string.IsNullOrEmpty(aplc.FormFile) && !keyList.Contains(aplc.FormFile))
                    //{
                    //    errors.Add(SR.GetString("FormNotExists", "[{0}]关联的表单[{1}]不存在",aplc.Name,aplc.FormFile));
                    //}

                }


                #endregion

                #region 审核
                ApproveActivity aprv = a as ApproveActivity;
                if (aprv != null)
                {
                    if (cnames.Contains(aprv.CName))
                    {
                        errors.Add(SR.GetString("CNameRepeat", "[{0}]中文标题[{1}]存在重复", aprv.Name, aprv.CName));
                    }
                    else
                    {
                        cnames.Add(aprv.CName);
                    }

                    if (enames.Contains(aprv.EName))
                    {
                        errors.Add(SR.GetString("ENameRepeat", "[{0}]英文标题[{1}]存在重复", aprv.Name, aprv.EName));
                    }
                    else
                    {
                        enames.Add(aprv.EName);
                    }
                }
                #endregion
            }

          
            if (errors.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }


    /// <summary>
    /// 流程设计器设计类
    /// </summary>
    [ActivityDesignerTheme(typeof(DefaultSequentialWorkflowDesignerTheme))]
    public class DefaultSequentialWorkflowRootDesigner : SequentialWorkflowRootDesigner
    {
        protected override void Initialize(Activity activity)
        {
            base.Initialize(activity);

            this.Header.Text = SR.GetString("FlowChart", "流程图");
            DefaultSequenceActivity ac = activity as DefaultSequenceActivity;
            if (ac != null && string.IsNullOrEmpty(ac.WorkFlowTitle)==false)
            {
                this.Header.Text = ac.WorkFlowTitle;
            }
        }

        protected override void OnPaint(ActivityDesignerPaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.Header != null)
            {
                DefaultSequenceActivity ac = this.Activity as DefaultSequenceActivity;
                if (ac != null && string.IsNullOrEmpty(ac.WorkFlowTitle) == false)
                {
                    this.Header.Text = ac.WorkFlowTitle;
                }

                this.Header.OnPaint(e);
            }
          
        }

        public override bool CanInsertActivities(HitTestInfo insertLocation, System.Collections.ObjectModel.ReadOnlyCollection<Activity> activitiesToInsert)
        {
            return true ;
        }


        public override void InsertActivities(HitTestInfo insertLocation, System.Collections.ObjectModel.ReadOnlyCollection<Activity> activitiesToInsert)
        {
            base.InsertActivities(insertLocation, activitiesToInsert);
        }
    }

    /// <summary>
    /// 控制流程图的主题
    /// </summary>
    internal class DefaultSequentialWorkflowDesignerTheme : CompositeDesignerTheme
    {
        public DefaultSequentialWorkflowDesignerTheme(WorkflowTheme theme)
            : base(theme)
        {
            this.WatermarkAlignment = DesignerContentAlignment.BottomRight;
            this.ShowDropShadow = true;
            this.ConnectorStartCap = LineAnchor.None;
            this.ConnectorEndCap = LineAnchor.ArrowAnchor;
            this.ForeColor = Color.FromArgb(0xff, 0, 0, 0);
            this.BorderColor = Color.FromArgb(0xff, 0x49, 0x77, 180);
            this.BorderStyle = DashStyle.Solid;
            this.BackColorStart = Color.White;
            this.BackColorEnd = Color.White;
        }

    }

}
