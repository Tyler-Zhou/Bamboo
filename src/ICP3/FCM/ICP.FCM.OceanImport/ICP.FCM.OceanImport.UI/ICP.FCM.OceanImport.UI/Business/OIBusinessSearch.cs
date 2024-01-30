using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Practices.CompositeUI;

using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.DataCache.ServiceInterface;
using System.Diagnostics;
using System.Reflection;

namespace ICP.FCM.OceanImport.UI
{
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OIBusinessSearch : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
            }
        }


        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();
            }
        }


        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }


        /// <summary>
        /// 选择的方案发生改变时
        /// </summary>
        public event SelectedHandler ProgramSelectedChanged;
        #endregion

        #region 初始化
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;
        public OIBusinessSearch()
        {
            InitializeComponent();
            if (LocalData.IsDesignMode) return;

            this.Disposed += delegate
            {
                this.ProgramSelectedChanged = null;
                this.OnSearched = null;
                this.RemoveKeyDownHandle();
                this.pnlMain.SizeChanged -= this.pnlMain_SizeChanged;
                this.cmbCompany.SelectedIndexChanged -= this.cmbCompany_SelectedIndexChanged;
                this.cmbCompany.OnFirstEnter -= this.OncmbCompanyFirstEnter;
                this.RemoveControlsEnterHandle();
                this.cmbDate.OnFirstEnter -= this.OncmbDateFirstEnter;
                this.cmbFiler.OnFirstEnter -= this.OncmbFilerFirstEnter;
                this.cmbprogram.SelectedIndexChanged -= this.cmbProgram_SelectedIndexChanged;
                this.cmbprogram.OnFirstEnter -= this.OncmbprogramFirstEnter;
                this.cmbSales.Enter -= this.cmbSales_Enter;
                this.cmbState.OnFirstEnter -= this.OncmbStateFirstEnter;

                if (Workitem != null)
                {

                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                this.fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;
                InitControls();
                SetKeyDownToSearch();
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in panel1.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in panel1.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }
        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2)
            {
                this.btnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                this.btnClear.PerformClick();
            }
        }

        public Guid companyID
        {
            get
            {
                if (this.cmbCompany.EditValue == null || (Guid)this.cmbCompany.EditValue == Guid.Empty)
                {
                    return LocalData.UserInfo.DefaultCompanyID;
                }
                else
                {
                    return (Guid)this.cmbCompany.EditValue;
                }
            }
        }
        private void BindState()
        {
            //状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIOrderState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();

            foreach (var item in orderStates)
            {
                if (item.Value == OIOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();
        }
        private void BindProgram()
        {
            this.cmbprogram.Properties.BeginUpdate();
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<ReleaseCargoProgram>> releasePrograms = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<ReleaseCargoProgram>(LocalData.IsEnglish);
            foreach (var item in releasePrograms)
            {
                if (item.Value == ReleaseCargoProgram.Custom)
                    continue;
                this.cmbprogram.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbprogram.Properties.EndUpdate();
        }
        private void OncmbCustomerServiceFirstEnter(object sender, EventArgs e)
        {
            this.BindCustomerService();
        }
        private void OncmbFilerFirstEnter(object sender, EventArgs e)
        {
            BindFiler();
        }
        private void OncmbStateFirstEnter(object sender, EventArgs e)
        {
            BindState();
        }
        private void OncmbprogramFirstEnter(object sender, EventArgs e)
        {
            BindProgram();
        }
        private void OncmbDateFirstEnter(object sender, EventArgs e)
        {
            BindDate();
        }
        /// <summary>
        /// 加载控件
        /// </summary>
        private void InitControls()
        {
            //注册延迟加载
            SetLazyLoaders();

            SetLazyDataLodersWithDynamicCondition();

            //客服
            this.cmbCustomerService.OnFirstEnter += this.OncmbCustomerServiceFirstEnter;
           

            //文件
            this.cmbFiler.OnFirstEnter += this.OncmbFilerFirstEnter;
            //状态
            this.cmbState.ShowSelectedValue(System.DBNull.Value, LocalData.IsEnglish ? "ALL" : "全部");

            this.cmbState.OnFirstEnter += this.OncmbStateFirstEnter;
           
            //放货方案
            this.cmbprogram.ShowSelectedValue(ReleaseCargoProgram.Custom, LocalData.IsEnglish ? "Custom" : "自定义");
            this.cmbprogram.OnFirstEnter += this.OncmbprogramFirstEnter;
            
            //时间
            this.cmbDate.ShowSelectedValue(OIBusinessDateSearchType.CreateDate, LocalData.IsEnglish ? "Create Date" : "创建日期");
            this.cmbDate.OnFirstEnter += this.OncmbDateFirstEnter;

            SetControlsEnterToSearch();
        }
        private void BindDate()
        {
            //时间
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
            cmbDate.Properties.BeginUpdate();
            foreach (var item in dateSearchTypes)
            {
                if (item.Value == OIBusinessDateSearchType.All || item.Value == OIBusinessDateSearchType.CreateDate)
                {
                    continue;
                }
                cmbDate.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDate.Properties.EndUpdate();
        }

        /// <summary>
        /// 绑定客服
        /// </summary>
        private void BindCustomerService()
        {
            ICPCommUIHelper.SetMcmbUsers(this.cmbCustomerService, companyID, "客服", string.Empty, true);
        }
        private void BindFiler()
        {
            ICPCommUIHelper.SetMcmbUsers(this.cmbFiler, companyID, "文件", string.Empty, true);
        }
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            this.txtAgent.Text = string.Empty;
            this.txtCustomer.Text = string.Empty;
            this.txtNo.Text = string.Empty;
            this.txtCtnNO.Text = string.Empty;
            this.txtBLNO.Text = string.Empty;
            this.txtShipper.Text = string.Empty;
            this.txtConsignee.Text = string.Empty;
            this.txtLoadingPort.Text = string.Empty;
            this.txtDischargePort.Text = string.Empty;
            this.txtPlaceOfDelivery.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "No":
                        this.txtNo.Text = value;
                        break;
                    case "MBLNo":
                        this.txtBLNO.Text = value;
                        break;
                    case "SubNo":
                        this.txtBLNO.Text = value;
                        break;
                    case "ContainerNo":
                        this.txtCtnNO.Text = value;
                        break;
                    case "CustomerName":
                        this.txtCustomer.Text = value;
                        break;
                    case "AgentName":
                        this.txtAgent.Text = value;
                        break;
                    case "ConsigneeName":
                        this.txtConsignee.Text = value;
                        break;
                    case "ShipperName":
                        this.txtShipper.Text = value;
                        break;
                    case "POLName":
                        this.txtLoadingPort.Text = value;
                        break;
                    case "PODName":
                        this.txtDischargePort.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        this.txtPlaceOfDelivery.Text = value;
                        break;
                }
            }
        }

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        private void OncmbCompanyFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
            cmbCompany.Properties.BeginUpdate();
            cmbCompany.Properties.Items.Clear();
            cmbCompany.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(string.Empty, null));

            foreach (var item in userCompanyList)
            {
                cmbCompany.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? item.EShortName : item.CShortName, item.ID));
            }

            cmbCompany.Properties.EndUpdate();
        }
        /// <summary>
        /// 注册延迟加载的数据源
        /// </summary>
        void SetLazyLoaders()
        {
            this.cmbCompany.SelectedIndexChanged -= this.cmbCompany_SelectedIndexChanged;
            this.cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);
            this.cmbCompany.SelectedIndexChanged += this.cmbCompany_SelectedIndexChanged;
            ////操作公司列表   
            this.cmbCompany.OnFirstEnter += this.OncmbCompanyFirstEnter;
  
        }

        /// <summary>
        /// 延迟加载，而且条件是动态的
        /// </summary>
        void SetLazyDataLodersWithDynamicCondition()
        {
            this.cmbSales.Enter += new EventHandler(cmbSales_Enter);
        }
        /// <summary>
        /// 加载揽货人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cmbSales_Enter(object sender, EventArgs e)
        {
            Guid depID = Guid.Empty;
            if (cmbCompany.EditValue != null && string.IsNullOrEmpty(cmbCompany.EditValue.ToString()))
            {
                depID = (Guid)cmbCompany.EditValue;
            }
            ICPCommUIHelper.SetComboxUsers(cmbSales, depID, string.Empty, string.Empty, true);
        }

        /// <summary>
        /// 设置一些TextBox输入Enter键时,触发查询事件
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown += this.OnItemKeyDown;
                  
                }
            }
        }
        private void RemoveControlsEnterHandle()
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown -= this.OnItemKeyDown;

                }
            }
        }
        private void OnItemKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.btnSearch.PerformClick();
        }

        #endregion

        #region 清空
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;

            }

            lwchkIsValid.Checked = null;
        }

        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {

            if (OnSearched != null)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    OnSearched(this, GetData());

                    MethodBase method = MethodBase.GetCurrentMethod();
                    StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "SEARCH","海进业务查找");

                }
            }
        }

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            DateTime? dtFrom = null, dtTo = null;
            OIBusinessDateSearchType dateSearchType = OIBusinessDateSearchType.All;
            OIOrderState? orderState = null;
            Guid? customerServiceID = null;
            Guid? filerID = null;
            Guid? salesID = null;
            Guid[] companyIDs = null;

            if (this.cmbCompany.EditValue != null)
            {
                companyIDs = new Guid[1];
                companyIDs[0] = new Guid(this.cmbCompany.EditValue.ToString());
            }
            else
            {
                companyIDs = Utility.GetCompanyIDs(UserService);
            }

            if (this.cmbSales.EditValue != null && new Guid(this.cmbSales.EditValue.ToString()) != Guid.Empty)
            {
                salesID = new Guid(this.cmbSales.EditValue.ToString());
            }
            if (this.cmbCustomerService.EditValue != null && new Guid(this.cmbCustomerService.EditValue.ToString()) != Guid.Empty)
            {
                customerServiceID = new Guid(this.cmbCustomerService.EditValue.ToString());
            }

            if (this.cmbFiler.EditValue != null && new Guid(this.cmbFiler.EditValue.ToString()) != Guid.Empty)
            {
                filerID = new Guid(this.cmbFiler.EditValue.ToString());
            }

            if (bgDate.Expanded && cmbDate.EditValue != null && cmbDate.EditValue.ToString() != OIBusinessDateSearchType.All.ToString())
            {
                dateSearchType = (OIBusinessDateSearchType)cmbDate.EditValue;
                dtFrom = fromToDateMonthControl1.From;
                dtTo = fromToDateMonthControl1.To;
            }
            if (cmbState.EditValue != null && !string.IsNullOrEmpty(cmbState.EditValue.ToString()))
            {
                orderState = (OIOrderState)cmbState.EditValue;
            }

            try
            {
                List<OceanBusinessList> list = OceanImportService.GetBusinessList(
                                        companyIDs,
                                        this.txtNo.Text.Trim(),
                                        this.txtBLNO.Text.Trim(),
                                        this.txtCtnNO.Text.Trim(),
                                        this.txtTelexNo.Text.Trim(),
                                        this.txtClearanceNo.Text.Trim(),
                                        this.txtCustomer.Text.Trim(),
                                        this.txtAgent.Text.Trim(),
                                        this.txtConsignee.Text.Trim(),
                                        this.txtShipper.Text.Trim(),
                                        this.txtNotifier.Text.Trim(),
                                        this.txtLoadingPort.Text.Trim(),
                                        this.txtDischargePort.Text.Trim(),
                                        this.txtPlaceOfDelivery.Text.Trim(),
                                        this.txtVesselName.Text.Trim(),
                                        this.txtVoyageNo.Text.Trim(),
                                        customerServiceID,
                                        filerID,
                                        salesID,
                                        orderState,
                                        this.lwchkIsValid.Checked,
                                        int.Parse(this.numMax.Value.ToString()),
                                        dateSearchType,
                                        dtFrom,
                                        dtTo,
                                        (ReleaseBLSearchStatue)rdoRelease.SelectedIndex,
                                        (ReceiveRCSearchStatue)rdoReceive.SelectedIndex,
                                        (ApplyRCSearchStatue)rdoApply.SelectedIndex,
                                        (ReleaseRCSearchStatue)rdoReleaseRC.SelectedIndex,
                                        (URBSearchStatue)rdoIsURB.SelectedIndex,
                                        (UDNSearchStatue)rdoIsUDN.SelectedIndex,
                                        (ARCSearchStatue)rdoARC.SelectedIndex);

                return list;

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                return null;
            }


        }
        #endregion

        private void pnlMain_SizeChanged(object sender, EventArgs e)
        {
            this.bcMain.Width = this.pnlMain.Width - 17;
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindFiler();
            this.BindCustomerService();
        }

        #region 方案发生改变时
        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReleaseCargoProgram program = (ReleaseCargoProgram)cmbprogram.SelectedIndex;
            switch (program)
            {
                case ReleaseCargoProgram.Custom:
                    SetCustomer();
                    break;
                case ReleaseCargoProgram.NotApliedReleaseCargo:
                    SetNotApliedReleaseCargo();
                    break;
                case ReleaseCargoProgram.AppliedReleaseCargo:
                    SetAppliedReleaseCargo();
                    break;
                case ReleaseCargoProgram.NotReleasedBL:
                    SetNotReleasedBL();
                    break;
                case ReleaseCargoProgram.ReleasedBL:
                    SetReleasedBL();
                    break;
                case ReleaseCargoProgram.AcceptedReleaseBL:
                    SetAcceptedReleaseBL();
                    break;
                case ReleaseCargoProgram.ReleasedCargo:
                    SetReleasedCargo();
                    break;
                default:
                    break;
            }

            if (ProgramSelectedChanged != null)
            {
                this.ProgramSelectedChanged(sender, program);
            }
        }

        /// <summary>
        /// 设置自定义方案的样式
        /// </summary>
        private void SetCustomer()
        {
            this.rdoApply.Enabled = true;
            this.rdoApply.SelectedIndex = 0;

            this.rdoReceive.Enabled = true;
            this.rdoReceive.SelectedIndex = 0;

            this.rdoReleaseRC.Enabled = true;
            this.rdoReleaseRC.SelectedIndex = 0;

            this.rdoRelease.Enabled = true;
            this.rdoRelease.SelectedIndex = 0;

            this.rdoIsUDN.Enabled = true;
            this.rdoIsUDN.SelectedIndex = 0;

            this.rdoIsURB.Enabled = true;
            this.rdoIsURB.SelectedIndex = 0;

            this.rdoARC.Enabled = true;
            this.rdoARC.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置未申请放货方案的样式
        /// </summary>
        private void SetNotApliedReleaseCargo()
        {
            this.rdoApply.Enabled = false;
            this.rdoApply.SelectedIndex = 2;

            this.rdoReceive.Enabled = true;
            this.rdoReceive.SelectedIndex = 0;

            this.rdoReleaseRC.Enabled = true;
            this.rdoReleaseRC.SelectedIndex = 0;

            this.rdoRelease.Enabled = true;
            this.rdoRelease.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置已申请放货方案的样式
        /// </summary>
        private void SetAppliedReleaseCargo()
        {
            this.rdoApply.Enabled = false;
            this.rdoApply.SelectedIndex = 1;

            this.rdoReceive.Enabled = true;
            this.rdoReceive.SelectedIndex = 0;

            this.rdoReleaseRC.Enabled = true;
            this.rdoReleaseRC.SelectedIndex = 0;

            this.rdoRelease.Enabled = false;
            this.rdoRelease.SelectedIndex = 1;
        }
        /// <summary>
        /// 设置未下达放单指令方案的样式
        /// </summary>
        private void SetNotReleasedBL()
        {
            this.rdoApply.Enabled = false;
            this.rdoApply.SelectedIndex = 0;

            this.rdoReceive.Enabled = false;
            this.rdoReceive.SelectedIndex = 0;

            this.rdoReleaseRC.Enabled = false;
            this.rdoReleaseRC.SelectedIndex = 0;

            this.rdoRelease.Enabled = false;
            this.rdoRelease.SelectedIndex = 2;
        }
        /// <summary>
        /// 设置已下达放单指令方案的样式
        /// </summary>
        private void SetReleasedBL()
        {
            this.rdoApply.Enabled = true;
            this.rdoApply.SelectedIndex = 0;

            this.rdoReceive.Enabled = true;
            this.rdoReceive.SelectedIndex = 2;

            this.rdoReleaseRC.Enabled = true;
            this.rdoReleaseRC.SelectedIndex = 0;

            this.rdoRelease.Enabled = false;
            this.rdoRelease.SelectedIndex = 1;
        }
        /// <summary>
        /// 设置已签收放单指令方案的样式
        /// </summary>
        private void SetAcceptedReleaseBL()
        {
            this.rdoApply.Enabled = true;
            this.rdoApply.SelectedIndex = 0;

            this.rdoReceive.Enabled = false;
            this.rdoReceive.SelectedIndex = 1;

            this.rdoReleaseRC.Enabled = true;
            this.rdoReleaseRC.SelectedIndex = 0;

            this.rdoRelease.Enabled = false;
            this.rdoRelease.SelectedIndex = 1;
        }
        /// <summary>
        /// 设置已放货样式
        /// </summary>
        private void SetReleasedCargo()
        {
            this.rdoApply.Enabled = true;
            this.rdoApply.SelectedIndex = 0;

            this.rdoReceive.Enabled = true;
            this.rdoReceive.SelectedIndex = 0;

            this.rdoReleaseRC.Enabled = false;
            this.rdoReleaseRC.SelectedIndex = 1;

            this.rdoRelease.Enabled = false;
            this.rdoRelease.SelectedIndex = 1;
        }

        #endregion

    }

}