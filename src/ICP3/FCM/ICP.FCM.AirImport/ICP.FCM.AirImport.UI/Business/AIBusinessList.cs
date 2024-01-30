using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.AirImport.ServiceInterface;
using ICP.FCM.AirImport.UI.Common;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;

namespace ICP.FCM.AirImport.UI
{
    [ToolboxItem(false)]
    public partial class OIBusinessList : ICP.Framework.ClientComponents.UIFramework.BaseListPart
    {
        public OIBusinessList()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.CurrentChanged = null;
                this.KeyDown = null;
                this.gcMain.DataSource = null;
                this.bsList.DataSource = null;
                this.bsList.Dispose();

                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }

            };
        }

        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }


        public IAirImportService AirImportService
        {
            get
            {
                return ServiceClient.GetService<IAirImportService>();
            }
        }


        public AirImportPrintHelper AirImportPrintHelper
        {
            get
            {
                return ClientHelper.Get<AirImportPrintHelper, AirImportPrintHelper>();
            }
        }

        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }


        public IClientAirImportService ClientAirImportService
        {
            get { return ServiceClient.GetClientService<IClientAirImportService>(); }
        }
        #endregion

        #region 私有字段

        /// <summary>
        /// 当前活动行
        /// </summary>
        private AirBusinessList CurrentRow
        {
            get
            {
                return bsList.Current as AirBusinessList;
            }
        }
        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessage();
                InitControls();
            }
        }

        private void InitControls()
        {
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<AIOrderState>> orderStates = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<AIOrderState>(LocalData.IsEnglish);
            foreach (var item in orderStates)
            {
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
        }

        private void InitMessage()
        {
            this.RegisterMessage("11091600001", LocalData.IsEnglish ? "Please save MBL info" : "请先保存MBL信息");
        }

        #endregion

        #region 按钮事件

        #region 新增 & 复制
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_AddData)]
        public void Command_AddData(object sender, EventArgs e)
        {
            ClientAirImportService.AddBooking(null, EditPartSaved);
        }

        /// <summary>
        /// 复制 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_CopyData)]
        public void Command_CopyData(object sender, EventArgs e)
        {
            ClientAirImportService.CopyBooking(ShowCriteria, null, EditPartSaved);
        }
        #endregion

        #region 编辑

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e)
        {
            ClientAirImportService.EditBooking(ShowCriteria, null, EditPartSaved);
        }

        #endregion

        #region 账单

        [CommandHandler(AIBusinessCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            ClientAirImportService.OpenBill(CurrentRow.ID);
        }

        #endregion

        #region 打印报表

        /// <summary>
        /// 打印到港通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_PrintArrivalNotice)]
        public void Command_PrintArrivalNotice(object sender, EventArgs e)
        {
            if (this.CurrentRow == null) return;
            ClientAirImportService.PrintArrivalNotice(CurrentRow.ID);
        }

        /// <summary>
        /// 打印放货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_PrintReleaseOrder)]
        public void Command_PrintReleaseOrder(object sender, EventArgs e)
        {
            if (this.CurrentRow == null) return;
            ClientAirImportService.PrintReleaseOrder(CurrentRow.ID);
        }

        /// <summary>
        /// 打印利润表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_PrintProfit)]
        public void Command_PrintProfit(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ClientAirImportService.PrintProfit(CurrentRow.ID);

        }

        /// <summary>
        /// 打印Authority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_PrintAuthority)]
        public void Command_PrintAuthority(object sender, EventArgs e)
        {
            if (this.CurrentRow == null) return;
            ClientAirImportService.PrintAuthority(CurrentRow.ID);
        }

        private ICP.Message.ServiceInterface.Message GetOperationInfo()
        {
            if (CurrentRow == null)
                return null;
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.UserProperties = new ICP.Message.ServiceInterface.MessageUserPropertiesObject();
            message.UserProperties.OperationType = ICP.Framework.CommonLibrary.Common.OperationType.AirImport;
            message.UserProperties.OperationId = CurrentRow.ID;
            message.UserProperties.FormType = ICP.Framework.CommonLibrary.Common.FormType.Booking;
            message.UserProperties.FormId = CurrentRow.ID;

            return message;
        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_CancelData)]
        public void Command_CancelData(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            ClientAirImportService.CancelBooking(CurrentRow.ID, EditPartSaved);
        }

        #endregion

        #region 刷新
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_RefreshData)]
        public void Command_RefreshData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<AirBusinessList> blList = DataSource as List<AirBusinessList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList)
                    {
                        ids.Add(item.ID);
                    }

                    List<AirBusinessList> list = AirImportService.GetBusinessListByIds(ids.ToArray());
                    this.DataSource = list;
                }
                catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }
            }
        }
        #endregion

        #region 提货通知书
        /// <summary>
        /// 提货通知书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_CargoBook)]
        public void Command_CargoBook(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;
            ClientAirImportService.OpenCargoBook(CurrentRow.ID, null, EditPartSaved);

        }
        #endregion

        #region 集装箱跟踪
        /// <summary>
        /// 集装箱跟踪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_BoxTracking)]
        public void Command_BoxTracking(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                OIBusinessTracking oibusinessTrack = Workitem.Items.AddNew<OIBusinessTracking>();
                IWorkspace workSpace = Workitem.RootWorkItem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

                SmartPartInfo sm = new SmartPartInfo();
                sm.Description = "集装箱跟踪";
                sm.Title = "集装箱跟踪";
                workSpace.Show(oibusinessTrack, sm);
            }

        }
        #endregion

        #region 业务下载
        /// <summary>
        /// 下载 
        /// </summary>
        [CommandHandler(AIBusinessCommandConstants.Command_DownLoad)]
        public void Command_DownLoad(object sender, EventArgs e)
        {
            ClientAirImportService.AiDownLoad();
        }

        /// <summary>
        /// This is the subscription for the CustomerAdded event
        /// We're using the default scope, which is Global
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [EventSubscription(AIBusinessCommandConstants.Command_AIInsertToListAfterDownLoad)]
        public void OnInsertToListAfterDownLoad(object sender, DataEventArgs<List<AirBusinessList>> e)
        {
            List<AirBusinessList> list = e.Data as List<AirBusinessList>;
            if (list != null && list.Count > 0)
            {
                List<AirBusinessList> dataList = bsList.DataSource as List<AirBusinessList>;
                if (dataList == null || dataList.Count == 0)
                {
                    //数据源等于Null的时候，直接赋值数据源
                    bsList.DataSource = list;
                }
                else
                {
                    //数据源不等于Null的时候，加入数据源
                    foreach (AirBusinessList b in list)
                    {
                        //    if ((from AirBusinessList o in dataList where o.ID == b.ID select o).ToList().Count == 0)                     
                        bsList.Insert(0, b);
                    }
                }
                //刷新显示
                bsList.ResetBindings(false);
            }
        }

        #endregion

        #region 放货
        /// <summary>
        /// 放货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_Delivery)]
        public void Command_Delivery(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            ClientAirImportService.AiDelivery(CurrentRow.ID);
        }

        #endregion

        #region 业务转移
        /// <summary>
        /// 业务转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(AIBusinessCommandConstants.Command_BusinessTransfer)]
        public void Command_BusinessTransfer(object sender, EventArgs e)
        {
            BusinessTransfer();
        }

        /// <summary>
        /// 业务转移
        /// </summary>
        public void BusinessTransfer()
        {
            ClientAirImportService.BusinessTransfer(CurrentRow.ID);
        }
        #endregion



        private EditPartShowCriteria ShowCriteria
        {
            get
            {
                if (CurrentRow == null)
                {
                    return null;
                }
                return new EditPartShowCriteria { OperationNo = CurrentRow.No, BillNo = CurrentRow.ID };

            }
        }
        #endregion

        #region 重写
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                List<AirBusinessList> list = value as List<AirBusinessList>;

                bsList.DataSource = list;

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, Current);
                }
                gvMain.BestFitColumns();


                string message = string.Empty;

                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                    else
                    {
                       // message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                    //message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                if (list.Count.ToString().Length == 1)
                {
                    gvMain.IndicatorWidth = 30;
                }
                else
                {
                    gvMain.IndicatorWidth = list.Count.ToString().Length * 17;
                }
                //LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }
        public override object Current
        {
            get
            {
                return bsList.Current as AirBusinessList;
            }
        }
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        #endregion

        #region 私有方法
        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            AirBusinessInfo data = prams[0] as AirBusinessInfo;

            //特殊数据的处理
            string hblNo = prams[2].ToString();
            //string containerNo = prams[2].ToString();

            data.SubNo = hblNo;
            data.MBLID = data.MBLInfo.ID;
            data.MBLNo = data.MBLInfo.MBLNo;
            data.FlightNo = data.MBLInfo.FlightNo;
            data.ITNO = data.MBLInfo.ITNO;
            data.OBLRcved = Convert.ToBoolean(prams[3]);

            List<AirBusinessList> source = this.DataSource as List<AirBusinessList>;
            if (source == null || source.Count == 0)
            {
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                AirBusinessList tager = source.Find(delegate(AirBusinessList item) { return item.ID == data.ID; });
                if (tager == null)
                {
                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    Utility.CopyToValue(data, tager, typeof(AirBusinessList));
                    bsList.ResetItem(bsList.IndexOf(tager));
                }
            }
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }

        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void TruckEditPartSaved(object[] prams)
        {
            Guid[] ids = new Guid[1];
            ids[0] = CurrentRow.ID;
            List<AirBusinessList> datas = AirImportService.GetBusinessListByIds(ids);
            //CurrentRow.ContainerNo = datas[0].ContainerNo;
            bsList.ResetCurrentItem();
        }

        /// <summary>
        /// 当前行改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }
        }

        /// <summary>
        /// 单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == this.colOBLRcved)
            {
                if (CurrentRow == null || CurrentRow.State == AIOrderState.Rejected)
                {
                    return;
                }

                OIBusinessReceived bsTransfer = Workitem.Items.AddNew<OIBusinessReceived>();
                bsTransfer.BusinessID = CurrentRow.ID;

                string title = LocalData.IsEnglish ? "Set OBLRcved Date" : "设置收到正本时间";

                if (Utility.ShowDialog(bsTransfer, title) == DialogResult.OK)
                {
                    AirBusinessList currentRow = CurrentRow;
                    currentRow.OBLRcved = bsTransfer.OBLRcved;

                    bsList.ResetCurrentItem();
                }
            }
        }

        public new event KeyEventHandler KeyDown;
        /// <summary>
        /// 回车编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[AIBusinessCommandConstants.Command_EditData].Execute();
            }
            else if (this.KeyDown != null
                && e.KeyCode == Keys.F5
                && this.gvMain.FocusedColumn != null
                && this.gvMain.FocusedValue != null)
            {
                string text = gvMain.GetFocusedDisplayText();
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvMain.FocusedColumn.FieldName, text);
                this.KeyDown(keyValue, null);
            }
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
            {
                Workitem.Commands[AIBusinessCommandConstants.Command_ShowSearch].Execute();
            }
        }
        /// <summary>
        /// 双击编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                Workitem.Commands[AIBusinessCommandConstants.Command_EditData].Execute();
            }
        }

        private void gvMain_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            AirBusinessList list = gvMain.GetRow(e.RowHandle) as AirBusinessList;
            if (list == null) return;

            if (list.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (list.State == AIOrderState.NewOrder)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.NewLine);
            }
            else if (list.State == AIOrderState.Release)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
            }
            else if (list.State == AIOrderState.Rejected)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Error);
            }
        }

        #endregion

        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }
    }
}
