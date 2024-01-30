using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.XtraEditors;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.ReleaseBL
{
    /// <summary>
    /// 放单列表
    /// </summary>
    [ToolboxItem(false)]
    public partial class ReleaseBLSearchPart : BaseSearchPart
    {
        /// <summary>
        /// 选择的方案发生改变时
        /// </summary>
        public event SelectedHandler ProgramSelectedChanged;

        /// <summary>
        /// 缓存的查询参数
        /// </summary>
        ReleaseBLParameter searchParameter = new ReleaseBLParameter();

        #region 查询成员

        public ReleaseBLSearchStatue releaseBLSearchStatue { get { return (ReleaseBLSearchStatue)rdoRelease.SelectedIndex; } }
        public ReleaseRCSearchStatue releaseRCSearchStatue { get { return (ReleaseRCSearchStatue)rdoReleaseRC.SelectedIndex; } }
        public ApplyReleaseSearchStatue applyReleaseSearchStatue { get { return (ApplyReleaseSearchStatue)rdoApply.SelectedIndex; } }
        public ReceiveRCSearchStatue receiveRCSearchStatue { get { return (ReceiveRCSearchStatue)rdoReceive.SelectedIndex; } }

        public ReleaseType? ReleaseTypeValue
        {
            get
            {
                if (cmbReleaseType.EditValue != null && cmbReleaseType.EditValue != DBNull.Value) return (ReleaseType)cmbReleaseType.EditValue;
                else return null;
            }
        }

        public DateTime? ETDFrom
        {
            get
            {
                return dateMonthControlETD.From;
            }
        }

        public DateTime? ETDTo
        {
            get
            {
                return dateMonthControlETD.To;
            }
        }

        public DateTime? ETAFrom
        {
            get
            {
                return dateMonthControlETA.From;
            }
        }
        public DateTime? ETATo
        {
            get
            {
                return dateMonthControlETA.To;
            }
        }

        public DateTime? ReleaseFrom
        {
            get
            {
                return dateMonthControlRelease.From;
            }
        }

        public DateTime? ReleaseTo
        {
            get
            {
                return dateMonthControlRelease.To;
            }
        }


        public DateTime? ApplyFrom
        {
            get
            {
                return null;
            }
        }

        public DateTime? ApplyTo
        {
            get
            {
                return null;
            }
        }

        public DateTime? AcceptFrom
        {
            get
            {
                return null;
            }
        }

        public DateTime? AcceptTo
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// 口岸
        /// </summary>
        public List<Guid> CompanyIds
        {
            get
            {
                return (from CheckedListBoxItem item in chkcmbCompany.Properties.Items where item.CheckState == CheckState.Checked select new Guid(item.Value.ToString())).ToList();
            }
        }
        #endregion

        #region 服务注入
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 财务服务
        /// </summary>
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        #endregion
        
        #region 初始化

        public ReleaseBLSearchPart()
        {
            InitializeComponent();
            Disposed += delegate {
                RemoveKeyDownHandle();
                ProgramSelectedChanged = null;
                OnSearched = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

       

        private void SetOverSea()
        {
            if (LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.FAM_OverSeaMBL))
            {
                chkIsOverSea.Enabled = true;
            }
            else 
            {
                chkIsOverSea.Enabled = chkIsOverSea.Checked = false;
            }
        }

        private void SetCmbSource()
        {
            #region 放单类型
            List<EnumHelper.ListItem<ReleaseType>> releaseTypes = EnumHelper.GetEnumValues<ReleaseType>(LocalData.IsEnglish);
            cmbReleaseType.Properties.BeginUpdate();
            cmbReleaseType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", DBNull.Value));
            foreach (var item in releaseTypes)
            {
                if (item.Value == 0  || item.Value == ReleaseType.OconvT || item.Value == ReleaseType.TconvO) continue;
                cmbReleaseType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbReleaseType.SelectedIndex = 0;
            cmbReleaseType.Properties.EndUpdate();
            #endregion

            cmbprogram.Properties.BeginUpdate();
            List<EnumHelper.ListItem<ReleaseProgram>> releasePrograms = EnumHelper.GetEnumValues<ReleaseProgram>(LocalData.IsEnglish);
            foreach (var item in releasePrograms)
            {
                cmbprogram.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbprogram.SelectedIndex = 0;
            cmbprogram.Properties.EndUpdate();
        }

        private void SetKeyDownToSearch()
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        

        #endregion

        

        #region ISearchPart 成员

        


        public override event SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public override void RaiseSearched(object data)
        {
            DataPageInfo dataPageInfo = data as DataPageInfo;
            searchParameter.DataPageInfo = dataPageInfo;
            if (OnSearched != null)
                OnSearched(this, GetData());
        }

        public override object GetData()
        {
            try
            {
                ReleasePageList list = FinanceService.GetReleaseBLListByList(searchParameter.CompanyIds
                                                                        //, searchParameter.releaseBLState
                                                                        , searchParameter.releaseBLSearchStatue
                                                                        , searchParameter.releaseRCSearchStatue
                                                                        , searchParameter.applyReleaseSearchStatue
                                                                        , searchParameter.receiveRCSearchStatue
                                                                        , searchParameter.ReceiveOBL
                                                                        , searchParameter.releaseType
                                                                        , searchParameter.blNo
                                                                        , searchParameter.soNo
                                                                        , searchParameter.ctnNo
                                                                        , searchParameter.operationNo
                                                                        , searchParameter.customerName
                                                                        , searchParameter.vessel
                                                                        , searchParameter.voyageNo
                                                                        , searchParameter.ChangedReleaseType
                                                                        , searchParameter.applyBeginTime
                                                                        , searchParameter.applyEndTime
                                                                        , searchParameter.releaseBeginTime
                                                                        , searchParameter.releaseEndTime
                                                                        , searchParameter.etdBeginTime
                                                                        , searchParameter.etdEndTime
                                                                        , searchParameter.acceptBeginDate
                                                                        , searchParameter.acceptEndTime
                                                                        , searchParameter.etaBeginTime
                                                                        , searchParameter.etaEndTime
                                                                        , searchParameter.IsMonthlyPay
                                                                        , searchParameter.IsTrustRelease
                                                                        , searchParameter.IsSWBRelease
                                                                        ,searchParameter.IsOverSeaMBL
                                                                        ,searchParameter.IsAirandOther
                                                                        , searchParameter.DataPageInfo);
                List<ReleaseBLList> releaseList = new List<ReleaseBLList>();
                if (list != null) releaseList = list.GetList<ReleaseBLList>();
                foreach (var item in releaseList)
                {
                    if (!string.IsNullOrEmpty(item.TelexNo))
                    {
                        item.TelexNo = item.TelexNo.Substring(item.TelexNo.Length - 20);
                    }
                }

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        #endregion

        #region Event
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                //绑定公司
                FAMUtility.BindCheckComboBoxByCompany(chkcmbCompany);

                FAMUtility.SetCustomerTextEditNullValuePrompt(new List<TextEdit> { txtCustomer });

                FAMUtility.SetTextEditNullValuePrompt(new List<TextEdit> { txtVessel }
                    , LocalData.IsEnglish ? "Please Input Code or Name" : "请输入代码名称.");

                SetCmbSource();
                SetKeyDownToSearch();
                SetOverSea();

                dateMonthControlRelease.IsEngish = LocalData.IsEnglish;
                dateMonthControlETD.IsEngish = LocalData.IsEnglish;

            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    //LocalCommonServices.PermissionService.HaveActionPermission();
                    searchParameter.CompanyIds = CompanyIds.ToArray();
                    //searchParameter.releaseBLState = ReleaseBLState;
                    searchParameter.releaseType = ReleaseTypeValue;
                    searchParameter.blNo = txtBLNO.Text.Trim();
                    searchParameter.soNo = txtSoNo.Text.Trim();
                    searchParameter.ctnNo = txtCtnNo.Text.Trim();
                    searchParameter.operationNo = txtOperationNo.Text.Trim();
                    searchParameter.customerName = txtCustomer.Text.Trim();
                    searchParameter.vessel = txtVessel.Text.Trim();
                    searchParameter.voyageNo = txtVoyageNo.Text.Trim();

                    if (chkIsChangedToTel.Checked && chkIsChangedToOrg.Checked == false)
                        searchParameter.ChangedReleaseType = 1;
                    else if (chkIsChangedToOrg.Checked && chkIsChangedToTel.Checked == false)
                        searchParameter.ChangedReleaseType = 2;
                    else
                        searchParameter.ChangedReleaseType = 0;

                    searchParameter.IsMonthlyPay = chkIsMonthlyPay.Checked;
                    searchParameter.IsTrustRelease = chkIsTrustRelease.Checked;
                    searchParameter.IsSWBRelease = chkIsSWBRelease.Checked;

                    searchParameter.IsOverSeaMBL = chkIsOverSea.Checked;
                    searchParameter.IsAirandOther = chkOther.Checked;

                    searchParameter.applyBeginTime = ApplyFrom;
                    searchParameter.applyEndTime = ApplyTo;
                    searchParameter.acceptBeginDate = AcceptFrom;
                    searchParameter.acceptEndTime = AcceptTo;
                    searchParameter.releaseBeginTime = ReleaseFrom;
                    searchParameter.releaseEndTime = ReleaseTo;
                    searchParameter.etdBeginTime = ETDFrom;
                    searchParameter.etdEndTime = ETDTo;
                    searchParameter.etaBeginTime = ETAFrom;
                    searchParameter.etaEndTime = ETATo;
                    searchParameter.releaseBLSearchStatue = releaseBLSearchStatue;
                    searchParameter.receiveRCSearchStatue = receiveRCSearchStatue;
                    searchParameter.applyReleaseSearchStatue = applyReleaseSearchStatue;
                    searchParameter.releaseRCSearchStatue = releaseRCSearchStatue;
                    searchParameter.ReceiveOBL = rdoOBLRec.SelectedIndex;

                    searchParameter.DataPageInfo.PageSize = int.Parse(sePageSize.Value.ToString());
                    searchParameter.DataPageInfo.CurrentPage = 1;
                    if (string.IsNullOrEmpty(searchParameter.DataPageInfo.SortByName))
                    {
                        searchParameter.DataPageInfo.SortByName = "CreateDate";
                        searchParameter.DataPageInfo.SortOrderType = SortOrderType.Desc;
                    }

                    if (OnSearched != null)
                    {
                        ReleasePageList list = GetData() as ReleasePageList;
                        if (list != null && list.DataPageInfo != null)
                        {
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "total search " + list.DataPageInfo.TotalCount.ToString() + " data." : "总共查询到 "
                                                        + list.DataPageInfo.TotalCount.ToString() + " 条数据.");
                        }
                        OnSearched(this, list);
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),ex.Message);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBaseInfo.Controls)
            {
                if (item is LWImageComboBoxEdit || item is ImageComboBoxEdit)
                {
                    (item as ImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is TextEdit
                         && (item is SpinEdit) == false
                         && item.Enabled
                         && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;

                else if (item is CheckEdit) (item as CheckEdit).Checked = false;
            }
        }

        #endregion

        private void cmbReleaseType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbReleaseType.EditValue != null && cmbReleaseType.EditValue != DBNull.Value)
            {
                ReleaseType type = (ReleaseType)cmbReleaseType.EditValue;
                if (type == ReleaseType.Telex)
                {
                    chkIsChangedToTel.Enabled = false;
                    chkIsChangedToOrg.Enabled = true;
                }

                else if (type == ReleaseType.Original)
                {
                    chkIsChangedToTel.Enabled = true;
                    chkIsChangedToOrg.Enabled = false;
                }

                else
                {
                    chkIsChangedToTel.Enabled = false;
                    chkIsChangedToOrg.Enabled = false;
                }

            }
            else {
                chkIsChangedToTel.Enabled = true;
                chkIsChangedToOrg.Enabled = true;
            }


            chkIsChangedToOrg.Checked = chkIsChangedToTel.Checked = false;
        }

        private void chkIsChangedToTel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsChangedToTel.Checked)
                chkIsChangedToOrg.Checked = false;
        }

        private void chkIsChangedToOrg_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsChangedToOrg.Checked)
                chkIsChangedToTel.Checked = false;
        }
       
        #region 方案发生改变时
        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

            ReleaseProgram program = (ReleaseProgram)cmbprogram.SelectedIndex;

            switch (program)
            {
                case ReleaseProgram.Custom:
                    SetCustomer();
                    break;
                case ReleaseProgram.NotApliedReleaseBL:
                    SetNotApliedReleaseBL();
                    break;
                case ReleaseProgram.AppliedReleaseBL:
                    SetAppliedReleaseBL();
                    break;
                case ReleaseProgram.NotReleasedBL:
                    SetNotReleasedBL();
                    break;
                case ReleaseProgram.ReleasedBL:
                    SetReleasedBL();
                    break;
                case ReleaseProgram.AcceptedReleaseBL:
                    SetAcceptedReleaseBL();
                    break;
                case ReleaseProgram.ReleasedCargo:
                    SetReleasedCargo();
                    break;
                case ReleaseProgram.ReleasedOBL:
                    SetReceivedOBL();
                    break;
                default:
                    break;
            }

            if (ProgramSelectedChanged != null)
            {
                ProgramSelectedChanged(sender, program);
            }


        }

        /// <summary>
        /// 设置自定义方案的样式
        /// </summary>
        private void SetCustomer()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = true;
            rdoRelease.SelectedIndex = 0;

        }
        /// <summary>
        /// 设置未申请放单方案的样式
        /// </summary>
        private void SetNotApliedReleaseBL()
        {
            rdoApply.Enabled = false;
            rdoApply.SelectedIndex = 2;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = true;
            rdoRelease.SelectedIndex = 0;

            rdoOBLRec.Enabled = false;
            rdoOBLRec.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置已申请放单方案的样式
        /// </summary>
        private void SetAppliedReleaseBL()
        {
            rdoApply.Enabled = false;
            rdoApply.SelectedIndex = 1;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = true;
            rdoRelease.SelectedIndex = 0;

            rdoOBLRec.Enabled = false;
            rdoOBLRec.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置未下达放单指令方案的样式
        /// </summary>
        private void SetNotReleasedBL()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = false;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = false;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 2;

            rdoOBLRec.Enabled = false;
            rdoOBLRec.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置已下达放单指令方案的样式
        /// </summary>
        private void SetReleasedBL()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 1;

            rdoOBLRec.Enabled = false;
            rdoOBLRec.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置已签收放单指令方案的样式
        /// </summary>
        private void SetAcceptedReleaseBL()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = false;
            rdoReceive.SelectedIndex = 1;

            rdoReleaseRC.Enabled = true;
            rdoReleaseRC.SelectedIndex = 0;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 1;

            rdoOBLRec.Enabled = false;
            rdoOBLRec.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置已放货样式
        /// </summary>
        private void SetReleasedCargo()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = false;
            rdoReleaseRC.SelectedIndex = 1;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 1;

            rdoOBLRec.Enabled = false;
            rdoOBLRec.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置已收到正本样式
        /// </summary>
        private void SetReceivedOBL()
        {
            rdoApply.Enabled = true;
            rdoApply.SelectedIndex = 0;

            rdoReceive.Enabled = true;
            rdoReceive.SelectedIndex = 0;

            rdoReleaseRC.Enabled = false;
            rdoReleaseRC.SelectedIndex = 2;

            rdoRelease.Enabled = false;
            rdoRelease.SelectedIndex = 1;

            rdoOBLRec.Enabled = false;
            rdoOBLRec.SelectedIndex = 1;
        }
        #endregion
    }

    /// <summary>
    /// 查询参数实体
    /// </summary>
    public class ReleaseBLParameter
    {
        public Guid[] CompanyIds { get; set; }
        public ReleaseBLParameter() { DataPageInfo = new DataPageInfo(); }
        public ReleaseType? releaseType { get; set; }
        public string blNo { get; set; }
        public string soNo { get; set; }
        public string ctnNo { get; set; }
        public string operationNo { get; set; }
        public string customerName { get; set; }
        public string vessel { get; set; }
        public string voyageNo { get; set; }
        public int ChangedReleaseType { get; set; }

        public DateTime? applyBeginTime { get; set; }
        public DateTime? applyEndTime { get; set; }

        public DateTime? acceptBeginDate { get; set; }
        public DateTime? acceptEndTime { get; set; }
        public DateTime? releaseBeginTime { get; set; }
        public DateTime? releaseEndTime { get; set; }
        public DateTime? etdBeginTime { get; set; }
        public DateTime? etdEndTime { get; set; }
        public DateTime? etaBeginTime { get; set; }
        public DateTime? etaEndTime { get; set; }
        public DataPageInfo DataPageInfo { get; set; }
        public bool IsMonthlyPay { get; set; }
        public bool IsTrustRelease { get; set; }
        public bool IsSWBRelease { get; set; }
        public bool IsOverSeaMBL { get; set; }
        public bool IsAirandOther { get; set; }
        public ReleaseBLSearchStatue releaseBLSearchStatue { get; set; }
        public ReleaseRCSearchStatue releaseRCSearchStatue { get; set; }
        public ApplyReleaseSearchStatue applyReleaseSearchStatue { get; set; }
        public ReceiveRCSearchStatue receiveRCSearchStatue { get; set; }
        public int ReceiveOBL { get; set; }
        
    }
}
