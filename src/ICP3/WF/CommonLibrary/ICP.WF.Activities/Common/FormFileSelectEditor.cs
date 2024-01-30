using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.WF.Activities
{
    /// <summary>
    /// 表单选择界面
    /// </summary>
    public partial class FormFileSelectEditor : DevExpress.XtraEditors.XtraForm
    {
        #region 本地变量

        private ITypeDescriptorContext _descriptorContext;

        private LocalWorkflowService _localService;

        private string _selectFormFileName;

        #endregion

        #region 初始化


        public FormFileSelectEditor()
		{
			InitializeComponent();

            if (DesignMode == false)
            {
                this.Load += new EventHandler(FormFileSelectEditor_Load);
            }
        }

        public FormFileSelectEditor(ITypeDescriptorContext descriptorContext, string selectFormFileName)
            : this()
        {
            _descriptorContext = descriptorContext;
            _selectFormFileName = selectFormFileName;
        }

        void FormFileSelectEditor_Load(object sender, EventArgs e)
        {
            if (DesignMode == false)
            {
                //ICP.Framework.ClientComponents.IMEControl.SetIme(this);

                InitFormList();
            }
        }

        #endregion

        #region 公共属性


        /// <summary>
        /// 选择的表单文件名
        /// </summary>
        public string SelectedDataSourceName
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
            IWorkFlowConfigService cfgService = (IWorkFlowConfigService)((IServiceProvider)_descriptorContext.Container).GetService(typeof(IWorkFlowConfigService));

            FormProfileList selectFormProfile = null;

            if (cfgService != null)
            {

                List<FormProfileList> items = new List<FormProfileList>();
                byte[] fList = cfgService.GetGetFormProfileListZip(string.Empty, string.Empty, LocalData.IsEnglish);
                items = (List<FormProfileList>)DataZipStream.DecompressionArrayList(fList);
                if (items == null)
                {
                    items = new List<FormProfileList>();
                }


                
                List<ListViewItem> itemList = new List<ListViewItem>();
            
                foreach (FormProfileList i in items)
                {
                    ListViewItem lvItem = new ListViewItem();
                    lvItem.Text = i.ProfileName;
                    lvItem.Tag = i;
                    itemList.Add(lvItem);

                    if (i.KeyWord == _selectFormFileName)
                    {
                        selectFormProfile = i;
                    }
                }

                listFormFileName.ValueMember = "Tag";
                listFormFileName.DisplayMember = "Text";
                listFormFileName.DataSource = itemList;

            }
            ///默认选择上次的表单
            if (selectFormProfile != null)
            {
                listFormFileName.SelectedValue = selectFormProfile;
            }
        }

        #endregion

        #region 事件处理

        /*确定*/
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (listFormFileName.SelectedValue==null)
            {
                XtraMessageBox.Show("Please select a form");
                return;
            }

            FormProfileList ob = this.listFormFileName.SelectedValue as FormProfileList;
            if (ob != null)
            {
                _selectFormFileName = ob.KeyWord;
            }
            else
            {
                _selectFormFileName = string.Empty;
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
        #endregion



        /// <summary>
        /// 选择的表单发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFormFileName_SelectedIndexChanged(object sender, EventArgs e)
        {
            IFormDesignClientService clientService = (IFormDesignClientService)((IServiceProvider)_descriptorContext.Container).GetService(typeof(IFormDesignClientService));
            if (clientService == null)
            {

                return;
            }
            if (this.listFormFileName.SelectedValue != null)
            {
                FormProfileList ob = this.listFormFileName.SelectedValue as FormProfileList;
                if (ob != null)
                {
                    if (string.IsNullOrEmpty(ob.PorfileContent) == false)
                    {
                        Control ctrl = clientService.BuildFormFromXml(ob.PorfileContent);
                        ctrl.Dock = DockStyle.Fill;
                        splitContainer1.Panel2.Controls.Clear();
                        splitContainer1.Panel2.Controls.Add(ctrl);
                    }
                }
            }

        }
        /// <summary>
        /// 双击确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFormFileName_DoubleClick(object sender, EventArgs e)
        {
            if (this.listFormFileName.SelectedValue != null)
            {
                btnOK_Click(null,null);
            }
        }

    }
}
