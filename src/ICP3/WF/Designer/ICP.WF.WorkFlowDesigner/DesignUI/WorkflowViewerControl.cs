
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using ICP.WF.Activities;

namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    /// �����ʱ���������̲鿴�Ľ���
    /// </summary>
    public partial class WorkflowViewerControl : UserControl, IDisposable, IServiceProvider, ISite
    {
        #region ��������

        private WorkflowView            workflowView;
        private DesignSurface           designSurface;
        private WorkflowLoader          loader;
        private string workflowName = "DefaultSequenceActivity";
        private string workflowSaveAs =string.Empty;
        private WorkflowTypes workflowCreationType = WorkflowTypes.DefaultSequenceWorkflow;

        #endregion

        #region ���캯��

        public WorkflowViewerControl()
        {
            InitializeComponent();
            if (this.workItem != null)
            {
                this.workItem.Items.Remove(this);
                this.workItem = null;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Init();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                UnloadWorkflow();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region ���ط���

        private void Init()
        {
           // WorkflowTheme.CurrentTheme.ReadOnly = false;
           // WorkflowTheme.CurrentTheme.AmbientTheme.ShowConfigErrors = false;
           // WorkflowTheme.CurrentTheme.ReadOnly = true;

            //this.propertyGrid.BackColor = BackColor;
            //this.propertyGrid.Font = WorkflowTheme.CurrentTheme.AmbientTheme.Font;
            //this.propertyGrid.Site = this;
            this.workflowSaveAs =Application.StartupPath + "\\" + this.workflowName + ".xoml";


           
        }

        private void UnloadWorkflow()
        {
            IDesignerHost designerHost = this.designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost != null && designerHost.Container.Components.Count > 0)
            {
                WorkflowLoader.DestroyObjectGraphFromDesignerHost(designerHost, designerHost.RootComponent as Activity);
            }
            ISelectionService selectionService = this.designSurface.GetService(typeof(ISelectionService)) as ISelectionService;
            if (selectionService != null)
            {
                selectionService.SelectionChanged -= new EventHandler(OnSelectionChanged);
            }

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

        private void CreateWorkflowFile()
        {
            if (this.workflowCreationType == WorkflowTypes.DefaultSequenceWorkflow)
            {
                Activity workflow = new DefaultSequenceActivity();
                workflow.SetValue(WorkflowMarkupSerializer.XClassProperty, "ICP.WF.Activities.DefaultSequenceActivity");
                workflow.Name = this.workflowName;
                XmlWriter xmlWriter = XmlTextWriter.Create(this.workflowSaveAs);
                WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();

                serializer.Serialize(xmlWriter, workflow);
                xmlWriter.Close();
            }


        }

        #endregion

        #region ��������

        [Microsoft.Practices.ObjectBuilder.Dependency]
        public Microsoft.Practices.CompositeUI.WorkItem workItem { get; set; }

        /// <summary>
        /// �����Ƿ�ֻ�鿴·��ͼ����
        /// </summary>
        public bool OnlyWorkFlowView
        {
            get;
            set;
        }

        /// <summary>
        /// ��ʾ����ͼģ��·��
        /// </summary>
        public string TemplateXomlFile
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
        /// ��������
        /// </summary>
        public string WorkflowName
        {
            get { return workflowName; }
            set { workflowName = value;  }
        }

        /// <summary>
        /// ���̱���·��
        /// </summary>
        public string WorkflowSaveAs
        {
            get { return workflowSaveAs; }
            set { workflowSaveAs = value; TemplateXomlFile = value; }
        }
        
        /// <summary>
        /// ��������
        /// </summary>
        public WorkflowTypes WorkflowCreationType
        {
            get { return this.workflowCreationType; }
            set { this.workflowCreationType = value; }
        }

        /// <summary>
        /// ������ƿؼ� 
        /// </summary>
        public WorkflowView WorkflowView
        {
            get { return this.workflowView; }
        }
        #endregion


        #region �ⲿ�ӿ�
        /// <summary>
        /// ��ʾĬ������
        /// </summary>
        public void ShowDefaultWorkflow()
        {
            DefaultSequenceActivity workflow = new DefaultSequenceActivity();
            workflow.Name = workflowName;
            // workflow.SetValue(WorkflowMarkupSerializer.XClassProperty, "foo.Workflow1");
            string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\Temp";

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string tempFile = path + @"\DefaultSequence.xoml";
            XmlWriter xmlWriter = XmlTextWriter.Create(tempFile);
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            serializer.Serialize(xmlWriter, workflow);
            xmlWriter.Close();
            LoadWorkflow(tempFile);
        }

        /// <summary>
        /// ����ָ���ļ�����
        /// </summary>
        /// <param name="xoml"></param>
        public void LoadWorkflow(string xoml)
        {
            SuspendLayout();

            if (!File.Exists(xoml))
            {
                CreateWorkflowFile();
            }

            //UnloadWorkflow();

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

            //if (loader == null)
            //{
            //    loader = workItem.Services.AddNew<WorkflowLoader>(); // new WorkflowLoader(null, null, null, (System.Drawing.Design.IToolboxService)hostSurfaceManager.GetService(typeof(System.Drawing.Design.IToolboxService)));
            //}
         
          
           
           

            IDesignerHost designerHost = designSurface.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (designerHost != null && designerHost.RootComponent != null)
            {
                IRootDesigner rootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as IRootDesigner;
                if (rootDesigner != null)
                {
                    //

                    //this.loader = loader;
                    this.workflowView = rootDesigner.GetView(ViewTechnology.Default) as WorkflowView;
                    this.panel1.Controls.Add(this.workflowView);
                    this.workflowView.Dock = DockStyle.Fill;
                    this.workflowView.TabIndex = 1;
                    this.workflowView.TabStop = true;
                    this.workflowView.HScrollBar.TabStop = false;
                    this.workflowView.VScrollBar.TabStop = false;
                    this.workflowView.Focus();

                    ISelectionService selectionService = designSurface.GetService(typeof(ISelectionService)) as ISelectionService;
                    if (selectionService != null)
                    {
                        selectionService.SelectionChanged += new EventHandler(OnSelectionChanged);
                    }
                }
            }

            ResumeLayout(true);
        }

        #endregion

        #region �¼�����

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            ISelectionService selectionService =this.designSurface.GetService(typeof(ISelectionService)) as ISelectionService;
            if (selectionService != null)
            {
                //this.propertyGrid.SelectedObjects = new ArrayList(selectionService.GetSelectedComponents()).ToArray();
            }
        }
        #endregion


        #region ISite �ӿ�ʵ��

        public IComponent Component
        {
            get { return this; }
        }

        public new bool DesignMode
        {
            get { return true; }
        }

        #endregion

        #region IServiceProvider�ӿ�ʵ��

        new public object GetService(Type serviceType)
        {
            if (workItem != null)
            {
                ICPFlwoHostSurfaceManager hostSurfaceManager = (ICPFlwoHostSurfaceManager)workItem.Services.Get(typeof(ICPFlwoHostSurfaceManager));
                return hostSurfaceManager.GetService(serviceType);
            }
            else
            {
                return null;
            }
            //return (this.workflowView != null) ? ((IServiceProvider)this.workflowView).GetService(serviceType) : null;
            //if (designSurface != null)
            //{
            //    return designSurface.GetService(serviceType);
            //}
            //else
            //{
            //    return null;
            //}
        }
        #endregion

    }
}
