using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 查询合约面板
    /// </summary>
    [ToolboxItem(false)]
    public partial class OPSearchPart : BaseSearchPart
    {
        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 海运运价管理服务
        /// </summary>
        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }
        /// <summary>
        /// 菜单配置管理服务
        /// </summary>
        public IPermissionService PermissionService
        {
            get
            {
                return ServiceClient.GetService<IPermissionService>();
            }
        }
        /// <summary>
        /// 合约UI辅助服务
        /// </summary>
        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }

        #endregion

        #region Delegate
        /// <summary>
        /// 查询数据之前
        /// </summary>
        public event CancelEventHandler BeForeSearchData;
        /// <summary>
        /// 查询数据
        /// </summary>
        public override event SearchResultHandler OnSearched;
        #endregion

        #region  init
        /// <summary>
        /// 查询合约面板
        /// </summary>
        public OPSearchPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                BeForeSearchData = null;
                OnSearched = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (!DesignMode) { InitMessage(); }
        }
        /// <summary>
        /// 重写加载，初始化控
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                txtCarrier.ToolTip = txtPOD.ToolTip = txtPOL.ToolTip = txtVia.ToolTip =
                txtDelivery.ToolTip = NativeLanguageService.GetText(this, "SearchBoxToolTip");

                Utility.SearchPartKeyEnterToSearch(new List<Control> 
                {
                    txtCarrier,txtName,txtNO,txtPOD,txtPOL,txtPublisher,txtVia
                }, btnSearch);

                Utility.SearchPartKeyF2ToSearch(new List<Control> 
                {
                   txtCarrier,txtName,txtNO,txtPOD,txtPOL,txtPublisher,txtVia
                }, btnClean);
                InitControls();
            }
        }

        #endregion

        #region event
        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bool isCancel = false;
                if (BeForeSearchData != null)
                {
                    CancelEventArgs ce = new CancelEventArgs();
                    BeForeSearchData(this, ce);
                    isCancel = ce.Cancel;
                }

                if (isCancel) return;

                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }
            }
        }
        /// <summary>
        /// 清空查询
        /// </summary>
        private void btnClean_Click(object sender, EventArgs e)
        {
            chkDate.Checked = true;
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is TextEdit
                    && (item is SpinEdit) == false
                    && (item is ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            cmbShipLine.SelectedIndex = cmbState.SelectedIndex = cmbType.SelectedIndex = 0;


            dteFrom.DateTime = DateTime.Now;
            dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddDays(14);
        }
        /// <summary>
        /// 启用、禁用时间范围查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDate_CheckedChanged(object sender, EventArgs e)
        {
            dteFrom.Enabled = dteTo.Enabled = chkDate.Checked;
        }

        #endregion

        #region ISearchPart 成员
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            try
            {
                DateTime? from = null, to = null;
                if (chkDate.Checked)
                {
                    from = dteFrom.DateTime.Date;
                    to = Utility.GetEndDate(dteTo.DateTime);
                }

                RateType? rateType = null;
                if (cmbType.EditValue != null && cmbType.EditValue != DBNull.Value)
                    rateType = (RateType)cmbType.EditValue;

                OceanState? OceanState = null;
                if (cmbState.EditValue != null && cmbState.EditValue != DBNull.Value)
                    OceanState = (OceanState)cmbState.EditValue;


                Guid? shipplineID = null;
                if (cmbShipLine.EditValue != null && cmbShipLine.EditValue != DBNull.Value)
                    shipplineID = new Guid(cmbShipLine.EditValue.ToString());

                List<OceanList> list = OceanPriceService.GetOceanList(txtNO.Text.Trim()
                                    , txtName.Text.Trim()
                                    , txtCarrier.Text.Trim()
                                    , shipplineID
                                    , txtPOL.Text.Trim()
                                    , txtVia.Text.Trim()
                                    , txtPOD.Text.Trim()
                                    , txtDelivery.Text.Trim()
                                    , rateType
                                    , OceanState
                                    , from
                                    , to
                                    , txtPublisher.EditText
                                    , LocalData.UserInfo.LoginID);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), list.Count + " record  found ");

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); return null; }

        }

        /// <summary>
        /// 
        /// </summary>
        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        #endregion

        #region Method
        /// <summary>
        /// 注册提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("SearchBoxToolTip", "Please input Code, Chinese name or English name.");
        }

        /// <summary>
        /// 初始化控件值
        /// </summary>
        private void InitControls()
        {
            #region RateType
            List<EnumHelper.ListItem<RateType>> contractTypes = EnumHelper.GetEnumValues<RateType>(LocalData.IsEnglish);
            cmbType.Properties.BeginUpdate();
            cmbType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "ALL", DBNull.Value));
            foreach (var item in contractTypes)
            {
                cmbType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbType.SelectedIndex = 0;
            cmbType.Properties.EndUpdate();

            #endregion

            #region OceanState
            List<EnumHelper.ListItem<OceanState>> OceanStates = EnumHelper.GetEnumValues<OceanState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();
            cmbState.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "ALL", DBNull.Value));
            foreach (var item in OceanStates.Where(item => item.Value != OceanState.Expired))
            {
                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();
            cmbState.SelectedIndex = 2;
            #endregion

            #region ShipLine

            Utility.SetEnterToExecuteOnec(cmbShipLine, delegate
            {
                List<ShippingLineList> shippingLines = OceanPriceUIDataHelper.ShippingLines;
                ShippingLineList emptySL = new ShippingLineList { ID = Guid.Empty };
                emptySL.EName = emptySL.CName = emptySL.Code = string.Empty;

                cmbShipLine.Properties.BeginUpdate();

                shippingLines.Insert(0, emptySL);

                foreach (ShippingLineList item in shippingLines)
                {
                    cmbShipLine.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
                }
                cmbShipLine.Properties.EndUpdate();
            }
            );
            #endregion

            #region  Publisher 只有 [维护所有人的运价]可以查到别人的运价

            txtPublisher.EditText = LocalData.UserInfo.LoginName;
            txtPublisher.EditValue = LocalData.UserInfo.LoginID;

            if (LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.FRM_EDITALLCONTRACT) == false)
            {
                txtPublisher.Enabled = false;
            }
            else
            {
                txtPublisher.Enabled = true;

                //揽货人
                Utility.SetEnterToExecuteOnec(txtPublisher, delegate
                {
                    List<ModuleUserList> users = PermissionService.GetModuleUserList(FunctionConstants.FRM_OCEANPRICELIST, null, 0);
                    ModuleUserList currentUser = users.Find(item => item.ID == LocalData.UserInfo.LoginID);
                    Dictionary<string, string> col = new Dictionary<string, string>
                    {
                        {"EName", "Name"},
                        {"Code", "Code"}
                    };
                    txtPublisher.InitSource(users, col, "EName", "ID");
                });
            }
            #endregion

            chkDate.Checked = dteFrom.Enabled = dteTo.Enabled = true;
            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            dteTo.DateTime = Utility.GetEndDate(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddDays(14));
        }

        #endregion
    }
}
