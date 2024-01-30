using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraEditors;

namespace ICP.FAM.UI.CustomerBill.Print
{
    [ToolboxItem(false)]
    public partial class PrintBillConfigPart : BaseEditPart
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
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<IOrganizationService>();
            }
        }

        public IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
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

        #region 本地变量

        PrintBillConfigData _CurrentData = null;

        Dictionary<Guid, PrintBillCacheInfo> _CacheInfo = new Dictionary<Guid, PrintBillCacheInfo>();

        /// <summary>
        /// 缓存的帐单汇率信息
        /// </summary>
        List<SolutionExchangeRateList> _BillRate = new List<SolutionExchangeRateList>();

        Guid _CurrentCompanyID = Guid.Empty;
        bool _SaveRate = false;

        #endregion

        #region init

        public PrintBillConfigPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gcRate.DataSource = null;
                bindingSource1.DataSource = null;
                bindingSource1.Dispose();
                _BillInfo = null;
                _BillRate = null;
                _CacheInfo = null;
                _ConfigureInfo = null;
                _CurrentData = null;
                _OperationCommonInfo = null;
                bsCurrencyRateData.DataSource = null;
                bsCurrencyRateData.Dispose();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(this);
                    WorkItem = null;
                }

            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitData();
            InitControls();
        }

        private void InitData()
        {
            _CacheInfo.Add(_OperationCommonInfo.CompanyID, new PrintBillCacheInfo());
            //_CacheInfo[_OperationCommonInfo.CompanyID].RateList = _RateList;
            _CacheInfo[_OperationCommonInfo.CompanyID].RateList = FinanceService.GetCompanyPrintBillExchangeRateList(_ConfigureInfo.CompanyID);
            _CacheInfo[_OperationCommonInfo.CompanyID].ConfigureInfo = _ConfigureInfo;
            _CacheInfo[_OperationCommonInfo.CompanyID].ReportConfigures = ConfigureService.GetReportConfigureList(_ConfigureInfo.CompanyID, BillReportConfigConstants.CustomerBillConfig);
            _CacheInfo[_OperationCommonInfo.CompanyID].Customer = CustomerService.GetCustomerInfo(_ConfigureInfo.CustomerID);
        }

        private void InitControls()
        {
            BulidLogo();

            cmbNote.Properties.Items.Add(new ImageComboBoxItem("INVOICE", "INVOICE"));
            cmbNote.Properties.Items.Add(new ImageComboBoxItem("TAX INVOICE", "TAX INVOICE"));
            cmbNote.Properties.Items.Add(new ImageComboBoxItem("DEBIT NOTE", "DEBIT NOTE"));
            cmbNote.Properties.Items.Add(new ImageComboBoxItem("CREDIT NOTE", "CREDIT NOTE"));
            cmbNote.SelectedIndex = 0;

            RdoInit();
            BulidCompanyInfo();



            foreach (var item in _OperationCommonInfo.Forms)
            {
                chkListBLNO.Items.Add(item.No, item.ID == _BillInfo.FormID);
            }

            if (_OperationCommonInfo.OperationType == OperationType.OceanExport)
            {
                labShip.Visible = cmbPrintBillShipType.Visible = true;
                List<EnumHelper.ListItem<PrintBillShipType>> printBillShipTypes
                    = EnumHelper.GetEnumValues<PrintBillShipType>(LocalData.IsEnglish);
                foreach (var item in printBillShipTypes)
                {
                    cmbPrintBillShipType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                }
            }
            else if (_OperationCommonInfo.OperationType == OperationType.OceanImport)
            {
                chkShowFETA.Visible = true;
            }

        }

        private void RdoInit()
        {
            if (LocalData.IsEnglish) rdoCNEN.SelectedIndex = 1;
            else rdoCNEN.SelectedIndex = 0;

            rdoCNEN.SelectedIndexChanged += delegate
            {
                CompanyChanged();
            };

            rdoPrintType.SelectedIndexChanged += delegate
            {
                switch (rdoPrintType.SelectedIndex)
                {
                    case 0:
                        _CurrentData.PrintBillType = PrintBillType.Normal;
                        labPrintDate.Visible = dtePrintDate.Visible = true;
                        break;
                    case 1:
                        _CurrentData.PrintBillType = PrintBillType.Commission;
                        //佣金帐单打印日不能更变
                        _CurrentData.PrintData = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                        labPrintDate.Visible = dtePrintDate.Visible = false;
                        break;
                }
            };
        }

        #region Company

        private void BulidCompanyInfo()
        {
            //List<OrganizationList>  userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            List<OrganizationList> allCompanyList = OrganizationService.GetOfficeList();
            foreach (var item in allCompanyList)
            {
                cmbTitelCompany.Properties.Items.Add(new ImageComboBoxItem
                    (LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }

            cmbTitelCompany.Properties.Items.Add(new ImageComboBoxItem("COLA", new Guid("F29D7D48-1D3B-4298-9615-1DA72E4A837E")));
            cmbTitelCompany.EditValueChanged += delegate
            {
                CompanyChanged();
            };
            cmbTitelCompany.EditValue = _OperationCommonInfo.CompanyID;
        }

        private void CompanyChanged()
        {
            try
            {
                if (cmbTitelCompany.EditValue == null) return;

                Guid companyID = new Guid(cmbTitelCompany.EditValue.ToString());
                if (companyID.IsNullOrEmpty()) return;

                _CurrentCompanyID = companyID;
                if (_CacheInfo.ContainsKey(companyID) == false) BulidCacheInfoByCompanyID(companyID);

                BulidCompanyDes(companyID);
                BulidCompanyReportConfigureInfo(companyID);
                BulidRates(companyID);
            }
            catch (Exception ex)
            {
                string strmessage = LocalData.IsEnglish ? "The company's configuration information is incorrect, please choose other companies" : "该公司的配置信息有误，请选择其他公司！";
                XtraMessageBox.Show(strmessage, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void BulidCompanyDes(Guid companyID)
        {
            CustomerInfo tager = _CacheInfo[companyID].Customer;
            StringBuilder strDes = new StringBuilder();
            if (rdoCNEN.SelectedIndex == 1)
            {
                txtTitelCompanyName.Text = _CurrentData.TitelCompanyName = tager.EBillName;
                strDes.Append(tager.EAddress + "\r\n");
            }
            else
            {
                txtTitelCompanyName.Text = _CurrentData.TitelCompanyName = tager.CBillName;
                strDes.Append(tager.CAddress + "\r\n");
            }
            if (tager.Tel1.IsNullOrEmpty() == false) strDes.Append("Tel:" + tager.Tel1 + " ");
            if (tager.Fax.IsNullOrEmpty() == false) strDes.Append("Fax:" + tager.Fax);
            txtTitelCompanyDes.Text = _CurrentData.TitelCompanyDes = strDes.ToString();
        }

        #endregion

        /// <summary>
        /// 公司报表参数
        /// </summary>
        private void BulidCompanyReportConfigureInfo(Guid companyID)
        {
            CompanyReportConfigureList reportConfigure = _CacheInfo[companyID].ReportConfigures;

            #region SuffixNo

            ReportParameterList reportParameter = reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.ShowSuffix);
            labSuffixNo.Visible = txtSuffixNo.Visible = false;
            if (reportParameter != null && reportParameter.ParameterValue.IsNullOrEmpty() == false)
            {
                bool isShowSuffixNo = false;
                bool isSrcc = bool.TryParse(reportParameter.ParameterValue, out isShowSuffixNo);

                if (_BillInfo.Type == BillType.AR && isSrcc && isShowSuffixNo)
                {
                    labSuffixNo.Visible = txtSuffixNo.Visible = true;
                    SingleResult result = FinanceReportService.GetSuffixNo(_CurrentData.BillID);
                    string no = result.GetValue<string>("SuffixNo");
                    string maxNo = result.GetValue<string>("MaxNo");
                    if (string.IsNullOrEmpty(no))
                    {
                        _CurrentData.SuffixNo = (Convert.ToInt32(maxNo) + 1).ToString();
                    }
                    else
                    {
                        _CurrentData.SuffixNo = no;
                        txtSuffixNo.Properties.ReadOnly = true;
                    }

                    int length = _CurrentData.SuffixNo.Length;

                    for (int i = 0; i < 5 - length; i++)
                        _CurrentData.SuffixNo = "0" + _CurrentData.SuffixNo;
                }
            }

            #endregion

            #region BankInfo

            string _Signature = string.Empty;
            string _BillBankInfo_CN = string.Empty;
            string _BillBankInfo_EN = string.Empty;
            string _BillRemarkInfo_EN = string.Empty;
            string _BillRemarkInfo_CN = string.Empty;

            if (reportConfigure.Parameters != null && reportConfigure.Parameters.Count != 0)
            {
                _Signature = reportConfigure.Parameters.Find(b => b.Code == BillReportConfigConstants.Signature).ParameterValue;

                _BillBankInfo_CN = reportConfigure.Parameters.Find(b => b.Code == BillReportConfigConstants.BillBankInfo_CN).ParameterValue;

                _BillBankInfo_EN = reportConfigure.Parameters.Find(b => b.Code == BillReportConfigConstants.BillBankInfo_EN).ParameterValue;

                _BillRemarkInfo_EN = reportConfigure.Parameters.Find(b => b.Code == BillReportConfigConstants.BillRemarkInfo_EN).ParameterValue;

                _BillRemarkInfo_CN = reportConfigure.Parameters.Find(b => b.Code == BillReportConfigConstants.BillRemarkInfo_CN).ParameterValue;
            }

            _CurrentData.Signature = _Signature;
            if (rdoCNEN.SelectedIndex == 1)
            {
                txtBankInfo.Text = _CurrentData.BankInfo = _BillBankInfo_EN;
                txtRemarkInfo.Text = _CurrentData.RemarkInfo = _BillRemarkInfo_EN;
            }
            else
            {
                txtBankInfo.Text = _CurrentData.BankInfo = _BillBankInfo_CN;
                txtRemarkInfo.Text = _CurrentData.RemarkInfo = _BillRemarkInfo_CN;
            }

            #endregion

            #region Logo

            if (reportConfigure.Parameters != null && reportConfigure.Parameters.Count != 0)
            {
                ReportParameterList logoParameter = reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.Logo);

                int logoIndex = 0;
                if (logoParameter != null && logoParameter.ParameterValue.IsNullOrEmpty() == false)
                {
                    int.TryParse(logoParameter.ParameterValue, out logoIndex);
                }
                cmbLogoFileName.SelectedIndex = logoIndex;
            }
            #endregion

            #region Note

            if (reportConfigure.Parameters != null && reportConfigure.Parameters.Count != 0)
            {
                int noteIndex = 0;
                if (_BillInfo.Type == BillType.DC)
                {
                    ReportParameterList noteParameter = reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.DRCR_NOTE);

                    if (noteParameter != null && noteParameter.ParameterValue.IsNullOrEmpty() == false)
                    {
                        int.TryParse(noteParameter.ParameterValue, out noteIndex);
                    }
                }
                else if (_BillInfo.Type == BillType.AR)
                {
                    ReportParameterList noteParameter = reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.DR_NOTE);
                    if (noteParameter != null && noteParameter.ParameterValue.IsNullOrEmpty() == false)
                    {
                        int.TryParse(noteParameter.ParameterValue, out noteIndex);
                    }
                    cmbNote.SelectedIndex = noteIndex;
                }
                else if (_BillInfo.Type == BillType.AP)
                {
                    ReportParameterList noteParameter = reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.CR_NOTE);
                    if (noteParameter != null && noteParameter.ParameterValue.IsNullOrEmpty() == false)
                    {
                        int.TryParse(noteParameter.ParameterValue, out noteIndex);
                    }

                }
                cmbNote.SelectedIndex = noteIndex;
            }

            #endregion
        }

        /// <summary>
        /// 生成缓存信息
        /// </summary>
        /// <param name="companyID"></param>
        private void BulidCacheInfoByCompanyID(Guid companyID)
        {
            _CacheInfo.Add(companyID, new PrintBillCacheInfo());
            _CacheInfo[companyID].RateList = FinanceService.GetCompanyPrintBillExchangeRateList(companyID);
            //_CacheInfo[companyID].RateList = configureService.GetCompanyExchangeRateList(companyID, true);
            _CacheInfo[companyID].ConfigureInfo = ConfigureService.GetCompanyConfigureInfo(companyID);
            _CacheInfo[companyID].ReportConfigures = ConfigureService.GetReportConfigureList(companyID, BillReportConfigConstants.CustomerBillConfig);
            _CacheInfo[companyID].Customer = CustomerService.GetCustomerInfo(_CacheInfo[companyID].ConfigureInfo.CustomerID);
        }

        /// <summary>
        /// 构建默认币种汇率
        /// </summary>
        private void BulidRates(Guid companyID)
        {
            //if (_BillRate == null || _BillRate.Count == 0) _BillRate = finReportService.GetPrintBillRateList(_CurrentData.BillID);

            if (_BillRate == null || _BillRate.Count == 0)
            {
                _BillRate = new List<SolutionExchangeRateList>();
                //SolutionExchangeRateList usd2hkd = _CacheInfo[companyID].RateList.Find(delegate(SolutionExchangeRateList item) { return item.SourceCurrency == "USD" && item.TargetCurrency == "HKD" && _BillInfo.AccountDate>= item.FromDate && _BillInfo.AccountDate <= item.ToDate; });
                SolutionExchangeRateList usd2hkd = GetMatchSolutionExchangeRate("USD", "HKD", _CacheInfo[companyID].RateList);
                if (usd2hkd == null)
                {
                    usd2hkd = new SolutionExchangeRateList();
                    usd2hkd.ID = Guid.NewGuid();
                    usd2hkd.SourceCurrency = "USD";
                    usd2hkd.TargetCurrency = "HKD";
                    usd2hkd.Rate = 1;
                }
                _BillRate.Add(usd2hkd);

                SolutionExchangeRateList hkd2usd = GetMatchSolutionExchangeRate("HKD", "USD", _CacheInfo[companyID].RateList);
                if (hkd2usd == null)
                {
                    hkd2usd = new SolutionExchangeRateList();
                    hkd2usd.ID = Guid.NewGuid();
                    hkd2usd.SourceCurrency = "HKD";
                    hkd2usd.TargetCurrency = "USD";
                    hkd2usd.Rate = 1;
                }
                _BillRate.Add(hkd2usd);

                SolutionExchangeRateList rmb2usd = GetMatchSolutionExchangeRate("RMB", "USD", _CacheInfo[companyID].RateList);
                if (rmb2usd == null)
                {
                    rmb2usd = new SolutionExchangeRateList();
                    rmb2usd.ID = Guid.NewGuid();
                    rmb2usd.SourceCurrency = "RMB";
                    rmb2usd.TargetCurrency = "USD";
                    rmb2usd.Rate = 1;

                }
                _BillRate.Add(rmb2usd);

                SolutionExchangeRateList usd2rmb = GetMatchSolutionExchangeRate("USD", "RMB", _CacheInfo[companyID].RateList);
                if (usd2rmb == null)
                {
                    usd2rmb = new SolutionExchangeRateList();
                    usd2rmb.ID = Guid.NewGuid();
                    usd2rmb.SourceCurrency = "USD";
                    usd2rmb.TargetCurrency = "RMB";
                    usd2rmb.Rate = 1;
                }
                _BillRate.Add(usd2rmb);

                SolutionExchangeRateList rmb2hkd = GetMatchSolutionExchangeRate("RMB", "HKD", _CacheInfo[companyID].RateList);
                if (rmb2hkd == null)
                {
                    rmb2hkd = new SolutionExchangeRateList();
                    rmb2hkd.ID = Guid.NewGuid();
                    rmb2hkd.SourceCurrency = "RMB";
                    rmb2hkd.TargetCurrency = "HKD";
                    rmb2hkd.Rate = 1;
                }
                _BillRate.Add(rmb2hkd);

                SolutionExchangeRateList hkd2rmb = GetMatchSolutionExchangeRate("HKD", "RMB", _CacheInfo[companyID].RateList);
                if (hkd2rmb == null)
                {
                    hkd2rmb = new SolutionExchangeRateList();
                    hkd2rmb.ID = Guid.NewGuid();
                    hkd2rmb.SourceCurrency = "HKD";
                    hkd2rmb.TargetCurrency = "RMB";
                    hkd2rmb.Rate = 1;
                }
                _BillRate.Add(hkd2rmb);
            }
            bsCurrencyRateData.DataSource = _BillRate;
            bsCurrencyRateData.ResetBindings(false);
        }

        public SolutionExchangeRateList GetMatchSolutionExchangeRate(string sourceCurrency, string targetCurrency, List<SolutionExchangeRateList> rateList)
        {
            if (string.IsNullOrEmpty(sourceCurrency) || string.IsNullOrEmpty(targetCurrency))
            {
                return null;
            }

            if (sourceCurrency == targetCurrency)
            {
                SolutionExchangeRateList exchangeRate = new SolutionExchangeRateList();
                exchangeRate.SourceCurrency = sourceCurrency;
                exchangeRate.TargetCurrency = targetCurrency;
                exchangeRate.Rate = 1m;
                return exchangeRate;
            }

            for (int i = 0; i < rateList.Count; i++)
            {
                if (rateList[i].SourceCurrency == sourceCurrency && rateList[i].TargetCurrency == targetCurrency)
                {
                    return rateList[i];
                }
            }

            return null;
        }

        private void BulidLogo()
        {
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + "\\Reports\\LOGO\\");
            FileInfo[] files = dir.GetFiles();
            foreach (var item in files)
            {
                if (item.Extension.ToUpper().Contains("GIF") == false) continue;
                cmbLogoFileName.Properties.Items.Add(new ImageComboBoxItem(item.Name.Substring(0, item.Name.Length - 4), item.FullName));
            }
            cmbLogoFileName.EditValueChanged += delegate
            {
                if (cmbLogoFileName.EditValue != null)
                {
                    pictureLogo.Image = Bitmap.FromFile(cmbLogoFileName.EditValue.ToString());
                }
            };
            cmbLogoFileName.EditValue = _CurrentData.LogoFileName = files[0].FullName;
        }

        #endregion

        #region IEditPart成员

        public override object DataSource
        {
            get
            {
                return bindingSource1.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        private void BindingData(object value)
        {
            _CurrentData = value as PrintBillConfigData;
            bindingSource1.DataSource = _CurrentData;
        }


        #endregion

        #region IPart 成员
        OperationCommonInfo _OperationCommonInfo = null;
        ConfigureInfo _ConfigureInfo = null;
        //List<SolutionExchangeRateList> _RateList = null;
        BillInfo _BillInfo = null;

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "BLCommonInfo")
                {
                    _OperationCommonInfo = item.Value as OperationCommonInfo;
                    _CurrentCompanyID = _OperationCommonInfo.CompanyID;
                }
                else if (item.Key == "ConfigureInfo")
                {
                    _ConfigureInfo = item.Value as ConfigureInfo;
                }
                //else if (item.Key == "SolutionExchangeRateList")
                //{
                //    _RateList = item.Value as List<SolutionExchangeRateList>;
                //}
                else if (item.Key == "BillInfo")
                {
                    _BillInfo = item.Value as BillInfo;
                }
            }
        }
        #endregion

        #region btn event

        private void txtSuffixNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ClickOK();

            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }

        private void ClickOK()
        {
            #region 构建币种换算
            StringBuilder strBulid = new StringBuilder();
            if (rdoCNEN.SelectedIndex == 0)
            {
                strBulid.Append("2.币种换算:");
            }
            else
            {
                strBulid.Append("2.Currency conversion:");
            }

            List<SolutionExchangeRateList> rates = bsCurrencyRateData.DataSource as List<SolutionExchangeRateList>;

            for (int i = 0; i < rates.Count; i++)
            {
                if (i >= 2 && i % 2 == 0) strBulid.Append("           ");

                ////strBulid.Append(rates[i].SourceCurrency + ":" + rates[i].TargetCurrency + "＝" + "1: " + rates[i].Rate.ToString("F2"));
                strBulid.Append(rates[i].SourceCurrency + ":" + rates[i].TargetCurrency + "＝" + (rates[i].IsValid ? ("1: " + rates[i].Rate.ToString("F2")) : (rates[i].Rate.ToString("F2") + ": 1")));
                if (i == 0 || i % 2 == 0) strBulid.Append("  ");
                else strBulid.Append("\r\n");
            }

            _CurrentData.RateInfo = strBulid.ToString();
            #endregion

            #region 构建提单号
            foreach (CheckedListBoxItem item in chkListBLNO.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    if (_CurrentData.BLNO.IsNullOrEmpty() == false) _CurrentData.BLNO += "," + item.Value.ToString();
                    else _CurrentData.BLNO += item.Value.ToString();
                }
            }
            #endregion

            #region 如果汇率有变动.就保存起来
            if (_SaveRate)
            {
                List<string> sourceCurrencys = new List<string>(), tagerCurrencys = new List<string>();
                List<decimal> rateValues = new List<decimal>();
                foreach (var item in rates)
                {
                    sourceCurrencys.Add(item.SourceCurrency);
                    tagerCurrencys.Add(item.TargetCurrency);
                    rateValues.Add(item.Rate);
                }
                FinanceReportService.SavePrintBillRateList(_CurrentData.BillID, sourceCurrencys.ToArray(), tagerCurrencys.ToArray(), rateValues.ToArray(), LocalData.UserInfo.LoginID);
            }
            #endregion
            _CurrentData.IsEN = rdoCNEN.SelectedIndex == 1;
            _CurrentData.PrintBillCacheInfo = _CacheInfo[_CurrentCompanyID];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        private void gvRate_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            _SaveRate = true;
        }

        #endregion
    }
}
