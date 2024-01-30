using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;

using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using Microsoft.Practices.ObjectBuilder;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.FCM.OceanExport.UI.BL;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanExport.UI.BL.HBL;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.Service;
using ICP.FAM.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;
using System.Linq;
using ICP.FCM.Common.UI;
using DevExpress.XtraEditors.Controls;
using System.Threading;


namespace ICP.FCM.OceanExport.UI.HBL
{
    [ToolboxItem(false)]
    public partial class HBLEditPart : BaseEditPart
    {
        #region 服务注入

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IGeographyService geographyService { get; set; }


        [ServiceDependency]
        public IDataFindClientService dfService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ITransportFoundationService tfService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [ServiceDependency]
        public IOceanExportService oeService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanExportPrintHelper OceanExportPrintHelper { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public IFinanceClientService finClientService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        #endregion

        #region 本地变量

        #region 箱信息

        /// <summary>
        /// 当前业务是否有箱列表
        /// </summary>
        private bool isHasContainer = false;

        /// <summary>
        /// 箱信息是否发生改变
        /// </summary>
        private bool isCtnCharge = false;

        /// <summary>
        /// 删除的箱ID集合
        /// </summary>
        private List<Guid> ctnIDList = new List<Guid>();

        /// <summary>
        /// 删除的箱更新时间集合
        /// </summary>
        private List<DateTime?> ctnUpdateDateList = new List<DateTime?>();

        #endregion

        OceanBookingInfo _bookingInfo = null;
        OceanHBLInfo _CurrentBLInfo = null;
        List<OceanBLContainerList> _ctnList = null;
        /// <summary>
        /// 代理下拉数据源
        /// </summary>
        List<CustomerList> _agentCustomersList = null;
        List<BookingBLInfo> _OceanMBLs = null;
        bool isChangedCtnList = false;

        bool isInitBookingContainerDescription = false;
        ContainerDescription bookingContainerDescription;
        List<ContainerList> ctntypes = null;
        List<ContainerList> Ctntypes
        {
            get
            {
                if (ctntypes != null) return ctntypes;

                ctntypes = tfService.GetContainerList(string.Empty, true, 0);
                return ctntypes;
            }
        }

        /// 是否需要申请代理
        /// </summary>
        bool _isNeedRequestAgent = false;

        /// <summary>
        /// 是否需要保存前自动申请代理
        /// </summary>
        bool _isNeedAutoRequestAgent = false;
        /// <summary>
        /// 是否单击右侧按钮
        /// </summary>
        bool isSpinRight = false;

        // private VoyageDateInfoHelper voyageDateHelper = null;

        /// <summary>
        /// 标记是否保存已经保存了HBL
        /// </summary>
        bool isSave = false;

        string[] arrAMSCTYO = new string[] { "美国航线", "AMERICA", "美国东海岸航线", "America Eest", "美国西海岸航线", "America West" };

        string[] arrAMS8FH5 = new string[] { "加拿大航线", "CANADA" };

        #endregion

        #region 初始化

        public HBLEditPart()
        {
            InitializeComponent();
            if (LocalData.IsDesignMode)
            {
                return;
            }
            this.Disposed += delegate { if (Workitem != null) Workitem.Items.Remove(this); };
            if (LocalData.IsEnglish == false) SetCnText();

            //  voyageDateHelper = new VoyageDateInfoHelper();
            // voyageDateHelper.Init(VoyageFormType.HBL, stxtPreVoyage, stxtVoyage, stxtPlaceOfReceipt, txtPOLCode, txtPODCode, dtPETD, dtETD, dtETA, null, null, null, chkShowPreVoyage, chkShowVoyage);


        }


        #endregion

        #region 初始化的一些方法

        private void SetCnText()
        {
            tabPageBase.Text = "基础";
            labAgent.Text = "代理";
            labChecker.Text = "对单人";
            labConsignee.Text = "收货人";
            labContainerDescription.Text = "集装箱号";
            labFinalDestination.Text = "最终目的地";
            labFreightDescription.Text = "应收/应付";
            labIssueDate.Text = "签发日期";
            labIssueBy.Text = "签发人";
            labIssuePlace.Text = "签发地";
            labMarks.Text = "标记与标号";
            labMeasurement.Text = "体积";
            labNotifyParty.Text = "通知人";
            labPaymentTerm.Text = "付款方式";
            labPlaceOfDelivery.Text = "交货地";
            labPlaceOfReceipt.Text = "收货地";
            labPOD.Text = "卸货港";
            labPOL2.Text = "装货港";
            labPreVoyage.Text = "驳船";
            labQuantity.Text = "件数";
            labRefNo.Text = "业务号";
            labReleaseDate.Text = "放单时间";
            labReleaseType.Text = "放单类型";
            labShipper.Text = "发货人";

            labTransportClause.Text = "运输条款";
            labVoyage.Text = "大船";
            labWeight.Text = "重量";
            labCtnQtyInfo.Text = "集装箱或件数合计";
            labDescriptionOfGoods.Text = "包装种类或货名";
            labType.Text = "类型";
            chkIsWoodPacking.Text = "木质包装";

            barSave.Caption = "保存(&S)";
            barSaveAs.Caption = "另存为(&A)";

            barRefresh.Caption = "刷新(&R)";

            barSubCheck.Caption = "对单";
            barCheck.Caption = "申请(&K)";
            barCheckDone.Caption = "完成(&D)";

            barClose.Caption = "关闭(&C)";



            barSubPrint.Caption = "打印";
            barPrintBL.Caption = "打印提单";

            //barReplyAgent.Caption = "申请代理(&R)";
            btnContainer.Text = "箱信息";

            navBarBaseInfo.Caption = "基本信息";
            navBarBLInfo.Caption = "提单信息";
            navBarCargo.Caption = "货物信息";
            navBarIssueInfo.Caption = "签发信息";

            labMBLNo.Text = "主提单号";
            labHBlNo.Text = "分提单号";

            chkShowVoyage.Text = chkShowPreVoyage.Text = "显示";
        }
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
            if (_CurrentBLInfo.AMSShipperDescription == null)
            {
                _CurrentBLInfo.AMSShipperDescription = new CustomerDescription();
            }
            if (_CurrentBLInfo.AMSConsigneeDescription == null)
            {
                _CurrentBLInfo.AMSConsigneeDescription = new CustomerDescription();
            }
            if (_CurrentBLInfo.AMSNotifyPartyDescription == null)
            {
                _CurrentBLInfo.AMSNotifyPartyDescription = new CustomerDescription();
            }

            txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
            txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);
            txtAgentDescription.Text = _CurrentBLInfo.AgentDescription.ToString(LocalData.IsEnglish);
            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.NotifyPartyID) && _CurrentBLInfo.NotifyPartyDescription == null)
                txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";
            else
            {
                txtNotifyPartyDescription.Text = _CurrentBLInfo.NotifyPartyDescription.ToString(LocalData.IsEnglish);
                if (txtNotifyPartyDescription.Text.Length == 0) txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";
            }


            txtAMSShipperDescription.Text = _CurrentBLInfo.AMSShipperDescription.ToString(LocalData.IsEnglish);
            txtAMSConsigneeDescription.Text = _CurrentBLInfo.AMSConsigneeDescription.ToString(LocalData.IsEnglish);
            txtAMSNotifyPartyDescription.Text = _CurrentBLInfo.AMSNotifyPartyDescription.ToString(LocalData.IsEnglish);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            this.panelScroll.Click += delegate { panelScroll.Focus(); };

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ID))
            {
                _CurrentBLInfo.Marks = @"N/M";
            }

            txtNo.Properties.ReadOnly = false;
            txtNo.Text = _CurrentBLInfo.No;
            SetComboboxEnumSource();
            SetComboboxSource();
            RefreshControlsByData();
            SearchRegister();
            stxtRefNo.Focus();

            if (_bookingInfo == null && _CurrentBLInfo != null && !Utility.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID))
            {
                _bookingInfo = oeService.GetOceanBookingInfo(_CurrentBLInfo.OceanBookingID);
            }

            if (_bookingInfo != null && !Utility.GuidIsNullOrEmpty(_bookingInfo.ID))
            {
                if (_bookingInfo.OEOperationType == FCMOperationType.BULK)
                {
                    btnContainer.Enabled = false;
                }
                else
                {
                    btnContainer.Enabled = true;
                }
            }
            else
            {
                btnContainer.Enabled = false;
            }


        }

        /// <summary>
        /// 初始化消息 
        /// </summary>
        private void InitMessage()
        {
            this.RegisterMessage("CreateBills", LocalData.IsEnglish ? "According to the contract system is generated with the bills" : "系统已根据合约生成了应付账单");
            this.RegisterMessage("CreateBillsForPayORCon", LocalData.IsEnglish ? "Modification of the contract or payment, system is reformed with the bills" : "修改了合约或付款方式，系统已重新生成了应付账单");
            this.RegisterMessage("CreateBillsForContainer", LocalData.IsEnglish ? "Modify box information, system is reformed with the bills" : "修改了箱信息，系统已重新生成了应付账单");
            this.RegisterMessage("CreateBillsForALL", LocalData.IsEnglish ? "Modify the contract or payment and box information, system is reformed with the bills" : "修改了合约或付款方式及箱信息，系统已重新生成了应付账单");
            this.RegisterMessage("IsExisteMBLNo", "MBLNo:{0} 已经存在,是否继续保存");
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

                if (_CurrentBLInfo.IsRequestAgent) stxtAgent.Enabled = false;
                //_CurrentBLInfo.WoodPacking = txtIsWoodPacking.Text = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
                #endregion
            }
            else
            {
                if (string.IsNullOrEmpty(_CurrentBLInfo.ContainerQtyDescription))
                    txtCtnQty.Text = "SHIPPER'S LOAD COUNT & SEAL(0*0) CONTAINER S.T.C.";
                else
                    txtCtnQty.Text = "SHIPPER'S LOAD COUNT & SEAL(" + _CurrentBLInfo.ContainerQtyDescription + ") CONTAINER S.T.C.";

                if (string.IsNullOrEmpty(_CurrentBLInfo.TransportClauseName) == false) txtCtnQty.Text += "\r\n" + _CurrentBLInfo.TransportClauseName;

                stxtRefNo.Properties.ReadOnly = true;
                stxtRefNo.Properties.Buttons[0].Enabled = false;
                stxtRefNo.Text = _CurrentBLInfo.RefNo;
                stxtRefNo.Tag = _CurrentBLInfo.OceanBookingID;
            }

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID) == false) RefreshEnabledByBookingType(_CurrentBLInfo.OEOperationType);

            #region WoodPacking
            bool isInitWoodPacking = _CurrentBLInfo.IsNew || string.IsNullOrEmpty(_CurrentBLInfo.WoodPacking) ? true : false;
            chkIsWoodPacking.CheckedChanged += delegate
            {
                if (isInitWoodPacking)
                {
                    if (chkIsWoodPacking.Checked)
                        _CurrentBLInfo.WoodPacking = txtIsWoodPacking.Text = "WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT AND HAS BEEN FUMIGATED";
                    else
                        _CurrentBLInfo.WoodPacking = txtIsWoodPacking.Text = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
                }
                isInitWoodPacking = true;
            };
            #endregion
            OceanExportPrintHelper.SetVoyageCheckByVoyageShowType(_CurrentBLInfo.VoyageShowType, chkShowVoyage, chkShowPreVoyage, _CurrentBLInfo.VesselVoyage, _CurrentBLInfo.PreVesselVoyage);
            //chkShowPreVoyage.CheckedChanged += delegate { _CurrentBLInfo.VoyageShowType = OceanExportPrintHelper.GetVoyageShowTypeByVoyageCheck(chkShowPreVoyage, chkShowVoyage); };
            //chkShowVoyage.CheckedChanged += delegate { _CurrentBLInfo.VoyageShowType = OceanExportPrintHelper.GetVoyageShowTypeByVoyageCheck(chkShowPreVoyage, chkShowVoyage); };
        }

        #region Combobox

        /// <summary>
        /// Enum 提单类型 放单类型
        /// </summary>
        void SetComboboxEnumSource()
        {
            //提单类型
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<IssueType>> issueTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<IssueType>(LocalData.IsEnglish);
            foreach (var item in issueTypes)
            {
                if (item.Value == 0) continue;
                cmbIssueType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            //放单类型
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<FCMReleaseType>> releaseTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<FCMReleaseType>(LocalData.IsEnglish);
            foreach (var item in releaseTypes)
            {
                if (item.Value == 0) continue;
                cmbReleaseType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            //AMSEntryType
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AMSEntryType>> amsEntryTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AMSEntryType>(LocalData.IsEnglish);
            foreach (var item in amsEntryTypes)
            {
                if (item.Value == 0) continue;
                cmbAMSEntryType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            //ACIEntryType
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<ACIEntryType>> aciEntryTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<ACIEntryType>(LocalData.IsEnglish);
            foreach (var item in aciEntryTypes)
            {
                if (item.Value == 0) continue;
                cmbACIEntryType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        /// <summary>
        /// 签发人 付款方式 运输条款 包装 重量 体积 代理
        /// </summary>
        void SetComboboxSource()
        {
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
                List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(cmbPaymentTerm, DataDictionaryType.PaymentTerm, DataBindType.EName, true);

                cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
                cmbPaymentTerm.SelectedIndexChanged += new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
            });
            if (_CurrentBLInfo.PreVoyageID != null)
            {
                this.stxtPreVoyage.ShowSelectedValue(_CurrentBLInfo.PreVoyageID, _CurrentBLInfo.PreVesselVoyage);
            }
            if (_CurrentBLInfo.VoyageID != null)
            {
                this.stxtVoyage.ShowSelectedValue(_CurrentBLInfo.VoyageID, _CurrentBLInfo.VesselVoyage);
            }
            #endregion

            #region 包装
            cmbQuantityUnit.ShowSelectedValue(_CurrentBLInfo.QuantityUnitID, _CurrentBLInfo.QuantityUnitName);
            Utility.SetEnterToExecuteOnec(cmbQuantityUnit, delegate
            {
                List<DataDictionaryList> packTypes = ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
            });
            #endregion

            #region 重量
            cmbWeightUnit.ShowSelectedValue(_CurrentBLInfo.WeightUnitID, _CurrentBLInfo.WeightUnitName);
            Utility.SetEnterToExecuteOnec(cmbWeightUnit, delegate
            {
                List<DataDictionaryList> weightUnitsList = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
            });
            #endregion

            #region 体积
            cmbMeasurementUnit.ShowSelectedValue(_CurrentBLInfo.MeasurementUnitID, _CurrentBLInfo.MeasurementUnitName);
            Utility.SetEnterToExecuteOnec(cmbMeasurementUnit, delegate
            {
                List<DataDictionaryList> volUnitss = ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
            });
            #endregion

            #region 运输条款

            cmbTransportClause.ShowSelectedValue(_CurrentBLInfo.TransportClauseID, _CurrentBLInfo.TransportClauseName);
            Utility.SetEnterToExecuteOnec(cmbTransportClause, delegate
            {
                List<TransportClauseList> transportClauseList = ICPCommUIHelper.SetCmbTransportClause(cmbTransportClause);
                cmbTransportClause.SelectedIndexChanged += delegate
                {
                    if (string.IsNullOrEmpty(_CurrentBLInfo.ContainerQtyDescription))
                        txtCtnQty.Text = "SHIPPER'S LOAD COUNT & SEAL(0*0) CONTAINER S.T.C.";
                    else
                        txtCtnQty.Text = "SHIPPER'S LOAD COUNT & SEAL(" + _CurrentBLInfo.ContainerQtyDescription + ") CONTAINER S.T.C.";

                    if (string.IsNullOrEmpty(_CurrentBLInfo.TransportClauseName) == false) txtCtnQty.Text += "\r\n" + _CurrentBLInfo.TransportClauseName;
                };

            });
            #endregion

            #endregion

            #region MBL

            if (_CurrentBLInfo.IsNew == false || _CurrentBLInfo.ShippingOrderID.IsNullOrEmpty() == false)
            {
                OceanBookingList booking = oeService.GetOceanBookingListByIds(new Guid[] { _CurrentBLInfo.OceanBookingID })[0];
                _OceanMBLs = booking.OceanMBLs;
                foreach (var item in _OceanMBLs)
                {
                    cmbMBLNO.Properties.Items.Add(item.NO);
                }
                cmbMBLNO.Text = _CurrentBLInfo.MBLNo;
            }
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
                stxtAgent.OnOk += new EventHandler(stxtAgent_OnOk);
            });

            #endregion

        }


        void stxtAgent_OnOk(object sender, EventArgs e)
        {
            CustomerDescription des = stxtAgent.CustomerDescription;
            if (des == null)
            {
                des = new CustomerDescription();
            }
            //_CurrentBLInfo.IsDirty = true;
            _CurrentBLInfo.AgentDescription = des;
            txtAgentDescription.Text = stxtAgent.CustomerDescription.ToString(LocalData.IsEnglish);
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
            //	数据从订单表[fcm..OceanBookings]检索
            //	查询面板及栏位定义，请参照订舱单列表
            //	筛选范围：订舱单.状态不等于“已关单”
            dfService.Register(stxtRefNo, ICP.FCM.OceanExport.ServiceInterface.Comm.FCMFinderConstants.OceanBookingFinder, SearchFieldConstants.BookingNO, SearchFieldConstants.OceanBookingResultValue,
                  delegate(object inputSource, object[] resultData)
                  {

                      Guid bookingID = new Guid(resultData[0].ToString());
                      string reno = resultData[2].ToString();
                      if (_CurrentBLInfo.OceanBookingID != bookingID)
                      {
                          AfterSearchRefNo(bookingID, reno);

                          if (_bookingInfo.OEOperationType == FCMOperationType.BULK)
                          {
                              btnContainer.Enabled = false;
                          }
                          else
                          {
                              btnContainer.Enabled = true;
                          }

                      }
                      else
                      {
                          LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ?
                        "You has been selected this booking." : "您已选择了该票订舱单.");
                      }

                  },
                  delegate
                  {
                      stxtRefNo.Text = _CurrentBLInfo.RefNo = string.Empty;
                      stxtRefNo.Tag = _CurrentBLInfo.OceanBookingID = Guid.Empty;
                      _CurrentBLInfo.OceanBookingID = Guid.Empty;
                      _CurrentBLInfo.MBLNo = string.Empty;
                      cmbMBLNO.Properties.Items.Clear();
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

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
                true);
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
                true);
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
                 this.txtNotifyPartyDescription,
                 ICPCommUIHelper,
                 true
                 , "SAME AS CONSIGNEE");
                notifyPartyBridge.Init();

            });
            stxtNotifyParty.OnOk += new EventHandler(stxtNotifyParty_OnOk);
            #endregion

            #region AMS
            //shipper
            Utility.SetEnterToExecuteOnec(stxtAMSShipper, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                CustomerFinderBridge aMSShipperBridge = new CustomerFinderBridge(
               this.stxtAMSShipper,
               _countryList,
               this.dfService,
               this.customerService,
               _CurrentBLInfo.AMSShipperDescription,
               this.txtAMSShipperDescription,
               ICPCommUIHelper,
               LocalData.IsEnglish);
                aMSShipperBridge.Init();

            });


            //Consignee
            Utility.SetEnterToExecuteOnec(stxtAMSConsignee, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                CustomerFinderBridge aMSConsigneeBridge = new CustomerFinderBridge(
                this.stxtAMSConsignee,
                _countryList,
                this.dfService,
                this.customerService,
                _CurrentBLInfo.AMSConsigneeDescription,
                this.txtAMSConsigneeDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                aMSConsigneeBridge.Init();
            });


            //NotifyParty
            Utility.SetEnterToExecuteOnec(stxtAMSNotifyParty, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                CustomerFinderBridge aMSNotifyPartyBridge = new CustomerFinderBridge(
                this.stxtAMSNotifyParty,
                _countryList,
                this.dfService,
                this.customerService,
                _CurrentBLInfo.AMSNotifyPartyDescription,
                this.txtAMSNotifyPartyDescription,
                ICPCommUIHelper,
                LocalData.IsEnglish);
                aMSNotifyPartyBridge.Init();
            });

            #endregion

            dfService.Register(stxtChecker, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
              delegate(object inputSource, object[] resultData)
              {
                  stxtChecker.Text = _CurrentBLInfo.CheckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                  stxtChecker.Tag = _CurrentBLInfo.CheckerID = new Guid(resultData[0].ToString());

              }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region Port

            //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
            //大船 筛选：装货港=当前装货港and卸货港=当前卸货港

            #region PlaceOfReceipt
            dfService.Register(stxtPlaceOfReceipt, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());

                      bool isUpdatePort = true;
                      //如果改变收货地,且大船或驳船不为空，给出提示
                      if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID) == false
                          && portID != _CurrentBLInfo.PlaceOfReceiptID)
                      {
                          if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "选择的收货地驳船的装货港不匹配,是否清空驳船?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question) == DialogResult.Yes)
                          {
                              stxtPreVoyage.Text = _CurrentBLInfo.PreVesselVoyage = string.Empty;
                              stxtPreVoyage.Tag = _CurrentBLInfo.PreVoyageID = null;

                          }
                          else
                              isUpdatePort = false;
                      }

                      if (isUpdatePort)
                      {
                          txtPlaceOfReceiptName.Text = _CurrentBLInfo.PlaceOfReceiptName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          stxtPlaceOfReceipt.Text = _CurrentBLInfo.PlaceOfReceiptCode = resultData[1].ToString();
                          stxtPlaceOfReceipt.Tag = _CurrentBLInfo.PlaceOfReceiptID = portID;
                      }
                  },
                  delegate
                  {
                      stxtPlaceOfReceipt.Tag = _CurrentBLInfo.PlaceOfReceiptID = null;
                      stxtPlaceOfReceipt.Text = _CurrentBLInfo.PlaceOfReceiptCode = string.Empty;
                      txtPlaceOfReceiptName.Text = _CurrentBLInfo.PlaceOfReceiptName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region POL
            dfService.Register(txtPOLCode, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      //如果改变装货港,且大船不为空，给出提示
                      bool isUpdatePort = true;
                      if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false
                          && portID != _CurrentBLInfo.POLID)
                      {
                          if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "选择的收货地与大船装货港或驳船的卸货港不匹配,是否清空大船和驳船?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question) == DialogResult.Yes)
                          {
                              stxtPreVoyage.Text = _CurrentBLInfo.PreVesselVoyage = string.Empty;
                              stxtPreVoyage.Tag = _CurrentBLInfo.PreVoyageID = null;
                              stxtVoyage.Text = _CurrentBLInfo.VesselVoyage = string.Empty;
                              stxtVoyage.Tag = _CurrentBLInfo.VoyageID = null;
                          }
                          else
                              isUpdatePort = false;
                      }

                      if (isUpdatePort)
                      {
                          txtPOLName.Text = _CurrentBLInfo.POLName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          txtPOLCode.Text = _CurrentBLInfo.POLCode = resultData[1].ToString();
                          txtPOLCode.Tag = _CurrentBLInfo.POLID = portID;
                      }
                  },
                  delegate
                  {
                      txtPOLCode.Tag = _CurrentBLInfo.POLID = Guid.Empty;
                      txtPOLCode.Text = _CurrentBLInfo.POLCode = string.Empty;
                      txtPOLName.Text = _CurrentBLInfo.POLName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region POD
            dfService.Register(txtPODCode, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      //如果改变卸货港,且大船不为空，给出提示
                      bool isUpdatePort = true;
                      if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false
                          && portID != _CurrentBLInfo.PODID)
                      {
                          if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "选择的装货港与大船的卸货港不匹配,是否清空大船?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question) == DialogResult.Yes)
                          {
                              stxtVoyage.Text = _CurrentBLInfo.VesselVoyage = string.Empty;
                              stxtVoyage.Tag = _CurrentBLInfo.VoyageID = null;
                          }
                          else
                              isUpdatePort = false;
                      }

                      if (isUpdatePort)
                      {
                          txtPODName.Text = _CurrentBLInfo.PODName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                          txtPODCode.Text = _CurrentBLInfo.PODCode = resultData[1].ToString();
                          txtPODCode.Tag = _CurrentBLInfo.PODID = portID;

                          //如果不是to Door,填充交货地和最终目的地
                          if (string.IsNullOrEmpty(_CurrentBLInfo.TransportClauseName) == false
                              && _CurrentBLInfo.TransportClauseName.ToUpper().Contains("-DOOR") == false)
                          {
                              //txtPlaceOfDeliveryName.Text = _CurrentBLInfo.PlaceOfDeliveryName = _CurrentBLInfo.PODName;
                              //stxtPlaceOfReceipt.Text = _CurrentBLInfo.PlaceOfDeliveryCode = _CurrentBLInfo.PODCode;
                              //stxtPlaceOfDelivery.Tag = _CurrentBLInfo.PlaceOfDeliveryID = portID;

                              txtFinalDestinationName.Text = _CurrentBLInfo.FinalDestinationName = _CurrentBLInfo.PODName;
                              stxtFinalDestination.Text = _CurrentBLInfo.FinalDestinationCode = _CurrentBLInfo.PODCode;
                              stxtFinalDestination.Tag = _CurrentBLInfo.FinalDestinationID = portID;
                          }
                      }


                  },
                  delegate
                  {
                      txtPODCode.Tag = _CurrentBLInfo.PODID = Guid.Empty;
                      txtPODCode.Text = _CurrentBLInfo.PODCode = string.Empty;
                      txtPODName.Text = _CurrentBLInfo.PODName = string.Empty;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region PlaceOfDelivery
            dfService.Register(stxtPlaceOfDelivery, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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
            #region FinalDestination
            dfService.Register(stxtFinalDestination, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      stxtFinalDestination.Text = _CurrentBLInfo.FinalDestinationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                      txtFinalDestinationName.Text = _CurrentBLInfo.FinalDestinationName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                      stxtFinalDestination.Text = _CurrentBLInfo.FinalDestinationCode = resultData[1].ToString();
                      stxtFinalDestination.Tag = _CurrentBLInfo.FinalDestinationID = new Guid(resultData[0].ToString());
                  },
                  delegate
                  {
                      txtFinalDestinationName.Text = _CurrentBLInfo.FinalDestinationName = string.Empty;
                      stxtFinalDestination.Text = _CurrentBLInfo.FinalDestinationCode = string.Empty;
                      stxtFinalDestination.Tag = _CurrentBLInfo.FinalDestinationID = null;
                  },
                  ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion
            #region IssuePlace
            dfService.Register(stxtIssuePlace, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    stxtIssuePlace.Text = _CurrentBLInfo.IssuePlaceName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtIssuePlace.Tag = _CurrentBLInfo.IssuePlaceID = new Guid(resultData[0].ToString());
                }, Guid.Empty, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion

            #endregion

            #region Voyage

            //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
            //dfService.Register(stxtPreVoyage,
            //    CommonFinderConstants.VesselVoyageFinder,
            //    SearchFieldConstants.VesselVoyage,
            //    SearchFieldConstants.VesselResultValue,
            //    this.GetConditionsForSearchPreVoyage,
            //    delegate(object inputSource, object[] resultData)
            //    {
            //        stxtPreVoyage.Text = _CurrentBLInfo.PreVesselVoyage = resultData[1].ToString() + "/" + resultData[2].ToString();
            //        stxtPreVoyage.Tag = _CurrentBLInfo.PreVoyageID = new Guid(resultData[0].ToString());
            //        PreVoyageChanged();
            //    },
            //    delegate
            //    {
            //        stxtPreVoyage.Text = _CurrentBLInfo.PreVesselVoyage = string.Empty;
            //        stxtPreVoyage.Tag = _CurrentBLInfo.PreVoyageID = null;
            //        PreVoyageChanged();
            //    },
            //    ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            //大船 筛选：装货港=当前装货港and卸货港=当前卸货港
            //dfService.Register(stxtVoyage,
            //    CommonFinderConstants.VesselVoyageFinder,
            //    SearchFieldConstants.VesselVoyage,
            //    SearchFieldConstants.VesselResultValue,
            //    this.GetConditionsForSearchVoyage,
            //    delegate(object inputSource, object[] resultData)
            //    {
            //        stxtVoyage.Text = _CurrentBLInfo.VesselVoyage = resultData[1].ToString() + "/" + resultData[2].ToString();
            //        stxtVoyage.Tag = _CurrentBLInfo.VoyageID = new Guid(resultData[0].ToString());
            //        VoyageChanged();
            //    },
            //    delegate
            //    {
            //        stxtVoyage.Text = _CurrentBLInfo.VesselVoyage = string.Empty;
            //        stxtVoyage.Tag = _CurrentBLInfo.VoyageID = null;
            //        VoyageChanged();
            //    },
            //    ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            stxtPreVoyage.EditValueChanged += new EventHandler(stxtPreVoyage_EditValueChanged);
            stxtVoyage.EditValueChanged += new EventHandler(stxtVoyage_EditValueChanged);
            #endregion
        }

        void stxtVoyage_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(stxtVoyage.EditText.Trim()))
            {
                chkShowVoyage.Checked = true;
            }
            else
            {

                chkShowVoyage.Checked = false;
            }
        }

        void stxtPreVoyage_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(stxtPreVoyage.EditText.Trim()))
            {
                chkShowPreVoyage.Checked = true;
            }
            else
            {

                chkShowPreVoyage.Checked = false;
            }
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
        //大船 筛选：装货港=当前装货港and卸货港=当前卸货港
        SearchConditionCollection GetConditionsForSearchVoyage()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("POLID", _CurrentBLInfo.POLID, false);
            conditions.AddWithValue("POLName", _CurrentBLInfo.POLCode, false);
            conditions.AddWithValue("PODID", _CurrentBLInfo.PODID, false);
            conditions.AddWithValue("PODName", _CurrentBLInfo.PODCode, false);
            return conditions;
        }

        //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
        SearchConditionCollection GetConditionsForSearchPreVoyage()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("POLID", _CurrentBLInfo.PlaceOfReceiptID, false);
            conditions.AddWithValue("POLName", _CurrentBLInfo.PlaceOfReceiptCode, false);
            conditions.AddWithValue("PODID", _CurrentBLInfo.POLID, false);
            conditions.AddWithValue("PODName", _CurrentBLInfo.POLCode, false);
            return conditions;
        }

        #endregion

        #endregion

        #region 控件或数据的联动操作

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

            bool isFirst = false;
            if (_agentCustomersList == null)
            {
                _agentCustomersList = configureService.GetCompanyAgentList(_CurrentBLInfo.CompanyID, true);
                isFirst = true;
            }

            if (_bookingInfo != null &&
                !Utility.GuidIsNullOrEmpty(_bookingInfo.CustomerID) &&
                (string.IsNullOrEmpty(this._bookingInfo.SalesTypeName) == false &&
                        (this._bookingInfo.SalesTypeName.Contains("指定货") ||
                        this._bookingInfo.SalesTypeName.ToUpper().Contains("AGENT"))))
            {
                CustomerList find = (from d in _agentCustomersList where d.ID == _bookingInfo.CustomerID select d).Take(1).SingleOrDefault();
                if (find == null)
                {
                    CustomerList opCustomer = new CustomerList();  //业务的客户
                    opCustomer.ID = _bookingInfo.CustomerID;
                    opCustomer.IsDangerous = true;
                    opCustomer.EName = _bookingInfo.CustomerName;
                    _agentCustomersList.Insert(1, opCustomer);
                }
            }
            else
            {
                CustomerList CustomerFind = (from d in _agentCustomersList where d.IsDangerous == true select d).Take(1).SingleOrDefault();
                if (CustomerFind != null)
                {
                    _agentCustomersList.Remove(CustomerFind);
                    if (_CurrentBLInfo.AgentID == CustomerFind.ID)
                    {
                        _CurrentBLInfo.AgentID = null;
                        _CurrentBLInfo.AgentName = string.Empty;
                        _CurrentBLInfo.AgentDescription = new CustomerDescription();
                    }
                }
            }

            if (isFirst)
            {
                CustomerList emptyCustomer = new CustomerList();
                emptyCustomer.CName = emptyCustomer.EName = string.Empty;
                emptyCustomer.ID = Guid.Empty;
                _agentCustomersList.Insert(0, emptyCustomer);
            }

            SetAgentSource(_agentCustomersList, true);
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
            {
                BulidAgentDescriqitonByID(_CurrentBLInfo.AgentID);
            }
            else
            {
                stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription;
                txtAgentDescription.Text = _CurrentBLInfo.AgentDescription.ToString(LocalData.IsEnglish);
            }
            stxtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
            stxtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
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
        ///
        int i = 0;

        private void BulidAgentDescriqitonByID(Guid? id)
        {
            if (Utility.GuidIsNullOrEmpty(id))
            {
                stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription = new CustomerDescription();
            }
            else
            {
                stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription;

                if (i == 0 || i == 1) { }
                else
                {
                    ICPCommUIHelper.SetCustomerDesByID(id, _CurrentBLInfo.AgentDescription);
                }
            }

            i++;

            txtAgentDescription.Text = _CurrentBLInfo.AgentDescription.ToString(LocalData.IsEnglish);
        }
        #endregion

        #region 搜索业务号后把Booking的数据填充到当前页面

        /// <summary>
        /// 搜索业务号后把Booking的数据填充到当前页面
        /// </summary>
        private void AfterSearchRefNo(Guid bookingID, string reno)
        {
            #region 	如果之前已选择业务号

            _bookingInfo = oeService.GetOceanBookingInfo(bookingID);

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID) == false && _CurrentBLInfo.OceanBookingID != bookingID)
            {
                ///	否则提示：“是否重新导入发货人、收货人、通知人、地点、货物信息？”，如果选择是，则继续执行下一步，否则退出。
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否重新导入发货人、收货人、通知人、地点、货物信息?"
                                 , LocalData.IsEnglish ? "Tip" : "提示"
                                 , MessageBoxButtons.YesNo
                                 , MessageBoxIcon.Question) == DialogResult.No)
                {
                    stxtRefNo.Text = _CurrentBLInfo.RefNo = reno;
                    stxtRefNo.Tag = _CurrentBLInfo.OceanBookingID = bookingID;
                    this.stxtPreVoyage.Tag = this.stxtPreVoyage.EditValue = _CurrentBLInfo.PreVoyageID = _bookingInfo.PreVoyageID;
                    this.stxtPreVoyage.EditText = _CurrentBLInfo.PreVesselVoyage = _bookingInfo.PreVoyageName;
                    this.stxtVoyage.Tag = this.stxtVoyage.EditValue = _CurrentBLInfo.VoyageID = _bookingInfo.VoyageID;
                    this.stxtVoyage.EditText = _CurrentBLInfo.VesselVoyage = _bookingInfo.VoyageName;

                    this.dtETD.EditValue = _CurrentBLInfo.ETD = _bookingInfo.ETD;
                    this.dtETA.EditValue = _CurrentBLInfo.ETA = _bookingInfo.ETA;

                    return;
                }

            }
            #endregion


            #region 如果选择的订舱单为“只出MBL”时，提示用户，需要修改订单属性，然后再新增HBL。

            if (_bookingInfo.IsOnlyMBL)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "此订舱单已钩选\"只出MBL\",强制关联HBL会自动修改订舱单数据.是否继续?"
                                    , LocalData.IsEnglish ? "Tip" : "提示"
                                    , MessageBoxButtons.YesNo
                                    , MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            #endregion

            if (_bookingInfo.OceanMBLs != null && _bookingInfo.OceanMBLs.Count > 0)
            {
                isHasContainer = true;
            }
            #region 填充
            _CurrentBLInfo.CustomerName = _bookingInfo.CustomerName;

            _CurrentBLInfo.CompanyID = _bookingInfo.CompanyID;
            stxtRefNo.Text = _CurrentBLInfo.RefNo = _bookingInfo.No;
            stxtRefNo.Tag = _CurrentBLInfo.OceanBookingID = _bookingInfo.ID;
            _CurrentBLInfo.OceanBookingID = _bookingInfo.ID;

            _CurrentBLInfo.AgentOfCarrierName = _bookingInfo.AgentOfCarrierName;
            _CurrentBLInfo.SONO = _bookingInfo.OceanShippingOrderNo;
            _CurrentBLInfo.CarrierName = _bookingInfo.CarrierName;
            _CurrentBLInfo.SalesName = _bookingInfo.SalesName;
            _CurrentBLInfo.FilerName = _bookingInfo.FilerName;
            _CurrentBLInfo.BookingerName = _bookingInfo.BookingerName;
            _CurrentBLInfo.OverseasFilerName = _bookingInfo.OverSeasFilerName;

            if (_bookingInfo.HBLReleaseType != null && _bookingInfo.HBLReleaseType.Value != FCMReleaseType.Unknown)
                _CurrentBLInfo.ReleaseType = _bookingInfo.HBLReleaseType.Value;


            #region 收发通,代理

            #region 发货人

            _CurrentBLInfo.ShipperID = _bookingInfo.ShipperID == null ? Guid.Empty : _bookingInfo.ShipperID.Value;
            _CurrentBLInfo.ShipperName = _bookingInfo.ShipperName;
            _CurrentBLInfo.ShipperDescription = _bookingInfo.ShipperDescription;
            if (_CurrentBLInfo.ShipperDescription != null)
                txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);

            #endregion

            #region NotifyParty .收货人 = 订舱单.收货人 通知人描述 = “SAME AS CONSIGNEE”

            _CurrentBLInfo.ConsigneeID = _bookingInfo.ConsigneeID;
            _CurrentBLInfo.ConsigneeName = _bookingInfo.ConsigneeName;
            stxtConsignee.CustomerDescription = _CurrentBLInfo.ConsigneeDescription = _bookingInfo.ConsigneeDescription;
            if (_CurrentBLInfo.ConsigneeDescription != null)
                txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);

            _CurrentBLInfo.NotifyPartyID = Guid.Empty;
            _CurrentBLInfo.NotifyPartyName = string.Empty;
            stxtNotifyParty.CustomerDescription = _CurrentBLInfo.NotifyPartyDescription = new CustomerDescription();
            txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";

            #endregion

            #region Agent  HBL.代理 = 订舱单.代理

            SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);

            _CurrentBLInfo.AgentID = _bookingInfo.AgentID;
            _CurrentBLInfo.AgentName = _bookingInfo.AgentName;
            _CurrentBLInfo.AgentDescription = _bookingInfo.AgentDescription;
            //SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);

            #endregion

            #endregion

            #region Port

            //	HBL.收货地描述 = 订舱单.收货地.名称 
            //	HBL.装货港描述 = 订舱单. 装货港.名称 
            //	HBL.卸货港描述 = 订舱单. 卸货港.名称 
            //	HBL.交货地描述 = 订舱单. 交货地.名称 
            _CurrentBLInfo.PlaceOfDeliveryID = _bookingInfo.PlaceOfDeliveryID;
            _CurrentBLInfo.PlaceOfDeliveryCode = _bookingInfo.PlaceOfDeliveryName;
            _CurrentBLInfo.PlaceOfDeliveryName = _bookingInfo.PlaceOfDeliveryName;

            _CurrentBLInfo.POLID = _bookingInfo.POLID;
            _CurrentBLInfo.POLCode = _bookingInfo.POLName;
            _CurrentBLInfo.POLName = _bookingInfo.POLName;

            _CurrentBLInfo.PODID = _bookingInfo.PODID;
            _CurrentBLInfo.PODCode = _bookingInfo.PODName;
            _CurrentBLInfo.PODName = _bookingInfo.PODName;

            _CurrentBLInfo.PlaceOfReceiptID = _bookingInfo.PlaceOfReceiptID;
            _CurrentBLInfo.PlaceOfReceiptCode = _bookingInfo.PlaceOfReceiptName;
            _CurrentBLInfo.PlaceOfReceiptName = _bookingInfo.PlaceOfReceiptName;

            //	MBL.最终目的地 = if 主提单.运输条款<>DOOR and 只出MBL=false then 订舱单.交货地 else 订舱单.最终目的地
            //	MBL. 最终目的地描述=最终目的地.名称 
            if (string.IsNullOrEmpty(_bookingInfo.TransportClauseName) == false
               && _bookingInfo.TransportClauseName.Contains("-DOOR") == false
               && _bookingInfo.IsOnlyMBL == false)
            {
                _CurrentBLInfo.FinalDestinationID = _bookingInfo.PlaceOfDeliveryID;
                _CurrentBLInfo.FinalDestinationCode = _bookingInfo.PlaceOfDeliveryName;
                _CurrentBLInfo.FinalDestinationName = _bookingInfo.PlaceOfDeliveryName;
            }
            else
            {
                _CurrentBLInfo.FinalDestinationID = _bookingInfo.FinalDestinationID;
                _CurrentBLInfo.FinalDestinationCode = _bookingInfo.FinalDestinationName;
                _CurrentBLInfo.FinalDestinationName = _bookingInfo.FinalDestinationName;
            }

            #endregion

            #region Voyage

            _CurrentBLInfo.PreVoyageID = _bookingInfo.PreVoyageID;
            _CurrentBLInfo.PreVesselVoyage = _bookingInfo.PreVoyageName;
            _CurrentBLInfo.VoyageID = _bookingInfo.VoyageID;
            _CurrentBLInfo.VesselVoyage = _bookingInfo.VoyageName;
            OceanExportPrintHelper.SetPrintCheckByVoyageType(_CurrentBLInfo.PreVoyageID, _CurrentBLInfo.VoyageID, chkShowPreVoyage, chkShowVoyage);
            _CurrentBLInfo.ETD = _bookingInfo.ETD;
            _CurrentBLInfo.ETA = _bookingInfo.ETA;

            //if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false)
            //{
            //    VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.VoyageID.Value);
            //}

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

            _CurrentBLInfo.TransportClauseID = _bookingInfo.TransportClauseID;
            _CurrentBLInfo.TransportClauseName = _bookingInfo.TransportClauseName;
            cmbTransportClause.ShowSelectedValue(_CurrentBLInfo.TransportClauseID, _CurrentBLInfo.TransportClauseName);

            cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
            _CurrentBLInfo.BookingPaymentTermID = _CurrentBLInfo.PaymentTermID = _bookingInfo.HBLPaymentTermID;
            cmbPaymentTerm.Text = _CurrentBLInfo.PaymentTermName = _bookingInfo.HBLPaymentTermName;
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

            _CurrentBLInfo.Quantity = _bookingInfo.Quantity;
            if (Utility.GuidIsNullOrEmpty(_bookingInfo.QuantityUnitID) == false)
            {
                cmbQuantityUnit.Text = _CurrentBLInfo.QuantityUnitName = _bookingInfo.QuantityUnitName;
                _CurrentBLInfo.QuantityUnitID = _bookingInfo.QuantityUnitID.Value;

                cmbQuantityUnit.ShowSelectedValue(_CurrentBLInfo.QuantityUnitID, _CurrentBLInfo.QuantityUnitName);

            }
            _CurrentBLInfo.Weight = _bookingInfo.Weight;
            if (Utility.GuidIsNullOrEmpty(_bookingInfo.WeightUnitID) == false)
            {
                cmbWeightUnit.Text = _CurrentBLInfo.WeightUnitName = _bookingInfo.WeightUnitName;
                _CurrentBLInfo.WeightUnitID = _bookingInfo.WeightUnitID.Value;

                cmbWeightUnit.ShowSelectedValue(_CurrentBLInfo.WeightUnitID, _CurrentBLInfo.WeightUnitName);
            }
            _CurrentBLInfo.Measurement = _bookingInfo.Measurement;
            if (Utility.GuidIsNullOrEmpty(_bookingInfo.MeasurementUnitID) == false)
            {
                cmbMeasurementUnit.Text = _CurrentBLInfo.MeasurementUnitName = _bookingInfo.MeasurementUnitName;
                _CurrentBLInfo.MeasurementUnitID = _bookingInfo.MeasurementUnitID.Value;
                cmbMeasurementUnit.ShowSelectedValue(_CurrentBLInfo.MeasurementUnitID, _CurrentBLInfo.MeasurementUnitName);
            }
            #endregion

            _CurrentBLInfo.CarrierName = _bookingInfo.CarrierName;

            _CurrentBLInfo.ContractID = _bookingInfo.ContractID;
            _CurrentBLInfo.IsHasContract = !Utility.GuidIsNullOrEmpty(_bookingInfo.ContractID);

            bookingContainerDescription = _bookingInfo.ContainerDescription;
            bindingSource1.EndEdit();
            RefreshEnabledByBookingType(_bookingInfo.OEOperationType);

            cmbMBLNO.Properties.Items.Clear();
            _OceanMBLs = _bookingInfo.OceanMBLs;
            if (_OceanMBLs == null) _OceanMBLs = new List<BookingBLInfo>();
            foreach (var item in _OceanMBLs)
            {
                cmbMBLNO.Properties.Items.Add(item.NO);
            }
            #endregion

            barReplyAgent.Enabled = true;
        }

        #endregion

        #region Voyage Changed

        /// <summary>
        /// 大船改变，填充ETD，如果没有驳船，填充ETA， 截柜日，截关日,截文件日
        /// </summary>
        private void VoyageChanged()
        {
            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID))
            {
                chkShowVoyage.Checked = chkShowVoyage.Enabled = false;

                if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
                {
                    dtETA.EditValue = _CurrentBLInfo.ETA = null;
                }
                dtPETD.EditValue = _CurrentBLInfo.ETD = null;
            }
            else
            {
                //VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.VoyageID.Value);
                //dtPOR.EditValue = _CurrentBLInfo.ETD = voyageInfo.ETD;

                //if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
                //{
                //    dtPOD.EditValue = _CurrentBLInfo.ETA = voyageInfo.ETA;

                //}

                if (string.IsNullOrEmpty(_CurrentBLInfo.PreVesselVoyage))
                {
                    chkShowVoyage.Checked = chkShowVoyage.Enabled = true;
                }
                else
                {
                    chkShowPreVoyage.Checked = chkShowPreVoyage.Enabled = true;
                    chkShowVoyage.Checked = false; chkShowVoyage.Enabled = true;
                }
            }
        }

        //驳船
        private void chkShowPreVoyage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPreVoyage.Checked)
            {
                switch (_CurrentBLInfo.VoyageShowType)
                {
                    case VoyageShowType.Confirm:   //大船
                        _CurrentBLInfo.VoyageShowType = VoyageShowType.All;
                        break;

                    case VoyageShowType.Unknown:
                        _CurrentBLInfo.VoyageShowType = VoyageShowType.PreConfirm;  //驳船
                        break;
                }
            }
            else
            {
                switch (_CurrentBLInfo.VoyageShowType)
                {
                    case VoyageShowType.All:
                        _CurrentBLInfo.VoyageShowType = VoyageShowType.Confirm; //大船
                        break;

                    case VoyageShowType.PreConfirm:
                        _CurrentBLInfo.VoyageShowType = VoyageShowType.Unknown;
                        break;
                }
            }

            _CurrentBLInfo.IsDirty = true;
        }

        //大船
        private void chkShowVoyage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowVoyage.Checked)
            {
                switch (_CurrentBLInfo.VoyageShowType)
                {
                    case VoyageShowType.PreConfirm:   //驳船
                        _CurrentBLInfo.VoyageShowType = VoyageShowType.All;
                        break;

                    case VoyageShowType.Unknown:
                        _CurrentBLInfo.VoyageShowType = VoyageShowType.Confirm;  //大船
                        break;
                }
            }
            else
            {
                switch (_CurrentBLInfo.VoyageShowType)
                {
                    case VoyageShowType.All:
                        _CurrentBLInfo.VoyageShowType = VoyageShowType.PreConfirm;  //驳船
                        break;

                    case VoyageShowType.Confirm:
                        _CurrentBLInfo.VoyageShowType = VoyageShowType.Unknown;
                        break;
                }
            }

            _CurrentBLInfo.IsDirty = true;
        }

        /// <summary>
        /// 驳船改变,填充ETA， 截柜日，截关日,截文件日
        /// </summary>
        private void PreVoyageChanged()
        {
            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
            {
                chkShowPreVoyage.Checked = chkShowPreVoyage.Enabled = false;
                dtETA.EditValue = _CurrentBLInfo.ETA = null;
            }
            else
            {
                //VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.PreVoyageID.Value);
                //dtPOD.EditValue = _CurrentBLInfo.ETA = voyageInfo.ETA;

                chkShowPreVoyage.Checked = chkShowPreVoyage.Enabled = true;
                chkShowVoyage.Checked = false; chkShowVoyage.Enabled = true;
            }
        }

        /// <summary>
        /// 如果交货地所在的国家不存在于公司配置客户对应的国家并且代理已存在，需要提示用户是否清空代理并重新申请代理
        /// </summary>
        void PlaceOfDeliveryChanged()
        {
            _isNeedRequestAgent = false;
            _isNeedAutoRequestAgent = false;

            //如果交货地所在的国家不存在于公司配置客户对应的国家并且代理已存在，需要提示用户是否清空代理并重新申请代理
            if (_CurrentBLInfo.IsRequestAgent == false
                && Utility.GuidIsNullOrEmpty(_CurrentBLInfo.AgentID) == false)
            {
                try
                {
                    bool isExist = oeService.IsPortCountryExistCompanyConfig(_CurrentBLInfo.PlaceOfDeliveryID.Value, _CurrentBLInfo.CompanyID);
                    if (isExist == false)
                    {
                        _isNeedRequestAgent = true;
                        if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Current BL need request agent.is clear current BL\'s agent?"
                                                                  : "当前提单需要申请代理,是否清空代理?"
                           , LocalData.IsEnglish ? "Tip" : "提示"
                           , MessageBoxButtons.YesNo
                           , MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _isNeedAutoRequestAgent = true;
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

        #endregion

        #region

        #region  付款方式
        void cmbPaymentTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.BookingPaymentTermID) == false && _CurrentBLInfo.BookingPaymentTermID != _CurrentBLInfo.PaymentTermID)
            {
                if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "现选择的HBL付款方式与订舱单中HBL付款方式不同,是否继续?",
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

        #endregion

        #region 刷新可操作性
        /// <summary>
        /// 根据业务类型 刷新 重量,单位,体积,等 可用性
        /// </summary>
        /// <param name="oeOperationType">业务类型</param>
        void RefreshEnabledByBookingType(FCMOperationType oeOperationType)
        {
            if (oeOperationType == FCMOperationType.FCL || oeOperationType == FCMOperationType.LCL)
                numMeasurement.Properties.ReadOnly = numWeight.Properties.ReadOnly = numQuantity.Properties.ReadOnly = true;
            else
                numMeasurement.Properties.ReadOnly = numWeight.Properties.ReadOnly = numQuantity.Properties.ReadOnly = false;
        }

        /// <summary>
        /// 刷新按钮可用性
        /// </summary>
        void RefreshBarEnabled()
        {
            if (_CurrentBLInfo.IsValid == false)
            {
                barCheck.Enabled = barCheckDone.Enabled = barSubCheck.Enabled
                    = barPrintBL.Enabled = barSaveAs.Enabled = barSave.Enabled = barReplyAgent.Enabled = false;
            }
            else if (_CurrentBLInfo.IsNew)
            {
                barRefresh.Enabled = barCheck.Enabled = barCheckDone.Enabled =
                  barPrintBL.Enabled = barSaveAs.Enabled = barReplyAgent.Enabled = false;
            }
            else
            {
                barPrintBL.Enabled = barRefresh.Enabled = barSaveAs.Enabled = barReplyAgent.Enabled = true;

                if (string.IsNullOrEmpty(_CurrentBLInfo.ContainerNos) == false)
                {

                    if (_CurrentBLInfo.State == OEBLState.Draft)
                    {
                        barCheck.Enabled = barCheckDone.Enabled = true;
                    }
                    else if (_CurrentBLInfo.State == OEBLState.Checking)
                    {
                        barCheckDone.Enabled = true;
                    }
                }
                else
                    barCheck.Enabled = barCheckDone.Enabled = false;

                //对单完成和已放单，则不可申请代理
                if (_CurrentBLInfo.State == OEBLState.Checked || _CurrentBLInfo.State == OEBLState.Release)
                {
                    barReplyAgent.Enabled = false;
                }
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
            if (ValidateData() == false) return false;
            try
            {
                if (_CurrentBLInfo.IsDirty == false && _CurrentBLInfo.IsNew == false && isChangedCtnList == false) return false;
                if (!AutoRequestAgent()) return false;

                #region 处理与MBL的关系
                BookingBLInfo existBlItem = _OceanMBLs.Find(delegate(BookingBLInfo item) { return item.NO == _CurrentBLInfo.MBLNo; });
                if (existBlItem != null)
                {
                    _CurrentBLInfo.MBLID = existBlItem.ID;
                    _CurrentBLInfo.MBLUpdateDate = existBlItem.UpdateDate;
                }
                else if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.MBLID) == false)
                {
                    HBLIsUpdateMBLNOForm f = this.Workitem.Items.AddNew<HBLIsUpdateMBLNOForm>();
                    DialogResult result = f.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        return false;
                    else if (result == DialogResult.No)//Add New
                    {
                        _CurrentBLInfo.MBLID = Guid.Empty;
                        _CurrentBLInfo.MBLUpdateDate = null;
                    }
                    //else //Update
                    //{
                    //    BookingBLInfo mbl = _OceanMBLs.Find(delegate(BookingBLInfo item) { return item.ID == _CurrentBLInfo.MBLID; });
                    //    _CurrentBLInfo.MBLUpdateDate = mbl.UpdateDate;
                    //}
                }
                #endregion


                barSave.Enabled = false;
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Saveing......" : "保存中.....");

                bool isAutoBulidMBL = false;

                DialogResult dialogResult = DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "Update MBL?" : "是否需要更新MBL？"), (LocalData.IsEnglish ? "ToolTip" : "提示"), MessageBoxButtons.OKCancel);

                if (isChangedCtnList || ctnIDList.Count > 0)
                {
                    #region 更新HBL与箱信息
                    bool IsSynToMBL = false;
                    if (dialogResult == DialogResult.OK) { IsSynToMBL = true; }

                    if (_CurrentBLInfo.ID != Guid.Empty)
                    {

                        //  _CurrentBLInfo.UpdateDate = oeService.GetOceanHBLInfo(_CurrentBLInfo.ID).UpdateDate; 
                    };
                    SaveHBLInfoParameter hbl = ConvertHBLToParameter(false, _CurrentBLInfo);
                    if (hbl.mblID.IsNullOrEmpty() && string.IsNullOrEmpty(hbl.mblNO) == false)
                    {
                        isAutoBulidMBL = true;
                        if (!IsExisteMBLNo())
                        {
                            return false;
                        }
                    }

                    SaveBLContainerParameter ctn = ConvertCtnToParameter(false, _CurrentBLInfo.OceanBookingID, _ctnList);
                    //SingleResult result = oeService.SaveOceanHBLAndContainerWithTrans(hbl, ctn, ctnIDList, ctnUpdateDateList, IsSynBoxInfo);
                    SingleResult result = oeService.SaveOceanHBLAndContainerWithTrans(hbl, ctn, ctnIDList, ctnUpdateDateList, IsSynToMBL);
                    SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                    ManyResult ctnResult = null;
                    if (ctn != null)
                    {
                        //SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                        ctnResult = result.GetValue<ManyResult>("ContainerResult");
                        isHasContainer = true;
                    }

                    isSave = true;
                    _CurrentBLInfo.IsDirty = false;
                    #endregion

                    #region  处理返回值
                    UpdateBLBySaved(blResult);

                    if (ctnResult != null && ctnResult.Items.Count > 0)
                    {
                        _CurrentBLInfo.MBLUpdateDate = ctnResult.Items[0].GetValue<DateTime?>("MBLUpdateDate");
                        if (existBlItem != null)
                        {
                            existBlItem.UpdateDate = _CurrentBLInfo.MBLUpdateDate;
                        }
                   
                        for (int i = 0; i < ctnResult.Items.Count; i++)
                        {
                            if (i >= _ctnList.Count)
                            {
                                break;
                            }
                            _ctnList[i].ID = ctnResult.Items[i].GetValue<Guid>("ID");
                            _ctnList[i].CargoID = ctnResult.Items[i].GetValue<Guid?>("CargoID");

                            _ctnList[i].UpdateDate = ctnResult.Items[i].GetValue<DateTime?>("UpdateDate");
                            _ctnList[i].CargoUpdateDate = ctnResult.Items[i].GetValue<DateTime?>("CargoUpdateDate");
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 只更新HBL
                    SaveHBLInfoParameter hbl = ConvertHBLToParameter(false, _CurrentBLInfo);
                    if (hbl.mblID.IsNullOrEmpty() && string.IsNullOrEmpty(hbl.mblNO) == false)
                    {
                        isAutoBulidMBL = true;

                        if (!IsExisteMBLNo())
                        {
                            return false;
                        }
                    }

                    bool IsSynToMBL = false;
                    if (dialogResult == DialogResult.OK) { IsSynToMBL = true; }

                    SingleResult result = oeService.SaveOceanHBLInfo(hbl, IsSynToMBL);

                    UpdateBLBySaved(result);
                    isSave = true;
                    _CurrentBLInfo.IsDirty = false;
                    #endregion
                }

                #region 重新生成运价
                if ((isChangedCtnList || ctnIDList.Count > 0) && _CurrentBLInfo.IsHasContract)
                {
                    if (isCtnCharge || _CurrentBLInfo.IsChargePayOrCon)
                    {
                        try
                        {
                            SingleResult result = this.oeService.CreateBill(_CurrentBLInfo.OceanBookingID, LocalData.UserInfo.LoginID);

                            if (result != null)
                            {
                                int s = result.GetValue<Byte>("State");
                                if (s == 1)
                                {
                                    if (_CurrentBLInfo.IsChargePayOrCon && isCtnCharge)
                                    { Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBillsForALL")); }
                                    else if (_CurrentBLInfo.IsChargePayOrCon)
                                    { Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBillsForPayORCon")); }
                                    else if (isCtnCharge)
                                    { Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBillsForContainer")); }

                                    //Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBills"));
                                    _CurrentBLInfo.IsChargePayOrCon = false;
                                    isCtnCharge = false;
                                }
                                else if (s == 2)
                                {
                                    string message = result.GetValue<string>("message");

                                    string title = LocalData.IsEnglish ? "Generate the bill Error:" : "生成账单失败：";
                                    Utility.ShowMessage(title + message);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                        }

                    }
                }
                #endregion

                ctnIDList = new List<Guid>();
                ctnUpdateDateList = new List<DateTime?>();

                AfterSave(false, isAutoBulidMBL);
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);

                return false;
            }
            finally { barSave.Enabled = true; }
        }

        /// <summary>
        /// MBL是否重复，并弹出提示是否继续
        /// </summary>
        /// <returns></returns>
        private bool IsExisteMBLNo()
        {
            if (oeService.IsExistsMBLNo(_CurrentBLInfo.MBLID, _CurrentBLInfo.MBLNo))
            {
                string message = string.Format(NativeLanguageService.GetText(this, "IsExisteMBLNo"), _CurrentBLInfo.MBLNo);

                DialogResult dr = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                                  (LocalData.IsEnglish ? " Tip" : "提示"),
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question,
                                                   MessageBoxDefaultButton.Button2);

                if (dr == DialogResult.No || dr == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private void UpdateBLBySaved(SingleResult result)
        {
            _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
            _CurrentBLInfo.MBLID = result.GetValue<Guid>("OceanMBLID");
            _CurrentBLInfo.No = result.GetValue<string>("No");
            _CurrentBLInfo.AMSNo = result.GetValue<string>("AMSNo");
            _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            _CurrentBLInfo.MBLUpdateDate = result.GetValue<DateTime?>("MBLUpdateDate");

            BookingBLInfo existBlItem = _OceanMBLs.Find(delegate(BookingBLInfo item) { return item.ID == _CurrentBLInfo.MBLID; });
            if (existBlItem != null)
            {
                existBlItem.NO = _CurrentBLInfo.MBLNo;
                existBlItem.UpdateDate = _CurrentBLInfo.MBLUpdateDate;
            }
            else
            {
                BookingBLInfo newItem = new BookingBLInfo();
                newItem.ID = _CurrentBLInfo.MBLID;
                newItem.NO = _CurrentBLInfo.MBLNo;
                newItem.UpdateDate = _CurrentBLInfo.MBLUpdateDate;
                _OceanMBLs.Add(newItem);
            }

            cmbMBLNO.Properties.Items.Clear();
            foreach (var item in _OceanMBLs)
            {
                cmbMBLNO.Properties.Items.Add(item.NO);
            }
            cmbMBLNO.Text = _CurrentBLInfo.MBLNo;

        }

        /// <summary>
        /// 自动申请代理
        /// </summary>
        private bool AutoRequestAgent()
        {
            if (_isNeedRequestAgent)
            {
                if (_isNeedAutoRequestAgent)
                {
                    SingleResult result = fcmCommonService.RequestOceanAgent(_CurrentBLInfo.OceanBookingID
                                                                            , ICP.Framework.CommonLibrary.Common.OperationType.OceanExport
                                                                            , LocalData.UserInfo.LoginID
                                                                            , DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified)
                                                                            , AgentType.Normal
                                                                            , string.Empty, null);
                    _isNeedRequestAgent = false;
                    _CurrentBLInfo.IsRequestAgent = true;
                    stxtAgent.Enabled = false;
                    return true;
                }
                else
                {
                    bool srcc = fcmCommonClientService.OpenAgentRequestPart(_CurrentBLInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, this.Workitem);
                    if (srcc)
                    {
                        bool isDirty = _CurrentBLInfo.IsDirty;
                        _isNeedRequestAgent = false;
                        _CurrentBLInfo.IsRequestAgent = true;
                        stxtAgent.Enabled = false;
                        _CurrentBLInfo.IsDirty = isDirty;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        void AfterSave(bool IsSaveAs, bool isAutoBulidMBL)
        {
            //this.btnContainer.Enabled = true;
            if (containerListPart != null)
            {
                containerListPart.HBLID = _CurrentBLInfo.ID;
                containerListPart.ShippingOrderID = _CurrentBLInfo.OceanBookingID;
            }
            isChangedCtnList = false;


            _CurrentBLInfo.ContainerNos = Utility.BulidCtnNOByContainerList(_ctnList);
            if (string.IsNullOrEmpty(_CurrentBLInfo.ContainerNos) == false)
            {
                cmbMBLNO.Properties.ReadOnly = true;
            }
            stxtRefNo.Properties.ReadOnly = true;

            _CurrentBLInfo.IsDirty = false;
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

            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
            stxtRefNo.Properties.ReadOnly = true;
            stxtRefNo.Properties.Buttons[0].Enabled = false;
            if (!IsSaveAs)
                this.Title = LocalData.IsEnglish ? "Edit HBL " + _CurrentBLInfo.No : "编辑HBL " + _CurrentBLInfo.No;

            RefreshBarEnabled();
            if (Saved != null) Saved(new object[] { _CurrentBLInfo });

            string message = string.Empty;

            if (IsSaveAs)
                message = LocalData.IsEnglish ? "Save as a new bl successfully." : "已成功另存为一票新的提单.";
            else
                message = LocalData.IsEnglish ? "Save Successfully" : "保存成功.";


            if (isAutoBulidMBL)
                message += LocalData.IsEnglish ? "System has been bulid a new MBL." : "系统已自动生成一票新的MBL.";

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
        }

        private bool ValidateData()
        {
            this.EndEdit();

            bool isScrr = true;
            if (_CurrentBLInfo.Validate
                    (
                        delegate(ValidateEventArgs e)
                        {
                            if (string.IsNullOrEmpty(_CurrentBLInfo.MBLNo))
                            {
                                e.SetErrorInfo("MBLNo", LocalData.IsEnglish ? "Must Select Or Input MBLNO" : "必须选择或输入MBL号.");
                            }
                            if (_CurrentBLInfo.POLID != Guid.Empty && _CurrentBLInfo.POLID == _CurrentBLInfo.PODID)
                            {
                                e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                            }
                            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ShipperID))
                            {
                                e.SetErrorInfo("ShipperID", LocalData.IsEnglish ? "Shipper Must Inpu" : "发货人必须输入.");
                            }


                        }
                    ) == false) isScrr = false;

            return isScrr;
        }

        #endregion

        #region 另存为

        private void barSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveAs();
        }

        bool SaveAs()
        {
            if (ValidateData() == false) return false;

            if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的提单?",
                            LocalData.IsEnglish ? "Tip" : "提示",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.No)
            {
                return false;
            }

            try
            {

                if (_ctnList == null) _ctnList = oeService.GetOceanHBLContainerList(_CurrentBLInfo.ID);
                bool needSaveCtn = false;//如果没有关联的箱，不提交保存箱
                bool isSaveCtn = false;//询问用户是否保存箱
                #region
                foreach (var item in _ctnList) { if (item.Relation) { needSaveCtn = true; continue; } }
                if (needSaveCtn)
                {
                    if (DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Un Done" : "是否保存箱?",
                                        LocalData.IsEnglish ? "Tip" : "提示",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        isSaveCtn = true;
                    }
                }
                #endregion


                _CurrentBLInfo.AMSNo = string.Empty;
                _CurrentBLInfo.ISFNo = string.Empty;

                if (_CurrentBLInfo.MBLUpdateDate == null)
                {
                    BookingBLInfo existBlItem = _OceanMBLs.Find(delegate(BookingBLInfo item) { return item.NO == _CurrentBLInfo.MBLNo; });
                    if (existBlItem != null)
                    {
                        _CurrentBLInfo.MBLID = existBlItem.ID;
                        _CurrentBLInfo.MBLUpdateDate = existBlItem.UpdateDate;
                    }
                }

                bool isAutoBulidMBL = false;
                //txtCtnInfo.Text = _CurrentBLInfo.ContainerDescription = string.Empty;
                if (isSaveCtn == false)
                {
                    SaveHBLInfoParameter hbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    if (hbl.mblID.IsNullOrEmpty() && string.IsNullOrEmpty(hbl.mblNO) == false) isAutoBulidMBL = true;

                    SingleResult result = oeService.SaveOceanHBLInfo(hbl, true);

                    _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.MBLID = result.GetValue<Guid>("OceanMBLID");
                    _CurrentBLInfo.MBLUpdateDate = result.GetValue<DateTime?>("MBLUpdateDate");
                    _CurrentBLInfo.No = result.GetValue<string>("No");
                    //_CurrentBLInfo.AMSNo = result.GetValue<string>("AMSNo");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    txtCtnInfo.Text = _CurrentBLInfo.ContainerDescription = string.Empty;
                }
                else
                {
                    SaveHBLInfoParameter mbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    SaveHBLInfoParameter hbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    if (hbl.mblID.IsNullOrEmpty() && string.IsNullOrEmpty(hbl.mblNO) == false) isAutoBulidMBL = true;


                    SaveBLContainerParameter ctn = ConvertCtnToParameter(true, _CurrentBLInfo.OceanBookingID, _ctnList);
                    //SingleResult result = oeService.SaveOceanHBLAndContainerWithTrans(mbl, ctn, ctnIDList, ctnUpdateDateList,true);
                    SingleResult result = oeService.SaveOceanHBLAndContainerWithTrans(mbl, ctn, ctnIDList, ctnUpdateDateList, true);

                    SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                    ManyResult ctnResult = result.GetValue<ManyResult>("ContainerResult");

                    #region  处理返回值

                    _CurrentBLInfo.ID = blResult.GetValue<Guid>("ID");
                    _CurrentBLInfo.MBLID = blResult.GetValue<Guid>("OceanMBLID");
                    _CurrentBLInfo.MBLUpdateDate=blResult.GetValue<DateTime?>("MBLUpdateDate");
                    _CurrentBLInfo.No = blResult.GetValue<string>("No");
                    //_CurrentBLInfo.AMSNo = result.GetValue<string>("AMSNO");
                    _CurrentBLInfo.UpdateDate = blResult.GetValue<DateTime?>("UpdateDate");


                    _CurrentBLInfo.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    _CurrentBLInfo.CreateByID = LocalData.UserInfo.LoginID;
                    _CurrentBLInfo.CreateByName = LocalData.UserInfo.LoginName;

                    if (ctnResult != null && ctnResult.Items.Count > 0)
                    {
                        _CurrentBLInfo.MBLUpdateDate = ctnResult.Items[0].GetValue<DateTime?>("MBLUpdateDate");


                        BookingBLInfo existBlItem = _OceanMBLs.Find(delegate(BookingBLInfo item) { return item.NO == _CurrentBLInfo.MBLNo; });
                        if (existBlItem != null)
                        {
                            existBlItem.UpdateDate = _CurrentBLInfo.MBLUpdateDate;
                        }

                    }


                    for (int i = 0; i < ctnResult.Items.Count; i++)
                    {
                        _ctnList[i].ID = ctnResult.Items[i].GetValue<Guid>("ID");
                        _ctnList[i].CargoID = ctnResult.Items[i].GetValue<Guid?>("CargoID");

                        _ctnList[i].UpdateDate = ctnResult.Items[i].GetValue<DateTime?>("UpdateDate");
                        _ctnList[i].CargoUpdateDate = ctnResult.Items[i].GetValue<DateTime?>("CargoUpdateDate");
                    }
                    #endregion
                }
                AfterSave(true, isAutoBulidMBL);

                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);

                return false;
            }
        }

        #endregion

        #region  打开箱列表

        ICP.FCM.OceanExport.UI.Container.ContainerListPart containerListPart = null;

        private void btnContainer_Click(object sender, EventArgs e)
        {
            OpenCtn();
        }

        private void OpenCtn()
        {
            try
            {
                #region 初始化 箱数据
                if (_ctnList == null)
                {

                    if (_CurrentBLInfo.IsNew == false)
                    {
                        _ctnList = oeService.GetOceanHBLContainerList(_CurrentBLInfo.ID);
                    }
                    else
                    {
                        List<OceanContainerList> bookingCtns = oeService.GetOceanContainerList(_CurrentBLInfo.OceanBookingID);
                        if (bookingCtns != null && bookingCtns.Count > 0)
                        {
                            _ctnList = new List<OceanBLContainerList>();
                            foreach (var item in bookingCtns)
                            {
                                OceanBLContainerList ctn = new OceanBLContainerList();
                                Utility.CopyToValue(item, ctn, typeof(OceanBLContainerList));

                                _ctnList.Add(ctn);
                            }
                        }
                        else
                        {
                            _ctnList = new List<OceanBLContainerList>();
                            if (isInitBookingContainerDescription == false && bookingContainerDescription != null)
                            {
                                foreach (var ctn in bookingContainerDescription.Containers)
                                {
                                    for (int i = 0; i < ctn.Quantity; i++)
                                    {
                                        OceanBLContainerList newCtn = new OceanBLContainerList();
                                        //newCtn.Quantity = ctn.Quantity == 0 ? 1 : ctn.Quantity;
                                        newCtn.Quantity = 1;
                                        newCtn.Measurement = 0m;
                                        newCtn.TypeName = ctn.Type;
                                        newCtn.Weight = ctn.Weight ?? 0m;
                                        newCtn.Relation = true;
                                        newCtn.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                                        newCtn.CreateByID = LocalData.UserInfo.LoginID;
                                        newCtn.CreateByName = LocalData.UserInfo.LoginName;
                                        ContainerList tager = Ctntypes.Find(delegate(ContainerList item) { return item.Code == (ctn.Size + ctn.Type); });
                                        if (tager != null)
                                        {
                                            newCtn.TypeID = tager.ID;
                                            newCtn.TypeName = tager.Code;
                                        }
                                        _ctnList.Add(newCtn);
                                    }
                                }

                                isInitBookingContainerDescription = true;
                            }
                        }
                    }
                }
                #endregion

                #region 初始化 箱页面实例
                if (containerListPart == null)
                {

                    containerListPart = this.Workitem.Items.AddNew<ICP.FCM.OceanExport.UI.Container.ContainerListPart>();
                    if (_bookingInfo == null)
                    {
                        _bookingInfo = oeService.GetOceanBookingInfo(_CurrentBLInfo.OceanBookingID);
                    }

                    containerListPart.OEOperationType = _bookingInfo.OEOperationType;
                    containerListPart.BLQtyUnit = cmbQuantityUnit.Text;
                    containerListPart.BLWeightUnit = this.cmbWeightUnit.Text;
                    containerListPart.BLMeasurementUnit = this.cmbMeasurementUnit.Text;
                    containerListPart.BLQuantityUnitID = _CurrentBLInfo.QuantityUnitID;
                    containerListPart.BLWeightUnitID = _CurrentBLInfo.WeightUnitID;
                    containerListPart.BLMeasurementUnitID = _CurrentBLInfo.MeasurementUnitID;
                    containerListPart.BLSourceType = FCMBLType.MBL;
                    containerListPart.MBLID = _CurrentBLInfo.ID;
                    containerListPart.BLMeasurementUnitID = _CurrentBLInfo.MeasurementUnitID;
                    containerListPart.ShippingOrderID = _CurrentBLInfo.OceanBookingID;

                    containerListPart.Saved += delegate(object[] prams)
                    {
                        #region 确定箱后更新页面信息
                        _ctnList = prams[0] as List<OceanBLContainerList>;

                        this.txtCtnInfo.Text = containerListPart.CTNInfo;
                        txtCtnQtyInfo.Text = containerListPart.QtyInfo;
                        _CurrentBLInfo.ContainerQtyDescription = containerListPart.ContainerString;

                        if (string.IsNullOrEmpty(_CurrentBLInfo.ContainerQtyDescription))
                            txtCtnQty.Text = "SHIPPER'S LOAD COUNT & SEAL(0*0) CONTAINER S.T.C.";
                        else
                            txtCtnQty.Text = "SHIPPER'S LOAD COUNT & SEAL(" + _CurrentBLInfo.ContainerQtyDescription + ") CONTAINER S.T.C.";

                        if (string.IsNullOrEmpty(_CurrentBLInfo.TransportClauseName) == false) txtCtnQty.Text += "\r\n" + _CurrentBLInfo.TransportClauseName;

                        #region 更新数量 重量件数

                        StringBuilder strbuilder = new StringBuilder();

                        int qty = 0;
                        decimal weight = 0, measurement = 0;
                        foreach (var item in _ctnList)
                        {
                            if (item.Relation == false) continue;

                            if (strbuilder.Length > 0) strbuilder.Append(ICP.Framework.CommonLibrary.Common.GlobalConstants.ShowDividedSymbol);
                            strbuilder.Append(item.No);

                            qty += item.Quantity;
                            weight += item.Weight;
                            measurement += item.Measurement;
                        }
                        numQuantity.Value = _CurrentBLInfo.Quantity = qty;
                        numWeight.Value = _CurrentBLInfo.Weight = weight;
                        numMeasurement.Value = _CurrentBLInfo.Measurement = measurement;
                        _CurrentBLInfo.ContainerNos = strbuilder.ToString();

                        #endregion

                        isChangedCtnList = true;

                        #region 确定箱信息更新后，是否需要更新账单
                        if (prams.Length > 1)
                        {
                            isCtnCharge = Convert.ToBoolean(prams[1]);
                        }
                        #endregion

                        #region 判断是否有删除过箱信息

                        List<Guid> deleteIDList = prams[2] as List<Guid>;
                        if (deleteIDList != null)
                        {
                            if (ctnIDList == null)
                            {
                                ctnIDList = new List<Guid>();
                            }
                            foreach (Guid id in deleteIDList)
                            {
                                if (!ctnIDList.Contains(id))
                                {
                                    ctnIDList.Add(id);
                                }
                            }
                        }

                        ctnUpdateDateList = prams[3] as List<DateTime?>;

                        #endregion

                        #endregion
                    };
                }
                #endregion

                containerListPart.ReadOnly = false;
                containerListPart.DataSource = _ctnList;
                containerListPart.IDList = ctnIDList;
                containerListPart.UpdateDateList = ctnUpdateDateList;
                PartLoader.ShowDialog(containerListPart, LocalData.IsEnglish ? "Edit HBLCotainer" : "编辑HBL箱信息", FormBorderStyle.Sizable);

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        }

        #endregion

        #region 关闭

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        void HBLEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
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
                stateValues.Add("OceanBLList", _CurrentBLInfo);
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

        private void barCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.State != OEBLState.Draft) return;

                try
                {
                    SingleResult result = oeService.ChangeOceanHBLState(_CurrentBLInfo.ID, OEBLState.Checking, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);

                    bool isDirty = _CurrentBLInfo.IsDirty;
                    _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    _CurrentBLInfo.State = OEBLState.Checking;
                    _CurrentBLInfo.IsDirty = isDirty;
                    RefreshBarEnabled();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Check Successfully" : "设置对单成功.");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }

                RefreshBarEnabled();
            }
        }
        private void barCheckDone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentBLInfo.ID == Guid.Empty) return;

                try
                {
                    SingleResult result = oeService.ChangeOceanHBLState(_CurrentBLInfo.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);
                    bool isDirty = _CurrentBLInfo.IsDirty;
                    _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    _CurrentBLInfo.State = OEBLState.Checked;
                    _CurrentBLInfo.IsDirty = isDirty;
                    RefreshBarEnabled();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Checked Successfully" : "设置完成对单成功.");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
                RefreshBarEnabled();
            }
        }

        private void barEDI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barBill_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OperationCommonInfo operationCommonInfo = fcmCommonService.GetOperationCommonInfo(_CurrentBLInfo.OceanBookingID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = _CurrentBLInfo.ID;
                    finClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                }
                else
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }

        private void barReplyAgent_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool srcc = fcmCommonClientService.OpenAgentRequestPart(_CurrentBLInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, this.Workitem);
            if (srcc)
            {
                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.IsRequestAgent = true;
                stxtAgent.Enabled = false;
                _CurrentBLInfo.IsDirty = isDirty;
            }
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
            _CurrentBLInfo = oeService.GetOceanHBLInfo(_CurrentBLInfo.ID);
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
            this.bindingSource1.DataSource = _CurrentBLInfo;
            bindingSource1.ResetBindings(false);
            InitCustomerDescriptionObject();
            this.Refresh();

            // }
        }

        #endregion

        #region IEditPart 成员
        void BindingData(object data)
        {

            _CurrentBLInfo = data as OceanHBLInfo;
            this.bindingSource1.DataSource = _CurrentBLInfo;

            _CurrentBLInfo.IsChargePayOrCon = false;
            isCtnCharge = false;

            InitMessage();
            InitControls();
            RefreshBarEnabled();
            InitCustomerDescriptionObject();
            _CurrentBLInfo.IsDirty = false;
        }

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value); }
        }

        public override bool SaveData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                return this.Save();
            }
        }

        public override void EndEdit()
        {
            _CurrentBLInfo.VoyageShowType = OceanExportPrintHelper.GetVoyageShowTypeByVoyageCheck(chkShowPreVoyage, chkShowVoyage);
            this.Validate();
            bindingSource1.EndEdit();
        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region 把客户端对象转换为服务保存方法所需的对象

        SaveHBLInfoParameter ConvertHBLToParameter(bool IsSaveAs, OceanHBLInfo info)
        {
            DateTime? dt = info.UpdateDate;
            if (info.UpdateDate.HasValue)
            {
                dt = DateTime.SpecifyKind(info.UpdateDate.Value, DateTimeKind.Unspecified);
            }
            SaveHBLInfoParameter parameter = new SaveHBLInfoParameter
            {
                id = IsSaveAs ? Guid.Empty : info.ID,
                oceanBookingID = info.OceanBookingID,
                hblNo = info.No,
                mblID = info.MBLID,
                mblNO = info.MBLNo,
                numberOfOriginal = info.NumberOfOriginal,
                voyageShowType = info.VoyageShowType,
                checkerID = info.CheckerID,
                shipperID = info.ShipperID,
                shipperDescription = info.ShipperDescription,
                consigneeID = info.ConsigneeID,
                consigneeDescription = info.ConsigneeDescription,
                notifyPartyID = info.NotifyPartyID,
                notifyPartyDescription = info.NotifyPartyDescription,
                agentID = info.AgentID,
                agentDescription = info.AgentDescription,
                placeOfReceiptID = info.PlaceOfReceiptID,
                placeOfReceiptName = info.PlaceOfReceiptName,
                preVoyageID = info.PreVoyageID,
                voyageID = info.VoyageID,
                polID = info.POLID,
                polName = info.POLName,
                podID = info.PODID,
                podName = info.PODName,
                placeOfDeliveryID = info.PlaceOfDeliveryID,
                placeOfDeliveryName = info.PlaceOfDeliveryName,
                finalDestinationID = info.FinalDestinationID,
                finalDestinationName = info.FinalDestinationName,
                transportClauseID = info.TransportClauseID,
                paymentTermID = info.PaymentTermID,
                freightDescription = info.FreightDescription,
                releaseType = info.ReleaseType,
                releaseDate = info.ReleaseDate,
                quantity = info.Quantity,
                quantityUnitID = info.QuantityUnitID,
                weight = info.Weight,
                weightUnitID = info.WeightUnitID,
                measurement = info.Measurement,
                measurementUnitID = info.MeasurementUnitID,
                marks = info.Marks,
                goodsDescription = info.GoodsDescription,
                isWoodPacking = info.IsWoodPacking,
                ctnQtyInfo = info.CtnQtyInfo,
                issuePlaceID = info.IssuePlaceID,
                issueByID = info.IssueByID,
                issueDate = info.IssueDate,
                woodPacking = info.WoodPacking,
                issueType = info.IssueType,
                saveByID = LocalData.UserInfo.LoginID,
                updateDate = IsSaveAs ? null : dt,
                amsNo = _CurrentBLInfo.AMSNo,
                amsShipperDescription = _CurrentBLInfo.AMSShipperDescription,
                amsConsigneeDescription = _CurrentBLInfo.AMSConsigneeDescription,
                amsNotifyPartyDescription = _CurrentBLInfo.AMSNotifyPartyDescription,
                isfNo = _CurrentBLInfo.ISFNo,
                aciEntryType = _CurrentBLInfo.ACIEntryType,
                amsEntryType = _CurrentBLInfo.AMSEntry,
                mblUpdateDate = _CurrentBLInfo.MBLUpdateDate,
                ETA = _CurrentBLInfo.ETA,
                ETD = _CurrentBLInfo.ETD,
                PreETD = _CurrentBLInfo.PreETD
            };

            return parameter;
        }

        SaveBLContainerParameter ConvertCtnToParameter(bool IsSaveAs, Guid oceanBookingID, List<OceanBLContainerList> list)
        {

            List<OceanBLContainerList> ulist = new List<OceanBLContainerList>();
            //if (!Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ID))
            //{
            //    oeService.GetOceanHBLContainerList(_CurrentBLInfo.ID);
            //}

            if (_ctnList == null || _ctnList.Count <= 0) return null;

            List<bool> p_relations = new List<bool>();
            List<Guid> p_shippingOrderID = new List<Guid>(), p_containerTypeIDs = new List<Guid>();
            List<string> p_containerNos = new List<string>(), p_containerSOs = new List<string>(), p_containerSealNos = new List<string>(),
                p_containerMarks = new List<string>(), p_containerCommoditys = new List<string>();
            List<bool> p_containerIsSOCs = new List<bool>(), p_containerIsPartOfs = new List<bool>();
            List<int> p_containerQuantitys = new List<int>();
            List<decimal> p_containerWeights = new List<decimal>(), p_containerMeasurements = new List<decimal>();

            List<Guid?> p_ids = new List<Guid?>(), p_cargoIDs = new List<Guid?>();
            List<DateTime?> p_updateDates = new List<DateTime?>(), p_cargoUpadateDates = new List<DateTime?>();

            foreach (var item in _ctnList)
            {
                p_ids.Add(item.ID);
                if (IsSaveAs)
                {
                    p_cargoIDs.Add(Guid.Empty);
                }
                else
                {
                    p_cargoIDs.Add(item.CargoID);
                }

                p_relations.Add(item.Relation);
                p_containerNos.Add(item.No);
                p_containerSOs.Add(item.ShippingOrderNo);
                p_containerTypeIDs.Add(item.TypeID);
                p_containerSealNos.Add(item.SealNo);
                p_containerMarks.Add(item.Marks);
                p_containerCommoditys.Add(item.Commodity);
                p_containerQuantitys.Add(item.Quantity);
                p_containerWeights.Add(item.Weight);
                p_containerMeasurements.Add(item.Measurement);
                p_containerIsSOCs.Add(item.IsSOC);
                p_containerIsPartOfs.Add(item.IsPartOf);

                p_cargoUpadateDates.Add(item.CargoUpdateDate);

                OceanBLContainerList cl = ulist.Find(o => o.ID == item.ID);
                if (cl != null)
                {
                    DateTime? dt = cl.UpdateDate;
                    if (cl.UpdateDate.HasValue)
                    {
                        dt = DateTime.SpecifyKind(cl.UpdateDate.Value, DateTimeKind.Unspecified);
                    }
                    p_updateDates.Add(dt);
                }
                else
                {
                    DateTime? dt = item.UpdateDate;
                    if (item.UpdateDate.HasValue)
                    {
                        dt = DateTime.SpecifyKind(item.UpdateDate.Value, DateTimeKind.Unspecified);
                    }
                    p_updateDates.Add(dt);
                }

            }

            SaveBLContainerParameter parameter = new SaveBLContainerParameter
            {
                oceanBookingID = oceanBookingID,
                blID = Guid.Empty,
                relations = p_relations.ToArray(),
                ids = p_ids.ToArray(),
                cargoIds = p_cargoIDs.ToArray(),
                containerNos = p_containerNos.ToArray(),
                containerSOs = p_containerSOs.ToArray(),
                containerTypeIDs = p_containerTypeIDs.ToArray(),
                containerSealNos = p_containerSealNos.ToArray(),
                containerMarks = p_containerMarks.ToArray(),
                containerCommoditys = p_containerCommoditys.ToArray(),
                containerQuantitys = p_containerQuantitys.ToArray(),
                containerWeights = p_containerWeights.ToArray(),
                containerMeasurements = p_containerMeasurements.ToArray(),
                containerIsSOCs = p_containerIsSOCs.ToArray(),
                containerIsPartOfs = p_containerIsPartOfs.ToArray(),
                saveByID = LocalData.UserInfo.LoginID,
                updateDates = p_updateDates.ToArray(),
                cargoUpdateDates = p_cargoUpadateDates.ToArray()
            };

            return parameter;
        }

        #endregion

        #region AMS No


        private void txtNo_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNo.Text.Trim()))
            {
                if (_bookingInfo != null)
                {
                    if (!string.IsNullOrEmpty(_bookingInfo.ShippingLineName))
                    {
                        if (arrAMSCTYO.Contains(_bookingInfo.ShippingLineName))
                        {
                            _CurrentBLInfo.AMSNo = "CTYO" + txtNo.Text.Trim();
                            return;
                        }

                        if (arrAMS8FH5.Contains(_bookingInfo.ShippingLineName))
                        {
                            _CurrentBLInfo.AMSNo = "8FH5" + txtNo.Text.Trim();
                            return;
                        }

                        else
                        {
                            _CurrentBLInfo.AMSNo = txtNo.Text.Trim();
                        }
                    }
                    else
                    {
                        _CurrentBLInfo.AMSNo = txtNo.Text;
                    }
                }
            }
        }

        #endregion

    }
}
