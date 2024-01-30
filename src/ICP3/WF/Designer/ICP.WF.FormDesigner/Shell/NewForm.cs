
//-----------------------------------------------------------------------
// <copyright file="NewForm.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.FormDesigner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using ICP.WF.ServiceInterface;
    using ICP.WF.ServiceInterface.DataObject;
    using ICP.WF.Controls;

    /// <summary>
    /// 新建窗体
    /// </summary>
	public partial class NewForm: DevExpress.XtraEditors.XtraForm
    {
        #region 本地变量

        private LocalWorkflowService _localService;

        private string _templateFile;

        #endregion

        #region 初始化


        public NewForm()
		{
			InitializeComponent();

            if (DesignMode == false)
            {
                this.Load += new EventHandler(FormFileSelectEditor_Load);
            }
            this.Disposed += delegate {
                this._localService = null;
                this.listFormFileName.SelectedIndexChanged -= this.listFormFileName_SelectedIndexChanged;
                this.txtSaveAs.Properties.ButtonClick -=this.buttonEdit1_Properties_ButtonClick;
                this.txtName.TextChanged -= this.txtName_TextChanged;
           
            };
        }

        void FormFileSelectEditor_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {

                InitControls();

                InitFormList();
            }
        }

        #endregion

        #region 公共属性


        /// <summary>
        /// 新增表单名
        /// </summary>
        public string FormName
        {
            get { return txtName.Text.Trim(); }
        }

        /// <summary>
        /// 默认保存路径
        /// </summary>
        public string SaveAsPath
        {
            get { return txtSaveAs.Text.Trim(); }
        }

     
        /// <summary>
        /// 模板文件路径
        /// </summary>
        public string TemplateFile
        {
            get { return _templateFile; }
            set { _templateFile = value; }
        }

        /*本地服务*/
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

        /*控件数据初始化*/
        private void InitControls()
        {
            txtName.Text = "Form1";
            UpdateSaveAsFile();
        }

        /*表单数据初始化*/
        private void InitFormList()
        {
            listFormFileName.Items.Clear();

            List<FormTemplateItem> items = LocalService.GetFormTemplateList();
            FormTemplateItem item = new FormTemplateItem();
            item.Name = "Null";
            item.TemplateFile = LocalService.FormTemplateDir+@"\Null.xml";
            items.Insert(0, item);

            this.listFormFileName.DataSource = items;
            this.listFormFileName.ValueMember = "TemplateFile";
            this.listFormFileName.DisplayMember = "Name";


            //默认选择第一个默认表单
           // listFormFileName_ItemSelectionChanged(this, new ListViewItemSelectionChangedEventArgs(listFormFileName.Items[0], 0, true));
        }

        /**/
        private void UpdateSaveAsFile()
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                this.txtSaveAs.Text = System.IO.Path.Combine(LocalService.FormDir, txtName.Text + ".xml");
            }
        }

        /*保存前窗体验证*/
        private bool ValidateData()
        {
            errorTip.Clear();
            bool isSucc = true;

            if (txtName.Text.Trim().Length == 0)
            {
                errorTip.SetError(txtName, Utility.GetString("MustInput", "表单名称必须填写！"));
                isSucc = false;
                txtName.Focus();
            }

            if (this.txtName.Text.Trim().Contains(" "))
            {
                errorTip.SetError(txtName, Utility.GetString("FormNameCannotContainSpaceCharacter", "表单名称不能包含空格等特殊字符．"));
                isSucc = false;
                txtName.Focus();
            }
            if (File.Exists(this.txtSaveAs.Text))
            {
                errorTip.SetError(txtName, Utility.GetString("FileIsexists", "该文件已经存在，请重新命名"));
                isSucc = false;
                txtName.Focus();
            }
            return isSucc;
        }
        

        #endregion

        #region 事件处理

        /*确定*/
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false)
            {
                return;
            }

            FormTemplateItem item =(FormTemplateItem)listFormFileName.SelectedItem;
            if (item==null
                || item.Name=="Null")
            {
                _templateFile = string.Empty;
            }
            else
            {
                _templateFile = listFormFileName.SelectedValue.ToString();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /*取消*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //刷新显示列表
        private void listFormFileName_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item != null && e.IsSelected)
            {
                _templateFile = e.Item.Tag.ToString();
                if (string.IsNullOrEmpty(_templateFile) == false && File.Exists(_templateFile))
                {
                    Control ctrl = (Control)FormBuildService.CreateObjectFromFile(null, _templateFile, new System.Collections.ArrayList());
                    if (ctrl != null)
                    {
                        ctrl.Dock = DockStyle.Fill;
                        splitContainer1.Panel2.Controls.Clear();
                        splitContainer1.Panel2.Controls.Add(ctrl);
                    }
                }
                else
                {
                    splitContainer1.Panel2.Controls.Clear();
                }
            }
        }

        /*浏览*/
        private void buttonEdit1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            this.dlgSaveFile.InitialDirectory = LocalService.FormDir;
            this.dlgSaveFile.DefaultExt = "xml";
            if (this.dlgSaveFile.ShowDialog(this) == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(this.dlgSaveFile.FileName))
                {
                    this.txtSaveAs.Text = this.dlgSaveFile.FileName;
                }
            }
        }

        /*同步另存为对话框*/
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            UpdateSaveAsFile();
        }

        #endregion

        /// <summary>
        /// 选择的模板发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFormFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listFormFileName.SelectedValue != null)
            {
                _templateFile = this.listFormFileName.SelectedValue.ToString();
                if (string.IsNullOrEmpty(_templateFile) == false && File.Exists(_templateFile))
                {
                    Control ctrl = (Control)FormBuildService.CreateObjectFromFile(null, _templateFile, new System.Collections.ArrayList());
                    if (ctrl != null)
                    {
                        ctrl.Dock = DockStyle.Fill;
                        splitContainer1.Panel2.Controls.Clear();
                        splitContainer1.Panel2.Controls.Add(ctrl);
                    }
                }
                else
                {
                    splitContainer1.Panel2.Controls.Clear();
                }
            }
            else
            {
                splitContainer1.Panel2.Controls.Clear();
            }
        }

    }
}
