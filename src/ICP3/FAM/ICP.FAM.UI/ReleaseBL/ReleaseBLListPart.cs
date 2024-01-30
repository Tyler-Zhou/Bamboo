using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.UI.ReleaseBL.Dialogs;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using DevExpress.XtraEditors;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.FCM.Common.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.ReleaseBL
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public partial class ReleaseBLListPart : BaseListPart
    {
        #region Service
        /// <summary>
        /// 
        /// </summary>
        [ServiceDependency]
        public WorkItem Workitem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IBusinessInfoProviderFactory BusinessInfoProviderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
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
        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IClientOceanExportService ClientOceanExportService
        {
            get
            {
                return ServiceClient.GetService<IClientOceanExportService>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IOperationAgentService OperationAgentService
        {
            get { return ServiceClient.GetService<IOperationAgentService>(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }
        #endregion

        #region init
        /// <summary>
        /// 
        /// </summary>
        public ReleaseBLListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                InvokeGetData = null;
                CurrentChanged = null;
                _dataPageInfo = null;
                invoiceList = null;

                bsList.PositionChanged -= bsMainList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                gcMain.DataSource = null;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }

            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bvMain.BestFitColumns();
            InitControls();
        }
        /// <summary>
        /// 
        /// </summary>
        private void InitControls()
        {
            #region 放单状态

            List<EnumHelper.ListItem<ReleaseBLState>> releaseStates = EnumHelper.GetEnumValues<ReleaseBLState>(LocalData.IsEnglish);
            foreach (var item in releaseStates)
            {
                if (item.Value == ReleaseBLState.Created)
                    rcmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, 0));
                else
                    rcmbState.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, -1));
            }
            #endregion

            #region 放单类型

            List<EnumHelper.ListItem<ReleaseType>> releaseTypes = EnumHelper.GetEnumValues<ReleaseType>(LocalData.IsEnglish);
            foreach (var item in releaseTypes)
            {
                rcmbReleaseType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            #endregion
        }

        #endregion

        #region 常量
        List<ReleaseBLList> invoiceList = new List<ReleaseBLList>();

        VisibleMode visibleMode = VisibleMode.All;
        #endregion

        #region IListPart 成员
        /// <summary>
        /// 
        /// </summary>
        public override object Current
        {
            get { return bsList.Current; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected ReleaseBLList CurrentRow
        {
            get { return Current as ReleaseBLList; }
        }

        /// <summary>
        /// gvBLList选中行
        /// </summary>
        List<ReleaseBLList> SelectedItems
        {
            get
            {
                List<ReleaseBLList> tagers = new List<ReleaseBLList>();

                int[] Handle = bvMain.GetSelectedRows();
                for (int i = 0; i < Handle.Length; i++)
                {
                    ReleaseBLList dataRow = (ReleaseBLList)bvMain.GetRow(Handle[i]);
                    tagers.Add(dataRow);
                }
                return tagers;
            }
        }

        /// <summary>
        /// 接收ReleaseBLList
        /// </summary>
        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                BindingData(value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibleMode"></param>
        public void SetDataSourceByVisibleMode(VisibleMode visibleMode)
        {
            if (visibleMode == VisibleMode.All)
            {
                bsList.DataSource = invoiceList;
            }
            if (visibleMode == VisibleMode.HBL)
            {
                bsList.DataSource = invoiceList.FindAll(invo => invo.BLType == FCMBLType.HBL);
            }
            if (visibleMode == VisibleMode.MBL)
            {
                bsList.DataSource = invoiceList.FindAll(invo => invo.BLType == FCMBLType.MBL);
            }

            bsList.ResetBindings(false);
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }


        /// <summary>
        /// 
        /// </summary>
        DataPageInfo _dataPageInfo = new DataPageInfo();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void BindingData(object value)
        {
            ReleasePageList source = value as ReleasePageList;

            if (source != null) invoiceList = source.GetList<ReleaseBLList>();
            if (source == null || invoiceList.Count == 0)
            {
                bsList.DataSource = typeof(ReleaseBLList);
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                bvMain.SortInfo.Clear();
                labTip.Text = string.Empty;
            }
            else
            {
                SetDataSourceByVisibleMode(visibleMode);
                _dataPageInfo = source.DataPageInfo;
                if (_dataPageInfo != null)
                {
                    int pageCount = _dataPageInfo.TotalCount / _dataPageInfo.PageSize;
                    if (pageCount == 1 && _dataPageInfo.TotalCount > _dataPageInfo.PageSize)
                    {
                        pageCount = 2;
                    }
                    if (pageCount == 0 && _dataPageInfo.TotalCount > 0)
                    {
                        pageCount = 1;
                    }
                    pageControl1.TotalPage = pageCount;

                    pageControl1.CurrentPage = _dataPageInfo.CurrentPage;
                    bvMain.SortInfo.Clear();
                    ColumnSortOrder sortOrder = _dataPageInfo.SortOrderType == SortOrderType.Asc ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
                    GridColumn col = null;
                    for (int i = 0; i < bvMain.Columns.Count; i++)
                    {
                        if (bvMain.Columns[i].FieldName == _dataPageInfo.SortByName) col = bvMain.Columns[i];
                    }
                    bvMain.SortInfo.Clear();
                    if (col != null)
                        bvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                }

                labTip.Text = "您有" + source.CreatedCount.ToString() + "条新放单记录," + source.IssueCount.ToString() + "条未放单的记录.";
            }

            bvMain.BestFitColumns();

            //this.gcMain.EndUpdate();
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
        }

        public override event CurrentChangedHandler CurrentChanged;
        public override event InvokeGetDataHandler InvokeGetData;

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null)
            {

                CurrentChanged(this, Current);
            }
        }


        private void bvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            //if (e.RowHandle < 0 || e.Column.Name != colState.Name) return;

            //ReleaseBLList list = bvMain.GetRow(e.RowHandle) as ReleaseBLList;
            //if (list == null || list.State == ReleaseBLState.Unknown) return;

            //if (list.State == ReleaseBLState.Issue)
            //{
            //    e.Appearance.ForeColor = ReleaseBLColorConstant.Issue;
            //    e.Appearance.Options.UseForeColor = true;
            //}

        }

        private void bvMain_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        private void bvMain_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            if (InvokeGetData != null && _dataPageInfo.TotalCount > 0)
            {
                e.Cancel = true;
                _dataPageInfo.SortByName = e.Column.FieldName;
                if (e.ColumnSortOrder == ColumnSortOrder.Ascending ||
                    e.ColumnSortOrder == ColumnSortOrder.None)
                {
                    _dataPageInfo.SortOrderType = SortOrderType.Desc;
                }
                else
                {
                    _dataPageInfo.SortOrderType = SortOrderType.Asc;
                }
                InvokeGetData(this, _dataPageInfo);
            }
        }

        private void bvMain_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0) return;

            if (e.Clicks == 1)
            {
                if (e.Column == ColOBLRec)
                {
                    if (!CurrentRow.OBLRec)
                    {
                        Workitem.Commands[ReleaseBLCommondConstants.Command_Recevied].Execute();
                    }
                    else
                    {
                        Workitem.Commands[ReleaseBLCommondConstants.Command_CancelRecevied].Execute();
                    }
                }
                if (e.Column != colIsApplyTelex) return;
                if (!CurrentRow.IsApplyTelex)
                {
                    Workitem.Commands[ReleaseBLCommondConstants.Commond_Apply].Execute();
                }
                else
                {
                    Workitem.Commands[ReleaseBLCommondConstants.Commond_CancelApply].Execute();
                }
                return;
            }
            else if (e.Column != colIsApplyTelex)
            {
                if (e.Column.FieldName == "ExpressOrderNo" || e.Column.FieldName == "Remark")
                {
                    bvMain.OptionsBehavior.Editable = true;
                }
                else
                {
                    Workitem.Commands[ReleaseBLCommondConstants.Commond_Edit].Execute();
                    return;
                }
            }
        }

        // public override event ICP.Framework.ClientComponents.UIFramework.SavedHandler Saved;


        private bool Save(ReleaseBLList releaseBLList)
        {
            ReleaseBLList currentData = releaseBLList;

            if (currentData == null) return false;
            if (currentData.Validate
                (
                delegate(ValidateEventArgs e)
                {
                    if ((short)currentData.State >= (short)ReleaseBLState.Released)
                    {
                        if (currentData.ReleaseType == ReleaseType.Telex && currentData.TelexNo.IsNullOrEmpty())
                        {
                            e.SetErrorInfo("TelexNo", LocalData.IsEnglish ? "TelexNo Must Input." : "电放号必需输入");
                        }
                    }
                }

                ) == false)
            {
                return false;
            }

            try
            {
                ReleaseType type;
                if (currentData.ReleaseType == ReleaseType.TconvO || currentData.ReleaseType == ReleaseType.OconvT)
                {
                    type = currentData.ReleaseType == ReleaseType.TconvO ? ReleaseType.Original : ReleaseType.Telex;
                }
                else
                {
                    type = currentData.ReleaseType;
                }
                SingleResult result = FinanceService.SaveReleaseBLInfo(currentData.ID
                                                                , currentData.CustomerID
                                                                , currentData.CustomerContact
                                                                , type
                                                                , currentData.State
                                                                , currentData.TelexNo.Encrypt(currentData.ID.ToString(), EncryptType.DES_ID)
                                                                , currentData.ExpressOrderNo
                                                                , currentData.Remark
                                                                , LocalData.UserInfo.LoginID
                                                                , currentData.UpdateDate
                                                                , currentData.BLUpdateDate);

                currentData.CancelEdit();

                //if (currentData.State == ReleaseBLState.Created) currentData.State = ReleaseBLState.Issue;

                currentData.ID = result.GetValue<Guid>("ID");
                currentData.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                currentData.BLUpdateDate = result.GetValue<DateTime?>("BLUpdateDate");
                currentData.BeginEdit();
                // if (Saved != null) Saved(currentData, new object[] { result });

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Save Successfully" : "保存成功");
                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }


        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            if (InvokeGetData != null)
            {
                _dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, _dataPageInfo);
            }
        }

        private void bvMain_MouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = bvMain.CalcHitInfo(e.Location);

            if (hitInfo.InRowCell == false || hitInfo.RowHandle < 0 || hitInfo.Column != colIsAlwayTelex)
            {
                toolTip1.Hide(this);
                return;
            }

            ReleaseBLList item = bvMain.GetRow(hitInfo.RowHandle) as ReleaseBLList;
            if (item == null || item.IsAlwayTelex == false) { return; }
            string toolTxt = string.Format("客户：{0}({1})\r\n收获人：{2}({3})", item.CustomerCName, item.CustomerEName, item.ConsigneeCName, item.ConsigneeEName);

            string str = toolTip1.GetToolTip(this);
            if (toolTip1.Active && str == toolTxt) { return; }

            Point p = gcMain.PointToClient(MousePosition);
            p.X += 20;
            p.Y += 30;
            toolTip1.Show(toolTxt, this, p, 5000);
        }

        /// <summary>
        /// 编辑备注或快速单号后自动保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bvMain_HiddenEditor(object sender, EventArgs e)
        {

        }



        private void bvMain_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            ReleaseBLList releaseBLList = bvMain.GetRow(e.RowHandle) as ReleaseBLList;
            if (releaseBLList != null)
            {
                if (releaseBLList.IsDirty)
                {
                    Save(releaseBLList);
                }

                releaseBLList.IsDirty = false;
            }

            releaseBLList = null;
        }

        #endregion

        #region Commands

        #region Commond_Edit

        [CommandHandler(ReleaseBLCommondConstants.Commond_Edit)]
        public void Commond_Edit(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                try
                {
                    ReleaseBLList currentRow = CurrentRow;

                    //传递一个副本
                    ReleaseBLList listData = FAMUtility.Clone<ReleaseBLList>(CurrentRow);
                    PartLoader.ShowEditPart<ReleaseBLEditPart>(Workitem, listData, LocalData.IsEnglish ? "Edit ReaeaseBL" : "编辑放单",
                        delegate(object[] prams)
                        {
                            if (prams != null && prams.Length > 0)
                            {
                                ReleaseBLList data = prams[0] as ReleaseBLList;
                                FAMUtility.CopyToValue(data, currentRow, typeof(ReleaseBLList));
                                bsList.ResetCurrentItem();
                                Refresh();
                            }
                        });
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "自动签收该放单记录 Failed" : "自动签收该放单记录") + ex.Message);
                }
            }
        }

        #endregion

        #region 接收

        [CommandHandler(ReleaseBLCommondConstants.Commond_Received)]
        public void Commond_Received(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;
                throw new Exception("The function has been removed.");
            }
        }

        #endregion

        #region 申请

        [CommandHandler(ReleaseBLCommondConstants.Commond_Apply)]
        public void Commond_Apply(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                try
                {
                    DateTime? dt = null;
                    dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                    SingleResult result = FinanceService.ApplyReleaseBL(CurrentRow.ID, dt, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    ReleaseBLList currentRow = CurrentRow;
                    currentRow.ApplyDate = dt;
                    currentRow.IsApplyTelex = true;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Mark as 'The release order is received from the customer'." : "标识成功：收到客户的放单请求。");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Failed to mark as 'The release order is received from the customer'" : "标识失败：收到客户的放单请求。") + ex.Message);
                }
            }
        }

        [CommandHandler(ReleaseBLCommondConstants.Commond_CancelApply)]
        public void Commond_CancelApply(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                try
                {
                    if (XtraMessageBox.Show(LocalData.IsEnglish ? "Do you want to un-mark as 'The release order is received from the customer'?" : "确定取消标识：收到客户的放单请求?"
                        , LocalData.IsEnglish ? "Tip" : "提示"
                        , MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                    DateTime? dt = null;

                    SingleResult result = FinanceService.ApplyReleaseBL(CurrentRow.ID, dt, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

                    ReleaseBLList currentRow = CurrentRow;
                    currentRow.ApplyDate = dt;
                    currentRow.IsApplyTelex = false;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Un-mark as 'The release order is received from the customer'" : "取消标识：收到客户的放单请求。");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Failed to un-mark as 'The release order is received from the customer'" : "取消标识失败：收到客户的放单请求。") + ex.Message);
                }
            }
        }
        #endregion

        #region 放单

        [CommandHandler(ReleaseBLCommondConstants.Commond_ReleaseBL)]
        public void Commond_ReleaseBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;
                string title = LocalData.IsEnglish ? "Please confirma" : "请确认";
                string message = LocalData.IsEnglish ? "Are you sure to Release the bill of loading?" : "确定放单吗？";
                string reMessage = LocalData.IsEnglish ? "HBL  Not Release,Are you sure to MBL Release the bill of loading?" : "HBL还没有放单，确认MBL放单吗?";

                //MBL放单 如果有HBL没有放单 需要确认
                if (CurrentRow.BLType == FCMBLType.MBL
                       && CurrentRow.IsNotReForHbl)
                {
                    DialogResult dre = XtraMessageBox.Show(reMessage, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dre != DialogResult.Yes)
                    {
                        return;
                    }
                }
                else
                {
                    DialogResult dr = XtraMessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }
                }

                try
                {
                    ReleaseBLState state = ReleaseBLState.Released;

                    string no = string.Empty;
                    //提单号+当前时间（年月日时分：201107081430）
                    no = CurrentRow.BlNo + FAMUtility.GetDateTimeString(DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified));
                    no = no.Encrypt(CurrentRow.ID.ToString(), EncryptType.DES_ID);

                    SingleResult result = FinanceService.ChangeReleaseBLState(CurrentRow.ID
                                                                        , state
                                                                        , true
                                                                        , no.Substring(no.Length - 20)
                                                                        , LocalData.UserInfo.LoginID
                                                                        , CurrentRow.UpdateDate
                                                                        , CurrentRow.BLUpdateDate
                                                                        , false
                                                                        , CurrentRow.AgentID
                                                                        , CurrentRow.BlNo
                                                                        , LocalData.UserInfo.UserName
                                                                        , CurrentRow.FilerEmail
                                                                        , CurrentRow.POLFilerEmail
                                                                        , CurrentRow.IsCRelease
                                                                        , CurrentRow.IsOverSeaMBL,
                                                                        CurrentRow.OperationID
                                                                        );

                    ReleaseBLList currentRow = CurrentRow;

                    if (currentRow.ReleaseType == ReleaseType.Telex)
                        currentRow.TelexNo = no.Substring(no.Length - 20);

                    currentRow.ReleaseBy = LocalData.UserInfo.LoginName;
                    currentRow.ReleaseDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                    currentRow.State = state;
                    currentRow.RBLD = !currentRow.RBLD;
                    currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                    currentRow.BLUpdateDate = result.GetValue<DateTime?>("BLUpdateDate");
                    bsList.ResetCurrentItem();

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed Release State Successfully" : "更改放单状态成功");
                    if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("10060") && ex.Message.Contains("一段时间后没有正确答复"))
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Release State Failed" : "更改放单状态失败,连接国外服务超时，请稍候再试！"));
                    }
                    else
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Release State Failed" : "更改放单状态失败") + ex.Message);
                }
            }
        }


        [CommandHandler(ReleaseBLCommondConstants.Commond_CancelReleaseBL)]
        public void Commond_CancelReleaseBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;
                string title = LocalData.IsEnglish ? "Please confirma" : "请确认";
                string message = LocalData.IsEnglish ? "A notice mail of Cancel Release is sent to the agent and CC to you. But you must make sure the agent whether receive the CANCELL notice!" : "系统已发了取消放单的通知邮件给目的港，同时抄送给了您。您务必要检查邮件是否收到并保证取消通知到了港后代理!";
                DialogResult dr = XtraMessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        ReleaseBLState state = ReleaseBLState.Created;

                        SingleResult result = FinanceService.ChangeReleaseBLState(CurrentRow.ID
                                                                            , state
                                                                            , false
                                                                            , ""
                                                                            , LocalData.UserInfo.LoginID
                                                                            , CurrentRow.UpdateDate
                                                                            , CurrentRow.BLUpdateDate
                                                                            , true
                                                                            , CurrentRow.AgentID
                                                                            , CurrentRow.BlNo
                                                                            , LocalData.UserInfo.UserName
                                                                            , CurrentRow.FilerEmail
                                                                            , CurrentRow.POLFilerEmail
                                                                            , CurrentRow.IsCRelease
                                                                            , CurrentRow.IsOverSeaMBL
                                                                            , CurrentRow.OperationID);

                        ReleaseBLList currentRow = CurrentRow;
                        currentRow.ExpressOrderNo = string.Empty;
                        currentRow.TelexNo = string.Empty;
                        currentRow.ReleaseBy = string.Empty;
                        currentRow.ReleaseDate = null;
                        currentRow.State = state;
                        currentRow.RBLD = !currentRow.RBLD;
                        currentRow.IsCRelease = true;
                        currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
                        currentRow.BLUpdateDate = result.GetValue<DateTime?>("BLUpdateDate");
                        bsList.ResetCurrentItem();

                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed Release State Successfully" : "更改放单状态成功");
                        if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Release State Failed" : "更改放单状态失败") + ex.Message);
                    }
                }
            }
        }

        #endregion

        [CommandHandler(ReleaseBLCommondConstants.Command_ViewBusinessInfo)]
        public void Command_ViewBusinessInfo(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                IBusinessInfoProvider provider = BusinessInfoProviderFactory.GetBusinessInfoProvider(CurrentRow.OperationType);
                if (CurrentRow.OperationType == OperationType.Other)
                {
                    FAMUtility.ShowMessage(LocalData.IsEnglish ? @"No found,Please contact the system administrator" : @"其它业务不能查看业务信息!");
                    return;
                }
                provider.ShowBusinessInfo(CurrentRow.OperationType, CurrentRow.OperationID, ClientConstants.MainWorkspace);
            }
        }

        [CommandHandler(ReleaseBLCommondConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null) return;

                OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(CurrentRow.OperationID, CurrentRow.OperationType);
                FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
            }
        }




        [CommandHandler(ReleaseBLCommondConstants.Command_VisibleALL)]
        public void Command_VisibleAll(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (visibleMode == VisibleMode.All) return;

                visibleMode = VisibleMode.All;

                SetDataSourceByVisibleMode(visibleMode);
            }
        }

        [CommandHandler(ReleaseBLCommondConstants.Command_VisibleHBL)]
        public void Command_VisibleHBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (visibleMode == VisibleMode.HBL) return;

                visibleMode = VisibleMode.HBL;

                SetDataSourceByVisibleMode(visibleMode);
            }
        }


        [CommandHandler(ReleaseBLCommondConstants.Command_VisibleMBL)]
        public void Command_VisibleMBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (visibleMode == VisibleMode.MBL) return;

                visibleMode = VisibleMode.MBL;

                SetDataSourceByVisibleMode(visibleMode);
            }
        }

        [CommandHandler(ReleaseBLCommondConstants.Command_PrintBL)]
        public void Command_PrintBL(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow.BLID != Guid.Empty)
                {
                    List<OceanBLList> list = OceanExportService.GetOceanBLListByIds(new Guid[] { (Guid)CurrentRow.BLID });
                    if (list != null && list.Count > 0)
                    {
                        ClientOceanExportService.PrintBillOfLoading(list[0]);
                    }
                }
            }
        }

        #region 改变放单类型

        [CommandHandler(ReleaseBLCommondConstants.Commond_Change2Original)]
        public void Commond_Change2Original(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                //ReleaseType type;

                //if (CurrentRow.ReleaseType == ReleaseType.Original || CurrentRow.ReleaseType == ReleaseType.Telex)
                //{
                //    type = CurrentRow.ReleaseType == ReleaseType.Original ? ReleaseType.Telex : ReleaseType.Original;
                //}
                //else
                //{
                //    type = CurrentRow.ReleaseType == ReleaseType.OconvT ? ReleaseType.Original : ReleaseType.Telex;
                //}


                ChangedReleaseType(ReleaseType.Original);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed Release Type Successfully" : "更改放单类型成功");
                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Release Type Failed" : "更改放单类型失败")
                    + ex.Message);
            }
        }

        [CommandHandler(ReleaseBLCommondConstants.Commond_Change2Telex)]
        public void Commond_Change2Telex(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                //ReleaseType type;

                //if (CurrentRow.ReleaseType == ReleaseType.Original || CurrentRow.ReleaseType == ReleaseType.Telex)
                //{
                //    type = CurrentRow.ReleaseType == ReleaseType.Original ? ReleaseType.Telex : ReleaseType.Original;
                //}
                //else
                //{
                //    type = CurrentRow.ReleaseType == ReleaseType.OconvT ? ReleaseType.Original : ReleaseType.Telex;
                //}


                ChangedReleaseType(ReleaseType.Telex);



                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed Release Type Successfully" : "更改放单类型成功");
                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed Release Type Failed" : "更改放单类型失败")
                    + ex.Message);
            }
        }

        void ChangedReleaseType(ReleaseType type)
        {
            SingleResult result = FinanceService.ChangeReleaseTypeToTel(CurrentRow.ID, type, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate,
                CurrentRow.BLUpdateDate, CurrentRow.State, CurrentRow.FilerEmail, CurrentRow.AgentID, CurrentRow.BlNo, LocalData.UserInfo.UserName, CurrentRow.OperationID);

            ReleaseBLList currentRow = CurrentRow;

            if (type == ReleaseType.Original || type == ReleaseType.Telex)
            {

                currentRow.ReleaseType = type;
            }
            currentRow.TelexNo = result.GetValue<string>("TelNo");
            currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
            currentRow.BLUpdateDate = result.GetValue<DateTime?>("BLUpdateDate");

            bsList.ResetCurrentItem();
        }

        #endregion

        #region 催款

        [CommandHandler(ReleaseBLCommondConstants.Commond_PressMoney)]
        public void Commond_PressMoney(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<ReleaseAndArList> waitsends = FinanceService.GetReleaseAndArList(CurrentRow.OperationID);
                    if (waitsends != null && waitsends.Count > 0)
                    {
                        List<ExCustomer> ExCustomers = CustomerService.CheckExCustomer(waitsends[0].CompanyID, CurrentRow.CustomerID, LocalData.IsEnglish);
                        if (ExCustomers != null && ExCustomers.Count > 0)
                        {
                            if (!FAMUtility.ShowResultMessage("此票业务是例外客户，是否发送催放货邮件？"))
                            {
                                return;
                            }
                        }
                        List<ExRelease> ExReleases = FinanceService.CheckExRelease(CurrentRow.ID);
                        if (ExReleases != null && ExReleases.Count > 1)
                        {
                            if (!FAMUtility.ShowResultMessage("此票业务已设置为不催放货，是否发送催放货邮件？"))
                            {
                                return;
                            }
                        }

                        string message = FinanceService.SendAREmail(waitsends[0], waitsends[0].Days < 7 ? true : false, false);
                        if (string.IsNullOrEmpty(message))
                        {
                            LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Send reminder Email Successfully" : "成功发送催款邮件！");
                        }
                        else
                        {
                            LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                        }
                        CurrentChanged(this, CurrentRow);
                    }
                    else
                    {
                        FAMUtility.ShowMessage(LocalData.IsEnglish ? @"The business does not require payment！" : @"该业务不需要催款！");
                    }

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }

        #endregion

        #region 修改催款联系人

        [CommandHandler(ReleaseBLCommondConstants.Commond_SetArEmail)]
        public void Commond_SetArEmail(object sender, EventArgs e)
        {
            AddArEmail showArEmail = new AddArEmail();

            showArEmail.OperationID = CurrentRow.OperationID;
            showArEmail.CustomerID = CurrentRow.CustomerID;

            string title = LocalData.IsEnglish ? "Set Ar Eamil" : "设置催款联系人邮箱";
            PartLoader.ShowDialog(showArEmail, title);

            Commond_Refresh(null, null);
            CurrentChanged(this, CurrentRow);
        }

        #endregion


        #region 例外客户

        [CommandHandler(ReleaseBLCommondConstants.Commond_ExceptionCustomer)]
        public void Commond_ExceptionCustomer(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    AddExceptionCustomer voucherPart = Workitem.Items.AddNew<AddExceptionCustomer>();
                    string title = LocalData.IsEnglish ? "Set Exception Customer" : "设置例外客户";

                    PartLoader.ShowDialog(voucherPart, title);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }

        #endregion


        #region 设置例外放货

        [CommandHandler(ReleaseBLCommondConstants.Commond_ExRelease)]
        public void Commond_ExRelease(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    if (SelectedItems.Count < 1)
                    {
                        FAMUtility.ShowMessage(LocalData.IsEnglish ? @"Please select the release list you need to set！" : @"请选择需要设置的放单！");
                        return;
                    }

                    foreach (ReleaseBLList selete in SelectedItems)
                    {
                        FinanceService.SaveExRelease(selete.ID, LocalData.UserInfo.LoginID);
                    }

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Successfully!" : "设置成功！");
                    CurrentChanged(this, CurrentRow);
                    Commond_Refresh(null, null);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }

        #endregion

        #region 刷新

        [CommandHandler(ReleaseBLCommondConstants.Commond_Refresh)]
        public void Commond_Refresh(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    List<ReleaseBLList> blList = DataSource as List<ReleaseBLList>;
                    if (blList == null || blList.Count == 0) return;

                    List<Guid> ids = new List<Guid>();
                    foreach (var item in blList) ids.Add(item.ID);

                    List<ReleaseBLList> releaseList = FinanceService.GetReleaseBLListByIds(ids.ToArray());


                    bsList.DataSource = releaseList;
                    bsList.ResetBindings(false);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),

                        ex.Message);
                }
            }
        }

        #endregion


        #region 接收到正本

        [CommandHandler(ReleaseBLCommondConstants.Command_Recevied)]
        public void Command_Recevied(object sender, EventArgs e)
        {

            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    if (CurrentRow != null)
                    {
                        if (!CurrentRow.RBLD)
                        {
                            FAMUtility.ShowMessage(LocalData.IsEnglish ? @"Not RBLD" : @"此业务还未放单!");
                            return;
                        }
                        if (XtraMessageBox.Show(LocalData.IsEnglish ? "Determine the received OB/L?" : "确定接收到正本放单?"
                        , LocalData.IsEnglish ? "Tip" : "提示"
                        , MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }

                        FinanceService.ChangeRecState(CurrentRow.ID, 1, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                        bsList.ResetCurrentItem();
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Successful implementation of the OB/L receiver" : "执行接收正本成功");
                        if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                        Commond_Refresh(null, null);
                    }

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),

                        ex.Message);
                }
            }
        }

        #endregion

        #region 取消接收到正本

        [CommandHandler(ReleaseBLCommondConstants.Command_CancelRecevied)]
        public void Command_CancelRecevied(object sender, EventArgs e)
        {

            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {
                    if (CurrentRow != null)
                    {
                        if (!CurrentRow.OBLRec)
                        {
                            FAMUtility.ShowMessage(LocalData.IsEnglish ? @"Not OBLRec，will not be canceled" : @"此业务未接受到正本不需要取消!");
                            return;
                        }

                        if (XtraMessageBox.Show(LocalData.IsEnglish ? "Determine the cancellation of the OB/L?" : "确定取消接收到正本放单?"
                        , LocalData.IsEnglish ? "Tip" : "提示"
                        , MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }

                        FinanceService.ChangeRecState(CurrentRow.ID, 0, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                        bsList.ResetCurrentItem();
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel OBLRec State Successfully" : "取消收到正本成功！");
                        if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
                        Commond_Refresh(null, null);
                    }

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),

                        ex.Message);
                }
            }
        }

        #endregion

        #region List导出成Excel
        [CommandHandler(ReleaseBLCommondConstants.Command_ExportToExcel)]
        public void Command_ExportToExcel(object sender, EventArgs e)
        {
            int theradID = 0;

            using (new CursorHelper(Cursors.WaitCursor))
            {
                try
                {

                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.FileName = "ReleaseList" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                    saveFile.Filter = "xls files(*.xls)|*.xls";
                    saveFile.RestoreDirectory = true;
                    saveFile.FilterIndex = 2;
                    if (saveFile.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    string fileName = saveFile.FileName.ToString();

                    theradID = LoadingServce.ShowLoadingForm("Exporting......");

                    bvMain.ExportToXls(fileName);

                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
                finally
                {
                    LoadingServce.CloseLoadingForm(theradID);

                }
            }
        }

        #endregion
        #endregion

    }
}

public enum VisibleMode
{

    All, HBL, MBL
}
