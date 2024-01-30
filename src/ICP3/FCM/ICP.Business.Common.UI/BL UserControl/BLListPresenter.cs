using ICP.DataCache.ServiceInterface;
using System.Collections.Generic;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;

namespace ICP.Business.Common.UI.BL
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="blList"></param>
    public delegate void BLListBindDataDelegate(List<OceanBLList> blList);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="blList"></param>
    public delegate void BillListBindDataDelegate(List<BLList> blList);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="blList"></param>
    public delegate void BillOfLadingListBindDataDelegate(List<BillOfLadingList> blList);
    ///// <summary>
    ///// 提单列表呈现类
    ///// </summary>
    //public sealed class BLListPresenter
    //{
    //    #region 服务

    //    #endregion

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public UCBLListPart blListPart { get; set; }

    //    public void SetDataSource(BusinessOperationContext context, UCBLListPart _blListPart)
    //    {
    //    }

    //    private void BindData(List<OceanBLList> blList)
    //    {
    //        this.blListPart.DataSource = blList;
    //    }

    //}

    /// <summary>
    /// 提单列表常量
    /// </summary>
    public partial class BLWorkSpaceConstants
    {
        /// <summary>
        /// 添加MBL
        /// </summary>
        public const string BLCommand_AddMBL = "BLCommand_AddMBL";
        /// <summary>
        /// 添加HBL
        /// </summary>
        public const string BLCommand_AddHBL = "BLCommand_AddHBL";
        /// <summary>
        /// 预配提单
        /// </summary>
        public const string BLCommand_DeclarationBL = "BLCOMMAND_DECLARATIONBL";
        /// <summary>
        /// 添加HBL
        /// </summary>
        public const string BLCommand_AddDeclareHBL = "BLCommand_AddDeclareHBL";
        /// <summary>
        /// 复制
        /// </summary>
        public const string BLCommand_Copy = "BLCommand_Copy";
        /// <summary>
        /// 移除
        /// </summary>
        public const string BLCommand_Remove = "BLCommand_Remove";
        /// <summary>
        /// 编辑
        /// </summary>
        public const string BLCommand_Edit = "BLCommand_Edit";
        /// <summary>
        /// 刷新
        /// </summary>
        public const string BLCommand_Refersh = "BLCommand_Refersh";
        /// <summary>
        /// Split BL
        /// </summary>
        public const string BLCommand_SplitBL = "BLCommand_SplitBL";
        /// <summary>
        /// Merge BL
        /// </summary>
        public const string BLCommand_MergeBL = "BLCommand_MergeBL";
        /// <summary>
        /// EDI MBL
        /// </summary>
        public const string BLCommand_E_MBL = "BLCommand_E_MBL";
        /// <summary>
        /// EDI VGM
        /// </summary>
        public const string BLCommand_E_VGM = "BLCommand_E_VGM";

        /// <summary>
        /// EDI 订舱
        /// </summary>
        public const string BLCommand_E_Booking = "BLCommand_E_Booking";
        /// <summary>
        /// EDI 预配
        /// </summary>
        public const string BLCommand_E_Preplan = "BLCommand_E_Preplan";
        /// <summary>
        /// EDI 补料
        /// </summary>
        public const string BLCommand_E_Supplement = "BLCommand_E_Supplement";
        /// <summary>
        /// EDI 码头
        /// </summary>
        public const string BLCommand_E_Wharf = "BLCommand_E_Wharf";


        /// <summary>
        /// EDI  Is Finish
        /// </summary>
        public const string BLCommand_E_ISF = "BLCommand_E_ISF";
        /// <summary>
        /// EDI AMS
        /// </summary>
        public const string BLCommand_E_AMS = "BLCommand_E_AMS";
        /// <summary>
        /// 位置改变
        /// </summary>
        public const string BLCommand_PositionChanged = "BLCommand_PositionChanged";
        /// <summary>
        /// BL 状态信息
        /// </summary>
        public const string BLCommandState_BLInfo = "BLCommandState_BLInfo";
        /// <summary>
        /// AMS
        /// </summary>
        public const string BLCommand_AMS = "BLCommand_AMS";
        /// <summary>
        /// 确认AMS
        /// </summary>
        public const string BLCommand_ConfirmedAMS = "BLCommand_ConfirmedAMS";
        /// <summary>
        /// ACI
        /// </summary>
        public const string BLCommand_ACI = "BLCommand_ACI";
        /// <summary>
        /// Copy AMS To
        /// </summary>
        public const string BLCommand_CopyAMSTo = "BLCommand_CopyAMSTo";
        /// <summary>
        /// AMS ACI
        /// </summary>
        public const string BLCommand_AMSACI = "BLCommand_AMSACI";
        /// <summary>
        /// AMS ACI Is Finish
        /// </summary>
        public const string BLCommand_AMSACIISF = "BLCommand_AMSACIISF";
        /// <summary>
        /// AMS Is Finish
        /// </summary>
        public const string BLCommand_AMSISF = "BLCommand_AMSISF";
        /// <summary>
        /// Is Finish
        /// </summary>
        public const string BLCommand_ISF = "BLCommand_ISF";
        /// <summary>
        /// 打印提单
        /// </summary>
        public const string BLCommand_PrintBL = "BLCommand_OELPrintBL";
        /// <summary>
        /// 打印利润表
        /// </summary>
        public const string BLCommand_PrintProfit = "BLCommand_OEPrintProfit";
        /// <summary>
        /// 打印装箱单
        /// </summary>
        public const string BLCommand_PrintLoadCtn = "BLCommand_OELPrintLoadCtn";
        /// <summary>
        /// 打印装货单
        /// </summary>
        public const string BLCommand_PrintLoadGoods = "BLCommand_OELPrintLoadGoods";

        /// <summary>
        /// 打印装箱单
        /// </summary>
        public const string BLCommand_PrintLoadContainer = "BLCommand_OELPrintLoadContainer";
        /// <summary>
        /// 打印装箱单副本
        /// </summary>
        public const string BLCommand_PrintLoadContainerCopy = "BLCommand_PrintLoadContainerCopy";

        /// <summary>
        /// 客户确认补料(中文版)
        /// </summary>
        public const string BLCommand_MailBLCopyToCustomerCHS = "BLCommand_MailBLCopyToCustomerCHS";

        /// <summary>
        /// 客户确认补料(英文版)
        /// </summary>
        public const string BLCommand_MailBLCopyToCustomerENG = "BLCommand_MailBLCopyToCustomerENG";

        /// <summary>
        /// 向代理确认所有提单 (中文版)
        /// </summary>
        public const string BLCommand_MailAllBLCopyToAgentCHS = "BLCommand_MailAllBLCopyToAgentCHS";

        /// <summary>
        /// 向代理确认所有提单 (英文版)
        /// </summary>
        public const string BLCommand_MailAllBLCopyToAgentENG = "BLCommand_MailAllBLCopyToAgentENG";

    }
}
