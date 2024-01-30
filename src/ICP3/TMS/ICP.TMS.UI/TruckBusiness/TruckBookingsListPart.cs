using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.TMS.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.SmartParts;
using ICP.Framework.CommonLibrary.Helper;

namespace ICP.TMS.UI
{
    [ToolboxItem(false)]
    public partial class TruckBookingsListPart : BaseListPart
    {
        public TruckBookingsListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                this.KeyDown = null;
                this.CurrentChanged = null;
                this.treeIDList = null;
                this.treeMain.DataSource = null;
                this.treeMain.CustomDrawNodeIndicator -= this.treeMain_CustomDrawNodeIndicator;
                this.treeMain.DoubleClick -= this.treeMain_DoubleClick;
                this.treeMain.KeyDown -= this.treeMain_KeyDown;
                this.treeMain.NodeCellStyle -= this.treeMain_NodeCellStyle;
                this.bsList.PositionChanged -= this.bsList_PositionChanged;
                this.bsList.DataSource = null;
                this.bsList.Dispose();
                if (this.Workitem != null)
                {
                    this.Workitem.Items.Remove(this);
                    this.Workitem = null;
                }

            
            
            };
        }

        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public ITruckBookingService TruckBookingService
        {
            get
            {
                return ServiceClient.GetService<ITruckBookingService>();
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

        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>();
            }
        }

        #endregion

        #region 私有字段
        /// <summary>
        /// 当前行数据
        /// </summary>
        private TruckBookingsList CurrentRow
        {
            get
            {
              return  bsList.Current as TruckBookingsList;
            }
        }
        /// <summary>
        /// 数据列表
        /// </summary>
        private List<TruckBookingsList> DataList
        {
            get
            {
                return bsList.DataSource as List<TruckBookingsList>;
            }
        }
        #endregion

        #region 初始化 
        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitMessages();
            }
        }
        private bool isInitComboBox = false;

        private void InitComboBoxItems()
        {
            if (isInitComboBox)
            {
                return;
            }
            //状态
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<TruckBusinessState>> truckState = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<TruckBusinessState>(LocalData.IsEnglish);
            cmbState.Properties.BeginUpdate();
            foreach (var item in truckState)
            {
                cmbState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbState.Properties.EndUpdate();

            //类型
            List<ICP.Framework.CommonLibrary.Helper.EnumHelper.ListItem<TruckBookingType>> truckType = ICP.Framework.CommonLibrary.Helper.EnumHelper.GetEnumValues<TruckBookingType>(LocalData.IsEnglish);
            cmbType.Properties.BeginUpdate();
            foreach (var item in truckType)
            {
                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbType.Properties.EndUpdate();
            isInitComboBox = true;
        }
        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMessages()
        {
            this.RegisterMessage("DeleteNotTruck", LocalData.IsEnglish ? "Only un-dispatched items can be removed" : "该业务数据不是未派车状态,无法删除");
            this.RegisterMessage("1108190001",LocalData.IsEnglish?"Are you sure to invalidate the selected time?": "确认要作废该数据");
            this.RegisterMessage("1108190002", LocalData.IsEnglish ? "Are you sure to resume the selected time?" : "确认要激活该数据");
        }
        #endregion

        #region 重写

        List<Guid> treeIDList = new List<Guid>();

        /// <summary>
        /// 数据源
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                treeMain.BeginUpdate();

                List<TruckBookingsList> list = value as List<TruckBookingsList>;

                if (list == null)
                {
                    list = new List<TruckBookingsList>();
                }
                treeIDList = new List<Guid>();
                foreach (TruckBookingsList item in list)
                {
                    SetID(item);
                }

                if (list.Count > 0)
                {
                    InitComboBoxItems();
                }
                bsList.DataSource = list;

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
                treeMain.ExpandAll();
                treeMain.BestFitColumns();
                treeMain.EndUpdate();

                string message = string.Empty;

                if (list.Count > 0)
                {
                    if (LocalData.IsEnglish)
                    {
                        message = string.Format("{0} record found", list.Count);
                    }
                    else
                    {
                        message = string.Format("查询到 {0} 条记录", list.Count);
                    }
                }
                else
                {
                    message = LocalData.IsEnglish ? "Nothing found!" : "没有查询到任何结果。";
                }

                if (list.Count.ToString().Length == 1)
                {
                    treeMain.IndicatorWidth = 30;
                }
                else
                {
                    treeMain.IndicatorWidth = list.Count.ToString().Length * 17;
                }
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            }
        }
        private void SetID(TruckBookingsList item)
        {
            if (treeIDList.Contains(item.BookingID))
            {
                item.ID = Guid.NewGuid();
            }
            else
            {
                item.ID = item.BookingID;
                treeIDList.Add(item.BookingID);
            }
            if (item.ID == Guid.Empty)
            {
                item.ID = Guid.NewGuid();
            }

        }
        public override event ICP.Framework.ClientComponents.UIFramework.CurrentChangedHandler CurrentChanged;
        #endregion

        #region 按钮事件
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Add)]
        public void Command_Add(object sender, EventArgs e)
        {
            string title=LocalData.IsEnglish?"Add TruckBookings":"新增拖车业务";
            PartLoader.ShowEditPart<TruckBookingsEdit>(Workitem, null, title, EditPartSaved);
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Copy)]
        public void Command_Copy(object sender, EventArgs e)
        {
            string title = LocalData.IsEnglish ? "Add TruckBookings" : "新增拖车业务";

            if (CurrentRow == null)
            {
                return;
            }

            TruckBookingsEdit editPart = Workitem.Items.AddNew<TruckBookingsEdit>();

            IWorkspace mainWorkspace = Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

            CurrentRow.EditType = TruckBookingEditType.Edit;
            CurrentRow.TruckEditMode = EditMode.Copy;
            editPart.DataSource = CurrentRow;

            SmartPartInfo smartPartInfo = new SmartPartInfo();
            smartPartInfo.Title = title;
            mainWorkspace.Show(editPart, smartPartInfo);

            editPart.Saved += delegate(object[] prams)
            {
                EditPartSaved(prams);
            };

            editPart.DeleteDataed += delegate(object[] prams)
            {
                EditPartDeletaed(prams);
            };

        }


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Edit)]
        public void Command_Edit(object sender, EventArgs e)
        {
            EditData(TruckBookingEditType.Edit,string.Empty);
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        private void EditData(TruckBookingEditType editType,string titles)
        {
            if (CurrentRow == null)
            {
                return;
            }

            string title = LocalData.IsEnglish ? "Edit TruckBookings" : "编辑拖车业务";
            if (!string.IsNullOrEmpty(titles))
            {
                title = titles;
            }
            using (new CursorHelper())
            {
                TruckBookingsEdit editPart = Workitem.Items.AddNew<TruckBookingsEdit>();

                IWorkspace mainWorkspace = Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];

                CurrentRow.EditType = editType;
                editPart.DataSource = CurrentRow;

                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = title;
                mainWorkspace.Show(editPart, smartPartInfo);

                editPart.Saved += delegate(object[] prams)
                {
                    EditPartSaved(prams);
                };

                editPart.DeleteDataed += delegate(object[] prams)
                {
                    EditPartDeletaed(prams);
                };
            }
            
                
        }

        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Cancel)]
        public void Command_Cancel(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            string message = string.Empty;
            if (CurrentRow.IsValid)
            {
                message = NativeLanguageService.GetText(this, "1108190001");
            }
            else
            {
                message = NativeLanguageService.GetText(this, "1108190002");
            }

            if (!Utility.ShowResultMessage(message))
            {
                return;
            }

            try
            {
                SingleResult result = TruckBookingService.CancelTruckBookings(CurrentRow.BookingID, CurrentRow.IsValid, LocalData.UserInfo.LoginID, CurrentRow.BookingUpdateDate, LocalData.IsEnglish);

                Guid id = result.GetValue<Guid>("ID");
                DateTime? updateDate = result.GetValue<DateTime?>("UpdateDate");
                bool isValid = !CurrentRow.IsValid;

                List<TruckBookingsList> trckList = (from d in DataList where d.BookingID == id select d).ToList();

                if (trckList != null && trckList.Count > 0)
                {
                    foreach (TruckBookingsList item in trckList)
                    {
                        item.BookingID = id;
                        item.BookingUpdateDate = updateDate;
                        item.IsValid = isValid;
                    }
                }

                bsList.ResetBindings(false);

                CurrentChanged(sender, CurrentRow);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Error: " : "错误: ") + ex.Message);
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Delete)]
        public void Command_Delete(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            if(CurrentRow.State!=TruckBusinessState.NoTruck)
            {
                Utility.ShowMessage(NativeLanguageService.GetText(this, "DeleteNotTruck"));
                return;
            }

            if (!Utility.EnquireIsDeleteCurrentData())
            {
                return;
            }

            try
            {
                if (CurrentRow.ContainerID != null && CurrentRow.ContainerID != Guid.Empty)
                {
                    Guid bookingID = CurrentRow.BookingID;
                    List<Guid> ids = new List<Guid>();
                    List<DateTime?> updateDates = new List<DateTime?>();

                    ids.Add(CurrentRow.ContainerID.Value);
                    updateDates.Add(CurrentRow.ContainerUpdateDate);

                    TruckBookingService.DeleteContainer(ids.ToArray(), CurrentRow.BookingID, updateDates.ToArray(), LocalData.UserInfo.LoginID, LocalData.IsEnglish);

                    //判断是否需要移除当前行
                    int i = (from d in DataList where d.BookingID == CurrentRow.BookingID select d).Count();
                    if (i > 1)
                    {
                        //如果这个业务下还有其他的箱信息，则移除当前行
                        bsList.RemoveCurrent();
                     
                    }
                    else
                    {
                        //如果这个业务下只有这一个箱信息,则将箱信息都清空
                        CurrentRow.ContainerID = null;
                        CurrentRow.ContainerNo = null;
                        CurrentRow.ContainerType = string.Empty;
                        CurrentRow.ContainerUpdateDate = null;
                        CurrentRow.DriverName = null;
                        CurrentRow.LastFreeDate = null;
                        CurrentRow.Remark = null;
                        CurrentRow.State = TruckBusinessState.NoTruck;
                        CurrentRow.StateDescription = EnumHelper.GetDescription<TruckBusinessState>(TruckBusinessState.NoTruck, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                        CurrentRow.TrayNo = null;
                        CurrentRow.TruckDate = null;
                        CurrentRow.TruckNo = null;
                        CurrentRow.TruckPlace = null;

                        bsList.ResetCurrentItem();

                    }
                    bsList_PositionChanged(null, null);

                    RefreshState(bookingID);
                }

              
                   
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm()
                    , (LocalData.IsEnglish ? "Delete Faily" : "删除失败.") + ex.Message);
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Print)]
        public void Command_Print(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 派车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Truck)]
        public void Command_Truck(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            string title = LocalData.IsEnglish ? "Truck Edit" : "编辑派车信息";

            EditData(TruckBookingEditType.Edit,title);
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_DownLoadBusiness)]
        public void Command_DownLoadBusiness(object sender, EventArgs e)
        {
            DownloadBusinessWorkitem downLoadWorkitem = Workitem.Items.AddNew<DownloadBusinessWorkitem>();
            downLoadWorkitem.Run();

            downLoadWorkitem.Saved += new SavedHandler(downLoadWorkitem_Saved);

        }

        void downLoadWorkitem_Saved(params object[] prams)
        {
            if (prams.Length > 0)
            {
                AddItem(prams[0]);
            }
        }

        /// <summary>
        /// 账单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(CurrentRow.BookingID, OperationType.Truck);
            if (operationCommonInfo != null)
            {
                operationCommonInfo.CurrentFormID = CurrentRow.BookingID;
                FinanceClientService.ShowBillList(operationCommonInfo, ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            }
        }


        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(TruckBookingsCommandConstants.Command_Refresh)]
        public void Command_Refresh(object sender, EventArgs e)
        {

            try
            {
                List<TruckBookingsList> blList = DataSource as List<TruckBookingsList>;
                if (blList == null || blList.Count == 0) return;

                List<Guid> ids = new List<Guid>();
                foreach (var item in blList)
                {
                    if (item.ContainerID != null)
                    {
                        ids.Add(item.BookingID);
                    }
                }

                List<TruckBookingsList> list = TruckBookingService.GetTruckBookingsListByIds(ids.ToArray(),LocalData.IsEnglish);
                this.DataSource = list;
            }
            catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex); }

        }


        #endregion

        #region Tree事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMain_DoubleClick(object sender, EventArgs e)
        {
            EditData(TruckBookingEditType.Edit,string.Empty);
        }
        /// <summary>
        /// 画行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMain_CustomDrawNodeIndicator(object sender, DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.IsNodeIndicator == false || e.ObjectArgs == null) return;

            DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = e.ObjectArgs as DevExpress.Utils.Drawing.IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = treeMain.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
        }

        private void treeMain_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Node == null) return;
            TruckBookingsList listData = treeMain.GetDataRecordByNode(e.Node) as TruckBookingsList;
            if (listData == null) return;

            if (listData.IsValid == false)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Disabled);
            }
            else if (listData.State == TruckBusinessState.Completed)
            {
                ICP.Framework.CommonLibrary.Helper.GridHelper.SetColorStyle(e.Appearance, ICP.Framework.CommonLibrary.Helper.PresenceStyle.Confirmed);
            }
        }

        public new event KeyEventHandler KeyDown;
        /// <summary>
        /// 单元格键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EditData(TruckBookingEditType.Edit,string.Empty);
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (this.KeyDown != null && treeMain.FocusedColumn != null && treeMain.FocusedNode != null)
                {
                    string text = treeMain.FocusedNode.GetDisplayText(treeMain.FocusedColumn);
                    Dictionary<string, object> keyValue = new Dictionary<string, object>();
                    keyValue.Add(treeMain.FocusedColumn.FieldName, text);
                    this.KeyDown(keyValue, null);
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                Workitem.Commands[TruckBookingsCommandConstants.Command_ShowSearch].Execute();
            }
        }
        #endregion

        #region 私有事件
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="prams"></param>
        void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }

            foreach (object parm in prams)
            {
                TruckBookingsList data = parm as TruckBookingsList;
                if (data == null)
                {
                    return;
                }

                if (DataList == null || DataList.Count == 0)
                {
                    bsList.DataSource = new List<TruckBookingsList>();

                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    TruckBookingsList tager = DataList.Find(delegate(TruckBookingsList item) { return item.ContainerID == data.ContainerID; });
                    if (tager == null)
                    {
                        if (!treeIDList.Contains(data.BookingID))
                        {
                            treeIDList.Add(data.BookingID);
                        }
                        data.ID = Guid.NewGuid();
                        bsList.Insert(0, data);
                     



                        TruckBookingsList ordData = DataList.Find(delegate(TruckBookingsList item) { return item.BookingID == data.BookingID && Utility.GuidIsNullOrEmpty(item.ContainerID); });
                        if (ordData != null && bsList.Contains(ordData))
                        {
                            bsList.Remove(ordData);
                        }

                        bsList.ResetBindings(false);
                    }
                    else
                    {
                        Guid id = tager.ID;
                        Utility.CopyToValue(data, tager, typeof(TruckBookingsList));
                        tager.ID = id;

                        SetID(data);
                    }
                }
            }

            bsList.ResetBindings(false);

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }

          
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="prams"></param>
        void EditPartDeletaed(object[] prams)
        {
            if (prams == null || prams.Length == 0)
            {
                return;
            }

            List<Guid> idList = prams[0] as List<Guid>;
            if (idList == null || idList.Count == 0)
            {
                return;
            }

            foreach (Guid id in idList)
            {
                TruckBookingsList tager = DataList.Find(delegate(TruckBookingsList item) { return item.ContainerID == id; });
                if (tager != null)
                {
                    bsList.Remove(tager);
                }

                RefreshState(tager.BookingID);                   
            }

            bsList.ResetBindings(false);

            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }

        }
        /// <summary>
        /// 刷新状态
        /// </summary>
        private void RefreshState(Guid bookingID)
        {
            List<TruckBookingsList> list = (from d in DataList where d.BookingID == bookingID select d).ToList();
            int m = (from d in DataList where d.BookingID == bookingID && (d.State == TruckBusinessState.Completed || d.State == TruckBusinessState.Return) select d).Count();

            if (list.Count == m)
            {
                foreach (TruckBookingsList item in list)
                { 
                    if(bsList.Contains(item))
                    {
                        item.State = TruckBusinessState.Completed;
                        item.StateDescription = EnumHelper.GetDescription<TruckBusinessState>(TruckBusinessState.Completed, ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish);
                    }
                }
                bsList.ResetBindings(false);
            }

        }

        /// <summary>
        /// 下载时，添加新的数据
        /// </summary>
        /// <param name="item"></param>
        public override void AddItem(object item)
        {
            List<TruckBookingsList> list=item as List<TruckBookingsList>;

            if (list != null)
            {
                if (DataList == null)
                {
                    bsList.DataSource = new List<TruckBookingsList>();
                }
                foreach (TruckBookingsList booking in list)
                {
                    SetID(booking);
                    bsList.Insert(0,booking);
                }

                bsList.ResetBindings(false);
            }

        }

        /// <summary>
        /// 当前行发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        #endregion

    
    
    }

}
