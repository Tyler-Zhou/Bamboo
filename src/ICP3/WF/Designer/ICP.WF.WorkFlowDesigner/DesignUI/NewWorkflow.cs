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
    /// ������ѡ�����
    /// </summary>
    public partial class NewWorkflow : DevExpress.XtraEditors.XtraForm
    {
        #region ��������

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

        #region ��ʼ��

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

        #region ��������

        [Microsoft.Practices.ObjectBuilder.Dependency]
        public Microsoft.Practices.CompositeUI.WorkItem WorkItem { get; set; }

        private string _workflowName;
        /// <summary>
        /// ����������
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
        /// ����������·��
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
        /// ����������
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
        /// ���Ҫ������ģ���д�������xoml��Ϊģ������·��������Ϊnull
        /// </summary>
        public string TemplateXoml
        {
            get { return _xoml; }
            set { _xoml = value; }
        }
        #endregion


        #region �¼�����

        /// <summary>
        /// ȷ�� 
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
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// �ļ��������ı�ʱ������·���е��ļ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            UpdateSaveAsFile();
        }

        /// <summary>
        /// ����ļ�
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


        #region ���ط���

        private bool ValidateData()
        {
            errorTip.Clear();
            bool isSucc = true;

            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                errorTip.SetError(txtName, Utility.GetString("MustInput", "Workflow Name must input��"));
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
        /// �����ļ��б�
        /// </summary>        
        private void UpdateSaveAsFile()
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                this.txtSaveAs.Text = System.IO.Path.Combine(LocalService.WorkFlowDir, txtName.Text + ".xoml");
            }
        }
        /// <summary>
        /// ��ʼ��ģ���б�
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

            //Ĭ��ѡ���һ������ģ��
            //this.listWorkFlowTemplateView.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControls()
        {
            txtName.Text = "workflow1";
            txtSaveAs.Text = DefaultConfigMananger.Default.FolwDesignFolder.Path + @"\" + txtName.Text + ".xoml";
        }

        /// <summary>
        /// ��ʼ�������б�ͼ
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
        /// ѡ�������ͼ�����ı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listWorkFlowTemplateView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ˢ����ʾ�б�
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

                    //ˢ������ͼ
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
