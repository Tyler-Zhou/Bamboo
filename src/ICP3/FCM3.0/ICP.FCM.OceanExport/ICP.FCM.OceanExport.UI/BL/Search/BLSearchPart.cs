using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using Microsoft.Practices.CompositeUI;

using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.FCM.OceanExport.UI.BL;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;

namespace ICP.FCM.OceanExport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class BLSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {

        #region 服务注入
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public IOceanExportService oeService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        #endregion

        #region init

        public BLSearchPart()
        {
            InitializeComponent();
            if (DesignMode) return;

            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
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
            }
        }

        private void SetKeyDownToSearch()
        {
            navBarGroupBase.KeyDown += new KeyEventHandler(item_KeyDown);
            navBarControl1.KeyDown += new KeyEventHandler(item_KeyDown);

            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                && (item is DevExpress.XtraEditors.SpinEdit) == false)
                {
                    item.KeyDown += new KeyEventHandler(item_KeyDown);
                }
            }
        }

        void item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F2 || e.KeyCode == Keys.F5) this.btnSearch.PerformClick();
            else if (e.KeyCode == Keys.F3) this.btnClear.PerformClick();
        }

        private void InitControls()
        {

            this.fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;

            panelChecking.BackColor = OEBLColorConstant.CheckingColor;
            panelFinished.BackColor = OEBLColorConstant.CheckedColor;
            panelRelease.BackColor = OEBLColorConstant.ReleaseColor;

            #region emun

            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OEBLState>> blStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OEBLState>(LocalData.IsEnglish);

            cmbState.Properties.BeginUpdate();

            if (SearchBLState == null)
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "ALL" : "全部", System.DBNull.Value));

            SetCmbStates(blStates);

            cmbState.Properties.EndUpdate();


            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);

            cmbDateSearchType.Properties.BeginUpdate();

            foreach (var item in dateSearchTypes)
            {
                if (item.Value == DateSearchType.All) continue;
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            cmbDateSearchType.Properties.EndUpdate();

            cmbDateSearchType.SelectedIndex = 0;


            #endregion

            //船东
            Utility.SetEnterToExecuteOnec(mcmbCarrier, delegate
            {
                ICPCommUIHelper.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
            });

            #region Company and Sales

            bool isCompanyChanged = true;
            List<OrganizationList> userCompanyList = SetCompany();
            chkcmbCompany.EditValueChanged += delegate
            {
                isCompanyChanged = true;
            };

            //	当前用户所在的操作口岸的海运订舱单中所有揽货人
            mcmbSales.Enter += delegate
            {
                if (isCompanyChanged)
                {
                    if (CompanyIDs.Count == 0)
                    {
                        mcmbSales.ClearItems();
                    }
                    else
                    {
                        List<UserList> saless = userService.GetUnderlingUserList(CompanyIDs.ToArray(), null, null, true);
                        Dictionary<string, string> col = new Dictionary<string, string>();
                        col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
                        col.Add("Code", "代码");
                        mcmbSales.InitSource<UserList>(saless, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
                    }
                    isCompanyChanged = false;
                }
            };
            #endregion

            #region Filer 	当前用户所在的部门的所有用户

            Utility.SetEnterToExecuteOnec(mcmbFiler, delegate
            {
                List<OrganizationList> deps = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, null);
                List<Guid> depIds = new List<Guid>();
                foreach (var item in deps) { depIds.Add(item.ID); }

                List<UserList> filers = userService.GetUnderlingUserList(depIds.ToArray(), null, null, true);
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
                col.Add("Code", "代码");
                mcmbFiler.InitSource<UserList>(filers, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });

            mcmbFiler.ShowSelectedValue(Guid.Empty, "");

            #endregion

            //海外部客服
            Utility.SetEnterToExecuteOnec(mcmbOverseasFiler, delegate
            {
                List<Guid> ids = new List<Guid>();
                foreach (var item in userCompanyList)
                {
                    ids.Add(item.ID);
                }

                List<UserList> userList = userService.GetUnderlingUserList(ids.ToArray(), new string[] { "海外拓展", "客服" }, null, true);

                UserList insertItem = new UserList();
                insertItem.ID = Guid.Empty;
                insertItem.EName = insertItem.CName = string.Empty;
                userList.Insert(0, insertItem);

                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
                col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbOverseasFiler.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            });
        }

        private List<OrganizationList> SetCompany()
        {
            List<OrganizationList> userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);

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
                List<OceanBLList> list = oeService.GetOceanBLList(CompanyIDs.ToArray(),
                                                                    txtOperationNo.Text.Trim(),
                                                                    txtBLNO.Text.Trim(),
                                                                    txtCtnNO.Text.Trim(),
                                                                    txtOrderNO.Text.Trim(),
                                                                    txtSCNO.Text.Trim(),
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
                                                                    DateSearchTypeValue,
                                                                    From,
                                                                    To,
                                                                    int.Parse(numMax.Value.ToString()));

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "total search " + list.Count.ToString() + " data." : "总共查询到 " + list.Count.ToString() + " 条数据.");
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
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Searching..." : "查找数据中...");

                if (OnSearched != null)
                    OnSearched(this, GetData());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in navBarGroupBase.Controls)
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

                if (cmbState.EditValue != null && cmbState.EditValue != System.DBNull.Value) return (OEBLState)cmbState.EditValue;
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
    }

    public class MBLSearchPart : BLSearchPart
    {
        public override object GetData()
        {
            try
            {
                List<OceanBLList> list = oeService.GetOceanMBLList(base.CompanyIDs.ToArray(),
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
