using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using Microsoft.Practices.CompositeUI;

using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;

namespace ICP.FCM.AirExport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class BookingSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IAirExportService oeService { get; set; }


        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        #endregion

        /// <summary>
        /// 如果是为搜索器准备的界面，订舱不要默认设置成自己
        /// 添加时间：2011-07-20 10:10
        /// </summary>
        public bool IsLoadFromFinder { get; set; }

        #region init

        public BookingSearchPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            if (LocalData.IsEnglish == false) SetCnText();

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };

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
            foreach (Control item in navBarGroupBase.Controls)
            {
                item.KeyDown += new KeyEventHandler(item_KeyDown);
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
            labAir.Text = "航空公司";
            labCompany.Text = "操作口岸";
            labFlightNo.Text = "航班号";
            labCustomer.Text = "客户";
            labPlaceOfDelivery.Text = "交货地";
            labMax.Text = "最大行数";
            labOperationNo.Text = "业务号";
            labDetination.Text = "目的港";
            labDeparture.Text = "起运港";
            labSales.Text = "揽货人";
            labScno.Text = "合约号";
            //labShippingOrderNo.Text = "订舱号";
            labAgentOfCarrier.Text = "承运人";
            labState.Text = "状态";
            //labVessel.Text = "船名航次";
            labUser.Text = "订舱";
            labFrom.Text = "从";
            labTo.Text = "到";
            fromToDateMonthControl1.IsEngish = false;
            labIsValid.Text = "有效性";
            lwchkIsValid.CheckedText = "有效的";
            lwchkIsValid.UnCheckedText = "无效的";
            lwchkIsValid.NULLText = "全部";

            nbarBase.Caption = "基本信息";
            nbarDate.Caption = "日期";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            InitControls();
        }

        private void InitControls()
        {
            #region enum

            SetState();


            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchType.All) continue;
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.SelectedIndex = 0;

            #endregion

            #region 船东
            Utility.SetEnterToExecuteOnec(mcmbAirline, delegate
            {
                ICPCommUIHelper.BindCustomerList(mcmbAirline,CustomerType.Airline);
            });

            #endregion

            #region 用户和部门

            List<OrganizationList> userCompanyList = SetCompany();

            //	当前用户所在的部门的所有用户	包含下属部门的所有用户
            Utility.SetEnterToExecuteOnec(mcmbFiler, delegate
            {
                List<Guid> ids = new List<Guid>();
                foreach (var item in userCompanyList) ids.Add(item.ID);

                List<UserList> users = userService.GetUnderlingUserList(ids.ToArray(), null, null, true);
                UserList currentUser = users.Find(delegate(UserList item) { return item.ID == LocalData.UserInfo.LoginID; });
                if (currentUser != null)
                {
                    users.Remove(currentUser);
                    users.Insert(0, currentUser);
                }
                else
                {
                    currentUser = new UserList();
                    currentUser = userService.GetUserInfo(LocalData.UserInfo.LoginID);
                    users.Insert(0, currentUser);
                }

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
                mcmbFiler.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

            //	当前用户所在的操作口岸的空运订舱单中所有揽货人	数据从用户表[sm..Users]检索
            Utility.SetEnterToExecuteOnec(mcmbSales, delegate
            {
                List<Guid> ids = new List<Guid>();
                foreach (var item in userCompanyList) ids.Add(item.ID);

                List<UserList> users = oeService.GetOrderSalesByCompanyIDs(ids.ToArray(), LocalData.IsEnglish);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
                mcmbSales.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });


            //if (!IsLoadFromFinder)
            //{
            //    this.mcmbFiler.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
            //}
            //else
            //{
                //this.mcmbFiler.ShowSelectedValue(Guid.Empty, string.Empty);
            //}

            this.mcmbFiler.SelectedRow += new EventHandler(mcmbFiler_SelectedRow);

            #endregion

            SetControlsEnterToSearch();
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
            if (SearchOrderStates != null)
            {
                foreach (var item in SearchOrderStates)
                {
                    string description = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetDescription<AEOrderState>(item.Value, LocalData.IsEnglish);
                    cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(description, item.Value));
                }
            }
            else
            {
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", null));
                List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AEOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AEOrderState>(LocalData.IsEnglish);

                foreach (var item in orderStates)
                {
                    if (item.Value == AEOrderState.Unknown) continue;
                    cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
                }
            }

            cmbState.SelectedIndex = 0;
        }

        /// <summary>
        /// 设置一些TextBox输入Enter键时,触发查询事件
        /// </summary>
        private void SetControlsEnterToSearch()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) this.btnSearch.PerformClick();
                    };
                }
            }
        }


        private List<OrganizationList> SetCompany()
        {
            List<OrganizationList> userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
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
            //this.txtCtnNO.Text = string.Empty;
            //this.txtOrderNO.Text = string.Empty;
            this.stxtCustomer.Text = string.Empty;
            this.stxtAgentOfCarrier.Text = string.Empty;
            //this.stxtVessel.Text = string.Empty;
            this.txtFlightNo.Text = string.Empty;
            this.stxtDeparture.Text = string.Empty;
            this.stxtDetination.Text = string.Empty;
            this.stxtFinalDestination.Text = string.Empty;

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
                        //this.txtCtnNO.Text = GetFirst(value);
                        break;
                    case "AirShippingOrderNo":
                        //this.txtOrderNO.Text = value;
                        break;
                    case "CustomerName":
                        this.stxtCustomer.Text = value;
                        break;
                    case "AgentOfCarrierName":
                        this.stxtAgentOfCarrier.Text = value;
                        break;
                    case "VesselVoyage":
                        string[] vvs = value.Split('/');
                        if (vvs.Length > 0)
                        {
                            //this.stxtVessel.Text = vvs[0];

                            if (vvs.Length > 1)
                            {
                                string[] left = vvs[1].Split(';');
                                if (left.Length > 0)
                                {
                                    this.txtFlightNo.Text = left[0];
                                }
                            }
                        }
                        break;
                    case "POLName":
                        this.stxtDeparture.Text = value;
                        break;
                    case "PODName":
                        this.stxtDetination.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        this.stxtFinalDestination.Text = value;
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
                //List<AirBookingList> list = new List<AirBookingList>();
                List<AirBookingList> list = oeService.GetAirBookingList(CompanyIDs.ToArray(),
                                                                            txtOperationNo.Text.Trim(),
                                                                            this.txtBLNO.Text.Trim(),
                                                                            txtSCNO.Text.Trim(),
                                                                            stxtCustomer.Text.Trim(),
                                                                            this.mcmbAirline.EditText,
                                                                            stxtAgentOfCarrier.Text.Trim(),
                                                                            this.txtFlightNo.Text.Trim(),
                                                                            stxtDeparture.Text.Trim(),
                                                                            this.stxtDetination.Text.Trim(),
                                                                            stxtFinalDestination.Text.Trim(),
                                                                            SalesID,
                                                                            OwnerID,
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
            foreach (Control item in navBarGroupBase.Controls)
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

        protected virtual List<AEOrderState?> SearchOrderStates { get { return null; } }

        public AEOrderState? OrderStateValue
        {
            get
            {
                if (cmbState.EditValue != null) return (AEOrderState)cmbState.EditValue;
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

        public Guid? OwnerID
        {
            get
            {
                if (mcmbFiler.EditValue == null) return null;
                return new Guid(mcmbFiler.EditValue.ToString());
            }
        }

        #endregion
    }

    public class BookingFinderSearchPart : BookingSearchPart
    {
        protected override List<AEOrderState?> SearchOrderStates
        {
            get
            {
                List<AEOrderState?> states = new List<AEOrderState?>();
                states.Add(AEOrderState.BookingConfirmed);
              
                return states;
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
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
