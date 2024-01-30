using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI.CC;
using ICP.Common.UI.Configure.Charging;
using ICP.Common.UI.Configure.ChargingCode;
using ICP.Common.UI.Configure.CommpanyConfigure;
using ICP.Common.UI.Configure.Currency;
using ICP.Common.UI.Configure.EDIConfigure;
using ICP.Common.UI.Configure.ReportConfigure;
using ICP.Common.UI.CustomerFinder;
using ICP.Common.UI.CustomerManager;
using ICP.Common.UI.Geography.CountryProvince;
using ICP.Common.UI.Geography.Location;
using ICP.Common.UI.GoTo;
using ICP.Common.UI.ReportView;
using ICP.Common.UI.TransportFoundation.Commodity;
using ICP.Common.UI.TransportFoundation.Container;
using ICP.Common.UI.TransportFoundation.DataDictionary;
using ICP.Common.UI.TransportFoundation.Flight;
using ICP.Common.UI.TransportFoundation.TransportClause;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIManagement;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Common.UI.Common;
using ICP.Common.UI.Configure.TerminalLogins;
using ICP.Crawler.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
namespace ICP.Common.UI
{
    public class CommonModuleInit : ModuleInit
    {
        /// <summary>
        /// 勾子管理类
        /// </summary>
        private KeyboardHookLib _keyboardHook = null;

        #region Service

        private ITerminalService TerminalService
        {
            get
            {
                return ServiceClient.GetService<ITerminalService>();
            }
        }
        //static ITerminalService terminalService = ServiceProxyFactory.Create<ITerminalService>("TerminalService");
        public IGeographyService GeographyService
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

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }
        public IKeyboardEventService KeyboardEventService
        {
            get
            {
                return ServiceClient.GetClientService<IKeyboardEventService>();
            }
        }

        #endregion

        #region init

        WorkItem _rootWorkItem;
        IDataUIManageService _uiService;
        IDataFinderFactory _datafinderFactory;

        public CommonModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataUIManageService uiService
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _uiService = uiService;
            _datafinderFactory = datafinderFactory;
        }

        #endregion

        #region DataFinder Register

        public override void AddServices()
        {
            base.AddServices();
            //#endregion
            _rootWorkItem.Services.AddNew<FileConvertService, IFileConvertService>();
            _rootWorkItem.Services.AddNew<ExcelService, IExcelService>();
            _rootWorkItem.Services.AddNew<ClientBaseDataService, IClientBaseDataService>();
            _rootWorkItem.Services.AddNew<BaseDataNotifyService, IBaseDataNotifyService>();
            _rootWorkItem.Services.AddNew<KeyboardEventService, IKeyboardEventService>();
            _rootWorkItem.Services.AddNew<ClientServers, IClientServers>();
            _rootWorkItem.Services.AddNew<ReportViewService, IReportViewService>();

            #region Location


            //ChargeCode
            _datafinderFactory.Register<ChargingCodeInfoFinder>(CommonFinderConstants.ChargingCodeInfoFinder);

            _datafinderFactory.Register<ChargingCodeFinder>(CommonFinderConstants.ChargingCodeFinder);

            //Location
            _datafinderFactory.Register<LocationFinder>(CommonFinderConstants.LocationFinder);

            _datafinderFactory.Register<OceanLocationFinder>(CommonFinderConstants.OceanLocationFinder);
            _datafinderFactory.Register<AirLocationFinder>(CommonFinderConstants.AirLocationFinder);
            _datafinderFactory.Register<OtherLocationFinder>(CommonFinderConstants.OtherLocationFinder);

            _datafinderFactory.Register<CustomerInvoiceTitleFinder>(CommonFinderConstants.InvoiceTitleFinder);

            #endregion

            #region Customer

            _datafinderFactory.Register<CustomerFinder.CustomerFinder>(CommonFinderConstants.CustoemrFinder);
            _datafinderFactory.Register<CustomerAirlineFinder>(CommonFinderConstants.CustomerAirlineFinder);
            _datafinderFactory.Register<CustomerCarrierFinder>(CommonFinderConstants.CustomerCarrierFinder);
            _datafinderFactory.Register<CustomerForwardingFinder>(CommonFinderConstants.CustomerForwardingFinder);
            _datafinderFactory.Register<CustomerAgentOfCarrierFinder>(CommonFinderConstants.CustomerAgentOfCarrierFinder);
            _datafinderFactory.Register<CustomerAgentFinder>(CommonFinderConstants.CustomerAgentFinder);
            _datafinderFactory.Register<CustomerCustomsBrokerFinder>(CommonFinderConstants.CustomerCustomsBrokerFinder);
            _datafinderFactory.Register<CustomerTruckerFinder>(CommonFinderConstants.CustomerTruckerFinder);
            _datafinderFactory.Register<CustomerWarehouseFinder>(CommonFinderConstants.CustomerWarehouseFinder);
            _datafinderFactory.Register<CustomerStorageFinder>(CommonFinderConstants.CustomerStorageFinder);

            #endregion

            #region VesselVoyage

            PickOneHandler vesselVoyagePoh = delegate(string searchValue, string property
                                                                                    , SearchConditionCollection conditions
                                                                                    , FinderTriggerType triggerType)
            {

                Guid? polId = Guid.Empty;
                Guid? podId = Guid.Empty;


                string vesselName = string.Empty, voyageNo = string.Empty;

                if (triggerType == FinderTriggerType.KeyEnter)
                {
                    if (property.Contains(SearchFieldConstants.Vessel))
                        vesselName = searchValue;
                    else
                        voyageNo = searchValue;
                }

                List<VoyageList> list = TransportFoundationService.GetVoyageList(null, vesselName, voyageNo, null, null, null, true, 100);
                return list;
            };
            PickManyHandler vesselVoyagePmh = delegate(string searchValue, string property
                                                                                , SearchConditionCollection conditions
                                                                                , GetExistValueHandler getExistValueHandler, FinderTriggerType triggerType)
            {
                return null;
            };

            string vesselVoyageFinderTitle = LocalData.IsEnglish == false ? "船名航次查询" : @"Vessel\Voyage Finder";
            _datafinderFactory.Register<VesselVoyageDataFinderUIProxyLogic>(
                CommonFinderConstants.VesselVoyageFinder,
                vesselVoyageFinderTitle,
                vesselVoyagePoh,
                vesselVoyagePmh);
            #endregion

            #region currency
            PickOneHandler currencyPoh = delegate(string searchValue, string property
                                                                                    , SearchConditionCollection conditions
                                                                                    , FinderTriggerType triggerType)
            {
                string name, code;
                name = code = string.Empty;
                if (triggerType == FinderTriggerType.KeyEnter)
                {
                    if (property.Contains(SearchFieldConstants.Code))
                        code = searchValue;
                    else
                        name = searchValue;
                }

                List<CurrencyList> list = ConfigureService.GetCurrencyList(code,
                                                                            name,
                                                                             null,
                                                                             true,
                                                                             0);
                return list;
            };
            PickManyHandler currencyPmh = delegate(string searchValue, string property
                                                                                , SearchConditionCollection conditions
                                                                                , GetExistValueHandler getExistValueHandler, FinderTriggerType triggerType)
            {
                return null;
            };
            string currencyFinderTitle = LocalData.IsEnglish == false ? "币种查询" : @"Currency Finder";
            _datafinderFactory.Register<CurrencyDataFinderUIProxy>(CommonFinderConstants.CurrencyFinder, currencyFinderTitle, currencyPoh, currencyPmh);
            #endregion

            #region TerminalLogins
            PickOneHandler terminalLoginsPoh = delegate(string searchValue, string property
                                                                                    , SearchConditionCollection conditions
                                                                                    , FinderTriggerType triggerType)
            {
                string userID, code;
                userID = code = string.Empty;
                if (triggerType == FinderTriggerType.KeyEnter)
                {
                    if (property.Contains(SearchFieldConstants.Code))
                        code = searchValue;
                    else
                        userID = searchValue;
                }

                List<TerminalLogins> list = TerminalService.GetTerminalLoginsList(code,userID, 0, true);
                return list;
            };
            PickManyHandler terminalLoginsPmh = delegate(string searchValue, string property
                                                                                , SearchConditionCollection conditions
                                                                                , GetExistValueHandler getExistValueHandler, FinderTriggerType triggerType)
            {
                return null;
            };
            string terminalLoginsFinderTitle = LocalData.IsEnglish == false ? "码头账户查询" : @"TerminalLogins Finder";
            _datafinderFactory.Register<TerminalLoginsDataFinderUIProxy>(CommonFinderConstants.TerminalLoginsFinder, terminalLoginsFinderTitle, terminalLoginsPoh, terminalLoginsPmh);
            #endregion

            #region ChargingCode

            #endregion

            #region  SolutionChargeCodeFinderUIProxy

            PickOneHandler solutionChargeCodePoh = delegate(string searchValue, string property
                                                                                    , SearchConditionCollection conditions
                                                                                    , FinderTriggerType triggerType)
            {
                Guid solutionID = Guid.Empty;
                string Name = triggerType == FinderTriggerType.KeyEnter ? searchValue : string.Empty;

                if (conditions != null)
                {
                    if (conditions.Contain("SolutionID"))
                    {
                        solutionID = (Guid)conditions.GetValue("SolutionID").Value;
                    }
                }

                List<SolutionChargingCodeList> list = ConfigureService.GetSolutionChargingCodeListByList(solutionID, Name, null, null, null);
                return list;
            };

            PickManyHandler solutionChargeCodePohPmh = delegate(string searchValue, string property
                                                                              , SearchConditionCollection conditions
                                                                              , GetExistValueHandler getExistValueHandler, FinderTriggerType triggerType)
            {
                return null;
            };

            string solutionChargeCodeFinderTitle = LocalData.IsEnglish ? "ChargeCode Finder" : "费用代码搜索器";

            _datafinderFactory.Register<SolutionChargeCodeFinderUIProxy>(
                CommonFinderConstants.SolutionChargingCodeFinder,
                LocalData.IsEnglish ? "ChargeCode Finder" : "费用代码搜索器",
                solutionChargeCodePoh,
                solutionChargeCodePohPmh);

            #endregion
        }

        #endregion

        private List<LocationList> GetFinderLocationList(string searchValue, string property
                                                        , SearchConditionCollection conditions
                                                        , FinderTriggerType triggerType
                                                        , bool? isOcean, bool? isAir, bool? isOther)
        {
            string codeOrName = triggerType == FinderTriggerType.KeyEnter ? searchValue : string.Empty;
            List<LocationList> list = GeographyService.GetLocationList(codeOrName, null, null, isOcean, isAir, isOther, true, 100);
            return list;
        }

        //#endregion

        #region TransportFoundation

        //打开品名
        [CommandHandler(CommonCommandConstants.Common_CommodityList)]
        public void Open_Common_CommodityList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<CommodityLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 箱
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_ContainerList)]
        public void Open_Common_ContainerList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<ContainerLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);

        }

        /// <summary>
        /// 字典
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_DataDictionaryList)]
        public void Open_Common_DataDictionaryList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<DataDictionaryLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 航班
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_FlightList)]
        public void Open_Common_FlightList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<FlightLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 航线
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_ShippingLineList)]
        public void Open_Common_ShippingLineList(object sender, EventArgs e)
        {
            //ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");

            //_uiService.Open<TransportFoundation.ShippingLine.ShippingLineLayoutUIProxyLogic>();

            //ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            ShippingLineMangerWorkItem bookingWorkitem = _rootWorkItem.WorkItems.AddNew<ShippingLineMangerWorkItem>();

            try
            {
                bookingWorkitem.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                bookingWorkitem.Dispose();
            }

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 运输条款
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_TransportClauseList)]
        public void Open_Common_TransportClauseList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<TransportClauseLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 船名
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_VesselList)]
        public void Open_Common_VesselList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<VesselLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 航次
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_VoyageList)]
        public void Open_Common_VoyageList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<VoyageLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region  Geography

        /// <summary>
        /// 国家省份列表命令名
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_CountryProvinceList)]
        public void Open_Common_CountryProvinceList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");


            _uiService.Open<CountryProvinceLayoutUIProxyLogic>();


            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 地点列表命令名
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_LocationList)]
        public void Open_Common_LocationList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<LocationLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region Customer

        /// <summary>
        /// 客户列表命令名
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_CustomerList)]
        public void Open_Common_CustomerList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            //_uiService.Open<CustomerLayoutUIProxyLogic>();

            CustomerManagerWorkItem customerWorkItem = _rootWorkItem.WorkItems.AddNew<CustomerManagerWorkItem>();
            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];

            customerWorkItem.Show(
                mainWorkspace,
                LocalData.IsEnglish ? "Customer Manager" : "客户管理");

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region CommpanyConfigure

        /// <summary>
        /// 公司配置列表命令名
        /// </summary>
        [CommandHandler(CommonCommandConstants.Common_ConfigureList)]
        public void Open_Common_ConfigureList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            CommpanyConfigureWorkItem commpanyConfigureWorkItem = _rootWorkItem.WorkItems.AddNew<CommpanyConfigureWorkItem>();
            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            commpanyConfigureWorkItem.Show(
                mainWorkspace,
                "公司配置");

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region EDIConfigure

        //EDI配置列表命令名
        [CommandHandler(CommonCommandConstants.Common_EDIConfigureList)]
        public void Open_Common_EDIConfigureList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            EDIConfigureWorkItem ediConfigureWorkItem = _rootWorkItem.WorkItems.AddNew<EDIConfigureWorkItem>();
            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];

            ediConfigureWorkItem.Show(
                mainWorkspace,
                "EDI配置");

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region ReportConfigure

        //Report配置列表命令名
        [CommandHandler(CommonCommandConstants.Common_ReportConfigList)]
        public void Open_Common_ReportConfigureList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            ReportConfigureWorkItem reportConfigureWorkItem = _rootWorkItem.WorkItems.AddNew<ReportConfigureWorkItem>();
            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];

            reportConfigureWorkItem.Show(
                mainWorkspace,
                "报表配置");

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region Configer

        //费用代码
        [CommandHandler(CommonCommandConstants.Common_ChargingCodeList)]
        public void Open_Common_ChargingCodeList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<ChargingLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        //币种
        [CommandHandler(CommonCommandConstants.Common_CurrencyList)]
        public void Open_Common_CurrencyList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<CurrencyLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        //码头账户
        [CommandHandler(CommonCommandConstants.Common_TerminalLogin)]
        public void Open_Common_TerminalLoginsList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<TerminalLoginsLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }


        //解决方案
        [CommandHandler(CommonCommandConstants.Common_SolutionList)]
        public void Open_Common_SolutionList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            _uiService.Open<SolutionLayoutUIProxyLogic>();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region Common_CooperationCustomer

        //公司配置列表命令名
        [CommandHandler(CommonCommandConstants.Common_CooperationCustomer)]
        public void Open_CooperationCustomer(object sender, EventArgs e)
        {
            CCWorkitem ccWorkitem = _rootWorkItem.WorkItems.AddNew<CCWorkitem>();
            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            ccWorkitem.Run();
        }


        //影视项目
        [CommandHandler(CommonCommandConstants.Common_MovieProjects)]
        public void Common_MovieProjects(object sender, EventArgs e)
        {
            MovieProjectWorkItem mpWorkitem = _rootWorkItem.WorkItems.AddNew<MovieProjectWorkItem>();
            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            mpWorkitem.Run();
        }

        /// <summary>
        /// MAC地址管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommonCommandConstants.Common_AuthCode)]
        public void Common_AuthCode(object sender, EventArgs e)
        {
            AuthcodeWorkItem mpWorkitem = _rootWorkItem.WorkItems.AddNew<AuthcodeWorkItem>();
            IWorkspace mainWorkspace = _rootWorkItem.Workspaces[ClientConstants.MainWorkspace];
            mpWorkitem.Run();
        }


        #endregion
    }
}
