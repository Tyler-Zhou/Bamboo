using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FCM.OceanImport.UI.Common;
using ICP.FCM.Common.ServiceInterface.Common;

namespace ICP.FCM.OceanImport.UI
{
    [ToolboxItem(false)]
    [Microsoft.Practices.CompositeUI.SmartParts.SmartPart]
    public partial class OrderSearchPart : ICP.Framework.ClientComponents.UIFramework.BaseSearchPart
    {
        #region 服务注入
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
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

        #endregion

        #region init

        public OrderSearchPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.OnSearched = null;
                RemoveKeyDownHandle();
                RemoveControlsEnterToSearch();
                this.cmbDateSearchType.SelectedIndexChanged -= this.cmbDateSearchType_SelectedIndexChanged;
                this.cmbDateSearchType.OnFirstEnter -= this.OncmbDateSearchTypeFirstEnter;
                this.cmbState.OnFirstEnter -= this.OncmbStateFirstEnter;
                this.mcmbCarrier.OnFirstEnter -= this.OnmcmbCarrierFirstEnter;
                this.mcmbSales.OnFirstEnter -= this.OnmcmbSalesFirstEnter;
                this.pnlScroll.SizeChanged -= this.pnlScroll_SizeChanged;
                
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }
        public override event ICP.Framework.ClientComponents.UIFramework.SearchResultHandler OnSearched;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
                SetKeyDownToSearch(); ;
            }
        }
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
                    case "AgentName":
                        this.stxtAgent.Text = value;
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

        public override void RaiseSearched()
        {
            btnSearch.PerformClick();
        }
        private void OncmbStateFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIOrderState>(LocalData.IsEnglish);
            orderStates.RemoveAll(item => item.Value == OIOrderState.Unknown);
            this.cmbState.Properties.BeginUpdate();
            foreach (var item in orderStates)
            {
               
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            this.cmbState.Properties.EndUpdate();
        }
        private void OnmcmbCarrierFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.BindCustomerList(mcmbCarrier, CustomerType.Carrier);
        }
        private void OnmcmbSalesFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetMcmbUsersByCommand(this.mcmbSales, CommandConstants.OceanImport_OrderList, true, true);
        }
        private void OncmbDateSearchTypeFirstEnter(object sender, EventArgs e)
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<OIBusinessDateSearchType>> dateSearchTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<OIBusinessDateSearchType>(LocalData.IsEnglish);
            cmbDateSearchType.Properties.BeginUpdate();
            this.cmbDateSearchType.Properties.Items.Clear();
            foreach (var item in dateSearchTypes)
            {
                cmbDateSearchType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbDateSearchType.Properties.EndUpdate();
          
        }
        private void InitControls()
        {

            this.fromToDateMonthControl1.IsEngish = LocalData.IsEnglish;
            this.cmbState.ShowSelectedValue(System.DBNull.Value, LocalData.IsEnglish ? "ALL" : "全部");
            this.cmbState.OnFirstEnter += this.OncmbStateFirstEnter;
          

            //船公司  
            this.mcmbCarrier.OnFirstEnter += this.OnmcmbCarrierFirstEnter;
            

            //揽货人
            this.mcmbSales.OnFirstEnter += this.OnmcmbSalesFirstEnter;
       


            if (ICP.Framework.CommonLibrary.Client.LocalCommonServices.PermissionService.HaveActionPermission(FCMPermissionsConstants.FCM_NAServices))
            {
                ICPCommUIHelper.BindDepartmentByAll(treeBoxSalesDep);
            }
            else
            {
                //绑定用户公司列表
                ICPCommUIHelper.BindCompanyByUser(treeBoxSalesDep, CheckState.Checked);
                this.mcmbSales.ShowSelectedValue(LocalData.UserInfo.LoginID, LocalData.UserInfo.LoginName);
            }
            cmbDateSearchType.SelectedIndexChanged += new EventHandler(cmbDateSearchType_SelectedIndexChanged);
            this.cmbDateSearchType.ShowSelectedValue(OIBusinessDateSearchType.All, LocalData.IsEnglish ? "All Date" : "全部日期");
            this.cmbDateSearchType.OnFirstEnter += this.OncmbDateSearchTypeFirstEnter;
         
           

            SetControlsEnterToSearch();
        }

        private void SetKeyDownToSearch()
        {
            foreach (Control item in panel1.Controls)
            {
                item.KeyDown +=item_KeyDown;
            }
        }
        private void RemoveKeyDownHandle()
        {
            foreach (Control item in panel1.Controls)
            {
                item.KeyDown -=item_KeyDown;
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
        private void RemoveControlsEnterToSearch()
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
        private void OnItemKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.btnSearch.PerformClick();
        }

        void cmbDateSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDateSearchType.EditValue.ToString() == OIBusinessDateSearchType.All.ToString())
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
                if (mcmbSales.EditValue == null || new Guid(mcmbSales.EditValue.ToString()) == Guid.Empty)
                {
                    return null;
                }
                return new Guid(mcmbSales.EditValue.ToString());
            }
        }

        #endregion

        #region ISearchPart 成员

        public override object GetData()
        {
            DateTime? dtFrom = null, dtTo = null;
            OIBusinessDateSearchType dateSearchType = OIBusinessDateSearchType.All;
            OIOrderState? orderState = null;

            if (cmbState.EditValue != null && cmbState.EditValue != System.DBNull.Value)
            {
                orderState = (OIOrderState)cmbState.EditValue;
            }

            if (nbarDate.Expanded && cmbDateSearchType.EditValue != null && cmbDateSearchType.EditValue.ToString() != OIBusinessDateSearchType.All.ToString())
            {
                dateSearchType = (OIBusinessDateSearchType)cmbDateSearchType.EditValue;
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
                List<OceanOrderList> list = OceanImportService.GetOIOrderList(companyIDs.ToArray(),
                                                                            txtOperationNo.Text.Trim(),
                                                                            stxtAgent.Text.Trim(),
                                                                            stxtCustomer.Text.Trim(),
                                                                            mcmbCarrier.EditText,
                                                                            stxtPOL.Text.Trim(),
                                                                            stxtPOD.Text.Trim(),
                                                                            stxtDestination.Text.Trim(),
                                                                            SalesID,
                                                                            orderState,
                                                                            lwchkIsValid.Checked,
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


        #endregion

        #region btn

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (OnSearched != null)
                {
                    using (new CursorHelper())
                    {
                        OnSearched(this, GetData());
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is LWImageComboBoxEdit)
                {
                    (item as LWImageComboBoxEdit).SelectedIndex = 0;
                }
                else if (item is MultiSearchCommonBox)
                {
                    (item as MultiSearchCommonBox).EditValue = null;
                    (item as MultiSearchCommonBox).EditText = string.Empty;
                }
                else if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                {
                    item.Text = string.Empty;
                }
            }

            cmbState.SelectedIndex = 0;
            cmbDateSearchType.SelectedIndex = 0;

        }

        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        [CommandHandler(OIOrderCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                this.RaiseSearched();
            }
        }

        private void pnlScroll_SizeChanged(object sender, EventArgs e)
        {
            bcMain.Width = pnlScroll.Width - 15;
        }
    }

    public class OrderFinderSearchPart : OrderSearchPart
    {

    }
}
