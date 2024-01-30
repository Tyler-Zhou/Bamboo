using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    /// 表单选择界面
    /// </summary>
    public partial class WorkFlowFileDown : DevExpress.XtraEditors.XtraForm
    {

        #region 服务
        /// <summary>
        /// 工作流管理服务
        /// </summary>
        public IWorkFlowConfigService WorkFlowConfigService
        {
            get 
            {
                return ServiceClient.GetService<IWorkFlowConfigService>();
            }
        }

        public IFormDesignClientService FormDesignClientService 
        {
            get 
            {
                return ServiceClient.GetClientService<IFormDesignClientService>();
            }
        }
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        #endregion

        #region 本地变量

        private LocalWorkflowService _localService;

        private string _selectFormFileName;

        #endregion


        #region 初始化




        public WorkFlowFileDown()
        {
            InitializeComponent();

            if (DesignMode == false)
            {
                this.Load += new EventHandler(FormFileSelectEditor_Load);
            }
            this.Disposed += new EventHandler(WorkFlowFileDown_Disposed);
        }

        void WorkFlowFileDown_Disposed(object sender, EventArgs e)
        {
            this.listFormFileName.DoubleClick -= this.listFormFileName_DoubleClick;
            this.listFormFileName.SelectedIndexChanged -= this.listFormFileName_SelectedIndexChanged;
            this._localService = null;

            if (this.WorkItem != null)
            {
                this.WorkItem.Items.Remove(this);
                this.WorkItem = null;
            }
        }

     

        void FormFileSelectEditor_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                InitWorkFlowList();
            }
        }

        #endregion

        #region 公共属性




        /// <summary>
        /// 选择的表单文件名
        /// </summary>
        public string DownWorkFlowFileName
        {
            get
            {
                return _selectFormFileName;
            }
        }


        /// <summary>
        /// 本地服务
        /// </summary>
        public LocalWorkflowService LocalService
        {
            get
            {
                if (_localService == null) _localService = new LocalWorkflowService();

                return _localService;
            }
        }
        /// <summary>
        /// 配置信息
        /// </summary>
        public WorkFlowConfigInfo WFInfo
        {
            get;
            set;
        }

        #endregion

        #region 本地方法

        /*初始化可选表单列表*/
        private void InitWorkFlowList()
        {

            List<WorkFlowConfigInfo> items = new List<WorkFlowConfigInfo>();
            byte[] wfList = WorkFlowConfigService.GetWorkFlowConfigListZip(null, LocalData.IsEnglish);
            items = (List<WorkFlowConfigInfo>)DataZipStream.DecompressionArrayList(wfList);
            if (items == null)
            {
                items = new List<WorkFlowConfigInfo>();
            }

            List<ListViewItem> itemList = new List<ListViewItem>();

            foreach (WorkFlowConfigInfo i in items)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = LocalData.IsEnglish ? i.EDescription : i.CDescription;
                lvItem.Tag = i;

                itemList.Add(lvItem);
            }

            this.listFormFileName.ValueMember = "Tag";
            this.listFormFileName.DisplayMember = "Text";
            this.listFormFileName.DataSource = itemList;
          
        }

        #endregion

        #region 事件处理

        /*确定*/
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listFormFileName.SelectedValue==null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Please select a form");
                return;
            }


            WorkFlowConfigInfo ob = this.listFormFileName.SelectedValue as WorkFlowConfigInfo;
            if (ob != null)
            {
                _selectFormFileName = SaveAsXoml(ob);
                WFInfo = ob;
            }
            else
            {
                _selectFormFileName = string.Empty;
                WFInfo = new WorkFlowConfigInfo();
            }
            if (!string.IsNullOrEmpty(_selectFormFileName))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /*取消*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 保存表单是否给出提示
        /// </summary>
        /// <param name="forceFilePrompt"></param>
        public string SaveAsXoml(WorkFlowConfigInfo item)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = new ICP.WF.ServiceInterface.LocalWorkflowService().WorkFlowDir;
                dlg.AddExtension = false;
                dlg.DefaultExt = "xoml";
                dlg.Filter = "Xoml files|*.xoml";
                dlg.FileName = item.KeyWord;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter xomlfile = new StreamWriter(dlg.FileName);
                    xomlfile.Write(item.WorkFlowFileContent);
                    xomlfile.Close();

                    StreamWriter rulefile = new StreamWriter(dlg.FileName.Replace(".xoml", ".rules") );
                    rulefile.Write(item.RuleFileContent);
                    rulefile.Close();

                    return dlg.FileName;
                }
                else
                {
                    return string.Empty;
                }
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 选择的项发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFormFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listFormFileName.SelectedValue != null)
            {
                WorkFlowConfigInfo item = this.listFormFileName.SelectedValue as WorkFlowConfigInfo;
                if (item != null)
                {
                    if (!string.IsNullOrEmpty(item.WorkFlowFileContent))
                    {
                        viewWorkFlow.ShowWorkFlowChart(item.WorkFlowFileContent);
                        return;
                    }
                }
            }

            viewWorkFlow.ClearWorkFlowChart();
        }

        /// <summary>
        /// 双击确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFormFileName_DoubleClick(object sender, EventArgs e)
        {
            if (listFormFileName.SelectedValue!=null)
            {
                btnOK_Click(null, null);
            }
        }


    }
}
