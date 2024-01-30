namespace ICP.Common.CommandHandler.ServiceInterface
{
    /// <summary>
    /// 处理命令名称类（命名规范为Command_业务类型_命令名称）
    /// </summary>
    public static class CommonCommandName
    {

        #region 公共的处理命令（命名规则以Command_Common_事件名称）
        /// <summary>
        /// 测试命名
        /// </summary>
        public const string Command_Common_Test = "Command_Common_Test";
        /// <summary>
        /// F5刷新
        /// </summary>
        public const string Command_F5_Search = "Command_F5_Search";
        /// <summary>
        /// 刷新
        /// </summary>
        public const string Command_Common_Refresh = "Command_Common_Refresh";
        /// <summary>
        /// CSP Booking Download
        /// </summary>
        public const string Command_Common_CSPBooking_Download = "Command_Common_CSPBooking_Download";

        #endregion 公共的处理命令（命名规则以Command_Common_事件名称）

        #region 海出的处理命令（命名规则以Command_OceanExport_事件名称）
        /// <summary>
        /// 打印订舱确认书(宁波)
        /// </summary>
        public const string Command_OceanExport_Print_BookingConfirmation4NB = "Command_OceanExport_Print_BookingConfirmation4NB";
        /// <summary>
        /// 查看海出订单状态
        /// </summary>
        public const string Command_OceanExport_ViewOrderStates = "Command_OceanExport_ViewOrderStates";
        /// <summary>
        /// 新增订舱单         
        /// </summary>
        public const string CommandForSoAdd = "Command_ForSoAdd";
        /// <summary>
        /// 编辑/打开 订舱单
        /// </summary>
        public const string CommandForSoEdit = "Command_ForSoEdit";
        /// <summary>
        /// 取消订舱单
        /// </summary>
        public const string CommandForSoCancel = "Command_ForSoCancel";
        /// <summary>
        /// 恢复订舱单
        /// </summary>
        public const string CommandForSoResume = "Command_ForSoResume";
        /// <summary>
        /// 申请订舱
        /// </summary>
        public const string CommandForSoApplySo = "Command_ForSoApplySO";
        /// <summary>
        /// 申请代理
        /// </summary>
        public const string CommandForSoApplyAgent = "Command_ForSoApplyAgent";
        /// <summary>
        /// 复制订舱单
        /// </summary>
        public const string CommandForSoCopy = "Command_ForSoCopy";
        /// <summary>
        /// 快速订舱
        /// </summary>
        public const string Command_ForFasterAdd = "Command_ForFasterAdd";
        /// <summary>
        /// 复制业务
        /// </summary>
        public const string Command_OceanExport_CopyBusiness = "Command_OceanExport_CopyBusiness";
        /// <summary>
        /// 订舱装船
        /// </summary>
        public const string CommandForSoOnBoard = "Command_ForSoOnBoard";
        /// <summary>
        /// 打印订舱单
        /// </summary>
        public const string CommandForSoPrintOrder = "Command_ForSoPrintOrder";
        /// <summary>
        /// 打印订舱确认书
        /// </summary>
        public const string CommandForSoPrint = "Command_ForSoPrint";
        /// <summary>
        /// 打开Bkg Failed
        /// </summary>
        public const string CommandCommunicationBkgFailed = "Command_CommunicationBkgFailed";
        /// <summary>
        /// 货物跟踪
        /// </summary>
        public const string CommandCommunicationCargoTracking = "Command_CommunicationCargoTracking";
        /// <summary>
        /// 分发文档
        /// </summary>
        public const string CommandCommunicationAgentDocs = "Command_CommunicationAgentDocs";
        /// <summary>
        /// 询价
        /// </summary>
        public const string CommandCommunicationInquireRates = "Command_CommunicationInquireRates";
        /// <summary>
        /// 电子订舱
        /// </summary>
        public const string CommandCommunicationOnlineBkg = "Command_CommunicationOnlineBkg";
        /// <summary>
        /// 电子订舱确认
        /// </summary>
        public const string Command_CommunicationOnlineConfirm = "Command_CommunicationOnlineConfirm";
        /// <summary>
        /// 邮件订舱
        /// </summary>
        public const string Command_CommunicationMailBooking = "Command_CommunicationMailBooking";
        /// <summary>
        /// 确认费用(中文版)
        /// </summary>
        public const string CommandCommunicationConfirmDebitFeesChs = "Command_CommunicationConfirmDebitFeesCHS";
        /// <summary>
        /// 确认费用(英文版)
        /// </summary>
        public const string CommandCommunicationConfirmDebitFeesEng = "Command_CommunicationConfirmDebitFeesENG";
        /// <summary>
        /// 利润承诺(中文版)
        /// </summary>
        public const string CommandCommunicationAskProfitPromiseChs = "Command_CommunicationAskProfitPromiseCHS";
        /// <summary>
        /// 利润承诺(英文版)
        /// </summary>
        public const string CommandCommunicationAskProfitPromiseEng = "Command_CommunicationAskProfitPromiseENG";
        /// <summary>
        /// 取消订舱 邮件中文版
        /// </summary>
        public const string CommandCommunicationMailSoCancelationtoCustomerChs = "Command_CommunicationMailSOCancelationtoCustomerCHS";
        /// <summary>
        /// 取消订舱 邮件英文版
        /// </summary>
        public const string CommandCommunicationMailSoCancelationtoCustomerEng = "Command_CommunicationMailSOCancelationtoCustomerENG";
        /// <summary>
        /// 客户补料  邮件中文版
        /// </summary>
        public const string CommandCommunicationAskCustomerForSichs = "Command_CommunicationAskCustomerForSICHS";
        /// <summary>
        /// 客户补料   邮件英文版
        /// </summary>
        public const string CommandCommunicationAskCustomerForSieng = "Command_CommunicationAskCustomerForSIENG";
        /// <summary>
        /// 客户订舱成功 邮件中文版
        /// </summary>
        public const string CommandCommunicationMailSoCopyToCustomerChs = "Command_CommunicationMailSOCopyToCustomerCHS";
        /// <summary>
        /// 客户订舱成功 邮件英文版
        /// </summary>
        public const string CommandCommunicationMailSoCopyToCustomerEng = "Command_CommunicationMailSOCopyToCustomerENG";
        /// <summary>
        /// 客户确认补料 邮件中文版
        /// </summary>
        public const string CommandCommunicationMailBlCopyToCustomerChs = "Command_CommunicationMailBLCopyToCustomerCHS";
        /// <summary>
        /// 客户确认补料 邮件英文版
        /// </summary>
        public const string CommandCommunicationMailBlCopyToCustomerEng = "Command_CommunicationMailBLCopyToCustomerENG";
        /// <summary>
        /// 通知客户订舱失败 邮件中文版
        /// </summary>
        public const string CommandCommunicationMailSoFailureToCustomerChs = "Command_CommunicationMailSOFailureToCustomerCHS";
        /// <summary>
        /// 通知客户订舱失败 邮件英文版
        /// </summary>
        public const string CommandCommunicationMailSoFailureToCustomerEng = "Command_CommunicationMailSOFailureToCustomerENG";
        /// <summary>
        /// 打开MBL
        /// </summary>
        public const string CommandCommunicationOpenMbl = "Command_CommunicationOpenMBL";
        /// <summary>
        /// 打开HBL
        /// </summary>
        public const string CommandCommunicationOpenHbl = "Command_CommunicationOpenHBL";
        /// <summary>
        /// 打开账单列表
        /// </summary>
        public const string CommandCommunicationAccInfo = "Command_CommunicationAccInfo";
        /// <summary>
        /// 批量新增账单
        /// </summary>
        public const string Command_CommunicationBatchAddAccInfo = "Command_CommunicationBatchAddAccInfo";
        /// <summary>
        /// 报关
        /// </summary>
        public const string CommandCommunicationOrderCustoms = "Command_CommunicationOrderCustoms";
        /// <summary>
        /// 派车
        /// </summary>
        public const string CommandCommunicationOrderTruck = "Command_CommunicationOrderTruck";
        /// <summary>
        /// 打开商检单
        /// </summary>
        public const string CommandCommunicationCommidityInspection = "Command_CommunicationCommidityInspection";
        /// <summary>
        /// 要求客户补料确认
        /// </summary>
        public const string Command_EmailCenterCustomerConfirmedSI = "Command_EmailCenterCustomerConfirmedSI";
        /// <summary>
        /// 打开提单列表
        /// </summary>
        public const string CommandCommunicationBlInfo = "Command_CommunicationBLInfo";
        /// <summary>
        /// 任务中心上传SI附件
        /// </summary>
        public const string CommandCommunicationUploadSIAttachment = "Command_CommunicationUploadAttachment";
        /// <summary>
        /// 上传SI附档
        /// </summary>
        public const string CommandUploadSiAttachment = "Command_EmailCenterUploadSIAttachment";
        /// <summary>
        /// 上传SO附档
        /// </summary>
        public const string CommandUploadSOAttachment = "Command_EmailCenterUploadSOAttachment";
        /// <summary>
        /// 上传MBL附档
        /// </summary>
        public const string CommandUploadMBLAttachment = "Command_EmailCenterUploadMBLAttachment";
        /// <summary>
        /// 上传AN附档
        /// </summary>
        public const string CommandUploadANAttachment = "Command_EmailCenterUploadANAttachment";
        /// <summary>
        /// 上传SO附档
        /// </summary>
        public const string CommandUploadAPAttachment = "Command_EmailCenterUploadAPAttachment";
        /// <summary>
        /// 上传附件
        /// </summary>
        public const string CommandUploadAttachment = "Command_EmailCenterUploadAttachment";
        /// <summary>
        /// 上传拖拽附件
        /// </summary>
        public const string CommandUploadDragAttachment = "Command_EmailCenterUploadDragAttachment";
        /// <summary>
        /// 设置业务关联
        /// </summary>
        public const string CommandEmailCenterOperationMessageRelation = "Command_EmailCenterOperationMessageRelation";
        /// <summary>
        /// 新增MBL
        /// </summary>
        public const string CommandEmailCenterAddMbl = "Command_EmailCenterAddMBL";
        /// <summary>
        /// 新增HBL
        /// </summary>
        public const string CommandEmailCenterAddHbl = "Command_EmailCenterAddHBL";
        /// <summary>
        /// 装箱
        /// </summary>
        public const string CommandLoadContainer = "Command_LoadContainer";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_DataCommit = "Command_EmailSubmit";
        /// <summary>
        /// 保存按钮
        /// </summary>
        public const string Command_Submit = "Command_Submit";
        /// <summary>
        /// 刷新按钮
        /// </summary>
        public const string Command_Refresh = "Command_Refresh";
        /// <summary>
        /// 打开事件列表界面
        /// </summary>
        public const string Command_OpenEvent = "Command_OpenEvent";
        /// <summary>
        /// 刷新列表的修改记录
        /// </summary>
        public const string Command_ListRefresh = "Command_ListRefresh";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_CfmAP = "Command_CfmAP";
        /// <summary>
        /// 
        /// </summary>
        public const string Command_CfmAR = "Command_CfmAR";
        /// <summary>
        /// 接受代理修订的账单
        /// </summary>
        public const string Command_CommunicationAcptRev = "Command_CommunicationAcptRev";
        /// <summary>
        /// 审核当前业务存在利润
        /// </summary>
        public const string Command_OceanTrackings = "Command_OceanTrackings";
        /// <summary>
        /// 通知客户订舱确认书(中文版)
        /// </summary>
        public const string Command_MailSoConfirmationToCustomerCHS = "Command_MailSoConfirmationToCustomerCHS";
        /// <summary>
        /// 通知客户订舱确认书(英文版)
        /// </summary>
        public const string Command_MailSoConfirmationToCustomerENG = "Command_MailSoConfirmationToCustomerENG";
        /// <summary>
        /// SOD成功以后发送邮件给客服(So Copy)
        /// </summary>
        public const string Command_MainSOCustomerServiceSOD = "Command_MainSOCustomerServiceSOD";
        /// <summary>
        /// SOD成功以后发送邮件给客服(文档列表上传附件)
        /// </summary>
        public const string Command_MainSOCustomerServiceSODDocument = "Command_MainSOCustomerServiceSODDocument";
        /// <summary>
        /// 通知代理订舱确认书
        /// </summary>
        public const string Command_MailSoConfirmationToAgent = "Command_MailSoConfirmationToAgent";
        /// <summary>
        /// 订舱失败 爆仓
        /// </summary>
        public const string Command_FailureBlastingWarehouse = "Command_FailureBlastingWarehouse";
        /// <summary>
        /// 订舱失败 柜型
        /// </summary>
        public const string Command_FailureCubicleType = "Command_FailureCubicleType";
        /// <summary>
        /// 订舱失败 船期
        /// </summary>
        public const string Command_FailureShippingDate = "Command_FailureShippingDate";
        /// <summary>
        /// 订舱失败 其他
        /// </summary>
        public const string Command_FailureOther = "Command_FailureOther";
        /// <summary>
        /// 取消订舱 货源
        /// </summary>
        public const string Command_CancelSourcing = "Command_CancelSourcing";
        /// <summary>
        /// 取消订舱 价格
        /// </summary>
        public const string Command_CancelPrice = "Command_CancelPrice";
        /// <summary>
        /// 取消订舱 贸易
        /// </summary>
        public const string Command_CancelTrade = "Command_CancelTrade";
        /// <summary>
        /// 取消订舱 其他
        /// </summary>
        public const string Command_CancelOther = "Command_CancelOther";
        /// <summary>
        /// 恢复订舱
        /// </summary>
        public const string Command_RestoreBooking = "Command_RestoreBooking";
        /// <summary>
        /// 订舱员恢复订舱
        /// </summary>
        public const string Command_ClerkRestoreBooking = "Command_ClerkRestoreBooking";
        /// <summary>
        /// 快捷方式打开任务中心
        /// </summary>
        public const string Command_QuickOpenTaskCenter = "Command_QuickOpenTaskCenter";

        #endregion 海出的处理命令（命名规则以Command_OceanExport_事件名称）

        #region 海进的处理命令（命名规则以Command_OceanImport_事件名称）
        /// <summary>
        /// 业务下载
        /// </summary>
        public const string OceanImport_Download = "OceanImport_Download";

        /// <summary>
        /// 编辑
        /// </summary>
        public const string OceanImport_Edit = "OceanImport_Edit";

        /// <summary>
        /// 取消海进业务
        /// </summary>
        public const string OceanImport_Cancel = "OceanImport_Cancel";

        /// <summary>
        /// 海进业务转移
        /// </summary>
        public const string OceanImport_Transfer = "OceanImport_Transfer";

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        public const string OceanImport_PrintArrivalNotice = "OceanImport_PrintArrivalNotice";

        /// <summary>
        /// 放货通知书
        /// </summary>
        public const string OceanImport_ReleaseOrder = "OceanImport_ReleaseOrder";

        /// <summary>
        /// 打印利润表
        /// </summary>
        public const string OceanImport_PrintProfit = "OceanImport_PrintProfit";

        /// <summary>
        /// 打印工作表
        /// </summary>
        public const string OceanImport_PrintWorkSheet = "OceanImport_PrintWorkSheet";

        /// <summary>
        /// 打印货单提单
        /// </summary>
        public const string OceanImport_PrintForwardingBill = "OceanImport_PrintForwardingBill";

        /// <summary>
        /// 打印出口业务信息报表
        /// </summary>
        public const string OceanImport_PrintExportBusinessInfo = "OceanImport_PrintExportBusinessInfo";

        /// <summary>
        /// 账单
        /// </summary>
        public const string OceanImport_Bill = "OceanImport_Bill";

        /// <summary>
        /// 打印提货通知书
        /// </summary>
        public const string OceanImport_CargoBook = "OceanImport_CargoBook";

        /// <summary>
        /// 签收放单
        /// </summary>
        public const string OceanImport_ReceiveRN = "OceanImport_ReceiveRN";

        /// <summary>
        /// 申请放单
        /// </summary>
        public const string OceanImport_ApplyReleaseCargo = "OceanImport_ApplyReleaseCargo";

        /// <summary>
        /// 同意放货
        /// </summary>
        public const string OceanImport_AgreeRC = "OceanImport_AgreeRC";

        /// <summary>
        /// 放货
        /// </summary>
        public const string OceanImport_Delivery = "OceanImport_Delivery";

        /// <summary>
        /// 取消申请放货
        /// </summary>
        public const string OceanImport_CancelApplyRC = "OceanImport_CancelApplyRC";

        /// <summary>
        /// 取消同意放货
        /// </summary>
        public const string OceanImport_CancelAgreeRC = "OceanImport_CancelAgreeRC";

        /// <summary>
        /// 取消放货
        /// </summary>
        public const string OceanImport_CancelDelivery = "OceanImport_CancelDelivery";

        /// <summary>
        /// 海进异常放货申请
        /// </summary>
        public const string OceanImport_ExceptionReleaseRC = "OceanImport_ExceptionReleaseRC";

        /// <summary>
        /// 上传A/N附件
        /// </summary>
        public const string OceanImport_UploadANAttachment = "OceanImport_UploadANAttachment";

        /// <summary>
        /// 海进上传附件
        /// </summary>
        public const string OceanImport_UploadAttachment = "OceanImport_UploadAttachment";
        /// <summary>
        /// 发送到港通知书给客户 (中文版)
        /// </summary>
        public const string OceanImport_MailANToCustomerCHS = "OceanImport_MailANToCustomerCHS";
        /// <summary>
        /// 发送到港通知书给客户 (英文版)
        /// </summary>
        public const string OceanImport_MailANToCustomerENG = "OceanImport_MailANToCustomerENG";
        /// <summary>
        /// 发送提货通知书给客户 (中文版)
        /// </summary>
        public const string OceanImport_MailPickUpToCustomerCHS = "OceanImport_MailPickUpToCustomerCHS";
        /// <summary>
        /// 发送提货通知书给客户 (英文版)
        /// </summary>
        public const string OceanImport_MailPickUpToCustomerENG = "OceanImport_MailPickUpToCustomerENG";

        /// <summary>
        /// 新增
        /// </summary>
        public const string OceanImport_AddOrder = "OceanImport_AddOrder";

        /// <summary>
        /// 复制
        /// </summary>
        public const string OceanImport_CopyOiBusiness = "OceanImport_CopyOiBusiness";

        /// <summary>
        /// 收到MBL正本
        /// </summary>
        public const string OceanImport_OiomblRcved = "OceanImport_OiomblRcved";

        /// <summary>
        /// 财务已发送MBL
        /// </summary>
        public const string OceanImport_MAILDMBL = "OceanImport_MAILDMBL";

        /// <summary>
        /// 财务关帐
        /// </summary>
        public const string OceanImport_State_ACCLOS = "OceanImport_State_ACCLOS";

        /// <summary>
        /// 收到正本
        /// </summary>
        public const string OceanImport_OioblRcved = "OceanImport_OioblRcved";

        /// <summary>
        /// 付款
        /// </summary>
        public const string OceanImport_OiPayment = "OceanImport_OiPayment";


        /// <summary>
        /// 发送催款邮件
        /// </summary>
        public const string OceanImport_MailPayNt = "OceanImport_MailPayNt";

        /// <summary>
        /// 催港前放单
        /// </summary>
        public const string OceanImport_NoticeRelease = "OceanImport_NoticeRelease";

        /// <summary>
        /// 修改放单指令
        /// </summary>
        public const string OceanImport_UpdateIsReleaseOrderRequired = "OceanImport_UpdateIsReleaseOrderRequired";

        /// <summary>
        /// 催代理分发文件(中文版)
        /// </summary>
        public const string OceanImport_MailOverseaAgentCHS = "OceanImport_MailOverseaAgentCHS";

        /// <summary>
        /// 催代理分发文件(英文版)
        /// </summary>
        public const string OceanImport_MailOverseaAgentENG = "OceanImport_MailOverseaAgentENG";

        /// <summary>
        /// ETA 前5天催清关文件
        /// </summary>
        public const string OceanImport_MailCustomrequest = "OceanImport_MailCustomrequest";

        /// <summary>
        /// ETA 变更通知
        /// </summary>
        public const string OceanImport_MailEtachange = "OceanImport_MailEtachange";

        /// <summary>
        /// LFD 通知
        /// </summary>
        public const string OceanImport_MailLfdnotice = "OceanImport_MailLfdnotice";

        /// <summary>
        /// LFD 前2工作日后每天提柜提醒
        /// </summary>
        public const string OceanImport_MailContainerpickupreminder = "OceanImport_MailContainerpickupreminder";

        /// <summary>
        /// 提柜后第2天开始每天还空提醒
        /// </summary>
        public const string OceanImport_MailEmptyreturnnotice = "OceanImport_MailEmptyreturnnotice";

        /// <summary>
        /// CONTAINER AVAILABLE FOR PICK UP(英文版)
        /// </summary>
        public const string OceanImport_MailContaineravailableforpickup = "OceanImport_MailContaineravailableforpickup";

        //           打开业务 Open Shipment
        //  打开派车单 Open Truck Order
        //-------------------------
        //  上传A/N附件 Upload A/N Copy
        //  上传附件... Upload Attachment...
        //-------------------------
        //  取消        Cancel
        //  货物追踪    Cargo Tracking
        //  任务中心    Task Center


        #endregion 海进的处理命令（命名规则以Command_OceanImport_事件名称）

        #region 空出的处理命令（命名规则以Command_AirExport_事件名称）
        /// <summary>
        /// 新增
        /// </summary>
        public const string AirExport_AddData = "AirExport_AddData";

        /// <summary>
        /// 复制
        /// </summary>
        public const string AirExport_CopyData = "AirExport_CopyData";

        /// <summary>
        /// 编辑
        /// </summary>
        public const string AirExport_EditData = "AirExport_EditData";

        /// <summary>
        /// 取消
        /// </summary>
        public const string AirExport_CancelData = "AirExport_CancelData";

        /// <summary>
        /// 申请代理
        /// </summary>
        public const string AirExport_ReplyAgent = "AirExport_ReplyAgent";

        /// <summary>
        /// 提单
        /// </summary>
        public const string AirExport_OpenBl = "AirExport_OpenBl";
        /// <summary>
        /// 账单
        /// </summary>
        public const string AirExport_OpenBill = "AirExport_OpenBill";

        /// <summary>
        /// 业务联单
        /// </summary>
        public const string AirExport_PrintOrder = "AirExport_PrintOrder";

        #endregion 空出的处理命令（命名规则以Command_AirExport_事件名称）

        #region 空进的处理命令（命名规则以Command_AirImport_事件名称）
        /// <summary>
        /// 新增
        /// </summary>
        public const string AirImport_AddBooking = "AirImport_AddBooking";

        /// <summary>
        /// 复制
        /// </summary>
        public const string AirExport_CopyBooking = "AirExport_CopyBooking";

        /// <summary>
        /// 业务下载
        /// </summary>
        public const string AirImport_AiDownLoad = "AirImport_AiDownLoad";
        /// <summary>
        /// 编辑
        /// </summary>
        public const string AirImport_EditBooking = "AirImport_EditBooking";

        /// <summary>
        /// 取消
        /// </summary>
        public const string AirImport_CancelBooking = "AirImport_CancelBooking";

        /// <summary>
        /// 业务转移
        /// </summary>
        public const string AirImport_BusinessTransfer = "AirImport_BusinessTransfer";

        /// <summary>
        /// 到港通知书
        /// </summary>
        public const string AirImport_PrintArrivalNotice = "AirImport_PrintArrivalNotice";

        /// <summary>
        /// 放货通知书
        /// </summary>
        public const string AirImport_PrintReleaseOrder = "AirImport_PrintReleaseOrder";

        /// <summary>
        /// 利润表
        /// </summary>
        public const string AirImport_PrintProfit = "AirImport_PrintProfit";
        /// <summary>
        /// 打印Authority To Make Entry
        /// </summary>
        public const string AirImport_PrintAuthority = "AirImport_PrintAuthority";
        /// <summary>
        /// 账单
        /// </summary>
        public const string AirImport_OpenBill = "AirImport_OpenBill";

        /// <summary>
        /// 提货通知书
        /// </summary>
        public const string AirImport_OpenCargoBook = "AirImport_OpenCargoBook";

        /// <summary>
        /// 放货
        /// </summary>
        public const string AirImport_AiDelivery = "AirImport_AiDelivery";

        /// <summary>
        /// 取消放货
        /// </summary>
        public const string AirImport_CancelDelivery = "AirImport_CancelDelivery";


        #endregion 空进的处理命令（命名规则以Command_AirImport_事件名称）

        #region 其它业务的处理命令（命名规则以Command_Other_事件名称）
        /// <summary>
        /// 新增
        /// </summary>
        public const string OtherBusiness_AddData = "OtherBusiness_AddData";

        /// <summary>
        /// 复制
        /// </summary>
        public const string OtherBusiness_CopyData = "OtherBusiness_CopyData";

        /// <summary>
        /// 编辑
        /// </summary>
        public const string OtherBusiness_EditData = "OtherBusiness_EditData";

        /// <summary>
        /// 取消
        /// </summary>
        public const string OtherBusiness_CancelData = "OtherBusiness_CancelData";

        /// <summary>
        /// 账单
        /// </summary>
        public const string OtherBusiness_Bill = "OtherBusiness_Bill";

        /// <summary>
        /// 核销单
        /// </summary>
        public const string OtherBusiness_VerifiSheet = "OtherBusiness_VerifiSheet";


        /// <summary>
        /// 拖车服务
        /// </summary>
        public const string OtherBusiness_PickUp = "OtherBusiness_PickUp";


        /// <summary>
        /// 业务联系单
        /// </summary>
        public const string OtherBusiness_PrintOrder = "OtherBusiness_PrintOrder";

        /// <summary>
        /// 利润表
        /// </summary>
        public const string OtherBusiness_Profit = "OtherBusiness_Profit";

        #endregion 其它业务的处理命令（命名规则以Command_Other_事件名称）

        #region 其它业务-电商物流的处理命令（命名规则以Command_Other_ECommerce_事件名称）
        /// <summary>
        /// 新增
        /// </summary>
        public const string Other_ECommerce_AddData = "Command_Other_ECommerce_AddData";

        /// <summary>
        /// 复制
        /// </summary>
        public const string Other_ECommerce_CopyData = "Command_Other_ECommerce_CopyData";

        /// <summary>
        /// 编辑
        /// </summary>
        public const string Other_ECommerce_EditData = "Command_Other_ECommerce_EditData";

        /// <summary>
        /// 取消
        /// </summary>
        public const string Other_ECommerce_CancelData = "Command_Other_ECommerce_CancelData";

        /// <summary>
        /// 账单
        /// </summary>
        public const string Other_ECommerce_Bill = "Command_Other_ECommerce_Bill";

        /// <summary>
        /// 核销单
        /// </summary>
        public const string Other_ECommerce_VerifiSheet = "Command_Other_ECommerce_VerifiSheet";

        #endregion 其它业务-电商物流的处理命令（命名规则以Command_Other_ECommerce_事件名称）

        #region 拖车的处理命令（命名规则以Command_Truck_事件名称）

        #endregion 拖车的处理命令（命名规则以Command_Truck_事件名称）

        #region 报关的处理命令（命名规则以Command_Customs_事件名称）

        #endregion 报关的处理命令（命名规则以Command_Customs_事件名称）

        #region 内贸的处理命令（命名规则以Command_Internal_事件名称）

        #endregion 内贸的处理命令（命名规则以Command_Internal_事件名称）

        #region 不确定业务的处理命令（命名规则以Command_Unknown_事件名称）

        #endregion 不确定业务的处理命令（命名规则以Command_Unknown_事件名称）

        #region  邮件中心的处理命令

        /// <summary>
        /// 此业务的客户历史邮件
        /// </summary>
        public const string CommandMail_MailofCustomer = "CommandMail_MailofCustomer";
        /// <summary>
        /// 此业务的承运人历史邮件
        /// </summary>
        public const string CommandMail_HistoryofCarrier = "CommandMail_HistoryofCarrier";
        /// <summary>
        /// 此业务的代理历史邮件
        /// </summary>
        public const string CommandMail_HistoryofAgent = "CommandMail_HistoryofAgent";
        /// <summary>
        /// 此业务的历史邮件
        /// </summary>
        public const string CommandMail_MailHistory = "CommandMail_MailHistory";
        /// <summary>
        /// 客户的历史邮件
        /// </summary>
        public const string CommandMail_HistoryofCustomers = "CommandMail_HistoryofCustomers";
        /// <summary>
        /// 业务的承运人历史邮件
        /// </summary>
        public const string CommandMail_HistoryofCarriers = "CommandMail_HistoryofCarriers";
        /// <summary>
        /// 业务的代理历史邮件
        /// </summary>
        public const string CommandMail_HistoryofAgents = "CommandMail_HistoryofAgents";



        #endregion

        #region  询价的处理命令
        /// <summary>
        /// 邮件-询问人
        /// </summary>
        public const string Command_EmailMailtoAskpeople = "Command_EmailMailtoAskpeople";
        /// <summary>
        /// 邮件-承运人
        /// </summary>
        public const string Command_EmailMailtoCarrier = "Command_EmailMailtoCarrier";
        #endregion

    }
}
