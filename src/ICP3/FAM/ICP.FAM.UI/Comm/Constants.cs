
namespace ICP.FAM.UI
{
    /// <summary>
    /// 系统中定义好的功能项命令名
    /// </summary>
    public class ActionsConstants
    {
        /// <summary>
        /// 维护特殊帐单
        /// </summary>
        public const string FAM_EDITSPECIALBILL = "FAM_EDITSPECIALBILL";

        /// <summary>
        /// Terms管理员
        /// </summary>
        public const string FAM_TREMMANAGER = "FAM_TREMMANAGER";

        /// <summary>
        /// 销帐成功后刷新帐单列表面板
        /// </summary>
        public const string FAM_REFRESHBILLLISTPART = "FAM_REFRESHBILLLISTPART";

        /// <summary>
        /// 发票汇率查询
        /// </summary>
        public const string SEARCH_INVOICE_EXCHANGE = "SEARCH_INVOICE_EXCHANGE";

        /// <summary>
        /// 发票汇率编辑
        /// </summary>
        public const string EDIT_INVOICE_EXCHANGE = "EDIT_INVOICE_EXCHANGE";

        /// <summary>
        /// 海外指定货MBL放单
        /// </summary>
        public const string FAM_OverSeaMBL = "FAM_OverSeaMBL";

        /// <summary>
        /// 解锁
        /// </summary>
        public const string FAM_CHECK_UntieLock = "FAM_CHECK_UntieLock";
        /// <summary>
        /// 直连支付
        /// </summary>
        public const string FAM_DirectBankPayment = "FAM_DirectBankPayment";
        /// <summary>
        /// 应收账单转应付账单
        /// </summary>
        public const string CovnertBillForARToDN = "FAM_COVNERTBILLFORARTODN";
    }
    
    /// <summary>
    /// 财务模块常量
    /// </summary>
    public class ModuleConstantsForFAM
    {
        /// <summary>
        /// 录入正本提单
        /// </summary>
        public const string FAM_RECORDMBL = "FAM_RECORDMBL";

        /// <summary>
        /// 放单
        /// </summary>
        public const string FAM_RELEASEBL = "FAM_RELEASEBL";

        /// <summary>
        /// 放货
        /// </summary>
        public const string FAM_RealeaseRC = "FAM_RealeaseRC";

        /// <summary>
        /// 电放
        /// </summary>
        public const string FAM_RELEASEBYTELEX = "FAM_RELEASEBYTELEX";

        /// <summary>
        /// 查看回执
        /// </summary>
        public const string FAM_VIEWFEEDBACK = "FAM_VIEWFEEDBACK";

        /// <summary>
        /// 查看电放申请
        /// </summary>
        public const string FAM_VIEWTELEXAPPLICATION = "FAM_VIEWTELEXAPPLICATION";

        /// <summary>
        /// 录入电放申请单
        /// </summary>
        public const string FAM_ADDTELEXAPPLY = "FAM_ADDTELEXAPPLY";

        /// <summary>
        /// 录入单票业务电放申请单
        /// </summary>
        public const string FAM_ADDSINGLETELEXAPPLY = "FAM_ADDSINGLETELEXAPPLY";

        /// <summary>
        /// 录入总电放申请单
        /// </summary>
        public const string FAM_ADDTOTALTELEXAPPLY = "FAM_ADDTOTALTELEXAPPLY";

        /// <summary>
        /// 正本放货
        /// </summary>
        public const string FAM_SENDORIGINAL = "FAM_SENDORIGINAL";


        /// <summary>
        /// 改为正本放货
        /// </summary>
        public const string FAM_CHANGETOORIGINAL = "FAM_CHANGETOORIGINAL";

        /// <summary>
        /// 改为电放
        /// </summary>
        public const string FAM_CHANGETOTELEX = "FAM_CHANGETOTELEX";

        /// <summary>
        /// 查看后续业务
        /// </summary>
        public const string FAM_VIEWNEXTJOBS = "FAM_VIEWNEXTJOBS";

        /// <summary>
        /// 业务
        /// </summary>
        public const string FAM_BUSINESSLIST = "FAM_BUSINESSLIST";
        /// <summary>
        /// 帐单
        /// </summary>
        public const string FAM_BILLLIST = "FAM_BILLLIST";
        /// <summary>
        /// 核销
        /// </summary>
        public const string FAM_WRITEOFFLIST = "FAM_WRITEOFFLIST";
        /// <summary>
        /// State 销账列表操作银行流水
        /// </summary>
        public const string FAM_STATEOBJECT_WRITEOFFLIST_BANKTRANSACTION = "StateObject_WriteOffList_BankTransaction";
        /// <summary>
        /// 发票
        /// </summary>
        public const string FAM_INVOICELIST = "FAM_INVOICELIST";
        /// <summary>
        /// 银行
        /// </summary>
        public const string FAM_BANKLIST = "FAM_BANKLIST";
        /// <summary>
        /// 汇率
        /// </summary>
        public const string FAM_RATELIST = "FAM_RATELIST";

        /// <summary>
        /// 电放申请单管理
        /// </summary>
        public const string FAM_TELEXRELEASE = "FAM_TELEXRELEASE";

        /// <summary>
        /// 代理对账
        /// </summary>
        public const string FAM_AGENTBILLCHECKING = "FAM_AGENTBILLCHECKING";

        /// <summary>
        /// 日记帐
        /// </summary>
        public const string FAM_JOURNAL = "FAM_JOURNAL";

        /// <summary>
        /// 核销单
        /// </summary>
        public const string FAM_VERIFISHEETLIST = "FAM_VERIFISHEETLIST";

        /// <summary>
        /// 打印支票样式
        /// </summary>
        public const string WriteOffReportType = "WriteOffReportType";

        /// <summary>
        /// 打开月结协议维护的主界面
        /// </summary>
        public const string FAM_MONTHLY_CLOSING_ENTRY = "FAM_MONTHLY_CLOSING_ENTRY";

        /// <summary>
        /// 财务关帐
        /// </summary>
        public const string FAM_ACCOUNTSCLOSE = "FAM_ACCOUNTSCLOSE";

        /// <summary>
        /// 财务关帐
        /// </summary>
        public const string FAM_INVOICEEXCHANGE = "FAM_INVOICEEXCHANGE";

        /// <summary>
        /// 会计科目 
        /// </summary>
        public const string FAM_DLCODE = "FAM_DLCODE";

        /// <summary>
        /// 凭证列表
        /// </summary>
        public const string FAM_LedgerList = "FAM_LedgerList";

        /// <summary>
        /// 汇率调整
        /// </summary>
        public const string FAM_AdjustRate = "FAM_ADJUSTRATE";

        /// <summary>
        /// 导入期初余额
        /// </summary>
        public const string FAM_ImportBeginBalance = "FAM_IMPORTBEGINBALANCE";

        /// <summary>
        /// 应收账款控制
        /// </summary>
        public const string FAM_AccountantControl = "FAM_ACCOUNTANTCONTROL";

        /// <summary>
        /// 应收账款控制
        /// </summary>
        public const string FAM_ChargeCodeConfigure = "FAM_CHARGECODECONFIGURE";

        /// <summary>
        /// 批量账单管理
        /// </summary>
        public const string FAM_BatchBillManage = "FAM_BATCH_BILL_MANAGE";

        /// <summary>
        /// 水单列表
        /// </summary>
        public const string FAM_BankReceiptList = "FAM_Bank_Receipt_List";
        /// <summary>
        /// 银行流水
        /// </summary>
        public const string FAM_BANKTRANSACTION = "FAM__BankTransaction";
    }

    #region SearchFieldConstants
    /// <summary>
    /// 查询字段常量
    /// </summary>
    public class SearchFieldConstants
    {
        /// <summary>
        ///  "ID", "Code", "EName", "CName"
        /// </summary>
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName" };
        /// <summary>
        /// "ID", "ChargingCodeID", "Name", "IsAgent" 
        /// </summary>
        public static readonly string[] ChargeCodeResultValue = new string[] { "ID", "ChargingCodeID", "ChargingCodeName", "IsAgent" };
        /// <summary>
        ///  "ID", "Code", "EName", "CName", "Type", "State","CheckedState"
        /// </summary>
        public static readonly string[] ResultValueWithState = new string[] { "ID", "Code", "EName", "CName", "Type", "State", "CheckedState" };
        /// <summary>
        /// "ID", "Code", "EName", "CName"
        /// 适用于搜索“港口”
        /// </summary>
        public static readonly string[] PortResultValue = new string[] { "ID", "Code", "EName", "CName" };
        /// <summary>
        /// 0"ID", 1"Code", 2"EName", 3"CName",4"Type",5"TradeTermID",6"TradeTermName",7"State",8"CheckedState",9"Term"
        ///  适用于搜索“客户”
        /// </summary>
        public static readonly string[] CustomerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName", "State", "CheckedState", "Term", "UpdateDate", "Fax", "EMail", "CAddress", "BankAccountNo", "TaxIdNo" };
        /// <summary>
        /// 发票返回字段
        /// </summary>
        public static readonly string[] InvoiceTitleResultValue = new string[] { "ID", "Code", "Name", "TaxNo", "AddressTel", "InvoiceType", "BankAccountNo", "InvoiceType" };
        /// <summary>
        /// ID，NO，VesselName
        /// </summary>
        public static readonly string[] VesselResultValue = new string[] { "ID", "No", "VesselName" };
        /// <summary>
        /// ID，NO，VesselName
        /// </summary>
        public static readonly string[] VoyageResultValue = new string[] { "ID", "No", "VoyageName" };
        /// <summary>
        /// ID
        /// </summary>
        public static readonly string[] BillResultValue = new string[] { "ID" };
        /// <summary>
        /// 业务返回字段
        /// </summary>
        public static readonly string[] BusinessResultValue = new string[] { "ID", "No" };
        /// <summary>
        /// 银行流水返回字段
        /// </summary>
        public static readonly string[] ResultValueBankTransaction = new string[] { "ID", "BusinessNO", "TransactionAmount", "OperationDateTime" };
        /// <summary>
        /// 客户银行结果集
        /// </summary>
        public static readonly string[] CustomerBankResultValue = new string[] { "ID", "AccountName", "AccountNO", "BranchName" };
        /// <summary>
        /// 船名/航次
        /// </summary>
        public const string VesselVoyage = @"Vessel/Voyage";
        public const string Vessel = @"Vessel";
        public const string Voyage = @"Voyage";
        public const string BillNoBLNo = @"BillNo/BLNo";
        public const string BillNo = @"BillNo";
        public const string BLNo = @"BLNo";
        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";
        public const string ID = "ID";
        public const string BusinessNo = "BusinessNo";
    } 
    #endregion

    #region UIConstants
    public class UIConstants
    {
        public const string EditSource = "EditSource";

    } 
    #endregion
}
