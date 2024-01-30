using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.ClientComponents.Controls;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.Extender;
using ICP.Common.ServiceInterface.Client;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Sys.ServiceInterface.DataObjects;

namespace ICP.FAM.UI.WriteOff
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public partial class WriteOffList : BaseListPart
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        #endregion

        #region 成员
        DataPageInfo _dataPageInfo = new DataPageInfo();

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Guid, WriteOffItemList> selectList = new Dictionary<Guid, WriteOffItemList>();
        /// <summary>
        /// 
        /// </summary>
        public override event InvokeGetDataHandler InvokeGetData;
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
        public override event SelectedHandler Selected;
        #endregion

        #region 构造函数
        public WriteOffList()
        {
            InitializeComponent();

            Load += new EventHandler(WriteOffList_Load);
            Disposed += delegate
            {
                CurrentChanged = null;
                Selected = null;
                KeyDown = null;
                InvokeGetData = null;
                CurrentChanging = null;
                gcMain.DataSource = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                _dataPageInfo = null;
                pageControl1.PageChanged -= pageControl1_PageChanged;

                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }


            };

        } 
        #endregion

        #region 初始化

        void WriteOffList_Load(object sender, EventArgs e)
        {

            if (!DesignMode)
            {
                InitMeesage();
                InitControls();
                BulidRowCellStyle();
            }

        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            SetReadOnly(true);

            gvWriteOffList.ShowGridViewRowNo(50);
            cmbWay.BeginUpdate();
            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AR, 0));
            cmbWay.Items.Add(new ImageComboBoxItem("", FeeWay.AP, 1));
            cmbWay.EndUpdate();
        }
        /// <summary>
        /// 初始化消息
        /// </summary>
        private void InitMeesage()
        {
            RegisterMessage("1109270001", LocalData.IsEnglish ? "The writeoff Bill not by you create" : "该销账单不是由您创建的");
            RegisterMessage("1109270002", LocalData.IsEnglish?"The writeoff bill has to the account":"该销账单已经到账");
            RegisterMessage("1109270003", LocalData.IsEnglish ? "The writeoff bill  is approved,it could not be deleted " : "该销账单已经审核,无法删除");
            RegisterMessage("1109270004", LocalData.IsEnglish?"The writeoff bill not to account,cannot approval":"该销账单还未到账,无法审核");
            RegisterMessage("1109270005", LocalData.IsEnglish ? "The bill is already approved" : "该销账单已经审核");
            RegisterMessage("1109270006", LocalData.IsEnglish ? "The writeoff bill , it could not be approved. " : "该销账单还未审核,无法进行取消审核操作");
            RegisterMessage("1109270007", LocalData.IsEnglish ? "The write-off single has to account to account, can not be repeated" : "该销账单已经到账,无法重复到账");
            RegisterMessage("1109270008", LocalData.IsEnglish ? "The write-off single not to account, cannot cancel account" : "该销账单还未到账,无法取消到账");
            RegisterMessage("1109270009", LocalData.IsEnglish?"Are you sure to approve?":"确认审核?");
            RegisterMessage("1109270010", LocalData.IsEnglish ? "Are you sure to undo the approval?" : "确认取消审核?");

            RegisterMessage("1110270001", LocalData.IsEnglish ? "The selected bills should be the same branch/division." : "不是相同的公司的无法选择");
            RegisterMessage("1110270002", LocalData.IsEnglish ? "The selected bills should be the same direction." : "不是相同的方向的无法选择");
            RegisterMessage("1110270003", LocalData.IsEnglish ? "Current data have to account to account, not many" : "当前数据已经到帐,无法多次到帐");
            RegisterMessage("1110270004", LocalData.IsEnglish ? "Current data does not arrive, unable to cancel account" : "当前数据未到帐,无法取消到帐");
            RegisterMessage("1411068888888", LocalData.IsEnglish ? "Are you sure to untie lock" : "确认解锁?");
            
        }

        void BulidRowCellStyle()
        {
            StyleFormatCondition commStyleFormatCondition = new StyleFormatCondition();
            gvWriteOffList.FormatConditions.Add(commStyleFormatCondition);

            commStyleFormatCondition.Appearance.ForeColor = Color.Red;
            commStyleFormatCondition.Condition = FormatConditionEnum.None;
            commStyleFormatCondition.Expression = "[" + colAmount.FieldName + "] < 0";
            commStyleFormatCondition.Condition = FormatConditionEnum.Expression;
            commStyleFormatCondition.ApplyToRow = false;
            commStyleFormatCondition.Column = colAmount;

            
        }
        #endregion

        #region IListPart 成员

        public override object Current
        {
            get { return bsList.Current; }
        }
        protected WriteOffItemList CurrentRow
        {
            get { return Current as WriteOffItemList; }
        }

        public override object DataSource
        {
            get
            {
                return bsList.DataSource;
            }
            set
            {
                PageList list = value as PageList;
                List<WriteOffItemList> itemList=new List<WriteOffItemList>();
                if(list !=null) itemList=list.GetList<WriteOffItemList>();
                if (list == null || itemList==null|| itemList.Count == 0)
                {
                    bsList.DataSource = itemList;
                    pageControl1.TotalPage =pageControl1.CurrentPage= 0;
                    gvWriteOffList.SortInfo.Clear();
                }
                else
                {
                    foreach (WriteOffItemList item in itemList)
                    {
                        //如果是在已选择的列表中，则将"选择"勾上、同时更新数据到至明细列表中
                        if (selectList.Keys.Contains(item.ID))
                        {
                            item.IsCheck = true;
                        }
                    }
                    RefreshSelectList();

                    bsList.DataSource = itemList;
                    bsList.ResetBindings(false);
                    _dataPageInfo = list.DataPageInfo;

                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, Current);
                    }

                    gvWriteOffList.BestFitColumns();


                    if (_dataPageInfo != null)
                    {
                        int totalCount = _dataPageInfo.TotalCount;
                        int pageSize = _dataPageInfo.PageSize;
                        int pageCount = totalCount / pageSize;

                        if (pageCount == 1 && totalCount > pageSize)
                        {
                            pageCount = 2;
                        }
                        if (pageCount == 0 && totalCount > 0)
                        {
                            pageCount = 1;
                        }
                        pageControl1.TotalPage = pageCount;

                        pageControl1.CurrentPage = _dataPageInfo.CurrentPage;
                        gvWriteOffList.SortInfo.Clear();
                        ColumnSortOrder sortOrder = _dataPageInfo.SortOrderType == SortOrderType.Asc ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
                        GridColumn col = null;

                        for (int i = 0; i < gvWriteOffList.Columns.Count; i++)
                        {
                            if (gvWriteOffList.Columns[i].FieldName == _dataPageInfo.SortByName)
                            {
                                col = gvWriteOffList.Columns[i];
                            }
                        }
                        gvWriteOffList.SortInfo.Clear();
                        if (col != null)
                            gvWriteOffList.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                    }

                  
                }

                gvWriteOffList.FocusedRowHandle = 0;
            }
        }

        public List<WriteOffItemList> DataList
        {
            get
            {
                return DataSource as List<WriteOffItemList>;
            }
            set
            {
                DataSource = value;
            }
        }

        public override void RemoveItem(int index)
        {
            bsList.RemoveAt(index);
        }

        public override void RemoveItem(object item)
        {
            bsList.Remove(item);
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
                CurrentChanged(this, CurrentRow);
            }
        }
        #endregion

        #region 私有方法
        void SetReadOnly(bool isReadonly)
        {
            foreach (GridColumn column in gvWriteOffList.Columns)
            {
                column.OptionsColumn.ReadOnly = isReadonly;
                column.OptionsColumn.AllowEdit = !isReadonly;
            }
        }
        #endregion

        #region Grid事件
        private void gvWriteOffList_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            #region 多选模式时勾选某一数据
            if (e.Column == colIsCheck)
            {
                if (Selected != null)
                {
                    if (!CurrentRow.IsCheck)
                    {
                        #region 选中
                        string message = string.Empty;

                        //int t1 = (from s in selectList where s.Value.CompanyID != CurrentRow.CompanyID select s.Value.ID).Count();
                        //if (t1 > 0)
                        //{
                        //    //不是相同公司的数据，不能选择到一起
                        //    Utility.ShowMessage(NativeLanguageService.GetText(this, "1110270001"));

                        //    return;
                        //}
                        int t2 = (from s in selectList where s.Value.Type != CurrentRow.Type select s.Value.ID).Count();
                        if (t2 > 0)
                        {
                            //不是相同方向的，不能选择到一起
                            FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1110270002"));

                            return;
                        }

                        int t3 = (from s in selectList where s.Value.HasRecoginized != CurrentRow.HasRecoginized select s.Value.ID).Count();
                        if (t3 > 0)
                        {
                            if (CurrentRow.HasRecoginized)
                            {
                                //当前是已到帐，但之前都是选择了未到帐的，不允许选择当前行
                                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1110270003"));

                                return;
                            }
                            else
                            {
                                //当前是未到帐，但之前都选择了已到帐的，不允许选择当前行
                                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1110270004"));

                                return;
                            }
                        }



                        if (!selectList.Keys.Contains(CurrentRow.ID))
                        {
                            selectList.Add(CurrentRow.ID, CurrentRow);
                        }

                        #endregion
                    }
                    else
                    {
                        //取消选中，有可能是重新查询过的,指针已发生改变了，所以要根据ID来判断
                        Guid removeID = new Guid(CurrentRow.ID.ToString());
                        var items = (from w in DataList where w.ID == removeID select w);
                        foreach (var itme in items)
                        {
                            WriteOffItemList writeOffItem = itme as WriteOffItemList;
                            if (writeOffItem != null)
                            {
                                if (selectList.Keys.Contains(writeOffItem.ID))
                                {
                                    selectList.Remove(writeOffItem.ID);
                                }
                            }

                        }
                    }

                    Selected(this, selectList.Values.ToList());


                    CurrentRow.IsCheck = !CurrentRow.IsCheck;
                    bsList.ResetCurrentItem();
                }
            }

            #endregion

            //到帐
            if (e.Column == colIsBullion)
            {
                if (CurrentRow.HasRecoginized)
                {
                    Workitem.Commands[WriteOffCommands.Command_CancelBullion].Execute();
                }
                else
                {
                    Workitem.Commands[WriteOffCommands.Command_Bullion].Execute();
                }
            }
            if (e.Clicks == 2)
            {
                Workitem.Commands[WriteOffCommands.Command_EditData].Execute();
            }
        }

        /// <summary>
        /// 设置行样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvWriteOffList_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            WriteOffItemList list = gvWriteOffList.GetRow(e.RowHandle) as WriteOffItemList;
            if (list == null) return;
            //if (list.HasRecoginized) e.Appearance.BackColor = System.Drawing.Color.PowderBlue;
            if (list.HasRecoginized)
            {
                e.Appearance.ForeColor = Color.Blue;
            }
            if (list.IsValid == false)
            {
                GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Disabled);
            }
        }
        #endregion

        #region Work Item

        /// <summary>
        /// 刷新
        /// </summary>
        private void RefreshControls()
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                bsList.ResetCurrentItem();

                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }
            }
        }

        #region 编辑
        [CommandHandler(WriteOffCommands.Command_EditData)]
        public void Command_EditData(object sender, EventArgs e) {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                EditData();
            }
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        protected void EditData()
        {
            if (CurrentRow == null)
            {
                return;
            }

            string title = LocalData.IsEnglish ? "Edit Check" : "编辑销帐单";
            PartLoader.ShowEditPart<WriteOffEditPart>(Workitem.Items.AddNew<WorkItem>(), CurrentRow, title, EditPartSaved);
        }
        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (prams == null || prams.Length == 0)
                {
                    return;
                }
                WriteOffItemInfo checkInfo = prams[0] as WriteOffItemInfo;
                if (checkInfo == null)
                {
                    return;
                }
                List<Guid> checkIDList = new List<Guid>();
                checkIDList.Add(checkInfo.ID);

                List<WriteOffItemList> newList = FinanceService.GetWriteOffListByIds(checkIDList.ToArray());

                if (newList == null || newList.Count == 0)
                {
                    return;
                }
                bool isChangeSelectList = false;


                foreach (WriteOffItemList data in newList)
                {
                    if (DataList == null || DataList.Count == 0)
                    {
                        DataSource = new List<WriteOffItemList>();

                        bsList.Add(data);
                    }
                    else
                    {
                        WriteOffItemList tager = DataList.Find(delegate(WriteOffItemList item) { return item.ID == data.ID; });

                        if (tager == null)
                        {
                            bsList.Insert(0, data);
                        }
                        else
                        {
                            FAMUtility.CopyToValue(data, tager, typeof(WriteOffItemList));

                            //更新已选择列表中的信息
                            if (selectList.Keys.Contains(tager.ID))
                            {
                                isChangeSelectList = true;
                            }
                        }


                    }
                }
                //如果当前数据已经被选择了，则更新多选列表中的内容
                if (isChangeSelectList)
                {
                    RefreshSelectList();
                }

                bsList.ResetBindings(false);

                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }

            }
        }


        #endregion

        #region 新增收款
        [CommandHandler(WriteOffCommands.Command_WriteOff_AddData_DR)]
        public void Command_WriteOff_AddData_DR(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "Receivables" : "收账";

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("FeeWay", FeeWay.AR);
                PartLoader.ShowEditPart<WriteOffEditPart>(Workitem.Items.AddNew<WorkItem>(), null, dic, title, EditPartSaved, string.Empty);
            }
        }
        #endregion

        #region 新增付款
        [CommandHandler(WriteOffCommands.Command_WriteOff_AddData_CR)]
        public void Command_WriteOff_AddData_CR(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                string title = LocalData.IsEnglish ? "Payment" : "付账";

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("FeeWay", FeeWay.AP);
                PartLoader.ShowEditPart<WriteOffEditPart>(Workitem.Items.AddNew<WorkItem>(), null, dic, title, EditPartSaved, string.Empty);
            }
        }
        #endregion

        #region 删除
        [CommandHandler(WriteOffCommands.Command_DeleteData)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(CurrentRow.ApprovalByName))
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109270003"));
                return;
            }

            UserInfo creater = UserService.GetUserInfo(CurrentRow.CreatedByID);
            if (creater.IsValid && creater.ID != LocalData.UserInfo.LoginID)
            {
                FAMUtility.ShowMessage(LocalData.IsEnglish ? "The write off record not your own creation can not be deleted, please contact to create delete ！" : "此销账记录不是您本人创建不能删除，请联系创建人删除！");
                return;
            }

            string createMessage = string.Empty;
            string bullionsMessage = string.Empty;
            string deleteMessage = LocalData.IsEnglish ? "Srue Delete Current Data?" : "确认删除当前数据?";
            string message = string.Empty;

            if (CurrentRow.CreatedByID != LocalData.UserInfo.LoginID)
            {
                createMessage = NativeLanguageService.GetText(this, "1109270001");
            }
            if (!string.IsNullOrEmpty(CurrentRow.BankByName))
            {
                bullionsMessage = NativeLanguageService.GetText(this, "1109270002");
            }
            if (!string.IsNullOrEmpty(createMessage))
            {
                message = createMessage;
            }
            if (!string.IsNullOrEmpty(bullionsMessage))
            {
                if (!string.IsNullOrEmpty(message))
                {
                    message = message + Environment.NewLine + bullionsMessage;
                }
                else
                {
                    message = bullionsMessage;
                }
            }

            if (string.IsNullOrEmpty(message))
            {
                message = deleteMessage;
            }
            else
            {
                message = message + Environment.NewLine + deleteMessage;
            }

            if (!FAMUtility.ShowResultMessage(message))
            {
                return;
            }

            try
            {
                Guid id = CurrentRow.CheckID;
                if (!CurrentRow.IsMultCurrency)
                {
                    //不是多币种的时候，删除全部的
                    FinanceService.RemoveWriterOff(CurrentRow.CheckID, LocalData.UserInfo.LoginID, CurrentRow.CheckUpdateDate);
                    bsList.RemoveCurrent();
                    bsList.ResetBindings(false);
                }
                else
                {

                    //多币种的时候，只删除当前币种的
                    FinanceService.RemoveWriterCurrency(CurrentRow.ID, LocalData.UserInfo.LoginID, CurrentRow.UpdateDate);
                    bsList.RemoveCurrent();
                    bsList.ResetBindings(false);

                    //刷新其他的数据
                    List<Guid> idList = new List<Guid>();
                    idList.Add(id);
                    RefreshUI(idList);
                }
                //刷新已选择的列表
                if (selectList.Keys.Contains(id))
                {
                    RefreshSelectList();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }
        }
        #endregion

        #region 作废
        [CommandHandler(WriteOffCommands.Command_VoidData)]
        public void Command_VoidData(object sender, EventArgs e)
        {
            if (CurrentRow != null)
            {
                if (CurrentRow.IsValid)
                {
                    if (!FAMUtility.ShowResultMessage(LocalData.IsEnglish ? "Srue Void Current Data?" : "确认作废当前数据?"))
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
                //else
                //{
                //    if (!Utility.ShowResultMessage(LocalData.IsEnglish ? "Srue Activation Current Data?" : "确认激活当前数据?"))
                //    {
                //        return;
                //    }
                //}
             
                try
                {
                    SingleResult result = FinanceService.VoidCheckData(
                        CurrentRow.CheckID,
                        !CurrentRow.IsValid,
                        LocalData.UserInfo.LoginID,
                        CurrentRow.CheckUpdateDate,
                        LocalData.IsEnglish);

                    CurrentRow.IsValid = !CurrentRow.IsValid;
                    CurrentRow.CheckUpdateDate = result.GetValue<DateTime?>("UpdateDate");

                    if (CurrentChanged != null)
                    {
                        CurrentChanged(this, CurrentRow);
                    }

                    //if (!this.CurrentRow.IsValid)
                    //{
                        LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel ChekckData Successfully." : "销帐信息已经成功作废.");
                    //}
                    //else
                    //{
                    //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), LocalData.IsEnglish ? "Available ChekckData Successfully." : "销帐信息已经成功激活.");
                    //}
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),  ex.Message);
                }
            }
        }
        #endregion

        #region 查看凭证

        [CommandHandler(WriteOffCommands.Command_ListCredentials)]
        public void Command_ListCredentials(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                CredentialsDetailEditor view = Workitem.Items.AddNew<CredentialsDetailEditor>();
                WriteOffItemList currentItem = bsList.Current as WriteOffItemList;
                view.WriteOffID = currentItem.CheckID;
                view.VoucherSeqNo = currentItem.VoucherSeqNo;

                view.Text = LocalData.IsEnglish ? "Credentials Detail" : "凭证明细";
                DialogResult dlg = view.ShowDialog();
            }
        }

        #endregion

        #region 到账
        [CommandHandler(WriteOffCommands.Command_Bullion)]
        public void Command_Bullion(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(CurrentRow.BankByName))
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109270007"));
                    return;
                }
                SetAccountInfo setAccount = Workitem.Items.AddNew<SetAccountInfo>();
                setAccount.ItemList = CurrentRow;
                string title = LocalData.IsEnglish ? "Set Bullion" : "设置到帐信息";
                if (PartLoader.ShowDialog(setAccount, title) == DialogResult.OK)
                {
                    FAMUtility.CopyToValue(setAccount.ItemList, CurrentRow, typeof(WriteOffItemList));
                    CurrentRow.Amount = CurrentRow.FinalAmount;

                    RefreshControls();
                    if (selectList.Keys.Contains(CurrentRow.ID))
                    {
                        RefreshSelectList();
                    }

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Set Successfully" : "设置成功");
                }
            }
        }

        #endregion

        #region 取消到账
        [CommandHandler(WriteOffCommands.Command_CancelBullion)]
        public void Command_CancelBullion(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(CurrentRow.BankByName))
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109270008"));
                    return;
                }
                if (!string.IsNullOrEmpty(CurrentRow.ApprovalByName))
                {
                    //已审核的，无法取消到账
                    return;
                }
                CancelBullion cancelBullion = Workitem.Items.AddNew<CancelBullion>();
                cancelBullion.ItemList = CurrentRow;
                string title = LocalData.IsEnglish ? "Cancel Bullion" : "取消到账";
                if (PartLoader.ShowDialog(cancelBullion, title) == DialogResult.OK)
                {
                    FAMUtility.CopyToValue(cancelBullion.ItemList, CurrentRow, typeof(WriteOffItemList));

                    RefreshControls();
                    if (selectList.Keys.Contains(CurrentRow.ID))
                    {
                        RefreshSelectList();
                    }


                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel Successfully" : "取消成功");
                }
            }
        }
        #endregion

        #region 审核
        [CommandHandler(WriteOffCommands.Command_Auditor)]
        public void Command_Auditor(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(CurrentRow.ApprovalByName))
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109270005"));
                    return;
                }
                if (string.IsNullOrEmpty(CurrentRow.BankByName))
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109270004"));
                    return;
                }
                if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1109270009")))
                {
                    return;
                }
                try
                {
                    ManyResult result = FinanceService.AuditorWriterOff(
                         new Guid[] { CurrentRow.CheckID },
                         new DateTime?[] { CurrentRow.CheckUpdateDate },
                         LocalData.UserInfo.LoginID,
                         true);

                    CurrentRow.ApprovalByName = LocalData.UserInfo.LoginName;
                    CurrentRow.UpdateDate = result.Items[0].GetValue<DateTime?>("UpdateDate");
                    CurrentRow.CheckUpdateDate = result.Items[0].GetValue<DateTime?>("CheckUpdateDate");
                    CurrentRow.VoucherSeqNo = result.Items[0].GetValue<String>("VoucherSeqNo");

                    //刷新工具栏及当前数据
                    RefreshControls();

                    //刷新其他的数据
                    List<Guid> idList = new List<Guid>();
                    idList.Add(CurrentRow.CheckID);
                    RefreshUI(idList);
                    if (selectList.Keys.Contains(CurrentRow.ID))
                    {
                        RefreshSelectList();
                    }
                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Auditor Successfully" : "审核成功");

                    string message = LocalData.IsEnglish ? "VoucherSeqNo:" : "凭证号:";
                    message += CurrentRow.VoucherSeqNo;
                    FAMUtility.ShowMessage(message);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }
        #endregion

        #region 取消审核
        [CommandHandler(WriteOffCommands.Command_UnAuditor)]
        public void Command_UnAuditor(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(CurrentRow.ApprovalByName))
                {
                    FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1109270006"));
                    return;
                }
                if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1109270010")))
                {
                    return;
                }

                try
                {
                    ManyResult result = FinanceService.AuditorWriterOff(
                         new Guid[] { CurrentRow.CheckID },
                         new DateTime?[] { CurrentRow.CheckUpdateDate },
                         LocalData.UserInfo.LoginID,
                         false);

                    CurrentRow.ApprovalByName = string.Empty;
                    CurrentRow.UpdateDate = result.Items[0].GetValue<DateTime?>("UpdateDate");
                    CurrentRow.CheckUpdateDate = result.Items[0].GetValue<DateTime?>("CheckUpdateDate");
                    CurrentRow.VoucherSeqNo = result.Items[0].GetValue<String>("VoucherSeqNo");

                    //刷新工具栏及当前数据
                    RefreshControls();
                    //刷新其他的数据
                    List<Guid> idList = new List<Guid>();
                    idList.Add(CurrentRow.CheckID);
                    RefreshUI(idList);
                    if (selectList.Keys.Contains(CurrentRow.ID))
                    {

                        RefreshSelectList();
                    }

                    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Cancel Successfully" : "取消成功");
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }
            }
        }
        #endregion

        #region 允许多选
        public void SetCheck(bool isVisible)
        {
            colIsCheck.VisibleIndex = 0;
            colIsCheck.Width = 50;
            colIsCheck.Visible = isVisible;
        }
        #endregion

        #region 打印
        [CommandHandler(WriteOffCommands.Command_PrintCheck)]
        public void Command_PrintCheck(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                //销帐信息数据源
                WriteOffItemInfo writeOffItemInfo = new WriteOffItemInfo();
                List<WriteOffBill> chargeList = new List<WriteOffBill>();

                /// 账单、费用列表
                List<WriteOffBill> billList = new List<WriteOffBill>();
                /// 币种金额列表
                List<OperationCurrencyAmountList> amountList = new List<OperationCurrencyAmountList>();
                /// 其他项目数据列表
                List<WriteOffCharge> expensesList = new List<WriteOffCharge>();

                #region  获得数据
                writeOffItemInfo = FinanceService.GetWriteOffItemInfo(CurrentRow.CheckID);
                if (writeOffItemInfo == null)
                {
                    return;
                }

                chargeList = FinanceService.GetWriteOffBillsByIds(CurrentRow.CheckID);
                amountList = FinanceService.GetOperationCurrencyAmountList(CurrentRow.CheckID);
                expensesList = FinanceService.GetWriteOffCharges(CurrentRow.CheckID);

                if (chargeList == null)
                {
                    chargeList = new List<WriteOffBill>();
                }
                if (writeOffItemInfo.CheckMode == CheckMode.Bill)
                {
                    #region 账单
                    billList = (from d in chargeList
                                group d by new { d.BillID, BillRefNo = d.BillNo, d.CurrencyID, d.CurrencyName, d.Way, d.IsCommission, d.BillAmount } into g
                                select new WriteOffBill
                                {
                                    BillID = g.Key.BillID,
                                    BillNo = g.Key.BillRefNo,
                                    CurrencyID = g.Key.CurrencyID,
                                    CurrencyName = g.Key.CurrencyName,
                                    Way = g.Key.Way,
                                    IsCommission = g.Key.IsCommission,
                                    Amount= g.Key.BillAmount,
                                    //Amount = g.Sum(p => p.Amount),
                                    AvailbeWriteOffAmount = g.Sum(p => p.AvailbeWriteOffAmount),
                                    ExchangeRate = g.Max(p => p.ExchangeRate),
                                    FinalAmount = g.Sum(p => p.FinalAmount),
                                    WriteOffAmount = g.Sum(p => p.WriteOffAmount)
                                }).ToList();

                    #endregion
                }
                else
                {
                    #region 费用

                    billList = (from d in chargeList
                                group d by new { d.BillID, BillNo = d.BillNo, d.CurrencyID, d.CurrencyName, d.Way, d.IsCommission, d.ChargingCodeID, d.ChargeName } into g
                                select new WriteOffBill
                                {
                                    BillID = g.Key.BillID,
                                    BillNo = g.Key.BillNo,
                                    CurrencyID = g.Key.CurrencyID,
                                    CurrencyName = g.Key.CurrencyName,
                                    Way = g.Key.Way,
                                    IsCommission = g.Key.IsCommission,
                                    ChargingCodeID = g.Key.ChargingCodeID,
                                    ChargeName = g.Key.ChargeName,
                                    Amount = g.Sum(p => p.Amount),
                                    AvailbeWriteOffAmount = g.Sum(p => p.AvailbeWriteOffAmount),
                                    ExchangeRate = g.Max(p => p.ExchangeRate),
                                    FinalAmount = g.Sum(p => p.FinalAmount),
                                    WriteOffAmount = g.Sum(p => p.WriteOffAmount)
                                }).ToList();

                    #endregion
                }

                #endregion

                CompanyReportConfigureList reportConfigure
                  = ConfigureService.GetReportConfigureList(CurrentRow.CompanyID, UI.ModuleConstantsForFAM.WriteOffReportType);
                if (reportConfigure == null || reportConfigure.Parameters == null || reportConfigure.Parameters.Count == 0)
                {
                    #region 收付款清单和对冲单

                    string titleEName = string.Empty;
                    string titleCName = string.Empty;
                    try
                    {
                        WriteOffBillReportData reportData = new WriteOffBillReportData();
                        reportData.BaseReportData = new WriteOffBillBaseReportData();
                        //reportData.BaseReportData.CompanyName = LocalData.UserInfo.DefaultCompanyName;
                        reportData.BaseReportData.CompanyName = writeOffItemInfo.CompanyName;
                        reportData.BaseReportData.No = writeOffItemInfo.No;
                        reportData.BaseReportData.Customer = writeOffItemInfo.CustomerName;
                        reportData.BaseReportData.Remark = writeOffItemInfo.Remark;
                        reportData.BaseReportData.CreateBy = writeOffItemInfo.CreateByName;
                        reportData.BaseReportData.PrintDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);

                        int flag = 0;
                        foreach (var bill in billList)
                        {
                            if (bill.Way == FeeWay.AP)
                            {
                                flag++;
                            }
                            else if (bill.Way == FeeWay.AR)
                            {
                                flag--;
                            }
                        }

                        if (Math.Abs(flag) != billList.Count)
                        {
                            titleEName = "Print Hedge Bill";
                            titleCName = "打印对冲单";
                            if (writeOffItemInfo.Way == FeeWay.AR)
                            {
                                reportData.BaseReportData.ReceivedOrPayedDateLabel = LocalData.IsEnglish ? "Received Date:" : "收款日期:";
                                reportData.BaseReportData.ActualReceivedOrPayedAmountLabel = LocalData.IsEnglish ? "Receive Actually:" : "实收金额:";
                                reportData.BaseReportData.UnReceivedOrPayAmountLabel = LocalData.IsEnglish ? "UnReceived Amount" : "未收金额";
                            }
                            else
                            {
                                reportData.BaseReportData.ReceivedOrPayedDateLabel = LocalData.IsEnglish ? "Payment Date:" : "付款日期:";
                                reportData.BaseReportData.ActualReceivedOrPayedAmountLabel = LocalData.IsEnglish ? "Pay Actually:" : "实付金额:";
                                reportData.BaseReportData.UnReceivedOrPayAmountLabel = LocalData.IsEnglish ? "UnPay Amount" : "未付金额";
                            }
                        }
                        else if (writeOffItemInfo.Way == FeeWay.AR)
                        {
                            titleEName = "Print Collection List";
                            titleCName = "打印收款清单";
                            reportData.BaseReportData.ReportTitle = LocalData.IsEnglish ? "Collection List" : "收款清单";
                            reportData.BaseReportData.ReceivedOrPayedDateLabel = LocalData.IsEnglish ? "Received Date:" : "收款日期:";
                            reportData.BaseReportData.ActualReceivedOrPayedAmountLabel = LocalData.IsEnglish ? "Receive Actually:" : "实收金额:";
                            reportData.BaseReportData.UnReceivedOrPayAmountLabel = LocalData.IsEnglish ? "UnReceived Amount" : "未收金额";
                        }
                        else
                        {
                            titleEName = "Print Payment List";
                            titleCName = "打印付款清单";
                            reportData.BaseReportData.ReportTitle = LocalData.IsEnglish ? "Payment List" : "付款清单";
                            reportData.BaseReportData.ReceivedOrPayedDateLabel = LocalData.IsEnglish ? "Payment Date:" : "付款日期:";
                            reportData.BaseReportData.ActualReceivedOrPayedAmountLabel = LocalData.IsEnglish ? "Pay Actually:" : "实付金额:";
                            reportData.BaseReportData.UnReceivedOrPayAmountLabel = LocalData.IsEnglish ? "UnPay Amount" : "未付金额";
                        }

                        string receivedOrPayedDateString = string.Empty;
                        string bank = string.Empty;
                        string actuallyAmount = string.Empty;

                        foreach (var currencyItem in amountList)
                        {
                            if (receivedOrPayedDateString != string.Empty && currencyItem.BankDate != null)
                            {
                                receivedOrPayedDateString += ", ";
                            }

                            if (actuallyAmount != string.Empty)
                            {
                                actuallyAmount += ", ";
                            }

                            if (bank != string.Empty && currencyItem.BankAccountID != null && currencyItem.BankAccountID != Guid.Empty)
                            {
                                bank += ", ";
                            }

                            if (currencyItem.BankDate != null)
                            {
                                receivedOrPayedDateString += "(" + currencyItem.CurrencyName + ")" + currencyItem.BankDate.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                            }

                            actuallyAmount += "(" + currencyItem.CurrencyName + ")" + currencyItem.TotalAmount.ToString("n");


                            bank += currencyItem.BankName;
                        }

                        reportData.BaseReportData.ReceivedOrPayedDate = receivedOrPayedDateString;
                        reportData.BaseReportData.Bank = bank;
                        reportData.BaseReportData.ActualReceivedOrPayedAmount = actuallyAmount;
                        reportData.BaseReportData.AmountWrittenOff = actuallyAmount;

                        reportData.DetailList = new List<WriteOffBillDetailReportData>();
                        foreach (var bill in billList)
                        {
                            WriteOffBillDetailReportData billItem = new WriteOffBillDetailReportData();
                            billItem.ChargeName = bill.ChargeName;  //如果是“账单模式”，则为空
                            if (bill.IsCommission)
                            {
                                billItem.BillRefNo = bill.BillNo + "(Y)";
                            }
                            else
                            {
                                billItem.BillRefNo = bill.BillNo;
                            }

                            billItem.Currency = bill.CurrencyName;
                            billItem.Amount = bill.Way == FeeWay.AR ? bill.Amount.ToString("n") : (-bill.Amount).ToString("n");
                            billItem.WriteOffAmount = bill.Way == FeeWay.AR ? bill.WriteOffAmount.ToString("n") : (-bill.WriteOffAmount).ToString("n");
                            //billItem.UnReceivedOrPayAmount = bill.WriteOffAmount < 0 ? "0.00" : (bill.Way == FeeWay.AR ? (bill.Amount - bill.WriteOffAmount).ToString("n") : (bill.WriteOffAmount - bill.Amount).ToString("n"));

                            if (bill.AvailbeWriteOffAmount == 0)
                            {
                                billItem.UnReceivedOrPayAmount = "0.00";
                            }
                            else
                            {
                                billItem.UnReceivedOrPayAmount = bill.WriteOffAmount < 0 ? "0.00" : (bill.Way == FeeWay.AR ? (bill.Amount - bill.WriteOffAmount).ToString("n") : (bill.WriteOffAmount - bill.Amount).ToString("n"));
                            }

                            billItem.FinalAmount = bill.Way == FeeWay.AR ? bill.FinalAmount.ToString("n") : (-bill.FinalAmount).ToString("n");
                            billItem.ExchangeRate = bill.ExchangeRate.ToString();
                            reportData.DetailList.Add(billItem);
                        }

                        reportData.TotalWriteOfFeeList = new List<TotalWriteOffFeeReportData>();
                        IEnumerable<IGrouping<String, WriteOffBill>> arr = billList.GroupBy(i => i.CurrencyName);
                        foreach (IGrouping<String, WriteOffBill> item in arr)
                        {
                            List<WriteOffBill> listByGroup = item.ToList<WriteOffBill>();
                            TotalWriteOffFeeReportData totalFeeItem = new TotalWriteOffFeeReportData();
                            totalFeeItem.TotalBillAmount = listByGroup.Sum(p => p.Way == FeeWay.AR ? p.Amount : -p.Amount).ToString("n");
                            totalFeeItem.TotalWriteOffAmount = listByGroup.Sum(p => p.Way == FeeWay.AR ? p.WriteOffAmount : -p.WriteOffAmount).ToString("n");
                            totalFeeItem.FinalAmount = listByGroup.Sum(p => p.Way == FeeWay.AR ? p.FinalAmount : -p.FinalAmount).ToString("n");

                            totalFeeItem.Currency = item.Key;

                            reportData.TotalWriteOfFeeList.Add(totalFeeItem);
                        }

                        #region 核销金额
                        string totalWriteOfString = string.Empty;
                        foreach (var item in reportData.TotalWriteOfFeeList)
                        {
                            if (!string.IsNullOrEmpty(totalWriteOfString))
                            {
                                totalWriteOfString += ", ";
                            }

                            totalWriteOfString += "(" + item.Currency + ")" + item.TotalWriteOffAmount;
                        }

                        reportData.BaseReportData.AmountWrittenOff = totalWriteOfString;
                        #endregion

                        if (Math.Abs(flag) != billList.Count)
                        {
                            reportData.DebitList = new List<TotalWriteOffFeeReportData>();
                            reportData.CreditList = new List<TotalWriteOffFeeReportData>();
                            IEnumerable<IGrouping<FeeWay, WriteOffBill>> arrWay = billList.GroupBy(i => i.Way);
                            foreach (IGrouping<FeeWay, WriteOffBill> item in arrWay)
                            {
                                if (item.Key == FeeWay.AR)
                                {
                                    List<WriteOffBill> listByDR = item.ToList<WriteOffBill>();
                                    IEnumerable<IGrouping<string, WriteOffBill>> arrCurrency = listByDR.GroupBy(i => i.CurrencyName);
                                    foreach (var itemByDRAndCurrency in arrCurrency)
                                    {
                                        List<WriteOffBill> listByDRAndCurrency = itemByDRAndCurrency.ToList<WriteOffBill>();
                                        TotalWriteOffFeeReportData WayItem = new TotalWriteOffFeeReportData();
                                        WayItem.Currency = itemByDRAndCurrency.Key;
                                        WayItem.TotalBillAmount = itemByDRAndCurrency.Sum(i => i.Amount).ToString("n");
                                        WayItem.TotalWriteOffAmount = itemByDRAndCurrency.Sum(i => i.WriteOffAmount).ToString("n");
                                        WayItem.FinalAmount = itemByDRAndCurrency.Sum(i => i.FinalAmount).ToString("n");
                                        reportData.DebitList.Add(WayItem);
                                    }

                                    if (reportData.DebitList.Count > 0)
                                    {
                                        reportData.DebitList[0].LabelText = LocalData.IsEnglish ? "Debit:" : "应收:";
                                    }
                                }
                                else
                                {
                                    List<WriteOffBill> listByCR = item.ToList<WriteOffBill>();

                                    IEnumerable<IGrouping<string, WriteOffBill>> arrCurrency = listByCR.GroupBy(i => i.CurrencyName);
                                    foreach (var itemByCRAndCurrency in arrCurrency)
                                    {
                                        List<WriteOffBill> listByDRAndCurrency = itemByCRAndCurrency.ToList<WriteOffBill>();
                                        TotalWriteOffFeeReportData cRWayItem = new TotalWriteOffFeeReportData();
                                        cRWayItem.Currency = itemByCRAndCurrency.Key;
                                        cRWayItem.TotalBillAmount = (-itemByCRAndCurrency.Sum(i => i.Amount)).ToString("n");
                                        cRWayItem.TotalWriteOffAmount = (-itemByCRAndCurrency.Sum(i => i.WriteOffAmount)).ToString("n");
                                        cRWayItem.FinalAmount = (-itemByCRAndCurrency.Sum(i => i.FinalAmount)).ToString("n");
                                        reportData.CreditList.Add(cRWayItem);
                                    }

                                    if (reportData.CreditList.Count > 0)
                                    {
                                        reportData.CreditList[0].LabelText = LocalData.IsEnglish ? "Credit:" : "应付:";
                                    }
                                }
                            }
                        }

                        reportData.ChargeReportDataList = new List<WriteOffChargeReportData>();
                        foreach (var charge in expensesList)
                        {
                            WriteOffChargeReportData chargeItem = new WriteOffChargeReportData();
                            chargeItem.CustomerName = charge.CustomerName;
                            chargeItem.BillNo = charge.BillNo;

                            chargeItem.GLDescription = charge.GLDescription;
                            chargeItem.Currency = charge.CurrencyName;
                            chargeItem.Amout = charge.Way == FeeWay.AR ? charge.Amount.ToString("n") : (-charge.Amount).ToString("n");
                            chargeItem.ExchangeRate = charge.ExchangeRate.ToString();
                            chargeItem.Remark = charge.Remark;
                            reportData.ChargeReportDataList.Add(chargeItem);
                        }

                        reportData.TotalChargeFeeList = new List<TotalWriteOffFeeReportData>();


                        IEnumerable<IGrouping<String, WriteOffCharge>> arrExpenses = expensesList.GroupBy(i => i.CurrencyName);
                        foreach (IGrouping<String, WriteOffCharge> expenseItem in arrExpenses)
                        {
                            TotalWriteOffFeeReportData chargeTotalItem = new TotalWriteOffFeeReportData();

                            chargeTotalItem.Currency = expenseItem.Key;
                            chargeTotalItem.TotalBillAmount = expenseItem.ToList<WriteOffCharge>().Sum(p => p.Way == FeeWay.AR ? p.Amount : -p.Amount).ToString("n");
                            reportData.TotalChargeFeeList.Add(chargeTotalItem);
                        }

                        IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? titleEName : titleCName, (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                        string fileName = Application.StartupPath + "\\Reports\\FAM\\";
                        if (Math.Abs(flag) != billList.Count)
                        {
                            if (writeOffItemInfo.CheckMode == CheckMode.Bill || writeOffItemInfo.CheckMode == CheckMode.Charge)  //其实英文版本没用到
                            {
                                fileName += LocalData.IsEnglish ? "HedgeBill_EN.frx" : "HedgeBill_CN.frx";
                            }
                            //else
                            //{
                            //    fileName += LocalData.IsEnglish ? "HedgeFee_EN.frx" : "HedgeFee_CN.frx";
                            //}
                        }
                        else if (writeOffItemInfo.CheckMode == CheckMode.Bill || writeOffItemInfo.CheckMode == CheckMode.Charge)
                        {
                            fileName += LocalData.IsEnglish ? "WriteOffBill_EN.frx" : "WriteOffBill_CN.frx";
                        }
                        //else
                        //{
                        //    fileName += LocalData.IsEnglish ? "WriteOffFee_EN.frx" : "WriteOffFee_CN.frx";
                        //}

                        Dictionary<string, object> reportSource = new Dictionary<string, object>();
                        reportSource.Add("WriteOffBillBaseReportData", reportData.BaseReportData);
                        reportSource.Add("WriteOffBillDetailReportData", reportData.DetailList.OrderBy(i => i.BillRefNo).ToList());
                        reportSource.Add("TotalWriteOffFeeReportData", reportData.TotalWriteOfFeeList);
                        reportSource.Add("WriteOffChargeReportData", reportData.ChargeReportDataList);
                        reportSource.Add("TotalChargeFeeReportData", reportData.TotalChargeFeeList);
                        if (Math.Abs(flag) != billList.Count)
                        {
                            reportSource.Add("DebitBillDetailReportData", reportData.DebitList);
                            reportSource.Add("CreditBillDetailReportData", reportData.CreditList);
                        }

                        viewer.BindData(fileName, reportSource, null);
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                    }

                    #endregion
                }
                else
                {
                    // 打印支票
                    PrintCash(reportConfigure.Parameters[0], writeOffItemInfo, billList, expensesList);
                }
            }
        }

        #region 打印支票

        /// <summary>
        /// 打印支票
        /// </summary>
        private void PrintCash(ReportParameterList para, WriteOffItemInfo currentItemInfo, List<WriteOffBill> currentBillList, List<WriteOffCharge> expensesList)
        {
            if (para == null) return;
            //WriteOffItemInfo currentItemInfo = bsWriteOff.DataSource as WriteOffItemInfo;
            //List<WriteOffBill> currentBillList = bsBills.DataSource as List<WriteOffBill>;
            string fileName = string.Empty;
            string titleString = string.Empty;
            CashReportData reportData = new CashReportData();
            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            #region 构建数据源
            try
            {
                reportData.BaseReportData = new CashBaseReportData();
                reportData.BaseReportData.CustomerName = currentItemInfo.CustomerName;
                reportData.BaseReportData.Remark = currentItemInfo.Remark;
                reportData.BaseReportData.CheckNO = currentItemInfo.CheckNo;
                decimal amount = 0m;
                if (currentBillList != null && currentBillList.Count > 0)
                {
                    #region Amount
                    IEnumerable<IGrouping<Guid, WriteOffBill>> arr = currentBillList.GroupBy(i => i.CurrencyID);
                    foreach (IGrouping<Guid, WriteOffBill> item in arr)  //应该只有一个币种
                    {
                        List<WriteOffBill> listByGroup = item.ToList<WriteOffBill>();
                        amount = listByGroup.Sum(p => p.Way == FeeWay.AR ? p.WriteOffAmount : -p.WriteOffAmount);
                    }
                    #endregion
                }

                if (expensesList != null && expensesList.Count > 0)
                {
                    amount += expensesList.Sum(i => i.Way == FeeWay.AR ? i.Amount : -i.Amount);
                }

                reportData.BaseReportData.Amount = Math.Abs(amount).ToString("n");
                #region Total
                reportData.BaseReportData.Total = ReportHelper.GetText(Math.Abs(Convert.ToDecimal(reportData.BaseReportData.Amount)));
                #endregion

                fileName = Application.StartupPath + "\\Reports\\FAM\\";
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
            #endregion

            if (para.ParameterValue.ToUpper() == ("check_la").ToUpper() ||
                (currentItemInfo.Way == FeeWay.AR &&
                (para.ParameterValue.ToUpper() == ("check_ca").ToUpper() || para.ParameterValue.ToUpper() == ("check_jdy").ToUpper() || 
                para.ParameterValue.ToUpper() == ("check_nj").ToUpper())))  //洛杉矶公司
            {
                reportData.BaseReportData.CheckDate = currentItemInfo.CheckDate.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);

                #region 拼装客户地址
                CustomerInfo customerInfo = CustomerService.GetCustomerInfo(currentItemInfo.CustomerID);
                reportData.BaseReportData.CustomerEAddress = customerInfo.EAddress;
                if (!string.IsNullOrEmpty(customerInfo.EMail))
                {
                    reportData.BaseReportData.CustomerEAddress += Environment.NewLine + "EMAIL: " + customerInfo.EMail;
                }

                if (!string.IsNullOrEmpty(customerInfo.Tel1))
                {
                    reportData.BaseReportData.CustomerEAddress += Environment.NewLine + "TEL: " + customerInfo.Tel1;
                }

                if (!string.IsNullOrEmpty(customerInfo.Fax))
                {
                    reportData.BaseReportData.CustomerEAddress += Environment.NewLine + "FAX: " + customerInfo.Tel1;
                }

                List<CustomerContactList> contacts = CustomerService.GetCustomerContactList(currentItemInfo.CustomerID);
                if (contacts != null && contacts.Count > 0)
                {
                    reportData.BaseReportData.CustomerEAddress += Environment.NewLine + "Attn: " + contacts[0].EName;
                }

                #endregion


                #region 账单明细
                reportData.BillList = new List<CashBillReportData>();
                if (currentBillList != null && currentBillList.Count > 0)
                {
                    List<Guid> billIds = new List<Guid>();

                    foreach (var item in currentBillList)
                    {
                        billIds.Add(item.BillID);

                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = item.RefNo;
                        billItem.BillNo = item.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == item.Way ? item.WriteOffAmount.ToString("n") : (-item.WriteOffAmount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }

                    List<CurrencyBillList> billList = FinanceService.GetBillListByIds(billIds.ToArray(), LocalData.IsEnglish);
                    foreach (var bi in reportData.BillList)
                    {
                        CurrencyBillList billFind = (from d in billList where d.BillNO == bi.BillNo select d).Take(1).SingleOrDefault();
                        if (billFind != null)
                        {
                            bi.BLNo = billFind.BLNo;
                            bi.RefNo = billFind.OperationNO; 
                        }
                    }
                }

                if (expensesList != null && expensesList.Count > 0)
                {
                    foreach (var charge in expensesList)
                    {
                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = charge.GLDescription;
                        billItem.BillNo = charge.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == charge.Way ? charge.Amount.ToString("n") : (-charge.Amount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }
                }

                #endregion

                fileName += "RPT_Check_LA.frx";
                titleString = "LA";
                reportSource.Add("BaseReportData", reportData.BaseReportData);
                if (reportData.BillList != null)
                {
                    reportSource.Add("BillListReportData", reportData.BillList);
                }
            }
            else if ((currentItemInfo.Way == FeeWay.AP &&
                (para.ParameterValue.ToUpper() == ("check_nj").ToUpper() || para.ParameterValue.ToUpper() == ("check_jdy").ToUpper())) ||
                para.ParameterValue.ToUpper() == ("check_Pacgran").ToUpper())  //芝加哥公司,CTC International Inc.,纽约公司 或者 PACGRAN INC.(拖车公司支票)
            {
                reportData.BaseReportData.CheckDate = currentItemInfo.CheckDate.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);

                #region 拼装客户地址
                CustomerInfo customerInfo = CustomerService.GetCustomerInfo(currentItemInfo.CustomerID);
                reportData.BaseReportData.CustomerEAddress = customerInfo.EAddress;
                #endregion

                #region 账单明细

                reportData.BillList = new List<CashBillReportData>();
                if (currentBillList != null && currentBillList.Count > 0)
                {
                    List<Guid> billIds = new List<Guid>();

                    foreach (var item in currentBillList)
                    {
                        billIds.Add(item.BillID);

                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = item.RefNo;
                        billItem.BillNo = item.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == item.Way ? item.WriteOffAmount.ToString("n") : (-item.WriteOffAmount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }

                    List<CurrencyBillList> billList = FinanceService.GetBillListByIds(billIds.ToArray(), LocalData.IsEnglish);
                    foreach (var bi in reportData.BillList)
                    {
                        CurrencyBillList billFind = (from d in billList where d.BillNO == bi.BillNo select d).Take(1).SingleOrDefault();
                        if (billFind != null)
                        {
                            bi.BLNo = billFind.BLNo;
                            bi.RefNo = billFind.OperationNO;
                            if (para.ParameterValue.ToUpper() == ("check_nj").ToUpper())
                            {
                                bi.BillNo = billFind.BillOrCustomerRefNo;
                            }
                        }
                    }
                }     

                if (expensesList != null && expensesList.Count > 0)
                {
                    foreach (var charge in expensesList)
                    {
                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = charge.GLDescription;
                        billItem.BillNo = charge.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == charge.Way ? charge.Amount.ToString("n") : (-charge.Amount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }
                }

                #endregion

                if (para.ParameterValue.ToUpper() == ("check_nj").ToUpper())
                {
                    fileName += "RPT_Check_NJ.frx";
                    titleString = "NJ";
                }
                else if(para.ParameterValue.ToUpper() == ("check_la").ToUpper())
                {
                    fileName += "RPT_Check_NJ.frx";
                    titleString = "LA";
                }
                else if (para.ParameterValue.ToUpper() == ("check_jdy").ToUpper())
                {
                    fileName += "RPT_Check_JDY.frx";
                    titleString = "JDY";
                }
                else
                {
                    if (currentItemInfo.Way == FeeWay.AR)
                    {
                        fileName += "RPT_Check_Pacgran.frx";
                    }
                    else
                    {
                        fileName += "RPT_Check_PacgranAP.frx";
                    }

                    titleString = "Pring Pacgran Check";
                }

                reportSource.Add("BaseReportData", reportData.BaseReportData);
                if (reportData.BillList != null)
                {
                    reportSource.Add("BillListReportData", reportData.BillList);
                }
            }
            else if (currentItemInfo.Way == FeeWay.AP && para.ParameterValue.ToUpper() == ("check_ca").ToUpper())   //温哥华公司
            {
                reportData.BaseReportData.CheckDate = currentItemInfo.CheckDate.ToString("MMMM dd,yyyy");
                reportData.BaseReportData.CurrencyName = CurrentRow.Currency;
                #region 账单
                //List<OperationCurrencyAmountList> currentCurrencyAmountList = UCAccountListInfo.DataSource as List<OperationCurrencyAmountList>;
                //reportData.BaseReportData.CurrencyName = currencyList[currentCurrencyAmountList[0].CurrencyID];
                reportData.BillList = new List<CashBillReportData>();
                List<Guid> ids = new List<Guid>();
                foreach (var item in currentBillList)
                {
                    ids.Add(item.BillID);
                    CashBillReportData billItem = new CashBillReportData();
                    billItem.RefNo = item.RefNo;
                    billItem.BillNo = item.BillNo;
                    billItem.WriteOffAmount = currentItemInfo.Way == item.Way ? item.WriteOffAmount.ToString("n") : (-item.WriteOffAmount).ToString("n");
                    reportData.BillList.Add(billItem);
                }

                List<CurrencyBillList> billList = FinanceService.GetBillListByIds(ids.ToArray(), LocalData.IsEnglish);
                foreach (var bi in reportData.BillList)
                {
                    CurrencyBillList billFind = (from d in billList where d.BillNO == bi.BillNo select d).Take(1).SingleOrDefault();
                    if (billFind != null)
                    {
                        bi.BillNo = billFind.BillOrCustomerRefNo;
                        bi.BLNo = billFind.BLNo;
                        bi.RefNo = billFind.OperationNO;
                    }
                }

                if (expensesList != null && expensesList.Count > 0)
                {
                    foreach (var charge in expensesList)
                    {
                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = charge.GLDescription;
                        billItem.BillNo = charge.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == charge.Way ? charge.Amount.ToString("n") : (-charge.Amount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }
                }

                int count = reportData.BillList.Count;
                if (count > 15 && count < 31)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        reportData.BaseReportData.CustomerRefNO1 += reportData.BillList[i].BillNo + "\r\n";
                        reportData.BaseReportData.Description1 += reportData.BillList[i].BLNo + "\r\n";
                        reportData.BaseReportData.Amount1 += Convert.ToDecimal(reportData.BillList[i].WriteOffAmount.ToString()).ToString("N") + "\r\n";
                    }
                    for (int i = 15; i < count; i++)
                    {
                        reportData.BaseReportData.CustomerRefNO2 += reportData.BillList[i].BillNo + "\r\n";
                        reportData.BaseReportData.Description2 += reportData.BillList[i].BLNo + "\r\n";
                        reportData.BaseReportData.Amount2 += Convert.ToDecimal(reportData.BillList[i].WriteOffAmount.ToString()).ToString("N") + "\r\n";
                    }
                }
                else if (count >= 0 && count < 16)
                {
                    for (int i = 0; i < count; i++)
                    {
                        reportData.BaseReportData.CustomerRefNO1 += reportData.BillList[i].BillNo + "\r\n";
                        reportData.BaseReportData.Description1 += reportData.BillList[i].BLNo + "\r\n";
                        reportData.BaseReportData.Amount1 += Convert.ToDecimal(reportData.BillList[i].WriteOffAmount.ToString()).ToString("N") + "\r\n";
                    }
                }
                else
                {
                    //ShowReport = "true";
                }
                
                #endregion

                fileName += "RPT_Check_CA.frx";
                titleString = "CA";
                reportSource.Add("BaseReportData", reportData.BaseReportData);
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(titleString, (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            viewer.BindData(fileName, reportSource, null);
        }

        #endregion

        #endregion

        #region 打印凭证

        [CommandHandler(WriteOffCommands.Command_CredentialsPrint)]
        public void Command_CredentialsPrint(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                //if (CurrentRow == null)
                //{
                //    return;
                //}
                ////凭证数据源
                //List<PrintLedgerMasterReports> hdList = new List<PrintLedgerMasterReports>();
                //PrintLedgerMasterReports obj = FinanceService.GetPrintLedgerReportDateByRefID(CurrentRow.CheckID, LocalData.IsEnglish);
                //if (obj == null)
                //    return;
                //hdList.Add(obj);

                //LedgerPrint.Print(Workitem, ReportViewService, hdList);

                //销账凭证数据源
                PrintLedgerMasterReports hdObj = new PrintLedgerMasterReports();

                #region  获得数据

                hdObj = FinanceService.GetPrintLedgerReportDateByRefID(CurrentRow.CheckID, LocalData.IsEnglish);

                #endregion

                if (hdObj == null) return;
                IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Credentials Print" : "打印凭证",
                    (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

                string fileName = Application.StartupPath + "\\Reports\\FAM\\RptCredentialsList_CN.frx";
                Dictionary<string, object> reportSource = new Dictionary<string, object>();

                reportSource.Add("HdReportSource", hdObj);
                reportSource.Add("DtlReportSource", hdObj.DetailList);
                viewer.BindData(fileName, reportSource, null);
            }
        }

        #endregion

        #region 全选
        [CommandHandler(WriteOffCommands.Command_AllCheck)]
        public void Command_AllCheck(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                #region 没有选中当前行的、默认全选当前公司的、应收的、未到账的

                selectList.Clear();

                foreach (WriteOffItemList item in DataList)
                {
                    if (item.Type == FeeWay.AR && !item.HasRecoginized && item.CompanyID == LocalData.UserInfo.DefaultCompanyID)
                    {
                        item.IsCheck = true;

                        selectList.Add(item.ID, item);
                    }
                    else
                    {
                        item.IsCheck = false;
                    }
                }

                bsList.ResetBindings(false);

                if (Selected != null)
                {
                    Selected(this, selectList.Values.ToList());
                }
                #endregion
            }
            else
            {
                #region 有选中当前行,选中所有跟当前行公司的、应收的、到账信息的一样的
                selectList.Clear();

                foreach (WriteOffItemList item in DataList)
                {
                    if (item.Type == CurrentRow.Type && item.HasRecoginized == CurrentRow.HasRecoginized && item.CompanyID == CurrentRow.CompanyID && item.IsAuditor == CurrentRow.IsAuditor)
                    {
                        item.IsCheck = true;

                        selectList.Add(item.ID, item);
                    }
                    else
                    {
                        item.IsCheck = false;
                    }
                }

                bsList.ResetBindings(false);

                if (Selected != null)
                {
                    Selected(this, selectList.Values.ToList());
                }
                #endregion
            }
        }
        #endregion

        #region 解锁
        private bool IsUntieLock(ConfigureInfo companyConfig,string no,DateTime? bankDate)
        {
            string message = string.Empty;
            if (bankDate == null)
            {
                if (LocalData.IsEnglish)
                {
                    message = string.Format("CheckNo[{0}] not bank Date", no);
                }
                else
                {
                    message = string.Format("销账单号[{0}]没有到帐日期,无法确定会计期间", no);
                }
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                return false;
            }
            //会计已关帐，不允许再解锁
            if (DataTypeHelper.GetDateTime(bankDate) <= DataTypeHelper.GetDateTime(companyConfig.AccountingClosingdate))
            {
                if (LocalData.IsEnglish)
                {
                    message = string.Format("CheckNo[{0}] bank Date [{1}] in accounting Closing [{2}] period, does not allow unlocked", no, bankDate.Value.ToShortDateString(), companyConfig.AccountingClosingdate.Value.ToShortDateString());
                }
                else
                {
                    message = string.Format("销账单号[{0}]的到账日期[{1}]在会计关帐[{2}]期内,不允许解锁", no, bankDate.Value.ToShortDateString(), companyConfig.AccountingClosingdate.Value.ToShortDateString());
                }
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),message);
                return false;
            }
            //计费还没有关帐的，不需要解锁
            if (DataTypeHelper.GetDateTime(bankDate).Date> DataTypeHelper.GetDateTime(companyConfig.ChargingClosingdate).Date)
            {
                if (LocalData.IsEnglish)
                {
                    message = string.Format("CheckNo[{0}] {0} bank Date [{1}] is not charging Closing [{2}] period, no need to unlock", no, bankDate.Value.ToShortDateString(), companyConfig.ChargingClosingdate.Value.ToShortDateString());
                }
                else
                {
                    message = string.Format("销账单号[{0}]的到账日期[{1}]在不在计费关帐[{2}]期内,不需要解锁", no, bankDate.Value.ToShortDateString(), companyConfig.ChargingClosingdate.Value.ToShortDateString());
                }
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), message);
                return false;
            }
            return true;
        }
        [CommandHandler(WriteOffCommands.Command_UntieLock)]
        public void Command_UntieLock(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            ConfigureInfo companyConfig=ConfigureService.GetCompanyConfigureInfo(CurrentRow.CompanyID);
            
            List<Guid> checkIDs = new List<Guid>();
            List<DateTime?> checkUpdates = new List<DateTime?>();
            List<Guid> checkAmountIDs = new List<Guid>();
            List<DateTime?> checkAmountUpdates = new List<DateTime?>();

            int[] selectRowsHandle = gvWriteOffList.GetSelectedRows();
            foreach (var row in selectRowsHandle)
            {
                WriteOffItemList item = gvWriteOffList.GetRow(row) as WriteOffItemList;
                if (!IsUntieLock(companyConfig, item.No, item.ReachedDate))
                {
                    return;
                }
                if (!checkIDs.Contains(item.CheckID))
                {
                    checkIDs.Add(item.CheckID);
                    checkUpdates.Add(item.CheckUpdateDate);
                }
                checkAmountIDs.Add(item.ID);
                checkAmountUpdates.Add(item.UpdateDate);
     
            }
            if (!checkIDs.Contains(CurrentRow.CheckID))
            {
                if (!IsUntieLock(companyConfig, CurrentRow.No, CurrentRow.ReachedDate))
                {
                    return;
                }
                checkIDs.Add(CurrentRow.CheckID);
                checkUpdates.Add(CurrentRow.CheckUpdateDate);
            }
            if (!checkAmountIDs.Contains(CurrentRow.ID))
            {
                checkAmountIDs.Add(CurrentRow.ID);
                checkAmountUpdates.Add(CurrentRow.UpdateDate);
            }
            //提示是否解锁
            if (!FAMUtility.ShowResultMessage(NativeLanguageService.GetText(this, "1411068888888")))
            {
                return;
            }
            try
            {

                List<UntieLockCheckResult> result = FinanceService.UntieLockChecks(checkIDs.ToArray(),
                                                                checkUpdates.ToArray(),
                                                                checkAmountIDs.ToArray(),
                                                                checkAmountUpdates.ToArray(),
                                                                LocalData.UserInfo.LoginID);

                foreach (UntieLockCheckResult item in result)
                {
                    if (item.Type == 1)
                    {
                        List<WriteOffItemList> list = (from d in DataList where d.CheckID == item.ID select d).ToList();
                        foreach (WriteOffItemList check in list)
                        {
                            //设置Check中的审核、凭证号信息
                            check.CheckUpdateDate = item.UpdateDate;
                            check.VoucherSeqNo = string.Empty;
                            check.ApprovalByName = string.Empty;
                            check.AuditByID = null;
                        }
                    }
                    else if (item.Type == 2)
                    {
                        List<WriteOffItemList> list = (from d in DataList where d.ID == item.ID select d).ToList();
                        foreach (WriteOffItemList check in list)
                        {
                            //设置CheckAmount中的到账信息
                            check.UpdateDate = item.UpdateDate;
                            check.ReachedByID = null;
                            check.ReachedDate = null;
                            check.BankByName = string.Empty;
                        }
                    }
                }

                bsList.ResetBindings(false);
                if (CurrentChanged != null)
                {
                    CurrentChanged(this, CurrentRow);
                }

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "UntieLock Success!" : "解锁成功!");
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(),ex);
            }

        }
        #endregion


        #region 直连支付
        /// <summary>
        /// 直连支付
        /// </summary>
        [CommandHandler(WriteOffCommands.Command_DirectBank)]
        public void Command_DirectBank(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            string title = LocalData.IsEnglish ? "Single Payment" : "单笔支付";
            PartLoader.ShowEditPart<UCSinglePayment>(Workitem, CurrentRow.ID, EditMode.New, null, title, null, "" + CurrentRow.ID);
        }
        #endregion
        #endregion

        #region 移除/清空已选择的列表时，将主列表的勾选数据刷新
        /// <summary>
        /// 多选列表中、移除或清空已选择的列表时，刷新主列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public void MultiListRemoveData(object sender, object data)
        {
            if (sender.ToString() == "Remove")
            {

                Guid removeID = new Guid(data.ToString());

                var items = (from w in DataList where w.ID == removeID select w);

                foreach (var itme in items)
                {
                    WriteOffItemList writeOffItem = itme as WriteOffItemList;
                    if (writeOffItem != null)
                    {
                        writeOffItem.IsCheck = false;

                        bsList.ResetBindings(false);

                        if (selectList.Keys.Contains(writeOffItem.ID))
                        {
                            selectList.Remove(writeOffItem.ID);
                        }
                    }

                }


            }
            else if (sender.ToString() == "Clear")
            {
                selectList.Clear();

                DataList.ForEach(o => o.IsCheck = false);

                bsList.ResetBindings(false);
            }
            else
            {
                return;
            }

        }

        #endregion

        #region 分页
        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            if (InvokeGetData != null)
            {
                _dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, _dataPageInfo);
            }
        }
        private void gvWriteOffList_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            if (DataList == null || DataList.Count == 0)
            {
                return;
            }
            if (InvokeGetData != null)
            {
                e.Cancel = true;

                _dataPageInfo.CurrentPage = 1;
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
        #endregion

        #region 热键
        public new event KeyEventHandler KeyDown;
        private void gvWriteOffList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && CurrentRow != null)
            {
                Workitem.Commands[WriteOffCommands.Command_EditData].Execute();
            }
            else if (KeyDown != null
              && e.KeyCode == Keys.F5
              && gvWriteOffList.FocusedColumn != null
              && gvWriteOffList.FocusedValue != null)
            {
                string text = gvWriteOffList.GetFocusedDisplayText();
                Dictionary<string, object> keyValue = new Dictionary<string, object>();
                keyValue.Add(gvWriteOffList.FocusedColumn.FieldName, text);
                KeyDown(keyValue, null);
            }
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
            {
                Workitem.Commands[WriteOffCommands.Command_ShowSearch].Execute();
            }
        }
        #endregion

        #region 刷新
        /// <summary>
        /// 在多列表中操作时，刷新主列表数据
        /// </summary>
        /// <param name="operatingType"></param>
        /// <param name="data"></param>
        public void RefreshUI(object operatingType, object data)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<WriteOffItemList> list = data as List<WriteOffItemList>;

                if (DataList == null || DataList.Count == 0)
                {
                    return;
                }

                if (operatingType.ToString() == "Reached" || operatingType.ToString() == "CancelReached")
                {
                    //到账与取消到账
                    foreach (WriteOffItemList itemData in list)
                    {
                        WriteOffItemList tager = DataList.Find(delegate(WriteOffItemList item) { return item.ID == itemData.ID; });
                        if (tager != null)
                        {
                            FAMUtility.CopyToValue(data, tager, typeof(BankList));
                            if (operatingType.ToString() == "Reached")
                            {
                                tager.Amount = itemData.FinalAmount;
                            }
                            bsList.ResetItem(bsList.IndexOf(tager));
                        }
                    }
                }
                else if (operatingType.ToString() == "Auditor" || operatingType.ToString() == "UnAuditor")
                {
                    //审核与取消审核，从数据库中取得数据，更新
                    List<Guid> checkID = (from d in list group d by d.CheckID into g select g.Key).ToList();

                    RefreshUI(checkID);
                }
            }
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="list"></param>
        public void RefreshUI(List<Guid> idList)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                List<WriteOffItemList> list = FinanceService.GetWriteOffListByIds(idList.ToArray());
                foreach (WriteOffItemList item in list)
                {
                    WriteOffItemList tager = DataList.Find(delegate(WriteOffItemList data) { return item.ID == data.ID; });
                    if (tager != null)
                    {
                        FAMUtility.CopyToValue(item, tager, typeof(WriteOffItemList));

                        if (selectList.Keys.Contains(tager.ID))
                        {
                            tager.IsCheck = true;
                        }
                        bsList.ResetItem(bsList.IndexOf(tager));
                    }
                }
            }
        }
        /// <summary>
        /// 刷新已选择列表中的数据
        /// </summary>
        public void RefreshSelectList()
        {
            Dictionary<Guid, WriteOffItemList> idList = new Dictionary<Guid, WriteOffItemList>();

            //先找到需要进行更新的纪录
            foreach (Guid id in selectList.Keys)
            {
                WriteOffItemList tager = DataList.Find(delegate(WriteOffItemList item) { return item.ID == id; });
                if (tager != null)
                {
                    idList.Add(tager.ID, tager);
                }
            }
            //更新信息
            foreach (Guid id in idList.Keys)
            {
                if (selectList.Keys.Contains(id))
                {
                    selectList[id] = idList[id];
                }
            }

            if (idList.Keys.Count > 0)
            {
                Selected(this, selectList.Values.ToList());
            }

        }
        #endregion



    }
}
