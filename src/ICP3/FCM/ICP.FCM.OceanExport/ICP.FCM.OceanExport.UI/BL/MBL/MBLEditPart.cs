using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ICP.FCM.OceanExport.ServiceInterface.Comm;
using ICP.FCM.OceanExport.UI.Container;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.ClientComponents.Controls;
using ICP.FCM.OceanExport.UI.Common;
using ICP.Common.UI;
using ICP.Framework.ClientComponents.Service;
using ICP.FAM.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.EDI.ServiceInterface;
using System.Linq;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Business.Common.UI;
using ICP.Business.Common.UI.Common;
using Microsoft.Practices.CompositeUI.SmartParts;
using ActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;
using System.Threading;
using System.Text.RegularExpressions;
using ICP.FCM.OceanExport.UI.BL.MBL;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FCM.OceanExport.UI.MBL
{
    /// <summary>
    /// 主提单编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class MBLEditPart : BaseEditPart
    {
        #region Service
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem
        {
            get;
            set;
        }

        private IGeographyService GeographyService
        {
            get
            {
                return ServiceClient.GetService<IGeographyService>();
            }
        }

        private IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }

        private IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        private ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }

        public IFCMCommonService fcmComonService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonService>();
            }

        }

        private IFCMCommonService FCMCommonService
        {
            get
            {

                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        private IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }

        private ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        private IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }

        private ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }

        private IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// EDI客户端服务
        /// </summary>
        public IEDIClientService ediClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();
            }
        }

        private OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }

        UCContactAndDocumentPart ucMblOtherPart;

        private UCContactAndDocumentPart UCMblOtherPart
        {
            get
            {
                if (ucMblOtherPart != null)
                {
                    return ucMblOtherPart;
                }
                else
                {
                    ucMblOtherPart = Workitem.SmartParts.AddNew<UCContactAndDocumentPart>();
                    return ucMblOtherPart;
                }
            }
        }


        private IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }

        private IEDIClientService EDIClientService
        {
            get
            {
                return ServiceClient.GetClientService<IEDIClientService>();
            }
        }
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }
        }

        #endregion

        #region Init
        /// <summary>
        /// 构造函数
        /// </summary>
        public MBLEditPart()
        {
            InitializeComponent();
            SyncLocalData = true;
            if (!LocalData.IsDesignMode)
            {
                Load += (sender, e) =>
                {

                    SetNullValuePrompt();
                    if (LocalData.IsEnglish == false) SetCnText();
                    UCMblOtherPart.BindData(OperationContext);
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
                                UCMblOtherPart.AddDocuments(strFiles, DocumentType.MBL);
                            }
                            if (_businessOperationParameter.ActionType ==
                                ActionType.Create)
                            {
                                CustomerCarrierObjects customerCarrier = FCM.Common.ServiceInterface.FCMInterfaceUtility.CreateCustomerCarrierInfo(false, false, true, _businessOperationParameter.Message.SendFrom,
                                                              OEUtility.GetSenderName(_businessOperationParameter.Message.CreatorName));
                                UCMblOtherPart.InsertCustomerInfo(customerCarrier);
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
                                    UCMblOtherPart.InnerBindData(contactInfo.CustomerCarrier);
                                    isSaveOperationContact = true;
                                }
                            }
                        }
                        
                    }
                    #endregion
                    if (EditMode == EditMode.New || EditMode == EditMode.Copy)
                    {
                        txtResponsibleParty.Text = "CITY OCEAN LOGISTICS CO.,LTD";
                        txtResponsiblePerson.Text = LocalData.UserInfo.UserEname;
                    }
                    barSavingTools.Visible = false;
                    barSavingClose.ItemClick += barSavingClose_ItemClick;
                    barCancel.ItemClick += barCancel_ItemClick;
                    barlabMessage.ItemClick += barlabMessage_ItemClick;
                    TimerSaveData = new System.Windows.Forms.Timer();
                    TimerSaveData.Interval = 1000;
                    TimerSaveData.Tick += RefreshTime_Tick;
                    txtFreightDescription.Leave += txtFreightDescription_Leave;
                    this.chkSpecial.CheckedChanged += this.chkSpecial_CheckedChanged;
                };

                Disposed += delegate
                {

                    _CurrentBLInfo = null;
                    dxErrorProvider1.DataSource = null;

                    barSavingClose.ItemClick -= barSavingClose_ItemClick;
                    barCancel.ItemClick -= barCancel_ItemClick;
                    barlabMessage.ItemClick -= barlabMessage_ItemClick;
                    TimerSaveData.Tick -= RefreshTime_Tick;

                    SmartPartClosing -= MBLEditPart_SmartPartClosing;
                    stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
                    stxtAgent.OnOk -= stxtAgent_OnOk;
                    stxtConsignee.OnOk -= stxtConsignee_OnOk;
                    stxtNotifyParty.OnOk -= stxtNotifyParty_OnOk;
                    stxtPreVoyage.EditValueChanged -= stxtPreVoyage_EditValueChanged;
                    stxtShipper.OnOk -= stxtShipper_OnOk;
                    stxtVoyage.EditValueChanged -= stxtVoyage_EditValueChanged;
                    chkHasContract.CheckedChanged -= chkHasContract_CheckedChanged;
                    txtFreightDescription.EditValueChanged -= txtFreightDescription_Leave;
                    this.chkSpecial.CheckedChanged -= this.chkSpecial_CheckedChanged;

                    stateValues = null;
                    bindingSource1.DataSource = null;
                    bindingSource1.Dispose();
                    mcmbIssueBy.Enter -= OnIssueByEnter;

                    Saved = null;
                    ucMblOtherPart.Dispose();

                    //this.cmbBLTitle.OnFirstEnter -= this.OncmbBLTitleFirstEnter;
                    cmbIssueType.OnFirstEnter -= OncmbIssueTypeFirstEnter;
                    cmbReleaseType.OnFirstEnter -= OncmbReleaseTypeFirstEnter;

                    cmbPaymentTerm.OnFirstEnter -= OncmbPaymentTermFirstEnter;
                    cmbPaymentTerm.SelectedIndexChanged -= cmbPaymentTerm_SelectedIndexChanged;

                    cmbQuantityUnit.OnFirstEnter -= OncmbQuantityUnitFirstEnter;

                    cmbWeightUnit.OnFirstEnter -= OncmbWeightUnitFirstEnter;
                    cmbMeasurementUnit.OnFirstEnter -= OncmbMeasurementUnitFirstEnter;

                    cmbTransportClause.OnFirstEnter -= OncmbTransportClauseFirstEnter;

                    stxtAgent.FirstTimeEnter -= OnstxtAgentFirstEnter;

                    //shipper
                    stxtShipper.OnFirstEnter -= OnstxtShipperFirstTimeEnter;

                    //Consignee
                    stxtConsignee.OnFirstEnter -= OnstxtConsigneeFirstTimeEnter;


                    //NotifyParty
                    stxtNotifyParty.OnFirstEnter -= OnstxtNotifyPartyFirstTimeEnter;
                    _bookingInfo = null;
                    ReleaseDataFinder();
                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(ucMblOtherPart);
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
                    ucMblOtherPart = null;
                    PerformLayout();
                };

            }

        }
        /// <summary>
        /// 释放注册的搜索器
        /// </summary>
        private void ReleaseDataFinder()
        {
            if (placeOfReceiptFinder != null)
            {
                placeOfReceiptFinder.Dispose();
            }
            if (polCodeFinder != null)
            {
                polCodeFinder.Dispose();
            }
            if (podCodeFinder != null)
            {
                podCodeFinder.Dispose();
            }
            if (placeOfDeliveryFinder != null)
            {
                placeOfDeliveryFinder.Dispose();
            }
            if (finalDestinationFinder != null)
            {
                finalDestinationFinder.Dispose();
            }
            if (issuePlaceFinder != null)
            {
                issuePlaceFinder.Dispose();
            }
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
            //lblGateIn.Text = "进箱日期";
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
            labResponsibleParty.Text = "责    任     方";
            labResponsiblePerson.Text = "责     任     人";
            labWeightSite.Text = "称  重  地  点";
            labVerifiedPerson.Text = "重 量 核 实 人";
            labVerifiedDate.Text = "核  实  日  期";
            labWeightingDate.Text = "称  重  日  期";
            chkSpecial.Text = "特殊货物";

            labTransportClause.Text = "运输条款";
            labVoyage.Text = "大船";
            labWeight.Text = "重量";
            labType.Text = "类型";
            labCtnQtyInfo.Text = "集装箱或件数合计";
            labDescriptionOfGoods.Text = "包装种类/货名";
            //labContractNo.Text = "合约号";
            chkIsWoodPacking.Text = "木质包装";

            barSubCheck.Caption = "对单";
            barCheck.Caption = "申请(&K)";
            barCheckDone.Caption = "完成(&D)";
            barRefresh.Caption = "刷新(&R)";

            barSave.Caption = "保存(&S)";
            barSavingClose.Caption = "保存并关闭";
            barCancel.Caption = "取消";
            barSaveAs.Caption = "另存为(&A)";

            barClose.Caption = "关闭(&C)";


            barSubPrint.Caption = "打印";
            barPrintBL.Caption = "打印提单";
            barPrintLoadGoods.Caption = "打印装货单";
            barPrintLoadContainer.Caption = "打印装箱单";
            barPrintLoadContainerCopy.Caption = "打印装箱单(副本)";

            barReplyAgent.Caption = "申请代理(&R)";

            btnContainer.Text = "箱信息";

            navBarBaseInfo.Caption = "基本信息";
            navBarBLInfo.Caption = "提单信息";
            navBarCargo.Caption = "货物信息";
            navBarIssueInfo.Caption = "签发信息";

            labMBLNo.Text = "主提单号";
            labHBLNO.Text = "分提单号";

            chkShowVoyage.Text = chkShowPreVoyage.Text = "显示";

            barbl.Caption = "客户确认补料";
            barchs.Caption = "客户确认补料(中文版)";
            bareng.Caption = "客户确认补料(英文版)";

        }
        private void SetContractBoxByHasContract(bool hasContract)
        {
            txtContractNo.Enabled = hasContract;
            txtContractNo.BackColor = hasContract ? SystemColors.Info : txtContractNo.BackColor;
        }
        /// <summary>
        /// 设置一些控件的提示信息
        /// </summary>
        void SetNullValuePrompt()
        {
            OEUtility.SetCustomerTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtShipper,
                stxtConsignee,
                stxtChecker,
                stxtIssuePlace,
                stxtNotifyParty ,
            });
            OEUtility.SetPortTextEditNullValuePrompt(new List<TextEdit>
            {
                stxtFinalDestination,
                stxtPlaceOfDelivery ,
                stxtPlaceOfReceipt ,
                txtPOLCode,
                txtPODCode,
                cmbWeightSite,
            });

            OEUtility.SetTextEditNullValuePrompt(new List<TextEdit> { stxtRefNo }
                , LocalData.IsEnglish ? "Please Input Operation NO." : "请输入业务号.");


            string tip = LocalData.IsEnglish ? "Un Done" :
                        "此栏目链接于订舱单,保存时修改的内容将会更新到订舱单中.";

            stxtPlaceOfReceipt.ToolTip = txtPOLCode.ToolTip = txtPODCode.ToolTip = txtPlaceOfDeliveryName.ToolTip =
            stxtPreVoyage.ToolTip = stxtVoyage.ToolTip = tip;


        }

        IDictionary<string, object> stateValues;
        public override void Init(IDictionary<string, object> values)
        {
            stateValues = values;
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "BusinessOperationParameter".ToUpper())
                {
                    _businessOperationParameter = item.Value as BusinessOperationParameter;
                    break;
                }
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
        /// 文件名集合
        /// </summary>
        List<string> FilesNames = new List<string>();

        /// <summary>
        /// 主体数据
        /// </summary>
        OceanMBLInfo _CurrentBLInfo = null;

        OceanBookingInfo _bookingInfo = null;

        VGMInfo _CurrentVGMInfo = null;

        /// <summary>
        /// 箱数据
        /// </summary>
        List<OceanBLContainerList> _ctnList = null;

        /// <summary>
        /// 代理下拉数据源
        /// </summary>

        /// <summary>
        /// 订阅箱面板的改变事件.来判断箱是否有改变.用于在保存时判断是否需要保存箱
        /// </summary>
        bool isChangedCtnList = false;
        /// <summary>
        /// 是否已根据bookingContainerDescription生成过箱,控制只生成
        List<CustomerList> _agentCustomersList = null;
        /// <summary>
        /// 
        /// </summary>
        bool isInitBookingContainerDescription = false;
        /// <summary>
        /// Booking的箱描述.在新单时用来生成箱的
        /// </summary>
        ContainerDescription bookingContainerDescription;

        /// <summary>
        /// 邮件中心与ICP业务关联信息
        /// </summary>
        BusinessOperationParameter _businessOperationParameter = null;

        /// <summary>
        /// 通知人客户信息
        /// </summary>
        CustomerDescriptionForNew notifyinfo;

        string CarrierName = string.Empty;

        /// <summary>
        /// 标记是否保存已经保存了MBL
        /// </summary>
        bool isSave = false;

        bool isUpdateShipper = false;
        /// <summary>
        /// 控制只获取一次.请用使用CtnLists属性
        /// </summary>
        List<ContainerList> ctnTypes = null;
        List<ContainerList> CtnTypes
        {
            get
            {
                if (ctnTypes != null) return ctnTypes;

                ctnTypes = TransportFoundationService.GetContainerList(string.Empty, true, 0);
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

        /// <summary>
        /// 宁波公司ID
        /// </summary>
        Guid NBCompanyID = new Guid("A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9");

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
        #endregion

        #endregion

        #region 初始化的一些方法

        /// <summary>
        ///初始化Description对象
        /// </summary>
        private void InitCustomerDescriptionObject()
        {
            if (_CurrentBLInfo.ShipperDescription == null)
            {
                _CurrentBLInfo.ShipperDescription = new CustomerDescriptionForNew();
            }
            if (_CurrentBLInfo.ConsigneeDescription == null)
            {
                _CurrentBLInfo.ConsigneeDescription = new CustomerDescriptionForNew();
            }
            if (_CurrentBLInfo.NotifyPartyDescription == null)
            {
                _CurrentBLInfo.NotifyPartyDescription = new CustomerDescriptionForNew();
            }
            if (_CurrentBLInfo.AgentDescription == null)
            {
                _CurrentBLInfo.AgentDescription = new CustomerDescription();
            }

            txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
            txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.NotifyPartyID) && _CurrentBLInfo.NotifyPartyDescription == null)
            {
                txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";
                notifyinfo = new CustomerDescriptionForNew {Remark = "SAME AS CONSIGNEE"};
                _CurrentBLInfo.NotifyPartyDescription = notifyinfo;
            }
            else
            {
                txtNotifyPartyDescription.Text = _CurrentBLInfo.NotifyPartyDescription.ToString(LocalData.IsEnglish);
                if (txtNotifyPartyDescription.Text.Length == 0)
                {
                    txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";
                    notifyinfo = new CustomerDescriptionForNew();
                    notifyinfo.Remark = "SAME AS CONSIGNEE";
                    _CurrentBLInfo.NotifyPartyDescription = notifyinfo;
                }
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
        }
        private void OnPanelScrollClick(object sender, EventArgs e)
        {
            panelMain.Focus();
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            panelMain.Click += OnPanelScrollClick;

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID)) _CurrentBLInfo.Marks = @"N/M";

            SetComboboxEnumSource();
            SetComboboxSource();
            RefreshControlsByData();
            SetContractBoxByHasContract(!ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ContractID));
            SearchRegister();

            if (ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.DefaultCompanyID != new System.Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9"))
            {
                chkSpecial.Visible = false;
                container.Enabled = false;
                barAddHbl.Visibility = BarItemVisibility.Never;
                barEDIANL.Visibility = BarItemVisibility.Never;
                barGetCommodity.Visibility = BarItemVisibility.Never;
                barPrintLoadContainer.Visibility = BarItemVisibility.Never;
                barPrintLoadContainerCopy.Visibility = BarItemVisibility.Never;
            }

            if (_bookingInfo == null && _CurrentBLInfo != null && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID))
            {
                _bookingInfo = OceanExportService.GetOceanBookingInfo(_CurrentBLInfo.OceanBookingID);
            }

            if (_bookingInfo != null || !ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID))
            {
                btnContainer.Enabled = _CurrentBLInfo.OEOperationType != FCMOperationType.BULK;
            }
            else
            {
                btnContainer.Enabled = false;
            }

            if (_bookingInfo != null && _bookingInfo.CompanyID == NBCompanyID)
            {
                lblNBPODCode.Visible = txtNBPODCode.Visible = true;
            }

            if (_CurrentBLInfo == null || _CurrentBLInfo.HasFee)
            {
                checkHasFee.Checked = true;
            }
            else
            {
                checkHasFee.Checked = false;
            }

            if (!LocalData.IsDesignMode)
            {
                SmartPartClosing += MBLEditPart_SmartPartClosing;
                ActivateSmartPartClosingEvent(Workitem);
            }
            stxtRefNo.Focus();
            if (_CurrentBLInfo.CargoDescription != null && _CurrentBLInfo.CargoDescription.Type > 0)
            {
                chkSpecial.Checked = true;
            }
            else
                chkSpecial.Checked = false;

            if (_bookingInfo != null)
            {
                labContractName.Text = _bookingInfo.ContractName + Environment.NewLine + _bookingInfo.ItemCode;
            }
        }
        private bool isSaveOperationContact = false;
        BusinessOperationContext OperationContext = new BusinessOperationContext();
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

                OperationContext = GetContext(_CurrentBLInfo);
                stxtRefNo.Properties.ReadOnly = true;
                stxtRefNo.Properties.Buttons[0].Enabled = false;

                stxtRefNo.Text = _CurrentBLInfo.RefNo;
                stxtRefNo.Tag = _CurrentBLInfo.OceanBookingID;
            }

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID) == false) RefreshEnabledByBookingType(_CurrentBLInfo.OEOperationType);

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

            //if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID))
            //    btnContainer.Enabled = false;

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


            //提单抬头
            //if (this._CurrentBLInfo != null && !ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(this._CurrentBLInfo.BLTitleID))
            //{
            //    this.cmbBLTitle.ShowSelectedValue(this._CurrentBLInfo.BLTitleID, this._CurrentBLInfo.BLTitleName);
            //}
            //this.cmbBLTitle.OnFirstEnter += this.OncmbBLTitleFirstEnter;



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

        private void OnIssueByEnter(object sender, EventArgs e)
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

            cmbPaymentTerm.SelectedIndexChanged -= cmbPaymentTerm_SelectedIndexChanged;
            cmbPaymentTerm.SelectedIndexChanged += cmbPaymentTerm_SelectedIndexChanged;
        }
        private void OncmbQuantityUnitFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbQuantityUnit, DataDictionaryType.QuantityUnit, DataBindType.EName);
        }
        private void OncmbWeightUnitFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbWeightUnit, DataDictionaryType.WeightUnit, DataBindType.EName);
        }
        private void OncmbMeasurementUnitFirstEnter(object sender, EventArgs e)
        {
            ICPCommUIHelper.SetCmbDataDictionary(cmbMeasurementUnit, DataDictionaryType.MeasurementUnit, DataBindType.EName);
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
        private void OnstxtAgentFirstEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);

            foreach (CountryList c in _countryList)
            {
                stxtAgent.CountryItems.Add(new ImageComboBoxItem(c.EName));
            }

            SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);
            stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
            stxtAgent.EditValueChanged += stxtAgent_EditValueChanged;
            stxtAgent.EditValueChanging += stxtAgent_EditValueChanging;

        }
        /// <summary>
        /// 签发人 付款方式 运输条款 包装 重量 体积 代理
        /// </summary>
        void SetComboboxSource()
        {
            #region 签发人-- 属于操作口岸，并且角色为操作员的用户
            mcmbIssueBy.Enter += OnIssueByEnter;

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
            cmbMeasurementUnit.OnFirstEnter += OncmbMeasurementUnitFirstEnter;

            #endregion

            #region 运输条款

            cmbTransportClause.ShowSelectedValue(_CurrentBLInfo.TransportClauseID, _CurrentBLInfo.TransportClauseName);
            cmbTransportClause.OnFirstEnter += OncmbTransportClauseFirstEnter;

            #endregion

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

            #region Voyage
            stxtPreVoyage.EditValueChanged += stxtPreVoyage_EditValueChanged;
            stxtVoyage.EditValueChanged += stxtVoyage_EditValueChanged;
            #endregion

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
        /// <summary>
        /// 订舱人发生改变
        /// </summary>
        private void BookingPartyChanged()
        {
            if (isUpdateShipper)
            {

                _CurrentBLInfo.ShipperID = _CurrentBLInfo.BookingPartyID;
                _CurrentBLInfo.ShipperName = _CurrentBLInfo.BookingPartyName;


                CustomerDescription bookingPartyDescription = new CustomerDescription();

                ICPCommUIHelper.SetCustomerDesByID(_CurrentBLInfo.BookingPartyID, bookingPartyDescription);

                _CurrentBLInfo.ShipperDescription = bookingPartyDescription.ConvertToNew();
                stxtShipper.DataSource = bookingPartyDescription.ConvertToNew();


                if (_CurrentBLInfo.ShipperDescription != null)
                    txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
            }
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

        #endregion

        #region 搜索器
        /// <summary>
        /// 缓存国家列表,只获取一次.现只用于客户弹出式描述框
        /// </summary>
        List<CountryList> _countryList = null;

        private void OnstxtShipperFirstTimeEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);
            //shipper
            CustomerForNewFinderBridge shipperBridge = new CustomerForNewFinderBridge(
            stxtShipper,
            _countryList,
            DataFindClientService,
            _CurrentBLInfo.ShipperDescription,
            txtShipperDescription,
            ICPCommUIHelper,
            LocalData.IsEnglish);
            shipperBridge.Init();
        }
        private void OnstxtConsigneeFirstTimeEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);
            CustomerForNewFinderBridge consigneeBridge = new CustomerForNewFinderBridge(
            stxtConsignee,
            _countryList,
            DataFindClientService,
            _CurrentBLInfo.ConsigneeDescription,
            txtConsigneeDescription,
            ICPCommUIHelper,
            LocalData.IsEnglish);
            consigneeBridge.Init();
        }
        private void OnstxtNotifyPartyFirstTimeEnter(object sender, EventArgs e)
        {
            if (_countryList == null) _countryList = GeographyService.GetCountryListByFCM(string.Empty, string.Empty, true, true, 0);

            CustomerForNewFinderBridge notifyPartyBridge = new CustomerForNewFinderBridge(
             stxtNotifyParty,
             _countryList,
             DataFindClientService,
             _CurrentBLInfo.NotifyPartyDescription,
             txtNotifyPartyDescription,
             ICPCommUIHelper,
             LocalData.IsEnglish
             , "SAME AS CONSIGNEE");
            notifyPartyBridge.Init();
        }
        IDisposable placeOfReceiptFinder, polCodeFinder, podCodeFinder, placeOfDeliveryFinder, finalDestinationFinder, issuePlaceFinder, WeightSiteFinder;
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
                      if (_CurrentBLInfo.OceanBookingID != bookingID)
                      {
                          AfterSearchRefNo(bookingID);
                          btnContainer.Enabled = _CurrentBLInfo.OEOperationType != FCMOperationType.BULK;
                      }
                      else
                      {
                          LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ?
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

                  }, ClientConstants.MainWorkspace);

            #endregion

            #region Customer

            #region SCNA

            //shipper
            stxtShipper.OnFirstEnter += OnstxtShipperFirstTimeEnter;

            stxtShipper.OnOk += stxtShipper_OnOk;
            //Consignee
            stxtConsignee.OnFirstEnter += OnstxtConsigneeFirstTimeEnter;
            stxtConsignee.OnOk += stxtConsignee_OnOk;

            //NotifyParty
            stxtNotifyParty.OnFirstEnter += OnstxtNotifyPartyFirstTimeEnter;
            stxtNotifyParty.OnOk += stxtNotifyParty_OnOk;
            #endregion

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
                List<CustomerCarrierObjects> contactList = UCMblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _CurrentBLInfo.CheckerID);
                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.CheckerID))
                {
                    _CurrentBLInfo.CheckerID = Guid.Empty;
                }
                stxtChecker.SetOperationContext(OperationContext);
                checkerCustomerFinderBridge = new CustomerContactFinderBridge(stxtChecker, null, contactList, ContactStage.BL, (Guid)_CurrentBLInfo.CheckerID, true, ContactType.Customer);
                checkerCustomerFinderBridge.Init();
                stxtChecker.OnOk += stxtChecker_OnOk;
                stxtChecker.OnRefresh += stxtChecker_OnRefresh;
                stxtChecker.BeforeEditValueChanged += stxtChecker_EditValueChanging;
                stxtChecker.AfterEditValueChanged += stxtChecker_EditValueChanged;
            });


            #endregion

            #region Port

            //驳船 搜索的默认条件为 装货港=当前收货地and卸货港=当前装货港
            //大船 筛选：装货港=当前装货港and卸货港=当前卸货港

            #region PlaceOfReceipt
            placeOfReceiptFinder = DataFindClientService.Register(stxtPlaceOfReceipt, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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

            WeightSiteFinder = DataFindClientService.Register(cmbWeightSite, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
                    delegate(object inputSource, object[] resultData)
                    {
                        Guid portID = new Guid(resultData[0].ToString());
                        txtWeightSite.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                        cmbWeightSite.Text = resultData[1].ToString();
                        cmbWeightSite.Tag = portID;
                    },
                    delegate
                    {
                        cmbWeightSite.Tag = null;
                        cmbWeightSite.Text = string.Empty;
                        txtWeightSite.Text = string.Empty;
                    },
                    ClientConstants.MainWorkspace);

            #region POL

            polCodeFinder = DataFindClientService.Register(txtPOLCode, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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

            podCodeFinder = DataFindClientService.Register(txtPODCode, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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
                    ClientConstants.MainWorkspace);
            #endregion
            #region PlaceOfDelivery

            placeOfDeliveryFinder = DataFindClientService.Register(stxtPlaceOfDelivery, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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

            finalDestinationFinder = DataFindClientService.Register(stxtFinalDestination, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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

            issuePlaceFinder = DataFindClientService.Register(stxtIssuePlace, CommonFinderConstants.OceanLocationFinder, SearchFieldConstants.CodeName, SearchFieldConstants.PortResultValue,
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

            #endregion
        }


        void stxtChecker_OnOk(object sender, EventArgs e)
        {
            List<CustomerCarrierObjects> currentList = stxtChecker.ContactList;
            UCMblOtherPart.CurrentContactList.RemoveAll(item => item.CustomerID == _CurrentBLInfo.CheckerID && item.Type == ContactType.Customer);
            if (currentList.Count > 0)
            {
                UCMblOtherPart.InsertContactList(currentList);
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

            List<CustomerCarrierObjects> contactList = UCMblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == _CurrentBLInfo.CheckerID && item.Type == ContactType.Customer);
            UCMblOtherPart.CurrentContactList.RemoveAll(item => contactList.Contains(item));
            UCMblOtherPart.InsertContactList(temp);
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


        void stxtNotifyParty_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForNew des = stxtNotifyParty.DataSource;

            if (des == null)
            {
                des = new CustomerDescriptionForNew();
            }
            _CurrentBLInfo.NotifyPartyDescription = des;
        }

        void stxtConsignee_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForNew des = stxtConsignee.DataSource;
            if (des == null)
            {
                des = new CustomerDescriptionForNew();

            }
            _CurrentBLInfo.ConsigneeDescription = des;
        }

        void stxtShipper_OnOk(object sender, EventArgs e)
        {
            CustomerDescriptionForNew des = stxtShipper.DataSource ?? new CustomerDescriptionForNew();
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
            List<CustomerCarrierObjects> contactList = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentBLInfo.CompanyID, customerID, contactType, ContactStage.Unknown);
            if (contactList == null || contactList.Count <= 0)
                return;
            for (int i = 0; i < contactList.Count; i++)
            {
                contactList[i].Id = Guid.Empty;

            }
            List<CustomerCarrierObjects> currentContactList = UCMblOtherPart.CurrentContactList;
            if (currentContactList == null || currentContactList.Count <= 0)
            {
                UCMblOtherPart.InsertContactList(contactList);
            }
            else
            {
                List<string> nameList = (from item in currentContactList select item.Name).ToList();
                List<string> emailList = (from item in currentContactList select item.Mail).ToList();

                List<CustomerCarrierObjects> needAddContactList = contactList.FindAll(item => !nameList.Contains(item.Name) && !emailList.Contains(item.Mail));
                UCMblOtherPart.InsertContactList(needAddContactList);
            }
            customerControl.GetType().GetProperty("ContactList").SetValue(customerControl, contactList, null);

        }

        #endregion

        private List<CustomerCarrierObjects> GetCurrentContactListByCustomerID(Guid customerID, ContactType contactType)
        {
            List<CustomerCarrierObjects> contactList = UCMblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID && item.Type == contactType);
            return contactList;
        }

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
                BulidAgentDescriqitonByID(_CurrentBLInfo.AgentID);
            else
            {
                stxtAgent.CustomerDescription = _CurrentBLInfo.AgentDescription;
            }
            stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
            stxtAgent.EditValueChanged += stxtAgent_EditValueChanged;

            stxtAgent.OnOk -= stxtAgent_OnOk;
            stxtAgent.OnOk += stxtAgent_OnOk;

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
            if (ArgumentHelper.GuidIsNullOrEmpty(id))
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
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID) == false && _CurrentBLInfo.OceanBookingID != bookingID)
            {
                ///	否则提示：“是否重新导入发货人、收货人、通知人、地点、货物信息？”，如果选择是，则继续执行下一步，否则退出。
                if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "是否重新导入发货人、收货人、通知人、地点、货物信息?"
                                 , LocalData.IsEnglish ? "Tip" : "提示"
                                 , MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

            }
            #endregion
            #region 填充

            /*MBL新的取值逻辑默认值取值
             Shipper、Consignee、NotifyParty、TransportClause、ReleaseType、PaymentTerm、IsThirdPlace、IsThirdPlace默认为ShippingOrder；
             Shipper、Consignee、NotifyParty如果ShippingOrder中没有值，按照以前规则取值；
             Marks、GoodsDescription（对应OceanBooking中Commodity）、IsWoodPacking如果没有关联HBL的情况下默认取OceanBooking中；*/
            _bookingInfo = OceanExportService.GetOceanBookingInfo(bookingID);
            ConfigureInfo _configureInfo = ConfigureService.GetCompanyConfigureInfo(_bookingInfo.CompanyID, LocalData.IsEnglish);

            _CurrentBLInfo.CustomerName = _bookingInfo.CustomerName;
            _CurrentBLInfo.OEOperationType = _bookingInfo.OEOperationType;
            _CurrentBLInfo.BLTitleID = ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.BLTitleID) ? _configureInfo.BLTitleID : _bookingInfo.BLTitleID;
            _CurrentBLInfo.BLTitleName = string.IsNullOrEmpty(_bookingInfo.BLTitleName) ? _configureInfo.BLTitleName : _bookingInfo.BLTitleName;
            //cmbBLTitle.ShowSelectedValue(_CurrentBLInfo.BLTitleID, _CurrentBLInfo.BLTitleName);

            _CurrentBLInfo.ContractID = _bookingInfo.ContractID;
            _CurrentBLInfo.ContractNo = _bookingInfo.ContractNo;
            _CurrentBLInfo.IsChargePayOrCon = false;

            _CurrentBLInfo.CompanyID = _bookingInfo.CompanyID;
            if (!ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.CarrierID))
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

            #endregion

            #region Shipper  MBL.发货人 = 订舱单.发货人

            bool shipperIsNull = ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.BookingShipperID);
            bool consigneeIsNull = ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.BookingConsigneeID);
            bool notifyPartyIsNull = ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.BookingNotifyPartyID);

            //只出MBL
            //	MBL.发货人 = 订舱单.发货人,MBL.发货人描述 = 根据MBL.发货人生成
            //	MBL.收货人 = 订舱单.收货人,MBL.收货人描述 = 根据MBL.收货人生成
            if (_bookingInfo.IsOnlyMBL)
            {
                #region 发货人
                _CurrentBLInfo.ShipperID = shipperIsNull ? _bookingInfo.ShipperID : _bookingInfo.BookingShipperID;
                _CurrentBLInfo.ShipperName = shipperIsNull ? _bookingInfo.ShipperName : _bookingInfo.BookingShipperName;
                stxtShipper.DataSource = _bookingInfo.BookingShipperdescription.ConvertToNew();
                _CurrentBLInfo.ShipperDescription = (shipperIsNull ? _bookingInfo.ShipperDescription : _bookingInfo.BookingShipperdescription).ConvertToNew();
                if (_CurrentBLInfo.ShipperDescription != null)
                    txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
                #endregion

                #region 收货人
                _CurrentBLInfo.ConsigneeID = consigneeIsNull ? _bookingInfo.ConsigneeID : _bookingInfo.BookingConsigneeID;
                _CurrentBLInfo.ConsigneeName = consigneeIsNull ? _bookingInfo.ConsigneeName : _bookingInfo.BookingConsigneeName;
                stxtConsignee.DataSource = _CurrentBLInfo.ConsigneeDescription = (consigneeIsNull ? _bookingInfo.ConsigneeDescription : _bookingInfo.BookingConsigneedescription).ConvertToNew();
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
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_bookingInfo.CompanyID);

                _CurrentBLInfo.ShipperID = shipperIsNull ? configureInfo.CustomerID : _bookingInfo.BookingShipperID;
                _CurrentBLInfo.ShipperName = shipperIsNull ? configureInfo.CustomerName : _bookingInfo.BookingShipperName;
                ICPCommUIHelper.SetCustomerDesByID(_CurrentBLInfo.ShipperID, _CurrentBLInfo.ShipperDescription);
                if (_CurrentBLInfo.ShipperDescription != null)
                    txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);
                #endregion

                #region 收货人
                _CurrentBLInfo.ConsigneeID = consigneeIsNull ? _bookingInfo.AgentID : _bookingInfo.BookingConsigneeID;
                _CurrentBLInfo.ConsigneeName = consigneeIsNull ? _bookingInfo.AgentName : _bookingInfo.BookingConsigneeName;
                stxtConsignee.DataSource = _CurrentBLInfo.ConsigneeDescription = (consigneeIsNull ? _bookingInfo.AgentDescription : _bookingInfo.BookingConsigneedescription).ConvertToNew();
                if (_CurrentBLInfo.ConsigneeDescription != null)
                    txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);
                #endregion
            }

            #endregion

            #region NotifyParty = 通知人描述 = “SAME AS CONSIGNEE”

            _CurrentBLInfo.NotifyPartyID = notifyPartyIsNull ? Guid.Empty : _bookingInfo.BookingNotifyPartyID;
            _CurrentBLInfo.NotifyPartyName = notifyPartyIsNull ? string.Empty : _bookingInfo.BookingNotifyPartyname;
            stxtNotifyParty.DataSource = _CurrentBLInfo.NotifyPartyDescription = (notifyPartyIsNull ? new CustomerDescription() : _bookingInfo.BookingNotifyPartydescription).ConvertToNew();
            txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";

            #endregion

            #region 订舱人
            _CurrentBLInfo.BookingPartyID = _bookingInfo.BookingPartyID;
            _CurrentBLInfo.BookingPartyName = _bookingInfo.BookingPartyName;

            isUpdateShipper = _CurrentBLInfo.ShipperID == _CurrentBLInfo.BookingPartyID ? true : false;
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


            //_CurrentBLInfo.BookingPaymentTermID = _CurrentBLInfo.TransportClauseID = _bookingInfo.TransportClauseID;
            //_CurrentBLInfo.TransportClauseName = _bookingInfo.TransportClauseName; 
            _CurrentBLInfo.TransportClauseID = ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.MBLTransportClauseID) ? _bookingInfo.TransportClauseID : (Guid)_bookingInfo.MBLTransportClauseID;
            _CurrentBLInfo.TransportClauseName = ArgumentHelper.GuidIsNullOrEmpty(_bookingInfo.MBLTransportClauseID) ? _bookingInfo.TransportClauseName : _bookingInfo.MBLTransportClauseName;
            cmbTransportClause.ShowSelectedValue(_CurrentBLInfo.TransportClauseID, _CurrentBLInfo.TransportClauseName);

            _CurrentBLInfo.IsThirdPlacePayOrder = _bookingInfo.IsThirdPlacePayOrder;
            _CurrentBLInfo.CollectbyAgentOrderID = _bookingInfo.CollectbyAgentOrderID;
            _CurrentBLInfo.CollectbyAgentNameOrder = _bookingInfo.CollectbyAgentNameOrder;
            _CurrentBLInfo.ReleaseType = (FCMReleaseType)_bookingInfo.MBLReleaseType;
            cmbReleaseType.ShowSelectedValue(_CurrentBLInfo.ReleaseType, _CurrentBLInfo.ReleaseTypeName);

            cmbPaymentTerm.SelectedIndexChanged -= cmbPaymentTerm_SelectedIndexChanged;
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

            cmbPaymentTerm.SelectedIndexChanged += cmbPaymentTerm_SelectedIndexChanged;

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

            if (_bookingInfo != null || !ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.OceanBookingID))
            {
                btnContainer.Enabled = _CurrentBLInfo.OEOperationType != FCMOperationType.BULK;
            }
            else
            {
                btnContainer.Enabled = false;
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
            if (!ArgumentHelper.GuidIsNullOrEmpty(shippingLineID) && !ArgumentHelper.GuidIsNullOrEmpty(shipperID) && OEUtility.NAShippingLines.Contains(shipperID.Value))
            {
                stxtShipper.Properties.Appearance.BackColor = SystemColors.Info;
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
        //    if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID))
        //    {
        //        chkShowVoyage.Checked = chkShowVoyage.Enabled = false;

        //        if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
        //        {
        //            dteETD.EditValue = _CurrentBLInfo.ETD = null;
        //        }
        //        dteETA.EditValue = _CurrentBLInfo.ETA = null;

        //    }
        //    else
        //    {
        //        VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.VoyageID.Value);
        //        dteETA.EditValue = _CurrentBLInfo.ETA = voyageInfo.ETA;
        //        if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
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
        //    if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.PreVoyageID))
        //    {
        //        chkShowPreVoyage.Checked = chkShowPreVoyage.Enabled = false;
        //        dteETD.EditValue = _CurrentBLInfo.ETD = null;
        //        if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false)
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
                            //_isNeedRequestAgent = true;
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
            cmbPaymentTerm.SelectedIndexChanged -= cmbPaymentTerm_SelectedIndexChanged;

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.BookingPaymentTermID) == false && _CurrentBLInfo.BookingPaymentTermID != _CurrentBLInfo.PaymentTermID)
            {
                if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "现选择的MBL付款方式与订舱单中MBL付款方式不同,是否继续?",
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


            cmbPaymentTerm.SelectedIndexChanged += cmbPaymentTerm_SelectedIndexChanged;
        }

        #endregion

        #endregion


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
                        LocalCommonServices.ErrorTrace.SetErrorInfo(null, ex.Message);
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
            string labTag = saveState();
            if (!string.IsNullOrEmpty(labTag) && labTag.Equals("success"))
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
            barlabSeconds.Visibility = BarItemVisibility.Always;
            barlabSeconds.Caption = string.Format((LocalData.IsEnglish ? "({0} seconds)" : "({0} 秒)"), (int)span.TotalSeconds);
            if (span.TotalSeconds >= LocalData.AsynchronousSaveTimeout)
            {
                barCancel.Visibility = BarItemVisibility.Always;
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
                    SetLableMessage(
                    string.Format((LocalData.IsEnglish ? "Saving is successful at {0}" : "保存成功 于 {0}"), DateTime.Now.ToString("HH:mm:ss"))
                    , "success");
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
        #endregion

        private bool SaveOtherPart()
        {
            if ((_CurrentBLInfo != null && UCMblOtherPart.IsChanged) || isSaveOperationContact)
            {
                Stopwatch StopwatchStep = Stopwatch.StartNew();
                OperationLogID = Guid.NewGuid();
                //保存联系人列表及附件
                UCMblOtherPart.SetContext = GetContext(_CurrentBLInfo);
                UCMblOtherPart.Save(_CurrentBLInfo.UpdateDate);
                UpdateContactControlData();
                if (_businessOperationParameter == null)
                {
                    _businessOperationParameter = new BusinessOperationParameter();
                }

                _businessOperationParameter.Context = GetContext(_CurrentBLInfo);

                if (Saved != null) Saved(new object[] { _CurrentBLInfo,_businessOperationParameter, _businessOperationParameter.Context });

                StopwatchHelper.CustomRecordStopwatch(StopwatchStep, OperationLogID, DateTime.Now, BaseFormID,
                    "SAVE-MBL-OTHER", string.Format("保存MBL其它面板:联系人列表及附件;OperationID[{0}]MBL ID[{1}]", _CurrentBLInfo.OceanBookingID, _CurrentBLInfo.ID));
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "数据保存成功");
            }
            return true;
        }

        private bool Save(bool needValidate = true)
        {
            try
            {
                StopwatchSaveData = Stopwatch.StartNew();
                OperationLogID = Guid.NewGuid();
                StopwatchHelper.CustomRecordStopwatch(StopwatchSaveData, OperationLogID, DateTime.Now,
                    BaseFormID, "SAVE-MBL", string.Format("保存MBL;OperationID[{0}]MBL ID[{1}]", _CurrentBLInfo.OceanBookingID, _CurrentBLInfo.ID));

                if (needValidate && ValidateData(false) == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:数据未通过验证");
                    return false;
                }

                #region MBLNo已经存在，提示是否继续保存

                if (OceanExportService.IsExistsMBLNo(_CurrentBLInfo.MBLID, _CurrentBLInfo.No))
                {
                    string message = string.Format(NativeLanguageService.GetText(this, "IsExisteMBLNo"),
                        _CurrentBLInfo.No);

                    DialogResult dr = ShowQuestion(message, LocalData.IsEnglish ? " Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                    //DialogResult dr = MessageBoxService.Show(message,
                    //    (LocalData.IsEnglish ? " Tip" : "提示"),
                    //    MessageBoxButtons.YesNo,
                    //    MessageBoxIcon.Question,
                    //    MessageBoxDefaultButton.Button2);

                    if (dr == DialogResult.No || dr == DialogResult.Cancel)
                    {
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:MBL已存在");
                        return false;
                    }

                }

                #endregion

                if (_CurrentBLInfo.IsDirty == false && _CurrentBLInfo.IsNew == false && isChangedCtnList == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存成功:数据未更改!");
                    return true;
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Saveing......" : "保存中.....");

                barSave.Enabled = false;
                barSavingClose.Enabled = false;
                panelMain.Enabled = false;

                if (!AutoRequestAgent())
                {
                    return false;
                }
                bool isNew = false;


                if (ctnIDList.Count > 0 || isChangedCtnList == true)
                {
                    #region 保存MBL、箱信息

                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveMBLInfoParameter mbl = ConvertMBLToParameter(false, _CurrentBLInfo);
                    SaveBLContainerParameter ctn = ConvertCtnToParameter(false, _CurrentBLInfo.OceanBookingID, _ctnList);

                    SingleResult result = OceanExportService.SaveOceanMBLAndContainerWithTrans(mbl, ctn, ctnIDList,
                        ctnUpdateDateList, true);


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
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true,
                        string.Format("MBL & 箱信息保存成功 [{0}ms]", StopwatchStep.ElapsedMilliseconds));

                    #endregion
                }
                else
                {
                    #region 只保存MBL，没有箱或者箱没有发生改变时

                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveMBLInfoParameter mbl = ConvertMBLToParameter(false, _CurrentBLInfo);


                    SingleResult result = OceanExportService.SaveOceanMBLInfo(mbl);

                    isNew = ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.MBLID);

                    _CurrentBLInfo.MBLID = _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    isSave = true;
                    _CurrentBLInfo.IsDirty = false;
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true,
                        string.Format("MBL 保存成功 [{0}ms]", StopwatchStep.ElapsedMilliseconds));

                    #endregion
                }

                barSaveVGM_ItemClick(null, null);
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");

                #region 重新生成账单

                if (isNew && isFirstMBL)
                {
                    #region 新的MBL,判断是否为这个业务的第一个MBL

                    if (isFirstMBL && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ContractID) && isHasContainer)
                    {
                        //isFirstMBL 是第一个MBL
                        //ContractID 不为空
                        //isHasContainer 有箱列表
                        Stopwatch StopwatchStep = Stopwatch.StartNew();
                        try
                        {
                            SingleResult result = OceanExportService.CreateBill(_CurrentBLInfo.OceanBookingID,
                                LocalData.UserInfo.LoginID);
                            if (result != null)
                            {
                                int s = result.GetValue<Byte>("State");
                                if (s == 1)
                                {
                                    ShowMessage(NativeLanguageService.GetText(this, "CreateBills"));

                                    //MessageBoxService.ShowInfo(
                                    //    NativeLanguageService.GetText(this, "CreateBills"));

                                    _CurrentBLInfo.IsChargePayOrCon = false;
                                    isCtnCharge = false;
                                }
                                else if (s == 2)
                                {
                                    string title = (LocalData.IsEnglish ? "Generate the bill Error:" : "生成账单失败：");
                                    string message = result.GetValue<string>("ErrorMessage");
                                    ShowMessage(title + message);
                                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                        }
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true, string.Empty,
                            string.Format("已生成账单 [{0}ms]", StopwatchStep.ElapsedMilliseconds));
                    }

                    #endregion
                }
                else
                {
                    if (isHasContainer && _CurrentBLInfo.IsHasContract)
                    {
                        #region 编辑MBL时，如果付款方式、合约、箱信息有改变,则重新生成账单
                        if (_CurrentBLInfo.IsChargePayOrCon || isCtnCharge)
                        {
                            try
                            {
                                Stopwatch StopwatchStep = Stopwatch.StartNew();
                                SingleResult result = OceanExportService.CreateBill(_CurrentBLInfo.OceanBookingID,
                                    LocalData.UserInfo.LoginID);

                                if (result != null)
                                {

                                    int s = result.GetValue<Byte>("State");
                                    string title = string.Empty;
                                    if (s == 1)
                                    {
                                        if (_CurrentBLInfo.IsChargePayOrCon && isCtnCharge)
                                        {
                                            title = NativeLanguageService.GetText(this, "CreateBillsForALL");
                                        }
                                        else if (isCtnCharge)
                                        {
                                            title = NativeLanguageService.GetText(this, "CreateBillsForContainer");
                                        }
                                        else if (_CurrentBLInfo.IsChargePayOrCon)
                                        {
                                            title = NativeLanguageService.GetText(this, "CreateBillsForPayORCon");
                                        }
                                        if (!string.IsNullOrEmpty(title))
                                        {
                                            ShowMessage(title);
                                            StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID,
                                                true, string.Empty,
                                                string.Format(title + "{0}", StopwatchStep.ElapsedMilliseconds));
                                        }
                                        _CurrentBLInfo.IsChargePayOrCon = false;
                                        isCtnCharge = false;
                                    }
                                    else if (s == 2)
                                    {
                                        string message = result.GetValue<string>("message");

                                        title = LocalData.IsEnglish ? "Generate the bill Error:" : "生成账单失败：";
                                        ShowMessage(title + message);
                                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false,
                                            string.Empty,
                                            string.Format(title + "{0}", StopwatchStep.ElapsedMilliseconds));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true,
                                    string.Empty, string.Format("更新账单失败 SessionId[{0}]", LocalData.SessionId));
                                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                            }
                        }
                        #endregion
                    }
                }

                #endregion

                ctnIDList = new List<Guid>();
                ctnUpdateDateList = new List<DateTime?>();

                FilesNames = UCMblOtherPart.CurrentDocumentName;

                AfterSave(false);

                RefreshCustomerDesc();
                return true;
            }
            catch (Exception ex)
            {
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存失败，SessionId[{0}]", LocalData.SessionId));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
            finally { barSave.Enabled = true; barSavingClose.Enabled = true; panelMain.Enabled = true; }
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

        void AfterSave(bool IsSaveAs)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(AfterSave), IsSaveAs);
            }
            else
            {
                string MBLID = "";
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
                Title = LocalData.IsEnglish ? "Edit MBL " + _CurrentBLInfo.No : "编辑MBL " + _CurrentBLInfo.No;
                RefreshBarEnabled();


                if (!UCMblOtherPart.IsChanged && !isSaveOperationContact)
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
                    MBLID = string.Format("MBL ID[{0}]", _CurrentBLInfo.ID);
                    EditMode = EditMode.Edit;
                }
                string message = string.Empty;

                if (IsSaveAs)
                    message = LocalData.IsEnglish ? "Save as a new bl successfully." : "已成功另存为一票新的提单.";
                else
                {
                    message = LocalData.IsEnglish ? "Save Successfully" : "保存成功";
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);

                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Empty, string.Empty, MBLID + message);
            }
        }

        private bool ValidateData(bool IsSaveAs)
        {
            EndEdit();

            List<bool> isScrrs = new List<bool> { true, true };

            string errorInfo = string.Empty;
            if (_CurrentBLInfo.Validate
                (
                    delegate(ValidateEventArgs e)
                    {
                        if (_CurrentBLInfo.ContactID != Guid.Empty && (_CurrentBLInfo.AgentID == Guid.Empty || _CurrentBLInfo.AgentID == null))
                        {
                            e.SetErrorInfo(null, LocalData.IsEnglish ? "Agent Must Input" : "代理必须输入.");
                        }

                        if (_CurrentBLInfo.POLID != Guid.Empty && _CurrentBLInfo.POLID == _CurrentBLInfo.PODID)
                        {
                            e.SetErrorInfo("PODID", LocalData.IsEnglish ? "POD can't Same as POL." : "卸货港不能和装货港相同.");
                        }
                        if (string.IsNullOrEmpty(_CurrentBLInfo.No))
                        {
                            e.SetErrorInfo("No", LocalData.IsEnglish ? "MBL NO Must Input" : "MBL NO 必须输入.");
                        }
                        if (_CurrentBLInfo.ShippingLineID != null && OEUtility.NAShippingLines.Contains(_CurrentBLInfo.ShippingLineID.Value) && ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ShipperID))
                        {
                            e.SetErrorInfo("ShipperID", LocalData.IsEnglish ? "Shipper Must Input" : "收货人必须输入.");
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

                        if (string.IsNullOrEmpty(CarrierName))
                        {
                            CustomerInfo cus = CustomerService.GetCustomerInfo((Guid)_CurrentBLInfo.CarrierID);
                            CarrierName = cus.Code;
                        }

                        bool isUsa = false;
                        if (_CurrentBLInfo.FinalDestinationID != null)
                        {
                            LocationInfo FinalDestination = GeographyService.GetLocationInfo((Guid)_CurrentBLInfo.FinalDestinationID);
                            if (FinalDestination.CountryID.ToString().ToUpper() == "37F06C2D-E5F6-4A6F-BB55-9DA3EC3B42A4")
                            {
                                isUsa = true;
                            }
                        }
                        else
                        {
                            if (USAShippingLines.Count(r => r == _CurrentBLInfo.ShippingLineID) > 0)
                            {
                                isUsa = true;
                            }
                        }

                        if (CarrierName == "MSC")
                        {
                            if (isUsa && _CurrentBLInfo.ContractID != Guid.Empty && _CurrentBLInfo.ContractID != null)
                            {
                                if (_CurrentBLInfo.BookingPartyID.ToString().ToUpper() != "0751E34D-6FC6-E511-938F-0026551CA878")
                                {
                                    e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract can only be used DAWU as the booking party" : "MSC的合约，只能用达悟作为订舱订舱人");
                                    isScrrs[0] = false;
                                }
                                if (_CurrentBLInfo.ShipperID.ToString().ToUpper() != "0751E34D-6FC6-E511-938F-0026551CA878")
                                {
                                    e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract can only be used DAWU as the booking shipper" : "MSC的合约，只能用达悟作为订舱发货人");
                                    isScrrs[0] = false;
                                }
                                if (_CurrentBLInfo.ConsigneeID.ToString().ToUpper() != "B8006234-2F00-E611-80D5-2047477D7A58")
                                {
                                    e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract can only be used JDY as the booking consignee" : "MSC的合约，只能用JDY作为订舱收货人");
                                    isScrrs[0] = false;
                                }
                                if (_CurrentBLInfo.AgentID.ToString().ToUpper() != "B8006234-2F00-E611-80D5-2047477D7A58")
                                {
                                    e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract can only be used DAWU as the agent" : "MSC的合约，只能用JDY作为代理");
                                    isScrrs[0] = false;
                                }
                            }
                            else if (!isUsa)
                            {
                                string message = LocalData.IsEnglish ? "Is the business co-load to your counterparts?" : "此票业务是否co-load给同行？";
                                if (_CurrentBLInfo.ShipperName.ToString().Contains("CITY OCEAN"))
                                {
                                    DialogResult dr = ShowQuestion(message, LocalData.IsEnglish ? " Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                                    if (dr == DialogResult.No)
                                    {
                                        e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract， The shipper cannot be CITY OCEAN" : "MSC的船东，发货人不能为CITY OCEAN");
                                        isScrrs[0] = false;
                                    }
                                }
                                if (_CurrentBLInfo.ConsigneeName.ToString().Contains("CITY OCEAN"))
                                {
                                    DialogResult dr = ShowQuestion(message, LocalData.IsEnglish ? " Tip" : "提示", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                                    if (dr == DialogResult.No)
                                    {
                                        e.SetErrorInfo("ContractID", LocalData.IsEnglish ? "MSC's contract， The shipper cannot be CITY OCEAN" : "MSC的船东，收货人不能为CITY OCEAN");
                                        isScrrs[0] = false;
                                    }
                                }
                            }
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

            if (UCMblOtherPart.ValidateData() == false)
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
                if (ValidateData(true) == false)
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

                if (_ctnList == null) _ctnList = OceanExportService.GetOceanMBLContainerList(_CurrentBLInfo.ID);
                bool needSaveCtn = false; //如果没有关联的箱，不提交保存箱
                bool isSaveCtn = false; //询问用户是否保存箱

                #region

                foreach (var item in _ctnList)
                {
                    if (item.Relation)
                    {
                        needSaveCtn = true;
                        continue;
                    }
                }
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

                //txtCtnInfo.Text = _CurrentBLInfo.ContainerDescription = string.Empty;
                if (isSaveCtn == false)
                {
                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveMBLInfoParameter mbl = ConvertMBLToParameter(true, _CurrentBLInfo);
                    SingleResult result = OceanExportService.SaveOceanMBLInfo(mbl);
                    _CurrentBLInfo.MBLID = _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    txtCtnInfo.Text = _CurrentBLInfo.ContainerDescription = string.Empty;
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false,
                        string.Format("MBL[{0}]保存成功[{1}]", _CurrentBLInfo.No, StopwatchStep.ElapsedMilliseconds));
                }
                else
                {
                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveMBLInfoParameter mbl = ConvertMBLToParameter(true, _CurrentBLInfo);
                    SaveBLContainerParameter ctn = ConvertCtnToParameter(true, _CurrentBLInfo.OceanBookingID, _ctnList);
                    SingleResult result = OceanExportService.SaveOceanMBLAndContainerWithTrans(mbl, ctn, null, null,
                        true);

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

                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false,
                        string.Format("MBL [{0}] & 箱信息 保存成功[{1}]", _CurrentBLInfo.No, StopwatchStep.ElapsedMilliseconds));
                }
                AfterSave(true);

                return true;
            }
            catch (Exception ex)
            {
                StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存失败，SessionId[{0}]", LocalData.SessionId));
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); return false;
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
                        _ctnList = OceanExportService.GetOceanMBLContainerList(_CurrentBLInfo.ID);
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
                                        newCtn.ShippingOrderNo = OceanShippingOrderNo;
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
                if (containerListPart == null || containerListPart.IsDisposed)
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
                        isChangedCtnList = true;

                        #region 确定箱后更新页面信息
                        _ctnList = prams[0] as List<OceanBLContainerList>;
                        if (_ctnList != null && _ctnList.Count != 0)
                        {
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
                PartLoader.FakeShowDialog(Workitem, containerListPart, (LocalData.IsEnglish ? "Edit MBLCotainer" : "编辑MBL箱信息"), FormWindowState.Normal, this);

                //PartLoader.ShowDialog(containerListPart, LocalData.IsEnglish ? "Edit MBLCotainer" : "编辑MBL箱信息", FormBorderStyle.Sizable);
                // PartLoader.FormShowDialog(containerListPart, LocalData.IsEnglish ? "Edit MBLCotainer" : "编辑MBL箱信息");

            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        #endregion

        #region 关闭

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            FindForm().Close();
        }

        void MBLEditPart_SmartPartClosing(object sender, WorkspaceCancelEventArgs e)
        {
            if (_CurrentBLInfo.IsDirty && isSave == false)
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

        private void barPrintLoadGoods_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.IsDirty)
            {
                if (SaveData() == false) return;
            }
            ClientOceanExportService.PrintLoadGoods(_CurrentBLInfo);

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

        #region 对单

        private void barCheck_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.State != OEBLState.Draft) return;

            try
            {
                SingleResult result = OceanExportService.ChangeOceanMBLState(_CurrentBLInfo.ID, OEBLState.Checking, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);

                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _CurrentBLInfo.State = OEBLState.Checking;
                _CurrentBLInfo.IsDirty = isDirty;
                RefreshBarEnabled();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Check Successfully" : "设置对单成功.");
                if (Saved != null) Saved(new object[] { _CurrentBLInfo });
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
            RefreshBarEnabled();
        }

        private void barCheckDone_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty) return;

            try
            {
                SingleResult result = OceanExportService.ChangeOceanMBLState(_CurrentBLInfo.ID, OEBLState.Checked, LocalData.UserInfo.LoginID, _CurrentBLInfo.UpdateDate);

                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                _CurrentBLInfo.State = OEBLState.Checking;
                _CurrentBLInfo.IsDirty = isDirty;
                RefreshBarEnabled();
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Checked Successfully" : "设置完成对单成功.");
                if (Saved != null) Saved(new object[] { _CurrentBLInfo });
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }

            RefreshBarEnabled();
        }

        #endregion

        private void barReplyAgent_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool srcc = ClientOceanExportService.ReplyAgent(_CurrentBLInfo.OceanBookingID, null, null);
            if (srcc)
            {
                bool isDirty = _CurrentBLInfo.IsDirty;
                _CurrentBLInfo.IsRequestAgent = true;
                stxtAgent.Enabled = false;
                _CurrentBLInfo.IsDirty = isDirty;
            }
        }



        private void barBill_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty) return;
            ClientOceanExportService.OpenBill(_CurrentBLInfo.OceanBookingID, OperationType.OceanExport);
        }

        private void barRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {

            RefreshBLData();


        }

        private void RefreshBLData()
        {

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID)) return;

            Focus();
            _CurrentBLInfo = OceanExportService.GetOceanMBLInfo(_CurrentBLInfo.ID);
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
            bindingSource1.DataSource = _CurrentBLInfo;
            bindingSource1.ResetBindings(false);
            InitCustomerDescriptionObject();


            RefreshCustomerDesc();

            Refresh();

            //}
        }

        private void RemoveContactList(Guid changeID)
        {
            if (changeID == Guid.Empty)
                return;

            UCMblOtherPart.RemoveContactList(changeID, null);

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
        private delegate void DataBindDelegate(OceanMBLInfo mblInfo);
        void BindingData(OceanMBLInfo mblInfo)
        {
            if (mblInfo != null)
            {
                InnerBindData(mblInfo);
            }
            else
            {
                string operationNo = stateValues["OperationNo"] as string;
                string mblNo = stateValues["MBLNo"] as string;
                OceanMBLInfo temp = OceanExportService.GetOceanMBLInfo(operationNo, mblNo);
                InnerBindData(temp);
            }



        }
        private void InnerBindData(OceanMBLInfo mblInfo)
        {

            _CurrentBLInfo = mblInfo;
            UCMblOtherPart.Dock = DockStyle.Fill;
            navBarControlContainerContact.Controls.Clear();
            navBarControlContainerContact.Controls.Add(UCMblOtherPart);
            bindingSource1.DataSource = _CurrentBLInfo;
            _CurrentBLInfo.IsChargePayOrCon = false;

            isHasContainer = !string.IsNullOrEmpty(_CurrentBLInfo.ContainerNos);
            isFirstMBL = false;

            isUpdateShipper = _CurrentBLInfo.ShipperID == _CurrentBLInfo.BookingPartyID ? true : false;

            InitMessage();
            InitControls();
            RefreshBarEnabled();
            InitCustomerDescriptionObject();
            _originalMBLNO = _CurrentBLInfo.No;

            SetShipperColor(_CurrentBLInfo.ShippingLineID, _CurrentBLInfo.ShipperID);
            _CurrentBLInfo.IsDirty = false;
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();

            _CurrentVGMInfo = OceanExportService.GetVGMInfo(Guid.Empty, _CurrentBLInfo.MBLID);
            if (_CurrentVGMInfo == null)
            {
                _CurrentVGMInfo = new VGMInfo();
                _CurrentVGMInfo.ResponsibleParty = "CITY OCEAN LOGISTICS CO.,LTD.";
                _CurrentVGMInfo.ResponsiblePerson = LocalData.UserInfo.UserEname;
                _CurrentVGMInfo.VerifiedPerson = LocalData.UserInfo.UserEname;
                //_CurrentVGMInfo.WeightDate = DateTime.Now;
                txtResponsibleParty.Text = _CurrentVGMInfo.ResponsibleParty;
                txtResponsiblePerson.Text = _CurrentVGMInfo.ResponsiblePerson;
                txtVerifiedPerson.Text = _CurrentVGMInfo.VerifiedPerson;
            }
            else
            {
                txtResponsibleParty.Text = _CurrentVGMInfo.ResponsibleParty;
                txtResponsiblePerson.Text = _CurrentVGMInfo.ResponsiblePerson;
                cmbWeightSite.Tag = _CurrentVGMInfo.WeightSite;
                cmbWeightSite.EditValue = _CurrentVGMInfo.WeightSiteCode;
                cmbWeightSite.Text = _CurrentVGMInfo.WeightSiteCode;
                txtWeightSite.Text = _CurrentVGMInfo.WeightSiteName;
                txtVerifiedPerson.Text = _CurrentVGMInfo.VerifiedPerson;
                if (_CurrentVGMInfo.VerifiedDate != null)
                    dateVerifiedDate.DateTime = (DateTime)_CurrentVGMInfo.VerifiedDate;
                if (_CurrentVGMInfo.WeightDate != null)
                    dateWeightDate.DateTime = (DateTime)_CurrentVGMInfo.WeightDate;
            }

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
            set { BindingData(value as OceanMBLInfo); }
        }

        public override bool SaveData()
        {
            return Save();
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

        SaveMBLInfoParameter ConvertMBLToParameter(bool IsSaveAs, OceanMBLInfo info)
        {
            DateTime? dt = _CurrentBLInfo.UpdateDate;
            if (_CurrentBLInfo.UpdateDate.HasValue)
            {
                dt = DateTime.SpecifyKind(_CurrentBLInfo.UpdateDate.Value, DateTimeKind.Unspecified);

            }
            if (!chkHasContract.Checked)
            {
                _CurrentBLInfo.ContractID = Guid.Empty;
            }

            if (info.CargoDescription == null)
            {
                info.CargoDescription = new SpclCargoDescription();
            }

            if (info.CargoDescription.Type <= 0)
            {
                info.CargoDescription = new SpclCargoDescription();
                info.CargoDescription.Type = 0;
                info.CargoDescription.Centigrade = string.Empty;
                info.CargoDescription.CentigradeF = string.Empty;
                info.CargoDescription.DangerousClass = string.Empty;
                info.CargoDescription.DangerousNo = string.Empty;
                info.CargoDescription.DangerousPage = string.Empty;
                info.CargoDescription.DangerousProperty = string.Empty;
            }

            SaveMBLInfoParameter parameter = new SaveMBLInfoParameter
            {
                ID = IsSaveAs ? Guid.Empty : info.ID,
                OceanBookingID = info.OceanBookingID,
                MBLNo = info.No,
                NumberOfOriginal = info.NumberOfOriginal,
                VoyageShowType = info.VoyageShowType,
                CheckerID = info.CheckerID,
                ShipperID = info.ShipperID,
                ShipperDescription = info.ShipperDescription,
                ConsigneeID = info.ConsigneeID,
                ConsigneeDescription = info.ConsigneeDescription,
                NotifyPartyID = string.IsNullOrEmpty(stxtNotifyParty.Text) ? null : info.NotifyPartyID,
                NotifyPartyDescription = info.NotifyPartyDescription,
                AgentID = info.AgentID,
                AgentDescription = info.AgentDescription,
                PlaceOfReceiptID = info.PlaceOfReceiptID,
                PlaceOfReceiptName = info.PlaceOfReceiptName,
                PreVoyageID = info.PreVoyageID,
                VoyageID = info.VoyageID,
                POLID = info.POLID,
                POLName = info.POLName,
                PODID = info.PODID,
                PODName = info.PODName,
                NBPODCode = info.NBPODCode,
                PlaceOfDeliveryID = info.PlaceOfDeliveryID,
                PlaceOfDeliveryName = info.PlaceOfDeliveryName,
                FinalDestinationID = info.FinalDestinationID,
                FinalDestinationName = info.FinalDestinationName,
                TransportClauseID = info.TransportClauseID,
                PaymentTermID = info.PaymentTermID,
                FreightDescription = info.FreightDescription,
                NSITBLNotes  = info.NSITBLNotes,
                ReleaseType = info.ReleaseType,
                ReleaseDate = info.ReleaseDate,
                Quantity = info.Quantity,
                QuantityUnitID = info.QuantityUnitID,
                Weight = info.Weight,
                WeightUnitID = info.WeightUnitID,
                Measurement = info.Measurement,
                MeasurementUnitID = info.MeasurementUnitID,
                Marks = info.Marks,
                GoodsDescription = info.GoodsDescription,
                IsWoodPacking = info.IsWoodPacking,
                CTNQtyInfo = info.CtnQtyInfo,
                IssuePlaceID = info.IssuePlaceID,
                IssueByID = info.IssueByID,
                IssueDate = info.IssueDate,
                WoodPacking = info.WoodPacking,
                IssueType = info.IssueType,
                AgentText = info.AgentText,
                FreightRateID = info.ContractID,
                PreETD = info.PreETD,
                ETD = info.ETD,
                ETA = info.ETA,
                IsCarrierSendAMS = info.IsCarrierSendAMS,
                BookingPartyID = info.BookingPartyID,
                CollectbyAgentOrderID = info.CollectbyAgentOrderID,
                IsThirdPlacePayOrder = info.IsThirdPlacePayOrder,
                ReallyNotifyParty = info.ReallyNotifyParty,
                ReallyConsignee = info.ReallyConsignee,
                ReallyShipper = info.ReallyShipper,
                GateInDate = info.GateInDate,
                HSCODE = info.HSCODE,
                Commodity = info.Commodity,
                Container = info.Container,
                NotifyParty2 = txtNotify2.Text,
                HasFee = checkHasFee.Checked,
                CargoDescription = info.CargoDescription,
                SaveByID = LocalData.UserInfo.LoginID,
                UpdateDate = IsSaveAs ? null : dt,
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
            List<decimal> p_containerVGMCrossWeights = new List<decimal>();
            List<string> p_containerVGMMethods = new List<string>();
            List<string> p_containerCTNOpers = new List<string>();

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
                p_containerVGMCrossWeights.Add(item.VGMCrossWeight);
                p_containerVGMMethods.Add(item.VGMMethod);
                p_containerCTNOpers.Add(item.CTNOper);
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

        #region 查找合约
        private void txtContractNo_Click(object sender, EventArgs e)
        {
            if (!ClientOceanExportService.IsNeedAccept(_CurrentBLInfo.OceanBookingID)) return;
            SelectContract();
        }
        private void AfterSelectContract(object[] parameters)
        {
            FreightList selectedContract = parameters[0] as FreightList;
            if (selectedContract != null)
            {
                _CurrentBLInfo.ContractID = selectedContract.ID;
                txtContractNo.Text = _CurrentBLInfo.ContractNo = selectedContract.FreightNo;
                if (!string.IsNullOrEmpty(selectedContract.ContractName))
                {
                    labContractName.Text = selectedContract.ContractName + Environment.NewLine + selectedContract.ItemCode;
                }

                if (selectedContract.Carrier == "MSC")
                {
                    if (_CurrentBLInfo.FinalDestinationID != null)
                    {
                        LocationInfo FinalDestination = GeographyService.GetLocationInfo((Guid)_CurrentBLInfo.FinalDestinationID);
                        if (FinalDestination.CountryID.ToString().ToUpper() == "37F06C2D-E5F6-4A6F-BB55-9DA3EC3B42A4")
                        {
                            ReloadMscInfo();
                        }
                    }
                    else
                    {
                        if (USAShippingLines.Contains((Guid)_CurrentBLInfo.ShippingLineID))
                        {
                            ReloadMscInfo();
                        }
                    }
                }

                string Qty = string.Empty;
                string[] QtyAr;
                if (!string.IsNullOrEmpty(_CurrentBLInfo.ContainerQtyDescription))
                {
                    QtyAr = _CurrentBLInfo.ContainerQtyDescription.Split(';');

                }
                else
                {
                    QtyAr = null;
                }
                //CurrentBLInfo.ContainerQtyDescription.Substring(_CurrentBLInfo.ContainerQtyDescription.IndexOf("*") + 1);
                if (QtyAr != null)
                {
                    #region 根据箱信息查找合约中的费用信息
                    if (QtyAr.Length == 1)
                    {
                        Qty = QtyAr[0].Substring(QtyAr[0].IndexOf("*") + 1);
                        if (!string.IsNullOrEmpty(Qty))
                        {
                            string Price = GetObjectPropertyValue(selectedContract, "Rate_" + Qty);

                            if (string.IsNullOrEmpty(Price) || Price == "0")
                            {
                                XtraMessageBox.Show(
                                    "合约中没有<\"" + Qty + "\">的订舱、箱型不能自动生成费用信息!" + Environment.NewLine +
                                    "请输入正确的订舱、箱型或重新选择合约！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        foreach (string Q in QtyAr)
                        {
                            Qty = Q.Substring(Q.IndexOf("*") + 1);
                            if (!string.IsNullOrEmpty(Qty))
                            {
                                string Price = GetObjectPropertyValue(selectedContract, "Rate_" + Qty);

                                if (string.IsNullOrEmpty(Price) || Price == "0")
                                {
                                    XtraMessageBox.Show(
                                        "合约中没有<\"" + Qty + "\">的订舱、箱型不能自动生成费用信息!" + Environment.NewLine +
                                        "请输入正确的订舱、箱型或重新选择合约！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    } 
                    #endregion
                }
                else
                {
                    MessageBoxService.ShowWarning("箱信息未录入，选择合约后将不产生费用!");
                }
                
            }

        }

        /// <summary>
        /// MSC 重新刷新信息
        /// </summary>
        private void ReloadMscInfo()
        {

            _CurrentBLInfo.BookingPartyID = new Guid("0751E34D-6FC6-E511-938F-0026551CA878");
            _CurrentBLInfo.ShipperID = new Guid("0751E34D-6FC6-E511-938F-0026551CA878");
            _CurrentBLInfo.ConsigneeID = new Guid("B8006234-2F00-E611-80D5-2047477D7A58");
            _CurrentBLInfo.AgentID = new Guid("B8006234-2F00-E611-80D5-2047477D7A58");
            CustomerInfo shipper = CustomerService.GetCustomerInfo((Guid)_CurrentBLInfo.ShipperID);
            CustomerInfo Consignee = CustomerService.GetCustomerInfo((Guid)_CurrentBLInfo.ConsigneeID);
            CustomerInfo agent = CustomerService.GetCustomerInfo((Guid)_CurrentBLInfo.AgentID);

            mcmbBookingParty.EditText = shipper.CName;
            mcmbBookingParty.EditValue = _CurrentBLInfo.BookingPartyID;

            stxtShipper.Tag = _CurrentBLInfo.ShipperID = shipper.ID;
            stxtShipper.Text = _CurrentBLInfo.ShipperName = shipper.CName;
            ICPCommUIHelper.SetCustomerDesByID(_CurrentBLInfo.ShipperID, _CurrentBLInfo.ShipperDescription);

            stxtConsignee.Tag = _CurrentBLInfo.ConsigneeID = Consignee.ID;
            stxtConsignee.Text = _CurrentBLInfo.ConsigneeName = Consignee.CName;
            ICPCommUIHelper.SetCustomerDesByID(_CurrentBLInfo.ConsigneeID, _CurrentBLInfo.ConsigneeDescription);

            stxtAgent.Tag = _CurrentBLInfo.AgentID = agent.ID;
            stxtAgent.Text = _CurrentBLInfo.AgentName = agent.CName;
            //ICPCommUIHelper.SetCustomerDesByID(_CurrentBLInfo.AgentID, _CurrentBLInfo.AgentDescription);
            //stxtAgent_EditValueChanged(null, null);
            txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);

            txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);
        }

        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }

        /// <summary>
        /// 查找合约
        /// </summary>
        private void SelectContract()
        {
            _CurrentBLInfo = bindingSource1.DataSource as OceanMBLInfo;
            ClientOceanExportService.SelectContract(_CurrentBLInfo, AfterSelectContract);
        }

        private void txtContractNo_ButtonClick(object sender, ButtonPressedEventArgs e)
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
            _CurrentBLInfo.IsDirty = true;
        }

        #region EDI
        /// <summary>
        /// 倒角转半角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32; continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// EDI格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEDIStyle_ItemClick(object sender, ItemClickEventArgs e)
        {
            Select(true, true);

            #region Customer

            if (txtShipperDescription.Lines != null && txtShipperDescription.Lines.Length > 0)
            {
                string strTemp = ToDBC(txtShipperDescription.Text);
                _CurrentBLInfo.ShipperDescription = parseXmlForEDI(_CurrentBLInfo.ShipperDescription);
                txtShipperDescription.Text = OEUtility.BuindDescriptionFromXml(_CurrentBLInfo.ShipperDescription, true, false);

                if (strTemp.Trim() != txtShipperDescription.Text.Trim())
                {
                    txtShipperDescription.ForeColor = Color.Red;
                    _CurrentBLInfo.IsDirty = true;
                }
            }
            if (txtConsigneeDescription.Lines != null && txtConsigneeDescription.Lines.Length > 0)
            {
                string strTemp = ToDBC(txtConsigneeDescription.Text);
                _CurrentBLInfo.ConsigneeDescription = parseXmlForEDI(_CurrentBLInfo.ConsigneeDescription);
                txtConsigneeDescription.Text = OEUtility.BuindDescriptionFromXml(_CurrentBLInfo.ConsigneeDescription, true, false);

                if (strTemp.Trim() != txtConsigneeDescription.Text.Trim())
                {
                    txtConsigneeDescription.ForeColor = Color.Red;
                    _CurrentBLInfo.IsDirty = true;
                }
            }

            if (txtNotifyPartyDescription.Lines != null && txtNotifyPartyDescription.Lines.Length > 0)
            {
                string strTemp = ToDBC(txtNotifyPartyDescription.Text);
                _CurrentBLInfo.NotifyPartyDescription = parseXmlForEDI(_CurrentBLInfo.NotifyPartyDescription);
                txtNotifyPartyDescription.Text = OEUtility.BuindDescriptionFromXml(_CurrentBLInfo.NotifyPartyDescription, true, false);
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
            if (_CurrentBLInfo.CarrierName.Contains("中海") || _CurrentBLInfo.CarrierName.ToUpper().Contains("CHINA SHIPPING")
                || _CurrentBLInfo.CarrierName.Contains("中远") || _CurrentBLInfo.CarrierName.ToUpper().Contains("COSCO")
                )
            {
                if (txtMarks.Lines != null && txtMarks.Lines.Length > 0)
                {
                    string strTemp = EDIClientService.SplitString(ToDBC(txtMarks.Text), 18, 0);
                    if (txtMarks.Text != strTemp)
                    {
                        txtMarks.ForeColor = Color.Red;
                        _CurrentBLInfo.Marks = txtMarks.Text = strTemp;
                        _CurrentBLInfo.IsDirty = true;
                    }

                }

                if (txtGoodsDescription.Lines != null && txtGoodsDescription.Lines.Length > 0)
                {
                    string strTemp = EDIClientService.SplitString(ToDBC(txtGoodsDescription.Text), 30, 0);
                    if (txtGoodsDescription.Text != strTemp)
                    {
                        txtGoodsDescription.ForeColor = Color.Red;
                        _CurrentBLInfo.GoodsDescription = txtGoodsDescription.Text = strTemp;
                        _CurrentBLInfo.IsDirty = true;
                    }

                }

                if (txtFreightDescription.Lines != null && txtFreightDescription.Lines.Length > 0)
                {
                    string strTemp = EDIClientService.SplitString(ToDBC(txtFreightDescription.Text), 30, 0);
                    if (txtFreightDescription.Text != strTemp)
                    {
                        txtFreightDescription.ForeColor = Color.Red;
                        _CurrentBLInfo.FreightDescription = txtFreightDescription.Text = strTemp;
                        _CurrentBLInfo.IsDirty = true;
                    }

                }
            }
            #endregion

            #region 韩进
            if (_CurrentBLInfo.CarrierName.Contains("韩进") || _CurrentBLInfo.CarrierName.ToUpper().Contains("HANJIN"))
            {
                if (txtMarks.Lines != null && txtMarks.Lines.Length > 0)
                {
                    string strTemp = EDIClientService.SplitString(ToDBC(txtMarks.Text), 18, 0);
                    if (txtMarks.Text != strTemp)
                    {
                        txtMarks.ForeColor = Color.Red;
                        _CurrentBLInfo.Marks = txtMarks.Text = strTemp;
                        _CurrentBLInfo.IsDirty = true;
                    }

                }

                if (txtGoodsDescription.Lines != null && txtGoodsDescription.Lines.Length > 0)
                {
                    string strTemp = EDIClientService.SplitString(ToDBC(txtGoodsDescription.Text), 50, 0);
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

        #region 生成 EDI格式
        /// <summary>
        /// 生成 EDI格式
        /// </summary>
        /// <param name="paramCustomerDescription"></param>
        /// <returns></returns>
        private CustomerDescriptionForNew parseXmlForEDI(CustomerDescriptionForNew paramCustomerDescription)
        {

            if (paramCustomerDescription == null)
            {
                paramCustomerDescription = new CustomerDescriptionForNew();
            }
            paramCustomerDescription.Address = ToDBC(paramCustomerDescription.Address);
            paramCustomerDescription.City = ToDBC(paramCustomerDescription.City);
            paramCustomerDescription.Contact = ToDBC(paramCustomerDescription.Contact);
            paramCustomerDescription.Country = ToDBC(paramCustomerDescription.Country);
            paramCustomerDescription.Fax = ToDBC(paramCustomerDescription.Fax);
            paramCustomerDescription.Name = ToDBC(paramCustomerDescription.Name);
            paramCustomerDescription.Remark = ToDBC(paramCustomerDescription.Remark);
            paramCustomerDescription.Tel = ToDBC(paramCustomerDescription.Tel);

            paramCustomerDescription.Name = EDIClientService.SplitString(paramCustomerDescription.Name, 35, 0);

            string strTelAndFax = string.Empty;

            strTelAndFax = paramCustomerDescription.Tel == null ? string.Empty : "Tel:" + paramCustomerDescription.Tel;
            if (strTelAndFax.Length != 0) strTelAndFax += " ";
            strTelAndFax += paramCustomerDescription.Fax == null ? string.Empty : "Fax:" + paramCustomerDescription.Fax;
            strTelAndFax = cutxml(strTelAndFax, 35);

            if (strTelAndFax.Contains("Tel:")) strTelAndFax = strTelAndFax.Replace("Tel:", "");

            int index = strTelAndFax.IndexOf("Fax", StringComparison.Ordinal);

            if (index > 0)
            {
                paramCustomerDescription.Tel = strTelAndFax.Substring(0, index);
                paramCustomerDescription.Fax = strTelAndFax.Substring(index, strTelAndFax.Length - index).Replace("Fax:", "").Replace("\r\n", "");
            }
            else
            {
                paramCustomerDescription.Fax = strTelAndFax.Replace("Fax:", "");
            }

            paramCustomerDescription.Address = EDIClientService.SplitString(paramCustomerDescription.Address, 35, 0);

            int addressLastLineLength = paramCustomerDescription.Address.Trim().LastIndexOf("\r\n", StringComparison.Ordinal);
            string addressString = string.Empty;//Address最后一行,不包括换行符
            addressString = addressLastLineLength > 0
                ? paramCustomerDescription.Address.Substring(addressLastLineLength, paramCustomerDescription.Address.Length - addressLastLineLength).Replace("\r\n", "")
                : paramCustomerDescription.Address.Trim();

            if (!string.IsNullOrEmpty(paramCustomerDescription.City.Trim()))
            {
                string cityStateZipStr = addressString + " " + paramCustomerDescription.City;
            }

            paramCustomerDescription.Remark = EDIClientService.SplitString(paramCustomerDescription.Remark, 35, 0);

            return paramCustomerDescription;
        }

        /// <summary>
        /// 生成 EDI格式
        /// </summary>
        /// <param name="paramCustomerDescription"></param>
        /// <returns></returns>
        private CustomerDescription parseXmlForEDI(CustomerDescription paramCustomerDescription)
        {

            if (paramCustomerDescription == null)
            {
                paramCustomerDescription = new CustomerDescription();
            }
            paramCustomerDescription.Address = ToDBC(paramCustomerDescription.Address);
            paramCustomerDescription.City = ToDBC(paramCustomerDescription.City);
            paramCustomerDescription.Contact = ToDBC(paramCustomerDescription.Contact);
            paramCustomerDescription.Country = ToDBC(paramCustomerDescription.Country);
            paramCustomerDescription.Fax = ToDBC(paramCustomerDescription.Fax);
            paramCustomerDescription.Name = ToDBC(paramCustomerDescription.Name);
            paramCustomerDescription.Remark = ToDBC(paramCustomerDescription.Remark);
            paramCustomerDescription.Tel = ToDBC(paramCustomerDescription.Tel);

            paramCustomerDescription.Name = EDIClientService.SplitString(paramCustomerDescription.Name, 35, 0);

            string strTelAndFax = string.Empty;

            strTelAndFax = paramCustomerDescription.Tel == null ? string.Empty : "Tel:" + paramCustomerDescription.Tel;
            if (strTelAndFax.Length != 0) strTelAndFax += " ";
            strTelAndFax += paramCustomerDescription.Fax == null ? string.Empty : "Fax:" + paramCustomerDescription.Fax;
            strTelAndFax = cutxml(strTelAndFax, 35);

            if (strTelAndFax.Contains("Tel:")) strTelAndFax = strTelAndFax.Replace("Tel:", "");

            int index = strTelAndFax.IndexOf("Fax", StringComparison.Ordinal);

            if (index > 0)
            {
                paramCustomerDescription.Tel = strTelAndFax.Substring(0, index);
                paramCustomerDescription.Fax = strTelAndFax.Substring(index, strTelAndFax.Length - index).Replace("Fax:", "").Replace("\r\n", "");
            }
            else
            {
                paramCustomerDescription.Fax = strTelAndFax.Replace("Fax:", "");
            }

            paramCustomerDescription.Address = EDIClientService.SplitString(paramCustomerDescription.Address, 35, 0);

            int addressLastLineLength = paramCustomerDescription.Address.Trim().LastIndexOf("\r\n", StringComparison.Ordinal);
            string addressString = string.Empty;//Address最后一行,不包括换行符
            addressString = addressLastLineLength > 0
                ? paramCustomerDescription.Address.Substring(addressLastLineLength, paramCustomerDescription.Address.Length - addressLastLineLength).Replace("\r\n", "")
                : paramCustomerDescription.Address.Trim();

            if (!string.IsNullOrEmpty(paramCustomerDescription.City.Trim()))
            {
                string cityStateZipStr = addressString + " " + paramCustomerDescription.City;
            }

            paramCustomerDescription.Remark = EDIClientService.SplitString(paramCustomerDescription.Remark, 35, 0);

            return paramCustomerDescription;
        } 
        #endregion

        string cutxml(XmlNode xn, int length)
        {
            if (xn != null && xn.InnerText.Trim().Length > 0)
            {
                string strTemp = EDIService.Instance.ToDBC(xn.InnerText.Trim());
                xn.InnerText = CutStringByLength(strTemp, length);
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
                str = CutStringByLength(strTemp, length);
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
        private void barE_MBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!Save())
            {
                return;
            }

            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID))
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select current BL." : "请选择当前要发送EDI补料的提单.");
                return;
            }


            List<OceanBLList> mbls = new List<OceanBLList>();

            mbls.Add(_CurrentBLInfo);

            #region Comment Code
            //OceanBLList hjMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"); });
            //OceanBLList zhMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"); });
            //OceanBLList fyMBL = mbls.Find(delegate(OceanBLList item) { return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"); });
            //if (hjMBL == null && zhMBL == null && fyMBL == null)
            //{
            //    MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Shipowners are now only supports China Shipping , Hanjin,Hainan Pan Ocean Shipping Electronic batch." : "现在只支持船东是 [韩进]、[中海]、[海南泛洋] 的电子补料。");

            //    return;
            //}

            //string subjuect = string.Empty;
            //string toEmail = string.Empty;
            //StringBuilder mblNoBuilder = new StringBuilder();
            //List<string> operationNos = new List<string>();
            //List<Guid> mblIds = new List<Guid>();
            //List<Guid> oIds = new List<Guid>();
            //string key = string.Empty;
            //string tip = string.Empty;

            //#region 韩进
            //if (hjMBL != null)
            //{
            //    List<OceanBLList> hjMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("0F40E9A1-B388-44CC-B27F-7A9AEC6F6D58"); });
            //    foreach (var item in hjMBLs)
            //    {
            //        if (mblNoBuilder.Length > 0)
            //            mblNoBuilder.Append(",");

            //        mblNoBuilder.Append(item.SONO);

            //        operationNos.Add(item.No);

            //        mblIds.Add(item.MBLID);
            //        oIds.Add(item.ID);
            //    }
            //    subjuect = LocalData.IsEnglish ? "HANJIN SHIPPING(" + mblNoBuilder.ToString() + ")" : "韩进电子补料(" + mblNoBuilder.ToString() + ")";
            //    key = "HANJIN_SI";
            //    tip = LocalData.IsEnglish ? "HANJIN SHIPPING" : "韩进";
            //}
            //#endregion

            //#region 中海
            //else if (zhMBL != null)
            //{
            //    List<OceanBLList> zjMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("69B85E12-6208-432C-8D8E-D2E345239047"); });
            //    foreach (var item in zjMBLs)
            //    {
            //        if (mblNoBuilder.Length > 0)
            //            mblNoBuilder.Append(",");

            //        mblNoBuilder.Append(item.SONO);

            //        operationNos.Add(item.No);
            //        mblIds.Add(item.MBLID);
            //        oIds.Add(item.ID);
            //    }
            //    subjuect = LocalData.IsEnglish ? "CHINA SHIPPING(" + mblNoBuilder.ToString() + ")" : "中海电子补料(" + mblNoBuilder.ToString() + ")";
            //    key = "CSCL_SI";
            //    tip = LocalData.IsEnglish ? "CHINA SHIPPING" : "中海";
            //}
            //#endregion

            //#region 泛洋
            //else if (fyMBL != null)
            //{
            //    List<OceanBLList> fyMBLs = mbls.FindAll(delegate(OceanBLList item) { return item.CarrierID == new Guid("4968597D-7BFC-405C-AF9B-C521AB082C0B"); });
            //    foreach (var item in fyMBLs)
            //    {
            //        if (mblNoBuilder.Length > 0)
            //            mblNoBuilder.Append(",");

            //        mblNoBuilder.Append(item.SONO);

            //        operationNos.Add(item.No);
            //        mblIds.Add(item.MBLID);
            //        oIds.Add(item.ID);
            //    }
            //    subjuect = LocalData.IsEnglish ? "HAINAN PAN SHIPPING(" + mblNoBuilder.ToString() + ")" : "海南泛洋电子补料(" + mblNoBuilder.ToString() + ")";
            //    key = "FYCW_SI";
            //    tip = LocalData.IsEnglish ? "HAINAN PAN SHIPPING" : "海南泛洋";
            //}
            //#endregion

            //EDISendOption sendItem = new EDISendOption();
            //sendItem.ServiceKey = key;
            //sendItem.EdiMode = EDIMode.SI;
            //sendItem.CompanyID = _CurrentBLInfo.CompanyID;
            //sendItem.Subject = subjuect;
            //sendItem.IDs = oIds.ToArray();
            //sendItem.FIDs = mblIds.ToArray();
            //sendItem.NOs = operationNos.ToArray();
            //sendItem.OperationType = OperationType.OceanExport; 
            #endregion
            try
            {
                bool isSucc = false;
                //if (mblIds.Count > 0)
                //{
                //    isSucc = EDIClientService.SendEDI(sendItem);
                //}
                OECommonUtility.InnerEMBL(EDIClientService, mbls, _CurrentBLInfo.CompanyID, AMSEntryType.Unknown, ACIEntryType.Unknown, ref isSucc);
                if (isSucc)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                }

            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
            }
        }


        private BusinessOperationContext GetContext(OceanMBLInfo orderInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = orderInfo.OceanBookingID;
            context.OperationType = OperationType.OceanExport;
            context.OperationNO = orderInfo.RefNo;
            context.FormId = orderInfo.OceanBookingID;
            context.FormType = FormType.Booking;
            return context;
        }
        #endregion

        private void barchs_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.MailCustomerAskForConfirmSI(false, _CurrentBLInfo.OceanBookingID, null, _CurrentBLInfo);
        }

        private void bareng_ItemClick(object sender, ItemClickEventArgs e)
        {
            ClientOceanExportService.MailCustomerAskForConfirmSI(true, _CurrentBLInfo.OceanBookingID, null, _CurrentBLInfo);
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

        private void IsManualControl_CheckedChanged(object sender, EventArgs e)
        {
            numMeasurement.Properties.ReadOnly = numWeight.Properties.ReadOnly = numQuantity.Properties.ReadOnly = !IsManualControl.Checked;
        }

        private void txtFreightDescription_Leave(object sender, EventArgs e)
        {
            if (_CurrentBLInfo.CarrierName.Contains("中远") || _CurrentBLInfo.CarrierName.ToUpper().Contains("COSCO"))
            {
                List<string> newText = new List<string>();
                if (!string.IsNullOrEmpty(txtFreightDescription.Text))
                {
                    string strTemp = EDIClientService.SplitString(ToDBC(txtFreightDescription.Text), 30, 0);

                    if (txtFreightDescription.Text != strTemp)
                    {
                        //txtFreightDescription.ForeColor = Color.Red;
                        txtFreightDescription.Text = strTemp;
                    }
                }
            }
        }

        //private void txtCommdity_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtCommdity.Text))
        //    {
        //        string Commdity = ToDBC(txtCommdity.Text);
        //        string[] strArr = Commdity.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        //        Regex rx = new Regex(@"[0-9a-zA-Z_&#@/ -]");
        //        string[] newArr = new string[strArr.Length];
        //        string str = string.Empty;
        //        bool flag = false;
        //        if (strArr != null && strArr.Length > 0)
        //        {
        //            for (int i = 0; i < strArr.Length; i++)
        //            {
        //                str = string.Empty;
        //                foreach (char s in strArr[i])
        //                {
        //                    if (rx.IsMatch(s.ToString()))
        //                    {
        //                        str += s;
        //                    }
        //                    else
        //                        flag = true;
        //                }
        //                newArr[i] = str;
        //            }
        //        }

        //        if (flag)
        //        {
        //            str = string.Empty;
        //            for (int i = 0; i < newArr.Length; i++)
        //            {
        //                if (i < newArr.Length - 1)
        //                    str += newArr[i] + Environment.NewLine;
        //                else
        //                    str += newArr[i];
        //            }
        //            txtCommdity.Text = str;
        //            XtraMessageBox.Show("品名特殊符号已自动删除，请检查是否有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.None);
        //        }
        //        else
        //            txtCommdity.Text = Commdity;
        //    }
        //}

        /// <summary>
        /// 电子订舱ANL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barBooking_ItemClick(object sender, ItemClickEventArgs e)
        {
            List<Guid> mblIds = new List<Guid>();
            List<Guid> ids = new List<Guid>(1);
            mblIds.Add(_CurrentBLInfo.ID);
            ids.Add(_CurrentBLInfo.OceanBookingID);
            List<string> nos = new List<string>(1);
            nos.Add(_CurrentBLInfo.No);

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = "NBEDIBookingANL";
            sendItem.EdiMode = EDIMode.Booking;
            sendItem.CompanyID = _CurrentBLInfo.CompanyID;
            sendItem.Subject = "电子订舱(";
            sendItem.Subject += _CurrentBLInfo.No;
            sendItem.Subject += ")";
            sendItem.IDs = mblIds.ToArray();
            sendItem.FIDs = ids.ToArray();
            sendItem.NOs = nos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;

            bool isSucc = EDIClientService.SendEDI(sendItem);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
            }
        }

        /// <summary>
        /// 电子预配ANL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barPreplan_ItemClick(object sender, ItemClickEventArgs e)
        {
            string subjuect = string.Empty;
            string toEmail = string.Empty;
            System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
            List<string> operationNos = new List<string>();
            List<Guid> mblIds = new List<Guid>();
            List<Guid> oIds = new List<Guid>();
            string key = string.Empty;
            string tip = string.Empty;

            mblNoBuilder.Append(_CurrentBLInfo.No);

            operationNos.Add(_CurrentBLInfo.No);

            mblIds.Add(_CurrentBLInfo.MBLID);
            oIds.Add(_CurrentBLInfo.OceanBookingID);

            subjuect = LocalData.IsEnglish ? "NingBoEDIPre(" + mblNoBuilder.ToString() + ")" : "宁波澳航预配(" + mblNoBuilder.ToString() + ")";
            key = "NBEDIBookingANL";
            tip = LocalData.IsEnglish ? "NBEDIForANL" : "宁波澳航预配";

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = _CurrentBLInfo.CompanyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            bool isSucc = EDIClientService.SendEDI(sendItem);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
            }
        }

        /// <summary>
        /// 电子补料ANL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barSupplement_ItemClick(object sender, ItemClickEventArgs e)
        {
            _ctnList = OceanExportService.GetOceanMBLContainerList(_CurrentBLInfo.ID);
            if (_CurrentBLInfo.Quantity != _ctnList.Sum(r => r.Quantity) || _CurrentBLInfo.Weight != _ctnList.Sum(r => r.Weight) || _CurrentBLInfo.Measurement != _ctnList.Sum(r => r.Measurement))
            {
                DialogResult result = XtraMessageBox.Show("MBL的毛件体跟箱信息中的毛件体不一致，是否继续发送EDI？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }


            string subjuect = string.Empty;
            string toEmail = string.Empty;
            System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
            List<string> operationNos = new List<string>();
            List<Guid> mblIds = new List<Guid>();
            List<Guid> oIds = new List<Guid>();
            string key = string.Empty;
            string tip = string.Empty;

            mblNoBuilder.Append(_CurrentBLInfo.No);

            operationNos.Add(_CurrentBLInfo.No);

            mblIds.Add(_CurrentBLInfo.MBLID);
            oIds.Add(_CurrentBLInfo.OceanBookingID);

            subjuect = LocalData.IsEnglish ? "NingBoEDIANL(" + mblNoBuilder.ToString() + ")" : "宁波EDI澳航(" + mblNoBuilder.ToString() + ")";
            key = "NBEDISIANL";
            tip = LocalData.IsEnglish ? "NBEDIForANL" : "宁波EDI澳航";

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = _CurrentBLInfo.CompanyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            bool isSucc = EDIClientService.SendEDI(sendItem);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
            }
        }

        /// <summary>
        /// 电子码头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barWharf_ItemClick(object sender, ItemClickEventArgs e)
        {
            string subjuect = string.Empty;
            string toEmail = string.Empty;
            System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
            List<string> operationNos = new List<string>();
            List<Guid> mblIds = new List<Guid>();
            List<Guid> oIds = new List<Guid>();
            string key = string.Empty;
            string tip = string.Empty;

            mblNoBuilder.Append(_CurrentBLInfo.No);

            operationNos.Add(_CurrentBLInfo.No);

            mblIds.Add(_CurrentBLInfo.MBLID);
            oIds.Add(_CurrentBLInfo.OceanBookingID);

            subjuect = LocalData.IsEnglish ? "NingBoEDI(" + mblNoBuilder.ToString() + ")" : "宁波EDI中心(" + mblNoBuilder.ToString() + ")";
            key = "NBEDICenter";
            tip = LocalData.IsEnglish ? "NBEDI" : "宁波EDI中心";

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = _CurrentBLInfo.CompanyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            bool isSucc = EDIClientService.SendEDI(sendItem);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
            }
        }

        private void chkSpecial_CheckedChanged(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (chkSpecial.Checked)
                {
                    TypeOfGood typeGood = this.Workitem.Items.AddNew<TypeOfGood>();
                    string title = LocalData.IsEnglish ? "Special Goods Seting" : "设置特殊货物";
                    if (_CurrentBLInfo.CargoDescription != null)
                    {
                        typeGood.goodType = _CurrentBLInfo.CargoDescription.Type - 1;
                        typeGood.Centigrade = _CurrentBLInfo.CargoDescription.Centigrade;
                        typeGood.CentigradeF = _CurrentBLInfo.CargoDescription.CentigradeF;
                        typeGood.DangerousClass = _CurrentBLInfo.CargoDescription.DangerousClass;
                        typeGood.DangerousProperty = _CurrentBLInfo.CargoDescription.DangerousProperty;
                        typeGood.DangerousPage = _CurrentBLInfo.CargoDescription.DangerousPage;
                        typeGood.DangerousNo = _CurrentBLInfo.CargoDescription.DangerousNo;
                    }
                    DialogResult result = PartLoader.ShowDialog(typeGood, title);
                    if (result == DialogResult.OK)
                    {
                        if (_CurrentBLInfo.CargoDescription == null)
                            _CurrentBLInfo.CargoDescription = new SpclCargoDescription();

                        _CurrentBLInfo.CargoDescription.Type = typeGood.goodType;
                        _CurrentBLInfo.CargoDescription.Centigrade = typeGood.Centigrade;
                        _CurrentBLInfo.CargoDescription.CentigradeF = typeGood.CentigradeF;
                        _CurrentBLInfo.CargoDescription.DangerousClass = typeGood.DangerousClass;
                        _CurrentBLInfo.CargoDescription.DangerousProperty = typeGood.DangerousProperty;
                        _CurrentBLInfo.CargoDescription.DangerousPage = typeGood.DangerousPage;
                        _CurrentBLInfo.CargoDescription.DangerousNo = typeGood.DangerousNo;

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Special Goods Seting Success." : "设置特殊货物属性成功.");
                    }
                    else
                        chkSpecial.Checked = false;
                }
            }
        }

        private void barAddHbl_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (_CurrentBLInfo.HBLCount > 0 || !string.IsNullOrEmpty(_CurrentBLInfo.HBLNos))
            //{
            //    XtraMessageBox.Show("MBL已有HBL不能批量增加！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            AddHblNum numForm = new AddHblNum();
            string title = LocalData.IsEnglish ? "Set Num" : "设置数量";
            int num = 1;
            PartLoader.ShowDialog(numForm, title);
            num = numForm.num;

            try
            {
                for (int i = 1; i <= num; i++)
                {
                    int ASC = 64;
                    char ASCII = (char)(ASC + i);


                    SaveDeclareHBLInfoParameter hbl = new SaveDeclareHBLInfoParameter();
                    hbl.id = Guid.Empty;
                    hbl.oceanBookingID = _CurrentBLInfo.OceanBookingID;
                    hbl.hblNo = _CurrentBLInfo.No + ASCII;
                    hbl.mblID = _CurrentBLInfo.MBLID;
                    hbl.mblNO = _CurrentBLInfo.No;
                    //hbl.numberOfOriginal = 3;
                    //hbl.voyageShowType = _CurrentBLInfo.VoyageShowType;
                    //hbl.checkerID = _CurrentBLInfo.CheckerID;
                    //hbl.shipperID = _CurrentBLInfo.ShipperID;
                    //hbl.shipperDescription = _CurrentBLInfo.ShipperDescription;
                    //hbl.shipperID = _CurrentBLInfo.ShipperID;
                    //hbl.consigneeDescription = _CurrentBLInfo.ConsigneeDescription;
                    //hbl.consigneeID = _CurrentBLInfo.ConsigneeID;
                    //hbl.notifyPartyDescription = _CurrentBLInfo.NotifyPartyDescription;
                    //hbl.notifyPartyID = _CurrentBLInfo.NotifyPartyID;
                    //hbl.agentID = _CurrentBLInfo.AgentID;
                    //hbl.agentDescription = _CurrentBLInfo.AgentDescription;
                    //hbl.placeOfReceiptID = _CurrentBLInfo.PlaceOfReceiptID;
                    //hbl.placeOfReceiptName = _CurrentBLInfo.PlaceOfReceiptName;
                    //hbl.preVoyageID = _CurrentBLInfo.PreVoyageID;
                    //hbl.voyageID = _CurrentBLInfo.VoyageID;
                    //hbl.polID = _CurrentBLInfo.POLID;
                    //hbl.polName = _CurrentBLInfo.POLName;
                    //hbl.podID = _CurrentBLInfo.PODID;
                    //hbl.podName = _CurrentBLInfo.PODName;
                    //hbl.placeOfDeliveryID = _CurrentBLInfo.PlaceOfDeliveryID;
                    //hbl.placeOfDeliveryName = _CurrentBLInfo.PlaceOfDeliveryName;
                    //hbl.finalDestinationID = _CurrentBLInfo.FinalDestinationID;
                    //hbl.finalDestinationName = _CurrentBLInfo.FinalDestinationName;
                    //hbl.transportClauseID = _CurrentBLInfo.TransportClauseID;
                    //hbl.paymentTermID = _CurrentBLInfo.PaymentTermID;
                    //hbl.freightDescription = _CurrentBLInfo.FreightDescription;
                    //hbl.releaseType = _CurrentBLInfo.ReleaseType;
                    hbl.marks = _CurrentBLInfo.Marks;
                    hbl.goodsDescription = _CurrentBLInfo.GoodsDescription;
                    hbl.isWoodPacking = _CurrentBLInfo.IsWoodPacking;
                    hbl.woodPacking = _CurrentBLInfo.WoodPacking;
                    //hbl.bLTitleID = _CurrentBLInfo.BLTitleID;
                    hbl.saveByID = LocalData.UserInfo.LoginID;
                    //hbl.ETA = _CurrentBLInfo.ETA;
                    //hbl.ETD = _CurrentBLInfo.ETD;
                    //hbl.PreETD = _CurrentBLInfo.PreETD;
                    //hbl.GateInDate = _CurrentBLInfo.GateInDate;
                    //hbl.issueType = _CurrentBLInfo.IssueType;
                    //hbl.issueByID = _CurrentBLInfo.IssueByID;
                    //hbl.issuePlaceID = _CurrentBLInfo.IssuePlaceID;
                    hbl.quantityUnitID = _CurrentBLInfo.QuantityUnitID;
                    hbl.weightUnitID = _CurrentBLInfo.WeightUnitID;
                    hbl.measurementUnitID = _CurrentBLInfo.MeasurementUnitID;
                    SingleResult result = OceanExportService.SaveDeclareHBLInfo(hbl, false);
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), "批量生成报关单成功！");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), string.Format("批量生成报关单失败,{0}", ex.Message));
            }
        }

        private void barGetCommodity_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrEmpty(_CurrentBLInfo.HBLNos))
            {
                XtraMessageBox.Show("MBL没有关联的HBL！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            char[] arr = { ',' };
            string[] srr = { Environment.NewLine };
            string[] hblno = _CurrentBLInfo.HBLNos.Split(arr, StringSplitOptions.RemoveEmptyEntries);
            List<string> souers = new List<string>();
            string goodDes = string.Empty;
            DeclareHBLInfo info;
            for (int i = 0; i < hblno.Length; i++)
            {
                info = OceanExportService.GetDeclareHBLInfo(new Guid(hblno[i]));
                string[] goodarr = info.GoodsDescription.Split(srr, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < goodarr.Length; j++)
                {
                    if (goodDes == string.Empty)
                    {
                        goodDes += goodarr[j];
                        souers.Add(goodarr[j]);
                    }
                    else
                    {
                        if (souers.Count(r => r == goodarr[j]) == 0)
                        {
                            goodDes += Environment.NewLine + goodarr[j];
                            souers.Add(goodarr[j]);
                        }

                    }
                }
            }
            if (string.IsNullOrEmpty(_CurrentBLInfo.GoodsDescription))
            {
                _CurrentBLInfo.GoodsDescription = goodDes;
                txtGoodsDescription.Text = goodDes;
            }
            else
            {
                DialogResult result = XtraMessageBox.Show("当前MBL已含有货描是否覆盖? \nYES覆盖 NO追加 Cancel放弃", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _CurrentBLInfo.GoodsDescription = goodDes;
                    txtGoodsDescription.Text = goodDes;
                }
                else if (result == DialogResult.No)
                {
                    _CurrentBLInfo.GoodsDescription += Environment.NewLine + goodDes;
                    txtGoodsDescription.Text = _CurrentBLInfo.GoodsDescription;
                }
            }
        }

        private void barVGM_ItemClick(object sender, ItemClickEventArgs e)
        {
            string subjuect = string.Empty;
            string toEmail = string.Empty;
            System.Text.StringBuilder mblNoBuilder = new System.Text.StringBuilder();
            List<string> operationNos = new List<string>();
            List<Guid> mblIds = new List<Guid>();
            List<Guid> oIds = new List<Guid>();
            string key = string.Empty;
            string tip = string.Empty;

            mblNoBuilder.Append(_CurrentBLInfo.No);

            operationNos.Add(_CurrentBLInfo.No);

            mblIds.Add(_CurrentBLInfo.MBLID);
            oIds.Add(_CurrentBLInfo.OceanBookingID);

            subjuect = LocalData.IsEnglish ? "NingBoEDIVGM(" + mblNoBuilder.ToString() + ")" : "宁波EDI中心VGM(" + mblNoBuilder.ToString() + ")";
            key = "NBEDIVGMANL";
            tip = LocalData.IsEnglish ? "NBEDIVGM" : "宁波EDI中心VGM";

            EDISendOption sendItem = new EDISendOption();
            sendItem.ServiceKey = key;
            sendItem.EdiMode = EDIMode.SI;
            sendItem.CompanyID = _CurrentBLInfo.CompanyID;
            sendItem.Subject = subjuect;
            sendItem.IDs = oIds.ToArray();
            sendItem.FIDs = mblIds.ToArray();
            sendItem.NOs = operationNos.ToArray();
            sendItem.OperationType = OperationType.OceanExport;
            sendItem.SendByID = LocalData.UserInfo.LoginID;

            bool isSucc = EDIClientService.SendEDI(sendItem);
            if (isSucc)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
            }
        }

        private void barSaveVGM_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveVGMInfoParameter Parameter = new SaveVGMInfoParameter();
            Parameter.mblID = _CurrentBLInfo.MBLID;
            Parameter.ResponsibleParty = txtResponsibleParty.Text == txtResponsibleParty.Properties.NullText ? null : txtResponsibleParty.Text.Trim();
            Parameter.ResponsiblePerson = txtResponsiblePerson.Text == txtResponsiblePerson.Properties.NullText ? null : txtResponsiblePerson.Text.Trim();
            Parameter.WeightSite = cmbWeightSite.Tag == null ? Guid.Empty : (Guid)cmbWeightSite.Tag;
            if (dateVerifiedDate.DateTime.Year < 1900)
            {
                Parameter.VerifiedDate = null;
            }
            else
            {
                Parameter.VerifiedDate = dateVerifiedDate.DateTime;
            }
            if (dateWeightDate.DateTime.Year < 1900)
            {
                Parameter.WeightDate = null;
            }
            else
            {
                Parameter.WeightDate = dateWeightDate.DateTime;
            }
            Parameter.VerifiedPerson = txtVerifiedPerson.Text == txtVerifiedPerson.Properties.NullText ? null : txtVerifiedPerson.Text.Trim();
            Parameter.saveByID = LocalData.UserInfo.LoginID;
            Parameter.updateDate = _CurrentVGMInfo.UpdateDate;
            Parameter.id = _CurrentVGMInfo.ID;

            try
            {
                SingleResult single = OceanExportService.SaveVGMInfo(Parameter);
                //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                _CurrentVGMInfo.ID = single.GetValue<Guid>("ID");
                _CurrentVGMInfo.UpdateDate = single.GetValue<DateTime?>("UpdateDate");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), string.Format("保存VGM失败,{0}", ex.Message));
            }

        }

        private void barPrintLoadContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.IsDirty)
            {
                if (SaveData() == false) return;
            }
            ClientOceanExportService.PrintLoadContainers(_CurrentBLInfo,false);
        }

        private void barPrintLoadContainerCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (_CurrentBLInfo.ID == Guid.Empty || _CurrentBLInfo.IsDirty)
            {
                if (SaveData() == false) return;
            }
            ClientOceanExportService.PrintLoadContainers(_CurrentBLInfo, true);
        }

        private void bntDeclaration_ItemClick(object sender, ItemClickEventArgs e)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values["OperationID"] = _CurrentBLInfo.OceanBookingID;
            values["MBLID"] = _CurrentBLInfo.ID;
            values["FCMBLType"] = FCMBLType.DeclareHBL;
            ClientOceanExportService.DeclarationContainer(_CurrentBLInfo.No, values, null);
        }

        private void bntImportDeclaration_ItemClick(object sender, ItemClickEventArgs e)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values["OperationID"] = _CurrentBLInfo.OceanBookingID;
            values["MBLID"] = _CurrentBLInfo.ID;
            values["FCMBLType"] = FCMBLType.DeclareHBL;
            ClientOceanExportService.DeclarationImport(_CurrentBLInfo.No, values, null);
        }
    }
}


