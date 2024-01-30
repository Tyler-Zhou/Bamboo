using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using ICP.Business.Common.UI;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.DataCache.ServiceInterface;
using ICP.EDI.ServiceInterface;
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ActionType = ICP.Common.ServiceInterface.DataObjects.ActionType;

namespace ICP.FCM.OceanExport.UI.HBL
{
    /// <summary>
    /// HBL货代提单编辑界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class DeclareHBLEditPart : BaseEditPart
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
        ///// <summary>
        ///// 联系人和文档组合控件
        ///// </summary>
        //private UCContactAndDocumentPart ucHblOtherPart;
        ///// <summary>
        ///// 联系人和文档组合控件
        ///// </summary>
        //public UCContactAndDocumentPart UCHblOtherPart
        //{
        //    get
        //    {
        //        if (ucHblOtherPart != null)
        //        {
        //            return ucHblOtherPart;
        //        }
        //        else
        //        {
        //            ucHblOtherPart = Workitem.SmartParts.AddNew<UCContactAndDocumentPart>();
        //            return ucHblOtherPart;
        //        }
        //    }
        //}
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
        DeclareHBLInfo _CurrentBLInfo = null;
        /// <summary>
        /// 箱信息
        /// </summary>
        List<DeclareBLContainerList> _ctnList = null;
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

        #region Thread Save
        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime ThreadStartTime;
        #endregion
        #endregion

        #region 初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public DeclareHBLEditPart()
        {
            InitializeComponent();
            SyncLocalData = true;
            if (!LocalData.IsDesignMode)
            {
                Load += (sender, e) =>
                {
                    ActivateSmartPartClosingEvent(Workitem);
                    

                    barSavingClose.ItemClick += barSavingClose_ItemClick;
                    barCancel.ItemClick += barCancel_ItemClick;
                    barlabMessage.ItemClick += barlabMessage_ItemClick;
                    TimerSaveData = new System.Windows.Forms.Timer();
                    TimerSaveData.Interval = 1000;
                    if (LocalData.IsEnglish == false) SetCnText();
                };
                Disposed += delegate
                {
                    dxErrorProvider1.DataSource = null;
                    panelScroll.Click -= OnpanelScrollClick;
               
                    bindingContainers.DataSource = null;
                    bindingSource1.DataSource = null;
                    _agentCustomersList = null;
                    _bookingInfo = null;
                    _businessOperationParameter = null;
                    _countryList = null;
                    _ctnList = null;
                    _CurrentBLInfo = null;
                    _OceanMBLs = null;
                    _businessOperationParameter = null;
                    //ucHblOtherPart.Dispose();
                    Saved = null;
                    stateValues = null;
                    SmartPartClosing -= HBLEditPart_SmartPartClosing;
                    stateValues = null;
                    OperationContext = null;
                    mscmbCountry.OnFirstEnter -= OnmscmbCountryFirstEnter;

                    barSavingClose.ItemClick -= barSavingClose_ItemClick;
                    barCancel.ItemClick -= barCancel_ItemClick;
                    barlabMessage.ItemClick -= barlabMessage_ItemClick;

                    cmbISFImporterRefCountry.OnFirstEnter -= OncmbISFImporterRefCountryFirstEnter;

                    cmbConsigneeCountry.OnFirstEnter -= OncmbConsigneeCountryFirstEnter;

                    cmbBuyerCountry.OnFirstEnter -= OncmbBuyerCountryFirstEnter;

                    cmbPaymentTerm.OnFirstEnter -= OncmbPaymentTermFirstEnter;
                    cmbQuantityUnit.OnFirstEnter -= OncmbQuantityUnitFirstEnter;
                    cmbWeightUnit.OnFirstEnter -= OncmbWeightUnitFirstEnter;
                    cmbMeasurementUnit.OnFirstEnter -= OncmbMeasurementUnitFirstTimeEnter;
                    cmbTransportClause.OnFirstEnter -= OncmbTransportClauseFirstEnter;
                    stxtAgent.FirstTimeEnter -= OnstxtAgentFirstEnter;
                    cmbPaymentTerm.SelectedIndexChanged -= cmbPaymentTerm_SelectedIndexChanged;
                    stxtAgent.EditValueChanged -= stxtAgent_EditValueChanged;
                    stxtAgent.OnOk -= stxtAgent_OnOk;
                    stxtNotifyParty.OnOk -= stxtNotifyParty_OnOk;
                    stxtPreVoyage.EditValueChanged -= stxtPreVoyage_EditValueChanged;
                    stxtShipper.OnOk -= stxtShipper_OnOk;
                    stxtVoyage.EditValueChanged -= stxtVoyage_EditValueChanged;
                  
                    if (Workitem != null)
                    {
                        Workitem.Items.Remove(this);
                        Workitem = null;
                    }
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

            barWebEdi.Caption = "网上直接EDI";

            barSubPrint.Caption = "打印";
            barPrintBL.Caption = "打印提单";

            //barReplyAgent.Caption = "申请代理(&R)";
            btnContainer.Text = "箱信息";

            navBarBaseInfo.Caption = "基本信息";
            navBarBLInfo.Caption = "提单信息";
            navBarCargo.Caption = "货物信息";

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

            //if (ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.DefaultCompanyID != new System.Guid("a62a9f8e-e69c-4e6e-ad85-e75aed3c6cf9"))
            //{
            //    txtHscode.Visible = false;
            //    stxtPlacePay.Width = 335;
            //}

            //if (!string.IsNullOrEmpty(_CurrentBLInfo.DeclareNo))
            //{
            //    txtHscode.Enabled = true;
            //}
        
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
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.CompanyID) == false)
            {

                List<UserList> saless = UserService.GetUnderlingUserList(new Guid[] { _CurrentBLInfo.CompanyID }, null, new string[] { "操作员" }, true);
                Dictionary<string, string> col = new Dictionary<string, string>();
                col.Add(LocalData.IsEnglish ? "EName" : "CName", "名称");
                col.Add("Code", "代码");
             
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
                    
                    //_CurrentBLInfo.OceanBookingID = _businessOperationParameter.Context.OperationID;
                    List<OceanBLList> booking = OceanExportService.GetDeclareBLListByIds(_CurrentBLInfo.OceanBookingID == Guid.Empty ? _businessOperationParameter.Context.OperationID : _CurrentBLInfo.OceanBookingID);
                    OceanBookingList bookings = OceanExportService.GetOceanBookingListByIds(new Guid[] { _CurrentBLInfo.OceanBookingID == Guid.Empty ? _businessOperationParameter.Context.OperationID : _CurrentBLInfo.OceanBookingID})[0];
                    _OceanMBLs = bookings.OceanMBLs;
                    foreach (var item in booking)
                    {
                        if (item.ID == item.MBLID)
                        {
                            cmbMBLNO.Properties.Items.Add(item.No);
                        }
                    }
                    if (cmbMBLNO.Properties.Items.Count == 1)
                    {
                        cmbMBLNO.Text = cmbMBLNO.Properties.Items[0].ToString();
                        if (_CurrentBLInfo.MBLID == Guid.Empty)
                        {
                            _CurrentBLInfo.MBLID = booking.Find(r => r.ID == r.MBLID).MBLID;
                            _CurrentBLInfo.MBLNo = booking.Find(r => r.ID == r.MBLID).No;
                            _CurrentBLInfo.MBLUpdateDate = booking.Find(r => r.ID == r.MBLID).UpdateDate;
                            cmbMBLNO.SelectedIndex = 0;
                        }
                    }
                    else
                        cmbMBLNO.Text = _CurrentBLInfo.MBLNo;   
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
                if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.CheckerID))
                {
                    _CurrentBLInfo.CheckerID = Guid.Empty;
                }
                stxtChecker.SetOperationContext(OperationContext);
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
        }

        void stxtChecker_EditValueChanging(object sender, ChangingEventArgs e)
        {
            //Guid oldId = (Guid)e.OldValue;
            //if (!ArgumentHelper.GuidIsNullOrEmpty(oldId))
            //{
            //    RemoveContactList(oldId);
            //}

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
            //if (stxtChecker.IsContactDataChanged)
            //{
            //    if (!ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.CheckerID))
            //    {
            //        stxtChecker.ContactList = GetCurrentContactListByCustomerID((Guid)_CurrentBLInfo.CheckerID, ContactType.Customer);
            //    }
            //}
        }
        ///// <summary>
        ///// 移除联系人列表
        ///// </summary>
        ///// <param name="changeID"></param>
        //private void RemoveContactList(Guid changeID)
        //{
        //    ucHblOtherPart.RemoveContactList(changeID, null);
        //}
        ///// <summary>
        ///// 通过客户ID获取联系人信息
        ///// </summary>
        ///// <param name="customerID">客户ID</param>
        ///// <param name="contactType">联系人类型</param>
        ///// <returns></returns>
        //private List<CustomerCarrierObjects> GetCurrentContactListByCustomerID(Guid customerID, ContactType contactType)
        //{
        //    List<CustomerCarrierObjects> contactList = UCHblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID && item.Type == contactType);
        //    return contactList;
        //}
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

            //if (_bookingInfo.IsOnlyMBL)
            //{
            //    if (MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Un Done" : "此订舱单已钩选\"只出MBL\",强制关联HBL会自动修改订舱单数据.是否继续?"
            //                        , LocalData.IsEnglish ? "Tip" : "提示"
            //                        , MessageBoxButtons.YesNo) == DialogResult.No)
            //    {
            //        return;
            //    }
            //}

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
            if (_CurrentBLInfo != null || isSaveOperationContact)
            {
                Stopwatch StopwatchStep = Stopwatch.StartNew();
                Guid _OtherPartLogID = Guid.NewGuid();
                //保存联系人列表及附件
                //UCHblOtherPart.SetContext = GetContext(_CurrentBLInfo);
                //UCHblOtherPart.Save(_CurrentBLInfo.UpdateDate);
                UpdateContactControlData();
                if (_businessOperationParameter == null)
                {
                    _businessOperationParameter = new BusinessOperationParameter();
                }

                _businessOperationParameter.Context = GetContext(_CurrentBLInfo);

                if (Saved != null) Saved(new object[] { _CurrentBLInfo,_businessOperationParameter, _businessOperationParameter.Context });

                StopwatchHelper.CustomRecordStopwatch(StopwatchStep, _OtherPartLogID, DateTime.Now, BaseFormID, "SAVE-HBL-OTHER", string.Format("保存MBL其它面板:联系人列表及附件;OperationID[{0}]HBL ID[{1}]",_CurrentBLInfo.OceanBookingID,_CurrentBLInfo.ID));
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "数据保存成功");
            }
            return true;
        }


        private bool Save(bool needValidate=true)
        {
            try
            {
                OperationLogID = Guid.NewGuid();
                StopwatchSaveData = Stopwatch.StartNew();

                StopwatchHelper.CustomRecordStopwatch(StopwatchSaveData, OperationLogID, DateTime.Now, BaseFormID,
                    "SAVE-DeclareHBL", string.Format("保存DeclareHBL;Operation ID{0} HBL ID[{1}]", _CurrentBLInfo.OceanBookingID, _CurrentBLInfo.ID));

                if (needValidate && ValidateData() == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:数据未通过验证");
                    return false;
                }

                if (_CurrentBLInfo.IsDirty == false && _CurrentBLInfo.IsNew == false && isChangedCtnList == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存成功:数据未更改!");
                    return true;
                }

                isSave = true;

                #region 处理与MBL的关系
                BookingBLInfo existBlItem = _OceanMBLs.Find(delegate(BookingBLInfo item) { return item.NO == _CurrentBLInfo.MBLNo; });
                if (existBlItem != null)
                {
                    _CurrentBLInfo.MBLID = existBlItem.ID;
                    _CurrentBLInfo.MBLUpdateDate = existBlItem.UpdateDate;
                }
                else if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.MBLID) == false)
                {
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
                    };
                    if (isChangedCtnList == false && isToAgentChange && !ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID))//只更改IsToAgent 时也要触发更新MBL箱列表。
                    {
                        _ctnList = OceanExportService.GetDeclareHBLContainerList(_CurrentBLInfo.ID);
                    }
                    SaveDeclareHBLInfoParameter hbl = ConvertHBLToParameter(false, _CurrentBLInfo);
                    if (hbl.mblID == Guid.Empty && string.IsNullOrEmpty(hbl.mblNO) == false)
                    {
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存报关单失败:没有关联的MBL！"));
                        return false;
                    }

                    SaveBLContainerParameter ctn = ConvertCtnToParameter(false, _CurrentBLInfo.OceanBookingID, _ctnList);
                    //不是寄给港后代理的提单不需要同步到MBL。
                    SingleResult result = OceanExportService.SaveDeclareHBLAndContainerWithTrans(hbl, ctn, ctnIDList, ctnUpdateDateList, IsSynToMBL);
                    SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                    ManyResult ctnResult = null;
                    if (ctn != null)
                    {
                        //SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                        ctnResult = result.GetValue<ManyResult>("ContainerResult");
                        isHasContainer = true;
                    }
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true, string.Format("DeclareHBL & 箱信息 保存成功 [{0}ms]", StopwatchStep.ElapsedMilliseconds));
                    isSave = true;
                    _CurrentBLInfo.IsDirty = false;
                    _CurrentBLInfo.isneedNotice = false;
                    #endregion

                    #region  处理返回值
                    UpdateBLBySaved(blResult);

                    if (ctnResult != null && ctnResult.Items.Count > 0)
                    {
                        _CurrentBLInfo.MBLUpdateDate = ctnResult.Items[0].GetValue<DateTime?>("MBLUpdateDate");
                        
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
                    isAutoBulidMBL = false;
                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveDeclareHBLInfoParameter hbl = ConvertHBLToParameter(false, _CurrentBLInfo);
                    if (hbl.mblID == Guid.Empty && string.IsNullOrEmpty(hbl.mblNO) == false)
                    {
                        if (!IsExisteMBLNo())
                        {
                            StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存失败:DeclareHBL[{0}]已存在！", _CurrentBLInfo.No));
                            return false;
                        }
                    }

                    // bool IsSynToMBL = false;
                    //if (dialogResult == DialogResult.OK) { IsSynToMBL = true; }

                    SingleResult result = OceanExportService.SaveDeclareHBLInfo(hbl, IsSynToMBL);

                    UpdateBLBySaved(result);
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, true, string.Format("DeclareHBL 保存成功 [{0}ms]", StopwatchStep.ElapsedMilliseconds));
                    isSave = true;
                    _CurrentBLInfo.IsDirty = false;
                    _CurrentBLInfo.isneedNotice = false;
                    #endregion
                }

                ctnIDList = new List<Guid>();
                ctnUpdateDateList = new List<DateTime?>();

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


                _CurrentBLInfo.ContainerNos = OEUtility.BulidCtnNOByDeclareContainerList(_ctnList);
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
            #region BL Validate
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
            #endregion

            #region Container Validate
            if (_CurrentBLInfo.Validate
                        (
                            delegate(ValidateEventArgs e)
                            {
                                foreach (var item in containerListPart.DataSource as List<DeclareBLContainerList>)
                                {
                                    
                                    if (string.IsNullOrEmpty(item.No))
                                    {
                                    }
                                    else
                                    {
                                        string errorInfo = ICP.FCM.Common.ServiceInterface.Common.ValidateContainerHelper.CheckContainerNo(item.No);
                                        if (string.IsNullOrEmpty(errorInfo) == false)
                                        {
                                            isScrrs[1] = false;
                                            e.SetErrorInfo("No", errorInfo);
                                        }
                                    }
                                    if (item.TypeID == System.Guid.Empty)
                                    {
                                        isScrrs[1] = false;
                                        e.SetErrorInfo("TypeID", "箱型必须填写");
                                    }
                                }
                            }
                        ) == false) isScrrs[0] = false;
            #endregion
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
                    "SAVE-DeclareHBL", string.Format("另存DeclareHBL;OperationID[{0}]DeclareHBL ID[{1}]", _CurrentBLInfo.OceanBookingID, _CurrentBLInfo.ID));

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



                if (_ctnList == null) _ctnList = OceanExportService.GetDeclareHBLContainerList(_CurrentBLInfo.ID);
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
                    SaveDeclareHBLInfoParameter hbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    if (hbl.mblID == Guid.Empty && string.IsNullOrEmpty(hbl.mblNO) == false) isAutoBulidMBL = true;

                    SingleResult result = OceanExportService.SaveDeclareHBLInfo(hbl, true);

                    _CurrentBLInfo.ID = result.GetValue<Guid>("ID");
                    _CurrentBLInfo.MBLID = result.GetValue<Guid>("OceanMBLID");
                    _CurrentBLInfo.MBLUpdateDate = result.GetValue<DateTime?>("MBLUpdateDate");
                    _CurrentBLInfo.No = result.GetValue<string>("No");
                    _CurrentBLInfo.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    txtCtnInfo.Text = _CurrentBLInfo.ContainerDescription = string.Empty;
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("DeclareHBL[{0}]保存成功[{1}]", _CurrentBLInfo.No, StopwatchStep.ElapsedMilliseconds));
                }
                else
                {
                    isAutoBulidMBL = false;
                    Stopwatch StopwatchStep = Stopwatch.StartNew();
                    SaveDeclareHBLInfoParameter mbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    SaveDeclareHBLInfoParameter hbl = ConvertHBLToParameter(true, _CurrentBLInfo);
                    if (hbl.mblID == Guid.Empty && string.IsNullOrEmpty(hbl.mblNO) == false)
                    {
                        StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("保存失败:没有关联的MBL！"));
                        return false;
                    }

                    SaveBLContainerParameter ctn = ConvertCtnToParameter(true, _CurrentBLInfo.OceanBookingID, _ctnList);
                    //SingleResult result = oeService.SaveOceanHBLAndContainerWithTrans(mbl, ctn, ctnIDList, ctnUpdateDateList,true);
                    SingleResult result = OceanExportService.SaveDeclareHBLAndContainerWithTrans(mbl, ctn, ctnIDList, ctnUpdateDateList, true);

                    SingleResult blResult = result.GetValue<SingleResult>("BLResult");
                    ManyResult ctnResult = result.GetValue<ManyResult>("ContainerResult");

                    #region  处理返回值

                    _CurrentBLInfo.ID = blResult.GetValue<Guid>("ID");
                    _CurrentBLInfo.MBLID = blResult.GetValue<Guid>("OceanMBLID");
                    _CurrentBLInfo.MBLUpdateDate = blResult.GetValue<DateTime?>("MBLUpdateDate");
                    _CurrentBLInfo.No = blResult.GetValue<string>("No");
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

                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("DeclareHBL [{0}] & 箱信息 保存成功[{1}]", _CurrentBLInfo.No, StopwatchStep.ElapsedMilliseconds));

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
            //if (customerID == Guid.Empty)
            //{
            //    return;
            //}
            //List<CustomerCarrierObjects> contactList = new List<CustomerCarrierObjects>();
            //if (!UCHblOtherPart.CurrentContactList.Exists(item => item.CustomerID == customerID))
            //{
            //    contactList = FCMCommonService.GetLatestContactList(OperationType.OceanExport, _CurrentBLInfo.CompanyID, customerID, contactType, ContactStage.Unknown);
            //    if (contactList == null || contactList.Count <= 0)
            //        return;
            //    for (int i = 0; i < contactList.Count; i++)
            //    {
            //        contactList[i].Id = Guid.Empty;

            //    }
            //    List<CustomerCarrierObjects> currentContactList = UCHblOtherPart.CurrentContactList;
            //    if (currentContactList == null || currentContactList.Count <= 0)
            //    {
            //        UCHblOtherPart.InsertContactList(contactList);
            //    }
            //    else
            //    {
            //        List<string> nameList = (from item in currentContactList select item.Name).ToList();
            //        List<string> emailList = (from item in currentContactList select item.Mail).ToList();

            //        contactList = contactList.FindAll(item => !nameList.Contains(item.Name) && !emailList.Contains(item.Mail));
            //        UCHblOtherPart.InsertContactList(contactList);
            //    }
            //}
            //else
            //{
            //    contactList = UCHblOtherPart.CurrentContactList.FindAll(item => item.CustomerID == customerID);
            //}
            //SetContactList(customerID, contactList);



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
                #region 初始化 箱数据
                if (_ctnList == null)
                {
                    if (_CurrentBLInfo.IsNew == false)
                    {
                        _ctnList = OceanExportService.GetDeclareHBLContainerList(_CurrentBLInfo.ID);
                    }
                    else
                    {
                        List<OceanContainerList> bookingCtns = OceanExportService.GetOceanContainerList(_CurrentBLInfo.OceanBookingID);
                        if (bookingCtns != null && bookingCtns.Count > 0)
                        {
                            _ctnList = new List<DeclareBLContainerList>();
                            foreach (var item in bookingCtns)
                            {
                                DeclareBLContainerList ctn = new DeclareBLContainerList();
                                OEUtility.CopyToValue(item, ctn, typeof(DeclareBLContainerList));

                                _ctnList.Add(ctn);
                            }
                        }
                        else
                        {
                            _ctnList = new List<DeclareBLContainerList>();
                            if (isInitBookingContainerDescription == false && bookingContainerDescription != null)
                            {
                                foreach (var ctn in bookingContainerDescription.Containers)
                                {
                                    for (int i = 0; i < ctn.Quantity; i++)
                                    {
                                        DeclareBLContainerList newCtn = new DeclareBLContainerList();
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

                    containerListPart.Saved += delegate(object[] prams)
                    {
                        #region 确定箱后更新页面信息
                        _ctnList = ConvertDeclareContainerList(prams[0] as List<OceanBLContainerList>);

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
                containerListPart.DataSource = ConvertOceanContainerList(_ctnList);
                containerListPart.IDList = ctnIDList;
                containerListPart.UpdateDateList = ctnUpdateDateList;
                PartLoader.ShowDialog(containerListPart, LocalData.IsEnglish ? "Edit HBLCotainer" : "编辑HBL箱信息", FormBorderStyle.Sizable);

            }

            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message); }
        }

        private List<DeclareBLContainerList> ConvertDeclareContainerList(List<OceanBLContainerList> BLLists)
        {
            List<DeclareBLContainerList> NewList = new List<DeclareBLContainerList>();
            DeclareBLContainerList dlist;
            foreach (OceanBLContainerList olist in BLLists)
            {
                dlist = new DeclareBLContainerList();
                dlist.ArriveDate = olist.ArriveDate;
                dlist.BLID = olist.BLID;
                dlist.CargoCreateBy = olist.CargoCreateBy;
                dlist.CargoCreateDate = olist.CargoCreateDate;
                dlist.CargoID = olist.CargoID;
                dlist.CargoUpdateDate = olist.CargoUpdateDate;
                dlist.CarNo = olist.CarNo;
                dlist.Commodity = olist.Commodity;
                dlist.CreateByID = olist.CreateByID;
                dlist.CreateByName = olist.CreateByName;
                dlist.CreateDate = olist.CreateDate;
                dlist.CTNOper = olist.CTNOper;
                dlist.DeliveryDate = olist.DeliveryDate;
                dlist.DriverName = olist.DriverName;
                dlist.ID = olist.ID;
                dlist.IsDirty = olist.IsDirty;
                dlist.IsPartOf = olist.IsPartOf;
                dlist.IsSelected = olist.IsSelected;
                dlist.IsSOC = olist.IsSOC;
                dlist.Marks = olist.Marks;
                dlist.Measurement = olist.Measurement;
                dlist.No = olist.No;
                dlist.OceanBookingID = olist.OceanBookingID;
                dlist.PONO = olist.PONO;
                dlist.Quantity = olist.Quantity;
                dlist.Relation = olist.Relation;
                dlist.ReturnDate = olist.ReturnDate;
                dlist.SealNo = olist.SealNo;
                dlist.ShippingOrderNo = olist.ShippingOrderNo;
                dlist.TypeID = olist.TypeID;
                dlist.TypeName = olist.TypeName;
                dlist.Undoable = olist.Undoable;
                dlist.UpdateByID = olist.UpdateByID;
                dlist.UpdateByName = olist.UpdateByName;
                dlist.UpdateDate = olist.UpdateDate;
                dlist.VGMCrossWeight = olist.VGMCrossWeight;
                dlist.VGMMethod = olist.VGMMethod;
                dlist.Weight = olist.Weight;
                NewList.Add(dlist);
            }
            return NewList;
        }

        private List<OceanBLContainerList> ConvertOceanContainerList(List<DeclareBLContainerList> BLLists)
        {
            List<OceanBLContainerList> NewList = new List<OceanBLContainerList>();
            OceanBLContainerList dlist;
            foreach (DeclareBLContainerList olist in BLLists)
            {
                dlist = new OceanBLContainerList();
                dlist.ArriveDate = olist.ArriveDate;
                dlist.BLID = olist.BLID;
                dlist.CargoCreateBy = olist.CargoCreateBy;
                dlist.CargoCreateDate = olist.CargoCreateDate;
                dlist.CargoID = olist.CargoID;
                dlist.CargoUpdateDate = olist.CargoUpdateDate;
                dlist.CarNo = olist.CarNo;
                dlist.Commodity = olist.Commodity;
                dlist.CreateByID = olist.CreateByID;
                dlist.CreateByName = olist.CreateByName;
                dlist.CreateDate = olist.CreateDate;
                dlist.CTNOper = olist.CTNOper;
                dlist.DeliveryDate = olist.DeliveryDate;
                dlist.DriverName = olist.DriverName;
                dlist.ID = olist.ID;
                dlist.IsDirty = olist.IsDirty;
                dlist.IsPartOf = olist.IsPartOf;
                dlist.IsSelected = olist.IsSelected;
                dlist.IsSOC = olist.IsSOC;
                dlist.Marks = olist.Marks;
                dlist.Measurement = olist.Measurement;
                dlist.No = olist.No;
                dlist.OceanBookingID = olist.OceanBookingID;
                dlist.PONO = olist.PONO;
                dlist.Quantity = olist.Quantity;
                dlist.Relation = olist.Relation;
                dlist.ReturnDate = olist.ReturnDate;
                dlist.SealNo = olist.SealNo;
                dlist.ShippingOrderNo = olist.ShippingOrderNo;
                dlist.TypeID = olist.TypeID;
                dlist.TypeName = olist.TypeName;
                dlist.Undoable = olist.Undoable;
                dlist.UpdateByID = olist.UpdateByID;
                dlist.UpdateByName = olist.UpdateByName;
                dlist.UpdateDate = olist.UpdateDate;
                dlist.VGMCrossWeight = olist.VGMCrossWeight;
                dlist.VGMMethod = olist.VGMMethod;
                dlist.Weight = olist.Weight;
                NewList.Add(dlist);
            }
            return NewList;
        }
        #endregion

        #region 关闭

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            Type t = typeof(EDIValue);
            System.Reflection.PropertyInfo[] propertyInfos = t.GetProperties();
            foreach (System.Reflection.PropertyInfo propertyInfo in propertyInfos)
            {
                ICP.Framework.CommonLibrary.Common.GuidRequiredAttribute ts = (GuidRequiredAttribute)propertyInfo.GetCustomAttributes(true)[0];
                string s = ts.CMessage;
            }
            
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
            }
        }

        private void RefreshBLData()
        {
            if (ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.ID)) return;

            Focus();
            _CurrentBLInfo = OceanExportService.GetDeclareHBLInfo(_CurrentBLInfo.ID);
            _CurrentBLInfo.CancelEdit();
            _CurrentBLInfo.BeginEdit();
            bindingSource1.DataSource = _CurrentBLInfo;
            bindingSource1.ResetBindings(false);
            InitCustomerDescriptionObject();
            Refresh();

            // }
        }

        #endregion
        private delegate void DataBindDelegate(DeclareHBLInfo hblInfo);

        #region IEditPart 成员
        void BindingData(DeclareHBLInfo hblInfo)
        {
            if (hblInfo != null)
            {
                InnerBindData(hblInfo);
            }
            else
            {
    
                //string operationNo = stateValues["OperationNo"] as string;
                Guid hblid = (Guid)stateValues["HblID"];

                DeclareHBLInfo temp = OceanExportService.GetDeclareHBLInfo(hblid);

                InnerBindData(temp);

            }
        }
        private void InnerBindData(DeclareHBLInfo hblInfo)
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
            set { BindingData(value as DeclareHBLInfo); }
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

        SaveDeclareHBLInfoParameter ConvertHBLToParameter(bool IsSaveAs, DeclareHBLInfo info)
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

            SaveDeclareHBLInfoParameter parameter = new SaveDeclareHBLInfoParameter
            {
                id = IsSaveAs ? Guid.Empty : info.ID,
                oceanBookingID = info.OceanBookingID,
                hblNo = info.No,
                mblID = info.MBLID,
                mblNO = info.MBLNo,
                quantity = info.Quantity,
                quantityUnitID = info.QuantityUnitID,
                weight = info.Weight,
                weightUnitID = info.WeightUnitID,
                measurement = info.Measurement,
                measurementUnitID = info.MeasurementUnitID,
                marks = info.Marks,
                goodsDescription = info.GoodsDescription,
                isWoodPacking = info.IsWoodPacking,
                HSCODE = info.HSCODE,
                ctnQtyInfo = info.CtnQtyInfo,
                saveByID = LocalData.UserInfo.LoginID,
                updateDate = IsSaveAs ? null : dt,
                mblUpdateDate = _CurrentBLInfo.MBLUpdateDate
            };

            return parameter;
        }

        SaveBLContainerParameter ConvertCtnToParameter(bool IsSaveAs, Guid oceanBookingID, List<DeclareBLContainerList> list)
        {

            List<DeclareBLContainerList> ulist = new List<DeclareBLContainerList>();
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


                DeclareBLContainerList cl = ulist.Find(o => o.ID == item.ID);
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

        private BusinessOperationContext GetContext(DeclareHBLInfo orderInfo)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = orderInfo.OceanBookingID;
            context.OperationNO = orderInfo.RefNo;
            context.OperationType = OperationType.OceanExport;
            context.FormId = orderInfo.OceanBookingID;
            context.FormType = FormType.Booking;
            return context;
        }

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

        private void barblCHS_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ClientOceanExportService.MailCustomerAskForConfirmSI(false, _bookingInfo.ID, _CurrentBLInfo, null);
        }

        private void barBlENG_ItemClick(object sender, ItemClickEventArgs e)
        {
            //ClientOceanExportService.MailCustomerAskForConfirmSI(true, _bookingInfo.ID, _CurrentBLInfo, null);
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
        private void chkIsBook_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbMBLNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_bookingInfo == null) return;
            OceanMBLInfo mInfo = OceanExportService.GetOceanMBLInfo(_bookingInfo.No, cmbMBLNO.Text);
            if (mInfo == null) return;

            _CurrentBLInfo.CustomerName = mInfo.CustomerName;

            _CurrentBLInfo.CompanyID = mInfo.CompanyID;
            
            _CurrentBLInfo.OceanBookingID = mInfo.OceanBookingID;

            _CurrentBLInfo.AgentOfCarrierName = mInfo.AgentOfCarrierName;
            _CurrentBLInfo.CarrierName = mInfo.CarrierName;
            _CurrentBLInfo.SalesName = mInfo.SalesName;
            _CurrentBLInfo.FilerName = mInfo.FilerName;
            _CurrentBLInfo.BookingerName = mInfo.BookingerName;
            _CurrentBLInfo.ReleaseType = mInfo.ReleaseType;
            cmbReleaseType.ShowSelectedValue(_CurrentBLInfo.ReleaseType, _CurrentBLInfo.ReleaseTypeName);


            #region 收发通,代理

            #region 发货人

            _CurrentBLInfo.ShipperID = mInfo.ShipperID == null ? Guid.Empty : mInfo.ShipperID.Value;
            _CurrentBLInfo.ShipperName = mInfo.ShipperName;
            _CurrentBLInfo.ShipperDescription = mInfo.ShipperDescription.ConvertToOriginal();
            if (_CurrentBLInfo.ShipperDescription != null)
                txtShipperDescription.Text = _CurrentBLInfo.ShipperDescription.ToString(LocalData.IsEnglish);

            #endregion

            #region NotifyParty .收货人 = 订舱单.收货人 通知人描述 = “SAME AS CONSIGNEE”

            _CurrentBLInfo.ConsigneeID = mInfo.ConsigneeID;
            _CurrentBLInfo.ConsigneeName = mInfo.ConsigneeName;
            stxtConsignee.CustomerDescription = _CurrentBLInfo.ConsigneeDescription = mInfo.ConsigneeDescription.ConvertToOriginal();
            if (_CurrentBLInfo.ConsigneeDescription != null)
                txtConsigneeDescription.Text = _CurrentBLInfo.ConsigneeDescription.ToString(LocalData.IsEnglish);

            _CurrentBLInfo.NotifyPartyID = Guid.Empty;
            _CurrentBLInfo.NotifyPartyName = string.Empty;
            stxtNotifyParty.CustomerDescription = _CurrentBLInfo.NotifyPartyDescription = new CustomerDescription();
            txtNotifyPartyDescription.Text = "SAME AS CONSIGNEE";

            #endregion

            #region Agent  HBL.代理 = 订舱单.代理

            SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);

            _CurrentBLInfo.AgentID = mInfo.AgentID;
            _CurrentBLInfo.AgentName = mInfo.AgentName;
            _CurrentBLInfo.AgentDescription = mInfo.AgentDescription;
            //SetAgentSourceByCompanyID(_CurrentBLInfo.CompanyID);

            #endregion

            #region 订舱人
            _CurrentBLInfo.BookingPartyID = mInfo.BookingPartyID;
            _CurrentBLInfo.BookingPartyName = mInfo.BookingPartyName;
            isUpdateShipper = _CurrentBLInfo.ShipperID == _CurrentBLInfo.BookingPartyID ? true : false;

            #endregion
            #endregion

            #region Port

            //	HBL.收货地描述 = 订舱单.收货地.名称 
            //	HBL.装货港描述 = 订舱单. 装货港.名称 
            //	HBL.卸货港描述 = 订舱单. 卸货港.名称 
            //	HBL.交货地描述 = 订舱单. 交货地.名称 
            _CurrentBLInfo.PlaceOfDeliveryID = mInfo.PlaceOfDeliveryID;
            _CurrentBLInfo.PlaceOfDeliveryCode = mInfo.PlaceOfDeliveryCode;
            _CurrentBLInfo.PlaceOfDeliveryName = mInfo.PlaceOfDeliveryName;

            _CurrentBLInfo.POLID = mInfo.POLID;
            _CurrentBLInfo.POLCode = mInfo.POLCode;
            _CurrentBLInfo.POLName = mInfo.POLName;

            _CurrentBLInfo.PODID = mInfo.PODID;
            _CurrentBLInfo.PODCode = mInfo.PODCode;
            _CurrentBLInfo.PODName = mInfo.PODName;

            _CurrentBLInfo.PlaceOfReceiptID = mInfo.PlaceOfReceiptID;
            _CurrentBLInfo.PlaceOfReceiptCode = mInfo.PlaceOfReceiptCode;
            _CurrentBLInfo.PlaceOfReceiptName = mInfo.PlaceOfReceiptName;

            _CurrentBLInfo.FinalDestinationID = mInfo.PlaceOfDeliveryID;
            _CurrentBLInfo.FinalDestinationCode = mInfo.FinalDestinationCode;
            _CurrentBLInfo.FinalDestinationName = mInfo.PlaceOfDeliveryName;
            

            #endregion

            #region Voyage

            _CurrentBLInfo.PreVoyageID = mInfo.PreVoyageID;
            _CurrentBLInfo.VoyageID = mInfo.VoyageID;
            OceanExportPrintHelper.SetPrintCheckByVoyageType(_CurrentBLInfo.PreVoyageID, _CurrentBLInfo.VoyageID, chkShowPreVoyage, chkShowVoyage);
            _CurrentBLInfo.ETD = mInfo.ETD;
            _CurrentBLInfo.ETA = mInfo.ETA;

            //if (ICP.Framework.CommonLibrary.Helper.ArgumentHelper.GuidIsNullOrEmpty(_CurrentBLInfo.VoyageID) == false)
            //{
            //    VoyageInfo voyageInfo = tfService.GetVoyageInfo(_CurrentBLInfo.VoyageID.Value);
            //}

            #endregion

            #region 付款方式,运输条款

            _CurrentBLInfo.TransportClauseID = mInfo.TransportClauseID;
            _CurrentBLInfo.TransportClauseName = mInfo.TransportClauseName;
            cmbTransportClause.ShowSelectedValue(_CurrentBLInfo.TransportClauseID, _CurrentBLInfo.TransportClauseName);
            _CurrentBLInfo.PaymentTermID = mInfo.PaymentTermID;
            _CurrentBLInfo.PaymentTermName = mInfo.PaymentTermName;

            cmbPaymentTerm.SelectedIndexChanged -= new EventHandler(cmbPaymentTerm_SelectedIndexChanged);
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

            _CurrentBLInfo.Quantity = mInfo.Quantity;
            if (ArgumentHelper.GuidIsNullOrEmpty(mInfo.QuantityUnitID) == false)
            {
                cmbQuantityUnit.Text = _CurrentBLInfo.QuantityUnitName = mInfo.QuantityUnitName;
                _CurrentBLInfo.QuantityUnitID = mInfo.QuantityUnitID;

                cmbQuantityUnit.ShowSelectedValue(_CurrentBLInfo.QuantityUnitID, _CurrentBLInfo.QuantityUnitName);

            }
            _CurrentBLInfo.Weight = mInfo.Weight;
            if (ArgumentHelper.GuidIsNullOrEmpty(mInfo.WeightUnitID) == false)
            {
                cmbWeightUnit.Text = _CurrentBLInfo.WeightUnitName = mInfo.WeightUnitName;
                _CurrentBLInfo.WeightUnitID = mInfo.WeightUnitID;
                cmbWeightUnit.ShowSelectedValue(_CurrentBLInfo.WeightUnitID, _CurrentBLInfo.WeightUnitName);
            }
            _CurrentBLInfo.Measurement = mInfo.Measurement;
            if (ArgumentHelper.GuidIsNullOrEmpty(mInfo.MeasurementUnitID) == false)
            {
                cmbMeasurementUnit.Text = _CurrentBLInfo.MeasurementUnitName = mInfo.MeasurementUnitName;
                _CurrentBLInfo.MeasurementUnitID = mInfo.MeasurementUnitID;
                cmbMeasurementUnit.ShowSelectedValue(_CurrentBLInfo.MeasurementUnitID, _CurrentBLInfo.MeasurementUnitName);
            }
            #endregion


            _CurrentBLInfo.CarrierName = mInfo.CarrierName;

            _CurrentBLInfo.ContractID = mInfo.ContractID;
            _CurrentBLInfo.IsHasContract = !ArgumentHelper.GuidIsNullOrEmpty(mInfo.ContractID);
            bindingSource1.EndEdit();
            bindingSource1.ResetBindings(false);
        }
    }
}
