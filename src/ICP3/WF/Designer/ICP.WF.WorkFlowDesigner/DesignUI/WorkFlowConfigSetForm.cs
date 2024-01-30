using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.WF.ServiceInterface.DataObject;
using DevExpress.XtraEditors;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using System.IO;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.WF.Activities;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    /// 发布流程界面
    /// </summary>
    public partial class WorkFlowConfigSetForm : XtraForm
    {
        #region 本地变量
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID
        {
            get;
            set;
        }

        /// <summary>
        /// 流程文件
        /// </summary>
        public string _flowFile
        {
            get;
            set;
        }
        /// <summary>
        /// 规则文件
        /// </summary>
        public string _ruleFile
        {
            get;
            set;
        }
        /// <summary>
        /// 关键字
        /// </summary>
        public string _key
        {
            get;
            set;
        }

        /// <summary>
        /// 职位列表
        /// </summary>
        UCConfigJobList UCJobList = null;
        /// <summary>
        /// 员工列表
        /// </summary>
        UCConfigUserList UCUserList = null;
        /// <summary>
        /// 配置信息
        /// </summary>
        WorkFlowConfigInfo wfInfo = new WorkFlowConfigInfo();
       
        #endregion

        #region 服务

        public IWorkFlowExtendService WorkFlowExtendService
        {
            get 
            {
               return ServiceClient.GetService<IWorkFlowExtendService>();
            }
        }

        public IWorkFlowConfigService WorkFlowConfigService
        {
            get 
            {
                return ServiceClient.GetService<IWorkFlowConfigService>();
            }
        }

        public IDataFindClientService DataFindClientService 
        {
            get 
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IUserService UserService 
        {
            get 
            {
                return ServiceClient.GetService<IUserService>();
            }
        }


        public IJobService JobService 
        {
            get 
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
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

        #region 初始化


        public WorkFlowConfigSetForm()
        {
            InitializeComponent();

            if (DesignMode == false)
            {
                this.Load += new EventHandler(WorkFlowConfigSetForm_Load);
                this.Disposed += new EventHandler(WorkFlowConfigSetForm_Disposed);
            }
        }

        void WorkFlowConfigSetForm_Disposed(object sender, EventArgs e)
        {
            if (this.UCJobList != null)
            {
                this.WorkItem.Items.Remove(this.UCJobList);
                this.UCJobList.Dispose();
                this.UCJobList = null;
            }
            if (this.UCUserList != null)
            {
                this.WorkItem.Items.Remove(this.UCUserList);
                this.UCUserList.Dispose();
                this.UCUserList = null;
            }
            this.cmbKeyWord.SelectedValueChanged -=this.cmbKeyWord_SelectedValueChanged;
            this.wfInfo = null;
            if (this.WorkItem != null)
            {
                this.WorkItem.Items.Remove(this);
                this.WorkItem = null;
            }
        }

        void WorkFlowConfigSetForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.InitControls();

            }
        }


       
      
        #endregion

        #region 外部接口
        /// <summary>
        /// 设置初始化数据
        /// </summary>
        /// <param name="key">Ｋｅｙ</param>
        /// <param name="xomlFile">流程文件</param>
        /// <param name="ruleFile">规则文件</param>
        public void SetSource(string key, string xomlFile, string ruleFile)
        {
            _key = key;
            _flowFile = xomlFile;
            _ruleFile = ruleFile;

        }

        #endregion


        #region 本地方法

        /*控件数据初始化*/
        private void InitControls()
        {
            UCJobList = this.WorkItem.Items.AddNew<UCConfigJobList>();
            UCUserList = this.WorkItem.Items.AddNew<UCConfigUserList>();

            UCJobList.Dock = DockStyle.Fill;
            UCUserList.Dock = DockStyle.Fill;

            gbxJob.Controls.Add(UCJobList);
            gbxUser.Controls.Add(UCUserList);

            ComboxData oldData = null;

            List<WorkFlowConfigInfo> infoList = new List<WorkFlowConfigInfo>();
            byte[] wfList = WorkFlowConfigService.GetWorkFlowConfigListZip(null, LocalData.IsEnglish);
            infoList = (List<WorkFlowConfigInfo>)DataZipStream.DecompressionArrayList(wfList);

            if (infoList == null)
            {
                return;
            }

            foreach(WorkFlowConfigInfo info in infoList)
            {
                ComboxData data = new ComboxData();
                data.Value = info.Id.ToString();
                data.Text = info.KeyWord;
                //设置默认的
                if (ID!=null&& info.Id ==new Guid(ID.ToString()))
                {
                    oldData = data;
                    wfInfo.Id = info.Id;
                }

                cmbKeyWord.Properties.Items.Add(data);
            }
            //选择默认的
            if (oldData != null)
            {
                cmbKeyWord.SelectedItem = oldData;
            }

           // cmbKeyWord.Text = _key;

            txtFlowFile.Text = _flowFile;
            txtRuleFile.Text = _ruleFile;

            //如果没有规则文件，则创建一个新的规则文件
            if (!System.IO.File.Exists(_ruleFile))
            {
                ICPRuleSet ruleSet = new ICPRuleSet();
                RuleSetSerializer.SerializeToFile<ICPRuleSet>(ruleSet, _ruleFile);

            }

           //流程分类
            List<DataDictionaryList> dataList = TransportFoundationService.GetDataDictionaryList(null, null, DataDictionaryType.WorkflowCategory, true, 0);
           foreach (DataDictionaryList data in dataList)
           {
               cmbCategory.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish?data.EName:data.CName,data.ID));
           }
        }


        private bool ValidateData()
        {
            bool isSucc = true;

            if (cmbKeyWord.Text.Trim().Length == 0)
            {
                errorList.SetError(cmbKeyWord, Utility.GetString("MustInput", "Must Input"));
                cmbKeyWord.Focus();
                isSucc = false;
            }
            if (txtEDescription.Text.Trim().Length == 0)
            {
                errorList.SetError(txtEDescription, Utility.GetString("MustInput", "Must Input"));
                txtEDescription.Focus();
                isSucc = false;
            }
            if (txtCDescription.Text.Trim().Length == 0)
            {
                errorList.SetError(txtCDescription, Utility.GetString("MustInput", "Must Input"));
                txtCDescription.Focus();
                isSucc = false;
            }
            if(!File.Exists(txtFlowFile.Text))
            {
                errorList.SetError(txtEDescription, Utility.GetString("FileNoExists", "File No Exists"));
                txtEDescription.Focus();
                isSucc = false;
            }
            if(!File.Exists(txtRuleFile.Text))
            {
                errorList.SetError(txtRuleFile, Utility.GetString("FileNoExists", "File No Exists"));
                txtRuleFile.Focus();
                isSucc = false;
            }
            if (this.cmbCategory.EditValue==null)
            {
                errorList.SetError(cmbCategory, Utility.GetString("MustInput", "Must Input"));
                cmbCategory.Focus();
                isSucc = false;
            }

            if (isSucc == false) return false;

            return isSucc;
        }
        /// <summary>
        /// 保存配置
        /// </summary>
        private bool SaveConfig()
        {
            try
            {
                ///主表信息
                wfInfo.CDescription = this.txtCDescription.Text;
                wfInfo.CPrintTitle = this.txtCPrintTitle.Text;
                wfInfo.Days = Convert.ToByte(this.numDays.Value);
                wfInfo.EDescription = this.txtEDescription.Text;
                wfInfo.EPrintTitle = this.txtEPrintTitle.Text;
                wfInfo.IsOA = this.ckbOA.Checked;
                wfInfo.Version = this.numVersion.Value.ToString();
                wfInfo.KeyWord = this.cmbKeyWord.Text;
                wfInfo.CategoryID = new Guid(cmbCategory.EditValue.ToString());


                ///设计与规则文件               
                string xomlContext = string.Empty;
                if (System.IO.File.Exists(_flowFile))
                {
                    using (System.IO.StreamReader rd = new System.IO.StreamReader(_flowFile))
                    {
                        xomlContext = rd.ReadToEnd();
                    }
                }
                string ruleContext = string.Empty;
                if (System.IO.File.Exists(_ruleFile))
                {
                    using (System.IO.StreamReader rd = new System.IO.StreamReader(_ruleFile))
                    {
                        ruleContext = rd.ReadToEnd();
                    }
                }

                List<Guid?> oIDList = new List<Guid?>();
                List<Guid?> uIDList = new List<Guid?>();

                Guid newID = WorkFlowConfigService.SaveWorkFlowConfigInfo(wfInfo.Id, wfInfo.CategoryID, wfInfo.KeyWord, xomlContext, ruleContext, wfInfo.CDescription, wfInfo.EDescription, wfInfo.EPrintTitle, wfInfo.CPrintTitle, wfInfo.Days, wfInfo.IsOA, LocalData.UserInfo.LoginID, wfInfo.UpdateDate, wfInfo.CDescription, wfInfo.EDescription, wfInfo.Version, LocalData.IsEnglish);



                UCJobList.CheckData();
                UCUserList.CheckData();

            
                #region 保存职位
                if (UCJobList.JobList != null)
                {
                    oIDList.Clear();
                    uIDList.Clear();

                    foreach (WorkFlowConfigJobPermissionInfo job in UCJobList.JobList)
                    {
                        oIDList.Add(job.OrganizationID);
                        uIDList.Add(job.JobID);
                    }
                }

                WorkFlowConfigService.SetWorkflowJobPermissionInfo(
                   newID,
                   oIDList.ToArray(),
                   uIDList.ToArray(),
                   LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                #endregion

                #region 保存用户
                if (UCUserList.UserList != null)
                {
                    oIDList.Clear();
                    uIDList.Clear();

                    foreach (WorkFlowConfigUserPermissionInfo user in UCUserList.UserList)
                    {
                        oIDList.Add(null);
                        uIDList.Add(user.UserID);
                    }

                    WorkFlowConfigService.SetWorkflowUserPermissionInfo(
                        newID,
                        uIDList.ToArray(),
                        LocalData.UserInfo.LoginID, LocalData.IsEnglish);
                }
                #endregion

                  return true;

            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
            return false;
        }

        #endregion


        #region 按钮方法
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateData() )
            {
                return;
            }
            try
            {
                if (!SaveConfig())
                {
                    return;
                }
                else
                {

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(Utility.GetString("Failly", "Save failly"));
            }
        }
        #endregion


        #region 选择的名称发生改变时
        /// <summary>
        /// 选择的名称发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbKeyWord_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbKeyWord.SelectedItem == null)
            {
                wfInfo = new WorkFlowConfigInfo();
                return;
            }
            ComboxData data=cmbKeyWord.SelectedItem as ComboxData;
            if(data==null)
            {
                wfInfo = new WorkFlowConfigInfo();
                if (ID != null)
                {
                    wfInfo.Id = new Guid(ID.ToString());
                }
                return ;
            }
            Guid id = new Guid(data.Value);
            if (id != null)
            {
                wfInfo = WorkFlowConfigService.GetWorkFlowConfigInfoByID(id, LocalData.IsEnglish);
                if (wfInfo == null)
                {
                    return;
                }
                this.txtCDescription.Text = wfInfo.CDescription;
                this.txtCPrintTitle.Text = wfInfo.CPrintTitle;
                this.txtEDescription.Text = wfInfo.EDescription;
                this.txtEPrintTitle.Text = wfInfo.EPrintTitle;

                this.numDays.Value = wfInfo.Days;
                this.numVersion.Value = Convert.ToDecimal(wfInfo.Version) ;

                this.ckbOA.Checked = wfInfo.IsOA;

                this.cmbCategory.EditValue = wfInfo.CategoryID;

                UCJobList.JobList = wfInfo.JobPermissions;
                UCJobList.RefreshList();


                UCUserList.UserList = wfInfo.UserPermissions;
                UCUserList.RefreshList();
            }

        }
        #endregion



    }
    public class ComboxData
    {
        public string Text { set; get; }
        public string Value { set; get; }
        public override string ToString()
        {
            return Text;
        }
    }
}
