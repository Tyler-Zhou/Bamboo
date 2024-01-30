using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.UI.ReleaseRC.Dialogs;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.WF.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace ICP.FAM.UI.ReleaseRC
{
    [ToolboxItem(false)]
    public partial class ReleaseRCListPart : BaseListPart
    {
        #region Service

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IBusinessInfoProviderFactory BusinessInfoProviderFactory
        {
            get
            {
                return ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            }
        }

        public IFCMCommonService FCMCommonService
        {
            get
            {
                return ServiceClient.GetService<IFCMCommonService>();
            }
        }

        public IFinanceClientService FinanceClientService
        {
            get
            {
                return ServiceClient.GetClientService<IFinanceClientService>();
            }
        }


        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IWorkflowClientService WorkflowClientService
        {
            get
            {
                return ServiceClient.GetClientService<IWorkflowClientService>();
            }
        }

        #endregion

        #region init

        public ReleaseRCListPart()
        {
            InitializeComponent();
            Disposed += delegate
            {
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsMainList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                CurrentChanged = null;
                _dataPageInfo = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bvMain.BestFitColumns();
            InitControls();
        }

        private void InitControls()
        {
            #region 放单状态

            List<EnumHelper.ListItem<ReleaseRCState>> releaseStates = EnumHelper.GetEnumValues<ReleaseRCState>(LocalData.IsEnglish);
            foreach (var item in releaseStates)
            {
                if (item.Value == ReleaseRCState.RBL)
                    rcmbStatea.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value, 0));
                else
                    rcmbStatea.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            #endregion

            #region 放单类型

            List<EnumHelper.ListItem<ReleaseType>> releaseTypes = EnumHelper.GetEnumValues<ReleaseType>(LocalData.IsEnglish);
            foreach (var item in releaseTypes)
            {
                ReleaseType1.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            #endregion

            #region 提单类型

            List<EnumHelper.ListItem<FormType>> blType = EnumHelper.GetEnumValues<FormType>(LocalData.IsEnglish);
            foreach (var item in blType)
            {
                ByType.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }

            #endregion
        }

        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected ReleaseRCList CurrentRow
        {
            get { return Current as ReleaseRCList; }
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

        DataPageInfo _dataPageInfo = new DataPageInfo();
        private void BindingData(object value)
        {
            ReleasePageList source = value as ReleasePageList;
            List<ReleaseRCList> list = new List<ReleaseRCList>();
            if (source != null) list = source.GetList<ReleaseRCList>();
            if (source == null || list == null || list.Count == 0)
            {

                bsList.DataSource = list;
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                bvMain.SortInfo.Clear();
                //labTip.Text = string.Empty;
            }
            else
            {
                bsList.DataSource = list;
                bsList.ResetBindings(false);
                _dataPageInfo = source.DataPageInfo;
                if (_dataPageInfo != null)
                {
                    int pageSize = _dataPageInfo.PageSize;
                    int totalCount = _dataPageInfo.TotalCount;
                    int pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
                    //int pageCount = totalCount / pageSize;
                    //if (pageCount == 1 && totalCount > pageSize)
                    //{
                    //    pageCount = 2;
                    //}
                    //if (pageCount == 0 && totalCount > 0)
                    //{
                    //    pageCount = 1;
                    //}
                    pageControl1.TotalPage = pageCount;

                    pageControl1.CurrentPage = _dataPageInfo.CurrentPage;
                    bvMain.SortInfo.Clear();
                    ColumnSortOrder sortOrder = _dataPageInfo.SortOrderType == SortOrderType.Asc ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
                    GridColumn col = bvMain.Columns.ColumnByFieldName(_dataPageInfo.SortByName);
                    if (col != null)
                        bvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                }

            }
            if (CurrentChanged != null)
            {
                CurrentChanged(this, Current);
            }

            bvMain.BestFitColumns();


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

        private List<ReleaseRCList> SelectRows
        {
            get
            {
                int[] rowIndexs = bvMain.GetSelectedRows();

                if (rowIndexs.Length == 0) return null;

                List<ReleaseRCList> tagers = new List<ReleaseRCList>();
                foreach (var item in rowIndexs)
                {
                    ReleaseRCList dr = bvMain.GetRow(item) as ReleaseRCList;
                    if (dr != null) tagers.Add(dr);
                }

                return tagers;
            }
        }

        #endregion

        #region GridView Event

        private void bsMainList_PositionChanged(object sender, EventArgs e)
        {
            if (CurrentChanged != null) CurrentChanged(this, Current);
        }


        private void bvMain_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0 || e.Column.Name != colState.Name) return;

            ReleaseRCList list = bvMain.GetRow(e.RowHandle) as ReleaseRCList;
            if (list == null || list.State == ReleaseRCState.Unknown) return;

            if (list.State == ReleaseRCState.RcvRBL)
            {
                e.Appearance.ForeColor = ReleaseRCColorConstant.Issue;
                e.Appearance.Options.UseForeColor = true;
            }

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
            if (InvokeGetData != null)
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

            //if (e.Clicks == 1)
            //{
            //    if (e.Column != colIsApplyTelex) return;
            //    Workitem.Commands[ReleaseRCCommondConstants.Commond_Apply].Execute();
            //}
            //else if (e.Column != colIsApplyTelex) 
            //{
            //Workitem.Commands[ReleaseRCCommondConstants.Commond_Edit].Execute();
            //}
        }

        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            if (InvokeGetData != null)
            {
                _dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, _dataPageInfo);
            }
        }

        #endregion

        #region Commands

        #region Commond_Edit

        [CommandHandler(ReleaseRCCommondConstants.Commond_Edit)]
        public void Commond_Edit(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            try
            {
                ReleaseRCList currentRow = CurrentRow;

                //传递一个副本
                ReleaseRCList listData = FAMUtility.Clone<ReleaseRCList>(CurrentRow);
                PartLoader.ShowEditPart<ReleaseRCEditPart>(Workitem, listData, LocalData.IsEnglish ? "Edit ReaeaseBL" : "编辑放货",
                    delegate(object[] prams)
                    {
                        if (prams != null && prams.Length > 0)
                        {
                            ReleaseRCList data = prams[0] as ReleaseRCList;
                            FAMUtility.CopyToValue(data, currentRow, typeof(ReleaseRCList));
                            bsList.ResetCurrentItem();
                            Refresh();
                        }
                    });
                if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "自动签收该放货记录 Failed" : "自动签收该放货记录") + ex.Message);
            }
        }

        #endregion

        #region RCV

        //[CommandHandler(ReleaseRCCommondConstants.Commond_ChangeType)]
        //public void Commond_ChangeType(object sender, EventArgs e)
        //{
        //    if (CurrentRow == null) return;

        //    try
        //    {
        //        ReleaseType type = CurrentRow.ReleaseType == ReleaseType.Original ? ReleaseType.Telex : ReleaseType.Original;
        //        SingleResult result = finService.ChangeReleaseRCState(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);

        //        ReleaseRCList currentRow = CurrentRow;
        //        currentRow.ReleaseType = type;
        //        currentRow.ID = result.GetValue<Guid>("ID");
        //        currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
        //        bsList.ResetCurrentItem();

        //        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed ReleaseRC Type Successfully" : "更改放货类型成功");
        //        if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        //    }
        //    catch (Exception ex)
        //    {
        //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Changed ReleaseRC Type Failed" : "更改放货类型失败") + ex.Message);
        //    }
        //}

        #endregion

        #region 是否已接收放单通知

        [CommandHandler(ReleaseRCCommondConstants.Commond_Received)]
        public void Commond_Received(object sender, EventArgs e)
        {
            ReleaseRCList releaseRCList = SelectRows.Find(delegate(ReleaseRCList v) { return v.State != ReleaseRCState.RBL; });
            if (CurrentRow == null || releaseRCList != null)
            {
                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Please choose the state for RBL item！" : "请选择状态为已放单的项！");
                return;
            }
            try
            {
                //选中单据，更改为已放单
                List<Guid> ids = new List<Guid>();
                List<DateTime?> dts = new List<DateTime?>();
                foreach (ReleaseRCList item in SelectRows)
                {
                    ids.Add(item.ID);
                    dts.Add(item.UpdateDate);
                }
                ManyResult result = FinanceService.ChangeReleaseRCState(ids.ToArray(), LocalData.UserInfo.LoginID, dts.ToArray());

                foreach (ReleaseRCList srbs in SelectRows)
                {
                    foreach (SingleResult item in result.Items)
                    {
                        Guid id = item.GetValue<Guid>("ID");
                        DateTime? updateDate = item.GetValue<DateTime?>("UpdateDate");
                        if (srbs.ID == id)
                        {
                            srbs.State = ReleaseRCState.RcvRBL;
                            srbs.UpdateDate = updateDate;
                            bsList.ResetCurrentItem();
                            break;
                        }
                    }
                }

                if (CurrentChanged != null)
                    CurrentChanged(this, Current);

                bvMain.RefreshData();


                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Changed ReleaseRC State Successfully" : "更改放货状态成功");

            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), (LocalData.IsEnglish ? "Changed ReleaseRC State Failed" : "更改放货状态失败") + ex.Message);
            }
        }



        #endregion

        #region 放货/取消放货

        //[CommandHandler(ReleaseRCCommondConstants.Commond_Received)]
        //public void Commond_Received(object sender, EventArgs e)
        //{
        //    if (CurrentRow == null || CurrentRow.State != ReleaseRCState.RC || (short)ReleaseRCState.RC - 5 != 0) return;
        //    bool value;
        //    if (CurrentRow.State == ReleaseRCState.RC) { value = true; } else { value = false; }
        //    try
        //    {
        //        ReleaseRCState state = CurrentRow.State != ReleaseRCState.RC ? ReleaseRCState.RC : (ReleaseRCState)((short)ReleaseRCState.RC - 5);

        //        string no = string.Empty;
        //        if (state == ReleaseRCState.RBL && CurrentRow.ReleaseType == ReleaseType.Telex)
        //        {
        //            //提单号+当前时间（年月日时分：201107081430）
        //            no = CurrentRow.BlNo + Utility.GetDateTimeString(DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified));
        //        }

        //        SingleResult result = finService.ChangeReleaseRC(CurrentRow.ID
        //                                                            , value
        //                                                            , LocalData.UserInfo.LoginID
        //                                                            , CurrentRow.UpdateDate
        //                                                            , LocalData.IsEnglish);

        //        ReleaseRCList currentRow = CurrentRow;
        //        if (state == ReleaseRCState.RC)
        //        {
        //            if (currentRow.ReleaseType == ReleaseType.Telex)
        //                currentRow.TelexNo = no;

        //            currentRow.ReleaseBy = LocalData.UserInfo.LoginName;
        //            currentRow.ReleaseDate = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified);

        //        }
        //        else
        //        {
        //            currentRow.ExpressOrderNo = string.Empty;
        //            currentRow.TelexNo = string.Empty;
        //            currentRow.ReleaseBy = string.Empty;
        //            currentRow.ReleaseDate = null;
        //        }

        //        currentRow.State = state;
        //        currentRow.ID = result.GetValue<Guid>("ID");
        //        currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
        //        bsList.ResetCurrentItem();

        //        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed ReleaseRC State Successfully" : "更改放货状态成功");
        //        if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        //    }
        //    catch (Exception ex)
        //    {
        //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Changed ReleaseRC State Failed" : "更改放货状态失败") + ex.Message);
        //    }
        //}

        #endregion

        #region 备注信息

        [CommandHandler(ReleaseRCCommondConstants.Commond_Apply)]
        public void Commond_Apply(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            RemarkPart expressPart = Workitem.Items.AddNew<RemarkPart>();
            expressPart.DataSource = CurrentRow;
            string title = LocalData.IsEnglish ? "Update RcRemark" : "更改备注信息";
            expressPart.Saved += delegate(object[] prams)
            {
                if (prams != null && prams.Length > 0)
                {
                    ReleaseRCList data = prams[0] as ReleaseRCList;
                    if (data != null)
                    {
                        ReleaseRCList currentRow = CurrentRow;
                        currentRow.RcRemark = data.RcRemark;
                        currentRow.UpdateDate = data.UpdateDate;
                        bsList.ResetCurrentItem();
                    }
                }
            };

            PartLoader.ShowDialog(expressPart, title);

        }

        #endregion

        #region Transit

        [CommandHandler(ReleaseRCCommondConstants.Commond_ReleaseBL)]
        public void Commond_ReleaseOriginal(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            TransitionPart Part = Workitem.Items.AddNew<TransitionPart>();
            Part.DataSource = CurrentRow;
            string title = LocalData.IsEnglish ? "Transit RcCompany" : "转移放货公司";
            Part.Saved += delegate(object[] prams)
            {
                if (prams != null && prams.Length > 0)
                {
                    ReleaseRCList data = prams[0] as ReleaseRCList;
                    if (data != null)
                    {
                        ReleaseRCList currentRow = CurrentRow;
                        currentRow.UpdateDate = data.UpdateDate;
                        currentRow.ConsigneeName = data.ConsigneeName;
                        currentRow.BlNo = data.BlNo;
                        currentRow.RcCompanyName = data.RcCompanyName;
                        bsList.ResetCurrentItem();
                    }
                }
            };

            PartLoader.ShowDialog(Part, title);

        }

        #endregion

        #region 查看业务信息
        [CommandHandler(ReleaseRCCommondConstants.Command_ViewBusinessInfo)]
        public void Command_ViewBusinessInfo(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            IBusinessInfoProvider provider = BusinessInfoProviderFactory.GetBusinessInfoProvider(OperationType.OceanExport);
            provider.ShowBusinessInfo(OperationType.OceanExport, CurrentRow.OperationID, ClientConstants.MainWorkspace);
        }
        #endregion

        #region 账单
        [CommandHandler(ReleaseRCCommondConstants.Command_Bill)]
        public void Command_Bill(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            OperationCommonInfo operationCommonInfo = FCMCommonService.GetOperationCommonInfo(CurrentRow.OperationID, OperationType.OceanImport);
            FinanceClientService.ShowBillList(operationCommonInfo, ClientConstants.MainWorkspace);
        }
        #endregion


        #region 异常放货申请流程
        /// <summary>
        /// 异常放货流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(ReleaseRCCommondConstants.Command_ExceptionReleaseRC)]
        public void Command_ExceptionReleaseRC(object sender, EventArgs e)
        {
            if (CurrentRow == null) return;

            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow.IsNew) return;

                try
                {
                    BusinessList business = FinanceService.GetBusinessListByIDs(new Guid[] { CurrentRow.OperationID }).GetList<BusinessList>()[0];

                    WorkflowClientService.StartExceptionRealeaseRCWorkFlow(
                        LocalData.UserInfo.LoginID,
                        business.CompanyID,
                        LocalData.IsEnglish ? "No" : "单号:" + business.OperationNO,
                        LocalData.IsEnglish ? "Exception Realease RC" : "异常放货申请",
                        CurrentRow.OperationNo,
                        business.CompanyID,
                        CurrentRow.OperationID,
                        CurrentRow.OperationNo,
                        business.SalesName != null ? business.SalesName : string.Empty,
                        EnumHelper.GetDescription<OperationType>(business.OperationType, LocalData.IsEnglish),
                        business.ARDescription,
                        business.APDescription,
                        business.ProfitDescription,
                        LocalData.UserInfo.UserName,
                        business.CustomerID,
                        business.CustomerName,
                        string.Empty);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);

                }
            }
        }
        #endregion

        //#region 收到放货通知

        //[CommandHandler(ReleaseRCCommondConstants.Commond_ChangeType)]
        //public void Commond_ChangeType(object sender, EventArgs e)
        //{
        //    if (CurrentRow == null) return;

        //    try
        //    {
        //        ReleaseType type = CurrentRow.ReleaseType == ReleaseType.Original ? ReleaseType.Telex : ReleaseType.Original;
        //        SingleResult result = finService.ChangeReleaseBLType(CurrentRow.ID, type, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate, CurrentRow.BLUpdateDate);

        //        ReleaseRCList currentRow = CurrentRow;
        //        currentRow.ReleaseType = type;
        //        currentRow.UpdateDate = result.GetValue<DateTime?>("UpdateDate");
        //        currentRow.BLUpdateDate = result.GetValue<DateTime?>("BLUpdateDate");
        //        bsList.ResetCurrentItem();

        //        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Changed ReleaseRC Type Successfully" : "更改放货类型成功");
        //        if (CurrentChanged != null) CurrentChanged(this, CurrentRow);
        //    }
        //    catch (Exception ex)
        //    {
        //        LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), (LocalData.IsEnglish ? "Changed ReleaseRC Type Failed" : "更改放货类型失败") + ex.Message);
        //    }
        //}

        //#endregion

        #region 刷新

        [CommandHandler(ReleaseRCCommondConstants.Commond_Refresh)]
        public void Commond_Refresh(object sender, EventArgs e)
        {
            //try
            //{
            //    List<ReleaseRCList> blList = DataSource as List<ReleaseRCList>;
            //    if (blList == null || blList.Count == 0) return;

            //    List<Guid> ids = new List<Guid>();
            //    foreach (var item in blList) ids.Add(item.ID);

            //    List<ReleaseRCList> list = finService.GetReleaseBLList(ids.ToArray());
            //    bsList.DataSource = list;
            //    bsList.ResetBindings(false);
            //}
            //catch (Exception ex) { LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message); }
        }

        #endregion


        #endregion


    }

}
