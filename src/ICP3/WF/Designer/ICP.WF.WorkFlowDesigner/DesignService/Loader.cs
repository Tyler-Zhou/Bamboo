

namespace ICP.WF.WorkFlowDesigner
{
    using System;
    using System.Collections;
    using System.ComponentModel.Design;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing.Design;
    using System.IO;
    using System.Text;
    using System.Workflow.ComponentModel;
    using System.Workflow.ComponentModel.Compiler;
    using System.Workflow.ComponentModel.Design;
    using System.Workflow.ComponentModel.Serialization;
    using System.Xml;
    using ICP.WF.Activities;
    using ICP.WF.ServiceInterface;
    using System.Collections.Generic;

    /// <summary>
    /// 提供可用于实现自定义设计器加载程序的基本设计器加载程序
    /// </summary>
    public sealed class WorkflowLoader : WorkflowDesignerLoader
    {
        #region 局部变量

        private string xoml = string.Empty;
        private IWorkFlowConfigService _cofigService;
        private IFormDesignClientService _clientService;
        private IWorkFlowExtendService _extendService;
        private IToolboxService _toolBoxService;

        #endregion

        #region 构造函数

        internal void InitServices(IWorkFlowConfigService cofigService
            , IWorkFlowExtendService extendService
            , IFormDesignClientService clientService
            , IToolboxService toolBoxService)
        {
            _extendService = extendService;
            _cofigService = cofigService;
            _clientService = clientService;
            _toolBoxService = toolBoxService;
        }

        public WorkflowLoader()
        {

        }
        #endregion

        #region WorkflowDesignerLoader重载

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            //添加全部服务到本地宿主中
            IDesignerLoaderHost host = LoaderHost;
            if (host != null)
            {
                this.SetBaseComponentClassName("ICP.WF.Activities.DefaultSequenceActivity");
                TypeProvider typeProvider = new TypeProvider(host);

                //加载程序集引用
                typeProvider.AddAssemblyReference(typeof(System.EventHandler).Assembly.Location);
                typeProvider.AddAssemblyReference(typeof(System.ComponentModel.AttributeCollection).Assembly.Location);
                typeProvider.AddAssemblyReference(typeof(System.Workflow.ComponentModel.CompositeActivity).Assembly.Location);
                typeProvider.AddAssemblyReference(typeof(System.Workflow.Activities.SequentialWorkflowActivity).Assembly.Location);
                typeProvider.AddAssemblyReference(typeof(DefaultSequenceActivity).Assembly.Location);
                typeProvider.AddAssemblyReference(typeof(ICP.WF.ServiceInterface.IBusinessDataExchangeService).Assembly.Location);

                //加载服务到本地宿主中
                if (host.GetService(typeof(IMenuCommandService)) == null)
                {
                    host.AddService(typeof(IMenuCommandService), new WorkflowMenuCommandService(host));
                }
                if (host.GetService(typeof(ITypeProvider)) == null)
                {
                    host.AddService(typeof(ITypeProvider), typeProvider, true);
                }
                if (host.GetService(typeof(IPropertyValueUIService)) == null)
                {
                    host.AddService(typeof(IPropertyValueUIService), new PropertyValueUIService());
                }
                if (host.GetService(typeof(IEventBindingService)) == null)
                {
                    host.AddService(typeof(IEventBindingService), new EventBindingService(host, this));
                }
                if (host.GetService(typeof(IMemberCreationService)) == null)
                {
                    host.AddService(typeof(IMemberCreationService), new MemberCreationService(host, this));
                }
                if (host.GetService(typeof(IExtendedUIService)) == null)
                {
                    host.AddService(typeof(IExtendedUIService), new ExtendedUIService());
                }
                if (_cofigService != null)
                {
                    host.AddService(typeof(IWorkFlowConfigService), _cofigService);
                }
                if (_clientService != null)
                {
                    host.AddService(typeof(IFormDesignClientService), _clientService);
                }
                if (_extendService != null)
                {
                    host.AddService(typeof(ICP.WF.ServiceInterface.IWorkFlowExtendService), _extendService);
                }
                if (_toolBoxService != null)
                {
                    host.AddService(typeof(IToolboxService), _toolBoxService);
                }
            }
        }

        /// <summary>
        /// 释放初始化加载的一些服务
        /// </summary>
        public override void Dispose()
        {
            IDesignerLoaderHost host = LoaderHost;
            if (host != null)
            {
                host.RemoveService(typeof(IIdentifierCreationService));
                host.RemoveService(typeof(IMenuCommandService));
                //host.RemoveService(typeof(IToolboxService));
                host.RemoveService(typeof(ITypeProvider), true);
                host.RemoveService(typeof(IWorkflowCompilerOptionsService));
                host.RemoveService(typeof(IEventBindingService));
                host.RemoveService(typeof(IWorkFlowConfigService));
                host.RemoveService(typeof(IFormDesignClientService));
            }

            base.Dispose();
        }



        protected override void OnEndLoad(bool successful, ICollection errors)
        {
            base.OnEndLoad(successful, errors);

            string layoutFile = Path.Combine(Path.GetDirectoryName(this.xoml), Path.GetFileNameWithoutExtension(this.xoml) + ".layout");
            if (File.Exists(layoutFile))
            {
                IList loaderrors = null;
                using (XmlReader xmlReader = XmlReader.Create(layoutFile))
                {
                    LoadDesignerLayout(xmlReader, out loaderrors);
                }
            }
        }

        public void PerformLoad()
        {
            IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));

            // 加载活动树
            XmlReader reader = new XmlTextReader(this.xoml.Trim());
            Activity rootActivity = null;
            try
            {
                WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                rootActivity = xomlSerializer.Deserialize(reader) as Activity;
            }
            finally
            {
                reader.Close();
            }

            if (rootActivity != null && designerHost != null)
            {
                DestroyObjectGraphFromDesignerHost(designerHost, (Activity)designerHost.RootComponent);
                AddObjectGraphToDesignerHost(designerHost, rootActivity);
                Type companionType = rootActivity.GetValue(WorkflowMarkupSerializer.XClassProperty) as Type;
                if (companionType != null)
                {
                    SetBaseComponentClassName(companionType.FullName);
                }
            }

            designerHost.Activate();
        }
        /// <summary>
        /// 加载流程.
        /// </summary>
        /// <param name="serializationManager"></param>
        protected override void PerformLoad(IDesignerSerializationManager serializationManager)
        {
            base.PerformLoad(serializationManager);
            IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));

            // 加载活动树
            XmlReader reader = new XmlTextReader(this.xoml.Trim());
            Activity rootActivity = null;
            try
            {
                WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                rootActivity = xomlSerializer.Deserialize(reader) as Activity;
            }
            finally
            {
                reader.Close();
            }

            if (rootActivity != null && designerHost != null)
            {
              //  DestroyObjectGraphFromDesignerHost(designerHost, rootActivity);
                AddObjectGraphToDesignerHost(designerHost, rootActivity);
                Type companionType = rootActivity.GetValue(WorkflowMarkupSerializer.XClassProperty) as Type;
                if (companionType != null)
                {
                    SetBaseComponentClassName(companionType.FullName);
                }
            }

            designerHost.Activate();
        }

        public List<string> Validate()
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            DefaultSequenceActivity rootActivity = host.RootComponent as DefaultSequenceActivity;
            List<string> errors = new List<string>();
            if (rootActivity != null)
            {
                rootActivity.Validate(errors);
            }

            return errors;
        }

        public override void Flush()
        {
            PerformFlush(null);
        }

        /// <summary>
        /// 保存Xoml文件,并刷新规则到一个Xml文件.
        /// </summary>
        /// <param name="manager"></param>
        protected override void PerformFlush(IDesignerSerializationManager manager)
        {
            base.Flush();

            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            Activity rootActivity = host.RootComponent as Activity;
            if (host != null && host.RootComponent != null)
            {
                if (rootActivity != null)
                {
                    XmlTextWriter xmlWriter = new XmlTextWriter(this.Xoml, Encoding.UTF8);
                    try
                    {
                        WorkflowMarkupSerializer xomlSerializer = new WorkflowMarkupSerializer();
                        xomlSerializer.Serialize(xmlWriter, rootActivity);
                    }
                    finally
                    {
                        xmlWriter.Close();
                    }
                }
            }


            //刷新规则文件
            ICP.WF.Activities.ICPRuleSet definitions = (ICP.WF.Activities.ICPRuleSet)rootActivity.GetValue(ICP.WF.Activities.DefaultSequenceActivity.ICPRuleDefinitionsProperty );
            if (definitions != null)
            {
                string rulesFile = Path.Combine(Path.GetDirectoryName(this.xoml), Path.GetFileNameWithoutExtension(this.xoml) + ".rules");
                RuleSetSerializer.SerializeToFile<ICPRuleSet>(definitions, rulesFile);
            }
        }

        private XmlWriter CreateXmlWriter(object output)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            settings.OmitXmlDeclaration = true;
            settings.CloseOutput = true;
            if (output is string)
            {
                return XmlWriter.Create(output as string, settings);
            }
            if (output is TextWriter)
            {
                return XmlWriter.Create(output as TextWriter, settings);
            }
            return null;
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// XOML文件
        /// </summary>
        public string Xoml
        {
            get
            {
                return this.xoml;
            }

            set
            {
                this.xoml = value;
            }
        }

        public override TextReader GetFileReader(string filePath)
        {
            return new StreamReader(new FileStream(filePath, FileMode.OpenOrCreate));

        }

        public override TextWriter GetFileWriter(string filePath)
        {
            return new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate));
        }

        public override string FileName
        {
            get
            {
                return xoml;
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 添加对象图在设计器中
        /// </summary>
        /// <param name="designerHost">提供用于管理设计器事务和组件的接口</param>
        /// <param name="activity">活动</param>
        public static void AddObjectGraphToDesignerHost(
            IDesignerHost designerHost, 
            Activity activity)
        {
            Guid Definitions_Class = new Guid("3FA84B23-B15B-4161-8EB8-37A54EFEEFC7");
            if (designerHost == null)
            {
                throw new ArgumentNullException("designerHost");
            }
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            string rootSiteName = activity.QualifiedName;
            if (activity.Parent == null)
            {
                string fullClassName = activity.UserData[Definitions_Class] as string;
                if (fullClassName == null)
                {
                    fullClassName = activity.GetType().FullName;
                }
               
                rootSiteName = (fullClassName.LastIndexOf('.') != -1) ? fullClassName.Substring(fullClassName.LastIndexOf('.') + 1) : fullClassName;
                if (designerHost.RootComponent==null
                    || activity.GetType() != designerHost.RootComponent.GetType())
                {
                    designerHost.Container.Add(activity);
                }
            }
            else
            {
                designerHost.Container.Add(activity, activity.QualifiedName);
            }

            if (activity is CompositeActivity)
            {
                foreach (Activity activity2 in Helpers.GetNestedActivities(activity as CompositeActivity))
                {
                    designerHost.Container.Add(activity2, activity2.QualifiedName);
                }
            }
        }


        /// <summary>
        /// 销毁对象从设计器中
        /// </summary>
        /// <param name="designerHost">提供用于管理设计器事务和组件的接口</param>
        /// <param name="activity">活动</param>
        internal static void DestroyObjectGraphFromDesignerHost(IDesignerHost designerHost, Activity activity)
        {
            if (designerHost == null)
            {
                throw new ArgumentNullException("designerHost");
            }
            if (activity == null)
            {
                throw new ArgumentNullException("activity");
            }

            designerHost.DestroyComponent(activity);
            if (activity is CompositeActivity)
            {
                foreach (Activity activity2 in Helpers.GetNestedActivities(activity as CompositeActivity))
                {
                    designerHost.DestroyComponent(activity2);
                }
            }
        }

        #endregion

    }
}
