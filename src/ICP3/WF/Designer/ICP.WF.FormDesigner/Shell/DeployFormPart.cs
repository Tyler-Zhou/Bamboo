using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.WF.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.WF.FormDesigner
{
    [ToolboxItem(false)]
    public partial class DeployFormPart : BasePart
    {
        public DeployFormPart()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.Disposed += new EventHandler(DeployForm_Disposed);
            }
        }

        #region 本地变量　

        string _formKey;
        string _formFileName;
        string _dataSchemaName;

        #endregion

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


        /// <summary>
        /// 工作流扩展服务
        /// </summary>
        public IWorkFlowExtendService WorkFlowExtendService
        {
            get 
            {
                return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }

        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        #endregion

        #region 构造函数


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode == false)
            {
                this.cmbKeys.EditValueChanged += new System.EventHandler(this.cmbName_SelectedIndexChanged);
                InitMessage();
                InitControls();

            }
        }
        /// <summary>
        /// 注册消息
        /// </summary>
        private void InitMessage()
        {
            this.RegisterMessage("KeyWordNoIsNull", "请输入关键字");
            this.RegisterMessage("CNameIsNull", "请输入中文名称");
            this.RegisterMessage("ENameIsNull", "请输入英文名称");
            this.RegisterMessage("VersionIsNull", "请输入版本号");
        }
        void DeployForm_Disposed(object sender, EventArgs e)
        {
            this.cmbKeys.Leave -= this.cmbKeys_Leave;
            this.cmbKeys.ProcessNewValue -= this.cmbKeys_ProcessNewValue;
            this.cmbKeys.EditValueChanged -= this.cmbName_SelectedIndexChanged;
            if (WorkItem != null)
            {
                this.WorkItem.Items.Remove(this);
                this.WorkItem = null;
            }
        }

        #endregion

        #region 事件处理

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidateData() == false)
            {
                return;
            }

            try
            {
                SaveProfile();

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish?"Save Success":"保存成功");

                this.FindForm().DialogResult = DialogResult.OK;
                this.FindForm().Close();
            }
            catch (Exception ex)
            {
                  LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(),ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.Cancel;
            this.FindForm().Close();
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid? id = (Guid?)cmbKeys.GetColumnValue("Id");
            if (id.HasValue
                && id != Guid.Empty)
            {
                txtCName.Text = (string)cmbKeys.GetColumnValue("CName");
                txtEName.Text = (string)cmbKeys.GetColumnValue("EName");
                numVersion.Text = (string)cmbKeys.GetColumnValue("Version");
            }
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="type">表单类型</param>
        /// <param name="text">部署标题</param>
        /// <param name="file">部署文件</param>
        public void InitData(
            string key, 
            string formFile,
            string dataSchameFile)
        {

            _formKey = key;
            _formFileName = formFile;
            _dataSchemaName = dataSchameFile;

        }

        #endregion

        #region 本地方法

        /*控件数据初始化*/
        private void InitControls()
        {

            List<FormProfileList> forms = new List<FormProfileList>();
            byte[] zipBytes = WorkFlowConfigService.GetGetFormProfileListZip(string.Empty, string.Empty, LocalData.IsEnglish);

            forms = (List<FormProfileList>)DataZipStream.DecompressionArrayList(zipBytes);

            if (forms == null)
            {
                return;
            }

            FormProfileList fp = forms.Find(delegate(FormProfileList f) { return f.KeyWord == _formKey; });
            if (fp == null)
            {
                fp = new FormProfileList();
                fp.Id = Guid.Empty;
                fp.KeyWord = _formKey;
                forms.Insert(0, fp);
            }
            //else
            //{
            //    FormProfileList t = new FormProfileList();
            //    t.Id = Guid.Empty;
            //    t.KeyWord = _formKey;
            //    forms.Insert(0, t);

            //    txtCName.Text = fp.CName;
            //    txtEName.Text = fp.EName;
            //    numVersion.Text = fp.Version;
            //}
            
            cmbKeys.Properties.DataSource = forms;
            cmbKeys.Properties.DisplayMember = "KeyWord";
            cmbKeys.Properties.ValueMember = "Id";
            cmbKeys.EditValue = fp.Id;
        }

        /*部署数据源和表单*/
        private void SaveProfile()
        {
                string formContent = string.Empty;
                using (System.IO.StreamReader rd = new System.IO.StreamReader(_formFileName))
                {
                    formContent = rd.ReadToEnd();
                }

                string dataSchameContent = string.Empty;
                using (System.IO.StreamReader rd = new System.IO.StreamReader(_dataSchemaName))
                {
                    dataSchameContent = rd.ReadToEnd();
                }

                    WorkFlowConfigService.SaveFormProfile(
                    (Guid)cmbKeys.EditValue,
                    cmbKeys.Text,
                    txtCName.Text,
                    txtEName.Text,
                    formContent,
                    dataSchameContent,
                    numVersion.Text,
                    LocalData.UserInfo.LoginID,
                    null,LocalData.IsEnglish);
        }


        /*验证*/
        private bool ValidateData()
        {
            errorTip.ClearErrors();

            bool isSucc = true;
            if (string.IsNullOrEmpty(cmbKeys.Text))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "KeyWordNoIsNull"),this.cmbKeys);
                isSucc = false;
                cmbKeys.Focus();
            }

            if (string.IsNullOrEmpty(txtCName.Text))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "CNameIsNull"),this.txtCName);
                isSucc = false;
                txtCName.Focus();
            }

            if (string.IsNullOrEmpty(txtEName.Text))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "ENameIsNull"),this.txtEName);
                isSucc = false;
                txtEName.Focus();
            }

            if (string.IsNullOrEmpty(numVersion.Text))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), NativeLanguageService.GetText(this, "VersionIsNull"),this.numVersion);
                isSucc = false;
                numVersion.Focus();
            }

            return isSucc;
        }

        #endregion

        private void cmbKeys_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            //FormProfileList profile = new FormProfileList();
            //profile.Id = Guid.NewGuid();
            //profile.KeyWord = e.DisplayValue.ToString();
            //((System.Collections.Generic.List<FormProfileList>)cmbKeys.Properties.DataSource).Add(profile);
            //e.Handled = false;
        }

        private void cmbKeys_Leave(object sender, EventArgs e)
        {
            string text = cmbKeys.Text;

            List<FormProfileList> forms = cmbKeys.Properties.DataSource as List<FormProfileList>;
            if (forms == null)
            {
                return; 
            }

           FormProfileList f= forms.Find(delegate(FormProfileList fp) { return fp.KeyWord == text; });
           if (f == null)
           {
               FormProfileList tf = forms.Find(delegate(FormProfileList fp) { return fp.Id == Guid.Empty; });
               tf.KeyWord = text;
           }
           else
           {
               cmbKeys.EditValue = f.Id;
           }
        }
    }
}
