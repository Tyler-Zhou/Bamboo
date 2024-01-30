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
using ICP.FCM.Common.ServiceInterface.Common;

namespace ICP.FCM.OceanExport.UI.Order
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OrderSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
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

        #region init 

        public OrderSearchPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false)
            {
               // SetCnText();
            }
            this.Disposed += delegate {
                this.OnSearched = null;
                this.RemoveKeyDownHandle();
                this.RemoveControlEnterHandle();
                this.cmbDateSearchType.SelectedIndexChanged -= this.cmbDateSearchType_SelectedIndexChanged;
                this.mcmbSales.SelectedRow -= this.mcmbSales_SelectedRow;
                this.mcmbOverseasFiler.OnFirstEnter -= this.OnMcmbOnverseasFilerEnter;
                this.mcmbCarrier.OnFirstEnter -= this.OnMcmbCarrierEnter;
                this.mcmbSales.OnFirstEnter -= this.OnMcmbSalesEnter;
                this.cmbState.OnFirstEnter -= this.OnCmbStateEnter;
                this.cmbDateSearchType.OnFirstEnter -= this.OnCmbDateSearchTypeEnter;
                
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

        }
        private void SetCnText()
        {
            labCarrier.Text = "船公司";
            this.btnClear.Text = "清空(&L)";
            this.btnSearch.Text = "查询(&S)";
            labSalesDepartment.Text = "揽货部门";
            labCustomer.Text = "客户";
            labDestination.Text = "交货地";
            labFrom.Text = "从";
            labTo.Text = "到";
            fromToDateMonthControl1.IsEngish = false;

            labMax.Text = "最大行数";
            labOperationNo .Text = "业务号";
            labPOD.Text = "卸货港";
            labPOL.Text = "装货港";
            labSales.Text = "揽货人";
            labOverseasFiler.Text = "海外部客服";
            labState.Text = "状态";

            labIsValid.Text = "有效性";
            lwchkIsValid.CheckedText = "有效";
            lwchkIsValid.NULLText = "全部";
            lwchkIsValid.UnCheckedText = "无效";

            treeBoxSalesDep.AllText = "全选";
            nbarBase.Caption = "基本信息";
            nbarDate.Caption = "日期";
        }

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
            foreach (Control item in panel4.Controls)
            {
                item.KeyDown +=item_KeyDown;
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
        private void OnCmbStateEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OEOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OEOrderState>(LocalData.IsEnglish);
            this.cmbState.Properties.BeginUpdate();
            foreach (var item in orderStates)
            {
                if (item.Value == OEOrderState.Unknown) continue;
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbState.Properties.EndUpdate();
        }
        private void OnMcmbCarrierEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCustomerList(this.mcmbCarrier, CustomerType.Carrier);
        }
        private void OnMcmbSalesEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetMcmbUsersByCommand(this.mcmbSales, CommandConstants.OceanExport_OrderList, true, true);
        }
        private void OnMcmbOnverseasFilerEnter(object sender, EventArgs e)
        {
            List<Guid> companyIDs = treeBoxSalesDep.EditValue;
            List<UserList> userList = UserService.GetUnderlingUserList(null, new string[] { "海外客服" }, null, true);

            UserList insertItem = new UserList();
            insertItem.ID = Guid.Empty;
            insertItem.EName = insertItem.CName = string.Empty;
            userList.Insert(0, insertItem);

            Dictionary<string, string> col = new Dictionary<string, string>();
            col.Add(LocalData.IsEnglish ? "EName" : "CName", LocalData.IsEnglish ? "Name" : "名称");
            col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

            mcmbOverseasFiler.InitSource<UserList>(userList, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
        }
        private void OnCmbDateSearchTypeEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<DateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<DateSearchType>(LocalData.IsEnglish);
            this.cmbDateSearchType.Properties.BeginUpdate();
            this.cmbDateSearchType.Properties.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbDateSearchType.Properties.EndUpdate();
        }
        private void InitControls()
        {
            this.fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;
            this.cmbState.ShowSelectedValue(System.DBNull.Value, LocalData.IsEnglish ? "ALL" : "全部");

            this.cmbState.OnFirstEnter += this.OnCmbStateEnter;
            //船公司
            this.mcmbCarrier.OnFirstEnter += this.OnMcmbCarrierEnter;
            //揽货人
            this.mcmbSales.OnFirstEnter += this.OnMcmbSalesEnter;
            //海外部客服
            this.mcmbOverseasFiler.OnFirstEnter += this.OnMcmbOnverseasFilerEnter;

            

            if (ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices))
            {
                ICPCommUIHelper.BindDepartmentByAll(treeBoxSalesDep);
            }
            else
            {
                ICPCommUIHelper.BindCompanyByUser(treeBoxSalesDep, CheckState.Checked);
                this.mcmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
            }
            this.mcmbSales.SelectedRow += new EventHandler(mcmbSales_SelectedRow);
            cmbDateSearchType.SelectedIndexChanged += new EventHandler(cmbDateSearchType_SelectedIndexChanged);
            this.cmbDateSearchType.ShowSelectedValue(DateSearchType.All, LocalData.IsEnglish ? "All Date" : "全部日期");
            this.cmbDateSearchType.OnFirstEnter += this.OnCmbDateSearchTypeEnter;

            SetControlsEnterToSearch();
        }

        void mcmbSales_SelectedRow(object sender, EventArgs e)
        {
            this.Workitem.State["SalesId"] = this.SalesID;
        }

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

        void cmbDateSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDateSearchType.EditValue.ToString() == DateSearchType.All.ToString())
                fromToDateMonthControl1.Enabled = false;
            else
                fromToDateMonthControl1.Enabled = true;
        }

        #endregion

        #region 属性

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

        #endregion

        #region ISearchPart 成员

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null)
            {
                return;
            }

            this.txtOperationNo.Text = string.Empty;
            this.stxtCustomer.Text = string.Empty;
            this.stxtPOL.Text = string.Empty;
            this.stxtPOD.Text = string.Empty;
            this.stxtDestination.Text = string.Empty;

            foreach (var item in values)
            {
                string value = item.Value == null ? string.Empty : item.Value.ToString();
                switch (item.Key)
                {
                    case "RefNo":
                        txtOperationNo.Text = value;
                        break;
                    case "CustomerName":
                        this.stxtCustomer.Text = value;
                        break;
                    case "POLName":
                        this.stxtPOL.Text = value;
                        break;
                    case "PODName":
                        this.stxtPOD.Text = value;
                        break;
                    case "PlaceOfDeliveryName":
                        this.stxtDestination.Text = value;
                        break;
                }
            }
        }

        public override object GetData()
        {
            DateTime? dtFrom = null, dtTo = null;
            DateSearchType dateSearchType = DateSearchType.All;
            OEOrderState? orderState = null;

            if (cmbState.EditValue != null && cmbState.EditValue != System.DBNull.Value)
            {
                orderState = (OEOrderState)cmbState.EditValue;
            }

            if (nbarDate.Expanded && cmbDateSearchType.EditValue != null && cmbDateSearchType.EditValue.ToString() != DateSearchType.All.ToString())
            {
                dateSearchType = (DateSearchType)cmbDateSearchType.EditValue;
                dtFrom = fromToDateMonthControl1.From;
                dtTo = fromToDateMonthControl1.To;
            }

            List<Guid> companyIDs = treeBoxSalesDep.EditValue;
            

            //List<OrganizationList> userCompanyList = userService.GetUserCompanyList(LocalData.UserInfo.LoginID, OrganizationType.Company);
            //foreach (var item in userCompanyList)
            //{
            //    companyIDs.Add(item.ID);
            //}

            if (treeBoxSalesDep.EditValue.Count == 0)
            {                
                    companyIDs = treeBoxSalesDep.GetAllAvailableValues();                
            }

            try
            {
                List<OceanOrderList> list = OceanExportService.GetOceanOrderList(companyIDs.ToArray(),
                                                                            txtOperationNo.Text.Trim(),
                                                                            stxtCustomer.Text.Trim(),
                                                                            stxtPOL.Text.Trim(),
                                                                            stxtPOD.Text.Trim(),
                                                                            stxtDestination.Text.Trim(),
                                                                            mcmbCarrier.EditText,
                                                                            lwchkIsValid.Checked,
                                                                            orderState,
                                                                            SalesID,
                                                                            OverseasFilerID,
                                                                            dateSearchType,
                                                                            dtFrom,
                                                                            dtTo,
                                                                            int.Parse(numMax.Value.ToString()));

                return list;
            }
            catch (Exception ex) 
            { 
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); 
                return null; 
            }
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
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
            foreach (Control item in  panel4.Controls)
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

            cmbState.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndex = 0;
        }

        #endregion
    }

    public class OrderFinderSearchPart : OrderSearchPart
    { 
        
    }
}
