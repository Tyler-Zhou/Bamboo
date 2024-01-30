using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using ICP.Framework.ClientComponents.Controls;
using ICP.Common.UI;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface.DataObjects;
using DevExpress.XtraEditors;

namespace ICP.FAM.UI.ChargeConfigure
{
    public partial class ChargeConfigListPart : BaseListPart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        /// <summary>
        /// 船东列表
        /// </summary>
        List<CustomerList> carriers = null;

        List<ShippingLineList> shippingLines = new List<ShippingLineList>();

        /// <summary>
        /// 费用列表
        /// </summary>
        List<ChargingCodeList> chargelist = new List<ChargingCodeList>();

        List<LocalFeeConfigure> dataList = null;

        List<ViewList> view = new List<ViewList>();

        public ChargeConfigListPart()
        {
            InitializeComponent();
        }

        private void ChargeConfigListPart_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            List<LocalOrganizationInfo> userCompanyList = FAMUtility.GetCompanyList();
            cmbCompany.Properties.BeginUpdate();
            foreach (var item in userCompanyList)
            {
                cmbCompany.Properties.Items.Add(item.ID, LocalData.IsEnglish ? item.EShortName : item.CShortName,
                                                   CheckState.Checked, true);
            }
            cmbCompany.Properties.EndUpdate();

            carriers = CustomerService.GetCustomerListByList(string.Empty, string.Empty, string.Empty, string.Empty,
                                                            string.Empty, string.Empty, null, null, CustomerStateType.Valid,
                                                            CustomerType.Carrier, null, null, null, null, null, 0);
            CmbCarriers.Properties.BeginUpdate();
            foreach (CustomerList carrier in carriers)
            {
                CmbCarriers.Properties.Items.Add(carrier.ID, LocalData.IsEnglish ? carrier.EName : carrier.CName,
                                                   CheckState.Unchecked, true);
            }
            CmbCarriers.Properties.EndUpdate();

            shippingLines = TransportFoundationService.GetShippingLineList(string.Empty, string.Empty, true, 0);

            cmbShippingLine.Properties.BeginUpdate();
            foreach (ShippingLineList shippingline in shippingLines)
            {
                cmbShippingLine.Properties.Items.Add(shippingline.ID, LocalData.IsEnglish ? shippingline.EName : shippingline.CName,
                                                   CheckState.Unchecked, true);
            }
            cmbShippingLine.Properties.EndUpdate();

            CmbCharges.Properties.Items.Clear();
            chargelist = ConfigureService.GetChargingCodeListBySearch(null, null, null, false, true, 0);

            foreach (var item in chargelist)
            {
                CmbCharges.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName));
            }

        }

        /// <summary>
        /// 新增模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            ChargeConfigEditPart editpart = Workitem.Items.AddNew<ChargeConfigEditPart>();
            editpart.close += delegate
            {
                btnSearch_Click(null, null);
            };
            string title = LocalData.IsEnglish ? "Add ChargeCode Configure" : "新增费用模板设置";
            PartLoader.ShowDialog(editpart, title);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Guid> carriers = new List<Guid>();
            foreach (CheckedListBoxItem item in CmbCarriers.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    carriers.Add(new Guid(item.Value.ToString()));
                }
            }

            List<Guid> shipLines = new List<Guid>();
            foreach (CheckedListBoxItem item in cmbShippingLine.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    shipLines.Add(new Guid(item.Value.ToString()));
                }
            }

            List<Guid> companys = new List<Guid>();
            foreach (CheckedListBoxItem item in cmbCompany.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    companys.Add(new Guid(item.Value.ToString()));
                }
            }
            List<Guid> Locations = new List<Guid>();

            Guid chargeid = CmbCharges.SelectedIndex < 0 ? Guid.Empty : chargelist[CmbCharges.SelectedIndex].ID;

            dataList = FinanceService.GetLoaclFeeConfigureList(carriers.ToArray(), Locations.ToArray(), shipLines.ToArray(), companys.ToArray(), chargeid, DateTime.Now, DateTime.Now, true, LocalData.IsEnglish);
            gcMain.DataSource = dataList;
            gvMain.RefreshData();
        }

        private void gvMain_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (view != null)
                view.ForEach(r => r.Close());
            if (gvMain.FocusedRowHandle < 0)
                return;

            Point screenPoint = MousePosition;
            string datastring = string.Empty;

            if (e.FocusedColumn.FieldName == "CarrierNames")
            {
                ViewList cview = new ViewList();
                datastring = gvMain.GetRowCellDisplayText(gvMain.FocusedRowHandle, e.FocusedColumn);
                if (!string.IsNullOrEmpty(datastring))
                {
                    cview.SetData(datastring);
                    cview.Left = screenPoint.X + 10;
                    cview.Top = screenPoint.Y + 10;

                    cview.Show();
                    view.Add(cview);
                }
            }
            else if (e.FocusedColumn.FieldName == "ShippingLineNames")
            {
                ViewList cview = new ViewList();
                datastring = gvMain.GetRowCellDisplayText(gvMain.FocusedRowHandle, e.FocusedColumn);
                if (!string.IsNullOrEmpty(datastring))
                {
                    cview.SetData(datastring);
                    cview.Left = screenPoint.X + 10;
                    cview.Top = screenPoint.Y + 10;

                    cview.Show();
                    view.Add(cview);
                }
            }
            else if (e.FocusedColumn.FieldName == "CompanyNames")
            {
                ViewList cview = new ViewList();
                datastring = gvMain.GetRowCellDisplayText(gvMain.FocusedRowHandle, e.FocusedColumn);
                if (!string.IsNullOrEmpty(datastring))
                {
                    cview.SetData(datastring);
                    cview.Left = screenPoint.X + 10;
                    cview.Top = screenPoint.Y + 10;

                    cview.Show();
                    view.Add(cview);
                }
            }
            else if (e.FocusedColumn.FieldName == "LocationNames")
            {
                ViewList cview = new ViewList();
                datastring = gvMain.GetRowCellDisplayText(gvMain.FocusedRowHandle, e.FocusedColumn);
                if (!string.IsNullOrEmpty(datastring))
                {
                    cview.SetData(datastring);
                    cview.Left = screenPoint.X + 10;
                    cview.Top = screenPoint.Y + 10;

                    cview.Show();
                    view.Add(cview);
                }
            }
            else if (e.FocusedColumn.FieldName == "Prices")
            {
                ViewList cview = new ViewList();
                datastring = gvMain.GetRowCellDisplayText(gvMain.FocusedRowHandle, e.FocusedColumn);
                if (!string.IsNullOrEmpty(datastring))
                {
                    cview.SetData(datastring);
                    cview.Left = screenPoint.X + 10;
                    cview.Top = screenPoint.Y + 10;

                    cview.Show();
                    view.Add(cview);
                }
            }
        }

        private void gcMain_Leave(object sender, EventArgs e)
        {
            if (view.Count > 0) view.ForEach(r => r.Close());
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0)
            {
                barAdd_ItemClick(null, null);
            }
            else
            {
                barEdit_ItemClick(null, null);
            }
           
        }

        private void gvMain_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (view.Count > 0) view.ForEach(r => r.Close());
            if (e.FocusedRowHandle < 0) return;

            if (gvMain.FocusedColumn.FieldName == "Prices" || gvMain.FocusedColumn.FieldName == "LocationNames" || gvMain.FocusedColumn.FieldName == "CompanyNames" || gvMain.FocusedColumn.FieldName == "ShippingLineNames" || gvMain.FocusedColumn.FieldName == "CarrierNames")
            {
                Point screenPoint = MousePosition;
                string datastring = string.Empty;

                ViewList cview = new ViewList();
                datastring = gvMain.GetRowCellDisplayText(gvMain.FocusedRowHandle, gvMain.FocusedColumn);
                if (!string.IsNullOrEmpty(datastring))
                {
                    cview.SetData(datastring);
                    cview.Left = screenPoint.X + 10;
                    cview.Top = screenPoint.Y + 10;

                    cview.Show();
                    view.Add(cview);
                }
            }
            else
            {
                if (view.Count > 0) view.ForEach(r => r.Close());
            }
        }

        private void CmbCharges_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                CmbCharges.SelectedIndex = -1;
            }
        }

        private void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0)
            {
                return;
            }

            ChargeConfigEditPart editpart = Workitem.Items.AddNew<ChargeConfigEditPart>();
            editpart.SetData(dataList[gvMain.FocusedRowHandle]);
            editpart.close += delegate
            {
                btnSearch_Click(null, null);
            };
            string title = LocalData.IsEnglish ? "Edit ChargeCode Configure" : "编辑费用模板设置";
            PartLoader.ShowDialog(editpart, title);
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0) return;

            string message = LocalData.IsEnglish ? "Are you sure you want to delete NO:" + gvMain.GetFocusedRowCellValue("No").ToString() + " Configure?" : "你确定要删除 NO:" + gvMain.GetFocusedRowCellValue("No").ToString() + " 的费用配置吗?";
            DialogResult result = XtraMessageBox.Show(message, "Tips", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    int rowHandler = gvMain.FocusedRowHandle;
                    Guid id = dataList[rowHandler].ID;
                    FinanceService.DeleteLoaclFeeConfigure(id, LocalData.IsEnglish);

                    gvMain.DeleteRow(rowHandler);
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete Successfully" : "删除成功");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Delete failed" + ex.Message : "删除失败" + ex.Message);
                }
            }
        }
    }
}
