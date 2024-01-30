using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.WF.ServiceInterface.Client;


namespace ICP.WF.WorkFlowDesigner
{

    /// <summary>
    /// 工作流选择界面
    /// </summary>
    public partial class NewWorkflow : DevExpress.XtraEditors.XtraForm
    {
        #region 本地属性

        LocalWorkflowService _localService;
        public LocalWorkflowService LocalService
        {
            get
            {
                if (_localService == null) _localService = new LocalWorkflowService();

                return _localService;
            }
        }
        private WorkflowViewerControl viewHost = null;

        #endregion

        #region 初始化

        public NewWorkflow()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this._localService = null;
                this.errorTip.DataSource = null;
                this.txtName.TextChanged -= this.txtName_TextChanged;
                this.listWorkFlowTemplateView.SelectedIndexChanged -= this.listWorkFlowTemplateView_SelectedIndexChanged;
                this.txtSaveAs.Properties.ButtonClick -=this.buttonEdit1_Properties_ButtonClick;
                if (this.viewHost != null)
                {
                    this.viewHost.Dispose();
                    this.viewHost = null;
                }
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
            
        }

        private void NewWorkflow_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {

                InitControls();

                InitializeWorkflowView();

                InitWorkFlowTemplateList();

                UpdateSaveAsFile();
            }
        }

        #endregion

        #region 公共属性

        [Microsoft.Practices.ObjectBuilder.Dependency]
        public Microsoft.Practices.CompositeUI.WorkItem WorkItem { get; set; }

        private string _workflowName;
        /// <summary>
        /// 工作流名称
        /// </summary>
        public string WorkflowName
        {
            get
            {
                return _workflowName;
            }
            set
            {
                _workflowName = value;
            }
        }

        private string _workflowSaveAs;
        /// <summary>
        /// 工作流保存路径
        /// </summary>
        public string WorkflowSaveAs
        {
            get
            {
                return _workflowSaveAs;
            }
            set
            {
                _workflowSaveAs = value;
            }
        }

        private WorkflowTypes _workflowType = WorkflowTypes.DefaultSequenceWorkflow;
        /// <summary>
        /// 工作流类型
        /// </summary>
        public WorkflowTypes WorkflowCreationType
        {
            get
            {
                return _workflowType;
            }
            set
            {
                _workflowType = value;
            }
        }

        string _xoml;
        /// <summary>
        /// 如果要从流程模板中创建流程xoml就为模板流程路径，否则为null
        /// </summary>
        public string TemplateXoml
        {
            get { return _xoml; }
            set { _xoml = value; }
        }
        #endregion


        #region 事件处理

        /// <summary>
        /// 确定 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                return;
            }

            this._workflowName = txtName.Text;
            this._workflowSaveAs = txtSaveAs.Text;
            this._workflowType = WorkflowTypes.DefaultSequenceWorkflow;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 文件名发生改变时，更新路径中的文件名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            UpdateSaveAsFile();
        }

        /// <summary>
        /// 浏览文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            this.dlgSaveFile.InitialDirectory = LocalService.WorkFlowDir;
            this.dlgSaveFile.DefaultExt = "xoml";
            if (this.dlgSaveFile.ShowDialog(this) == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(this.dlgSaveFile.FileName))
                {
                    this.txtSaveAs.Text = this.dlgSaveFile.FileName;
                }
            }
        }

        #endregion


        #region 本地方法

        private bool ValidateData()
        {
            errorTip.Clear();
            bool isSucc = true;

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                errorTip.SetError(txtName, Utility.GetString("MustInput", "Workflow Name must input！"));
                isSucc = false;
                txtName.Focus();
            }

            if (this.txtName.Text.Trim().Contains(" "))
            {
                errorTip.SetError(txtName, Utility.GetString("NameCannotContainSpaceCharacter", "Workflow Name cannot contain a space character!"));
                isSucc = false;
                txtName.Focus();
            }
            if (File.Exists(this.txtSaveAs.Text))
            {
                errorTip.SetError(txtName, Utility.GetString("FileIsexists", "File Already Exists!!!"));
                isSucc = false;
                txtName.Focus();
            }



            return isSucc;
        }
        /// <summary>
        /// 更新文件列表
        /// </summary>        
        private void UpdateSaveAsFile()
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                this.txtSaveAs.Text = System.IO.Path.Combine(LocalService.WorkFlowDir, txtName.Text + ".xoml");
            }
        }
        /// <summary>
        /// 初始化模板列表
        /// </summary>
        private void InitWorkFlowTemplateList()
        {
            List<WorkFlowTemplateItem> itemList = LocalService.GetWorkFlowTemplateList();

            WorkFlowTemplateItem item = new WorkFlowTemplateItem();
            item.Id = Guid.NewGuid();
            item.Name = "Null";
            item.TemplateFile = string.Empty;
            item.Version = "1.0";

            itemList.Insert(0, item);

            this.listWorkFlowTemplateView.DisplayMember = "Name";
            this.listWorkFlowTemplateView.ValueMember = "TemplateFile";
            this.listWorkFlowTemplateView.DataSource = itemList;

            //默认选择第一个流程模版
            //this.listWorkFlowTemplateView.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            txtName.Text = "workflow1";
            txtSaveAs.Text = DefaultConfigMananger.Default.FolwDesignFolder.Path + @"\" + txtName.Text + ".xoml";
        }

        /// <summary>
        /// 初始化流程列表图
        /// </summary>
        private void InitializeWorkflowView()
        {
            this.viewHost = WorkItem.Items.AddNew<WorkflowViewerControl>();
            this.viewHost.Dock = DockStyle.Fill;
            this.viewHost.OnlyWorkFlowView = true;
            this.viewHostPanel.Controls.Add(viewHost);
        }

        public void RefreshFlowChart(string fileName)
        {
            if (fileName != null && viewHost != null)
                this.viewHost.LoadWorkflow(fileName);
        }

        #endregion

        /// <summary>
        /// 选择的流程图发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listWorkFlowTemplateView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //刷新显示列表
            if (this.viewHost == null)
            {
                InitializeWorkflowView();
            }

            if (this.listWorkFlowTemplateView.SelectedValue != null)
            {
                string templateFile = this.listWorkFlowTemplateView.SelectedValue.ToString();
                string name = this.listWorkFlowTemplateView.Text.ToString();
                if (!string.IsNullOrEmpty(templateFile))
                {
                    string path = DefaultConfigMananger.Default.FlowTemplateFolder.Path;

                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }

                    string tempFile = path + @"\" + name;
                    if (!File.Exists(tempFile))
                    {
                        using (StreamWriter tempWriter = new StreamWriter(tempFile))
                        {
                            tempWriter.Write(templateFile);
                        }
                    }

                    //刷新流程图
                    RefreshFlowChart(tempFile);

                    _xoml = tempFile;

                    return;
                }
            }

            if (viewHost != null)
            {
                _xoml = string.Empty;
                viewHost.ShowDefaultWorkflow();
            }
        }

        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.dlgSaveFile.InitialDirectory = DefaultConfigMananger.Default.FolwDesignFolder.Path;
            this.dlgSaveFile.DefaultExt = "xoml";
            if (this.dlgSaveFile.ShowDialog(this) == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(this.dlgSaveFile.FileName))
                {
                    this.txtSaveAs.Text = this.dlgSaveFile.FileName;
                }
            }

        }

   
    }



}
