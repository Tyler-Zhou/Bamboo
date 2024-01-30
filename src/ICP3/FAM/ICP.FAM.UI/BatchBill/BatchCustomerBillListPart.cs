using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FAM.UI.BatchBill
{
    /// <summary>
    /// 批量账单管理界面
    /// </summary>
    [ToolboxItem(false)]
    public partial class BatchCustomerBillListPart : BaseListPart
    {
        #region Fields & Services & Property & Delegate
        #region Services
        /// <summary>
        /// WorkItem
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 财务服务
        /// </summary>
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
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
        /// 报表服务
        /// </summary>
        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }
        /// <summary>
        /// FCM通用服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 汇率服务
        /// </summary>
        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }
        #endregion

        #region Property

        BillList CurrentRow
        {
            get
            {
                if (bsBillList.Current == null) return null;
                return bsBillList.Current as BillList;
            }
        }

        public override object Current
        {
            get { return bsBillList.Current; }
        }

        public override object DataSource
        {
            get
            {
                return bsBillList.DataSource;
            }
            set
            {
                bsBillList.DataSource = value;
                bsBillList.ResetBindings(false);
                SetColumnsWidth();
                if (CurrentChanged != null) 
                    CurrentChanged(this, Current);
            }
        }
        /// <summary>
        /// 选择的账单
        /// </summary>
        IEnumerable<BillList> _SelectBillList
        {
            get
            {
                List<BillList> tagers = new List<BillList>();
                List<BillList> billLists = DataSource as List<BillList>;
                if (billLists != null)
                {
                    tagers.AddRange(billLists.Where(blItem => blItem.Selected));
                }
                return tagers;
            }
        }

        #endregion

        #region Delegate
        public override event CurrentChangedHandler CurrentChanged;
        public override event CancelEventHandler CurrentChanging;
        #endregion 
        #endregion

        #region Init
        /// <summary>
        /// 批量账单管理界面
        /// </summary>
        public BatchCustomerBillListPart()
        {
            InitializeComponent();
            if (DesignMode) return;
            RegisterEvent();
            Disposed += delegate
            {
                UnRegisterEvent();
                if (Workitem != null) Workitem.Items.Remove(this);
            };
        }
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="values"></param>
        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
            }
        }
        /// <summary>
        /// 加载
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (DesignMode)
            {
                return;
            }
            InitMessage();
            InitControls();
            bsBillList.ResetBindings(false);
        }
        
        #endregion

        #region Contorls Event
        #region BarItem Click
        /// <summary>
        /// Add Batch Bill
        /// </summary>
        [CommandHandler(BatchBillCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            try
            {
                FinanceClientService.BatchAddBill(null);
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }
        /// <summary>
        /// 打印账单
        /// </summary>
        [CommandHandler(BatchBillCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {
            try
            {
                using (new CursorHelper(Cursors.WaitCursor))
                {
                    List<Guid> tagers = new List<Guid>();
                    tagers.AddRange(_SelectBillList.Select(blItem => blItem.ID));
                    if (!_SelectBillList.Any())
                        return;
                    FinanceClientService.PrintBatchBill(_SelectBillList.First().CustomerID,_SelectBillList.First().CompanyID, tagers.ToArray(), LocalData.UserInfo.LoginID);
                }
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex); }
        }
        #endregion

        #region GridView Event
        /// <summary>
        /// 焦点离开行之前
        /// </summary>
        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }
        /// <summary>
        /// 行改变
        /// </summary>
        private void bsBillList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }
        /// <summary>
        /// 列样式改变
        /// </summary>
        private void gvBillList_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            BillList list = gvBillList.GetRow(e.RowHandle) as BillList;
            if (list == null || list.Type == BillType.DC)
            {
                e.Appearance.ForeColor = Color.FromArgb(0, 0, 155);
                e.Appearance.Options.UseForeColor = true;
            }
        }
        /// <summary>
        /// 点击选择
        /// </summary>
        private void gvBillList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            if (e.Column == colSelected)
            {
                CurrentRow.Selected = !CurrentRow.Selected;
                bsBillList.ResetCurrentItem();
            }
        }
        /// <summary>
        /// 双击
        /// </summary>
        void gvBillList_DoubleClick(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }

                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(CurrentRow.OperationID, CurrentRow.OperationType);
                if (operationCommonInfo != null)
                {
                    FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
                }
                else
                {
                    FAMUtility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"无对应的数据,请联系系统管理员!");
                }
            }
        }
        /// <summary>
        /// 行号
        /// </summary>
        void gvBillList_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
        #endregion

        #endregion

        #region Custom Method
        /// <summary>
        /// 提示信息
        /// </summary>
        private void InitMessage()
        {
            RegisterMessage("PreventPrint", LocalData.IsEnglish ? "Prevent Print" : "存在不同客户或计费日期账单，禁止打印!");
        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            //State
            List<EnumHelper.ListItem<BillState>> billStates = EnumHelper.GetEnumValues<BillState>(LocalData.IsEnglish);
            foreach (var item in billStates)
            {
                cmbState.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            //BillType
            List<EnumHelper.ListItem<BillType>> billTypes = EnumHelper.GetEnumValues<BillType>(LocalData.IsEnglish);
            foreach (var item in billTypes)
            {
                if (item.Value == BillType.None) continue;
                cmbType.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }
            //OperationType
            List<EnumHelper.ListItem<OperationType>> operationType = EnumHelper.GetEnumValues<OperationType>(LocalData.IsEnglish);
            foreach (var item in operationType)
            {
                if (item.Value == OperationType.Unknown) continue;
                cmbOperationType.Items.Add(new ImageComboBoxItem(item.Name, item.Value, (short)item.Value));
            }
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            gvBillList.RowCellClick += gvBillList_RowCellClick;
            gvBillList.DoubleClick += gvBillList_DoubleClick;
            gvBillList.CustomDrawRowIndicator += gvBillList_CustomDrawRowIndicator;
        }
        /// <summary>
        /// 移除事件
        /// </summary>
        private void UnRegisterEvent()
        {
            gvBillList.RowCellClick -= gvBillList_RowCellClick;
            gvBillList.DoubleClick -= gvBillList_DoubleClick;
            gvBillList.CustomDrawRowIndicator -= gvBillList_CustomDrawRowIndicator;
        }

        void SetColumnsWidth()
        {
            gvBillList.BestFitColumns();
        }
        #endregion
    }
}
