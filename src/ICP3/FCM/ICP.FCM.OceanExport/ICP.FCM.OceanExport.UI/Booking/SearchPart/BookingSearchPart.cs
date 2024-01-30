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
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;

namespace ICP.FCM.OceanExport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class BookingSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

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
        /// 如果是为搜索器准备的界面，订舱不要默认设置成自己
        /// 添加时间：2011-07-20 10:10
        /// </summary>
        public bool IsLoadFromFinder { get; set; }

        #region init
        /// <summary>
        /// 构造函数
        /// </summary>
        public BookingSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsDesignMode) return;
            if (LocalData.IsEnglish == false) SetCnText();

            this.Disposed += delegate {
                this.userCompanyList = null;
                this.OnSearched = null;
                this.RemoveKeyDownHandle();
                this.RemoveControlEnterHandle();
                this.mcmbOverseasFiler.OnFirstEnter -= this.OnmcmbOverseasFilerEnter;
                this.mcmbFiler.SelectedRow -= this.mcmbFiler_SelectedRow;
                this.mcmbFiler.OnFirstEnter -= this.OnmcmbFilerEnter;
                this.mcmbSales.OnFirstEnter -= this.OnmcmbSalesEnter;
                this.cmbDateSearchType.OnFirstEnter -= this.OnCmbDateSearchTypeEnter;
                this.mcmbCarrier.OnFirstEnter -= this.OnMcmbCarrierEnter;
                this.btnClear.Click -= this.btnClear_Click;
                this.btnSearch.Click -= this.btnSearch_Click;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;

                }
            };

            this.Load += new EventHandler(BookingSearchPart_Load);
        }

        void BookingSearchPart_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                SetKeyDownToSearch();
            }
        }
        private void SetKeyDownToSearch()
        {
            foreach (Control item in panel4.Controls)
            {
                item.KeyDown += item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in panel4.Controls)
            {
                item.KeyDown -= item_KeyDown;
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) this.btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) this.btnClear.PerformClick();
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
            labOverseasFiler.Text = "海外部客服";
            labScno.Text = "合约号";
            labShippingOrderNo.Text = "订舱号";
            labAgentOfCarrier.Text = "承运人";
            labState.Text = "状态";
            labVessel.Text = "船名航次";
            labUser.Text = "订舱";
            labFrom.Text = "从";
            labTo.Text = "到";
            fromToDateMonthControl1.IsEngish = false;
            labIsValid.Text = "有效性";
            lwchkIsValid.CheckedText = "有效的";
            lwchkIsValid.UnCheckedText = "无效的";
            lwchkIsValid.NULLText = "全部";
            
            labVessel.Text = "船名";
            labVoayge.Text = "航次";

            nbarBase.Caption = "基本信息";
            nbarDate.Caption = "日期";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!LocalData.IsDesignMode)
            {
                InitControls();
            }
        }
        private void OnCmbDateSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            dateSearchTypes.RemoveAll(item => item.Value == DateSearchType.All);
            cmbDateSearchType.Properties.BeginUpdate();
            this.cmbDateSearchType.Properties.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.Properties.EndUpdate();
        }
        private void OnMcmbCarrierEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCustomerList(this.mcmbCarrier, CustomerType.Carrier);
        }
        private void OnmcmbFilerEnter(object sender, EventArgs e)
        {
            List<Guid> ids = new List<Guid>();
            foreach (var item in userCompanyList)
            {
                ids.Add(item.ID);
            }
            List<UserList> users = UserService.GetUnderlingUserList(ids.ToArray(), null, null, true);
            UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
            if (currentUser != null)
            {
                users.Remove(currentUser);
                users.Insert(0, currentUser);
            }
            else
            {
                currentUser = new UserList();
                currentUser = UserService.GetUserInfo(LocalData.UserInfo.LoginID);
                users.Insert(0, currentUser);
            }

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            mcmbFiler.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        private void OnmcmbOverseasFilerEnter(object sender, EventArgs e)
        {
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
        private void OnmcmbSalesEnter(object sender,EventArgs e)
        {
            //List<Guid> ids = new List<Guid>(); 
            //foreach (var item in userCompanyList)
            //{
            //    ids.Add(item.ID);
            //}
            //List<UserList> users = oeService.GetOrderSalesByCompanyIDs(ids.ToArray(), LocalData.IsEnglish);

            //Dictionary<string, string> col = new Dictionary<string, string>();
            //col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ?"Name":"名称");
            //col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
            //mcmbSales.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");

            List<UserList> userList = UserService.GetUnderlingUserList(null, new string[] { "海外拓展", "销售代表", "电商顾问", "拓展员", "总裁", "副总裁", "总经理助理", "分公司总经理" }, null, true);

            UserList insertItem = new UserList();
            insertItem.ID = Guid.Empty;
            insertItem.EName = insertItem.CName = string.Empty;
            userList.Insert(0, insertItem);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

            mcmbSales.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> userCompanyList;
        private void InitControls()
        {
            #region enum

            SetState();

            this.cmbDateSearchType.ShowSelectedValue(DateSearchType.CreateDate, LocalData.IsEnglish ? "Create Date" : "创建日");
            this.cmbDateSearchType.OnFirstEnter += this.OnCmbDateSearchTypeEnter;
            this.mcmbCarrier.OnFirstEnter += this.OnMcmbCarrierEnter;

            #endregion


            #region 用户和部门

            userCompanyList = SetCompany();

            //	当前用户所在的部门的所有用户	包含下属部门的所有用户
            this.mcmbFiler.OnFirstEnter += this.OnmcmbFilerEnter;

            this.mcmbFiler.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);

            //海外部客服
            this.mcmbOverseasFiler.OnFirstEnter += this.OnmcmbOverseasFilerEnter;

            //	当前用户所在的操作口岸的海运订舱单中所有揽货人	数据从用户表[sm..Users]检索
            this.mcmbSales.OnFirstEnter += this.OnmcmbSalesEnter;

            this.mcmbFiler.SelectedRow += new EventHandler(mcmbFiler_SelectedRow);

            #endregion

            SetControlsEnterToSearch();
        }
        private void RemoveControlEnterHandle()
        {
            foreach (Control item in panel4.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown -= OnControlKeyDown;

                }
            }
        }
        void mcmbFiler_SelectedRow(object sender, EventArgs e)
        {
            this.Workitem.State["BookingerId"] = this.OwnerID;
        }

        /// <summary>
        /// 设置状态查询栏位的可查状态值
        /// </summary>
        protected virtual void SetState()
        {
            cmbState.Properties.BeginUpdate();
            if (SearchOrderStates != null)
            {
                foreach (var item in SearchOrderStates)
                {
                    if (item == null)
                    {
                        cmbState.Properties.Items.Insert(0,new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", null));
                    }
                    else
                    {
                        string description = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<OEOrderState>(item.Value, LocalData.IsEnglish);
                        cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(description, item.Value));
                    }
                }
            }
            else
            {
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", null));
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OEOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OEOrderState>(LocalData.IsEnglish);

                foreach (var item in orderStates)
                {
                    if (item.Value == OEOrderState.Unknown || item.Value == OEOrderState.Rejected) continue;
                    cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
            }

            cmbState.Properties.EndUpdate();
            cmbState.SelectedIndex = 0;
        }

        /// <summary>
        /// 设置一些TextBox输入Enter键时,触发查询事件
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in panel4.Controls)
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
            if (e.KeyCode == Keys.Enter) this.btnSearch.PerformClick();
        }

        private List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> SetCompany()
        {
            List<ICP.Framework.CommonLibrary.Server.LocalOrganizationInfo> userCompanyList = LocalData.UserInfo.UserOrganizationList.FindAll(item => item.Type == ICP.Framework.CommonLibrary.Server.LocalOrganizationType.Company);
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

        #region ISearchPart 成员

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }

        string GetFirst(string value)
        {
            string[] nos = value.Split(',');
            if (nos.Length > 0)
            {
                return nos[0];
            }
            else
            {
                return value;
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            this.txtOperationNo.Text = string.Empty;
            this.txtBLNO.Text = string.Empty;
            this.txtCtnNO.Text = string.Empty;
            this.txtOrderNO.Text = string.Empty;
            this.stxtCustomer.Text = string.Empty;
            this.stxtAgentOfCarrier.Text = string.Empty;
            this.stxtVessel.Text = string.Empty;
            this.txtVoayge.Text = string.Empty;
            this.stxtPOL.Text = string.Empty;
            this.stxtPOD.Text = string.Empty;
            this.stxtPlaceOfDelivery.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "No":
                        txtOperationNo.Text = value;
                        break;
                    case "MBLNo":
                        this.txtBLNO.Text = GetFirst(value);
                        break;
                    case "HBLNo":
                        this.txtBLNO.Text = GetFirst(value);
                        break;
                    case "ContainerNo":
                        this.txtCtnNO.Text = GetFirst(value);
                        break;
                    case "OceanShippingOrderNo":
                        this.txtOrderNO.Text = value;
                        break;
                    case"CustomerName":
                        this.stxtCustomer.Text = value;
                        break;
                    case "AgentOfCarrierName":
                        this.stxtAgentOfCarrier.Text = value;
                        break;
                    case "VesselVoyage":
                        string[] vvs = value.Split('/');
                        if (vvs.Length > 0)
                        {
                            this.stxtVessel.Text = vvs[0];

                            if (vvs.Length > 1)
                            {
                                string[] left = vvs[1].Split(';');
                                if (left.Length > 0)
                                {
                                    this.txtVoayge.Text = left[0];
                                }
                            }
                        }
                        break;
                    case "POLName":
                        this.stxtPOL.Text = value;
                        break;
                    case "PODName":
                        this.stxtPOD.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        this.stxtPlaceOfDelivery.Text = value;
                        break;
                }
            }
        }

        public override object GetData()
        {
            if (CompanyIDs.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "At least one company must be selected." : "必须至少选择一个操作口岸.");

                return null;
            }
            try
            {
                List<OceanBookingList> list = OceanExportService.GetOceanBookingList(CompanyIDs.ToArray(),
                                                                            txtOperationNo.Text.Trim(),
                                                                            this.txtBLNO.Text.Trim(),
                                                                            this.txtCtnNO.Text.Trim(),
                                                                            txtOrderNO.Text.Trim(),
                                                                            txtSCNO.Text.Trim(),
                                                                            txtCusClearanceNo.Text.Trim(),
                                                                            stxtCustomer.Text.Trim(),
                                                                            this.mcmbCarrier.EditText,
                                                                            stxtAgentOfCarrier.Text.Trim(),
                                                                            stxtVessel.Text.Trim(),
                                                                            this.txtVoayge.Text.Trim(),
                                                                            stxtPOL.Text.Trim(),
                                                                            stxtPOD.Text.Trim(),
                                                                            stxtPlaceOfDelivery.Text.Trim(),
                                                                            txtRefNo.Text.Trim(),
                                                                            SalesID,                                                                    
                                                                            OwnerID,
                                                                            OverseasFilerID,
                                                                            lwchkIsValid.Checked,
                                                                            OrderStateValue,
                                                                            DateSearchTypeValue,
                                                                            From,
                                                                            To,
                                                                            int.Parse(numMax.Value.ToString()));

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
                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel4.Controls)
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
                {
                    item.Text = string.Empty;
                }

                cmbDateSearchType.SelectedIndex = 0;
            }
        }

        #endregion

        #region 公共成员

        protected virtual List<OEOrderState?> SearchOrderStates { get { return null; } }

        public OEOrderState? OrderStateValue
        {
            get
            {
                if (cmbState.EditValue != null) return (OEOrderState)cmbState.EditValue;
                else return null;
            }
        }

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
                if (mcmbSales.EditValue == null) return null;
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

        protected bool IsFinder = false;


        #endregion
    }

    public class BookingFinderSearchPart : BookingSearchPart
    {
        protected override List<OEOrderState?> SearchOrderStates
        {
            get
            {
                List<OEOrderState?> states = new List<OEOrderState?>();
                states.Add(OEOrderState.BookingConfirmed);
                                         
                return states; 
            }
        }
      
        public override void Init(IDictionary<string, object> values)
        {
            this.mcmbFiler.ShowSelectedValue(null,null);
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == SearchFieldConstants.BookingNO)
                {
                    txtOperationNo.Text = item.Value == null ? string.Empty : item.Value.ToString();
                }
            }
        }
    }
}
