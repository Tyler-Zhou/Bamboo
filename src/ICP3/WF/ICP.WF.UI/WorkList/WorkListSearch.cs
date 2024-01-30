using System;
using System.Collections.Generic;
using System.Linq;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Helper;
using System.Windows.Forms;

namespace ICP.WF.UI
{
    public partial class WorkListSearch : BaseSearchPart
    {
        public WorkListSearch()
        {
            InitializeComponent();
            this.Disposed += delegate {
                this.OnSearched = null;
                this.dataList = null;
                if (this.organizationFinder != null)
                {
                    this.organizationFinder.Dispose();
                    this.organizationFinder = null;
                }
                this.cmbWorkFlowCode.OnFirstEnter -= this.OncmbWorkFlowCodeFirstEnter;
                this.txtApplyName.OnFirstEnter -= this.OntxtApplyNameFirstEnter;
                //this.cmbState.OnFirstEnter -= this.OncmbStateFirstEnter;                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            
            };
        }


        #region 服务

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
        /// 搜索器客户端服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
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

        /// <summary>
        /// 任务服务类
        /// </summary>
        public IWorkflowService WorkflowService
        {
            get 
            {
                return ServiceClient.GetService<IWorkflowService>();
            }
        }
        
        /// <summary>
        /// 用户管理
        /// </summary>
        public IUserService UserService
        {
            get 
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        public IOrganizationService OrganizationService
        {
            get 
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        #endregion

        #region 属性
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        List<WorkFlowTypeCS> dataList;
        private IDisposable organizationFinder;
        #endregion

        #region 窗体事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControl();
            }
        }
        private void OncmbWorkFlowCodeFirstEnter(object sender, EventArgs e)
        {
            List<WorkFlowConfigInfo> wfList = new List<WorkFlowConfigInfo>();
            byte[] wfitems = WorkFlowConfigService.GetWorkFlowConfigListZip(null, LocalData.IsEnglish);
            wfList = (List<WorkFlowConfigInfo>)DataZipStream.DecompressionArrayList(wfitems);
            if (wfList == null)
            {
                wfList = new List<WorkFlowConfigInfo>();
            }



            dataList = new List<WorkFlowTypeCS>();


            var infoList = from c in wfList

                           group c by c.CategoryName into g
                           select new { Key = g.Key, Info = g };

            foreach (var s in infoList)
            {
                WorkFlowTypeCS cs = new WorkFlowTypeCS();
                cs.ID = Guid.NewGuid();
                cs.ParentID = CommandConstants.NewID;
                cs.Name = s.Key;
                dataList.Add(cs);

                foreach (var t in s.Info)
                {
                    WorkFlowTypeCS csDetail = new WorkFlowTypeCS();
                    csDetail.ID = t.Id;
                    csDetail.ParentID = cs.ID;
                    csDetail.Name = LocalData.IsEnglish ? t.EDescription : t.CDescription;
                    dataList.Add(csDetail);
                }
            }

            WorkFlowTypeCS allCS = new WorkFlowTypeCS();
            allCS.ID = CommandConstants.NewID;
            allCS.Name = LocalData.IsEnglish ? "All" : "全部";
            allCS.ParentID = null;

            dataList.Add(allCS);



            cmbWorkFlowCode.DataSource = dataList;
            cmbWorkFlowCode.ParentMember = "ParentID";
            cmbWorkFlowCode.ValueMember = "ID";
            cmbWorkFlowCode.DisplayMember = "Name";
        }
        private void OntxtApplyNameFirstEnter(object sender, EventArgs e)
        {
            List<UserList> userList = UserService.GetUserListByList(null, null, null, null, null, null, true, true, 0);


            Dictionary<string, string> col = new Dictionary<string, string>();
            if (LocalData.IsEnglish)
            {
                col.Add("EName", "Name");
                col.Add("Code", "Code");
            }
            else
            {
                col.Add("CName", "名称");
                col.Add("Code", "代码");
            }

            txtApplyName.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "Code");
        }
        /// <summary>
        /// 初始化控件 
        /// </summary>
        private void InitControl()
        {
            this.grxType.Properties.Items.Clear();
            this.grxType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("All", LocalData.IsEnglish?"ALL": "全部"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("ME", LocalData.IsEnglish?"I Created":"我创建的"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("CY", LocalData.IsEnglish?"I participate":"我参与的"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("XS", LocalData.IsEnglish?"Subordinate Created":"下属创建的")});
            this.grxType.Size = new System.Drawing.Size(120, 92);

            this.dateMonthControl1.IsEngish = LocalData.IsEnglish;
            this.grxType.SelectedIndex = 0;

            #region 初始化流程下拉框
            this.cmbWorkFlowCode.OnFirstEnter += this.OncmbWorkFlowCodeFirstEnter;

            #endregion


            #region 初始化部门下拉框

          organizationFinder=  DataFindClientService.RegisterMiniFinder(stxtOrganization, SystemFinderConstants.OrganizationFinder, @"Code/Name"
                          , LocalData.IsEnglish ? "EShortName" : "CShortName", "ID", new string[] { "ID", "EShortName", "CShortName" },
                delegate(object inputSource, object[] resultData)
                {
                    stxtOrganization.Text = LocalData.IsEnglish ? resultData[1].ToString() : resultData[2].ToString();
                    stxtOrganization.Tag = new Guid(resultData[0].ToString());
                }, null);

            #endregion

            #region  绑定用户
          this.txtApplyName.OnFirstEnter += this.OntxtApplyNameFirstEnter;
     
            #endregion

            #region 状态
            //this.cmbState.ShowSelectedValue(WorkItemSearchStatus.Waiting,EnumHelper.GetDescription<WorkItemSearchStatus>(WorkItemSearchStatus.Waiting,LocalData.IsEnglish));
            //this.cmbState.OnFirstEnter += this.OncmbStateFirstEnter;
          List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<WorkItemSearchStatus>> masterStatus = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<WorkItemSearchStatus>(LocalData.IsEnglish);
          this.chcState.Properties.BeginUpdate();
          this.chcState.Properties.Items.Clear();
          foreach (var item in masterStatus)
          {
              if (item.Value != WorkItemSearchStatus.All)
              {
                  if (item.Value == WorkItemSearchStatus.Waiting || item.Value == WorkItemSearchStatus.Processing)
                      chcState.Properties.Items.Add(item.Value, item.Name, CheckState.Checked, true);
                  else
                      chcState.Properties.Items.Add(item.Value, item.Name, CheckState.Unchecked, true);
              }
          }
          this.chcState.Properties.EndUpdate();
            
            #endregion

        }
        //private void OncmbStateFirstEnter(object sender, EventArgs e)
        //{
        //    List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<WorkItemSearchStatus>> statusList = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<WorkItemSearchStatus>(LocalData.IsEnglish);
        //    this.cmbState.Properties.BeginUpdate();
        //    this.cmbState.Properties.Items.Clear();
        //    foreach (ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<WorkItemSearchStatus> status in statusList)
        //    {
        //        cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(status.Name, status.Value));
        //    }
        //    this.cmbState.Properties.EndUpdate();
        //}
        #endregion

        #region 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.stxtOrganization.Tag = null;
            this.stxtOrganization.Text = string.Empty;

            this.cmbWorkFlowCode.SelectedValue = null;
            this.cmbWorkFlowCode.SelectedText = null;


            this.txtApplyName.EditValue = null;
            this.txtApplyName.EditText = null;

            this.txtNo.Text = string.Empty;
            this.txtWorkName.Text = string.Empty;

            grxType.SelectedIndex = 0;

            //this.cmbState.SelectedText = string.Empty;
            //this.cmbState.SelectedItem = null;
            this.chcState.SelectedText = string.Empty;            


            this.numMax.Value = 100;
        }

        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");

            if (OnSearched != null)
            {
                OnSearched(this, GetData());
            }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
            //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }

        public List<WorkItemSearchStatus> StateList
        {
            get
            {
                List<WorkItemSearchStatus> list = new List<WorkItemSearchStatus>();
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chcState.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        list.Add((WorkItemSearchStatus)(item.Value));
                }
                return list;
            }
        }

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            string workflowName = string.Empty,
                   workName = this.txtWorkName.Text,
                   no = this.txtNo.Text,
                   applicantorName = string.Empty,
                   subordinateName = string.Empty;

            //WorkItemSearchStatus? state = null;

            int maxResult = (int)numMax.Value;

            //if (cmbState.EditValue != null && !string.IsNullOrEmpty(cmbState.EditValue.ToString()))
            //{
            //    state = (WorkItemSearchStatus)cmbState.EditValue;
            //}
            if (string.IsNullOrEmpty(this.txtApplyName.EditText))
            {
                this.txtApplyName.EditValue = null;
            }

            if (this.txtApplyName.EditValue != null)
            {
                applicantorName = this.txtApplyName.EditValue.ToString();
            }

            Guid? depID = null;
            if (this.stxtOrganization.Tag != null && !string.IsNullOrEmpty(this.stxtOrganization.Tag.ToString()))
            {
                depID = new Guid(this.stxtOrganization.Tag.ToString());
            }

            WorkListSearchType searchType = (WorkListSearchType)this.grxType.SelectedIndex;

            #region 流程
            if (cmbWorkFlowCode.Text != null)
            {
                workflowName = this.cmbWorkFlowCode.Text.ToString();

                if (this.cmbWorkFlowCode.SelectedValue != null && this.cmbWorkFlowCode.SelectedValue.ToString() == CommandConstants.NewID.ToString())
                {
                    workflowName = null;
                }
            }
            #endregion
            DateTime? endFromDate = null;
            DateTime? endToDate = null;
            if (this.lwDatePicker1.DateTime !=DateTime.MinValue)
            {
                endFromDate = this.lwDatePicker1.DateTime;
            }
            if (this.lwDatePicker2.DateTime != DateTime.MinValue)
            {
                endToDate = this.lwDatePicker2.DateTime;
            }
            try
            {
             //   this.dateMonthControl2.From
                //调用接口，查询数据
               string result  = WorkflowService.GetWorkList(
                    LocalData.UserInfo.LoginID,
                    depID,
                    workflowName,
                    workName,
                    no,
                    applicantorName,
                    searchType,
                    StateList.ToArray(),
                    this.dateMonthControl1.From,
                    this.dateMonthControl1.To,
                    endFromDate,
                    endToDate,
                    maxResult
                    );

               List<WorkFlowData> workFlowDataList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<WorkFlowData>>(result);
               return workFlowDataList;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return null;
            }


        }
        #endregion

    

    }
}
