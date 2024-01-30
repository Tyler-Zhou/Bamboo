using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.UI.Business;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FCM.OtherBusiness.UI.Common
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public partial class OBListPart : BaseListPart
    {
        #region 字段 & 属性 & 事件
        bool _ShownTips = false;
        /// <summary>
        /// 
        /// </summary>
        public virtual string CommandEditData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CommandShowSearch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual AddType AddType
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public AddType addType
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public new event KeyEventHandler KeyDown;
        #endregion
        
        #region 服务注入
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OtherBusinessPrintHelper OtherBusinessPrintHelper
        {
            get
            {
                return ClientHelper.Get<OtherBusinessPrintHelper, OtherBusinessPrintHelper>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOtherBusinessService OtherBusinessService
        {
            get
            {
                return ServiceClient.GetService<IOtherBusinessService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public OtherUIHelper ExportUIHelper
        {
            get
            {
                return ClientHelper.Get<OtherUIHelper, OtherUIHelper>();
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public OBListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                CurrentChanged = null;
                CurrentChanging = null;
                gcMain.DataSource = null;
                bsOBList.PositionChanged -= bsOBList_PositionChanged;
                bsOBList.DataSource = null;
                bsOBList.Dispose();

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            };

            Load += OrderListPart_Load;

            if (!LocalData.IsEnglish)
            {
                SetCnText();

            }
        }
        void OrderListPart_Load(object sender, EventArgs e)
        {
            DataSource = new List<OtherBusinessList>();
            _ShownTips = true;

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControls();
        }

        #region IListPart成员
        /// <summary>
        /// 
        /// </summary>
        public override event CurrentChangedHandler CurrentChanged;
        /// <summary>
        /// 
        /// </summary>
        public override event CancelEventHandler CurrentChanging;
        /// <summary>
        /// 
        /// </summary>
        public override object Current
        {
            get { return bsOBList.Current; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected OtherBusinessList CurrentRow
        {
            get { return Current as OtherBusinessList; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsOBList.DataSource;
            }
            set
            {
                List<OtherBusinessList> list = value as List<OtherBusinessList>;
                string message = LocalData.IsEnglish ? "{0} records found" : "查询到 {0} 条记录";
                if (list == null || list.Count == 0)
                {
                    message = string.Format(message, 0);
                    bsOBList.DataSource = typeof(OtherBusinessList);
                }
                else
                {
                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, Current);
                    }
                    SetColumnsWidth();
                    bsOBList.DataSource = list;
                    bsOBList.ResetBindings(false);
                    message = string.Format(message, list.Count);
                }
                if (_ShownTips)
                {
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), message);
                }
            }
        }
        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void InitControls()
        {
            Utility.ShowGridRowNo(gvMain);
            //订单状态
            List<EnumHelper.ListItem<OBOrderState>> orderStates = EnumHelper.GetEnumValues<OBOrderState>(LocalData.IsEnglish);
            foreach (var item in orderStates)
            {
                rcmState.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
        }
        private void SetCnText()
        {
            colNO.Caption = "业务号";
            colIsValid.Caption = "是否有效";
            colState.Caption = "状态";
            colEta.Caption = "ETA";
            colEtd.Caption = "ETD";
            colCustomerName.Caption = "客户";
            colShipperName.Caption = "发货人";
            colConsigneeName.Caption = "收货人";
            colFeta.Caption = "FETA";
            colHblno.Caption = "HBL No";
            colMblno.Caption = "MBL No";
            colNotifyPartyName.Caption = "通知人";
            colFinalDestinationName.Caption = "交货地";
            colConsigneeName.Caption = "发货人";
            colShipperName.Caption = "收货人";
            colCarrierName.Caption = "船东";
            colAgengofCarrierName.Caption = "代理人";
            colPolName.Caption = "起运港";
            colPodName.Caption = "目的港";
            colVesselVoyage.Caption = "船名航次";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public void GridCurrentChanged(object sender, object data)
        {
            if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        }

        void SetColumnsWidth()
        {
            gvMain.BestFitColumns();
        }
        public override void Refresh(object items)
        {
            List<OtherBusinessList> list = DataSource as List<OtherBusinessList>;
            if (list == null) return;
            List<OtherBusinessList> newLists = items as List<OtherBusinessList>;

            foreach (var item in newLists)
            {
                OtherBusinessList tager = list.Find(delegate(OtherBusinessList jItem) { return item.ID == jItem.ID; });
                if (tager == null) continue;

                Utility.CopyToValue(item, tager, typeof(OtherBusinessList));
            }
            bsOBList.ResetBindings(false);
        }

        public override void RemoveItem(int index)
        {
            bsOBList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsOBList.Remove(item);
        }
        #region GRID EVENTS
        private void gvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2 && CurrentRow != null)
                Workitem.Commands[CommandEditData].Execute();
        }

        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[CommandEditData].Execute();
            }
            else if (KeyDown != null
                 && e.KeyCode == Keys.F5
                 && gvMain.FocusedColumn != null
                 && gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();//.GetDisplayText(treeMain.FocusedColumn);
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                KeyDown(keyValue, null);
            }
            else if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[CommandShowSearch].Execute();
            }
        }

        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                Workitem.Commands[CommandEditData].Execute();
            }
        }

        private void gvMain_BeforeLeaveRow(object sender, RowAllowEventArgs e)
        {
            if (CurrentChanging != null)
            {
                CancelEventArgs ce = new CancelEventArgs();
                CurrentChanging(this, ce);
                e.Allow = !ce.Cancel;
            }
        }

        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            OtherBusinessList list = gvMain.GetRow(e.RowHandle) as OtherBusinessList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
            else if (list.State == OBOrderState.NewOrder)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
            }
            else if (list.State == OBOrderState.MBLConfirmed)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Error);
            }
        }

        private void bsOBList_PositionChanged(object sender, EventArgs e)
        {
            Workitem.State[OrderStateConstants.CurrentRow] = CurrentRow;

            if (CurrentChanged != null) CurrentChanged(this, Current);
        }

        private void gvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
        #endregion
    }
}
