using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.FRM.ServiceInterface;
using ICP.FRM.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FRM.UI.OceanPrice
{
    /// <summary>
    /// 合约编辑界面
    /// </summary>
    [ToolboxItem(false)]
    [SmartPart]
    public partial class OPContractEditPart : BaseEditPart
    {
        #region 服务注入
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 合约服务
        /// </summary>
        public IOceanPriceService OceanPriceService
        {
            get
            {
                return ServiceClient.GetService<IOceanPriceService>();
            }
        }
        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 基础数据服务管理
        /// </summary>
        public ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 合约UI辅助服务
        /// </summary>
        public OceanPriceUIDataHelper OceanPriceUIDataHelper
        {
            get
            {
                return ClientHelper.Get<OceanPriceUIDataHelper, OceanPriceUIDataHelper>();
            }
        }
        #endregion

        #region 属性

        OceanInfo _CurrentData = null;

        OceanCustomers CurrentOceanCustomers
        {
            get { return bsOceanCustomers.Current as OceanCustomers; }
        }

        OceanUnitList CurrentOceanUnit
        {
            get { return bsRateUnit.Current as OceanUnitList; }
            set
            {
                OceanUnitList unit = CurrentOceanUnit;
                unit = value;
            }
        }

        #endregion

        #region init
        /// <summary>
        /// 合约编辑界面
        /// </summary>
        public OPContractEditPart()
        {
            InitializeComponent();
            Enabled = false;
            Disposed += delegate {
                Saved = null;
                gcRate.DataSource = null;
                gcSCN.DataSource = null;
                
                bsOceanCustomers.DataSource = null;
                bsOceanCustomers.Dispose();
                bsOceanInfo.DataSource = null;
                bsOceanInfo.Dispose();
                bsRateUnit.DataSource = null;
                bsRateUnit.Dispose();
                _CurrentData = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!DesignMode) { InitMessage(); }
        }
        /// <summary>
        /// 注册提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("SaveSuccessfully", "Save Successfully");
            RegisterMessage("SearchBoxToolTip", "Please input Code, Chinese name or English name.");
            RegisterMessage("RemarkToolTip", "Clicks the button Remark to view detail.");


            RegisterMessage("ValidateFromDate", "Duration(Form) must be less than Duration(To).");
            RegisterMessage("ValidateCarrierName", "Carrier must input.");

            RegisterMessage("ValidateUnitCount",  "Must Input more then zero units.");
           
            RegisterMessage("ValidateUnit", "Unit Must Input.");
            RegisterMessage("ValidateUnitExist", "RateUnit Exist.");
            RegisterMessage("RemoveUnit", "All of rates with the container will be removed, are you sure?");
            RegisterMessage("EditRemarkTitel", "Edit Remark");

            RegisterMessage("Publish", "&Publish");
            RegisterMessage("Pause", "&Pause");
        }
        /// <summary>
        /// 重写加载，初始化控
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            InitControls();
            SearchRegister();
            bsOceanCustomers.PositionChanged += delegate
            {
                barRemoveCustomer.Enabled = CurrentOceanCustomers != null;
            };

            panelMain.Click += delegate { panelMain.Focus(); };

            SetEnabledByRateType();

            BaseDataObject data = bsOceanInfo.DataSource as BaseDataObject;
            if (data != null) data.BeginEdit(); 

            base.OnLoad(e);
        }

        private void InitControls()
        {
            SetToolTip();

            #region ShippingLine

            Utility.SetEnterToExecuteOnec(cmbShipLine, delegate
            {
                List<ShippingLineList> shippingLines = OceanPriceUIDataHelper.ShippingLines;
                cmbShipLine.Properties.BeginUpdate();
                cmbShipLine.Properties.Items.Clear();
                foreach (ShippingLineList item in shippingLines)
                {
                    cmbShipLine.Properties.Items.Add(new ImageComboBoxItem(item.EName , item.ID));
                }
                cmbShipLine.Properties.EndUpdate();

            });
           
            #endregion

            #region Payment

            List<DataDictionaryList> list = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.PaymentTerm, true, 0);
            DataDictionaryList both = new DataDictionaryList() { ID = Guid.Empty, EName = "BOTH" };
            list.Insert(0, both);
            cmbPayment.Properties.BeginUpdate();
            foreach (var item in list)
            {
                cmbPayment.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.ID));
            }
            cmbPayment.Properties.EndUpdate();
            #endregion

            #region Container

            List<ContainerList> ctnLists = TransportFoundationService.GetContainerList(string.Empty, true, 0);
            rcmbRateUnit.Properties.BeginUpdate();
            foreach (var item in ctnLists)
            {
                rcmbRateUnit.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            rcmbRateUnit.Properties.EndUpdate();
            #endregion

            #region Currency
            List<CurrencyList> currencyLists = OceanPriceUIDataHelper.Currencys;
            cmbCurrency.Properties.BeginUpdate();
            foreach (var item in currencyLists)
            {
                cmbCurrency.Properties.Items.Add(new ImageComboBoxItem(item.Code, item.ID));
            }
            cmbCurrency.Properties.EndUpdate();
            #endregion

            #region Enum

            List<EnumHelper.ListItem<RateType>> rateTypes = EnumHelper.GetEnumValues<RateType>(LocalData.IsEnglish);
            cmbRateType.Properties.BeginUpdate();
            foreach (var item in rateTypes)
            {
                cmbRateType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbRateType.Properties.EndUpdate();

            cmbRateType.SelectedIndexChanged += cmbRateType_SelectedIndexChanged;

            List<EnumHelper.ListItem<ContractType>> contractTypes = EnumHelper.GetEnumValues<ContractType>(LocalData.IsEnglish);
            cmbContractType.Properties.BeginUpdate();
            foreach (var item in contractTypes)
            {
                cmbContractType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbContractType.Properties.EndUpdate();
            #endregion
        }

        private void SetToolTip()
        {
            txtCarrier.ToolTip =  
            colNotifyName.ToolTip =
            colShipperName.ToolTip =
            colConsigneeName.ToolTip = NativeLanguageService.GetText(this, "SearchBoxToolTip");


            toolTip1.SetToolTip(txtRemark,NativeLanguageService.GetText(this, "SearchBoxToolTip"));
        }

        void SearchRegister()
        {
            #region Shipper

            DataFindClientService.RegisterGridColumnFinder(colShipperName
                                              , CommonFinderConstants.CustoemrFinder
                                              , "ShipperID"
                                              , "ShipperName"
                                              , "ID"
                                              , LocalData.IsEnglish ? "EName" : "EName");
            #endregion

            #region Consignee

            DataFindClientService.RegisterGridColumnFinder(colConsigneeName
                                            , CommonFinderConstants.CustoemrFinder
                                            , "ConsigneeID"
                                            , "ConsigneeName"
                                            , "ID"
                                            , LocalData.IsEnglish ? "EName" : "EName");
            #endregion

            #region Notify

            DataFindClientService.RegisterGridColumnFinder(colNotifyName
                                          , CommonFinderConstants.CustoemrFinder
                                          , "NotifyID"
                                          , "NotifyName"
                                          , "ID"
                                          , LocalData.IsEnglish ? "EName" : "EName");
            #endregion


            #region 船东

        DataFindClientService.Register(txtCarrier, CommonFinderConstants.CustomerCarrierFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
          delegate(object inputSource, object[] resultData)
          {
              if (_CurrentData == null)
                  return;
              txtCarrier.Text = _CurrentData.CarrierName  = resultData[1].ToString() ;
              txtCarrier.Tag = _CurrentData.CarrierID = new Guid(resultData[0].ToString());

          }, ClientConstants.MainWorkspace);


            #endregion
        }

        void gvSCN_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (_CurrentData != null) _CurrentData.IsDirty = true;
        }

        void gvRate_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (_CurrentData != null) _CurrentData.IsDirty = true;
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            txtContractNo.Focus();
            if (data == null)
            {
                _CurrentData = null;
                txtRemark.Rtf = "";
                bsOceanInfo.DataSource = typeof(OceanInfo);
                bsOceanCustomers.DataSource = new List<OceanCustomers>();
                bsRateUnit.DataSource = new List<OceanUnitList>();
                Enabled = false;
            }
            else
            {
                OceanInfo curData = data as OceanInfo;
                if (curData == null) return;

                bsOceanInfo.DataSource = _CurrentData = curData;
                bsOceanCustomers.DataSource = _CurrentData.OceanCustomers;
                bsRateUnit.DataSource = _CurrentData.OceanUnits;

                Enabled = _CurrentData.Permission >= OceanPermission.Edit;

                //船公司
                cmbShipLine.ShowSelectedValue(_CurrentData.ShippingLineID, _CurrentData.ShippingLineName);

                ((BaseDataObject)data).CancelEdit();
                ((BaseDataObject)data).BeginEdit();

                if (_CurrentData.IsNew) Workitem.Commands[OPCommonConstants.Command_InsterNewData].Execute();


                if (_CurrentData.State == OceanState.Expired || _CurrentData.State == OceanState.Invalidated || _CurrentData.State == OceanState.Draft)
                {
                    barPublish.Caption = NativeLanguageService.GetText(this, "Publish");
                    barSave.Enabled = true;
                }
                else
                {
                    barPublish.Caption = NativeLanguageService.GetText(this, "Pause");
                    barSave.Enabled = false;
                }
                txtRemark.Rtf = _CurrentData.Remark;
                _CurrentData.IsDirty = false;

            }
            gvRate.CellValueChanged -= gvRate_CellValueChanged;
            gvSCN.CellValueChanged -= gvSCN_CellValueChanged;
            gvRate.CellValueChanged += gvRate_CellValueChanged;
            gvSCN.CellValueChanged += gvSCN_CellValueChanged;
        }

        public override object DataSource
        {
            get { return bsOceanInfo.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return Save();
        }

        public override void EndEdit()
        {
            Validate();
            bsOceanCustomers.EndEdit();
            bsOceanInfo.EndEdit();
            bsRateUnit.EndEdit();
        }
        /// <summary>
        /// 保存数据后
        /// </summary>
        public override event SavedHandler Saved;

        #endregion

        #region IPart成员

        public override void Init(IDictionary<string, object> values)
        {
             
        }

        #endregion

        #region Window Event
        /// <summary>
        /// 合约类型
        /// </summary>
        void cmbRateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SetEnabledByRateType();
            }
        }
        /// <summary>
        /// 收发通:新增
        /// </summary>
        private void barInsert_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddSCNData();
        }
        /// <summary>
        /// 收发通:删除
        /// </summary>
        private void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteSCNData();
        }
        /// <summary>
        /// 箱:新增
        /// </summary>
        private void barInsertRateUnit_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddRateUnit();
        }
        /// <summary>
        /// 箱:删除
        /// </summary>
        private void barRemoveRateUnit_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteRateUnit();
        }
        /// <summary>
        /// 保存合约
        /// </summary>
        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save();
            }
        }
        /// <summary>
        /// 发布合约
        /// </summary>
        private void barPublish_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Workitem.Commands[OPCommonConstants.Command_PublishPauseData].Execute();
            }
        }
        /// <summary>
        /// 打开Remark编辑框
        /// </summary>
        private void labRemark_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditRemarkPart er = new EditRemarkPart();
            er.SetSouce(txtRemark.Rtf);

            DialogResult dr = PartLoader.ShowDialog(er, NativeLanguageService.GetText(this, "EditRemarkTitel"), FormBorderStyle.Sizable, FormWindowState.Normal, true, false);

            if (dr == DialogResult.OK) _CurrentData.Remark = txtRemark.Rtf = er.Remark;
        }
        /// <summary>
        /// Remark更改时赋值到当前合约实体
        /// </summary>
        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (_CurrentData != null)
                _CurrentData.Remark = txtRemark.Rtf;
        }
        #endregion

        #region Method
        #region Save
        private bool Save()
        {
            EndEdit();

            OceanInfo currentData = bsOceanInfo.DataSource as OceanInfo;

            if (currentData == null) return false;

            if (currentData.IsNew == false && currentData.IsDirty == false) return true;



            if (ValidateData() == false) return false;

            try
            {

                List<Guid> unitIds = currentData.OceanUnits.Select(item => item.UnitID).ToList();

                SingleResultData result = OceanPriceService.SaveOceanInfo(currentData.ID
                                                            , currentData.ContractNo
                                                            , currentData.ContractName
                                                            , currentData.ContractType
                                                            , currentData.CarrierID
                                                            , currentData.ShippingLineID
                                                            , currentData.PaymentTermID
                                                            , currentData.CurrencyID
                                                            , currentData.FromDate
                                                            , currentData.ToDate
                                                            , currentData.RateType
                                                            , currentData.ShipperIDs
                                                            , currentData.ConsigneeIDs
                                                            , currentData.NotifyIDs
                                                            , unitIds.ToArray()
                                                            , currentData.Remark
                                                            , LocalData.UserInfo.LoginID
                                                            , currentData.UpdateDate);


                if (currentData.State == OceanState.Published)
                {
                    currentData.State = OceanState.Draft;
                    SetPublish();
                }

                currentData.ID = result.ID;
                currentData.UpdateDate = result.UpdateDate;
                currentData.ShippingLineName = cmbShipLine.Text;
                currentData.CancelEdit();
                currentData.BeginEdit();
                if (Saved != null) Saved(currentData, new object[] { result });
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), NativeLanguageService.GetText(this, "SaveSuccessfully"));
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                return false;
            }
        }
        /// <summary>
        /// 发布合约
        /// </summary>
        public void SetPublish()
        {
            barPublish.Enabled = true;
            barPublish.Caption = NativeLanguageService.GetText(this, "Publish");
        }
        /// <summary>
        /// 验证录入数据
        /// </summary>
        /// <returns></returns>
        public bool ValidateData()
        {
            OceanInfo currentData = _CurrentData;
            Validate();
            bsOceanCustomers.EndEdit();
            bsOceanInfo.EndEdit();
            bsRateUnit.EndEdit();

            dxErrorProvider1.ClearErrors();
            dxErrorProvider2.ClearErrors();
            dxErrorProvider3.ClearErrors();

            bool isSrcc = true;
            if (currentData.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (currentData.FromDate > currentData.ToDate)
                            e.SetErrorInfo("FromDate", NativeLanguageService.GetText(this, "ValidateFromDate"));

                        if (currentData.RateType == RateType.Contract && currentData.CarrierID.IsNullOrEmpty())
                            e.SetErrorInfo("CarrierName", NativeLanguageService.GetText(this, "ValidateCarrierName"));

                        #region SCN
                        if (currentData.RateType != RateType.Market && currentData.OceanCustomers != null)
                        {
                            //Shipper必输入
                            foreach (var item in currentData.OceanCustomers)
                            {
                                if (item.Validate(delegate(ValidateEventArgs ce)
                                {
                                    //if (Utility.GuidIsNullOrEmpty(item.ShipperID))
                                    //    ce.SetErrorInfo("ShipperName", LocalData.IsEnglish ? "Shipper Must Input." : "发货人必须输入.");

                                }) == false) isSrcc = false;

                            }
                        }
                        #endregion

                        #region OceanUnits

                        if (currentData.OceanUnits == null || currentData.OceanUnits.Count == 0)
                        {
                            e.SetErrorInfo("OceanUnits", NativeLanguageService.GetText(this, "ValidateUnitCount"));
                        }
                        else
                        {
                            List<Guid> unitIds = new List<Guid>();
                            //箱型必须输入
                            foreach (var item in currentData.OceanUnits)
                            {
                                if (item.Validate(delegate(ValidateEventArgs ce)
                                {
                                    if (Utility.GuidIsNullOrEmpty(item.UnitID))

                                        ce.SetErrorInfo("UnitID", NativeLanguageService.GetText(this, "ValidateUnit"));
                                    else if (unitIds.Contains(item.UnitID))
                                    {
                                        ce.SetErrorInfo("UnitID", NativeLanguageService.GetText(this, "ValidateUnitExist"));
                                    }
                                    else
                                        unitIds.Add(item.UnitID);

                                }) == false) isSrcc = false;

                            }
                        }

                        #endregion
                    }
                ) == false)
            {
                isSrcc = false;
            }
            return isSrcc;
        }
        #endregion

        #region 收发通SCN
        private void AddSCNData()
        {
            List<OceanCustomers> lists = bsOceanCustomers.DataSource as List<OceanCustomers>;
            if (lists == null)
            {
                _CurrentData.OceanCustomers = new List<OceanCustomers>();
                bsOceanCustomers.DataSource = lists = _CurrentData.OceanCustomers;
            }

            OceanCustomers oc = new OceanCustomers();
            lists.Insert(0, oc);
            bsOceanCustomers.DataSource = lists;
            bsOceanCustomers.ResetBindings(false);

            _CurrentData.IsDirty = true;
        }
        private void DeleteSCNData()
        {
            if (CurrentOceanCustomers == null) return;

            OceanCustomers currentData = CurrentOceanCustomers;
            List<OceanCustomers> lists = bsOceanCustomers.DataSource as List<OceanCustomers>;
            lists.Remove(currentData);
            bsOceanCustomers.DataSource = lists;
            bsOceanCustomers.ResetBindings(false);

            _CurrentData.IsDirty = true;
        }

        #endregion

        #region RateUnit

        private void AddRateUnit()
        {
            List<OceanUnitList> lists = bsRateUnit.DataSource as List<OceanUnitList>;
            if (lists == null)
            {
                _CurrentData.OceanUnits = new List<OceanUnitList>();
                bsRateUnit.DataSource = lists = _CurrentData.OceanUnits;
            }

            OceanUnitList unit = new OceanUnitList();
            unit.OceanID = _CurrentData.ID;
            lists.Add(unit);
            bsRateUnit.DataSource = lists;
            bsRateUnit.ResetBindings(false);

            _CurrentData.IsDirty = true;
        }

        private void DeleteRateUnit()
        {
            if (CurrentOceanUnit == null) return;
            //All of the container's rates will be removed, are you sure?
            if (_CurrentData.IsNew == false)
            {

                DialogResult result = XtraMessageBox.Show(
                                                NativeLanguageService.GetText(this, "RemoveUnit"),
                                                 "Tip",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

            }

            OceanUnitList unit = CurrentOceanUnit;
            List<OceanUnitList> lists = bsRateUnit.DataSource as List<OceanUnitList>;
            lists.Remove(unit);
            bsRateUnit.DataSource = lists;
            bsRateUnit.ResetBindings(false);

            _CurrentData.IsDirty = true;
        }
        #endregion
        void SetEnabledByRateType()
        {
            EndEdit();

            txtCarrier.Enabled = cmbShipLine.Enabled = groupSCN.Enabled = true;
            groupRate.Enabled = true;

            if (_CurrentData == null) return;

            RateType rateType = _CurrentData.RateType;

            if (rateType == RateType.Market)
            {
                txtCarrier.Enabled = false;

                txtCarrier.Text = _CurrentData.CarrierName = string.Empty;
                txtCarrier.Tag = _CurrentData.CarrierID = Guid.Empty;

                groupSCN.Enabled = false;
                bsOceanCustomers.DataSource = _CurrentData.OceanCustomers = new List<OceanCustomers>();
            }
        }
        #endregion
    }
}
