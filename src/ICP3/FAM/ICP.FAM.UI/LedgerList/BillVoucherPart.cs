using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;

namespace ICP.FAM.UI
{
    public partial class BillVoucherPart : BasePart
    {
        public BillVoucherPart()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
        }

        /// <summary>
        /// 工作类型 1 生成计账凭证 2 整理凭证 3 产生汇兑损益
        /// </summary>
        public int workType { get; set; }
        public static string ReturnMess { get; set; }

        private List<Guid> CompanyIDList
        {
            get
            {
                List<Guid> list = new List<Guid>();

                list.Add(new Guid("2B109BA9-D770-419D-9323-34EE1553FC2E"));   //  LA
                list.Add(new Guid("A2F4FE4F-DA13-44F6-8B9F-F07C189503C8"));   //  NJ
                list.Add(new Guid("E563E2C9-6CA7-412B-99A4-F246379720C0"));   //  VOR
                list.Add(new Guid("0501D29D-0EFE-E111-B376-0026551CA87B"));   //  BR
                list.Add(new Guid("5A827ADF-38C7-4A2F-99A7-AD717CE91718"));   //  VN
                list.Add(new Guid("1DBF7671-0D2D-4F08-A8A9-3663A0DB0037"));   //  ML
                list.Add(new Guid("82D15564-0786-E211-BCDE-0026551CA87B"));   //  AUS     

                return list;
            }
        }

        /// <summary>
        /// 是否海外解决方案下的口岸
        /// </summary>
        private bool ISoverSeasSolution
        {
            get
            {
                Guid companyID = DataTypeHelper.GetGuid(cmbCompany.EditValue, Guid.Empty);
                ConfigureInfo configInfo=configureService.GetCompanyConfigureInfo(companyID);
                return configInfo.SolutionID.Equals(new Guid("2A254061-0465-4B07-81CF-E18198B45802"));
            }
        }

        public ConfigureInfo configureInfo
        {
            get;
            set;
        }

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IBusinessInfoProviderFactory BusinessInfoProviderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            }
        }

        public IConfigureService configureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }


        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region 窗体事件

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Close();
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }
        private void InitControls()
        {
            ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID,LocalData.UserInfo.DefaultCompanyName);
            if (workType == 1)
            {
                for (int i = -4; i <= 0; i++)
                {
                    DateTime date = DateTime.Now.AddMonths(i);
                    string text = date.Year.ToString("####") + "-" + date.Month.ToString("##");
                    cmbMonth.Properties.Items.Add(new ImageComboBoxItem(text));
                }
                cmbMonth.SelectedIndex = 3;//默认为上个月的
            }
            if (workType == 3)
            {
                labDate.Visible = true;
                labMonth.Visible = false;
                cmbMonth.Visible = false;
                dtpChargingCloseDate.Visible = true;
            }
            else
            {
                for (int i = -24; i <= 0; i++)
                {
                    DateTime date = DateTime.Now.AddMonths(i);
                    string text = date.Year.ToString("####") + "-" + date.Month.ToString("##");
                    cmbMonth.Properties.Items.Add(new ImageComboBoxItem(text));
                }
                cmbMonth.SelectedIndex = 22;//默认为上个月的
            }
        }
        #endregion

        #region  确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbCompany.EditValue == null ||
                string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                XtraMessageBox.Show("请选择公司.");
                return;
            }
            if ((cmbMonth.EditValue == null ||
                string.IsNullOrEmpty(cmbMonth.EditValue.ToString())) && workType != 3)
            {
                XtraMessageBox.Show("请选择月份.");
                return;
            }
            int theradID = 0;
            if (workType == 2)
            {
                Guid companyID = DataTypeHelper.GetGuid(cmbCompany.EditValue);
                DateTime startDate = Convert.ToDateTime(DataTypeHelper.GetString(cmbMonth.EditValue) + "-01");
                string Date = startDate.ToString("yyMM");
                try
                {
                    string message = "正在整理{" + cmbCompany.Text + startDate.Year.ToString("####") + "年" + startDate.Month.ToString("##") + "月" + "}的凭证,请稍候...";
                    theradID = LoadingServce.ShowLoadingForm(message);

                    ReturnMess = FinanceService.ArrangeLedger(companyID, Date);
                    
                    if (FindForm() != null)
                    {
                        FindForm().DialogResult = DialogResult.OK;
                        FindForm().Close();
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
                finally
                {
                    if (theradID != 0)
                    {
                        LoadingServce.CloseLoadingForm(theradID);
                    }
                }
            }
            if (workType == 3)
            {
                if (ISoverSeasSolution)
                {
                    MessageBox.Show(LocalData.IsEnglish ? "Is Soverseas solution company,Do not need to Adjust Rate！" : "海外解决方案下口岸不需要生成汇兑损益！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                //如果能找到汇率的开始日期=会计关帐日+1天的信息，则汇率有更新
                List<SolutionExchangeRateList> rateList = configureService.GetCompanyExchangeRateList(configureInfo.CompanyID, true);
                int count = (from d in rateList
                                where d.FromDate.ToShortDateString() == dtpChargingCloseDate.DateTime.AddDays(1).ToShortDateString()
                                select d).Count();

                if (count == 0)
                {
                    MessageBox.Show(LocalData.IsEnglish ? "Rate has not been updated,Do not need to Adjust Rate！" : "汇率没有更新不需要生成汇兑损益！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                theradID = LoadingServce.ShowLoadingForm("正在生成汇兑损益凭证...");
                try
                {
                    FinanceService.AdjustRateForVoucher(configureInfo.CompanyID, dtpChargingCloseDate.DateTime, LocalData.UserInfo.LoginID);
                    if (FindForm() != null)
                    {
                        FindForm().DialogResult = DialogResult.OK;
                        FindForm().Close();
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "汇兑损益生成失败!" + Environment.NewLine + "错误信息:" + Environment.NewLine + ex.Message);
                    //DevExpress.XtraEditors.XtraMessageBox.Show("汇兑损益生成失败!" + System.Environment.NewLine + "错误信息:" + System.Environment.NewLine + ex.Message);
                }
                finally
                {
                    LoadingServce.CloseLoadingForm(theradID);
                }
            }
            else
            {
                
                try
                {
                    Guid companyID = DataTypeHelper.GetGuid(cmbCompany.EditValue);
                    DateTime startDate = Convert.ToDateTime(DataTypeHelper.GetString(cmbMonth.EditValue) + "-01");
                    DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                    ConfigureInfo configure = configureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID, LocalData.IsEnglish);
                    if (startDate< configure.ChargingClosingdate)
                    {
                        MessageBox.Show(LocalData.IsEnglish ? "The Voucher number should be generated after the account is closed." : "凭证号应该在会计关账后生成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string message = "正在生成{" + startDate.Year.ToString("####") + "年" + startDate.Month.ToString("##") + "月" + "}的账单凭证,请稍候...";
                    theradID = LoadingServce.ShowLoadingForm(message);

                    FinanceService.GenerateBillVoucher(companyID,startDate,endDate,LocalData.UserInfo.LoginID);

                    if (FindForm() != null)
                    {
                        FindForm().DialogResult = DialogResult.OK;
                        FindForm().Close();
                    }
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
                finally
                {
                    if (theradID != 0)
                    {
                        LoadingServce.CloseLoadingForm(theradID);
                    }
                }
            }
        }
        #endregion

        #region 选择的公司发生改变时
        /// <summary>
        ///  选择的公司发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCompanyChanged();
        }
        /// <summary>
        /// 选择的公司发生改变
        /// </summary>
        private void SelectCompanyChanged()
        {
            Guid companyID = Guid.Empty;
            if (cmbCompany.EditValue != null)
            {
                companyID = (Guid)cmbCompany.EditValue;
            }

            if (companyID == Guid.Empty)
            {
                return;
            }

            configureInfo = configureService.GetCompanyConfigureInfo(companyID);
            if (!ValidateCompanyHasConfig())
            {
                dtpChargingCloseDate.EditValue = DateTime.Now;
                return;
            }
            dtpChargingCloseDate.EditValue = configureInfo.ChargingClosingdate;
        }

        /// <summary>
        /// 验证当前选择的公司是否有公司配置信息
        /// </summary>
        /// <returns></returns>
        private bool ValidateCompanyHasConfig()
        {
            string message = string.Empty;
            if (configureInfo == null)
            {
                message = LocalData.IsEnglish ? "not find the company configuration information" : "没有找到对应的公司配置信息,请联系系统管理员";
                XtraMessageBox.Show(message);
                return false;
            }
            return true;
        }
        #endregion
    }
}
