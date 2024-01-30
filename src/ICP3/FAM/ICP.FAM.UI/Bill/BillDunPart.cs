using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using ICP.Common.ServiceInterface.Client;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.UI;

namespace ICP.FAM.UI.Bill
{
    public partial class BillDunPart : BasePart
    {
        public BillDunPart()
        {
            InitializeComponent();
            Disposed += delegate {
                RateList = null;
                bsList.DataSource = null;
                bsList.Dispose();
                Selected = null;
                cmbCompany.OnFirstEnter -= OncmbCompanyFirstEnter;
                cmbCompany.SelectedIndexChanged -= cmbCompany_SelectedIndexChanged;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }


        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }
        public InvoiceReportHelper InvoiceReportHelper
        {
            get
            {
                return ClientHelper.Get<InvoiceReportHelper, InvoiceReportHelper>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 汇率信息
        /// </summary>
        public List<SolutionExchangeRateList> RateList = new List<SolutionExchangeRateList>();
        /// <summary>
        /// 数据源
        /// </summary>
        public object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                List<CurrencyBillList> list = value as List<CurrencyBillList>;
                if (list == null)
                {
                    barClear.Enabled = false;
                    barRemove.Enabled = false;
                    list = new List<CurrencyBillList>();
                }
                else
                {
                    barClear.Enabled = true;
                    barRemove.Enabled = true;
                }

                bsList.DataSource = list;
                listBox.DataSource = bsList;
                listBox.DisplayMember = "BillNO";
                listBox.ValueMember = "ID";
                bsList.ResetBindings(false);
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<CurrencyBillList> DataList
        {
            get
            {
                List<CurrencyBillList> list = bsList.DataSource as List<CurrencyBillList>;
                if (list == null)
                {
                    list = new List<CurrencyBillList>();
                }
                return list;
            }
        }
        /// <summary>
        /// 当前行数据
        /// </summary>
        private CurrencyBillList CurrentRow
        {
            get
            {
                return bsList.Current as CurrencyBillList;
            }
        }
        /// <summary>
        /// 事件
        /// </summary>
        public  event SelectedHandler Selected;
        #endregion

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }
        private void OncmbCompanyFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitControls()
        { 

            //绑定公司
           
            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            cmbCompany.OnFirstEnter += OncmbCompanyFirstEnter;
           

            //绑定类型
            
            List<EnumHelper.ListItem<DunGroupType>> dunGroupType = EnumHelper.GetEnumValues<DunGroupType>(LocalData.IsEnglish);
            cmbType.Properties.BeginUpdate();
            foreach (var item in dunGroupType)
            {
                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbType.SelectedIndex = 0;
            cmbType.Properties.EndUpdate();

        }


        private void chkAmount_CheckedChanged(object sender, EventArgs e)
        {
            cmbCurrency.Enabled = chkAmount.Checked;
            if (chkAmount.Checked && cmbCurrency.Properties.Items.Count == 0)
            {
                List<CurrencyList> list=ConfigureService.GetCurrencyList(string.Empty, string.Empty, null, true, 0);
                cmbCurrency.Properties.BeginUpdate();
                cmbCurrency.Properties.Items.Clear();
                foreach (CurrencyList item in list)
                {
                    cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
                }
                cmbCurrency.Properties.EndUpdate();
                cmbCurrency.SelectedIndex = 0;
            }
        }
        #endregion

        #region 初始化银行
        /// <summary>
        /// 初始化银行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitBank();
        }
        /// <summary>
        /// 初始化银行
        /// </summary>
        private void InitBank()
        {
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                Guid companyID = new Guid(cmbCompany.EditValue.ToString());
                DataPageInfo dataPage=new DataPageInfo();
                dataPage.SortByName = LocalData.IsEnglish ? "EName" : "CName";
                dataPage.SortOrderType = SortOrderType.Asc;
                PageList pagelist=FinanceService.GetBankList(new Guid[1] { companyID }, string.Empty, string.Empty, string.Empty, true, dataPage, LocalData.IsEnglish);

                List<BankList> bankList=pagelist.GetList<BankList>();

                cmbBank.Properties.Items.BeginUpdate();
                cmbBank.Properties.Items.Clear();

                foreach (BankList bank in bankList)
                {
                    cmbBank.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish?bank.EName:bank.CName,bank.ID));
                }
                cmbBank.Properties.Items.EndUpdate();
                cmbBank.SelectedIndex = 0;
            }
        }
        #endregion

        #region 移除&清空
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            CurrencyBillList item = CurrentRow;

            if (DataList.Contains(item))
            {
                DataList.Remove(item);
                DataSource = DataList;
                bsList.ResetBindings(false);

                if (Selected != null)
                {
                    Selected("Remove", item.CurrentID);
                }
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            string message = LocalData.IsEnglish ? "Sure Clare Data" : "确认清空数据";
            if (!FAMUtility.ShowResultMessage(message))
            {
                return;
            }

            ClareList();
        }
        /// <summary>
        /// 清空列表
        /// </summary>
        private void ClareList()
        {
            DataList.Clear();
            DataSource = DataList;
            bsList.ResetBindings(false);

            if (Selected != null)
            {
                Selected("Clear", null);
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
            if (DataList == null || DataList.Count == 0)
            {
                return;
            }

            string fileName = Application.StartupPath + "\\Reports\\FAM\\";
            Guid customerID = Guid.Empty;
            Guid companyID = Guid.Empty;
            Guid bankID = Guid.Empty;
            List<Guid> billIDList = new List<Guid>();

            customerID = DataList[0].CustomerID;

            if (cmbBank.EditValue != null && !string.IsNullOrEmpty(cmbBank.EditValue.ToString()))
            {
                bankID = new Guid(cmbBank.EditValue.ToString());
            }
            if (cmbCompany.EditValue != null && !string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                companyID = new Guid(cmbCompany.EditValue.ToString());
            }

            billIDList = (from d in DataList group d by d.ID into g select g.Key).ToList();

            BillDunReportData reportData = FinanceReportService.GetBillDunReportData(customerID, companyID, LocalData.UserInfo.LoginID, bankID, billIDList.ToArray());

            if (reportData == null)
            {
                return;
            }

            if (reportData.CompanyInfo != null)
            {
                reportData.CompanyInfo.BankName = "银行名称:" + reportData.CompanyInfo.BankName;
            }

            if (reportData.AccountList != null)
            {
                string accountInfo = string.Empty;
                foreach (var sub in reportData.AccountList)
                {
                    if (accountInfo != string.Empty) accountInfo += "\n\r";
                    accountInfo += sub.CurrencyName + "帐号:" + sub.AccountNo;
                }

                reportData.AccountList[0].AccountNo = accountInfo;
            }

            if (RateList.Count == 0)
            {
                RateList = ConfigureService.GetCompanyExchangeRateList(companyID, true);
            }

            #region 币种与分组方式
            if (cmbType.EditValue != null)
            {
                reportData.DunGroupType = ((DunGroupType)cmbType.EditValue).ToString();
            }
            if (chkAmount.Checked && cmbCurrency.EditValue != null)
            {
                reportData.ToCurrencyName = cmbCurrency.Text;
                reportData.ToCurrencyID = new Guid(cmbCurrency.EditValue.ToString());
            }
            #endregion

            #region 转换报表显示的帐单\费用列表

            //报表显示的帐单\费用列表
            List<BillDunReportDataChargeInfo> reportChargeList = new List<BillDunReportDataChargeInfo>();

            if (reportData.DBCostList != null && reportData.DBCostList.Count > 0)
            {
                if (reportData.DunGroupType == DunGroupType.BillTotal.ToString())
                {
                    #region 帐单

                    fileName += "GatheringTotal.frx";

                    foreach (var item in DataList)
                    {
                        List<BillDunReportDataCostInfo> listByGroup = (from c in reportData.DBCostList where c.BillNo == item.BillNO && c.CurrencyID == item.CurrencyID select c).ToList();
                        if (listByGroup.Count > 0)
                        {
                            BillDunReportDataChargeInfo bill = new BillDunReportDataChargeInfo();
                            bill.BillNo = listByGroup[0].BillNo;
                            bill.BLNo = listByGroup[0].BLNo;
                            bill.ContainerNos = listByGroup[0].ContainerNos;
                            bill.ETD = listByGroup[0].ETD == null ? string.Empty : listByGroup[0].ETD.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                            bill.ETA = listByGroup[0].ETA == null ? string.Empty : listByGroup[0].ETA.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                            bill.PODName = listByGroup[0].PODName;
                            bill.InvoiceNo = listByGroup[0].InvoiceNo;
                            //listByGroup[0].Amount = listByGroup.Sum(i => i.Amount); 
                            listByGroup[0].Amount = item.Amount - item.WriteOffAmount;
                            bill = ConvertAmount(reportData.ToCurrencyName, reportData.ToCurrencyID, bill, listByGroup[0], item.AccountDate == null ? DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified) : item.AccountDate.Value);
                            
                            reportChargeList.Add(bill);
                        }
                    }                 

                    #endregion
                }
                else
                {
                    #region 费用
                    fileName += "GatheringFeeDetail.frx";

                    foreach (var item in DataList)
                    {
                        List<BillDunReportDataCostInfo> listByGroup = (from c in reportData.DBCostList where c.BillNo == item.BillNO && c.CurrencyID == item.CurrencyID select c).ToList();
                        for (int i = 0; i < listByGroup.Count; i++)
                        {
                            BillDunReportDataChargeInfo fee = new BillDunReportDataChargeInfo();
                            if (i == 0)
                            {
                                fee.BillNo = listByGroup[i].BillNo;
                            }

                            fee.BLNo = listByGroup[i].BLNo;
                            fee.ChargeName = listByGroup[i].ChargeName;
                            fee.ContainerNos = listByGroup[i].ContainerNos;
                            fee.ETD = listByGroup[i].ETD == null ? string.Empty : listByGroup[i].ETD.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                            fee.ETA = listByGroup[i].ETA == null ? string.Empty : listByGroup[i].ETA.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                            fee.PODName = listByGroup[i].PODName;
                            fee.InvoiceNo = listByGroup[0].InvoiceNo;
                            fee = ConvertAmount(reportData.ToCurrencyName, reportData.ToCurrencyID, fee, listByGroup[i], item.AccountDate == null ? DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified) : item.AccountDate.Value);
                            reportChargeList.Add(fee);
                        }
                       
                        BillDunReportDataChargeInfo subTotal = ConvertToChargeInfoForSubTotal(reportData.ToCurrencyName, reportData.ToCurrencyID, item);
                        subTotal.ChargeName = "SubTotal:";
                        reportChargeList.Add(subTotal);

                        BillDunReportDataChargeInfo notPaidSubTotal = ConvertToChargeInfoForNotPaidSubTotal(reportData.ToCurrencyName, reportData.ToCurrencyID, item);
                        notPaidSubTotal.ChargeName = "NotPaidSubTotal:";
                        reportChargeList.Add(notPaidSubTotal);
                    }

                    #endregion
                }
            }

            reportData.ReportChargeList = reportChargeList;

            #endregion

            #region BaseInfo
            BaseInfo baseInfo = new BaseInfo();
            baseInfo.ToCurrencyName = string.IsNullOrEmpty(reportData.ToCurrencyName) ? string.Empty : "折合" + reportData.ToCurrencyName;
            if (reportData.DunGroupType == DunGroupType.BillTotal.ToString())
            {
                baseInfo.TotalUSDAmount = reportData.ReportChargeList.Sum(i => i.USDAmount == null ? 0m : i.USDAmount.Value);
                if (baseInfo.TotalUSDAmount == 0)
                {
                    baseInfo.TotalUSDAmount = 0;
                }

                baseInfo.TotalRMBAmount = reportData.ReportChargeList.Sum(i => i.RMBAmount == null ? 0m : i.RMBAmount.Value);
                if (baseInfo.TotalRMBAmount == 0)
                {
                    baseInfo.TotalRMBAmount = 0;
                }

                baseInfo.TotalHKDAmount = reportData.ReportChargeList.Sum(i => i.HKDAmount == null ? 0m : i.HKDAmount.Value);
                if (baseInfo.TotalHKDAmount == 0)
                {
                    baseInfo.TotalHKDAmount = 0;
                }

                baseInfo.TotalToAmount = reportData.ReportChargeList.Sum(i => i.ToAmount == null ? 0m : i.ToAmount.Value);
                if (baseInfo.TotalToAmount == 0)
                {
                    baseInfo.TotalToAmount = 0;
                }
            }
            else
            {
                baseInfo.TotalUSDAmount = reportData.ReportChargeList.Where(j => j.ChargeName != "SubTotal:" && j.ChargeName != "NotPaidSubTotal:").Sum(i => i.USDAmount == null ? 0m : i.USDAmount.Value);
                if (baseInfo.TotalUSDAmount == 0)
                {
                    baseInfo.TotalUSDAmount = 0;
                }

                baseInfo.TotalRMBAmount = reportData.ReportChargeList.Where(j => j.ChargeName != "SubTotal:" && j.ChargeName != "NotPaidSubTotal:").Sum(i => i.RMBAmount == null ? 0m : i.RMBAmount.Value);
                if (baseInfo.TotalRMBAmount == 0)
                {
                    baseInfo.TotalRMBAmount = 0;
                }

                baseInfo.TotalHKDAmount = reportData.ReportChargeList.Where(j => j.ChargeName != "SubTotal:" && j.ChargeName != "NotPaidSubTotal:").Sum(i => i.HKDAmount == null ? 0m : i.HKDAmount.Value);
                if (baseInfo.TotalHKDAmount == 0)
                {
                    baseInfo.TotalHKDAmount = 0;
                }

                baseInfo.TotalToAmount = reportData.ReportChargeList.Where(j => j.ChargeName != "SubTotal:" && j.ChargeName != "NotPaidSubTotal:").Sum(i => i.ToAmount == null ? 0m : i.ToAmount.Value);
                if (baseInfo.TotalToAmount == 0)
                {
                    baseInfo.TotalToAmount = 0;
                }

                baseInfo.TotalNotPaidUSDAmount = reportData.ReportChargeList.Where(j => j.ChargeName == "NotPaidSubTotal:").Sum(i => i.USDAmount == null ? 0m : i.USDAmount.Value);
                if (baseInfo.TotalNotPaidUSDAmount == 0)
                {
                    baseInfo.TotalNotPaidUSDAmount = 0;
                }

                baseInfo.TotalNotPaidRMBAmount = reportData.ReportChargeList.Where(j => j.ChargeName == "NotPaidSubTotal:").Sum(i => i.RMBAmount == null ? 0m : i.RMBAmount.Value);
                if (baseInfo.TotalNotPaidRMBAmount == 0)
                {
                    baseInfo.TotalNotPaidRMBAmount = 0;
                }

                baseInfo.TotalNotPaidHKDAmount = reportData.ReportChargeList.Where(j => j.ChargeName == "NotPaidSubTotal:").Sum(i => i.HKDAmount == null ? 0m : i.HKDAmount.Value);
                if (baseInfo.TotalNotPaidHKDAmount == 0)
                {
                    baseInfo.TotalNotPaidHKDAmount = 0;
                }

                baseInfo.TotalNotPaidToAmount = reportData.ReportChargeList.Where(j => j.ChargeName == "NotPaidSubTotal:").Sum(i => i.ToAmount == null ? 0m : i.ToAmount.Value);
                if (baseInfo.TotalNotPaidToAmount == 0)
                {
                    baseInfo.TotalNotPaidToAmount = 0;
                }
            }

            #endregion

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Dun Bill" : "催款单", (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("BillDunReportData_BaseInfo", baseInfo);
            reportSource.Add("BillDunReportData_CompanyInfo", reportData.CompanyInfo);
            reportSource.Add("BillDunReportData_CustomerInfo", reportData.CustomerInfo);
            reportSource.Add("BillDunReportData_BillList", reportData.ReportChargeList);
            reportSource.Add("BillDunReportData_AccountList", reportData.AccountList);
            viewer.BindData(fileName, reportSource, null);
        }

        /// <summary>
        /// 转换金额
        /// </summary>
        /// <param name="toCurrencyName"></param>
        /// <param name="toCurrencyID"></param>
        /// <param name="reportData"></param>
        /// <param name="dbData"></param>
        /// <returns></returns>
        private BillDunReportDataChargeInfo ConvertAmount(string toCurrencyName,Guid toCurrencyID,BillDunReportDataChargeInfo reportData, BillDunReportDataCostInfo dbData, DateTime billDate)
        {
            if (dbData.CurrencyName == "RMB")
            {
                if (reportData.RMBAmount == null && dbData.Amount != null)
                {
                    reportData.RMBAmount = 0m;
                }
                reportData.RMBAmount += dbData.Amount;

                if (reportData.RMBUnpaidAmount == null && dbData.UnPaidAmount != null)
                {
                    reportData.RMBUnpaidAmount = 0m;
                }
                reportData.RMBUnpaidAmount += dbData.UnPaidAmount;

            }
            else if (dbData.CurrencyName == "USD")
            {
                if (reportData.USDAmount == null && dbData.Amount != null)
                {
                    reportData.USDAmount = 0m;
                }
                reportData.USDAmount += dbData.Amount;

                if (reportData.USDUnpaidAmount == null && dbData.UnPaidAmount != null)
                {
                    reportData.USDUnpaidAmount = 0m;
                }
                reportData.USDUnpaidAmount += dbData.UnPaidAmount;
            }
            else if (dbData.CurrencyName == "HKD")
            {
                if (reportData.HKDAmount == null && dbData.Amount != null)
                {
                    reportData.HKDAmount = 0m;
                }
                reportData.HKDAmount += dbData.Amount;

                if (reportData.HKDUnpaidAmount == null && dbData.UnPaidAmount != null)
                {
                    reportData.HKDUnpaidAmount = 0m;
                }
                reportData.HKDUnpaidAmount += dbData.UnPaidAmount;
            }
        
            if (!string.IsNullOrEmpty(toCurrencyName))
            {
                if (reportData.ToAmount== null)
                {
                    reportData.ToAmount = 0m;
                }
                decimal amount = dbData.Amount == null ? 0 : dbData.Amount.Value;
                try
                {
                    reportData.ToAmount += Math.Round(RateHelper.GetAmountByRate(amount, dbData.CurrencyID, toCurrencyID, RateList, billDate), 2);
                }
                catch
                {
                     reportData.ToAmount +=amount;
                }

                if (reportData.ToUnpaidAmount == null)
                {
                    reportData.ToUnpaidAmount = 0m;
                }
                decimal unPaidAmount = dbData.UnPaidAmount == null ? 0 : dbData.UnPaidAmount.Value;
                try
                {
                    reportData.ToUnpaidAmount += Math.Round(RateHelper.GetAmountByRate(unPaidAmount, dbData.CurrencyID, toCurrencyID, RateList, billDate), 2);
                }
                catch
                {
                    reportData.ToUnpaidAmount += unPaidAmount;
                }
            }
            return reportData;
        }

        /// <summary>
        /// 转换金额
        /// </summary>
        /// <param name="toCurrencyName"></param>
        /// <param name="toCurrencyID"></param>
        /// <param name="reportData"></param>
        /// <param name="dbData"></param>
        /// <returns></returns>
        private BillDunReportDataChargeInfo ConvertToChargeInfoForSubTotal(string toCurrencyName, Guid toCurrencyID, CurrencyBillList dbData)
        {
            BillDunReportDataChargeInfo bill = new BillDunReportDataChargeInfo();
            if (dbData.CurrencyName == "RMB")
            {
                bill.RMBAmount = dbData.Amount;
            }
            else if (dbData.CurrencyName == "USD")
            {
                bill.USDAmount= dbData.Amount;
            }
            else if (dbData.CurrencyName == "HKD")
            {
                bill.HKDAmount = dbData.Amount;
            }

            if (!string.IsNullOrEmpty(toCurrencyName))
            {
                if (bill.ToAmount == null)
                {
                    bill.ToAmount = 0m;
                }

                try
                {
                    bill.ToAmount += Math.Round(RateHelper.GetAmountByRate(dbData.Amount, dbData.CurrencyID, toCurrencyID, RateList, dbData.AccountDate == null ? DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified) : dbData.AccountDate.Value), 2);
                }
                catch
                {
                    bill.ToAmount = dbData.Amount;
                }
            }

            return bill;
        }
      
        private BillDunReportDataChargeInfo ConvertToChargeInfoForNotPaidSubTotal(string toCurrencyName, Guid toCurrencyID, CurrencyBillList dbData)
        {
            BillDunReportDataChargeInfo bill = new BillDunReportDataChargeInfo();
            if (dbData.CurrencyName == "RMB")
            {
                bill.RMBAmount = dbData.Amount - dbData.WriteOffAmount;
            }
            else if (dbData.CurrencyName == "USD")
            {
                bill.USDAmount = dbData.Amount - dbData.WriteOffAmount;
            }
            else if (dbData.CurrencyName == "HKD")
            {
                bill.HKDAmount = dbData.Amount - dbData.WriteOffAmount;
            }

            if (!string.IsNullOrEmpty(toCurrencyName))
            {
                if (bill.ToAmount == null)
                {
                    bill.ToAmount = 0m;
                }

                try
                {
                    bill.ToAmount += Math.Round(RateHelper.GetAmountByRate(dbData.Amount - dbData.WriteOffAmount, dbData.CurrencyID, toCurrencyID, RateList, dbData.AccountDate== null? DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified): dbData.AccountDate.Value), 2);
                }
                catch
                {
                    bill.ToAmount = dbData.Amount - dbData.WriteOffAmount;
                }
            }

            return bill;
        }

        #endregion
    }
}
