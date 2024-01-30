using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface;
using System.Linq;
using ICP.Common.UI;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using System.Data;
using DevExpress.XtraGrid.Columns;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;

namespace ICP.FAM.UI.ChargeConfigure
{
    public partial class ChargeConfigEditPart : BaseEditPart
    {
        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        public IGeographyService geographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        #region 属性

        /// <summary>
        /// 公司设置
        /// </summary>
        ConfigureInfo configureInfo = null;

        /// <summary>
        /// 船东列表
        /// </summary>
        List<CustomerList> carriers = null;

        /// <summary>
        /// 航线列表
        /// </summary>
        List<ShippingLineList> shippingLines = new List<ShippingLineList>();

        /// <summary>
        /// 国家列表
        /// </summary>
        List<CountryList> Countrylist = new List<CountryList>();

        /// <summary>
        /// 城市列表
        /// </summary>
        List<LocationList> listCity = new List<LocationList>();

        /// <summary>
        /// 币种列表
        /// </summary>
        List<SolutionCurrencyList> currencyList = new List<SolutionCurrencyList>();

        /// <summary>
        /// 数据列表
        /// </summary>
        List<LocalFeeConfigure> datasours = new List<LocalFeeConfigure>();
        LocalFeeConfigure currentConfig = null;

        /// <summary>
        /// 费用列表
        /// </summary>
        List<ChargingCodeList> chargelist = new List<ChargingCodeList>();

        DataTable chargeTable = new DataTable();
        List<ViewList> view = new List<ViewList>();
        bool ischange = true;
        public delegate void CloseRefult();
        public event CloseRefult close;
        #endregion


        public ChargeConfigEditPart()
        {
            InitializeComponent();

            Disposed += delegate
            {
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
        }

        private void ChargeConfigEditPart_Load(object sender, EventArgs e)
        {
            SetCN();
            InitControls();
            DataBind();
        }

        /// <summary>
        ///调整中文状态控件位置
        /// </summary>
        private void SetCN()
        {
            checkCompany.Left = 45;
            checkCompany.Width = 70;
            checkCarrier.Left = 45;
            checkCarrier.Width = 70;
            labCharge.Left = 88;
            labCharge.Width = 50;

            checkLocation.Left = 442;
            checkLocation.Width = 70;
            checkShipLine.Left = 442;
            checkShipLine.Width = 70;
        }

        public void SetData(LocalFeeConfigure config)
        {
            currentConfig = config;
        }

        private void DataBind()
        {
            if (currentConfig == null)
                return;

            checkCompany.Checked = currentConfig.IsCommpany;
            checkCarrier.Checked = currentConfig.IsCarrier;
            checkShipLine.Checked = currentConfig.IsShippingLine;
            checkLocation.Checked = currentConfig.IsLocation;

            if (checkCompany.Checked)
            {
                foreach (CheckedListBoxItem item in cmbCompany.Properties.Items)
                {
                    if (currentConfig.CompanyIDs.Contains(new Guid(item.Value.ToString())))
                    {
                        item.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        item.CheckState = CheckState.Unchecked;
                    }
                }
            }

            if (checkCarrier.Checked)
            {
                foreach (CheckedListBoxItem item in CmbCarriers.Properties.Items)
                {
                    if (currentConfig.CarrierIDs.Contains(new Guid(item.Value.ToString())))
                    {
                        item.CheckState = CheckState.Checked;
                    }
                }
            }

            if (checkShipLine.Checked)
            {
                foreach (CheckedListBoxItem item in cmbShippingLine.Properties.Items)
                {
                    if (currentConfig.ShippingLineIDs.Contains(new Guid(item.Value.ToString())))
                    {
                        item.CheckState = CheckState.Checked;
                    }
                }
            }

            if (checkLocation.Checked)
            {
                foreach (CheckedListBoxItem item in cmbCityState.Properties.Items)
                {
                    if (currentConfig.ShippingLineIDs.Contains(new Guid(item.Value.ToString())))
                    {
                        item.CheckState = CheckState.Checked;
                    }
                }
            }

            if (checkLocation.Checked)
            {
                cmbCityState.Properties.Items.Clear();
                foreach (LocationList ll in listCity)
                {
                    cmbCityState.Properties.Items.Add(ll.ID, LocalData.IsEnglish ? ll.EName : ll.EName,
                                           CheckState.Checked, true);
                }
            }

            CmbCharges.SelectedIndex = chargelist.FindIndex(r => r.ID == currentConfig.ChargeID);
            cmbChargeUnit.SelectedIndex = (int)currentConfig.ChargeUnit - 1;
            cmbCurrency.SelectedIndex = currencyList.FindIndex(r => r.CurrencyID == currentConfig.CurrencyID);
            if (cmbChargeUnit.SelectedIndex != 1)
            {
                numPrice.Value = Convert.ToDecimal(currentConfig.Prices.Substring(currentConfig.Prices.IndexOf(":") + 1, currentConfig.Prices.Length - 1 - currentConfig.Prices.IndexOf(":")));
            }
            else
            {
                panel2.Visible = true;
                barAddUnit.Visibility = BarItemVisibility.Always;
                numPrice.Enabled = false;

                string[] priceArr = currentConfig.Prices.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string price, unit;
                foreach (string pricestr in priceArr)
                {
                    unit = pricestr.Substring(0, pricestr.IndexOf(":"));
                    price = pricestr.Substring(pricestr.IndexOf(":") + 1, pricestr.Length - 1 - pricestr.IndexOf(":"));

                    if (!chargeTable.Columns.Contains(unit))
                    {
                        SetColumn(ViewUnit.Columns.AddVisible(unit, unit), unit);

                        DataColumn col = new DataColumn(unit);
                        col.DataType = typeof(decimal);
                        chargeTable.Columns.Add(col);
                    }

                    chargeTable.Rows[0][unit] = Convert.ToDecimal(price);
                }
                gridUnit.DataSource = chargeTable;
                ViewUnit.RefreshData();
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //操作口岸列表   
            //Utility.SetEnterToExecuteOnec(cmbCompany, delegate
            //{
            //    ICPCommUIHelper.BindCompanyByUser(cmbCompany, false);
            //});

            //cmbCompany.ShowSelectedValue(LocalData.UserInfo.DefaultCompanyID, LocalData.UserInfo.DefaultCompanyName);

            List<LocalOrganizationInfo> userCompanyList = FAMUtility.GetCompanyList();
            cmbCompany.Properties.BeginUpdate();
            cmbCompany.Properties.Items.Clear();
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
                CmbCarriers.Properties.Items.Add(carrier.ID, LocalData.IsEnglish ? '(' + carrier.Code.Trim() + ')' + carrier.EName : '(' + carrier.Code.Trim() + ')' + carrier.CName,
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

            Countrylist = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            Countrylist = Countrylist.OrderBy(r => r.EName).ToList();
            cmbCountry.Properties.BeginUpdate();
            foreach (CountryList country in Countrylist)
            {
                cmbCountry.Properties.Items.Add(LocalData.IsEnglish ? country.EName : country.CName);
            }
            cmbCountry.Properties.EndUpdate();
            cmbCountry.SelectedIndexChanged += new EventHandler(cmbCountry_SelectedIndexChanged);

            cmbChargeUnit.Properties.BeginUpdate();
            cmbChargeUnit.Properties.Items.Add(LocalData.IsEnglish ? "Vote" : "票");
            cmbChargeUnit.Properties.Items.Add(LocalData.IsEnglish ? "Cont" : "柜");
            cmbChargeUnit.Properties.Items.Add(LocalData.IsEnglish ? "HBL" : "HBL");
            cmbChargeUnit.Properties.EndUpdate();
            cmbChargeUnit.SelectedIndex = 0;

            BindComboboxByCompany(LocalData.UserInfo.DefaultCompanyID);

            #region 添加常用箱型
            SetColumn(ViewUnit.Columns.AddVisible("20GP", "20GP"), "20GP");
            SetColumn(ViewUnit.Columns.AddVisible("20HQ", "20HQ"), "20HQ");
            SetColumn(ViewUnit.Columns.AddVisible("40GP", "40GP"), "40GP");
            SetColumn(ViewUnit.Columns.AddVisible("40HQ", "40HQ"), "40HQ");
            SetColumn(ViewUnit.Columns.AddVisible("40NOR", "40NOR"), "40NOR");
            SetColumn(ViewUnit.Columns.AddVisible("45HQ", "45HQ"), "45HQ");
            SetColumn(ViewUnit.Columns.AddVisible("45GP", "45GP"), "45GP");

            //SetColumn(gvCon.Columns.AddVisible("20GP", "20GP"), "20GP");
            //SetColumn(gvCon.Columns.AddVisible("20HQ", "20HQ"), "20HQ");
            //SetColumn(gvCon.Columns.AddVisible("40GP", "40GP"), "40GP");
            //SetColumn(gvCon.Columns.AddVisible("40HQ", "40HQ"), "40HQ");
            //SetColumn(gvCon.Columns.AddVisible("40NOR", "40NOR"), "40NOR");
            //SetColumn(gvCon.Columns.AddVisible("45HQ", "45HQ"), "45HQ");
            //SetColumn(gvCon.Columns.AddVisible("45GP", "45GP"), "45GP");45GP", "45GP"), "45GP");


            //gvCon.Columns["_20GP"].Visible = true;
            //gvCon.Columns["_20HQ"].Visible = true;
            //gvCon.Columns["_40GP"].Visible = true;
            //gvCon.Columns["_40HQ"].Visible = true;
            //gvCon.Columns["_40NOR"].Visible = true;
            //gvCon.Columns["_45HQ"].Visible = true;
            //foreach (GridColumn gc in gvCon.Columns)
            //{
            //    gc.Caption = gc.FieldName.Substring(1, gc.FieldName.Length - 1);
            //}

            DataColumn GP20 = new DataColumn("20GP");
            GP20.DataType = typeof(decimal);
            chargeTable.Columns.Add(GP20);
            DataColumn HQ20 = new DataColumn("20HQ");
            HQ20.DataType = typeof(decimal);
            chargeTable.Columns.Add(HQ20);
            DataColumn GP40 = new DataColumn("40GP");
            GP40.DataType = typeof(decimal);
            chargeTable.Columns.Add(GP40);
            DataColumn HQ40 = new DataColumn("40HQ");
            HQ40.DataType = typeof(decimal);
            chargeTable.Columns.Add(HQ40);
            DataColumn NOR40 = new DataColumn("40NOR");
            NOR40.DataType = typeof(decimal);
            chargeTable.Columns.Add(NOR40);
            DataColumn HQ45 = new DataColumn("45HQ");
            HQ45.DataType = typeof(decimal);
            chargeTable.Columns.Add(HQ45);
            DataColumn GP45 = new DataColumn("45GP");
            GP45.DataType = typeof(decimal);
            chargeTable.Columns.Add(GP45);
            chargeTable.Rows.Add(chargeTable.NewRow());

            gridUnit.DataSource = chargeTable;
            ViewUnit.RefreshData();
            #endregion
        }

        /// <summary>
        /// 按国家绑定下拉框
        /// </summary>
        /// <param name="companyid"></param>
        private void BindComboboxByCompany(Guid companyid)
        {
            configureInfo = ConfigureService.GetCompanyConfigureInfo(companyid);

            CmbCharges.Properties.Items.Clear();
            chargelist = ConfigureService.GetChargingCodeListBySearch(null, null, null, false, true, 0);

            foreach (var item in chargelist)
            {
                CmbCharges.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName));
            }

            currencyList = ConfigureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);

            cmbCurrency.Properties.Items.Clear();
            cmbCurrency.Properties.BeginUpdate();
            foreach (SolutionCurrencyList currency in currencyList)
            {
                cmbCurrency.Properties.Items.Add(currency.CurrencyName);
            }
            cmbCurrency.Properties.EndUpdate();
            cmbCurrency.SelectedIndex = currencyList.FindIndex(r => r.CurrencyID == configureInfo.StandardCurrencyID);
        }

        private void SetColumn(GridColumn column, string name)
        {
            column.Caption = name;
            column.FieldName = name;
            column.Name = "col" + name;
            column.DisplayFormat.FormatType = FormatType.Numeric;
            column.DisplayFormat.FormatString = "N2";
            column.OptionsColumn.AllowEdit = true;
            column.Width = 60;
        }

        /// <summary>
        /// 国家发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCountry.EditValue == null)
                return;
            LoadCity();
        }

        /// <summary>
        /// 加载国家所属城市
        /// </summary>
        private void LoadCity()
        {
            if (cmbCountry.EditValue == null)
            {
                return;
            }

            using (new CursorHelper(Cursors.WaitCursor))
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "正在加载城市信息...");
                //根据国家 加载城市信息
                cmbCityState.Properties.Items.Clear();

                string countryName = cmbCountry.SelectedItem.ToString();
                if (countryName != string.Empty)
                {
                    List<CountryList> list = geographyService.GetCountryList(string.Empty, countryName, true, 0);
                    if (list.Count > 0)
                    {
                        listCity = geographyService.GetLocationList(string.Empty,
                                                                   list[0].ID,
                                                                   null,
                                                                   true,
                                                                   null,
                                                                   null,
                                                                   true,
                                                                   0);

                        listCity = listCity.OrderBy(r => r.EName).ToList();
                        foreach (LocationList ll in listCity)
                        {
                            cmbCityState.Properties.Items.Add(ll.ID, LocalData.IsEnglish ? ll.EName : ll.EName,
                                                   CheckState.Unchecked, true);
                        }

                        cmbCityState.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// 公司发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbCompany.SelectedIndex < 0)
            //{
            //    return;
            //}

            //BindComboboxByCompany(new Guid(cmbCompany.Value.ToString()));
        }

        private void barAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CmbCharges.SelectedIndex < 0)
            {
                string message = LocalData.IsEnglish ? "Please select the charge code first!" : "请选择需要设置的费用代码！";
                XtraMessageBox.Show(message, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChargingCodeList selcharge = chargelist[CmbCharges.SelectedIndex];
            SolutionCurrencyList selCurrency = currencyList[cmbCurrency.SelectedIndex];
            string price = string.Empty;

            string companyNames = string.Empty;
            List<Guid> companyIDs = new List<Guid>();
            if (checkCompany.Checked)
            {
                foreach (CheckedListBoxItem item in cmbCompany.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        companyIDs.Add(new Guid(item.Value.ToString()));
                        if (string.IsNullOrEmpty(companyNames))
                        {
                            companyNames = item.Description;
                        }
                        else
                        {
                            companyNames = companyNames + "," + item.Description;
                        }
                    }
                }
            }
            else
            {
                companyNames = LocalData.IsEnglish ? "All Companys" : "所有公司";
            }

            string carrierNames = string.Empty;
            List<Guid> carriers = new List<Guid>();
            if (checkCarrier.Checked)
            {
                foreach (CheckedListBoxItem item in CmbCarriers.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        carriers.Add(new Guid(item.Value.ToString()));
                        if (string.IsNullOrEmpty(carrierNames))
                        {
                            carrierNames = item.Description;
                        }
                        else
                        {
                            carrierNames = carrierNames + "," + item.Description;
                        }
                    }
                }
            }

            string shipLineNames = string.Empty;
            List<Guid> shipLines = new List<Guid>();
            if (checkShipLine.Checked)
            {
                foreach (CheckedListBoxItem item in cmbShippingLine.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        shipLines.Add(new Guid(item.Value.ToString()));
                        if (string.IsNullOrEmpty(shipLineNames))
                        {
                            shipLineNames = item.Description;
                        }
                        else
                        {
                            shipLineNames = shipLineNames + "," + item.Description;
                        }
                    }
                }
            }

            string cityNames = string.Empty;
            List<Guid> citys = new List<Guid>();
            if (checkLocation.Checked)
            {
                foreach (CheckedListBoxItem item in cmbCityState.Properties.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        citys.Add(new Guid(item.Value.ToString()));
                        if (string.IsNullOrEmpty(cityNames))
                        {
                            cityNames = item.Description;
                        }
                        else
                        {
                            cityNames = cityNames + "," + item.Description;
                        }
                    }
                }
            }

            if (cmbChargeUnit.SelectedIndex != 1)
            {
                price = numPrice.Value.ToString("N2");
            }
            else
            {
                foreach (DataColumn col in chargeTable.Columns)
                {
                    if (chargeTable.Rows[0][col] != null && chargeTable.Rows[0][col] != DBNull.Value)
                    {
                        if (string.IsNullOrEmpty(price))
                        {
                            price = col.ColumnName + ":" + chargeTable.Rows[0][col].ToString();
                        }
                        else
                        {
                            price += "," + col.ColumnName + ":" + chargeTable.Rows[0][col].ToString();
                        }
                    }
                }
            }

            if (currentConfig == null)
            {
                currentConfig = new LocalFeeConfigure();
            }

            currentConfig.CarrierIDs = carriers.ToArray();
            currentConfig.CarrierNames = carrierNames;
            currentConfig.ChargeID = selcharge.ID;
            currentConfig.ChargeName = LocalData.IsEnglish ? selcharge.EName : selcharge.CName;
            currentConfig.ChargeUnit = (byte)(cmbChargeUnit.SelectedIndex + 1);
            currentConfig.ChargeUnitName = cmbChargeUnit.Text;
            currentConfig.CompanyIDs = companyIDs.ToArray();
            currentConfig.CompanyNames = companyNames;
            currentConfig.CurrencyID = selCurrency.CurrencyID;
            currentConfig.CurrencyName = selCurrency.CurrencyName;
            currentConfig.IsCarrier = checkCarrier.Checked;
            currentConfig.IsCommpany = checkCompany.Checked;
            currentConfig.IsLocation = checkLocation.Checked;
            currentConfig.IsShippingLine = checkShipLine.Checked;
            currentConfig.LocationIDs = citys.ToArray();
            currentConfig.LocationNames = cityNames;
            currentConfig.ShippingLineIDs = shipLines.ToArray();
            currentConfig.ShippingLineNames = shipLineNames;
            currentConfig.Prices = price;
            currentConfig.IsValid = true;

            if (!datasours.Contains(currentConfig))
            {
                datasours.Add(currentConfig);
            }

            gcMain.DataSource = datasours;
            gvMain.RefreshData();

            //chargeTable.Rows.Clear();
            //chargeTable.Rows.Add(chargeTable.NewRow());
            //ViewUnit.RefreshData();

            //ischange = false;
        }

        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (gvMain.FocusedRowHandle < 0) return;

            ViewUnit.DeleteRow(gvMain.FocusedRowHandle);
        }

        /// <summary>
        /// 费用单位变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbChargeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChargeUnit.SelectedIndex != 1)
            {
                panel2.Visible = false;
                barAddUnit.Visibility = BarItemVisibility.Never;
                numPrice.Enabled = true;
            }
            else
            {
                panel2.Visible = true;
                barAddUnit.Visibility = BarItemVisibility.Always;
                numPrice.Enabled = false;
            }
        }

        /// <summary>
        /// 添加新箱型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAddUnit_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddContainer voucherPart = Workitem.Items.AddNew<AddContainer>();
            string title = LocalData.IsEnglish ? "Add Container Type" : "增加箱型";
            voucherPart.AddS += delegate(string name, decimal price)
            {
                bool exist = false;
                foreach (GridColumn vc in ViewUnit.Columns)
                {
                    if (vc.FieldName == name)
                    {
                        exist = true;
                    }
                }
                if (!exist)
                {
                    SetColumn(ViewUnit.Columns.AddVisible(name, name), name);
                }


                //GridColumn c = gvCon.Columns["_" + name];
                //if (c != null)
                //{
                //    c.Visible = true;
                //}

                //Type entityType = cons[0].GetType();
                //PropertyInfo propertyInfo = entityType.GetProperty("_" + name);
                //propertyInfo.SetValue(cons[0], price, null);

                if (!chargeTable.Columns.Contains(name))
                {
                    DataColumn col = new DataColumn(name);
                    col.DataType = typeof(decimal);
                    chargeTable.Columns.Add(name);
                }

                gridUnit.DataSource = chargeTable;
                ViewUnit.SetRowCellValue(0, name, price);
                ViewUnit.RefreshData();
            };
            PartLoader.ShowDialog(voucherPart, title);
        }

        private void ViewUnit_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < -1)
            {
                return;
            }

            //Type entityType = cons[0].GetType();
            //PropertyInfo propertyInfo = entityType.GetProperty("_" + e.Column.FieldName);
            //propertyInfo.SetValue(cons[0],Convert.ToDecimal(e.Value),null);
        }

        private void checkCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCompany.Checked)
            {
                cmbCompany.Enabled = true;
            }
            else
            {
                cmbCompany.Enabled = false;
            }
        }

        private void checkShipLine_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShipLine.Checked)
            {
                cmbShippingLine.Enabled = true;
            }
            else
            {
                cmbShippingLine.Enabled = false;
            }
        }

        private void checkCarrier_CheckedChanged(object sender, EventArgs e)
        {
            if (checkCarrier.Checked)
            {
                CmbCarriers.Enabled = true;
            }
            else
            {
                CmbCarriers.Enabled = false;
            }
        }

        private void checkLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (checkLocation.Checked)
            {
                cmbCountry.Enabled = true;
                cmbCityState.Enabled = true;
            }
            else
            {
                cmbCountry.Enabled = false;
                cmbCityState.Enabled = false;
            }
        }

        private void gvMain_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (view.Count > 0) view.ForEach(r => r.Close());
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

        private void ChargeConfigEditPart_Closing(object sender, FormClosingEventArgs e)
        {
            if (view.Count > 0) view.ForEach(r => r.Close());

            if (close != null) close();
        }

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (CmbCharges.SelectedIndex < 0)
                {
                    string message = LocalData.IsEnglish ? "Please select the charge code first!" : "请选择需要设置的费用代码！";
                    XtraMessageBox.Show(message, "Tips", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                barAdd_ItemClick(null, null);

                LocalFeeConfigureSaveRequest save = new LocalFeeConfigureSaveRequest();
                save.CarrierIDs = currentConfig.CarrierIDs;
                save.ChargeID = currentConfig.ChargeID;
                save.ChargeUnit = currentConfig.ChargeUnit;
                List<string> ChargeUnits = new List<string>();
                List<string> Prices = new List<string>();

                if (cmbChargeUnit.SelectedIndex != 1)
                {
                    ChargeUnits.Add(cmbChargeUnit.Text);
                    Prices.Add(numPrice.Value.ToString());
                }
                else
                {
                    string[] priceArr = currentConfig.Prices.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string price, unit;
                    foreach (string pricestr in priceArr)
                    {
                        unit = pricestr.Substring(0, pricestr.IndexOf(":"));
                        price = pricestr.Substring(pricestr.IndexOf(":") + 1, pricestr.Length - 1 - pricestr.IndexOf(":"));

                        ChargeUnits.Add(unit);
                        Prices.Add(price);
                    }
                }

                save.ChargeUnits = ChargeUnits.ToArray();
                save.Prices = Prices.ToArray();
                save.CompanyIDs = currentConfig.CompanyIDs;
                save.CurrencyID = currentConfig.CurrencyID;
                save.ENDDate = DateTime.Parse("2037-12-31");
                save.Id = currentConfig.ID;
                save.IsCarrier = currentConfig.IsCarrier;
                save.IsCommpany = currentConfig.IsCommpany;
                save.IsLocation = currentConfig.IsLocation;
                save.IsShippingLine = currentConfig.IsShippingLine;
                save.IsValid = currentConfig.IsValid;
                save.LocationIDs = currentConfig.LocationIDs;
                save.SaveByID = LocalData.UserInfo.LoginID;
                save.ShippingLineIDs = currentConfig.ShippingLineIDs;
                save.StartDate = DateTime.Now;
                save.UpdateDate = currentConfig.UpdateDate;

                SingleResult result = FinanceService.SaveLocalFeeConfigure(save);
                if (result != null)
                {
                    currentConfig.ID = result.GetValue<Guid>("ID");
                    currentConfig.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save failed" + ex.Message : "保存失败" + ex.Message);
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


    }
}
