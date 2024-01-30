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
using ICP.WF.Activities.Common;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using System.Text.RegularExpressions;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 发邮件活动
    /// </summary>
    [ToolboxItem(typeof(ActivityToolboxItem)), ToolboxBitmap(typeof(SendEMailActivity), "Resources.SendMessageActivity.bmp")]
    [Designer(typeof(SendEMailActivityDesigner), typeof(IDesigner))]
    [SRDescription("DescSendEMailActivity"), SRCategory("Standard"), SRTitle("TitleSendEMailActivity")]
    [ActivityValidator(typeof(SendEMailActivityValidator))]
    public partial class SendEMailActivity : Activity, IValidateService
    {
        #region 构造函数


        public SendEMailActivity()
        {
            InitializeComponent();
        }

        #endregion

        #region 属性

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


        /// <summary>
        /// 主题
        /// </summary>
        public static readonly DependencyProperty SubjectProperty = DependencyProperty.Register("Subject", typeof(string), typeof(SendEMailActivity), new PropertyMetadata(DependencyPropertyOptions.Metadata));
        [DefaultValue((string)null), SRCategory("EMail"), SRDescription("SubjectDesc")]
        [Editor(typeof(MessageContentEditor), typeof(UITypeEditor))]
        [SRDisplayName("DispSubject"), ICPBrowsable(true)]
        public string Subject
        {
            get
            {
                return (base.GetValue(SubjectProperty) as string);
            }
            set
            {
                base.SetValue(SubjectProperty, value);
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(SendEMailActivity), new PropertyMetadata(DependencyPropertyOptions.Metadata));
        [DefaultValue((string)null), SRCategory("EMail"), SRDescription("MailContent")]
        [Editor(typeof(MessageContentEditor), typeof(UITypeEditor))]
        [SRDisplayName("DispMail"), ICPBrowsable(true)]
        public string Message
        {
            get
            {
                return (base.GetValue(MessageProperty) as string);
            }
            set
            {
                base.SetValue(MessageProperty, value);
            }
        }


        /// <summary>
        /// 接收人
        /// </summary>
        string _conditionName;
        [Browsable(true), SRCategory("EMail"), SRDescription("Receiver")]
        [Editor(typeof(CurrentStepExcutorEditor), typeof(UITypeEditor))]
        [SRDisplayName("MailConditionName"), ICPBrowsable(true)]
        public string ConditionName
        {
            get { return _conditionName; }
            set { _conditionName = value; }
        }

        #endregion

        #region 方法重载

        protected override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            if (this.Enabled == false) return ActivityExecutionStatus.Closed;

            IWorkFlowExtendService extendService = (IWorkFlowExtendService)executionContext.GetService(typeof(IWorkFlowExtendService));
            if (extendService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            //判断服务是否已经注入
            IWorkflowService wService = (IWorkflowService)executionContext.GetService(typeof(IWorkflowService));
            if (wService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            //取出当前用户
            string caller = WFHelpers.GetRootActivity(this).GetValue(DefaultSequenceActivity.ProposerProperty).ToString();
            Guid? sender = null;
            try
            {
                sender = new Guid(caller);
            }
            catch
            {
                sender = null;
            }

            //如果邮件里面有系统常量的.需要进行处理

            string sendMsg = ProcessMessage(wService, this.Message);
            string strSubject = ProcessMessage(wService,this.Subject);

            //发送邮件给满足接受消息条件的用户

            List<Guid> users = GetExcutors(executionContext, executionContext.Activity);
            if (users != null && users.Count > 0)
            {
                extendService.SendEMail(sender.Value, users.ToArray(), strSubject, sendMsg);
            }
            return ActivityExecutionStatus.Closed;
        }

        #endregion

        #region 本地方法

        /// <summary>
        /// 邮件内容处理
        /// </summary>
        /// <param name="originalityMessage"></param>
        /// <returns></returns>
        private string ProcessMessage(IWorkflowService workflowSvc, string originalityMessage)
        {
            string rtnmsg = originalityMessage;

            Regex reg = new Regex(@"\{\$.*?\$\}", RegexOptions.Multiline);
            Match match = reg.Match(originalityMessage);
            while (match.Success && string.IsNullOrEmpty(match.Value) == false)
            {
                Dictionary<string, object> dataCollect = workflowSvc.GetDataCollect(this.WorkflowInstanceId).DataCollect;
                if (dataCollect != null)
                {
                    string key = match.Value.Replace("{$", "").Replace("$}", "").Trim();
                    string val = string.Empty;
                    if (dataCollect.ContainsKey(key))
                    {
                        val = dataCollect[key].ToString();
                    }
                    if (this.Message.Contains(match.Value))
                    {
                        rtnmsg = rtnmsg.Replace(match.Value, val);
                    }
                }
                match = match.NextMatch();
            }

            return originalityMessage;
        }

        private List<Guid> GetExcutors(IServiceProvider serviceProvider, Activity declaringActivity)
        {
            IWorkFlowExtendService wService = (IWorkFlowExtendService)serviceProvider.GetService(typeof(IWorkFlowExtendService));
            if (wService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }

            ICPRuleSet rs = WFHelpers.GetRootActivity(declaringActivity).GetValue(DefaultSequenceActivity.ICPRuleDefinitionsProperty) as ICPRuleSet;
            if (string.IsNullOrEmpty(ConditionName))
            {
                return new List<Guid>();
            }

            ICPCondition condition = rs.Conditions.Find(delegate(ICPCondition con) { return con.ConditionName.Equals(ConditionName); });
            if (condition == null)
            {
                return new List<Guid>();
            }

            IWorkflowService wfService = (IWorkflowService)serviceProvider.GetService(typeof(IWorkflowService));
            if (wfService == null)
            {
                throw new WorkFlowServiceNotFoundException();
            }
            Dictionary<string, object> obs = wfService.GetDataCollect(this.WorkflowInstanceId).DataCollect;
            foreach (string key in obs.Keys)
            {
                condition.Rule.ReplaceRuleValue(key, obs[key].ToString());
            }
      
            List<Guid> users = condition.Rule.Evaluate(wService);
            return users;
        }

        #endregion

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
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [EName] property", this.Name, SR.GetString("EName","EName")));
                isSucc = false;
            }

            if (string.IsNullOrEmpty(this.ConditionName))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [EName] property", this.Name, SR.GetString("ConditionName","ConditionName")));
                isSucc = false;
            }


            if (string.IsNullOrEmpty(this.Message))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [EName] property", this.Name, SR.GetString("Message","Message")));
                isSucc = false;
            }
            if (string.IsNullOrEmpty(this.Subject))
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [EName] property", this.Name, SR.GetString("Subject", "Subject")));
                isSucc = false;
            }

            return isSucc;
        }
    }



    /// <summary>
    /// 审批活动验证逻辑
    /// </summary>
    public class SendEMailActivityValidator : ActivityValidator
    {
        public override ValidationErrorCollection Validate(ValidationManager manager, object obj)
        {
            ValidationErrorCollection errors = new ValidationErrorCollection();
            SendEMailActivity activity = obj as SendEMailActivity;
            errors.AddRange(base.Validate(manager, obj));
            return errors;
        }
    }


    /// <summary>
    /// 审批活动设计器组件
    /// </summary>
    [ActivityDesignerTheme(typeof(SendEMailActivityDesignerTheme))]
    public class SendEMailActivityDesigner : ActivityDesigner
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
            Bitmap bmp = Properties.Resources.SendMessageActivity;
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
            SendEMailActivityDesignerTheme compositeDesignerTheme = (SendEMailActivityDesignerTheme)e.DesignerTheme;
            ActivityDesignerPaint.DrawRoundedRectangle(graphics, compositeDesignerTheme.BorderPen, this.Bounds, compositeDesignerTheme.BorderWidth);

            string text = this.Text;
            SendEMailActivity activity = this.Activity as SendEMailActivity;
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
    public class SendEMailActivityDesignerTheme : ActivityDesignerTheme
    {
        public SendEMailActivityDesignerTheme(WorkflowTheme theme)
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
