using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using ICP.WF.Activities.Common;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 申请活动-该活动必须是流程的第一个节点
    /// </summary>
    [ToolboxItem(typeof(ActivityToolboxItem)), ToolboxBitmap(typeof(ApplicationActivity), "Resources.ApplicationActivity.bmp")]
    [ActivityValidator(typeof(ApplicationActivityValidator))]
    [SRDescription("DescApplicationActivity"), SRCategory("Standard"), SRTitle("TitleApplicationActivity")]
    [Designer(typeof(ApplicationActivityDesigner), typeof(IDesigner))]
    public partial class ApplicationActivity : Activity, IValidateService
    {

        #region 构造函数


        public ApplicationActivity()
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

        object formFile;
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


        FunctionData postFunction = null;
        /// <summary>
        /// 完成方法
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


        FunctionData initFunction = null;
        /// <summary>
        /// 初始化方法
        /// </summary>
        [SRDisplayName("AppInitEvent"), ICPBrowsable(true), Editor(typeof(PostFunctionEditor), typeof(UITypeEditor)), RefreshProperties(RefreshProperties.All), DefaultValue((string)null), SRCategory("Custom"), SRDescription("InitEvent")]
        public FunctionData InitEvent
        {
            get
            {
                return initFunction;
            }
            set
            {
                initFunction = value;
            }
        }
        #endregion

        #region  方法重载

        private ActivityExecutionStatus ExcuteAcitivity(ActivityExecutionContext executionContext)
        {
            //判断服务是否已经注入
            IWorkflowService wService = (IWorkflowService)executionContext.GetService(typeof(IWorkflowService));
            if (wService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            //取出业务外面传入数据<考虑到有可能是退回的单，需要将之前的数据全部重新保存进去>
            DataSet data = null;
            Dictionary<string, object> dataCollect = WFHelpers.GetRootActivity(this).GetValue(DefaultSequenceActivity.DataCollectionProperty) as Dictionary<string, object>;
            if (dataCollect != null && dataCollect.ContainsKey(WWFConstants.MainWorkItemDataSet))
            {
                data = dataCollect[WWFConstants.MainWorkItemDataSet] as DataSet;
            }
            string caller = WFHelpers.GetRootActivity(this).GetValue(DefaultSequenceActivity.ProposerProperty).ToString();
            string callerDepartment = WFHelpers.GetRootActivity(this).GetValue(DefaultSequenceActivity.ProposerDepartmentProperty).ToString();


            //加入当前任务
            Guid workItemId = wService.ApplyTask(
                this.WorkflowInstanceId,
                this.FormFile.ToString(),
                this.CName,
                this.EName,
                new Guid(caller),
                data,
                new Guid[] { new Guid(caller) });

            //执行InitEvent函数
            if (InitEvent != null)
            {
                InitEvent.Excute(this.WorkflowInstanceId, wService);
            }

            //加入队列.等待用户完成
            string qName = workItemId.ToString();
            WorkflowQueuingService qServ = executionContext.GetService<WorkflowQueuingService>();
            WorkflowQueue q = qServ.CreateWorkflowQueue(qName, false);
            q.QueueItemAvailable += OnContineNextStep;

            return ActivityExecutionStatus.Executing;
        }

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            if (this.Enabled == false) return ActivityExecutionStatus.Closed;

            return ExcuteAcitivity(executionContext);
        }


        protected override ActivityExecutionStatus HandleFault(ActivityExecutionContext executionContext, Exception exception)
        {
            return ActivityExecutionStatus.Closed;
        }

        private void OnContineNextStep(object sender, QueueEventArgs e)
        {
            //如果外部任务完成。则标志该任务完成 。并进入下一步任务中
            ActivityExecutionContext context = sender as ActivityExecutionContext;
            if (context == null)
            {
                throw new ArgumentNullException("sender");
            }

            WorkflowQueuingService qServ = context.GetService<WorkflowQueuingService>();
            if (qServ.Exists(e.QueueName) == false) return;

            WorkflowQueue q = qServ.GetWorkflowQueue(e.QueueName);
            EnqueueItem result = (EnqueueItem)q.Dequeue();
            if (result != null && result.Opinion)
            {
                if (result.Participants != null && result.Participants.Length > 0)
                {
                    //加入指派人列表到队列中

                    string keyname = this.WorkflowInstanceId.ToString() + "Participants";
                    WorkflowQueue qw;

                    if (qServ.Exists(keyname))
                    {
                        qw = qServ.GetWorkflowQueue(keyname);
                        while (qw.Count != 0)
                        {
                            qw.Dequeue();
                        }
                    }
                    else
                    {
                        qw = qServ.CreateWorkflowQueue(keyname, false);
                    }
                    qw.Enqueue(result.Participants);
                }

                //删除该队列

                qServ.DeleteWorkflowQueue(e.QueueName);

                //执行PostFounction函数
                IWorkflowService wService = (IWorkflowService)context.GetService(typeof(IWorkflowService));
                if (wService != null && CompleteEvent != null)
                {
                    CompleteEvent.Excute(this.WorkflowInstanceId, wService);
                }

                //任务完成
                context.CloseActivity();
            }
        }
        #endregion

        #region IValidateService接口实现

        public bool Validate(List<string> errors)
        {
            if (this.Enabled == false) return true;

            if (errors == null) errors = new List<string>();
            bool isSucc = true;

            if (string.IsNullOrEmpty(this.CName))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [CName] property", this.Name, SR.GetString("CName", "CName")));
                isSucc = false;
            }

            if (string.IsNullOrEmpty(this.EName))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [EName] property", this.Name, SR.GetString("EName", "EName")));
                isSucc = false;
            }

            if (this.FormFile == null || string.IsNullOrEmpty(this.FormFile.ToString()))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [FormFile] property", this.Name, SR.GetString("FormFile", "FormFile")));
                isSucc = false;
            }

            return isSucc;
        }

        #endregion
    }

    /// <summary>
    /// 申请活动验证逻辑
    /// </summary>
    public class ApplicationActivityValidator : ActivityValidator
    {
        public override ValidationErrorCollection Validate(ValidationManager manager, object obj)
        {
            ValidationErrorCollection errors = new ValidationErrorCollection();
            errors.AddRange(base.Validate(manager, obj));

            return errors;
        }
    }


    /// <summary>
    /// 申请活动设计器组件
    /// </summary>
    [ActivityDesignerTheme(typeof(ApplicationActivityDesignerTheme))]
    public class ApplicationActivityDesigner : ActivityDesigner
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
            Bitmap bmp = Properties.Resources.ApplicationActivity;
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
            ApplicationActivityDesignerTheme compositeDesignerTheme = (ApplicationActivityDesignerTheme)e.DesignerTheme;
            ActivityDesignerPaint.DrawRoundedRectangle(graphics, compositeDesignerTheme.BorderPen, this.Bounds, compositeDesignerTheme.BorderWidth);

            string text = this.Text;
            ApplicationActivity activity = this.Activity as ApplicationActivity;
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
    public class ApplicationActivityDesignerTheme : ActivityDesignerTheme
    {
        public ApplicationActivityDesignerTheme(WorkflowTheme theme)
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
