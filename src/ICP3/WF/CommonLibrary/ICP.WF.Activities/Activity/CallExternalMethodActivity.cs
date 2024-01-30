using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using ICP.WF.Activities.Common;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using System.Runtime.Serialization.Formatters.Binary;
using ICP.Framework.CommonLibrary.Attributes;
using System.Threading;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 调用业务接口活动
    /// </summary>
    [ToolboxItem(typeof(ActivityToolboxItem)), ToolboxBitmap(typeof(LWCallExternalMethodActivity), "Resources.LWCallExternMethod.png")]
    [Designer(typeof(LWCallExternalMethodActivityDesigner), typeof(IDesigner))]
    [SRDescription("DescLWCallExternalMethodActivity"), SRCategory("Standard"), SRTitle("TitleLWCallExternalMethodActivity")]
    public partial class LWCallExternalMethodActivity : Activity, IMethodFindProvider,IValidateService
	{
        public static DependencyProperty MethodProperty = DependencyProperty.Register("Method", typeof(MethodData), typeof(LWCallExternalMethodActivity), new PropertyMetadata(DependencyPropertyOptions.Metadata, new Attribute[] { new BrowsableAttribute(false), new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content) }));

		public LWCallExternalMethodActivity()
		{
			InitializeComponent();
		}

        /// <summary>
        /// 默认只对IBusinessDataExchangeService接口进行交互
        /// </summary>
        [Browsable(false)]
        public Type InterfaceType
        {
            get
            {
                return typeof(IBusinessDataExchangeService);
            }
        }

        #region 重写属性
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

        #endregion

        /// <summary>
        /// 执行的外部方法
        /// </summary>
        [SRDisplayName("MethodName"), ICPBrowsable(true), Editor(typeof(MethodDataEditor), typeof(UITypeEditor)), RefreshProperties(RefreshProperties.All), DefaultValue((string)null), SRCategory("Custom"), SRDescription("MethodName")]
        public MethodData Method
        {
            get
            {
                return (base.GetValue(MethodProperty) as MethodData);
            }
            set
            {
                base.SetValue(MethodProperty, value);
            }
        }

        List<MethodData> IMethodFindProvider.GetPropertyValues(ITypeDescriptorContext typeDescriptorContext)
        {
            List<MethodData> methods = new List<MethodData>();
            if (this.InterfaceType != null)
            {
                if (!(typeDescriptorContext.PropertyDescriptor.Name == "Method"))
                {
                    return methods;
                }
                foreach (MethodInfo info in this.InterfaceType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    if (!info.IsSpecialName)
                    {
                        MethodData data = new MethodData();
                        data.MetodName = info.Name;
                        object[] attrs = info.GetCustomAttributes(typeof(ExternalMethodAttribute), true);
                        if (attrs != null && attrs.Length > 0)
                        {
                            ExternalMethodAttribute attr = attrs[0] as ExternalMethodAttribute;
                            if (attr != null)
                            {
                                data.MethodDesc = attr.Description;
                                data.AliasName = attr.AliasName;
                                InitializeParameters(info, data.Parameters);
                                methods.Add(data);
                            }
                        }
                    }
                }
            }

            return methods;
        }


        private void InitializeParameters(MethodInfo methodBase, ParameterCollection parameters)
        {
            if (parameters == null) parameters = new ParameterCollection();

            foreach (ParameterInfo info in methodBase.GetParameters())
            {
                ParameterData data = new ParameterData();
                data.ParameterName = info.Name;
                data.ParameterType = info.ParameterType;
                object[] attrs = info.GetCustomAttributes(typeof(ExternalMetodParameterAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    ExternalMetodParameterAttribute attr = attrs[0] as ExternalMetodParameterAttribute;
                    if (attr != null)
                    {
                        data.ParameterDesc = attr.Description;
                        data.AliasName = attr.AliasName;
                    }
                }
                parameters.Add(data);
            }
        }

        protected sealed override ActivityExecutionStatus Execute(ActivityExecutionContext executionContext)
        {
            //直接跳过不不执行
            if (this.Enabled == false) return  ActivityExecutionStatus.Closed;

            if (executionContext == null)
            {
                throw new ArgumentNullException("executionContext");
            }
            if (this.InterfaceType == null)
            {
                throw new ArgumentNullException("InterfaceType");
            }

            if (this.Method != null)
            {
                Type interfaceType = this.InterfaceType;
                string methodName = this.Method.MetodName;
                object service = executionContext.GetService(interfaceType);
                if (service == null)
                {
                    throw new InvalidOperationException(SR.GetString("Error_ServiceNotFound", new object[] { this.InterfaceType }));
                }

                try
                {
                    Dictionary<string, object> vals = ((IWorkflowService)executionContext.GetService(typeof(IWorkflowService))).GetDataCollect(this.WorkflowInstanceId).DataCollect;
                    MethodInfo method = interfaceType.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
                    ParameterModifier[] parameterModifiers = null;
                    object[] messageArgs = InvokeHelper.GetParameters(method, this.Method.Parameters, vals, out parameterModifiers);
                    //LogHelper.SaveLog("----------华丽的分割线!!!-------------------");
                    //int i = 0;
                    //foreach (object obj in messageArgs)
                    //{
                    //    i++;
                    //    //LogHelper.SaveLog(i.ToString()+"   "+(obj==null?"":obj.ToString()));
                    //}
                    interfaceType.InvokeMember(this.Method.MetodName, BindingFlags.Default| BindingFlags.InvokeMethod, new ExternalDataExchangeBinder(), service, messageArgs, parameterModifiers, null, null);
                }
                catch (Exception ex)
                {
                    LogHelper.SaveLog(ex.Message);
                    throw ex;
                }
            }

            return ActivityExecutionStatus.Closed;
        }


        public bool Validate(List<string> errors)
        {
            //Enabled==false不执行验证

            if (this.Enabled == false) return true;

            if (errors == null) errors = new List<string>();
            bool isSucc = true;

            if (this.Method==null)
            {
                errors.Add(SR.GetString("NecessaryToProperty", "Necessary to set up [CName] property", this.Name, SR.GetString("MethodName", "Method")));
                isSucc = false;
            }

            return isSucc;
        }

	}


    /// <summary>
    /// 调用业务接口活动设计器组件
    /// </summary>
    [ActivityDesignerTheme(typeof(LWCallExternalMethodActivityDesignerTheme))]
    public class LWCallExternalMethodActivityDesigner : ActivityDesigner
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
            Bitmap bmp = Properties.Resources.LWCallExternMethod;
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
            LWCallExternalMethodActivityDesignerTheme compositeDesignerTheme = (LWCallExternalMethodActivityDesignerTheme)e.DesignerTheme;
            ActivityDesignerPaint.DrawRoundedRectangle(graphics, compositeDesignerTheme.BorderPen, this.Bounds, compositeDesignerTheme.BorderWidth);

            string text = this.Text;
            LWCallExternalMethodActivity activity = this.Activity as LWCallExternalMethodActivity;
            if (activity != null)
            {
                text =SR.IsEnglish?"Business Interface":"业务交互接口";
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
    public class LWCallExternalMethodActivityDesignerTheme : ActivityDesignerTheme
    {
        public LWCallExternalMethodActivityDesignerTheme(WorkflowTheme theme)
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


    internal interface IMethodFindProvider
    {
        List<MethodData> GetPropertyValues(ITypeDescriptorContext typeDescriptorContext);
    }
}
