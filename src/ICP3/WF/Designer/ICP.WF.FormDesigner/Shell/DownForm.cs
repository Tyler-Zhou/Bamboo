//-----------------------------------------------------------------------
// <copyright file="FileDownForm.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.WF.FormDesigner
{

    /// <summary>
    /// 表单下载界面
    /// </summary>
    public partial class FileDownForm : DevExpress.XtraEditors.XtraForm
    {

        #region 服务

        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

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

        #endregion

        #region 本地变量

        private LocalWorkflowService _localService;

        private string _selectFormFileName;

        #endregion

        #region 初始化



        public FileDownForm()
        {
            InitializeComponent();

            this.Disposed += delegate {
                this._localService = null;
                this.listFormFileName.SelectedIndexChanged -= this.listFormFileName_SelectedIndexChanged;
                if (WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            };

        }

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode )
            {

                InitFormList();
            }
        }

        #endregion

        #region 公共属性



        /// <summary>
        /// 选择的表单文件名
        /// </summary>
        public string FormDownFileName
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


        #endregion

        #region 本地方法

        /*初始化可选表单列表*/
        private void InitFormList()
        {
            IList<FilesInfo> filesList = new List<FilesInfo>();

            DateTime dt1 = DateTime.Now;



            List<FormProfileList> items = new List<FormProfileList>();
            byte[] zipBytes = WorkFlowConfigService.GetGetFormProfileListZip(string.Empty, string.Empty, LocalData.IsEnglish);

            items = (List<FormProfileList>)DataZipStream.DecompressionArrayList(zipBytes);

            if (items == null)
            {
                return;
            }

            foreach (FormProfileList i in items)
            {
                FilesInfo filesInfo = new FilesInfo();
                filesInfo.Tag = i;
                filesInfo.Text = i.ProfileName;

                filesList.Add(filesInfo);
            }
           
            this.listFormFileName.DataSource = filesList;
            this.listFormFileName.ValueMember = "Tag";
            this.listFormFileName.DisplayMember = "Text";

        }

        #endregion

        #region 事件处理

        /*确定*/
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listFormFileName.SelectedItems.Count == 0)
            {
                XtraMessageBox.Show("Please select a form");
                return;
            }

            FormProfileList ob = this.listFormFileName.SelectedValue as FormProfileList;
            if (ob != null)
            {
                if (string.IsNullOrEmpty(ob.PorfileContent) == false)
                {
                    _selectFormFileName = SaveAs(ob.PorfileContent, ob.KeyWord);
                }
            }
            else
            {
                _selectFormFileName = string.Empty;
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
        /// 选择的列表项发生改变时，刷新窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFormFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listFormFileName.SelectedValue != null)
            {
                FormProfileList ob = this.listFormFileName.SelectedValue as FormProfileList;
                if (ob != null)
                {
                    if (!string.IsNullOrEmpty(ob.PorfileContent))
                    {
                        Control ctrl = FormDesignClientService.BuildFormFromXml(ob.PorfileContent);
                        ctrl.Dock = DockStyle.Fill;
                        splitContainer1.Panel2.Controls.Clear();
                        splitContainer1.Panel2.Controls.Add(ctrl);
                    }
                }
            }
        }

        /// <summary>
        /// 保存表单是否给出提示
        /// </summary>
        /// <param name="forceFilePrompt"></param>
        public string SaveAs(string content,string defaultFileName)
        {
            try
            {
                string fileName = string.Empty;
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.InitialDirectory = ICP.WF.ServiceInterface.Client.DefaultConfigMananger.Default.FormDesignFolder.Path;
                dlg.DefaultExt = "xml";
                dlg.Filter = "xml files|*.xml";
                dlg.FileName = defaultFileName;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    fileName = dlg.FileName;

                    StreamWriter file = new StreamWriter(fileName);
                    file.Write(content);
                    file.Close();
                }

                return fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
    /// <summary>
    /// 文件信息
    /// </summary>
    public class FilesInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FilesName
        {
            get;
            set;
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilesPath
        {
            get;
            set;
        }
        /// <summary>
        /// Tag
        /// </summary>
        public object Tag
        {
            get;
            set;
        }
        public string Text
        {
            get;
            set;
        }
    }

}
