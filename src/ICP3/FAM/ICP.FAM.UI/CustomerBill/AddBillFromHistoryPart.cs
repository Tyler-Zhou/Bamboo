using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Data;

namespace ICP.FAM.UI.CustomerBill
{
    public partial class AddBillFromHistoryPart : BaseEditPart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }


        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }


        #endregion

        public AddBillFromHistoryPart()
        {
            InitializeComponent();
            Disposed += delegate {
                gcBillList.DataSource = null;
                gcMain.DataSource = null;
                bsBillList.DataSource = null;
                bsBillList.Dispose();
                bsBusiness.DataSource = null;
                bsBusiness.Dispose();
                _companyIDs = null;
                _ConfigureInfo = null;
                _CurrencyList = null;
                _OperationCommonInfo = null;
                _operationCustomer = null;
                _RateList = null;
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }
            
            };
            //OperationCommonInfo
        }

        List<Guid> _companyIDs = new List<Guid>();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtOperationNo.KeyDown += new KeyEventHandler(txtOperationNo_KeyDown);

            //State
            List<EnumHelper.ListItem<BillState>> billStates = EnumHelper.GetEnumValues<BillState>(LocalData.IsEnglish);
            foreach (var item in billStates)
            {
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            //BillType
            List<EnumHelper.ListItem<BillType>> billTypes = EnumHelper.GetEnumValues<BillType>(LocalData.IsEnglish);
            foreach (var item in billTypes)
            {
                if (item.Value == BillType.None) continue;
                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }

            //FeeWays
            List<EnumHelper.ListItem<FeeWay>> feeWays = EnumHelper.GetEnumValues<FeeWay>(LocalData.IsEnglish);
            foreach (var item in feeWays)
            {
                if (item.Value == FeeWay.None) continue;
                cmbWay.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }

            _companyIDs = FAMUtility.GetCompanyIDList();

            DataTable dt = FinanceService.GetValidReNos(_OperationCommonInfo.OperationID);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    cmbRefNo.Properties.Items.Add(new ImageComboBoxItem(item[1].ToString(), item[0],(int)item[2]));
                }
            }
            if (cmbRefNo.Properties.Items.Count == 0)
            {
                foreach (var item in _OperationCommonInfo.Forms)
                {
                    cmbRefNo.Properties.Items.Add(new ImageComboBoxItem(item.No, item.ID,(int)item.Type ));
                }
            }

            if (cmbRefNo.Properties.Items.Count>0)
            {
                cmbRefNo.SelectedIndex = 0;
            }
                   
        }

        #region gvEvent

        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 ) return;

            bsBillList.DataSource = typeof(BillInfo);
            bsBillList.ResetBindings(false);

            BusinessList list = bsBusiness.Current as BusinessList;
            ShowFeeByBusiness(list);

        }

        private void ShowFeeByBusiness(BusinessList list)
        {
            if (list == null) return;

            List<BillInfo> bills = FinanceService.GetBillInfos(list.ID);
            if (bills == null || bills.Count == 0)
            {
                bsBillList.DataSource = typeof(BillInfo);
                bsBillList.ResetBindings(false);
            }
            else
            {
                foreach (var item in bills)
                {
                    item.CurrencyAmounts = null;
                    item.CurrencyRates = null;
                }

                bsBillList.DataSource = bills;
                bsBillList.ResetBindings(false);
                gvBillList.ExpandAllGroups();

                //SetBusinessVisible(false);
            }
            groupFees.Text = list.OperationNO;
        }

        private void gvBillList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column.FieldName != colSelected.FieldName) return;

            GridHitInfo  hitInfo = gvBillList.CalcHitInfo(e.X, e.Y);
            if (hitInfo != null && hitInfo.HitTest != GridHitTest.RowCell) return;

            BillInfo billinfo = bsBillList.Current as BillInfo;
            billinfo.Selected = !billinfo.Selected;
            foreach (var item in billinfo.Fees)
            {
                item.Selected = billinfo.Selected;
            }
            bsBillList.ResetBindings(false);
            for (int i = 1; i < gcBillList.Views.Count; i++)
            {
                gcBillList.Views[i].RefreshData();
            }

        }

        private void gvFee_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column.FieldName != colSelected1.FieldName) return;

            List<BillInfo> source = bsBillList.DataSource as List<BillInfo>;
            if (source == null || source.Count == 0) return;

            BaseView currentView = gcBillList.GetViewAt(new Point(e.X, e.Y));
            if (currentView == null) return;

            ChargeList fee = currentView.GetRow(e.RowHandle) as ChargeList;
            if (fee == null) return;

            BillInfo billinfo = source.Find(delegate(BillInfo item) { return item.ID == fee.BillID; });
            if (billinfo == null) return;

            fee.Selected = !fee.Selected;
            if (fee.Selected)
            {
                billinfo.Selected = true;
            }
            else
            {
                bool selected = false;
                foreach (var item in billinfo.Fees)
                {
                    if (item.Selected) selected = true;
                }
                billinfo.Selected = selected;
            }

            bsBillList.ResetBindings(false);
            currentView.RefreshData();
            gvBillList.RefreshData();
        }

        #endregion

        #region Button

        void txtOperationNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            btnSearchOperation.PerformClick();
        }

        private void btnSearchOperation_Click(object sender, EventArgs e)
        {
            if (txtOperationNo.Text.IsNullOrEmpty()) return;

            PageList business=
                FinanceService.GetBusinessListByList(new Guid[] { _OperationCommonInfo.CompanyID }, txtOperationNo.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                                       null, null, true, _OperationCommonInfo.OperationType, null, new DataPageInfo { PageSize = 10, SortByName = "CreateDate", SortOrderType = SortOrderType.Desc });
            List<BusinessList> list = new List<BusinessList>();
            if (business != null) list = business.GetList<BusinessList>();
            if (business != null && list.Count > 0)
            {
                BusinessList tager = list.Find(delegate(BusinessList item) { return item.ID == _OperationCommonInfo.OperationID; });
                if (tager != null) list.Remove(tager);
            }

            if (business == null || list.Count == 0)
            {

                bsBusiness.DataSource = typeof(BusinessList);
                bsBusiness.ResetBindings(false);
                bsBillList.DataSource = typeof(BillInfo);
                bsBillList.ResetBindings(false);

                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),"找不到记录.");
                return;
            }

            bsBusiness.DataSource = list;
            bsBusiness.ResetBindings(false);

            if (list.Count != 0)
                ShowFeeByBusiness(list[0]);
            //else
            //    SetBusinessVisible(true);
        }

        private void btnRecently_Click(object sender, EventArgs e)
        {

            PageList business =
                FinanceService.GetBusinessListByList(new Guid[]{_OperationCommonInfo .CompanyID}
                                            , string.Empty
                                            , string.Empty, string.Empty
                                            , _operationCustomer.EName
                                            , string.Empty, string.Empty
                                            , null, null,true,_OperationCommonInfo.OperationType
                                            , null
                                            , new DataPageInfo { PageSize = 10, SortByName = "CreateDate", SortOrderType = SortOrderType.Desc });
            List<BusinessList> list = new List<BusinessList>();
            if (business != null) list = business.GetList<BusinessList>();

            if (list != null && list.Count > 0)
            {
                BusinessList tager = list.Find(delegate(BusinessList item) { return item.ID == _OperationCommonInfo.OperationID; });
                if (tager != null) list.Remove(tager);
            }

            if (list == null || list.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "找不到记录.");
                return;
            }

            bsBusiness.DataSource = list;
            bsBusiness.ResetBindings(false);

            if (list != null && list.Count != 0)
                ShowFeeByBusiness(list[0]);
            //else
            //    SetBusinessVisible(true);
        }

        private void SetBusinessVisible(bool p)
        {
            isShow = p;
            if (isShow)
            {
                groupBusiness.Height = 220;
                panelBusiness.Height = 160;
                btnHideBusinessPanel.Text = LocalData.IsEnglish ? "Hide Business Panel" : "隐藏业务面板";
            }
            else
            {
                groupBusiness.Height = 60;
                panelBusiness.Height = 0;
                btnHideBusinessPanel.Text = LocalData.IsEnglish ? "Show Business Panel" : "显示业务面板";
            }
        }

        public List<BillInfo> SelectedBills = null;

        private void btnOK_Click(object sender, EventArgs e)
        {
            Validate();
            bsBillList.EndEdit();

            #region 获取已选列表
            List<BillInfo> billinfos = bsBillList.DataSource as List<BillInfo>;
            

            List<BillInfo> selectedBills = new List<BillInfo>();

            if (billinfos != null && billinfos.Count > 0)
            {
                foreach (var item in billinfos)
                {
                    if (item.Selected == false) continue;

                    BillInfo billinfo = FAMUtility.Clone<BillInfo>(item);
                    billinfo.Fees = new List<ChargeList>();
                    foreach (var fee in item.Fees)
                    {
                        if(fee.Selected)billinfo.Fees.Add(fee);
                    }
                    selectedBills.Add(billinfo);
                }
            }

            if (selectedBills.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "未选择任何数据.");
                return;
            }

            #endregion

            if (XtraMessageBox.Show("确定开始创建账单吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            #region

            List<BillInfo> infos = selectedBills;
            foreach (var item in infos)
            {
                int trem = (item.DueDate - item.AccountDate).Days;
                item.AccountDate = _OperationCommonInfo.OperationDate < _ConfigureInfo.ChargingClosingdate ? DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date : _OperationCommonInfo.OperationDate;
                item.DueDate = item.AccountDate.AddDays(trem);

                //默认关联到第一单

                if (item.Type== BillType.DC)
                {
                    item.FormID = new Guid(cmbRefNo.Value.ToString());
                    item.FormType = (FormType)cmbRefNo.Properties.Items[cmbRefNo.SelectedIndex].ImageIndex;
                    item.FormNo = cmbRefNo.Text.ToString();
                }
                else
                {
                    item.FormID = _OperationCommonInfo.Forms[0].ID;
                    item.FormType = _OperationCommonInfo.Forms[0].Type;
                    item.FormNo = _OperationCommonInfo.Forms[0].No;
                }

                item.CompanyID = _OperationCommonInfo.CompanyID;
                //ID置空
                item.ID = Guid.Empty;
                item.UpdateDate = null;
                foreach (var fee in item.Fees)
                {
                    fee.ID = Guid.Empty;
                    fee.Rate = RateHelper.GetRate(fee.CurrencyID, _ConfigureInfo.StandardCurrencyID, DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified), _RateList);
                    //fee.refid = null;
                    fee.UpdateDate = null;
                    fee.FromType = (int)_OperationCommonInfo.OperationType;
                }
            }

            try
            {
                List<int> lspApplyStates = new List<int>();
                List<HierarchyManyResult> results = FinanceService.SaveBillInfos(_OperationCommonInfo.OperationID, infos, LocalData.UserInfo.LoginID, 1, _OperationCommonInfo.OperationDate);
                for (int i = 0; i < infos.Count; i++)
                {
                    infos[i].ID = results[i].GetValue<Guid>("ID");
                    infos[i].No = results[i].GetValue<string>("No");
                    infos[i].UpdateDate = results[i].GetValue<DateTime?>("UpdateDate");

                    lspApplyStates.Add(results[i].GetValue<int>("ApplyState"));
                    for (int j = 0; j < infos[i].Fees.Count; j++)
                    {
                        infos[i].Fees[j].ID = results[i].Childs[j].GetValue<Guid>("ID");
                        infos[i].Fees[j].UpdateDate = results[i].Childs[j].GetValue<DateTime?>("UpdateDate");

                    }
                }

                List<BillList> source = bsBillList.DataSource as List<BillList>;
                if (source == null) source = new List<BillList>();

                foreach (var item in infos)
                {
                    source.Add(item);
                }
                bsBillList.DataSource = source;
                bsBillList.ResetBindings(false);
                gvBillList.ExpandAllGroups();

               // int applyState = lspApplyStates.Sort();
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return; }

            #endregion

            SelectedBills = selectedBills;

            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        bool isShow = true;
        private void btnHideBusinessPanel_Click(object sender, EventArgs e)
        {
            SetBusinessVisible(!isShow);
        }

        #endregion

        #region IPart 成员
        OperationCommonInfo _OperationCommonInfo = null;
        ConfigureInfo _ConfigureInfo = null;
        List<SolutionExchangeRateList> _RateList = null;
        List<SolutionCurrencyList> _CurrencyList = null;

        //业务的客户
        OperationCustomer _operationCustomer = null;

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "BLCommonInfo")
                {
                    _OperationCommonInfo = item.Value as OperationCommonInfo;
                    foreach (var customer in _OperationCommonInfo.TradeCustomers)
                    {
                        if (customer.IsCustomer) { _operationCustomer = customer; break; }
                    }

                }
                else if (item.Key == "ConfigureInfo")
                {
                    _ConfigureInfo = item.Value as ConfigureInfo;
                }
                else if (item.Key == "SolutionExchangeRateList")
                {
                    _RateList = item.Value as List<SolutionExchangeRateList>;
                }
                else if (item.Key == "SolutionCurrencyList")
                {
                    _CurrencyList = item.Value as List<SolutionCurrencyList>;
                }
            }
        }
        #endregion

        #region Select

        private void barSelectAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<BillInfo> billinfos = bsBillList.DataSource as List<BillInfo>;
            if (billinfos == null || billinfos.Count == 0) return;

            foreach (var item in billinfos)
            {
                item.Selected = true;
                foreach (var fee in item.Fees)
                {
                    fee.Selected = true;
                }
            }

            for (int i = 0; i < gcBillList.Views.Count; i++)
            {
                gcBillList.Views[i].RefreshData();
            }
        }
        #endregion
    }
}
