using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI.CustomerManager;
using ICP.DataCache.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.FileSystem.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.ReportCenter.UI;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ICP.FAM.UI.CustomerBill
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public partial class CustomerBillEditPart : BaseEditPart
    {
        #region 服务
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ITransportFoundationService TransportFoundationService
        {
            get
            {
                return ServiceClient.GetService<ITransportFoundationService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IWorkflowClientService WorkflowClientService
        {
            get
            {
                return ServiceClient.GetClientService<IWorkflowClientService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IOceanImportService OceanImportService
        {
            get
            { return ServiceClient.GetService<IOceanImportService>(); }
        }
        /// <summary>
        /// 
        /// </summary>
        IOceanExportService OceanExportService
        {
            get { return ServiceClient.GetService<IOceanExportService>(); }
        }
        /// <summary>
        /// 
        /// </summary>
        IAirExportService AirExportService
        {
            get { return ServiceClient.GetService<IAirExportService>(); }
        }
        /// <summary>
        /// 
        /// </summary>
        IOperationAgentService OperationAgentService
        {
            get { return ServiceClient.GetService<IOperationAgentService>(); }
        }
        /// <summary>
        /// 
        /// </summary>
        RefereshBillService RefereshBillService
        {
            get
            {
                return ClientHelper.Get<RefereshBillService, RefereshBillService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        IDataFinderFactory DataFinderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IDataFinderFactory>();
            }
        }
        #endregion

        #region Init
        /// <summary>
        /// 
        /// </summary>
        public CustomerBillEditPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            barSavingTools.Visible = false;

            #region Event
            barSavingClose.ItemClick += barSavingClose_ItemClick;
            barCancel.ItemClick += barCancel_ItemClick;
            barlabMessage.ItemClick += barlabMessage_ItemClick;
            //seTrem.EditValueChanged += seTrem_EditValueChanged; 
            #endregion

            TimerSaveData = new Timer();
            TimerSaveData.Interval = 1000;
            TimerSaveData.Tick += TimerSaveData_Tick;
            Disposed += delegate
            {
                CheckDispatchState();
                if (customerFinder != null)
                {
                    customerFinder.Dispose();
                    customerFinder = null;
                }
                if (shipToFinder != null)
                {
                    shipToFinder.Dispose();
                    shipToFinder = null;
                }
                if (chargingCodeFinder != null)
                {
                    chargingCodeFinder.Dispose();
                    chargingCodeFinder = null;
                }
                gcChargeList.DataSource = null;
                bsBillInfo.DataSource = null;
                bsBillInfo.Dispose();
                bsChargeList.PositionChanged -= bsChargeList_PositionChanged;
                bsChargeList.DataSource = null;
                bsChargeList.Dispose();
                bsReviseChargeList.DataSource = null;
                bsReviseChargeList.Dispose();
                Saved = null;
                _AgentCustoemr = null;
                _ConfigureInfo = null;
                _CurrencyList = null;
                _DefaultFromData = null;
                _OperationCommonInfo = null;
                _packTypes = null;
                _RateList = null;
                _TradeCustoemrs = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };
            if (!LocalData.IsEnglish) gcReviseFeeHistory.Text = "原始费用(只读)";
        }

        #endregion

        #region 本地属性
        /// <summary>
        /// 
        /// </summary>
        bool isShow = false;
        /// <summary>
        /// 申请分文件和修订的状态
        /// </summary>
        int _ApplyState = -1;
        /// <summary>
        /// 当前行号
        /// </summary>
        int currentRowHandle = -1;
        /// <summary>
        /// 
        /// </summary>
        bool billIsDispacth = false;
        /// <summary>
        /// 是否是第一次新建代理
        /// </summary>
        bool IsFirstNewAgent = true;
        /// <summary>
        /// 账单关帐后保存权限
        /// </summary>
        string COMEditPer = "COMMON_AFTERACCOUNTCLOSESAVE";
        /// <summary>
        /// 
        /// </summary>
        Guid currentOperationID;
        /// <summary>
        /// Thread Save 开始时间
        /// </summary>
        DateTime ThreadStartTime;
        /// <summary>
        /// 
        /// </summary>
        AgentBillCheckStatusEnum _AgentBillCheckStatusEnum = AgentBillCheckStatusEnum.None;
        /// <summary>
        /// 
        /// </summary>
        ChargeList oldCharge;
        /// <summary>
        /// 
        /// </summary>
        OperationCommonInfo _OperationCommonInfo = null;
        /// <summary>
        /// 
        /// </summary>
        ConfigureInfo _ConfigureInfo = null;
        /// <summary>
        /// 默认的业务单据信息
        /// </summary>
        FormData _DefaultFromData = null;
        /// <summary>
        /// 业务中的代理客户
        /// </summary>
        OperationCustomer _AgentCustoemr = null;
        /// <summary>
        /// 
        /// </summary>
        List<SolutionExchangeRateList> _RateList = null;
        /// <summary>
        /// 
        /// </summary>
        List<SolutionCurrencyList> _CurrencyList = null;
        /// <summary>
        /// 
        /// </summary>
        List<DataDictionaryList> _packTypes = null;
        /// <summary>
        /// 当前业务的客户列表(收发通)包括帐单已选的新客户
        /// </summary>
        List<OperationCustomer> _TradeCustoemrs = new List<OperationCustomer>();
        /// <summary>
        /// 
        /// </summary>
        List<CustomerCarrierObjects> cusList = new List<CustomerCarrierObjects>();
        /// <summary>
        /// 缓存的客户详细信息
        /// </summary>
        Dictionary<Guid, CustomerInfo> customerInfos = new Dictionary<Guid, CustomerInfo>();
        /// <summary>
        /// 月结客户列表
        /// </summary>
        Dictionary<Guid, MonthlyClosingCustomer> mcCustomers = new Dictionary<Guid, MonthlyClosingCustomer>();

        #region 申请分文件和修订的状态(ApplyState)
        /// <summary>
        /// 申请分文件和修订的状态
        /// </summary>
        public int ApplyState
        {
            get
            {
                return _ApplyState;
            }
            set
            {
                _ApplyState = value;
            }
        }
        #endregion

        #region 开始执行Revise时间(StartTime)
        /// <summary>
        /// 
        /// </summary>
        DateTime? startTime;
        /// <summary>
        /// 
        /// </summary>
        DateTime? StartTime
        {
            get
            {
                if (startTime != null)
                {
                    return startTime;
                }
                else
                {
                    startTime = FinanceService.GetStartDateForReviseAgentBill();
                    return startTime;
                }
            }
        } 
        #endregion

        DateTime? OICreateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 最后一行
        /// </summary>
        ChargeList LastCharge
        {
            get
            {
                if (gvChargeList.RowCount > 0) return gvChargeList.GetRow(gvChargeList.RowCount - 1) as ChargeList;
                else return null;
            }
            set
            {
                if (LastCharge != null)
                {
                    LastCharge = value;
                }
            }
        }

        /// <summary>
        /// 当前帐单对象
        /// </summary>
        BillInfo _CurrentData
        {
            get
            {
                if (bsBillInfo.DataSource == null) return null;
                else return bsBillInfo.DataSource as BillInfo;
            }
            set
            {
                if (_CurrentData != null)
                {
                    BillInfo currentData = _CurrentData;
                    currentData = value;
                }
            }
        }
        
        /// <summary>
        /// 费用当前行
        /// </summary>
        ChargeList CurrentCharge
        {
            get
            {
                if (bsChargeList.Current == null) return null;
                else return bsChargeList.Current as ChargeList;
            }
            set
            {
                if (CurrentCharge != null)
                {
                    ChargeList currentCharge = CurrentCharge;
                    currentCharge = value;
                }
            }
        }
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType
        {
            get;
            set;
        }
        /// <summary>
        /// 分文件类型
        /// </summary>
        public DocumentState DocumentState
        {
            get;
            set;
        }
        public CustomerBillListPart CustomerBillListPart
        {
            get;
            set;
        }

        /// <summary>
        /// 海运费费用Code
        /// </summary>
        public static List<Guid> OCChargingCodeID
        {
            get
            {
                List<Guid> idlist = new List<Guid>();
                idlist.Add(new Guid("E746D7BA-8077-4297-ABF4-C5AD28833405"));//ocb
                idlist.Add(new Guid("5EDFFC17-3C45-4F76-BCF5-580D30F67697"));//odb
                idlist.Add(new Guid("8FF31CF8-F798-4567-91E3-3B200F6DE125"));//CFS 集装箱场站费用 
                return idlist;
            }
        }

        #endregion

        #region Init Controls and Data

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                panelInfo.Height = 100;
                panelMore.Height = 0;
                InitMessage();
                InitControls();
            }
        }

        void InitMessage()
        {
            RegisterMessage("DeleteChargeError", LocalData.IsEnglish ? "Failed to delete, cannot be removed by contract generation Cost" : "删除失败,无法删除由合约产生的费用");
        }

        void InitControls()
        {
            if (_OperationCommonInfo.OperationType == OperationType.Truck ||
                (_OperationCommonInfo.OperationType == OperationType.OceanImport &&
                _OperationCommonInfo.CompanyID == new Guid("85A7D77F-2070-43E0-B866-FFF151BDCC5A")))
            {
            }
            else
            {
                colRemark.ColumnEdit = rtxtRemark;
            }

            FAMUtility.SetGridViewColumnAllowEditColor(gvChargeList);

            InitComboboxSource();
            SearchRegister();
            SearchBoxAdapter.RegisterChargingCodeMultipleSearchBox(DataFindClientService, txtFreightIncluded);

            if (!LocalData.IsEnglish)
            {
                labAREmail.Text = "催放货联系人邮箱(多个邮箱请以；隔开)";
                labReleaseEmail.Text = "放单联系人邮箱";
            }
        }

        public void Setvisible(BillInfo Data)
        {
            if (Data != null && Data.Type == BillType.AR && (Data.OperationType == OperationType.OceanExport || Data.OperationType == OperationType.OceanImport))
            {
                if (_ConfigureInfo.SolutionID.ToString().ToUpper() == "B6E4DDED-4359-456A-B835-E8401C910FD0")
                {
                    labAREmail.Visible = true;
                    cmbAREmail.Visible = true;
                }
            }
            else
            {
                labAREmail.Visible = false;
                labReleaseEmail.Visible = false;
                cmbAREmail.Visible = false;
                cmbReleaseEmail.Visible = false;
            }
        }

        /// <summary>
        /// 初始化Combobox的数据源
        /// </summary>
        void InitComboboxSource()
        {
            lookupEdit.BestFitMode = BestFitMode.BestFit;

            lookupEdit.DataSource = _CurrencyList;
            lookupEdit.DisplayMember = "CurrencyName";
            lookupEdit.ValueMember = "CurrencyID";
            lookupEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
            lookupEdit.UseCtrlScroll = false;
            lookupEdit.Columns.Add(new LookUpColumnInfo("CurrencyName", LocalData.IsEnglish ? "Currency" : "币种"));
            lookupEdit.BestFit();
            colCurrencyID.BestFit();
            foreach (var item in _CurrencyList)
            {
                cmbPadCurrency.Properties.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
                repositoryItemImageComboBox3.Items.Add(new ImageComboBoxItem(item.CurrencyName, item.CurrencyID));
            }
            cmbRefNo.SelectedIndexChanged += delegate
            {
                if (cmbRefNo.EditValue == null || cmbRefNo.EditValue.ToString().Length == 0)
                {
                    _CurrentData.FormID = Guid.Empty;
                    _CurrentData.FormType = FormType.Unknown;
                }
                else
                {
                    _CurrentData.FormID = cmbRefNo.EditValue.ToString().ToGuid();
                    FormType sFormType = _OperationCommonInfo.Forms.Find(delegate(FormData item) { return item.ID == _CurrentData.FormID; }).Type;
                    if (sFormType != FormType.Unknown)
                    {
                        _CurrentData.FormType = sFormType;
                    }
                }
            };

            _packTypes = TransportFoundationService.GetDataDictionaryList(string.Empty, string.Empty, DataDictionaryType.ValuationUnit, true, 0);
            foreach (var item in _packTypes)
            {
                cmbUnit.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
                repositoryItemImageComboBox2.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? item.EName : item.CName, item.ID));
            }

            cmbUnit.SelectedIndexChanged += (sender, e) =>
            {
                Guid guidUnitID = (Guid)((ImageComboBoxEdit)sender).EditValue;
                if (!"E0EBD8DD-90FC-462A-B51D-B4B1D06539D4".Equals(guidUnitID.ToString()))
                {
                    CurrentCharge.ContainerID = null;
                }
            };

            #region Container No(柜号)
            List<FormData> formDatas = new List<FormData>();
            try
            {
                switch (OperationType)
                {
                    case OperationType.AirExport:
                        List<AirContainerList> aeContainers = AirExportService.GetAirContainerList(_OperationCommonInfo.OperationID);
                        formDatas.AddRange(aeContainers.Select(airItem => new FormData { ID = airItem.ID, No = airItem.No }));
                        break;
                    case OperationType.OceanExport:
                        List<OceanContainerList> oeContainers = OceanExportService.GetOceanContainerList(_OperationCommonInfo.OperationID);
                        formDatas.AddRange(oeContainers.Select(oeItem => new FormData { ID = oeItem.ID, No = oeItem.No }));
                        break;
                    case OperationType.OceanImport:
                        List<OIBusinessContainerList> oiContainers = OceanImportService.GetOIContainerList(_OperationCommonInfo.OperationID);
                        formDatas.AddRange(from oiItem in oiContainers where oiItem.ID != null select new FormData { ID = oiItem.ID.Value, No = oiItem.No });
                        break;
                }
            }
            catch (Exception ex)
            {
                formDatas = new List<FormData>();
            }

            cmbContainerNo.BeginUpdate();
            foreach (FormData item in formDatas)
            {
                cmbContainerNo.Items.Add(new ImageComboBoxItem(item.No, item.ID));
            }
            cmbContainerNo.EndUpdate();
            #endregion

            #region emun

            List<EnumHelper.ListItem<FeeWay>> feeWays = EnumHelper.GetEnumValues<FeeWay>(LocalData.IsEnglish);

            foreach (var item in feeWays)
            {
                if (item.Value == FeeWay.None) continue;
                cmbWay.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
                repositoryItemImageComboBox1.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }
            #endregion
        }
        void InitCustomerPopup(BillType type)
        {
            if (_CurrentData == null) return;

            if (_CurrentData.CustomerDescription == null) _CurrentData.CustomerDescription = new FAMCustomerDescription();

            famCustomerDescriptionPart1.SetParentControl(cmbCustomer, _CurrentData.CustomerDescription);
        }
        IDisposable customerFinder, shipToFinder, chargingCodeFinder;
        /// <summary>
        /// 注册搜索器
        /// </summary>
        void SearchRegister()
        {
            #region Customer
            //注册客户搜索器
            customerFinder = DataFindClientService.Register(cmbCustomer, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.CustomerResultValue,
                                   GetCustomerSearchConditionCollection,
                   delegate (object inputSource, object[] resultData)
                   {
                       cmbCustomer.ClosePopup();
                       Guid id = new Guid(resultData[0].ToString());
                       if (id == _CurrentData.CustomerID)
                       {
                           return;
                       }

                       OperationCustomer tager = _TradeCustoemrs.Find(delegate (OperationCustomer item) { return item.ID == id; });
                       if (tager != null)
                       {
                           cmbCustomer.EditValue = _CurrentData.CustomerID = id;
                           _CurrentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                       }
                       else
                       {
                           CustomerStateType state = (CustomerStateType)resultData[7];
                           if (state == CustomerStateType.Invalid)
                           {
                               if (PartLoader.PopCustomerIsInvalid() != DialogResult.Yes)
                               {
                                   return;
                               }
                           }

                           CustomerCodeApplyState? approved = (CustomerCodeApplyState?)resultData[8];
                           if (!approved.HasValue
                               || (approved.HasValue && approved.Value != CustomerCodeApplyState.Passed))
                           {

                               if (approved.Value == CustomerCodeApplyState.Processing)
                               {
                                   DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customers has not been approved!" : "该客户尚未通过审核!"
                    , LocalData.IsEnglish ? "Tip" : "提示"
                    , MessageBoxButtons.OK);

                                   return;
                               }
                               else if (approved.Value == CustomerCodeApplyState.UnApply)
                               {
                                   if ((resultData[11] == null ||
                                       string.IsNullOrEmpty(resultData[11].ToString())) &&
                                       (resultData[12] == null ||
                                       string.IsNullOrEmpty(resultData[12].ToString())))
                                   {
                                       MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                   , LocalData.IsEnglish ? "Tip" : "提示"
                   , MessageBoxButtons.OK);

                                       return;
                                   }

                                   DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer have not yet applied for the code. Whether to apply the code?" : "该客户尚未申请代码，是否要申请代码?"
               , LocalData.IsEnglish ? "Tip" : "提示"
               , MessageBoxButtons.YesNo);
                                   if (result == DialogResult.Yes)
                                   {
                                       CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                         LocalData.UserInfo.LoginID,
                                                                         LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                         (DateTime?)resultData[10]);
                                   }

                                   return;
                               }
                               else if (approved.Value == CustomerCodeApplyState.Unpassed)
                               {
                                   if ((resultData[11] == null ||
                                       string.IsNullOrEmpty(resultData[11].ToString())) &&
                                       (resultData[12] == null ||
                                       string.IsNullOrEmpty(resultData[12].ToString())))
                                   {
                                       MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The customer's fax and E-mail are empty, please add customer information and then apply the code!" : "该客户的传真和邮箱都为空，请补充客户资料后再申请代码!"
                   , LocalData.IsEnglish ? "Tip" : "提示"
                   , MessageBoxButtons.OK);

                                       return;
                                   }

                                   DialogResult result = MessageBoxService.ShowQuestion("该客户尚未通过审核，若重新申请代码需要去完善客户资料。是否重新申请代码?"
               , LocalData.IsEnglish ? "Tip" : "提示"
               , MessageBoxButtons.YesNo);
                                   if (result == DialogResult.Yes)
                                   {
                                       CustomerService.ApplyCustomerCode(new Guid(resultData[0].ToString()),
                                                                         LocalData.UserInfo.LoginID,
                                                                         LocalData.IsEnglish ? "Customer AutoApply. Source : order Customer." : "客户代码自动申请。来源：订单 客户。",
                                                                         (DateTime?)resultData[10]);
                                   }

                                   return;
                               }
                           }

                           tager = new OperationCustomer();
                           tager.ID = id;
                           tager.Code = resultData[1].ToString();
                           tager.EName = resultData[2].ToString();
                           tager.CName = resultData[3].ToString();
                           tager.Term = int.Parse(resultData[9].ToString());
                           _TradeCustoemrs.Insert(0, tager);
                           cmbCustomer.Properties.DataSource = _TradeCustoemrs;
                           cmbCustomer.EditValueChanged += cmbCustomer_EditValueChanged;
                           cmbCustomer.EditValue = _CurrentData.CustomerID = id;
                           _CurrentData.CustomerName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                       }
                   },
                   delegate ()
                   {
                       cmbCustomer.EditValue = null;
                       _CurrentData.CustomerName = string.Empty;
                       _CurrentData.CustomerID = Guid.Empty;
                   },
                   ClientConstants.MainWorkspace);

            #endregion

            #region ShipTo

            shipToFinder = DataFindClientService.Register(txtShipTo, CommonFinderConstants.CustoemrFinder, SearchFieldConstants.CodeName, SearchFieldConstants.CustomerResultValue,
                                        GetCustomerSearchConditionCollection,
                  delegate (object inputSource, object[] resultData)
                  {
                      Guid id = new Guid(resultData[0].ToString());
                      txtShipTo.Tag = _CurrentData.ShipToID = id;
                      txtShipTo.Text = _CurrentData.ShipToName = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();

                  },
                  delegate ()
                  {
                      txtShipTo.Tag = _CurrentData.ShipToID = null;
                      txtShipTo.Text = _CurrentData.ShipToName = string.Empty;
                  },
                  ClientConstants.MainWorkspace);
            #endregion

            #region ChargingCode
            chargingCodeFinder = DataFindClientService.RegisterGridColumnFinder(colChargingCode
                                                 , CommonFinderConstants.SolutionChargingCodeFinder
                                                 , new string[] { "ChargingCodeID", "FeeCode", "ChargingDescription", "IsAgent" }
                                                 , new string[] { "ChargingCodeID", "Code", "ChargingCodeName", "IsAgent" }
                                                   , GetSolutionChargingCodeSearchCondition);

            #endregion
        }

        SearchConditionCollection GetCustomerSearchConditionCollection()
        {
            if (_CurrentData == null || _CurrentData.Type != BillType.DC) return null;

            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", _ConfigureInfo.SolutionID, false);
            return conditions;
        }

        SearchConditionCollection GetSolutionChargingCodeSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", _ConfigureInfo.SolutionID, false);
            return conditions;
        }

        #region Customer

        /// <summary>
        /// 初始客户
        /// </summary>
        void InitTradeCustomers()
        {
            _TradeCustoemrs = new List<OperationCustomer>();
            if (_OperationCommonInfo.TradeCustomers == null) _OperationCommonInfo.TradeCustomers = new List<OperationCustomer>();

            foreach (var item in _OperationCommonInfo.TradeCustomers)
            {
                if (_CurrentData != null && _CurrentData.Type == BillType.DC)
                {
                    if (item.IsAgent == false) continue;
                }

                _TradeCustoemrs.Add(item);
            }

            if (FAMUtility.GuidIsNullOrEmpty(_CurrentData.CustomerID) == false)
            {
                OperationCustomer tager = _TradeCustoemrs.Find(delegate (OperationCustomer item) { return item.ID == _CurrentData.CustomerID; });
                if (tager == null)
                {
                    OperationCustomer temp = new OperationCustomer();
                    temp.ID = _CurrentData.CustomerID;
                    temp.CName = temp.EName = _CurrentData.CustomerName;
                    _TradeCustoemrs.Insert(0, temp);
                }
            }
            cmbCustomer.ClosePopup();
            cmbCustomer.Properties.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            cmbCustomer.Properties.Columns[0].FieldName = LocalData.IsEnglish ? "EName" : "CName";

            cmbCustomer.Properties.DataSource = _TradeCustoemrs;
            if (FAMUtility.GuidIsNullOrEmpty(_CurrentData.CustomerID) == false)
                cmbCustomer.EditValue = _TradeCustoemrs[0];

            cmbCustomer.EditValueChanged -= cmbCustomer_EditValueChanged;
            cmbCustomer.EditValueChanged += cmbCustomer_EditValueChanged;

            if (_OperationCommonInfo != null && _OperationCommonInfo.OperationType == OperationType.OceanExport)
            {
                ContactObjects dataList = FCMCommonService.GetContactList(_OperationCommonInfo.OperationID, _OperationCommonInfo.OperationType);
                cusList = dataList.CustomerCarrier;

                List<CustomerCarrierObjects> currentlist = new List<CustomerCarrierObjects>();
                if (cusList != null && cusList.Count > 0)
                {
                    cmbAREmail.Properties.Items.Clear();
                    currentlist = cusList.FindAll(r => r.CustomerID == _CurrentData.CustomerID);
                    if (currentlist != null && currentlist.Count > 0)
                    {
                        foreach (var item in currentlist)
                        {
                            cmbAREmail.Properties.Items.Add(item.Mail);
                        }
                    }

                    if (currentlist.Count(r => r.AR) > 0)
                    {
                        cmbAREmail.Text = currentlist.OrderByDescending(r => r.CreateDate).ToList().Find(r => r.AR).Mail;
                    }
                    else
                    {
                        cmbAREmail.SelectedIndex = 0;
                    }
                }
                else
                {

                    if (_CurrentData != null && _CurrentData.CustomerID != Guid.Empty)
                    {
                        List<CustomerContactList> list = CustomerService.GetCustomerContactList(_CurrentData.CustomerID);
                        foreach (var item in list)
                        {
                            cmbAREmail.Properties.Items.Add(item.EMail);
                        }
                        cmbAREmail.SelectedIndex = 0;
                    }
                }
            }
        }

       

        void dteAccountDate_EditValueChanged(object sender, EventArgs e)
        {
            bsBillInfo.EndEdit();
            if (_CurrentData == null || _CurrentData.AccountDate == null) return;
            _CurrentData.AccountDate = dteAccountDate.DateTime;
            Guid id = new Guid(cmbCustomer.EditValue.ToString());
            MonthlyClosingCustomer tager = GetMonthlyClosingCustomer();
            SetCreditTermAndDueDate(tager);
            //dteDueDate.DateTime = _CurrentData.DueDate = dteAccountDate.DateTime.AddDays((int)seTrem.Value);
        }

        void cmbCustomer_EditValueChanged(object sender, EventArgs e)
        {
            cmbCustomer.EditValueChanged -= cmbCustomer_EditValueChanged;
            if (cmbCustomer.EditValue == null) return;
            MonthlyClosingCustomer tager = GetMonthlyClosingCustomer();
            bsBillInfo.EndEdit();

            //seTrem.EditValueChanged -= seTrem_EditValueChanged;
            //dteDueDate.DateTime = _CurrentData.DueDate = _CurrentData.AccountDate.AddDays(tager.CreditTerm);
            SetCreditTermAndDueDate(tager);
            //DateTime operationDate = Convert.ToDateTime(_OperationCommonInfo.OperationDate.ToShortDateString());
            //DateTime chargingCloseDate = Convert.ToDateTime(_ConfigureInfo.ChargingClosingdate.Value.ToShortDateString());
            //if (operationDate > chargingCloseDate)
            //{
            //    seTrem.Value = tager.CreditTerm;
            //}
            //else
            //{
            //    seTrem.Value = 0;
            //}

            //seTrem.Value = tager.Term;

            //seTrem.EditValueChanged += seTrem_EditValueChanged;

            CustomerInfo customer;
            if (customerInfos.ContainsKey(tager.CustomerID)) customer = customerInfos[tager.CustomerID];
            else
            {
                customer = CustomerService.GetCustomerInfo(tager.CustomerID);
                customerInfos.Add(tager.CustomerID, customer);
            }

            if (_CurrentData.CustomerDescription == null) _CurrentData.CustomerDescription = new FAMCustomerDescription();
            _CurrentData.CustomerDescription.Name = LocalData.IsEnglish ? customer.EName : customer.CName;
            _CurrentData.CustomerDescription.Address = LocalData.IsEnglish ? customer.EAddress : customer.CAddress;
            _CurrentData.CustomerDescription.Tel = customer.Tel1;
            _CurrentData.CustomerDescription.Fax = customer.Fax;
            _CurrentData.IsAgentOfCarrier = tager.IsAgentOfCarrier;
            famCustomerDescriptionPart1.SetCustomerDescription(_CurrentData.CustomerDescription);

            if (cusList != null && cusList.Count > 0)
            {
                int i = cusList.FindIndex(r => r.CustomerID == tager.CustomerID);
                if (i >= 0)
                {
                    cmbAREmail.Text = cusList[i].Mail;
                }
                else
                {
                    cmbAREmail.Text = string.Empty;
                }
            }

            cmbCustomer.EditValueChanged += cmbCustomer_EditValueChanged;
        }

        public void setEmai()
        {
            //if (cmbCustomer.EditValue == null || cusList == null) return;

            //if (cusList.Exists(r => r.CustomerID.ToString().ToUpper() == cmbCustomer.EditValue.ToString().ToUpper()))
            //{
            //    cmbAREmail.SelectedIndex = cusList.FindIndex(r => r.CustomerID.ToString().ToUpper() == cmbCustomer.EditValue.ToString().ToUpper());
            //}
        }

        #endregion

        #region CurrencyRate

        /// <summary>
        /// 这个是在bindingData里调用的.用来初始已存储用户自订的汇率
        /// </summary>
        void InitCurrencyRate()
        {
            cmbPadCurrency.SelectedIndexChanged -= cmbPadCurrency_SelectedIndexChanged;
            chkPayByCurrency.CheckedChanged -= chkPayByCurrency_CheckedChanged;

            if (_CurrentData.PayCurrencyId.IsNullOrEmpty())
            {
                cmbPadCurrency.Enabled = gcRate.Enabled = chkPayByCurrency.Checked = false;
            }
            else
            {
                cmbPadCurrency.EditValue = _CurrentData.PayCurrencyId;
                cmbPadCurrency.Enabled = gcRate.Enabled = chkPayByCurrency.Checked = true;
            }

            if (_CurrentData.CurrencyRates != null)
            {
                bsCurrencyRateData.DataSource = _CurrentData.CurrencyRates;
                bsCurrencyRateData.ResetBindings(false);
            }
            else
            {
                bsCurrencyRateData.DataSource = typeof(SolutionExchangeRateList);
                bsCurrencyRateData.ResetBindings(false);
            }

            chkPayByCurrency.CheckedChanged += chkPayByCurrency_CheckedChanged;
            cmbPadCurrency.SelectedIndexChanged += cmbPadCurrency_SelectedIndexChanged;
        }

        void cmbPadCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshReteListPart();
        }

        void RefreshReteListPart()
        {
            List<ChargeList> currentFeeList = bsChargeList.DataSource as List<ChargeList>;

            if (cmbPadCurrency.EditValue == null)
            {
                _CurrentData.PayCurrencyId = null;
                return;
            }

            _CurrentData.PayCurrencyId = new Guid(cmbPadCurrency.EditValue.ToString());
            _CurrentData.CurrencyRates = new List<CurrencyRateData>();

            List<SolutionExchangeRateList> tagers = new List<SolutionExchangeRateList>();

            if (_CurrentData.PayCurrencyId != null)
            {
                foreach (var item in currentFeeList)
                {
                    SolutionExchangeRateList solutionExchangeRate = RateHelper.GetMatchSolutionExchangeRateList(item.CurrencyID, _CurrentData.PayCurrencyId.Value, _RateList, false);
                    if (solutionExchangeRate != null)
                    {
                        tagers.Add(solutionExchangeRate);
                    }
                }
            }

            if (tagers.Count == 0)
            {
                bsCurrencyRateData.DataSource = typeof(SolutionExchangeRateList);
                bsCurrencyRateData.ResetBindings(false);
            }
            else
            {
                foreach (var item in tagers)
                {
                    CurrencyRateData temp = new CurrencyRateData();
                    temp.CurrencyID = item.SourceCurrencyID;
                    temp.CurrencyName = item.SourceCurrency;
                    temp.Rate = item.Rate;
                    _CurrentData.CurrencyRates.Add(temp);
                }

                bsCurrencyRateData.DataSource = _CurrentData.CurrencyRates;
                bsCurrencyRateData.ResetBindings(false);
            }

            InitCurrencyRate();
        }

        void chkPayByCurrency_CheckedChanged(object sender, EventArgs e)
        {
            cmbPadCurrency.Enabled = gcRate.Enabled = chkPayByCurrency.Checked;
            if (chkPayByCurrency.Checked)
            {
                if (_CurrentData.PayCurrencyId.IsNullOrEmpty())
                    cmbPadCurrency.EditValue = _CurrentData.PayCurrencyId = _ConfigureInfo.DefaultCurrencyID;
            }
            else
            {
                cmbPadCurrency.EditValue = _CurrentData.PayCurrencyId = Guid.Empty;
            }
        }

        #endregion

        #region  BillType
        /// <summary>
        /// 这个是在bindingData里调用的.用来初始类型rdo
        /// </summary>
        void InitBillType()
        {
            rdoGroupType.SelectedIndexChanged -= rdoGroupType_SelectedIndexChanged;
            if (_CurrentData.Type == BillType.AR)
            {
                rdoGroupType.SelectedIndex = 0;
            }
            else if (_CurrentData.Type == BillType.AP)
            {
                rdoGroupType.SelectedIndex = 1;
            }
            else if (_CurrentData.Type == BillType.DC)
            {
                rdoGroupType.SelectedIndex = 2;
            }
            BillTypeChanged(true);

            rdoGroupType.SelectedIndexChanged += rdoGroupType_SelectedIndexChanged;
        }

        void rdoGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CurrentData.Type = (BillType)(short)(rdoGroupType.SelectedIndex + 1);
            BillTypeChanged(false);
        }

        /// <summary>
        /// 如果是代理帐单,费用的 IsAgent必需为True.(代收代付标记：出口是指针对代理账单，进口都需要, 即出口才控制可编辑，进口不需要控制)
        /// </summary>
        /// <param name="isInit">初始进入帐单时不用更新Fee</param>
        void BillTypeChanged(bool isInit)
        {
            if (_CurrentData.Type == BillType.DC)
            {
                //colIsAgent.OptionsColumn.AllowEdit = false;
                //colIsAgent.OptionsColumn.TabStop = false;

                if (_CurrentData.CustomerID.IsNullOrEmpty() && _AgentCustoemr != null)
                {
                    _CurrentData.CustomerID = _AgentCustoemr.ID;
                    _CurrentData.CustomerName = _AgentCustoemr.EName;
                }

                if (isInit == false)
                {
                    List<ChargeList> chargeList = bsChargeList.DataSource as List<ChargeList>;
                    foreach (var item in chargeList) { item.IsAgent = true; }

                    bsChargeList.DataSource = chargeList;
                    bsChargeList.ResetBindings(false);
                }
            }
            else
            {
                //colIsAgent.OptionsColumn.AllowEdit = true;
                //colIsAgent.OptionsColumn.TabStop = true;
            }

            if (isInit == false)
            {
                InitTradeCustomers();
            }
        }

        #endregion
        void CheckDispatchState()
        {
            if ((currentOperationID == null || currentOperationID == Guid.Empty) && (_OperationCommonInfo.OperationID == null || _OperationCommonInfo.OperationID == Guid.Empty))
            {
                return;
            }

            currentOperationID = (currentOperationID == null || currentOperationID == Guid.Empty) ? _OperationCommonInfo.OperationID : currentOperationID;
            int state = OperationAgentService.UspGetDispatchLogState(currentOperationID);
            if (state == 1)
            {
                DispathLogData newDispathLog = OperationAgentService.GetDispatchFileLogByOperation(currentOperationID);
                DateTime dt = DateTime.ParseExact(newDispathLog.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                dt = dt.ToLocalTime();
                if (newDispathLog.CreateBy == LocalData.UserInfo.LoginID)
                {
                    bool reslut = FAMUtility.ShowResultMessage(LocalData.IsEnglish ? "The D/C fees has been changed at" + dt.ToString("yyyy-MM-dd HH:mm:ss") + " \n You must re-dispatch Docs to the agent in time."
                                              : "代理账单在(" + dt.ToString("yyyy-MM-dd HH:mm:ss") + ")有过更改 \n 请及时地向代理手动分发文件(Docs)。");
                    if (reslut)
                    {
                        try
                        {
                            BusinessOperationContext context = BusinessOperationContext.Current;
                            context.OperationID = currentOperationID;
                            context.OperationType = OperationType;
                            context.FormType = FormType.Booking;
                            FCM.Common.UI.FCMUIUtility.ShowDispatchDocumentNew(Workitem, context, null, 1);
                        }
                        catch (Exception ex)
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                        }
                    }
                }

            }
        }
        #endregion

        #region Control Event
        /// <summary>
        /// 是否开发票按钮状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void chkVATinvoice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVATinvoice.Checked == true)
            {
                _CurrentData.IsVATInvoiced = true;
                cetxtTaxrate.Enabled = true;
                if (_CurrentData.Type == BillType.AR)
                {
                    if (_CurrentData.Taxrate == null || _CurrentData.Taxrate == 0m)
                    {
                        _CurrentData.Taxrate = _ConfigureInfo.VATrate;
                    }
                }
                else if (_CurrentData.Type == BillType.AP)
                {
                    BeginInvoke(new Action(() =>
                    {
                        Decimal decAP = FinanceService.GetAPBillTariff(_CurrentData.CompanyID, _CurrentData.CustomerID);
                        _CurrentData.Taxrate = (Decimal?)decAP;
                    }));
                }
                else
                {
                    _CurrentData.Taxrate = 0m;
                }
                colIsInvoicedVAT.OptionsColumn.AllowEdit = true;
            }

            if (chkVATinvoice.Checked == false)
            {
                _CurrentData.IsVATInvoiced = false;
                cetxtTaxrate.Enabled = false;
                foreach (var item in _CurrentData.Fees)
                {
                    if (item.IsVATInvoiced == true)
                    {
                        item.IsVATInvoiced = !item.IsVATInvoiced;
                    }
                }
            }
            bsChargeList.ResetBindings(false);
        }
        /// <summary>
        /// 
        /// </summary>
        void billFinder_DataChoosed(object sender, DataFindEventArgs e)
        {
            if (e.Data != null && e.Data[0] != null)
            {
                object[] cuslist = e.Data[0] as object[];
                string strcustName = "";

                Guid customerID = new Guid(cuslist[0].ToString());
                strcustName = cuslist[2].ToString();

                string strocean = LocalData.IsEnglish ? "ICP recommend to set " + strcustName + " as the Carrier of the Shipment." : "是否把" + strcustName + "客户做为业务承运人?";

                if (DialogResult.Yes == MessageBoxService.ShowQuestion(strocean, "", MessageBoxButtons.YesNo))
                {
                    OceanImportService.ChangeOIMBLforCarrier(_OperationCommonInfo.OperationID, customerID);
                    _OperationCommonInfo.AgentOfCarrierID = customerID;
                }
            }
        }
        void chkIsGST_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsGST.Checked == true)
            {
                foreach (var item in _CurrentData.Fees)
                {
                    item.IsGST = true;
                }
            }
            else
            {
                foreach (var item in _CurrentData.Fees)
                {
                    item.IsGST = false;
                }
            }
            bsChargeList.ResetBindings(false);
        }
        #endregion

        #region BarItem Click
        void barSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveData(false);
        }
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
        void TimerSaveData_Tick(object sender, EventArgs e)
        {
            if (!barSavingTools.Visible)
                return;
            DateTime curTime = DateTime.Now;
            TimeSpan span = curTime - ThreadStartTime;
            barlabSeconds.Caption = string.Format((LocalData.IsEnglish ? "({0}seconds)" : "({0}秒)"), (int)span.TotalSeconds);
            if (span.TotalSeconds >= LocalData.AsynchronousSaveTimeout)
            {
                barCancel.Visibility = BarItemVisibility.Always;
            }
        }
        void barNewFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewFee();
        }
        void barRemove_ItemClick(object sender, ItemClickEventArgs e)
        {
            RemoveFee();
        }
        void barMore_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetMoreVisible(!isShow);
        }
        /// <summary>
        /// 保存客户参考号
        /// </summary>
        void barSaveCustomerRefNo_ItemClick(object sender, ItemClickEventArgs e)
        {
            _CurrentData.EndEdit();
            txtNo.Focus();
            txtCustomerRefNo.Focus();

            SingleResultData result = FinanceService.SaveBillCustomerRefNo(_CurrentData.ID, _CurrentData.CustomerRefNo, _CurrentData.UpdateDate, LocalData.UserInfo.LoginID);

            _CurrentData.UpdateDate = result.UpdateDate;

            _CurrentData.EndEdit();

            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }
        void barNewARFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewFee(true);
        }
        void barNewAPFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            NewFee(false);
        }
        void barReviseFee_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (CurrentCharge != null && !CurrentCharge.IsNew)
            {
                if (CurrentCharge.Type == FeeType.Price|| CurrentCharge.Type == FeeType.Rebate)
                {
                    MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The selected fee can't be revised because it's can't only be declared by the agent. Please mail with the agent for revising the fee then re-dispatching. "
                        : "不能修订此费用，该费用只有代理才能修改，请发邮件联系代理修改账单并重新分发文件。");
                    return;
                }
                List<ChargeList> source = bsReviseChargeList.DataSource as List<ChargeList>;
                ChargeList tager = new ChargeList();
                if (source == null)
                {
                    bsReviseChargeList.DataSource = new List<ChargeList>();
                }
                else
                {
                    tager = source.Find(delegate (ChargeList item)
                    {
                        if (item != null)
                            return item.ID == CurrentCharge.ID;
                        return false;
                    });
                    if (tager == null) { tager = new ChargeList(); };
                }
                if (FAMUtility.GuidIsNullOrEmpty(tager.ID))
                {
                    CurrentCharge.FromType = (int)OperationType;
                    FAMUtility.CopyToValue(CurrentCharge, tager, typeof(ChargeList));
                    bsReviseChargeList.Add(tager);
                }
                _CurrentData.ReviseHistoryFees = bsReviseChargeList.DataSource as List<ChargeList>;
                CurrentCharge.IsAllowEdit = true;
                colUnitPrice.OptionsColumn.AllowEdit = true;
                RefreshChargeBillBarEnabled();
                gvChargeList.RefreshRow(currentRowHandle);

            }
        }
        /// <summary>
        /// 申请代码
        /// </summary>
        void btnApplyCode_Click(object sender, EventArgs e)
        {
            if (_CurrentData != null && _CurrentData.CustomerID != Guid.Empty)
            {
                CustomerInfo cusInfo = CustomerService.GetCustomerInfo(_CurrentData.CustomerID);

                if (!String.IsNullOrEmpty(cusInfo.Code))
                {
                    return;
                }
                ApplyCustomerCodeForm applyCodeForm = Workitem.Items.AddNew<ApplyCustomerCodeForm>();
                applyCodeForm.CurrentCustomer = cusInfo;
                string title = LocalData.IsEnglish ? "Apply Code" : "申请代码";

                DialogResult dlg = PartLoader.ShowDialog(applyCodeForm, title);
                if (dlg != DialogResult.OK)
                {
                    return;
                }
            }

        }
        /// <summary>
        /// 关帐后保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAccountClos_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveData(true);
        }
        #endregion

        #region GvEvent
        void lwGVReviseHistory_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {

            if (e.RowHandle < 0) return;
            ChargeList tmp = (ChargeList)lwGVReviseHistory.GetRow(e.RowHandle);
            if (tmp.IsCancel)
            {
                e.Appearance.ForeColor = Color.Blue;
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }
        void gvChargeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gvChargeList.FocusedColumn == colWay
                    || gvChargeList.FocusedColumn == colChargingCode
                    || gvChargeList.FocusedColumn == colChargingDescription
                    || gvChargeList.FocusedColumn == colUnitPrice
                    || gvChargeList.FocusedColumn == colQuantity
                    || gvChargeList.FocusedColumn == colUnitID
                    || gvChargeList.FocusedColumn == colCurrencyID
                    || gvChargeList.FocusedColumn == colContainerNo)
                {
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    if (colIsAgent.OptionsColumn.AllowEdit == true)
                    {
                        if (gvChargeList.FocusedColumn == colRemark) SendKeys.Send("{TAB}");
                        else if (gvChargeList.FocusedColumn == colIsAgent)
                        {
                            if (gvChargeList.FocusedRowHandle >= 0 && gvChargeList.FocusedRowHandle == gvChargeList.RowCount - 1)
                                NewFee();

                            //SendKeys.Send("{TAB}");
                        }
                    }
                    else if (gvChargeList.FocusedColumn == colRemark)
                    {
                        if (gvChargeList.FocusedRowHandle >= 0 && gvChargeList.FocusedRowHandle == gvChargeList.RowCount - 1)
                            NewFee();

                        //SendKeys.Send("{TAB}");
                    }


                }

            }

        }
        void bsChargeList_PositionChanged(object sender, EventArgs e)
        {
            RefreshChargeBillBarEnabled();
        }
        void gvChargeList_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0) return;
            //Amount
            if (e.Column.Name == colUnitPrice.Name || e.Column.Name == colQuantity.Name)
            {
                object row = gvChargeList.GetRow(e.RowHandle);

                if (row != null && row as ChargeList != null)
                {
                    ChargeList fee = row as ChargeList;

                    //小于0时反置方向
                    if (fee.UnitPrice < 0)
                    {
                        fee.UnitPrice = 0 - fee.UnitPrice;
                        if (fee.Way == FeeWay.AP) fee.Way = FeeWay.AR;
                        else fee.Way = FeeWay.AP;
                    }

                    fee.Amount = decimal.Parse((fee.Quantity * fee.UnitPrice).ToString("n"));
                    RefreshBillTatol();
                }

            }

            if (_CurrentData.Type == BillType.DC && CurrentCharge.ID != Guid.Empty && (barReviseFee.Enabled == false || barReviseFee.Visibility == BarItemVisibility.Never))
            {
                var tager = _CurrentData.ReviseHistoryFees.Find(delegate (ChargeList item) { if (item != null) return item.ID == CurrentCharge.ID; return false; });
                if (tager == null)
                {
                    _CurrentData.ReviseHistoryFees.Add(oldCharge);

                }
                else
                {
                    if (tager.Way == CurrentCharge.Way && tager.UnitPrice == CurrentCharge.UnitPrice && tager.Quantity == CurrentCharge.Quantity
                        && tager.FeeCode == CurrentCharge.FeeCode && tager.CurrencyName == CurrentCharge.CurrencyName)
                    {
                        _CurrentData.ReviseHistoryFees.Remove(tager);
                    }
                }

            }
            if (_CurrentData != null) _CurrentData.IsDirty = true;
        }
        void gvChargeList_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Value == null)
            {
                return;
            }

            if (e.Column == colCurrencyID)
            {
                object row = gvChargeList.GetRow(e.RowHandle);
                Guid curID = new Guid(e.Value.ToString());
                if (row != null && row as ChargeList != null)
                {
                    ChargeList fee = row as ChargeList;

                    fee.Rate = 0;
                    fee.Rate = RateHelper.GetRate(curID, _ConfigureInfo.StandardCurrencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), _RateList);
                    fee.CurrencyID = curID;
                    RefreshBillTatol();
                }

                if (_CurrentData != null) _CurrentData.IsDirty = true;

                RefreshReteListPart();
            }
        }
        void RefreshBillTatol()
        {
            List<ChargeList> list = bsChargeList.DataSource as List<ChargeList>;
            if (list == null || list.Count == 0)
            {
                txtChargeTotal.Text = string.Empty;
                txtApTotal.Text = string.Empty;
                txtARTotal.Text = string.Empty;
            }
            else
            {
                Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
                #region 构建币种字典
                foreach (var item in list)
                {
                    if (dic.Keys.Contains(item.CurrencyID) == false)
                    {
                        dic.Add(item.CurrencyID, 0m);
                    }
                    if (item.Way == FeeWay.AR)
                    {
                        dic[item.CurrencyID] += item.Amount;
                    }
                    else
                    {
                        dic[item.CurrencyID] -= item.Amount;
                    }
                }
                #endregion

                #region 构建合计字符串
                StringBuilder strbulider = new StringBuilder();
                StringBuilder apstring = new StringBuilder();
                StringBuilder arstring = new StringBuilder();
                foreach (var item in dic)
                {
                    string currencyName = RateHelper.GetCurrencyNameByCurrencyID(item.Key);

                    if (strbulider.Length > 0) strbulider.Append(" ");
                    strbulider.Append(currencyName);
                    strbulider.Append(":" + item.Value.ToString("n"));

                    //统计应收项
                    decimal apAmount = (from d in list where d.Way == FeeWay.AP && d.CurrencyID == item.Key select d.Amount).Sum();
                    if (apstring.Length > 0) apstring.Append(" ");
                    apstring.Append(currencyName);
                    apstring.Append(":" + apAmount.ToString("n"));

                    //统计应付项
                    decimal arAmount = (from d in list where d.Way == FeeWay.AR && d.CurrencyID == item.Key select d.Amount).Sum();
                    if (arstring.Length > 0) arstring.Append(" ");
                    arstring.Append(currencyName);
                    arstring.Append(":" + arAmount.ToString("n"));


                }
                txtChargeTotal.Text = strbulider.ToString();
                txtApTotal.Text = apstring.ToString();
                txtARTotal.Text = arstring.ToString();
                #endregion
            }
        }
        void gvChargeList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 10000);
            }
        }
        void gcChargeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == false && e.Alt == false)
            {
                switch (e.KeyCode)
                {
                    case Keys.F2:
                        CustomerBillListPart.AddData(BillType.AR);
                        break;
                    case Keys.F3:
                        CustomerBillListPart.AddData(BillType.AP);
                        break;
                    case Keys.F4:
                        CustomerBillListPart.AddData(BillType.DC);
                        break;
                }
            }
        }
        void gvChargeList_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (e.FocusedColumn == colUnitPrice || e.FocusedColumn == colQuantity)
            {
                //切换到英文输入法
                try
                {
                    var enInput = InputLanguage.FromCulture(CultureInfo.GetCultureInfo("en-US"));

                    if (enInput != null)
                    {
                        InputLanguage.CurrentInputLanguage = enInput;
                    }
                    else
                    {
                        InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
                    }
                }
                catch
                {

                }

            }
        }
        void gvChargeList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            try
            {
                currentRowHandle = e.FocusedRowHandle;
                if (currentRowHandle < 0) return;
                if (StartTime < OICreateDate)
                {
                    ChargeClone();
                    if (CurrentCharge != null && CurrentCharge.FromType != null && OperationType == OperationType.OceanExport)
                    {

                        if ((CurrentCharge.FromType != (int)OperationType && _CurrentData.Type == BillType.DC) || CurrentCharge.Type == FeeType.Price || CurrentCharge.Type == FeeType.Rebate)
                        {

                            gvChargeList.OptionsBehavior.Editable = false;
                        }
                        else
                        {
                            gvChargeList.OptionsBehavior.Editable = true;
                        }
                    }

                    if (OperationType == OperationType.OceanImport && CurrentCharge.FromType == 1 && CurrentCharge.Type != FeeType.Price && CurrentCharge.Type != FeeType.Rebate)
                    {
                        barReviseFee.Visibility = BarItemVisibility.Never;
                        barReviseFee.Enabled = false; ;
                        lblReviseTip.Visible = false;


                    }
                    else
                    {
                        barReviseFee.Visibility = BarItemVisibility.Never;
                        lblReviseTip.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        void gvChargeList_RowCellClick(object sender, RowCellClickEventArgs e)
        {

            if (!_ConfigureInfo.IsVATinvoice && e.Column == colIsInvoicedVAT)
            {
                //公司配置为不开增值税发票，不能选择
                return;
            }

            if (e.Column == colIsInvoicedVAT && CurrentCharge.ChargingCodeID == _ConfigureInfo.VATFeeID)
            {
                FAMUtility.ShowMessage(LocalData.IsEnglish ? "have no choice for VAT cost" : "增值税费用不能选择！");
                return;
            }

            //账单中的是否开增值税发票没有选择  勾选上
            if (e.Column == colIsInvoicedVAT && _CurrentData.IsVATInvoiced == false)
            {
                chkVATinvoice.Checked = true;
            }

            if (e.Column == colIsAgent)
            {
                CurrentCharge.IsAgent = !CurrentCharge.IsAgent;
            }
            else if (e.Column == colIsInvoicedVAT)
            {
                CurrentCharge.IsVATInvoiced = !CurrentCharge.IsVATInvoiced;
            }
            else if (e.Column == colIsSecondSale)
            {

                CurrentCharge.IsSecondSale = !CurrentCharge.IsSecondSale;
            }
            else if (e.Column == colIsGST)
            {
                CurrentCharge.IsGST = !CurrentCharge.IsGST;
            }

            bsChargeList.ResetCurrentItem();
        }
        /// <summary>
        /// 列颜色
        /// </summary>
        void gvChargeList_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            ChargeList chargeItem = gvChargeList.GetRow(e.RowHandle) as ChargeList;

            if (chargeItem == null)
            {
                return;
            }
            //joe 代理账单不能修改
            if (chargeItem.Type == FeeType.Price || chargeItem.Type == FeeType.Rebate)
            {
                e.Appearance.BackColor = Color.LightYellow;
                e.Appearance.Options.UseForeColor = true;
            }
            else if (chargeItem.FromType != null && chargeItem.FromType != (int)OperationType && StartTime < OICreateDate)
            {
                e.Appearance.ForeColor = Color.Gray;
                e.Appearance.Options.UseForeColor = true;

            }

        }
        #endregion

        #region 本地方法

        void RefreshChargeBillBarEnabled()
        {
            if (CurrentCharge == null)
            {
                barRemove.Enabled = false;
            }
            else
            {
                if (StartTime > OICreateDate)
                {
                    barReviseFee.Visibility = BarItemVisibility.Never;
                    lblReviseTip.Visible = false;
                    if (CurrentCharge.Type == FeeType.Price)
                    {
                        barRemove.Enabled = false;
                        gvChargeList.OptionsBehavior.Editable = false;
                    }
                    else
                    {
                        barRemove.Enabled = true;
                        gvChargeList.OptionsBehavior.Editable = true;
                    }
                }
                else
                {
                    if (OperationType == OperationType.OceanImport && CurrentCharge.FromType == 1 && CurrentCharge.Type != FeeType.Price && CurrentCharge.Type != FeeType.Rebate)
                    {
                        barReviseFee.Visibility = BarItemVisibility.Never;
                        //lblReviseTip.Top = panelInfo.Bottom + 5;
                        // lblReviseTip.Visible = true;
                        lblReviseTip.Visible = false;

                    }
                    else
                    {
                        barReviseFee.Visibility = BarItemVisibility.Never;
                        lblReviseTip.Visible = false;
                    }
                    //// barReviseFee.Visibility = OperationType == OperationType.OceanImport || CurrentCharge.IsNew ? BarItemVisibility.Never : BarItemVisibility.Always;
                    // barReviseFee.Enabled = OperationType == OperationType.OceanImport ? true : false;
                    if (CurrentCharge.Type == FeeType.Price)
                    {
                        barRemove.Enabled = false;
                        // barReviseFee.Enabled = false;
                        gvChargeList.OptionsBehavior.Editable = false;
                    }

                    else
                    {

                        if (_CurrentData.Type != BillType.DC || CurrentCharge.IsDispatch == false)
                        {
                            barRemove.Enabled = true;
                            gvChargeList.OptionsBehavior.Editable = true;
                            barReviseFee.Enabled = false;
                            lblReviseTip.Visible = false;
                        }
                        else
                        {
                            if (OperationType == OperationType.OceanExport && CurrentCharge.FromType == 2)
                            {
                                barRemove.Enabled = false;
                                barReviseFee.Enabled = false;
                            }
                            else if (OperationType == OperationType.OceanImport && CurrentCharge.FromType == 1)
                            {
                                if (CurrentCharge.IsAllowEdit)
                                {
                                    barRemove.Enabled = false;
                                    gvChargeList.OptionsBehavior.Editable = true;
                                }
                                else
                                {
                                    barRemove.Enabled = false;
                                    gvChargeList.OptionsBehavior.Editable = false;
                                }
                            }
                            else if (CurrentCharge.FromType == (int)OperationType && !CurrentCharge.IsDispatch)
                            {
                                // barReviseFee.Enabled = false;
                                barRemove.Enabled = true;
                                gvChargeList.OptionsBehavior.Editable = true;
                            }
                            else if (CurrentCharge.Type != FeeType.Price || CurrentCharge.Type !=FeeType.Rebate)
                            {
                                barRemove.Enabled = true;
                                gvChargeList.OptionsBehavior.Editable = true;
                            }
                            else
                            {
                                barRemove.Enabled = false;
                                gvChargeList.OptionsBehavior.Editable = true;
                            }
                        }
                    }
                }
                List<ChargeList> list = bsReviseChargeList.DataSource as List<ChargeList>;
                if (list == null)
                {
                    list = new List<ChargeList>();
                }
                bool isGetRow = false;

                for (int i = 0; i < list.Count; i++)
                {
                    ChargeList dr = lwGVReviseHistory.GetRow(i) as ChargeList;
                    if (dr != null && dr.ID == CurrentCharge.ID)
                    {

                        dr.IsCancel = true;
                    }
                    else
                    {
                        if (dr != null)
                        {
                            dr.IsCancel = false;
                        }

                    }
                }
                lwGVReviseHistory.RefreshData();
                if (StartTime < OICreateDate && _CurrentData.Type == BillType.DC)
                {
                    cmbRefNo.Enabled = !billIsDispacth;
                }
                else
                {
                    cmbRefNo.Enabled = true;
                }


                if (RefereshBillService.RefereshBillDelete != null) RefereshBillService.RefereshBillDelete(barRemove.Enabled && !billIsDispacth);
            }
        }
        /// <summary>
        /// 获取当前客户月结资料
        /// </summary>
        /// <returns></returns>
        MonthlyClosingCustomer GetMonthlyClosingCustomer()
        {
            MonthlyClosingCustomer mcCustomer = null;
            if (cmbCustomer.EditValue == null)
                return mcCustomer;
            Guid id = Guid.Empty;
            try
            {
                id = new Guid(cmbCustomer.EditValue.ToString());
            }
            catch { }
            if (id.IsNullOrEmpty())
                return mcCustomer;
            if (mcCustomers.ContainsKey(id))
            {
                mcCustomer = mcCustomers[id];
            }
            else
            {
                mcCustomer = FinanceService.GetMonthlyClosingCustomer(id, _ConfigureInfo.CompanyID);
                mcCustomers.Add(id, mcCustomer);
            }
            return mcCustomer;
        }

        void SetCreditTermAndDueDate(MonthlyClosingCustomer mcCustomer,bool setDueDate = true)
        {

            //非月结客户
            if (mcCustomer ==null || mcCustomer.CalculateTermType == CalculateTermType.Unknown)
            {
                if(setDueDate) dteDueDate.DateTime = _CurrentData.AccountDate;
                seTrem.Value = 0;
                return;
            }
            //付款日
            DateTime paymentDate = DateTime.MinValue;
            //月结配置付款日
            DateTime configPaymentDate = DateTime.MinValue;
            //付款日计算类型判断:
            switch (mcCustomer.CalculateTermType)
            {
                case CalculateTermType.BillingDate:
                    paymentDate = dteAccountDate.DateTime;
                    break;
                case CalculateTermType.ETD:
                    paymentDate = _OperationCommonInfo.ETD;
                    break;
                case CalculateTermType.ETA:
                    paymentDate = _OperationCommonInfo.ETA;
                    break;
            }
            if (paymentDate == DateTime.MinValue)
            {
                return;
            }
            //根据月结设置设置付款日
            paymentDate = Convert.ToDateTime(paymentDate.ToShortDateString());
            DateTime dueDate = DateTime.MinValue;
            if (mcCustomer.PaymentDate > 0)
            {
                configPaymentDate = Convert.ToDateTime(string.Format("{0}-{1}-{2}", paymentDate.Year, paymentDate.Month, mcCustomer.PaymentDate));
                if(paymentDate>configPaymentDate)
                    configPaymentDate = configPaymentDate.AddMonths(1);
                paymentDate = configPaymentDate;
            }
            //账单日期在有效期外
            if(_CurrentData.AccountDate> paymentDate.AddDays(mcCustomer.CreditTerm))
            {
                //账单日作为付款日
                paymentDate = _CurrentData.AccountDate;
                //计入下个周期
                if (mcCustomer.PaymentDate > 0)
                {
                    configPaymentDate = Convert.ToDateTime(string.Format("{0}-{1}-{2}", paymentDate.Year, paymentDate.Month, mcCustomer.PaymentDate));
                    if (paymentDate > configPaymentDate)
                        configPaymentDate = configPaymentDate.AddMonths(1);
                    paymentDate = configPaymentDate;
                }
            }
            dueDate = paymentDate.AddDays(mcCustomer.CreditTerm);
            if (setDueDate) dteDueDate.DateTime = dueDate;
            int creditTerm = _CurrentData == null ? 0 : (dueDate - paymentDate).Days;
            seTrem.Value = (creditTerm < 0) ? 0 : creditTerm;
        }

        /// <summary>
        /// 复制当前行数据
        /// </summary>
        void ChargeClone()
        {
            if (CurrentCharge != null)
            {
                oldCharge = new ChargeList();
                oldCharge.Amount = CurrentCharge.Amount;
                oldCharge.BillAmount = CurrentCharge.BillAmount;
                oldCharge.BillID = CurrentCharge.BillID;
                oldCharge.BillNo = CurrentCharge.BillNo;
                oldCharge.BillUpdateDate = CurrentCharge.BillUpdateDate;
                oldCharge.ChargeName = CurrentCharge.ChargeName;
                oldCharge.ChargingCode = CurrentCharge.ChargingCode;
                oldCharge.ChargingCodeID = CurrentCharge.ChargingCodeID;
                oldCharge.ChargingDescription = CurrentCharge.ChargingDescription;
                oldCharge.CreateByID = CurrentCharge.CreateByID;
                oldCharge.CreateByName = CurrentCharge.CreateByName;
                oldCharge.CreateDate = CurrentCharge.CreateDate;
                oldCharge.CurrencyID = CurrentCharge.CurrencyID;
                oldCharge.CurrencyName = CurrentCharge.CurrencyName;
                oldCharge.FeeCode = CurrentCharge.FeeCode;
                oldCharge.FromType = CurrentCharge.FromType;
                oldCharge.ID = CurrentCharge.ID;
                oldCharge.InvoiceAmount = CurrentCharge.InvoiceAmount;
                oldCharge.IsAgent = CurrentCharge.IsAgent;
                oldCharge.IsAllowEdit = CurrentCharge.IsAllowEdit;
                oldCharge.IsCancel = CurrentCharge.IsCancel;
                oldCharge.IsCommission = CurrentCharge.IsCommission;
                oldCharge.IsSecondSale = CurrentCharge.IsSecondSale;
                oldCharge.IsVATInvoiced = CurrentCharge.IsVATInvoiced;
                oldCharge.PayAmount = CurrentCharge.PayAmount;
                oldCharge.Quantity = CurrentCharge.Quantity;
                oldCharge.Rate = CurrentCharge.Rate;
                oldCharge.Remark = CurrentCharge.Remark;
                oldCharge.Selected = CurrentCharge.Selected;
                oldCharge.Type = CurrentCharge.Type;
                oldCharge.UnitID = CurrentCharge.UnitID;
                oldCharge.UnitName = CurrentCharge.UnitName;
                oldCharge.UnitPrice = CurrentCharge.UnitPrice;
                oldCharge.UpdateDate = CurrentCharge.UpdateDate;
                oldCharge.Way = CurrentCharge.Way;

            }
        }

        #region SavingAndClose

        void BeginThreadInit()
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
        void SavingThreadStart(bool isCloseThis)
        {
            barlabMessage.Caption = LocalData.IsEnglish ? "Saving is in progress..." : "正在保存...";
            ThreadSaveData = new Thread(SavingAndClose);
            ThreadSaveData.Name = "SavingBooking";
            ThreadSaveData.Start(isCloseThis);
        }
        void SavingAndClose(object o)
        {
            try
            {
                ClientHelper.SetApplicationContext();
                bool isCloseThis = (bool)o;
                bool isSave = SaveData(false);
                TimerSaveData.Stop();
                if (isSave)
                {
                    barlabMessage.Tag = "Success";
                    barlabMessage.Caption =
                        string.Format((LocalData.IsEnglish ? "Saving is successful at {0}" : "保存成功 于 {0}"),
                            DateTime.Now.ToString("HH:mm:ss"));
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
            finally
            {
                barlabSeconds.Visibility = BarItemVisibility.Never;
            }
        }
        string saveState()
        {
            return barlabMessage.Tag == null ? "saving" : barlabMessage.Tag.ToString();
        }
        void SetLableMessage(string message, string state)
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
        void SetMoreVisible(bool p)
        {
            isShow = p;
            if (isShow)
            {
                panelInfo.Height = 211;
                panelMore.Height = 129;
                barMore.Caption = LocalData.IsEnglish ? "&Hide" : "隐藏";
            }
            else
            {
                panelInfo.Height = 100;
                panelMore.Height = 0;
                barMore.Caption = LocalData.IsEnglish ? "&More" : "更多";
            }
        }

        #endregion

        #region Save
        public bool SaveData(bool IsClosingEdit)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                StopwatchSaveData = StopwatchHelper.StartStopwatch();
                OperationLogID = Guid.NewGuid();

                StopwatchHelper.CustomRecordStopwatch(StopwatchSaveData, OperationLogID, DateTime.Now,
                        BaseFormID, "SAVE-BILL",
                        string.Format("账单保存;OperationID[{0}]Bill ID[{1}]", _CurrentData.OperationID, _CurrentData.ID));

                bool isAppcfmEvent = false;

                if (ValidateData() == false)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "保存失败:数据未通过验证");
                    return false;
                }

                if (cmbAREmail.Visible && string.IsNullOrEmpty(cmbAREmail.Text) && _CurrentData.Type == BillType.AR)
                {
                    MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "AR Email is null,fill in Please!" : "催放货联系人邮箱为空，请重新填写！");
                    cmbAREmail.Focus();
                    return false;
                }

                #region 海进、应付处理数据：
                if (_OperationCommonInfo.OperationType == OperationType.OceanImport && _CurrentData.Type == BillType.AP)
                {
                    foreach (var item in _CurrentData.Fees)
                    {
                        if ((_OperationCommonInfo.AgentOfCarrierID == null || _OperationCommonInfo.AgentOfCarrierID == Guid.Empty)
                            || (OCChargingCodeID.Contains(item.ChargingCodeID) && (Guid)_OperationCommonInfo.AgentOfCarrierID != _CurrentData.CustomerID))
                        {
                            string strocean = LocalData.IsEnglish ? "ICP recommend to set " + _CurrentData.CustomerName + " as the Carrier of the Shipment." : "是否把应付账单客户做为承运人?";

                            if (DialogResult.Yes == MessageBoxService.ShowQuestion(strocean, "", MessageBoxButtons.YesNo))
                            {
                                OceanImportService.ChangeOIMBLforCarrier(_OperationCommonInfo.OperationID, _CurrentData.CustomerID);
                                _OperationCommonInfo.AgentOfCarrierID = _CurrentData.CustomerID;
                            }
                            //判断承运人是否为空 为空弹出界面选择承运人
                            else if (_OperationCommonInfo.AgentOfCarrierID == null || _OperationCommonInfo.AgentOfCarrierID == Guid.Empty)
                            {
                                SearchConditionCollection searchList = new SearchConditionCollection();

                                searchList.AddWithValue("CustomerType", CustomerType.Forwarding, true);

                                IDataFinder finder = DataFinderFactory.GetDataFinder(CommonFinderConstants.CustomerAgentOfCarrierFinder);
                                finder.DataChoosed += billFinder_DataChoosed;
                                finder.PickMany(
                                    CommonFinderConstants.CustoemrFinder,
                                    SearchFieldConstants.CodeName,
                                    searchList,
                                    SearchFieldConstants.ResultValue,
                                    FinderTriggerType.KeyEnter,
                                    null,
                                    ClientConstants.MainWorkspace);
                            }
                            break;
                        }
                    }
                }
                #endregion

                #region 构建 费用列表参数
                List<Guid> rateCurrencyIDs = new List<Guid>();
                List<decimal> rateValues = new List<decimal>();

                if (_CurrentData.PayCurrencyId.IsNullOrEmpty() == false)
                {
                    List<CurrencyRateData> serateList = bsCurrencyRateData.DataSource as List<CurrencyRateData>;
                    if (serateList != null && serateList.Count > 0)
                    {
                        foreach (var item in serateList)
                        {
                            rateValues.Add(item.Rate);
                            rateCurrencyIDs.Add(item.CurrencyID);
                        }
                    }
                }
                #endregion

                #region 构建 费用列表参数
                List<Guid?> feeIDs = new List<Guid?>();
                List<FeeType> feeTypes = new List<FeeType>();
                List<FeeWay> feeWays = new List<FeeWay>();
                List<Guid> feeChargingCodeIDs = new List<Guid>(), feeCurrencyIDs = new List<Guid>(), feeUnitIDs = new List<Guid>();
                List<string> feeChargingDescriptions = new List<string>();
                List<decimal> feeRates = new List<decimal>(), feeQuantities = new List<decimal>(), feeUnitPrices = new List<decimal>(), feeAmounts = new List<decimal>();
                List<string> feeRemarks = new List<string>();
                List<DateTime?> feeUpdateDates = new List<DateTime?>();
                List<FormType> formTypes = new List<FormType>();

                List<bool> feeIsAgents = new List<bool>();
                List<bool> feeIsSecondSales = new List<bool>();
                List<bool> feeIsVATInvoiceds = new List<bool>();
                List<bool> feeIsGSTs = new List<bool>();
                List<Guid?> feeContainerIDs = new List<Guid?>();
                List<int?> feeFromType = new List<int?>();
                ///是否保存到费用新表
                List<bool> feeIsRevises = new List<bool>();

                if (_CurrentData.IsVATInvoiced)
                {
                    //如果需要开增值税发票
                    if (_CurrentData.Fees.Where(a => a.IsVATInvoiced).Count() > 0)
                    {
                        Dictionary<Guid, decimal> dic = new Dictionary<Guid, decimal>();
                        //已经存在增值税费用
                        if (_CurrentData.Fees.Where(a => a.ChargingCodeID == _ConfigureInfo.VATFeeID).Count() > 0)
                        {
                            if (_CurrentData.Fees.Where(a => a.IsDirty == true && a.ChargingCodeID == _ConfigureInfo.VATFeeID).Count() > 0)
                            {
                                DialogResult resu = FAMUtility.SaveVATchanges();
                                if (resu == DialogResult.Yes)
                                {
                                    goto Dirty;
                                }
                            }

                            ChargeList charge;

                            //分币种
                            if (chkSort.Checked == true)
                            {
                                foreach (var item in _CurrentData.Fees)
                                {
                                    if (item.IsVATInvoiced)
                                    {
                                        if (dic.Keys.Contains(item.CurrencyID) == false) dic.Add(item.CurrencyID, 0m);

                                        if (item.Way == FeeWay.AR)
                                        {
                                            dic[item.CurrencyID] += item.Amount;
                                        }
                                        else if (item.Way == FeeWay.AP)
                                        {
                                            dic[item.CurrencyID] -= item.Amount;
                                        }
                                    }

                                }
                                foreach (var item in dic)
                                {
                                    if (_CurrentData.Fees.Where(a => a.ChargingCodeID == (Guid)_ConfigureInfo.VATFeeID && a.CurrencyID == item.Key).Count() > 0)
                                    {
                                        charge = _CurrentData.Fees.Find(f => f.ChargingCodeID == (Guid)_ConfigureInfo.VATFeeID && f.CurrencyID == item.Key);
                                        charge.ChargeName = _ConfigureInfo.VATFeeName;

                                        if (item.Value > 0)
                                        {
                                            charge.Way = FeeWay.AR;
                                        }
                                        else
                                        {
                                            charge.Way = FeeWay.AP;
                                        }
                                        charge.Quantity = (Decimal)_CurrentData.Taxrate / 100;
                                        charge.CurrencyName = _ConfigureInfo.StandardCurrency;
                                        charge.ChargingCodeID = (Guid)_ConfigureInfo.VATFeeID;
                                        charge.CurrencyID = item.Key;
                                        charge.Rate = 1;
                                        charge.UnitPrice = Math.Abs(item.Value);
                                        charge.UnitID = _packTypes[0].ID;
                                        charge.Amount = decimal.Parse((Math.Abs(item.Value) * charge.Quantity).ToString("n"));
                                    }
                                    else
                                    {

                                        charge = new ChargeList();
                                        charge.Quantity = (Decimal)_CurrentData.Taxrate / 100;
                                        charge.CurrencyName = _ConfigureInfo.StandardCurrency;
                                        charge.ChargingCodeID = (Guid)_ConfigureInfo.VATFeeID;
                                        charge.FeeCode = _ConfigureInfo.VATFeeCode;
                                        charge.ChargingDescription = _ConfigureInfo.VATFeeName;
                                        charge.ChargingCode = _ConfigureInfo.VATFeeName;
                                        charge.CurrencyID = item.Key;
                                        charge.Rate = 1;
                                        charge.UnitPrice = Math.Abs(item.Value);
                                        charge.UnitID = _packTypes[0].ID;
                                        charge.IsAgent = false;
                                        charge.IsCommission = false;
                                        charge.IsSecondSale = false;
                                        charge.Amount = decimal.Parse((Math.Abs(item.Value) * charge.Quantity).ToString("n"));
                                        (bsChargeList.List as List<ChargeList>).Add(charge);
                                    }
                                }
                            }
                            else
                            {
                                ChargeList reportcharge = _CurrentData.Fees.Find(f => f.ChargingCodeID == (Guid)_ConfigureInfo.VATFeeID);
                                List<SolutionExchangeRateList> RateList = new List<SolutionExchangeRateList>();
                                RateList = ConfigureService.GetCompanyExchangeRateList(_CurrentData.CompanyID, true);
                                reportcharge.ChargeName = _ConfigureInfo.VATFeeName;
                                decimal total = 0m;
                                foreach (var item in _CurrentData.Fees)
                                {
                                    if (item.IsVATInvoiced)
                                    {
                                        decimal im = RateHelper.GetAmountByRate(item.Amount, item.CurrencyID, _ConfigureInfo.StandardCurrencyID, RateList, _CurrentData.AccountDate);
                                        if (item.Way == FeeWay.AR)
                                        {
                                            total += im;
                                        }
                                        else if (item.Way == FeeWay.AP)
                                        {
                                            total -= im;
                                        }
                                    }
                                }
                                total = Decimal.Round(total, 2);
                                if (total > 0)
                                {
                                    reportcharge.Way = FeeWay.AR;
                                }
                                else
                                {
                                    reportcharge.Way = FeeWay.AP;
                                }
                                reportcharge.Quantity = (Decimal)_CurrentData.Taxrate / 100;
                                reportcharge.CurrencyName = _ConfigureInfo.StandardCurrency;
                                reportcharge.ChargingCodeID = (Guid)_ConfigureInfo.VATFeeID;
                                reportcharge.CurrencyID = _ConfigureInfo.StandardCurrencyID;
                                reportcharge.Rate = 1;
                                reportcharge.UnitPrice = Math.Abs(total);
                                reportcharge.UnitID = _packTypes[0].ID;
                                reportcharge.Amount = decimal.Parse((Math.Abs(total) * reportcharge.Quantity).ToString("n"));

                            }
                        }
                        //新增
                        else
                        {
                            ChargeList reportcharge = new ChargeList();
                            //分币种开增值税
                            if (chkSort.Checked == true)
                            {
                                foreach (var item in _CurrentData.Fees)
                                {
                                    if (item.IsVATInvoiced)
                                    {
                                        if (dic.Keys.Contains(item.CurrencyID) == false) dic.Add(item.CurrencyID, 0m);

                                        if (item.Way == FeeWay.AR)
                                        {
                                            dic[item.CurrencyID] += item.Amount;
                                        }
                                        else if (item.Way == FeeWay.AP)
                                        {
                                            dic[item.CurrencyID] -= item.Amount;
                                        }
                                    }
                                }
                                foreach (var item in dic)
                                {
                                    reportcharge = new ChargeList();
                                    reportcharge.Quantity = (Decimal)_CurrentData.Taxrate / 100;
                                    reportcharge.CurrencyName = _ConfigureInfo.StandardCurrency;
                                    reportcharge.ChargingCodeID = (Guid)_ConfigureInfo.VATFeeID;
                                    reportcharge.FeeCode = _ConfigureInfo.VATFeeCode;
                                    reportcharge.ChargingDescription = _ConfigureInfo.VATFeeName;
                                    reportcharge.ChargingCode = _ConfigureInfo.VATFeeName;
                                    reportcharge.CurrencyID = item.Key;
                                    reportcharge.Rate = 1;
                                    reportcharge.UnitPrice = Math.Abs(item.Value);
                                    reportcharge.UnitID = _packTypes[0].ID;
                                    reportcharge.IsAgent = false;
                                    reportcharge.IsCommission = false;
                                    reportcharge.IsSecondSale = false;
                                    reportcharge.Amount = decimal.Parse((Math.Abs(item.Value) * reportcharge.Quantity).ToString("n"));
                                    //_CurrentData.Fees.Add(reportcharge);
                                    (bsChargeList.List as List<ChargeList>).Add(reportcharge);
                                }
                            }
                            else
                            {
                                List<SolutionExchangeRateList> RateList = new List<SolutionExchangeRateList>();
                                RateList = ConfigureService.GetCompanyExchangeRateList(_CurrentData.CompanyID, true);
                                reportcharge.ChargeName = _ConfigureInfo.VATFeeName;
                                decimal total = 0m;
                                foreach (var item in _CurrentData.Fees)
                                {
                                    if (item.IsVATInvoiced)
                                    {
                                        decimal im = RateHelper.GetAmountByRate(item.Amount, item.CurrencyID, _ConfigureInfo.StandardCurrencyID, RateList, _CurrentData.AccountDate);
                                        if (item.Way == FeeWay.AR)
                                        {
                                            total += im;
                                        }
                                        else if (item.Way == FeeWay.AP)
                                        {
                                            total -= im;
                                        }
                                    }
                                }
                                total = Decimal.Round(total, 2);

                                if (total > 0)
                                {
                                    reportcharge.Way = FeeWay.AR;
                                }
                                else
                                {
                                    reportcharge.Way = FeeWay.AP;
                                }
                                reportcharge.Quantity = (Decimal)_CurrentData.Taxrate / 100;
                                reportcharge.CurrencyName = _ConfigureInfo.StandardCurrency;
                                reportcharge.ChargingCodeID = (Guid)_ConfigureInfo.VATFeeID;
                                reportcharge.FeeCode = _ConfigureInfo.VATFeeCode;
                                reportcharge.ChargingDescription = _ConfigureInfo.VATFeeName;
                                reportcharge.ChargingCode = _ConfigureInfo.VATFeeName;
                                reportcharge.CurrencyID = _ConfigureInfo.StandardCurrencyID;
                                reportcharge.Rate = 1;
                                reportcharge.UnitPrice = Math.Abs(total);
                                reportcharge.UnitID = _packTypes[0].ID;
                                reportcharge.IsAgent = false;
                                reportcharge.IsCommission = false;
                                reportcharge.IsSecondSale = false;
                                reportcharge.Amount = decimal.Parse((Math.Abs(total) * reportcharge.Quantity).ToString("n"));
                                //_CurrentData.Fees.Add(reportcharge);
                                (bsChargeList.List as List<ChargeList>).Add(reportcharge);
                            }
                        }
                    }
                }
                Dirty:
                foreach (var item in _CurrentData.Fees)
                {
                    feeIDs.Add(item.ID);
                    feeTypes.Add(item.Type);
                    feeWays.Add(item.Way);
                    feeChargingCodeIDs.Add(item.ChargingCodeID);
                    feeChargingDescriptions.Add(item.ChargingDescription);
                    feeCurrencyIDs.Add(item.CurrencyID);
                    feeContainerIDs.Add(item.ContainerID);
                    feeUnitIDs.Add(item.UnitID);
                    feeRates.Add(item.Rate);
                    feeQuantities.Add(item.Quantity);
                    feeUnitPrices.Add(item.UnitPrice);
                    feeAmounts.Add(item.Amount);
                    feeRemarks.Add(item.Remark);
                    feeIsAgents.Add(item.IsAgent);
                    feeIsSecondSales.Add(item.IsSecondSale);
                    feeIsVATInvoiceds.Add(item.IsVATInvoiced);
                    feeIsGSTs.Add(item.IsGST);
                    feeUpdateDates.Add(item.UpdateDate);
                    feeFromType.Add(item.FromType == null ? (int)OperationType : item.FromType);
                    feeIsRevises.Add(false);

                }
                if (_CurrentData.ReviseHistoryFees != null && _CurrentData.ReviseHistoryFees.Count > 0)
                {
                    foreach (var item in _CurrentData.ReviseHistoryFees)
                    {
                        if (item != null && item.ChargingCodeID != null && item.ChargingCodeID != Guid.Empty)
                        {
                            feeIDs.Add(item.ID);
                            feeTypes.Add(item.Type);
                            feeWays.Add(item.Way);
                            feeChargingCodeIDs.Add(item.ChargingCodeID);
                            feeChargingDescriptions.Add(item.ChargingDescription);
                            feeCurrencyIDs.Add(item.CurrencyID);
                            feeContainerIDs.Add(item.ContainerID);
                            feeUnitIDs.Add(item.UnitID);
                            feeRates.Add(item.Rate);
                            feeQuantities.Add(item.Quantity);
                            feeUnitPrices.Add(item.UnitPrice);
                            feeAmounts.Add(item.Amount);
                            feeRemarks.Add(item.Remark);
                            feeIsAgents.Add(item.IsAgent);
                            feeIsSecondSales.Add(item.IsSecondSale);
                            feeIsVATInvoiceds.Add(item.IsVATInvoiced);
                            feeIsGSTs.Add(item.IsGST);
                            feeUpdateDates.Add(item.UpdateDate);
                            feeFromType.Add(item.FromType);
                            feeIsRevises.Add(true);
                        }
                    }
                }
                #endregion

                try
                {


                    #region 调用服务接口

                    HierarchyManyResult result = FinanceService.SaveBillInfo(
                        _OperationCommonInfo.OperationID
                        , _CurrentData.FormID
                        , _CurrentData.FormType
                        , _CurrentData.ID
                        , _CurrentData.CompanyID
                        , _CurrentData.CustomerID
                        , _CurrentData.ShipToID
                        , _CurrentData.CustomerDescription
                        , _CurrentData.CustomerRefNo
                        , _CurrentData.Type
                        , _CurrentData.AccountDate
                        , _CurrentData.DueDate
                        , _CurrentData.PayCurrencyId
                        , _CurrentData.AuditorID
                        , _CurrentData.AuditorEmail
                        , _CurrentData.State
                        , _CurrentData.No
                        , _OperationCommonInfo.OperationDate
                        , _CurrentData.FromType
                        , rateCurrencyIDs.ToArray()
                        , rateValues.ToArray()
                        , _CurrentData.Remark
                        , _CurrentData.IsVATInvoiced
                        , _CurrentData.Taxrate
                        , _CurrentData.UpdateDate
                        , feeIDs.ToArray()
                        , feeWays.ToArray()
                        , feeTypes.ToArray()
                        , feeIsAgents.ToArray()
                        , feeIsSecondSales.ToArray()
                        , feeIsVATInvoiceds.ToArray()
                        , feeIsGSTs.ToArray()
                        , feeChargingCodeIDs.ToArray()
                        , feeChargingDescriptions.ToArray()
                        , feeCurrencyIDs.ToArray()
                        , feeRates.ToArray()
                        , feeContainerIDs.ToArray()
                        , feeUnitIDs.ToArray()
                        , feeUnitPrices.ToArray()
                        , feeQuantities.ToArray()
                        , feeAmounts.ToArray()
                        , feeRemarks.ToArray()
                        , feeUpdateDates.ToArray()
                        , feeFromType.ToArray()
                        , feeIsRevises.ToArray()
                        , IsClosingEdit
                        , isAppcfmEvent
                        , LocalData.UserInfo.LoginID
                        );

                    #endregion

                    List<CustomerCarrierObjects> DataSourceList = new List<CustomerCarrierObjects>();
                    CustomerCarrierObjects newCustomer = new CustomerCarrierObjects();

                    string check = "^([a-z0-9A-Z_]+[-|.]?)+[a-z0-9A-Z]@([a-z0-9A-Z]+(-[a-z0-9A-Z]+)?.)+[a-zA-Z]{2,}$";
                    Regex reg = new Regex(check);

                    if (!string.IsNullOrEmpty(cmbAREmail.Text) && cmbAREmail.Visible)
                    {
                        string[] maillist = cmbAREmail.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (maillist != null && maillist.Length > 0)
                        {
                            foreach (string mail in maillist)
                            {
                                if (reg.IsMatch(mail))
                                {
                                    newCustomer = cusList.Find(r => r.Mail.ToUpper().Trim() == mail.ToUpper().Trim());
                                    if (newCustomer == null)
                                    {
                                        newCustomer = new CustomerCarrierObjects();
                                        newCustomer.Release = false;
                                        newCustomer.AR = true;
                                        newCustomer.CreateByID = LocalData.UserInfo.LoginID;
                                        newCustomer.Name = mail.Substring(0, mail.IndexOf('@'));
                                        newCustomer.Mail = mail;
                                        newCustomer.OceanBookingID = _OperationCommonInfo.OperationID;
                                        newCustomer.OperationType = _OperationCommonInfo.OperationType;
                                        newCustomer.Type = ContactType.Customer;
                                        newCustomer.BillID = null;
                                        newCustomer.CustomerID = _CurrentData.CustomerID;

                                        DataSourceList.Add(newCustomer);
                                    }
                                }
                                else
                                {
                                    MessageBoxService.ShowWarning(LocalData.IsEnglish ? mail + ":The mailbox format is out of specification"
                                             : mail + "：邮箱格式不符合规范", "Tips", MessageBoxButtons.OK);
                                    continue;
                                }
                            }
                        }
                    }

                    if (DataSourceList.Count > 0)
                    {
                        FCMCommonService.SaveContactList(DataSourceList);
                    }

                    ContactObjects dataList = FCMCommonService.GetContactList(_OperationCommonInfo.OperationID, OperationType.OceanExport);
                    if (dataList != null && dataList.CustomerCarrier != null)
                    {
                        cusList = dataList.CustomerCarrier;
                    }

                    #region 把返回值设置到;当前对象 和 更新币种帐单列表

                    _CurrentData.ID = result.GetValue<Guid>("ID");
                    _CurrentData.No = result.GetValue<string>("No");
                    _CurrentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    _ApplyState = result.GetValue<int>("ApplyState");
                    for (int i = 0; i < _CurrentData.Fees.Count; i++)
                    {
                        _CurrentData.Fees[i].ID = result.Childs[i].GetValue<Guid>("ID");
                        _CurrentData.Fees[i].UpdateDate = result.Childs[i].GetValue<DateTime?>("UpdateDate");
                        _CurrentData.Fees[i].IsAllowEdit = false;
                    }

                    #endregion

                    string ChargeCodes = "";
                    if (txtFreightIncluded.Tag != null)
                    {
                        List<Guid> listChargeCode = txtFreightIncluded.Tag as List<Guid>;
                        List<string> strChargeCode = new List<string>();
                        foreach (var item in listChargeCode)
                        {
                            strChargeCode.Add(item.ToString());
                        }

                        ChargeCodes = string.Join(",", strChargeCode.ToArray());
                    }


                    FinanceService.UpdateOperationFreightIncluded(_CurrentData.OperationID, ChargeCodes,
                        _CurrentData.OperationType);

                    AfterSaveData();
                    //bsBillInfo.ResetCurrentItem();
                    bsChargeList.ResetBindings(false);
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, "账单保存成功");
                    return true;
                }
                catch (Exception ex)
                {
                    StopwatchHelper.CustomUpdateStopwatchLog(StopwatchSaveData, OperationLogID, false, string.Format("账单保存失败SessionId:[{0}]", LocalData.SessionId));
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), string.Format("{0}\r\nOperationLogID[{1}]", ex.Message, OperationLogID));
                    return false;
                }
            }
        }

        void AfterSaveData()
        {
            if (_CurrentData.ReviseHistoryFees != null)
            {
                _CurrentData.ReviseHistoryFees.Clear();
            }

            _CurrentData.CancelEdit();
            _CurrentData.BeginEdit();
            rdoGroupType.Enabled = false;
            if (Saved != null) Saved(new object[] { _CurrentData.ID });

            //添加修改账单时提示用户
            bool state = (OperationType == OperationType.OceanExport || OperationType == OperationType.OceanImport) && _CurrentData.Type == BillType.DC ? true : false;
            if (state && StartTime < OICreateDate)
            {

                switch (_ApplyState)
                {
                    case 1:
                        if (DialogResult.Yes == MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Saving can't be executed. Because D/C Fees have been revised by the agent,\t\n  you must accept the revised fees first. \t\n  Clicks Yes to enter the page [Accept Revised D/C Fees From The Agent] \t\n  Clicks No to continue to enter the page [Account Info]"
                               : "代理账单不能被保存，因为代理已经修订了代理账单费用，您必须先签收此次修订。\t\n 单击[Yes]进入签收修订页面。\t\n 单击[NO]继续打开账单页面。", "", MessageBoxButtons.YesNo))
                        {
                            FCM.Common.UI.FCMUIUtility.ShowReviseAccepte(Workitem, _CurrentData.OperationID);
                        }
                        break;
                    case 2:
                        FAMUtility.ShowMessage(LocalData.IsEnglish ? "Saving the D/C fees  is successful. You must re-dispatch Docs to the agent in time."
                              : "您已成功修改了代理账单，您必须及时地向代理手动再次分发文件(Docs)。");
                        break;
                    case 3:
                        if (DialogResult.Yes == MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Saving can't be executed. Because  the agent have re-dispatched Docs,\t\n you must accept the dispatching first. \t\n Clicks Yes to enter the page [Accept Dispatched Docs From The Agent] \t\n Clicks No to continue to enter the page [Account Info]"
                           : "代理账单不能被保存，因为代理已经重新分发了文档，您必须先签收此次分发。换行符 单击[Yes]进入签收代理分发文档页面。换行符 单击[NO]继续打开账单页面。",
                           "", MessageBoxButtons.YesNo))
                        {
                            FCM.Common.UI.FCMUIUtility.ShowAcceptedDocumentCompareNew(Workitem, Guid.Empty, _CurrentData.OperationID, false);
                        }
                        break;
                    case 4:
                        MessageBoxService.ShowQuestion(LocalData.IsEnglish ?
                         " Saving the D/C fees  is successful. The revised fees will be sent to the agent automatically（DCRev）.\t\n Note: After the agent accept the revised fees（DCRcv）, the D/C fees are consistent with the agent."
                         : "您已成功修改了代理账单，系统已自动发送修订的费用给代理（DCRev）。\t\n注：代理签收了您修订的费用后（DCRcv），双边代理的账单将会即时一致。");
                        break;
                    case 5:
                        FAMUtility.ShowMessage(LocalData.IsEnglish ? "Saving the D/C fees  is successful. You must re-dispatch Docs to the agent in time."
                              : "您已成功修改了代理账单，您必须及时地向代理手动再次分发文件(Docs)。");
                        break;
                    case 6:
                        MessageBoxService.ShowQuestion(LocalData.IsEnglish ?
                         " Saving the D/C fees  is successful. The revised fees will be sent to the agent automatically（DCRev）.\t\n Note: After the agent accept the revised fees（DCRcv）, the D/C fees are consistent with the agent."
                         : "您已成功修改了代理账单，系统已自动发送修订的费用给代理（DCRev）。\t\n注：代理签收了您修订的费用后（DCRcv），双边代理的账单将会即时一致。");
                        break;

                    case 8:
                        MessageBoxService.ShowQuestion(LocalData.IsEnglish ?
                         " With the final revised fees, ICP will cancel the DCRev request.\t\nNote：Because now the current fees are not different with the agent's."
                         : "根据最终修改的费用，ICP将取消发给代理的修订请求。\t\n注：因为当前费用已经与代理的费用一致。");
                        break;
                    case 9:
                        MessageBoxService.ShowQuestion(LocalData.IsEnglish ?
                         " With the final revised fees, ICP will cancel the DocS request.\t\nNote：Because now the current fees are not different with the agent's."
                         : "根据最终修改的费用，ICP将取消发给代理的自动的分发请求。\t\n注：因为当前费用已经与代理的费用一致。");
                        break;
                    default:
                        break;
                }
            }

            if (OperationType == OperationType.OceanImport && StartTime > OICreateDate && _CurrentData.Type == BillType.DC)
            {
                MessageBoxService.ShowQuestion(LocalData.IsEnglish ?
                  "ICP would not send to the agent  with the notice of revised D/C fees which downloaded before 11/11 (not include 11/11).\t\n You must notify D/C fees manually to the agent by mail."
                  : "对于11/11(不包含11/11)之前下载的业务，系统不会发送修订费用通知给代理。\t\n您必须手动发邮件通知代理进行更改代理费用。");
            }
            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
        }

        bool ValidateData()
        {
            EndEdit();

            //if (_CurrentData == null || this.panelEdit.Enabled == false) return false;
            bool isSrcc = true;

            if (_CurrentData.IsVATInvoiced == true && (_CurrentData.Taxrate == 0m || _CurrentData.Taxrate == null))
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "VAT rate is invalid" : "增值税率输入有误.");
                isSrcc = false;
            }

            //业务时间已经关帐   允许调整计费时间但是 计费时间不能小于关帐时间
            if (_OperationCommonInfo.OperationDate < _ConfigureInfo.ChargingClosingdate && _CurrentData.AccountDate < _ConfigureInfo.ChargingClosingdate)
            {
                DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Account has been closed.Closing date is" + _ConfigureInfo.ChargingClosingdate.ToString() : "业务已经关帐，计费时间不能小于关帐时间"
                             , LocalData.IsEnglish ? "Tip" : "提示"
                             , MessageBoxButtons.OK);

                return false;
            }


            //if (this.OperationType == OperationType.OceanImport && _CurrentData.AgentID == _CurrentData.CustomerID)
            //{
            //    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), LocalData.IsEnglish ? "For the new regualtion of Agent Fees.All of agent fees can be only revised by POL Agent. If POD Agent wanted to resived it, POD Agent shall Email the new fees to POL Agent first, POL Agent would re-fill in fees into ICP and re-dipatch them to POD Agent."
            //        : "关于代理费用的新规定.所有的代理费用只能由起运港代理修改。如果目的港代理需要变更费用，请通过邮件跟起运港代理沟通，运港代理沟通会将新费用重新输入到ICP，并重新分发文件。");
            //    isSrcc = false;
            //}


            if (_CurrentData.AgentID != null && _CurrentData.AgentID != Guid.Empty)
            {
                if (_CurrentData.CustomerID == _CurrentData.AgentID && _CurrentData.Type != BillType.DC)
                {
                    DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The agent " + _CurrentData.CustomerName + " can not be filled in AR/AP fees. Please re-create D/C fees with the agent..Do you want to continue?" : "账单客户" + _CurrentData.CustomerName + "为代理，账单类型应为代理账单！是否继续？"
                               , LocalData.IsEnglish ? "Tip" : "提示"
                               , MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                }

                if (_CurrentData.Type == BillType.DC && _CurrentData.CustomerID != _CurrentData.AgentID)
                {
                    DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "The agent " + _CurrentData.CustomerName + " can be filled only in DC fees. Otherwise please re-create AR/AP fees with the customer " + _CurrentData.CustomerName + ".Do you want to continue?" : "账单类型为代理账单，账单客户与代理不一致！是否继续?"
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            if (_CurrentData.Validate
                (
                    delegate (ValidateEventArgs e)
                    {
                        if (_CurrentData.FormID.IsNullOrEmpty())
                        {
                            e.SetErrorInfo("FormID", LocalData.IsEnglish ? "At least associated with a manifest." : "至少关联一个联单");
                        }
                        //IF当前业务.离港日 >= 公司配置.计费关账日 AND 账单. 计费日期 <=公司配置.计费关账日 Then 该账单的业务已在计费关账期[xxxxxxx]内，所以计费日期只能大于计费关账期。
                        if (_OperationCommonInfo.OperationDate >= _ConfigureInfo.ChargingClosingdate && _CurrentData.AccountDate <= _ConfigureInfo.ChargingClosingdate)
                        {
                            e.SetErrorInfo("AccountDate", (LocalData.IsEnglish ? "The business of the bill has been off in the Charging Closing date [" : "该账单的业务已在计费关账期[") + _ConfigureInfo.ChargingClosingdate.Value.ToShortDateString() + (LocalData.IsEnglish ? "], so the billing date can only be greater than the Charging Closing date" : "]内，所以计费日期只能大于计费关账期"));
                        }
                    }
                ) == false)
                isSrcc = false;

            List<ChargeList> chargeList = bsChargeList.DataSource as List<ChargeList>;
            if (chargeList == null || chargeList.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), LocalData.IsEnglish ? "Enter at least one fee" : "至少输入一条费用");
                return false;
            }

            foreach (var item in chargeList)
            {
                if (item.Validate
                    (
                        delegate (ValidateEventArgs e)
                        {

                            decimal de = decimal.Parse((item.Quantity * item.UnitPrice).ToString("n"));
                            if (item.Amount != decimal.Parse((item.Quantity * item.UnitPrice).ToString("n")))
                            {
                                e.SetErrorInfo("Amount", LocalData.IsEnglish ? "The amount is invalid." : "金额输入有误.");
                            }

                            if (item.Rate == 0m)
                            {
                                e.SetErrorInfo("Rate", LocalData.IsEnglish ? "Exchange rate is invalid." : "汇率输入有误.");
                            }
                        }
                    ) == false)
                    isSrcc = false;
            }
            //// 如果帐单有但是费用没有不能保存（只针对公司配置中有开增值税发票的公司）
            if (_ConfigureInfo.IsVATinvoice && _CurrentData.IsVATInvoiced)
            {
                if (chargeList.Where(a => a.IsVATInvoiced == true).Count() < 1)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(
                                   null,
                                   LocalData.IsEnglish ? "There is no fee that need  to open the VAT invoice" : "没有费用需要开增值税发票！"
                                   );
                    isSrcc = false;
                }
            }

            if (_CurrentData.Type == BillType.DC && cmbRefNo.SelectedItem == null)
            {

                LocalCommonServices.ErrorTrace.SetErrorInfo(
                  null,
                  LocalData.IsEnglish ? "Please select Ref. NO!" : "请选择参考号！"
                  );
                isSrcc = false;
            }

            return isSrcc;
        }

        #endregion

        #region Fee

        void NewFee()
        {
            NewFee(true);
        }
        void NewFee(bool direction)
        {
            //ChargeList preRow = null;
            //if (gvChargeList.RowCount > 0)
            //    preRow = gvChargeList.GetRow(gvChargeList.RowCount - 1) as ChargeList;

            //ChargeList newFeeRow;

            //if (preRow != null)
            //    newFeeRow = Utility.Clone<ChargeList>(preRow);
            //else
            //{
            ChargeList newFeeRow = new ChargeList();
            if (LastCharge != null && !FAMUtility.GuidIsNullOrEmpty(LastCharge.CurrencyID))
            {
                newFeeRow.CurrencyID = LastCharge.CurrencyID;
                newFeeRow.CurrencyName = LastCharge.CurrencyName;
            }
            else
            {
                newFeeRow.CurrencyID = _ConfigureInfo.DefaultCurrencyID;
                newFeeRow.CurrencyName = _ConfigureInfo.DefaultCurrency;
            }
            newFeeRow.Quantity = 1;

            newFeeRow.UnitID = _packTypes[0].ID;
            newFeeRow.UnitName = LocalData.IsEnglish ? _packTypes[0].EName : _packTypes[0].CName;

            //newFeeRow.Rate = 1;
            newFeeRow.Rate = RateHelper.GetRate(_ConfigureInfo.DefaultCurrencyID, _ConfigureInfo.StandardCurrencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), _RateList);

            if (_CurrentData.Type == BillType.DC)
            {
                if (direction == true)
                    newFeeRow.Way = FeeWay.AR;
                else
                    newFeeRow.Way = FeeWay.AP;

                if (OperationType == OperationType.OceanExport || OperationType == OperationType.OceanImport)
                {
                    if (OperationAgentService.BusinessIsDownLoad(_CurrentData.OperationID) && IsFirstNewAgent)
                    {
                        IsFirstNewAgent = false;
                        MessageBoxService.ShowQuestion(LocalData.IsEnglish ?
                            "For append new D/C fees, you should negotiate with the agent via email first before filling-in the D/C fees in ICP."
                             + "\t\n Otherwise the agent might refuse the new D/C fees." :
                            "您最好先通过邮件与代理商讨是否接受新的费用，妥协后在ICP输入新费用，否则代理可能拒绝签收费用。");
                    }

                }
            }
            else
            {
                if (LastCharge != null) newFeeRow.Way = LastCharge.Way;
                else
                {
                    if (_CurrentData.Type == BillType.AP)
                    {
                        newFeeRow.Way = FeeWay.AP;
                    }
                    else if (_CurrentData.Type == BillType.AR)
                    {
                        newFeeRow.Way = FeeWay.AR;
                    }
                }
            }

            //费用增加是否开增值税发票并且默认与帐单一致
            if (_CurrentData.IsVATInvoiced)
            {
                newFeeRow.IsVATInvoiced = true;
            }
            newFeeRow.ID = Guid.Empty;
            newFeeRow.UnitID = new Guid("E4143DCD-A8F3-E011-8ED9-001321CC6D9F");
            newFeeRow.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            newFeeRow.CreateByID = LocalData.UserInfo.LoginID;
            newFeeRow.CreateByName = LocalData.UserInfo.LoginName;
            if (_DefaultFromData == null)
            {
                newFeeRow.Quantity = 1;
            }
            else
            {
                newFeeRow.Quantity = _DefaultFromData.CntQty;
            }
            newFeeRow.Amount = newFeeRow.Quantity * newFeeRow.UnitPrice;

            if (_CurrentData.Type == BillType.DC) newFeeRow.IsAgent = true;

            newFeeRow.FromType = (int)OperationType;

            (bsChargeList.List as List<ChargeList>).Add(newFeeRow);
            bsChargeList.ResetBindings(false);
            if (bsChargeList.Count > 0)
                bsChargeList.Position = bsChargeList.Count - 1;
            gcChargeList.Focus();

            gvChargeList.ClearSelection();
            gvChargeList.SelectCell(bsChargeList.Position, colChargingCode);
            gvChargeList.FocusedColumn = colChargingCode;
            _CurrentData.IsDirty = true;
        }

        void RemoveFee()
        {
            if (CurrentCharge == null || gvChargeList.FocusedRowHandle < 0) return;

            //if (CurrentCharge.IsDispatch)
            //{
            //    MessageBoxService.ShowQuestion("Test");
            //    return;

            //}

            int[] selectRowsHandle = gvChargeList.GetSelectedRows();

            if (selectRowsHandle == null || selectRowsHandle.Length == 0) return;

            bool isDelete = true;
            for (int i = 0; i < selectRowsHandle.Length; i++)
            {
                int row = selectRowsHandle[i] - i;

                ChargeList chargeItem = gvChargeList.GetRow(row) as ChargeList;

                if (chargeItem == null)
                {
                    continue;
                }
                if (chargeItem.Type == FeeType.Price || chargeItem.Type == FeeType.Rebate)
                {
                    isDelete = false;
                }
            }
            if (isDelete)
            {
                gvChargeList.DeleteSelectedRows();
            }
            else
            {
                //删除失败，无法删除由合约产生的费用
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "DeleteChargeError"));

            }


            _CurrentData.IsDirty = true;
        }
        #endregion
        #endregion

        #region IEditPart 成员


        void BindingData(object data)
        {
            //关帐后保存按钮默认不显示
            barAccountClos.Visibility = BarItemVisibility.Never;

            if (data == null)
            {
                Enabled = false;
                bsBillInfo.DataSource = typeof(BillInfo);
                bsChargeList.DataSource = typeof(ChargeList);
                lblReviseTip.Visible = false;
                barReviseFee.Visibility = BarItemVisibility.Never;

            }
            else
            {
                cmbCustomer.EditValueChanged -= cmbCustomer_EditValueChanged;
                BillInfo source = data as BillInfo;
                currentOperationID = source.OperationID;
                InitByNewData(source);
                bsBillInfo.DataSource = source;
                bsChargeList.DataSource = null;
                bsChargeList.DataSource = source.Fees;
                if (source.ReviseHistoryFees != null) source.ReviseHistoryFees.RemoveAll((ChargeList item) => item == null);

                bsReviseChargeList.DataSource = source.ReviseHistoryFees;
                cmbRefNo.Properties.Items.Clear();
                cmbCustomer.Enabled = true;
                barRemove.Enabled = true;
                barNewFee.Enabled = true;
                if (source.Type == BillType.DC)
                {

                    if (OperationType == OperationType.OceanImport && source.ID != Guid.Empty)
                    {
                        cmbCustomer.Enabled = false;
                    }

                    DataTable dt = FinanceService.GetValidReNos(_OperationCommonInfo.OperationID);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            cmbRefNo.Properties.Items.Add(new ImageComboBoxItem(item[1].ToString(), item[0]));
                        }
                    }
                }

                if (cmbRefNo.Properties.Items.Count == 0)
                {
                    foreach (var item in _OperationCommonInfo.Forms)
                    {

                        cmbRefNo.Properties.Items.Add(new ImageComboBoxItem(item.No, item.ID));
                    }
                }

                //控制账单界面 增值税率
                if (_ConfigureInfo.IsVATinvoice == false)
                {
                    chkVATinvoice.Checked = false;
                    chkVATinvoice.Enabled = false;
                    cetxtTaxrate.Enabled = false;
                    colIsInvoicedVAT.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    cetxtTaxrate.Enabled = source.IsVATInvoiced == true;
                    chkVATinvoice.Checked = source.IsVATInvoiced;
                }

                InitTradeCustomers();
                InitBillType();
                InitCurrencyRate();
                InitCustomerPopup(source.Type);
                Enabled = true;
                ((BaseDataObject)data).CancelEdit();
                ((BaseDataObject)data).BeginEdit();

                if (LocalCommonServices.PermissionService.HaveActionPermission(ActionsConstants.FAM_TREMMANAGER))
                {
                    seTrem.Properties.ReadOnly = false;
                }
                else
                {
                    seTrem.Properties.ReadOnly = true;
                }

                if (_AgentBillCheckStatusEnum == AgentBillCheckStatusEnum.Checking || _AgentBillCheckStatusEnum == AgentBillCheckStatusEnum.StartCheck)
                {
                    barSave.Enabled = false;
                    barSavingClose.Enabled = false;
                    barSaveCustomerRefNo.Visibility = BarItemVisibility.Always;
                    barSave.Caption = LocalData.IsEnglish ? "Reconciliation is not allowed to modify the bill." : "对帐中,不允许修改帐单.";
                }
                else if ((short)_CurrentData.State >= (short)BillState.Approved)
                {
                    barSave.Enabled = false;
                    barSavingClose.Enabled = false;
                    barSaveCustomerRefNo.Visibility = BarItemVisibility.Always;
                    barSave.Caption = LocalData.IsEnglish ? "The current bill has been reviewed, not allowed to modify the bill." : "当前帐单已审核,不允许修改帐单.";
                }
                else if (_CurrentData.AccountDate < _ConfigureInfo.ChargingClosingdate)
                {
                    //2014-10-28 周任平 将关帐日由会计关帐日改为计费关收日
                    barSave.Enabled = false;
                    barSavingClose.Enabled = false;
                    barSaveCustomerRefNo.Visibility = BarItemVisibility.Always;
                    barSave.Caption = (LocalData.IsEnglish ? "Billing date of the bill in the Accounting Closing date [" : "帐单计费日期在计费关账期[") + _ConfigureInfo.ChargingClosingdate.Value.ToShortDateString() + (LocalData.IsEnglish ? "] is not allowed to change the bill" : "]内 , 不允许更改帐单");
                    if (LocalCommonServices.PermissionService.HaveActionPermission(COMEditPer))
                    {
                        barAccountClos.Visibility = BarItemVisibility.Always;
                    }
                }

                else
                {
                    barSave.Enabled = true;
                    barSavingClose.Enabled = true;
                    barSaveCustomerRefNo.Visibility = BarItemVisibility.Never;
                    barSave.Caption = LocalData.IsEnglish ? "&Save" : "保存(&S)";
                }

                //帐单对应业务下的业务时间 < 财务关账时间，那么帐单计费时间可以更改，但是不可以计费时间<= 计费关账时间
                if (_CurrentData.IsNew && _OperationCommonInfo.OperationDate < _ConfigureInfo.ChargingClosingdate)
                {
                    dteAccountDate.Properties.ReadOnly = false;
                    dteAccountDate.EditValueChanged += dteAccountDate_EditValueChanged;
                }
                else
                {
                    dteAccountDate.Properties.ReadOnly = true;
                    dteAccountDate.EditValueChanged -= dteAccountDate_EditValueChanged;
                }

                if (!_CurrentData.IsNew && _OperationCommonInfo.OperationDate < _ConfigureInfo.ChargingClosingdate && _CurrentData.AccountDate > _ConfigureInfo.ChargingClosingdate)
                {
                    dteAccountDate.Properties.ReadOnly = false;
                    dteAccountDate.EditValueChanged += dteAccountDate_EditValueChanged;
                }

                if (source.Type == BillType.DC)
                {
                    barNewFee.Visibility = BarItemVisibility.Never;
                    barNewARFee.Visibility = BarItemVisibility.Always;
                    barNewAPFee.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    barNewFee.Visibility = BarItemVisibility.Always;
                    barNewARFee.Visibility = BarItemVisibility.Never;
                    barNewAPFee.Visibility = BarItemVisibility.Never;
                }

                if (source.Fees == null || source.Fees.Count == 0)
                {
                    lblReviseTip.Visible = false;
                    barReviseFee.Visibility = BarItemVisibility.Never;
                }
                if (source.IsNew) cmbCustomer.Focus();
                else gcChargeList.Focus();
                billIsDispacth = false;
                foreach (ChargeList fee in source.Fees)
                {
                    if (fee.IsDispatch)
                    {
                        billIsDispacth = true;
                        break;
                    }
                }
                if (StartTime < OICreateDate && source.Type == BillType.DC)
                {
                    cmbRefNo.Enabled = !billIsDispacth;
                }
                else
                {
                    cmbRefNo.Enabled = true;
                }

                if (RefereshBillService.RefereshBillDelete != null)
                    RefereshBillService.RefereshBillDelete(barRemove.Enabled && !billIsDispacth);

                MonthlyClosingCustomer tager = GetMonthlyClosingCustomer();
                SetCreditTermAndDueDate(tager, false);
                //seTrem.Value = (source.DueDate.Date - source.AccountDate.Date).Days;

                FreightIncludedInfo freightIncludedInfo = new FreightIncludedInfo();

                freightIncludedInfo = FinanceService.GetOperationFreightIncluded(_CurrentData.OperationID, _CurrentData.OperationType);

                if (freightIncludedInfo != null && !string.IsNullOrEmpty(freightIncludedInfo.FreightIncludedCodes))
                {
                    txtFreightIncluded.Text = freightIncludedInfo.FreightIncludedCodes;
                }
            }
            Setvisible(data as BillInfo);
            RefreshBillTatol();
        }

        void InitByNewData(BillInfo source)
        {
            if (source.IsNew)
            {
                source.Fees = new List<ChargeList>();
                rdoGroupType.Enabled = true;

                if (_DefaultFromData != null)
                {
                    source.FormNo = _DefaultFromData.No;
                    source.FormID = _DefaultFromData.ID;
                    source.FormType = _DefaultFromData.Type;

                    if (source.Type == BillType.DC && source.FormType != FormType.HBL)
                    {
                        foreach (var item in _OperationCommonInfo.Forms)
                        {
                            if (item.Type == FormType.HBL)
                            {
                                source.FormID = item.ID;
                                source.FormNo = item.No;
                                break;
                            }
                        }
                    }
                }

            }
            else
            {
                rdoGroupType.Enabled = false;
            }

            gcReviseFeeHistory.Visible = false;
        }
        public override object DataSource
        {
            get
            {
                return bsBillInfo.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }

        public override event SavedHandler Saved;

        public override void EndEdit()
        {
            Validate();
            bsBillInfo.EndEdit();
            bsChargeList.EndEdit();
            bsCurrencyRateData.EndEdit();
        }

        #endregion

        #region IPart 成员
        

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key == "BLCommonInfo")
                {
                    _OperationCommonInfo = item.Value as OperationCommonInfo;
                    foreach (var customer in _OperationCommonInfo.TradeCustomers)
                    {
                        if (customer.IsAgent) { _AgentCustoemr = customer; break; }
                    }
                }
                else if (item.Key == "ConfigureInfo")
                {
                    _ConfigureInfo = item.Value as ConfigureInfo;
                }
                else if (item.Key == "SolutionExchangeRateList")
                {
                    _RateList = item.Value as List<SolutionExchangeRateList>;
                }
                else if (item.Key == "SolutionCurrencyList")
                {
                    _CurrencyList = item.Value as List<SolutionCurrencyList>;
                }
                else if (item.Key == "DefaultFromData")
                {
                    _DefaultFromData = item.Value as FormData;

                }
                else if (item.Key == "AgentBillCheckStatus")
                {
                    _AgentBillCheckStatusEnum = (AgentBillCheckStatusEnum)Enum.Parse(typeof(AgentBillCheckStatusEnum), item.Value.ToString());
                }
            }
            OICreateDate = OperationAgentService.GetCreateDateOIBusiness(_OperationCommonInfo.OperationID);
        }
        #endregion

        #region Commands

        #region 运费付乞流程

        [CommandHandler(CustomerBillCommands.Commond_PayoffWF)]
        public void Commond_PayoffWF(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentData.IsNew || _CurrentData.Type == BillType.AR) return;

                if (_CurrentData.IsDirty)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "请先保存数据.");
                    return;
                }

                BusinessList business = FinanceService.GetBusinessListByIDs(new Guid[] { _OperationCommonInfo.OperationID }).GetList<BusinessList>()[0];
                ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(_CurrentData.CompanyID);


                PaymentFreightPart payFreightPart = Workitem.Items.AddNew<PaymentFreightPart>();
                PaymentFreightItem paymentFreight = new PaymentFreightItem();//数据库接口实现

                #region Amount

                Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
                decimal amount = 0m;
                foreach (var item in _CurrentData.Fees)
                {
                    decimal itemAmount = item.Way == FeeWay.AP ? item.Amount : -item.Amount;
                    if (item.Rate > 0)
                    {
                        amount = amount + (itemAmount * item.Rate);
                    }
                    else
                    {
                        amount += itemAmount;
                    }
                    if (dic.ContainsKey(item.CurrencyName) == false)
                    {
                        dic.Add(item.CurrencyName, 0m);
                    }
                    dic[item.CurrencyName] += itemAmount;
                }
                paymentFreight.Amount = amount.ToString("n");

                StringBuilder strBulid = new StringBuilder();
                foreach (var item in dic)
                {
                    strBulid.Append(item.Key + ":" + item.Value.ToString("n") + " ");
                }

                paymentFreight.OriginalityAmount = strBulid.ToString();
                #endregion

                #region
                paymentFreight.CustomerName = business.CustomerName;
                paymentFreight.GoodsNmae = business.GoodsNmae;
                if (configureInfo != null)
                {
                    if (LocalData.IsEnglish)
                    {
                        paymentFreight.CurrencyName = "Amount(" + configureInfo.StandardCurrency + ")";
                    }
                    else
                    {
                        paymentFreight.CurrencyName = "金额(" + configureInfo.StandardCurrency + ")";
                    }
                    //string ename = "Amount(" + CurrentData.CurrencyName + ")";
                    //string cname = "金额(" + CurrentData.CurrencyName + ")";
                    //paymentFreight.CurrencyName = configureInfo.StandardCurrency;
                }

                paymentFreight.BillToID = _CurrentData.CustomerID;
                paymentFreight.BillToName = _CurrentData.CustomerName;
                paymentFreight.BillNo = _CurrentData.No;

                FormData formData = _OperationCommonInfo.Forms.Find(delegate (FormData item) { return item.ID == _CurrentData.FormID; });
                paymentFreight.BLNo = formData == null ? string.Empty : formData.No;
                paymentFreight.PaymentMode = "支票";
                paymentFreight.CompanyID = _CurrentData.CompanyID;
                paymentFreight.BankName = string.Empty;
                paymentFreight.BankNo = "见发票";
                paymentFreight.Price = 0;
                paymentFreight.Qty = 0;
                paymentFreight.Remark = string.Empty;
                paymentFreight.WorkFlowName = string.Format(LocalData.IsEnglish ? "Application for reimbursement of freight <{0}>({1})" : "申请付<{0}>({1})的运费", business.OperationNO, _CurrentData.CustomerName);

                #endregion

                payFreightPart.SetDataSource(paymentFreight, Guid.Empty);

                IWorkspace mainWorkspace = Workitem.Workspaces[ClientConstants.MainWorkspace];

                SmartPartInfo smartPartInfo = new SmartPartInfo { Title = "运费付乞流程" };
                mainWorkspace.Show(payFreightPart, smartPartInfo);
            }
        }

        #endregion

        #region 亏损流程申请

        [CommandHandler(CustomerBillCommands.Commond_Deficit)]
        public void Commond_Deficit(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (_CurrentData.IsNew) return;

                if (_CurrentData.IsDirty)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), "请先保存数据.");
                    return;
                }

                try
                {
                    BusinessList business = FinanceService.GetBusinessListByIDs(new Guid[] { _OperationCommonInfo.OperationID }).GetList<BusinessList>()[0];

                    WorkflowClientService.StartDeficitWorkFlow(
                        LocalData.UserInfo.LoginID,
                        LocalData.UserInfo.DefaultDepartmentID,
                        LocalData.IsEnglish ? "No" : "单号:" + business.OperationNO,
                        LocalData.IsEnglish ? "Loss process application" : "亏损流程申请",
                        _OperationCommonInfo.OperationNo,
                        business.CompanyID,
                        _OperationCommonInfo.OperationID,
                        _OperationCommonInfo.OperationNo,
                        business.SalesName != null ? business.SalesName : string.Empty,
                        EnumHelper.GetDescription<OperationType>(_OperationCommonInfo.OperationType, LocalData.IsEnglish),
                        business.ARDescription,
                        business.APDescription,
                        business.ProfitDescription,
                        LocalData.UserInfo.UserName,
                        business.CustomerID,
                        business.CustomerName,
                        string.Empty);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);

                }
            }
        }
        #endregion

        #region 额外支付流程申请
        [CommandHandler(CustomerBillCommands.Commond_AdditionalWF)]
        public void Commond_AdditionalWF(object sender, EventArgs e)
        {
            //using (new CursorHelper(Cursors.WaitCursor))
            //{
            //    if (_CurrentData.IsNew) return;

            //    if (_CurrentData.IsDirty)
            //    {
            //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), "请先保存数据.");
            //        return;
            //    }

            //    try
            //    {
            //        BusinessList business = finService.GetBusinessListByIDs(new Guid[] { _OperationCommonInfo.OperationID }).GetList<BusinessList>()[0];
            //        wfClientService.StartAdditionalPaymentsWorkFlow(
            //            LocalData.UserInfo.LoginID
            //            , _OperationCommonInfo.CompanyID
            //            , LocalData.IsEnglish ? "No" : "单号:" + business.OperationNO
            //            , LocalData.IsEnglish ? "AdditionalPayments process application" : "额外支付流程申请"
            //            , DateTime.Now
            //            , _OperationCommonInfo.OperationNo
            //            , business.ARDescription
            //            , false
            //            , business.APDescription
            //            , false
            //            , false
            //            ,false,string.Empty);


            //    }

            //    catch (Exception ex)
            //    {
            //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            //    }
            //}
        }

        #endregion

        #endregion

        #region Comment Code
        //void seTrem_EditValueChanged(object sender, EventArgs e)
        //{
        //    bsBillInfo.EndEdit();
        //    if (_CurrentData == null || _CurrentData.AccountDate == null) return;
        //    dteDueDate.DateTime = _CurrentData.DueDate = _CurrentData.AccountDate.AddDays((int)seTrem.Value);
        //}
        #endregion
    }
}
