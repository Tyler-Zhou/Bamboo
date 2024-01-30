using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanExport.UI.Common;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.Service;
using ICP.FAM.ServiceInterface;
using ICP.OA.ServiceInterface.DataObjects;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.EDI.ServiceInterface;
using System.Linq;
using ICP.FCM.Common.UI;

namespace ICP.FCM.OceanExport.UI.MBL
{
    [ToolboxItem(false)]
    public partial class MBLEditPart : BaseEditPart
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
        public ICP.Common.ServiceInterface.ITransportFoundationService tfService { get; set; }


        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService fcmCommonService { get; set; }

        [ServiceDependency]
        public ICP.FCM.Common.ServiceInterface.IFCMCommonClientService fcmCommonClientService { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.ICustomerService customerService { get; set; }

        [ServiceDependency]
        public IOceanExportService oeService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public ICPCommUIHelper ICPCommUIHelper { get; set; }

        [ServiceDependency]
        public ICP.Common.ServiceInterface.IConfigureService configureService { get; set; }

        [Dependency(NotPresentBehavior = NotPresentBehavior.CreateNew)]
        public OceanExportPrintHelper OceanExportPrintHelper { get; set; }

        [ServiceDependency]
        public IFinanceClientService finClientService { get; set; }

        [ServiceDependency]
        public IEDIClientService ediClientService
        {
            get;
            set;
        }


        // private VoyageDateInfoHelper voyageDateHelper = null;

        #endregion

        #region Init

        public MBLEditPart()
        {
            InitializeComponent();
            if (!LocalData.IsDesignMode)
            {
                if (LocalData.IsEnglish == false) SetCnText();
                // voyageDateHelper = new VoyageDateInfoHelper();
                // voyageDateHelper.Init(VoyageFormType.MBL, stxtPreVoyage, stxtVoyage, stxtPlaceOfReceipt, txtPOLCode, txtPODCode, dtPETD, dtETD, dtETA, null, null, null, chkShowPreVoyage, chkShowVoyage);
                SetNullValuePrompt();
                this.Disposed += delegate
                {
                    if (_CurrentBLInfo != null) { _CurrentBLInfo = null; }

                    if (Workitem != null) Workitem.Items.Remove(this);
                };

                if (Workitem != null) Workitem.Items.Remove(this);
            };

            //  stxtVoyage.EditValueChanged += new EventHandler(stxtVoyage_EditValueChanged);
            // stxtPreVoyage.EditValueChanged += new EventHandler(stxtPreVoyage_EditValueChanged);
        }

        //void stxtPreVoyage_EditValueChanged(object sender, EventArgs e)
        //{
        //    _CurrentBLInfo.IsDirty = true;
        //}

        //void stxtVoyage_EditValueChanged(object sender, EventArgs e)
        //{
        //    _CurrentBLInfo.IsDirty = true;
        //}

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
            labAMSInfo.Text = "AMS信息";
            labChecker.Text = "对单人";
            labConsignee.Text = "收货人";
            labContainerInfo.Text = "集装箱号";
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
            labType.Text = "类型";
            labCtnQtyInfo.Text = "集装箱或件数合计";
            labDescriptionOfGoods.Text = "包装种类或货名";
            labContractNo.Text = "合约号";
            chkIsWoodPacking.Text = "木质包装";

            barSubCheck.Caption = "对单";
            barCheck.Caption = "申请(&K)";
            barCheckDone.Caption = "完成(&D)";
            barRefresh.Caption = "刷新(&R)";

            barSave.Caption = "保存(&S)";
            barSaveAs.Caption = "另存为(&A)";

            barClose.Caption = "关闭(&C)";


            barSubPrint.Caption = "打印";
            barPrintBL.Caption = "打印提单";
            barPrintLoadGoods.Caption = "打印装货单";

            barReplyAgent.Caption = "申请代理(&R)";

            btnContainer.Text = "箱信息";

            navBarBaseInfo.Caption = "基本信息";
            navBarBLInfo.Caption = "提单信息";
            navBarCargo.Caption = "货物信息";
            navBarIssueInfo.Caption = "签发信息";

            labMBLNo.Text = "主提单号";
            labHBLNO.Text = "分提单号";

            chkShowVoyage.Text = chkShowPreVoyage.Text = "显示";

        }
        private void SetContractBoxByHasContract(bool hasContract)
        {
            this.txtContractNo.Enabled = hasContract;
            this.txtContractNo.BackColor = hasContract ? SystemColors.Info : this.txtNo.BackColor;
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
                stxtChecker,
                stxtIssuePlace,
                stxtNotifyParty ,
            });
            Utility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtFinalDestination,
                stxtPlaceOfDelivery ,
                stxtPlaceOfReceipt ,
                txtPOLCode,
                txtPODCode,
            });
            //Utility.SetVoyageTextEditNullValuePrompt(new List<TextEdit>
            //{
            //    stxtPreVoyage ,
            //    stxtVoyage ,
            //});

            Utility.SetTextEditNullValuePrompt(new List<TextEdit> { stxtRefNo }
                , LocalData.IsEnglish ? "Please Input Operation NO." : "请输入业务号.");


            string tip = LocalData.IsEnglish ? "Un Done" :
                        "此栏目链接于订舱单,保存时修改的内容将会更新到订舱单中.";

            stxtPlaceOfReceipt.ToolTip = txtPOLCode.ToolTip = txtPODCode.ToolTip = txtPlaceOfDeliveryName.ToolTip =
            stxtPreVoyage.ToolTip = stxtVoyage.ToolTip = tip;


        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                this.SmartPartClosing += new EventHandler<Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs>(MBLEditPart_SmartPartClosing);
                this.ActivateSmartPartClosingEvent(this.Workitem);
                _CurrentBLInfo.CancelEdit();
                _CurrentBLInfo.BeginEdit();
                //SetContractBoxByHasContract(_CurrentBLInfo.is
            }
        }

        #endregion

        #region 本地变量

        #region 判断是否需要生成账单的参数

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

        /// <summary>
        /// 当前业务是否有箱列表
        /// </summary>
        private bool isHasContainer = false;

        /// <summary>
        /// 是否为该业务的第一个MBL
        /// </summary>
        private bool isFirstMBL = false;

        #endregion

        /// <summary>
        /// 主体数据
        /// </summary>
        OceanMBLInfo _CurrentBLInfo = null;

        OceanBookingInfo _bookingInfo = null;

        /// <summary>
        /// 箱数据
        /// </summary>
        List<OceanBLContainerList> _ctnList = null;

        /// <summary>
        /// 代理下拉数据源
        /// </summary>
        List<CustomerList> _agentCustomersList = null;

        /// <summary>
        /// 订阅箱面板的改变事件.来判断箱是否有改变.用于在保存时判断是否需要保存箱
        /// </summary>
        bool isChangedCtnList = false;
        /// <summary>
        /// 是否已根据bookingContainerDescription生成过箱,控制只生成一次.
        /// </summary>
        bool isInitBookingContainerDescription = false;
        /// <summary>
        /// Booking的箱描述.在新单时用来生成箱的
        /// </summary>
        ContainerDescription bookingContainerDescription;
        /// <summary>
        /// 标记是否保存已经保存了MBL
        /// </summary>
        bool isSave = false;

        /// <summary>
        /// 控制只获取一次.请用使用CtnLists属性
        /// </summary>
        List<ContainerList> ctnTypes = null;
        List<ContainerList> CtnTypes
        {
            get
            {
                if (ctnTypes != null) return ctnTypes;

                ctnTypes = tfService.GetContainerList(string.Empty, true, 0);
                return ctnTypes;
            }
        }

        /// <summary>
        /// 是否需要申请代理
        /// </summary>
        bool _isNeedRequestAgent = false;

        /// <summary>
        /// 是否需要保存前自动申请代理
        /// </summary>
        bool _isNeedAutoRequestAgent = false;

        /// <summary>
        /// 原始提单号
        /// </summary>
        string _originalMBLNO = string.Empty;

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

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.NotifyPartyID) && _CurrentBLInfo.NotifyPartyDescription == null)
                txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";
            else
            {
                txtNotifyPartyDescription.Text = _CurrentBLInfo.NotifyPartyDescription.ToString(LocalData.IsEnglish);
                if (txtNotifyPartyDescription.Text.Length == 0) txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";
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
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            this.panelScroll.Click += delegate { panelScroll.Focus(); };

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ID)) _CurrentBLInfo.Marks = @"N/M";

            SetComboboxEnumSource();
            SetComboboxSource();
            RefreshControlsByData();
            SearchRegister();

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

            stxtRefNo.Focus();
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
                //_CurrentBLInfo.WoodPacking = txtWoodPacking.Text = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
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

            if (_CurrentBLInfo.IsRequestAgent) stxtAgent.Enabled = false;


            #region WoodPacking
            bool isInitWoodPacking = _CurrentBLInfo.IsNew || string.IsNullOrEmpty(_CurrentBLInfo.WoodPacking) ? true : false;
            chkIsWoodPacking.CheckedChanged += delegate
            {
                if (isInitWoodPacking)
                {
                    if (chkIsWoodPacking.Checked)
                        _CurrentBLInfo.WoodPacking = txtWoodPacking.Text = "WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT AND HAS BEEN FUMIGATED";
                    else
                        _CurrentBLInfo.WoodPacking = txtWoodPacking.Text = "NO WOOD PACKAGING MATERIAL IS USED IN THE SHIPMENT";
                }
                isInitWoodPacking = true;
            };
            #endregion

            //if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID))
            //    btnContainer.Enabled = false;

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
            cmbIssueType.Properties.BeginUpdate();
            foreach (var item in issueTypes)
            {
                if (item.Value == 0) continue;
                cmbIssueType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbIssueType.Properties.EndUpdate();

            //放单类型
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<FCMReleaseType>> releaseTypes = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<FCMReleaseType>(LocalData.IsEnglish);
            cmbReleaseType.Properties.BeginUpdate();
            foreach (var item in releaseTypes)
            {
                if (item.Value == 0) continue;
                cmbReleaseType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbReleaseType.Properties.EndUpdate();
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
                if (_countryList == null) _countryList = geographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);

                foreach (CountryList c in _countryList)
                {
                    stxtAgent.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }

                SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);
                stxtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
                stxtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
            });
            #endregion

            #region Voyage
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

                //stxtVoyage.popEdit1.EditValue = string.Empty;
                //_CurrentBLInfo.VoyageID = null;
                //_CurrentBLInfo.VesselVoyage = string.Empty;
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

                //stxtPreVoyage.popEdit1.EditValue = string.Empty;
                //_CurrentBLInfo.PreVoyageID = null;
                //_CurrentBLInfo.PreVesselVoyage = string.Empty;
                chkShowPreVoyage.Checked = false;
            }
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
                      if (_CurrentBLInfo.OceanBookingID != bookingID)
                      {
                          AfterSearchRefNo(bookingID);
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
                      if (_CurrentBLInfo.IsNew)
                      {
                          _CurrentBLInfo.OceanBookingID = Guid.Empty;
                          stxtRefNo.Text = _CurrentBLInfo.RefNo = string.Empty;
                          btnContainer.Enabled = false;
                          _ctnList = null;
                          containerListPart = null;
                      }

                  }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);

            #endregion

            #region Customer

            #region SCNA

            //shipper
            Utility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = geographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);
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
                if (_countryList == null) _countryList = geographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);
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
                if (_countryList == null) _countryList = geographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);

                CustomerFinderBridge notifyPartyBridge = new CustomerFinderBridge(
                 this.stxtNotifyParty,
                 _countryList,
                 this.dfService,
                 this.customerService,
                 _CurrentBLInfo.NotifyPartyDescription,
                 this.txtNotifyPartyDescription,
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
                              txtPlaceOfDeliveryName.Text = _CurrentBLInfo.PlaceOfDeliveryName = _CurrentBLInfo.PODName;
                              stxtPlaceOfDelivery.Text = _CurrentBLInfo.PlaceOfDeliveryCode = _CurrentBLInfo.PODCode;
                              stxtPlaceOfDelivery.Tag = _CurrentBLInfo.PlaceOfDeliveryID = portID;

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

            #endregion
        }

        void stxtNotifyParty_OnOk(object sender, EventArgs e)
        {
            CustomerDescription des = stxtNotifyParty.CustomerDescription;

            if (des == null)
            {
                des = new CustomerDescription();
            }
            _CurrentBLInfo.NotifyPartyDescription = des;
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            CustomerDescription des = stxtConsignee.CustomerDescription;
            if (des == null)
            {
                des = new CustomerDescription();

            }
            _CurrentBLInfo.ConsigneeDescription = des;
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            CustomerDescription des = stxtShipper.CustomerDescription;
            if (des == null)
            {
                des = new CustomerDescription();

            }
            _CurrentBLInfo.ShipperDescription = des;
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
            CustomerDescription des = stxtAgent.CustomerDescription;
            if (des == null)
            {
                des = new CustomerDescription();
            }
            _CurrentBLInfo.AgentDescription = des;

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
                stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription;
                ICPCommUIHelper.SetCustomerDesByID(id, _CurrentBLInfo.AgentDescription);
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
            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID) == false && _CurrentBLInfo.OceanBookingID != bookingID)
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

            _bookingInfo = oeService.GetOceanBookingInfo(bookingID);
            _CurrentBLInfo.CustomerName = _bookingInfo.CustomerName;


            _CurrentBLInfo.ContractID = _bookingInfo.ContractID;
            _CurrentBLInfo.ContractNo = _bookingInfo.ContractNo;
            _CurrentBLInfo.IsChargePayOrCon = false;

            _CurrentBLInfo.CompanyID = _bookingInfo.CompanyID;
            if (!Utility.GuidIsNullOrEmpty(_bookingInfo.CarrierID))
            {
                _CurrentBLInfo.CarrierID = _bookingInfo.CarrierID.Value;
            }
            stxtRefNo.Text = _CurrentBLInfo.RefNo = _bookingInfo.No;
            stxtRefNo.Tag = _CurrentBLInfo.OceanBookingID = _bookingInfo.ID;

            _CurrentBLInfo.AgentOfCarrierName = _bookingInfo.AgentOfCarrierName;
            _CurrentBLInfo.SONO = _bookingInfo.OceanShippingOrderNo;
            _CurrentBLInfo.CarrierName = _bookingInfo.CarrierName;
            _CurrentBLInfo.SalesName = _bookingInfo.SalesName;
            _CurrentBLInfo.FilerName = _bookingInfo.FilerName;
            _CurrentBLInfo.BookingerName = _bookingInfo.BookingerName;
            _CurrentBLInfo.OverseasFilerName = _bookingInfo.OverSeasFilerName;

            #region 收发通,代理

            #region Agent  MBL.代理 = 订舱单.代理

            SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);

            _CurrentBLInfo.AgentID = _bookingInfo.AgentID;
            _CurrentBLInfo.AgentName = _bookingInfo.AgentName;
            _CurrentBLInfo.AgentDescription = _bookingInfo.AgentDescription;
            //SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID); 

            #endregion

            #region Shipper  MBL.发货人 = 订舱单.发货人

            //只出MBL
            //	MBL.发货人 = 订舱单.发货人,MBL.发货人描述 = 根据MBL.发货人生成
            //	MBL.收货人 = 订舱单.收货人,MBL.收货人描述 = 根据MBL.收货人生成
            if (_bookingInfo.IsOnlyMBL)
            {
                #region 发货人
                _CurrentBLInfo.ShipperID = _bookingInfo.ShipperID == null ? Guid.Empty : _bookingInfo.ShipperID.Value;
                _CurrentBLInfo.ShipperName = _bookingInfo.ShipperName;
                stxtShipper.CustomerDescription = _CurrentBLInfo.ShipperDescription = _bookingInfo.ShipperDescription;
                if (_CurrentBLInfo.ShipperDescription != null)
                    txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
                #endregion

                #region 收货人
                _CurrentBLInfo.ConsigneeID = _bookingInfo.ConsigneeID;
                _CurrentBLInfo.ConsigneeName = _bookingInfo.ConsigneeName;
                stxtConsignee.CustomerDescription = _CurrentBLInfo.ConsigneeDescription = _bookingInfo.ConsigneeDescription;
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
                ConfigureInfo configureInfo = configureService.GetCompanyConfigureInfo(_bookingInfo.CompanyID);

                _CurrentBLInfo.ShipperID = configureInfo.CustomerID;
                _CurrentBLInfo.ShipperName = configureInfo.CustomerName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentBLInfo.ShipperID, _CurrentBLInfo.ShipperDescription);
                if (_CurrentBLInfo.ShipperDescription != null)
                    txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
                #endregion

                #region 收货人
                _CurrentBLInfo.ConsigneeID = _bookingInfo.AgentID;
                _CurrentBLInfo.ConsigneeName = _bookingInfo.AgentName;
                stxtConsignee.CustomerDescription = _CurrentBLInfo.ConsigneeDescription = _bookingInfo.AgentDescription;
                if (_CurrentBLInfo.ConsigneeDescription != null)
                    txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);
                #endregion

            }

            #endregion

            #region NotifyParty = 通知人描述 = “SAME AS CONSIGNEE”

            _CurrentBLInfo.NotifyPartyID = Guid.Empty;
            _CurrentBLInfo.NotifyPartyName = string.Empty;
            stxtNotifyParty.CustomerDescription = _CurrentBLInfo.NotifyPartyDescription = new CustomerDescription();
            txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";

            #endregion

            #endregion

            #region Port

            //	MBL.收货地描述 = 订舱单.收货地.名称 
            //	MBL.装货港描述 = 订舱单. 装货港.名称 
            //	MBL.卸货港描述 = 订舱单. 卸货港.名称 
            //	MBL.交货地描述 = 订舱单. 交货地.名称 
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

            #region Voyage, PreVoyage, ETD, ETA

            _CurrentBLInfo.PreVoyageID = _bookingInfo.PreVoyageID;
            _CurrentBLInfo.PreVesselVoyage = _bookingInfo.PreVoyageName;
           
            _CurrentBLInfo.VoyageID = _bookingInfo.VoyageID;
            _CurrentBLInfo.VesselVoyage = _bookingInfo.VoyageName;
          
            _CurrentBLInfo.ETD = _bookingInfo.ETD;
            _CurrentBLInfo.ETA = _bookingInfo.ETA;
            OceanExportPrintHelper.SetPrintCheckByVoyageType(_CurrentBLInfo.PreVoyageID, _CurrentBLInfo.VoyageID, chkShowPreVoyage, chkShowVoyage);

            if (_bookingInfo.MBLReleaseType != null && _bookingInfo.MBLReleaseType.Value != FCMReleaseType.Unknown)
                _CurrentBLInfo.ReleaseType = _bookingInfo.MBLReleaseType.Value;

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


            _CurrentBLInfo.BookingPaymentTermID = _CurrentBLInfo.TransportClauseID = _bookingInfo.TransportClauseID;
            _CurrentBLInfo.TransportClauseName = _bookingInfo.TransportClauseName;
            cmbTransportClause.ShowSelectedValue(_CurrentBLInfo.TransportClauseID, _CurrentBLInfo.TransportClauseName);


            cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
            _CurrentBLInfo.BookingPaymentTermID = _CurrentBLInfo.PaymentTermID = _bookingInfo.MBLPaymentTermID;
            cmbPaymentTerm.Text = _CurrentBLInfo.PaymentTermName = _bookingInfo.MBLPaymentTermName;
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

            if (_bookingInfo.BookingContainers != null && _bookingInfo.BookingContainers.Count > 0)
            {
                isHasContainer = true;
            }
            if (_bookingInfo.OceanMBLs == null || _bookingInfo.OceanMBLs.Count == 0)
            {
                isFirstMBL = true;
            }

            _CurrentBLInfo.CarrierName = _bookingInfo.CarrierName;

            //设置
            SetShipperColor(_bookingInfo.ShippingLineID, _bookingInfo.ShippingLineID);

            bookingContainerDescription = _bookingInfo.ContainerDescription;
            RefreshEnabledByBookingType(_bookingInfo.OEOperationType);

            bindingSource1.EndEdit();
            #endregion

            barReplyAgent.Enabled = true;
        }
        /// <summary>
        /// 设置发货人是否必填
        /// </summary>
        /// <param name="shippingLineID"></param>
        /// <param name="shipperID"></param>
        private void SetShipperColor(Guid? shippingLineID, Guid? shipperID)
        {
            //美加线，Shipper要必填,其他情况下要必填
            if (!Utility.GuidIsNullOrEmpty(shippingLineID) && !Utility.GuidIsNullOrEmpty(shipperID) && Utility.NAShippingLines.Contains(shipperID.Value))
            {
                stxtShipper.Properties.Appearance.BackColor = System.Drawing.SystemColors.Info;
            }
            else
            {
                stxtShipper.Properties.Appearance.BackColor = Color.White;
            }
        }

        #endregion

        #region Voyage Changed
        //到港日ETA跟大船,离港日ETD如果有驳船就跟驳船

        /// <summary>
        /// 大船改变，填充ETD，如果没有驳船，填充ETA， 截柜日，截关日,截文件日
        /// </summary>
        //private void VoyageChanged()
        //{
        //    if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID))
        //    {
        //        chkShowVoyage.Checked = chkShowVoyage.Enabled = false;

        //        if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
        //        {
        //            dteETD.EditValue = _CurrentBLInfo.ETD = null;
        //        }
        //        dteETA.EditValue = _CurrentBLInfo.ETA = null;

        //    }
        //    else
        //    {
        //        VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.VoyageID.Value);
        //        dteETA.EditValue = _CurrentBLInfo.ETA = voyageInfo.ETA;
        //        if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
        //        {
        //            dteETD.EditValue = _CurrentBLInfo.ETD = voyageInfo.ETD;

        //        }

        //        if (string.IsNullOrEmpty(_CurrentBLInfo.PreVesselVoyage))
        //        {
        //            chkShowVoyage.Checked = chkShowVoyage.Enabled = true;
        //        }
        //        else
        //        {
        //            chkShowPreVoyage.Checked = chkShowPreVoyage.Enabled = true;
        //            chkShowVoyage.Checked = false; chkShowVoyage.Enabled = true;
        //        }
        //    }
        //}

        /// <summary>
        /// 驳船改变,填充ETD， 截柜日，截关日,截文件日  如果海运订舱单中有驳船就是驳船的离港日，否则海运订舱单.大船.离港日
        ///// </summary>
        //private void PreVoyageChanged()
        //{
        //    if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
        //    {
        //        chkShowPreVoyage.Checked = chkShowPreVoyage.Enabled = false;
        //        dteETD.EditValue = _CurrentBLInfo.ETD = null;
        //        if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false)
        //        {
        //            VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.VoyageID.Value);
        //            dteETD.EditValue = _CurrentBLInfo.ETD = voyageInfo.ETD;
        //        }
        //    }
        //    else
        //    {
        //        VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.PreVoyageID.Value);
        //        dteETD.EditValue = _CurrentBLInfo.ETD = voyageInfo.ETD;

        //        chkShowPreVoyage.Checked = chkShowPreVoyage.Enabled = true;
        //        chkShowVoyage.Checked = false; chkShowVoyage.Enabled = true;
        //    }
        //}

        #endregion


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
                            //_isNeedRequestAgent = true;
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

        #region 船名航次

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
        #endregion

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
                barCheck.Enabled = barCheckDone.Enabled = barE_MBL.Enabled = barSubCheck.Enabled
                    = barPrintBL.Enabled = barReplyAgent.Enabled = barSaveAs.Enabled = barSave.Enabled = false;
            }
            else if (_CurrentBLInfo.IsNew)
            {
                barCheck.Enabled = barCheckDone.Enabled = barE_MBL.Enabled =
                   barPrintBL.Enabled = barSaveAs.Enabled = barReplyAgent.Enabled = false;
            }
            else
            {
                barPrintBL.Enabled = barRefresh.Enabled = barSaveAs.Enabled = barReplyAgent.Enabled = barE_MBL.Enabled = true;

                if (string.IsNullOrEmpty(_CurrentBLInfo.HBLNos) && string.IsNullOrEmpty(_CurrentBLInfo.ContainerNos) == false)
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

            if (ValidateData(false) == false) return false;

            #region MBLNo已经存在，提示是否继续保存
            if (oeService.IsExistsMBLNo(_CurrentBLInfo.MBLID, _CurrentBLInfo.No))
            {
                string message = string.Format(NativeLanguageService.GetText(this, "IsExisteMBLNo"), _CurrentBLInfo.No);

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
            #endregion

            if (_CurrentBLInfo.IsDirty == false && _CurrentBLInfo.IsNew == false && isChangedCtnList == false) return false;
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Saveing......" : "保存中.....");
            barSave.Enabled = false;

            try
            {
                if (!AutoRequestAgent()) return false;
                bool isNew = false;

                if (ctnIDList.Count > 0 || isChangedCtnList == true)
                {
                    #region 保存MBL、箱信息
                    SaveMBLInfoParameter mbl = ConvertMBLToParameter(false, _CurrentBLInfo);
                    SaveBLContainerParameter ctn = ConvertCtnToParameter(false, _CurrentBLInfo.OceanBookingID, _ctnList);

                    SingleResult result = oeService.SaveOceanMBLAndContainerWithTrans(mbl, ctn, ctnIDList, ctnUpdateDateList, true);

                    SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                    ManyResult ctnResult = result.GetValue<ManyResult>("ContainerResult");

                    #endregion

                    #region  处理返回值
                    _CurrentBLInfo.MBLID = _CurrentBLInfo.ID = blResult.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = blResult.GetValue<DateTime?>("UpdateDate");

                    isSave = true;
                    isHasContainer = true;
                    _CurrentBLInfo.IsDirty = false;

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
                    #endregion
                }
                else
                {
                    #region 只保存MBL，没有箱或者箱没有发生改变时
                    SaveMBLInfoParameter mbl = ConvertMBLToParameter(false, _CurrentBLInfo);
                    SingleResult result = oeService.SaveOceanMBLInfo(mbl);

                    isNew = Utility.GuidIsNullOrEmpty(_CurrentBLInfo.MBLID);

                    _CurrentBLInfo.MBLID = _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    isSave = true;
                    _CurrentBLInfo.IsDirty = false;
                    #endregion
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                #region 重新生成账单
                if (isNew && isFirstMBL)
                {
                    #region 新的MBL,判断是否为这个业务的第一个MBL
                    if (isFirstMBL && !Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ContractID) && isHasContainer)
                    {
                        //isFirstMBL 是第一个MBL
                        //ContractID 不为空
                        //isHasContainer 有箱列表
                        try
                        {
                            SingleResult result = oeService.CreateBill(_CurrentBLInfo.OceanBookingID, LocalData.UserInfo.LoginID);

                            if (result != null)
                            {
                                int s = result.GetValue<Byte>("State");
                                if (s == 1)
                                {
                                    Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBills"));

                                    _CurrentBLInfo.IsChargePayOrCon = false;
                                    isCtnCharge = false;
                                }
                                else if (s == 2)
                                {
                                    string message = result.GetValue<string>("ErrorMessage");
                                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                        }
                    }
                    #endregion
                }
                else
                {
                    if (isHasContainer && _CurrentBLInfo.IsHasContract)
                    {
                        //编辑MBL时，如果付款方式、合约、箱信息有改变,则重新生成账单
                        if (_CurrentBLInfo.IsChargePayOrCon || isCtnCharge)
                        {
                            try
                            {
                                SingleResult result = oeService.CreateBill(_CurrentBLInfo.OceanBookingID, LocalData.UserInfo.LoginID);

                                if (result != null)
                                {

                                    int s = result.GetValue<Byte>("State");
                                    if (s == 1)
                                    {
                                        if (_CurrentBLInfo.IsChargePayOrCon && isCtnCharge)
                                        { Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBillsForALL")); }
                                        else if (isCtnCharge)
                                        { Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBillsForContainer")); }
                                        else if (_CurrentBLInfo.IsChargePayOrCon)
                                        { Utility.ShowMessage(NativeLanguageService.GetText(this, "CreateBillsForPayORCon")); }
                                        //Utility.ShowMessage(_message);

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
                }
                #endregion

                ctnIDList = new List<Guid>();
                ctnUpdateDateList = new List<DateTime?>();

                AfterSave(false);

                RefreshCustomerDesc();

                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
            finally { barSave.Enabled = true; }
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

        void AfterSave(bool IsSaveAs)
        {
            isChangedCtnList = false;

            if (containerListPart != null)
            {
                containerListPart.MBLID = _CurrentBLInfo.ID;
                containerListPart.ShippingOrderID = _CurrentBLInfo.OceanBookingID;
            }



            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
            stxtRefNo.Properties.ReadOnly = true;
            stxtRefNo.Properties.Buttons[0].Enabled = false;
            this.Title = LocalData.IsEnglish ? "Edit MBL " + _CurrentBLInfo.No : "编辑MBL " + _CurrentBLInfo.No;
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
            string errorInfo = string.Empty;
            if (_CurrentBLInfo.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (_CurrentBLInfo.POLID != Guid.Empty && _CurrentBLInfo.POLID == _CurrentBLInfo.PODID)
                        {
                            e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                        }
                        if (string.IsNullOrEmpty(_CurrentBLInfo.No))
                        {
                            e.SetErrorInfo("No", LocalData.IsEnglish ? "MBL NO Must Input" : "MBL NO 必须输入.");
                        }
                        if (_CurrentBLInfo.ShippingLineID != null && Utility.NAShippingLines.Contains(_CurrentBLInfo.ShippingLineID.Value) && Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ShipperID))
                        {
                            e.SetErrorInfo("ShipperID", LocalData.IsEnglish ? "Shipper Must Input" : "收货人必须输入.");
                        }


                        //else if (IsSaveAs && string.IsNullOrEmpty(_originalMBLNO) == false && _CurrentBLInfo.No.Trim() == _originalMBLNO.Trim())
                        //    e.SetErrorInfo("No", LocalData.IsEnglish ? "Input a new MBL NO please." : "请输入一个新的MBL NO.");
                        //isScrr = voyageDateHelper.ValidateDateInfo();


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

                if (_ctnList == null) _ctnList = oeService.GetOceanMBLContainerList(_CurrentBLInfo.ID);
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

                //txtCtnInfo.Text = _CurrentBLInfo.ContainerDescription = string.Empty;
                if (isSaveCtn == false)
                {
                    SaveMBLInfoParameter mbl = ConvertMBLToParameter(true, _CurrentBLInfo);
                    SingleResult result = oeService.SaveOceanMBLInfo(mbl);
                    _CurrentBLInfo.MBLID = _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    txtCtnInfo.Text = _CurrentBLInfo.ContainerDescription = string.Empty;
                }
                else
                {
                    SaveMBLInfoParameter mbl = ConvertMBLToParameter(true, _CurrentBLInfo);
                    SaveBLContainerParameter ctn = ConvertCtnToParameter(true, _CurrentBLInfo.OceanBookingID, _ctnList);
                    SingleResult result = oeService.SaveOceanMBLAndContainerWithTrans(mbl, ctn, null, null, true);

                    SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                    ManyResult ctnResult = result.GetValue<ManyResult>("ContainerResult");

                    #region  处理返回值
                    _CurrentBLInfo.MBLID = _CurrentBLInfo.ID = blResult.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = blResult.GetValue<DateTime?>("UpdateDate");

                    _CurrentBLInfo.CreateDate = DateTime.Now;
                    _CurrentBLInfo.CreateByID = LocalData.UserInfo.LoginID;
                    _CurrentBLInfo.CreateByName = LocalData.UserInfo.LoginName;

                    for (int i = 0; i < ctnResult.Items.Count; i++)
                    {
                        _ctnList[i].ID = ctnResult.Items[i].GetValue<Guid>("ID");
                        _ctnList[i].CargoID = ctnResult.Items[i].GetValue<Guid?>("CargoID");

                        _ctnList[i].UpdateDate = ctnResult.Items[i].GetValue<DateTime?>("UpdateDate");
                        _ctnList[i].CargoUpdateDate = ctnResult.Items[i].GetValue<DateTime?>("CargoUpdateDate");
                    }
                    #endregion
                }
                AfterSave(true);

                return true;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); return false; }
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
                        _ctnList = oeService.GetOceanMBLContainerList(_CurrentBLInfo.ID);
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
                                        newCtn.Quantity = 1;
                                        newCtn.Measurement = 0m;
                                        newCtn.TypeName = ctn.Type;
                                        newCtn.Weight = ctn.Weight ?? 0m;
                                        newCtn.Relation = true;
                                        newCtn.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                                        newCtn.CreateByID = LocalData.UserInfo.LoginID;
                                        newCtn.CreateByName = LocalData.UserInfo.LoginName;
                                        ContainerList tager = CtnTypes.Find(delegate(ContainerList item) { return item.Code == (ctn.Size + ctn.Type); });
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
                        isChangedCtnList = true;

                        #region 确定箱后更新页面信息
                        _ctnList = prams[0] as List<OceanBLContainerList>;
                        if (_ctnList != null && _ctnList.Count != 0)
                        {
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


                        }
                        #endregion

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

                    };
                }
                #endregion

                containerListPart.ReadOnly = !string.IsNullOrEmpty(_CurrentBLInfo.HBLNos);
                containerListPart.DataSource = _ctnList;
                containerListPart.IDList = ctnIDList;
                containerListPart.UpdateDateList = ctnUpdateDateList;

                PartLoader.ShowDialog(containerListPart, LocalData.IsEnglish ? "Edit MBLCotainer" : "编辑MBL箱信息", FormBorderStyle.Sizable);

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        }

        #endregion

        #region 关闭

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.FindForm().Close();
        }

        void MBLEditPart_SmartPartClosing(object sender, Microsoft.Practices.CompositeUI.SmartParts.WorkspaceCancelEventArgs e)
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

        private void barPrintLoadGoods_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.IsDirty)
            {
                if (SaveData() == false) return;
            }

            OceanExportPrintHelper.PrintOELoadGoods(_CurrentBLInfo);
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

        private void barCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.State != OEBLState.Draft) return;

            try
            {
                SingleResult result = oeService.ChangeOceanMBLState(_CurrentBLInfo.ID, OEBLState.Checking, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);

                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _CurrentBLInfo.State = OEBLState.Checking;
                _CurrentBLInfo.IsDirty = isDirty;
                RefreshBarEnabled();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Set Check Successfully" : "设置对单成功.");
                if (Saved != null) Saved(new object[] { _CurrentBLInfo });
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            RefreshBarEnabled();
        }

        private void barCheckDone_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty) return;

            try
            {
                SingleResult result = oeService.ChangeOceanMBLState(_CurrentBLInfo.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);

                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _CurrentBLInfo.State = OEBLState.Checking;
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
            bool srcc = fcmCommonClientService.OpenAgentRequestPart(_CurrentBLInfo.ID, ICP.Framework.CommonLibrary.Common.OperationType.OceanExport, this.Workitem);
            if (srcc)
            {
                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.IsRequestAgent = true;
                stxtAgent.Enabled = false;
                _CurrentBLInfo.IsDirty = isDirty;
            }
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

        private void barRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            RefreshBLData();


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
            _CurrentBLInfo = oeService.GetOceanMBLInfo(_CurrentBLInfo.ID);
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
            this.bindingSource1.DataSource = _CurrentBLInfo;
            bindingSource1.ResetBindings(false);
            InitCustomerDescriptionObject();


            RefreshCustomerDesc();

            this.Refresh();

            //}
        }
        /// <summary>
        /// 刷新客户备注信息
        /// </summary>
        private void RefreshCustomerDesc()
        {
            if (_CurrentBLInfo == null)
            {
                return;
            }
            _CurrentBLInfo.IsDirty = false;
            if (_CurrentBLInfo.ShipperDescription != null)
            {
                _CurrentBLInfo.ShipperDescription.IsDirty = false;
            }
            if (_CurrentBLInfo.ConsigneeDescription != null)
            {
                _CurrentBLInfo.ConsigneeDescription.IsDirty = false;
            }
            if (_CurrentBLInfo.AgentDescription != null)
            {
                _CurrentBLInfo.AgentDescription.IsDirty = false;
            }
            if (_CurrentBLInfo.AgentDescription != null)
            {
                _CurrentBLInfo.AgentDescription.IsDirty = false;
            }

        }

        #endregion

        #region IEditPart 成员

        void BindingData(object data)
        {


            _CurrentBLInfo = data as OceanMBLInfo;
            this.bindingSource1.DataSource = _CurrentBLInfo;
            _CurrentBLInfo.IsChargePayOrCon = false;

            isHasContainer = !string.IsNullOrEmpty(_CurrentBLInfo.ContainerNos);
            isFirstMBL = false;

            InitMessage();
            InitControls();
            RefreshBarEnabled();
            InitCustomerDescriptionObject();
            _originalMBLNO = _CurrentBLInfo.No;

            SetShipperColor(_CurrentBLInfo.ShippingLineID, _CurrentBLInfo.ShipperID);
            _CurrentBLInfo.IsDirty = false;


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

            _CurrentBLInfo.VoyageShowType = OceanExportPrintHelper.GetVoyageShowTypeByVoyageCheck(chkShowPreVoyage, chkShowVoyage);
            this.Validate();

            bindingSource1.EndEdit();

        }

        public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;

        #endregion

        #region 把客户端对象转换为服务保存方法所需的对象

        SaveMBLInfoParameter ConvertMBLToParameter(bool IsSaveAs, OceanMBLInfo info)
        {
            DateTime? dt = _CurrentBLInfo.UpdateDate;
            if (_CurrentBLInfo.UpdateDate.HasValue)
            {
                dt = DateTime.SpecifyKind(_CurrentBLInfo.UpdateDate.Value, DateTimeKind.Unspecified);

            }
            if (!this.chkHasContract.Checked)
            {
                _CurrentBLInfo.ContractID = Guid.Empty;
            }

            SaveMBLInfoParameter parameter = new SaveMBLInfoParameter
            {
                id = IsSaveAs ? Guid.Empty : info.ID,
                oceanBookingID = info.OceanBookingID,
                mblNo = info.No,
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
                AgentText = info.AgentText,
                FreightRateID = info.ContractID,
                saveByID = LocalData.UserInfo.LoginID,
                updateDate = IsSaveAs ? null : dt,
                PreETD = info.PreETD,
                ETD = info.ETD,
                ETA = info.ETA
            };

            return parameter;
        }

        SaveBLContainerParameter ConvertCtnToParameter(bool IsSaveAs, Guid oceanBookingID, List<OceanBLContainerList> list)
        {
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
                if (item.UpdateDate.HasValue)
                {
                    item.UpdateDate = DateTime.SpecifyKind(item.UpdateDate.Value, DateTimeKind.Unspecified);
                }
                if (item.CargoUpdateDate.HasValue)
                {
                    item.CargoUpdateDate = DateTime.SpecifyKind(item.CargoUpdateDate.Value, DateTimeKind.Unspecified);
                }

                p_ids.Add(item.ID);
                p_cargoIDs.Add(item.CargoID);
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
                p_updateDates.Add(item.UpdateDate);
                p_cargoUpadateDates.Add(item.CargoUpdateDate);
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

        #region 查找合约
        private void txtContractNo_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 查找合约
        /// </summary>
        private void SelectContract()
        {
            _CurrentBLInfo = this.bindingSource1.DataSource as OceanMBLInfo;
            try
            {

                Guid placeOfReceiptID = Guid.Empty;
                Guid polid = Guid.Empty;
                Guid podid = Guid.Empty;
                Guid? deleveryid = Guid.Empty;
                Guid? finalDestinationid = Guid.Empty;
                if (_CurrentBLInfo.POLID == Guid.Empty || _CurrentBLInfo.POLID == null ||
                    _CurrentBLInfo.PODID == Guid.Empty || _CurrentBLInfo.PODID == null ||
                    _CurrentBLInfo.PlaceOfDeliveryID == Guid.Empty || _CurrentBLInfo.PlaceOfDeliveryID == null)

                { Utility.ShowMessage(LocalData.IsEnglish ? "pol、pod、delivery must  input" : "起运港、目的港、交货地必须填写"); return; }
                else
                {
                    if (!Utility.GuidIsNullOrEmpty(_CurrentBLInfo.PlaceOfReceiptID))
                    {
                        placeOfReceiptID = _CurrentBLInfo.PlaceOfReceiptID.Value;
                    }
                    polid = _CurrentBLInfo.POLID;
                    podid = _CurrentBLInfo.PODID;
                    deleveryid = _CurrentBLInfo.PlaceOfDeliveryID;
                    finalDestinationid = _CurrentBLInfo.FinalDestinationID;
                    
                }
                Cursor = Cursors.WaitCursor;
                FreightDataList freightDataList =
                    oeService.GetFreight(_CurrentBLInfo.ContractNo == "无" ? string.Empty : _CurrentBLInfo.ContractNo,
                    _CurrentBLInfo.CarrierID,
                    placeOfReceiptID,
                    polid,
                    deleveryid,
                    null,
                    string.Empty,
                    new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                    new DateTime(DateTime.Now.AddDays(15).Year, DateTime.Now.AddDays(15).Month, DateTime.Now.AddDays(15).Day),
                    _CurrentBLInfo.ContractID);

                FreightInfo freight = Workitem.Items.AddNew<FreightInfo>();
                     
                    FreightParameter freightParameter = new FreightParameter();
                    freightParameter.prices = freightDataList.DataList;
                    freightParameter.unitList = freightDataList.UnitList;
                    freightParameter.scno = _CurrentBLInfo.ContractNo;
                    freightParameter.shipownerid = _CurrentBLInfo.CarrierID;
                    freightParameter.placeOfReceiptID = placeOfReceiptID;
                    freightParameter.polid = polid;
                    freightParameter.podid = deleveryid;
                    freightParameter.finalDestinationID = finalDestinationid;
                    freightParameter.goodsdes = string.Empty;
                    freightParameter.freightID = _CurrentBLInfo.ContractID;
                    freightParameter.etd = this.dtETD.DateTime;


                    freight.SetDataSource(freightParameter);

                string title = LocalData.IsEnglish ? "Select Price" : "选择运价";
                if (DialogResult.OK == PartLoader.ShowDialog(freight, title, FormBorderStyle.FixedSingle, FormWindowState.Maximized, true, false))
                {
                    FreightList SelectedPrice = freight.SelectedPrice;

                    if (SelectedPrice != null)
                    {
                        bool needChangeCNS = (_CurrentBLInfo.ContractID != SelectedPrice.OceanId);

                        _CurrentBLInfo.ContractID = SelectedPrice.ID;
                        txtContractNo.Text = _CurrentBLInfo.ContractNo = SelectedPrice.FreightNo;
                        //this.chkHasContract.Checked = false;

                    }
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void txtContractNo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SelectContract();
        }
        private void txtContractNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectContract();
            }
        }

        #endregion

        private void chkHasContract_CheckedChanged(object sender, EventArgs e)
        {
            SetContractBoxByHasContract(chkHasContract.Checked);
        }

        #region EDI
        /// <summary>
        /// EDI格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEDIStyle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Select(true, true);

            #region Customer

            if (txtShipperDescription.Lines != null && txtShipperDescription.Lines.Length > 0)
            {
                string strTemp = this.txtShipperDescription.Text;
                _CurrentBLInfo.ShipperDescription = parseXmlForEDI(_CurrentBLInfo.ShipperDescription);
                this.txtShipperDescription.Text = Utility.BuindDescriptionFromXml(this._CurrentBLInfo.ShipperDescription, true, false);

                if (strTemp.Trim() != txtShipperDescription.Text.Trim())
                {
                    txtShipperDescription.ForeColor = Color.Red;
                    _CurrentBLInfo.IsDirty = true;
                }
            }
            if (this.txtConsigneeDescription.Lines != null && txtConsigneeDescription.Lines.Length > 0)
            {
                string strTemp = this.txtConsigneeDescription.Text;
                this._CurrentBLInfo.ConsigneeDescription = parseXmlForEDI(_CurrentBLInfo.ConsigneeDescription);
                this.txtConsigneeDescription.Text = Utility.BuindDescriptionFromXml(this._CurrentBLInfo.ConsigneeDescription, true, false);

                if (strTemp.Trim() != txtConsigneeDescription.Text.Trim())
                {
                    txtConsigneeDescription.ForeColor = Color.Red;
                    _CurrentBLInfo.IsDirty = true;
                }
            }

            if (this.txtNotifyPartyDescription.Lines != null && txtNotifyPartyDescription.Lines.Length > 0)
            {
                string strTemp = this.txtNotifyPartyDescription.Text;
                this._CurrentBLInfo.NotifyPartyDescription = parseXmlForEDI(_CurrentBLInfo.NotifyPartyDescription);
                this.txtNotifyPartyDescription.Text = Utility.BuindDescriptionFromXml(this._CurrentBLInfo.NotifyPartyDescription, true, false);
                if (strTemp.Trim() != txtNotifyPartyDescription.Text.Trim())
                {
                    txtNotifyPartyDescription.ForeColor = Color.Red;
                    _CurrentBLInfo.IsDirty = true;
                }
            }


            #endregion

            #region mask & goodDis

            if (_CurrentBLInfo.CarrierName == null) _CurrentBLInfo.CarrierName = string.Empty;

            #region 中海
            if (_CurrentBLInfo.CarrierName.Contains("中海") || _CurrentBLInfo.CarrierName.ToUpper().Contains("CHINA SHIPPING"))
            {
                if (txtMarks.Lines != null && txtMarks.Lines.Length > 0)
                {
                    string strTemp = ediClientService.SplitString(txtMarks.Text, 18, 0);
                    if (txtMarks.Text != strTemp)
                    {
                        txtMarks.ForeColor = Color.Red;
                        _CurrentBLInfo.Marks = txtMarks.Text = strTemp;
                        _CurrentBLInfo.IsDirty = true;
                    }

                }

                if (txtGoodsDescription.Lines != null && txtGoodsDescription.Lines.Length > 0)
                {
                    string strTemp = ediClientService.SplitString(txtGoodsDescription.Text, 30, 0);
                    if (txtGoodsDescription.Text != strTemp)
                    {
                        txtGoodsDescription.ForeColor = Color.Red;
                        _CurrentBLInfo.GoodsDescription = txtGoodsDescription.Text = strTemp;
                        _CurrentBLInfo.IsDirty = true;
                    }

                }
            }
            #endregion

            #region 韩进
            if (_CurrentBLInfo.CarrierName.Contains("韩进") || _CurrentBLInfo.CarrierName.ToUpper().Contains("HANJIN"))
            {
                if (this.txtMarks.Lines != null && txtMarks.Lines.Length > 0)
                {
                    string strTemp = ediClientService.SplitString(txtMarks.Text, 18, 0);
                    if (txtMarks.Text != strTemp)
                    {
                        txtMarks.ForeColor = Color.Red;
                        _CurrentBLInfo.Marks = txtMarks.Text = strTemp;
                        _CurrentBLInfo.IsDirty = true;
                    }

                }

                if (this.txtGoodsDescription.Lines != null && txtGoodsDescription.Lines.Length > 0)
                {
                    string strTemp = ediClientService.SplitString(txtGoodsDescription.Text, 50, 0);
                    if (txtGoodsDescription.Text != strTemp)
                    {
                        txtGoodsDescription.ForeColor = Color.Red;
                        _CurrentBLInfo.GoodsDescription = txtGoodsDescription.Text = strTemp;
                        _CurrentBLInfo.IsDirty = true;
                    }

                }
            }
            #endregion

            #endregion
        }

        /// <summary>
        /// 生成 EDI格式
        /// </summary>
        /// <param name="strXml"></param>
        /// <returns></returns>
        private CustomerDescription parseXmlForEDI(CustomerDescription customerDesc)
        {

            if (customerDesc == null)
            {
                customerDesc = new CustomerDescription();
            }

            customerDesc.Name = ediClientService.SplitString(customerDesc.Name, 35, 0);

            string strTelAndFax = string.Empty;

            strTelAndFax = customerDesc.Tel == null ? string.Empty : "Tel:" + customerDesc.Tel;
            if (strTelAndFax.Length != 0) strTelAndFax += " ";
            strTelAndFax += customerDesc.Fax == null ? string.Empty : "Fax:" + customerDesc.Fax;
            strTelAndFax = cutxml(strTelAndFax, 35);

            if (strTelAndFax.Contains("Tel:")) strTelAndFax = strTelAndFax.Replace("Tel:", "");

            int index = strTelAndFax.IndexOf("Fax");

            if (index > 0)
            {

                customerDesc.Tel = strTelAndFax.Substring(0, index);
                customerDesc.Fax = strTelAndFax.Substring(index, strTelAndFax.Length - index).Replace("Fax:", "").Replace("\r\n", "");
            }
            else
            {
                customerDesc.Fax = strTelAndFax.Replace("Fax:", "");
            }

            customerDesc.Address = ediClientService.SplitString(customerDesc.Address, 35, 0);

            int addressLastLineLength = customerDesc.Address.Trim().LastIndexOf("\r\n");
            string addressString = string.Empty;//Address最后一行,不包括换行符
            if (addressLastLineLength > 0)
            {
                addressString = customerDesc.Address.Substring(addressLastLineLength, customerDesc.Address.Length - addressLastLineLength).Replace("\r\n", "");
            }
            else
            {
                addressString = customerDesc.Address.Trim();
            }

            if (!string.IsNullOrEmpty(customerDesc.City.Trim()))
            {
                string cityStateZipStr = addressString + " " + customerDesc.City;
            }

            customerDesc.Remark = ediClientService.SplitString(customerDesc.Remark, 35, 0);

            return customerDesc;
        }

        string cutxml(System.Xml.XmlNode xn, int length)
        {
            if (xn != null && xn.InnerText.Trim().Length > 0)
            {
                string strTemp = EDIService.Instance.ToDBC(xn.InnerText.Trim());
                xn.InnerText = this.CutStringByLength(strTemp, length);
            }
            else
            {
                xn.InnerText = string.Empty;
            }

            return xn.InnerText;
        }

        string cutxml(string str, int length)
        {
            if (str.Length != 0)
            {
                string strTemp = EDIService.Instance.ToDBC(str.Trim());
                strTemp = strTemp.Replace("  ", " ");
                str = this.CutStringByLength(strTemp, length);
            }

            return str;
        }

        private string CutStringByLength(string strInput, int cutLength)
        {
            string returnStr = string.Empty;
            string[] strs = strInput.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            for (int i = 0; i < strs.Length; i++)
            {
                strs[i] = CutStringHelper(strs[i], cutLength);
                returnStr += strs[i];
            }
            return returnStr;
        }
        private string CutStringHelper(string strInput, int cutLength)
        {
            StringBuilder strb = new StringBuilder();

            while (strInput.Length > 0)
            {
                if (strInput.Length <= cutLength)
                {
                    strb.Append(strInput + "\r\n");
                    break;
                }

                string temp = strInput.Substring(0, cutLength);

                if (strInput[cutLength] == ' ')
                {
                    strb.Append(temp.Trim());
                    strInput = strInput.Replace(temp, "");
                    strb.Append("\r\n");
                    continue;
                }

                if (temp.LastIndexOf(' ') >= 0)
                {
                    temp = temp.Substring(0, temp.LastIndexOf(' '));
                    strInput = strInput.Replace(temp + ' ', "");
                }
                else
                {
                    strInput = strInput.Replace(temp, "");
                }
                strb.Append(temp.Trim() + "\r\n");

            }

            return strb.ToString();
        }


        string PreDo(TextBox tb)
        {
            tb.Text = tb.Text.Trim();
            return tb.Text;
        }

        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barE_MBL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!this.Save())
            {
                return;
            }

            if (Utility.GuidIsNullOrEmpty(_CurrentBLInfo.ID))
            {
                Utility.ShowMessage(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
                return;
            }


            List<OceanBLList> mbls = new List<OceanBLList>();

            mbls.Add(_CurrentBLInfo);

            OceanBLList hjMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"); });
            OceanBLList zhMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"); });
            OceanBLList fyMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"); });
            if (hjMBL == null && zhMBL == null && fyMBL == null)
            {
                Utility.ShowMessage(LocalData.IsEnglish ? "Shipowners are now only supports China Shipping , Hanjin,Hainan Pan Ocean Shipping Electronic batch." : "现在只支持船东是 [韩进]、[中海]、[海南泛洋] 的电子补料。");

                return;
            }

            string subjuect = string.Empty;
            string toEmail = string.Empty;
            System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
            List<string> operationNos = new List<string>();
            List<Guid> mblIds = new List<Guid>();
            string key = string.Empty;
            string tip = string.Empty;

            #region 韩进
            if (hjMBL != null)
            {
                List<OceanBLList> hjMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"); });
                foreach (var item in hjMBLs)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.SONO);

                    operationNos.Add(item.No);

                    mblIds.Add(item.MBLID);
                }
                subjuect = LocalData.IsEnglish ? "HANJIN SHIPPING(" + mblNoBuilder.ToString() + ")" : "韩进电子补料(" + mblNoBuilder.ToString() + ")";
                key = "HANJIN_SI";
                tip = LocalData.IsEnglish ? "HANJIN SHIPPING" : "韩进";
            }
            #endregion

            #region 中海
            else if (zhMBL != null)
            {
                List<OceanBLList> zjMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"); });
                foreach (var item in zjMBLs)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.SONO);

                    operationNos.Add(item.No);
                    mblIds.Add(item.MBLID);
                }
                subjuect = LocalData.IsEnglish ? "CHINA SHIPPING(" + mblNoBuilder.ToString() + ")" : "中海电子补料(" + mblNoBuilder.ToString() + ")";
                key = "CSCL_SI";
                tip = LocalData.IsEnglish ? "CHINA SHIPPING" : "中海";
            }
            #endregion

            #region 泛洋
            else if (fyMBL != null)
            {
                List<OceanBLList> fyMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"); });
                foreach (var item in fyMBLs)
                {
                    if (mblNoBuilder.Length > 0)
                        mblNoBuilder.Append(",");

                    mblNoBuilder.Append(item.SONO);

                    operationNos.Add(item.No);
                    mblIds.Add(item.MBLID);
                }
                subjuect = LocalData.IsEnglish ? "HAINAN PAN SHIPPING(" + mblNoBuilder.ToString() + ")" : "海南泛洋电子补料(" + mblNoBuilder.ToString() + ")";
                key = "FYCW_SI";
                tip = LocalData.IsEnglish ? "HAINAN PAN SHIPPING" : "海南泛洋";
            }
            #endregion

            try
            {
                bool isSucc = false;

                if (mblIds.Count > 0)
                {
                    isSucc = ediClientService.SendEDI(
                        key,
                        EDILogType.SI,
                        _CurrentBLInfo.CompanyID,
                        subjuect,
                        mblIds.ToArray(),
                        operationNos.ToArray(),
                        ICP.Framework.CommonLibrary.Common.OperationType.OceanExport);
                }

                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMessage(LocalData.IsEnglish ? "Send failed" : "发送失败!" + System.Environment.NewLine + ex.Message);
            }
        }
        #endregion

    }
}


