using DevExpress.XtraBars;
using DevExpress.XtraTreeList.Nodes;
using ICP.Business.Common.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.UI;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
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
    public partial class UCBLTreePart : BaseListPart, IBaseBLPart, IDataBind
    {
        #region Fields & Property & Services & Contact

        #region 变量
        /// <summary>
        /// BillOfLadingList 提单列表数据对象
        /// </summary>
        private BillOfLadingList currentItem = null;
        #endregion

        #region 常量
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, BarItem> _barItemDic = new Dictionary<string, BarItem>();
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
        /// ICP公用操作
        /// </summary>
        public IICPCommonOperationService ICPCommonOperationService
        {
            get { return ServiceClient.GetClientService<IICPCommonOperationService>(); }
        }
        /// <summary>
        /// 通用服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get { return ServiceClient.GetClientService<IFCMCommonService>(); }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// bsBLList数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return _BSMain.DataSource;
            }
            set
            {
                _BSMain.DataSource = value;
                _BSMain.ResetBindings(false);
                Workitem.Commands[BLWorkSpaceConstants.BLCommand_PositionChanged].Execute();
            }
        }

        /// <summary>
        /// 业务操作上下文
        /// </summary>
        public BusinessOperationContext CurrentContext { get; set; }

        /// <summary>
        /// 当前行
        /// </summary>
        public BillOfLadingList CurrentRow { get { return _BSMain.Current == null ? null : _BSMain.Current as BillOfLadingList; } }

        /// <summary>
        /// gvBLList选中行
        /// </summary>
        List<BillOfLadingList> SelectedItems
        {
            get
            {
                if (treeMain.Selection == null || treeMain.Selection.Count == 0) return null;

                List<BillOfLadingList> tagers = new List<BillOfLadingList>();
                foreach (TreeListNode item in treeMain.Selection)
                {
                    BillOfLadingList bl = treeMain.GetDataRecordByNode(item) as BillOfLadingList;
                    tagers.Add(bl);
                }
                return tagers;
            }
        }

        
        #endregion

        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public UCBLTreePart()
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
            bartoAgentchs.ItemClick += bartoAgentchs_ItemClick;
            bartoAgenteng.ItemClick += bartoAgenteng_ItemClick;

            treeMain.DoubleClick += TreeMain_DoubleClick;
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
            //gvBLList.KeyDown += gvBLList_KeyDown;
            //宁波口岸
            if (LocalData.UserInfo.DefaultCompanyID != new Guid("A62A9F8E-E69C-4E6E-AD85-E75AED3C6CF9"))
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
        /// 
        /// </summary>
        private void TreeMain_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentRow != null) Workitem.Commands[BLWorkSpaceConstants.BLCommand_Edit].Execute();
        }
        /// <summary>
        /// Is Finish
        /// </summary>
        void barISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_ISF].Execute();
        }
        /// <summary>
        /// AMS Is Finish
        /// </summary>
        void barAMSISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AMSISF].Execute();
        }
        /// <summary>
        /// AMSACI Is Finish
        /// </summary>
        void barAMSACIISF_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AMSACIISF].Execute();
        }
        /// <summary>
        /// 点击AMSACI
        /// </summary>
        void barAMSACI_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AMSACI].Execute();
        }
        /// <summary>
        /// Copy AMS To
        /// </summary>
        void barCopyAMSTo_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_CopyAMSTo].Execute();
        }
        /// <summary>
        /// ACI
        /// </summary>
        void barACI_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_ACI].Execute();
        }
        /// <summary>
        /// AMS
        /// </summary>
        void barAMS_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AMS].Execute();
        }

        /// <summary>
        /// EDI MBL
        /// </summary>
        void barEMBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_E_MBL].Execute();
        }

        /// <summary>
        /// EDI VGM
        /// </summary>
        void barEVGM_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_E_VGM].Execute();
        }

        /// <summary>
        /// Merge BL
        /// </summary>
        void barMergeBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MergeBL].Execute();
        }
        /// <summary>
        /// Split BL
        /// </summary>
        void barSplitBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_SplitBL].Execute();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        void barRefersh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Refersh].Execute();
        }
        /// <summary>
        /// 打印装货单
        /// </summary>
        void barPrintLoadGoods_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintLoadGoods].Execute();
        }

        /// <summary>
        /// 打印装箱单
        /// </summary>
        void barPrintLoadContainer_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintLoadContainer].Execute();
        }
        /// <summary>
        /// 打印装箱单副本
        /// </summary>
        void barPrintLoadContainerCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintLoadContainerCopy].Execute();
        }
        /// <summary>
        /// 打印装箱单
        /// </summary>
        void barPrintLoadCtn_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintLoadCtn].Execute();
        }
        /// <summary>
        /// 打印利润表
        /// </summary>
        void barProfit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintProfit].Execute();
        }
        /// <summary>
        /// 打印提单
        /// </summary>
        void barPrintBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_PrintBL].Execute();
        }
        /// <summary>
        /// 删除提单
        /// </summary>
        void barDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Remove].Execute();
        }
        /// <summary>
        /// 添加HBL
        /// </summary>
        void barAddHBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AddHBL].Execute();
        }
        /// <summary>
        /// 添加MBL
        /// </summary>
        void barAddMBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_AddMBL].Execute();
        }
        /// <summary>
        /// 添加MBL
        /// </summary>
        void barDeclarationBL_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_DeclarationBL].Execute();
        }
        /// <summary>
        /// 编辑提单
        /// </summary>
        void barEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Edit].Execute();
        }
        /// <summary>
        /// 复制提单
        /// </summary>
        void barCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_Copy].Execute();
        }
        /// <summary>
        /// 客户确认补料(中文)
        /// </summary>
        void barchs_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MailBLCopyToCustomerCHS].Execute();
        }
        /// <summary>
        /// 客户确认补料(英文)
        /// </summary>
        void bareng_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MailBLCopyToCustomerENG].Execute();
        }
        /// <summary>
        /// 向代理确认所有提单 (中文版)
        /// </summary>
        void bartoAgentchs_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MailAllBLCopyToAgentCHS].Execute();
        }
        /// <summary>
        /// 向代理确认所有提单 (英文版)
        /// </summary>
        void bartoAgenteng_ItemClick(object sender, ItemClickEventArgs e)
        {
            Workitem.Commands[BLWorkSpaceConstants.BLCommand_MailAllBLCopyToAgentENG].Execute();
        }
        #endregion

        #region DataBind
        /// <summary>
        /// bsBLList绑定数据
        /// </summary>
        /// <param name="blList">数据源</param>
        public void BindData(List<BillOfLadingList> blList)
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
            CurrentContext = context;
            WaitCallback callback = (temp) =>
            {
                try
                {
                    ClientHelper.SetApplicationContext();
                    List<BillOfLadingList> blList = FCMCommonService.GetBillOfLadingList(context);
                    if (IsDisposed)
                        return;
                    BillOfLadingListBindDataDelegate bindDelegate = BindData;
                    if (blList.Any())
                    {
                        if (blList[0].OperationID != context.OperationID)
                        {
                            blList = new List<BillOfLadingList>();
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
            return ICPCommonOperationService.TemplateCode == TemplateCode;
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
            businessOperation.TemplateCode = ICPCommonOperationService.TemplateCode;
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
        public void EMBLBusiness(OperationType operationType, List<BillOfLadingList> selectedList, Guid companyID, ref bool isSucc)
        {
            switch (operationType)
            {
                case OperationType.OceanExport:
                    //TODO:IicpCommonOperation.InnerEMBL(selectedList, companyID, AMSEntryType.Unknown, ACIEntryType.Unknown, ref isSucc, CurrentContext);
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
        public void EISFBusiness(OperationType operationType, BillOfLadingList listData)
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
            //TODO:IicpCommonOperation.SendEdiamsaicisf(key, subjuect, oIds, hblIds, operationNos, isSucc, ediMode, CurrentRow.CompanyID, CurrentContext);
        }

        /// <summary>
        /// 发送至代理
        /// </summary>
        /// <param name="currentItem"></param>
        /// <param name="flg"></param>
        public void SendToAgent(BillOfLadingList currentItem, bool flg)
        {
            //TODO:Send To Agent
            //if (currentItem != null)
            //{
            //    string result = IicpCommonOperation.MailALLBLCopyToAgent(flg, currentItem.OperationID);
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), result);
            //        return;
            //    }
            //}
        }

        /// <summary>
        /// 发送邮件的方法
        /// </summary>
        /// <param name="currentItem"></param>
        /// <param name="flg"></param>
        public void Send(BillOfLadingList currentItem, bool flg)
        {
            //TODO:Send Email
            //if (currentItem != null)
            //{
            //    OceanMBLInfo oceanMblInfo = null;
            //    OceanHBLInfo oceanHbl = null;
            //    if (currentItem.BLType == FCMBLType.MBL)
            //    {
            //        //MBL
            //        oceanMblInfo = new OceanMBLInfo
            //        {
            //            No = currentItem.No,
            //            ReleaseType = currentItem.ReleaseType,
            //            ID = currentItem.MBLID
            //        };
            //    }
            //    else
            //    {
            //        //HBL;
            //        oceanHbl = new OceanHBLInfo
            //        {
            //            No = currentItem.No,
            //            ReleaseType = currentItem.ReleaseType,
            //            ID = currentItem.ID
            //        };
            //    }

            //    string result = IicpCommonOperation.MailCustomerAskForConfirmSI(flg, currentItem.OceanBookingID,
            //                                                                      oceanHbl, oceanMblInfo);
            //    if (!string.IsNullOrEmpty(result))
            //    {
            //        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), result);
            //    }
            //}
        }
        #endregion

        #region 工具栏事件
        /// <summary>
        /// 设置工具栏启用状态
        /// </summary>
        /// <param name="listData"></param>
        void SetToolEnable(BillOfLadingList listData)
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
                //TODO:启用禁用合并/拆分提单功能
                //if (listData.IsValid == false)
                //{
                //}
                //else
                //{
                //    barSplitBL.Enabled = false;
                //    barMergeBL.Enabled = false;
                //    if (listData.State != OEBLState.Checked && listData.State != OEBLState.Release)
                //    {
                //        barMergeBL.Enabled = false;
                //    }
                //    if (listData.State != OEBLState.Checked && listData.State != OEBLState.Release && listData.HBLCount <= 0)
                //    {
                //        barSplitBL.Enabled = false;
                //    }
                //}
            }
        }

        /// <summary>
        /// 设置工具栏
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PositionChanged)]
        public void BLCommand_PositionChanged(object sender, EventArgs e)
        {
            if (Workitem.State[BLWorkSpaceConstants.BLCommandState_BLInfo] == null) return;
            BillOfLadingList listData = (BillOfLadingList)Workitem.State[BLWorkSpaceConstants.BLCommandState_BLInfo];
            SetToolEnable(listData);
        }

        #region 添加MBL
        /// <summary>
        /// 
        /// </summary>
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
                        ICPCommonOperationService.AddMBL(dic);
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
        /// <summary>
        /// 
        /// </summary>
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
                        ICPCommonOperationService.AddHBL(dic);
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
                        ICPCommonOperationService.DeclarationBL("", values);
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
        public void Copy(BillOfLadingList listData)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {

                if (listData == null) return;

                //if (listData.BLType == FCMBLType.MBL)
                //{
                //    ICPCommonOperationService.InnerCopyMBLData(listData);
                //}
                //else
                //{
                //    ICPCommonOperationService.InnerCopyHBLData(listData);
                //}
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
            if (CurrentRow.BLType == BillOfLadingType.MBL)
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Dictionary<string, object> dictionary = CreateBusinessParameter(ActionType.Edit, false);
                    ICPCommonOperationService.EditMBL(CurrentRow.OperationNo, CurrentRow.No, dictionary);
                    Operationlog("TemplateCode=" + ICPCommonOperationService.TemplateCode + "      NO=" + CurrentRow.No + "       BLType=MBL" + "         Bookid=" + CurrentRow.OperationID, "BL列表记录日志");
                }
            }
            else
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    Dictionary<string, object> dic = CreateBusinessParameter(ActionType.Edit, false);
                    ICPCommonOperationService.EditHBL(CurrentRow.OperationNo, CurrentRow.No, dic);
                    Operationlog("TemplateCode=" + ICPCommonOperationService.TemplateCode + "       NO=" + CurrentRow.No + "        BLType=HBL" + "         Bookid=" + CurrentRow.OperationID, "BL列表记录日志");
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
                    ICPCommonOperationService.PrintBillOfLoading(CurrentRow.ID);
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
                    ICPCommonOperationService.PrintBookingProfit(CurrentRow.OperationID);
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
                    //if (CurrentRow.BLType == FCMBLType.MBL)
                    //{
                    //    ICPCommonOperationService.PrintLoadGoods(CurrentRow);
                    //}
                    //else
                    //{
                    //    Utility.ShowMessage(LocalData.IsEnglish ? "Only the MBL print shipping order." : "只能MBL打印装货单.");
                    //}
                }
            }

        }
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintLoadContainer)]
        public void BLCommand_PrintLoadContainer(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                //if (!IsNullObject())
                //{
                //    if (CurrentRow.BLType == FCMBLType.MBL)
                //    {
                //        ICPCommonOperationService.PrintLoadContainer(CurrentRow, false);
                //    }
                //    else
                //    {
                //        Utility.ShowMessage(LocalData.IsEnglish ? "Only the MBL print shipping order." : "只能MBL打印装货单.");
                //    }
                //}
            }

        }
        [CommandHandler(BLWorkSpaceConstants.BLCommand_PrintLoadContainerCopy)]
        public void BLCommand_PrintLoadContainerCopy(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    //if (CurrentRow.BLType == FCMBLType.MBL)
                    //{
                    //    ICPCommonOperationService.PrintLoadContainer(CurrentRow, true);
                    //}
                    //else
                    //{
                    //    Utility.ShowMessage(LocalData.IsEnglish ? "Only the MBL print shipping order." : "只能MBL打印装货单.");
                    //}
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

        private void Refersh()
        {
            try
            {
                DataSource = FCMCommonService.GetBillOfLadingList(CurrentContext);
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
                        BillOfLadingList oceanBl = CurrentRow;
                        //if (IicpCommonOperation.InnerDelete(CurrentRow, DataSource, _BSMain, CurrentContext) == true)
                        //{
                        //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(),
                        //       oceanBl.No + " " + (LocalData.IsEnglish ? "Delete Successfully" : "删除成功"));
                        //    Refersh();
                        //}
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
                    //IicpCommonOperation.SplitBillOfLoading(CurrentRow, null);
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
                    //IicpCommonOperation.MergeBillOfLoading(SelectedItems);
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

                    if (CurrentRow.BLType != BillOfLadingType.MBL)
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one MBL." : "请选择一个MBL提单.");
                        return;
                    }
                    //OceanMBLInfo mbl = OceanExportService.GetOceanMBLInfo(CurrentRow.MBLID);

                    //if (mbl.BookingPartyID == null || mbl.BookingPartyID == Guid.Empty)
                    //{
                    //    Utility.ShowMessage(LocalData.IsEnglish ? "MBL has no booking party." : "MBL提单没有订舱人.");
                    //    return;
                    //}

                    //ConfigureInfo companyConfig = ConfigureService.GetCompanyConfigureInfoByCustomer((Guid)mbl.BookingPartyID);
                    //if (companyConfig == null)
                    //{
                    //    Utility.ShowMessage(LocalData.IsEnglish ? "MBL's booking party is wrong, cannot send EDI." : "MBL提单订舱人无效，不能发送EDI.");
                    //    return;
                    //}
                    //if (companyConfig.CompanyID != CurrentRow.CompanyID)
                    //{
                    //    string message = LocalData.IsEnglish ? "MBL's booking party and MBL's company are different" + Environment.NewLine + "Are you sure send EDI by [" + companyConfig.CompanyName + "]" : "MBL提单订舱人所属公司和提单所属公司不同" + Environment.NewLine + "是否确定以订舱人[" + companyConfig.CompanyName + "]发送EDI？";
                    //    DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                    //                  LocalData.IsEnglish ? "Tip" : "提示",
                    //                  MessageBoxButtons.YesNo,
                    //                  MessageBoxIcon.Question);
                    //    if (result == DialogResult.No)
                    //        return;
                    //}


                    //EMBLBusiness(CurrentContext.OperationType, SelectedItems, companyConfig.CompanyID, ref  isSucc);
                    //if (isSucc)
                    //{
                    //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Send Successfully!" : "发送成功!");
                    //}

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
                    if (CurrentRow == null || CurrentRow.BLType != BillOfLadingType.MBL)
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one MBL." : "请选择一个MBL提单.");
                        return;
                    }

                    //ICPCommonOperationService.InnerEVGM(CurrentRow, CurrentRow.CompanyID, ref isSucc, CurrentContext);
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
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AMS)]
        public void BLCommand_AMS(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (!IsNullObject())
                {
                    string subjuect = string.Empty;
                    if (CurrentRow.BLType != BillOfLadingType.HBL)
                    {
                        Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                        return;
                    }
                    string toEmail = string.Empty;
                    List<Guid> oIds = new List<Guid>();
                    oIds.Add(CurrentRow.OperationID);
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
        [CommandHandler(BLWorkSpaceConstants.BLCommand_ACI)]
        public void BLCommand_ACI(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != BillOfLadingType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OperationID);
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
        [CommandHandler(BLWorkSpaceConstants.BLCommand_CopyAMSTo)]
        public void BLCommand_CopyAMSTo(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                //ClientOceanExportService.CopyAMSTo(Workitem, CurrentRow);
            }

        }

        /// <summary>
        /// AMSACI按钮
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AMSACI)]
        public void BLCommand_AMSACI(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != BillOfLadingType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OperationID);
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
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AMSACIISF)]
        public void BLCommand_AMSACIISF(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != BillOfLadingType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OperationID);
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
        [CommandHandler(BLWorkSpaceConstants.BLCommand_AMSISF)]
        public void BLCommand_AMSISF(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != BillOfLadingType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OperationID);
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
        [CommandHandler(BLWorkSpaceConstants.BLCommand_ISF)]
        public void BLCommand_ISF(object sender, EventArgs e)
        {
            if (IsEqual())
            {
                if (IsNullObject()) return;
                if (CurrentRow.BLType != BillOfLadingType.HBL)
                {
                    Utility.ShowMessage(LocalData.IsEnglish ? "Please select only one HBL." : "请选择一个HBL提单.");
                    return;
                }
                string subjuect = string.Empty;
                string toEmail = string.Empty;
                List<Guid> oIds = new List<Guid>();
                oIds.Add(CurrentRow.OperationID);
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

        #endregion

        #region     新加按钮  (王乐俊 2013-09-30)
        /// <summary>
        /// 确认补料中文版
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MailBLCopyToCustomerCHS)]
        public void BLCommand_MailBLCopyToCustomerCHS(object sender, EventArgs e)
        {
            Send(currentItem, false);
        }

        /// <summary>
        /// 确认补料英文版
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MailBLCopyToCustomerENG)]
        public void BLCommand_MailBLCopyToCustomerENG(object sender, EventArgs e)
        {
            Send(currentItem, true);
        }
        #endregion

        #region 向代理确认提单  (LL 2013-12-23)
        /// <summary>
        /// 向代理确认提单中文版
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MailAllBLCopyToAgentCHS)]
        public void BLCommand_MailAllBLCopyToAgentCHS(object sender, EventArgs e)
        {
            SendToAgent(currentItem, false);
        }

        /// <summary>
        /// 向代理确认提单英文版
        /// </summary>
        [CommandHandler(BLWorkSpaceConstants.BLCommand_MailAllBLCopyToAgentENG)]
        public void BLCommand_MailAllBLCopyToAgentENG(object sender, EventArgs e)
        {
            SendToAgent(currentItem, true);
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
        private void ToolStripMenuItemF5_Click(object sender, EventArgs e)
        {
            F5Query();
        }
        /// <summary>
        /// 
        /// </summary>
        public void F5Query()
        {
            string text = treeMain.FocusedNode.GetDisplayText("").Replace("'", "''");
            if (text.ToLower().Contains("checked")) return;
            BusinessOperationContext businessOperation = new BusinessOperationContext();
            businessOperation.OperationNO = text;
            businessOperation.OperationID = Guid.NewGuid();
            ServiceClient.GetService<IICPCommonOperationService>().OpenTaskCenter(businessOperation);
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
