using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;

using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FCM.DomesticTrade.UI
{
    [ToolboxItem(false)]
    [SmartPart]
    public partial class BookingSearchPart : BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IDomesticTradeService DomesticTradeService
        {
            get
            {
                return ServiceClient.GetService<IDomesticTradeService>();
            }
        }
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
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

        public BookingSearchPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            if (LocalData.IsEnglish == false) SetCnText();

            Disposed += delegate {
                OnSearched = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            Load += new EventHandler(BookingSearchPart_Load);
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
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2) btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) btnClear.PerformClick();
        }

        void SetCnText()
        {
            btnClear.Text = "清空(&L)";
            btnSearch.Text = "查询(&S)";

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
            if (DesignMode) return;

            InitControls();
        }

        private void InitControls()
        {
            #region enum

            SetState();


            List<EnumHelper.ListItem<DateSearchType>> dateSearchTypes = EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchType.All) continue;
                cmbDateSearchType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.SelectedIndex = 0;

            #endregion

            #region 船东
            Utility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelper.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
            });

            #endregion

            #region 用户和部门

            List<OrganizationList> userCompanyList = SetCompany();

            //	当前用户所在的部门的所有用户	包含下属部门的所有用户
            Utility.SetEnterToExecuteOnec(mcmbFiler, delegate
            {
                List<Guid> ids = new List<Guid>();
                foreach (var item in userCompanyList) ids.Add(item.ID);

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
            });

            //	当前用户所在的操作口岸的海运订舱单中所有揽货人	数据从用户表[sm..Users]检索
            Utility.SetEnterToExecuteOnec(mcmbSales, delegate
            {
                List<Guid> ids = new List<Guid>();
                foreach (var item in userCompanyList) ids.Add(item.ID);

                List<UserList> users = DomesticTradeService.GetOrderSalesByCompanyIDs(ids.ToArray(), LocalData.IsEnglish);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ?"Name":"名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");
                mcmbSales.InitSource<UserList>(users, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });


            if (!IsLoadFromFinder)
            {
                mcmbFiler.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
            }
            else
            {
                mcmbFiler.ShowSelectedValue(Guid.Empty, string.Empty);
            }
            mcmbFiler.SelectedRow += new EventHandler(mcmbFiler_SelectedRow);

            #endregion

            SetControlsEnterToSearch();
        }

        void mcmbFiler_SelectedRow(object sender, EventArgs e)
        {
            Workitem.State["BookingerId"] = OwnerID;
        }

        /// <summary>
        /// 设置状态查询栏位的可查状态值
        /// </summary>
        protected virtual void SetState()
        {
            List<EnumHelper.ListItem<DTOrderState>> orderStates = EnumHelper.GetEnumValues<DTOrderState>(LocalData.IsEnglish);
            if (SearchOrderState == null) cmbState.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", DBNull.Value));
            foreach (var item in orderStates)
            {
                if (SearchOrderState != null && item.Value != SearchOrderState.Value)continue;
                else if (item.Value == DTOrderState.Unknown || item.Value == DTOrderState.Rejected) continue;

                cmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
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
                if (item is TextEdit
                && (item is SpinEdit) == false)
                {
                    item.KeyDown += delegate(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
                    };
                }
            }
        }


        private List<OrganizationList> SetCompany()
        {
            List<OrganizationList> userCompanyList = UserService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
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

        public override event SearchResultHandler OnSearched;

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

            txtOperationNo.Text = string.Empty;
            txtCtnNO.Text = string.Empty;
            txtOrderNO.Text = string.Empty;
            stxtCustomer.Text = string.Empty;
            stxtAgentOfCarrier.Text = string.Empty;
            stxtVessel.Text = string.Empty;
            txtVoayge.Text = string.Empty;
            stxtPOL.Text = string.Empty;
            stxtPOD.Text = string.Empty;
            stxtPlaceOfDelivery.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "No":
                        txtOperationNo.Text = value;
                        break;
                    //case "MBLNo":
                    //    this.txtBLNO.Text = GetFirst(value);
                    //    break;
                    //case "HBLNo":
                    //    this.txtBLNO.Text = GetFirst(value);
                    //    break;
                    case "ContainerNo":
                        txtCtnNO.Text = GetFirst(value);
                        break;
                    case "OceanShippingOrderNo":
                        txtOrderNO.Text = value;
                        break;
                    case"CustomerName":
                        stxtCustomer.Text = value;
                        break;
                    case "AgentOfCarrierName":
                        stxtAgentOfCarrier.Text = value;
                        break;
                    case "VesselVoyage":
                        string[] vvs = value.Split('/');
                        if (vvs.Length > 0)
                        {
                            stxtVessel.Text = vvs[0];

                            if (vvs.Length > 1)
                            {
                                string[] left = vvs[1].Split(';');
                                if (left.Length > 0)
                                {
                                    txtVoayge.Text = left[0];
                                }
                            }
                        }
                        break;
                    case "POLName":
                        stxtPOL.Text = value;
                        break;
                    case "PODName":
                        stxtPOD.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        stxtPlaceOfDelivery.Text = value;
                        break;
                }
            }
        }

        public override object GetData()
        {
            if (CompanyIDs.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "At least one company must be selected." : "必须至少选择一个操作口岸.");

                return null;
            }
            try
            {
                List<DTBookingList> list = DomesticTradeService.GetDTBookingList(CompanyIDs.ToArray(),
                                                                            txtOperationNo.Text.Trim(),
                                                                            txtCtnNO.Text.Trim(),
                                                                            txtOrderNO.Text.Trim(),
                                                                            txtSCNO.Text.Trim(),
                                                                            stxtCustomer.Text.Trim(),
                                                                            mcmbCarrier.EditText,
                                                                            stxtAgentOfCarrier.Text.Trim(),
                                                                            stxtVessel.Text.Trim(),
                                                                            txtVoayge.Text.Trim(),
                                                                            stxtPOL.Text.Trim(),
                                                                            stxtPOD.Text.Trim(),
                                                                            stxtPlaceOfDelivery.Text.Trim(),
                                                                            SalesID,
                                                                            OwnerID,
                                                                            lwchkIsValid.Checked,
                                                                            OrderStateValue,
                                                                            DateSearchTypeValue,
                                                                            From,
                                                                            To,
                                                                            int.Parse(numMax.Value.ToString()),
                                                                            LocalData.IsEnglish);

                return list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return null; }
        }

        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (OnSearched != null)
                OnSearched(this, GetData());
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
                else if (item is TextEdit
                         && (item is SpinEdit) == false
                         && item.Enabled == true
                         && (item as TextEdit).Properties.ReadOnly == false)
                {
                    item.Text = string.Empty;
                }

                cmbDateSearchType.SelectedIndex = 0;
            }
        }

        #endregion

        #region 公共成员

        protected virtual DTOrderState? SearchOrderState { get { return null; } }

        public DTOrderState? OrderStateValue
        {
            get
            {
                if (SearchOrderState != null) return SearchOrderState;

                if (cmbState.EditValue != null && cmbState.EditValue != DBNull.Value) return (DTOrderState)cmbState.EditValue;
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
                foreach (CheckedListBoxItem item in chkcmbCompany.Properties.Items)
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
        protected override DTOrderState? SearchOrderState { get { return DTOrderState.BookingConfirmed; } }

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
