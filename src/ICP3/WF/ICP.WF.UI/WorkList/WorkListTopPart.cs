using System;
using System.Collections.Generic;
using System.ComponentModel;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using DevExpress.XtraTreeList.Nodes;
using ICP.Framework.CommonLibrary.Server;
using ICP.Framework.ClientComponents.UIFramework;



namespace ICP.WF.UI
{
    public partial class WorkListTopPart : ICP.Framework.ClientComponents.UIManagement.BasePart
    {
        public WorkListTopPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OrgList = null;
                this.treeDepartment.DoubleClick -= this.treeDepartment_DoubleClick;
                this.cmbOrganization.EditValueChanged -= this.cmbOrganization_EditValueChanged;
                this.cmbOrganization.Properties.QueryPopUp -= this.cmbOrganization_Properties_QueryPopUp;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }

            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem { get; set; }

        /// <summary>
        /// 工作流配置管理服务
        /// </summary>
        public IWorkFlowConfigService WorkFlowConfigService
        {
            get
            {
                return ServiceClient.GetService<IWorkFlowConfigService>();
            }
        }

        /// <summary>
        /// 用户服务类
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        /// <summary>
        /// 职位服务类
        /// </summary>
        public IJobService JobService
        {
            get
            {
                return ServiceClient.GetService<IJobService>();
            }
        }

        /// <summary>
        /// 流程服务类
        /// </summary>
        public IWorkflowService workFlowService
        {
            get
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }


        #endregion


        #region 属性
        /// <summary>
        /// 流程ID
        /// </summary>
        public Guid WorkFlowConfigID
        {
            get;
            set;
        }
        /// <summary>
        /// 所属部门
        /// </summary>
        public Guid OrganizationID
        {
            get;
            set;
        }
        /// <summary>
        /// 工作名称
        /// </summary>
        public string WorkName
        {
            get;
            set;
        }


        /// <summary>
        /// 工作项明细
        /// </summary>
        public WorkItemInfo workItem
        {
            get;
            set;
        }
        /// <summary>
        /// 工作名
        /// </summary>
        public string WorkItemName
        {
            get
            {
                return this.txtWorkName.Text;
            }
            set
            {
                this.txtWorkName.Text = value;
            }
        }
        /// <summary>
        /// 流程名
        /// </summary>
        public string WorkFlowName
        {
            get
            {
                return this.txtWorkFlow.Text;
            }
        }
        /// <summary>
        /// 单号
        /// </summary>
        public string WorkItemNo
        {
            get
            {
                return this.txtNo.Text;
            }
            set
            {
                this.txtNo.Text = value;
            }
        }

        public List<UserOrganizationTreeList> OrgList = new List<UserOrganizationTreeList>();
        #endregion

        #region 窗体加载

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);

            if (!DesignMode)
            {
                //InitControls();
            }
        }

        #endregion

        #region 初始化数据
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitControls(bool cmbOrganizationEnabled = true)
        {
            if (!string.IsNullOrEmpty(this.WorkName))
            {
                this.txtWorkName.Text = this.WorkName;
            }

            this.cmbOrganization.Enabled = cmbOrganizationEnabled;
            ///绑定流程名
            WorkFlowConfigInfo info = WorkFlowConfigService.GetWorkFlowConfigInfoByID(WorkFlowConfigID, LocalData.IsEnglish);
            this.txtWorkFlow.Text = LocalData.IsEnglish ? info.EDescription : info.CDescription;

            ///绑定部门列表
            OrgList = UserService.GetUserOrganizationTreeList(LocalData.UserInfo.LoginID);
            bsList.DataSource = OrgList;


            this.colCShortName.Visible = !LocalData.IsEnglish;
            this.colEShortName.Visible = LocalData.IsEnglish;


            //默认当前用户的默认部门 Update 周任平  2011-06-16
            //再次修改部门，主要针对分公司总裁、副总裁、总经理没有默认部门的用户
            //如果当前用户的部门列表中有管理部，则默认部门为默认公司下的管理部
            LocalOrganizationInfo depInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.IsDefault && item.Type == LocalOrganizationType.Department; });
            if (depInfo != null)
            {
                cmbOrganization.Text = depInfo.FullName;
                OrganizationID = depInfo.ID;
            }
            else
            {
                //没有默认部门，找到默认公司
                LocalOrganizationInfo companyInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.IsDefault && item.Type == LocalOrganizationType.Company; });
                if (companyInfo != null)
                {
                    LocalOrganizationInfo glbDepInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.Type == LocalOrganizationType.Department && item.ParentID == companyInfo.ID && item.CShortName == "管理部"; });
                    if (glbDepInfo != null)
                    {
                        cmbOrganization.Text = glbDepInfo.FullName;
                        OrganizationID = glbDepInfo.ID;
                    }
                    else
                    {
                        cmbOrganization.Text = companyInfo.FullName;
                        OrganizationID = companyInfo.ID;
                    }
                }
                else
                {
                    //没有默认公司、找到默认区域
                    //LocalOrganizationInfo regionInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.IsDefault && item.Type == LocalOrganizationType.Section; });
                    //if (regionInfo != null)
                    //{
                    //    cmbOrganization.Text = regionInfo.FullName;
                    //    OrganizationID = regionInfo.ID;
                    //}
                    //else
                    //{

                    //    LocalOrganizationInfo hqInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.IsDefault && item.Type == LocalOrganizationType.Root; });
                    //    if (hqInfo != null && hqInfo.DefaultCompanyID != null)
                    //    {
                    //        cmbOrganization.Text = hqInfo.DefaultCompanyFullName;
                    //        OrganizationID = hqInfo.DefaultCompanyID.Value;
                    //    }
                    //    else
                    //    {
                    //        DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The user has no default department." : "该用户没有默认部门。");
                    //        return;
                    //    }
                    //}
                    LocalOrganizationInfo hqInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.IsDefault && item.Type == LocalOrganizationType.Root; });
                    if (hqInfo != null && hqInfo.DefaultCompanyID != null)
                    {
                        cmbOrganization.Text = hqInfo.DefaultCompanyFullName;
                        OrganizationID = hqInfo.DefaultCompanyID.Value;
                    }
                    else
                    {
                        LocalOrganizationInfo regionInfo = LocalData.UserInfo.UserOrganizationList.Find(delegate(LocalOrganizationInfo item) { return item.IsDefault && item.Type == LocalOrganizationType.Section; });
                        if (regionInfo != null)
                        {
                            cmbOrganization.Text = regionInfo.FullName;
                            OrganizationID = regionInfo.ID;
                        }
                        else
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The user has no default department." : "该用户没有默认部门。");
                            return;
                        }
                    }
                }
            }

        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            bool isSucc = true;

            errorList.ClearErrors();

            if (string.IsNullOrEmpty(txtWorkFlow.Text))
            {
                errorList.SetError(txtWorkFlow, Utility.GetString("MustInput", "Must Input"));
                txtWorkFlow.Focus();
                isSucc = false;
            }
            if (string.IsNullOrEmpty(txtWorkName.Text))
            {
                errorList.SetError(txtWorkName, Utility.GetString("MustInput", "Must Input"));
                txtWorkName.Focus();
                isSucc = false;
            }
            if (cmbOrganization.EditValue == null)
            {
                errorList.SetError(cmbOrganization, Utility.GetString("MustInput", "Must Input"));
                cmbOrganization.Focus();
                isSucc = false;
            }

            return isSucc;
        }
        #endregion

        #region 获得数据
        /// <summary>
        /// 获得数据
        /// </summary>
        public void GetData()
        {
            WorkName = this.txtWorkName.Text;

        }
        #endregion

        #region 保存数据
        public void Save()
        {

        }
        #endregion

        #region 双击选择
        /// <summary>
        /// 双击选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeDepartment_DoubleClick(object sender, EventArgs e)
        {
            OrganizationList selectOrg = bsList.Current as OrganizationList;
            if (selectOrg != null)
            {
                this.treeDepartment.ExpandAll();

                this.cmbOrganization.Text = selectOrg.FullName;

                OrganizationID = selectOrg.ID;
            }

            cmbOrganization.ClosePopup();

        }
        private void cmbOrganization_Properties_QueryPopUp(object sender, CancelEventArgs e)
        {
            if (this.cmbOrganization.EditValue != null && OrganizationID != null && OrganizationID != Guid.Empty)
            {
                TreeListNode node = treeDepartment.FindNodeByFieldValue("ID", OrganizationID);
                if (node != null)
                {
                    treeDepartment.FocusedNode = node;
                }
            }
        }
        #endregion


        public event SelectedHandler Selected;
        private void cmbOrganization_EditValueChanged(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                UserOrganizationTreeList item = bsList.Current as UserOrganizationTreeList;
                if (item != null)
                {
                    Selected(sender, item);
                }
            }
        }


    }
}
