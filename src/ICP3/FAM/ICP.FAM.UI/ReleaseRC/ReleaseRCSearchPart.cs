using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.XtraEditors;
using ICP.Sys.ServiceInterface;

namespace ICP.FAM.UI.ReleaseRC
{
    [ToolboxItem(false)]
    public partial class ReleaseRCSearchPart : BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        [ServiceDependency]
        public IUserService userService { get; set; }

        #endregion

        #region 初始化

        public ReleaseRCSearchPart()
        {
            InitializeComponent();
            Disposed += delegate {
                RemoveKeyDownHandle();
                OnSearched = null;
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit> { txtCustomer });

            FAMUtility.SetTextEditNullValuePrompt(new List<TextEdit> { txtVessel }
                , LocalData.IsEnglish ? "Please Input Code or Name" : "请输入代码名称.");
            if (!DesignMode)
            {
                InitControls();
                SetCmbSource();
                SetKeyDownToSearch();
            }

        }
        private void InitControls()
        {
            #region Company

            List<LocalOrganizationInfo> userCompanyList = FAMUtility.GetCompanyList();

            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);
            }
            chkcmbCompany.Properties.EndUpdate();

            #endregion
        }
        private void SetCmbSource()
        {
            #region 放单状态

            List<EnumHelper.ListItem<ReleaseRCState>> releaseStates = EnumHelper.GetEnumValues<ReleaseRCState>(LocalData.IsEnglish);
            chkcmbState.Properties.BeginUpdate();
            chkcmbState.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", DBNull.Value));
            foreach (var item in releaseStates)
            {
                if (item.Value == 0) continue;
                chkcmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            chkcmbState.SelectedIndex = 2;
            chkcmbState.Properties.EndUpdate();
            #endregion
            #region 放单类型
            List<EnumHelper.ListItem<ReleaseType>> releaseTypes = EnumHelper.GetEnumValues<ReleaseType>(LocalData.IsEnglish);
            cmbReleaseType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", DBNull.Value));
            foreach (var item in releaseTypes)
            {
                if (item.Value == 0 || item.Value == ReleaseType.OconvT || item.Value == ReleaseType.TconvO) continue;
                cmbReleaseType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbReleaseType.SelectedIndex = 0;

            #endregion
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
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }

        #endregion

        #region 查询成员

        public ReleaseRCState? ReleaseRCState
        {
            get
            {
                if (chkcmbState.EditValue != null && chkcmbState.EditValue != DBNull.Value)
                    return (ReleaseRCState)chkcmbState.EditValue;
                else return null;
            }
        }

        public ReleaseType? ReleaseTypeValue
        {
            get
            {
                if (cmbReleaseType.EditValue != null && cmbReleaseType.EditValue != DBNull.Value) return (ReleaseType)cmbReleaseType.EditValue;
                else return null;
            }
        }

        //RCFrom
        public DateTime? ReleaseFrom
        {
            get
            {
                if (rdoRelease.SelectedIndex == (int)DateType.Unknown)
                {
                    return null;
                }
                else if (rdoRelease.SelectedIndex == (int)DateType.LastMonth)
                {
                    DateTime dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(-1);
                    return new DateTime(dt.Year, dt.Month, 1);
                }
                else if (rdoRelease.SelectedIndex == (int)DateType.ThisMonth)
                {
                    return new DateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Year, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Month, 1);
                }
                else
                {
                    if (dteReleaseFrom.DateTime == DateTime.MinValue) return null;

                    return dteReleaseFrom.DateTime.Date;
                }
            }
        }

        //RCTo
        public DateTime? ReleaseTo
        {
            get
            {
                if (rdoApply.SelectedIndex == (int)DateType.Unknown)
                {
                    return null;
                }
                else if (rdoApply.SelectedIndex == (int)DateType.LastMonth)
                {
                    DateTime dt = FAMUtility.GetEndDate(new DateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Year, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Month, 1));//本月月头
                    return dt.AddDays(-1);
                }
                else if (rdoApply.SelectedIndex == (int)DateType.ThisMonth)
                {
                    DateTime dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(1);//下月月头
                    dt = FAMUtility.GetEndDate(new DateTime(dt.Year, dt.Month, 1));
                    return dt.AddDays(-1);
                }
                else
                {
                    if (dteReleaseTo.DateTime == DateTime.MinValue) return null;

                    return FAMUtility.GetEndDate(dteReleaseTo.DateTime);
                }
            }
        }
        //BLFrom
        public DateTime? ApplyFrom
        {
            get
            {
                if (rdoApply.SelectedIndex == (int)DateType.Unknown)
                {
                    return null;
                }
                else if (rdoApply.SelectedIndex == (int)DateType.LastMonth)
                {
                    DateTime dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(-1);
                    return new DateTime(dt.Year, dt.Month, 1);
                }
                else if (rdoApply.SelectedIndex == (int)DateType.ThisMonth)
                {
                    return new DateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Year, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Month, 1);
                }
                else
                {
                    if (dteApplyFrom.DateTime == DateTime.MinValue) return null;

                    return dteApplyFrom.DateTime.Date;
                }
            }
        }
        //BLTo
        public DateTime? ApplyTo
        {
            get
            {
                if (rdoApply.SelectedIndex == (int)DateType.Unknown)
                {
                    return null;
                }
                else if (rdoApply.SelectedIndex == (int)DateType.LastMonth)
                {
                    DateTime dt = FAMUtility.GetEndDate(new DateTime(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Year, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).Month, 1));//本月月头
                    return dt.AddDays(-1);
                }
                else if (rdoApply.SelectedIndex == (int)DateType.ThisMonth)
                {
                    DateTime dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddMonths(1);//下月月头
                    dt = FAMUtility.GetEndDate(new DateTime(dt.Year, dt.Month, 1));
                    return dt.AddDays(-1);
                }
                else
                {
                    if (dteApplyTo.DateTime == DateTime.MinValue) return null;

                    return FAMUtility.GetEndDate(dteApplyTo.DateTime);
                }
            }
        }
        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }
        enum DateType
        {
            Unknown,
            LastMonth,
            ThisMonth,
            Specify
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
                ReleasePageList list = FinanceService.GetReleaseRCList(searchParameter.RcCompanyIDs
                                                                        , searchParameter.releaseRCState
                                                                        , searchParameter.releaseType
                                                                        , searchParameter.blNo
                                                                        , searchParameter.ctnNo
                                                                        , searchParameter.ConsigneeName
                                                                        , searchParameter.vessel
                                                                        , searchParameter.voyageNo
                                                                        , searchParameter.releaseBeginTime
                                                                        , searchParameter.releaseEndTime
                                                                        , searchParameter.RcBeginTime
                                                                        , searchParameter.RcEndTime
                                                                        , LocalData.IsEnglish
                                                                        , searchParameter.DataPageInfo);
            //    List<ReleaseRCList> releaseList = new List<ReleaseRCList>();
            //    if (list != null) releaseList = list.GetList<ReleaseRCList>();
            //    if (releaseList != null)
            //    {
            //        foreach (var item in releaseList)
            //        {
            //            item.TelexNo = item.TelexNo.Decrypt(item.ID.ToString(), EncryptType.DES_ID);
            //        }
            //    }
                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        #endregion

        #region btn
        /// <summary>
        /// 缓存的查询参数
        /// </summary>
        ReleaseBLParameter searchParameter = new ReleaseBLParameter();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                searchParameter.RcCompanyIDs = CompanyIDs.ToArray();
                searchParameter.releaseRCState = ReleaseRCState;
                searchParameter.releaseType = ReleaseTypeValue;
                searchParameter.blNo = txtBLNO.Text.Trim();
                searchParameter.ctnNo = txtCtnNo.Text.Trim();
                //searchParameter.operationNo = txtConsignee.Text.Trim();
                //searchParameter.customerName = txtCustomer.Text.Trim();
                searchParameter.vessel = txtVessel.Text.Trim();
                searchParameter.voyageNo = txtVoyageNo.Text.Trim();

                searchParameter.RcBeginTime = ReleaseFrom;
                searchParameter.RcEndTime = ReleaseTo;

                searchParameter.releaseBeginTime = ApplyFrom;
                searchParameter.releaseEndTime = ApplyTo;
                searchParameter.ConsigneeName = txtConsignee.Text.Trim();
                //searchParameter.etdEndTime = this.ETDTo;

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
                         && item.Enabled == true
                         && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            //rdoApply.SelectedIndex = rdoETDDate.SelectedIndex = rdoRelease.SelectedIndex = 0;
        }

        private void rdoDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoRelease.SelectedIndex == 3)
                dteReleaseFrom.Enabled = dteReleaseTo.Enabled = true;
            else
                dteReleaseFrom.Enabled = dteReleaseTo.Enabled = false;
        }



        #endregion

        private void rdoApply_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoApply.SelectedIndex == 3)
                dteApplyFrom.Enabled = dteApplyTo.Enabled = true;
            else
                dteApplyFrom.Enabled = dteApplyTo.Enabled = false;
        }


    }

    /// <summary>
    /// 查询参数实体
    /// </summary>
    public class ReleaseBLParameter
    {
        public Guid[] RcCompanyIDs { get; set; }
        public ReleaseBLParameter() { DataPageInfo = new DataPageInfo(); }
        public ReleaseRCState? releaseRCState { get; set; }
        public ReleaseType? releaseType { get; set; }
        public string blNo { get; set; }
        public string ctnNo { get; set; }
        public string ConsigneeName { get; set; }
        public string vessel { get; set; }
        public string voyageNo { get; set; }

        public DateTime? RcBeginTime { get; set; }
        public DateTime? RcEndTime { get; set; }
        public DateTime? releaseBeginTime { get; set; }
        public DateTime? releaseEndTime { get; set; }
        public DataPageInfo DataPageInfo { get; set; }
    }
}
