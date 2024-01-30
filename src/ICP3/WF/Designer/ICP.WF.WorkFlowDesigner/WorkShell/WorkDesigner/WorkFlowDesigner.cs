using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Workflow.ComponentModel.Design;
using System.ComponentModel.Design;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.WF.Activities;
using System.IO;
using System.Xml;
using System.Workflow.ComponentModel.Serialization;
using ICP.WF.ServiceInterface.Client;
using System.Collections;
using System.Workflow.ComponentModel;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.WorkFlowDesigner
{
    public partial class WorkFlowDesigner : XtraUserControl, IWorkFlowDesigner, IServiceProvider, ISite
    {
        public WorkFlowDesigner()
        {
            InitializeComponent();

            Init();
            this.Disposed += delegate {
                this.SelectionChanged = null;
                this.ActiveDesignerChanged = null;
                this.OnException = null;
                if (this.workItem != null)
                {
                    this.workItem.Items.Remove(this);
                    this.workItem = null;
                }
            
            };
        }

        #region 本地变量

        private WorkflowView workflowView;
        private DesignSurface designSurface;
        private WorkflowLoader loader;
        private string workflowName = "workflow1";
        private string workflowSaveAs = string.Empty;
        private WorkflowTypes workflowCreationType = WorkflowTypes.DefaultSequenceWorkflow;

        #endregion


        #region 公共属性

        [Microsoft.Practices.ObjectBuilder.Dependency]
        public Microsoft.Practices.CompositeUI.WorkItem workItem { get; set; }
        /// <summary>
        /// 选择的活动控件发生改变
        /// </summary>
        public event SelectedChangedEventHandler SelectionChanged;

        public event ActiveDesignerChangedEventHandler ActiveDesignerChanged; 
        /// <summary>
        /// 异常
        /// </summary>
        public event ExceptionEventHandler OnException;

        /// <summary>
        /// 流程名称
        /// </summary>
        public string WorkflowName
        {
            get { return workflowName; }
            set { workflowName = value; }
        }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string WorkflowSaveAs
        {
            get { return workflowSaveAs; }
            set
            {
                workflowSaveAs = value;
            }
        }

        /// <summary>
        /// 流程类型
        /// </summary>
        public WorkflowTypes WorkflowCreationType
        {
            get { return workflowCreationType; }
            set { workflowCreationType = value; }
        }

        /// <summary>
        /// 模板流程路径
        /// </summary>
        public string TemplateXoml
        {
            get
            {
                string xoml = string.Empty;
                if (this.loader != null)
                {
                    try
                    {
                        this.loader.Flush();
                        xoml = this.loader.Xoml;
                    }
                    catch
                    {
                    }
                }
                return xoml;
            }
            set
            {
                try
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        LoadWorkflow(value);
                    }
                }
                catch
                {
                }
            }
        }


        /// <summary>
        /// 流程设计控件 
        /// </summary>
        public WorkflowView WorkFlowView
        {
            get { return this.workflowView; }
        }

        #endregion

        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        private IWorkFlowConfigService _configService;
        private IFormDesignClientService _clientService;
        private IWorkFlowExtendService _extenService;

        #endregion


        #region 初始事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
           
        }
       
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {

           // WorkflowTheme.CurrentTheme.ReadOnly = false;
           // WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
            //WorkflowTheme.CurrentTheme.ReadOnly = true;


            
        }

        /// <summary>
        /// 销毁资源
        /// </summary>
        private void UnloadWorkflow()
        {
            IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost != null && designerHost.Container.Components.Count > 0)
            {
                WorkflowLoader.DestroyObjectGraphFromDesignerHost(designerHost, designerHost.RootComponent as Activity);
            }

            //ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
            //if (selectionService != null)
            //{

            //    selectionService.SelectionChanged -= new EventHandler(SelectionChanged);
            //}
            if (this.designSurface != null)
            {
                this.designSurface.Dispose();
                this.designSurface = null;
            }
            if (this.workflowView != null)
            {
                Controls.Remove(this.workflowView);
                this.workflowView.Dispose();
                this.workflowView = null;
            }
        }
        #endregion

        #region 重写事件

        /// <summary>
        /// 开始布局
        /// </summary>
        public void StartLayout()
        {
            this.SuspendLayout();
        }

        /// <summary>
        /// 结束布局
        /// </summary>
        public void EndLayout()
        {
            this.ResumeLayout(true);
        }

        /// <summary>
        /// 获得Size 
        /// </summary>
        public Size GetSize
        {
            get
            {
                return this.ClientSize;
            }
        }

        /// <summary>
        /// 刷新布局
        /// </summary>
        public void ReLayout()
        {
            this.PerformLayout();
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="commandname"></param>
        public void ExecuteAction(CommandID commandname)
        {
            if (commandname == null) return;

            IMenuCommandService menuService = designSurface.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
            try
            {
                menuService.GlobalInvoke(commandname);
            }
            catch (Exception ex)
            {
                OnException(this, new ExceptionEventArgs(ex));
            }
        }
        /// <summary>
        /// 加载指定工作流
        /// </summary>
        /// <param name="xoml"></param>
        public void LoadWorkflow(string xoml)
        {
            SuspendLayout();

            IDesignerHost designerHost = null;
            if (designSurface == null)
            {
                ICPFlwoHostSurfaceManager hostSurfaceManager = (ICPFlwoHostSurfaceManager)workItem.Services.Get(typeof(ICPFlwoHostSurfaceManager));//new LWFormDesignSurfaceManager();
                designSurface = hostSurfaceManager.CreateDesignSurface(this);
                loader = new WorkflowLoader();
                loader.InitServices(null, null, null, (System.Drawing.Design.IToolboxService)designSurface.GetService(typeof(System.Drawing.Design.IToolboxService)));
                loader.Xoml = xoml;
                designSurface.BeginLoad(loader);
            }
            else
            {
                loader.Xoml = xoml;
                loader.PerformLoad();
            }

            loader.Xoml = workflowSaveAs;
            designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost != null && designerHost.RootComponent != null)
            {
                IRootDesigner rootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as IRootDesigner;
                if (rootDesigner != null)
                {
                    this.workflowView = rootDesigner.GetView(ViewTechnology.Default) as WorkflowView;
                    this.workflowView.AllowDrop = true;
                    this.workflowView.Dock = DockStyle.Fill;
                    this.workflowView.TabIndex = 1;
                    this.workflowView.TabStop = true;
                    this.workflowView.HScrollBar.TabStop = false;
                    this.workflowView.VScrollBar.TabStop = false;
                    this.workflowView.Focus();
                    this.Controls.Add(this.workflowView);

                    ISelectionService selectionService = (ISelectionService)designSurface.GetService(typeof(ISelectionService));
                    if (selectionService != null)
                    {
                         selectionService.SelectionChanged += delegate(object sender, EventArgs arg)
                        {
                            ICollection selectedComponents = selectionService.GetSelectedComponents();
                            if (this.SelectionChanged != null)
                            {
                                System.Workflow.ComponentModel.Activity[] comps = new System.Workflow.ComponentModel.Activity[selectedComponents.Count];
                                int i = 0;
                                foreach (System.Workflow.ComponentModel.Activity o in selectedComponents)
                                {
                                    comps[i] = o;
                                    i++;
                                }

                                if (i > 0)
                                {
                                    SelectedEventArgs args = new SelectedEventArgs(comps[i - 1]);
                                    this.SelectionChanged(sender, args);
                                }
                            }
                        };
                    }

                    if (ActiveDesignerChanged != null)
                    {
                        ActiveDesignerChanged(this, new ActiveDesignerChangedEventArgs(designerHost));
                    }
                }
            }

            ResumeLayout(true);
        }


        /// <summary>
        /// 显示默认工作流程
        /// </summary>
        public void ShowDefaultWorkflow()
        {
            DefaultSequenceActivity workflow = new DefaultSequenceActivity();
            workflow.Name = workflowName;
            string path = DefaultConfigMananger.Default.FlowTemplateFolder.Path;// Path.GetDirectoryName(Application.ExecutablePath) + @"\Temp";

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string tempFile = path + @"\DefaultSequence.xoml";
            if (!File.Exists(tempFile))
            {
                using (XmlWriter xmlWriter = XmlTextWriter.Create(tempFile))
                {
                    WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
                    serializer.Serialize(xmlWriter, workflow);
                }
            }

            LoadWorkflow(tempFile);


        }
        /// <summary>
        /// 保存流程到指定文件
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath)
        {
            if (this.loader != null)
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    this.loader.Xoml = filePath;
                    this.workflowSaveAs = filePath;
                }
                else
                {
                    this.loader.Xoml = WorkflowSaveAs;
                    
                }

                this.loader.Flush();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully :" + workflowSaveAs : "保存成功:" + workflowSaveAs);
            }
        }
        /// <summary>
        /// 获得错误提示
        /// </summary>
        /// <returns></returns>
        public List<string> GetErrorList()
        {
            List<string> errorList = new List<string>();
            if (this.loader != null)
            {
                errorList = loader.Validate();
            }
            return errorList;
        }

        #endregion


        #region 外部接口

        public void InitServies(IWorkFlowConfigService configService
          , IWorkFlowExtendService extenService
          , IFormDesignClientService clientService)
        {
            _extenService = extenService;
            _configService = configService;
            _clientService = clientService;
        }


        /// <summary>
        /// 操作命令
        /// </summary>
        /// <param name="cmd"></param>
        public void InvokeStandardCommand(CommandID cmd)
        {
            IMenuCommandService menuService = designSurface.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
            if (menuService != null)
            {
                menuService.GlobalInvoke(cmd);
            }
        }
        #endregion

        #region ISite 接口实现

        public IComponent Component
        {
            get { return this; }
        }

        public new bool DesignMode
        {
            get { return true; }
        }

        #endregion

        #region IServiceProvider接口实现

        public new object GetService(Type serviceType)
        {
            //if (designSurface != null)
            //{
            //    return designSurface.GetService(serviceType);
            //}
            //else
            //{
            //    return null;
            //}
            if (workItem != null)
            {
                ICPFlwoHostSurfaceManager hostSurfaceManager = (ICPFlwoHostSurfaceManager)workItem.Services.Get(typeof(ICPFlwoHostSurfaceManager));
                return hostSurfaceManager.GetService(serviceType);
            }
            else
            {
                return null;
            }
        }

        #endregion


    }

}
