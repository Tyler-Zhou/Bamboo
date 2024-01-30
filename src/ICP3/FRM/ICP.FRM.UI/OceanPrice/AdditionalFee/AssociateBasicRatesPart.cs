using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.FRM.ServiceInterface;

using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents;

namespace ICP.FRM.UI.OceanPrice
{
    [ToolboxItem(false)]
    public partial class AssociateBasicRatesPart : BaseListEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }
        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }

        #endregion

        #region 初始化

        public AssociateBasicRatesPart()
        {
            InitializeComponent();
            Disposed += delegate {
                _AdditionalFees = null;
                _AllAssocBaseRates = null;
                _parentList = null;
                _SearchBaseRates = null;
             
                gcFee.DataSource = null;
                gcRates.DataSource = null;
                bsFee.DataSource = null;
                bsFee.Dispose();
                bsRates.DataSource = null;
                bsRates.Dispose();
                ChangedAdditional = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }

        private void InitMessage()
        {
            RegisterMessage("SaveSuccessfully", "Save Successfully");
            RegisterMessage("CurrentChanged", "The current contract is changed and has not yet been saved. Clicks Yes to save the changes and go to the next conatct. Clicks No to desert all of changing. Clicks Cancel to return.");

            //NativeLanguageService.GetText(this, "SaveSuccessfully")
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        private void InitControls()
        {

            Utility.SearchPartKeyEnterToSearch(new List<Control> 
            {
               txtPOL , txtVIA , txtPOD
                , txtDelivery , txtItemCode , txtCarrier
                , txtComm , txtTerm , txtSurCharge , txtDescription
            }, btnSearch);

            Utility.ShowGridRowNo(gvFee);
            Utility.ShowGridRowNo(gvRates); 

            SetToolTip();
            InitComboboxSource();
        }

        void InitComboboxSource()
        {
            #region AccountType
            List<EnumHelper.ListItem<AccountType>> accountTypes = EnumHelper.GetEnumValues<AccountType>(LocalData.IsEnglish);
            foreach (var item in accountTypes)
            {
                if (item.Value == AccountType.None)
                    cmbAccountType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, item.Value));
                else
                    cmbAccountType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));

            }
            #endregion

            #region carriers

            List<CustomerList> carriers = OceanPriceUIDataHelper.Carriers;
            foreach (var item in carriers)
            {
                rcmbCustomer.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
            }
            #endregion

            #region Currency
            List<CurrencyList> currencyLists = OceanPriceUIDataHelper.Currencys;
            foreach (var item in currencyLists)
                rcmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));

            #endregion
        }

        private void SetToolTip()
        {
            chkCarrierExcl.ToolTip = chkCommExcl.ToolTip = chkDeliveryExcl.ToolTip = chkDescriptionExcl.ToolTip
                = chkItemCodeExcl.ToolTip = chkPODExcl.ToolTip = chkPOLExcl.ToolTip = chkSurChargeExcl.ToolTip
                = chkTermExcl.ToolTip = chkVIAExcl.ToolTip = "Checked if the searching would not contain the value.";

            txtPOD.ToolTip = txtPOL.ToolTip = txtVIA.ToolTip = txtDelivery.ToolTip 
                = "You can use semicolons for dividing multi-Ports, e.g. Yuantian; Hongkong";

            txtItemCode.ToolTip = "You can use semicolons for dividing multi-Item Code, e.g. A.1.3; A.1.4";
            txtCarrier.ToolTip = "You can use semicolons for dividing multi- Carrier, e.g. ABC; BBC";
            //txtComm.ToolTip = "You can use semicolons for dividing multi-Commodity, e.g. FAK; Furniture";
            txtTerm.ToolTip = "You can use semicolons for dividing multi- Term, e.g. CY-CY; CY-CFS";
            txtSurCharge.ToolTip = "You can use semicolons for dividing multi- Term, e.g. CY-CY; CY-CFS";
            txtDescription.ToolTip = "You can use semicolons for dividing multi-Description, e.g. ABC; CBA";
        }

        #endregion

        #region btn

        private void btnClean_Click(object sender, EventArgs e)
        {
            Clean();
        }

        private void Clean()
        {
            txtPOD.Text = txtPOL.Text = txtVIA.Text = txtDelivery.Text= txtItemCode.Text
                = txtCarrier.Text =txtComm.Text =  txtTerm.Text=  txtSurCharge.Text =txtDescription.Text = string.Empty;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int theradID = 0;
            try
            {
                theradID = LoadingServce.ShowLoadingForm("Loading...");


                Search();
            }
            catch (Exception ex)
            {
                LoadingServce.CloseLoadingForm(theradID);
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }


        }

        private void Search()
        {
            List<object> list = OceanPriceService.GetOceanBaseRates(_parentList.ID
                                        , txtPOL.Text.Trim(), chkPOLExcl.Checked
                                        , txtVIA.Text.Trim(), chkVIAExcl.Checked
                                        , txtPOD.Text.Trim(), chkPODExcl.Checked
                                        , txtDelivery.Text.Trim(), chkDeliveryExcl.Checked
                                        , txtFinal.Text.Trim(),chkFinal.Checked
                                        , txtItemCode.Text.Trim(), chkItemCodeExcl.Checked
                                        , txtCarrier.Text.Trim(), chkCarrierExcl.Checked
                                        , txtComm.Text.Trim(), chkCommExcl.Checked
                                        , txtTerm.Text.Trim(), chkTermExcl.Checked
                                        , txtSurCharge.Text.Trim(), chkSurChargeExcl.Checked
                                        , txtDescription.Text.Trim(), chkDescriptionExcl.Checked).Data;


           List<string> unitNameList = (List<string>)list[0];



           if (list != null && list.Count > 1)
           {
               _SearchBaseRates = (List<ClientBaseRatesList>)list[1];
           }
           else
           {
               _SearchBaseRates = new List<ClientBaseRatesList>();
           }


            BulidRatesSouce();

        }

        #endregion

        #region GvEvent

        private void gvRates_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            ClientBaseRatesList item = gvRates.GetRow(e.RowHandle) as ClientBaseRatesList;
            if (item == null) return;

            if (item.IsInCondition == false)
                e.Appearance.ForeColor = Color.Red;

            if (item.CurrentAssoc != item.OriginalAssoc)
                e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Bold);

        }
        private void gvRates_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colRates_CurrentAssoc) return;

            ClientBaseRatesList dr = gvRates.GetRow(e.RowHandle) as ClientBaseRatesList;
            if (dr == null) return;

            dr.CurrentAssoc = !dr.CurrentAssoc;
            gvRates.RefreshData();
        }

        private void gvFee_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column != colFee_Selected) return;

            ClientAdditionalFeeList dr = gvFee.GetRow(e.RowHandle) as ClientAdditionalFeeList;
            if (dr == null) return;

            List<ClientBaseRatesList> rates = bsRates.DataSource as List<ClientBaseRatesList>;
            foreach (var item in rates)
            {
                if (item.OriginalAssoc != item.CurrentAssoc)
                {
                    DialogResult result = XtraMessageBox.Show(
                        NativeLanguageService.GetText(this, "CurrentChanged")
                       , "Tip"
                       , MessageBoxButtons.YesNoCancel
                       , MessageBoxIcon.Question);

                    if (result == DialogResult.Cancel) return;
                    else if (result == DialogResult.No) break;
                    else
                    {
                        bool isSaveSrcc = SaveData();

                        if (isSaveSrcc) break;
                        else return;
                    }
                }
            }


            dr.Selected = !dr.Selected;
            gvFee.RefreshData();

            #region 没有勾选时禁用面板
            List<ClientAdditionalFeeList> selectedFees = _AdditionalFees.FindAll(f => f.Selected);
            if (selectedFees == null || selectedFees.Count == 0) panelRates.Enabled = false;
            else panelRates.Enabled = true;
            #endregion

            #region BaseRates

            List<Guid> feesIds = new List<Guid>();
            foreach (var item in _AdditionalFees) { if(item.Selected ) feesIds.Add(item.ID); }

            List<Guid> selectedFeesBaseRateIDs = GetMergerBaseRateIDs(feesIds);
            _SelectedAssocBaseRates = _AllAssocBaseRates.FindAll(r => selectedFeesBaseRateIDs.Contains(r.ID));

            BulidRatesSouce();

            #endregion
        }

        void BulidRatesSouce()
        {
            List<ClientBaseRatesList> clientRates = new List<ClientBaseRatesList>();
            if (_SearchBaseRates != null && _SearchBaseRates.Count != 0)
            {
                List<ClientBaseRatesList> temp = Utility.Clone<List<ClientBaseRatesList>>(_SearchBaseRates);
                foreach (var item in temp)
                {
                    item.IsInCondition = true;
                }

                clientRates.AddRange(temp);
            }

            if (_SelectedAssocBaseRates != null && _SelectedAssocBaseRates.Count != 0)
            {
                foreach (var item in _SelectedAssocBaseRates)
                {
                    if (_SearchBaseRates == null || _SearchBaseRates.Count == 0)
                    {
                        ClientBaseRatesList tager = Utility.Clone<ClientBaseRatesList>(item);
                        tager.CurrentAssoc = tager.OriginalAssoc = true;
                        tager.IsInCondition = true;
                        clientRates.Add(tager);
                    }
                    else
                    {

                        ClientBaseRatesList tager = clientRates.Find(f => f.ID == item.ID);
                        if (tager != null)
                        {
                            tager.CurrentAssoc = tager.OriginalAssoc = true;
                            tager.IsInCondition = true;
                        }
                        else
                        {
                            tager = Utility.Clone<ClientBaseRatesList>(item);
                            tager.CurrentAssoc = tager.OriginalAssoc = true;
                            tager.IsInCondition = false;
                            clientRates.Add(tager);
                        }
                    }
                }
            }

            bsRates.DataSource = clientRates;
            bsRates.ResetBindings(false);


            if (clientRates.Count > 999)
            {
                gvRates.IndicatorWidth = clientRates.Count.ToString().Length * 13;
            }


        }

        #endregion

        #region interFace

        //所有已关联的Rates
        List<ClientBaseRatesList> _AllAssocBaseRates = null;

        //所有已选择的费用关联的Rates
        List<ClientBaseRatesList> _SelectedAssocBaseRates = null;

        //查询出来的Rates
        List<ClientBaseRatesList> _SearchBaseRates = null;

        List<ClientAdditionalFeeList> _AdditionalFees = null;

        OceanList _parentList = null;
        public void SetSource(OceanList oceanList, List<ClientAdditionalFeeList> orgFees)
        {
            _parentList = oceanList;

            #region AdditionalFees
            //建立一个副本
            _AdditionalFees = Utility.Clone<List<ClientAdditionalFeeList>>(orgFees);

            BulidFeeGridViewColumnsByOceanUnits(_parentList.OceanUnits);
            BulidRatesGridViewColumnsByOceanUnits(_parentList.OceanUnits);

            bsFee.DataSource = _AdditionalFees;
            bsFee.ResetBindings(false);

            #endregion

            #region 获取所有已关联的费用
            List<Guid> allAssocBaseRatesIDs = new List<Guid>();
            foreach (var item in _AdditionalFees)
            {
                foreach (var id in item.BaseRateIDs)
                {
                    if (allAssocBaseRatesIDs.Contains(id) == false) allAssocBaseRatesIDs.Add(id);
                }
            }
            if (allAssocBaseRatesIDs.Count == 0) _AllAssocBaseRates = new List<ClientBaseRatesList>();
            else
            {

                List<object> list = OceanPriceService.GetOceanBaseRatesByIds(allAssocBaseRatesIDs.ToArray()).Data;

                List<string> unitNameList = (List<string>)list[0];


                if (list != null && list.Count > 1)
                {
                    _AllAssocBaseRates = (List<ClientBaseRatesList>)list[1];
                }
                else
                {
                    _AllAssocBaseRates = new List<ClientBaseRatesList>();
                }
            }
            #endregion

            #region BaseRates

            List<Guid> feesIds = new List<Guid>();
            foreach (var item in _AdditionalFees) { feesIds.Add(item.ID); }

            List<Guid> selectedFeesBaseRateIDs = GetMergerBaseRateIDs(feesIds);
            _SelectedAssocBaseRates = _AllAssocBaseRates.FindAll(r => selectedFeesBaseRateIDs.Contains(r.ID));

            foreach (var item in _SelectedAssocBaseRates) 
            { 
                item.CurrentAssoc = item.IsInCondition = item.OriginalAssoc = true;
            }

            bsRates.DataSource = _SelectedAssocBaseRates;
            bsRates.ResetBindings(false);

            #endregion

        }

        /// <summary>
        /// 获取BaseRate的集合(交集)
        /// </summary>
        /// <param name="feeIds">AdditionalFee的ID</param>
        /// <returns>返合BaseRate的集合(交集)</returns>
        private List<Guid> GetMergerBaseRateIDs(List<Guid> feeIds)
        {
            List<ClientAdditionalFeeList> fees = _AdditionalFees.FindAll(f => feeIds.Contains(f.ID));
            if (fees == null || fees.Count == 0) return new List<Guid>();
            if (fees.Count == 1) return fees[0].BaseRateIDs;
           
            //1.先获取最小集合
            ClientAdditionalFeeList minFees = fees.OrderBy(f => f.BaseRateIDs.Count).FirstOrDefault();
            List<Guid> minBaseRateIDs = minFees.BaseRateIDs;
            if (minBaseRateIDs.Count == 0) return new List<Guid>();

            List<ClientAdditionalFeeList> needValidateFees = fees.FindAll(f => f.ID != minFees.ID);
            if (needValidateFees == null || needValidateFees.Count == 0) return minBaseRateIDs;

            foreach (var item in needValidateFees)
            {
                foreach (var id in item.BaseRateIDs)
                {
                    if (minBaseRateIDs.Contains(id) == false) minBaseRateIDs.Remove(id);
                }
            }
            return minBaseRateIDs;
            
        }

        #region BulidColumns

        private void BulidFeeGridViewColumnsByOceanUnits(List<OceanUnitList> units)
        {
            int visibleIndex = 3;

            foreach (var item in units)
            {
                #region  SetVisible= true;
                switch (item.UnitName)
                {
                    case "20GP": colFee_Rate_20GP.VisibleIndex = visibleIndex; break;
                    case "40GP": colFee_Rate_40GP.VisibleIndex = visibleIndex; break;
                    case "40HQ": colFee_Rate_40HQ.VisibleIndex = visibleIndex; break;
                    case "45HQ": colFee_Rate_45HQ.VisibleIndex = visibleIndex; break;
                    case "20NOR": colFee_Rate_20NOR.VisibleIndex = visibleIndex; break;
                    case "40NOR": colFee_Rate_40NOR.VisibleIndex = visibleIndex; break;

                    case "20FR": colFee_Rate_20FR.VisibleIndex = visibleIndex; break;
                    case "20RH": colFee_Rate_20RH.VisibleIndex = visibleIndex; break;
                    case "20RF": colFee_Rate_20RF.VisibleIndex = visibleIndex; break;
                    case "20HQ": colFee_Rate_20HQ.VisibleIndex = visibleIndex; break;
                    case "20TK": colFee_Rate_20TK.VisibleIndex = visibleIndex; break;
                    case "20OT": colFee_Rate_20OT.VisibleIndex = visibleIndex; break;
                    case "20HT": colFee_Rate_20HT.VisibleIndex = visibleIndex; break;

                    case "40TK": colFee_Rate_40TK.VisibleIndex = visibleIndex; break;
                    case "40OT": colFee_Rate_40OT.VisibleIndex = visibleIndex; break;
                    case "40FR": colFee_Rate_40FR.VisibleIndex = visibleIndex; break;
                    case "40HT": colFee_Rate_40HT.VisibleIndex = visibleIndex; break;
                    case "40RH": colFee_Rate_40RH.VisibleIndex = visibleIndex; break;
                    case "40RF": colFee_Rate_40RF.VisibleIndex = visibleIndex; break;

                    case "45GP": colFee_Rate_45GP.VisibleIndex = visibleIndex; break;
                    case "45RF": colFee_Rate_45RF.VisibleIndex = visibleIndex; break;
                    case "45HT": colFee_Rate_45HT.VisibleIndex = visibleIndex; break;
                    case "45FR": colFee_Rate_45FR.VisibleIndex = visibleIndex; break;
                    case "45OT": colFee_Rate_45OT.VisibleIndex = visibleIndex; break;
                    case "45TK": colFee_Rate_45TK.VisibleIndex = visibleIndex; break;
                    case "45RH": colFee_Rate_45RH.VisibleIndex = visibleIndex; break;

                    case "53HQ": colFee_Rate_53HQ.VisibleIndex = visibleIndex; break;
                }
                visibleIndex++;
                #endregion
            }

            colFee_Selected.VisibleIndex = 0;
            colFee_ChargingCodeDescription.VisibleIndex = 1;
            colFee_CustomerID.VisibleIndex = 2;

            colFee_Percent.VisibleIndex = visibleIndex + 1;
            colFee_CurrencyID.VisibleIndex = colFee_Percent.VisibleIndex + 1;
            colFee_FromDate.VisibleIndex = colFee_CurrencyID.VisibleIndex + 1;
            colFee_ToDate.VisibleIndex = colFee_FromDate.VisibleIndex + 1;

        }

        private void BulidRatesGridViewColumnsByOceanUnits(List<OceanUnitList> units)
        {
            int visibleIndex = 12;
            foreach (var item in units)
            {
                #region  SetVisible= true;
                switch (item.UnitName)
                {
                    case "20GP": colRates_Rate_20GP.VisibleIndex = visibleIndex; break;
                    case "40GP": colRates_Rate_40GP.VisibleIndex = visibleIndex; break;
                    case "40HQ": colRates_Rate_40HQ.VisibleIndex = visibleIndex; break;
                    case "45HQ": colRates_Rate_45HQ.VisibleIndex = visibleIndex; break;
                    case "20NOR": colRates_Rate_20NOR.VisibleIndex = visibleIndex; break;
                    case "40NOR": colRates_Rate_40NOR.VisibleIndex = visibleIndex; break;

                    case "20FR": colRates_Rate_20FR.VisibleIndex = visibleIndex; break;
                    case "20RH": colRates_Rate_20RH.VisibleIndex = visibleIndex; break;
                    case "20RF": colRates_Rate_20RF.VisibleIndex = visibleIndex; break;
                    case "20HQ": colRates_Rate_20HQ.VisibleIndex = visibleIndex; break;
                    case "20TK": colRates_Rate_20TK.VisibleIndex = visibleIndex; break;
                    case "20OT": colRates_Rate_20OT.VisibleIndex = visibleIndex; break;
                    case "20HT": colRates_Rate_20HT.VisibleIndex = visibleIndex; break;

                    case "40TK": colRates_Rate_40TK.VisibleIndex = visibleIndex; break;
                    case "40OT": colRates_Rate_40OT.VisibleIndex = visibleIndex; break;
                    case "40FR": colRates_Rate_40FR.VisibleIndex = visibleIndex; break;
                    case "40HT": colRates_Rate_40HT.VisibleIndex = visibleIndex; break;
                    case "40RH": colRates_Rate_40RH.VisibleIndex = visibleIndex; break;
                    case "40RF": colRates_Rate_40RF.VisibleIndex = visibleIndex; break;

                    case "45GP": colRates_Rate_45GP.VisibleIndex = visibleIndex; break;
                    case "45RF": colRates_Rate_45RF.VisibleIndex = visibleIndex; break;
                    case "45HT": colRates_Rate_45HT.VisibleIndex = visibleIndex; break;
                    case "45FR": colRates_Rate_45FR.VisibleIndex = visibleIndex; break;
                    case "45OT": colRates_Rate_45OT.VisibleIndex = visibleIndex; break;
                    case "45TK": colRates_Rate_45TK.VisibleIndex = visibleIndex; break;
                    case "45RH": colRates_Rate_45RH.VisibleIndex = visibleIndex; break;

                    case "53HQ": colRates_Rate_53HQ.VisibleIndex = visibleIndex; break;
                }
                visibleIndex++;
                #endregion
            }

            colRates_CurrentAssoc.VisibleIndex =0;
            colRates_OriginalAssoc.VisibleIndex =1;
            colRates_Account.VisibleIndex =2;
            colRates_AccountType.VisibleIndex = 3;
            colRates_Carrier.VisibleIndex =4;
            colRates_POL.VisibleIndex =5;
            colRates_VIA.VisibleIndex =6;
            colRates_POD.VisibleIndex =7;
            colRates_PlaceOfDelivery.VisibleIndex =8;
            colRates_FinalDestination.VisibleIndex = 9;
            colRates_ItemCode.VisibleIndex =10;
            colRates_Comm.VisibleIndex =11;
            colRates_TransportClause.VisibleIndex =12;

            colRates_SurCharge.VisibleIndex = visibleIndex + 1;
            colRates_ClosingDate.VisibleIndex = colRates_SurCharge.VisibleIndex + 1;
            colRates_TransitTime.VisibleIndex = colRates_ClosingDate.VisibleIndex + 1;
            colRates_Description.VisibleIndex = colRates_TransitTime.VisibleIndex + 1;
            colRates_FromDate.VisibleIndex = colRates_Description.VisibleIndex + 1;
            colRates_ToDate.VisibleIndex = colRates_FromDate.VisibleIndex + 1;
            colRates_BasePortNo.VisibleIndex = colRates_ToDate.VisibleIndex + 1;
            colRates_OriginalArbitraryNo.VisibleIndex = colRates_BasePortNo.VisibleIndex + 1;
            colRates_DestinationArbitraryNo.VisibleIndex = colRates_OriginalArbitraryNo.VisibleIndex + 1;
        }

        #endregion

        #endregion

        #region 工作流

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }
        List<ClientBaseRatesList> SelectedOceanItem
        {
            get
            {
                int[] rowIndexs = gvRates.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<ClientBaseRatesList> tagers = new List<ClientBaseRatesList>();
                foreach (var item in rowIndexs)
                {
                    ClientBaseRatesList dr = gvRates.GetRow(item) as ClientBaseRatesList;
                    if (dr != null) tagers.Add(dr);
                }

                return tagers;
            }
        }
        /// <summary>
        /// 关联全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAssociateAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<ClientBaseRatesList> rates = bsRates.DataSource as List<ClientBaseRatesList>;
                if (rates != null && rates.Count > 0)
                {
                    foreach (var item in rates) { item.CurrentAssoc = true; }

                    gvRates.RefreshData();
                }
            }
        }
        /// <summary>
        /// 取消全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barUnassociateAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<ClientBaseRatesList> rates = bsRates.DataSource as List<ClientBaseRatesList>;
                if (rates != null && rates.Count > 0)
                {
                    foreach (var item in rates) { item.CurrentAssoc = false; }

                    gvRates.RefreshData();
                }
            }
        }
        /// <summary>
        /// 关联选择部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAssociateSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                foreach (var item in SelectedOceanItem)
                { 
                    item.CurrentAssoc = true; 
                }
                gvRates.RefreshData();
            }
        }
        /// <summary>
        /// 取消选择的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barUnassocateSelect_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                foreach (var item in SelectedOceanItem) 
                { 
                    item.CurrentAssoc = false;
                }
                gvRates.RefreshData();
            }
        }

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SaveData();
            }
        }

        public override bool SaveData()
        {
            //附加费不会因为关联了直挂港而自动关联到加驳船启运港运费上!
            MessageBoxService.ShowWarning("Additional fee will not automatically associated on the arbitrary POL if you only associate with basic POL.");
            List<ClientBaseRatesList> rates = bsRates.DataSource as List<ClientBaseRatesList>;

             List<ClientBaseRatesList> selectRates=new List<ClientBaseRatesList>();

            List<Guid> feeIDs = new List<Guid>();
            List<Guid> rateIDs = new List<Guid>();
            List<bool> isAdditionals = new List<bool>();
            //保存前的关系
            Dictionary<Guid, List<Guid>> additionals = new Dictionary<Guid, List<Guid>>();

            #region 

            List<Guid> allSelectedFeeIds = new List<Guid>();

            foreach (var item in _AdditionalFees)
            {
                if (item.Selected == false) continue;

                additionals.Add(item.ID,item.BaseRateIDs);
                allSelectedFeeIds.Add(item.ID);
            }


            foreach (var item in rates)
            {
                if (item.CurrentAssoc == false && item.OriginalAssoc == false) continue;

                if(item.CurrentAssoc)
                {
                    selectRates.Add(item);
                }

                //新增的关系
                if (item.CurrentAssoc && item.OriginalAssoc == false)
                {
                    foreach (var selectedFeeId in allSelectedFeeIds)
                    {
                        feeIDs.Add(selectedFeeId);
                        rateIDs.Add(item.ID);
                        isAdditionals.Add(true);
                    }
                }
                //需删除的关系
                else if (item.OriginalAssoc == true && item.CurrentAssoc == false)
                {
                    foreach (var selectedFeeId in allSelectedFeeIds)
                    {

                        feeIDs.Add(selectedFeeId);
                        rateIDs.Add(item.ID);
                        isAdditionals.Add(false);
                    }
                }


            }
            #endregion

            if (feeIDs.Count == 0 || rateIDs.Count == 0 || isAdditionals.Count == 0) return false;

            try
            {
                ManyResult result = OceanPriceService.SetOceanAdditionalFee2ItemInfo(feeIDs.ToArray()
                                                                            , rateIDs.ToArray()
                                                                            , isAdditionals.ToArray()
                                                                            , LocalData.UserInfo.LoginID);

                #region 构建保存后关系 
                for (int i = 0; i < feeIDs.Count; i++)
                {
                    if (additionals.ContainsKey(feeIDs[i]) == false) continue;

                    if(isAdditionals[i])
                    {
                        if (additionals[feeIDs[i]].Contains(rateIDs[i]) == false) additionals[feeIDs[i]].Add(rateIDs[i]);
                    }
                    else
                        additionals[feeIDs[i]].Remove(rateIDs[i]);
                }

                foreach (var item in additionals)
                {
                    ClientAdditionalFeeList tager = _AdditionalFees.Find(f => f.ID == item.Key);
                    if (tager != null) { tager.BaseRateIDs = item.Value; }
                }
                #endregion

                foreach (var item in rates)
                {
                    item.OriginalAssoc = item.CurrentAssoc;
                }

                _SelectedAssocBaseRates = selectRates;

                gvRates.RefreshData();


                if (ChangedAdditional != null)
                {
                    ChangedAdditional(additionals);
                }



                LocalCommonServices.Statusbar.SetStatusBarPanel(NativeLanguageService.GetText(this, "SaveSuccessfully"));
                return true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
                //LocalCommonServices.ErrorTrace.SetErrorInfo(this, ex); 
                return false; 
            }
        }

        #endregion

        #region 事件

        public delegate void ChangedAdditionalHandler(Dictionary<Guid, List<Guid>> additionals);
        /// <summary>
        /// 保存过后返回Fee和Item的关联
        /// </summary>
        public event ChangedAdditionalHandler ChangedAdditional;

        #endregion

     

    }

    /// <summary>
    /// 为了构建服务端保存数据的帮助类
    /// </summary>
    class FeeAndRates
    {
        public Guid FeeID { get; set; }
        public Guid RatesID { get; set; }
    }
}
