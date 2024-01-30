using DevExpress.Data;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.FAM.UI
{  
    /// <summary>
    /// 代理对账列表界面
    /// </summary>
    public partial class AgentBillCheckingList : BaseListPart
    {

        public AgentBillCheckingList()
        {
            InitializeComponent();
            Disposed += delegate {
                gcMain.DataSource = null;
                dataPageInfo = null;
                bsList.PositionChanged -= bsList_PositionChanged;
                bsList.DataSource = null;
                bsList.Dispose();
                InvokeGetData = null;
                CurrentChanged = null;
                InvokeGetData = null;
                if (Workitem != null)
                {
                    Workitem.Items.Remove(this);
                    Workitem = null;
                }
            
            
            };
            
        }

        #region 服务
        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }
        #endregion

        #region 重写

        public List<AgnetBillCheckList> DataSourceList
        {
            get
            {
                List<AgnetBillCheckList> list = DataSource as List<AgnetBillCheckList>;

                if (list == null)
                {
                    list = new List<AgnetBillCheckList>();
                }
                return list;
            }
        }

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
        /// 绑定数据源
        /// </summary>
        /// <param name="value"></param>
        private void BindingData(object value)
        {
            PageList source = value as PageList;
            if (source == null || source.GetList<AgnetBillCheckList>().Count == 0)
            {
                bsList.DataSource = new List<AgnetBillCheckList>();
                pageControl1.TotalPage = 0;
                pageControl1.CurrentPage = 0;
                gvMain.SortInfo.Clear();
            }
            else
            {
                bsList.DataSource = source.GetList <AgnetBillCheckList>();
                bsList.ResetBindings(false);
                dataPageInfo = source.DataPageInfo;
                if (source.DataPageInfo != null)
                {
                    int pageCount = source.DataPageInfo.TotalCount / source.DataPageInfo.PageSize;
                    if (pageCount == 1 && source.DataPageInfo.TotalCount > source.DataPageInfo.PageSize)
                    {
                        pageCount = 2;
                    }
                    if (pageCount == 0 && source.DataPageInfo.TotalCount > 0)
                    {
                        pageCount = 1;
                    }
                    pageControl1.TotalPage = pageCount;

                    pageControl1.CurrentPage = source.DataPageInfo.CurrentPage;
                    gvMain.SortInfo.Clear();
                    ColumnSortOrder sortOrder = source.DataPageInfo.SortOrderType == SortOrderType.Asc ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;
                    GridColumn col = null;
                    for (int i = 0; i < gvMain.Columns.Count; i++)
                    {
                        if (gvMain.Columns[i].FieldName == source.DataPageInfo.SortByName) col = gvMain.Columns[i];
                    }
                    gvMain.SortInfo.Clear();
                    if (col != null)
                        gvMain.SortInfo.Add(new GridColumnSortInfo(col, sortOrder));
                }
            }
            
        }

        public override object Current
        {
            get
            {
                return bsList.Current;
            }
        }

        public override event InvokeGetDataHandler InvokeGetData;

        public override event CurrentChangedHandler CurrentChanged;



        #endregion

        #region 初始化
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                InitControls();
            }
        }

        private void InitControls()
        {
            FAMUtility.ShowGridRowNo(gvMain);

            List<EnumHelper.ListItem<AgentBillCheckStatusEnum>> checkStatus = EnumHelper.GetEnumValues<AgentBillCheckStatusEnum>(LocalData.IsEnglish);
            foreach (var item in checkStatus)
            {
                cmbStatus.Properties.Items.Add(new ImageComboBoxItem(item.Name, item.Value));
            }
            //初始化消息
            RegisterMessage("1107290001", LocalData.IsEnglish ? "Verifing Bill is done,it could not be deleted" : "对帐单已经对账,无法删除");
        }

        #endregion

        #region 本地变量
        /// <summary>
        /// 当前行
        /// </summary>
        private AgnetBillCheckList CurrentRow
        {
            get
            {
                return bsList.Current as AgnetBillCheckList;
            }
        }
        /// <summary>
        /// 分页信息
        /// </summary>
        private DataPageInfo dataPageInfo=new DataPageInfo();
        #endregion

        #region 命令
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(ABCCommandConstants.Command_ABCAdd)]
        public void Command_AddData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                PartLoader.ShowEditPart<AgentBillCheckingEdit>(Workitem, null, LocalData.IsEnglish ? "Add AgentBillChecking" : "新增代理对账单", EditPartSaved);
            }
        }
        /// <summary>
        /// 打开 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(ABCCommandConstants.Command_ABCOpen)]
        public void Command_OpenData(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (CurrentRow == null)
                {
                    return;
                }
                string title = LocalData.IsEnglish ? "Edit AgentBillChecking" : "编辑对账单";

                if (CurrentRow.Status != AgentBillCheckStatusEnum.Created)
                {
                    title = LocalData.IsEnglish ? "View AgentBillChecking" : "查看对账单";
                }
                PartLoader.ShowEditPart<AgentBillCheckingEdit>(Workitem, CurrentRow, title, EditPartSaved);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(ABCCommandConstants.Command_ABCDelete)]
        public void Command_DeleteData(object sender, EventArgs e)
        {
            if (CurrentRow == null)
            {
                return;
            }
            if (!FAMUtility.EnquireIsDeleteCurrentData())
            {
                return;
            }
            if (CurrentRow.Status != AgentBillCheckStatusEnum.Created)
            {
                FAMUtility.ShowMessage(NativeLanguageService.GetText(this, "1107290001"));
                return;
            }

            try
            {
                FinanceService.RemoveAgnetBillCheck(CurrentRow.ID,LocalData.UserInfo.LoginID,CurrentRow.UpdateDate,LocalData.IsEnglish);

                bsList.RemoveCurrent();

                bsList.ResetBindings(false);
            }
            catch(Exception ex)
            {
                  LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
            }

        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 编辑界面保存数据
        /// </summary>
        /// <param name="prams"></param>
        private void EditPartSaved(object[] prams)
        {
            if (prams == null || prams.Length == 0) return;

            AgnetBillCheckList data = prams[0] as AgnetBillCheckList;

            List<AgnetBillCheckList> source = DataSource as List<AgnetBillCheckList>;


            if (source == null || source.Count == 0)
            {
                DataSource = new List<AgnetBillCheckList>();
                bsList.Add(data);
                bsList.ResetBindings(false);
            }
            else
            {
                AgnetBillCheckList tager = source.Find(delegate(AgnetBillCheckList item) { return item.ID == data.ID; });
                if (tager == null)
                {

                    bsList.Insert(0, data);
                    bsList.ResetBindings(false);
                }
                else
                {
                    FAMUtility.CopyToValue(data, tager, typeof(AgnetBillCheckList));

                    bsList.ResetItem(bsList.IndexOf(tager));
                }

            }
            if (CurrentChanged != null)
            {
                CurrentChanged(this, CurrentRow);
            }
        }
        #endregion

        #region Grid事件
        /// <summary>
        /// 选择行发生改变
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
        /// <summary>
        /// 行样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_RowStyle(object sender, RowStyleEventArgs e)
        {
              if (e.RowHandle < 0) return;
              AgnetBillCheckList list = gvMain.GetRow(e.RowHandle) as AgnetBillCheckList;
              if (list == null)
              {
                  return;
              }
              if (list.Status == AgentBillCheckStatusEnum.Checking)
              {
                  GridHelper.SetColorStyle(e.Appearance, PresenceStyle.NewLine);
              }
              else if (list.Status == AgentBillCheckStatusEnum.NotifiedBillOwner)
              {
                  GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Error);
              }
              else if (list.Status == AgentBillCheckStatusEnum.Completed)
              {
                  GridHelper.SetColorStyle(e.Appearance, PresenceStyle.Confirmed);
              }

        }
        /// <summary>
        /// 双击打开 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_DoubleClick(object sender, EventArgs e)
        {
            Workitem.Commands[ABCCommandConstants.Command_ABCOpen].Execute();
        }

        #endregion

        #region 分页
        /// <summary>
        /// 分页工具中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageControl1_PageChanged(object sender, PageChangedEventArgs e)
        {
            if (InvokeGetData != null)
            {
                dataPageInfo.CurrentPage = e.CurrentPage;
                InvokeGetData(this, dataPageInfo);
            }
        }
        /// <summary>
        /// 排序分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CustomerSorting(object sender, SortingCancelEventArgs e)
        {
            if (DataSourceList == null || DataSourceList.Count == 0)
            {
                return;
            }
            if (InvokeGetData != null)
            {
                e.Cancel = true;
                if (e.Column == colBillCheckUnit)
                {
                    dataPageInfo.SortByName = " LaunchCompanyName,LaunchUserName,CheckCompanyName,CheckUserName ";
                }
                else if (e.Column == colBillCheckRange)
                {
                    dataPageInfo.SortByName = " OperTexts,EndingETD ";
                }
                else
                {
                    dataPageInfo.SortByName = e.Column.FieldName;
                }
               
                if (e.ColumnSortOrder == ColumnSortOrder.Ascending ||
                    e.ColumnSortOrder == ColumnSortOrder.None)
                {
                    dataPageInfo.SortOrderType = SortOrderType.Desc;
                }
                else
                {
                    dataPageInfo.SortOrderType = SortOrderType.Asc;
                }
                InvokeGetData(this, dataPageInfo);
            }
        }
        #endregion

 

        private void gcMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F6 && CurrentRow != null)
            {
                Workitem.Commands[ABCCommandConstants.Command_ABCShowSearch].Execute();
            }
        }

    }
}
