using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.FAM.UI
{
    public partial class AgentBillCheckingEdit : BaseEditPart
    {
        public AgentBillCheckingEdit()
        {
            InitializeComponent();
            Closing += new EventHandler<FormClosingEventArgs>(AgentBillCheckingEdit_Closing);
            AdvancedCheckList_gridView.RowCellStyle+=new RowCellStyleEventHandler(AdvancedCheckList_gridView_RowCellStyle);
            SimpleCheckList_gridView.RowCellStyle += new RowCellStyleEventHandler(SimpleCheckList_gridView_RowCellStyle);
            Disposed += delegate
            {
                Closing -= AgentBillCheckingEdit_Closing;
                cmbCheckCompany.OnFirstEnter -= OnCheckCompanyFirstTimeEnter;
                cmbCheckCompany.SelectedIndexChanged -= cmbCheckCompany_SelectedIndexChanged;
                AdvancedCheckList_gridView.RowCellStyle -= AdvancedCheckList_gridView_RowCellStyle;
                SimpleCheckList_gridView.RowCellStyle -= SimpleCheckList_gridView_RowCellStyle;
                CheckList_gridControl.DataSource = null;
                bsAgentCheckBill.DataSource = null;
                bsAgentCheckBill.Dispose();
                bsDetailList.DataSource = null;
                bsDetailList.Dispose();
                Saved = null;
                _agnetBillCheckList = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        void AgentBillCheckingEdit_Closing(object sender, FormClosingEventArgs e)
        {
            if (_agnetBillCheckList.IsDirty && barSave.Enabled)
            {
                DialogResult dr = FAMUtility.EnquireIsSaveCurrentDataByUpdated();

                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!Save())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

     

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetService<IFinanceClientService>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        #endregion

        #region 属性&变量

        public override object DataSource
        {
            get
            {
                return bsAgentCheckBill.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        /// <summary>
        /// 当前对账数据
        /// </summary>
        private AgnetBillCheckList _agnetBillCheckList
        {
            get;
            set;
        }

        private List<AgentBillCheckDetail> detailList;
        /// <summary>
        /// 明细列表数据源(全部)
        /// </summary>
        public List<AgentBillCheckDetail> DetailList
        {
            set
            {
                detailList = value;
            }
            get
            {
                return detailList;
            }
        }
        /// <summary>
        /// 明细列表当前数据源
        /// </summary>
        private List<AgentBillCheckDetail> CurrentDetailist
        {
            get
            {
                return bsDetailList.DataSource as List<AgentBillCheckDetail>;
            }
            set
            {
                bsDetailList.DataSource = value;
                bsDetailList.ResetBindings(false);
            }
        }

        /// <summary>
        /// 是否有错误数据
        /// </summary>
        private bool isError
        {
            
            get
            {
                if (DetailList == null)
                {
                    return true;
                }
                int i = (from d in DetailList where d.Gap > 0 select d.Gap).Count();

                return i > 0 ? true : false;                
            }


        }

        public override event SavedHandler Saved;

        #endregion

        #region 初始化
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="data"></param>
        private void BindingData(object data)
        {

            _agnetBillCheckList = data as AgnetBillCheckList;

            if (data == null)
            {
                _agnetBillCheckList = new AgnetBillCheckList();
                _agnetBillCheckList.CreateID = LocalData.UserInfo.LoginID;
                _agnetBillCheckList.CreateName = LocalData.UserInfo.LoginName;
                _agnetBillCheckList.LaunchCompanyID = LocalData.UserInfo.DefaultCompanyID;
                _agnetBillCheckList.LaunchCompanyName = LocalData.UserInfo.DefaultCompanyName;
                _agnetBillCheckList.LaunchUserID = LocalData.UserInfo.LoginID;
                _agnetBillCheckList.LaunchUserName = LocalData.UserInfo.LoginName;
                _agnetBillCheckList.Status = AgentBillCheckStatusEnum.Created;
                _agnetBillCheckList.EndingETD = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddMonths(-1);
                _agnetBillCheckList.CreateDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                _agnetBillCheckList.OperValues = "1,2,3,4,5";
            }
            else
            {
                //绑定主表信息
                _agnetBillCheckList = FinanceService.GetAgnetBillCheckInfo(_agnetBillCheckList.ID, LocalData.IsEnglish);
                _agnetBillCheckList.OperValues = _agnetBillCheckList.OperValues.Replace(GlobalConstants.DividedSymbol,", ");

                //绑定明细
                BindDetailList();
            }


            bsAgentCheckBill.DataSource = _agnetBillCheckList;
            bsAgentCheckBill.ResetBindings(false);


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
                SetSimpleStyle();
            }
        }
        /// <summary>
        /// 设置简洁栏位的样式
        /// </summary>
        private void SetSimpleStyle()
        {
            if (FAMUtility.GuidIsNullOrEmpty(_agnetBillCheckList.ID))
            {
                return;
            }

            List<Guid> companyIDList = FAMUtility.GetCompanyIDList();

           
            if (companyIDList.Contains(_agnetBillCheckList.LaunchCompanyID))
            {
                //如果是发起方查看
                debit1_gridColumn.Visible=true;
                credit1_gridColumn.Visible=true;
                balance1_gridColumn.Visible = true;

                balance2_gridColumn.Visible = false;
                credit2_gridColumn.Visible = false;
                debit2_gridColumn.Visible = false;

            }
            else if (companyIDList.Contains(_agnetBillCheckList.CheckCompanyID))
            {
                //如果是核对方查看
                debit1_gridColumn.Visible = false;
                credit1_gridColumn.Visible = false;
                balance1_gridColumn.Visible = false;
                balance2_gridColumn.Visible = true;
                credit2_gridColumn.Visible = true;
                debit2_gridColumn.Visible = true;
            }
            else
            {
                //否则，禁用简洁栏位
                ckbMuit.Enabled = false;
            }


        }
        private void OnCheckCompanyFirstTimeEnter(object sender, EventArgs e)
        {
            FAMUtility.BindComboBoxByAllCompany(cmbCheckCompany);
            
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {

            InitOperType();
       
            if (_agnetBillCheckList.ID==Guid.Empty)
            {

                //绑定当前公司所在公司列表
                FAMUtility.BindComboBoxByCompany(cmbLaunchCompany);
                //默认公司与用户
                cmbLaunchCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID,LocalData.UserInfo.DefaultCompanyName);
                mscLaunchUser.ShowSelectedValue(LocalData.UserInfo.LoginID,LocalData.UserInfo.LoginName);

                mscLaunchUser.ReadOnly = true;
                //核对公司
                cmbCheckCompany.OnFirstEnter += OnCheckCompanyFirstTimeEnter;
                
               
            }
            else
            {
                cmbLaunchCompany.ShowSelectedValue(_agnetBillCheckList.LaunchCompanyID, _agnetBillCheckList.LaunchCompanyName);
                mscLaunchUser.ShowSelectedValue(_agnetBillCheckList.LaunchUserID, _agnetBillCheckList.LaunchUserName);
                cmbCheckCompany.ShowSelectedValue(_agnetBillCheckList.CheckCompanyID, _agnetBillCheckList.CheckCompanyName);
                mscCheckUser.ShowSelectedValue(_agnetBillCheckList.CheckUserID, _agnetBillCheckList.CheckUserName);
            }
            SetToolInfo();
            cmbCheckCompany.SelectedIndexChanged += cmbCheckCompany_SelectedIndexChanged;

            _agnetBillCheckList.CancelEdit();
            _agnetBillCheckList.BeginEdit();
        }
        /// <summary>
        /// 绑定业务类型下拉框
        /// </summary>
        private void InitOperType()
        {
            List<EnumHelper.ListItem<OperationType>> operType = EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            if (_agnetBillCheckList.ID==Guid.Empty)
            {
                //新增时，默认全部都选中
                foreach (var item in operType)
                {
                    if (item.Value == OperationType.OceanExport || item.Value == OperationType.OceanImport || item.Value == OperationType.AirExport || item.Value == OperationType.AirImport || item.Value == OperationType.Other)
                    {
                        cmbOperType.Properties.Items.Add(item.Value.GetHashCode(), item.Name, CheckState.Checked, true);
                    }
                }
                cmbOperType.Properties.SelectAllItemCaption = LocalData.IsEnglish ? "All" : "全部";
                _agnetBillCheckList.OperValues = "1, 2, 3, 4, 5";
            }
            else
            { 
                //查看状态下
                foreach (var item in operType)
                {
                    CheckState checkeState  ;
                    if (item.Value == OperationType.OceanExport || item.Value == OperationType.OceanImport || item.Value == OperationType.AirExport || item.Value == OperationType.AirImport || item.Value == OperationType.Other)
                    {
                        if (_agnetBillCheckList.OperValues.Contains(item.Value.GetHashCode().ToString()))
                        {
                            checkeState = CheckState.Checked;
                        }
                        else
                        {
                            checkeState = CheckState.Unchecked;
                        }

                        cmbOperType.Properties.Items.Add(item.Value.GetHashCode().ToString(), item.Name, checkeState,true);
                    }
                }
                cmbOperType.Properties.SelectAllItemCaption = LocalData.IsEnglish ? "All" : "全部";


            }
           toolTipOperType.SetToolTip(cmbOperType, cmbOperType.Text);
        }

        /// <summary>
        /// 设置工具栏的状态与状态的描述
        /// </summary>
        private void SetToolInfo()
        {
            barCompleted.Enabled = false;
            barStartCheck.Enabled = false;
            barWriteOff.Enabled = false;
            barChecking.Enabled = false;
            barNotifiedBillOwner.Enabled = false; 
            barSave.Enabled=false;
            barPrint.Enabled=false;
            txtStatus.Text = string.Empty;

            if (_agnetBillCheckList.ID == Guid.Empty)
            {
                barSave.Enabled = true;
                barRefresh.Enabled = false;
            }
            else if (_agnetBillCheckList.LaunchUserID == LocalData.UserInfo.LoginID)
            {
                #region 当前用户是发起方

                barChecking.Enabled = false;
                barNotifiedBillOwner.Enabled = false;
                barCompleted.Enabled = false;


                if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.Created)
                {
                    #region 状态是创建时
                    barSave.Enabled = true;

                    if (DetailList == null || DetailList.Count == 0)
                    {
                        barStartCheck.Enabled = false;
                        txtStatus.Text = NativeLanguageService.GetText(this, "1107290002");
                    }
                    else
                    {
                        barStartCheck.Enabled = true;
                        txtStatus.Text = NativeLanguageService.GetText(this, "1107290003");
                    }
                    #endregion
                }
                else
                {
                    //禁用按钮与对账数据
                    SetBaseReadOnly();
                    barStartCheck.Enabled = false;
                    barSave.Enabled = false;

                    string userName = _agnetBillCheckList.CheckCompanyName + ": " + _agnetBillCheckList.CheckUserName;

                    if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.NotifiedBillOwner)
                    {
                        #region 修改帐单状态

                        SetBaseReadOnly();
                        barSave.Enabled = true;
                        barStartCheck.Enabled = true;
                        txtStatus.Text = string.Format(NativeLanguageService.GetText(this, "1107290006"), userName);

                        #endregion
                    }
                    else
                    {
                        #region 其它状态

                        if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.StartCheck)
                        {
                            txtStatus.Text = string.Format(NativeLanguageService.GetText(this, "1107290004"), userName);
                        }
                        else if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.Checking)
                        {
                            txtStatus.Text = string.Format(NativeLanguageService.GetText(this, "1107290005"), userName);
                        }
                        else if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.Completed)
                        {
                            txtStatus.Text = NativeLanguageService.GetText(this, "1107290007");
                            barWriteOff.Enabled = true;
                        }
                        #endregion
                    }
                }
                #endregion
            }
            else if (_agnetBillCheckList.CheckUserID == LocalData.UserInfo.LoginID)
            {
                #region 当前用户是核对人
                SetBaseReadOnly();
                barStartCheck.Enabled = false;
                barSave.Enabled = false;

                string userName = _agnetBillCheckList.LaunchCompanyName + ": " + _agnetBillCheckList.LaunchUserName;

                if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.Created)
                {
                    txtStatus.Text = string.Format(NativeLanguageService.GetText(this, "1107290008"), userName);
                }
                else if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.StartCheck)
                {
                    txtStatus.Text = string.Format(NativeLanguageService.GetText(this, "1107290009"), userName);
                    barChecking.Enabled = true;
                    barNotifiedBillOwner.Enabled = false;
                    barCompleted.Enabled = false;

                }
                else if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.Checking)
                {
                    if (isError)
                    {
                        txtStatus.Text = NativeLanguageService.GetText(this, "1107290010");
                        barNotifiedBillOwner.Enabled = true;
                        barCompleted.Enabled = false;
                    }
                    else
                    {
                        txtStatus.Text = NativeLanguageService.GetText(this, "1107290011");
                        barNotifiedBillOwner.Enabled = false;
                        barCompleted.Enabled = true;
                    }
                }
                else if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.NotifiedBillOwner)
                {
                    txtStatus.Text = string.Format(NativeLanguageService.GetText(this, "1107290012"), userName);
                    barCompleted.Enabled = false;
                    barChecking.Enabled = true;
                    barNotifiedBillOwner.Enabled = false;
                }
                else if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.Completed)
                {
                    txtStatus.Text = NativeLanguageService.GetText(this, "1107290013");

                    barCompleted.Enabled = false;
                    barChecking.Enabled = false;
                    barNotifiedBillOwner.Enabled = false;

                    barWriteOff.Enabled = true;
                }
                #endregion
            }
            else
            {
                #region 其它人
                txtStatus.Text = NativeLanguageService.GetText(this, "1107290014");
                SetBaseReadOnly();

                barSave.Enabled = false;
                barStartCheck.Enabled = false;
                barChecking.Enabled = false;
                barNotifiedBillOwner.Enabled = false;
                barCompleted.Enabled = false;
                barWriteOff.Enabled = false;

                #endregion
            }


            if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.Completed)
            {
                barWriteOff.Enabled = true;
                barPrint.Enabled = true;
            }
            else
            {
                barWriteOff.Enabled = false;
                barPrint.Enabled = false;
            }


        }
        /// <summary>
        /// 不允许修改对账单的内容
        /// </summary>
        private void SetBaseReadOnly()
        {
            cmbLaunchCompany.Properties.ReadOnly = true;
            cmbCheckCompany.Properties.ReadOnly = true;
            mscLaunchUser.ReadOnly = true;
            cmbOperType.Properties.ReadOnly = true;
            dteEngingETD.Properties.ReadOnly = true;
        }
        /// <summary>
        /// 根据公司绑定员工 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbCheckCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid orgID;
            mscCheckUser.ShowSelectedValue(null,null);
            if(cmbCheckCompany.EditValue==null||(Guid)cmbCheckCompany.EditValue==Guid.Empty)
            {
               orgID=Guid.Empty;
            }
            else
            {
                orgID=(Guid)cmbCheckCompany.EditValue;
            }
            //根据选择的公司绑定员工
            FAMUtility.BindCmbBoxUserByOrg(mscCheckUser, orgID);
        }
        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMessage()
        {

            RegisterMessage("1107290002", LocalData.IsEnglish?"The Verifing Bill has been created, you need to click [Save] to generate the verifing list.":"对账单已创建，您需要单击[保存]按钮生成核对明细");
            RegisterMessage("1107290003",LocalData.IsEnglish?"The list of the bill verifing is generated, you can click [Apply Verifing] to apply verification with the agent. ":"已创建对账单并已生成对账明细，您可以单击[发起对账]按钮发起对账了");
            RegisterMessage("1107290004", LocalData.IsEnglish ? "Verifing Bill is applied." : "已发起对账,等待[{0}]核对账单");
            RegisterMessage("1107290005", LocalData.IsEnglish ? "Verifing Bill is in progress. " : "核对账单中,等待[{0}]完成对账");
            //this.RegisterMessage("1107290006", "[{0}]已通知修改账单，核对无误后您需要重新点击[保存]重新生成对账明细");
            RegisterMessage("1107290006", LocalData.IsEnglish ? "The agent, [{0}] has sent the notice of revising bills. You need to click [Save] to re-generate the verifing list after checking without mistakes,then apply verifing." : "[{0}]已通知修改账单，核对无误后您需要点击[发起对账]重新生成对账明细和发起对账");

            RegisterMessage("1107290007", LocalData.IsEnglish ? "Verifing Bill is done, The all of bills could be paied." : "已完成对账，您可以进行销账了");
            RegisterMessage("1107290008", LocalData.IsEnglish ? "Verifing Bill is created." : "已创建,等待[{0}]发起对账");
            RegisterMessage("1107290009", LocalData.IsEnglish ? "The agent, [{0}] has applied the Verifing Bill." : "[{0}]已发起对账，您可以点击[核对账单]进行核对");
            RegisterMessage("1107290010", LocalData.IsEnglish ? "Verfing Bill is done. And mistakes are found. You need to send notification for revising bills." : "核对账单并发现错误，您需要通知修改账单");
            RegisterMessage("1107290011", LocalData.IsEnglish ? "Verfing Bill is done without mistakes. You can finish the Verifing Bill." : "核对账单并确认正确，您可以完成对账了");
            RegisterMessage("1107290012", LocalData.IsEnglish ? "The notice is sent for revising bills." : "已通知修改账单 ,等候[{0}]重新发起对账");
            RegisterMessage("1107290013", LocalData.IsEnglish ? "Verifing Bill is finished. You can Pay the bills." : "本次对账已完成,您可以进行销账了");
            RegisterMessage("1107290014", LocalData.IsEnglish ? "Operations on the selected Verifing Bill only allow it's Apply By or Verify By." : "只有发起方或核对方才拥有权限处理这票对账单");
            RegisterMessage("1107290015", LocalData.IsEnglish ? "Could not apply Verifing because the list of bills is empty." : "核对明细为空,不能发起对账");
            RegisterMessage("1107290016", LocalData.IsEnglish ? "As there are mistakes in bills, you need to notify for revising bills." : "不能完成对账单，因为账单有错误，您需要通知修改账单");

            RegisterMessage("1108250001", LocalData.IsEnglish ? "Verifing is applied." : "发起对账成功");
            RegisterMessage("1108250002", LocalData.IsEnglish ? "The state of the Verifing Bill is changed." : "更改对账单状态成功"); 
        }

        /// <summary>
        /// 绑定明细列表
        /// </summary>
        private void BindDetailList()
        {
            DetailList= FinanceService.GetAgentBillCheckDetailList(_agnetBillCheckList.ID, LocalData.IsEnglish);

            if (DetailList == null)
            {
                return;
            }
            if(ckbGap.Checked)
            {
                CurrentDetailist = (from d in DetailList where d.Gap != 0 select d).ToList();
            }
            else
            {
                CurrentDetailist = DetailList;
            }
        }
        #endregion

        #region Guid Style
        private void Maximize_checkButton_CheckedChanged(object sender, EventArgs e)
        {

            gcBase.Visible = !Maximize_checkButton.Checked;

        }

        private void AdvancedColumn_checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbMuit.Checked == true)
                CheckList_gridControl.MainView = CheckList_gridControl.ViewCollection[1];
            else
                CheckList_gridControl.MainView = CheckList_gridControl.ViewCollection[0];
        }


        private void AdvancedCheckList_gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column != colGap) return;

            AgentBillCheckDetail checkDetail = AdvancedCheckList_gridView.GetRow(e.RowHandle) as AgentBillCheckDetail;
            if (checkDetail == null) return;

            if (checkDetail.Gap != 0)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

        void SimpleCheckList_gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            if (e.Column != gap_gridColumn) return;

            AgentBillCheckDetail checkDetail = SimpleCheckList_gridView.GetRow(e.RowHandle) as AgentBillCheckDetail;
            if (checkDetail == null) return;

            if (checkDetail.Gap != 0)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }
        #endregion

        #region 工具栏方法

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Save())
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                }
            }
        }

        private bool Save()
        {
            if (_agnetBillCheckList.Status != AgentBillCheckStatusEnum.Created && _agnetBillCheckList.Status != AgentBillCheckStatusEnum.NotifiedBillOwner)
            {
                //状态不是创建、通知修改账单的,返回
                return false;
            }
            if (_agnetBillCheckList.LaunchUserID != LocalData.UserInfo.LoginID)
            {
                //当前用户不是发起人,返回
                return false;
            }

            if (!ValidateData())
            {
                //验证信息没有通过,返回
                return false;
            }
            try
            {
                SingleResult singleResult = FinanceService.SaveAgentBillCheck(
                                 _agnetBillCheckList.ID,
                                 _agnetBillCheckList.LaunchCompanyID,
                                 _agnetBillCheckList.LaunchUserID,
                                 _agnetBillCheckList.CheckCompanyID,
                                 _agnetBillCheckList.CheckUserID,
                                 _agnetBillCheckList.OperationTypes,
                                 _agnetBillCheckList.EndingETD,
                                 _agnetBillCheckList.CreateID,
                                 _agnetBillCheckList.UpdateDate,
                                 LocalData.IsEnglish);

                _agnetBillCheckList.OperTexts = cmbOperType.Text;
                if (FAMUtility.GuidIsNullOrEmpty(_agnetBillCheckList.ID))
                {
                    _agnetBillCheckList.Status = AgentBillCheckStatusEnum.Created;
                }

                //if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.NotifiedBillOwner)
                //{
                //    _agnetBillCheckList.Status = AgentBillCheckStatusEnum.NotifiedBillAndRegenerate;
                //}

                _agnetBillCheckList.ID = singleResult.GetValue<Guid>("ID");
                _agnetBillCheckList.No = singleResult.GetValue<String>("No");
                _agnetBillCheckList.UpdateDate = singleResult.GetValue<DateTime?>("UpdateDate");

                //绑定明细
                BindDetailList();

                if (Saved != null)
                {
                    Saved(_agnetBillCheckList);
                }

                bsAgentCheckBill.ResetBindings(false);

                SetToolInfo();

                return true;
            }
            catch (Exception ex)
            {
                
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }
        
        #endregion

        #region 发起对账
        /// <summary>
        /// 发起对账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barStartCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_agnetBillCheckList.Status != AgentBillCheckStatusEnum.Created && _agnetBillCheckList.Status != AgentBillCheckStatusEnum.NotifiedBillOwner)
                {
                    //不是已创建的，无法发起对账
                    return;
                }

                if (_agnetBillCheckList.LaunchUserID != LocalData.UserInfo.LoginID)
                {
                    //当前用户不是创建人
                    return;
                }

                if (detailIsNull)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1107290015"));
                    return;
                }

                //状态为"已通知修改帐单",则先自动重新生成核对明细，然后发起对账
                if (_agnetBillCheckList.Status == AgentBillCheckStatusEnum.NotifiedBillOwner)
                {
                    if (!ValidateData())
                    {
                        //验证信息没有通过,返回
                        return;
                    }
                    try
                    {
                        SingleResult singleResult = FinanceService.SaveAgentBillCheck(
                                         _agnetBillCheckList.ID,
                                         _agnetBillCheckList.LaunchCompanyID,
                                         _agnetBillCheckList.LaunchUserID,
                                         _agnetBillCheckList.CheckCompanyID,
                                         _agnetBillCheckList.CheckUserID,
                                         _agnetBillCheckList.OperationTypes,
                                         _agnetBillCheckList.EndingETD,
                                         _agnetBillCheckList.CreateID,
                                         _agnetBillCheckList.UpdateDate,
                                         LocalData.IsEnglish);

                        _agnetBillCheckList.OperTexts = cmbOperType.Text;

                        _agnetBillCheckList.ID = singleResult.GetValue<Guid>("ID");
                        _agnetBillCheckList.No = singleResult.GetValue<String>("No");
                        _agnetBillCheckList.UpdateDate = singleResult.GetValue<DateTime?>("UpdateDate");

                        //绑定明细
                        BindDetailList();
                        bsAgentCheckBill.ResetBindings(false);
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                    }
                }

                try
                {
                    SingleResult restult = FinanceService.ChangeAgentBillCheckStatus(_agnetBillCheckList.ID, AgentBillCheckStatusEnum.StartCheck, string.Empty, LocalData.UserInfo.LoginID, _agnetBillCheckList.UpdateDate, LocalData.IsEnglish);
                    _agnetBillCheckList.UpdateDate = restult.GetValue<DateTime?>("Updatedate");
                    _agnetBillCheckList.Status = AgentBillCheckStatusEnum.StartCheck;

                    AlterSave();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "1108250001"));

                    //刷新工具栏与状态
                    SetToolInfo();
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Agent Bill Checking Status Failed" : "更改代理对账单状态失败") + ex.Message);
                }
            }
        }

        #endregion

        #region 核对中
        /// <summary>
        /// 核对中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barChecking_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_agnetBillCheckList.Status != AgentBillCheckStatusEnum.StartCheck && _agnetBillCheckList.Status != AgentBillCheckStatusEnum.Checking)
                {
                    //状态不等于已发起或已核对，则返回
                    return;
                }
                if (_agnetBillCheckList.CheckUserID != LocalData.UserInfo.LoginID)
                {
                    //当前用户不是核对人
                    return;
                }

                if (detailIsNull)
                {
                    return;
                }

                try
                {
                    SingleResult restult = FinanceService.ChangeAgentBillCheckStatus(_agnetBillCheckList.ID, AgentBillCheckStatusEnum.Checking, string.Empty, LocalData.UserInfo.LoginID, _agnetBillCheckList.UpdateDate, LocalData.IsEnglish);
                    _agnetBillCheckList.UpdateDate = restult.GetValue<DateTime?>("UpdateDate");

                    _agnetBillCheckList.Status = AgentBillCheckStatusEnum.Checking;

                    AlterSave();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "1108250002"));

                    //刷新工具栏与状态
                    SetToolInfo();
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Agent Bill Checking Status Failed" : "更改代理对账单状态失败") + ex.Message);
                }
            }
        }
        #endregion

        #region 通知修改账单 
        /// <summary>
        /// 通知修改账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barNotifiedBillOwner_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_agnetBillCheckList.Status != AgentBillCheckStatusEnum.NotifiedBillOwner && _agnetBillCheckList.Status != AgentBillCheckStatusEnum.Checking)
                {
                    //状态不等于已发起或已核对，则返回
                    return;
                }

                if (_agnetBillCheckList.CheckUserID != LocalData.UserInfo.LoginID)
                {
                    //当前用户不是核对人
                    return;
                }

                if (detailIsNull)
                {
                    //数据为空
                    return;
                }
                try
                {
                    SingleResult restult = FinanceService.ChangeAgentBillCheckStatus(_agnetBillCheckList.ID, AgentBillCheckStatusEnum.NotifiedBillOwner, string.Empty, LocalData.UserInfo.LoginID, _agnetBillCheckList.UpdateDate, LocalData.IsEnglish);
                    _agnetBillCheckList.UpdateDate = restult.GetValue<DateTime?>("UpdateDate");
                    _agnetBillCheckList.Status = AgentBillCheckStatusEnum.NotifiedBillOwner;

                    AlterSave();


                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "1108250002"));

                    //刷新工具栏与状态
                    SetToolInfo();
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Agent Bill Checking Status Failed" : "更改代理对账单状态失败") + ex.Message);
                }
            }
        }
        #endregion

        #region 完成对账
        /// <summary>
        /// 已完成对账
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barCompleted_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //状态不是对账中
                if (_agnetBillCheckList.Status != AgentBillCheckStatusEnum.Checking)
                {
                    return;
                }
                //当前用户不是核对人
                if (_agnetBillCheckList.CheckUserID != LocalData.UserInfo.LoginID)
                {
                    return;
                }
                //没有明细
                if (detailIsNull)
                {
                    return;
                }
                //明细有错误
                if (isError)
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1107290016"));
                    return;
                }

                try
                {
                    SingleResult restult = FinanceService.ChangeAgentBillCheckStatus(_agnetBillCheckList.ID, AgentBillCheckStatusEnum.Completed, string.Empty, LocalData.UserInfo.LoginID, _agnetBillCheckList.UpdateDate, LocalData.IsEnglish);
                    _agnetBillCheckList.UpdateDate = restult.GetValue<DateTime?>("UpdateDate");
                    _agnetBillCheckList.Status = AgentBillCheckStatusEnum.Completed;

                    AlterSave();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "1108250002"));

                    //刷新工具栏与状态
                    SetToolInfo();
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Agent Bill Checking Status Failed" : "更改代理对账单状态失败") + ex.Message);
                }
            }
        }
        #endregion

        #region 销账 
        /// <summary>
        /// 销账 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barWriteOff_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (DetailList == null || DetailList.Count == 0)
                {
                    return;
                }
                int i = (from d in DetailList group d by d.CurrencyName into g select g.Key).Count();

                Guid companyID = Guid.Empty;
                Guid customerID = Guid.Empty;
                string companyName = string.Empty;
                string customerName = string.Empty;
                FeeWay feeWay = FeeWay.None;
                List<WriteOffBill> billFeeList = new List<WriteOffBill>();

                #region 销账模式
                WriteOffType writeOffType = WriteOffType.Single;
                if (i > 1)
                {
                    string CurrencyTitle = LocalData.IsEnglish ? "Set WriteOff Type" : "设置销账模式";
                    UCWriteOffCurrencyType writeOffCurrencyType = Workitem.Items.AddNew<UCWriteOffCurrencyType>();
                    if (PartLoader.ShowDialog(writeOffCurrencyType, CurrencyTitle) == DialogResult.OK)
                    {
                        writeOffType = writeOffCurrencyType.writeOffType;
                    }
                    else
                    {
                        return;
                    }
                }
                #endregion

                string title = string.Empty;
                Dictionary<string, object> dicList = new Dictionary<string, object>();
                List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
                var findLaunch = userCompanyList.Where(p => p.ID == _agnetBillCheckList.LaunchCompanyID);
                var findCheck = userCompanyList.Where(p => p.ID == _agnetBillCheckList.CheckCompanyID);

                //if(_agnetBillCheckList.LaunchCompanyID==LocalData.UserInfo.DefaultCompanyID)
                if (findLaunch.Count() > 0)
                {
                    #region 当前用户是发起公司的

                    #region 收付款类型
                    decimal launchBalance = (from d in DetailList select d.LaunchBalance).Sum();
                    if (launchBalance > 0)
                    {
                        feeWay = FeeWay.AR;
                        title = LocalData.IsEnglish ? "Collection" : "收款";
                    }
                    else
                    {
                        feeWay = FeeWay.AP;
                        title = LocalData.IsEnglish ? "Payment" : "付款";
                    }
                    #endregion

                    #region 公司
                    ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_agnetBillCheckList.CheckCompanyID);
                    if (configureInfo != null)
                    {
                        customerID = configureInfo.CustomerID;
                        customerName = configureInfo.CustomerName;
                    }

                    #endregion

                    #region 数据源
                    billFeeList = FinanceService.GetBillFeeByAgentBillCheck(_agnetBillCheckList.ID, 1, LocalData.IsEnglish);
                    #endregion

                    companyID = _agnetBillCheckList.LaunchCompanyID;
                    companyName = _agnetBillCheckList.LaunchCompanyName;

                    #endregion
                }
                //else if (_agnetBillCheckList.CheckCompanyID == LocalData.UserInfo.DefaultCompanyID)
                else if (findCheck.Count() > 0)
                {
                    #region 当前用户是核对公司的

                    #region 收付款类型
                    decimal launchBalance = (from d in DetailList select d.CheckBalance).Sum();
                    if (launchBalance > 0)
                    {
                        feeWay = FeeWay.AR;
                        title = LocalData.IsEnglish ? "Collection" : "收款";
                    }
                    else
                    {
                        feeWay = FeeWay.AP;
                        title = LocalData.IsEnglish ? "Payment" : "付款";
                    }
                    #endregion

                    #region 公司
                    ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_agnetBillCheckList.LaunchCompanyID);
                    if (configureInfo != null)
                    {
                        customerID = configureInfo.CustomerID;
                        customerName = configureInfo.CustomerName;
                    }

                    #endregion

                    #region 数据源
                    billFeeList = FinanceService.GetBillFeeByAgentBillCheck(_agnetBillCheckList.ID, 2, LocalData.IsEnglish);
                    #endregion

                    companyID = _agnetBillCheckList.CheckCompanyID;
                    companyName = _agnetBillCheckList.CheckCompanyName;

                    #endregion
                }
                else
                {
                    return;
                }

                #region 币种&销账类型
                List<Guid> currencyIDList = new List<Guid>();
                if (writeOffType == WriteOffType.Single)
                {
                    currencyIDList.Add(DetailList[0].CurrencyID);
                }
                else
                {
                    currencyIDList = (from d in DetailList group d by d.CurrencyID into g select g.Key).ToList();
                }

                #endregion

                dicList.Add("CompanyID", companyID);
                dicList.Add("CompanyName", companyName);
                dicList.Add("CustomerID", customerID);
                dicList.Add("CustomerName", customerName);
                dicList.Add("DataList", billFeeList);
                dicList.Add("FeeWay", feeWay);
                dicList.Add("CurrencyIDList", currencyIDList);
                dicList.Add("WriteOffType", writeOffType);


                FinanceClientService.ShowWriteOffEditor(title, dicList, ClientConstants.MainWorkspace);

            }
        }
        #endregion

        #region 打印

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_agnetBillCheckList == null || DetailList == null || DetailList.Count == 0) return;
                if (_agnetBillCheckList.IsDirty && barSave.Enabled)
                {
                    XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }

                try
                {
                    AgentBillCheckingReportData reportData = new AgentBillCheckingReportData();
                    reportData.BaseReportData = new AgentBillCheckingBaseReportData();
                    reportData.BaseReportData.PrintDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified); ;
                    reportData.BaseReportData.LaunchCompanyName = _agnetBillCheckList.LaunchCompanyName;
                    reportData.BaseReportData.LaunchUserName = _agnetBillCheckList.LaunchUserName;
                    reportData.BaseReportData.CheckCompanyName = _agnetBillCheckList.CheckCompanyName;
                    reportData.BaseReportData.CheckUserName = _agnetBillCheckList.CheckUserName;
                    reportData.BaseReportData.OperationTypes = _agnetBillCheckList.OperTexts;
                    reportData.BaseReportData.EndingETD = _agnetBillCheckList.EndingETD;

                    reportData.DetailList = new List<AgentBillCheckDetailReportData>();
                    decimal totalLaunchDebit = 0.0M;
                    decimal totalLaunchCredit = 0.0M;
                    decimal totalLaunchBalance = 0.0M;
                    decimal totalGap = 0.0M;
                    decimal totalCheckBalance = 0.0M;
                    decimal totalCheckDebit = 0.0M;
                    decimal totalCheckCredit = 0.0M;

                    foreach (var detailItem in DetailList)
                    {
                        AgentBillCheckDetailReportData detailReportItem = new AgentBillCheckDetailReportData();
                        detailReportItem.ETD = detailItem.ETD;
                        detailReportItem.BLNO = detailItem.BLNO;
                        detailReportItem.CurrencyName = detailItem.CurrencyName;
                        detailReportItem.LaunchBillNOs = detailItem.LaunchBillNOs;
                        detailReportItem.LaunchDebit = detailItem.LaunchDebit.ToString();
                        detailReportItem.LaunchCredit = detailItem.LaunchCredit.ToString();
                        detailReportItem.LaunchBalance = detailItem.LaunchBalance.ToString();
                        detailReportItem.Gap = detailItem.Gap.ToString();
                        detailReportItem.CheckBalance = detailItem.CheckBalance.ToString();
                        detailReportItem.CheckDebit = detailItem.CheckDebit.ToString();
                        detailReportItem.CheckCredit = detailItem.CheckCredit.ToString();
                        detailReportItem.CheckBillNOs = detailItem.CheckBillNOs;
                        reportData.DetailList.Add(detailReportItem);

                        totalLaunchDebit += detailItem.LaunchDebit;
                        totalLaunchCredit += detailItem.LaunchCredit;
                        totalLaunchBalance += detailItem.LaunchBalance;
                        totalGap += detailItem.Gap;
                        totalCheckBalance += detailItem.CheckBalance;
                        totalCheckDebit += detailItem.CheckDebit;
                        totalCheckCredit += detailItem.CheckCredit;
                    }

                    reportData.BaseReportData.TotalLaunchDebit = totalLaunchDebit.ToString();
                    reportData.BaseReportData.TotalLaunchCredit = totalLaunchCredit.ToString();
                    reportData.BaseReportData.TotalLaunchBalance = totalLaunchBalance.ToString();
                    reportData.BaseReportData.TotalGap = totalGap.ToString();
                    reportData.BaseReportData.TotalCheckBalance = totalCheckBalance.ToString();
                    reportData.BaseReportData.TotalCheckDebit = totalCheckDebit.ToString();
                    reportData.BaseReportData.TotalCheckCredit = totalCheckCredit.ToString();

                    IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Agent BillChecking" : "打印代理对账单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                    string fileName = Application.StartupPath + "\\Reports\\FAM\\";

                    if (LocalData.IsEnglish)
                        fileName += "AgentBillChecking_EN.frx";
                    else
                        fileName += "AgentBillChecking_CN.frx";

                    Dictionary<string, object> reportSource = new Dictionary<string, object>();
                    reportSource.Add("AgentBillCheckingBaseReportData", reportData.BaseReportData);
                    reportSource.Add("AgentBillCheckingDetailReportData", reportData.DetailList);
                    viewer.BindData(fileName, reportSource, null);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                }
            }
        }

        #endregion

        #region 关闭
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_agnetBillCheckList.ID == Guid.Empty)
                {
                    return;
                }
                //绑定主表信息
                _agnetBillCheckList = FinanceService.GetAgnetBillCheckInfo(_agnetBillCheckList.ID, LocalData.IsEnglish);
                _agnetBillCheckList.OperValues = _agnetBillCheckList.OperValues.Replace(GlobalConstants.DividedSymbol, ", ");

                //绑定明细
                BindDetailList();

                bsAgentCheckBill.DataSource = _agnetBillCheckList;
                bsAgentCheckBill.ResetBindings(false);

                _agnetBillCheckList.IsDirty = false;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Refresh Successfully" : "刷新成功");
            }
        }
        #endregion

        #region 保存时更新数据
        private void AlterSave()
        {
            if (Saved != null)
            {
                Saved(_agnetBillCheckList);
            }
        }
        #endregion

        /// <summary>
        /// 明细是否为空或者没有数据
        /// </summary>
        private bool detailIsNull
        {
            get
            {
                if (DetailList == null || DetailList.Count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            _agnetBillCheckList.EndEdit();
            bsAgentCheckBill.EndEdit();

            if (!_agnetBillCheckList.Validate())
            {
                return false;
            }
            return true;
        }

        #endregion

        #region GAP大于0
        private void ckbGap_CheckedChanged(object sender, EventArgs e)
        {
            if (DetailList == null)
            {
                return;
            }
            if (ckbGap.Checked)
            {
                CurrentDetailist = (from d in DetailList where d.Gap != 0 select d).ToList();
            }
            else
            {
                CurrentDetailist = DetailList;
            }
        }

        #endregion


    }
}
