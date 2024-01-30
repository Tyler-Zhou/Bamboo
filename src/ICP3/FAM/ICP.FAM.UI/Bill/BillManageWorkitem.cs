using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.FAM.ServiceInterface.DataObjects;
using System;

namespace ICP.FAM.UI.Bill
{
    public class BillWorkitem : WorkItem
    {


        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        }

        private void Show()
        {
            BillMainWorkspace mainSpce = SmartParts.Get<BillMainWorkspace>("BillMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<BillMainWorkspace>("BillMainWorkspace");

                #region AddPart

                BillToolBar toolBar = SmartParts.AddNew<BillToolBar>();
                IWorkspace toolBarWorkspace = Workspaces[BillWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BillListPart listPart = SmartParts.AddNew<BillListPart>();
                IWorkspace listWorkspace = Workspaces[BillWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BillSearchPart searchPart = SmartParts.AddNew<BillSearchPart>();
                IWorkspace searchWorkspace = Workspaces[BillWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                BillOperationListPart operationListPart = SmartParts.AddNew<BillOperationListPart>();
                IWorkspace operationListWorkspace = Workspaces[BillWorkSpaceConstants.OperationListWorkspace];
                operationListWorkspace.Show(operationListPart);

                BillOperationToolBar operationToolBar = SmartParts.AddNew<BillOperationToolBar>();
                IWorkspace operationToolBarWorkspace = Workspaces[BillWorkSpaceConstants.OperationToolBarWorkspace];
                operationToolBarWorkspace.Show(operationToolBar);

                BillDunPart billDunPart = SmartParts.AddNew<BillDunPart>();


                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "BillList Info" : "帐单列表";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                BillUIAdapter bookingAdapter = new BillUIAdapter();

                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add(operationListWorkspace.GetType().Name, operationListWorkspace);
                dic.Add(billDunPart.GetType().Name, billDunPart);
                dic.Add("OperationListWorkspace", operationListWorkspace);
                dic.Add("OperationToolBarWorkspace", operationToolBarWorkspace);
                dic.Add(mainSpce.GetType().Name, mainSpce);

                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(operationToolBar.GetType().Name, operationToolBar);
                dic.Add(operationListPart.GetType().Name, operationListPart);
                if (RootWorkItem.State[ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION] != null)
                {
                    dic.Add(ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION, RootWorkItem.State[ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION]);
                    RootWorkItem.State[ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION] = null;
                }
                bookingAdapter.Init(dic);
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }

        /// <summary>
        /// 根据条件加载财务列表界面
        /// </summary>
        /// <param name="criteria"></param>
        public void Show(BillListQueryCriteria criteria)
        {
            BillMainWorkspace mainSpce = SmartParts.Get<BillMainWorkspace>("BillMainWorkspace");
            if (mainSpce == null)
            {
                mainSpce = SmartParts.AddNew<BillMainWorkspace>("BillMainWorkspace");

                #region AddPart

                BillToolBar toolBar = SmartParts.AddNew<BillToolBar>();
                IWorkspace toolBarWorkspace = Workspaces[BillWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolBar);

                BillListPart listPart = SmartParts.AddNew<BillListPart>();
                IWorkspace listWorkspace = Workspaces[BillWorkSpaceConstants.ListWorkspace];
                listWorkspace.Show(listPart);

                BillSearchPart searchPart = SmartParts.AddNew<BillSearchPart>();
                IWorkspace searchWorkspace = Workspaces[BillWorkSpaceConstants.SearchWorkspace];
                searchWorkspace.Show(searchPart);

                BillOperationListPart operationListPart = SmartParts.AddNew<BillOperationListPart>();
                IWorkspace operationListWorkspace = Workspaces[BillWorkSpaceConstants.OperationListWorkspace];
                operationListWorkspace.Show(operationListPart);

                BillOperationToolBar operationToolBar = SmartParts.AddNew<BillOperationToolBar>();
                IWorkspace operationToolBarWorkspace = Workspaces[BillWorkSpaceConstants.OperationToolBarWorkspace];
                operationToolBarWorkspace.Show(operationToolBar);

                BillDunPart billDunPart = SmartParts.AddNew<BillDunPart>();


                #endregion

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "BillList Info" : "帐单列表";
                mainWorkspace.Show(mainSpce, smartPartInfo);


                BillUIAdapter bookingAdapter = new BillUIAdapter();

                Dictionary<string, object> dic = new Dictionary<string, object>();

                dic.Add(operationListWorkspace.GetType().Name, operationListWorkspace);
                dic.Add(billDunPart.GetType().Name, billDunPart);
                dic.Add("OperationListWorkspace", operationListWorkspace);
                dic.Add("OperationToolBarWorkspace", operationToolBarWorkspace);
                dic.Add(mainSpce.GetType().Name, mainSpce);

                dic.Add(toolBar.GetType().Name, toolBar);
                dic.Add(listPart.GetType().Name, listPart);
                dic.Add(searchPart.GetType().Name, searchPart);
                dic.Add(operationToolBar.GetType().Name, operationToolBar);
                dic.Add(operationListPart.GetType().Name, operationListPart);
                dic.Add("BillListQueryCriteria", criteria);
                if (RootWorkItem.State[ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION] != null)
                {
                    dic.Add(ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION, RootWorkItem.State[ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION]);
                    RootWorkItem.State[ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION] = null;
                }
                bookingAdapter.Init(dic);
                
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(mainSpce);
            }
        }
    }

    /// <summary>
    /// 命常量
    /// </summary>
    public class BillCommandConstants
    {
        public const string Command_ViewBusinessInfo = "Command_ViewBusinessInfo";
        public const string Command_ViewBillList = "Command_ViewBillList";

        public const string Command_WriteOffHistory = "Command_WriteOffHistory";
        public const string Command_FeeDetail = "Command_FeeDetail";
        public const string Command_ShowSearch = "Command_ShowSearch";
        public const string Command_ShowSelected = "Command_ShowSelected";
        public const string Command_ShowTotal = "Command_ShowTotal";
        public const string Command_WriteOff = "Command_WriteOff";
        public const string Command_MultiCurrencyWriteOff = "Command_MultiCurrencyWriteOff";

        public const string Command_BusinessCost = "Command_BusinessCost";
        public const string Command_Auditor = "Command_Auditor";
        public const string Command_UnAuditor = "Command_UnAuditor";
        public const string Command_CheckSelected = "Command_CheckSelected";
        public const string Command_SetInvoiceNo = "Command_SetInvoiceNo";
        public const string Command_ViewInvoice = "Command_ViewInvoice";
        public const string Command_AddInvoice = "Command_AddInvoice";
        public const string Command_InvoiceContract = "Command_InvoiceContract";

        public const string Command_PaymentRequest = "Command_PaymentRequest";

        public const string Command_AllCheck = "Command_AllCheck";
        public const string Command_OpenTaskCenter = "Command_OpenTaskCenter";

        public const string Command_Remove = "Command_Remove";
        public const string Command_Clear = "Command_Clear";
        public const string Command_FinderConfirm = "Command_FinderConfirm";
        public const string Command_FinderSelectAll = "Command_FinderSelectAll";
        public const string Command_FinderClearAll = "Command_FinderClearAll";

        public const string Command_ClareSelectListByProgram = "Command_ClareSelectListByProgram";
        public const string Command_ClareDetailByProgram = "Command_ClareDetailByProgram";
        /// <summary>
        /// 应收账单转换成代理账单
        /// </summary>
        public const string Command_ConvertBillFromARToDN = "Command_ConvertBillFromARToDN";

    }
    /// <summary>
    /// WorkSpace常量
    /// </summary>
    public class BillWorkSpaceConstants
    {
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ListWorkspace = "ListWorkspace";

        public const string OperationToolBarWorkspace = "OperationToolBarWorkspace";
        public const string OperationListWorkspace = "OperationListWorkspace";
    }

    /// <summary>
    /// UI适配器
    /// </summary>
    public class BillUIAdapter:IDisposable
    {
  
        #region parts

        IToolBar _toolBar;
        BillSearchPart _searchPart;
        BillListPart _mainListPart;

        IWorkspace _operationToolBarWorkspace;
        IWorkspace _operationListWorkspace;

        IToolBar _operationToolBar;
        BillOperationListPart _operationListPart;
        BillDunPart _billDunPart;
        BillMainWorkspace _billMainWorkspace;

        BillProgram billProgram = BillProgram.Custom;

        #endregion

        #region interface

        public void Init(Dictionary<string, object> controls)
        {
            _toolBar = (IToolBar)controls[typeof(BillToolBar).Name];
            _searchPart = (BillSearchPart)controls[typeof(BillSearchPart).Name];
            _mainListPart = (BillListPart)controls[typeof(BillListPart).Name];
            _operationToolBar = (IToolBar)controls[typeof(BillOperationToolBar).Name];

            _operationListWorkspace = (IWorkspace)controls["OperationListWorkspace"];
            _operationToolBarWorkspace = (IWorkspace)controls["OperationToolBarWorkspace"];
            _billMainWorkspace = (BillMainWorkspace)controls[typeof(BillMainWorkspace).Name];

            _operationListPart = (BillOperationListPart)controls[typeof(BillOperationListPart).Name];
            _billDunPart = (BillDunPart)controls[typeof(BillDunPart).Name];
            RefreshBarEnabled(_toolBar, null);
            RefreshOperationBarEnabled(_operationToolBar, null);
            #region Connection

            #region 主列表事件
            _mainListPart.CurrentChanged += delegate(object sender, object data)
            {
                CurrencyBillList listData = data as CurrencyBillList;
                #region toolBar

                RefreshBarEnabled(_toolBar, listData);

                #endregion
            };
            ///勾选一项
            _mainListPart.Selected += delegate(object sender, object data)
            {
                if (billProgram == BillProgram.Dun)
                {
                    //催款单
                    _billDunPart.DataSource = data;
                }
                else
                {
                    _operationListPart.DataSource = data;
                    if (GetBillProgram())
                    {
                        RefreshOperationBarEnabled(_operationToolBar, data);
                    }
                }

            };
            _mainListPart.KeyDown += new KeyEventHandler(_mainListPart_KeyDown);

            _mainListPart.LinkClickedEvent += new LinkClickedEventHandler(_mainListPart_LinkClickedEvent);
            #endregion

            #region 催款单事件
            _billDunPart.Selected += delegate(object sender, object data)
            {
                _mainListPart.MultiListRemoveData(sender, data);
            };
            #endregion

            #region 多列表事件
            _operationListPart.CurrentChanged += delegate(object sender, object data)
            {
                CurrencyBillList listData = data as CurrencyBillList;

                #region toolBar

                RefreshOperationBarEnabled(_operationToolBar, listData);

                #endregion
            };

            _operationListPart.Selected += delegate(object sender, object data)
            {
                _mainListPart.MultiListRemoveData(sender, data);

                if (GetBillProgram())
                {
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", _operationListPart.isMultiCurrency);
                }
            };


            //列表中执行某一个方法时，更新主列表中的数据
            _operationListPart.ListOperating += delegate(object sender, object data)
            {
                _mainListPart.RefreshUI(sender, data);
            };

            #endregion

            #region 查询
            _searchPart.OnSearched += delegate(object sender, object results)
            {
                _mainListPart.DataSource = results;
            };
            #endregion

            #region 分页
            _mainListPart.InvokeGetData += delegate(object sender, object data)
            {
                _searchPart.RaiseSearched(data);
            };
            #endregion

            #region 选择的方案发生改变时
            _searchPart.ProgramSelectedChanged += new SelectedHandler(_searchPart_ProgramSelectedChanged);


            #endregion

            #endregion

            BillListQueryCriteria criteria = null;

            foreach (var item in controls)
            {
                if (item.Key.ToUpper() == "BillListQueryCriteria".ToUpper())
                {
                    criteria = item.Value as BillListQueryCriteria;
                    //criteria.OperationID = Guid.NewGuid();
                    _searchPart.querycriteria = criteria;
                }
                if(item.Key == ModuleConstantsForFAM.FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION)
                {
                    BankTransactionInfo _BankTransactionInfo=item.Value as BankTransactionInfo;
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add("BANKTRANSACTION", item.Value);
                    _searchPart.Init(keyValue);
                    _operationListPart.Init(keyValue);
                    _searchPart.RaiseSearched();
                }
            }

            if (criteria !=null)
            {
                _searchPart.SeaDate();
            }
        }
        /// <summary>
        /// 方案是否为应收/付 核销或自定义
        /// </summary>
        /// <returns></returns>
        private bool GetBillProgram()
        {
            if (billProgram == BillProgram.CheckWriteOff || billProgram == BillProgram.DepositWriteOff || billProgram == BillProgram.Custom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #region 事件
        /// <summary>
        /// 只查询该客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mainListPart_LinkClickedEvent(object sender, LinkClickedEventArgs e)
        {
            if (sender != null)
            {
                string customerName = sender as string;
                if (!string.IsNullOrEmpty(customerName))
                {
                    _searchPart.CustomerNameSearch(customerName);

                    _searchPart.RaiseSearched();
                }
            }
        }
        /// <summary>
        /// 列表中按下某个键时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mainListPart_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != null)
            {
                Dictionary<string, object> keyValue = sender as Dictionary<string, object>;
                if (keyValue != null)
                {
                    _searchPart.Init(keyValue);
                    _searchPart.RaiseSearched();
                }
            }
        }
        /// <summary>
        /// 方案发生改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        void _searchPart_ProgramSelectedChanged(object sender, object data)
        {
            BillProgram program = (BillProgram)data;

            billProgram = program;
            _operationListPart.Billprogram = program;
            _mainListPart.Billprogram = program;

            switch (program)
            {
                case BillProgram.Custom:
                    #region 自定义
                    _operationToolBar.SetVisible("barCheck", true);
                    _operationToolBar.SetVisible("barWriteOff", true);
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", true);
                    _operationToolBar.SetVisible("barWorkFlow", true);
                    _operationToolBar.SetVisible("barAuditor", true);
                    _operationToolBar.SetVisible("barUnAuditor", true);
                    _operationToolBar.SetVisible("barInvoice", true);
                    _operationToolBar.SetVisible("numoperateRate", true);
                    _operationToolBar.SetVisible("barLab", true);
                    _operationToolBar.SetVisible("barBusinessCost", true);
                    _operationToolBar.SetVisible("barPaymentRequest", true);
                    break;
                #endregion
                case BillProgram.DepositWriteOff:
                    #region 应收核销
                    _operationToolBar.SetVisible("barCheck", true);
                    _operationToolBar.SetVisible("barWriteOff", true);
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", true);
                    _operationToolBar.SetVisible("barWorkFlow", false);
                    _operationToolBar.SetVisible("barAuditor", false);
                    _operationToolBar.SetVisible("barUnAuditor", false);
                    _operationToolBar.SetVisible("barInvoice", false);
                    _operationToolBar.SetVisible("numoperateRate", false);
                    _operationToolBar.SetVisible("barLab", false);
                    _operationToolBar.SetVisible("barBusinessCost", false);
                    _operationToolBar.SetVisible("barPaymentRequest", false);
                    break;
                #endregion
                case BillProgram.CheckWriteOff:
                    #region 应付核销
                    _operationToolBar.SetVisible("barCheck", true);
                    _operationToolBar.SetVisible("barWriteOff", true);
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", true);
                    _operationToolBar.SetVisible("barPaymentRequest", false);
                    _operationToolBar.SetVisible("barAuditor", false);
                    _operationToolBar.SetVisible("barUnAuditor", false);
                    _operationToolBar.SetVisible("barInvoice", false);
                    _operationToolBar.SetVisible("numoperateRate", false);
                    _operationToolBar.SetVisible("barLab", false);
                    _operationToolBar.SetVisible("barBusinessCost", false);
                    break;
                #endregion
                case BillProgram.PaymentRequest:
                    #region 应收核销
                    _operationToolBar.SetVisible("barWriteOff", false);
                    _operationToolBar.SetVisible("barCheck", false);
                    _operationToolBar.SetVisible("barWorkFlow", true);
                    _operationToolBar.SetVisible("barPaymentRequest", true);
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", false);
                    _operationToolBar.SetVisible("barAuditor", false);
                    _operationToolBar.SetVisible("barUnAuditor", false);
                    _operationToolBar.SetVisible("barInvoice", false);
                    _operationToolBar.SetVisible("numoperateRate", false);
                    _operationToolBar.SetVisible("barLab", false);
                    _operationToolBar.SetVisible("barBusinessCost", false);
                    break;
                #endregion
                case BillProgram.OperationManagement:
                    #region 业务管理成本
                    _operationToolBar.SetVisible("barCheck", false);
                    _operationToolBar.SetVisible("barWriteOff", false);
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", false);
                    _operationToolBar.SetVisible("barAuditor", false);
                    _operationToolBar.SetVisible("barUnAuditor", false);
                    _operationToolBar.SetVisible("barInvoice", false);
                    _operationToolBar.SetVisible("barWorkFlow", true);
                    _operationToolBar.SetVisible("barPaymentRequest", false);
                    _operationToolBar.SetVisible("numoperateRate", true);
                    _operationToolBar.SetVisible("barLab", true);
                    _operationToolBar.SetVisible("barBusinessCost", true);
                    break;
                #endregion
                case BillProgram.Invoicing:
                    #region 开发票
                    _operationToolBar.SetVisible("barCheck", false);
                    _operationToolBar.SetVisible("barInvoice", true);
                    _operationToolBar.SetVisible("barPaymentRequest", false);
                    _operationToolBar.SetVisible("barWriteOff", false);
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", false);
                    _operationToolBar.SetVisible("barAuditor", false);
                    _operationToolBar.SetVisible("barUnAuditor", false);
                    _operationToolBar.SetVisible("barWorkFlow", false);
                    _operationToolBar.SetVisible("numoperateRate", false);
                    _operationToolBar.SetVisible("barLab", false);
                    _operationToolBar.SetVisible("barBusinessCost", false);
                    break;
                case BillProgram.Auditor:
                    _operationToolBar.SetVisible("barWorkFlow", false);
                    _operationToolBar.SetVisible("barCheck", false);
                    _operationToolBar.SetVisible("barAuditor", true);
                    _operationToolBar.SetVisible("barUnAuditor", true);
                    _operationToolBar.SetVisible("barPaymentRequest", false);
                    _operationToolBar.SetVisible("barWriteOff", false);
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", false);
                    _operationToolBar.SetVisible("barInvoice", false);
                    _operationToolBar.SetVisible("numoperateRate", false);
                    _operationToolBar.SetVisible("barLab", false);
                    _operationToolBar.SetVisible("barBusinessCost", false);
                    break;
                #endregion
                default:
                    break;
            }

            if (program == BillProgram.Dun)
            {
                #region 催款单
                _billMainWorkspace.OperationToolBarWorkspace.Visible = false;
                _operationListWorkspace.Show(_billDunPart);
                #endregion
            }
            else
            {
                #region 非催款单
                _billMainWorkspace.OperationToolBarWorkspace.Visible = true;
                _operationListWorkspace.Show(_operationListPart);
                #endregion
            }



        }
        /// <summary>
        /// 刷新主列表的工具条
        /// </summary>
        /// <param name="toolBar"></param>
        /// <param name="listData"></param>
        private void RefreshBarEnabled(IToolBar toolBar, CurrencyBillList listData)
        {
            if (listData == null)
            {
                toolBar.SetEnable("barBusinessInfo", false);
                toolBar.SetEnable("barFeeDetail", false);
                toolBar.SetEnable("barWriteOffHistory", false);
                toolBar.SetEnable("barAuditor", false);
                toolBar.SetEnable("barUnAuditor", false);
                toolBar.SetEnable("barAddInvoice", false);
                toolBar.SetEnable("barViewInvoice", false);
                toolBar.SetEnable("barAllCheck", false);
                toolBar.SetEnable("barConvertBillFromARToDN", false);
            }
            else
            {
                toolBar.SetEnable("barBusinessInfo", true);
                toolBar.SetEnable("barFeeDetail", true);
                toolBar.SetEnable("barWriteOffHistory", true);
                toolBar.SetEnable("barAuditor", true);
                toolBar.SetEnable("barUnAuditor", true);
                toolBar.SetEnable("barAddInvoice", true);
                toolBar.SetEnable("barViewInvoice", true);
                toolBar.SetEnable("barAllCheck", true);
                toolBar.SetEnable("barConvertBillFromARToDN", true);

                //发票号
                if (string.IsNullOrEmpty(listData.InvoiceNo))
                {
                    toolBar.SetEnable("barViewInvoice", false);
                }
                else
                {
                    toolBar.SetEnable("barViewInvoice", true);
                }
            }

        }
        /// <summary>
        /// 刷新明细列表的工具条
        /// </summary>
        /// <param name="toolBar"></param>
        /// <param name="data"></param>
        private void RefreshOperationBarEnabled(IToolBar toolBar, object data)
        {
            if (data == null)
            {
                toolBar.SetEnable("barWriteOff", false);
                toolBar.SetEnable("barMultiCurrencyWriteOff", false);
                toolBar.SetEnable("barCheck", false);
                toolBar.SetEnable("barWorkFlow", false);
                toolBar.SetEnable("barAuditor", false);
                toolBar.SetEnable("barUnAuditor", false);
                toolBar.SetEnable("barInvoice", false);
                toolBar.SetEnable("numoperateRate", false);
                toolBar.SetEnable("barLab", false);
                toolBar.SetEnable("barBusinessCost", false);
                toolBar.SetEnable("barRemove", false);
                toolBar.SetEnable("barClear", false);
                toolBar.SetEnable("barPaymentRequest", false);
                toolBar.SetEnable("barInvoiceContract", false);
            }
            else
            {
                toolBar.SetEnable("barCheck", true);
                toolBar.SetEnable("barWriteOff", true);
                toolBar.SetEnable("barWorkFlow", true);
                toolBar.SetEnable("barMultiCurrencyWriteOff", true);
                toolBar.SetEnable("barAuditor", true);
                toolBar.SetEnable("barUnAuditor", true);
                toolBar.SetEnable("barInvoice", true);
                toolBar.SetEnable("numoperateRate", true);
                toolBar.SetEnable("barLab", true);
                toolBar.SetEnable("barBusinessCost", true);
                toolBar.SetEnable("barRemove", true);
                toolBar.SetEnable("barClear", true);
                toolBar.SetEnable("barPaymentRequest", true);
                toolBar.SetEnable("barInvoiceContract", true);

                //List<CurrencyBillList> list = data as List<CurrencyBillList>;
                List<CurrencyBillList> list = new List<CurrencyBillList>();
                if (data is CurrencyBillList)
                {
                    list.Add(data as CurrencyBillList);
                }
                else
                {
                    list = data as List<CurrencyBillList>;
                }


                #region 销帐按钮
                if (list == null || list.Count == 0)
                {
                    _operationToolBar.SetEnable("barCheck", false);
                    _operationToolBar.SetVisible("barWriteOff", false);
                    _operationToolBar.SetVisible("barMultiCurrencyWriteOff", false);
                }
                else
                {
                    int i = (from l in list group l by l.CurrencyID into g select new { g.Key }).Count();
                    int j = (from d in list where Math.Abs(d.Amount) != Math.Abs(d.WriteOffAmount) select d.ID).Count();

                    if (i > 1) //多币种
                    {
                        if (j > 0)   //有未核销的
                        {
                            _operationToolBar.SetVisible("barWriteOff", true);
                            _operationToolBar.SetEnable("barWriteOff", true);

                            _operationToolBar.SetVisible("barMultiCurrencyWriteOff", true);
                            _operationToolBar.SetEnable("barMultiCurrencyWriteOff", true);
                        }
                        else
                        {
                            _operationToolBar.SetVisible("barWriteOff", false);
                            _operationToolBar.SetVisible("barMultiCurrencyWriteOff", false);
                            _operationToolBar.SetEnable("barCheck", false);
                        }
                    }
                    else //单币种
                    {
                        _operationToolBar.SetVisible("barMultiCurrencyWriteOff", false);
                        if (j > 0)   //有未核销的
                        {
                            _operationToolBar.SetVisible("barWriteOff", true);
                            _operationToolBar.SetEnable("barWriteOff", true);
                        }
                        else
                        {
                            _operationToolBar.SetVisible("barWriteOff", false);
                            _operationToolBar.SetEnable("barCheck", false);
                        }
                    }
                }

                #endregion

                #region 审核
                if (list == null || list.Count == 0)
                {
                    toolBar.SetEnable("barAuditor", false);
                    toolBar.SetEnable("barUnAuditor", false);
                }
                else
                {

                    int k = (from d in list where d.Checked select d.ID).Count();
                    int t = (from d in list where d.State == BillState.Approved select d.ID).Count();
                    if (k == list.Count)  //都是已审核过
                    {
                        toolBar.SetEnable("barAuditor", false);
                        if (t == list.Count)  //都是已审核状态
                        {
                            toolBar.SetEnable("barUnAuditor", true);
                        }
                        else
                        {
                            toolBar.SetEnable("barUnAuditor", false);
                        }
                    }
                    else if (k == 0)//都没有审核过
                    {
                        toolBar.SetEnable("barAuditor", true);
                        toolBar.SetEnable("barUnAuditor", false);
                    }
                }
                #endregion
            }
        } 
        #endregion

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            _billDunPart = null;
            _billMainWorkspace = null;
            _mainListPart = null;
            _operationListPart = null;
            _operationListWorkspace = null;
            _operationToolBar = null;
            _operationToolBarWorkspace = null;
            _searchPart = null;
            _toolBar = null;
         
            
        }

        #endregion
    }
}
