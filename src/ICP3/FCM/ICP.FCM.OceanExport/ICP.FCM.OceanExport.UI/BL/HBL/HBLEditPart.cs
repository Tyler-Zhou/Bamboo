using System.Globalization;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTab;
using HtmlAgilityPack;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.Common;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.DataCache.ServiceInterface;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.Comm;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.UI.BL.HBL;
using ICP.FCM.OceanExport.UI.Container;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.HBL
{
    /// <summary>
    /// HBL货代提单编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class HBLEditPart : BaseEditPart
    {
        #region 服务注入
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }
        /// <summary>
        /// 国家，省份，地点信息维护服务
        /// </summary>
        public IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();

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
        /// 用户信息服务
        /// </summary>
        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
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
        /// 公共客户管理服务
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 配置管理服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 海运出口服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// FCM公共服务(Client)
        /// </summary>
        public IFCMCommonService fcmComonService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonService>();
            }

        }
        /// <summary>
        /// UI通用帮助类
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 海出打印服务
        /// </summary>
        public OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }
        /// <summary>
        /// FCM公共服务(Server)
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 财务客户端服务
        /// </summary>
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }
        /// <summary>
        /// 业务客户端服务接口
        /// </summary>
        public IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }
        /// <summary>
        /// 联系人和文档组合控件
        /// </summary>
        private UCContactAndDocumentPart ucHblOtherPart;
        /// <summary>
        /// 联系人和文档组合控件
        /// </summary>
        public UCContactAndDocumentPart UCHblOtherPart
        {
            get
            {
                if (ucHblOtherPart != null)
                {
                    return ucHblOtherPart;
                }
                else
                {
                    ucHblOtherPart = Workitem.SmartParts.AddNew<UCContactAndDocumentPart>();
                    return ucHblOtherPart;
                }
            }
        }
        /// <summary>
        /// EDI客户端服务
        /// </summary>
        public static IEDIClientService EDIClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();

            }
        }
        /// <summary>
        /// 海运出口客户端服务接口
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }

        #endregion

        #region 本地变量

        #region 箱信息

        /// <summary>
        /// 当前业务是否有箱列表
        /// </summary>
        private bool isHasContainer = false;
        /// <summary>
        /// 是否为该业务的第一个HBL
        /// </summary>
        private bool isFirstHBL = false;

        /// <summary>
        /// 箱信息是否发生改变
        /// </summary>
        private bool isCtnCharge = false;
        /// <summary>
        /// 寄给代理标识是否改变
        /// </summary>
        private bool isToAgentChange = false;
        /// <summary>
        /// 箱列表信息是否更改
        /// </summary>
        bool isChangedCtnList = false;

        /// <summary>
        /// 删除的箱ID集合
        /// </summary>
        private List<Guid> ctnIDList = new List<Guid>();

        /// <summary>
        /// 删除的箱更新时间集合
        /// </summary>
        private List<DateTime?> ctnUpdateDateList = new List<DateTime?>();

        #endregion
        /// <summary>
        /// 是否保存操作联系人
        /// </summary>
        private bool isSaveOperationContact = false;
        /// <summary>
        /// 业务操作上下文
        /// </summary>
        BusinessOperationContext OperationContext = null;
        /// <summary>
        /// 订舱详细信息
        /// </summary>
        OceanBookingInfo _bookingInfo = null;
        /// <summary>
        /// HBL详细信息
        /// </summary>
        OceanHBLInfo _CurrentBLInfo = null;
        /// <summary>
        /// 箱信息
        /// </summary>
        List<OceanBLContainerList> _ctnList = null;
        /// <summary>
        /// 代理下拉数据源
        /// </summary>
        List<CustomerList> _agentCustomersList = null;
        /// <summary>
        /// 订舱的提单信息
        /// </summary>
        List<BookingBLInfo> _OceanMBLs = null;

        /// <summary>
        /// 是否生成中海CY-CY免代理费的MEMO
        /// </summary>
        bool IBM = true;
        /// <summary>
        /// 是否初始化订舱箱描述
        /// </summary>
        bool isInitBookingContainerDescription = false;
        /// <summary>
        /// 箱描述
        /// </summary>
        ContainerDescription bookingContainerDescription;
        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;
        /// <summary>
        /// 箱信息
        /// </summary>
        List<ContainerList> ctntypes = null;
        /// <summary>
        /// 箱信息
        /// </summary>
        List<ContainerList> Ctntypes
        {
            get
            {
                if (ctntypes != null) return ctntypes;

                ctntypes = TransportFoundationService.GetContainerList(string.Empty, true, 0);
                return ctntypes;
            }
        }

        /// <summary>
        /// 文件名集合
        /// </summary>
        List<string> FilesNames = new List<string>();

        /// <summary>
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

        /// <summary>
        /// 标记是否保存已经保存了HBL
        /// </summary>
        bool isSave = true;

        /// <summary>
        /// 改bookingparty是否更新shipper
        /// </summary>
        bool isUpdateShipper = false;

        //提单抬头为如果抬头选择 CITY OCEAN LOGISTICS CO.,LTD.，打印HBL时，SCAC：CTYO
        //如果抬头选择 TOP SHIPPING LOGISTICS CO.,LTD 打印HBL时，SCAC：TPHJ
        //如果抬头选择 HARVEST LOGISTIC CORPORATION 打印HBL时，SCAC：8FH5
        //Guid cTYOID = new Guid("58B92680-A316-4F45-957A-053B11EB9CDF");
        //Guid tPHJID = new Guid("6B89A7C1-AD43-40D6-81A8-1A02362B58BB");
        //Guid hARVESTID = new Guid("12BF9D06-139C-412D-A68E-A1530ABF4BB4");

        /// <summary>
        /// 航线集合
        /// </summary>
        string[] arrAMSCTYO = new string[] { "美国航线", "AMERICA", "美国东海岸航线", "America Eest", "美国西海岸航线", "America West" };
        /// <summary>
        /// 航线集合
        /// </summary>
        string[] arrAMS8FH5 = new string[] { "加拿大航线", "CANADA" };

        /// <summary>
        /// 美线
        /// </summary>
        public static List<Guid> USAShippingLines
        {
            get
            {
                List<Guid> idlist = new List<Guid>();

                idlist.Add(new Guid("6F51BA0E-397C-4AF8-A453-617B1051E76B"));//美西
                idlist.Add(new Guid("8F09FD42-3BBA-4EA9-BB5B-80E53770CA84"));//北美区
                idlist.Add(new Guid("FC4361F1-FF7A-4B57-B411-99E106D1B7C0"));//美国航线
                idlist.Add(new Guid("E2D05D39-B9A2-4C7D-838E-C6FA466609EE"));//美东
                return idlist;
            }
        }

        #region Thread Save
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime ThreadStartTime;

        List<CustomerDescriptionForAMS> amsShippers = new List<CustomerDescriptionForAMS>();
        List<CustomerDescriptionForAMS> amsConsignees = new List<CustomerDescriptionForAMS>();
        List<CustomerDescriptionForAMS> amsSellers = new List<CustomerDescriptionForAMS>();
        List<CustomerDescriptionForAMS> amsShiptos = new List<CustomerDescriptionForAMS>();
        List<CustomerDescriptionForAMS> amsManus = new List<CustomerDescriptionForAMS>();
        List<CustomerDescriptionForAMS> amsStuffings = new List<CustomerDescriptionForAMS>();
        List<CustomerDescriptionForAMS> amsConsolidators = new List<CustomerDescriptionForAMS>();
        List<CustomerDescriptionForAMS> amsBuyers = new List<CustomerDescriptionForAMS>();
        List<CustomerDescriptionForAMS> amsBookingPartys = new List<CustomerDescriptionForAMS>();

        #endregion
        #endregion

        #region 初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public HBLEditPart()
        {
            InitializeComponent();
            SyncLocalData = true;
            if (!LocalData.IsDesignMode)
            {
                Load += (sender, e) =>
                {
                    chkToAgent.CheckStateChanged += chkToAgent_CheckStateChanged;
                    navBarControlContainerContact.Controls.Clear();
                    UCHblOtherPart.Dock = DockStyle.Fill;
                    navBarControlContainerContact.Controls.Add(UCHblOtherPart);
                    ActivateSmartPartClosingEvent(Workitem);
                    UCHblOtherPart.BindData(OperationContext);
                    #region 判断邮件地址是否存在联系人列表中
                    if (_businessOperationParameter != null)
                    {
                        if (_businessOperationParameter.Message != null)
                        {
                            if (_businessOperationParameter.Message.Attachments.Count != 0)
                            {
                                List<string> strFiles = new List<string>();
                                foreach (var item in _businessOperationParameter.Message.Attachments)
                                {
                                    strFiles.Add(item.ClientPath);
                                }
                                UCHblOtherPart.AddDocuments(strFiles, DocumentType.HBL);
                            }
                            if (_businessOperationParameter.ActionType ==
                                ActionType.Create)
                            {
                                CustomerCarrierObjects customerCarrier = FCM.Common.ServiceInterface.FCMInterfaceUtility.CreateCustomerCarrierInfo(false, false, true, _businessOperationParameter.Message.SendFrom,
                                                              OEUtility.GetSenderName(_businessOperationParameter.Message.CreatorName));
                                UCHblOtherPart.InsertCustomerInfo(customerCarrier);
                                isSaveOperationContact = true;
                            }
                        }
                        else
                        {
                            if (EditMode == EditMode.Copy)
                            {
                                ContactObjects contactInfo = FCMCommonService.GetContactList(_businessOperationParameter.Context.OperationID, _businessOperationParameter.Context.OperationType);
                                if (contactInfo != null && contactInfo.CustomerCarrier != null)
                                {
                                    UCHblOtherPart.InnerBindData(contactInfo.CustomerCarrier);
                                    isSaveOperationContact = true;
                                }
                            }
                        }
                    }
                    #endregion
                    barSavingTools.Visible = false;
                    barSavingClose.ItemClick += barSavingClose_ItemClick;
                    barCancel.ItemClick += barCancel_ItemClick;
                    barlabMessage.ItemClick += barlabMessage_ItemClick;
                    TimerSaveData = new System.Windows.Forms.Timer();
                    TimerSaveData.Interval = 1000;
                    TimerSaveData.Tick += RefreshTime_Tick;
                    chkIsBook.CheckedChanged += chkIsBook_CheckedChanged;
                    stxtAMSShipper.SelectedIndexChanged += stxtAMSShipper_SelectedIndexChanged;
                    stxtSeller.SelectedIndexChanged += stxtSeller_SelectedIndexChanged;
                    stxtAMSConsignee.SelectedIndexChanged += stxtAMSConsignee_SelectedIndexChanged;
                    stxtBuyer.SelectedIndexChanged += stxtBuyer_SelectedIndexChanged;
                    stxtManufacturer.SelectedIndexChanged += stxtManufacturer_SelectedIndexChanged;
                    stxtStuffingLocation.SelectedIndexChanged += stxtStuffingLocation_SelectedIndexChanged;
                    stxtConsolidator.SelectedIndexChanged += stxtConsolidator_SelectedIndexChanged;
                    stxtBookingPartyInfo.SelectedIndexChanged += stxtBookingPartyInfo_SelectedIndexChanged;
                    if (LocalData.IsEnglish == false) SetCnText();
                };
                Disposed += delegate
                {
                    dxErrorProvider1.DataSource = null;
                    panelScroll.Click -= OnpanelScrollClick;
                    gcAMSACIISF.DataSource = null;
                    gcAMSContainer.DataSource = null;
                    bindingAMSACIISF.DataSource = null;
                    bindingContainers.DataSource = null;
                    bindingSource1.DataSource = null;
                    _agentCustomersList = null;
                    _bookingInfo = null;
                    _businessOperationParameter = null;
                    _countryList = null;
                    _countryListForAMS = null;
                    _ctnList = null;
                    _CurrentBLInfo = null;
                    _OceanMBLs = null;
                    _businessOperationParameter = null;
                    ucHblOtherPart.Dispose();
                    Saved = null;
                    stateValues = null;
                    arrAMS8FH5 = null;
                    arrAMSCTYO = null;
                    mcmbIssueBy.Enter -= OnmdmbIssueByEnter;
                    SmartPartClosing -= HBLEditPart_SmartPartClosing;
                    stateValues = null;
                    OperationContext = null;
                    mscmbCountry.OnFirstEnter -= OnmscmbCountryFirstEnter;

                    barSavingClose.ItemClick -= barSavingClose_ItemClick;
                    barCancel.ItemClick -= barCancel_ItemClick;
                    barlabMessage.ItemClick -= barlabMessage_ItemClick;
                    TimerSaveData.Tick -= RefreshTime_Tick;

                    cmbISFImporterRefCountry.OnFirstEnter -= OncmbISFImporterRefCountryFirstEnter;

                    cmbConsigneeCountry.OnFirstEnter -= OncmbConsigneeCountryFirstEnter;

                    cmbBuyerCountry.OnFirstEnter -= OncmbBuyerCountryFirstEnter;
                    chkIsBook.CheckedChanged -= chkIsBook_CheckedChanged;
                    cmbIssueType.OnFirstEnter -= OncmbIssueTypeFirstEnter;
                    cmbReleaseType.OnFirstEnter -= OncmbReleaseTypeFirstEnter;
                    cmbAMSEntryType.OnFirstEnter -= OncmbAMSEntryTypeFirstEnter;
                    cmbACIEntryType.OnFirstEnter -= OncmbACIEntryTypeFirstEnter;
                    cmbAMSEntryType.SelectedIndexChanged -= cmbAMSEntryType_SelectedIndexChanged;
                    cmbBondRef.SelectedIndexChanged -= cmbBondRef_SelectedIndexChanged;
                    //this.cmbBLTitle.OnFirstEnter -= this.OncmbBLTitleFirstEnter;

                    cmbPaymentTerm.OnFirstEnter -= OncmbPaymentTermFirstEnter;
                    cmbQuantityUnit.OnFirstEnter -= OncmbQuantityUnitFirstEnter;
                    cmbWeightUnit.OnFirstEnter -= OncmbWeightUnitFirstEnter;
                    cmbMeasurementUnit.OnFirstEnter -= OncmbMeasurementUnitFirstTimeEnter;
                    cmbTransportClause.OnFirstEnter -= OncmbTransportClauseFirstEnter;
                    stxtAgent.FirstTimeEnter -= OnstxtAgentFirstEnter;
                    mcmbIssueBy.Enter -= OnmdmbIssueByEnter;
                    cmbPaymentTerm.SelectedIndexChanged -= cmbPaymentTerm_SelectedIndexChanged;
                    cmbConsigneeNumber.SelectedIndexChanged -= cmbConsigneeNumber_SelectedIndexChanged;
                    cmbISFImporterRef.SelectedIndexChanged -= cmbImporterRef_SelectedIndexChanged;
                    stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
                    stxtAgent.OnOk -= stxtAgent_OnOk;
                    stxtNotifyParty.OnOk -= stxtNotifyParty_OnOk;
                    stxtPreVoyage.EditValueChanged -= stxtPreVoyage_EditValueChanged;
                    stxtShipper.OnOk -= stxtShipper_OnOk;
                    stxtVoyage.EditValueChanged -= stxtVoyage_EditValueChanged;
                    xtraTabControl1.SelectedPageChanged -= xtraTabControl1_SelectedPageChanged;
                    vScrollBar2.Scroll -= vScrollBar2_Scroll;
                    cmbBondRef.SelectedIndexChanged -= cmbBondRef_SelectedIndexChanged;
                    cmbCargoType.SelectedIndexChanged -= cmbCargoType_SelectedIndexChanged;
                    stxtAMSNotifyParty.OnOk -= stxtAMSNotifyParty_OnOk;
                    stxtAMSShipper.SelectedIndexChanged -= stxtAMSShipper_SelectedIndexChanged;
                    stxtSeller.SelectedIndexChanged -= stxtSeller_SelectedIndexChanged;
                    stxtAMSConsignee.SelectedIndexChanged -= stxtAMSConsignee_SelectedIndexChanged;
                    stxtBuyer.SelectedIndexChanged -= stxtBuyer_SelectedIndexChanged;
                    stxtManufacturer.SelectedIndexChanged -= stxtManufacturer_SelectedIndexChanged;
                    stxtStuffingLocation.SelectedIndexChanged -= stxtStuffingLocation_SelectedIndexChanged;
                    stxtConsolidator.SelectedIndexChanged -= stxtConsolidator_SelectedIndexChanged;
                    stxtBookingPartyInfo.SelectedIndexChanged -= stxtBookingPartyInfo_SelectedIndexChanged;
                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(ucHblOtherPart);
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                    ucHblOtherPart = null;
                    PerformLayout();
                };
                barSubEDI.Enabled = false;
            }

        }
        /// <summary>
        /// 状态值集合
        /// </summary>
        IDictionary<string, object> stateValues;
        /// <summary>
        /// 状态值集合
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            stateValues = values;
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "BusinessOperationParameter".ToUpper())
                {
                    _businessOperationParameter = item.Value as BusinessOperationParameter;
                    return;
                }
            }
        }


        #endregion

        #region 初始化的一些方法
        /// <summary>
        /// 设置控件中文文本
        /// </summary>
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
            //lblGateIn.Text = "进箱日期";
            chkIsWoodPacking.Text = "木质包装";

            barSave.Caption = "保存(&S)";
            barSavingClose.Caption = "保存并关闭";
            barCancel.Caption = "取消";

            barSaveAs.Caption = "另存为(&A)";

            barRefresh.Caption = "刷新(&R)";

            barSubCheck.Caption = "对单";
            barCheck.Caption = "申请(&K)";
            barCheckDone.Caption = "完成(&D)";

            barClose.Caption = "关闭(&C)";

            barWebEdi.Caption = "已网上AMS";

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


            barbl.Caption = "客户确认补料";
            barblCHS.Caption = "客户确认补料(中文版)";
            barBlENG.Caption = "客户确认补料(英文版)";
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

            txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
            txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);
            txtAgentDescription.Text = _CurrentBLInfo.AgentDescription.ToString(LocalData.IsEnglish);
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.NotifyPartyID) && _CurrentBLInfo.NotifyPartyDescription == null)
                txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";
            else
            {
                txtNotifyPartyDescription.Text = _CurrentBLInfo.NotifyPartyDescription.ToString(LocalData.IsEnglish);
                if (txtNotifyPartyDescription.Text.Length == 0) txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";
            }


        }
        /// <summary>
        /// 面板获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnpanelScrollClick(object sender, EventArgs e)
        {
            panelScroll.Focus();
        }
        /// <summary>
        /// 国家信息首次获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>控件待选择列表值初始化</remarks>
        private void OnmscmbCountryFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetMcmbCountry(mscmbCountry);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OncmbISFImporterRefCountryFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetMcmbCountry(cmbISFImporterRefCountry);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OncmbConsigneeCountryFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetMcmbCountry(cmbConsigneeCountry);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OncmbBuyerCountryFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetMcmbCountry(cmbBuyerCountry);
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            panelScroll.Click += OnpanelScrollClick;

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID))
            {
                _CurrentBLInfo.Marks = @"N/M";
                //chkToAgent.Checked = true;
            }

            isFirstHBL = false;
            txtNo.Properties.ReadOnly = false;
            txtNo.Text = _CurrentBLInfo.No;
            SetComboboxEnumSource();
            SetComboboxSource();
            RefreshControlsByData();

            SearchRegister();
            stxtRefNo.Focus();

            if (_bookingInfo == null && _CurrentBLInfo != null && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID))
            {
                _bookingInfo = OceanExportService.GetOceanBookingInfo(_CurrentBLInfo.OceanBookingID);
            }

            if (_bookingInfo != null && !ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.ID))
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
            //绑定国家VesselFlag
            mscmbCountry.OnFirstEnter += OnmscmbCountryFirstEnter;

            cmbISFImporterRefCountry.OnFirstEnter += OncmbISFImporterRefCountryFirstEnter;

            cmbConsigneeCountry.OnFirstEnter += OncmbConsigneeCountryFirstEnter;

            cmbBuyerCountry.OnFirstEnter += OncmbBuyerCountryFirstEnter;

            panelISFImporterRef.Visible = false;
            panelConsignee.Visible = false;
            panelBuyer.Visible = false;
            txtISFImporterRef.Enabled = false;
            txtConsigneeNumber.Enabled = false;
            txtBuyerImportNumber.Enabled = false;
            txtBondRefNumber.Enabled = false;
            _CurrentBLInfo.isneedNotice = false;

            if (ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.DefaultCompanyID != new System.Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9"))
            {
                chkIsBook.Visible = false;
                txtBookNo.Visible = false;
                stxtPlacePay.Width = 335;
            }

            if (!string.IsNullOrEmpty(_CurrentBLInfo.DeclareNo))
            {
                chkIsBook.Checked = true;
                txtBookNo.Enabled = true;
            }

        }

        /// <summary>
        /// 初始化消息 
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("CreateBills", LocalData.IsEnglish ? "According to the contract system is generated with the bills" : "系统已根据合约生成了应付账单");
            RegisterMessage("CreateBillsForPayORCon", LocalData.IsEnglish ? "Modification of the contract or payment, system is reformed with the bills" : "修改了合约或付款方式，系统已重新生成了应付账单");
            RegisterMessage("CreateBillsForContainer", LocalData.IsEnglish ? "Modify box information, system is reformed with the bills" : "修改了箱信息，系统已重新生成了应付账单");
            RegisterMessage("CreateBillsForALL", LocalData.IsEnglish ? "Modify the contract or payment and box information, system is reformed with the bills" : "修改了合约或付款方式及箱信息，系统已重新生成了应付账单");
            RegisterMessage("IsExisteMBLNo", "MBLNo:{0} 已经存在,是否继续保存");
            RegisterMessage("IsToAgent", LocalData.IsEnglish ? "该分提单已放单，不能修改‘是否寄给目的港代理’标识，请通知放单人取消放单后修改!" : "该分提单已放单，不能修改‘是否寄给目的港代理’标识，请通知放单人取消放单后修改!");
            RegisterMessage("ReleaseChange", LocalData.IsEnglish ? "该分提单已放单，不能修改放单类型，请通知放单人取消放单后再修改放单类型!" : "该分提单已放单，不能修改放单类型，请通知放单人取消放单后再修改放单类型!");
            RegisterMessage("CreateBillsForIsToAgent", LocalData.IsEnglish ? "Modify Mark‘ToAgent’ information, system is reformed with the bills!" : "修改了‘寄给代理’标识，系统已重新生成了应付账单!");
            RegisterMessage("IsBuildCSCLMemo", LocalData.IsEnglish ? "此业务是否为要求出鹏城海提单的同行货业务?" : "此业务是否为要求出鹏城海提单的同行货业务?");
        }

        /// <summary>
        /// 根据数据源刷新控件
        /// </summary>
        private void RefreshControlsByData()
        {
            if (_CurrentBLInfo.IsNew)
            {
                #region New Init
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                if (configureInfo != null)
                {
                    _CurrentBLInfo.IssuePlaceID = configureInfo.IssuePlaceID ?? Guid.Empty;
                    _CurrentBLInfo.IssuePlaceName = configureInfo.IssuePlaceName;
                }

                if (_CurrentBLInfo.IsRequestAgent) stxtAgent.Enabled = false;
                OperationContext = new BusinessOperationContext();
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

                OperationContext = GetContext(_CurrentBLInfo);
                stxtRefNo.Properties.ReadOnly = true;
                stxtRefNo.Properties.Buttons[0].Enabled = false;
                stxtRefNo.Text = _CurrentBLInfo.RefNo;
                stxtRefNo.Tag = _CurrentBLInfo.OceanBookingID;
            }

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID) == false) RefreshEnabledByBookingType(_CurrentBLInfo.OEOperationType);

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
        private void OncmbIssueTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<IssueType>> issueTypes = EnumHelper.GetEnumValues<IssueType>(LocalData.IsEnglish);
            issueTypes.RemoveAll(item => item.Value == IssueType.Unknown);
            cmbIssueType.Properties.BeginUpdate();
            cmbIssueType.Properties.Items.Clear();
            foreach (var item in issueTypes)
            {
                cmbIssueType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbIssueType.Properties.EndUpdate();
        }
        private void OncmbReleaseTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<FCMReleaseType>> releaseTypes = EnumHelper.GetEnumValues<FCMReleaseType>(LocalData.IsEnglish);
            releaseTypes.RemoveAll(item => item.Value == FCMReleaseType.Unknown);
            cmbReleaseType.Properties.BeginUpdate();
            cmbReleaseType.Properties.Items.Clear();
            foreach (var item in releaseTypes)
            {
                cmbReleaseType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbReleaseType.Properties.EndUpdate();
        }
        private void OncmbAMSEntryTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<AMSEntryType>> amsEntryTypes = EnumHelper.GetEnumValues<AMSEntryType>(LocalData.IsEnglish);
            cmbAMSEntryType.Properties.BeginUpdate();
            cmbAMSEntryType.Properties.Items.Clear();
            foreach (var item in amsEntryTypes)
            {
                cmbAMSEntryType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));

            }
            cmbAMSEntryType.Properties.EndUpdate();
        }
        private void OncmbACIEntryTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<ACIEntryType>> aciEntryTypes = EnumHelper.GetEnumValues<ACIEntryType>(LocalData.IsEnglish);
            cmbACIEntryType.Properties.BeginUpdate();
            cmbACIEntryType.Properties.Items.Clear();
            foreach (var item in aciEntryTypes)
            {
                cmbACIEntryType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            cmbACIEntryType.Properties.EndUpdate();
        }
        /// <summary>
        /// Enum 提单类型 放单类型
        /// </summary>
        void SetComboboxEnumSource()
        {
            //签单类型
            cmbIssueType.ShowSelectedValue(_CurrentBLInfo.IssueType, _CurrentBLInfo.IssueTypeName);
            cmbIssueType.OnFirstEnter += OncmbIssueTypeFirstEnter;


            //放单类型
            cmbReleaseType.ShowSelectedValue(_CurrentBLInfo.ReleaseType, _CurrentBLInfo.ReleaseTypeName);
            cmbReleaseType.OnFirstEnter += OncmbReleaseTypeFirstEnter;


            //AMSEntryType
            if (_CurrentBLInfo.AMSEntry != null)
            {
                cmbAMSEntryType.ShowSelectedValue(_CurrentBLInfo.AMSEntry, _CurrentBLInfo.AMSEntry.ToString());
            }
            cmbAMSEntryType.OnFirstEnter += OncmbAMSEntryTypeFirstEnter;


            //ACIEntryType
            if (_CurrentBLInfo.ACIEntryType != null)
            {
                cmbACIEntryType.ShowSelectedValue(_CurrentBLInfo.ACIEntryType, _CurrentBLInfo.ACIEntryType.ToString());

            }
            cmbACIEntryType.OnFirstEnter += OncmbACIEntryTypeFirstEnter;




            //收货人和进口商税号

            List<EnumHelper.ListItem<ConsigneeAndBuyerType>> consigneeAndBuyer = EnumHelper.GetEnumValues<ConsigneeAndBuyerType>(LocalData.IsEnglish);
            foreach (var item in consigneeAndBuyer)
            {
                cmbConsigneeNumber.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                cmbBuyerImportNumber.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            //ISF ImporterRef

            List<EnumHelper.ListItem<ImportRefType>> importRefType = EnumHelper.GetEnumValues<ImportRefType>(LocalData.IsEnglish);
            foreach (var item in importRefType)
            {
                cmbISFImporterRef.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            //CargoTypeForAMS 货物种类
            List<EnumHelper.ListItem<CargoTypeForAMS>> cargoTypes = EnumHelper.GetEnumValues<CargoTypeForAMS>(LocalData.IsEnglish);
            foreach (var item in cargoTypes)
            {
                cmbCargoType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            //BondRef
            List<EnumHelper.ListItem<BondRef>> bondRef = EnumHelper.GetEnumValues<BondRef>(LocalData.IsEnglish);
            foreach (var item in bondRef)
            {
                cmbBondRef.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            //BondActivityCode
            List<EnumHelper.ListItem<BondActivityCode>> bondActivityCode = EnumHelper.GetEnumValues<BondActivityCode>(LocalData.IsEnglish);
            foreach (var item in bondActivityCode)
            {
                cmbBondActivityCode.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            //提单抬头
            //if (!ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(this._CurrentBLInfo.BLTitleID))
            //{
            //    this.cmbBLTitle.ShowSelectedValue(this._CurrentBLInfo.BLTitleID, this._CurrentBLInfo.BLTitleName);
            //}
            //this.cmbBLTitle.OnFirstEnter += this.OncmbBLTitleFirstEnter;


            #region BookingParty
            OEUtility.SetEnterToExecuteOnec(mcmbBookingParty, delegate
            {
                if (_CurrentBLInfo.CompanyID == null || _CurrentBLInfo.CompanyID == Guid.Empty)
                {
                    return;
                }
                List<ConfigureCustomerInfo> conCustomer = fcmComonService.GetConfigureCustomers(_CurrentBLInfo.CompanyID);
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "CustomerEname" : "CustomerCname", LocalData.IsEnglish ? "CustomerName" : "客户名称");
                //col.Add("Code", LocalData.IsEnglish ? "Code" : "代码");

                mcmbBookingParty.InitSource<ConfigureCustomerInfo>(conCustomer, col, LocalData.IsEnglish ? "CustomerEname" : "CustomerCname", "CustomerID");

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.BookingPartyID) && conCustomer != null & conCustomer.Count > 0)
                {
                    ConfigureCustomerInfo conCustomerinfo = conCustomer.FirstOrDefault(con => con.IsDefault == true);
                    if (conCustomerinfo != null)
                    {
                        _CurrentBLInfo.BookingPartyID = conCustomerinfo.CustomerID;
                    }
                }

                mcmbBookingParty.EditValueChanged += delegate
                {
                    BookingPartyChanged();
                };
            });
            #endregion

        }
        private void BookingPartyChanged()
        {
            if (isUpdateShipper && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.BookingPartyID))
            {
                CustomerDescription bookingPartyDescription = new CustomerDescription();

                ICPCommUIHelper.SetCustomerDesByID(_CurrentBLInfo.BookingPartyID, bookingPartyDescription);

                _CurrentBLInfo.ShipperDescription = bookingPartyDescription;
                stxtShipper.CustomerDescription = bookingPartyDescription;


                _CurrentBLInfo.ShipperID = _CurrentBLInfo.BookingPartyID;
                _CurrentBLInfo.ShipperName = _CurrentBLInfo.BookingPartyName;



                if (_CurrentBLInfo.ShipperDescription != null)
                    txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
            }
            //_oceanBookingInfo.BookingShipperName=_oceanBookingInfo.bookingpartyn
        }

        //private void OncmbBLTitleFirstEnter(object sender, EventArgs e)
        //{
        //    List<ConfigureKeyList> bLTitlelist = ConfigureService.GetConfigureKeyListForBLTitle();
        //    this.cmbBLTitle.Properties.BeginUpdate();
        //    this.cmbBLTitle.Properties.Items.Clear();
        //    foreach (ConfigureKeyList item in bLTitlelist)
        //    {
        //        cmbBLTitle.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.ID));
        //    }
        //    this.cmbBLTitle.Properties.EndUpdate();
        //}
        private void OnmdmbIssueByEnter(object sender, EventArgs e)
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.CompanyID) == false &&
                    (mcmbIssueBy.DataSource == null || mcmbIssueBy.DataSource.Rows.Count == 0))
            {

                List<UserList> saless = UserService.GetUnderlingUserList(new Guid[] { _CurrentBLInfo.CompanyID }, null, new string[] { "操作员" }, true);
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
                col.Add("Code", "代码");
                mcmbIssueBy.InitSource<UserList>(saless, col, LocalData.IsEnglish ? "EName" : "CName", "ID");
            }
        }
        private void OncmbPaymentTermFirstEnter(object sender, EventArgs e)
        {
            List<DataDictionaryList> payments = ICPCommUIHelper.SetCmbDataDictionary(cmbPaymentTerm, DataDictionaryType.PaymentTerm, DataBindType.EName, true);

            cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
            cmbPaymentTerm.SelectedIndexChanged += new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
        }
        private void OncmbWeightUnitFirstEnter(object sender, EventArgs e)
        {
            List<DataDictionaryList> weightUnitsList = ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
        }
        private void OncmbMeasurementUnitFirstTimeEnter(object sender, EventArgs e)
        {
            List<DataDictionaryList> volUnitss = ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
        }
        private void OncmbTransportClauseFirstEnter(object sender, EventArgs e)
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
        }
        private void OncmbQuantityUnitFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
        }

        /// <summary>
        /// 签发人 付款方式 运输条款 包装 重量 体积 代理
        /// </summary>
        void SetComboboxSource()
        {
            #region 签发人-- 属于操作口岸，并且角色为操作员的用户
            mcmbIssueBy.Enter += OnmdmbIssueByEnter;
            #endregion

            #region 付款方式 运输条款 包装 重量 体积

            #region 付款方式

            if (_CurrentBLInfo.PaymentTermID != null)
                cmbPaymentTerm.ShowSelectedValue(_CurrentBLInfo.PaymentTermID, _CurrentBLInfo.PaymentTermName);
            cmbPaymentTerm.OnFirstEnter += OncmbPaymentTermFirstEnter;

            if (_CurrentBLInfo.PreVoyageID != null)
            {
                stxtPreVoyage.ShowSelectedValue(_CurrentBLInfo.PreVoyageID, _CurrentBLInfo.PreVesselVoyage);
            }
            if (_CurrentBLInfo.VoyageID != null)
            {
                stxtVoyage.ShowSelectedValue(_CurrentBLInfo.VoyageID, _CurrentBLInfo.VesselVoyage);
            }
            #endregion

            #region 包装
            cmbQuantityUnit.ShowSelectedValue(_CurrentBLInfo.QuantityUnitID, _CurrentBLInfo.QuantityUnitName);
            cmbQuantityUnit.OnFirstEnter += OncmbQuantityUnitFirstEnter;


            #endregion

            #region 重量
            cmbWeightUnit.ShowSelectedValue(_CurrentBLInfo.WeightUnitID, _CurrentBLInfo.WeightUnitName);
            cmbWeightUnit.OnFirstEnter += OncmbWeightUnitFirstEnter;


            #endregion

            #region 体积
            cmbMeasurementUnit.ShowSelectedValue(_CurrentBLInfo.MeasurementUnitID, _CurrentBLInfo.MeasurementUnitName);
            cmbMeasurementUnit.OnFirstEnter += OncmbMeasurementUnitFirstTimeEnter;


            #endregion

            #region 运输条款

            cmbTransportClause.ShowSelectedValue(_CurrentBLInfo.TransportClauseID, _CurrentBLInfo.TransportClauseName);
            cmbTransportClause.OnFirstEnter += OncmbTransportClauseFirstEnter;


            #endregion

            #endregion

            #region MBL

            if (_CurrentBLInfo.IsNew == false || _CurrentBLInfo.ShippingOrderID != Guid.Empty)
            {
                try
                {
                    if (_CurrentBLInfo.OceanBookingID != Guid.Empty)
                    {
                        OceanBookingList booking = OceanExportService.GetOceanBookingListByIds(new Guid[] { _CurrentBLInfo.OceanBookingID })[0];
                        _OceanMBLs = booking.OceanMBLs;
                        foreach (var item in _OceanMBLs)
                        {
                            cmbMBLNO.Properties.Items.Add(item.NO);
                        }
                        cmbMBLNO.Text = _CurrentBLInfo.MBLNo;
                    }

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
            #endregion

            #region Agent
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.AgentID) == false)
            {
                List<CustomerList> agentCustomers = new List<CustomerList>();
                CustomerList agentCustomer = new CustomerList();
                agentCustomer.CName = agentCustomer.EName = _CurrentBLInfo.AgentName;
                agentCustomer.ID = _CurrentBLInfo.AgentID.Value;
                agentCustomers.Insert(0, agentCustomer);

                SetAgentSource(agentCustomers, false);
            }
            stxtAgent.FirstTimeEnter += OnstxtAgentFirstEnter;



            #endregion
        }
        private void OnstxtAgentFirstEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            foreach (CountryList c in _countryList)
            {
                stxtAgent.CountryItems.Add(new ImageComboBoxItem(LocalData.IsEnglish ? c.EName : c.CName));
            }

            SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);
            stxtAgent.EditValueChanged -= new EventHandler(stxtAgent_EditValueChanged);
            stxtAgent.EditValueChanged += new EventHandler(stxtAgent_EditValueChanged);
            stxtAgent.OnOk += new EventHandler(stxtAgent_OnOk);
            stxtAgent.EditValueChanging -= new ChangingEventHandler(stxtAgent_EditValueChanging);
            stxtAgent.EditValueChanging += new ChangingEventHandler(stxtAgent_EditValueChanging);
        }

        void stxtAgent_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (_bookingInfo != null && _bookingInfo.DownState && !ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.AgentID))
            {
                
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "港后已经下载业务，不能修改代理，请联系此代理单击[业务转移]。" : "It's readonly because the agent has already downloaded the shipment. Please ask the agent to --Transit-- the shipment.");
                e.Cancel = true;
                return;
            }
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
        List<CountryList> _countryListForAMS = null;
        /// <summary>
        /// 业务联系人客户搜索器桥接处理类
        /// </summary>
        CustomerContactFinderBridge checkerCustomerFinderBridge;
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
            DataFindClientService.Register(stxtRefNo, FCMFinderConstants.OceanBookingFinder, SearchFieldConstants.BookingNO, SearchFieldConstants.OceanBookingResultValue,
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
                          LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ?
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
                  ClientConstants.MainWorkspace);

            #endregion

            #region Customer

            #region SCNA

            //shipper
            OEUtility.SetEnterToExecuteOnec(stxtShipper, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                //shipper
                CustomerFinderBridge shipperBridge = new CustomerFinderBridge(
                stxtShipper,
                _countryList,
                DataFindClientService,
                CustomerService,
                _CurrentBLInfo.ShipperDescription,
                txtShipperDescription,
                ICPCommUIHelper,
                true);
                shipperBridge.Init();
            });
            stxtShipper.OnOk += new EventHandler(stxtShipper_OnOk);
            //Consignee
            OEUtility.SetEnterToExecuteOnec(stxtConsignee, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                CustomerFinderBridge consigneeBridge = new CustomerFinderBridge(
                stxtConsignee,
                _countryList,
                DataFindClientService,
                CustomerService,
                _CurrentBLInfo.ConsigneeDescription,
                txtConsigneeDescription,
                ICPCommUIHelper,
                true);
                consigneeBridge.Init();
            });

            stxtConsignee.OnOk += new EventHandler(stxtConsignee_OnOk);
            //NotifyParty
            OEUtility.SetEnterToExecuteOnec(stxtNotifyParty, delegate
            {
                if (_countryList == null) _countryList = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);

                CustomerFinderBridge notifyPartyBridge = new CustomerFinderBridge(
                 stxtNotifyParty,
                 _countryList,
                 DataFindClientService,
                 CustomerService,
                 _CurrentBLInfo.NotifyPartyDescription,
                 txtNotifyPartyDescription,
                 ICPCommUIHelper,
                 true
                 , "SAME AS CONSIGNEE");
                notifyPartyBridge.Init();

            });
            stxtNotifyParty.OnOk += new EventHandler(stxtNotifyParty_OnOk);

            #endregion

            //DataFindClientService.Register(stxtChecker, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
            //  delegate(object inputSource, object[] resultData)
            //  {
            //      stxtChecker.Text = _CurrentBLInfo.CheckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
            //      stxtChecker.Tag = _CurrentBLInfo.CheckerID = new Guid(resultData[0].ToString());

            //  }, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);


            DataFindClientService.Register(stxtChecker, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.ResultValue,
             delegate(object inputSource, object[] resultData)
             {
                 stxtChecker.Text = _CurrentBLInfo.CheckerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                 stxtChecker.Tag = _CurrentBLInfo.CheckerID = new Guid(resultData[0].ToString());

                 Guid id = new Guid(resultData[0].ToString());
                 stxtChecker.SetCustomerID(id);
                 CustomerDescription _customerDescription = new CustomerDescription();
                 ICPCommUIHelper.SetCustomerDesByID(id, _customerDescription);
                 stxtChecker.CustomerDescription = _customerDescription;

             }, delegate
             {
                 stxtChecker.Tag = _CurrentBLInfo.CheckerID = Guid.Empty;
                 stxtChecker.Text = _CurrentBLInfo.CheckerName = string.Empty;
                 stxtChecker.SetCustomerID(Guid.Empty);
                 stxtChecker.ContactList = new List<CustomerCarrierObjects>();
                 stxtChecker.CustomerDescription = new CustomerDescription();
             }, ClientConstants.MainWorkspace);


            OEUtility.SetEnterToExecuteOnec(stxtChecker, delegate
            {
                List<CustomerCarrierObjects> contactList = UCHblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _CurrentBLInfo.CheckerID);

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.CheckerID))
                {
                    _CurrentBLInfo.CheckerID = Guid.Empty;
                }
                stxtChecker.SetOperationContext(OperationContext);
                checkerCustomerFinderBridge = new CustomerContactFinderBridge(stxtChecker, null, contactList, ContactStage.BL, (Guid)_CurrentBLInfo.CheckerID, true, ContactType.Customer);
                checkerCustomerFinderBridge.Init();
                stxtChecker.OnOk += new EventHandler(stxtChecker_OnOk);
                stxtChecker.OnRefresh += new EventHandler(stxtChecker_OnRefresh);
                stxtChecker.BeforeEditValueChanged += new ChangingEventHandler(stxtChecker_EditValueChanging);
                stxtChecker.AfterEditValueChanged += new EventHandler(stxtChecker_EditValueChanged);
            });


            #endregion

            #region Port

            //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
            //大船 筛选：装货港=当前装货港and卸货港=当前卸货港

            #region PlaceOfReceipt
            DataFindClientService.Register(stxtPlaceOfReceipt, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());

                      bool isUpdatePort = true;
                      //如果改变收货地,且大船或驳船不为空，给出提示
                      if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID) == false
                          && portID != _CurrentBLInfo.PlaceOfReceiptID)
                      {
                          if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "选择的收货地驳船的装货港不匹配,是否清空驳船?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                  ClientConstants.MainWorkspace);
            #endregion
            #region POL
            DataFindClientService.Register(txtPOLCode, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      //如果改变装货港,且大船不为空，给出提示
                      bool isUpdatePort = true;
                      if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false
                          && portID != _CurrentBLInfo.POLID)
                      {
                          if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "选择的收货地与大船装货港或驳船的卸货港不匹配,是否清空大船和驳船?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                  ClientConstants.MainWorkspace);
            #endregion
            #region POD
            DataFindClientService.Register(txtPODCode, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      //如果改变卸货港,且大船不为空，给出提示
                      bool isUpdatePort = true;
                      if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false
                          && portID != _CurrentBLInfo.PODID)
                      {
                          if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "选择的装货港与大船的卸货港不匹配,是否清空大船?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                  ClientConstants.MainWorkspace);
            #endregion
            #region PlaceOfDelivery
            DataFindClientService.Register(stxtPlaceOfDelivery, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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
                  ClientConstants.MainWorkspace);
            #endregion
            #region FinalDestination
            DataFindClientService.Register(stxtFinalDestination, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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
                  ClientConstants.MainWorkspace);
            #endregion
            #region IssuePlace
            DataFindClientService.Register(stxtIssuePlace, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                delegate(object inputSource, object[] resultData)
                {
                    stxtIssuePlace.Text = _CurrentBLInfo.IssuePlaceName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                    stxtIssuePlace.Tag = _CurrentBLInfo.IssuePlaceID = new Guid(resultData[0].ToString());
                }, Guid.Empty, ClientConstants.MainWorkspace);
            #endregion
            #region 第3付款地
            PortFinderBridge pfbPlacePayOrder = new PortFinderBridge(stxtPlacePay, DataFindClientService, LocalData.IsEnglish);
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

        void stxtChecker_OnOk(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> currentList = stxtChecker.ContactList;
            UCHblOtherPart.CurrentContactList.RemoveAll(item => item.CustomerID == _CurrentBLInfo.CheckerID && item.Type == ContactType.Customer);
            if (currentList.Count > 0)
            {
                UCHblOtherPart.InsertContactList(currentList);
            }
        }

        void stxtChecker_OnRefresh(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> temp = new List<CustomerCarrierObjects>();
            if (EditMode == EditMode.New || EditMode == EditMode.Copy)
            {
                temp = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentBLInfo.CompanyID, (Guid)_CurrentBLInfo.CheckerID, ContactType.Customer, ContactStage.Unknown);

            }
            else
            {
                temp = FCMCommonService.GetContactListByContactStage(_CurrentBLInfo.OceanBookingID, OperationType.OceanExport, ContactType.Customer, ContactStage.Unknown, _CurrentBLInfo.CheckerID);
            }

            List<CustomerCarrierObjects> contactList = UCHblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _CurrentBLInfo.CheckerID && item.Type == ContactType.Customer);
            UCHblOtherPart.CurrentContactList.RemoveAll(item => contactList.Contains(item));
            UCHblOtherPart.InsertContactList(temp);
            stxtChecker.ContactList = contactList;
        }

        void stxtChecker_EditValueChanging(object sender, ChangingEventArgs e)
        {
            Guid oldId = (Guid)e.OldValue;
            if (!ArgumentHelper.GuidIsNullOrEmpty(oldId))
            {
                RemoveContactList(oldId);
            }

        }

        void stxtChecker_EditValueChanged(object sender, EventArgs e)
        {
            AddLastestContact((Guid)_CurrentBLInfo.CheckerID, stxtChecker, ContactType.Customer);
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

        #region 联系人信息
        /// <summary>
        /// 更新联系人控件数据
        /// </summary>
        private void UpdateContactControlData()
        {
            if (stxtChecker.IsContactDataChanged)
            {
                if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.CheckerID))
                {
                    stxtChecker.ContactList = GetCurrentContactListByCustomerID((Guid)_CurrentBLInfo.CheckerID, ContactType.Customer);
                }
            }
        }
        /// <summary>
        /// 移除联系人列表
        /// </summary>
        /// <param name="changeID"></param>
        private void RemoveContactList(Guid changeID)
        {
            ucHblOtherPart.RemoveContactList(changeID, null);
        }
        /// <summary>
        /// 通过客户ID获取联系人信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="contactType">联系人类型</param>
        /// <returns></returns>
        private List<CustomerCarrierObjects> GetCurrentContactListByCustomerID(Guid customerID, ContactType contactType)
        {
            List<CustomerCarrierObjects> contactList = UCHblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID && item.Type == contactType);
            return contactList;
        }
        #endregion

        #region 控件或数据的联动操作

        #region 当公司更变时需 设置Agent数据源
        /// <summary>
        /// 设置Agent数据源
        /// </summary>
        private void SetAgentSourceByCompanyID(Guid companyID)
        {
            stxtAgent.DataSource = null;
            if (ArgumentHelper.GuidIsNullOrEmpty(companyID) || _CurrentBLInfo.IsRequestAgent)
            {
                stxtAgent.Enabled = false;
                return;
            }

            bool isFirst = false;
            if (_agentCustomersList == null)
            {
                _agentCustomersList = ConfigureService.GetCompanyAgentList(_CurrentBLInfo.CompanyID, true);
                isFirst = true;
            }

            if (_bookingInfo != null &&
                !ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.CustomerID) &&
                (string.IsNullOrEmpty(_bookingInfo.SalesTypeName) == false &&
                        (_bookingInfo.SalesTypeName.Contains("指定货") ||
                        _bookingInfo.SalesTypeName.ToUpper().Contains("AGENT"))))
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
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.AgentID))
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
            if (ArgumentHelper.GuidIsNullOrEmpty(id))
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

            _bookingInfo = OceanExportService.GetOceanBookingInfo(bookingID);

            ConfigureInfo _configureInfo = ConfigureService.GetCompanyConfigureInfo(_bookingInfo.CompanyID, LocalData.IsEnglish);

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID) == false && _CurrentBLInfo.OceanBookingID != bookingID)
            {
                ///	否则提示：“是否重新导入发货人、收货人、通知人、地点、货物信息？”，如果选择是，则继续执行下一步，否则退出。
                if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "是否重新导入发货人、收货人、通知人、地点、货物信息?"
                                 , LocalData.IsEnglish ? "Tip" : "提示"
                                 , MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    stxtRefNo.Text = _CurrentBLInfo.RefNo = reno;
                    stxtRefNo.Tag = _CurrentBLInfo.OceanBookingID = bookingID;
                    stxtPreVoyage.Tag = stxtPreVoyage.EditValue = _CurrentBLInfo.PreVoyageID = _bookingInfo.PreVoyageID;
                    stxtPreVoyage.EditText = _CurrentBLInfo.PreVesselVoyage = _bookingInfo.PreVoyageName;
                    stxtVoyage.Tag = stxtVoyage.EditValue = _CurrentBLInfo.VoyageID = _bookingInfo.VoyageID;
                    stxtVoyage.EditText = _CurrentBLInfo.VesselVoyage = _bookingInfo.VoyageName;

                    _CurrentBLInfo.BLTitleID = ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.BLTitleID) ? _configureInfo.BLTitleID : _bookingInfo.BLTitleID;
                    _CurrentBLInfo.BLTitleName = string.IsNullOrEmpty(_bookingInfo.BLTitleName) ? _configureInfo.BLTitleName : _bookingInfo.BLTitleName;
                    // cmbBLTitle.ShowSelectedValue(_CurrentBLInfo.BLTitleID, _CurrentBLInfo.BLTitleName);

                    dtETD.EditValue = _CurrentBLInfo.ETD = _bookingInfo.ETD;
                    dtETA.EditValue = _CurrentBLInfo.ETA = _bookingInfo.ETA;
                    _OceanMBLs = new List<BookingBLInfo>();

                    return;
                }

            }
            #endregion


            #region 如果选择的订舱单为“只出MBL”时，提示用户，需要修改订单属性，然后再新增HBL。

            if (_bookingInfo.IsOnlyMBL)
            {
                if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "此订舱单已钩选\"只出MBL\",强制关联HBL会自动修改订舱单数据.是否继续?"
                                    , LocalData.IsEnglish ? "Tip" : "提示"
                                    , MessageBoxButtons.YesNo) == DialogResult.No)
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

            #region 订舱人
            _CurrentBLInfo.BookingPartyID = _bookingInfo.BookingPartyID;
            _CurrentBLInfo.BookingPartyName = _bookingInfo.BookingPartyName;
            isUpdateShipper = _CurrentBLInfo.ShipperID == _CurrentBLInfo.BookingPartyID ? true : false;

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

            //if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false)
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
            if (ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.QuantityUnitID) == false)
            {
                cmbQuantityUnit.Text = _CurrentBLInfo.QuantityUnitName = _bookingInfo.QuantityUnitName;
                _CurrentBLInfo.QuantityUnitID = _bookingInfo.QuantityUnitID.Value;

                cmbQuantityUnit.ShowSelectedValue(_CurrentBLInfo.QuantityUnitID, _CurrentBLInfo.QuantityUnitName);

            }
            _CurrentBLInfo.Weight = _bookingInfo.Weight;
            if (ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.WeightUnitID) == false)
            {
                cmbWeightUnit.Text = _CurrentBLInfo.WeightUnitName = _bookingInfo.WeightUnitName;
                _CurrentBLInfo.WeightUnitID = _bookingInfo.WeightUnitID.Value;

                cmbWeightUnit.ShowSelectedValue(_CurrentBLInfo.WeightUnitID, _CurrentBLInfo.WeightUnitName);
            }
            _CurrentBLInfo.Measurement = _bookingInfo.Measurement;
            if (ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.MeasurementUnitID) == false)
            {
                cmbMeasurementUnit.Text = _CurrentBLInfo.MeasurementUnitName = _bookingInfo.MeasurementUnitName;
                _CurrentBLInfo.MeasurementUnitID = _bookingInfo.MeasurementUnitID.Value;
                cmbMeasurementUnit.ShowSelectedValue(_CurrentBLInfo.MeasurementUnitID, _CurrentBLInfo.MeasurementUnitName);
            }
            #endregion


            _CurrentBLInfo.BLTitleID = ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.BLTitleID) ? _configureInfo.BLTitleID : _bookingInfo.BLTitleID;
            _CurrentBLInfo.BLTitleName = string.IsNullOrEmpty(_bookingInfo.BLTitleName) ? _configureInfo.BLTitleName : _bookingInfo.BLTitleName;
            //cmbBLTitle.ShowSelectedValue(_CurrentBLInfo.BLTitleID, _CurrentBLInfo.BLTitleName);


            _CurrentBLInfo.CarrierName = _bookingInfo.CarrierName;

            _CurrentBLInfo.ContractID = _bookingInfo.ContractID;
            _CurrentBLInfo.IsHasContract = !ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.ContractID);

            bookingContainerDescription = _bookingInfo.ContainerDescription;
            bindingSource1.EndEdit();
            RefreshEnabledByBookingType(_bookingInfo.OEOperationType);

            cmbMBLNO.EditValue = _CurrentBLInfo.MBLNo = string.Empty;
            cmbMBLNO.Properties.Items.Clear();
            _OceanMBLs = _bookingInfo.OceanMBLs;
            if (_OceanMBLs == null) _OceanMBLs = new List<BookingBLInfo>();
            foreach (var item in _OceanMBLs)
            {
                cmbMBLNO.Properties.Items.Add(item.NO);
            }
            #endregion

            if (_bookingInfo.OceanHBLs == null || _bookingInfo.OceanHBLs.Count == 0)
            {
                isFirstHBL = true;
            }
            barReplyAgent.Enabled = true;
        }

        #endregion

        #region Voyage Changed

        /// <summary>
        /// 大船改变，填充ETD，如果没有驳船，填充ETA， 截柜日，截关日,截文件日
        /// </summary>
        private void VoyageChanged()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID))
            {
                chkShowVoyage.Checked = chkShowVoyage.Enabled = false;

                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
                {
                    dtETA.EditValue = _CurrentBLInfo.ETA = null;
                }
                dtPETD.EditValue = _CurrentBLInfo.ETD = null;
            }
            else
            {
                //VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.VoyageID.Value);
                //dtPOR.EditValue = _CurrentBLInfo.ETD = voyageInfo.ETD;

                //if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
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
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
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
                && ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.AgentID) == false)
            {
                try
                {
                    bool isExist = OceanExportService.IsPortCountryExistCompanyConfig(_CurrentBLInfo.PlaceOfDeliveryID.Value, _CurrentBLInfo.CompanyID);
                    if (isExist == false)
                    {
                        _isNeedRequestAgent = true;
                        if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Current BL need request agent.is clear current BL\'s agent?"
                                                                  : "当前提单需要申请代理,是否清空代理?"
                           , LocalData.IsEnglish ? "Tip" : "提示"
                           , MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            _isNeedAutoRequestAgent = true;
                            stxtAgent.Enabled = false;
                            stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription = new CustomerDescription();
                            stxtAgent.EditValue = _CurrentBLInfo.AgentID = Guid.Empty;
                            _CurrentBLInfo.AgentName = string.Empty;
                        }
                    }
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
            }
        }

        #endregion

        #region

        #region  付款方式
        void cmbPaymentTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.BookingPaymentTermID) == false && _CurrentBLInfo.BookingPaymentTermID != _CurrentBLInfo.PaymentTermID)
            {
                if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "现选择的HBL付款方式与订舱单中HBL付款方式不同,是否继续?",
                                             LocalData.IsEnglish ? "Tip" : "提示",
                                             MessageBoxButtons.YesNo) == DialogResult.No)
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

        private void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            //try
            //{
            //    BeginThreadInit();
            //    SavingThreadStart(false);
            //}
            //catch (Exception ex)
            //{

            //    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "保存出现系统错误：" + ex.Message);
            //}
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (Save())
                {
                    try
                    {
                        SaveOtherPart();
                    }
                    catch (Exception ex)
                    {
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty, string.Empty, string.Format("Other Part 保存失败:SessionId[{0}]", LocalData.SessionId));
                        LocalCommonServices.ErrorTrace.SetErrorInfo(null, string.Format("{0}\r\nLog ID:[{1}]", ex.Message, OperationLogID));
                    }
                }

            }
        }

        #region SavingClose
        /// <summary>
        /// 保存并关闭
        /// </summary>
        void barSavingClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                BeginThreadInit();
                SavingThreadStart(true);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "保存出现系统错误：" + ex.Message);
            }
        }

        void barCancel_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                TimerSaveData.Stop();
                if (ThreadSaveData != null)
                {
                    ThreadSaveData.Abort();
                    ThreadSaveData = null;
                }
                barSavingTools.Visible = false;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "保存出现系统错误：" + ex.Message);
            }
        }
        void barlabMessage_ItemClick(object sender, ItemClickEventArgs e)
        {
            string labTag = barlabMessage.Tag == null ? "" : barlabMessage.Tag.ToString();
            if (!string.IsNullOrEmpty(labTag) && labTag.Equals("Success"))
            {
                barSavingTools.Visible = false;
            }
        }
        void RefreshTime_Tick(object sender, EventArgs e)
        {
            if (!barSavingTools.Visible)
                return;
            DateTime curTime = DateTime.Now;
            TimeSpan span = curTime - ThreadStartTime;
            barlabSeconds.Caption = string.Format((LocalData.IsEnglish ? "({0}seconds)" : "({0}秒)"), (int)span.TotalSeconds);
            if (span.TotalSeconds >= LocalData.AsynchronousSaveTimeout)
            {
                barCancel.Enabled = true;
            }
        }
        private void BeginThreadInit()
        {
            barlabMessage.Tag = "";
            barlabSeconds.Caption = LocalData.IsEnglish ? "(0 seconds)" : "(0 秒)";
            SetLableMessage(LocalData.IsEnglish ? "Saving is in progress..." : "正在保存...", "saving");
            ThreadStartTime = DateTime.Now;
            TimerSaveData.Start();

            barlabSeconds.Visibility = BarItemVisibility.Always;
            barCancel.Visibility = BarItemVisibility.Never;
            barSavingTools.Visible = true;
        }

        private void SavingThreadStart(bool isCloseThis)
        {
            barlabMessage.Caption = LocalData.IsEnglish ? "Saving is in progress..." : "正在保存...";
            ThreadSaveData = new Thread(SavingAndClose);
            ThreadSaveData.Name = "SavingMBL";
            ThreadSaveData.Start(isCloseThis);
        }

        private void SavingAndClose(object o)
        {
            try
            {
                ClientHelper.SetApplicationContext();
                bool isCloseThis = (bool)o;
                if (Save())
                {
                    SaveOtherPart();
                }
                TimerSaveData.Stop();
                if (saveState().Equals("success"))
                {
                    if (isCloseThis && FindForm() != null)
                    {
                        FindForm().Close();
                    }
                }
            }
            catch (Exception ex)
            {
                barlabMessage.Caption = "保存出现系统错误";
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        private string saveState()
        {
            return barlabMessage.Tag == null ? "saving" : barlabMessage.Tag.ToString();
        }
        private void SetLableMessage(string message, string state)
        {
            if (InvokeRequired)
                Invoke(new Action<string, string>(SetLableMessage), message, state);
            else
            {
                string beforeState = saveState();
                switch (state)
                {
                    case "exception":
                        if (beforeState.Equals(state))
                            barlabMessage.Caption += message;
                        else
                            barlabMessage.Caption = string.IsNullOrEmpty(message) ? "保存出现系统错误" : message;
                        break;
                    default:
                        barlabMessage.Caption = message;
                        break;
                }
                if (!beforeState.Equals("exception"))
                    barlabMessage.Tag = state;
                if (!state.Equals("success") && !state.Equals("exception")) return;
                TimerSaveData.Stop();
                barlabSeconds.Visibility = BarItemVisibility.Never;
            }
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <returns>是否继续</returns>
        private delegate DialogResult showForm();
        /// <summary>
        /// 显示窗体
        /// </summary>
        private showForm _ShowForm;

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <returns>是否继续</returns>
        private DialogResult ShowForm()
        {
            if (InvokeRequired)
            {
                _ShowForm = ShowForm;
                return (DialogResult)Invoke(_ShowForm);
            }
            HBLIsUpdateMBLNOForm f = Workitem.Items.AddNew<HBLIsUpdateMBLNOForm>();
            DialogResult result = f.ShowDialog(this);
            return result;
        }
        #endregion

        private bool SaveOtherPart()
        {
            if ((_CurrentBLInfo != null && UCHblOtherPart.IsChanged) || isSaveOperationContact)
            {
                Stopwatch StopwatchStep = Stopwatch.StartNew();
                Guid _OtherPartLogID = Guid.NewGuid();
                //保存联系人列表及附件
                UCHblOtherPart.SetContext = GetContext(_CurrentBLInfo);
                UCHblOtherPart.Save(_CurrentBLInfo.UpdateDate);
                UpdateContactControlData();
                if (_businessOperationParameter == null)
                {
                    _businessOperationParameter = new BusinessOperationParameter();
                }

                _businessOperationParameter.Context = GetContext(_CurrentBLInfo);

                if (Saved != null) Saved(new object[] { _CurrentBLInfo, _businessOperationParameter,_businessOperationParameter.Context });

                StopwatchHelper.CustomRecordStopwatch(StopwatchStep, _OtherPartLogID, DateTime.Now, BaseFormID, "SAVE-HBL-OTHER", string.Format("保存MBL其它面板:联系人列表及附件;OperationID[{0}]HBL ID[{1}]", _CurrentBLInfo.OceanBookingID, _CurrentBLInfo.ID));
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "数据保存成功");
            }
            return true;
        }


        private bool Save(bool needValidate = true)
        {
            try
            {
                OperationLogID = Guid.NewGuid();
                StopwatchSaveData = Stopwatch.StartNew();

                StopwatchHelper.CustomRecordStopwatch(StopwatchSaveData, OperationLogID, DateTime.Now, BaseFormID,
                    "SAVE-HBL", string.Format("保存HBL;Operation ID{0} HBL ID[{1}]", _CurrentBLInfo.OceanBookingID, _CurrentBLInfo.ID));

                if (needValidate && ValidateData() == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:数据未通过验证");
                    return false;
                }


                if (AmsIsChanged) _CurrentBLInfo.IsDirty = true;

                #region 格式化AMS ContainerDetails HSCode
                List<ContainerDetailsForAMS> containerDetails = new List<ContainerDetailsForAMS>();
                ContainerDetailsForAMS c1 = new ContainerDetailsForAMS();
                c1.HarmonizedTariffCode = txtHS1.Text;
                c1.CountryOfOrigin = mscmbHS1.EditValue == null ? Guid.Empty : new Guid(mscmbHS1.EditValue.ToString());
                c1.CountryName = mscmbHS1.EditText;
                if (!CheckHSCountry(c1))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS1 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c1);
                ContainerDetailsForAMS c2 = new ContainerDetailsForAMS();
                c2.HarmonizedTariffCode = txtHS2.Text;
                c2.CountryOfOrigin = mscmbHS2.EditValue == null ? Guid.Empty : new Guid(mscmbHS2.EditValue.ToString());
                c2.CountryName = mscmbHS2.EditText;
                if (!CheckHSCountry(c2))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS2 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c2);
                ContainerDetailsForAMS c3 = new ContainerDetailsForAMS();
                c3.HarmonizedTariffCode = txtHS3.Text;
                c3.CountryOfOrigin = mscmbHS3.EditValue == null ? Guid.Empty : new Guid(mscmbHS3.EditValue.ToString());
                c3.CountryName = mscmbHS3.EditText;
                if (!CheckHSCountry(c3))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS3 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c3);
                ContainerDetailsForAMS c4 = new ContainerDetailsForAMS();
                c4.HarmonizedTariffCode = txtHS4.Text;
                c4.CountryOfOrigin = mscmbHS4.EditValue == null ? Guid.Empty : new Guid(mscmbHS4.EditValue.ToString());
                c4.CountryName = mscmbHS4.EditText;
                if (!CheckHSCountry(c4))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS4 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c4);
                ContainerDetailsForAMS c5 = new ContainerDetailsForAMS();
                c5.HarmonizedTariffCode = txtHS5.Text;
                c5.CountryOfOrigin = mscmbHS5.EditValue == null ? Guid.Empty : new Guid(mscmbHS5.EditValue.ToString());
                c5.CountryName = mscmbHS5.EditText;
                if (!CheckHSCountry(c5))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS5 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c5);
                ContainerDetailsForAMS c6 = new ContainerDetailsForAMS();
                c6.HarmonizedTariffCode = txtHS6.Text;
                c6.CountryOfOrigin = mscmbHS6.EditValue == null ? Guid.Empty : new Guid(mscmbHS6.EditValue.ToString());
                c6.CountryName = mscmbHS6.EditText;
                if (!CheckHSCountry(c6))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS6 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c6);
                ContainerDetailsForAMS c7 = new ContainerDetailsForAMS();
                c7.HarmonizedTariffCode = txtHS7.Text;
                c7.CountryOfOrigin = mscmbHS7.EditValue == null ? Guid.Empty : new Guid(mscmbHS7.EditValue.ToString());
                c7.CountryName = mscmbHS7.EditText;
                if (!CheckHSCountry(c7))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS7 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c7);
                ContainerDetailsForAMS c8 = new ContainerDetailsForAMS();
                c8.HarmonizedTariffCode = txtHS8.Text;
                c8.CountryOfOrigin = mscmbHS8.EditValue == null ? Guid.Empty : new Guid(mscmbHS8.EditValue.ToString());
                c8.CountryName = mscmbHS8.EditText;
                if (!CheckHSCountry(c8))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS8 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c8);
                ContainerDetailsForAMS c9 = new ContainerDetailsForAMS();
                c9.HarmonizedTariffCode = txtHS9.Text;
                c9.CountryOfOrigin = mscmbHS9.EditValue == null ? Guid.Empty : new Guid(mscmbHS9.EditValue.ToString());
                c9.CountryName = mscmbHS9.EditText;
                if (!CheckHSCountry(c9))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS9 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c9);
                ContainerDetailsForAMS c10 = new ContainerDetailsForAMS();
                c10.HarmonizedTariffCode = txtHS10.Text;
                c10.CountryOfOrigin = mscmbHS10.EditValue == null ? Guid.Empty : new Guid(mscmbHS10.EditValue.ToString());
                c10.CountryName = mscmbHS10.EditText;
                if (!CheckHSCountry(c10))
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:HS10 Country Of Origin is wrong!");
                    ShowMessage("Country Of Origin is wrong!");
                    //MessageBoxService.ShowInfo("Country Of Origin is wrong!");
                    return false;
                }
                containerDetails.Add(c10);
                #endregion

                #region AMS标记已经修改
                if (DataSourceForAMSList != null)
                    foreach (OceanHBL2AmsAciIsf o in DataSourceForAMSList)
                    {
                        bool c = true;
                        if (o.IsDirty)
                        {
                            _CurrentBLInfo.IsDirty = true;
                            c = false;
                        }
                        o.ContainerDetails = containerDetails;
                        o.Flag = mscmbCountry.EditValue == null ? Guid.Empty : new Guid(mscmbCountry.EditValue.ToString()); ;
                        o.IMO = txtIMO.Text.Trim();
                        o.VesselName = txtVesselName.Text.Trim();
                        o.VoyageNumber = txtVoyageNumber.Text.Trim();
                        o.LastPortOfCall = txtLastPortOfCall.Text.Trim();
                        o.FirstPorOtfCall = txtFirstPortOfCall.Text.Trim();
                        o.PortOfLoading = txtPOL.Text.Trim();
                        if (string.IsNullOrEmpty(txtETD.Text.Trim()))
                            o.Etd = null;
                        else
                            o.Etd = Convert.ToDateTime(txtETD.Text.Trim());
                        if (string.IsNullOrEmpty(txtFirstPortOfCallDate.Text.Trim()))
                            o.FirstPortOfCallDate = null;
                        else
                            o.FirstPortOfCallDate = Convert.ToDateTime(txtFirstPortOfCallDate.Text.Trim());
                        if (c)
                            o.IsDirty = false;
                    }
                #endregion


                if (_CurrentBLInfo.IsDirty == false && _CurrentBLInfo.IsNew == false && isChangedCtnList == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存成功:数据未更改!");
                    return true;
                }

                #region Comment Code - 是否生成中海CY-CY免代理费的MEMO
                //LocationInfo placeofdelivery = GeographyService.GetLocationInfo(_bookingInfo.PlaceOfDeliveryID);
                ////   1、  如果业务的船东为中海、交货地的国家为美国或加拿大、揽货类型为同行货和运输条款为CY-CY，那么在分文件自动增加memo：此票为免代理费的同行货业务(The co-load’ business agent fee-free )
                //// 2、  满足上述条件并且HBL的shipper与订舱客户不相同，在保存提单时提示用户：此业务是否为要求出鹏城海提单的同行货业务？
                //if (isFirstHBL && _CurrentBLInfo.ShipperID != _bookingInfo.CustomerID && _bookingInfo.CarrierCode == "CSCL"
                //    && _bookingInfo.SalesTypeID == new Guid("F25174F7-3ED3-47B6-9E07-82B12D713EB5") && _bookingInfo.TransportClauseID == new Guid("BC6CF07B-9BEA-4A5B-A1F3-DCBB6BB6BF15")
                //    && (placeofdelivery.CountryID == new Guid("C063DED5-8428-46CB-904A-C4CD4326C7AA") || placeofdelivery.CountryID == new Guid("37F06C2D-E5F6-4A6F-BB55-9DA3EC3B42A4")))
                //{
                //    string message = NativeLanguageService.GetText(this, "IsBuildCSCLMemo");
                //    IBM = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                //                                          (LocalData.IsEnglish ? " Tip" : "提示"),
                //                                          MessageBoxButtons.YesNo) == DialogResult.No ? false : true;
                //}
                #endregion

                FilesNames = UCHblOtherPart.CurrentDocumentName;

                #region 保存AMS信息
                if (DataSourceForAMSList != null)
                {
                    if (DataSourceForAMSList.Count > 0)
                    {
                        if (DataSourceForAMSList.Any(o => o.Container == null))
                        {

                            StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:未添加AMS箱信息！");
                            ShowMessage("请添加AMS箱信息！");
                            //MessageBoxService.ShowInfo("请添加AMS箱信息！");
                            return false;
                        }
                        AMSEntryType amsEntryType = (AMSEntryType)Enum.Parse(typeof(AMSEntryType), cmbAMSEntryType.SelectedItem.ToString());
                        OceanExportService.SaveAmsAciIsfOjbects(DataSourceForAMSList, _CurrentBLInfo.ID, LocalData.UserInfo.LoginID, amsEntryType);
                        //更新列表
                        //LoadAmsAndContainer();
                        gvAMSACIISF.SelectRow(0);
                    }
                }
                #endregion

                isSave = true;



                if (!AutoRequestAgent())
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:自动申请代理失败!");
                    return false;
                }

                #region 处理与MBL的关系
                BookingBLInfo existBlItem = _OceanMBLs.Find(delegate(BookingBLInfo item) { return item.NO == _CurrentBLInfo.MBLNo; });
                if (existBlItem != null)
                {
                    _CurrentBLInfo.MBLID = existBlItem.ID;
                    _CurrentBLInfo.MBLUpdateDate = existBlItem.UpdateDate;
                }
                else if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.MBLID) == false)
                {
                    //HBLIsUpdateMBLNOForm f = Workitem.Items.AddNew<HBLIsUpdateMBLNOForm>();
                    //DialogResult result = f.ShowDialog(this);
                    DialogResult result = ShowForm();
                    if (result == DialogResult.Cancel)
                    {
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:取消更新MBL单号!");
                        return false;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        _CurrentBLInfo.MBLID = _CurrentBLInfo.MBLID;
                        _CurrentBLInfo.MBLUpdateDate = _CurrentBLInfo.MBLUpdateDate;
                    }
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
                barSavingClose.Enabled = false;
                panelScroll.Enabled = false;
                bool isAutoBulidMBL = false;
                bool IsSynToMBL = false;
                if (isChangedCtnList || isCtnCharge || _CurrentBLInfo.isneedNotice)
                {
                    DialogResult dialogResult = ShowQuestion((LocalData.IsEnglish ? "Update MBL?" : "是否需要更新MBL？"), (LocalData.IsEnglish ? "ToolTip" : "提示"), MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK) { IsSynToMBL = true; }
                }

                if (isChangedCtnList || ctnIDList.Count > 0 || isToAgentChange)
                {
                    #region 更新HBL与箱信息
                    Stopwatch StopwatchStep = Stopwatch.StartNew();

                    if (_CurrentBLInfo.ID != Guid.Empty)
                    {
                        //  _CurrentBLInfo.UpdateDate = oeService.GetOceanHBLInfo(_CurrentBLInfo.ID).UpdateDate; 
                    };
                    if (isChangedCtnList == false && isToAgentChange && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID))//只更改IsToAgent 时也要触发更新MBL箱列表。
                    {
                        _ctnList = OceanExportService.GetOceanHBLContainerList(_CurrentBLInfo.ID);
                    }
                    SaveHBLInfoParameter hbl = ConvertHBLToParameter(false, _CurrentBLInfo);
                    if (hbl.mblID == Guid.Empty && string.IsNullOrEmpty(hbl.mblNO) == false)
                    {
                        isAutoBulidMBL = true;
                        if (!IsExisteMBLNo())
                        {

                            StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存失败:自动创建MBL失败，MBL[{0}]已存在！", _CurrentBLInfo.No));
                            return false;
                        }
                    }

                    SaveBLContainerParameter ctn = ConvertCtnToParameter(false, _CurrentBLInfo.OceanBookingID, _ctnList);
                    //不是寄给港后代理的提单不需要同步到MBL。
                    SingleResult result = OceanExportService.SaveOceanHBLAndContainerWithTrans(hbl, ctn, ctnIDList, ctnUpdateDateList, IsSynToMBL);
                    SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                    ManyResult ctnResult = null;
                    if (ctn != null)
                    {
                        //SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                        ctnResult = result.GetValue<ManyResult>("ContainerResult");
                        isHasContainer = true;
                    }
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true, string.Format("HBL & 箱信息 保存成功 [{0}ms]", StopwatchStep.ElapsedMilliseconds));
                    isSave = true;
                    _CurrentBLInfo.IsDirty = false;
                    _CurrentBLInfo.isneedNotice = false;
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
                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveHBLInfoParameter hbl = ConvertHBLToParameter(false, _CurrentBLInfo);
                    if (hbl.mblID == Guid.Empty && string.IsNullOrEmpty(hbl.mblNO) == false)
                    {
                        isAutoBulidMBL = true;

                        if (!IsExisteMBLNo())
                        {
                            StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存失败:HBL[{0}]已存在！", _CurrentBLInfo.No));
                            return false;
                        }
                    }

                    // bool IsSynToMBL = false;
                    //if (dialogResult == DialogResult.OK) { IsSynToMBL = true; }

                    SingleResult result = OceanExportService.SaveOceanHBLInfo(hbl, IsSynToMBL);

                    UpdateBLBySaved(result);
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true, string.Format("HBL 保存成功 [{0}ms]", StopwatchStep.ElapsedMilliseconds));
                    isSave = true;
                    _CurrentBLInfo.IsDirty = false;
                    _CurrentBLInfo.isneedNotice = false;
                    #endregion
                }

                #region 重新生成运价
                //修改了寄给代理标识（IsToAgent）并且箱不为空，则重新生成运价
                if ((isChangedCtnList || ctnIDList.Count > 0 || (isToAgentChange && _ctnList == null)) && _CurrentBLInfo.IsHasContract)
                {
                    if (isCtnCharge || _CurrentBLInfo.IsChargePayOrCon || isToAgentChange)
                    {
                        try
                        {
                            Stopwatch StopwatchStep = Stopwatch.StartNew();
                            SingleResult result = OceanExportService.CreateBill(_CurrentBLInfo.OceanBookingID, LocalData.UserInfo.LoginID);

                            if (result != null)
                            {
                                int s = result.GetValue<Byte>("State");
                                string title = string.Empty;
                                if (s == 1)
                                {
                                    if (_CurrentBLInfo.IsChargePayOrCon && isCtnCharge && isToAgentChange)
                                    {
                                        title = NativeLanguageService.GetText(this, "CreateBillsForALL");
                                    }
                                    else if (_CurrentBLInfo.IsChargePayOrCon)
                                    {
                                        title = NativeLanguageService.GetText(this, "CreateBillsForPayORCon");
                                    }
                                    else if (isCtnCharge)
                                    {
                                        title = NativeLanguageService.GetText(this, "CreateBillsForContainer");
                                    }
                                    else if (isToAgentChange)
                                    {
                                        title = NativeLanguageService.GetText(this, "CreateBillsForIsToAgent");
                                    }
                                    if (!string.IsNullOrEmpty(title))
                                    {
                                        ShowMessage(title);
                                        //MessageBoxService.ShowInfo(title);
                                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true, string.Empty, string.Format(title + "[{0}ms]", StopwatchStep.ElapsedMilliseconds));
                                    }
                                    _CurrentBLInfo.IsChargePayOrCon = false;
                                    isCtnCharge = false;
                                    isToAgentChange = false;
                                }
                                else if (s == 2)
                                {
                                    string message = result.GetValue<string>("message");
                                    title = LocalData.IsEnglish ? "Generate the bill Error:" : "生成账单失败：";
                                    ShowMessage(title + message);
                                    //MessageBoxService.ShowInfo(title + message);
                                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty, string.Format(title + "{0}", StopwatchStep.ElapsedMilliseconds));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true, string.Empty, string.Format("更新运价失败 SessionId[{0}]", LocalData.SessionId));
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), string.Format("{0}\r\nLog ID:[{1}]", ex.Message, OperationLogID));
                        }
                    }
                }
                #endregion

                ctnIDList = new List<Guid>();
                ctnUpdateDateList = new List<DateTime?>();

                AmsIsChanged = false;

                AfterSave(false, isAutoBulidMBL);
                return true;
            }
            catch (Exception ex)
            {
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存失败，SessionId[{0}]", LocalData.SessionId));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), string.Format("{0}\r\nLog ID:[{1}]", ex.Message, OperationLogID));
                return false;
            }
            finally { barSave.Enabled = true; barSavingClose.Enabled = true; panelScroll.Enabled = true; }
        }
        /// <summary>
        /// 判断HSCode国家是否正确
        /// </summary>
        /// <param name="c1"></param>
        private bool CheckHSCountry(ContainerDetailsForAMS c1)
        {
            bool c = true;
            if (!string.IsNullOrEmpty(c1.HarmonizedTariffCode.Trim()))
            {
                if (c1.CountryOfOrigin == Guid.Empty)
                {
                    c = false;
                }
            }
            return c;
        }

        /// <summary>
        /// MBL是否重复，并弹出提示是否继续
        /// </summary>
        /// <returns></returns>
        private bool IsExisteMBLNo()
        {
            if (OceanExportService.IsExistsMBLNo(_CurrentBLInfo.MBLID, _CurrentBLInfo.MBLNo))
            {
                string message = string.Format(NativeLanguageService.GetText(this, "IsExisteMBLNo"), _CurrentBLInfo.MBLNo);
                DialogResult dr = ShowQuestion(message,
                                                 (LocalData.IsEnglish ? " Tip" : "提示"),
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxDefaultButton.Button2);
                //DialogResult dr = MessageBoxService.Show(message,
                //                                  (LocalData.IsEnglish ? " Tip" : "提示"),
                //                                   MessageBoxButtons.YesNo,
                //                                   MessageBoxIcon.Question,
                //                                   MessageBoxDefaultButton.Button2);

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
            if (InvokeRequired)
            {
                Invoke(new Action(() => { cmbMBLNO.Text = _CurrentBLInfo.MBLNo; }));
            }
            else
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
                    SingleResult result = FCMCommonService.RequestOceanAgent(_CurrentBLInfo.OceanBookingID
                                                                            , OperationType.OceanExport
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
                    bool srcc = ClientOceanExportService.ReplyAgent(_CurrentBLInfo.OceanBookingID, null, null);
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
            if (InvokeRequired)
            {
                Invoke(new Action<bool, bool>(AfterSave), IsSaveAs, isAutoBulidMBL);
            }
            else
            {
                string HBLID = "";
                //this.btnContainer.Enabled = true;
                if (containerListPart != null)
                {
                    containerListPart.HBLID = _CurrentBLInfo.ID;
                    containerListPart.ShippingOrderID = _CurrentBLInfo.OceanBookingID;
                }
                isChangedCtnList = false;


                _CurrentBLInfo.ContainerNos = OEUtility.BulidCtnNOByContainerList(_ctnList);
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
                    Title = LocalData.IsEnglish ? "Edit HBL " + _CurrentBLInfo.No : "编辑HBL " + _CurrentBLInfo.No;
                RefreshBarEnabled();
                if (!ucHblOtherPart.IsChanged && !isSaveOperationContact)
                {
                    if (_businessOperationParameter == null)
                    {
                        _businessOperationParameter = new BusinessOperationParameter();
                    }

                    _businessOperationParameter.Context = GetContext(_CurrentBLInfo);

                    if (Saved != null) Saved(new object[] { _CurrentBLInfo,_businessOperationParameter, _businessOperationParameter.Context });
                }

                if (EditMode == EditMode.New)
                {
                    HBLID = string.Format("MBL ID[{0}]", _CurrentBLInfo.ID);
                    EditMode = EditMode.Edit;
                }
                string message = string.Empty;
                if (IsSaveAs)
                    message = LocalData.IsEnglish ? "Save as a new bl successfully." : "已成功另存为一票新的提单.";
                else
                    message = (LocalData.IsEnglish ? "Save Successfully" : "保存成功.");

                if (isAutoBulidMBL)
                    message += LocalData.IsEnglish ? "System has been bulid a new MBL." : "系统已自动生成一票新的MBL.";
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty, string.Empty, HBLID + message);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
            }
        }

        private bool ValidateData()
        {
            EndEdit();
            List<bool> isScrrs = new List<bool> { true, true };
            //bool isScrr = true;
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
                            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ShipperID))
                            {
                                e.SetErrorInfo("ShipperID", LocalData.IsEnglish ? "Shipper Must Inpu" : "发货人必须输入.");
                            }

                            //有驳船，驳船离港日必须输入
                            if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID) && !_CurrentBLInfo.PreETD.HasValue)
                            {
                                e.SetErrorInfo("PORETD", LocalData.IsEnglish ? "PORETD Must Input" : "有驳船，驳船离港日必须输入.");
                            }

                            //有大船 大船离港日 ，到港日必须输入
                            if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID))
                            {
                                if (!_CurrentBLInfo.ETD.HasValue)
                                {
                                    e.SetErrorInfo("ETD", LocalData.IsEnglish ? "ETD Must Input" : "有大船，大船离港日必须输入.");
                                }
                                if (!_CurrentBLInfo.ETA.HasValue)
                                {
                                    e.SetErrorInfo("ETA", LocalData.IsEnglish ? "ETA Must Input" : "有大船，大船到港日必须输入.");
                                }
                            }
                            if (_CurrentBLInfo.GateInDate > _CurrentBLInfo.ETD)
                            {
                                e.SetErrorInfo("GateInDate", LocalData.IsEnglish ? "GateInDate greater than ETD" : "您输入的进港日大于离港日.");
                            }
                            //宁波口岸需验证关键字段是否包含中文
                            if (("" + _CurrentBLInfo.CompanyID).ToUpper() == "A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9")
                            {
                                if (Utility.IsContainsChinese(_CurrentBLInfo.ShipperDescription.ToString(CultureInfo.InvariantCulture)))
                                {
                                    isScrrs[0] = false;
                                    e.SetErrorInfo("Shipper", LocalData.IsEnglish ? "Shipper include Chinese." : "发货人中包含中文.");
                                }
                                if (Utility.IsContainsChinese(_CurrentBLInfo.ConsigneeDescription.ToString(CultureInfo.InvariantCulture)))
                                {
                                    isScrrs[0] = false;
                                    e.SetErrorInfo("Consignee", LocalData.IsEnglish ? "Consignee include Chinese." : "收货人中包含中文.");
                                }
                                if (Utility.IsContainsChinese(_CurrentBLInfo.NotifyPartyDescription.ToString(CultureInfo.InvariantCulture)))
                                {
                                    isScrrs[0] = false;
                                    e.SetErrorInfo("NotifyParty", LocalData.IsEnglish ? "NotifyParty include Chinese." : "通知人中包含中文.");
                                }
                                if (Utility.IsContainsChinese(_CurrentBLInfo.GoodsDescription))
                                {
                                    isScrrs[0] = false;
                                    e.SetErrorInfo("GoodsDescription", LocalData.IsEnglish ? "Goods description include Chinese." : "货物描述中包含中文.");
                                }
                                if (Utility.IsContainsChinese(_CurrentBLInfo.Marks))
                                {
                                    isScrrs[0] = false;
                                    e.SetErrorInfo("Marks", LocalData.IsEnglish ? "Marks include Chinese." : "标记与标号中包含中文.");
                                }
                                if (Utility.IsContainsChinese(_CurrentBLInfo.CtnQtyInfo))
                                {
                                    isScrrs[0] = false;
                                    e.SetErrorInfo("CtnQtyInfo", LocalData.IsEnglish ? "Ctn Qty Info include Chinese." : "集装箱件数与合计中包含中文.");
                                }
                                if (Utility.IsContainsChinese(_CurrentBLInfo.FreightDescription))
                                {
                                    isScrrs[0] = false;
                                    e.SetErrorInfo("FreightDescription", LocalData.IsEnglish ? "Freight description include Chinese." : "应收/应付中包含中文.");
                                }
                            }
                        }
                    ) == false) isScrrs[0] = false;

            if (UCHblOtherPart.ValidateData() == false)
            {
                isScrrs[1] = false;
            }

            bool isScrr = true;
            foreach (var item in isScrrs)
                if (item == false) isScrr = false;

            return isScrr;
        }

        #endregion

        #region 另存为

        private void barSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveAs();
        }

        bool SaveAs()
        {
            try
            {
                OperationLogID = Guid.NewGuid();
                StopwatchSaveData = Stopwatch.StartNew();

                StopwatchHelper.CustomRecordStopwatch(StopwatchSaveData, OperationLogID, DateTime.Now, BaseFormID,
                    "SAVE-HBL", string.Format("另存HBL;OperationID[{0}]HBL ID[{1}]", _CurrentBLInfo.OceanBookingID, _CurrentBLInfo.ID));

                if (ValidateData() == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:数据未通过验证");
                    return false;
                }

                if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "是否另存为一票新的提单?",
                                LocalData.IsEnglish ? "Tip" : "提示",
                                MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:取消保存");
                    return false;
                }



                if (_ctnList == null) _ctnList = OceanExportService.GetOceanHBLContainerList(_CurrentBLInfo.ID);
                bool needSaveCtn = false;//如果没有关联的箱，不提交保存箱
                bool isSaveCtn = false;//询问用户是否保存箱
                #region
                foreach (var item in _ctnList) { if (item.Relation) { needSaveCtn = true; continue; } }
                if (needSaveCtn)
                {
                    if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "是否保存箱?",
                                        LocalData.IsEnglish ? "Tip" : "提示",
                                        MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveHBLInfoParameter hbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    if (hbl.mblID == Guid.Empty && string.IsNullOrEmpty(hbl.mblNO) == false) isAutoBulidMBL = true;

                    SingleResult result = OceanExportService.SaveOceanHBLInfo(hbl, true);

                    _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.MBLID = result.GetValue<Guid>("OceanMBLID");
                    _CurrentBLInfo.MBLUpdateDate = result.GetValue<DateTime?>("MBLUpdateDate");
                    _CurrentBLInfo.No = result.GetValue<string>("No");
                    //_CurrentBLInfo.AMSNo = result.GetValue<string>("AMSNo");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    txtCtnInfo.Text = _CurrentBLInfo.ContainerDescription = string.Empty;
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("HBL[{0}]保存成功[{1}]", _CurrentBLInfo.No, StopwatchStep.ElapsedMilliseconds));
                }
                else
                {
                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveHBLInfoParameter mbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    SaveHBLInfoParameter hbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    if (hbl.mblID == Guid.Empty && string.IsNullOrEmpty(hbl.mblNO) == false) isAutoBulidMBL = true;


                    SaveBLContainerParameter ctn = ConvertCtnToParameter(true, _CurrentBLInfo.OceanBookingID, _ctnList);
                    //SingleResult result = oeService.SaveOceanHBLAndContainerWithTrans(mbl, ctn, ctnIDList, ctnUpdateDateList,true);
                    SingleResult result = OceanExportService.SaveOceanHBLAndContainerWithTrans(mbl, ctn, ctnIDList, ctnUpdateDateList, true);

                    SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                    ManyResult ctnResult = result.GetValue<ManyResult>("ContainerResult");

                    #region  处理返回值

                    _CurrentBLInfo.ID = blResult.GetValue<Guid>("ID");
                    _CurrentBLInfo.MBLID = blResult.GetValue<Guid>("OceanMBLID");
                    _CurrentBLInfo.MBLUpdateDate = blResult.GetValue<DateTime?>("MBLUpdateDate");
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

                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("HBL [{0}] & 箱信息 保存成功[{1}]", _CurrentBLInfo.No, StopwatchStep.ElapsedMilliseconds));

                }
                AfterSave(true, isAutoBulidMBL);

                return true;
            }
            catch (Exception ex)
            {
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存失败，SessionId[{0}]", LocalData.SessionId));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), string.Format("{0}\r\nLog ID:[{1}]", ex.Message, OperationLogID));

                return false;
            }
        }

        #endregion

        #region 添加对应客户的上一票业务的对应沟通阶段的联系人
        /// <summary>
        /// 添加对应客户的上一票业务的对应沟通阶段的联系人
        /// </summary>
        /// <param name="customerID"></param>
        private void AddLastestContact(Guid customerID, object customerControl, ContactType contactType)
        {
            if (customerID == Guid.Empty)
            {
                return;
            }
            List<CustomerCarrierObjects> contactList = new List<CustomerCarrierObjects>();
            if (!UCHblOtherPart.CurrentContactList.Exists(item => item.CustomerID == customerID))
            {
                contactList = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentBLInfo.CompanyID, customerID, contactType, ContactStage.Unknown);
                if (contactList == null || contactList.Count <= 0)
                    return;
                for (int i = 0; i < contactList.Count; i++)
                {
                    contactList[i].Id = Guid.Empty;

                }
                List<CustomerCarrierObjects> currentContactList = UCHblOtherPart.CurrentContactList;
                if (currentContactList == null || currentContactList.Count <= 0)
                {
                    UCHblOtherPart.InsertContactList(contactList);
                }
                else
                {
                    List<string> nameList = (from item in currentContactList select item.Name).ToList();
                    List<string> emailList = (from item in currentContactList select item.Mail).ToList();

                    contactList = contactList.FindAll(item => !nameList.Contains(item.Name) && !emailList.Contains(item.Mail));
                    UCHblOtherPart.InsertContactList(contactList);
                }
            }
            else
            {
                contactList = UCHblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID);
            }
            SetContactList(customerID, contactList);



        }
        private void SetContactList(Guid customerID, List<CustomerCarrierObjects> contactList)
        {
            if (_CurrentBLInfo.CheckerID == customerID)
            {
                stxtChecker.ContactList = contactList;

            }

        }

        #endregion

        #region  打开箱列表

        ContainerListPart containerListPart = null;

        private void btnContainer_Click(object sender, EventArgs e)
        {
            OpenCtn();
        }

        private void OpenCtn()
        {
            try
            {
                string OceanShippingOrderNo = string.Empty;
                Regex rx = new Regex(@"[0-9a-zA-Z]");
                if (_bookingInfo != null && !string.IsNullOrEmpty(_bookingInfo.OceanShippingOrderNo))
                {
                    foreach (char charname in _bookingInfo.OceanShippingOrderNo)
                    {
                        if (rx.IsMatch(charname.ToString()))
                        {
                            OceanShippingOrderNo += charname;
                        }
                        else
                            break;
                    }
                }

                #region 初始化 箱数据
                if (_ctnList == null)
                {
                    if (_CurrentBLInfo.IsNew == false)
                    {
                        _ctnList = OceanExportService.GetOceanHBLContainerList(_CurrentBLInfo.ID);
                    }
                    else
                    {
                        List<OceanContainerList> bookingCtns = OceanExportService.GetOceanContainerList(_CurrentBLInfo.OceanBookingID);
                        if (bookingCtns != null && bookingCtns.Count > 0)
                        {
                            _ctnList = new List<OceanBLContainerList>();
                            foreach (var item in bookingCtns)
                            {
                                OceanBLContainerList ctn = new OceanBLContainerList();
                                OEUtility.CopyToValue(item, ctn, typeof(OceanBLContainerList));

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
                                        newCtn.ShippingOrderNo = OceanShippingOrderNo;
                                        _ctnList.Add(newCtn);
                                    }
                                }

                                isInitBookingContainerDescription = true;
                            }
                        }
                    }
                }

                //} 
                #endregion

                #region 初始化 箱页面实例
                if (containerListPart == null)
                {

                    containerListPart = Workitem.Items.AddNew<ContainerListPart>();
                    if (_bookingInfo == null)
                    {
                        _bookingInfo = OceanExportService.GetOceanBookingInfo(_CurrentBLInfo.OceanBookingID);
                    }

                    containerListPart.OEOperationType = _bookingInfo.OEOperationType;
                    containerListPart.BLQtyUnit = cmbQuantityUnit.Text;
                    containerListPart.BLWeightUnit = cmbWeightUnit.Text;
                    containerListPart.BLMeasurementUnit = cmbMeasurementUnit.Text;
                    containerListPart.BLQuantityUnitID = _CurrentBLInfo.QuantityUnitID;
                    containerListPart.BLWeightUnitID = _CurrentBLInfo.WeightUnitID;
                    containerListPart.BLMeasurementUnitID = _CurrentBLInfo.MeasurementUnitID;
                    containerListPart.BLSourceType = FCMBLType.MBL;
                    containerListPart.MBLID = _CurrentBLInfo.ID;
                    containerListPart.BLMeasurementUnitID = _CurrentBLInfo.MeasurementUnitID;
                    containerListPart.ShippingOrderID = _CurrentBLInfo.OceanBookingID;
                    containerListPart.ShippingOrderNo = OceanShippingOrderNo;

                    containerListPart.Saved += delegate(object[] prams)
                    {
                        #region 确定箱后更新页面信息
                        _ctnList = prams[0] as List<OceanBLContainerList>;

                        txtCtnInfo.Text = containerListPart.CTNInfo;
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

                            if (strbuilder.Length > 0) strbuilder.Append(GlobalConstants.ShowDividedSymbol);
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

            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        #endregion

        #region 关闭

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        void HBLEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (_CurrentBLInfo.IsDirty || isSave == false)
            {
                DialogResult result = PartLoader.EnquireIsSaveCurrentDataByUpdated();
                if (result == DialogResult.Yes)
                {
                    if (SaveData() == false) e.Cancel = true;
                }
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        #endregion

        #region 打印

        private void barPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.IsDirty)
            {
                if (SaveData() == false) return;
            }
            ClientOceanExportService.PrintBillOfLoading(_CurrentBLInfo.ID);

        }

        private Message.ServiceInterface.Message GetOperationInfo()
        {
            if (_CurrentBLInfo == null)
                return null;
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = _CurrentBLInfo.ID;
            message.UserProperties.FormType = FormType.Booking;
            message.UserProperties.FormId = _CurrentBLInfo.ID;

            return message;
        }
        #endregion

        #region barItem Click 事件

        private void barCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.State != OEBLState.Draft) return;

                try
                {
                    SingleResult result = OceanExportService.ChangeOceanHBLState(_CurrentBLInfo.ID, OEBLState.Checking, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);

                    bool isDirty = _CurrentBLInfo.IsDirty;
                    _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    _CurrentBLInfo.State = OEBLState.Checking;
                    _CurrentBLInfo.IsDirty = isDirty;
                    RefreshBarEnabled();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Check Successfully" : "设置对单成功.");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }

                RefreshBarEnabled();
            }
        }
        private void barCheckDone_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentBLInfo.ID == Guid.Empty) return;

                try
                {
                    SingleResult result = OceanExportService.ChangeOceanHBLState(_CurrentBLInfo.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);
                    bool isDirty = _CurrentBLInfo.IsDirty;
                    _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    _CurrentBLInfo.State = OEBLState.Checked;
                    _CurrentBLInfo.IsDirty = isDirty;
                    RefreshBarEnabled();
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Checked Successfully" : "设置完成对单成功.");
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
                RefreshBarEnabled();
            }
        }

        private void barAMS_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region 验证
            if (AmsIsChanged) _CurrentBLInfo.IsDirty = true;
            if (_CurrentBLInfo.IsDirty || isSave == false)
            {
                MessageBoxService.ShowInfo("Please save current BL.");
                return;
            }
            if (_CurrentBLInfo.ID == Guid.Empty)
            {
                MessageBoxService.ShowInfo("Please select current BL.");
                return;
            }
            if (string.IsNullOrEmpty(txtVesselName.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VesselName！");
                return;
            }
            if (string.IsNullOrEmpty(txtVoyageNumber.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VoyageNumber！");
                return;
            }
            if (txtVoyageNumber.Text.Trim().Length > 5)
            {
                MessageBoxService.ShowInfo("VoyageNumber is wrong！");
                return;
            }
            //if (cmbBLTitle.SelectedItem.ToString() != "CITY OCEAN LOGISTICS CO.,LTD.")
            //{
            //    MessageBoxService.ShowInfo("Only support 'CITY OCEAN LOGISTICS CO.,LTD.' company！");
            //    return;
            //}
            if (string.IsNullOrEmpty(txtIMO.Text))
            {
                MessageBoxService.ShowInfo("Please Get IMO&Flag！");
                return;
            }
            if (string.IsNullOrEmpty(mscmbCountry.EditText))
            {
                MessageBoxService.ShowInfo("Please Get IMO&Flag！");
                return;
            }
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.Unknown)
            {
                MessageBoxService.ShowInfo("Please select AMSEntryType.");
                return;
            }
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.StayonBoard && string.IsNullOrEmpty(txtFirstPortOfCallDate.Text))
            {
                MessageBoxService.ShowInfo("Must input First Port Of Call Date.");
                return;
            }
            if (DataSourceForAMSList == null)
                return;
            if (DataSourceForAMSList.Count < 1)
                return;
            StringBuilder errorText = new StringBuilder();
            int rowCount = DataSourceForAMSList.Count;
            int i = 1;
            bool isError = false;
            foreach (OceanHBL2AmsAciIsf oha in DataSourceForAMSList)
            {
                errorText.AppendLine(i.ToString());
                i++;
                if (rowCount > 1)
                {
                    if (string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Must input Mark！");
                        isError = true;
                    }
                }
                else
                    if (!string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Currently there is only one data,mark should be empty！");
                        isError = true;
                    }
                if (string.IsNullOrEmpty(oha.LastPortOfCall))
                {
                    errorText.AppendLine("Must input LastPortOfCall！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.FirstPorOtfCall))
                {
                    errorText.AppendLine("Must input FirstPortOfCall！");
                    isError = true;
                }
                isError = CheckShipperForAMSInfo(errorText, isError, oha);

                if (CheckCustomerForAMSInfo("Consignee", oha.Consignee, ref errorText))
                    isError = true;
                isError = CheckContainerForAMSInfo(errorText, isError, oha);
            }
            if (isError)
            {
                MessageBoxService.ShowInfo(errorText.ToString());
                return;
            }

            #endregion

            string subjuect = string.Empty;
            string toEmail = string.Empty;
            List<Guid> oIds = new List<Guid>();
            oIds.Add(_CurrentBLInfo.OceanBookingID);
            List<Guid> hblIds = new List<Guid>();
            hblIds.Add(_CurrentBLInfo.ID);
            List<string> operationNos = new List<string>();
            operationNos.Add(_CurrentBLInfo.No);
            string key = "AMS";
            string tip = string.Empty;
            bool isSucc = false;
            subjuect = "AMS(" + _CurrentBLInfo.No.ToString() + ")";

            tip = "AMS";
            try
            {
                ClientOceanExportService.SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.AMS, _CurrentBLInfo.CompanyID, null, null);

                OceanExportService.ChangeAmsState(_CurrentBLInfo.OceanBookingID, true, LocalData.IsEnglish);
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }

        }

        /// <summary>
        /// 确认AMS费用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barConfirmedAMS_ItemClick(object sender, ItemClickEventArgs e)
        {

            try
            {
                List<Guid> hblIds = new List<Guid>();
                hblIds.Add(_CurrentBLInfo.ID);
                ClientOceanExportService.ConfirmedAMS(hblIds.ToArray(),LocalData.UserInfo.LoginID);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(null, LocalData.IsEnglish ? "Confirmed!" : "已确认!");
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// 判断税号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="oha"></param>
        /// <param name="errorText"></param>
        /// <returns>是否错误=true</returns>
        private bool CheckRefNumberForAMSInfo(string type, OceanHBL2AmsAciIsf oha, ref StringBuilder errorText)
        {
            bool t = false;
            if (type == "ISF Importer Ref#")
            {
                string[] num = oha.ISFImporterID.Split('-');
                if (oha.ISFImporterIDType == ImportRefType.EIN)
                {
                    bool tt = false;
                    if (num.Length != 2)
                    {
                        tt = true;
                    }
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 2)
                        {
                            tt = true;
                        }
                        if (i == 1 && num[i].Length != 7 && num[i].Length != 9)
                        {
                            tt = true;
                        }
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("ISF Importer Ref# is wrong！");
                    }
                }
                if (oha.ISFImporterIDType == ImportRefType.SSN)
                {
                    bool tt = false;
                    if (num.Length != 3)
                    {
                        tt = true;
                    }
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 3)
                        {
                            tt = true;
                        }
                        if (i == 1 && num[i].Length != 2)
                        {
                            tt = true;
                        }
                        if (i == 2 && num[i].Length != 4)
                        {
                            tt = true;
                        }
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("ISF Importer Ref# is wrong！");
                    }
                    //判断First Name……
                    if (string.IsNullOrEmpty(oha.ISFImporterFirstName))
                    {
                        errorText.AppendLine("Must input ISFImporter FirstName！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ISFImporterLastName))
                    {
                        errorText.AppendLine("Must input ISFImporter LastName！");
                        t = true;
                    }
                    if (oha.ISFImporterDateOfBirth == null)
                    {
                        errorText.AppendLine("Must input ISFImporter DateOfBirth！");
                        t = true;
                    }
                }
                if (oha.ISFImporterIDType == ImportRefType.CBP)
                {
                    bool tt = false;
                    if (num.Length != 2)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 6)
                            tt = true;
                        if (i == 1 && num[i].Length != 5)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("ISF Importer Ref# is wrong！");
                    }
                }
                if (oha.ISFImporterIDType == ImportRefType.Passport)
                {
                    //判断First Name……
                    if (oha.ISFImporterCountryOfIssuance == null || oha.ISFImporterCountryOfIssuance == Guid.Empty)
                    {
                        errorText.AppendLine("Must select ISFImporter Country of Issuance: ！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ISFImporterFirstName))
                    {
                        errorText.AppendLine("Must input ISFImporter FirstName！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ISFImporterLastName))
                    {
                        errorText.AppendLine("Must input ISFImporter LastName！");
                        t = true;
                    }
                    if (oha.ISFImporterDateOfBirth == null)
                    {
                        errorText.AppendLine("Must input ISFImporter DateOfBirth！");
                        t = true;
                    }
                }
            }
            ///////////////////////////////////
            if (type == "BondRef#")
            {
                string[] num = oha.BondReferenceNumber.Split('-');
                if (oha.BondReferenceType == BondRef.EIN)
                {
                    bool tt = false;
                    if (num.Length != 2)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 2)
                            tt = true;
                        if (i == 1 && num[i].Length != 7 && num[i].Length != 9)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("BondRef# is wrong！");
                    }
                }
                if (oha.BondReferenceType == BondRef.SSN)
                {
                    bool tt = false;
                    if (num.Length != 3)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 3)
                            tt = true;
                        if (i == 1 && num[i].Length != 2)
                            tt = true;
                        if (i == 2 && num[i].Length != 4)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("BondRef# is wrong！");
                    }
                }
                if (oha.BondReferenceType == BondRef.CBP)
                {
                    bool tt = false;
                    if (num.Length != 2)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 6)
                            tt = true;
                        if (i == 1 && num[i].Length != 5)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("BondRef# is wrong！");
                    }
                }
            }
            ///////////////////////////////////
            if (type == "Consignee#")
            {
                string[] num = oha.ConsigneeNumber.Split('-');
                if (oha.ConsigneeNumberQualifier == ConsigneeAndBuyerType.EIN)
                {
                    bool tt = false;
                    if (num.Length != 2)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 2)
                            tt = true;
                        if (i == 1 && num[i].Length != 7 && num[i].Length != 9)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("Consignee# is wrong！");
                    }
                }
                if (oha.ConsigneeNumberQualifier == ConsigneeAndBuyerType.SSN)
                {
                    bool tt = false;
                    if (num.Length != 3)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 3)
                            tt = true;
                        if (i == 1 && num[i].Length != 2)
                            tt = true;
                        if (i == 2 && num[i].Length != 4)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("Consignee# is wrong！");
                    }
                    if (string.IsNullOrEmpty(oha.ConsigneeFirstName))
                    {
                        errorText.AppendLine("Must input Consignee# FirstName！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ConsigneeLastName))
                    {
                        errorText.AppendLine("Must input Consignee# LastName！");
                        t = true;
                    }
                    if (oha.ConsigneePassportDOB == null)
                    {
                        errorText.AppendLine("Must input Consignee# DateOfBirth！");
                        t = true;
                    }
                }
                if (oha.ConsigneeNumberQualifier == ConsigneeAndBuyerType.CBP)
                {
                    bool tt = false;
                    if (num.Length != 2)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 6)
                            tt = true;
                        if (i == 1 && num[i].Length != 5)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("Consignee# is wrong！");
                    }
                }
                if (oha.ConsigneeNumberQualifier == ConsigneeAndBuyerType.Passport)
                {
                    //判断First Name……
                    if (oha.ConsigneePassportIssuanceCountry == null || oha.ConsigneePassportIssuanceCountry == Guid.Empty)
                    {
                        errorText.AppendLine("Must select Consignee# Country of Issuance！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ConsigneeFirstName))
                    {
                        errorText.AppendLine("Must input Consignee# FirstName！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ConsigneeLastName))
                    {
                        errorText.AppendLine("Must input Consignee# LastName！");
                        t = true;
                    }
                    if (oha.ConsigneePassportDOB == null)
                    {
                        errorText.AppendLine("Must input Consignee# DateOfBirth！");
                        t = true;
                    }
                }
            }
            ///////////////////////////////////
            if (type == "Importer#")
            {
                string[] num = oha.ImporterOfRecordNumber.Split('-');
                if (oha.ImporterOfRecordNumberQualifier == ConsigneeAndBuyerType.EIN)
                {
                    bool tt = false;
                    if (num.Length != 2)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 2)
                            tt = true;
                        if (i == 1 && num[i].Length != 7 && num[i].Length != 9)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("Importer# is wrong！");
                    }
                }
                if (oha.ImporterOfRecordNumberQualifier == ConsigneeAndBuyerType.SSN)
                {
                    bool tt = false;
                    if (num.Length != 3)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 3)
                            tt = true;
                        if (i == 1 && num[i].Length != 2)
                            tt = true;
                        if (i == 2 && num[i].Length != 4)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("Importer# is wrong！");
                    }
                    if (string.IsNullOrEmpty(oha.ImporterOfRecordFirstName))
                    {
                        errorText.AppendLine("Must input Importer# FirstName！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ImporterOfRecordLastName))
                    {
                        errorText.AppendLine("Must input Importer# LastName！");
                        t = true;
                    }
                    if (oha.ImporterOfRecordDOB == null)
                    {
                        errorText.AppendLine("Must input Importer# DateOfBirth！");
                        t = true;
                    }
                }
                if (oha.ImporterOfRecordNumberQualifier == ConsigneeAndBuyerType.CBP)
                {
                    bool tt = false;
                    if (num.Length != 2)
                        tt = true;
                    for (int i = 0; i < num.Length; i++)
                    {
                        if (i == 0 && num[i].Length != 6)
                            tt = true;
                        if (i == 1 && num[i].Length != 5)
                            tt = true;
                    }
                    if (tt)
                    {
                        t = true;
                        errorText.AppendLine("Importer# is wrong！");
                    }
                }
                if (oha.ImporterOfRecordNumberQualifier == ConsigneeAndBuyerType.Passport)
                {
                    //判断First Name……
                    if (oha.ImporterOfPassportIssuanceCountry == null || oha.ImporterOfPassportIssuanceCountry == Guid.Empty)
                    {
                        errorText.AppendLine("Must select Importer# Country of Issuance！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ImporterOfRecordFirstName))
                    {
                        errorText.AppendLine("Must input Importer# FirstName！");
                        t = true;
                    }
                    if (string.IsNullOrEmpty(oha.ImporterOfRecordLastName))
                    {
                        errorText.AppendLine("Must input Importer# LastName！");
                        t = true;
                    }
                    if (oha.ImporterOfRecordDOB == null)
                    {
                        errorText.AppendLine("Must input Importer# DateOfBirth！");
                        t = true;
                    }
                }
            }
            return t;
        }

        private void barACI_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region 验证
            if (AmsIsChanged) _CurrentBLInfo.IsDirty = true;
            if (_CurrentBLInfo.IsDirty || isSave == false)
            {
                MessageBoxService.ShowInfo("Please save current BL.");
                return;
            }
            if (_CurrentBLInfo.ID == Guid.Empty)
            {
                MessageBoxService.ShowInfo("Please select current BL.");
                return;
            }
            if (string.IsNullOrEmpty(txtVesselName.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VesselName！");
                return;
            }
            if (string.IsNullOrEmpty(txtVoyageNumber.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VoyageNumber！");
                return;
            }
            if (txtVoyageNumber.Text.Trim().Length > 5)
            {
                MessageBoxService.ShowInfo("VoyageNumber is wrong！");
                return;
            }
            //if (cmbBLTitle.SelectedItem.ToString() != "CITY OCEAN LOGISTICS CO.,LTD.")
            //{
            //    MessageBoxService.ShowInfo("Only support 'CITY OCEAN LOGISTICS CO.,LTD.' company！");
            //    return;
            //}
            if (_CurrentBLInfo.ACIEntryType == ACIEntryType.Unknown)
            {
                MessageBoxService.ShowInfo("Please select ACIEntryType.");
                return;
            }
            if (DataSourceForAMSList == null)
                return;
            if (DataSourceForAMSList.Count < 1)
                return;
            StringBuilder errorText = new StringBuilder();
            int rowCount = DataSourceForAMSList.Count;
            int i = 1;
            bool isError = false;
            foreach (OceanHBL2AmsAciIsf oha in DataSourceForAMSList)
            {
                errorText.AppendLine(i.ToString());
                i++;
                if (rowCount > 1)
                {
                    if (string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Must input Mark！");
                        isError = true;
                    }
                }
                else
                    if (!string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Currently there is only one data,mark should be empty！");
                        isError = true;
                    }
                if (string.IsNullOrEmpty(oha.LastPortOfCall))
                {
                    errorText.AppendLine("Must input LastPortOfCall！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.FirstPorOtfCall))
                {
                    errorText.AppendLine("Must input FirstPortOfCall！");
                    isError = true;
                }
                isError = CheckShipperForAMSInfo(errorText, isError, oha);
                if (CheckCustomerForAMSInfo("Consignee", oha.Consignee, ref errorText))
                {
                    isError = true;
                }
                CheckContainerForAMSInfo(errorText, isError, oha);
            }
            if (isError)
            {
                MessageBoxService.ShowInfo(errorText.ToString());
                return;
            }

            #endregion
            string subjuect = string.Empty;
            string toEmail = string.Empty;
            List<Guid> oIds = new List<Guid>();
            oIds.Add(_CurrentBLInfo.OceanBookingID);
            List<Guid> hblIds = new List<Guid>();
            hblIds.Add(_CurrentBLInfo.ID);
            List<string> operationNos = new List<string>();
            operationNos.Add(_CurrentBLInfo.No);
            string key = "ACI";
            string tip = string.Empty;
            bool isSucc = false;
            subjuect = "ACI(" + _CurrentBLInfo.No.ToString() + ")";

            tip = "ACI";
            try
            {
                ClientOceanExportService.SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.ACI, _CurrentBLInfo.CompanyID, null, null);

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }
        }

        private void barISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region 验证
            if (AmsIsChanged) _CurrentBLInfo.IsDirty = true;
            if (_CurrentBLInfo.IsDirty || isSave == false)
            {
                MessageBoxService.ShowInfo("Please save current BL.");
                return;
            }
            if (_CurrentBLInfo.ID == Guid.Empty)
            {
                MessageBoxService.ShowInfo("Please select current BL.");
                return;
            }
            if (string.IsNullOrEmpty(txtVesselName.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VesselName！");
                return;
            }
            if (string.IsNullOrEmpty(txtVoyageNumber.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VoyageNumber！");
                return;
            }
            if (txtVoyageNumber.Text.Trim().Length > 5)
            {
                MessageBoxService.ShowInfo("VoyageNumber is wrong！");
                return;
            }
            //if (cmbBLTitle.SelectedItem.ToString() != "CITY OCEAN LOGISTICS CO.,LTD.")
            //{
            //    MessageBoxService.ShowInfo("Only support 'CITY OCEAN LOGISTICS CO.,LTD.' company！");
            //    return;
            //}
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.Unknown)
            {
                MessageBoxService.ShowInfo("Please select ISFEntryType.");
                return;
            }
            bool isStayOnBoard = false;
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.StayonBoard)
                isStayOnBoard = true;
            if (DataSourceForAMSList == null)
                return;
            if (DataSourceForAMSList.Count < 1)
                return;
            StringBuilder errorText = new StringBuilder();
            int rowCount = DataSourceForAMSList.Count;
            int i = 1;
            bool isError = false;
            foreach (OceanHBL2AmsAciIsf oha in DataSourceForAMSList)
            {
                errorText.AppendLine(i.ToString());
                i++;
                if (rowCount > 1)
                {
                    if (string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Must input Mark！");
                        isError = true;
                    }
                }
                else
                    if (!string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Currently there is only one data,mark should be empty！");
                        isError = true;
                    }

                if (string.IsNullOrEmpty(oha.ISFImporterID))
                {
                    errorText.AppendLine("Must input ISF Importer Ref# ！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.BondReferenceNumber))
                {
                    errorText.AppendLine("Must input BondRef# ！");
                    isError = true;
                }
                if (oha.CargoTypeForAMS != CargoTypeForAMS.HouseholdGoodsOrPersonalEffects)
                    if (oha.BondActivityCode == BondActivityCode.Unknown)
                    {
                        errorText.AppendLine("Must select BondActivityCode ！");
                        isError = true;
                    }

                //判断税号
                if (CheckRefNumberForAMSInfo("ISF Importer Ref#", oha, ref errorText))
                    isError = true;
                if (CheckRefNumberForAMSInfo("BondRef#", oha, ref errorText))
                    isError = true;

                if (!isStayOnBoard)
                {
                    if (CheckCustomerForAMSInfo("Seller", oha.Seller, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Buyer", oha.Buyer, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Manufacturer", oha.Manufacturer, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("StuffingLocation", oha.StuffingLocation, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Consolidator", oha.Consolidator, ref errorText))
                        isError = true;
                    if (string.IsNullOrEmpty(oha.ConsigneeNumber))
                    {
                        errorText.AppendLine("Must input Consignee# ！");
                        isError = true;
                    }
                    if (string.IsNullOrEmpty(oha.ImporterOfRecordNumber))
                    {
                        errorText.AppendLine("Must input Importer# ！");
                        isError = true;
                    }
                    if (CheckRefNumberForAMSInfo("Consignee#", oha, ref errorText))
                        isError = true;
                    if (CheckRefNumberForAMSInfo("Importer#", oha, ref errorText))
                        isError = true;
                }
                else
                {
                    if (CheckCustomerForAMSInfo("Booking Party", oha.BookingPartyInfo, ref errorText))
                        isError = true;
                }
                if (CheckCustomerForAMSInfo("ShipTo", oha.ShipTo, ref errorText))
                    isError = true;
            }
            if (isError)
            {
                MessageBoxService.ShowInfo(errorText.ToString());
                return;
            }

            #endregion
            string subjuect = string.Empty;
            string toEmail = string.Empty;
            List<Guid> oIds = new List<Guid>();
            oIds.Add(_CurrentBLInfo.OceanBookingID);
            List<Guid> hblIds = new List<Guid>();
            hblIds.Add(_CurrentBLInfo.ID);
            List<string> operationNos = new List<string>();
            operationNos.Add(_CurrentBLInfo.No);
            string key = "ISF";
            string tip = string.Empty;
            bool isSucc = false;
            subjuect = "ISF(" + _CurrentBLInfo.No.ToString() + ")";

            tip = "ISF";
            try
            {
                ClientOceanExportService.SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.ISF, _CurrentBLInfo.CompanyID, null, null);

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }
        }

        private void btnAMSandACI_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region 验证
            if (AmsIsChanged) _CurrentBLInfo.IsDirty = true;
            if (_CurrentBLInfo.IsDirty || isSave == false)
            {
                MessageBoxService.ShowInfo("Please save current BL.");
                return;
            }
            if (_CurrentBLInfo.ID == Guid.Empty)
            {
                MessageBoxService.ShowInfo("Please select current BL.");
                return;
            }
            if (string.IsNullOrEmpty(txtVesselName.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VesselName！");
                return;
            }
            if (string.IsNullOrEmpty(txtVoyageNumber.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VoyageNumber！");
                return;
            }
            if (txtVoyageNumber.Text.Trim().Length > 5)
            {
                MessageBoxService.ShowInfo("VoyageNumber is wrong！");
                return;
            }
            //if (cmbBLTitle.SelectedItem.ToString() != "CITY OCEAN LOGISTICS CO.,LTD.")
            //{
            //    MessageBoxService.ShowInfo("Only support 'CITY OCEAN LOGISTICS CO.,LTD.' company！");
            //    return;
            //}
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.Unknown)
            {
                MessageBoxService.ShowInfo("Please select AMSEntryType.");
                return;
            }
            if (_CurrentBLInfo.ACIEntryType == ACIEntryType.Unknown)
            {
                MessageBoxService.ShowInfo("Please select ACIEntryType.");
                return;
            }
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.StayonBoard && string.IsNullOrEmpty(txtFirstPortOfCallDate.Text))
            {
                MessageBoxService.ShowInfo("Must input First Port Of Call Date.");
                return;
            }
            if (DataSourceForAMSList == null)
                return;
            if (DataSourceForAMSList.Count < 1)
                return;
            StringBuilder errorText = new StringBuilder();
            int rowCount = DataSourceForAMSList.Count;
            int i = 1;
            bool isError = false;
            foreach (OceanHBL2AmsAciIsf oha in DataSourceForAMSList)
            {
                errorText.AppendLine(i.ToString());
                i++;
                if (rowCount > 1)
                {
                    if (string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Must input Mark！");
                        isError = true;
                    }
                }
                else
                    if (!string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Currently there is only one data,mark should be empty！");
                        isError = true;
                    }
                if (string.IsNullOrEmpty(oha.LastPortOfCall))
                {
                    errorText.AppendLine("Must input LastPortOfCall！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.FirstPorOtfCall))
                {
                    errorText.AppendLine("Must input FirstPortOfCall！");
                    isError = true;
                }
                isError = CheckShipperForAMSInfo(errorText, isError, oha);
                if (CheckCustomerForAMSInfo("Consignee", oha.Consignee, ref errorText))
                {
                    isError = true;
                }
                CheckContainerForAMSInfo(errorText, isError, oha);
            }
            if (isError)
            {
                MessageBoxService.ShowInfo(errorText.ToString());
                return;
            }

            #endregion
            string subjuect = string.Empty;
            string toEmail = string.Empty;
            List<Guid> oIds = new List<Guid>();
            oIds.Add(_CurrentBLInfo.OceanBookingID);
            List<Guid> hblIds = new List<Guid>();
            hblIds.Add(_CurrentBLInfo.ID);
            List<string> operationNos = new List<string>();
            operationNos.Add(_CurrentBLInfo.No);
            string key = "AMSACI";
            string tip = string.Empty;
            bool isSucc = false;
            subjuect = "AMS&ACI(" + _CurrentBLInfo.No.ToString() + ")";

            tip = "AMSACI";
            try
            {
                ClientOceanExportService.SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.AMSACI, _CurrentBLInfo.CompanyID, null, null);
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }
        }

        private void barAMSandISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region 验证
            if (AmsIsChanged) _CurrentBLInfo.IsDirty = true;
            if (_CurrentBLInfo.IsDirty)
            {
                MessageBoxService.ShowInfo("Please save current BL.");
                return;
            }
            if (_CurrentBLInfo.ID == Guid.Empty)
            {
                MessageBoxService.ShowInfo("Please select current BL.");
                return;
            }
            if (string.IsNullOrEmpty(txtVesselName.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VesselName！");
                return;
            }
            if (string.IsNullOrEmpty(txtVoyageNumber.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VoyageNumber！");
                return;
            }
            if (txtVoyageNumber.Text.Trim().Length > 5)
            {
                MessageBoxService.ShowInfo("VoyageNumber is wrong！");
                return;
            }
            //if (cmbBLTitle.SelectedItem.ToString() != "CITY OCEAN LOGISTICS CO.,LTD.")
            //{
            //    MessageBoxService.ShowInfo("Only support 'CITY OCEAN LOGISTICS CO.,LTD.' company！");
            //    return;
            //}
            if (string.IsNullOrEmpty(txtIMO.Text))
            {
                MessageBoxService.ShowInfo("Please Get IMO&Flag！");
                return;
            }
            if (string.IsNullOrEmpty(mscmbCountry.EditText))
            {
                MessageBoxService.ShowInfo("Please Get IMO&Flag！");
                return;
            }
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.Unknown)
            {
                MessageBoxService.ShowInfo("Please select AMS/ISF EntryType.");
                return;
            }
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.StayonBoard && string.IsNullOrEmpty(txtFirstPortOfCallDate.Text))
            {
                MessageBoxService.ShowInfo("Must input First Port Of Call Date.");
                return;
            }
            bool isStayOnBoard = false;
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.StayonBoard)
                isStayOnBoard = true;
            if (DataSourceForAMSList == null)
                return;
            if (DataSourceForAMSList.Count < 1)
                return;
            StringBuilder errorText = new StringBuilder();
            int rowCount = DataSourceForAMSList.Count;
            int i = 1;
            bool isError = false;
            foreach (OceanHBL2AmsAciIsf oha in DataSourceForAMSList)
            {
                errorText.AppendLine(i.ToString());
                i++;
                if (rowCount > 1)
                {
                    if (string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Must input Mark！");
                        isError = true;
                    }
                }
                else
                    if (!string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Currently there is only one data,mark should be empty！");
                        isError = true;
                    }
                if (string.IsNullOrEmpty(oha.LastPortOfCall))
                {
                    errorText.AppendLine("Must input LastPortOfCall！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.FirstPorOtfCall))
                {
                    errorText.AppendLine("Must input FirstPortOfCall！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.ISFImporterID))
                {
                    errorText.AppendLine("Must input ISF Importer Ref# ！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.BondReferenceNumber))
                {
                    errorText.AppendLine("Must input BondRef# ！");
                    isError = true;
                }
                if (oha.CargoTypeForAMS != CargoTypeForAMS.HouseholdGoodsOrPersonalEffects)
                    if (oha.BondActivityCode == BondActivityCode.Unknown)
                    {
                        errorText.AppendLine("Must select BondActivityCode ！");
                        isError = true;
                    }
                //判断税号
                if (CheckRefNumberForAMSInfo("ISF Importer Ref#", oha, ref errorText))
                    isError = true;
                if (CheckRefNumberForAMSInfo("BondRef#", oha, ref errorText))
                    isError = true;
                if (!isStayOnBoard)
                {
                    if (CheckCustomerForAMSInfo("Seller", oha.Seller, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Buyer", oha.Buyer, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Manufacturer", oha.Manufacturer, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("StuffingLocation", oha.StuffingLocation, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Consolidator", oha.Consolidator, ref errorText))
                        isError = true;
                    if (string.IsNullOrEmpty(oha.ConsigneeNumber))
                    {
                        errorText.AppendLine("Must input Consignee# ！");
                        isError = true;
                    }
                    if (string.IsNullOrEmpty(oha.ImporterOfRecordNumber))
                    {
                        errorText.AppendLine("Must input Importer# ！");
                        isError = true;
                    }
                    if (CheckRefNumberForAMSInfo("Consignee#", oha, ref errorText))
                        isError = true;
                    if (CheckRefNumberForAMSInfo("Importer#", oha, ref errorText))
                        isError = true;
                }
                else
                {
                    if (CheckCustomerForAMSInfo("Booking Party", oha.BookingPartyInfo, ref errorText))
                        isError = true;
                }
                isError = CheckShipperForAMSInfo(errorText, isError, oha);
                if (CheckCustomerForAMSInfo("Consignee", oha.Consignee, ref errorText))
                    isError = true;
                if (CheckCustomerForAMSInfo("ShipTo", oha.ShipTo, ref errorText))
                    isError = true;
                isError = CheckContainerForAMSInfo(errorText, isError, oha);
            }
            if (isError)
            {
                MessageBoxService.ShowInfo(errorText.ToString());
                return;
            }

            #endregion

            string subjuect = string.Empty;
            string toEmail = string.Empty;
            List<Guid> oIds = new List<Guid>();
            oIds.Add(_CurrentBLInfo.OceanBookingID);
            List<Guid> hblIds = new List<Guid>();
            hblIds.Add(_CurrentBLInfo.ID);
            List<string> operationNos = new List<string>();
            operationNos.Add(_CurrentBLInfo.No);
            string key = "AMSISF";
            string tip = string.Empty;
            bool isSucc = false;
            subjuect = "AMS&ISF(" + _CurrentBLInfo.No.ToString() + ")";

            tip = "AMSISF";
            try
            {
                ClientOceanExportService.SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.AMSISF, _CurrentBLInfo.CompanyID, null, null);

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }
        }

        private void barAMSAndACIAndISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            #region 验证
            if (AmsIsChanged) _CurrentBLInfo.IsDirty = true;
            if (_CurrentBLInfo.IsDirty || isSave == false)
            {
                MessageBoxService.ShowInfo("Please save current BL.");
                return;
            }
            if (_CurrentBLInfo.ID == Guid.Empty)
            {
                MessageBoxService.ShowInfo("Please select current BL.");
                return;
            }
            if (string.IsNullOrEmpty(txtVesselName.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VesselName！");
                return;
            }
            if (string.IsNullOrEmpty(txtVoyageNumber.Text.Trim()))
            {
                MessageBoxService.ShowInfo("Must input VoyageNumber！");
                return;
            }
            if (txtVoyageNumber.Text.Trim().Length > 5)
            {
                MessageBoxService.ShowInfo("VoyageNumber is wrong！");
                return;
            }
            //if (cmbBLTitle.SelectedItem.ToString() != "CITY OCEAN LOGISTICS CO.,LTD.")
            //{
            //    MessageBoxService.ShowInfo("Only support 'CITY OCEAN LOGISTICS CO.,LTD.' company！");
            //    return;
            //}
            if (_CurrentBLInfo.ACIEntryType == ACIEntryType.Unknown)
            {
                MessageBoxService.ShowInfo("Please select ACIEntryType.");
                return;
            }
            if (string.IsNullOrEmpty(txtIMO.Text))
            {
                MessageBoxService.ShowInfo("Please Get IMO&Flag！");
                return;
            }
            if (string.IsNullOrEmpty(mscmbCountry.EditText))
            {
                MessageBoxService.ShowInfo("Please Get IMO&Flag！");
                return;
            }
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.Unknown)
            {
                MessageBoxService.ShowInfo("Please select AMS/ISF EntryType.");
                return;
            }
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.StayonBoard && string.IsNullOrEmpty(txtFirstPortOfCallDate.Text))
            {
                MessageBoxService.ShowInfo("Must input First Port Of Call Date.");
                return;
            }
            bool isStayOnBoard = false;
            if (_CurrentBLInfo.AMSEntry == AMSEntryType.StayonBoard)
                isStayOnBoard = true;
            if (DataSourceForAMSList == null)
                return;
            if (DataSourceForAMSList.Count < 1)
                return;
            StringBuilder errorText = new StringBuilder();
            int rowCount = DataSourceForAMSList.Count;
            int i = 1;
            bool isError = false;
            foreach (OceanHBL2AmsAciIsf oha in DataSourceForAMSList)
            {
                errorText.AppendLine(i.ToString());
                i++;
                if (rowCount > 1)
                {
                    if (string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Must input Mark！");
                        isError = true;
                    }
                }
                else
                    if (!string.IsNullOrEmpty(oha.Mark))
                    {
                        errorText.AppendLine("Currently there is only one data,mark should be empty！");
                        isError = true;
                    }
                if (string.IsNullOrEmpty(oha.LastPortOfCall))
                {
                    errorText.AppendLine("Must input LastPortOfCall！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.FirstPorOtfCall))
                {
                    errorText.AppendLine("Must input FirstPortOfCall！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.ISFImporterID))
                {
                    errorText.AppendLine("Must input ISF Importer Ref# ！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(oha.BondReferenceNumber))
                {
                    errorText.AppendLine("Must input BondRef# ！");
                    isError = true;
                }
                if (oha.CargoTypeForAMS != CargoTypeForAMS.HouseholdGoodsOrPersonalEffects)
                    if (oha.BondActivityCode == BondActivityCode.Unknown)
                    {
                        errorText.AppendLine("Must select BondActivityCode ！");
                        isError = true;
                    }
                //判断税号
                if (CheckRefNumberForAMSInfo("ISF Importer Ref#", oha, ref errorText))
                    isError = true;
                if (CheckRefNumberForAMSInfo("BondRef#", oha, ref errorText))
                    isError = true;
                if (!isStayOnBoard)
                {
                    if (CheckCustomerForAMSInfo("Seller", oha.Seller, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Buyer", oha.Buyer, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Manufacturer", oha.Manufacturer, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("StuffingLocation", oha.StuffingLocation, ref errorText))
                        isError = true;
                    if (CheckCustomerForAMSInfo("Consolidator", oha.Consolidator, ref errorText))
                        isError = true;
                    if (string.IsNullOrEmpty(oha.ConsigneeNumber))
                    {
                        errorText.AppendLine("Must input Consignee# ！");
                        isError = true;
                    }
                    if (string.IsNullOrEmpty(oha.ImporterOfRecordNumber))
                    {
                        errorText.AppendLine("Must input Importer# ！");
                        isError = true;
                    }
                    if (CheckRefNumberForAMSInfo("Consignee#", oha, ref errorText))
                        isError = true;
                    if (CheckRefNumberForAMSInfo("Importer#", oha, ref errorText))
                        isError = true;
                }
                else
                {
                    if (CheckCustomerForAMSInfo("Booking Party", oha.BookingPartyInfo, ref errorText))
                        isError = true;
                }
                isError = CheckShipperForAMSInfo(errorText, isError, oha);
                if (CheckCustomerForAMSInfo("Consignee", oha.Consignee, ref errorText))
                    isError = true;
                if (CheckCustomerForAMSInfo("ShipTo", oha.ShipTo, ref errorText))
                    isError = true;
                isError = CheckContainerForAMSInfo(errorText, isError, oha);
            }
            if (isError)
            {
                MessageBoxService.ShowInfo(errorText.ToString());
                return;
            }

            #endregion

            string subjuect = string.Empty;
            string toEmail = string.Empty;
            List<Guid> oIds = new List<Guid>();
            oIds.Add(_CurrentBLInfo.OceanBookingID);
            List<Guid> hblIds = new List<Guid>();
            hblIds.Add(_CurrentBLInfo.ID);
            List<string> operationNos = new List<string>();
            operationNos.Add(_CurrentBLInfo.No);
            string key = "AMSACIISF";
            string tip = string.Empty;
            bool isSucc = false;
            subjuect = "AMS&ACI&ISF(" + _CurrentBLInfo.No.ToString() + ")";

            tip = "AMSACIISF";
            try
            {
                ClientOceanExportService.SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.AMSACIISF, _CurrentBLInfo.CompanyID, null, null);

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }
        }


        /// <summary>
        /// 验证AMS箱信息
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="isError"></param>
        /// <param name="oha"></param>
        /// <returns></returns>
        private static bool CheckContainerForAMSInfo(StringBuilder errorText, bool isError, OceanHBL2AmsAciIsf oha)
        {
            foreach (ContainerForAMS c in oha.Container)
            {
                if (string.IsNullOrEmpty(c.ContainerNumber))
                {
                    errorText.AppendLine("Must input ContainerNumber！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(c.Seal))
                {
                    errorText.AppendLine("Must input Seal！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(c.Kilos))
                {
                    errorText.AppendLine("Must input Kilos！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(c.Quantity))
                {
                    errorText.AppendLine("Must input Quantity！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(c.UnitOfMeasure) || c.UnitOfMeasure.Length > 3)
                {
                    errorText.AppendLine("Unit (\"" + c.UnitOfMeasure + "\") is wrong！");
                    isError = true;
                }
                if (string.IsNullOrEmpty(c.FreeFormDescription))
                {
                    errorText.AppendLine("Must input FreeFormDescription！");
                    isError = true;
                }
            }
            return isError;
        }
        /// <summary>
        /// 验证amsShipper信息
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="isError"></param>
        /// <param name="oha"></param>
        /// <returns></returns>
        private static bool CheckShipperForAMSInfo(StringBuilder errorText, bool isError, OceanHBL2AmsAciIsf oha)
        {
            if (string.IsNullOrEmpty(oha.Shipper.Name))
            {
                errorText.AppendLine("Must input ShipperName！");
                isError = true;
            }
            if (string.IsNullOrEmpty(oha.Shipper.Address))
            {
                errorText.AppendLine("Must input ShipperAddress！");
                isError = true;
            }
            if (string.IsNullOrEmpty(oha.Shipper.City))
            {
                errorText.AppendLine("Must input ShipperCity！");
                isError = true;
            }
            if (string.IsNullOrEmpty(oha.Shipper.Country))
            {
                errorText.AppendLine("Must input ShipperCountry！");
                isError = true;
            }
            return isError;
        }
        /// <summary>
        /// 判断联系人信息
        /// </summary>
        /// <param name="type">联系人类型（Consige/Seller/buyer……）</param>
        /// <param name="errorText">错误信息ref</param>
        /// <param name="customer">联系人信息</param>
        /// <returns></returns>
        private static bool CheckCustomerForAMSInfo(string type, CustomerDescriptionForAMS customer, ref StringBuilder errorText)
        {
            bool isError = false;
            if (string.IsNullOrEmpty(customer.Name))
            {
                errorText.AppendLine("Must input " + type + " Name！");
                isError = true;
            }
            if (string.IsNullOrEmpty(customer.Address))
            {
                errorText.AppendLine("Must input " + type + " Address！");
                isError = true;
            }
            if (string.IsNullOrEmpty(customer.City))
            {
                errorText.AppendLine("Must input " + type + " City！");
                isError = true;
            }
            if (string.IsNullOrEmpty(customer.Country))
            {
                errorText.AppendLine("Must input " + type + " Country！");
                isError = true;
            }
            if (type == "Consignee" || type == "Buyer" || type == "ShipTo")
                if (string.IsNullOrEmpty(customer.Zip))
                {
                    errorText.AppendLine("Must input " + type + " Zip！");
                    isError = true;
                }
            return isError;
        }


        private void barBill_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(_CurrentBLInfo.OceanBookingID, OperationType.OceanExport);
                if (operationCommonInfo != null)
                {
                    operationCommonInfo.CurrentFormID = _CurrentBLInfo.ID;
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    MessageBoxService.ShowInfo(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }

        private void barReplyAgent_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool srcc = FCMCommonClientService.OpenAgentRequestPart(_CurrentBLInfo.OceanBookingID, OperationType.OceanExport, null, null);
            if (srcc)
            {
                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.IsRequestAgent = true;
                stxtAgent.Enabled = false;
                _CurrentBLInfo.IsDirty = isDirty;
            }
        }

        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                RefreshBLData();
                if (xtraTabControl1.SelectedTabPage.Name == "tabPageAMS")
                {
                    LoadAmsAndContainer();
                }

            }
        }

        private void RefreshBLData()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID)) return;

            Focus();
            _CurrentBLInfo = OceanExportService.GetOceanHBLInfo(_CurrentBLInfo.ID);
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
            bindingSource1.DataSource = _CurrentBLInfo;
            bindingSource1.ResetBindings(false);
            InitCustomerDescriptionObject();
            Refresh();

            // }
        }

        #endregion
        private delegate void DataBindDelegate(OceanHBLInfo hblInfo);

        #region IEditPart 成员
        void BindingData(OceanHBLInfo hblInfo)
        {
            if (hblInfo != null)
            {
                InnerBindData(hblInfo);
            }
            else
            {

                string operationNo = stateValues["OperationNo"] as string;
                string hblNo = stateValues["HBLNo"] as string;

                OceanHBLInfo temp = OceanExportService.GetOceanHBLInfo(operationNo, hblNo.TrimStart());

                InnerBindData(temp);

            }
        }
        private void InnerBindData(OceanHBLInfo hblInfo)
        {


            _CurrentBLInfo = hblInfo;
            bindingSource1.DataSource = _CurrentBLInfo;

            _CurrentBLInfo.IsChargePayOrCon = false;
            isCtnCharge = false;

            isUpdateShipper = _CurrentBLInfo.ShipperID == _CurrentBLInfo.BookingPartyID ? true : false;

            InitMessage();
            InitControls();
            RefreshBarEnabled();
            InitCustomerDescriptionObject();
            _CurrentBLInfo.IsDirty = false;
            AmsIsChanged = false;
            _CurrentBLInfo.CancelEdit();
            bindingSource1.CancelEdit();

            if (_businessOperationParameter != null && _businessOperationParameter.ActionType == ActionType.Create)
            {
                string refNo = _businessOperationParameter.Context.OperationNO;
                stxtRefNo.Text = refNo;
                stxtRefNo.PerformClick(stxtRefNo.Properties.Buttons[0]);
            }
        }

        public override object DataSource
        {
            get { return bindingSource1.DataSource; }
            set { BindingData(value as OceanHBLInfo); }
        }

        public override bool SaveData()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                return Save();
            }
        }

        public override void EndEdit()
        {
            _CurrentBLInfo.VoyageShowType = OceanExportPrintHelper.GetVoyageShowTypeByVoyageCheck(chkShowPreVoyage, chkShowVoyage);
            Validate();
            bindingSource1.EndEdit();
        }

        public override event SavedHandler Saved;

        #endregion

        #region 把客户端对象转换为服务保存方法所需的对象

        SaveHBLInfoParameter ConvertHBLToParameter(bool IsSaveAs, OceanHBLInfo info)
        {
            DateTime? dt = info.UpdateDate;
            if (info.UpdateDate.HasValue)
            {
                dt = DateTime.SpecifyKind(info.UpdateDate.Value, DateTimeKind.Unspecified);
            }

            if (stxtNotifyParty.EditValue == null || stxtNotifyParty.EditValue.ToString() == "")
            {
                info.NotifyPartyID = null;
                //info.NotifyPartyName = "";
                //info.NotifyPartyDescription = null;
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
                bLTitleID = info.BLTitleID,
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
                PreETD = _CurrentBLInfo.PreETD,
                // CSCLGateIn = _CurrentBLInfo.CSCLGateIn,
                IsToAgent = _CurrentBLInfo.IsToAgent,
                IsBuildCSCLMemo = IBM,
                isThirdPlacePayOrder = info.IsThirdPlacePayOrder,
                bookingPartyID = info.BookingPartyID,
                collectbyAgentOrderID = info.CollectbyAgentOrderID,
                scacCode = info.ScacCode,
                DeclareNo = info.DeclareNo,
                GateInDate = info.GateInDate
            };

            return parameter;
        }

        SaveBLContainerParameter ConvertCtnToParameter(bool IsSaveAs, Guid oceanBookingID, List<OceanBLContainerList> list)
        {

            List<OceanBLContainerList> ulist = new List<OceanBLContainerList>();
            //if (!ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID))
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
            List<decimal> p_containerVGMCrossWeights = new List<decimal>();
            List<string> p_containerVGMMethods = new List<string>();
            List<string> p_containerCTNOpers = new List<string>();

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
                p_containerVGMCrossWeights.Add(item.VGMCrossWeight);
                p_containerVGMMethods.Add(item.VGMMethod);
                p_containerCTNOpers.Add(item.CTNOper);


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
                containerVGMCrossWeights = p_containerVGMCrossWeights.ToArray(),
                containerVGMMethods = p_containerVGMMethods.ToArray(),
                containerCTNOpers = p_containerCTNOpers.ToArray(),
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
        //private void cmbBlTitle_SelectValueChanging(object sender, EventArgs e)
        //{
        //    if (_bookingInfo != null)
        //    {
        //        if (!string.IsNullOrEmpty(_bookingInfo.ShippingLineName))
        //        {
        //            _CurrentBLInfo.BeginEdit();
        //            if (arrAMSCTYO.Contains(_bookingInfo.ShippingLineName)&& !string.IsNullOrEmpty(_CurrentBLInfo.AMSNo))
        //            {
        //                if ((Guid)cmbBLTitle.EditValue == cTYOID)
        //                { 
        //                    _CurrentBLInfo.AMSNo = !_CurrentBLInfo.AMSNo.StartsWith("CTYO") ? "CTY0" + txtNo.Text.Trim() : txtNo.Text.Trim();
        //                    return;
        //                }
        //                if ((Guid)cmbBLTitle.EditValue == tPHJID)
        //                {
        //                    _CurrentBLInfo.AMSNo = !_CurrentBLInfo.AMSNo.StartsWith("TPHJ") ? "TPHJ" + txtNo.Text.Trim() : txtNo.Text.Trim();
        //                    return;
        //                }
        //            }
        //            if (arrAMS8FH5.Contains(_bookingInfo.ShippingLineName))
        //            {
        //                if ((Guid)cmbBLTitle.EditValue == hARVESTID)
        //                {
        //                    _CurrentBLInfo.AMSNo = !_CurrentBLInfo.AMSNo.StartsWith("8FH5") ? "8FH5" + txtNo.Text.Trim() : txtNo.Text.Trim();
        //                    return;
        //                }
        //            }
        //            else
        //            {
        //                _CurrentBLInfo.AMSNo = txtNo.Text.Trim();
        //            }
        //        }
        //    }
        //}

        private BusinessOperationContext GetContext(OceanHBLInfo orderInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = orderInfo.OceanBookingID;
            context.OperationNO = orderInfo.RefNo;
            context.OperationType = OperationType.OceanExport;
            context.FormId = orderInfo.OceanBookingID;
            context.FormType = FormType.Booking;
            return context;
        }

        private void txtNo_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (string.IsNullOrEmpty(_CurrentBLInfo.AMSNo))
                if (!string.IsNullOrEmpty(txtNo.Text.Trim()))
                {
                    if (_bookingInfo != null)
                    {
                        if (_CurrentBLInfo.BookingPartyID.ToString().ToUpper() != "0751E34D-6FC6-E511-938F-0026551CA878")
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
                                    _CurrentBLInfo.AMSNo = "8F6P" + txtNo.Text.Trim();
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
                        else
                        {
                            if (!string.IsNullOrEmpty(_bookingInfo.ShippingLineName))
                            {

                                if (arrAMSCTYO.Contains(_bookingInfo.ShippingLineName))
                                {
                                    _CurrentBLInfo.AMSNo = "DWSD" + txtNo.Text.Trim();
                                    return;
                                }


                                if (arrAMS8FH5.Contains(_bookingInfo.ShippingLineName))
                                {
                                    _CurrentBLInfo.AMSNo = "8F9G" + txtNo.Text.Trim();
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
        }

        #endregion

        private void txtNo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ReleaseType_Click(object sender, EventArgs e)
        {
            if (_CurrentBLInfo.ReleaseDate != null)
            {
                MessageBoxService.ShowInfo(NativeLanguageService.GetText(this, "ReleaseChange"));
                cmbReleaseType.Enabled = false;
            }
        }

        private void chkToAgent_Click(object sender, EventArgs e)
        {
            isToAgentChange = true;
            if (_CurrentBLInfo.ReleaseDate != null)
            {
                MessageBoxService.ShowInfo(NativeLanguageService.GetText(this, "IsToAgent"));
                chkToAgent.Enabled = false;
            }
        }

        #region Same as ……

        private void ckSeller_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSeller.Checked)
            {
                if (CurrentRowDataForAMS.Shipper == null)
                {
                    ckSeller.Checked = false;
                    return;
                }
                CurrentRowDataForAMS.Seller.Name = CurrentRowDataForAMS.Shipper.Name;
                CurrentRowDataForAMS.Seller.Address = CurrentRowDataForAMS.Shipper.Address;
                CurrentRowDataForAMS.Seller.Country = CurrentRowDataForAMS.Shipper.Country;
                CurrentRowDataForAMS.Seller.City = CurrentRowDataForAMS.Shipper.City;
                CurrentRowDataForAMS.Seller.Zip = CurrentRowDataForAMS.Shipper.Zip;
                SellerDescription.SetDataBinding(CurrentRowDataForAMS.Seller);
            }
            else
            {
                CurrentRowDataForAMS.Seller.Name = string.Empty;
                CurrentRowDataForAMS.Seller.Address = string.Empty;
                CurrentRowDataForAMS.Seller.Country = string.Empty;
                CurrentRowDataForAMS.Seller.City = string.Empty;
                CurrentRowDataForAMS.Seller.Zip = string.Empty;
                SellerDescription.SetDataBinding(CurrentRowDataForAMS.Seller);
            }
        }

        private void ckBuyer_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBuyer.Checked)
            {
                if (CurrentRowDataForAMS.Consignee == null)
                {
                    ckBuyer.Checked = false;
                    return;
                }
                CurrentRowDataForAMS.Buyer.Name = CurrentRowDataForAMS.Consignee.Name;
                CurrentRowDataForAMS.Buyer.Address = CurrentRowDataForAMS.Consignee.Address;
                CurrentRowDataForAMS.Buyer.Country = CurrentRowDataForAMS.Consignee.Country;
                CurrentRowDataForAMS.Buyer.City = CurrentRowDataForAMS.Consignee.City;
                CurrentRowDataForAMS.Buyer.Zip = CurrentRowDataForAMS.Consignee.Zip;
                BuyerDescription.SetDataBinding(CurrentRowDataForAMS.Buyer);
            }
            else
            {
                CurrentRowDataForAMS.Buyer.Name = string.Empty;
                CurrentRowDataForAMS.Buyer.Address = string.Empty;
                CurrentRowDataForAMS.Buyer.Country = string.Empty;
                CurrentRowDataForAMS.Buyer.City = string.Empty;
                CurrentRowDataForAMS.Buyer.Zip = string.Empty;
                BuyerDescription.SetDataBinding(CurrentRowDataForAMS.Buyer);
            }
        }

        private void ckShipTo_CheckedChanged(object sender, EventArgs e)
        {
            if (ckShipTo.Checked)
            {
                if (CurrentRowDataForAMS.Buyer == null)
                {
                    ckShipTo.Checked = false;
                    return;
                }
                CurrentRowDataForAMS.ShipTo.Name = CurrentRowDataForAMS.Buyer.Name;
                CurrentRowDataForAMS.ShipTo.Address = CurrentRowDataForAMS.Buyer.Address;
                CurrentRowDataForAMS.ShipTo.Country = CurrentRowDataForAMS.Buyer.Country;
                CurrentRowDataForAMS.ShipTo.City = CurrentRowDataForAMS.Buyer.City;
                CurrentRowDataForAMS.ShipTo.Zip = CurrentRowDataForAMS.Buyer.Zip;
                ShiptoDescription.SetDataBinding(CurrentRowDataForAMS.ShipTo);
            }
            else
            {
                CurrentRowDataForAMS.ShipTo.Name = string.Empty;
                CurrentRowDataForAMS.ShipTo.Address = string.Empty;
                CurrentRowDataForAMS.ShipTo.Country = string.Empty;
                CurrentRowDataForAMS.ShipTo.City = string.Empty;
                CurrentRowDataForAMS.ShipTo.Zip = string.Empty;
                ShiptoDescription.SetDataBinding(CurrentRowDataForAMS.ShipTo);
            }
        }

        private void ckManufacturer_CheckedChanged(object sender, EventArgs e)
        {
            if (ckManufacturer.Checked)
            {
                if (CurrentRowDataForAMS.Seller == null)
                {
                    ckShipTo.Checked = false;
                    return;
                }
                CurrentRowDataForAMS.Manufacturer.Name = CurrentRowDataForAMS.Seller.Name;
                CurrentRowDataForAMS.Manufacturer.Address = CurrentRowDataForAMS.Seller.Address;
                CurrentRowDataForAMS.Manufacturer.Country = CurrentRowDataForAMS.Seller.Country;
                CurrentRowDataForAMS.Manufacturer.City = CurrentRowDataForAMS.Seller.City;
                CurrentRowDataForAMS.Manufacturer.Zip = CurrentRowDataForAMS.Seller.Zip;
                ManuDescription.SetDataBinding(CurrentRowDataForAMS.Manufacturer);
            }
            else
            {
                CurrentRowDataForAMS.Manufacturer.Name = string.Empty;
                CurrentRowDataForAMS.Manufacturer.Address = string.Empty;
                CurrentRowDataForAMS.Manufacturer.Country = string.Empty;
                CurrentRowDataForAMS.Manufacturer.City = string.Empty;
                CurrentRowDataForAMS.Manufacturer.Zip = string.Empty;
                ManuDescription.SetDataBinding(CurrentRowDataForAMS.Manufacturer);
            }
        }

        private void ckStuffingLocation_CheckedChanged(object sender, EventArgs e)
        {
            if (ckStuffingLocation.Checked)
            {
                if (CurrentRowDataForAMS.Seller == null)
                {
                    ckStuffingLocation.Checked = false;
                    return;
                }
                CurrentRowDataForAMS.StuffingLocation.Name = CurrentRowDataForAMS.Seller.Name;
                CurrentRowDataForAMS.StuffingLocation.Address = CurrentRowDataForAMS.Seller.Address;
                CurrentRowDataForAMS.StuffingLocation.Country = CurrentRowDataForAMS.Seller.Country;
                CurrentRowDataForAMS.StuffingLocation.City = CurrentRowDataForAMS.Seller.City;
                CurrentRowDataForAMS.StuffingLocation.Zip = CurrentRowDataForAMS.Seller.Zip;
                StuffingDescription.SetDataBinding(CurrentRowDataForAMS.StuffingLocation);
            }
            else
            {
                CurrentRowDataForAMS.StuffingLocation.Name = string.Empty;
                CurrentRowDataForAMS.StuffingLocation.Address = string.Empty;
                CurrentRowDataForAMS.StuffingLocation.Country = string.Empty;
                CurrentRowDataForAMS.StuffingLocation.City = string.Empty;
                CurrentRowDataForAMS.StuffingLocation.Zip = string.Empty;
                StuffingDescription.SetDataBinding(CurrentRowDataForAMS.StuffingLocation);
            }
        }

        private void ckConsolidator_CheckedChanged(object sender, EventArgs e)
        {
            if (ckConsolidator.Checked)
            {
                if (CurrentRowDataForAMS.StuffingLocation == null)
                {
                    ckShipTo.Checked = false;
                    return;
                }
                CurrentRowDataForAMS.Consolidator.Name = CurrentRowDataForAMS.StuffingLocation.Name;
                CurrentRowDataForAMS.Consolidator.Address = CurrentRowDataForAMS.StuffingLocation.Address;
                CurrentRowDataForAMS.Consolidator.Country = CurrentRowDataForAMS.StuffingLocation.Country;
                CurrentRowDataForAMS.Consolidator.City = CurrentRowDataForAMS.StuffingLocation.City;
                CurrentRowDataForAMS.Consolidator.Zip = CurrentRowDataForAMS.StuffingLocation.Zip;
                ConsolidatorDescription.SetDataBinding(CurrentRowDataForAMS.Consolidator);
            }
            else
            {
                CurrentRowDataForAMS.Consolidator.Name = string.Empty;
                CurrentRowDataForAMS.Consolidator.Address = string.Empty;
                CurrentRowDataForAMS.Consolidator.Country = string.Empty;
                CurrentRowDataForAMS.Consolidator.City = string.Empty;
                CurrentRowDataForAMS.Consolidator.Zip = string.Empty;
                ConsolidatorDescription.SetDataBinding(CurrentRowDataForAMS.Consolidator);
            }
        }

        /// <summary>
        /// consigneeRef与ISFImporterRef相同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckConsigneeRef_CheckedChanged(object sender, EventArgs e)
        {
            if (((ImportRefType)Enum.Parse(typeof(ImportRefType), cmbISFImporterRef.SelectedItem.ToString())) == ImportRefType.SCAC)
            {
                ckConsigneeRef.Checked = false;
                return;
            }
            if (ckConsigneeRef.Checked)
            {
                CurrentRowDataForAMS.ConsigneeNumber = txtISFImporterRef.EditValue.ToString();
                CurrentRowDataForAMS.ConsigneeNumberQualifier = (ConsigneeAndBuyerType)Enum.Parse(typeof(ConsigneeAndBuyerType), cmbISFImporterRef.SelectedItem.ToString());
                CurrentRowDataForAMS.ConsigneeFirstName = txtISFImporterRefFirstName.EditValue.ToString();
                CurrentRowDataForAMS.ConsigneeLastName = txtISFImporterRefLastName.EditValue.ToString();
                CurrentRowDataForAMS.ConsigneePassportIssuanceCountryName = cmbISFImporterRefCountry.EditText;
                CurrentRowDataForAMS.ConsigneePassportIssuanceCountry = cmbISFImporterRefCountry.EditValue == null || cmbISFImporterRefCountry.EditValue == DBNull.Value ? Guid.Empty : (Guid)cmbISFImporterRefCountry.EditValue;
                CurrentRowDataForAMS.ConsigneePassportDOB = CurrentRowDataForAMS.ISFImporterDateOfBirth;
            }
            else
            {
                CurrentRowDataForAMS.ConsigneeNumber = string.Empty;
                CurrentRowDataForAMS.ConsigneeNumberQualifier = ConsigneeAndBuyerType.Unknown;
                CurrentRowDataForAMS.ConsigneeFirstName = string.Empty;
                CurrentRowDataForAMS.ConsigneeLastName = string.Empty;
                CurrentRowDataForAMS.ConsigneePassportIssuanceCountryName = string.Empty;
                CurrentRowDataForAMS.ConsigneePassportIssuanceCountry = Guid.Empty;
                CurrentRowDataForAMS.ConsigneePassportDOB = null;
            }
        }
        /// <summary>
        /// BuyerRef与ISFImporterRef相同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckBuyerRef_CheckedChanged(object sender, EventArgs e)
        {
            if (((ImportRefType)Enum.Parse(typeof(ImportRefType), cmbISFImporterRef.SelectedItem.ToString())) == ImportRefType.SCAC)
            {
                ckConsigneeRef.Checked = false;
                return;
            }
            if (ckBuyerRef.Checked)
            {
                CurrentRowDataForAMS.ImporterOfRecordNumber = txtISFImporterRef.EditValue.ToString();
                CurrentRowDataForAMS.ImporterOfRecordNumberQualifier = (ConsigneeAndBuyerType)Enum.Parse(typeof(ConsigneeAndBuyerType), cmbISFImporterRef.SelectedItem.ToString());
                CurrentRowDataForAMS.ImporterOfRecordFirstName = txtISFImporterRefFirstName.EditValue.ToString();
                CurrentRowDataForAMS.ImporterOfRecordLastName = txtISFImporterRefLastName.EditValue.ToString();
                CurrentRowDataForAMS.ImporterOfPassportIssuanceCountryName = cmbISFImporterRefCountry.EditText;
                CurrentRowDataForAMS.ImporterOfPassportIssuanceCountry = cmbISFImporterRefCountry.EditValue == null || cmbISFImporterRefCountry.EditValue == DBNull.Value ? Guid.Empty : (Guid)cmbISFImporterRefCountry.EditValue;
                CurrentRowDataForAMS.ImporterOfRecordDOB = CurrentRowDataForAMS.ISFImporterDateOfBirth;
            }
            else
            {
                CurrentRowDataForAMS.ImporterOfRecordNumber = string.Empty;
                CurrentRowDataForAMS.ImporterOfRecordNumberQualifier = ConsigneeAndBuyerType.Unknown;
                CurrentRowDataForAMS.ImporterOfRecordFirstName = string.Empty;
                CurrentRowDataForAMS.ImporterOfRecordLastName = string.Empty;
                CurrentRowDataForAMS.ImporterOfPassportIssuanceCountryName = string.Empty;
                CurrentRowDataForAMS.ImporterOfPassportIssuanceCountry = Guid.Empty;
                CurrentRowDataForAMS.ImporterOfRecordDOB = null;
            }
        }
        #endregion
        string vesselName = string.Empty;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            simpleButton1.Enabled = false;
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "正在获取IMO/Flag...");

            vesselName = txtVesselName.Text.Trim().Replace(' ', '+');
            if (!string.IsNullOrEmpty(vesselName))
            {
                string html = GetIMOandFlag(vesselName);
                //分析数据
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                HtmlNode tableNode = null;
                HtmlNode tableType = null;
                for (int i = 2; i < 10; i++)//驳船信息从第二行开始  取前10寻找
                {
                    tableNode = doc.DocumentNode.SelectSingleNode("/html/body/table/tr[" + i + "]/td[2]");//船名
                    tableType = doc.DocumentNode.SelectSingleNode("/html/body/table/tr[" + i + "]/td[6]");//驳船类型

                    if (tableType != null && tableType.OuterHtml != null)
                    {
                        if (tableType.OuterHtml.Contains("Container Ship"))//找货运船
                        {
                            break;
                        }
                    }


                    if (tableNode == null || tableType == null)
                    {
                        tableNode = doc.DocumentNode.SelectSingleNode("/html/body/table/tr[2]/td[2]");
                        break;
                    }
                }

                string imo = string.Empty;
                if (tableNode == null)
                {
                    MessageBoxService.ShowInfo("未找到相应的IMO&Flag代码！");
                    txtIMO.Text = string.Empty;
                    mscmbCountry.EditValue = Guid.Empty;
                    mscmbCountry.EditText = string.Empty;
                    ImoFlag imoflag = new ImoFlag();
                    imoflag.VesselName = vesselName;
                    imoflag.Show();
                    simpleButton1.Enabled = true;
                    return;
                }
                imo = tableNode.InnerText.Substring(0, 7);
                //imo是数字
                int res = 0;
                if (!int.TryParse(imo, out res))
                {
                    MessageBoxService.ShowInfo("未找到相应的IMO&Flag代码！");
                    return;
                }
                string flag = doc.DocumentNode.SelectSingleNode("/html/body/table/tr[2]/td[3]").InnerText;
                flag = flag == "UK" ? "GB" : flag.Replace("&nbsp;", "");
                txtIMO.Text = imo;
                //Flag
                List<CountryList> countryList = ICPCommUIHelper.GeographyService.GetCountryList(string.Empty, flag, true, 1);

                //显示IMO和Flag
                if (countryList.Count > 0)
                {
                    Guid countryId = countryList[0].ID;//
                    mscmbCountry.EditValue = countryId;
                    mscmbCountry.EditText = LocalData.IsEnglish ? countryList[0].EName : countryList[0].CName;
                }
                else
                {
                    MessageBoxService.ShowInfo("未找到代码为" + flag + "的国家！");
                    ImoFlag imoflag = new ImoFlag();
                    imoflag.VesselName = vesselName;
                    imoflag.Show();
                }
            }
            else
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), "请输入船名！");
            }
            simpleButton1.Enabled = true;
        }
        SendHead oSendHead = new SendHead();
        Http oHttp = new Http();
        public string GetIMOandFlag(string vesselName)
        {
            string url = "http://shipnumber.com/";
            Uri actionUrl = new Uri(url);
            oSendHead.Method = "get";
            oSendHead.Referer = url;
            oSendHead.Host = "http://" + actionUrl.Host;
            if (url.IndexOf("https://") >= 0) oSendHead.Host = "https://" + actionUrl.Host;
            oSendHead.Action = actionUrl.PathAndQuery;
            oSendHead.AcceptLanguage = "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3";

            oHttp.Send(ref oSendHead);
            oSendHead.Cookies = oHttp.Cookies;
            oSendHead.Referer = url;
            oSendHead.Method = "post";
            oSendHead.PostData = "name=" + vesselName + "&s=search&ac=a";//post
            oHttp.Send(ref oSendHead);
            oSendHead.Cookies = oHttp.Cookies;
            return oSendHead.Html;
        }

        /// <summary>
        /// 标记AMS是否更改 
        /// </summary>
        bool AmsIsChanged = false;

        /// <summary>
        /// 列表数据源
        /// </summary>
        public List<OceanHBL2AmsAciIsf> DataSourceForAMSList
        {
            get
            {
                return bindingAMSACIISF.List as List<OceanHBL2AmsAciIsf>;
            }
            set
            {
                if (bindingAMSACIISF.DataSource != value)
                {
                    bindingAMSACIISF.DataSource = value;
                }
            }
        }
        /// <summary>
        /// 列表当前行
        /// </summary>
        public OceanHBL2AmsAciIsf CurrentRowDataForAMS
        {
            get
            {
                return bindingAMSACIISF.Current as OceanHBL2AmsAciIsf;
            }
        }
        /// <summary>
        /// 列表数据源Container
        /// </summary>
        public List<ContainerForAMS> DataSourceForContainerList
        {
            get
            {
                return bindingContainers.List as List<ContainerForAMS>;
            }
            set
            {
                if (bindingContainers.DataSource != value)
                {
                    bindingContainers.DataSource = value;
                }
            }
        }
        /// <summary>
        /// 列表当前行
        /// </summary>
        public ContainerForAMS CurrentRowDataForContainer
        {
            get
            {
                return bindingContainers.Current as ContainerForAMS;
            }
        }

        /// <summary>
        /// 记录当前HBL的所有箱号和封条号
        /// </summary>
        public List<ContainerForAMS> ContainerNum { get; set; }

        /// <summary>
        /// 切换选项卡时加载AMSACIISF信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage.Name == "tabPageAMS")
            {
                if ((AMSEntryType)Enum.Parse(typeof(AMSEntryType), cmbAMSEntryType.SelectedItem.ToString()) == AMSEntryType.StayonBoard)
                    StayOnBoard(false);
                barSubEDI.Enabled = true;
                if (DataSourceForAMSList == null || DataSourceForAMSList.Count == 0)
                {
                    LoadPartyDesc();
                    LoadAmsAndContainer();

                    OEUtility.SetEnterToExecuteOnec(mscmbHS1, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS1);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS2, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS2);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS3, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS3);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS4, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS4);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS5, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS5);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS6, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS6);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS7, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS7);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS8, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS8);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS9, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS9);
                    });
                    OEUtility.SetEnterToExecuteOnec(mscmbHS10, delegate
                    {
                        ICPCommUIHelper.SetMcmbCountry(mscmbHS10);
                    });
                    #region last port and first port
                    DataFindClientService.Register(txtLastPortOfCall, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                  delegate(object inputSource, object[] resultData)
                  {
                      Guid portID = new Guid(resultData[0].ToString());
                      txtLastPortOfCall.Text = resultData[1].ToString();
                  },
                  delegate
                  {
                      txtLastPortOfCall.Text = string.Empty;
                  },
                  ClientConstants.MainWorkspace);

                    DataFindClientService.Register(txtFirstPortOfCall, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                    delegate(object inputSource, object[] resultData)
                    {
                        Guid portID = new Guid(resultData[0].ToString());
                        txtFirstPortOfCall.Text = resultData[1].ToString();
                    },
                    delegate
                    {
                        txtFirstPortOfCall.Text = string.Empty;
                    },
                    ClientConstants.MainWorkspace);

                    DataFindClientService.Register(txtPOL, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                    delegate(object inputSource, object[] resultData)
                    {
                        Guid portID = new Guid(resultData[0].ToString());
                        txtPOL.Text = resultData[1].ToString();
                    },
                    delegate
                    {
                        txtPOL.Text = string.Empty;
                    },
                    ClientConstants.MainWorkspace);
                    #endregion
                    isSave = true;
                }
            }
            else
            {
                barSubEDI.Enabled = false;
            }
        }
        /// <summary>
        /// 船名航次是否更改
        /// </summary>
        bool VesselVoyageIsChanged = false;
        private void LoadAmsAndContainer()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID)) return;
            //加载数据
            if (DataSourceForAMSList == null)
                DataSourceForAMSList = new List<OceanHBL2AmsAciIsf>();
            if (DataSourceForContainerList == null)
                DataSourceForContainerList = new List<ContainerForAMS>();
            DataSourceForAMSList = OceanExportService.GetAmsAciIsfOjbectsList(_CurrentBLInfo.ID, LocalData.IsEnglish);
            if (ContainerNum == null)
            {
                ContainerNum = OceanExportService.GetContainerNumByHBLID(_CurrentBLInfo.ID);
            }
            if (ContainerNum != null && ContainerNum.Count > 0)
            {
                if (cmbBoxType.Items.Count < 1)
                    foreach (ContainerForAMS c in ContainerNum)
                        cmbBoxType.Items.Add(new ImageComboBoxItem(c.ContainerNumber));
            }

            if (DataSourceForAMSList.Count > 0)
            {
                //加载VesselName/IMO和Flag VoyageNumber信息
                string v = DataSourceForAMSList[0].VesselName;
                if (string.IsNullOrEmpty(v))
                {
                    string vv = _CurrentBLInfo.VesselVoyage;
                    if (!string.IsNullOrEmpty(vv))
                    {
                        txtVesselName.Text = vv.Substring(0, vv.IndexOf("/"));
                        VesselVoyageIsChanged = true;
                    }
                }
                else
                    txtVesselName.Text = v;
                txtIMO.Text = DataSourceForAMSList[0].IMO;
                mscmbCountry.EditText = DataSourceForAMSList[0].FlagName;
                mscmbCountry.EditValue = DataSourceForAMSList[0].Flag;
                string voyageNumber = DataSourceForAMSList[0].VoyageNumber;
                if (string.IsNullOrEmpty(voyageNumber))
                {
                    string vv = _CurrentBLInfo.VesselVoyage;
                    if (!string.IsNullOrEmpty(vv))
                    {
                        string voyage = vv.Substring(vv.IndexOf("/") + 1).Replace("V.", "");
                        if (voyage.Length > 5)
                            if (voyage.IndexOf('-') > 0)
                                txtVoyageNumber.Text = voyage.Substring(0, 4) + voyage.Substring(voyage.Length - 1);
                            else
                                txtVoyageNumber.Text = voyage;
                        else
                            txtVoyageNumber.Text = voyage;
                        VesselVoyageIsChanged = true;
                    }
                }
                else
                    txtVoyageNumber.Text = voyageNumber;
                //加载LastPort和FirstPort
                txtLastPortOfCall.Text = DataSourceForAMSList[0].LastPortOfCall;
                txtFirstPortOfCall.Text = DataSourceForAMSList[0].FirstPorOtfCall;
                txtPOL.Text = DataSourceForAMSList[0].PortOfLoading;
                txtETD.Text = DataSourceForAMSList[0].Etd.ToString();
                txtFirstPortOfCallDate.Text = DataSourceForAMSList[0].FirstPortOfCallDate.ToString();
                #region 加载HSCode
                List<ContainerDetailsForAMS> containerDetails = DataSourceForAMSList[0].ContainerDetails;
                if (containerDetails != null)
                {
                    if (containerDetails.Count == 10)
                    {
                        txtHS1.Text = containerDetails[0].HarmonizedTariffCode;
                        txtHS2.Text = containerDetails[1].HarmonizedTariffCode;
                        txtHS3.Text = containerDetails[2].HarmonizedTariffCode;
                        txtHS4.Text = containerDetails[3].HarmonizedTariffCode;
                        txtHS5.Text = containerDetails[4].HarmonizedTariffCode;
                        txtHS6.Text = containerDetails[5].HarmonizedTariffCode;
                        txtHS7.Text = containerDetails[6].HarmonizedTariffCode;
                        txtHS8.Text = containerDetails[7].HarmonizedTariffCode;
                        txtHS9.Text = containerDetails[8].HarmonizedTariffCode;
                        txtHS10.Text = containerDetails[9].HarmonizedTariffCode;
                        mscmbHS1.EditValue = containerDetails[0].CountryOfOrigin;
                        mscmbHS2.EditValue = containerDetails[1].CountryOfOrigin;
                        mscmbHS3.EditValue = containerDetails[2].CountryOfOrigin;
                        mscmbHS4.EditValue = containerDetails[3].CountryOfOrigin;
                        mscmbHS5.EditValue = containerDetails[4].CountryOfOrigin;
                        mscmbHS6.EditValue = containerDetails[5].CountryOfOrigin;
                        mscmbHS7.EditValue = containerDetails[6].CountryOfOrigin;
                        mscmbHS8.EditValue = containerDetails[7].CountryOfOrigin;
                        mscmbHS9.EditValue = containerDetails[8].CountryOfOrigin;
                        mscmbHS10.EditValue = containerDetails[9].CountryOfOrigin;
                        mscmbHS1.EditText = containerDetails[0].CountryName;
                        mscmbHS2.EditText = containerDetails[1].CountryName;
                        mscmbHS3.EditText = containerDetails[2].CountryName;
                        mscmbHS4.EditText = containerDetails[3].CountryName;
                        mscmbHS5.EditText = containerDetails[4].CountryName;
                        mscmbHS6.EditText = containerDetails[5].CountryName;
                        mscmbHS7.EditText = containerDetails[6].CountryName;
                        mscmbHS8.EditText = containerDetails[7].CountryName;
                        mscmbHS9.EditText = containerDetails[8].CountryName;
                        mscmbHS10.EditText = containerDetails[9].CountryName;
                    }
                }
                #endregion
                bindingAMSACIISF.MoveFirst();
                //加载ams
                OceanHBL2AmsAciIsf ams = CurrentRowDataForAMS;
                ShipDescription.SetDataBinding(ams.Shipper);
                ConsigneeDescription.SetDataBinding(ams.Consignee);
                txtAMSNotifyPartyDescription.Text = ams.NotifyParty == null ? string.Empty : ams.NotifyParty.ToString(LocalData.IsEnglish);
                SellerDescription.SetDataBinding(ams.Seller);
                BuyerDescription.SetDataBinding(ams.Buyer);
                ShiptoDescription.SetDataBinding(ams.ShipTo);
                ManuDescription.SetDataBinding(ams.Manufacturer);
                StuffingDescription.SetDataBinding(ams.StuffingLocation);
                ConsolidatorDescription.SetDataBinding(ams.Consolidator);
                BookingDescription.SetDataBinding(ams.BookingPartyInfo);

                //加载Contains
                if (ams.Container != null)
                    DataSourceForContainerList = ams.Container;
                if (VesselVoyageIsChanged)
                    AmsIsChanged = true;
                else
                    AmsIsChanged = false;
            }
            else
            {
                string vv = _CurrentBLInfo.VesselVoyage;
                if (!string.IsNullOrEmpty(vv))
                {
                    txtVesselName.Text = vv.Substring(0, vv.IndexOf("/"));
                    string voyage = vv.Substring(vv.IndexOf("/") + 1).Replace("V.", "");
                    if (voyage.Length > 5)
                        if (voyage.IndexOf('-') > 0)
                            txtVoyageNumber.Text = voyage.Substring(0, 4) + voyage.Substring(voyage.Length - 1);
                        else
                            txtVoyageNumber.Text = voyage;
                    else
                        txtVoyageNumber.Text = voyage;
                }
                txtLastPortOfCall.Text = _CurrentBLInfo.POLCode;
                txtFirstPortOfCall.Text = _CurrentBLInfo.PODCode;
                txtPOL.Text = _CurrentBLInfo.POLCode;
                txtETD.Text = _CurrentBLInfo.ETD.ToString();
                txtFirstPortOfCallDate.Text = _CurrentBLInfo.ETA.ToString();
                AmsIsChanged = true;
                if (ContainerNum != null && ContainerNum.Count > 0)
                {
                    //是否导入联系人和箱信息
                    DialogResult dialogResult = MessageBoxService.ShowQuestion("是否导入上次AMS收发通信息？", (LocalData.IsEnglish ? "ToolTip" : "提示"), MessageBoxButtons.OKCancel);
                    if (dialogResult == DialogResult.OK)
                    {
                        try
                        {
                            List<OceanHBL2AmsAciIsf> lastAMS = OceanExportService.GetLastAmsAciIsfOjbectsList(_CurrentBLInfo.ID, LocalData.IsEnglish);
                            if (lastAMS.Count > 0)
                            {
                                //联系人
                                gvAMSACIISF.AddNewRow();
                                CurrentRowDataForAMS.OceanHBLID = _CurrentBLInfo.ID;
                                CurrentRowDataForAMS.CreateBy = LocalData.UserInfo.LoginID;

                                CurrentRowDataForAMS.Shipper = lastAMS[0].Shipper;
                                CurrentRowDataForAMS.Consignee = lastAMS[0].Consignee;

                                CurrentRowDataForAMS.Shipper.Name = lastAMS[0].Shipper.Name;
                                CurrentRowDataForAMS.Shipper.Country = lastAMS[0].Shipper.Country;
                                CurrentRowDataForAMS.Shipper.City = lastAMS[0].Shipper.City;
                                CurrentRowDataForAMS.Shipper.Address = lastAMS[0].Shipper.Address;
                                CurrentRowDataForAMS.ShipperDesc = lastAMS[0].ShipperDesc;
                                ShipDescription.SetDataBinding(CurrentRowDataForAMS.Shipper);

                                CurrentRowDataForAMS.Consignee.Name = lastAMS[0].Consignee.Name;
                                CurrentRowDataForAMS.Consignee.Country = lastAMS[0].Consignee.Country;
                                CurrentRowDataForAMS.Consignee.City = lastAMS[0].Consignee.City;
                                CurrentRowDataForAMS.Consignee.Address = lastAMS[0].Consignee.Address;
                                CurrentRowDataForAMS.ConsigneeDesc = lastAMS[0].ConsigneeDesc;
                                ConsigneeDescription.SetDataBinding(CurrentRowDataForAMS.Consignee);
                            }
                            else
                            {
                                gvAMSACIISF.AddNewRow();
                                CurrentRowDataForAMS.OceanHBLID = _CurrentBLInfo.ID;
                                CurrentRowDataForAMS.CreateBy = LocalData.UserInfo.LoginID;
                                CurrentRowDataForAMS.Shipper.Name = _CurrentBLInfo.ShipperDescription.Name;
                                CurrentRowDataForAMS.Shipper.Country = _CurrentBLInfo.ShipperDescription.Country;
                                CurrentRowDataForAMS.Shipper.City = _CurrentBLInfo.ShipperDescription.City;
                                CurrentRowDataForAMS.Shipper.Address = _CurrentBLInfo.ShipperDescription.Address + _CurrentBLInfo.ShipperDescription.Remark;
                                ShipDescription.SetDataBinding(CurrentRowDataForAMS.Shipper);
                                CurrentRowDataForAMS.ShipperDesc = CurrentRowDataForAMS.Shipper.ToString(true);

                                CurrentRowDataForAMS.Consignee.Name = _CurrentBLInfo.ConsigneeDescription.Name;
                                CurrentRowDataForAMS.Consignee.Country = _CurrentBLInfo.ConsigneeDescription.Country;
                                CurrentRowDataForAMS.Consignee.City = _CurrentBLInfo.ConsigneeDescription.City;
                                CurrentRowDataForAMS.Consignee.Address = _CurrentBLInfo.ConsigneeDescription.Address + _CurrentBLInfo.ConsigneeDescription.Remark;
                                ConsigneeDescription.SetDataBinding(CurrentRowDataForAMS.Consignee);
                                CurrentRowDataForAMS.ConsigneeDesc = CurrentRowDataForAMS.Consignee.ToString(true);

                                CurrentRowDataForAMS.BondActivityCode = BondActivityCode.Unknown;
                                CurrentRowDataForAMS.BondReferenceType = BondRef.Unknown;
                                CurrentRowDataForAMS.ImporterOfRecordNumberQualifier = ConsigneeAndBuyerType.Unknown;
                                CurrentRowDataForAMS.ISFImporterIDType = ImportRefType.Unknown;
                                CurrentRowDataForAMS.ConsigneeNumberQualifier = ConsigneeAndBuyerType.Unknown;
                                CurrentRowDataForAMS.CargoTypeForAMS = CargoTypeForAMS.Unknown;
                            }

                            if (DataSourceForContainerList == null)
                                DataSourceForContainerList = new List<ContainerForAMS>();
                            //箱
                            foreach (ContainerForAMS c in ContainerNum)
                            {
                                //gvAMSContainer.AddNewRow();
                                ContainerForAMS container = new ContainerForAMS();
                                container.ContainerNumber = c.ContainerNumber.Replace("-", "");
                                //container.FreeFormDescription = c.FreeFormDescription.Replace(Environment.NewLine, " ");
                                container.FreeFormDescription = c.FreeFormDescription;
                                container.Kilos = c.Kilos;
                                container.Quantity = c.Quantity;
                                container.Seal = c.Seal.Replace("-", "");
                                container.UnitOfMeasure = c.UnitOfMeasure;

                                DataSourceForContainerList.Add(container);
                            }
                            if (CurrentRowDataForAMS.Container == null)
                                CurrentRowDataForAMS.Container = new List<ContainerForAMS>();
                            CurrentRowDataForAMS.Container = DataSourceForContainerList;

                            SetStxtEnabled(true);
                            bindingAMSACIISF.ResetBindings(false);
                            bindingContainers.ResetBindings(false);
                        }
                        catch (Exception ex)
                        {
                            MessageBoxService.ShowInfo("Load container of failure information！");
                        }

                    }
                    else
                        SetStxtEnabled(false);
                }
                else
                {
                    MessageBoxService.ShowInfo("Load container of failure information！");
                    gcAMSACIISF.Enabled = false;
                    gcAMSContainer.Enabled = false;
                    barAddAMSACIISF.Enabled = false;
                    barDeleteAMSACIISF.Enabled = false;
                    barAddContainer.Enabled = false;
                    barDelContainer.Enabled = false;
                    SetStxtEnabled(false);
                    return;
                }
            }

        }

        /// <summary>
        /// 设置AMS StxtEnabled
        /// </summary>
        /// <param name="b">true or false</param>
        private void SetStxtEnabled(bool b)
        {
            stxtAMSShipper.Enabled = b;
            stxtAMSConsignee.Enabled = b;
            stxtAMSNotifyParty.Enabled = b;
            stxtSeller.Enabled = b;
            stxtBuyer.Enabled = b;
            stxtManufacturer.Enabled = b;
            stxtConsolidator.Enabled = b;
            stxtShipToPatry.Enabled = b;
            stxtStuffingLocation.Enabled = b;
            stxtBookingPartyInfo.Enabled = b;
        }
        /// <summary>
        /// 初始化联系人
        /// </summary>
        private void LoadPartyDesc()
        {
            if (_countryListForAMS == null)
            {
                _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
                _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            }
            #region AMS
            //shipper
            if (ShipDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    ShipDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            ShipDescription.geographyService = GeographyService;
            ShipDescription.OnOk += new EventHandler(stxtAMSShipper_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtAMSShipper, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }

            //    CustomerFinderBridgeFotAMS aMSShipperBridge = new CustomerFinderBridgeFotAMS(
            //       stxtAMSShipper,
            //       _countryListForAMS,
            //       DataFindClientService,
            //       CustomerService,
            //       CurrentRowDataForAMS.Shipper,
            //       null,
            //       ICPCommUIHelper,
            //       LocalData.IsEnglish,
            //       GeographyService);
            //    aMSShipperBridge.Init();
            //    stxtAMSShipper.OnOk += new EventHandler(stxtAMSShipper_OnOk);
            //});
            //Consignee
            if (ConsigneeDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    ConsigneeDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            ConsigneeDescription.geographyService = GeographyService;
            ConsigneeDescription.OnOk += new EventHandler(stxtAMSConsignee_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtAMSConsignee, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSConsigneeBridge = new CustomerFinderBridgeFotAMS(
            //    stxtAMSConsignee,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.Consignee,
            //    null,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSConsigneeBridge.Init();
            //    stxtAMSConsignee.OnOk += new EventHandler(stxtAMSConsignee_OnOk);
            //});
            ////NotifyParty
            //Utility.SetEnterToExecuteOnec(stxtAMSNotifyParty, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSNotifyPartyBridge = new CustomerFinderBridgeFotAMS(
            //    stxtAMSNotifyParty,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.NotifyParty,
            //    txtAMSNotifyPartyDescription,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSNotifyPartyBridge.Init();
            //    stxtAMSNotifyParty.OnOk += new EventHandler(stxtAMSNotifyParty_OnOk);
            //});
            if (SellerDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    SellerDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            SellerDescription.geographyService = GeographyService;
            SellerDescription.OnOk += new EventHandler(stxtSeller_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtSeller, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSNotifyPartyBridge = new CustomerFinderBridgeFotAMS(
            //    stxtSeller,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.Seller,
            //    null,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSNotifyPartyBridge.Init();
            //    stxtSeller.OnOk += new EventHandler(stxtSeller_OnOk);
            //});
            if (BuyerDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    BuyerDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            BuyerDescription.geographyService = GeographyService;
            BuyerDescription.OnOk += new EventHandler(stxtBuyer_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtBuyer, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSNotifyPartyBridge = new CustomerFinderBridgeFotAMS(
            //    stxtBuyer,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.Buyer,
            //    null,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSNotifyPartyBridge.Init();
            //    stxtBuyer.OnOk += new EventHandler(stxtBuyer_OnOk);
            //});
            if (ShiptoDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    ShiptoDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            ShiptoDescription.geographyService = GeographyService;
            ShiptoDescription.OnOk += new EventHandler(stxtShipToParty_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtShipToPatry, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSNotifyPartyBridge = new CustomerFinderBridgeFotAMS(
            //    stxtShipToPatry,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.ShipTo,
            //    null,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSNotifyPartyBridge.Init();
            //    stxtShipToPatry.OnOk += new EventHandler(stxtShipToParty_OnOk);
            //});
            if (ManuDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    ManuDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            ManuDescription.geographyService = GeographyService;
            ManuDescription.OnOk += new EventHandler(stxtManufacturer_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtManufacturer, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSNotifyPartyBridge = new CustomerFinderBridgeFotAMS(
            //    stxtManufacturer,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.Manufacturer,
            //    null,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSNotifyPartyBridge.Init();
            //    stxtManufacturer.OnOk += new EventHandler(stxtManufacturer_OnOk);
            //});
            if (StuffingDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    StuffingDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            StuffingDescription.geographyService = GeographyService;
            StuffingDescription.OnOk += new EventHandler(stxtStuffingLocation_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtStuffingLocation, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSNotifyPartyBridge = new CustomerFinderBridgeFotAMS(
            //    stxtStuffingLocation,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.StuffingLocation,
            //    null,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSNotifyPartyBridge.Init();
            //    stxtStuffingLocation.OnOk += new EventHandler(stxtStuffingLocation_OnOk);
            //});
            if (ConsolidatorDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    ConsolidatorDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            ConsolidatorDescription.geographyService = GeographyService;
            ConsolidatorDescription.OnOk += new EventHandler(stxtConsolidator_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtConsolidator, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSNotifyPartyBridge = new CustomerFinderBridgeFotAMS(
            //    stxtConsolidator,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.Consolidator,
            //    null,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSNotifyPartyBridge.Init();
            //    stxtConsolidator.OnOk += new EventHandler(stxtConsolidator_OnOk);
            //});
            if (BookingDescription.CountryItems.Count < 1)
            {
                foreach (CountryList c in _countryListForAMS)
                {
                    BookingDescription.CountryItems.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(c.EName));
                }
            }
            BookingDescription.geographyService = GeographyService;
            BookingDescription.OnOk += new EventHandler(stxtBookingPartyInfo_OnOk);
            //Utility.SetEnterToExecuteOnec(stxtBookingPartyInfo, delegate
            //{
            //    if (_countryListForAMS == null)
            //    {
            //        _countryListForAMS = GeographyService.GetCountryList(string.Empty, string.Empty, true, 0);
            //        _countryListForAMS = _countryListForAMS.FindAll(delegate(CountryList c) { return (c.EName == "China" || c.EName == "United States" || c.EName == "Canada" || c.EName == "Viet Nam" || c.EName == "Australia" || c.EName == "Malaysia" || c.EName == "Hong Kong" || c.EName == "Macau" || c.EName == "Taiwan, Province of China"); });
            //    }
            //    CustomerFinderBridgeFotAMS aMSNotifyPartyBridge = new CustomerFinderBridgeFotAMS(
            //    stxtBookingPartyInfo,
            //    _countryListForAMS,
            //    DataFindClientService,
            //    CustomerService,
            //    CurrentRowDataForAMS.BookingPartyInfo,
            //    null,
            //    ICPCommUIHelper,
            //    LocalData.IsEnglish,
            //       GeographyService);
            //    aMSNotifyPartyBridge.Init();
            //    stxtBookingPartyInfo.OnOk += new EventHandler(stxtBookingPartyInfo_OnOk);
            //});

            #endregion
        }

        /// <summary>
        /// 新增AMS ACI ISF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAddAMSACIISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            //判断是否有箱信息
            foreach (OceanHBL2AmsAciIsf oha in DataSourceForAMSList)
            {
                if (oha.Container == null)
                {
                    MessageBoxService.ShowInfo("必须添加箱信息！");
                    return;
                }
                if (oha.Container.Count < 1)
                {
                    MessageBoxService.ShowInfo("必须添加箱信息！");
                    return;
                }
                if (string.IsNullOrEmpty(txtIMO.Text.Trim()))
                {
                    MessageBoxService.ShowInfo("必须获取IMO！");
                    return;
                }
                if (string.IsNullOrEmpty(mscmbCountry.EditText.Trim()))
                {
                    MessageBoxService.ShowInfo("必须获取Flag！");
                    return;
                }
            }

            gvAMSACIISF.AddNewRow();
            CurrentRowDataForAMS.Shipper = new CustomerDescriptionForAMS();
            CurrentRowDataForAMS.Consignee = new CustomerDescriptionForAMS();
            CurrentRowDataForAMS.Seller = new CustomerDescriptionForAMS();
            CurrentRowDataForAMS.Manufacturer = new CustomerDescriptionForAMS();
            CurrentRowDataForAMS.StuffingLocation = new CustomerDescriptionForAMS();
            CurrentRowDataForAMS.Buyer = new CustomerDescriptionForAMS();
            CurrentRowDataForAMS.ShipTo = new CustomerDescriptionForAMS();
            CurrentRowDataForAMS.NotifyParty = new CustomerDescriptionForAMS();
            if (_CurrentBLInfo != null)
            {
                CurrentRowDataForAMS.CreateBy = LocalData.UserInfo.LoginID;
                CurrentRowDataForAMS.OceanHBLID = _CurrentBLInfo.ID;
            }

            ClearPartyDesc();
            SetStxtEnabled(true);

            cmbBondActivityCode.SelectedIndex = 0;
            cmbBondRef.SelectedIndex = 0;
            cmbISFImporterRef.SelectedIndex = 0;
            cmbBuyerImportNumber.SelectedIndex = 0;
            cmbConsigneeNumber.SelectedIndex = 0;
            cmbCargoType.SelectedIndex = 0;
        }
        /// <summary>
        /// 清空Party信息
        /// </summary>
        private void ClearPartyDesc()
        {
            stxtAMSShipper.Text = string.Empty;
            stxtAMSConsignee.Text = string.Empty;
            stxtAMSNotifyParty.Text = string.Empty;
            stxtSeller.Text = string.Empty;
            stxtBuyer.Text = string.Empty;
            stxtManufacturer.Text = string.Empty;
            stxtStuffingLocation.Text = string.Empty;
            stxtShipToPatry.Text = string.Empty;
            stxtConsolidator.Text = string.Empty;
            stxtBookingPartyInfo.Text = string.Empty;

            //txtAMSShipperDescription.Text = string.Empty;
            //txtAMSConsigneeDescription.Text = string.Empty;
            //txtAMSNotifyPartyDescription.Text = string.Empty;
            //txtSeller.Text = string.Empty;
            //txtBuyer.Text = string.Empty;
            //txtManufacturer.Text = string.Empty;
            //txtStuffingLocation.Text = string.Empty;
            //txtShipToPatry.Text = string.Empty;
            //txtConsolidator.Text = string.Empty;
            //txtBookingPartyInfo.Text = string.Empty;
        }
        /// <summary>
        /// 删除AmsAciIsf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DataSourceForAMSList == null)
            {
                cmbBondActivityCode.SelectedIndex = 0;
                cmbBondRef.SelectedIndex = 0;
                cmbISFImporterRef.SelectedIndex = 0;
                cmbBuyerImportNumber.SelectedIndex = 0;
                cmbConsigneeNumber.SelectedIndex = 0;
                cmbCargoType.SelectedIndex = 0;
                return;
            }
            if (DataSourceForAMSList.Count < 1)
            {
                cmbBondActivityCode.SelectedIndex = 0;
                cmbBondRef.SelectedIndex = 0;
                cmbISFImporterRef.SelectedIndex = 0;
                cmbBuyerImportNumber.SelectedIndex = 0;
                cmbConsigneeNumber.SelectedIndex = 0;
                cmbCargoType.SelectedIndex = 0;
                return;
            }
            //删除箱信息
            CurrentRowDataForAMS.Container = null;
            DataSourceForAMSList.Remove(CurrentRowDataForAMS);
            bindingAMSACIISF.ResetBindings(false);
            if (DataSourceForAMSList.Count == 0)
            {
                ClearPartyDesc();
                //加载箱信息
                DataSourceForContainerList = null;
                bindingContainers.ResetBindings(false);
                SetStxtEnabled(false);
                return;
            }
            //加载箱信息
            DataSourceForContainerList = CurrentRowDataForAMS.Container;
        }

        /// <summary>
        /// 添加箱信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barAddContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentRowDataForAMS == null)
            {
                MessageBoxService.ShowInfo("请选择要添加箱的AMS/ACI/ISF数据行！");
                return;
            }
            if (DataSourceForContainerList == null)
                DataSourceForContainerList = new List<ContainerForAMS>();
            gvAMSContainer.AddNewRow();
            //将箱添加到当前ams
            CurrentRowDataForAMS.Container = DataSourceForContainerList;
        }
        /// <summary>
        /// 删除箱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barDelContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            DataSourceForContainerList.Remove(CurrentRowDataForContainer);
            CurrentRowDataForAMS.Container = DataSourceForContainerList;
            bindingAMSACIISF.ResetBindings(false);
            bindingContainers.ResetBindings(false);
            AmsIsChanged = true;
        }
        /// <summary>
        /// 改变选择的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAMSACIISF_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (DataSourceForAMSList != null)
                if (DataSourceForAMSList.Count > 0)
                {
                    ClearPartyDesc();
                    if (CurrentRowDataForAMS.ID == Guid.Empty)
                    {
                        return;
                    }
                    //加载ams
                    OceanHBL2AmsAciIsf ams = CurrentRowDataForAMS;

                    ConsigneeDescription.SetDataBinding(ams.Consignee);
                    txtAMSNotifyPartyDescription.Text = ams.NotifyParty == null ? string.Empty : ams.NotifyParty.ToString(LocalData.IsEnglish);


                    SellerDescription.SetDataBinding(ams.Seller);
                    BuyerDescription.SetDataBinding(ams.Buyer);
                    ShiptoDescription.SetDataBinding(ams.ShipTo);
                    ManuDescription.SetDataBinding(ams.Manufacturer);
                    StuffingDescription.SetDataBinding(ams.StuffingLocation);
                    ConsolidatorDescription.SetDataBinding(ams.Consolidator);
                    BookingDescription.SetDataBinding(ams.BookingPartyInfo);

                    //加载Contains
                    DataSourceForContainerList = ams.Container;
                    bindingAMSACIISF.ResetBindings(false);
                    bindingContainers.ResetBindings(false);
                    LoadPartyDesc();
                }
        }

        private void gvAMSContainer_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            //选择箱号加载封条号
            if (e.Column.FieldName == "ContainerNumber")
            {
                ContainerForAMS container = ContainerNum.Find(delegate(ContainerForAMS c) { return c.ContainerNumber == e.Value.ToString(); });
                CurrentRowDataForContainer.Seal = container.Seal;
                CurrentRowDataForContainer.UnitOfMeasure = container.UnitOfMeasure;
                CurrentRowDataForContainer.Quantity = container.Quantity;
                CurrentRowDataForContainer.Kilos = container.Kilos;
                CurrentRowDataForContainer.FreeFormDescription = container.FreeFormDescription.Replace(Environment.NewLine, " ");
                gvAMSContainer.Focus();
            }
            AmsIsChanged = true;
        }

        private void gvAMSACIISF_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            AmsIsChanged = true;
        }

        #region OK PartyDesc

        void stxtAMSShipper_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = ShipDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.Shipper = des;
                //CurrentRowDataForAMS.ShipperDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtAMSConsignee_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = ConsigneeDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.Consignee = des;
                //CurrentRowDataForAMS.ConsigneeDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtAMSNotifyParty_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = stxtAMSNotifyParty.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.NotifyParty = des;
                CurrentRowDataForAMS.NotifyPartyDesc = des.ToString(true);
                bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtSeller_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = SellerDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.Seller = des;
                //CurrentRowDataForAMS.SellerDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtBuyer_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = BuyerDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.Buyer = des;
                //CurrentRowDataForAMS.BuyerDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtShipToParty_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = ShiptoDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.ShipTo = des;
                //CurrentRowDataForAMS.ShipToDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtManufacturer_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = ManuDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.Manufacturer = des;
                //CurrentRowDataForAMS.ManufacturerDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtStuffingLocation_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = StuffingDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.StuffingLocation = des;
                //CurrentRowDataForAMS.StuffingLocationDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtConsolidator_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = ConsolidatorDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.Consolidator = des;
                //CurrentRowDataForAMS.ConsolidatorDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }
        void stxtBookingPartyInfo_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForAMS des = BookingDescription.CustomerDescriptionForAMS;
            if (des == null)
            {
                des = new CustomerDescriptionForAMS();
            }
            if (CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.BookingPartyInfo = des;
                //CurrentRowDataForAMS.BookingPartyInfoDesc = des.ToString(true);
                //bindingAMSACIISF.ResetCurrentItem();
            }
        }

        #endregion

        private void cmbAMSEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AMSEntryType type = (AMSEntryType)Enum.Parse(typeof(AMSEntryType), cmbAMSEntryType.SelectedItem.ToString());
            if (type == AMSEntryType.PorttoPort || type == AMSEntryType.InlandTransit)
            {
                if (cmbISFImporterRef.SelectedItem != null)
                    if ((ImportRefType)Enum.Parse(typeof(ImportRefType), cmbISFImporterRef.SelectedItem.ToString()) == ImportRefType.SCAC)
                    {
                        MessageBoxService.ShowInfo("ISF Entry Types,Port toPort and Inland USA,cannot use the following as SCAC:ISF Importer Ref# Type.");
                        cmbISFImporterRef.SelectedIndex = 0;
                        return;
                    }
            }
            if (type == AMSEntryType.StayonBoard)
            {
                StayOnBoard(false);
            }
            else
            {
                StayOnBoard(true);
            }
        }

        private void StayOnBoard(bool b)
        {
            ckSeller.Enabled = b;
            ckManufacturer.Enabled = b;
            ckStuffingLocation.Enabled = b;
            ckConsolidator.Enabled = b;
            ckBuyer.Enabled = b;
            stxtSeller.Enabled = b;
            stxtManufacturer.Enabled = b;
            stxtStuffingLocation.Enabled = b;
            stxtConsolidator.Enabled = b;
            stxtBuyer.Enabled = b;
            stxtBookingPartyInfo.Enabled = !b;
            ckConsigneeRef.Enabled = b;
            cmbConsigneeNumber.Enabled = b;
            txtConsigneeNumber.Enabled = b;
            ckBuyerRef.Enabled = b;
            cmbBuyerImportNumber.Enabled = b;
            txtBuyerImportNumber.Enabled = b;
            //cmbConsigneeNumber.SelectedIndex = 0;
            //cmbBuyerImportNumber.SelectedIndex = 0;
        }

        /// <summary>
        /// ISF Importer Ref#
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbImporterRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtISFImporterRef.Enabled = true;
            ImportRefType itype = (ImportRefType)Enum.Parse(typeof(ImportRefType), cmbISFImporterRef.SelectedItem.ToString());
            if (itype == ImportRefType.Unknown)
            {
                CurrentRowDataForAMS.ISFImporterID = string.Empty;
                CurrentRowDataForAMS.ISFImporterIDType = ImportRefType.Unknown;
                txtISFImporterRef.Enabled = false;
            }
            if (itype == ImportRefType.SCAC)
            {
                AMSEntryType type = (AMSEntryType)Enum.Parse(typeof(AMSEntryType), cmbAMSEntryType.SelectedItem.ToString());
                if (type == AMSEntryType.PorttoPort || type == AMSEntryType.InlandTransit)
                {
                    MessageBoxService.ShowInfo("ISF Entry Types,Port toPort and Inland USA,cannot use the following as SCAC:ISF Importer Ref# Type.");
                    cmbISFImporterRef.SelectedIndex = 0;
                    return;
                }
            }
            SetRef(cmbISFImporterRef, panelISFImporterRef, cmbISFImporterRefCountry);
        }
        /// <summary>
        /// Consignee Ref#
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbConsigneeNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtConsigneeNumber.Enabled = true;
            ConsigneeAndBuyerType itype = (ConsigneeAndBuyerType)Enum.Parse(typeof(ConsigneeAndBuyerType), cmbConsigneeNumber.SelectedItem.ToString());
            if (itype == ConsigneeAndBuyerType.Unknown && CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.ConsigneeNumber = string.Empty;
                CurrentRowDataForAMS.ConsigneeNumberQualifier = ConsigneeAndBuyerType.Unknown;
                txtConsigneeNumber.Enabled = false;
            }
            SetRef(cmbConsigneeNumber, panelConsignee, cmbConsigneeCountry);
        }

        private void cmbImportNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuyerImportNumber.Enabled = true;
            ConsigneeAndBuyerType itype = (ConsigneeAndBuyerType)Enum.Parse(typeof(ConsigneeAndBuyerType), cmbBuyerImportNumber.SelectedItem.ToString());
            if (itype == ConsigneeAndBuyerType.Unknown && CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.ImporterOfRecordNumber = string.Empty;
                CurrentRowDataForAMS.ImporterOfRecordNumberQualifier = ConsigneeAndBuyerType.Unknown;
                txtBuyerImportNumber.Enabled = false;
            }
            SetRef(cmbBuyerImportNumber, panelBuyer, cmbBuyerCountry);
        }

        private void cmbBondRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBondRefNumber.Enabled = true;
            BondRef itype = (BondRef)Enum.Parse(typeof(BondRef), cmbBondRef.SelectedItem.ToString());
            if (cmbBondRef.SelectedIndex == 0 && CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.BondReferenceNumber = string.Empty;
                CurrentRowDataForAMS.BondReferenceType = BondRef.Unknown;
                txtBondRefNumber.Enabled = false;
            }
        }

        private void cmbCargoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBondActivityCode.Enabled = true;
            CargoTypeForAMS itype = (CargoTypeForAMS)Enum.Parse(typeof(CargoTypeForAMS), cmbCargoType.SelectedItem.ToString());
            if (itype == CargoTypeForAMS.HouseholdGoodsOrPersonalEffects && CurrentRowDataForAMS != null)
            {
                CurrentRowDataForAMS.BondActivityCode = BondActivityCode.Unknown;// cmbBondActivityCode.SelectedIndex = 0;
                cmbBondActivityCode.Enabled = false;
            }
        }

        /// <summary>
        /// 设置税号的显示与隐藏
        /// </summary>
        /// <param name="cmbImporterRef"></param>
        /// <param name="panelISFImporterRef"></param>
        /// <param name="cmbISFImporterRefCountry"></param>
        private void SetRef(LWImageComboBoxEdit cmbImporterRef, PanelControl panelISFImporterRef, MultiSearchCommonBox cmbISFImporterRefCountry)
        {
            if (cmbImporterRef.EditValue.ToString() == "Passport" || cmbImporterRef.EditValue.ToString() == "SSN")
            {
                panelISFImporterRef.Visible = true;
                if (cmbImporterRef.EditValue.ToString() == "SSN")
                    cmbISFImporterRefCountry.Enabled = false;
                else
                    cmbISFImporterRefCountry.Enabled = true;
            }
            else
                panelISFImporterRef.Visible = false;
        }


        #region 滚动条事件

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            panelRight.Top = -vScrollBar2.Value;
            panelLeft.Top = -vScrollBar2.Value;
        }
        #endregion

        private void txtSH1_EditValueChanged(object sender, EventArgs e)
        {
            AmsIsChanged = true;
        }

        private void mscmbHS1_EditValueChanged(object sender, EventArgs e)
        {
            AmsIsChanged = true;
        }

        private void btnCopyCustomerFrom_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barblCHS_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.MailCustomerAskForConfirmSI(false, _bookingInfo.ID, _CurrentBLInfo, null);
        }

        private void barBlENG_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.MailCustomerAskForConfirmSI(true, _bookingInfo.ID, _CurrentBLInfo, null);
        }

        private void chkThirdPlacePay_CheckedChanged(object sender, EventArgs e)
        {
            stxtPlacePay.Enabled = chkThirdPlacePay.Checked;

            if (chkThirdPlacePay.Checked == false)
            {
                _CurrentBLInfo.CollectbyAgentOrderID = Guid.Empty;
                _CurrentBLInfo.CollectbyAgentNameOrder = string.Empty;
            }
        }

        /// <summary>
        /// 寄给代理状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkToAgent_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkToAgent.Checked == false)
            {
                if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "取消寄给代理后代理将无法下载这票业务，是否继续?",
                                   LocalData.IsEnglish ? "Tip" : "提示",
                                   MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    chkToAgent.Checked = true;
                }
            }

        }

        private void chkIsBook_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsBook.Checked)
            {
                txtBookNo.Enabled = true;
                if (string.IsNullOrEmpty(txtBookNo.Text))
                {
                    txtBookNo.Text = _CurrentBLInfo.No;
                }
            }
            else
            {
                txtBookNo.Enabled = false;
                txtBookNo.Text = string.Empty;
            }
        }

        private void txtAMSNO_EditValueChanged(object sender, EventArgs e)
        {
            AmsIsChanged = true;
        }

        private void barWebEdi_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DialogResult.Yes == ShowQuestion((LocalData.IsEnglish ? "Whether confirmed online directly AMS EDI?" : "是否确认已在网上直接 AMS EDI？"), (LocalData.IsEnglish ? "ToolTip" : "提示"), MessageBoxButtons.YesNo))
            {
                OceanExportService.ChangeAmsState(_CurrentBLInfo.OceanBookingID, true, LocalData.IsEnglish);
            }

        }

        #region AMS客户信息
        private void stxtAMSShipper_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtAMSShipper.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames(stxtAMSShipper.Text, "", "", "", "", "", "", "", "");

                if (list != null && list.Count > 0)
                {
                    amsShippers.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsShippers.Count(r => r.Name.ToUpper() == obj.Shipper.Name.ToUpper()) == 0)
                        {
                            amsShippers.Add(obj.Shipper);
                        }
                    }

                    stxtAMSShipper.Properties.Items.Clear();
                    stxtAMSShipper.Properties.BeginUpdate();
                    amsShippers.ForEach(r =>
                    {
                        stxtAMSShipper.Properties.Items.Add(r.Name);
                    });
                    stxtAMSShipper.Properties.EndUpdate();

                    stxtAMSShipper_Enter(sender, null);
                }
            }
        }

        private void stxtAMSShipper_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtAMSShipper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtAMSShipper.SelectedIndex < 0) return;

            CustomerDescriptionForAMS shipper = amsShippers[stxtAMSShipper.SelectedIndex];
            if (shipper != null)
            {
                ShipDescription.SetDataBinding(shipper);
            }
        }

        private void stxtSeller_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtSeller.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames("", "", stxtSeller.Text, "", "", "", "", "", "");

                if (list != null && list.Count > 0)
                {
                    amsSellers.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsSellers.Count(r => r.Name.ToUpper() == obj.Seller.Name.ToUpper()) == 0)
                        {
                            amsSellers.Add(obj.Shipper);
                        }
                    }

                    stxtSeller.Properties.Items.Clear();
                    stxtSeller.Properties.BeginUpdate();
                    amsSellers.ForEach(r =>
                    {
                        stxtSeller.Properties.Items.Add(r.Name);
                    });
                    stxtSeller.Properties.EndUpdate();

                    stxtSeller_Enter(sender, null);
                }
            }
        }

        private void stxtSeller_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtSeller.SelectedIndex < 0) return;

            CustomerDescriptionForAMS seller = amsSellers[stxtSeller.SelectedIndex];
            if (seller != null)
            {
                SellerDescription.SetDataBinding(seller);
            }
        }

        private void stxtAMSConsignee_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtAMSConsignee.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames("", stxtAMSConsignee.Text, "", "", "", "", "", "", "");

                if (list != null && list.Count > 0)
                {
                    amsConsignees.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsConsignees.Count(r => r.Name.ToUpper() == obj.Consignee.Name.ToUpper()) == 0)
                        {
                            amsConsignees.Add(obj.Consignee);
                        }
                    }

                    stxtAMSConsignee.Properties.Items.Clear();
                    stxtAMSConsignee.Properties.BeginUpdate();
                    amsConsignees.ForEach(r =>
                    {
                        stxtAMSConsignee.Properties.Items.Add(r.Name);
                    });
                    stxtAMSConsignee.Properties.EndUpdate();

                    stxtAMSConsignee_Enter(sender, null);
                }
            }
        }

        private void stxtAMSConsignee_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtAMSConsignee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtAMSConsignee.SelectedIndex < 0) return;

            CustomerDescriptionForAMS consignee = amsConsignees[stxtAMSConsignee.SelectedIndex];
            if (consignee != null)
            {
                ConsigneeDescription.SetDataBinding(consignee);
            }
        }

        private void stxtBuyer_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtBuyer.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames("", "", "", stxtBuyer.Text, "", "", "", "", "");

                if (list != null && list.Count > 0)
                {
                    amsBuyers.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsBuyers.Count(r => r.Name.ToUpper() == obj.Buyer.Name.ToUpper()) == 0)
                        {
                            amsBuyers.Add(obj.Buyer);
                        }
                    }

                    stxtBuyer.Properties.Items.Clear();
                    stxtBuyer.Properties.BeginUpdate();
                    amsBuyers.ForEach(r =>
                    {
                        stxtBuyer.Properties.Items.Add(r.Name);
                    });
                    stxtBuyer.Properties.EndUpdate();

                    stxtBuyer_Enter(sender, null);
                }
            }
        }

        private void stxtBuyer_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtBuyer.SelectedIndex < 0) return;

            CustomerDescriptionForAMS buyer = amsBuyers[stxtBuyer.SelectedIndex];
            if (buyer != null)
            {
                BuyerDescription.SetDataBinding(buyer);
            }
        }

        private void stxtManufacturer_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtManufacturer.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames("", "", "", "", stxtManufacturer.Text, "", "", "", "");

                if (list != null && list.Count > 0)
                {
                    amsManus.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsManus.Count(r => r.Name.ToUpper() == obj.Manufacturer.Name.ToUpper()) == 0)
                        {
                            amsManus.Add(obj.Manufacturer);
                        }
                    }

                    stxtManufacturer.Properties.Items.Clear();
                    stxtManufacturer.Properties.BeginUpdate();
                    amsManus.ForEach(r =>
                    {
                        stxtManufacturer.Properties.Items.Add(r.Name);
                    });
                    stxtManufacturer.Properties.EndUpdate();

                    stxtManufacturer_Enter(sender, null);
                }
            }
        }

        private void stxtManufacturer_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtManufacturer.SelectedIndex < 0) return;

            CustomerDescriptionForAMS manu = amsManus[stxtManufacturer.SelectedIndex];
            if (manu != null)
            {
                ManuDescription.SetDataBinding(manu);
            }
        }

        private void stxtStuffingLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtStuffingLocation.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames("", "", "", "", "", stxtStuffingLocation.Text, "", "", "");

                if (list != null && list.Count > 0)
                {
                    amsStuffings.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsStuffings.Count(r => r.Name.ToUpper() == obj.StuffingLocation.Name.ToUpper()) == 0)
                        {
                            amsStuffings.Add(obj.StuffingLocation);
                        }
                    }

                    stxtStuffingLocation.Properties.Items.Clear();
                    stxtStuffingLocation.Properties.BeginUpdate();
                    amsStuffings.ForEach(r =>
                    {
                        stxtStuffingLocation.Properties.Items.Add(r.Name);
                    });
                    stxtStuffingLocation.Properties.EndUpdate();

                    stxtStuffingLocation_Enter(sender, null);
                }
            }
        }

        private void stxtStuffingLocation_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtStuffingLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtStuffingLocation.SelectedIndex < 0) return;

            CustomerDescriptionForAMS stuffing = amsStuffings[stxtStuffingLocation.SelectedIndex];
            if (stuffing != null)
            {
                StuffingDescription.SetDataBinding(stuffing);
            }
        }

        private void stxtShipToPatry_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtShipToPatry.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames("", "", "", "", "", "", "", stxtShipToPatry.Text, "");

                if (list != null && list.Count > 0)
                {
                    amsShiptos.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsShiptos.Count(r => r.Name.ToUpper() == obj.ShipTo.Name.ToUpper()) == 0)
                        {
                            amsShiptos.Add(obj.ShipTo);
                        }
                    }

                    stxtShipToPatry.Properties.Items.Clear();
                    stxtShipToPatry.Properties.BeginUpdate();
                    amsShiptos.ForEach(r =>
                    {
                        stxtShipToPatry.Properties.Items.Add(r.Name);
                    });
                    stxtShipToPatry.Properties.EndUpdate();

                    stxtShipToPatry_Enter(sender, null);
                }
            }
        }

        private void stxtShipToPatry_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtShipToPatry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtShipToPatry.SelectedIndex < 0) return;

            CustomerDescriptionForAMS shipto = amsShiptos[stxtShipToPatry.SelectedIndex];
            if (shipto != null)
            {
                ShiptoDescription.SetDataBinding(shipto);
            }
        }

        private void stxtConsolidator_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtConsolidator.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames("", "", "", "", "", "", stxtConsolidator.Text, "", "");

                if (list != null && list.Count > 0)
                {
                    amsConsolidators.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsConsolidators.Count(r => r.Name.ToUpper() == obj.Consolidator.Name.ToUpper()) == 0)
                        {
                            amsConsolidators.Add(obj.Consolidator);
                        }
                    }

                    stxtConsolidator.Properties.Items.Clear();
                    stxtConsolidator.Properties.BeginUpdate();
                    amsConsolidators.ForEach(r =>
                    {
                        stxtConsolidator.Properties.Items.Add(r.Name);
                    });
                    stxtConsolidator.Properties.EndUpdate();

                    stxtConsolidator_Enter(sender, null);
                }
            }
        }

        private void stxtConsolidator_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtConsolidator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtConsolidator.SelectedIndex < 0) return;

            CustomerDescriptionForAMS consolidator = amsConsolidators[stxtConsolidator.SelectedIndex];
            if (consolidator != null)
            {
                ConsolidatorDescription.SetDataBinding(consolidator);
            }
        }

        private void stxtBookingPartyInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(stxtBookingPartyInfo.Text))
                return;

            if (e.KeyCode == Keys.Enter)
            {
                List<OceanHBL2AmsAciIsf> list = OceanExportService.GetAmsListByCustomerNames("", "", "", "", "", "", "", "", stxtBookingPartyInfo.Text);

                if (list != null && list.Count > 0)
                {
                    amsBookingPartys.Clear();
                    foreach (OceanHBL2AmsAciIsf obj in list)
                    {
                        if (amsBookingPartys.Count(r => r.Name.ToUpper() == obj.BookingPartyInfo.Name.ToUpper()) == 0)
                        {
                            amsBookingPartys.Add(obj.BookingPartyInfo);
                        }
                    }

                    stxtBookingPartyInfo.Properties.Items.Clear();
                    stxtBookingPartyInfo.Properties.BeginUpdate();
                    amsBookingPartys.ForEach(r =>
                    {
                        stxtBookingPartyInfo.Properties.Items.Add(r.Name);
                    });
                    stxtBookingPartyInfo.Properties.EndUpdate();

                    stxtBookingPartyInfo_Enter(sender, null);
                }
            }
        }

        private void stxtBookingPartyInfo_Enter(object sender, EventArgs e)
        {
            BeginInvoke(new System.Windows.Forms.MethodInvoker(delegate { ((ComboBoxEdit)sender).ShowPopup(); }));
        }

        private void stxtBookingPartyInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stxtBookingPartyInfo.SelectedIndex < 0) return;

            CustomerDescriptionForAMS bookingparty = amsBookingPartys[stxtBookingPartyInfo.SelectedIndex];
            if (bookingparty != null)
            {
                BookingDescription.SetDataBinding(bookingparty);
            }
        }
        #endregion

        
    }
}
