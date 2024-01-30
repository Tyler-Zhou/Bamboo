using System;
using System.Drawing;
using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface;
using ICP.Common.UI;
using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents;

namespace ICP.FAM.UI
{
    /// <summary>
    /// 财务关账界面
    /// </summary>
    public partial class AccountsClosePart : BasePart
    {
        public AccountsClosePart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                configureInfo = null;
                cmbCompany.OnFirstEnter -= OnCompanyEnter;
                cmbCompany.SelectedIndexChanged -= cmbCompany_SelectedIndexChanged;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

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
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region 属性
        public ConfigureInfo configureInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 是否为会计关帐,区分计费关帐与会计关帐
        /// </summary>
        public bool IsAccountingClosing { get; set; }
        #endregion

        #region 初始化

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
                SetText();
                SetAccountPartVisible();
            }
        }

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
        /// 非中国大陆的口岸
        /// </summary>
        private bool IsNotChinaCompany
        {
            get
            {
                Guid companyID = DataTypeHelper.GetGuid(cmbCompany.EditValue, Guid.Empty);
                return CompanyIDList.Contains(companyID);
            }
        }

        private void SetText()
        {
            if (!LocalData.IsEnglish)
            {
                labCompany.Text = "公司";
                labChargingCloseDate.Text = "计费关帐日";
                labAccountingClosingDate.Text = "会计关帐日";
            }
        }
        private void OnCompanyEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            cmbCompany.OnFirstEnter += OnCompanyEnter;
            cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);

            if (LocalData.IsEnglish)
            {
                labCompany.Location = new Point(11, 12);
                labChargingCloseDate.Location = new Point(11, 45);
                labAccountingClosingDate.Location = new Point(11, 81);

                cmbCompany.Location = new Point(147, 9);
                dtpChargingCloseDate.Location = new Point(147, 42);
                dtpAccountingClosingDate.Location = new Point(147, 78);
            }
            else
            {
                labCompany.Location = new Point(31, 12);
                labChargingCloseDate.Location = new Point(31, 45);
                labAccountingClosingDate.Location = new Point(31, 81);

                cmbCompany.Location = new Point(127, 9);
                dtpChargingCloseDate.Location = new Point(127, 42);
                dtpAccountingClosingDate.Location = new Point(127, 78);
            }
        }
        private void SetAccountPartVisible()
        {
            //if (IsNotChinaCompany)
            //{
            //    this.Height = 120;
            //    this.FindForm().Height = 170;
            //    this.labAccountingClosingDate.Visible = false;
            //    this.dtpAccountingClosingDate.Visible = false;

            //    this.btnOK.Location = new System.Drawing.Point(43, 80);
            //    this.btnClose.Location = new System.Drawing.Point(176, 80);
            //}
            //else
            //{
            Height = 150;
            FindForm().Height = 200;
            labAccountingClosingDate.Visible = true;
            dtpAccountingClosingDate.Visible = true;

            btnOK.Location = new Point(43, 110);
            btnClose.Location = new Point(176, 110);
            //}
        }

        #endregion

        #region 关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            FindForm().Close();
        }
        #endregion

        #region 确定
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!SaveData())
            {
                return;
            }

            FindForm().Close();
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private bool SaveData()
        {
            if (!ValidateData())
            {
                return false;
            }

            #region  提示
            if (!LocalData.IsEnglish)
            {
                string message = string.Empty;
                message = "确认要关帐?";
                message = message + Environment.NewLine + "计费关帐到 " + dtpChargingCloseDate.DateTime.Year.ToString() + "年" + dtpChargingCloseDate.DateTime.Month + "月";
                message = message + Environment.NewLine + "会计关帐到 " + dtpAccountingClosingDate.DateTime.Year.ToString() + "年" + dtpAccountingClosingDate.DateTime.Month + "月";

                if (dtpAccountingClosingDate.DateTime.ToShortDateString() == dtpChargingCloseDate.DateTime.ToShortDateString())
                {
                    IsAccountingClosing = true;
                }
                if (!FAMUtility.ShowResultMessage(message))
                {
                    return false;
                }
            }
            #endregion

            #region 验证信息
            configureInfo.ChargingClosingdate = dtpChargingCloseDate.DateTime;
            configureInfo.AccountingClosingdate = dtpAccountingClosingDate.DateTime;
            #endregion

            #region 保存公司配置
            try
            {
                ConfigureSaveRequest saveRequest = new ConfigureSaveRequest()
                {
                    ID = configureInfo.ID,
                    CompanyID = configureInfo.CompanyID,
                    CustomerID = configureInfo.CustomerID,
                    StandardCurrencyID = configureInfo.StandardCurrencyID,
                    DefaultCurrencyID = configureInfo.DefaultCurrencyID,
                    SolutionID = configureInfo.SolutionID,
                    IssuePlaceID = configureInfo.IssuePlaceID,
                    BusinessClosingDate = configureInfo.BusinessClosingDate,
                    ChargingClosingDate = configureInfo.ChargingClosingdate,
                    AccountingClosingDate = configureInfo.AccountingClosingdate,
                    ShortCode = configureInfo.ShortCode,
                    DefaultAgentDescription = configureInfo.DefaultAgentDescription,
                    BLTitleID = configureInfo.BLTitleID,
                    IsVATinvoice = configureInfo.IsVATinvoice,
                    VATFEEID = configureInfo.VATFeeID,
                    VATrateAP = configureInfo.VATrate,
                    CMBNetComUserID = configureInfo.CMBNetComUserID,
                    SaveByID = LocalData.UserInfo.LoginID,
                    UpdateDate = configureInfo.UpdateDate,
                };
                SingleResultData ResultData = ConfigureService.SaveConfigureInfo(saveRequest);

                configureInfo.UpdateDate = ResultData.UpdateDate;

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
            #endregion
            return true;
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
                MessageBoxService.ShowError(message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            string message = string.Empty;
            if (!ValidateCompanyHasConfig())
            {
                return false;
            }

            //bug3465:北美区和加拿大解决方案下的公司，计费关帐日允许往前调整。(By Pearl)
            if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0") &&
                configureInfo.ChargingClosingdate > dtpChargingCloseDate.DateTime)
            {
                message = LocalData.IsEnglish ? "Accounting closing date can not be less than before the Closing Date" : "关帐日期不能小于之前的关帐日期";
                MessageBoxService.ShowWarning(message);
                return false;
            }

            return true;
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

            SetAccountPartVisible();

            configureInfo = ConfigureService.GetCompanyConfigureInfo(companyID);
            if (!ValidateCompanyHasConfig())
            {
                dtpChargingCloseDate.EditValue = null;
                dtpAccountingClosingDate.EditValue = null;
                return;
            }
            dtpChargingCloseDate.EditValue = configureInfo.ChargingClosingdate;
            dtpAccountingClosingDate.EditValue = configureInfo.AccountingClosingdate;
        }
        #endregion


    }
}
