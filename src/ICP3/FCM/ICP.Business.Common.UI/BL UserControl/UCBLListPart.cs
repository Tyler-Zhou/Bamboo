using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.CompositeObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Utility = ICP.Framework.ClientComponents.Controls.Utility;

namespace ICP.Business.Common.UI.BL
{
    /// <summary>
    /// 提单用户控件类
    /// </summary>
    [SmartPart]
    public partial class UCBLListPart : UserControl, IBaseBLPart, IDataBind
    {
        #region Fields & Property & Services & Contact

        #region 变量
        /// <summary>
        /// OceanBLList 提单列表数据对象
        /// </summary>
        private OceanBLList oceanBlList = null;
        #endregion

        #region 服务
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
        /// 海出客户端服务
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetClientService<IClientOceanExportService>();
            }

        }
        /// <summary>
        /// 海出服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// ICP通用UI辅助类
        /// </summary>
        public ICPCommUIHelper ICPCommUIHelper
        {
            get
            {
                return ClientHelper.Get<ICPCommUIHelper, ICPCommUIHelper>();
            }
        }
        /// <summary>
        /// 配置服务
        /// </summary>
        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        /// <summary>
        /// FCM Common Service
        /// </summary>
        IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// FCM Common Client Service
        /// </summary>
        IFCMCommonClientService FCMCommonClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFCMCommonClientService>();
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// bsBLList数据源
        /// </summary>
        public object DataSource
        {
            get
            {
                return bsBLList.DataSource;
            }
            set
            {
                bsBLList.DataSource = value;
                bsBLList.ResetBindings(false);
                Workitem.Commands[BLWorkSpaceConstants.BLCommand_PositionChanged].Execute();
            }
        }

        /// <summary>
        /// 业务操作上下文
        /// </summary>
        public BusinessOperationContext CurrentContext { get; set; }

        /// <summary>
        /// ICP公用操作
        /// </summary>
        public IICPCommonOperationService IicpCommonOperation
        {
            get { return ServiceClient.GetClientService<IICPCommonOperationService>(); }
        }

        /// <summary>
        /// 当前行
        /// </summary>
        public OceanBLList CurrentRow { get { return bsBLList.Current == null ? null : bsBLList.Current as OceanBLList; } }

        /// <summary>
        /// gvBLList选中行
        /// </summary>
        List<OceanBLList> SelectedItems
        {
            get
            {
                List<OceanBLList> tagers = new List<OceanBLList>();

                int[] Handle = gvBLList.GetSelectedRows();
                for (int i = 0; i < Handle.Length; i++)
                {
                    OceanBLList dataRow = (OceanBLList)gvBLList.GetRow(i);
                    tagers.Add(dataRow);
                }
                return tagers;
            }
        }

        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateCode { get; set; }
        #endregion

        #region 常量
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public UCBLListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                _barItemDic.Clear();
                _barItemDic = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            if (!LocalData.IsEnglish && !LocalData.IsDesignMode)
            {
                barAdd.Caption = "新增";
                barCopy.Caption = "复制";
                barDelete.Caption = "删除";
                barPrint.Caption = "打印";
                barRefersh.Caption = "刷新";
                barSplitAndMerge.Caption = "分单/合单";
                barEdit.Caption = "编辑";
                barProfit.Caption = "利润打印";
                barPrintLoadGoods.Caption = "打印装货单";
                barLoadContainer.Caption = "打印装箱单";
                barLoadContainerCopy.Caption = "打印装箱单(副本)";
                barEMBL.Caption = "电子补料";

                barSplitBL.Caption = "分单";
                barMergeBL.Caption = "合单";
                barbl.Caption = "发送邮件";
                barchs.Caption = "向客户确认提单(中文版)";
                bareng.Caption = "向客户确认提单(英文版)";
                bartoAgentchs.Caption = "向代理确认所有提单(中文版)";
                bartoAgenteng.Caption = "向代理确认所有提单(英文版)";
                barPrintBL.Caption = "打印提单";
            }
            RegisterEvents();

        }
        #endregion

        #region 初始化

        #region USBLToolPart
        /// <summary>
        /// 注册事件
        /// </summary>
        void RegisterEvents()
        {
            barAddHBL.ItemClick += barAddHBL_ItemClick;
            barAddMBL.ItemClick += barAddMBL_ItemClick;
            barDeclarationBL.ItemClick += barDeclarationBL_ItemClick;
            barEdit.ItemClick += barEdit_ItemClick;
            barCopy.ItemClick += barCopy_ItemClick;
            barDelete.ItemClick += barDelete_ItemClick;
            barPrintBL.ItemClick += barPrintBL_ItemClick;
            barProfit.ItemClick += barProfit_ItemClick;
            barPrintLoadCtn.ItemClick += barPrintLoadCtn_ItemClick;
            barPrintLoadGoods.ItemClick += barPrintLoadGoods_ItemClick;
            barLoadContainer.ItemClick += barPrintLoadContainer_ItemClick;
            barLoadContainerCopy.ItemClick += barPrintLoadContainerCopy_ItemClick;
            barRefersh.ItemClick += barRefersh_ItemClick;
            barSplitBL.ItemClick += barSplitBL_ItemClick;
            barMergeBL.ItemClick += barMergeBL_ItemClick;
            barEMBL.ItemClick += barEMBL_ItemClick;
            barvgm.ItemClick += barEVGM_ItemClick;

            barAMS.ItemClick += barAMS_ItemClick;
            barACI.ItemClick += barACI_ItemClick;
            barCopyAMSTo.ItemClick += barCopyAMSTo_ItemClick;
            barAMSACI.ItemClick += barAMSACI_ItemClick;
            barAMSACIISF.ItemClick += barAMSACIISF_ItemClick;
            barAMSISF.ItemClick += barAMSISF_ItemClick;
            barISF.ItemClick += barISF_ItemClick;
            barchs.ItemClick += barchs_ItemClick;
            bareng.ItemClick += bareng_ItemClick;
            barConfirmedAMS.ItemClick += BarConfirmedAMS_ItemClick;
            bartoAgentchs.ItemClick += bartoAgentchs_ItemClick;
            bartoAgenteng.ItemClick += bartoAgenteng_ItemClick;
            barImportPO.ItemClick += barImportPO_ItemClick;

        }

        
        /// <summary>
        /// Is Finish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_ISF].Execute();
        }
        /// <summary>
        /// AMS Is Finish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAMSISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AMSISF].Execute();
        }
        /// <summary>
        /// AMSACI Is Finish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAMSACIISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AMSACIISF].Execute();
        }
        /// <summary>
        /// 点击AMSACI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAMSACI_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AMSACI].Execute();
        }
        /// <summary>
        /// Copy AMS To
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barCopyAMSTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_CopyAMSTo].Execute();
        }
        /// <summary>
        /// ACI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barACI_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_ACI].Execute();
        }
        /// <summary>
        /// AMS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAMS_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AMS].Execute();
        }
        /// <summary>
        /// 确认AMS
        /// </summary>
        private void BarConfirmedAMS_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_ConfirmedAMS].Execute();
        }
        /// <summary>
        /// EDI MBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barEMBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_E_MBL].Execute();
        }
        /// <summary>
        /// EDI VGM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barEVGM_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_E_VGM].Execute();
        }
        /// <summary>
        /// Merge BL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barMergeBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MergeBL].Execute();
        }
        /// <summary>
        /// Split BL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barSplitBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_SplitBL].Execute();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barRefersh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Refersh].Execute();
        }
        /// <summary>
        /// 打印装货单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barPrintLoadGoods_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintLoadGoods].Execute();
        }
        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barPrintLoadContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintLoadContainer].Execute();
        }
        /// <summary>
        /// 打印装箱单副本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barPrintLoadContainerCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintLoadContainerCopy].Execute();
        }
        /// <summary>
        /// 打印装箱单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barPrintLoadCtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintLoadCtn].Execute();
        }
        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barProfit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintProfit].Execute();
        }
        /// <summary>
        /// 打印提单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barPrintBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintBL].Execute();
        }
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Remove].Execute();
        }
        /// <summary>
        /// 添加HBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAddHBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AddHBL].Execute();
        }
        /// <summary>
        /// 添加MBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barAddMBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AddMBL].Execute();
        }
        /// <summary>
        /// 添加MBL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barDeclarationBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_DeclarationBL].Execute();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Edit].Execute();
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Copy].Execute();
        }
        /// <summary>
        /// 客户确认补料(中文)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barchs_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MailBLCopyToCustomerCHS].Execute();
        }
        /// <summary>
        /// 客户确认补料(英文)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bareng_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MailBLCopyToCustomerENG].Execute();
        }
        /// <summary>
        /// 向代理确认所有提单 (中文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bartoAgentchs_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MailAllBLCopyToAgentCHS].Execute();
        }
        /// <summary>
        /// 向代理确认所有提单 (英文版)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void bartoAgenteng_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MailAllBLCopyToAgentENG].Execute();
        }
        /// <summary>
        /// 导入PO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void barImportPO_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!IsNullObject())
            {
                FCMCommonClientService.ImportPurchaseOrder(CurrentContext, null);
            }
        }
        #endregion

        #region USBLListPart
        /// <summary>
        /// 重写加载方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode && !LocalData.IsEnglish)
            {
                InitControls();
            }

            List<EnumHelper.ListItem<OECompletionStatus>> oeCompletionStatus =
                EnumHelper.GetEnumValues<OECompletionStatus>(LocalData.IsEnglish);
            repBLCfm.Items.Clear();
            repMBLD.Items.Clear();
            repReleaseState.Items.Clear();
            foreach (var item in oeCompletionStatus)
            {
                repBLCfm.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
                repMBLD.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            List<EnumHelper.ListItem<FCMReleaseState>> fcmReleaseState =
              EnumHelper.GetEnumValues<FCMReleaseState>(LocalData.IsEnglish);
            foreach (var listItem in fcmReleaseState)
            {
                if (listItem.Value != FCMReleaseState.Unknown)
                {
                    repReleaseState.Items.Add(new ImageComboBoxItem(listItem.Name,
                                                                                               listItem.Value));
                }
            }

            List<EnumHelper.ListItem<FCMReleaseType>> fcmReleasetype =
             EnumHelper.GetEnumValues<FCMReleaseType>(LocalData.IsEnglish);
            foreach (var listItem in fcmReleasetype)
            {
                if (listItem.Value != FCMReleaseType.Unknown)
                {
                    repReleasetype.Items.Add(new ImageComboBoxItem(listItem.Name,
                                                                                               listItem.Value));
                }
            }

            gvBLList.KeyDown += gvBLList_KeyDown;

            if (ICP.Framework.CommonLibrary.Client.LocalData.UserInfo.DefaultCompanyID != new System.Guid("A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9"))
            {
                barLoadContainer.Visibility = BarItemVisibility.Never;
                barLoadContainerCopy.Visibility = BarItemVisibility.Never;
            }
        }

        /// <summary>
        /// 初始化控件(设置控件显示文本)
        /// </summary>
        void InitControls()
        {
            if (pnlGridList.Controls.Count == 0)
                pnlGridList.Controls.Add(gcBLList);

            colBLNo.Caption = "提单号";
            colShipperName.Caption = "发货人";
            colConsigneeName.Caption = "收货人";
            colNotifyPartyName.Caption = "通知人";
            colBLCfm.ToolTip = "已发BL COPY 让客户确认";
            colMBLD.ToolTip = "已向承运人补料";
            colRBLA.ToolTip = "已申请放单";
            colRBLD.ToolTip = "已放单";
            colRBLH.ToolTip = "发生hold货";
            colRBLRcv.ToolTip = "代理已收到放单通知";
            colRC.ToolTip = "BL已放货";
            colReleaseState.Caption = "放单类型";
            colAMS.ToolTip = "AMS已完成";
            colISF.ToolTip = "ISF已完成";
            colReleaseType.Caption = "放货类型";
            colTelexNo.Caption = "电放号";
            ToolStripMenuItemF5.Text = "按选中查询(F5)";

        }
        #endregion

        void RefershToolBar()
        {
            Workitem.State[BLWorkSpaceConstants.BLCommandState_BLInfo] = CurrentRow;
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PositionChanged].Execute();
        }
        #endregion

        #region 窗体事件

        /// <summary>
        /// 网格焦点行改变
        /// </summary>
        private void gvBLList_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            oceanBlList = gvBLList.GetRow(e.FocusedRowHandle) as OceanBLList;
        }

        /// <summary>
        /// 网格按键
        /// </summary>
        private void gcBLList_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                try
                {
                    Clipboard.SetText(gvBLList.GetFocusedDisplayText());
                }
                catch
                {
                    try
                    {
                        Clipboard.SetText(gvBLList.GetFocusedDisplayText());
                    }
                    catch (Exception ex)
                    {

                    }
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// 行双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBLList_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                if (CurrentRow != null) Workitem.Commands[BLWorkSpaceConstants.BLCommand_Edit].Execute();
            }
        }

        private void ToolStripMenuItemF5_Click(object sender, EventArgs e)
        {
            F5Query();
        }
        private void ToolStripMenuItemMarkSendAMS_Click(object sender, EventArgs e)
        {
            ChangeAMSState(BLAMSState.Send);
        }

        private void ToolStripMenuItemMarkConfirmedAMS_Click(object sender, EventArgs e)
        {
            ChangeAMSState(BLAMSState.Confirmed);
        }
        #endregion

        #region DataBind
        /// <summary>
        /// bsBLList绑定数据
        /// </summary>
        /// <param name="blList">数据源</param>
        public void BindData(List<OceanBLList> blList)
        {
            DataSource = null;
            DataSource = blList;
        }

        /// <summary>
        /// gcBLList绑定数据
        /// </summary>
        /// <param name="context"></param>
        public void DataBind(BusinessOperationContext context)
        {
            if(context.OperationType==OperationType.Other&&context.FormType==FormType.ECommerceOrder)
            {
                if(!(context["RefOperationID"] + "").IsNullOrEmpty())
                {
                    context.OperationID = new Guid(context["RefOperationID"] + "");
                    context.OperationType = (OperationType)byte.Parse(context["RefOperationType"]+"");
                    context.OperationNO = context["RefOperationNo"] + "";
                    context.SONO = context["RefSONO"] + "";
                    context.FormType = FormType.Unknown;
                }
            }
            CurrentContext = context;
            WaitCallback callback = (temp) =>
            {
                try
                {
                    ClientHelper.SetApplicationContext();
                    List<OceanBLList> blList = OceanExportService.GetOceanBLListByOperationInfo(context);
                    if (IsDisposed)
                        return;
                    BLListBindDataDelegate bindDelegate = BindData;
                    if (blList.Any())
                    {
                        if (blList[0].OceanBookingID != context.OperationID)
                        {
                            blList = new List<OceanBLList>();
                        }
                    }
                    Invoke(bindDelegate, new object[] { blList });

                }
                catch (Exception ex)
                {
                    if (!IsDisposed)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                    }
                }


            };
            ThreadPool.QueueUserWorkItem(callback);
        }
        /// <summary>
        /// 控件只读
        /// </summary>
        /// <param name="flg">是否只读</param>
        public void ControlsReadOnly(bool flg)
        {
            //TODO:启用禁用菜单栏
        }
        #endregion

        #region 自定义方法
        /// <summary>
        /// 控件设置启用禁用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="enable"></param>
        public void SetEnable(string name, bool enable)
        {
            if (_barItemDic.ContainsKey(name) && _barItemDic[name] != null)
                _barItemDic[name].Enabled = enable;
        }

        /// <summary>
        /// 当前的TemplateCode和上下文中的TemplateCode是否相等
        /// </summary>
        /// <returns></returns>
        public bool IsEqual()
        {
            return IicpCommonOperation.TemplateCode == TemplateCode;
        }

        /// <summary>
        /// 当前选中行项是否为空
        /// </summary>
        /// <returns></returns>
        private bool IsNullObject()
        {
            bool isNull = true;
            if (CurrentRow != null && CurrentContext != null)
            {
                isNull = false;
            }
            return isNull;
        }

        /// <summary>
        /// 创造业务参数
        /// </summary>
        /// <param name="actionType">动作类型</param>
        /// <param name="isNewOrder">是否新单</param>
        /// <returns></returns>
        public Dictionary<string, object> CreateBusinessParameter(ActionType actionType, bool isNewOrder)
        {
            BusinessOperationParameter businessOperation = new BusinessOperationParameter();

            if (actionType == ActionType.Edit)
            {

                BusinessOperationContext context = new BusinessOperationContext();
                context.OperationNO = CurrentContext.OperationNO;
                context.OperationID = CurrentContext.OperationID;
                context.OperationType = CurrentContext.OperationType;
                context.FormId = CurrentContext.FormId;
                context.SONO = CurrentContext.SONO;
                businessOperation.Context = context;
            }
            else
            {
                if (isNewOrder)
                    businessOperation.Context = new BusinessOperationContext();
                else
                {
                    BusinessOperationContext context = new BusinessOperationContext();
                    context.OperationNO = CurrentContext.OperationNO;
                    context.OperationID = CurrentContext.OperationID;
                    context.OperationType = CurrentContext.OperationType;
                    context.FormId = CurrentContext.FormId;
                    context.SONO = CurrentContext.SONO;
                    businessOperation.Context = context;
                }
            }
            businessOperation.ActionType = actionType;
            businessOperation.TemplateCode = IicpCommonOperation.TemplateCode;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("businessOperationParameter", businessOperation);
            return dic;
        }

        /// <summary>
        /// MBL电子补料
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="selectedList"></param>
        /// <param name="companyID"></param>
        /// <param name="isSucc"></param>
        public void EMBLBusiness(OperationType operationType, List<OceanBLList> selectedList, Guid companyID, ref bool isSucc)
        {
            switch (operationType)
            {
                case OperationType.OceanExport:
                    IicpCommonOperation.InnerEMBL(selectedList, companyID, AMSEntryType.Unknown, ACIEntryType.Unknown, ref isSucc, CurrentContext);
                    break;
                case OperationType.AirExport:
                    //AirExportEMBL();
                    break;
            }
        }

        /// <summary>
        /// MBL电子补料
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="listData"></param>
        public void EISFBusiness(OperationType operationType, OceanBLList listData)
        {
            switch (operationType)
            {
                case OperationType.OceanExport:
                    OceanExportEISF();
                    break;
                case OperationType.AirExport:
                    //AirExportEISF();
                    break;
            }
        }
        void OceanExportEISF()
        {
            // AddOceanExportBLItem();
            Workitem.Commands["Command_OELISF"].Execute();
        }

        /// <summary>
        /// 发送AMS/AIC/ISF的方法
        /// </summary>
        /// <param name="key"></param>
        /// <param name="subjuect"></param>
        /// <param name="oIds"></param>
        /// <param name="hblIds"></param>
        /// <param name="operationNos"></param>
        /// <param name="isSucc"></param>
        /// <param name="ediMode"></param>
        public void SendEDI(string key, string subjuect, List<Guid> oIds, List<Guid> hblIds, List<string> operationNos, bool isSucc, EDIMode ediMode)
        {

            IicpCommonOperation.SendEdiamsaicisf(key, subjuect, oIds, hblIds, operationNos, isSucc, ediMode, CurrentRow.CompanyID, CurrentContext);
        }

        /// <summary>
        /// 发送至代理
        /// </summary>
        /// <param name="oceanBlList"></param>
        /// <param name="flg"></param>
        public void SendToAgent(OceanBLList oceanBlList, bool flg)
        {
            if (oceanBlList != null)
            {
                string result = IicpCommonOperation.MailALLBLCopyToAgent(flg, oceanBlList.OceanBookingID);
                if (!string.IsNullOrEmpty(result))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), result);
                    return;
                }
            }
        }

        /// <summary>
        /// 发送邮件的方法
        /// </summary>
        /// <param name="oceanBlList"></param>
        /// <param name="flg"></param>
        public void Send(OceanBLList oceanBlList, bool flg)
        {
            if (oceanBlList != null)
            {
                OceanMBLInfo oceanMblInfo = null;
                OceanHBLInfo oceanHbl = null;
                if (oceanBlList.BLType == FCMBLType.MBL)
                {
                    //MBL
                    oceanMblInfo = new OceanMBLInfo
                    {
                        No = oceanBlList.No,
                        ReleaseType = oceanBlList.ReleaseType,
                        ID = oceanBlList.MBLID
                    };
                }
                else
                {
                    //HBL;
                    oceanHbl = new OceanHBLInfo
                    {
                        No = oceanBlList.No,
                        ReleaseType = oceanBlList.ReleaseType,
                        ID = oceanBlList.ID
                    };
                }

                string result = IicpCommonOperation.MailCustomerAskForConfirmSI(flg, oceanBlList.OceanBookingID,
                                                                                  oceanHbl, oceanMblInfo);
                if (!string.IsNullOrEmpty(result))
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), result);
                }
            }
        }
        #endregion

        #region 工具栏事件
        /// <summary>
        /// 设置工具栏启用状态
        /// </summary>
        /// <param name="listData"></param>
        void SetToolEnable(OceanBLList listData)
        {
            if (listData == null || listData.IsNew)
            {
                barSplitAndMerge.Enabled = false;
                barSplitBL.Enabled = false;
                barMergeBL.Enabled = false;
            }
            else
            {
                barSplitAndMerge.Enabled = false;
                if (listData.IsValid == false)
                {
                }
                else
                {
                    barSplitBL.Enabled = false;
                    barMergeBL.Enabled = false;
                    if (listData.State != OEBLState.Checked && listData.State != OEBLState.Release)
                    {
                        barMergeBL.Enabled = false;
                    }
                    if (listData.State != OEBLState.Checked && listData.State != OEBLState.Release && listData.HBLCount <= 0)
                    {
                        barSplitBL.Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// 设置工具栏
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PositionChanged)]
        public void BLCommand_PositionChanged(object sender, EventArgs e)
        {
            if (Workitem.State[BLWorkSpaceConstants.BLCommandState_BLInfo] == null) return;
            OceanBLList listData = (OceanBLList)Workitem.State[BLWorkSpaceConstants.BLCommandState_BLInfo];
            SetToolEnable(listData);
        }

        #region 添加MBL
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AddMBL)]
        public void BLCommand_AddMBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!string.IsNullOrEmpty(CurrentContext.SONO))
                {
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        Dictionary<string, object> dic = CreateBusinessParameter(ActionType.Create, false);
                        IicpCommonOperation.AddMBL(dic);
                    }
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "MBL could not be created due to SONO is not filled." : "当前订舱单没有SONO无法建立MBL.");
                }
            }

        }

        #endregion

        #region 添加HBL
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AddHBL)]
        public void BLCommand_AddHBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!string.IsNullOrEmpty(CurrentContext.SONO))
                {
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        Dictionary<string, object> dic = CreateBusinessParameter(ActionType.Create, false);
                        IicpCommonOperation.AddHBL(dic);
                    }
                }

                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "HBL could not be created due to SONO is not filled." : "当前订舱单没有SONO无法建立HBL.");
                }
            }
        }

        #endregion

        #region 预配提单
        /// <summary>
        /// 预配提单
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_DeclarationBL)]
        public void BLCommand_DeclarationBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!string.IsNullOrEmpty(CurrentContext.SONO))
                {
                    using (new CursorHelper(Cursors.WaitCursor))
                    {
                        Dictionary<string, object> values = new Dictionary<string, object>();
                        values["OperationID"] = CurrentContext.OperationID;
                        values["FCMBLType"] = FCMBLType.DeclareHBL;
                        IicpCommonOperation.DeclarationBL("", values);
                    }
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(null, LocalData.IsEnglish ? "BL could not be declaration due to SONO is not filled." : "当前订舱单没有SONO无法进行预配操作.");
                }
            }

        }
        #endregion

        #region Copy
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_Copy)]
        public void BLCommand_Copy(object sender, EventArgs e)
        {

            if (!IsNullObject())
            {
                Copy(CurrentRow);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listData"></param>
        public void Copy(OceanBLList listData)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                if (listData == null) return;

                if (listData.BLType == FCMBLType.MBL)
                {
                    IicpCommonOperation.InnerCopyMBLData(listData);
                }
                else
                {
                    IicpCommonOperation.InnerCopyHBLData(listData);
                }
            }
        }
        #endregion

        #region Edit
        [CommandHandler(BLWorkSpaceConstants.BLCommand_Edit)]
        public void BLCommand_Edit(object sender, EventArgs e)
        {
            if (!IsNullObject())
            {
                Edit();
            }
        }
        void Edit()
        {
            if (CurrentRow.BLType == FCMBLType.MBL)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Dictionary<string, object> dictionary = CreateBusinessParameter(ActionType.Edit, false);
                    IicpCommonOperation.EditMBL(CurrentRow.RefNo, CurrentRow.No, dictionary);
                    Operationlog("TemplateCode=" + IicpCommonOperation.TemplateCode + "      NO=" + CurrentRow.No + "       BLType=MBL" + "         Bookid=" + CurrentRow.OceanBookingID, "BL列表记录日志");
                }
            }
            else
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Dictionary<string, object> dic = CreateBusinessParameter(ActionType.Edit, false);
                    IicpCommonOperation.EditHBL(CurrentRow.RefNo, CurrentRow.No, dic);
                    Operationlog("TemplateCode=" + IicpCommonOperation.TemplateCode + "       NO=" + CurrentRow.No + "        BLType=HBL" + "         Bookid=" + CurrentRow.OceanBookingID, "BL列表记录日志");
                }
            }
        }
        #endregion

        #region Print
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintBL)]
        public void BLCommand_PrintBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    IicpCommonOperation.PrintBillOfLoading(CurrentRow.ID);
                }
            }
        }
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintProfit)]
        public void BLCommand_PrintProfit(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    IicpCommonOperation.PrintBookingProfit(CurrentRow.OceanBookingID);
                }
            }

        }
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintLoadCtn)]
        public void BLCommand_PrintLoadCtn(object sender, EventArgs e)
        {
            if (!IsNullObject())
            {
            }
        }
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintLoadGoods)]
        public void BLCommand_PrintLoadGoods(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    if (CurrentRow.BLType == FCMBLType.MBL)
                    {
                        IicpCommonOperation.PrintLoadGoods(CurrentRow);
                    }
                    else
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Only the MBL print shipping order." : "只能MBL打印装货单.");
                    }
                }
            }

        }
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintLoadContainer)]
        public void BLCommand_PrintLoadContainer(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    if (CurrentRow.BLType == FCMBLType.MBL)
                    {
                        IicpCommonOperation.PrintLoadContainer(CurrentRow, false);
                    }
                    else
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Only the MBL print shipping order." : "只能MBL打印装货单.");
                    }
                }
            }

        }
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintLoadContainerCopy)]
        public void BLCommand_PrintLoadContainerCopy(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    if (CurrentRow.BLType == FCMBLType.MBL)
                    {
                        IicpCommonOperation.PrintLoadContainer(CurrentRow, true);
                    }
                    else
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Only the MBL print shipping order." : "只能MBL打印装货单.");
                    }
                }
            }

        }
        #endregion

        #region Refersh
        [CommandHandler(BLWorkSpaceConstants.BLCommand_Refersh)]
        public void BLCommand_Refersh(object sender, EventArgs e)
        {
            Refersh();
        }

        public void Refersh()
        {
            try
            {
                DataSource = OceanExportService.GetOceanBLListByOperationInfo(CurrentContext);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
        }

        #endregion

        #region Remove
        [CommandHandler(BLWorkSpaceConstants.BLCommand_Remove)]
        public void BLCommand_Remove(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    try
                    {
                        OceanBLList oceanBl = CurrentRow;
                        if (IicpCommonOperation.InnerDelete(CurrentRow, DataSource, bsBLList, CurrentContext) == true)
                        {
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),
                               oceanBl.No + " " + (LocalData.IsEnglish ? "Delete Successfully" : "删除成功"));
                            Refersh();
                        }
                    }
                    catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
                }
            }
        }
        #endregion

        #region Split BL
        [CommandHandler(BLWorkSpaceConstants.BLCommand_SplitBL)]
        public void BLCommand_SplitBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    IicpCommonOperation.SplitBillOfLoading(CurrentRow, null);
                }
            }
        }

        #endregion

        #region  Merge BL
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MergeBL)]
        public void BLCommand_MergeBL(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    IicpCommonOperation.MergeBillOfLoading(SelectedItems);
                }
            }

        }

        #endregion

        #region E-MBL
        [CommandHandler(BLWorkSpaceConstants.BLCommand_E_MBL)]
        public void BLCommand_E_MBL(object sender, EventArgs e)
        {
            if (!IsNullObject())
            {
                E_MBL();
            }
        }

        void E_MBL()
        {
            if (IsEqual())
            {
                bool isSucc = false;
                try
                {

                    if (CurrentRow.BLType != FCMBLType.MBL)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish ? "Please select only one MBL." : "请选择一个MBL提单.");
                        return;
                    }
                    OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(CurrentRow.MBLID);

                    if (mbl.BookingPartyID == null || mbl.BookingPartyID == Guid.Empty)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish ? "MBL has no booking party." : "MBL提单没有订舱人.");
                        return;
                    }

                    ConfigureInfo companyConfig = ConfigureService.GetCompanyConfigureInfoByCustomer((Guid)mbl.BookingPartyID);
                    if (companyConfig == null)
                    {
                        MessageBoxService.ShowInfo(LocalData.IsEnglish ? "MBL's booking party is wrong, cannot send EDI." : "MBL提单订舱人无效，不能发送EDI.");
                        return;
                    }
                    if (companyConfig.CompanyID != CurrentRow.CompanyID)
                    {
                        string message = LocalData.IsEnglish ? "MBL's booking party and MBL's company are different" + Environment.NewLine + "Are you sure send EDI by [" + companyConfig.CompanyName + "]" : "MBL提单订舱人所属公司和提单所属公司不同" + Environment.NewLine + "是否确定以订舱人[" + companyConfig.CompanyName + "]发送EDI？";
                        DialogResult result = MessageBoxService.ShowQuestion(message,
                                      LocalData.IsEnglish ? "Tip" : "提示",
                                      MessageBoxButtons.YesNo);
                        if (result == DialogResult.No)
                            return;
                    }


                    EMBLBusiness(CurrentContext.OperationType, SelectedItems, companyConfig.CompanyID, ref  isSucc);
                    if (isSucc)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                    }

                }
                catch (Exception ex)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
                }
            }

        }

        #endregion

        #region E-VGM
        [CommandHandler(BLWorkSpaceConstants.BLCommand_E_VGM)]
        public void BLCommand_E_VGM(object sender, EventArgs e)
        {
            if (!IsNullObject())
            {
                E_VGM();
            }
        }

        void E_VGM()
        {
            if (IsEqual())
            {
                bool isSucc = false;
                try
                {
                    if (CurrentRow == null || CurrentRow.BLType != FCMBLType.MBL)
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one MBL." : "请选择一个MBL提单.");
                        return;
                    }

                    IicpCommonOperation.InnerEVGM(CurrentRow, CurrentRow.CompanyID, ref isSucc, CurrentContext);
                    if (isSucc)
                    {
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                    }
                }
                catch (Exception ex)
                {
                    //Utility.ShowMessage(LocalData.IsEnglish ? "Send failed" : "发送失败!" + Environment.NewLine + ex.Message);
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex);
                }
            }

        }

        #endregion

        #region  新加按钮
        /// <summary>
        /// AMS按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AMS)]
        public void BLCommand_AMS(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    string subjuect = string.Empty;
                    if (CurrentRow.BLType != FCMBLType.HBL)
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                        return;
                    }
                    string toEmail = string.Empty;
                    List<Guid> oIds = new List<Guid>();
                    oIds.Add(CurrentRow.OceanBookingID);
                    List<Guid> hblIds = new List<Guid>();
                    hblIds.Add(CurrentRow.ID);
                    List<string> operationNos = new List<string>();
                    operationNos.Add(CurrentRow.No);
                    string key = "AMS";
                    string tip = string.Empty;
                    bool isSucc = false;
                    subjuect = "AMS(" + CurrentRow.No.Trim() + ")";

                    tip = "AMS";
                    SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.AMS);
                }
            }

        }
        /// <summary>
        /// ACI按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_ACI)]
        public void BLCommand_ACI(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != FCMBLType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OceanBookingID);
                List<Guid> hblIds = new List<Guid>();
                hblIds.Add(CurrentRow.ID);
                List<string> operationNos = new List<string>();
                operationNos.Add(CurrentRow.No);
                string key = "ACI";
                string tip = string.Empty;
                bool isSucc = false;
                subjuect = "ACI(" + CurrentRow.No.Trim() + ")";

                tip = "ACI";
                SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.ACI);

            }

        }
        /// <summary>
        /// CopyAMSTo按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_CopyAMSTo)]
        public void BLCommand_CopyAMSTo(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                ClientOceanExportService.CopyAMSTo(Workitem, CurrentRow);
            }

        }

        /// <summary>
        /// AMSACI按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AMSACI)]
        public void BLCommand_AMSACI(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != FCMBLType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OceanBookingID);
                List<Guid> hblIds = new List<Guid>();
                hblIds.Add(CurrentRow.ID);
                List<string> operationNos = new List<string>();
                operationNos.Add(CurrentRow.No);
                string key = "AMSACI";
                string tip = string.Empty;
                bool isSucc = false;
                subjuect = "AMS&ACI(" + CurrentRow.No.Trim() + ")";

                tip = "AMSACI";
                SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.AMSACI);
            }
        }


        /// <summary>
        /// AMSACIISF按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AMSACIISF)]
        public void BLCommand_AMSACIISF(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != FCMBLType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OceanBookingID);
                List<Guid> hblIds = new List<Guid>();
                hblIds.Add(CurrentRow.ID);
                List<string> operationNos = new List<string>();
                operationNos.Add(CurrentRow.No);
                string key = "AMSACIISF";
                string tip = string.Empty;
                bool isSucc = false;
                subjuect = "AMS&ACI&ISF(" + CurrentRow.No.Trim() + ")";

                tip = "AMSACIISF";
                SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.AMSACIISF);
            }

        }

        /// <summary>
        /// AMSISF按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AMSISF)]
        public void BLCommand_AMSISF(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != FCMBLType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OceanBookingID);
                List<Guid> hblIds = new List<Guid>();
                hblIds.Add(CurrentRow.ID);
                List<string> operationNos = new List<string>();
                operationNos.Add(CurrentRow.No);
                string key = "AMSISF";
                string tip = string.Empty;
                bool isSucc = false;
                subjuect = "AMS&ISF(" + CurrentRow.No.Trim() + ")";

                tip = "AMSISF";
                SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.AMSISF);
            }

        }
        /// <summary>
        /// ISF按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_ISF)]
        public void BLCommand_ISF(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != FCMBLType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OceanBookingID);
                List<Guid> hblIds = new List<Guid>();
                hblIds.Add(CurrentRow.ID);
                List<string> operationNos = new List<string>();
                operationNos.Add(CurrentRow.No);
                string key = "ISF";
                string tip = string.Empty;
                bool isSucc = false;
                subjuect = "ISF(" + CurrentRow.No.Trim() + ")";

                tip = "ISF";
                SendEDI(key, subjuect, oIds, hblIds, operationNos, isSucc, EDIMode.ISF);
            }

        }
        /// <summary>
        /// 确认AMS费用
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_ConfirmedAMS)]
        public void BLCommand_ConfirmedAMS(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    string subjuect = string.Empty;
                    if (CurrentRow.BLType != FCMBLType.HBL)
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                        return;
                    }
                    List<Guid> hblIds = new List<Guid>();
                    hblIds.Add(CurrentRow.ID);
                    IicpCommonOperation.ConfirmedAMS(hblIds.ToArray(),LocalData.UserInfo.LoginID);
                }
            }

        }
        #endregion

        #region     新加按钮  (王乐俊 2013-09-30)
        /// <summary>
        /// 确认补料中文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MailBLCopyToCustomerCHS)]
        public void BLCommand_MailBLCopyToCustomerCHS(object sender, EventArgs e)
        {
            Send(oceanBlList, false);
        }

        /// <summary>
        /// 确认补料英文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MailBLCopyToCustomerENG)]
        public void BLCommand_MailBLCopyToCustomerENG(object sender, EventArgs e)
        {
            Send(oceanBlList, true);
        }
        #endregion

        #region 向代理确认提单  (LL 2013-12-23)
        /// <summary>
        /// 向代理确认提单中文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MailAllBLCopyToAgentCHS)]
        public void BLCommand_MailAllBLCopyToAgentCHS(object sender, EventArgs e)
        {
            SendToAgent(oceanBlList, false);
        }

        /// <summary>
        /// 向代理确认提单英文版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MailAllBLCopyToAgentENG)]
        public void BLCommand_MailAllBLCopyToAgentENG(object sender, EventArgs e)
        {
            SendToAgent(oceanBlList, true);
        }
        #endregion

        #endregion

        #region 记录日志

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="msg">操作轨迹</param>
        /// <param name="name"></param>
        public void Operationlog(string msg, string name)
        {
            StreamWriter sw = File.AppendText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, name) + ".Log");
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss: ") + msg);
            sw.Close();
        }
        #endregion

        #region   列表F5操作
        void gvBLList_KeyDown(object sender, KeyEventArgs e)
        {
            if (
             e.KeyCode == Keys.F5
             && gvBLList.FocusedColumn != null
             && gvBLList.FocusedValue != null)
            {
                F5Query();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void F5Query()
        {
            string text = gvBLList.GetFocusedDisplayText().Replace("'", "''");
            if (text.ToLower().Contains("checked")) return;
            BusinessOperationContext businessOperation = new BusinessOperationContext();
            businessOperation.OperationNO = text;
            businessOperation.OperationID = Guid.NewGuid();
            ServiceClient.GetService<IICPCommonOperationService>().OpenTaskCenter(businessOperation);
        }
        #endregion

        #region Change AMS State
        private void ChangeAMSState(BLAMSState state)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    string subjuect = string.Empty;
                    if (CurrentRow.BLType != FCMBLType.HBL)
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                        return;
                    }
                    DialogResult dr = MessageBoxService.ShowWarning("请确认AMS信息是否一致，特别是船名信息", "提示", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    List<Guid> hblIds = new List<Guid>();
                    hblIds.Add(CurrentRow.ID);
                    SaveRequestBLState saveRequest = new SaveRequestBLState()
                    {
                        HBLIDs = hblIds.ToArray(),
                        AMSState = state,
                        SaveBy = LocalData.UserInfo.LoginID,
                    };
                    FCMCommonService.SaveOceanBLAMSState(saveRequest);
                }
            }
        }
        #endregion

        #region Comment Code
        private void PositionChanged(object sender, EventArgs e)
        {
            RefershToolBar();
        }
        #endregion

        
    }
}
