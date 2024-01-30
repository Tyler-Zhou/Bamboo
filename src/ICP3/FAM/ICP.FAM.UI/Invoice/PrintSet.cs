using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
namespace ICP.FAM.UI.Invoice
{
    [ToolboxItem(false)]
    public partial class PrintSet : BaseEditPart
    {
        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }



        public RateHelper RateHelper
        {
            get {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        public override event SavedHandler Saved;
        #endregion

        public PrintSet()
        {
            InitializeComponent();
            Disposed += delegate {
                Saved = null;
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
            if (!DesignMode)
            {
                Init();
                BindCurrency();
                //rad.SelectedIndex = 1;

                if (_invoiceFeeDate != null)
                {
                    chkCurrency.Value = _invoiceFeeDate.CurrencyID;
                    txtRate.Text = _invoiceFeeDate.Rate.ToString("F2");//保留两位小数
                }

                chkProxy.Checked = true;
                chkCompany.Checked = true;
                if (LocalData.UserInfo.DefaultCompanyID == _daLianCompanyID) //大连公司
                {
                    chkCurrencyTotal.Checked = false;
                    rad.SelectedIndex = 0;
                    radReport.SelectedIndex = 1;
                }
                else
                {
                    chkCurrencyTotal.Checked = true;
                    rad.SelectedIndex = 1;
                }

                chkCurrency.SelectedIndexChanged += new EventHandler(chkCurrency_SelectedIndexChanged);
            }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource; 
            }
            set 
            {
                //bsList.DataSource = value;
                _invoiceFeeDate = value as InvoiceFeeDate;
            }
        }

        InvoiceFeeDate _invoiceFeeDate = null;
        /// <summary>
        /// 大连公司ID
        /// </summary>
        Guid _daLianCompanyID = new Guid("b1afad8f-55dd-4e29-a250-eb82ab3971fe");

        Guid RMBCurrencyID
        {
            get
            {
                if (_currencyList != null)
                {
                    return _currencyList.Find(t => t.CurrencyName == "RMB").CurrencyID;
                }

                return Guid.Empty;
            }
        }

        #region 初始化
        private void Init()
        {
            labCurrency.Text = LocalData.IsEnglish ? "Currency" : "折合币种";
            labRate.Text = LocalData.IsEnglish ? "Rate" : "汇率";
            chkProxy.Text = LocalData.IsEnglish ? "Include Deputize" : "添加代理文本";
            chkCompany.Text = LocalData.IsEnglish ? "Include Company Name" : "显示公司名称";
            chkCurrencyTotal.Text = LocalData.IsEnglish ? "Show RMB Total" : "显示本位币汇总";
        }

        Dictionary<Guid, string> _dicCurrency = new Dictionary<Guid, string>();
        List<SolutionCurrencyList> _currencyList = null;
        private void BindCurrency()
        {
            _dicCurrency = new Dictionary<Guid, string>();
            try
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                chkCurrency.Properties.Items.Clear();

                if (configureInfo != null)
                {
                    _currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
                    if (_currencyList != null)
                    {
                        foreach (var item in _currencyList)
                        {
                            chkCurrency.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));

                            _dicCurrency.Add(item.CurrencyID, item.CurrencyName);
                        }

                    }

                }

            }
            catch
            {
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                chkCurrency.Properties.Items.Clear();

                if (configureInfo != null)
                {
                    _currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
                    if (_currencyList != null)
                    {
                        foreach (var item in _currencyList)
                        {
                            chkCurrency.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));

                            _dicCurrency.Add(item.CurrencyID, item.CurrencyName);
                        }

                    }

                }
            }
        }
        # endregion
        #region 本地方法
        //获取打印参数
        public string[] GetConfigs()
        {
            string[] configs = new string[7];
            if (chkProxy.Checked)
            {
                configs[0] = "true";
            }
            else
            {
                configs[0] = "false";
            }//代理文本
            if (chkCurrencyTotal.Checked)
            {
                configs[1] = "true";
            }
            else
            {
                configs[1] = "false";
            }//本位币(RMB)
            if (chkCompany.Checked)
            {
                configs[2] = "true";
            }
            else
            {
                configs[2] = "false";
            }//公司
            configs[3] = GetCurrency();//币种
            configs[4] = GetRate();//汇率     
            configs[5] = GetCheckStyle().ToString();//格式
            configs[6] = GetCheckReportStyle().ToString();//报表种类
            return configs;
        }
        public string GetCurrency()
        {
            return chkCurrency.Text.ToString();
        }
        public string GetRate()
        {
            return txtRate.Text.Trim();
        }//汇率

        public CheckStype GetCheckStyle()
        {
            if (rad.SelectedIndex == 1)
            {
                return CheckStype.New;
            }
            else
            {
                return CheckStype.Old;
            }
        }//格式
        public CheckReport GetCheckReportStyle()
        {
            if (radReport.SelectedIndex == 0)
            {
                return CheckReport.Own;
            }
            if (radReport.SelectedIndex == 1)
            {
                return CheckReport.Chromatography;
            }
            else
            {
                return CheckReport.Ship;
            }
        }//报表
        #endregion

        private void btnCancle_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }

        #region enum
        public enum CheckStype
        {
            Old,
            New
        }//样式
        public enum CheckReport
        {
            Own,//运输发票(自有格式)
            Chromatography,
            Ship
        }//报表选择
        #endregion

        private void chkCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {

            Guid SourceCurrencyId = (Guid)chkCurrency.Value;
            GetExchangeRate(SourceCurrencyId);

        }
        private void GetExchangeRate(Guid SourceCurrencyId)
        {
            if (string.IsNullOrEmpty(chkCurrency.Text) == false)
            {
                try
                {
                    DateTime time;
                    List<SolutionExchangeRateList> rate;
                    time = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);
                    //获取汇率列表
                    rate = ConfigureService.GetCompanyExchangeRateList(LocalData.UserInfo.DefaultCompanyID, true);
                    //Guid TargetCurrencyId = new Guid("5e317b57-f8c4-4551-953f-78325d3ba5b4");//RMB为目标币种
                    Guid TargetCurrencyId = RMBCurrencyID;
                    //获取相关币种汇率
                    decimal rate1 = RateHelper.GetRate(SourceCurrencyId, TargetCurrencyId, time, rate);
                    txtRate.Text = rate1.ToString("F2");//保留两位小数
                }
                catch (Exception)
                {
                    string message = LocalData.IsEnglish ? "Business-owned company does not have to configure the currency exchange rate" : "业务所属公司没有配置该币种汇率";
                    FAMUtility.ShowMessage(message);
                    txtRate.Text = "0";
                    return;
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string[] configs = GetConfigs();
            if (Saved != null) Saved(new object[] { configs });
            FindForm().DialogResult = DialogResult.OK;
            FindForm().Close();
        }
    }
}
