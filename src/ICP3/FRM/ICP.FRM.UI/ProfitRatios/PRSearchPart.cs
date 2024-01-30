using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Server;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FRM.UI.ProfitRatios
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class PRSearchPart : BaseSearchPart
    {
        #region 成员
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        /// <summary>
        /// 基础数据服务管理
        /// </summary>
        private ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 客户服务
        /// </summary>
        private ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        /// <summary>
        /// 利润配比服务
        /// </summary>
        private IProfitRatiosService ProfitRatiosService
        {
            get
            {
                return ServiceClient.GetService<IProfitRatiosService>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private List<Guid> CompanyIDs
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


        /// <summary>
        /// 
        /// </summary>
        private DateTime? BeginTime
        {
            get
            {
                return dmcGateInRange.From;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private DateTime? EndTime
        {
            get
            {
                return dmcGateInRange.To;
            }
        }
        #endregion

        #region init 
        /// <summary>
        /// 
        /// </summary>
        public PRSearchPart()
        {
            InitializeComponent();
            Disposed += delegate {
                OnSearched = null;
                RemoveKeyDownHandle();
                RemoveControlEnterHandle();
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

        }
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
                SetKeyDownToSearch();
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in panelBase.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in panelBase.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }
        private void InitControls()
        {
            SetCompany();
            //cbcbCarrier.IsDefaultAllCheck = true;
            //tccShippingLine.IsDefaultAllCheck = true;
            dmcGateInRange.IsEngish = LocalData.IsEnglish;
            dmcGateInRange.From = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            dmcGateInRange.To = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddDays(7);
            SetControlsEnterToSearch();
        }

        private void SetControlsEnterToSearch()
        {
            KeyDown += OnControlKeyDown;
            foreach (Control item in panelForm.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown += OnControlKeyDown;
                }
            }
        }
        private void OnControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
        }
        private void RemoveControlEnterHandle()
        {
            KeyDown -= OnControlKeyDown;
            foreach (Control item in panelBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown -= OnControlKeyDown;

                }
            }
        }

        #endregion

        #region ISearchPart 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            if (CompanyIDs.Count == 0)
            {
                throw new Exception(LocalData.IsEnglish ? "At least one company must be selected." : "必须至少选择一个操作口岸.");
            }
            QueryCriteria4ProfitRatios queryParamater = new QueryCriteria4ProfitRatios()
            {
                CompanyIDs = CompanyIDs.ToArray(),
                CarrierIDs = cbcbCarrier.SelectValuesToGuid,
                VesselIDs = cbcbVessel.SelectValuesToGuid,
                ShippingLineIDs = tccShippingLine.EditValue.ToArray(),
                POLIDs = cbcbPOLID.SelectValuesToGuid,
                PODIDs = cbcbPODID.SelectValuesToGuid,
                PlaceOfDeliveryIDs = cbcbPlaceOfDeliveryID.SelectValuesToGuid,
                ContractNo = txtContractNo.Text,
                IsNonContractNo = checkIsNonContractNo.Checked,
                BookingNo = txtBookingNo.Text,
                OperationNo = txtOperationNo.Text,
                BeginTime = BeginTime,
                EndTime = EndTime,
            };
            return ProfitRatiosService.GetBusinessStatisticsList(queryParamater);
        }
        /// <summary>
        /// 
        /// </summary>
        public override event SearchResultHandler OnSearched;
        /// <summary>
        /// 
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    if (OnSearched != null)
                        OnSearched(this, GetData());
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control item in panelBase.Controls)
                {
                    if (item is LWImageComboBoxEdit)
                    {
                        (item as LWImageComboBoxEdit).SelectedIndex = 0;
                    }
                    else if (item is MultiSearchCommonBox)
                    {
                        MultiSearchCommonBox msc = (MultiSearchCommonBox)item;
                        msc.ShowSelectedValue(null, string.Empty);
                    }
                    else if (item is DevExpress.XtraEditors.TextEdit
                        && (item is DevExpress.XtraEditors.SpinEdit) == false
                        && item.Enabled == true
                        && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                        item.Text = string.Empty;

                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        #endregion

        #region 查询
        /// <summary>
        /// 口岸
        /// </summary>
        /// <returns></returns>
        private List<LocalOrganizationInfo> SetCompany()
        {
            List<LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Clear();
            foreach (var item in userCompanyList)
            {
                if (item.ID == LocalData.UserInfo.DefaultCompanyID)
                {
                    chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);
                }
                else
                {
                    chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                       CheckState.Unchecked, true);
                }
            }

            chkcmbCompany.Properties.EndUpdate();
            return userCompanyList;
        }
        
        #endregion
    }
}
