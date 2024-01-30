using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.UI.BL;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.DataCache.ServiceInterface;
using EnumDocumentState = ICP.FileSystem.ServiceInterface.DocumentState;
using System.Diagnostics;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.FCM.OceanExport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class BLSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        public ICP.Sys.ServiceInterface.IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IUserService>();
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

        /// <summary>
        /// 选择的方案发生改变时
        /// </summary>
        public event SelectedHandler ProgramSelectedChanged;

        #region init

        public BLSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsDesignMode) return;

            this.Disposed += delegate {
                this.OnSearched = null;
                this.ProgramSelectedChanged = null;
                this.RemoveKeyDownHandle();
                this.mcmbOverseasFiler.OnFirstEnter -= this.OnmcmbOverseasFilerEnter;
                this.mcmbSales.Enter -= this.OnmcmbSalesEnter;
                this.mcmbCarrier.OnFirstEnter -= this.OnmcmbCarrierEnter;
                this.mcmbFiler.OnFirstEnter -= this.OnmcmbFilerEnter;
                this.cmbprogram.SelectedIndexChanged -= this.cmbProgram_SelectedIndexChanged;
                this.cmbDateSearchType.OnFirstEnter -= this.OncmbDateSearchTypeEnter;
                this.cmbprogram.OnFirstEnter -= this.OncmbprogramEnter;
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        void SetCnText()
        {
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&S)";

            labBLNO.Text = "提单号";
            labCarrier.Text = "船公司";
            labCompany.Text = "操作口岸";
            labCtnNo.Text = "箱号";
            labCustomer.Text = "客户";
            labPlaceOfDelivery.Text = "交货地";
            labMax.Text = "最大行数";
            labOperationNo.Text = "业务号";
            labPOD.Text = "卸货港";
            labPOL.Text = "装货港";
            labSales.Text = "揽货人";
            labScno.Text = "合约号";
            labShippingOrderNo.Text = "订舱号";
            labAgentOfCarrier.Text = "承运人";
            labState.Text = "状态";
            labVessel.Text = "船名";
            labVoayge.Text = "航次";

            labUser.Text = "文件";
            labFileState.Text = "文件状态";
            labOverseasFiler.Text = "海外部客服";
            labFrom.Text = "从";
            labTo.Text = "到";
            fromToDateMonthControl1.IsEngish = false;

            nbarBase.Caption = "基本信息";
            nbarDate.Caption = "日期";

            labChecking.Text = "对单中";
            labFinished.Text = "完成";
            labRelease.Text = "放单";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                SearchRegister();
                SetKeyDownToSearch();
                if (!LocalData.IsEnglish)
                {
                    SetCnText(); 
                }
            }
        }

        private void SetKeyDownToSearch()
        {
            //navBarGroupBase.KeyDown += new KeyEventHandler(item_KeyDown);
            //navBarControl1.KeyDown += new KeyEventHandler(item_KeyDown);

            foreach (Control item in panel2.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown += new KeyEventHandler(item_KeyDown);
                }
            }
        }
        private void RemoveKeyDownHandle()
        {
            navBarGroupBase.KeyDown -= item_KeyDown;
            navBarControl1.KeyDown -= item_KeyDown;

            foreach (Control item in panel2.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown -= item_KeyDown;
                }
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F5) this.btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) this.btnClear.PerformClick();
        }
        private void OncmbprogramEnter(object sender, EventArgs e)
        {
            this.cmbprogram.Properties.BeginUpdate();
            this.cmbprogram.Properties.Items.Clear();
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<ReleaseProgram>> releasePrograms = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<ReleaseProgram>(LocalData.IsEnglish);
            foreach (var item in releasePrograms)
            {
                this.cmbprogram.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbprogram.Properties.EndUpdate();
        }
        private void OncbmFileStateEnter(object sender, EventArgs e)
        {
            #region 文件状态下拉框复制
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<EnumDocumentState>> docState = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<EnumDocumentState>(LocalData.IsEnglish);
            cbmFileState.Properties.BeginUpdate();
            this.cbmFileState.Properties.Items.Clear();
            foreach (var item in docState)
            {
                cbmFileState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cbmFileState.Properties.EndUpdate();
            #endregion
        }
        private void OncmbDateSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchDispatchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchDispatchType>(LocalData.IsEnglish);
            dateSearchTypes.RemoveAll(item => item.Value == DateSearchDispatchType.All);
            cmbDateSearchType.Properties.BeginUpdate();
            cmbDateSearchType.Properties.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.Properties.EndUpdate();
        }
        private void OnmcmbCarrierEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
        }
        bool isCompanyChanged = true;
        private void OnchkcmbCompanyEditValueChanged(object sender, EventArgs e)
        {
            isCompanyChanged = true;
        }
        private void OnmcmbSalesEnter(object sender, EventArgs e)
        {
            if (isCompanyChanged)
            {
                if (CompanyIDs.Count == 0)
                {
                    mcmbSales.ClearItems();
                }
                else
                {
                    List<UserList> saless = UserService.GetUnderlingUserList(CompanyIDs.ToArray(), null, null, true);
                    Dictionary<string, string> col = new Dictionary<string, string>();
                    col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
                    col.Add("Code", "代码");
                    mcmbSales.InitSource<UserList>(saless, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
                }
                isCompanyChanged = false;
            }
        }
        private void OnmcmbFilerEnter(object sender, EventArgs e)
        {
            List<OrganizationList> deps = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, null);
            List<Guid> depIds = new List<Guid>();
            foreach (var item in deps) { depIds.Add(item.ID); }

            List<UserList> filers = UserService.GetUnderlingUserList(depIds.ToArray(), null, null, true);
            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
            col.Add("Code", "代码");
            mcmbFiler.InitSource<UserList>(filers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
       
        private void OnmcmbOverseasFilerEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
            List<Guid> ids = new List<Guid>();
            foreach (var item in userCompanyList)
            {
                ids.Add(item.ID);
            }

            List<UserList> userList = UserService.GetUnderlingUserList(ids.ToArray(), new string[] { "海外拓展", "客服" }, null, true);

            UserList insertItem = new UserList();
            insertItem.ID = Guid.Empty;
            insertItem.EName = insertItem.CName = string.Empty;
            userList.Insert(0, insertItem);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

            mcmbOverseasFiler.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        private void InitControls()
        {

            this.fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;

            panelChecking.BackColor = OEBLColorConstant.CheckingColor;
            panelFinished.BackColor = OEBLColorConstant.CheckedColor;
            panelRelease.BackColor = OEBLColorConstant.ReleaseColor;

            #region emun
            //方案
           
            this.cmbprogram.ShowSelectedValue(ReleaseProgram.Custom, LocalData.IsEnglish ? "Custom" : "自定义");
            this.cmbprogram.OnFirstEnter += this.OncmbprogramEnter;
            
            
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OEBLState>> blStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OEBLState>(LocalData.IsEnglish);

            cmbState.Properties.BeginUpdate();

            if (SearchBLState == null)
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", DBNull.Value));

            SetCmbStates(blStates);

            cmbState.Properties.EndUpdate();

            this.cbmFileState.ShowSelectedValue(EnumDocumentState.Pending, LocalData.IsEnglish ? "Pending" : "Pending");
            this.cbmFileState.OnFirstEnter += this.OncbmFileStateEnter;
 
            
            this.cmbDateSearchType.ShowSelectedValue(DateSearchDispatchType.CreateDate, LocalData.IsEnglish ? "Create Date" : "创建日");
            this.cmbDateSearchType.OnFirstEnter += this.OncmbDateSearchTypeEnter;


            #endregion

            //船东
            this.mcmbCarrier.OnFirstEnter += this.OnmcmbCarrierEnter;
 

            #region Company and Sales

           
            SetCompany();
           
            chkcmbCompany.EditValueChanged += this.OnchkcmbCompanyEditValueChanged;
          

            //	当前用户所在的操作口岸的海运订舱单中所有揽货人
            mcmbSales.Enter += this.OnmcmbSalesEnter;
       
            #endregion

            #region Filer 	当前用户所在的部门的所有用户
            this.mcmbFiler.OnFirstEnter += this.OnmcmbFilerEnter;
       

            mcmbFiler.ShowSelectedValue(Guid.Empty, "");

            #endregion

            //海外部客服
            this.mcmbOverseasFiler.OnFirstEnter += this.OnmcmbOverseasFilerEnter;
        
        }

        private void SetCompany()
        {
            chkcmbCompany.Properties.BeginUpdate();
            chkcmbCompany.Properties.Items.Add(LocalData.UserInfo.DefaultCompanyID,LocalData.UserInfo.DefaultCompanyName, CheckState.Checked, true);
            chkcmbCompany.Properties.EndUpdate();
            OEUtility.SetEnterToExecuteOnec(this.chkcmbCompany, delegate
            {
                List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
                ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo defaultCompany = userCompanyList.Find(item => item.ID == LocalData.UserInfo.DefaultCompanyID);
                userCompanyList.Remove(defaultCompany);
                this.chkcmbCompany.Properties.Items.Clear();
                chkcmbCompany.Properties.BeginUpdate();
                chkcmbCompany.Properties.Items.Add(defaultCompany.ID, LocalData.IsEnglish ? defaultCompany.EShortName : defaultCompany.CShortName, CheckState.Checked, true);
                foreach (var item in userCompanyList)
                {
                    chkcmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                       CheckState.Unchecked, true);
                }

                chkcmbCompany.Properties.EndUpdate();

                
            });
            
        }

        private void SetCmbStates(List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OEBLState>> blState)
        {
            cmbState.Properties.BeginUpdate();

            foreach (var item in blState)
            {
                if (item.Value == OEBLState.Unknown) continue;

                if (SearchBLState != null)
                {
                    if (item.Value != SearchBLState.Value) continue;
                }

                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();

            cmbState.SelectedIndex = 0;
        }

        private void SearchRegister()
        {
            #region User



            #endregion
        }

        #endregion

        #region ISearchPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;

            this.btnClear_Click(null, null);
            foreach (var item in values)
            {
                switch (item.Key)
                {
                    case "RefNo":
                        txtOperationNo.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "No":
                        txtBLNO.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "SONO":
                        txtOrderNO.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "ContainerNos":
                        txtCtnNO.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "VesselVoyage":
                        txtVoayeNo.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "AgentOfCarrierName":
                        stxtAgentOfCarrier.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "CustomerName":
                        stxtCustomer.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "PlaceOfDeliveryName":
                        stxtPlaceOfDelivery.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "PODName":
                        stxtPOD.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "POLName":
                        stxtPOL.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "CarrierName":
                        mcmbCarrier.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "FilerName":
                        mcmbFiler.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                    case "SalesName":
                        mcmbSales.Text = item.Value == null ? string.Empty : item.Value.ToString();
                        break;
                }
            }
        }

        public override object GetData()
        {
            try
            {
              string result  = OceanExportService.GetOceanBLList(CompanyIDs.ToArray(),
                                                                    txtOperationNo.Text.Trim(),
                                                                    txtBLNO.Text.Trim(),
                                                                    txtCtnNO.Text.Trim(),
                                                                    txtOrderNO.Text.Trim(),
                                                                    txtSCNO.Text.Trim(),
                                                                    txtTelexNo.Text.Trim(),
                                                                    stxtCustomer.Text.Trim(),
                                                                    mcmbCarrier.EditText,
                                                                    stxtAgentOfCarrier.Text.Trim(),
                                                                    stxtVessel.Text.Trim(),
                                                                    txtVoayeNo.Text.Trim(),
                                                                    stxtPOL.Text.Trim(),
                                                                    stxtPOD.Text.Trim(),
                                                                    stxtPlaceOfDelivery.Text.Trim(),
                                                                    txtConsigneeName.Text.Trim(),
                                                                    SalesID,
                                                                    OwnerID,
                                                                    OverseasFilerID,
                                                                    BLStateValue,
                                                                   DateSearchDispatchTypeValue,
                                                                    From,
                                                                    To,
                                                                    int.Parse(numMax.Value.ToString()),
                                                                    (ReleaseBLSearchStatue)rdoRelease.SelectedIndex,
                                                                    (ReleaseRCSearchStatue)rdoReleaseRC.SelectedIndex,
                                                                    (ApplyReleaseSearchStatue)rdoApply.SelectedIndex,
                                                                    (ReceiveRCSearchStatue)rdoReceive.SelectedIndex,
                                                                    DocumentState
                                                                    );
                List<OceanBLList> list = JSONSerializerHelper.DeserializeFromJson<List<OceanBLList>>(result);

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

               // LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Searching..." : "查找数据中...");

                if (OnSearched != null)
                {
                    Stopwatch stopwatch = Stopwatch.StartNew();
                    OnSearched(this, GetData());
                    System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
                    StopwatchHelper.EndStopwatch(stopwatch, DateTime.Now, method.DeclaringType.FullName, "SEARCH","海出HBL查找");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.panel2.Controls)
            {
                if (item is ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit)
                {
                    (item as ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is DevExpress.XtraEditors.TextEdit
                         && (item is DevExpress.XtraEditors.SpinEdit) == false
                         && item.Enabled == true
                         && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
                else if (item is ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox) (item as ICP.Framework.ClientComponents.Controls.MultiSearchCommonBox).EditText = string.Empty;

                cmbDateSearchType.SelectedIndex = 0;
            }
        }

        #endregion

        #region 公共成员


        protected virtual OEBLState? SearchBLState { get { return null; } }

        public OEBLState? BLStateValue
        {
            get
            {
                if (SearchBLState != null) return SearchBLState;

                if (cmbState.EditValue != null && cmbState.EditValue != DBNull.Value) return (OEBLState)cmbState.EditValue;
                else return null;
            }
        }

        public EnumDocumentState? DocumentState 
        {
            get 
            {
                if (cbmFileState.EditValue != null && cbmFileState.EditValue != DBNull.Value) return (EnumDocumentState)cbmFileState.EditValue;
                else return null;         
            }
        
        }

        //protected virtual FCMReleaseState? SearchReleaseState { get { return null; } }

        //public FCMReleaseState? ReleaseStateValue
        //{
        //    get
        //    {
        //        if (SearchReleaseState != null) return SearchReleaseState;

        //        if (cmbReleaseState.EditValue != null && cmbReleaseState.EditValue != System.DBNull.Value) return (FCMReleaseState)cmbReleaseState.EditValue;
        //        else return null;
        //    }
        //}

        public DateSearchType DateSearchTypeValue
        {
            get
            {
                if (nbarDate.Expanded && cmbDateSearchType.EditValue.ToString() != DateSearchType.All.ToString())
                {
                    return (DateSearchType)cmbDateSearchType.EditValue;
                }
                else
                    return DateSearchType.All;
            }
        }

        public DateSearchDispatchType DateSearchDispatchTypeValue
        {
            get
            {
                if (nbarDate.Expanded && cmbDateSearchType.EditValue.ToString() != DateSearchDispatchType.All.ToString())
                {
                    return (DateSearchDispatchType)cmbDateSearchType.EditValue;
                }
                else
                    return DateSearchDispatchType.All;
            }
        }

        public DateTime? From
        {
            get
            {
                if (DateSearchTypeValue == DateSearchType.All) return null;
                else return fromToDateMonthControl1.From;
            }
        }

        public DateTime? To
        {
            get
            {
                if (DateSearchTypeValue == DateSearchType.All) return null;
                else return fromToDateMonthControl1.To;
            }
        }

        public List<Guid> CompanyIDs
        {
            get
            {
                List<Guid> companyIDs = new List<Guid>();
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chkcmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                        companyIDs.Add(new Guid(item.Value.ToString()));
                }
                return companyIDs;
            }
        }

        public Guid? SalesID
        {
            get
            {
                if (this.mcmbSales.EditValue == null) return null;
                return new Guid(mcmbSales.EditValue.ToString());
            }
        }

        public Guid? OverseasFilerID
        {
            get
            {
                if (mcmbOverseasFiler.EditValue == null) return null;
                else if ((new Guid(mcmbOverseasFiler.EditValue.ToString())) == Guid.Empty)
                {
                    return null;
                }

                return new Guid(mcmbOverseasFiler.EditValue.ToString());
            }
        }

        public Guid? OwnerID
        {
            get
            {
                if (mcmbFiler.EditValue == null) return null;
                return new Guid(mcmbFiler.EditValue.ToString());
            }
        }

        #endregion
        #region 方案发生改变时
        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

            ReleaseProgram program = (ReleaseProgram)cmbprogram.SelectedIndex;

            switch (program)
            {
                case ReleaseProgram.Custom:
                    SetCustomer();
                    break;
                case ReleaseProgram.NotApliedReleaseBL:
                    SetNotApliedReleaseBL();
                    break;
                case ReleaseProgram.AppliedReleaseBL:
                    SetAppliedReleaseBL();
                    break;
                case ReleaseProgram.NotReleasedBL:
                    SetNotReleasedBL();
                    break;
                case ReleaseProgram.ReleasedBL:
                    SetReleasedBL();
                    break;
                case ReleaseProgram.AcceptedReleaseBL:
                    SetAcceptedReleaseBL();
                    break;
                case ReleaseProgram.ReleasedCargo:
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

        }
        /// <summary>
        /// 设置未申请放单方案的样式
        /// </summary>
        private void SetNotApliedReleaseBL()
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
        /// 设置已申请放单方案的样式
        /// </summary>
        private void SetAppliedReleaseBL()
        {
            this.rdoApply.Enabled = false;
            this.rdoApply.SelectedIndex = 1;

            this.rdoReceive.Enabled = true;
            this.rdoReceive.SelectedIndex = 0;

            this.rdoReleaseRC.Enabled = true;
            this.rdoReleaseRC.SelectedIndex = 0;

            this.rdoRelease.Enabled = true;
            this.rdoRelease.SelectedIndex = 0;
        }
        /// <summary>
        /// 设置未下达放单指令方案的样式
        /// </summary>
        private void SetNotReleasedBL()
        {
            this.rdoApply.Enabled = true;
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
            this.rdoReceive.SelectedIndex = 0;

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

    public class MBLSearchPart : BLSearchPart
    {
        public override object GetData()
        {
            try
            {
                List<OceanBLList> list = OceanExportService.GetOceanMBLList(base.CompanyIDs.ToArray(),
                                                                    txtOperationNo.Text.Trim(),
                                                                    txtBLNO.Text.Trim(),
                                                                    txtOrderNO.Text.Trim(),
                                                                    txtSCNO.Text.Trim(),
                                                                    stxtCustomer.Text.Trim(),
                                                                    cmbDateSearchType.Text.Trim(),
                                                                    stxtAgentOfCarrier.Text.Trim(),
                                                                    stxtVessel.Text.Trim(),
                                                                    string.Empty,
                                                                    stxtPOL.Text.Trim(),
                                                                    stxtPOD.Text.Trim(),
                                                                    stxtPlaceOfDelivery.Text.Trim(),
                                                                    base.SalesID,
                                                                    base.BLStateValue,
                                                                    base.DateSearchTypeValue,
                                                                    base.From,
                                                                    base.To,
                                                                    OwnerID,
                                                                    int.Parse(numMax.Value.ToString()));

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return null; }

        }
    }
}
