using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using ICP.WF.Activities;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.WF.Activities.Common;
using ICP.Framework.CommonLibrary.Attributes;
using System.Data;
using ICP.Framework.ClientComponents.Controls;
using System.ServiceModel;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 审批活动
    /// </summary>
    [ToolboxItem(typeof(ActivityToolboxItem)), ToolboxBitmap(typeof(ApproveActivity), "Resources.ApproveActivity.bmp")]
    [Designer(typeof(ApproveActivityDesigner), typeof(IDesigner))]
    [SRDescription("DescApproveActivity"), SRCategory("Standard"), SRTitle("TitleApproveActivity")]
    [ActivityValidator(typeof(ApproveActivityValidator))]
    public partial class ApproveActivity: Activity,IValidateService
    {
        #region 构造函数


        public ApproveActivity()
		{
			InitializeComponent();
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 名称
        /// </summary>
        [SRDisplayName("DispName"), ICPBrowsable(true), SRCategory("Base")]
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        [SRDisplayName("DispDescription"), ICPBrowsable(true), SRCategory("Base")]
        public new string Description
        {
            get
            {
                return base.Description;
            }
            set
            {
                base.Description = value;
            }
        }

        string eName = "";
        /// <summary>
        /// 英文名称
        /// </summary>
        [SRDisplayName("EName"), ICPBrowsable(true), SRCategory("Custom"), SRDescription("EnglishDescription")]
        public string EName
        {
            get { return eName; }
            set { eName = value; }
        }

        string cName;
        /// <summary>
        /// 中文名称
        /// </summary>
        [SRDisplayName("CName"), ICPBrowsable(true), SRCategory("Custom"), SRDescription("ChineseDescription")]
        public string CName
        {
            get { return cName; }
            set { cName = value; }
        }

        object formFile = "Approve";
        /// <summary>
        /// 关联的表单
        /// </summary>
        [SRDisplayName("FormFile"), ICPBrowsable(true), Editor(typeof(FormFileEditor), typeof(UITypeEditor)), DefaultValue((string)null), SRCategory("Custom"), SRDescription("AssociatedForm")]
        public object FormFile
        {
            get
            {
                return formFile;
            }
            set
            {
                formFile = value;
            }
        }

        bool must = true;
        /// <summary>
        /// 必须执行
        /// </summary>
        [SRDisplayName("DispMust"), ICPBrowsable(true), SRCategory("Custom"), SRDescription("MustDescription")]
        public bool Must
        {
            get
            {
                return must;
            }
            set
            {
                must = value;
            }
        }

        string _conditionName;
        [SRDisplayName("AuditorConditionName"), ICPBrowsable(true), SRCategory("Custom"), SRDescription("TaskHolders"), Editor(typeof(CurrentStepExcutorEditor), typeof(UITypeEditor))]
        public string ConditionName
        {
            get { return _conditionName; }
            set { _conditionName = value; }
        }

        FunctionData postFunction=null;
        /// <summary>
        /// 执行的外部方法
        /// </summary>
        [SRDisplayName("DispCompleteEvent"), ICPBrowsable(true), Editor(typeof(PostFunctionEditor), typeof(UITypeEditor)), RefreshProperties(RefreshProperties.All), DefaultValue((string)null), SRCategory("Custom"), SRDescription("CompleteEvent")]
        public FunctionData CompleteEvent
        {
            get
            {
                return postFunction;
            }
            set
            {
                postFunction = value;
            }
        }

        #endregion

        #region 方法重载 

        /*执行*/
        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            //Enabled==false直接跳过
            if (this.Enabled == false) return ActivityExecutionStatus.Closed;

            IWorkflowService wService = (IWorkflowService)executionContext.GetService(typeof(IWorkflowService));
            if (wService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            #region 计算可以签收该任务的人


            List<Guid> users=new List<Guid>();

            //先从队列中找（如果队列中有，则说明是用户指派的），如果不存在根据条件查询
            WorkflowQueuingService qServ = executionContext.GetService<WorkflowQueuingService>();
            string keyname = this.WorkflowInstanceId.ToString() + "Participants";
            if (qServ.Exists(keyname))
            {
                WorkflowQueue q = qServ.GetWorkflowQueue(keyname);
                if (q != null)
                {
                    users.AddRange((Guid[])q.Dequeue());

                    //删除存放代办人列表的队列
                    qServ.DeleteWorkflowQueue(keyname);
                }
            }

            //根据条件查询
            if (users == null || users.Count == 0)
            {
                users = GetExcutors(executionContext, executionContext.Activity);
            }
          
            if (users == null || users.Count == 0)
            {
                if (must)
                {
                    WorkflowExecutorNullExceptionInfo myerr = new WorkflowExecutorNullExceptionInfo();
                    myerr.CallerId = Guid.Empty;
                    myerr.WorkitemId = Guid.Empty;                
                    myerr.WName = SR.IsEnglish ? this.eName : this.cName;
                    FaultException<WorkflowExecutorNullExceptionInfo> err = new FaultException<WorkflowExecutorNullExceptionInfo>(myerr);
                    
                    throw err;

                    //没有下一步执行人，弹出指派的界面
                    //throw new WorkflowExecutorNullException(SR.IsEnglish ? this.eName : this.cName, Guid.Empty, Guid.Empty);
                }
                else
                {
                    return ActivityExecutionStatus.Closed;
                }
            }

            #endregion

            #region 加入当前任务
            //下一步执行的表单，在服务端设置，此处只需要传入空值
            DataSet data = null;
            string caller = WFHelpers.GetRootActivity(this).GetValue(DefaultSequenceActivity.ProposerProperty).ToString();
            string callerDepartment = WFHelpers.GetRootActivity(this).GetValue(DefaultSequenceActivity.ProposerDepartmentProperty).ToString();



            //加入当前任务
            Guid workItemId = wService.NewTask(
                this.WorkflowInstanceId,
                this.formFile.ToString(),
                this.CName,
                this.EName,
                new Guid(caller),  
                data, 
                users.ToArray());


            #endregion

            #region 加入队列.等待用户完成

            string qName = workItemId.ToString();
            WorkflowQueue qw = qServ.CreateWorkflowQueue(qName, false);
            qw.QueueItemAvailable += OnContinueNextStep;

            return ActivityExecutionStatus.Executing;

            #endregion
        }

        /*失败时候处理*/
        protected override ActivityExecutionStatus HandleFault(ActivityExecutionContext executionContext, Exception exception)
        {
            return ActivityExecutionStatus.Closed;
        }

        #endregion

        #region 事件处理

        private void OnContinueNextStep(object sender, QueueEventArgs e)
        {
            ActivityExecutionContext context = sender as ActivityExecutionContext;
            if (context == null)
            {
                throw new ArgumentNullException("sender");
            }

            WorkflowQueuingService qServ = context.GetService<WorkflowQueuingService>();
            if (qServ.Exists(e.QueueName) == false) return;

            WorkflowQueue q = qServ.GetWorkflowQueue(e.QueueName);
            EnqueueItem result = (EnqueueItem)q.Dequeue();
            if (result != null)
            {
                qServ.DeleteWorkflowQueue(e.QueueName);
                if (result.Opinion)
                {
                    if (result.Participants != null && result.Participants.Length > 0)
                    {
                        //加入指派人列表到队列中
                        string keyname = this.WorkflowInstanceId.ToString() + "Participants";
                        WorkflowQueue qw = qServ.CreateWorkflowQueue(keyname, false);
                        qw.Enqueue(result.Participants);
                    }

                    //执行PostFounction函数
                    IWorkflowService wService = (IWorkflowService)context.GetService(typeof(IWorkflowService));
                    if (wService != null && postFunction != null)
                    {
                        postFunction.Excute(this.WorkflowInstanceId, wService);
                    }

                    //如果审批同意，继续下一步。 
                    context.CloseActivity();
                }
                else
                {
                    //如果不同意，则取消该流程
                    context.CancelActivity(WFHelpers.GetRootActivity(this));
                }
            }
        }

        #endregion

        #region 本地方法

        /*根据条件获取，可以签收当前任务的人*/
        private List<Guid> GetExcutors(IServiceProvider serviceProvider, Activity declaringActivity)
        {
            IWorkFlowExtendService wService = (IWorkFlowExtendService)serviceProvider.GetService(typeof(IWorkFlowExtendService));
            if (wService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            IWorkflowService wfService = (IWorkflowService)serviceProvider.GetService(typeof(IWorkflowService));
            if (wfService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            ICPRuleSet rs =WFHelpers.GetRootActivity(declaringActivity).GetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty) as ICPRuleSet;
            if (string.IsNullOrEmpty(ConditionName))
            {
                throw new ConditionNameNotFoundException();
            }

            ICPCondition condition = rs.Conditions.Find(delegate(ICPCondition con) { return con.ConditionName.Equals(ConditionName); });
            if (condition == null)
            {
                throw new ConditionNotFoundException();
            }

            Dictionary<string, object> obs = wfService.GetDataCollect(this.WorkflowInstanceId).DataCollect;
            foreach (string key in obs.Keys)
            {
                if (obs[key] != null)
                {
                    condition.Rule.ReplaceRuleValue(key, obs[key].ToString());
                }
            }
            List<Guid> users = condition.Rule.Evaluate(wService);
            return users;
        }

        #endregion

        #region IValidateService接口实现

        public bool Validate(List<string> errors)
        {
            //Enabled==false不执行验证

            if (this.Enabled == false) return true;

            if (errors == null) errors = new List<string>();
            bool isSucc = true;

            if (string.IsNullOrEmpty(this.CName))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [CName] property", this.Name, SR.GetString("CName","CName")));
                isSucc = false;
            }
            if (this.formFile == null || string.IsNullOrEmpty(this.formFile.ToString()))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [FormFile] property", this.Name, SR.GetString("FormFile", "FormFile")));
                isSucc = false;
            }
            if (string.IsNullOrEmpty(this.EName))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [EName] property", this.Name, SR.GetString("EName","EName")));
                isSucc = false;
            }

            if (string.IsNullOrEmpty(this.ConditionName))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [ConditionName] property", this.Name, SR.GetString("ConditionName","ConditionName")));
                isSucc = false;
            }

            return isSucc;
        }

        #endregion
    }


    /// <summary>
    /// 审批活动验证逻辑
    /// </summary>
    public class ApproveActivityValidator : ActivityValidator
    {
        public override ValidationErrorCollection Validate(ValidationManager manager, object obj)
        {
            ValidationErrorCollection errors = new ValidationErrorCollection();
            //ApproveActivity activity = obj as ApproveActivity;
            errors.AddRange(base.Validate(manager, obj));
            return errors;
        }
    }


    /// <summary>
    /// 审批活动设计器组件
    /// </summary>
    [ActivityDesignerTheme(typeof(ApproveActivityDesignerTheme))]
    public class ApproveActivityDesigner : ActivityDesigner
    {
        public override bool CanBeParentedTo(CompositeActivityDesigner parentActivityDesigner)
        {
            return true;
        }

        protected override Rectangle ImageRectangle
        {
            get
            {
                Rectangle bounds = this.Bounds;
                Size sz = new Size(24, 24);
                Rectangle imageRect = new Rectangle();
                imageRect.X = bounds.Left + ((bounds.Width - sz.Width) / 2);
                imageRect.Y = bounds.Top + 4;
                imageRect.Size = sz;
                return imageRect;
            }
        }

        protected override Rectangle TextRectangle
        {
            get
            {
                return new Rectangle(
                    this.Bounds.Left + 2,
                    this.ImageRectangle.Bottom,
                    this.Bounds.Width - 4,
                    this.Bounds.Height - this.ImageRectangle.Height - 1);
            }
        }

        protected override void Initialize(Activity activity)
        {
            base.Initialize(activity);
            Bitmap bmp = Properties.Resources.ApproveActivity;
            bmp.MakeTransparent();
            this.Image = bmp;
        }

        readonly static Size BaseSize = new Size(64, 64);
        protected override Size OnLayoutSize(ActivityDesignerLayoutEventArgs e)
        {
            return BaseSize;
        }



        protected override void OnPaint(ActivityDesignerPaintEventArgs e)
        {
            DrawCustomActivity(e);
        }

        private void DrawCustomActivity(ActivityDesignerPaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            ApproveActivityDesignerTheme compositeDesignerTheme = (ApproveActivityDesignerTheme)e.DesignerTheme;
            ActivityDesignerPaint.DrawRoundedRectangle(graphics, compositeDesignerTheme.BorderPen, this.Bounds, compositeDesignerTheme.BorderWidth);

            string text = this.Text;
            ApproveActivity activity = this.Activity as ApproveActivity;
            if (activity != null)
            {
                text = string.IsNullOrEmpty(activity.CName) ? activity.EName : activity.CName;
            }

            Rectangle textRectangle = this.TextRectangle;
            if (!string.IsNullOrEmpty(text) && !textRectangle.IsEmpty)
            {
                ActivityDesignerPaint.DrawText(graphics, compositeDesignerTheme.Font, text, textRectangle, StringAlignment.Center, e.AmbientTheme.TextQuality, compositeDesignerTheme.ForegroundBrush);
            }

            System.Drawing.Image image = this.Image;
            Rectangle imageRectangle = this.ImageRectangle;
            if (image != null && !imageRectangle.IsEmpty)
            {
                ActivityDesignerPaint.DrawImage(graphics, image, imageRectangle, DesignerContentAlignment.Fill);
            }

        }
    }


    /// <summary>
    /// 设计时环境中的设计器提供外观属性设置
    /// </summary>
    public class ApproveActivityDesignerTheme : ActivityDesignerTheme
    {
        public ApproveActivityDesignerTheme(WorkflowTheme theme)
            : base(theme)
        {
            base.Initialize();
            this.BorderStyle = DashStyle.Solid;
            this.BorderColor = Color.FromArgb(0, 0, 0);
            this.BackColorStart = Color.FromArgb(37, 15, 242);
            this.BackColorEnd = Color.FromArgb(189, 184, 254);
            this.BackgroundStyle = LinearGradientMode.Vertical;
            this.ForeColor = Color.Black;
        }
    }
}
