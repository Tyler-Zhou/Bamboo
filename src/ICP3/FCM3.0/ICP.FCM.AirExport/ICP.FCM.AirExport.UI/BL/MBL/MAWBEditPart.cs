using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.AirExport.UI.BL;
using ICP.Framework.ClientComponents.Controls;
using DevExpress.XtraEditors.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.FAM.ServiceInterface;
using ICP.Common.UI;
using ICP.OA.ServiceInterface.DataObjects;

namespace ICP.FCM.AirExport.UI.MBL
{
    [ToolboxItem(false)]
    public partial class MAWBEditPart : BaseEditPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }

        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public IAirExportService aeService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public ITransportFoundationService TransportFoundationService { get; set; }

        [ServiceDependency]
        public IFinanceClientService finClientService { get; set; }

        #endregion

        #region Init

        public MAWBEditPart()
        {
            InitializeComponent();
            if (LocalData.IsEnglish == false) SetCnText();

            SetNullValuePrompt();
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
        }

        /// <summary>
        /// 防闪烁
        /// </summary>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x0014) return;// 禁掉清除背景消息

            base.WndProc(ref m);
        }

        private void SetCnText()
        {
            labAgent.Text = "代理";

            labChecker.Text = "对单人";
            labConsignee.Text = "收货人";
            labFreightDescription.Text = "支付信息";
            labIssueDate.Text = "签发日期";
            labIssueBy.Text = "签发人";
            labIssuePlace.Text = "签发地";
            labMarks.Text = "唛头";
            labChargeKGSLBS.Text = "计费重量";
            labPaymentTerm.Text = "付款方式";
            labDetination.Text = "目的港";
            labDeparture.Text = "起运港";

            labQuantity.Text = "件数";
            labRefNo.Text = "业务号";

            labShipper.Text = "发货人";
            labWeight.Text = "毛重";

            barSubCheck.Caption = "完成对单(&D)";
            //barCheck.Caption = "申请(&K)";
            //barCheckDone.Caption = "完成(&D)";
            barRefresh.Caption = "刷新(&R)";

            barSave.Caption = "保存(&S)";
            barSaveAs.Caption = "另存为(&A)";

            barClose.Caption = "关闭(&C)";


            barSubPrint.Caption = "打印";
            barPrintBL.Caption = "打印提单";
            barPrintLoadGoods.Caption = "打印装货单";

            barReplyAgent.Caption = "申请代理(&R)";



            navBarBaseInfo.Caption = "基本信息";
            navBarBLInfo.Caption = "提单信息";
            navBarCargo.Caption = "货物信息";
            navBarIssueInfo.Caption = "签发信息";

            labMAWBNO.Text = "MAWB NO";
            labHBLNO.Text = "HAWB NO";
            labETD.Text = "起航日";
            labETA.Text = "到达日";
            labToBy1.Text = "中转站/承运人一";
            labToBy2.Text = "中转站/承运人二";
            labToBy3.Text = "中转站/承运人三";

            labDCLCarriage.Text = "运输声明价值";
            labDCLCustoms.Text = "海关声明价值";
            labOther.Text = "其他";
            labInsurance.Text = "保险金额";
            labReleaseType.Text = "放单类型";
            labReleaseDate.Text = "放单时间";
            labFlightNo.Text = "航班号";
            labAgentOfCarrier.Text = "承运人";
            labAirCompany.Text = "航空公司";
            labGoodsDescription.Text = "货物描述";
            labHandlingInfomation.Text = "操作信息";
            labPlaceOfDelivery.Text = "交货地";
            labShipperAccount.Text = "账号";
            labConsigneeAccountNo.Text = "账号";
            labAgentAccountNo.Text = "代理账号";
            labMeasurement.Text = "体积";
            //labTax.Text = "税款";        
            labTarifflevel.Text = "Rate Class";
            labNotifyParty.Text = "通知人";
            groupBox1.Text = "其他费用";
        }

        /// <summary>
        /// 设置一些控件的提示信息
        /// </summary>
        void SetNullValuePrompt()
        {
            Utility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtShipper,
                stxtConsignee,
                stxtAgentOfCarrier,
                stxtChecker,
                stxtIssuePlace,
                stxtNotifyParty ,
            });
            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtPlaceOfDelivery ,
                txtDepartureCode,
                txtDetinationCode,
            });

            Utility.SetTextEditNullValuePrompt(new List<TextEdit> { stxtRefNo }
                , LocalData.IsEnglish ? "Please Input Operation NO." : "请输入业务号.");


            string tip = LocalData.IsEnglish ? "Un Done" :
                        "此栏目链接于订舱单,保存时修改的内容将会更新到订舱单中.";

            txtDepartureCode.ToolTip = txtDetinationCode.ToolTip = txtPlaceOfDeliveryName.ToolTip = tip;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SmartPartClosing += new EventHandler<Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs>(MAWBEditPart_SmartPartClosing);
            this.ActivateSmartPartClosingEvent(this.Workitem);
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
        }

        #endregion

        #region 本地变量

        /// <summary>
        /// 主体数据
        /// </summary>
        AirMBLInfo _CurrentBLInfo = null;

        /// <summary>
        /// 是否需要申请代理
        /// </summary>
        bool _isNeedRequestAgent = false;

        /// <summary>
        ///是否保存了MAWB 
        /// </summary>
        bool isSave = false;
        /// <summary>
        /// 原始提单号
        /// </summary>
        string _originalMBLNO = string.Empty;

        const decimal KGS_TO_LBS_QUOTIETY = 2.2046M;

        #endregion

        #region 初始化的一些方法

        /// <summary>
        ///初始化Description对象
        /// </summary>
        private void InitCustomerDescriptionObject()
        {
            if (_CurrentBLInfo.ShipperDescription == null)
            {
                _CurrentBLInfo.ShipperDescription = new CustomerDescription();
            }
            if (_CurrentBLInfo.ConsigneeDescription == null)
            {
                _CurrentBLInfo.ConsigneeDescription = new CustomerDescription();
            }
            if (_CurrentBLInfo.NotifyPartyDescription == null)
            {
                _CurrentBLInfo.NotifyPartyDescription = new CustomerDescription();
            }
            if (_CurrentBLInfo.AgentDescription == null)
            {
                _CurrentBLInfo.AgentDescription = new CustomerDescription();
            }

            txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
            txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            barAdd.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Plus_16;
            barDelect.Glyph = ICP.Framework.ClientComponents.Resources.GlobalResource.Delete_16;
            RefreshToolBars();
            this.panelScroll.Click += delegate { panelScroll.Focus(); };

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ID)) _CurrentBLInfo.Marks = @"N/M";

            SetComboboxEnumSource();
            SetComboboxSource();
            RefreshControlsByData();
            SearchRegister();
            stxtRefNo.Focus();

            SetCurrencyList();
        }

        /// <summary>
        /// 根据数据源刷新控件
        /// </summary>
        private void RefreshControlsByData()
        {
            if (_CurrentBLInfo.IsNew)
            {
                #region New Init
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                if (configureInfo != null)
                {
                    _CurrentBLInfo.IssuePlaceID = configureInfo.IssuePlaceID ?? Guid.Empty;
                    _CurrentBLInfo.IssuePlaceName = configureInfo.IssuePlaceName;
                }
                stxtAgent.Enabled = false;
                #endregion
            }
            else
            {
                stxtRefNo.Properties.ReadOnly = true;
                stxtRefNo.Properties.Buttons[0].Enabled = false;

                stxtRefNo.Text = _CurrentBLInfo.RefNo;
                stxtRefNo.Tag = _CurrentBLInfo.AirBookingID;
            }

            if (_CurrentBLInfo.IsRequestAgent) stxtAgent.Enabled = false;
        }

        #region Combobox

        /// <summary>
        /// Enum 提单类型 放单类型
        /// </summary>
        void SetComboboxEnumSource()
        {
            //放单类型
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<FCMReleaseType>> releaseTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<FCMReleaseType>(LocalData.IsEnglish);
            foreach (var item in releaseTypes)
            {
                if (item.Value == 0) continue;
                cmbReleaseType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            //运价等级
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<RateClass>> rateClasss = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<RateClass>(LocalData.IsEnglish);
            foreach (var rateItem in rateClasss)
            {
                if (rateItem.Value == 0) continue;
                cmbTarifflevel.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(rateItem.Name, rateItem.Value));
            }
        }

        /// <summary>
        /// 签发人 付款方式 运输条款 包装 重量 体积 代理
        /// </summary>
        void SetComboboxSource()
        {
            this.mcmbAirCompany.ShowSelectedValue(this._CurrentBLInfo.AirCompanyID, this._CurrentBLInfo.AirCompanyName);
            Utility.SetEnterToExecuteOnec(mcmbAirCompany, delegate
            {
                ICPCommUIHelper.BindCustomerList(mcmbAirCompany, CustomerType.Airline);
            });

            if (_CurrentBLInfo.FilightNoID != null)
                cmbFlightNo.ShowSelectedValue(_CurrentBLInfo.FilightNoID, _CurrentBLInfo.FilightNo);
            this.cmbFlightNo.SelectedRow += new EventHandler(cmbFlightNo_SelectedRow);
            Utility.SetEnterToExecuteOnec(cmbFlightNo, delegate
            {
                List<FlightList> flightList = TransportFoundationService.GetFlightList(null, string.Empty, true, 0);
                if (flightList != null && flightList.Count > 0)
                {
                    Dictionary<string, string> col = new Dictionary<string, string>();
                    col.Add("No", "航班号");
                    col.Add("AirlineName", "航空公司");
                    cmbFlightNo.InitSource<FlightList>(flightList, col, "No", "ID");
                }
            });

            #region 签发人-- 属于操作口岸，并且角色为操作员的用户
            mcmbIssueBy.Enter += delegate
            {
                if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.CompanyID) == false &&
                    (mcmbIssueBy.DataSource == null || mcmbIssueBy.DataSource.Rows.Count == 0))
                {

                    List<UserList> saless = userService.GetUnderlingUserList(new Guid[] { _CurrentBLInfo.CompanyID }, null, new string[] { "操作员" }, true);
                    Dictionary<string, string> col = new Dictionary<string, string>();
                    col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
                    col.Add("Code", "代码");
                    mcmbIssueBy.InitSource<UserList>(saless, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
                }
            };
            #endregion

            #region 付款方式 运输条款 包装 重量 体积

            #region 付款方式
            if (_CurrentBLInfo.PaymentTermID != null)
                cmbPaymentTerm.ShowSelectedValue(_CurrentBLInfo.PaymentTermID, _CurrentBLInfo.PaymentTermName);
            Utility.SetEnterToExecuteOnec(cmbPaymentTerm, delegate
            {
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(cmbPaymentTerm, DataDictionaryType.PaymentTerm, DataBindType.EName);

                cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
                cmbPaymentTerm.SelectedIndexChanged += new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
            });

            if (_CurrentBLInfo.OtherPaymentTermID != null)
                cmbOther.ShowSelectedValue(_CurrentBLInfo.OtherPaymentTermID, _CurrentBLInfo.OtherPaymentTermName);
            Utility.SetEnterToExecuteOnec(cmbOther, delegate
            {
                ICPCommUIHelper.SetCmbDataDictionary(cmbOther, DataDictionaryType.PaymentTerm, DataBindType.EName);
            });

            #endregion

            #region 包装
            cmbQuantityUnit.ShowSelectedValue(_CurrentBLInfo.QuantityUnitID, _CurrentBLInfo.QuantityUnitName);
            Utility.SetEnterToExecuteOnec(cmbQuantityUnit, delegate
            {
                List<DataDictionaryList> packTypes = ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit);
            });
            #endregion

            ////#region 重量
            ////cmbWeightUnit.ShowSelectedValue(_CurrentBLInfo.WeightUnitID, _CurrentBLInfo.WeightUnitName);
            ////Utility.SetEnterToExecuteOnec(cmbWeightUnit, delegate
            ////{
            ////    List<DataDictionaryList> weightUnitsList = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit);
            ////});
            ////#endregion

            #region 体积
            cmbMeasurementUnit.ShowSelectedValue(_CurrentBLInfo.MeasurementUnitID, _CurrentBLInfo.MeasurementUnitName);
            Utility.SetEnterToExecuteOnec(cmbMeasurementUnit, delegate
            {
                List<DataDictionaryList> volUnitss = ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit);
            });
            #endregion

            #endregion

            #region Agent
            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.AgentID) == false)
            {
                List<CustomerList> agentCustomers = new List<CustomerList>();
                CustomerList agentCustomer = new CustomerList();
                agentCustomer.CName = agentCustomer.EName = _CurrentBLInfo.AgentName;
                agentCustomer.ID = _CurrentBLInfo.AgentID.Value;
                agentCustomers.Insert(0, agentCustomer);
                SetAgentSource(agentCustomers, false);
            }
            Utility.SetEnterToExecuteOnec(stxtAgent, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                foreach (CountryList c in _countryList)
                {
                    stxtAgent.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
                }

                SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);
                stxtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
                stxtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
            });
            #endregion

        }

        #endregion

        #region 搜索器
        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;
        /// <summary>
        /// 搜索器注册
        /// </summary>
        void SearchRegister()
        {
            #region RefNo

            //	选择范围
            //	数据从订单表[fcm..AirBookings]检索
            //	查询面板及栏位定义，请参照订舱单列表
            //	筛选范围：订舱单.状态不等于“已关单”
            dfService.Register(stxtRefNo, ICP.FCM.AirExport.ServiceInterface.Comm.FCMFinderConstants.AirBookingFinder, SearchFieldConstants.BookingNO, SearchFieldConstants.AirBookingResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid bookingID = new Guid(resultData[0].ToString());
                      if (_CurrentBLInfo.AirBookingID != bookingID)
                      {
                          AfterSearchRefNo(bookingID);
                      }
                      else
                      {
                          LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ?
                        "You has been selected this booking." : "您已选择了该票订舱单.");
                      }

                  },
                  delegate
                  {
                      if (_CurrentBLInfo.IsNew)
                      {
                          _CurrentBLInfo.AirBookingID = Guid.Empty;
                          stxtRefNo.Text = _CurrentBLInfo.RefNo = string.Empty;
                      }

                  }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region Customer

            #region SCNA

            //shipper
            Utility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                //shipper
                CustomerFinderBridge shipperBridge = new CustomerFinderBridge(
                this.stxtShipper,
                _countryList,
                this.dfService,
                this.customerService,
                _CurrentBLInfo.ShipperDescription,
                this.txtShipperDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                shipperBridge.Init();
            });

            stxtShipper.OnOk += new EventHandler(stxtShipper_OnOk);

            //Consignee
            Utility.SetEnterToExecuteOnec(stxtConsignee, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                CustomerFinderBridge consigneeBridge = new CustomerFinderBridge(
                this.stxtConsignee,
                _countryList,
                this.dfService,
                this.customerService,
                _CurrentBLInfo.ConsigneeDescription,
                this.txtConsigneeDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                consigneeBridge.Init();
            });

            stxtConsignee.OnOk += new EventHandler(stxtConsignee_OnOk);

            //NotifyParty
            Utility.SetEnterToExecuteOnec(stxtNotifyParty, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                CustomerFinderBridge notifyPartyBridge = new CustomerFinderBridge(
                 this.stxtNotifyParty,
                 _countryList,
                 this.dfService,
                 this.customerService,
                 _CurrentBLInfo.NotifyPartyDescription,
                 null,
                 ICPCommUIHelper,
                 LocalData.IsEnglish
                 , "SAME AS CONSIGNEE");
                notifyPartyBridge.Init();

            });

            stxtNotifyParty.OnOk += new EventHandler(stxtNotifyParty_OnOk);

            #endregion

            dfService.Register(stxtChecker, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
              delegate(object inputSource, object[] resultData)
              {
                  stxtChecker.Text = _CurrentBLInfo.CheckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                  stxtChecker.Tag = _CurrentBLInfo.CheckerID = new Guid(resultData[0].ToString());

              }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);


            dfService.Register(stxtAgentOfCarrier, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
               delegate(object inputSource, object[] resultData)
               {
                   stxtAgentOfCarrier.Text = _CurrentBLInfo.AgentOfCarrierName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                   stxtAgentOfCarrier.Tag = _CurrentBLInfo.AgentOfCarrierID = new Guid(resultData[0].ToString());
               },
               delegate()
               {
                   stxtAgentOfCarrier.Text = string.Empty;
                   stxtAgentOfCarrier.Tag = string.Empty;

               }
               , ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion

            #region Port

            //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
            //大船 筛选：装货港=当前装货港and卸货港=当前卸货港

            #region POL
            dfService.Register(txtDepartureCode, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      if (_CurrentBLInfo.DepartureID != portID)
                      {
                          txtDepartureName.Text = _CurrentBLInfo.DepartureName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          txtDepartureCode.Text = _CurrentBLInfo.DepartureCode = resultData[1].ToString();
                          txtDepartureCode.Tag = _CurrentBLInfo.DepartureID = portID;
                      }
                  },
                  delegate
                  {
                      txtDepartureCode.Tag = _CurrentBLInfo.DepartureID = Guid.Empty;
                      txtDepartureCode.Text = _CurrentBLInfo.DepartureCode = string.Empty;
                      txtDepartureName.Text = _CurrentBLInfo.DepartureName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region POD
            dfService.Register(txtDetinationCode, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      if (_CurrentBLInfo.DetinationID != portID)
                      {
                          txtDetinationName.Text = _CurrentBLInfo.DetinationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          txtDetinationCode.Text = _CurrentBLInfo.DetinationCode = resultData[1].ToString();
                          txtDetinationCode.Tag = _CurrentBLInfo.DetinationID = portID;
                      }

                  },
                  delegate
                  {
                      txtDetinationCode.Tag = _CurrentBLInfo.DetinationID = Guid.Empty;
                      txtDetinationCode.Text = _CurrentBLInfo.DetinationCode = string.Empty;
                      txtDetinationName.Text = _CurrentBLInfo.DetinationName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region PlaceOfDelivery
            dfService.Register(stxtPlaceOfDelivery, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      txtPlaceOfDeliveryName.Text = _CurrentBLInfo.PlaceOfDeliveryName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                      stxtPlaceOfDelivery.Text = _CurrentBLInfo.PlaceOfDeliveryCode = resultData[1].ToString();
                      stxtPlaceOfDelivery.Tag = _CurrentBLInfo.PlaceOfDeliveryID = new Guid(resultData[0].ToString());

                      PlaceOfDeliveryChanged();

                  },
                  delegate
                  {
                      stxtPlaceOfDelivery.Tag = _CurrentBLInfo.PlaceOfDeliveryID = null;
                      stxtPlaceOfDelivery.Text = _CurrentBLInfo.PlaceOfDeliveryCode = string.Empty;
                      txtPlaceOfDeliveryName.Text = _CurrentBLInfo.PlaceOfDeliveryName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion

            #region IssuePlace
            dfService.Register(stxtIssuePlace, CommonFinderConstants.AirLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    stxtIssuePlace.Text = _CurrentBLInfo.IssuePlaceName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtIssuePlace.Tag = _CurrentBLInfo.IssuePlaceID = new Guid(resultData[0].ToString());
                }, Guid.Empty, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion

            #endregion
        }

        void stxtNotifyParty_OnOk(object sender, EventArgs e)
        {
            if (stxtNotifyParty.CustomerDescription != null)
            {
                _CurrentBLInfo.NotifyPartyDescription = stxtNotifyParty.CustomerDescription;
            }
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            if (stxtConsignee.CustomerDescription != null)
            {
                _CurrentBLInfo.ConsigneeDescription = stxtConsignee.CustomerDescription;
            }
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            if (stxtShipper.CustomerDescription != null)
            {
                _CurrentBLInfo.ShipperDescription = stxtShipper.CustomerDescription;
            }
        }

        #endregion

        #endregion

        #region 控件或数据的联动操作

        private void mainGridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        #region 其它费用的新增和删除

        /*添加其它费用*/
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EndEdit();
            OtherChargeItem newData = new OtherChargeItem();
            OtherChargesBindingSource.Insert(0, newData);
            OtherChargesBindingSource.MoveFirst();
            RefreshToolBars();
            if (!_CurrentBLInfo.IsDirty)
            {
                _CurrentBLInfo.IsDirty = true;
            }
        }

        /*删除其它费用*/
        private void barDelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Are you sure to delete?" : "确认删除当前费用?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlg != DialogResult.OK)
            {
                return;
            }

            OtherChargesBindingSource.EndEdit();
            try
            {
                if (OtherChargesBindingSource.Current != null)
                {
                    OtherChargesBindingSource.RemoveCurrent();
                    OtherChargesBindingSource.MoveFirst();
                }

                RefreshToolBars();
                if (!_CurrentBLInfo.IsDirty)
                {
                    _CurrentBLInfo.IsDirty = true;
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        /*刷新工具栏*/
        private void RefreshToolBars()
        {
            if (OtherChargesBindingSource.Current == null)
            {
                barDelect.Enabled = false;
            }
            else
            {
                barDelect.Enabled = true;
            }
        }

        #endregion

        #region 当公司更变时需 设置Agent数据源
        /// <summary>
        /// 设置Agent数据源
        /// </summary>
        private void SetAgentSourceByCompanyID(Guid companyID)
        {
            stxtAgent.DataSource = null;
            if (Utility.GuidIsNullOrEmpty(companyID) || _CurrentBLInfo.IsRequestAgent)
            {
                stxtAgent.Enabled = false;
                return;
            }

            List<CustomerList> agentCustomers = configureService.GetCompanyAgentList(_CurrentBLInfo.CompanyID, true);
            CustomerList emptyCustomer = new CustomerList();
            emptyCustomer.CName = emptyCustomer.EName = string.Empty;
            emptyCustomer.ID = Guid.Empty;
            agentCustomers.Insert(0, emptyCustomer);
            SetAgentSource(agentCustomers, true);
        }
        private void SetAgentSource(List<CustomerList> agentCustomers, bool isBulidNewDescription)
        {
            stxtAgent.SetLanguage(LocalData.IsEnglish);
            stxtAgent.DataSource = agentCustomers;
            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.AgentID))
            {
                stxtAgent.EditValue = _CurrentBLInfo.AgentID = agentCustomers[0].ID;
            }

            if (_CurrentBLInfo.IsRequestAgent == false) stxtAgent.Enabled = true;

            if (isBulidNewDescription)
                BulidAgentDescriqitonByID(_CurrentBLInfo.AgentID);
            else
            {
                stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription;
            }
            stxtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
            stxtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);

            stxtAgent.OnOk -= new EventHandler(stxtAgent_OnOk);
            stxtAgent.OnOk += new EventHandler(stxtAgent_OnOk);

        }

        void stxtAgent_OnOk(object sender, EventArgs e)
        {
            if (_CurrentBLInfo.AgentDescription == null) return;
        }
        void stxtAgent_EditValueChanged(object sender, EventArgs e)
        {
            if (stxtAgent.EditValue != null && stxtAgent.EditValue.ToString().Length > 0)
            {
                Guid id = new Guid(stxtAgent.EditValue.ToString());
                BulidAgentDescriqitonByID(id);
            }
        }
        /// <summary>
        /// 根据ID生成代理的描述和把描述填充到描述框
        /// </summary>
        private void BulidAgentDescriqitonByID(Guid? id)
        {
            if (Utility.GuidIsNullOrEmpty(id))
            {
                stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription = new CustomerDescription();
            }
            else
            {
                ICPCommUIHelper.SetCustomerDesByID(id, _CurrentBLInfo.AgentDescription);
                stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription;
            }
        }
        #endregion

        #region 搜索业务号后把Booking的数据填充到当前页面
        /// <summary>
        /// 搜索业务号后把Booking的数据填充到当前页面
        /// </summary>
        private void AfterSearchRefNo(Guid bookingID)
        {
            #region 	如果之前已选择业务号
            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.AirBookingID) == false && _CurrentBLInfo.AirBookingID != bookingID)
            {
                ///	否则提示：“是否重新导入发货人、收货人、通知人、地点、货物信息？”，如果选择是，则继续执行下一步，否则退出。
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否重新导入发货人、收货人、通知人、地点、货物信息?"
                                 , LocalData.IsEnglish ? "Tip" : "提示"
                                 , MessageBoxButtons.YesNo
                                 , MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

            }
            #endregion
            #region 填充
            AirBookingInfo bookingInfo = aeService.GetAirBookingInfo(bookingID);
            _CurrentBLInfo.CustomerName = bookingInfo.CustomerName;

            _CurrentBLInfo.CompanyID = bookingInfo.CompanyID;
            SetCurrencyList();
            stxtRefNo.Text = _CurrentBLInfo.RefNo = bookingInfo.No;
            stxtRefNo.Tag = _CurrentBLInfo.AirBookingID = bookingInfo.ID;

            _CurrentBLInfo.AgentOfCarrierName = bookingInfo.AgentOfCarrierName;
            _CurrentBLInfo.AgentOfCarrierID = bookingInfo.AgentOfCarrierID;
            _CurrentBLInfo.FilightNoID = bookingInfo.FilightId;
            _CurrentBLInfo.FilightNo = bookingInfo.FilightNo;
            cmbFlightNo.ShowSelectedValue(_CurrentBLInfo.FilightNoID, _CurrentBLInfo.FilightNo);

            if (bookingInfo.AirCompanyId != null)
            {
                _CurrentBLInfo.AirCompanyID = bookingInfo.AirCompanyId.Value;
                _CurrentBLInfo.AirCompanyName = bookingInfo.AirCompanyName;
            }

            this.mcmbAirCompany.ShowSelectedValue(this._CurrentBLInfo.AirCompanyID, this._CurrentBLInfo.AirCompanyName);

            _CurrentBLInfo.SalesName = bookingInfo.SalesName;
            _CurrentBLInfo.FilerName = bookingInfo.FilerName;
            _CurrentBLInfo.BookingerName = bookingInfo.BookingerName;
         
            _CurrentBLInfo.ETD = bookingInfo.ETD;        
            _CurrentBLInfo.ETA = bookingInfo.ETA;       

            #region 收发通,代理

            #region Agent  MBL.代理 = 订舱单.代理

            //SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);

            _CurrentBLInfo.AgentID = bookingInfo.AgentID;
            _CurrentBLInfo.AgentName = bookingInfo.AgentName;
            _CurrentBLInfo.AgentDescription = bookingInfo.AgentDescription;
            SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);

            #endregion

            #region Shipper  MBL.发货人 = 订舱单.发货人

            //只出MBL
            //	MBL.发货人 = 订舱单.发货人,MBL.发货人描述 = 根据MBL.发货人生成
            //	MBL.收货人 = 订舱单.收货人,MBL.收货人描述 = 根据MBL.收货人生成
            if (bookingInfo.IsOnlyMBL)
            {
                #region 发货人
                _CurrentBLInfo.ShipperID = bookingInfo.ShipperID;
                _CurrentBLInfo.ShipperName = bookingInfo.ShipperName;
                stxtShipper.CustomerDescription = _CurrentBLInfo.ShipperDescription = bookingInfo.ShipperDescription;
                if (_CurrentBLInfo.ShipperDescription != null)
                    txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
                #endregion

                #region 收货人
                _CurrentBLInfo.ConsigneeID = bookingInfo.ConsigneeID;
                _CurrentBLInfo.ConsigneeName = bookingInfo.ConsigneeName;
                stxtConsignee.CustomerDescription = _CurrentBLInfo.ConsigneeDescription = bookingInfo.ConsigneeDescription;
                if (_CurrentBLInfo.ConsigneeDescription != null)
                    txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);
                #endregion
            }
            //	如果该票订舱单.[只出MBL] = false，则导入如下信息到MBL中：
            //	MBL.发货人 = 订舱单.操作口岸.关联的客户 (参照公司配置)，MBL.发货人描述 = 根据MBL.发货人生成
            //	MBL.收货人 = 订舱单.代理，MBL.收货人描述 = 根据MBL.收货人生成
            else
            {
                #region 发货人
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(bookingInfo.CompanyID);

                _CurrentBLInfo.ShipperID = configureInfo.CustomerID;
                _CurrentBLInfo.ShipperName = configureInfo.CustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentBLInfo.ShipperID, _CurrentBLInfo.ShipperDescription);
                if (_CurrentBLInfo.ShipperDescription != null)
                    txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
                #endregion

                #region 收货人
                _CurrentBLInfo.ConsigneeID = bookingInfo.AgentID;
                _CurrentBLInfo.ConsigneeName = bookingInfo.AgentName;
                stxtConsignee.CustomerDescription = _CurrentBLInfo.ConsigneeDescription = bookingInfo.AgentDescription;
                if (_CurrentBLInfo.ConsigneeDescription != null)
                    txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);
                #endregion

            }

            #endregion

            #region NotifyParty = 通知人描述 = “SAME AS CONSIGNEE”

            _CurrentBLInfo.NotifyPartyID = Guid.Empty;
            _CurrentBLInfo.NotifyPartyName = string.Empty;
            stxtNotifyParty.CustomerDescription = _CurrentBLInfo.NotifyPartyDescription = new CustomerDescription();
            //txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";

            #endregion

            #endregion

            #region Port

            //	MBL.装货港描述 = 订舱单. 装货港.名称 
            //	MBL.卸货港描述 = 订舱单. 卸货港.名称 
            //	MBL.交货地描述 = 订舱单. 交货地.名称 
            _CurrentBLInfo.PlaceOfDeliveryID = bookingInfo.PlaceOfDeliveryID;
            _CurrentBLInfo.PlaceOfDeliveryCode = bookingInfo.PlaceOfDeliveryName;
            _CurrentBLInfo.PlaceOfDeliveryName = bookingInfo.PlaceOfDeliveryName;

            _CurrentBLInfo.DepartureID = bookingInfo.POLID;
            _CurrentBLInfo.DepartureCode = bookingInfo.DepartureName;
            _CurrentBLInfo.DepartureName = bookingInfo.DepartureName;

            _CurrentBLInfo.DetinationID = bookingInfo.PODID;
            _CurrentBLInfo.DetinationCode = bookingInfo.DetinationName;
            _CurrentBLInfo.DetinationName = bookingInfo.DetinationName;

            #endregion

            #region Voyage, PreVoyage, ETD, ETA

            //if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false)
            //{
            //    VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.VoyageID.Value);
            //    _CurrentBLInfo.TranshipmentPortName = voyageInfo.TranshipmentPortName;
            //}


            if (bookingInfo.MBLReleaseType != null && bookingInfo.MBLReleaseType.Value != FCMReleaseType.Unknown)
                _CurrentBLInfo.ReleaseType = bookingInfo.MBLReleaseType.Value;

            #endregion

            #region 付款方式,运输条款,数量,重量,体积
            //	MBL.付款方式 = 订舱单. MBL付款方式
            //	MBL.运输条款 = 订舱单. 运输条款
            //	MBL.数量 = 订舱单. 数量
            //	MBL.数量单位 = 订舱单.数量单位
            //	MBL.重量 = 订舱单. 重量
            //	MBL.重量单位 = 订舱单. 重量单位
            //	MBL.体积 = 订舱单.体积
            //	MBL.体积单位 = 订舱单.体积单位      

            cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
            _CurrentBLInfo.BookingPaymentTermID = _CurrentBLInfo.PaymentTermID = bookingInfo.MBLPaymentTermID;
            cmbPaymentTerm.Text = _CurrentBLInfo.PaymentTermName = bookingInfo.MBLPaymentTermName;
            cmbPaymentTerm.ShowSelectedValue(_CurrentBLInfo.PaymentTermID, _CurrentBLInfo.PaymentTermName);

            if (string.IsNullOrEmpty(_CurrentBLInfo.PaymentTermName) == false)
            {
                if (_CurrentBLInfo.PaymentTermName == "CC" || _CurrentBLInfo.PaymentTermName == "到付")
                    txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = "FREIGHT COLLECT";
                else
                    txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = "FREIGHT PREPAID";
            }
            else
                txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = string.Empty;

            cmbPaymentTerm.SelectedIndexChanged += new EventHandler(cmbPaymentTerm_SelectedIndexChanged);

            _CurrentBLInfo.Quantity = bookingInfo.Quantity;
            if (Utility.GuidIsNullOrEmpty(bookingInfo.QuantityUnitID) == false)
            {
                cmbQuantityUnit.Text = _CurrentBLInfo.QuantityUnitName = bookingInfo.QuantityUnitName;
                _CurrentBLInfo.QuantityUnitID = bookingInfo.QuantityUnitID.Value;

                cmbQuantityUnit.ShowSelectedValue(_CurrentBLInfo.QuantityUnitID, _CurrentBLInfo.QuantityUnitName);

            }
            //_CurrentBLInfo.Weight = bookingInfo.Weight;
            if (Utility.GuidIsNullOrEmpty(bookingInfo.WeightUnitID) == false)
            {
                if (bookingInfo.WeightUnitName.ToUpper() == "KGS")
                {
                    _CurrentBLInfo.GrossKGS = bookingInfo.Weight;
                    _CurrentBLInfo.GrossLBS = bookingInfo.Weight * 2.20462M;
                }
                else
                {
                    _CurrentBLInfo.GrossLBS = bookingInfo.Weight;
                    _CurrentBLInfo.GrossKGS = bookingInfo.Weight / 2.20462M;
                }

            }

            _CurrentBLInfo.Measurement = bookingInfo.Measurement;
            if (Utility.GuidIsNullOrEmpty(bookingInfo.MeasurementUnitID) == false)
            {
                cmbMeasurementUnit.Text = _CurrentBLInfo.MeasurementUnitName = bookingInfo.MeasurementUnitName;
                _CurrentBLInfo.MeasurementUnitID = bookingInfo.MeasurementUnitID.Value;
                cmbMeasurementUnit.ShowSelectedValue(_CurrentBLInfo.MeasurementUnitID, _CurrentBLInfo.MeasurementUnitName);
            }
            #endregion

            //RefreshEnabledByBookingType(bookingInfo.AEOperationType);

            bindingSource1.EndEdit();
            #endregion
        }

        #endregion

        #region FilightNo Changed

        private void cmbFlightNo_SelectedRow(object sender, EventArgs e)
        {
            if (cmbFlightNo.EditValue != null && cmbFlightNo.EditValue.ToString().Length > 0)
            {
                FlightInfo flightInfo = TransportFoundationService.GetFilghtInfo(new Guid(cmbFlightNo.EditValue.ToString()));

                if (mcmbAirCompany.EditValue != null && mcmbAirCompany.EditValue.ToString().Length > 0)
                {
                    if ((Guid)mcmbAirCompany.EditValue != flightInfo.AirlineID)
                    {
                        DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show("是否替换原有航空公司?",
                                                            "提示",
                                                           MessageBoxButtons.YesNo,
                                                           MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            _CurrentBLInfo.AirCompanyID = flightInfo.AirlineID;
                            _CurrentBLInfo.AirCompanyName = flightInfo.AirlineName;
                            this.mcmbAirCompany.ShowSelectedValue(this._CurrentBLInfo.AirCompanyID, this._CurrentBLInfo.AirCompanyName);
                        }
                    }
                }
                else
                {
                    _CurrentBLInfo.AirCompanyID = flightInfo.AirlineID;
                    _CurrentBLInfo.AirCompanyName = flightInfo.AirlineName;
                    this.mcmbAirCompany.ShowSelectedValue(this._CurrentBLInfo.AirCompanyID, this._CurrentBLInfo.AirCompanyName);
                }
            }

            if (!_CurrentBLInfo.IsDirty)
            {
                _CurrentBLInfo.IsDirty = true;
            }
        }
      
        #endregion

        private void SetCurrencyList()
        {
            if (!Utility.GuidIsNullOrEmpty(_CurrentBLInfo.CompanyID))
            {
                List<SolutionCurrencyList> currencyList = new List<SolutionCurrencyList>();

                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(_CurrentBLInfo.CompanyID);
                //找到解决方案
                if (configureInfo != null)
                {
                    currencyList = configureService.GetSolutionCurrencyList(configureInfo.SolutionID, true);
                }
                else
                {
                    return;
                }

                foreach (SolutionCurrencyList currency in currencyList)
                {
                    cmbBuyCur.Properties.Items.Add(new ImageComboBoxItem(currency.CurrencyName, currency.CurrencyID));
                    cmbIATACur.Properties.Items.Add(new ImageComboBoxItem(currency.CurrencyName, currency.CurrencyID));
                }
            }
        }

        /// <summary>
        /// 如果交货地所在的国家不存在于公司配置客户对应的国家并且代理已存在，需要提示用户是否清空代理并重新申请代理
        /// </summary>
        void PlaceOfDeliveryChanged()
        {
            _isNeedRequestAgent = false;

            //如果交货地所在的国家不存在于公司配置客户对应的国家并且代理已存在，需要提示用户是否清空代理并重新申请代理
            if (_CurrentBLInfo.IsRequestAgent == false
                && Utility.GuidIsNullOrEmpty(_CurrentBLInfo.AgentID) == false
                && _CurrentBLInfo.IsNew == false)
            {
                try
                {
                    bool isExist = aeService.IsPortCountryExistCompanyConfig(_CurrentBLInfo.PlaceOfDeliveryID.Value, _CurrentBLInfo.CompanyID);
                    if (isExist == false)
                    {
                        if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current BL need request agent.is clear current BL\'s agent?"
                                                                  : "当前提单需要申请代理,是否清空代理?"
                           , LocalData.IsEnglish ? "Tip" : "提示"
                           , MessageBoxButtons.YesNo
                           , MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _isNeedRequestAgent = true;
                            stxtAgent.Enabled = false;
                            stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription = new CustomerDescription();
                            stxtAgent.EditValue = _CurrentBLInfo.AgentID = Guid.Empty;
                            _CurrentBLInfo.AgentName = string.Empty;
                        }
                    }
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
            }
        }

        #region  付款方式
        void cmbPaymentTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.BookingPaymentTermID) == false && _CurrentBLInfo.BookingPaymentTermID != _CurrentBLInfo.PaymentTermID)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "现选择的MBL付款方式与订舱单中MBL付款方式不同,是否继续?",
                                             LocalData.IsEnglish ? "Tip" : "提示",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question) == DialogResult.No)
                {
                    cmbPaymentTerm.EditValue = _CurrentBLInfo.PaymentTermID = _CurrentBLInfo.BookingPaymentTermID;
                }
            }

            //_CurrentBLInfo.BookingPaymentTermID = _CurrentBLInfo.PaymentTermID;

            if (cmbPaymentTerm.Text == "CC" || cmbPaymentTerm.Text == "到付")
                txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = "FREIGHT COLLECT";
            else
                txtFreightDescription.Text = _CurrentBLInfo.FreightDescription = "FREIGHT PREPAID";


            cmbPaymentTerm.SelectedIndexChanged += new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
        }

        #endregion

        #endregion

        #region 刷新可操作性

        /// <summary>
        /// 刷新按钮可用性
        /// </summary>
        void RefreshBarEnabled()
        {
            if (_CurrentBLInfo.IsNew)
            {
                barSubCheck.Enabled = barE_MBL.Enabled =
                  barPrintBL.Enabled = barReplyAgent.Enabled = barSaveAs.Enabled = barBill.Enabled = false;
            }
            else
            {
                barPrintBL.Enabled = barRefresh.Enabled = barSaveAs.Enabled = barReplyAgent.Enabled = barE_MBL.Enabled = barBill.Enabled = true;

                if (string.IsNullOrEmpty(_CurrentBLInfo.HAWBNos))
                {
                    if (_CurrentBLInfo.State != AEBLState.Release && _CurrentBLInfo.State != AEBLState.Checked)
                    {
                        barSubCheck.Enabled = true;
                    }
                }
                else
                    barSubCheck.Enabled = false;
            }
        }

        #endregion

        #region 保存

        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                Save();
            }
        }

        private bool Save()
        {
            if (ValidateData(false) == false) return false;
            try
            {
                if (_CurrentBLInfo.IsDirty == false && _CurrentBLInfo.IsNew == false) return false;

                barSave.Enabled = false;

                AutoRequestAgent();

                SaveMBLInfoParameter mbl = ConvertMBLToParameter(false, _CurrentBLInfo);
                SingleResult result = aeService.SaveAirMBLInfo(mbl);
                _CurrentBLInfo.MBLID = _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                AfterSave(false);
                isSave = true;

                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
            finally { barSave.Enabled = true; }
        }

        /// <summary>
        /// 自动申请代理
        /// </summary>
        private void AutoRequestAgent()
        {
            //if (_CurrentBLInfo.IsNew == false && _isNeedRequestAgent)
            //{
            //    SingleResult result = fcmCommonService.RequestAirAgent(_CurrentBLInfo.AirBookingID
            //                                                            , ICP.Framework.CommonLibrary.Client.OperationType.AirExport
            //                                                            , LocalData.UserInfo.LoginID
            //                                                            , DateTime.Now
            //                                                            , AgentType.Normal
            //                                                            , string.Empty, null);
            //    _isNeedRequestAgent = false;
            //    _CurrentBLInfo.IsRequestAgent = true;
            //    stxtAgent.Enabled = false;
            //}
        }
        void AfterSave(bool IsSaveAs)
        {
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();

            if (_CurrentBLInfo.ShipperDescription != null)
            {
                _CurrentBLInfo.ShipperDescription.IsDirty = false;
            }
            if (_CurrentBLInfo.ConsigneeDescription != null)
            {
                _CurrentBLInfo.ConsigneeDescription.IsDirty = false;
            }
            if (_CurrentBLInfo.NotifyPartyDescription != null)
            {
                _CurrentBLInfo.NotifyPartyDescription.IsDirty = false;
            }
            if (_CurrentBLInfo.AgentDescription != null)
            {
                _CurrentBLInfo.AgentDescription.IsDirty = false;
            }

            stxtRefNo.Properties.ReadOnly = true;
            stxtRefNo.Properties.Buttons[0].Enabled = false;
            this.Title = LocalData.IsEnglish ? "Edit MAWB " + _CurrentBLInfo.No : "编辑MAWB " + _CurrentBLInfo.No;
            RefreshBarEnabled();
            if (Saved != null) Saved(new object[] { _CurrentBLInfo });
            if (IsSaveAs)
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save as a new bl successfully." : "已成功另存为一票新的提单.");
            else
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }

        private bool ValidateData(bool IsSaveAs)
        {
            this.EndEdit();

            bool isScrr = true;
            //string errorInfo = string.Empty;
            if (_CurrentBLInfo.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (_CurrentBLInfo.DepartureID != Guid.Empty && _CurrentBLInfo.DepartureID == _CurrentBLInfo.DetinationID)
                            e.SetErrorInfo("DepartureID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");

                        //if (_CurrentBLInfo.FilightNoID == null || _CurrentBLInfo.FilightNoID== Guid.Empty)
                        //    e.SetErrorInfo("FlightNo", LocalData.IsEnglish ? "Flight No Must Input" : "航班号必须填写");
                        if (_CurrentBLInfo.ETA != null && _CurrentBLInfo.ETD != null
                           && _CurrentBLInfo.ETD > _CurrentBLInfo.ETA)
                            e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETD can't bigger ETA." : "ETD不能大于ETA.");

                        if (string.IsNullOrEmpty(_CurrentBLInfo.No))
                            e.SetErrorInfo("No", LocalData.IsEnglish ? "MAWB NO Must Input" : "MAWB NO必须填写");
                        else if (IsSaveAs && string.IsNullOrEmpty(_originalMBLNO) == false && _CurrentBLInfo.No.Trim() == _originalMBLNO.Trim())
                            e.SetErrorInfo("No", LocalData.IsEnglish ? "Input a new MAWB NO please." : "请输入一个新的MAWB NO.");

                    }
                ) == false) isScrr = false;

            return isScrr;
        }

        #endregion

        #region 另存为

        private void barSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                SaveAs();
            }
        }

        bool SaveAs()
        {
            if (ValidateData(true) == false) return false;

            if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的提单?",
                                        LocalData.IsEnglish ? "Tip" : "提示",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            try
            {
                SaveMBLInfoParameter mbl = ConvertMBLToParameter(true, _CurrentBLInfo);
                SingleResult result = aeService.SaveAirMBLInfo(mbl);
                _CurrentBLInfo.MBLID = _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                AfterSave(true);

                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
        }

        #endregion

        #region 关闭

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        void MAWBEditPart_SmartPartClosing(object sender, Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs e)
        {
            if (_CurrentBLInfo.IsDirty && isSave == false)
            {
                DialogResult result = PartLoader.EnquireIsSaveCurrentDataByUpdated();
                if (result == DialogResult.Yes)
                {
                    if (this.SaveData() == false) e.Cancel = true;
                }
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        #endregion

        #region 账单

        private void barBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentBLInfo == null || _CurrentBLInfo.ID == Guid.Empty) return;
                if (_CurrentBLInfo.IsDirty)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                    return;
                }

                if (_CurrentBLInfo.MBLID == null || _CurrentBLInfo.MBLID == Guid.Empty)
                {
                    Utility.ShowMessage(NativeLanguageService.GetText(this, "11091600001"));
                    return;
                }

                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(_CurrentBLInfo.AirBookingID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = _CurrentBLInfo.MBLID;
                    finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                }
                else
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }

            }
        }

        #endregion

        #region 打印

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.IsDirty)
                {
                    if (SaveData() == false) return;
                }

                Dictionary<string, object> stateValues = new Dictionary<string, object>();
                stateValues.Add("AirBLList", _CurrentBLInfo);
                string no = _CurrentBLInfo.No.Length <= 4 ? _CurrentBLInfo.No : _CurrentBLInfo.No.Substring(_CurrentBLInfo.No.Length - 4, 4);
                string title = (LocalData.IsEnglish ? "Print BL" : "打印提单") + ("-" + no);
                PartLoader.ShowEditPart<NewBLPrintPart>(Workitem, null, stateValues, title, null, null);
            }
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_CurrentBLInfo == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = _CurrentBLInfo.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = _CurrentBLInfo.ID;

            return message;
        }
        #endregion

        #region barItem Click 事件

        #region 对单

        //private void barCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.State != BLState.Draft) return;

        //    try
        //    {
        //        SingleResult result = aeService.ChangeAirMBLState(_CurrentBLInfo.ID, BLState.Checking, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);

        //        bool isDirty = _CurrentBLInfo.IsDirty;
        //        _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
        //        _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
        //        _CurrentBLInfo.State = BLState.Checking;
        //        _CurrentBLInfo.IsDirty = isDirty;
        //        RefreshBarEnabled();
        //        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Check Successfully" : "设置对单成功.");
        //        if (Saved != null) Saved(new object[] { _CurrentBLInfo });
        //    }
        //    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
        //    RefreshBarEnabled();
        //}

        private void barSubCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty) return;

            try
            {
                SingleResult result = aeService.ChangeAirMBLState(_CurrentBLInfo.ID, AEBLState.Checked, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);

                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _CurrentBLInfo.State = AEBLState.Checking;
                _CurrentBLInfo.IsDirty = isDirty;
                RefreshBarEnabled();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Checked Successfully" : "设置完成对单成功.");
                if (Saved != null) Saved(new object[] { _CurrentBLInfo });
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }

            RefreshBarEnabled();
        }

        #endregion

        private void barReplyAgent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool srcc = fcmCommonClientService.OpenAgentRequestPart(_CurrentBLInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.AirExport, this.Workitem);
            if (srcc)
            {
                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.IsRequestAgent = true;
                stxtAgent.Enabled = false;
                _CurrentBLInfo.IsDirty = isDirty;
            }
        }

        private void barE_MBL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                RefreshBLData();
            }
        }

        private void RefreshBLData()
        {
            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ID)) return;

            //DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Sure Refresh Data?" : "是否刷新数据?",
            //                                LocalData.IsEnglish ? "Tip" : "提示",
            //                                MessageBoxButtons.YesNo,
            //                                MessageBoxIcon.Question);
            //if (dialogResult == DialogResult.Yes)
            //{
            this.Focus();
            _CurrentBLInfo = aeService.GetAirMBLInfo(_CurrentBLInfo.ID);
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
            this.bindingSource1.DataSource = _CurrentBLInfo;
            bindingSource1.ResetBindings(false);
            InitCustomerDescriptionObject();
            this.Refresh();
            //}
        }

        private void textboxEventControl()
        {
            this.txtGKGS.Enter += delegate(object sender, EventArgs e) { this.txtGKGS.TextChanged += new EventHandler(txtGKGS_TextChanged); };
            this.txtGLBS.Enter += delegate(object sender, EventArgs e) { this.txtGLBS.TextChanged += new EventHandler(txtGLBS_TextChanged); };
            this.txtChKGS.Enter += delegate(object sender, EventArgs e) { this.txtChKGS.TextChanged += new EventHandler(txtChKGS_TextChanged); };
            this.txtChLBS.Enter += delegate(object sender, EventArgs e) { this.txtChLBS.TextChanged += new EventHandler(txtChLBS_TextChanged); };

            this.txtGKGS.Leave += delegate(object sender, EventArgs e) { this.txtGKGS.TextChanged -= new EventHandler(txtGKGS_TextChanged); };
            this.txtGLBS.Leave += delegate(object sender, EventArgs e) { this.txtGLBS.TextChanged -= new EventHandler(txtGLBS_TextChanged); };
            this.txtChKGS.Leave += delegate(object sender, EventArgs e) { this.txtChKGS.TextChanged -= new EventHandler(txtChKGS_TextChanged); };
            this.txtChLBS.Leave += delegate(object sender, EventArgs e) { this.txtChLBS.TextChanged -= new EventHandler(txtChLBS_TextChanged); };

            this.txtBuyPrice.Enter += delegate(object sender, EventArgs e) { this.txtBuyPrice.TextChanged += new EventHandler(txtBuyPrice_TextChanged); };
            this.radioGroup1.Enter += delegate(object sender, EventArgs e) { this.radioGroup1.EditValueChanged += new EventHandler(txtBuyPrice_TextChanged); };
            this.txtIATAPrice.Enter += delegate(object sender, EventArgs e) { this.txtIATAPrice.TextChanged += new EventHandler(txtIATABuyPrice_TextChanged); };
            this.radioGroup2.Enter += delegate(object sender, EventArgs e) { this.radioGroup2.EditValueChanged += new EventHandler(txtIATABuyPrice_TextChanged); };

            this.txtBuyPrice.Leave += delegate(object sender, EventArgs e) { this.txtBuyPrice.TextChanged -= new EventHandler(txtBuyPrice_TextChanged); };
            this.radioGroup1.Leave += delegate(object sender, EventArgs e) { this.radioGroup1.EditValueChanged -= new EventHandler(txtBuyPrice_TextChanged); };
            this.txtIATAPrice.Leave += delegate(object sender, EventArgs e) { this.txtIATAPrice.TextChanged -= new EventHandler(txtIATABuyPrice_TextChanged); };
            this.radioGroup2.Leave += delegate(object sender, EventArgs e) { this.radioGroup2.EditValueChanged -= new EventHandler(txtIATABuyPrice_TextChanged); };
        }

        private void txtBuyPrice_TextChanged(object sender, EventArgs e)
        {
            if (_CurrentBLInfo == null) return;

            if (radioGroup1.EditValue == null || radioGroup1.EditValue.ToString().Length == 0) return;

            try
            {
                decimal salesamount = 0m;
                if ((bool)radioGroup1.EditValue)
                {
                    salesamount = ConvertToDecimal(this.txtChKGS.Text);
                }
                else
                {
                    salesamount = ConvertToDecimal(this.txtChLBS.Text);
                }

                _CurrentBLInfo.RateCharge = ConvertToDecimal(txtBuyPrice.Text);
                this.txtBuyAmount.EditValue = _CurrentBLInfo.RateAmount = ConvertToDecimal(txtBuyPrice.Text) * salesamount;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        private void txtIATABuyPrice_TextChanged(object sender, EventArgs e)
        {
            if (_CurrentBLInfo == null) return;

            if (radioGroup2.EditValue == null || radioGroup2.EditValue.ToString().Length == 0) return;

            try
            {
                decimal iaamount = 0m;
                if ((bool)radioGroup2.EditValue)
                {
                    iaamount = ConvertToDecimal(this.txtChKGS.Text);
                }
                else
                {
                    iaamount = ConvertToDecimal(this.txtChLBS.Text);
                }

                _CurrentBLInfo.IATARateCharge = ConvertToDecimal(txtIATAPrice.Text);
                this.txtIATAAmount.EditValue = _CurrentBLInfo.IATARateAmount = ConvertToDecimal(this.txtIATAPrice.Text) * iaamount;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
            }
        }

        private void txtGKGS_TextChanged(object sender, EventArgs e)
        {
            if (txtGKGS.Text.Trim().Length <= 0)
            {
                int temp = 0;
                txtGKGS.Text = temp.ToString("F3");
                return;
            }
            try
            {
                this.txtGKGS.TextChanged -= new EventHandler(txtGKGS_TextChanged);

                this.txtGLBS.Text = (ConvertToDecimal(txtGKGS.Text) * KGS_TO_LBS_QUOTIETY).ToString("F3");
                _CurrentBLInfo.GrossKGS = Convert.ToDecimal(txtGKGS.Text);

                this.txtGKGS.TextChanged += new EventHandler(txtGKGS_TextChanged);
            }
            catch (Exception)
            {
                this.txtGKGS.TextChanged += new EventHandler(txtGKGS_TextChanged);
                return;
            }
        }

        private void txtGLBS_TextChanged(object sender, EventArgs e)
        {
            if (txtGLBS.Text.Trim().Length <= 0)
            {
                int temp = 0;
                txtGLBS.Text = temp.ToString("F3");
                return;
            }

            try
            {
                this.txtGLBS.TextChanged -= new EventHandler(txtGLBS_TextChanged);

                this.txtGKGS.Text = (ConvertToDecimal(txtGLBS.Text) / KGS_TO_LBS_QUOTIETY).ToString("F3");
                _CurrentBLInfo.GrossKGS = Convert.ToDecimal(txtGKGS.Text);

                this.txtGLBS.TextChanged += new EventHandler(txtGLBS_TextChanged);

            }
            catch (Exception)
            {
                this.txtGLBS.TextChanged += new EventHandler(txtGLBS_TextChanged);
                return;
            }
        }

        private void txtChKGS_TextChanged(object sender, EventArgs e)
        {
            if (txtChKGS.Text.Trim().Length <= 0)
            {
                int temp = 0;
                txtChKGS.Text = temp.ToString("F3");
                return;
            }
            try
            {
                this.txtChKGS.TextChanged -= new EventHandler(txtChKGS_TextChanged);

                this.txtChLBS.Text = (ConvertToDecimal(txtChKGS.Text) * KGS_TO_LBS_QUOTIETY).ToString("F3");
                _CurrentBLInfo.ChargeKGS = Convert.ToDecimal(txtChKGS.Text);
                this.txtChKGS.TextChanged += new EventHandler(txtChKGS_TextChanged);
            }
            catch (Exception)
            {
                this.txtChKGS.TextChanged += new EventHandler(txtChKGS_TextChanged);
                return;
            }
        }

        private void txtChLBS_TextChanged(object sender, EventArgs e)
        {
            if (txtChLBS.Text.Trim().Length <= 0)
            {
                int temp = 0;
                txtChLBS.Text = temp.ToString("F3");
                return;
            }

            try
            {
                this.txtChLBS.TextChanged -= new EventHandler(txtChLBS_TextChanged);

                this.txtChKGS.Text = (ConvertToDecimal(txtChLBS.Text) / KGS_TO_LBS_QUOTIETY).ToString("F3");

                _CurrentBLInfo.ChargeKGS = Convert.ToDecimal(txtChKGS.Text);
                this.txtChLBS.TextChanged += new EventHandler(txtChLBS_TextChanged);

            }
            catch (Exception)
            {
                this.txtChLBS.TextChanged += new EventHandler(txtChLBS_TextChanged);
                return;
            }
        }

        //转换为Decimal
        private decimal ConvertToDecimal(object pamValue)
        {
            try
            {
                return Convert.ToDecimal(pamValue);
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {
            _CurrentBLInfo = data as AirMBLInfo;
            this.bindingSource1.DataSource = _CurrentBLInfo;

            textboxEventControl();
            InitControls();
            RefreshBarEnabled();
            InitCustomerDescriptionObject();
            _originalMBLNO = _CurrentBLInfo.No;
            //_CurrentBLInfo.BeginEdit();
        }

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            return this.Save();
        }

        public override void EndEdit()
        {
            this.Validate();
            bindingSource1.EndEdit();
            OtherChargesBindingSource.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region 把客户端对象转换为服务保存方法所需的对象

        SaveMBLInfoParameter ConvertMBLToParameter(bool IsSaveAs, AirMBLInfo info)
        {
            string otherChargersXMLString = string.Empty;
            if (info.OtherChargeList != null && info.OtherChargeList.Count > 0)
            {
                OtherChargeXML item = new OtherChargeXML
                {
                    Items = (from i in info.OtherChargeList
                             select new OtherChargeItemXML
                             {
                                 ChargeName = i.ChargeName,
                                 Amount = i.Amount.ToString()
                             }).ToList()
                };

                otherChargersXMLString = ICP.Framework.CommonLibrary.Helper.SerializerHelper.SerializeToString<OtherChargeXML>(item, true, true);
            }

            SaveMBLInfoParameter parameter = new SaveMBLInfoParameter
            {
                id = IsSaveAs ? Guid.Empty : info.ID,
                airBookingID = info.AirBookingID,
                mblNo = info.No,
                numberOfOriginal = info.NumberOfOriginal,
                checkerID = info.CheckerID,
                shipperID = info.ShipperID,
                shipperDescription = info.ShipperDescription,
                ShipperAccountNo = info.ShipperAccountNo,
                consigneeID = info.ConsigneeID,
                consigneeDescription = info.ConsigneeDescription,
                ConsigneeAccountNo = info.ConsigneeAccountNo,
                notifyPartyID = info.NotifyPartyID,
                notifyPartyDescription = info.NotifyPartyDescription,
                agentID = info.AgentID,
                agentDescription = info.AgentDescription,
                AgentOfCarrierID = info.AgentOfCarrierID,
                AgentIATACode = info.AgentIATACode,
                AgentAccountNo = info.AgentAccountNo,
                DepartureID = info.DepartureID,
                DepartureName = info.DepartureName,
                ETD=info.ETD,
                DetinationID = info.DetinationID,
                DetinationName = info.DetinationName,
                ETA=info.ETA,
                FilightNoID = info.FilightNoID,
                AirCompanyID = info.AirCompanyID,
                placeOfDeliveryID = info.PlaceOfDeliveryID,
                TranshipmentPort1 = info.TranshipmentPort1,
                TranshipmentPort1By = info.TranshipmentPort1By,
                TranshipmentPort2 = info.TranshipmentPort2,
                TranshipmentPort2By = info.TranshipmentPort2By,
                TranshipmentPort3 = info.TranshipmentPort3,
                TranshipmentPort3By = info.TranshipmentPort3By,
                DeclaredValueForCarriage = info.DeclaredValueForCarriage,
                DeclaredValueForCustoms = info.DeclaredValueForCustoms,
                InsuranceAmount = info.InsuranceAmount,
                RateClass = info.RateClass,
                CurrencyID = info.CurrencyID,
                IATACurrencyID = info.IATACurrencyID,
                RateCharge = info.RateCharge,
                IATARateCharge = info.IATARateCharge,
                RateAmount = info.RateAmount,
                IATARateAmount = info.IATARateAmount,
                HandingInformation = info.HandingInformation,
                OtherChargeList = otherChargersXMLString,
                OtherChargeDescription = info.OtherChargeDescription,
                Tax = info.Tax,
                ValuationCharge = info.ValuationCharge,
                AgentCharger = info.AgentCharger,
                CarrierCharger = info.CarrierCharger,
                CurrencyConversionRate = info.CurrencyConversionRate,
                DestinationCurrencyAmount = info.DestinationCurrencyAmount,
                ChargesAtDestination = info.ChargesAtDestination,
                ChargeableWeightUnitIsKGS = info.ChargeableWeightUnitIsKGS,
                IATAChargeableWeightUnitIsKGS = info.IATAChargeableWeightUnitIsKGS,
                paymentTermID = info.PaymentTermID,
                OtherPaymentTermID = info.OtherPaymentTermID,
                freightDescription = info.FreightDescription,
                releaseType = info.ReleaseType,
                releaseDate = info.ReleaseDate,
                quantity = info.Quantity,
                quantityUnitID = info.QuantityUnitID,
                weight = info.GrossKGS,
                ChargeableWeight = info.ChargeKGS,
                measurement = info.Measurement,
                measurementUnitID = info.MeasurementUnitID,
                marks = info.Marks,
                goodsDescription = info.GoodsDescription,

                issuePlaceID = info.IssuePlaceID,
                issueByID = info.IssueByID,
                issueDate = info.IssueDate,

                saveByID = LocalData.UserInfo.LoginID,
                updateDate = IsSaveAs ? null : _CurrentBLInfo.UpdateDate
            };

            return parameter;
        }

        #endregion
    }    
}


