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
using ICP.Common.UI;
using ICP.Common.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents;
using System.Windows.Forms;

namespace ICP.FRM.UI
{
    /// <summary>
    /// 商务关账界面
    /// </summary>
    public partial class BusinessClosePart : BasePart
    {
        /// <summary>
        /// 商务关账
        /// </summary>
        public BusinessClosePart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                _ConfigureInfo = null;
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
        /// <summary>
        /// WorkItem服务
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 配置对象
        /// </summary>
        ConfigureInfo _ConfigureInfo
        {
            get;
            set;
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                message ="商务关帐到 " + dtpBusinessCloseDate.DateTime.Year.ToString() + "年" + dtpBusinessCloseDate.DateTime.Month + "月";

                if ((MessageBoxService.ShowQuestion(message,"确认要关帐",MessageBoxButtons.YesNo))==DialogResult.No)
                {
                    return false;
                }
            }
            #endregion
            _ConfigureInfo.BusinessClosingDate = dtpBusinessCloseDate.DateTime;


            #region 保存公司配置
            try
            {
                ConfigureSaveRequest saveRequest = new ConfigureSaveRequest()
                {
                    ID = _ConfigureInfo.ID,
                    CompanyID = _ConfigureInfo.CompanyID,
                    CustomerID = _ConfigureInfo.CustomerID,
                    StandardCurrencyID = _ConfigureInfo.StandardCurrencyID,
                    DefaultCurrencyID = _ConfigureInfo.DefaultCurrencyID,
                    SolutionID = _ConfigureInfo.SolutionID,
                    IssuePlaceID = _ConfigureInfo.IssuePlaceID,
                    BusinessClosingDate = _ConfigureInfo.BusinessClosingDate,
                    ChargingClosingDate = _ConfigureInfo.ChargingClosingdate,
                    AccountingClosingDate = _ConfigureInfo.AccountingClosingdate,
                    ShortCode = _ConfigureInfo.ShortCode,
                    DefaultAgentDescription = _ConfigureInfo.DefaultAgentDescription,
                    BLTitleID = _ConfigureInfo.BLTitleID,
                    IsVATinvoice = _ConfigureInfo.IsVATinvoice,
                    VATFEEID = _ConfigureInfo.VATFeeID,
                    VATrateAP = _ConfigureInfo.VATrate,
                    CMBNetComUserID = _ConfigureInfo.CMBNetComUserID,
                    SaveByID = LocalData.UserInfo.LoginID,
                    UpdateDate = _ConfigureInfo.UpdateDate,
                };
                SingleResultData ResultData = ConfigureService.SaveConfigureInfo(saveRequest);

                _ConfigureInfo.UpdateDate = ResultData.UpdateDate;

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
            if (_ConfigureInfo == null)
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


            _ConfigureInfo = ConfigureService.GetCompanyConfigureInfo(companyID);
            if (!ValidateCompanyHasConfig())
            {
                dtpBusinessCloseDate.EditValue = null;
                return;
            }
            dtpBusinessCloseDate.EditValue = _ConfigureInfo.BusinessClosingDate;
        }
        #endregion


    }
}
