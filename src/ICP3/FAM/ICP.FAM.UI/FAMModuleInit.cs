using System;
using ICP.FAM.ServiceInterface;
using ICP.FAM.UI.BatchBill;
using ICP.FAM.UI.Bill;
using ICP.FAM.UI.Bill.Finder;
using ICP.FAM.UI.Business;
using ICP.FAM.UI.GLCode.Finder;
using ICP.FAM.UI.MonthlyClosingEntry;
using ICP.FAM.UI.ReleaseBL;
using ICP.FAM.UI.ReleaseRC;
using ICP.FAM.UI.TelexApply;
using ICP.FAM.UI.WriteOff;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.UI.CustomerBill.Print;
using ICP.FAM.UI.VerificationSheet;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.FAM.UI.AccReceControl;
using ICP.FAM.UI.ChargeConfigure;
using ICP.FAM.UI.BankReceiptList;
using ICP.FAM.UI.BankTransaction;
using ICP.FAM.UI.BankTransaction.Finder;
using ICP.FAM.UI.BankReceiptList.Finder;

namespace ICP.FAM.UI
{
    public class FAMModuleInit : ModuleInit
    {
        #region 初始化

        WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;

        public FAMModuleInit(
            [ServiceDependency]WorkItem rootWorkItem,
            [ServiceDependency]IDataFinderFactory datafinderFactory)
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();

            _rootWorkItem.Services.AddNew<FinanceClientService, IFinanceClientService>();
        
            BillControllerFactory billControllerFactory = _rootWorkItem.Services.AddNew<BillControllerFactory>();
            billControllerFactory.Register<OEBillController>(OperationType.OceanExport);
            billControllerFactory.Register<OIBillController>(OperationType.OceanImport);
            billControllerFactory.Register<AEBillController>(OperationType.AirExport );
            billControllerFactory.Register<AIBillController>(OperationType.AirImport );
            billControllerFactory.Register<OtherBillController>(OperationType.Other);
            billControllerFactory.Register<TruckBillController>(OperationType.Truck);
            billControllerFactory.Register<CustomsBillController>(OperationType.Customs);
            billControllerFactory.Register<InternalBillController>(OperationType.Internal); 

            
            _datafinderFactory.Register<BillFinder>(FAMFinderConstants.BillFinder);
            _datafinderFactory.Register<GLCodeFinder>(FAMFinderConstants.GLCodeFinder);
            _datafinderFactory.Register<BankTransactionFinder>(FAMFinderConstants.BankTransactionFinder);
            _datafinderFactory.Register<BankReceiptFinder>(FAMFinderConstants.BankReceiptFinder);


        }
        #endregion

        #region Commond

        #region 放单列表
        [CommandHandler(ModuleConstantsForFAM.FAM_RELEASEBL)]
        public void Open_FAM_RELEASEBL(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm();

            ReleaseBLWorkItem workItem = _rootWorkItem.WorkItems.AddNew<ReleaseBLWorkItem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);

        }
        #endregion

        #region 放货列表
        [CommandHandler(ModuleConstantsForFAM.FAM_RealeaseRC)]
        public void Open_FAM_RealeaseRC(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            ReleaseRCWorkItem workItem = _rootWorkItem.WorkItems.AddNew<ReleaseRCWorkItem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);

        }
        #endregion

        #region 业务列表
        [CommandHandler(ModuleConstantsForFAM.FAM_BUSINESSLIST)]
        public void Open_FAM_BUSINESSLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
         
            BusinessWorkitem businessWorkitem = _rootWorkItem.Items.AddNew<BusinessWorkitem>();
            businessWorkitem.Run();

            LoadingServce.CloseLoadingForm(theradID);

        }
        #endregion

        #region 帐单列表
        [CommandHandler(ModuleConstantsForFAM.FAM_BILLLIST)]
        public void Open_FAM_BILLLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            BillWorkitem billWorkitem = _rootWorkItem.Items.AddNew<BillWorkitem>();
            billWorkitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 银行列表
        [CommandHandler(ModuleConstantsForFAM.FAM_BANKLIST)]
        public void Open_FAM_BANKLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            BankWorkitem bankWorkitem = _rootWorkItem.Items.AddNew<BankWorkitem>();

            bankWorkitem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 发票列表
        [CommandHandler(ModuleConstantsForFAM.FAM_INVOICELIST)]
        public void Open_FAM_INVOICELIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            InvoiceWorkitem invoiceWorkitem = _rootWorkItem.Items.AddNew<InvoiceWorkitem>();
            invoiceWorkitem.Run();

            LoadingServce.CloseLoadingForm(theradID);

        }
        #endregion

        #region 汇率列表
        [CommandHandler(ModuleConstantsForFAM.FAM_RATELIST)]
        public void Open_FAM_RATELIST(object sender, EventArgs e)
        {

        }
        #endregion

        #region 核销列表
        [CommandHandler(ModuleConstantsForFAM.FAM_WRITEOFFLIST)]
        public void Open_FAM_WRITEOFFLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            
            WriteOffWorkItem workitem = _rootWorkItem.Items.AddNew<WriteOffWorkItem>();
            workitem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 总电放

        [CommandHandler(ModuleConstantsForFAM.FAM_TELEXRELEASE)]
        public void Open_FAM_TELEXRELEASE(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            
            TelexApplyWorkItem workitem = _rootWorkItem.Items.AddNew<TelexApplyWorkItem>();
            workitem.Run();

            LoadingServce.CloseLoadingForm(theradID);

        }

        #endregion

        #region 代理对账

        [CommandHandler(ModuleConstantsForFAM.FAM_AGENTBILLCHECKING)]
        public void Open_FAM_AGENTBILLCHECKING(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID= LoadingServce.ShowLoadingForm("Loading...");

            AgentBillCheckingWorkitem workitem = _rootWorkItem.Items.AddNew<AgentBillCheckingWorkitem>();
            workitem.Run();

            LoadingServce.CloseLoadingForm(theradID);

        }


        #endregion

        #region 月结协议

        [CommandHandler(ModuleConstantsForFAM.FAM_MONTHLY_CLOSING_ENTRY)]
        public void Open_FAM_MONTHLY_CLOSING_ENTRY(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            EntryWorkItem workitem = _rootWorkItem.Items.AddNew<EntryWorkItem>();
            workitem.Run();

            LoadingServce.CloseLoadingForm(theradID);

        }

        #endregion

        #region 日记帐

        [CommandHandler(ModuleConstantsForFAM.FAM_JOURNAL)]
        public void Open_FAM_JOURNAL(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
  
            JournalWorkitem workitem = _rootWorkItem.Items.AddNew<JournalWorkitem>();
            workitem.Run();

            LoadingServce.CloseLoadingForm(theradID);

        }
        #endregion

        #region 核销单列表

        [CommandHandler(ModuleConstantsForFAM.FAM_VERIFISHEETLIST)]
        public void Open_FAM_VERIFISHEETLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            VerificationSheetWorkItem workitem = _rootWorkItem.Items.AddNew<VerificationSheetWorkItem>();
            workitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 财务关帐
        [CommandHandler(ModuleConstantsForFAM.FAM_ACCOUNTSCLOSE)]
        public void Open_FAM_ACCOUNTSCLOSE(object sender, EventArgs e)
        {
            AccountsClosePart closePart = _rootWorkItem.Items.AddNew<AccountsClosePart>();

            string title =LocalData.IsEnglish?"Accounts Close":"财务关帐";

            PartLoader.ShowDialog(closePart, title);
        }
        #endregion

        #region 发票汇率

        [CommandHandler(ModuleConstantsForFAM.FAM_INVOICEEXCHANGE)]
        public void Open_FAM_INVOICEEXCHANGE(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            InvoiceExchangeWorkitem workitem = _rootWorkItem.Items.AddNew<InvoiceExchangeWorkitem>();
            workitem.Run();
            LoadingServce.CloseLoadingForm(theradID);

            //string title =LocalData.IsEnglish?"发票汇率":"Invoice Exchange";

            //PartLoader.ShowDialog(closePart, title);
        }
        #endregion

        #region 会计科目

        [CommandHandler(ModuleConstantsForFAM.FAM_DLCODE)]
        public void FAM_DLCODE(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            GLCodeWorkitem workitem = _rootWorkItem.Items.AddNew<GLCodeWorkitem>();
            workitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 凭证列表

        [CommandHandler(ModuleConstantsForFAM.FAM_LedgerList)]
        public void Open_FAM_LedgerList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            LedgerListWorkItem workItem = _rootWorkItem.Items.AddNew<LedgerListWorkItem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 调整汇率

        [CommandHandler(ModuleConstantsForFAM.FAM_AdjustRate)]
        public void FAM_AdjustRate(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            AdjustRateWorkItem workItem = _rootWorkItem.Items.AddNew<AdjustRateWorkItem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 导入期初余额
        [CommandHandler(ModuleConstantsForFAM.FAM_ImportBeginBalance)]
        public void FAM_ImportBeginBalance(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            BeginBalanceWorkItem workItem = _rootWorkItem.Items.AddNew<BeginBalanceWorkItem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 批量账单管理
        [CommandHandler(ModuleConstantsForFAM.FAM_BatchBillManage)]
        public void FAM_BatchBillManage(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            BatchBillWorkitem batchBillWorkItem = _rootWorkItem.Items.AddNew<BatchBillWorkitem>();
            try
            {
                batchBillWorkItem.Run();
            }
            catch
            {
                batchBillWorkItem.Dispose();
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
        }

        #endregion

        #region 应收账款控制
        [CommandHandler(ModuleConstantsForFAM.FAM_AccountantControl)]
        public void FAM_AccountantControl(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            AccControlWorkitem workItem = _rootWorkItem.Items.AddNew<AccControlWorkitem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 应收账款控制
        [CommandHandler(ModuleConstantsForFAM.FAM_ChargeCodeConfigure)]
        public void FAM_ChargeCodeConfigure(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            ChargeConfigWorkItem workItem = _rootWorkItem.Items.AddNew<ChargeConfigWorkItem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 水单列表

        [CommandHandler(ModuleConstantsForFAM.FAM_BankReceiptList)]
        public void Open_FAM_BankReceiptList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            BankReceiptListWorkItem workItem = _rootWorkItem.Items.AddNew<BankReceiptListWorkItem>();
            workItem.Run();

            LoadingServce.CloseLoadingForm(theradID);
        }

        #endregion

        #region 银行流水
        [CommandHandler(ModuleConstantsForFAM.FAM_BANKTRANSACTION)]
        public void OPEN_PLUGIN_CMB(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            BankTransactionWorkItem workitem = _rootWorkItem.Items.AddNew<BankTransactionWorkItem>();
            workitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion
        #endregion
    }
}
